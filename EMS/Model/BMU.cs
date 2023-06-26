using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class BMU
    {
        public ushort ID { get; set; }
        public ushort Voltage { get; set; }
        public ushort Temp { get; set; }
        public ushort ErrorCode { get; set; }
        public string BMUName { get; set; }
        public Ceil[] CeilArray { get; set; }

        public BMU() 
        {
            
        }
    }
}
