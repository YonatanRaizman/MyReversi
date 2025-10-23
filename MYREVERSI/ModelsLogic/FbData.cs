using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Requests;
using MyReversi.Models;
using Plugin.CloudFirestore;
namespace MyReversi.ModelsLogic
{
    class FbData : FbDataModel
    {
        public FbData()
        {
            FirebaseAuthConfig fac = new()
            {
                ApiKey = Keys.FbApiKey,
                Providers = [new EmailProvider()]
            };
            facl = new FirebaseAuthClient(fac);
            fdb = CrossCloudFirestore.Current.Instance;
        }
        public override async void CreateUserWithEmailAndPasswordAsync(string email, string password, string name, Action<System.Threading.Tasks.Task> OnComplete)
        {
            await facl.CreateUserWithEmailAndPasswordAsync(email, password, name).ContinueWith(OnComplete);
        }
        public override async void SignInWithEmailAndPasswordAsync(string email, string password, Action<System.Threading.Tasks.Task> OnComplete)
        {
            await facl.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(OnComplete);
        }

        public override string DisplayName
        {
            get
            {
                string dn = string.Empty;
                if (facl.User != null)
                    dn = facl.User.Info.DisplayName;
                return dn;
            }
        }
        public override string UserId
        {
            get
            {
                return facl.User.Uid;
            }
        }
    }
}

