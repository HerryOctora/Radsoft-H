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
    public class MFeeSetupOptionalController : ApiController
    {
        static readonly string _Obj = "MFeeSetupOptional Controller";
        static readonly MFeeSetupOptionalReps _MFeeSetupOptionalReps = new MFeeSetupOptionalReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _MFeeSetupOptionalReps.MFeeSetupOptional_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]MFeeSetupOptional _MFeeSetupOptional)
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
                            if (PermissionID == "MFeeSetupOptional_U")
                            {
                                int _newHisPK = _MFeeSetupOptionalReps.MFeeSetupOptional_Update(_MFeeSetupOptional, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update MFeeSetupOptional Success", _Obj, "", param1, _MFeeSetupOptional.MFeeSetupOptionalPK, _MFeeSetupOptional.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update MFeeSetupOptional Success");
                            }
                            if (PermissionID == "MFeeSetupOptional_A")
                            {
                                _MFeeSetupOptionalReps.MFeeSetupOptional_Approved(_MFeeSetupOptional);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved MFeeSetupOptional Success", _Obj, "", param1, _MFeeSetupOptional.MFeeSetupOptionalPK, _MFeeSetupOptional.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved MFeeSetupOptional Success");
                            }
                            if (PermissionID == "MFeeSetupOptional_V")
                            {
                                _MFeeSetupOptionalReps.MFeeSetupOptional_Void(_MFeeSetupOptional);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void MFeeSetupOptional Success", _Obj, "", param1, _MFeeSetupOptional.MFeeSetupOptionalPK, _MFeeSetupOptional.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void MFeeSetupOptional Success");
                            }
                            if (PermissionID == "MFeeSetupOptional_R")
                            {
                                _MFeeSetupOptionalReps.MFeeSetupOptional_Reject(_MFeeSetupOptional);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject MFeeSetupOptional Success", _Obj, "", param1, _MFeeSetupOptional.MFeeSetupOptionalPK, _MFeeSetupOptional.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject MFeeSetupOptional Success");
                            }
                            if (PermissionID == "MFeeSetupOptional_I")
                            {
                                int _lastPKByLastUpdate = _MFeeSetupOptionalReps.MFeeSetupOptional_Add(_MFeeSetupOptional, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add MFeeSetupOptional Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert MFeeSetupOptional Success");
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