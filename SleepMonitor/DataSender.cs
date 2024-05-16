using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor
{
  
    public class Observations
    {
        public bool ObservationStatus { get; set; }
        
        public string ObservationCode { get; set; }
        public DateTime ObservationIssued { get; set; }
        public string ObservationPerformer { get; set; }
    }
}
