using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQMFramework.Utils.Common
{
    public static class ConfigHelper
    {
        public static string ServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceUrl"];
            }
        }
    }
}
