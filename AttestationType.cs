namespace ExamsPerformance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attestation_type")]
    public partial class AttestationType
    {
        [Key]
        public string AttestationTypeName { get; set; }
    }

}
