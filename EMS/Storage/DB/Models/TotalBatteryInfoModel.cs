using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class TotalBatteryInfoModel
    {
        public int BCMUID { get; set; }
        public int TotalVoltage { get; set; }
        public int TotalCurrent { get; set; }
        public int AverageTemperature { get; set; }
    }
}
