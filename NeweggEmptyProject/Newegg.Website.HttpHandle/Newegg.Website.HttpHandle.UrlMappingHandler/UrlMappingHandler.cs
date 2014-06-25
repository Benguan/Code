using System;
using System.Web;

namespace Newegg.Website.HttpHandle.UrlMappingHandler
{
    public class UrlMappingHandler:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpContext.Current.Response.Write("ProcessRequest for IHttpHandler, now datetime is " +
                                               DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br/>");
        }

        public bool IsReusable
        {
            get { return true; }
        }


    }
}

