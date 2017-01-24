using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQMFramework.Utils.Common
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object obj)
        {
            if (obj == null)
                return default(T);
            string str = JsonConvert.SerializeObject(obj);
            var result = JsonConvert.DeserializeObject<T>(str);
            return result;
        }

        public static IEnumerable<T> ListCovnertTo<T>(this IEnumerable<object> list)
        {
            if (list == null)
                return null;
            return list.Select(obj => ConvertTo<T>(obj));
        }
    }
}
