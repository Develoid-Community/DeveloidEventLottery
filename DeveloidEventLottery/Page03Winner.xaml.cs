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
    /// Page03Winner.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page03Winner : Page
    {
        public Page03Winner()
        {
            InitializeComponent();

            MainWindow.FOTTER_LOCK = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Get winner list.");

            Winner.Get();
            WinnerList.ItemsSource = Bindings.LIST_WINNER_VIEW;
            WinnerList.Items.Refresh();
        }

        // 메인으로
        private void ButtonClick_ToMain(object s, RoutedEventArgs e)
        {
            MainWindow.ContainerBack();
            MainWindow.ContainerBack();
        }

        // 다시 추첨
        private void ButtonClick_LotteryReRun(object s, RoutedEventArgs e)
        {
            MainWindow.ContainerBack();
        }

        // CSV로 결과 저장
        private void ButtonClick_SaveCSV(object s, RoutedEventArgs e)
        {
            Console.WriteLine("Save winner list csv.");
            CSV.SaveWinner();
            MessageBox.Show("당첨자 목록 저장 완료.", "안내", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
