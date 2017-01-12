
//页面初始化

var data = "";
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm);


    //下载文件
    $('.xiazaidoc').bind("click", function () {
        debugger;
        var Path = $(this).next().text();
        var fileName = 'AllReport';
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

    self.ReportID = data.ReportID;
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;

    self.reportDocList = ko.mapping.fromJS(data.resultModel);
    self.CountLength = data.resultModel.length;


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
