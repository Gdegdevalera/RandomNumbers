namespace WebApplication1.Models
{
    public class Response<T> where T : CalculationResult
    {
        public T Set { get; set; }
    }

    public abstract class CalculationResult
    {
        public int Result { get; set; }

        public bool IsValidResult { get; set; }
    }

    public class SumsResult : CalculationResult
    {
        public int[] Numbers { get; set; }
    }

    public class SubtractionsResult : CalculationResult
    {
        public int[] Numbers { get; set; }
    }

    public class MultiplicationsResult : CalculationResult
    {
        public int Number1 { get; set; }

        public int Number2 { get; set; }
    }

    public class DivisionsResult : CalculationResult
    {
        public int Number1 { get; set; }

        public int Number2 { get; set; }
    }
}
