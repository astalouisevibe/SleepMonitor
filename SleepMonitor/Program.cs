using System.Device.Gpio;
using System;
using RaspberryPiNetDll;


namespace SleepMonitor
{
    // DEN RIGTIGE KODE 
    public class Program
    {
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
        }
    }
}




// _______________________________________________________________

// SIMULERET KODE TIL AT TESTE

/*
static void Main(string[] args)
{
    // Liste med testværdier
    List<double> FiveMinMeas = new List<double> { 10, 30, 40, 50, 60, 70,80,90}; // Eksempelværdier

    // Bestem filstien
    string SmData = "Sleepdata.json";
    string fullPath = "..\\..\\..\\Sleepdata.json";

    try
    {
        // Skriv værdierne til filen
        using (StreamWriter writer = new StreamWriter(fullPath, append: false))
        {
            foreach (double testValue in FiveMinMeas)
            {
                writer.WriteLine(testValue);
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
*/

