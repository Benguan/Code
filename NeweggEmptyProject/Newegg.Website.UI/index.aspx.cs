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
using System.Web;
using System.Web.UI;
using Newegg.Website.DomainModel;
using Newegg.Website.Service.BizFacade;
using Newegg.Website.Configuration.Common;
using Newegg.Website.WebModel.UI;

public partial class index : PageBase
{
    public static void ValidateCacheOutput(HttpContext context, Object data, ref HttpValidationStatus status)
    {
        if (context.Request.QueryString["id"] != null && context.Request.QueryString["id"].ToString() == "2")
        {
            status = HttpValidationStatus.IgnoreThisRequest;
        }
        else
        {
            status = HttpValidationStatus.Valid;
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(ValidateCacheOutput), null);

        this.Response.Write("==================Page Handle Begin==================");
        this.Response.Write("<br/>");
        this.Response.Write("OnPreInit");
        this.Response.Write("<br/>");
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        this.Response.Write("OnInit");
        this.Response.Write("<br/>");
        base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        this.Response.Write("OnPreRender");
        this.Response.Write("<br/>");
        this.Response.Write(DateTime.Now.ToString());
        this.Response.Write("<br/>");
        //this.Response.ContentType = "text/plain";
        //this.Response.ContentType = "application/octet-stram";
        //this.Response.AddHeader("Content-Disposition", "attachment; filename=index.html");
        base.OnPreRender(e);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        this.Response.Write("Render");
        this.Response.Write("<br/>");
        this.Response.Write("==================Page Handle End==================");
        this.Response.Write("<br/>");
        base.Render(writer);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.Response.Write("Page_Load");
        this.Response.Write("<br/>");
        
    }

    protected override void ProcessFormRequest()
    {
        this.Response.Write("ProcessFormRequest");
        this.Response.Write("<br/>");
        base.ProcessFormRequest();
    }

    public override void ProcessRequest(HttpContext context)
    {
        HttpContext.Current.Response.Write("ProcessRequest for index.aspx, now datetime is" + DateTime.Now.ToString());
        HttpContext.Current.Response.Write("<br/>");

        base.ProcessRequest(context);
    }
}