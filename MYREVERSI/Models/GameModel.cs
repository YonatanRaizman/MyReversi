using Plugin.CloudFirestore;
using MyReversi.ModelsLogic;
using Plugin.CloudFirestore.Attributes;

namespace MyReversi.Models
{
    public abstract class GameModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        protected GameStatus _status = new();
        protected string[,]? gameBoard;
        protected IndexedButton[,]? gameButtons;
        protected string nextPlay = Strings.blackDisc;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        protected abstract GameStatus Status { get; }
        [Ignored]
        public string StatusMessage => Status.StatusMessage;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; } = false;
        public List<int> Move { get; set; } = [Keys.NoMove, Keys.NoMove];
        [Ignored]
        public abstract string OpponentName { get; }
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public bool IsHostUser { get; set; }
        private static readonly int[,] Directions = new int[,]
        {
            { -1, -1 }, { -1, 0 }, { -1, 1 },
            {  0, -1 },            {  0, 1 },
            {  1, -1 }, {  1, 0 }, {  1, 1 }
        };
        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void Init(Grid board);
        protected abstract void UpdateStatus();
        protected abstract void OnButtonClicked(object? sender, EventArgs e);
        protected abstract void Play(int rowIndex, int columnIndex, bool MyMove);
        protected abstract void UpdateFbMove();
        //protected abstract bool IsLegalMove(int row, int col, string disc);
        //protected abstract List<(int r, int c)> GetFlips(int row, int col, string disc);
        //protected abstract void ApplyMove(int row, int col, string disc);
    }
}
