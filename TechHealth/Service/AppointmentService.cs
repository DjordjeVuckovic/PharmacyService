// File:    AppointmentService.cs
// Author:  nsred
// Created: Thursday, April 7, 2022 6:13:48 PM
// Purpose: Definition of Class AppointmentService

using System;
using System.Collections.Generic;
using TechHealth.Exceptions;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.Service
{
   public class AppointmentService
   {
      
      private PatientService patientService = new PatientService();
      private DoctorService doctorService = new DoctorService();
      private RoomService roomService = new RoomService();
      public Appointment GetById(string idAppointment)
      {
         return AppointmentRepository.Instance.GetById(idAppointment);
      }
      
      public List<Appointment> GetAll()
      {
         var temp = AppointmentRepository.Instance.GetAllToList();
         BindDataForAppointments(temp);
         return temp;
      }

      public List<Appointment> GetAllFuture(DateTime startDate, Doctor doctor,Patient patient)
      {
         List<Appointment> ret = new List<Appointment>();
         
         DateTime myStart = startDate;
         DateTime startTime = startDate;
         DateTime endTime = startDate.AddMinutes(30);
         TimeSpan duration = new TimeSpan(0, 0, 30, 0);
         TimeSpan startduration = new TimeSpan(0, 8, 0, 0);
         TimeSpan finishduration = new TimeSpan(0, 24, 0, 0);
         DateTime endDate = myStart.AddDays(2);
         while (startDate < endDate)
         {
            while(startduration <= finishduration)
            {
               Appointment appointment = new Appointment
               {
                  Date = startDate,
                  Emergent = false,
                  IdAppointment = null,
                  Room = RoomRepository.Instance.GetById("S2"),
                  Patient = patient,
                  AppointmentType = AppointmentType.examination,
                  Doctor = doctor,
                  Evident = false,
                  StartTimeD = startTime,
                  FinishTimeD = endTime
               };
               if (CheckAvailabilityForFuture(appointment))
               {
                  ret.Add(appointment);
               }
               startduration= startduration.Add(duration);
               startTime = startTime.Add(duration);
               endTime = endTime.Add(duration);
            }
            startDate =startDate.AddDays(1);
            startduration=new TimeSpan(0, 8, 0, 0);
            finishduration = new TimeSpan(0, 24, 0, 0);
         }
         return ret;
      }

      public bool Create(Appointment appointment)
      {
         CheckAvailability(appointment);
         return AppointmentRepository.Instance.Create(appointment);
      }
      
      public bool Update(Appointment appointment)
      {
            return AppointmentRepository.Instance.Update(appointment);
        }
      
      public bool Delete(string idAppointment)
      {
            return AppointmentRepository.Instance.Delete(idAppointment);
        }
      
      public List<Appointment> GetByDoctorId(string doctorId)
      {
         var temp = AppointmentRepository.Instance.GetByDoctorId(doctorId);
         BindDataForAppointments(temp);
         return temp;
      }
      
      public List<Appointment> GetByPatientId(string patientId)
      {
            var temp = AppointmentRepository.Instance.GetByPatientId(patientId);
            BindDataForAppointments(temp);
            return temp;
        }

        public List<Appointment> GetByRoomId(string roomId)
      {
         throw new NotImplementedException();
      }

      private void BindDataForAppointments(List<Appointment> appointments)
      {
         foreach (var appointment in appointments)
         {
            appointment.Doctor = DoctorRepository.Instance.GetDoctorbyId(appointment.Doctor.Jmbg);
         }

         foreach (var appointment in appointments)
         {
            appointment.Patient = PatientRepository.Instance.GetPatientbyId(appointment.Patient.Jmbg);
         }
         
      }

      private void CheckAvailability(Appointment appointment)
      {
         foreach (var existingAppointment in AppointmentRepository.Instance.GetAllToList())
         {
            if (existingAppointment.DoctorConflicts(appointment))
            {
               throw new AppointmentConflictException(existingAppointment,appointment);
            }
            
         }
         
      }
      private bool CheckAvailabilityForFuture(Appointment appointment)
      {
         bool ret = true;
         foreach (var existingAppointment in AppointmentRepository.Instance.GetAllToList())
         {
            if (existingAppointment.DoctorConflicts(appointment))
            {
               ret = false;
            }
            
         }
         return ret;
      }
   
   }
}