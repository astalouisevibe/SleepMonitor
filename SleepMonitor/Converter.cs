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
    public  class Converter
    {
        private double _bitValue;
        public Converter(int bitValue)
        {
            _bitValue = bitValue;
        }
        // Method to convert the specified analog voltage to a digital value
        public double ConvertBitToVolt(double BitValue)
        {
            // express in percentage, rounds up to the nearest 10'th
            double PercentValue = Math.Round(BitValue / 10.23);
            Console.WriteLine($"{PercentValue}%");

            // express in volt
            double VoltValue = ((BitValue * 3.3) / 1023); //ref spænding måske ændres
            Console.WriteLine($"{VoltValue} volt");

            Debug.WriteLine($"{VoltValue}%");

            return VoltValue;
        }
    }
}
