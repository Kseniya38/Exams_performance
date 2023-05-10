namespace Exams_performance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Group")]
    public partial class Group
    {
        [Key]
        public string group_id { get; set; }

        //public string Group_id
        //{
        //    get { return group_id; }
        //    set { group_id = value; }
        //}
    }
}
