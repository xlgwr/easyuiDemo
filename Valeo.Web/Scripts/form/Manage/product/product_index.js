 
//页面初始化
$(function () {

    /*生成分页标签*/
    paginator(self, model.pageIndex, model.pageSize, model.totalCount);


    
   


    var vm = new ViewModel();
    ko.applyBindings(vm);

   
    //将【超出费用】为0的标记为粉红色
    var arrData = new Array();
    var objTable = document.getElementById("tblData");
    var data = new String();
    if (objTable) {
        for (var i = 0; i < objTable.rows.length; i++) {
            for (var j = 0; j < objTable.rows[i].cells.length; j++) {


                if (objTable.rows[i].cells[j].id == "OverMoney") {

                    data = objTable.rows[i].cells[j].innerText;
                   
                  
                    if (data == "0") {
                        
                        objTable.rows[i].cells[j].style.backgroundColor = ' #FFC0CB';
                        
                    }
                  
                }
            }
        }
    }

            
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //添加动态监视数组对象
    self.mesages = ko.observableArray(data);

};