using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.Car
{
    public class PassengerCar: BasicCar
    {
        protected byte MaxPassengerCount;

        public PassengerCar(byte maxPassengerCount, string name = "passenger", byte speed = 50, byte size = 1) : base(name, speed, size)
        {
            MaxPassengerCount = maxPassengerCount;
        }

        public override Dictionary<string, string> Stats
        {
            get
            {
                var res = base.Stats;
                res.Add(nameof(MaxPassengerCount), $"{MaxPassengerCount} {(MaxPassengerCount == 1 ? "person" : "people")}");
                return res;
            }
        }

        public override BasicCar Copy() => new PassengerCar(MaxPassengerCount, Name, Speed, Size);
    }
}
