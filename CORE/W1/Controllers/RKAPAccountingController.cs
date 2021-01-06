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
using RFSRepositoryThree;

namespace W1.Controllers
{
    public class RKAPAccountingController : ApiController
    {
        static readonly string _Obj = "RKAP Accounting Controller";
        static readonly RKAPAccountingReps _RKAPAccountingReps = new RKAPAccountingReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _RKAPAccountingReps.RKAPAccounting_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody] RKAPAccounting _RKAPAccounting)
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
                            if (PermissionID == "RKAPAccounting_U")
                            {
                                int _newHisPK = _RKAPAccountingReps.RKAPAccounting_Update(_RKAPAccounting, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update RKAP Accounting Success", _Obj, "", param1, _RKAPAccounting.RKAP_AccountingPK, _RKAPAccounting.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update RKAP Accounting Success");
                            }
                            if (PermissionID == "RKAPAccounting_A")
                            {
                                _RKAPAccountingReps.RKAPAccounting_Approved(_RKAPAccounting);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved RKAP Accounting Success", _Obj, "", param1, _RKAPAccounting.RKAP_AccountingPK, _RKAPAccounting.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved RKAP Accounting Success");
                            }
                            if (PermissionID == "RKAPAccounting_V")
                            {
                                _RKAPAccountingReps.RKAPAccounting_Void(_RKAPAccounting);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void RKAP Accounting Success", _Obj, "", param1, _RKAPAccounting.RKAP_AccountingPK, _RKAPAccounting.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void RKAP Accounting Success");
                            }
                            if (PermissionID == "RKAPAccounting_R")
                            {
                                _RKAPAccountingReps.RKAPAccounting_Reject(_RKAPAccounting);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject RKAP Accounting Success", _Obj, "", param1, _RKAPAccounting.RKAP_AccountingPK, _RKAPAccounting.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject RKAP Accounting Success");
                            }
                            if (PermissionID == "RKAPAccounting_I")
                            {
                                int _lastPKByLastUpdate = _RKAPAccountingReps.RKAPAccounting_Add(_RKAPAccounting, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add RKAP Accounting Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert RKAP Accounting Success");
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