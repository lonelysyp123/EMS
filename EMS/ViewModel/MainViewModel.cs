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

        public StateContentViewModel StateContent;
        public DisplayContentViewModel DisplayContent;

        public MainViewModel()
        {
            StateContent = new StateContentViewModel();
            DisplayContent = new DisplayContentViewModel();
        }
    }
}
