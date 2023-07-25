using EMS.Model;
using EMS.MyControl;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EMS.View
{
    /// <summary>
    /// SeriesBatteryView.xaml 的交互逻辑
    /// </summary>
    public partial class SeriesBatteryView : Window
    {
        public SeriesBatteryView(BatteryTotalBase viewmodel)
        {
            InitializeComponent();

            InitView(viewmodel);
            //Series1.DataContext = viewmodel.Series[0];
        }

        private void InitView(BatteryTotalBase item)
        {
            for (int i = 0; i < 3; i++)
            {
                Grid grid;
                if (i == 0)
                {
                    grid = BMUA;
                }
                else if (i == 1)
                {
                    grid = BMUB;
                }
                else
                {
                    grid = BMUC;
                }

                for (int l = 0;l < item.Series[i].Batteries.Count; l++)
                {
                    Battery battery = new Battery();
                    Grid.SetRow(battery, l/7);
                    Grid.SetColumn(battery, l%7);
                    battery.Margin = new Thickness(5);
                    Binding binding = new Binding() { Path = new PropertyPath("SOC")};
                    battery.SetBinding(Battery.SOCProperty, binding);
                    battery.DataContext = item.Series[i].Batteries[l];
                    grid.Children.Add(battery);
                }
            }
        }
    }
}
