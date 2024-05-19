using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetDll;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Device.Spi;
using System.Threading;
using Iot.Device.Adc; // ADC nuget-pakke
using UnitsNet;
using System.Runtime.Intrinsics.X86;
using System.Device.Gpio;

namespace SleepMonitor
{
    public class Controller
        {
            public double Threshold = 0.5; // tilpasset
            private RaspberryPiDll _rpi;
            private RaspberryPiNetDll.Keys B2;
            public Stopwatch stopwatch;
            public Converter converter;

            public List<double> FiveMinMeas { get; private set; } = new List<double>();
            public List<double> CreateTask { get; private set; } = new List<double>();

            private Adc adc;
        private double measuredValue;

        public Controller(Adc adc)
            {
                stopwatch = new Stopwatch();
                converter = new Converter();
                _rpi = new RaspberryPiDll();
                B2 = new RaspberryPiNetDll.Keys(_rpi, RaspberryPiNetDll.Keys.KEYS.SW2);
                this.adc=adc; 
            }

        public Controller(double measuredValue)
        {
            this.measuredValue = measuredValue;
        }

        public void StartReading()
            {
                stopwatch.Start();
                while (stopwatch.Elapsed.TotalMinutes<=2)  // ændress til 5 minutter
                {
                    double value = adc.ReadDigitalValue();
                    double voltValue = converter.ConvertBitToVolt(value);

                    FiveMinMeas.Add(voltValue);

                    if (stopwatch.Elapsed.TotalMinutes >= 1)
                    {
                        var outofbed = AnalyzeData();
                        if (outofbed)
                        {
                            break;
                        }
                        stopwatch.Restart();
                        FiveMinMeas.Clear();
                    }
                }
            }

            public bool AnalyzeData()
            {
                double average = FiveMinMeas.Average();
                if (average < Threshold)
                {
                    Console.WriteLine("Alarm");
                    CreateTask.Add(average);
                Console.WriteLine("borger ikke i seng");
            }
                CreateTask.Add(average);
                return false;
            }
        }
    }

