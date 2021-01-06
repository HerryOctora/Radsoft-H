$(document).ready(function () {
    document.title = 'FORM RETRIEVE MKBD DAILY';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var GlobLastMonth;

    //1
    initButton();
    //2
    initWindow();
    //3
    refresh();

    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        //$("#BtnRetrieveAUM").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});

        //$("#BtnGenerateRevaluation").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});

        //$("#BtnRetrieveClosePriceReksadana").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});
        //$("#BtnRetrieveMFee").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});
    }

    function initWindow() {

        if (_GlobClientCode == "17") {
            $("#lblCheckAgentCommission").show();
            $("#lblRetrieveAgentComm").show();
            
    
        }
        else
        {
            $("#lblCheckAgentCommission").hide();
            $("#lblRetrieveAgentComm").hide();
        }



        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
            value: new Date(),
        });
 

        function OnChangeDate() {
            _ChecklastMonth($("#Date").data("kendoDatePicker").value());
            refresh();
        }


        _ChecklastMonth($("#Date").data("kendoDatePicker").value());

        function _ChecklastMonth(_date) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/GetEndOfMonth/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString(_date, "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    GlobLastMonth = data;
                    _thisDate = kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yyyy");

                    if (_GlobClientCode == "03" && (_thisDate == data)) {
                        $("#lblCheckAgentCSR").show();
                        $("#lblPostingAgentCSR").show();
                    }
                    else {
                        $("#lblCheckAgentCSR").hide();
                        $("#lblPostingAgentCSR").hide();
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
       

        WinRetrieveMKBDDaily = $("#WinRetrieveMKBDDaily").kendoWindow({
            height: 550,
            title: "* RETRIEVE MKBD DATA",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinRetrieveMKBDDaily.center();
        WinRetrieveMKBDDaily.open();

    }

    function refresh() {
        $("#CheckAUM").val("");
        $("#CheckClosePrice").val("");
        $("#CheckRevaluation").val("");
        $("#CheckManagementFee").val("");
        $("#CheckAgentCommission").val("");
        $("#CheckAgentCSR").val("");

        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckAUM/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckAUM").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckClosePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckClosePrice").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckRevaluation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/PortfolioRevaluation",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckRevaluation").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/ManagementFee",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckManagementFee").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckAgentCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/AgentCommission",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckAgentCommission").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/ValidateCheckAgentCSR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/AgentCorporateSocialResponsibilities",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CheckAgentCSR").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



    }
  

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnRetrieveAUM").click(function () {
        if ($("#CheckAUM").val() != "Ready For Retrieve AUM")
        {
            alertify.alert("Can't Process Data, Please Check Status CheckAUM First!");
            return;
        }

        alertify.confirm("Are you sure want to Retrieve AUM ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/RetrieveAUM/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });
    
    $("#BtnRetrieveClosePriceReksadana").click(function () {
        if ($("#CheckClosePrice").val() != "Ready For Retrieve Close Price Reksadana") {
            alertify.alert("Can't Process Data, Please Check Status CheckClosePrice First!");
            return;
        }

        alertify.confirm("Are you sure want to Retrieve ClosePrice Reksadana ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/RetrieveClosePriceReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });


            }
        });
    });

    $("#BtnGenerateRevaluation").click(function () {
        if ($("#CheckRevaluation").val() != "Ready For Generate Revaluation") {
            alertify.alert("Can't Process Data, Please Check Status CheckRevaluation First!");
            return;
        }
        alertify.confirm("Are you sure want to Generate Portfolio Revaluation ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/GeneratePortfolioRevaluation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnRetrieveMFee").click(function () {
        if (_GlobClientCode != "21") {
            if ($("#CheckManagementFee").val() != "Ready For Generate Management Fee") {
                alertify.alert("Can't Process Data, Please Check Status CheckManagementFee First!");
                return;
            }
        }


        alertify.confirm("Are you sure want to Retrieve Management Fee ?", function (e) {
            if (e) {


                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/RetrieveManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnVoidAUM").click(function () {
        if ($("#CheckAUM").val() != "DONE") {
            alertify.alert("Can't Process Data, Check AUM Must Be DONE First!");
            return;
        }

        alertify.confirm("Are you sure want to Retrieve AUM ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/VoidAUM/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnVoidClosePriceReksadana").click(function () {
        if ($("#CheckClosePrice").val() != "DONE") {
            alertify.alert("Can't Process Data, Check Close Price Reksadana Must Be DONE First!");
            return;
        }

        alertify.confirm("Are you sure want to Retrieve ClosePrice Reksadana ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/VoidClosePriceReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });


            }
        });
    });

    $("#BtnVoidRevaluation").click(function () {
        if ($("#CheckRevaluation").val() != "DONE") {
            alertify.alert("Can't Process Data, Check Revaluation Must Be DONE First!");
            return;
        }
        alertify.confirm("Are you sure want to Generate Portfolio Revaluation ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/VoidPortfolioRevaluation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnVoidMFee").click(function () {
        if ($("#CheckManagementFee").val() != "DONE") {
            alertify.alert("Can't Process Data, Check Management Fee Must Be DONE First!");
            return;
        }
        alertify.confirm("Are you sure want to Retrieve Management Fee ?", function (e) {
            if (e) {


                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/VoidManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnRetrieveAgentComm").click(function () {
            alertify.confirm("Are you sure want to Retrieve Agent Commission ?", function (e) {
                if (e) {


                    $.ajax({
                        url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/RetrieveAgentCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            alertify.alert(data)
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            });

    });
 
    $("#BtnPostingAgentComm").click(function () {
        if ($("#CheckAgentCommission").val() == "Ready For Posting Agent Commission" || $("#CheckAgentCommission").val() == "DONE") {
            alertify.confirm("Are you sure want to Posting Agent Commission ?", function (e) {
                if (e) {


                    $.ajax({
                        url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/PostingAgentCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            alertify.alert(data)
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            });
        }
        else
        {
            alertify.alert("Can't Process Data, Check Agent Commission Must Be Ready First!");
            return;
        }
     
    });

    $("#BtnPostingAgentCSR").click(function () {
        if ($("#CheckAgentCSR").val() == "Ready For Posting Agent CSR" || $("#CheckAgentCSR").val() == "DONE") {
            alertify.confirm("Are you sure want to Posting Agent Commission ?", function (e) {
                if (e) {


                    $.ajax({
                        url: window.location.origin + "/Radsoft/RetrieveMKBDDaily/PostingAgentCSR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            alertify.alert(data)
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            });
        }
        else {
            alertify.alert("Can't Process Data, Check Agent CSR Must Be Ready First!");
            return;
        }

    });




});
