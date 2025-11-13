using MyReversi.ModelsLogic;
using Plugin.CloudFirestore.Attributes;

namespace MyReversi.Models
{
    internal abstract class GameModel
    {
        protected FbData fbd = new();
        [Ignored]
        public string Id { get; set; } = string.Empty;

        public string HostName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public bool IsFull { get; set; }

        public int RowSize { get; set; }
        [Ignored]

        public abstract string OpponentName { get;}
        [Ignored]

        public string MyName { get; set; } = new User().Name;
        [Ignored]

        public string RowSizeName => $"{RowSize} X {RowSize}";

        public bool IsHost { get; set; }

        public string GuestName { get; set; } = string.Empty;

        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
    }
}
