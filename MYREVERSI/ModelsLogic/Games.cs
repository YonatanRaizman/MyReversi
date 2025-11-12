using MyReversi.Models;
using Plugin.CloudFirestore;

namespace MyReversi.ModelsLogic
{
    public class Games : GamesModel
    {
        internal void AddGame()
        {
            IsBusy = true;
            currentGame = new(SelectedGameSize);
            currentGame.IsHostUser = true;
            currentGame.SetDocument(OnComplete);
        }
        private void OnComplete(Task task)
        {
            IsBusy = false;
            OnGameAdded?.Invoke(this, currentGame!);
        }
        public void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.GamesCollection, OnChange!);
        }
        public void RemoveSnapshotListener()
        {
            ilr?.Remove();
        }
        private void OnChange(IQuerySnapshot snapshot, Exception error)
        {
            fbd.GetDocumentsWhereEqualTo(Keys.GamesCollection, nameof(GameModel.IsFull), false, OnComplete);
        }

        private void OnComplete(IQuerySnapshot qs)
        {
            GamesList!.Clear();
            foreach (IDocumentSnapshot ds in qs.Documents)
            {
                Game? game = ds.ToObject<Game>();
                if (game != null)
                {
                    game.Id = ds.Id;
                    GamesList.Add(game);
                }
            }
            OnGamesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
