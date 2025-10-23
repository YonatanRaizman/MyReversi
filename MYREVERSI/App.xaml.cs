using MyReversi.Views;

namespace MyReversi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RegisterPage();
        }
    }
}
