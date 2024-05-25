using System.Device.Gpio;
using System;
using RaspberryPiNetDll;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using FileShare;
using System.Globalization;

namespace SleepMonitor
{
    public class Program
    {

        static void Main(string[] args)
        {

            // Opret instanser af nødvendige objekter
            string jsonFilePath = "..\\..\\..\\Sleepdata.json";
            Converter converter = new Converter();
            converter.ProcessFilesAndCreateObservations(jsonFilePath);
        }
    }
}

         