using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Exams_performance
{
    [Table("Students_in_group")]
    public partial class Students_in_group
    {
        [Key]
        [Column(Order = 0)]
        public int student_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string group_id { get; set; }
    }
}
