using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace StandUp
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void minute_in_TextChanged(object sender, TextChangedEventArgs e)
        {
            int minutes;
            if (int.TryParse(minute_in.Text, out minutes))
            {
                start_button.IsEnabled = true;
                minute_left.Content = minute_in.Text+":00";
            }
            else
            {
                start_button.IsEnabled = false;
                minute_left.Content = "0:00";
            }
        }
        
        private string minutesAndSeconds(int seconds)
        {
            int minutes = seconds / 60;
            seconds = seconds % 60;
            string minutesString = minutes.ToString();
            string secondsString = seconds.ToString();
            if (secondsString.Length < 2) secondsString = "0" + secondsString;
            return minutesString + ":" + secondsString;
        }

        private void countDown(int minutes)
        {
            System.Windows.Threading.DispatcherTimer dt = new System.Windows.Threading.DispatcherTimer();
            dt.Tick += new EventHandler(dispatcherTimer_Tick);
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string minutesLeft = minute_left.Content.ToString();
            int minutes;
            int seconds;
            int.TryParse(minutesLeft.Substring(0, minutesLeft.IndexOf(':')), out minutes);
            int.TryParse(minutesLeft.Substring(minutesLeft.IndexOf(':') + 1), out seconds);
            seconds = (minutes * 60) + seconds;
            if (seconds > 0)
            {
                minute_left.Content = minutesAndSeconds(seconds - 1);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Get up! Do you want to restart the timer?", "StandUp!", MessageBoxButton.YesNo);
                switch(result)
                {
                    case MessageBoxResult.Yes:
                        minute_left.Content = minute_in.Text + ":00";
                        break;
                    case MessageBoxResult.No:
                        this.Close();
                        break;
                }
            }
            
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            int minutes;
            int.TryParse(minute_in.Text, out minutes);
            countDown(minutes);
        }
    }
}
