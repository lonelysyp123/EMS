using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class SeriesBatteryInfoModel
    {
        [Key]
        public int ID { get; set; }
        public string BCMUID { get; set; }
        public string BMUID { get; set; }
        public int SeriesVoltage { get; set; }
        public int SeriesCurrent { get; set; }
        public int Voltage0 { get; set; }
        public int Voltage1 { get; set; }
        public int Voltage2 { get; set; }
        public int Voltage3 { get; set; }
        public int Voltage4 { get; set; }
        public int Voltage5 { get; set; }
        public int Voltage6 { get; set; }
        public int Voltage7 { get; set; }
        public int Voltage8 { get; set; }
        public int Voltage9 { get; set; }
        public int Voltage10 { get; set; }
        public int Voltage11 { get; set; }
        public int Voltage12 { get; set; }
        public int Voltage13 { get; set; }
        public int Voltage14 { get; set; }
        public int Voltage15 { get; set; }
        public int Current0 { get; set; }
        public int Current1 { get; set; }
        public int Current2 { get; set; }
        public int Current3 { get; set; }
        public int Current4 { get; set; }
        public int Current5 { get; set; }
        public int Current6 { get; set; }
        public int Current7 { get; set; }
        public int Current8 { get; set; }
        public int Current9 { get; set; }
        public int Current10 { get; set; }
        public int Current11 { get; set; }
        public int Current12 { get; set; }
        public int Current13 { get; set; }
        public int Current14 { get; set; }
        public int Current15 { get; set; }
        public int Temperature0 { get; set; }
        public int Temperature1 { get; set; }
        public int Temperature2 { get; set; }
        public int Temperature3 { get; set; }
        public int Temperature4 { get; set; }
        public int Temperature5 { get; set; }
        public int Temperature6 { get; set; }
        public int Temperature7 { get; set; }
        public int Temperature8 { get; set; }
        public int Temperature9 { get; set; }
        public int Temperature10 { get; set; }
        public int Temperature11 { get; set; }
        public int Temperature12 { get; set; }
        public int Temperature13 { get; set; }
        public int Temperature14 { get; set; }
        public int Temperature15 { get; set; }
        public int Temperature16 { get; set; }
        public int Temperature17 { get; set; }
        public int Temperature18 { get; set; }
        public int Temperature19 { get; set; }
        public int Temperature20 { get; set; }
        public int Temperature21 { get; set; }
        public int Temperature22 { get; set; }
        public int Temperature23 { get; set; }
        public int Temperature24 { get; set; }
        public int Temperature25 { get; set; }
        public int Temperature26 { get; set; }
        public int Temperature27 { get; set; }
        public int Temperature28 { get; set; }
        public int Temperature29 { get; set; }
        public int Temperature30 { get; set; }
        public int Temperature31 { get; set; }
        public DateTime HappenTime { get; set; }
    }
}
