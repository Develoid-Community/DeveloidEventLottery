using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 제목표시줄 + 중복 실행 방지

        // 중복 실행 방지
        [DllImportAttribute("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string clsName, string wndName);

        private Point startPos;
        readonly System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        // 상태 변경되는 경우
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) // 최대화
            {
                //TitleBar.Margin = new Thickness(7);
                //rectMax.Visibility = Visibility.Hidden;
                //rectMin.Visibility = Visibility.Visible;
            }
            else // 이외 전부
            {
                //TitleBar.Margin = new Thickness(0);
                //rectMax.Visibility = Visibility.Visible;
                //rectMin.Visibility = Visibility.Hidden;
            }
        }

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

        // 마우스 클릭을 놓은 경우
        private void System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount >= 2)
                {
                    this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
                }
                else
                {
                    startPos = e.GetPosition(null);
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                var pos = PointToScreen(e.GetPosition(this));
                IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
                IntPtr hMenu = GetSystemMenu(hWnd, false);
                int cmd = TrackPopupMenu(hMenu, 0x100, (int)pos.X, (int)pos.Y, 0, hWnd, IntPtr.Zero);
                if (cmd > 0) SendMessage(hWnd, 0x112, (IntPtr)cmd, IntPtr.Zero);
            }
        }

        // 최소화
        private void Mimimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // 최대화
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
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
