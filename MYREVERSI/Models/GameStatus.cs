namespace MyReversi.Models
{
    public class GameStatus
    {
        private readonly string[] msgs = [Strings.PlayMessage, Strings.WaitMessage];
        public enum Statuses { Wait, Play }
        public Statuses CurrentStatus { get; set; } = Statuses.Wait;
        public string StatusMessage => msgs[(int)CurrentStatus];

        public void UpdateStatus()
        {
            CurrentStatus = CurrentStatus == Statuses.Play ? Statuses.Wait : Statuses.Play;
        }
    }
}
