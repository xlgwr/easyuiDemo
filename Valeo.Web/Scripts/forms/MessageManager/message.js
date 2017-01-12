//关闭新增消息框
function closeDialog() {
    $('#editdlg').dialog('close');
}

//跳转到新增消息
function showAdd(title) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        iconCls: "icon-add",
        href: '/MessageManager/Add',
        modal: true,
        onLoad: function () {
           
        }
    });
}

function submitForm(formId) {

    var submitform = $('#' + formId);
    $.ajax({
        type: 'POST',
        url: submitform.attr('action'),
        data: submitform.serialize(),
        success: function (data) {
      
            if (data.result) {
                submitform[0].reset();
                closeDialog();
                loadData();
            } else {
                $.messager.alert('提示信息', data.Msg);
            }
        }
    });
}
//选择会员
function GetUserList() {
    $('#searchdlg').dialog({
        title: "添加会员",
        closed: false,
        cache: false,
        href: '/MessageManager/GetMember?functionName=GetMember',
        modal: true
    });
}

function GetMember(memberRow) {

    var strName = '';
    var strId = '';
    for (var i = 0; i < memberRow; i++) {

    }
    $.each(memberRow, function (n, obj) {

        strName += obj.MemberName + ';';
        strId += obj.MemberID + ';';
    });
    //$("#addMemberName").textbox('setValue', memberRow.MemberName);
    //$("#addMemberId").val(memberRow.MemberID);
    $("#addMemberName").val(strName);
    $("#addMemberId").val(strId);
}

function showEdit(messageId, title, isEdit) {
     
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        iconCls: isEdit ? "icon-edit" : "icon_webedite",
        href: '/MessageManager/Edit?messageID=' + messageId + '&isEdit=' + isEdit,
        modal: true,
        onLoad: function () {
        }
    });
}