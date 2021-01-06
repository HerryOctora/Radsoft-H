$(document).ready(function () {
    document.title = 'HOME PAGE';
    
    //Global Variabel
    var win;
    var gridHeight = screen.height;
    
    //1
    initButton();

    //2
    initWindow();

    //3
    initDailyTransactions();

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
        $("#FilterDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date(),
            change: OnChangeFilterDate
        });
        function OnChangeFilterDate() {
            var _date = Date.parse($("#FilterDate").data("kendoDatePicker").value());

            // Check if date parse is successful
            if (!_date) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            } else {
                // Get Data Daily Transactions
                resetData();
                initDailyTransactions();
            
            }
        }
    }

    function initDailyTransactions() {
        var _date = $("#FilterDate").data("kendoDatePicker").value();

        // Set Filter Date
        if (_date == undefined || _date == "" || _date == null) {
            _date = new Date();
        }

        // Get Data Daily Transactions
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetDailyTransactionsByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString(_date, "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                resetData();
                
                $("#EndDayTrails").val(data[0].EndDayTrails);
                $("#LastEndDayTrails").val(data[0].LastEndDayTrails);
                $("#FundPosition").val(data[0].FundPosition);
                $("#LastFundPosition").val(data[0].LastEndDayFundPosition);
                $("#NAV").val(data[0].NAV);
                $("#LastCloseNAV").val(data[0].LastCloseNAV);
                $("#ClosePrice").val(data[0].ClosePrice);
                $("#LastClosePrice").val(data[0].LastClosePrice);
                $("#ClientSubscription").val(data[0].ClientSubscription);
                $("#ClientRedemption").val(data[0].ClientRedemption);
                $("#ClientSwitching").val(data[0].ClientSwitching);
                $("#OMSTD").val(data[0].OMSTD);
                $("#OMSEquity").val(data[0].OMSEquity);
                $("#OMSBond").val(data[0].OMSBond);
                $("#Dealing").val(data[0].Dealing);
                $("#Settlement").val(data[0].Settlement);
            },
            error: function (data) {
                alertify.alert(data.responseText);
                resetData();
                return;
            }
        });
    }

    function resetData() {
        //$("#FilterDate").data("kendoDatePicker").value(new Date());
        $("#EndDayTrails").val("");
        $("#FundPosition").val("");
        $("#NAV").val("");
        $("#ClosePrice").val("");
        $("#ClientSubscription").val("");
        $("#ClientRedemption").val("");
        $("#ClientSwitching").val("");
        $("#OMSTD").val("");
        $("#OMSEquity").val("");
        $("#OMSBond").val("");
        $("#Dealing").val("");
        $("#Settlement").val("");
        $("#LastEndDayTrails").val("");
        $("#LastFundPosition").val("");
        $("#LastCloseNAV").val("");
        $("#LastClosePrice").val("");
    }

    $("#BtnReload").click(function () {
        initDailyTransactions();
    });

    $("#BtnClear").click(function () {
        //$("#FilterDate").data("kendoDatePicker").value(new Date());
        resetData();
    });

    $("#BtnGoToFormEndDayTrails").click(function () {
        window.open(window.location.origin + "/WEB/Accounting/EndDayTrails.html", '_blank');
    });

    $("#BtnGoToFormFundPosition").click(function () {
        window.open(window.location.origin + "/WEB/Investment/EndDayTrailsFundPortfolio.html", '_blank');
    });

    $("#BtnGoToFormNAV").click(function () {
        window.open(window.location.origin + "/WEB/Masters/CloseNav.html", '_blank');
    });

    $("#BtnGoToFormClosePrice").click(function () {
        window.open(window.location.origin + "/WEB/Masters/ClosePrice.html", '_blank');
    });

    $("#BtnGoToFormClientSubscription").click(function () {
        window.open(window.location.origin + "/WEB/UnitRegistry/ClientSubscription.html", '_blank');
    });

    $("#BtnGoToFormClientRedemption").click(function () {
        window.open(window.location.origin + "/WEB/UnitRegistry/ClientRedemption.html", '_blank');
    });

    $("#BtnGoToFormClientSwitching").click(function () {
        window.open(window.location.origin + "/WEB/UnitRegistry/ClientSwitching.html", '_blank');
    });

    $("#BtnGoToFormOMSTD").click(function () {
        window.open(window.location.origin + "/WEB/Investment/OMSTimeDeposit.html", '_blank');
    });

    $("#BtnGoToFormOMSEquity").click(function () {
        window.open(window.location.origin + "/WEB/Investment/OMSEquity.html", '_blank');
    });

    $("#BtnGoToFormOMSBond").click(function () {
        window.open(window.location.origin + "/WEB/Investment/OMSBond.html", '_blank');
    });

    $("#BtnGoToFormDealing").click(function () {
        window.open(window.location.origin + "/WEB/Investment/DealingInstruction.html", '_blank');
    });

    $("#BtnGoToFormSettlement").click(function () {
        window.open(window.location.origin + "/WEB/Investment/SettlementInstruction.html", '_blank');
    });

});
