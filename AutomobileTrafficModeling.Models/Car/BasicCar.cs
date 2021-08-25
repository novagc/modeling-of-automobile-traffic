using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.Car
{
    public abstract class BasicCar
    {
        public string Type { get; protected set; }
        public string Name { get; set; }

        public CarStatistic Stats { get; protected set; }
     
        public int WaitingTime { get; protected set; }
        public int DrivingTime { get; protected set; }

        protected byte Speed;
        protected byte Size;

        protected BasicCar(string name, byte speed, byte size)
        {
            Name = name;
            Speed = speed;
            Size = size;
        }

        public abstract BasicCar Copy();

        public virtual void Wait()
        {
            WaitingTime++;
        }

        public virtual void Drive()
        {
            DrivingTime++;
        }
    }
}
