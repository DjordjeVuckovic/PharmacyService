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
    public partial class AddAppointmentSecretary : Window
    {
        private List<Doctor> doctors = DoctorRepository.Instance.GetAllToList();
        private List<Patient> patients = PatientRepository.Instance.GetAllToList();
        private List<Room> rooms = RoomRepository.Instance.GetAllToList();
        private AppointmentController appointmentController = new AppointmentController();
        private AppointmentType t1;
        public AddAppointmentSecretary(AppointmentType t)
        {
            InitializeComponent();
            t1 = t;
            if (t.Equals(AppointmentType.examination))
            {
                addLabel.Content = "Add Examination";
            }
            else
            {
                addLabel.Content = "Add Operation";
            }
            List<String> doctorsForCombo = new List<String>();
            foreach (var d in doctors)
            {
                doctorsForCombo.Add(d.FullSpecialization);
            }
            List<String> patientsForCombo = new List<String>();
            foreach (var p in patients)
            {
                patientsForCombo.Add(p.FullName);
            }
            List<String> roomsForCombo = new List<String>();
            foreach (var r in rooms)
            {
                roomsForCombo.Add(r.roomId);
            }
            doctorCombo.ItemsSource = doctorsForCombo;
            patientCombo.ItemsSource = patientsForCombo;
            roomCombo.ItemsSource = roomsForCombo;
        }
        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Doctor dr = new Doctor();
            foreach (var d in doctors)
            {
                if (doctorCombo.SelectedItem.Equals(d.FullSpecialization))
                {
                    dr = d;
                }
            }
            Patient pa = new Patient();
            foreach (var p in patients)
            {
                if (patientCombo.SelectedItem.Equals(p.FullName))
                {
                    pa = p;
                }
            }
            Appointment a = new Appointment()
            {
                AppointmentType = t1,
                Date = DateTime.Parse(datePick.Text),
                Doctor = dr,
                Emergent = false,
                FinishTime = "",
                StartTime = "",
                IdAppointment = Guid.NewGuid().ToString("N"),
                Patient = pa,
                Room = rooms[roomCombo.SelectedIndex],
                ShouldSerialize = true
            };
            appointmentController.Create(a);
            Close();
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}