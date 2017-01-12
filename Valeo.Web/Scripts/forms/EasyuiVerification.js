$(function () {
    $.extend($.fn.validatebox.defaults.rules, {
        //验证汉字  
        CHS: {
            validator: function (value) {
                return /^[\u0391-\uFFE5]+$/.test(value);
            },
            message: '请输入汉字'
        },
        //验证英文  
        EN: {
            validator: function (value) {
                return /^[^\u4e00-\u9fa5]{0,}$/.test(value);
            },
            message: '请输入英文'
        },
        //只能输入数字和英文包含空格
        ENNUM:
        {
            validator: function (value) {
                return /^[0-9a-zA-Z\ ]*$/g.test(value);
            },
            message: '只能输入数字,英文和空格'
        },
        //只能输入数字和英文不包含空格
        ENNUM2:
        {
            validator: function (value) {
                return /^[0-9a-zA-Z]*$/g.test(value);
            },
            message: '只能输入数字和英文'
        },
        //不能有中文
        ENNUMC:
        {
            validator: function (value) {
                return /^[^\u4e00-\u9fa5]+$/.test(value);
            },
            message: '不能输入中文'
        },

        //不能输入特殊符号 
        TS: {
            validator: function (value) {
                return /^[\u4E00-\u9FA5A-Za-z0-9]+$/.test(value);
            },
            message: '不能输入特殊符号'
        },
        //验证密码
        PASS: {
            validator: function (value) {
                return /^(?![a-zA-Z0-9]+$)(?![^a-zA-Z/D]+$)(?![^0-9/D]+$).{8,64}$/.test(value);
            },
            message: '密码为大于8位且小于64位的英文字母、符号与数字的组合'
        },
        //电话号码验证  
        Mobile: {
            //value值为文本框中的值  
            validator: function (value) {
                var reg = /^[0-9\+ -]*$/;
                return reg.test(value);
            },
            message: '请输入正确的号码'
        },
        //传真  
        Fax: {
            validator: function (value) {
                var reg = /^[0-9]*$/;
                return reg.test(value);
            },
            message: '请输入正确的传真'
        },
        Emile: {
            //value值为文本框中的值  
            validator: function (value) {
                var reg = /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/;
                return reg.test(value);
            },
            message: '请输入正确的电邮'
        },
        //国内邮编验证  
        ZipCode: {
            validator: function (value) {
                var reg = /^[0-9]\d{5}$/;
                return reg.test(value);
            },
            message: 'The zip code must be 6 digits and 0 began.'
        },
        //数字  
        Number: {
            validator: function (value) {
                var reg = /^[0-9]*$/;
                return reg.test(value);
            },
            message: '只能输入数字'
        },
        Product: {
            validator: function (value) {
                var reg = /^[+]?(?:0|[1-9]\d*)(?:\.\d{1,2})?$/;
                return reg.test(value);
            },
            message: '请输入正确金额'
        },
        DateDay: {
            validator: function (value) {
                var reg = /^(\d{4})-(0\d{1}|1[0-2])-(0\d{1}|[12]\d{1}|3[01])$/;
                return reg.test(value);
            },
            message: '请输入正确的日期'
        },
        DateDayS: {
            validator: function (value) {
                var reg = /^(((\d{4})-(0\d{1}|1[0-2])-(0\d{1}|[12]\d{1}|3[01])),)*((\d{4})-(0\d{1}|1[0-2])-(0\d{1}|[12]\d{1}|3[01]))$/;
                return reg.test(value);
            },
            message: '日期请按正确的格式输入，多个用逗号分隔'
        },
        DateDaySS: {
            validator: function (value) {
                var reg = /^(\d{2}|\d{4})(?:\-)?([0]{1}\d{1}|[1]{1}[0-2]{1})(?:\-)?([0-2]{1}\d{1}|[3]{1}[0-1]{1})(?:\s)?([0-1]{1}\d{1}|[2]{1}[0-3]{1})(?::)?([0-5]{1}\d{1})(?::)?([0-5]{1}\d{1})$/;
                return reg.test(value);
            },
            message: '请选择正确的时间'
        },
        Ages: {
            validator: function (value) {
                var reg = /^((([1-9]|[1-9]\d)|100),)*(([1-9]|[1-9]\d)|100)$/;
                return reg.test(value);
            },
            message: '请按照说明正确输入年龄'
        },
        Genders: {
            validator: function (value) {
                var reg = /^([\u7537\u5973],)*[\u7537\u5973]$/;
                return reg.test(value);
            },
            message: '0.男   1.女   多个用逗号分隔'
        },
        Maritals: {
            validator: function (value) {
                if (value == "") {
                    return true;
                }
                else {
                    var bl = true;
                    var valmaritals = value.split(',');
                    for (var i = 0; i < valmaritals.length; i++) {
                        if (valmaritals[i] != "未婚" && valmaritals[i] != "已婚" && valmaritals[i] != "离婚" && valmaritals[i] != "鳏寡" && valmaritals[i] != "其他") {
                            bl = false;
                            break;
                        }
                    }
                    return bl;
                }
                //var reg = /^([0-4],)*[0-4]$/;
                //return reg.test(value);
            },
            message: '请从“未婚/已婚/离婚/鳏寡/其他”中选取1个填写，多个用逗号分隔'
        },        //验证汉字  
        CHSs: {
            validator: function (value) {
                return /^(([\u0391-\uFFE5]+),)*([\u0391-\uFFE5]+)+$/.test(value);
            },
            message: '请输入汉字多个已半角逗号分'
        },
        //验证英文  
        ENs: {
            validator: function (value) {
                return /^(([A-Za-z\ 0-9,.]+),)*([0-9A-Za-z\ ,.]+)$/.test(value);
            },
            message: '请输入英文.'
        },
        EmptyString: {
            validator: function (value) {
                return value.replace(/^\s*|\s*$/g, "").length > 0;
            },
            message: '不能由空字符串组成'
        },
        equals: {
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: '两次输入不一致'
        },
        number2: {
            validator: function (value, param) {
                return /^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(value) && parseInt(value, 10) >= parseInt(param[0], 10);
            },
            message: '请输入在大于或等于{0}的数字.'
        },
        digits: {
            validator: function (value, param) {
                return /^\d+$/.test(value) && parseInt(value, 10) >= parseInt(param[0], 10);
            },
            message: '请输入在大于或等于{0}的数字.'
        },
        isValidityDigits: {
            validator: function (value, param) {
                return tmpmonths.includes(value) ? true : /^\d+$/.test(value) && parseInt(value, 10) >= parseInt(param[0], 10);
            },
            message: '请输入在大于或等于{0}的数字.'
        },
        isChineseChar: {
            validator: function (value, param) {
                if (/[a-zA-Z]/gi.test(value)) {
                    return false;
                }
                return /[\u4E00-\u9FA5\uF900-\uFA2D]/.test(value);
            },
            message: '请输入中文，谢谢.'
        },
        isEnglishChar: {
            validator: function (value, param) {
                return !/[\u4E00-\u9FA5\uF900-\uFA2D]/.test(value);
            },
            message: '请输入英语，谢谢.'
        }
    }
    );
});

function fullnum(stsTimeid, Endtimeid) {
    //var stsT=new Date($('#'+stsTimeid).datebox('getValue'));
    var stsT = $('#' + stsTimeid).datebox('getValue');
    var endT = $('#' + Endtimeid).datebox('getValue');

    if (stsT != "" && endT != "" && new Date(stsT) > new Date(endT)) {
        //$.messager.alert(title, timeMsg, "info");
        //只要选择了日期，不管是开始或者结束都对比一下，如果结束小于开始，则清空结束日期的值并弹出日历选择框   .datebox('showPanel')
        $('#' + Endtimeid).datebox('setValue', '');
        return false;
    }
    return true;
}


var numberboxEdit = { type: 'numberbox', options: { min: 0 } };
var validateboxDateBox = { type: 'datebox', options: { required: true } };
var validateboxDateBoxNo = { type: 'datebox', options: {} };
var validatedatetimebox = { type: 'datetimebox', options: { required: true } };
var validatedatetimeboxNo = { type: 'datetimebox', options: {} };

var validateboxTextBoxNo128EN = { type: 'validatebox', options: { validType: ['isEnglishChar', 'length[0,128]'] } };
var validateboxTextBoxNo128CN = { type: 'validatebox', options: { validType: ['isChineseChar', 'length[0,128]'] } };

var validateboxTextBox128 = { type: 'validatebox', options: { required: true, validType: 'length[0,128]' } };
var validateboxTextBoxNo128 = { type: 'validatebox', options: { validType: 'length[0,128]' } };
var validateboxTextBox = { type: 'validatebox', options: { required: true, validType: 'length[0,64]' } };
var validateboxTextBoxNo = { type: 'validatebox', options: { validType: 'length[0,64]' } };
var validateboxTextBox32 = { type: 'validatebox', options: { required: true, validType: 'length[0,32]' } };
var validateboxTextBoxNo32 = { type: 'validatebox', options: { validType: 'length[0,32]' } };
var validateboxTextBox16 = { type: 'validatebox', options: { required: true, validType: 'length[0,16]' } };
var validateboxTextBoxNo16 = { type: 'validatebox', options: { validType: 'length[0,16]' } };

var validateboxNumberEdit = { type: 'validatebox', options: { required: true, validType: 'number2[0]' } };
var validateboxdigitsEdit = { type: 'validatebox', options: { required: true, validType: 'digits[0]' } };


