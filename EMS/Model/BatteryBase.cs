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
        public ushort Voltage
        {

            get => _voltage;
            set
            {
                SetProperty(ref _voltage, value);
            }
        }

        private ushort _current;
        public ushort Current
        {

            get => _current;
            set
            {
                SetProperty(ref _current, value);
            }
        }

        public string BatteryID { get; set; }

        public BatteryBase() { }
    }
}
