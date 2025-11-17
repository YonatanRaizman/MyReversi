using Plugin.CloudFirestore;
using MyReversi.ModelsLogic;
using Plugin.CloudFirestore.Attributes;

namespace MyReversi.Models
{
    public abstract class GameModel
    {
        protected FbData fbd = new();
        [Ignored]
        protected IListenerRegistration? ilr;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        protected abstract GameStatus Status { get; }
        protected string[,]? gameBoard;
        protected IndexedButton[,]? gameButtons;
        [Ignored]
        public string StatusMessage => Status.StatusMessage;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; } = false;
        [Ignored] 

        public abstract string OpponentName { get; }
        [Ignored]

        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public bool IsHostUser { get; set; }

        public string GuestName { get; set; } = string.Empty;

        public abstract void SetDocument(Action<Task> OnComplete);
        protected abstract void UpdateStatus();
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
        public abstract void DeleteDocument(Action<Task> OnComplete);
        public abstract void InitGame(Grid board);
        protected abstract void OnButtonClicked(object? sender, EventArgs e);
        protected abstract void Play(int rowIndex, int columnIndex);
    }
}
