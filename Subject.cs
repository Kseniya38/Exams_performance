namespace Exams_performance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subject")]
    public partial class Subject
    {
        [Key]
        public int subject_id { get; set; }

        private string subject_name;
        public string Subject_name
        {
            get { return subject_name; }
            set { subject_name = value; }
        }
    }
}
