using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;
using RFSRepositoryOne;
using System.IO;
using System.Net.Http.Headers;

namespace W1.Controllers
{
    public class AgentCSRFundController : ApiController
    {
        static readonly string _Obj = "AgentCSRFund Controller";
        static readonly AgentCSRFundReps _AgentCSRFundReps = new AgentCSRFundReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3, all = 9)
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
                        return Request.CreateResponse(HttpStatusCode.OK, _AgentCSRFundReps.AgentCSRFund_Select(param3));
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

        [HttpPost]
        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]AgentCSRFund _AgentCSRFund)
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
                            if (PermissionID == "AgentCSRFund_U")
                            {
                                int _newHisPK = _AgentCSRFundReps.AgentCSRFund_Update(_AgentCSRFund, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update AgentCSRFund Success", _Obj, "", param1, _AgentCSRFund.AgentCSRFundPK, _AgentCSRFund.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update AgentCSRFund Success");
                            }
                            if (PermissionID == "AgentCSRFund_A")
                            {
                                _AgentCSRFundReps.AgentCSRFund_Approved(_AgentCSRFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved AgentCSRFund Success", _Obj, "", param1, _AgentCSRFund.AgentCSRFundPK, _AgentCSRFund.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved AgentCSRFund Success");
                            }
                            if (PermissionID == "AgentCSRFund_V")
                            {
                                _AgentCSRFundReps.AgentCSRFund_Void(_AgentCSRFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void AgentCSRFund Success", _Obj, "", param1, _AgentCSRFund.AgentCSRFundPK, _AgentCSRFund.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void AgentCSRFund Success");
                            }
                            if (PermissionID == "AgentCSRFund_R")
                            {
                                _AgentCSRFundReps.AgentCSRFund_Reject(_AgentCSRFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject AgentCSRFund Success", _Obj, "", param1, _AgentCSRFund.AgentCSRFundPK, _AgentCSRFund.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject AgentCSRFund Success");
                            }
                            if (PermissionID == "AgentCSRFund_I")
                            {
                                int _lastPK = _AgentCSRFundReps.AgentCSRFund_Add(_AgentCSRFund, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add AgentCSRFund Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert AgentCSRFund Success");
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

        [HttpGet]
        public HttpResponseMessage ValidateAgentAndFund(string param1, string param2, int param3, int param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _AgentCSRFundReps.AgentCSRFund_ValidateAgentAndFund(param3,param4,param5));
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
    }
}