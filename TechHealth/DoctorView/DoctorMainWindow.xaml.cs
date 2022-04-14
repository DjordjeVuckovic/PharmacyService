﻿using System.Collections.ObjectModel;
using System.Windows;
using TechHealth.DoctorView.CRUDAppointments;
using TechHealth.DoctorView.ViewModel;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.DoctorView
{
    public partial class DoctorMainWindow : Window
    {
        private static DoctorMainWindow _instance;
        private static string doctorId;
        private ObservableCollection<Appointment> _appointments;
        public static DoctorMainWindow GetInstance(string id)
        {
                
                if (_instance == null)
                {
                     doctorId = id;
                    _instance = new DoctorMainWindow();
                }
                return _instance;
        }
        public static DoctorMainWindow GetInstance()
        {
            return _instance;
        }
        private DoctorMainWindow()
        {
            InitializeComponent();
            _instance = this;
            DataContext = this;
        }
        public ObservableCollection<Appointment> Appointments =>
            _appointments ?? (_appointments =
                new ObservableCollection<Appointment>(AppointmentRepository.Instance.GetByDoctorId(doctorId)));
        private void Examination_OnClick(object sender, RoutedEventArgs e)
        {
            new CreateExamination(doctorId).ShowDialog();
        }
       

        private void Surgery_OnClick(object sender, RoutedEventArgs e)
        {
            new CreateSurgery(doctorId).ShowDialog();
        }

        private void UpdateExamination_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataAppointment.SelectedIndex != -1)
            {
                AppointmentsUpdate updateAppointment = new AppointmentsUpdate((Appointment) dataAppointment.SelectedItem);
                updateAppointment.ShowDialog();
            }
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataAppointment.SelectedIndex != -1)
            {
                Appointment appointment = (Appointment) dataAppointment.SelectedItem;
                AppointmentRepository.Instance.Delete(appointment.IdAppointment);
                Appointments.Remove(appointment);
                MessageBox.Show("You are successfully deleted an appointment");
            }
        }
    }
}