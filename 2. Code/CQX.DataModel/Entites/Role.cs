namespace CQX.DataModel.Entites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Role : EntityBase
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
