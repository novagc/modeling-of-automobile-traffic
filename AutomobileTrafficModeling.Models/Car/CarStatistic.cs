using System.Collections.Generic;

namespace AutomobileTrafficModeling.Models.Car
{
    public struct CarStatistic
    {
        public readonly string Type;
        public readonly string Name;

        public readonly byte Size;

        public readonly int WaitingTime;
        public readonly int RidingTime;

        public readonly RidingDirection Direction;

        public readonly Dictionary<string, long> Stats;

        public CarStatistic(string type, string name, byte size, int waitingTime, int ridingTime, RidingDirection direction, Dictionary<string, long> stats)
        {
            Type = type;
            Name = name;

            Size = size;

            WaitingTime = waitingTime;
            RidingTime = ridingTime;

            Direction = direction; 

            Stats = stats;
        }
    }
}
