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
using RFSRepositoryTwo;

namespace W1.Controllers
{
    public class BloombergEquityController : ApiController
    {

        static readonly string _Obj = "Bloomberg Equity Controller";
        static readonly BloombergEquityReps _BloombergEquityReps = new BloombergEquityReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3)
         */
        [HttpGet]
        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _BloombergEquityReps.BloombergEquity_SelectByDateFromTo(param3, param4, param5));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BloombergEquity _BloombergEquity)
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
                            if (PermissionID == "BloombergEquity_U")
                            {
                                int _newHisPK = _BloombergEquityReps.BloombergEquity_Update(_BloombergEquity, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Close Price Success", _Obj, "", param1, _BloombergEquity.BloombergEquityPK, _BloombergEquity.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Close Price Success");
                            }
                            if (PermissionID == "BloombergEquity_A")
                            {
                                _BloombergEquityReps.BloombergEquity_Approved(_BloombergEquity);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Close Price Success", _Obj, "", param1, _BloombergEquity.BloombergEquityPK, _BloombergEquity.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Close Price Success");
                            }
                            if (PermissionID == "BloombergEquity_V")
                            {
                                _BloombergEquityReps.BloombergEquity_Void(_BloombergEquity);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Close Price Success", _Obj, "", param1, _BloombergEquity.BloombergEquityPK, _BloombergEquity.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Close Price Success");
                            }
                            if (PermissionID == "BloombergEquity_R")
                            {
                                _BloombergEquityReps.BloombergEquity_Reject(_BloombergEquity);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Close Price Success", _Obj, "", param1, _BloombergEquity.BloombergEquityPK, _BloombergEquity.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Close Price Success");
                            }
                            if (PermissionID == "BloombergEquity_I")
                            {
                                int _lastPKByLastUpdate = _BloombergEquityReps.BloombergEquity_Add(_BloombergEquity, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Close Price Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Close Price Success");
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