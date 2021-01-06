
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;

namespace W1.Controllers
{
    public class BankCustodianController : ApiController
    {
        static readonly string _Obj = "Bank Custodian Controller";
        static readonly BankCustodianReps _bankCustodianReps = new BankCustodianReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
* param1 = userID
* param2 = sessionID
*/
        [HttpGet]
        public HttpResponseMessage GetBranchNameByBankCustodianPK(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _bankCustodianReps.Get_BranchNameByBankCustodianPK(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get BranchName By BankCustodian PK", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = Type 
        */
        [HttpGet]
        public HttpResponseMessage GetBankCustodianCombo(string param1, string param2,string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _bankCustodianReps.BankCustodian_Combo(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Bank Custodian Combo", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

         /*
         * param1 = userID
         * param2 = sessionID
         * param3 = Type 
         */
        [HttpGet]
        public HttpResponseMessage GetBankCustodianComboIDOnly(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _bankCustodianReps.BankCustodian_ComboIDOnly(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Bank Custodian Combo ID Only", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3)
         */
        [HttpGet]
        public HttpResponseMessage GetData(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _bankCustodianReps.BankCustodian_Select(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BankCustodian _bankCustodian)
        {
            string PermissionID;
            PermissionID = param3;
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            bool havePrivillege = _host.Get_Privillege(param1, PermissionID);
                            if (PermissionID == "BankCustodian_U")
                            {
                                int _newHisPK = _bankCustodianReps.BankCustodian_Update(_bankCustodian, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Bank Custodian Success", _Obj, "", param1, _bankCustodian.BankCustodianPK, _bankCustodian.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Bank Custodian Success");
                            }
                            if (PermissionID == "BankCustodian_A")
                            {
                                _bankCustodianReps.BankCustodian_Approved(_bankCustodian);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Bank Custodian Success", _Obj, "", param1, _bankCustodian.BankCustodianPK, _bankCustodian.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Bank Custodian Success");
                            }
                            if (PermissionID == "BankCustodian_V")
                            {
                                _bankCustodianReps.BankCustodian_Void(_bankCustodian);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Bank Custodian Success", _Obj, "", param1, _bankCustodian.BankCustodianPK, _bankCustodian.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Bank Custodian Success");
                            }
                            if (PermissionID == "BankCustodian_R")
                            {
                                _bankCustodianReps.BankCustodian_Reject(_bankCustodian);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Bank Custodian Success", _Obj, "", param1, _bankCustodian.BankCustodianPK, _bankCustodian.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Bank Custodian Success");
                            }
                            if (PermissionID == "BankCustodian_I")
                            {
                                int _lastPKByLastUpdate = _bankCustodianReps.BankCustodian_Add(_bankCustodian, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Bank Custodian Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Bank Custodian Success");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoActionMessage);
                            }

                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

    }
}
