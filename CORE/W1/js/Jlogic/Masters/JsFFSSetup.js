$(document).ready(function () {
    document.title = 'FORM FFS Setup';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCountry;
    var htmlCountry;
    var htmlCountryDesc;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOldData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnOldData.png"
        });

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnApproved").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });

        $("#BtnReject").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });


        $("#BtnImportFFSSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

    }



    function initWindow() {

        
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        win = $("#WinFFSSetup").kendoWindow({
            height: 600,
            title: "FFSSetup Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");

    }



    var GlobValidator = $("#WinFFSSetup").kendoValidator().data("kendoValidator");

    function validateData() {

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.error("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }


            $("#FFSSetupPK").val(dataItemX.FFSSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#Col1").val(dataItemX.Col1);
            $("#Col2").val(dataItemX.Col2);
            $("#Col3").val(dataItemX.Col3);
            $("#Col4").val(dataItemX.Col4);
            $("#Col5").val(dataItemX.Col5);
            $("#Col6").val(dataItemX.Col6);
            $("#Col7").val(dataItemX.Col7);
            $("#Col8").val(dataItemX.Col8);
            $("#Col9").val(dataItemX.Col9);
            $("#Col10").val(dataItemX.Col10);
            $("#Col11").val(dataItemX.Col11);
            $("#Col12").val(dataItemX.Col12);
            $("#Col13").val(dataItemX.Col13);
            $("#Col14").val(dataItemX.Col14);
            $("#Col15").val(dataItemX.Col15);
            $("#Col16").val(dataItemX.Col16);
            $("#Col17").val(dataItemX.Col17);
            $("#Col18").val(dataItemX.Col18);
            $("#Col19").val(dataItemX.Col19);
            $("#Col20").val(dataItemX.Col20);
            $("#Col21").val(dataItemX.Col21);
            $("#Col22").val(dataItemX.Col22);
            $("#Col23").val(dataItemX.Col23);
            $("#Col24").val(dataItemX.Col24);
            $("#Col25").val(dataItemX.Col25);
            $("#Col26").val(dataItemX.Col26);
            $("#Col27").val(dataItemX.Col27);
            $("#TemplateType").val(dataItemX.TemplateType);
            $("#InceptionIndex").val(dataItemX.InceptionIndex);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }



       
        $("#InceptionIndex").kendoNumericTextBox({
            format: "n0",
            value: setInceptionIndex()

        });
        function setInceptionIndex() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InceptionIndex;
            }
        }

       
        $("#Col24").kendoNumericTextBox({
            format: "n0",
            value: setCol24()

        });
        function setCol24() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Col24;
            }
        }


       


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    dataSource: data,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }

        $("#TemplateType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Open End", value: 1 },
                { text: "WithoutBenchmark", value: 2 },
                 { text: "Under1Year", value: 3 }
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbTemplateType()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbTemplateType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TemplateType;
            }
        }
       

        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundPK").val("");
        $("#Col1").val("");
        $("#Col2").val("");
        $("#Col3").val("");
        $("#Col4").val("");
        $("#Col5").val("");
        $("#Col6").val("");
        $("#Col7").val("");
        $("#Col8").val("");
        $("#Col9").val("");
        $("#Col10").val("");
        $("#Col11").val("");
        $("#Col12").val("");
        $("#Col13").val("");
        $("#Col14").val("");
        $("#Col15").val("");
        $("#Col16").val("");
        $("#Col17").val("");
        $("#Col18").val("");
        $("#Col19").val("");
        $("#Col20").val("");
        $("#Col21").val("");
        $("#Col22").val("");
        $("#Col23").val("");
        $("#Col24").val("");
        $("#Col25").val("");
        $("#Col26").val("");
        $("#Col27").val("");
        $("#TemplateType").val("");
        $("#InceptionIndex").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
    }

    function getDataSource(_url) {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: _url,
                                     dataType: "json"
                                 }
                         },
                 batch: true,
                 cache: false,
                 error: function (e) {
                     alert(e.errorThrown + " - " + e.xhr.responseText);
                     this.cancelChanges();
                 },
                 pageSize: 100,
                 schema: {
                     model: {
                         fields: {
                             FFSSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             ValueDate: { type: "date" },
                             Col1: { type: "string" },
                             Col2: { type: "string" },
                             Col3: { type: "string" },
                             Col4: { type: "string" },
                             Col5: { type: "string" },
                             Col6: { type: "string" },
                             Col7: { type: "string" },
                             Col8: { type: "string" },
                             Col9: { type: "string" },
                             Col10: { type: "string" },
                             Col11: { type: "string" },
                             Col12: { type: "string" },
                             Col13: { type: "string" },
                             Col14: { type: "string" },
                             Col15: { type: "string" },
                             Col16: { type: "string" },
                             Col17: { type: "string" },
                             Col18: { type: "string" },
                             Col19: { type: "string" },
                             Col20: { type: "string" },
                             Col21: { type: "string" },
                             Col22: { type: "string" },
                             Col23: { type: "string" },
                             Col24: { type: "number" },
                             Col25: { type: "string" },
                             Col26: { type: "string" },
                             Col27: { type: "string" },
                             TemplateType: { type: "number" },
                             TemplateTypeDesc: { type: "string" },
                             InceptionIndex : {type: "number"},
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridFFSSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFFSSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFFSSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FFSSetupApprovedURL = window.location.origin + "/Radsoft/FFSSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FFSSetupApprovedURL);

        $("#gridFFSSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FFSSetup"
                }
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "FFSSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "Col1", title: "Col 1", width: 200 },
                { field: "Col2", title: "Col 2", width: 200 },
                { field: "Col3", title: "Col 3", width: 200 },
                { field: "Col4", title: "Col 4", width: 200 },
                { field: "Col5", title: "Col 5", width: 200 },
                { field: "Col6", title: "Col 6", width: 200 },
                { field: "Col7", title: "Col 7", width: 200 },
                { field: "Col8", title: "Col 8", width: 200 },
                { field: "Col9", title: "Col 9", width: 200 },
                { field: "Col10", title: "Col 10", width: 200 },
                { field: "Col11", title: "Col 11", width: 200 },
                { field: "Col12", title: "Col 12", width: 200 },
                { field: "Col13", title: "Col 13", width: 200 },
                { field: "Col14", title: "Col 14", width: 200 },
                { field: "Col15", title: "Col 15", width: 200 },
                { field: "Col16", title: "Col 16", width: 200 },
                { field: "Col17", title: "Col 17", width: 200 },
                { field: "Col18", title: "Col 18", width: 200 },
                { field: "Col19", title: "Col 19", width: 200 },
                { field: "Col20", title: "Col 20", width: 200 },
                { field: "Col21", title: "Col 21", width: 200 },
                { field: "Col22", title: "Col 22", width: 200 },
                { field: "Col23", title: "Col 23", width: 200 },
                { field: "Col24", title: "Col 24", width: 200 },
                { field: "Col25", title: "Col 25", width: 200 },
                { field: "Col26", title: "Col 26", width: 200 },
                { field: "Col27", title: "Col 27", width: 200 },
                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                { field: "InceptionIndex", title: "Inception Index", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFFSSetup").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);

                    if (tabindex == 1) {
                        var FFSSetupPendingURL = window.location.origin + "/Radsoft/FFSSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FFSSetupPendingURL);
                        $("#gridFFSSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFSSetup"
                                }
                            },
                            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                            columnMenu: false,
                            pageable: {
                                input: true,
                                numeric: false
                            },
                            reorderable: true,
                            sortable: true,
                            resizable: true,
                            toolbar: ["excel"],
                            columns: [
                                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                { field: "FFSSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "Col1", title: "Col 1", width: 200 },
                                { field: "Col2", title: "Col 2", width: 200 },
                                { field: "Col3", title: "Col 3", width: 200 },
                                { field: "Col4", title: "Col 4", width: 200 },
                                { field: "Col5", title: "Col 5", width: 200 },
                                { field: "Col6", title: "Col 6", width: 200 },
                                { field: "Col7", title: "Col 7", width: 200 },
                                { field: "Col8", title: "Col 8", width: 200 },
                                { field: "Col9", title: "Col 9", width: 200 },
                                { field: "Col10", title: "Col 10", width: 200 },
                                { field: "Col11", title: "Col 11", width: 200 },
                                { field: "Col12", title: "Col 12", width: 200 },
                                { field: "Col13", title: "Col 13", width: 200 },
                                { field: "Col14", title: "Col 14", width: 200 },
                                { field: "Col15", title: "Col 15", width: 200 },
                                { field: "Col16", title: "Col 16", width: 200 },
                                { field: "Col17", title: "Col 17", width: 200 },
                                { field: "Col18", title: "Col 18", width: 200 },
                                { field: "Col19", title: "Col 19", width: 200 },
                                { field: "Col20", title: "Col 20", width: 200 },
                                { field: "Col21", title: "Col 21", width: 200 },
                                { field: "Col22", title: "Col 22", width: 200 },
                                { field: "Col23", title: "Col 23", width: 200 },
                                { field: "Col24", title: "Col 24", width: 200 },
                                { field: "Col25", title: "Col 25", width: 200 },
                                { field: "Col26", title: "Col 26", width: 200 },
                                { field: "Col27", title: "Col 27", width: 200 },
                                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                                { field: "InceptionIndex", title: "Inception Index", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var FFSSetupHistoryURL = window.location.origin + "/Radsoft/FFSSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FFSSetupHistoryURL);

                        $("#gridFFSSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFSSetup"
                                }
                            },
                            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                            columnMenu: false,
                            pageable: {
                                input: true,
                                numeric: false
                            },
                            reorderable: true,
                            sortable: true,
                            resizable: true,
                            dataBound: gridHistoryDataBound,
                            toolbar: ["excel"],
                            columns: [
                                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                { field: "FFSSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "Col1", title: "Col 1", width: 200 },
                                { field: "Col2", title: "Col 2", width: 200 },
                                { field: "Col3", title: "Col 3", width: 200 },
                                { field: "Col4", title: "Col 4", width: 200 },
                                { field: "Col5", title: "Col 5", width: 200 },
                                { field: "Col6", title: "Col 6", width: 200 },
                                { field: "Col7", title: "Col 7", width: 200 },
                                { field: "Col8", title: "Col 8", width: 200 },
                                { field: "Col9", title: "Col 9", width: 200 },
                                { field: "Col10", title: "Col 10", width: 200 },
                                { field: "Col11", title: "Col 11", width: 200 },
                                { field: "Col12", title: "Col 12", width: 200 },
                                { field: "Col13", title: "Col 13", width: 200 },
                                { field: "Col14", title: "Col 14", width: 200 },
                                { field: "Col15", title: "Col 15", width: 200 },
                                { field: "Col16", title: "Col 16", width: 200 },
                                { field: "Col17", title: "Col 17", width: 200 },
                                { field: "Col18", title: "Col 18", width: 200 },
                                { field: "Col19", title: "Col 19", width: 200 },
                                { field: "Col20", title: "Col 20", width: 200 },
                                { field: "Col21", title: "Col 21", width: 200 },
                                { field: "Col22", title: "Col 22", width: 200 },
                                { field: "Col23", title: "Col 23", width: 200 },
                                { field: "Col24", title: "Col 24", width: 200 },
                                { field: "Col25", title: "Col 25", width: 200 },
                                { field: "Col26", title: "Col 26", width: 200 },
                                { field: "Col27", title: "Col 27", width: 200 },
                                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                                { field: "InceptionIndex", title: "Inception Index", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

                            ]
                        });
                    }
                } else {
                    refresh();
                }
            }
        });
    }


    function gridHistoryDataBound() {
        var grid = $("#gridFFSSetupHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        showDetails(null);

    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                        var FFSSetup = {
                            FundPK: $('#FundPK').val(),
                            ValueDate: $('#ValueDate').val(),
                            Col1: $('#Col1').val(),
                            Col2: $('#Col2').val(),
                            Col3: $('#Col3').val(),
                            Col4: $('#Col4').val(),
                            Col5: $('#Col5').val(),
                            Col6: $('#Col6').val(),
                            Col7: $('#Col7').val(),
                            Col8: $('#Col8').val(),
                            Col9: $('#Col9').val(),
                            Col10: $('#Col10').val(),
                            Col11: $('#Col11').val(),
                            Col12: $('#Col12').val(),
                            Col13: $('#Col13').val(),
                            Col14: $('#Col14').val(),
                            Col15: $('#Col15').val(),
                            Col16: $('#Col16').val(),
                            Col17: $('#Col17').val(),
                            Col18: $('#Col18').val(),
                            Col19: $('#Col19').val(),
                            Col20: $('#Col20').val(),
                            Col21: $('#Col21').val(),
                            Col22: $('#Col22').val(),
                            Col23: $('#Col23').val(),
                            Col24: $('#Col24').val(),
                            Col25: $('#Col25').val(),
                            Col26: $('#Col26').val(),
                            Col27: $('#Col27').val(),
                            TemplateType: $('#TemplateType').val(),
                            InceptionIndex: $('#InceptionIndex').val(),
                            //Notes: str,
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FFSSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_I",
                            type: 'POST',
                            data: JSON.stringify(FFSSetup),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.success(data);
                                win.close();
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    var FFSSetup = {
                                    FFSSetupPK: $('#FFSSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    Col1: $('#Col1').val(),
                                    Col2: $('#Col2').val(),
                                    Col3: $('#Col3').val(),
                                    Col4: $('#Col4').val(),
                                    Col5: $('#Col5').val(),
                                    Col6: $('#Col6').val(),
                                    Col7: $('#Col7').val(),
                                    Col8: $('#Col8').val(),
                                    Col9: $('#Col9').val(),
                                    Col10: $('#Col10').val(),
                                    Col11: $('#Col11').val(),
                                    Col12: $('#Col12').val(),
                                    Col13: $('#Col13').val(),
                                    Col14: $('#Col14').val(),
                                    Col15: $('#Col15').val(),
                                    Col16: $('#Col16').val(),
                                    Col17: $('#Col17').val(),
                                    Col18: $('#Col18').val(),
                                    Col19: $('#Col19').val(),
                                    Col20: $('#Col20').val(),
                                    Col21: $('#Col21').val(),
                                    Col22: $('#Col22').val(),
                                    Col23: $('#Col23').val(),
                                    Col24: $('#Col24').val(),
                                    Col25: $('#Col25').val(),
                                    Col26: $('#Col26').val(),
                                    Col27: $('#Col27').val(),
                                    TemplateType: $('#TemplateType').val(),
                                    InceptionIndex: $('#InceptionIndex').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FFSSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(FFSSetup),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
                                        win.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                }
            });
        }
    });

    $("#BtnOldData").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSSetup",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FFSSetup" + "/" + $("#FFSSetupPK").val(),
                                                dataType: "json"
                                            }
                                    },
                            batch: true,
                            cache: false,
                            error: function (e) {
                                alert(e.errorThrown + " - " + e.xhr.responseText);
                                this.cancelChanges();
                            },
                            pageSize: 10,
                            schema: {
                                model: {
                                    fields: {
                                        FieldName: { type: "string" },
                                        OldValue: { type: "string" },
                                        NewValue: { type: "string" },
                                        Similarity: { type: "number" },
                                    }
                                }
                            }
                        },
                        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                        columnMenu: false,
                        pageable: {
                            input: true,
                            numeric: false
                        },
                        height: 470,
                        reorderable: true,
                        sortable: true,
                        resizable: true,
                        dataBound: gridOldDataDataBound,
                        columns: [
                            { field: "FieldName", title: "FieldName", width: 300 },
                            { field: "OldValue", title: "OldValue", width: 300 },
                            { field: "NewValue", title: "NewValue", width: 300 },
                            { field: "Similarity", title: "Similarity", width: 120 }
                        ]
                    });
                    winOldData.center();
                    winOldData.open();
                } else {
                    alertify.alert("Data has been Changed by other user, Please check it first!");
                    win.close();
                    refresh();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };

    $("#BtnApproved").click(function () {
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                            var FFSSetup = {
                                FFSSetupPK: $('#FFSSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_A",
                                type: 'POST',
                                data: JSON.stringify(FFSSetup),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
            }
        });
    });

    $("#BtnVoid").click(function () {
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                            var FFSSetup = {
                                FFSSetupPK: $('#FFSSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_V",
                                type: 'POST',
                                data: JSON.stringify(FFSSetup),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
                                    win.close();
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
            }
        });
    });

    $("#BtnReject").click(function () {
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                            var FFSSetup = {
                                FFSSetupPK: $('#FFSSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_R",
                                type: 'POST',
                                data: JSON.stringify(FFSSetup),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
            }
        });

    });

    $("#BtnImportFFSSetup").click(function () {
        document.getElementById("FileImportFFSSetup").click();
    });

    $("#FileImportFFSSetup").change(function () {
        //getFileType(document.getElementById('FileImportCloseNavSInvest').value);
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportFFSSetup").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("FFSSetup", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data == "false") {
                        $.unblockUI();
                        $("#FileImportFFSSetup").val("");
                        alertify.alert(data)
                        refresh();
                    }
                    else {
                        $.unblockUI();
                        $("#FileImportFFSSetup").val("");
                        alertify.alert(data)
                    }



                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportFFSSetup").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportFFSSetup").val("");
        }

    });


});
