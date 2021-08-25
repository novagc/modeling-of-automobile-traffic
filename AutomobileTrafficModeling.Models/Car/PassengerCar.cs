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

        public PassengerCar(byte maxPassengerCount = 4, string name = "passenger", byte speed = 50, byte size = 1) : base(name, speed, size)
        {
            Type = "Passenger";
            MaxPassengerCount = maxPassengerCount;

            Stats = new CarStatistic(Type, Name, new Dictionary<string, long>
            {
                { nameof(Speed), Speed },
                { nameof(Size), Size },
                { nameof(MaxPassengerCount), MaxPassengerCount }
            });
        }

        public override BasicCar Copy() => new PassengerCar(MaxPassengerCount, Name, Speed, Size);
    }
}
