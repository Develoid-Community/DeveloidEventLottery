using System;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.FOTTER_LOCK = false;

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
