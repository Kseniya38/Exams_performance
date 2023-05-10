namespace Exams_performance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attestation_type")]
    public partial class Attestation_type
    {
        [Key]
        public string attestation_type { get; set; }
    }

}
