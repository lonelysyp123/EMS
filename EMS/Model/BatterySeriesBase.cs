using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    /// <summary>
    /// 电池串
    /// </summary>
    public class BatterySeriesBase : ObservableObject
    {
        private double _seriesVoltage;
        /// <summary>
        /// 电池串总电压
        /// </summary>
        public double SeriesVoltage
        {

            get => _seriesVoltage;
            set
            {
                SetProperty(ref _seriesVoltage, value);
            }
        }

        private double _seriesCurrent;
        /// <summary>
        /// 电池串电流
        /// </summary>
        public double SeriesCurrent
        {

            get => _seriesCurrent;
            set
            {
                SetProperty(ref _seriesCurrent, value);
            }
        }

        private int _alarmCode;
        /// <summary>
        /// 告警状态
        /// </summary>
        public int AlarmCode
        {

            get => _alarmCode;
            set
            {
                SetProperty(ref _alarmCode, value);
            }
        }

        private int _faultCode;
        /// <summary>
        /// 故障状态
        /// </summary>
        public int FaultCode
        {

            get => _faultCode;
            set
            {
                SetProperty(ref _faultCode, value);
            }
        }

        private int _chargeChannelCode;
        /// <summary>
        /// 充电通道状态
        /// </summary>
        public int ChargeChannelCode
        {

            get => _chargeChannelCode;
            set
            {
                SetProperty(ref _chargeChannelCode, value);
            }
        }

        private int _chargeCapacitySum;
        /// <summary>
        /// 充电累计容量
        /// </summary>
        public int ChargeCapacitySum
        {

            get => _chargeCapacitySum;
            set
            {
                SetProperty(ref _chargeCapacitySum, value);
            }
        }

        /// <summary>
        /// 电池串id
        /// </summary>
        public string SeriesId { get; set; }

        /// <summary>
        /// 单串电池数
        /// </summary>
        public ushort BatteryCount { get; set; }

        /// <summary>
        /// 电池集合
        /// </summary>
        public ObservableCollection<BatteryBase> Batteries { get; set;}

        public BatterySeriesBase()
        {
            Batteries = new ObservableCollection<BatteryBase>();
        }
    }
}
