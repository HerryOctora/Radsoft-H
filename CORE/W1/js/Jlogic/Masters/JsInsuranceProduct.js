$(document).ready(function () {
    document.title = 'INSURANCE PRODUCT';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;   
    var gridHeight = screen.height - 300;
    //grid filter
    var filterField;
    var filterOperator;
    var filtervalue;
    var filterlogic;
    //end grid filter


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
    }

    function initWindow() {
        //
    }

    win = $("#WinInsuranceProduct").kendoWindow({
        height: 900,
        title: "Insurance Product",
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



    $("#btnListBankCountry1").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#BankCountry1";
        htmlCountryDesc = "#BankCountry1Desc";
    });
    $("#btnClearListBankCountry1").click(function () {
        $("#BankCountry1").val("");
        $("#BankCountry1Desc").val("");
    });

    $("#btnListBankCountry2").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#BankCountry2";
        htmlCountryDesc = "#BankCountry2Desc";
    });
    $("#btnClearListBankCountry2").click(function () {
        $("#BankCountry2").val("");
        $("#BankCountry2Desc").val("");
    });

    $("#btnListBankCountry3").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#BankCountry3";
        htmlCountryDesc = "#BankCountry3Desc";
    });
    $("#btnClearListBankCountry3").click(function () {
        $("#BankCountry3").val("");
        $("#BankCountry3Desc").val("");
    });


    function getDataSourceListCountry() {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 25,
                schema: {
                    model: {
                        fields: {
                            Code: { type: "string" },
                            DescOne: { type: "string" },

                        }
                    }
                }
            });
    }
    

    ////combo box Bank
    //$.ajax({
    //    url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //    type: 'GET',
    //    contentType: "application/json;charset=utf-8",
    //    success: function (data) {
            
    //        $("#NamaBank1").kendoComboBox({
    //            dataValueField: "BankPK",
    //            dataTextField: "ID",
    //            dataSource: data,
    //            filter: "contains",
    //            suggest: true,
    //            change: onChangeNamaBank1,
    //            value: setCmbNamaBank1()
    //        });
    //        $("#NamaBank2").kendoComboBox({
    //            dataValueField: "BankPK",
    //            dataTextField: "ID",
    //            dataSource: data,
    //            filter: "contains",
    //            suggest: true,
    //            change: onChangeNamaBank2,
    //            value: setCmbNamaBank2()
    //        });
    //        $("#NamaBank3").kendoComboBox({
    //            dataValueField: "BankPK",
    //            dataTextField: "ID",
    //            dataSource: data,
    //            filter: "contains",
    //            suggest: true,
    //            change: onChangeNamaBank3,
    //            value: setCmbNamaBank3()
    //        });

    //        function onChangeNamaBank1() {
    //            $("#BIMemberCode1").val("");
    //            $("#BICCode1Name").val("");
    //            $("#BankCountry1").val("");
    //            if (this.value() && this.selectedIndex == -1) {
    //                var dt = this.dataSource._data[0];
    //                this.text('');
    //            }
    //            else if ($("#NamaBank1").val() != "") {
    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank1").val(),
    //                    type: 'GET',
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {
    //                        $("#BIMemberCode1").val(data.BICode);
    //                        $("#BankCountry1Desc").val(data.CountryDesc);
    //                        $("#BICCode1Name").val(data.SInvestID);
    //                    },
    //                    error: function (data) {
    //                        alert(data.responseText);
    //                    }
    //                });
    //            }

    //        }
    //        function onChangeNamaBank2() {
    //            $("#BIMemberCode2").val("");
    //            $("#BankCountry2").val("");
    //            $("#BICCode2Name").val("");
    //            if (this.value() && this.selectedIndex == -1) {
    //                var dt = this.dataSource._data[0];
    //                this.text('');
    //            }
    //            else if ($("#NamaBank2").val() != "") {
    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank2").val(),
    //                    type: 'GET',
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {
    //                        $("#BIMemberCode2").val(data.BICode);
    //                        $("#BankCountry2Desc").val(data.CountryDesc);
    //                        $("#BICCode2Name").val(data.SInvestID);
    //                    },
    //                    error: function (data) {
    //                        alert(data.responseText);
    //                    }
    //                });
    //            }

    //        }
    //        function onChangeNamaBank3() {
    //            $("#BIMemberCode3").val("");
    //            $("#BankCountry3").val("");
    //            $("#BICCode3Name").val("");
    //            if (this.value() && this.selectedIndex == -1) {
    //                var dt = this.dataSource._data[0];
    //                this.text('');
    //            }
    //            else if ($("#NamaBank3").val() != "") {
    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank3").val(),
    //                    type: 'GET',
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {
    //                        $("#BIMemberCode3").val(data.BICode);
    //                        $("#BankCountry3Desc").val(data.CountryDesc);
    //                        $("#BICCode3Name").val(data.SInvestID);
    //                    },
    //                    error: function (data) {
    //                        alert(data.responseText);
    //                    }
    //                });
    //            }

    //        }
    //        function setCmbNamaBank1() {
    //            if (data.NamaBank1 == null) {
    //                return "";
    //            } else {
    //                if (data.NamaBank1 == 0) {
    //                    return "";
    //                } else {
    //                    return data.NamaBank1;
    //                }
    //            }
    //        }
    //        function setCmbNamaBank2() {
    //            if (data.NamaBank2 == null) {
    //                return "";
    //            } else {
    //                if (data.NamaBank2 == 0) {
    //                    return "";
    //                } else {
    //                    return data.NamaBank2;
    //                }
    //            }
    //        }
    //        function setCmbNamaBank3() {
    //            if (data.NamaBank3 == null) {
    //                return "";
    //            } else {
    //                if (data.NamaBank3 == 0) {
    //                    return "";
    //                } else {
    //                    return data.NamaBank3;
    //                }
    //            }
    //        }

    //    },
    //    error: function (data) {
    //        alertify.alert(data.responseText);
    //    }
    //});

    ////combo box MataUang
    //$.ajax({
    //    url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MataUang",
    //    type: 'GET',
    //    contentType: "application/json;charset=utf-8",
    //    success: function (data) {
    //        $("#MataUang1").kendoComboBox({
    //            dataValueField: "Code",
    //            dataTextField: "DescOne",
    //            filter: "contains",
    //            suggest: true,
    //            dataSource: data,
    //            //change: onChangeMataUang,
    //            value: setCmbMataUang1()
    //        });
    //        $("#MataUang2").kendoComboBox({
    //            dataValueField: "Code",
    //            dataTextField: "DescOne",
    //            filter: "contains",
    //            suggest: true,
    //            dataSource: data,
    //            //change: onChangeMataUang,
    //            value: setCmbMataUang2()
    //        });
    //        $("#MataUang3").kendoComboBox({
    //            dataValueField: "Code",
    //            dataTextField: "DescOne",
    //            filter: "contains",
    //            suggest: true,
    //            dataSource: data,
    //            //change: onChangeMataUang,
    //            value: setCmbMataUang3()
    //        });
    //        //$("#RDNCurrency").kendoComboBox({
    //        //    dataValueField: "Code",
    //        //    dataTextField: "DescOne",
    //        //    filter: "contains",
    //        //    suggest: true,
    //        //    dataSource: data,
    //        //    change: onChangeMataUang,
    //        //    value: setCmbRDNCurrency()
    //        //});
    //        function setCmbMataUang1() {
    //            if (data.MataUang1 == 4) {
    //                $("#lblOtherCurrency").show();
    //                $("#OtherCurrency").attr("required", true);
    //            } else {
    //                $("#lblOtherCurrency").hide();
    //                $("#OtherCurrency").attr("required", false);
    //            }

    //            if (data.MataUang1 == null) {
    //                return "";
    //            }
    //            else {
    //                if (data.MataUang1 == 0) {
    //                    return "";
    //                } else {
    //                    return data.MataUang1;
    //                }
    //            }
    //        }
    //        function setCmbMataUang2() {
    //            if (data.MataUang2 == null) {
    //                return "";
    //            } else {
    //                if (data.MataUang2 == 0) {
    //                    return "";
    //                } else {
    //                    return data.MataUang2;
    //                }
    //            }
    //        }
    //        function setCmbMataUang3() {
    //            if (data.MataUang3 == null) {
    //                return "";
    //            } else {
    //                if (data.MataUang3 == 0) {
    //                    return "";
    //                } else {
    //                    return data.MataUang3;
    //                }
    //            }
    //        }
    //    },
    //    error: function (data) {
    //        alertify.alert(data.responseText);
    //    }
    //});
    


    var GlobValidator = $("#WinInsuranceProduct").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {

            //if ($("#ID").val().length > 50) {
            //    alertify.alert("Validation not Pass, char more than 50 for ID");
            //    return 0;
            //}

            //if ($("#Name").val().length > 100) {
            //    alertify.alert("Validation not Pass, char more than 100 for Name");
            //    return 0;
            //}

            if ($("#Notes").val().length > 1000) {
                alertify.alert("Validation not Pass, char more than 1000 for Notes");
                return 0;
            }

            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        initListCountry();
        initListProvince();
        initListCity();

        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            $("#ID").attr('readonly', false);
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
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#ID").attr('readonly', true);
            }

            $("#InsuranceProductPK").val(dataItemX.FundClientPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);

            //$("#ID").val(kendo.toString(kendo.parseDate(dataItemX.Date), 'dd/MMM/yyyy'));
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#NomorRekening1").val(dataItemX.NomorRekening1);
            $("#NomorRekening2").val(dataItemX.NomorRekening2);
            $("#NomorRekening3").val(dataItemX.NomorRekening3);
            $("#MataUang1").val(dataItemX.MataUang1);
            $("#MataUang2").val(dataItemX.MataUang2);
            $("#MataUang3").val(dataItemX.MataUang3);
            $("#NamaBank1").val(dataItemX.NamaBank1);
            $("#NamaBank2").val(dataItemX.NamaBank2);
            $("#NamaBank3").val(dataItemX.NamaBank3);
            $("#NamaNasabah1").val(dataItemX.NamaNasabah1);
            $("#NamaNasabah2").val(dataItemX.NamaNasabah2);
            $("#NamaNasabah3").val(dataItemX.NamaNasabah3);
            $("#BankBranchName1").val(dataItemX.BankBranchName1);
            $("#BankBranchName2").val(dataItemX.BankBranchName2);
            $("#BankBranchName3").val(dataItemX.BankBranchName3);
            $("#RemarkBank1").val(dataItemX.RemarkBank1);
            $("#RemarkBank2").val(dataItemX.RemarkBank2);
            $("#RemarkBank3").val(dataItemX.RemarkBank3);

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
       
        //combo box Bank
        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#NamaBank1").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeNamaBank1,
                    value: setCmbNamaBank1()
                });
                $("#NamaBank2").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeNamaBank2,
                    value: setCmbNamaBank2()
                });
                $("#NamaBank3").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeNamaBank3,
                    value: setCmbNamaBank3()
                });

                function onChangeNamaBank1() {
                    $("#BIMemberCode1").val("");
                    $("#BICCode1Name").val("");
                    $("#BankCountry1").val("");
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    else if ($("#NamaBank1").val() != "") {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank1").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BIMemberCode1").val(data.BICode);
                                $("#BankCountry1Desc").val(data.CountryDesc);
                                $("#BICCode1Name").val(data.SInvestID);
                            },
                            error: function (data) {
                                alert(data.responseText);
                            }
                        });
                    }

                }
                function onChangeNamaBank2() {
                    $("#BIMemberCode2").val("");
                    $("#BankCountry2").val("");
                    $("#BICCode2Name").val("");
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    else if ($("#NamaBank2").val() != "") {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank2").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BIMemberCode2").val(data.BICode);
                                $("#BankCountry2Desc").val(data.CountryDesc);
                                $("#BICCode2Name").val(data.SInvestID);
                            },
                            error: function (data) {
                                alert(data.responseText);
                            }
                        });
                    }

                }
                function onChangeNamaBank3() {
                    $("#BIMemberCode3").val("");
                    $("#BankCountry3").val("");
                    $("#BICCode3Name").val("");
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    else if ($("#NamaBank3").val() != "") {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank3").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BIMemberCode3").val(data.BICode);
                                $("#BankCountry3Desc").val(data.CountryDesc);
                                $("#BICCode3Name").val(data.SInvestID);
                            },
                            error: function (data) {
                                alert(data.responseText);
                            }
                        });
                    }

                }
                function setCmbNamaBank1() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.NamaBank1;
                    }
                }
                function setCmbNamaBank2() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.NamaBank2;
                    }
                }
                function setCmbNamaBank3() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.NamaBank3;
                    }
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //combo box MataUang
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MataUang",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MataUang1").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    //change: onChangeMataUang,
                    value: setCmbMataUang1()
                });
                $("#MataUang2").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    //change: onChangeMataUang,
                    value: setCmbMataUang2()
                });
                $("#MataUang3").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    //change: onChangeMataUang,
                    value: setCmbMataUang3()
                });
                //$("#RDNCurrency").kendoComboBox({
                //    dataValueField: "Code",
                //    dataTextField: "DescOne",
                //    filter: "contains",
                //    suggest: true,
                //    dataSource: data,
                //    change: onChangeMataUang,
                //    value: setCmbRDNCurrency()
                //});
                function setCmbMataUang1() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.MataUang1;
                    }
                }
                function setCmbMataUang2() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.MataUang2;
                    }
                }
                function setCmbMataUang3() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.MataUang3;
                    }
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        



        win.center();
        win.open();
    }
    //Combo Box ListCountry
    function initListCountry() {
        var dsListCountry = getDataSourceListCountry();
        $("#gridListCountry").empty();
        $("#gridListCountry").kendoGrid({
            dataSource: dsListCountry,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
                //{ command: { text: "Select"}, title: " ", width: 60 },
                { command: { text: "Select", click: ListCountrySelect }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "Country", width: 200 }
            ]
        });
    }

    function initListProvince() {
        var dsListProvince = getDataSourceListProvince();
        $("#gridListProvince").empty();
        $("#gridListProvince").kendoGrid({
            dataSource: dsListProvince,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
                { command: { text: "Select", click: ListProvinceSelect }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "Province", width: 200 }
            ]
        });
    }

    function initListCity() {
        var dsListCity = getDataSourceListCity();
        $("#gridListCity").empty();
        $("#gridListCity").kendoGrid({
            dataSource: dsListCity,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },

            columns: [
                { command: { text: "Select", click: ListCitySelect }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "City", width: 200 }
            ]
        });
    }


    function ListCountrySelect(e) {
        e.preventDefault();
        var grid = $("#gridListCountry").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCountryDesc).val(dataItemX.DescOne);
        $(htmlCountry).val(dataItemX.Code);
        WinListCountry.close();
    }

    function ListProvinceSelect(e) {
        e.preventDefault();
        var grid = $("#gridListProvince").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlProvinceDesc).val(dataItemX.DescOne);
        $(htmlProvince).val(dataItemX.Code);
        WinListProvince.close();

    }

    function ListCitySelect(e) {
        e.preventDefault();
        var grid = $("#gridListCity").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCityDesc).val(dataItemX.DescOne);
        $(htmlCityPK).val(dataItemX.Code);

        WinListCity.close();

    }


    function getDataSourceListCity() {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CityRHB",
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 25,
                schema: {
                    model: {
                        fields: {
                            Code: { type: "string" },
                            DescOne: { type: "string" },

                        }
                    }
                }
            });
    }

    function getDataSourceListProvince() {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDIProvince",
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 25,
                schema: {
                    model: {
                        fields: {
                            Code: { type: "string" },
                            DescOne: { type: "string" },

                        }
                    }
                }
            });
    }

    function getDataSourceListCountry() {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 25,
                schema: {
                    model: {
                        fields: {
                            Code: { type: "string" },
                            DescOne: { type: "string" },

                        }
                    }
                }
            });
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#InsuranceProductPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#NamaBank1").val("");
        $("#NamaBank2").val("");
        $("#NamaBank3").val("");
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
                            InsuranceProductPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            NamaBank1: { type: "number" },
                            NamaBank2: { type: "number" },
                            NamaBank3: { type: "number" },
                            NomorRekening1: { type: "number" },
                            NomorRekening2: { type: "number" },
                            NomorRekening3: { type: "number" },
                            BankBranchName1: { type: "string" },
                            BankBranchName2: { type: "string" },
                            BankBranchName3: { type: "string" },
                            NamaNasabah1: { type: "string" },
                            NamaNasabah2: { type: "string" },
                            NamaNasabah3: { type: "string" },
                            RemarkBank1: { type: "string" },
                            RemarkBank2: { type: "string" },
                            RemarkBank3: { type: "string" },

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
            var gridApproved = $("#gridInsuranceProductApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridInsuranceProductPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridInsuranceProductHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var InsuranceProductApprovedURL = window.location.origin + "/Radsoft/InsuranceProduct/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(InsuranceProductApprovedURL);

        $("#gridInsuranceProductApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form InsuranceProduct"
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
                { field: "FundClientPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                //{ field: "Date", title: "Date", width: 200 },
                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 200 },
                { field: "NamaBank1Desc", title: "NamaBank1", width: 200 },
                { field: "NamaBank2Desc", title: "NamaBank2", width: 200 },
                { field: "NamaBank3Desc", title: "NamaBank3", width: 300 },
                { field: "NamaNasabah1", title: "NamaNasabah1", width: 200 },
                { field: "NamaNasabah2", title: "NamaNasabah2", width: 200 },
                { field: "NamaNasabah3", title: "NamaNasabah3", width: 300 },
                { field: "MataUang1Desc", title: "MataUang1", width: 200 },
                { field: "MataUang2Desc", title: "MataUang2", width: 200 },
                { field: "MataUang3Desc", title: "MataUang3", width: 300 },
                { field: "NomorRekening1", title: "NomorRekening1", width: 200 },
                { field: "NomorRekening2", title: "NomorRekening2", width: 200 },
                { field: "NomorRekening3", title: "NomorRekening3", width: 300 },
                { field: "BankBranchName1", title: "BankBranchName1", width: 300 },
                { field: "BankBranchName2", title: "BankBranchName2", width: 300 },
                { field: "BankBranchName3", title: "BankBranchName3", width: 300 },
                { field: "RemarkBank1", title: "RemarkBank1", width: 300 },
                { field: "RemarkBank2", title: "RemarkBank2", width: 300 },
                { field: "RemarkBank3", title: "RemarkBank3", width: 300 },

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
        $("#TabInsuranceProduct").kendoTabStrip({
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
                        RecalGridPending()
                    }
                    if (tabindex == 2) {
                        RecalGridHistory()
                    }
                } else {
                    refresh();
                }
            }
        });


    }

    function RecalGridPending() {
        $("#gridInsuranceProductPending").empty();
        var _search = "";
        var _selectsearch = "";
        if ($("#search").val() == "") {
            _search = "all";
            //alert(dataItemX.KodeDomisiliPropinsi);
        }
        else {
            _search = $("#search").val();
        }

        if ($("#searchGridData").val() == "") {
            _selectsearch = "all";
        }
        else {
            _selectsearch = $("#searchGridData").val();
        }


        //if (GlobUsersClientMode == 1) {
        var InsuranceProductPendingURL = window.location.origin + "/Radsoft/InsuranceProduct/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
            dataSourcePending = getDataSource(InsuranceProductPendingURL);
        //}

        //else {
        //    var FundClientPendingURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResultViewOnAgentUsers/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + _search,
        //    dataSourcePending = getDataSource(FundClientPendingURL);
        //}
        
            $("#gridInsuranceProductPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund Client"
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
                { field: "FundClientPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                //{ field: "Date", title: "Date", width: 200 },
                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 200 },
                { field: "NamaBank1Desc", title: "NamaBank1", width: 200 },
                { field: "NamaBank2Desc", title: "NamaBank2", width: 200 },
                { field: "NamaBank3Desc", title: "NamaBank3", width: 300 },
                { field: "NamaNasabah1", title: "NamaNasabah1", width: 200 },
                { field: "NamaNasabah2", title: "NamaNasabah2", width: 200 },
                { field: "NamaNasabah3", title: "NamaNasabah3", width: 300 },
                { field: "MataUang1Desc", title: "MataUang1", width: 200 },
                { field: "MataUang2Desc", title: "MataUang2", width: 200 },
                { field: "MataUang3Desc", title: "MataUang3", width: 300 },
                { field: "NomorRekening1", title: "NomorRekening1", width: 200 },
                { field: "NomorRekening2", title: "NomorRekening2", width: 200 },
                { field: "NomorRekening3", title: "NomorRekening3", width: 300 },
                { field: "BankBranchName1", title: "BankBranchName1", width: 300 },
                { field: "BankBranchName2", title: "BankBranchName2", width: 300 },
                { field: "BankBranchName3", title: "BankBranchName3", width: 300 },
                { field: "RemarkBank1", title: "RemarkBank1", width: 300 },
                { field: "RemarkBank2", title: "RemarkBank2", width: 300 },
                { field: "RemarkBank3", title: "RemarkBank3", width: 300 },

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

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridInsuranceProductPending").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourcePending.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end filter


    }

    function RecalGridHistory() {
        $("#gridInsuranceProductHistory").empty();
        var _search = "";
        var _selectsearch = "";
        if ($("#search").val() == "") {
            _search = "all";
        }
        else {
            _search = $("#search").val();
        }

        if ($("#searchGridData").val() == "") {
            _selectsearch = "all";
        }
        else {
            _selectsearch = $("#searchGridData").val();
        }

        //if (GlobUsersClientMode == 1) {
        var InsuranceProductHistoryURL = window.location.origin + "/Radsoft/InsuranceProduct/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
            dataSourceHistory = getDataSource(InsuranceProductHistoryURL);
        //}
        //else
        //{
        //    var FundClientHistoryURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResultViewOnAgentUsers/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + _search,
        //    dataSourceHistory = getDataSourceSearch(FundClientHistoryURL);
        //}

            $("#gridInsuranceProductHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund Client"
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
                { field: "FundClientPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                //{ field: "Date", title: "Date", width: 200 },
                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 200 },
                { field: "NamaBank1Desc", title: "NamaBank1", width: 200 },
                { field: "NamaBank2Desc", title: "NamaBank2", width: 200 },
                { field: "NamaBank3Desc", title: "NamaBank3", width: 300 },
                { field: "NamaNasabah1", title: "NamaNasabah1", width: 200 },
                { field: "NamaNasabah2", title: "NamaNasabah2", width: 200 },
                { field: "NamaNasabah3", title: "NamaNasabah3", width: 300 },
                { field: "MataUang1Desc", title: "MataUang1", width: 200 },
                { field: "MataUang2Desc", title: "MataUang2", width: 200 },
                { field: "MataUang3Desc", title: "MataUang3", width: 300 },
                { field: "NomorRekening1", title: "NomorRekening1", width: 200 },
                { field: "NomorRekening2", title: "NomorRekening2", width: 200 },
                { field: "NomorRekening3", title: "NomorRekening3", width: 300 },
                { field: "BankBranchName1", title: "BankBranchName1", width: 300 },
                { field: "BankBranchName2", title: "BankBranchName2", width: 300 },
                { field: "BankBranchName3", title: "BankBranchName3", width: 300 },
                { field: "RemarkBank1", title: "RemarkBank1", width: 300 },
                { field: "RemarkBank2", title: "RemarkBank2", width: 300 },
                { field: "RemarkBank3", title: "RemarkBank3", width: 300 },

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


        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridInsuranceProductHistory").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourceHistory.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end filter


    }


    function gridHistoryDataBound() {
        var grid = $("#gridInsuranceProductHistory").data("kendoGrid");
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
        //$("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var InsuranceProduct = {
                        ID: $('#ID').val(),
                        Name: $('#Name').val(),
                        NomorRekening1: $('#NomorRekening1').val(),
                        NomorRekening2: $('#NomorRekening2').val(),
                        NomorRekening3: $('#NomorRekening3').val(),
                        MataUang1: $('#MataUang1').val(),
                        MataUang2: $('#MataUang2').val(),
                        MataUang3: $('#MataUang3').val(),
                        NamaBank1: $('#NamaBank1').val(),
                        NamaBank2: $('#NamaBank2').val(),
                        NamaBank3: $('#NamaBank3').val(),
                        NamaNasabah1: $('#NamaNasabah1').val(),
                        NamaNasabah2: $('#NamaNasabah2').val(),
                        NamaNasabah3: $('#NamaNasabah3').val(),
                        BankBranchName1: $('#BankBranchName1').val(),
                        BankBranchName2: $('#BankBranchName2').val(),
                        BankBranchName3: $('#BankBranchName3').val(),
                        RemarkBank1: $('#RemarkBank1').val(),
                        RemarkBank2: $('#RemarkBank2').val(),
                        RemarkBank3: $('#RemarkBank3').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/InsuranceProduct/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")+ "/" + "InsuranceProduct_I", 
                        type: 'POST',
                        data: JSON.stringify(InsuranceProduct),
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
                       
            });
            
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InsuranceProductPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var InsuranceProduct = {
                                    FundClientPK: $('#InsuranceProductPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    NomorRekening1: $('#NomorRekening1').val(),
                                    NomorRekening2: $('#NomorRekening2').val(),
                                    NomorRekening3: $('#NomorRekening3').val(),
                                    MataUang1: $('#MataUang1').val(),
                                    MataUang2: $('#MataUang2').val(),
                                    MataUang3: $('#MataUang3').val(),
                                    NamaBank1: $('#NamaBank1').val(),
                                    NamaBank2: $('#NamaBank2').val(),
                                    NamaBank3: $('#NamaBank3').val(),
                                    NamaNasabah1: $('#NamaNasabah1').val(),
                                    NamaNasabah2: $('#NamaNasabah2').val(),
                                    NamaNasabah3: $('#NamaNasabah3').val(),
                                    BankBranchName1: $('#BankBranchName1').val(),
                                    BankBranchName2: $('#BankBranchName2').val(),
                                    BankBranchName3: $('#BankBranchName3').val(),
                                    RemarkBank1: $('#RemarkBank1').val(),
                                    RemarkBank2: $('#RemarkBank2').val(),
                                    RemarkBank3: $('#RemarkBank3').val(), 
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/InsuranceProduct/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InsuranceProduct_U",
                                    type: 'POST',
                                    data: JSON.stringify(InsuranceProduct),
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
        }
    });

    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InsuranceProductPK").val() + "/" + $("#HistoryPK").val() + "/" + "InsuranceProduct",
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
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "InsuranceProduct" + "/" + $("#InsuranceProductPK").val(),
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
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InsuranceProductPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var InsuranceProduct = {
                                FundClientPK: $('#InsuranceProductPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/InsuranceProduct/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InsuranceProduct_A",
                                type: 'POST',
                                data: JSON.stringify(InsuranceProduct),
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

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InsuranceProductPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var InsuranceProduct = {
                                FundClientPK: $('#InsuranceProductPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/InsuranceProduct/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InsuranceProduct_V",
                                type: 'POST',
                                data: JSON.stringify(InsuranceProduct),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InsuranceProductPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var InsuranceProduct = {
                                FundClientPK: $('#InsuranceProductPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/InsuranceProduct/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InsuranceProduct_R",
                                type: 'POST',
                                data: JSON.stringify(InsuranceProduct),
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