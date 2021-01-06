$(document).ready(function () {
    document.title = 'FORM FundClientAffiliated';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;

    var WinListCity;
    var WinListBankVA;
    var htmlCityPK;
    var htmlCityDesc;
    var WinListProvince;
    var htmlProvince;
    var htmlProvinceDesc;
    var WinListCountry;
    var htmlCountry;
    var htmlCountryDesc;
    var WinListNationality;
    var WinListNationalityOfficer1;
    var WinListNationalityOfficer2;
    var WinListNationalityOfficer3;
    var WinListNationalityOfficer4;
    var gridListBankVA;
    var WinListSpouseNationality;
    var htmlNationality;
    var htmlNationalityDesc;
    var htmlNationalityOfficer1;
    var htmlNationalityOfficer1Desc;
    var htmlNationalityOfficer2;
    var htmlNationalityOfficer2Desc;
    var htmlNationalityOfficer3;
    var htmlNationalityOfficer3Desc;
    var htmlNationalityOfficer4;
    var htmlNationalityOfficer4Desc;
    var BankVA;
    var BankVADesc;
    var htmlSpouseNationality;
    var htmlSpouseNationalityDesc;
    var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
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
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
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

        $("#DatePengkinianData").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#RegistrationDateIdentitasInd1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#ExpiredDateIdentitasInd1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        $("#RegistrationNPWP").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        $("#RegistrationDateIdentitasInd2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        $("#ExpiredDateIdentitasInd2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        $("#TanggalLahir").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        $("#SpouseDateOfBirth").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });

        function OnChangeParamDate() {
            var _FilterDate = Date.parse($("#ParamDate").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_FilterDate) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                return;
            }

        }


        win = $("#WinFundClientAffiliated").kendoWindow({
            height: "95%",
            title: "Fund Client Affiliated Detail",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinListCity = $("#WinListCity").kendoWindow({
            height: "420px",
            title: "City List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListProvince = $("#WinListProvince").kendoWindow({
            height: "420px",
            title: "Province List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListCountry = $("#WinListCountry").kendoWindow({
            height: "420px",
            title: "Country List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }

        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinFundClientAffiliated").kendoValidator().data("kendoValidator");


    function validateData() {

        if ($("#Name").val().length > 200) {
            alertify.alert("Validation not Pass, char more than 200 for Internal Name");
            return 0;
        }
        if ($("#SID").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for SID");
            return 0;
        }
        if ($("#NPWP").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for NPWP");
            return 0;
        }

        if ($("#NamaDepanInd").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for First Name");
            return 0;
        }
        if ($("#NamaTengahInd").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Middle Name");
            return 0;
        }
        if ($("#NamaBelakangInd").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Last Name");
            return 0;
        }
        if ($("#TempatLahir").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Birth Place");
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
        if ($("#OtherAlamatInd1").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Identity Address (1)");
            return 0;
        }
        if ($("#AlamatInd2").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Correspondence Address (2)");
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
        if ($("#NatureOfBusinessLainnya").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for N.O.B (Text)");
            return 0;
        }
        if ($("#SpouseName").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Spouse Name");
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
        if ($("#TeleponKantor").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Phone");
            return 0;
        }
        if ($("#PolitisLainnya").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for P.E (Text)");
            return 0;
        }

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
        initListCity();
        initListProvince();
        initListCountry();
        $(GlobTabStrip.items()[0]).show();
        $(GlobTabStrip.items()[1]).show();
        $(GlobTabStrip.items()[2]).show();
        $(GlobTabStrip.items()[3]).show();

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
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").show();
                $("#BtnOldData").hide();
                $("#BtnUpdate").hide();
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

            $("#NoIdentitasInd1").val(dataItemX.NoIdentitasInd1);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#SID").val(dataItemX.SID);
            $("#NPWP").val(dataItemX.NPWP);
            $("#BitShareAbleToGroup").prop('checked', dataItemX.BitShareAbleToGroup);
            $("#AssetOwner").val(dataItemX.AssetOwner);
            $("#DatePengkinianData").val(dataItemX.DatePengkinianData);
            $("#NamaDepanInd").val(dataItemX.NamaDepanInd);
            $("#NamaTengahInd").val(dataItemX.NamaTengahInd);
            $("#NamaBelakangInd").val(dataItemX.NamaBelakangInd);
            $("#NoIdentitasInd1").val(dataItemX.NoIdentitasInd1);
            $("#RegistrationDateIdentitasInd1").val(dataItemX.RegistrationDateIdentitasInd1);
            $("#ExpiredDateIdentitasInd1").val(dataItemX.ExpiredDateIdentitasInd1);
            $("#OtherAlamatInd1").val(dataItemX.OtherAlamatInd1);
            $("#OtherPropinsiInd1").val(dataItemX.OtherPropinsiInd1);
            $("#OtherNegaraInd1").val(dataItemX.OtherNegaraInd1);
            $("#OtherKodePosInd1").val(dataItemX.OtherKodePosInd1);
            $("#IdentitasInd2").val(dataItemX.IdentitasInd2);
            $("#NoIdentitasInd2").val(dataItemX.NoIdentitasInd2);
            $("#RegistrationDateIdentitasInd2").val(dataItemX.RegistrationDateIdentitasInd2);
            $("#ExpiredDateIdentitasInd2").val(dataItemX.ExpiredDateIdentitasInd2);
            $("#AlamatInd2").val(dataItemX.AlamatInd2);
            $("#DomicileRT").val(dataItemX.DomicileRT);
            $("#DomicileRW").val(dataItemX.DomicileRW);
            $("#KodeKotaInd2").val(dataItemX.KodeKotaInd2);
            $("#KodeDomisiliPropinsi").val(dataItemX.KodeDomisiliPropinsi);
            $("#CountryofDomicile").val(dataItemX.CountryofDomicile);
            $("#KodePosInd2").val(dataItemX.KodePosInd2);
            $("#TempatLahir").val(dataItemX.TempatLahir);
            $("#TanggalLahir").val(dataItemX.TanggalLahir);
            $("#JenisKelamin").val(dataItemX.JenisKelamin);
            $("#Nationality").val(dataItemX.Nationality);
            $("#CountryOfBirth").val(dataItemX.CountryOfBirth);
            $("#Agama").val(dataItemX.Agama);
            $("#OtherAgama").val(dataItemX.OtherAgama);
            $("#Pendidikan").val(dataItemX.Pendidikan);
            $("#OtherPendidikan").val(dataItemX.OtherPendidikan);
            $("#MotherMaidenName").val(dataItemX.MotherMaidenName);
            $("#AhliWaris").val(dataItemX.AhliWaris);
            $("#HubunganAhliWaris").val(dataItemX.HubunganAhliWaris);
            $("#Pekerjaan").val(dataItemX.Pekerjaan);
            $("#OtherOccupation").val(dataItemX.OtherOccupation);
            $("#NatureOfBusiness").val(dataItemX.NatureOfBusiness);
            $("#NatureOfBusinessLainnya").val(dataItemX.NatureOfBusinessLainnya);
            $("#PenghasilanInd").val(dataItemX.PenghasilanInd);
            $("#SumberDanaInd").val(dataItemX.SumberDanaInd);
            $("#MaksudTujuanInd").val(dataItemX.MaksudTujuanInd);
            $("#StatusPerkawinan").val(dataItemX.StatusPerkawinan);
            $("#SpouseName").val(dataItemX.SpouseName);
            $("#SpouseDateOfBirth").val(dataItemX.SpouseDateOfBirth);
            $("#SpouseBirthPlace").val(dataItemX.SpouseBirthPlace);
            $("#SpouseOccupation").val(dataItemX.SpouseOccupation);
            $("#OtherSpouseOccupation").val(dataItemX.OtherSpouseOccupation);
            $("#SpouseNatureOfBusiness").val(dataItemX.SpouseNatureOfBusiness);
            $("#SpouseNatureOfBusinessOther").val(dataItemX.SpouseNatureOfBusinessOther);
            $("#SpouseIDNo").val(dataItemX.SpouseIDNo);
            $("#SpouseNationality").val(dataItemX.SpouseNationality);
            $("#SpouseAnnualIncome").val(dataItemX.SpouseAnnualIncome);
            $("#NamaKantor").val(dataItemX.NamaKantor);
            $("#EmployerLineOfBusiness").val(dataItemX.EmployerLineOfBusiness);
            $("#JabatanKantor").val(dataItemX.JabatanKantor);
            $("#TeleponKantor").val(dataItemX.TeleponKantor);
            $("#AlamatKantorInd").val(dataItemX.AlamatKantorInd);
            $("#KodeKotaKantorInd").val(dataItemX.KodeKotaKantorInd);
            $("#KodePropinsiKantorInd").val(dataItemX.KodePropinsiKantorInd);
            $("#KodeCountryofKantor").val(dataItemX.KodeCountryofKantor);
            $("#KodePosKantorInd").val(dataItemX.KodePosKantorInd);
            $("#Politis").val(dataItemX.Politis);
            $("#PolitisLainnya").val(dataItemX.PolitisLainnya);
            $("#PolitisRelation").val(dataItemX.PolitisRelation);
            $("#PolitisName").val(dataItemX.PolitisName);
            $("#PolitisFT").val(dataItemX.PolitisFT);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#EntryTime").val(dataItemX.EntryTime);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#UpdateTime").val(dataItemX.UpdateTime);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#ApprovedTime").val(dataItemX.ApprovedTime);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#VoidTime").val(dataItemX.VoidTime);
            $("#LastUpdate").val(dataItemX.LastUpdate);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            if (dataItemX.RegistrationNPWP == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || dataItemX.RegistrationNPWP == '1/1/1900 12:00:00 AM') {
                $("#RegistrationNPWP").data("kendoDatePicker").value("");
            } else {
                $("#RegistrationNPWP").data("kendoDatePicker").value(new Date(dataItemX.RegistrationNPWP));
            }

            if ($("#Agama").val(dataItemX.Agama) == 7) {
                $("#lblotherreligion").show();
            } else {
                $("#lblotherreligion").hide();
            }

            if ($("#Pendidikan").val(dataItemX.Pendidikan) == 8) {
                $("#lblotherEducation").show();
            }
            else {
                $("#lblotherEducation").hide();
            }

            if ($("#Pekerjaan").val(dataItemX.Pekerjaan) == 9) {
                $("#lbloccupation").show();
            } else {
                $("#lbloccupation").hide();
            }

            if ($("#SpouseOccupation").val(dataItemX.SpouseOccupation) == 9) {
                $("#lblOtherSpouseOccupation").show();
            } else {
                $("#lblOtherSpouseOccupation").hide();
            }

        }

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
                    value: setCmbInvestorType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbInvestorType() {

            if (dataItemX.InvestorType == null) {
                return "";
            } else {
                if (dataItemX.InvestorType == 0) {
                    return "";
                } else {
                    return dataItemX.InvestorType;
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
            if (dataItemX.InvestorsRiskProfile == null) {
                return "";
            } else {
                if (dataItemX.InvestorsRiskProfile == 0) {
                    return "";
                } else {
                    return dataItemX.InvestorsRiskProfile;
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
            if (dataItemX.KYCRiskProfile == null) {
                return "";
            } else {
                if (dataItemX.KYCRiskProfile == 0) {
                    return "";
                } else {
                    return dataItemX.KYCRiskProfile;
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

            if ($("#AssetOwner").val() == 2) {
                $("#LblAssetOwner").show();
            } else {
                $("#LblAssetOwner").hide();
                $("#BeneficialName").val("");
                $("#BeneficialAddress").val("");
                $("#BeneficialIdentity").val("");
                $("#BeneficialWork").val("");
                $("#BeneficialRelation").val("");
                $("#BeneficialHomeNo").val("");
                $("#BeneficialPhoneNumber").val("");
                $("#BeneficialNPWP").val("");
            }
        }
        function setCmbAssetOwner() {
            if (dataItemX.AssetOwner == null) {
                return "";
            }

            else {
                if (dataItemX.AssetOwner == 0) {
                    return "";
                }
                else if ($("#AssetOwner").val() == 2) {
                    $("#LblAssetOwner").show();
                }
                else if ($("#AssetOwner").val() == 1) {
                    $("#LblAssetOwner").hide();
                }

                else {
                    return dataItemX.AssetOwner;
                }


            }


        }

        $("#BitShareAbleToGroup").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitShareAbleToGroup,
            value: setCmbBitShareAbleToGroup()
        });
        function OnChangeBitShareAbleToGroup() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitShareAbleToGroup() {
            if (dataItemX.BitShareAbleToGroup == null) {
                return false;
            } else {
                return dataItemX.BitShareAbleToGroup;
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

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setCmbIdentitasInd1() {
            if (dataItemX.IdentitasInd1 == null) {
                return "";
            } else {
                if (dataItemX.IdentitasInd1 == 0) {
                    return "";
                } else {
                    return dataItemX.IdentitasInd1;
                }
            }
        }
        function setCmbIdentitasInd2() {
            if (dataItemX.IdentitasInd2 == null) {
                return "";
            } else {
                if (dataItemX.IdentitasInd2 == 0) {
                    return "";
                } else {
                    return dataItemX.IdentitasInd2;
                }
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

        function checkIdentitySeumurHidup() {

            if ($("#IdentitasInd1").val() == 7) {
                $("#ExpiredDateIdentitasInd1").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasInd1").val() != 7) {
                $("#ExpiredDateIdentitasInd1").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasInd2").val() == 7) {
                $("#ExpiredDateIdentitasInd2").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasInd2").val() != 7) {
                $("#ExpiredDateIdentitasInd2").data("kendoDatePicker").value("")
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
            if (dataItemX.JenisKelamin == null) {
                return "";
            } else {
                if (dataItemX.JenisKelamin == 0) {
                    return "";
                } else {
                    return dataItemX.JenisKelamin;
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
            if (this.value() == 7) {
                $("#lblotherreligion").show();
                $("#OtherAgama").attr("required", true);
            } else {
                $("#lblotherreligion").hide();
                $("#OtherAgama").attr("required", false);
            }

        }
        function setCmbAgama() {
            if (dataItemX.Agama == 7) {
                $("#lblotherreligion").show();
                $("#OtherAgama").attr("required", true);
            } else {
                $("#lblotherreligion").hide();
                $("#OtherAgama").attr("required", false);
            }
            if (dataItemX.Agama == null) {
                return "";
            } else {
                if (dataItemX.Agama == 0) {
                    return "";
                }
                else if (dataItemX.Agama == 7) {
                    $("#lblotherreligion").show();
                }
                else {
                    return dataItemX.Agama;
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
            if (this.value() == 8) {
                $("#lblotherEducation").show();
                $("#OtherPendidikan").attr("required", true);
            } else {
                $("#lblotherEducation").hide();
                $("#OtherPendidikan").attr("required", false);
            }

        }
        function setCmbPendidikan() {
            if (dataItemX.Pendidikan == 8) {
                $("#lblotherEducation").show();
                $("#OtherPendidikan").attr("required", true);
            } else {
                $("#lblotherEducation").hide();
                $("#OtherPendidikan").attr("required", false);
            }
            if (dataItemX.Pendidikan == null) {
                return "";
            } else {
                if (dataItemX.Pendidikan == 0) {
                    return "";
                }
                else if (dataItemX.Pendidikan == 8) {
                    $("#lblotherEducation").show();
                }
                else {
                    return dataItemX.Pendidikan;
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

        function onChangePekerjaan() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($('#Pekerjaan').val() == 3) {
                $("#lblNatureOfBusiness").show();
                $("#lbloccupation").hide();
                $("#OtherOccupation").attr("required", false);

            }
            else if ($('#Pekerjaan').val() == 1) {
                if (_GlobClientCode == "08") {
                    $("#KYCRiskProfile").data("kendoComboBox").value(3);
                }

                $("#LblPropinsi").show();
                $("#LblOfficeaddress").hide();
                $("#LblOfficeCity").hide();
                $("#LblOfficezipcode").hide();
                $("#LblOfficeProvince").hide();
                $("#LblOfficeCountry").hide();
                $("#LblOfficeName").hide();
                $("#LblOfficePosition").hide();


                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#lbloccupation").hide();
                $("#OtherOccupation").attr("required", false);
            }

            else if ($('#Pekerjaan').val() == 2) {
                if (_GlobClientCode == "08") {
                    $("#KYCRiskProfile").data("kendoComboBox").value(3);
                }

                $("#LblPropinsi").show();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();
                $("#LblOfficeaddress").hide();
                $("#LblOfficeCity").hide();
                $("#LblOfficezipcode").hide();
                $("#LblOfficeProvince").hide();
                $("#LblOfficeCountry").hide();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#lbloccupation").hide();
                $("#OtherOccupation").attr("required", false);
            }
            else if ($('#Pekerjaan').val() == 6) {
                $("#LblPropinsi").show();
                $("#LblOfficeaddress").hide();
                $("#LblOfficeCity").hide();
                $("#LblOfficezipcode").hide();
                $("#LblOfficeProvince").hide();
                $("#LblOfficeCountry").hide();
                $("#LblOfficeName").hide();
                $("#LblOfficePosition").hide();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#lbloccupation").hide();
                $("#OtherOccupation").attr("required", false);
            }
            else if ($('#Pekerjaan').val() == 9) {
                $("#lbloccupation").show();
                $("#OtherOccupation").attr("required", true);
            }

            else {
                $("#lblNatureOfBusiness").hide();
                $("#LblPropinsi").show();
                $("#LblOfficeaddress").show();
                $("#LblOfficeCity").show();
                $("#LblOfficezipcode").show();
                $("#LblOfficeProvince").show();
                $("#LblOfficeCountry").show();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();
                $("#LblOfficeName").show();
                $("#LblOfficePosition").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#lbloccupation").hide();
                $("#OtherOccupation").attr("required", false);
            }
        }

        function setCmbPekerjaan() {
            if (dataItemX.Pekerjaan == 3) {
                $("#lblNatureOfBusiness").show();
                $("#OtherOccupation").attr("required", false);

            }
            else if (dataItemX.Pekerjaan == 9) {
                $("#lbloccupation").show();
                $("#OtherOccupation").attr("required", true);

            }
            else if (dataItemX.Pekerjaan == 1) {
                $("#LblPropinsi").show();
                $("#LblOfficeaddress").hide();
                $("#LblOfficeCity").hide();
                $("#LblOfficezipcode").hide();
                $("#LblOfficeProvince").hide();
                $("#LblOfficeCountry").hide();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#OtherOccupation").attr("required", false);
            }

            else if (dataItemX.Pekerjaan == 2) {
                $("#LblPropinsi").show();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#OtherOccupation").attr("required", false);
            }
            else if (dataItemX.Pekerjaan == 6) {
                $("#LblPropinsi").show();
                $("#LblOfficeaddress").hide();
                $("#LblOfficeCity").hide();
                $("#LblOfficezipcode").hide();
                $("#LblOfficeProvince").hide();
                $("#LblOfficeCountry").hide();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#OtherOccupation").attr("required", false);
            }

            else {
                $("#lblNatureOfBusiness").hide();
                $("#LblPropinsi").show();
                $("#LblOfficeaddress").show();
                $("#LblOfficeCity").show();
                $("#LblOfficezipcode").show();
                $("#LblOfficeProvince").show();
                $("#LblOfficeCountry").show();
                $("#LblCorrespondenceRT").show();
                $("#LblCorrespondenceRW").show();
                $("#LblDomicileRT").show();

                $("#LblDomicileRW").show();
                $("#LblIdentity1RT").show();
                $("#LblIdentity1RW").show();
                $("#LblDomicileProvince").show();
                $("#OtherOccupation").attr("required", false);
            }
            if (dataItemX.Pekerjaan == null) {
                return "";
            } else {
                if (dataItemX.Pekerjaan == 0) {
                    return "";
                } else {
                    return dataItemX.Pekerjaan;
                }
            }
        }


        function onChangeSpouseOccupation() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#SpouseOccupation").val() == 9) {

                $("#lblOtherSpouseOccupation").show();
                $("#OtherSpouseOccupation").attr("required", true);
            }
            else {

                $("#lblOtherSpouseOccupation").hide();
                $("#OtherSpouseOccupation").attr("required", false);
            }
        }

        function setCmbSpouseOccupation() {

            if ($("#SpouseOccupation").val() == 9) {

                $("#lblOtherSpouseOccupation").show();
                $("#OtherSpouseOccupation").attr("required", true);
            }
            else {

                $("#lblOtherSpouseOccupation").hide();
                $("#OtherSpouseOccupation").attr("required", false);
            }

            if (dataItemX.SpouseOccupation == null) {
                return "";
            }
            else {
                if (dataItemX.SpouseOccupation == 0) {
                    return "";
                } else {
                    return dataItemX.SpouseOccupation;
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

                $("#SpouseNatureOfBusiness").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeNatureOfBusiness,
                    value: setCmbSpouseNatureOfBusiness()
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
            if (dataItemX.NatureOfBusiness == null) {
                return "";
            } else {
                if (dataItemX.NatureOfBusiness == 0) {
                    return "";
                } else {
                    return dataItemX.NatureOfBusiness;
                }
            }
        }

        function setCmbSpouseNatureOfBusiness() {
            if (dataItemX.SpouseNatureOfBusiness == null) {
                return "";
            } else {
                if (dataItemX.SpouseNatureOfBusiness == 0) {
                    return "";
                } else {
                    return dataItemX.SpouseNatureOfBusiness;
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

                $("#SpouseAnnualIncome").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePenghasilanInd,
                    value: setCmbSpouseAnnualIncome()
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
            if (dataItemX.PenghasilanInd == null) {
                return "";
            } else {
                if (dataItemX.PenghasilanInd == 0) {
                    return "";
                } else {

                    return dataItemX.PenghasilanInd;
                }
            }
        }
        function setCmbSpouseAnnualIncome() {
            if (dataItemX.SpouseAnnualIncome == null) {
                return "";
            } else {
                if (dataItemX.SpouseAnnualIncome == 0) {
                    return "";
                } else {
                    return dataItemX.SpouseAnnualIncome;
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

            if (this.value() == 10) {
                $("#lblOtherSourceOfFunds").show();
                $("#OtherSourceOfFunds").attr("required", true);
            } else {
                $("#lblOtherSourceOfFunds").hide();
                $("#OtherSourceOfFunds").attr("required", false);
            }
        }
        function setCmbSumberDanaInd() {
            if (dataItemX.SumberDanaInd == 10) {
                $("#lblOtherSourceOfFunds").show();
                $("#OtherSourceOfFunds").attr("required", true);
            } else {
                $("#lblOtherSourceOfFunds").hide();
                $("#OtherSourceOfFunds").attr("required", false);
            }
            if (dataItemX.SumberDanaInd == null) {
                return "";
            } else {
                if (dataItemX.SumberDanaInd == 0) {
                    return "";
                }
                else if (dataItemX.SumberDanaInd == 10) {
                    $("#lblOtherSourceOfFunds").show();
                }
                else {
                    return dataItemX.SumberDanaInd;
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

            if (this.value() == 5) {
                $("#lblInvestmentObjectives").show();
                $("#OtherInvestmentObjectives").attr("required", true);
            } else {
                $("#lblInvestmentObjectives").hide();
                $("#OtherInvestmentObjectives").attr("required", false);
            }
        }
        function setCmbMaksudTujuanInd() {
            if (dataItemX.MaksudTujuanInd == 5) {
                $("#lblInvestmentObjectives").show();
                $("#OtherInvestmentObjectives").attr("required", true);
            } else {
                $("#lblInvestmentObjectives").hide();
                $("#OtherInvestmentObjectives").attr("required", false);
            }
            if (dataItemX.MaksudTujuanInd == null) {
                return "";
            } else {
                if (dataItemX.MaksudTujuanInd == 0) {
                    return "";
                }
                else if (dataItemX.MaksudTujuanInd == 5) {
                    $("#lblInvestmentObjectives").show();
                }
                else {
                    return dataItemX.MaksudTujuanInd;
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

            if ($("#StatusPerkawinan").val() == 2) {
                $("#lblSpouse").show();
            }
            else {
                $("#lblSpouse").hide();
            }
        }

        function setCmbStatusPerkawinan() {
            if (dataItemX.StatusPerkawinan == 2) {
                $("#lblSpouse").show();
            }
            else {
                $("#lblSpouse").hide();
            }
            if (dataItemX.StatusPerkawinan == null) {
                return "";
            } else {
                if (dataItemX.StatusPerkawinan == 0) {
                    return "";
                } else {
                    return dataItemX.StatusPerkawinan;
                }
            }

        }

        //combo box EmployerLineOfBusiness
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/LineBusiness",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#EmployerLineOfBusiness").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeEmployerLineOfBusiness,
                    value: setCmbEmployerLineOfBusiness()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeEmployerLineOfBusiness() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbEmployerLineOfBusiness() {
            if (dataItemX.EmployerLineOfBusiness == null) {
                return "";
            } else {
                if (dataItemX.EmployerLineOfBusiness == 0) {
                    return "";
                } else {
                    return dataItemX.EmployerLineOfBusiness;
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
            if (dataItemX.Politis == null) {
                return "";
            } else {
                if (dataItemX.Politis == 0) {
                    return "";
                } else {
                    return dataItemX.Politis;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PolitisRelation",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PolitisRelation").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePolitisRelation,
                    value: setCmbPolitisRelation()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePolitisRelation() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPolitisRelation() {
            if (dataItemX.PolitisRelation == null) {
                return "";
            } else {
                if (dataItemX.PolitisRelation == 0) {
                    return "";
                } else {
                    return dataItemX.PolitisRelation;
                }
            }
        }

        win.center();
        win.open();
    }

    $("#btnListOtherKodeKotaInd1").click(function () {
        WinListCity.center();
        WinListCity.open();

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


        htmlCityPK = "#OtherKodeKotaInd1";
        htmlCityDesc = "#OtherKodeKotaInd1Desc";
    });
    $("#btnClearListOtherKodeKotaInd1").click(function () {
        $("#OtherKodeKotaInd1").val("");
        $("#OtherKodeKotaInd1Desc").val("");
    });

    $("#btnListOtherPropinsiInd1").click(function () {
        WinListProvince.center();
        WinListProvince.open();


        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });

        htmlProvince = "#OtherPropinsiInd1";
        htmlProvinceDesc = "#OtherPropinsiInd1Desc";
    });
    $("#btnClearListOtherPropinsiInd1").click(function () {
        $("#OtherPropinsiInd1").val("");
        $("#OtherPropinsiInd1Desc").val("");
    });

    $("#btnListDomicileProvince").click(function () {
        WinListProvince.center();
        WinListProvince.open();

        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });
        htmlProvince = "#KodeDomisiliPropinsi";
        htmlProvinceDesc = "#KodeDomisiliPropinsiDesc";
    });

    $("#btnClearDomicileProvince").click(function () {
        $("#KodeDomisiliPropinsi").val("");
        $("#KodeDomisiliPropinsiDesc").val("");
    });

    $("#btnListOtherNegaraInd1").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#OtherNegaraInd1";
        htmlCountryDesc = "#OtherNegaraInd1Desc";
    });
    $("#btnClearListOtherNegaraInd1").click(function () {
        $("#OtherNegaraInd1").val("");
        $("#OtherNegaraInd1Desc").val("");
    });

    $("#btnCountryofDomicile").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#CountryofDomicile";
        htmlCountryDesc = "#CountryofDomicileDesc";
    });
    $("#btnClearCountryofDomicile").click(function () {
        $("#CountryofDomicile").val("");
        $("#CountryofDomicileDesc").val("");
    });


    $("#btnListKodeKotaInd2").click(function () {
        WinListCity.center();
        WinListCity.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


        htmlCityPK = "#KodeKotaInd2";
        htmlCityDesc = "#KodeKotaInd2Desc";
    });
    $("#btnClearListKodeKotaInd2").click(function () {
        $("#KodeKotaInd2").val("");
        $("#KodeKotaInd2Desc").val("");
    });

    $("#btnListNationality").click(function () {
        initListNationality();
        WinListNationality.center();
        WinListNationality.open();

        var element = $(this);
        var _obj = element.offset();
        WinListNationality.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationality = "#Nationality";
        htmlNationalityDesc = "#NationalityDesc";

    });
    $("#btnClearListNationality").click(function () {
        $("#Nationality").val("");
        $("#NationalityDesc").val("");
    });

    $("#btnListCountryOfBirth").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#CountryOfBirth";
        htmlCountryDesc = "#CountryOfBirthDesc";
    });

    $("#btnClearListCountryOfBirth").click(function () {
        $("#CountryOfBirth").val("");
        $("#CountryOfBirthDesc").val("");
    });

    $("#btnListSpouseNationality").click(function () {
        WinListNationality.center();
        WinListNationality.open();
        var element = $(this);
        var _obj = element.offset();
        WinListNationality.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationality = "#SpouseNationality";
        htmlNationalityDesc = "#SpouseNationalityDesc";

    });
    $("#btnClearListSpouseNationality").click(function () {
        $("#SpouseNationality").val("");
        $("#SpouseNationalityDesc").val("");
    });

    $("#btnListOfficeCity").click(function () {
        WinListCity.center();
        WinListCity.open();

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


        htmlCityPK = "#KodeKotaKantorInd";
        htmlCityDesc = "#KodeKotaKantorIndDesc";
    });
    $("#btnClearOfficeCity").click(function () {
        $("#KodeKotaKantorInd").val("");
        $("#KodeKotaKantorIndDesc").val("");
    });

    $("#btnListOfficeProvince").click(function () {
        WinListProvince.center();
        WinListProvince.open();

        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });
        htmlProvince = "#KodePropinsiKantorInd";
        htmlProvinceDesc = "#KodePropinsiKantorIndDesc";
    });

    $("#btnClearOfficeProvince").click(function () {
        $("#KodePropinsiKantorInd").val("");
        $("#KodePropinsiKantorIndDesc").val("");
    });

    $("#btnListOfficeCountry").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#KodeCountryofKantor";
        htmlCountryDesc = "#KodeCountryofKantorDesc";
    });

    $("#btnClearOfficeCountry").click(function () {
        $("#KodeCountryofKantor").val("");
        $("#KodeCountryofKantorDesc").val("");
    });


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


    function ListCitySelect(e) {
        e.preventDefault();
        var grid = $("#gridListCity").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCityDesc).val(dataItemX.DescOne);
        $(htmlCityPK).val(dataItemX.Code);

        WinListCity.close();

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

    function ListProvinceSelect(e) {
        e.preventDefault();
        var grid = $("#gridListProvince").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlProvinceDesc).val(dataItemX.DescOne);
        $(htmlProvince).val(dataItemX.Code);
        WinListProvince.close();

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
                { command: { text: "Select", click: ListCountrySelect }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "Country", width: 200 }
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

    function initListNationality() {
        var dsListNationality = getDataSourceListCountry();
        $("#gridListNationality").empty();
        $("#gridListNationality").kendoGrid({
            dataSource: dsListNationality,
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
                { command: { text: "Select", click: ListNationalitySelect }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "Nationality", width: 200 }
            ]
        });
    }

    function ListNationalitySelect(e) {
        e.preventDefault();
        var grid = $("#gridListNationality").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityDesc).val(dataItemX.DescOne);
        $(htmlNationality).val(dataItemX.Code);
        WinListNationality.close();

    }


    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundClientAffiliatedPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Type").val("");
        $("#FundClientAffiliated").val("");
        $("#NoRek").val("");
        $("#Phone").val("");
        $("#Fax").val("");
        $("#Email").val("");
        $("#Address").val("");
        $("#TaxID").val("");
        $("#BankInformation").val("");
        $("#BeneficiaryName").val("");
        $("#Description").val("");
        $("#JoinDate").val("");
        $("#Groups").val("");
        $("#Levels").val("");
        $("#ParentPK").val("");
        $("#ParentID").val("");
        $("#Depth").val("");
        $("#DepartmentPK").val("");
        $("#CompanyPositionSchemePK").val("");
        $("#BitisFundClientAffiliatedBank").prop('checked', false);
        $("#BitIsFundClientAffiliatedCSR").prop('checked', false);
        $("#NPWPNo").val("");
        $("#BitPPH23").prop('checked', false);
        $("#BitPPH21").prop('checked', false);
        $("#MMethod").val("");
        $("#SharingCalculation").val("");
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
        $("#BtnUpdate").hide();
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
                            FundClientAffiliatedPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            FundClientAffiliated: { type: "number" },
                            NoRek: { type: "string" },
                            Phone: { type: "string" },
                            Fax: { type: "string" },
                            Email: { type: "string" },
                            Address: { type: "string" },
                            TaxID: { type: "string" },
                            BankInformation: { type: "string" },
                            BeneficiaryName: { type: "string" },
                            Description: { type: "string" },
                            JoinDate: { type: "date" },
                            Groups: { type: "boolean" },
                            Levels: { type: "number" },
                            ParentPK: { type: "number" },
                            ParentID: { type: "string" },
                            ParentName: { type: "string" },
                            Depth: { type: "number" },
                            ParentPK1: { type: "number" },
                            ParentPK2: { type: "number" },
                            ParentPK3: { type: "number" },
                            ParentPK4: { type: "number" },
                            ParentPK5: { type: "number" },
                            ParentPK6: { type: "number" },
                            ParentPK7: { type: "number" },
                            ParentPK8: { type: "number" },
                            ParentPK9: { type: "number" },
                            DepartmentPK: { type: "number" },
                            DepartmentID: { type: "string" },
                            CompanyPositionSchemePK: { type: "number" },
                            CompanyPositionSchemeID: { type: "string" },
                            BitisFundClientAffiliatedBank: { type: "boolean" },
                            BitIsFundClientAffiliatedCSR: { type: "boolean" },
                            NPWPNo: { type: "string" },
                            BitPPH23: { type: "boolean" },
                            BitPPH21: { type: "boolean" },
                            MMethod: { type: "number" },
                            MMethodDesc: { type: "string" },
                            SharingCalculation: { type: "number" },
                            SharingCalculationDesc: { type: "string" },
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
            var gridApproved = $("#gridFundClientAffiliatedApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundClientAffiliatedPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundClientAffiliatedHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundClientAffiliatedApprovedURL = window.location.origin + "/Radsoft/FundClientAffiliated/GetDataFromFundClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FundClientAffiliatedApprovedURL);

        $("#gridFundClientAffiliatedApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Data Master"
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
                { field: "NoIdentitasInd1", title: "Identity Number (1)", width: 200 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Name", title: "Name", width: 300 },
               
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
        $("#TabFundClientAffiliated").kendoTabStrip({
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
                        var FundClientAffiliatedPendingURL = window.location.origin + "/Radsoft/FundClientAffiliated/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FundClientAffiliatedPendingURL);

                        $("#gridFundClientAffiliatedPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Client Data Master"
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
                                { field: "NoIdentitasInd1", title: "Identity Number (1)", width: 200 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Name", title: "Name", width: 300 },

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

                        var FundClientAffiliatedHistoryURL = window.location.origin + "/Radsoft/FundClientAffiliated/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FundClientAffiliatedHistoryURL);

                        $("#gridFundClientAffiliatedHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Client Data Master"
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
                                { field: "NoIdentitasInd1", title: "Identity Number (1)", width: 200 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Name", title: "Name", width: 300 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

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
        var grid = $("#gridFundClientAffiliatedHistory").data("kendoGrid");
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


    function clearDataFundClientAffiliated() {
        $("#MIAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MIPercent").data("kendoNumericTextBox").value("");
        $("#DateAmortize").val("");
        $("#RangeTo").attr('readonly', false);

    }




    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Update data?", function (e) {
                if (e) {

                    var FundClientAffiliated = {
                        IdentitasInd1: $('#IdentitasInd1').val(),
                        Status: $('#Status').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        InvestorType: $('#InvestorType').val(),
                        Name: $('#Name').val(),
                        SID: $('#SID').val(),
                        NPWP: $('#NPWP').val(),
                        RegistrationNPWP: $('#RegistrationNPWP').val(),
                        InvestorsRiskProfile: $('#InvestorsRiskProfile').val(),
                        KYCRiskProfile: $('#KYCRiskProfile').val(),
                        BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                        AssetOwner: $('#AssetOwner').val(),
                        DatePengkinianData: $('#DatePengkinianData').val(),
                        NamaDepanInd: $('#NamaDepanInd').val(),
                        NamaTengahInd: $('#NamaTengahInd').val(),
                        NamaBelakangInd: $('#NamaBelakangInd').val(),
                        NoIdentitasInd1: $('#NoIdentitasInd1').val(),
                        RegistrationDateIdentitasInd1: $('#RegistrationDateIdentitasInd1').val(),
                        ExpiredDateIdentitasInd1: $('#ExpiredDateIdentitasInd1').val(),
                        OtherAlamatInd1: $('#OtherAlamatInd1').val(),
                        OtherPropinsiInd1: $('#OtherPropinsiInd1').val(),
                        OtherNegaraInd1: $('#OtherNegaraInd1').val(),
                        OtherKodePosInd1: $('#OtherKodePosInd1').val(),
                        IdentitasInd2: $('#IdentitasInd2').val(),
                        NoIdentitasInd2: $('#NoIdentitasInd2').val(),
                        RegistrationDateIdentitasInd2: $('#RegistrationDateIdentitasInd2').val(),
                        ExpiredDateIdentitasInd2: $('#ExpiredDateIdentitasInd2').val(),
                        AlamatInd2: $('#AlamatInd2').val(),
                        DomicileRT: $('#DomicileRT').val(),
                        DomicileRW: $('#DomicileRW').val(),
                        KodeKotaInd2: $('#KodeKotaInd2').val(),
                        KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),
                        CountryofDomicile: $('#CountryofDomicile').val(),
                        KodePosInd2: $('#KodePosInd2').val(),
                        TempatLahir: $('#TempatLahir').val(),
                        TanggalLahir: $('#TanggalLahir').val(),
                        JenisKelamin: $('#JenisKelamin').val(),
                        Nationality: $('#Nationality').val(),
                        CountryOfBirth: $('#CountryOfBirth').val(),
                        Agama: $('#Agama').val(),
                        OtherAgama: $('#OtherAgama').val(),
                        Pendidikan: $('#Pendidikan').val(),
                        OtherPendidikan: $('#OtherPendidikan').val(),
                        MotherMaidenName: $('#MotherMaidenName').val(),
                        AhliWaris: $('#AhliWaris').val(),
                        HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                        Pekerjaan: $('#Pekerjaan').val(),
                        OtherOccupation: $('#OtherOccupation').val(),
                        NatureOfBusiness: $('#NatureOfBusiness').val(),
                        NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                        PenghasilanInd: $('#PenghasilanInd').val(),
                        SumberDanaInd: $('#SumberDanaInd').val(),
                        MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                        StatusPerkawinan: $('#StatusPerkawinan').val(),
                        SpouseName: $('#SpouseName').val(),
                        SpouseDateOfBirth: $('#SpouseDateOfBirth').val(),
                        SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                        SpouseOccupation: $('#SpouseOccupation').val(),
                        OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                        SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                        SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther').val(),
                        SpouseIDNo: $('#SpouseIDNo').val(),
                        SpouseNationality: $('#SpouseNationality').val(),
                        SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),
                        NamaKantor: $('#NamaKantor').val(),
                        EmployerLineOfBusiness: $('#EmployerLineOfBusiness').val(),
                        JabatanKantor: $('#JabatanKantor').val(),
                        TeleponKantor: $('#TeleponKantor').val(),
                        AlamatKantorInd: $('#AlamatKantorInd').val(),
                        KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                        KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                        KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                        KodePosKantorInd: $('#KodePosKantorInd').val(),
                        Politis: $('#Politis').val(),
                        PolitisLainnya: $('#PolitisLainnya').val(),
                        PolitisRelation: $('#PolitisRelation').val(),
                        PolitisName: $('#PolitisName').val(),
                        PolitisFT: $('#PolitisFT').val(),
                        EntryUsersID: $('#EntryUsersID').val(),
                        EntryTime: $('#EntryTime').val(),
                        UpdateUsersID: $('#UpdateUsersID').val(),
                        UpdateTime: $('#UpdateTime').val(),
                        ApprovedUsersID: $('#ApprovedUsersID').val(),
                        ApprovedTime: $('#ApprovedTime').val(),
                        VoidUsersID: $('#VoidUsersID').val(),
                        VoidTime: $('#VoidTime').val(),
                        LastUpdate: $('#LastUpdate').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundClientAffiliated/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_I",
                        type: 'POST',
                        data: JSON.stringify(FundClientAffiliated),
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


            alertify.confirm("Are you sure want to Update data?", function (e) {
                if (e) {
                    var FundClientAffiliated = {
                        IdentitasInd1: $('#IdentitasInd1').val(),
                        Status: $('#Status').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        InvestorType: $('#InvestorType').val(),
                        Name: $('#Name').val(),
                        SID: $('#SID').val(),
                        NPWP: $('#NPWP').val(),
                        RegistrationNPWP: $('#RegistrationNPWP').val(),
                        InvestorsRiskProfile: $('#InvestorsRiskProfile').val(),
                        KYCRiskProfile: $('#KYCRiskProfile').val(),
                        BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                        AssetOwner: $('#AssetOwner').val(),
                        DatePengkinianData: $('#DatePengkinianData').val(),
                        NamaDepanInd: $('#NamaDepanInd').val(),
                        NamaTengahInd: $('#NamaTengahInd').val(),
                        NamaBelakangInd: $('#NamaBelakangInd').val(),
                        NoIdentitasInd1: $('#NoIdentitasInd1').val(),
                        RegistrationDateIdentitasInd1: $('#RegistrationDateIdentitasInd1').val(),
                        ExpiredDateIdentitasInd1: $('#ExpiredDateIdentitasInd1').val(),
                        OtherAlamatInd1: $('#OtherAlamatInd1').val(),
                        OtherPropinsiInd1: $('#OtherPropinsiInd1').val(),
                        OtherNegaraInd1: $('#OtherNegaraInd1').val(),
                        OtherKodePosInd1: $('#OtherKodePosInd1').val(),
                        IdentitasInd2: $('#IdentitasInd2').val(),
                        NoIdentitasInd2: $('#NoIdentitasInd2').val(),
                        RegistrationDateIdentitasInd2: $('#RegistrationDateIdentitasInd2').val(),
                        ExpiredDateIdentitasInd2: $('#ExpiredDateIdentitasInd2').val(),
                        AlamatInd2: $('#AlamatInd2').val(),
                        DomicileRT: $('#DomicileRT').val(),
                        DomicileRW: $('#DomicileRW').val(),
                        KodeKotaInd2: $('#KodeKotaInd2').val(),
                        KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),
                        CountryofDomicile: $('#CountryofDomicile').val(),
                        KodePosInd2: $('#KodePosInd2').val(),
                        TempatLahir: $('#TempatLahir').val(),
                        TanggalLahir: $('#TanggalLahir').val(),
                        JenisKelamin: $('#JenisKelamin').val(),
                        Nationality: $('#Nationality').val(),
                        CountryOfBirth: $('#CountryOfBirth').val(),
                        Agama: $('#Agama').val(),
                        OtherAgama: $('#OtherAgama').val(),
                        Pendidikan: $('#Pendidikan').val(),
                        OtherPendidikan: $('#OtherPendidikan').val(),
                        MotherMaidenName: $('#MotherMaidenName').val(),
                        AhliWaris: $('#AhliWaris').val(),
                        HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                        Pekerjaan: $('#Pekerjaan').val(),
                        OtherOccupation: $('#OtherOccupation').val(),
                        NatureOfBusiness: $('#NatureOfBusiness').val(),
                        NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                        PenghasilanInd: $('#PenghasilanInd').val(),
                        SumberDanaInd: $('#SumberDanaInd').val(),
                        MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                        StatusPerkawinan: $('#StatusPerkawinan').val(),
                        SpouseName: $('#SpouseName').val(),
                        SpouseDateOfBirth: $('#SpouseDateOfBirth').val(),
                        SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                        SpouseOccupation: $('#SpouseOccupation').val(),
                        OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                        SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                        SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther').val(),
                        SpouseIDNo: $('#SpouseIDNo').val(),
                        SpouseNationality: $('#SpouseNationality').val(),
                        SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),
                        NamaKantor: $('#NamaKantor').val(),
                        EmployerLineOfBusiness: $('#EmployerLineOfBusiness').val(),
                        JabatanKantor: $('#JabatanKantor').val(),
                        TeleponKantor: $('#TeleponKantor').val(),
                        AlamatKantorInd: $('#AlamatKantorInd').val(),
                        KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                        KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                        KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                        KodePosKantorInd: $('#KodePosKantorInd').val(),
                        Politis: $('#Politis').val(),
                        PolitisLainnya: $('#PolitisLainnya').val(),
                        PolitisRelation: $('#PolitisRelation').val(),
                        PolitisName: $('#PolitisName').val(),
                        PolitisFT: $('#PolitisFT').val(),
                        EntryUsersID: $('#EntryUsersID').val(),
                        EntryTime: $('#EntryTime').val(),
                        UpdateUsersID: $('#UpdateUsersID').val(),
                        UpdateTime: $('#UpdateTime').val(),
                        ApprovedUsersID: $('#ApprovedUsersID').val(),
                        ApprovedTime: $('#ApprovedTime').val(),
                        VoidUsersID: $('#VoidUsersID').val(),
                        VoidTime: $('#VoidTime').val(),
                        LastUpdate: $('#LastUpdate').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundClientAffiliated/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_U",
                        type: 'POST',
                        data: JSON.stringify(FundClientAffiliated),
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

    $("#BtnApproved").click(function () {

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                var FundClientAffiliated = {
                    NoIdentitasInd1: $('#NoIdentitasInd1').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClientAffiliated/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_A",
                    type: 'POST',
                    data: JSON.stringify(FundClientAffiliated),
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


    });

    $("#BtnReject").click(function () {

        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var FundClientAffiliated = {
                    NoIdentitasInd1: $('#NoIdentitasInd1').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClientAffiliated/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_R",
                    type: 'POST',
                    data: JSON.stringify(FundClientAffiliated),
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

    });


});