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
using TechHealth.Model;
using TechHealth.Repository;
using TechHealth.Controller;
using System.Collections.ObjectModel;

namespace TechHealth.View.SecretaryView
{
    public partial class NotificationReason : Window
    {
        private SecretaryNotificationController secretaryNotificationController = new SecretaryNotificationController();
        private DoctorVacationRequest doctorVacationRequest;
        private SecretaryNotification secretaryNotification = new SecretaryNotification();
        public NotificationReason(DoctorVacationRequest dVR)
        {
            InitializeComponent();
            doctorVacationRequest = dVR;
        }
        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            GenerateNotification();
            secretaryNotificationController.Create(secretaryNotification);
            new VacationRequestsView().Show();
            Close();
        }
        private void GenerateNotification()
        {
            secretaryNotification.Id = Guid.NewGuid().ToString("N");
            secretaryNotification.Person = doctorVacationRequest.Doctor;
            secretaryNotification.NotificationText = "Vacation request from " + doctorVacationRequest.StartDate.ToString("dd.MM.yyyy") + " to " + doctorVacationRequest.FinishDate.ToString("dd.MM.yyyy") + " is " + doctorVacationRequest.VacationStatus.ToString().ToLower() + ".";
            secretaryNotification.NotificationText += System.Environment.NewLine + reason.Text;
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
            Close();
        }
    }
}
