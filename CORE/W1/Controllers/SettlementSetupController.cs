
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
    public class SettlementSetupController : ApiController
    {
        static readonly string _Obj = "SettlementSetup Controller";
        static readonly SettlementSetupReps _SettlementSetupReps = new SettlementSetupReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


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
                        return Request.CreateResponse(HttpStatusCode.OK, _SettlementSetupReps.SettlementSetup_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]SettlementSetup _SettlementSetup)
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
                            if (PermissionID == "SettlementSetup_U")
                            {
                                int _newHisPK = _SettlementSetupReps.SettlementSetup_Update(_SettlementSetup, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update SettlementSetup Success", _Obj, "", param1, _SettlementSetup.SettlementSetupPK, _SettlementSetup.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Settlement Setup Success");
                            }
                            if (PermissionID == "SettlementSetup_A")
                            {
                                _SettlementSetupReps.SettlementSetup_Approved(_SettlementSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved SettlementSetup Success", _Obj, "", param1, _SettlementSetup.SettlementSetupPK, _SettlementSetup.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved SettlementSetup Success");
                            }
                            if (PermissionID == "SettlementSetup_V")
                            {
                                _SettlementSetupReps.SettlementSetup_Void(_SettlementSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void SettlementSetup Success", _Obj, "", param1, _SettlementSetup.SettlementSetupPK, _SettlementSetup.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void SettlementSetup Success");
                            }
                            if (PermissionID == "SettlementSetup_R")
                            {
                                _SettlementSetupReps.SettlementSetup_Reject(_SettlementSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject SettlementSetup Success", _Obj, "", param1, _SettlementSetup.SettlementSetupPK, _SettlementSetup.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject SettlementSetup Success");
                            }
                            if (PermissionID == "SettlementSetup_I")
                            {
                                int _lastPKByLastUpdate = _SettlementSetupReps.SettlementSetup_Add(_SettlementSetup, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add SettlementSetup Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert SettlementSetup Success");
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
        /*
 * param1 = userID
 * param2 = sessionID
 * param3 = FundPK
 */
        [HttpGet]
        public HttpResponseMessage GetRegDays(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _SettlementSetupReps.Get_RegDays(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, param1, param2, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }
    }
}
