
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
    public class CustodianJournalAccountNameSetupController : ApiController
    {
        static readonly string _Obj = "CustodianJournalAccountNameSetup Controller";
        static readonly CustodianJournalAccountNameSetupReps _custodianJournalAccountNameSetupReps = new CustodianJournalAccountNameSetupReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

   
        /*
       * param1 = userID
       * param2 = sessionID
       */
        [HttpGet]
        public HttpResponseMessage GetCustodianJournalAccountNameSetupCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Fund Cash Ref Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]CustodianJournalAccountNameSetup _custodianJournalAccountNameSetup)
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
                            if (PermissionID == "CustodianJournalAccountNameSetup_U")
                            {
                                int _newHisPK = _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Update(_custodianJournalAccountNameSetup, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Fund Cash Ref Success", _Obj, "", param1, _custodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, _custodianJournalAccountNameSetup.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fund Cash Ref Success");
                            }
                            if (PermissionID == "CustodianJournalAccountNameSetup_A")
                            {
                                _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Approved(_custodianJournalAccountNameSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Fund Cash Ref Success", _Obj, "", param1, _custodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, _custodianJournalAccountNameSetup.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fund Cash Ref Success");
                            }
                            if (PermissionID == "CustodianJournalAccountNameSetup_V")
                            {
                                _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Void(_custodianJournalAccountNameSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Fund Cash Ref Success", _Obj, "", param1, _custodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, _custodianJournalAccountNameSetup.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fund Cash Ref Success");
                            }
                            if (PermissionID == "CustodianJournalAccountNameSetup_R")
                            {
                                _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Reject(_custodianJournalAccountNameSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Fund Cash Ref Success", _Obj, "", param1, _custodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, _custodianJournalAccountNameSetup.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fund Cash Ref Success");
                            }
                            if (PermissionID == "CustodianJournalAccountNameSetup_I")
                            {
                                int _lastPKByLastUpdate = _custodianJournalAccountNameSetupReps.CustodianJournalAccountNameSetup_Add(_custodianJournalAccountNameSetup, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Cash Ref Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Fund Cash Ref Success");
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
