using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.Car
{
    public struct CarStatistic
    {
        public readonly string Type;
        public readonly string Name;
        public readonly Dictionary<string, long> Stats;

        public CarStatistic(string type, string name, Dictionary<string, long> stats)
        {
            Type = type;
            Name = name;
            Stats = stats;
        }
    }
}
