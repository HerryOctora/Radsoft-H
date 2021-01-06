$(document).ready(function () {
    document.title = 'FORM EXERCISE';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
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
        $("#BtnAccountRestructure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

  



    WinListInstrumentRights = $("#WinListInstrumentRights").kendoWindow({
        height: 450,
        title: "List Instrument Rights ",
        visible: false,
        width: 950,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 100 })
        },
        close: onWinListInstrumentRightsClose
    }).data("kendoWindow");

    function onWinListInstrumentRightsClose() {
        $("#gridListInstrumentRights").empty();

    } 
       
    function initWindow() {
                $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
                });

                $("#DistributionDate").kendoDatePicker({
                    format: "dd/MMM/yyyy",
                    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
                    value: null
                });

        win = $("#WinExercise").kendoWindow({
            height: 600,
            title: "Exercise Detail",
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

    var GlobValidator = $("#WinExercise").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
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

            $("#ExercisePK").val(dataItemX.ExercisePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#DistributionDate").data("kendoDatePicker").value(dataItemX.DistributionDate);
            $("#Notes").val(dataItemX.Notes);
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

        $("#Type").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Rights", value: 1 },
                { text: "Warrant", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            index: 0,
            change: OnChangeType,
            value: setCmbType()
        });
        function OnChangeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Type;
            }
        }

        //combo box InstrumentRightsPK
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#InstrumentRightsPK").kendoComboBox({
        //            dataValueField: "InstrumentPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            enable: false,
        //            change: OnChangeInstrumentRights,
        //            value: setCmbInstrumentRights()
        //        });
               
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeInstrumentRights() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        //function setCmbInstrumentRights() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.InstrumentPK == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.InstrumentPK;
        //        }
        //    }
        //}



        //combo box InstrumentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentComboByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/1",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
                }
            }
        }

        //combo box instrument rights
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentRightsPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    dataSource: data,
                    change: OnChangeInstrumentRightsPK,
                    value: setCmbInstrumentRightsPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentRightsPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbInstrumentRightsPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentRightsPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentRightsPK;
                }
            }
        }


        //combo box FundPK
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
                    enabled: false,
                    dataSource: data,
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPK() {
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

        $("#BalanceRights").kendoNumericTextBox({
            format: "n0",
            value: setBalanceRights(),
            //change: OnChangeAmount
        });
        function setBalanceRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BalanceRights == 0) {
                    return "";
                } else {
                    return dataItemX.BalanceRights;
                }
            }
        }


        $("#BalanceExercise").kendoNumericTextBox({
            format: "n0",
            value: setBalanceExercise(),
            //change: OnChangeAmount
        });
        function setBalanceExercise() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BalanceExercise == 0) {
                    return "";
                } else {
                    return dataItemX.BalanceExercise;
                }
            }
        }


        $("#BalanceRights").data("kendoNumericTextBox").enable(false);

        $("#Price").kendoNumericTextBox({
            format: "n0",
            value: setPrice(),
            //change: OnChangeAmount
        });
        function setPrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Price == 0) {
                    return "";
                } else {
                    return dataItemX.Price;
                }
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
        $("#ExercisePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").val("");
        $("#DistributionDate").val("");
        $("#FundPK").val("");
        $("#InstrumentPK").val("");
        $("#Type").val("");
        $("#InstrumentRightsPK").val("");
        $("#BalanceRights").val("");
        $("#BalanceExercise").val("");
        $("#Price").val("");
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
                             ExercisePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Date: { type: "date" },
                             DistributionDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             Type: { type: "number" },
                             InstrumentPK: { type: "number" },
                             InstrumentRightsPK: { type: "number" },
                             InstrumentRightsID: { type: "string" },
                             InstrumentRightsName: { type: "string" },
                             BalanceRights: { type: "number" },
                             BalanceExercise: { type: "number" },
                             Price: { type: "number" },
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
            var gridApproved = $("#gridExerciseApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridExercisePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridExerciseHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var ExerciseApprovedURL = window.location.origin + "/Radsoft/Exercise/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(ExerciseApprovedURL);

        $("#gridExerciseApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Exercise"
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
                { field: "ExercisePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "TypeDesc", title: "Type", width: 300 },
                { field: "InstrumentID", title: "Instrument ID", width: 300 },
                { field: "InstrumentRightsID", title: "InstrumentRights ID", width: 300 },
                { field: "BalanceRights", title: "BalanceRights", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "BalanceExercise", title: "BalanceExercise", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Price", title: "Price", width: 300 , format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "DistributionDate", title: "Distribution Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DistributionDate), 'dd/MMM/yyyy')#" },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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
        $("#TabExercise").kendoTabStrip({
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
                        var ExercisePendingURL = window.location.origin + "/Radsoft/Exercise/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(ExercisePendingURL);
                        $("#gridExercisePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Exercise"
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
                                { field: "ExercisePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "InstrumentID", title: "Instrument ID", width: 300 },
                                { field: "InstrumentRightsID", title: "InstrumentRights ID", width: 300 },
                                { field: "BalanceRights", title: "BalanceRights", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "BalanceExercise", title: "BalanceExercise", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "Price", title: "Price", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "DistributionDate", title: "Distribution Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DistributionDate), 'dd/MMM/yyyy')#" },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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

                        var ExerciseHistoryURL = window.location.origin + "/Radsoft/Exercise/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(ExerciseHistoryURL);

                        $("#gridExerciseHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Exercise"
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
                                { field: "ExercisePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "InstrumentID", title: "Instrument ID", width: 300 },
                                { field: "InstrumentRightsID", title: "InstrumentRights ID", width: 300 },
                                { field: "BalanceRights", title: "BalanceRights", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "BalanceExercise", title: "BalanceExercise", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "Price", title: "Price", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "DistributionDate", title: "Distribution Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DistributionDate), 'dd/MMM/yyyy')#" },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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
        var grid = $("#gridExerciseHistory").data("kendoGrid");
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
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    if ($("#BalanceExercise").data("kendoNumericTextBox").value() > $("#BalanceRights").data("kendoNumericTextBox").value()) {
                        alertify.alert("Balance Rights must > Balance Exercise");
                        return;
                    } else {
                        var Exercise = {
                            FundPK: $('#FundPK').val(),
                            Date: $('#Date').val(),
                            DistributionDate: $('#DistributionDate').val(),
                            InstrumentPK: $('#InstrumentPK').val(),
                            InstrumentRightsPK: $('#InstrumentRightsPK').val(),
                            Type: $('#Type').val(),
                            BalanceRights: $('#BalanceRights').val(),
                            BalanceExercise: $('#BalanceExercise').val(),
                            Price: $('#Price').val(),
                            EntryUsersID: sessionStorage.getItem("user")
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Exercise/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Exercise_I",
                            type: 'POST',
                            data: JSON.stringify(Exercise),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                win.close();
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        })
                    };

                            } else {
                                alertify.alert("Data ID Same Not Allow!");
                                win.close();
                                refresh();
                            }
            });
        }
    });


    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExercisePK").val() + "/" + $("#HistoryPK").val() + "/" + "Exercise",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                if ($("#BalanceExercise").data("kendoNumericTextBox").value() > $("#BalanceRights").data("kendoNumericTextBox").value()) {
                                    alertify.alert("Balance Rights must > Balance Exercise");
                                    return;
                                } else {
                                    var Exercise = {
                                        ExercisePK: $('#ExercisePK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        FundPK: $('#FundPK').val(),
                                        Date: $('#Date').val(),
                                        DistributionDate: $('#Date').val(),
                                        Type: $('#Type').val(),
                                        InstrumentPK: $('#InstrumentPK').val(),
                                        InstrumentRightsPK: $('#InstrumentRightsPK').val(),
                                        BalanceRights: $('#BalanceRights').val(),
                                        BalanceExercise: $('#BalanceExercise').val(),
                                        Price: $('#Price').val(),

                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Exercise/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Exercise_U",
                                        type: 'POST',
                                        data: JSON.stringify(Exercise),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            win.close();
                                            refresh();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
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
                }
            });
        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExercisePK").val() + "/" + $("#HistoryPK").val() + "/" + "Exercise",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Exercise" + "/" + $("#ExercisePK").val(),
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


    $("#btnListInstrumentRights").click(function () {
        initListInstrumentRights();
        WinListInstrumentRights.center();
        WinListInstrumentRights.open();


    });

    function initListInstrumentRights() {
        var dsListInstrumentRights = getDataSourceListInstrumentRights();
        $("#gridListInstrumentRights").kendoGrid({
            dataSource: dsListInstrumentRights,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListInstrumentRightsSelect }, title: " ", width: 85 },
               { field: "InstrumentRightsPK", title: "", hidden: true, width: 100 },
               { field: "InstrumentRightsID", title: "Rights ID", width: 300 },
               { field: "FundPK", title: "", hidden: true, width: 100 },
               { field: "FundID", title: "Fund ID", width: 300 },
               {
                   field: "Balance", title: "Balance", headerAttributes: {
                        style: "text-align: center"
                    }, width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" }
               },
            ]
        });
    }

    function getDataSourceListInstrumentRights() {



        return new kendo.data.DataSource(

                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Instrument/GetInstrumentRightsByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").data("kendoComboBox").value() + "/" + $("#Type").data("kendoComboBox").value() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                                 InstrumentRightsPK: { type: "number" },
                                 InstrumentRightsID: { type: "string" },
                                 FundPK: { type: "number" },
                                 FundID: { type: "string" },
                                 Balance: { type: "number" },
                             }
                         }
                     }
                 });
    }

    function ListInstrumentRightsSelect(e) {
        var grid = $("#gridListInstrumentRights").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $("#InstrumentRightsPK").data("kendoComboBox").value(dataItemX.InstrumentRightsPK);
        $("#FundPK").data("kendoComboBox").value(dataItemX.FundPK);
        $("#BalanceRights").data("kendoNumericTextBox").value(dataItemX.Balance);
        WinListInstrumentRights.close();

    }



    $("#BtnApproved").click(function () {
        
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExercisePK").val() + "/" + $("#HistoryPK").val() + "/" + "Exercise",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var Exercise = {
                                ExercisePK: $('#ExercisePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Exercise/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Exercise_A",
                                type: 'POST',
                                data: JSON.stringify(Exercise),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        } 
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
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExercisePK").val() + "/" + $("#HistoryPK").val() + "/" + "Exercise",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Exercise = {
                                ExercisePK: $('#ExercisePK').val(),
                                HistoryPK: $('#HistoryPK').val(),

                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Exercise/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Exercise_V",
                                type: 'POST',
                                data: JSON.stringify(Exercise),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
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
            }
        });
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExercisePK").val() + "/" + $("#HistoryPK").val() + "/" + "Exercise",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Exercise = {
                                ExercisePK: $('#ExercisePK').val(),
                                HistoryPK: $('#HistoryPK').val(),

                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Exercise/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Exercise_R",
                                type: 'POST',
                                data: JSON.stringify(Exercise),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
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
            }
        });

    });

});
