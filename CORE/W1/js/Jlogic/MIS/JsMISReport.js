$(document).ready(function () {
    document.title = 'FORM MIS REPORT';

    
       

    $("#BtnDownload").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnDownload.png"
    });
    $("#BtnCancel").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnClose.png"
    });

    $("#ValueDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        value: new Date(),
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
    });

    $.ajax({
        url: window.location.origin + "/Radsoft/MISCostCenter/GetMISCostCenterCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#ParamMISCostCenter").kendoComboBox({
                dataValueField: "MISCostCenterPK",
                dataTextField: "ID",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: OnChangeMISCostCenterPK
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function OnChangeMISCostCenterPK() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    

    $("#Name").kendoComboBox({
        dataValueField: "text",
        dataTextField: "text",
        dataSource: [
           { text: "Peach Tree By Cost Center Without Adjustment" },
           { text: "Peach Tree By Cost Center With Adjustment" },
        ],
        filter: "contains",
        suggest: true,
        change: OnChangeName,
        value: setCmbName()
    });
    function setCmbName() {
        return "";
    }
    function OnChangeName() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    $("#BtnDownload").click(function () {
        
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var MISRpt = {
                    ReportName: $('#Name').val(),
                    ValueDate: $('#ValueDate').val(),
                    MISCostCenterFrom: $("#ParamMISCostCenter").data("kendoComboBox").value()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/MISReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MISReport_O",
                    type: 'POST',
                    data: JSON.stringify(MISRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        window.location = data
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

});