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
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            //Groups = new HashSet<Group>();
        }

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
        

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Group> Groups { get; set; }
    }
}
