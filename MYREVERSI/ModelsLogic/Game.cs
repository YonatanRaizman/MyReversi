using MyReversi.Models;
using Plugin.CloudFirestore;

namespace MyReversi.ModelsLogic
{
    public class Game : GameModel
    {
        public override string OpponentName => IsHostUser? GuestName : HostName;
        protected override GameStatus Status => IsHostUser && IsHostTurn || !IsHostUser && !IsHostTurn ?
            new GameStatus { CurrentStatus = GameStatus.Status.Play } :
            new GameStatus { CurrentStatus = GameStatus.Status.Wait };

        public Game()
        {
            HostName = new User().Name;
            IsHostUser = true;
            Created = DateTime.Now;
        }

        protected override void UpdateStatus()
        {
            Status.CurrentStatus = IsHostUser && IsHostTurn || !IsHostUser && !IsHostTurn ?
                GameStatus.Status.Play : GameStatus.Status.Wait;
        }

        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }

        public void UpdateGuestUser(Action<Task> OnComplete)
        {
            GuestName = MyName;
            IsFull = true;
            UpdateFbJoinGame(OnComplete);
        }

        private void UpdateFbJoinGame(Action<Task> OnComplete)
        {
            Dictionary<string, object> dict = new()
            {
                { nameof(GuestName), GuestName },
                { nameof(IsFull), IsFull }
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

        private void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            Game? updatedGame = snapshot?.ToObject<Game>();
            if (updatedGame != null)
            {
                GuestName = updatedGame.GuestName;
                IsFull = updatedGame.IsFull;
                OnGameChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        public override void DeleteDocument(Action<Task> OnComplete)
        {
            fbd.DeleteDocument(Keys.GamesCollection, Id, OnComplete);
        }

        public override void InitGame(Grid board)
        {
            gameBoard = new string[8,8];
            gameButtons = new IndexedButton[8,8];
            IndexedButton btn;

            for (int i = 0; i < 8; i++)
            {
                board.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                board.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    btn = new IndexedButton(i, j);
                    gameButtons[i, j] = btn;
                    btn.Clicked += OnButtonClicked;
                    board.Add(btn, j, i);

                    btn.BackgroundColor = Color.FromArgb("#008000");

                    btn.BorderColor = Colors.Black;

                    btn.BorderWidth = 1;
                    btn.CornerRadius = 6;

                    btn.BorderColor = Colors.White;
                    btn.BorderWidth = 1;
                }
            }
        }

        protected override void OnButtonClicked(object? sender, EventArgs e)
        {
            IndexedButton? btn = sender as IndexedButton;

            if (btn != null)
            {
                Play(btn!.RowIndex, btn.ColumnIndex);
            }
        }

        protected override void Play(int rowIndex, int columnIndex)
        {
            gameButtons![rowIndex, columnIndex].Text = IsHostUser ? "⚫" : "⚪";
        }
    }
}
