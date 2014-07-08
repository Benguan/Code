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
            </ul>
        </div>
        <%
            } 
        %>
   <%--     <div class="menu_02_01">
            <ul>
                <li><span>BASE<span> </span></span></li>
                <li><a href="API/BaseAPI/NEG.merge.html">NEG.merge </a></li>
                <li><a href="API/BaseAPI/NEG.blend.html">NEG.blend </a></li>
                <li><a href="API/BaseAPI/NEG.run.html">NEG.run </a></li>
                <li><a href="API/BaseAPI/NEG.iRun.html">NEG.iRun </a></li>
                <li><a href="API/BaseAPI/NEG.Module.html">NEG.Module </a></li>
                <li><a href="API/BaseAPI/NEG.NS.html">NEG.NS </a></li>
                <li><a href="API/BaseAPI/NEG.on.html">NEG.on </a></li>
                <li><a href="API/BaseAPI/NEG.off.html">NEG.off </a></li>
                <li><a href="API/BaseAPI/NEG.setCDNTimestamp.html">NEG.setCDNTimestamp </a></li>
                <li><a href="API/BaseAPI/NEG.setVersion.html">NEG.setVersion </a></li>
                <li><a href="API/BaseAPI/NEG.trigger.html">NEG.trigger </a></li>
            </ul>
        </div>--%>
    
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
