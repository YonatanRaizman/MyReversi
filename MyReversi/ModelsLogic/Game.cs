using MyReversi.Models;

namespace MyReversi.ModelsLogic
{
    partial class Game : GameModel
    {
        public override string OpponentName => IsHost? GuestName : HostName;

        internal Game(GameSize selectedGameSize)
        {
            HostName = new User().Name;
            RowSize = selectedGameSize.Size;
            Created = DateTime.Now;
        }

        public override void SetDocument(Action<System.Threading.Tasks.Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }


    }
}
