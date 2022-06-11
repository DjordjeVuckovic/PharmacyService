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
using TechHealth.Controller;
using TechHealth.Repository;
using TechHealth.Model;

namespace TechHealth.View.SecretaryView
{
    public partial class AddEquipmentRequest : Window
    {
        public AddEquipmentRequest()
        {
            InitializeComponent();
        }
        private void Button_LogOut(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
        private void Button_Meetings(object sender, RoutedEventArgs e)
        {
            new MeetingsPickDate().Show();
            Close();
        }
        private void Button_Accounts(object sender, RoutedEventArgs e)
        {
            new AccountsView().Show();
            Close();
        }
        private void Button_Appointments(object sender, RoutedEventArgs e)
        {
            new AppointmentsPickDate().Show();
            Close();
        }
        private void Button_Equipment(object sender, RoutedEventArgs e)
        {
            new EquipmentRequestsView().Show();
            Close();
        }
        private void Button_EmergencyExamination(object sender, RoutedEventArgs e)
        {
            new EmergencyExamination().Show();
            Close();
        }
        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            new SecretaryMainWindow().Show();
            this.Close();
        }
        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            if (equipmentName.Text == "" || quantity.Text == "")
            {
                MessageBox.Show("Fill out all the fields.");
                return;
            }
            try
            {
                int q = Int32.Parse(quantity.Text);
            }
            catch
            {
                MessageBox.Show("Quantity must have only numbers.");
                return;
            }
            EquipmentRequest er = new EquipmentRequest(Guid.NewGuid().ToString("N"), equipmentName.Text, Int32.Parse(quantity.Text));
            EquipmentRequestRepository.Instance.Create(er);
            new AddEquipmentRequest().Show();
            this.Close();
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            new EquipmentRequestsView().Show();
            this.Close();
        }
    }
}
