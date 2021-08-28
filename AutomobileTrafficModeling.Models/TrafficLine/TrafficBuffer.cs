using System;
using System.Collections.Generic;
using AutomobileTrafficModeling.Models.Car;

namespace AutomobileTrafficModeling.Models.TrafficLine
{
    public class TrafficBuffer : IDisposable
    {
        public readonly List<BasicCar> Left;
        public readonly List<BasicCar> Forward;
        public readonly List<BasicCar> Right;

        public TrafficBuffer()
        {
            Left = new List<BasicCar>();
            Forward = new List<BasicCar>();
            Right = new List<BasicCar>();
        }

        public void Dispose()
        {
            Left.Clear();
            Forward.Clear();
            Right.Clear();
        }
    }
}
