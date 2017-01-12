 
//页面初始化
$(function(){
    var vm = new ViewModel();
    ko.applyBindings(vm);

    //分页控件加载处理
    $.jqPaginator('#personPagination', {
        totalPages: totalPage,
        visiblePages: 10,
        currentPage: currentPage,
        onPageChange: function (num, type) {
            if (type != 'init') {
                window.location.href = '/Person/Index?page=' + num;
            }
        }
    });
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //添加动态监视数组对象
    self.persons = ko.observableArray(data);

    //编辑
    self.edit = function(obj) {
        window.location.href = '/Person/Edit/' + obj.PersonID;
    };

    //删除
    self.remove = function(obj) {
        $.ajax({
            type: "post",
            url: "/Person/Del/" + obj.PersonID,
            dataType: "json",
            success: function(json) {
                alert(json.result);
                self.persons.remove(obj);
            }
        })
    }
};