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
                    StringBuilder sb = new StringBuilder();

                    foreach (var demo in (List<DemoDetailInfo>) ViewData["DemoDetailInfos"])
                    {
                        sb.AppendFormat("<li>{0}</li>", Html.ActionLink(demo.DemoName, "Demo", "Manager", new {id = demo.DemoID}, null).ToString());
                    }
                    this.Response.Write(sb.ToString());
                %>
            </ul>
        </div>

    </div>


</asp:Content>
