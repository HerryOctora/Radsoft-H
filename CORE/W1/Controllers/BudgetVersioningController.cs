
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

namespace W1.Controllers
{
    public class BudgetVersioningController : ApiController
    {
        static readonly string _Obj = "BudgetVersioning Controller";
        static readonly BudgetVersioningReps _BudgetVersioningReps = new BudgetVersioningReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _BudgetVersioningReps.BudgetVersioning_Select(param3));
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BudgetVersioning _BudgetVersioning)
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
                            if (PermissionID == "BudgetVersioning_U")
                            {
                                int _newHisPK = _BudgetVersioningReps.BudgetVersioning_Update(_BudgetVersioning, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update BudgetVersioning Success", _Obj, "", param1, _BudgetVersioning.BudgetVersioningPK, _BudgetVersioning.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update BudgetVersioning Success");
                            }
                            if (PermissionID == "BudgetVersioning_A")
                            {
                                _BudgetVersioningReps.BudgetVersioning_Approved(_BudgetVersioning);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved BudgetVersioning Success", _Obj, "", param1, _BudgetVersioning.BudgetVersioningPK, _BudgetVersioning.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved BudgetVersioning Success");
                            }
                            if (PermissionID == "BudgetVersioning_V")
                            {
                                _BudgetVersioningReps.BudgetVersioning_Void(_BudgetVersioning);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void BudgetVersioning Success", _Obj, "", param1, _BudgetVersioning.BudgetVersioningPK, _BudgetVersioning.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void BudgetVersioning Success");
                            }
                            if (PermissionID == "BudgetVersioning_R")
                            {
                                _BudgetVersioningReps.BudgetVersioning_Reject(_BudgetVersioning);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject BudgetVersioning Success", _Obj, "", param1, _BudgetVersioning.BudgetVersioningPK, _BudgetVersioning.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject BudgetVersioning Success");
                            }
                            if (PermissionID == "BudgetVersioning_I")
                            {
                                int _lastPKByLastUpdate = _BudgetVersioningReps.BudgetVersioning_Add(_BudgetVersioning, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add BudgetVersioning Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert BudgetVersioning Success");
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

    }
}
