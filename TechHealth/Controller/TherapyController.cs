﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechHealth.Model;
using TechHealth.Service;

namespace TechHealth.Controller
{
    public class TherapyController
    {
        private readonly TherapyService therapyService = new TherapyService();
        public void Create(Therapy therapy)
        {
            therapyService.Create(therapy);
        }

        public List<Therapy> GetAll()
        {
            return therapyService.GetAll();
        }
        public List<Therapy> GetAllByPatientId(string patientId)
        {
            return therapyService.GetAllByPatientId(patientId);
        }
    }
}