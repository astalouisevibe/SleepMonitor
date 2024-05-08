using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor
{
    // Extention 3.0
    //public class Patient
    //{
    //    public int PatientIdentifier { get; set; }
    //    public string PatientName { get; set;}
    //    public string PatientCommunicationLanguage { get; set;}


    //}

    public class Observations
    {
        public bool ObservationStatus { get; set; }
        
        public string ObservationCode { get; set; }
        public DateTime ObservationIssued { get; set; }
        public string ObservationPerformer { get; set; }
    }
}
