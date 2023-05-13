namespace ExamsPerformance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        private string teacherFIO;
        public string TeacherFIO
        {
            get { return teacherFIO; }
            set { teacherFIO = value; }
        }
    }
}
