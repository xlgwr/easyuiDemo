


//检索
function Filter(self) {

    var fromdate = $('#fromdate .form-control').val();  //筛选条件1  
    var todate = $('#todate .form-control').val();  //筛选条件2  
    var prdctNm = $("#prdctNm").find("option:selected").text();
    var paymentWay = $("#cbxpaymentWay").val();
    var payStatus = $("#cbxpayStatus").val();
    var orderStatus = $("#cbxorderStatus").val();

    $.ajax({
        type: "post",
        dataType: "json",
        url: '/Order/GetList', //回发到的页面   
        data: {
            page: model.pageIndex, pageSize: model.pageSize, fromdate: fromdate, todate: todate,
            prdctNm: prdctNm, paymentWay: paymentWay, payStatus: payStatus, orderStatus: orderStatus
        },
        success: function (data) {

            ///查询成功
            if (data.status == 1) {
                $("#tblData tr").remove();   //清空列表  
                //添加动态监视数组对象
                self.mesages(data.rows);
                model.pageIndex = data.pageIndex;
                model.totalCount = data.totalCount;



                if (model.totalCount == 0) {
                    model.totalCount = 1;
                }

         

                /*生成分页标签*/
                paginator(self, model.pageIndex, model.pageSize, model.totalCount);


                //将【超出费用】为0的标记为粉红色
                var arrData = new Array();
                var objTable = document.getElementById("tblData");
                var data = new String();

                if (objTable) {

                    for (var i = 0; i < objTable.rows.length; i++) {
                        for (var j = 0; j < objTable.rows[i].cells.length; j++) {

                           
                            if (objTable.rows[i].cells[j].id == "Operation") {

                                data = objTable.rows[i].cells[j].innerText;

                               
                                if (data != "付款") {

                                    objTable.rows[i].cells[j].style.visibility = "hidden";

                                    //$("#Operation").style.visibility = "hidden";
                                    //objTable.rows[i].cells[j].innerText = null;

                                }

                            }
                        }
                    }
                }
            }
            else
            {
                alert(data.mesage);
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

    //检索
    self.Clear = function () {

        window.location.href = '/Order/Index';

    };

    //检索
    self.fukuan = function (obj) {

        var row = obj;
        var flag = '1';
     
        window.location.href = '/Pay/Index?flag=' + flag + '&id=' + row.OrderID;

    };

};