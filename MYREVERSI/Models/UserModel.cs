using MyReversi.ModelsLogic;

namespace MyReversi.Models
{
    internal abstract class UserModel
    {
        protected FbData fbd = new();
        public EventHandler? OnAuthCompleted;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ForgotYourPassword { get; set; } = string.Empty;
        public abstract void Register();
        public abstract void Login();
        public abstract bool CanLogin();
        public abstract string GetFirebaseErrorMessage(string msg);
        public abstract bool CanRegister();
        public bool IsRegistered => (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password));
    }
}
