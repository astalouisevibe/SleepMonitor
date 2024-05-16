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
    }
}