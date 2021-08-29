using System.Linq;
using System.Collections.Generic;
using AutomobileTrafficModeling.Core.Generator;
using AutomobileTrafficModeling.Core.Generator.Data;
using AutomobileTrafficModeling.Models.Car;
using Xunit;

namespace AutomobileTrafficModeling.Tests
{
    public class GeneratorsTests
    {
        public PassengerCar PassengerCarExample = new PassengerCar();
        public Truck TruckExample = new Truck();
        public double TruckPart = 0.5;
        public TimesToNextCars Times = new TimesToNextCars(3, 3, 3, 3);
        public TimesToNextCars ShortTimes = new TimesToNextCars(1, 1, 1, 1);

        [Fact]
        public void Test1()
        {
            var allGenerator = new AllCarGenerator(Times, TruckPart, PassengerCarExample, TruckExample);
            var res = allGenerator.NextTurn();
            
            Assert.NotNull(res.Up);
            Assert.NotNull(res.Down);
            Assert.NotNull(res.Left);
            Assert.NotNull(res.Right);

            for (int i = 0; i < 3; i++)
            {
                res = allGenerator.NextTurn();

                Assert.Null(res.Up);
                Assert.Null(res.Down);
                Assert.Null(res.Left);
                Assert.Null(res.Right);
            }

            res = allGenerator.NextTurn();

            Assert.NotNull(res.Up);
            Assert.NotNull(res.Down);
            Assert.NotNull(res.Left);
            Assert.NotNull(res.Right);
        }

        [Fact]
        public void Test2()
        {
            var allGenerator = new AllCarGenerator(ShortTimes, TruckPart, PassengerCarExample, TruckExample);
            var temp = new List<BasicCar>();

            for (int i = 0; i < 100; i++)
            {
                var res = allGenerator.NextTurn();
                
                temp.Add(res.Up);
                temp.Add(res.Down);
                temp.Add(res.Left);
                temp.Add(res.Right);

                allGenerator.NextTurn();
            }

            Assert.Equal(400, temp.Count);
            Assert.InRange(temp.Count(x => x.Type == temp[0].Type) / (double)(temp.Count), 0.45, 0.55);
        }

        [Fact]
        public void Test3()
        {
            var passengerCarGenerator = new OnlyPassengerCarGenerator(Times, PassengerCarExample);
            var res = passengerCarGenerator.NextTurn();

            Assert.NotNull(res.Up);
            Assert.NotNull(res.Down);
            Assert.NotNull(res.Left);
            Assert.NotNull(res.Right);

            for (int i = 0; i < 3; i++)
            {
                res = passengerCarGenerator.NextTurn();

                Assert.Null(res.Up);
                Assert.Null(res.Down);
                Assert.Null(res.Left);
                Assert.Null(res.Right);
            }

            res = passengerCarGenerator.NextTurn();

            Assert.NotNull(res.Up);
            Assert.NotNull(res.Down);
            Assert.NotNull(res.Left);
            Assert.NotNull(res.Right);
        }

        [Fact]
        public void Test4()
        {
            var passengerCarGenerator = new OnlyPassengerCarGenerator(Times, PassengerCarExample);
            var temp = new List<BasicCar>();

            for (int i = 0; i < 100; i++)
            {
                var res = passengerCarGenerator.NextTurn();
                
                temp.Add(res.Up);
                temp.Add(res.Down);
                temp.Add(res.Left);
                temp.Add(res.Right);
            }

            Assert.Equal(100, temp.Count(x => x != null));
            Assert.Single(temp.Where(x => x != null).Select(x => x.Type).Distinct());
            Assert.Equal("Passenger", temp.Select(x => x.Type).Distinct().First());
        }
    }
}
