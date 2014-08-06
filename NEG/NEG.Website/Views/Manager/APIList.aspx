<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Controls.Common" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    APIList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="center">
        <%
            var apiCategories = ViewData["apiCategories"] as List<APICategory>;
            var apiDetailInfos = ViewData["apiDetailInfos"] as List<APIDetailInfo>;

            if (apiCategories != null)
            {
                foreach (var category in apiCategories) 
                {
        %>
        <div class="menu_02_01">
            <ul>
                <li><%: Html.Span(category.CategoryName) %></li>

                <%
                    if (apiDetailInfos != null)
                    {

                        foreach (APIDetailInfo info in apiDetailInfos)
                        {
                            if (category.CategoryID == info.CategoryID)
                            {
                                //+ "(" + info.LANG + ")"
                                bool langTag = info.LANG != ResourceManager.LANG_EN_US;
                                string langText = langTag ? "(" + info.LANG + ")" : "";
                                
                %>
                <li><%: Html.ActionLink(info.APIName + langText, "API", "Manager", new { id = info.APIID }, null) %></li>
                <%
                            }
                        }
                    }

                %>
            </ul>
        </div>
        <%
                }
            }
        %>
        
        <div class="clear01"></div>
    </div>
</asp:Content>
