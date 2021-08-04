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
using System.Timers;
using System.Windows.Threading;

namespace DeveloidEventLottery
{
    /// <summary>
    /// PageCountDown.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page02CountDown : Page
    {
        #region 변수
        public static int Count = 5;
        Timer timer;
        #endregion

        public Page02CountDown()
        {
            InitializeComponent();

            MainWindow.FOTTER_LOCK = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Count = 5;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += TimerTick;
            timer.Enabled = true;

            Console.WriteLine("Count down timer set.");
        }

        // 타이머 Tick, 1초 간격 동작
        private void TimerTick(object s, ElapsedEventArgs e)
        {
            Console.WriteLine(Count);

            if (Count < 1) // 0이 되면 타이머 종료 및 페이지 닫기
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(delegate
                {
                    timer.Stop();
                    Number.Text = "READY ?";
                    MainWindow.ContainerChange(new Page03Winner());
                }));
            }
            else
            {
                // 출력
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(delegate
                {
                    Number.Text = Count.ToString();
                }));
            }
            
            Count--;
        }
    }
}
