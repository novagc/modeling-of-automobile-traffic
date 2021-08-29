using System.Collections.Generic;

namespace AutomobileTrafficModeling.Models.Car
{
    public class PassengerCar: BasicCar
    {
        protected byte MaxPassengerCount;

        public PassengerCar(byte maxPassengerCount = 4, string name = "passenger", byte size = 1, 
            byte timeToRideForward = 10, byte timeToTurnLeft = 10, byte timeToTurnRight = 5) : base(name, size, timeToRideForward, timeToTurnLeft, timeToTurnRight)
        {
            Type = "Passenger";
            MaxPassengerCount = maxPassengerCount;
        }

        public override CarStatistic Stats => new CarStatistic(Type, Name, Size, WaitingTime, RidingTime, Direction, new Dictionary<string, long>
        {
            { nameof(MaxPassengerCount), MaxPassengerCount }
        });

        public override BasicCar Copy() => new PassengerCar(MaxPassengerCount, Name, Size, TimeToRideForward, TimeToTurnLeft, TimeToTurnRight);
    }
}
