//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    self.PersonID = ko.observable(data[0].PersonID);
    self.PersonName = ko.observable(data[0].PersonName);
    self.PersonAge = ko.observable(data[0].PersonAge);

    self.Commit = function () {
        $.ajax({
            type: "post",
            url: "/Person/Edit",
            data: { PersonID: self.PersonID, PersonName: self.PersonName, PersonAge: self.PersonAge },
            success: function (json) {
                alert(json.result);
                window.location.href = '/Person/Index';
            }
        });
    };

    self.back = function () {
        window.location.href = '/Person/Index';
    };
};