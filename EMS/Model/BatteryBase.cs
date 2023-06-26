using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    /// <summary>
    /// 单个电池
    /// </summary>
    public class BatteryBase
    {
        public ushort Voltage { get; set; }
        public ushort Current { get; set; }
        public string BatteryID { get; set; }

        public BatteryBase() { }
    }
}
