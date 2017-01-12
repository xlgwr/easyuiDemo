var MemberGradeID;

var btnType = true;

$(function () {
    var sss = 0;

    //初始化回显
    $.ajax({
        type: 'POST',
        url: "/MemberList/EchoC",
        data: { Memberid: Memberid },
        success: function (data) {
            var dl = data.List;
            $('.huoyue').html(dl.LastSeachTimeString);
            $("#MemberName").val(dl.MemberName);
            $('#Password').val(dl.Password);
            $("#RemainingSum").val(dl.RemainingSum);
            $("#FullName_En").val(dl.MemberComanyModel.FullName_En);
            $("#FullName_Tm").val(dl.MemberComanyModel.FullName_Tm);
            $("#FullName_Cn").val(dl.MemberComanyModel.FullName_Cn);
            $("#CIBRNO").val(dl.MemberComanyModel.CIBRNO);
            $("#SeatNO").val(dl.MemberComanyModel.SeatNO);
            $("#Floor").val(dl.MemberComanyModel.Floor);
            $("#RoomNO").val(dl.MemberComanyModel.RoomNO);
            $("#BuildName").val(dl.MemberComanyModel.BuildName);
            $("#StreetNumber").val(dl.MemberComanyModel.StreetNumber);
            $("#Street").val(dl.MemberComanyModel.Street);
            $("#HouseNO").val(dl.MemberComanyModel.HouseNO);
            $("#City").val(dl.MemberComanyModel.City);
            $("#PostalCode").val(dl.MemberComanyModel.PostalCode);
            $("#IPMemberGrade1").combobox("setValue",dl.MemberGradeID);
            $("#IPInvoiceDate1").val(dl.InvoiceDate);
            $("#BusinessType").val(dl.MemberComanyModel.BusinessType);
            $("#CountryID").val(dl.MemberComanyModel.CountryID);
            $('#LPicPath1').val(dl.MemberComanyModel.PicPath1)
            $('#hLPicPath1').val(dl.MemberComanyModel.PicPath1)
            $('#LPicPath2').val(dl.MemberComanyModel.PicPath2)
            $('#hLPicPath2').val(dl.MemberComanyModel.PicPath2)
            $('#LPicPath3').val(dl.MemberComanyModel.PicPath3)
            $('#hLPicPath3').val(dl.MemberComanyModel.PicPath3)
            $('#LPicPath4').val(dl.MemberComanyModel.PicPath4)
            $('#hLPicPath4').val(dl.MemberComanyModel.PicPath4)
            $("#PaymentWay").val(dl.PaymentWay);

            $('#MBPicPath3').val(dl.PicPath3)
            $('#hLPicPathM3').val(dl.PicPath3)

            MemberGradeID = dl.MemberGradeID;
   
            var html;
            if (html == undefined) html = $("#contextlist").html();
            for (var i = 0; i < dl.ContactPersonModel.length; i++) {
                var dlc = dl.ContactPersonModel[i];
                if (i > 0) {
                    $("#addcontext").trigger("click")
                } 
                $('.contextlist2:eq(' + i + ') #Surname').val(dlc.Surname)
                $('.contextlist2:eq(' + i + ') #Salutation').val(dlc.Salutation)
                $('.contextlist2:eq(' + i + ') #GivenNames').val(dlc.GivenNames)
                $('.contextlist2:eq(' + i + ') #LFullName_Tm').val(dlc.FullName_Tm)
                $('.contextlist2:eq(' + i + ') #LFullName_Cn').val(dlc.FullName_Cn)
                $('.contextlist2:eq(' + i + ') #Department').val(dlc.Department)
                $('.contextlist2:eq(' + i + ') #Position').val(dlc.Position)
                $('.contextlist2:eq(' + i + ') #OfficeTel1').val(dlc.OfficeTel1)
                $('.contextlist2:eq(' + i + ') #OfficeTel2').val(dlc.OfficeTel2)
                $('.contextlist2:eq(' + i + ') #Fax').val(dlc.Fax)
                $('.contextlist2:eq(' + i + ') #Email').val(dlc.Email)
                $('.contextlist2:eq(' + i + ') #MobilePhone').val(dlc.MobilePhone)
                $('.contextlist2:eq(' + i + ') #hPicPath1').val(dlc.PicPath1)
                $('.contextlist2:eq(' + i + ') #PicPath1').val(dlc.PicPath1)
                $('.contextlist2:eq(' + i + ') #hPicPath2').val(dlc.PicPath2)
                $('.contextlist2:eq(' + i + ') #PicPath2').val(dlc.PicPath2)

                $('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel1').val(dlc.AreaCodeOfficeTel1)
                $('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel2').val(dlc.AreaCodeOfficeTel2)
                $('.contextlist2:eq(' + i + ') #AreaCodeFax').val(dlc.AreaCodeFax)
                $('.contextlist2:eq(' + i + ') #AreaCodeMobilePhone').val(dlc.AreaCodeMobilePhone)
                if(dlc.Default==1){
                    $('.contextlist2:eq(' + i + ') input:radio[name="Default"][value=1]').attr("checked", true);
                }
            }
            $("#FinancialLicenseNo").val(dl.MemberComanyModel.FinancialLicenseNo);
            $("#SecuritiesLicenceNo").val(dl.MemberComanyModel.SecuritiesLicenceNo);
            $("#FValidityDate").datebox('setValue', dl.MemberComanyModel.FValidityDateString);
            $("#SValidityDate").datebox('setValue', dl.MemberComanyModel.SValidityDateString);

            if (dl.Enable == 1) {
                $('input[name="Enable"][value=1]').attr("checked", true);
            }
            else {
                $('input[name="Enable"][value=0]').attr("checked", true);
            }
            debugger;
            $("#Purpose1").attr('checked', dl.Purpose1 == 0 ? false : true);
            $("#Purpose2").attr('checked', dl.Purpose2 == 0 ? false : true);
            $("#Purpose3").attr('checked', dl.Purpose3 == 0 ? false : true);
            $("#Purpose4").attr('checked', dl.Purpose4 == 0 ? false : true);
            $("#Purpose5").attr('checked', dl.Purpose5 == 0 ? false : true);
            $("#Purpose6").attr('checked', dl.Purpose6 == 0 ? false : true);
            $("#Purpose7").attr('checked', dl.Purpose7 == 0 ? false : true);
            $("#Pathway1").attr('checked', dl.Pathway1 == 0 ? false : true);
            $("#Pathway2").attr('checked', dl.Pathway2 == 0 ? false : true);
            $("#Pathway3").attr('checked', dl.Pathway3 == 0 ? false : true);
            $("#Pathway4").attr('checked', dl.Pathway4 == 0 ? false : true);
            $("#Pathway5").attr('checked', dl.Pathway5 == 0 ? false : true);
            $("#Pathway6").attr('checked', dl.Pathway6 == 0 ? false : true);


            $('#Purpose7Remark').val(dl.Purpose7Remark)
            $('#Pathway6Remark').val(dl.Pathway6Remark)
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


function submitEditC() {

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
    column.BusinessType = $.trim($("#BusinessType").val());
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
    column.CountryID = $.trim($("#CountryID").val());
    column.FinancialLicenseNo = $.trim($("#FinancialLicenseNo").val());
    column.FValidityDate = $.trim($("#FValidityDate").datetimebox('getValue'));
    column.SecuritiesLicenceNo = $.trim($("#SecuritiesLicenceNo").val());
    column.SValidityDate = $.trim($("#SValidityDate").datetimebox('getValue'));
    column.sLPicPath1 = $.trim($("#sLPicPath1").val());
    column.sLPicPath3 = $.trim($("#sLPicPath2").val());
    column.sLPicPath2 = $.trim($("#sLPicPath3").val());
    column.sLPicPath4 = $.trim($("#sLPicPath4").val());
    column.LPicPath1 = $.trim($("#hLPicPath1").val());
    column.LPicPath3 = $.trim($("#hLPicPath2").val());
    column.LPicPath2 = $.trim($("#hLPicPath3").val());
    column.LPicPath4 = $.trim($("#hLPicPath4").val());
    rpt_Company.push(column);
    //公司联系人


    var email1 = '1';
    var email2 = '2';
    var email3 = '3';
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
        column.Salutation = $.trim($('.contextlist2:eq(' + i + ') #Salutation').val());
        column.Name_Tm = $.trim($('.contextlist2:eq(' + i + ') #LFullName_Tm').val());
        column.Name_Cn = $.trim($('.contextlist2:eq(' + i + ') #LFullName_Cn').val());
        column.Default = $.trim($('.contextlist2:eq(' + i + ') #Default').attr("checked"));
        column.Department = $.trim($('.contextlist2:eq(' + i + ') #Department').val());
        column.Position = $.trim($('.contextlist2:eq(' + i + ') #Position').val());
        column.OfficeTel1 = $.trim($('.contextlist2:eq(' + i + ') #OfficeTel1').val());
        column.OfficeTel2 = $.trim($('.contextlist2:eq(' + i + ') #OfficeTel2').val());
        column.MobilePhone = $.trim($('.contextlist2:eq(' + i + ') #MobilePhone').val());
        column.Fax = $.trim($('.contextlist2:eq(' + i + ') #Fax').val());
        column.Email = $.trim($('.contextlist2:eq(' + i + ') #Email').val());
        column.sPicPath1 = $.trim($('.contextlist2:eq(' + i + ') #sPicPath1').val());
        column.sPicPath2 = $.trim($('.contextlist2:eq(' + i + ') #sPicPath2').val());
        column.PicPath1 = $.trim($('.contextlist2:eq(' + i + ') #hPicPath1').val());
        column.PicPath2 = $.trim($('.contextlist2:eq(' + i + ') #hPicPath2').val());

        column.AOfficeTel1 = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel1').val());
        column.AOfficeTel2 = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeOfficeTel2').val());
        column.AFax = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeFax').val());
        column.AMobilePhone = $.trim($('.contextlist2:eq(' + i + ') #AreaCodeMobilePhone').val());
        rpt_ContactPerson.push(column);
    }

    //会员信息
    column = new Object();
    column.Memberid = Memberid;
    column.MemberName = $.trim($("#MemberName").val());
    column.Password = $.trim($("#Password").val());
    column.MemberGrade = $.trim($('#IPMemberGrade1').combobox("getValue"));
    column.InvoiceDate = $.trim($('#IPInvoiceDate1').val());
    column.staty = $.trim($('input:radio[name="Enable"]:checked').val());
    column.RemainingSum = $.trim($("#RemainingSum").val());
    column.PaymentWay = $.trim($("#PaymentWay").val());
    column.LPicPathM3 = $.trim($("#hLPicPathM3").val());
    column.sLPicPathM3 = $.trim($("#sLPicPathM3").val());
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
        url: "/MemberList/EditSaveC",
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

function JudgeChecked(str) {
    if (str.attr("checked")) {
        return 1;
    }
    else {
        return 0;
    }
}