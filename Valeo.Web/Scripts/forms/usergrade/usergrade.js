function closeDialog() {
    $('#editdlg').dialog('close');
}

//点击添加用户级别
function showAdd(title) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/UserGrade/Add',
        modal: true,
        onLoad: function () {
            $("input", $("#UserGradeID").next("span")).bind('blur', function () {
                $.ajax({
                    type: 'POST',
                    url: '/UserGrade/Verification',
                    data: 'gradeId=' + $('#UserGradeID').val(),
                    success: function (resp) {
                        if (resp.result) {
                            $.messager.alert(' 提示信息',resp.Msg,'error');
                            $("input", $("#UserGradeID").next("span")).focus();
                        }
                    }
                });
            });
        }
    });
}

//点击保存触发
function addGrade(isEdit) {
    
    //if ($('#UserGrade').val().replace(/(^s*)|(s*$)/g, "").length == 0 || $('#UserGrade').val().replace(/(^s*)|(s*$)/g, "").length>8) {
    //    $.messager.alert(' 提示信息', '级别长度不能超过1-8！', 'error', function () {
    //        $("input", $("#UserGrade").next("span")).focus();
    //    });
    //    return false;
    //}

    try {

        var isValid = $('#gradeAddForms').form('validate');
        console.log(isValid);
        if (!isValid) {
            return;
        }
    } catch (e) {
        console.log("Error: Save.")
        console.log(e);
    }

    SaveEditGrade(isEdit);
}


//保存用户级别
function SaveEditGrade(isEdit) {
 
    //获取当前表格选中行
    var selectRows = [];

    if (isEdit) {
        //关闭行的编辑
        var rows = $("#dataGradeList").datagrid("getRows");
        for (var i = 0; i < rows.length; i++) {
            $('#dataGradeList').datagrid('endEdit', i);
        }

        var tmpData = $("#dataGradeList").datagrid("getRows");
        for (var i = 0; i < tmpData.length; i++) {
            if (tmpData[i].Status==1) {
                selectRows.push(tmpData[i]);
            }
        }
    } else {
        selectRows = $("#dataGradeList").datagrid('getChecked');
    }


    var data=[];
    for(var i=0;i<selectRows.length;i++){ 
        data.push(selectRows[i].DataGradeID);
    }
    
    var m_Status = $('#FedexArea').combobox('getValue');
    var UserGradeID = $("#UserGradeID").val();
    var UserGrade = $("#UserGrade").val();
    var Remark = $("#Remark").val();

    var urls = null;
    if (isEdit) {
        urls = '/UserGrade/EditSave';
    } else {
        urls = '/UserGrade/AddSave';
    }

    $.ajax({
        type: 'POST',
        url: urls,
        data: {
            UserGradeID: UserGradeID,
            UserGrade: UserGrade,
            Status: m_Status,
            Remark:Remark,
            dataGradeIDs: data
        },
        cache: false,
        success: function (data) {
            if (data.result) {

                $.messager.alert('提示信息', data.Msg, 'info', function () {
                    closeDialog();
                    loadData();
                });
            } else {
                $.messager.alert('提示信息', data.Msg, 'error');
            }
        }
    });
}

//结束表格编辑
var editIndex = undefined;
function endEditing() {
    if (editIndex == undefined) { return true }
    if ($('#dataGradeList').datagrid('validateRow', editIndex)) {
        $('#dataGradeList').datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else {
        return false;
    }
} 