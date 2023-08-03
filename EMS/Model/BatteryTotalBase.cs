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
using System.Windows.Controls;
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

        private double _minVoltage;
        /// <summary>
        /// 单体最低电压
        /// </summary>
        public double MinVoltage
        {

            get => _minVoltage;
            set
            {
                SetProperty(ref _minVoltage, value);
            }
        }

        private int _minVoltageIndex;
        /// <summary>
        /// 单体最低电压编号
        /// </summary>
        public int MinVoltageIndex
        {

            get => _minVoltageIndex;
            set
            {
                SetProperty(ref _minVoltageIndex, value);
            }
        }

        private double _maxVoltage;
        /// <summary>
        /// 单体最高电压
        /// </summary>
        public double MaxVoltage
        {

            get => _maxVoltage;
            set
            {
                SetProperty(ref _maxVoltage, value);
            }
        }

        private int _maxVoltageIndex;
        /// <summary>
        /// 单体最高电压编号
        /// </summary>
        public int MaxVoltageIndex
        {

            get => _maxVoltageIndex;
            set
            {
                SetProperty(ref _maxVoltageIndex, value);
            }
        }

        private double _minTemperature;
        /// <summary>
        /// 单体最低温度
        /// </summary>
        public double MinTemperature
        {

            get => _minTemperature;
            set
            {
                SetProperty(ref _minTemperature, value);
            }
        }

        private int _minTemperatureIndex;
        /// <summary>
        /// 单体最低温度编号
        /// </summary>
        public int MinTemperatureIndex
        {

            get => _minTemperatureIndex;
            set
            {
                SetProperty(ref _minTemperatureIndex, value);
            }
        }

        private double _maxTemperature;
        /// <summary>
        /// 单体最高温度
        /// </summary>
        public double MaxTemperature
        {

            get => _maxTemperature;
            set
            {
                SetProperty(ref _maxTemperature, value);
            }
        }

        private int _maxTemperatureIndex;
        /// <summary>
        /// 单体最高温度编号
        /// </summary>
        public int MaxTemperatureIndex
        {

            get => _maxTemperatureIndex;
            set
            {
                SetProperty(ref _maxTemperatureIndex, value);
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

        private BitmapSource _internetImage;
        /// <summary>
        /// 连接图标
        /// </summary>
        public BitmapSource InternetImage
        {

            get => _internetImage;
            set
            {
                SetProperty(ref _internetImage, value);
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

        public ushort BatteryCount { get; set; }
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

        private bool _isInternet;
        public bool IsInternet
        {
            get { return _isInternet; }
            set
            {
                if (_isInternet != value)
                {
                    _isInternet = value;
                    InternetImageChange(value);
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
            InternetImageChange(false);
            DaqImageChange(false);
            RecordImageChange(false);
        }

        /// <summary>
        /// 标签图标
        /// </summary>
        public void ImageTitle()
        {
            DevImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/BMS.png"));
        }

        public void ConnectImageChange(bool isconnected)
        {
            if (isconnected)
            {
                ConnectImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnConnect.png"));
            }
            else
            {
                ConnectImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffConnect.png"));
            }
        }

        public void InternetImageChange(bool isinternet)
        {
            if (isinternet)
            {
                InternetImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/InNet.png"));
            }
            else
            {
                InternetImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OutNet.png"));
            }
        }

        public void DaqImageChange(bool isdaq)
        {
            if (isdaq)
            {
                DaqDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnDaq.png"));
            }
            else
            {
                DaqDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffDaq.png"));
            }
        }

        public void RecordImageChange(bool isrecord)
        {
            if (isrecord)
            {
                RecordDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnRecord.png"));
            }
            else
            {
                RecordDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffRecord.png"));
            }
        }

        public bool RequestInterNet()
        {
            Console.WriteLine("设备请求入网");
            return true;
        }
    }
}
