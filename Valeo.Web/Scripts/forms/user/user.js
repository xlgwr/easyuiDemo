function closeDialog() {
    $('#editdlg').dialog('close');
}
function showAdd(title) { 
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/User/Add',
        iconCls: "icon-add",
        modal: true,
        onLoad: function () { 
            $("input", $("#UserId").next("span")).bind('blur', function () {
                $.ajax({
                    type: 'POST',
                    url: '/User/Verification',
                    data: 'UserId=' + $('#UserId').val(),
                    success: function (resp) {
                        if (resp.result) {
                            $.messager.alert('提示信息 ', resp.Msg, 'info', function () {
                                $("input", $("#UserId").next("span")).focus();
                            }); 
                        }
                    }
                });
            });
        }
    });
 
   // $('#editdlg').dialog('refresh', '/User/Add');
   // $('#editdlg').dialog('open')
}
function showEdit(userId, title) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/User/Edit?userId=' + userId,
        iconCls: "icon-edit",
        modal: true
    });
    //$('#editdlg').dialog('refresh', '/User/Edit?userId=' + userId);
    //$('#editdlg').dialog('open')
}
function submitForm(formId)
{
    var submitform = $('#' + formId);
    $.ajax({
        type: 'POST',
        url: submitform.attr('action'),
        data: submitform.serialize(),
        success: function (data) {
            if (data.result) {
                $.messager.alert('提示信息 ', data.Msg, 'info', function () {
                    submitform[0].reset();
                    closeDialog();
                    loadData();
                }); 
            } else {
                $.messager.alert('提示信息 ', data.Msg);
            }
        }
    });
}