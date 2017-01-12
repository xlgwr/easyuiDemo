//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);

    //下载文件
    $('.xiazaidoc').bind("click", function () {
        var tmpData = new Date();
        var Path = $(this).next().text();
        var fileName = $(this).children('span').text() + '_PublicReport' + tmpData.valueOf();

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


function GetRequest(url) {
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

var ViewModel = function () {
    var self = this;
    console.log(data);
    if (data.type == 0) {
        layer.alert(data.message);
    }
    self.MemberID = data.MemberID;
    self.MemberName = data.MemberName;
    self.SelectName = data.SelectName;
    self.Content = data.Content;
    self.SmallType = data.SmallType;
    self.AddTime = data.AddTime;
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;
    self.ReportID = data.ReportID;
    self.searchList = ko.mapping.fromJS(data.lstSelectListItem);
    self.CountLength = data.allCount;
    self.allCount = data.allCount;
    self.resultModel = data.resultModel;
    self.reportDocList = data.resultModel;
    //导出
    self.export = function () {

        var form = $("#frmConds");//定义一个form表单 
        $("#txtParam").val(ko.mapping.toJSON(data));
        form.submit();//表单提交 
    };
};