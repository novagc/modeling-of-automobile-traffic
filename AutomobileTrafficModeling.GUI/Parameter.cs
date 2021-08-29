using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutomobileTrafficModeling.GUI
{
    public class Parameter
    {
        public Grid Grid;
        public Label Label;
        public Slider Slider;
        public string Text;

        public Parameter(Grid grid, Label label, Slider slider, string text, double defaultValue, bool isInt = true)
        {
            Grid = grid;
            Label = label;
            Slider = slider;
            Text = text;

            Label.Content = isInt ? $"{Text}{Environment.NewLine}{Slider.Value}" : $"{Text}{Environment.NewLine}{Slider.Value : 0.00}";

            Slider.ValueChanged += (_, __) =>
            {
                Label.Content = isInt ? $"{Text}{Environment.NewLine}{Slider.Value}" : $"{Text}{Environment.NewLine}{Slider.Value: 0.00}";
            };

            Slider.Value = defaultValue;
        }

        public void Enable()
        {
            Grid.IsEnabled = true;
        }

        public void Disable()
        {
            Grid.IsEnabled = false;
        }

        public byte GetByte() => Convert.ToByte(Slider.Value);
        public int GetInt() => Convert.ToInt32(Slider.Value);
        public double GetDouble() => Slider.Value;
    }
}
