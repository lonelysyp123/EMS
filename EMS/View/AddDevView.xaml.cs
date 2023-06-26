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
    /// AddDevView.xaml 的交互逻辑
    /// </summary>
    public partial class AddDevView : Window
    {
        public bool IsRtu = false;
        public AddDevView()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ChooseTCP_Click(object sender, RoutedEventArgs e)
        {
            TCPGrid.Visibility = Visibility.Visible;
            RTUGrid.Visibility = Visibility.Collapsed;
            IsRtu = false;
        }

        private void ChooseRTU_Click(object sender, RoutedEventArgs e)
        {
            RTUGrid.Visibility = Visibility.Visible;
            TCPGrid.Visibility = Visibility.Collapsed;
            IsRtu = true;
        }
    }
}
