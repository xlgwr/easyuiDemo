
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    }
});

$(function () {
    $('#UserGradeID').combobox({
        onSelect: function (record) {
            var pars = {
                UserGradeID: record.value
            }
            loadData(pars);
        }
    });
});



function checkboxFormatter(value, rowData, rowIndex) {

    if (value == 1) {
        return '<span class="rright">√</span>';//'<input type="checkbox" checked="checked">';
    } else if (value == 0) {
        return '<span class="rerror">×</span>';//'<input type="checkbox" >';
    } else {
        return '<span class="rnothing">○</span>';
    }
}

//结束表格编辑
var editIndex = undefined;
function endEditing() {
    if (editIndex == undefined) { return true }
    if ($('#PowerList').datagrid('validateRow', editIndex)) {
        $('#PowerList').datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else {
        return false;
    }
}
//点击表格中checkbobox方法
function onClickCell(index, field) {

    var dd = $('#PowerList').datagrid('getRows');
    console.log(dd[index]);
    if (dd[index][field] == 2) {
        if (endEditing()) {
            editIndex = index;
        }
        return;
    }
    if (endEditing()) {
        $('#PowerList').datagrid('selectRow', index)
                .datagrid('editCell', { index: index, field: field });
        editIndex = index;
    }
}
//保存
function SavePower() {
    var rows = $("#PowerList").datagrid("getRows");
    for (var i = 0; i < rows.length; i++) {
        $('#PowerList').datagrid('endEdit', i);
    }

    var data = $("#PowerList").datagrid("getRows");
    var userGradeid = $('#UserGradeID').combobox('getValue');

    $.ajax({
        type: 'POST',
        url: '/UserPower/EditSave',
        data: {
            UserGradeID: userGradeid,
            model: data
        },
        cache: false,
        success: function (data) {
            if (data.result) {
                $.messager.alert(' 提示信息',data.Msg);
                //submitform[0].reset();
                //closeDialog();
                loadData();
            } else {

            }
        }
    });
}