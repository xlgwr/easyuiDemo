/// <reference path="../../../jquery.min.js" />

//个人
var btnType = true;

$(function () {

    $('#tt').tree({
        onCheck: function (node, checked) {
            var rpt_quanxian = new Array();
            var nodes = $('#tt').tree('getChildren');
            if (node.id == "key_4" && node.checked) {
                nodes = $('#tt').tree('getChildren');
                var ix = "";
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].id == 'key_66' || nodes[i].id == 'key_65') {
                        nodes[i].checkState = 'checked';
                        nodes[i].checked = true
                    }
                };
            }
            else if (node.id == "key_4" && !node.checked) {
                nodes = $('#tt').tree('getChildren');
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].id == 'key_66' || nodes[i].id == 'key_65') {
                        nodes[i].checkState = 'unchecked';
                        nodes[i].checked = false
                    }
                };
            }
            else if (node.id == "key_66" || node.id == "key_65") {
                var key_66 = $('#tt').tree('find', 'key_66');
                var key_65 = $('#tt').tree('find', 'key_65');
                if (!key_66.checked && !key_65.checked) {
                    for (var i = 0; i < nodes.length; i++) {
                        if (nodes[i].id == 'key_4' ) {
                            nodes[i].checkState = 'unchecked';
                            nodes[i].checked = false
                        }
                    };
                }
            }



            rpt_quanxian = getChildren2();
            if (rpt_quanxian.length < 0) return;
            $.ajax({
                type: 'POST',
                url: "/MemberList/getChildren",
                data: { rpt_quanxian: JSON.stringify(rpt_quanxian) },
                success: function (result) {
                    var myJson = eval('(' + result + ')');
                    $('#tt').tree({
                        data: myJson
                    });
                }
            });
        }
    });

    function getChildren2() {

        var rpt_quanxian = new Array();
        nodes = $('#tt').tree('getChildren');
        var ix = "";
        for (var i = 0; i < nodes.length; i++) {
            column = new Object();
            if (nodes[i].id.length < 2) {
                ix = nodes[i].id;
                continue;
            }
            column.ID = nodes[i].id;
            column.checked = nodes[i].checked == true ? 1 : 0;
            column.LanguageCode = nodes[i].tag;
            column.ix = ix;
            rpt_quanxian.push(column);
        };
        return rpt_quanxian;
    }

    $(document).on('click', '.myfile .files', function () {
        debugger;
        var rpt_quanxian = new Array();
        var nodes = $('#tt').tree('getChildren');
        for (var i = 0; i < nodes.length; i++) {
            column = new Object();
            if (nodes[i].id.length < 2) continue;
            column.ID = nodes[i].id;
            column.checked = nodes[i].checked == true ? 1 : 0;
            column.LanguageCode = nodes[i].tag;
            rpt_quanxian.push(column);
        }
        debugger;


    //$(".myfile .files").click(function () {
        $('#lImg').attr('src', 'data:image/png;base64,' + "")
        $('#divIMG').dialog({
            title: "图片查看",
            bgiframe: true,
            resizable: false,
            height: 400,
            width: 400,
            modal: true
        });
        $.ajax({
            type: "post",
            dataType: "text",
            url: '/MemberList/GetFileASCII', //回发到的页面  
            data: { content: $(this).parent().children(".hPath").val() },
            success: function (data) {
                $('#lImg').css("width", "98%");
                $('#lImg').css("height", "98%");
                $('#lImg').attr('src', 'data:image/png;base64,' + data)
            }
        });
    });

    //#region  提醒发票日改变索引
    $("#IPInvoiceDate").combobox({
        value: null,
        filter: function (q, row) { //q：文本框内你输入的值，row：列表数据集合；
            var opts = $(this).combobox('options');
            //统一转换成小写进行比较；==0：匹配首位，>=0：匹配任意位置；
            return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0;
        },
        onHidePanel: function () {
            //文本框内的文本；
            var txt_Box = $("#IPInvoiceDate").combobox('getText');
            //获取列表数据；
            var listdata = $.data(this, "combobox");
            var rowdata = listdata.data;

            //遍历列表，若匹配则将值设为当前列并返回；否则将值设为null；
            for (var i = 0; i < rowdata.length; i++) {
                var _row = rowdata[i];
                var txt_Val = _row["text"];
                if (txt_Val.toLowerCase() == $.trim(txt_Box.toLowerCase())) {
                    $("#IPInvoiceDate").combobox('setValue', _row["value"]);
                    $("#IPInvoiceDate").val(_row["value"]);
                    return;
                }
                else if ($.trim(txt_Box.toLowerCase()).length <= 0) {
                    $("#IPInvoiceDate").val("");
                    return;
                }
            }
            //若函数没有返回（即没有与文本框内容匹配的条目），值为null，清空文本框；
            $("#IPInvoiceDate").combobox('setValue', rowdata.length);
        }
    });
    //#endregion

    //#region  加载级别权限
    $("#IPMemberGrade").combobox({
        onChange: function () {
            IPMemberGradeChange();
        }
    });

    function IPMemberGradeChange() {
        $(".quan").attr("checked", false);
        $.ajax({
            type: 'POST',
            url: "/MemberList/GetMemberGradeRightList",
            data: { MemberGradeID: $('#IPMemberGrade').combobox('getValue') },
            success: function (result) {
                debugger;
                var myJson = eval('(' + result + ')');
                $('#tt').tree({
                    data: myJson
                });
            }
        });
    }
    //#endregion
    $("#btnPCancel").click(function () {
        $(".IPEnable  input[type='radio']").get(0).checked = true;
        $("#Individual input[type='text']").val("");
        $("#IPMemberGrade").combobox('setValue', "");
        $("#IPSalutation").combobox('setValue', 1);
        $("#IPCountryID").combobox('setValue', "");
        $("#Individual").window("close");
    });
});

$(function () {
    var data = new Date();

    var html;
    if (html == undefined) html = $("#contextlist").html();
    /*追加联系人-------------begin---------------*/
    $(document).on("click", ".deletebtn", function () {
        console.log($('#editdlg').length);
        debugger;
        if ($("#contextlist").children().length < 1) {
            $.messager.alert(title, "至少添加一位联系人", "info");
            return
        };
        var file = $(this).parent().parent().parent().parent().parent().parent().find("input");
        file.after(file.clone().val("")).remove();
        $(this).parent().parent().parent().parent().parent().parent().remove();
    });
    //追加联系人
    $("#addcontext").click(function () {
        if (html == undefined) html = $("#contextlist").html();
        /*验证最多有三条联系人*/
        if ($("#contextlist").children().length > 2) {
            $.messager.alert(title, "最多添加三位联系人", "info");
            return
        };
        /*追加联系人信息*/
        $(html).appendTo("#contextlist");
        $.parser.parse(".contextlist2")
    });
    /*追加联系人-------------end---------------*/
});


//$(document).on('change', '.myfile input[type=file]', function () {
function addimg(obj, file_size) {
    console.log(obj.length);
    var filepath = obj.val();
    if (obj.val().length > 0) {
        var extStart = filepath.lastIndexOf(".");
        var ext = filepath.substring(extStart, filepath.length).toUpperCase();
        if (ext != ".BMP" && ext != ".JPG" && ext != ".JPEG" && ext != ".PNG" && ext != ".TIFF") {
            $.messager.alert(title, "图片限于bmp、jpg、jpeg、png、tiff格式", "info");
            obj.after(obj.clone().val("")).remove();
            return false;
        }
        debugger;
        var size = file_size / 1024;
        if (size > 10240) {
            $.messager.alert(title, "上传的图片大小不能超过10M！", "info");
            obj.after(obj.clone().val("")).remove();
            return false;
        }
    }
    return true;
}
//});


function showAddP(title) {
    $("#editdlg").append("");
    $('#editdlg').dialog({
        title: title,
        width: 1150 ,
        height: 550,
        closed: false,
        cache: false,
        href: '/MemberList/AddP',
        modal: true,
        onLoad: function () {
            defaultMember();
        }
    });
}

function showAddC(title) {
    $("#editdlg").append("");
    $('#editdlg').dialog({
        title: title,
        width: 1150,
        height: 550,
        closed: false,
        cache: false,
        href: '/MemberList/AddC',
        modal: true,
        onLoad: function() {
            defaultMember();
        }
    });
}

function showEditP(title, start, isEdit) {
    debugger;
    var checkedrows;
    var memberid;
    var type;
    var titname;
    if (start == 1 || start == 0) {
        if (start == 1) {
            checkedrows = $('#data').datagrid('getSelected');
        }
        else if (start == 0) {
            checkedrows = $('#date1').datagrid('getSelected');
        }
        if (checkedrows == null) {
            $.messager.alert(title, "请选择会员", "info");
            return;
        }
        memberid = checkedrows["MemberID"];
        type = start;
        titname = title;
    }
    else {
        memberid = title;
        type = isEdit;
        titname = start;
    }

    $("#editdlg").append("");
    if (type == 0) {
        $('#editdlg').dialog({
            width: 1150,
            height: 550,
            title: titname,
            closed: false,
            cache: false,
            href: '/MemberList/EditP?Memberid=' + memberid + '&isEdit=' + isEdit,
            modal: true
        });
    }
    else if (type == 1) {
        $('#editdlg').dialog({
            width: 1150,
            height: 550,
            title: titname,
            closed: false,
            cache: false,
            href: '/MemberList/EditC?Memberid=' + memberid + '&isEdit=' + isEdit,
            modal: true
        });
    }
}

function JudgeChecked(str) {
    if (str.attr("checked")) {
        return 1;
    }
    else {
        return 0;
    }
}

function submit() {

    if (!btnType) return;
    btnType = false;

    //#region 公司信息绑定
    var column = new Object();
    var rpt_Company = new Array();//公司信息
    var rpt_ContactPerson = new Array();//公司联系人信息
    var rpt_Member = new Array();//会员信息
    var rpt_quanxian = new Array();

    var nodes = $('#tt').tree('getChildren');
    for (var i = 0; i < nodes.length; i++) {
        column = new Object();
        if (nodes[i].id.length < 2) continue;
        column.ID = nodes[i].id;
        column.checked = nodes[i].checked == true ? 1 : 0;
        column.LanguageCode = nodes[i].tag;
        rpt_quanxian.push(column);
    }

    if (rpt_quanxian == null || rpt_quanxian == "") {
        $.messager.alert(title, "权限不能为空", "info");
        return;
    }
    column = new Object();
    column.CompanyName_En = $.trim($("#FullName_En").val());
    column.CompanyName_Tm = $.trim($("#FullName_Tm").val());
    column.CompanyName_Cn = $.trim($("#FullName_Cn").val());
    column.BusinessType = $.trim($("#BusinessType").combobox('getValue'));
    column.CIBRNO = $.trim($("#CIBRNO").val());
    column.SeatNO = $.trim($("#SeatNO").val());
    column.Floor = $.trim($("#Floor").val());
    column.RoomNO = $.trim($("#RoomNO").val());
    column.BuildName = $.trim($("#BuildName").val());
    column.StreetNumber = $.trim($("#StreetNumber").val());
    column.Street = $.trim($("#Street").val());
    column.HouseNO = $.trim($("#HouseNO").val());
    column.City = $.trim($("#City").val());
    column.PostalCode = $.trim($("#PostalCode").val());
    column.CountryID = $.trim($("#CountryID").combobox('getValue'));
    column.FinancialLicenseNo = $.trim($("#FinancialLicenseNo").val());
    column.FValidityDate = $.trim($("#FValidityDate").datetimebox('getValue'));
    column.SecuritiesLicenceNo = $.trim($("#SecuritiesLicenceNo").val());
    column.SValidityDate = $.trim($("#SValidityDate").datetimebox('getValue'));
    column.sLPicPath1 = $.trim($("#sLPicPath1").val());
    column.sLPicPath3 = $.trim($("#sLPicPath2").val());
    column.sLPicPath2 = $.trim($("#sLPicPath3").val());
    column.sLPicPath4 = $.trim($("#sLPicPath4").val());
    rpt_Company.push(column);
    var email1 = '1';
    var email2 = '2';
    var email3 = '3';
    //公司联系人
    for (var i = 0; i < $("#contextlist").children().length ; i++) {
        column = new Object();
        if (i == 0) email1 = $.trim($(".contextlist2:eq(" + i + ") #Email").val());
        if (i == 1) email2 = $.trim($(".contextlist2:eq(" + i + ") #Email").val());
        if (i == 2) email3 = $.trim($(".contextlist2:eq(" + i + ") #Email").val());
        //电邮不能重复
        if (email1 == email2 || email1 == email3 || email2 == email3) {
            $.messager.alert(title, "电邮不能相同", "info");
            return;
        }
        column.Surname = $.trim($('.contextlist2:eq(' + i + ') #Surname').val());
        column.GivenNames = $.trim($('.contextlist2:eq(' + i + ') #GivenNames').val());
        //column.Salutation = $.trim($('.contextlist2:eq(' + i + ') #Salutation').combobox('getText'));
        column.Salutation = $.trim($('.contextlist2:eq(' + i + ') #Salutation').val());
        column.Name_Tm = $.trim($('.contextlist2:eq(' + i + ') #LFullName_Tm').val());
        column.Name_Cn = $.trim($('.contextlist2:eq(' + i + ') #LFullName_Cn').val());
        column.Default = $.trim($('.contextlist2:eq(' + i + ') #Default').attr("checked"));
        //column.Default = $.trim($('.contextlist2:eq(' + i + ') #Default').attr("checked"));
        column.Department = $.trim($('.contextlist2:eq(' + i + ') #Department').val());
        column.Position = $.trim($('.contextlist2:eq(' + i + ') #Position').val());
        column.OfficeTel1 = $.trim($('.contextlist2:eq(' + i + ') #OfficeTel1').val());
        column.OfficeTel2 = $.trim($('.contextlist2:eq(' + i + ') #OfficeTel2').val());
        column.MobilePhone = $.trim($('.contextlist2:eq(' + i + ') #MobilePhone').val());
        column.Fax = $.trim($('.contextlist2:eq(' + i + ') #Fax').val());
        column.Email = $.trim($('.contextlist2:eq(' + i + ') #Email').val());
        column.sPicPath1 = $.trim($('.contextlist2:eq(' + i + ') #sPicPath1').val());
        column.sPicPath2 = $.trim($('.contextlist2:eq(' + i + ') #sPicPath2').val());

        column.AOfficeTel1 = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel1').val());
        column.AOfficeTel2 = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel2').val());
        column.AMobilePhone = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeMobilePhone').val());
        column.AFax = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeFax').val());
        rpt_ContactPerson.push(column);
    }

    //会员信息
    column = new Object();
    column.MemberName = $.trim($("#MemberName").val()); 
    column.Password = $.trim($("#Password").val());
    column.MemberGrade = $.trim($('#IPMemberGrade').combobox('getValue'));
    column.InvoiceDate = $.trim($('#IPInvoiceDate').combobox('getValue'));
    column.staty = $.trim($('#staty').val());
    var RemainingSum = $.trim($("#RemainingSum").val());
    if ($.trim($("#RemainingSum").val()).length < 0 || $.trim($('#RemainingSum').val()) == "") {
        RemainingSum = 0
    }
    column.RemainingSum = RemainingSum;
    column.PaymentWay = $.trim($("#PaymentWay").combobox('getValue'));
    column.Purpose1 = JudgeChecked($("#Purpose1"));
    column.Purpose2 = JudgeChecked($("#Purpose2"));
    column.Purpose3 = JudgeChecked($("#Purpose3"));
    column.Purpose4 = JudgeChecked($("#Purpose4"));
    column.Purpose5 = JudgeChecked($("#Purpose5"));
    column.Purpose6 = JudgeChecked($("#Purpose6"));
    column.Purpose7 = JudgeChecked($("#Purpose7"));
    column.Pathway1 = JudgeChecked($("#Pathway1"));
    column.Pathway2 = JudgeChecked($("#Pathway2"));
    column.Pathway3 = JudgeChecked($("#Pathway3"));
    column.Pathway4 = JudgeChecked($("#Pathway4"));
    column.Pathway5 = JudgeChecked($("#Pathway5"));
    column.Pathway6 = JudgeChecked($("#Pathway6"));

    column.Purpose7Remark = $("#Purpose7Remark").val();
    column.Pathway6Remark = $("#Pathway6Remark").val();
    rpt_Member.push(column);
    //#endregion

    $("#MemberAddForm").ajaxSubmit({
        type: "post",
        url: "/MemberList/AddSaveC",
        dataType: "json",
        data: {
            memberJson: JSON.stringify(rpt_Member),
            companyJson: JSON.stringify(rpt_Company),
            contactPersonJson: JSON.stringify(rpt_ContactPerson),
            quanxian: JSON.stringify(rpt_quanxian)
        },
        success: function (json) {
            btnType = true;
            $.messager.alert(title, json.Msg, "info");
            if (json.result == 1) {
                $('#editdlg').dialog('close');
                $('#data').datagrid('reload');
            }
        }
    });
}

function submitForm() {

    if (!btnType) return;
    btnType = false;

    //#region 公司信息绑定
    var column = new Object();
    var rpt_Member = new Array();//会员信息
    var rpt_quanxian = new Array();
    var nodes = $('#tt').tree('getChildren');
    for (var i = 0; i < nodes.length; i++) {
        column = new Object();
        if (nodes[i].id.length < 2) continue;
        column.ID = nodes[i].id;
        column.checked = nodes[i].checked == true ? 1 : 0;
        column.LanguageCode = nodes[i].tag;
        rpt_quanxian.push(column);
    }

    if (rpt_quanxian == null || rpt_quanxian == "") {
        $.messager.alert(title, "权限不能为空", "info");
        return;
    }
    //会员信息
    column = new Object();
    column.MemberName = $.trim($("#txtPMemberName").val());
    column.Password = $.trim($("#txtPPassword").val());
    column.MemberGrade = $.trim($('#IPMemberGrade').combobox('getValue'));
    column.InvoiceDate = $.trim($('#IPInvoiceDate').combobox('getValue'));
    column.staty = $.trim($('#staty').val());
    var RemainingSum = $.trim($("#RemainingSum").val());
    if ($.trim($("#RemainingSum").val()).length < 0 || $.trim($('#RemainingSum').val()) == "")
    {
        RemainingSum=0
    }
    column.RemainingSum = RemainingSum;
    column.Surname = $.trim($('#txtPSurname').val());
    column.GivenNames = $.trim($('#txtPGivenNames').val());
    column.Salutation = $.trim($('#Salutation').combobox('getValue'));
    column.Name_Tm = $.trim($('#txtPFullName_Tm').val());
    column.Name_Cn = $.trim($('#txtPFullName_Cn').val());
    column.SeatNO = $.trim($("#txtPSeatNO").val());
    column.Floor = $.trim($("#txtPFloor").val());
    column.RoomNO = $.trim($("#txtPRoomNO").val());
    column.BuildName = $.trim($("#txtPBuildName").val());
    column.StreetNumber = $.trim($("#txtPStreetNumber").val());
    column.Street = $.trim($("#txtPStreet").val());
    column.HouseNO = $.trim($("#txtPHouseNO").val());
    column.City = $.trim($("#txtPCity").val());
    column.PostalCode = $.trim($("#txtPPostalCode").val());
    column.CountryID = $.trim($("#CountryID").combobox('getValue'));
    column.HomeTel = $.trim($("#txtPHomeTel").val());
    column.OfficeTel = $.trim($("#txtPOfficeTel").val());
    column.MobilePhone = $.trim($("#txtPMobilePhone").val());
    column.Email = $.trim($("#txtPEmail").val());
    column.sLPicPath1 = $.trim($("#SHPicPath1").val());
    column.sLPicPath2 = $.trim($("#SHPicPath2").val());
    column.PaymentWay = $.trim($("#PaymentWay").combobox('getValue'));
    column.Purpose1 = JudgeChecked($("#cbxPPurpose1"));
    column.Purpose2 = JudgeChecked($("#cbxPPurpose2"));
    column.Purpose3 = JudgeChecked($("#cbxPPurpose3"));
    column.Purpose4 = JudgeChecked($("#cbxPPurpose4"));
    column.Purpose5 = JudgeChecked($("#cbxPPurpose5"));
    column.Purpose6 = JudgeChecked($("#cbxPPurpose6"));
    column.Purpose7 = JudgeChecked($("#cbxPPurpose7"));
    column.Pathway1 = JudgeChecked($("#cbxPathway1"));
    column.Pathway2 = JudgeChecked($("#cbxPathway2"));
    column.Pathway3 = JudgeChecked($("#cbxPathway3"));
    column.Pathway4 = JudgeChecked($("#cbxPathway4"));
    column.Pathway5 = JudgeChecked($("#cbxPathway5"));
    column.Pathway6 = JudgeChecked($("#cbxPathway6"));
    column.Purpose7Remark = $("#Purpose7Remark").val();
    column.Pathway6Remark = $("#Pathway6Remark").val();


    column.AHomeTel = $.trim($("#AreaCodeHomeTel").val());
    column.AOfficeTel = $.trim($("#AreaCodeOfficeTel").val());
    column.AMobilePhone = $.trim($("#AreaCodeMobilePhone").val());
    rpt_Member.push(column);
    $("#MemberAddForm").ajaxSubmit({
        type: "post",
        url: "/MemberList/AddSaveP",
        dataType: "json",
        data: {
            memberJson: JSON.stringify(rpt_Member),
            quanxian: JSON.stringify(rpt_quanxian)
        },
        success: function (json) {
            btnType = true;
            $.messager.alert(title, json.Msg,"info");
            if (json.result == 1) {
                $('#editdlg').dialog('close');
                $('#data1').datagrid('reload');
            }
        }
    });
}





