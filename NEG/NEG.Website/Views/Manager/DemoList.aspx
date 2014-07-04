<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DemoList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="center">
        <div class="menu_manager">
            <ul>
                <% 
                    foreach (var demo in (List<DemoDetailInfo>)ViewData["DemoDetailInfos"])
                    {
                        this.Response.Write(Html.ActionLink(demo.DemoName, "Demo", "Manager", new {id = demo.DemoID}, null));
                    } 
                %>
            </ul>
        </div>

    </div>


</asp:Content>
