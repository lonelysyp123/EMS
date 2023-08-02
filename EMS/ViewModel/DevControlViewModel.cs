using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.Model
{
    public class DevControlViewModel : ViewModelBase
    {
        private int _year;
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                SetProperty(ref _year, value);
            }
        }

        private int _month;
        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                SetProperty(ref _month, value);
            }
        }

        private int _day;
        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                SetProperty(ref _day, value);
            }
        }

        private int _hour;
        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                SetProperty(ref _hour, value);
            }
        }

        private int _minute;
        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                SetProperty(ref _minute, value);
            }
        }

        private int _second;
        public int Second
        {
            get
            {
                return _second;
            }
            set
            {
                SetProperty(ref _second, value);
            }
        }

        private string _address1;
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                SetProperty(ref _address1, value);
            }
        }

        private string _address2;
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                SetProperty(ref _address2, value);
            }
        }

        private string _mask1;
        public string Mask1
        {
            get
            {
                return _mask1;
            }
            set
            {
                SetProperty(ref _mask1, value);
            }
        }

        private string _mask2;
        public string Mask2
        {
            get
            {
                return _mask2;
            }
            set
            {
                SetProperty(ref _mask2, value);
            }
        }


        private string _gateway1;
        public string Gateway1
        {
            get
            {
                return _gateway1;
            }
            set
            {
                SetProperty(ref _gateway1, value);
            }
        }

        private string _gateway2;
        public string Gateway2
        {
            get
            {
                return _gateway2;
            }
            set
            {
                SetProperty(ref _gateway2, value);
            }
        }

        private List<string> _channels;
        public List<string> Channels
        {
            get
            {
                return _channels;
            }
            set
            {
                SetProperty(ref _channels, value);
            }
        }

        private string _selectedOpenChannel;
        public string SelectedOpenChannel
        {
            get
            {
                return _selectedOpenChannel;
            }
            set
            {
                SetProperty(ref _selectedOpenChannel, value);
            }
        }

        private string _selectedCloseChannel;
        public string SelectedCloseChannel
        {
            get
            {
                return _selectedCloseChannel;
            }
            set
            {
                SetProperty(ref _selectedCloseChannel, value);
            }
        }

        private string _balancedMode;
        public string BalancedMode
        {
            get
            {
                return _balancedMode;
            }
            set
            {
                SetProperty(ref _balancedMode, value);
            }
        }

        public RelayCommand ReadTimeInfoCommand { get; set; }
        public RelayCommand SyncTimeInfoCommand { get; set; }
        public RelayCommand ReadNetInfoCommand { get; set; }
        public RelayCommand SyncNetInfoCommand { get; set; }
        public RelayCommand OpenChargeChannelCommand { get; set; }
        public RelayCommand CloseChargeChannelCommand { get; set; }
        public RelayCommand SelectBalancedModeCommand { get; set; }
        public RelayCommand InNetCommand { get; set; }

        private ModbusClient ModbusClient;
        public DevControlViewModel(ModbusClient client) 
        {
            ReadTimeInfoCommand = new RelayCommand(ReadTimeInfo);
            SyncTimeInfoCommand = new RelayCommand(SyncTimeInfo);
            ReadNetInfoCommand = new RelayCommand(ReadNetInfo);
            SyncNetInfoCommand = new RelayCommand(SyncNetInfo);
            OpenChargeChannelCommand = new RelayCommand(OpenChargeChannel);
            CloseChargeChannelCommand = new RelayCommand(CloseChargeChannel);
            SelectBalancedModeCommand = new RelayCommand(SelectBalancedMode);
            InNetCommand = new RelayCommand(InNet);

            ModbusClient = client;
        }

        private void InNet()
        {
            ModbusClient.WriteFunc(40019, 0xBB11);
        }

        private void SelectBalancedMode()
        {
            if (BalancedMode == "自动模式")
            {
                ModbusClient.WriteFunc(40018, 0xAA11);
            }
            else
            {
                ModbusClient.WriteFunc(40018, 0xAA22);
            }
        }

        private void CloseChargeChannel()
        {
            if (int.TryParse(SelectedCloseChannel, out int index))
            {
                ushort value = (ushort)(index + 0xAA00);
                ModbusClient.WriteFunc(40017, value);
            }
        }

        private void OpenChargeChannel()
        {
            if (int.TryParse(SelectedOpenChannel, out int index))
            {
                ushort value = (ushort)(index + 0xAA00);
                ModbusClient.WriteFunc(40016, value);
            }
        }

        private void SyncNetInfo()
        {
            if (int.TryParse(Address1, out int value1))
            {
                ModbusClient.WriteFunc(40100, (ushort)value1);
            }
            if (int.TryParse(Address2, out int value2))
            {
                ModbusClient.WriteFunc(40101, (ushort)value2);
            }
            if (int.TryParse(Mask1, out int value3))
            {
                ModbusClient.WriteFunc(40102, (ushort)value3);
            }
            if (int.TryParse(Mask2, out int value4))
            {
                ModbusClient.WriteFunc(40103, (ushort)value4);
            }
            if (int.TryParse(Gateway1, out int value5))
            {
                ModbusClient.WriteFunc(40104, (ushort)value5);
            }
            if (int.TryParse(Gateway2, out int value6))
            {
                ModbusClient.WriteFunc(40105, (ushort)value6);
            }
        }

        private void ReadNetInfo()
        {
            var data = ModbusClient.ReadFunc(40100, 6);
            Address1 = BitConverter.ToUInt16(data, 0).ToString();
            Address2 = BitConverter.ToUInt16(data, 1).ToString();
            Mask1 = BitConverter.ToUInt16(data, 2).ToString();
            Mask2 = BitConverter.ToUInt16(data, 3).ToString();
            Gateway1 = BitConverter.ToUInt16(data, 4).ToString();
            Gateway2 = BitConverter.ToUInt16(data, 5).ToString();
        }

        private void SyncTimeInfo()
        {
            ModbusClient.WriteFunc(40000, (ushort)Year);
            ModbusClient.WriteFunc(40001, (ushort)Month);
            ModbusClient.WriteFunc(40002, (ushort)Day);
            ModbusClient.WriteFunc(40003, (ushort)Hour);
            ModbusClient.WriteFunc(40004, (ushort)Minute);
            ModbusClient.WriteFunc(40005, (ushort)Second);
        }

        private void ReadTimeInfo()
        {
            var data = ModbusClient.ReadFunc(40000, 6);
            Year = BitConverter.ToUInt16(data, 0);
            Month = BitConverter.ToUInt16(data, 2);
            Day = BitConverter.ToUInt16(data, 4);
            Hour = BitConverter.ToUInt16(data, 6);
            Minute = BitConverter.ToUInt16(data, 8);
            Second = BitConverter.ToUInt16(data, 10);
        }
    }
}
