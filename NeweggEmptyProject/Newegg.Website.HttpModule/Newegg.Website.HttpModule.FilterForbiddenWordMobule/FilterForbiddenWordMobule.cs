using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web;

namespace Newegg.Website.HttpModule.FilterForbiddenWordMobule
{
    public class FilterForbiddenWordMobule : IHttpModule
    {
        private static PropertyInfo s_isReadOnlyPropertyInfo;

        static FilterForbiddenWordMobule()
        {
            Type type = typeof (NameObjectCollectionBase);
            s_isReadOnlyPropertyInfo = type.GetProperty(
                "IsReadOnly",
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }

        private static void OnBeginRequest(object sender, EventArgs e)
        {
            var request = (sender as HttpApplication).Request;
            ProcessCollection(request.QueryString);
            ProcessCollection(request.Form);
        }


        private static void ProcessCollection(NameValueCollection collection)
        {
            var copy = new NameValueCollection();

            foreach (string key in collection.AllKeys)
            {
                Array.ForEach(
                    collection.GetValues(key),
                    v => copy.Add(key, ForbiddenWord.Filter(v)));
            }

            s_isReadOnlyPropertyInfo.SetValue(collection, false, null);

            collection.Clear();
            collection.Add(copy);

            s_isReadOnlyPropertyInfo.SetValue(collection, true, null);

        }
    }
}
