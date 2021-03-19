using Frontend.ViewModel;
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

namespace Frontend.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für FolderData.xaml
    /// </summary>
    public partial class FolderData : UserControl
    {
        UserDataViewModel UserDataViewModel;
        public FolderData()
        {
            InitializeComponent();
            Loaded += Init;
        }

        public void Init(object sender, RoutedEventArgs e)
        {
            UserDataViewModel = (UserDataViewModel)DataContext;
        }

        private void PdfFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                UserDataViewModel.PdfFolder = dialog.SelectedPath;
            }
        }

        private void ImageFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                UserDataViewModel.JpgFolder = dialog.SelectedPath;
            }
        }

        private void DocxFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                UserDataViewModel.DocxFolder = dialog.SelectedPath;
            }
        }
    }
}
