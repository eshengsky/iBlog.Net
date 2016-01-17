; +function ($, window, document) {
    'use strict';

    //构造函数
    //element: jQuery选择器选择的元素
    //options: 合并后的参数对象
    function Plugin(element, options) {
        this.$element = $(element);
        this.options = options;
        this._init();
    }

    //插件名称
    Plugin.NAME = 'cycleText';

    //版本号
    Plugin.VERSION = '1.0.0';

    //默认值
    Plugin.DEFAULTS = {
        separator: '|',
        animation: 'flipInX',
        interval: 2000
    };

    Plugin.prototype._init = function () {
        var that = this;
        this.originalText = this.$element.text();
        this.textArray = this.originalText.split(this.options.separator);
        this.$element.empty();
        $.each(this.textArray, function (i) {
            that.$element.append('<span class="cycleText animated ' + that.options.animation + '" style="display: none;">' + this + '</span>');
        });
        this.start();
    };

    Plugin.prototype.start = function () {
        var that = this;
        var index = 0;
        this.$element.find("span.cycleText:eq(0)").css('display', 'inline-block');
        this._interval = setInterval(function () {
            index++;
            index = index % that.textArray.length;
            that.$element.find("span.cycleText").hide();
            that.$element.find("span.cycleText:eq(" + index + ")").css('display', 'inline-block');
        }, this.options.interval);
    };

    Plugin.prototype.stop = function () {
        this._interval = clearInterval(this._interval);
    };

    function fn(args) {
        var that = this;
        return this.each(function () {
            var $this = $(this);
            var data = $this.data('plugin_' + Plugin.NAME);
            var options = $.extend({}, Plugin.DEFAULTS, $this.data(), typeof args === 'object' && args)
            //未初始化则执行初始化
            if (!data) {
                $this.data('plugin_' + Plugin.NAME, (data = new Plugin(this, options)))
            }

            if (data[args])
            {
                data[args].apply(data, Array.prototype.slice.call(arguments, 1));
            }
        });
    }

    $.fn[Plugin.NAME] = fn;
    $.fn[Plugin.NAME].Constructor = Plugin;

}(jQuery, window, document);
