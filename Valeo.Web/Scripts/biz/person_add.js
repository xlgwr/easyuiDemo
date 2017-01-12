//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    self.PersonName = ko.observable();
    self.PersonAge = ko.observable();

    //【提交】按钮押下处理
    self.Commit = function () {
        $.ajax({
            type: "post",
            url: "/Person/Add",
            data: {
                PersonName: self.PersonName,
                PersonAge: self.PersonAge
            },
            success: function (json) {
                alert(json.result);
                window.location.href = '/Person/Index';
            }
        })
    };

    //【返回】按钮押下处理
    self.back = function () {
        window.location.href = '/Person/Index';
    };
}