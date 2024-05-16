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

namespace SleepMonitor
{
    public class Controller
    {
        //pseudo værdi: 
        public double UpperThreashold = 3.0; // tilpasses
        public double LowerThreashold = 1.5; //fjernes?
        public Stopwatch stopwatch;
        public Converter converter = new Converter();
        
        List<Controller> Sleepdata = new List<Controller>();
        public List<double> TempMeas { get; private set; } = new List<double>(); // slettes
        public List<double> FiveMinMeas { get; private set; } = new List<double>();
        public List<double> CreateTask { get; private set; } = new List<double>();

        // private static Timer timer; Nødvendig?
        private Adc adc;

        public Controller() 
        { 
            stopwatch = new Stopwatch();
            adc = new Adc();

        }
       
        public int ReadFlexSensorValue() // Method to read the value of the long flex sensor
        {
            // Code to read the analog voltage value measured by the sensor
            // This is just a placeholder return value, replace it with actual implementation
            return 0;
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
            }
        }

        public bool Analysedata() // measurement is the value from the ADC
        {
            double average = FiveMinMeas.Average();
            if (average < UpperThreashold || average > LowerThreashold) // fjerne lowerthreashold
            {
                Console.WriteLine("Alarm");
                CreateTask.Add(average);
                // Kald display, update metode 
                throw new ArgumentOutOfRangeException(); // excption: borger ikke længere i seng
            }
            CreateTask.Add(average);
            return false;


            //while (true) // while the program is running
            //{
            //    List<double> readings = new List<double>();
            //    for (int i = 0; i < 60; i++) // data læses hvert second i 1 min 
            //    {
            //        double voltage = adc.AdcSpi();
            //        readings.Add(voltage);
            //        Thread.Sleep(250);
            //    }
            //    // int OneMin = TimeSpan.Duration(1).TotalMinutes; 
            //    // Kan denne linje bruge til at sætte siden til 1 minut???

            //    double average = TempMeas.Average();
            //    TempMeas.Clear();
            //    FiveMinMeas.Add(average);

            //    Console.WriteLine($"Average voltage for the minute: {average}");

            //    if (FiveMinMeas.Count == 5)
            //    {
            //        double fiveMinAverage = FiveMinMeas.Average();
            //        FiveMinMeas.Clear();
            //        CreateTask.Add(fiveMinAverage);
            //        // Samme problem som ovenfor

            //        Console.WriteLine($"Average voltage for five minutes: {fiveMinAverage}");
            //    }
            }

        private Exception ArgumentOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException("Plejehjemsbeboeren er ikke længere i sengen");
        }
    


        // Method to stop measuring sensor input from long flex sensors
        // Measurement stops when the stop button on Cura’s platform is pressed
        //public float StopReading()
        //{
        //    // Stop the stopwatch to measure the time
        //    stopwatch.Stop();

        //    // Placeholder code to simulate sensor measurement
        //    // This is just a placeholder return value, replace it with actual implementation
        //    return 0.0f;
        //}

        //public void ResetData()
        //{
        //    stopwatch.Reset();
        //    Sleepdata.Clear();
        //}


    }
}
