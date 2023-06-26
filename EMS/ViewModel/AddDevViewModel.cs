using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.ViewModel
{
    public class AddDevViewModel : ViewModelBase
    {
        private Visibility _tcpVisibility;
        public Visibility TCPVisibility
        {
            get => _tcpVisibility;
            set
            {
                SetProperty(ref _tcpVisibility, value);
            }
        }

        private Visibility _rtuVisibility;
        public Visibility RTUVisibility
        {
            get => _rtuVisibility;
            set
            {
                SetProperty(ref _rtuVisibility, value);
            }
        }

        public RelayCommand SelectTCPCommand { get; set; }
        public RelayCommand SelectRTUCommand { get; set; }

        public AddDevViewModel() 
        {
            SelectTCPCommand = new RelayCommand(SelectTCP);
            SelectRTUCommand = new RelayCommand(SelectRTU);
        }

        private void SelectRTU()
        {
            TCPVisibility = Visibility.Collapsed;
            RTUVisibility = Visibility.Visible;
        }

        private void SelectTCP()
        {
            TCPVisibility = Visibility.Visible;
            RTUVisibility = Visibility.Collapsed;
        }
    }
}
