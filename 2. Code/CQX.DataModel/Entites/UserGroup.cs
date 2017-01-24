using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataModel.Entites
{
    public class UserGroup : EntityBase
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public List<Role> Roles { get; set; }
        public List<User> Users { get; set; }
    }
}
