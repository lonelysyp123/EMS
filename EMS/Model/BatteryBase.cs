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
        private ushort _voltage;
        /// <summary>
        /// 电压
        /// </summary>
        public ushort Voltage
        {

            get => _voltage;
            set
            {
                SetProperty(ref _voltage, value);
            }
        }

        private ushort _current;
        /// <summary>
        /// 电流
        /// </summary>
        public ushort Current
        {

            get => _current;
            set
            {
                SetProperty(ref _current, value);
            }
        }

        /// <summary>
        /// 电池id
        /// </summary>
        public string BatteryID { get; set; }

        public BatteryBase() { }
    }
}
