using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        private SolidColorBrush _voltageColor;
        /// <summary>
        /// 电压值颜色(红色=高温，蓝色=低温)
        /// </summary>
        public SolidColorBrush VoltageColor 
        { 
            get => _voltageColor; 
            set
            {
                SetProperty(ref _voltageColor, value);
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

        private SolidColorBrush _temperatureColor;
        /// <summary>
        /// 温度值颜色(红色=高温，蓝色=低温)
        /// </summary>
        public SolidColorBrush TemperatureColor
        {
            get => _temperatureColor;
            set
            {
                SetProperty(ref _temperatureColor, value);
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

        private int _batteryNumber;
        public int BatteryNumber
        {
            get { return _batteryNumber; }
            set { SetProperty(ref _batteryNumber, value); }
        }
        public BatteryBase() 
        {

        }
    }
}
