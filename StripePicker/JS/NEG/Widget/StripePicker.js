
//neg-sp-data-id
/*
var stopPropagation = function(e){
    		e = e || window.event;  
		    if(e.stopPropagation) { //W3C阻止冒泡方法  
		        e.stopPropagation();  
		    } 
		    else {  
		        e.cancelBubble = true; //IE阻止冒泡方法  
		    }  
    	};

jQuery("#ddYearStripe .atsDDAnchor .atsDD").show();



NEG(jQuery("#ddYearStripe .atsDDAnchor .atsDD")[0]).on("click",function(e){
	var data= e.toElement.getAttribute("neg-sp-data-id");
	console.info(data);

	stopPropagation(e);
})
*/

NEG.Module('NEG.Widget.StripePicker',function(require){
	var jQuery = require('Utility.JQuery');
    function StripePicker(option,container){

    	var selectedData=[];

        var me = arguments.callee;
        if (!(this instanceof me)) {
            return new me(option,container);
        }


        var maxStep = 0;

    	var stopPropagation = function(e){
    		e = e || window.event;  
		    if(e.stopPropagation) { //W3C阻止冒泡方法  
		        e.stopPropagation();  
		    } 
		    else {  
		        e.cancelBubble = true; //IE阻止冒泡方法  
		    }  
    	};


        var _option = {
        	stripes:[]
        	,contents:[]
        	,stripeDisable:"atsDisabled"
        	,prepareData:null
        }

		NEG.merge(_option,option);


        var processStripe = function(){

        }

        var contentClickEvent = function(e){

        	var id = e.toElement.getAttribute("neg-sp-data-id");

        	var data = prepareStripeData(id);
        	
        	nextStripe(data);

        	stopPropagation(e);
        }

        var api= {
        	next:nextStripe
        }

        NEG.merge(this,api);

    };

    return StripePicker;

});

var testData= {"input-year":"1995","makes":[{"value":"Acura","key":58},{"value":"Alfa Romeo","key":16},{"value":"AM General","key":44},{"value":"Audi","key":73},{"value":"Bentley","key":69},{"value":"BMW","key":31},{"value":"Buick","key":45},{"value":"Cadillac","key":46},{"value":"Chevrolet","key":47},{"value":"Chrysler","key":39},{"value":"Dodge","key":40},{"value":"Eagle","key":41},{"value":"Ferrari","key":78},{"value":"Ford","key":54},{"value":"Freightliner","key":497},{"value":"Geo","key":50},{"value":"GMC","key":48},{"value":"Hino","key":499},{"value":"Honda","key":59},{"value":"Hyundai","key":3},{"value":"Infiniti","key":68},{"value":"International","key":71},{"value":"Isuzu","key":37},{"value":"Jaguar","key":20},{"value":"Jeep","key":42},{"value":"Kenworth","key":559},{"value":"Kia","key":21},{"value":"Laforza","key":25},{"value":"Lamborghini","key":38},{"value":"Land Rover","key":11},{"value":"Lexus","key":75},{"value":"Lincoln","key":55},{"value":"Lotus","key":84},{"value":"Mack","key":496},{"value":"Mazda","key":80},{"value":"Mercedes-Benz","key":63},{"value":"Mercury","key":56},{"value":"Mitsubishi","key":72},{"value":"Mitsubishi Fuso","key":500},{"value":"Morgan","key":12},{"value":"Nissan","key":67},{"value":"Oldsmobile","key":51},{"value":"Peterbilt","key":560},{"value":"Plymouth","key":43},{"value":"Pontiac","key":52},{"value":"Porsche","key":2},{"value":"Rolls Royce","key":70},{"value":"Saab","key":65},{"value":"Saturn","key":53},{"value":"Subaru","key":13},{"value":"Suzuki","key":1},{"value":"Toyota","key":76},{"value":"UD","key":498},{"value":"Volkswagen","key":74},{"value":"Volvo","key":27}]};

NEG.run(function(require){

	var stripePicker = require("NEG.Widget.StripePicker");
	var stripes = jQuery(".atsTextButtonSelect");
	var contents = jQuery(".atsDDAnchor .atsDD");

	var container=stripePicker({
		stripes:stripes
		,contents:contents
		,prepareStripeData:function(selectedData){
			console.info(
					{
						selectedData:selectedData
					}
				);
		}
	});

});






