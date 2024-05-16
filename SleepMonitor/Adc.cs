using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Adc;

namespace SleepMonitor
{
    public class Adc
    {
        // Pseudo værdi
        //private double value = 20;

        private Mcp3008 mcp;
        private static Timer timer;
        
        // Field: Number of single ended input channel on the ADC
        protected new byte ChannelCount;

        // Property, input value
        public double ReferenceVoltage { get; set; }
        public int channel { get; private set; }

        // Contrcutor, (extern?)
        public Adc()
        {
            // Creating a new HW Spi object with two parameters, the busId and chipSelectLine
            // ** var hardwareSpiSettings = new SpiConnectionSettings(0, 0);
         //   var hardwareSpiSettings = new SpiConnectionSettings(0, 0); // ændres måske
            // i2cdetect -y 1

            // ** kode modtaget af Lars Mortensen: 
            //var hardwareSpiSettings = new SpiConnectionSettings(1, 42)
            //{
            //    ClockFrequency = 1000000
            //};

            // The object that actively communicates with the device, take the previous object as a parameter
            // ** SpiDevice spi = SpiDevice.Create(hardwareSpiSettings);
            // ** mcp = new Mcp3008(spi); // instantiere
           // SpiDevice spi = SpiDevice.Create(hardwareSpiSettings);
        //    mcp = new Mcp3008(spi); // instantiere
           
        }


        //Methods

        // Read value from ADC
      
        public double ReadDigitalValue() // --> trådfunktion / thread
        {
            // double value=  mcp.Read(ChannelCount);
            double value = 2; // pseudo værdi
             // values is between 0 and 1023
              Console.WriteLine($"{value}");
              Thread.Sleep(250);
              return value;
        }
    }
}
