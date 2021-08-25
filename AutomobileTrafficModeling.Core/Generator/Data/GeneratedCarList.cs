using AutomobileTrafficModeling.Models.Car;

namespace AutomobileTrafficModeling.Core.Generator.Data
{
    public struct GeneratedCarList
    {
        public readonly BasicCar Up;
        public readonly BasicCar Down;
        public readonly BasicCar Left;
        public readonly BasicCar Right;

        public GeneratedCarList(BasicCar up, BasicCar down, BasicCar left, BasicCar right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
    }
}
