namespace ExamsPerformance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {        
        public Student() { }

        [Key]
        public int StudentId { get; set; }

        [Required]
        private string studentFIO;
        private DateTime studentBirthDate;
        private int studentGender;

        public string StudentFIO
        {
            get { return studentFIO; }
            set { studentFIO = value; }
        }

        public DateTime StudentBirthDate
        {
            get { return studentBirthDate; }
            set { studentBirthDate = value; }
        }

        public int StudentGender
        {
            get { return studentGender; }
            set { studentGender = value; }
        }        
    }
}
