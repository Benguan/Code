using System;
using System.Text;
using System.Web;

namespace Newegg.Website.HttpModule.OutPutCacheModule
{
    public class OutPutCacheModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {

            application.BeginRequest += new EventHandler(application_BeginRequest);
            
            application.ResolveRequestCache += new EventHandler(application_ResovleRequestCache);
            application.PostResolveRequestCache += new EventHandler(application_PostResolveRequestCache);
            //application.MapRequestHandler += new EventHandler(application_MapRequestHandler);
            //application.PostMapRequestHandler += new EventHandler(application_PostMapRequestHandler);

            application.PreRequestHandlerExecute += new EventHandler(application_PreRequestHandlerExecute);
            application.PostRequestHandlerExecute += new EventHandler(application_PostRequestHandlerExecute);

            application.PostReleaseRequestState += new EventHandler(application_PostReleaseRequestState);
            application.PostUpdateRequestCache += new EventHandler(application_PostUpdateRequestCache);
        }

        private void application_MapRequestHandler(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("MapRequestHandler:" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }

        private void application_PostMapRequestHandler(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PostMapRequestHandler:" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }


        private void application_PostRequestHandlerExecute(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PostRequestHandlerExecute:" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }


        private void application_PreRequestHandlerExecute(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PreRequestHandlerExecute:" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }


        private void application_PostResolveRequestCache(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PostResolveRequestCache" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }

        private void application_BeginRequest(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("BeginRequest" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }


        private void application_ResovleRequestCache(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("ResolveRequestCache" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }

        private void application_PostReleaseRequestState(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PostReleaseRequestState" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }


        private void application_PostUpdateRequestCache(object sender, EventArgs eventArgs)
        {
            //TO DO
            HttpContext.Current.Response.Write("PostUpdateRequestCache" + DateTime.Now.ToString());
            HttpContext.Current.Response.Write("<br />");
        }
    }
}
