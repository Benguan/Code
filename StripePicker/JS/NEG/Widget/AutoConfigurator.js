
NEG.Module('NEG.Widget.AutoConfigurator', function (require) {
    var jQuery = require('Utility.JQuery');

    function AutoConfigurator(option, container) {

        var me = arguments.callee
            , stripeStatus = []
            , selectedData = []
            , strip = function (StripeEnable, EventDefined, ContentEventDefined, Key, DefaultStripeText, AutoExpand) {
                this.stripeEnable = StripeEnable
                this.stripeEventDefined = EventDefined;
                this.contentEventDefined = ContentEventDefined;
                this.stripeKey = Key;
                this.defaultStripeText = DefaultStripeText;
                this.autoExpand = AutoExpand;
            }
            , isCompleted = false;

        selectedData.get = function (stripe) {
            var d = this;
            for (var i = 0; i < d.length; i++) {
                if (d[i].stripe === stripe) {
                    return d[i].value;
                }
            }
        }
        , selectedData.set = function (stripe, value) {
            var d = this;
            for (var i = 0; i < d.length; i++) {
                if (d[i].stripe === stripe) {
                    d[i].value = value;
                    return;
                }
            }

            this.push({ "stripe": stripe, "value": value });
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

        var Event = {
            buttonGo: container + "AutoConfigurator_Button_Go"
        };

        _option = {
            stripeSelector: ""
           , contentSelector: ""
           , buttonGoSelector: ""
           , stripes: []
           , contents: []
           , stripeTextSelector: ".atsLabel"
           , strileDisableClass: "atsDisabled"
           , buttonGoDisableClass: "atbDisabled"
           , processData: null
           , go: null
           , stripeKey: "neg-sp-data-Key"
           , stripeValue: "neg-sp-data-value"
           , defaultStripeText: "neg-sp-data-defaultStrip"
           , autoExpandKey: "neg-sp-data-autoExpand"
        }
        
        NEG.merge(_option, option);
        NEG.on(Event.buttonGo, _option.go);

        _option.stripes = jQuery(_option.stripeSelector);
        _option.contents = jQuery(_option.contentSelector);

        var beforeProcessData = function (step, selectedData) {

            var $preStripe = jQuery(_option.stripes[step])
                , $buttonGo = jQuery(_option.buttonGoSelector);

            $preStripe.find(_option.contentSelector).hide();
            var stripeKey = $preStripe.attr(_option.stripeKey);
            $preStripe.find(_option.stripeTextSelector).text(selectedData.get(stripeKey).value);

            var arrayLength = selectedData.length;
            var needRefresh = arrayLength - step > 1;
            /*重新选择了前面的filter,需要初始化后续filter*/
            if (needRefresh) {
                isCompleted = false;
                if (!$buttonGo.hasClass(_option.buttonGoDisableClass)) {
                    $buttonGo.addClass(_option.buttonGoDisableClass);
                    NEG(jQuery(_option.buttonGoSelector)[0]).off("click", goClickHandler);
                    stripeStatus.buttonGoEventDefined = false;
                }

                for (var i = step + 1; i < arrayLength ; i++) {
                    selectedData.pop();
                };


                for (var i = step + 1 ; i < _option.stripes.length; i++) {
                    var $stripe = jQuery(_option.stripes[i]);
                    $stripe.addClass(_option.strileDisableClass);
                    $stripe.find(_option.stripeTextSelector).text(stripeStatus[i].defaultStripeText);
                    //解除事件绑定
                    if (stripeStatus[i].stripeEventDefined) {
                        _option.stripes[i] && NEG(_option.stripes[i]).off("click", stripeClickHanlder);
                        stripeStatus[i].stripeEventDefined = false;
                    }
                }
            }
        }

        var goClickHandler = function () {
            NEG.trigger(Event.buttonGo, selectedData);
        }

        var contentClickHandler = function (e) {
            //NEG.ArrayIndexOf()
            var tag = e.toElement;

            //获取当前的step
            var step = NEG.ArrayIndexOf(_option.contents, jQuery(tag).parents(_option.contentSelector)[0]);

            var key = tag.getAttribute(_option.stripeValue)
                , value = tag.innerHTML;

            stopPropagation(e);

            if (!key) {
                return;
            };

            selectedData.set(stripeStatus[step].stripeKey, { "key": key, "value": value });

            beforeProcessData(step, selectedData);

            //处理下一个
            step++;

            var preStep = step - 1
            , preStripe = _option.stripes[preStep]
            , nextStripe = _option.stripes[step];

            if (_option.processData(selectedData, preStripe, nextStripe)) {

                if (step >= _option.stripes.length || step > stripeStatus.autoExpandLength) {
                    isCompleted = true;

                    if (!stripeStatus.buttonGoEventDefined) {
                        NEG(jQuery(_option.buttonGoSelector)[0]).on("click", goClickHandler);
                        stripeStatus.buttonGoEventDefined = true;
                    }
                };

                afterProcessData(step, selectedData);
            }
        }

        var stripeClickHanlder = function (e) {

            var tag = e.target;

            //鼠标触发的click和 NEG.trigger 触发的click 不一致，后续check 
            var step = NEG.ArrayIndexOf(_option.stripes, tag) > -1 ?
                        NEG.ArrayIndexOf(_option.stripes, tag) :
                        NEG.ArrayIndexOf(_option.stripes, jQuery(tag).parents(_option.stripeSelector)[0]);

            var content = _option.contents[step];
            var isHidden = jQuery(content).is(":hidden");

            if (stripeStatus[step] && !stripeStatus[step].contentEventDefined) {
                NEG(content).on("click", contentClickHandler);

                stripeStatus[step].contentEventDefined = true;
            }

            jQuery(_option.contents).hide();
            isHidden ? jQuery(content).show() : jQuery(content).hide();
        }


        var afterProcessData = function (step, selectedData) {

            //第一个stripe 绑定click事件.
            if (!stripeStatus[step]) { return };
            if (!stripeStatus[step].stripeEventDefined) {
                _option.stripes[step] && NEG(_option.stripes[step]).on("click", stripeClickHanlder);
                stripeStatus[step].stripeEventDefined = true;
            }

            if (step > 0) {
                jQuery(_option.stripes[step]).removeClass(_option.strileDisableClass);
            }

            //需要自动触发的
            if (stripeStatus[step].autoExpand) {
                NEG(_option.stripes[step]).trigger("click", "need data");
            }

            if (isCompleted) {
                jQuery(_option.buttonGoSelector).removeClass(_option.buttonGoDisableClass);
            }
        }

        var init = function () {
            var autoExpandLength = 0;
            for (var i = 0; i < _option.stripes.length; i++) {

                var key = _option.stripes[i].getAttribute(_option.stripeKey);
                var stripeEnable = (i == 0);
                var defaultStripeTextList = jQuery("[" + _option.defaultStripeText + "]");
                var autoExpand = _option.stripes[i].getAttribute(_option.autoExpandKey);
                if (autoExpand) {
                    autoExpandLength++;
                }

                stripeStatus[i] = new strip(stripeEnable, false, false, key, jQuery(defaultStripeTextList[i]).text(), autoExpand);
            };

            stripeStatus.autoExpandLength = autoExpandLength;

            afterProcessData(0);
        }

        //NEG.merge(this,api);
        init();
    };

    return AutoConfigurator;

});

function o(key, value) {
    this.key = key;
    this.value = value;
}

var testData = [];

for (var i = 0; i < 10; i++) {
    testData.push(new o(i, "Make" + i));
}

for (var i = 0; i < 10; i++) {
    testData.push(new o(i, "Model" + i));
}

for (var i = 0; i < 10; i++) {
    testData.push(new o(i, "Category" + i));
}

for (var i = 0; i < 10; i++) {
    testData.push(new o(i, "SubCategory" + i));
}


NEG.run(function (require) {

    var stripePicker = require("NEG.Widget.AutoConfigurator");

    var container = stripePicker({
        stripeSelector: ".atsTextButtonSelect"
        , contentSelector: ".atsDDAnchor .atsDD"
        , buttonGoSelector: "#ddSubmit"
        , processData: function (selectedData, preStripe, nextStripe) {
            var length = selectedData.length;

            var liString = "<ul>";
            for (var i = (length - 1) * 10; i < length * 10 && i < testData.length; i++) {
                liString += "<li><a href='#' neg-sp-data-value=" + testData[i].key + ">" + testData[i].value + "</a></li>";
            }
            liString += "</ul>";

            var $nextStripe = jQuery(nextStripe);
            $nextStripe.find(".atsDDContent").html(liString);
            return true;
        }
        , go: function (e, selectedData) {
            console.log(selectedData);
        }
    }, "asYmmChooserStripe");

});


