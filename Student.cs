namespace Exams_performance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            //Groups = new HashSet<Group>();
        }

        [Key]
        public int student_id { get; set; }

        [Required]
        private string student_fio;
        private DateTime student_birth_date;
        private int student_gender;

        public string Student_fio
        {
            get { return student_fio; }
            set { student_fio = value; }
        }

        public DateTime Student_birth_date
        {
            get { return student_birth_date; }
            set { student_birth_date = value; }
        }

        public int Student_gender
        {
            get { return student_gender; }
            set { student_gender = value; }
        }
        

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Group> Groups { get; set; }
    }
}
