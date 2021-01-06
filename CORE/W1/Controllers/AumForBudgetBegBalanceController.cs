
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
    public class AumForBudgetBegBalanceController : ApiController
    {
        static readonly string _Obj = "AumForBudgetBegBalance Controller";
        static readonly AumForBudgetBegBalanceReps _AumForBudgetBegBalanceReps = new AumForBudgetBegBalanceReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]AumForBudgetBegBalance _AumForBudgetBegBalance)
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
                            if (PermissionID == "AumForBudgetBegBalance_U")
                            {
                                int _newHisPK = _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Update(_AumForBudgetBegBalance, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update AumForBudgetBegBalance Success", _Obj, "", param1, _AumForBudgetBegBalance.AumForBudgetBegBalancePK, _AumForBudgetBegBalance.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update AumForBudgetBegBalance Success");
                            }
                            if (PermissionID == "AumForBudgetBegBalance_A")
                            {
                                _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Approved(_AumForBudgetBegBalance);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved AumForBudgetBegBalance Success", _Obj, "", param1, _AumForBudgetBegBalance.AumForBudgetBegBalancePK, _AumForBudgetBegBalance.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved AumForBudgetBegBalance Success");
                            }
                            if (PermissionID == "AumForBudgetBegBalance_V")
                            {
                                _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Void(_AumForBudgetBegBalance);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void AumForBudgetBegBalance Success", _Obj, "", param1, _AumForBudgetBegBalance.AumForBudgetBegBalancePK, _AumForBudgetBegBalance.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void AumForBudgetBegBalance Success");
                            }
                            if (PermissionID == "AumForBudgetBegBalance_R")
                            {
                                _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Reject(_AumForBudgetBegBalance);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject AumForBudgetBegBalance Success", _Obj, "", param1, _AumForBudgetBegBalance.AumForBudgetBegBalancePK, _AumForBudgetBegBalance.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject AumForBudgetBegBalance Success");
                            }
                            if (PermissionID == "AumForBudgetBegBalance_I")
                            {
                                int _lastPKByLastUpdate = _AumForBudgetBegBalanceReps.AumForBudgetBegBalance_Add(_AumForBudgetBegBalance, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add AumForBudgetBegBalance Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert AumForBudgetBegBalance Success");
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
