using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor
{
    public class Adc
    {
        // Pseudo værdi
        private double value = 20;

        private Mcp3008 mcp;
        private static Timer timer;


        // Field: Number of single ended input channel on the ADC
        protected new byte ChannelCount;

        // Property, input value
        public double ReferenceVoltage { get; set; }

        // Contrcutor, (extern?)
        public Adc()
        {
            // Creating a new HW Spi object with two parameters, the busId and chipSelectLine
            var hardwareSpiSettings = new SpiConnectionSettings(0, 0);

            // The object that actively communicates with the device, take the previous object as a parameter
            SpiDevice spi = SpiDevice.Create(hardwareSpiSettings);
            var mcp = new Mcp3008(spi);
        }


        //Methods

        // Read value from ADC
        public virtual int AdcSpi(int channel)
        {
            Console.WriteLine($"Input value is {channel}");
            return mcp.Read(channel);

        }

        public double ConvertToDigital() // --> trådfunktion / thread
        {
            //  return ReadAdc(ChannelCount);
            while (true)
            {
                Console.Clear();
                // values is between 0 and 1023
                double value = mcp.Read(0);
                Console.WriteLine($"{value}");
                // express in percentage, rounds up to the nearest 10'th
                Console.WriteLine($"{Math.Round(value / 10.23, 1)}%");
                // waits 500 ms before it measures again
                return (value * 3.3) / 1023;
                Thread.Sleep(500);
            }
        }
    }
