using MyReversi.ModelsLogic;
using MyReversi.ViewModels;

namespace MyReversi.Views;

partial class GamePage : ContentPage
{
    public GamePage(Game game)
	{
		InitializeComponent();
		BindingContext = new GamePageVM(game);
	}
}