using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exams_performance
{
    [Table("Attestation")]
    public class Attestation
    {
        [Key]
        [Column(Order = 0)]
        public int student_id { get; set; }
        public int teacher_id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int subject_id { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime attestation_date { get; set; }
        public string attestation_type { get; set; }
        public int? mark { get; set; }
        public string result { get; set; }

        public Attestation() { }

        public Attestation(int student_id, int teacher_id, int subject_id, DateTime attestation_date, string attestation_type, int? mark, string result)
        {
            this.student_id = student_id;
            this.teacher_id = teacher_id;
            this.subject_id = subject_id;
            this.attestation_date = attestation_date;
            this.attestation_type = attestation_type;
            this.mark = mark;
            this.result = result;
        }
        

    
        /*
        public override string ToString()
        {
            return "Студент: " + student_id + " Преподаватель: " + teacher_id + " Предмет: " + subject_id + " Дата: " + attestation_date + " Вид аттестации: " + attestation_type + " Балл: " + mark + " Оценка: " + result;
        }
        */

    }
}
