/*  this.optional(element) 用于表单控件的值不为空时才触发验证  */
(function () {
    //覆写默认的date校验，需要moment.js的支持
    jQuery.validator.methods.date = function (value, element, param) {
        if (this.optional(element)) {
            return true;
        }

        if (!moment) {
            throw '没有引用moment.js';
        }
        var date = moment(value);
        return date.isValid();
    };

    // 手机号码校验
    //正确的手机号码(如:13800571506 013800571505)则返回ture,否则返回false
    jQuery.validator.addMethod('mobilephone', function (value, element, param) {
        if (this.optional(element)) {
            return true;
        }
        var mobilephoneNumPat = /^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}$/;
        var matchArray = value.match(mobilephoneNumPat);
        return matchArray != null;
    });

    //电话号码校验
    //正确的电话号码（包括区号和“-”如0571-1234567[8] 010-1234567[8] ）则返回ture,否则返回false
    jQuery.validator.addMethod('telphone', function (value, element, param) {
        if (this.optional(element)) {
            return true;
        }
        var telphoneNumPat = /^0d{2}-d{7,8}|0d{3}-d{7,8}$/;
        var matchArray = value.match(telphoneNumPat);
        return matchArray != null;
    });

    //18位身份证验证,输入正确的号码返回ture,否则返回false
    jQuery.validator.addMethod('idcard', function (value, element, param) {
        if (this.optional(element)) {
            return true;
        }
        var date, Ai;
        var verify = "10x98765432";
        var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
        var area = ['', '', '', '', '', '', '', '', '', '', '', '北京', '天津', '河北', '山西', '内蒙古', '', '', '', '', '', '辽宁', '吉林', '黑龙江', '', '', '', '', '', '', '', '上海', '江苏', '浙江', '安微', '福建', '江西', '山东', '', '', '', '河南', '湖北', '湖南', '广东', '广西', '海南', '', '', '', '重庆', '四川', '贵州', '云南', '西藏', '', '', '', '', '', '', '陕西', '甘肃', '青海', '宁夏', '新疆', '', '', '', '', '', '台湾', '', '', '', '', '', '', '', '', '', '香港', '澳门', '', '', '', '', '', '', '', '', '国外'];
        var re = value.match(/^(\d{2})\d{4}(((\d{2})(\d{2})(\d{2})(\d{3}))|((\d{4})(\d{2})(\d{2})(\d{3}[x\d])))$/i);
        if (re == null) return false;
        if (re[1] >= area.length || area[re[1]] == "") return false;
        if (re[2].length == 12) {
            Ai = value.substr(0, 17);
            date = [re[9], re[10], re[11]].join("-");
        }
        else {
            Ai = value.substr(0, 6) + "19" + value.substr(6);
            date = ["19" + re[4], re[5], re[6]].join("-");
        }
        var temp = moment(date);
        if (!temp.isValid) {
            return false;
        }
        var sum = 0;
        for (var i = 0; i <= 16; i++) {
            sum += Ai.charAt(i) * Wi[i];
        }
        Ai += verify.charAt(sum % 11);
        return (value.length == 15 || value.length == 18 && value == Ai);
    });

    var $Unob = jQuery.validator.unobtrusive.adapters;
    $Unob.addBool('mobilephone');
    $Unob.addBool('telphone');
    $Unob.addBool('idcard');
})();
