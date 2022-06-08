﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.Service
{
    public class GradeService
    {
        public AppointmentGrade GetById(string id)
        {
            return GradeRepository.Instance.GetById(id);
        }

        public List<AppointmentGrade> GetAll()
        {
            return GradeRepository.Instance.GetAllToList();
        }

        public bool Create(AppointmentGrade grade)
        {
            return GradeRepository.Instance.Create(grade);
        }

        public bool Update(AppointmentGrade grade)
        {
            return GradeRepository.Instance.Update(grade);
        }

        public bool Delete(string id)
        {
            return GradeRepository.Instance.Delete(id);
        }

        public List<int> GetStaffGrades()
        {
            List<int> staffGrades = new List<int>();
            foreach (var grade in GetAll())
            {
                staffGrades.Add(grade.OcenaOsoblje);
            }
            return staffGrades;
        }

        public List<int> GetGeneralGrades()
        {
            List<int> staffGrades = new List<int>();
            foreach (var grade in GetAll())
            {
                staffGrades.Add(grade.UkupnaOcena);
            }
            return staffGrades;
        }

        public double GetGradesSum(List<int> grades)
        {
            double sum = 0;
            foreach (var grade in grades)
            {
                sum += grade;
            }
            return sum;
        }

        public int GetGradesNum(int gradeValue, List<int> grades)
        {
            int gradeValueCount = 0;
            foreach (var grade in grades)
            {
                if (grade == gradeValue)
                {
                    gradeValueCount += 1;
                }
            }
            return gradeValueCount;
        }

        public double GetAverageGrade(List<int> grades)
        {
            double sum = GetGradesSum(grades);
            double num = grades.Count;
            return sum / num;
        }
    }
}
