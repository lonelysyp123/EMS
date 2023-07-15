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
        [Key]
        public int ID { get; set; }
        public string BCMUID { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double SOH { get; set; }
        public double SOC { get; set; }
        public double AverageTemperature { get; set; }
        public DateTime HappenTime { get; set; }
    }
}
