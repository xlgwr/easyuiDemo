var MemberGradeID;

var btnType = true;
$(function () {

    var sss = 0;

    //初始化回显
    $.ajax({
        type: 'POST',
        url: "/MemberList/EchoP",
        data: { Memberid: Memberid },
        success: function (data) {
            var dl = data.List;
            MemberGradeID = dl.MemberGradeID
            $('.huoyue').html(dl.LastSeachTimeString);
            $("#txtPMemberName").val(dl.MemberName);

            $("#IPMemberGrade1").combobox("setValue",dl.MemberGradeID);
            $('#txtPPassword').val(dl.Password);
            $("#IPInvoiceDate1").val(dl.InvoiceDate);
            $("#RemainingSum").val(dl.RemainingSum);
            if (dl.Enable == 1) {
                $('input[name="Enable"][value=1]').attr("checked", true);
            }
            else {
                $('input[name="Enable"][value=0]').attr("checked", true);
            }
            $("#txtPSurname").val(dl.Surname);
            $("#txtPGivenNames").val(dl.GivenNames);
            $("#txtPFullName_Cn").val(dl.FullName_Cn); 
            $("#txtPFullName_Tm").val(dl.FullName_Tm);
            $("#Salutation").val(dl.Salutation);
            $("#txtPSeatNO").val(dl.SeatNO);
            $("#txtPFloor").val(dl.Floor);
            $("#txtPRoomNO").val(dl.RoomNO);
            $("#txtPBuildName").val(dl.BuildName);
            $("#txtPStreetNumber").val(dl.StreetNumber);
            $("#txtPStreet").val(dl.Street);
            $("#txtPHouseNO").val(dl.HouseNO);
            $("#txtPCity").val(dl.City);
            $("#txtPPostalCode").val(dl.PostalCode);
            $("#CountryID").val(dl.CountryID);
            $("#txtPHomeTel").val(dl.HomeTel);
            $("#txtPOfficeTel").val(dl.OfficeTel);
            $("#txtPMobilePhone").val(dl.MobilePhone);
            $("#txtPEmail").val(dl.Email);
            $("#PaymentWay").val(dl.PaymentWay);
            $('#txtPPicPath1').val(dl.PicPath1)
            $('#hPicPath1').val(dl.PicPath1)
            $('#txtPPicPath2').val(dl.PicPath2)
            $('#hPicPath2').val(dl.PicPath2)
            $("#cbxPPurpose1").attr('checked', dl.Purpose1 == 0 ? false : true);
            $("#cbxPPurpose2").attr('checked', dl.Purpose2 == 0 ? false : true);
            $("#cbxPPurpose3").attr('checked', dl.Purpose3 == 0 ? false : true);
            $("#cbxPPurpose4").attr('checked', dl.Purpose4 == 0 ? false : true);
            $("#cbxPPurpose5").attr('checked', dl.Purpose5 == 0 ? false : true);
            $("#cbxPPurpose6").attr('checked', dl.Purpose6 == 0 ? false : true);
            $("#cbxPPurpose7").attr('checked', dl.Purpose7 == 0 ? false : true);
            $("#cbxPathway1").attr('checked', dl.Pathway1 == 0 ? false : true);
            $("#cbxPathway2").attr('checked', dl.Pathway2 == 0 ? false : true);
            $("#cbxPathway3").attr('checked', dl.Pathway3 == 0 ? false : true);
            $("#cbxPathway4").attr('checked', dl.Pathway4 == 0 ? false : true);
            $("#cbxPathway5").attr('checked', dl.Pathway5 == 0 ? false : true);
            $("#cbxPathway6").attr('checked', dl.Pathway6 == 0 ? false : true);

            $("#AreaCodeHomeTel").val(dl.AreaCodeHomeTel);
            $("#AreaCodeOfficeTel").val(dl.AreaCodeOfficeTel);
            $("#AreaCodeMobilePhone").val(dl.AreaCodeMobilePhone);
            $("#Purpose7Remark").val(dl.Purpose7Remark);
            $("#Pathway6Remark").val(dl.Pathway6Remark);
            $('#txtPPicPath3').val(dl.PicPath3)
            $('#hPicPath3').val(dl.PicPath3)

            console.log(dl);
        }
    });

    //#region  加载级别权限
    $("#IPMemberGrade1").combobox({
        onChange: function () {
            sss++;
            if (sss == 1) return;
            $(".quan").attr("checked", false);
            $.ajax({
                type: 'POST',
                url: "/MemberList/GetMemberGradeRightList",
                data: { MemberGradeID: $('#IPMemberGrade1').combobox('getValue') },
                success: function (result) {
                    var myJson = eval('(' + result + ')');

                    $('#tt').tree({
                        data: myJson
                    });
                }
            });
        }
    });

    $.ajax({
        type: 'GET',
        url: '/MemberList/GetMemberGradeRightKeyChk',
        data: { Memberid: Memberid },
        animate: true,
        checkbox: true,
        success: function (result) {
            var myJson = eval('(' + result + ')');

            $('#tt').tree({
                data: myJson
            });
        }
    });
});

function submitEditP() {

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


    $("#IPTable input[class='quan']").each(function () {
        column = new Object();
        column.ID = $(this).eq(0).attr('id');
        column.checked = $(this).prop('checked') == true ? 1 : 0;
        column.LanguageCode = $(this).eq(0).val();
        rpt_quanxian.push(column);
    });

    if (rpt_quanxian == null || rpt_quanxian == "") {
        $.messager.alert(title, "权限不能为空", "info");
        return;
    }
    //会员信息
    column = new Object();
    column.Memberid = Memberid;
    column.MemberName = $.trim($("#txtPMemberName").val());
    column.Password = $.trim($("#txtPPassword").val());
    column.MemberGrade = $.trim($('#IPMemberGrade1').combobox("getValue"));
    column.InvoiceDate = $.trim($('#IPInvoiceDate1').val());
    column.staty = $.trim($('input:radio[name="Enable"]:checked').val());
    column.RemainingSum = $.trim($("#RemainingSum").val());
    column.Surname = $.trim($("#txtPSurname").val());
    column.GivenNames = $.trim($("#txtPGivenNames").val());
    column.Salutation = $.trim($("#Salutation").val());
    column.FullName_Tm = $.trim($("#txtPFullName_Tm").val());
    column.FullName_Cn = $.trim($("#txtPFullName_Cn").val());
    column.SeatNO = $.trim($("#txtPSeatNO").val());
    column.Floor = $.trim($("#txtPFloor").val());
    column.RoomNO = $.trim($("#txtPRoomNO").val());
    column.BuildName = $.trim($("#txtPBuildName").val());
    column.StreetNumber = $.trim($("#txtPStreetNumber").val());
    column.Street = $.trim($("#txtPStreet").val());
    column.HouseNO = $.trim($("#txtPHouseNO").val());
    column.City = $.trim($("#txtPCity").val());
    column.PostalCode = $.trim($("#txtPPostalCode").val());
    column.CountryID = $.trim($("#CountryID").val());
    column.HomeTel = $.trim($("#txtPHomeTel").val());
    column.OfficeTel = $.trim($("#txtPOfficeTel").val());
    column.MobilePhone = $.trim($("#txtPMobilePhone").val());
    column.Email = $.trim($("#txtPEmail").val());
    column.PaymentWay = $.trim($("#PaymentWay").val());
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

    column.PicPath1 = $.trim($("#txtPPicPath1").val());
    column.hPicPath1 = $.trim($("#hPicPath1").val());
    column.SHPicPath1 = $.trim($("#SHPicPath1").val());
    column.PicPath2 = $.trim($("#txtPPicPath2").val());
    column.hPicPath2 = $.trim($("#hPicPath2").val());
    column.SHPicPath2 = $.trim($("#SHPicPath2").val());
    column.PicPath3 = $.trim($("#txtPPicPath3").val());
    column.hPicPath3 = $.trim($("#hPicPath3").val());
    column.SHPicPath3 = $.trim($("#SHPicPath3").val());
    column.AHomeTel = $.trim($("#AreaCodeHomeTel").val());
    column.AOfficeTel = $.trim($("#AreaCodeOfficeTel").val());
    column.AMobilePhone = $.trim($("#AreaCodeMobilePhone").val());


    column.Purpose7Remark = $("#Purpose7Remark").val();
    column.Pathway6Remark = $("#Pathway6Remark").val();
    rpt_Member.push(column);
    //#endregion

    $("#MemberAddForm").ajaxSubmit({
        type: "post",
        url: "/MemberList/EditSave",
        dataType: "json",
        data: {
            memberJson: JSON.stringify(rpt_Member),
            quanxian: JSON.stringify(rpt_quanxian)
        },
        success: function (json) {
            btnType = true;
            $.messager.alert(title, json.Msg, "info");
            if (json.result==1){
                $('#editdlg').dialog('close');
                $('#data1').datagrid('reload');
            }
        }
    });
}

function JudgeChecked(str) {
    if (str.attr("checked")) {
        return 1;
    }
    else {
        return 0;
    }
}