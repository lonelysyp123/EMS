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

namespace EMS.MyControl
{
    /// <summary>
    /// Battery.xaml 的交互逻辑
    /// </summary>
    public partial class Battery : UserControl
    {
        public Battery()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SOCProperty =
            DependencyProperty.Register(
                "SOC",
                typeof(double),
                typeof(Battery),
                new PropertyMetadata(0.0, OnSOCPropertyChangedCallback));

        private static void OnSOCPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Battery;
            if (e.NewValue != null)
            {
                double obj = double.Parse(e.NewValue.ToString());
                if (obj >= 50) 
                {
                    if (obj == 100)
                    {
                        control.BatteryIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Battery100.png"));
                    }
                    else
                    {
                        control.BatteryIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Battery70.png"));
                    }
                }
                else
                {
                    if (obj == 0)
                    {
                        control.BatteryIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Battery0.png"));
                    }
                    else
                    {
                        control.BatteryIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Battery30.png"));
                    }
                }
                control.BatterySOC.Text = "SOC:" + obj + "%";
            }
        }

        public double SOC
        {
            get
            {
                return (double)GetValue(SOCProperty);
            }
            set
            {
                SetValue(SOCProperty, value);
            }
        }
    }
}
