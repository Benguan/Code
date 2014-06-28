<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import namespace="NEG.Website.Controls.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NEG.JS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="center aboutNEG">
        <%= Html.Lang("AboutNEGBody") %>
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
