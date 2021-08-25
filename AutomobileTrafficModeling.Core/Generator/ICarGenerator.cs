using AutomobileTrafficModeling.Core.Generator.Data;

namespace AutomobileTrafficModeling.Core.Generator
{
    interface ICarGenerator
    {
        public TimesToNextCars TimesToNextCar { get; }
        public GeneratedCarList NextTurn();
    }
}
