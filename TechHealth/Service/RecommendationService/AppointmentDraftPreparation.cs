﻿using System;
using System.Collections.Generic;

namespace TechHealth.Service.RecommendationService
{
    class AppointmentDraftPreparation
    {
        List<TimeSpan> HoursOfRecommendAppointment;
        DateTime startDate;
        DateTime endDate;

        public AppointmentDraftPreparation(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            HoursOfRecommendAppointment = new List<TimeSpan>() { new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0) };
        }

        public List<AppointmentDraft> GetRecommendedAppointmentDrafts()
        {
            List<AppointmentDraft> possibleRecommendation = new List<AppointmentDraft>();
            int performanceConstraint = 0;
            while (startDate <= endDate && performanceConstraint++ < 5)
            {
                CreateStartTimeOfAppointment(possibleRecommendation);
                startDate = startDate.AddDays(1);
            }
            return possibleRecommendation;
        }

        private void CreateStartTimeOfAppointment(List<AppointmentDraft> possibleRecommendation)
        {
            for (int i = 0; i < HoursOfRecommendAppointment.Count; i++)
            {
                possibleRecommendation.Add(new AppointmentDraft(startDate + HoursOfRecommendAppointment[i]));
            }
        }
    }
}