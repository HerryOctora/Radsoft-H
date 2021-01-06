
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
    public class FFSSetup_21Controller : ApiController
    {
        static readonly string _Obj = "FFSSetup_21 Controller";
        static readonly FFSSetup_21Reps _FFSSetup_21Reps = new FFSSetup_21Reps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FFSSetup_21Reps.FFSSetup_21_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FFSSetup_21 _FFSSetup_21)
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
                            if (PermissionID == "FFSSetup_21_U")
                            {
                                int _newHisPK = _FFSSetup_21Reps.FFSSetup_21_Update(_FFSSetup_21, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FFS Setup Success", _Obj, "", param1, _FFSSetup_21.FFSSetup_21PK, _FFSSetup_21.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FFS Setup Success");
                            }
                            if (PermissionID == "FFSSetup_21_A")
                            {
                                _FFSSetup_21Reps.FFSSetup_21_Approved(_FFSSetup_21);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FFS Setup Success", _Obj, "", param1, _FFSSetup_21.FFSSetup_21PK, _FFSSetup_21.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FFS Setup Success");
                            }
                            if (PermissionID == "FFSSetup_21_V")
                            {
                                _FFSSetup_21Reps.FFSSetup_21_Void(_FFSSetup_21);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FFS Setup Success", _Obj, "", param1, _FFSSetup_21.FFSSetup_21PK, _FFSSetup_21.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FFS Setup Success");
                            }
                            if (PermissionID == "FFSSetup_21_R")
                            {
                                _FFSSetup_21Reps.FFSSetup_21_Reject(_FFSSetup_21);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FFS Setup Success", _Obj, "", param1, _FFSSetup_21.FFSSetup_21PK, _FFSSetup_21.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FFS Setup Success");
                            }
                            if (PermissionID == "FFSSetup_21_I")
                            {
                                int _lastPK = _FFSSetup_21Reps.FFSSetup_21_Add(_FFSSetup_21, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FFS Setup Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert FFS Setup Success");
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