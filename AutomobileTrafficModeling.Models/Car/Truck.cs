using System.Collections.Generic;

namespace AutomobileTrafficModeling.Models.Car
{
    public class Truck: BasicCar
    {
        protected uint CargoWeight;

        public Truck(uint cargoWeight = 1000, string name = "truck", byte size = 4, 
            byte timeToRideForward = 10, byte timeToTurnLeft = 10, byte timeToTurnRight = 5) : base(name, size, timeToRideForward, timeToTurnLeft, timeToTurnRight)
        {
            Type = "Truck";
            CargoWeight = cargoWeight;
        }

        public override CarStatistic Stats => new CarStatistic(Type, Name, Size, WaitingTime, RidingTime, Direction, new Dictionary<string, long>
        {
            { nameof(CargoWeight), CargoWeight }
        });

        public override BasicCar Copy() => new Truck(CargoWeight, Name, Size, TimeToRideForward, TimeToTurnLeft, TimeToTurnRight);
    }
}
