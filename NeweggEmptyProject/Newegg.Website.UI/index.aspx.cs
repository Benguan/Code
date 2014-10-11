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
       
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        
        base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        
        base.OnPreRender(e);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        
        base.Render(writer);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    protected override void ProcessFormRequest()
    {
        
        base.ProcessFormRequest();
    }

    public override void ProcessRequest(HttpContext context)
    {
        base.ProcessRequest(context);
    }
}