using System.Text.Json;
using System;


namespace SleepMonitor
{
    internal class API
    {
        private Controller controller;

        public API(Controller controller) 
        {
            this.controller = controller;
        }

        public void DataToJsonFile(string filePath)
        {
    //        float data = controller.GetData();

    //        var jsonData = new { Data = data };


   //         string json = JsonSerializer.Serialize(jsonData);

    //        File.WriteAllText(filePath, json);
    //        Console.WriteLine("Data exporteret som JsonFil");
        }
    }
}
