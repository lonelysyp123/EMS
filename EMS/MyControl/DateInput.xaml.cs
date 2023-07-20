using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace EMS.MyControl
{
    /// <summary>
    /// DateInput.xaml 的交互逻辑
    /// </summary>
    public partial class DateInput : UserControl
    {
        public DateInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DateTextProperty = 
            DependencyProperty.Register(
                "DateText", 
                typeof(string), 
                typeof(DateInput),
                new PropertyMetadata(null, OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DateInput;
            if (e.NewValue != null)
            {
                var items = (e.NewValue as string).Split(':');
                if (items.Length == 3)
                {
                    control.P1.Text = items[0];
                    control.P2.Text = items[1];
                    control.P3.Text = items[2];
                }
            }
        }

        private string _dateText;
        public string DateText
        {
            get
            {
                return (string)GetValue(DateTextProperty);
            }
            set
            {
                SetValue(DateTextProperty, value);
            }
        }

        private void Date_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if ((sender as TextBox).Text.Length == 2)
            {
                e.Handled = true;
            }
        }

        private void P1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = DateText.Split(':');
            if (items.Length == 3)
            {
                DateText = (sender as TextBox).Text + ":" + items[1] + ":" + items[2];
            }
        }

        private void P2_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = DateText.Split(':');
            if (items.Length == 3)
            {
                DateText = items[0] + ":" + (sender as TextBox).Text + ":" + items[2];
            }
        }

        private void P3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = DateText.Split(':');
            if (items.Length == 3)
            {
                DateText = items[0] + ":" + items[1] + ":" + (sender as TextBox).Text;
            }
        }
    }
}
