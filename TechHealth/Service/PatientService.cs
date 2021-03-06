// File:    PatientService.cs
// Author:  djord
// Created: Thursday, April 7, 2022 6:18:02 PM
// Purpose: Definition of Class PatientService

using System;
using System.Collections.Generic;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.Service
{
   public class PatientService
   {

        public Patient GetById(string patientId) => PatientRepository.Instance.GetPatientbyId(patientId);

        public List<Patient> GetAll()
      {
         return PatientRepository.Instance.GetAllToList();
      }

        public bool Create(Patient patient)
        {
            return PatientRepository.Instance.Create(patient);
        }

        public bool Update(Patient patient)
        {
            return PatientRepository.Instance.Update(patient);
        }

        public bool Delete(string jmbg)
        {
            return PatientRepository.Instance.Delete(jmbg);
        }

        public bool CheckAvailability(string jmbg, string username)
        {
            return PatientRepository.Instance.CheckAvailability(jmbg, username);
        }
    }
}