using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "别名", Width = new DataGridLength(4, DataGridLengthUnitType.Star), Binding = new Binding("TotalID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalVoltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("TotalCurrent") });
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
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "别名", Width = new DataGridLength(4, DataGridLengthUnitType.Star), Binding = new Binding("SeriesId") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SeriesVoltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("SeriesCurrent") });
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
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "别名", Width = new DataGridLength(4, DataGridLengthUnitType.Star), Binding = new Binding("BatteryID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Voltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Current") });
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
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "别名", Width = new DataGridLength(4, DataGridLengthUnitType.Star), Binding = new Binding("BatteryID") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电压", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Voltage") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "电流", Width = new DataGridLength(3, DataGridLengthUnitType.Star), Binding = new Binding("Current") });
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

        //private void SetConnect_Click(object sender, RoutedEventArgs e)
        //{
        //}

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
