using EMS.Common.Modbus.ModbusTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EMS.Model
{
    public class BCMU
    {
        /// <summary>
        /// 电池簇总电压
        /// </summary>
        public ushort TotalVoltage { get; set; }

        /// <summary>
        /// 电池簇总电流
        /// </summary>
        public ushort TotalCurrent { get; set; }

        /// <summary>
        /// 电池簇总SOC
        /// </summary>
        public ushort TotalSOC { get; set; }

        /// <summary>
        /// 电池簇总SOH
        /// </summary>
        public ushort TotalSOH { get; set; }

        /// <summary>
        /// 电池簇平均温度
        /// </summary>
        public short TotalAverageTemp { get; set; }

        /// <summary>
        /// 电池簇单体最低电压
        /// </summary>
        public ushort TotalMinVoltage { get; set; }

        /// <summary>
        /// 电池簇单体最高电压
        /// </summary>
        public ushort TotalMaxVoltage { get; set; }

        /// <summary>
        /// 电池簇单体最低电压编号
        /// </summary>
        public ushort TotalMinVoltageIndex { get; set; }

        /// <summary>
        /// 电池簇单体最高电压编号
        /// </summary>
        public ushort TotalMaxVoltageIndex { get; set; }

        /// <summary>
        /// 电池簇单体最低温度
        /// </summary>
        public short TotalMinTemp { get; set; }

        /// <summary>
        /// 电池簇单体最高温度
        /// </summary>
        public short TotalMaxTemp { get; set; }

        /// <summary>
        /// 电池簇单体最低温度编号
        /// </summary>
        public ushort TotalMinTempIndex { get; set; }

        /// <summary>
        /// 电池簇单体最高温度编号
        /// </summary>
        public ushort TotalMaxTempIndex { get; set; }

        /// <summary>
        /// 循环次数
        /// </summary>
        public ushort CycleCount { get; set; }

        /// <summary>
        /// 硬件版本
        /// </summary>
        public ushort HardwareVer { get; set; }

        /// <summary>
        /// BMCU硬件主版本
        /// </summary>
        public ushort MainHardwareVer { get; set; }

        /// <summary>
        /// BMCU硬件子版本
        /// </summary>
        public ushort MinorHardwareVer { get; set; }

        /// <summary>
        /// BMCU软件主版本
        /// </summary>
        public ushort MainSoftwareVer { get; set; }

        /// <summary>
        /// BMCU软件子版本
        /// </summary>
        public ushort MinorSoftwareVer { get; set; }

        /// <summary>
        /// 电池总数
        /// </summary>
        public ushort BatteryUnitCount { get; set;}

        /// <summary>
        /// 串联电池模块个数
        /// </summary>
        public ushort BatterySeriesCount { get; set; }

        /// <summary>
        /// 模块中电池个数
        /// </summary>
        public ushort BatteryUnitCountInSeries { get; set; }

        /// <summary>
        /// 电压平台
        /// </summary>
        public ushort VoltagePlatform { get; set; }

        /// <summary>
        /// 单串额定AH容量
        /// </summary>
        public ushort BatterySeriesCapacity { get; set; }

        public string BCMUName { get; set; }
        public BMU[] BMUArray { get; set; }
        public string Address { get; set; }
        public int DefaultPort { get; set; }
        private ModbusClient client;
        private bool IsConnected = false;
        public BCMU()
        {

        }

        public void ConnectBCMU()
        {
            if (!IsConnected)
            {
                client = new ModbusClient(Address, DefaultPort);
                client.Connect();

                // 信息补全
                BCMUName = "BCMU(" + Address + ")";
                BMUArray = new BMU[] { new BMU() { BMUName = "BMU-A" }, new BMU() { BMUName = "BMU-B" }, new BMU() { BMUName = "BMU-C" } };

                IsConnected = true;
            }
        }

        public void DisconnectBCMU()
        {
            if (IsConnected)
            {
                client.Disconnect();
                IsConnected = false;
            }
        }

        //public int InitBCMU()
        //{
        //    client = new ModbusClient(Address, DefaultPort);
        //    client.ReadS16
        //}

        private void ReadTotalBatteryInfo()
        {
            TotalVoltage = client.ReadU16(11000);
            TotalCurrent = client.ReadU16(11001);
            TotalSOC = client.ReadU16(11002);
            TotalSOH = client.ReadU16(11003);
            TotalAverageTemp = client.ReadS16(11004);
            TotalMinVoltage = client.ReadU16(11005);
            TotalMaxVoltage = client.ReadU16(11006);
            TotalMinVoltageIndex = client.ReadU16(11007);
            TotalMaxVoltageIndex = client.ReadU16(11008);
            TotalMinTemp = client.ReadS16(11009);
            TotalMaxTemp = client.ReadS16(11010);
            TotalMinTempIndex = client.ReadU16(11011);
            TotalMaxTempIndex = client.ReadU16(11012);
            CycleCount = client.ReadU16(11013);
            HardwareVer = client.ReadU16(11014);
            MainHardwareVer = client.ReadU16(11015);
            MinorHardwareVer = client.ReadU16(11016);
            MainSoftwareVer = client.ReadU16(11017);
            MinorSoftwareVer = client.ReadU16(11018);
            BatteryUnitCount = client.ReadU16(11019);
            BatterySeriesCount = client.ReadU16(11020);
            BatteryUnitCountInSeries = client.ReadU16(11021);
            VoltagePlatform = client.ReadU16(11022);
            BatterySeriesCapacity = client.ReadU16(11023);
        }

        private void ReadSeriesBatteryInfo()
        {

        }

        private void ReadUnitBatteryInfo()
        {

        }
    }
}
