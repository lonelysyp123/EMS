using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace EMS.MyControl
{
    /// <summary>
    /// IDInput.xaml 的交互逻辑
    /// </summary>
    public partial class IDInput : UserControl
    {
        public IDInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IdTextProperty =
            DependencyProperty.Register(
                "IdText",
                typeof(string),
                typeof(IDInput),
                new PropertyMetadata(null, OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as IDInput;
            var items = (e.NewValue as string).Split('-');
            if (items.Length == 3)
            {
                control.P1.Text = items[0];
                control.P2.Text = items[1];
                control.P3.Text = items[2];
            }
        }

        public string IdText
        {
            get
            {
                return (string)GetValue(IdTextProperty);
            }
            set
            {
                SetValue(IdTextProperty, value);
            }
        }

        public void SetAddressText(string date)
        {
            string[] parts = date.Split('-');
            if (parts.Length == 3)
            {
                P1.Text = parts[0];
                P2.Text = parts[1];
                P3.Text = parts[2];
            }
        }

        private void P1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = IdText.Split('-');
            if (items.Length == 3)
            {
                IdText = (sender as TextBox).Text + "-" + items[1] + "-" + items[2];
            }
        }

        private void P2_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = IdText.Split('-');
            if (items.Length == 3)
            {
                IdText = items[0] + "-" + (sender as TextBox).Text + "-" + items[2];
            }
        }

        private void P3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = IdText.Split('-');
            if (items.Length == 3)
            {
                IdText = items[0] + "-" + items[1] + "-" + (sender as TextBox).Text;
            }
        }
    }
}
