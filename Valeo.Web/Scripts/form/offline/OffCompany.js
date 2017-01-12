



//页面初始化
$(function () {

    $("ul.dropdown-menu").on("click", "[data-stopPropagation]", function (e) {
        e.stopPropagation();
    });
    $('#Tab a:first').tab('show'); //初始化显示哪个tab 
    $('#Tab a').click(function (e) {
        e.preventDefault();  //阻止a链接的跳转行为 
        $(this).tab('show'); //显示当前选中的链接及关联的content 
    });

    $("#radiodiv input[type=radio]").change(function () {


        //debugger;
        $("#radiodiv>.defautradio").removeClass("checkedradio");
        $(this).parent().parent().parent().addClass("checkedradio");

        //document.getElementById("checkbox").checked = false;
        //$('#checkbox').attr("checked", true);
    })

    $(document).on('change', '.myfile .uploathid', function () {
        $(this).parent().siblings().children(".txtfile").val($(this).val());
    });


    var vm = new ViewModel();
    ko.applyBindings(vm);

});


//定义ViewModel对象
var ViewModel = function () {
    var self = this;


    //重置公司
    self.Clear = function () {


        Clear(self);

    };

    //添加查询记录到表格中
    self.Add = function () {

        Add(self);
       
    };

    //移除某一行记录
    self.RemoveCom = function () {

        RemoveCom(self);

    };

    //移除某一行记录
    self.RemovePer = function () {

        RemovePer(self);

    };
    //公司查询信息提交
    self.SaveCompany = function (obj) {

        SaveCompany(self);

    };


    //公司查询信息提交
    self.SavePer = function (obj) {

        SavePer(self);

    };



    //重置公司
    self.ClearPer = function () {


        ClearPer(self);

    };

    //添加查询记录到表格中
    self.AddPer = function () {

        AddPer(self);
   
    };

};

function ClearPer() {

    ClearPer(self);

}

function ClearPer() {

    var sss = document.getElementById("per").getElementsByTagName("input");

    for (var i = 0; i < sss.length; i++) {

        debugger;
        if (sss[i].type == "checkbox") {

            sss[i].checked = false;

        }
        if (sss[i].localName == "input") {
            debugger;

            sss[i].value = "";

        }
    }


}

function Clear()
{

    var sss = document.getElementById("comp").getElementsByTagName("input");

    for (var i = 0; i < sss.length; i++) {

       
        if (sss[i].type == "checkbox") {

            sss[i].checked = false;

        }
        if (sss[i].localName == "input") {
           
 
            sss[i].value = "";

        }
        if (sss[i].type == "radio") {
            

            sss[i].checked = false;

        }
    }

   
}

function SaveCompany() {


    var SelectName = eval(document.getElementById("SelectName")).value;

    var obj = document.getElementById('SelectSalutation');
    var SelectSalutation = obj.options[obj.selectedIndex].text;//获取文本

    var SelectTel = eval(document.getElementById("SelectTel")).value;
    var SelectEmail = eval(document.getElementById("SelectEmail")).value;


    var tab = document.getElementById("tblData");
    if (tab.rows.length<2) {
        debugger;
        alert(OFC_MSG_001);
        return;
    }
    //表单
    var rpt_columns = new Array();
    var column = new Object();

    for (var i = 1; i < tab.rows.length; i++) {

       

        column = new Object();
        column.Name_En = tab.rows[i].cells[1].innerHTML;
        column.Name_Cn = tab.rows[i].cells[2].innerHTML;
        column.RegNo1 = tab.rows[i].cells[3].innerHTML;
        column.RegNo2 = tab.rows[i].cells[4].innerHTML;
        column.ComAddress = tab.rows[i].cells[5].innerHTML;
        var type = tab.rows[i].cells[6].value;
        debugger;
        if (type == "1_1") {
            column.ReportType = 1;
        } else {
            column.ReportType = type;
        }

        if (type == 3 || type == 4 || type == 6) {

       
            column.Remark = tab.rows[i].cells[6].innerHTML;
        }
        else
        {
            column.Remark = "";
        }
        rpt_columns.push(column);
    }

    $.ajax({
        url: "/CompanyOffline/SaveCompany",
        type: "post",
        ///                                           ComOrPer 0:公司 1:个人
        data: { SelectName: SelectName, SelectSalutation: SelectSalutation, SelectTel: SelectTel, SelectEmail: SelectEmail, MainJson: JSON.stringify(rpt_columns), ComOrPer: 0 },
        cache: false,
        async: false,
        dataType: "json",
        success: function (packJson) {

            if (packJson.status == 1) {

                alert(packJson.message);
                window.location.href = '/CompanyOffline/Index';
            }
            else {
                alert(packJson.message);
            }
        }
    });
}


//添加查询记录到表格中
function Add() {

    //选中的radio的ID
    var CheckId;
    //选中的radio的ID对应的查册内容个数
    var checkNum = 0;
    //所有选中的明细控件
    var CheckDetail

    //添加数据检查
    var NameEn = eval(document.getElementById("Name_En")).value;
    var NameCN = eval(document.getElementById("Name_Cn")).value;
    if (NameEn == "" && NameCN == "") {

        alert(OFC_MSG_002);
        return;
    }

    var gongsiqz = document.getElementById("gongsiqz");

    if (gongsiqz.checked == false) {



        var sss = document.getElementById("radiodiv").getElementsByTagName("input");
        //二者相等 var xx = $("#radiodiv input")


        for (var i = 0; i < sss.length; i++) {
            if (sss[i].type == "radio" && sss[i].checked == true) {

                if (true) {

                }
                CheckId = sss[i].id

            }
        }


        if (CheckId == undefined) {

            alert(OFC_MSG_003);
            return;
        }

        var CheckDetail = document.getElementsByName(CheckId)

        for (var i = 0; i < CheckDetail.length; i++) {
            if (CheckDetail[i].checked == true) {


                if (CheckDetail[i].id == 3) {


                    var year_3 = document.getElementById("year3").value;
                    if (year_3 == undefined || year_3 == "") {
                        alert(OFC_MSG_004);
                        year_3.focus();
                        return;
                    }
                }
                if (CheckDetail[i].id == 4) {
                    debugger;
                    var year_6 = document.getElementById("year4").value;
                    if (year_6 == undefined || year_6 == "") {
                        alert(OFC_MSG_004);
                        year_6.focus();
                        return;
                    }
                }
                if (CheckDetail[i].id == 6) {

                    var other_13 = document.getElementById("other6").value;
                    if (other_13 == undefined || other_13 == "") {
                        alert(OFC_MSG_005);
                        other_13.focus();
                        return;
                    }
                }
                checkNum = parseFloat(checkNum) + 1;
            }
        }
        if (checkNum < 1) {
            alert(OFC_MSG_006);
            return;
        }


        for (var i = 0; i < CheckDetail.length; i++) {
            if (CheckDetail[i].checked == true) {

                var tab = document.getElementById("tblData");
                var row = tab.insertRow(-1);


                var cell1 = row.insertCell(-1);
                var cell2 = row.insertCell(-1);
                var cell3 = row.insertCell(-1);
                var cell4 = row.insertCell(-1);
                var cell5 = row.insertCell(-1);
                var cell6 = row.insertCell(-1);
                var cell7 = row.insertCell(-1);
                var cell8 = row.insertCell(-1);


                var rowIndex = 0

                if (tab.rows.length != 2) {

                    var _row = tab.rows; //获取table的行
                    var _cell = _row[tab.rows.length - 2].cells;  //最末一行
                    rowIndex = _cell[0].innerHTML;             //第一个为该行编号

                };



                cell1.innerHTML = parseFloat(rowIndex) + 1;
                cell2.innerHTML = eval(document.getElementById("Name_En")).value;
                cell3.innerHTML = eval(document.getElementById("Name_Cn")).value;
                cell4.innerHTML = eval(document.getElementById("RegNo1")).value;
                cell5.innerHTML = eval(document.getElementById("RegNo2")).value;
                cell6.innerHTML = eval(document.getElementById("ComAddress")).value;


                //cell7.innerHTML = eval(document.getElementById("formGroupInputLarge")).value;
                var ssf = "";
                var Num = "";
                if (CheckDetail[i].id == 3) {


                     Num = document.getElementById("year3").value;
                     ssf = OFC_MSG_007 + Num + OFC_MSG_008;
                }
                if (CheckDetail[i].id == 4) {

                    Num = document.getElementById("year4").value;
                    ssf = OFC_MSG_009 + Num + OFC_MSG_010;
                }
                if (CheckDetail[i].id == 6) {

                    Num = document.getElementById("other6").value;
                    ssf = Num ;
                }
                if (CheckDetail[i].id != 3 && CheckDetail[i].id != 4 && CheckDetail[i].id != 6) {

                    ssf = CheckDetail[i].parentNode.innerHTML.split(";")[1];
                }
                cell7.innerHTML = ssf ;
                cell7.value = CheckDetail[i].id;

                cell8.innerHTML = '<button class="btn btn-danger btn-sm" onclick="Remove(this.parentNode.parentNode)" type="submit">' + OFC_MSG_011+ '</button>'

            }
        }

    }
    else
    {
        var tab = document.getElementById("tblData");
        var row = tab.insertRow(-1);


        var cell1 = row.insertCell(-1);
        var cell2 = row.insertCell(-1);
        var cell3 = row.insertCell(-1);
        var cell4 = row.insertCell(-1);
        var cell5 = row.insertCell(-1);
        var cell6 = row.insertCell(-1);
        var cell7 = row.insertCell(-1);
        var cell8 = row.insertCell(-1);


        var rowIndex = 0

        if (tab.rows.length != 2) {

            var _row = tab.rows; //获取table的行
            var _cell = _row[tab.rows.length - 2].cells;  //最末一行
            rowIndex = _cell[0].innerHTML;             //第一个为该行编号

        };



        cell1.innerHTML = parseFloat(rowIndex) + 1;
        cell2.innerHTML = eval(document.getElementById("Name_En")).value;
        cell3.innerHTML = eval(document.getElementById("Name_Cn")).value;
        cell4.innerHTML = eval(document.getElementById("RegNo1")).value;
        cell5.innerHTML = eval(document.getElementById("RegNo2")).value;
        cell6.innerHTML = eval(document.getElementById("ComAddress")).value;

        cell7.innerHTML = OFC_MSG_012;
        cell7.value = "10";
        cell8.innerHTML = '<button class="btn btn-danger btn-sm" onclick="RemoveCom(this.parentNode.parentNode)" type="submit">' + OFC_MSG_011 + '</button>'
    }
    Clear(self);
};

//移除某一行记录
function RemoveCom(row) {


    var tb = document.getElementById("tblData");

    var index = row.rowIndex; //有这个属性，嘿嘿
    tb.deleteRow(index)//这里的数字是行的索引

    //document.getElementById('tblData').deleteRow(obj.parentElement.parentElement.rowIndex);


};


function AddPer()
{

    //添加数据检查
    var NameEn = eval(document.getElementById("Name_EnPer")).value;
    var NameCN = eval(document.getElementById("Name_CnPer")).value;
    if (NameEn == "" && NameCN == "") {

        alert(OFC_MSG_013);
        return;
    }


    var Num = 0;
    var xx = $("#checkB input")
    var year13 = document.getElementById("year13");

    for (var i = 0; i < xx.length; i++) {
        if (xx[i].checked == true) {

            if (xx[i].id == '13' && (year13.value == "" || year13.value == undefined)) {
                alert(OFC_MSG_014);
                return;

            }
            Num++;
        }
    }
   
    if (Num==0) {
        alert(OFC_MSG_015);
        return;

    }

    for (var i = 0; i < xx.length; i++) {

        if (xx[i].checked==true) {


        var tab = document.getElementById("tblDataPer");
        var row = tab.insertRow(-1);


        var cell1 = row.insertCell(-1);
        var cell2 = row.insertCell(-1);
        var cell3 = row.insertCell(-1);
        var cell4 = row.insertCell(-1);
        var cell5 = row.insertCell(-1);
        var cell6 = row.insertCell(-1);
        var cell7 = row.insertCell(-1);



        var rowIndex = 0

        if (tab.rows.length != 2) {

            var _row = tab.rows; //获取table的行
            var _cell = _row[tab.rows.length - 2].cells;  //最末一行
            rowIndex = _cell[0].innerHTML;             //第一个为该行编号

        };



        cell1.innerHTML = parseFloat(rowIndex) + 1;
        cell2.innerHTML = eval(document.getElementById("Name_EnPer")).value;
        cell3.innerHTML = eval(document.getElementById("Name_CnPer")).value;
        cell4.innerHTML = eval(document.getElementById("RegNo1Per")).value;
        cell5.innerHTML = eval(document.getElementById("RegNo2Per")).value;
        debugger;
        if (xx[i].id != 13) {

            ssf = xx[i].parentNode.innerHTML.split(";")[1];
        }
        else
        {

            ssf = eval(document.getElementById("year13")).value;;
        }
        cell6.innerHTML = ssf;
        cell6.value = xx[i].id;
        cell7.innerHTML = '<button class="btn btn-danger btn-sm" onclick="RemovePer(this.parentNode.parentNode)" type="submit">' + OFC_MSG_011 + '</button>'
        }
    }
    ClearPer(self);
}


function SavePer() {


    var SelectName = eval(document.getElementById("SelectNamePer")).value;

    var obj = document.getElementById('SelectSalutationPer');
    var SelectSalutation = obj.options[obj.selectedIndex].text;//获取文本

    var SelectTel = eval(document.getElementById("SelectTelPer")).value;
    var SelectEmail = eval(document.getElementById("SelectEmailPer")).value;


    var tab = document.getElementById("tblDataPer");
    if (tab.rows.length < 2) {

        alert(OFC_MSG_016);
        return;
    }
    //表单
    var rpt_columns = new Array();
    var column = new Object();

    for (var i = 1; i < tab.rows.length; i++) {



        column = new Object();
        column.Name_En = tab.rows[i].cells[1].innerHTML;
        column.Name_Cn = tab.rows[i].cells[2].innerHTML;
        column.RegNo1 = tab.rows[i].cells[3].innerHTML;
        column.RegNo2 = tab.rows[i].cells[4].innerHTML;
        column.ReportType = tab.rows[i].cells[5].value;

        debugger;
        if (tab.rows[i].cells[5].value == 13) {


            column.Remark = tab.rows[i].cells[5].innerHTML;
        }
        else {
            column.Remark = "";
        }
        rpt_columns.push(column);
    }

    $.ajax({
        url: "/CompanyOffline/SavePer",
        type: "post",
        ///                                           ComOrPer 0:公司 1:个人
        data: { SelectName: SelectName, SelectSalutation: SelectSalutation, SelectTel: SelectTel, SelectEmail: SelectEmail, MainJson: JSON.stringify(rpt_columns), ComOrPer: 1 },
        cache: false,
        async: false,
        dataType: "json",
        success: function (packJson) {

            
            if (packJson.status == 1) {

                alert(packJson.message);
                window.location.href = '/CompanyOffline/Index';
            }
            else {
                alert(packJson.message);
            }
        }
    });
}

//移除某一行记录
function RemovePer(row) {


    var tb = document.getElementById("tblDataPer");

    var index = row.rowIndex; //有这个属性，嘿嘿
    tb.deleteRow(index)//这里的数字是行的索引

    //document.getElementById('tblData').deleteRow(obj.parentElement.parentElement.rowIndex);


};