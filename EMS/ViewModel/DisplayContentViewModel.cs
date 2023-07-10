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
using System.Threading;
using EMS.Common.Modbus.ModbusTCP;
using OxyPlot.Series;
using System.ComponentModel;
using System.Windows;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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
        private List<ModbusClient> ClientList;
        public int DaqTimeSpan = 0;
        public IntegratedDevViewModel IntegratedDev;
        public DisplayContentViewModel()
        {
            IntegratedDev = new IntegratedDevViewModel();
            OnlineBatteryTotalList = new ObservableCollection<BatteryTotalBase>();
            TreeName = "EMS";
            ClientList = new List<ModbusClient>();
        }

        public void AddConnectedDev(BatteryTotalBase battery)
        {
            if(!OnlineBatteryTotalList.Contains(battery))
            {
                if (int.TryParse(battery.Port, out int port))
                {
                    OnlineBatteryTotalList.Add(battery);
                    ClientList.Add(new ModbusClient(battery.IP, port));
                    ConnectBCMU(OnlineBatteryTotalList.Count-1);
                }
                else
                {
                    // 设备连接信息有误
                    throw new Exception("Connect Info is Error");
                }
            }
        }

        public void RemoveDisConnectedDev(BatteryTotalBase item)
        {
            if (!IsStartReadData)
            {
                Disconnect(OnlineBatteryTotalList.IndexOf(item));
                OnlineBatteryTotalList.Remove(item);
            }
            else
            {
                MessageBox.Show("请先停止数据采集");
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        private void ConnectBCMU(int index)
        {
            try
            {
                if (!OnlineBatteryTotalList[index].IsConnected)
                {
                    ClientList[index].Connect();
                    OnlineBatteryTotalList[index].IsConnected = true;
                    OnlineBatteryTotalList[index].ImageTitleChange();
                    InitBatteryTotal(index);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect(int index)
        {
            if (OnlineBatteryTotalList[index].IsConnected)
            {
                ClientList[index].Disconnect();
                OnlineBatteryTotalList[index].IsConnected = false;
                OnlineBatteryTotalList[index].ImageTitleChange();
            }
        }

        /// <summary>
        /// 初始化电池总簇信息
        /// </summary>
        public void InitBatteryTotal(int index)
        {
            if (OnlineBatteryTotalList[index].IsConnected)
            {
                // 信息补全
                OnlineBatteryTotalList[index].BCMUID = ClientList[index].ReadU16(10000).ToString();
                OnlineBatteryTotalList[index].TotalVoltage = ClientList[index].ReadU16(10001) * 0.001;
                OnlineBatteryTotalList[index].TotalCurrent = ClientList[index].ReadU16(10002) * 0.1;
                OnlineBatteryTotalList[index].SeriesCount = ClientList[index].ReadU16(10100);
                OnlineBatteryTotalList[index].Series.Clear();
                for (int i = 0; i < OnlineBatteryTotalList[index].SeriesCount; i++)
                {
                    BatterySeriesBase series = new BatterySeriesBase();
                    series.SeriesId = ClientList[index].ReadU16((ushort)(11000 + i * 10)).ToString();
                    series.SeriesVoltage = ClientList[index].ReadU16((ushort)(11001 + i * 10)) * 0.001;
                    series.SeriesCurrent = ClientList[index].ReadU16((ushort)(11002 + i * 10)) * 0.1;
                    series.BatteryCount = ClientList[index].ReadU16((ushort)(10200 + i * 10));
                    for (int j = 0; j < series.BatteryCount; j++)
                    {
                        BatteryBase battery = new BatteryBase();
                        battery.BatteryID = ClientList[index].ReadU16((ushort)(12000 + j * 10)).ToString();
                        battery.Voltage = ClientList[index].ReadU16((ushort)(12001 + j * 10)) * 0.001;
                        battery.Current = ClientList[index].ReadU16((ushort)(12002 + j * 10)) * 0.1;
                        //battery.Temperature = client.ReadU16((ushort)(12002 + j * 10)) * 0.1;
                        //battery.SOC = client.ReadU16((ushort)(12002 + j * 10)) * 0.1;
                        //battery.Capacity = client.ReadU16((ushort)(12002 + j * 10)) * 0.1;
                        //battery.Resistance = client.ReadU16((ushort)(12002 + j * 10));
                        series.Batteries.Add(battery);
                    }
                    OnlineBatteryTotalList[index].Series.Add(series);
                }
            }
        }

        public void DisplayRealTimeData()
        {
            IsStartReadData = true;
            for (int i = 0; i < OnlineBatteryTotalList.Count; i++)
            {
                Thread thread = new Thread(ReadBatteryTotalData);
                thread.Start(i);
            }
        }

        public void StopDisplayRealTimeData()
        {
            IsStartReadData = false;
        }

        private bool IsStartReadData = false;
        public bool IsStartSaveData = false;
        private void ReadBatteryTotalData(object index)
        {
            try
            {
                while (true)
                {
                    if (!IsStartReadData)
                    {
                        break;
                    }

                    Thread.Sleep(DaqTimeSpan * 1000);
                    //** 注：应该尽可能的少次多量读取数据，多次读取数据会因为读取次数过于频繁导致丢包
                    // 获取总簇电池信息
                    OnlineBatteryTotalList[(int)index].TotalVoltage = ClientList[(int)index].ReadU16(10001);
                    OnlineBatteryTotalList[(int)index].TotalCurrent = ClientList[(int)index].ReadU16(10002);
                    for (int i = 0; i < OnlineBatteryTotalList[(int)index].Series.Count; i++)
                    {
                        // 获取单串电池信息
                        ushort[] seriesValues = ClientList[(int)index].ReadU16Array((ushort)(11001 + i * 10), 2);
                        OnlineBatteryTotalList[(int)index].Series[i].SeriesVoltage = seriesValues[0];
                        OnlineBatteryTotalList[(int)index].Series[i].SeriesCurrent = seriesValues[1];

                        for (int j = 0; j < OnlineBatteryTotalList[(int)index].Series[i].Batteries.Count; j++)
                        {
                            // 获取单个电池信息
                            ushort[] BatteryValues = ClientList[(int)index].ReadU16Array((ushort)(12001 + j * 10), 3);
                            OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Voltage = BatteryValues[0];
                            OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Current = BatteryValues[1];
                        }
                    }

                    if (IsStartSaveData)
                    {
                        DateTime date = DateTime.Now;
                        TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
                        TotalBatteryInfoModel TotalModel = new TotalBatteryInfoModel();
                        TotalModel.BCMUID = OnlineBatteryTotalList[(int)index].BCMUID;
                        TotalModel.TotalVoltage = OnlineBatteryTotalList[(int)index].TotalVoltage;
                        TotalModel.TotalCurrent = OnlineBatteryTotalList[(int)index].TotalCurrent;
                        TotalModel.HappenTime = date;
                        TotalManage.Insert(TotalModel);

                        SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                        for (int i = 0; i < OnlineBatteryTotalList[(int)index].Series.Count; i++)
                        {
                            SeriesBatteryInfoModel model = new SeriesBatteryInfoModel();
                            model.BCMUID = OnlineBatteryTotalList[(int)index].BCMUID;
                            model.BMUID = OnlineBatteryTotalList[(int)index].Series[i].SeriesId;
                            model.HappenTime = date;
                            typeof(SeriesBatteryInfoModel).GetProperty("SeriesVoltage").SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].SeriesVoltage);
                            typeof(SeriesBatteryInfoModel).GetProperty("SeriesCurrent").SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].SeriesCurrent);
                            for (int j = 0; j < OnlineBatteryTotalList[(int)index].Series[i].Batteries.Count; j++)
                            {
                                typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Voltage);
                                typeof(SeriesBatteryInfoModel).GetProperty("Current" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Current);
                            }
                            SeriesManage.Insert(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
