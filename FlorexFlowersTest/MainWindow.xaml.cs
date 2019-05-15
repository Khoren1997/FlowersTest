using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
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
using System.Threading;
using FlorexFlowersTest.AllPages;
using System.Windows.Threading;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml.Linq;

namespace FlorexFlowersTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public static void GetIPAddress()
        {
            
            var path = @"../../EntredIpAddresses/IpAddresses.txt";
            StreamWriter streamWriter = new StreamWriter(path);
            IPAddress[] hostAddresses = Dns.GetHostAddresses("");
            foreach (IPAddress hostAddress in hostAddresses)
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(hostAddress) &&  // ignore loopback addresses
                    !hostAddress.ToString().StartsWith("169.254."))// ignore link-local addresses
                {
                    streamWriter.Write(hostAddress.ToString() + "\r\n");
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
        }

        private void ShowWelcomeMessage(object sender, RoutedEventArgs e)
        {
            if (text.Text != "" && text.Text != " ")
            {
                GetIPAddress();
                WelcomeText.Text = $"Hello dear " + text.Text + ". We want to introduce you to our rules. If you answer correctly 9 questions out of 10, you will receive a 10% discount.";
                WelcomeText.Visibility = Visibility.Visible;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5d);
                timer.Tick += TimerTick;
                timer.Start();
            }
            else
            {
                MessageBox.Show("The text box can't be empty!");
            }

        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            StartTest.Visibility = Visibility.Visible;
        }
        //private void RadioButtonChecked(object sender, RoutedEventArgs e)
        //{
        //    var radioButton = sender as RadioButton;
        //    if (radioButton == null)
        //        return;
        //    var intIndex =radioButton.Content.ToString();
        //    MessageBox.Show(intIndex);
        //}
        public void StartQuiz(object sender, RoutedEventArgs e)
        {
            Page_1 page_1 = new Page_1();
            page_1.Show();
            mw.Close();
        }
    }
}
