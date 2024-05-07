using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetDll;
using static RaspberryPiNetDll.Led;

namespace SleepMonitor
{
    internal class Converter
    {
        private RaspberryPiDll raspberryPi;
        private ADC adc;


        public Converter(RaspberryPiDll raspberryPi, ADC adc)
        {
            this.raspberryPi = raspberryPi;
            this.adc = adc;
        }

        public int ConvertToBCD(int value)
        {
            int hundreds = value / 100;
            int tens = (value - (hundreds * 100)) / 10;
            int ones = value % 10;
            string BCD = Convert.ToString(hundreds, 2).PadLeft(4, '0') + Convert.ToString(tens, 2).PadLeft(4, '0') + Convert.ToString(ones, 2).PadLeft(4, '0');
            return Convert.ToInt16(BCD, 2);
        }

        public class ADC
        {

            private int channel;
            private float referenceVoltage;

            public ADC(int channel, float referenceVoltage)
            {
                this.channel = channel;
                this.referenceVoltage = referenceVoltage;
            }

        }
        // Method to convert the specified analog voltage to a digital value
        public float ConvertToDigital(float analogValue)
        {
            // Placeholder code for analog to digital conversion
            // Replace it with actual implementation
            return analogValue;
        }

        // Method for ADC SPI communication
        public float ADCSpi()
        {
            // Placeholder code for ADC SPI communication
            // Replace it with actual implementation
            return 0.0f;
        }
    }
}
