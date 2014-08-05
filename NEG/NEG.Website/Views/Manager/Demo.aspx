<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="body">
        <% using (Html.BeginForm(Convert.ToBoolean(ViewData["IsUpdate"]) ? "DemoUpdate" : "DemoAdd", "Manager", FormMethod.Post, new { @class = "form-horizontal" }))
           { 
        %>

        <%
               var detail = (DemoDetailInfo)ViewData["demoDetailInfo"];
        %>
            <input type="hidden" name="status" value="true" />
            <input type="hidden" name="demoID" value="<%:detail == null ? 0 : detail.DemoID %>" />

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall">Demo名</label>
                <div class="col-sm-5">
                    <input id="demoName" name="demoName" class="form-control " value="<%:detail == null ? string.Empty : detail.DemoName %>" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall">Demo Show</label>
                <div class="col-sm-5">
                    <textarea id="demoShowParts" rows="10" class="form-control " name="demoShowParts"><%:detail == null ? string.Empty : detail.DemoShowParts  %></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall">Demo Code</label>
                <div class="col-sm-5">
                    <textarea id="demoCode" class="form-control "  rows="10" name="demoCode"><%:detail == null ? string.Empty : detail.DemoCode %></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall">HTML Code</label>
                <div class="col-sm-5">
                    <textarea id="htmlCode" class="form-control " rows="10" name="htmlCode"><%:detail == null ? string.Empty : detail.HtmlCode %></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall">Image Url</label>
                <div class="col-sm-5">
                    <input id="showImage" class="form-control "  class="form-control " name="showImage" value="<%:detail == null ? string.Empty : detail.ShowImage %>" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-2 control-label" for="formGroupInputSmall"></label>
                <div class="col-sm-5">
                    <input id="buttom" class="btn btn-primary" type="submit" value="<%:Convert.ToBoolean(ViewData["IsUpdate"])?"更新":"添加" %>" />
                </div>
            </div>

        <% } %>
    </div>


</asp:Content>


