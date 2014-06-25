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
    public  class FormManager
    {
        #region 静态变量
        public const string PARAM_SEARCH_ID = "id";
        #endregion

        #region fileds
        private HttpRequest httpRequest;
        #endregion

        

        public FormManager(HttpRequest request)
        {
            httpRequest = request;
        }

        
        public string GetFormValue(string paramName)
        {
            if (string.IsNullOrEmpty(httpRequest.Form.Get(paramName)))
            {
                return string.Empty;
                //return m_HttpRequest.Form.Get(paramName);
            }
            return httpRequest.Form.Get(paramName).Trim();
        }

    }
}
