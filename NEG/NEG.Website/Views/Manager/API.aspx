<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Models" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>API</title>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <style>
        textarea{ height: 200px;width: 600px;}
    </style>
</head>
<body>
   
    <div class="body">
        <% using (Html.BeginForm(Convert.ToBoolean(ViewData["IsUpdate"]) ? "APIUpdate" : "APIAdd", "Manager", FormMethod.Post))
           { %>
         <%
               var detail = (APIDetailInfo)ViewData["APIDetailInfo"];
        %>
        <input type="hidden" name="status" value="true"/>
        <input type="hidden" name="demoID" value="<%:detail == null ? 0 : detail.DemoID %>"/>
        <table>
            <tr>
                <td>Demo名</td>
                <td><input id="demoName" name="demoName" value="<%:detail == null ? string.Empty : detail.DemoName %>"/></td>
            </tr>
            <tr>
                <td>Demo Show</td>
                <td>
                    <textarea id="demoShowParts" name="demoShowParts"><%:detail == null ? string.Empty : detail.DemoShowParts  %></textarea>
                </td>
            </tr>
            <tr>
                <td>Demo Code</td>
                <td><textarea id="demoCode" name="demoCode"><%:detail == null ? string.Empty : detail.DemoCode %></textarea></td>
            </tr>
            <tr>
                <td>HTML Code</td>
                <td><textarea id="htmlCode" name="htmlCode"><%:detail == null ? string.Empty : detail.HtmlCode %></textarea></td>
            </tr>
            <tr>
                <td>Image Url</td>
                <td><input id="showImage" name="showImage" value="<%:detail == null ? string.Empty : detail.ShowImage %>" /></td>
            </tr>
            <tr>
                <td></td>
                <td><input id="buttom" type="submit" value="<%:Convert.ToBoolean(ViewData["IsUpdate"])?"更新":"添加" %>"/></td>
            </tr>
        </table>
         <% } %>
    </div>
</body>
</html>
