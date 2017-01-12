function closeDialog() {
    $('#editdlg').dialog('close');
}
function closeDialogSearch() {
    $('#searchdlg').dialog('close');
}
function showAdd(title) { 
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Package/Add',
        modal: true,
        onLoad: function () { 
            $("input", $("#AddPackageId").next("span")).bind('blur', function () {
                if ($('#AddPackageId').val() != "") { 
                    $.ajax({
                        type: 'POST',
                        url: '/Package/Verification',
                        data: 'PackageId=' + $('#AddPackageId').val(),
                        success: function(resp) {
                            if (resp.result) {
                                $.messager.alert('提示信息 ', resp.Msg, 'info', function () {
                                    $("input", $("#AddPackageId").next("span")).focus();
                                });  
                                return;
                            }
                        }
                    });
                }
            });
            $("input", $("#AddDiscount").next("span")).bind('blur', function () {
                SetTotal();
            });
            $('#AddMemberGradeId').combobox({onSelect:function() { 
                dgProductAdd.bindDatagrid([]);
                SetTotal();
            }});

            $('#AddCurrency').combobox({
                onSelect: function () {
                    dgProductAdd.bindDatagrid([]);
                    SetTotal();
                }
            });
            if ($("#AddCurrency").combobox('getValue') == "RMB") {
                $(".unitCurrency").html("￥");
            } else {
                $(".unitCurrency").html("$");
            }
        }
    });
  
}
function showEdit(packageId, title, isEdit) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Package/Edit?PackageID=' + packageId + '&isEdit=' + isEdit,
        modal: true,
        iconCls:isEdit?"icon-edit":"icon_webedite",
        onLoad: function () { 
            $("input", $("#AddDiscount").next("span")).bind('blur', function () { 
                SetTotal();
            });
            $('#AddMemberGradeId').combobox({
                onSelect: function () {
                    dgProductAdd.bindDatagrid([]);
                    SetTotal();
                }
            });

            $('#AddCurrency').combobox({
                onSelect: function () {
                    dgProductAdd.bindDatagrid([]);
                    SetTotal();
                }
            });
            if ($("#AddCurrency").combobox('getValue') == "RMB") {
                $(".unitCurrency").html("￥");
            } else {
                $(".unitCurrency").html("$");
            }
        }
    }); 
} 