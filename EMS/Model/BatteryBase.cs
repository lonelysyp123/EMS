using CommunityToolkit.Mvvm.ComponentModel;
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
    public class BatteryBase : ObservableObject
    {
        private double _voltage;
        /// <summary>
        /// 电压
        /// </summary>
        public double Voltage
        {

            get => _voltage;
            set
            {
                SetProperty(ref _voltage, value);
            }
        }

        private double _current;
        /// <summary>
        /// 电流
        /// </summary>
        public double Current
        {

            get => _current;
            set
            {
                SetProperty(ref _current, value);
            }
        }

        private double _temperature1;
        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature1
        {

            get => _temperature1;
            set
            {
                SetProperty(ref _temperature1, value);
            }
        }

        private double _temperature2;
        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature2
        {

            get => _temperature2;
            set
            {
                SetProperty(ref _temperature2, value);
            }
        }

        private double _soc;
        /// <summary>
        /// SOC
        /// </summary>
        public double SOC
        {

            get => _soc;
            set
            {
                SetProperty(ref _soc, value);
            }
        }

        private int _resistance;
        /// <summary>
        /// 单体内阻 mΩ
        /// </summary>
        public int Resistance
        {

            get => _resistance;
            set
            {
                SetProperty(ref _resistance, value);
            }
        }

        private double _capacity;
        /// <summary>
        /// 单体放满容量
        /// </summary>
        public double Capacity
        {

            get => _capacity;
            set
            {
                SetProperty(ref _capacity, value);
            }
        }

        /// <summary>
        /// 电池id
        /// </summary>
        public string BatteryID { get; set; }

        public BatteryBase() { }
    }
}
