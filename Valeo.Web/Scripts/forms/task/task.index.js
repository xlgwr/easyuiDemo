function closeDialog() {
    $('#editdlg').dialog('close');
}


function showAddIndex(title) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        collapsible: false,
        minimizable: false,
        maximized: true,
        width: 1190,
        height: 555,
        href: '/TaskList/Add',
        modal: true,
        onLoad: function () {

        }
    });
}



//导出
function exportTask() {

    resultData = $("#taskList").datagrid('getRows');
    if (resultData.length == 0) {
        $.messager.alert('提示信息', "没有要导出的记录！", 'errro');

    } else {
        var form = $("#SearchTaskList");//定义一个form表单
        form.submit();//表单提交
    }
}

//修改数据
function submitForm(formId) {
    var submitform = $('#' + formId);
    console.log(submitform.serialize());

    $.ajax({
        type: 'POST',
        url: submitform.attr('action'),
        data: submitform.serialize(),
        success: function (data) {
            if (data.result) {
                $.messager.alert('提示信息', data.Msg, 'info');
                submitform[0].reset();
                closeDialog();
                loadData();
            } else {
                $.messager.alert('提示信息', data.Msg, 'errro');
            }
        }
    });
}


//////////////////////////////新增界面
function btnSearchResetAdd() {
    $('#SearchTaskListAdd').form('clear');
    $("#CbxBigTypes").combobox("setValue", 1);
    $("#CbxSmallTypes").combobox("setValue", 0);
}
//根据订单查询任务
function BtnSearchOrder() {

    var member = $("#MemberNames").val();
    if (member != "") {
        var pars = {
            //MemberID: $("#MemberIdSearch2").val(),
            MemberName: $("#MemberNames").val(),
            OrderID: $("#OrderIDs").val(),
            BigType: $("#CbxBigTypes").combo("getValue"),
            SmallType: $("#CbxSmallTypes").combo("getValue"),
        };
        url = '/TaskList/GetOrderData',
        loadOrderData(pars, url);
    } else {
        $.messager.alert('提示信息', '请选择账户名', 'info');
    }
}

//选择订单后加载数据
$('#orderList').datagrid({
    onClickRow: function (rowIndex, rowData) {
        var pars = rowData;

        var url1 = '/TaskList/GetCourtData';
        loadCourtData(pars, url1);

        var url2 = '/TaskList/GetCompanyData';
        loadCompanyData(pars, url2);

        var url3 = '/TaskList/GetLandData';
        loadLandData(pars, url3);

        var url4 = '/TaskList/GetOtherData';
        loadOtherData(pars, url4);

        var url5 = '/TaskList/GetPersonData';
        loadPersonData(pars, url5);

    }
});

var editRowcourt = undefined; //定义全局变量：当前编辑的行
var editRowcompany = undefined;
var editRowland = undefined;
var editRowother = undefined;
var editRowperson = undefined;

function editEndingCourt(editRowIndex) {
    //添加时先判断是否有开启编辑的行，如果有则把开户编辑的那行结束编辑
    try {
        if (editRowIndex == undefined) {
            return true;
        }
        if ($('#courtList').datagrid('validateRow', editRowIndex)) {

            $('#courtList').datagrid('endEdit', editRowIndex);

            var D_PerName_En = $('#courtList').datagrid('getRows')[editRowIndex]['D_PerName_En'];
            var D_PerName_Cn = $('#courtList').datagrid('getRows')[editRowIndex]['D_PerName_Cn'];
            var P_PerName_En = $('#courtList').datagrid('getRows')[editRowIndex]['P_PerName_En'];
            var P_PerName_Cn = $('#courtList').datagrid('getRows')[editRowIndex]['P_PerName_Cn'];
            var Representation = $('#courtList').datagrid('getRows')[editRowIndex]['Representation'];
            var Case_No = $('#courtList').datagrid('getRows')[editRowIndex]['Case_No'];
            var Street = $('#courtList').datagrid('getRows')[editRowIndex]['Street'];
            var BuildName = $('#courtList').datagrid('getRows')[editRowIndex]['BuildName'];
            var LotType = $('#courtList').datagrid('getRows')[editRowIndex]['LotType'];
            var LotNo = $('#courtList').datagrid('getRows')[editRowIndex]['LotNo'];
            var SelectName = $('#courtList').datagrid('getRows')[editRowIndex]['SelectName'];

            if (SelectName.length == 0 ||
                (D_PerName_En.length == 0 && D_PerName_Cn.length == 0 && P_PerName_En.length == 0 && P_PerName_Cn.length == 0 &&
                Representation.length == 0 && Case_No.length == 0 && Street.length == 0 && BuildName.length == 0 &&
                LotType.length == 0 && LotNo.length == 0)) {
                $('#courtList').datagrid('beginEdit', editRowIndex);
                return false;
            }

            editRowcourt = undefined;
            return true;
        } else {
            return false;
        }
    } catch (e) {

        editRowcourt = undefined;
        console.log(e);
        return true;
    }

}

function editEndingCompany(editRowIndex) {
    //添加时先判断是否有开启编辑的行，如果有则把开户编辑的那行结束编辑
    try {
        if (editRowIndex == undefined) {
            return true;
        }

        if ($('#companyList').datagrid('validateRow', editRowIndex)) {

            $('#companyList').datagrid('endEdit', editRowIndex);

            var SelectName = $('#companyList').datagrid('getRows')[editRowIndex]['SelectName'];

            var SelectName = $('#companyList').datagrid('getRows')[editRowIndex]['SelectName'];
            var Name_En = $('#companyList').datagrid('getRows')[editRowIndex]['Name_En'];
            var Name_Cn = $('#companyList').datagrid('getRows')[editRowIndex]['Name_Cn'];
            var RegNo1 = $('#companyList').datagrid('getRows')[editRowIndex]['RegNo1'];
            var RegNo2 = $('#companyList').datagrid('getRows')[editRowIndex]['RegNo2'];
            var ComAddress = $('#companyList').datagrid('getRows')[editRowIndex]['ComAddress'];

            if (SelectName.length == 0 ||
                (Name_En.length == 0 && Name_Cn.length == 0 && RegNo1.length == 0 && RegNo2.length == 0 &&
                ComAddress.length == 0)) {
                $('#companyList').datagrid('beginEdit', editRowIndex);
                return false;
            }
            editRowcompany = undefined;
            return true;
        } else {
            return false;
        }
    } catch (e) {
        editRowcompany = undefined;
        console.log(e);
        return true;
    }

}

function editEndingCompanyPer(editRowIndex) {
    //添加时先判断是否有开启编辑的行，如果有则把开户编辑的那行结束编辑
    try {
        if (editRowIndex == undefined) {
            return true;
        }

        if ($('#personList').datagrid('validateRow', editRowIndex)) {

            $('#personList').datagrid('endEdit', editRowIndex);

            var SelectName = $('#personList').datagrid('getRows')[editRowIndex]['SelectName'];

            var Name_En = $('#personList').datagrid('getRows')[editRowIndex]['Name_En'];
            var Name_Cn = $('#personList').datagrid('getRows')[editRowIndex]['Name_Cn'];
            var RegNo1 = $('#personList').datagrid('getRows')[editRowIndex]['RegNo1'];
            var RegNo2 = $('#personList').datagrid('getRows')[editRowIndex]['RegNo2'];

            if (SelectName.length == 0 ||
                (Name_En.length == 0 && Name_Cn.length == 0 && RegNo1.length == 0 && RegNo2.length == 0)) {
                $('#personList').datagrid('beginEdit', editRowIndex);
                return false;
            }
            editRowperson = undefined;
            return true;
        } else {
            return false;
        }
    } catch (e) {
        console.log(e);
        editRowperson = undefined;
        return true;
    }

}

function editEndingLand(editRowIndex) {
    debugger;
    //添加时先判断是否有开启编辑的行，如果有则把开户编辑的那行结束编辑
    try {
        if (editRowIndex == undefined) {
            return true;
        }

        if ($('#landList').datagrid('validateRow', editRowIndex)) {

            $('#landList').datagrid('endEdit', editRowIndex);

            var SelectName = $('#landList').datagrid('getRows')[editRowIndex]['SelectName'];
            var SearchType = $('#landList').datagrid('getRows')[editRowIndex]['SearchType'];

            var BuildName = $('#landList').datagrid('getRows')[editRowIndex]['BuildName'];
            //var HouseNO = $('#landList').datagrid('getRows')[editRowIndex]['HouseNO'];
            var RoomNO = $('#landList').datagrid('getRows')[editRowIndex]['RoomNO'];
            var SeatNO = $('#landList').datagrid('getRows')[editRowIndex]['SeatNO'];
            var Floor = $('#landList').datagrid('getRows')[editRowIndex]['Floor'];
            var Street = $('#landList').datagrid('getRows')[editRowIndex]['Street'];
            var StreetNumber = $('#landList').datagrid('getRows')[editRowIndex]['StreetNumber'];
            var Area = $('#landList').datagrid('getRows')[editRowIndex]['Area'];
            var LotType = $('#landList').datagrid('getRows')[editRowIndex]['LotType'];
            var LotNo = $('#landList').datagrid('getRows')[editRowIndex]['LotNo'];
            var Section = $('#landList').datagrid('getRows')[editRowIndex]['Section'];
            var Subsection = $('#landList').datagrid('getRows')[editRowIndex]['Subsection'];

            if (SearchType == 1) {
                if (SelectName.length == 0 ||
                               (BuildName.length == 0 && RoomNO.length == 0 && SeatNO.length == 0 &&
                               Floor.length == 0 && Street.length == 0 && StreetNumber.length == 0 && Area.length == 0 &&
                               LotType.length == 0 && LotNo.length == 0 && Section.length == 0 && Subsection.length == 0)) {
                    $('#landList').datagrid('beginEdit', editRowIndex);
                    return false;
                }
            }

            editRowland = undefined;
            return true;
        } else {
            return false;
        }
    } catch (e) {
        editRowland = undefined;
        return true;
        console.log(e);
    }

}

function editEndingOther(editRowIndex) {
    //添加时先判断是否有开启编辑的行，如果有则把开户编辑的那行结束编辑
    try {

        if (editRowIndex == undefined) {
            return true;
        }

        if ($('#otherList').datagrid('validateRow', editRowIndex)) {

            $('#otherList').datagrid('endEdit', editRowIndex);
            var SelectName = $('#otherList').datagrid('getRows')[editRowIndex]['SelectName'];
            var SelectContent = $('#otherList').datagrid('getRows')[editRowIndex]['SelectContent'];

            if (SelectName.length == 0 || SelectContent.length == 0) {
                $('#otherList').datagrid('beginEdit', editRowIndex);
                return false;
            }
            editRowother = undefined;
            return true;
        } else {
            return false;
        }
    } catch (e) {
        editRowother = undefined;
        return true;
        console.log(e);
    }
}

var AgeLimit = [{ "value": "0", "text": "7年" }, { "value": "1", "text": "全部" }];
var CourtType = [{ "value": "0", "text": "所有法庭" }, { "value": "1", "text": "高等法庭及区域法院及小额钱债" }, { "value": "3", "text": "破产案/个人债务重组" }];
var RecordType = [{ "value": "1", "text": "全部记录" }, { "value": "2", "text": "现实记录" }];
var SearchType = [{ "value": "1", "text": "土地查册" }, { "value": "2", "text": "注册摘要" }, { "value": "3", "text": "注册摘要表格" }, { "value": "4", "text": "其他" }];
var Salutation = [{ "value": "Mr", "text": "Mr" }, { "value": "Mrs", "text": "Mrs" }, { "value": "Ms", "text": "Ms" }];
var ReportType1 = [{ "value": "0", "text": "最近公司年报" }, { "value": "1", "text": "组织章程大纲及章程细则" }, { "value": "2", "text": "公司注册证书" },
                   { "value": "3", "text": "公司年报" }, { "value": "4", "text": "抵押" }, { "value": "5", "text": "有效地商业/分行登记证核证副本" }, { "value": "6", "text": "其他" },
                   { "value": "7", "text": "商业登记册内资料摘录的核证本" }, { "value": "8", "text": "商业登记册内资料摘录的电子摘录" }, { "value": "9", "text": "组织规章大纲及章程细则" },
                   { "value": "10", "text": "有效地商业/分行登记证核证副本" }, { "value": "11", "text": "公司强制性清盘案记录查册" }];
var ReportType2 = [{ "value": "12", "text": "破产案查册" }, { "value": "13", "text": "有限公司董事查册" }, { "value": "14", "text": "其他" }];

//复制并新增新行
function btnCopyAdd(index, type) {
    debugger;

    var _selectMember = {
        SelectName: null,
        SelectSalutation: null,
        SelectTel: null,
        SelectEmail: null,
    }
    try {
        if (_selectRowMember) {
            if (_selectRowMember.SalutationVM) {
                _selectMember.SelectSalutation = _selectRowMember.SalutationVM;
            }
            if (_selectRowMember.MobilePhone) {
                _selectMember.SelectTel = _selectRowMember.MobilePhone;
            }
            if (_selectRowMember.Email) {
                _selectMember.SelectEmail = _selectRowMember.Email;
            }
            if (_selectRowMember.MemberName) {
                _selectMember.SelectName = _selectRowMember.MemberName;
            }
        }
    } catch (e) {
        console.log(e);
    }

    //法庭
    if (type == 1) {
        var rows = $('#courtList').datagrid('getRows');
        var rowIndex = rows.length;

        //添加时如果没有正在编辑的行，则在datagrid的第一行插入一行
        if (editEndingCourt(editRowcourt)) {
            if (index == 0) {
                $('#courtList').datagrid('appendRow', {
                    OrderListID: null,
                    D_PerName_En: null,
                    D_PerName_Cn: null,
                    P_PerName_En: null,
                    P_PerName_Cn: null,
                    Representation: null,
                    Street: null,
                    Representation: null,
                    BuildName: null,
                    LotType: null,
                    LotNo: null,
                    Case_No: null,
                    AgeLimit: 0,
                    CourtType: 0,
                    Remark: null,
                    SelectName: _selectMember.SelectName,
                    SelectSalutation: _selectMember.SelectSalutation,
                    SelectTel: _selectMember.SelectTel,
                    SelectEmail: _selectMember.SelectEmail
                });
            } else {
                $('#courtList').datagrid('appendRow', {
                    OrderListID: rows[index].OrderListID,
                    D_PerName_En: rows[index].D_PerName_En,
                    D_PerName_Cn: rows[index].D_PerName_Cn,
                    P_PerName_En: rows[index].P_PerName_En,
                    P_PerName_Cn: rows[index].P_PerName_Cn,
                    Representation: rows[index].Representation,
                    Street: rows[index].Street,
                    Representation: rows[index].Representation,
                    BuildName: rows[index].BuildName,
                    LotType: rows[index].LotType,
                    LotNo: rows[index].LotNo,
                    Case_No: rows[index].Case_No,
                    AgeLimit: rows[index].AgeLimit,
                    CourtType: rows[index].CourtType,
                    Remark: rows[index].Remark,
                    SelectName: rows[index].SelectName,
                    SelectSalutation: rows[index].SelectSalutation,
                    SelectTel: rows[index].SelectTel,
                    SelectEmail: rows[index].SelectEmail
                });
            }
            //将新插入的那一行开户编辑状态
            $('#courtList').datagrid("beginEdit", rowIndex);

            editRowcourt = rowIndex;
        }
        $('#courtList').datagrid("unselectAll");

    }
        //公司
    else if (type == 2) {
        var rows = $('#companyList').datagrid('getRows');
        var rowIndex = rows.length;

        //添加时如果没有正在编辑的行，则在datagrid的第一行插入一行
        if (editEndingCompany(editRowcompany)) {
            if (index == 0) {
                $('#companyList').datagrid('appendRow', {
                    OrderListID: null,
                    Name_En: null,
                    Name_Cn: null,
                    RegNo1: null,
                    RegNo2: null,
                    ComAddress: null,
                    ReportType: 0,
                    Remark: null,
                    SelectName: _selectMember.SelectName,
                    SelectSalutation: _selectMember.SelectSalutation,
                    SelectTel: _selectMember.SelectTel,
                    SelectEmail: _selectMember.SelectEmail
                });
            } else {
                $('#companyList').datagrid('appendRow', {
                    OrderListID: rows[index].OrderListID,
                    Name_En: rows[index].Name_En,
                    Name_Cn: rows[index].Name_Cn,
                    RegNo1: rows[index].RegNo1,
                    RegNo2: rows[index].RegNo2,
                    ComAddress: rows[index].ComAddress,
                    ReportType: rows[index].ReportType,
                    Remark: rows[index].Remark,
                    SelectName: rows[index].SelectName,
                    SelectSalutation: rows[index].SelectSalutation,
                    SelectTel: rows[index].SelectTel,
                    SelectEmail: rows[index].SelectEmail
                });
            }
            //将新插入的那一行开户编辑状态
            $('#companyList').datagrid("beginEdit", rowIndex);

            editRowcompany = rowIndex;
        }
        $('#companyList').datagrid("unselectAll");

    }
        //土地
    else if (type == 3) {

        var rows = $('#landList').datagrid('getRows');
        var rowIndex = rows.length;

        //添加时如果没有正在编辑的行，则在datagrid的第一行插入一行
        if (editEndingLand(editRowland)) {
            if (index == 0) {
                $('#landList').datagrid('appendRow', {
                    OrderListID: null,
                    RecordType: 1,
                    BuildName: null,
                    HouseNO: null,
                    RoomNO: null,
                    SeatNO: null,
                    Floor: null,
                    Street: null,
                    StreetNumber: null,
                    Area: null,
                    LotType: null,
                    LotNo: null,
                    Section: null,
                    Subsection: null,
                    SearchType: 1,
                    Remark: null,
                    SelectName: _selectMember.SelectName,
                    SelectSalutation: _selectMember.SelectSalutation,
                    SelectTel: _selectMember.SelectTel,
                    SelectEmail: _selectMember.SelectEmail
                });
            } else {
                $('#landList').datagrid('appendRow', {
                    OrderListID: rows[index].OrderListID,
                    RecordType: rows[index].RecordType,
                    BuildName: rows[index].BuildName,
                    HouseNO: rows[index].HouseNO,
                    RoomNO: rows[index].RoomNO,
                    SeatNO: rows[index].SeatNO,
                    Floor: rows[index].Floor,
                    Street: rows[index].Street,
                    StreetNumber: rows[index].StreetNumber,
                    Area: rows[index].Area,
                    LotType: rows[index].LotType,
                    LotNo: rows[index].LotNo,
                    Section: rows[index].Section,
                    Subsection: rows[index].Subsection,
                    SearchType: rows[index].SearchType,
                    Remark: rows[index].Remark,
                    SelectName: rows[index].SelectName,
                    SelectSalutation: rows[index].SelectSalutation,
                    SelectTel: rows[index].SelectTel,
                    SelectEmail: rows[index].SelectEmail
                });
            }
            //将新插入的那一行开户编辑状态
            $('#landList').datagrid("beginEdit", rowIndex);

            editRowland = rowIndex;
        }
        $('#landList').datagrid("unselectAll");
    }
        //其他
    else if (type == 4) {
        var rows = $('#otherList').datagrid('getRows');
        var rowIndex = rows.length;

        //添加时如果没有正在编辑的行，则在datagrid的第一行插入一行
        if (editEndingOther(editRowother)) {
            if (index == 0) {
                $('#otherList').datagrid('appendRow', {
                    OrderListID: null,
                    SelectContent: null,
                    Remark: null,
                    SelectName: _selectMember.SelectName,
                    SelectSalutation: _selectMember.SelectSalutation,
                    SelectTel: _selectMember.SelectTel,
                    SelectEmail: _selectMember.SelectEmail
                });
            } else {
                $('#otherList').datagrid('appendRow', {
                    OrderListID: rows[index].OrderListID,
                    SelectContent: rows[index].SelectContent,
                    Remark: rows[index].Remark,
                    SelectName: rows[index].SelectName,
                    SelectSalutation: rows[index].SelectSalutation,
                    SelectTel: rows[index].SelectTel,
                    SelectEmail: rows[index].SelectEmail
                });
            }
            //将新插入的那一行开户编辑状态
            $('#otherList').datagrid("beginEdit", rowIndex);

            editRowother = rowIndex;
        }
        $('#otherList').datagrid("unselectAll");
    }
        //公司中的个人
    else if (type == 5) {
        var rows = $('#personList').datagrid('getRows');
        var rowIndex = rows.length;

        //添加时如果没有正在编辑的行，则在datagrid的第一行插入一行
        if (editEndingCompanyPer(editRowperson)) {
            if (index == 0) {
                $('#personList').datagrid('appendRow', {
                    OrderListID: null,
                    Name_En: null,
                    Name_Cn: null,
                    RegNo1: null,
                    RegNo2: null,
                    ReportType: ReportType2[0].text,
                    Remark: null,
                    SelectName: _selectMember.SelectName,
                    SelectSalutation: _selectMember.SelectSalutation,
                    SelectTel: _selectMember.SelectTel,
                    SelectEmail: _selectMember.SelectEmail
                });
            } else {
                $('#personList').datagrid('appendRow', {
                    OrderListID: rows[index].OrderListID,
                    Name_En: rows[index].Name_En,
                    Name_Cn: rows[index].Name_Cn,
                    RegNo1: rows[index].RegNo1,
                    ReportType: rows[index].ReportType,
                    RegNo2: rows[index].RegNo2,
                    Remark: rows[index].Remark,
                    SelectName: rows[index].SelectName,
                    SelectSalutation: rows[index].SelectSalutation,
                    SelectTel: rows[index].SelectTel,
                    SelectEmail: rows[index].SelectEmail
                });
            }
            //将新插入的那一行开户编辑状态
            $('#personList').datagrid("beginEdit", rowIndex);

            editRowperson = rowIndex;
        }
        $('#personList').datagrid("unselectAll");

    }

}


//删除选中行数据
function deleteOther(index, type) {

    if (type == 1) {

        $('#courtList').datagrid('endEdit', index);
        $('#courtList').datagrid("deleteRow", index);
        editRowcourt = undefined;

    } else if (type == 2) {

        $('#companyList').datagrid('endEdit', index);
        $('#companyList').datagrid("deleteRow", index);
        editRowcompany = undefined;

    } else if (type == 3) {

        $('#landList').datagrid('endEdit', index);
        $('#landList').datagrid("deleteRow", index);
        editRowland = undefined;

    } else if (type == 4) {

        $('#otherList').datagrid('endEdit', index);
        $('#otherList').datagrid("deleteRow", index);
        editRowother = undefined;

    } else if (type == 5) {

        $('#personList').datagrid('endEdit', index);
        $('#personList').datagrid("deleteRow", index);
        editRowperson = undefined;
    }
}
