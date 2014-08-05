<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Controls.Common" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NEG.JS
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
                <li><%: Html.ActionLink(info.ModuleName, "Detail", "Module", new {id= info.ModuleKey},null) %></li>
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


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
