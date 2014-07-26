<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Models" %>
<%@ Import Namespace="NEG.Website.Controls.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    API Document
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="center">

        <%
            var apiCategories = ViewData["apiCategories"] as List<APICategory>;
            var apiDetailInfos = ViewData["apiDetailInfos"] as List<APIDetailInfo>;

            foreach (var category in apiCategories)
            {
        %>
        <div class="menu_02_01">
            <ul>
                <li><%:Html.Span(category.CategoryName) %></li>

                <%

                    foreach (APIDetailInfo info in apiDetailInfos)
                    {
                        if (category.CategoryID == info.CategoryID)
                        {
                %>        
                  
                

                <li><%: Html.ActionLink(info.APIName, "APIDetail", "API") %></li>

                <%
                        }
                    }

                %>
            </ul>
        </div>
        <%
            } 
        %>
      
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
