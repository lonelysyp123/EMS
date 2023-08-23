using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public  class ParameterSettingViewModel: ViewModelBase
    {
        /// <summary>
        /// 单体过压保护阈值
        /// </summary>
        private double _singleOverVolThresh;
        public double SingleOverVolThresh
        {
            get => _singleOverVolThresh;
            set
            {
                SetProperty(ref  _singleOverVolThresh, value);
            }
        }

        /// <summary>
        /// 单体过压保护恢复阈值
        /// </summary>
        private double _singleRecoveryOverVolThresh;
        public double SingleRecoveryOverVolThresh
        {
            get => _singleRecoveryOverVolThresh;
            set
            {
                SetProperty(ref _singleRecoveryOverVolThresh, value);
            }
        }

        /// <summary>
        /// 单体低压保护阈值
        /// </summary>
        private double _singleLowVolThresh;
        public double SingleLowVolThresh
        {
            get => _singleLowVolThresh;
            set
            {
                SetProperty(ref _singleLowVolThresh, value);
            }

        }

        /// <summary>
        /// 单体低压保护恢复阈值
        /// </summary>
        private double _singleRecoveryLowVolThresh;
        public double SingleRecoveryLowVolThresh
        {
            get => _singleRecoveryLowVolThresh; 
            set
            {
                SetProperty(ref _singleRecoveryLowVolThresh, value);
            }
        }

        /// <summary>
        /// 单体高温保护阈值
        /// </summary>
        private double _singleHighTempThresh;
        public double SingleHighTempThresh
        {
            get => _singleHighTempThresh;
            set
            {
                SetProperty(ref _singleHighTempThresh, value);
            }
        }
        
        /// <summary>
        /// 单体高温保护恢复阈值
        /// </summary>
        private double _singleRecoveryHighTempThresh;
        public double SingleRecoveryHighTempThresh
        {
            get => _singleRecoveryHighTempThresh; 
            set
            {
                SetProperty(ref _singleRecoveryHighTempThresh, value);
            }
        }

        /// <summary>
        /// 单体低温保护阈值
        /// </summary>
        private double _singleLowTempThresh;
        public double SingleLowTempThresh
        {
            get => _singleLowTempThresh;
            set
            {
                SetProperty(ref _singleLowTempThresh, value);
            }
        }

        /// <summary>
        /// 单体低温保护恢复阈值
        /// </summary>
        private double _singleRecoveryLowTempThresh;
        public double SingleRecoveryLowTempThresh
        {
            get => _singleRecoveryLowTempThresh;
            set
            {
                SetProperty(ref _singleRecoveryLowTempThresh, value);
            }
        }
        /// <summary>
        /// 簇充电过流保护阈值
        /// </summary>
        private double _clusterCharOverCurrentThresh;
        public double ClusterCharOverCurrentThresh
        {
            get => _clusterCharOverCurrentThresh;
            set
            {
                SetProperty(ref _clusterCharOverCurrentThresh, value);
            }
        }
        /// <summary>
        /// 簇充电过流保护恢复阈值
        /// </summary>
        private double _clusterRecoveryCharOverCurrentThresh;
        public double ClusterRecoveryCharOverCurrentThresh
        {
            get => _clusterRecoveryCharOverCurrentThresh;
            set
            {
                SetProperty(ref _clusterRecoveryCharOverCurrentThresh, value);
            }
        }
        /// <summary>
        /// 簇放电过流保护阈值
        /// </summary>
        private double _clusterDisCharOverCurrentThresh;
        public double ClusterDisCharOverCurrentThresh
        {
            get => _clusterDisCharOverCurrentThresh;
            set
            {
                SetProperty(ref _clusterDisCharOverCurrentThresh, value);
            }
        }
        /// <summary>
        /// 簇放电过流保护恢复阈值
        /// </summary>
        private double _clusterRecoveryDisCharOverCurrentThresh;
        public double ClusterRecoveryDisCharOverCurrentThresh
        {
            get => _clusterRecoveryDisCharOverCurrentThresh;
            set
            {
                SetProperty(ref _clusterRecoveryDisCharOverCurrentThresh, value);
            }
        }


        public RelayCommand ReadClusterThreshInfoCommand { get; set; }
        public RelayCommand SyncClusterThreshInfofCommand { get; set; }
        public RelayCommand ReadSingleVolThreshInfoCommand { get; set; }
        public RelayCommand SyncSingleVolThreshInofCommand { get; set; }
        public RelayCommand ReadSingleTempThreshInfoCommand { get; set; }
        public RelayCommand SyncSingleTempThreshInfoCommand { get; set; }

        private ModbusClient ModbusClient;
        public ParameterSettingViewModel(ModbusClient client) 
        {
            ModbusClient = client;
            ReadClusterThreshInfoCommand = new RelayCommand(ReadClusterThreshInfo);
            SyncClusterThreshInfofCommand = new RelayCommand(SyncClusterThreshInfo);
            ReadSingleVolThreshInfoCommand = new RelayCommand(ReadSingleVolThreshInfo);
            SyncSingleVolThreshInofCommand = new RelayCommand(SyncSingleVolThreshInof);
            ReadSingleTempThreshInfoCommand=new RelayCommand(ReadSingleTempThreshInfo);
            SyncSingleTempThreshInfoCommand=new RelayCommand(SyncSingleTempThreshInfo);
            
        }

        private void SyncSingleTempThreshInfo()
        {
            ModbusClient.WriteFunc(40112, (ushort)(SingleHighTempThresh * 10));
            ModbusClient.WriteFunc(40113, (ushort)(SingleRecoveryHighTempThresh * 10));
            ModbusClient.WriteFunc(40114, (ushort)(SingleLowTempThresh * 10));
            ModbusClient.WriteFunc(40115, (ushort)(SingleRecoveryLowTempThresh * 10));

        }

        private void ReadSingleTempThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40112, 4);
            SingleHighTempThresh = BitConverter.ToUInt16(data, 0) * 0.1;
            SingleRecoveryHighTempThresh = BitConverter.ToUInt16(data, 2) * 0.1;
            SingleLowTempThresh = BitConverter.ToUInt16(data, 4) * 0.1;
            SingleRecoveryLowTempThresh = BitConverter.ToUInt16(data, 6) * 0.1;

        }

        private void SyncSingleVolThreshInof()
        {
            ModbusClient.WriteFunc(40104, (ushort)(SingleOverVolThresh * 1000));
            ModbusClient.WriteFunc(40105, (ushort)(SingleRecoveryOverVolThresh * 1000));
            ModbusClient.WriteFunc(40106, (ushort)(SingleLowVolThresh * 1000));
            ModbusClient.WriteFunc(40107, (ushort)(SingleRecoveryLowVolThresh * 1000));
        }

        private void ReadSingleVolThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40104, 4);
            SingleOverVolThresh = BitConverter.ToUInt16(data, 0) * 0.001;
            SingleRecoveryOverVolThresh =BitConverter.ToUInt16(data, 2)*0.001;
            SingleLowVolThresh =BitConverter.ToUInt16(data,4 )*0.001;
            SingleRecoveryLowVolThresh =BitConverter.ToUInt16(data, 6)*0.001;
        }

        private void SyncClusterThreshInfo()
        {
            ModbusClient.WriteFunc(40108, (ushort)(ClusterCharOverCurrentThresh*10));
            ModbusClient.WriteFunc(40109,(ushort)(ClusterRecoveryCharOverCurrentThresh*10));
            ModbusClient.WriteFunc(40110,(ushort)(ClusterDisCharOverCurrentThresh*10));
            ModbusClient.WriteFunc(40111,(ushort)(ClusterRecoveryDisCharOverCurrentThresh*10));
           
        }

        private void ReadClusterThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40108, 4);
            ClusterCharOverCurrentThresh = BitConverter.ToUInt16(data, 0) * 0.1;
            ClusterRecoveryCharOverCurrentThresh = BitConverter.ToUInt16(data, 2)*0.1;
            ClusterDisCharOverCurrentThresh = BitConverter.ToUInt16(data, 4)*0.1;
            ClusterRecoveryDisCharOverCurrentThresh = BitConverter.ToUInt16(data, 6) * 0.1;
        }
    }
}
