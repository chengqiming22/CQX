using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.Web.Identity
{
    public class IdentityRole : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
