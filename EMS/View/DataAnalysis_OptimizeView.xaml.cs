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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.View
{
    /// <summary>
    /// DataAnalysis_OptimizeView.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysis_OptimizeView : Page
    {
        public DataAnalysis_OptimizeView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    foreach (var item in e.AddedItems)
            //    {
            //        viewmodel.SelectedDataTypeList.Add(item.ToString());
            //    }
            //}

            //if (e.RemovedItems.Count > 0)
            //{
            //    foreach (var item in e.RemovedItems)
            //    {
            //        viewmodel.SelectedDataTypeList.Remove(item.ToString());

            //    }
            //}

            //viewmodel.SwitchDataType();
        }
    }
}
