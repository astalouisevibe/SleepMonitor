using System.Device.Gpio;
using System;
using RaspberryPiNetDll;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using FileShare;

namespace SleepMonitor
{
    public class Program
    {
        // DEN RIGTIGE KODE 
        /*
            static void Main(string[] args)
            {
                // Opret instanser af nødvendige objekter
                RaspberryPiDll _rpi = new RaspberryPiDll();
                RaspberryPiNetDll.Keys B1 = new Keys(_rpi, Keys.KEYS.SW1);

                // Opret en instans af Converter klassen
                Converter converter = new Converter();

                // Opret en instans af Adc klassen
                Adc adc = new Adc();

                if (!_rpi.Open())
                {
                    Console.WriteLine("Error with open communication to Raspberry Pi");
                    return;
                }


                // Læs bitværdi fra ADC'en
                double bitValue = adc.ReadDigitalValue();

                // Konverter bitværdi til volt ved hjælp af ConvertBitToVolt metoden i Converter klassen
                double voltValue = converter.ConvertBitToVolt(bitValue);

                // Opret en instans af Controller klassen og lever voltværdien som parameter
                Controller controller = new Controller(Convert.ToInt32(voltValue));

                // Start læsning af sensorer ved at kalde StartReading metoden på Controller instansen
                controller.StartReading();


                //UDVIDELSE MED KNAPTRYK
                /* if (B1.KeyPressed == 1)
                {
                    controller.StartReading();
                }
                */



        // _______________________________________________________________

        // SIMULERET KODE TIL AT TESTE
        // BN: pi, PW: raspberry, 

        static void Main(string[] args)
        {
            // Simulerede testmålinger, gennemsnitsværdier
            List<double> testValues = new List<double> { 20, 25, 30, 35, 40,45,55 }; // Eksempelværdier

            string fullPath = "..\\..\\..\\Sleepdata.json";

            try
            {
                // Opret lister til tidspunkter og målinger
                List<DateTime> timeStamps = new List<DateTime>();
                List<double> measurements = new List<double>();

                // Tilføj passende DateTime-værdier for hver testværdi
                DateTime currentTime = DateTime.Now;
                foreach (var testValue in testValues)
                {
                    timeStamps.Add(currentTime);
                    measurements.Add(testValue);
                }

                // Skriv værdierne til filen
                using (StreamWriter writer = new StreamWriter(fullPath, append: false))
                {
                    for (int i = 0; i < testValues.Count; i++)
                    {
                        writer.WriteLine($"{timeStamps[i]:g},{measurements[i]}");
                    }
                }

                Console.WriteLine($"Data has been written to {fullPath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

