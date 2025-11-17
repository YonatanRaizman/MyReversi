namespace MyReversi.Models
{
    public partial class IndexedButton : Button
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public IndexedButton(int row, int column)
        {
            RowIndex = row;
            ColumnIndex = column;
            FontSize = 12;
            HeightRequest = 45;
            WidthRequest = 45;
        }
    }
}
