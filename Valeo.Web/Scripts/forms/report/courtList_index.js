
//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm);
    $("#SortCaseID").val(Type);
    $("#SortCaseID  option[value=" + Type + "] ").attr("selected", true);
    $("#SortCaseID").change(function () {
        window.location.href = '/CourtPrivew/GenerateHTML?flag=' + flag + '&TaskID=' + id + '&ReportID=' + ReportID + '&BigType=' + BigType + '&Type=' + $("#SortCaseID").val();
    });

    //下载文件
    $('.xiazaidoc').bind("click", function () {

        var Path = $(this).next().text();
        var tmpData = new Date();
        var fileName = $(this).children('span').text() + '_CourtReport' + tmpData.valueOf();
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
function isObjNull(obj) {
    if (obj) {
        try {
            if (obj==null) {
                return true;
            }
        } catch (e) {
            return false;
        }
        return false;
    } 
    return true;
  
}
//定义ViewModel对象
var ViewModel = function () {
    debugger;
    var self = this;
    if (data.type == 0) {
        layer.alert(data.message, { title: "提示信息" });
    }
    self.MemberID = data.MemberID;
    self.MemberName = data.MemberName;
    self.SelectName = data.SelectName;
    self.Content = data.Content;
    self.CourtType = data.CourtType;
    self.AgeLimit = data.AgeLimit;
    self.Remark = data.Remark;

    self.AddTime = data.lstCaseReport[0].addtime;
    self.ReportID = data.lstCaseReport[0].ReportID;
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;
    self.JudgementText = data.lstCaseReport[0].JudgementText;
    self.searchList = ko.mapping.fromJS(data.lstCaseReport);

    if (data.lstCaseReport) {
        if (data.lstCaseReport.length == 0) {
            self.CountLength = 0;
        } else {
            self.CountLength = isObjNull(data.lstCaseReport[0].CaseNo) ? 0 : data.totalCount;
        }
    } else {
        self.CountLength = 0;
    }

    self.reportDocList = ko.mapping.fromJS(data.resultModel);

    if (data.resultModel) {
        if (data.resultModel.length == 0) {
            self.docCountLength = 0;
        } else {
            self.docCountLength = data.resultModel.length;
        }
    }
    var samet = 0;
    for (var i = 0; i < data.lstCaseReport.length; i++) {
        if (data.lstCaseReport[0].Same == 1)
            samet = 1;
    }
    self.samet = samet;

    model.pageIndex = data.pageIndex;
    model.totalCount = data.totalCount;

    if (model.totalCount == 0) {
        model.totalCount = 1;
    }

    //导出
    self.export = function () {

        //window.location.href = '/CourtMintor/Download?json=' + ko.mapping.toJSON(data);

        var form = $("#frmConds");//定义一个form表单 
        $("#txtParam").val(ko.mapping.toJSON(data));
        form.submit();//表单提交 

        //$.ajax({
        //    type: "post",
        //    url: "/CourtMintor/Down",
        //    dataType: "json",
        //    data: { json: data },
        //    success: function (json) {
        //       layer.alert(json.result);
        //        self.persons.remove(obj);
        //    }
        //});
    };
    console.log(self);
};
