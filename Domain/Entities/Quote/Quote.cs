using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quote : Entity<int>
    {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string State { get; set; }
        public string Observation { get; set; }
        public int IdPatient { get; set; }
        public Patient Patient { get; set; }
        public List<CareStaff> CareStaff { get; }

        public Quote(DateTime date, DateTime startTime, DateTime endTime, Patient patient)
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Patient = patient;
            CareStaff = new List<CareStaff>();
        }
        public Quote()
        {
            CareStaff = new List<CareStaff>();
        }

        public void AddRangeCareStaff(List<CareStaff> careStaff) 
        {
            CareStaff.AddRange(careStaff);
        }

    }
}
