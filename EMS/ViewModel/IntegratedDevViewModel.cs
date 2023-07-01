using CommunityToolkit.Mvvm.Input;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
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

            // 初始化设备列表
            BatteryTotalList = new ObservableCollection<BatteryTotalBase>();
            DevConnectInfoManage manage = new DevConnectInfoManage();
            var entites = manage.Get();
            if (entites != null)
            {
                foreach (var entity in entites)
                {
                    BatteryTotalList.Add(new BatteryTotalBase(entity.IP, entity.Port) { BCMUID = entity.BCMUID });
                }
            }
        }

        private void DelAllDev()
        {
            BatteryTotalList.Clear();
            DevConnectInfoManage manage = new DevConnectInfoManage();
            manage.DeleteAll();
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
                        var objs = BatteryTotalList.Where(dev => dev.BCMUID == port).ToList();
                        if (objs.Count == 0)
                        {
                            //! 界面上新增IP
                            BatteryTotalBase dev = new BatteryTotalBase();
                            dev.BCMUID = port;
                            dev.IsRTU = true;
                            BatteryTotalList.Add(dev);
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
                            dev.BCMUID = "Test";
                            dev.IP = ip;
                            dev.Port = view.TCPPort.Text;
                            dev.TotalID = ip;
                            dev.IsRTU = false;
                            BatteryTotalList.Add(dev);
                            //! 配置文件中新增IP
                            DevConnectInfoModel entity = new DevConnectInfoModel() { BCMUID = "Test", IP = ip, Port = view.TCPPort.Text };
                            DevConnectInfoManage manage = new DevConnectInfoManage();
                            manage.Insert(entity);
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
                    BatteryTotalList.Add(dev);
                }
                else
                {
                    //! 判断该IP是否存在
                    var objs = BatteryTotalList.Where(dev => dev.TotalID == view.IPText.AddressText).ToList();
                    if (objs.Count == 0)
                    {
                        // add Modbus TCP Dev
                        BatteryTotalBase dev = new BatteryTotalBase();
                        dev.BCMUID = "Test";
                        dev.TotalID = view.IPText.AddressText;
                        dev.IsRTU = false;
                        dev.IP = view.IPText.AddressText;
                        dev.Port = view.TCPPort.Text;
                        BatteryTotalList.Add(dev);

                        //! 配置文件中新增IP
                        DevConnectInfoModel entity = new DevConnectInfoModel() { BCMUID = "Test", IP = view.IPText.AddressText, Port = view.TCPPort.Text };
                        DevConnectInfoManage manage = new DevConnectInfoManage();
                        manage.Insert(entity);
                    }
                }
            }
        }
    }
}
