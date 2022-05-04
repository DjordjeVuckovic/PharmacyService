﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechHealth.Model;
using TechHealth.Service;

namespace TechHealth.Controller
{
    public class AnamnesisController
    {
        private readonly AnamnesisService anamnesisService = new AnamnesisService();
        public Anamnesis GetByAppointmentId(string appointmentId)
        {
            return anamnesisService.GetByAppointmentId(appointmentId);
        }

        public void Create(Anamnesis anamnesis)
        {
            anamnesisService.Create(anamnesis);
        }
        public void Update(Anamnesis anamnesis)
        {
             anamnesisService.Update(anamnesis);
        }

        public ObservableCollection<Anamnesis> GetAllAnamnesisExaminationsByPatient(string patientId)
        {
            return new ObservableCollection<Anamnesis>(anamnesisService.GetAllAnamnesisExaminationsByPatient(patientId));
        }
        public ObservableCollection<Anamnesis> GetAllAnamnesisSurgeriesByPatient(string patientId)
        {
            return new ObservableCollection<Anamnesis>(anamnesisService.GetAllAnamnesisSurgeriesByPatient(patientId));
        }
    }
}