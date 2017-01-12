
//初始化
$( function(){

    var vm = new ViewModel();
    ko.applyBindings(vm);
});


//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //添加
    self.Add = function () {

        Add(self)
    };


    //重置
    self.Clear = function () {

      
        Clear(self)

    };

    //重置
    self.Save = function () {


        Save(self)

    };
};

//追加
function Add() {



    //添加检查
    var lstCtl = document.getElementById("dataRcd").getElementsByTagName("input");
    //添加条数
    var Qty = 0;
    for (var i = 0; i < lstCtl.length; i++) {

        if (lstCtl[i].type == "checkbox" && lstCtl[i].checked == true) {
            Qty++;
            var idDetail = lstCtl[i].value
            if (idDetail == 'type1') {

                var Section = eval(document.getElementById("Section")).value;
                var Subsection = eval(document.getElementById("Subsection")).value;

                debugger;
                var ctnSection = 0;
                if (Section != '') {
                    ctnSection++
                }
                if (Subsection != '') {
                    ctnSection++
                }
                if (ctnSection>1) {
                    alert("大分段和小分段只能填写一个")
                    return;
                }
            }

            //二层内容
            var lstCtlDetail = document.getElementById(idDetail).getElementsByTagName("input");
            //判断是否有值个数
            var ctn = 0
            for (var a = 0; a < lstCtlDetail.length; a++) {
                if (lstCtlDetail[a].type == "text") {


                    if (lstCtlDetail[a].value != '' && lstCtlDetail[a].value != undefined) {
                        ctn++;
                    }
                }
            }
            if (ctn == 0) {
                alert("请输入查询内容");
                return;
            }
        }

    }
     
    if (Qty < 1) {

        alert("请选择查册选项");
        return;
    }
    //执行添加



    for (var i = 0; i < lstCtl.length; i++) {

        if (lstCtl[i].type == "checkbox" && lstCtl[i].checked == true) {
           
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
            var cell9 = row.insertCell(-1);
            var cell10 = row.insertCell(-1);
            var cell11 = row.insertCell(-1);
            var cell12 = row.insertCell(-1);
            var cell13 = row.insertCell(-1);
            var cell14 = row.insertCell(-1);
            var cell15 = row.insertCell(-1);

            var rowIndex = 0

            if (tab.rows.length != 2) {

                var _row = tab.rows; //获取table的行
                var _cell = _row[tab.rows.length - 2].cells;  //最末一行
                rowIndex = _cell[0].innerHTML;             //第一个为该行编号

            };


            var idDetail = lstCtl[i].value


            var lstCtlDetail = document.getElementById(idDetail).getElementsByTagName("input");
            if (idDetail == 'type1') {

                // NO. 	座号 	层数 	单位 	大厦名称 	街号 	街名 	地区 	地段类别DD 	地段编号 	大分段／小分段 	查询类型 	备注 	操作
                cell1.innerHTML = parseFloat(rowIndex) + 1;
                cell2.innerHTML = eval(document.getElementById("SeatNO")).value;
                cell3.innerHTML = eval(document.getElementById("Floor")).value;
                cell4.innerHTML = eval(document.getElementById("RoomNO")).value;
                cell5.innerHTML = eval(document.getElementById("BuildName")).value;
                cell6.innerHTML = eval(document.getElementById("StreetNumber")).value;
                cell7.innerHTML = eval(document.getElementById("Street")).value;
                cell8.innerHTML = eval(document.getElementById("Area")).value;
                cell9.innerHTML = eval(document.getElementById("LotType")).value;
                cell10.innerHTML = eval(document.getElementById("LotNo")).value;


                var Section = eval(document.getElementById("Section")).value;
                var Subsection = eval(document.getElementById("Subsection")).value;

                debugger;
                cell11.innerHTML = eval(document.getElementById("Section")).value + eval(document.getElementById("Subsection")).value;
                //2：大节点 1：小节点
                if (Section!='') {
                    cell11.value = '2';
                }
                else
                {
                    cell11.value = '1';
                }              
                cell12.innerHTML = '土地查册';
                cell12.value = '1';
                //记录类别,扣套餐时要判断(1:全部记录 2:现实记录)当SeachType为1时此栏位有效
                if (document.getElementById('inlineRadio1').checked) {
                    cell13.value = '1';
                }
                else
                {
                    cell13.value = '2';
                }
                cell13.innerHTML = '';
                cell14.innerHTML = '<button class="btn btn-danger btn-sm" onclick="RemovePer(this.parentNode.parentNode)" type="submit">' + '删除' + '</button>';

            }
            else {


                cell1.innerHTML = parseFloat(rowIndex) + 1;

                var strType = '';
                var value = '';
                if (idDetail=='type2') {
                    strType = '注册摘要'
                    value = 2;
                }
                if (idDetail == 'type3') {
                    strType = '注册摘要表格'
                    value = 3;
                }
                if (idDetail == 'type4') {
                    strType = '其他'
                    value = 4;
                }
                debugger;
                //12 类型 ：类型编号 13 备注：记录类别 14 按钮
                cell12.innerHTML = strType;
                cell12.value = value;
                cell13.innerHTML = lstCtlDetail[1].value;
                cell13.value = '';
                cell14.innerHTML = '<button class="btn btn-danger btn-sm" onclick="RemovePer(this.parentNode.parentNode)" type="submit">' + '删除' + '</button>';
            }
        }
      
    }
    Clear(self);
};

    //清空
function Clear() {

  
    var sss = document.getElementById("dataRcd").getElementsByTagName("input");
    for (var i = 0; i < sss.length; i++) {

        debugger;
        if (sss[i].type == "checkbox") {

            sss[i].checked = false;

        }
        if (sss[i].type == "text") {
            debugger;

            sss[i].value = "";

        }
    }

};

//移除某一行记录
function RemovePer(row) {


    var tb = document.getElementById("tblData");

    var index = row.rowIndex; //有这个属性，嘿嘿
    tb.deleteRow(index)//这里的数字是行的索引

    //document.getElementById('tblData').deleteRow(obj.parentElement.parentElement.rowIndex);


};

//提交
function Save() {


    var SelectName = eval(document.getElementById("SelectName")).value;

    var obj = document.getElementById('SelectSalutation');
    var SelectSalutation = obj.options[obj.selectedIndex].text;//获取文本

    var SelectTel = eval(document.getElementById("SelectTel")).value;
    var SelectEmail = eval(document.getElementById("SelectEmail")).value;


    var tab = document.getElementById("tblData");

    //判断是否有记录
    if (tab.rows.length < 2) {

        alert('请先添加记录');
        return;
    }
    //表单
    var rpt_columns = new Array();
    var column = new Object();

    for (var i = 1; i < tab.rows.length; i++) {



        column = new Object();
        column.SeatNO = tab.rows[i].cells[1].innerHTML;
        column.Floor = tab.rows[i].cells[2].innerHTML;
        column.RoomNO = tab.rows[i].cells[3].innerHTML;
        column.BuildName = tab.rows[i].cells[4].innerHTML;
        column.StreetNumber = tab.rows[i].cells[5].innerHTML;
        column.Street = tab.rows[i].cells[6].innerHTML;
        column.Area = tab.rows[i].cells[7].innerHTML;
        column.LotType = tab.rows[i].cells[8].innerHTML;
        column.LotNo = tab.rows[i].cells[9].innerHTML;
        //2：大节点 1：小节点
        if (tab.rows[i].cells[10].value='2') {
            column.Section = tab.rows[i].cells[10].innerHTML
            column.SubSection=''
        }
        else
        {
            column.SubSection = tab.rows[i].cells[10].innerHTML
            column.Section = ''
        }

        debugger;
        column.RecordType = tab.rows[i].cells[12].value;
        column.SeachType = tab.rows[i].cells[11].value;
        column.Remark = tab.rows[i].cells[12].innerHTML;

        rpt_columns.push(column);
    }

    $.ajax({
        url: "/LandOffline/Save",
        type: "post",
        ///                                           ComOrPer 0:公司 1:个人
        data: { SelectName: SelectName, SelectSalutation: SelectSalutation, SelectTel: SelectTel, SelectEmail: SelectEmail, MainJson: JSON.stringify(rpt_columns) },
        cache: false,
        async: false,
        dataType: "json",
        success: function (packJson) {


            if (packJson.status == 1) {

                alert(packJson.message);
                Clear(self);
            }
            else {
                alert(packJson.message);
            }
        }
    });
}
