
NEG.Module('NEG.Widget.StripePicker', function (require) {
    var jQuery = require('Utility.JQuery');
    function StripePicker(option, container) {

        var me = arguments.callee;
        var stripeStatus = []
            , selectedData = []
            , strip = function (StripeEnable, EventDefined,ContentEventDefined, Key, DefaultStripeText ) {
                this.stripeEnable = StripeEnable
                this.stripEventDefined = EventDefined;
                this.contentEventDefined = ContentEventDefined;
                this.dataKey = Key;
                this.defaultStripeText = DefaultStripeText;
            };

        selectedData.get = function(stripe){
            var d = this;
            for(var i =0; i< d.length;i++){
                if(d[i].stripe === stripe ){
                    return d[i].value;
                }
            }
        }
        ,selectedData.set = function(stripe,value){
            var d= this ;
            for(var i =0; i< d.length;i++){
                if(d[i].stripe === stripe ){
                    d[i].value = value;
                    return ;
                }
            }

            this.push({"stripe":stripe,"value":value});
        }

        if (!(this instanceof me)) {
            return new me(option, container);
        }

        var stopPropagation = function (e) {
            e = e || window.event;
            if (e.stopPropagation) { //W3C阻止冒泡方法  
                e.stopPropagation();
            }
            else {
                e.cancelBubble = true; //IE阻止冒泡方法  
            }
        };

        var _option = {
            stripeSelector:""
            , contentSelector:""
            , stripes: []
            , contents: []
            , stripeTextSelector: ".atsLabel"
            , processData: null
            , dataKey: "neg-sp-data-Key"
            , dataValue: "neg-sp-data-value"
            , defaultStripeText :"neg-sp-data-defaultStrip"
            , autoExpand: true
        }

        NEG.merge(_option, option);

        _option.stripes = jQuery(_option.stripeSelector);
        _option.contents = jQuery(_option.contentSelector);

        var beforeProcessData= function (selectedData, preStripe, nextStripe){

            var $preStripe = jQuery(preStripe);
            $preStripe.find(_option.contentSelector).hide();
            var stripeKey = $preStripe.attr(_option.dataKey);
            $preStripe.find(_option.stripeTextSelector).text(selectedData.get(stripeKey).value);


            if(selectedData.length > 1){

            }


        }

        var afterProcessData = function (index,selectedData,preStripe,nextStripe) {
            //第一个stripe 绑定click事件.
            if (!stripeStatus[index] || (stripeStatus[index] && !stripeStatus[index].stripEventDefined)) {
                _option.stripes[index] && NEG(_option.stripes[index]).on("click", function (e) {

                    $firstContent = jQuery(_option.contents[index]);
                    var isHidden = jQuery($firstContent).is(":hidden");


                    if(!stripeStatus[index] || (stripeStatus[index] && !stripeStatus[index].contentEventDefined)) {
                        NEG(_option.contents[index]).on("click", contentClickEvent);

                        stripeStatus[index].contentEventDefined = true;
                    }
                    
                    jQuery(_option.contents).hide();
                    if (isHidden) {
                        $firstContent.show();
                    }
                    else {
                        $firstContent.hide();
                    }

                });

                stripeStatus[index].stripEventDefined = true;
            }

            //自动触发除了1级别以外的tag.
            if (_option.autoExpand && index > 0) {
                NEG(_option.stripes[index]).trigger("click");
            }

        }

        var init = function () {
            for (var i = 0; i < _option.stripes.length; i++) {

                var key = _option.stripes[i].getAttribute(_option.dataKey);
                var stripeEnable = (i == 0);
                var defaultStripeText = jQuery(_option.stripes[i]).find()
                stripeStatus[i] = new strip(stripeEnable, false, false ,key, defaultStripeText);
            };

            afterProcessData(0);
        }




        var contentClickEvent = function (e) {
            //NEG.ArrayIndexOf()
            var tag = e.toElement;

            //获取当前的step
            var step = NEG.ArrayIndexOf(_option.contents, jQuery(tag).parents(_option.contentSelector)[0]);

            var key = tag.getAttribute(_option.dataValue)
                , value = tag.innerHTML;

            
            stopPropagation(e);

            if (!key) {
                return;
            }


            selectedData.set(stripeStatus[step].dataKey,{ "key":key,"value":value });

            //处理下一个
            step++;

            var preStep = step - 1
            , preStripe = _option.stripes[preStep]
            , nextStripe = _option.stripes[step];
            
            beforeProcessData(selectedData,preStripe,nextStripe);

            if(_option.processData(selectedData,preStripe,nextStripe)){
                afterProcessData(step, selectedData,preStripe,nextStripe);
            }

        }

        //NEG.merge(this,api);
        init();

    };

    return StripePicker;

});

var testData = { "input-year": "1995", "makes": [{ "value": "Acura", "key": 58 }, { "value": "Alfa Romeo", "key": 16 }, { "value": "AM General", "key": 44 }, { "value": "Audi", "key": 73 }, { "value": "Bentley", "key": 69 }, { "value": "BMW", "key": 31 }, { "value": "Buick", "key": 45 }, { "value": "Cadillac", "key": 46 }, { "value": "Chevrolet", "key": 47 }, { "value": "Chrysler", "key": 39 }, { "value": "Dodge", "key": 40 }, { "value": "Eagle", "key": 41 }, { "value": "Ferrari", "key": 78 }, { "value": "Ford", "key": 54 }, { "value": "Freightliner", "key": 497 }, { "value": "Geo", "key": 50 }, { "value": "GMC", "key": 48 }, { "value": "Hino", "key": 499 }, { "value": "Honda", "key": 59 }, { "value": "Hyundai", "key": 3 }, { "value": "Infiniti", "key": 68 }, { "value": "International", "key": 71 }, { "value": "Isuzu", "key": 37 }, { "value": "Jaguar", "key": 20 }, { "value": "Jeep", "key": 42 }, { "value": "Kenworth", "key": 559 }, { "value": "Kia", "key": 21 }, { "value": "Laforza", "key": 25 }, { "value": "Lamborghini", "key": 38 }, { "value": "Land Rover", "key": 11 }, { "value": "Lexus", "key": 75 }, { "value": "Lincoln", "key": 55 }, { "value": "Lotus", "key": 84 }, { "value": "Mack", "key": 496 }, { "value": "Mazda", "key": 80 }, { "value": "Mercedes-Benz", "key": 63 }, { "value": "Mercury", "key": 56 }, { "value": "Mitsubishi", "key": 72 }, { "value": "Mitsubishi Fuso", "key": 500 }, { "value": "Morgan", "key": 12 }, { "value": "Nissan", "key": 67 }, { "value": "Oldsmobile", "key": 51 }, { "value": "Peterbilt", "key": 560 }, { "value": "Plymouth", "key": 43 }, { "value": "Pontiac", "key": 52 }, { "value": "Porsche", "key": 2 }, { "value": "Rolls Royce", "key": 70 }, { "value": "Saab", "key": 65 }, { "value": "Saturn", "key": 53 }, { "value": "Subaru", "key": 13 }, { "value": "Suzuki", "key": 1 }, { "value": "Toyota", "key": 76 }, { "value": "UD", "key": 498 }, { "value": "Volkswagen", "key": 74 }, { "value": "Volvo", "key": 27 }] };

NEG.run(function (require) {

    var stripePicker = require("NEG.Widget.StripePicker");
    var stripes = jQuery(".atsTextButtonSelect");
    var contents = jQuery(".atsDDAnchor .atsDD");

    var container = stripePicker({
        stripeSelector: ".atsTextButtonSelect"
        ,contentSelector: ".atsDDAnchor .atsDD"
        ,processData: function (selectedData, preStripe, nextStripe) {
            
            // TO DO 
            var $nextStripe = jQuery(nextStripe);

            var liString = "<ul>";

            for(var i=0; i< 10; i ++){
                liString += "<li><a href='#' neg-sp-data-value="+testData.makes[i].key +">" + testData.makes[i].value + "</a></li>";
            }

            liString+="</ul>";

            $nextStripe.find(".atsDDContent").html(liString);
            $nextStripe.removeClass("atsDisabled")

            return true;
        }
    });

});


/*

var fun1 = function(){
    console.log(1);
jQuery("#ddYearStripe .atsDD").show();
}

var fun2 = function(){
    console.log(2);
}

jQuery("#ddYearStripe .atsWrap")[0].addEventListener("click",fun1,false);

jQuery("#ddYearStripe .atsDDAnchor")[0].addEventListener("click",fun2,false);


*/



