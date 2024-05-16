using RaspberryPiNetDll;


namespace SleepMonitor
{
    public class Program
    {
        Adc adc = new Adc();
        static void Main(string[] args)
        {
            string files = Directory.GetCurrentDirectory();
            string[] files1 = Directory.GetFiles(files);
            string SmData = "C://Users//Programmering//Programmering s2//Søvnmonimoni//SleepMonitor//monimoni.txt";
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


        }
            RaspberryPiDll _rpi = new RaspberryPiDll();
            RaspberryPiNetDll.Keys B1 = new Keys(_rpi, Keys.KEYS.SW1);
            Controller controller = new Controller();



          /*  if (!_rpi.Open())
            {
                Console.WriteLine("Error with open communication to Raspberry Pi");
                return;
            }
          */

            while (true)
            {


                   controller.StartReading();


                    // Tjek om knappen er blevet trykket
                    /*if (B1.KeyPressed == 1)
                    {
                        controller.StartReading();

                    }
                    */

                }
            }
    }
}