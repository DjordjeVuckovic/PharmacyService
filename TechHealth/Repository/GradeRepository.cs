﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHealth.Model;

namespace TechHealth.Repository
{
    public class GradeRepository : GenericRepository<string, AppointmentGrade>
    {

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static GradeRepository()
        {
        }

        private GradeRepository()
        {
        }
        public static GradeRepository Instance { get; } = new GradeRepository();


        protected override string GetKey(AppointmentGrade entity)
        {
            return entity.EvidentAppointment.IdAppointment;
        }

        protected override string GetPath()
        {
            return @"../../Json/grade.json";
        }

        protected override void RemoveAllReference(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(AppointmentGrade entity)
        {
            entity.EvidentAppointment.ShouldSerialize = false;
        }
    }
}
