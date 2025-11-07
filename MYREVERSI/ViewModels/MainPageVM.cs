using MyReversi.Models;
using MyReversi.ModelsLogic;
using MyReversi.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyReversi.ViewModels
{
    internal partial class MainPageVM : ObservableObject
    {
        private readonly Games games = new();

        public ICommand AddGameCommand => new Command(AddGame);

        public bool IsBusy => games.IsBusy;

        public ObservableCollection<GameSize>? GameSizes { get => games.GameSizes; set => games.GameSizes = value; }

        public GameSize SelectedGameSize { get => games.SelectedGameSize; set => games.SelectedGameSize = value; }

        public ObservableCollection<Game>? GamesList => games.GamesList;

        public Game SelectedItem
        {
            get =>  games.CurrentGame;

            set
            {
                if (value != null)
                {
                    games.CurrentGame = value;
                    MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Shell.Current.Navigation.PushAsync(new GamePage(value));
                    });
                }
            }
        }

        private void AddGame()
        {
            games.AddGame();
            OnPropertyChanged(nameof(IsBusy));
        }

        public MainPageVM()
        {
            games.OnGameAdded += OnGameAdded;
            games.OnGamesChanged += OnGamesChanged;
        }

        private void OnGameAdded(object? sender, Game game)
        {
            OnPropertyChanged(nameof(IsBusy));
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.Navigation.PushAsync(new GamePage(game), true);
            });
        }

        private void OnGamesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GamesList));
        }

        internal void AddSnapshotListener()
        {
            games.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            games.RemoveSnapshotListener();
        }
    }
}
