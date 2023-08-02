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

        public double MinVoltage { get; set; }
        public int MinVoltageIndex { get; set; }
        public double MaxVoltage { get; set; }
        public int MaxVoltageIndex { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public int MinTemperatureIndex { get; set; }
        public int MaxTemperatureIndex { get; set; }

        public string AlarmState { get; set; }
        public string FaultState { get; set; }
        public string ChargeChannelState { get; set; }
        public double ChargeCapacitySum { get; set; }

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

        public double Capacity0 { get; set; }
        public double Capacity1 { get; set; }
        public double Capacity2 { get; set; }
        public double Capacity3 { get; set; }
        public double Capacity4 { get; set; }
        public double Capacity5 { get; set; }
        public double Capacity6 { get; set; }
        public double Capacity7 { get; set; }
        public double Capacity8 { get; set; }
        public double Capacity9 { get; set; }
        public double Capacity10 { get; set; }
        public double Capacity11 { get; set; }
        public double Capacity12 { get; set; }
        public double Capacity13 { get; set; }
        public double Capacity14 { get; set; }
        public double Capacity15 { get; set; }

        public double SOC0 { get; set; }
        public double SOC1 { get; set; }
        public double SOC2 { get; set; }
        public double SOC3 { get; set; }
        public double SOC4 { get; set; }
        public double SOC5 { get; set; }
        public double SOC6 { get; set; }
        public double SOC7 { get; set; }
        public double SOC8 { get; set; }
        public double SOC9 { get; set; }
        public double SOC10 { get; set; }
        public double SOC11 { get; set; }
        public double SOC12 { get; set; }
        public double SOC13 { get; set; }
        public double SOC14 { get; set; }
        public double SOC15 { get; set; }

        public double Resistance0 { get; set; }
        public double Resistance1 { get; set; }
        public double Resistance2 { get; set; }
        public double Resistance3 { get; set; }
        public double Resistance4 { get; set; }
        public double Resistance5 { get; set; }
        public double Resistance6 { get; set; }
        public double Resistance7 { get; set; }
        public double Resistance8 { get; set; }
        public double Resistance9 { get; set; }
        public double Resistance10 { get; set; }
        public double Resistance11 { get; set; }
        public double Resistance12 { get; set; }
        public double Resistance13 { get; set; }
        public double Resistance14 { get; set; }
        public double Resistance15 { get; set; }

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
