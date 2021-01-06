
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
    public class FundClientConsigneeMappingController : ApiController
    {
        static readonly string _Obj = "FundClientConsigneeMapping Controller";
        static readonly FundClientConsigneeMappingReps _FundClientConsigneeMappingReps = new FundClientConsigneeMappingReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundClientConsigneeMapping _FundClientConsigneeMapping)
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
                            if (PermissionID == "FundClientConsigneeMapping_U")
                            {
                                int _newHisPK = _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Update(_FundClientConsigneeMapping, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FundClientConsigneeMapping Success", _Obj, "", param1, _FundClientConsigneeMapping.FundClientConsigneeMappingPK, _FundClientConsigneeMapping.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FundClientConsigneeMapping Success");
                            }
                            if (PermissionID == "FundClientConsigneeMapping_A")
                            {
                                _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Approved(_FundClientConsigneeMapping);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FundClientConsigneeMapping Success", _Obj, "", param1, _FundClientConsigneeMapping.FundClientConsigneeMappingPK, _FundClientConsigneeMapping.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FundClientConsigneeMapping Success");
                            }
                            if (PermissionID == "FundClientConsigneeMapping_V")
                            {
                                _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Void(_FundClientConsigneeMapping);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FundClientConsigneeMapping Success", _Obj, "", param1, _FundClientConsigneeMapping.FundClientConsigneeMappingPK, _FundClientConsigneeMapping.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FundClientConsigneeMapping Success");
                            }
                            if (PermissionID == "FundClientConsigneeMapping_R")
                            {
                                _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Reject(_FundClientConsigneeMapping);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FundClientConsigneeMapping Success", _Obj, "", param1, _FundClientConsigneeMapping.FundClientConsigneeMappingPK, _FundClientConsigneeMapping.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FundClientConsigneeMapping Success");
                            }
                            if (PermissionID == "FundClientConsigneeMapping_I")
                            {
                                int _lastPK = _FundClientConsigneeMappingReps.FundClientConsigneeMapping_Add(_FundClientConsigneeMapping, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FundClientConsigneeMapping Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert FundClientConsigneeMapping Success");
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
