using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Adc;
using RaspberryPiNetDll;
using static RaspberryPiNetDll.Led;
using Iot.Device.Adc;

namespace SleepMonitor
{
    public class Converter
    {
        // Method to convert the specified analog voltage to a digital value
        public double ConvertBitToVolt(double value)
        {
            try
            {
                Downloader downloader = new Downloader("F23_Gruppe_05"); // Create a Downloader instance with the same group name
                FileStream newLocalStream = new FileStream("monimoni", FileMode.Create); // Create a new file to save data in
                downloader.Load("monimoni", newLocalStream); // Get data from the file specified (should match filename returned from uploader) 
            }
            catch (Exception)
            {
                Console.WriteLine("here");
            }

            // express in percentage, rounds up to the nearest 10'th
            double PercentValue = Math.Round(value / 10.23);
            Console.WriteLine($"{PercentValue}%");

            // express in volt
            double VoltValue = ((value * 3.3) / 1023); //ref spænding måske ændres
            Console.WriteLine($"{VoltValue} volt");

            Debug.WriteLine($"{VoltValue}%");

            return VoltValue;
        }

    }


    //______________________________________________________________________________________

    // KAN IKKE HUSKE OM DETTE BRUGES I SIMULERING - AFVENT SLETNING

    /* public double bitValue;
     public Converter(int bitValue)
     {
         this.bitValue = bitValue;
     }
    */
}
