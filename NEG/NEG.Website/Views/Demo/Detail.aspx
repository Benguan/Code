<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/info.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Demo Page
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="StyleContent" runat="server">
    <%: Styles.Render("~/Content/DemoCSS") %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="font02">Demo Show</div>
        <%=  ((DemoDetailInfo)ViewData["DemoDetailInfo"]).DemoShowParts %>
    
        <div class="space"></div>
        
        <div class="font02">NEG.JS Code</div>
        <%= ((DemoDetailInfo)ViewData["DemoDetailInfo"]).DemoCode %>
        
        <div class="space"></div>
        <div class="font02">HTML Code</div>
        <%= ((DemoDetailInfo)ViewData["DemoDetailInfo"]).HtmlCode %>

       
</asp:Content>
