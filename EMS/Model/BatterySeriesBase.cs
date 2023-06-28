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
        public ushort SeriesVoltage
        {

            get => _seriesVoltage;
            set
            {
                SetProperty(ref _seriesVoltage, value);
            }
        }

        private ushort _seriesCurrent;
        public ushort SeriesCurrent
        {

            get => _seriesCurrent;
            set
            {
                SetProperty(ref _seriesCurrent, value);
            }
        }

        public string SeriesId { get; set; }
        public ushort BatteryCount { get; set; }

        public ObservableCollection<BatteryBase> Batteries { get; set;}

        public BatterySeriesBase()
        {
            Batteries = new ObservableCollection<BatteryBase>();
        }
    }
}
