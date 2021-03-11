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
using MailToDocx;
using System.Windows.Threading;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t1;
        Thread StatusUpdate;

        string pdf;
        string jpg;
        string docx;


        public string hostname;
        public int port;
        public string username;
        public string password;

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

            PdfFolder.Text = "C:\\Users\\Ile\\Desktop\\EDZTESTFOLDER\\Pdf"; ;
            ImageFolder.Text = "C:\\Users\\Ile\\Desktop\\EDZTESTFOLDER\\jpg";
            DocxFolder.Text = "C:\\Users\\Ile\\Desktop\\EDZTESTFOLDER\\docx";

            hostname = "mail.your-server.de";
            port = 993;
            username = "orga@enddarm-zentrum.de";
            password = "W444mZMuu6a8b12c";

            pdf = PdfFolder.Text;
            jpg = ImageFolder.Text;
            docx = DocxFolder.Text;

            //  DispatcherTimer setup
            DispatcherTimer dispatcherTimer;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(updateStatus);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }


        public void updateStatus(object sender, EventArgs e)
        {
            if(Pipeline.name.Length > 0)
            {
                if (Pipeline.name.Contains("ERROR"))
                {
                    Status.Text = Pipeline.name;

                    //var converter = new System.Windows.Media.BrushConverter();
                    //var brush = (Brush)converter.ConvertFromString("#00FF0090");
                    //Status.Foreground = brush;
                }
                else
                {                    
                    Status.Text = "Received new mail: " + Pipeline.name;

                    //var converter = new System.Windows.Media.BrushConverter();
                    //var brush = (Brush)converter.ConvertFromString("#00000090");
                    //Status.Foreground = brush;
                }                
            }
        }

        public void runPipeline()
        {

            Pipeline.f(pdf, jpg, docx, hostname, port, username, password);
        }


        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            t1 = new Thread(runPipeline);
            t1.Start();
            StartButton.Visibility = Visibility.Collapsed;
            EmailAccount.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
            IsRunning.Visibility = Visibility.Visible;

            try
            {
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
            MailDataWindow.Init(this);
            MailDataWindow.Show();
        }
        

        private void PdfFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            pdf = PdfFolder.Text;
        }

        private void ImageFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            jpg = ImageFolder.Text;
        }

        private void DocxFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            docx = DocxFolder.Text;
        }


        private void PdfFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                PdfFolder.Text = dialog.SelectedPath;
            }
        }

        private void ImageFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                ImageFolder.Text = dialog.SelectedPath;
            }
        }

        private void DocxFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                DocxFolder.Text = dialog.SelectedPath;
            }
        }
    }
}
