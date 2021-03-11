using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Text.RegularExpressions;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für EmailAccountData.xaml
    /// </summary>
    public partial class EmailAccountData : UserControl
    {
        MainWindow MainWindow;
        public EmailAccountData()
        {
            InitializeComponent();
        }

        public void Init(MainWindow mainWindow)
        {
            MainWindow = mainWindow;

            MailServer.Text = MainWindow.hostname;
            Port.Text = MainWindow.port.ToString();
            AccountName.Text = MainWindow.username;
            AccountPassword.Text = MainWindow.password;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MailServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.hostname = MailServer.Text;
        }

        private void Port_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.port = Int32.Parse(Port.Text);
        }

        private void AccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.username = AccountName.Text;
        }

        private void AccountPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.password = AccountPassword.Text;
        }

    }
}
