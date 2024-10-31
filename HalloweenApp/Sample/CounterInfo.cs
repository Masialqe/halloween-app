namespace HalloweenApp.Sample
{
    public class CounterInfo
    {
        public int CounterValue { get; set; } = 0;
        public int CounterValueNegative => CounterValue * -1;

    }
}
