using MyReversi.Models;
using Plugin.CloudFirestore;

namespace MyReversi.ModelsLogic
{
    public partial class Game : GameModel
    {
        public override string OpponentName => IsHostUser? GuestName : HostName;

        internal Game(GameSize selectedGameSize)
        {
            HostName = new User().Name;
            RowSize = selectedGameSize.Size;
            Created = DateTime.Now;
        }

        public override void SetDocument(Action<System.Threading.Tasks.Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }

        public Game()
        {
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
    }
}
