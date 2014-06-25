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

namespace Newegg.Website.WebModel.Resource
{
    public class QueryStringManager
    {
        #region fileds
        private HttpRequest httpRequest;
        #endregion

        #region properties
        public string GetQueryStringValue(string paramName)
        {
            return httpRequest.QueryString[paramName];
        }
        #endregion

        #region 构造函数
        public QueryStringManager(HttpRequest request)
        {
            httpRequest = request;
        }
        #endregion
    }
}
