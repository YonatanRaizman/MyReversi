using MyReversi.ViewModels;

namespace MyReversi.NewFolder;
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageVM();
        }
    }
