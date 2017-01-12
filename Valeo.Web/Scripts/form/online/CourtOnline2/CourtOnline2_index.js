

//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);
    var Request = new Object();
    Request = GetRequest(window.location.search);
    
    $.ajax({
        type: "post",
        dataType: "json",
        data: { TaskID: Request["TaskID"] },
        url: '/CourtOnline2/GetList', //回发到的页面   
        success: function (data) {
            var row = data.rows[0];
            var key = Request["txtKEY"];
            var selectContent = "";
            var AgeLimit = "";
            var CourtType = "";
            if (key == 'DEN') {
                selectContent = beigao + row["D_PerName_En"]
            }
            else if (key == 'DCN') {
                selectContent = beigao + row["D_PerName_Cn"]
            }
            else if (key == 'PEN') {
                selectContent = yuangao + row["P_PerName_En"]
            }
            else if (key == 'PCN') {
                selectContent = yuangao + row["P_PerName_Cn"]
            }
            else if (key == 'CNO') {
                selectContent = strCaseNo + row["Case_No"]
            }
            else if (key == 'RPT') {
                selectContent = strCaseNo + row["Case_No"]
            }
            else if (key == 'ADD1') {
                selectContent = row["Street"] + row["BuildName"]
            }
            else if (key == 'ADD2') {
                selectContent = row["LotType"] + row["LotNo"]
            }
            if (row["AgeLimit"] == 0) {
                AgeLimit = "7年";
            }
            else {
                AgeLimit = "全部";
            }
            if (row["CourtType"] == 0) {
                CourtType = "所有法庭";
            }
            if (row["CourtType"] == 1) {
                CourtType = "高等法庭及区域法院及小额钱债";
            }
            if (row["CourtType"] == 2) {
                CourtType = "破产案/个人债务重组";
            }
            //console.log(data);
            $("#txtMemberID").text(row["MemberID"]);
            $("#txtTSelectName").text(row["SelectName"]);
            $("#txtCourt").text(CourtType);
            $("#txtPeriod").text(AgeLimit);
            $("#txtSelectContent").text(selectContent);
            $("#txtTRemark").text(row["Remark"]);
        }
    }); 
    $.ajax({
        type: "post",
        dataType: "json",
        data: { txtKEY: Request["txtKEY"], Entityid: Request["Entityid"], CaseNo: Request["Case_No"], Caseid: Request["Caseid"] },
        url: '/CourtOnline2/GetCaseList', //回发到的页面   
        success: function (data) {
            var row = data.rows[0];
            $("#txtTCase_No").text(row["CaseNoNew"]);
            $("#txtP_PerName").text(row["Plaintiff"]);
            $("#txtD_PerName").text(row["Defendant"]);
            $("#txtCrime").text(row["Cause"]);
            $("#txtCrimeType").text(row["Nature"]);
            $("#txtMoney").text(row["Currency"]);
            $("#txtJudge").text(row["Judge"]);
        }
    });
});

function GetRequest(url) {
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split(":")[0]] = unescape(strs[i].split(":")[1]);
        }
    }
    return theRequest;
}


//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //编辑
    self.GET = function () {
        
    };
};