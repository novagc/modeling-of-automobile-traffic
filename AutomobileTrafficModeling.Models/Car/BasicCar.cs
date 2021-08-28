namespace AutomobileTrafficModeling.Models.Car
{
    public abstract class BasicCar
    {
        public string Type { get; protected set; }
        public string Name { get; set; }
     
        public int WaitingTime { get; protected set; }
        public int DrivingTime { get; protected set; }

        public byte TimeToRide { get; set; }
        public RidingDirection Direction { get; set; }

        public readonly byte TimeToRideForward;
        public readonly byte TimeToTurnLeft;
        public readonly byte TimeToTurnRight;

        public readonly byte Size;

        public abstract CarStatistic Stats { get; }

        protected BasicCar(string name, byte size, byte timeToRideForward, byte timeToTurnLeft, byte timeToTurnRight)
        {
            Name = name;
            Size = size;

            TimeToRideForward = timeToRideForward;
            TimeToTurnLeft = timeToTurnLeft;
            TimeToTurnRight = timeToTurnRight;

            TimeToRide = timeToRideForward;
        }

        public abstract BasicCar Copy();

        public virtual void Wait(int time = 1)
        {
            WaitingTime += time;
        }

        public virtual void Ride(int time = 1)
        {
            DrivingTime += time;
        }

        public virtual bool EndRiding()
        {
            return DrivingTime >= TimeToRide;
        }
    }
}
