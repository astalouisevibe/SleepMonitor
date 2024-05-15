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
        // skal der oprettes instanser af RaspberryPi og Adc'en
        private RaspberryPiDll raspberryPi;
        private Adc adc;


        public Converter(RaspberryPiDll raspberryPi, Adc adc)
        {
            this.raspberryPi = raspberryPi;
            this.adc = adc;
        }

        // Denne metode er fra 1. semester --> kan ikke bruges i dette projekt
        //public int ConvertToBCD(int value)
        //{
        //    int hundreds = value / 100;
        //    int tens = (value - (hundreds * 100)) / 10;
        //    int ones = value % 10;
        //    string BCD = Convert.ToString(hundreds, 2).PadLeft(4, '0') + Convert.ToString(tens, 2).PadLeft(4, '0') + Convert.ToString(ones, 2).PadLeft(4, '0');
        //    return Convert.ToInt16(BCD, 2);
        //}

        // Der oprettes en ny ADC, som ikke er koblet til NuGet-pakken (skal fjernes)
        //public class Mcp3008
        //{
           

        //    private int channel;
        //    private float referenceVoltage;

        //    public Mcp3008(int channel, float referenceVoltage)
        //    {
        //        this.channel = channel;
        //        this.referenceVoltage = referenceVoltage;
        //    }

        //}
        // Method to convert the specified analog voltage to a digital value
        public double ConvertBitToVolt(double value)
        {
            double BitValue = adc.ReadDigitalValue();

            // express in percentage, rounds up to the nearest 10'th
            double PercentValue = Math.Round(BitValue / 10.23);
            Console.WriteLine($"{PercentValue}%");

            // express in volt
            double VoltValue = ((BitValue * 3.3) / 1023);
            Console.WriteLine($"{VoltValue} volt");

            Debug.WriteLine($"{VoltValue}%");

            return VoltValue;

            // waits 500 ms before it measures again
            Thread.Sleep(500);
           
            // Placeholder code for analog to digital conversion
            // Replace it with actual implementation
           
        }

        // Method for ADC SPI communication
        //public float ADCSpi()
        //{
        //    // Placeholder code for ADC SPI communication
        //    // Replace it with actual implementation
        //    return 0.0f;
        //}
    }
}
