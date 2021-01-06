$(document).ready(function () {
    //DEV
    window._RDOEnable = false;
   // window._GlobUrlServerBO = window.location.hostname + ':17050';
    window._GlobUrlServerRDOApi = '192.168.20.102:9092/api/';
    window._GlobUrlServerRDOSched = '192.168.20.102:9095/';
    window._GlobUrlServerVerifyBulkToFO = 'http://192.168.20.102:9092/api/userprofile/verify';
    window._GlobMMIFrontUrl = 'http://momid-rd.activyco.id/notify/';
    window._GlobXApiKeyMMI = 'eCQuwktfxJ6zLv4w1AN9b4nsQ2o0o83mEBPhSSvDCv8';
  
    //UPLOAD SETUP
    window._GlobMaxFileSizeInMB = 50;

    //window._DckID = '317404novr';
    //window._DckPassword = '5hgcvSWf';

    window._DckID = 'PayTren';
    window._DckPassword = 'q9bV2FzM';
    window._DckIP = '10.162.73.2';
    window._DckURL = 'http://202.57.21.10:8000/dukcapil/get_json/paytren/CALL_NIK';

    // 01 = ASCEND
    // 02 = AURORA
    // 03 = INSIGHT
    // 04 = INDO ARTHA BUANA
    // 05 = MNC
    // 06 = INDOASIA
    // 07 = PAYTREN
    // 08 = RHB
    // 09 = EMCO
    // 10 = MANDIRI
    // 11 = TASPEN
    // 12 = KOSPIN
    // 13 = RAHA
    // 14 = AYERS
    // 15 = RHB SECURITIES
    // 16 = SCG
    // 17 = JARVIS
    // 18 = NUSANTARA
    // 19 = INDOSTERLING
    // 20 = NIKKO
    // 21 = CIPTADANA
    // 22 = STAR
    // 23 = DANA HAJI
    // 24 = BNI
    // 25 = VALBURY
    // 26 = PII
    // 27 = CHUBB
    // 28 = PURWANTO
    // 29 = PRINCIPAL
    // 30 = ANARGYA
    // 31 = SEQUISE
    // 32 = NOBU
    // 33 = BERDIKARI
    // 99 = DEVELOPMENT
    // PASANG SESUAI NO CLIENT

    window._GlobClientCode = '03';
    window._ParamFundScheme = 'TRUE';
    window._ComplianceEmail = 'TRUE'; //setting email kalau breach dari oms equity

    window._RDO_AddProfile = "RDO/AddProfile_" + _GlobClientCode;
    window._RDO_AddTransaction = "RDO/AddTransaction_" + _GlobClientCode;
    window._RDO_RegistrationStatus = "RDO/RegistrationStatus_" + _GlobClientCode;
    window._RDO_TransactionStatus = "RDO/TransactionStatus_" + _GlobClientCode;
    window._RDO_UpdateNav = "RDO/UpdateNAV_" + _GlobClientCode;

    //PROD
    //window._GlobUrlServerBO = 'BO';
    //window._GlobUrlServerRDOApi = '192.172.25.7:8082/api/';
    //window._GlobUrlServerRDOSched = '192.172.25.7:8081/admin/';
    if(location.pathname.substring(location.pathname.lastIndexOf("/") + 1) == "Login.html")
    {
        return;
    };
    $.blockUI({});

    if (localStorage.getItem("expireMonitor") != null || localStorage.getItem("expireMonitor") != undefined)
    {
        if (sessionStorage.getItem("id") == null || sessionStorage.getItem("id") == 'undefined' && new Date().getTime() < localStorage.getItem("expireMonitor")) {
            sessionStorage.id = localStorage.getItem("SessionID");
            sessionStorage.user = localStorage.getItem("User");
        }
    }
    if (sessionStorage.getItem("id") == "") {
        alert("You Have No Session, Page will back to Login Page - Please Relogin")
        window.parent.location.href = window.location.origin + "/WEB/Login.html";
    }

    $.ajax({
        url: window.location.origin + '/Radsoft/quest/SessionCheck/' + sessionStorage.getItem("user") + '/' + sessionStorage.getItem("id"),
        type: 'GET',
        cache: 'false',
        contenType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == true) {
                $.unblockUI();
            }
        },
        error: function (data) {
            $.unblockUI();
            alert("Double Login Or No Session, Page will back to Login Page - Please Relog")
            window.parent.location.href = window.location.origin + "/WEB/Login.html";
        }
    });

    
});
