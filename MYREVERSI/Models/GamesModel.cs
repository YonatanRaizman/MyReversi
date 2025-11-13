using MyReversi.ModelsLogic;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;

namespace MyReversi.Models
{
    public abstract class GamesModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;

        public bool IsBusy { get; set; }
        public ObservableCollection<Game>? GamesList { get; set; } = [];
        public ObservableCollection<GameSize>? GameSizes { get; set; } = [new GameSize(3), new GameSize(4), new GameSize(5)];
        public EventHandler<Game>? OnGameAdded;
        public EventHandler? OnGamesChanged;
        public GameSize SelectedGameSize { get; set; } = new GameSize();
        protected Game? currentGame;
        public Game? CurrentGame { get => CurrentGame; set => currentGame = value; }
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
    }
}
