
//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm);
    
});


//修改
function Edit(self) {

    
    
    var oldPsw = eval(document.getElementById('oldPsw')).value;
    var newPsw1 = eval(document.getElementById('newPsw1')).value;
    var newPsw2 = eval(document.getElementById('newPsw2')).value;

    $.ajax({
        type: "post",
        url: "/Member/Edit",
        data: { memberID: MemberId, oldPsw: oldPsw, newPsw1: newPsw1, newPsw2: newPsw2 },
        dataType: "json",
        success: function (json) {
            if (json.status == 1) {
                //成功
                alert(json.message);
                window.location.href = '/Member/Index';
            }
            else
            {
               
                //失败
                alert(json.message);
            }
           
            
        }
    })
   
   
}


//定义ViewModel对象
var ViewModel = function () {
    var self = this;
   
    //修改
    self.Edit = function () {

        Edit(self);
    };


    //重置
    self.Clear = function () {

        window.location.href = '/Member/Index';

    };


};