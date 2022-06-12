﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechHealth.Annotations;
using TechHealth.Controller;
using TechHealth.Core;
using TechHealth.Model;
using TechHealth.Repository;
using TechHealth.View.PatientView.ViewModel;

namespace TechHealth.View.PatientView.View
{
    /// <summary>
    /// Interaction logic for AppointmentHistory.xaml
    /// </summary>
    public partial class AppointmentHistory : UserControl, INotifyPropertyChanged
    {
        private Patient patient;
        public Appointment selected;
        public AppointmentController appController = new AppointmentController();
        public GradeController gradeController = new GradeController();
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand GradeAppointmentCommand { get; set; }
        public RelayCommand DetailsCommand { get; set; }
        public RelayCommand NoteCommand { get; set; }
        
        public Appointment GetSelected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Appointment> Past { get; set; }

        public AppointmentHistory()
        {
            InitializeComponent();
            DataContext = this;
            Past = new ObservableCollection<Appointment>(appController.GetAllEvident());
            LoadDoctors();
            GradeAppointmentCommand = new RelayCommand(param => ExecuteGrade(), param => CanExecuteGrade());
            DetailsCommand = new RelayCommand(param => ExecuteDetail(), param => CanExecuteDetails());
            NoteCommand = new RelayCommand(param => ExecuteNote(), param => CanExecuteNote());
        }

        private bool CanExecuteNote()
        {
            return selected != null;
        }

        private void ExecuteNote()
        {
            new Notes(GetSelected).ShowDialog();
        }

        private bool CanExecuteDetails()
        {
            return selected != null;
        }

        private void ExecuteDetail()
        {
            new AppointmentDetails(GetSelected).ShowDialog();
        }

        private bool CanExecuteGrade()
        {
            if (selected == null || selected.Graded == true) //|| gradeController.IsGraded(GetSelected))
            {
                return false;
            }
            return true;
        }

        private void ExecuteGrade()
        {
            new RateAppointment(GetSelected).ShowDialog();
        }

        private void LoadDoctors()
        {
            for (int i = 0; i < Past.Count; i++)
            {
                Past[i].Doctor = DoctorRepository.Instance.GetDoctorById(Past[i].Doctor.Jmbg);
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Notes(GetSelected).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AppointmentDetails(GetSelected).ShowDialog();
        }
    }
}
