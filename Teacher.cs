namespace Exams_performance
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
        public int teacher_id { get; set; }

        [Required]
        private string teacher_fio;
        public string Teacher_fio
        {
            get { return teacher_fio; }
            set { teacher_fio = value; }
        }
    }
}
