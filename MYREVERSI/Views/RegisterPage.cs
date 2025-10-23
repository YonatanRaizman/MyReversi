using MyReversi.ViewModels;

namespace MYREVERSI.Views
{
    internal class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterPageVM();
        }
    }
}
