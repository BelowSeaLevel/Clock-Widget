using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Makes sure we use the Timers timer, and not the Threading timer.
        readonly System.Timers.Timer t = new System.Timers.Timer();

        
        public MainWindow()
        {
            CenterWindowOnScreen();
            InstallMeOnStartUp();
            //WindowPosition();

            InitializeComponent();
            

            //Sets the timer to 1 sec.
            t.Interval = 1000;
            t.Elapsed += TimeMethod;
            //Starts the Timer.
            t.Start();

            
            //Gets the current day/month/year.
            TodaysDate();
        }

        /// <summary>
        /// Will call UpdateTime() method.
        /// </summary>
        private void TimeMethod(object sender, EventArgs e)
        {
            UpdateTime();
        }

        /// <summary>
        /// Will update TextBlock every second
        /// </summary>
        private void UpdateTime()
        {
            // Gets current time.
            DateTime time = DateTime.Now;


            // Executes the specified delegate synchronously,
            // on the thread the Dispatcher is associated with.
            this.Dispatcher.Invoke(() =>
            {
                TextBlockTime.Text = time.ToString("HH:m:ss");
            });

        }

        /// <summary>
        /// Gets current day / month / year.
        /// </summary>
        private void TodaysDate()
        {
            DateTime days = DateTime.Now;
            TextBlockDate.Text = days.ToString("dddd");
            TextBlockDate.Text += " ";
            TextBlockDate.Text += days.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Makes us able to move the application around,
        /// while we have transparency active.
        /// </summary>
        private new void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        /// <summary>
        /// Exists the application
        /// </summary>
        private void ButtonClickExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Instals it so it will run the application at start up.
        /// </summary>
        void InstallMeOnStartUp()
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            catch { }
        }

        private void WindowPosition(object sender, EventArgs e)
        {
            
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }


        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

    }
}
