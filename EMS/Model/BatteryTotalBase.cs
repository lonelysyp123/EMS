using EMS.Common.Modbus.ModbusTCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Model
{
    /// <summary>
    /// 电池簇
    /// </summary>
    public class BatteryTotalBase
    {
        public ushort TotalVoltage { get; set; }
        public ushort TotalCurrent { get; set; }
        public ushort SeriesCount { get; set; }
        public ushort BatteriesCount { get; set; }
        public string TotalID { get; set; }
        public ObservableCollection<BatterySeriesBase> Series { get; set; }

        private ModbusClient client;
        private bool IsConnected = false;
        public BatteryTotalBase()
        {
            Series = new ObservableCollection<BatterySeriesBase>();

        }

        public void ConnectByTCP(string Address, int Port)
        {
            if (!IsConnected)
            {
                client = new ModbusClient(Address, Port);
                client.Connect();
                IsConnected = true;
            }
        }

        public void ConnectByRTU(string Port, int Rate, int Parity, int Databits, int Stopbits)
        {
            if (!IsConnected)
            {
                client = new ModbusClient(Port, Rate, Parity, Databits, Stopbits);
                client.Connect();
                IsConnected = true;
            }
        }

        public void InitBatteryTotal()
        {
            if (IsConnected)
            {
                // 信息补全
                TotalID = client.ReadU16(10000).ToString();

                SeriesCount = client.ReadU16(10000);
                BatteriesCount = client.ReadU16(10000);
                for (int i = 0; i < SeriesCount; i++)
                {
                    BatterySeriesBase series = new BatterySeriesBase();
                    series.SeriesId = client.ReadU16(10000).ToString();
                    for (int j = 0; j < BatteriesCount; j++)
                    {
                        BatteryBase battery = new BatteryBase();
                        battery.BatteryID = client.ReadU16((ushort)(10000+ i * BatteriesCount + j)).ToString();
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
                client.Disconnect();
                IsConnected = false;
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
                    // 获取总簇电池信息

                    // 获取单串电池信息

                    // 获取单个电池信息

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
