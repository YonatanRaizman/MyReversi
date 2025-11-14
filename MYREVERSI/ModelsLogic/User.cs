using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyReversi.Models;

namespace MyReversi.ModelsLogic
{
    internal class User : UserModel
    {
        public new Action<object?, EventArgs> OnAuthCompleted { get; internal set; }

        public override void Register()
        {
            fbd.CreateUserWithEmailAndPasswordAsync(Email, Password, Name, OnComplete);
        }

        public override void Login()
        {
            fbd.SignInWithEmailAndPasswordAsync(Email, Password, OnComplete);
        }

        public User()
        {
            Name = Preferences.Get(Keys.NameKey, string.Empty);
            Password = Preferences.Get(Keys.PasswordKey, string.Empty);
            Email = Preferences.Get(Keys.EmailKey, string.Empty);
           

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
                ShowAlert(Keys.RegistrationFailed);
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
            return msg.Contains(Keys.ErrorMessageReason) ?
                msg.Contains(Keys.EmailExists) ? Keys.EmailExistsErrorMessage :
                msg.Contains(Keys.InvalidEmailAddress) ? Keys.InvalidEmailErrorMessage :
                msg.Contains(Keys.WeakPassword) ? Keys.WeakPasswordErrorMessage :
                msg.Contains(Keys.RegistrationFailed) ? Keys.RegistrationFailed :
                Keys.UserNotFound : Keys.UserNotFound;

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

        public override bool CanLogin()
        {
            return (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email));
        }
    }
}
