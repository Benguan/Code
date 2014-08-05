<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="NEG.Website.Models" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Demo</title>
    <style>
        textarea {
            height: 200px;
            width: 600px;
        }
    </style>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.2.0/css/bootstrap.min.css">
</head>
<body>

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
</body>
</html>
