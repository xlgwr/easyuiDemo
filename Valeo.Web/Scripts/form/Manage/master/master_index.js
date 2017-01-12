

//页面初始化
$(function () {
    $("ul.dropdown-menu").on("click", "[data-stopPropagation]", function (e) {
        e.stopPropagation();
    });
    $('#Tab a:first').tab('show'); //初始化显示哪个tab 
    $('#Tab a').click(function (e) {
        e.preventDefault();  //阻止a链接的跳转行为 
        $(this).tab('show'); //显示当前选中的链接及关联的content 
    });

    $(document).on('change', '.myfile .uploathid', function () {
        $(this).parent().siblings().children(".txtfile").val($(this).val());
    });


    //会员信息回显
    $.ajax({
        type: "post",
        dataType: "json",
        url: '/Master/GetList', //回发到的页面   
        success: function (data) {
            var datavar = data.rows[0];
            if (datavar["Type"] == 1)
            {
                $("#liIndividual").hide();
                $("#liCompany").show();
                $("#individual").hide();
                $("#company").show();
                $("#hidMemberComanyID").val(datavar["MemberComanyID"]);

                $("#txtCompanyName_En").val(datavar["CompanyName_En"]);
                $("#txtCompanyName_Tm").val(datavar["CompanyName_Tm"]);
                $("#txtCompanyName_Cn").val(datavar["CompanyName_Cn"]);
                $("#txtCompanyName_En").val(datavar["CompanyName_En"]);
                $("#txtBusinessType").val(datavar["BusinessType"]);
                $("#txtCIBRNO").val(datavar["CIBRNO"]);
                $("#txtSeatNO").val(datavar["SeatNO"]);
                $("#txtFloor").val(datavar["Floor"]);
                $("#txtRoomNO").val(datavar["RoomNO"]);
                $("#txtBuildName").val(datavar["BuildName"]);
                $("#txtStreetNumber").val(datavar["StreetNumber"]);
                $("#txtStreet").val(datavar["Street"]);
                $("#txtHouseNO").val(datavar["HouseNO"]);
                $("#txtCity").val(datavar["City"]);
                $("#txtPostalCode").val(datavar["PostalCode"]);
                $("#txtCountryID").val(datavar["CountryID"]);
                $("#txtSurname").val(datavar["Surname"]);
                $("#txtGivenNames").val(datavar["GivenNames"]);
                $("#txtSalutation").val(datavar["Salutation"]);
                $("#txtName_Tm").val(datavar["Name_Tm"]);
                $("#txtName_Cn").val(datavar["Name_Cn"]);
                $("#txtDepartment").val(datavar["Department"]);
                $("#txtPosition").val(datavar["Position"]);
                $("#txtOfficeTel1").val(datavar["OfficeTel1"]);
                $("#txtOfficeTel2").val(datavar["OfficeTel2"]);
                $("#txtMobilePhone").val(datavar["MobilePhone"]);
                $("#txtFax").val(datavar["Fax"]);
                $("#txtEmail").val(datavar["Email"]);
                $("#txtPicPath1").val(datavar["CPPicPath1"]);
                $("#txtPicPath2").val(datavar["CPPicPath2"]);
            }
            else if (datavar["Type"] == 0)
            {
                $("#liCompany").hide();
                $("#liIndividual").show();
                $("#company").hide();
                $("#individual").show();

                $("#txtGSurname").val(datavar["GSurname"]);
                $("#txtGGivenNames").val(datavar["GGivenNames"]);
                $("#txtGSalutation").val(datavar["GSalutation"]);
                $("#txtGName_Tm").val(datavar["GName_Tm"]);
                $("#txtGName_Cn").val(datavar["GName_Cn"]);
                $("#txtGSeatNO").val(datavar["GSeatNO"]);
                $("#txtGFloor").val(datavar["GFloor"]);
                $("#txtGRoomNO").val(datavar["GRoomNO"]);
                $("#txtGBuildName").val(datavar["GBuildName"]);
                $("#txtGStreetNumber").val(datavar["GStreetNumber"]);
                $("#txtGStreet").val(datavar["GStreet"]);
                $("#txtGHouseNO").val(datavar["GHouseNO"]);
                $("#txtGCity").val(datavar["GCity"]);
                $("#txtGPostalCode").val(datavar["GPostalCode"]);
                $("#txtGCountryID").val(datavar["GCountryID"]);
                $("#txtGHomeTel").val(datavar["GHomeTel"]);
                $("#txtGOfficeTel").val(datavar["GOfficeTel"]);
                $("#txtGMobilePhone").val(datavar["GMobilePhone"]);
                $("#txtGEmail").val(datavar["GEmail"]);
                $("#txtGPaymentWay").val(datavar["GPaymentWay"]);
                $("#txtGPicPath1").val(datavar["GPicPath1"]);
                $("#txtGPicPath2").val(datavar["GPicPath2"]);
            }
        }
    });
    var vm = new ViewModel();
    ko.applyBindings(vm);
});

//#region 个人注册修改
function EditIndividual(self) {
    //#region 个人信息绑定
    var column = new Object();
    var rpt_member = new Array();
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
    rpt_member.push(column);
    //#endregion
    $("#formToSendEmail").ajaxSubmit({
        type: "post",
        url: "/Master/EditIndividual",
        dataType: "json",
        data: { memberJson: JSON.stringify(rpt_member) },
        success: function (json) {
            alert(json.result);
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

    column.MemberComanyID = $("#hidMemberComanyID").val();
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
    column.PicPath1 = $("#txtCPPicPath1").val();
    column.PicPath2 = $("#txtCPPicPath1").val();
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
    column.PicPath1 = $("#txtCPPicPath1").val();
    column.PicPath2 = $("#txtCPPicPath1").val();
    rpt_ContactPerson.push(column);

    //会员信息
    column = new Object();
    column.PaymentWay = $("#txtGPaymentWay").val();
    rpt_Member.push(column);
    //#endregion

    $("#formToSendEmail").ajaxSubmit({
        type: "post",
        url: "/Master/EditCompany",
        dataType: "json",
        data: { memberJson: JSON.stringify(rpt_Member), companyJson: JSON.stringify(rpt_Company), contactPersonJson: JSON.stringify(rpt_ContactPerson) },
        success: function (json) {
            alert(json.result);
            self.persons.remove(obj);
        }
    });
}
//#endregion

function JudgeChecked(str) {
    if (str.attr("checked")) {
        return 1;
    }
    else {
        return 0;
    }
}

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //编辑
    self.Edit = function () {
        if ($("#company").is(":visible")) {
            EditCompany(self);
        }
        if ($("#individual").is(":visible")) {

            EditIndividual(self)
        }
    };
};