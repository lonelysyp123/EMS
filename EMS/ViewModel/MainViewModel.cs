using CommunityToolkit.Mvvm.ComponentModel;
using EMS.Model;
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
        private ObservableCollection<BMS> _devList;
        public ObservableCollection<BMS> DevList
        {
            get => _devList;
            set
            {
                SetProperty(ref _devList, value);
            }
        }

        private int _devIndex;
        public int DevIndex
        {
            get => _devIndex;
            set
            {
                SetProperty(ref _devIndex, value);
            }
        }

        public StateContentViewModel StateContent;

        public MainViewModel() 
        {
            StateContent = new StateContentViewModel();
        }
    }
}
