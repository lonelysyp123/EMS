using CommunityToolkit.Mvvm.Input;
using EMS.View;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.Model
{
    public class IntegratedDevViewModel : ViewModelBase
    {
        private ObservableCollection<BatteryTotalBase> _batteryTotalList;
        public ObservableCollection<BatteryTotalBase> BatteryTotalList
        {
            get => _batteryTotalList;
            set
            {
                SetProperty(ref _batteryTotalList, value);
            }
        }

        private string _communicationProtocol;
        public string CommunicationProtocol
        {

            get => _communicationProtocol;
            set
            {
                SetProperty(ref _communicationProtocol, value);
            }
        }

        public RelayCommand AddDevCommand { get; set; }
        public RelayCommand AddDevArrayCommand { get; set; }
        public RelayCommand DelAllDevCommand { get; set; }

        public IntegratedDevViewModel()
        {
            AddDevCommand = new RelayCommand(AddDev);
            AddDevArrayCommand = new RelayCommand(AddDevArray);
            DelAllDevCommand = new RelayCommand(DelAllDev);
            //AddTest();

            BatteryTotalList = new ObservableCollection<BatteryTotalBase>();
        }

        private void DelAllDev()
        {
            BatteryTotalList.Clear();
        }

        private void AddDevArray()
        {
            AddDevArrayView view = new AddDevArrayView();
            if (view.ShowDialog() == true)
            {
                // 新增设备
                if (view.IsRtu)
                {
                    // add Modbus RTU Dev Array
                    for (int i = view.beforeN; i <= view.afterN; i++)
                    {
                        string port = view.segment + i.ToString();
                        //! 判断该IP是否存在
                        var objs = BatteryTotalList.Where(dev => dev.TotalID == port).ToList();
                        if (objs.Count == 0)
                        {
                            //! 界面上新增IP
                            BatteryTotalBase dev = new BatteryTotalBase();
                            dev.TotalID = port;
                            dev.IsRTU = true;
                            dev.ConnectParam.Add(port);
                            dev.ConnectParam.Add(view.Rate.Text);
                            dev.ConnectParam.Add(view.DataBits.Text);
                            dev.ConnectParam.Add(view.Parity.Text);
                            dev.ConnectParam.Add(view.StopBits.Text);
                            BatteryTotalList.Add(dev);
                            //! 配置文件中新增IP
                            //helper.InsertIP(IPConfigFilePath, ip);
                        }
                    }
                }
                else
                {
                    // add Modbus TCP Dev Array
                    for (int i = view.beforeN; i <= view.afterN; i++)
                    {
                        string ip = view.segment + i.ToString();
                        //! 判断该IP是否存在
                        var objs = BatteryTotalList.Where(dev => dev.TotalID == ip).ToList();
                        if (objs.Count == 0)
                        {
                            //! 界面上新增IP
                            BatteryTotalBase dev = new BatteryTotalBase();
                            dev.TotalID = ip;
                            dev.IsRTU = false;
                            dev.ConnectParam.Add(ip);
                            dev.ConnectParam.Add(view.TCPPort.Text);
                            BatteryTotalList.Add(dev);
                            //! 配置文件中新增IP
                            //helper.InsertIP(IPConfigFilePath, ip);
                        }
                    }
                }
            }
        }

        private void AddDev()
        {
            AddDevView view = new AddDevView();
            if (view.ShowDialog() == true)
            {
                // 新增设备
                if (view.IsRtu)
                {
                    // add Modbus RTU Dev
                    BatteryTotalBase dev = new BatteryTotalBase();
                    dev.TotalID = view.RTUPort.Text;
                    dev.IsRTU = true;
                    dev.ConnectParam.Add(view.RTUPort.Text);
                    dev.ConnectParam.Add(view.Rate.Text);
                    dev.ConnectParam.Add(view.DataBits.Text);
                    dev.ConnectParam.Add(view.Parity.Text);
                    dev.ConnectParam.Add(view.StopBits.Text);
                    BatteryTotalList.Add(dev);
                }
                else
                {
                    // add Modbus TCP Dev
                    BatteryTotalBase dev = new BatteryTotalBase();
                    dev.TotalID = view.IPText.AddressText;
                    dev.IsRTU = false;
                    dev.ConnectParam.Add(view.IPText.AddressText);
                    dev.ConnectParam.Add(view.TCPPort.Text);
                    BatteryTotalList.Add(dev);
                }
            }
        }

        private void AddTest()
        {
            BatterySeriesBase series1 = new BatterySeriesBase() { SeriesId = "A", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "A-1" }, new BatteryBase() { BatteryID = "A-2" } } };
            BatterySeriesBase series2 = new BatterySeriesBase() { SeriesId = "B", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "B-1" }, new BatteryBase() { BatteryID = "B-2" } } };
            BatterySeriesBase series3 = new BatterySeriesBase() { SeriesId = "C", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "C-1" }, new BatteryBase() { BatteryID = "C-2" } } };
            BatteryTotalList = new ObservableCollection<BatteryTotalBase>() {
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

        private void AddBatteryTotal(string Address, int Port)
        {
            BatteryTotalBase total = new BatteryTotalBase();



            BatterySeriesBase series1 = new BatterySeriesBase() { SeriesId = "A", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "A-1" }, new BatteryBase() { BatteryID = "A-2" } } };
            BatterySeriesBase series2 = new BatterySeriesBase() { SeriesId = "B", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "B-1" }, new BatteryBase() { BatteryID = "B-2" } } };
            BatterySeriesBase series3 = new BatterySeriesBase() { SeriesId = "C", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "C-1" }, new BatteryBase() { BatteryID = "C-2" } } };
            BatteryTotalList = new ObservableCollection<BatteryTotalBase>() {
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

        private void AddBatteryTotal(string Port, int Rate, int Parity, int Databits, int Stopbits)
        {
            BatterySeriesBase series1 = new BatterySeriesBase() { SeriesId = "A", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "A-1" }, new BatteryBase() { BatteryID = "A-2" } } };
            BatterySeriesBase series2 = new BatterySeriesBase() { SeriesId = "B", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "B-1" }, new BatteryBase() { BatteryID = "B-2" } } };
            BatterySeriesBase series3 = new BatterySeriesBase() { SeriesId = "C", Batteries = new ObservableCollection<BatteryBase>() { new BatteryBase() { BatteryID = "C-1" }, new BatteryBase() { BatteryID = "C-2" } } };
            BatteryTotalList = new ObservableCollection<BatteryTotalBase>() {
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
