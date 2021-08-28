namespace AutomobileTrafficModeling.Models.TrafficLight
{
    public struct TrafficLightDuration
    {
        public readonly byte Green;
        public readonly byte Yellow;

        public TrafficLightDuration(byte green = 50, byte yellow = 4)
        {
            Green = green;
            Yellow = yellow;
        }
    }
}
