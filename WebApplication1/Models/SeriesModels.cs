using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class NextNumberResult
    {
        public string Series { get; set; }

        public string Explanation { get; set; }
    }

    public class DuplicatesResult
    {
        public int[][] Set { get; set; }

        public int[] ColumnsWithDuplicate { get; set; }

        public int[] RowsWithDuplicate { get; set; }
    }

    public class MatchesResult
    {
        public string[] Pairs { get; set; }

        public string Result { get; set; }
    }

    public class SearchResult
    {
        public int[] NumbersToFind { get; set; }

        public int[] Array { get; set; }

        public Dictionary<string, int[]> Result { get; set; }
    }

    public class ExactSearchResult
    {
        public int[][] ExactPairsToFind { get; set; }

        public int[] Array { get; set; }

        public Dictionary<string, int[]> Result { get; set; }
    }
}
