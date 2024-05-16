using RaspberryPiNetDll;


namespace SleepMonitor
{
    public class Program
    {
        static void Main(string[] args)
        {


            string files = Directory.GetCurrentDirectory();
            string[] files1 = Directory.GetFiles(files);
            string SmData = "monimoni.txt"; // kan evt. ændres til relativ
            string fullPath = Path.Combine(Environment.CurrentDirectory, SmData);
            string[] lines = File.ReadAllLines(SmData);

           
               foreach (string file in lines)
               {
                   Console.WriteLine(file);
               }
               using (StreamReader sr = new StreamReader(SmData))
               {
                   while (sr.EndOfStream != true)
                   {
                       string? line = sr.ReadLine();
                       Console.WriteLine($"{line}");
                   }
               }

          

            // Opret instanser af nødvendige objekter
            RaspberryPiDll _rpi = new RaspberryPiDll();
            RaspberryPiNetDll.Keys B1 = new Keys(_rpi, Keys.KEYS.SW1);
          
            // Opret en instans af Converter klassen
            Converter converter = new Converter();

            // Opret en instans af Adc klassen
            Adc adc = new Adc();

            /*  if (!_rpi.Open())
             {
                 Console.WriteLine("Error with open communication to Raspberry Pi");
                 return;
             }
           */

            // Læs bitværdi fra ADC'en
            double bitValue = adc.ReadDigitalValue();

            // Konverter bitværdi til volt ved hjælp af ConvertBitToVolt metoden i Converter klassen
            double voltValue = converter.ConvertBitToVolt(bitValue);

            // Opret en instans af Controller klassen og lever voltværdien som parameter
            Controller controller = new Controller(Convert.ToInt32(voltValue));

            // Start læsning af sensorer ved at kalde StartReading metoden på Controller instansen
            controller.StartReading();



           /* if (B1.KeyPressed == 1)
            {
                controller.StartReading();
            }
           */
          
            // måske dvale

            }
        }
    }

   
