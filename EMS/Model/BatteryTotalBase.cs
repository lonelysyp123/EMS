using CommunityToolkit.Mvvm.ComponentModel;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EMS.Model
{
    /// <summary>
    /// 电池簇
    /// </summary>
    public class BatteryTotalBase : ObservableObject
    {
        private ushort _totalVoltage;
        public ushort TotalVoltage
        {

            get => _totalVoltage;
            set
            {
                SetProperty(ref _totalVoltage, value);
            }
        }

        private ushort _totalCurrent;
        public ushort TotalCurrent
        {

            get => _totalCurrent;
            set
            {
                SetProperty(ref _totalCurrent, value);
            }
        }

        private ushort _averageTemperature;
        public ushort AverageTemperature
        {

            get => _averageTemperature;
            set
            {
                SetProperty(ref _averageTemperature, value);
            }
        }

        private BitmapSource _imageTitle;
        public BitmapSource ImageTitle
        {

            get => _imageTitle;
            set
            {
                SetProperty(ref _imageTitle, value);
            }
        }

        private string _BCMUID;
        public string BCMUID
        {

            get => _BCMUID;
            set
            {
                SetProperty(ref _BCMUID, value);
            }
        }

        private string _totalID;
        public string TotalID
        {

            get => _totalID;
            set
            {
                SetProperty(ref _totalID, value);
            }
        }

        public string IP { set; get; }
        public string Port { set; get; }

        public ushort SeriesCount { get; set; }
        public ushort BatteriesCount { get; set; }
        public ObservableCollection<BatterySeriesBase> Series { get; set; }

        private ModbusClient client;
        private bool IsConnected = false;
        public bool IsRTU;
        public bool IsDaq = false;
        public ConcurrentQueue<List<SeriesBatteryInfoModel>> SeriesBatteryInfoList;
        public ConcurrentQueue<TotalBatteryInfoModel> TotalBatteryInfo;
        public BatteryTotalBase()
        {
            Series = new ObservableCollection<BatterySeriesBase>();
            ImageTitleChange();
        }

        public BatteryTotalBase(string ip, string port)
        {
            Series = new ObservableCollection<BatterySeriesBase>();
            IP = ip;
            Port = port;
            TotalID = ip;
            ImageTitleChange();
        }

        public void ImageTitleChange()
        {
            BitmapImage bi;
            if (IsConnected)
            {
                DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                FileInfo[] files = directory.GetFiles("Online.png");
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                bi.EndInit();
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                FileInfo[] files = directory.GetFiles("Offline.png");
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                bi.EndInit();
            }
            ImageTitle = bi;
        }

        public void Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    if (IsRTU)
                    {
                        //client = new ModbusClient(ConnectParam[0], int.Parse(ConnectParam[1]), int.Parse(ConnectParam[2]), int.Parse(ConnectParam[3]), int.Parse(ConnectParam[4]));
                        //client.Connect();
                        //IsConnected = true;
                    }
                    else
                    {
                        client = new ModbusClient(IP, int.Parse(Port));
                        client.Connect();
                        IsConnected = true;
                    }
                    ImageTitleChange();
                    InitBatteryTotal();
                    StartListener();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitBatteryTotal()
        {
            if (IsConnected)
            {
                // 信息补全
                BCMUID = client.ReadU16(10000).ToString();
                TotalVoltage = client.ReadU16(10001);
                TotalCurrent = client.ReadU16(10002);
                SeriesCount = client.ReadU16(10100);
                Series.Clear();
                for (int i = 0; i < SeriesCount; i++)
                {
                    BatterySeriesBase series = new BatterySeriesBase();
                    series.SeriesId = client.ReadU16((ushort)(11000 + i * 10)).ToString();
                    series.SeriesVoltage = client.ReadU16((ushort)(11001 + i * 10));
                    series.SeriesCurrent = client.ReadU16((ushort)(11002 + i * 10));
                    series.BatteryCount = client.ReadU16((ushort)(10200 + i * 10));
                    for (int j = 0; j < series.BatteryCount; j++)
                    {
                        BatteryBase battery = new BatteryBase();
                        battery.BatteryID = client.ReadU16((ushort)(12000+ j * 10)).ToString();
                        battery.Voltage = client.ReadU16((ushort)(12001 + j * 10));
                        battery.Current = client.ReadU16((ushort)(12002 + j * 10));
                        series.Batteries.Add(battery);
                    }
                    Series.Add(series);
                }
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                cts.Cancel();
                Thread.Sleep(500);
                client.Disconnect();
                IsConnected = false;
                ImageTitleChange();
            }
        }

        CancellationTokenSource cts;
        public void StartListener()
        {
            cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(t => DataListener(cts.Token));
        }

        private void DataListener(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    //** 注：应该尽可能的少次多量读取数据，多次读取数据会因为读取次数过于频繁导致丢包
                    // 获取总簇电池信息
                    TotalVoltage = client.ReadU16(10001);
                    TotalCurrent = client.ReadU16(10002);
                    for (int i = 0; i < Series.Count; i++)
                    {
                        // 获取单串电池信息
                        ushort[] seriesValues = client.ReadU16Array((ushort)(11001 + i * 10), 2);
                        Series[i].SeriesVoltage = seriesValues[0];
                        Series[i].SeriesCurrent = seriesValues[1];

                        for (int j = 0; j < Series[i].Batteries.Count; j++)
                        {
                            // 获取单个电池信息
                            ushort[] BatteryValues = client.ReadU16Array((ushort)(12001 + j * 10),2);
                            Series[i].Batteries[j].Voltage = BatteryValues[0];
                            Series[i].Batteries[j].Current = BatteryValues[1];
                        }
                    }

                    if (IsDaq)
                    {
                        SeriesBatteryInfoManage manage = new SeriesBatteryInfoManage();
                        for (int i = 0; i < Series.Count; i++)
                        {
                            SeriesBatteryInfoModel model = new SeriesBatteryInfoModel();
                            model.BCMUID = BCMUID;
                            model.BMUID = Series[i].SeriesId;
                            model.SerialNuw = i + 1;
                            model.HappenTime = DateTime.Now;
                            for (int j = 0; j < Series[i].Batteries.Count; j++)
                            {
                                typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(model, Series[i].Batteries[j].Voltage);
                            }
                            manage.Insert(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void StartRecordData()
        {
            SeriesBatteryInfoList = new ConcurrentQueue<List<SeriesBatteryInfoModel>>();
            TotalBatteryInfo = new ConcurrentQueue<TotalBatteryInfoModel>();
            IsDaq = true;
        }

        public void StopRecordData()
        {
            IsDaq = true;
            Thread.Sleep(100);
            SeriesBatteryInfoList = null;
            TotalBatteryInfo = null;
        }
    }
}
