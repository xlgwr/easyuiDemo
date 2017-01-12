

//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);
});

function KEY() {
    var txtKey = "";
    if ($.trim($("#txtD_PerName_En").val()) != "") {
        txtKey = 'DEN'
        return txtKey;
    }
    else if ($.trim($("#txtD_PerName_Cn").val()) != "") {
        txtKey = 'DCN'
        return txtKey;
    }
    else if ($.trim($("#txtP_PerName_En").val()) != "") {
        txtKey = 'PEN'
        return txtKey;
    }
    else if ($.trim($("#txtP_PerName_Cn").val()) != "") {
        txtKey = 'PCN'
        return txtKey;
    }
    else if ($.trim($("#txtCase_No").val()) != "") {
        txtKey = 'CNO'
        return txtKey;
    }
    else if ($.trim($("#txtRepresentation").val()) != "") {
        txtKey = 'RPT'
        return txtKey;
    }
    else if ($.trim($("#txtStreet").val()) != "" || $.trim($("#txtBuildName").val()) != "") {
        txtKey = 'ADD1'
        return txtKey;
    }
    else if ($.trim($("#txtLotType").val()) != "" || $.trim($("#txtLotNo").val()) != "") {
        txtKey = 'ADD2'
        return txtKey;
    }
}

//定义ViewModel对象
var ViewModel = function () {


    var self = this;

    //编辑
    self.GET = function () {

        
        var txtKey = KEY();
        var column = new Object();
        var rpt_column = new Array();
        column.AgeLimit = $.trim($("input[name='inAgeLimit'][checked]").val());
        column.txtKey = txtKey;
        column.CourtType = $.trim($("input[name='inCourtType'][checked]").val());
        column.D_PerName_En = $.trim($("#txtD_PerName_En").val());
        column.D_PerName_Cn = $.trim($("#txtD_PerName_Cn").val());
        column.P_PerName_En = $.trim($("#txtP_PerName_En").val());
        column.P_PerName_Cn = $.trim($("#txtP_PerName_Cn").val());
        column.Case_No = $.trim($("#txtCase_No").val());
        column.Representation = $.trim($("#txtRepresentation").val());
        column.Street = $.trim($("#txtStreet").val());
        column.BuildName = $.trim($("#txtBuildName").val());
        column.LotType = $.trim($("#txtLotType").val());
        column.LotNo = $.trim($("#txtLotNo").val());
        column.Remark = $.trim($("#txtRemark").val());
        column.SelectName = $.trim($("#txtSelectName").val());

        if ($.trim($("#txtD_PerName_En").val()) != "" || $.trim($("#txtD_PerName_Cn").val()) != "" ||
            $.trim($("#txtP_PerName_En").val()) != "" || $.trim($("#txtP_PerName_Cn").val()) != "") {
            $.ajax({
                type: "post",
                dataType: "json",
                url: '/CourtOnline/GetList', //回发到的页面   
                data: {
                    DName_en: $.trim($("#txtD_PerName_En").val()),
                    DName_cn: $.trim($("#txtD_PerName_Cn").val()),
                    PName_en: $.trim($("#txtP_PerName_En").val()),
                    PName_cn: $.trim($("#txtP_PerName_Cn").val()),
                    Representation: "",
                },
                success: function (data) {
                    var html = "";
                    for (var i = 0; i < data.rows.length; i++) {
                        html += "<tr>"
                        html += "<td><a>zhang san </a><span style='display: none'>" + data.rows[i].Entityid + "</span></td>"
                        html += "</tr>";
                    }
                    $(".table").html(html);
                }
            });
            $('#tabm td').live("click", function () {

                var Entityid = $.trim($(this).children("span").text());
                column.Entityid = Entityid;
                rpt_column.push(column);

                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: '/CourtOnline/Create',
                    data: { strJson: JSON.stringify(rpt_column), txtKEY: txtKey },
                    success: function (data) {
                        if (data.type == 1) {
                            var html = "/CourtOnline2/Index?txtKEY:" + txtKey + "&Entityid:" + Entityid + "&TaskID:" + data.message;
                            window.location.href = html;
                        }
                    }
                });
            });
        }
        else if ($.trim($("#txtCase_No").val()) != "") {
            rpt_column.push(column);

            $.ajax({
                type: "post",
                dataType: "json",
                url: '/CourtOnline/Create',
                data: { strJson: JSON.stringify(rpt_column), txtKEY: txtKey },
                success: function (data) {
                    if (data.type == 1) {
                        var html = "/CourtOnline2/Index?txtKEY:" + txtKey + "&Case_No:" + $.trim($("#txtCase_No").val()) + "&TaskID:" + data.message;
                        window.location.href = html;
                    }
                }
            });
        }
        else if ($.trim($("#txtRepresentation").val()) != "") {
            var Caseid = "0";
            $.ajax({
                type: "post",
                dataType: "json",
                url: '/CourtOnline/GetList', //回发到的页面   
                data: {
                    DName_en: "",
                    DName_cn: "",
                    PName_en: "",
                    PName_cn: "",
                    Representation: $.trim($("#txtRepresentation").val()),
                },
                success: function (data) {
                    if (data.RepresentationD.length > 0) {
                        Caseid = data.RepresentationD[0].Caseid
                    }
                    else {
                        Caseid = data.RepresentationP[0].Caseid
                    }
                    column.Caseid = Caseid;
                    rpt_column.push(column);
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        url: '/CourtOnline/Create',
                        data: { strJson: JSON.stringify(rpt_column), txtKEY: txtKey },
                        success: function (data) {
                            console.log(data);
                            if (data.type == 1) {
                                var html = "/CourtOnline2/Index?txtKEY:" + txtKey + "&Caseid:" + Caseid + "&TaskID:" + data.message;
                                window.location.href = html;
                            }
                        }
                    });
                }
            });
            
           
        }
        else if ($.trim($("#txtStreet").val()) != "" || $.trim($("#txtBuildName").val()) != ""||
            $.trim($("#txtLotType").val()) != "" || $.trim($("#txtLotNo").val()) != "") {
            var Entityid = "0";
            $.ajax({
                type: "post",
                dataType: "json",
                url: '/CourtOnline/GetAddList', //回发到的页面   
                data: {
                    Street: $.trim($("#txtStreet").val()),
                    BuildName: $.trim($("#txtBuildName").val()),
                    LotType: $.trim($("#txtLotType").val()),
                    LotNo: $.trim($("#txtLotNo").val())
                },
                success: function (data) {
                    Entityid = data.rows[0].Entityid
                    column.Entityid = Entityid;
                    rpt_column.push(column);
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        url: '/CourtOnline/Create',
                        data: { strJson: JSON.stringify(rpt_column), txtKEY: txtKey },
                        success: function (data) {
                            console.log(data);
                            if (data.type == 1) {
                                var html = "/CourtOnline2/Index?txtKEY:" + txtKey + "&Entityid:" + Entityid + "&TaskID:" + data.message;
                                window.location.href = html;
                            }
                        }
                    });
                }
            });
        }
    };
};