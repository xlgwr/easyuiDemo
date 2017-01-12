
//页面初始化
$(function () {


    debugger;
    alert(sss);
    var sss = $("#txtApplyType").combobox('getValue');
    $("#tabs").tabs("select", parseInt(sss));

    $("#txtApplyType").combobox({
        onChange: function (n, o) {

            var val = $("#txtApplyType").combobox('getValue');
            //var map = { "0": "tab1", "1": "tab2", "2": "tab2", "信贷": "tab5", "3": "tab4" };
            //var NewdivId = map[val];
            $("#tabs").tabs("select", parseInt(val));
            //document.getElementById(divId).style.display = 'none';
            //document.getElementById(NewdivId).style.display = 'block';
            //divId = NewdivId;

        }
    });


});