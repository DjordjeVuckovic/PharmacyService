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
      public List<Appointment> GetAllNotEvidentByDoctorId(string doctorId)
      {
         var temp =  new List<Appointment>(GetByDoctorId(doctorId));
         var temp1 = new List<Appointment>();
         foreach (var vAppointment in temp)
         {
            if (!vAppointment.Evident)
            {
               temp1.Add(vAppointment);
            }

         }
         return temp1;
      }
        public void Postpone(Appointment appointment)
        {
            for (int daysPostponed = 1; daysPostponed <= 5; daysPostponed++)
            {
                appointment.Date = appointment.Date.AddDays(1);
                appointment.StartTime = appointment.StartTime.AddDays(1);
                appointment.FinishTime = appointment.FinishTime.AddDays(1);
                if (AppointmentRepository.Instance.CanPostpone(appointment)) break;
            }
            AppointmentRepository.Instance.Update(appointment);
        }

        public List<Appointment> GetAllNotEvident()
      {
         var notEvidentAppointments = new List<Appointment>();
         foreach (var vAppointment in GetAll())
         {
            if (!vAppointment.Evident)
            {
               notEvidentAppointments.Add(vAppointment);
            }

         }
         return notEvidentAppointments;
      }

        public List<Appointment> GetAllEvident()
        {
            var evidentAppointments = new List<Appointment>();
            foreach (var vAppointment in GetAll())
            {
                if (vAppointment.Evident)
                {
                    evidentAppointments.Add(vAppointment);
                }

            }
            return evidentAppointments;
        }

        public List<Appointment> GetAllFuture(DateTime startDateRegion, DateTime finishDateRegion, Doctor doctor,Patient patient, Room room)
      {
         List<Appointment> ret = new List<Appointment>();

            DateTime startTime = startDateRegion;
            DateTime endTime = startDateRegion.AddMinutes(30);
            TimeSpan duration = new TimeSpan(0, 0, 30, 0);
            TimeSpan startduration = new TimeSpan(0, 8, 0, 0);
            TimeSpan finishduration = new TimeSpan(0, 24, 0, 0);
            DateTime endDate = finishDateRegion;
            while (startDateRegion <= endDate) // <endDate
            {
                while (startduration <= finishduration)
                {
                    var appointment = AppointmentSuggestedAppointment(startDateRegion, doctor, patient, room, startTime, endTime);
                    if (CheckAvailabilityForFuture(appointment))
                    {
                        ret.Add(appointment);
                    }
                    startduration = startduration.Add(duration);
                    startTime = startTime.Add(duration);
                    endTime = endTime.Add(duration);
                }
                startDateRegion = startDateRegion.AddDays(1);
                startduration = new TimeSpan(0, 8, 0, 0);
                finishduration = new TimeSpan(0, 24, 0, 0);
            }
            return ret;
        }

        private static Appointment AppointmentSuggestedAppointment(DateTime startDateRegion, Doctor doctor, Patient patient,
           Room room, DateTime startTime, DateTime endTime)
        {
           Appointment appointment = new Appointment
           {
              Date = startDateRegion,
              Emergent = false,
              IdAppointment = null,
              //Room = RoomRepository.Instance.GetById("S2"),
              Room = room,
              Patient = patient,
              AppointmentType = AppointmentType.examination,
              Doctor = doctor,
              Evident = false,
              StartTime = startTime,
              FinishTime = endTime
           };
           return appointment;
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
            var temp = AppointmentRepository.Instance.GetByRoomId(roomId);
            BindDataForAppointments(temp);
            return temp;
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

         foreach (var appointment in appointments)
         {
            appointment.Room = RoomRepository.Instance.GetRoombyId(appointment.Room.roomId);
         }

      }

      private void CheckAvailability(Appointment appointment)
      {
         foreach (var existingAppointment in GetAllNotEvident())
         {
            IsDoctorConflict(appointment, existingAppointment);
            IsPatientConflict(appointment, existingAppointment);
            IsRoomConflict(appointment, existingAppointment);
         }
         
      }

      private static void IsRoomConflict(Appointment appointment, Appointment existingAppointment)
      {
         if (existingAppointment.RoomConflicts(appointment))
         {
            throw new AppointmentRoomConflictException(existingAppointment, appointment);
         }
      }

      private static void IsPatientConflict(Appointment appointment, Appointment existingAppointment)
      {
         if (existingAppointment.PatientConflicts(appointment))
         {
            throw new AppointmentPatientConflictException(existingAppointment, appointment);
         }
      }

      private static void IsDoctorConflict(Appointment appointment, Appointment existingAppointment)
      {
         if (existingAppointment.DoctorConflicts(appointment))
         {
            throw new AppointmentConflictException(existingAppointment, appointment);
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