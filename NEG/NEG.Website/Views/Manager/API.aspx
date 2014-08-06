<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="NEG.Website.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body">
        <% 
            using (Html.BeginForm(Convert.ToBoolean(ViewData["IsUpdate"]) ? "APIUpdate" : "APIAdd", "Manager", FormMethod.Post, new { @class = "form-horizontal" }))
            { 
        %>
        <%
                var detail = (APIDetailInfo)ViewData["apiDetailInfo"];
        %>
        <input type="hidden" name="status" value="true" />
        <input type="hidden" name="apiID" value="<%:detail == null ? 0 : detail.APIID %>" />
        <input type="hidden" id="langInput" name="lang" value="<%:detail==null?"en-US":detail.LANG %>"/>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">API Key</label>
            <div class="col-sm-5">
                <input id="apiKey" name="apiKey" class="form-control " value="<%:detail == null ? string.Empty : detail.APIKey %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">API Name</label>
            <div class="col-sm-5">
                <input id="apiName" name="apiName" class="form-control " value="<%:detail == null ? string.Empty : detail.APIName %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Summary</label>
            <div class="col-sm-5">
                <input id="Summary" name="Summary" class="form-control " value="<%:detail == null ? string.Empty : detail.Summary %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Syntax</label>
            <div class="col-sm-5">
                <input id="Syntax" name="syntax" class="form-control " value="<%:detail == null ? string.Empty : detail.Syntax %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Parameter</label>
            <div class="col-sm-5">
                <textarea id="parameterInfo" class="form-control " rows="10" name="parameterInfo"><%:detail == null ? string.Empty : detail.ParameterInfo  %></textarea>
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Return</label>
            <div class="col-sm-5">
                <textarea id="ReturnValue" class="form-control " rows="10" name="ReturnValue"><%:detail == null ? string.Empty : detail.ReturnValue %></textarea>
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Example</label>
            <div class="col-sm-5">
                <textarea id="example" class="form-control " rows="10" name="example"><%:detail == null ? string.Empty : detail.Example %></textarea>
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">DemoKey</label>
            <div class="col-sm-2">
                <input id="DemoKey" name="DemoKey" class="form-control " value="<%:detail == null ? string.Empty : detail.DemoKey %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">CategoryID</label>
            <div class="col-sm-2">
                <input id="CategoryID" name="CategoryID" class="form-control " value="<%:detail == null ? string.Empty : detail.CategoryID.ToString() %>" />
            </div>
        </div>
        
        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Lang</label>
            <div class="col-sm-2">
                <select class="form-control" id="langSelect">
                    <option value="en-US" <%=detail.LANG=="en-US"?"selected=true":"" %>>English</option>
                    <option value="zh-CN" <%=detail.LANG=="zh-CN"?"selected=true":"" %>>中文</option>
                </select>
            </div>
        </div>
                
        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall">Priority</label>
            <div class="col-sm-2">
                <input id="Priority" name="Priority" class="form-control " value="<%:detail == null ? string.Empty : detail.Priority.ToString() %>" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-xs-2 control-label" for="formGroupInputSmall"></label>
            <div class="col-sm-5">
                <input id="buttom" type="submit" class="btn btn-primary " value="<%:Convert.ToBoolean(ViewData["IsUpdate"])?"更新":"添加" %>" />
            </div>
        </div>
        
        <script>
            NEG.run(function (require) {
                var target = document.getElementById("langSelect");
                NEG(target).on("change", function () {
                    document.getElementById("langInput").value = this.value;
                })
            });

        </script>
        <% } %>
    </div>
</asp:Content>



    
