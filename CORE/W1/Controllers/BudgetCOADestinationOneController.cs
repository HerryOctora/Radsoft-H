
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
    public class BudgetCOADestinationOneController : ApiController
    {
        static readonly string _Obj = "Budget COA Destination One Controller";
        static readonly BudgetCOADestinationOneReps _BudgetCOADestinationOneReps = new BudgetCOADestinationOneReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BudgetCOADestinationOne _BudgetCOADestinationOne)
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
                            if (PermissionID == "BudgetCOADestinationOne_U")
                            {
                                int _newHisPK = _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Update(_BudgetCOADestinationOne, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Budget COA Destination One Success", _Obj, "", param1, _BudgetCOADestinationOne.BudgetCOADestinationOnePK, _BudgetCOADestinationOne.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Budget COA Destination One Success");
                            }
                            if (PermissionID == "BudgetCOADestinationOne_A")
                            {
                                _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Approved(_BudgetCOADestinationOne);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Budget COA Destination One Success", _Obj, "", param1, _BudgetCOADestinationOne.BudgetCOADestinationOnePK, _BudgetCOADestinationOne.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Budget COA Destination One Success");
                            }
                            if (PermissionID == "BudgetCOADestinationOne_V")
                            {
                                _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Void(_BudgetCOADestinationOne);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Budget COA Destination One Success", _Obj, "", param1, _BudgetCOADestinationOne.BudgetCOADestinationOnePK, _BudgetCOADestinationOne.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Budget COA Destination One Success");
                            }
                            if (PermissionID == "BudgetCOADestinationOne_R")
                            {
                                _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Reject(_BudgetCOADestinationOne);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Budget COA Destination One Success", _Obj, "", param1, _BudgetCOADestinationOne.BudgetCOADestinationOnePK, _BudgetCOADestinationOne.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Budget COA Destination One Success");
                            }
                            if (PermissionID == "BudgetCOADestinationOne_I")
                            {
                                int _lastPKByLastUpdate = _BudgetCOADestinationOneReps.BudgetCOADestinationOne_Add(_BudgetCOADestinationOne, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Budget COA Destination One Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Budget COA Destination One Success");
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