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

namespace OseroTest
{
    /// <summary>
    /// UserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class OseroUserControl : UserControl
    {
        public OseroUserControl()
        {
            InitializeComponent();

            // String imgFileName = "";
            // imgFileName = "osero-illust1.png";

            // Image img = new Image();
            // BitmapImage imgSrc = new BitmapImage();
            // imgSrc.BeginInit();
            // imgSrc.UriSource = new Uri(imgFileName,UriKind.Relative);
            // imgSrc.CacheOption = BitmapCacheOption.OnLoad;
            // imgSrc.EndInit();

            // img.Source = imgSrc;
            // GridRoot.Children.Add(img);

        }
    }
}
