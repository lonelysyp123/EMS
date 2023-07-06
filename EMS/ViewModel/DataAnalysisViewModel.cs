using CommunityToolkit.Mvvm.Input;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Storage.DB.DBManage;
using static System.Net.Mime.MediaTypeNames;
using EMS.Model;
using System.Collections.ObjectModel;
using EMS.Storage.DB.Models;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows.Markup;

namespace EMS.ViewModel
{
    public class DataAnalysisViewModel : ViewModelBase
    {
        private PlotModel _displayData;
        public PlotModel DisplayData
        {
            get => _displayData;
            set
            {
                SetProperty(ref _displayData, value);
            }
        }

        private ObservableCollection<string> _dataTypeList;
        public ObservableCollection<string> DataTypeList
        {
            get => _dataTypeList;
            set
            {
                SetProperty(ref _dataTypeList, value);
            }
        }

        private string _selectedDataType;
        public string SelectedDataType
        {
            get => _selectedDataType;
            set
            {
                if(SetProperty(ref _selectedDataType, value))
                {
                    // 改变选择展示的数据类型
                    SwitchDataType(value);
                }
            }
        }

        private string _startTime1;
        public string StartTime1
        {
            get => _startTime1;
            set
            {
                SetProperty(ref _startTime1, value);
            }
        }

        private string _startTime2;
        public string StartTime2
        {
            get => _startTime2;
            set
            {
                SetProperty(ref _startTime2, value);
            }
        }

        private string _endTime1;
        public string EndTime1
        {
            get => _endTime1;
            set
            {
                SetProperty(ref _endTime1, value);
            }
        }

        private string _endTime2;
        public string EndTime2
        {
            get => _endTime2;
            set
            {
                SetProperty(ref _endTime2, value);
            }
        }

        private string _idSeries;
        public string IdSeries
        {
            get => _idSeries;
            set
            {
                SetProperty(ref _idSeries, value);
            }
        }

        public RelayCommand QueryCommand { set; get; }

        private List<BatteryBase> BatteryData;
        private List<BatterySeriesBase> SeriesData;
        private List<BatteryTotalBase> TotalData;

        public DataAnalysisViewModel()
        {
            QueryCommand = new RelayCommand(Query);
            DisplayData = new PlotModel();
            DisplayData = new PlotModel();
            DataTypeList = new ObservableCollection<string>();
            IdSeries = "0-0-0";
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";

            //ChartShowNow(storeModel.VolCollect.ToArray());
        }

        public DataAnalysisViewModel(List<BatteryTotalBase> items)
        {
            QueryCommand = new RelayCommand(Query);
            DisplayData = new PlotModel();
            DataTypeList = new ObservableCollection<string>();
            IdSeries = "0-0-0";
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (IdSeries != null)
            {
                var items = IdSeries.Split('-');
                if (TryCombinTime(StartTime1, StartTime2, out DateTime StartTime))
                {
                    if (TryCombinTime(EndTime1, EndTime2, out DateTime EndTime))
                    {
                        if (items[0] != "N")
                        {
                            if (items[1] != "N")
                            {
                                SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                                var SeriesList = SeriesManage.Find(items[0], items[1], StartTime, EndTime);
                                
                                if (items[2] != "N")
                                {
                                    DataTypeList.Clear();
                                    DataTypeList.Add("Voltage");
                                    DataTypeList.Add("Current");
                                    if (int.TryParse(items[2], out int Sort))
                                    {
                                        // 查询Battery数据
                                        BatteryData = new List<BatteryBase>();
                                        for (int i = 1; i < SeriesList.Count; i++)
                                        {
                                            BatteryBase battery = new BatteryBase();
                                            var item0 = typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + Sort).GetValue(SeriesList[i]);
                                            if (ushort.TryParse(item0.ToString(), out ushort vol))
                                            {
                                                var item1 = typeof(SeriesBatteryInfoModel).GetProperty("Current" + Sort).GetValue(SeriesList[i]);
                                                if (ushort.TryParse(item1.ToString(), out ushort cur))
                                                {
                                                    battery.Voltage = vol;
                                                    battery.Current = cur;
                                                    BatteryData.Add(battery);
                                                }
                                            }
                                        }
                                        ChartShowNow(BatteryData.Select(p=>p.Voltage).Select<ushort, double>(x=>x).ToArray());
                                    }
                                }
                                else
                                {
                                    DataTypeList.Clear();
                                    DataTypeList.Add("Voltage");
                                    DataTypeList.Add("Current");
                                    // 查询Series数据
                                    SeriesData = new List<BatterySeriesBase>();
                                    for (int i = 1; i < SeriesList.Count; i++)
                                    {
                                        BatterySeriesBase series = new BatterySeriesBase();
                                        series.SeriesVoltage = (ushort)SeriesList[i].SeriesVoltage;
                                        series.SeriesCurrent = (ushort)SeriesList[i].SeriesCurrent;
                                        SeriesData.Add(series);
                                    }
                                    ChartShowNow(SeriesData.Select(p => p.SeriesVoltage).Select<ushort, double>(x => x).ToArray());
                                }
                            }
                            else
                            {
                                DataTypeList.Clear();
                                DataTypeList.Add("Voltage");
                                DataTypeList.Add("Current");
                                // 查询Total数据
                                TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
                                var TotalList = TotalManage.Find(items[0], StartTime, EndTime);
                                TotalData = new List<BatteryTotalBase>();
                                for (int i = 1; i < TotalList.Count; i++)
                                {
                                    BatteryTotalBase total = new BatteryTotalBase();
                                    total.TotalVoltage = (ushort)TotalList[i].TotalVoltage;
                                    total.TotalCurrent = (ushort)TotalList[i].TotalCurrent;
                                    TotalData.Add(total);
                                }
                                ChartShowNow(TotalData.Select(p => p.TotalVoltage).Select<ushort, double>(x => x).ToArray());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        private void SwitchDataType(string type)
        {
            var items = IdSeries.Split('-');
            if (items[0] != "N")
            {
                if (items[1] != "N")
                {
                    if (items[2] != "N")
                    {
                        if (type == "Voltage")
                        {
                            ChartShowNow(BatteryData.Select(p => p.Voltage).Select<ushort, double>(x => x).ToArray());
                        }
                        else if(type == "Current")
                        {
                            ChartShowNow(BatteryData.Select(p => p.Current).Select<ushort, double>(x => x).ToArray());
                        }
                    }
                    else
                    {
                        if (type == "Voltage")
                        {
                            ChartShowNow(SeriesData.Select(p => p.SeriesVoltage).Select<ushort, double>(x => x).ToArray());
                        }
                        else if(type == "Current")
                        {
                            ChartShowNow(SeriesData.Select(p => p.SeriesCurrent).Select<ushort, double>(x => x).ToArray());
                        }
                    }
                }
                else
                {
                    if (type == "Voltage")
                    {
                        ChartShowNow(TotalData.Select(p => p.TotalVoltage).Select<ushort, double>(x => x).ToArray());
                    }
                    else if (type == "Current")
                    {
                        ChartShowNow(TotalData.Select(p => p.TotalCurrent).Select<ushort, double>(x => x).ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 合成时间
        /// </summary>
        /// <param name="obj1">时间1</param>
        /// <param name="obj2">时间2</param>
        /// <param name="CombinTime">合成后的时间</param>
        /// <returns>是否合成成功</returns>
        private bool TryCombinTime(string obj1, string obj2, out DateTime CombinTime)
        {
            if (obj1 != null)
            {
                DateTime time1 = DateTime.Parse(obj1);
                if (TimeSpan.TryParse(obj2, out TimeSpan time2))
                {
                    CombinTime = time1 + time2;
                }
                else
                {
                    CombinTime = DateTime.Now;
                    return false;
                }
            }
            else
            {
                CombinTime = DateTime.Now;
                return false;
            }
            return true;
        }

        private void DisplayElcData()
        {
            InitChart("电流", "时间");
            //ChartShowNow(storeModel.ElcCollect.ToArray());
        }

        private void DisplayVolData()
        {
            InitChart("电压", "时间");
            //ChartShowNow(storeModel.VolCollect.ToArray());
        }

        /// <summary>
        /// 初始化图表控件（定义X，Y轴）
        /// </summary>
        private void InitChart(string LeftName, string BottomName)
        {
            //! Axes
            DisplayData.Axes.Clear();
            DisplayData.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = LeftName });
            DisplayData.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Title = BottomName });
        }

        /// <summary>
        /// 绘制光谱
        /// </summary>
        /// <param name="Data">数据</param>
        public void ChartShowNow(double[] Data)
        {
            try
            {
                //! Series
                LineSeries lineSeries = new LineSeries();
                for (int i = 0; i < Data.Length; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, Data[i]));
                }

                DisplayData.Series.Clear();
                DisplayData.Series.Add(lineSeries);
                DisplayData.InvalidatePlot(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

