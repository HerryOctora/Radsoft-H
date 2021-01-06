$(document).ready(function () {
    document.title = 'FORM FUND CLIENT';
    //Global Variabel
    var win;
    var WinIdentity1;
    var winOldData;
    var tabindex, subtabindex;
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
    var GlobUsersClientMode;
    //grid filter
    var filterField;
    var filterOperator;
    var filtervalue;
    var filterlogic;
    //end grid filter

    var checkedApproved = {};

    function addDays(date, days) {
        var result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    }

    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    //setting hide show label
    if (_GlobClientCode == "03") {
        $("#lblPIC").show();
    }
    else if (_GlobClientCode == "08") {
        $("#lblFrontID").show();
        $("#LblTotalAsset").show();
    }
    else if (_GlobClientCode == "10") {
        $("#lblSelfieImgUrl").show();
        $("#lblKtpImgUrl").show();
    }
    else if (_GlobClientCode == '25' || _GlobClientCode == '14') {
        $("#LblReferralIDFO").show();
    }
    else if (_GlobClientCode == '32') {
        $("#lblGetDataFromCoreBanking").show();
    }
    else {
        $("#lblPIC").hide();
        $("#lblFrontID").hide();
        $("#LblReferralIDFO").hide();
        $("#lblSelfieImgUrl").hide();
        $("#lblKtpImgUrl").hide();
        $("#lblGetDataFromCoreBanking").hide();
        $("#LblTotalAsset").hide();
    }

    $('#CopyFromKTP1').change(function () {
        if ($('#CopyFromKTP1').is(":checked") == true) {
            $("#AlamatInd1").val($("#OtherAlamatInd1").val());
            $("#CorrespondenceRT").val($("#Identity1RT").val());
            $("#CorrespondenceRW").val($("#Identity1RW").val());
            $("#KodeKotaInd1").val($("#OtherKodeKotaInd1").val());
            $("#KodeKotaInd1Desc").val($("#OtherKodeKotaInd1Desc").val());
            $("#Propinsi").val($("#OtherPropinsiInd1").val());
            $("#PropinsiDesc").val($("#OtherPropinsiInd1Desc").val());
            $("#CountryofCorrespondence").val($("#OtherNegaraInd1").val());
            $("#CountryofCorrespondenceDesc").val($("#OtherNegaraInd1Desc").val());
            $("#KodePosInd1").val($("#OtherKodePosInd1").val());
        } else {
            $("#AlamatInd1").val('');
            $("#CorrespondenceRT").val('');
            $("#CorrespondenceRW").val('');
            $("#KodeKotaInd1").val('');
            $("#KodeKotaInd1Desc").val('');
            $("#Propinsi").val('');
            $("#PropinsiDesc").val('');
            $("#CountryofCorrespondence").val('');
            $("#CountryofCorrespondenceDesc").val('');
            $("#KodePosInd1").val('');
        }
    });

    $('#CopyFromKTP2').change(function () {
        if ($('#CopyFromKTP2').is(":checked") == true) {
            $("#AlamatInd2").val($("#OtherAlamatInd1").val());
            $("#DomicileRT").val($("#Identity1RT").val());
            $("#DomicileRW").val($("#Identity1RW").val());
            $("#KodeKotaInd2").val($("#OtherKodeKotaInd1").val());
            $("#KodeKotaInd2Desc").val($("#OtherKodeKotaInd1Desc").val());
            $("#KodeDomisiliPropinsi").val($("#OtherPropinsiInd1").val());
            $("#KodeDomisiliPropinsiDesc").val($("#OtherPropinsiInd1Desc").val());
            $("#CountryofDomicile").val($("#OtherNegaraInd1").val());
            $("#CountryofDomicileDesc").val($("#OtherNegaraInd1Desc").val());
            $("#KodePosInd2").val($("#OtherKodePosInd1").val());
        } else {
            $("#AlamatInd2").val('');
            $("#DomicileRT").val('');
            $("#DomicileRW").val('');
            $("#KodeKotaInd2").val('');
            $("#KodeKotaInd2Desc").val('');
            $("#KodeDomisiliPropinsi").val('');
            $("#KodeDomisiliPropinsiDesc").val('');
            $("#CountryofDomicile").val('');
            $("#CountryofDomicileDesc").val('');
            $("#KodePosInd2").val('');
        }
    });

    $('#CopyFromCorrespondence').change(function () {
        if ($('#CopyFromCorrespondence').is(":checked") == true) {
            $('#CopyFromKTP2').prop("checked", false);
            $("#AlamatInd2").val($("#AlamatInd1").val());
            $("#DomicileRT").val($("#CorrespondenceRT").val());
            $("#DomicileRW").val($("#CorrespondenceRW").val());
            $("#KodeKotaInd2").val($("#KodeKotaInd1").val());
            $("#KodeKotaInd2Desc").val($("#KodeKotaInd1Desc").val());
            $("#KodeDomisiliPropinsi").val($("#Propinsi").val());
            $("#KodeDomisiliPropinsiDesc").val($("#PropinsiDesc").val());
            $("#CountryofDomicile").val($("#CountryofCorrespondence").val());
            $("#CountryofDomicileDesc").val($("#CountryofCorrespondenceDesc").val());
            $("#KodePosInd2").val($("#KodePosInd1").val());
        } else {
            $("#AlamatInd2").val('');
            $("#DomicileRT").val('');
            $("#DomicileRW").val('');
            $("#KodeKotaInd2").val('');
            $("#KodeKotaInd2Desc").val('');
            $("#KodeDomisiliPropinsi").val('');
            $("#KodeDomisiliPropinsiDesc").val('');
            $("#CountryofDomicile").val('');
            $("#CountryofDomicileDesc").val('');
            $("#KodePosInd2").val('');
        }
    });

    $("#BtnClientRpt").click(function () {
        showWinReport();
    });

    if (_GlobClientCode == '03') {
        function showWinReport() {
            clearParamReport();
            $("#ParamCategory").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "All", value: 3 },
                    { text: "Individual", value: 1 },
                    { text: "Institution", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamCategory,
                suggest: true
            });

            $("#ParamDate").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Expired Date Identity(1)", value: 1 },
                    { text: "Expired Date SKD", value: 3 },
                    { text: "Expired Date SIUP", value: 4 },
                    { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                    { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                    { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                    { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                    { text: "Update KYC Date", value: 9 },
                    { text: "Birth Date", value: 10 },
                ],
                filter: "contains",
                change: OnChangeParamDate,
                suggest: true
            });

            //combo box Agama
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamReligion").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeParamReligion
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function onChangeParamReligion() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

                if (this.value() != 0) {
                    $("#ParamDate").data("kendoComboBox").value(10);
                }

            }

            $("#ParamSort").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Date From To", value: 1 },
                    { text: "Month", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamSort,
                suggest: true
            });


            $("#ParamMonth").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "JANUARY", value: 1 },
                    { text: "FEBRUARY", value: 2 },
                    { text: "MARCH", value: 3 },
                    { text: "APRIL", value: 4 },
                    { text: "MAY", value: 5 },
                    { text: "JUNE", value: 6 },
                    { text: "JULY", value: 7 },
                    { text: "AUGUST", value: 8 },
                    { text: "SEPTEMBER", value: 9 },
                    { text: "OCTOBER", value: 10 },
                    { text: "NOVEMBER", value: 11 },
                    { text: "DECEMBER", value: 12 },
                ],
                filter: "contains",
                //change: OnChangeParamMonth,
                suggest: true
            });

            function OnChangeParamSort() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

                if (this.value() == 1) {
                    $("#LblDateFrom").show();
                    $("#LblDateTo").show();
                    $("#lblMonth").hide();
                }
                else {
                    $("#lblMonth").show();
                    $("#LblDateFrom").hide();
                    $("#LblDateTo").hide();
                    $("#DateFrom").val("");
                    $("#DateTo").val("");
                }

            }

            //$("#ParamDate").data("kendoComboBox").value(10);

            WinGenerateReport.center();
            WinGenerateReport.open();

        }



        function OnChangeParamDate() {
            alert(this.value());

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            $("#ParamReligion").data("kendoComboBox").value("");
            $("#ParamMonth").data("kendoComboBox").value("");


            if (this.value() == 11) {
                $("#lblReligion").show();
                $("#lblMonth").hide();
                $("#btnDisplayIdentity1").hide();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else if (this.value() == 10) {

                $("#lblReligion").show();
                $("#lblSort").show();
                $("#lblMonth").show();
                $("#LblDateFrom").show();
                $("#btnDisplayIdentity1").show();
                $("#LblDateTo").show();

            }
            else if (this.value() == 9 || this.value() == 1 || this.value() == 2
                || this.value() == 3 || this.value() == 4 || this.value() == 5
                || this.value() == 6 || this.value() == 7 || this.value() == 8) {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").hide();
                $("#LblDateTo").hide();
            }

        }



        function clearParamReport() {
            //$("#ParamCategory").val("");
            $("#ParamDate").val("");
            $("#lblMonth").hide();
            $("#lblReligion").hide();
            $("#LblDateFrom").hide();
            $("#LblDateTo").hide();
            $("#DateFrom").val("");
            $("#DateTo").val("");
        }


        function OnChangeParamCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {

                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date Identity(1)", value: 1 },
                        { text: "Update KYC Date", value: 9 },
                        { text: "Birth Date", value: 10 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });


            }
            else if (this.value() == 2) {

                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date SKD", value: 3 },
                        { text: "Expired Date SIUP", value: 4 },
                        { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                        { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                        { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                        { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                        { text: "Update KYC Date", value: 9 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

            }
            else {

                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Update KYC Date", value: 9 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

                $("#lblReligion").hide();
            }
        }
    }
    else if (_GlobClientCode == '09' || _GlobClientCode == '14') //---------------------------------------------------------------------EMCO dan AYERS-----------
    {
        $("#lblReligion").show();

        function showWinReport() {
            clearParamReport();
            $("#ParamCategory").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    //{ text: "All", value: 3 },
                    { text: "Individual", value: 1 },
                    //{ text: "Institution", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamCategory,
                suggest: true
            });

            $("#ParamDate").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Expired Date Identity(1)", value: 1 },
                    { text: "Expired Date SKD", value: 3 },
                    { text: "Expired Date SIUP", value: 4 },
                    { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                    { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                    { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                    { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                    { text: "Update KYC Date", value: 9 },
                    { text: "Birth Date", value: 10 },
                    { text: "Religion", value: 11 },
                ],
                filter: "contains",
                change: OnChangeParamDate,
                suggest: true
            });

            //combo box Agama
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamReligion").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeParamReligion
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function onChangeParamReligion() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }


            }

            $("#ParamSort").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Date From To", value: 1 },
                    { text: "Month", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamSort,
                suggest: true
            });


            $("#ParamMonth").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "JANUARY", value: 1 },
                    { text: "FEBRUARY", value: 2 },
                    { text: "MARCH", value: 3 },
                    { text: "APRIL", value: 4 },
                    { text: "MAY", value: 5 },
                    { text: "JUNE", value: 6 },
                    { text: "JULY", value: 7 },
                    { text: "AUGUST", value: 8 },
                    { text: "SEPTEMBER", value: 9 },
                    { text: "OCTOBER", value: 10 },
                    { text: "NOVEMBER", value: 11 },
                    { text: "DECEMBER", value: 12 },
                ],
                filter: "contains",
                //change: OnChangeParamDate,
                suggest: true
            });

            function OnChangeParamSort() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

                if (this.value() == 1) {
                    $("#LblDateFrom").show();
                    $("#LblDateTo").show();
                    $("#lblMonth").hide();
                }
                else {
                    $("#lblMonth").show();
                    $("#LblDateFrom").hide();
                    $("#LblDateTo").hide();
                    $("#DateFrom").val("");
                    $("#DateTo").val("");
                }

            }


            WinGenerateReport.center();
            WinGenerateReport.open();

        }

        function OnChangeParamDate() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            $("#ParamReligion").data("kendoComboBox").value("");
            $("#ParamMonth").data("kendoComboBox").value("");


            if (this.value() == 11) {
                $("#lblReligion").show();
                $("#lblMonth").hide();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else if (this.value() == 10) {
                $("#lblReligion").show();
                $("#lblSort").show();
                $("#lblMonth").show();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else if (this.value() == 9 || this.value() == 1 || this.value() == 2
                || this.value() == 3 || this.value() == 4 || this.value() == 5
                || this.value() == 6 || this.value() == 7 || this.value() == 8) {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").hide();
                $("#LblDateTo").hide();
            }

        }




        function clearParamReport() {
            //$("#ParamCategory").val("");
            $("#ParamDate").val("");
            $("#lblMonth").hide();
            $("#lblReligion").hide();
            $("#LblDateFrom").hide();
            $("#LblDateTo").hide();
            $("#DateFrom").val("");
            $("#DateTo").val("");
        }


        function OnChangeParamCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date Identity(1)", value: 1 },
                        { text: "Update KYC Date", value: 9 },
                        { text: "Birth Date", value: 10 },
                        { text: "Religion", value: 11 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });


            }
            else if (this.value() == 2) {

                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date SKD", value: 3 },
                        { text: "Expired Date SIUP", value: 4 },
                        { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                        { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                        { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                        { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                        { text: "Update KYC Date", value: 9 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

            }
            else {

                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Update KYC Date", value: 9 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

                $("#lblReligion").hide();
            }
        }

    }
    else //ini yg buat standart----------------------------------------------------------------------------SELESAI CORE---------------
    {
        //$("#lblReligion").show();
        function showWinReport() {
            clearParamReport();
            $("#ParamCategory").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "All", value: 3 },
                    { text: "Individual", value: 1 },
                    { text: "Institution", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamCategory,
                suggest: true
            });

            $("#ParamDate").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Expired Date Identity(1)", value: 1 },
                    { text: "Expired Date Identity(2)", value: 2 },
                    { text: "Expired Date SKD", value: 3 },
                    { text: "Expired Date SIUP", value: 4 },
                    { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                    { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                    { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                    { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                    { text: "Update KYC Date", value: 9 },
                    { text: "Birth Date", value: 10 },
                    { text: "Religion", value: 11 }
                ],
                filter: "contains",
                change: OnChangeParamDate,
                suggest: true
            });


            $("#ParamSort").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Date From To", value: 1 },
                    { text: "Month", value: 2 },

                ],
                filter: "contains",
                change: OnChangeParamSort,
                suggest: true
            });

            $("#ParamMonth").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "JANUARY", value: 1 },
                    { text: "FEBRUARY", value: 2 },
                    { text: "MARCH", value: 3 },
                    { text: "APRIL", value: 4 },
                    { text: "MAY", value: 5 },
                    { text: "JUNE", value: 6 },
                    { text: "JULY", value: 7 },
                    { text: "AUGUST", value: 8 },
                    { text: "SEPTEMBER", value: 9 },
                    { text: "OCTOBER", value: 10 },
                    { text: "NOVEMBER", value: 11 },
                    { text: "DECEMBER", value: 12 },
                ],
                filter: "contains",
                //change: OnChangeParamMonth,
                suggest: true
            });

            //combo box Agama
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamReligion").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeParamReligion
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function onChangeParamReligion() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');

                }

                if (this.value() == 11) {
                    $("#ParamDate").data("kendoComboBox").value(11);

                }
                else if (this.value() == 10) {
                    $("#ParamDate").data("kendoComboBox").value(10);
                }

            }

            function OnChangeParamSort() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

                if (this.value() == 1) {
                    $("#LblDateFrom").show();
                    $("#LblDateTo").show();
                    $("#lblMonth").hide();
                }
                else {
                    $("#lblMonth").show();
                    $("#LblDateFrom").hide();
                    $("#LblDateTo").hide();
                    $("#DateFrom").val("");
                    $("#DateTo").val("");
                }

            }

            WinGenerateReport.center();
            WinGenerateReport.open();

        }

        function OnChangeParamDate() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            $("#ParamReligion").data("kendoComboBox").value("");
            $("#ParamMonth").data("kendoComboBox").value("");


            if (this.value() == 11) {
                $("#lblReligion").show();
                $("#lblMonth").hide();
                $("#LblDateFrom").hide();
                $("#LblDateTo").hide();
            }
            else if (this.value() == 10) {
                if (_GlobClientCode != 25) {
                    $("#lblReligion").show();
                    $("#lblSort").show();
                    $("#lblMonth").show();
                    $("#LblDateFrom").show();
                    $("#LblDateTo").show();
                }
            }
            else if (this.value() == 9 || this.value() == 1 || this.value() == 2
                || this.value() == 3 || this.value() == 4 || this.value() == 5
                || this.value() == 6 || this.value() == 7 || this.value() == 8) {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").show();
                $("#LblDateTo").show();
            }
            else {
                $("#lblReligion").hide();
                $("#lblMonth").hide();
                $("#LblDateFrom").hide();
                $("#LblDateTo").hide();
            }


        }




        function clearParamReport() {
            //$("#ParamCategory").val("");
            $("#ParamDate").val("");
            $("#lblMonth").hide();
            $("#lblReligion").hide();
            $("#LblDateFrom").hide();
            $("#LblDateTo").hide();
            $("#DateFrom").val("");
            $("#DateTo").val("");
            $("#ParamReligion").val("");
            $("#ParamMonth").val("");

        }


        function OnChangeParamCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            clearParamReport();
            if (this.value() == 1) {
                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date Identity(1)", value: 1 },
                        { text: "Expired Date Identity(2)", value: 2 },
                        { text: "Update KYC Date", value: 9 },
                        { text: "Birth Date", value: 10 },
                        { text: "Religion", value: 11 }
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });


            }
            else if (this.value() == 2) {
                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Expired Date SKD", value: 3 },
                        { text: "Expired Date SIUP", value: 4 },
                        { text: "Expired Date Identity 1 Officer(1)", value: 5 },
                        { text: "Expired Date Identity 2 Officer(1)", value: 6 },
                        { text: "Expired Date Identity 1 Officer(2)", value: 7 },
                        { text: "Expired Date Identity 2 Officer(2)", value: 8 },
                        { text: "Update KYC Date", value: 9 }
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

            }
            else {
                $("#ParamDate").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Update KYC Date", value: 9 },
                    ],
                    filter: "contains",
                    change: OnChangeParamDate,
                    suggest: true
                });

            }
        }



    }

    $("#BtnOkReport").click(function () {
        alertify.confirm("Are you sure want to Generate Report ?", function (e) {
            if (e) {


                $.blockUI({});
                var Report = {
                    InvestorType: $("#ParamInvestorType").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateTo').val(),
                    ParamDate: $("#ParamDate").val(),
                    ParamReligion: $("#ParamReligion").val(),
                    ParamMonth: $("#ParamMonth").val(),
                };

                if (_GlobClientCode == "25") {
                    if ($("#ParamDate").val() == '10') {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/GenerateReportFundClientIndividuByBirthDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(Report),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $.unblockUI();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }

                        });
                    }
                    else {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/FundClientByInvestorTypeReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(Report),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $.unblockUI();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }

                        });
                    }
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/FundClientByInvestorTypeReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(Report),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');
                            //window.location = data
                            $.unblockUI();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }

                    });
                }

                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Reports/FundClientByInvestorTypeReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                //    type: 'POST',
                //    data: JSON.stringify(Report),
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        var newwindow = window.open(data, '_blank');
                //        //window.location = data
                //        $.unblockUI();
                //    },
                //    error: function (data) {
                //        alertify.alert(data.responseText);
                //        $.unblockUI();
                //    }

                //});
            }
        });

    });

    $("#BtnCancelReport").click(function () {

        alertify.confirm("Are you sure want to close Generate Report?", function (e) {
            if (e) {
                WinGenerateReport.close();
                alertify.alert("Close Generate Report");
            }
        });
    });


    function initButton() {
        if (_GlobClientCode == "10") {
            $("#lblPolitisName").show();
            $("#lblPolitisFT").show();
        }
        else {
            $("#lblPolitisName").hide();
            $("#lblPolitisFT").hide();
        }

        $("#BtnClientRpt").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnCopyClient").kendoButton({
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
        $("#BtnGenerateARIAText").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOkGenerateARIAText").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelGenerateARIAText").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnOkGenerateSInvest").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelGenerateSInvest").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnGenerateSInvest").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnGenerateSInvestBankAccount").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnGenerateSInvestBankAccountVA").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnGenerateNKPD").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOkGenerateNKPD").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelGenerateNKPD").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnSuspendBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnUnSuspendBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnImportSID").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBanks").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnKYCUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnDckData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });
        $("#BtnSendMail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnPreview").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnSendMailUnitTrustReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnPreviewUnitTrustReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnIDCard").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnSelfie").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnSignature").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnBeneficial").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnUpSID").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnImportFailedSID").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnGetDataFromCoreBanking").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnOKGetDataFromCoreBanking").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelGetDataFromCoreBanking").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

    }

    function initWindow() {
        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeParamDateFrom
        });

        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeParamDateTo
        });

        $("#ParamSInvestDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeParamSInvestDateFrom
        });

        $("#ParamSInvestDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeParamSInvestDateTo
        });

        function OnChangeParamSInvestDateFrom() {

            var currentSInvestDate = Date.parse($("#ParamSInvestDateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentSInvestDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamSInvestDateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#ParamSInvestDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamSInvestDateTo").data("kendoDatePicker").value($("#ParamSInvestDateFrom").data("kendoDatePicker").value());
            }

        }

        function OnChangeParamSInvestDateTo() {

            var currentSInvestDate = Date.parse($("#ParamSInvestDateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentSInvestDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamSInvestDateTo").data("kendoDatePicker").value(new Date());
                return;
            }


        }

        function OnChangeParamDateFrom() {

            var currentDate = Date.parse($("#ParamDateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamDateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#ParamDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamDateTo").data("kendoDatePicker").value($("#ParamDateFrom").data("kendoDatePicker").value());
            }

            initHistoricalSummary($("#FundClientPK").val());
            initPositionSummary($("#FundClientPK").val());
        }

        function OnChangeParamDateTo() {

            var currentDate = Date.parse($("#ParamDateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamDateTo").data("kendoDatePicker").value(new Date());
                return;
            }
            initHistoricalSummary($("#FundClientPK").val());
            initPositionSummary($("#FundClientPK").val());

        }

        $("#paramValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeparamValueDateFrom,
            value: '01/Jan/1990'
            //value: new Date(),
        });

        $("#paramValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeparamValueDateTo,
            value: new Date(),
        });


        function OnChangeparamValueDateFrom() {
            var _DateFrom = Date.parse($("#paramValueDateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#paramValueDateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            //$("#paramValueDateTo").data("kendoDatePicker").value($("#paramValueDateFrom").data("kendoDatePicker").value());
            refresh();
        }
        function OnChangeparamValueDateTo() {
            var _DateTo = Date.parse($("#paramValueDateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateTo) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#paramValueDateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }

        ////GetUserClientMode
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Users/GetUsersClientMode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        GlobUsersClientMode = data;
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


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

        $("#DateTo").kendoDatePicker({
            format: "dd-MMMM-yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd-MMMM-yyyy",
            value: new Date(),
            max: new Date(9998, 11, 31),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
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
            max: new Date(9998, 11, 31),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#SIUPExpirationDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            max: new Date(9998, 11, 31),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DormantDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#SpouseDateOfBirth").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#DatePengkinianData").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#OpeningDateSinvest").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#RenewingDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });

        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });
        $("#DOBOfficer1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#DOBOfficer2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#DOBOfficer3").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#DOBOfficer4").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#FaceToFaceDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });



        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
            }
        }
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

        WinGenerateReport = $("#WinGenerateReport").kendoWindow({
            height: "300px",
            title: "* Report",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateReportClose
        }).data("kendoWindow");

        WinGenerateSInvest = $("#WinGenerateSInvest").kendoWindow({
            height: "250px",
            title: "* Generate SInvest",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateSInvestClose
        }).data("kendoWindow");


        win = $("#WinFundClient").kendoWindow({
            height: "95%",
            title: "Fund Client Detail",
            visible: false,
            width: 1300,
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


        WinListNationality = $("#WinListNationality").kendoWindow({
            height: "420px",
            title: "Nationality List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListNationalityOfficer1 = $("#WinListNationalityOfficer1").kendoWindow({
            height: "420px",
            title: "NationalityOfficer1 List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListNationalityOfficer2 = $("#WinListNationalityOfficer2").kendoWindow({
            height: "420px",
            title: "NationalityOfficer2 List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListNationalityOfficer3 = $("#WinListNationalityOfficer3").kendoWindow({
            height: "420px",
            title: "NationalityOfficer3 List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListNationalityOfficer4 = $("#WinListNationalityOfficer4").kendoWindow({
            height: "420px",
            title: "NationalityOfficer4 List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinListBankVA = $("#WinListBankVA").kendoWindow({
            height: "440px",
            title: "BankVA List",
            visible: false,
            width: "900px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinGenerateUnitTrustReport = $("#WinGenerateUnitTrustReport").kendoWindow({
            height: "250px",
            title: "* Generate Unit Trust Report",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateUnitTrustReportClose
        }).data("kendoWindow");

        WinUpdateSID = $("#winUpdateSID").kendoWindow({
            height: "250px",
            title: "* Update SID",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            //close: onWinGenerateUnitTrustReportClose
        }).data("kendoWindow");

        //WinListSpouseNationality = $("#WinListSpouseNationality").kendoWindow({
        //    height: "520px",
        //    title: "SpouseNationality List",
        //    visible: false,
        //    width: "570px",
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    close: onWinListSpouseNationalityClose
        //}).data("kendoWindow");


        WinGetDataFromCoreBanking = $("#WinGetDataFromCoreBanking").kendoWindow({
            height: "250px",
            title: "* DATA FROM CIF CORE BANK",
            visible: false,
            width: "570px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinWinGetDataFromCoreBankingClose
        }).data("kendoWindow");


        WinIdentity1 = $("#winIdentity1").kendoWindow({
            height: 720,
            title: "Image",
            visible: false,
            width: 1280,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 });
            },

        }).data("kendoWindow");


        $("#searchGridData").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "ALL", value: "ALL" },
                { text: "TOP 200", value: "TOP200" },
                { text: "TOP 500", value: "TOP500" },
                { text: "TOP 1000", value: "TOP1000" },
                { text: "IFUA ONLY", value: "IFUAONLY" }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeSearchGridData,
            value: setCmbSearchGridData()
        });
        function OnChangeSearchGridData() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh();
        }

        function setCmbSearchGridData() {
            //if (_GlobClientCode == "21" || _GlobClientCode == "10")
            //    return "TOP200";
            //else
            //    return "ALL";

            if (_GlobClientCode == "03")
                return "ALL";
            else
                return "TOP200";


        }


    }

    function setFundClient(data) {

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
        $("#ComplRequired").val(data.ComplRequired);
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
        $("#FailedSinvestDesc").val(data.FailedSinvestDesc);
        $("#ClientCategory").val(data.ClientCategory);
        $("#InvestorType").val(data.InvestorType);
        $("#NamaKantor").val(data.NamaKantor);
        $("#JabatanKantor").val(data.JabatanKantor);
        $("#CompanyFax").val(data.CompanyFax);
        $("#CompanyMail").val(data.CompanyMail);
        $("#DatePengkinianData").val(kendo.toString(kendo.parseDate(data.DatePengkinianData), 'dd/MMM/yyyy'));
        $("#OpeningDateSinvest").val(kendo.toString(kendo.parseDate(data.OpeningDateSinvest), 'dd/MMM/yyyy'));
        $("#FaceToFaceDate").val(kendo.toString(kendo.parseDate(data.FaceToFaceDate), 'dd/MMM/yyyy'));
        $("#ReferralIDFO").val(data.ReferralIDFO);
        if (data.BitIsAfiliated == true) {
            $("#BitIsAfiliated").text('AFILIATED WITH CLIENT NO ' + data.AfiliatedFrom);
        } else {
            $("#BitIsAfiliated").text('FREE');
        }
        if (data.BitIsSuspend == true) {
            $("#BitIsSuspend").text('SUSPENDED FOR TRANSACTION');
        } else {
            $("#BitIsSuspend").text('AVAILABLE FOR TRANSACTION');
        }

        var _dormantDate = new Date(data.DormantDate);
        if (_dormantDate <= new Date() && kendo.toString(kendo.parseDate(data.DormantDate), 'dd/MM/yyyy') != '01/01/1900' && _dormantDate != null) {
            $("#BitIsSuspend").text('NOT SUSPENDED YET BUT DORMANT ACCOUNT');
        }

        $("#BeneficialName").val(data.BeneficialName);
        $("#BeneficialAddress").val(data.BeneficialAddress);
        $("#BeneficialIdentity").val(data.BeneficialIdentity);
        $("#BeneficialWork").val(data.BeneficialWork);
        $("#BeneficialRelation").val(data.BeneficialRelation);
        $("#BeneficialHomeNo").val(data.BeneficialHomeNo);
        $("#BeneficialPhoneNumber").val(data.BeneficialPhoneNumber);
        $("#BeneficialNPWP").val(data.BeneficialNPWP);
        $("#Referral").val(data.Referral);
        $("#FrontID").val(data.FrontID);

        $("#SelfieImgUrl").val(data.SelfieImgUrl);
        $("#KtpImgUrl").val(data.KtpImgUrl);

        if (_GlobClientCode == "20")
            initQuestionnaire();
        if (data.StatusAffiliated == 1 && _GlobClientCode == "10") {
            initAffiliated(data.NoIdentitasInd1);
        }



        //var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
        var _fatcaInsIndInit = "FATCA";
        console.log(_GlobClientCode);
        if (data.InvestorType == 1) {
            //ResetTab()
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $(GlobTabStrip.items()[3]).show();
            $(GlobTabStrip.items()[4]).hide();
            $(GlobTabStrip.items()[5]).hide();
            $(GlobTabStrip.items()[6]).show();
            $("#LblCapitalPaidIn").hide();


            if (_GlobClientCode == "07") {
                $(GlobTabStrip.items()[7]).show();
                $("#lblDckData").show();
                $("#btnDisplayKTP").show();
                $(GlobTabStrip.items()[11]).hide();
                if (data.Status == 1) {

                    $("#lblByPass").show();
                }
                else {

                    $("#lblByPass").hide();
                }
            } else {
                $(GlobTabStrip.items()[7]).hide();
                $(GlobTabStrip.items()[11]).show();
            }
            if (_GlobClientCode == "08") {
                $("#lblMigrationStatus").show();
                $("#lblSegmentClass").show();
                $("#lblCompanyTypeOJK").hide();
            }
            else {
                $("#lblMigrationStatus").hide();
                $("#lblSegmentClass").hide();
                $("#lblCompanyTypeOJK").hide();
            }

            if (_GlobClientCode == "10") {
                $("#lblRiskProfileScore").hide();
                $("#LblBitShareAbleToGroup").show();
                $("#lblRemarkBank1").show();
                $("#lblRemarkBank2").show();
                $("#btnDisplayIdentity1").show();
                $("#lblRemarkBank3").show();
                $("#InvestorsRiskProfile").attr('readonly', false);

            }
            else {
                $("#lblRiskProfileScore").show();
                $("#LblBitShareAbleToGroup").hide();
                $("#lblRemarkBank1").hide();
                $("#lblRemarkBank2").hide();
                $("#lblRemarkBank3").hide();
                $("#InvestorsRiskProfile").attr('readonly', true);
            }

            if (_GlobClientCode == "24") {
                $("#LblBitShareAbleToGroup").show();

            }
            else {
                $("#LblBitShareAbleToGroup").hide();
            }

            if (_GlobClientCode == '02') {
                console.log('B');
                $("#lblBtnEmail").show();
                $("#lblBtnPreview").show();
            }
            else if (_GlobClientCode == '03') {
                $("#lblBtnEmail").show();
                $("#lblBtnPreview").show();
            }
            else {
                $("#lblBtnEmail").hide();
                $("#lblBtnPreview").hide();
            }

            if (_GlobClientCode == '12') {
                if (data.Status == 2) {
                    $("#lblBtnPreviewUnitTrustReport").show();
                    $("#lblBtnEmailUnitTrustReport").show();
                }
                else {
                    $("#lblBtnPreviewUnitTrustReport").hide();
                    $("#lblBtnEmailUnitTrustReport").hide();
                }
            }
            else {
                $("#lblBtnPreviewUnitTrustReport").hide();
                $("#lblBtnEmailUnitTrustReport").hide();
            }
            $(GlobTabStrip.items()[8]).hide();
            $(GlobTabStrip.items()[9]).show();
            RefreshTab(0);
            _fatcaInsIndInit = "FATCA";
        } else if (data.InvestorType == 2) {
            //ResetTab()
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).hide();
            $(GlobTabStrip.items()[3]).hide();
            $(GlobTabStrip.items()[4]).show();
            $(GlobTabStrip.items()[5]).show();
            $(GlobTabStrip.items()[6]).show();
            $("#LblCapitalPaidIn").show();

            if (_GlobClientCode == "07") {
                $(GlobTabStrip.items()[7]).show();
                $("#lblDckData").show();
            } else {
                $(GlobTabStrip.items()[7]).hide();
            }

            if (_GlobClientCode == "08") {
                $("#lblMigrationStatus").hide();
                $("#lblSegmentClass").hide();
                $("#lblCompanyTypeOJK").show();
            }
            else {
                $("#lblMigrationStatus").hide();
                $("#lblSegmentClass").hide();
                $("#lblCompanyTypeOJK").hide();
            }

            if (_GlobClientCode == "10") {
                $("#lblRiskProfileScore").hide();
                $("#LblBitShareAbleToGroup").show();
                $("#lblRemarkBank1").show();
                $("#lblRemarkBank2").show();
                $("#lblRemarkBank3").show();
                $("#InvestorsRiskProfile").attr('readonly', false);
            }
            else {
                $("#lblRiskProfileScore").show();
                $("#LblBitShareAbleToGroup").hide();
                $("#lblRemarkBank1").hide();
                $("#lblRemarkBank2").hide();
                $("#lblRemarkBank3").hide();
                $("#InvestorsRiskProfile").attr('readonly', true);
            }

            if (_GlobClientCode == "24") {
                $("#LblBitShareAbleToGroup").show();

            }
            else {
                $("#LblBitShareAbleToGroup").hide();
            }

            $(GlobTabStrip.items()[8]).hide();
            $(GlobTabStrip.items()[9]).show();
            RefreshTab(0);
            _fatcaInsIndInit = "FATCAInsti";
        }

        RequiredAttributes(data.InvestorType);

        $("#AccountRDO").val("");
        $("#IDCardRDO").val("");
        $("#StatusRDO").val("");
        $("#InvalidLoginAttempsRDO").val("");
        if (_RDOEnable == true) {
            if (data.FundClientPK > 0 && data.FundClientPK != null) {
                $("#IDCardRDO").val(data.NoIdentitasInd1);
                if (data.EntryUsersID == 'RDO') {
                    $("#AccountRDO").val("ONLINE");
                } else {
                    $("#AccountRDO").val("REGULER");
                }

                // STATUS RDO DAN INVALID LOGIN ATTEMPS
                //READY -> sudah complete dan verified
                //PROFILE COMPLETE -> sudah complete dan belum verified
                //NONE -> profile belum complete dan belum verified
                //LOCKED -> user salah masukin password lebih dari 5x
                $.ajax({
                    url: "http://" + _GlobUrlServerRDOSched + "user/summary?email=" + $("#Email").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data != null) {
                            $("#StatusRDO").val(data.userStatus);
                            $("#InvalidLoginAttempsRDO").val(data.invalidLoginAttempts);
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });






            }
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
        $("#CountryOfBirth").val(data.CountryOfBirth);
        $("#HubunganAhliWaris").val(data.HubunganAhliWaris);
        $("#NatureOfBusinessLainnya").val(data.NatureOfBusinessLainnya);
        $("#PolitisLainnya").val(data.PolitisLainnya);
        $("#PolitisName").val(data.PolitisName);
        $("#PolitisFT").val(data.PolitisFT);
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

        $("#AlamatKantorInd").val(data.AlamatKantorInd);
        $("#KodeKotaKantorInd").val(data.KodeKotaKantorInd);
        $("#KodeKotaKantorIndDesc").val(data.KodeKotaKantorIndDesc);
        $("#KodePosKantorInd").val(data.KodePosKantorInd);
        $("#KodePropinsiKantorInd").val(data.KodePropinsiKantorInd);
        $("#KodePropinsiKantorIndDesc").val(data.KodePropinsiKantorIndDesc);
        $("#KodeCountryofKantor").val(data.KodeCountryofKantor);
        $("#KodeCountryofKantorDesc").val(data.KodeCountryofKantorDesc);
        $("#CorrespondenceRT").val(data.CorrespondenceRT);
        $("#CorrespondenceRW").val(data.CorrespondenceRW);
        $("#DomicileRT").val(data.DomicileRT);
        $("#DomicileRW").val(data.DomicileRW);
        $("#Identity1RT").val(data.Identity1RT);
        $("#Identity1RW").val(data.Identity1RW);
        $("#KodeDomisiliPropinsi").val(data.KodeDomisiliPropinsi);
        $("#KodeDomisiliPropinsiDesc").val(data.KodeDomisiliPropinsiDesc);
        $("#TeleponRumah").val(data.TeleponRumah);
        $("#TeleponSelular").val(data.TeleponSelular);
        $("#OtherTeleponRumah").val(data.OtherTeleponRumah);
        $("#OtherTeleponSelular").val(data.OtherTeleponSelular);
        $("#TeleponSelular").val(data.TeleponSelular);
        $("#Fax").val(data.Fax);
        $("#OtherFax").val(data.OtherFax);
        $("#TeleponKantor").val(data.TeleponKantor);

        $("#CountryofCorrespondence").val(data.CountryofCorrespondence);
        $("#CountryofCorrespondenceDesc").val(data.CountryofCorrespondenceDesc);
        $("#CountryofDomicile").val(data.CountryofDomicile);
        $("#CountryofDomicileDesc").val(data.CountryofDomicileDesc);
        $("#OtherAgama").val(data.OtherAgama);
        $("#OtherPendidikan").val(data.OtherPendidikan);
        $("#OtherSpouseOccupation").val(data.OtherSpouseOccupation);
        $("#OtherCurrency").val(data.OtherCurrency);
        $("#OtherInvestmentObjectives").val(data.OtherInvestmentObjectives);


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
        $("#TeleponBisnis").val(data.TeleponBisnis);
        $("#OtherCharacteristic").val(data.OtherCharacteristic);
        $("#OtherInvestmentObjectivesIns").val(data.OtherInvestmentObjectivesIns);
        $("#OtherSourceOfFundsIns").val(data.OtherSourceOfFundsIns);

        $("#PlaceOfBirthOfficer1").val(data.PlaceOfBirthOfficer1);
        $("#PlaceOfBirthOfficer2").val(data.PlaceOfBirthOfficer2);
        $("#PlaceOfBirthOfficer3").val(data.PlaceOfBirthOfficer3);
        $("#PlaceOfBirthOfficer4").val(data.PlaceOfBirthOfficer4);
        $("#AlamatOfficer1").val(data.AlamatOfficer1);
        $("#AlamatOfficer2").val(data.AlamatOfficer2);
        $("#AlamatOfficer3").val(data.AlamatOfficer3);
        $("#AlamatOfficer4").val(data.AlamatOfficer4);

        //BANK INFORMATION
        $("#JumlahBank").val(data.JumlahBank);
        $("#NamaBank1").val(data.NamaBank1);
        $("#NomorRekening1").val(data.NomorRekening1);
        $("#BICCode1Name").val(data.BICCode1Name);
        $("#NamaNasabah1").val(data.NamaNasabah1);
        $("#NamaBank2").val(data.NamaBank2);
        $("#NomorRekening2").val(data.NomorRekening2);
        $("#BICCode2Name").val(data.BICCode2Name);
        $("#NamaNasabah2").val(data.NamaNasabah2);
        $("#NamaBank3").val(data.NamaBank3);
        $("#NomorRekening3").val(data.NomorRekening3);
        $("#BICCode3Name").val(data.BICCode3Name);
        $("#NamaNasabah3").val(data.NamaNasabah3);
        //SInvest
        //$("#BIMemberCode1").val(data.BIMemberCode1);
        //$("#BIMemberCode2").val(data.BIMemberCode2);
        //$("#BIMemberCode3").val(data.BIMemberCode3);
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
        $("#RemarkBank1").val(data.RemarkBank1);
        $("#RemarkBank2").val(data.RemarkBank2);
        $("#RemarkBank3").val(data.RemarkBank3);

        //SPOUSE
        $("#SpouseBirthPlace").val(data.SpouseBirthPlace);
        $("#SpouseNatureOfBusinessOther").val(data.SpouseNatureOfBusinessOther);
        $("#SpouseIDNo").val(data.SpouseIDNo);
        $("#RenewingDate").val(kendo.toString(kendo.parseDate(data.RenewingDate), 'dd/MMM/yyyy'))

        $("#IsFaceToFace").prop('checked', data.IsFaceToFace);
        $("#BitDefaultPayment1").prop('checked', data.BitDefaultPayment1);
        $("#BitDefaultPayment2").prop('checked', data.BitDefaultPayment2);
        $("#BitDefaultPayment3").prop('checked', data.BitDefaultPayment3);

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

        if (data.DOBOfficer1 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.DOBOfficer1 == '1/1/1900 12:00:00 AM') {
            $("#DOBOfficer1 ").data("kendoDatePicker").value("");
        } else {
            $("#DOBOfficer1 ").data("kendoDatePicker").value(new Date(data.DOBOfficer1));
        }
        if (data.DOBOfficer2 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.DOBOfficer2 == '1/1/1900 12:00:00 AM') {
            $("#DOBOfficer2 ").data("kendoDatePicker").value("");
        } else {
            $("#DOBOfficer2 ").data("kendoDatePicker").value(new Date(data.DOBOfficer2));
        }
        if (data.DOBOfficer3 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.DOBOfficer3 == '1/1/1900 12:00:00 AM') {
            $("#DOBOfficer3 ").data("kendoDatePicker").value("");
        } else {
            $("#DOBOfficer3 ").data("kendoDatePicker").value(new Date(data.DOBOfficer3));
        }
        if (data.DOBOfficer4 == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.DOBOfficer4 == '1/1/1900 12:00:00 AM') {
            $("#DOBOfficer4 ").data("kendoDatePicker").value("");
        } else {
            $("#DOBOfficer4 ").data("kendoDatePicker").value(new Date(data.DOBOfficer4));
        }

        if (data.FaceToFaceDate == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)' || data.FaceToFaceDate == '1/1/1900 12:00:00 AM') {
            $("#FaceToFaceDate").data("kendoDatePicker").value("");
        } else {
            $("#FaceToFaceDate").data("kendoDatePicker").value(new Date(data.FaceToFaceDate));
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
        $("#Propinsi").val(data.Propinsi);
        $("#PropinsiDesc").val(data.PropinsiDesc);
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
        $("#SpouseNationality").val(data.SpouseNationality);
        $("#SpouseNationalityDesc").val(data.SpouseNationalityDesc);


        $("#FrontID").val(data.FrontID);
        $("#EmployerLineOfBusiness").val(data.EmployerLineOfBusiness);
        $("#EmployerLineOfBusinessDesc").val(data.EmployerLineOfBusinessDesc);


        $("#NationalityOfficer1").val(data.NationalityOfficer1);
        $("#NationalityOfficer1Desc").val(data.NationalityOfficer1Desc);
        $("#NationalityOfficer2").val(data.NationalityOfficer2);
        $("#NationalityOfficer2Desc").val(data.NationalityOfficer2Desc);
        $("#NationalityOfficer3").val(data.NationalityOfficer3);
        $("#NationalityOfficer3Desc").val(data.NationalityOfficer3Desc);
        $("#NationalityOfficer4").val(data.NationalityOfficer4);
        $("#NationalityOfficer4Desc").val(data.NationalityOfficer4Desc);

        $("#NoIdentitasOfficer1").val(data.NoIdentitasOfficer1);
        $("#NoIdentitasOfficer2").val(data.NoIdentitasOfficer2);
        $("#NoIdentitasOfficer3").val(data.NoIdentitasOfficer3);
        $("#NoIdentitasOfficer4").val(data.NoIdentitasOfficer4);

        //OTHER
        $("#OtherOccupation").val(data.OtherOccupation);
        $("#OtherAgama").val(data.OtherAgama);
        $("#OtherPendidikan").val(data.OtherPendidikan);
        $("#OtherSourceOfFunds").val(data.OtherSourceOfFunds);
        $("#OtherInvestmentObjectives").val(data.OtherInvestmentObjectives);
        $("#OtherTipe").val(data.OtherTipe);

        //RDN
        $("#RDNAccountNo").val(data.RDNAccountNo);
        $("#RDNAccountName").val(data.RDNAccountName);
        $("#RDNBankBranchName").val(data.RDNBankBranchName);


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

        $('#BitDefaultPayment1').change(function () {
            if (this.checked) {
                $("#BitDefaultPayment2").prop('checked', false);
                $("#BitDefaultPayment3").prop('checked', false);
            }
        });
        $('#BitDefaultPayment2').change(function () {
            if (this.checked) {
                $("#BitDefaultPayment1").prop('checked', false);
                $("#BitDefaultPayment3").prop('checked', false);
            }
        });
        $('#BitDefaultPayment3').change(function () {
            if (this.checked) {
                $("#BitDefaultPayment1").prop('checked', false);
                $("#BitDefaultPayment2").prop('checked', false);
            }
        });




        //Others
        if ($("#Pendidikan").val(data.Pendidikan) == 8) {
            $("#lblotherEducation").show();
        }
        else {
            $("#lblotherEducation").hide();
        }

        if ($("#Agama").val(data.Agama) == 7) {
            $("#lblotherreligion").show();
        } else {
            $("#lblotherreligion").hide();
        }

        if ($("#Pekerjaan").val(data.Pekerjaan) == 9) {
            $("#lbloccupation").show();
        } else {
            $("#lbloccupation").hide();
        }

        if ($("#SpouseOccupation").val(data.SpouseOccupation) == 9) {
            $("#lblOtherSpouseOccupation").show();
        } else {
            $("#lblOtherSpouseOccupation").hide();
        }

        if ($("#Currency").val(data.Currency) == 4) {
            $("#lblOtherCurrency").show();
        } else {
            $("#lblOtherCurrency").hide();
        }

        if ($("#SumberDanaInd").val(data.SumberDanaInd) == 10) {
            $("#lblOtherSourceOfFunds").show();
        } else {
            $("#lblOtherSourceOfFunds").hide();
        }

        if ($("#MaksudTujuanInd").val(data.MaksudTujuanInd) == 5) {
            $("#lblInvestmentObjectives").show();
        } else {
            $("#lblInvestmentObjectives").hide();
        }

        if ($("#Tipe").val(data.Tipe) == 8) {
            $("#lbltype").show();
        } else {
            $("#lbltype").hide();
        }
        if ($("#Karakteristik").val(data.Karakteristik) == 8) {
            $("#lblCharacteristic").hide();
        } else {
            $("#lblCharacteristic").show();
        }
        if ($("#MaksudTujuanInstitusi").val(data.MaksudTujuanInstitusi) == 5) {
            $("#lblMaksudTujuanInstitusi").show();
        } else {
            $("#lblMaksudTujuanInstitusi").hide();
        }
        if ($("#SumberDanaInstitusi").val(data.SumberDanaInstitusi) == 5) {
            $("#lblSumberDanaInstitusi").show();
        } else {
            $("#lblSumberDanaInstitusi").hide();
        }



        //General

        if ($("#AssetOwner").val(data.AssetOwner) == 2) {
            $("#LblAssetOwner").show();
        } else {
            $("#LblAssetOwner").hide();
        }

        if ($("#ClientOnBoard").val(data.ClientOnBoard) == 3) {
            $("#LblReferral").show();
        } else {
            $("#LblReferral").hide();
        }



        //NUMERIC

        $("#CapitalPaidIn").kendoNumericTextBox({
            format: "n0",
            value: setCapitalPaidIn(),
        });

        function setCapitalPaidIn() {
            if (data.CapitalPaidIn == null) {
                return "";
            } else {
                return data.CapitalPaidIn;
            }
        }

        $("#TotalAsset").kendoNumericTextBox({
            format: "n2",
            value: setTotalAsset(),
        });

        function setTotalAsset() {
            if (data.TotalAsset == null) {
                return "";
            } else {
                return data.TotalAsset;
            }
        }


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
                    $("#InvestorsRiskProfile").data("kendoComboBox").value(data.InvestorRiskProfile);
                    $("#InvestorsRiskProfile").data("kendoComboBox").text(data.InvestorRiskProfileDesc);
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


        // COMBO BOX BIT
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
            if (data.Child == null) {
                return false;
            } else {
                return data.BitShareAbleToGroup;
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





        //combo box InvestorType
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
                    change: onChangeInvestorType,
                    value: setCmbInvestorType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeInvestorType() {
            var _fatcaInsInd;
            //console.log(_GlobClientCode);

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                if (_GlobClientCode == "20")
                    initQuestionnaire();
                //var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
                if ($("#InvestorType").data("kendoComboBox").value() == 1) {
                    //ResetTab()
                    $(GlobTabStrip.items()[0]).show();
                    $(GlobTabStrip.items()[1]).show();
                    $(GlobTabStrip.items()[2]).show();
                    $(GlobTabStrip.items()[3]).show();
                    $(GlobTabStrip.items()[4]).hide();
                    $(GlobTabStrip.items()[5]).hide();
                    $(GlobTabStrip.items()[6]).show();
                    if (_GlobClientCode == "07") {
                        $(GlobTabStrip.items()[7]).show();
                        $("#lblDckData").show();
                    } else {
                        $(GlobTabStrip.items()[7]).hide();
                    }
                    if (_GlobClientCode == "08") {
                        $("#lblMigrationStatus").show();
                        $("#lblSegmentClass").show();
                        $("#lblCompanyTypeOJK").hide();
                    }
                    else {
                        $("#lblMigrationStatus").hide();
                        $("#lblSegmentClass").hide();
                        $("#lblCompanyTypeOJK").hide();
                    }

                    if (_GlobClientCode == "10") {
                        $("#lblRiskProfileScore").hide();
                        $("#LblBitShareAbleToGroup").show();
                        $("#lblRemarkBank1").show();
                        $("#lblRemarkBank2").show();
                        $("#lblRemarkBank3").show();
                        $("#InvestorsRiskProfile").attr('readonly', false);
                    }
                    else {
                        $("#lblRiskProfileScore").show();
                        $("#LblBitShareAbleToGroup").hide();
                        $("#lblRemarkBank1").hide();
                        $("#lblRemarkBank2").hide();
                        $("#lblRemarkBank3").hide();
                        $("#InvestorsRiskProfile").attr('readonly', true);
                    }

                    if (_GlobClientCode == "24") {
                        $("#LblBitShareAbleToGroup").show();

                    }
                    else {
                        $("#LblBitShareAbleToGroup").hide();
                    }

                    $(GlobTabStrip.items()[8]).hide();
                    $("#LblChild").hide();
                    _fatcaInsInd = "FATCA";

                } else if ($("#InvestorType").data("kendoComboBox").value() == 2) {
                    //ResetTab()
                    $(GlobTabStrip.items()[0]).show();
                    $(GlobTabStrip.items()[1]).show();
                    $(GlobTabStrip.items()[2]).hide();
                    $(GlobTabStrip.items()[3]).hide();
                    $(GlobTabStrip.items()[4]).show();
                    $(GlobTabStrip.items()[5]).show();
                    $(GlobTabStrip.items()[6]).show();
                    if (_GlobClientCode == "07") {
                        $(GlobTabStrip.items()[7]).show();
                        $("#lblDckData").show();
                    } else {
                        $(GlobTabStrip.items()[7]).hide();
                    }

                    if (_GlobClientCode == "08") {
                        $("#lblMigrationStatus").hide();
                        $("#lblSegmentClass").hide();
                        $("#lblCompanyTypeOJK").show();
                    }
                    else {
                        $("#lblMigrationStatus").hide();
                        $("#lblSegmentClass").hide();
                        $("#lblCompanyTypeOJK").hide();
                    }

                    if (_GlobClientCode == "10") {
                        $("#lblRiskProfileScore").hide();
                        $("#LblBitShareAbleToGroup").show();
                        $("#lblRemarkBank1").show();
                        $("#lblRemarkBank2").show();
                        $("#lblRemarkBank3").show();
                        $("#InvestorsRiskProfile").attr('readonly', false);
                    }
                    else {
                        $("#lblRiskProfileScore").show();
                        $("#LblBitShareAbleToGroup").hide();
                        $("#lblRemarkBank1").hide();
                        $("#lblRemarkBank2").hide();
                        $("#lblRemarkBank3").hide();
                        $("#InvestorsRiskProfile").attr('readonly', true);
                    }

                    if (_GlobClientCode == "24") {
                        $("#LblBitShareAbleToGroup").show();

                    }
                    else {
                        $("#LblBitShareAbleToGroup").hide();
                    }

                    $(GlobTabStrip.items()[8]).hide();
                    $("#LblChild").hide();
                    _fatcaInsInd = "FATCAInsti";
                }

                if ($("#InvestorType").data("kendoComboBox").value() == 1) {
                    $("#LblCapitalPaidIn").hide();
                }
                else {
                    $("#LblCapitalPaidIn").show();
                }

                $("#InvestorType").data("kendoComboBox").value($("#InvestorType").val());

                $.ajax({
                    url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fatcaInsInd,
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

            }
            ClearAttributes();
            RequiredAttributes($("#InvestorType").data("kendoComboBox").value());
            RefreshTab(0);
        }
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
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Users/GetUsersCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UsersPK").kendoComboBox({
                    dataValueField: "UsersPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeUsers,
                    value: setCmbUsers()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeUsers() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbUsers() {
            if (data.UsersPK == null) {
                return "";
            } else {
                if (data.UsersPK == 0) {
                    return "";
                } else {
                    return data.UsersPK;
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

            if ($("#StatusPerkawinan").val() == 2) {
                $("#lblSpouse").show();
            }
            else {
                $("#lblSpouse").hide();
            }
        }

        function setCmbStatusPerkawinan() {
            if (data.StatusPerkawinan == 2) {
                $("#lblSpouse").show();
            }
            else {
                $("#lblSpouse").hide();
            }
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
            if (data.EmployerLineOfBusiness == null) {
                return "";
            } else {
                if (data.EmployerLineOfBusiness == 0) {
                    return "";
                } else {
                    return data.EmployerLineOfBusiness;
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

                $("#BeneficialWork").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeBeneficialWork,
                    value: setCmbBeneficialWork()
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
            if (data.Pekerjaan == 3) {
                $("#lblNatureOfBusiness").show();
                $("#OtherOccupation").attr("required", false);

            }
            else if (data.Pekerjaan == 9) {
                $("#lbloccupation").show();
                $("#OtherOccupation").attr("required", true);

            }
            else if (data.Pekerjaan == 1) {
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

            else if (data.Pekerjaan == 2) {
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
            else if (data.Pekerjaan == 6) {
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

            if (data.SpouseOccupation == null) {
                return "";
            }
            else {
                if (data.SpouseOccupation == 0) {
                    return "";
                } else {
                    return data.SpouseOccupation;
                }
            }
        }


        function onChangeBeneficialWork() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        function setCmbBeneficialWork() {

            if (data.BeneficialWork == 0) {
                return "";
            } else {
                return data.BeneficialWork;
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
            if (data.Pendidikan == 8) {
                $("#lblotherEducation").show();
                $("#OtherPendidikan").attr("required", true);
            } else {
                $("#lblotherEducation").hide();
                $("#OtherPendidikan").attr("required", false);
            }
            if (data.Pendidikan == null) {
                return "";
            } else {
                if (data.Pendidikan == 0) {
                    return "";
                }
                else if (data.Pendidikan == 8) {
                    $("#lblotherEducation").show();
                }
                else {
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
            if (this.value() == 7) {
                $("#lblotherreligion").show();
                $("#OtherAgama").attr("required", true);
            } else {
                $("#lblotherreligion").hide();
                $("#OtherAgama").attr("required", false);
            }

        }
        function setCmbAgama() {
            if (data.Agama == 7) {
                $("#lblotherreligion").show();
                $("#OtherAgama").attr("required", true);
            } else {
                $("#lblotherreligion").hide();
                $("#OtherAgama").attr("required", false);
            }
            if (data.Agama == null) {
                return "";
            } else {
                if (data.Agama == 0) {
                    return "";
                }
                else if (data.Agama == 7) {
                    $("#lblotherreligion").show();
                }
                else {
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
            if (data.SumberDanaInd == 10) {
                $("#lblOtherSourceOfFunds").show();
                $("#OtherSourceOfFunds").attr("required", true);
            } else {
                $("#lblOtherSourceOfFunds").hide();
                $("#OtherSourceOfFunds").attr("required", false);
            }
            if (data.SumberDanaInd == null) {
                return "";
            } else {
                if (data.SumberDanaInd == 0) {
                    return "";
                }
                else if (data.SumberDanaInd == 10) {
                    $("#lblOtherSourceOfFunds").show();
                }
                else {
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

            if (this.value() == 5) {
                $("#lblInvestmentObjectives").show();
                $("#OtherInvestmentObjectives").attr("required", true);
            } else {
                $("#lblInvestmentObjectives").hide();
                $("#OtherInvestmentObjectives").attr("required", false);
            }
        }
        function setCmbMaksudTujuanInd() {
            if (data.MaksudTujuanInd == 5) {
                $("#lblInvestmentObjectives").show();
                $("#OtherInvestmentObjectives").attr("required", true);
            } else {
                $("#lblInvestmentObjectives").hide();
                $("#OtherInvestmentObjectives").attr("required", false);
            }
            if (data.MaksudTujuanInd == null) {
                return "";
            } else {
                if (data.MaksudTujuanInd == 0) {
                    return "";
                }
                else if (data.MaksudTujuanInd == 5) {
                    $("#lblInvestmentObjectives").show();
                }
                else {
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
                //----------------------------------//
                $("#IdentityTypeOfficer1").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentityTypeOfficer1()
                });
                $("#IdentityTypeOfficer2").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentityTypeOfficer2()
                });
                $("#IdentityTypeOfficer3").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentityTypeOfficer3()
                });
                $("#IdentityTypeOfficer4").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIdentitas,
                    dataSource: data,
                    value: setCmbIdentityTypeOfficer4()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


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

            if ($("#IdentitasInd3").val() == 7) {
                $("#ExpiredDateIdentitasInd3").data("kendoDatePicker").value("31129998")
            }
            else if ($("#IdentitasInd3").val() != 7) {
                $("#ExpiredDateIdentitasInd3").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasInd4").val() == 7) {
                $("#ExpiredDateIdentitasInd4").data("kendoDatePicker").value("31129998")
            }
            else if ($("#IdentitasInd4").val() != 7) {
                $("#ExpiredDateIdentitasInd4").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns11").val() == 7) {
                $("#ExpiredDateIdentitasIns11").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns11").val() != 7) {
                $("#ExpiredDateIdentitasIns11").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns12").val() == 7) {
                $("#ExpiredDateIdentitasIns12").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns12").val() != 7) {
                $("#ExpiredDateIdentitasIns12").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns13").val() == 7) {
                $("#ExpiredDateIdentitasIns13").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns13").val() != 7) {
                $("#ExpiredDateIdentitasIns13").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns14").val() == 7) {
                $("#ExpiredDateIdentitasIns14").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns14").val() != 7) {
                $("#ExpiredDateIdentitasIns14").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns21").val() == 7) {
                $("#ExpiredDateIdentitasIns21").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns21").val() != 7) {
                $("#ExpiredDateIdentitasIns21").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns22").val() == 7) {
                $("#ExpiredDateIdentitasIns22").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns22").val() != 7) {
                $("#ExpiredDateIdentitasIns22").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns23").val() == 7) {
                $("#ExpiredDateIdentitasIns23").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns23").val() != 7) {
                $("#ExpiredDateIdentitasIns23").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns24").val() == 7) {
                $("#ExpiredDateIdentitasIns24").data("kendoDatePicker").value("31129998")
            } else if ($("#IdentitasIns24").val() != 7) {
                $("#ExpiredDateIdentitasIns24").data("kendoDatePicker").value("")
            }

            if ($("#IdentitasIns31").val() == 7) {
                $("#ExpiredDateIdentitasIns31").data("kendoDatePicker").value("31129998")
            }


            if ($("#IdentitasIns32").val() == 7) {
                $("#ExpiredDateIdentitasIns32").data("kendoDatePicker").value("31129998")
            }


            if ($("#IdentitasIns33").val() == 7) {
                $("#ExpiredDateIdentitasIns33").data("kendoDatePicker").value("31129998")
            }

            if ($("#IdentitasIns34").val() == 7) {
                $("#ExpiredDateIdentitasIns34").data("kendoDatePicker").value("31129998")
            }

            if ($("#IdentitasIns41").val() == 7) {
                $("#ExpiredDateIdentitasIns41").data("kendoDatePicker").value("31129998")
            }

            if ($("#IdentitasIns42").val() == 7) {
                $("#ExpiredDateIdentitasIns42").data("kendoDatePicker").value("31129998")
            }

            if ($("#IdentitasIns43").val() == 7) {
                $("#ExpiredDateIdentitasIns43").data("kendoDatePicker").value("31129998")
            }

            if ($("#IdentitasIns44").val() == 7) {
                $("#ExpiredDateIdentitasIns44").data("kendoDatePicker").value("31129998")
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

        function setCmbIdentityTypeOfficer1() {
            if (data.IdentityTypeOfficer1 == null) {
                return "";
            } else {
                if (data.IdentityTypeOfficer1 == 0) {
                    return "";
                } else {
                    return data.IdentityTypeOfficer1;
                }
            }
        }
        function setCmbIdentityTypeOfficer2() {
            if (data.IdentityTypeOfficer2 == null) {
                return "";
            } else {
                if (data.IdentityTypeOfficer2 == 0) {
                    return "";
                } else {
                    return data.IdentityTypeOfficer2;
                }
            }
        }
        function setCmbIdentityTypeOfficer3() {
            if (data.IdentityTypeOfficer3 == null) {
                return "";
            } else {
                if (data.IdentityTypeOfficer3 == 0) {
                    return "";
                } else {
                    return data.IdentityTypeOfficer3;
                }
            }
        }
        function setCmbIdentityTypeOfficer4() {
            if (data.IdentityTypeOfficer4 == null) {
                return "";
            } else {
                if (data.IdentityTypeOfficer4 == 0) {
                    return "";
                } else {
                    return data.IdentityTypeOfficer4;
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

            if (this.value() == 8) {
                $("#lbltype").show();
                $("#OtherTipe").attr("required", true);
            } else {
                $("#lbltype").hide();
                $("#OtherTipe").attr("required", false);
            }
        }
        function setCmbTipe() {
            if (data.Tipe == 8) {
                $("#lbltype").show();
                $("#OtherTipe").attr("required", true);
            } else {
                $("#lbltype").hide();
                $("#OtherTipe").attr("required", false);
            }
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

            if (this.value() == 8) {
                $("#lblCharacteristic").show();
                $("#OtherCharacteristic").attr("required", true);
            } else {
                $("#lblCharacteristic").hide();
                $("#OtherCharacteristic").attr("required", false);
            }
        }
        function setCmbKarakteristik() {
            if (data.Karakteristik == 8) {
                $("#lblCharacteristic").show();
                $("#OtherCharacteristic").attr("required", true);
            } else {
                $("#lblCharacteristic").hide();
                $("#OtherCharacteristic").attr("required", false);
            }
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

            if (this.value() == 5) {
                $("#lblSumberDanaInstitusi").show();
                $("#OtherSourceOfFundsIns").attr("required", true);
            } else {
                $("#lblSumberDanaInstitusi").hide();
                $("#OtherSourceOfFundsIns").attr("required", false);
            }
        }
        function setCmbSumberDanaInstitusi() {
            if (data.SumberDanaInstitusi == 5) {
                $("#lblSumberDanaInstitusi").show();
                $("#OtherSourceOfFundsIns").attr("required", true);
            } else {
                $("#lblSumberDanaInstitusi").hide();
                $("#OtherSourceOfFundsIns").attr("required", false);
            }
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

            if (this.value() == 5) {
                $("#lblMaksudTujuanInstitusi").show();
                $("#OtherInvestmentObjectivesIns").attr("required", true);
            } else {
                $("#lblMaksudTujuanInstitusi").hide();
                $("#OtherInvestmentObjectivesIns").attr("required", false);
            }
        }

        function setCmbMaksudTujuanInstitusi() {
            if (data.MaksudTujuanInstitusi == 5) {
                $("#lblMaksudTujuanInstitusi").show();
                $("#OtherInvestmentObjectivesIns").attr("required", true);
            } else {
                $("#lblMaksudTujuanInstitusi").hide();
                $("#OtherInvestmentObjectivesIns").attr("required", false);
            }
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
                $("#RDNCurrency").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMataUang,
                    value: setCmbRDNCurrency()
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

            if (this.value() == 4) {
                $("#lblOtherCurrency").show();
                $("#OtherCurrency").attr("required", true);
            } else {
                $("#lblOtherCurrency").hide();
                $("#OtherCurrency").attr("required", false);
            }
        }

        function setCmbMataUang1() {
            if (data.MataUang1 == 4) {
                $("#lblOtherCurrency").show();
                $("#OtherCurrency").attr("required", true);
            } else {
                $("#lblOtherCurrency").hide();
                $("#OtherCurrency").attr("required", false);
            }

            if (data.MataUang1 == null) {
                return "";
            }
            else {
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

        function setCmbRDNCurrency() {
            if (data.RDNCurrency == null) {
                return "";
            } else {
                if (data.RDNCurrency == 0) {
                    return "";
                } else {
                    return data.RDNCurrency;
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
            if (data.AssetOwner == null) {
                return "";
            }

            else {
                if (data.AssetOwner == 0) {
                    return "";
                }
                else if ($("#AssetOwner").val() == 2) {
                    $("#LblAssetOwner").show();
                }
                else if ($("#AssetOwner").val() == 1) {
                    $("#LblAssetOwner").hide();
                }

                else {
                    return data.AssetOwner;
                }


            }


        }



        $("#ClientOnBoard").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Conventional Walk-in", value: 1 },
                { text: "Online", value: 2 },
                { text: "Referral", value: 3 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeClientOnBoard,
            value: setCmbClientOnBoard()
        });
        function OnChangeClientOnBoard() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#ClientOnBoard").val() == 3) {
                $("#LblReferral").show();
            } else {
                $("#LblReferral").hide();
                $("#Referral").val("");
            }
        }

        function setCmbClientOnBoard() {
            if (data.ClientOnBoard == null) {
                return '';
            } else {
                if ($("#ClientOnBoard").val() == 3) {
                    $("#LblReferral").show();
                } else {
                    $("#LblReferral").hide();
                }

                return data.ClientOnBoard;
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
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fatcaInsIndInit,
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
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankRDNPK() {
            $("#RDNBIMemberCode").val("");
            $("#BankRDNCountry").val("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else if ($("#BankRDNPK").val() != "") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankRDNPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#RDNBIMemberCode").val(data.BICode);
                        $("#BankRDNCountryDesc").val(data.CountryDesc);
                    },
                    error: function (data) {
                        alert(data.responseText);
                    }
                });
            }


        }



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


        if (data.NamaBank1 == null || data.NamaBank1 == 0) {
            $("#BIMemberCode1").val("");
            $("#BICCode1Name").val("");
            $("#BankCountry1").val("");
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + data.NamaBank1,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#BIMemberCode1").val(data.BICode);
                    $("#BankCountry1Desc").val(data.CountryDesc);
                    $("#BICCode1Name").val(data.SInvestID);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }

        if (data.NamaBank2 == null || data.NamaBank2 == 0) {
            $("#BIMemberCode2").val("");
            $("#BankCountry2").val("");
            $("#BICCode2Name").val("");
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + data.NamaBank2,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#BIMemberCode2").val(data.BICode);
                    $("#BankCountry2Desc").val(data.CountryDesc);
                    $("#BICCode2Name").val(data.SInvestID);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }

        if (data.NamaBank3 == null || data.NamaBank3 == 0) {
            $("#BIMemberCode3").val("");
            $("#BankCountry3").val("");
            $("#BICCode3Name").val("");
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + data.NamaBank3,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#BIMemberCode3").val(data.BICode);
                    $("#BankCountry3Desc").val(data.CountryDesc);
                    $("#BICCode3Name").val(data.SInvestID);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }

        if (data.BankRDNPK == null || data.BankRDNPK == 0) {
            $("#RDNBIMemberCode").val("");
            $("#BankRDNCountry").val("");
            $("#RDNBICCode").val("");
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Bank/GetBICCodeAndCountryByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + data.BankRDNPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#RDNBIMemberCode").val(data.BICode);
                    $("#BankRDNCountry").val(data.CountryDesc);
                    $("#RDNBICCode").val(data.SInvestID);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }

        //Tab Sub Data
        var loadedTabs = [0];
        $("#TabSub").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                subtabindex = $(e.item).index();
                //alert(subtabindex);
            }
        }).data("kendoTabStrip").select(0);

        //combo box Migration Status
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MigrationStatus",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MigrationStatus").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMigrationStatus,
                    value: setCmbMigrationStatus()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeMigrationStatus() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbMigrationStatus() {
            if (data.MigrationStatus == null) {
                return "";
            } else {
                if (data.MigrationStatus == 0) {
                    return "";
                } else {
                    return data.MigrationStatus;
                }
            }
        }

        //combo box Company Type OJK
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyTypeOJK",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CompanyTypeOJK").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeCompanyTypeOJK,
                    value: setCmbCompanyTypeOJK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeCompanyTypeOJK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
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

        //combo box Migration Status
        $.ajax({
            url: window.location.origin + "/Radsoft/SegmentClass/GetSegmentClassCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SegmentClass").kendoComboBox({
                    dataValueField: "SegmentClassPK",
                    dataTextField: "Name",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSegmentClass,
                    value: setCmbSegmentClass()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeSegmentClass() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSegmentClass() {
            if (data.MigrationStatus == null) {
                return "";
            } else {
                if (data.SegmentClass == 0) {
                    return "";
                } else {
                    return data.SegmentClass;
                }
            }
        }


        //combo box InvestorType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Legality",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Legality").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeLegality,
                    value: setCmbLegality()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeLegality() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbLegality() {
            if (data.Legality == null) {
                return "";
            } else {
                if (data.Legality == 0) {
                    return "";
                } else {
                    return data.Legality;
                }
            }
        }



        // COMBO BOX BIT
        $("#CantSubs").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCantSubs,
            value: setCmbCantSubs()
        });
        function OnChangeCantSubs() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCantSubs() {
            if (data.CantSubs == null) {
                return false;
            } else {
                return data.CantSubs;
            }
        }


        // COMBO BOX BIT
        $("#CantRedempt").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCantRedempt,
            value: setCmbCantRedempt()
        });
        function OnChangeCantRedempt() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCantRedempt() {
            if (data.CantRedempt == null) {
                return false;
            } else {
                return data.CantRedempt;
            }
        }


        // COMBO BOX BIT
        $("#CantSwitch").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCantSwitch,
            value: setCmbCantSwitch()
        });
        function OnChangeCantSwitch() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCantSwitch() {
            if (data.CantSwitch == null) {
                return false;
            } else {
                return data.CantSwitch;
            }
        }


        //combo box Agama
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgamaOfficer1").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAgamaOfficer1,
                    value: setCmbAgamaOfficer1()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgamaOfficer1() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        function setCmbAgamaOfficer1() {
            if (data.AgamaOfficer1 == 0) {
                return "";
            }
            else {
                return data.AgamaOfficer1;
            }
        }


        //combo box Agama
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgamaOfficer2").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAgamaOfficer2,
                    value: setCmbAgamaOfficer2()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgamaOfficer2() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        function setCmbAgamaOfficer2() {
            if (data.AgamaOfficer2 == 0) {
                return "";
            }
            else {
                return data.AgamaOfficer2;
            }
        }


        //combo box Agama
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgamaOfficer3").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAgamaOfficer4,
                    value: setCmbAgamaOfficer3()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgamaOfficer3() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        function setCmbAgamaOfficer3() {
            if (data.AgamaOfficer3 == 0) {
                return "";
            }
            else {
                return data.AgamaOfficer3;
            }
        }


        //combo box Agama
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Religion",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgamaOfficer4").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAgamaOfficer4,
                    value: setCmbAgamaOfficer4()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgamaOfficer4() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        function setCmbAgamaOfficer4() {
            if (data.AgamaOfficer4 == 0) {
                return "";
            }
            else {
                return data.AgamaOfficer4;
            }
        }
        $("#BitisTA").prop('checked', data.BitisTA);


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
            if (data.PolitisRelation == null) {
                return "";
            } else {
                if (data.PolitisRelation == 0) {
                    return "";
                } else {
                    return data.PolitisRelation;
                }
            }
        }


        win.center();
        win.open();
    }





    var GlobValidator = $("#WinFundClient").kendoValidator().data("kendoValidator");

    function validateData() {

        if (_GlobClientCode == "07") {
            $("#SellingAgentPK").attr("required", false);
            $("#OtherOccupation").attr("required", false);
            $("#OtherPendidikan").attr("required", false);
            $("#OtherSourceOfFunds").attr("required", false);
            $("#OtherInvestmentObjectives").attr("required", false);
            $("#OtherSpouseOccupation").attr("required", false);
            $("#SpouseNatureOfBusinessOther").attr("required", false);

        }
        if ($('#InvestorType').val() == 1) {
            if ($('#StatementType').val() != 0 || $('#StatementType').val() != "") {
                if ($('#StatementType').val() == 1) {
                    $("#Email").attr("required", false);
                }
                else {
                    $("#Email").attr("required", true);
                }

            }

            if (_GlobClientCode == "25") {

                if ($('#InvestorType').val() == 1) {
                    if ($("#OtherNegaraInd1").val() == 0 || $("#OtherNegaraInd1").val() == null) {
                        $("#OtherNegaraInd1").val("");
                    }
                }
            }
            if (_GlobClientCode == "08") {
                $("#CapitalPaidIn").attr("required", false);
                $("#TotalAsset").attr("required", false);
            }
        }

        if ($('#InvestorType').val() == 2) {

            if (_GlobClientCode == "08") {
                if ($("#CapitalPaidIn").val() == 0 || $("#CapitalPaidIn").val() == null) {
                    $("#CapitalPaidIn").val("");
                }
                $("#CapitalPaidIn").attr("required", true);
                $("#TotalAsset").attr("required", true);
            }
        }

        if (_GlobClientCode == "25") {
            $("#Email").attr("required", false)
        }

        if ($("#SACode").val().length >= 30) {
            alertify.alert("Validation not Pass, char more than 30 for SACode");
            return 0;
        }
        if ($("#SID").val().length > 15) {
            alertify.alert("Validation not Pass, char more than 15 for SID");
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
        if ($("#AlamatInd1").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Correspondence Address (1)");
            return 0;
        }
        if ($("#AlamatInd2").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Correspondence Address (2)");
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
        if ($("#NatureOfBusinessLainnya").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for N.O.B (Text)");
            return 0;
        }
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

        if ($("#NoIdentitasOfficer1").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (1)");
            return 0;
        }
        if ($("#NoIdentitasOfficer2").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (2)");
            return 0;
        }
        if ($("#NoIdentitasOfficer3").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Identity Number (3)");
            return 0;
        }
        if ($("#NoIdentitasOfficer4").val().length > 50) {
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
        if ($("#Email").val().length > 256) {
            alertify.alert("Validation not Pass, char more than 256 for Email");
            return 0;
        }
        if ($("#TeleponRumah").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Phone");
            return 0;
        }


        if ($("#TeleponSelular").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Cell Phone");
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

        if ($("#TeleponKantor").val().length > 30) {
            alertify.alert("Validation not Pass, char more than 30 for Phone");
            return 0;
        }

        if ($("#NamaPerusahaan").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Company Name");
            return 0;
        }
        if ($("#AlamatPerusahaan").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Address");
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
        if ($("#NoSKD").val().length > 40) {
            alertify.alert("Validation not Pass, char more than 40 for SKD Number ");
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
        if ($("#NomorSIUP").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for SIUP Number");
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

        if ($("#NomorRekening1").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Bank Account No 1");
            return 0;
        }
        if ($("#NamaNasabah1").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 1");
            return 0;
        }
        if ($("#BICCode1Name").val().length > 30) {
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



        if ($("#NomorRekening2").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Bank Account No 2");
            return 0;
        }
        if ($("#NamaNasabah2").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 2");
            return 0;
        }
        if ($("#BICCode2Name").val().length > 30) {
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



        if ($("#NomorRekening3").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for Bank Account No 3");
            return 0;
        }
        if ($("#NamaNasabah3").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Account Holder Name 3");
            return 0;
        }
        if ($("#BICCode3Name").val().length > 30) {
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

    function CheckSACode() {
        return 1;

    }


    function ShowAllTab() {
        //console.log(_GlobClientCode);
        $(GlobTabStrip.items()[0]).show();
        $(GlobTabStrip.items()[1]).show();
        $(GlobTabStrip.items()[2]).show();
        $(GlobTabStrip.items()[3]).show();
        $(GlobTabStrip.items()[4]).show();
        $(GlobTabStrip.items()[5]).show();
        $(GlobTabStrip.items()[6]).show();


        if (_GlobClientCode == "07") {
            $(GlobTabStrip.items()[7]).show();
            $(GlobTabStrip.items()[10]).show();
            $("#lblByPass").show();
            $("#lblDckData").show();
        }
        if (_GlobClientCode == "08") {
            $("#lblMigrationStatus").show();
            $("#lblSegmentClass").show();
            $("#lblCompanyTypeOJK").show();
        }
        else {
            $("#lblMigrationStatus").hide();
            $("#lblSegmentClass").hide();
            $("#lblCompanyTypeOJK").hide();
        }

        if (_GlobClientCode == "09") {
            $("#lblLegality").show();
            $("#lblRenewingDate").show();
        }

        else {
            $("#lblLegality").hide();
            $("#lblRenewingDate").hide();
        }

        if (_GlobClientCode == "10") {
            $("#lblRiskProfileScore").hide();
            $("#LblBitShareAbleToGroup").show();
            $("#lblRemarkBank1").show();
            $("#lblRemarkBank2").show();
            $("#lblRemarkBank3").show();
            $("#InvestorsRiskProfile").attr('readonly', false);
        }
        else {
            $("#lblRiskProfileScore").show();
            $("#LblBitShareAbleToGroup").hide();
            $("#lblRemarkBank1").hide();
            $("#lblRemarkBank2").hide();
            $("#lblRemarkBank3").hide();
            $("#InvestorsRiskProfile").attr('readonly', true);
        }

        if (_GlobClientCode == "24") {
            $("#LblBitShareAbleToGroup").show();

        }
        else {
            $("#LblBitShareAbleToGroup").hide();
        }

        if (_GlobClientCode == "25") {
            $(GlobTabStrip.items()[12]).show();
        }

        $(GlobTabStrip.items()[8]).show();
        $(GlobTabStrip.items()[9]).show();
        $(GlobTabStrip.items()[10]).show();
    }

    function ResetTab() {
        $(GlobTabStrip.items()[0]).hide();
        $(GlobTabStrip.items()[1]).hide();
        $(GlobTabStrip.items()[2]).hide();
        $(GlobTabStrip.items()[3]).hide();
        $(GlobTabStrip.items()[4]).hide();
        $(GlobTabStrip.items()[5]).hide();
        $(GlobTabStrip.items()[6]).hide();
        $(GlobTabStrip.items()[7]).hide();
        $(GlobTabStrip.items()[8]).hide();
        $(GlobTabStrip.items()[9]).hide();
        $(GlobTabStrip.items()[10]).hide();
        $(GlobTabStrip.items()[11]).hide();
        $(GlobTabStrip.items()[12]).hide();
    }


    function showDetails(e) {
        initListCountry();
        initListProvince();
        initListCity();
        //initListNationality();
        //initListNationalityOfficer1();
        //initListNationalityOfficer2();
        //initListNationalityOfficer3();
        //initListNationalityOfficer4();


        if (e == null) {
            ResetTab();
            RefreshTab(0);
            ShowAllTab();
            if (_GlobClientCode == "08") {
                $("#lblMigrationStatus").show();
                $("#lblSegmentClass").show();
                $("#lblCompanyTypeOJK").show();
            }
            else {
                $("#lblMigrationStatus").hide();
                $("#lblSegmentClass").hide();
                $("#lblCompanyTypeOJK").hide();
            }

            if (_GlobClientCode == "09") {
                $("#lblLegality").show();
                $("#lblRenewingDate").show();
            }

            else {
                $("#lblLegality").hide();
                $("#lblRenewingDate").hide();
            }


            if (_GlobClientCode == "10") {
                $("#lblRiskProfileScore").hide();
                $("#LblBitShareAbleToGroup").show();
                $("#lblRemarkBank1").show();
                $("#lblRemarkBank2").show();
                $("#lblRemarkBank3").show();
                $("#InvestorsRiskProfile").attr('readonly', false);
                $("#LblEmployerLineOfBusiness").show();
                $("#lblComplRequired").show();
                $("#lblOpeningDateSinvest").show();

            }
            else {
                $("#lblRiskProfileScore").show();
                $("#LblBitShareAbleToGroup").hide();
                $("#lblRemarkBank1").hide();
                $("#lblRemarkBank2").hide();
                $("#lblRemarkBank3").hide();
                $("#InvestorsRiskProfile").attr('readonly', true);
                $("#LblEmployerLineOfBusiness").hide();
                $("#lblComplRequired").hide();
                $("#lblOpeningDateSinvest").hide();
            }

            if (_GlobClientCode == "24") {
                $("#LblBitShareAbleToGroup").show();

            }
            else {
                $("#LblBitShareAbleToGroup").hide();
            }

            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#BtnCopyClient").hide();
            $("#StatusHeader").val("NEW");
            $("#lblKYCUpdate").hide();
            $("#lblBtnEmail").hide();
            $("#lblBtnPreview").hide();
            $("#lblBtnEmailUnitTrustReport").hide();
            $("#lblBtnPreviewUnitTrustReport").hide();
            $("#lblNotifVerifyClient").hide();

            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $(GlobTabStrip.items()[3]).hide();
            $(GlobTabStrip.items()[4]).hide();
            $(GlobTabStrip.items()[5]).show();
            $(GlobTabStrip.items()[6]).show();
            $(GlobTabStrip.items()[8]).hide();
            $(GlobTabStrip.items()[7]).hide();
            $(GlobTabStrip.items()[9]).hide();
            $(GlobTabStrip.items()[10]).hide();
            //$(GlobTabStrip.items()[11]).hide();
            //$(GlobTabStrip.items()[12]).hide();

            ClearAttributes();
            setFundClient(false);
        } else {
            ResetTab();
            ShowAllTab();
            if (_GlobClientCode == '02') {
                $("#lblBtnEmail").show();
                $("#lblBtnPreview").show();
                $("#btnDisplayIdentity1").hide();
            }
            else if (_GlobClientCode == '03') {
                $("#lblBtnEmail").show();
                $("#lblBtnPreview").show();
                $("#btnDisplayIdentity1").hide();
            }
            else {
                $("#lblBtnEmail").hide();
                $("#btnDisplayIdentity1").hide();
                $("#lblBtnPreview").hide();
            }
            if (_GlobClientCode == "08") {
                $("#lblMigrationStatus").show();
                $("#lblSegmentClass").show();
                $("#btnDisplayIdentity1").hide();
                $("#lblCompanyTypeOJK").show();
            }
            else {
                $("#lblMigrationStatus").hide();
                $("#lblSegmentClass").hide();
                $("#btnDisplayIdentity1").hide();
                $("#lblCompanyTypeOJK").hide();
            }
            if (_GlobClientCode == "09") {
                $("#lblLegality").show();
                $("#btnDisplayIdentity1").hide();
                $("#lblRenewingDate").show();
            }

            else {
                $("#lblLegality").hide();
                $("#lblRenewingDate").hide();
            }

            if (_GlobClientCode == "10") {
                $("#lblRiskProfileScore").hide();
                $("#LblBitShareAbleToGroup").show();
                $("#lblRemarkBank1").show();
                $("#lblRemarkBank2").show();
                $("#lblRemarkBank3").show();
                $("#btnDisplayIdentity1").show();
                $("#InvestorsRiskProfile").attr('readonly', false);
                $("#lblOpeningDateSinvest").show();

            }

            else {
                $("#BankVA").hide();
                $("#lblRiskProfileScore").show();
                $("#LblBitShareAbleToGroup").hide();
                $("#lblRemarkBank1").hide();
                $("#lblRemarkBank2").hide();
                $("#lblRemarkBank3").hide();
                $("#btnDisplayIdentity1").hide();
                $("#InvestorsRiskProfile").attr('readonly', true);
                $("#lblOpeningDateSinvest").hide();
            }

            if (_GlobClientCode == "24") {
                $("#LblBitShareAbleToGroup").show();

            }
            else {
                $("#LblBitShareAbleToGroup").hide();
            }

            if (_GlobClientCode == '12') {
                $("#lblBtnPreviewUnitTrustReport").show();
                $("#lblBtnEmailUnitTrustReport").show();
            }
            else {
                $("#lblBtnPreviewUnitTrustReport").hide();
                $("#lblBtnEmailUnitTrustReport").hide();
            }

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
                if (_GlobClientCode == "10") {
                    initListBankVA(dataItemX.FundClientPK);
                }
                $(GlobTabStrip.items()[8]).show();
                $(GlobTabStrip.items()[9]).show();
                $(GlobTabStrip.items()[10]).show();
                $("#lblKYCUpdate").show();
                //RefreshTab(7);
                RefreshTab(0);
                //ResetTab();
                initGridTabSummary(dataItemX.FundClientPK);
                initHistory(dataItemX.FundClientPK);
                initBankList(dataItemX.FundClientPK);
                initBankDefault(dataItemX.FundClientPK);
                initHistoricalSummary(dataItemX.FundClientPK);
                initPositionSummary(dataItemX.FundClientPK);

                if (_GlobClientCode == "10") {
                    $("#lblNotifVerifyClient").show();
                    if (dataItemX.EntryUsersID == "BKLP") {
                        $("#btnDisplayIdentity1").show();
                        $("#BankVA").show();
                        $("#lblBank1").hide();
                        $("#lblBank1Detail").hide();
                        $("#lblBank2").hide();
                        $("#lblBank2Detail").hide();
                        $("#lblBank3").hide();
                        $("#lblBank3Detail").hide();
                        $("#lblBank4").hide();
                        $("#lblBank4Detail").hide();
                    }
                    else {
                        $("#btnDisplayIdentity1").hide();
                        $("#BankVA").hide();
                        $("#lblBank1").show();
                        $("#lblBank1Detail").show();
                        $("#lblBank2").show();
                        $("#lblBank2Detail").show();
                        $("#lblBank3").show();
                        $("#lblBank3Detail").show();
                        $("#lblBank4").show();
                        $("#lblBank4Detail").show();
                    }
                }
                else {
                    $("#lblNotifVerifyClient").hide();
                }




                return;
            }
            else if (tabindex == 1) {
                grid = $("#gridFundClientPending").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                initGridPending(dataItemX.FundClientPK);
                if (_GlobClientCode == "10") {
                    if (dataItemX.EntryUsersID == "BKLP") {
                        $("#btnDisplayIdentity1").show();
                        $("#BankVA").show();
                        $("#lblBank1").hide();
                        $("#lblBank1Detail").hide();
                        $("#lblBank2").hide();
                        $("#lblBank2Detail").hide();
                        $("#lblBank3").hide();
                        $("#lblBank3Detail").hide();
                        $("#lblBank4").hide();
                        $("#lblBank4Detail").hide();
                    }
                    else {
                        $("#btnDisplayIdentity1").hide();
                        $("#BankVA").hide();
                        $("#lblBank1").show();
                        $("#lblBank1Detail").show();
                        $("#lblBank2").show();
                        $("#lblBank2Detail").show();
                        $("#lblBank3").show();
                        $("#lblBank3Detail").show();
                        $("#lblBank4").show();
                        $("#lblBank4Detail").show();
                    }
                    initListBankVA(dataItemX.FundClientPK);
                }
                $(GlobTabStrip.items()[8]).show();
                $(GlobTabStrip.items()[9]).show();
                $(GlobTabStrip.items()[10]).show();
                $("#lblKYCUpdate").hide();
                $("#lblNotifVerifyClient").hide();
                //RefreshTab(7);
                RefreshTab(0);
                //ResetTab();
                initGridTabSummary(dataItemX.FundClientPK);
                initHistory(dataItemX.FundClientPK);
                initBankList(dataItemX.FundClientPK);
                initBankDefault(dataItemX.FundClientPK);
                initHistoricalSummary(dataItemX.FundClientPK);
                initPositionSummary(dataItemX.FundClientPK);
                return;
            }

            else if (tabindex == 2) {
                grid = $("#gridFundClientHistory").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                initGridHistory(dataItemX.FundClientPK, dataItemX.HistoryPK);
                if (_GlobClientCode == "10") {
                    if (dataItemX.EntryUsersID == "BKLP") {
                        $("#btnDisplayIdentity1").show();
                        $("#BankVA").show();
                        $("#lblBank1").hide();
                        $("#lblBank1Detail").hide();
                        $("#lblBank2").hide();
                        $("#lblBank2Detail").hide();
                        $("#lblBank3").hide();
                        $("#lblBank3Detail").hide();
                        $("#lblBank4").hide();
                        $("#lblBank4Detail").hide();
                    }
                    else {
                        $("#btnDisplayIdentity1").hide();
                        $("#BankVA").hide();
                        $("#lblBank1").show();
                        $("#lblBank1Detail").show();
                        $("#lblBank2").show();
                        $("#lblBank2Detail").show();
                        $("#lblBank3").show();
                        $("#lblBank3Detail").show();
                        $("#lblBank4").show();
                        $("#lblBank4Detail").show();
                    }
                    initListBankVA(dataItemX.FundClientPK);
                }
                $(GlobTabStrip.items()[8]).hide();
                $(GlobTabStrip.items()[9]).show();
                $(GlobTabStrip.items()[10]).show();
                $("#lblKYCUpdate").hide();
                $("#lblNotifVerifyClient").hide();
                //RefreshTab(7);
                RefreshTab(0);
                //ResetTab();
                initGridTabSummary(dataItemX.FundClientPK);
                initHistory(dataItemX.FundClientPK);
                initBankList(dataItemX.FundClientPK);
                initBankDefault(dataItemX.FundClientPK);
                initHistoricalSummary(dataItemX.FundClientPK);
                initPositionSummary(dataItemX.FundClientPK);
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
        if (_GlobClientCode != "07") {
            refresh();
        }

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
        $("#OtherOccupation").val("");
        $("#Pendidikan").val("");
        $("#OtherPendidikan").val("");
        $("#Agama").val("");
        $("#OtherAgama").val("");
        $("#PenghasilanInd").val("");
        $("#SumberDanaInd").val("");
        $("#OtherSourceOfFunds").val("");
        $("#MaksudTujuanInd").val("");
        $("#OtherInvestmentObjectives").val("");
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
        $("#NatureOfBusinessLainnya").val("");
        $("#Politis").val("");
        $("#PolitisRelation").val("");
        $("#PolitisLainnya").val("");
        $("#PolitisName").val("");
        $("#PolitisFT").val("");
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
        $("#OtherSpouseOccupation ").val("");
        $("#SpouseNatureOfBusiness").val("");
        $("#SpouseNatureOfBusinessOther").val("");
        $("#SpouseIDNo").val("");
        $("#SpouseNationality").val("");
        $("#SpouseNationalityDesc").val("");
        $("#SpouseAnnualIncome").val("");

        $("#AlamatKantorInd").val("");
        $("#KodeKotaKantorInd").val("");
        $("#KodePosKantorInd").val("");
        $("#KodePropinsiKantorInd").val("");
        $("#KodeCountryofKantor").val("");
        $("#CorrespondenceRT").val("");
        $("#CorrespondenceRW").val("");
        $("#DomicileRT").val("");
        $("#DomicileRW").val("");
        $("#Identity1RT").val("");
        $("#Identity1RW").val("");
        $("#KodeDomisiliPropinsi").val("");
        $("#NamaKantor").val("");
        $("#JabatanKantor").val("");

        $("#KodeDomisiliPropinsi").val("");
        $("#KodeKotaKantorInd").val("");
        $("#KodePropinsiKantorInd").val("");
        $("#KodeCountryofKantor").val("");
        $("#MigrationStatus").val("");
        $("#SegmentClass").val("");
        $("#EmployerLineOfBusiness").val("");
        $("#EmployerLineOfBusinessDesc").val("");
        $("#FrontID").val("");



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
        $("#InternalCategoryPK").val("");
        $("#SellingAgentPK").val("");
        $("#UsersPK").val("");
        $("#SID").val("");
        $("#IFUACode").val("");
        $("#Child").val("");
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
        $("#BICCode1Name").val("");
        $("#ReferralIDFO").val("");
        $("#NamaNasabah1").val("");
        $("#MataUang1").val("");
        $("#OtherCurrency").val("");
        $("#NamaBank2").val("");
        $("#NomorRekening2").val("");
        $("#BankBranchName2").val("");
        $("#BICCode2Name").val("");
        $("#NamaNasabah2").val("");
        $("#MataUang2").val("");
        $("#NamaBank3").val("");
        $("#NomorRekening3").val("");
        $("#BankBranchName3").val("");
        $("#BICCode3Name").val("");
        $("#BankRDNPK").val("");
        $("#RDNBICCode").val("");
        $("#RDNBIMemberCode").val("");
        $("#RDNCurrency").val("");
        $("#NamaNasabah3").val("");
        $("#MataUang3").val("");
        $("#IsFaceToFace").prop('checked', false);
        $("#BitDefaultPayment1").prop('checked', false);
        $("#BitDefaultPayment2").prop('checked', false);
        $("#BitDefaultPayment3").prop('checked', false);
        $("#NamaDepanInd").val("");
        $("#NamaTengahInd").val("");
        $("#NamaBelakangInd").val("");
        $("#TempatLahir").val("");
        $("#TanggalLahir").val("");
        $("#JenisKelamin").val("");
        $("#StatusPerkawinan").val("");
        $("#Pekerjaan").val("");
        $("#OtherOccupation").val("");
        $("#Pendidikan").val("");
        $("#OtherPendidikan").val("");
        $("#Agama").val("");
        $("#OtherAgama").val("");
        $("#PenghasilanInd").val("");
        $("#SumberDanaInd").val("");
        $("#OtherSourceOfFunds").val("");
        $("#CapitalPaidIn").val("");
        $("#MaksudTujuanInd").val("");
        $("#OtherInvestmentObjectives").val("");
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
        $("#OtherTipe").val("");
        $("#Karakteristik").val("");
        $("#OtherCharacteristic").val("");
        $("#NoSKD").val("");
        $("#PenghasilanInstitusi").val("");
        $("#SumberDanaInstitusi").val("");
        $("#OtherSourceOfFundsIns").val("");
        $("#MaksudTujuanInstitusi").val("");
        $("#OtherInvestmentObjectivesIns").val("");
        $("#AlamatPerusahaan").val("");
        $("#KodeKotaIns").val("");
        $("#KodeKotaInsDesc").val("");
        $("#KodePosIns").val("");
        $("#SpouseName").val("");
        $("#MotherMaidenName").val("");
        $("#AhliWaris").val("");
        $("#HubunganAhliWaris").val("");
        $("#NatureOfBusiness").val("");
        $("#NatureOfBusinessLainnya").val("");
        $("#Politis").val("");
        $("#PolitisLainnya").val("");
        $("#PolitisName").val("");
        $("#PolitisFT").val("");
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
        $("#InvestorsRiskProfileDesc").val("");
        $("#KYCRiskProfile").val("");
        $("#KYCRiskProfileDesc").val("");
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
        $("#OtherSpouseOccupation ").val("");
        $("#SpouseNatureOfBusiness").val("");
        $("#SpouseNatureOfBusinessOther").val("");
        $("#SpouseIDNo").val("");
        $("#SpouseNationality").val("");
        $("#SpouseNationalityDesc").val("");
        $("#SpouseAnnualIncome").val("");
        $("#NamaKantor").val("");
        $("#JabatanKantor").val("");
        $("#CompanyFax").val("");
        $("#CompanyMail").val("");
        $("#DatePengkinianData").val("");
        $("#OpeningDateSinvest").val("");
        $("#Legality").val("");
        $("#RenewingDate").val("");
        $("#BitShareAbleToGroup").val("");
        $("#RemarkBank1").val("");
        $("#RemarkBank2").val("");
        $("#RemarkBank3").val("");
        $("#CantSubs").val("");
        $("#CantRedempt").val("");
        $("#CantSwitch").val("");
        $("#ByPassDukcapil").prop('checked', false);

        $("#BeneficialName").val("");
        $("#BeneficialAddress").val("");
        $("#BeneficialIdentity").val("");
        $("#BeneficialWork").val("");
        $("#BeneficialRelation").val("");
        $("#BeneficialHomeNo").val("");
        $("#BeneficialPhoneNumber").val("");
        $("#BeneficialNPWP").val("");
        $("#ClientOnBoard").val("");
        $("#Referral").val("");

        $("#BitisTA").prop('checked', false);

        $("#SelfieImgUrl").val("");
        $("#KtpImgUrl").val("");
    }


    function clearDataIns() {
        $("#NamaPerusahaan").val("");
        $("#Domisili").val("");
        $("#Tipe").val("");
        $("#OtherTipe").val("");
        $("#Karakteristik").val("");
        $("#OtherCharacteristic").val("");
        $("#NoSKD").val("");
        $("#UsersPK").val("");
        $("#PenghasilanInstitusi").val("");
        $("#SumberDanaInstitusi").val("");
        $("#OtherSourceOfFundsIns").val("");
        $("#MaksudTujuanInstitusi").val("");
        $("#OtherInvestmentObjectivesIns").val("");
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
        $("#CompanyTypeOJK").val("");

        $("#DOBOfficer1").val("");
        $("#DOBOfficer2").val("");
        $("#DOBOfficer3").val("");
        $("#DOBOfficer4").val("");
        $("#PlaceOfBirthOfficer1").val("");
        $("#PlaceOfBirthOfficer2").val("");
        $("#PlaceOfBirthOfficer3").val("");
        $("#PlaceOfBirthOfficer4").val("");
        $("#AgamaOfficer1").val("");
        $("#AgamaOfficer2").val("");
        $("#AgamaOfficer3").val("");
        $("#AgamaOfficer4").val("");
        $("#AlamatOfficer1").val("");
        $("#AlamatOfficer2").val("");
        $("#AlamatOfficer3").val("");
        $("#AlamatOfficer4").val("");

        $("#IdentityTypeOfficer1").val("");
        $("#IdentityTypeOfficer2").val("");
        $("#IdentityTypeOfficer3").val("");
        $("#IdentityTypeOfficer4").val("");
        $("#NoIdentitasOfficer1").val("");
        $("#NoIdentitasOfficer2").val("");
        $("#NoIdentitasOfficer3").val("");
        $("#NoIdentitasOfficer4").val("");


        $("#TotalAsset").val("");
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

    function onWinGenerateReportClose() {
        $("#ParamCategory").val("");
    }

    function onWinGenerateSInvestClose() {
        $("#ParamCategory").val("");
    }

    function onWinGenerateNKPDClose() {
        $("#ParamDateNKPD").data("kendoDatePicker").value("");
    }

    function onWinGenerateReportClose() {
        $("#ParamCategory").val("");
    }

    function onWinGenerateUnitTrustReportClose() {

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
                            //    Status: { type: "number" },
                            //    StatusDesc: { type: "string" },
                            //    Notes: { type: "string" },
                            //    ID: { type: "string" },
                            //    OldID: { type: "string" },
                            //    Name: { type: "string" },
                            //    ClientCategory: { type: "string" },
                            //    ClientCategoryDesc: { type: "string" },
                            //    InvestorType: { type: "string" },
                            //    InvestorTypeDesc: { type: "string" },
                            //    InternalCategoryPK: { type: "number" },
                            //    InternalCategoryID: { type: "string" },
                            //    SellingAgentPK: { type: "number" },
                            //    SellingAgentID: { type: "string" },
                            //    SID: { type: "string" },
                            //    IFUACode: { type: "string" },
                            //    Child: { type: "boolean" },
                            //    ARIA: { type: "boolean" },
                            //    Registered: { type: "boolean" },
                            //    JumlahDanaAwal: { type: "number" },
                            //    JumlahDanaSaatIniCash: { type: "number" },
                            //    JumlahDanaSaatIni: { type: "number" },
                            //    Negara: { type: "string" },
                            //    NegaraDesc: { type: "string" },
                            //    Nationality: { type: "string" },
                            //    NPWP: { type: "string" },
                            //    SACode: { type: "string" },
                            //    Propinsi: { type: "number" },
                            //    TeleponSelular: { type: "string" },
                            //    Email: { type: "string" },
                            //    Fax: { type: "string" },
                            //    DormantDate: { type: "date" },
                            //    Description: { type: "string" },
                            //    JumlahBank: { type: "number" },
                            //    NamaBank1: { type: "number" },
                            //    NomorRekening1: { type: "string" },
                            //    BICCode1Name: { type: "string" },
                            //    NamaNasabah1: { type: "string" },
                            //    MataUang1: { type: "string" },
                            //    OtherCurrency: { type: "string" },
                            //    NamaBank2: { type: "number" },
                            //    NomorRekening2: { type: "string" },
                            //    BICCode2Name: { type: "string" },
                            //    NamaNasabah2: { type: "string" },
                            //    MataUang2: { type: "string" },
                            //    NamaBank3: { type: "number" },
                            //    NomorRekening3: { type: "string" },
                            //    BICCode3Name: { type: "string" },
                            //    NamaNasabah3: { type: "string" },
                            //    MataUang3: { type: "string" },
                            //    IsFaceToFace: { type: "boolean" },
                            //    BitDefaultPayment1: { type: "boolean" },
                            //    BitDefaultPayment2: { type: "boolean" },
                            //    BitDefaultPayment3: { type: "boolean" },
                            //    KYCRiskProfile: { type: "number" },
                            //    KYCRiskProfileDesc: { type: "string" },
                            //    BankRDNPK: { type: "number" },
                            //    RDNBankBranchName: { type: "string" },
                            //    RDNBICCode: { type: "string" },
                            //    RDNBIMemberCode: { type: "string" },
                            //    RDNCurrency: { type: "string" },
                            //    NamaDepanInd: { type: "string" },
                            //    NamaTengahInd: { type: "string" },
                            //    NamaBelakangInd: { type: "string" },
                            //    TempatLahir: { type: "string" },
                            //    TanggalLahir: { type: "date" },
                            //    JenisKelamin: { type: "number" },
                            //    StatusPerkawinan: { type: "number" },
                            //    Pekerjaan: { type: "number" },
                            //    OtherOccupation: { type: "string" },
                            //    Pendidikan: { type: "number" },
                            //    OtherPendidikan: { type: "string" },
                            //    Agama: { type: "number" },
                            //    OtherAgama: { type: "string" },
                            //    PenghasilanInd: { type: "number" },
                            //    SumberDanaInd: { type: "number" },
                            //    OtherSourceOfFunds: { type: "string" },
                            //    MaksudTujuanInd: { type: "number" },
                            //    OtherInvestmentObjectives: { type: "string" },
                            //    AlamatInd1: { type: "string" },
                            //    KodeKotaInd1: { type: "string" },
                            //    KodeKotaInd1Desc: { type: "string" },
                            //    KodePosInd1: { type: "number" },
                            //    AlamatInd2: { type: "string" },
                            //    KodeKotaInd2: { type: "string" },
                            //    KodeKotaInd2Desc: { type: "string" },
                            //    KodePosInd2: { type: "number" },
                            //    NamaPerusahaan: { type: "string" },
                            //    Domisili: { type: "number" },
                            //    Tipe: { type: "number" },
                            //    OtherTipe: { type: "string" },
                            //    Karakteristik: { type: "number" },
                            //    OtherCharacteristic: { type: "string" },
                            //    NoSKD: { type: "string" },
                            //    PenghasilanInstitusi: { type: "number" },
                            //    SumberDanaInstitusi: { type: "number" },
                            //    OtherSourceOfFundsIns: { type: "number" },
                            //    MaksudTujuanInstitusi: { type: "number" },
                            //    OtherInvestmentObjectivesIns: { type: "string" },
                            //    AlamatPerusahaan: { type: "string" },
                            //    KodeKotaIns: { type: "string" },
                            //    KodeKotaInsDesc: { type: "string" },
                            //    KodePosIns: { type: "number" },
                            //    SpouseName: { type: "string" },
                            //    MotherMaidenName: { type: "string" },
                            //    AhliWaris: { type: "string" },
                            //    HubunganAhliWaris: { type: "string" },
                            //    NatureOfBusiness: { type: "number" },
                            //    NatureOfBusinessLainnya: { type: "string" },
                            //    Politis: { type: "number" },
                            //    PolitisLainnya: { type: "string" },
                            //    TeleponRumah: { type: "string" },
                            //    OtherAlamatInd1: { type: "string" },
                            //    OtherKodeKotaInd1: { type: "string" },
                            //    OtherKodeKotaInd1Desc: { type: "string" },
                            //    OtherKodePosInd1: { type: "number" },
                            //    OtherPropinsiInd1: { type: "number" },
                            //    CountryOfBirth: { type: "number" },
                            //    OtherNegaraInd1: { type: "string" },
                            //    OtherAlamatInd2: { type: "string" },
                            //    OtherKodeKotaInd2: { type: "string" },
                            //    OtherKodeKotaInd2Desc: { type: "string" },
                            //    OtherKodePosInd2: { type: "number" },
                            //    OtherPropinsiInd2: { type: "number" },
                            //    OtherNegaraInd2: { type: "string" },
                            //    OtherAlamatInd3: { type: "string" },
                            //    OtherKodeKotaInd3: { type: "string" },
                            //    OtherKodeKotaInd3Desc: { type: "string" },
                            //    OtherKodePosInd3: { type: "number" },
                            //    OtherPropinsiInd3: { type: "number" },
                            //    OtherNegaraInd3: { type: "string" },
                            //    OtherTeleponRumah: { type: "string" },
                            //    OtherTeleponSelular: { type: "string" },
                            //    OtherEmail: { type: "string" },
                            //    OtherFax: { type: "string" },
                            //    JumlahIdentitasInd: { type: "number" },
                            //    IdentitasInd1: { type: "number" },
                            //    NoIdentitasInd1: { type: "string" },
                            //    RegistrationDateIdentitasInd1: { type: "date" },
                            //    ExpiredDateIdentitasInd1: { type: "date" },
                            //    IdentitasInd2: { type: "number" },
                            //    NoIdentitasInd2: { type: "string" },
                            //    RegistrationDateIdentitasInd2: { type: "date" },
                            //    ExpiredDateIdentitasInd2: { type: "date" },
                            //    IdentitasInd3: { type: "number" },
                            //    NoIdentitasInd3: { type: "string" },
                            //    RegistrationDateIdentitasInd3: { type: "date" },
                            //    ExpiredDateIdentitasInd3: { type: "date" },
                            //    IdentitasInd4: { type: "number" },
                            //    NoIdentitasInd4: { type: "string" },
                            //    RegistrationDateIdentitasInd4: { type: "date" },
                            //    ExpiredDateIdentitasInd4: { type: "date" },
                            //    RegistrationNPWP: { type: "date" },
                            //    ExpiredDateSKD: { type: "date" },
                            //    SIUPExpirationDate: { type: "date" },
                            //    TanggalBerdiri: { type: "date" },
                            //    LokasiBerdiri: { type: "string" },
                            //    TeleponBisnis: { type: "string" },
                            //    NomorAnggaran: { type: "string" },
                            //    NomorSIUP: { type: "string" },
                            //    AssetFor1Year: { type: "string" },
                            //    AssetFor2Year: { type: "string" },
                            //    AssetFor3Year: { type: "string" },
                            //    OperatingProfitFor1Year: { type: "string" },
                            //    OperatingProfitFor2Year: { type: "string" },
                            //    OperatingProfitFor3Year: { type: "string" },
                            //    JumlahPejabat: { type: "number" },
                            //    NamaDepanIns1: { type: "string" },
                            //    NamaTengahIns1: { type: "string" },
                            //    NamaBelakangIns1: { type: "string" },
                            //    Jabatan1: { type: "string" },
                            //    JumlahIdentitasIns1: { type: "number" },
                            //    IdentitasIns11: { type: "string" },
                            //    NoIdentitasIns11: { type: "string" },
                            //    RegistrationDateIdentitasIns11: { type: "date" },
                            //    ExpiredDateIdentitasIns11: { type: "date" },
                            //    IdentitasIns12: { type: "string" },
                            //    NoIdentitasIns12: { type: "string" },
                            //    RegistrationDateIdentitasIns12: { type: "date" },
                            //    ExpiredDateIdentitasIns12: { type: "date" },
                            //    IdentitasIns13: { type: "string" },
                            //    NoIdentitasIns13: { type: "string" },
                            //    RegistrationDateIdentitasIns13: { type: "date" },
                            //    ExpiredDateIdentitasIns13: { type: "date" },
                            //    IdentitasIns14: { type: "string" },
                            //    NoIdentitasIns14: { type: "string" },
                            //    RegistrationDateIdentitasIns14: { type: "date" },
                            //    ExpiredDateIdentitasIns14: { type: "date" },
                            //    NamaDepanIns2: { type: "string" },
                            //    NamaTengahIns2: { type: "string" },
                            //    NamaBelakangIns2: { type: "string" },
                            //    Jabatan2: { type: "string" },
                            //    JumlahIdentitasIns2: { type: "number" },
                            //    IdentitasIns21: { type: "string" },
                            //    NoIdentitasIns21: { type: "string" },
                            //    RegistrationDateIdentitasIns21: { type: "date" },
                            //    ExpiredDateIdentitasIns21: { type: "date" },
                            //    IdentitasIns22: { type: "string" },
                            //    NoIdentitasIns22: { type: "string" },
                            //    RegistrationDateIdentitasIns22: { type: "date" },
                            //    ExpiredDateIdentitasIns22: { type: "date" },
                            //    IdentitasIns23: { type: "string" },
                            //    NoIdentitasIns23: { type: "string" },
                            //    RegistrationDateIdentitasIns23: { type: "date" },
                            //    ExpiredDateIdentitasIns23: { type: "date" },
                            //    IdentitasIns24: { type: "string" },
                            //    NoIdentitasIns24: { type: "string" },
                            //    RegistrationDateIdentitasIns24: { type: "date" },
                            //    ExpiredDateIdentitasIns24: { type: "date" },
                            //    NamaDepanIns3: { type: "string" },
                            //    NamaTengahIns3: { type: "string" },
                            //    NamaBelakangIns3: { type: "string" },
                            //    Jabatan3: { type: "string" },
                            //    JumlahIdentitasIns3: { type: "number" },
                            //    IdentitasIns31: { type: "string" },
                            //    NoIdentitasIns31: { type: "string" },
                            //    RegistrationDateIdentitasIns31: { type: "date" },
                            //    ExpiredDateIdentitasIns31: { type: "date" },
                            //    IdentitasIns32: { type: "string" },
                            //    NoIdentitasIns32: { type: "string" },
                            //    RegistrationDateIdentitasIns32: { type: "date" },
                            //    ExpiredDateIdentitasIns32: { type: "date" },
                            //    IdentitasIns33: { type: "string" },
                            //    NoIdentitasIns33: { type: "string" },
                            //    RegistrationDateIdentitasIns33: { type: "date" },
                            //    ExpiredDateIdentitasIns33: { type: "date" },
                            //    IdentitasIns34: { type: "string" },
                            //    NoIdentitasIns34: { type: "string" },
                            //    RegistrationDateIdentitasIns34: { type: "date" },
                            //    ExpiredDateIdentitasIns34: { type: "date" },
                            //    NamaDepanIns4: { type: "string" },
                            //    NamaTengahIns4: { type: "string" },
                            //    NamaBelakangIns4: { type: "string" },
                            //    Jabatan1: { type: "string" },
                            //    JumlahIdentitasIns4: { type: "number" },
                            //    IdentitasIns41: { type: "string" },
                            //    NoIdentitasIns41: { type: "string" },
                            //    RegistrationDateIdentitasIns41: { type: "date" },
                            //    ExpiredDateIdentitasIns41: { type: "date" },
                            //    IdentitasIns42: { type: "string" },
                            //    NoIdentitasIns42: { type: "string" },
                            //    RegistrationDateIdentitasIns42: { type: "date" },
                            //    ExpiredDateIdentitasIns42: { type: "date" },
                            //    IdentitasIns43: { type: "string" },
                            //    NoIdentitasIns43: { type: "string" },
                            //    RegistrationDateIdentitasIns43: { type: "date" },
                            //    ExpiredDateIdentitasIns43: { type: "date" },
                            //    IdentitasIns44: { type: "string" },
                            //    NoIdentitasIns44: { type: "string" },
                            //    RegistrationDateIdentitasIns44: { type: "date" },
                            //    ExpiredDateIdentitasIns44: { type: "date" },
                            //    BIMemberCode1: { type: "string" },
                            //    BIMemberCode2: { type: "string" },
                            //    BIMemberCode3: { type: "string" },
                            //    PhoneIns1: { type: "string" },
                            //    EmailIns1: { type: "string" },
                            //    PhoneIns2: { type: "string" },
                            //    EmailIns2: { type: "string" },
                            //    InvestorsRiskProfile: { type: "number" },
                            //    InvestorsRiskProfileDesc: { type: "string" },
                            //    AssetOwner: { type: "number" },
                            //    AssetOwnerDesc: { type: "string" },
                            //    StatementType: { type: "number" },
                            //    StatementTypeDesc: { type: "string" },
                            //    FATCA: { type: "number" },
                            //    FATCADesc: { type: "string" },
                            //    TIN: { type: "string" },
                            //    TINIssuanceCountry: { type: "string" },
                            //    TINIssuanceCountryDesc: { type: "string" },
                            //    GIIN: { type: "string" },
                            //    SubstantialOwnerName: { type: "string" },
                            //    SubstantialOwnerAddress: { type: "string" },
                            //    SubstantialOwnerTIN: { type: "string" },
                            //    BankCountry1: { type: "string" },
                            //    BankCountry1Desc: { type: "string" },
                            //    BankCountry2: { type: "string" },
                            //    BankCountry2Desc: { type: "string" },
                            //    BankCountry3: { type: "string" },
                            //    BankCountry3Desc: { type: "string" },
                            //    EntryUsersID: { type: "string" },
                            //    EntryTime: { type: "date" },
                            //    UpdateUsersID: { type: "string" },
                            //    UpdateTime: { type: "date" },
                            //    ApprovedUsersID: { type: "string" },
                            //    ApprovedTime: { type: "date" },
                            //    VoidUsersID: { type: "string" },
                            //    VoidTime: { type: "date" },
                            //    LastUpdate: { type: "date" },
                            //    Timestamp: { type: "string" },
                            //    BitIsSuspend: { type: "boolean" },
                            //    SuspendBy: { type: "string" },
                            //    SuspendTime: { type: "date" },
                            //    UnSuspendBy: { type: "string" },
                            //    UnSuspendTime: { type: "date" },
                            //    SpouseBirthPlace: { type: "string" },
                            //    SpouseDateOfBirth: { type: "date" }, //yang ini
                            //    SpouseOccupation: { type: "number" },
                            //    SpouseOccupationDesc: { type: "string" },
                            //    OtherSpouseOccupation: { type: "string" },
                            //    SpouseNatureOfBusiness: { type: "number" },
                            //    SpouseNatureOfBusinessDesc: { type: "string" },
                            //    SpouseNatureOfBusinessOther: { type: "string" },
                            //    SpouseIDNo: { type: "string" },
                            //    SpouseNationality: { type: "string" },
                            //    SpouseNationalityDesc: { type: "string" },
                            //    SpouseAnnualIncome: { type: "number" },
                            //    DatePengkinianData: { type: "date" },
                            //    SpouseAnnualIncome: { type: "number" },
                            //    SegmentClass: { type: "number" },
                            //    SegmentClassDesc: { type: "string" },
                            //    MigrationStatus: { type: "number" },
                            //    MigrationStatusDesc: { type: "string" },
                            //    CompanyTypeOJK: { type: "number" },
                            //    CompanyTypeOJKDesc: { type: "string" },
                            //    Legality: { type: "number" },
                            //    LegalityDesc: { type: "string" },
                            //    RenewingDate: { type: "date" },
                            //    BitShareAbleToGroup: { type: "boolean" },
                            //    RemarkBank1: { type: "string" },
                            //    RemarkBank2: { type: "string" },
                            //    RemarkBank3: { type: "string" },

                            //    DOBOfficer1: { type: "date" },
                            //    DOBOfficer2: { type: "date" },
                            //    DOBOfficer3: { type: "date" },
                            //    DOBOfficer4: { type: "date" },
                            //    PlaceOfBirthOfficer1: { type: "string" },
                            //    PlaceOfBirthOfficer2: { type: "string" },
                            //    PlaceOfBirthOfficer3: { type: "string" },
                            //    PlaceOfBirthOfficer4: { type: "string" },
                            //    AgamaOfficer1: { type: "string" },
                            //    AgamaOfficer2: { type: "string" },
                            //    AgamaOfficer3: { type: "string" },
                            //    AgamaOfficer4: { type: "string" },
                            //    AlamatOfficer1: { type: "string" },
                            //    AlamatOfficer2: { type: "string" },
                            //    AlamatOfficer3: { type: "string" },
                            //    AlamatOfficer4: { type: "string" },
                            //    BitisTA: { type: "boolean" },

                            //    FrontID: { type: "string" },
                            //    FaceToFaceDate: { type: "date" },
                            //    EmployerLineOfBusiness: { type: "number" },
                            //    EmployerLineOfBusinessDesc: { type: "string" },
                            //    TeleponKantor: { type: "string" },
                            //    NationalityOfficer1: { type: "number" },
                            //    NationalityOfficer1Desc: { type: "string" },
                            //    NationalityOfficer2: { type: "number" },
                            //    NationalityOfficer2Desc: { type: "string" },
                            //    NationalityOfficer3: { type: "number" },
                            //    NationalityOfficer3Desc: { type: "string" },
                            //    NationalityOfficer4: { type: "number" },
                            //    NationalityOfficer4Desc: { type: "string" },

                            //    IdentityTypeOfficer1: { type: "number" },
                            //    IdentityTypeOfficer2: { type: "number" },
                            //    IdentityTypeOfficer3: { type: "number" },
                            //    IdentityTypeOfficer4: { type: "number" },
                            //    NoIdentitasOfficer1: { type: "string" },
                            //    NoIdentitasOfficer2: { type: "string" },
                            //    NoIdentitasOfficer3: { type: "string" },
                            //    NoIdentitasOfficer4: { type: "string" },

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
                            TanggalLahir: { type: "date" },
                            KYCRiskProfile: { type: "number" },
                            FrontID: { type: "string" },
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

    function getDataSummary(_url) {
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
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            UnitAmount: { type: "number" },
                            Nav: { type: "number" },
                            TotalAmount: { type: "number" },

                        }
                    }
                }
            });
    }

    function getDataHistoricalSummary(_url) {
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
                            TradeDate: { type: "date" },
                            FundID: { type: "string" },
                            Type: { type: "string" },
                            CashAmount: { type: "number" },
                            UnitAmount: { type: "number" },
                            TotalAmount: { type: "number" },

                        }
                    }
                }
            });
    }

    function getDataPositionSummary(_url) {
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

                            Date: { type: "date" },
                            FundID: { type: "string" },
                            UnitAmount: { type: "number" },
                            AvgNav: { type: "number" },

                        }
                    }
                }
            });
    }

    function getDataBankList(_url) {
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
                            StatusDesc: { type: "string" },
                            BankPK: { type: "number" },
                            BankID: { type: "string" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            AccountName: { type: "string" },
                            AccountNo: { type: "string" },
                            NoBank: { type: "string" },
                        }
                    }
                }
            });
    }

    function refresh() {

        if (tabindex == 0 || tabindex == undefined) {
            //grid filter
            var gridTesFilter = $("#gridFundClientApproved").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            initGrid();
        }
        if (tabindex == 1) {
            //grid filter
            var gridTesFilter = $("#gridFundClientPending").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            RecalGridPending();
            //var gridPending = $("#gridSettlementInstructionPending").data("kendoGrid");
            //gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            //grid filter
            var gridTesFilter = $("#gridFundClientHistory").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            RecalGridHistory();
            //var gridHistory = $("#gridSettlementInstructionHistory").data("kendoGrid");
            //gridHistory.dataSource.read();
        }
    }

    function initGrid() {

        $("#gridFundClientApproved").empty();
        var _search = "";
        var _selectsearch = "";
        var _paramDateFrom, _paramDateTo = "";
        if ($("#paramValueDateFrom").val() == null || $("#paramValueDateFrom").val() == "" || $("#paramValueDateTo").val() == null || $("#paramValueDateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            _paramDateFrom = kendo.toString($("#paramValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
            _paramDateTo = kendo.toString($("#paramValueDateTo").data("kendoDatePicker").value(), "MM-dd-yyyy");
        }


        if ($("#search").val() == "") {
            _search = "all";
        }
        else {
            _search = $("#search").val();
        }

        if ($("#searchGridData").val() == "") {
            _selectsearch = "all";
            _PageSize = $("#PageSize").val();
        }
        else {
            _selectsearch = $("#searchGridData").val();
            _PageSize = 200;
        }

        //if (GlobUsersClientMode == 1) {
        var FundClientApprovedURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + _search + "/" + _selectsearch + "/" + _paramDateFrom + "/" + _paramDateTo,
            dataSourceApproved = getDataSourceSearch(FundClientApprovedURL);
        //}
        //else
        //{
        //    var FundClientApprovedURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResultViewOnAgentUsers/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + _search,
        //    dataSourceApproved = getDataSourceSearch(FundClientApprovedURL);
        //}

        if (_GlobClientCode == "10") {

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
                    {
                        headerTemplate: "<input type='checkbox' id='chbApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "ComplRequired", title: "Compl Required", width: 100 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "FrontID", title: "Front ID", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", title: "KYC Risk Profile", width: 150 },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]
            }).data("kendoGrid");

        }
        else if (_GlobClientCode == "29") {
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
                    {
                        headerTemplate: "<input type='checkbox' id='chbApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },

                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ComplRequired", title: "Compl Required", width: 100 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", title: "KYC Risk Profile", width: 150 },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                ]
            }).data("kendoGrid");
        }
        else {
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
                    {
                        headerTemplate: "<input type='checkbox' id='chbApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },

                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "ComplRequired", title: "Compl Required", width: 100 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", title: "KYC Risk Profile", width: 150 },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]
            }).data("kendoGrid");
        }

        if (_PageSize != undefined) {
            grid.dataSource.pageSize(_PageSize);
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridFundClientApproved").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourceApproved.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end grid filter


        function gridApprovedOnDataBound() {
            if (_GlobClientCode == "08") {
                var grid = $("#gridFundClientApproved").data("kendoGrid");
                var data = grid.dataSource.data();
                $.each(data, function (i, row) {
                    if (row.BitIsSuspend == true) {
                        $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
                    }
                });
            }
            else {
                var grid = $("#gridFundClientApproved").data("kendoGrid");
                var data = grid.dataSource.data();
                $.each(data, function (i, row) {
                    if (row.BitIsSuspend == true) {
                        $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
                    }
                    if (row.KYCRiskProfile == 2) {
                        $('tr[data-uid="' + row.uid + '"] ').addClass("highRiskRowWaiting");
                    }
                    if (row.KYCRiskProfile == 3) {
                        $('tr[data-uid="' + row.uid + '"] ').addClass("highRiskSuspend");
                    }
                });
            }

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].FundClientPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-selected-approved")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }

        }

        grid.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;

        $('#chbApproved').change(function (ev) {


            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxApproved').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-selected-approved'))) {
                        $(item).click();
                        $(".checkboxApproved").prop('checked', false);
                    }
                } else {
                    if ($(item).closest('tr').is('.k-selected-approved')) {
                        $(item).click();
                        $(".checkboxApproved").prop('checked', true);
                    }
                }
            });

            grid.dataSource.pageSize(oldPageSizeApproved);

            var checked = [];
            for (var i in checkedApproved) {
                if (checkedApproved[i]) {
                    checked.push(i);
                }
            }


        });


        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridFundClientApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.FundClientPK] = checked;
            if (checked) {
                rowA.addClass("k-selected-approved");
                $(".checkboxApproved" + dataItemZ.FundClientPK).prop('checked', true);
            } else {
                rowA.removeClass("k-selected-approved");
                $(".checkboxApproved" + dataItemZ.FundClientPK).prop('checked', false);
            }

        }

        $("#SelectedAllApproved").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {


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
                    alertify.set('notifier', 'position', 'top-center'); alertify.success(_msg + _b);
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
        var _selectsearch = "";
        var _paramDateFrom, _paramDateTo = "";
        if ($("#paramValueDateFrom").val() == null || $("#paramValueDateFrom").val() == "" || $("#paramValueDateTo").val() == null || $("#paramValueDateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            _paramDateFrom = kendo.toString($("#paramValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
            _paramDateTo = kendo.toString($("#paramValueDateTo").data("kendoDatePicker").value(), "MM-dd-yyyy");
        }

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
        var FundClientPendingURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + _search + "/" + _selectsearch + "/" + _paramDateFrom + "/" + _paramDateTo,
            dataSourcePending = getDataSource(FundClientPendingURL);
        //}

        //else {
        //    var FundClientPendingURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResultViewOnAgentUsers/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + _search,
        //    dataSourcePending = getDataSource(FundClientPendingURL);
        //}
        if (_GlobClientCode == "10") {
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
                    { command: { text: "Show", click: showDetails }, title: " ", width: 90 },
                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "ComplRequired", title: "Compl Required", width: 100 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "HistoryPK", title: "HistNo.", width: 95, filterable: false },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "FrontID", title: "Front ID", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", hidden: true, title: "KYC Risk Profile", width: 150 },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]

            });
        }
        else if (_GlobClientCode == "29") {
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
                    { command: { text: "Show", click: showDetails }, title: " ", width: 90 },
                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "HistoryPK", title: "HistNo.", width: 95, filterable: false },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", hidden: true, title: "KYC Risk Profile", width: 150 },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                ]

            });
        }
        else {
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
                    { command: { text: "Show", click: showDetails }, title: " ", width: 90 },
                    { field: "FundClientPK", title: "SysNo.", width: 95 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "HistoryPK", title: "HistNo.", width: 95, filterable: false },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "KYCRiskProfile", hidden: true, title: "KYC Risk Profile", width: 150 },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]

            });
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridFundClientPending").data("kendoGrid");
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
        $("#gridFundClientHistory").empty();
        var _search = "";
        var _selectsearch = "";

        var _paramDateFrom, _paramDateTo = "";
        if ($("#paramValueDateFrom").val() == null || $("#paramValueDateFrom").val() == "" || $("#paramValueDateTo").val() == null || $("#paramValueDateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            _paramDateFrom = kendo.toString($("#paramValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
            _paramDateTo = kendo.toString($("#paramValueDateTo").data("kendoDatePicker").value(), "MM-dd-yyyy");
        }

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
        var FundClientHistoryURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + _search + "/" + _selectsearch + "/" + _paramDateFrom + "/" + _paramDateTo,
            dataSourceHistory = getDataSourceSearch(FundClientHistoryURL);
        //}
        //else
        //{
        //    var FundClientHistoryURL = window.location.origin + "/Radsoft/FundClient/GetDataByFundClientSearchResultViewOnAgentUsers/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + _search,
        //    dataSourceHistory = getDataSourceSearch(FundClientHistoryURL);
        //}

        if (_GlobClientCode == "10") {
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
                    { field: "ComplRequired", title: "Compl Required", width: 100, template: "#= ComplRequired ? 'Yes' : 'No' #" },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "FrontID", title: "Front ID", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]

            });
        }
        else if (_GlobClientCode == "29") {
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
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                ]

            });
        }
        else {
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
                    { field: "FlagFailedSinvest", title: "S-Failed", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: false, width: 120 },
                    { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                    { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                    { field: "ID", title: "ID", width: 150 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "NoIdentitasInd1", title: "No ID", width: 150 },
                    { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                    { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                    { field: "Email", title: "Email", width: 150 },
                    { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "CapitalPaidIn", title: "CapitalPaidIn", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "SuspendBy", title: "Suspend By", width: 200 },
                    //{ field: "SuspendTime", title: "Suspend Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "UnSuspendBy", title: "UnSuspendBy", width: 200 },
                    //{ field: "UnSuspendTime", title: "UnSuspendTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    //{ field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 100 },

                ]

            });
        }


        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridFundClientHistory").data("kendoGrid");
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

    $("#PageSize").change(function () {

        var _PageSize = 100;
        if ($("#PageSize").val() != undefined || $("#PageSize").val() > 0)
            _PageSize = $("#PageSize").val();
        if (tabindex == 0 || tabindex == undefined) {
            var gridTesFilter = $("#gridFundClientApproved").data("kendoGrid");

            if (_PageSize != undefined) {
                gridTesFilter.dataSource.pageSize(_PageSize);
            }
        }
        if (tabindex == 1) {
            var gridTesFilter = $("#gridFundClientPending").data("kendoGrid");

            if (_PageSize != undefined) {
                gridTesFilter.dataSource.pageSize(_PageSize);
            }
        }
        if (tabindex == 2) {
            var gridTesFilter = $("#gridFundClientHistory").data("kendoGrid");

            if (_PageSize != undefined) {
                gridTesFilter.dataSource.pageSize(_PageSize);
            }
        }
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
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        if ($('#SACode').val() != "" || $('#SACode').val() != 0) {
            var val = CheckSACode();
        }
        else {
            var val = validateData();
        }

        var _ValidateKtp, _NoIdentitasInd1;
        _ValidateKtp = "";

        if ($("#NoIdentitasInd1").val() == "undefined" || $("#NoIdentitasInd1").val() == "")
            _NoIdentitasInd1 = "NULL";
        else
            _NoIdentitasInd1 = $("#NoIdentitasInd1").val();


        if (val == 1) {

            if ($("#InvestorType").data("kendoComboBox").value() == 1) {
                clearDataIns();
            }
            else if ($("#InvestorType").data("kendoComboBox").value() == 2) {
                clearDataInd();
            }
            //if ($("#BitDefaultPayment1").prop('checked') == false && $("#BitDefaultPayment2").prop('checked') == false && $("#BitDefaultPayment3").prop('checked') == false) {
            //    alertify.alert("Plase Choose Default Payment Bank First !");
            //    return;
            //}
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/FundClient/FundClient_GenerateNewClientID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ClientCategory").data("kendoComboBox").value() + "/" + "FundClient_GenerateNewClientID",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        $("#ID").val(data);
                    //        alertify.alert("Your New Client ID is " + data);
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundClient/ValidateNoIdentitasInd1/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _NoIdentitasInd1,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data != "") {
                                _ValidateKtp = data;
                            }
                            else
                                _ValidateKtp = "";

                            var FundClient = {
                                ID: $('#ID').val(),
                                Name: $('#Name').val(),
                                ClientCategory: $('#ClientCategory').val(),
                                InvestorType: $('#InvestorType').val(),
                                InternalCategoryPK: $('#InternalCategoryPK').val(),
                                SellingAgentPK: $('#SellingAgentPK').val(),
                                UsersPK: $('#UsersPK').val(),
                                SID: $('#SID').val(),
                                IFUACode: $('#IFUACode').val(),
                                Child: $('#Child').val(),
                                ARIA: $('#ARIA').val(),
                                RiskProfileScore: $("#RiskProfileScore").val(),
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
                                BICCode1Name: $('#BICCode1Name').val(),
                                NamaNasabah1: $('#NamaNasabah1').val(),
                                MataUang1: $('#MataUang1').val(),
                                OtherCurrency: $('#OtherCurrency').val(),
                                NamaBank2: $('#NamaBank2').val(),
                                NomorRekening2: $('#NomorRekening2').val(),
                                BICCode2Name: $('#BICCode2Name').val(),
                                NamaNasabah2: $('#NamaNasabah2').val(),
                                MataUang2: $('#MataUang2').val(),
                                NamaBank3: $('#NamaBank3').val(),
                                NomorRekening3: $('#NomorRekening3').val(),
                                BICCode3Name: $('#BICCode3Name').val(),
                                NamaNasabah3: $('#NamaNasabah3').val(),
                                MataUang3: $('#MataUang3').val(),
                                IsFaceToFace: $('#IsFaceToFace').is(":checked"),
                                BitDefaultPayment1: $('#BitDefaultPayment1').is(":checked"),
                                BitDefaultPayment2: $('#BitDefaultPayment2').is(":checked"),
                                BitDefaultPayment3: $('#BitDefaultPayment3').is(":checked"),
                                KYCRiskProfile: $('#KYCRiskProfile').val(),
                                NamaDepanInd: $('#NamaDepanInd').val(),
                                NamaTengahInd: $('#NamaTengahInd').val(),
                                NamaBelakangInd: $('#NamaBelakangInd').val(),
                                TempatLahir: $('#TempatLahir').val(),
                                TanggalLahir: $('#TanggalLahir').val(),
                                JenisKelamin: $('#JenisKelamin').val(),
                                StatusPerkawinan: $('#StatusPerkawinan').val(),
                                Pekerjaan: $('#Pekerjaan').val(),
                                OtherOccupation: $('#OtherOccupation').val(),
                                Pendidikan: $('#Pendidikan').val(),
                                OtherPendidikan: $('#OtherPendidikan').val(),
                                Agama: $('#Agama').val(),
                                OtherAgama: $('#OtherAgama').val(),
                                PenghasilanInd: $('#PenghasilanInd').val(),
                                SumberDanaInd: $('#SumberDanaInd').val(),
                                OtherSourceOfFunds: $('#OtherSourceOfFunds').val(),
                                CapitalPaidIn: $('#CapitalPaidIn').val(),
                                MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                                OtherInvestmentObjectives: $('#OtherInvestmentObjectives').val(),
                                AlamatInd1: $('#AlamatInd1').val(),
                                KodeKotaInd1: $('#KodeKotaInd1').val(),
                                KodePosInd1: $('#KodePosInd1').val(),
                                AlamatInd2: $('#AlamatInd2').val(),
                                KodeKotaInd2: $('#KodeKotaInd2').val(),
                                KodePosInd2: $('#KodePosInd2').val(),
                                NamaPerusahaan: $('#NamaPerusahaan').val(),
                                Domisili: $('#Domisili').val(),
                                Tipe: $('#Tipe').val(),
                                OtherTipe: $('#OtherTipe').val(),
                                Karakteristik: $('#Karakteristik').val(),
                                OtherCharacteristic: $('#OtherCharacteristic').val(),
                                NoSKD: $('#NoSKD').val(),
                                PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
                                SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
                                OtherSourceOfFundsIns: $('#OtherSourceOfFundsIns').val(),
                                MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
                                OtherInvestmentObjectivesIns: $('#OtherInvestmentObjectivesIns').val(),
                                AlamatPerusahaan: $('#AlamatPerusahaan').val(),
                                KodeKotaIns: $('#KodeKotaIns').val(),
                                KodePosIns: $('#KodePosIns').val(),
                                SpouseName: $('#SpouseName').val(),
                                MotherMaidenName: $('#MotherMaidenName').val(),
                                AhliWaris: $('#AhliWaris').val(),
                                HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                                NatureOfBusiness: $('#NatureOfBusiness').val(),
                                NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                                Politis: $('#Politis').val(),
                                PolitisRelation: $('#PolitisRelation').val(),
                                PolitisLainnya: $('#PolitisLainnya').val(),
                                PolitisName: $('#PolitisName').val(),
                                PolitisFT: $('#PolitisFT').val(),
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
                                RDNBankBranchName: $('#RDNBankBranchName').val(),
                                RDNCurrency: $('#RDNCurrency').val(),

                                NamaKantor: $('#NamaKantor').val(),
                                JabatanKantor: $('#JabatanKantor').val(),
                                OfficePosition: $('#JabatanKantor').val(),

                                SpouseBirthPlace: $('#SpouseBirthPlace').val(), //yang ini
                                SpouseDateOfBirth: $('#SpouseDateOfBirth ').val(),
                                SpouseOccupation: $('#SpouseOccupation').val(),
                                OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                                SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                                SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther').val(),
                                SpouseIDNo: $('#SpouseIDNo').val(),
                                SpouseNationality: $('#SpouseNationality').val(),
                                SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),

                                AlamatKantorInd: $('#AlamatKantorInd').val(),
                                KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                                KodePosKantorInd: $('#KodePosKantorInd').val(),
                                KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                                KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                                CorrespondenceRT: $('#CorrespondenceRT').val(),
                                CorrespondenceRW: $('#CorrespondenceRW').val(),
                                DomicileRT: $('#DomicileRT').val(),
                                DomicileRW: $('#DomicileRW').val(),
                                Identity1RT: $('#Identity1RT').val(),
                                Identity1RW: $('#Identity1RW').val(),
                                KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),

                                CompanyFax: $('#CompanyFax').val(),
                                CompanyMail: $('#CompanyMail').val(),
                                MigrationStatus: $('#MigrationStatus').val(),
                                SegmentClass: $('#SegmentClass').val(),
                                CompanyTypeOJK: $('#CompanyTypeOJK').val(),
                                Legality: $('#Legality').val(),
                                RenewingDate: $('#RenewingDate').val(),
                                BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                                RemarkBank1: $('#RemarkBank1').val(),
                                RemarkBank2: $('#RemarkBank2').val(),
                                RemarkBank3: $('#RemarkBank3').val(),
                                CantSubs: $('#CantSubs').val(),
                                CantRedempt: $('#CantRedempt').val(),
                                CantSwitch: $('#CantSwitch').val(),

                                BeneficialName: $('#BeneficialName').val(),
                                BeneficialAddress: $('#BeneficialAddress').val(),
                                BeneficialIdentity: $('#BeneficialIdentity').val(),
                                BeneficialWork: $('#BeneficialWork').val(),
                                BeneficialRelation: $('#BeneficialRelation').val(),
                                BeneficialHomeNo: $('#BeneficialHomeNo').val(),
                                BeneficialPhoneNumber: $('#BeneficialPhoneNumber').val(),
                                BeneficialNPWP: $('#BeneficialNPWP').val(),
                                ClientOnBoard: $("#ClientOnBoard").val(),
                                Referral: $("#Referral").val(),

                                AlamatOfficer1: $("#AlamatOfficer1").val(),
                                AlamatOfficer2: $("#AlamatOfficer2").val(),
                                AlamatOfficer3: $("#AlamatOfficer3").val(),
                                AlamatOfficer4: $("#AlamatOfficer4").val(),
                                AgamaOfficer1: $("#AgamaOfficer1").val(),
                                AgamaOfficer2: $("#AgamaOfficer2").val(),
                                AgamaOfficer3: $("#AgamaOfficer3").val(),
                                AgamaOfficer4: $("#AgamaOfficer4").val(),
                                PlaceOfBirthOfficer1: $("#PlaceOfBirthOfficer1").val(),
                                PlaceOfBirthOfficer2: $("#PlaceOfBirthOfficer2").val(),
                                PlaceOfBirthOfficer3: $("#PlaceOfBirthOfficer3").val(),
                                PlaceOfBirthOfficer4: $("#PlaceOfBirthOfficer4").val(),
                                DOBOfficer1: $("#DOBOfficer1").val(),
                                DOBOfficer2: $("#DOBOfficer2").val(),
                                DOBOfficer3: $("#DOBOfficer3").val(),
                                DOBOfficer4: $("#DOBOfficer4").val(),

                                FaceToFaceDate: $('#FaceToFaceDate').val(),
                                EmployerLineOfBusiness: $('#EmployerLineOfBusiness').val(),
                                BitisTA: $('#BitisTA').is(":checked"),
                                TeleponKantor: $('#TeleponKantor').val(),
                                NationalityOfficer1: $('#NationalityOfficer1').val(),
                                NationalityOfficer2: $('#NationalityOfficer2').val(),
                                NationalityOfficer3: $('#NationalityOfficer3').val(),
                                NationalityOfficer4: $('#NationalityOfficer4').val(),

                                IdentityTypeOfficer1: $('#IdentityTypeOfficer1').val(),
                                IdentityTypeOfficer2: $('#IdentityTypeOfficer2').val(),
                                IdentityTypeOfficer3: $('#IdentityTypeOfficer3').val(),
                                IdentityTypeOfficer4: $('#IdentityTypeOfficer4').val(),
                                NoIdentitasOfficer1: $('#NoIdentitasOfficer1').val(),
                                NoIdentitasOfficer2: $('#NoIdentitasOfficer2').val(),
                                NoIdentitasOfficer3: $('#NoIdentitasOfficer3').val(),
                                NoIdentitasOfficer4: $('#NoIdentitasOfficer4').val(),

                                SelfieImgUrl: $('#SelfieImgUrl').val(),
                                KtpImgUrl: $('#KtpImgUrl').val(),
                                TotalAsset: $('#TotalAsset').val(),
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_I",
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data + "<br\><br\>  " + _ValidateKtp);
                                    win.close();
                                    //refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                            //        },
                            //        error: function (data) {
                            //            alertify.alert(data.responseText);
                            //        }
                            //    });


                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }// sini batasnya
            });
        }
    });

    $("#BtnUpdate").click(function () {


        if ($('#SACode').val() != "" || $('#SACode').val() != 0) {
            var val = CheckSACode();
        }
        else {
            var val = validateData();
        }

        if (val == 1) {

            if ($("#InvestorType").data("kendoComboBox").value() == 1) {
                clearDataIns();
            }
            else if ($("#InvestorType").data("kendoComboBox").value() == 2) {
                clearDataInd();
            }
            //if ($("#BitDefaultPayment1").prop('checked') == false && $("#BitDefaultPayment2").prop('checked') == false && $("#BitDefaultPayment3").prop('checked') == false) {
            //    alertify.alert("Plase Choose Default Payment Bank First !");
            //    return;
            //}
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {

                if (e) {
                    //CUSTOM
                    if (_GlobClientCode == "10") {

                        alertify.confirm("Is it part of KYC Update (Pengkinian Data)?", function (e) {

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
                                            SellingAgentPK: $('#SellingAgentPK').val(),
                                            UsersPK: $('#UsersPK').val(),
                                            SID: $('#SID').val(),
                                            IFUACode: $('#IFUACode').val(),
                                            Child: $('#Child').val(),
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
                                            BICCode1Name: $('#BICCode1Name').val(),
                                            NamaNasabah1: $('#NamaNasabah1').val(),
                                            MataUang1: $('#MataUang1').val(),
                                            OtherCurrency: $('#OtherCurrency').val(),
                                            NamaBank2: $('#NamaBank2').val(),
                                            NomorRekening2: $('#NomorRekening2').val(),
                                            BICCode2Name: $('#BICCode2Name').val(),
                                            NamaNasabah2: $('#NamaNasabah2').val(),
                                            MataUang2: $('#MataUang2').val(),
                                            NamaBank3: $('#NamaBank3').val(),
                                            NomorRekening3: $('#NomorRekening3').val(),
                                            BICCode3Name: $('#BICCode3Name').val(),
                                            NamaNasabah3: $('#NamaNasabah3').val(),
                                            MataUang3: $('#MataUang3').val(),
                                            IsFaceToFace: $('#IsFaceToFace').is(":checked"),
                                            BitDefaultPayment1: $('#BitDefaultPayment1').is(":checked"),
                                            BitDefaultPayment2: $('#BitDefaultPayment2').is(":checked"),
                                            BitDefaultPayment3: $('#BitDefaultPayment3').is(":checked"),
                                            KYCRiskProfile: $('#KYCRiskProfile').val(),
                                            NamaDepanInd: $('#NamaDepanInd').val(),
                                            NamaTengahInd: $('#NamaTengahInd').val(),
                                            NamaBelakangInd: $('#NamaBelakangInd').val(),
                                            TempatLahir: $('#TempatLahir').val(),
                                            TanggalLahir: $('#TanggalLahir').val(),
                                            JenisKelamin: $('#JenisKelamin').val(),
                                            StatusPerkawinan: $('#StatusPerkawinan').val(),
                                            Pekerjaan: $('#Pekerjaan').val(),
                                            OtherOccupation: $('#OtherOccupation').val(),
                                            Pendidikan: $('#Pendidikan').val(),
                                            OtherPendidikan: $('#OtherPendidikan').val(),
                                            Agama: $('#Agama').val(),
                                            OtherAgama: $('#OtherAgama').val(),
                                            PenghasilanInd: $('#PenghasilanInd').val(),
                                            SumberDanaInd: $('#SumberDanaInd').val(),
                                            OtherSourceOfFunds: $('#OtherSourceOfFunds').val(),
                                            CapitalPaidIn: $('#CapitalPaidIn').val(),
                                            MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                                            OtherInvestmentObjectives: $('#OtherInvestmentObjectives').val(),
                                            AlamatInd1: $('#AlamatInd1').val(),
                                            KodeKotaInd1: $('#KodeKotaInd1').val(),
                                            KodePosInd1: $('#KodePosInd1').val(),
                                            AlamatInd2: $('#AlamatInd2').val(),
                                            KodeKotaInd2: $('#KodeKotaInd2').val(),
                                            KodePosInd2: $('#KodePosInd2').val(),
                                            NamaPerusahaan: $('#NamaPerusahaan').val(),
                                            Domisili: $('#Domisili').val(),
                                            Tipe: $('#Tipe').val(),
                                            OtherTipe: $('#OtherTipe').val(),
                                            Karakteristik: $('#Karakteristik').val(),
                                            OtherCharacteristic: $('#OtherCharacteristic').val(),
                                            NoSKD: $('#NoSKD').val(),
                                            PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
                                            SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
                                            OtherSourceOfFundsIns: $('#OtherSourceOfFundsIns').val(),
                                            MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
                                            OtherInvestmentObjectivesIns: $('#OtherInvestmentObjectivesIns').val(),
                                            AlamatPerusahaan: $('#AlamatPerusahaan').val(),
                                            KodeKotaIns: $('#KodeKotaIns').val(),
                                            KodePosIns: $('#KodePosIns').val(),
                                            SpouseName: $('#SpouseName').val(),
                                            MotherMaidenName: $('#MotherMaidenName').val(),
                                            AhliWaris: $('#AhliWaris').val(),
                                            HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                                            NatureOfBusiness: $('#NatureOfBusiness').val(),
                                            NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                                            Politis: $('#Politis').val(),
                                            PolitisRelation: $('#PolitisRelation').val(),
                                            PolitisLainnya: $('#PolitisLainnya').val(),
                                            PolitisName: $('#PolitisName').val(),
                                            PolitisFT: $('#PolitisFT').val(),
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
                                            RDNBankBranchName: $('#RDNBankBranchName').val(),
                                            RDNCurrency: $('#RDNCurrency').val(),

                                            NPWPPerson2: $('#NPWPPerson2').val(),
                                            SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                                            SpouseDateOfBirth: $('#SpouseDateOfBirth ').val(),
                                            SpouseOccupation: $('#SpouseOccupation').val(),
                                            OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                                            SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                                            SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther ').val(),
                                            SpouseIDNo: $('#SpouseIDNo').val(),
                                            SpouseNationality: $('#SpouseNationality').val(),
                                            SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),

                                            NamaKantor: $('#NamaKantor').val(),
                                            JabatanKantor: $('#JabatanKantor').val(),
                                            OfficePosition: $('#JabatanKantor').val(),

                                            AlamatKantorInd: $('#AlamatKantorInd').val(),
                                            KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                                            KodePosKantorInd: $('#KodePosKantorInd').val(),
                                            KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                                            KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                                            CorrespondenceRT: $('#CorrespondenceRT').val(),
                                            CorrespondenceRW: $('#CorrespondenceRW').val(),
                                            DomicileRT: $('#DomicileRT').val(),
                                            DomicileRW: $('#DomicileRW').val(),
                                            Identity1RT: $('#Identity1RT').val(),
                                            Identity1RW: $('#Identity1RW').val(),
                                            KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),
                                            CompanyFax: $('#CompanyFax').val(),
                                            CompanyMail: $('#CompanyMail').val(),
                                            MigrationStatus: $('#MigrationStatus').val(),
                                            SegmentClass: $('#SegmentClass').val(),
                                            CompanyTypeOJK: $('#CompanyTypeOJK').val(),
                                            Legality: $('#Legality').val(),
                                            RenewingDate: $('#RenewingDate').val(),
                                            BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                                            RemarkBank1: $('#RemarkBank1').val(),
                                            RemarkBank2: $('#RemarkBank2').val(),
                                            RemarkBank3: $('#RemarkBank3').val(),
                                            CantSubs: $('#CantSubs').val(),
                                            CantRedempt: $('#CantRedempt').val(),
                                            CantSwitch: $('#CantSwitch').val(),

                                            BeneficialName: $('#BeneficialName').val(),
                                            BeneficialAddress: $('#BeneficialAddress').val(),
                                            BeneficialIdentity: $('#BeneficialIdentity').val(),
                                            BeneficialWork: $('#BeneficialWork').val(),
                                            BeneficialRelation: $('#BeneficialRelation').val(),
                                            BeneficialHomeNo: $('#BeneficialHomeNo').val(),
                                            BeneficialPhoneNumber: $('#BeneficialPhoneNumber').val(),
                                            BeneficialNPWP: $('#BeneficialNPWP').val(),
                                            ClientOnBoard: $("#ClientOnBoard").val(),
                                            Referral: $("#Referral").val(),

                                            AlamatOfficer1: $("#AlamatOfficer1").val(),
                                            AlamatOfficer2: $("#AlamatOfficer2").val(),
                                            AlamatOfficer3: $("#AlamatOfficer3").val(),
                                            AlamatOfficer4: $("#AlamatOfficer4").val(),
                                            AgamaOfficer1: $("#AgamaOfficer1").val(),
                                            AgamaOfficer2: $("#AgamaOfficer2").val(),
                                            AgamaOfficer3: $("#AgamaOfficer3").val(),
                                            AgamaOfficer4: $("#AgamaOfficer4").val(),
                                            PlaceOfBirthOfficer1: $("#PlaceOfBirthOfficer1").val(),
                                            PlaceOfBirthOfficer2: $("#PlaceOfBirthOfficer2").val(),
                                            PlaceOfBirthOfficer3: $("#PlaceOfBirthOfficer3").val(),
                                            PlaceOfBirthOfficer4: $("#PlaceOfBirthOfficer4").val(),
                                            DOBOfficer1: $("#DOBOfficer1").val(),
                                            DOBOfficer2: $("#DOBOfficer2").val(),
                                            DOBOfficer3: $("#DOBOfficer3").val(),
                                            DOBOfficer4: $("#DOBOfficer4").val(),
                                            FrontID: $("#FrontID").val(),
                                            FaceToFaceDate: $('#FaceToFaceDate').val(),
                                            UpdateUsersID: sessionStorage.getItem("user"),
                                            BitisTA: $('#BitisTA').is(":checked"),

                                            TeleponKantor: $('#TeleponKantor').val(),
                                            NationalityOfficer1: $('#NationalityOfficer1').val(),
                                            NationalityOfficer2: $('#NationalityOfficer2').val(),
                                            NationalityOfficer3: $('#NationalityOfficer3').val(),
                                            NationalityOfficer4: $('#NationalityOfficer4').val(),

                                            IdentityTypeOfficer1: $('#IdentityTypeOfficer1').val(),
                                            IdentityTypeOfficer2: $('#IdentityTypeOfficer2').val(),
                                            IdentityTypeOfficer3: $('#IdentityTypeOfficer3').val(),
                                            IdentityTypeOfficer4: $('#IdentityTypeOfficer4').val(),
                                            NoIdentitasOfficer1: $('#NoIdentitasOfficer1').val(),
                                            NoIdentitasOfficer2: $('#NoIdentitasOfficer2').val(),
                                            NoIdentitasOfficer3: $('#NoIdentitasOfficer3').val(),
                                            NoIdentitasOfficer4: $('#NoIdentitasOfficer4').val(),
                                            StatusPengkinianData: 1,
                                            SelfieImgUrl: $('#SelfieImgUrl').val(),
                                            KtpImgUrl: $('#KtpImgUrl').val(),
                                            TotalAsset: $('#TotalAsset').val(),

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_U",
                                            type: 'POST',
                                            data: JSON.stringify(FundClient),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
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
                        }, function () {
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
                                            SellingAgentPK: $('#SellingAgentPK').val(),
                                            UsersPK: $('#UsersPK').val(),
                                            SID: $('#SID').val(),
                                            IFUACode: $('#IFUACode').val(),
                                            Child: $('#Child').val(),
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
                                            BICCode1Name: $('#BICCode1Name').val(),
                                            NamaNasabah1: $('#NamaNasabah1').val(),
                                            MataUang1: $('#MataUang1').val(),
                                            OtherCurrency: $('#OtherCurrency').val(),
                                            NamaBank2: $('#NamaBank2').val(),
                                            NomorRekening2: $('#NomorRekening2').val(),
                                            BICCode2Name: $('#BICCode2Name').val(),
                                            NamaNasabah2: $('#NamaNasabah2').val(),
                                            MataUang2: $('#MataUang2').val(),
                                            NamaBank3: $('#NamaBank3').val(),
                                            NomorRekening3: $('#NomorRekening3').val(),
                                            BICCode3Name: $('#BICCode3Name').val(),
                                            NamaNasabah3: $('#NamaNasabah3').val(),
                                            MataUang3: $('#MataUang3').val(),
                                            IsFaceToFace: $('#IsFaceToFace').is(":checked"),
                                            BitDefaultPayment1: $('#BitDefaultPayment1').is(":checked"),
                                            BitDefaultPayment2: $('#BitDefaultPayment2').is(":checked"),
                                            BitDefaultPayment3: $('#BitDefaultPayment3').is(":checked"),
                                            KYCRiskProfile: $('#KYCRiskProfile').val(),
                                            NamaDepanInd: $('#NamaDepanInd').val(),
                                            NamaTengahInd: $('#NamaTengahInd').val(),
                                            NamaBelakangInd: $('#NamaBelakangInd').val(),
                                            TempatLahir: $('#TempatLahir').val(),
                                            TanggalLahir: $('#TanggalLahir').val(),
                                            JenisKelamin: $('#JenisKelamin').val(),
                                            StatusPerkawinan: $('#StatusPerkawinan').val(),
                                            Pekerjaan: $('#Pekerjaan').val(),
                                            OtherOccupation: $('#OtherOccupation').val(),
                                            Pendidikan: $('#Pendidikan').val(),
                                            OtherPendidikan: $('#OtherPendidikan').val(),
                                            Agama: $('#Agama').val(),
                                            OtherAgama: $('#OtherAgama').val(),
                                            PenghasilanInd: $('#PenghasilanInd').val(),
                                            SumberDanaInd: $('#SumberDanaInd').val(),
                                            OtherSourceOfFunds: $('#OtherSourceOfFunds').val(),
                                            CapitalPaidIn: $('#CapitalPaidIn').val(),
                                            MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                                            OtherInvestmentObjectives: $('#OtherInvestmentObjectives').val(),
                                            AlamatInd1: $('#AlamatInd1').val(),
                                            KodeKotaInd1: $('#KodeKotaInd1').val(),
                                            KodePosInd1: $('#KodePosInd1').val(),
                                            AlamatInd2: $('#AlamatInd2').val(),
                                            KodeKotaInd2: $('#KodeKotaInd2').val(),
                                            KodePosInd2: $('#KodePosInd2').val(),
                                            NamaPerusahaan: $('#NamaPerusahaan').val(),
                                            Domisili: $('#Domisili').val(),
                                            Tipe: $('#Tipe').val(),
                                            OtherTipe: $('#OtherTipe').val(),
                                            Karakteristik: $('#Karakteristik').val(),
                                            OtherCharacteristic: $('#OtherCharacteristic').val(),
                                            NoSKD: $('#NoSKD').val(),
                                            PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
                                            SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
                                            OtherSourceOfFundsIns: $('#OtherSourceOfFundsIns').val(),
                                            MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
                                            OtherInvestmentObjectivesIns: $('#OtherInvestmentObjectivesIns').val(),
                                            AlamatPerusahaan: $('#AlamatPerusahaan').val(),
                                            KodeKotaIns: $('#KodeKotaIns').val(),
                                            KodePosIns: $('#KodePosIns').val(),
                                            SpouseName: $('#SpouseName').val(),
                                            MotherMaidenName: $('#MotherMaidenName').val(),
                                            AhliWaris: $('#AhliWaris').val(),
                                            HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                                            NatureOfBusiness: $('#NatureOfBusiness').val(),
                                            NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                                            Politis: $('#Politis').val(),
                                            PolitisRelation: $('#PolitisRelation').val(),
                                            PolitisLainnya: $('#PolitisLainnya').val(),
                                            PolitisName: $('#PolitisName').val(),
                                            PolitisFT: $('#PolitisFT').val(),
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
                                            RDNBankBranchName: $('#RDNBankBranchName').val(),
                                            RDNCurrency: $('#RDNCurrency').val(),

                                            NPWPPerson2: $('#NPWPPerson2').val(),
                                            SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                                            SpouseDateOfBirth: $('#SpouseDateOfBirth ').val(),
                                            SpouseOccupation: $('#SpouseOccupation').val(),
                                            OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                                            SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                                            SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther ').val(),
                                            SpouseIDNo: $('#SpouseIDNo').val(),
                                            SpouseNationality: $('#SpouseNationality').val(),
                                            SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),

                                            NamaKantor: $('#NamaKantor').val(),
                                            JabatanKantor: $('#JabatanKantor').val(),
                                            OfficePosition: $('#JabatanKantor').val(),

                                            AlamatKantorInd: $('#AlamatKantorInd').val(),
                                            KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                                            KodePosKantorInd: $('#KodePosKantorInd').val(),
                                            KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                                            KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                                            CorrespondenceRT: $('#CorrespondenceRT').val(),
                                            CorrespondenceRW: $('#CorrespondenceRW').val(),
                                            DomicileRT: $('#DomicileRT').val(),
                                            DomicileRW: $('#DomicileRW').val(),
                                            Identity1RT: $('#Identity1RT').val(),
                                            Identity1RW: $('#Identity1RW').val(),
                                            KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),
                                            CompanyFax: $('#CompanyFax').val(),
                                            CompanyMail: $('#CompanyMail').val(),
                                            MigrationStatus: $('#MigrationStatus').val(),
                                            SegmentClass: $('#SegmentClass').val(),
                                            CompanyTypeOJK: $('#CompanyTypeOJK').val(),
                                            Legality: $('#Legality').val(),
                                            RenewingDate: $('#RenewingDate').val(),
                                            BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                                            RemarkBank1: $('#RemarkBank1').val(),
                                            RemarkBank2: $('#RemarkBank2').val(),
                                            RemarkBank3: $('#RemarkBank3').val(),
                                            CantSubs: $('#CantSubs').val(),
                                            CantRedempt: $('#CantRedempt').val(),
                                            CantSwitch: $('#CantSwitch').val(),

                                            BeneficialName: $('#BeneficialName').val(),
                                            BeneficialAddress: $('#BeneficialAddress').val(),
                                            BeneficialIdentity: $('#BeneficialIdentity').val(),
                                            BeneficialWork: $('#BeneficialWork').val(),
                                            BeneficialRelation: $('#BeneficialRelation').val(),
                                            BeneficialHomeNo: $('#BeneficialHomeNo').val(),
                                            BeneficialPhoneNumber: $('#BeneficialPhoneNumber').val(),
                                            BeneficialNPWP: $('#BeneficialNPWP').val(),
                                            ClientOnBoard: $("#ClientOnBoard").val(),
                                            Referral: $("#Referral").val(),

                                            AlamatOfficer1: $("#AlamatOfficer1").val(),
                                            AlamatOfficer2: $("#AlamatOfficer2").val(),
                                            AlamatOfficer3: $("#AlamatOfficer3").val(),
                                            AlamatOfficer4: $("#AlamatOfficer4").val(),
                                            AgamaOfficer1: $("#AgamaOfficer1").val(),
                                            AgamaOfficer2: $("#AgamaOfficer2").val(),
                                            AgamaOfficer3: $("#AgamaOfficer3").val(),
                                            AgamaOfficer4: $("#AgamaOfficer4").val(),
                                            PlaceOfBirthOfficer1: $("#PlaceOfBirthOfficer1").val(),
                                            PlaceOfBirthOfficer2: $("#PlaceOfBirthOfficer2").val(),
                                            PlaceOfBirthOfficer3: $("#PlaceOfBirthOfficer3").val(),
                                            PlaceOfBirthOfficer4: $("#PlaceOfBirthOfficer4").val(),
                                            DOBOfficer1: $("#DOBOfficer1").val(),
                                            DOBOfficer2: $("#DOBOfficer2").val(),
                                            DOBOfficer3: $("#DOBOfficer3").val(),
                                            DOBOfficer4: $("#DOBOfficer4").val(),
                                            FrontID: $("#FrontID").val(),
                                            FaceToFaceDate: $('#FaceToFaceDate').val(),
                                            UpdateUsersID: sessionStorage.getItem("user"),
                                            BitisTA: $('#BitisTA').is(":checked"),

                                            TeleponKantor: $('#TeleponKantor').val(),
                                            NationalityOfficer1: $('#NationalityOfficer1').val(),
                                            NationalityOfficer2: $('#NationalityOfficer2').val(),
                                            NationalityOfficer3: $('#NationalityOfficer3').val(),
                                            NationalityOfficer4: $('#NationalityOfficer4').val(),

                                            IdentityTypeOfficer1: $('#IdentityTypeOfficer1').val(),
                                            IdentityTypeOfficer2: $('#IdentityTypeOfficer2').val(),
                                            IdentityTypeOfficer3: $('#IdentityTypeOfficer3').val(),
                                            IdentityTypeOfficer4: $('#IdentityTypeOfficer4').val(),
                                            NoIdentitasOfficer1: $('#NoIdentitasOfficer1').val(),
                                            NoIdentitasOfficer2: $('#NoIdentitasOfficer2').val(),
                                            NoIdentitasOfficer3: $('#NoIdentitasOfficer3').val(),
                                            NoIdentitasOfficer4: $('#NoIdentitasOfficer4').val(),
                                            StatusPengkinianData: 0,
                                            SelfieImgUrl: $('#SelfieImgUrl').val(),
                                            KtpImgUrl: $('#KtpImgUrl').val(),
                                            TotalAsset: $('#TotalAsset').val(),

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_U",
                                            type: 'POST',
                                            data: JSON.stringify(FundClient),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
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

                        ).set('labels', { ok: 'Yes', cancel: 'No' });;// sini batasnya 


                    }
                    else {
                        //CORE
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
                                        SellingAgentPK: $('#SellingAgentPK').val(),
                                        UsersPK: $('#UsersPK').val(),
                                        SID: $('#SID').val(),
                                        IFUACode: $('#IFUACode').val(),
                                        Child: $('#Child').val(),
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
                                        BICCode1Name: $('#BICCode1Name').val(),
                                        NamaNasabah1: $('#NamaNasabah1').val(),
                                        MataUang1: $('#MataUang1').val(),
                                        OtherCurrency: $('#OtherCurrency').val(),
                                        NamaBank2: $('#NamaBank2').val(),
                                        NomorRekening2: $('#NomorRekening2').val(),
                                        BICCode2Name: $('#BICCode2Name').val(),
                                        NamaNasabah2: $('#NamaNasabah2').val(),
                                        MataUang2: $('#MataUang2').val(),
                                        NamaBank3: $('#NamaBank3').val(),
                                        NomorRekening3: $('#NomorRekening3').val(),
                                        BICCode3Name: $('#BICCode3Name').val(),
                                        NamaNasabah3: $('#NamaNasabah3').val(),
                                        MataUang3: $('#MataUang3').val(),
                                        IsFaceToFace: $('#IsFaceToFace').is(":checked"),
                                        BitDefaultPayment1: $('#BitDefaultPayment1').is(":checked"),
                                        BitDefaultPayment2: $('#BitDefaultPayment2').is(":checked"),
                                        BitDefaultPayment3: $('#BitDefaultPayment3').is(":checked"),
                                        KYCRiskProfile: $('#KYCRiskProfile').val(),
                                        NamaDepanInd: $('#NamaDepanInd').val(),
                                        NamaTengahInd: $('#NamaTengahInd').val(),
                                        NamaBelakangInd: $('#NamaBelakangInd').val(),
                                        TempatLahir: $('#TempatLahir').val(),
                                        TanggalLahir: $('#TanggalLahir').val(),
                                        JenisKelamin: $('#JenisKelamin').val(),
                                        StatusPerkawinan: $('#StatusPerkawinan').val(),
                                        Pekerjaan: $('#Pekerjaan').val(),
                                        OtherOccupation: $('#OtherOccupation').val(),
                                        Pendidikan: $('#Pendidikan').val(),
                                        OtherPendidikan: $('#OtherPendidikan').val(),
                                        Agama: $('#Agama').val(),
                                        OtherAgama: $('#OtherAgama').val(),
                                        PenghasilanInd: $('#PenghasilanInd').val(),
                                        SumberDanaInd: $('#SumberDanaInd').val(),
                                        OtherSourceOfFunds: $('#OtherSourceOfFunds').val(),
                                        CapitalPaidIn: $('#CapitalPaidIn').val(),
                                        MaksudTujuanInd: $('#MaksudTujuanInd').val(),
                                        OtherInvestmentObjectives: $('#OtherInvestmentObjectives').val(),
                                        AlamatInd1: $('#AlamatInd1').val(),
                                        KodeKotaInd1: $('#KodeKotaInd1').val(),
                                        KodePosInd1: $('#KodePosInd1').val(),
                                        AlamatInd2: $('#AlamatInd2').val(),
                                        KodeKotaInd2: $('#KodeKotaInd2').val(),
                                        KodePosInd2: $('#KodePosInd2').val(),
                                        NamaPerusahaan: $('#NamaPerusahaan').val(),
                                        Domisili: $('#Domisili').val(),
                                        Tipe: $('#Tipe').val(),
                                        OtherTipe: $('#OtherTipe').val(),
                                        Karakteristik: $('#Karakteristik').val(),
                                        OtherCharacteristic: $('#OtherCharacteristic').val(),
                                        NoSKD: $('#NoSKD').val(),
                                        PenghasilanInstitusi: $('#PenghasilanInstitusi').val(),
                                        SumberDanaInstitusi: $('#SumberDanaInstitusi').val(),
                                        OtherSourceOfFundsIns: $('#OtherSourceOfFundsIns').val(),
                                        MaksudTujuanInstitusi: $('#MaksudTujuanInstitusi').val(),
                                        OtherInvestmentObjectivesIns: $('#OtherInvestmentObjectivesIns').val(),
                                        AlamatPerusahaan: $('#AlamatPerusahaan').val(),
                                        KodeKotaIns: $('#KodeKotaIns').val(),
                                        KodePosIns: $('#KodePosIns').val(),
                                        SpouseName: $('#SpouseName').val(),
                                        MotherMaidenName: $('#MotherMaidenName').val(),
                                        AhliWaris: $('#AhliWaris').val(),
                                        HubunganAhliWaris: $('#HubunganAhliWaris').val(),
                                        NatureOfBusiness: $('#NatureOfBusiness').val(),
                                        NatureOfBusinessLainnya: $('#NatureOfBusinessLainnya').val(),
                                        Politis: $('#Politis').val(),
                                        PolitisRelation: $('#PolitisRelation').val(),
                                        PolitisLainnya: $('#PolitisLainnya').val(),
                                        PolitisName: $('#PolitisName').val(),
                                        PolitisFT: $('#PolitisFT').val(),
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
                                        RDNBankBranchName: $('#RDNBankBranchName').val(),
                                        RDNCurrency: $('#RDNCurrency').val(),

                                        NPWPPerson2: $('#NPWPPerson2').val(),
                                        SpouseBirthPlace: $('#SpouseBirthPlace').val(),
                                        SpouseDateOfBirth: $('#SpouseDateOfBirth ').val(),
                                        SpouseOccupation: $('#SpouseOccupation').val(),
                                        OtherSpouseOccupation: $('#OtherSpouseOccupation').val(),
                                        SpouseNatureOfBusiness: $('#SpouseNatureOfBusiness').val(),
                                        SpouseNatureOfBusinessOther: $('#SpouseNatureOfBusinessOther ').val(),
                                        SpouseIDNo: $('#SpouseIDNo').val(),
                                        SpouseNationality: $('#SpouseNationality').val(),
                                        SpouseAnnualIncome: $('#SpouseAnnualIncome').val(),

                                        NamaKantor: $('#NamaKantor').val(),
                                        JabatanKantor: $('#JabatanKantor').val(),
                                        OfficePosition: $('#JabatanKantor').val(),

                                        AlamatKantorInd: $('#AlamatKantorInd').val(),
                                        KodeKotaKantorInd: $('#KodeKotaKantorInd').val(),
                                        KodePosKantorInd: $('#KodePosKantorInd').val(),
                                        KodePropinsiKantorInd: $('#KodePropinsiKantorInd').val(),
                                        KodeCountryofKantor: $('#KodeCountryofKantor').val(),
                                        CorrespondenceRT: $('#CorrespondenceRT').val(),
                                        CorrespondenceRW: $('#CorrespondenceRW').val(),
                                        DomicileRT: $('#DomicileRT').val(),
                                        DomicileRW: $('#DomicileRW').val(),
                                        Identity1RT: $('#Identity1RT').val(),
                                        Identity1RW: $('#Identity1RW').val(),
                                        KodeDomisiliPropinsi: $('#KodeDomisiliPropinsi').val(),
                                        CompanyFax: $('#CompanyFax').val(),
                                        CompanyMail: $('#CompanyMail').val(),
                                        MigrationStatus: $('#MigrationStatus').val(),
                                        SegmentClass: $('#SegmentClass').val(),
                                        CompanyTypeOJK: $('#CompanyTypeOJK').val(),
                                        Legality: $('#Legality').val(),
                                        RenewingDate: $('#RenewingDate').val(),
                                        BitShareAbleToGroup: $('#BitShareAbleToGroup').val(),
                                        RemarkBank1: $('#RemarkBank1').val(),
                                        RemarkBank2: $('#RemarkBank2').val(),
                                        RemarkBank3: $('#RemarkBank3').val(),
                                        CantSubs: $('#CantSubs').val(),
                                        CantRedempt: $('#CantRedempt').val(),
                                        CantSwitch: $('#CantSwitch').val(),

                                        BeneficialName: $('#BeneficialName').val(),
                                        BeneficialAddress: $('#BeneficialAddress').val(),
                                        BeneficialIdentity: $('#BeneficialIdentity').val(),
                                        BeneficialWork: $('#BeneficialWork').val(),
                                        BeneficialRelation: $('#BeneficialRelation').val(),
                                        BeneficialHomeNo: $('#BeneficialHomeNo').val(),
                                        BeneficialPhoneNumber: $('#BeneficialPhoneNumber').val(),
                                        BeneficialNPWP: $('#BeneficialNPWP').val(),
                                        ClientOnBoard: $("#ClientOnBoard").val(),
                                        Referral: $("#Referral").val(),

                                        AlamatOfficer1: $("#AlamatOfficer1").val(),
                                        AlamatOfficer2: $("#AlamatOfficer2").val(),
                                        AlamatOfficer3: $("#AlamatOfficer3").val(),
                                        AlamatOfficer4: $("#AlamatOfficer4").val(),
                                        AgamaOfficer1: $("#AgamaOfficer1").val(),
                                        AgamaOfficer2: $("#AgamaOfficer2").val(),
                                        AgamaOfficer3: $("#AgamaOfficer3").val(),
                                        AgamaOfficer4: $("#AgamaOfficer4").val(),
                                        PlaceOfBirthOfficer1: $("#PlaceOfBirthOfficer1").val(),
                                        PlaceOfBirthOfficer2: $("#PlaceOfBirthOfficer2").val(),
                                        PlaceOfBirthOfficer3: $("#PlaceOfBirthOfficer3").val(),
                                        PlaceOfBirthOfficer4: $("#PlaceOfBirthOfficer4").val(),
                                        DOBOfficer1: $("#DOBOfficer1").val(),
                                        DOBOfficer2: $("#DOBOfficer2").val(),
                                        DOBOfficer3: $("#DOBOfficer3").val(),
                                        DOBOfficer4: $("#DOBOfficer4").val(),
                                        FrontID: $("#FrontID").val(),
                                        FaceToFaceDate: $('#FaceToFaceDate').val(),
                                        UpdateUsersID: sessionStorage.getItem("user"),
                                        BitisTA: $('#BitisTA').is(":checked"),

                                        TeleponKantor: $('#TeleponKantor').val(),
                                        NationalityOfficer1: $('#NationalityOfficer1').val(),
                                        NationalityOfficer2: $('#NationalityOfficer2').val(),
                                        NationalityOfficer3: $('#NationalityOfficer3').val(),
                                        NationalityOfficer4: $('#NationalityOfficer4').val(),

                                        IdentityTypeOfficer1: $('#IdentityTypeOfficer1').val(),
                                        IdentityTypeOfficer2: $('#IdentityTypeOfficer2').val(),
                                        IdentityTypeOfficer3: $('#IdentityTypeOfficer3').val(),
                                        IdentityTypeOfficer4: $('#IdentityTypeOfficer4').val(),
                                        NoIdentitasOfficer1: $('#NoIdentitasOfficer1').val(),
                                        NoIdentitasOfficer2: $('#NoIdentitasOfficer2').val(),
                                        NoIdentitasOfficer3: $('#NoIdentitasOfficer3').val(),
                                        NoIdentitasOfficer4: $('#NoIdentitasOfficer4').val(),
                                        StatusPengkinianData: 0,
                                        SelfieImgUrl: $('#SelfieImgUrl').val(),
                                        KtpImgUrl: $('#KtpImgUrl').val(),
                                        TotalAsset: $('#TotalAsset').val(),


                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_U",
                                        type: 'POST',
                                        data: JSON.stringify(FundClient),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
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
                }
            });
        }
    });

    $("#BtnOldData").click(function () {

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

    function GenerateClientIDAndApproveClient() {

        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/FundClient_GenerateNewClientID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InvestorType").val() + "/" + $("#FundClientPK").val() + "/" + "FundClient_GenerateNewClientID",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _newClientID = "";
                $("#ID").val(data);
                _newClientID = data;

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
                        $.unblockUI();
                        alertify.alert(data);
                        if (_newClientID != "") {
                            alertify.alert("Your new clientID :" + _newClientID);
                        }
                        win.close();

                        //refresh();
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            },
            error: function (data) {
                $.unblockUI();
                alertify.alert(data.responseText);
            }
        });
    }

    $("#BtnApproved").click(function () {
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClient",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            if ($("#ID").val() == null || $("#ID").val() == "" || $("#ID").val() == "000") {
                                // KERJAAN

                                if ($('#InvestorType').val() == "2") {
                                    GenerateClientIDAndApproveClient();
                                }

                                if (_GlobClientCode == "07" && $('#ByPassDukcapil').is(":checked") == false) {

                                    if ($('#NoIdentitasInd1').val() == '' || $('#NoIdentitasInd1').val() == null) {
                                        alertify.alert('NO ID IS EMPTY PLEASE CHECK');
                                        $.unblockUI();
                                        return;
                                    }
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/DukcapilData/CheckData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#NoIdentitasInd1').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == false) {
                                                // Check data ke API 
                                                var dck = {
                                                    nik: $('#NoIdentitasInd1').val(),
                                                    user_id: _DckID,
                                                    password: _DckPassword,
                                                    IP_USER: _DckIP
                                                };
                                                $.ajax({
                                                    url: _DckURL,
                                                    type: 'POST',
                                                    contentType: "application/json;charset=utf-8",
                                                    data: JSON.stringify(dck),
                                                    success: function (data) {
                                                        if (data.content == null) {
                                                            alertify.alert('NO DATA IN DUKCAPIL, CANNOT CONTINUE');
                                                            $.unblockUI();
                                                            return;
                                                        }
                                                        if (data.content[0].RESPON == null) {
                                                            console.log(data.content[0].NIK);
                                                            // Update 3 Field Dan Simpen Data


                                                            var dckUpdateFundClient = {
                                                                FundClientPK: $('#FundClientPK').val(),
                                                                NIK: data.content[0].NIK,
                                                                NAMA_LGKP: data.content[0].NAMA_LGKP,
                                                                TGL_LHR: data.content[0].TGL_LHR,
                                                                JENIS_KLMIN: data.content[0].JENIS_KLMIN
                                                            };

                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/DukcapilData/DukcapilUpdateFundClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                type: 'POST',
                                                                contentType: "application/json;charset=utf-8",
                                                                data: JSON.stringify(dckUpdateFundClient),
                                                                success: function (data) {
                                                                    if (data != null) {
                                                                        alertify.alert(data);
                                                                        GenerateClientIDAndApproveClient();
                                                                    }
                                                                },
                                                                error: function (data) {
                                                                    alertify.alert(data.responseText);
                                                                    $.unblockUI();
                                                                }
                                                            });

                                                        } else {
                                                            alertify.alert('NO DATA IN DUKCAPIL, CANNOT CONTINUE');
                                                            $.unblockUI();
                                                        }

                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                        $.unblockUI();
                                                    }
                                                });

                                            } else {
                                                GenerateClientIDAndApproveClient();
                                            }

                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });

                                } else {
                                    console.log(true);
                                    GenerateClientIDAndApproveClient();
                                }

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
                                        $.unblockUI();
                                        alertify.alert(data);
                                        win.close();

                                        //refresh();
                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        } else {
                            $.unblockUI();
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }

                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnVoid").click(function () {
        alertify.prompt("Are you sure want to Void, please give notes:", "", function (e, str) {
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
                                Notes: str,
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_V",
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {

                                    alertify.alert(data);
                                    win.close();
                                    if (_RDOEnable == true) {
                                        $.ajax({
                                            //unvoid?id=477
                                            url: "http://" + _GlobUrlServerRDOApi + "userprofile/void?id=" + $("#FundClientPK").val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                console.log(data);
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    }
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
        alertify.prompt("Are you sure want to Reject, please give notes:", "", function (e, str) {
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
                                Notes: str,
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_R",
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
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

    $("#BtnGenerateARIAText").click(function () {
        showWinGenerateARIAText();
    });

    // Untuk Form Generate

    function showWinGenerateARIAText() {
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

    $("#BtnOkGenerateARIAText").click(function () {


        alertify.confirm("Are you sure want to Generate Report ?", function (e) {
            if (e) {
                $.blockUI({});
                var GenerateAria = {
                    InvestorType: $("#ParamInvestorType").val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/FundClientByInvestorTypeReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(GenerateAria),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        var newwindow = window.open(data, '_blank');
                        //window.location = data
                        $.unblockUI();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }

                });
            }
        });

    });

    $("#BtnCancelGenerateARIAText").click(function () {

        alertify.confirm("Are you sure want to close Generate Report?", function (e) {
            if (e) {
                WinGenerateARIAText.close();
                alertify.alert("Close Generate Report");
            }
        });
    });

    $("#BtnGenerateSInvest").click(function () {
        showWinGenerateSInvest();
    });

    // Untuk Form Generate

    function showWinGenerateSInvest() {
        //if (_GlobClientCode == '99')
        //{
        //    $("#LblTypeClient").show();
        //}
        //else
        //{
        //    $("#LblTypeClient").hide();
        //}

        $("#ParamInvestorType").kendoComboBox({
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

        $("#ParamFundClientPKFrom").kendoNumericTextBox({
            format: "n0"

        });


        $("#ParamFundClientPKTo").kendoNumericTextBox({
            format: "n0"

        });

        $("#ParamType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "New", value: 1 },
                { text: "Amendment", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeParamType,
            index: 0

        });

        function OnChangeParamType() {
            if ($("#ParamType").val() == 2 && _GlobClientCode == "29") {
                $("#LblSinvestDateFrom").show();
                $("#LblSinvestDateTo").show();
            }
            else {
                $("#LblSinvestDateFrom").hide();
                $("#LblSinvestDateTo").hide();

            }
        }

        WinGenerateSInvest.center();
        WinGenerateSInvest.open();

    }

    $("#BtnOkSInvest").click(function () {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        alertify.confirm("Are you sure want to Generate S-Invest ?", function (e) {
            if (e) {

                if ($('#ParamFundClientPKFrom').val() == "0" || $('#ParamFundClientPKFrom').val() == null || $('#ParamFundClientPKFrom').val() == "") {
                    ParamFundClientPKFrom = "0";
                }
                else {
                    ParamFundClientPKFrom = $('#ParamFundClientPKFrom').val()
                }


                if ($('#ParamFundClientPKTo').val() == "0" || $('#ParamFundClientPKTo').val() == null || $('#ParamFundClientPKTo').val() == "") {
                    ParamFundClientPKTo = "0";
                }
                else {
                    ParamFundClientPKTo = $('#ParamFundClientPKTo').val()
                }


                if ($('#ParamType').val() == "0" || $('#ParamType').val() == null || $('#ParamType').val() == "") {
                    ParamType = "0";
                }
                else {
                    ParamType = $('#ParamType').val()
                }



                $.blockUI({});

                var FundClient = {
                    FundClientSelected: stringFundClientFrom,
                    ParamSInvestDateFrom: $('#ParamSInvestDateFrom').val(),
                    ParamSInvestDateTo: $('#ParamSInvestDateTo').val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/GenerateSInvest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamInvestorType").val() + "/" + ParamFundClientPKFrom + "/" + ParamFundClientPKTo + "/" + ParamType,
                    type: 'POST',
                    data: JSON.stringify(FundClient),
                    close: onPopUpClose,
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadSInvest").attr("href", data);
                        $("#downloadSInvest").attr("download", "RadsoftSInvestFile_ClientData.txt");
                        document.getElementById("downloadSInvest").click();

                        if (_GlobClientCode == "10") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/CheckSInvestByHighRiskMonitoring/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamInvestorType").val(),
                                type: 'POST',
                                data: JSON.stringify(FundClient),
                                close: onPopUpClose,
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data)
                                },
                                error: function (data) {
                                    alertify.alert("Error : CheckSInvestByHighRiskMonitoring, Please Contact Administrator!");
                                }

                            });
                        }
                        $.unblockUI();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }

                });
            }
        });

    });

    $("#BtnCancelSInvest").click(function () {

        alertify.confirm("Are you sure want to close Generate Report?", function (e) {
            if (e) {
                WinGenerateSInvest.close();
                alertify.alert("Close Generate Report");
            }
        });
    });

    $("#BtnGenerateNKPD").click(function () {
        showWinGenerateNKPD();
    });

    // Untuk Form Generate

    function showWinGenerateNKPD() {


        WinGenerateNKPD.center();
        WinGenerateNKPD.open();

    }

    $("#BtnOkGenerateNKPD").click(function () {


        alertify.confirm("Are you sure want to Generate ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/GenerateNKPD/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateNKPD").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadSInvest").attr("href", data);
                        $("#downloadSInvest").attr("download", "Radsoft_NKPD.txt");
                        document.getElementById("downloadSInvest").click();
                        //window.location = data
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }

                });
            }
        });

    });

    $("#BtnCancelGenerateNKPD").click(function () {

        alertify.confirm("Are you sure want to close Generate?", function (e) {
            if (e) {
                WinGenerateNKPD.close();
                alertify.alert("Close Generate");
            }
        });
    });

    function RefreshTab(_index) {
        //$("#TabSub").data("kendoTabStrip").select(_index);
        GlobTabStrip.select(_index);
    }

    function RequiredAttributes(_investorType) {
        ClearAttributes();



        if (_investorType == 1) {

            $("#NamaDepanInd").attr("required", true);
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
            $("#Nationality").attr("required", true);
            $("#IdentitasInd1").attr("required", true);
            $("#NoIdentitasInd1").attr("required", true);
            $("#OtherKodeKotaInd1").attr("required", true);
            $("#OtherAlamatInd1").attr("required", true);
            $("#Email").attr("required", true);

            if (_GlobClientCode == "25") {
                $("#OtherNegaraInd1").attr("required", true);
            }
        }
        else if (_investorType == 2) {

            $("#Negara").attr("required", true);
            $("#NamaPerusahaan").attr("required", true);
            $("#Domisili").attr("required", true);
            $("#Tipe").attr("required", true);
            $("#Karakteristik").attr("required", true);
            $("#PenghasilanInstitusi").attr("required", true);
            $("#SumberDanaInstitusi").attr("required", true);
            $("#MaksudTujuanInstitusi").attr("required", true);
            $("#AlamatPerusahaan").attr("required", true);
            $("#KodeKotaIns").attr("required", true);
            $("#TanggalBerdiri").attr("required", true);
            $("#LokasiBerdiri").attr("required", true);
            $("#NamaDepanIns1").attr("required", true);
            $("#IdentitasIns11").attr("required", true);
            $("#NoIdentitasIns11").attr("required", true);

            if (_GlobClientCode == "08") {
                $("#CapitalPaidIn").attr("required", true);
                $("#TotalAsset").attr("required", true);
            }
        }
    }

    function ClearAttributes() {
        $("#NamaDepanInd").attr("required", false);
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
        $("#Nationality").attr("required", false);
        $("#IdentitasInd1").attr("required", false);
        $("#NoIdentitasInd1").attr("required", false);
        $("#Negara").attr("required", false);
        $("#NamaPerusahaan").attr("required", false);
        $("#Domisili").attr("required", false);
        $("#Tipe").attr("required", false);
        $("#Karakteristik").attr("required", false);
        $("#PenghasilanInstitusi").attr("required", false);
        $("#SumberDanaInstitusi").attr("required", false);
        $("#MaksudTujuanInstitusi").attr("required", false);
        $("#AlamatPerusahaan").attr("required", false);
        $("#KodeKotaIns").attr("required", false);
        $("#TanggalBerdiri").attr("required", false);
        $("#LokasiBerdiri").attr("required", false);
        $("#NamaDepanIns1").attr("required", false);
        $("#IdentitasIns11").attr("required", false);
        $("#NoIdentitasIns11").attr("required", false);
        $("#OtherKodeKotaInd1").attr("required", false);
        $("#OtherAlamatInd1").attr("required", false);
        $("#Email").attr("required", false);
        $("#CapitalPaidIn").attr("required", false);
        $("#TotalAsset").attr("required", false);
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

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });

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

    $("#btnListOtherKodeKotaInd2").click(function () {
        WinListCity.center();
        WinListCity.open();

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


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

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


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

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


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

        var element = $(this);
        var _obj = element.offset();
        WinListCity.wrapper.css({ top: parseInt(_obj.top) });


        htmlCityPK = "#CompanyCityName";
        htmlCityDesc = "#CompanyCityNameDesc";
    });

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

        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });


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

    $("#btnCountryofCorrespondence").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

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

    $("#btnListOtherNegaraInd2").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

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


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

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


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

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

    $("#btnListBankRDNCountry").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#BankRDNCountry";
        htmlCountryDesc = "#BankRDNCountryDesc";
    });
    $("#btnClearListBankRDNCountry").click(function () {
        $("#BankRDNCountry").val("");
        $("#BankRDNCountryDesc").val("");
    });


    $("#btnCountryofEstablishment").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#CountryofEstablishment";
        htmlCountryDesc = "#CountryofEstablishmentDesc";
    });

    $("#btnCountryofCompany").click(function () {
        WinListCountry.center();
        WinListCountry.open();


        var element = $(this);
        var _obj = element.offset();
        WinListCountry.wrapper.css({ top: parseInt(_obj.top) });

        htmlCountry = "#CountryofCompany";
        htmlCountryDesc = "#CountryofCompanyDesc";
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


        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });

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

    $("#btnListOtherPropinsiInd2").click(function () {
        WinListProvince.center();
        WinListProvince.open();


        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });

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

        var element = $(this);
        var _obj = element.offset();
        WinListProvince.wrapper.css({ top: parseInt(_obj.top) });
        htmlProvince = "#OtherPropinsiInd3";
        htmlProvinceDesc = "#OtherPropinsiInd3Desc";
    });
    $("#btnClearListOtherPropinsiInd3").click(function () {
        $("#OtherPropinsiInd3").val("");
        $("#OtherPropinsiInd3Desc").val("");
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

    //function getDataSourceListNationality() {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 25,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         Code: { type: "string" },
    //                         DescOne: { type: "string" },

    //                     }
    //                 }
    //             }
    //         });
    //}

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


    //function getDataSourceListNationalityOfficer1() {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 25,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         Code: { type: "string" },
    //                         DescOne: { type: "string" },

    //                     }
    //                 }
    //             }
    //         });
    //}

    $("#btnListNationalityOfficer1").click(function () {
        initListNationalityOfficer1();
        WinListNationalityOfficer1.center();
        WinListNationalityOfficer1.open();

        var element = $(this);
        var _obj = element.offset();
        WinListNationalityOfficer1.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationalityOfficer1 = "#NationalityOfficer1";
        htmlNationalityOfficer1Desc = "#NationalityOfficer1Desc";

    });
    $("#btnClearListNationalityOfficer1").click(function () {
        $("#NationalityOfficer1").val("");
        $("#NationalityOfficer1Desc").val("");
    });


    //function getDataSourceListNationalityOfficer2() {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 25,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         Code: { type: "string" },
    //                         DescOne: { type: "string" },

    //                     }
    //                 }
    //             }
    //         });
    //}

    $("#btnListNationalityOfficer2").click(function () {
        initListNationalityOfficer2();
        WinListNationalityOfficer2.center();
        WinListNationalityOfficer2.open();

        var element = $(this);
        var _obj = element.offset();
        WinListNationalityOfficer2.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationalityOfficer2 = "#NationalityOfficer2";
        htmlNationalityOfficer2Desc = "#NationalityOfficer2Desc";

    });
    $("#btnClearListNationalityOfficer2").click(function () {
        $("#NationalityOfficer2").val("");
        $("#NationalityOfficer2Desc").val("");
    });

    //function getDataSourceListNationalityOfficer3() {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 25,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         Code: { type: "string" },
    //                         DescOne: { type: "string" },

    //                     }
    //                 }
    //             }
    //         });
    //}

    $("#btnListNationalityOfficer3").click(function () {
        initListNationalityOfficer3();
        WinListNationalityOfficer3.center();
        WinListNationalityOfficer3.open();

        var element = $(this);
        var _obj = element.offset();
        WinListNationalityOfficer3.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationalityOfficer3 = "#NationalityOfficer3";
        htmlNationalityOfficer3Desc = "#NationalityOfficer3Desc";

    });
    $("#btnClearListNationalityOfficer3").click(function () {
        $("#NationalityOfficer3").val("");
        $("#NationalityOfficer3Desc").val("");
    });

    //function getDataSourceListNationalityOfficer4() {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 25,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         Code: { type: "string" },
    //                         DescOne: { type: "string" },

    //                     }
    //                 }
    //             }
    //         });
    //}

    $("#BtnShowVA").click(function () {
        WinListBankVA.center();
        WinListBankVA.open();

        var element = $(this);
        var _obj = element.offset();
        WinListBankVA.wrapper.css({ top: parseInt(_obj.top) });
        htmlBankVA = "#BankVA";
        htmlBankVADesc = "#BankVADesc";

    });

    $("#btnListNationalityOfficer4").click(function () {
        initListNationalityOfficer4();
        WinListNationalityOfficer4.center();
        WinListNationalityOfficer4.open();

        var element = $(this);
        var _obj = element.offset();
        WinListNationalityOfficer4.wrapper.css({ top: parseInt(_obj.top) });
        htmlNationalityOfficer4 = "#NationalityOfficer4";
        htmlNationalityOfficer4Desc = "#NationalityOfficer4Desc";

    });
    $("#btnClearListNationalityOfficer4").click(function () {
        $("#NationalityOfficer4").val("");
        $("#NationalityOfficer4Desc").val("");
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

    function initListNationalityOfficer1() {
        var dsListNationalityOfficer1 = getDataSourceListCountry();
        $("#gridListNationalityOfficer1").empty();
        $("#gridListNationalityOfficer1").kendoGrid({
            dataSource: dsListNationalityOfficer1,
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
                { command: { text: "Select", click: ListNationalityOfficer1Select }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "NationalityOfficer1", width: 200 }
            ]
        });
    }

    function ListNationalityOfficer1Select(e) {
        e.preventDefault();
        var grid = $("#gridListNationalityOfficer1").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityOfficer1Desc).val(dataItemX.DescOne);
        $(htmlNationalityOfficer1).val(dataItemX.Code);
        WinListNationalityOfficer1.close();

    }

    function initListNationalityOfficer2() {
        var dsListNationalityOfficer2 = getDataSourceListCountry();
        $("#gridListNationalityOfficer2").empty();
        $("#gridListNationalityOfficer2").kendoGrid({
            dataSource: dsListNationalityOfficer2,
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
                { command: { text: "Select", click: ListNationalityOfficer2Select }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "NationalityOfficer2", width: 200 }
            ]
        });
    }

    function ListNationalityOfficer2Select(e) {
        e.preventDefault();
        var grid = $("#gridListNationalityOfficer2").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityOfficer2Desc).val(dataItemX.DescOne);
        $(htmlNationalityOfficer2).val(dataItemX.Code);
        WinListNationalityOfficer2.close();

    }

    function initListNationalityOfficer3() {
        var dsListNationalityOfficer3 = getDataSourceListCountry();
        $("#gridListNationalityOfficer3").empty();
        $("#gridListNationalityOfficer3").kendoGrid({
            dataSource: dsListNationalityOfficer3,
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
                { command: { text: "Select", click: ListNationalityOfficer3Select }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "NationalityOfficer3", width: 200 }
            ]
        });
    }

    function ListNationalityOfficer3Select(e) {
        e.preventDefault();
        var grid = $("#gridListNationalityOfficer3").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityOfficer3Desc).val(dataItemX.DescOne);
        $(htmlNationalityOfficer3).val(dataItemX.Code);
        WinListNationalityOfficer3.close();

    }

    function initListNationalityOfficer4() {
        var dsListNationalityOfficer4 = getDataSourceListCountry();
        $("#gridListNationalityOfficer4").empty();
        $("#gridListNationalityOfficer4").kendoGrid({
            dataSource: dsListNationalityOfficer4,
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
                { command: { text: "Select", click: ListNationalityOfficer4Select }, title: " ", width: 60 },
                //{ field: "Code", title: "No", width: 100 },
                { field: "DescOne", title: "NationalityOfficer4", width: 200 }
            ]
        });
    }

    function ListNationalityOfficer4Select(e) {
        e.preventDefault();
        var grid = $("#gridListNationalityOfficer4").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityOfficer4Desc).val(dataItemX.DescOne);
        $(htmlNationalityOfficer4).val(dataItemX.Code);
        WinListNationalityOfficer4.close();

    }

    function getDataSourceListBankVA(_fundClientPK) {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: window.location.origin + "/Radsoft/FundClient/GetBankVA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
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

    function initListBankVA(_fundClientPK) {
        var dsListBankVA = getDataSourceListBankVA(_fundClientPK);
        $("#gridListBankVA").empty();
        $("#gridListBankVA").kendoGrid({
            dataSource: dsListBankVA,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: false,
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
                { field: "Currency", title: "Currency", width: 100 },
                { field: "BankName", title: "Bank Name", width: 200 },
                { field: "AccountNo", title: "Account No", width: 150 },
                { field: "AccountName", title: "Account Name", width: 200 },
            ]
        });
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

    $("#BtnGenerateSInvestBankAccount").click(function () {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)


        alertify.confirm("Are you sure want to Generate S-Invest Bank Account ?", function (e) {
            if (e) {

                var FundClient = {
                    FundClientSelected: stringFundClientFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/GenerateSInvest_BankAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundClient),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //var newwindow = window.open(data, '_blank');
                        //window.location = data

                        $("#downloadFileRadsoft").attr("href", data);
                        $("#downloadFileRadsoft").attr("download", "S-Invest_BankAccount.txt");
                        document.getElementById("downloadFileRadsoft").click();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }

                });
            }
        });
    });

    $("#BtnGenerateSInvestBankAccountVA").click(function () {


        alertify.confirm("Are you sure want to Generate S-Invest Bank Account VA?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/GenerateSInvest_BankAccountVA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //var newwindow = window.open(data, '_blank');
                        //window.location = data

                        $("#downloadFileRadsoft").attr("href", data);
                        $("#downloadFileRadsoft").attr("download", "S-Invest_BankAccount.txt");
                        document.getElementById("downloadFileRadsoft").click();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }

                });
            }
        });
    });

    $("#BtnSuspendBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)


        alertify.confirm("Are you sure want to Suspend by Selected Data ?", function (e) {
            if (e) {
                var FundClient = {
                    FundClientSelected: stringFundClientFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/SuspendBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundClient),
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

    $("#BtnUnSuspendBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)


        alertify.confirm("Are you sure want to UnSuspend by Selected Data ?", function (e) {
            if (e) {
                var FundClient = {
                    FundClientSelected: stringFundClientFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/UnSuspendBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundClient),
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

    $("#BtnCopyClient").click(function () {

        if ($("#BitIsAfiliated").text() != 'FREE') {
            alertify.alert('Cannot create from afiliated client');
            return;
        }

        alertify.confirm("Are you sure want to create new afiliated client using this data?", function (e) {
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



    $("#BtnImportBanks").click(function () {
        document.getElementById("FileImportBanks").click();
    });

    $("#FileImportBanks").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportBanks").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("UpdateBanksTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateBanksTemp_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBanks").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBanks").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBanks").val("");
        }
    });

    function initGridTabSummary(_fundClientPK) {
        if (_fundClientPK == undefined || _fundClientPK == "") {
            $(GlobTabStrip.items()[8]).hide();
            _fundClientPK = 0;
        }

        $("#gridSummaryData").empty();
        var FundClientSummaryURL = window.location.origin + "/Radsoft/FundClient/GetSummary/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
            dataSourceHistory = getDataSummary(FundClientSummaryURL);

        $("#gridSummaryData").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Summary"
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
            columns: [

                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "UnitAmount", title: "Unit Amount", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Nav", title: "Nav", width: 300, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TotalAmount", title: "Total Amount", format: "{0:n0}", attributes: { style: "text-align:right;" } }
            ]
        });
    }

    $("#BtnImportIndividual").click(function () {
        document.getElementById("FileImportIndividual").click();
    });


    $("#BtnImportSID").click(function () {
        document.getElementById("FileImportSID").click();
    });

    $("#FileImportSID").change(function () {

        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportSID").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("UpdateSIDTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateSIDTemp_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportSID").val("");
                    alertify.alert("Import Success");
                    if (_GlobClientCode == "29") {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/HostToHostSwivel/ClientRevise/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                console.log(data);
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                    refresh();
                    if (_GlobClientCode == "07") {
                        var wnd = window.open(_GlobUrlServerVerifyBulkToFO);
                        wnd.close();
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportSID").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportSID").val("");
        }
    });

    $("#FileImportIndividual").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportIndividual").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("FundClientInd", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportIndividual").val("");

                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportIndividual").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportIndividual").val("");
        }
    });

    $("#BtnImportInstitusi").click(function () {
        document.getElementById("FileImportInstitusi").click();
    });

    $("#FileImportInstitusi").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportInstitusi").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("FundClientIns", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClient_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportInstitusi").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportInstitusi").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportInstitusi").val("");
        }
    });

    $("#BtnKYCUpdate").click(function () {
        alertify.confirm("Are you sure want to Update KYC Date for this client ?", function () {
            $.ajax({
                url: window.location.origin + "/Radsoft/FundClient/DatePengkinianData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#HistoryPK").val() + "/" + $("#FundClientPK").val() + "/" + sessionStorage.getItem("user"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (FundClient) {
                    alertify.alert('Success Update Kyc Date');
                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!
                    var yyyy = today.getFullYear();
                    today = mm + '/' + dd + '/' + yyyy;
                    $('#DatePengkinianData').val(today)
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }, function () {
            alertify.alert('Cancel Update KYC Date');
        });

    });

    function initHistory(_fundclientPK) {

        $("#gridHistoryRequest").empty();
        var CustomerServiceBookApprovedURL = window.location.origin + "/Radsoft/CustomerServiceBook/GetDataCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundclientPK + "/" + 2,
            dataSourceApproved = getDataSource(CustomerServiceBookApprovedURL);

        $("#gridHistoryRequest").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Customer Service Book"
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
            columns: [

                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "EntryUsersID", title: "EntryUsersID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "Email", title: "Email", width: 200 },
                { field: "Phone", title: "Phone", width: 150 },
                { field: "AskLineDesc", title: "Ask Line", width: 100 },
                { field: "Message", title: "Message", width: 400 },
                { field: "Solution", title: "Solution", width: 400 },
                { field: "StatusMessageDesc", title: "Status Message", width: 120 },
                { field: "InternalComment", title: "Internal Comment", width: 300 },
                { field: "DoneUsersID", title: "Done ID", width: 100 },
                { field: "DoneTime", title: "Done Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UnDoneUsersID", title: "UnDone UsersID", width: 120 },
                { field: "UnDoneTime", title: "UnDone Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                //{ field: "VoidUsersID", title: "VoidID", width: 200 },
                //{ field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                //{ field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });
    }

    $("#BtnDckData").click(function () {
        if ($('#NoIdentitasInd1').val() == '' || $('#NoIdentitasInd1').val() == null) {
            alertify.alert('NO ID IS EMPTY');
            return;
        }


        alertify.confirm("Are you sure want to Check Dukcapil for this client ?", function () {

            var dck = {
                nik: $('#NoIdentitasInd1').val(),
                user_id: _DckID,
                password: _DckPassword,
                IP_USER: _DckIP
            };
            $.ajax({
                url: _DckURL,
                type: 'POST',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(dck),
                success: function (data) {
                    if (data.content == null) {
                        alertify.alert('NO DATA');
                        return;
                    }

                    if (data.content[0].RESPON == null) {
                        console.log(data.content[0].NIK);
                        alertify.alert(
                            data.content[0].NIK + '</br>' +
                            data.content[0].NAMA_LGKP + '</br>' +
                            data.content[0].AGAMA + '</br>' +
                            data.content[0].ALAMAT + '</br>' +
                            data.content[0].STATUS_KAWIN + '</br>' +
                            data.content[0].JENIS_KLMIN + '</br>' +
                            data.content[0].TGL_LHR + '</br>'
                        );

                    } else {
                        alertify.alert('NO DATA');
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }, function () {
            alertify.alert('Cancel Check Dukcapil');
        });

    });


    function initBankList(_fundclientPK) {

        $("#gridBankList").empty();
        var FundClientBankListURL = window.location.origin + "/Radsoft/FundClientBankList/GetDataCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundclientPK,
            dataSourceApproved = getDataBankList(FundClientBankListURL);

        $("#gridBankList").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Bank List"
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
            columns: [

                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "StatusDesc", title: "Status", filterable: false, width: 120 },
                { field: "BankID", title: "Bank", width: 150 },
                { field: "CurrencyID", title: "Currency", width: 150 },
                { field: "AccountName", title: "Account Name", width: 300 },
                { field: "AccountNo", title: "Account No", width: 150 },
                { field: "NoBank", title: "NoBank", width: 100 }
            ]
        });
    }


    function initBankDefault(_fundclientPK) {

        $("#gridBankDefault").empty();
        var FundClientBankListURL = window.location.origin + "/Radsoft/FundClientBankDefault/GetDataCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundclientPK,
            dataSourceApproved = getDataBankList(FundClientBankListURL);

        $("#gridBankDefault").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Bank Default"
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
            columns: [

                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "StatusDesc", title: "Status", filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "BankRecipientDesc", title: "Bank Recipient", width: 150 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "FundName", title: "Fund Name", width: 150 },
            ]
        });
    }

    $("#BtnPreview").click(function () {
        var date = new Date();
        var months = ["I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"];

        //alertify.alert($('#KodeKotaInd1').text());

        if ($('#SID').val() != "" || $('#SID').val() != null) {
            alertify.confirm("Are you sure want to Preview File ?", function (e) {
                if (e) {
                    $.blockUI({});
                    var EmailToClient = {
                        FundClientPK: $('#FundClientPK').val(),
                        SID: $('#SID').val(),
                        Month: months[date.getMonth()]
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundClient/GenerateReportSID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EmailToClient),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            //window.location = data;
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });


                }
            });
        }
        else {
            alertify.alert("SID is Not Available")
        }

    });
    $("#BtnSendMail").click(function () {
        //alertify.alert($('#Name').val());

        if ($('#SID').val() != "" || $('#SID').val() != null) {
            var CheckExisting = {
                FundClientPK: $('#FundClientPK').val(),
                ReportName: "Generate SID",
                InvestorType: $('#InvestorType').val()
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/FundClient/CheckExistingFile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(CheckExisting),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == 'File is Exist') {
                        var _email;
                        if ($('#InvestorType').val() == 1) {
                            _email = $('#Email').val();
                        }
                        else {
                            _email = $('#CompanyMail').val();
                        }
                        sendemail($('#FundClientPK').val(), $('#SID').val(), _email, $('#Name').val(), 'Generate SID');

                    }
                    else {
                        alertify.alert('File is Not Exist, Please Preview File First')
                    }
                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }
            });
        }
        else {
            alertify.alert("SID is Not Available")
        }



    });

    $("#BtnPreviewUnitTrustReport").click(function () {
        //$('#FundFrom').data('kendoMultiSelect').destroy();
        $('#FundFrom').unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();


        $("#StatusReport").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "POSTED ONLY", value: 1 },
                { text: "REVISED ONLY", value: 2 },
                { text: "APPROVED ONLY", value: 3 },
                { text: "PENDING ONLY", value: 4 },
                { text: "HISTORY ONLY", value: 5 },
                { text: "POSTED & APPROVED", value: 6 },
                { text: "POSTED & APPROVED & PENDING", value: 7 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatus,
            value: setStatus()
            //index: setStatus,
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setStatus() {
            if (_GlobClientCode == "10") {
                return 1;
            }
            else {
                return 7;
            }
        }


        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#FundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        WinGenerateUnitTrustReport.center();
        WinGenerateUnitTrustReport.open();

    });
    $("#BtnSendMailUnitTrustReport").click(function () {
        //alertify.alert($('#Name').val());

        if ($('#SID').val() != "" || $('#SID').val() != null) {
            if ($('#InvestorType').val() == 1) {
                var CheckExisting = {
                    FundClientPK: $('#FundClientPK').val(),
                    ReportName: "Unit Trust Report",
                    ClientName: $('#Name').val(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/CheckExistingFile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(CheckExisting),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 'File is Exist') {
                            sendemail($('#FundClientPK').val(), $('#SID').val(), $('#Email').val(), $('#Name').val());

                        }
                        else {
                            alertify.alert('File is Not Exist, Please Preview File First')
                        }
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
            else {
                alertify.alert("This Function Only For Individual Client")
            }
        }
        else {
            alertify.alert("SID is Not Available")
        }



    });


    $("#BtnOkUnitTrustReport").click(function () {
        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        //if (validateData() == 1) {
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});

                var UnitRegistryRpt = {

                    ReportName: "Unit Trust Report",
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    FundClientFrom: $('#FundClientPK').val(),
                    ClientName: $('#Name').val(),
                    FundFrom: stringFundFrom,
                    Status: $("#StatusReport").data("kendoComboBox").value(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/UnitRegistryReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(UnitRegistryRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        var newwindow = window.open(data, '_blank');
                        //var multiSelect = $('#gridFundClient').data("kendoMultiSelect");
                        //checkedIds = {};
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });


            }
        });



    });

    $("#BtnCancelUnitTrustReport").click(function () {

        alertify.confirm("Are you sure want to cancel Download Report?", function (e) {
            if (e) {
                WinGenerateUnitTrustReport.close();
                //alertify.alert("Cancel Download Report");
            }
        });
    });

    function validatedataReport() {
        if (GlobValidator.validate()) {
            return 1;
        }
        else {

            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function sendemail(_pk, _sid, _email, _name, _reportName) {
        //alertify.alert(filename);
        var SendMail = {
            FundClientPK: _pk,
            SID: _sid,
            Email: _email,
            Name: _name,
            ReportName: _reportName,
        };

        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/SendMailByInput/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(SendMail),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $.unblockUI();
                alertify.alert(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }


    var WinKTP;

    WinKTP = $("#winKTP").kendoWindow({
        height: 720,
        title: "Image",
        visible: false,
        width: 1280,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 150 });
        },

    }).data("kendoWindow");

    var zoomInBtn = window.document.getElementById('BtnZoomInKTP');
    var zoomOutBtn = window.document.getElementById('BtnZoomOutKTP');
    var image = window.document.getElementById('ImgApi_1KTP');
    scale = 1;
    angle = 0;
    $('#BtnRotateKTP').click(function () {
        angle += 90;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtn.addEventListener('click', function () {
        scale += .25;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtn.addEventListener('click', function () {
        scale -= .25;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    $("#btnDisplayKTP").click(function () {
        $.ajax({
            url: "http://" + _GlobUrlServerRDOApi + "image/profile?id=" + $("#FundClientPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $.ajax({
                    url: "http://" + _GlobUrlServerRDOApi + "image/profileoriginal?id=" + data.ktp.original,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (A) {
                        if (A.type !== "application/pdf") {
                            $("#ImgApi_1KTP").attr("src", A.base64);
                            $("#ImgApi_KTP").attr("src", "");
                            $("#ImgApi_KTP").hide();
                            $("#ImgApi_1KTP").show();
                        } else {
                            $("#ImgApi_KTP").removeAttr("type");
                            $("#ImgApi_KTP").attr("src", A.base64);
                            $("#ImgApi_1KTP").attr("src", "");
                            $("#ImgApi_KTP").show();
                            $("#ImgApi_1KTP").hide();
                        }
                        WinKTP.center();
                        WinKTP.open();
                        scale = 1;
                        angle = 0;
                        var image = window.document.getElementById('ImgApi_1KTP');
                        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
                    },
                    error: function (data) {
                    }
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    //show identity 1 mandiri

    var zoomInBtn = window.document.getElementById('BtnZoomInIdentity1');
    var zoomOutBtn = window.document.getElementById('BtnZoomOutIdentity1');
    var image = window.document.getElementById('ImgApi_1Identity1');
    scale = 1;
    angle = 0;
    $('#BtnRotateIdentity1').click(function () {
        angle += 90;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtn.addEventListener('click', function () {
        scale += .25;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtn.addEventListener('click', function () {
        scale -= .25;
        image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    $("#btnDisplayIdentity1").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.type !== "application/pdf") {
                    $("#ImgApi_1Identity1").attr("src", data[0].Base64);
                    $("#ImgApi_Identity1").attr("src", "");
                    $("#ImgApi_Identity1").hide();
                    $("#ImgApi_1Identity1").show();
                } else {
                    $("#ImgApi_Identity1").removeAttr("type");
                    $("#ImgApi_Identity1").attr("src", data[0].Base64);
                    $("#ImgApi_1Identity1").attr("src", "");
                    $("#ImgApi_Identity1").show();
                    $("#ImgApi_1Identity1").hide();
                }
                WinIdentity1.center();
                WinIdentity1.open();
                scale = 1;
                angle = 0;
                var image = window.document.getElementById('ImgApi_1Identity1');
                image.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });


    $("#BtnNotifVerifyClient").click(function () {
        if ($("#IFUACode").val() != null && $("#IFUACode").val() != "") {
            alertify.alert("No IFUACode");
            return;
        }

        var IFUA = {
            ifuacode: $("#IFUACode").val()
        }

        $.ajax({
            url: _GlobMMIFrontUrl,
            type: 'POST',
            data: JSON.stringify(IFUA),
            headers: {
                'x-api-key': _GlobXApiKeyMMI
            },
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                alertify.alert(data.message);
            },
            error: function (data) {
                alertify.alert(data.message);
            }
        });
    });



    function initHistoricalSummary(_fundClientPK) {

        $("#gridHistoricalSummaryData").empty();
        var FundClientHistoricalSummaryURL = window.location.origin + "/Radsoft/FundClient/GetHistoricalSummary/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistoricalSummary = getDataHistoricalSummary(FundClientHistoricalSummaryURL);

        $("#gridHistoricalSummaryData").kendoGrid({
            dataSource: dataSourceHistoricalSummary,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Summary"
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
            columns: [

                { field: "TradeDate", title: "Trade Date", width: 150, template: "#= kendo.toString(kendo.parseDate(TradeDate), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "Type", title: "Type", width: 150 },
                { field: "CashAmount", title: "Cash Amount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "UnitAmount", title: "Unit Amount", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                { field: "TotalAmount", title: "Total Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "NAV", title: "NAV", width: 150, format: "{0:n8}", attributes: { style: "text-align:right;" } },
            ]
        });
    }


    function initPositionSummary(_fundClientPK) {

        $("#gridPositionSummaryData").empty();
        var FundClientPositionSummaryURL = window.location.origin + "/Radsoft/FundClient/GetPositionSummary/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePositionSummary = getDataPositionSummary(FundClientPositionSummaryURL);

        $("#gridPositionSummaryData").kendoGrid({
            dataSource: dataSourcePositionSummary,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Summary"
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
            columns: [

                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "FundName", title: "Fund Name", width: 200 },
                { field: "CurrencyID", title: "Currency ID", width: 150 },
                { field: "UnitAmount", title: "Unit Amount", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                { field: "NAV", title: "NAV", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                { field: "Balance", title: "Balance", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "BalanceUSD", title: "Balance USD", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
            ]
        });
    }


    WinIDCard = $("#WinIDCard").kendoWindow({
        height: 720,
        title: "Image",
        visible: false,
        width: 1280,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 150 });
        },

    }).data("kendoWindow");

    WinSelfie = $("#WinSelfie").kendoWindow({
        height: 720,
        title: "Image",
        visible: false,
        width: 1280,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 150 });
        },

    }).data("kendoWindow");

    WinSignature = $("#WinSignature").kendoWindow({
        height: 720,
        title: "Image",
        visible: false,
        width: 1280,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 150 });
        },

    }).data("kendoWindow");

    WinBeneficial = $("#WinBeneficial").kendoWindow({
        height: 720,
        title: "Image",
        visible: false,
        width: 1280,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 150 });
        },

    }).data("kendoWindow");


    var zoomInBtnIDCard = window.document.getElementById('BtnZoomInIDCard');
    var zoomOutBtnIDCard = window.document.getElementById('BtnZoomOutIDCard');
    var imageIDCard = window.document.getElementById('ImgApi_1IDCard');
    scale = 1;
    angle = 0;
    $('#BtnRotateIDCard').click(function () {
        angle += 90;
        imageIDCard.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtnIDCard.addEventListener('click', function () {
        scale += .25;
        imageIDCard.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtnIDCard.addEventListener('click', function () {
        scale -= .25;
        imageIDCard.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });


    var zoomInBtnSelfie = window.document.getElementById('BtnZoomInSelfie');
    var zoomOutBtnSelfie = window.document.getElementById('BtnZoomOutSelfie');
    var imageSelfie = window.document.getElementById('ImgApi_1Selfie');
    scale = 1;
    angle = 0;
    $('#BtnRotateSelfie').click(function () {
        angle += 90;
        imageSelfie.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtnSelfie.addEventListener('click', function () {
        scale += .25;
        imageSelfie.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtnSelfie.addEventListener('click', function () {
        scale -= .25;
        imageSelfie.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });


    var zoomInBtnSignature = window.document.getElementById('BtnZoomInSignature');
    var zoomOutBtnSignature = window.document.getElementById('BtnZoomOutSignature');
    var imageSignature = window.document.getElementById('ImgApi_1Signature');
    scale = 1;
    angle = 0;
    $('#BtnRotateSignature').click(function () {
        angle += 90;
        imageSignature.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtnSignature.addEventListener('click', function () {
        scale += .25;
        imageSignature.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtnSignature.addEventListener('click', function () {
        scale -= .25;
        imageSignature.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });


    var zoomInBtnBeneficial = window.document.getElementById('BtnZoomInBeneficial');
    var zoomOutBtnBeneficial = window.document.getElementById('BtnZoomOutBeneficial');
    var imageBeneficial = window.document.getElementById('ImgApi_1Beneficial');
    scale = 1;
    angle = 0;
    $('#BtnRotateBeneficial').click(function () {
        angle += 90;
        imageBeneficial.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomInBtnBeneficial.addEventListener('click', function () {
        scale += .25;
        imageBeneficial.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });

    zoomOutBtnBeneficial.addEventListener('click', function () {
        scale -= .25;
        imageBeneficial.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
    });


    $("#BtnIDCard").click(function () {

        alertify.confirm("Are you sure want to Download ID Card ?", function (e) {
            if (e) {
                if (e) {

                    $.ajax({
                        url: "http://158.140.177.25:17060/mole/service/MOLEService.svc/GetUserImage?noKTP=" + $("#NoIdentitasInd1").val() + "&jenisimage=1",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            //if (data.type !== "application/pdf") {

                            var _data = data;
                            var _cleanData = _data.replace(String.fromCharCode(92), "");
                            var _str1 = "data:image;base64, ";
                            var _cleanData = _str1.concat(_cleanData);

                            $("#ImgApi_1IDCard").attr("src", _cleanData);

                            $("#ImgApi_1IDCard").show();
                            console.log(_cleanData);


                            WinIDCard.center();
                            WinIDCard.open();
                            scale = 1;
                            angle = 0;
                            var imageIDCard = window.document.getElementById('ImgApi_1IDCard');
                            imageIDCard.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            }
        });
    });

    $("#BtnSelfie").click(function () {

        alertify.confirm("Are you sure want to Download Selfie ?", function (e) {
            if (e) {
                if (e) {

                    $.ajax({
                        url: "http://158.140.177.25:17060/mole/service/MOLEService.svc/GetUserImage?noKTP=" + $("#NoIdentitasInd1").val() + "&jenisimage=2",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            //if (data.type !== "application/pdf") {

                            var _data = data;
                            var _cleanData = _data.replace(String.fromCharCode(92), "");
                            var _str1 = "data:image;base64, ";
                            var _cleanData = _str1.concat(_cleanData);

                            $("#ImgApi_1Selfie").attr("src", _cleanData);

                            $("#ImgApi_1Selfie").show();
                            console.log(_cleanData);


                            WinSelfie.center();
                            WinSelfie.open();
                            scale = 1;
                            angle = 0;
                            var imageSelfie = window.document.getElementById('ImgApi_1Selfie');
                            imageSelfie.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            }
        });
    });

    $("#BtnSignature").click(function () {

        alertify.confirm("Are you sure want to Download Signature ?", function (e) {
            if (e) {
                if (e) {

                    $.ajax({
                        url: "http://158.140.177.25:17060/mole/service/MOLEService.svc/GetUserImage?noKTP=" + $("#NoIdentitasInd1").val() + "&jenisimage=3",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            //if (data.type !== "application/pdf") {

                            var _data = data;
                            var _cleanData = _data.replace(String.fromCharCode(92), "");
                            var _str1 = "data:image;base64, ";
                            var _cleanData = _str1.concat(_cleanData);

                            $("#ImgApi_1Signature").attr("src", _cleanData);

                            $("#ImgApi_1Signature").show();
                            console.log(_cleanData);


                            WinSignature.center();
                            WinSignature.open();
                            scale = 1;
                            angle = 0;
                            var imageSignature = window.document.getElementById('ImgApi_1Signature');
                            imageSignature.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            }
        });
    });

    $("#BtnBeneficial").click(function () {

        alertify.confirm("Are you sure want to Download Beneficial ?", function (e) {
            if (e) {
                if (e) {

                    $.ajax({
                        url: "http://158.140.177.25:17060/mole/service/MOLEService.svc/GetUserImage?noKTP=" + $("#NoIdentitasInd1").val() + "&jenisimage=4",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            //if (data.type !== "application/pdf") {

                            var _data = data;
                            var _cleanData = _data.replace(String.fromCharCode(92), "");
                            var _str1 = "data:image;base64, ";
                            var _cleanData = _str1.concat(_cleanData);

                            $("#ImgApi_1Beneficial").attr("src", _cleanData);

                            $("#ImgApi_1Beneficial").show();
                            console.log(_cleanData);


                            WinBeneficial.center();
                            WinBeneficial.open();
                            scale = 1;
                            angle = 0;
                            var imageBeneficial = window.document.getElementById('ImgApi_1Beneficial');
                            imageBeneficial.style.transform = 'scale(' + scale + ') rotate(' + angle + 'deg)';
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            }
        });
    });


    function getDataAffiliated(_url) {
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
                            RiskAffiliatedPK: { type: "number" },
                            Question: { type: "string" }
                        }
                    }
                }
            });
    }

    function initAffiliated(_NoIdentitasInd1) {


        $("#gridFundClientAffiliated").empty();
        var FundClientAffiliatedURL = window.location.origin + "/Radsoft/FundClient/GetFundClientAffiliated/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InvestorType").val() + "/" + _NoIdentitasInd1,
            dataSourceApproved = getDataAffiliated(FundClientAffiliatedURL);

        $("#gridFundClientAffiliated").kendoGrid({
            dataSource: dataSourceApproved,
            height: 500,
            scrollable: {
                virtual: true
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
            columns: [

                { field: "FundClientPK", title: "SysNo.", filterable: false, width: 120 },
                //general
                { field: "ClientCategoryDesc", title: "Client Category", width: 100, hidden: true },
                { field: "InvestorTypeDesc", title: "Investor Type", width: 100 },
                { field: "Name", title: "Name", width: 300 },
                { field: "SID", title: "SID", width: 150 },
                { field: "NPWP", title: "NPWP", width: 150 },
                { field: "RegistrationNPWP", title: "RegistrationNPWP", width: 150, template: "#= kendo.toString(kendo.parseDate(RegistrationNPWP), 'dd/MMM/yyyy')#" },
                { field: "InvestorsRiskProfileDesc", title: "InvestorsRiskProfile", width: 150 },
                { field: "KYCRiskProfileDesc", title: "KYC Risk Profile", width: 150 },
                { field: "BitShareAbleToGroup", title: "BitShareAbleToGroup", width: 150, template: "#= BitShareAbleToGroup ? 'Yes' : 'No' #" },
                { field: "AssetOwnerDesc", title: "AssetOwner", width: 150 },
                { field: "DatePengkinianData", title: "DatePengkinianData", width: 150, template: "#= kendo.toString(kendo.parseDate(DatePengkinianData), 'dd/MMM/yyyy')#" },

                //bank information
                { field: "TanggalLahir", title: "DOB", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                { field: "TeleponSelular", title: "Telepon Selular", width: 150 },
                { field: "Email", title: "Email", width: 150 },
                { field: "BitIsSuspend", title: "Is Suspend", width: 100, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },

                //Individual Opening 
                { field: "NamaDepanInd", title: "First name", width: 150 },
                { field: "NamaTengahInd", title: "Middle Name", width: 150 },
                { field: "NamaBelakangInd", title: "Last Name", width: 150 },
                //Identity
                { field: "IdentitasInd1Desc", title: "Identity Type (1)", width: 150 },
                { field: "NoIdentitasInd1", title: "Identity Number (1)", width: 150 },
                { field: "RegistrationDateIdentitasInd1", title: "Regs date Identity (1)", width: 150, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd1), 'dd/MMM/yyyy')#" },
                { field: "ExpiredDateIdentitasInd1", title: "Expr Date Identity (1)", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd1), 'dd/MMM/yyyy')#" },
                { field: "OtherAlamatInd1", title: "Identity Address(1)", width: 150 },
                { field: "Identity1RT", title: "Identity 1 RT (1)", width: 150 },
                { field: "Identity1RW", title: "Identity 1 RW (1)", width: 150 },
                { field: "OtherKodeKotaInd1Desc", title: "Identity City (1)", width: 150 },
                { field: "OtherPropinsiInd1Desc", title: "Identity Province (1)", width: 150 },
                { field: "OtherNegaraInd1Desc", title: "Identity Country (1)", width: 150 },
                { field: "OtherKodePosInd1", title: "Identity Zip Code (1)", width: 150 },
                { field: "IdentitasInd2Desc", title: "Identity Type (2)", width: 150 },
                { field: "NoIdentitasInd2", title: "Identity Number (2)", width: 150 },
                { field: "RegistrationDateIdentitasInd2", title: "Regs. Date Identity (2)", width: 150, template: "#= kendo.toString(kendo.parseDate(RegistrationDateIdentitasInd2), 'dd/MMM/yyyy')#" },
                { field: "ExpiredDateIdentitasInd2", title: "Expr. Date Identity (2)", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpiredDateIdentitasInd2), 'dd/MMM/yyyy')#" },
                //Domicile
                { field: "AlamatInd2", title: "Domicile Address", width: 150 },
                { field: "DomicileRT", title: "Domicile RT", width: 150 },
                { field: "DomicileRW", title: "Domicile RW", width: 150 },
                { field: "KodeKotaInd2Desc", title: "Domicile City", width: 150 },
                { field: "KodeDomisiliPropinsiDesc", title: "Domicile Province", width: 150 },
                { field: "CountryofDomicileDesc", title: "Country of Domicile", width: 150 },
                { field: "KodePosInd2", title: "Domicile Zip Code", width: 150 },
                //Information I
                { field: "TempatLahir", title: "Birth Place", width: 150 },
                { field: "TanggalLahir", title: "Birth Date", width: 150, template: "#= kendo.toString(kendo.parseDate(TanggalLahir), 'dd/MMM/yyyy')#" },
                { field: "JenisKelaminDesc", title: "Sex", width: 150 },
                { field: "Nationality", title: "Nationality", width: 150 },
                { field: "CountryOfBirth", title: "Country of Birth", width: 150 },
                { field: "AgamaDesc", title: "Religion", width: 150 },
                { field: "OtherAgama", title: "Other Religion", width: 150 },
                { field: "PendidikanDesc", title: "Education", width: 150 },
                { field: "OtherPendidikan", title: "Other Education", width: 150 },
                { field: "MotherMaidenName", title: "Mother Maiden Name", width: 150 },

                { field: "AhliWaris", title: "Heir", width: 150 },
                { field: "HubunganAhliWaris", title: "Heir Relation", width: 150 },
                { field: "PekerjaanDesc", title: "Occupation", width: 150 },
                { field: "OtherOccupation", title: "Other Occupation", width: 150 },
                { field: "NatureOfBusinessDesc", title: "Nature Of Business", width: 150 },
                { field: "NatureOfBusinessLainnya", title: "N.O.B (Text)", width: 150 },
                { field: "PenghasilanIndDesc", title: "Income per Annum", width: 150 },
                { field: "SumberDanaIndDesc", title: "Source Of Funds", width: 150 },
                { field: "MaksudTujuanIndDesc", title: "Investment Objectives", width: 150 },
                { field: "StatusPerkawinanDesc", title: "Marital Status", width: 150 },

                { field: "SpouseName", title: "Spouse Name", width: 150 },
                { field: "SpouseDateOfBirth", title: "Spouse Birth Date", width: 150, template: "#= kendo.toString(kendo.parseDate(SpouseDateOfBirth), 'dd/MMM/yyyy')#" },
                { field: "SpouseBirthPlace", title: "Spouse Birth Place", width: 150 },
                { field: "SpouseOccupationDesc", title: "Spouse Occupation", width: 150 },
                { field: "OtherSpouseOccupation", title: "Other Spouse Occupation", width: 150 },
                { field: "SpouseNatureOfBusinessDesc", title: "Spouse Nature Of Business", width: 150 },
                { field: "SpouseNatureOfBusinessOther", title: "Spouse N.O.B (Text)", width: 150 },
                { field: "SpouseIDNo", title: "Spouse ID Number", width: 150 },
                { field: "SpouseNationalityDesc", title: "Spouse Nationality", width: 150 },
                { field: "SpouseAnnualIncomeDesc", title: "Spouse Annual Income", width: 150 },
                //Information II
                { field: "NamaKantor", title: "Office Name", width: 150 },
                { field: "EmployerLineOfBusinessDesc", title: "Employer Line Of Business", width: 150 },
                { field: "JabatanKantor", title: "Office Position", width: 150 },
                { field: "TeleponKantor", title: "Office Phone", width: 150 },
                { field: "AlamatKantorInd", title: "Office address", width: 150 },
                { field: "KodeKotaKantorIndDesc", title: "Office City", width: 150 },
                { field: "KodePropinsiKantorIndDesc", title: "Office Province", width: 150 },
                { field: "KodeCountryofKantorDesc", title: "Office Country", width: 150 },
                { field: "KodePosKantorInd", title: "Office zipcode", width: 150 },

                //OTHERS
                //Other
                { field: "PolitisDesc", title: "Politically Exposed", width: 150 },
                { field: "PolitisLainnya", title: "P.E (Text)", width: 150 },
                { field: "PolitisRelationDesc", title: "Political Relation", width: 150 },
                { field: "PolitisName", title: "Political Name", width: 150 },
                { field: "PolitisFT", title: "Political Position", width: 150 },
            ]
        });
    }


    $("#BtnUpSID").click(function () {

        WinUpdateSID.center();
        WinUpdateSID.open();

    });

    $("#BtnImportFailedSID").click(function () {
        document.getElementById("FileImportFailedSID").click();
    });

    $("#FileImportFailedSID").change(function () {

        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportFailedSID").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("UpdateFailedSIDTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateSIDTemp_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportFailedSID").val("");
                    alertify.alert("Import Success");
                    refresh();
                    if (_GlobClientCode == "29") {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/HostToHostSwivel/HelpDeskCreate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                console.log(data);
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportFailedSID").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportFailedSID").val("");
        }
    });

    $("#BtnGetDataFromCoreBanking").click(function () {

        WinGetDataFromCoreBanking.center();
        WinGetDataFromCoreBanking.open();

    });

    function onWinWinGetDataFromCoreBankingClose() {
        $("#CIFNo").val("");
    }

    $("#BtnCancelGetDataFromCoreBanking").click(function () {

        WinGetDataFromCoreBanking.close();

    });

    $("#BtnOKGetDataFromCoreBanking").click(function () {

        $("#InvestorType").data("kendoComboBox").value(1);
        $("#Name").val("TEST DATA");
        $("#Description").val("DESCRIPTION HERE");
        $("#KYCRiskProfile").data("kendoComboBox").value(1);
        $("#SID").val("SID HERE");
        $("#IFUACode").val("IFUA HERE");
        $("#NPWP").val("NPWP HERE");
        $("#RegistrationNPWP").data("kendoDatePicker").value(new Date('2019-05-01'));
        $("#StatementType").data("kendoComboBox").value(1);

        WinGetDataFromCoreBanking.close();

    });


});
