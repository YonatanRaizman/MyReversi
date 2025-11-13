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
        public EventHandler<Game>? OnGameAdded;
        public EventHandler? OnGamesChanged;
        protected Game? currentGame;
        public Game? CurrentGame { get => CurrentGame; set => currentGame = value; }
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
    }
}
