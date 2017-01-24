namespace CQX.DataModel.Entites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Permission : EntityBase
    {
        [Required]
        public short Type { get; set; }

        [Required]
        public long ResourceId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
