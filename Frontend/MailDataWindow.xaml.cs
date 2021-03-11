﻿using System;
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
using System.Windows.Shapes;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MailDataWindow.xaml
    /// </summary>
    public partial class MailDataWindow : Window
    {
        public MailDataWindow()
        {
            InitializeComponent();
        }

        public void Init(MainWindow mainWindow)
        {
            EmailAccountData.Init(mainWindow);
        }
    }
}
