using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ExamsPerformance
{
    [Table("Students_in_group")]
    public partial class StudentsInGroup
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string GroupId { get; set; }
    }
}
