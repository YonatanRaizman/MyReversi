using MyReversi.ViewModels;

namespace MyReversi.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
        InitializeComponent();
        BindingContext = new RegisterPageVM();
    }
}