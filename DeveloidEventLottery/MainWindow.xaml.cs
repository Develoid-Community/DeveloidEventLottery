using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeveloidEventLottery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 제목표시줄

        private Point startPos;
        readonly System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;

        // 위치 변경되는 경우
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (var item in screens)
            {
                sum += item.WorkingArea.Width;
                if (sum >= this.Left + this.Width / 2)
                {
                    this.MaxHeight = item.WorkingArea.Height;
                    break;
                }
            }
        }

        // 마우스 잡고 이동하는 경우
        private void System_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.WindowState == WindowState.Maximized && Math.Abs(startPos.Y - e.GetPosition(null).Y) > 2)
                {
                    var point = PointToScreen(e.GetPosition(null));

                    this.WindowState = WindowState.Normal;

                    this.Left = point.X - this.ActualWidth / 2;
                    this.Top = point.Y - TitleBar.ActualHeight / 2;
                }
                DragMove();
            }
        }

        // 닫기
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        // 콘솔 창
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        // 프레임
        public static Frame CONTAINER;
        public static TextBlock FOTTER;

        public static bool FOTTER_LOCK = false;

        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            if (Debugger.IsAttached == false)
            {
                AllocConsole();
                Console.WriteLine("Debug mode on.");
            }
#endif
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CONTAINER = Container;
            FOTTER = Footer;

            ContainerChange(new Page01Main());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached == false)
            {
                Console.WriteLine("Press Enter to close..");
                Console.ReadLine();
            }
#endif
        }

        public static void ContainerChange(Page page)
        {
            CONTAINER.Navigate(page);
        }

        public static void ContainerBack()
        {
            if (CONTAINER.CanGoBack) CONTAINER.NavigationService.GoBack();
        }

        public static void ContainerForword()
        {
            if (CONTAINER.CanGoForward) CONTAINER.NavigationService.GoForward();
        }

        private void Footer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!FOTTER_LOCK)
            {
                FOTTER.Visibility = Visibility.Hidden;
                ContainerChange(new Page00Information());
            }
        }
    }
}
