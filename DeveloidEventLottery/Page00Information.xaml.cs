using System.Windows;
using System.Windows.Controls;

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
