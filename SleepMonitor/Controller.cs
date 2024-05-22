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
using Newtonsoft.Json;

namespace SleepMonitor
{
    public class Controller
    {
        public double Threshold = 0.5; // tilpasset
        public Stopwatch stopwatch = new Stopwatch();
        public Converter converter = new Converter();

        public List<Measurement> Measurements { get; private set; } = new List<Measurement>();
        public List<AverageMeasurement> AverageMeasurements { get; private set; } = new List<AverageMeasurement>();

        private Adc adc;

        public Controller(Adc adc)
        {
            this.adc = adc;
        }

        public void StartReading()
        {
            stopwatch.Start();
            Task.Run(() =>
            {
                while (true)
                {
                    double value = adc.ReadDigitalValue();

                    Measurements.Add(new Measurement { Timestamp = DateTime.Now, Value = value });

                    //if 5 min passed run update 
                    if (stopwatch.Elapsed.TotalMinutes >= 5)
                    {
                        var outofbed = Analysedata(); // split list into 5 get average and then return true if it worked
                        if (outofbed)
                            if (stopwatch.Elapsed.TotalMinutes >= 5)
                            {
                                CalculateAndStoreAverage();
                                stopwatch.Restart();
                            }

                        Thread.Sleep(250); // Assuming measurements are taken every 250ms
                    }
                }
            });
        }


        private void CalculateAndStoreAverage()
        {
            var lastFiveMinutesMeasurements = Measurements.Where(m => m.Timestamp >= DateTime.Now.AddMinutes(-5)).ToList();
            double averageValue = lastFiveMinutesMeasurements.Average(m => m.Value);
            DateTime firstMeasurementTime = lastFiveMinutesMeasurements.First().Timestamp;

            AverageMeasurements.Add(new AverageMeasurement { Timestamp = firstMeasurementTime, AverageValue = averageValue });
        }


        }
    }

    public class Measurement
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }

    public class AverageMeasurement
    {
        public DateTime Timestamp { get; set; }
        public double AverageValue { get; set; }
    }


