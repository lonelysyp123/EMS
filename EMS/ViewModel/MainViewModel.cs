using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EMS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ImageSource _startDaqImageSource;
        public ImageSource StartDaqImageSource
        {
            get
            {
                return _startDaqImageSource;
            }
            set
            {
                SetProperty(ref _startDaqImageSource, value);
            }
        }

        private SolidColorBrush _saveDataFillColor;
        public SolidColorBrush SaveDataFillColor
        {
            get
            {
                return _saveDataFillColor;
            }
            set
            {
                SetProperty(ref _saveDataFillColor, value);
            }
        }

        public RelayCommand OpenSystemSetViewCommand { set; get; }
        public RelayCommand OpenDataAnalysisViewCommand { set; get; }
        public RelayCommand OpenAboutCommand { set; get; }
        public RelayCommand StartOrStopDaqCommand { set; get; }
        public RelayCommand StartOrStopSaveDataCommand { set; get; }

        //public StateContentViewModel StateContent;
        public DisplayContentViewModel DisplayContent;
        public SystemConfigurationBase SystemConfiguration;

        public MainViewModel()
        {
            OpenSystemSetViewCommand = new RelayCommand(OpenSystemSetView);
            OpenDataAnalysisViewCommand = new RelayCommand(OpenDataAnalysisView);
            OpenAboutCommand = new RelayCommand(OpenAboutView);
            StartOrStopDaqCommand = new RelayCommand(StartOrStopDaq);
            StartOrStopSaveDataCommand = new RelayCommand(StartOrStopSaveData);
            //StateContent = new StateContentViewModel();
            DisplayContent = new DisplayContentViewModel();
            SystemConfiguration = InitSystemConfiguration();
            DisplayContent.DaqTimeSpan = SystemConfiguration.daqConfiguration.DaqTimeSpan;
            DaqImageButtonChange();
            SaveImageButtonChange();
        }

        public SystemConfigurationBase InitSystemConfiguration()
        {
            SystemConfigurationBase item = new SystemConfigurationBase();
            // 数据采集配置初始化
            DBManage<DaqConfigurationModel> manage = new DBManage<DaqConfigurationModel>();
            var daqconfigurations = manage.Get();
            if (daqconfigurations != null && daqconfigurations.Count > 0)
            {
                item.daqConfiguration = daqconfigurations[0];
            }
            return item;
        }

        private void StartOrStopSaveData()
        {
            if (DisplayContent.IsStartSaveData)
            {
                DisplayContent.IsStartSaveData = false;
                SaveImageButtonChange();
            }
            else
            {
                DisplayContent.IsStartSaveData = true;
                SaveImageButtonChange();
            }
        }

        private void StartOrStopDaq()
        {
            if (IsStartDaq)
            {
                // 停止采集和显示数据
                DisplayContent.StopDisplayRealTimeData();
                IsStartDaq = false;
                DaqImageButtonChange();
                //ShowOperation("数据采集已停止", "操作");
            }
            else
            {
                // 开始采集并显示数据
                DisplayContent.DisplayRealTimeData();
                IsStartDaq = true;
                DaqImageButtonChange();
                //ShowOperation("数据采集已开始", "操作");
            }
        }

        private bool IsStartDaq = false;
        public void DaqImageButtonChange()
        {
            BitmapImage bi;
            if (IsStartDaq)
            {
                DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                FileInfo[] files = directory.GetFiles("pause.png");
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                bi.EndInit();
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                FileInfo[] files = directory.GetFiles("play.png");
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                bi.EndInit();
            }
            StartDaqImageSource = bi;
        }

        private void SaveImageButtonChange()
        {
            if (DisplayContent.IsStartSaveData)
            {
                SaveDataFillColor = new SolidColorBrush(Colors.Red);
            }
            else
            {
                SaveDataFillColor = new SolidColorBrush(Colors.LightGreen);
            }
        }

        private void OpenAboutView()
        {
            AboutView aboutView = new AboutView();
            aboutView.ShowDialog();
        }

        private void OpenDataAnalysisView()
        {
            //DataAnalysisView view = new DataAnalysisView(DisplayContent.IntegratedDev.BatteryTotalList.ToList());
            DataAnalysisView view = new DataAnalysisView();
            view.ShowDialog();
        }

        private void OpenSystemSetView()
        {
            DBManage<DaqConfigurationModel> manage = new DBManage<DaqConfigurationModel>();
            var daqconfigurations = manage.Get();
            if (daqconfigurations != null && daqconfigurations.Count>0)
            {
                SystemConfiguration.daqConfiguration = daqconfigurations[0];
                SystemSetView view = new SystemSetView(SystemConfiguration);
                if (view.ShowDialog() == true)
                {
                    manage.Update(SystemConfiguration.daqConfiguration);
                    DisplayContent.DaqTimeSpan = SystemConfiguration.daqConfiguration.DaqTimeSpan;
                }
            }
            else
            {
                SystemSetView view = new SystemSetView(SystemConfiguration);
                if (view.ShowDialog() == true)
                {
                    manage.Insert(SystemConfiguration.daqConfiguration);
                    DisplayContent.DaqTimeSpan = SystemConfiguration.daqConfiguration.DaqTimeSpan;
                }
            }
        }
    }
}
