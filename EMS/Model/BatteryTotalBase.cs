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
        private double _totalVoltage;
        /// <summary>
        /// 总簇电压    0.1V 
        /// </summary>
        public double TotalVoltage
        {

            get => _totalVoltage;
            set
            {
                SetProperty(ref _totalVoltage, value);
            }
        }

        private double _totalCurrent;
        /// <summary>
        /// 总簇电流    0.1A
        /// </summary>
        public double TotalCurrent
        {

            get => _totalCurrent;
            set
            {
                SetProperty(ref _totalCurrent, value);
            }
        }

        private double _totalSOC;
        /// <summary>
        /// 总簇SOC   0.1%
        /// </summary>
        public double TotalSOC
        {

            get => _totalSOC;
            set
            {
                SetProperty(ref _totalSOC, value);
            }
        }

        private double _totalSOH;
        /// <summary>
        /// 总簇SOH   0.1%
        /// </summary>
        public double TotalSOH
        {

            get => _totalSOH;
            set
            {
                SetProperty(ref _totalSOH, value);
            }
        }

        private double _averageTemperature;
        /// <summary>
        /// 平均温度    0.1℃
        /// </summary>
        public double AverageTemperature
        {

            get => _averageTemperature;
            set
            {
                SetProperty(ref _averageTemperature, value);
            }
        }

        private BitmapSource _imageTitle;
        /// <summary>
        /// 标签图标
        /// </summary>
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
        public ushort BatteriesCountInSeries { get; set; }
        public ObservableCollection<BatterySeriesBase> Series { get; set; }

        //private ModbusClient client;
        public bool IsConnected = false;
        public bool IsRTU;
        public bool IsDaq = false;
        public BatteryTotalBase()
        {
            Series = new ObservableCollection<BatterySeriesBase>();
            ImageTitleChange();
        }

        /// <summary>
        /// 生成电池总簇实例
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口号</param>
        public BatteryTotalBase(string ip, string port)
        {
            Series = new ObservableCollection<BatterySeriesBase>();
            IP = ip;
            Port = port;
            TotalID = ip;
            ImageTitleChange();
        }

        /// <summary>
        /// 标签图标改变
        /// </summary>
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
    }
}
