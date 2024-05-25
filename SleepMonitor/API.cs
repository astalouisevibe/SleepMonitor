using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SleepMonitor
{
    internal class API
    {
        Converter converter { get; set; }
        public API(Converter converter)
        {
            this.converter = converter;
        }

        public void DataToJsonFile(string csvFilePath, string jsonFilePath)
        {
            try
            {
                // Læs værdierne fra .csv filen
                List<double> testValues = ReadValuesFromCsv(csvFilePath);

                // Beregn gennemsnittet af de første 1200 værdier
                double averageValue = CalculateAverage(testValues, 1200);

                // Opret en dictionary til at gemme gennemsnitsværdien med en timestamp
                var sleepData = new
                {
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
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

        private List<double> ReadValuesFromCsv(string filePath)
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
                    if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)) //
                    {
                        values.Add(value);
                    }
                }
            }

            return values;
        }

        private double CalculateAverage(List<double> values, int count) // ligges i converter
        {
            // Hvis der er færre værdier end count, så beregn gennemsnittet af alle tilgængelige værdier
            int actualCount = Math.Min(values.Count, count);

            if (actualCount == 0)
                return 0;

            double sum = values.Take(actualCount).Sum();
            return sum / actualCount;
        }

        private void WriteDataToJson(object data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (var writer = new StreamWriter(filePath, append: false))
            {
                writer.Write(json);
            }
        }
    }
}
