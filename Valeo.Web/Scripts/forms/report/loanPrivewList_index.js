/// <reference path="../../knockout-2.0.0.js" />
/// <reference path="../../knockout-2.0.0.debug.js" />
//页面初始化

var data = "";
$(function () {
    debugger;
    var vm = new ViewModel();
    ko.applyBindings(vm);

    //下载文件
    $('.xiazaidoc').bind("click", function () {

        var tmpData = new Date();
        var Path = $(this).next().text();
        var fileName = $(this).children('span').text() + '_LoanDoc' + tmpData.valueOf();
        if (Path != "null" && Path.length > 0 && Path != null) {

            $.post("/DownloadFile/Exists", { id: Path }, function (e) {

                if (e.type) {
                    var tmpurl = '/DownloadFile/Uploads?filePath=' + Path + '&flieName=' + fileName

                    window.open(tmpurl);
                } else {
                    $.messager.alert('提示信息', "文件不存在，无法下载。", 'error')
                    return;
                }
            });
        }
    });
});


var ViewModel = function () {
    debugger;
    var self = this;
    if (data.type == 0) {
        layer.alert(data.message);
    }
    console.log(data);
    self.MemberID = data.MemberID;
    self.MemberName = data.MemberName
    self.SelectName = data.SelectName
    self.Content = data.Content;
    self.NumberID = data.NumberID
    self.Idtype = data.Idtype
    self.AddTime = data.AddTime
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;
    self.ReportID = data.ReportID;

    self.searchList = ko.mapping.fromJS(data.lstCreditOnlineXQVM);//
    if (data.lstCreditOnlineXQVM.length) {
        self.Borrower = data.lstCreditOnlineXQVM[0].Borrower_En + " " + data.lstCreditOnlineXQVM[0].Borrower_Cn;
        self.Alias = data.lstCreditOnlineXQVM[0].Alias_En + " " + data.lstCreditOnlineXQVM[0].Alias_Cn;
        self.IDNumber = data.lstCreditOnlineXQVM[0].IDNumber;
        self.Gender = data.lstCreditOnlineXQVM[0].Gender;
        self.Type = data.lstCreditOnlineXQVM[0].Type;
        self.Birthdate = data.lstCreditOnlineXQVM[0].Birthdate;
        self.Marital = data.lstCreditOnlineXQVM[0].Marital;
        self.Address = data.lstCreditOnlineXQVM[0].Address_En + " " + data.lstCreditOnlineXQVM[0].Address_Cn;
        self.LenghtTime = data.lstCreditOnlineXQVM[0].LenghtTime;
        self.TelNo1 = data.lstCreditOnlineXQVM[0].TelNo1;
        self.Employer = data.lstCreditOnlineXQVM[0].Employer;
        self.Department = data.lstCreditOnlineXQVM[0].Department;
        self.Position = data.lstCreditOnlineXQVM[0].Position;
        self.OfficeAddress = data.lstCreditOnlineXQVM[0].OfficeAddress;
        self.ServicesTime = data.lstCreditOnlineXQVM[0].ServicesTime;
        self.TelNo2 = data.lstCreditOnlineXQVM[0].TelNo2;
        self.TelNo3 = data.lstCreditOnlineXQVM[0].TelNo3;
    } else {
        self.Borrower = null;
        self.Employer = null;
    }

    self.reportDocList = data.resultModel;
    self.CountLength = data.lstCreditOnlineXQVM?0:data.lstCreditOnlineXQVM.length;

    //导出
    self.export = function () {
        var form = $("#frmConds");//定义一个form表单 
        $("#txtParam").val(ko.mapping.toJSON(data));
        form.submit();//表单提交 
    };
};