function getContent() {
    var content = encodeURI(UM.getEditor('myEditor').getContent());
    FuncPrent = $("#PageNameId").combo("getValue");
    LangKeyValue = $("#LangTypeId").combo("getValue");
    $.ajax({
        url: '/WebEdit/AddWebData', //回发到的页面    
        type: "post",
        cache: false,
        async: false,
        data: {
            FuncPrentId: FuncPrent,
            LangKey: LangKeyValue,
            HtmlContent: content
        },
        dataType: "json",
        success: function (data) {
             
            if (data.type == 1) {
                $.messager.alert('提示信息 ', data.message);
            }
        }
    });

}
$(function () {
    $('#PageNameId').combobox({
        onChange: function () {
          
            FuncPrent = $("#PageNameId").combo("getValue");
            LangKeyValue = $("#LangTypeId").combo("getValue");
            var pars = {
                FuncPrentId: FuncPrent,
                LangKey: LangKeyValue
            };
            loadData(pars)
        }
    });

    $('#LangTypeId').combobox({
        onChange: function () {
            FuncPrent = $("#PageNameId").combo("getValue");
            LangKeyValue = $("#LangTypeId").combo("getValue");
            var pars = {
                FuncPrentId: FuncPrent,
                LangKey: LangKeyValue
            };
            loadData(pars)
        }
    });
});

function loadData(pars) {
    $.ajax({
        url: '/WebEdit/GetHtmlData', //回发到的页面    
        type: "post",
        cache: false,
        async: false,
        data: pars,
        dataType: "json",
        success: function (data) {
            if (data.type == 1) {
                (UM.getEditor('myEditor').setContent(data.content));
            }
        }
    });
}

