﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.ServiceContract.Models
{
    public class ModuleModel
    {
        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public List<PageModel> Pages { get; set; }
    }
}
