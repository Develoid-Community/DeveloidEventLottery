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

namespace DeveloidEventLottery
{
    /// <summary>
    /// Page00Information.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page00Information : Page
    {
        public Page00Information()
        {
            InitializeComponent();
        }

        private void ButtonClick_ToMain(object s, RoutedEventArgs e)
        {
            MainWindow.FOTTER.Visibility = Visibility.Visible;
            MainWindow.ContainerBack();
        }
    }
}
