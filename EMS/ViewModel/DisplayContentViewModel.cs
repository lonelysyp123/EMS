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
using System.Collections.Concurrent;

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

        //public ConcurrentQueue<TotalBatteryInfoModel> TotalBatteryInfoQueue;
        public IntegratedDevViewModel IntegratedDev;
        public DisplayContentViewModel()
        {
            IntegratedDev = new IntegratedDevViewModel();
            OnlineBatteryTotalList = new ObservableCollection<BatteryTotalBase>();
            TreeName = "EMS";
        }

        /// <summary>
        /// 开始保存电池数据
        /// </summary>
        /// <param name="TimeSpan">保存间隔</param>
        public void StartSaveBatteryInfo(int TimeSpan)
        {
            for (int i = 0; i < OnlineBatteryTotalList.Count; i++)
            {
                OnlineBatteryTotalList[i].StartRecordData(TimeSpan);
            }
        }

        /// <summary>
        /// 停止保存电池数据
        /// </summary>
        public void StopSaveBatteryInfo()
        {
            for (int i = 0; i < OnlineBatteryTotalList.Count; i++)
            {
                OnlineBatteryTotalList[i].StopRecordData();
            }
        }

        public void AddConnectedDev(BatteryTotalBase battery)
        {
            if(!OnlineBatteryTotalList.Contains(battery))
            {
                OnlineBatteryTotalList.Add(battery);
            }
        }
    }
}
