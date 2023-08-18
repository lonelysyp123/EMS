using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.MyControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        public void Test_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    AddDevIntoView(item as BatteryTotalBase);
                }
            }
            else
            {
                foreach (var item in e.OldItems)
                {
                    RemoveDevIntoView(e.OldStartingIndex);
                }
            }
        }

        public void AddDevIntoView(BatteryTotalBase model)
        {
            DataControl control = new DataControl(model);
            control.Margin = new Thickness(30,10,30,10);
            
            for (int i = 0; i < 9; i++)
            {
                bool mIsEmpty = true;
                for (int l = 0; l < MainBody.Children.Count; l++)
                {
                    int col = Grid.GetColumn(MainBody.Children[l]);
                    int row = Grid.GetRow(MainBody.Children[l]);

                    if (col*3+row == i)
                    {
                        mIsEmpty = false;
                    }
                }

                if (mIsEmpty)
                {
                    Grid.SetColumn(control, i / 3);
                    Grid.SetRow(control, i % 3);
                    MainBody.Children.Add(control);
                    break;
                }
            }
        }

        public void RemoveDevIntoView(int index)
        {
            if (MainBody.Children.Count >= index)
            {
                MainBody.Children.RemoveAt(index);
            }
        }

        private void MainBody_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataControl control = e.Source as DataControl;
            if (control != null)
            {
                // 打开单个电池展示界面
                SeriesBatteryView view = new SeriesBatteryView((BatteryTotalBase)control.DataContext);
                view.Show();
            }
        }

        private ObservableCollection<BatteryTotalBase> devSource;
        public ObservableCollection<BatteryTotalBase> DevSource
        {
            get
            {
                return devSource;
            }
            set
            {
                devSource = value;
            }
        }
    }
}
