using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataModel.Entites
{
    public class Page : EntityBase
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Controller { get; set; }

        [Required]
        [StringLength(20)]
        public string Action { get; set; }

        [JsonIgnore]
        public Module Group { get; set; }
    }
}
