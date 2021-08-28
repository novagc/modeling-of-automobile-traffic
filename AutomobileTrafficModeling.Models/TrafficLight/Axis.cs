namespace AutomobileTrafficModeling.Models.TrafficLight
{
    public struct Axis
    {
        public readonly TrafficLightDuration Duration;

        public readonly byte Width;

        public Axis(byte width = 10, TrafficLightDuration? duration = null)
        {
            Width = width;
            Duration = duration ?? new TrafficLightDuration();
        }
    }
}
