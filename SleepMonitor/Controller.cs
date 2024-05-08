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


namespace SleepMonitor
{
    public class Controller
    {
        public Stopwatch stopwatch;
        
        List<Controller> Sleepdata = new List<Controller>();
        public List<double> TempMeas { get; private set; } = new List<double>();
        public List<double> FiveMinMeas { get; private set; } = new List<double>();
        public List<double> CreateTask { get; private set; } = new List<double>();

        // private static Timer timer; Nødvendig?
        private Adc adc;

        public Controller() 
        { 
            stopwatch = new Stopwatch();
        }
       
        public int ReadFlexSensorValue() // Method to read the value of the long flex sensor
        {
            // Code to read the analog voltage value measured by the sensor
            // This is just a placeholder return value, replace it with actual implementation
            return 0;
        }

        // Method to start measuring sensor input from long flex sensors
        // Measurement starts when the start button on Cura’s platform is pressed
       public float StartReading()
        {
            // Code to start the measurement
            stopwatch.Start();
            while (true)
            {
            // Sleepdata.Add();
            }
            return 0;
           
        }

        public void Analysedata(double measurement) // measurement is the value from the ADC
        {
            while (true) // while the program is running
            {
                List<double> readings = new List<double>();
                for (int i = 0; i < 60; i++) // data læses hvert second
                {
                    double voltage = adc.AdcSpi();
                    readings.Add(voltage);
                    Thread.Sleep(1000);
                }
                //int OneMin = TimeSpan.Duration(1).TotalMinutes; 
                // Kan denne linje bruge til at sætte siden til 1 minut???

                double average = TempMeas.Average();
                TempMeas.Clear();
                FiveMinMeas.Add(new List<double>(average));
                // skal ændres til at være average, men average er ikke en liste
                // og kan derfor ikke tilføjjes. Hvordan gøres det?

                Console.WriteLine($"Average voltage for the minute: {average}");

                if (FiveMinMeas.Count == 5)
                {
                    double fiveMinAverage = FiveMinMeas.Average();
                    FiveMinMeas.Clear();
                    CreateTask.Add(fiveMinAverage);
                    // Samme problem som ovenfor

                    Console.WriteLine($"Average voltage for five minutes: {fiveMinAverage}");
                }


            }

        }


        // Method to stop measuring sensor input from long flex sensors
        // Measurement stops when the stop button on Cura’s platform is pressed
        public float StopReading()
        {
            // Stop the stopwatch to measure the time
            stopwatch.Stop();

            // Placeholder code to simulate sensor measurement
            // This is just a placeholder return value, replace it with actual implementation
            return 0.0f;
        }

        public void ResetData()
        {
            stopwatch.Reset();
            Sleepdata.Clear();
        }


    }
}
