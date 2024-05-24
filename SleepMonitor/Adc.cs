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
    // DEN RIGTIGE KLASSE TIL ADC

    public class Adc
    {
        private SpiDevice mcp3008;
        private int channel;

        public Adc(int busId, int chipSelectLine, int channel)
        {
            var settings = new SpiConnectionSettings(busId, chipSelectLine)
            {
                ClockFrequency = 500000, // Juster om nødvendigt
                Mode = SpiMode.Mode0
            };
         //   mcp3008 = SpiDevice.Create(settings);
            this.channel = channel;
        }
    }
}


        // _______________________________________________________________


        // SIMULERET METODE TIL AT LÆSE FAST DIGITAL VÆRDI




        //        // Read value from ADC

        //    public double ReadDigitalValue() // --> trådfunktion / thread
        //  {
        //    double value = mcp.Read(ChannelCount);
        //double value = 20; // pseudo værdi
        // values is between 0 and 1023
        //  Console.WriteLine($"{value}");
        // Thread.Sleep(250);
        // return value;
        //   }
        // }
        // }
   








/*

   
    // i2cdetect -y 1

    // ** kode modtaget af Lars Mortensen: 
  //  var hardwareSpiSettings = new SpiConnectionSettings(1, 42)
   // {
   //     ClockFrequency = 1000000
    // };


 */


