using RaspberryPiNetDll;


namespace SleepMonitor
{
    public class Program
    {
        static void Main(string[] args)
        {
            RaspberryPiDll _rpi = new RaspberryPiDll();
            RaspberryPiNetDll.Keys B1 = new Keys(_rpi, Keys.KEYS.SW1);
            Controller controller = new Controller();

            if (!_rpi.Open())
            {
                Console.WriteLine("Error with open communication to Raspberry Pi");
                return;
            }


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