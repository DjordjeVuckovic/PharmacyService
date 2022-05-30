﻿using System.Collections.Generic;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.Service
{
    public class AnamnesisService
    {
        public Anamnesis GetByAppointmentId(string appointmentId)
        {
          var anamnesis = AnamnesisRepository.Instance.GetByAppointmentId(appointmentId);
          BindDataForAnamnesis(anamnesis);
          return anamnesis;
        }

        private void BindDataForAnamnesis(Anamnesis anamneses)
        {
            
            anamneses.Appointment = AppointmentRepository.Instance.GetById(anamneses.Appointment.IdAppointment);
            
        }

        public List<Anamnesis> GetAll()
        {
            var anamneses = AnamnesisRepository.Instance.GetAllToList();
            BindDataForAnamneses(anamneses);
            return anamneses;
        }

        public bool Create(Anamnesis anamnesis)
        {
            return AnamnesisRepository.Instance.Create(anamnesis);
        }
        public bool Update(Anamnesis anamnesis)
        {
            return AnamnesisRepository.Instance.Update(anamnesis);
        }

        public List<Anamnesis> GetAllAnamnesisSurgeriesByPatient(string patientId)
        {
            var temp = new List<Anamnesis>();
            foreach (var an in GetAll() )
            {
                if (an.Appointment != null && an.Appointment.AppointmentType == AppointmentType.operation && an.Appointment.Patient.Jmbg.Equals(patientId))
                {
                    temp.Add(an);
                    BindDataForAppointment(an.Appointment);
                }
            }
            return temp;
        }
        public List<Anamnesis> GetAllAnamnesisExaminationsByPatient(string patientId)
        {
            var temp = new List<Anamnesis>();
            foreach (var an in GetAll())
            {
                if (an.Appointment != null && an.Appointment.AppointmentType == AppointmentType.examination && an.Appointment.Patient.Jmbg.Equals(patientId))
                {
                    temp.Add(an);
                    BindDataForAppointment(an.Appointment);
                }
            }
            return temp;
        }
        private void BindDataForAnamneses(List<Anamnesis> anamneses)
        {
            foreach (var an in anamneses)
            {
                an.Appointment = AppointmentRepository.Instance.GetById(an.Appointment.IdAppointment);
            }

        }

        private void BindDataForAppointment(Appointment appointment)
        {
            appointment.Doctor = DoctorRepository.Instance.GetDoctorbyId(appointment.Doctor.Jmbg);
        }
    }
}