using EMS.Model;
using EMS.MyControl;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

        private void InitView(BatteryTotalBase item)
        {
            for (int i = 0; i < item.Series.Count; i++)
            {
                Grid grid;
                Grid gridb;
                if (i == 0)
                {
                    grid = BMUA;
                    gridb = BMUA_Battery;
                }
                else if (i == 1)
                {
                    grid = BMUB;
                    gridb = BMUB_Battery;
                }
                else
                {
                    grid = BMUC;
                    gridb = BMUC_Battery;
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
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
                    gridb.Children.Add(battery);
                }
                grid.DataContext = item.Series[i];
            }
        }
    }
}
