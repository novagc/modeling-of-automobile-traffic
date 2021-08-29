using System;
using AutomobileTrafficModeling.Core.Generator.Data;
using AutomobileTrafficModeling.Models.Car;

namespace AutomobileTrafficModeling.Core.Generator
{
    public class AllCarGenerator: ICarGenerator
    {
        public TimesToNextCars TimesToNextCar { get; }
        public double TruckPart { get; }

        private readonly PassengerCar _passengerExample;
        private readonly Truck _truckExample;

        private readonly Random _rnd;

        private ulong _turn;
        private ulong _nextPassengerCarIndex;
        private ulong _nextTruckIndex;

        public AllCarGenerator(TimesToNextCars timesToNextCar, double truckPart, PassengerCar passengerExample, Truck truck)
        {
            TimesToNextCar = timesToNextCar;
            TruckPart = truckPart;

            _passengerExample = passengerExample ?? new PassengerCar();
            _truckExample = truck ?? new Truck();

            _rnd = new Random();
        }

        public GeneratedCarList NextTurn()
        {
            var res = new BasicCar[] { null, null, null, null };

            if (TimesToNextCar.Up > 0 && _turn % (ulong)TimesToNextCar.Up == 0)
            {
                if (_rnd.NextDouble() <= TruckPart)
                {
                    res[0] = _truckExample.Copy();
                    res[0].Name = $"truck #{_nextTruckIndex++}";
                }
                else
                {
                    res[0] = _passengerExample.Copy();
                    res[0].Name = $"passenger #{_nextPassengerCarIndex++}";
                }
            }
            if (TimesToNextCar.Down > 0 && _turn % (ulong)TimesToNextCar.Down == 0)
            {
                if (_rnd.NextDouble() <= TruckPart)
                {
                    res[1] = _truckExample.Copy();
                    res[1].Name = $"truck #{_nextTruckIndex++}";
                }
                else
                {
                    res[1] = _passengerExample.Copy();
                    res[1].Name = $"passenger #{_nextPassengerCarIndex++}";
                }
            }
            if (TimesToNextCar.Left > 0 && _turn % (ulong)TimesToNextCar.Left == 0)
            {
                if (_rnd.NextDouble() <= TruckPart)
                {
                    res[2] = _truckExample.Copy();
                    res[2].Name = $"truck #{_nextTruckIndex++}";
                }
                else
                {
                    res[2] = _passengerExample.Copy();
                    res[2].Name = $"passenger #{_nextPassengerCarIndex++}";
                }
            }
            if (TimesToNextCar.Right > 0 && _turn % (ulong)TimesToNextCar.Right == 0)
            {
                if (_rnd.NextDouble() <= TruckPart)
                {
                    res[3] = _truckExample.Copy();
                    res[3].Name = $"truck #{_nextTruckIndex++}";
                }
                else
                {
                    res[3] = _passengerExample.Copy();
                    res[3].Name = $"passenger #{_nextPassengerCarIndex++}";
                }
            }

            _turn++;

            return new GeneratedCarList(res[0], res[1], res[2], res[3]);
        }
    }
}
