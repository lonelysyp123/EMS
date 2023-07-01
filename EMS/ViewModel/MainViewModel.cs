using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private int _batteryTotalIndex;
        public int BatteryTotalIndex
        {
            get => _batteryTotalIndex;
            set
            {
                SetProperty(ref _batteryTotalIndex, value);
            }
        }

        public RelayCommand OpenSystemSetViewCommand { set; get; }
        public RelayCommand StartDaqCommand { set; get; }
        public RelayCommand StopDaqCommand { set; get; }

        public StateContentViewModel StateContent;
        public DisplayContentViewModel DisplayContent;

        public MainViewModel()
        {
            OpenSystemSetViewCommand = new RelayCommand(OpenSystemSetView);
            StartDaqCommand = new RelayCommand(StartDaq);
            StopDaqCommand = new RelayCommand(StopDaq);

            StateContent = new StateContentViewModel();
            DisplayContent = new DisplayContentViewModel();
        }

        private void StopDaq()
        {
            DisplayContent.StopSaveBatteryInfo();
        }

        private void StartDaq()
        {
            DisplayContent.StartSaveBatteryInfo();
        }

        private void OpenSystemSetView()
        {
            SystemSetView view = new SystemSetView();
            view.ShowDialog();
        }
    }
}
