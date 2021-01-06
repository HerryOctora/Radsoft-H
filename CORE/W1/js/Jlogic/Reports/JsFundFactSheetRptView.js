$(document).ready(function () {
    var _valueDate, _periodPK, _fundPK;

    // Get All Parameters
    getAllParameters();

    // Kebijakan Investasi
    createTableKebijakanInvestasi();
    
    // Informasi Produk
    createTableInformasiProduk();

    // Chart Komposisi Sektor
    createChart();
    $("#contentChart").bind("kendo:skinChange", createChart);
    $(".box").bind("change", refreshChart);
    
    // Chart Profil Risiko
    createChartSlider();

    // Chart Grafik Kinerja 1
    createChart2();
    $("#contentChart2").bind("kendo:skinChange", createChart2);
    $(".box").bind("change", refreshChart2);

    // Chart Grafik Kinerja 2
    createChart3();
    $("#contentChart3").bind("kendo:skinChange", createChart3);
    $(".box").bind("change", refreshChart3);

    // Table Kinerja 1
    createTableKinerja1();

    // Table Kinerja 2
    createTableKinerja2();

    // Others Functions
    getBrowserUse();
    printWindow();

    function createTableFromJSON(data) {
        var myBooks = [];
        myBooks = data;

        // EXTRACT VALUE FOR HTML HEADER. 
        // ('Book ID', 'Book Name', 'Category' and 'Price')
        var col = [];
        for (var i = 0; i < myBooks.length; i++) {
            for (var key in myBooks[i]) {
                if (col.indexOf(key) === -1) {
                    col.push(key);
                }
            }
        }

        // CREATE DYNAMIC TABLE.
        var table = document.createElement("table");
        table.setAttribute("border", "0");
        table.setAttribute("style", "width: 100%; font-family: 'Biko'; margin-top: -5px; margin-left: -3px;");

        // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

        var tr = table.insertRow(-1);                   // TABLE ROW.

        for (var i = 0; i < col.length; i++) {
            var th = document.createElement("th");      // TABLE HEADER.
            th.innerHTML = col[i];
            //tr.appendChild(th);

            if (j == 0) {
                th.setAttribute("width", "30%");
                th.setAttribute("valign", "top");
                th.setAttribute("align", "left");
                th.setAttribute("style", "color: #070606; font-family: 'Biko'; font-size: 12px; margin-top: -1px; margin-bottom: -1px;");
            } else {
                th.setAttribute("width", "30%");
                th.setAttribute("valign", "top");
                th.setAttribute("align", "left");
                th.setAttribute("style", "color: #070606; font-family: 'Biko'; font-size: 12px; margin-top: -1px; margin-bottom: -1px;");
            }
        }

        // ADD JSON DATA TO THE TABLE AS ROWS.
        for (var i = 0; i < myBooks.length; i++) {
            tr = table.insertRow(-1);

            for (var j = 0; j < col.length; j++) {
                if (j == 0) {
                    var td1 = tr.insertCell(-1);

                    td1.setAttribute("width", "30%");
                    td1.setAttribute("valign", "top");
                    td1.setAttribute("align", "left");
                    td1.setAttribute("style", "color: #070606; font-family: 'Biko'; font-size: 12px; margin-top: -1px; margin-bottom: -1px;");

                    td1.innerHTML = myBooks[i][col[j]];
                } else {
                    var td2 = tr.insertCell(-1);

                    td2.setAttribute("width", "70%");
                    td2.setAttribute("valign", "top");
                    td2.setAttribute("align", "left");
                    td2.setAttribute("style", "color: #070606; font-family: 'Biko'; font-size: 12px; margin-top: -1px; margin-bottom: -1px;");

                    td2.innerHTML = myBooks[i][col[j]];
                }                
            }
        }

        // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
        var divContainer = document.getElementById("dataKebijakanInvestasi");
        divContainer.innerHTML = "";
        divContainer.appendChild(table);
    }
    
    function createTableKebijakanInvestasi() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetKebijakanInvestasi/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                createTableFromJSON(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
                return;
            }
        });
    }
    
    function createTableInformasiProduk() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetInformasiProduk/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#val_TanggalPerdana").text(data[0].TanggalPerdana);
                $("#val_NilaiAktivaBersih").text(formatMoney(data[0].NilaiAktivaBersih, 2, ""));
                $("#val_TotalUnitPenyertaan").text(formatMoney(data[0].TotalUnitPenyertaan, 2, ""));
                $("#val_NilaiAktivaBersihAtauUnit").text(formatMoney(data[0].NilaiAktivaBersihAtauUnit, 2, ""));
                $("#val_FaktorRisikoUtama").text(data[0].FaktorRisikoUtama);
                $("#val_ManfaatInvestasi").text(data[0].ManfaatInvestasi);
                $("#val_ImbalJasaManagerInvestasi").text(data[0].ImbalJasaManagerInvestasi);
                $("#val_ImbalJasaBankKustodian").text(data[0].ImbalJasaBankKustodian);
                $("#val_BiayaPembelian").text(data[0].BiayaPembelian);
                $("#val_BiayaPenjualan").text(data[0].BiayaPenjualan);
                $("#val_BankKustodian").text(data[0].BankKustodian);
                $("#val_BankAccount").text(data[0].BankAccount);
                $("#val_Fund").text(data[0].Fund);
                $("#val_NPWP").text(data[0].NPWP);
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $("#val_TanggalPerdana").text("");
                $("#val_NilaiAktivaBersih").text("0");
                $("#val_TotalUnitPenyertaan").text("0");
                $("#val_NilaiAktivaBersihAtauUnit").text("0");
                $("#val_FaktorRisikoUtama").text("");
                $("#val_ManfaatInvestasi").text("");
                $("#val_ImbalJasaManagerInvestasi").text("0%");
                $("#val_ImbalJasaBankKustodian").text("0%");
                $("#val_BiayaPembelian").text("0%");
                $("#val_BiayaPenjualan").text("0%");
                $("#val_BankKustodian").text("");
                $("#val_BankAccount").text("");
                $("#val_Fund").text("");
                $("#val_NPWP").text("");
                return;
            }
        });
    }

    function createChart() {
        $("#chart").kendoChart({
            title: {
                text: ""
            },
            chartArea: {
                width: 500,
                height: 250
            },
            plotArea: {
                margin: {
                    top: 10,
                    left: 30
                }
            },
            legend: {
                position: "top"
            },
            seriesDefaults: {
                type: "pie",
                labels: {
                    color: "black",
                    template: "${ category } - ${ value }%",
                    position: "outsideEnd",
                    visible: true,
                    background: "transparent"
                }
            },
            dataSource: {
                transport:
                             {
                                 read:
                                     {
                                         url: window.location.origin + "/Radsoft/Host/GetKomposisiSektor/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
                                         dataType: "json"
                                     }
                             }
            },
            series: [{
                type: "pie",
                field: "Value",
                categoryField: "Category"
            }],
            seriesColors: ["#061735", "#094374", "#06b293", "#044d8a", "#03a9f4", "#ff9800", "#fad84a", "#4caf50"],
            tooltip: {
                visible: true,
                template: "${ category } - ${ value }%",
                color: "white"
            }
        });
    }

    function refreshChart() {
        var chart = $("#chart").data("kendoChart"),
            pieSeries = chart.options.series[0],
            labels = $("#labels").prop("checked"),
            alignInputs = $("input[name='alignType']"),
            alignLabels = alignInputs.filter(":checked").val();

        chart.options.transitions = false;
        pieSeries.labels.visible = labels;
        pieSeries.labels.align = alignLabels;

        alignInputs.attr("disabled", !labels);

        chart.refresh();
    }

    function downloadChart() {
        var chart = $("#chart").getKendoChart();
        chart.saveAsPDF();
    }

    function createChartSlider() {
        // Get Position Profil Risiko & Create Profil Risiko
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetPositionProfilRisiko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                createProfilRisiko(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
                createProfilRisiko(1);
                return;
            }
        });

        //var slider = $("#slider").kendoSlider({
        //    change: sliderOnChange,
        //    slide: sliderOnSlide,
        //    //precision: 1,
        //    smallStep: 1,
        //    largeStep: 1,
        //    min: 1,
        //    max: 10,
        //    value: 9,
        //    tooltip: {
        //        format: "{0:n0}"
        //    }
        //}).data("kendoSlider");
        //function sliderOnSlide(e) {
        //    //kendoConsole.log("Slide :: new slide value is: " + e.value);
        //}
        //function sliderOnChange(e) {
        //    //kendoConsole.log("Change :: new value is: " + e.value);
        //}
    }

    function createProfilRisiko(_position) {
        var _valPosition;
        if (_position == undefined || _position == "" || _position == "0" || _position == 0 || _position == null) {
            _valPosition = "5px";
        } else {
            if (_position == 1) {
                _valPosition = "20px";
            }
            else if (_position == 2) {
                _valPosition = "75px";
            }
            else if (_position == 3) {
                _valPosition = "140px";
            }
            else if (_position == 4) {
                _valPosition = "195px";
            }
            else if (_position == 5) {
                _valPosition = "255px";
            }
            else {
                _valPosition = "5px";
            }
        }

        var image = $("<img>");
        var div = $("<div>");
        image.load(function () {
            div.css({
                "width": this.width,
                "height": this.height,
                "margin-left": _valPosition,
                "padding-top": "1px",
                "background-image": "url(" + this.src + ")"
            });
            $("#divProfilRisiko").append(div);
        });
        image.attr("src", "../../Images/Menu/separator.png");
    }

    function createChart2() {
        $("#chart2").kendoChart({
            title: {
                text: ""
            },
            legend: {
                position: "top",
            },
            chartArea: {
                width: 300,
                height: 150,
                border: {
                    width: 2,
                    color: "#0f8fb9"
                }
            },
            seriesDefaults: {
                type: "line",
                style: "smooth",
                labels: {
                    color: "black",
                    visible: false,
                    background: "transparent",
                }
            },
            dataSource: {
                transport:
                             {
                                 read:
                                     {
                                         url: window.location.origin + "/Radsoft/Host/GetGrafikKinerja1/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
                                         dataType: "json"
                                     }
                             },
                group: {
                    field: "Name"
                },
                sort: {
                    field: "Date",
                    dir: "asc"
                },
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" },
                            Value: { type: "number" },
                            Date: { type: "date" }
                        }
                    }
                }
            },
            series: [
                {
                    type: "line",
                    field: "Value",
                    categoryField: "Date",
                    aggregate: "sum"
                }
            ],
            seriesColors: ["#094374", "#3b59ba", "#06b293", "#ffd800", "#00ffff", "#ff6a00", "#4800ff", "#0094ff", "#ff0000"],
            categoryAxis: {
                baseUnit: "months",
                minorGridLines: {
                    visible: false
                },
                majorGridLines: {
                    visible: false
                }
            },
            valueAxis: {
                format: "{0:n0}",
                min: 900,
                max: 1150,
            }
        });
    }

    function refreshChart2() {
        var chart = $("#chart2").data("kendoChart"),
            series = chart.options.series,
            categoryAxis = chart.options.categoryAxis,
            baseUnitInputs = $("input:radio[name=baseUnit]"),
            aggregateInputs = $("input:radio[name=aggregate]");

        for (var i = 0, length = series.length; i < length; i++) {
            series[i].aggregate = aggregateInputs.filter(":checked").val();
        };

        categoryAxis.baseUnit = baseUnitInputs.filter(":checked").val();

        chart.refresh();
    }

    function createChart3() {
        $("#chart3").kendoChart({
            title: {
                text: ""
            },
            legend: {
                position: "top",
            },
            chartArea: {
                width: 300,
                height: 150,
                border: {
                    width: 2,
                    color: "#0f8fb9"
                }
            },
            seriesDefaults: {
                type: "column",
                style: "smooth",
                labels: {
                    color: "black",
                    visible: false,
                    background: "transparent",
                }
            },
            dataSource: {
                transport:
                             {
                                 read:
                                     {
                                         url: window.location.origin + "/Radsoft/Host/GetGrafikKinerja2/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
                                         dataType: "json"
                                     }
                             },
                group: {
                    field: "Name"
                },
                sort: {
                    field: "Date",
                    dir: "asc"
                },
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" },
                            Value: { type: "number" },
                            Date: { type: "date" }
                        }
                    }
                }
            },
            series: [
                {
                    type: "column",
                    field: "Value",
                    categoryField: "Date",
                }
            ],
            seriesColors: ["#094374", "#3b59ba", "#06b293", "#ffd800", "#00ffff", "#ff6a00", "#4800ff", "#0094ff", "#ff0000"],
            categoryAxis: {
                baseUnit: "months",
                minorGridLines: {
                    visible: false
                },
                majorGridLines: {
                    visible: false
                },
                labels: {
                    padding: {
                        top: 15
                    }
                }
            },
            valueAxis: {
                labels: {
                    format: "{0:n2}%"
                },
                min: -3,
                max: 9
            }
        });
    }

    function refreshChart3() {
        var chart = $("#chart3").data("kendoChart"),
            series = chart.options.series,
            categoryAxis = chart.options.categoryAxis,
            baseUnitInputs = $("input:radio[name=Chart3_baseUnit]"),
            aggregateInputs = $("input:radio[name=Chart3_aggregate]");

        for (var i = 0, length = series.length; i < length; i++) {
            series[i].aggregate = aggregateInputs.filter(":checked").val();
        };

        categoryAxis.baseUnit = baseUnitInputs.filter(":checked").val();

        chart.refresh();
    }
    
    function createTableKinerja1() {
        var myList = [];
        //myList = [{ "val_FundID": "ADE", "val_1Mo": 0.87, "val_3Mo": 0.19, "val_6Mo": 0.00, "val_YTD": 0.00, "val_1Y": 0.00, "val_3Y": 0.00, "val_SejakPerdana": -0.10 }, { "val_FundID": "IRDSH", "val_1Mo": 5.46, "val_3Mo": 6.45, "val_6Mo": 0.00, "val_YTD": 0.00, "val_1Y": 0.00, "val_3Y": 0.00, "val_SejakPerdana": 6.49 }, { "val_FundID": "IHSG", "val_1Mo": 6.78, "val_3Mo": 7.71, "val_6Mo": 0.00, "val_YTD": 0.00, "val_1Y": 0.00, "val_3Y": 0.00, "val_SejakPerdana": 9.38 }];

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetTableKinerja1/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                myList = data;

                // Builds the HTML Table out of myList.
                function buildHtmlTable(selector) {
                    var columns = addAllColumnHeaders(myList, selector);

                    for (var i = 0; i < myList.length; i++) {
                        var row$ = $('<tr/>');
                        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                            var cellValue = myList[i][columns[colIndex]];

                            if (cellValue == null) { cellValue = ""; }
                            if (cellValue == 0) { cellValue = "-"; }

                            row$.append($('<td/>').html(cellValue));
                        }
                        $(selector).append(row$);
                    }
                }

                // Adds a header row to the table and returns the set of columns.
                // Need to do union of keys from all records as some records may not contain
                // all records.
                function addAllColumnHeaders(myList, selector) {
                    var columnSet = [];
                    var headerTr$ = $('<tr/>');

                    // Get Column Header
                    for (var i = 0; i < myList.length; i++) {
                        var rowHash = myList[i];
                        for (var key in rowHash) {
                            if ($.inArray(key, columnSet) == -1) {
                                columnSet.push(key);
                                headerTr$.append($('<th/>').html(key));
                            }
                        }
                    }

                    //// Create Table Header
                    //$(selector).append(headerTr$);

                    return columnSet;
                }

                //// Set Data Table
                //buildHtmlTable('#excelDataTable');

                // Builds the HTML Table out of myList.
                function createTable(selector) {
                    var columns = addAllColumnHeaders(myList, selector);
                    var rows = 0;
                    var columnName0, columnName1, columnName2, columnName3, columnName4, columnName5, columnName6, columnName7;
                    var addtionalName;

                    columnName0 = "#val_FundID";
                    columnName1 = "#val_1Mo";
                    columnName2 = "#val_3Mo";
                    columnName3 = "#val_6Mo";
                    columnName4 = "#val_YTD";
                    columnName5 = "#val_1Y";
                    columnName6 = "#val_3Y";
                    columnName7 = "#val_SejakPerdana";

                    for (var i = 0; i < myList.length; i++) {
                        if (rows == 0) { addtionalName = ""; }
                        if (rows > 0) { addtionalName = "_" + rows; }

                        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                            var cellValue = myList[i][columns[colIndex]];

                            if (cellValue == null) { cellValue = ""; }
                            if (cellValue == 0) { cellValue = "-"; }

                            if (colIndex == 0 || colIndex == "0") {
                                $(columnName0 + addtionalName).text(cellValue);
                            }
                            if (colIndex == 1 || colIndex == "1") {
                                if (cellValue == "-") { $(columnName1 + addtionalName).text(cellValue); } else {
                                    $(columnName1 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 2 || colIndex == "2") {
                                if (cellValue == "-") { $(columnName2 + addtionalName).text(cellValue); } else {
                                    $(columnName2 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 3 || colIndex == "3") {
                                if (cellValue == "-") { $(columnName3 + addtionalName).text(cellValue); } else {
                                    $(columnName3 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 4 || colIndex == "4") {
                                if (cellValue == "-") { $(columnName4 + addtionalName).text(cellValue); } else {
                                    $(columnName4 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 5 || colIndex == "5") {
                                if (cellValue == "-") { $(columnName5 + addtionalName).text(cellValue); } else {
                                    $(columnName5 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 6 || colIndex == "6") {
                                if (cellValue == "-") { $(columnName6 + addtionalName).text(cellValue); } else {
                                    $(columnName6 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                            if (colIndex == 7 || colIndex == "7") {
                                if (cellValue == "-") { $(columnName7 + addtionalName).text(cellValue); } else {
                                    $(columnName7 + addtionalName).text(formatMoney(cellValue, 2, ""));
                                }
                            }
                        }

                        $("#detilTabelKinerja1" + addtionalName).show();
                        rows = rows + 1;
                    }
                }

                createTable('#dataTableKinerja');
            },
            error: function (data) {
                alertify.alert(data.responseText);
                return;
            }
        });
    }

    function createTableKinerja2() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetTableKinerja2/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDate + "/" + _periodPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data[0].Beta == 0) { $("#val_Beta").text("0"); } else { $("#val_Beta").text(formatMoney(data[0].Beta, 2, "")); }
                if (data[0].SharpeRatio == 0) { $("#val_SharpeRatio").text("0"); } else { $("#val_SharpeRatio").text(formatMoney(data[0].SharpeRatio, 2, "")); }
                if (data[0].AnnStdDeviation == 0) { $("#val_AnnStdDeviation").text("0" + "%"); } else { $("#val_AnnStdDeviation").text(formatMoney(data[0].AnnStdDeviation, 2, "") + "%"); }
                if (data[0].InformationRatio == 0) { $("#val_InformationRatio").text("0"); } else { $("#val_InformationRatio").text(formatMoney(data[0].InformationRatio, 2, "")); }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $("#val_Beta").text("0");
                $("#val_SharpeRatio").text("0");
                $("#val_AnnStdDeviation").text("0" + "%");
                $("#val_InformationRatio").text("0");
                return;
            }
        });
    }

    function printWindow() {
        //window.print();
    }
    
    function formatMoney(number, places, symbol, thousand, decimal) {
        number = number || 0;
        places = !isNaN(places = Math.abs(places)) ? places : 2;
        symbol = symbol !== undefined ? symbol : "$";
        thousand = thousand || ",";
        decimal = decimal || ".";
        var negative = number < 0 ? "-" : "",
            i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
            j = (j = i.length) > 3 ? j % 3 : 0;
        return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
    }

    function getBrowserUse() {
        var _browser = "";
        if ((navigator.userAgent.indexOf("Opera") || navigator.userAgent.indexOf('OPR')) != -1)
        {
            _browser = 'Opera';
            //alertify.success('Opera');
        }
        else if (navigator.userAgent.indexOf("Chrome") != -1)
        {
            _browser = 'Chrome';
            //alertify.success('Chrome');
        }
        else if (navigator.userAgent.indexOf("Safari") != -1)
        {
            _browser = 'Safari';
            //alertify.success('Safari');
        }
        else if (navigator.userAgent.indexOf("Firefox") != -1)
        {
            _browser = 'Firefox';
            //alertify.success('Firefox');
        }
        else if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10
        {
            _browser = 'IE';
            //alertify.success('IE');
        }
        else
        {
            _browser = 'Unknown Browser';
            //alertify.success('Unknown Browser');
        }

        if (_browser == "Chrome") {
            document.getElementById("td_InformasiProduk").setAttribute("style", "height: 568px;");
        } else if (_browser == "Firefox") {
            document.getElementById("td_InformasiProduk").setAttribute("style", "height: 576px;");
        } else {
            document.getElementById("td_InformasiProduk").setAttribute("style", "height: 565px;");
        }

        //// Result
        //alertify.success(_browser);
    }
    
    function getURLParameters(paramName) {
        var sURL = window.document.URL.toString().toLowerCase();
        if (sURL.indexOf("?") > 0) {
            var arrParams = sURL.split("?");
            var arrURLParams = arrParams[1].split("&");
            var arrParamNames = new Array(arrURLParams.length);
            var arrParamValues = new Array(arrURLParams.length);

            var i = 0;
            for (i = 0; i < arrURLParams.length; i++) {
                var sParam = arrURLParams[i].split("=");
                arrParamNames[i] = sParam[0];
                if (sParam[1] != "")
                    arrParamValues[i] = unescape(sParam[1]);
                else
                    arrParamValues[i] = "No Value";
            }

            for (i = 0; i < arrURLParams.length; i++) {
                if (arrParamNames[i] == paramName) {
                    //jAlert("Parameter:" + arrParamValues[i], "Info");
                    return arrParamValues[i];
                }
            }
            return "No Parameters Found";
        }
    }
    
    function getAllParameters() {
        // Clear All Parameters
        _valueDate = "";
        _periodPK = "";
        _fundPK = "";

        // Set All Parameters
        _valueDate = getURLParameters("valuedate");
        _periodPK = getURLParameters("periodpk");
        _fundPK = getURLParameters("fundpk");

        if (_valueDate == undefined || _valueDate == "" || _valueDate == null) {
            _valueDate = "01-01-1900";
        }

        if (_periodPK == undefined || _periodPK == "" || _periodPK == null) {
            _periodPK = "0";
        }

        if (_fundPK == undefined || _fundPK == "" || _fundPK == null) {
            _fundPK = "0";
        }

        //alertify.alert("Value Date : " + _valueDate + "; " + "PeriodPK : " + _periodPK + "; " + "FundPK : " + _fundPK);
    }

});