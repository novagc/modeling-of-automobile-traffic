namespace AutomobileTrafficModeling.Models.TrafficLight
{
    public struct SimpleTrafficLight
    {
        public readonly Axis HorizontalAxis;
        public readonly Axis VerticalAxis;

        public SimpleTrafficLight(Axis? horizontalAxis = null, Axis? verticalAxis = null)
        {
            HorizontalAxis = horizontalAxis ?? new Axis();
            VerticalAxis = verticalAxis ?? new Axis();
        }

        public SimpleTrafficLight(
            byte horizontalGreenDuration, byte horizontalYellowDuration, byte horizontalLineWidth,
            byte verticalGreenDuration, byte verticalYellowDuration, byte verticalLineWidth
            )
        {
            HorizontalAxis = new Axis(horizontalLineWidth, new TrafficLightDuration(horizontalGreenDuration, horizontalYellowDuration));
            VerticalAxis = new Axis(verticalLineWidth, new TrafficLightDuration(verticalGreenDuration, verticalYellowDuration));
        }
    }
}
