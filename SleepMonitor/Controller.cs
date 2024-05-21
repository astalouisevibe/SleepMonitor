using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetDll;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Device.Spi;
using System.Threading;
using Iot.Device.Adc; // ADC nuget-pakke
using UnitsNet;
using System.Runtime.Intrinsics.X86;
using System.Device.Gpio;
using Newtonsoft.Json;

namespace SleepMonitor
{
    public class Controller
    {
        //public double Threshold = 0.5; // tilpasset

        //public List<Measurement> Measurements { get; private set; } = new List<Measurement>();
        // Listen 'Measurement' ligger i solution 2
        //public List<FiveMinMeas> AverageMeasurements { get; private set; } = new List<FiveMinMeas>();
    }

    //public class Measurement
    //{
    //    public DateTime Timestamp { get; set; }
    //    public double Value { get; set; }
    //}

    
}


