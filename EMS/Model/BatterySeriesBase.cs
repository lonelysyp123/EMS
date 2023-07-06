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
        private ushort _seriesVoltage;
        /// <summary>
        /// 电池串总电压
        /// </summary>
        public ushort SeriesVoltage
        {

            get => _seriesVoltage;
            set
            {
                SetProperty(ref _seriesVoltage, value);
            }
        }

        private ushort _seriesCurrent;
        /// <summary>
        /// 电池串电流
        /// </summary>
        public ushort SeriesCurrent
        {

            get => _seriesCurrent;
            set
            {
                SetProperty(ref _seriesCurrent, value);
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
