function closeDialog() {
    $('#editdlg').dialog('close');
}
//关闭选择任务界面
function closeTaskDialog() {
    $('#editdlgTaskID').dialog('close');
}

//关闭选择法庭.公司.土地等数据界面
function closeDataDialog() {
    $('#editdlgData').dialog('close');
}

//新增报告
function showAdd(title, s_reportdata, isSelected) {
    debugger;
    var url;
    var m_method = 'post';
    var para = { reportList: s_reportdata, isSelected: isSelected }
    if (isSelected == 2) {
        url = '/ReportList/Add';
    } else {
        m_method = 'get';
        url = '/ReportList/Edit?BigType=' + s_reportdata.BigType + '&SmallType=' + s_reportdata.SmallType + '&ReportID=' + s_reportdata.ReportID + '&isSelected=' + isSelected;
    }
    $('#editdlg').dialog({
        title: title,
        method: m_method,
        closed: false,
        cache: false,
        width: 1190,
        height: 555,
        minimizable: false, maximized: true,
        href: url,
        queryParams: para,
        modal: true,
        onLoad: function () {

        }
    });

    //$('#editdlg').dialog('refresh', url);
}

//title, BigType, SmallType, ReportID
function showEditsReport() {

    var currrow = $('#reportList').datagrid('getSelected');
    if (!currrow) {
        $.messager.alert('提示信息', "请选择要修改的记录!", 'info');
        return;
    }

    var urls = '/ReportList/Edit?BigType=' + currrow.BigType + '&SmallType=' + currrow.SmallType + '&ReportID=' + currrow.ReportID;
    console.log(urls);
    $('#editdlg').dialog({
        title: '修改',
        method: 'get',
        href: urls,
        queryParams: {},
        closed: false,
        cache: false,
        minimizable: false, maximized: true,
        width: 1190,
        height: 555,
        modal: true,
        onLoad: function () {

        }
    });
    //$('#editdlg').dialog('refresh', urls);
}


//选择案件信息
function showAddData(title, tmpSmallType) {

    var TaskID = $("#TaskIDA").val();
    if (TaskID.length == 0) {
        $.messager.alert('提示信息', "请先选择任务!", 'info');
        return;
    }

    var SmallType2 = $("#SmallTypeAdd").combobox("getValue");

    if (tmpSmallType != -1) {
        SmallType2 = tmpSmallType
    }

    $('#editdlgData').dialog({
        title: title,
        method: 'get',
        closed: false,
        cache: false,
        minimizable: false,
        maximized: false,
        width: 1150,
        height: 550,
        href: '/ReportList/AddDataList?SmallType=' + SmallType2 + '&TaskID=' + TaskID,
        modal: true,
        onLoad: function () {

        }
    });
}

//选择任务编号
function showAddTaskID(title) {

    var SmallType = $("#SmallTypeAdd").combobox("getValue");

    $('#editdlgTaskID').dialog({
        title: title,
        closed: false,
        cache: false,
        minimizable: false,
        maximized: false,
        width: 1150,
        height: 550,
        method: 'get',
        href: '/ReportList/AddTaskID?SmallType=' + SmallType,
        modal: true,
        onLoad: function () {

        }
    });
}


//选中公共数据
function showAddPublic(title, tableid) {

    $('#editdlgPublic').dialog({
        title: title,
        closed: false,
        cache: false,
        minimizable: false,
        maximized: false,
        width: 800,
        height: 600,
        method: 'get',
        //queryParams:columns,
        href: '/ReportList/AddPublicList?tableID=' + tableid,
        modal: true,
        onLoad: function () {

        }
    });
}

formatterDate = function (date) {
    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
    var month = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : "0"
    + (date.getMonth() + 1);
    return date.getFullYear() + '-' + month + '-' + day;
};

//点击查找任务编号
function SearchTaskID() {

    //搜索界面选中的数据
    var checkedrows = $('#taskList').datagrid('getChecked');

    if (checkedrows.length == 1) {
        $("#TaskIDA").textbox('setValue', checkedrows[0].TaskID);//赋值
        $("#MemberNameA").textbox('setValue', checkedrows[0].MemberName);//赋值
        $("#MemberIdSearch").val(checkedrows[0].MemberID);

        $('#editdlgTaskID').dialog('close');
    }

}


//获取搜索界面选中的法庭数据，并加载到表格中
function AddDataOk() {
    debugger;
    //搜索界面选中的数据
    var checkedrows = $('#CourtData').datagrid('getChecked');
    if (checkedrows.length == 0) {
        return;
    }

    //循环把选中数据添加到表格中
    for (var i = 0; i < checkedrows.length; i++) {

        $('#courtList').datagrid('appendRow', {
            Id: checkedrows[i].Id,
            CaseNo: checkedrows[i].CaseNo,
            Year: checkedrows[i].Year,
            NumberTimes: checkedrows[i].NumberTimes,
            CourtName: checkedrows[i].CourtName,
            CaseTypeName: checkedrows[i].CaseTypeName,
            SerialNo: checkedrows[i].SerialNo,
            CourtDay: checkedrows[i].CourtDay,
            Plaintiff: checkedrows[i].Plaintiff,
            P_Address: checkedrows[i].P_Address,
            Defendant: checkedrows[i].Defendant,
            D_Address: checkedrows[i].D_Address,
            Nature: checkedrows[i].Nature,
            Judge: checkedrows[i].Judge,
            Representation: checkedrows[i].Representation,
            Representation_P: checkedrows[i].Representation_P,
            Representation_D: checkedrows[i].Representation_D,
            Hearing: checkedrows[i].Hearing,
            Currency: checkedrows[i].Currency,
            Amount: checkedrows[i].Amount,
            DataGradeID: checkedrows[i].DataGradeID
        });
    }
    $('#courtList').datagrid("unselectAll");
    $('#editdlgData').dialog('close');
}


//获取搜索界面选中的公司数据，并加载到表格中
function AddCompanyOk() {

    //搜索界面选中的数据
    var checkedrows = $('#CompanyData').datagrid('getChecked');
    if (checkedrows.length == 0) {
        return;
    }
    //循环把选中数据添加到表格中
    for (var i = 0; i < checkedrows.length; i++) {

        $('#companyList').datagrid('appendRow', {
            Entityid: checkedrows[i].Entityid,
            CRNo: checkedrows[i].CRNo,
            FullName_En: checkedrows[i].FullName_En,
            FullName_Cn: checkedrows[i].FullName_Cn,
            CountryID: checkedrows[i].CountryID,
            CompanyType: checkedrows[i].CompanyType,
            IncorporationDate: checkedrows[i].IncorporationDate,
            AuthorizedCapital: checkedrows[i].AuthorizedCapital,
            Areas: checkedrows[i].Areas,
            PlaceRegistration: checkedrows[i].PlaceRegistration,
            Listed: checkedrows[i].Listed,
            IssuedShares: checkedrows[i].IssuedShares,
            ActiveStatus: checkedrows[i].ActiveStatus,
            WindingUpMode: checkedrows[i].WindingUpMode,
            DissolutionDate: checkedrows[i].DissolutionDate,
            RegisterOfCharges: checkedrows[i].RegisterOfCharges,
            ImportantNote: checkedrows[i].ImportantNote,
            Remark: checkedrows[i].Remark,
            DataGradeID: checkedrows[i].DataGradeID
        });
    }
    $('#companyList').datagrid("unselectAll");
    $('#editdlgData').dialog('close');

}

//获取搜索界面选中的土地数据，并加载到表格中
function AddLandOk() {

    //搜索界面选中的数据
    var checkedrows = $('#LandData').datagrid('getChecked');
    if (checkedrows.length == 0) {
        return;
    }

    //循环把选中数据添加到表格中
    for (var i = 0; i < checkedrows.length; i++) {

        $('#landList').datagrid('appendRow', {
            LandID: checkedrows[i].LandID,
            BuildName: checkedrows[i].BuildName,
            HouseNO: checkedrows[i].HouseNO,
            RoomNO: checkedrows[i].RoomNO,
            SeatNO: checkedrows[i].SeatNO,
            Floor: checkedrows[i].Floor,
            Street: checkedrows[i].Street,
            StreetNumber: checkedrows[i].StreetNumber,
            Area: checkedrows[i].Area,
            LotType: checkedrows[i].LotType,
            LotNo: checkedrows[i].LotNo,
            Section: checkedrows[i].Section,
            Subsection: checkedrows[i].Subsection,
            Rent: checkedrows[i].Rent,
            HeldUnder: checkedrows[i].HeldUnder,
            LeaseTerm: checkedrows[i].LeaseTerm,
            ShareLot: checkedrows[i].ShareLot,
            Address_En: checkedrows[i].Address_En,
            Address_Cn: checkedrows[i].Address_Cn,
            SearchTime: checkedrows[i].SearchTime,
            SearchType: checkedrows[i].SearchType,
            Remark: checkedrows[i].Remark,
            DataGradeID: checkedrows[i].DataGradeID
        });
    }
    $('#landList').datagrid("unselectAll");
    $('#editdlgData').dialog('close');
}
function closeDialogSearch() {
    $('#searchdlg').dialog('close');
}
//获取搜索界面选中的信贷数据，并加载到表格中
function AddLoanOk() {

    //搜索界面选中的数据
    var checkedrows = $('#LoanData').datagrid('getChecked');
    if (checkedrows.length == 0) {
        return;
    }
    //循环把选中数据添加到表格中
    for (var i = 0; i < checkedrows.length; i++) {

        $('#loanList').datagrid('appendRow', {
            LoanID: checkedrows[i].LoanID,
            LoanNo: checkedrows[i].LoanNo,
            LoanType: checkedrows[i].LoanType,
            Currencies: checkedrows[i].Currencies,
            ApplicationDate: checkedrows[i].ApplicationDate,
            ApplicatinAmount: checkedrows[i].ApplicatinAmount,
            LoanPrincipal: checkedrows[i].LoanPrincipal,
            ApprovalDate: checkedrows[i].ApprovalDate,
            PrincipalOS: checkedrows[i].PrincipalOS,
            TotalOS: checkedrows[i].TotalOS,
            LastPaidDate: checkedrows[i].LastPaidDate,
            OverdueDays: checkedrows[i].OverdueDays,
            CurrentStatus: checkedrows[i].CurrentStatus,
            Remark: checkedrows[i].Remark,
            Lender: checkedrows[i].Lender,
            Guarantor: checkedrows[i].Guarantor,
            IDNumber: checkedrows[i].IDNumber,
            Borrower_En: checkedrows[i].Borrower_En,
            DataGradeID: checkedrows[i].DataGradeID
        });
    }
    $('#landList').datagrid("unselectAll");
    $('#editdlgData').dialog('close');
}



