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
using OxyPlot.Legends;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace EMS.ViewModel
{
    public class DataAnalysisViewModel : ViewModelBase
    {
        private PlotModel _displayData;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel DisplayDataModel
        {
            get => _displayData;
            set
            {
                SetProperty(ref _displayData, value);
            }
        }

        private string _selectedTotal;
        /// <summary>
        /// BCMU序列号
        /// </summary>
        public string SelectedTotal
        {
            get => _selectedTotal;
            set
            {
                SetProperty(ref _selectedTotal, value);
            }
        }

        private string _selectedSeries;
        /// <summary>
        /// BMU序列号
        /// </summary>
        public string SelectedSeries
        {
            get => _selectedSeries;
            set
            {
                SetProperty(ref _selectedSeries, value);
            }
        }

        private string _startTime1;
        /// <summary>
        /// 开始时间-日期
        /// </summary>
        public string StartTime1
        {
            get => _startTime1;
            set
            {
                SetProperty(ref _startTime1, value);
            }
        }

        private string _startTime2;
        /// <summary>
        /// 开始时间-时分秒
        /// </summary>
        public string StartTime2
        {
            get => _startTime2;
            set
            {
                SetProperty(ref _startTime2, value);
            }
        }

        private string _endTime1;
        /// <summary>
        /// 结束时间-日期
        /// </summary>
        public string EndTime1
        {
            get => _endTime1;
            set
            {
                SetProperty(ref _endTime1, value);
            }
        }

        private string _endTime2;
        /// <summary>
        /// 结束时间-时分秒
        /// </summary>
        public string EndTime2
        {
            get => _endTime2;
            set
            {
                SetProperty(ref _endTime2, value);
            }
        }

        private ListBoxItem _selectedType;
        /// <summary>
        /// 选中的数据类型
        /// </summary>
        public ListBoxItem SelectedType
        {
            get => _selectedType;
            set
            {
                SetProperty(ref _selectedType, value);
            }
        }

        private int _selectedTypeIndex;
        /// <summary>
        /// 选中的数据类型序号
        /// </summary>
        public int SelectedTypeIndex
        {
            get => _selectedTypeIndex;
            set
            {
                SetProperty(ref _selectedTypeIndex, value);
            }
        }

        public RelayCommand QueryCommand { set; get; }

        /// <summary>
        /// 选中的电池集合
        /// </summary>
        public List<string> SelectedBatteryList;

        /// <summary>
        /// 查询数据集合
        /// </summary>
        public List<List<double[]>> DisplayDataList;

        public List<DateTime[]> TimeList;

        public DataAnalysisViewModel()
        {
            QueryCommand = new RelayCommand(Query);

            DisplayDataModel = new PlotModel();
            DisplayDataList = new List<List<double[]>>();
            TimeList = new List<DateTime[]>();
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            SelectedBatteryList = new List<string>();
            //ChartShowNow(storeModel.VolCollect.ToArray());
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DisplayDataList.Clear();
            TimeList.Clear();
            if (SelectedTotal != null && SelectedSeries != null)
            {
                if (TryCombinTime(StartTime1, StartTime2, out DateTime StartTime) && TryCombinTime(EndTime1, EndTime2, out DateTime EndTime))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        DisplayDataList.Add(QueryBatteryInfo(SelectedTotal, SelectedSeries, (i+1).ToString(), StartTime, EndTime));
                    }
                }
            }
        }

        /// <summary>
        /// 查询单体电池数据
        /// </summary>
        /// <param name="BCMUID">BCMUID</param>
        /// <param name="BMUID">BMUID</param>
        /// <param name="sort">电池序号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        private List<double[]> QueryBatteryInfo(string BCMUID, string BMUID, string sort, DateTime startTime, DateTime endTime)
        {
            SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
            var SeriesList = SeriesManage.Find(BCMUID, BMUID, startTime, endTime);
            List<double[]> obj = new List<double[]>();
            if (int.TryParse(sort, out int Sort))
            {
                // 查询Battery数据
                List<double> vols = new List<double>();
                List<double> caps = new List<double>();
                List<double> socList = new List<double>();
                List<double> resistances = new List<double>();
                List<double> temperature1List = new List<double>();
                List<double> temperature2List = new List<double>();
                List<DateTime> times = new List<DateTime>();
                for (int i = 1; i < SeriesList.Count; i++)
                {
                    var item0 = typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + (Sort - 1)).GetValue(SeriesList[i]);
                    if (double.TryParse(item0.ToString(), out double vol))
                    {
                        vols.Add(vol);
                    }

                    var item1 = typeof(SeriesBatteryInfoModel).GetProperty("Capacity" + (Sort - 1)).GetValue(SeriesList[i]);
                    if (double.TryParse(item1.ToString(), out double cap))
                    {
                        caps.Add(cap);
                    }

                    var item2 = typeof(SeriesBatteryInfoModel).GetProperty("SOC" + (Sort - 1)).GetValue(SeriesList[i]);
                    if (double.TryParse(item2.ToString(), out double soc))
                    {
                        socList.Add(soc);
                    }

                    var item3 = typeof(SeriesBatteryInfoModel).GetProperty("Resistance" + (Sort - 1)).GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double resistance))
                    {
                        resistances.Add(resistance);
                    }

                    var item4 = typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (Sort - 1) * 2).GetValue(SeriesList[i]);
                    if (double.TryParse(item4.ToString(), out double temperature1))
                    {
                        temperature1List.Add(temperature1);
                    }

                    var item5 = typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + ((Sort - 1) * 2 + 1)).GetValue(SeriesList[i]);
                    if (double.TryParse(item5.ToString(), out double temperature2))
                    {
                        temperature2List.Add(temperature2);
                    }

                    times.Add(SeriesList[i].HappenTime);
                }
                obj.Add(vols.ToArray());
                obj.Add(socList.ToArray());
                obj.Add(resistances.ToArray());
                obj.Add(temperature1List.ToArray());
                obj.Add(temperature2List.ToArray());
                obj.Add(caps.ToArray());
                TimeList.Add(times.ToArray());
            }
            return obj;
        }

        /// <summary>
        /// 查询电池串数据
        /// </summary>
        /// <param name="BCMUID">BCMUID</param>
        /// <param name="BMUID">BMUID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        //private void QuerySeriesBatteryInfo(string BCMUID, string BMUID, DateTime startTime, DateTime endTime)
        //{
        //    SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
        //    var SeriesList = SeriesManage.Find(BCMUID, BMUID, startTime, endTime);
        //    // 查询Series数据
        //    List<double> vols = new List<double>();
        //    List<double> curs = new List<double>();
        //    for (int i = 1; i < SeriesList.Count; i++)
        //    {
        //        vols.Add(SeriesList[i].SeriesVoltage);
        //        curs.Add(SeriesList[i].SeriesCurrent);
        //        TimeList.Add(SeriesList[i].HappenTime);
        //    }
        //    DisplayDataList.Add(vols.ToArray());
        //    DisplayDataList.Add(curs.ToArray());
        //}

        /// <summary>
        /// 查询电池簇数据
        /// </summary>
        /// <param name="BCMUID">BCMUID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        //private void QueryTotalBatteryInfo(string BCMUID, DateTime startTime, DateTime endTime)
        //{
        //    // 查询Total数据
        //    List<double> vols = new List<double>();
        //    List<double> curs = new List<double>();
        //    List<double> socList = new List<double>();
        //    List<double> sohList = new List<double>();
        //    List<double> averageTemperatures = new List<double>();
        //    TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
        //    var TotalList = TotalManage.Find(BCMUID, startTime, endTime);
        //    for (int i = 1; i < TotalList.Count; i++)
        //    {
        //        vols.Add(TotalList[i].Voltage);
        //        curs.Add(TotalList[i].Current);
        //        socList.Add(TotalList[i].SOC);
        //        sohList.Add(TotalList[i].SOH);
        //        averageTemperatures.Add(TotalList[i].AverageTemperature);
        //        TimeList.Add(TotalList[i].HappenTime);
        //    }
        //    DisplayDataList.Add(vols.ToArray());
        //    DisplayDataList.Add(curs.ToArray());
        //    DisplayDataList.Add(socList.ToArray());
        //    DisplayDataList.Add(sohList.ToArray());
        //    DisplayDataList.Add(averageTemperatures.ToArray());
        //}

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        public void SwitchBatteryData()
        {
            InitChart();
            DisplayDataModel.Series.Clear();
            for (int i = 0; i < SelectedBatteryList.Count; i++)
            {
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = SelectedBatteryList[i];
                lineSeries.MarkerSize = 3;
                lineSeries.MarkerType = MarkerType.Circle;
                if (int.TryParse(SelectedBatteryList[i], out int index))
                {
                    if (DisplayDataList.Count > 0 && DisplayDataList.Count > index - 1)
                    {
                        if (DisplayDataList[index - 1].Count > 0)
                        {
                            if (DisplayDataList[index - 1][SelectedTypeIndex].Length > 0)
                            {
                                for (int j = 0; j < DisplayDataList[index - 1][SelectedTypeIndex].Length; j++)
                                {
                                    lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[index - 1][j], DisplayDataList[index - 1][SelectedTypeIndex][j]));
                                }
                                DisplayDataModel.Series.Add(lineSeries);
                            }
                        }
                    }
                }
            }
            DisplayDataModel.InvalidatePlot(true);
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

        /// <summary>
        /// 初始化图表控件（定义X，Y轴）
        /// </summary>
        private void InitChart()
        {
            //! Legend
            DisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            DisplayDataModel.Legends.Add(l);

            //! Axes
            DisplayDataModel.Axes.Clear();
            DisplayDataModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = SelectedType.Content.ToString() });
            DisplayDataModel.Axes.Add(new DateTimeAxis() { 
                Position = AxisPosition.Bottom, 
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",
            });
        }
    }
}

