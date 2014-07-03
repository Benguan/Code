<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Demo</title>
    <style>
        textarea{ height: 200px;width: 600px;}
    </style>
</head>
<body>
   
    <div class="body">
        <% using (Html.BeginForm())
           { %>
        <input type="hidden" name="status" value="true"/>
        <table>
            <tr>
                <td>Demo名</td>
                <td><input id="demoName" name="demoName"/></td>
            </tr>
            <tr>
                <td>Demo Show</td>
                <td>
                    <textarea id="demoShowParts" name="demoShowParts"></textarea>
                </td>
            </tr>
            <tr>
                <td>Demo Code</td>
                <td><textarea id="demoCode" name="demoCode"></textarea></td>
            </tr>
            <tr>
                <td>HTML Code</td>
                <td><textarea id="htmlCode" name="htmlCode"></textarea></td>
            </tr>
            <tr>
                <td>Image Url</td>
                <td><input id="showImage" name="showImage" /></td>
            </tr>
            <tr>
                <td></td>
                <td><input id="buttom" type="submit" value="添加"/></td>
            </tr>
        </table>
         <% } %>
    </div>
</body>
</html>
