

function Save() {


    var SelectName = eval(document.getElementById("SelectName")).value;

    var obj = document.getElementById('SelectSalutation');
    var SelectSalutation = obj.options[obj.selectedIndex].text;//获取文本

    var SelectTel = eval(document.getElementById("SelectTel")).value;
    var SelectEmail = eval(document.getElementById("SelectEmail")).value;
    var SelectContent = eval(document.getElementById("SelectContent")).value;

    $.ajax({
        type: "post",
        url: "/OtherOffline/Save",
        data: { SelectName: SelectName, SelectSalutation: SelectSalutation, SelectTel: SelectTel, SelectEmail: SelectEmail, SelectContent: SelectContent },
        datatype: "json",
        success: function (data) {

            if (data.status = 1) {


                alert(data.message);
                //window.location.href = '/OtherOffline/Index';
                Clear(self);
            }
            else
            {

                alert(data.message);
            }
        }

    })

}


//页面初始化
$(function () {


   var vm = new ViewModel();
   ko.applyBindings(vm);    

});


//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //修改
    self.Save = function () {

       
        Save(self);
    };


    //重置
    self.Clear = function () {

        document.getElementById("SelectContent").innerHTML = "";
       

    };


};