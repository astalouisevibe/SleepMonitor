using FileShare;
namespace testFileShare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataset = $"Data_{DateTime.Now:f}";
            string filename = $"Data_{DateTime.Now:d}";
            List<string> UpdatedFiles = new List<string>();
            try
            {
                // Test
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
                // Console.WriteLine("fejl" + filesOnline);
                //FileStream newLocalStream = new FileStream(dataset, FileMode.Create); // Create a new file to save data in
                //downloader.Load(filename, newLocalStream); // Get data from the file specified (should match filename returned from uploader) 
                //// streamreader --> **

                foreach (var update in UpdatedFiles)
                {
                    string[] readData = File.ReadAllLines(update);
                    foreach (var data in readData)
                    {
                        try
                        {
                            var number = Convert.ToInt32(data);
                            // gemme i 5 min liste
                            //Console.WriteLine(number);

                            Observations observation = new Observations
                            {
                                ObservationCode = number,
                                ObservationIssued = DateTime.Now:f,
                                ObservationPerformer = "Plejehjemspersonale"
                            }
                            Console.WriteLine($"Observation: {observation.ObservationCode}, Issued: {observation.ObservationIssued}, Performer: {observation.ObservationPerformer}}");
                        }
                        catch (Exception e)
                        {
                        
                        }

                    }
                    //  avarage beregner
                    // printe alarm hvis graensevaerdi er over
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
