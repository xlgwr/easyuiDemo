var getDataHtml = {
    Data: undefined,
    initFun: undefined,
    dataDisplay: function (dataresult, msgid) {
        var _curr = getDataHtml;
        if (msgid && dataresult) {

            console.log(msgid);
            $(msgid).html('');
            $(msgid).append('<h2>土 地 登 記 冊 LAND REGISTER</h2>');
            $(msgid).append('<p> <strong>SEARCH DATE AND TIME:</strong>' + dataresult.SearchTime + '</p>');
            $(msgid).append('<p> <strong>查冊種類 SEARCH Type:</strong>' + dataresult.SearchType + '</p>');
            $(msgid).append('<p> <strong>本登記冊列明有關物業截至 </strong>' + dataresult.CutTime + ' <strong>之資料</strong></p>');
            $(msgid).append('<h2>物業資料 PROPERTY PARTICULARS</h2>');
            $(msgid).append('<p> <strong>PROPERTY REFERENCE NUMBER (LandNO):</strong>' + dataresult.LandNO + '</p>');
            $(msgid).append('<p> <strong>LOT NO.:</strong>' + dataresult.LotNo + '</p>');
            $(msgid).append('<p><strong>SHARE OF THE LOT:</strong>' + dataresult.ShareLot + '</p>');
            $(msgid).append('<p><strong>Address:</strong> ' + dataresult.Address + '</p>');
            $(msgid).append('<p><strong>地址:</strong> ' + dataresult.Address_Cn + '</p>');
            $(msgid).append('<hr>');
            $(msgid).append('<h2>業主資料 OWNER PARTICULARS</h2>');
            dataresult.OwnerName.forEach(function (o) {
                $(msgid).append('<p><strong>NAME OF OWNER:</strong>' + o.name + ',<strong>Type:</strong>' + o.type +
                    ',<strong>Capacity:</strong>' + o.Capacity +
                    ',<strong>MEMORIAL NO.</strong>' + o.MemorialNo +
                    ',<strong>DATE OF INSTRUMENT:</strong>' + o.DateInstrument +
                    ',<strong>DATE OF REGISTRATION:</strong>' + o.DateRegistration +
                    ',<strong>Consideration:</strong>' + o.Consideration +
                    '</p>');
            }, this);
            $(msgid).append('<hr>');
            $(msgid).append('<h2>物業涉及的 INCUMBRANCES</h2>');
            dataresult.InFavourName.forEach(function (o) {
                $(msgid).append('<p><strong>IN FAVOUR OF:</strong>' + o.name + ',<strong>Type:</strong>' + o.type +
                    ',<strong>MEMORIAL NO.</strong>' + o.MemorialNo +
                    ',<strong>DATE OF INSTRUMENT:</strong>' + o.DateInstrument +
                    ',<strong>DATE OF REGISTRATION:</strong>' + o.DateRegistration +
                    ',<strong>Nature:</strong>' + o.Nature +
                    ',<strong>Consideration:</strong>' + o.Consideration +
                    '</p>');
            }, this);
        } else {
            if (!dataresult) {
                console.log('没有记录');
            } else {
                console.log('没有找到标签：' + msgid);
            }

        }
    },

    getData: function (fileData, msgid) {
        var _curr = getDataHtml;
        //data 
        var dataresult = {
            SearchTime: undefined,
            SearchType: undefined,
            CutTime: undefined,
            LandNO: undefined,//物業參考編號
            LotNo: undefined,//地段編號
            HeldUnder: undefined,//批約
            LeaseTerm: undefined,//年期
            Commencement: undefined,//開始日期
            Rent: undefined,//開始日期
            ShareLot: undefined,
            Address: undefined,
            Address_Cn: undefined,
            Remarks: undefined,
            OwnerName: [],
            InFavourName: [],
            PendingRegName: [],
            OwnerNameAll: [],
            InFavourNameAll: [],
            PendingRegNameAll: []
        }

        if (!fileData) {
            console.log("Error:提供的文件内容为空。。。");
            if (_curr.initFun) {
                _curr.initFun.initHtmlData(undefined);
            }
            return undefined;
        }
        console.log(fileData.length);

        var $body = $('<div></div>').html(fileData);
        var $Alltable = $body.children('table');

        console.log($Alltable.length);


        //土 地 登 記 冊 LAND REGISTER
        var $talbe0 = $Alltable.eq(0);
        //物業資料 PROPERTY PARTICULARS
        var $talbe1 = $Alltable.eq(1);
        //業主資料 OWNER PARTICULARS
        var $talbe2 = $Alltable.eq(2);
        //物業涉及的 INCUMBRANCES
        var $talbe3 = $Alltable.eq(3);
        //等待註冊的契約
        var $talbe4 = $Alltable.eq(4);

        for (var t = 0; t < $Alltable.length; t++) {
            var table = $Alltable.eq(t);
            var tableText = table.text();
            if (_curr.chkFileType(/LAND\ REGISTER/ig, tableText) && _curr.chkFileType(/本登記冊列明有關物業截至/ig, tableText)) {
                $talbe0 = table;
                continue;
            }
            if (_curr.chkFileType(/PROPERTY\ PARTICULARS/ig, tableText) && _curr.chkFileType(/PROPERTY\ REFERENCE\ NUMBER/ig, tableText)) {
                $talbe1 = table;
                continue;
            }
            if (_curr.chkFileType(/OWNER\ PARTICULARS/ig, tableText) && _curr.chkFileType(/NAME\ OF\ OWNER/ig, tableText)) {
                $talbe2 = table;
                continue;
            }
            if (_curr.chkFileType(/INCUMBRANCES/ig, tableText) && _curr.chkFileType(/IN\ FAVOUR\ OF/ig, tableText)) {
                $talbe3 = table;
                continue;
            }
            if (_curr.chkFileType(/DEEDS\ PENDING\ REGISTRATION/ig, tableText) && _curr.chkFileType(/IN\ FAVOUR\ OF/ig, tableText)) {
                $talbe4 = table;
                continue;
            }
        }


        //土 地 登 記 冊 LAND REGISTER
        var $talbe0Tr = $talbe0.find('tr');
        for (var x = 0; x < $talbe0Tr.length; x++) {
            var tr0 = $talbe0Tr.eq(x);
            tr0.find('br').replaceWith("@");
            var tr0Text = tr0.text().trim();
            if (!tr0Text) continue;

            if (_curr.chkFileType(/SEARCH\ TYPE\:/ig, tr0Text) || _curr.chkFileType(/SEARCH\ DATE\ AND\ TIME\:/ig)) {
                var tr0TextStr = tr0Text.split('@');
                //console.log(tr0TextStr)
                tr0TextStr.forEach(function (str) {
                    if (_curr.chkFileType(/SEARCH\ DATE\ AND\ TIME\:/ig, str)) {
                        var tmpTime = str.split('TIME:')[1].trim().replace(/ /g, ' ');
                        dataresult.SearchTime = _curr.formateDateToRightDate(tmpTime, 0, 1, 2);
                    }
                    if (_curr.chkFileType(/SEARCH\ TYPE\:/ig, str)) {
                        dataresult.SearchType = str.split('TYPE:')[1].trim();
                    }
                }, this);
                continue;
            }

            if (_curr.chkFileType(/本登記冊列明有關物業截至/ig, tr0Text)) {
                //console.log(tr0Text)
                var tr0TextStr = tr0Text.split('@');
                tr0TextStr.forEach(function (str) {
                    if (_curr.chkFileType(/本登記冊列明有關物業截至/ig, str)) {
                        var dd = str.split(' ');
                        //console.log(dd)
                        var tmpTimeCutTime = dd[1].trim().replace(/ /g, ' ');
                        dataresult.CutTime = _curr.formateDateToRightDate(tmpTimeCutTime, 0, 1, 2);
                    }
                }, this);
                continue;
            }

        }

        //物業資料 PROPERTY PARTICULARS
        var $talbe1Tr = $talbe1.find('tr');
        for (var x = 0; x < $talbe1Tr.length; x++) {
            var tr1 = $talbe1Tr.eq(x);
            var tr1Text = tr1.text().trim();
            if (!tr1Text) continue;

            //console.log(tr1Text);

            if (_curr.chkFileType(/PROPERTY\ REFERENCE\ NUMBER\ \(PRN\)\:/ig, tr1Text)) {
                dataresult.LandNO = tr1Text.split(':')[1].trim();
                continue;
            }
            if (_curr.chkFileType(/LOT\ NO\.\:/ig, tr1Text)) {

                dataresult.LotNo = tr1.children('td').eq(1).text().trim();
                dataresult.HeldUnder = tr1.children('td').eq(3).text().trim();
                continue;
            }
            if (_curr.chkFileType(/SHARE\ OF\ THE\ LOT\:/ig, tr1Text)) {
                dataresult.ShareLot = tr1Text.split(':')[1].trim();
                continue;
            }
            if (_curr.chkFileType(/COMMENCEMENT\ OF\ LEASE\ TERM\:/ig, tr1Text)) {

               var tCommencement = tr1Text.split(':')[1].trim();
               dataresult.Commencement = _curr.formateDateToRightDate(tCommencement, 0, 1, 2);
                continue;
            }
            if (_curr.chkFileType(/LEASE\ TERM\:/ig, tr1Text)) {
                dataresult.LeaseTerm = tr1Text.split(':')[1].trim();
                continue;
            }

            if (_curr.chkFileType(/RENT\ PER\ ANNUM\:/ig, tr1Text)) {
                dataresult.Rent = tr1Text.split(':')[1].trim();
                continue;
            }
            if (_curr.chkFileType(/REMARKS\:/ig, tr1Text)) {
                dataresult.Remarks = tr1Text.split(':')[1].trim();
                continue;
            }
            if (_curr.chkFileType(/address\:/ig, tr1Text)) {
                var tmptd = tr1.children('td').eq(1);
                var tmptdZh = tr1.children('td').eq(3);
                tmptd.find('br').replaceWith(" ,");
                dataresult.Address = tmptd.text().trim();
                dataresult.Address_Cn = tmptdZh.text().trim();
                continue;
            }
        }

        //業主資料 OWNER PARTICULARS
        var $talbe2Tr = $talbe2.find('tr');
        for (var x = 3; x < $talbe2Tr.length; x++) {
            var tr2 = $talbe2Tr.eq(x);
            var trNextAll = tr2.next().text().replace(/[\↵\t\n\v\r]/g, '').trim();
            console.log(trNextAll);

            var _alltd2 = tr2.children('td');
            if (_alltd2.length < 6) continue;
            var tmp_owner = _alltd2.eq(0).text().trim();
            var tmp_Capacity = _alltd2.eq(1).text().trim();
            var tmp_MemorialNo = _alltd2.eq(2).text().trim();
            var tmp_DateInstrument = _curr.formateDateToRightDate(_alltd2.eq(3).text().trim(), 0, 1, 2);
            var tmp_DateRegistration = _curr.formateDateToRightDate(_alltd2.eq(4).text().trim(), 0, 1, 2);
            var tmp_Consideration = _alltd2.eq(5).text().trim();


            if (tmp_owner === '--') { tmp_owner = undefined };

            if (_curr.chkFileType(/REMARKS\:/ig, trNextAll)) {
                trNextAll = trNextAll.split(':')[1].trim();
            }
            if (tmp_owner) {
                tmp_owner = tmp_owner.replace('-', '');
            }
            var tmpPush = {
                FullName_En: tmp_owner,
                Owner: tmp_owner,
                Type: undefined,
                Capacity: tmp_Capacity,
                MemorialNo: tmp_MemorialNo,
                DateInstrument: tmp_DateInstrument,
                DateRegistration: tmp_DateRegistration,
                Consideration: tmp_Consideration,
                Remarks: trNextAll
            };
            if (tmp_owner) {
                if (_curr.chkFileType(/limited|Ltd\.|Co\.|公司/ig, tmp_owner)) {
                    //公司
                    tmpPush.Type = 2;
                } else {
                    //个人
                    tmpPush.Type = 1;
                }
                dataresult.OwnerName.push(tmpPush);
            }
            dataresult.OwnerNameAll.push(tmpPush);
        }

        //物業涉及的 INCUMBRANCES
        var $talbe3Tr = $talbe3.find('tr');
        for (var x = 3; x < $talbe3Tr.length; x++) {
            var tr3 = $talbe3Tr.eq(x);
            var trNextAll = tr3.next().text().replace(/[\↵\t\n\v\r]/g, '').trim();
            console.log(trNextAll);

            var _alltd3 = tr3.children('td');
            if (_alltd3.length < 6) continue;
            var tmp_MemorialNo = _alltd3.eq(0).text().trim();
            var tmp_DateInstrument = _curr.formateDateToRightDate( _alltd3.eq(1).text().trim(), 0, 1, 2);
            var tmp_DateRegistration = _curr.formateDateToRightDate(_alltd3.eq(2).text().trim(), 0, 1, 2);
            var tmp_Nature = _alltd3.eq(3).text().trim();
            var tmp_InFavourName = _alltd3.eq(4).text().trim();
            var tmp_Consideration = _alltd3.eq(5).text().trim();

            if (tmp_InFavourName === '--') { tmp_InFavourName = undefined };

            if (_curr.chkFileType(/REMARKS\:/ig, trNextAll)) {
                trNextAll = trNextAll.split(':')[1].trim();
            }
            if (tmp_InFavourName) {
                tmp_InFavourName = tmp_InFavourName.replace('-', '');
            }
            var tmpPush = {
                FullName_En: tmp_InFavourName,
                Favour: tmp_InFavourName,
                Type: undefined,
                Nature: tmp_Nature,
                MemorialNo: tmp_MemorialNo,
                DateInstrument: tmp_DateInstrument,
                DateRegistration: tmp_DateRegistration,
                Consideration: tmp_Consideration,
                Remarks: trNextAll
            };
            if (tmp_InFavourName) {
                if (_curr.chkFileType(/limited|Ltd\.|Co\.|公司/ig, tmp_InFavourName)) {
                    //公司
                    tmpPush.Type = 2;
                } else {
                    //个人
                    tmpPush.Type = 1;
                }
                dataresult.InFavourName.push(tmpPush);
            }

            dataresult.InFavourNameAll.push(tmpPush);
        }

        //等待註冊的契約
        var $talbe4Tr = $talbe4.find('tr');
        for (var x = 3; x < $talbe4Tr.length; x++) {
            var tr4 = $talbe4Tr.eq(x);
            var trNextAll = tr4.next().text().replace(/[\↵\t\n\v\r]/g, '').trim();
            console.log(trNextAll);

            var _alltd4 = tr4.children('td');
            if (_alltd4.length < 6) continue;
            var tmp_MemorialNo = _alltd4.eq(0).text().trim();
            var tmp_DateInstrument = _curr.formateDateToRightDate(_alltd4.eq(1).text().trim(), 0, 1, 2);
            var tmp_DateDelivery = _curr.formateDateToRightDate(_alltd4.eq(2).text().trim(), 0, 1, 2);
            var tmp_Nature = _alltd4.eq(3).text().trim();
            var tmp_InFavourName = _alltd4.eq(4).text().trim();
            var tmp_Consideration = _alltd4.eq(5).text().trim();

            if (tmp_InFavourName === '--') { tmp_InFavourName = undefined };

            if (_curr.chkFileType(/REMARKS\:/ig, trNextAll)) {
                trNextAll = trNextAll.split(':')[1].trim();
            }

            if (tmp_InFavourName) {
                tmp_InFavourName = tmp_InFavourName.replace('-', '');
            }

            var tmpPush = {
                FullName_En: tmp_InFavourName,
                Favour: tmp_InFavourName,
                Type: undefined,
                Nature: tmp_Nature,
                MemorialNo: tmp_MemorialNo,
                DateInstrument: tmp_DateInstrument,
                DateDelivery: tmp_DateDelivery,
                Consideration: tmp_Consideration,
                Remarks: trNextAll
            }
            if (tmp_InFavourName) {
                if (_curr.chkFileType(/limited|Ltd\.|Co\.|行政|公司/ig, tmp_InFavourName)) {
                    //公司2
                    tmpPush.Type = 2;
                } else {
                    //个人1
                    tmpPush.Type = 1;
                }
                dataresult.PendingRegName.push(tmpPush);
            }
            dataresult.PendingRegNameAll.push(tmpPush)
        }

        //console.log(dataresult);
        //_curr.dataDisplay(dataresult, msgid);
        _curr.Data = dataresult;
        if (_curr.initFun) {
            _curr.initFun.initHtmlData(_curr.Data);
        }
        return dataresult;
    },

    getDataHtmlClick: function (input, msgid) {
        var _curr = getDataHtml;
        try {
            console.log(input.value);
            if (!input.value) return null;
            if (!_curr.chkFileType(/\.htm[l]?$/i, input.value)) {
                console.log("请选择html|htm文件.");
                alert("请选择html|htm文件.");
                input.focus();
                return null;
            }

            //支持chrome IE10
            if (window.FileReader) {
                var file = input.files[0];
                filename = file.name.split(".")[0];
                var reader = new FileReader();
                reader.onload = function () {
                    //console.log(this.result);
                    _curr.getData(this.result, msgid);
                    return this.result;
                }
                reader.readAsText(file);
            }
                //支持IE 7 8 9 10
            else if (typeof window.ActiveXObject != 'undefined') {
                var xmlDoc;
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = false;
                xmlDoc.load(input.value);
                //console.log(xmlDoc.xml);
                _curr.getData(xmlDoc.xml, msgid);

                return xmlDoc.xml;
            }
                //支持FF
            else if (document.implementation && document.implementation.createDocument) {
                var xmlDoc;
                xmlDoc = document.implementation.createDocument("", "", null);
                xmlDoc.async = false;
                xmlDoc.load(input.value);
                //console.log(xmlDoc.xml);
                _curr.getData(xmlDoc.xml, msgid);

                return xmlDoc.xml;
            } else {
                alert('error');
                return null;
            }
        } catch (error) {
            alert(error);
            return null;
        }
    },
    chkFileType: function (reg, value) {
        try {
            return reg.test(value);
        } catch (error) {
            alert(error);
            return false;
        }
    },
    formatDate: function (curr_time) {
        try {
            var strDate = curr_time.getFullYear() + "-";
            strDate += curr_time.getMonth() + 1 + "-";
            strDate += curr_time.getDate()
            // //console.log(strDate);
            return strDate
        } catch (e) {
            return curr_time
        }
    },
    formatDateTime: function (curr_time) {
        try {
            var strDate = curr_time.getFullYear() + "-";
            strDate += curr_time.getMonth() + 1 + "-";
            strDate += curr_time.getDate() + " ";
            strDate += curr_time.getHours() + ":";
            strDate += curr_time.getMinutes() + ":";
            strDate += curr_time.getSeconds();
            // //console.log(strDate);
            return strDate
        } catch (e) {
            return curr_time
        }

    },
    formateDateToRightDate: function (value, dIndex, mIndex, yIndex) {

        if (value.indexOf('/') > -1) {
            var dateValue = value
            var timeValue = undefined;
            if (value.indexOf(' ')) {
                var dd0 = value.split(' ');
                dateValue = dd0[0];
                timeValue = dd0[1];
            }
            var dd = dateValue.split('/');
            if (dd.length == 3) {
                try {
                    var days = dd[dIndex];
                    var month = dd[mIndex];
                    var years = dd[yIndex];
                    //2016-09-21 00:00:00
                    var result = years + '-' + month + '-' + days
                    if (timeValue) {
                        result += ' ' + timeValue;
                    }
                    return result;
                } catch (e) {
                    return value;
                }
            }
        }
        return value;
    }
}