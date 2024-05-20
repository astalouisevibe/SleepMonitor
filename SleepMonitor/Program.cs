using System.Device.Gpio;
using System;
using RaspberryPiNetDll;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using FileShare;
    using Newtonsoft.Json;
using System.Globalization;

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
            string csvFilePath = "..\\..\\..\\monimoni.csv";
            string jsonFilePath = "..\\..\\..\\Sleepdata.json";

            // Læs den målte værdi fra monimoni.csv
            double measuredValue = ReadMeasuredValueFromCsv(csvFilePath);

            // Initialiser ADC (du skal selv implementere oprettelsen af ADC objektet korrekt)
            Adc adc = new Adc(); // Initialiser med korrekt constructor for din hardware
            Controller controller = new Controller(adc);

            // Start målingen
            controller.StartReading();

            // Programmet fortsætter med andre opgaver eller venter på brugerinput
            Console.WriteLine("Målingen er startet. Tryk på en tast for at afslutte.");
            Console.ReadKey();
        }

        static double ReadMeasuredValueFromCsv(string filePath)
        {
            double measuredValue = 0.0;

            using (var reader = new StreamReader(filePath))
            {
                // Spring header-linjen over, hvis der er en
                string headerLine = reader.ReadLine();

                // Læs den første linje i .csv filen
                string firstLine = reader.ReadLine();
                if (double.TryParse(firstLine, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    measuredValue = value;
                }
            }

            return measuredValue;
        }
    }
}

/*
        static void Main(string[] args)
        {

            string csvFilePath = "..\\..\\..\\monimoni.csv";
            string jsonFilePath = "..\\..\\..\\Sleepdata.json";

            // Initialiser API med en passende controller, hvis nødvendigt
            Controller controller = new Controller();
            API api = new API(controller);

            // Kald API metoden til at behandle data og gemme som JSON
            api.DataToJsonFile(csvFilePath, jsonFilePath);



            // Simulerede testmålinger

            try
            {
                // Læs værdierne fra .csv filen
                List<double> testValues = ReadValuesFromCsv(csvFilePath);

                // Beregn gennemsnittet af de første 1200 værdier
                double averageValue = CalculateAverage(testValues, 1200);

                // Opret en dictionary til at gemme gennemsnitsværdien med en timestamp
                var sleepData = new
                {
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:"),
                    AverageValue = averageValue
                };

                // Skriv data til .json filen
                WriteDataToJson(sleepData, jsonFilePath);

                Console.WriteLine($"Data has been written to {jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static List<double> ReadValuesFromCsv(string filePath)
        {
            var values = new List<double>();

            using (var reader = new StreamReader(filePath))
            {
                // Spring header-linjen over, hvis der er en
                string headerLine = reader.ReadLine();

                // Læs hver linje i .csv filen
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                    {
                        values.Add(value);
                    }
                }
            }

            return values;
        }

        static double CalculateAverage(List<double> values, int count)
        {
            // Hvis der er færre værdier end count, så beregn gennemsnittet af alle tilgængelige værdier
            int actualCount = Math.Min(values.Count, count);

            if (actualCount == 0)
                return 0;

            double sum = values.Take(actualCount).Sum();
            return sum / actualCount;
        }

        static void WriteDataToJson(object data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (var writer = new StreamWriter(filePath, append: false))
            {
                writer.Write(json);
            }
        }
    }
}


*/

/*
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
                    Console.WriteLine($"{currentTime}" + " --- " +$"{testValue}");
                }

                // Skriv værdierne til filen
                using (StreamWriter writer = new StreamWriter(jsonFilePath, append: false))
                {
                    for (int i = 0; i < testValues.Count; i++)
                    {
                        writer.WriteLine($"{timeStamps[i]:g},{measurements[i]}");
                    }
                }


                Console.WriteLine($"Data has been written to {jsonFilePath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            } */



