
NEG.Module('NEG.Widget.AutoConfigurator', function (require) {
    var jQuery = require('Utility.JQuery');

    function AutoConfigurator(option, container) {
        var me = arguments.callee
            , stripeStatus = []
            , selectedData = []
            , strip = function (StripeEnable, Key, DefaultStripeText, AutoExpand) {
                this.stripeEnable = StripeEnable
                this.stripeEventDefined = false;
                this.contentEventDefined = false;
                this.stripeKey = Key;
                this.defaultStripeText = DefaultStripeText;
                this.autoExpand = AutoExpand;
                this.contentLeaveEventDefined = !_option.enableMouseLeave;
            }
            , isCompleted = false;

        if (!(this instanceof me)) {
            return new me(option, container);
        }

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

        if (!container) { container = "container"; }

        var Event = {
            buttonGo: container + "AutoConfigurator_Button_Go"
        };

        _option = {
            stripeSelector: ""
           , contentSelector: ""
           , buttonGoSelector: ""
           , stripes: []
           , contents: []
           , strileDisableClass: ""
           , buttonGoDisableClass: ""
           , processData: null
           , go: null
           , stripeKey: "neg-sp-data-Key"
           , stripeValue: "neg-sp-data-value"
           , defaultStripeText: "neg-sp-data-defaultStrip"
           , autoExpandKey: "neg-sp-data-autoExpand"
           , enableMouseLeave: true
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
            $preStripe.find("[" + _option.defaultStripeText + "]").text(selectedData.get(stripeKey).value);

            var arrayLength = selectedData.length;
            var needRefresh = arrayLength - step > 1;
            /*重新选择了前面的filter,需要初始化后续stripe*/
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
                    $stripe.find("[" + _option.defaultStripeText + "]").text(stripeStatus[i].defaultStripeText);

                    //解除事件绑定
                    if (stripeStatus[i].stripeEventDefined) {
                        _option.stripes[i] && NEG(_option.stripes[i]).off("click", stripeClickHandler);
                        stripeStatus[i].stripeEventDefined = false;
                    }
                }
            }
        }

        var goClickHandler = function () {
            NEG.trigger(Event.buttonGo, selectedData);
        }

        var helper = {
            setTimeout: function (callBack, timeout, param) {
                var args = Array.prototype.slice.call(arguments, 2);
                var _cb = function () {
                    callBack.apply(null, args);
                }
                return window.setTimeout(_cb, timeout);
            },
            stopPropagation: function (e) {
                e = e || window.event;
                if (e.stopPropagation) { //W3C阻止冒泡方法 
                    e.stopPropagation();
                }
                else {
                    e.cancelBubble = true; //IE阻止冒泡方法  
                }
            }
        };

        var t = null;

        var hideContent = function (target) {
            jQuery(target).hide();
        }

        var contentLeaveHandler = function (e) {
            var stripTarget = jQuery(e.target).parents(_option.stripeSelector)[0];
            var step = NEG.ArrayIndexOf(_option.stripes, stripTarget);

            if (jQuery(_option.contents[step]).is(":hidden")) {
                return;
            }

            t = helper.setTimeout(hideContent, 500, _option.contents[step]);
        }

        var contentEnterHandler = function (e) {
            var stripeTarget = jQuery(e.target).parents(_option.stripeSelector)[0];
            var step = NEG.ArrayIndexOf(_option.stripes, stripeTarget);
            if (jQuery(_option.contents[step]).is(":hidden")) {
                return;
            }
            window.clearTimeout(t);
        }

        var contentClickHandler = function (e) {
            //NEG.ArrayIndexOf()
            var target = e.toElement;

            //获取当前的step
            var step = NEG.ArrayIndexOf(_option.contents, jQuery(target).parents(_option.contentSelector)[0]);

            var key = target.getAttribute(_option.stripeValue)
                , value = target.innerHTML;

            helper.stopPropagation(e);

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

        var stripeClickHandler = function (e) {

            var tag = e.target;

            //鼠标触发的click和 NEG.trigger 触发的click 不一致，后续check neg代码
            var step = NEG.ArrayIndexOf(_option.stripes, tag) > -1 ?
                        NEG.ArrayIndexOf(_option.stripes, tag) :
                        NEG.ArrayIndexOf(_option.stripes, jQuery(tag).parents(_option.stripeSelector)[0]);

            var content = _option.contents[step]
                , stripe = _option.stripes[step];

            var isHidden = jQuery(content).is(":hidden");

            if (stripeStatus[step] && !stripeStatus[step].contentEventDefined) {
                NEG(content).on("click", contentClickHandler);

                stripeStatus[step].contentEventDefined = true;
            }

            if (stripeStatus[step] && !stripeStatus[step].contentLeaveEventDefined) {
                jQuery(stripe).on("mouseleave", contentLeaveHandler);
                jQuery(stripe).on("mouseenter", contentEnterHandler);

                stripeStatus[step].contentLeaveEventDefined = true;
            }

            jQuery(_option.contents).hide();
            isHidden ? jQuery(content).show() : jQuery(content).hide();
        }

        var afterProcessData = function (step, selectedData) {
            //第一个stripe 绑定click事件.
            if (!stripeStatus[step]) { return };
            if (!stripeStatus[step].stripeEventDefined) {
                _option.stripes[step] && NEG(_option.stripes[step]).on("click", stripeClickHandler);
                stripeStatus[step].stripeEventDefined = true;
            }

            if (step > 0) {
                jQuery(_option.stripes[step]).removeClass(_option.strileDisableClass);
            }

            //需要自动触发的
            if (stripeStatus[step].autoExpand) {
                NEG(_option.stripes[step]).trigger("click", "");
            }

            if (isCompleted) {
                jQuery(_option.buttonGoSelector).removeClass(_option.buttonGoDisableClass);
            }

            clearTimeout(t);
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

                stripeStatus[i] = new strip(stripeEnable, key, jQuery(defaultStripeTextList[i]).text(), autoExpand);
            };

            stripeStatus.autoExpandLength = autoExpandLength;

            afterProcessData(0);
        }

        //NEG.merge(this,api);
        init();
    };

    return AutoConfigurator;

});
