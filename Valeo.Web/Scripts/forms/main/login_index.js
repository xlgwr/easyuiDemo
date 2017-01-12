 
//页面初始化
$(function () {
 
    $("#btnLogin").click(function () {
      
        if (getSubmitInputCheck() == false) return;

        var userName = $("#txtUserId").val().replace(/^\s*|\s*$/g, "");
        var password = $("#txtLoginPwd").val().replace(/^\s*|\s*$/g, "");

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
                    $.messager.alert('提示信息', data.message);
                }
            }
        })

    });

    $("#btnForgot").click(function () {
 
        var userName = $("#txtUserId").val().replace(/^\s*|\s*$/g, "");
        
        //if (defV != undefined) dlg.find(".messager-input").val(userName);
        $.messager.prompt('提示信息', '请输入用户名：', function (userName) {

            if (userName) {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: '/Login/Forget', //回发到的页面   
                    data: {
                        userName: userName
                    },
                    success: function (data) {
                        if (data.type == 1) {
                            $.messager.alert('提示信息 ', data.message);
                        }
                        else {
                            $.messager.alert('提示信息 ', data.message);
                        }
                    }
                })
            }
        },userName);
    });
});

 
 