$(document).ready(function () {


    var x = document.getElementById("myAudio");

    function playAudio() {
        x.play();
    }
    //Global Variabel
    var tabindex;
    var _parameterText = "";
    document.title = 'FORM NOTIFICATION';
    $("#BtnOpenNewTab").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnAdd.png"
    });
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {
        alert('Your Got New Message. ');
        // Html encode display name and message.
        FindQuotesRoom();
    };

    $.connection.hub.start().done(function () {
    });

    //1
    FindQuotesRoom();

    

    $(document).bind('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            FindQuotesRoom();
        }
    });

    $("#BtnFind").click(function () {
        FindQuotesRoom();
    });

    function FindQuotesRoom() {
        
        playAudio();
        _parameterText = $("#ParameterText").val();
        if (_parameterText == undefined || _parameterText == "") {
            _parameterText = "ALL"
        }

        $("#gridQuotesRoom").empty();
        $("#gridQuotesRoom").kendoGrid({
            dataSource: {
                transport:
                        {
                            read:
                                {
                                    url: window.location.origin + "/Radsoft/Host/GetNotification/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + sessionStorage.getItem("user"),
                                    dataType: "json"
                                }
                        },
                batch: true,
                cache: false,
                error: function (e) {
                    $("#ParameterText").val("");
                    alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();                    
                    return;
                },
                pageSize: 1000000000,
                schema: {
                    model: {
                        fields: {
                            
                        }
                    }
                }
            },
            //filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            height: screen.height + 1100,
            reorderable: true,
            sortable: true,
            resizable: true,
            dataBound: gridDataBound,
            columns: [
                { field: "Name", title: "Name", width: 150 },
                { field: "Message", title: "Message", width: 300 },
                 { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });
    }

    function gridDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"></td></tr>');
        }
    };

});