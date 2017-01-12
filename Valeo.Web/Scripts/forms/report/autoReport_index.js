
//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm);
    $("#SortCaseID").val(Type);
    $("#SortCaseID  option[value=" + Type + "] ").attr("selected", true);
    $("#SortCaseID").change(function () {
        window.location.href = '/AutoMontoring/GenerateHTML?flag=' + flag + '&TaskID=' + id + '&TaskListID=' + TaskListID + '&ReportID=' + ReportID + '&Type=' + $("#SortCaseID").val();
    });

    //下载文件
    $('.xiazaidoc').bind("click", function () {

        var Path = $(this).next().text();
        var fileName = 'AutoMontoringReport';
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
    self.Remark = data.Remark;

    self.CountLength = data.lstCaseReport[0].CaseNo == null ? 0 : data.totalCount;
    self.AddTime = data.lstCaseReport[0].addtime;
    self.ReportID = data.ReportID;
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;
    self.JudgementText = data.lstCaseReport[0].JudgementText;
    self.searchList = ko.mapping.fromJS(data.lstCaseReport);

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

};
