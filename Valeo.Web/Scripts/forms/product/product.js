function closeDialog() {
    $('#editdlg').dialog('close');
}
function showAdd(title) { 
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Product/Add',
        modal: true,
        //maximized: true,
        onLoad: function () { 
            $("input", $("#ProductId").next("span")).bind('blur', function () {
                $.ajax({
                    type: 'POST',
                    url: '/Product/Verification',
                    data: 'ProductId=' + $('#ProductId').val(),
                    success: function (resp) {
                        if (resp.result) {  
                            $.messager.alert('提示信息 ', resp.Msg, 'info', function () {
                                $("input", $("#ProductId").next("span")).focus();
                            });
                        }
                    }
                });
            });
            $('#Discount').numberbox({
                onChange: function (newValue, oldValue) {
                    var discountValue = Number($('#Discount').numberbox('getValue'))/100;
                    var priceValue = $('#Price').numberbox('getValue');
                    if (discountValue == 0) {
                        discountValue = 1;
                    }
                    var priceValueDis=(priceValue * discountValue).toFixed(0);
                    $('#DiscountPrice').numberbox('setValue', priceValueDis);

                    if (priceValueDis <= 0) {
                        $('#aisShow').combobox('select', 0)
                    }

                    //console.log(priceValue <= priceValueDis)
                }
            });
            $('#Price').numberbox({
                onChange: function (newValue, oldValue) {
                    var discountValue = Number($('#Discount').numberbox('getValue'))/100;
                    var priceValue = $('#Price').numberbox('getValue');
                    if (discountValue == 0) {
                        discountValue = 1;
                    }
                    var priceValueDis = (priceValue * discountValue).toFixed(0);
                    $('#DiscountPrice').numberbox('setValue', priceValueDis);


                    if (priceValueDis <= 0) {
                        $('#aisShow').combobox('select', 0)
                    }
                    //console.log(priceValue <= priceValueDis)

                }
            });
            $('#SmallType').combobox({
                onSelect: function (param) {
                    SmallTypeChange();
                }
            });
            $('#bigType').combobox({
                onSelect: function (param) {
                    BigTypeChange();
                    SmallTypeChange();
                }
            });
            $('#SeachType').combobox({
                onSelect: function (param) {
                    SeachTypeChange();
                }
            });
            $('#AddCurrency').combobox({
                onSelect: function (param) {
                    SetunitCurrency();
                }
            });
            SetunitCurrency();
            SmallTypeChange();
        }
    });
 
}
function SetunitCurrency() {
    if ($("#AddCurrency").combobox('getValue') == "RMB") {
        $(".unitCurrency").html("￥");
    } else {
        $(".unitCurrency").html("$");
    }
}

function SmallTypeChange() {
    var bigTypeValue = $('#bigType').combobox('getValues');
    var smallTypeValue = $('#SmallType').combobox('getValues');
    $("#tdAgeLimit1").hide();
    $("#tdAgeLimit2").hide();
    $("#trSeachAndRecord").hide();
    $("#trReport").hide();
    $("#trLender").hide();
    $("#trPeopleCount").hide();
    if (smallTypeValue == '1' && bigTypeValue == '1') //离线  公司
    {
        $("#trReport").show();
    } else if (smallTypeValue == '2' && bigTypeValue == '1') {//离线  土地
        $("#trSeachAndRecord").show();
        SeachTypeChange();
    } else if (smallTypeValue == '0') { // 法庭
        $("#tdAgeLimit1").show();
        $("#tdAgeLimit2").show();
    } else if (smallTypeValue == '3' && bigTypeValue == '0') { //在线 信贷
        $("#trLender").show();
    }
    if (bigTypeValue == '2') //自动监测
    {
        $("#trPeopleCount").show();
    }
}
function SeachTypeChange() {
    var seachTypeValue = $('#SeachType').combobox('getValues');
    $("#tdRecord1").hide();
    $("#tdRecord2").hide();
    if (seachTypeValue == '1') {
        $("#tdRecord1").show();
        $("#tdRecord2").show();
    }
}
function showEdit(productId, title, isEdit) {
    $('#editdlg').dialog({
        title: title,
        closed: false,
        cache: false,
        href: '/Product/Edit?productId=' + productId + '&isEdit=' + isEdit,
        modal: true,
        iconCls: isEdit ? "icon-edit" : "icon_webedite",
        //maximized: true,
        onLoad: function () {
            debugger;
            $('#Discount').numberbox({
                
                onChange: function (newValue, oldValue) { 
                    var discountValue = Number($('#Discount').numberbox('getValue'))/ 100;

                    var priceValue = $('#Price').numberbox('getValue');
                    if (discountValue==0) {
                        discountValue = 1;
                    }
                    var priceValueDis = (priceValue * discountValue).toFixed(0);
                    $('#DiscountPrice').numberbox('setValue', priceValueDis);

                    //console.log("****************1:" + (priceValue - priceValueDis))

                    if (priceValueDis <= 0) {
                        $('#aisShow').combobox('select', 0)
                    }
                }
            });
            $('#Price').numberbox({
                onChange: function (newValue, oldValue) {
                    var discountValue = Number($('#Discount').numberbox('getValue')) / 100;
                    var priceValue = $('#Price').numberbox('getValue');
                    if (discountValue == 0) {
                        discountValue = 1;
                    }
                    //console.log("****************1:" + (priceValue - priceValueDis))
                    var priceValueDis = (priceValue * discountValue).toFixed(0);
                    $('#DiscountPrice').numberbox('setValue', priceValueDis);
                    if (priceValueDis<=0) {
                        $('#aisShow').combobox('select', 0)
                    }
                }
            });
            $('#SmallType').combobox({
                onSelect: function (param) {
                    SmallTypeChange();
                }
            });
            $('#bigType').combobox({
                onSelect: function (param) {
                    BigTypeChange(); 
                    SmallTypeChange();
                }
            });
            $('#SeachType').combobox({
                onSelect: function (param) {
                    SeachTypeChange();
                }
            });
            $('#AddCurrency').combobox({
                onSelect: function (param) {
                    SetunitCurrency();
                }
            });
            SetunitCurrency();
            BigTypeChange();
            SetSmallType();
            SmallTypeChange();

            SetReportType();

        }
    }); 
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
                   //loadData();
                   var pars = {
                       BigType: $("#BigTypeSearch").combobox("getValue"),
                       SmallType: $("#SmallTypeSearch").combobox("getValue"),
                       ProductNo: $("#ProductNoSearch").val(),
                       MemberGradeID: $("#MemberGradeIDSearch").combobox("getValue"),
                       Currency: $("#CurrencySearch").combobox("getValue")
                   };
                   var url = "/Product/ProductPage";
                   loadData(pars, url);
                });
                
            } else {
                $.messager.alert('提示信息 ', data.Msg);
            }
        }
    });
}