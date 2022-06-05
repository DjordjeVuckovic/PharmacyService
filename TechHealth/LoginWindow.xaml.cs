﻿using System;
using System.Linq;
using System.Windows;
using TechHealth.DoctorView;
using TechHealth.Model;
using TechHealth.Repository;
using TechHealth.View.PatientView;
using TechHealth.View.SecretaryView;

namespace TechHealth
{
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
        }

        

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(!Log())
            {
                MessageBox.Show("Wrong credential.Try again!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private bool Log()
        {
            string user = Username.Text;
            string pass = Password.Password;
            bool successLogin=false;
            Doctor doctor = DoctorRepository.Instance.GetDoctorByUser(user);
            if (doctor != null && pass.Equals(doctor.Password))
            {
                new DoctorWindow(doctor.Jmbg).Show();
                successLogin = true;
                Close();
            }

            Secretary secretary = SecretaryRepository.Instance.GetSecretaryByUser(user);
            if (secretary != null && pass.Equals(secretary.Password))
            {
                new SecretaryMainWindow().Show();
                successLogin = true;
                Close();
            }

            Patient patient = PatientRepository.Instance.GetPatientByUser(user);
            if (patient != null && pass.Equals(patient.Password))
            {
                new PatientMainWindow().Show();
                successLogin = true;
                Close();
            }

            return successLogin;

        }


    }
}