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
using System.IO;

namespace EMS.View
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : Window
    {
        public AboutView()
        {
            InitializeComponent();
            string EXE_Path = Environment.CurrentDirectory;
            string About_Path = "Resource/About/test.pdf";
            string Combine_path = System.IO.Path.Combine(EXE_Path, About_Path);
            theBrowser.Navigate(Combine_path);

        }
    }
}
