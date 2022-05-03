﻿using System.Collections.Generic;
using TechHealth.Model;

namespace TechHealth.Repository
{
    public class AnamnesisRepository : GenericRepository<string, Anamnesis>
    {
        private static readonly AnamnesisRepository instance = new AnamnesisRepository();

        static AnamnesisRepository()
        {
        }

        private AnamnesisRepository()
        {
        }
        public static AnamnesisRepository Instance => instance;
        protected override string GetPath()
        {
            return @"../../Json/anamnesis.json";
        }

        protected override string GetKey(Anamnesis entity)
        {
            return entity.AnamnesisId;
        }

        protected override void RemoveAllReference(string key)
        {
            throw new System.NotImplementedException();
        }

        protected override void ShouldSerialize(Anamnesis entity)
        {
            entity.Appointment.ShouldSerialize = false;
        }

        public List<Anamnesis> GetByDoctorId(string id)
        {
            List<Anamnesis> anamneses = new List<Anamnesis>();
            foreach (var t in GetAllToList())
            {
                if (id.Equals(t.Appointment.Doctor.Jmbg))
                {
                    anamneses.Add(t);
                }
            }
            return anamneses;
        }

        public Anamnesis GetByAppointmentId(string appointmentId)
        {
            Anamnesis retval = null;
            foreach (var t in GetAllToList())
            {
                if (appointmentId == t.Appointment.IdAppointment)
                {
                    retval = t;
                    break;
                }
            }

            return retval;
        }

    }
}