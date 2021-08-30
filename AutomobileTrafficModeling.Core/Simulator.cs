using System;
using System.Collections.Generic;
using System.Linq;

using AutomobileTrafficModeling.Core.Generator;
using AutomobileTrafficModeling.Models.Car;
using AutomobileTrafficModeling.Models.TrafficLight;
using AutomobileTrafficModeling.Models.TrafficLine;

namespace AutomobileTrafficModeling.Core
{
    public class Simulator
    {
        public int TotalTime;
        
        public int TotalCarsCount;
        public int TotalEndRidingCarsCount;

        public double AverageWaitingRidingForwardTime;
        public double AverageWaitingTurningLeftTime;
        public double AverageWaitingTurningRightTime;

        public double AverageRidingForwardTime;
        public double AverageTurningLeftTime;
        public double AverageTurningRightTime;

        public List<CarStatistic> AllCarsStats;

        public Dictionary<string, long> TotalEndRidingCarsStats;
        public Dictionary<string, int> TotalEndRidingCarsCountByType;

        private readonly ICarGenerator _generator;
        
        private readonly SimpleTrafficLight _trafficLight;
        
        private Axis _activeTrafficAxis;
        private int _activeTrafficAxisIndex;

        private int _timeLeft;

        private readonly Random _rnd;

        private readonly TrafficLine _up;
        private readonly TrafficLine _down;
        private readonly TrafficLine _left;
        private readonly TrafficLine _right;

        public Simulator(ICarGenerator generator, SimpleTrafficLight trafficLight)
        {
            _generator = generator;
            _trafficLight = trafficLight;

            _rnd = new Random();

            _up = new TrafficLine();
            _down = new TrafficLine();
            _left = new TrafficLine();
            _right = new TrafficLine();

            AllCarsStats = new List<CarStatistic>();
            TotalEndRidingCarsStats = new Dictionary<string, long>();
            TotalEndRidingCarsCountByType = new Dictionary<string, int>();
        }

        public void Start()
        {
            Generate();
        }

        public void NextTurn()
        {
            if (_timeLeft == 0)
            {
                _activeTrafficAxisIndex = (_activeTrafficAxisIndex + 1) % 2;
                _activeTrafficAxis = _activeTrafficAxisIndex == 0 ? _trafficLight.VerticalAxis : _trafficLight.HorizontalAxis;
                _timeLeft = _activeTrafficAxis.Duration.Green + _activeTrafficAxis.Duration.Yellow;
            }

            Generate();

            if (_activeTrafficAxisIndex == 0)
            {
                SimulateRiding(_up, _down);
            }
            else
            {
                SimulateRiding(_left, _right);
            }

            _timeLeft--;
            TotalTime++;
        }

        public void End()
        {
            _up.Dispose();
            _down.Dispose();
            _left.Dispose();
            _right.Dispose();

            TotalEndRidingCarsCount = AllCarsStats.Count;

            foreach (var typeGroup in AllCarsStats.GroupBy(x => x.Type))
            {
                TotalEndRidingCarsCountByType.Add(typeGroup.Key, typeGroup.Count());
            }

            TotalEndRidingCarsStats.Add("Size", 0);

            var tempWaiting = new Dictionary<RidingDirection, long>
            {
                { RidingDirection.Forward, 0 },
                { RidingDirection.Left, 0 },
                { RidingDirection.Right, 0 },
            };

            var tempWaitingCount = new Dictionary<RidingDirection, long>
            {
                { RidingDirection.Forward, 0 },
                { RidingDirection.Left, 0 },
                { RidingDirection.Right, 0 },
            };

            var tempRiding = new Dictionary<RidingDirection, long>
            {
                { RidingDirection.Forward, 0 },
                { RidingDirection.Left, 0 },
                { RidingDirection.Right, 0 },
            };

            var tempRidingCount = new Dictionary<RidingDirection, long>
            {
                { RidingDirection.Forward, 0 },
                { RidingDirection.Left, 0 },
                { RidingDirection.Right, 0 },
            };

            foreach (var x in AllCarsStats)
            {
                tempWaiting[x.Direction] += x.WaitingTime;
                tempWaitingCount[x.Direction]++;

                tempRiding[x.Direction] += x.RidingTime;
                tempRidingCount[x.Direction]++;

                TotalEndRidingCarsStats["Size"] += x.Size;
                foreach (var y in x.Stats)
                {
                    if (!TotalEndRidingCarsStats.ContainsKey(y.Key))
                    {
                        TotalEndRidingCarsStats.Add(y.Key, y.Value);
                    }
                    else
                    {
                        TotalEndRidingCarsStats[y.Key] += y.Value;
                    }
                }
            }

            AverageWaitingRidingForwardTime = tempWaiting[RidingDirection.Forward] / (double)(tempWaitingCount[RidingDirection.Forward]);
            AverageWaitingTurningLeftTime = tempWaiting[RidingDirection.Left] / (double)(tempWaitingCount[RidingDirection.Left]);
            AverageWaitingTurningRightTime = tempWaiting[RidingDirection.Right] / (double)(tempWaitingCount[RidingDirection.Right]);

            AverageRidingForwardTime = tempRiding[RidingDirection.Forward] / (double)(tempRidingCount[RidingDirection.Forward]);
            AverageTurningLeftTime = tempRiding[RidingDirection.Left] / (double)(tempRidingCount[RidingDirection.Left]);
            AverageTurningRightTime = tempRiding[RidingDirection.Right] / (double)(tempRidingCount[RidingDirection.Right]);
        }

        private void SimulateRiding(TrafficLine firstLine, TrafficLine secondLine)
        {
            TryToEndRiding(firstLine.Riding.Right);
            TryToEndRiding(firstLine.Riding.Forward);
            TryToEndRiding(firstLine.Riding.Left);

            TryToEndRiding(secondLine.Riding.Right);
            TryToEndRiding(secondLine.Riding.Forward);
            TryToEndRiding(secondLine.Riding.Left);

            TryToTurnLeft(firstLine.Waiting.Left, firstLine.Riding.Left, secondLine.Riding.Forward, _activeTrafficAxis.Width, _timeLeft);
            TryToTurnLeft(secondLine.Waiting.Left, secondLine.Riding.Left, firstLine.Riding.Forward, _activeTrafficAxis.Width, _timeLeft);

            TryToRideForward(firstLine.Waiting.Forward, firstLine.Riding.Forward, secondLine.Riding.Left, _activeTrafficAxis.Width, _timeLeft);
            TryToRideForward(secondLine.Waiting.Forward, secondLine.Riding.Forward, firstLine.Riding.Left, _activeTrafficAxis.Width, _timeLeft);

            TryToTurnRight(firstLine.Waiting.Right, firstLine.Riding.Right, _activeTrafficAxis.Width, _timeLeft);
            TryToTurnRight(secondLine.Waiting.Right, secondLine.Riding.Right, _activeTrafficAxis.Width, _timeLeft);

            Wait(firstLine.Waiting.Right);
            Wait(firstLine.Waiting.Forward);
            Wait(firstLine.Waiting.Left);

            Wait(secondLine.Waiting.Right);
            Wait(secondLine.Waiting.Forward);
            Wait(secondLine.Waiting.Left);
        }

        private void TryToEndRiding(List<BasicCar> cars)
        {
            foreach (var car in cars.ToArray()
                .Where(x =>
                {
                    x.Ride();
                    return x.EndRiding();
                })
            )
            {
                AllCarsStats.Add(car.Stats);
                cars.Remove(car);
            }
        }

        private void TryToTurnLeft(List<BasicCar> waitingCars, List<BasicCar> ridingCars, List<BasicCar> oppositeCarsRidingForward, int width, int timeLeft)
        {
            if (oppositeCarsRidingForward.Sum(x => x.Size) <= width / 2 && timeLeft > _activeTrafficAxis.Duration.Yellow)
            {
                width -= ridingCars.Sum(x => x.Size);
                while (width > 0 && waitingCars.Any() && waitingCars[0].TimeToRide < timeLeft)
                {
                    ridingCars.Add(waitingCars[0]);
                    width -= waitingCars[0].Size;
                    waitingCars.RemoveAt(0);
                }
            }
        }

        private void TryToRideForward(List<BasicCar> waitingCars, List<BasicCar> ridingCars,
            List<BasicCar> oppositeCarsTurningLeft, int width, int timeLeft)
        {
            if (!oppositeCarsTurningLeft.Any() && timeLeft > _activeTrafficAxis.Duration.Yellow)
            {
                width -= ridingCars.Sum(x => x.Size);
                while (width > 0 && waitingCars.Any() && waitingCars[0].TimeToRide < timeLeft)
                {
                    ridingCars.Add(waitingCars[0]);
                    width -= waitingCars[0].Size;
                    waitingCars.RemoveAt(0);
                }
            }
        }

        private void TryToTurnRight(List<BasicCar> waitingCars, List<BasicCar> ridingCars, int width, int timeLeft)
        {
            if (timeLeft > _activeTrafficAxis.Duration.Yellow)
            {
                width -= ridingCars.Sum(x => x.Size);
                while (width > 0 && waitingCars.Any() && waitingCars[0].TimeToRide < timeLeft)
                {
                    ridingCars.Add(waitingCars[0]);
                    width -= waitingCars[0].Size;
                    waitingCars.RemoveAt(0);
                }
            }
        }

        private void Wait(List<BasicCar> waitingCars)
        {
            foreach (var car in waitingCars)
            {
                car.Wait();
            }
        }

        private void Distribute(BasicCar car, TrafficLine trafficLine)
        {
            if (car != null)
            {
                switch (_rnd.Next() % 3)
                {
                    case 0:
                        car.Direction = RidingDirection.Forward;
                        car.TimeToRide = car.TimeToRideForward;
                        trafficLine.Waiting.Forward.Add(car);
                        break;
                    case 1:
                        car.Direction = RidingDirection.Left;
                        car.TimeToRide = car.TimeToTurnLeft;
                        trafficLine.Waiting.Left.Add(car);
                        break;
                    case 2:
                        car.Direction = RidingDirection.Right;
                        car.TimeToRide = car.TimeToTurnRight;
                        trafficLine.Waiting.Right.Add(car);
                        break;
                }

                TotalCarsCount++;
            }
        }

        private void Generate()
        {
            var res = _generator.NextTurn();

            Distribute(res.Up, _up);
            Distribute(res.Down, _down);
            Distribute(res.Left, _left);
            Distribute(res.Right, _right);
        }
    }
}
