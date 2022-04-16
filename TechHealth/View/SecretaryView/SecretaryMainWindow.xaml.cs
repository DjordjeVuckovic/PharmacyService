﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TechHealth.View.SecretaryView
{
    public partial class SecretaryMainWindow : Window
    {
        private ObservableCollection<Patient> users = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> guests = new ObservableCollection<Patient>();
        private PatientController patientController = new PatientController();
        public SecretaryMainWindow()
        {
            InitializeComponent();
            foreach(var p in PatientRepository.Instance.GetAll().Values)
            {
                if (!p.Guest)
                {
                    users.Add(p);
                }
                else
                {
                    guests.Add(p);
                }
            }
            accountList.ItemsSource = users;
            guestList.ItemsSource = guests;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            new AddPatient().ShowDialog();
            Update();
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (accountList.SelectedIndex == -1)
            {
                MessageBox.Show("You didn't select an account.");
                return;
            }
            Patient p = (Patient)accountList.SelectedItems[0];
            if(!p.Guest)
            {
                patientController.Delete(p.Jmbg);
                users = new ObservableCollection<Patient>(PatientRepository.Instance.GetAll().Values);
                accountList.ItemsSource = users;
            }
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (accountList.SelectedIndex == -1)
            {
                MessageBox.Show("You didn't select an account.");
                return;
            }
            Patient patient = (Patient)accountList.SelectedItems[0];
            new UpdatePatient(patient).ShowDialog();
            Update();
        }
        private void Button_Click_Add_Guest(object sender, RoutedEventArgs e)
        {
            new AddGuest().ShowDialog();
            Update();
        }
        private void Button_Click_Delete_Guest(object sender, RoutedEventArgs e)
        {
            if (guestList.SelectedIndex == -1)
            {
                MessageBox.Show("You didn't select a guest.");
                return;
            }
            Patient p = (Patient)guestList.SelectedItems[0];
            if (p.Guest)
            {
                patientController.Delete(p.Jmbg);
                Update();
            }
        }
        private void Button_Click_Edit_Guest(object sender, RoutedEventArgs e)
        {
            if (guestList.SelectedIndex == -1)
            {
                MessageBox.Show("You didn't select a guest.");
                return;
            }
            Patient patient = (Patient)guestList.SelectedItems[0];
            new UpdateGuest(patient).ShowDialog();
            Update();
        }
        public void Update()
        {
            users.Clear();
            guests.Clear();
            foreach (var r in PatientRepository.Instance.GetAll().Values)
            {
                if (!r.Guest)
                {
                    users.Add(r);
                }
                else
                {
                    guests.Add(r);
                }
            }
        }
        private void accountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
