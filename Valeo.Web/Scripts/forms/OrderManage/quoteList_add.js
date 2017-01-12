
//1:修改
var etoolbarProduct = [{
    text: '添加产品',
    iconCls: 'icon-add',
    handler: function () { showProdctAdd(1); }
}, '-', {
    text: '删除产品',
    iconCls: 'icon-remove',
    handler: function () { remove(1); }
}, '-', {
    text: '接受修改',
    iconCls: 'icon-save',
    handler: function () { accept(1) }
}];
//addDataToList = getDataProduct();
//function getDataProduct() {
//    var rows = [];
//    for (var i = 1; i <= 10; i++) {
//        var amount = Math.floor(Math.random() * 1000);
//        var price = Math.floor(Math.random() * 1000);

//        rows.push({
//            ApplyID: i,
//            Price: price,
//            ProductId: i,
//            ProductName: 'Name ' + i,
//            Remark: 'Note ' + i,
//            TotalAmount: amount * price,
//            UnitsBuy: amount,
//            Validity: $.fn.datebox.defaults.formatter(new Date())
//        });
//    }
//    return rows;
//}




//flag，0：add,1:edit
function selectID(row) {
    var arrList = [];
    var flag = currFlag;
    var $TotalAmount = undefined;

    if (flag) {
        arrList = editDataToList
        $TotalAmount = $('#eTotalAmount');
    } else {
        arrList = addDataToList
        $TotalAmount = $('#aTotalAmount');

    }
    //console.log($("#GetListProdct").datagrid('getData'));
    //console.log(typeof (a));
    if (row) {
        //console.log(row);
        //console.log(arrList);

        var exit = 0;
        var currIndex = -1;
        for (var i = 0; i < arrList.length; i++) {
            var tmpa = arrList[i];
            if (tmpa.ProductId === row.ProductId) {
                //console.log(tmpa);
                arrList[i].UnitsBuy = parseInt(arrList[i].UnitsBuy, 10) + 1;
                arrList[i].TotalAmount = parseInt(arrList[i].UnitsBuy, 10) * parseFloat(tmpa.Price);
                currIndex = i;
                exit = 1;
                break;
            }
        }
        if (exit === 0) {
            arrList.push({
                //ApplyID: i,
                Price: row.Price,
                Currency: row.Currency,
                ProductId: row.ProductId,
                ProductName: row.ProductName,
                Remark: 'Note',
                TotalAmount: row.Price,
                UnitsBuy: 1,
                Validity: row.Validity
            });
        }

        if (flag) {
            editDataToList = arrList
        } else {
            addDataToList = arrList
        }

        updateLoadData(flag);

        if (flag) {
            $('#edgProduct').datagrid('selectRow', currIndex == -1 ? (arrList.length - 1) : currIndex);
        } else {
            $('#adgProduct').datagrid('selectRow', currIndex == -1 ? (arrList.length - 1) : currIndex);
        }

        $('#e2ditdlg').dialog('close');

    } else {
        alert("没有选择任何记录。")
    }
}
//flag，0：add,1:edit
function updateLoadData(flag) {
    var arrList = [];
    var $TotalAmount = undefined;
    var dgProduct = undefined;

    if (flag) {
        arrList = editDataToList
        $TotalAmount = $('#eTotalAmount');
        dgProduct = $('#edgProduct');
    } else {
        arrList = addDataToList
        $TotalAmount = $('#aTotalAmount');
        dgProduct = $('#adgProduct');
    }
    //console.log(arrList);
    var sum = 0;
    for (var i = 0; i < arrList.length; i++) {
        var tmpa = arrList[i];
        arrList[i].TotalAmount = parseInt(arrList[i].UnitsBuy, 10) * parseFloat(tmpa.Price)
        sum += arrList[i].TotalAmount
    }
    //console.log(sum);

    $TotalAmount.textbox('setValue', sum);

    dgProduct.datagrid("loadData", arrList);
}

var aeditIndex = undefined;
var eeditIndex = undefined;

//flag，0：add,1:edit
function endEditing(flag) {
    var editIndex = undefined;
    var dgProduct = undefined;

    if (flag) {
        editIndex = eeditIndex
        dgProduct = $('#edgProduct');
    } else {
        editIndex = aeditIndex
        dgProduct = $('#adgProduct');

    }
    if (editIndex == undefined) { return true }

    if (dgProduct.datagrid('validateRow', editIndex)) {

        var ed = dgProduct.datagrid('getEditor', { index: editIndex, field: 'Validity' });
        //console.log(ed);
       //console.log(editIndex);

        var productname = $(ed.target).combobox('getText');
        dgProduct.datagrid('getRows')[editIndex]['label'] = productname;

        dgProduct.datagrid('endEdit', editIndex);
        if (flag) {
            eeditIndex = undefined
        } else {
            aeditIndex = undefined
        }
        updateLoadData(flag);
        return true;
    } else {
        return false;
    }
}
function aonClickRow(index) {
    var editIndex = aeditIndex

    if (editIndex != index) {
        if (endEditing(0)) {
            $('#adgProduct').datagrid('selectRow', index)
                    .datagrid('beginEdit', index);
        } else {
            $('#adgProduct').datagrid('selectRow', editIndex);
        }
    }
    aeditIndex = index;
    //console.log(editIndex);
}
function eonClickRow(index) {
    var editIndex = eeditIndex

    if (editIndex != index) {
        if (endEditing(1)) {
            $('#edgProduct').datagrid('selectRow', index)
                    .datagrid('beginEdit', index);
        } else {
            $('#edgProduct').datagrid('selectRow', editIndex);
        }
    }
    eeditIndex = index;
    //console.log(editIndex);
}
function onClose() {
    //console.log("on Close.");
    eeditIndex = undefined;
    aeditIndex = undefined;
}
function accept(flag) {

    var dgProduct = undefined;
    if (flag) {
        dgProduct = $('#edgProduct');
    } else {
        dgProduct = $('#adgProduct');
    }

    if (endEditing(flag)) {
        dgProduct.datagrid('acceptChanges');
    }
    updateLoadData(flag);
}
function remove(flag) {
    //console.log(editIndex);
    if (flag) {
        if (eeditIndex == undefined) { return }
        editDataToList.splice(eeditIndex, 1);
        eeditIndex = undefined;
    } else {
        if (aeditIndex == undefined) { return }
        addDataToList.splice(aeditIndex, 1);
        aeditIndex = undefined;
    }
    updateLoadData(flag);
}