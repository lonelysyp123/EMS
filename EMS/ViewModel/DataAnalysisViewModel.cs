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

namespace EMS.ViewModel
{
    public class DataAnalysisViewModel : ViewModelBase
    {
        private PlotModel _displayData;
        public PlotModel DisplayDataModel
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

        public List<string> SelectedDataTypeList;
        public List<double[]> DisplayDataList;
        public List<DateTime> TimeList;

        public DataAnalysisViewModel()
        {
            QueryCommand = new RelayCommand(Query);
            DisplayDataList = new List<double[]>();
            TimeList = new List<DateTime>();
            DisplayDataModel = new PlotModel();
            var l = new Legend
            {
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            DisplayDataModel.Legends.Add(l);
            DataTypeList = new ObservableCollection<string>();
            IdSeries = "1-1-1";
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            SelectedDataTypeList = new List<string>();
            //ChartShowNow(storeModel.VolCollect.ToArray());
        }

        public DataAnalysisViewModel(List<BatteryTotalBase> items)
        {
            QueryCommand = new RelayCommand(Query);
            DisplayDataList = new List<double[]>();
            TimeList = new List<DateTime>();
            DisplayDataModel = new PlotModel();
            var l = new Legend
            {
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            DisplayDataModel.Legends.Add(l);
            DataTypeList = new ObservableCollection<string>();
            IdSeries = "1-1-1";      
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            SelectedDataTypeList = new List<string>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DisplayDataList.Clear();
            TimeList.Clear();
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
                                    DataTypeList.Add("SOC");
                                    DataTypeList.Add("Resistance");
                                    DataTypeList.Add("Temperature1");
                                    DataTypeList.Add("Temperature2");
                                    if (int.TryParse(items[2], out int Sort))
                                    {
                                        // 查询Battery数据
                                        List<double> vols = new List<double>();
                                        List<double> curs = new List<double>();
                                        List<double> socList = new List<double>();
                                        List<double> resistances = new List<double>();
                                        List<double> temperature1List = new List<double>();
                                        List<double> temperature2List = new List<double>();

                                        for (int i = 1; i < SeriesList.Count; i++)
                                        {
                                            var item0 = typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + (Sort-1)).GetValue(SeriesList[i]);
                                            if (double.TryParse(item0.ToString(), out double vol))
                                            {
                                                vols.Add(vol);
                                            }

                                            var item1 = typeof(SeriesBatteryInfoModel).GetProperty("Current" + (Sort-1)).GetValue(SeriesList[i]);
                                            if (double.TryParse(item1.ToString(), out double cur))
                                            {
                                                curs.Add(cur);
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

                                            var item5 = typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + ((Sort - 1) * 2 - 1)).GetValue(SeriesList[i]);
                                            if (double.TryParse(item5.ToString(), out double temperature2))
                                            {
                                                temperature2List.Add(temperature2);
                                            }

                                            TimeList.Add(SeriesList[i].HappenTime);
                                        }
                                        DisplayDataList.Add(vols.ToArray());
                                        DisplayDataList.Add(curs.ToArray());
                                        DisplayDataList.Add(socList.ToArray());
                                        DisplayDataList.Add(resistances.ToArray());
                                        DisplayDataList.Add(temperature1List.ToArray());
                                        DisplayDataList.Add(temperature2List.ToArray());
                                    }
                                }
                                else
                                {
                                    DataTypeList.Clear();
                                    DataTypeList.Add("Voltage");
                                    DataTypeList.Add("Current");
                                    // 查询Series数据
                                    List<double> vols = new List<double>();
                                    List<double> curs = new List<double>();
                                    for (int i = 1; i < SeriesList.Count; i++)
                                    {
                                        vols.Add(SeriesList[i].SeriesVoltage);
                                        curs.Add(SeriesList[i].SeriesCurrent);
                                        var span = SeriesList[i].HappenTime - StartTime;
                                        TimeList.Add(SeriesList[i].HappenTime);
                                    }
                                    DisplayDataList.Add(vols.ToArray());
                                    DisplayDataList.Add(curs.ToArray());
                                }
                            }
                            else
                            {
                                DataTypeList.Clear();
                                DataTypeList.Add("Voltage");
                                DataTypeList.Add("Current");
                                DataTypeList.Add("SOH");
                                DataTypeList.Add("SOC");
                                DataTypeList.Add("AverageTemperature");

                                // 查询Total数据
                                List<double> vols = new List<double>();
                                List<double> curs = new List<double>();
                                List<double> socList = new List<double>();
                                List<double> sohList = new List<double>();
                                List<double> averageTemperatures = new List<double>();
                                TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
                                var TotalList = TotalManage.Find(items[0], StartTime, EndTime);
                                for (int i = 1; i < TotalList.Count; i++)
                                {
                                    vols.Add(TotalList[i].Voltage);
                                    curs.Add(TotalList[i].Current);
                                    socList.Add(TotalList[i].SOC);
                                    sohList.Add(TotalList[i].SOH);
                                    averageTemperatures.Add(TotalList[i].AverageTemperature);
                                    var span = TotalList[i].HappenTime - StartTime;
                                    TimeList.Add(TotalList[i].HappenTime);
                                }
                                DisplayDataList.Add(vols.ToArray());
                                DisplayDataList.Add(curs.ToArray());
                                DisplayDataList.Add(socList.ToArray());
                                DisplayDataList.Add(sohList.ToArray());
                                DisplayDataList.Add(averageTemperatures.ToArray());
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
        public void SwitchDataType()
        {
            InitChart();
            DisplayDataModel.Series.Clear();
            for (int i = 0; i < SelectedDataTypeList.Count; i++)
            {
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = SelectedDataTypeList[i];
                lineSeries.MarkerSize = 3;
                lineSeries.MarkerType = MarkerType.Circle;
                int index = DataTypeList.IndexOf(SelectedDataTypeList[i]);
                for (int j = 0; j < DisplayDataList[index].Length; j++)
                {
                    lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[j], DisplayDataList[index][j]));
                }
                DisplayDataModel.Series.Add(lineSeries);
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
            //! Axes
            DisplayDataModel.Axes.Clear();
            DisplayDataModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = "幅值" });
            DisplayDataModel.Axes.Add(new DateTimeAxis() { 
                Position = AxisPosition.Bottom, 
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",
                
            });
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

                DisplayDataModel.Series.Clear();
                DisplayDataModel.Series.Add(lineSeries);
                DisplayDataModel.InvalidatePlot(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChartShowNow(List<double[]> Data)
        {
            try
            {
                DisplayDataModel.Series.Clear();
                //! Series
                for (int j = 0; j < Data.Count; j++)
                {
                    LineSeries lineSeries = new LineSeries();
                    for (int i = 0; i < Data[j].Length; i++)
                    {
                        lineSeries.Points.Add(new DataPoint(i, Data[j][i]));
                    }
                    lineSeries.Title = "test" + j.ToString();
                    DisplayDataModel.Series.Add(lineSeries);
                }
                DisplayDataModel.InvalidatePlot(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddSeries(LineSeries line)
        {
            DisplayDataModel.Series.Add(line);
        }

        public void RemoveSeries(LineSeries line)
        {
            DisplayDataModel.Series.Remove(line);
        }
    }
}

