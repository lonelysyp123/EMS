using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.MyControl
{
    /// <summary>
    /// DataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataControl : UserControl
    {
        
       
        public DataControl()
        {
            InitializeComponent();
        }

        public DataControl(BatteryTotalBase model)
        {
            InitializeComponent();
            this.DataContext = model;
            DisplayContentViewModel viewModel = new DisplayContentViewModel();
            //model.FaultyStateBCMUColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
           // viewModel.GetActiveProtect(model);
            //viewModel.GetActiveFaulty(model);
            //viewModel.GetActiveAlarm(model);
            //FalutyState.InvalidateVisual();
            //model.GetColor(model.FaultyColorINFO);


        }
    }
}
