using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetDll;

namespace SleepMonitor
{
    public class Controller
    {
        public Stopwatch stopwatch;
        
        List<Controller> Sleepdata = new List<Controller>();

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
             //   Sleepdata.Add();
            }
            return 0;
           
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
