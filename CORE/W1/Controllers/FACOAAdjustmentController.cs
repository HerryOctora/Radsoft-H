
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
    public class FACOAAdjustmentController : ApiController
    {
        static readonly string _Obj = "FACOAAdjustment Controller";
        static readonly FACOAAdjustmentReps _FACOAAdjustmentReps = new FACOAAdjustmentReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3)
         */
        [HttpGet]
        public HttpResponseMessage GetFACOAAdjustmentCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FACOAAdjustmentReps.FACOAAdjustment_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get FACOAAdjustment Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FACOAAdjustmentReps.FACOAAdjustment_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FACOAAdjustment _FACOAAdjustment)
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
                            if (PermissionID == "FACOAAdjustment_U")
                            {
                                int _newHisPK = _FACOAAdjustmentReps.FACOAAdjustment_Update(_FACOAAdjustment, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FACOAAdjustment Success", _Obj, "", param1, _FACOAAdjustment.FACOAAdjustmentPK, _FACOAAdjustment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FACOAAdjustment Success");
                            }
                            if (PermissionID == "FACOAAdjustment_A")
                            {
                                _FACOAAdjustmentReps.FACOAAdjustment_Approved(_FACOAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FACOAAdjustment Success", _Obj, "", param1, _FACOAAdjustment.FACOAAdjustmentPK, _FACOAAdjustment.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FACOAAdjustment Success");
                            }
                            if (PermissionID == "FACOAAdjustment_V")
                            {
                                _FACOAAdjustmentReps.FACOAAdjustment_Void(_FACOAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FACOAAdjustment Success", _Obj, "", param1, _FACOAAdjustment.FACOAAdjustmentPK, _FACOAAdjustment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FACOAAdjustment Success");
                            }
                            if (PermissionID == "FACOAAdjustment_R")
                            {
                                _FACOAAdjustmentReps.FACOAAdjustment_Reject(_FACOAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FACOAAdjustment Success", _Obj, "", param1, _FACOAAdjustment.FACOAAdjustmentPK, _FACOAAdjustment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FACOAAdjustment Success");
                            }
                            if (PermissionID == "FACOAAdjustment_I")
                            {
                                int _lastPKByLastUpdate = _FACOAAdjustmentReps.FACOAAdjustment_Add(_FACOAAdjustment, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FACOAAdjustment Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert FACOAAdjustment Success");
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