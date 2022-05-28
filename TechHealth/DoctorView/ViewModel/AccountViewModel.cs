﻿using MaterialDesignThemes.Wpf;
using TechHealth.Controller;
using TechHealth.Core;
using TechHealth.DoctorView.View;
using TechHealth.DoctorView.Windows;
using TechHealth.Model;

namespace TechHealth.DoctorView.ViewModel
{
    public class AccountViewModel
    {
        private readonly DoctorController doctorController = new DoctorController();
        private Doctor Doctor { get; set; }
        public RelayCommand VacationCommand { get; set; }
        public RelayCommand VacationHistoryCommand { get; set; }
        public AccountViewModel(string doctorId)
        {
            Doctor = doctorController.GetById(doctorId);
            VacationCommand = new RelayCommand(param => ExecuteVacation());
            VacationHistoryCommand = new RelayCommand(param => ExecuteVacationHistory());
        }

        private void ExecuteVacation()
        {
            var vm = new DoctorVacationViewModel(Doctor);
            var vacationView = new DoctorVacationWindow()
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => vacationView.Close();
            vacationView.ShowDialog();
        }

        private void ExecuteVacationHistory()
        {
            var vm = new VacationHistoryViewModel(Doctor);
            MainViewModel.GetInstance().CurrentView = vm;
        }
        
    }
}