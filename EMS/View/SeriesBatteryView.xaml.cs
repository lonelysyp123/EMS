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
            for (int i = 0; i < item.Series.Count; i++)
            {
                Grid grid = new Grid();
                Grid.SetRow(grid, i+1);
                grid.Margin = new Thickness(20,15,20,15);
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Border border = new Border();
                border.Width = 150;
                border.Background = new SolidColorBrush(Color.FromRgb(253,255,240));
                Grid.SetColumn(border, 0);
                grid.Children.Add(border);

                Grid grid1 = new Grid();
                Grid.SetColumn(grid1, 1);
                for (int j = 0; j < 7; j++)
                {
                    grid1.ColumnDefinitions.Add(new ColumnDefinition());
                }
                grid1.RowDefinitions.Add(new RowDefinition());
                grid1.RowDefinitions.Add(new RowDefinition());

                for (int l = 0;l < item.Series[i].Batteries.Count; l++)
                {
                    Battery battery = new Battery();
                    Grid.SetRow(battery, l/7);
                    Grid.SetColumn(battery, l%7);
                    battery.Margin = new Thickness(5);
                    Binding binding = new Binding() { Path = new PropertyPath("SOC")};
                    battery.SetBinding(Battery.SOCProperty, binding);
                    battery.DataContext = item.Series[i].Batteries[l];
                    grid1.Children.Add(battery);
                }

                grid.Children.Add(grid1);

                ViewBody.Children.Add(grid);
            }
        }
    }
}
