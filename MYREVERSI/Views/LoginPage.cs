using MyReversi.ViewModels;

namespace MYREVERSI.Views
{
    public partial class LoginPage : ContentPage
    {
        public void LogInPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageVM();
        }

        private void InitializeComponent()
        {
        }
    }
}
