 
//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm); 
 
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;
     
    //【登录】按钮押下处理
    self.login = function () {
 
        if (getSubmitInputCheck(self) == false) return;

        userName = $("#txtUserId").val().trim();
        password = $("#txtLoginPwd").val().trim();
        
        $.ajax({
            type: "post",
            dataType: "json",
            url: '/Login/login', //回发到的页面   
            data: {
                userName: userName,
                password: password
            },
            success: function (data) {
                if (data.type == 1) {
 

                    window.location.href = '/Home/Index';
 
                }
                else {
                    alert(data.message)
                }
            }
        })
    };
   
}; 
 