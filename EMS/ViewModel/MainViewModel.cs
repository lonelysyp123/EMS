using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand OpenSystemSetViewCommand { set; get; }
        public RelayCommand OpenDataAnalysisViewCommand { set; get; }
        public RelayCommand StartDaqCommand { set; get; }
        public RelayCommand StopDaqCommand { set; get; }

        public RelayCommand OpenAboutCommand { set; get; }

        public StateContentViewModel StateContent;
        public DisplayContentViewModel DisplayContent;
        public SystemConfigurationBase SystemConfiguration;
        public MainViewModel()
        {
            OpenSystemSetViewCommand = new RelayCommand(OpenSystemSetView);
            OpenDataAnalysisViewCommand = new RelayCommand(OpenDataAnalysisView);
            StartDaqCommand = new RelayCommand(StartDaq);
            StopDaqCommand = new RelayCommand(StopDaq);
            OpenAboutCommand = new RelayCommand(OpenAboutView);
            StateContent = new StateContentViewModel();
            DisplayContent = new DisplayContentViewModel();
            SystemConfiguration = new SystemConfigurationBase();
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

        private void StopDaq()
        {
            DisplayContent.StopSaveBatteryInfo();
        }

        private void StartDaq()
        {
            DisplayContent.StartSaveBatteryInfo(SystemConfiguration.daqConfiguration.DaqTimeSpan);
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
                }
            }
            else
            {
                SystemSetView view = new SystemSetView(SystemConfiguration);
                if (view.ShowDialog() == true)
                {
                    manage.Insert(SystemConfiguration.daqConfiguration);
                }
            }
        }
       

    }
}
