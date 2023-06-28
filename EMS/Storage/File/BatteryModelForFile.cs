using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.File
{
    public class BatteryModelForFile
    {
        public int ID { get; set; }
        public string BatteryID { get; set; }
        public int Voltage { get; set; }
        public int Current { get; set; }
    }
}
