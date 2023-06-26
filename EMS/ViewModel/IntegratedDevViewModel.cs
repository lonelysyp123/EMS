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
            throw new NotImplementedException();
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
                            AddIPInView(port);
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
                            AddIPInView(ip);
                            //! 配置文件中新增IP
                            //helper.InsertIP(IPConfigFilePath, ip);
                        }
                    }
                }
            }
        }

        private void AddIPInView(string ip)
        {
            BatteryTotalBase dev = new BatteryTotalBase();
            dev.TotalID = ip;
            BatteryTotalList.Add(dev);
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
                    BatteryTotalList.Add(new BatteryTotalBase() { TotalID = view.RTUPort.Text });
                }
                else
                {
                    // add Modbus TCP Dev
                    BatteryTotalList.Add(new BatteryTotalBase() { TotalID = view.IPText.AddressText });
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
