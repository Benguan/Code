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


using System;
using System.Net;
using System.Web;
using System.Web.UI;
using Newegg.Website.WebModel.Resource;
using Utility= Newegg.Framework.Web;

namespace Newegg.Website.WebModel.UI
{
    public abstract class CompactPageBase:Page
    {
        private const string CONTENT_TYPE = "text/html";
        private const string HTTPCONTEXT_NAME = "PageBase";

        private RequestContext requestContext;

        #region 构造函数
        public CompactPageBase()
        {
            HttpContext.Current.Items[HTTPCONTEXT_NAME] = this;
        }

        public static CompactPageBase Current
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                return HttpContext.Current.Items[HTTPCONTEXT_NAME] as CompactPageBase;
            }
        }
        #endregion

        /// <summary>
        /// Gets the query string manager.
        /// </summary>
        /// <value>The query string manager.</value>
        public QueryStringManager QueryStringManager
        {
            get
            {
                return new QueryStringManager(Request);
            }
        }

        /// <summary>
        /// Gets the form manager.
        /// </summary>
        /// <value>The form manager.</value>
        public FormManager FormManager
        {
            get { return new FormManager(Request); }
        }

        public virtual RequestContext RequestContext
        {
            get
            {
                if (requestContext == null)
                {
                    requestContext = new RequestContext(QueryStringManager);
                }
                return requestContext;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);
        }

        protected bool IsFromPost
        {
            get { return Utility.WebUtility.IsFromPost(); }
        }

        protected override void OnInit(EventArgs e)
        {
            if (IsFromPost)
            {
                ProcessFormRequest();
            }

            Response.ContentType = CONTENT_TYPE;
            base.OnInit(e);
        }

        /// <summary>
        /// 页面提交执行,各子类具体实现
        /// </summary>
        protected virtual void ProcessFormRequest()
        {

        }

       
    }
}
