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

        private BitmapSource _devImage;
        /// <summary>
        /// 标签图标
        /// </summary>
        public BitmapSource DevImage
        {

            get => _devImage;
            set
            {
                SetProperty(ref _devImage, value);
            }
        }

        private BitmapSource _connectImage;
        /// <summary>
        /// 连接图标
        /// </summary>
        public BitmapSource ConnectImage
        {

            get => _connectImage;
            set
            {
                SetProperty(ref _connectImage, value);
            }
        }

        private BitmapSource _daqDataImage;
        /// <summary>
        /// 采集图标
        /// </summary>
        public BitmapSource DaqDataImage
        {

            get => _daqDataImage;
            set
            {
                SetProperty(ref _daqDataImage, value);
            }
        }

        private BitmapSource _recordDataImage;
        /// <summary>
        /// 记录图标
        /// </summary>
        public BitmapSource RecordDataImage
        {

            get => _recordDataImage;
            set
            {
                SetProperty(ref _recordDataImage, value);
            }
        }

        private string _BCMUID;
        public string BCMUID
        {

            get => _BCMUID;
            set
            {
                if(SetProperty(ref _BCMUID, value))
                {
                    _totalID = "BMS(" + value + ")";
                }
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
        public ushort BatteriesCountInSeries { get; set; }
        public ObservableCollection<BatterySeriesBase> Series { get; set; }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    ConnectImageChange(value);
                }
            }
        }

        private bool _isDaqData;
        public bool IsDaqData
        {
            get { return _isDaqData; }
            set
            {
                if (_isDaqData != value)
                {
                    _isDaqData = value;
                    DaqImageChange(value);
                }
            }
        }

        private bool _isRecordData;
        public bool IsRecordData
        {
            get { return _isRecordData; } 
            set
            {
                if (_isRecordData != value)
                {
                    _isRecordData = value;
                    RecordImageChange(value);
                }
            }
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
            ImageTitle();
            ConnectImageChange(false);
            DaqImageChange(false);
            RecordImageChange(false);
        }

        /// <summary>
        /// 标签图标
        /// </summary>
        public void ImageTitle()
        {
            BitmapImage bi;
            DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
            FileInfo[] files = directory.GetFiles("BMS.png");
            bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            bi.EndInit();
            DevImage = bi;
        }

        public void ConnectImageChange(bool isconnected)
        {
            BitmapImage bi;
            DirectoryInfo directory;
            FileInfo[] files;
            if (isconnected)
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OnConnect.png");
                bi = new BitmapImage();
            }
            else
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OffConnect.png");
                bi = new BitmapImage();
            }
            bi.BeginInit();
            bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            bi.EndInit();
            ConnectImage = bi;
        }

        public void DaqImageChange(bool isdaq)
        {
            BitmapImage bi;
            DirectoryInfo directory;
            FileInfo[] files;
            if (isdaq)
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OnDaq.png");
                bi = new BitmapImage();
            }
            else
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OffDaq.png");
                bi = new BitmapImage();
            }
            bi.BeginInit();
            bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            bi.EndInit();
            DaqDataImage = bi;
        }

        public void RecordImageChange(bool isrecord)
        {
            BitmapImage bi;
            DirectoryInfo directory;
            FileInfo[] files;
            if (isrecord)
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OnRecord.png");
                bi = new BitmapImage();
            }
            else
            {
                directory = new DirectoryInfo("./Resource/Image");
                files = directory.GetFiles("OffRecord.png");
                bi = new BitmapImage();
            }
            bi.BeginInit();
            bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            bi.EndInit();
            RecordDataImage = bi;
        }
    }
}
