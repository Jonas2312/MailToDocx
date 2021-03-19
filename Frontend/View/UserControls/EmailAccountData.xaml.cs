using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Text.RegularExpressions;
using Frontend.ViewModel;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für EmailAccountData.xaml
    /// </summary>
    public partial class EmailAccountData : UserControl
    {
        UserDataViewModel UserDataViewModel;
        public EmailAccountData()
        {
            InitializeComponent();
            Loaded += Init;
        }

        public void Init(object sender, RoutedEventArgs e)
        {
            UserDataViewModel = (UserDataViewModel)DataContext;
        }

    }
}
