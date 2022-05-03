﻿using System;

namespace TechHealth.Model
{

    public class Anamnesis
    {
        public Appointment AnmnesisAppointment { get; set; }
        //public MainIssue MainIssue { get; set; }
        public string MainIssue { get; set; }
        public DateTime AnamnesisDate { get; set; }
        public string AnamnesisId { get; set; }
        public string OtherSymptoms { get; set; }
        public string GeneralAmnesis { get; set; }
        public string Cns { get; set; }
        public string Eye { get; set; }
        public string Neck { get; set; }
        public string Mss { get; set; }
        public string Skin { get; set; }
        public string Ear { get; set; }
        public string Cus { get; set; }
        public string Infectious { get; set; }
        public string Ugs { get; set; }
        public string Respiratory { get; set; }
        public string Mouth { get; set; }
        public string Gi { get; set; }
    }
}