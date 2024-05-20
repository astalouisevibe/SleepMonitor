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

            int result = ((readBuffer[1] & 0x03) << 8) + readBuffer[2];

            Console.WriteLine($"Digital value: {result}");
            return result;
        }

    }
}













