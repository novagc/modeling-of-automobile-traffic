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

        public Truck(uint cargoWeight, string name = "truck", byte speed = 40, byte size = 4) : base(name, speed, size)
        {
            CargoWeight = cargoWeight;
        }

        public override Dictionary<string, string> Stats
        {
            get
            {
                var res = base.Stats;
                res.Add(nameof(CargoWeight), $"{CargoWeight} kg");
                return res;
            }
        }

        public override BasicCar Copy() => new Truck(CargoWeight, Name, Speed, Size);
    }
}
