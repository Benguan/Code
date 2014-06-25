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

using Newegg.Website.WebModel.Resource;
using Newegg.Website.WebModel.UI;

namespace Newegg.Website.WebModel.Control
{
    public abstract class ServiceControlBase : System.Web.UI.Control
    {
        protected CompactPageBase ContentPage
        {
            get { return Page as CompactPageBase; }
        }

        public RequestContext RequestContext
        {
            get { return ContentPage.RequestContext; }
        }

        protected QueryStringManager QueryStringManager
        {
            get { return ContentPage.QueryStringManager; }
        }
    }
}
