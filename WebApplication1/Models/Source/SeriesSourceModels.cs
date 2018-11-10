namespace WebApplication1.Models.Source
{
    public class DuplicatesSource
    {
        public int[][] Set { get; set; }
    }

    public class SearchSource
    {
        public int[] Array { get; set; }
        public int[] Numbers { get; set; }
    }

    public class ExactSearchSource
    {
        public int[] Array { get; set; }
        public int[][] Pairs { get; set; }
    }
}
