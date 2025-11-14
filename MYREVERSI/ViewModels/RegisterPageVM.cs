using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyReversi.Models;
using MyReversi.ModelsLogic;
using System.Windows.Input;

namespace MyReversi.ViewModels
{
    internal partial class RegisterPageVM : ObservableObject
    {
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsPassword { get; set; } = true;
        public ICommand RegisterCommand { get; }
        private readonly User user = new();

        public bool CanRegister()
        {
            return user.CanRegister();
        }

        public RegisterPageVM()
        {
            RegisterCommand = new Command(Register, CanRegister);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            user.OnAuthCompleted += OnAuthComplete;
            
        }

        private void OnAuthComplete(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                }
            });
        }

        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }
        private void Register()
        {
            user.Register();
        }
        public string UserName
        {
            get => user.Name;
            set
            {
                user.Name = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }

        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }

        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }

        }

        public string ForgotYourPassword
        {
            get => user.ForgotYourPassword;
            set
            {
                user.ForgotYourPassword = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
    }
}
