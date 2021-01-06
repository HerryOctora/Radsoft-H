$(document).ready(function () {
    //Open Menu
    
    var gridHeight = screen.height;

    $('#Frame').on('load', function () {
        self.scrollTo(50, 0);
    });
    $("#Frame").attr("src", "../../WEB/HomePage.html");
    $(document).bind('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 96) {
            WinHomeNavigation.open();
        }
    });

    InitMenu();

    var _hasToChangePassword = false;

    $("#StripeUser").text(sessionStorage.getItem("user"));
    CheckChangePassword();

    WinHomeNavigation = $("#WinHomeNavigation").kendoWindow({
        height: 1000,
        title: "Navigation",
        visible: false,
        width: 820,
        resizable:false,
        open: function (e) {
            this.wrapper.css({ top: 50, left: 5 })
            
        },
    }).data("kendoWindow");

    WinUsersDetail = $("#WinUsersDetail").kendoWindow({
        height: 300,
        title: "* User Access Trail",
        visible: false,
        width: 600,
        open: function (e) {
            this.wrapper.css({ top: 150 })
        },
        modal: true
    }).data("kendoWindow");

    WinLockScreen = $("#LockScreen").kendoWindow({
        height: screen.height - 300,
        width: screen.width - 50,
        title: "Screen Locked",
        visible: false,
        draggable: false,
        open: function (e) {
            this.wrapper.css({ top: 100 })
        },
        close: function (e) {
            if (sessionStorage.getItem("id") == "") {
                window.parent.location.href = window.location.origin + "/WEB/Login.html";
            }
        },
        modal: true
    }).data("kendoWindow");

    WinChangePassword = $("#ChangePasswordScreen").kendoWindow({
        height: 400,
        width: 600,
        title: "Change Password",
        visible: false,
        draggable: false,
        open: function (e) {
            this.wrapper.css({ top: 100 })
        },
        close: function (e) {
            if (_hasToChangePassword == true) {
                e.preventDefault();
                alertify.alert("You Must Change Your Password");
            }
        },
        modal: true
    }).data("kendoWindow");

    $("#HomeChangePassword").click(function () {
        self.scrollTo(0, 0);
        WinChangePassword.open();
        WinChangePassword.center();
        $("#ChangePassword").focus();
        $("#BtnCancel").kendoButton({
            enable: true
        });
    });

    $("#HomeMenu").click(function () {
        self.scrollTo(0, 0);
        WinHomeNavigation.open();
        $("#Frame").attr("src", "");
    });

    $("#HomePage").click(function () {
        self.scrollTo(0, 0);
        WinHomeNavigation.close();
        $("#Frame").attr("src", "../../WEB/HomePage.html");
    });

    function CheckChangePassword() {
        if (sessionStorage.getItem("id") != "") {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/CheckChangePassword/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        _hasToChangePassword = true;
                        WinChangePassword.open();
                        WinChangePassword.center();
                        $("#ChangePassword").focus();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    }

    $("#BtnChangePassword").click(function () {        
        if ($("#ChangePassword").val() == null || $("#ChangePassword").val() == "") {
            alertify.alert("Please input your new Password");
        } else {
            alertify.confirm("Are you sure want to Change your password?", function (e) {
                if (e) {
                    var ChangePassword = {
                        NewPassword: $("#ChangePassword").val()
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/ChangePassword/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ChangePassword),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "Success Change Password") {
                                _hasToChangePassword = false;
                                alertify.alert(data);
                                $("#ChangePassword").text("");
                                $("#ChangePassword").val("");
                                WinChangePassword.close();
                            } else {
                                alertify.alert(data);
                                $("#ChangePassword").text("");
                                $("#ChangePassword").val("");
                            }
                            
                            
                            
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $("#ChangePassword").text("");
                            $("#ChangePassword").val("");
                            WinChangePassword.close();
                        }
                    });
                }
            });
        }
    });

    $("#HomeLockScreen").click(function () {
        self.scrollTo(0, 0);
        WinLockScreen.open();
        WinLockScreen.center();
        sessionStorage.id = "";
        $("#unlockPassword").focus();
    });

    $("#BtnCancel").click(function () {
        WinChangePassword.close();
    });

    $("#BtnUnlock").click(function () {
        
        if ($("#unlockPassword").val() == null || $("#unlockPassword").val() == "") {
            alertify.alert("Please Input Unlock Password");
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/UnlockScreen/" + sessionStorage.getItem("user") + "/" + $("#unlockPassword").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.alert("Wrong Password");
                    } else {
                        sessionStorage.id = data;
                        $("#unlockPassword").text("");
                        $("#unlockPassword").val("");
                        WinLockScreen.close();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    });

    $("#HomeLogout").click(function () {
        
        alertify.confirm("Are you sure want to Log out?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + '/Radsoft/UsersAccessTrail/UsersAccessTrail_Logout/' + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        sessionStorage.clear();
                        window.location = window.location.origin + "/WEB/Login.html";
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#StripeUser").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/UsersAccessTrail/GetUserAccessTrailByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + sessionStorage.getItem("user"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#LoginSuccessTimeLast").val(data.LoginSuccessTimeLast);
                $("#LoginFailTimeLast").val(data.LoginFailTimeLast);
                $("#LogoutTimeLast").val(data.LogoutTimeLast);
                $("#ChangePasswordTimeLast").val(data.ChangePasswordTimeLast);
                $("#LoginSuccessFreq").val(data.LoginSuccessFreq);
                $("#LoginFailFreq").val(data.LoginFailFreq);
                $("#LogoutFreq").val(data.LogoutFreq);
                $("#ChangePasswordFreq").val(data.ChangePasswordFreq);
                WinUsersDetail.center();
                WinUsersDetail.open();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    });
    
    $("#RLogo").click(function () {
        WinHomeNavigation.open();
    });

    $("#btnOpenForm").click(function () {
        OpenForm($("#MenuSearch").data("kendoComboBox").value());

    });

    $("#HomeLoginTrail").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/UsersAccessTrail/GetUserAccessTrailByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + sessionStorage.getItem("user"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#LoginSuccessTimeLast").val(data.LoginSuccessTimeLast);
                $("#LoginFailTimeLast").val(data.LoginFailTimeLast);
                $("#LogoutTimeLast").val(data.LogoutTimeLast);
                $("#ChangePasswordTimeLast").val(data.ChangePasswordTimeLast);
                $("#LoginSuccessFreq").val(data.LoginSuccessFreq);
                $("#LoginFailFreq").val(data.LoginFailFreq);
                $("#LogoutFreq").val(data.LogoutFreq);
                $("#ChangePasswordFreq").val(data.ChangePasswordFreq);
                WinUsersDetail.center();
                WinUsersDetail.open();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    });

    $("#Menu").kendoMenu({
        select: onMenu_Selected,
        orientation: "vertical"
    }).css({
        width: "150px"
    });
    
    $("#MenuSearch").kendoComboBox({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "Account", value: "Account" },
            { text: "Account Budget", value: "AccountBudget" },
            //{ text: "Accounting Report Template", value: "AccountingReportTemplate" },
            { text: "Accounting Rpt", value: "AccountingRpt" },
            { text: "Accounting Setup", value: "AccountingSetup" },
            { text: "Account Ledger Balance", value: "AccountLedgerBalance" },
            { text: "Activity", value: "Activity" },
            { text: "Agent/Direct", value: "Agent" },
            //{ text: "Agent Fee Setup", value: "AgentFeeSetup" },
            { text: "Agent Fund Position", value: "AgentFundPosition" },
            { text: "Agent Users", value: "AgentUsers" },
            { text: "AUM", value: "AUM" },
            { text: "Bank", value: "Bank" },
            { text: "Bank Branch", value: "BankBranch" },
            { text: "Benchmark Index", value: "BenchmarkIndex" },
            { text: "BlackList Name", value: "BlackListName" },
            { text: "Bloomberg Equity", value: "BloombergEquity" },
            { text: "Board", value: "Board" },
            { text: "Business Type", value: "BusinessType" },
            { text: "CAM Interface", value: "CAMInterface" },
            //{ text: "Category Scheme", value: "CategoryScheme" },
            //{ text: "Client Dashboard", value: "ClientDashboard" },
            { text: "Cashier Payment", value: "CashierPayment" },
            { text: "Cashier Receipt", value: "CashierReceipt" },
            { text: "Cash Ref", value: "CashRef" },
            { text: "Customer Service Book", value: "CustomerServiceBook" },
            { text: "Client Redemption", value: "ClientRedemption" },
            { text: "Client Subscription", value: "ClientSubscription" },
            { text: "Client Switching", value: "ClientSwitching" },
            { text: "Close Nav", value: "CloseNav" },
            { text: "Close Price", value: "ClosePrice" },
            { text: "Commission Rpt", value: "CommissionRpt" },
            { text: "Company", value: "Company" },
            { text: "Company Account Trading", value: "CompanyAccountTrading" },
            //{ text: "Company Position", value: "CompanyPosition" },
            { text: "Consignee", value: "Consignee" },
            { text: "Corporate Action", value: "CorporateAction" },
            //{ text: "Corporate Ar Ap Payment", value: "CorporateArApPayment" },
            { text: "Counterpart", value: "Counterpart" },
            { text: "Counterpart Commission", value: "CounterpartCommission" },
            { text: "Coupon Scheduler", value: "CouponScheduler" },
            { text: "Currency", value: "Currency" },
            { text: "Currency Rate", value: "CurrencyRate" },
            { text: "Custodian Journal Account Name Setup", value: "CustodianJournalAccountNameSetup" },
            //{ text: "Dashboard", value: "Dashboard" },
            { text: "Daily Data For Commission Rpt New Log", value: "DailyDataForCommissionRptNewLog" },
            { text: "Dealing Instruction", value: "DealingInstruction" },
            { text: "Department", value: "Department" },
            { text: "Deposito Type", value: "DepositoType" },
            { text: "Distributed Income", value: "DistributedIncome" },
            //{ text: "Dormant Account", value: "DormantAccount" },
            { text: "Download File From IDX", value: "DownloadFileFromIDX" },
            { text: "Employee", value: "Employee" },
            { text: "End Day Trails Journal and Unit", value: "EndDayTrails" },
            { text: "End Day Trails FundPortfolio", value: "EndDayTrailsFundPortfolio" },
            { text: "Exercise", value: "Exercise" },
            { text: "Exposure Monitoring", value: "ExposureMonitoring" },
            { text: "Finance Rpt", value: "FinanceRpt" },
            { text: "Fund Client Document", value: "FundClientDocument" },
            { text: "Finance Setup", value: "FinanceSetup" },
            { text: "FIFO Bond Position", value: "FIFOBondPosition" },
            { text: "Fixed Asset", value: "FixedAsset" },
            { text: "Fund Fact Sheet Rpt", value: "FundFactSheetRpt" },
            { text: "Fund", value: "Fund" },
            { text: "Fund Admin Rpt", value: "FundAccountingRpt" },
            { text: "Fund Accounting Setup", value: "FundAccountingSetup" },
            { text: "Fund Cash Ref", value: "FundCashRef" },
            { text: "Fund Client", value: "FundClient" },
            { text: "Fund Client Verification", value: "FundClientVerification" },
            { text: "Fund Client Cash Ref", value: "FundClientCashRef" },
            { text: "Fund Client Position", value: "FundClientPosition" },
            { text: "Fund Daily Fee Summary", value: "FundDailyFee" },
            { text: "Fund Exposure", value: "FundExposure" },
            { text: "Fund Fee", value: "FundFee" },
            { text: "Fund Index", value: "FundIndex" },
            { text: "Fund Journal", value: "FundJournal" },
            { text: "Fund Position Adjustment", value: "FundPositionAdjustment" },
            { text: "Fund Journal Account", value: "FundJournalAccount" },
            { text: "Fund Risk Profile", value: "FundRiskProfile" },
            { text: "Generate Rebate", value: "GenerateRebate" },
            { text: "Good Fund Reconcile", value: "GoodFundReconcile" },
            { text: "Corporate GOV AM", value: "CorporateGovAM" },
            { text: "Groups", value: "Groups" },
            { text: "Groups Roles", value: "GroupsRoles" },
            { text: "Groups Users", value: "GroupsUsers" },
            { text: "High Risk Monitoring", value: "HighRiskMonitoring" },
            { text: "High Risk Value Setup", value: "HighRiskValueSetup" },
            { text: "Holding", value: "Holding" },
            { text: "Holding Period", value: "HoldingPeriod" },
            { text: "Index", value: "Index" },
            { text: "Instrument", value: "Instrument" },
            { text: "Instrument Company Type", value: "InstrumentCompanyType" },
            { text: "Instrument Index", value: "InstrumentIndex" },
            { text: "Instrument Syariah", value: "InstrumentSyariah" },
            { text: "Instrument Type", value: "InstrumentType" },
            { text: "Internal Category", value: "InternalCategory" },
            { text: "Investment Instruction", value: "InvestmentInstruction" },
            { text: "Issuer", value: "Issuer" },
            { text: "Journal", value: "Journal" },
            { text: "Market", value: "Market" },
            { text: "Market Holiday", value: "MarketHoliday" },
            { text: "Manage Investment", value: "ManageInvestment" },
            { text: "Manage UR Transaction", value: "ManageURTransaction" },
            { text: "Master Value", value: "MasterValue" },
            { text: "MKBD Trails", value: "MKBDTrails" },
            { text: "Nav Mapping Report", value: "NavMappingReport" },
            { text: "NAWC Daily Process", value: "NAWCDailyProcess" },
            { text: "Notification", value: "Notification" },
            { text: "Office", value: "Office" },
            { text: "Compliance Rpt", value: "OjkRpt" },
            { text: "OMS Bond", value: "OMSBond" },
            { text: "OMS Equity", value: "OMSEquity" },
            { text: "OMS Time Deposit", value: "OMSTimeDeposit" },
            { text: "Period", value: "Period" },
            { text: "Permission", value: "Permission" },
            { text: "Portfolio Revaluation", value: "PortfolioRevaluation" },
            { text: "Prepaid", value: "Prepaid" },
            //{ text: "Rebate Fee Setup", value: "RebateFeeSetup" },
            { text: "Reguler Instruction", value: "RegulerInstruction" },
            { text: "Reksadana Type", value: "ReksadanaType" },
            { text: "Retrieve From Bridge", value: "RetrieveFromBridge" },
            { text: "Retrieve MKBD Daily", value: "RetrieveMKBDDaily" },
            { text: "Risk Profile Monitoring", value: "RiskProfileMonitoring" },
            { text: "Risk Profile Score", value: "RiskProfileScore" },
            { text: "Risk Questionnaire", value: "RiskQuestionnaire" },
            { text: "Risk Questionnaire Answer", value: "RiskQuestionnaireAnswer" },
            { text: "Roles", value: "Roles" },
            { text: "Roles Permission", value: "RolesPermission" },
            { text: "Roles Permission Notification", value: "RolesPermissionNotification" },
            { text: "Roles Users", value: "RolesUsers" },
            { text: "Sector", value: "Sector" },
            { text: "Security Setup", value: "SecuritySetup" },
            { text: "Settlement Instruction", value: "SettlementInstruction" },
            { text: "Signature", value: "Signature" },
            { text: "SubSector", value: "SubSector" },
            { text: "Switching Fund", value: "SwitchingFund" },
            { text: "TB Reconcile With BK", value: "TBReconcileTemp" },
            { text: "Template Import", value: "TemplateImport" },
            { text: "Trx Portfolio", value: "TrxPortfolio" },
            { text: "Unit Reconcile", value: "UnitReconcile" },
            { text: "Unit Registry Rpt", value: "UnitRegistryRpt" },
            { text: "Internal Close Price", value: "UpdateClosePrice" },
            { text: "Transaction from APERD", value: "UpdatePaymentSInvestTemp" },
            { text: "Users", value: "Users" },
            { text: "End Year Closing", value: "EndYearClosing" },
            { text: "RL504", value: "RL504" },
            { text: "RL510CEquity", value: "RL510CEquity" },
            { text: "RL510CBond", value: "RL510CBond" },
            { text: "RL510CSbn", value: "RL510CSbn" },
            { text: "Bank Interest Setup", value: "BankInterestSetup" },
            { text: "Haircut MKBD", value: "HaircutMKBD" },
            { text: "Template Fund Accounting Setup", value: "TemplateFundAccountingSetup" },
            { text: "Bond Redemption", value: "BondRedemption" },
            { text: "OMS Reksadana", value: "OMSReksadana" },
            { text: "Suspended And Inactive Client", value: "SuspendedAndInactiveClient" },
            { text: "Segment Class", value: "SegmentClass" },
            { text: "Data Migration", value: "DataMigration" },
            //{ text: "Client Subscription With Interest", value: "ClientSubscriptionWithInterest" },
            { text: "Fund Client Bank List", value: "FundClientBankList" },
            { text: "Transaction Promo", value: "TransactionPromo" },
            { text: "Fund Transfer Inter Bank", value: "FundTransferInterBank" },
            { text: "Investment Rpt", value: "InvestmentRpt" },
            { text: "Import Fxd", value: "ImportFxd" },
            { text: "Fund Client Bank Default", value: "FundClientBankDefault" },
            { text: "Template Report", value: "TemplateReport" },
            { text: "Fund Window Redemption", value: "FundWindowRedemption" },
            { text: "Fund Client Agent Setup", value: "FundClientAgentSetup" },
            { text: "SInvest Setup", value: "SInvestSetup" },
            { text: "Tax Amnesty Rpt", value: "TaxAmnestyRpt" },
            { text: "FFS Setup", value: "FFSSetup" },
            { text: "Reksadana Instrument", value: "ReksadanaInstrument" },
            { text: "Generate Unit Fee Summary", value: "GenerateUnitFeeSummary" },
            { text: "FFS Setup 02", value: "FFSSetup_02" },
            { text: "FFS Setup 21", value: "FFSSetup_21" },
            { text: "Currency Reval", value: "CurrencyReval" },
            { text: "Settlement Setup", value: "SettlementSetup" },
            { text: "PPH21Setup", value: "PPH21Setup" },
            { text: "High Risk Setup", value: "HighRiskSetup" },
            { text: "Fund Client Position End Year", value: "FundClientPositionEndYear" },
            { text: "SAP Master Customer", value: "SAPMSCustomer" },
            { text: "SAP Master Account", value: "SAPMSAccount" },
            { text: "SAP Bridge Journal", value: "SAPBridgeJournal" },
            { text: "Interface Journal SAP", value: "InterfaceJournalSAP" },
            { text: "Import Trx From BK", value: "SIEquity" },
            { text: "Master Sales", value: "MSSales" },
            { text: "Master Transaction", value: "MSTransaction" },
            { text: "Range Price", value: "RangePrice" },
            { text: "Customer Dashboard", value: "CustomerDashboard" },
            //{ text: "Agent CSR Fund", value: "AgentCSRFund" },
            { text: "Custodian Rpt", value: "CustodianRpt" },
            { text: "Fund Avg Priority", value: "FundAvgPriority" },
            { text: "Back Load Setup", value: "BackLoadSetup" },
            { text: "Advisory Fee", value: "AdvisoryFee" },
            { text: "Direct Investment", value: "DirectInvestment" },
            { text: "Insurance Product", value: "InsuranceProduct" },
            { text: "Trx Unit Payment Mapping", value: "TrxUnitPaymentMapping" },
            { text: "Trx Unit Payment Provider", value: "TrxUnitPaymentProvider" },
            { text: "Trx Unit Payment Type", value: "TrxUnitPaymentType" },
            { text: "Sharing Fee Setup", value: "SharingFeeSetup" },
        ],
        filter: "contains",
        suggest: true,
        change: OnChangeSearch,
        index: 0
    });


    function OnChangeSearch() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }
    
    function onMenu_Selected(e) {
        var item = $(e.item).children(".k-link").text().replace(/\s/g, "")
        item = item.toString();


        if (item != "FundAdmin" && item != "Accounting" && item != "Master" && item != "Finance" && item != "UnitRegistry"
            && item != "Compliance" && item != "Investment" && item != "Investment" && item != "Dealing" && item != "Settlement" && item != "Administrator") {
            // Sebelum tutup Panel, Check Permission sama Pasang Window.
            OpenForm(item);
        }

    }
    
    function OpenForm(item) {

        if (item == "Direct") {
            item = "Agent";
        }
        if (item == "TBReconcilewithBK") {
            item = "TBReconcileTemp";
        }
        if (item == "TransactionfromAPERD") {
            item = "UpdatePaymentSInvestTemp";
        }
        if (item == "EndDayTrailsJournalandUnit") {
            item = "EndDayTrails";
        }

        if (item == "FundDailyFeeSummary") {
            item = "FundDailyFee";
        }
        if (item == "FundFeeSetup") {
            item = "FundFeeSetup";
        }
        if (item == "InternalClosePrice") {
            item = "UpdateClosePrice";
        }
        if (item == "FundAdminRpt") {
            item = "FundAccountingRpt";
        }
        if (item == "ComplianceRpt") {
            item = "OjkRpt";
        }
        if (item == "CustodianRpt") {
            item = "CustodianRpt";
        }
        if (item == "ImportTrxFromBK") {
            item = "SIEquity";
        }

        if (_GlobClientCode == '20') {
            if (item == "PoolingAccount") {
                item = "DormantAccount";
            }
        }





        if (item != "FundAdmin" && item != "Accounting" && item != "Master" && item != "Finance" && item != "UnitRegistry"
            && item != "Compliance" && item != "Investment" && item != "Investment" && item != "Dealing" && item != "Settlement" && item != "Administrator") {
            // Sebelum tutup Panel, Check Permission sama Pasang Window.
            $.ajax({
                url: window.location.origin + '/Radsoft/Host/PermissionOpenCheck/' + sessionStorage.getItem("user") + '/' + item + "_O",
                type: 'GET',
                cache: 'false',
                contenType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        if (item == "AgentUsers"
                            || item == "Groups"
                            || item == "GroupsRoles"
                            || item == "SharingFeeSetup"
                            || item == "GroupsUsers"
                            || item == "Permission"
                            || item == "Roles"
                            || item == "RolesPermission"
                            || item == "RolesUsers"
                            || item == "SecuritySetup"
                            || item == "Users") {
                            $("#Frame").attr("src", "../../WEB/Settings/" + item + ".html");
                        }
                        else if (item == "AuditCOAFromSource"
                            || item == "BalanceFromSource"
                            || item == "BudgetCOADestinationOne"
                            || item == "COADestinationOne"
                            || item == "COADestinationOneMappingRpt"
                            || item == "COADestinationTwo"
                            || item == "COADestinationTwoMappingRpt"
                            || item == "COAFromSource"
                            || item == "MappingSourceToDestinationOne"
                            || item == "MFRItemRpt"
                            || item == "MFRMappingRpt"
                            || item == "MISCostCenter"
                            || item == "MISReport"
                        ) {
                            $("#Frame").attr("src", "../../WEB/MIS/" + item + ".html");
                        }
                        else if (item == "Account"
                            || item == "AccountBudget"
                            //|| item == "AccountingReportTemplate"
                            || item == "AccountingSetup"
                            || item == "Activity"
                            || item == "Agent"
                            || item == "AgentFundPosition"
                            || item == "AgentCSRFund"
                            || item == "AgentFeeSetup"
                            || item == "AUM"
                            || item == "Bank"
                            || item == "BankBranch"
                            || item == "BankCustodian"
                            || item == "BenchmarkIndex"
                            || item == "BlackListName"
                            || item == "Board"
                            || item == "BusinessType"
                            || item == "CAMInterface"
                            || item == "CashRef"
                            || item == "CategoryScheme"
                            || item == "CloseNav"
                            || item == "ClosePrice"
                            || item == "Company"
                            || item == "CompanyAccountTrading"
                            || item == "CompanyPosition"
                            || item == "Consignee"
                            || item == "CorporateArApPayment"
                            || item == "Counterpart"
                            || item == "CounterpartCommission"
                            || item == "CouponScheduler"
                            || item == "Currency"
                            || item == "CurrencyRate"
                            || item == "Custodian"
                            || item == "CustodianJournalAccountNameSetup"
                            || item == "CustodianMKBDMapping"
                            //|| item == "Dashboard"
                            || item == "DailyDataForCommissionRptNewLog"
                            || item == "Department"
                            || item == "DepositoType"
                            || item == "DormantAccount"
                            || item == "DownloadFileFromIDX"
                            || item == "Employee"
                            || item == "FAAdjustment"
                            || item == "FACOAAdjustment"
                            || item == "FACOAMapping"
                            || item == "FIFOBondPosition"
                            || item == "FinanceSetup"
                            || item == "Fund"
                            || item == "FundAccountingSetup"
                            || item == "FundCashRef"
                            || item == "FundClient"
                            || item == "FundClientCashRef"
                            || item == "FundClientPosition"
                            || item == "FundClientWallet"
                            || item == "FundDailyFee"
                            || item == "FundExposure"
                            || item == "FundFee"
                            || item == "FundIndex"
                            || item == "FundJournalAccount"
                            || item == "FundRiskProfile"
                            || item == "GoodFundReconcile"
                            || item == "HighRiskMonitoring"
                            || item == "HighRiskValueSetup"
                            || item == "Holding"
                            || item == "Index"
                            || item == "Instrument"
                            || item == "InstrumentCompanyType"
                            || item == "InstrumentIndex"
                            || item == "InstrumentSyariah"
                            || item == "InstrumentType"
                            || item == "InternalCategory"
                            || item == "Issuer"
                            || item == "ManageInvestment"
                            || item == "ManageURTransaction"
                            || item == "Market"
                            || item == "MarketHoliday"
                            || item == "MasterValue"
                            || item == "NavMappingReport"
                            || item == "Office"
                            || item == "Period"
                            //|| item == "RebateFeeSetup"
                            || item == "RegulerInstruction"
                            || item == "ReksadanaType"
                            || item == "RiskProfileMonitoring"
                            || item == "RiskProfileScore"
                            || item == "RiskQuestionnaire"
                            || item == "RiskQuestionnaireAnswer"
                            || item == "RolesPermissionNotification"
                            || item == "Sector"
                            || item == "Signature"
                            || item == "SubSector"
                            || item == "SwitchingFund"
                            || item == "TemplateImport"
                            || item == "UpdateClosePrice"
                            || item == "BankInterestSetup"
                            || item == "CustomerServiceBook"
                            || item == "FundClientVerification"
                            || item == "HaircutMKBD"
                            || item == "TemplateFundAccountingSetup"
                            || item == "BondRedemption"
                            || item == "FundClientDocument"
                            || item == "SuspendedAndInactiveClient"
                            || item == "SegmentClass"
                            || item == "DataMigration"
                            || item == "FundClientBankList"
                            || item == "FundPositionAdjustment"
                            || item == "TransactionPromo"
                            || item == "FundTransferInterBank"
                            || item == "ImportFxd"
                            || item == "FundClientBankDefault"
                            || item == "TemplateReport"
                            //|| item == "ClientDashboard"
                            || item == "FundWindowRedemption"
                            || item == "FundClientAgentSetup"
                            || item == "SInvestSetup"
                            || item == "FFSSetup"
                            || item == "ReksadanaInstrument"
                            || item == "FFSSetup_02"
                            || item == "FFSSetup_21"
                            || item == "SettlementSetup"
                            || item == "HighRiskSetup"
                            || item == "FundClientPositionEndYear"
                            || item == "RangePrice"
                            || item == "PPH21Setup"
                            || item == "BloombergEquity"
                            || item == "FundAvgPriority"
                            || item == "CorporateGovAM"
                            || item == "SIEquity"
                            || item == "BackLoadSetup"
                            || item == "DirectInvestment"
                            || item == "AdvisoryFee"
                            || item == "InsuranceProduct"
                            || item == "TrxUnitPaymentMapping"
                            || item == "TrxUnitPaymentProvider"
                            || item == "TrxUnitPaymentType"
                        ) {
                            $("#Frame").attr("src", "../../WEB/Masters/" + item + ".html");
                        }
                        else if (item == "AccountLedgerBalance"
                            || item == "EndDayTrails"
                            || item == "FixedAsset"
                            || item == "FundJournal"
                            || item == "FundJournalScenario"
                            || item == "Journal"
                            || item == "MKBDTrails"
                            || item == "NAWCDailyProcess"
                            || item == "PortfolioRevaluation"
                            || item == "Prepaid"
                            || item == "RetrieveMKBDDaily"
                            || item == "TBReconcileTemp"
                            || item == "TrxPortfolio"
                            || item == "EndYearClosing"
                            || item == "RL504"
                            || item == "RL510CBond"
                            || item == "RL510CEquity"
                            || item == "RL510CSbn"
                            || item == "RetrieveFromBridge"
                            || item == "GenerateUnitFeeSummary"
                            || item == "CurrencyReval"
                        ) {
                            $("#Frame").attr("src", "../../WEB/Accounting/" + item + ".html");
                        }
                        else if (item == "CorporateAction"
                            || item == "DealingInstruction"
                            || item == "EndDayTrailsFundPortfolio"
                            || item == "Exercise"
                            || item == "ExposureMonitoring"
                            || item == "InvestmentInstruction"
                            || item == "Notification"
                            || item == "OMSBond"
                            || item == "OMSEquity"
                            || item == "OMSTimeDeposit"
                            || item == "SettlementInstruction"
                            || item == "OMSReksadana"
                        ) {
                            $("#Frame").attr("src", "../../WEB/Investment/" + item + ".html");
                        }
                        else if (item == "CashierPayment"
                            || item == "CashierReceipt") {
                            if (_GlobClientCode == '01') {
                                $("#Frame").attr("src", "../../WEB/CustomClient01/" + item + ".html");
                            } else {
                                $("#Frame").attr("src", "../../WEB/Finance/" + item + ".html");
                            }

                        }
                        else if (item == "AccountingRpt"
                            || item == "CommissionRpt"
                            || item == "FinanceRpt"
                            || item == "FundAccountingRpt"
                            || item == "OjkRpt"
                            || item == "S-InvestRpt"
                            || item == "UnitRegistryRpt"
                            || item == "FundFactSheetRpt"
                            || item == "InvestmentRpt"
                            || item == "TaxAmnestyRpt"
                            || item == "CustodianRpt"

                        ) {
                            $("#Frame").attr("src", "../../WEB/Reports/" + item + ".html");
                        }
                        else if (item == "ClientRedemption"
                            || item == "ClientSubscription"
                            || item == "ClientSwitching"
                            || item == "DistributedIncome"
                            || item == "GenerateRebate"
                            || item == "UnitReconcile"
                            || item == "UpdatePaymentSInvestTemp"
                            || item == "ClientSubscriptionWithInterest"
                            || item == "HoldingPeriod"
                            || item == "CustomerDashboard") {
                            $("#Frame").attr("src", "../../WEB/UnitRegistry/" + item + ".html");
                        }
                        else if (item == "SAPMSCustomer"
                            || item == "SAPMSAccount"
                            || item == "SAPBridgeJournal"
                            || item == "InterfaceJournalSAP") {
                            $("#Frame").attr("src", "../../WEB/SAPMaster/" + item + ".html");
                        }
                        else if (item == "MSSales"
                            || item == "MSTransaction") {
                            $("#Frame").attr("src", "../../WEB/BrokerageCommission/" + item + ".html");
                        }
                        WinHomeNavigation.close();
                    }
                },
                error: function (data) {
                    alertify.alert("You dont have permission");
                    $("#Frame").attr("src", "");
                }
            });
        }


    }


    function InitMenu() {
        ResetMenu();
        if (_GlobClientCode == '03') {
            $("#Accounting").show();
            $("#FundAdmin").show();
            $("#Finance").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
            $("#LblAgentCSRFund").show();
        }
        else if (_GlobClientCode == '07') {
            $("#Accounting").show();
            $("#FundAdmin").show();
            $("#Finance").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
            $("#LblFundClientVerification").show();
        }
        else if (_GlobClientCode == '08') {
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Administrator").show();
            $("#Accounting").show();
            $("#Finance").show();

            $("#LblCategoryScheme").show();
            $("#LblCompanyPosition").show();
        }
        else if (_GlobClientCode == '09') {
            $("#Accounting").show();
            $("#FundAdmin").show();
            $("#Finance").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();

            $("#LblSubsWithInterest").show();
        }
        else if (_GlobClientCode == '10') {
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Administrator").show();
            $("#LblCustomerDashboard").show();
        }
        else if (_GlobClientCode == '11') {
            $("#FundAdmin").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
            $("#SAP").show();
        }
        else if (_GlobClientCode == '15') {
            $("#BrokerageCommission").show();
        }
        else if (_GlobClientCode == '16') {
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Administrator").show();
        }
        else if (_GlobClientCode == '20') {
            $("#Accounting").show();
            $("#FundAdmin").show();
            $("#Finance").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
            $("#LblPoolingAccount").show();
        }
        else if (_GlobClientCode == '23') {
            $("#FundAdmin").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
        }
        else if (_GlobClientCode == '24') {
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Administrator").show();
            $("#LblCustomerDashboard").show();
        }
        else {
            $("#Accounting").show();
            $("#FundAdmin").show();
            $("#Finance").show();
            $("#UnitRegistry").show();
            $("#Compliance").show();
            $("#Investment").show();
            $("#Dealing").show();
            $("#Settlement").show();
            $("#Administrator").show();
        }
    }


    function ResetMenu() {
        $("#Accounting").hide();
        $("#FundAdmin").hide();
        $("#Finance").hide();
        $("#UnitRegistry").hide();
        $("#Compliance").hide();
        $("#Investment").hide();
        $("#Dealing").hide();
        $("#Settlement").hide();
        $("#Administrator").hide();
        $("#SAP").hide();
        $("#BrokerageCommission").hide();
        $("#LblFundClientVerification").hide();

        //03
        $("#LblAgentCSRFund").hide();

        //08
        $("#LblCategoryScheme").hide();
        $("#LblCompanyPosition").hide();

        //10
        $("#LblCustomerDashboard").hide();
        $("#LblSubsWithInterest").hide();

        //20
        $("#LblPoolingAccount").hide();



    }

});