using MyReversi.Models;

namespace MyReversi.ModelsLogic
{
    public class MainPageML : MainPageModel
    {
        public override void ShowInstructionsPrompt(object obj)
        {
            Application.Current!.MainPage!.DisplayAlert(Strings.InsructionsTxtTitle, Strings.InsructionsTxt, Strings.Ok);
        }
    }
}
