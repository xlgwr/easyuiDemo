
var currCurrency = undefined;
var currFlag = undefined;
var addDataToList = [];
var editDataToList = [];

var pars = {
    QuoteDateFrom: undefined,
    QuoteDateTo: undefined,
    QuoteID: undefined,
    MemberID: undefined,
    QuoteConfirm: undefined
};

function findData(data, id) {
    var row = undefined;
    //console.log(data, id);
    if (data.rows) {
        for (var i = 0; i < data.rows.length; i++) {
            var tmprow = data.rows[i];
            if (id == tmprow.QuoteID) {
                row = tmprow;
                break;
            }
        }
    }
    return row;
}
////
function showEdit(id) {
    var submitform = $('#ffedit');
    var currrow = findData(tmpdata, id);

    //console.log(currrow);
    if (currrow) {
        submitform.form('load', {
            QuoteID: currrow.QuoteID,
            QuoteDate: currrow.QuoteDate,
            QuoteConfirm: currrow.QuoteConfirm,
            MemberID: currrow.MemberID,
            Currency: currrow.Currency,
            TotalAmount: currrow.TotalAmount,
            Invoice: currrow.Invoice,
            //PathQuote: currrow.PathQuote,
            //PathContract: currrow.PathContract,
            Remark: currrow.Remark
        });

        editDataToList = [];

        $.post('/QuoteList/GetQuoteProdct', "quoteID=" + currrow.QuoteID, function (res) {
            //console.log(res);
            if (res.result) {
                editDataToList = res.rows;
            } else {
                alert(res.Msg);
                return;
            }

            $('#edgProduct').datagrid('loadData', editDataToList);
        });

        $('#editdlg').dialog('open')
    } else {
        alert("@BaseRes.DLL_MSG_001")
    }

}

function submitForm(formId, dialog) {
    var submitform = $('#' + formId);
    $.ajax({
        type: 'POST',
        url: submitform.attr('action'),
        data: submitform.serialize(),
        success: function (data) {
            if (data.result) {
                submitform[0].reset();
                closeDialog(dialog);
                loadData();
            } else {

            }
        }
    });
}



function update(id) {
    alert(id);
}

function cancel(id) {
    $('#adddlg').dialog('close');
}
