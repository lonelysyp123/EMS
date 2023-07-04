using EMS.Model;
using EMS.ViewModel;
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
    /// DataAnalysisView.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisView : Window
    {
        private DataAnalysisViewModel viewmodel;

        public DataAnalysisView()
        {
            InitializeComponent();

            viewmodel = new DataAnalysisViewModel();
            this.DataContext = viewmodel;
        }

        public DataAnalysisView(List<BatteryTotalBase> items)
        {
            InitializeComponent();

            viewmodel = new DataAnalysisViewModel(items);
            this.DataContext = viewmodel;
            //DevTree.Items.Add(viewmodel);
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
                    
                    //viewmodel.ChartShowNow()
                }
                else if (item is BatterySeriesBase)
                {
                    //CreateDataGridByBatterySeries((BatterySeriesBase)item);
                }
                else if (item is BatteryBase)
                {
                    //CreateDataGridByBattery((BatteryBase)item);
                }
                else
                {
                    //CreateDataGridByIntegratedDev((DisplayContentViewModel)item);
                }
            }
        }
    }
}
