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
    public class BatterySeriesBase
    {
        public ushort SeriesVoltage { get; set; }
        public ushort SeriesCurrent { get; set; }
        public string SeriesId { get; set; }

        public ObservableCollection<BatteryBase> Batteries { get; set;}

        public BatterySeriesBase()
        {
            Batteries = new ObservableCollection<BatteryBase>();
        }
    }
}
