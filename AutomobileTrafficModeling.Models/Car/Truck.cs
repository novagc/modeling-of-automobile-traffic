using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.Car
{
    public class Truck: BasicCar
    {
        protected uint CargoWeight;

        public Truck(uint cargoWeight = 1000, string name = "truck", byte speed = 40, byte size = 4) : base(name, speed, size)
        {
            Type = "Truck";
            CargoWeight = cargoWeight;

            Stats = new CarStatistic(Type, Name, new Dictionary<string, long>
            {
                { nameof(Speed), Speed },
                { nameof(Size), Size },
                { nameof(CargoWeight), CargoWeight }
            });
        }

        public override BasicCar Copy() => new Truck(CargoWeight, Name, Speed, Size);
    }
}
