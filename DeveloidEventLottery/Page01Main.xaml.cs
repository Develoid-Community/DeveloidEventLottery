using System;
using System.Windows;
using System.Windows.Controls;

namespace DeveloidEventLottery
{
    /// <summary>
    /// PageMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page01Main : Page
    {
        public Page01Main()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.FOTTER_LOCK = false;
        }

        // 상품 목록 가져오기
        private void ButtonClick_ImportItemList(object s, RoutedEventArgs e)
        {
            Console.WriteLine("Get event item list.");
            CSV.SetItemList();
            ItemList.ItemsSource = Bindings.LIST_ITEM;
            ItemList.Items.Refresh();

            if (ItemList.Items.Count > 0)
                MessageBox.Show("이벤트 상품 목록을 불러왔습니다.", "안내", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 유저 목록 가져오기
        private void ButtonClick_ImportUserList(object s, RoutedEventArgs e)
        {
            Console.WriteLine("Get user list.");
            CSV.SetUserList();
            UserList.ItemsSource = Bindings.LIST_USER_VIEW;
            UserList.Items.Refresh();

            if (UserList.Items.Count > 0)
                MessageBox.Show("참여 회원 목록을 불러왔습니다.", "안내", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 파일 다운로드
        private void ButtonClick_FormDownload(object s, RoutedEventArgs e)
        {
            Console.WriteLine("Save csv forms.");
            CSV.FormSave();
            MessageBox.Show("CSV 양식 저장 완료.", "안내", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 추첨
        private void ButtonClick_LotteryRun(object s, RoutedEventArgs e)
        {
            if (ItemList.Items.Count < 1)
            {
                Console.WriteLine("Event item list is null.");
                MessageBox.Show("이벤트 상품 목록을 추가해주세요", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (UserList.Items.Count < 1)
            {
                Console.WriteLine("User list is null.");
                MessageBox.Show("참여 회원 목록을 추가해주세요", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (MessageBox.Show("추첨을 진행합니다.", "안내", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Console.WriteLine("Lottery Start");
                MainWindow.ContainerChange(new Page02CountDown());
            }
            else
            {
                Console.WriteLine("Lottery Cancel");
                MessageBox.Show("추첨을 취소했습니다.");
            }

        }

    }
}
