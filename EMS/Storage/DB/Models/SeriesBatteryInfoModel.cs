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
        public double SeriesVoltage { get; set; }
        public double SeriesCurrent { get; set; }
        public double Voltage0 { get; set; }
        public double Voltage1 { get; set; }
        public double Voltage2 { get; set; }
        public double Voltage3 { get; set; }
        public double Voltage4 { get; set; }
        public double Voltage5 { get; set; }
        public double Voltage6 { get; set; }
        public double Voltage7 { get; set; }
        public double Voltage8 { get; set; }
        public double Voltage9 { get; set; }
        public double Voltage10 { get; set; }
        public double Voltage11 { get; set; }
        public double Voltage12 { get; set; }
        public double Voltage13 { get; set; }
        public double Voltage14 { get; set; }
        public double Voltage15 { get; set; }
        public double Current0 { get; set; }
        public double Current1 { get; set; }
        public double Current2 { get; set; }
        public double Current3 { get; set; }
        public double Current4 { get; set; }
        public double Current5 { get; set; }
        public double Current6 { get; set; }
        public double Current7 { get; set; }
        public double Current8 { get; set; }
        public double Current9 { get; set; }
        public double Current10 { get; set; }
        public double Current11 { get; set; }
        public double Current12 { get; set; }
        public double Current13 { get; set; }
        public double Current14 { get; set; }
        public double Current15 { get; set; }
        public double Temperature0 { get; set; }
        public double Temperature1 { get; set; }
        public double Temperature2 { get; set; }
        public double Temperature3 { get; set; }
        public double Temperature4 { get; set; }
        public double Temperature5 { get; set; }
        public double Temperature6 { get; set; }
        public double Temperature7 { get; set; }
        public double Temperature8 { get; set; }
        public double Temperature9 { get; set; }
        public double Temperature10 { get; set; }
        public double Temperature11 { get; set; }
        public double Temperature12 { get; set; }
        public double Temperature13 { get; set; }
        public double Temperature14 { get; set; }
        public double Temperature15 { get; set; }
        public double Temperature16 { get; set; }
        public double Temperature17 { get; set; }
        public double Temperature18 { get; set; }
        public double Temperature19 { get; set; }
        public double Temperature20 { get; set; }
        public double Temperature21 { get; set; }
        public double Temperature22 { get; set; }
        public double Temperature23 { get; set; }
        public double Temperature24 { get; set; }
        public double Temperature25 { get; set; }
        public double Temperature26 { get; set; }
        public double Temperature27 { get; set; }
        public double Temperature28 { get; set; }
        public double Temperature29 { get; set; }
        public double Temperature30 { get; set; }
        public double Temperature31 { get; set; }
        public DateTime HappenTime { get; set; }
    }
}
