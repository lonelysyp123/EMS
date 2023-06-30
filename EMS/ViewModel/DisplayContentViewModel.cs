using CommunityToolkit.Mvvm.Input;
using EMS.Storage.DB.Models;
using EMS.Storage.DB.DBManage;
using EMS.Model;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class DisplayContentViewModel : ViewModelBase
    {
        private ObservableCollection<BatteryTotalBase> _onlineBatteryTotalList;
        public ObservableCollection<BatteryTotalBase> OnlineBatteryTotalList
        {
            get => _onlineBatteryTotalList;
            set
            {
                SetProperty(ref _onlineBatteryTotalList, value);
            }
        }

        private string _treeName;
        public string TreeName
        {
            get => _treeName;
            set
            {
                SetProperty(ref _treeName, value);
            }
        }

        public IntegratedDevViewModel IntegratedDev;
        public DisplayContentViewModel()
        {
            IntegratedDev = new IntegratedDevViewModel();
            OnlineBatteryTotalList = new ObservableCollection<BatteryTotalBase>();
            TreeName = "EMS";
            //AddTest();

            BatteryManage manage = new BatteryManage();
            manage.InsertBatteryModelInfo(new BatteryModel() { BatteryID = "Test", Current = 1, Voltage = 2});
            manage.QueryBatteryModelInfo("Test");
        }

        private void AddTest()
        {
            BatterySeriesBase series1 = new BatterySeriesBase() { SeriesId = "A", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "A-1" }, new BatteryBase() { BatteryID = "A-2" } } };
            BatterySeriesBase series2 = new BatterySeriesBase() { SeriesId = "B", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "B-1" }, new BatteryBase() { BatteryID = "B-2" } } };
            BatterySeriesBase series3 = new BatterySeriesBase() { SeriesId = "C", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "C-1" }, new BatteryBase() { BatteryID = "C-2" } } };
            OnlineBatteryTotalList = new ObservableCollection<BatteryTotalBase>() {
                new BatteryTotalBase()
                {
                    TotalID = "127.0.0.1",
                    SeriesCount = 3,
                    TotalCurrent = 0,
                    TotalVoltage = 0,
                    Series = new ObservableCollection<BatterySeriesBase>(){ series1, series2, series3 }
                }
            };
        }
    }
}
