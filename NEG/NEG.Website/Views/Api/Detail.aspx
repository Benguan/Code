<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/info.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
  API - <%= ((APIDetailInfo) ViewData["apiDetailInfo"]).APIName %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var detailInfo = (APIDetailInfo) ViewData["apiDetailInfo"]; %>
    <div class="into_01">
        <div class="page">
            <%=detailInfo.APIName  %>
        </div>
    </div>
    
    
    <section class="page">
        <%
            if (detailInfo.Syntax != null)
            {
        %>
        <div class="font02">
            Syntax
        </div>
        <pre class="prettyprint lang-js prettyprinted" style="">
            <%= detailInfo.Syntax %>
        </pre>
        <%
            }
        %>
        <%
            if (detailInfo.ParameterInfo != null)
            {
        %>
        <div class="font02">
            Parameter
        </div>
        <div class="font03">
            <%= detailInfo.ParameterInfo %>
        </div>
        <%
            }
        %>
        <div class="clear02"></div>
        <div class="border_01">
            options {stripeSelector, contentSelector, buttonGoSelector, processData, go}
            <div class="border_02"></div>
            <div class="clear02"></div>
            <table border="0" cellspacing="1" cellpadding="0" style="width: 100%" class="table_01">
                <tbody><tr class="table_tr01">
                    <td>Parameter Name</td>
                    <td>Type</td>
                    <td>Description</td>
                </tr>
                <tr class="table_tr02">
                    <td>stripeSelector</td>
                    <td>String</td>
                    <td>stripe selector <span class="default_data">[*]</span></td>
                </tr>
               <tr class="table_tr03">
                    <td>contentSelector</td>
                    <td>String</td>
                    <td>content selector <span class="default_data">[*]</span></td>
                </tr>
                <tr class="table_tr02">
                    <td>buttonGoSelector</td>
                    <td>String</td>
                    <td>button-Go Selector <span class="default_data">[*]</span></td>
                </tr>
                <tr class="table_tr03">
                    <td>strileDisableClass</td>
                    <td>String</td>
                    <td>stripe disable class<span class="default_data">[*]</span></td>
                </tr>
               <tr class="table_tr02">
                    <td>processData</td>
                    <td>Function</td>
                    <td>recommended to fill the data here </td>
                </tr>
                <tr class="table_tr03">
                    <td>go</td>
                    <td>Function</td>
                    <td>will be triggered when selecte completed</td>
                </tr>
            </tbody></table>
        </div>
    </section>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
