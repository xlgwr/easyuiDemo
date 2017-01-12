
//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm);
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //self.categories = ko.mapping.fromJS(products);  
    //alert(ko.mapping.toJSON(data))

    if (data.type == 0) {
        layer.alert(data.message);
    }

    self.MemberName = data.MemberName;
    self.SelectName = data.SelectName;
    self.AddTime = data.AddTime;
    self.CompEmail = data.CompEmail;
    self.CompTel = data.CompTel;

    self.searchList = ko.mapping.fromJS(data.lstCaseReport);

    //导出
    self.export = function () {

        //window.location.href = '/CourtMintor/Download?json=' + ko.mapping.toJSON(data);

        var form = $("#frmConds");//定义一个form表单 
        $("#txtParam").val(ko.mapping.toJSON(data));
        form.submit();//表单提交 

        //$.ajax({
        //    type: "post",
        //    url: "/CourtMintor/Down",
        //    dataType: "json",
        //    data: { json: data },
        //    success: function (json) {
        //       layer.alert(json.result);
        //        self.persons.remove(obj);
        //    }
        //});
    };

};
