using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.FOTTER_LOCK = true;

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
