using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// AddDevArrayView.xaml 的交互逻辑
    /// </summary>
    public partial class AddDevArrayView : Window
    {
        public bool IsRtu = false;
        public string segment;
        public int beforeN;
        public int afterN;
        public AddDevArrayView()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsRtu)
            {
                if (RTUPort1.Text.Contains("COM") && RTUPort2.Text.Contains("COM"))
                {
                    segment = "COM";
                    beforeN = 0;
                    int.TryParse(RTUPort1.Text.Replace("COM", ""), out beforeN);
                    afterN = 0;
                    int.TryParse(RTUPort2.Text.Replace("COM", ""), out afterN);
                    if (afterN < beforeN)
                    {
                        MessageBox.Show("IP地址输入有误!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("端口号不合规!");
                    return;
                }
            }
            else
            {
                segment = IP1.P1.Text + "." + IP1.P2.Text + "." + IP1.P3.Text + ".";
                if (IP1.P1.Text == IP2.P1.Text && IP1.P2.Text == IP2.P2.Text && IP1.P3.Text == IP2.P3.Text)
                {
                    beforeN = 0;
                    int.TryParse(IP1.P4.Text, out beforeN);
                    afterN = 0;
                    int.TryParse(IP2.P4.Text, out afterN);
                    if (afterN < beforeN)
                    {
                        MessageBox.Show("IP地址输入有误!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("IP地址网段不同!");
                    return;
                }
            }
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
