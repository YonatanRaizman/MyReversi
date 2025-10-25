using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyReversi.Models;
using System.Threading.Tasks;

namespace MyReversi.ModelsLogic
{
    internal class User : UserModel /// change
    {
        public Action<object?, EventArgs> OnAuthCompleted { get; internal set; }

        public override void Register()
        {
            fbd.CreateUserWithEmailAndPasswordAsync(Email, Password,Name,OnComplete);
        }

        private void OnComplete(Task task)
        {
            if (task.IsCompletedSuccessfully)
            {
                SaveToPreferences();
                OnAuthCompleted?.Invoke(this, EventArgs.Empty);
            }
            else if (task.Exception != null)
            {
                string msg = task.Exception.Message;
                ShowAlert(GetFirebaseErrorMessage(msg));
            }
            else
                ShowAlert(Strings.RegistrationFailed);
        }

        private static void ShowAlert(string msg)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(msg,ToastDuration.Long).Show();
            });
        }

        public override string GetFirebaseErrorMessage(string msg)
        {
            if (msg.Contains(Strings.ErrMessageReason))
            {
                if (msg.Contains(Strings.EmailExists))
                    return Strings.EmailExistsErrorMessage;
                if (msg.Contains(Strings.InvalidEmailAddress))
                    return Strings.InvalidEmailErrorMessage;
                if (msg.Contains(Strings.WeakPassword))
                    return Strings.WeakPasswordErrorMessage;
            }
            return Strings.UnknownErrorMessage;
        }

        private void SaveToPreferences()
        {
            Preferences.Set(Keys.NameKey, Name);
            Preferences.Set(Keys.PasswordKey, Password);
            Preferences.Set(Keys.EmailKey, Email);
        }

        public override bool CanRegister()
        {
            return (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email));
        }
        public override void Login()
        {
            Preferences.Set(Keys.NameKey, Name);
            Preferences.Set(Keys.PasswordKey, Password);
            Preferences.Set(Keys.EmailKey, Email);
        }

        public override bool CanLogin()
        {
            return (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email));
        }

        public User()
        {
            Name = Preferences.Get(Keys.NameKey, string.Empty);
            Password = Preferences.Get(Keys.PasswordKey, string.Empty);
            Email = Preferences.Get(Keys.EmailKey, string.Empty);
        }
    }
}
