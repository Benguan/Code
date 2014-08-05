<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Controls.Common" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ModuleList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="center">
        <%
            var moduleCategories = ViewData["moduleCategories"] as List<ModuleCategory>;
            var moduleDetailInfos = ViewData["moduleDetailInfos"] as List<ModuleDetailInfo>;

            if (moduleCategories != null)
            {
                foreach (var category in moduleCategories) 
                {
        %>
        <div class="menu_02_01">
            <ul>
                <li><%: Html.Span(category.CategoryName) %></li>

                <%
                    if (moduleDetailInfos != null)
                    {

                        foreach (ModuleDetailInfo info in moduleDetailInfos)
                        {
                            if (category.CategoryID == info.CategoryID)
                            {
                %>
                <li><%: Html.ActionLink(info.ModuleName, "Module", "Manager", new {id= info.ModuleID},null) %></li>
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
    </div>

</asp:Content>
