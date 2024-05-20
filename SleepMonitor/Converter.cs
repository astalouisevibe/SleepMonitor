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
