//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);

    var Request = new Object();
    Request = GetRequest(window.location.search);

    //console.log('***********************************')
    //console.log(Request)

    var flag = Request["flag"];
    var TaskID = Request["taskId"];
    var BigType = Request["bigType"];
    var SmallType = Request["SmallType"];

    if (!SmallType) {
        console.log(Request)
        return;
    }
    $.ajax({
        type: "post",
        url: "/CompanyPrivew/Details",
        data: {
            flag: flag,
            TaskID: TaskID,
            BigType: BigType,
            SmallType:SmallType
        },
        success: function (json) {
            debugger;
            if (json.CompanyOnlineVM2[0].FullName_En == null) {
                json.CompanyOnlineVM2[0].FullName_En = '';
            }
            if (json.CompanyOnlineVM2[0].FullName_Cn == null) {
                json.CompanyOnlineVM2[0].FullName_Cn = '';
            }
            if (json.CompanyOnlineVM2[0].CompanyType == null) {
                json.CompanyOnlineVM2[0].CompanyType = '';
            }
            if (json.CompanyOnlineVM2[0].CRNo == null) {
                json.CompanyOnlineVM2[0].CRNo = '';
            }
            if (json.CompanyOnlineVM2[0].PlaceRegistration == null) {
                json.CompanyOnlineVM2[0].PlaceRegistration = '';
            }
            if (json.CompanyOnlineVM2[0].PhoneNumber == null) {
                json.CompanyOnlineVM2[0].PhoneNumber = '';
            }
            if (json.CompanyOnlineVM2[0].Email == null) {
                json.CompanyOnlineVM2[0].Email = '';
            }
            $("#FullName_EN").text(json.CompanyOnlineVM2[0].FullName_En )
            $("#FullName_CN").text(json.CompanyOnlineVM2[0].FullName_Cn);
            $("#CompanyType").text(json.CompanyOnlineVM2[0].CompanyType );
            $("#CRNo").text(json.CompanyOnlineVM2[0].CRNo );
            $("#PlaceRegistration").text(json.CompanyOnlineVM2[0].PlaceRegistration);
            $("#PhoneNumber").text(json.CompanyOnlineVM2[0].PhoneNumber);
            $("#Email").text(json.CompanyOnlineVM2[0].Email );
            var ShareholdersName = "";
            var DirectorsName = "";
            for (var i = 0; i < json.Shareholders.length; i++) {

                if (json.Shareholders[i].FullName_En == null) {
                    json.Shareholders[i].FullName_En = '';
                }
                if (json.Shareholders[i].FullName_Cn == null) {
                    json.Shareholders[i].FullName_Cn = '';
                }
                ShareholdersName += json.Shareholders[i].FullName_En + "" + json.Shareholders[i].FullName_Cn + ";";
            }

            $("#ShareholdersName").text(ShareholdersName);
            for (var i = 0; i < json.Directors.length; i++) {

                if (json.Directors[i].FullName_En == null) {
                    json.Directors[i].FullName_En = '';
                }
                if (json.Directors[i].FullName_Cn == null) {
                    json.Directors[i].FullName_Cn = '';
                }
                DirectorsName += json.Directors[i].FullName_En + "" + json.Directors[i].FullName_Cn + ";";
            }
            $("#DirectorsName").text(DirectorsName );
            var _html = "";
            for (var i = 0; i < json.CompanyChange.length; i++) {

                if (json.CompanyChange[i].EffectiveDate == null) {
                    json.CompanyChange[i].EffectiveDate = '';
                }
                if (json.CompanyChange[i].ChangeContent_En == null) {
                    json.CompanyChange[i].ChangeContent_En = '';
                }
                if (json.CompanyChange[i].ChangeContent_Cn == null) {
                    json.CompanyChange[i].ChangeContent_Cn = '';
                }
                _html += "<tr><td class='text-primary'>" + json.CompanyChange[i].EffectiveDate + "</td>";
                _html += "<td class='text-primary'>" + json.CompanyChange[i].ChangeContent_En + "  " + json.CompanyChange[i].ChangeContent_Cn + "</td></tr>";
            }
            $("#tbod").append(_html)
            _html = "";
            if (BigType == 0 && flag == 0) {
                var ComLength = Math.ceil(json.CompanyReport.length / 4);
                for (var i = 0; i < ComLength; i++) {
                    var claj = "";
                    var clao = "";
                    if (i % 2 == 0) {
                        claj = "class='borderr'";
                    }
                    else {
                        clao = "class='borderr'";
                    }
                    var a = 0;
                    a = i * 4;
                    var td1 = a < json.CompanyReport.length ? json.CompanyReport[a].ReportName : "";
                    var Pa1 = a < json.CompanyReport.length ? json.CompanyReport[a].PathExcel : "";
                    a = a + 1;
                    var td2 = a < json.CompanyReport.length ? json.CompanyReport[a].ReportName : "";
                    var Pa2 = a < json.CompanyReport.length ? json.CompanyReport[a].PathExcel : "";
                    a = a + 1;
                    var td3 = a < json.CompanyReport.length ? json.CompanyReport[a].ReportName : "";
                    var Pa3 = a < json.CompanyReport.length ? json.CompanyReport[a].PathExcel : "";
                    a = a + 1;
                    var td4 = a < json.CompanyReport.length ? json.CompanyReport[a].ReportName : "";
                    var Pa4 = a < json.CompanyReport.length ? json.CompanyReport[a].PathExcel : "";
                    _html += "<tr>";
                    _html += "<td width='25%' " + claj + "><a class='xiazai' href='#'>" + td1 + "</a><label style='display: none;'>" + Pa1 + "</label></td>";
                    _html += "<td width='25%' " + clao + "><a class='xiazai' href='#'>" + td2 + "</a><label style='display: none;'>" + Pa2 + "</label></td>";
                    _html += "<td width='25%' " + claj + "><a class='xiazai' href='#'>" + td3 + "</a><label style='display: none;'>" + Pa3 + "</label></td>";
                    _html += "<td width='25%' " + clao + "><a class='xiazai' href='#'>" + td4 + "</a><label style='display: none;'>" + Pa4 + "</label></td>";
                    _html += "</tr>"
                }
            }
            else if (BigType == 1 || flag == 1) {
                var ReportLength = Math.ceil(json.ReportDocPath.length / 4);
                for (var i = 0; i < ReportLength; i++) {
                    var claj = "";
                    var clao = "";
                    if (i % 2 == 0) {
                        claj = "class='borderr'";
                    }
                    else {
                        clao = "class='borderr'";
                    }
                    var a = 0;
                    a = i * 4;
                    var td1 = a < json.ReportDocPath.length ? json.ReportDocPath[a].ReportName : "";
                    var Pa1 = a < json.ReportDocPath.length ? json.ReportDocPath[a].Path : "";
                    a = a + 1;
                    var td2 = a < json.ReportDocPath.length ? json.ReportDocPath[a].ReportName : "";
                    var Pa2 = a < json.ReportDocPath.length ? json.ReportDocPath[a].Path : "";
                    a = a + 1;
                    var td3 = a < json.ReportDocPath.length ? json.ReportDocPath[a].ReportName : "";
                    var Pa3 = a < json.ReportDocPath.length ? json.ReportDocPath[a].Path : "";
                    a = a + 1;
                    var td4 = a < json.ReportDocPath.length ? json.ReportDocPath[a].ReportName : "";
                    var Pa4 = a < json.ReportDocPath.length ? json.ReportDocPath[a].Path : "";
                    _html += "<tr>";
                    _html += "<td width='25%' " + claj + "><a class='xiazai' href='#'>" + td1 + "</a><label style='display: none;'>" + Pa1 + "</label></td>";
                    _html += "<td width='25%' " + clao + "><a class='xiazai' href='#'>" + td2 + "</a><label style='display: none;'>" + Pa2 + "</label></td>";
                    _html += "<td width='25%' " + claj + "><a class='xiazai' href='#'>" + td3 + "</a><label style='display: none;'>" + Pa3 + "</label></td>";
                    _html += "<td width='25%' " + clao + "><a class='xiazai' href='#'>" + td4 + "</a><label style='display: none;'>" + Pa4 + "</label></td>";
                    _html += "</tr>"
                }
            }
            $("#tab").append(_html)
        }
        , error: function (e) {
            console.log(e);
        }
    });

    $('.xiazai').bind("click", function () {
         
        //var Path = $(this).next().text();
        //if (Path != "null" && Path.length > 0 && Path != null) {

        //    window.location.href = "/CompanyPrivew/DownFile?filePath=" + Path + "";
        //    $("#xzp").append('<p>' + json.value + '</p>')
        //}        
        var Path = $(this).next().text();
        var fileName = 'CompanyReport';
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

};