using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections;
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

    public class CustomeSelectionItems
    {
        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        //Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(CustomeSelectionItems), new PropertyMetadata(OnSelectedItemsChanged));
        static public void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBox = d as ListBox;
            if ((listBox != null) && (listBox.SelectionMode == SelectionMode.Multiple))
            {
                if (e.OldValue != null)
                {
                    listBox.SelectionChanged -= OnlistBoxSelectionChanged;
                }
                IList collection = e.NewValue as IList;
                listBox.SelectedItems.Clear();
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        listBox.SelectedItems.Add(item);
                    }
                    listBox.SelectionChanged += OnlistBoxSelectionChanged;
                }
            }
        }

        static void OnlistBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList dataSource = GetSelectedItems(sender as DependencyObject);
            //添加用户选中的当前项.
            foreach (var item in e.AddedItems)
            {
                dataSource.Add(item);
            }
            //删除用户取消选中的当前项
            foreach (var item in e.RemovedItems)
            {
                dataSource.Remove(item);
            }
        }
    }
}
