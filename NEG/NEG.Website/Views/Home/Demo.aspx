<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Controls.Common" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NEG.JS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="center demo">
        <div class="menu_02_03">
            
            <%
                StringBuilder detailInfo = new StringBuilder();
                foreach (var data in (List<DemoDetailInfo>) (ViewData["DemoDetailInfos"]))
                {
                    detailInfo.Append(Html.ActionHover(data.ShowImage, data.DemoName, "DemoDetail", "hover"));
                }

                this.Response.Write(detailInfo.ToString());
            %>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
