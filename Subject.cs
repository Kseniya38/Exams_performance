namespace ExamsPerformance
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
        public int SubjectId { get; set; }

        private string subjectName;
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }
    }
}
