<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/info.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Models" %>
<%@ Import Namespace="NEG.Website.Controls.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    API - <%= ((APIDetailInfo) ViewData["apiDetailInfo"]).APIName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var detailInfo = (APIDetailInfo)ViewData["apiDetailInfo"]; %>
    <div class="into_01">
        <div class="page">
            <%=detailInfo.APIName  %>
        </div>
    </div>


    <section class="page">

        <%
            /*Summary Begin*/
            if (!string.IsNullOrWhiteSpace(detailInfo.Summary))
            {
        %>
        <div class="font02">
            Summary
        </div>
        <div class="font01">
            <%= detailInfo.Summary %>
        </div>
        <%
            /*Summary End*/
            }
        %>
        
        
        <%
            /*Syntax Begin*/
            if (!string.IsNullOrWhiteSpace(detailInfo.Syntax))
            {
        %>
        <div class="font02">
            Syntax
        </div>
        <pre class="prettyprint lang-js prettyprinted" style=""><%= detailInfo.Syntax %></pre>
        <%
            /*Syntax End*/
            }
        %>

        <%
            /*Parameter Begin*/
            if (!string.IsNullOrWhiteSpace(detailInfo.ParameterInfo))
            {
        %>
        <div class="font02">
            Parameter
        </div>
        <div class="font03">
            <%= detailInfo.ParameterInfo %>
        </div>
        <%
            /*Parameter Begin*/
            }
        %>

        
        <%
            /*Example Begin*/
            if (!string.IsNullOrWhiteSpace(detailInfo.Example))
            {
        %>
        <div class="example">
            <%
                if (detailInfo != null && detailInfo.DemoKey != null)
                {

                    var routeValues = new RouteValueDictionary();
                    routeValues.Add("id", detailInfo.DemoKey);

                    var attributes = new Dictionary<string, object>();
                    attributes.Add("class", "viewDemo");
                
            %>
            <%= Html.ActionLinkWithTag("span","[VIEW DEMO]","Detail","Demo",routeValues,attributes,null) %>
            <%
                }
            %>
<pre class="prettyprint lang-js prettyprinted" style="">
<%= detailInfo.Example %>
</pre>
        </div>
        
        <%
            }
        %>


        
    </section>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
