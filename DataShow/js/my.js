(function () {
    var RoutePath = "/ApiService/Execute",
    
    regGet = new RegExp(/^get/);
    regDelete = new RegExp(/^delete/);
    var my = {};
    my.log = function (msg) {
        console && console.log && console.log(msg);
    }
    my.info = function (msg) {
        alert(msg);
    };
    my.confirm = function (msg) {
        return confirm(msg);
    };
    my.success = function (msg) {
        alert(msg);
    };
    my.error = function (msg) {
        alert(msg);
    }
    my.getJsonName = function (json) {
        var obj = eval('(' + json + ')');
        var sRtn = "[";
        for (var i = 0; i < obj.length; i++) {
            if (i == obj.length - 1) {
                sRtn += "'" + obj[i].name + "'";
            }
            else {
                sRtn += "'" + obj[i].name + "',";
            }
        }
        sRtn += "]";
        return eval('(' + sRtn + ')');
    }
    my.getJsonValue = function (json) {
        var obj = eval('(' + json + ')');
        var sRtn = "[";
        for (var i = 0; i < obj.length; i++) {
            if (i == obj.length - 1) {
                sRtn += "" + obj[i].value + "";
            }
            else {
                sRtn += "" + obj[i].value + ",";
            }
        }
        sRtn += "]";
        return eval('(' + sRtn + ')');
    }
    my.getJsonNameByJosn = function (json) {
        var obj = json;
        var sRtn = "[";
        for (var i = 0; i < obj.length; i++) {
            if (i == obj.length - 1) {
                sRtn += "'" + obj[i].name + "'";
            }
            else {
                sRtn += "'" + obj[i].name + "',";
            }
        }
        sRtn += "]";
        return eval('(' + sRtn + ')');
    }
    my.getJsonValueByJosn = function (json) {
        var obj = json;
        var sRtn = "[";
        for (var i = 0; i < obj.length; i++) {
            if (i == obj.length - 1) {
                sRtn += "" + obj[i].value + "";
            }
            else {
                sRtn += "" + obj[i].value + ",";
            }
        }
        sRtn += "]";
        return eval('(' + sRtn + ')');
    }
    my.ajax = function (url, data, callback) {
        var repeatCount = 1;
        var currentRepeat = 0;
        return $.ajax({
            url: url,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            type: 'POST',
            cache: false,
            dataType: 'json'
        }).done(function (json) {
            if (json.Message) {
                currentRepeat++;
                if (currentRepeat > repeatCount)
                    return my.ajax(url, data, ajaxdone, errcount)
                else {
                    var parentWin = window;
                    while (parentWin && parentWin != parentWin.parent) {
                        parentWin.__error__ = json.Message;
                        parentWin = parentWin.parent;
                    }
                    parentWin.__error__ = json.Message;
                    if (window.debug || window.location.host.indexOf('localhost') > -1)
                        my.error(json.Message);
                    my.error(json.Message);
                    return;
                }
            }
            callback && callback(json.result);

        });
    }

    my.ajaxInvoke = function (method, data, callback, serverUrl) {      
        if (!serverUrl) serverUrl = RoutePath;
        if (method.indexOf('.') < 0) {
            method = 'My.Application.' + method;
        }       
        return my.ajax(serverUrl + '?invokeMethod=' + method, { postdata: JSON.stringify(data) }, callback)

    };

    window.my = my;
   
    if (typeof module === 'object' && typeof module.exports === 'object') { module.exports = my; }
    if (typeof angular === 'object') {
        angular.module('api', []).factory('my', function () {
            return my;
        });
    }
    if (typeof define === 'function') { define(['jquery'], function ($) { return my; }); }

    return my;
   
})(window)


   
