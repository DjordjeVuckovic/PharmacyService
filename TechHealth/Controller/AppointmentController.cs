using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using TechHealth.Model;
using TechHealth.Service;
using TechHealth.Service.IService;

namespace TechHealth.Controller
{
   public class AppointmentController
   {
      
      private readonly IAppointmentService appointmentService = new AppointmentService();

        public void Postpone(Appointment appointment) => appointmentService.Postpone(appointment);
        public Appointment GetById(string idAppointment) => appointmentService.GetById(idAppointment);

        public ObservableCollection<Appointment> GetAll() => new ObservableCollection<Appointment>(appointmentService.GetAll());

        public void Create(Appointment appointment) => appointmentService.Create(appointment);

        public void Update(Appointment appointment) => appointmentService.Update(appointment);

        public bool Delete(string idAppointment) => appointmentService.Delete(idAppointment);
        public List<Appointment> GetByDoctorId(string doctorId) => appointmentService.GetByDoctorId(doctorId);
        public List<Appointment> GetAllNotEvidentByDoctorId(string doctorId) => appointmentService.GetAllNotEvidentByDoctorId(doctorId);

        public List<Appointment> GetAllNotEvident() => appointmentService.GetAllNotEvident();

        public List<Appointment> GetAllEvident() => appointmentService.GetAllEvident();
   }
}