


//检索
function Filter(self) {


    var fromdate = $('#fromdate .form-control').val();  //筛选条件1  
    var todate = $('#todate .form-control').val();  //筛选条件2  
    var SchMemberNm = $("#SchMemberNm").val();   //筛选条件1  
    var SchContent = $("#SchContent").val();   //筛选条件2  
    var ProductName = $("#ProductName").find("option:selected").text();  //筛选条件1  
    var SchStatus = $("#cbxSchStatus").val(); //筛选条件2  


    $.ajax({
        type: "post",
        dataType: "json",
        url: '/SearchHistory/GetList', //回发到的页面   
        data: {
            page: model.pageIndex, pageSize: model.pageSize, fromdate: fromdate, todate: todate,
            SchMemberNm: SchMemberNm, SchContent: SchContent, ProductName: ProductName, SchStatus: SchStatus
        },

       
        success: function (json) {


            if (json.status == 1) {

                $("#tblData tr").remove();   //清空列表  

                //添加动态监视数组对象
                self.mesages(json.rows);

                model.pageIndex = json.pageIndex;
                model.totalCount = json.totalCount;

                if (json.totalCount == 0) {
                    json.totalCount = 1;
                }

                /*生成分页标签*/
                paginator(self, model.pageIndex, model.pageSize, model.totalCount);
            }
            else
            {
                //失败
                alert(json.message);
            }
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

    var vm = new ViewModel();
    ko.applyBindings(vm);
});



//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //添加动态监视数组对象
    self.mesages = ko.observableArray(data);

    //检索
    self.query = function () {

        Filter(self);;

    };

    //重置
    self.Clear = function () {

        window.location.href = '/SearchHistory/Index';

    };


};