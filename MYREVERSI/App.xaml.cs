using MyReversi.ModelsLogic;
using MyReversi.NewFolder;
using MyReversi.Views;

namespace MyReversi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            User user = new();
            Page page = user.IsRegistered ? new LoginPage() : new RegisterPage();
            MainPage = page;
        }
    }
}
