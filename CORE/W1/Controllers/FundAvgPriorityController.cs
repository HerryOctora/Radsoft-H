
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
    public class FundAvgPriorityController : ApiController
    {
        static readonly string _Obj = "FundAvgPriority Controller";
        static readonly FundAvgPriorityReps _FundAvgPriorityReps = new FundAvgPriorityReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        ///*
        // * param1 = userID
        // * param2 = sessionID
        // * param3 = status(pending = 1, approve = 2, history = 3)
        // */
        //[HttpGet]
        //public HttpResponseMessage GetGroupsCombo(string param1, string param2)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _groupsReps.Groups_Combo());
        //            }
        //            catch (Exception err)
        //            {

        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Groups Combo", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}


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
                        return Request.CreateResponse(HttpStatusCode.OK, _FundAvgPriorityReps.FundAvgPriority_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundAvgPriority _FundAvgPriority)
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
                            if (PermissionID == "FundAvgPriority_U")
                            {
                                int _newHisPK = _FundAvgPriorityReps.FundAvgPriority_Update(_FundAvgPriority, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FundAvgPriority Success", _Obj, "", param1, _FundAvgPriority.FundAvgPriorityPK, _FundAvgPriority.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FundAvgPriority Success");
                            }
                            if (PermissionID == "FundAvgPriority_A")
                            {
                                _FundAvgPriorityReps.FundAvgPriority_Approved(_FundAvgPriority);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FundAvgPriority Success", _Obj, "", param1, _FundAvgPriority.FundAvgPriorityPK, _FundAvgPriority.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FundAvgPriority Success");
                            }
                            if (PermissionID == "FundAvgPriority_V")
                            {
                                _FundAvgPriorityReps.FundAvgPriority_Void(_FundAvgPriority);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FundAvgPriority Success", _Obj, "", param1, _FundAvgPriority.FundAvgPriorityPK, _FundAvgPriority.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FundAvgPriority Success");
                            }
                            if (PermissionID == "FundAvgPriority_R")
                            {
                                _FundAvgPriorityReps.FundAvgPriority_Reject(_FundAvgPriority);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FundAvgPriority Success", _Obj, "", param1, _FundAvgPriority.FundAvgPriorityPK, _FundAvgPriority.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FundAvgPriority Success");
                            }
                            if (PermissionID == "FundAvgPriority_I")
                            {
                                int _lastPKByLastUpdate = _FundAvgPriorityReps.FundAvgPriority_Add(_FundAvgPriority, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FundAvgPriority Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert FundAvgPriority Success");
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