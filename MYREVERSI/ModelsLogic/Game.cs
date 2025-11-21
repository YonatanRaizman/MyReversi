using CommunityToolkit.Maui.Alerts;
using MyReversi.Models;
using Plugin.CloudFirestore;

namespace MyReversi.ModelsLogic
{
    public class Game : GameModel
    {
        public override string OpponentName => IsHostUser ? GuestName : HostName;
        protected override GameStatus Status => _status;

        public Game()
        {
           
            UpdateStatus();
        }

        protected override void UpdateStatus()
        {
            _status.CurrentStatus = IsHostUser && IsHostTurn || !IsHostUser && !IsHostTurn ?
                GameStatus.Statuses.Play : GameStatus.Statuses.Wait;
        }

        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }

        public void UpdateGuestUser(Action<Task> OnComplete)
        {
            IsFull = true;
            GuestName = MyName;
            UpdateFbJoinGame(OnComplete);
        }

        private void UpdateFbJoinGame(Action<Task> OnComplete)
        {
            Dictionary<string, object> dict = new()
            {
                { nameof(IsFull), IsFull },
                { nameof(GuestName), GuestName }
            };
            fbd.UpdateFields(Keys.GamesCollection, Id, dict, OnComplete);
        }

        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.GamesCollection, Id, OnChange);
        }

        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
            DeleteDocument(OnComplete);
        }

        private void OnComplete(Task task)
        {
            if (task.IsCompletedSuccessfully)
                OnGameDeleted?.Invoke(this, EventArgs.Empty);
        }

        public override void DeleteDocument(Action<Task> OnComplete)
        {
            fbd.DeleteDocument(Keys.GamesCollection, Id, OnComplete);
        }

        public override void Init(Grid board)
        {
            gameBoard = new string[8, 8];
            gameButtons = new IndexedButton[8, 8];
            IndexedButton btn;
            for (int i = 0; i < 8; i++)
            {
                board.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                board.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    btn = new IndexedButton(i, j);
                    gameButtons[i, j] = btn;
                    btn.Clicked += OnButtonClicked;
                    board.Add(btn, j, i);
                }

        }

        protected override void OnButtonClicked(object? sender, EventArgs e)
        {
            if (_status.CurrentStatus == GameStatus.Statuses.Play)
            {
                IndexedButton? btn = sender as IndexedButton;
                if (btn!.Text == string.Empty)
                    Play(btn!.RowIndex, btn.ColumnIndex, true);
            }
        }

        protected override void Play(int rowIndex, int columnIndex, bool MyMove)
        {
            gameButtons![rowIndex, columnIndex].Text = nextPlay;
            gameBoard![rowIndex, columnIndex] = nextPlay;
            nextPlay = nextPlay == Strings.blackDisc ? Strings.whiteDisc : Strings.blackDisc;
            if (MyMove)
            {
                Move[0] = rowIndex;
                Move[1] = columnIndex;
                _status.UpdateStatus();
                IsHostTurn = !IsHostTurn;
                UpdateFbMove();
            }
        }

        protected override void UpdateFbMove()
        {
            Dictionary<string, object> dict = new()
            {
                { nameof(Move), Move },
                { nameof(IsHostTurn), IsHostTurn }
            };
            fbd.UpdateFields(Keys.GamesCollection, Id, dict, OnComplete);
        }

        private void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            Game? updatedGame = snapshot?.ToObject<Game>();
            if (updatedGame != null)
            {
                IsFull = updatedGame.IsFull;
                GuestName = updatedGame.GuestName;
                OnGameChanged?.Invoke(this, EventArgs.Empty);
                if (_status.CurrentStatus == GameStatus.Statuses.Play && updatedGame.Move[0] != Keys.NoMove)
                    Play(updatedGame.Move[0], updatedGame.Move[1], false);
            }
            else
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Shell.Current.Navigation.PopAsync();
                    Toast.Make(Strings.GameDeleted, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
                });
            }
        }
    }
}
