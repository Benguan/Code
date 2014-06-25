/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Ben Guan (Ben.B.Guan@newegg.com)
 * Create Date:  06/26/2013
 * Usage:
 *
 * RevisionHistory
 * Date         Author               PageDescription
 * 
*****************************************************************/

using System.Web;
using Newegg.Website.WebModel.Resource;

namespace Newegg.Website.WebModel.UI
{
    public class RequestContext
    {
        #region fileds

        private QueryStringManager queryStringManager;
        protected HttpRequest request;
        #endregion
        private const string KEY_REQUEST_CONTEXT = "KeyRequestContext";

        public static RequestContext Current
        {
            get { return HttpContext.Current.Items[KEY_REQUEST_CONTEXT] as RequestContext; }
        }

        public RequestContext(QueryStringManager qsManager)
        {
            queryStringManager = qsManager;
            request = HttpContext.Current.Request;
            HttpContext.Current.Items[KEY_REQUEST_CONTEXT] = this;
        }

    }
}