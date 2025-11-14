namespace MyReversi.Models
{
    public partial class DiscsModel : Button
    {
        public enum DiscColor {black, white}

        public DiscColor CurrentDiscColor {  get; set; }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public bool IsGreen { get; set; }

        public DiscsModel(int row, int column, DiscColor disc_color, bool isGreen)
        {
            RowIndex = row;
            ColumnIndex = column;
            CurrentDiscColor = disc_color;
            IsGreen = isGreen;
            HeightRequest = 45;
            WidthRequest = 45;
        }
    }
}
