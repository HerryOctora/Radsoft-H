$(document).ready(function () {
    document.title = 'FORM CORPORATE GOV AM';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
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
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
        });

        win = $("#WinCorporateGovAM").kendoWindow({
            height: 755,
            title: "CorporateGovAM Detail",
            visible: false,
            width: 1872,
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

        function OnChangeDate() {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }
    }

    var GlobValidator = $("#WinCorporateGovAM").kendoValidator().data("kendoValidator");

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

            $("#CorporateGovAMPK").val(dataItemX.CorporateGovAMPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            //$("#Date").val(dataItemX.Date);
            $("#Date").val(kendo.toString(kendo.parseDate(dataItemX.Date), 'dd/MMM/yyyy'));
            $("#Row1").val(dataItemX.Row1);
            $("#Row2").val(dataItemX.Row2);
            $("#Row3").val(dataItemX.Row3);
            $("#Row4").val(dataItemX.Row4);
            $("#Row5").val(dataItemX.Row5);
            $("#Row6").val(dataItemX.Row6);
            $("#Row7").val(dataItemX.Row7);
            $("#Row8").val(dataItemX.Row8);
            $("#Row9").val(dataItemX.Row9);
            $("#Row10").val(dataItemX.Row10);
            $("#Row11").val(dataItemX.Row11);
            $("#Row12").val(dataItemX.Row12);
            $("#Row13").val(dataItemX.Row13);
            $("#Row14").val(dataItemX.Row14);
            $("#Row15").val(dataItemX.Row15);
            $("#Row16").val(dataItemX.Row16);
            $("#Row17").val(dataItemX.Row17);
            $("#Row18").val(dataItemX.Row18);
            $("#Row19").val(dataItemX.Row19);
            $("#Row20").val(dataItemX.Row20);
            $("#Row21").val(dataItemX.Row21);
            $("#Row22").val(dataItemX.Row22);
            $("#Row23").val(dataItemX.Row23);
            $("#Row24").val(dataItemX.Row24);
            $("#Row25").val(dataItemX.Row25);
            $("#Row26").val(dataItemX.Row26);
            $("#Row27").val(dataItemX.Row27);
            $("#Row28").val(dataItemX.Row28);
            $("#Row29").val(dataItemX.Row29);
            $("#Row30").val(dataItemX.Row30);
            $("#Row31").val(dataItemX.Row31);
            $("#Row32").val(dataItemX.Row32);
            $("#Row33").val(dataItemX.Row33);
            $("#Row34").val(dataItemX.Row34);
            $("#Row35").val(dataItemX.Row35);
            $("#Row36").val(dataItemX.Row36);
            $("#Row37").val(dataItemX.Row37);
            $("#Row38").val(dataItemX.Row38);
            $("#Row39").val(dataItemX.Row39);
            $("#Row40").val(dataItemX.Row40);
            $("#Row41").val(dataItemX.Row41);
            $("#Row42").val(dataItemX.Row42);
            $("#Row43").val(dataItemX.Row43);
            $("#Row44").val(dataItemX.Row44);
            $("#Row45").val(dataItemX.Row45);
            $("#Row46").val(dataItemX.Row46);
            $("#Row47").val(dataItemX.Row47);
            $("#Row48").val(dataItemX.Row48);
            $("#Row49").val(dataItemX.Row49);
            $("#Row50").val(dataItemX.Row50);
            $("#Row51").val(dataItemX.Row51);
            $("#Row52").val(dataItemX.Row52);
            $("#Row53").val(dataItemX.Row53);
            $("#Row54").val(dataItemX.Row54);
            $("#Row55").val(dataItemX.Row55);
            $("#Row56").val(dataItemX.Row56);
            $("#Row57").val(dataItemX.Row57);
            $("#Row58").val(dataItemX.Row58);
            $("#Row59").val(dataItemX.Row59);
            $("#Row60").val(dataItemX.Row60);
            $("#Row61").val(dataItemX.Row61);
            $("#Row62").val(dataItemX.Row62);
            $("#Row63").val(dataItemX.Row63);
            $("#Row64").val(dataItemX.Row64);
            $("#Row65").val(dataItemX.Row65);
            $("#Row66").val(dataItemX.Row66);
            $("#Row67").val(dataItemX.Row67);
            $("#Row68").val(dataItemX.Row68);
            $("#Row69").val(dataItemX.Row69);
            $("#Row70").val(dataItemX.Row70);
            $("#Row71").val(dataItemX.Row71);
            $("#Row72").val(dataItemX.Row72);
            $("#Row73").val(dataItemX.Row73);
            $("#Row74").val(dataItemX.Row74);
            $("#Row75").val(dataItemX.Row75);
            $("#Row76").val(dataItemX.Row76);
            $("#Row77").val(dataItemX.Row77);
            $("#Row78").val(dataItemX.Row78);
            $("#Row79").val(dataItemX.Row79);
            $("#Row80").val(dataItemX.Row80);
            $("#Row81").val(dataItemX.Row81);
            $("#Row82").val(dataItemX.Row82);
            $("#Row83").val(dataItemX.Row83);
            $("#Row84").val(dataItemX.Row84);
            $("#Row85").val(dataItemX.Row85);
            $("#Row86").val(dataItemX.Row86);
            $("#Row87").val(dataItemX.Row87);
            $("#Row88").val(dataItemX.Row88);
            $("#Row89").val(dataItemX.Row89);
            $("#Row90").val(dataItemX.Row90);
            $("#Row91").val(dataItemX.Row91);
            $("#Row92").val(dataItemX.Row92);
            $("#Row93").val(dataItemX.Row93);
            $("#Row94").val(dataItemX.Row94);
            $("#Row95").val(dataItemX.Row95);
            $("#Row96").val(dataItemX.Row96);
            $("#Row97").val(dataItemX.Row97);
            $("#Row98").val(dataItemX.Row98);
            $("#Row99").val(dataItemX.Row99);
            $("#Row100").val(dataItemX.Row100);

            $("#Row101").val(dataItemX.Row101);
            $("#Row102").val(dataItemX.Row102);
            $("#Row103").val(dataItemX.Row103);
            $("#Row104").val(dataItemX.Row104);
            $("#Row105").val(dataItemX.Row105);
            $("#Row106").val(dataItemX.Row106);
            $("#Row107").val(dataItemX.Row107);
            $("#Row108").val(dataItemX.Row108);
            $("#Row109").val(dataItemX.Row109);
            $("#Row110").val(dataItemX.Row110);
            $("#Row111").val(dataItemX.Row111);
            $("#Row112").val(dataItemX.Row112);
            $("#Row113").val(dataItemX.Row113);
            $("#Row114").val(dataItemX.Row114);
            $("#Row115").val(dataItemX.Row115);
            $("#Row116").val(dataItemX.Row116);
            $("#Row117").val(dataItemX.Row117);
            $("#Row118").val(dataItemX.Row118);
            $("#Row119").val(dataItemX.Row119);
            $("#Row120").val(dataItemX.Row120);
            $("#Row121").val(dataItemX.Row121);
            $("#Row122").val(dataItemX.Row122);
            $("#Row123").val(dataItemX.Row123);
            $("#Row124").val(dataItemX.Row124);
            $("#Row125").val(dataItemX.Row125);
            $("#Row126").val(dataItemX.Row126);
            $("#Row127").val(dataItemX.Row127);
            $("#Row128").val(dataItemX.Row128);
            $("#Row129").val(dataItemX.Row129);
            $("#Row130").val(dataItemX.Row130);
            $("#Row131").val(dataItemX.Row131);
            $("#Row132").val(dataItemX.Row132);
            $("#Row133").val(dataItemX.Row133);
            $("#Row134").val(dataItemX.Row134);
            $("#Row135").val(dataItemX.Row135);
            $("#Row136").val(dataItemX.Row136);
            $("#Row137").val(dataItemX.Row137);
            $("#Row138").val(dataItemX.Row138);
            $("#Row139").val(dataItemX.Row139);
            $("#Row140").val(dataItemX.Row140);
            $("#Row141").val(dataItemX.Row141);
            $("#Row142").val(dataItemX.Row142);
            $("#Row143").val(dataItemX.Row143);
            $("#Row144").val(dataItemX.Row144);
            $("#Row145").val(dataItemX.Row145);
            $("#Row146").val(dataItemX.Row146);
            $("#Row147").val(dataItemX.Row147);
            $("#Row148").val(dataItemX.Row148);
            $("#Row149").val(dataItemX.Row149);
            $("#Row150").val(dataItemX.Row150);
            $("#Row151").val(dataItemX.Row151);
            $("#Row152").val(dataItemX.Row152);
            $("#Row153").val(dataItemX.Row153);
            $("#Row154").val(dataItemX.Row154);
            $("#Row155").val(dataItemX.Row155);
            $("#Row156").val(dataItemX.Row156);
            $("#Row157").val(dataItemX.Row157);
            $("#Row158").val(dataItemX.Row158);
            $("#Row159").val(dataItemX.Row159);
            $("#Row160").val(dataItemX.Row160);
            $("#Row161").val(dataItemX.Row161);
            $("#Row162").val(dataItemX.Row162);
            $("#Row163").val(dataItemX.Row163);
            $("#Row164").val(dataItemX.Row164);
            $("#Row165").val(dataItemX.Row165);
            $("#Row166").val(dataItemX.Row166);
            $("#Row167").val(dataItemX.Row167);
            $("#Row168").val(dataItemX.Row168);
            $("#Row169").val(dataItemX.Row169);
            $("#Row170").val(dataItemX.Row170);
            $("#Row171").val(dataItemX.Row171);
            $("#Row172").val(dataItemX.Row172);
            $("#Row173").val(dataItemX.Row173);
            $("#Row174").val(dataItemX.Row174);
            $("#Row175").val(dataItemX.Row175);
            $("#Row176").val(dataItemX.Row176);
            $("#Row177").val(dataItemX.Row177);
            $("#Row178").val(dataItemX.Row178);
            $("#Row179").val(dataItemX.Row179);
            $("#Row180").val(dataItemX.Row180);
            $("#Row181").val(dataItemX.Row181);
            $("#Row182").val(dataItemX.Row182);
            $("#Row183").val(dataItemX.Row183);
            $("#Row184").val(dataItemX.Row184);
            $("#Row185").val(dataItemX.Row185);
            $("#Row186").val(dataItemX.Row186);
            $("#Row187").val(dataItemX.Row187);
            $("#Row188").val(dataItemX.Row188);
            $("#Row189").val(dataItemX.Row189);
            $("#Row190").val(dataItemX.Row190);
            $("#Row191").val(dataItemX.Row191);
            $("#Row192").val(dataItemX.Row192);
            $("#Row193").val(dataItemX.Row193);
            $("#Row194").val(dataItemX.Row194);
            $("#Row195").val(dataItemX.Row195);
            $("#Row196").val(dataItemX.Row196);
            $("#Row197").val(dataItemX.Row197);
            $("#Row198").val(dataItemX.Row198);
            $("#Row199").val(dataItemX.Row199);
            $("#Row200").val(dataItemX.Row200);

            $("#Row201").val(dataItemX.Row201);
            $("#Row202").val(dataItemX.Row202);
            $("#Row203").val(dataItemX.Row203);
            $("#Row204").val(dataItemX.Row204);
            $("#Row205").val(dataItemX.Row205);
            $("#Row206").val(dataItemX.Row206);
            $("#Row207").val(dataItemX.Row207);
            $("#Row208").val(dataItemX.Row208);
            $("#Row209").val(dataItemX.Row209);
            $("#Row210").val(dataItemX.Row210);
            $("#Row211").val(dataItemX.Row211);
            $("#Row212").val(dataItemX.Row212);
            $("#Row213").val(dataItemX.Row213);
            $("#Row214").val(dataItemX.Row214);
            $("#Row215").val(dataItemX.Row215);
            $("#Row216").val(dataItemX.Row216);
            $("#Row217").val(dataItemX.Row217);
            $("#Row218").val(dataItemX.Row218);
            $("#Row219").val(dataItemX.Row219);
            $("#Row220").val(dataItemX.Row220);
            $("#Row221").val(dataItemX.Row221);
            $("#Row222").val(dataItemX.Row222);
            $("#Row223").val(dataItemX.Row223);
            $("#Row224").val(dataItemX.Row224);
            $("#Row225").val(dataItemX.Row225);
            $("#Row226").val(dataItemX.Row226);
            $("#Row227").val(dataItemX.Row227);
            $("#Row228").val(dataItemX.Row228);
            $("#Row229").val(dataItemX.Row229);
            $("#Row230").val(dataItemX.Row230);
            $("#Row231").val(dataItemX.Row231);
            $("#Row232").val(dataItemX.Row232);
            $("#Row233").val(dataItemX.Row233);
            $("#Row234").val(dataItemX.Row234);
            $("#Row235").val(dataItemX.Row235);
            $("#Row236").val(dataItemX.Row236);
            $("#Row237").val(dataItemX.Row237);
            $("#Row238").val(dataItemX.Row238);
            $("#Row239").val(dataItemX.Row239);
            $("#Row240").val(dataItemX.Row240);
            $("#Row241").val(dataItemX.Row241);
            $("#Row242").val(dataItemX.Row242);
            $("#Row243").val(dataItemX.Row243);
            $("#Row244").val(dataItemX.Row244);
            $("#Row245").val(dataItemX.Row245);
            $("#Row246").val(dataItemX.Row246);
            $("#Row247").val(dataItemX.Row247);
            $("#Row248").val(dataItemX.Row248);
            $("#Row249").val(dataItemX.Row249);
            $("#Row250").val(dataItemX.Row250);
            $("#Row251").val(dataItemX.Row251);


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
        $("#Row1").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow1()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row1;
            }
        }

        $("#Row2").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow2()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row2;
            }
        }

        $("#Row3").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow3()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row3;
            }
        }

        $("#Row4").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow4()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row4;
            }
        }

        $("#Row5").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow5()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row5;
            }
        }

        $("#Row6").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow6()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow6() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row6;
            }
        }

        $("#Row7").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow7()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow7() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row7;
            }
        }

        $("#Row8").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow8()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow8() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row8;
            }
        }

        $("#Row9").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow9()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow9() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row9;
            }
        }

        $("#Row10").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow10()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow10() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row10;
            }
        }

        //11-20
        $("#Row11").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow11()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow11() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row11;
            }
        }

        $("#Row12").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow12()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow12() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row12;
            }
        }

        $("#Row13").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow13()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow13() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row13;
            }
        }

        $("#Row14").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow14()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow14() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row14;
            }
        }

        $("#Row15").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow15()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow15() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row15;
            }
        }

        $("#Row16").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow16()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow16() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row16;
            }
        }

        $("#Row17").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow17()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow17() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row17;
            }
        }

        $("#Row18").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow18()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow18() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row18;
            }
        }

        $("#Row19").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow19()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow19() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row19;
            }
        }

        $("#Row20").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow20()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow20() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row20;
            }
        }

        $("#Row21").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow21()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow21() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row21;
            }
        }

        $("#Row22").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow22()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow22() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row22;
            }
        }

        $("#Row23").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow23()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow23() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row23;
            }
        }

        $("#Row24").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow24()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow24() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row24;
            }
        }

        $("#Row25").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow25()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow25() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row25;
            }
        }

        $("#Row26").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow26()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow26() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row16;
            }
        }

        $("#Row27").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow27()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow27() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row27;
            }
        }

        $("#Row28").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow28()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow28() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row28;
            }
        }

        $("#Row29").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow29()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow29() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row29;
            }
        }

        $("#Row30").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow30()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow30() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row30;
            }
        }

       

        $("#Row31").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },

            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow31()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow31() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row31;
            }
        }

        $("#Row32").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow32()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow32() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row32;
            }
        }

        $("#Row33").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow33()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow33() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row33;
            }
        }

        $("#Row34").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow34()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow34() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row34;
            }
        }

        $("#Row35").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow35()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow35() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row35;
            }
        }

        $("#Row36").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow36()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow36() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row36;
            }
        }

        $("#Row37").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow37()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow37() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row37;
            }
        }

        $("#Row38").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow38()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow38() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row38;
            }
        }

        $("#Row39").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow39()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow39() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row39;
            }
        }

        $("#Row40").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow40()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow40() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row40;
            }
        }

        $("#Row41").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow41()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow41() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row41;
            }
        }

        $("#Row42").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow42()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow42() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row42;
            }
        }

        $("#Row43").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow43()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow43() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row43;
            }
        }

        $("#Row44").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow44()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow44() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row44;
            }
        }

        $("#Row45").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow45()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow45() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row45;
            }
        }

        $("#Row46").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow46()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow46() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row46;
            }
        }

        $("#Row47").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow47()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow47() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row47;
            }
        }

        $("#Row48").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow48()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow48() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row48;
            }
        }

        $("#Row49").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow49()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow49() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row49;
            }
        }

        $("#Row50").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow50()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow50() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row50;
            }
        }

        $("#Row51").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow51()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow51() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row51;
            }
        }

        $("#Row52").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow52()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow52() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row52;
            }
        }

        $("#Row53").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow53()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow53() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row53;
            }
        }

        $("#Row54").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow54()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow54() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row54;
            }
        }

        $("#Row55").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow55()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow55() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row55;
            }
        }

        $("#Row56").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow56()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow56() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row56;
            }
        }

        $("#Row57").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow57()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow57() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row57;
            }
        }

        $("#Row58").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow58()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow58() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row58;
            }
        }

        $("#Row59").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow59()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow59() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row59;
            }
        }

        $("#Row60").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow60()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow60() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row60;
            }
        }

        $("#Row61").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow61()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow61() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row61;
            }
        }

        $("#Row62").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow62()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow62() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row62;
            }
        }

        $("#Row63").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow63()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow63() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row63;
            }
        }

        $("#Row64").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow64()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow64() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row64;
            }
        }

        $("#Row65").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow65()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow65() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row65;
            }
        }

        $("#Row66").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow66()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow66() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row66;
            }
        }

        $("#Row67").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow67()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow67() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row67;
            }
        }

        $("#Row68").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow68()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow68() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row68;
            }
        }

        $("#Row69").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow69()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow69() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row69;
            }
        }

        $("#Row70").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow70()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow70() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row70;
            }
        }

        $("#Row71").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow71()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow71() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row71;
            }
        }

        $("#Row72").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow72()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow72() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row72;
            }
        }

        $("#Row73").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow73()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow73() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row73;
            }
        }

        $("#Row74").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow74()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow74() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row74;
            }
        }

        $("#Row75").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow75()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow75() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row75;
            }
        }

        $("#Row76").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow76()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow76() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row76;
            }
        }

        $("#Row77").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow77()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow77() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row77;
            }
        }

        $("#Row78").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow78()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow78() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row78;
            }
        }

        $("#Row79").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow79()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow79() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row79;
            }
        }

        $("#Row80").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow80()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow80() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row80;
            }
        }

        $("#Row81").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow81()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow81() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row81;
            }
        }

        $("#Row82").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow82()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow82() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row82;
            }
        }

        $("#Row83").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow83()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow83() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row83;
            }
        }

        $("#Row84").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow84()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow84() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row84;
            }
        }

        $("#Row85").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow85()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow85() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row85;
            }
        }

        $("#Row86").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow86()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow86() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row86;
            }
        }

        $("#Row87").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow87()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow87() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row87;
            }
        }

        $("#Row88").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow88()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow88() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row88;
            }
        }

        $("#Row89").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow89()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow89() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row89;
            }
        }

        $("#Row90").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow90()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow90() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row90;
            }
        }
       
        $("#Row91").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow91()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow91() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row91;
            }
        }

        $("#Row92").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow92()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow92() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row92;
            }
        }

        $("#Row93").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow93()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow93() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row93;
            }
        }

        $("#Row94").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow94()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow94() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row94;
            }
        }

        $("#Row95").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow95()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow95() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row95;
            }
        }

        $("#Row96").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow96()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow96() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row96;
            }
        }

        $("#Row97").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow97()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow97() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row97;
            }
        }

        $("#Row98").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow98()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow98() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row98;
            }
        }

        $("#Row99").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow99()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow99() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row99;
            }
        }

        $("#Row100").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                 { text: "3", value: 3 },
                { text: "4", value: 4 },
                 { text: "5", value: 5 },
            ],
            filter: "contains",
            //change: OnChangApprove1,
            value: setCmbRow100()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbRow100() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row100;
            }
        }

        $("#Row101").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow101()
        });

        function setCmbRow101() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row101;
            }
        }
        $("#Row102").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow102()
        });

        function setCmbRow102() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row102;
            }
        }
        $("#Row103").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow103()
        });

        function setCmbRow103() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row103;
            }
        }
        $("#Row104").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow104()
        });

        function setCmbRow104() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row104;
            }
        }
        $("#Row105").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow105()
        });

        function setCmbRow105() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row105;
            }
        }
        $("#Row106").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow106()
        });

        function setCmbRow106() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row106;
            }
        }
        $("#Row107").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow107()
        });

        function setCmbRow107() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row107;
            }
        }
        $("#Row108").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow108()
        });

        function setCmbRow108() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row108;
            }
        }
        $("#Row109").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow109()
        });

        function setCmbRow109() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row109;
            }
        }
        $("#Row110").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow110()
        });

        function setCmbRow110() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row110;
            }
        }
        $("#Row111").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow111()
        });

        function setCmbRow111() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row111;
            }
        }
        $("#Row112").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow112()
        });

        function setCmbRow112() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row112;
            }
        }
        $("#Row113").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow113()
        });

        function setCmbRow113() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row113;
            }
        }
        $("#Row114").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow114()
        });

        function setCmbRow114() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row114;
            }
        }
        $("#Row115").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow115()
        });

        function setCmbRow115() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row115;
            }
        }
        $("#Row116").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow116()
        });

        function setCmbRow116() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row116;
            }
        }
        $("#Row117").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow117()
        });

        function setCmbRow117() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row117;
            }
        }
        $("#Row118").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow118()
        });

        function setCmbRow118() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row118;
            }
        }
        $("#Row119").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow119()
        });

        function setCmbRow119() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row119;
            }
        }
        $("#Row120").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow120()
        });

        function setCmbRow120() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row120;
            }
        }
        $("#Row121").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow121()
        });

        function setCmbRow121() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row121;
            }
        }
        $("#Row122").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow122()
        });

        function setCmbRow122() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row122;
            }
        }
        $("#Row123").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow123()
        });

        function setCmbRow123() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row123;
            }
        }
        $("#Row124").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow124()
        });

        function setCmbRow124() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row124;
            }
        }
        $("#Row125").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow125()
        });

        function setCmbRow125() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row125;
            }
        }
        $("#Row126").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow126()
        });

        function setCmbRow126() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row126;
            }
        }
        $("#Row127").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow127()
        });

        function setCmbRow127() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row127;
            }
        }
        $("#Row128").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow128()
        });

        function setCmbRow128() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row128;
            }
        }
        $("#Row129").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow129()
        });

        function setCmbRow129() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row129;
            }
        }
        $("#Row130").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow130()
        });

        function setCmbRow130() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row130;
            }
        }
        $("#Row131").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow131()
        });

        function setCmbRow131() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row131;
            }
        }
        $("#Row132").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow132()
        });

        function setCmbRow132() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row132;
            }
        }
        $("#Row133").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow133()
        });

        function setCmbRow133() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row133;
            }
        }
        $("#Row134").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow134()
        });

        function setCmbRow134() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row134;
            }
        }
        $("#Row135").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow135()
        });

        function setCmbRow135() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row135;
            }
        }
        $("#Row136").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow136()
        });

        function setCmbRow136() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row136;
            }
        }
        $("#Row137").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow137()
        });

        function setCmbRow137() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row137;
            }
        }
        $("#Row138").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow138()
        });

        function setCmbRow138() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row138;
            }
        }
        $("#Row139").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow139()
        });

        function setCmbRow139() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row139;
            }
        }
        $("#Row140").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow140()
        });

        function setCmbRow140() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row140;
            }
        }
        $("#Row141").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow141()
        });

        function setCmbRow141() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row141;
            }
        }
        $("#Row142").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow142()
        });

        function setCmbRow142() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row142;
            }
        }
        $("#Row143").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow143()
        });

        function setCmbRow143() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row143;
            }
        }
        $("#Row144").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow144()
        });

        function setCmbRow144() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row144;
            }
        }
        $("#Row145").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow145()
        });

        function setCmbRow145() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row145;
            }
        }
        $("#Row146").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow146()
        });

        function setCmbRow146() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row146;
            }
        }
        $("#Row147").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow147()
        });

        function setCmbRow147() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row147;
            }
        }
        $("#Row148").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow148()
        });

        function setCmbRow148() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row148;
            }
        }
        $("#Row149").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow149()
        });

        function setCmbRow149() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row149;
            }
        }
        $("#Row150").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow150()
        });

        function setCmbRow150() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row150;
            }
        }
        $("#Row151").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow151()
        });

        function setCmbRow151() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row151;
            }
        }
        $("#Row152").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow152()
        });

        function setCmbRow152() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row152;
            }
        }
        $("#Row153").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow153()
        });

        function setCmbRow153() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row153;
            }
        }
        $("#Row154").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow154()
        });

        function setCmbRow154() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row154;
            }
        }
        $("#Row155").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow155()
        });

        function setCmbRow155() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row155;
            }
        }
        $("#Row156").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow156()
        });

        function setCmbRow156() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row156;
            }
        }
        $("#Row157").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow157()
        });

        function setCmbRow157() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row157;
            }
        }
        $("#Row158").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow158()
        });

        function setCmbRow158() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row158;
            }
        }
        $("#Row159").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow159()
        });

        function setCmbRow159() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row159;
            }
        }
        $("#Row160").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow160()
        });

        function setCmbRow160() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row160;
            }
        }
        $("#Row161").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow161()
        });

        function setCmbRow161() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row161;
            }
        }
        $("#Row162").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow162()
        });

        function setCmbRow162() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row162;
            }
        }
        $("#Row163").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow163()
        });

        function setCmbRow163() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row163;
            }
        }
        $("#Row164").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow164()
        });

        function setCmbRow164() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row164;
            }
        }
        $("#Row165").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow165()
        });

        function setCmbRow165() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row165;
            }
        }
        $("#Row166").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow166()
        });

        function setCmbRow166() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row166;
            }
        }
        $("#Row167").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow167()
        });

        function setCmbRow167() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row167;
            }
        }
        $("#Row168").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow168()
        });

        function setCmbRow168() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row168;
            }
        }
        $("#Row169").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow169()
        });

        function setCmbRow169() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row169;
            }
        }
        $("#Row170").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow170()
        });

        function setCmbRow170() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row170;
            }
        }
        $("#Row171").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow171()
        });

        function setCmbRow171() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row171;
            }
        }
        $("#Row172").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow172()
        });

        function setCmbRow172() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row172;
            }
        }
        $("#Row173").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow173()
        });

        function setCmbRow173() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row173;
            }
        }
        $("#Row174").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow174()
        });

        function setCmbRow174() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row174;
            }
        }
        $("#Row175").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow175()
        });

        function setCmbRow175() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row175;
            }
        }
        $("#Row176").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow176()
        });

        function setCmbRow176() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row176;
            }
        }
        $("#Row177").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow177()
        });

        function setCmbRow177() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row177;
            }
        }
        $("#Row178").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow178()
        });

        function setCmbRow178() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row178;
            }
        }
        $("#Row179").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow179()
        });

        function setCmbRow179() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row179;
            }
        }
        $("#Row180").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow180()
        });

        function setCmbRow180() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row180;
            }
        }
        $("#Row181").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow181()
        });

        function setCmbRow181() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row181;
            }
        }
        $("#Row182").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow182()
        });

        function setCmbRow182() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row182;
            }
        }
        $("#Row183").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow183()
        });

        function setCmbRow183() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row183;
            }
        }
        $("#Row184").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow184()
        });

        function setCmbRow184() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row184;
            }
        }
        $("#Row185").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow185()
        });

        function setCmbRow185() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row185;
            }
        }
        $("#Row186").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow186()
        });

        function setCmbRow186() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row186;
            }
        }
        $("#Row187").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow187()
        });

        function setCmbRow187() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row187;
            }
        }
        $("#Row188").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow188()
        });

        function setCmbRow188() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row188;
            }
        }
        $("#Row189").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow189()
        });

        function setCmbRow189() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row189;
            }
        }
        $("#Row190").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow190()
        });

        function setCmbRow190() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row190;
            }
        }
        $("#Row191").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow191()
        });

        function setCmbRow191() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row191;
            }
        }
        $("#Row192").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow192()
        });

        function setCmbRow192() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row192;
            }
        }
        $("#Row193").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow193()
        });

        function setCmbRow193() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row193;
            }
        }
        $("#Row194").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow194()
        });

        function setCmbRow194() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row194;
            }
        }
        $("#Row195").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow195()
        });

        function setCmbRow195() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row195;
            }
        }
        $("#Row196").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow196()
        });

        function setCmbRow196() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row196;
            }
        }
        $("#Row197").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow197()
        });

        function setCmbRow197() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row197;
            }
        }
        $("#Row198").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow198()
        });

        function setCmbRow198() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row198;
            }
        }
        $("#Row199").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow199()
        });

        function setCmbRow199() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row199;
            }
        }
        $("#Row200").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow200()
        });

        function setCmbRow200() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row200;
            }
        }
        $("#Row201").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow201()
        });

        function setCmbRow201() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row201;
            }
        }
        $("#Row202").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow202()
        });

        function setCmbRow202() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row202;
            }
        }
        $("#Row203").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow203()
        });

        function setCmbRow203() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row203;
            }
        }
        $("#Row204").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow204()
        });

        function setCmbRow204() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row204;
            }
        }
        $("#Row205").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow205()
        });

        function setCmbRow205() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row205;
            }
        }
        $("#Row206").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow206()
        });

        function setCmbRow206() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row206;
            }
        }
        $("#Row207").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow207()
        });

        function setCmbRow207() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row207;
            }
        }
        $("#Row208").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow208()
        });

        function setCmbRow208() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row208;
            }
        }
        $("#Row209").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow209()
        });

        function setCmbRow209() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row209;
            }
        }
        $("#Row210").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow210()
        });

        function setCmbRow210() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row210;
            }
        }
        $("#Row211").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow211()
        });

        function setCmbRow211() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row211;
            }
        }
        $("#Row212").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow212()
        });

        function setCmbRow212() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row212;
            }
        }
        $("#Row213").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow213()
        });

        function setCmbRow213() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row213;
            }
        }
        $("#Row214").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow214()
        });

        function setCmbRow214() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row214;
            }
        }
        $("#Row215").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow215()
        });

        function setCmbRow215() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row215;
            }
        }
        $("#Row216").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow216()
        });

        function setCmbRow216() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row216;
            }
        }
        $("#Row217").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow217()
        });

        function setCmbRow217() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row217;
            }
        }
        $("#Row218").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow218()
        });

        function setCmbRow218() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row218;
            }
        }
        $("#Row219").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow219()
        });

        function setCmbRow219() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row219;
            }
        }
        $("#Row220").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow220()
        });

        function setCmbRow220() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row220;
            }
        }
        $("#Row221").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow221()
        });

        function setCmbRow221() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row221;
            }
        }
        $("#Row222").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow222()
        });

        function setCmbRow222() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row222;
            }
        }
        $("#Row223").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow223()
        });

        function setCmbRow223() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row223;
            }
        }
        $("#Row224").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow224()
        });

        function setCmbRow224() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row224;
            }
        }
        $("#Row225").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow225()
        });

        function setCmbRow225() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row225;
            }
        }
        $("#Row226").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow226()
        });

        function setCmbRow226() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row226;
            }
        }
        $("#Row227").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow227()
        });

        function setCmbRow227() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row227;
            }
        }
        $("#Row228").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow228()
        });

        function setCmbRow228() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row228;
            }
        }
        $("#Row229").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow229()
        });

        function setCmbRow229() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row229;
            }
        }
        $("#Row230").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow230()
        });

        function setCmbRow230() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row230;
            }
        }
        $("#Row231").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow231()
        });

        function setCmbRow231() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row231;
            }
        }
        $("#Row232").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow232()
        });

        function setCmbRow232() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row232;
            }
        }
        $("#Row233").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow233()
        });

        function setCmbRow233() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row233;
            }
        }
        $("#Row234").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow234()
        });

        function setCmbRow234() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row234;
            }
        }
        $("#Row235").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow235()
        });

        function setCmbRow235() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row235;
            }
        }
        $("#Row236").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow236()
        });

        function setCmbRow236() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row236;
            }
        }
        $("#Row237").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow237()
        });

        function setCmbRow237() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row237;
            }
        }
        $("#Row238").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow238()
        });

        function setCmbRow238() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row238;
            }
        }
        $("#Row239").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow239()
        });

        function setCmbRow239() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row239;
            }
        }
        $("#Row240").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow240()
        });

        function setCmbRow240() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row240;
            }
        }
        $("#Row241").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow241()
        });

        function setCmbRow241() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row241;
            }
        }
        $("#Row242").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow242()
        });

        function setCmbRow242() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row242;
            }
        }
        $("#Row243").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow243()
        });

        function setCmbRow243() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row243;
            }
        }
        $("#Row244").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow244()
        });

        function setCmbRow244() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row244;
            }
        }
        $("#Row245").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow245()
        });

        function setCmbRow245() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row245;
            }
        }
        $("#Row246").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow246()
        });

        function setCmbRow246() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row246;
            }
        }
        $("#Row247").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow247()
        });

        function setCmbRow247() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row247;
            }
        }
        $("#Row248").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow248()
        });

        function setCmbRow248() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row248;
            }
        }
        $("#Row249").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow249()
        });

        function setCmbRow249() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row249;
            }
        }
        $("#Row250").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow250()
        });

        function setCmbRow250() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row250;
            }
        }
        $("#Row251").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{
                text: "1",
                value: 1
            },
             {
                 text: "2",
                 value: 2
             },
            ],
            filter: "contains",
            value: setCmbRow251()
        });

        function setCmbRow251() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Row251;
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
        $("#CorporateGovAMPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").val("");
        $("#Row1").val("");
        $("#Row2").val("");
        $("#Row3").val("");
        $("#Row4").val("");
        $("#Row5").val("");
        $("#Row6").val("");
        $("#Row7").val("");
        $("#Row8").val("");
        $("#Row9").val("");
        $("#Row10").val("");
        $("#Row11").val("");
        $("#Row12").val("");
        $("#Row13").val("");
        $("#Row14").val("");
        $("#Row15").val("");
        $("#Row16").val("");
        $("#Row17").val("");
        $("#Row18").val("");
        $("#Row19").val("");
        $("#Row20").val("");
        $("#Row21").val("");
        $("#Row22").val("");
        $("#Row23").val("");
        $("#Row24").val("");
        $("#Row25").val("");
        $("#Row26").val("");
        $("#Row27").val("");
        $("#Row28").val("");
        $("#Row29").val("");
        $("#Row30").val("");
        $("#Row31").val("");
        $("#Row32").val("");
        $("#Row33").val("");
        $("#Row34").val("");
        $("#Row35").val("");
        $("#Row36").val("");
        $("#Row37").val("");
        $("#Row38").val("");
        $("#Row39").val("");
        $("#Row40").val("");
        $("#Row41").val("");
        $("#Row42").val("");
        $("#Row43").val("");
        $("#Row44").val("");
        $("#Row45").val("");
        $("#Row46").val("");
        $("#Row47").val("");
        $("#Row48").val("");
        $("#Row49").val("");
        $("#Row50").val("");
        $("#Row51").val("");
        $("#Row52").val("");
        $("#Row53").val("");
        $("#Row54").val("");
        $("#Row55").val("");
        $("#Row56").val("");
        $("#Row57").val("");
        $("#Row58").val("");
        $("#Row59").val("");
        $("#Row60").val("");
        $("#Row61").val("");
        $("#Row62").val("");
        $("#Row63").val("");
        $("#Row64").val("");
        $("#Row65").val("");
        $("#Row66").val("");
        $("#Row67").val("");
        $("#Row68").val("");
        $("#Row69").val("");
        $("#Row70").val("");
        $("#Row71").val("");
        $("#Row72").val("");
        $("#Row73").val("");
        $("#Row74").val("");
        $("#Row75").val("");
        $("#Row76").val("");
        $("#Row77").val("");
        $("#Row78").val("");
        $("#Row79").val("");
        $("#Row80").val("");
        $("#Row81").val("");
        $("#Row82").val("");
        $("#Row83").val("");
        $("#Row84").val("");
        $("#Row85").val("");
        $("#Row86").val("");
        $("#Row87").val("");
        $("#Row88").val("");
        $("#Row89").val("");
        $("#Row90").val("");
        $("#Row91").val("");
        $("#Row92").val("");
        $("#Row93").val("");
        $("#Row94").val("");
        $("#Row95").val("");
        $("#Row96").val("");
        $("#Row97").val("");
        $("#Row98").val("");
        $("#Row99").val("");
        $("#Row100").val("");

        $("#Row101").val("");
        $("#Row102").val("");
        $("#Row103").val("");
        $("#Row104").val("");
        $("#Row105").val("");
        $("#Row106").val("");
        $("#Row107").val("");
        $("#Row108").val("");
        $("#Row109").val("");
        $("#Row110").val("");
        $("#Row111").val("");
        $("#Row112").val("");
        $("#Row113").val("");
        $("#Row114").val("");
        $("#Row115").val("");
        $("#Row116").val("");
        $("#Row117").val("");
        $("#Row118").val("");
        $("#Row119").val("");
        $("#Row120").val("");
        $("#Row121").val("");
        $("#Row122").val("");
        $("#Row123").val("");
        $("#Row124").val("");
        $("#Row125").val("");
        $("#Row126").val("");
        $("#Row127").val("");
        $("#Row128").val("");
        $("#Row129").val("");
        $("#Row130").val("");
        $("#Row131").val("");
        $("#Row132").val("");
        $("#Row133").val("");
        $("#Row134").val("");
        $("#Row135").val("");
        $("#Row136").val("");
        $("#Row137").val("");
        $("#Row138").val("");
        $("#Row139").val("");
        $("#Row140").val("");
        $("#Row141").val("");
        $("#Row142").val("");
        $("#Row143").val("");
        $("#Row144").val("");
        $("#Row145").val("");
        $("#Row146").val("");
        $("#Row147").val("");
        $("#Row148").val("");
        $("#Row149").val("");
        $("#Row150").val("");
        $("#Row151").val("");
        $("#Row152").val("");
        $("#Row153").val("");
        $("#Row154").val("");
        $("#Row155").val("");
        $("#Row156").val("");
        $("#Row157").val("");
        $("#Row158").val("");
        $("#Row159").val("");
        $("#Row160").val("");
        $("#Row161").val("");
        $("#Row162").val("");
        $("#Row163").val("");
        $("#Row164").val("");
        $("#Row165").val("");
        $("#Row166").val("");
        $("#Row167").val("");
        $("#Row168").val("");
        $("#Row169").val("");
        $("#Row170").val("");
        $("#Row171").val("");
        $("#Row172").val("");
        $("#Row173").val("");
        $("#Row174").val("");
        $("#Row175").val("");
        $("#Row176").val("");
        $("#Row177").val("");
        $("#Row178").val("");
        $("#Row179").val("");
        $("#Row180").val("");
        $("#Row181").val("");
        $("#Row182").val("");
        $("#Row183").val("");
        $("#Row184").val("");
        $("#Row185").val("");
        $("#Row186").val("");
        $("#Row187").val("");
        $("#Row188").val("");
        $("#Row189").val("");
        $("#Row190").val("");
        $("#Row191").val("");
        $("#Row192").val("");
        $("#Row193").val("");
        $("#Row194").val("");
        $("#Row195").val("");
        $("#Row196").val("");
        $("#Row197").val("");
        $("#Row198").val("");
        $("#Row199").val("");
        $("#Row200").val("");

        $("#Row201").val("");
        $("#Row202").val("");
        $("#Row203").val("");
        $("#Row204").val("");
        $("#Row205").val("");
        $("#Row206").val("");
        $("#Row207").val("");
        $("#Row208").val("");
        $("#Row209").val("");
        $("#Row210").val("");
        $("#Row211").val("");
        $("#Row212").val("");
        $("#Row213").val("");
        $("#Row214").val("");
        $("#Row215").val("");
        $("#Row216").val("");
        $("#Row217").val("");
        $("#Row218").val("");
        $("#Row219").val("");
        $("#Row220").val("");
        $("#Row221").val("");
        $("#Row222").val("");
        $("#Row223").val("");
        $("#Row224").val("");
        $("#Row225").val("");
        $("#Row226").val("");
        $("#Row227").val("");
        $("#Row228").val("");
        $("#Row229").val("");
        $("#Row230").val("");
        $("#Row231").val("");
        $("#Row232").val("");
        $("#Row233").val("");
        $("#Row234").val("");
        $("#Row235").val("");
        $("#Row236").val("");
        $("#Row237").val("");
        $("#Row238").val("");
        $("#Row239").val("");
        $("#Row240").val("");
        $("#Row241").val("");
        $("#Row242").val("");
        $("#Row243").val("");
        $("#Row244").val("");
        $("#Row245").val("");
        $("#Row246").val("");
        $("#Row247").val("");
        $("#Row248").val("");
        $("#Row249").val("");
        $("#Row250").val("");
        $("#Row251").val("");

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
                            CorporateGovAMPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Date: { type: "string" },
                            Row1: { type: "string" },
                            Row2: { type: "string" },
                            Row3: { type: "string" },
                            Row4: { type: "string" },
                            Row5: { type: "string" },
                            Row6: { type: "string" },
                            Row7: { type: "string" },
                            Row8: { type: "string" },
                            Row9: { type: "string" },
                            Row10: { type: "string" },
                            Row11: { type: "string" },
                            Row12: { type: "string" },
                            Row13: { type: "string" },
                            Row14: { type: "string" },
                            Row15: { type: "string" },
                            Row16: { type: "string" },
                            Row17: { type: "string" },
                            Row18: { type: "string" },
                            Row19: { type: "string" },
                            Row20: { type: "string" },
                            Row21: { type: "string" },
                            Row22: { type: "string" },
                            Row23: { type: "string" },
                            Row24: { type: "string" },
                            Row25: { type: "string" },
                            Row26: { type: "string" },
                            Row27: { type: "string" },
                            Row28: { type: "string" },
                            Row29: { type: "string" },
                            Row30: { type: "string" },
                            Row31: { type: "string" },
                            Row32: { type: "string" },
                            Row33: { type: "string" },
                            Row34: { type: "string" },
                            Row35: { type: "string" },
                            Row36: { type: "string" },
                            Row37: { type: "string" },
                            Row38: { type: "string" },
                            Row39: { type: "string" },
                            Row40: { type: "string" },
                            Row41: { type: "string" },
                            Row42: { type: "string" },
                            Row43: { type: "string" },
                            Row44: { type: "string" },
                            Row45: { type: "string" },
                            Row46: { type: "string" },
                            Row47: { type: "string" },
                            Row48: { type: "string" },
                            Row49: { type: "string" },
                            Row50: { type: "string" },
                            Row51: { type: "string" },
                            Row52: { type: "string" },
                            Row53: { type: "string" },
                            Row54: { type: "string" },
                            Row55: { type: "string" },
                            Row56: { type: "string" },
                            Row57: { type: "string" },
                            Row58: { type: "string" },
                            Row59: { type: "string" },
                            Row60: { type: "string" },
                            Row61: { type: "string" },
                            Row62: { type: "string" },
                            Row63: { type: "string" },
                            Row64: { type: "string" },
                            Row65: { type: "string" },
                            Row66: { type: "string" },
                            Row67: { type: "string" },
                            Row68: { type: "string" },
                            Row69: { type: "string" },
                            Row70: { type: "string" },
                            Row71: { type: "string" },
                            Row72: { type: "string" },
                            Row73: { type: "string" },
                            Row74: { type: "string" },
                            Row75: { type: "string" },
                            Row76: { type: "string" },
                            Row77: { type: "string" },
                            Row78: { type: "string" },
                            Row79: { type: "string" },
                            Row80: { type: "string" },
                            Row81: { type: "string" },
                            Row82: { type: "string" },
                            Row83: { type: "string" },
                            Row84: { type: "string" },
                            Row85: { type: "string" },
                            Row86: { type: "string" },
                            Row87: { type: "string" },
                            Row88: { type: "string" },
                            Row89: { type: "string" },
                            Row90: { type: "string" },
                            Row91: { type: "string" },
                            Row92: { type: "string" },
                            Row93: { type: "string" },
                            Row94: { type: "string" },
                            Row95: { type: "string" },
                            Row96: { type: "string" },
                            Row97: { type: "string" },
                            Row98: { type: "string" },
                            Row99: { type: "string" },
                            Row100: { type: "string" },

                            Row101: { type: "string" },
                            Row102: { type: "string" },
                            Row103: { type: "string" },
                            Row104: { type: "string" },
                            Row105: { type: "string" },
                            Row106: { type: "string" },
                            Row107: { type: "string" },
                            Row108: { type: "string" },
                            Row109: { type: "string" },
                            Row110: { type: "string" },
                            Row111: { type: "string" },
                            Row112: { type: "string" },
                            Row113: { type: "string" },
                            Row114: { type: "string" },
                            Row115: { type: "string" },
                            Row116: { type: "string" },
                            Row117: { type: "string" },
                            Row118: { type: "string" },
                            Row119: { type: "string" },
                            Row120: { type: "string" },
                            Row121: { type: "string" },
                            Row122: { type: "string" },
                            Row123: { type: "string" },
                            Row124: { type: "string" },
                            Row125: { type: "string" },
                            Row126: { type: "string" },
                            Row127: { type: "string" },
                            Row128: { type: "string" },
                            Row129: { type: "string" },
                            Row130: { type: "string" },
                            Row131: { type: "string" },
                            Row132: { type: "string" },
                            Row133: { type: "string" },
                            Row134: { type: "string" },
                            Row135: { type: "string" },
                            Row136: { type: "string" },
                            Row137: { type: "string" },
                            Row138: { type: "string" },
                            Row139: { type: "string" },
                            Row140: { type: "string" },
                            Row141: { type: "string" },
                            Row142: { type: "string" },
                            Row143: { type: "string" },
                            Row144: { type: "string" },
                            Row145: { type: "string" },
                            Row146: { type: "string" },
                            Row147: { type: "string" },
                            Row148: { type: "string" },
                            Row149: { type: "string" },
                            Row150: { type: "string" },
                            Row151: { type: "string" },
                            Row152: { type: "string" },
                            Row153: { type: "string" },
                            Row154: { type: "string" },
                            Row155: { type: "string" },
                            Row156: { type: "string" },
                            Row157: { type: "string" },
                            Row158: { type: "string" },
                            Row159: { type: "string" },
                            Row160: { type: "string" },
                            Row161: { type: "string" },
                            Row162: { type: "string" },
                            Row163: { type: "string" },
                            Row164: { type: "string" },
                            Row165: { type: "string" },
                            Row166: { type: "string" },
                            Row167: { type: "string" },
                            Row168: { type: "string" },
                            Row169: { type: "string" },
                            Row170: { type: "string" },
                            Row171: { type: "string" },
                            Row172: { type: "string" },
                            Row173: { type: "string" },
                            Row174: { type: "string" },
                            Row175: { type: "string" },
                            Row176: { type: "string" },
                            Row177: { type: "string" },
                            Row178: { type: "string" },
                            Row179: { type: "string" },
                            Row180: { type: "string" },
                            Row181: { type: "string" },
                            Row182: { type: "string" },
                            Row183: { type: "string" },
                            Row184: { type: "string" },
                            Row185: { type: "string" },
                            Row186: { type: "string" },
                            Row187: { type: "string" },
                            Row188: { type: "string" },
                            Row189: { type: "string" },
                            Row190: { type: "string" },
                            Row191: { type: "string" },
                            Row192: { type: "string" },
                            Row193: { type: "string" },
                            Row194: { type: "string" },
                            Row195: { type: "string" },
                            Row196: { type: "string" },
                            Row197: { type: "string" },
                            Row198: { type: "string" },
                            Row199: { type: "string" },
                            Row200: { type: "string" },

                            Row201: { type: "string" },
                            Row202: { type: "string" },
                            Row203: { type: "string" },
                            Row204: { type: "string" },
                            Row205: { type: "string" },
                            Row206: { type: "string" },
                            Row207: { type: "string" },
                            Row208: { type: "string" },
                            Row209: { type: "string" },
                            Row210: { type: "string" },
                            Row211: { type: "string" },
                            Row212: { type: "string" },
                            Row213: { type: "string" },
                            Row214: { type: "string" },
                            Row215: { type: "string" },
                            Row216: { type: "string" },
                            Row217: { type: "string" },
                            Row218: { type: "string" },
                            Row219: { type: "string" },
                            Row220: { type: "string" },
                            Row221: { type: "string" },
                            Row222: { type: "string" },
                            Row223: { type: "string" },
                            Row224: { type: "string" },
                            Row225: { type: "string" },
                            Row226: { type: "string" },
                            Row227: { type: "string" },
                            Row228: { type: "string" },
                            Row229: { type: "string" },
                            Row230: { type: "string" },
                            Row231: { type: "string" },
                            Row232: { type: "string" },
                            Row233: { type: "string" },
                            Row234: { type: "string" },
                            Row235: { type: "string" },
                            Row236: { type: "string" },
                            Row237: { type: "string" },
                            Row238: { type: "string" },
                            Row239: { type: "string" },
                            Row240: { type: "string" },
                            Row241: { type: "string" },
                            Row242: { type: "string" },
                            Row243: { type: "string" },
                            Row244: { type: "string" },
                            Row245: { type: "string" },
                            Row246: { type: "string" },
                            Row247: { type: "string" },
                            Row248: { type: "string" },
                            Row249: { type: "string" },
                            Row250: { type: "string" },
                            Row251: { type: "string" },


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
            var gridApproved = $("#gridCorporateGovAMApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCorporateGovAMPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCorporateGovAMHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CorporateGovAMApprovedURL = window.location.origin + "/Radsoft/CorporateGovAM/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(CorporateGovAMApprovedURL);

        $("#gridCorporateGovAMApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form CorporateGovAM"
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
                { field: "CorporateGovAMPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                //{ field: "Date", title: "Date", width: 200 },
                { field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
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
        $("#TabCorporateGovAM").kendoTabStrip({
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
                        var CorporateGovAMPendingURL = window.location.origin + "/Radsoft/CorporateGovAM/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(CorporateGovAMPendingURL);
                        $("#gridCorporateGovAMPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CorporateGovAM"
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
                                { field: "CorporateGovAMPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                //{ field: "Date", title: "Date", width: 200 },
                                { field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
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

                        var CorporateGovAMHistoryURL = window.location.origin + "/Radsoft/CorporateGovAM/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(CorporateGovAMHistoryURL);

                        $("#gridCorporateGovAMHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CorporateGovAM"
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
                                { field: "CorporateGovAMPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                //{ field: "Date", title: "Date", width: 200 },
                                { field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 180 },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

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
        var grid = $("#gridCorporateGovAMHistory").data("kendoGrid");
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



                    var CorporateGovAM = {
                        Date: $('#Date').val(),
                        Row1: $('#Row1').val(),
                        Row2: $('#Row2').val(),
                        Row3: $('#Row3').val(),
                        Row4: $('#Row4').val(),
                        Row5: $('#Row5').val(),
                        Row6: $('#Row6').val(),
                        Row7: $('#Row7').val(),
                        Row8: $('#Row8').val(),
                        Row9: $('#Row9').val(),
                        Row10: $('#Row10').val(),
                        Row11: $('#Row11').val(),
                        Row12: $('#Row12').val(),
                        Row13: $('#Row13').val(),
                        Row14: $('#Row14').val(),
                        Row15: $('#Row15').val(),
                        Row16: $('#Row16').val(),
                        Row17: $('#Row17').val(),
                        Row18: $('#Row18').val(),
                        Row19: $('#Row19').val(),
                        Row20: $('#Row20').val(),
                        Row21: $('#Row21').val(),
                        Row22: $('#Row22').val(),
                        Row23: $('#Row23').val(),
                        Row24: $('#Row24').val(),
                        Row25: $('#Row25').val(),
                        Row26: $('#Row26').val(),
                        Row27: $('#Row27').val(),
                        Row28: $('#Row28').val(),
                        Row29: $('#Row29').val(),
                        Row30: $('#Row30').val(),
                        Row31: $('#Row31').val(),
                        Row32: $('#Row32').val(),
                        Row33: $('#Row33').val(),
                        Row34: $('#Row34').val(),
                        Row35: $('#Row35').val(),
                        Row36: $('#Row36').val(),
                        Row37: $('#Row37').val(),
                        Row38: $('#Row38').val(),
                        Row39: $('#Row39').val(),
                        Row40: $('#Row40').val(),
                        Row41: $('#Row41').val(),
                        Row42: $('#Row42').val(),
                        Row43: $('#Row43').val(),
                        Row44: $('#Row44').val(),
                        Row45: $('#Row45').val(),
                        Row46: $('#Row46').val(),
                        Row47: $('#Row47').val(),
                        Row48: $('#Row48').val(),
                        Row49: $('#Row49').val(),
                        Row50: $('#Row50').val(),
                        Row51: $('#Row51').val(),
                        Row52: $('#Row52').val(),
                        Row53: $('#Row53').val(),
                        Row54: $('#Row54').val(),
                        Row55: $('#Row55').val(),
                        Row56: $('#Row56').val(),
                        Row57: $('#Row57').val(),
                        Row58: $('#Row58').val(),
                        Row59: $('#Row59').val(),
                        Row60: $('#Row60').val(),
                        Row61: $('#Row61').val(),
                        Row62: $('#Row62').val(),
                        Row63: $('#Row63').val(),
                        Row64: $('#Row64').val(),
                        Row65: $('#Row65').val(),
                        Row66: $('#Row66').val(),
                        Row67: $('#Row67').val(),
                        Row68: $('#Row68').val(),
                        Row69: $('#Row69').val(),
                        Row70: $('#Row70').val(),
                        Row71: $('#Row71').val(),
                        Row72: $('#Row72').val(),
                        Row73: $('#Row73').val(),
                        Row74: $('#Row74').val(),
                        Row75: $('#Row75').val(),
                        Row76: $('#Row76').val(),
                        Row77: $('#Row77').val(),
                        Row78: $('#Row78').val(),
                        Row79: $('#Row79').val(),
                        Row80: $('#Row80').val(),
                        Row81: $('#Row81').val(),
                        Row82: $('#Row82').val(),
                        Row83: $('#Row83').val(),
                        Row84: $('#Row84').val(),
                        Row85: $('#Row85').val(),
                        Row86: $('#Row86').val(),
                        Row87: $('#Row87').val(),
                        Row88: $('#Row88').val(),
                        Row89: $('#Row89').val(),
                        Row90: $('#Row90').val(),
                        Row91: $('#Row91').val(),
                        Row92: $('#Row92').val(),
                        Row93: $('#Row93').val(),
                        Row94: $('#Row94').val(),
                        Row95: $('#Row95').val(),
                        Row96: $('#Row96').val(),
                        Row97: $('#Row97').val(),
                        Row98: $('#Row98').val(),
                        Row99: $('#Row99').val(),
                        Row100: $('#Row100').val(),

                        Row101: $('#Row101').val(),
                        Row102: $('#Row102').val(),
                        Row103: $('#Row103').val(),
                        Row104: $('#Row104').val(),
                        Row105: $('#Row105').val(),
                        Row106: $('#Row106').val(),
                        Row107: $('#Row107').val(),
                        Row108: $('#Row108').val(),
                        Row109: $('#Row109').val(),
                        Row110: $('#Row110').val(),
                        Row111: $('#Row111').val(),
                        Row112: $('#Row112').val(),
                        Row113: $('#Row113').val(),
                        Row114: $('#Row114').val(),
                        Row115: $('#Row115').val(),
                        Row116: $('#Row116').val(),
                        Row117: $('#Row117').val(),
                        Row118: $('#Row118').val(),
                        Row119: $('#Row119').val(),
                        Row120: $('#Row120').val(),
                        Row121: $('#Row121').val(),
                        Row122: $('#Row122').val(),
                        Row123: $('#Row123').val(),
                        Row124: $('#Row124').val(),
                        Row125: $('#Row125').val(),
                        Row126: $('#Row126').val(),
                        Row127: $('#Row127').val(),
                        Row128: $('#Row128').val(),
                        Row129: $('#Row129').val(),
                        Row130: $('#Row130').val(),
                        Row131: $('#Row131').val(),
                        Row132: $('#Row132').val(),
                        Row133: $('#Row133').val(),
                        Row134: $('#Row134').val(),
                        Row135: $('#Row135').val(),
                        Row136: $('#Row136').val(),
                        Row137: $('#Row137').val(),
                        Row138: $('#Row138').val(),
                        Row139: $('#Row139').val(),
                        Row140: $('#Row140').val(),
                        Row141: $('#Row141').val(),
                        Row142: $('#Row142').val(),
                        Row143: $('#Row143').val(),
                        Row144: $('#Row144').val(),
                        Row145: $('#Row145').val(),
                        Row146: $('#Row146').val(),
                        Row147: $('#Row147').val(),
                        Row148: $('#Row148').val(),
                        Row149: $('#Row149').val(),
                        Row150: $('#Row150').val(),
                        Row151: $('#Row151').val(),
                        Row152: $('#Row152').val(),
                        Row153: $('#Row153').val(),
                        Row154: $('#Row154').val(),
                        Row155: $('#Row155').val(),
                        Row156: $('#Row156').val(),
                        Row157: $('#Row157').val(),
                        Row158: $('#Row158').val(),
                        Row159: $('#Row159').val(),
                        Row160: $('#Row160').val(),
                        Row161: $('#Row161').val(),
                        Row162: $('#Row162').val(),
                        Row163: $('#Row163').val(),
                        Row164: $('#Row164').val(),
                        Row165: $('#Row165').val(),
                        Row166: $('#Row166').val(),
                        Row167: $('#Row167').val(),
                        Row168: $('#Row168').val(),
                        Row169: $('#Row169').val(),
                        Row170: $('#Row170').val(),
                        Row171: $('#Row171').val(),
                        Row172: $('#Row172').val(),
                        Row173: $('#Row173').val(),
                        Row174: $('#Row174').val(),
                        Row175: $('#Row175').val(),
                        Row176: $('#Row176').val(),
                        Row177: $('#Row177').val(),
                        Row178: $('#Row178').val(),
                        Row179: $('#Row179').val(),
                        Row180: $('#Row180').val(),
                        Row181: $('#Row181').val(),
                        Row182: $('#Row182').val(),
                        Row183: $('#Row183').val(),
                        Row184: $('#Row184').val(),
                        Row185: $('#Row185').val(),
                        Row186: $('#Row186').val(),
                        Row187: $('#Row187').val(),
                        Row188: $('#Row188').val(),
                        Row189: $('#Row189').val(),
                        Row190: $('#Row190').val(),
                        Row191: $('#Row191').val(),
                        Row192: $('#Row192').val(),
                        Row193: $('#Row193').val(),
                        Row194: $('#Row194').val(),
                        Row195: $('#Row195').val(),
                        Row196: $('#Row196').val(),
                        Row197: $('#Row197').val(),
                        Row198: $('#Row198').val(),
                        Row199: $('#Row199').val(),
                        Row200: $('#Row200').val(),

                        Row201: $('#Row201').val(),
                        Row202: $('#Row202').val(),
                        Row203: $('#Row203').val(),
                        Row204: $('#Row204').val(),
                        Row205: $('#Row205').val(),
                        Row206: $('#Row206').val(),
                        Row207: $('#Row207').val(),
                        Row208: $('#Row208').val(),
                        Row209: $('#Row209').val(),
                        Row210: $('#Row210').val(),
                        Row211: $('#Row211').val(),
                        Row212: $('#Row212').val(),
                        Row213: $('#Row213').val(),
                        Row214: $('#Row214').val(),
                        Row215: $('#Row215').val(),
                        Row216: $('#Row216').val(),
                        Row217: $('#Row217').val(),
                        Row218: $('#Row218').val(),
                        Row219: $('#Row219').val(),
                        Row220: $('#Row220').val(),
                        Row221: $('#Row221').val(),
                        Row222: $('#Row222').val(),
                        Row223: $('#Row223').val(),
                        Row224: $('#Row224').val(),
                        Row225: $('#Row225').val(),
                        Row226: $('#Row226').val(),
                        Row227: $('#Row227').val(),
                        Row228: $('#Row228').val(),
                        Row229: $('#Row229').val(),
                        Row230: $('#Row230').val(),
                        Row231: $('#Row231').val(),
                        Row232: $('#Row232').val(),
                        Row233: $('#Row233').val(),
                        Row234: $('#Row234').val(),
                        Row235: $('#Row235').val(),
                        Row236: $('#Row236').val(),
                        Row237: $('#Row237').val(),
                        Row238: $('#Row238').val(),
                        Row239: $('#Row239').val(),
                        Row240: $('#Row240').val(),
                        Row241: $('#Row241').val(),
                        Row242: $('#Row242').val(),
                        Row243: $('#Row243').val(),
                        Row244: $('#Row244').val(),
                        Row245: $('#Row245').val(),
                        Row246: $('#Row246').val(),
                        Row247: $('#Row247').val(),
                        Row248: $('#Row248').val(),
                        Row249: $('#Row249').val(),
                        Row250: $('#Row250').val(),
                        Row251: $('#Row251').val(),

                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CorporateGovAM/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateGovAM_I",
                        type: 'POST',
                        data: JSON.stringify(CorporateGovAM),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateGovAMPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateGovAM",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var CorporateGovAM = {
                                    CorporateGovAMPK: $('#CorporateGovAMPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Date: $('#Date').val(),
                                    Row1: $('#Row1').val(),
                                    Row2: $('#Row2').val(),
                                    Row3: $('#Row3').val(),
                                    Row4: $('#Row4').val(),
                                    Row5: $('#Row5').val(),
                                    Row6: $('#Row6').val(),
                                    Row7: $('#Row7').val(),
                                    Row8: $('#Row8').val(),
                                    Row9: $('#Row9').val(),
                                    Row10: $('#Row10').val(),
                                    Row11: $('#Row11').val(),
                                    Row12: $('#Row12').val(),
                                    Row13: $('#Row13').val(),
                                    Row14: $('#Row14').val(),
                                    Row15: $('#Row15').val(),
                                    Row16: $('#Row16').val(),
                                    Row17: $('#Row17').val(),
                                    Row18: $('#Row18').val(),
                                    Row19: $('#Row19').val(),
                                    Row20: $('#Row20').val(),
                                    Row21: $('#Row21').val(),
                                    Row22: $('#Row22').val(),
                                    Row23: $('#Row23').val(),
                                    Row24: $('#Row24').val(),
                                    Row25: $('#Row25').val(),
                                    Row26: $('#Row26').val(),
                                    Row27: $('#Row27').val(),
                                    Row28: $('#Row28').val(),
                                    Row29: $('#Row29').val(),
                                    Row30: $('#Row30').val(),
                                    Row31: $('#Row31').val(),
                                    Row32: $('#Row32').val(),
                                    Row33: $('#Row33').val(),
                                    Row34: $('#Row34').val(),
                                    Row35: $('#Row35').val(),
                                    Row36: $('#Row36').val(),
                                    Row37: $('#Row37').val(),
                                    Row38: $('#Row38').val(),
                                    Row39: $('#Row39').val(),
                                    Row40: $('#Row40').val(),
                                    Row41: $('#Row41').val(),
                                    Row42: $('#Row42').val(),
                                    Row43: $('#Row43').val(),
                                    Row44: $('#Row44').val(),
                                    Row45: $('#Row45').val(),
                                    Row46: $('#Row46').val(),
                                    Row47: $('#Row47').val(),
                                    Row48: $('#Row48').val(),
                                    Row49: $('#Row49').val(),
                                    Row50: $('#Row50').val(),
                                    Row51: $('#Row51').val(),
                                    Row52: $('#Row52').val(),
                                    Row53: $('#Row53').val(),
                                    Row54: $('#Row54').val(),
                                    Row55: $('#Row55').val(),
                                    Row56: $('#Row56').val(),
                                    Row57: $('#Row57').val(),
                                    Row58: $('#Row58').val(),
                                    Row59: $('#Row59').val(),
                                    Row60: $('#Row60').val(),
                                    Row61: $('#Row61').val(),
                                    Row62: $('#Row62').val(),
                                    Row63: $('#Row63').val(),
                                    Row64: $('#Row64').val(),
                                    Row65: $('#Row65').val(),
                                    Row66: $('#Row66').val(),
                                    Row67: $('#Row67').val(),
                                    Row68: $('#Row68').val(),
                                    Row69: $('#Row69').val(),
                                    Row70: $('#Row70').val(),
                                    Row71: $('#Row71').val(),
                                    Row72: $('#Row72').val(),
                                    Row73: $('#Row73').val(),
                                    Row74: $('#Row74').val(),
                                    Row75: $('#Row75').val(),
                                    Row76: $('#Row76').val(),
                                    Row77: $('#Row77').val(),
                                    Row78: $('#Row78').val(),
                                    Row79: $('#Row79').val(),
                                    Row80: $('#Row80').val(),
                                    Row81: $('#Row81').val(),
                                    Row82: $('#Row82').val(),
                                    Row83: $('#Row83').val(),
                                    Row84: $('#Row84').val(),
                                    Row85: $('#Row85').val(),
                                    Row86: $('#Row86').val(),
                                    Row87: $('#Row87').val(),
                                    Row88: $('#Row88').val(),
                                    Row89: $('#Row89').val(),
                                    Row90: $('#Row90').val(),
                                    Row91: $('#Row91').val(),
                                    Row92: $('#Row92').val(),
                                    Row93: $('#Row93').val(),
                                    Row94: $('#Row94').val(),
                                    Row95: $('#Row95').val(),
                                    Row96: $('#Row96').val(),
                                    Row97: $('#Row97').val(),
                                    Row98: $('#Row98').val(),
                                    Row99: $('#Row99').val(),
                                    Row100: $('#Row100').val(),

                                    Row101: $('#Row101').val(),
                                    Row102: $('#Row102').val(),
                                    Row103: $('#Row103').val(),
                                    Row104: $('#Row104').val(),
                                    Row105: $('#Row105').val(),
                                    Row106: $('#Row106').val(),
                                    Row107: $('#Row107').val(),
                                    Row108: $('#Row108').val(),
                                    Row109: $('#Row109').val(),
                                    Row110: $('#Row110').val(),
                                    Row111: $('#Row111').val(),
                                    Row112: $('#Row112').val(),
                                    Row113: $('#Row113').val(),
                                    Row114: $('#Row114').val(),
                                    Row115: $('#Row115').val(),
                                    Row116: $('#Row116').val(),
                                    Row117: $('#Row117').val(),
                                    Row118: $('#Row118').val(),
                                    Row119: $('#Row119').val(),
                                    Row120: $('#Row120').val(),
                                    Row121: $('#Row121').val(),
                                    Row122: $('#Row122').val(),
                                    Row123: $('#Row123').val(),
                                    Row124: $('#Row124').val(),
                                    Row125: $('#Row125').val(),
                                    Row126: $('#Row126').val(),
                                    Row127: $('#Row127').val(),
                                    Row128: $('#Row128').val(),
                                    Row129: $('#Row129').val(),
                                    Row130: $('#Row130').val(),
                                    Row131: $('#Row131').val(),
                                    Row132: $('#Row132').val(),
                                    Row133: $('#Row133').val(),
                                    Row134: $('#Row134').val(),
                                    Row135: $('#Row135').val(),
                                    Row136: $('#Row136').val(),
                                    Row137: $('#Row137').val(),
                                    Row138: $('#Row138').val(),
                                    Row139: $('#Row139').val(),
                                    Row140: $('#Row140').val(),
                                    Row141: $('#Row141').val(),
                                    Row142: $('#Row142').val(),
                                    Row143: $('#Row143').val(),
                                    Row144: $('#Row144').val(),
                                    Row145: $('#Row145').val(),
                                    Row146: $('#Row146').val(),
                                    Row147: $('#Row147').val(),
                                    Row148: $('#Row148').val(),
                                    Row149: $('#Row149').val(),
                                    Row150: $('#Row150').val(),
                                    Row151: $('#Row151').val(),
                                    Row152: $('#Row152').val(),
                                    Row153: $('#Row153').val(),
                                    Row154: $('#Row154').val(),
                                    Row155: $('#Row155').val(),
                                    Row156: $('#Row156').val(),
                                    Row157: $('#Row157').val(),
                                    Row158: $('#Row158').val(),
                                    Row159: $('#Row159').val(),
                                    Row160: $('#Row160').val(),
                                    Row161: $('#Row161').val(),
                                    Row162: $('#Row162').val(),
                                    Row163: $('#Row163').val(),
                                    Row164: $('#Row164').val(),
                                    Row165: $('#Row165').val(),
                                    Row166: $('#Row166').val(),
                                    Row167: $('#Row167').val(),
                                    Row168: $('#Row168').val(),
                                    Row169: $('#Row169').val(),
                                    Row170: $('#Row170').val(),
                                    Row171: $('#Row171').val(),
                                    Row172: $('#Row172').val(),
                                    Row173: $('#Row173').val(),
                                    Row174: $('#Row174').val(),
                                    Row175: $('#Row175').val(),
                                    Row176: $('#Row176').val(),
                                    Row177: $('#Row177').val(),
                                    Row178: $('#Row178').val(),
                                    Row179: $('#Row179').val(),
                                    Row180: $('#Row180').val(),
                                    Row181: $('#Row181').val(),
                                    Row182: $('#Row182').val(),
                                    Row183: $('#Row183').val(),
                                    Row184: $('#Row184').val(),
                                    Row185: $('#Row185').val(),
                                    Row186: $('#Row186').val(),
                                    Row187: $('#Row187').val(),
                                    Row188: $('#Row188').val(),
                                    Row189: $('#Row189').val(),
                                    Row190: $('#Row190').val(),
                                    Row191: $('#Row191').val(),
                                    Row192: $('#Row192').val(),
                                    Row193: $('#Row193').val(),
                                    Row194: $('#Row194').val(),
                                    Row195: $('#Row195').val(),
                                    Row196: $('#Row196').val(),
                                    Row197: $('#Row197').val(),
                                    Row198: $('#Row198').val(),
                                    Row199: $('#Row199').val(),
                                    Row200: $('#Row200').val(),

                                    Row201: $('#Row201').val(),
                                    Row202: $('#Row202').val(),
                                    Row203: $('#Row203').val(),
                                    Row204: $('#Row204').val(),
                                    Row205: $('#Row205').val(),
                                    Row206: $('#Row206').val(),
                                    Row207: $('#Row207').val(),
                                    Row208: $('#Row208').val(),
                                    Row209: $('#Row209').val(),
                                    Row210: $('#Row210').val(),
                                    Row211: $('#Row211').val(),
                                    Row212: $('#Row212').val(),
                                    Row213: $('#Row213').val(),
                                    Row214: $('#Row214').val(),
                                    Row215: $('#Row215').val(),
                                    Row216: $('#Row216').val(),
                                    Row217: $('#Row217').val(),
                                    Row218: $('#Row218').val(),
                                    Row219: $('#Row219').val(),
                                    Row220: $('#Row220').val(),
                                    Row221: $('#Row221').val(),
                                    Row222: $('#Row222').val(),
                                    Row223: $('#Row223').val(),
                                    Row224: $('#Row224').val(),
                                    Row225: $('#Row225').val(),
                                    Row226: $('#Row226').val(),
                                    Row227: $('#Row227').val(),
                                    Row228: $('#Row228').val(),
                                    Row229: $('#Row229').val(),
                                    Row230: $('#Row230').val(),
                                    Row231: $('#Row231').val(),
                                    Row232: $('#Row232').val(),
                                    Row233: $('#Row233').val(),
                                    Row234: $('#Row234').val(),
                                    Row235: $('#Row235').val(),
                                    Row236: $('#Row236').val(),
                                    Row237: $('#Row237').val(),
                                    Row238: $('#Row238').val(),
                                    Row239: $('#Row239').val(),
                                    Row240: $('#Row240').val(),
                                    Row241: $('#Row241').val(),
                                    Row242: $('#Row242').val(),
                                    Row243: $('#Row243').val(),
                                    Row244: $('#Row244').val(),
                                    Row245: $('#Row245').val(),
                                    Row246: $('#Row246').val(),
                                    Row247: $('#Row247').val(),
                                    Row248: $('#Row248').val(),
                                    Row249: $('#Row249').val(),
                                    Row250: $('#Row250').val(),
                                    Row251: $('#Row251').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CorporateGovAM/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateGovAM_U",
                                    type: 'POST',
                                    data: JSON.stringify(CorporateGovAM),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateGovAMPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateGovAM",
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
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CorporateGovAM" + "/" + $("#CorporateGovAMPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateGovAMPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateGovAM",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateGovAM = {
                                CorporateGovAMPK: $('#CorporateGovAMPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateGovAM/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateGovAM_A",
                                type: 'POST',
                                data: JSON.stringify(CorporateGovAM),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateGovAMPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateGovAM",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateGovAM = {
                                CorporateGovAMPK: $('#CorporateGovAMPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateGovAM/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateGovAM_V",
                                type: 'POST',
                                data: JSON.stringify(CorporateGovAM),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateGovAMPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateGovAM",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateGovAM = {
                                CorporateGovAMPK: $('#CorporateGovAMPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateGovAM/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateGovAM_R",
                                type: 'POST',
                                data: JSON.stringify(CorporateGovAM),
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