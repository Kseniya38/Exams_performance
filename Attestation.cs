using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamsPerformance
{
    [Table("Attestation")]
    public class Attestation
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SubjectId { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime AttestationDate { get; set; }
        public string AttestationTypeName { get; set; }
        public int? Mark { get; set; }
        public string Result { get; set; }

        public Attestation() { }

        public Attestation(int studentId, int teacherId, int subjectId, DateTime attestationDate, string attestationTypeName, int? mark, string result)
        {
            this.StudentId = studentId;
            this.TeacherId = teacherId;
            this.SubjectId = subjectId;
            this.AttestationDate = attestationDate;
            this.AttestationTypeName = attestationTypeName;
            this.Mark = mark;
            this.Result = result;
        }
    }
}
