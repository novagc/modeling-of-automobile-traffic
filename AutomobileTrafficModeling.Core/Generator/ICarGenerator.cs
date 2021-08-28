using AutomobileTrafficModeling.Core.Generator.Data;

namespace AutomobileTrafficModeling.Core.Generator
{
    public interface ICarGenerator
    {
        public TimesToNextCars TimesToNextCar { get; }
        public GeneratedCarList NextTurn();
    }
}
