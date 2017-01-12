 
//页面初始化
$(function () {
      
    var vm = new ViewModel();
    ko.applyBindings(vm); 
});
 
//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    self.categories = ko.mapping.fromJS(products); 
    //alert(ko.mapping.toJSON(data))

    self.searchList = ko.mapping.fromJS(data);
    //$("#tblDataSearch tr").remove();

    //追加
    self.add = function (obj) {

        if (getAddInputCheck() == false) return false;

        var rowIndex = self.searchList().length + 1;
        var orderListID = $("input[name='rbtnProduct']:checked").val();
        var productName = $("input[name='rbtnProduct']:checked").siblings("span").html();

        if (orderListID != null) {
             
            //console.log(self.searchList());
            self.searchList.push({ 
                D_PerName_En: $("#txtPPerNameEn").val().trim(),
                D_PerName_Cn: $("#txtPPerNameCn").val().trim(),

                P_PerName_En: $("#txtDPerNameEn").val().trim(),
                P_PerName_Cn: $("#txtDPerNameCn").val().trim(),

                Case_No: $("#txtCaseNo").val().trim(),
                Representation: $("#txtRepresentation").val(),

                OrderListID: orderListID,
                ProductName: productName,

                Remark: $("#txtRemark").val(),
            });
        }
        //console.log(self.searchList());  
    };

    //批量上传
    self.import = function () {

    };

    //重置
    self.reset = function () {
        
        $("#txtSelectName").attr("value", "");

        $("#txtPPerNameEn").attr("value", "");
        $("#txtPPerNameEn").attr("value", "");

        $("#txtPPerNameCn").attr("value", "");
        $("#txtDPerNameEn").attr("value", "");
 
        $("#txtCaseNo").attr("value", "");
        $("#txtRepresentation").attr("value", "");
    };

    //提交
    self.submit = function () {

        if (getSubmitInputCheck(self) == false) return false;

        $.ajax({
            type: "post",
            url: "/CourtMintor/SearchSubmit",
            data: {
                json: ko.mapping.toJSON(self.searchList()),
                selectName: $("#txtSelectName").val()
            },
            success: function (json) {
                if (json.type == 1) {

                    window.location.href = '/CourtMintor/ReportList?id=' + json.id;

                    //$.ajax({
                    //    type: "post",
                    //    url: "/Pay/Index",
                    //    async: false,
                    //    data: {
                    //        flag: 0,
                    //        orderJson: ko.mapping.toJSON(json.orderJson), 
                    //    },
                    //    success: function (json) {
                     
 
                    //    }
                    //});
                }
                else {
                    alert(json.message)
                }
            }
        })
    };

    //删除
    self.remove = function (obj) {
        self.searchList.remove(obj); 
    }
 
}; 
 