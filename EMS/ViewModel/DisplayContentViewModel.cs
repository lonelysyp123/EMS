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
using System.Net;
using System.Windows.Threading;

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

        //public ConcurrentQueue<TotalBatteryInfoModel> TotalBatteryInfoQueue;
        private List<ModbusClient> ClientList;
        public int DaqTimeSpan = 0;
        public IntegratedDevViewModel IntegratedDev;
        public bool IsStartSaveData = false;
        public DisplayContentViewModel()
        {
            IntegratedDev = new IntegratedDevViewModel();
            OnlineBatteryTotalList = new ObservableCollection<BatteryTotalBase>();
            //TreeName = "EMS";
            ClientList = new List<ModbusClient>();
        }

        public bool IsStartDaqData = false;
        public bool AddConnectedDev(BatteryTotalBase battery)
        {
            try
            {
                if (!OnlineBatteryTotalList.Contains(battery))
                {
                    if (int.TryParse(battery.Port, out int port))
                    {
                        ModbusClient client = new ModbusClient(battery.IP, port);
                        Connect(battery, client);
                        OnlineBatteryTotalList.Add(battery);
                        ClientList.Add(client);
                        if (IsStartDaqData)
                        {
                            OnlineBatteryTotalList[OnlineBatteryTotalList.Count-1].IsDaqData = true;
                            Thread thread = new Thread(ReadBatteryTotalData);
                            thread.IsBackground = true;
                            thread.Start(OnlineBatteryTotalList.Count - 1);
                        }
                        return true;
                    }
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public int RemoveDisConnectedDev(BatteryTotalBase item)
        {
            try
            {
                if (OnlineBatteryTotalList.Count > 0)
                {
                    int index = OnlineBatteryTotalList.IndexOf(item);
                    Disconnect(index);
                    OnlineBatteryTotalList[index].IsDaqData = false;
                    OnlineBatteryTotalList.RemoveAt(index);
                    ClientList.RemoveAt(index);
                    return index;
                }
                return -1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        private void Connect(BatteryTotalBase obj, ModbusClient client)
        {
            try
            {
                if (!obj.IsConnected)
                {
                    client.Connect();
                    obj.IsConnected = true;
                    InitBatteryTotal(obj, client);
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
                        series.Batteries.Add(battery);
                    }
                    OnlineBatteryTotalList[index].Series.Add(series);
                }
            }
        }

        public void InitBatteryTotal(BatteryTotalBase obj, ModbusClient client)
        {
            if (obj.IsConnected)
            {
                // 信息补全
                obj.BCMUID = client.ReadU16(10000).ToString();
                obj.TotalVoltage = client.ReadU16(10001) * 0.001;
                obj.TotalCurrent = client.ReadU16(10002) * 0.1;
                obj.SeriesCount = client.ReadU16(10100);
                obj.Series.Clear();
                for (int i = 0; i < obj.SeriesCount; i++)
                {
                    BatterySeriesBase series = new BatterySeriesBase();
                    series.SeriesId = client.ReadU16((ushort)(11000 + i * 10)).ToString();
                    series.SeriesVoltage = client.ReadU16((ushort)(11001 + i * 10)) * 0.001;
                    series.SeriesCurrent = client.ReadU16((ushort)(11002 + i * 10)) * 0.1;
                    series.BatteryCount = client.ReadU16((ushort)(10200 + i * 10));
                    for (int j = 0; j < series.BatteryCount; j++)
                    {
                        BatteryBase battery = new BatteryBase();
                        battery.BatteryID = client.ReadU16((ushort)(12000 + j * 10)).ToString();
                        battery.Voltage = client.ReadU16((ushort)(12001 + j * 10)) * 0.001;
                        battery.Current = client.ReadU16((ushort)(12002 + j * 10)) * 0.1;
                        series.Batteries.Add(battery);
                    }
                    obj.Series.Add(series);
                }
            }
        }

        /// <summary>
        /// 初始化电池总簇信息
        /// </summary>
        public void InitBatteryTotalNew(int index)
        {
            if (OnlineBatteryTotalList[index].IsConnected)
            {
                //** 注：应该尽可能的少次多量读取数据，多次读取数据会因为读取次数过于频繁导致丢包
                var BCMUData = ClientList[(int)index].ReadFunc(11000, 100);
                var BMUData = ClientList[(int)index].ReadFunc(1000, 300);

                // 信息补全
                OnlineBatteryTotalList[index].BCMUID = BitConverter.ToString(BCMUData, 0, 0);
                OnlineBatteryTotalList[index].TotalVoltage = BitConverter.ToUInt16(BCMUData, 0) * 0.001;
                OnlineBatteryTotalList[index].TotalCurrent = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                OnlineBatteryTotalList[index].TotalSOC = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                OnlineBatteryTotalList[index].TotalSOH = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                OnlineBatteryTotalList[index].AverageTemperature = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                OnlineBatteryTotalList[index].SeriesCount = BitConverter.ToUInt16(BCMUData, 0);
                OnlineBatteryTotalList[index].BatteriesCountInSeries = BitConverter.ToUInt16(BCMUData, 0);
                OnlineBatteryTotalList[index].Series.Clear();
                for (int i = 0; i < OnlineBatteryTotalList[index].SeriesCount; i++)
                {
                    BatterySeriesBase series = new BatterySeriesBase();
                    series.SeriesId = BitConverter.ToString(BMUData, 0, 0);
                    series.SeriesVoltage = BitConverter.ToUInt16(BMUData, 0) * 0.001;
                    series.SeriesCurrent = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                    series.AlarmState = BitConverter.ToUInt16(BMUData, 0);
                    series.FaultState = BitConverter.ToUInt16(BMUData, 0);
                    series.ChargeChannelState = BitConverter.ToUInt16(BMUData, 0);
                    series.ChargeCapacitySum = BitConverter.ToUInt16(BMUData, 0);
                    for (int j = 0; j < OnlineBatteryTotalList[index].BatteriesCountInSeries; j++)
                    {
                        BatteryBase battery = new BatteryBase();
                        battery.BatteryID = BitConverter.ToString(BMUData, 0, 0);
                        battery.Voltage = BitConverter.ToUInt16(BMUData, 0) * 0.001;
                        battery.Current = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                        battery.Temperature1 = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                        battery.Temperature2 = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                        battery.SOC = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                        battery.Capacity = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                        battery.Resistance = BitConverter.ToUInt16(BMUData, 0);
                        series.Batteries.Add(battery);
                    }
                    OnlineBatteryTotalList[index].Series.Add(series);
                }
            }
        }

        public void DisplayRealTimeData()
        {
            IsStartDaqData = true;
            for (int i = 0; i < OnlineBatteryTotalList.Count; i++)
            {
                OnlineBatteryTotalList[i].IsDaqData = true;
                Thread thread = new Thread(ReadBatteryTotalData);
                thread.IsBackground = true;
                thread.Start(i);
            }
        }

        public void StopDisplayRealTimeData()
        {
            IsStartDaqData = false;
            for (int i = 0; i < OnlineBatteryTotalList.Count; i++)
            {
                OnlineBatteryTotalList[i].IsDaqData = false;
            }
        }

        private void ReadBatteryTotalData(object index)
        {
            var item = OnlineBatteryTotalList[(int)index];
            var client = ClientList[(int)index];
            while (true)
            {
                try
                {
                    if (!item.IsDaqData)
                    {
                        break;
                    }

                    Thread.Sleep(DaqTimeSpan * 1000);
                    if (item.IsConnected)
                    {
                        //** 注：应该尽可能的少次多量读取数据，多次读取数据会因为读取次数过于频繁导致丢包
                        // 获取总簇电池信息
                        item.TotalVoltage = client.ReadU16(10001) * 0.001;
                        item.TotalCurrent = client.ReadU16(10002) * 0.1;
                        for (int i = 0; i < item.Series.Count; i++)
                        {
                            // 获取单串电池信息
                            ushort[] seriesValues = client.ReadU16Array((ushort)(11001 + i * 10), 2);
                            item.Series[i].SeriesVoltage = seriesValues[0] * 0.001;
                            item.Series[i].SeriesCurrent = seriesValues[1] * 0.1;

                            for (int j = 0; j < item.Series[i].Batteries.Count; j++)
                            {
                                // 获取单个电池信息
                                ushort[] BatteryValues = client.ReadU16Array((ushort)(12001 + j * 10), 3);
                                item.Series[i].Batteries[j].Voltage = BatteryValues[0] * 0.001;
                                item.Series[i].Batteries[j].Current = BatteryValues[1] * 0.1;
                            }
                        }

                        if (item.IsRecordData)
                        {
                            DateTime date = DateTime.Now;
                            TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
                            TotalBatteryInfoModel TotalModel = new TotalBatteryInfoModel();
                            TotalModel.BCMUID = item.BCMUID;
                            TotalModel.Voltage = item.TotalVoltage;
                            TotalModel.Current = item.TotalCurrent;
                            TotalModel.HappenTime = date;
                            TotalManage.Insert(TotalModel);

                            SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                            for (int i = 0; i < item.Series.Count; i++)
                            {
                                SeriesBatteryInfoModel model = new SeriesBatteryInfoModel();
                                model.BCMUID = item.BCMUID;
                                model.BMUID = item.Series[i].SeriesId;
                                model.HappenTime = date;
                                typeof(SeriesBatteryInfoModel).GetProperty("SeriesVoltage").SetValue(model, item.Series[i].SeriesVoltage);
                                typeof(SeriesBatteryInfoModel).GetProperty("SeriesCurrent").SetValue(model, item.Series[i].SeriesCurrent);
                                for (int j = 0; j < item.Series[i].Batteries.Count; j++)
                                {
                                    typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(model, item.Series[i].Batteries[j].Voltage);
                                    typeof(SeriesBatteryInfoModel).GetProperty("Current" + j).SetValue(model, item.Series[i].Batteries[j].Current);
                                }
                                SeriesManage.Insert(model);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        item.IsConnected = false;
                        item.IsDaqData = false;
                        item.IsRecordData = false;
                    });
                    break;
                }
            }
        }

        private void ReadBatteryTotalDataNew(object index)
        {
            while (true)
            {
                try
                {
                    if (!OnlineBatteryTotalList[(int)index].IsDaqData)
                    {
                        break;
                    }

                    Thread.Sleep(DaqTimeSpan * 1000);
                    if (OnlineBatteryTotalList[(int)index].IsConnected)
                    {
                        //** 注：应该尽可能的少次多量读取数据，多次读取数据会因为读取次数过于频繁导致丢包
                        var BCMUData = ClientList[(int)index].ReadFunc(11000, 100);
                        var BMUData = ClientList[(int)index].ReadFunc(1000, 300);

                        // 获取总簇电池信息
                        OnlineBatteryTotalList[(int)index].TotalVoltage = BitConverter.ToUInt16(BCMUData, 0) * 0.001;
                        OnlineBatteryTotalList[(int)index].TotalCurrent = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                        OnlineBatteryTotalList[(int)index].TotalSOC = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                        OnlineBatteryTotalList[(int)index].TotalSOH = BitConverter.ToUInt16(BCMUData, 0) * 0.1;
                        OnlineBatteryTotalList[(int)index].AverageTemperature = BitConverter.ToUInt16(BCMUData, 0) * 0.1;

                        for (int i = 0; i < OnlineBatteryTotalList[(int)index].SeriesCount; i++)
                        {
                            // 获取单串电池信息
                            OnlineBatteryTotalList[(int)index].Series[i].SeriesVoltage = BitConverter.ToUInt16(BMUData, 0) * 0.001;
                            OnlineBatteryTotalList[(int)index].Series[i].SeriesCurrent = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                            OnlineBatteryTotalList[(int)index].Series[i].AlarmState = BitConverter.ToUInt16(BMUData, 0);
                            OnlineBatteryTotalList[(int)index].Series[i].FaultState = BitConverter.ToUInt16(BMUData, 0);
                            OnlineBatteryTotalList[(int)index].Series[i].ChargeChannelState = BitConverter.ToUInt16(BMUData, 0);
                            OnlineBatteryTotalList[(int)index].Series[i].ChargeCapacitySum = BitConverter.ToUInt16(BMUData, 0);

                            for (int j = 0; j < OnlineBatteryTotalList[(int)index].BatteriesCountInSeries; j++)
                            {
                                // 获取单个电池信息
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Voltage = BitConverter.ToUInt16(BMUData, 0) * 0.001;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Current = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Temperature1 = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Temperature2 = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].SOC = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Capacity = BitConverter.ToUInt16(BMUData, 0) * 0.1;
                                OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Resistance = BitConverter.ToUInt16(BMUData, 0);
                            }
                        }

                        if (OnlineBatteryTotalList[(int)index].IsRecordData)
                        {
                            DateTime date = DateTime.Now;
                            TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
                            TotalBatteryInfoModel TotalModel = new TotalBatteryInfoModel();
                            TotalModel.BCMUID = OnlineBatteryTotalList[(int)index].BCMUID;
                            TotalModel.Voltage = OnlineBatteryTotalList[(int)index].TotalVoltage;
                            TotalModel.Current = OnlineBatteryTotalList[(int)index].TotalCurrent;
                            TotalModel.SOH = OnlineBatteryTotalList[(int)index].TotalSOH;
                            TotalModel.SOC = OnlineBatteryTotalList[(int)index].TotalSOC;
                            TotalModel.AverageTemperature = OnlineBatteryTotalList[(int)index].AverageTemperature;
                            TotalModel.HappenTime = date;
                            TotalManage.Insert(TotalModel);

                            SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                            for (int i = 0; i < OnlineBatteryTotalList[(int)index].Series.Count; i++)
                            {
                                SeriesBatteryInfoModel model = new SeriesBatteryInfoModel();
                                model.BCMUID = OnlineBatteryTotalList[(int)index].BCMUID;
                                model.BMUID = OnlineBatteryTotalList[(int)index].Series[i].SeriesId;
                                model.SeriesVoltage = OnlineBatteryTotalList[(int)index].Series[i].SeriesVoltage;
                                model.SeriesCurrent = OnlineBatteryTotalList[(int)index].Series[i].SeriesCurrent;
                                model.HappenTime = date;
                                for (int j = 0; j < OnlineBatteryTotalList[(int)index].Series[i].Batteries.Count; j++)
                                {
                                    typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Voltage);
                                    typeof(SeriesBatteryInfoModel).GetProperty("Current" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Current);
                                    typeof(SeriesBatteryInfoModel).GetProperty("SOC" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].SOC);
                                    typeof(SeriesBatteryInfoModel).GetProperty("Resistance" + j).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Resistance);
                                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2)).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Temperature1);
                                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2 + 1)).SetValue(model, OnlineBatteryTotalList[(int)index].Series[i].Batteries[j].Temperature2);
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

        public bool Bytes_Str(byte[] values, int startindex, int num, out string value)
        {
            value = "";
            if (startindex >= 0 && num > 0)
            {
                if (startindex + num <= values.Length)
                {
                    value = BitConverter.ToString(values, startindex, num);
                    return true;
                }
            }
            return false;
        }
    }
}
