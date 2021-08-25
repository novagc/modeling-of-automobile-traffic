using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.TrafficLight
{
    public struct Axis
    {
        public readonly TrafficLightDuration Duration;

        public readonly byte LinesCount;
        public readonly byte LineWidth;

        public Axis(TrafficLightDuration? duration = null, byte linesCount = 1, byte lineWidth = 3)
        {
            Duration = duration ?? new TrafficLightDuration();
            LinesCount = linesCount;
            LineWidth = lineWidth;
        }
    }
}
