using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EMS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewmodel;

        public MainWindow()
        {
            InitializeComponent();

            viewmodel = new MainViewModel();
            this.DataContext = viewmodel;
            StateContent.DataContext = viewmodel.StateContent;
            DevListView.DataContext = viewmodel.DisplayContent.IntegratedDev;
            DevTree.Items.Add( viewmodel.DisplayContent );
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var view = sender as TreeView;
            var item = view.SelectedItem;
            if (item != null)
            {
                // 数据展示
                if (item is BatteryTotalBase)
                {
                    CreateDataGridByBatteryTotal((BatteryTotalBase)item);
                }
                else if (item is BatterySeriesBase)
                {
                    CreateDataGridByBatterySeries((BatterySeriesBase)item);
                }
                else if(item is BatteryBase)
                {
                    CreateDataGridByBattery((BatteryBase)item);
                }
                else
                {
                    CreateDataGridByIntegratedDev((DisplayContentViewModel)item);
                }
            }
        }

        private void CreateDataGridByIntegratedDev(DisplayContentViewModel item)
        {
            DataGridView.Children.Clear();
            DataGrid dataGrid = new DataGrid() { Name = "BatteryTotalDataView" };
            dataGrid.CanUserSortColumns = false;
            dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dataGrid.BorderBrush = new SolidColorBrush(Colors.Black);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserAddRows = false;
            ScrollViewer.SetVerticalScrollBarVisibility(dataGrid, ScrollBarVisibility.Hidden);
            dataGrid.MinRowHeight = 30;
            dataGrid.Background = new SolidColorBrush(Colors.Transparent);
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "序号", Width = new DataGridLength(2, DataGridLengthUnitType.Star), Binding = new Binding("BCMUID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "别名", Width = new DataGridLength(2, DataGridLengthUnitType.Star), Binding = new Binding("TotalID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalVoltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalCurrent") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "SOC", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalSOC") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "SOH", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalSOH") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "平均温度", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("AverageTemperature") });
            dataGrid.ItemsSource = item.OnlineBatteryTotalList;
            DataGridView.Children.Add(dataGrid);
        }

        private void CreateDataGridByBatteryTotal(BatteryTotalBase item)
        {
            DataGridView.Children.Clear();
            DataGrid dataGrid = new DataGrid() { Name = "BatteryTotalDataView" };
            dataGrid.CanUserSortColumns = false;
            dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dataGrid.BorderBrush = new SolidColorBrush(Colors.Black);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserAddRows = false;
            ScrollViewer.SetVerticalScrollBarVisibility(dataGrid, ScrollBarVisibility.Hidden);
            dataGrid.MinRowHeight = 30;
            dataGrid.Background = new SolidColorBrush(Colors.Transparent);
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "序号", Width = new DataGridLength(2, DataGridLengthUnitType.Star), Binding = new Binding("SeriesId") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SeriesVoltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SeriesCurrent") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "告警状态", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("AlarmState") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "故障状态", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("FaultState") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "充电通道状态", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("ChargeChannelState") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "充电累计容量", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("ChargeCapacitySum") });
            dataGrid.ItemsSource = item.Series;
            DataGridView.Children.Add(dataGrid);
        }

        private void CreateDataGridByBatterySeries(BatterySeriesBase item)
        {
            DataGridView.Children.Clear();
            DataGrid dataGrid = new DataGrid() { Name = "BatteryTotalDataView" };
            dataGrid.CanUserSortColumns = false;
            dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dataGrid.BorderBrush = new SolidColorBrush(Colors.Black);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserAddRows = false;
            ScrollViewer.SetVerticalScrollBarVisibility(dataGrid, ScrollBarVisibility.Hidden);
            dataGrid.MinRowHeight = 30;
            dataGrid.Background = new SolidColorBrush(Colors.Transparent);
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "序号", Width = new DataGridLength(2, DataGridLengthUnitType.Star), Binding = new Binding("BatteryID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Voltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Current") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "温度", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Temperature") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "SOC", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SOC") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "内阻", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Resistance") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "单体放满容量", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Capacity") });
            dataGrid.ItemsSource = item.Batteries;
            DataGridView.Children.Add(dataGrid);
        }

        private void CreateDataGridByBattery(BatteryBase battery)
        {
            DataGridView.Children.Clear();
            DataGrid dataGrid = new DataGrid() { Name = "BatteryTotalDataView" };
            dataGrid.CanUserSortColumns = false;
            dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dataGrid.BorderBrush = new SolidColorBrush(Colors.Black);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserAddRows = false;
            ScrollViewer.SetVerticalScrollBarVisibility(dataGrid, ScrollBarVisibility.Hidden);
            dataGrid.MinRowHeight = 30;
            dataGrid.Background = new SolidColorBrush(Colors.Transparent);
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "序号", Width = new DataGridLength(2, DataGridLengthUnitType.Star), Binding = new Binding("BatteryID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Voltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Current") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "温度", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Temperature") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "SOC", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SOC") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "内阻", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Resistance") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "单体放满容量", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Capacity") });
            dataGrid.ItemsSource = new ObservableCollection<BatteryBase>() { battery };
            DataGridView.Children.Add(dataGrid);
        }

        private void ReConnect_Click(object sender, RoutedEventArgs e)
        {
            var item = DevList.SelectedItem as BatteryTotalBase;
            try
            {
                // 连接成功后将设备信息添加到左边的导航栏中
                viewmodel.DisplayContent.AddConnectedDev(item);
                // 更新数据库中设备信息BCMUID
                DevConnectInfoManage manage = new DevConnectInfoManage();
                manage.Update(new DevConnectInfoModel() { BCMUID = item.BCMUID, IP = item.IP, Port = item.Port });
            }
            catch
            {
                viewmodel.DisplayContent.RemoveDisConnectedDev(item);
                MessageBox.Show("重新连接设备失败，请检查通讯参数和连接介质！");
            }
        }

        private void DisConnect_Click(object sender, RoutedEventArgs e)
        {
            // 断开连接设备
            var item = DevList.SelectedItem as BatteryTotalBase;
            viewmodel.DisplayContent.RemoveDisConnectedDev(item);
        }

        private void DelDev_Click(object sender, RoutedEventArgs e)
        {
            var item = DevList.SelectedItem as BatteryTotalBase;
            viewmodel.DisplayContent.IntegratedDev.BatteryTotalList.Remove(item);
            DevConnectInfoManage manage = new DevConnectInfoManage();
            manage.Delete(new DevConnectInfoModel() { IP = item.IP, Port = item.Port, BCMUID = item.BCMUID });
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
