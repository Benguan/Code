<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/info.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

    <% 
        var detail = (DemoDetailInfo)ViewData["DemoDetailInfo"];

        if (detail != null)
        {
    %>
    Demo-<%= detail.DemoName %>
    <%
        }

    %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="StyleContent" runat="server">
    <%: Styles.Render("~/Content/DemoCSS") %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        var detail = (DemoDetailInfo)ViewData["DemoDetailInfo"];
        if (detail != null)
        {
    %>

    <%
            if (!string.IsNullOrWhiteSpace(detail.ExecuteStyle))
            {
    %>
    <style><%= detail.ExecuteStyle %></style>
    <%
            }
    %>

    <%
            if (!string.IsNullOrWhiteSpace(detail.DemoShowParts))
            {
    %>
    <div class="font02">Demo Show</div>
    <%= detail.DemoShowParts %>
    <div class="space"></div>

    <%
            }
    %>

    <%
            if (!string.IsNullOrWhiteSpace(detail.DemoCode))
            {
    %>
    <div class="font02">NEG.JS Code</div>
    <pre class="prettyprint lang-js">
    <%= detail.DemoCode %>
        </pre>
    <div class="space"></div>
    <%
            }
    %>

    <%
            if (!string.IsNullOrWhiteSpace(detail.HtmlCode))
            {
    %>
    <div class="font02">HTML Code</div>
    <pre class="prettyprint lang-html">
        <%= detail.HtmlCode %>
        </pre>
    <%
            }
    %>

    <%
            if (!string.IsNullOrWhiteSpace(detail.ExecuteScript))
            {
    %>
    <script>
        <%= detail.ExecuteScript %>
    </script>

    <%
            }
    %>

    <%
        }
    %>
</asp:Content>
