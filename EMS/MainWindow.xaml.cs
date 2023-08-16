using EMS.Model;
using EMS.MyControl;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.View;
using EMS.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            DevListView.DataContext = viewmodel.DisplayContent;
            SelectedPage("DaqDataBorder");
        }

        private void ReConnect_Click(object sender, RoutedEventArgs e)
        {
            var item = DevList.SelectedItem as BatteryTotalBase;
            try
            {
                if (item.IsConnected)
                {
                    DisConnect_Click(null, null);
                    ReConnect_Click(null,null);
                }
                else
                {
                    // 连接成功后将设备信息添加到左边的导航栏中
                    if (viewmodel.DisplayContent.AddConnectedDev(item))
                    {
                        // 更新数据库中设备信息BCMUID
                        DevConnectInfoManage manage = new DevConnectInfoManage();
                        manage.Update(new DevConnectInfoModel() { BCMUID = item.BCMUID, IP = item.IP, Port = item.Port });
                    }
                }
            }
            catch
            {
                MessageBox.Show("重新连接设备失败，请检查通讯参数和连接介质！");
            }
        }

        private void DisConnect_Click(object sender, RoutedEventArgs e)
        {
            // 断开连接设备
            var item = DevList.SelectedItem as BatteryTotalBase;
            int index = viewmodel.DisplayContent.RemoveDisConnectedDev(item);
        }

        private void InterNet_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("入网操作");
            var item = DevList.SelectedItem as BatteryTotalBase;
            viewmodel.DisplayContent.RequestInterNet(item);
        }

        private void DelDev_Click(object sender, RoutedEventArgs e)
        {
            var item = DevList.SelectedItem as BatteryTotalBase;
            if (!item.IsConnected)
            {
                viewmodel.DisplayContent.BatteryTotalList.Remove(item);
                DevConnectInfoManage manage = new DevConnectInfoManage();
                manage.Delete(new DevConnectInfoModel() { IP = item.IP, Port = item.Port, BCMUID = item.BCMUID });
            }  
            else
            {
                MessageBox.Show("请先断开设备连接");    
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        DevTest_CollectView devTest_Daq;
        DataAnalysis_OptimizeView dataAnalysis_Optimize;
        DevControlView devControlView;
        //DevAlarm_ErrorView devAlarm_Error;
        private void SwitchBorder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LinearGradientBrush selectedbrush = new LinearGradientBrush(Color.FromRgb(255, 217, 173), Color.FromRgb(253, 159, 47), new Point(0.5, 0), new Point(0.5, 1));
            LinearGradientBrush unselectedbrush = new LinearGradientBrush(Color.FromRgb(228, 228, 228), Color.FromRgb(165, 165, 165), new Point(0.5, 0), new Point(0.5, 1));
            DaqDataBorder.Background = unselectedbrush;
            DaqDataBorder.Width = 150;
            AnalysisDataBorder.Background = unselectedbrush;
            AnalysisDataBorder.Width = 150;
            ControlBorder.Background = unselectedbrush;
            ControlBorder.Width = 150;
            //AlarmBorder.Background = unselectedbrush;
            (sender as Border).Background = selectedbrush;
            (sender as Border).Width = 200;
            SelectedPage((sender as Border).Name);
        }

        private void SelectedPage(string PageName)
        {
            switch (PageName)
            {
                case "DaqDataBorder":
                    if (devTest_Daq == null)
                    {
                        devTest_Daq = new DevTest_CollectView();
                        devTest_Daq.DevSource = viewmodel.DisplayContent.OnlineBatteryTotalList;
                        devTest_Daq.DevSource.CollectionChanged += devTest_Daq.Test_CollectionChanged;
                    }
                    MainBody.Content = new Frame() { Content = devTest_Daq };
                    break;
                case "AnalysisDataBorder":
                    if (dataAnalysis_Optimize == null)
                    {
                        dataAnalysis_Optimize = new DataAnalysis_OptimizeView();
                    }
                    MainBody.Content = new Frame() { Content = dataAnalysis_Optimize };
                    break;
                case "ControlBorder":
                    if (devControlView == null)
                    {
                        devControlView = new DevControlView();
                    }
                    devControlView.SyncContent(viewmodel.DisplayContent.OnlineBatteryTotalList.ToList(), viewmodel.DisplayContent.ClientList);
                    MainBody.Content = new Frame() { Content = devControlView };
                    break;
                default:
                    break;
            }
        }

        

        private void OperationManual_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "About");
            string filePath = System.IO.Path.Combine(folderPath, "OperationManual.pdf");

            System.Diagnostics.Process.Start(filePath);
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            
            view.ShowDialog();
            
        }

      

        



    }
   
    
    
}
