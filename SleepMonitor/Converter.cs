using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Adc;
using RaspberryPiNetDll;
using Iot.Device.Adc;
using FileShare;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace SleepMonitor
{
    public class Converter 
    {
        // Contructor?

        // Method to convert the specified analog voltage to a digital value
        public double ConvertBitToVolt(double value)
        {
            try
            {
                // DEMO upload
                //Uploader uploader = new Uploader("F23_Gruppe_05"); // Create an Uploader instance with a group name
                //FileStream localFileStream = new FileStream("monimoni.csv", FileMode.Open); // Open a filestream to data
                //string filename = uploader.Save("monimoni", localFileStream); // Upload data to a file
                // DEMO end


                Downloader downloader = new Downloader("F24ST2GRP5"); // Create a Downloader instance with the same group name
                List<string> filesOnline = downloader.GetFilenames(); // find navn på fil
                FileStream newLocalStream = new FileStream(filesOnline[filesOnline.Count], FileMode.Create); // Create a new file to save data in
                downloader.Load(filesOnline[filesOnline.Count], newLocalStream); // Get data from the file specified (should match filename returned from uploader) 
                // streamreader --> **

                var observations = ReadDataFromStream(newLocalStream);
                foreach (var observation in observations)
                {
                    Console.WriteLine($"Status: {observation.ObservationStatus}, Code: {observation.ObservationCode}, Issued: {observation.ObservationIssued}, Performer: {observation.ObservationPerformer}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fejl i upload og/eller download");
            }

            // express in percentage, rounds up to the nearest 10'th
            double PercentValue = Math.Round(value / 10.23);
            Console.WriteLine($"{PercentValue}%");

            // express in volt
            double VoltValue = ((value * 3.3) / 1023); //ref spænding måske ændres
            Console.WriteLine($"{VoltValue} volt");

            Debug.WriteLine($"{VoltValue}%");

            return VoltValue;
        }

        private List<Observations> ReadDataFromStream(FileStream stream)
        {
            List<Observations> observations = new List<Observations>();
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(','); // Assuming CSV format
                    observations.Add(new Observations
                    {
                        ObservationStatus = bool.Parse(data[0]),
                        ObservationCode = data[1],
                        ObservationIssued = DateTime.Parse(data[2]),
                        ObservationPerformer = data[3]
                    });
                }
            }
            return observations;
        }


        private void CalculateAndStoreAverage()
        {
            var lastFiveMinutesMeasurements = Measurements.Where(m => m.Timestamp >= DateTime.Now.AddMinutes(-5)).ToList();
            // ** streamreader i stedet for lastFiveMinutesMeasurements
            double averageValue = lastFiveMinutesMeasurements.Average(m => m.Value);
            DateTime firstMeasurementTime = lastFiveMinutesMeasurements.First().Timestamp;

            AverageMeasurements.Add(new FiveMinMeas { Timestamp = firstMeasurementTime, AverageValue = averageValue });
            SaveAverageMeasurementsToJson();
        }

        private void SaveAverageMeasurementsToJson()
        {
            string jsonFilePath = "..\\..\\..\\Sleepdata.json";
            var dataToSave = AverageMeasurements.Select(a => new
            {
                TimeStamp = a.Timestamp.ToString("yyyy-MM-dd HH:mm"),
                AverageValue = a.AverageValue
            }).ToList();

            string json = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }


    }
    public class FiveMinMeas
    {
        public DateTime Timestamp { get; set; }
        public double AverageValue { get; set; }
    }


    //______________________________________________________________________________________

    // KAN IKKE HUSKE OM DETTE BRUGES I SIMULERING - AFVENT SLETNING

    /* public double bitValue;
     public Converter(int bitValue)
     {
         this.bitValue = bitValue;
     }
    */
}
