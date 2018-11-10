namespace WebApplication1.Models.Source
{
    public class SumsSource
    {
        public int[] Numbers { get; set; }
        public bool IsValidResult { get; set; }
    }

    public class SubtractionsSource
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public bool IsValidResult { get; set; }
    }

    public class MultiplicationsSource
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public bool IsValidResult { get; set; }
    }

    public class DivisionsSource
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
}
