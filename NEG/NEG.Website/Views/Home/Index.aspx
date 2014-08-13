<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import namespace="NEG.Website.Controls.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NEG.JS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="center tutorial">
                
                <h3><%= Html.Lang("Setup") %></h3>

                <ol>
                    <li>
                        <%= string.Format(Html.Lang("SetupStep1"),"DownLoad/NEG.0.2.1.zip") %>
                    </li>

                    <li>
                        <%= string.Format( Html.Lang("SetupStep2"),"NEG.0.2.1") %>
                    </li>
                    
                    <br />
                    <pre class="prettyprint lang-js">
&lt;script src="js/NEG.0.2.1.js"&gt;&lt;/script&gt;</pre>
                </ol>

                <br/>
                <h3><%= Html.Lang("Introductory") %></h3>
   
    <%=Html.Lang("IntroductoryStep1") %>

                    <pre class="prettyprint lang-js">
//This code is run in a security sandbox 
NEG.run(function(require){
    alert("Hello Word!");
})</pre>

                    
                <br/>

                <div id="learmore">
                    <h3><%= Html.Lang("QuickStart") %></h3>
                    <div><%= Html.Lang("QuickStartStep1") %></div>
                        <pre class="prettyprint lang-js">
//Define module
NEG.Module("Biz.Page.LoginAccount.LoginModule",function(){
	var validateForm=function(){
		//DO ACTION
		return true;
	};

	var checkLogin= function(userName, password){
		//DO ACTION
		return true;
	};

	//you can define function on return.
	return {
		checkLogin:checkLogin,
		validateForm: validateForm
	};
});</pre>
                    <div><%= Html.Lang("QuickStartStep2") %></div>
                        <div></div>
<pre class="prettyprint lang-js">
NEG.run(function(require){
    //we can get module by require function
    var loginModule = require("Biz.Page.LoginAccount.LoginModule");

    if(!loginModule.validateForm()){
	    return "validate failed";
    }

    if(!loginModule.checkLogin("newegg","newegg123")){
	    return "login failed"
    }

    //DO ACTION AFTER LOGIN...
});</pre>
                       
                </div>
                <a class="learmore">Quick Start</a>
            </div>

</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    
    <script>
        jQuery('.learmore').click(function () {
            var $this = jQuery(this);
            var nextText = "Go for more API.";
            if ($this.html() == "Quick Start") {
                jQuery("#learmore").slideDown("fast");
                jQuery(".learmore").html(nextText);
            }
            else if ($this.html() == nextText) {
                window.location.href = "/Home/API";
            }
        })
    </script>
</asp:Content>
