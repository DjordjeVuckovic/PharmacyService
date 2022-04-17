﻿using TechHealth.Core;
using TechHealth.DoctorView.View;

namespace TechHealth.DoctorView.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private object _currentView;
        public DashBoardView DashBoardViewModel { get; set; }
        public RecordView RecordView { get; set; }
        public RelayCommand DashCommand { get; set; }
        public RelayCommand RecordCommand { get; set; }
        public RelayCommand AppointmentCommand { get; set; }
        public string DoctorId { get; set; }
        public AppointmentsView AppointmentsView { get; set; }
        public PatientView PatientsView { get; set; }
        public RelayCommand PateintCommand { get; set; }
        
        
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        /*
        public MainViewModel()
        {
            DashBoardViewModel = new DashBoardView();
            //RecordView = new RecordView();
            //AppointmentsWindow = AppointmentsWindow.GetInstance();

            CurrentView = DashBoardViewModel;
            DashCommand = new RelayCommand(o =>
                {
                    CurrentView = DashBoardViewModel;
                }
            );
            RecordCommand = new RelayCommand(o =>
                {
                    CurrentView = RecordView;
                }
            );
            AppointmentCommand = new RelayCommand(o =>
                {
                    //
                }
            );
        }*/
        private static MainViewModel _instance;

        public static MainViewModel GetInstance()
        {
            return _instance;
        }

        public MainViewModel(string doctorJmbg)
        {
            _instance = this;
            DoctorId = doctorJmbg;
            DashBoardViewModel = new DashBoardView();
            RecordView = new RecordView(doctorJmbg);
            AppointmentsView = new AppointmentsView(doctorJmbg);
            PatientsView = new PatientView();

            CurrentView = DashBoardViewModel;
            DashCommand = new RelayCommand(o =>
                {
                    CurrentView = DashBoardViewModel;
                }
            );
            RecordCommand = new RelayCommand(o =>
                {
                    CurrentView = RecordView;
                }
            );
            AppointmentCommand = new RelayCommand(o =>
                {
                    CurrentView = AppointmentsView;
                }
            );
            PateintCommand = new RelayCommand(o =>
                {
                    CurrentView = PatientsView;
                }
            );
           
            
            
        }

        protected MainViewModel()
        {
            
        }
    }
}