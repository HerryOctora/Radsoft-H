$(document).ready(function () {
    document.title = 'FORM DOWNLOAD FILE FROM IDX';
    
    //Global Variabel
    var win;
    var GlobValidator = $("#WinDownloadFileFromIDX").kendoValidator().data("kendoValidator");
    var staticURL = "http://idxdata.co.id/Download_Data/Daily/";

    initWindow();

    function initWindow() {
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                return;
            } else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            alertify.alert("Date is Holiday, Please Insert Date Correctly!");
                            return;
                        } else {
                            var _valueDate = $("#ValueDate").data("kendoDatePicker").value();
                            if (_valueDate == undefined || _valueDate == "" || _valueDate == null) {
                                $("#ValueDate").data("kendoDatePicker").value(new Date());
                                _valueDate = $("#ValueDate").data("kendoDatePicker").value();
                            }

                            var _fileType = $("#FileType").data("kendoComboBox").value();
                            if (_fileType == undefined || _fileType == "" || _fileType == null) {
                                $("#FileType").data("kendoComboBox").value("CP");
                                _fileType = $("#FileType").data("kendoComboBox").value();
                            }

                            var _fileLocation = staticURL + $("#FileType").data("kendoComboBox").text().replace(" ", "_") + "/";
                            var _fileName = _fileType + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "yyMMdd") + ".zip";

                            $("#FileLocation").val(_fileLocation);
                            $("#FileName").val(_fileName);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        return;
                    }
                });
            }
        }

        $("#FileType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "Closing Price", value: "CP" },
               { text: "Stock Summary", value: "SS" }
               
            ],
            filter: "contains",
            suggest: true,
            value: OnSetValue(),
            change: OnChangeFileType
        });
        function OnChangeFileType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            var _valueDate = $("#ValueDate").data("kendoDatePicker").value();
            if (_valueDate == undefined || _valueDate == "" || _valueDate == null) {
                alertify.alert("Date must be fill!");
                $("#FileName").val("");
                return;
            } else {
                var _fileLocation = staticURL + this.text().replace(" ", "_") + "/";
                var _fileName = this.value() + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "yyMMdd") + ".zip";

                $("#FileLocation").val(_fileLocation);
                $("#FileName").val(_fileName);
            }            
        }
        function OnSetValue() {
            return "CP";
        }

        win = $("#WinDownloadFileFromIDX").kendoWindow({
            title: "Download File From IDX",
            height: 350,
            width: 700,
            visible: false,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        clearData();
        setData();
        win.center();
        win.open();
    }

    function clearData() {
        $("#FileLocation").val("");
        $("#FileName").val("");
    }

    function setData() {
        var _valueDate = $("#ValueDate").data("kendoDatePicker").value();
        if (_valueDate == undefined || _valueDate == "" || _valueDate == null) {
            $("#ValueDate").data("kendoDatePicker").value(new Date());
            _valueDate = $("#ValueDate").data("kendoDatePicker").value();
        }

        var _fileType = $("#FileType").data("kendoComboBox").value();
        if (_fileType == undefined || _fileType == "" || _fileType == null) {
            $("#FileType").data("kendoComboBox").value("SS");
            _fileType = $("#FileType").data("kendoComboBox").value();
        }

        var _fileLocation = staticURL + $("#FileType").data("kendoComboBox").text().replace(" ", "_") + "/";
        var _fileName = _fileType + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "yyMMdd") + ".zip";

        $("#FileLocation").val(_fileLocation);
        $("#FileName").val(_fileName);
    }

    function validateData() {
        if (GlobValidator.validate()) {
            return 1;
        } else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    $("#BtnDownload").click(function () {        
        alertify.confirm("Are you sure want to download data?", function (e) {
            if (e) {
                console.log($('#FileLocation').val());
                console.log($('#FileName').val());
                $.blockUI({});
                var DownloadFileFromIDX = {
                    ValueDate: $('#ValueDate').val(),
                    FileType: $("#FileType").data("kendoComboBox").value(),
                    FileLocation: $('#FileLocation').val(),
                    FileName: $('#FileName').val()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/DownloadFileFromIDX/DownloadFileFromIDX/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DownloadFileFromIDX_Download/",
                    type: 'POST',
                    data: JSON.stringify(DownloadFileFromIDX),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        $.unblockUI();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        return;
                    }
                });
            }
        });
    });

    $("#BtnCancel").click(function () {        
        alertify.confirm("Are you sure want to close?", function (e) {
            if (e) {
                win.close();
            }
        });
    });

});