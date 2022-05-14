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
    public partial class GuestsView : Window
    {
        private ObservableCollection<Patient> guests = new ObservableCollection<Patient>();
        private PatientController patientController = new PatientController();
        public GuestsView()
        {
            InitializeComponent();
            foreach (var p in PatientRepository.Instance.GetAll().Values)
            {
                if (p.Guest)
                {
                    guests.Add(p);
                }
            }
            guestList.ItemsSource = guests;
        }

        private void Button_Click_Add_Guest(object sender, RoutedEventArgs e)
        {
            Hide();
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
            Hide();
            new UpdateGuest(patient).ShowDialog();
            Update();
        }
        public void Update()
        {
            guests.Clear();
            foreach (var r in PatientRepository.Instance.GetAll().Values)
            {
                if (r.Guest)
                {
                    guests.Add(r);
                }
            }
        }
    }
}
