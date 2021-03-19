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
using System.Threading;
using System.Windows.Threading;
using Frontend.ViewModel;
using Converter;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t1;

        UserDataViewModel UserDataViewModel;

        MailDataWindow MailDataWindow;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            StopButton.Visibility = Visibility.Collapsed;
            IsRunning.Visibility = Visibility.Collapsed;

            UserDataViewModel = new UserDataViewModel();
            DataContext = UserDataViewModel;

            //  DispatcherTimer setup
            DispatcherTimer dispatcherTimer;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(updateStatus);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }


        public void updateStatus(object sender, EventArgs e)
        {
            debug = ShowDebug.IsChecked.Value;

            UserDataViewModel.UserData.Save();
            if(Status.Text != status)
            {
                while (status.Split('\n').Length > 5)
                {
                    status = status.Substring(status.IndexOf("\n") + 1);
                }
                    

                status += " [" + DateTime.Now + "] \n";
                Status.Text = status;
            }
        }

        string status = String.Empty;
        bool debug = false;
        public void RunPipeline()
        {
            while(true)
            {
                try
                {
                    UserDataViewModel.RunPipeline();
                    status += "Received new mail: " + Pipeline.name;
                }
                catch(Exception e)
                {
                    if(debug)
                        status += e.Message;
                }
            }
        }
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            t1 = new Thread(RunPipeline);
            t1.Start();
            StartButton.Visibility = Visibility.Collapsed;
            EmailAccount.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
            IsRunning.Visibility = Visibility.Visible;

            try
            {
                if(MailDataWindow != null)
                    MailDataWindow.Close();
            }
            catch
            {

            }
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            t1.Abort();
            StartButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Collapsed;
            IsRunning.Visibility = Visibility.Collapsed;
            EmailAccount.Visibility = Visibility.Visible;
        }

        private void EmailAccount_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MailDataWindow != null)
                    MailDataWindow.Close();
            }
            catch
            {

            }
            MailDataWindow = new MailDataWindow();
            MailDataWindow.DataContext = UserDataViewModel;
            MailDataWindow.Show();
        }
        

    }
}
