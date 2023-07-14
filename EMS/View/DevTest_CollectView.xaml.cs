using EMS.Model;
using EMS.MyControl;
using System;
using System.Collections.Generic;
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

namespace EMS.View
{
    /// <summary>
    /// DevTest_CollectView.xaml 的交互逻辑
    /// </summary>
    public partial class DevTest_CollectView : Page
    {
        public DevTest_CollectView()
        {
            InitializeComponent();
        }

        public void AddDevIntoView(BatteryTotalBase model)
        {
            DataControl control = new DataControl();
            control.DataContext = model;
            control.Margin = new Thickness(30,10,30,10);
            Grid.SetColumn(control, 0);
            Grid.SetRow(control, 0);
            MainBody.Children.Add(control);
        }

        public void RemoveDevIntoView()
        {
            MainBody.Children.RemoveAt(0);
        }

        private void MainBody_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataControl control = e.Source as DataControl;
            if (control != null)
            {
                // 打开单个电池展示界面
            }
        }
    }
}
