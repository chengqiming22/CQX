namespace CQX.DataModel.Entites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : EntityBase
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(20)]
        public string RealName { get; set; }

        [StringLength(20)]
        public string MobileNumber { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public UserGroup Group { get; set; }
    }
}
