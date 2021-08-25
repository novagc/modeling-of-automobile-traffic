using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            byte horizontalGreenDuration, byte horizontalYellowDuration, byte horizontalLinesCount, byte horizontalLineWidth,
            byte verticalGreenDuration, byte verticalYellowDuration, byte verticalLinesCount, byte verticalLineWidth
            )
        {
            HorizontalAxis = new Axis(new TrafficLightDuration(horizontalGreenDuration, horizontalYellowDuration), horizontalLinesCount, horizontalLineWidth);
            VerticalAxis = new Axis(new TrafficLightDuration(verticalGreenDuration, verticalYellowDuration), verticalLinesCount, verticalLineWidth);
        }
    }
}
