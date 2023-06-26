using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.DB
{
    public class BatteryModel
    {
        public ushort Voltage { get; set; }
        public ushort Current { get; set; }
        [Key]
        public string BatteryID { get; set; }
    }
}
