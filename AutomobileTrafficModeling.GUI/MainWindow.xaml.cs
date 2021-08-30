using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutomobileTrafficModeling.Core;
using AutomobileTrafficModeling.Core.Generator;
using AutomobileTrafficModeling.Core.Generator.Data;
using AutomobileTrafficModeling.Models.Car;
using AutomobileTrafficModeling.Models.TrafficLight;

namespace AutomobileTrafficModeling.GUI
{
	public partial class MainWindow : Window
    {
	    public Parameter[] Parameters;

        public Simulator Simulator;

        public MainWindow()
        {
            InitializeComponent();
            Parameters = new []
            {
                new Parameter(grid00, label00, slider00, "horizontalGreenDuration", 50),          // 0
                new Parameter(grid10, label10, slider10, "horizontalYellowDuration", 10),         // 1
                new Parameter(grid20, label20, slider20, "horizontalLineWidth", 10),              // 2
                new Parameter(grid30, label30, slider30, "verticalGreenDuration", 50),            // 3
                new Parameter(grid40, label40, slider40, "verticalYellowDuration", 10),           // 4
                new Parameter(grid01, label01, slider01, "verticalLineWidth", 10),                // 5
                new Parameter(grid11, label11, slider11, "timeToNextUpCar", 10),                  // 6
                new Parameter(grid21, label21, slider21, "timeToNextDownCar", 10),                // 7
                new Parameter(grid31, label31, slider31, "timeToNextLeftCar", 10),                // 8
                new Parameter(grid41, label41, slider41, "timeToNextRightCar", 10),               // 9
                new Parameter(grid02, label02, slider02, "maxPassengerCount", 5),                 // 10
                new Parameter(grid12, label12, slider12, "passengerCarSize", 1),                  // 11 
                new Parameter(grid22, label22, slider22, "passengerCarTimeToRideForward", 5),     // 12
                new Parameter(grid32, label32, slider32, "passengerCarTimeToTurnLeft", 7),        // 13
                new Parameter(grid42, label42, slider42, "passengerCarTimeToTurnRight", 3),       // 14
                new Parameter(grid03, label03, slider03, "truckCargoWeight", 1000),               // 15
                new Parameter(grid13, label13, slider13, "truckSize", 3),                         // 16
                new Parameter(grid23, label23, slider23, "truckTimeToRideForward", 10),           // 17
                new Parameter(grid33, label33, slider33, "truckTimeToTurnLeft", 15),              // 18
                new Parameter(grid43, label43, slider43, "truckTimeToTurnRight", 8),              // 19
                new Parameter(grid04, label04, slider04, "truckPart", 0.15 , false),         // 20
                new Parameter(grid34, label34, slider34, "simulation time", 100)                  // 21
            };
        }

        private void generatorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (generatorComboBox != null && Parameters != null)
            {
                var temp = Parameters.Skip(15).Take(6);
                if (generatorComboBox.SelectedIndex == 0)
                {
                    foreach (var parameter in temp)
                    {
                        parameter.Enable();
                    }
                }
                else
                {
                    foreach (var parameter in temp)
                    {
                        parameter.Disable();
                    }
                }
            }
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            var mn = Math.Min(Parameters[0].GetByte(), Parameters[3].GetByte());
            if (Parameters.Skip(12).Take(3).Max(x => x.GetByte()) < mn && (generatorComboBox.SelectedIndex != 0 || Parameters.Skip(17).Take(3).Max(x => x.GetByte()) < mn))
            {
                ICarGenerator gen;

                var timesToNextCars = new TimesToNextCars(Parameters[6].GetByte(), Parameters[7].GetByte(),
                    Parameters[8].GetByte(), Parameters[9].GetByte());
                var passExample = new PassengerCar(
                    maxPassengerCount: Parameters[10].GetByte(),
                    size: Parameters[11].GetByte(),
                    timeToRideForward: Parameters[12].GetByte(),
                    timeToTurnLeft: Parameters[13].GetByte(),
                    timeToTurnRight: Parameters[14].GetByte()
                );

                if (generatorComboBox.SelectedIndex == 0)
                {
                    var truckPart = Parameters[20].GetDouble();

                    var truckExample = new Truck(
                        cargoWeight: (uint)Parameters[15].GetInt(),
                        size: Parameters[16].GetByte(),
                        timeToRideForward: Parameters[17].GetByte(),
                        timeToTurnLeft: Parameters[18].GetByte(),
                        timeToTurnRight: Parameters[19].GetByte()
                    );

                    gen = new AllCarGenerator(timesToNextCars, truckPart, passExample, truckExample);
                }
                else
                {
                    gen = new OnlyPassengerCarGenerator(timesToNextCars, passExample);
                }

                var trafficLight = new SimpleTrafficLight(
                    Parameters[0].GetByte(),
                    Parameters[1].GetByte(),
                    Parameters[2].GetByte(),
                    Parameters[3].GetByte(),
                    Parameters[4].GetByte(),
                    Parameters[5].GetByte()
                );
               
                Simulator = new Simulator(gen, trafficLight);
                Simulator.Start();

                var sb = new StringBuilder();
                
                await Task.Factory.StartNew(() =>
                {
                    int time = 0; 

                    Dispatcher.Invoke(() => time = Parameters[21].GetInt());

                    for (int i = 0; i < time; i++)
                    { 
                        Simulator.NextTurn();
                    }
                    Simulator.End();

                    sb.AppendLine($"{nameof(Simulator.TotalTime)}:{Simulator.TotalTime}");
                    sb.AppendLine($"{nameof(Simulator.TotalCarsCount)}:{Simulator.TotalCarsCount}");
                    sb.AppendLine($"{nameof(Simulator.TotalEndRidingCarsCount)}:{Simulator.TotalEndRidingCarsCount}");
                    sb.AppendLine($"{nameof(Simulator.AverageWaitingRidingForwardTime)}:{Simulator.AverageWaitingRidingForwardTime}");
                    sb.AppendLine($"{nameof(Simulator.AverageWaitingTurningLeftTime)}:{Simulator.AverageWaitingTurningLeftTime}");
                    sb.AppendLine($"{nameof(Simulator.AverageWaitingTurningRightTime)}:{Simulator.AverageWaitingTurningRightTime}");
                    sb.AppendLine($"{nameof(Simulator.AverageRidingForwardTime)}:{Simulator.AverageRidingForwardTime}");
                    sb.AppendLine($"{nameof(Simulator.AverageTurningLeftTime)}:{Simulator.AverageTurningLeftTime}");
                    sb.AppendLine($"{nameof(Simulator.AverageTurningRightTime)}:{Simulator.AverageTurningRightTime}");

                    foreach (var x in Simulator.TotalEndRidingCarsStats)
                    {
                        sb.AppendLine($"EndRidingCars.Total{x.Key}:{x.Value}");
                    }

                    foreach (var x in Simulator.TotalEndRidingCarsCountByType)
                    {
                        sb.AppendLine($"{x.Key} Count:{x.Value}");
                    }
                });

                Dispatcher.Invoke(() => outputTextBlock.Text = sb.ToString());

                MessageBox.Show("The simulation is over");

                Dispatcher.Invoke(() =>
                {
                    saveButton.IsEnabled = true;
                    IsEnabled = true;
                });
            }
            else
            {
                MessageBox.Show("The driving time must be less than the duration of the green light");
                IsEnabled = true;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Simulator != null)
            {
                IsEnabled = false;

                using var allDataFileStream = new FileStream("data.csv", FileMode.Create);
                using var allDataStreamWriter = new StreamWriter(allDataFileStream);
                
                var statList = Simulator.AllCarsStats
                    .Select(x => (IEnumerable<string>) x.Stats.Keys.ToArray())
                    .Aggregate((x, y) => x.Union(y))
                    .ToList();

                allDataStreamWriter.WriteLine($"Type;Name;Size;Waiting Time;Riding Time;Direction;{String.Join(";", statList)}");

                foreach (var s in Simulator.AllCarsStats)
                {
                    var stats = statList.Select(x => "").ToList();
                    foreach (var k in s.Stats)
                    {
                        stats[statList.IndexOf(k.Key)] = k.Value.ToString();
                    }
                    allDataStreamWriter.WriteLine($"{s.Type};{s.Name};{s.Size};{s.WaitingTime};{s.RidingTime};{s.Direction};{String.Join(";", stats)}");
                }

                using var shortResultFileStream = new FileStream("short.txt", FileMode.Create);
                using var sw = new StreamWriter(shortResultFileStream);

                sw.WriteLine($"{nameof(Simulator.TotalTime)}:{Simulator.TotalTime}");
                sw.WriteLine($"{nameof(Simulator.TotalCarsCount)}:{Simulator.TotalCarsCount}");
                sw.WriteLine($"{nameof(Simulator.TotalEndRidingCarsCount)}:{Simulator.TotalEndRidingCarsCount}");
                sw.WriteLine($"{nameof(Simulator.AverageWaitingRidingForwardTime)}:{Simulator.AverageWaitingRidingForwardTime}");
                sw.WriteLine($"{nameof(Simulator.AverageWaitingTurningLeftTime)}:{Simulator.AverageWaitingTurningLeftTime}");
                sw.WriteLine($"{nameof(Simulator.AverageWaitingTurningRightTime)}:{Simulator.AverageWaitingTurningRightTime}");
                sw.WriteLine($"{nameof(Simulator.AverageRidingForwardTime)}:{Simulator.AverageRidingForwardTime}");
                sw.WriteLine($"{nameof(Simulator.AverageTurningLeftTime)}:{Simulator.AverageTurningLeftTime}");
                sw.WriteLine($"{nameof(Simulator.AverageTurningRightTime)}:{Simulator.AverageTurningRightTime}");

                foreach (var x in Simulator.TotalEndRidingCarsStats)
                {
                    sw.WriteLine($"EndRidingCars.Total{x.Key}:{x.Value}");
                }
                
                foreach (var x in Simulator.TotalEndRidingCarsCountByType)
                {
                    sw.WriteLine($"{x.Key} Count:{x.Value}");
                }

                MessageBox.Show("Result saved");

                IsEnabled = true;
            }
        }
    }
}
