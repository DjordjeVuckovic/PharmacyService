﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Scar.Common.WPF.Controls;
using TechHealth.Model;
using TechHealth.Repository;
using TechHealth.View.ManagerView;
using TechHealth.View.SecretaryView;


namespace TechHealth
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
            
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new SecretaryMainWindow().Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new ManagerMainWindow().Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            new LoginWindow().Show();
            Close();

        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            

            InitializeComponent();
        }
    }
}
