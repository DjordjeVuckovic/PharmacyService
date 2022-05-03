﻿using System;
using TechHealth.Model;

namespace TechHealth.Repository
{
    public class PatientAllergensRepository : GenericRepository<string, PatientAllergens>
    {
        private static readonly PatientAllergensRepository instance = new PatientAllergensRepository();

        static PatientAllergensRepository()
        {
        }

        private PatientAllergensRepository()
        {
        }

        public static PatientAllergensRepository Instance => instance;

        protected override string GetKey(PatientAllergens entity)
        {
            return entity.PatientJMBG + "-" + entity.AllergenName;
        }

        protected override string GetPath()
        {
            return @"../../Json/patientAllergens.json";
        }
        protected override void RemoveAllReference(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(PatientAllergens entity)
        {
            entity.ShouldSerialize = true;
        }
    }
}
