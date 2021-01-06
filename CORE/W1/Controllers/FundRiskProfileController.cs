using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.IO;
using System.Net.Http.Headers;


namespace W1.Controllers
{
    public class FundRiskProfileController : ApiController
    {
        static readonly string _Obj = "Fund Risk Profile Controller";
        static readonly FundRiskProfileReps _FundRiskProfileReps = new FundRiskProfileReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FundRiskProfileReps.FundRiskProfile_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundRiskProfile _FundRiskProfile)
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
                            if (PermissionID == "FundRiskProfile_U")
                            {
                                int _newHisPK = _FundRiskProfileReps.FundRiskProfile_Update(_FundRiskProfile, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Fund Risk Profile Success", _Obj, "", param1, _FundRiskProfile.FundRiskProfilePK, _FundRiskProfile.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fund Risk Profile Success");
                            }
                            if (PermissionID == "FundRiskProfile_A")
                            {
                                _FundRiskProfileReps.FundRiskProfile_Approved(_FundRiskProfile);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Fund Risk Profile Success", _Obj, "", param1, _FundRiskProfile.FundRiskProfilePK, _FundRiskProfile.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fund Risk Profile Success");
                            }
                            if (PermissionID == "FundRiskProfile_V")
                            {
                                _FundRiskProfileReps.FundRiskProfile_Void(_FundRiskProfile);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Fund Risk Profile Success", _Obj, "", param1, _FundRiskProfile.FundRiskProfilePK, _FundRiskProfile.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fund Risk Profile Success");
                            }
                            if (PermissionID == "FundRiskProfile_R")
                            {
                                _FundRiskProfileReps.FundRiskProfile_Reject(_FundRiskProfile);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Fund Risk Profile Success", _Obj, "", param1, _FundRiskProfile.FundRiskProfilePK, _FundRiskProfile.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fund Risk Profile Success");
                            }
                            if (PermissionID == "FundRiskProfile_I")
                            {
                                int _lastPK = _FundRiskProfileReps.FundRiskProfile_Add(_FundRiskProfile, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Risk Profile Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Fund Risk Profile Success");
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
