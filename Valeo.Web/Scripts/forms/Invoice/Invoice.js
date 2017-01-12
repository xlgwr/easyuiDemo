function closeDialog() {
    $('#editdlg').dialog('close');
}

function showAdd(title, orderIds) {
    var orderidspara = {}
    for (var i = 0; i < orderIds.length; i++) {
        orderidspara["orderIds[" + i + "]"] = orderIds[i];
    }
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Invoice/Add',
        queryParams: orderidspara,
        method: 'post',
        modal: true,
        onLoad: function () {
            orderInvoices = [];
            try {
                var $a1 = $("#AddMemberName").next("span").find('a')[0];
                console.log($a1);
                $($a1).css("cursor", "pointer");

                $($a1).click(function () {
                    var orderInvoices = dgOrderProductAdd.thisdg.datagrid("getRows");
                    if (orderInvoices.length > 0) {
                        $.messager.alert('提示信息 ', '删除完所有订单才能重新选择会员！');
                        return false;
                    }
                    $('#searchdlg').dialog({
                        title: "选择会员",
                        closed: false,
                        cache: false,
                        href: '/SearchDialog/MemberSearch?functionName=GetMember',
                        modal: true
                    });
                });
                $("input", $("#addEmail").next("span")).bind('click', function () {
                    var orderInvoices = dgOrderProductAdd.thisdg.datagrid("getRows");
                    if (orderInvoices.length > 0) {
                        $.messager.alert('提示信息 ', '删除完所有订单才能重新选择会员！');
                        return false;
                    }
                    $('#searchdlg').dialog({
                        title: "选择会员",
                        closed: false,
                        cache: false,
                        href: '/SearchDialog/MemberSearch?functionName=GetMember',
                        modal: true
                    });
                });
            } catch (e) {
                console.log(e);
            }

            $("input", $("#AddDiscount").next("span")).bind('blur', function () {
                SetTotal();
            });
            $("input", $("#AddSubtotal").next("span")).bind('blur', function () {
                SetTotal();
            });
            $("input", $("#AddTotal").next("span")).bind('blur', function () {
                SetTotal();
            });
            SetTotal();
            CurrencydisableAdd();
        }
    });
}
function CurrencydisableEdit() {

    var orderInvoices = dgOrderProductEdit.thisdg.datagrid("getRows");
    if (orderInvoices.length > 0) {
        $("#Currency").combo("disable");
        $("#IsDposit").attr("disabled", true);
    }
}
function CurrencydisableAdd() {

    var orderInvoices = dgOrderProductAdd.thisdg.datagrid("getRows");
    if (orderInvoices.length > 0) {
        $("#Currency").combo("disable");
        $("#IsDposit").attr("disabled", true);
    }
}
function GetMember(memberRow) {
    $("#addMemberID").val(memberRow.MemberID);
    $("#AddMemberName").textbox('setValue', memberRow.MemberName);
    $("#addEmail").textbox('setValue', memberRow.Email);
}

function showSendEmail(rowIndex, title) {

    var gridData = $("#InvoiceList").datagrid('getData');
    var mailData = gridData.rows[rowIndex];

    var EmailBody = "<p><br/><br/><br/>"
    EmailBody = "Dear " + mailData.MemberName+":";
    EmailBody += "</p>"
    EmailBody += "<p><br/><br/><strong>"
    EmailBody += 'We have sent you <span style="color:#ff0000">AN INVOICE</span>, please check your mail box.'
    EmailBody += "</strong><br/><br/><br/><br/><br/></p>"
    EmailBody += "<p>"
    EmailBody += "Thank you for choosing <strong>" + mailData.Website + "</strong>"
    EmailBody += "</p><br/>"
    EmailBody += "<p>"
    EmailBody += '<em><span style="color:#548dd4">Feel free to contact our Customer Service:</span></em>'
    EmailBody += "</p>"
    EmailBody += "<p>"
    EmailBody += '<em><span style="color:#548dd4">Hotline: ' + mailData.CompTel+'</span></em>'
    EmailBody += "</p>"
    EmailBody += "<p>"
    EmailBody +=' <em><span style="color:#548dd4">eMail: '+ mailData.CompEmail+'</span></em>' 
    EmailBody += "</p><br/><br/><br/>"

    var EmailAppModel = {
        functionName: "SetMail",
        retId: mailData.InvoiceID,
        ToMail: mailData.Email == null ? "" : mailData.Email,
        Subject: "InvoceID:" + mailData.InvoiceID,
        EmailBody: EmailBody
    };
    parent.sendEmailInvoces(EmailAppModel, mailData.MemberID)

    //不用了
    return;
    //var urlpara = "functionName=SetMail&retId=" + mailData.InvoiceID;
    //urlpara += "&ToMail=" + encodeURIComponent(mailData.Email == null ? "" : mailData.Email);
    //urlpara += "&Subject=" + encodeURIComponent(mailData.InvoiceDateString + "所开发票");

    var table = '<table style="margin-top:10px; border-collapse: collapse;border: 1px solid #aaa; width: 100%;">'
    var tableEnd = "</table>"
    var th0 = "<tr><th>编号</th><th>币种</th><th>总金额</th></tr>"
    var th0V = "<tr><td>" +
        mailData.InvoiceID + "</td><td>" +
        mailData.Currency + "</td><td>" +
        mailData.Total + "</td></tr>"

    var h1 = "<h2>发票信息</h2>"
    var h2 = "<h2>订单列表</h2>"
    var th1 = "<tr><th>订单单号</th><th>产品名称</th><th>次数</th><th>单价</th><th>金额</th></tr>"
    //var EmailBody = '编号：' + mailData.InvoiceID + '\n'
    //            + '币种：' + mailData.Currency + '\n'
    //            + '总金额：' + mailData.Total + '\n'
    //            + '订单列表' + '\n'

    //            + '订单单号,产品名称,次数,单价,金额' + '\n';
    var trList = "";
    //获取订单数据
    $.ajax({
        type: 'POST',
        url: "/Invoice/Detail",
        data: "InvoiceId=" + mailData.InvoiceID,
        success: function (data) {

            if (data.result == 1) {

                for (i = 0 ; i < data.list.length; i++) {
                    //EmailBody += data.list[i].OrderID + ',' + data.list[i].ProductName + ',' + data.list[i].UnitsOver + ',' + data.list[i].Price + ',' + data.list[i].TotalAmount;
                    trList += "<tr><td>" + data.list[i].OrderID + "</td><td>" + data.list[i].ProductName + "</td><td>" + data.list[i].UnitsOver + "</td><td>" + data.list[i].Price + "</td><td>" + data.list[i].TotalAmount + "</td></tr>";
                }
                var EmailBody = h1 + table + th0 + th0V + tableEnd + h2 + table + th1 + trList + tableEnd;
                // urlpara += "&EmailBody=" + encodeURIComponent(EmailBody);

                var EmailAppModel = {
                    functionName: "SetMail",
                    retId: mailData.InvoiceID,
                    ToMail: mailData.Email == null ? "" : mailData.Email,
                    Subject: "InvoceID:" + mailData.InvoiceID,
                    EmailBody: EmailBody
                };
                parent.sendEmailInvoces(EmailAppModel, mailData.MemberID)

                //$('#sendMail').dialog({
                //    title: title,
                //    method:'post',
                //    closed: false,
                //    cache: false,
                //    queryParams: { EmailAppModel: EmailAppModel },
                //    href: '/SearchDialog/SendDefaultMailPost?memberID=' + mailData.MemberID,
                //    modal: true,
                //});

            } else {

            }
        }
    });

}

//发送邮件
function SendEmail(reportId, bigType, SmallType, taskId, MemberName, Content, TaskListID) {
    console.log("*********sendemail****************");
    //debugger;
    var selectRows = $('#reportList').datagrid('getChecked');

    parent.sendEmailPrivew(reportId, bigType, SmallType, taskId, MemberName, Content, TaskListID);

}
function SelectProduct() {
    if ($("#IsDposit").is(':checked')) {
        $("#divdgProduct").hide();
    } else {
        $("#divdgProduct").show();
    }
}

function SetMail(mailInfo) {
    $('#sendMail').dialog('close');
    $.ajax({
        type: 'POST',
        url: "/Invoice/SendMail",
        data: "InvoiceId=" + mailInfo.Id + "&EmailID=" + mailInfo.EmailID,
        success: function (data) {
            if (data.result) {

                loadData();

            } else {

            }
        }
    });
}

function showEdit(InvoiceId, title, isEdit) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Invoice/Edit?InvoiceId=' + InvoiceId + '&isEdit=' + isEdit,
        iconCls: isEdit ? "icon-edit" : "icon_webedite",
        modal: true,
        onLoad: function () {

            try {
                var $a1 = $("#AddMemberName").next("span").find('a')[0];
                console.log($a1);
                $($a1).css("cursor", "pointer");
                $($a1).click(function () {
                    var orderInvoices = dgOrderProductEdit.thisdg.datagrid("getRows");
                    if (orderInvoices.length > 0) {
                        $.messager.alert('提示信息 ', '删除完所有订单才能重新选择会员！');
                        return false;
                    }
                    $('#searchdlg').dialog({
                        title: "选择会员",
                        closed: false,
                        cache: false,
                        href: '/SearchDialog/MemberSearch?functionName=GetMember',
                        modal: true
                    });
                });
                $("input", $("#addEmail").next("span")).bind('click', function () {
                    var orderInvoices = dgOrderProductEdit.thisdg.datagrid("getRows");
                    if (orderInvoices.length > 0) {
                        $.messager.alert('提示信息 ', '删除完所有订单才能重新选择会员！');
                        return false;
                    }
                    $('#searchdlg').dialog({
                        title: "选择会员",
                        closed: false,
                        cache: false,
                        href: '/SearchDialog/MemberSearch?functionName=GetMember',
                        modal: true
                    });
                });

            } catch (e) {
                console.log(e)
            }

            $("input", $("#AddDiscount").next("span")).bind('blur', function () {
                SetTotal();
            });
            $("input", $("#AddSubtotal").next("span")).bind('blur', function () {
                SetTotal();
            });
            $("input", $("#AddTotal").next("span")).bind('blur', function () {
                SetTotal();
            });
            SelectProduct();
            CurrencydisableEdit();
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

            }
        }
    });
}
function UploadImg(formId) {

    $("#" + formId).ajaxSubmit({
        url: "/Invoice/AddImage",
        type: 'post',
        success: function (res) {

            if (res.result) {

                $("#AddPicPath1").val(res.path);
                $("#AddPicPathImg").attr("href", "/MemberList/GetFileImg?content=" + encodeURIComponent(res.path));
                $("#AddPicPathImg").show();
            }
        },
        error: function (er) {
            alert('操作失败！');
        },
        dataType: 'json'
    });
}