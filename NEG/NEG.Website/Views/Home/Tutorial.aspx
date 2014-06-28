<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NEG.JS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="center tutorial">
                
                <h3>PART 1 - GET SETUP</h3>

                <ol>
                    <li>Download and unzip the NEG.JS from <a href="DownLoad/NEG.0.2.1.zip">here</a></li>

                    <li>Look inside the <pre class="prettyprint inline">release</pre > folder to find <pre class="prettyprint inline">NEG.0.2.1.js</pre> and load this file from your html page.load</li>
                    <br />
                    <pre class="prettyprint lang-js">
&lt;script src="js/NEG.0.2.1.js"&gt;&lt;/script&gt;</pre>
                </ol>

                <br/>
                <h3>PART 2 - [NEG.JS] INTRODUCTORY</h3>
                    One of the most basic and useful feature is all of Javascript code run in a sandbox runtime.
                    <pre class="prettyprint lang-js">
//This code is run in a security sandbox 
NEG.run(function(require){
    alert("Hello Word!");
})</pre>

                    
                <br/>

                <div id="learmore">
                    <h3>[NEG.JS] QUICK START</h3>
                        <div>Let's define a module by NEG</div>
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
                        <div>Then, we can require module and use it.</div>
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
                window.location.href = "API.html";
            }
        })
    </script>
</asp:Content>
