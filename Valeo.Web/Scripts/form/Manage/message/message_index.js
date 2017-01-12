 
//检索
function Filter(self) {

    var fromdate = $('#fromdate .form-control').val();  //筛选条件1  
    var todate = $('#todate .form-control').val();  //筛选条件2  

    $.ajax({
        type: "post",
        dataType: "json",
        url: '/Message/GetList', //回发到的页面   
        data: { page: model.pageIndex, pageSize: model.pageSize, fromdate:fromdate, todate:todate},
        success: function (data) {

            $("#tblData tr").remove();   //清空列表  

            //添加动态监视数组对象
            self.mesages(data.rows);

            //$.each(data.rows, function (index, model) {
            //    $("#tblData").append('<tr><td>' + model.SendTimeCaption + '</td><td>'
            //                            + model.MessageTitle + '</td><td>'
            //                            + model.Message + '</td></tr>');  //将返回的数据追加到表格  
            //}); 

            model.pageIndex = data.pageIndex;
            model.totalCount = data.totalCount;

            if (model.totalCount == 0) {
                model.totalCount = 1;
            }
            /*生成分页标签*/
            paginator(self, model.pageIndex, model.pageSize, model.totalCount);
        }
    });
}

 
//页面初始化
$(function () {
  

    $('#fromdate').datetimepicker({
        useCurrent: false,
        format: "YYYY-MM-DD",
        minDate: '1900-01-01' 
    });
      
    //$("#fromdate").on("dp.change", function (e) {

    //    $('#fromdate').data("DateTimePicker").minDate(e.date);
    //});

    $('#todate').datetimepicker({
        useCurrent: false, 
        format: "YYYY-MM-DD",
        minDate: '1900-01-01'
    });

    //$("#todate").on("dp.change", function (e) {
    //    $('#todate').data("DateTimePicker").minDate(e.date);
    //}); 

    var vm = new ViewModel();
    ko.applyBindings(vm);
});
 
//定义ViewModel对象
var ViewModel = function () {
    var self = this;
     
    //添加动态监视数组对象
    self.mesages = ko.observableArray(data);
 
    //查询
    self.query = function () { 
        Filter(self); 
    };

    //清空
    self.clear = function (obj) {
         
        window.location.href = '/Message/Index';
    };

    //编辑
    self.edit = function(obj) {
        window.location.href = '/Message/Edit?MessageID=' + 2;
    };

    //删除
    self.remove = function(obj) {
        $.ajax({
            type: "post",
            url: "/Message/Delete?MessageID=" + '2',
            dataType: "json",
            success: function(json) {
                alert(json.result);
                self.persons.remove(obj);
            }
        })
    }
};