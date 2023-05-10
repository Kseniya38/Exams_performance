using System;
using System.Collections.Generic;

namespace Exams_performance
{
    public class AttestationItem
    {

        public AttestationItem(string student_fio, string teacher_fio, string subject_name, DateTime attestation_date, string attestation_type, int? mark, string result)
        {
            this.student_fio = student_fio;
            this.teacher_fio = teacher_fio;
            this.subject_name = subject_name;
            this.attestation_date = attestation_date;
            this.attestation_type = attestation_type;
            this.mark = mark;
            this.result = result;
        }

        public AppContext db = new AppContext();

        public string student_fio { get; set; }
        public string teacher_fio { get; set; }
        public string subject_name { get; set; }
        public DateTime attestation_date { get; set; }
        public string attestation_type { get; set; }
        public int? mark { get; set; }
        public string result { get; set; }


    }
}


    



