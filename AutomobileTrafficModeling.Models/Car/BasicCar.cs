using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileTrafficModeling.Models.Car
{
    public abstract class BasicCar
    {
        public string Name { get; protected set; }
        
        protected byte Speed;
        protected byte Size;

        protected BasicCar(string name, byte speed, byte size)
        {
            Name = name;
            Speed = speed;
            Size = size;
        }

        public virtual Dictionary<string, string> Stats => new Dictionary<string, string>
        {
            { nameof(Name), Name },
            { nameof(Speed), $"{Speed} km/h"},
            { nameof(Size), $"{Size} m" }
        };
    }
}
