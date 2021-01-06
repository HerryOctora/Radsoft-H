$(document).ready(function () {
    document.title = 'FORM SUSPENDED AND IN ACTIVE CLIENT';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCity;
    var htmlCityPK;
    var htmlCityDesc;
    var WinListProvince;
    var htmlProvince;
    var htmlProvinceDesc;
    var WinListCountry;
    var htmlCountry;
    var htmlCountryDesc;
    var WinListNationality;
    var WinListSpouseNationality;
    var htmlNationality;
    var htmlNationalityDesc;
    var htmlSpouseNationality;
    var htmlSpouseNationalityDesc;

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

        $("#BtnCopyClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
            
        });

        //$("#BtnAdd").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});

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

        //$("#BtnNew").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnAdd.png"
        //});

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        //$("#BtnGenerateARIAText").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        //});
        //$("#BtnOkGenerateARIAText").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});
        //$("#BtnCancelGenerateARIAText").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnClose.png"
        //});
        //$("#BtnGenerateSInvest").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});
        //$("#BtnGenerateSInvestBankAccount").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});
        //$("#BtnGenerateNKPD").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        //});
        //$("#BtnOkGenerateNKPD").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});
        //$("#BtnCancelGenerateNKPD").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnClose.png"
        //});
        //$("#BtnOpenNewTab").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnAdd.png"
        //});

        //$("#BtnSuspendBySelected").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnSave.png"
        //});
        $("#BtnUnSuspendBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

    }

    //function resetNotification() {
    //    $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
    //    alertify.set({
    //        labels: {
    //            ok: "OK",
    //            cancel: "Cancel"
    //        },
    //        delay: 4000,
    //        buttonReverse: false,
    //        buttonFocus: "ok"
    //    });
    //}

    function initWindow() {
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestorType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamInvestorType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    index: 0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $("#ParamDateARIA").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#ParamDateNKPD").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#TanggalLahir").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#RegistrationDateIdentitasInd1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#ExpiredDateIdentitasInd1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasInd2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasInd2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasInd3").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasInd3").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasInd4").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasInd4").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#RegistrationNPWP").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateSKD").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#TanggalBerdiri").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns11").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns11").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns12").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns12").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#RegistrationDateIdentitasIns13").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns13").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns14").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns14").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns21").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns21").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns22").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns22").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#RegistrationDateIdentitasIns23").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns23").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns24").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns24").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns31").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns31").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns32").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns32").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns33").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns33").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns34").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns34").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#RegistrationDateIdentitasIns41").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns41").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns42").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns42").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns43").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns43").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasIns44").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ExpiredDateIdentitasIns44").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#SIUPExpirationDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DormantDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#SpouseDateOfBirth ").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });

        WinGenerateNKPD = $("#WinGenerateNKPD").kendoWindow({
            height: "250px",
            title: "* Generate NKPD",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateNKPDClose
        }).data("kendoWindow");

        WinGenerateARIAText = $("#WinGenerateARIAText").kendoWindow({
            height: "250px",
            title: "* Generate ARIA",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateARIATextClose
        }).data("kendoWindow");


        win = $("#WinFundClient").kendoWindow({
            height: "95%",
            title: "Suspended And Inactive Client Detail",
            visible: false,
            width: 1450,
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

        WinListCity = $("#WinListCity").kendoWindow({
            height: "520px",
            title: "City List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCityClose
        }).data("kendoWindow");

        WinListProvince = $("#WinListProvince").kendoWindow({
            height: "520px",
            title: "Province List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListProvinceClose
        }).data("kendoWindow");

        WinListCountry = $("#WinListCountry").kendoWindow({
            height: "520px",
            title: "Country List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCountryClose
        }).data("kendoWindow");


        WinListNationality = $("#WinListNationality").kendoWindow({
            height: "520px",
            title: "Nationality List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListNationalityClose
        }).data("kendoWindow");

        WinListSpouseNationality = $("#WinListSpouseNationality").kendoWindow({
            height: "520px",
            title: "SpouseNationality List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListSpouseNationalityClose
        }).data("kendoWindow");

    }

    function setFundClient(data) {
        $("#rowBusinessTypeOJK").hide();
        if (data.Status == 1) {

            $("#StatusHeader").val("PENDING");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").show();
            $("#BtnCopyClient").hide();
            $("#ID").attr('readonly', true);
        }
        if (data.Status == 2) {
            $("#StatusHeader").val("APPROVED");
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnAdd").hide();
            $("#BtnOldData").hide();
            $("#ID").attr('readonly', true);
        }
        if (data.Status == 3) {

            $("#StatusHeader").val("VOID");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#BtnCopyClient").hide();

        }
        if (data.Status == 4) {
            $("#StatusHeader").val("WAITING");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
        }

        $("#FundClientPK").val(data.FundClientPK);
        $("#HistoryPK").val(data.HistoryPK);
        $("#Notes").val(data.Notes);
        $("#ID").val(data.ID);
        $("#OldID").val(data.OldID);
        $("#Name").val(data.Name);
        $("#SID").val(data.SID);
        $("#IFUACode").val(data.IFUACode);
        $("#NPWP").val(data.NPWP);
        $("#SACode").val(data.SACode);
        $("#Email").val(data.Email);
        $("#Description").val(data.Description);
        $("#ClientCategory").val(data.ClientCategory);
        $("#InvestorType").val(data.InvestorType);
        //$("#TipeOjk").val(data.TipeOjk);

        if (data.BitIsAfiliated == true) {
            $("#BitIsAfiliated").text('AFILIATED WITH CLIENT NO ' + data.AfiliatedFrom);
        } else {
            $("#BitIsAfiliated").text('FREE');
        }
        if (data.BitIsSuspend == true) {
            $("#BitIsSuspend").text('SUSPENDED');
        } else {
            $("#BitIsSuspend").text('ACTIVE');
        }

        var _dormantDate = new Date(data.DormantDate);
        if (_dormantDate <= new Date() && kendo.toString(kendo.parseDate(data.DormantDate), 'dd/MM/yyyy') != '01/01/1900' && _dormantDate != null) {
            $("#BitIsSuspend").text('INACTIVE');
        }

        var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
        if (data.InvestorType == 1) {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $(GlobTabStrip.items()[3]).show();
            $(GlobTabStrip.items()[4]).hide();
            $(GlobTabStrip.items()[5]).hide();
            $(GlobTabStrip.items()[6]).show();
            RefreshTab(0);
        } else if (data.InvestorType == 2) {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).hide();
            $(GlobTabStrip.items()[3]).hide();
            $(GlobTabStrip.items()[4]).show();
            $(GlobTabStrip.items()[6]).show();
            RefreshTab(0);
        }
        RequiredAttributes(data.InvestorType);

        var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
        if (data.TipeOjk == 1) {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();

            RefreshTab(0);
        } else if (data.TipeOjk == 2) {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();

            RefreshTab(0);
        }
        RequiredAttributesTipeOjk(data.TipeOjk);


        if (data.CompanyTypeOJK == 3) {
            $("#rowBusinessTypeOJK").show();
        }
        //INDIVIDUAL
        $("#NamaDepanInd").val(data.NamaDepanInd);
        $("#NamaTengahInd").val(data.NamaTengahInd);
        $("#NamaBelakangInd").val(data.NamaBelakangInd);
        $("#TempatLahir").val(data.TempatLahir);
        $("#AlamatInd1").val(data.AlamatInd1);
        $("#KodePosInd1").val(data.KodePosInd1);
        $("#AlamatInd2").val(data.AlamatInd2);
        $("#KodePosInd2").val(data.KodePosInd2);
        $("#SpouseName").val(data.SpouseName);
        $("#MotherMaidenName").val(data.MotherMaidenName);
        $("#AhliWaris").val(data.AhliWaris);
        $("#CountryOfBirth").val(data.CountryOfBirfth);
        $("#HubunganAhliWaris").val(data.HubunganAhliWaris);
        $("#NatureOfBusiness").val(data.NatureOfBusiness);
        $("#PolitisLainnya").val(data.PolitisLainnya);
        $("#OtherEmail").val(data.OtherEmail);
        $("#OtherAlamatInd1").val(data.OtherAlamatInd1);
        $("#OtherKodePosInd1").val(data.OtherKodePosInd1);
        $("#OtherAlamatInd2").val(data.OtherAlamatInd2);
        $("#OtherKodePosInd2").val(data.OtherKodePosInd2);
        $("#OtherAlamatInd3").val(data.OtherAlamatInd3);
        $("#OtherKodePosInd3").val(data.OtherKodePosInd3);
        $("#JumlahIdentitasInd").val(data.JumlahIdentitasInd);
        $("#NoIdentitasInd1").val(data.NoIdentitasInd1);
        $("#NoIdentitasInd2").val(data.NoIdentitasInd2);
        $("#NoIdentitasInd3").val(data.NoIdentitasInd3);
        $("#NoIdentitasInd4").val(data.NoIdentitasInd4);

        $("#TeleponRumah").val(data.TeleponRumah);
        $("#TeleponSelular").val(data.TeleponSelular);
        $("#OtherTeleponRumah").val(data.OtherTeleponRumah);
        $("#OtherTeleponSelular").val(data.OtherTeleponSelular);
        $("#TeleponSelular").val(data.TeleponSelular);
        $("#Fax").val(data.Fax);
        $("#OtherFax").val(data.OtherFax);


        $("#CountryofCorrespondence").val(data.CountryofCorrespondence);
        $("#CountryofCorrespondenceDesc").val(data.CountryofCorrespondenceDesc);
        $("#CountryofDomicile").val(data.CountryofDomicile);
        $("#CountryofDomicileDesc").val(data.CountryofDomicileDesc);


        //INSTITUTION
        $("#NamaPerusahaan").val(data.NamaPerusahaan);
        $("#NoSKD").val(data.NoSKD);
        $("#AlamatPerusahaan").val(data.AlamatPerusahaan);
        $("#KodePosIns").val(data.KodePosIns);
        $("#LokasiBerdiri").val(data.LokasiBerdiri);
        $("#NomorAnggaran").val(data.NomorAnggaran);
        $("#NomorSIUP").val(data.NomorSIUP);
        $("#JumlahPejabat").val(data.JumlahPejabat);
        $("#NamaDepanIns1").val(data.NamaDepanIns1);
        $("#NamaTengahIns1").val(data.NamaTengahIns1);
        $("#NamaBelakangIns1").val(data.NamaBelakangIns1);
        $("#Jabatan1").val(data.Jabatan1);
        $("#JumlahIdentitasIns1").val(data.JumlahIdentitasIns1);
        $("#NoIdentitasIns11").val(data.NoIdentitasIns11);
        $("#NoIdentitasIns12").val(data.NoIdentitasIns12);
        $("#NoIdentitasIns13").val(data.NoIdentitasIns13);
        $("#NoIdentitasIns14").val(data.NoIdentitasIns14);
        $("#NamaDepanIns2").val(data.NamaDepanIns2);
        $("#NamaTengahIns2").val(data.NamaTengahIns2);
        $("#NamaBelakangIns2").val(data.NamaBelakangIns2);
        $("#Jabatan2").val(data.Jabatan2);
        $("#JumlahIdentitasIns2").val(data.JumlahIdentitasIns2);
        $("#NoIdentitasIns21").val(data.NoIdentitasIns21);
        $("#NoIdentitasIns22").val(data.NoIdentitasIns22);
        $("#NoIdentitasIns23").val(data.NoIdentitasIns23);
        $("#NoIdentitasIns24").val(data.NoIdentitasIns24);
        $("#NamaDepanIns3").val(data.NamaDepanIns3);
        $("#NamaTengahIns3").val(data.NamaTengahIns3);
        $("#NamaBelakangIns3").val(data.NamaBelakangIns3);
        $("#Jabatan3").val(data.Jabatan3);
        $("#JumlahIdentitasIns3").val(data.JumlahIdentitasIns3);
        $("#NoIdentitasIns31").val(data.NoIdentitasIns31);
        $("#NoIdentitasIns32").val(data.NoIdentitasIns32);
        $("#NoIdentitasIns33").val(data.NoIdentitasIns33);
        $("#NoIdentitasIns34").val(data.NoIdentitasIns34);
        $("#NamaDepanIns4").val(data.NamaDepanIns4);
        $("#NamaTengahIns4").val(data.NamaTengahIns4);
        $("#NamaBelakangIns4").val(data.NamaBelakangIns4);
        $("#Jabatan4").val(data.Jabatan4);
        $("#JumlahIdentitasIns4").val(data.JumlahIdentitasIns4);
        $("#NoIdentitasIns41").val(data.NoIdentitasIns41);
        $("#NoIdentitasIns42").val(data.NoIdentitasIns42);
        $("#NoIdentitasIns43").val(data.NoIdentitasIns43);
        $("#NoIdentitasIns44").val(data.NoIdentitasIns44);

        $("#CountryofEstablishment").val(data.CountryofEstablishment);
        $("#CountryofEstablishmentDesc").val(data.CountryofEstablishmentDesc);
        $("#CompanyCityName").val(data.CompanyCityName);
        $("#CompanyCityNameDesc").val(data.CompanyCityNameDesc);
        $("#CountryofCompany").val(data.CountryofCompany);
        $("#CountryofCompanyDesc").val(data.CountryofCompanyDesc);
        $("#NPWPPerson1").val(data.NPWPPerson1);
        $("#NPWPPerson2").val(data.NPWPPerson2);

        $("#PhoneIns1").val(data.PhoneIns1);
        $("#PhoneIns2").val(data.PhoneIns2);

        //BANK INFORMATION
        $("#JumlahBank").val(data.JumlahBank);
        $("#NamaBank1").val(data.NamaBank1);
        $("#NomorRekening1").val(data.NomorRekening1);
        $("#BICCode1").val(data.BICCode1);
        $("#NamaNasabah1").val(data.NamaNasabah1);
        $("#NamaBank2").val(data.NamaBank2);
        $("#NomorRekening2").val(data.NomorRekening2);
        $("#BICCode2").val(data.BICCode2);
        $("#NamaNasabah2").val(data.NamaNasabah2);
        $("#NamaBank3").val(data.NamaBank3);
        $("#NomorRekening3").val(data.NomorRekening3);
        $("#BICCode3").val(data.BICCode3);
        $("#NamaNasabah3").val(data.NamaNasabah3);
        //SInvest
        $("#BIMemberCode1").val(data.BIMemberCode1);
        $("#BIMemberCode2").val(data.BIMemberCode2);
        $("#BIMemberCode3").val(data.BIMemberCode3);
        $("#BankBranchName1").val(data.BankBranchName1);
        $("#BankBranchName2").val(data.BankBranchName2);
        $("#BankBranchName3").val(data.BankBranchName3);
        $("#EmailIns1").val(data.EmailIns1);
        $("#EmailIns2").val(data.EmailIns2);
        $("#TIN").val(data.TIN);
        $("#GIIN").val(data.GIIN);
        $("#SubstantialOwnerName").val(data.SubstantialOwnerName);
        $("#SubstantialOwnerAddress").val(data.SubstantialOwnerAddress);
        $("#SubstantialOwnerTIN").val(data.SubstantialOwnerTIN);

        //DATE
        if (data.TanggalLahir == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.TanggalLahir == '1/1/1900 12:00:00 AM') {
            $("#TanggalLahir").data("kendoDatePicker").value("");
        } else {
            $("#TanggalLahir").data("kendoDatePicker").value(new Date(data.TanggalLahir));
        }
        if (data.RegistrationDateIdentitasInd1 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasInd1 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasInd1").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasInd1").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasInd1));
        }
        if (data.ExpiredDateIdentitasInd1 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasInd1 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasInd1").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasInd1").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasInd1));
        }
        if (data.RegistrationDateIdentitasInd2 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasInd2 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasInd2").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasInd2").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasInd2));
        }
        if (data.ExpiredDateIdentitasInd2 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasInd2 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasInd2").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasInd2").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasInd2));
        }
        if (data.RegistrationDateIdentitasInd3 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasInd3 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasInd3").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasInd3").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasInd3));
        }
        if (data.ExpiredDateIdentitasInd3 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasInd3 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasInd3").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasInd3").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasInd3));
        }
        if (data.RegistrationDateIdentitasInd4 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasInd4 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasInd4").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasInd4").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasInd4));
        }
        if (data.ExpiredDateIdentitasInd4 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasInd4 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasInd4").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasInd4").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasInd4));
        }
        if (data.RegistrationNPWP == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationNPWP == '1/1/1900 12:00:00 AM') {
            $("#RegistrationNPWP").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationNPWP").data("kendoDatePicker").value(new Date(data.RegistrationNPWP));
        }
        if (data.DormantDate == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.DormantDate == '1/1/1900 12:00:00 AM') {
            $("#DormantDate").data("kendoDatePicker").value("");
        } else {
            $("#DormantDate").data("kendoDatePicker").value(new Date(data.DormantDate));
        }
        if (data.ExpiredDateSKD == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateSKD == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateSKD").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateSKD").data("kendoDatePicker").value(new Date(data.ExpiredDateSKD));
        }
        if (data.TanggalBerdiri == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.TanggalBerdiri == '1/1/1900 12:00:00 AM') {
            $("#TanggalBerdiri").data("kendoDatePicker").value("");
        } else {
            $("#TanggalBerdiri").data("kendoDatePicker").value(new Date(data.TanggalBerdiri));
        }
        if (data.RegistrationDateIdentitasIns11 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns11 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns11").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns11").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns11));
        }
        if (data.ExpiredDateIdentitasIns11 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns11 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns11").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns11").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns11));
        }
        if (data.RegistrationDateIdentitasIns12 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns12 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns12").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns12").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns12));
        }
        if (data.ExpiredDateIdentitasIns12 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns12 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns12").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns12").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns12));
        }
        if (data.RegistrationDateIdentitasIns13 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns13 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns13").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns13").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns13));
        }
        if (data.ExpiredDateIdentitasIns13 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns13 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns13").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns13").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns13));
        }
        if (data.RegistrationDateIdentitasIns14 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns14 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns14").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns14").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns14));
        }
        if (data.ExpiredDateIdentitasIns14 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns14 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns14").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns14").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns14));
        }
        if (data.RegistrationDateIdentitasIns21 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns21 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns21").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns21").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns21));
        }
        if (data.ExpiredDateIdentitasIns21 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns21 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns21").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns21").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns21));
        }
        if (data.RegistrationDateIdentitasIns22 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns22 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns22").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns22").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns22));
        }
        if (data.ExpiredDateIdentitasIns22 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns22 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns22").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns22").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns22));
        }
        if (data.RegistrationDateIdentitasIns23 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns23 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns23").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns23").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns23));
        }
        if (data.ExpiredDateIdentitasIns23 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns23 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns23").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns23").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns23));
        }
        if (data.RegistrationDateIdentitasIns24 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns24 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns24").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns24").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns24));
        }
        if (data.ExpiredDateIdentitasIns24 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns24 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns24").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns24").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns24));
        }
        if (data.RegistrationDateIdentitasIns31 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns31 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns31").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns31").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns31));
        }
        if (data.ExpiredDateIdentitasIns31 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns31 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns31").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns31").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns31));
        }
        if (data.RegistrationDateIdentitasIns32 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns32 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns32").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns32").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns32));
        }
        if (data.ExpiredDateIdentitasIns32 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns32 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns32").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns32").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns32));
        }
        if (data.RegistrationDateIdentitasIns33 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns33 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns33").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns33").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns33));
        }
        if (data.ExpiredDateIdentitasIns33 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns33 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns33").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns33").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns33));
        }
        if (data.RegistrationDateIdentitasIns34 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns34 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns34").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns34").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns34));
        }
        if (data.ExpiredDateIdentitasIns34 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns34 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns34").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns34").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns34));
        }
        if (data.RegistrationDateIdentitasIns41 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns41 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns41").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns41").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns41));
        }
        if (data.ExpiredDateIdentitasIns41 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns41 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns41").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns41").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns41));
        }
        if (data.RegistrationDateIdentitasIns42 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns42 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns42").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns42").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns42));
        }
        if (data.ExpiredDateIdentitasIns42 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns42 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns42").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns42").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns42));
        }
        if (data.RegistrationDateIdentitasIns43 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns43 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns43").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns43").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns43));
        }
        if (data.ExpiredDateIdentitasIns43 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns43 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns43").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns43").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns43));
        }
        if (data.RegistrationDateIdentitasIns44 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationDateIdentitasIns44 == '1/1/1900 12:00:00 AM') {
            $("#RegistrationDateIdentitasIns44").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationDateIdentitasIns44").data("kendoDatePicker").value(new Date(data.RegistrationDateIdentitasIns44));
        }
        if (data.ExpiredDateIdentitasIns44 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.ExpiredDateIdentitasIns44 == '1/1/1900 12:00:00 AM') {
            $("#ExpiredDateIdentitasIns44").data("kendoDatePicker").value("");
        } else {
            $("#ExpiredDateIdentitasIns44").data("kendoDatePicker").value(new Date(data.ExpiredDateIdentitasIns44));
        }
        if (data.SIUPExpirationDate == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.SIUPExpirationDate == '1/1/1900 12:00:00 AM') {
            $("#SIUPExpirationDate").data("kendoDatePicker").value("");
        } else {
            $("#SIUPExpirationDate").data("kendoDatePicker").value(new Date(data.SIUPExpirationDate));
        }
        if (data.RegistrationNPWP == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.RegistrationNPWP == '1/1/1900 12:00:00 AM') {
            $("#RegistrationNPWP").data("kendoDatePicker").value("");
        } else {
            $("#RegistrationNPWP").data("kendoDatePicker").value(new Date(data.RegistrationNPWP));
        }
        if (data.SpouseDateOfBirth == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.SpouseDateOfBirth == '1/1/1900 12:00:00 AM') {
            $("#SpouseDateOfBirth ").data("kendoDatePicker").value("");
        } else {
            $("#SpouseDateOfBirth ").data("kendoDatePicker").value(new Date(data.SpouseDateOfBirth));
        }

        //LIST
        $("#Negara").val(data.Negara);
        $("#NegaraDesc").val(data.NegaraDesc);
        $("#Nationality").val(data.Nationality);
        $("#NationalityDesc").val(data.NationalityDesc);
        $("#Propinsi").val(data.Propinsi);
        $("#PropinsiDesc").val(data.PropinsiDesc);
        $("#KodeKotaInd1").val(data.KodeKotaInd1);
        $("#KodeKotaInd1Desc").val(data.KodeKotaInd1Desc);
        $("#KodeKotaInd2").val(data.KodeKotaInd2);
        $("#KodeKotaInd2Desc").val(data.KodeKotaInd2Desc);
        $("#OtherKodeKotaInd1").val(data.OtherKodeKotaInd1);
        $("#OtherKodeKotaInd1Desc").val(data.OtherKodeKotaInd1Desc);
        $("#OtherPropinsiInd1").val(data.OtherPropinsiInd1);
        $("#OtherPropinsiInd1Desc").val(data.OtherPropinsiInd1Desc);
        $("#CountryOfBirth").val(data.CountryOfBirth);
        $("#CountryOfBirthDesc").val(data.CountryOfBirthDesc);
        $("#OtherNegaraInd1").val(data.OtherNegaraInd1);
        $("#OtherNegaraInd1Desc").val(data.OtherNegaraInd1Desc);
        $("#OtherKodeKotaInd2").val(data.OtherKodeKotaInd2);
        $("#OtherKodeKotaInd2Desc").val(data.OtherKodeKotaInd2Desc);
        $("#OtherPropinsiInd2").val(data.OtherPropinsiInd2);
        $("#OtherPropinsiInd2Desc").val(data.OtherPropinsiInd2Desc);
        $("#OtherNegaraInd2").val(data.OtherNegaraInd2);
        $("#OtherNegaraInd2Desc").val(data.OtherNegaraInd2Desc);
        $("#OtherKodeKotaInd3").val(data.OtherKodeKotaInd3);
        $("#OtherKodeKotaInd3Desc").val(data.OtherKodeKotaInd3Desc);
        $("#OtherPropinsiInd3").val(data.OtherPropinsiInd3);
        $("#OtherPropinsiInd3Desc").val(data.OtherPropinsiInd3Desc);
        $("#OtherNegaraInd3").val(data.OtherNegaraInd3);
        $("#OtherNegaraInd3Desc").val(data.OtherNegaraInd3Desc);
        $("#KodeKotaIns").val(data.KodeKotaIns);
        $("#KodeKotaInsDesc").val(data.KodeKotaInsDesc);
        $("#BankCountry1").val(data.BankCountry1);
        $("#BankCountry1Desc").val(data.BankCountry1Desc);
        $("#BankCountry2").val(data.BankCountry2);
        $("#BankCountry2Desc").val(data.BankCountry2Desc);
        $("#BankCountry3").val(data.BankCountry3);
        $("#BankCountry3Desc").val(data.BankCountry3Desc);
        $("#TINIssuanceCountry").val(data.TINIssuanceCountry);
        $("#TINIssuanceCountryDesc").val(data.TINIssuanceCountryDesc);

        //RDN
        $("#RDNAccountNo").val(data.RDNAccountNo);
        $("#RDNAccountName").val(data.RDNAccountName);

        $("#SpouseBirthPlace").val(data.SpouseBirthPlace);

        $("#EntryUsersID").val(data.EntryUsersID);
        $("#UpdateUsersID").val(data.UpdateUsersID);
        $("#ApprovedUsersID").val(data.ApprovedUsersID);
        $("#VoidUsersID").val(data.VoidUsersID);
        $("#EntryTime").val(kendo.toString(kendo.parseDate(data.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#UpdateTime").val(kendo.toString(kendo.parseDate(data.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#ApprovedTime").val(kendo.toString(kendo.parseDate(data.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#VoidTime").val(kendo.toString(kendo.parseDate(data.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(data.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        $("#SuspendBy").val(data.SuspendBy);
        $("#SuspendTime").val(kendo.toString(kendo.parseDate(data.SuspendTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#UnSuspendBy").val(data.UnSuspendBy);
        $("#UnSuspendTime").val(kendo.toString(kendo.parseDate(data.UnSuspendTime), 'dd/MMM/yyyy HH:mm:ss'));




        //// FORMAT PHONE
        //$("#TeleponSelular").kendoMaskedTextBox({
        //    mask: "(999) 000-0000000",
        //    value: setTeleponSelular()
        //});
        //function setTeleponSelular() {
        //    if (data.TeleponSelular == null) {
        //        return "";
        //    } else {
        //        return data.TeleponSelular;
        //    }
        //}

        //$("#Fax").kendoMaskedTextBox({
        //    mask: "(999) 000-00000",
        //    value: setFax()
        //});
        //function setFax() {
        //    if (data.Fax == null) {
        //        return "";
        //    } else {
        //        return data.Fax;
        //    }
        //}

        //$("#TeleponBisnis").kendoMaskedTextBox({
        //    mask: "(999) 000-00000",
        //    value: setTeleponBisnis()
        //});
        //function setTeleponBisnis() {
        //    if (data.TeleponBisnis == null) {
        //        return "";
        //    } else {
        //        return data.TeleponBisnis;
        //    }
        //}

        //$("#TeleponRumah").kendoMaskedTextBox({
        //    mask: "(999) 000-00000",
        //    value: setTeleponRumah()
        //});
        //function setTeleponRumah() {
        //    if (data.TeleponRumah == null) {
        //        return "";
        //    } else {
        //        return data.TeleponRumah;
        //    }
        //}

        //$("#OtherTeleponRumah").kendoMaskedTextBox({
        //    mask: "(999) 000-00000",
        //    value: setOtherTeleponRumah()
        //});
        //function setOtherTeleponRumah() {
        //    if (data.OtherTeleponRumah == null) {
        //        return "";
        //    } else {
        //        return data.OtherTeleponRumah;
        //    }
        //}

        //$("#OtherTeleponSelular").kendoMaskedTextBox({
        //    mask: "(999) 000-0000000",
        //    value: setOtherTeleponSelular()
        //});
        //function setOtherTeleponSelular() {
        //    if (data.OtherTeleponSelular == null) {
        //        return "";
        //    } else {
        //        return data.OtherTeleponSelular;
        //    }
        //}

        //$("#PhoneIns1").kendoMaskedTextBox({
        //    mask: "(999) 000-0000000",
        //    value: setPhoneIns1()
        //});
        //function setPhoneIns1() {
        //    if (data.PhoneIns1 == null) {
        //        return "";
        //    } else {
        //        return data.PhoneIns1;
        //    }
        //}

        //$("#PhoneIns2").kendoMaskedTextBox({
        //    mask: "(999) 000-0000000",
        //    value: setPhoneIns2()
        //});
        //function setPhoneIns2() {
        //    if (data.PhoneIns2 == null) {
        //        return "";
        //    } else {
        //        return data.PhoneIns2;
        //    }
        //}

        //$("#OtherFax").kendoMaskedTextBox({
        //    mask: "(999) 000-00000",
        //    value: setOtherFax()
        //});
        //function setOtherFax() {
        //    if (data.OtherFax == null) {
        //        return "";
        //    } else {
        //        return data.OtherFax;
        //    }
        //}


        //NUMERIC
        $("#JumlahDanaAwal").kendoNumericTextBox({
            format: "n0",
            value: setJumlahDanaAwal()

        });
        function setJumlahDanaAwal() {
            if (data.JumlahDanaAwal == null) {
                return 0;
            } else {
                return data.JumlahDanaAwal;
            }
        }

        $("#RiskProfileScore").kendoNumericTextBox({
            format: "n0",
            value: setRiskProfileScore(),
            change: OnChangeRiskProfileScore

        });
        function setRiskProfileScore() {
            if (data.RiskProfileScore == null) {
                return 0
            } else {
                return data.RiskProfileScore;
            }
        }

        function OnChangeRiskProfileScore() {
            $.ajax({
                url: window.location.origin + "/Radsoft/RiskProfileScore/GetInvestorRiskProfileByScore/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#RiskProfileScore").data("kendoNumericTextBox").value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#InvestorsRiskProfile").data("kendoComboBox").value(data);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }



        // COMBO BOX BIT
        $("#Child").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",

            suggest: true,
            change: OnChangeChild,
            value: setCmbChild()
        });
        function OnChangeChild() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbChild() {
            if (data.Child == null) {
                return false;
            } else {
                return data.Child;
            }
        }

        $("#ARIA").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeARIA,
            value: setCmbARIA()
        });
        function OnChangeARIA() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbARIA() {
            if (data.ARIA == null) {
                return true;
            } else {
                return data.ARIA;
            }
        }

        $("#Registered").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeRegistered,
            value: setCmbRegistered()
        });
        function OnChangeRegistered() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbRegistered() {
            if (data.Registered == null) {
                return true;
            } else {
                return data.Registered;
            }
        }



        //ClientCategory
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClientCategory",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ClientCategory").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeClientCategory,
                    value: setCmbClientCategory()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbClientCategory() {

            if (data.ClientCategory == null) {
                return "";
            } else {
                if (data.ClientCategory == 0) {
                    return "";
                } else {
                    return data.ClientCategory;
                }
            }
        }

        function OnChangeClientCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //InvestorType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestorType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InvestorType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInvestorType,
                    value: setCmbInvestorType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbInvestorType() {

            if (data.InvestorType == null) {
                return "";
            } else {
                if (data.InvestorType == 0) {
                    return "";
                } else {
                    return data.InvestorType;
                }
            }
        }
        function OnChangeInvestorType() {


            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
                if ($("#InvestorType").data("kendoComboBox").value() == 1) {

                    $(GlobTabStrip.items()[0]).show();
                    $(GlobTabStrip.items()[1]).show();
                    $(GlobTabStrip.items()[2]).show();
                    $(GlobTabStrip.items()[3]).show();
                    $(GlobTabStrip.items()[4]).hide();
                    $(GlobTabStrip.items()[5]).hide();
                    $(GlobTabStrip.items()[6]).show();
                    $("#LblChild").hide();

                } else if ($("#InvestorType").data("kendoComboBox").value() == 2) {

                    $(GlobTabStrip.items()[0]).show();
                    $(GlobTabStrip.items()[1]).show();
                    $(GlobTabStrip.items()[2]).hide();
                    $(GlobTabStrip.items()[3]).hide();
                    $(GlobTabStrip.items()[4]).show();
                    $(GlobTabStrip.items()[5]).show();
                    $(GlobTabStrip.items()[6]).show();
                    $("#LblChild").show();
                }


            }
            ClearAttributes();
            RequiredAttributes($("#InvestorType").data("kendoComboBox").value());
            RefreshTab(0);
        }

        //CompanyTypeOJK
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CompanyTypeOJK",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CompanyTypeOJK").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCompanyTypeOJK,
                    value: setCmbCompanyTypeOJK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function OnChangeCompanyTypeOJK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                if ($("#CompanyTypeOJK").data("kendoComboBox").value() == 3) {
                    $("#rowBusinessTypeOJK").show();
                } else {
                    $("#rowBusinessTypeOJK").hide();
                }
            }
        }

        function setCmbCompanyTypeOJK() {

            if (data.CompanyTypeOJK == null) {
                return "";
            } else {
                if (data.CompanyTypeOJK == 0) {
                    return "";
                } else {
                    return data.CompanyTypeOJK;
                }
            }
        }

        //function OnChangeCompanyTypeOJK() {


        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //    else {
        //        var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
        //        if ($("#CompanyTypeOJK").data("kendoComboBox").value() == 4,5,6,7) {

        //            $(GlobTabStrip.items()[0]).show();
        //            $(GlobTabStrip.items()[1]).show();
        //            $(GlobTabStrip.items()[2]).show();
        //            $(GlobTabStrip.items()[3]).show();
        //            $(GlobTabStrip.items()[4]).hide();
        //            $(GlobTabStrip.items()[5]).hide();
        //            $(GlobTabStrip.items()[6]).show(); 
        //            $("#LblBusinessTypeOJK").hide();

        //        } else if ($("#CompanyTypeOJK").data("kendoComboBox").value() == 3) {

        //            $(GlobTabStrip.items()[0]).show();
        //            $(GlobTabStrip.items()[1]).show();
        //            $(GlobTabStrip.items()[2]).hide();
        //            $(GlobTabStrip.items()[3]).hide();
        //            $(GlobTabStrip.items()[4]).show();
        //            $(GlobTabStrip.items()[5]).show();
        //            $(GlobTabStrip.items()[6]).show();
        //            $("#LblBusinessTypeOJK").show();

        //        } else if ($("#CompanyTypeOJK").data("kendoComboBox").value() == 1,2) {

        //            $(GlobTabStrip.items()[0]).show();
        //            $(GlobTabStrip.items()[1]).show();
        //            $(GlobTabStrip.items()[2]).hide();
        //            $(GlobTabStrip.items()[3]).hide();
        //            $(GlobTabStrip.items()[4]).show();
        //            $(GlobTabStrip.items()[5]).show();
        //            $(GlobTabStrip.items()[6]).show();
        //            $("#LblNatureOfBusiness").show();
        //        }
        //        $("#CompanyTypeOJK").data("kendoComboBox").value($("#ClientCategory").val());

        //    }
        //    ClearAttributes();
        //    RequiredAttributes($("#ClientCategory").data("kendoComboBox").value());
        //    RefreshTab(0);
        //}

     

        //BusinessTypeOJK
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BusinessTypeOJK",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BusinessTypeOJK").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBusinessTypeOJK,
                    value: setCmbBusinessTypeOJK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeBusinessTypeOJK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBusinessTypeOJK() {

            if (data.BusinessTypeOJK == null) {
                return "";
            } else {
                if (data.BusinessTypeOJK == 0) {
                    return "";
                } else {
                    return data.BusinessTypeOJK;
                }
            }
        }
        //NatureOfBusiness
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/NatureOfBusiness",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#NatureOfBusiness").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeNatureOfBusiness,
                    value: setCmbNatureOfBusiness()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeNatureOfBusiness() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbNatureOfBusiness() {

            if (data.NatureOfBusiness == null) {
                return "";
            } else {
                if (data.NatureOfBusiness == 0) {
                    return "";
                } else {
                    return data.NatureOfBusiness;
                }
            }
        }


        //combo box Internal Category
        $.ajax({
            url: window.location.origin + "/Radsoft/InternalCategory/GetInternalCategoryCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InternalCategoryPK").kendoComboBox({
                    dataValueField: "InternalCategoryPK",
                    dataTextField: "Name",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeInternalCategory,
                    value: setCmbInternalCategory()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeInternalCategory() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInternalCategory() {
            if (data.InternalCategoryPK == null) {
                return "";
            } else {
                if (data.InternalCategoryPK == 0) {
                    return "";
                } else {
                    return data.InternalCategoryPK;
                }
            }
        }

        //combo box Selling Agent
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SellingAgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSellingAgent,
                    value: setCmbSellingAgent()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeSellingAgent() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSellingAgent() {
            if (data.SellingAgentPK == null) {
                return "";
            } else {
                if (data.SellingAgentPK == 0) {
                    return "";
                } else {
                    return data.SellingAgentPK;
                }
            }
        }


        //combo box JenisKelamin
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Sex",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JenisKelamin").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeJenisKelamin,
                    dataSource: data,
                    value: setCmbJenisKelamin()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeJenisKelamin() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbJenisKelamin() {
            if (data.JenisKelamin == null) {
                return "";
            } else {
                if (data.JenisKelamin == 0) {
                    return "";
                } else {
                    return data.JenisKelamin;
                }
            }
        }
        //combo box StatusPerkawinan
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MaritalStatus",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#StatusPerkawinan").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeStatusPerkawinan,
                    value: setCmbStatusPerkawinan()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeStatusPerkawinan() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbStatusPerkawinan() {
            if (data.StatusPerkawinan == null) {
                return "";
            } else {
                if (data.StatusPerkawinan == 0) {
                    return "";
                } else {
                    return data.StatusPerkawinan;
                }
            }
        }
        //combo box Pekerjaan
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Occupation",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Pekerjaan").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePekerjaan,
                    value: setCmbPekerjaan()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangePekerjaan() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbPekerjaan() {
            if (data.Pekerjaan == null) {
                return "";
            } else {
                if (data.Pekerjaan == 0) {
                    return "";
                } else {
                    return data.Pekerjaan;
                }
            }
        }

        //combo box Pendidikan
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EducationalBackground",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Pendidikan").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePendidikan,
                    value: setCmbPendidikan()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangePendidikan() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPendidikan() {
            if (data.Pendidikan == null) {
                return "";
            } else {
                if (data.Pendidikan == 0) {
                    return "";
                } else {
                    return data.Pendidikan;
                }
            }
        }
        //combo box Agama
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Agama").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAgama,
                    value: setCmbAgama()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgama() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAgama() {
            if (data.Agama == null) {
                return "";
            } else {
                if (data.Agama == 0) {
                    return "";
                } else {
                    return data.Agama;
                }
            }
        }
        //combo box Penghasilan Individual
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeIND",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PenghasilanInd").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePenghasilanInd,
                    value: setCmbPenghasilanInd()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePenghasilanInd() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPenghasilanInd() {
            if (data.PenghasilanInd == null) {
                return "";
            } else {
                if (data.PenghasilanInd == 0) {
                    return "";
                } else {
                    return data.PenghasilanInd;
                }
            }
        }

        //combo box SumberDana Individual
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeSourceIND",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SumberDanaInd").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSumberDanaInd,
                    value: setCmbSumberDanaInd()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeSumberDanaInd() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSumberDanaInd() {
            if (data.SumberDanaInd == null) {
                return "";
            } else {
                if (data.SumberDanaInd == 0) {
                    return "";
                } else {
                    return data.SumberDanaInd;
                }
            }
        }
        //combo box MaksudTujuan Individual
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentObjectivesIND",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MaksudTujuanInd").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMaksudTujuanInd,
                    value: setCmbMaksudTujuanInd()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeMaksudTujuanInd() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbMaksudTujuanInd() {
            if (data.MaksudTujuanInd == null) {
                return "";
            } else {
                if (data.MaksudTujuanInd == 0) {
                    return "";
                } else {
                    return data.MaksudTujuanInd;
                }
            }
        }

        //combo box NatureOfBusiness
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HRBusiness",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#NatureOfBusiness").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeNatureOfBusiness,
                    value: setCmbNatureOfBusiness()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeNatureOfBusiness() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbNatureOfBusiness() {
            if (data.NatureOfBusiness == null) {
                return "";
            } else {
                if (data.NatureOfBusiness == 0) {
                    return "";
                } else {
                    return data.NatureOfBusiness;
                }
            }
        }

        //combo box NatureOfBusinessInsti
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HRBusiness",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#NatureOfBusinessInsti").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeNatureOfBusinessInsti,
                    value: setCmbNatureOfBusinessInsti()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeNatureOfBusinessInsti() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbNatureOfBusinessInsti() {
            if (data.NatureOfBusinessInsti == null) {
                return "";
            } else {
                if (data.NatureOfBusinessInsti == 0) {
                    return "";
                } else {
                    return data.NatureOfBusinessInsti;
                }
            }
        }

        //combo box Politis
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HrPEP",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Politis").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePolitis,
                    value: setCmbPolitis()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePolitis() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPolitis() {
            if (data.Politis == null) {
                return "";
            } else {
                if (data.Politis == 0) {
                    return "";
                } else {
                    return data.Politis;
                }
            }
        }

        //combo box Identitas
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Identity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#IdentitasInd1").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasInd1()
                });
                $("#IdentitasInd2").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasInd2()
                });
                $("#IdentitasInd3").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasInd3()
                });
                $("#IdentitasInd4").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasInd4()
                });
                $("#IdentitasIns11").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns11()
                });
                $("#IdentitasIns12").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns12()
                });
                $("#IdentitasIns13").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns13()
                });
                $("#IdentitasIns14").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns14()
                });
                $("#IdentitasIns21").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns21()
                });
                $("#IdentitasIns22").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns22()
                });
                $("#IdentitasIns23").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns23()
                });
                $("#IdentitasIns24").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns24()
                });
                $("#IdentitasIns31").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns31()
                });
                $("#IdentitasIns32").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns32()
                });
                $("#IdentitasIns33").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns33()
                });
                $("#IdentitasIns34").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns34()
                });
                $("#IdentitasIns41").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns41()
                });
                $("#IdentitasIns42").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns42()
                });
                $("#IdentitasIns43").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns43()
                });
                $("#IdentitasIns44").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentitasIns44()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //combo box SpouseOccupation
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Occupation",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SpouseOccupation").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSpouseOccupation,
                    value: setCmbSpouseOccupation()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeSpouseOccupation() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbSpouseOccupation() {
            if (data.SpouseOccupation == null) {
                return "";
            } else {
                if (data.SpouseOccupation == 0) {
                    return "";
                } else {
                    return data.SpouseOccupation;
                }
            }
        }

        //combo box SpouseNatureOfBusiness
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HRBusiness",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SpouseNatureOfBusiness").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSpouseNatureOfBusiness,
                    value: setCmbSpouseNatureOfBusiness()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeSpouseNatureOfBusiness() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSpouseNatureOfBusiness() {
            if (data.SpouseNatureOfBusiness == null) {
                return "";
            } else {
                if (data.SpouseNatureOfBusiness == 0) {
                    return "";
                } else {
                    return data.SpouseNatureOfBusiness;
                }
            }
        }
        //combo box Penghasilan Individual
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeIND",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SpouseAnnualIncome").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSpouseAnnualIncome,
                    value: setCmbSpouseAnnualIncome()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeSpouseAnnualIncome() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSpouseAnnualIncome() {
            if (data.SpouseAnnualIncome == null) {
                return "";
            } else {
                if (data.SpouseAnnualIncome == 0) {
                    return "";
                } else {
                    return data.SpouseAnnualIncome;
                }
            }
        }

        function checkIdentitySeumurHidup() {
            if ($("#IdentitasInd1").val() == 7) {
                $("#ExpiredDateIdentitasInd1").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasInd2").val() == 7) {
                $("#ExpiredDateIdentitasInd2").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasInd3").val() == 7) {
                $("#ExpiredDateIdentitasInd3").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasInd4").val() == 7) {
                $("#ExpiredDateIdentitasInd4").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns11").val() == 7) {
                $("#ExpiredDateIdentitasIns11").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns12").val() == 7) {
                $("#ExpiredDateIdentitasIns12").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns13").val() == 7) {
                $("#ExpiredDateIdentitasIns13").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns14").val() == 7) {
                $("#ExpiredDateIdentitasIns14").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns21").val() == 7) {
                $("#ExpiredDateIdentitasIns21").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns22").val() == 7) {
                $("#ExpiredDateIdentitasIns22").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns23").val() == 7) {
                $("#ExpiredDateIdentitasIns23").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns24").val() == 7) {
                $("#ExpiredDateIdentitasIns24").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns31").val() == 7) {
                $("#ExpiredDateIdentitasIns31").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns32").val() == 7) {
                $("#ExpiredDateIdentitasIns32").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns33").val() == 7) {
                $("#ExpiredDateIdentitasIns33").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns34").val() == 7) {
                $("#ExpiredDateIdentitasIns34").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns41").val() == 7) {
                $("#ExpiredDateIdentitasIns41").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns42").val() == 7) {
                $("#ExpiredDateIdentitasIns42").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns43").val() == 7) {
                $("#ExpiredDateIdentitasIns43").data("kendoDatePicker").value("31122098")
            }
            if ($("#IdentitasIns44").val() == 7) {
                $("#ExpiredDateIdentitasIns44").data("kendoDatePicker").value("31122098")
            }

        }
        function onChangeIdentitas() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                checkIdentitySeumurHidup();
            }

        }
        function setCmbIdentitasInd1() {
            if (data.IdentitasInd1 == null) {
                return "";
            } else {
                if (data.IdentitasInd1 == 0) {
                    return "";
                } else {
                    return data.IdentitasInd1;
                }
            }
        }
        function setCmbIdentitasInd2() {
            if (data.IdentitasInd2 == null) {
                return "";
            } else {
                if (data.IdentitasInd2 == 0) {
                    return "";
                } else {
                    return data.IdentitasInd2;
                }
            }
        }
        function setCmbIdentitasInd3() {
            if (data.IdentitasInd3 == null) {
                return "";
            } else {
                if (data.IdentitasInd3 == 0) {
                    return "";
                } else {
                    return data.IdentitasInd3;
                }
            }
        }
        function setCmbIdentitasInd4() {
            if (data.IdentitasInd4 == null) {
                return "";
            } else {
                if (data.IdentitasInd4 == 0) {
                    return "";
                } else {
                    return data.IdentitasInd4;
                }
            }
        }
        function setCmbIdentitasIns11() {
            if (data.IdentitasIns11 == null) {
                return "";
            } else {
                if (data.IdentitasIns11 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns11;
                }
            }
        }
        function setCmbIdentitasIns12() {
            if (data.IdentitasIns12 == null) {
                return "";
            } else {
                if (data.IdentitasIns12 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns12;
                }
            }
        }
        function setCmbIdentitasIns13() {
            if (data.IdentitasIns13 == null) {
                return "";
            } else {
                if (data.IdentitasIns13 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns13;
                }
            }
        }
        function setCmbIdentitasIns14() {
            if (data.IdentitasIns14 == null) {
                return "";
            } else {
                if (data.IdentitasIns14 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns14;
                }
            }
        }
        function setCmbIdentitasIns21() {
            if (data.IdentitasIns21 == null) {
                return "";
            } else {
                if (data.IdentitasIns21 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns21;
                }
            }
        }
        function setCmbIdentitasIns22() {
            if (data.IdentitasIns22 == null) {
                return "";
            } else {
                if (data.IdentitasIns22 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns22;
                }
            }
        }
        function setCmbIdentitasIns23() {
            if (data.IdentitasIns23 == null) {
                return "";
            } else {
                if (data.IdentitasIns23 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns23;
                }
            }
        }
        function setCmbIdentitasIns24() {
            if (data.IdentitasIns24 == null) {
                return "";
            } else {
                if (data.IdentitasIns24 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns24;
                }
            }
        }
        function setCmbIdentitasIns31() {
            if (data.IdentitasIns31 == null) {
                return "";
            } else {
                if (data.IdentitasIns31 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns31;
                }
            }
        }
        function setCmbIdentitasIns32() {
            if (data.IdentitasIns32 == null) {
                return "";
            } else {
                if (data.IdentitasIns32 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns32;
                }
            }
        }
        function setCmbIdentitasIns33() {
            if (data.IdentitasIns33 == null) {
                return "";
            } else {
                if (data.IdentitasIns33 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns33;
                }
            }
        }
        function setCmbIdentitasIns34() {
            if (data.IdentitasIns34 == null) {
                return "";
            } else {
                if (data.IdentitasIns34 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns34;
                }
            }
        }
        function setCmbIdentitasIns41() {
            if (data.IdentitasIns41 == null) {
                return "";
            } else {
                if (data.IdentitasIns41 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns41;
                }
            }
        }
        function setCmbIdentitasIns42() {
            if (data.IdentitasIns42 == null) {
                return "";
            } else {
                if (data.IdentitasIns42 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns42;
                }
            }
        }
        function setCmbIdentitasIns43() {
            if (data.IdentitasIns43 == null) {
                return "";
            } else {
                if (data.IdentitasIns43 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns43;
                }
            }
        }
        function setCmbIdentitasIns44() {
            if (data.IdentitasIns44 == null) {
                return "";
            } else {
                if (data.IdentitasIns44 == 0) {
                    return "";
                } else {
                    return data.IdentitasIns44;
                }
            }
        }

        //combo box Domisili
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Domicile",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Domisili").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeDomisili,
                    value: setCmbDomisili()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeDomisili() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDomisili() {
            if (data.Domisili == null) {
                return "";
            } else {
                if (data.Domisili == 0) {
                    return "";
                } else {
                    return data.Domisili;
                }
            }
        }
        //combo box Tipe
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CompanyType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Tipe").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTipe,
                    value: setCmbTipe()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeTipe() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbTipe() {
            if (data.Tipe == null) {
                return "";
            } else {
                if (data.Tipe == 0) {
                    return "";
                } else {
                    return data.Tipe;
                }
            }
        }



        //combo box Karakteristik
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CompanyCharacteristic",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Karakteristik").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeKarakteristik,
                    value: setCmbKarakteristik()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeKarakteristik() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbKarakteristik() {
            if (data.Karakteristik == null) {
                return "";
            } else {
                if (data.Karakteristik == 0) {
                    return "";
                } else {
                    return data.Karakteristik;
                }
            }
        }

        //combo box PenghasilanInstitusi
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeINS",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PenghasilanInstitusi").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePenghasilanInstitusi,
                    value: setCmbPenghasilanInstitusi()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePenghasilanInstitusi() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbPenghasilanInstitusi() {
            if (data.PenghasilanInstitusi == null) {
                return "";
            } else {
                if (data.PenghasilanInstitusi == 0) {
                    return "";
                } else {
                    return data.PenghasilanInstitusi;
                }
            }
        }

        //combo box SumberDanaInstitusi
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeSourceINS",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SumberDanaInstitusi").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSumberDanaInstitusi,
                    value: setCmbSumberDanaInstitusi()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeSumberDanaInstitusi() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSumberDanaInstitusi() {
            if (data.SumberDanaInstitusi == null) {
                return "";
            } else {
                if (data.SumberDanaInstitusi == 0) {
                    return "";
                } else {
                    return data.SumberDanaInstitusi;
                }
            }
        }
        //combo box MaksudTujuanInstitusi
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentObjectivesINS",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MaksudTujuanInstitusi").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMaksudTujuanInstitusi,
                    value: setCmbMaksudTujuanInstitusi()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeMaksudTujuanInstitusi() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMaksudTujuanInstitusi() {
            if (data.MaksudTujuanInstitusi == null) {
                return "";
            } else {
                if (data.MaksudTujuanInstitusi == 0) {
                    return "";
                } else {
                    return data.MaksudTujuanInstitusi;
                }
            }
        }

        //combo box AssetFor1Year
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AssetINS",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AssetFor1Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAsset,
                    value: setCmbAssetFor1Year()
                });
                $("#AssetFor2Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAsset,
                    value: setCmbAssetFor2Year()
                });
                $("#AssetFor3Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAsset,
                    value: setCmbAssetFor3Year()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAsset() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAssetFor1Year() {
            if (data.AssetFor1Year == null) {
                return "";
            } else {
                if (data.AssetFor1Year == 0) {
                    return "";
                } else {
                    return data.AssetFor1Year;
                }
            }
        }
        function setCmbAssetFor2Year() {
            if (data.AssetFor2Year == null) {
                return "";
            } else {
                if (data.AssetFor2Year == 0) {
                    return "";
                } else {
                    return data.AssetFor2Year;
                }
            }
        }
        function setCmbAssetFor3Year() {
            if (data.AssetFor3Year == null) {
                return "";
            } else {
                if (data.AssetFor3Year == 0) {
                    return "";
                } else {
                    return data.AssetFor3Year;
                }
            }
        }

        //combo box OperatingProfitFor1Year
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/IncomeINS",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#OperatingProfitFor1Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeOperatingProfit,
                    value: setCmbOperatingProfitFor1Year()
                });
                $("#OperatingProfitFor2Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeOperatingProfit,
                    value: setCmbOperatingProfitFor2Year()
                });
                $("#OperatingProfitFor3Year").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeOperatingProfit,
                    value: setCmbOperatingProfitFor3Year()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeOperatingProfit() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbOperatingProfitFor1Year() {
            if (data.OperatingProfitFor1Year == null) {
                return "";
            } else {
                if (data.OperatingProfitFor1Year == 0) {
                    return "";
                } else {
                    return data.OperatingProfitFor1Year;
                }
            }
        }
        function setCmbOperatingProfitFor2Year() {
            if (data.OperatingProfitFor2Year == null) {
                return "";
            } else {
                if (data.OperatingProfitFor2Year == 0) {
                    return "";
                } else {
                    return data.OperatingProfitFor2Year;
                }
            }
        }
        function setCmbOperatingProfitFor3Year() {
            if (data.OperatingProfitFor3Year == null) {
                return "";
            } else {
                if (data.OperatingProfitFor3Year == 0) {
                    return "";
                } else {
                    return data.OperatingProfitFor3Year;
                }
            }
        }

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
                    change: onChangeMataUang,
                    value: setCmbMataUang1()
                });
                $("#MataUang2").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMataUang,
                    value: setCmbMataUang2()
                });
                $("#MataUang3").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMataUang,
                    value: setCmbMataUang3()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeMataUang() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMataUang1() {
            if (data.MataUang1 == null) {
                return "";
            } else {
                if (data.MataUang1 == 0) {
                    return "";
                } else {
                    return data.MataUang1;
                }
            }
        }
        function setCmbMataUang2() {
            if (data.MataUang2 == null) {
                return "";
            } else {
                if (data.MataUang2 == 0) {
                    return "";
                } else {
                    return data.MataUang2;
                }
            }
        }
        function setCmbMataUang3() {
            if (data.MataUang3 == null) {
                return "";
            } else {
                if (data.MataUang3 == 0) {
                    return "";
                } else {
                    return data.MataUang3;
                }
            }
        }


        //combo box Investors Risk Profile
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestorsRiskProfile",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InvestorsRiskProfile").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeInvestorsRiskProfile,
                    value: setCmbInvestorsRiskProfile()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeInvestorsRiskProfile() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInvestorsRiskProfile() {
            if (data.InvestorsRiskProfile == null) {
                return "";
            } else {
                if (data.InvestorsRiskProfile == 0) {
                    return "";
                } else {
                    return data.InvestorsRiskProfile;
                }
            }
        }

        //combo box Asset Owner
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AssetOwner",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AssetOwner").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAssetOwner,
                    value: setCmbAssetOwner()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeAssetOwner() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAssetOwner() {
            if (data.AssetOwner == null) {
                return "";
            } else {
                if (data.AssetOwner == 0) {
                    return "";
                } else {
                    return data.AssetOwner;
                }
            }
        }

        //combo box KYC Risk Profile
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/KYCRiskProfile",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#KYCRiskProfile").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enable: false,
                    change: onChangeKYCRiskProfile,
                    value: setCmbKYCRiskProfile()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeKYCRiskProfile() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbKYCRiskProfile() {
            if (data.KYCRiskProfile == null) {
                return "";
            } else {
                if (data.KYCRiskProfile == 0) {
                    return "";
                } else {
                    return data.KYCRiskProfile;
                }
            }
        }


        //combo box Statement Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/StatementType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#StatementType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeStatementType,
                    value: setCmbStatementType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeStatementType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbStatementType() {
            if (data.StatementType == null) {
                return "";
            } else {
                if (data.StatementType == 0) {
                    return "";
                } else {
                    return data.StatementType;
                }
            }
        }

        //combo box FATCA
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FATCA",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FATCA").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFATCA,
                    value: setCmbFATCA()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeFATCA() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFATCA() {
            if (data.FATCA == null) {
                return "";
            } else {
                if (data.FATCA == 0) {
                    return "";
                } else {
                    return data.FATCA;
                }
            }
        }


        //Combo Box Bank Name 
        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRDNPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankRDNPK,
                    value: setCmbBankRDNPK()
                });

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
            },
            error: function (data) {
                alertify.error(data.responseText);
            }
        });
        function onChangeBankRDNPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBankRDNPK() {
            if (data.BankRDNPK == null) {
                return "";
            } else {
                if (data.BankRDNPK == 0) {
                    return "";
                } else {
                    return data.BankRDNPK;
                }
            }
        }

        function onChangeNamaBank1() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else if ($("#NamaBank1").data("kendoComboBox").value() != "") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank1").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#BIMemberCode1").val(data);
                    },
                    error: function (data) {
                        alert(data.responseText);
                    }
                });
            }

        }
        function onChangeNamaBank2() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else if ($("#NamaBank2").data("kendoComboBox").value() != "") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank2").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#BIMemberCode2").val(data);
                    },
                    error: function (data) {
                        alert(data.responseText);
                    }
                });
            }

        }
        function onChangeNamaBank3() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else if ($("#NamaBank3").data("kendoComboBox").value() != "") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#NamaBank3").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#BIMemberCode3").val(data);
                    },
                    error: function (data) {
                        alert(data.responseText);
                    }
                });
            }

        }
        function setCmbNamaBank1() {
            if (data.NamaBank1 == null) {
                return "";
            } else {
                if (data.NamaBank1 == 0) {
                    return "";
                } else {
                    return data.NamaBank1;
                }
            }
        }
        function setCmbNamaBank2() {
            if (data.NamaBank2 == null) {
                return "";
            } else {
                if (data.NamaBank2 == 0) {
                    return "";
                } else {
                    return data.NamaBank2;
                }
            }
        }
        function setCmbNamaBank3() {
            if (data.NamaBank3 == null) {
                return "";
            } else {
                if (data.NamaBank3 == 0) {
                    return "";
                } else {
                    return data.NamaBank3;
                }
            }
        }


        var loadedTabs = [0];
        $("#TabSub").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {

                subtabindex = $(e.item).index();
            }
        }).data("kendoTabStrip").select(0);



        win.center();
        win.open();
    }

    var GlobValidator = $("#WinFundClient").kendoValidator().data("kendoValidator");

    function validateData() {
        //resetNotification();


        if ($("#NamaDepanInd").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for First Name");
            return 0;
        }
        if ($("#NamaTengahInd").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Middle Name");
            return 0;
        }
        if ($("#NamaBelakangInd").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Last Name");
            return 0;
        }
        if ($("#TempatLahir").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Birth Place");
            return 0;
        }
        if ($("#AlamatInd1").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for Correspondence Address (1)");
            return 0;
        }
        if ($("#AlamatInd2").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for Correspondence Address (2)");
            return 0;
        }

        if ($("#SpouseName").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Spouse Name");
            return 0;
        }
        if ($("#MotherMaidenName").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Mother Maiden Name");
            return 0;
        }
        if ($("#AhliWaris").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Heir");
            return 0;
        }
        if ($("#HubunganAhliWaris").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Heir Relation");
            return 0;
        }
        //if ($("#NatureOfBusinessLainnya").val().length > 100) {
        //    alertify.alert("Validation not Pass, char more than 100 for N.O.B (Text)");
        //    return 0;
        //}
        if ($("#PolitisLainnya").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for P.E (Text)");
            return 0;
        }
        if ($("#OtherTeleponRumah").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Other Home Phone");
            return 0;
        }
        if ($("#OtherTeleponSelular").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Other Cell Phone");
            return 0;
        }
        if ($("#OtherFax").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Other Fax");
            return 0;
        }
        if ($("#OtherEmail").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Other Email");
            return 0;
        }
        if ($("#OtherAlamatInd1").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Identity Address (1)");
            return 0;
        }
        if ($("#OtherAlamatInd2").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Identity Address (2)");
            return 0;
        }
        if ($("#OtherAlamatInd3").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Identity Address (3)");
            return 0;
        }


        if ($("#NoIdentitasInd1").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasInd2").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasInd3").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasInd4").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (4)");
            return 0;
        }


        if ($("#Name").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for Internal Name");
            return 0;
        }
        if ($("#SID").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for SID");
            return 0;
        }
        if ($("#IFUACode").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for IFUACode");
            return 0;
        }
        if ($("#NPWP").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for NPWP");
            return 0;
        }
        if ($("#SACode").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for SACode");
            return 0;
        }
        if ($("#Email").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Email");
            return 0;
        }
        if ($("#TeleponRumah").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Phone");
            return 0;
        }
        if ($("#TeleponSelular").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Cell Phone");
            return 0;
        }
        if ($("#Fax").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Fax");
            return 0;
        }
        if ($("#Description").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Description");
            return 0;
        }



        if ($("#NamaPerusahaan").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Company Name");
            return 0;
        }
        if ($("#AlamatPerusahaan").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for Address");
            return 0;
        }
        if ($("#NPWPPerson1").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Authorized Person 1 - NPWP No");
            return 0;
        }
        if ($("#NPWPPerson2").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Authorized Person 2 - NPWP No");
            return 0;
        }
        if ($("#NoSKD").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for SKD Number ");
            return 0;
        }
        if ($("#LokasiBerdiri").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Establishment Place ");
            return 0;
        }
        if ($("#NomorAnggaran").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Article of Association");
            return 0;
        }
        if ($("#NomorSIUP").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for SIUP Number");
            return 0;
        }

        if ($("#NamaDepanIns1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 First Name");
            return 0;
        }
        if ($("#NamaTengahIns1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 Middle Name");
            return 0;
        }
        if ($("#NamaBelakangIns1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 Last Name");
            return 0;
        }
        if ($("#Jabatan1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 Position");
            return 0;
        }
        if ($("#PhoneIns1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 Phone Number");
            return 0;
        }
        if ($("#EmailIns1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 1 Email");
            return 0;
        }
        if ($("#NoIdentitasIns11").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 1 Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasIns12").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 1 Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasIns13").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 1 Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasIns14").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 1 Identity Number (4)");
            return 0;
        }


        if ($("#NamaDepanIns2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 First Name");
            return 0;
        }
        if ($("#NamaTengahIns2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 Middle Name");
            return 0;
        }
        if ($("#NamaBelakangIns2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 Last Name");
            return 0;
        }
        if ($("#Jabatan2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 Position");
            return 0;
        }
        if ($("#PhoneIns2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 Phone Number");
            return 0;
        }
        if ($("#EmailIns2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 2 Email");
            return 0;
        }
        if ($("#NoIdentitasIns21").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 2 Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasIns22").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 2 Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasIns23").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 2 Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasIns24").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 2 Identity Number (4)");
            return 0;
        }


        if ($("#NamaDepanIns3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 3 First Name");
            return 0;
        }
        if ($("#NamaTengahIns3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 3 Middle Name");
            return 0;
        }
        if ($("#NamaBelakangIns3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 3 Last Name");
            return 0;
        }
        if ($("#Jabatan3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 3 Position");
            return 0;
        }
        if ($("#NoIdentitasIns31").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 3 Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasIns32").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 3 Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasIns33").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 3 Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasIns34").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 3 Identity Number (4)");
            return 0;
        }


        if ($("#NamaDepanIns4").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 4 First Name");
            return 0;
        }
        if ($("#NamaTengahIns4").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 4 Middle Name");
            return 0;
        }
        if ($("#NamaBelakangIns4").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 4 Last Name");
            return 0;
        }
        if ($("#Jabatan4").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Officer 4 Position");
            return 0;
        }

        if ($("#NoIdentitasIns41").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 4 Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasIns42").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 4 Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasIns43").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 4 Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasIns44").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for Officer 4 Identity Number (4)");
            return 0;
        }

        if ($("#NomorRekening1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Bank Account No 1");
            return 0;
        }
        if ($("#NamaNasabah1").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 1");
            return 0;
        }
        if ($("#BICCode1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BIC Code Name 1");
            return 0;
        }
        if ($("#BIMemberCode1").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BI Member Code 1");
            return 0;
        }
        if ($("#BankBranchName1").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for BankBranch 1");
            return 0;
        }



        if ($("#NomorRekening2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Bank Account No 2");
            return 0;
        }
        if ($("#NamaNasabah2").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 2");
            return 0;
        }
        if ($("#BICCode2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BIC Code Name 2");
            return 0;
        }
        if ($("#BIMemberCode2").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BI Member Code 2");
            return 0;
        }
        if ($("#BankBranchName2").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for BankBranch 2");
            return 0;
        }



        if ($("#NomorRekening3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Bank Account No 3");
            return 0;
        }
        if ($("#NamaNasabah3").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 3");
            return 0;
        }
        if ($("#BICCode3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BIC Code Name 3");
            return 0;
        }
        if ($("#BIMemberCode3").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for BI Member Code 3");
            return 0;
        }
        if ($("#BankBranchName3").val().length > 20) {
            alertify.alert("Validation not Pass, char more than 20 for BankBranch 3");
            return 0;
        }


        if ($("#SubstantialOwnerName").val().length > 70) {
            alertify.alert("Validation not Pass, char more than 70 for Substantial U.S. Owner Name");
            return 0;
        }

        if ($("#SubstantialOwnerAddress").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Substantial U.S. Owner Address");
            return 0;
        }

        if ($("#SubstantialOwnerTIN").val().length > 70) {
            alertify.alert("Validation not Pass, char more than 70 for Substantial U.S. Owner TIN");
            return 0;
        }
        if ($("#SpouseBirthPlace").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Birth Place");
            return 0;
        }
        if ($("#SpouseIDNo").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (1)");
            return 0;
        }

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }

        //return 1;
    }

    function showDetails(e) {

        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#BtnCopyClient").hide();
            $("#StatusHeader").val("NEW");
            var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $(GlobTabStrip.items()[3]).hide();
            $(GlobTabStrip.items()[4]).hide();
            $(GlobTabStrip.items()[5]).show();
            RefreshTab(0);
            ClearAttributes();
            setFundClient(false);
        } else {

            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            var dataItemX;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridFundClientApproved").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                initGridApprove(dataItemX.FundClientPK);
                return;
            }
            else if (tabindex == 1) {
                grid = $("#gridFundClientPending").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                initGridPending(dataItemX.FundClientPK);

            }

            else if (tabindex == 2) {
                grid = $("#gridFundClientHistory").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                initGridHistory(dataItemX.FundClientPK, dataItemX.HistoryPK);
                return;
            }
        }

    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        clearDataInd();
        clearDataIns();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundClientPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#OldID").val("");
        $("#Name").val("");
        $("#ClientCategory").val("");
        $("#InvestorType").val("");
        $("#BusinessTypeOJK").val("");
        //$("#NatureofBusiness").val("");
        $("#InternalCategoryPK").val("");
        $("#SellingAgentPK").val("");
        $("#SID").val("");
        $("#IFUACode").val("");
        $("#Child").val("");
        $("#CompanyTypeOJK").val("");
        $("#CompanyTypeOJKDesc").val("");
        $("#BusinessTypeOJK").val("");
        $("#BusinessTypeOJKDesc").val("");
        //$("#NatureOfBusiness").val("");
        //$("#NatureOfBusinessDesc").val("");
        $("#ARIA").val("");
        $("#Registered").val("");
        $("#JumlahDanaAwal").val("");
        $("#JumlahDanaSaatIniCash").val("");
        $("#JumlahDanaSaatIni").val("");
        $("#Negara").val("");
        $("#NegaraDesc").val("");
        $("#Nationality").val("");
        $("#NationalityDesc").val("");
        $("#NPWP").val("");
        $("#SACode").val("");
        $("#Propinsi").val("");
        $("#PropinsiDesc").val("");
        $("#TeleponSelular").val("");
        $("#Email").val("");
        $("#Fax").val("");
        $("#DormantDate").val("");
        $("#Description").val("");
        $("#JumlahBank").val("");
        $("#NamaBank1").val("");
        $("#NomorRekening1").val("");
        $("#BankBranchName1").val("");
        $("#BICCode1").val("");
        $("#NamaNasabah1").val("");
        $("#MataUang1").val("");
        $("#NamaBank2").val("");
        $("#NomorRekening2").val("");
        $("#BankBranchName2").val("");
        $("#BICCode2").val("");
        $("#NamaNasabah2").val("");
        $("#MataUang2").val("");
        $("#NamaBank3").val("");
        $("#NomorRekening3").val("");
        $("#BankBranchName3").val("");
        $("#BICCode3").val("");
        $("#NamaNasabah3").val("");
        $("#MataUang3").val("");
        $("#NamaDepanInd").val("");
        $("#NamaTengahInd").val("");
        $("#NamaBelakangInd").val("");
        $("#TempatLahir").val("");
        $("#TanggalLahir").val("");
        $("#JenisKelamin").val("");
        $("#StatusPerkawinan").val("");
        $("#Pekerjaan").val("");
        $("#Pendidikan").val("");
        $("#Agama").val("");
        $("#PenghasilanInd").val("");
        $("#SumberDanaInd").val("");
        $("#MaksudTujuanInd").val("");
        $("#AlamatInd1").val("");
        $("#KodeKotaInd1").val("");
        $("#KodeKotaInd1Desc").val("");
        $("#KodePosInd1").val("");
        $("#AlamatInd2").val("");
        $("#KodeKotaInd2").val("");
        $("#KodeKotaInd2Desc").val("");
        $("#KodePosInd2").val("");
        $("#NamaPerusahaan").val("");
        $("#Domisili").val("");
        $("#Tipe").val("");
        //$("#TipeOjk").val("");
        $("#Karakteristik").val("");
        $("#NoSKD").val("");
        $("#PenghasilanInstitusi").val("");
        $("#SumberDanaInstitusi").val("");
        $("#MaksudTujuanInstitusi").val("");
        $("#AlamatPerusahaan").val("");
        $("#KodeKotaIns").val("");
        $("#KodeKotaInsDesc").val("");
        $("#KodePosIns").val("");
        $("#SpouseName").val("");
        $("#MotherMaidenName").val("");
        $("#AhliWaris").val("");
        $("#HubunganAhliWaris").val("");
        $("#NatureOfBusiness").val("");
        $("#NatureOfBusinessDesc").val("");
        $("#Politis").val("");
        $("#PolitisLainnya").val("");
        $("#TeleponRumah").val("");
        $("#OtherAlamatInd1").val("");
        $("#OtherKodeKotaInd1").val("");
        $("#OtherKodeKotaInd1Desc").val("");
        $("#OtherKodePosInd1").val("");
        $("#OtherPropinsiInd1").val("");
        $("#OtherNegaraInd1").val("");
        $("#OtherAlamatInd2").val("");
        $("#OtherKodeKotaInd2").val("");
        $("#OtherKodeKotaInd2Desc").val("");
        $("#OtherKodePosInd2").val("");
        $("#OtherPropinsiInd2").val("");
        $("#OtherNegaraInd2").val("");
        $("#OtherAlamatInd3").val("");
        $("#OtherKodeKotaInd3").val("");
        $("#OtherKodeKotaInd3Desc").val("");
        $("#OtherKodePosInd3").val("");
        $("#OtherPropinsiInd3").val("");
        $("#OtherNegaraInd3").val("");
        $("#OtherTeleponRumah").val("");
        $("#OtherTeleponSelular").val("");
        $("#OtherEmail").val("");
        $("#OtherFax").val("");
        $("#JumlahIdentitasInd").val("");
        $("#IdentitasInd1").val("");
        $("#NoIdentitasInd1").val("");
        $("#RegistrationDateIdentitasInd1").val("");
        $("#ExpiredDateIdentitasInd1").val("");
        $("#IdentitasInd2").val("");
        $("#NoIdentitasInd2").val("");
        $("#RegistrationDateIdentitasInd2").val("");
        $("#ExpiredDateIdentitasInd2").val("");
        $("#IdentitasInd3").val("");
        $("#NoIdentitasInd3").val("");
        $("#RegistrationDateIdentitasInd3").val("");
        $("#ExpiredDateIdentitasInd3").val("");
        $("#IdentitasInd4").val("");
        $("#NoIdentitasInd4").val("");
        $("#RegistrationDateIdentitasInd4").val("");
        $("#ExpiredDateIdentitasInd4").val("");
        $("#RegistrationNPWP").val("");
        $("#ExpiredDateSKD").val("");
        $("#TanggalBerdiri").val("");
        $("#LokasiBerdiri").val("");
        $("#TeleponBisnis").val("");
        $("#NomorAnggaran").val("");
        $("#NomorSIUP").val("");
        $("#AssetFor1Year").val("");
        $("#AssetFor2Year").val("");
        $("#AssetFor3Year").val("");
        $("#OperatingProfitFor1Year").val("");
        $("#OperatingProfitFor2Year").val("");
        $("#OperatingProfitFor3Year").val("");
        $("#JumlahPejabat").val("");
        $("#NamaDepanIns1").val("");
        $("#NamaTengahIns1").val("");
        $("#NamaBelakangIns1").val("");
        $("#Jabatan1").val("");
        $("#JumlahIdentitasIns1").val("");
        $("#IdentitasIns11").val("");
        $("#NoIdentitasIns11").val("");
        $("#RegistrationDateIdentitasIns11").val("");
        $("#ExpiredDateIdentitasIns11").val("");
        $("#IdentitasIns12").val("");
        $("#NoIdentitasIns12").val("");
        $("#RegistrationDateIdentitasIns12").val("");
        $("#ExpiredDateIdentitasIns12").val("");
        $("#IdentitasIns13").val("");
        $("#NoIdentitasIns13").val("");
        $("#RegistrationDateIdentitasIns13").val("");
        $("#ExpiredDateIdentitasIns13").val("");
        $("#IdentitasIns14").val("");
        $("#NoIdentitasIns14").val("");
        $("#RegistrationDateIdentitasIns14").val("");
        $("#ExpiredDateIdentitasIns14").val("");
        $("#NamaDepanIns2").val("");
        $("#NamaTengahIns2").val("");
        $("#NamaBelakangIns2").val("");
        $("#Jabatan2").val("");
        $("#JumlahIdentitasIns2").val("");
        $("#IdentitasIns21").val("");
        $("#NoIdentitasIns21").val("");
        $("#RegistrationDateIdentitasIns21").val("");
        $("#ExpiredDateIdentitasIns21").val("");
        $("#IdentitasIns22").val("");
        $("#NoIdentitasIns22").val("");
        $("#RegistrationDateIdentitasIns22").val("");
        $("#ExpiredDateIdentitasIns22").val("");
        $("#IdentitasIns23").val("");
        $("#NoIdentitasIns23").val("");
        $("#RegistrationDateIdentitasIns23").val("");
        $("#ExpiredDateIdentitasIns23").val("");
        $("#IdentitasIns24").val("");
        $("#NoIdentitasIns24").val("");
        $("#RegistrationDateIdentitasIns24").val("");
        $("#ExpiredDateIdentitasIns24").val("");
        $("#NamaDepanIns3").val("");
        $("#NamaTengahIns3").val("");
        $("#NamaBelakangIns3").val("");
        $("#Jabatan3").val("");
        $("#JumlahIdentitasIns3").val("");
        $("#IdentitasIns31").val("");
        $("#NoIdentitasIns31").val("");
        $("#RegistrationDateIdentitasIns31").val("");
        $("#ExpiredDateIdentitasIns31").val("");
        $("#IdentitasIns32").val("");
        $("#NoIdentitasIns32").val("");
        $("#RegistrationDateIdentitasIns32").val("");
        $("#ExpiredDateIdentitasIns32").val("");
        $("#IdentitasIns33").val("");
        $("#NoIdentitasIns33").val("");
        $("#RegistrationDateIdentitasIns33").val("");
        $("#ExpiredDateIdentitasIns33").val("");
        $("#IdentitasIns34").val("");
        $("#NoIdentitasIns34").val("");
        $("#RegistrationDateIdentitasIns34").val("");
        $("#ExpiredDateIdentitasIns34").val("");
        $("#NamaDepanIns4").val("");
        $("#NamaTengahIns4").val("");
        $("#NamaBelakangIns4").val("");
        $("#Jabatan4").val("");
        $("#JumlahIdentitasIns4").val("");
        $("#IdentitasIns41").val("");
        $("#NoIdentitasIns41").val("");
        $("#RegistrationDateIdentitasIns41").val("");
        $("#ExpiredDateIdentitasIns41").val("");
        $("#IdentitasIns42").val("");
        $("#NoIdentitasIns42").val("");
        $("#RegistrationDateIdentitasIns42").val("");
        $("#ExpiredDateIdentitasIns42").val("");
        $("#IdentitasIns43").val("");
        $("#NoIdentitasIns43").val("");
        $("#RegistrationDateIdentitasIns43").val("");
        $("#ExpiredDateIdentitasIns43").val("");
        $("#IdentitasIns44").val("");
        $("#NoIdentitasIns44").val("");
        $("#RegistrationDateIdentitasIns44").val("");
        $("#ExpiredDateIdentitasIns44").val("");
        $("#BIMemberCode1").val("");
        $("#BIMemberCode2").val("");
        $("#BIMemberCode3").val("");
        $("#PhoneIns1").val("");
        $("#EmailIns1").val("");
        $("#PhoneIns2").val("");
        $("#EmailIns2").val("");
        $("#InvestorsRiskProfile").val("");
        $("#RiskProfileScore").val("");
        $("#KYCRiskProfile").val("");
        $("#KYCRiskProfileDesc").val("");
        $("#InvestorsRiskProfileDesc").val("");
        $("#AssetOwner").val("");
        $("#AssetOwnerDesc").val("");
        $("#StatementType").val("");
        $("#StatementTypeDesc").val("");
        $("#FATCA").val("");
        $("#FATCADesc").val("");
        $("#TIN").val("");
        $("#TINIssuanceCountry").val("");
        $("#TINIssuanceCountryDesc").val("");
        $("#GIIN").val("");
        $("#SubstantialOwnerName").val("");
        $("#SubstantialOwnerAddress").val("");
        $("#SubstantialOwnerTIN").val("");
        $("#BankCountry1").val("");
        $("#BankCountry1Desc").val("");
        $("#BankCountry2").val("");
        $("#BankCountry2Desc").val("");
        $("#BankCountry3").val("");
        $("#BankCountry3Desc").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#BitIsSuspend").text("");
        $("#SuspendBy").val("");
        $("#SuspendTime").val("");
        $("#UnSuspendBy").val("");
        $("#UnSuspendTime").val("");
        $("#SpouseBirthPlace").val("");
        $("#SpouseDateOfBirth ").val(""); //yang ini
        $("#SpouseOccupation ").val("");
        $("#SpouseNatureOfBusiness").val("");
        $("#SpouseNatureOfBusinessOther").val("");
        $("#SpouseIDNo").val("");
        $("#SpouseNationality").val("");
        $("#SpouseNationalityDesc").val("");
        $("#SpouseAnnualIncome").val("");
    }

    function clearDataInd() {
        $("#NamaDepanInd").val("");
        $("#NamaTengahInd").val("");
        $("#NamaBelakangInd").val("");
        $("#TempatLahir").val("");
        $("#TanggalLahir").val("");
        $("#JenisKelamin").val("");
        $("#StatusPerkawinan").val("");
        $("#Pekerjaan").val("");
        $("#Pendidikan").val("");
        $("#Agama").val("");
        $("#PenghasilanInd").val("");
        $("#SumberDanaInd").val("");
        $("#MaksudTujuanInd").val("");
        $("#AlamatInd1").val("");
        $("#KodeKotaInd1").val("");
        $("#KodeKotaInd1Desc").val("");
        $("#KodePosInd1").val("");
        $("#AlamatInd2").val("");
        $("#KodeKotaInd2").val("");
        $("#KodeKotaInd2Desc").val("");
        $("#KodePosInd2").val("");
        $("#SpouseName").val("");
        $("#MotherMaidenName").val("");
        $("#AhliWaris").val("");
        $("#HubunganAhliWaris").val("");
        $("#NatureOfBusiness").val("");
        $("#NatureOfBusinessDesc").val("");
        $("#Politis").val("");
        $("#PolitisLainnya").val("");
        $("#OtherAlamatInd1").val("");
        $("#OtherKodeKotaInd1").val("");
        $("#OtherKodeKotaInd1Desc").val("");
        $("#OtherKodePosInd1").val("");
        $("#OtherPropinsiInd1").val("");
        $("#OtherPropinsiInd1Desc").val("");
        $("#CountryOfBirth").val("");
        $("#CountryOfBirthDesc").val("");
        $("#OtherNegaraInd1").val("");
        $("#OtherNegaraInd1Desc").val("");
        $("#OtherAlamatInd2").val("");
        $("#OtherKodeKotaInd2").val("");
        $("#OtherKodeKotaInd2Desc").val("");
        $("#OtherKodePosInd2").val("");
        $("#OtherPropinsiInd2").val("");
        $("#OtherPropinsiInd2Desc").val("");
        $("#OtherNegaraInd2").val("");
        $("#OtherNegaraInd2Desc").val("");
        $("#OtherAlamatInd3").val("");
        $("#OtherKodeKotaInd3").val("");
        $("#OtherKodeKotaInd3Desc").val("");
        $("#OtherKodePosInd3").val("");
        $("#OtherPropinsiInd3").val("");
        $("#OtherPropinsiInd3Desc").val("");
        $("#OtherNegaraInd3").val("");
        $("#OtherNegaraInd3Desc").val("");
        $("#OtherTeleponRumah").val("");
        $("#OtherTeleponSelular").val("");
        $("#OtherEmail").val("");
        $("#OtherFax").val("");
        $("#JumlahIdentitasInd").val("");
        $("#IdentitasInd1").val("");
        $("#NoIdentitasInd1").val("");
        $("#RegistrationDateIdentitasInd1").val("");
        $("#ExpiredDateIdentitasInd1").val("");
        $("#IdentitasInd2").val("");
        $("#NoIdentitasInd2").val("");
        $("#RegistrationDateIdentitasInd2").val("");
        $("#ExpiredDateIdentitasInd2").val("");
        $("#IdentitasInd3").val("");
        $("#NoIdentitasInd3").val("");
        $("#RegistrationDateIdentitasInd3").val("");
        $("#ExpiredDateIdentitasInd3").val("");
        $("#IdentitasInd4").val("");
        $("#NoIdentitasInd4").val("");
        $("#RegistrationDateIdentitasInd4").val("");
        $("#ExpiredDateIdentitasInd4").val("");
        $("#CountryofCorrespondence").val("");
        $("#CountryofCorrespondenceDesc").val("");
        $("#CountryofDomicile").val("");
        $("#CountryofDomicileDesc").val("");
        $("#SpouseBirthPlace").val(""); //yang ini
        $("#SpouseDateOfBirth ").val("");
        $("#SpouseOccupation ").val("");
        $("#SpouseNatureOfBusiness").val("");
        $("#SpouseNatureOfBusinessOther").val("");
        $("#SpouseIDNo").val("");
        $("#SpouseNationality").val("");
        $("#SpouseNationalityDesc").val("");
        $("#SpouseAnnualIncome").val("");

    }

    function clearDataIns() {
        $("#NamaPerusahaan").val("");
        $("#Domisili").val("");
        $("#Tipe").val("");
        //$("#TipeOjk").val("");
        $("#Karakteristik").val("");
        $("#NoSKD").val("");
        $("#PenghasilanInstitusi").val("");
        $("#SumberDanaInstitusi").val("");
        $("#MaksudTujuanInstitusi").val("");
        $("#AlamatPerusahaan").val("");
        $("#KodeKotaIns").val("");
        $("#KodeKotaInsDesc").val("");
        $("#KodePosIns").val("");
        //$("#RegistrationNPWP").val("");
        $("#ExpiredDateSKD").val("");
        $("#TanggalBerdiri").val("");
        $("#LokasiBerdiri").val("");
        $("#TeleponBisnis").val("");
        $("#NomorAnggaran").val("");
        $("#NomorSIUP").val("");
        $("#AssetFor1Year").val("");
        $("#AssetFor2Year").val("");
        $("#AssetFor3Year").val("");
        $("#OperatingProfitFor1Year").val("");
        $("#OperatingProfitFor2Year").val("");
        $("#OperatingProfitFor3Year").val("");
        $("#JumlahPejabat").val("");
        $("#NamaDepanIns1").val("");
        $("#NamaTengahIns1").val("");
        $("#NamaBelakangIns1").val("");
        $("#Jabatan1").val("");
        $("#JumlahIdentitasIns1").val("");
        $("#IdentitasIns11").val("");
        $("#NoIdentitasIns11").val("");
        $("#RegistrationDateIdentitasIns11").val("");
        $("#ExpiredDateIdentitasIns11").val("");
        $("#IdentitasIns12").val("");
        $("#NoIdentitasIns12").val("");
        $("#RegistrationDateIdentitasIns12").val("");
        $("#ExpiredDateIdentitasIns12").val("");
        $("#IdentitasIns13").val("");
        $("#NoIdentitasIns13").val("");
        $("#RegistrationDateIdentitasIns13").val("");
        $("#ExpiredDateIdentitasIns13").val("");
        $("#IdentitasIns14").val("");
        $("#NoIdentitasIns14").val("");
        $("#RegistrationDateIdentitasIns14").val("");
        $("#ExpiredDateIdentitasIns14").val("");
        $("#NamaDepanIns2").val("");
        $("#NamaTengahIns2").val("");
        $("#NamaBelakangIns2").val("");
        $("#Jabatan2").val("");
        $("#JumlahIdentitasIns2").val("");
        $("#IdentitasIns21").val("");
        $("#NoIdentitasIns21").val("");
        $("#RegistrationDateIdentitasIns21").val("");
        $("#ExpiredDateIdentitasIns21").val("");
        $("#IdentitasIns22").val("");
        $("#NoIdentitasIns22").val("");
        $("#RegistrationDateIdentitasIns22").val("");
        $("#ExpiredDateIdentitasIns22").val("");
        $("#IdentitasIns23").val("");
        $("#NoIdentitasIns23").val("");
        $("#RegistrationDateIdentitasIns23").val("");
        $("#ExpiredDateIdentitasIns23").val("");
        $("#IdentitasIns24").val("");
        $("#NoIdentitasIns24").val("");
        $("#RegistrationDateIdentitasIns24").val("");
        $("#ExpiredDateIdentitasIns24").val("");
        $("#NamaDepanIns3").val("");
        $("#NamaTengahIns3").val("");
        $("#NamaBelakangIns3").val("");
        $("#Jabatan3").val("");
        $("#JumlahIdentitasIns3").val("");
        $("#IdentitasIns31").val("");
        $("#NoIdentitasIns31").val("");
        $("#RegistrationDateIdentitasIns31").val("");
        $("#ExpiredDateIdentitasIns31").val("");
        $("#IdentitasIns32").val("");
        $("#NoIdentitasIns32").val("");
        $("#RegistrationDateIdentitasIns32").val("");
        $("#ExpiredDateIdentitasIns32").val("");
        $("#IdentitasIns33").val("");
        $("#NoIdentitasIns33").val("");
        $("#RegistrationDateIdentitasIns33").val("");
        $("#ExpiredDateIdentitasIns33").val("");
        $("#IdentitasIns34").val("");
        $("#NoIdentitasIns34").val("");
        $("#RegistrationDateIdentitasIns34").val("");
        $("#ExpiredDateIdentitasIns34").val("");
        $("#NamaDepanIns4").val("");
        $("#NamaTengahIns4").val("");
        $("#NamaBelakangIns4").val("");
        $("#Jabatan4").val("");
        $("#JumlahIdentitasIns4").val("");
        $("#IdentitasIns41").val("");
        $("#NoIdentitasIns41").val("");
        $("#RegistrationDateIdentitasIns41").val("");
        $("#ExpiredDateIdentitasIns41").val("");
        $("#IdentitasIns42").val("");
        $("#NoIdentitasIns42").val("");
        $("#RegistrationDateIdentitasIns42").val("");
        $("#ExpiredDateIdentitasIns42").val("");
        $("#IdentitasIns43").val("");
        $("#NoIdentitasIns43").val("");
        $("#RegistrationDateIdentitasIns43").val("");
        $("#ExpiredDateIdentitasIns43").val("");
        $("#IdentitasIns44").val("");
        $("#NoIdentitasIns44").val("");
        $("#RegistrationDateIdentitasIns44").val("");
        $("#ExpiredDateIdentitasIns44").val("");
        $("#SIUPExpirationDate").val("");
        $("#CountryofEstablishment").val("");
        $("#CountryofEstablishmentDesc").val("");
        $("#CompanyCityName").val("");
        $("#CompanyCityNameDesc").val("");
        $("#CountryofCompany").val("");
        $("#CountryofCompanyDesc").val("");
        $("#NPWPPerson1").val("");
        $("#NPWPPerson2").val("");
    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        $("#BtnCopyClient").show();
    }

    function onWinGenerateARIATextClose() {
        $("#ParamCategory").val("");
    }

    function onWinGenerateSInvestClose() {
        $("#ParamCategory").val("");
    }

    function onWinGenerateNKPDClose() {
        $("#ParamDateNKPD").data("kendoDatePicker").value("");
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
                             FundClientPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             OldID: { type: "string" },
                             Name: { type: "string" },
                             ClientCategory: { type: "string" },
                             ClientCategoryDesc: { type: "string" },
                             InvestorType: { type: "string" },
                             InvestorTypeDesc: { type: "string" },
                             InternalCategoryPK: { type: "number" },
                             InternalCategoryID: { type: "string" },
                             SellingAgentPK: { type: "number" },
                             SellingAgentID: { type: "string" },
                             SID: { type: "string" },
                             IFUACode: { type: "string" },
                             Child: { type: "boolean" },
                             CompanyTypeOJK: { type: "number" },
                             CompanyTypeOJKDesc: { type: "string" },
                             BusinessTypeOJK: { type: "number" },
                             BusinessTypeOJKDesc: { type: "string" },
                             ARIA: { type: "boolean" },
                             Registered: { type: "boolean" },
                             JumlahDanaAwal: { type: "number" },
                             JumlahDanaSaatIniCash: { type: "number" },
                             JumlahDanaSaatIni: { type: "number" },
                             Negara: { type: "string" },
                             NegaraDesc: { type: "string" },
                             Nationality: { type: "string" },
                             NPWP: { type: "string" },
                             SACode: { type: "string" },
                             Propinsi: { type: "number" },
                             TeleponSelular: { type: "string" },
                             Email: { type: "string" },
                             Fax: { type: "string" },
                             DormantDate: { type: "date" },
                             Description: { type: "string" },
                             JumlahBank: { type: "number" },
                             NamaBank1: { type: "number" },
                             NomorRekening1: { type: "string" },
                             BICCode1: { type: "string" },
                             NamaNasabah1: { type: "string" },
                             MataUang1: { type: "string" },
                             NamaBank2: { type: "number" },
                             NomorRekening2: { type: "string" },
                             BICCode2: { type: "string" },
                             NamaNasabah2: { type: "string" },
                             MataUang2: { type: "string" },
                             NamaBank3: { type: "number" },
                             NomorRekening3: { type: "string" },
                             BICCode3: { type: "string" },
                             NamaNasabah3: { type: "string" },
                             MataUang3: { type: "string" },
                             NamaDepanInd: { type: "string" },
                             NamaTengahInd: { type: "string" },
                             NamaBelakangInd: { type: "string" },
                             TempatLahir: { type: "string" },
                             TanggalLahir: { type: "date" },
                             JenisKelamin: { type: "number" },
                             StatusPerkawinan: { type: "number" },
                             Pekerjaan: { type: "number" },
                             Pendidikan: { type: "number" },
                             Agama: { type: "number" },
                             PenghasilanInd: { type: "number" },
                             SumberDanaInd: { type: "number" },
                             MaksudTujuanInd: { type: "number" },
                             AlamatInd1: { type: "string" },
                             KodeKotaInd1: { type: "string" },
                             KodeKotaInd1Desc: { type: "string" },
                             KodePosInd1: { type: "number" },
                             AlamatInd2: { type: "string" },
                             KodeKotaInd2: { type: "string" },
                             KodeKotaInd2Desc: { type: "string" },
                             KodePosInd2: { type: "number" },
                             NamaPerusahaan: { type: "string" },
                             Domisili: { type: "number" },
                             Tipe: { type: "number" },
                             Karakteristik: { type: "number" },
                             NoSKD: { type: "string" },
                             PenghasilanInstitusi: { type: "number" },
                             SumberDanaInstitusi: { type: "number" },
                             MaksudTujuanInstitusi: { type: "number" },
                             AlamatPerusahaan: { type: "string" },
                             KodeKotaIns: { type: "string" },
                             KodeKotaInsDesc: { type: "string" },
                             KodePosIns: { type: "number" },
                             SpouseName: { type: "string" },
                             MotherMaidenName: { type: "string" },
                             AhliWaris: { type: "string" },
                             HubunganAhliWaris: { type: "string" },
                             NatureOfBusiness: { type: "number" },
                             NatureOfBusinessDesc: { type: "string" },
                             Politis: { type: "number" },
                             PolitisLainnya: { type: "string" },
                             TeleponRumah: { type: "string" },
                             OtherAlamatInd1: { type: "string" },
                             OtherKodeKotaInd1: { type: "string" },
                             OtherKodeKotaInd1Desc: { type: "string" },
                             OtherKodePosInd1: { type: "number" },
                             OtherPropinsiInd1: { type: "number" },
                             CountryOfBirth: { type: "number" },
                             OtherNegaraInd1: { type: "number" },
                             OtherAlamatInd2: { type: "string" },
                             OtherKodeKotaInd2: { type: "string" },
                             OtherKodeKotaInd2Desc: { type: "string" },
                             OtherKodePosInd2: { type: "number" },
                             OtherPropinsiInd2: { type: "number" },
                             OtherNegaraInd2: { type: "number" },
                             OtherAlamatInd3: { type: "string" },
                             OtherKodeKotaInd3: { type: "string" },
                             OtherKodeKotaInd3Desc: { type: "string" },
                             OtherKodePosInd3: { type: "number" },
                             OtherPropinsiInd3: { type: "number" },
                             OtherNegaraInd3: { type: "number" },
                             OtherTeleponRumah: { type: "string" },
                             OtherTeleponSelular: { type: "string" },
                             OtherEmail: { type: "string" },
                             OtherFax: { type: "string" },
                             JumlahIdentitasInd: { type: "number" },
                             IdentitasInd1: { type: "number" },
                             NoIdentitasInd1: { type: "string" },
                             RegistrationDateIdentitasInd1: { type: "date" },
                             ExpiredDateIdentitasInd1: { type: "date" },
                             IdentitasInd2: { type: "number" },
                             NoIdentitasInd2: { type: "string" },
                             RegistrationDateIdentitasInd2: { type: "date" },
                             ExpiredDateIdentitasInd2: { type: "date" },
                             IdentitasInd3: { type: "number" },
                             NoIdentitasInd3: { type: "string" },
                             RegistrationDateIdentitasInd3: { type: "date" },
                             ExpiredDateIdentitasInd3: { type: "date" },
                             IdentitasInd4: { type: "number" },
                             NoIdentitasInd4: { type: "string" },
                             RegistrationDateIdentitasInd4: { type: "date" },
                             ExpiredDateIdentitasInd4: { type: "date" },
                             RegistrationNPWP: { type: "date" },
                             ExpiredDateSKD: { type: "date" },
                             SIUPExpirationDate: { type: "date" },
                             TanggalBerdiri: { type: "date" },
                             LokasiBerdiri: { type: "string" },
                             TeleponBisnis: { type: "string" },
                             NomorAnggaran: { type: "string" },
                             NomorSIUP: { type: "string" },
                             AssetFor1Year: { type: "string" },
                             AssetFor2Year: { type: "string" },
                             AssetFor3Year: { type: "string" },
                             OperatingProfitFor1Year: { type: "string" },
                             OperatingProfitFor2Year: { type: "string" },
                             OperatingProfitFor3Year: { type: "string" },
                             JumlahPejabat: { type: "number" },
                             NamaDepanIns1: { type: "string" },
                             NamaTengahIns1: { type: "string" },
                             NamaBelakangIns1: { type: "string" },
                             Jabatan1: { type: "string" },
                             JumlahIdentitasIns1: { type: "number" },
                             IdentitasIns11: { type: "string" },
                             NoIdentitasIns11: { type: "string" },
                             RegistrationDateIdentitasIns11: { type: "date" },
                             ExpiredDateIdentitasIns11: { type: "date" },
                             IdentitasIns12: { type: "string" },
                             NoIdentitasIns12: { type: "string" },
                             RegistrationDateIdentitasIns12: { type: "date" },
                             ExpiredDateIdentitasIns12: { type: "date" },
                             IdentitasIns13: { type: "string" },
                             NoIdentitasIns13: { type: "string" },
                             RegistrationDateIdentitasIns13: { type: "date" },
                             ExpiredDateIdentitasIns13: { type: "date" },
                             IdentitasIns14: { type: "string" },
                             NoIdentitasIns14: { type: "string" },
                             RegistrationDateIdentitasIns14: { type: "date" },
                             ExpiredDateIdentitasIns14: { type: "date" },
                             NamaDepanIns2: { type: "string" },
                             NamaTengahIns2: { type: "string" },
                             NamaBelakangIns2: { type: "string" },
                             Jabatan2: { type: "string" },
                             JumlahIdentitasIns2: { type: "number" },
                             IdentitasIns21: { type: "string" },
                             NoIdentitasIns21: { type: "string" },
                             RegistrationDateIdentitasIns21: { type: "date" },
                             ExpiredDateIdentitasIns21: { type: "date" },
                             IdentitasIns22: { type: "string" },
                             NoIdentitasIns22: { type: "string" },
                             RegistrationDateIdentitasIns22: { type: "date" },
                             ExpiredDateIdentitasIns22: { type: "date" },
                             IdentitasIns23: { type: "string" },
                             NoIdentitasIns23: { type: "string" },
                             RegistrationDateIdentitasIns23: { type: "date" },
                             ExpiredDateIdentitasIns23: { type: "date" },
                             IdentitasIns24: { type: "string" },
                             NoIdentitasIns24: { type: "string" },
                             RegistrationDateIdentitasIns24: { type: "date" },
                             ExpiredDateIdentitasIns24: { type: "date" },
                             NamaDepanIns3: { type: "string" },
                             NamaTengahIns3: { type: "string" },
                             NamaBelakangIns3: { type: "string" },
                             Jabatan3: { type: "string" },
                             JumlahIdentitasIns3: { type: "number" },
                             IdentitasIns31: { type: "string" },
                             NoIdentitasIns31: { type: "string" },
                             RegistrationDateIdentitasIns31: { type: "date" },
                             ExpiredDateIdentitasIns31: { type: "date" },
                             IdentitasIns32: { type: "string" },
                             NoIdentitasIns32: { type: "string" },
                             RegistrationDateIdentitasIns32: { type: "date" },
                             ExpiredDateIdentitasIns32: { type: "date" },
                             IdentitasIns33: { type: "string" },
                             NoIdentitasIns33: { type: "string" },
                             RegistrationDateIdentitasIns33: { type: "date" },
                             ExpiredDateIdentitasIns33: { type: "date" },
                             IdentitasIns34: { type: "string" },
                             NoIdentitasIns34: { type: "string" },
                             RegistrationDateIdentitasIns34: { type: "date" },
                             ExpiredDateIdentitasIns34: { type: "date" },
                             NamaDepanIns4: { type: "string" },
                             NamaTengahIns4: { type: "string" },
                             NamaBelakangIns4: { type: "string" },
                             Jabatan1: { type: "string" },
                             JumlahIdentitasIns4: { type: "number" },
                             IdentitasIns41: { type: "string" },
                             NoIdentitasIns41: { type: "string" },
                             RegistrationDateIdentitasIns41: { type: "date" },
                             ExpiredDateIdentitasIns41: { type: "date" },
                             IdentitasIns42: { type: "string" },
                             NoIdentitasIns42: { type: "string" },
                             RegistrationDateIdentitasIns42: { type: "date" },
                             ExpiredDateIdentitasIns42: { type: "date" },
                             IdentitasIns43: { type: "string" },
                             NoIdentitasIns43: { type: "string" },
                             RegistrationDateIdentitasIns43: { type: "date" },
                             ExpiredDateIdentitasIns43: { type: "date" },
                             IdentitasIns44: { type: "string" },
                             NoIdentitasIns44: { type: "string" },
                             RegistrationDateIdentitasIns44: { type: "date" },
                             ExpiredDateIdentitasIns44: { type: "date" },
                             BIMemberCode1: { type: "string" },
                             BIMemberCode2: { type: "string" },
                             BIMemberCode3: { type: "string" },
                             PhoneIns1: { type: "string" },
                             EmailIns1: { type: "string" },
                             PhoneIns2: { type: "string" },
                             EmailIns2: { type: "string" },
                             InvestorsRiskProfile: { type: "number" },
                             InvestorsRiskProfileDesc: { type: "string" },
                             AssetOwner: { type: "number" },
                             AssetOwnerDesc: { type: "string" },
                             StatementType: { type: "number" },
                             StatementTypeDesc: { type: "string" },
                             FATCA: { type: "number" },
                             FATCADesc: { type: "string" },
                             TIN: { type: "string" },
                             TINIssuanceCountry: { type: "number" },
                             TINIssuanceCountryDesc: { type: "string" },
                             GIIN: { type: "string" },
                             SubstantialOwnerName: { type: "string" },
                             SubstantialOwnerAddress: { type: "string" },
                             SubstantialOwnerTIN: { type: "string" },
                             BankCountry1: { type: "string" },
                             BankCountry1Desc: { type: "string" },
                             BankCountry2: { type: "string" },
                             BankCountry2Desc: { type: "string" },
                             BankCountry3: { type: "string" },
                             BankCountry3Desc: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" },
                             BitIsSuspend: { type: "boolean" },
                             SuspendBy: { type: "string" },
                             SuspendTime: { type: "date" },
                             UnSuspendBy: { type: "string" },
                             UnSuspendTime: { type: "date" },
                             SpouseBirthPlace: { type: "string" },
                             SpouseDateOfBirth: { type: "date" }, //yang ini
                             SpouseOccupation: { type: "number" },
                             SpouseNatureOfBusiness: { type: "number" },
                             SpouseNatureOfBusinessOther: { type: "string" },
                             SpouseIDNo: { type: "string" },
                             SpouseNationality: { type: "string" },
                             SpouseAnnualIncome: { type: "number" },
                             KYCRiskProfile: { type: "number" },
                             KYCRiskProfileDesc: { type: "string" },
                         }
                     }
                 }
             });
    }

    function getDataSourceSearch(_url) {
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
                             FundClientPK: { type: "number" },
                             StatusDesc: { type: "string" },
                             SellingAgentID: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Email: { type: "string" },
                             TeleponSelular: { type: "string" },
                             InvestorTypeDesc: { type: "string" },
                             NamaBank1: { type: "number" },
                             NomorRekening1: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             SuspendBy: { type: "string" },
                             SuspendTime: { type: "date" },
                             UnSuspendBy: { type: "string" },
                             UnSuspendTime: { type: "date" }

                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
        if (tabindex == 1) {
            RecalGridPending();
            //var gridPending = $("#gridSettlementInstructionPending").data("kendoGrid");
            //gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            //var gridHistory = $("#gridSettlementInstructionHistory").data("kendoGrid");
            //gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        //resetNotification();
        $("#gridFundClientApproved").empty();
        var _search = "";
        if ($("#search").val() == "") {
            _search = "all";
        }
        else {
            _search = $("#search").val();
        }
        var FundClientApprovedURL = window.location.origin + "/Radsoft/SuspendedAndInactiveClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + _search,
            dataSourceApproved = getDataSourceSearch(FundClientApprovedURL);


        var grid = $("#gridFundClientApproved").kendoGrid({
            dataSource: dataSourceApproved,
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
            dataBound: gridApprovedOnDataBound,
            toolbar: ["excel"],
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
               { field: "FundClientPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               // { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "ID", title: "ID", width: 200 },
               { field: "Name", title: "Name", width: 400 },
               { field: "ClientCategoryDesc", title: "Client Category", width: 300, hidden: true },
               { field: "InvestorTypeDesc", title: "Investor Type", width: 300 },
               { field: "TeleponSelular", title: "Telepon Selular", width: 200 },
               { field: "Email", title: "Email", width: 200 },

               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "UpdateID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
               //{ field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "VoidUsersID", title: "VoidID", width: 200 },
               //{ field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "SuspendBy", title: "Suspend By", width: 200 },
               { field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
               { field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
            ]
        }).data("kendoGrid");

        function gridApprovedOnDataBound() {
            var grid = $("#gridFundClientApproved").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {
                if (row.BitIsSuspend == true) {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
                }
            });
        }

        $("#SelectedAllApproved").change(function () {
            //resetNotification();
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.success(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            //resetNotification();

            var grid = $("#gridFundClientApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _fundClientPK = dataItemX.FundClientPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _fundClientPK);

        }

        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundClient/" + _a + "/" + _b,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var _msg;
                    if (_a) {
                        _msg = "Select Data ";
                    } else {
                        _msg = "DeSelect Data ";
                    }
                    alertify.success(_msg + _b);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        function SelectDeselectAllData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundClient/" + _a,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail" + _b).prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundClient").kendoTabStrip({
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
        $("#gridFundClientPending").empty();
        var _search = "";
        if ($("#search").val() == "") {
            _search = "all";
        }
        else {
            _search = $("#search").val();
        }
        var FundClientPendingURL = window.location.origin + "/Radsoft/SuspendedAndInactiveClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + _search,
            dataSourcePending = getDataSource(FundClientPendingURL);

        $("#gridFundClientPending").kendoGrid({
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
               { command: { text: "Show", click: showDetails }, title: " ", width: 85 },
               { field: "FundClientPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "ID", title: "ID", width: 200 },
               { field: "Name", title: "Name", width: 400 },
               { field: "ClientCategoryDesc", title: "Client Category", width: 300, hidden: true },
               { field: "InvestorTypeDesc", title: "Investor Type", width: 300 },
               { field: "InternalCategoryID", title: "Internal Category", width: 300 },
               //{ field: "InvestorTypeDesc", title: "Investor Type", width: 300 },
               //{ field: "SellingAgentID", title: "Selling Agent", width: 300 },
               //{ field: "SID", title: "Client Code", width: 200 },
               //{ field: "Child", title: "Child", width: 200, template: "#= Child ? 'Yes' : 'No' #" },
               //{ field: "ARIA", title: "ARIA", width: 200, template: "#= ARIA ? 'Yes' : 'No' #" },
               //{ field: "Registered", title: "Registered", width: 200, template: "#= Registered ? 'Yes' : 'No' #" },
               //{ field: "JumlahDanaAwal", title: "Jumlah Dana Awal", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "JumlahDanaSaatIniCash", title: "Jumlah Dana Saat Ini (Cash)", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "JumlahDanaSaatIni", title: "Jumlah Dana Saat Ini (Unit)", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "NegaraDesc", title: "Negara", width: 200 },
               //{ field: "NationalityDesc", title: "Nationality", width: 200 },
               //{ field: "NPWP", title: "NPWP", width: 200 },
               //{ field: "PropinsiDesc", title: "Propinsi", width: 200 },
               //{ field: "TeleponSelular", title: "Telepon Selular", width: 200 },
               //{ field: "Email", title: "Email", width: 200 },
               //{ field: "Fax", title: "Fax", width: 200 },
               //{ field: "Description", title: "Description", width: 200 },
               //{ field: "JumlahBank", title: "Jumlah Bank", hidden: true, width: 200 },
               //{ field: "NamaBank1", title: "Nama Bank 1", hidden: true, width: 200 },
               //{ field: "NomorRekening1", title: "Nomor Rekening 1", hidden: true, width: 200 },
               //{ field: "BICCode1", title: "BIC Code 1", hidden: true, width: 200 },
               //{ field: "NamaNasabah1", title: "Nama Nasabah 1", hidden: true, width: 200 },
               //{ field: "MataUang1", title: "Mata Uang 1", hidden: true, width: 200 },
               //{ field: "NamaBank2", title: "Nama Bank 2", hidden: true, width: 200 },
               //{ field: "NomorRekening2", title: "Nomor Rekening 2", hidden: true, width: 200 },
               //{ field: "BICCode2", title: "BIC Code 2", hidden: true, width: 200 },
               //{ field: "NamaNasabah2", title: "Nama Nasabah 2", hidden: true, width: 200 },
               //{ field: "MataUang2", title: "Mata Uang 2", hidden: true, width: 200 },
               //{ field: "NamaBank3", title: "Nama Bank 3", hidden: true, width: 200 },
               //{ field: "NomorRekening3", title: "Nomor Rekening 3", hidden: true, width: 200 },
               //{ field: "BICCode3", title: "BIC Code 3", hidden: true, width: 200 },
               //{ field: "NamaNasabah3", title: "Nama Nasabah 3", hidden: true, width: 200 },
               //{ field: "MataUang3", title: "Mata Uang 3", hidden: true, width: 200 },
               //{ field: "NamaDepanInd", title: "Nama Depan Ind", width: 200 },
               //{ field: "NamaTengahInd", title: "Nama Tengah Ind", width: 200 },
               //{ field: "NamaBelakangInd", title: "Nama Belakang Ind", width: 200 },
               //{ field: "TempatLahir", title: "Tempat Lahir", width: 200 },
               //{ field: "TanggalLahir", title: "Tanggal Lahir", width: 200, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MM/yyyy')#" },
               //{ field: "JenisKelamin", title: "Jenis Kelamin", hidden: true, width: 200 },
               //{ field: "JenisKelaminDesc", title: "Jenis Kelamin", width: 200 },
               //{ field: "StatusPerkawinan", title: "Status Perkawinan", hidden: true, width: 200 },
               //{ field: "StatusPerkawinanDesc", title: "Status Perkawinan", width: 200 },
               //{ field: "Pekerjaan", title: "Pekerjaan", hidden: true, width: 200 },
               //{ field: "PekerjaanDesc", title: "Pekerjaan", width: 200 },
               //{ field: "Pendidikan", title: "Pendidikan", hidden: true, width: 200 },
               //{ field: "PendidikanDesc", title: "Pendidikan", width: 200 },
               //{ field: "Agama", title: "Agama", hidden: true, width: 200 },
               //{ field: "AgamaDesc", title: "Agama", width: 200 },
               //{ field: "PenghasilanInd", title: "Penghasilan Ind", hidden: true, width: 200 },
               //{ field: "PenghasilanIndDesc", title: "Penghasilan Ind", width: 200 },
               //{ field: "SumberDanaInd", title: "Sumber Dana Ind", hidden: true, width: 200 },
               //{ field: "SumberDanaIndDesc", title: "Sumber Dana Ind", width: 200 },
               //{ field: "MaksudTujuanInd", title: "Maksud Tujuan Ind", hidden: true, width: 200 },
               //{ field: "MaksudTujuanIndDesc", title: "Maksud Tujuan Ind", width: 200 },
               //{ field: "AlamatInd1", title: "Alamat Ind 1", width: 200 },
               //{ field: "KodeKotaInd1", title: "Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "KodeKotaInd1Desc", title: "Kode Kota Ind 1", width: 200 },
               //{ field: "KodePosInd1", title: "Kode Pos Ind 1", width: 200 },
               //{ field: "AlamatInd2", title: "Alamat Ind 2", hidden: true, width: 200 },
               //{ field: "KodeKotaInd2", title: "Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "KodeKotaInd2Desc", title: "Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "KodePosInd2", title: "Kode Pos Ind 2", hidden: true, width: 200 },
               //{ field: "NamaPerusahaan", title: "Nama Perusahaan", width: 200 },
               //{ field: "Domisili", title: "Domisili", hidden: true, width: 200 },
               //{ field: "DomisiliDesc", title: "Domisili", width: 200 },
               //{ field: "Tipe", title: "Tipe", hidden: true, width: 200 },
               //{ field: "TipeDesc", title: "Tipe", width: 200 },
               //{ field: "Karakteristik", title: "Karakteristik", hidden: true, width: 200 },
               //{ field: "KarakteristikDesc", title: "Karakteristik", width: 200 },
               //{ field: "NoSKD", title: "No SKD", width: 200 },
               //{ field: "PenghasilanInstitusi", title: "Penghasilan Institusi", hidden: true, width: 200 },
               //{ field: "PenghasilanInstitusiDesc", title: "Penghasilan Institusi", width: 300 },
               //{ field: "SumberDanaInstitusi", title: "Sumber Dana Institusi", hidden: true, width: 200 },
               //{ field: "SumberDanaInstitusiDesc", title: "Sumber Dana Institusi", width: 300 },
               //{ field: "MaksudTujuanInstitusi", title: "Maksud Tujuan Institusi", hidden: true, width: 200 },
               //{ field: "MaksudTujuanInstitusiDesc", title: "Maksud Tujuan Institusi", width: 300 },
               //{ field: "AlamatPerusahaan", title: "Alamat Perusahaan", width: 200 },
               //{ field: "KodeKotaIns", title: "Kode Kota Ins", hidden: true, width: 200 },
               //{ field: "KodeKotaInsDesc", title: "Kode Kota Ins", width: 200 },
               //{ field: "KodePosIns", title: "Kode Pos Ins", width: 200 },
               //{ field: "SpouseName", title: "Spouse Name", hidden: true, width: 200 },
               //{ field: "AhliWaris", title: "Ahli Waris", hidden: true, width: 200 },
               //{ field: "HubunganAhliWaris", title: "Hubungan Ahli Waris", hidden: true, width: 200 },
               //{ field: "NatureOfBusiness", title: "Nature Of Business", hidden: true, width: 200 },
               //{ field: "NatureOfBusinessLainnya", title: "Nature Of Business Lainnya", hidden: true, width: 200 },
               //{ field: "Politis", title: "Politis", hidden: true, width: 200 },
               //{ field: "PolitisLainnya", title: "Politis Lainnya", hidden: true, width: 200 },
               //{ field: "TeleponRumah", title: "Telepon Rumah", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd1", title: "Other Alamat Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd1", title: "Other Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd1Desc", title: "Other Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd1", title: "Other Kode Pos Ind 1", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd1", title: "Other Propinsi Ind 1", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd1", title: "Other Negara Ind 1", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd2", title: "Other Alamat Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd2", title: "Other Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd2Desc", title: "Other Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd2", title: "Other Kode Pos Ind 2", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd2", title: "Other Propinsi Ind 2", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd2", title: "Other Negara Ind 2", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd3", title: "Other Alamat Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd3", title: "Other Kode Kota Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd3Desc", title: "Other Kode Kota Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd3", title: "Other Kode Pos Ind 3", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd3", title: "Other Propinsi Ind 3", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd3", title: "Other Negara Ind3", hidden: true, width: 200 },
               //{ field: "OtherTeleponRumah", title: "Other Telepon Rumah", hidden: true, width: 200 },
               //{ field: "OtherTeleponSelular", title: "Other Telepon Selular", hidden: true, width: 200 },
               //{ field: "OtherEmail", title: "Other Email", hidden: true, width: 200 },
               //{ field: "OtherFax", title: "Other Fax", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasInd", title: "Jumlah Identitas Ind", hidden: true, width: 200 },
               //{ field: "IdentitasInd1", title: "Identitas Ind 1", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd1", title: "No Identitas Ind 1", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd1", title: "Registration Date Identitas Ind 1", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd1), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd1", title: "Expired Date Identitas Ind 1", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd1), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd2", title: "Identitas Ind 2", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd2", title: "No Identitas Ind 2", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd2", title: "Registration Date Identitas Ind2", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd2), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd2", title: "Expired Date Identitas Ind2", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd2), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd3", title: "Identitas Ind3", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd3", title: "No Identitas Ind3", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd3", title: "Registration Date Identitas Ind 3", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd3), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd3", title: "Expired Date Identitas Ind 3", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd3), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd4", title: "Identitas Ind 4", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd4", title: "No Identitas Ind 4", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd4", title: "Registration Date Identitas Ind 4", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd4), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd4", title: "Expired Date Identitas Ind 4", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd4), 'dd/MM/yyyy')#" },
               //{ field: "RegistrationNPWP", title: "Registration NPWP", hidden: true, width: 200 },
               //{ field: "ExpiredDateSKD", title: "Expired Date SKD", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateSKD), 'dd/MM/yyyy')#" },
               //{ field: "TanggalBerdiri", title: "Tanggal Berdiri", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(TanggalBerdiri), 'dd/MM/yyyy')#" },
               //{ field: "LokasiBerdiri", title: "Lokasi Berdiri", hidden: true, width: 200 },
               //{ field: "TeleponBisnis", title: "Telepon Bisnis", hidden: true, width: 200 },
               //{ field: "NomorAnggaran", title: "Nomor Anggaran", hidden: true, width: 200 },
               //{ field: "NomorSIUP", title: "Nomor SIUP", hidden: true, width: 200 },
               //{ field: "AssetFor1Year", title: "Asset For 1 Year", hidden: true, width: 200 },
               //{ field: "AssetFor2Year", title: "Asset For 2 Year", hidden: true, width: 200 },
               //{ field: "AssetFor3Year", title: "Asset For 3 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor1Year", title: "Operating Profit For 1 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor2Year", title: "Operating Profit For 2 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor3Year", title: "Operating Profit For 3 Year", hidden: true, width: 200 },
               //{ field: "JumlahPejabat", title: "Jumlah Pejabat", hidden: true, width: 200 },
               //{ field: "NamaDepanIns1", title: "Nama Depan Ins 1", hidden: true, width: 200 },
               //{ field: "NamaTengahIns1", title: "Nama Tengah Ins 1", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns1", title: "Nama Belakang Ins 1", hidden: true, width: 200 },
               //{ field: "Jabatan1", title: "Jabatan 1", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns1", title: "Jumlah Identitas Ins 1", hidden: true, width: 200 },
               //{ field: "IdentitasIns11", title: "Identitas Ins 11", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns11", title: "No Identitas Ins 11", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns11", title: "Registration Date Identitas Ins 11", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns11), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns11", title: "Expired Date Identitas Ins 11", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns11), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns12", title: "Identitas Ins 12", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns12", title: "No Identitas Ins 12", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns12", title: "Registration Date Identitas Ins 12", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns12), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns12", title: "Expired Date Identitas Ins 12", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns12), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns13", title: "Identitas Ins 13", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns13", title: "No Identitas Ins 13", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns13", title: "Registration Date Identitas Ins 13", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns13), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns13", title: "Expired Date Identitas Ins 13", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns13), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns14", title: "Identitas Ins 14", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns14", title: "NoIdentitasIns14", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns14", title: "RegistrationDateIdentitasIns14", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns14), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns14", title: "ExpiredDateIdentitasIns14", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns14), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns2", title: "NamaDepanIns2", hidden: true, width: 200 },
               //{ field: "NamaTengahIns2", title: "NamaTengahIns2", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns2", title: "NamaBelakangIns2", hidden: true, width: 200 },
               //{ field: "Jabatan2", title: "Jabatan2", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns2", title: "JumlahIdentitasIns2", hidden: true, width: 200 },
               //{ field: "IdentitasIns21", title: "IdentitasIns21", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns21", title: "NoIdentitasIns21", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns21", title: "RegistrationDateIdentitasIns21", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns21), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns21", title: "ExpiredDateIdentitasIns21", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns21), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns22", title: "IdentitasIns22", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns22", title: "NoIdentitasIns22", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns22", title: "RegistrationDateIdentitasIns22", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns22), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns22", title: "ExpiredDateIdentitasIns22", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns22), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns23", title: "IdentitasIns23", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns23", title: "NoIdentitasIns23", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns23", title: "RegistrationDateIdentitasIns23", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns23), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns23", title: "ExpiredDateIdentitasIns23", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns23), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns24", title: "IdentitasIns24", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns24", title: "NoIdentitasIns24", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns24", title: "RegistrationDateIdentitasIns24", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns24), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns24", title: "ExpiredDateIdentitasIns24", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns24), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns3", title: "NamaDepanIns3", hidden: true, width: 200 },
               //{ field: "NamaTengahIns3", title: "NamaTengahIns3", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns3", title: "NamaBelakangIns3", hidden: true, width: 200 },
               //{ field: "Jabatan3", title: "Jabatan3", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns3", title: "JumlahIdentitasIns3", hidden: true, width: 200 },
               //{ field: "IdentitasIns31", title: "IdentitasIns31", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns31", title: "NoIdentitasIns31", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns31", title: "RegistrationDateIdentitasIns31", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns31), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns31", title: "ExpiredDateIdentitasIns31", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns31), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns32", title: "IdentitasIns32", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns32", title: "NoIdentitasIns32", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns32", title: "RegistrationDateIdentitasIns32", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns32), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns32", title: "ExpiredDateIdentitasIns32", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns32), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns33", title: "IdentitasIns33", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns33", title: "NoIdentitasIns33", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns33", title: "RegistrationDateIdentitasIns33", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns33), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns33", title: "ExpiredDateIdentitasIns33", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns33), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns34", title: "IdentitasIns34", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns34", title: "NoIdentitasIns34", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns34", title: "RegistrationDateIdentitasIns34", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns34), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns34", title: "ExpiredDateIdentitasIns34", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns34), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns4", title: "NamaDepanIns4", hidden: true, width: 200 },
               //{ field: "NamaTengahIns4", title: "NamaTengahIns4", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns4", title: "NamaBelakangIns4", hidden: true, width: 200 },
               //{ field: "Jabatan4", title: "Jabatan4", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns4", title: "JumlahIdentitasIns4", hidden: true, width: 200 },
               //{ field: "IdentitasIns41", title: "IdentitasIns41", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns41", title: "NoIdentitasIns41", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns41", title: "RegistrationDateIdentitasIns41", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns41), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns41", title: "ExpiredDateIdentitasIns41", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns41), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns42", title: "IdentitasIns42", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns42", title: "NoIdentitasIns42", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns42", title: "RegistrationDateIdentitasIns42", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns42), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns42", title: "ExpiredDateIdentitasIns42", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns42), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns43", title: "IdentitasIns43", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns43", title: "NoIdentitasIns43", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns43", title: "RegistrationDateIdentitasIns43", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns43), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns43", title: "ExpiredDateIdentitasIns43", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns43), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns44", title: "IdentitasIns44", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns44", title: "NoIdentitasIns44", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns44", title: "RegistrationDateIdentitasIns44", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns44), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns44", title: "ExpiredDateIdentitasIns44", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns44), 'dd/MM/yyyy')#" },
               //{ field: "BIMemberCode1", title: "BIMemberCode1", hidden: true, width: 200 },
               //{ field: "BIMemberCode2", title: "BIMemberCode2", hidden: true, width: 200 },
               //{ field: "BIMemberCode3", title: "BIMemberCode3", hidden: true, width: 200 },
               //{ field: "PhoneIns1", title: "PhoneIns1", hidden: true, width: 200 },
               //{ field: "EmailIns1", title: "EmailIns1", hidden: true, width: 200 },
               //{ field: "PhoneIns2", title: "PhoneIns2", hidden: true, width: 200 },
               //{ field: "EmailIns2", title: "EmailIns2", hidden: true, width: 200 },
               //{ field: "InvestorsRiskProfile", title: "InvestorsRiskProfile", hidden: true, width: 200 },
               //{ field: "InvestorsRiskProfileDesc", title: "InvestorsRiskProfileDesc", hidden: true, width: 200 },
               //{ field: "AssetOwner", title: "AssetOwner", hidden: true, width: 200 },
               //{ field: "AssetOwnerDesc", title: "AssetOwnerDesc", hidden: true, width: 200 },
               //{ field: "StatementType", title: "StatementType", hidden: true, width: 200 },
               //{ field: "StatementTypeDesc", title: "StatementTypeDesc", hidden: true, width: 200 },
               //{ field: "FATCA", title: "FATCA", hidden: true, width: 200 },
               //{ field: "FATCADesc", title: "FATCADesc", hidden: true, width: 200 },
               //{ field: "TIN", title: "TIN", hidden: true, width: 200 },
               //{ field: "TINIssuanceCountry", title: "TINIssuanceCountry", hidden: true, width: 200 },
               //{ field: "TINIssuanceCountryDesc", title: "TINIssuanceCountryDesc", hidden: true, width: 200 },
               //{ field: "GIIN", title: "GIIN", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerName", title: "SubstantialOwnerName", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerAddress", title: "SubstantialOwnerAddress", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerTIN", title: "SubstantialOwnerTIN", hidden: true, width: 200 },
               //{ field: "BankBranchName1", title: "BankBranchName1", hidden: true, width: 200 },
               //{ field: "BankBranchName2", title: "BankBranchName2", hidden: true, width: 200 },
               //{ field: "BankBranchName3", title: "BankBranchName3", hidden: true, width: 200 },
               //{ field: "BankCountry1", title: "BankCountry1", hidden: true, width: 200 },
               //{ field: "BankCountry1Desc", title: "BankCountry1Desc", hidden: true, width: 200 },
               //{ field: "BankCountry2", title: "BankCountry2", hidden: true, width: 200 },
               //{ field: "BankCountry2Desc", title: "BankCountry2Desc", hidden: true, width: 200 },
               //{ field: "BankCountry3", title: "BankCountry3", hidden: true, width: 200 },
               //{ field: "BankCountry3Desc", title: "BankCountry3Desc", hidden: true, width: 200 },
               //{ field: "EntryUsersID", title: "Entry ID", width: 200 },
               //{ field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "UpdateUsersID", title: "UpdateID", width: 200 },
               //{ field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
               //{ field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "VoidUsersID", title: "VoidID", width: 200 },
               //{ field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "BitIsSuspend", title: "BitIsSuspend", width: 200, template: "#= BitIsSuspend ? 'Active' : 'Suspend' #" }
            ]

        });

    }

    function RecalGridHistory() {
        $("#gridFundClientHistory").empty();
        var _search = "";
        if ($("#search").val() == "") {
            _search = "all";
        }
        else {
            _search = $("#search").val();
        }
        var FundClientHistoryURL = window.location.origin + "/Radsoft/SuspendedAndInactiveClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + _search,
            dataSourceHistory = getDataSourceSearch(FundClientHistoryURL);

        $("#gridFundClientHistory").kendoGrid({
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
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "FundClientPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: false, width: 120 },
               { field: "ID", title: "ID", width: 200 },
               { field: "Name", title: "Name", width: 400 },
               { field: "ClientCategoryDesc", title: "Client Category", width: 300, hidden: true },
               { field: "InvestorTypeDesc", title: "Investor Type", width: 300 },
               { field: "InternalCategoryID", title: "Internal Category", width: 300 },
               //{ field: "InvestorTypeDesc", title: "Investor Type", width: 300 },
               //{ field: "SellingAgentID", title: "Selling Agent", width: 300 },
               //{ field: "SID", title: "Client Code", width: 200 },
               //{ field: "Child", title: "Child", width: 200, template: "#= Child ? 'Yes' : 'No' #" },
               //{ field: "ARIA", title: "ARIA", width: 200, template: "#= ARIA ? 'Yes' : 'No' #" },
               //{ field: "Registered", title: "Registered", width: 200, template: "#= Registered ? 'Yes' : 'No' #" },
               //{ field: "JumlahDanaAwal", title: "Jumlah Dana Awal", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "JumlahDanaSaatIniCash", title: "Jumlah Dana Saat Ini (Cash)", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "JumlahDanaSaatIni", title: "Jumlah Dana Saat Ini (Unit)", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               //{ field: "NegaraDesc", title: "Negara", width: 200 },
               //{ field: "NationalityDesc", title: "Nationality", width: 200 },
               //{ field: "NPWP", title: "NPWP", width: 200 },
               //{ field: "PropinsiDesc", title: "Propinsi", width: 200 },
               //{ field: "TeleponSelular", title: "Telepon Selular", width: 200 },
               //{ field: "Email", title: "Email", width: 200 },
               //{ field: "Fax", title: "Fax", width: 200 },
               //{ field: "Description", title: "Description", width: 200 },
               //{ field: "JumlahBank", title: "Jumlah Bank", hidden: true, width: 200 },
               //{ field: "NamaBank1", title: "Nama Bank 1", hidden: true, width: 200 },
               //{ field: "NomorRekening1", title: "Nomor Rekening 1", hidden: true, width: 200 },
               //{ field: "BICCode1", title: "BIC Code 1", hidden: true, width: 200 },
               //{ field: "NamaNasabah1", title: "Nama Nasabah 1", hidden: true, width: 200 },
               //{ field: "MataUang1", title: "Mata Uang 1", hidden: true, width: 200 },
               //{ field: "NamaBank2", title: "Nama Bank 2", hidden: true, width: 200 },
               //{ field: "NomorRekening2", title: "Nomor Rekening 2", hidden: true, width: 200 },
               //{ field: "BICCode2", title: "BIC Code 2", hidden: true, width: 200 },
               //{ field: "NamaNasabah2", title: "Nama Nasabah 2", hidden: true, width: 200 },
               //{ field: "MataUang2", title: "Mata Uang 2", hidden: true, width: 200 },
               //{ field: "NamaBank3", title: "Nama Bank 3", hidden: true, width: 200 },
               //{ field: "NomorRekening3", title: "Nomor Rekening 3", hidden: true, width: 200 },
               //{ field: "BICCode3", title: "BIC Code 3", hidden: true, width: 200 },
               //{ field: "NamaNasabah3", title: "Nama Nasabah 3", hidden: true, width: 200 },
               //{ field: "MataUang3", title: "Mata Uang 3", hidden: true, width: 200 },
               //{ field: "NamaDepanInd", title: "Nama Depan Ind", width: 200 },
               //{ field: "NamaTengahInd", title: "Nama Tengah Ind", width: 200 },
               //{ field: "NamaBelakangInd", title: "Nama Belakang Ind", width: 200 },
               //{ field: "TempatLahir", title: "Tempat Lahir", width: 200 },
               //{ field: "TanggalLahir", title: "Tanggal Lahir", width: 200, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MM/yyyy')#" },
               //{ field: "JenisKelamin", title: "Jenis Kelamin", hidden: true, width: 200 },
               //{ field: "JenisKelaminDesc", title: "Jenis Kelamin", width: 200 },
               //{ field: "StatusPerkawinan", title: "Status Perkawinan", hidden: true, width: 200 },
               //{ field: "StatusPerkawinanDesc", title: "Status Perkawinan", width: 200 },
               //{ field: "Pekerjaan", title: "Pekerjaan", hidden: true, width: 200 },
               //{ field: "PekerjaanDesc", title: "Pekerjaan", width: 200 },
               //{ field: "Pendidikan", title: "Pendidikan", hidden: true, width: 200 },
               //{ field: "PendidikanDesc", title: "Pendidikan", width: 200 },
               //{ field: "Agama", title: "Agama", hidden: true, width: 200 },
               //{ field: "AgamaDesc", title: "Agama", width: 200 },
               //{ field: "PenghasilanInd", title: "Penghasilan Ind", hidden: true, width: 200 },
               //{ field: "PenghasilanIndDesc", title: "Penghasilan Ind", width: 200 },
               //{ field: "SumberDanaInd", title: "Sumber Dana Ind", hidden: true, width: 200 },
               //{ field: "SumberDanaIndDesc", title: "Sumber Dana Ind", width: 200 },
               //{ field: "MaksudTujuanInd", title: "Maksud Tujuan Ind", hidden: true, width: 200 },
               //{ field: "MaksudTujuanIndDesc", title: "Maksud Tujuan Ind", width: 200 },
               //{ field: "AlamatInd1", title: "Alamat Ind 1", width: 200 },
               //{ field: "KodeKotaInd1", title: "Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "KodeKotaInd1Desc", title: "Kode Kota Ind 1", width: 200 },
               //{ field: "KodePosInd1", title: "Kode Pos Ind 1", width: 200 },
               //{ field: "AlamatInd2", title: "Alamat Ind 2", hidden: true, width: 200 },
               //{ field: "KodeKotaInd2", title: "Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "KodeKotaInd2Desc", title: "Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "KodePosInd2", title: "Kode Pos Ind 2", hidden: true, width: 200 },
               //{ field: "NamaPerusahaan", title: "Nama Perusahaan", width: 200 },
               //{ field: "Domisili", title: "Domisili", hidden: true, width: 200 },
               //{ field: "DomisiliDesc", title: "Domisili", width: 200 },
               //{ field: "Tipe", title: "Tipe", hidden: true, width: 200 },
               //{ field: "TipeDesc", title: "Tipe", width: 200 },
               //{ field: "Karakteristik", title: "Karakteristik", hidden: true, width: 200 },
               //{ field: "KarakteristikDesc", title: "Karakteristik", width: 200 },
               //{ field: "NoSKD", title: "No SKD", width: 200 },
               //{ field: "PenghasilanInstitusi", title: "Penghasilan Institusi", hidden: true, width: 200 },
               //{ field: "PenghasilanInstitusiDesc", title: "Penghasilan Institusi", width: 300 },
               //{ field: "SumberDanaInstitusi", title: "Sumber Dana Institusi", hidden: true, width: 200 },
               //{ field: "SumberDanaInstitusiDesc", title: "Sumber Dana Institusi", width: 300 },
               //{ field: "MaksudTujuanInstitusi", title: "Maksud Tujuan Institusi", hidden: true, width: 200 },
               //{ field: "MaksudTujuanInstitusiDesc", title: "Maksud Tujuan Institusi", width: 300 },
               //{ field: "AlamatPerusahaan", title: "Alamat Perusahaan", width: 200 },
               //{ field: "KodeKotaIns", title: "Kode Kota Ins", hidden: true, width: 200 },
               //{ field: "KodeKotaInsDesc", title: "Kode Kota Ins", width: 200 },
               //{ field: "KodePosIns", title: "Kode Pos Ins", width: 200 },
               //{ field: "SpouseName", title: "Spouse Name", hidden: true, width: 200 },
               //{ field: "AhliWaris", title: "Ahli Waris", hidden: true, width: 200 },
               //{ field: "HubunganAhliWaris", title: "Hubungan Ahli Waris", hidden: true, width: 200 },
               //{ field: "NatureOfBusiness", title: "Nature Of Business", hidden: true, width: 200 },
               //{ field: "NatureOfBusinessLainnya", title: "Nature Of Business Lainnya", hidden: true, width: 200 },
               //{ field: "Politis", title: "Politis", hidden: true, width: 200 },
               //{ field: "PolitisLainnya", title: "Politis Lainnya", hidden: true, width: 200 },
               //{ field: "TeleponRumah", title: "Telepon Rumah", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd1", title: "Other Alamat Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd1", title: "Other Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd1Desc", title: "Other Kode Kota Ind 1", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd1", title: "Other Kode Pos Ind 1", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd1", title: "Other Propinsi Ind 1", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd1", title: "Other Negara Ind 1", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd2", title: "Other Alamat Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd2", title: "Other Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd2Desc", title: "Other Kode Kota Ind 2", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd2", title: "Other Kode Pos Ind 2", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd2", title: "Other Propinsi Ind 2", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd2", title: "Other Negara Ind 2", hidden: true, width: 200 },
               //{ field: "OtherAlamatInd3", title: "Other Alamat Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd3", title: "Other Kode Kota Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodeKotaInd3Desc", title: "Other Kode Kota Ind 3", hidden: true, width: 200 },
               //{ field: "OtherKodePosInd3", title: "Other Kode Pos Ind 3", hidden: true, width: 200 },
               //{ field: "OtherPropinsiInd3", title: "Other Propinsi Ind 3", hidden: true, width: 200 },
               //{ field: "OtherNegaraInd3", title: "Other Negara Ind3", hidden: true, width: 200 },
               //{ field: "OtherTeleponRumah", title: "Other Telepon Rumah", hidden: true, width: 200 },
               //{ field: "OtherTeleponSelular", title: "Other Telepon Selular", hidden: true, width: 200 },
               //{ field: "OtherEmail", title: "Other Email", hidden: true, width: 200 },
               //{ field: "OtherFax", title: "Other Fax", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasInd", title: "Jumlah Identitas Ind", hidden: true, width: 200 },
               //{ field: "IdentitasInd1", title: "Identitas Ind 1", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd1", title: "No Identitas Ind 1", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd1", title: "Registration Date Identitas Ind 1", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd1), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd1", title: "Expired Date Identitas Ind 1", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd1), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd2", title: "Identitas Ind 2", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd2", title: "No Identitas Ind 2", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd2", title: "Registration Date Identitas Ind2", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd2), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd2", title: "Expired Date Identitas Ind2", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd2), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd3", title: "Identitas Ind3", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd3", title: "No Identitas Ind3", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd3", title: "Registration Date Identitas Ind 3", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd3), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd3", title: "Expired Date Identitas Ind 3", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd3), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasInd4", title: "Identitas Ind 4", hidden: true, width: 200 },
               //{ field: "NoIdentitasInd4", title: "No Identitas Ind 4", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasInd4", title: "Registration Date Identitas Ind 4", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd4), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasInd4", title: "Expired Date Identitas Ind 4", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd4), 'dd/MM/yyyy')#" },
               //{ field: "RegistrationNPWP", title: "Registration NPWP", hidden: true, width: 200 },
               //{ field: "ExpiredDateSKD", title: "Expired Date SKD", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateSKD), 'dd/MM/yyyy')#" },
               //{ field: "TanggalBerdiri", title: "Tanggal Berdiri", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(TanggalBerdiri), 'dd/MM/yyyy')#" },
               //{ field: "LokasiBerdiri", title: "Lokasi Berdiri", hidden: true, width: 200 },
               //{ field: "TeleponBisnis", title: "Telepon Bisnis", hidden: true, width: 200 },
               //{ field: "NomorAnggaran", title: "Nomor Anggaran", hidden: true, width: 200 },
               //{ field: "NomorSIUP", title: "Nomor SIUP", hidden: true, width: 200 },
               //{ field: "AssetFor1Year", title: "Asset For 1 Year", hidden: true, width: 200 },
               //{ field: "AssetFor2Year", title: "Asset For 2 Year", hidden: true, width: 200 },
               //{ field: "AssetFor3Year", title: "Asset For 3 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor1Year", title: "Operating Profit For 1 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor2Year", title: "Operating Profit For 2 Year", hidden: true, width: 200 },
               //{ field: "OperatingProfitFor3Year", title: "Operating Profit For 3 Year", hidden: true, width: 200 },
               //{ field: "JumlahPejabat", title: "Jumlah Pejabat", hidden: true, width: 200 },
               //{ field: "NamaDepanIns1", title: "Nama Depan Ins 1", hidden: true, width: 200 },
               //{ field: "NamaTengahIns1", title: "Nama Tengah Ins 1", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns1", title: "Nama Belakang Ins 1", hidden: true, width: 200 },
               //{ field: "Jabatan1", title: "Jabatan 1", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns1", title: "Jumlah Identitas Ins 1", hidden: true, width: 200 },
               //{ field: "IdentitasIns11", title: "Identitas Ins 11", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns11", title: "No Identitas Ins 11", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns11", title: "Registration Date Identitas Ins 11", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns11), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns11", title: "Expired Date Identitas Ins 11", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns11), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns12", title: "Identitas Ins 12", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns12", title: "No Identitas Ins 12", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns12", title: "Registration Date Identitas Ins 12", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns12), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns12", title: "Expired Date Identitas Ins 12", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns12), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns13", title: "Identitas Ins 13", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns13", title: "No Identitas Ins 13", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns13", title: "Registration Date Identitas Ins 13", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns13), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns13", title: "Expired Date Identitas Ins 13", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns13), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns14", title: "Identitas Ins 14", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns14", title: "NoIdentitasIns14", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns14", title: "RegistrationDateIdentitasIns14", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns14), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns14", title: "ExpiredDateIdentitasIns14", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns14), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns2", title: "NamaDepanIns2", hidden: true, width: 200 },
               //{ field: "NamaTengahIns2", title: "NamaTengahIns2", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns2", title: "NamaBelakangIns2", hidden: true, width: 200 },
               //{ field: "Jabatan2", title: "Jabatan2", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns2", title: "JumlahIdentitasIns2", hidden: true, width: 200 },
               //{ field: "IdentitasIns21", title: "IdentitasIns21", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns21", title: "NoIdentitasIns21", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns21", title: "RegistrationDateIdentitasIns21", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns21), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns21", title: "ExpiredDateIdentitasIns21", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns21), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns22", title: "IdentitasIns22", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns22", title: "NoIdentitasIns22", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns22", title: "RegistrationDateIdentitasIns22", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns22), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns22", title: "ExpiredDateIdentitasIns22", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns22), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns23", title: "IdentitasIns23", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns23", title: "NoIdentitasIns23", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns23", title: "RegistrationDateIdentitasIns23", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns23), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns23", title: "ExpiredDateIdentitasIns23", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns23), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns24", title: "IdentitasIns24", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns24", title: "NoIdentitasIns24", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns24", title: "RegistrationDateIdentitasIns24", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns24), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns24", title: "ExpiredDateIdentitasIns24", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns24), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns3", title: "NamaDepanIns3", hidden: true, width: 200 },
               //{ field: "NamaTengahIns3", title: "NamaTengahIns3", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns3", title: "NamaBelakangIns3", hidden: true, width: 200 },
               //{ field: "Jabatan3", title: "Jabatan3", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns3", title: "JumlahIdentitasIns3", hidden: true, width: 200 },
               //{ field: "IdentitasIns31", title: "IdentitasIns31", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns31", title: "NoIdentitasIns31", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns31", title: "RegistrationDateIdentitasIns31", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns31), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns31", title: "ExpiredDateIdentitasIns31", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns31), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns32", title: "IdentitasIns32", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns32", title: "NoIdentitasIns32", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns32", title: "RegistrationDateIdentitasIns32", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns32), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns32", title: "ExpiredDateIdentitasIns32", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns32), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns33", title: "IdentitasIns33", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns33", title: "NoIdentitasIns33", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns33", title: "RegistrationDateIdentitasIns33", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns33), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns33", title: "ExpiredDateIdentitasIns33", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns33), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns34", title: "IdentitasIns34", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns34", title: "NoIdentitasIns34", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns34", title: "RegistrationDateIdentitasIns34", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns34), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns34", title: "ExpiredDateIdentitasIns34", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns34), 'dd/MM/yyyy')#" },
               //{ field: "NamaDepanIns4", title: "NamaDepanIns4", hidden: true, width: 200 },
               //{ field: "NamaTengahIns4", title: "NamaTengahIns4", hidden: true, width: 200 },
               //{ field: "NamaBelakangIns4", title: "NamaBelakangIns4", hidden: true, width: 200 },
               //{ field: "Jabatan4", title: "Jabatan4", hidden: true, width: 200 },
               //{ field: "JumlahIdentitasIns4", title: "JumlahIdentitasIns4", hidden: true, width: 200 },
               //{ field: "IdentitasIns41", title: "IdentitasIns41", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns41", title: "NoIdentitasIns41", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns41", title: "RegistrationDateIdentitasIns41", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns41), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns41", title: "ExpiredDateIdentitasIns41", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns41), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns42", title: "IdentitasIns42", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns42", title: "NoIdentitasIns42", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns42", title: "RegistrationDateIdentitasIns42", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns42), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns42", title: "ExpiredDateIdentitasIns42", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns42), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns43", title: "IdentitasIns43", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns43", title: "NoIdentitasIns43", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns43", title: "RegistrationDateIdentitasIns43", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns43), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns43", title: "ExpiredDateIdentitasIns43", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns43), 'dd/MM/yyyy')#" },
               //{ field: "IdentitasIns44", title: "IdentitasIns44", hidden: true, width: 200 },
               //{ field: "NoIdentitasIns44", title: "NoIdentitasIns44", hidden: true, width: 200 },
               //{ field: "RegistrationDateIdentitasIns44", title: "RegistrationDateIdentitasIns44", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasIns44), 'dd/MM/yyyy')#" },
               //{ field: "ExpiredDateIdentitasIns44", title: "ExpiredDateIdentitasIns44", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasIns44), 'dd/MM/yyyy')#" },
               //{ field: "BIMemberCode1", title: "BIMemberCode1", hidden: true, width: 200 },
               //{ field: "BIMemberCode2", title: "BIMemberCode2", hidden: true, width: 200 },
               //{ field: "BIMemberCode3", title: "BIMemberCode3", hidden: true, width: 200 },
               //{ field: "PhoneIns1", title: "PhoneIns1", hidden: true, width: 200 },
               //{ field: "EmailIns1", title: "EmailIns1", hidden: true, width: 200 },
               //{ field: "PhoneIns2", title: "PhoneIns2", hidden: true, width: 200 },
               //{ field: "EmailIns2", title: "EmailIns2", hidden: true, width: 200 },
               //{ field: "InvestorsRiskProfile", title: "InvestorsRiskProfile", hidden: true, width: 200 },
               //{ field: "InvestorsRiskProfileDesc", title: "InvestorsRiskProfileDesc", hidden: true, width: 200 },
               //{ field: "AssetOwner", title: "AssetOwner", hidden: true, width: 200 },
               //{ field: "AssetOwnerDesc", title: "AssetOwnerDesc", hidden: true, width: 200 },
               //{ field: "StatementType", title: "StatementType", hidden: true, width: 200 },
               //{ field: "StatementTypeDesc", title: "StatementTypeDesc", hidden: true, width: 200 },
               //{ field: "FATCA", title: "FATCA", hidden: true, width: 200 },
               //{ field: "FATCADesc", title: "FATCADesc", hidden: true, width: 200 },
               //{ field: "TIN", title: "TIN", hidden: true, width: 200 },
               //{ field: "TINIssuanceCountry", title: "TINIssuanceCountry", hidden: true, width: 200 },
               //{ field: "TINIssuanceCountryDesc", title: "TINIssuanceCountryDesc", hidden: true, width: 200 },
               //{ field: "GIIN", title: "GIIN", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerName", title: "SubstantialOwnerName", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerAddress", title: "SubstantialOwnerAddress", hidden: true, width: 200 },
               //{ field: "SubstantialOwnerTIN", title: "SubstantialOwnerTIN", hidden: true, width: 200 },
               //{ field: "BankBranchName1", title: "BankBranchName1", hidden: true, width: 200 },
               //{ field: "BankBranchName2", title: "BankBranchName2", hidden: true, width: 200 },
               //{ field: "BankBranchName3", title: "BankBranchName3", hidden: true, width: 200 },
               //{ field: "BankCountry1", title: "BankCountry1", hidden: true, width: 200 },
               //{ field: "BankCountry1Desc", title: "BankCountry1Desc", hidden: true, width: 200 },
               //{ field: "BankCountry2", title: "BankCountry2", hidden: true, width: 200 },
               //{ field: "BankCountry2Desc", title: "BankCountry2Desc", hidden: true, width: 200 },
               //{ field: "BankCountry3", title: "BankCountry3", hidden: true, width: 200 },
               //{ field: "BankCountry3Desc", title: "BankCountry3Desc", hidden: true, width: 200 },
               //{ field: "EntryUsersID", title: "Entry ID", width: 200 },
               //{ field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "UpdateUsersID", title: "UpdateID", width: 200 },
               //{ field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
               //{ field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               //{ field: "VoidUsersID", title: "VoidID", width: 200 },
               //{ field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "BitIsSuspend", title: "BitIsSuspend", width: 200, template: "#= BitIsSuspend ? 'Active' : 'Suspend' #" }
            ]

        });

    }

    function gridHistoryDataBound() {
        var grid = $("#gridFundClientHistory").data("kendoGrid");
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

    $("#BtnSearch").click(function () {
        refresh();
    });

    $("#search").change(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {
        //resetNotification();
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Detail");
            }
        });
    });

    //$("#BtnNew").click(function () {
    //    showDetails(null);
    //});

    //$("#BtnAdd").click(function () {
    //    var val = validateData();
    //    if (val == 1) {
    //        resetNotification();
    //        if ($("#ClientCategory").data("kendoComboBox").value() == 1) {
    //            clearDataIns();
    //        }
    //        else if ($("#ClientCategory").data("kendoComboBox").value() == 2) {
    //            clearDataInd();
    //        }
    //        alertify.confirm("Are you sure want to Add data?", function (e) {
    //            if (e) {
    //                //$.ajax({
    //                //    url: window.location.origin + "/Radsoft/FundClient/FundClient_GenerateNewClientID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ClientCategory").data("kendoComboBox").value() + "/" + "FundClient_GenerateNewClientID",
    //                //    type: 'GET',
    //                //    contentType: "application/json;charset=utf-8",
    //                //    success: function (data) {
    //                //        $("#ID").val(data);
    //                //        alertify.alert("Your New Client ID is " + data);

    //                var FundClient = {
    //                    ID: $('#ID').val(),
    //                    Name: $('#Name').val(),
    //                    ClientCategory: $('#ClientCategory').val(),
    //                    InvestorType: $('#InvestorType').val(),
    //                    InternalCategoryPK: $('#InternalCategoryPK').val(),
    //                    SellingAgentPK: $('#SellingAgentPK').val(),
    //                    SID: $('#SID').val(),
    //                    IFUACode: $('#IFUACode').val(),
    //                    Child: $('#Child').val(),
    //                    CompanyTypeOJK: $('#CompanyTypeOJK').val(),
    //                    BusinessTypeOJK: $('#BusinessTypeOJK').val(),
    //                    ARIA: $('#ARIA').val(),
    //                    RiskProfileScore: $("#RiskProfileScore").val(),
    //                    KYCRiskProfile: $("#KYCRiskProfile").val(),
    //                    Registered: $('#Registered').val(),
    //                    JumlahDanaAwal: $('#JumlahDanaAwal').val(),
    //                    JumlahDanaSaatIniCash: $('#JumlahDanaSaatIniCash').val(),
    //                    JumlahDanaSaatIni: $('#JumlahDanaSaatIni').val(),
    //                    Negara: $('#Negara').val(),
    //                    Nationality: $('#Nationality').val(),
    //                    NPWP: $('#NPWP').val(),
    //                    SACode: $('#SACode').val(),
    //                    Propinsi: $('#Propinsi').val(),
    //                    TeleponSelular: $('#TeleponSelular').val(),
    //                    Email: $('#Email').val(),
    //                    Fax: $('#Fax').val(),
    //                    DormantDate: $('#DormantDate').val(),
    //                    Description: $('#Description').val(),
    //                    JumlahBank: $('#JumlahBank').val(),
    //                    NamaBank1: $('#NamaBank1').val(),
    //                    NomorRekening1: $('#NomorRekening1').val(),
    //                    BICCode1: $('#BICCode1').val(),
    //                    NamaNasabah1: $('#NamaNasabah1').val(),
    //                    MataUang1: $('#MataUang1').val(),
    //                    NamaBank2: $('#NamaBank2').val(),
    //                    NomorRekening2: $('#NomorRekening2').val(),
    //                    BICCode2: $('#BICCode2').val(),
    //                    NamaNasabah2: $('#NamaNasabah2').val(),
    //                    MataUang2: $('#MataUang2').val(),
    //                    NamaBank3: $('#NamaBank3').val(),
    //                    NomorRekening3: $('#NomorRekening3').val(),
    //                    BICCode3: $('#BICCode3').val(),
    //                    NamaNasabah3: $('#NamaNasabah3').val(),
    //                    MataUang3: $('#MataUang3').val(),
    //                    NamaDepanInd: $('#NamaDepanInd').val(),
    //                    NamaTengahInd: $('#NamaTengahInd').val(),
    //                    NamaBelakangInd: $('#NamaBelakangInd').val(),
    //                    TempatLahir: $('#TempatLahir').val(),
    //                    TanggalLahir: $('#TanggalLahir').val(),
    //                    JenisKelamin: $('#JenisKelamin').val(),
    //                    StatusPerkawinan: $('#StatusPerkawinan').val(),
    //                    Pekerjaan: $('#Pekerjaan').val(),
    //                    Pendidikan: $('#Pendidikan').val(),
    //                    Agama: $('#Agama').val(),
    //                    PenghasilanInd: $('#PenghasilanInd').val(),
    //                    SumberDanaInd: $('#SumberDanaInd').val(),
    //                    MaksudTujuanInd: $('#MaksudTujuanInd').val(),
    //                    AlamatInd1: $('#AlamatInd1').val(),
    //                    KodeKotaInd1: $('#KodeKotaInd1').val(),
    //                    KodePosInd1: $('#KodePosInd1').val(),
    //                    AlamatInd2: $('#AlamatInd2').val(),
    //                    KodeKotaInd2: $('#KodeKotaInd2').val(),
    //                    KodePosInd2: $('#KodePosInd2').val(),
    //                    NamaPerusahaan: $('#NamaPerusahaan').val(),
    //                    Domisili: $('#Domisili').val(),
    //                    Tipe: $('#Tipe').val(),
    //                    Karakteristik: $('#Karakteristik').val(),
    //                    NoSKD: $('#NoSKD').val(),
    //                    PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
    //                    SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
    //                    MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
    //                    AlamatPerusahaan: $('#AlamatPerusahaan').val(),
    //                    KodeKotaIns: $('#KodeKotaIns').val(),
    //                    KodePosIns: $('#KodePosIns').val(),
    //                    SpouseName: $('#SpouseName').val(),
    //                    MotherMaidenName: $('#MotherMaidenName').val(),
    //                    AhliWaris: $('#AhliWaris').val(),
    //                    HubunganAhliWaris: $('#HubunganAhliWaris').val(),
    //                    NatureOfBusiness: $('#NatureOfBusiness').val(),
    //                    NatureOfBusinessDesc: $('#NatureOfBusinessDesc').val(),
    //                    Politis: $('#Politis').val(),
    //                    PolitisLainnya: $('#PolitisLainnya').val(),
    //                    TeleponRumah: $('#TeleponRumah').val(),
    //                    OtherAlamatInd1: $('#OtherAlamatInd1').val(),
    //                    OtherKodeKotaInd1: $('#OtherKodeKotaInd1').val(),
    //                    OtherKodePosInd1: $('#OtherKodePosInd1').val(),
    //                    OtherPropinsiInd1: $('#OtherPropinsiInd1').val(),
    //                    CountryOfBirth: $('#CountryOfBirth').val(),
    //                    OtherNegaraInd1: $('#OtherNegaraInd1').val(),
    //                    OtherAlamatInd2: $('#OtherAlamatInd2').val(),
    //                    OtherKodeKotaInd2: $('#OtherKodeKotaInd2').val(),
    //                    OtherKodePosInd2: $('#OtherKodePosInd2').val(),
    //                    OtherPropinsiInd2: $('#OtherPropinsiInd2').val(),
    //                    OtherNegaraInd2: $('#OtherNegaraInd2').val(),
    //                    OtherAlamatInd3: $('#OtherAlamatInd3').val(),
    //                    OtherKodeKotaInd3: $('#OtherKodeKotaInd3').val(),
    //                    OtherKodePosInd3: $('#OtherKodePosInd3').val(),
    //                    OtherPropinsiInd3: $('#OtherPropinsiInd3').val(),
    //                    OtherNegaraInd3: $('#OtherNegaraInd3').val(),
    //                    OtherTeleponRumah: $('#OtherTeleponRumah').val(),
    //                    OtherTeleponSelular: $('#OtherTeleponSelular').val(),
    //                    OtherEmail: $('#OtherEmail').val(),
    //                    OtherFax: $('#OtherFax').val(),
    //                    JumlahIdentitasInd: $('#JumlahIdentitasInd').val(),
    //                    IdentitasInd1: $('#IdentitasInd1').val(),
    //                    NoIdentitasInd1: $('#NoIdentitasInd1').val(),
    //                    RegistrationDateIdentitasInd1: $('#RegistrationDateIdentitasInd1').val(),
    //                    ExpiredDateIdentitasInd1: $('#ExpiredDateIdentitasInd1').val(),
    //                    IdentitasInd2: $('#IdentitasInd2').val(),
    //                    NoIdentitasInd2: $('#NoIdentitasInd2').val(),
    //                    RegistrationDateIdentitasInd2: $('#RegistrationDateIdentitasInd2').val(),
    //                    ExpiredDateIdentitasInd2: $('#ExpiredDateIdentitasInd2').val(),
    //                    IdentitasInd3: $('#IdentitasInd3').val(),
    //                    NoIdentitasInd3: $('#NoIdentitasInd3').val(),
    //                    RegistrationDateIdentitasInd3: $('#RegistrationDateIdentitasInd3').val(),
    //                    ExpiredDateIdentitasInd3: $('#ExpiredDateIdentitasInd3').val(),
    //                    IdentitasInd4: $('#IdentitasInd4').val(),
    //                    NoIdentitasInd4: $('#NoIdentitasInd4').val(),
    //                    RegistrationDateIdentitasInd4: $('#RegistrationDateIdentitasInd4').val(),
    //                    ExpiredDateIdentitasInd4: $('#ExpiredDateIdentitasInd4').val(),
    //                    RegistrationNPWP: $('#RegistrationNPWP').val(),
    //                    ExpiredDateSKD: $('#ExpiredDateSKD').val(),
    //                    TanggalBerdiri: $('#TanggalBerdiri').val(),
    //                    LokasiBerdiri: $('#LokasiBerdiri').val(),
    //                    TeleponBisnis: $('#TeleponBisnis').val(),
    //                    NomorAnggaran: $('#NomorAnggaran').val(),
    //                    NomorSIUP: $('#NomorSIUP').val(),
    //                    AssetFor1Year: $('#AssetFor1Year').val(),
    //                    AssetFor2Year: $('#AssetFor2Year').val(),
    //                    AssetFor3Year: $('#AssetFor3Year').val(),
    //                    OperatingProfitFor1Year: $('#OperatingProfitFor1Year').val(),
    //                    OperatingProfitFor2Year: $('#OperatingProfitFor2Year').val(),
    //                    OperatingProfitFor3Year: $('#OperatingProfitFor3Year').val(),
    //                    JumlahPejabat: $('#JumlahPejabat').val(),
    //                    NamaDepanIns1: $('#NamaDepanIns1').val(),
    //                    NamaTengahIns1: $('#NamaTengahIns1').val(),
    //                    NamaBelakangIns1: $('#NamaBelakangIns1').val(),
    //                    Jabatan1: $('#Jabatan1').val(),
    //                    JumlahIdentitasIns1: $('#JumlahIdentitasIns1').val(),
    //                    IdentitasIns11: $('#IdentitasIns11').val(),
    //                    NoIdentitasIns11: $('#NoIdentitasIns11').val(),
    //                    RegistrationDateIdentitasIns11: $('#RegistrationDateIdentitasIns11').val(),
    //                    ExpiredDateIdentitasIns11: $('#ExpiredDateIdentitasIns11').val(),
    //                    IdentitasIns12: $('#IdentitasIns12').val(),
    //                    NoIdentitasIns12: $('#NoIdentitasIns12').val(),
    //                    RegistrationDateIdentitasIns12: $('#RegistrationDateIdentitasIns12').val(),
    //                    ExpiredDateIdentitasIns12: $('#ExpiredDateIdentitasIns12').val(),
    //                    IdentitasIns13: $('#IdentitasIns13').val(),
    //                    NoIdentitasIns13: $('#NoIdentitasIns13').val(),
    //                    RegistrationDateIdentitasIns13: $('#RegistrationDateIdentitasIns13').val(),
    //                    ExpiredDateIdentitasIns13: $('#ExpiredDateIdentitasIns13').val(),
    //                    IdentitasIns14: $('#IdentitasIns14').val(),
    //                    NoIdentitasIns14: $('#NoIdentitasIns14').val(),
    //                    RegistrationDateIdentitasIns14: $('#RegistrationDateIdentitasIns14').val(),
    //                    ExpiredDateIdentitasIns14: $('#ExpiredDateIdentitasIns14').val(),
    //                    NamaDepanIns2: $('#NamaDepanIns2').val(),
    //                    NamaTengahIns2: $('#NamaTengahIns2').val(),
    //                    NamaBelakangIns2: $('#NamaBelakangIns2').val(),
    //                    Jabatan2: $('#Jabatan2').val(),
    //                    JumlahIdentitasIns2: $('#JumlahIdentitasIns2').val(),
    //                    IdentitasIns21: $('#IdentitasIns21').val(),
    //                    NoIdentitasIns21: $('#NoIdentitasIns21').val(),
    //                    RegistrationDateIdentitasIns21: $('#RegistrationDateIdentitasIns21').val(),
    //                    ExpiredDateIdentitasIns21: $('#ExpiredDateIdentitasIns21').val(),
    //                    IdentitasIns22: $('#IdentitasIns22').val(),
    //                    NoIdentitasIns22: $('#NoIdentitasIns22').val(),
    //                    RegistrationDateIdentitasIns22: $('#RegistrationDateIdentitasIns22').val(),
    //                    ExpiredDateIdentitasIns22: $('#ExpiredDateIdentitasIns22').val(),
    //                    IdentitasIns23: $('#IdentitasIns23').val(),
    //                    NoIdentitasIns23: $('#NoIdentitasIns23').val(),
    //                    RegistrationDateIdentitasIns23: $('#RegistrationDateIdentitasIns23').val(),
    //                    ExpiredDateIdentitasIns23: $('#ExpiredDateIdentitasIns23').val(),
    //                    IdentitasIns24: $('#IdentitasIns24').val(),
    //                    NoIdentitasIns24: $('#NoIdentitasIns24').val(),
    //                    RegistrationDateIdentitasIns24: $('#RegistrationDateIdentitasIns24').val(),
    //                    ExpiredDateIdentitasIns24: $('#ExpiredDateIdentitasIns24').val(),
    //                    NamaDepanIns3: $('#NamaDepanIns3').val(),
    //                    NamaTengahIns3: $('#NamaTengahIns3').val(),
    //                    NamaBelakangIns3: $('#NamaBelakangIns3').val(),
    //                    Jabatan3: $('#Jabatan3').val(),
    //                    JumlahIdentitasIns3: $('#JumlahIdentitasIns3').val(),
    //                    IdentitasIns31: $('#IdentitasIns31').val(),
    //                    NoIdentitasIns31: $('#NoIdentitasIns31').val(),
    //                    RegistrationDateIdentitasIns31: $('#RegistrationDateIdentitasIns31').val(),
    //                    ExpiredDateIdentitasIns31: $('#ExpiredDateIdentitasIns31').val(),
    //                    IdentitasIns32: $('#IdentitasIns32').val(),
    //                    NoIdentitasIns32: $('#NoIdentitasIns32').val(),
    //                    RegistrationDateIdentitasIns32: $('#RegistrationDateIdentitasIns32').val(),
    //                    ExpiredDateIdentitasIns32: $('#ExpiredDateIdentitasIns32').val(),
    //                    IdentitasIns33: $('#IdentitasIns33').val(),
    //                    NoIdentitasIns33: $('#NoIdentitasIns33').val(),
    //                    RegistrationDateIdentitasIns33: $('#RegistrationDateIdentitasIns33').val(),
    //                    ExpiredDateIdentitasIns33: $('#ExpiredDateIdentitasIns33').val(),
    //                    IdentitasIns34: $('#IdentitasIns34').val(),
    //                    NoIdentitasIns34: $('#NoIdentitasIns34').val(),
    //                    RegistrationDateIdentitasIns34: $('#RegistrationDateIdentitasIns34').val(),
    //                    ExpiredDateIdentitasIns34: $('#ExpiredDateIdentitasIns34').val(),
    //                    NamaDepanIns4: $('#NamaDepanIns4').val(),
    //                    NamaTengahIns4: $('#NamaTengahIns4').val(),
    //                    NamaBelakangIns4: $('#NamaBelakangIns4').val(),
    //                    Jabatan4: $('#Jabatan4').val(),
    //                    JumlahIdentitasIns4: $('#JumlahIdentitasIns4').val(),
    //                    IdentitasIns41: $('#IdentitasIns41').val(),
    //                    NoIdentitasIns41: $('#NoIdentitasIns41').val(),
    //                    RegistrationDateIdentitasIns41: $('#RegistrationDateIdentitasIns41').val(),
    //                    ExpiredDateIdentitasIns41: $('#ExpiredDateIdentitasIns41').val(),
    //                    IdentitasIns42: $('#IdentitasIns42').val(),
    //                    NoIdentitasIns42: $('#NoIdentitasIns42').val(),
    //                    RegistrationDateIdentitasIns42: $('#RegistrationDateIdentitasIns42').val(),
    //                    ExpiredDateIdentitasIns42: $('#ExpiredDateIdentitasIns42').val(),
    //                    IdentitasIns43: $('#IdentitasIns43').val(),
    //                    NoIdentitasIns43: $('#NoIdentitasIns43').val(),
    //                    RegistrationDateIdentitasIns43: $('#RegistrationDateIdentitasIns43').val(),
    //                    ExpiredDateIdentitasIns43: $('#ExpiredDateIdentitasIns43').val(),
    //                    IdentitasIns44: $('#IdentitasIns44').val(),
    //                    NoIdentitasIns44: $('#NoIdentitasIns44').val(),
    //                    RegistrationDateIdentitasIns44: $('#RegistrationDateIdentitasIns44').val(),
    //                    ExpiredDateIdentitasIns44: $('#ExpiredDateIdentitasIns44').val(),
    //                    BIMemberCode1: $('#BIMemberCode1').val(),
    //                    BIMemberCode2: $('#BIMemberCode2').val(),
    //                    BIMemberCode3: $('#BIMemberCode3').val(),
    //                    PhoneIns1: $('#PhoneIns1').val(),
    //                    EmailIns1: $('#EmailIns1').val(),
    //                    PhoneIns2: $('#PhoneIns2').val(),
    //                    EmailIns2: $('#EmailIns2').val(),
    //                    InvestorsRiskProfile: $('#InvestorsRiskProfile').val(),
    //                    AssetOwner: $('#AssetOwner').val(),
    //                    StatementType: $('#StatementType').val(),
    //                    FATCA: $('#FATCA').val(),
    //                    TIN: $('#TIN').val(),
    //                    TINIssuanceCountry: $('#TINIssuanceCountry').val(),
    //                    GIIN: $('#GIIN').val(),
    //                    SubstantialOwnerName: $('#SubstantialOwnerName').val(),
    //                    SubstantialOwnerAddress: $('#SubstantialOwnerAddress').val(),
    //                    SubstantialOwnerTIN: $('#SubstantialOwnerTIN').val(),
    //                    BankBranchName1: $('#BankBranchName1').val(),
    //                    BankBranchName2: $('#BankBranchName2').val(),
    //                    BankBranchName3: $('#BankBranchName3').val(),
    //                    BankCountry1: $('#BankCountry1').val(),
    //                    BankCountry2: $('#BankCountry2').val(),
    //                    BankCountry3: $('#BankCountry3').val(),
    //                    EntryUsersID: sessionStorage.getItem("user"),
    //                    CountryofCorrespondence: $('#CountryofCorrespondence').val(),
    //                    CountryofDomicile: $('#CountryofDomicile').val(),
    //                    SIUPExpirationDate: $('#SIUPExpirationDate').val(),
    //                    CountryofEstablishment: $('#CountryofEstablishment').val(),
    //                    CompanyCityName: $('#CompanyCityName').val(),
    //                    CountryofCompany: $('#CountryofCompany').val(),
    //                    NPWPPerson1: $('#NPWPPerson1').val(),
    //                    //RDN
    //                    BankRDNPK: $('#BankRDNPK').val(),
    //                    RDNAccountNo: $('#RDNAccountNo').val(),
    //                    RDNAccountName: $('#RDNAccountName').val(),
    //                    NPWPPerson2: $('#NPWPPerson2').val(),

    //                    SpouseBirthPlace: $('#SpouseBirthPlace').val(), //yang ini
    //                    SpouseDateOfBirth: $('#SpouseDateOfBirth').val(),
    //                    SpouseDateOfBirth: $('#SpouseOccupation').val(),
    //                    SpouseDateOfBirth: $('#SpouseNatureOfBusiness').val(),
    //                    SpouseDateOfBirth: $('#SpouseNatureOfBusinessOther ').val(),
    //                    SpouseDateOfBirth: $('#SpouseIDNo').val(),
    //                    SpouseDateOfBirth: $('#SpouseNationality').val(),
    //                    SpouseDateOfBirth: $('#SpouseAnnualIncome').val(),
    //                    NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
    //                    NatureOfBusinessInsti: $('#NatureOfBusinessInsti').val(),
    //                };
    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_I",
    //                    type: 'POST',
    //                    data: JSON.stringify(FundClient),
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {
    //                        alertify.success(data);
    //                        win.close();
    //                        //refresh();
    //                    },
    //                    error: function (data) {
    //                        alertify.alert(data.responseText);
    //                    }
    //                });
    //                //        },
    //                //        error: function (data) {
    //                //            alertify.alert(data.responseText);
    //                //        }
    //                //    });
    //            }
    //        });
    //    }
    //});

    $("#BtnUpdate").click(function () {
        var val = 1;
        if (val == 1) {
            //resetNotification();
            if ($("#InvestorType").data("kendoComboBox").value() == 1) {
                clearDataIns();
            }
            else if ($("#InvestorType").data("kendoComboBox").value() == 2) {
                clearDataInd();
            }
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundClient = {
                                    FundClientPK: $('#FundClientPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    ClientCategory: $('#ClientCategory').val(),
                                    InvestorType: $('#InvestorType').val(),
                                    InternalCategoryPK: $('#InternalCategoryPK').val(),
                                    RiskProfileScore: $("#RiskProfileScore").val(),
                                    KYCRiskProfile: $("#KYCRiskProfile").val(),
                                    SellingAgentPK: $('#SellingAgentPK').val(),
                                    SID: $('#SID').val(),
                                    IFUACode: $('#IFUACode').val(),
                                    Child: $('#Child').val(),
                                    CompanyTypeOJK: $('#CompanyTypeOJK').val(),
                                    BusinessTypeOJK: $('#BusinessTypeOJK').val(),
                                    ARIA: $('#ARIA').val(),
                                    Registered: $('#Registered').val(),
                                    JumlahDanaAwal: $('#JumlahDanaAwal').val(),
                                    JumlahDanaSaatIniCash: $('#JumlahDanaSaatIniCash').val(),
                                    JumlahDanaSaatIni: $('#JumlahDanaSaatIni').val(),
                                    Negara: $('#Negara').val(),
                                    Nationality: $('#Nationality').val(),
                                    NPWP: $('#NPWP').val(),
                                    SACode: $('#SACode').val(),
                                    Propinsi: $('#Propinsi').val(),
                                    TeleponSelular: $('#TeleponSelular').val(),
                                    Email: $('#Email').val(),
                                    Fax: $('#Fax').val(),
                                    DormantDate: $('#DormantDate').val(),
                                    Description: $('#Description').val(),
                                    JumlahBank: $('#JumlahBank').val(),
                                    NamaBank1: $('#NamaBank1').val(),
                                    NomorRekening1: $('#NomorRekening1').val(),
                                    BICCode1: $('#BICCode1').val(),
                                    NamaNasabah1: $('#NamaNasabah1').val(),
                                    MataUang1: $('#MataUang1').val(),
                                    NamaBank2: $('#NamaBank2').val(),
                                    NomorRekening2: $('#NomorRekening2').val(),
                                    BICCode2: $('#BICCode2').val(),
                                    NamaNasabah2: $('#NamaNasabah2').val(),
                                    MataUang2: $('#MataUang2').val(),
                                    NamaBank3: $('#NamaBank3').val(),
                                    NomorRekening3: $('#NomorRekening3').val(),
                                    BICCode3: $('#BICCode3').val(),
                                    NamaNasabah3: $('#NamaNasabah3').val(),
                                    MataUang3: $('#MataUang3').val(),
                                    NamaDepanInd: $('#NamaDepanInd').val(),
                                    NamaTengahInd: $('#NamaTengahInd').val(),
                                    NamaBelakangInd: $('#NamaBelakangInd').val(),
                                    TempatLahir: $('#TempatLahir').val(),
                                    TanggalLahir: $('#TanggalLahir').val(),
                                    JenisKelamin: $('#JenisKelamin').val(),
                                    StatusPerkawinan: $('#StatusPerkawinan').val(),
                                    Pekerjaan: $('#Pekerjaan').val(),
                                    Pendidikan: $('#Pendidikan').val(),
                                    Agama: $('#Agama').val(),
                                    PenghasilanInd: $('#PenghasilanInd').val(),
                                    SumberDanaInd: $('#SumberDanaInd').val(),
                                    MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                                    AlamatInd1: $('#AlamatInd1').val(),
                                    KodeKotaInd1: $('#KodeKotaInd1').val(),
                                    KodePosInd1: $('#KodePosInd1').val(),
                                    AlamatInd2: $('#AlamatInd2').val(),
                                    KodeKotaInd2: $('#KodeKotaInd2').val(),
                                    KodePosInd2: $('#KodePosInd2').val(),
                                    NamaPerusahaan: $('#NamaPerusahaan').val(),
                                    Domisili: $('#Domisili').val(),
                                    Tipe: $('#Tipe').val(),
                                    Karakteristik: $('#Karakteristik').val(),
                                    NoSKD: $('#NoSKD').val(),
                                    PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
                                    SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
                                    MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
                                    AlamatPerusahaan: $('#AlamatPerusahaan').val(),
                                    KodeKotaIns: $('#KodeKotaIns').val(),
                                    KodePosIns: $('#KodePosIns').val(),
                                    SpouseName: $('#SpouseName').val(),
                                    MotherMaidenName: $('#MotherMaidenName').val(),
                                    AhliWaris: $('#AhliWaris').val(),
                                    HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                                    NatureOfBusiness: $('#NatureOfBusiness').val(),
                                    NatureOfBusinessDesc: $('#NatureOfBusinessDesc').val(),
                                    Politis: $('#Politis').val(),
                                    PolitisLainnya: $('#PolitisLainnya').val(),
                                    TeleponRumah: $('#TeleponRumah').val(),
                                    OtherAlamatInd1: $('#OtherAlamatInd1').val(),
                                    OtherKodeKotaInd1: $('#OtherKodeKotaInd1').val(),
                                    OtherKodePosInd1: $('#OtherKodePosInd1').val(),
                                    OtherPropinsiInd1: $('#OtherPropinsiInd1').val(),
                                    CountryOfBirth: $('#CountryOfBirth').val(),
                                    OtherNegaraInd1: $('#OtherNegaraInd1').val(),
                                    OtherAlamatInd2: $('#OtherAlamatInd2').val(),
                                    OtherKodeKotaInd2: $('#OtherKodeKotaInd2').val(),
                                    OtherKodePosInd2: $('#OtherKodePosInd2').val(),
                                    OtherPropinsiInd2: $('#OtherPropinsiInd2').val(),
                                    OtherNegaraInd2: $('#OtherNegaraInd2').val(),
                                    OtherAlamatInd3: $('#OtherAlamatInd3').val(),
                                    OtherKodeKotaInd3: $('#OtherKodeKotaInd3').val(),
                                    OtherKodePosInd3: $('#OtherKodePosInd3').val(),
                                    OtherPropinsiInd3: $('#OtherPropinsiInd3').val(),
                                    OtherNegaraInd3: $('#OtherNegaraInd3').val(),
                                    OtherTeleponRumah: $('#OtherTeleponRumah').val(),
                                    OtherTeleponSelular: $('#OtherTeleponSelular').val(),
                                    OtherEmail: $('#OtherEmail').val(),
                                    OtherFax: $('#OtherFax').val(),
                                    JumlahIdentitasInd: $('#JumlahIdentitasInd').val(),
                                    IdentitasInd1: $('#IdentitasInd1').val(),
                                    NoIdentitasInd1: $('#NoIdentitasInd1').val(),
                                    RegistrationDateIdentitasInd1: $('#RegistrationDateIdentitasInd1').val(),
                                    ExpiredDateIdentitasInd1: $('#ExpiredDateIdentitasInd1').val(),
                                    IdentitasInd2: $('#IdentitasInd2').val(),
                                    NoIdentitasInd2: $('#NoIdentitasInd2').val(),
                                    RegistrationDateIdentitasInd2: $('#RegistrationDateIdentitasInd2').val(),
                                    ExpiredDateIdentitasInd2: $('#ExpiredDateIdentitasInd2').val(),
                                    IdentitasInd3: $('#IdentitasInd3').val(),
                                    NoIdentitasInd3: $('#NoIdentitasInd3').val(),
                                    RegistrationDateIdentitasInd3: $('#RegistrationDateIdentitasInd3').val(),
                                    ExpiredDateIdentitasInd3: $('#ExpiredDateIdentitasInd3').val(),
                                    IdentitasInd4: $('#IdentitasInd4').val(),
                                    NoIdentitasInd4: $('#NoIdentitasInd4').val(),
                                    RegistrationDateIdentitasInd4: $('#RegistrationDateIdentitasInd4').val(),
                                    ExpiredDateIdentitasInd4: $('#ExpiredDateIdentitasInd4').val(),
                                    RegistrationNPWP: $('#RegistrationNPWP').val(),
                                    ExpiredDateSKD: $('#ExpiredDateSKD').val(),
                                    TanggalBerdiri: $('#TanggalBerdiri').val(),
                                    LokasiBerdiri: $('#LokasiBerdiri').val(),
                                    TeleponBisnis: $('#TeleponBisnis').val(),
                                    NomorAnggaran: $('#NomorAnggaran').val(),
                                    NomorSIUP: $('#NomorSIUP').val(),
                                    AssetFor1Year: $('#AssetFor1Year').val(),
                                    AssetFor2Year: $('#AssetFor2Year').val(),
                                    AssetFor3Year: $('#AssetFor3Year').val(),
                                    OperatingProfitFor1Year: $('#OperatingProfitFor1Year').val(),
                                    OperatingProfitFor2Year: $('#OperatingProfitFor2Year').val(),
                                    OperatingProfitFor3Year: $('#OperatingProfitFor3Year').val(),
                                    JumlahPejabat: $('#JumlahPejabat').val(),
                                    NamaDepanIns1: $('#NamaDepanIns1').val(),
                                    NamaTengahIns1: $('#NamaTengahIns1').val(),
                                    NamaBelakangIns1: $('#NamaBelakangIns1').val(),
                                    Jabatan1: $('#Jabatan1').val(),
                                    JumlahIdentitasIns1: $('#JumlahIdentitasIns1').val(),
                                    IdentitasIns11: $('#IdentitasIns11').val(),
                                    NoIdentitasIns11: $('#NoIdentitasIns11').val(),
                                    RegistrationDateIdentitasIns11: $('#RegistrationDateIdentitasIns11').val(),
                                    ExpiredDateIdentitasIns11: $('#ExpiredDateIdentitasIns11').val(),
                                    IdentitasIns12: $('#IdentitasIns12').val(),
                                    NoIdentitasIns12: $('#NoIdentitasIns12').val(),
                                    RegistrationDateIdentitasIns12: $('#RegistrationDateIdentitasIns12').val(),
                                    ExpiredDateIdentitasIns12: $('#ExpiredDateIdentitasIns12').val(),
                                    IdentitasIns13: $('#IdentitasIns13').val(),
                                    NoIdentitasIns13: $('#NoIdentitasIns13').val(),
                                    RegistrationDateIdentitasIns13: $('#RegistrationDateIdentitasIns13').val(),
                                    ExpiredDateIdentitasIns13: $('#ExpiredDateIdentitasIns13').val(),
                                    IdentitasIns14: $('#IdentitasIns14').val(),
                                    NoIdentitasIns14: $('#NoIdentitasIns14').val(),
                                    RegistrationDateIdentitasIns14: $('#RegistrationDateIdentitasIns14').val(),
                                    ExpiredDateIdentitasIns14: $('#ExpiredDateIdentitasIns14').val(),
                                    NamaDepanIns2: $('#NamaDepanIns2').val(),
                                    NamaTengahIns2: $('#NamaTengahIns2').val(),
                                    NamaBelakangIns2: $('#NamaBelakangIns2').val(),
                                    Jabatan2: $('#Jabatan2').val(),
                                    JumlahIdentitasIns2: $('#JumlahIdentitasIns2').val(),
                                    IdentitasIns21: $('#IdentitasIns21').val(),
                                    NoIdentitasIns21: $('#NoIdentitasIns21').val(),
                                    RegistrationDateIdentitasIns21: $('#RegistrationDateIdentitasIns21').val(),
                                    ExpiredDateIdentitasIns21: $('#ExpiredDateIdentitasIns21').val(),
                                    IdentitasIns22: $('#IdentitasIns22').val(),
                                    NoIdentitasIns22: $('#NoIdentitasIns22').val(),
                                    RegistrationDateIdentitasIns22: $('#RegistrationDateIdentitasIns22').val(),
                                    ExpiredDateIdentitasIns22: $('#ExpiredDateIdentitasIns22').val(),
                                    IdentitasIns23: $('#IdentitasIns23').val(),
                                    NoIdentitasIns23: $('#NoIdentitasIns23').val(),
                                    RegistrationDateIdentitasIns23: $('#RegistrationDateIdentitasIns23').val(),
                                    ExpiredDateIdentitasIns23: $('#ExpiredDateIdentitasIns23').val(),
                                    IdentitasIns24: $('#IdentitasIns24').val(),
                                    NoIdentitasIns24: $('#NoIdentitasIns24').val(),
                                    RegistrationDateIdentitasIns24: $('#RegistrationDateIdentitasIns24').val(),
                                    ExpiredDateIdentitasIns24: $('#ExpiredDateIdentitasIns24').val(),
                                    NamaDepanIns3: $('#NamaDepanIns3').val(),
                                    NamaTengahIns3: $('#NamaTengahIns3').val(),
                                    NamaBelakangIns3: $('#NamaBelakangIns3').val(),
                                    Jabatan3: $('#Jabatan3').val(),
                                    JumlahIdentitasIns3: $('#JumlahIdentitasIns3').val(),
                                    IdentitasIns31: $('#IdentitasIns31').val(),
                                    NoIdentitasIns31: $('#NoIdentitasIns31').val(),
                                    RegistrationDateIdentitasIns31: $('#RegistrationDateIdentitasIns31').val(),
                                    ExpiredDateIdentitasIns31: $('#ExpiredDateIdentitasIns31').val(),
                                    IdentitasIns32: $('#IdentitasIns32').val(),
                                    NoIdentitasIns32: $('#NoIdentitasIns32').val(),
                                    RegistrationDateIdentitasIns32: $('#RegistrationDateIdentitasIns32').val(),
                                    ExpiredDateIdentitasIns32: $('#ExpiredDateIdentitasIns32').val(),
                                    IdentitasIns33: $('#IdentitasIns33').val(),
                                    NoIdentitasIns33: $('#NoIdentitasIns33').val(),
                                    RegistrationDateIdentitasIns33: $('#RegistrationDateIdentitasIns33').val(),
                                    ExpiredDateIdentitasIns33: $('#ExpiredDateIdentitasIns33').val(),
                                    IdentitasIns34: $('#IdentitasIns34').val(),
                                    NoIdentitasIns34: $('#NoIdentitasIns34').val(),
                                    RegistrationDateIdentitasIns34: $('#RegistrationDateIdentitasIns34').val(),
                                    ExpiredDateIdentitasIns34: $('#ExpiredDateIdentitasIns34').val(),
                                    NamaDepanIns4: $('#NamaDepanIns4').val(),
                                    NamaTengahIns4: $('#NamaTengahIns4').val(),
                                    NamaBelakangIns4: $('#NamaBelakangIns4').val(),
                                    Jabatan4: $('#Jabatan4').val(),
                                    JumlahIdentitasIns4: $('#JumlahIdentitasIns4').val(),
                                    IdentitasIns41: $('#IdentitasIns41').val(),
                                    NoIdentitasIns41: $('#NoIdentitasIns41').val(),
                                    RegistrationDateIdentitasIns41: $('#RegistrationDateIdentitasIns41').val(),
                                    ExpiredDateIdentitasIns41: $('#ExpiredDateIdentitasIns41').val(),
                                    IdentitasIns42: $('#IdentitasIns42').val(),
                                    NoIdentitasIns42: $('#NoIdentitasIns42').val(),
                                    RegistrationDateIdentitasIns42: $('#RegistrationDateIdentitasIns42').val(),
                                    ExpiredDateIdentitasIns42: $('#ExpiredDateIdentitasIns42').val(),
                                    IdentitasIns43: $('#IdentitasIns43').val(),
                                    NoIdentitasIns43: $('#NoIdentitasIns43').val(),
                                    RegistrationDateIdentitasIns43: $('#RegistrationDateIdentitasIns43').val(),
                                    ExpiredDateIdentitasIns43: $('#ExpiredDateIdentitasIns43').val(),
                                    IdentitasIns44: $('#IdentitasIns44').val(),
                                    NoIdentitasIns44: $('#NoIdentitasIns44').val(),
                                    RegistrationDateIdentitasIns44: $('#RegistrationDateIdentitasIns44').val(),
                                    ExpiredDateIdentitasIns44: $('#ExpiredDateIdentitasIns44').val(),
                                    BIMemberCode1: $('#BIMemberCode1').val(),
                                    BIMemberCode2: $('#BIMemberCode2').val(),
                                    BIMemberCode3: $('#BIMemberCode3').val(),
                                    PhoneIns1: $('#PhoneIns1').val(),
                                    EmailIns1: $('#EmailIns1').val(),
                                    PhoneIns2: $('#PhoneIns2').val(),
                                    EmailIns2: $('#EmailIns2').val(),
                                    InvestorsRiskProfile: $('#InvestorsRiskProfile').val(),
                                    AssetOwner: $('#AssetOwner').val(),
                                    StatementType: $('#StatementType').val(),
                                    FATCA: $('#FATCA').val(),
                                    TIN: $('#TIN').val(),
                                    TINIssuanceCountry: $('#TINIssuanceCountry').val(),
                                    GIIN: $('#GIIN').val(),
                                    SubstantialOwnerName: $('#SubstantialOwnerName').val(),
                                    SubstantialOwnerAddress: $('#SubstantialOwnerAddress').val(),
                                    SubstantialOwnerTIN: $('#SubstantialOwnerTIN').val(),
                                    BankBranchName1: $('#BankBranchName1').val(),
                                    BankBranchName2: $('#BankBranchName2').val(),
                                    BankBranchName3: $('#BankBranchName3').val(),
                                    BankCountry1: $('#BankCountry1').val(),
                                    BankCountry2: $('#BankCountry2').val(),
                                    BankCountry3: $('#BankCountry3').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user"),
                                    CountryofCorrespondence: $('#CountryofCorrespondence').val(),
                                    CountryofDomicile: $('#CountryofDomicile').val(),
                                    SIUPExpirationDate: $('#SIUPExpirationDate').val(),
                                    CountryofEstablishment: $('#CountryofEstablishment').val(),
                                    CompanyCityName: $('#CompanyCityName').val(),
                                    CountryofCompany: $('#CountryofCompany').val(),
                                    NPWPPerson1: $('#NPWPPerson1').val(),
                                    //RDN
                                    BankRDNPK: $('#BankRDNPK').val(),
                                    RDNAccountNo: $('#RDNAccountNo').val(),
                                    RDNAccountName: $('#RDNAccountName').val(),

                                    NPWPPerson2: $('#NPWPPerson2').val(),
                                    SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                                    SpouseDateOfBirth: $('#SpouseDateOfBirth ').val(),
                                    SpouseDateOfBirth: $('#SpouseOccupation').val(),
                                    SpouseDateOfBirth: $('#SpouseNatureOfBusiness').val(),
                                    SpouseDateOfBirth: $('#SpouseNatureOfBusinessOther ').val(),
                                    SpouseDateOfBirth: $('#SpouseIDNo').val(),
                                    SpouseDateOfBirth: $('#SpouseNationality').val(),
                                    SpouseDateOfBirth: $('#SpouseAnnualIncome').val(),

                                    NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                                    NatureOfBusinessInsti: $('#NatureOfBusinessInsti').val(),



                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SuspendedAndInactiveClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundClient),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
                                        win.close();
                                        //refresh();
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
        //resetNotification();
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundClient" + "/" + $("#FundClientPK").val(),
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
        //resetNotification();
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            if ($("#ID").val() == null || $("#ID").val() == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundClient/FundClient_GenerateNewClientID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InvestorType").val() + "/" + $("#FundClientPK").val() + "/" + "FundClient_GenerateNewClientID",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#ID").val() == null || $("#ID").val() == "") {
                                            $("#ID").val(data);
                                            alertify.alert("Your New Client ID is " + data);
                                        }

                                        var FundClient = {
                                            ID: $('#ID').val(),
                                            FundClientPK: $('#FundClientPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_A",
                                            type: 'POST',
                                            data: JSON.stringify(FundClient),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.success(data);
                                                win.close();
                                                //refresh();
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                            else {
                                var FundClient = {
                                    ID: $('#ID').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_A",
                                    type: 'POST',
                                    data: JSON.stringify(FundClient),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
                                        win.close();
                                        //refresh();
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
    });

    $("#BtnVoid").click(function () {
        //resetNotification();
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundClient = {
                                FundClientPK: $('#FundClientPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_V",
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
                                    win.close();
                                    //refresh();

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
        //resetNotification();
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundClient = {
                                FundClientPK: $('#FundClientPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_R",
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
                                    win.close();
                                    //refresh();
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

    //$("#BtnGenerateARIAText").click(function () {
    //    showWinGenerateARIAText();
    //});

    //// Untuk Form Generate

    //function showWinGenerateARIAText() {
    //    $("#ParamCategory").kendoComboBox({
    //        dataValueField: "value",
    //        dataTextField: "text",
    //        dataSource: [
    //            { text: "Individual", value: 1 },
    //            { text: "Institution", value: 2 }
    //        ],
    //        filter: "contains",
    //        suggest: true,
    //        index: 0

    //    });

    //    WinGenerateARIAText.center();
    //    WinGenerateARIAText.open();

    //}

    //$("#BtnOkGenerateARIAText").click(function () {
    //    resetNotification();

    //    alertify.confirm("Are you sure want to Generate Report ?", function (e) {
    //        if (e) {
    //            $.blockUI({});
    //            var GenerateAria = {
    //                ClientCategory: $("#ParamCategory").val()
    //            };

    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/Reports/FundClientByClientCatReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                type: 'POST',
    //                data: JSON.stringify(GenerateAria),
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    var newwindow = window.open(data, '_blank');
    //                    //window.location = data
    //                    $.unblockUI();
    //                },
    //                error: function (data) {
    //                    alertify.error(data.responseText);
    //                }

    //            });
    //        }
    //    });

    //});

    //$("#BtnCancelGenerateARIAText").click(function () {
    //    resetNotification();
    //    alertify.confirm("Are you sure want to close Generate Report?", function (e) {
    //        if (e) {
    //            WinGenerateARIAText.close();
    //            alertify.success("Close Generate Report");
    //        }
    //    });
    //});

    //$("#BtnGenerateNKPD").click(function () {
    //    showWinGenerateNKPD();
    //});

    //// Untuk Form Generate

    //function showWinGenerateNKPD() {


    //    WinGenerateNKPD.center();
    //    WinGenerateNKPD.open();

    //}

    //$("#BtnOkGenerateNKPD").click(function () {
    //    resetNotification();

    //    alertify.confirm("Are you sure want to Generate ?", function (e) {
    //        if (e) {

    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/FundClient/GenerateNKPD/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateNKPD").data("kendoDatePicker").value(), "MM-dd-yy"),
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    var newwindow = window.open(data, '_blank');
    //                    //window.location = data
    //                },
    //                error: function (data) {
    //                    alertify.error(data.responseText);
    //                }

    //            });
    //        }
    //    });

    //});

    //$("#BtnCancelGenerateNKPD").click(function () {
    //    resetNotification();
    //    alertify.confirm("Are you sure want to close Generate?", function (e) {
    //        if (e) {
    //            WinGenerateNKPD.close();
    //            alertify.success("Close Generate");
    //        }
    //    });
    //});

    function RefreshTab(_index) {
        $("#TabSub").data("kendoTabStrip").select(_index);
    }

    function RequiredAttributes(_investorType) {

        if (_investorType == 1) {
            $("#NamaDepanInd").attr("required", true);
            //$("#Propinsi").attr("required", true);
            //$("#TeleponSelular").attr("required", true);
            $("#TempatLahir").attr("required", true);
            $("#TanggalLahir").attr("required", true);
            $("#CountryOfBirth").attr("required", true);
            $("#JenisKelamin").attr("required", true);
            $("#StatusPerkawinan").attr("required", true);
            $("#Pekerjaan").attr("required", true);
            $("#Pendidikan").attr("required", true);
            $("#Agama").attr("required", true);
            $("#PenghasilanInd").attr("required", true);
            $("#SumberDanaInd").attr("required", true);
            $("#MaksudTujuanInd").attr("required", true);
            $("#AlamatInd1").attr("required", true);
            $("#KodeKotaInd1").attr("required", true);
            //$("#KodePosInd1").attr("required", true);
            //$("#AlamatInd2").attr("required", true);
            //$("#KodeKotaInd2").attr("required", true);
            //$("#KodePosInd2").attr("required", true);
            //$("#SpouseName").attr("required", true);
            //$("#MotherMaidenName").attr("required", true);
            //$("#NatureOfBusiness").attr("required", true);
            //$("#Politis").attr("required", true);
            //$("#JumlahIdentitasInd").attr("required", true);
            $("#IdentitasInd1").attr("required", true);
            $("#NoIdentitasInd1").attr("required", true);
            //$("#RegistrationDateIdentitasInd1").attr("required", true);
            //$("#ExpiredDateIdentitasInd1").attr("required", true);

        }
        else if (_investorType == 2) {
            $("#NamaPerusahaan").attr("required", true);
            $("#Domisili").attr("required", true);
            $("#Tipe").attr("required", true);
            //$("#TipeOjk").attr("required", true);
            $("#Karakteristik").attr("required", true);
            //$("#NoSKD").attr("required", true);
            $("#PenghasilanInstitusi").attr("required", true);
            $("#SumberDanaInstitusi").attr("required", true);
            $("#MaksudTujuanInstitusi").attr("required", true);
            $("#AlamatPerusahaan").attr("required", true);
            $("#KodeKotaIns").attr("required", true);
            //$("#KodePosIns").attr("required", true);
            //$("#RegistrationNPWP").attr("required", true);
            $("#TanggalBerdiri").attr("required", true);
            $("#LokasiBerdiri").attr("required", true);
            //$("#NomorAnggaran").attr("required", true);
            //$("#NomorSIUP").attr("required", true);
            //$("#TeleponBisnis").attr("required", true);
            //$("#AssetFor1Year").attr("required", true);
            //$("#AssetFor2Year").attr("required", true);
            //$("#AssetFor3Year").attr("required", true);
            //$("#OperatingProfitFor1Year").attr("required", true);
            //$("#OperatingProfitFor2Year").attr("required", true);
            //$("#OperatingProfitFor3Year").attr("required", true);
            //$("#JumlahPejabat").attr("required", true);
            $("#NamaDepanIns1").attr("required", true);
            //$("#Jabatan1").attr("required", true);
            //$("#JumlahIdentitasIns1").attr("required", true);
            $("#IdentitasIns11").attr("required", true);
            $("#NoIdentitasIns11").attr("required", true);
            //$("#RegistrationDateIdentitasIns11").attr("required", true);
            //$("#ExpiredDateIdentitasIns11").attr("required", true);

        }

    }

    function RequiredAttributesTipeOjk(_TipeOjk) {

        if (_TipeOjk == 1) {
            $("#BusinessTypeOJK").attr("required", true);


        }
        else if (_TipeOjk == 2) {
            $("#NatureOfBusiness").attr("required", true);


        }

    }

    function ClearAttributes() {
        $("#NamaDepanInd").attr("required", false);
        //$("#Propinsi").attr("required", false);
        //$("#TeleponSelular").attr("required", false);
        $("#TempatLahir").attr("required", false);
        $("#TanggalLahir").attr("required", false);
        $("#CountryOfBirth").attr("required", false);
        $("#JenisKelamin").attr("required", false);
        $("#StatusPerkawinan").attr("required", false);
        $("#Pekerjaan").attr("required", false);
        $("#Pendidikan").attr("required", false);
        $("#Agama").attr("required", false);
        $("#PenghasilanInd").attr("required", false);
        $("#SumberDanaInd").attr("required", false);
        $("#MaksudTujuanInd").attr("required", false);
        $("#AlamatInd1").attr("required", false);
        $("#KodeKotaInd1").attr("required", false);
        //$("#KodePosInd1").attr("required", false);
        //$("#AlamatInd2").attr("required", false);
        //$("#KodeKotaInd2").attr("required", false);
        //$("#KodePosInd2").attr("required", false);
        //$("#SpouseName").attr("required", false);
        //$("#MotherMaidenName").attr("required", false);
        //$("#NatureOfBusiness").attr("required", false);
        //$("#Politis").attr("required", false);
        //$("#JumlahIdentitasInd").attr("required", false);
        $("#IdentitasInd1").attr("required", false);
        $("#NoIdentitasInd1").attr("required", false);
        //$("#RegistrationDateIdentitasInd1").attr("required", false);
        //$("#ExpiredDateIdentitasInd1").attr("required", false);
        $("#NamaPerusahaan").attr("required", false);
        $("#Domisili").attr("required", false);
        $("#Tipe").attr("required", false);
        //$("#TipeOjk").attr("required", false);
        $("#Karakteristik").attr("required", false);
        $("#NoSKD").attr("required", false);
        $("#PenghasilanInstitusi").attr("required", false);
        $("#SumberDanaInstitusi").attr("required", false);
        $("#MaksudTujuanInstitusi").attr("required", false);
        $("#AlamatPerusahaan").attr("required", false);
        $("#KodeKotaIns").attr("required", false);
        //$("#KodePosIns").attr("required", false);
        //$("#NoSKD").attr("required", false);
        //$("#RegistrationNPWP").attr("required", false);
        $("#TanggalBerdiri").attr("required", false);
        $("#LokasiBerdiri").attr("required", false);
        //$("#NomorAnggaran").attr("required", false);
        //$("#NomorSIUP").attr("required", false);
        //$("#TeleponBisnis").attr("required", false);
        //$("#AssetFor1Year").attr("required", false);
        //$("#AssetFor2Year").attr("required", false);
        //$("#AssetFor3Year").attr("required", false);
        //$("#OperatingProfitFor1Year").attr("required", false);
        //$("#OperatingProfitFor2Year").attr("required", false);
        //$("#OperatingProfitFor3Year").attr("required", false);
        //$("#JumlahPejabat").attr("required", false);
        $("#NamaDepanIns1").attr("required", false);
        //$("#Jabatan1").attr("required", false);
        //$("#JumlahIdentitasIns1").attr("required", false);
        $("#IdentitasIns11").attr("required", false);
        $("#NoIdentitasIns11").attr("required", false);
        //$("#RegistrationDateIdentitasIns11").attr("required", false);
        //$("#ExpiredDateIdentitasIns11").attr("required", false);

    }


    //INIT LIST

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

    $("#btnListKodeKotaInd1").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#KodeKotaInd1";
        htmlCityDesc = "#KodeKotaInd1Desc";
    });
    $("#btnClearListKodeKotaInd1").click(function () {
        $("#KodeKotaInd1").val("");
        $("#KodeKotaInd1Desc").val("");
    });

    $("#btnListKodeKotaInd2").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#KodeKotaInd2";
        htmlCityDesc = "#KodeKotaInd2Desc";
    });
    $("#btnClearListKodeKotaInd2").click(function () {
        $("#KodeKotaInd2").val("");
        $("#KodeKotaInd2Desc").val("");
    });

    $("#btnListOtherKodeKotaInd1").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#OtherKodeKotaInd1";
        htmlCityDesc = "#OtherKodeKotaInd1Desc";
    });
    $("#btnClearListOtherKodeKotaInd1").click(function () {
        $("#OtherKodeKotaInd1").val("");
        $("#OtherKodeKotaInd1Desc").val("");
    });

    $("#btnListOtherKodeKotaInd2").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#OtherKodeKotaInd2";
        htmlCityDesc = "#OtherKodeKotaInd2Desc";
    });
    $("#btnClearListOtherKodeKotaInd2").click(function () {
        $("#OtherKodeKotaInd2").val("");
        $("#OtherKodeKotaInd2Desc").val("");
    });

    $("#btnListOtherKodeKotaInd3").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#OtherKodeKotaInd3";
        htmlCityDesc = "#OtherKodeKotaInd3Desc";
    });
    $("#btnClearListOtherKodeKotaInd3").click(function () {
        $("#OtherKodeKotaInd3").val("");
        $("#OtherKodeKotaInd3Desc").val("");
    });

    $("#btnListKodeKotaIns").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#KodeKotaIns";
        htmlCityDesc = "#KodeKotaInsDesc";
    });
    $("#btnClearListKodeKotaIns").click(function () {
        $("#KodeKotaIns").val("");
        $("#KodeKotaInsDesc").val("");
    });

    $("#btnCompanyCityName").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCityPK = "#CompanyCityName";
        htmlCityDesc = "#CompanyCityNameDesc";
    });

    function initListCity() {
        var dsListCity = getDataSourceListCity();
        $("#gridListCity").kendoGrid({
            dataSource: dsListCity,
            height: "90%",
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

    function onWinListCityClose() {
        $("#gridListCity").empty();
    }

    function ListCitySelect(e) {
        var grid = $("#gridListCity").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCityDesc).val(dataItemX.DescOne);
        $(htmlCityPK).val(dataItemX.Code);
        WinListCity.close();

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

    $("#btnListNegara").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#Negara";
        htmlCountryDesc = "#NegaraDesc";
    });
    $("#btnClearListNegara").click(function () {
        $("#Negara").val("");
        $("#NegaraDesc").val("");
    });

    $("#btnListCountryOfBirth").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#CountryOfBirth";
        htmlCountryDesc = "#CountryOfBirthDesc";
    });
    $("#btnClearListCountryOfBirth").click(function () {
        $("#CountryOfBirth").val("");
        $("#CountryOfBirthDesc").val("");
    });

    $("#btnCountryofCorrespondence").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#CountryofCorrespondence";
        htmlCountryDesc = "#CountryofCorrespondenceDesc";
    });
    $("#btnClearCountryofCorrespondence").click(function () {
        $("#CountryofCorrespondence").val("");
        $("#CountryofCorrespondenceDesc").val("");
    });

    $("#btnCountryofDomicile").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#CountryofDomicile";
        htmlCountryDesc = "#CountryofDomicileDesc";
    });
    $("#btnClearCountryofDomicile").click(function () {
        $("#CountryofDomicile").val("");
        $("#CountryofDomicileDesc").val("");
    });

    $("#btnListOtherNegaraInd1").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#OtherNegaraInd1";
        htmlCountryDesc = "#OtherNegaraInd1Desc";
    });
    $("#btnClearListOtherNegaraInd1").click(function () {
        $("#OtherNegaraInd1").val("");
        $("#OtherNegaraInd1Desc").val("");
    });

    $("#btnListOtherNegaraInd2").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#OtherNegaraInd2";
        htmlCountryDesc = "#OtherNegaraInd2Desc";
    });
    $("#btnClearListOtherNegaraInd2").click(function () {
        $("#OtherNegaraInd2").val("");
        $("#OtherNegaraInd2Desc").val("");
    });

    $("#btnListOtherNegaraInd3").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#OtherNegaraInd3";
        htmlCountryDesc = "#OtherNegaraInd3Desc";
    });
    $("#btnClearListOtherNegaraInd3").click(function () {
        $("#OtherNegaraInd3").val("");
        $("#OtherNegaraInd3Desc").val("");
    });

    $("#btnListTINIssuanceCountry").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#TINIssuanceCountry";
        htmlCountryDesc = "#TINIssuanceCountryDesc";
    });
    $("#btnClearListTINIssuanceCountry").click(function () {
        $("#TINIssuanceCountry").val("");
        $("#TINIssuanceCountryDesc").val("");
    });

    $("#btnListBankCountry1").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
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
        initListCountry();
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
        initListCountry();
        htmlCountry = "#BankCountry3";
        htmlCountryDesc = "#BankCountry3Desc";
    });
    $("#btnClearListBankCountry3").click(function () {
        $("#BankCountry3").val("");
        $("#BankCountry3Desc").val("");
    });

    $("#btnCountryofEstablishment").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#CountryofEstablishment";
        htmlCountryDesc = "#CountryofEstablishmentDesc";
    });

    $("#btnCountryofCompany").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#CountryofCompany";
        htmlCountryDesc = "#CountryofCompanyDesc";
    });

    function initListCountry() {
        var dsListCountry = getDataSourceListCountry();
        $("#gridListCountry").empty();
        $("#gridListCountry").kendoGrid({
            dataSource: dsListCountry,
            height: "90%",
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
               { command: { text: "Select", click: ListCountrySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "Country", width: 200 }
            ]
        });
    }

    function onWinListCountryClose() {
        $("#gridListCountry").empty();
    }

    function ListCountrySelect(e) {
        var grid = $("#gridListCountry").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCountryDesc).val(dataItemX.DescOne);
        $(htmlCountry).val(dataItemX.Code);
        WinListCountry.close();
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

    $("#btnListPropinsi").click(function () {
        WinListProvince.center();
        WinListProvince.open();
        initListProvince();
        htmlProvince = "#Propinsi";
        htmlProvinceDesc = "#PropinsiDesc";
    });
    $("#btnClearListPropinsi").click(function () {
        $("#Propinsi").val("");
        $("#PropinsiDesc").val("");
    });

    $("#btnListOtherPropinsiInd1").click(function () {
        WinListProvince.center();
        WinListProvince.open();
        initListProvince();
        htmlProvince = "#OtherPropinsiInd1";
        htmlProvinceDesc = "#OtherPropinsiInd1Desc";
    });
    $("#btnClearListOtherPropinsiInd1").click(function () {
        $("#OtherPropinsiInd1").val("");
        $("#OtherPropinsiInd1Desc").val("");
    });

    $("#btnListOtherPropinsiInd2").click(function () {
        WinListProvince.center();
        WinListProvince.open();
        initListProvince();
        htmlProvince = "#OtherPropinsiInd2";
        htmlProvinceDesc = "#OtherPropinsiInd2Desc";
    });
    $("#btnClearListOtherPropinsiInd2").click(function () {
        $("#OtherPropinsiInd2").val("");
        $("#OtherPropinsiInd2Desc").val("");
    });

    $("#btnListOtherPropinsiInd3").click(function () {
        WinListProvince.center();
        WinListProvince.open();
        initListProvince();
        htmlProvince = "#OtherPropinsiInd3";
        htmlProvinceDesc = "#OtherPropinsiInd3Desc";
    });
    $("#btnClearListOtherPropinsiInd3").click(function () {
        $("#OtherPropinsiInd3").val("");
        $("#OtherPropinsiInd3Desc").val("");
    });


    function initListProvince() {
        var dsListProvince = getDataSourceListProvince();
        $("#gridListProvince").kendoGrid({
            dataSource: dsListProvince,
            height: "90%",
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

    function onWinListProvinceClose() {
        $("#gridListProvince").empty();
    }

    function ListProvinceSelect(e) {
        var grid = $("#gridListProvince").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlProvinceDesc).val(dataItemX.DescOne);
        $(htmlProvince).val(dataItemX.Code);
        WinListProvince.close();

    }


    function getDataSourceListNationality() {
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

    $("#btnListNationality").click(function () {
        WinListNationality.center();
        WinListNationality.open();
        initListNationality();
        htmlNationality = "#Nationality";
        htmlNationalityDesc = "#NationalityDesc";

    });
    $("#btnClearListNationality").click(function () {
        $("#Nationality").val("");
        $("#NationalityDesc").val("");
    });

    function initListNationality() {
        var dsListNationality = getDataSourceListNationality();
        $("#gridListNationality").empty();
        $("#gridListNationality").kendoGrid({
            dataSource: dsListNationality,
            height: "90%",
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
               { command: { text: "Select", click: ListNationalitySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "Nationality", width: 200 }
            ]
        });
    }

    function onWinListNationalityClose() {
        $("#gridListNationality").empty();
    }

    function ListNationalitySelect(e) {
        var grid = $("#gridListNationality").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityDesc).val(dataItemX.DescOne);
        $(htmlNationality).val(dataItemX.Code);
        WinListNationality.close();

    }



    function initListSpouseNationality() {
        var dsListSpouseNationality = getDataSourceListNationality();
        $("#gridListSpouseNationality").kendoGrid({
            dataSource: dsListSpouseNationality,
            height: "90%",
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
               { command: { text: "Select", click: ListSpouseNationalitySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "SpouseNationality", width: 200 }
            ]
        });
    }

    function onWinListSpouseNationalityClose() {
        $("#gridListSpouseNationality").empty();
    }

    function ListSpouseNationalitySelect(e) {
        var grid = $("#gridListSpouseNationality").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlSpouseNationalityDesc).val(dataItemX.DescOne);
        $(htmlSpouseNationality).val(dataItemX.Code);
        WinListSpouseNationality.close();

    }

    $("#BtnGenerateARIAText").click(function () {
        showWinGenerateARIAText();
    });

    function showWinGenerateARIAText(e) {

        $("#ParamCategory").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Individual", value: 1 },
                { text: "Institution", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            index: 0

        });

        WinGenerateARIAText.center();
        WinGenerateARIAText.open();

    }

    function validateDataGenerateARIAText() {


        return 1;

    }


    function initGridApprove(_fundClientPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetDataByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                setFundClient(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }


    function initGridHistory(_fundClientPK, _historyPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetDataByFundClientPKandHistoryPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK + "/" + _historyPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                setFundClient(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function initGridPending(_fundClientPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetDataByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                setFundClient(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    //$("#BtnGenerateSInvest").click(function () {
    //    resetNotification();

    //    alertify.confirm("Are you sure want to Generate S-Invest ?", function (e) {
    //        if (e) {

    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/FundClient/GenerateSInvest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#paramClientCategory").val(),
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    var newwindow = window.open(data, '_blank');
    //                    //window.location = data
    //                },
    //                error: function (data) {
    //                    alertify.error(data.responseText);
    //                }

    //            });
    //        }
    //    });
    //});

    //$("#BtnGenerateSInvestBankAccount").click(function () {
    //    resetNotification();

    //    alertify.confirm("Are you sure want to Generate S-Invest Bank Account ?", function (e) {
    //        if (e) {

    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/FundClient/GenerateSInvest_BankAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    var newwindow = window.open(data, '_blank');
    //                    //window.location = data
    //                },
    //                error: function (data) {
    //                    alertify.error(data.responseText);
    //                }

    //            });
    //        }
    //    });
    //});

    //$("#BtnSuspendBySelected").click(function (e) {
    //    resetNotification();
    //    alertify.confirm("Are you sure want to Suspend by Selected Data ?", function (e) {
    //        if (e) {
    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/FundClient/SuspendBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    alertify.alert(data);
    //                    refresh();
    //                },
    //                error: function (data) {
    //                    alertify.error(data.responseText);
    //                }
    //            });

    //        }
    //    });
    //});

    $("#BtnUnSuspendBySelected").click(function (e) {

        alertify.confirm("Are you sure want to UnSuspend by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/UnSuspendBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.error(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnCopyClient").click(function () {

        if ($("#BitIsAfiliated").text() != 'FREE') {
            alertify.alert('Cannot create from afiliated client');
            return;
        }

        alertify.confirm("Are you sure want to create new afiliated client using this data?", "", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/FundClient_CreateAfiliatedClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert('Success create client Afiliated from client No: ' + data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.error(data.responseText);
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