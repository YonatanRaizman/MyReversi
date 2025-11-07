using MyReversi.Models;
using MyReversi.ModelsLogic;
using System.Windows.Input;

namespace MyReversi.ViewModels
{
    internal class LoginPageVM : ObservableObject
    {
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsPassword { get; set; } = true;
        public ICommand LoginCommand { get; }
        private readonly User user = new();
        public bool CanLogin()
        {
            return user.CanLogin();
        }

        public LoginPageVM()
        {
            LoginCommand = new Command(Login, CanLogin);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            user.OnAuthCompleted += OnAuthComplete;
        }
        private void OnAuthComplete(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new MainPage();
                }
            });
        }
        private void Login()
        {
            user.Login();
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }


        public string UserName
        {
            get => user.Name;
            set
            {
                user.Name = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }

        }

        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }

        }

        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }

        }
    }
}
