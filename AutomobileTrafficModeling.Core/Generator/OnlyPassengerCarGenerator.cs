using AutomobileTrafficModeling.Core.Generator.Data;
using AutomobileTrafficModeling.Models.Car;

namespace AutomobileTrafficModeling.Core.Generator
{
    public class OnlyPassengerCarGenerator : ICarGenerator
    {
        public TimesToNextCars TimesToNextCar { get; }

        private readonly PassengerCar _passengerExample;

        private ulong _turn;
        private ulong _nextCarIndex;

        public OnlyPassengerCarGenerator(TimesToNextCars timesToNextCar, PassengerCar passengerExample)
        {
            TimesToNextCar = timesToNextCar;
            _passengerExample = passengerExample;
        }

        public GeneratedCarList NextTurn()
        {
            var res = new BasicCar[] { null, null, null, null };

            if (TimesToNextCar.Up > 0 && _turn % (ulong)TimesToNextCar.Up == 0)
            {
                res[0] = _passengerExample.Copy();
                res[0].Name = $"passenger #{_nextCarIndex++}";
            }
            if (TimesToNextCar.Down > 0 && _turn % (ulong)TimesToNextCar.Down == 0)
            {
                res[1] = _passengerExample.Copy();
                res[1].Name = $"passenger #{_nextCarIndex++}";
            }
            if (TimesToNextCar.Left > 0 && _turn % (ulong)TimesToNextCar.Left == 0)
            {
                res[2] = _passengerExample.Copy();
                res[2].Name = $"passenger #{_nextCarIndex++}";
            }
            if (TimesToNextCar.Right > 0 && _turn % (ulong)TimesToNextCar.Right == 0)
            {
                res[3] = _passengerExample.Copy();
                res[3].Name = $"passenger #{_nextCarIndex++}";
            }

            _turn++;

            return new GeneratedCarList(res[0], res[1], res[2], res[3]);
        }
    }
}
