/// <reference path="../../bootstrap/jquery-1.7.1.js" />

//页面初始化
$(function () {
    $("#btnSubmit").click( function () {
        if ($("#company").is(":visible")) {
            EditCompany();
        }
        if ($("#individual").is(":visible")) {
            EditIndividual()
        }
    })


    //#region 个人注册
    function EditIndividual() {
        //#region 个人信息绑定
        var column = new Object();
        var rpt_member = new Array();
        column.MemberName = $("#txtGMemberName").val();
        column.Password = $("#txtGPassword").val();
        column.Surname = $("#txtGSurname").val();
        column.GivenNames = $("#txtGGivenNames").val();
        column.Salutation = $("#txtGSalutation").val();
        column.Name_Tm = $("#txtGName_Tm").val();
        column.Name_Cn = $("#txtGName_Cn").val();
        column.SeatNO = $("#txtGSeatNO").val();
        column.Floor = $("#txtGFloor").val();
        column.RoomNO = $("#txtGRoomNO").val();
        column.BuildName = $("#txtGBuildName").val();
        column.StreetNumber = $("#txtGStreetNumber").val();
        column.Street = $("#txtGStreet").val();
        column.HouseNO = $("#txtGHouseNO").val();
        column.City = $("#txtGCity").val();
        column.PostalCode = $("#txtGPostalCode").val();
        column.CountryID = $("#txtGCountryID").val();
        column.OfficeTel = $("#txtGOfficeTel").val();
        column.HomeTel = $("#txtGHomeTel").val();
        column.MobilePhone = $("#txtGMobilePhone").val();
        column.Email = $("#txtGEmail").val();
        column.PaymentWay = $("#txtGPaymentWay").val();
        column.PicPath1 = $("#txtGPicPath1").val();
        column.PicPath2 = $("#txtGPicPath2").val();
        column.Purpose1 = JudgeChecked($("#chkPurpose1"));
        column.Purpose2 = JudgeChecked($("#chkPurpose2"));
        column.Purpose3 = JudgeChecked($("#chkPurpose3"));
        column.Purpose4 = JudgeChecked($("#chkPurpose4"));
        column.Purpose5 = JudgeChecked($("#chkPurpose5"));
        column.Purpose6 = JudgeChecked($("#chkPurpose6"));
        column.Purpose7 = JudgeChecked($("#chkPurpose7"));
        column.Pathway1 = JudgeChecked($("#chkPathway1"));
        column.Pathway2 = JudgeChecked($("#chkPathway2"));
        column.Pathway3 = JudgeChecked($("#chkPathway3"));
        column.Pathway4 = JudgeChecked($("#chkPathway4"));
        column.Pathway5 = JudgeChecked($("#chkPathway5"));
        column.Pathway6 = JudgeChecked($("#chkPathway6"));
        rpt_member.push(column);
        //#endregion

        $("#formToSendEmail").ajaxSubmit({
            type: "post",
            url: "/Register/AddIndividual",
            dataType: "json",
            data: { memberJson: JSON.stringify(rpt_member) },
            success: function (json) {
                alert(json.message);
                self.persons.remove(obj);
            }
        });
    }
    //#endregion
    
    //#region 公司注册
    function EditCompany(self) {
        //#region 公司信息绑定
        var column = new Object();
        var rpt_Company = new Array();//公司信息
        var rpt_ContactPerson = new Array();//公司联系人信息
        var rpt_Member = new Array();//会员信息


        column.CompanyName_En = $("#txtCompanyName_En").val();
        column.CompanyName_Tm = $("#txtCompanyName_Tm").val();
        column.CompanyName_Cn = $("#txtCompanyName_Cn").val();
        column.BusinessType = $("#txtBusinessType").val();
        column.CIBRNO = $("#txtCIBRNO").val();
        column.SeatNO = $("#txtSeatNO").val();
        column.Floor = $("#txtFloor").val();
        column.RoomNO = $("#txtRoomNO").val();
        column.BuildName = $("#txtBuildName").val();
        column.StreetNumber = $("#txtStreetNumber").val();
        column.Street = $("#txtStreet").val();
        column.HouseNO = $("#txtHouseNO").val();
        column.City = $("#txtCity").val();
        column.PostalCode = $("#txtPostalCode").val();
        column.CountryID = $("#txtCountryID").val();
        column.PicPath1 = $("#txtPicPath1").val();
        column.PicPath2 = $("#txtPicPath2").val();
        rpt_Company.push(column);

        //公司联系人
        column = new Object();
        column.Surname = $("#txtSurname").val();
        column.GivenNames = $("#txtGivenNames").val();
        column.Salutation = $("#txtSalutation").val();
        column.Name_Tm = $("#txtName_Tm").val();
        column.Name_Cn = $("#txtName_Cn").val();
        column.Department = $("#txtDepartment").val();
        column.Position = $("#txtPosition").val();
        column.OfficeTel1 = $("#txtOfficeTel1").val();
        column.OfficeTel2 = $("#txtOfficeTel2").val();
        column.MobilePhone = $("#txtMobilePhone").val();
        column.Fax = $("#txtFax").val();
        column.Email = $("#txtEmail").val();
        column.CPPicPath1 = $("#txtCPPicPath1").val();
        column.CPPicPath2 = $("#txtCPPicPath2").val();
        rpt_ContactPerson.push(column);

        //会员信息
        column = new Object();
        column.MemberName = $("#txtMemberName").val();
        column.Password = $("#txtPassword").val();
        column.OfficeTel = $("#txtGOfficeTel").val();
        column.HomeTel = $("#txtGHomeTel").val();
        column.MobilePhone = $("#txtGMobilePhone").val();
        column.Email = $("#txtGEmail").val();
        column.PaymentWay = $("#txtGPaymentWay").val();
        column.Purpose1 = JudgeChecked($("#chkPurpose1"));
        column.Purpose2 = JudgeChecked($("#chkPurpose2"));
        column.Purpose3 = JudgeChecked($("#chkPurpose3"));
        column.Purpose4 = JudgeChecked($("#chkPurpose4"));
        column.Purpose5 = JudgeChecked($("#chkPurpose5"));
        column.Purpose6 = JudgeChecked($("#chkPurpose6"));
        column.Purpose7 = JudgeChecked($("#chkPurpose7"));
        column.Pathway1 = JudgeChecked($("#chkPathway1"));
        column.Pathway2 = JudgeChecked($("#chkPathway2"));
        column.Pathway3 = JudgeChecked($("#chkPathway3"));
        column.Pathway4 = JudgeChecked($("#chkPathway4"));
        column.Pathway5 = JudgeChecked($("#chkPathway5"));
        column.Pathway6 = JudgeChecked($("#chkPathway6"));
        rpt_Member.push(column);
        //#endregion

        

        $("#formToSendEmail").ajaxSubmit({
            type: "post",
            url: "/Register/AddCompany",
            dataType: "json",
            data: { memberJson: JSON.stringify(rpt_Member), companyJson: JSON.stringify(rpt_Company), contactPersonJson: JSON.stringify(rpt_ContactPerson) },
            success: function (json) {
                $.ajax({
                    type: 'POST',
                    url: 'http://192.168.0.101:7082/api/upload',
                    data: {
                        authorValue: "123", rootFlag: "0", modelDir: "Case"
                    },
                    //contentType: "multipart/form-data",
                    dataType: 'json',
                    success: function (results) {
                        alert($.getJSON("http://192.168.0.101:7082/api/upload?authorValue=123455&rootFlag=0&modelDir=Case", LoadCustomers));
                    }
                });
                //alert(json.message);
                //self.persons.remove(obj);
            }
        });
    }
    //#endregion


    $(document).on('change', '.myfile .uploathid', function () {
        $(this).parent().siblings().children(".txtfile").val($(this).val());
    });

    function JudgeChecked(str) {
        if (str.attr("checked")) {
            return 1;
        }
        else {
            return 0;
        }
    }
});