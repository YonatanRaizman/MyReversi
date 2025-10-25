using System.Windows.Input;
using MyReversi.ModelsLogic;

namespace MyReversi.ViewModels
{
    internal class MainPageVM
    {
        private readonly User user = new();
        public ICommand RegisterCommand { get; }

        public MainPageVM()
        {
            RegisterCommand = new Command(Register, CanRegister);
        }

        public bool CanRegister()
        {
            return !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password);
        }

        private void Register()
        {
            user.Register();
        }

        public string Name
        {
            get => user.Name;
            set
            {
                user.Name = value;
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

        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
    }
}
