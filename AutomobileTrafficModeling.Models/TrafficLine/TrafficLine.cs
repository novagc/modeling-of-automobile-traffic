using System;

namespace AutomobileTrafficModeling.Models.TrafficLine
{
    public class TrafficLine : IDisposable
    {
        public readonly TrafficBuffer Waiting;
        public readonly TrafficBuffer Riding;

        public TrafficLine()
        {
            Waiting = new TrafficBuffer();
            Riding = new TrafficBuffer();
        }

        public void Dispose()
        {
            Waiting.Dispose();
            Riding.Dispose();
        }
    }
}
