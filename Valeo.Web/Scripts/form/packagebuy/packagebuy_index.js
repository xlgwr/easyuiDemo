 
//检索
function Filter(self) {

    var curr = $("input[name='rbtnCurrency']:checked").val();
    if (curr == null) return false;

    //alert(currency + '|' + curr)
    if (currency == curr) return;

    currency = curr;

    $.ajax({
        type: "post",
        dataType: "json",
        url: '/PackageBuy/GetList', //回发到的页面   
        data: { currency: currency },
        success: function (data) {
             
            //清空列表 
            document.getElementById('tblCourt').style.display = 'none'
            document.getElementById('tblLand').style.display = 'none'
            document.getElementById('tblCompany').style.display = 'none'
            document.getElementById('tblCredit').style.display = 'none'
            document.getElementById('tblCommon').style.display = 'none'
            document.getElementById('tblPackageData').hidden = 'hidden'

            $("#tblDataCourt tr").remove();
            $("#tblDataLand tr").remove();
            $("#tblDataCompany tr").remove();
            $("#tblDataCredit tr").remove();
            $("#tblDataComon tr").remove();

            //$("#tblPackageData").empty();
          
            //添加动态监视数组对象 
            self.CourtList(ko.mapping.fromJS(data.products.CourtList)());
            self.LandList(ko.mapping.fromJS(data.products.LandList)());
            self.CompanyList(ko.mapping.fromJS(data.products.CompanyList)());
            self.CreditList(ko.mapping.fromJS(data.products.CreditList)());
            self.ComonList(ko.mapping.fromJS(data.products.ComonList)());

            self.packages(ko.mapping.fromJS(data.packages)());
 
            if (self.CourtList().length > 0) document.getElementById('tblCourt').style.display = '';
            if (self.LandList().length > 0) document.getElementById('tblLand').style.display = '';
            if (self.CompanyList().length > 0) document.getElementById('tblCompany').style.display = '';
            if (self.CreditList().length > 0) document.getElementById('tblCredit').style.display = '';
            if (self.ComonList().length > 0) document.getElementById('tblCommon').style.display = '';

            if (self.packages().length > 0) document.getElementById('tblPackageData').hidden = '';

            //$.each(data.rows, function (index, model) {
            //    $("#tblData").append('<tr><td>' + model.SendTimeCaption + '</td><td>'
            //                            + model.MessageTitle + '</td><td>'
            //                            + model.Message + '</td></tr>');  //将返回的数据追加到表格  
            //});  

     
            refreshPackageBy(self); 
        }
    });

    return true; 
}

function refreshPackageBy(self) {

    $("#tblPackageBuy tr").remove(); 
    self.packageBuyList(ko.mapping.fromJS([])());
    TotalAmountValue = 0;
   
    //重新计算金额
    ProuctBuyAdd(self, self.CourtList());
    ProuctBuyAdd(self, self.LandList());
    ProuctBuyAdd(self, self.CompanyList());
    ProuctBuyAdd(self, self.CreditList());
    ProuctBuyAdd(self, self.ComonList());
  
   PackageBuyAdd(self, self.packages()); 
   document.getElementById("spanTotalAmount").innerText = currency + ' ' + TotalAmountValue; 
}


function ProuctBuyAdd(self, prouctList) {
 
    for (var i = 0; i < prouctList.length; i++) {

        if (prouctList[i].IsChecked() == true) {
            self.packageBuyList.push({
                PackageID: '',
                ProductID: prouctList[i].ProductID(),
                PackageProuctName: prouctList[i].ProductName(),
                UnitsBuy: prouctList[i].UnitsBuy(),
                TotalAmount: prouctList[i].TotalAmount(),
            });

            TotalAmountValue += prouctList[i].TotalAmount();
        }
    }
}
 
function PackageBuyAdd(self, prouctList) {
      
    for (var i = 0; i < prouctList.length; i++) {

        if (prouctList[i].IsChecked() == true) {
            
            self.packageBuyList.push({
                PackageID: prouctList[i].PackageID(),
                ProductID: '',
                PackageProuctName: prouctList[i].PackageName(),
                UnitsBuy: '',
                TotalAmount: prouctList[i].DiscountTotal(),
            });

            TotalAmountValue += prouctList[i].DiscountTotal();
        }
    }
}


//页面初始化
$(function () {

    var vm = new ViewModel();
    ko.applyBindings(vm); 
    $("input[name='rbtnCurrency']").removeAttr("checked");

    return Filter(self);

});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;
  
    //添加动态监视数组对象   
    self.CourtList = ko.mapping.fromJS(data);
    self.LandList = ko.mapping.fromJS(data);
    self.CompanyList = ko.mapping.fromJS(data);
    self.CreditList = ko.mapping.fromJS(data);
    self.ComonList = ko.mapping.fromJS(data);

    document.getElementById('tblCourt').style.display = 'none'
    document.getElementById('tblLand').style.display = 'none'
    document.getElementById('tblCompany').style.display = 'none'
    document.getElementById('tblCredit').style.display = 'none'
    document.getElementById('tblCommon').style.display = 'none'

    document.getElementById('tblPackageData').hidden = 'hidden'
    
    self.packages = ko.mapping.fromJS(data);
   
    self.packageBuyList = ko.mapping.fromJS(data); 

    //查询
    self.query = function () {
         
        return Filter(self);
    };
  
  
    self.showTotalAmount = function (obj) {

        var price = obj.Price();
        var unitsBuy = obj.UnitsBuy(); 
        if (unitsBuy == null) unitsBuy = 0;
        
        var totalAmount = parseInt(price) * parseInt(unitsBuy);
     
        if (obj.IsChecked()) {

            //刷新表格购买数据
            refreshPackageBy(self);
        }

        obj.TotalAmount(totalAmount);
        return totalAmount;
    };

    //产品选择
    self.checkProduct = function (obj) {
   
        //刷新表格购买数据
        refreshPackageBy(self); 
    }
   
    //【在线支付】按钮押下处理
    self.onlinePay = function () {

        //刷新表格购买数据
        refreshPackageBy(self);

        if (getSubmitInputCheck(self) == false) return;

        $.ajax({
            type: "post",
            url: "/PackageBuy/OrderPay",
            data: {
                json: ko.mapping.toJSON(self.packageBuyList()),
                currency: currency,
                total: TotalAmountValue,
            },
            success: function (json) {
                if (json.type == 1) {

                    window.location.href = '/Pay/Index?flag=0&id=' + json.id;

                    //$.ajax({
                    //    type: "post",
                    //    url: "/Pay/Index",
                    //    async: false,
                    //    data: {
                    //        flag: 0,
                    //        orderJson: ko.mapping.toJSON(json.orderJson), 
                    //    },
                    //    success: function (json) {




                    //    }
                    //});
                }
                else {
                    alert(json.message)
                }
            }
        })
    };

    //【离线支付】按钮押下处理
    self.offlinePay = function () {

        //刷新表格购买数据
        refreshPackageBy(self);

        if (getSubmitInputCheck(self) == false) return;

        $.ajax({
            type: "post",
            url: "/PackageBuy/OrderPay",
            data: {
                json: ko.mapping.toJSON(self.packageBuyList()),
                currency: currency,
                total: TotalAmountValue,
            },
            success: function (json) { 
                if (json.type == 1) {
                     
                    window.location.href = '/Order/Index?id=' + json.id;

                    //$.ajax({
                    //    type: "post",
                    //    url: "/Order/GetOrder",
                    //    data: { 
                    //        orderJson: ko.mapping.toJSON(json.orderJson),
                    //    },
                    //    success: function (json) {


                    //    }
                    //}); 
                }
                else
                {
                    alert(json.message)
                }
            }
        })
    };
}; 
 