using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CQX.Web
{
    public static class UrlExtensions
    {
        public static string ChangeQueryValue(this UrlHelper helper,Uri url, string queryName,object queryValue)
        {
            var uriBuilder = new UriBuilder(url);
            var qs = HttpUtility.ParseQueryString(url.Query);
            qs.Set(queryName, queryValue.ToString());
            uriBuilder.Query = qs.ToString();
            return uriBuilder.ToString();
        }
    }
}