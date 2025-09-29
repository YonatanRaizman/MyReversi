using MYREVERSI.Models;

namespace MYREVERSI.ModelsLogic
{
    internal class User : UserModel
    {
        public override void Register()
        {
            Preferences.Set(Keys.NameKey, Name);
        }
        public User()
        {
            Name = Preferences.Get(Keys.NameKey, string.Empty);
        }
    }
}
