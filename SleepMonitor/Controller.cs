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
        public double Threashold = 3.0; // tilpasses
        RaspberryPiDll _rpi;
        RaspberryPiNetDll.Keys B2;
        public Stopwatch stopwatch;
        public Converter converter;
        
        public List<double> FiveMinMeas { get; private set; } = new List<double>();
        public List<double> CreateTask { get; private set; } = new List<double>();

        // private static Timer timer; Nødvendig?
        private Adc adc;

        public Controller(int bitValue)
        {
            stopwatch = new Stopwatch();
            converter = new Converter();
            _rpi = new RaspberryPiDll();
            B2 = new Keys(_rpi, Keys.KEYS.SW2);
            adc = new Adc();
        }


        // Method to start measuring sensor input from long flex sensors
        // Measurement starts when the start button on Cura’s platform is pressed
       public void StartReading()
        {
            // Code to start the measurement
            stopwatch.Start();
            while (true)
            {
                double value = adc.ReadDigitalValue();
                double voltValue = converter.ConvertBitToVolt(value);
                
                FiveMinMeas.Add(voltValue);

                //if 5 min passed run update 
                if (stopwatch.Elapsed.TotalMinutes>=5) 
                {
                    var outofbed = Analysedata(); // split list into 5 get average and then return true if it worked
                    if (outofbed)
                    {
                        break;
                    }
                    // update disp console, opret display klasse (update)
                    stopwatch.Restart();
                    FiveMinMeas.Clear();
                }
                
                // if success clear list and sw
                // start over
            // logging?
            // check for updates // events

              //  if (stopwatch.Elapsed.TotalHours >= 8 /*|| B2.KeyPressed == 1 */ || value <= 1) // 1 er reference spænding = der er ingen i sengen
                //{
                  //  stopwatch.Stop();
                    //break;
                //}
              
            }
        }

        public bool Analysedata() // measurement is the value from the ADC
        {
            double average = FiveMinMeas.Average();
            if (average < Threashold)
            {
                Console.WriteLine("Alarm");
                CreateTask.Add(average);
                // Kald display, update metode 
                throw new ArgumentOutOfRangeException(); // excption: borger ikke længere i seng
            }
            CreateTask.Add(average);
            return false;


        }

        private Exception ArgumentOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException("Plejehjemsbeboeren er ikke længere i sengen");
        }

    }
}
