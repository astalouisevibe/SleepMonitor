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
        public void ProcessFilesAndCreateObservations()
        {
            string filename = $"Data_{DateTime.Now:d}";
            List<string> UpdatedFiles = new List<string>();

            try
            {
                Downloader downloader = new Downloader("F24ST2GRP5_test"); // Create a Downloader instance with the same group name
                List<string> filesOnline = downloader.GetFilenames(); // find navn på fil

                foreach (var file in filesOnline)
                {
                    if (file.StartsWith(filename))
                    {
                        Console.WriteLine(file);
                        UpdatedFiles.Add(file);
                    }
                }

                foreach (var update in UpdatedFiles)
                {
                    FileStream newLocalStream = new FileStream(update, FileMode.Create);
                    downloader.Load(update, newLocalStream);
                }

          
                foreach (var update in UpdatedFiles)
                {
                    string[] readData = File.ReadAllLines(update);
                    foreach (var data in readData)
                    {
                        if (int.TryParse(data, out int number))
                        {
                            // Opret en observation baseret på de specificerede egenskaber
                            Observations observation = new Observations
                            {
                                ObservationCode = Convert.ToString(number),
                                ObservationIssued = DateTime.Now,
                                ObservationPerformer = "Plejehjemspersonale"

                            };

                            if (number <= 143)
                            {
                                Console.WriteLine("Borger er ikke i seng");
                                break;
                                    }

                                // Brug observationen efter behov
                                Console.WriteLine($"Observation: {observation.ObservationCode}, Issued: {observation.ObservationIssued:f}, Performer: {observation.ObservationPerformer}");
                        }
                        else
                        {
                            Console.WriteLine(" ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /*
        
        
        
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


                //Downloader downloader = new Downloader("F24ST2GRP5"); // Create a Downloader instance with the same group name
                //List<string> filesOnline = downloader.GetFilenames(); // find navn på fil
                //FileStream newLocalStream = new FileStream(filesOnline[filesOnline.Count], FileMode.Create); // Create a new file to save data in
                //downloader.Load(filesOnline[filesOnline.Count], newLocalStream); // Get data from the file specified (should match filename returned from uploader) 
                //// streamreader --> **

                // Test
                Downloader downloader = new Downloader("F24ST2GRP5_test"); // Create a Downloader instance with the same group name
                List<string> filesOnline = downloader.GetFilenames(); // find navn på fil
                FileStream newLocalStream = new FileStream("TestWithRandomNumbers.cvs", FileMode.Create); // Create a new file to save data in
                downloader.Load("test", newLocalStream); // Get data from the file specified (should match filename returned from uploader) 
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
            double VoltValue = ((value * 5) / 1023); //ref spænding måske ændres
            Console.WriteLine($"{VoltValue} volt");

            Debug.WriteLine($"{VoltValue}%");

            return VoltValue;
        }

        public List<Observations> ReadDataFromStream(FileStream stream)
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
                        //ObservationStatus = bool.Parse(data[0]),
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

        public List<Measurement> Measurements { get; set; }
        public List<FiveMinMeas> AverageMeasurements { get; set; }

        public Converter()
        {
            Measurements = new List<Measurement>();
            AverageMeasurements = new List<FiveMinMeas>();
        }


        public class FiveMinMeas
        {
            public DateTime Timestamp { get; set; }
            public double AverageValue { get; set; }
        }


        public class Measurement
        {
            public DateTime Timestamp { get; set; }
            public double Value { get; set; }
        }
    }
}
        */
    //______________________________________________________________________________________

    // KAN IKKE HUSKE OM DETTE BRUGES I SIMULERING - AFVENT SLETNING

    /* public double bitValue;
     public Converter(int bitValue)
     {
         this.bitValue = bitValue;
     }
    */

