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
            mcp3008 = SpiDevice.Create(settings);
            this.channel = channel;
        }

        public double ReadDigitalValue()
        {
            byte[] writeBuffer = new byte[3];
            byte[] readBuffer = new byte[3];

            writeBuffer[0] = 0x01; // Start bit
            writeBuffer[1] = (byte)((8 + channel) << 4); // Single-ended mode, kanal valgt
            writeBuffer[2] = 0x00; // "Don't care" byte

            mcp3008.TransferFullDuplex(writeBuffer, readBuffer);

    // Hver gang nyt bliver bygget der skal over på RaspberryPi, skal det oploades (build --> public selections --> Publish)
    // publish / linux-arm : Indhold i mappen læggers over på RP
    // "Fileshare" anvendes til at flere kan tilgå samme mappe
    // Fileshare skal være en del af projektet - del af koden der skal lægges op
            int result = ((readBuffer[1] & 0x03) << 8) + readBuffer[2];

            Console.WriteLine($"Digital value: {result}");
            return result;
        }


        // _______________________________________________________________


        // SIMULERET METODE TIL AT LÆSE FAST DIGITAL VÆRDI


        
            private double fixedValue;

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
            public Adc(double fixedValue)
            {
                this.fixedValue = fixedValue;
            }

            // Simuleret metode til at læse en fast digital værdi
            public double ReadDigitalValue()
            {
                return fixedValue;
            }
        }
              }












/*

   
    // i2cdetect -y 1

    // ** kode modtaget af Lars Mortensen: 
  //  var hardwareSpiSettings = new SpiConnectionSettings(1, 42)
   // {
   //     ClockFrequency = 1000000
    // };


 */


