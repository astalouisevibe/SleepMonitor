﻿using System;
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
        private Mcp3008 mcp;
        private static Timer timer;
        protected new byte ChannelCount;
        public double ReferenceVoltage { get; set; }
        public int channel { get; private set; }

        private SpiDevice mcp3008;

        //public Adc()
        //{
        //    var connectionSettings = new SpiConnectionSettings(0, 0); // BusId 0, ChipSelectLine 0
        //    var spiController = SpiDevice.Create(connectionSettings);
        //    mcp3008 = spiController;
        //}

        /*public Adc()
        {
             Creating a new HW Spi object with two parameters, the busId and chipSelectLine
            var hardwareSpiSettings = new SpiConnectionSettings(0, 0);
             i2cdetect -y 1
        private SpiDevice spiDevice;
        

        public Adc()
        {
            var hardwareSpiSettings = new SpiConnectionSettings(0, 0);
            SpiDevice spi = SpiDevice.Create(hardwareSpiSettings);
            mcp = new Mcp3008(spi); // instantiere
        }

        //Read value from ADC
        public double ReadDigitalValue() // --> trådfunktion / thread
        {
            double value = mcp.Read(ChannelCount);
            //values is between 0 and 1023
            Console.WriteLine($"{value}");
            Thread.Sleep(250);
            return value;
        }
    }
        */


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


