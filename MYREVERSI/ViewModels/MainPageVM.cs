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

        private readonly MainPageML mainPageML = new();

        public ICommand AddGameCommand => new Command(AddGame);

        public bool IsBusy => games.IsBusy;

        public ObservableCollection<Game>? GamesList => games.GamesList;

        public ICommand InstructionsCommand { get; private set; }

        public Game? SelectedItem
        {
            get =>  games.CurrentGame;

            set
            {
                if (value != null)
                {
                    games.CurrentGame = value;
                    MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Shell.Current.Navigation.PushAsync(new GamePage(value), true);
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
            InstructionsCommand = new Command(ShowInstructionsPrompt);
            games.OnGameAdded += OnGameAdded;
            games.OnGamesChanged += OnGamesChanged;
        }

        public void ShowInstructionsPrompt(object obj)
        {
            mainPageML.ShowInstructionsPrompt(obj);
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
