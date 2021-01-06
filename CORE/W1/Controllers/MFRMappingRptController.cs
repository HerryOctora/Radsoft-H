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
    public class MFRMappingRptController : ApiController
    {
        static readonly string _Obj = "MFRMappingRpt Controller";
        static readonly MFRMappingRptReps _MFRMappingRptReps = new MFRMappingRptReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _MFRMappingRptReps.MFRMappingRpt_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]MFRMappingRpt _MFRMappingRpt)
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
                            if (PermissionID == "MFRMappingRpt_U")
                            {
                                int _newHisPK = _MFRMappingRptReps.MFRMappingRpt_Update(_MFRMappingRpt, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update MFR Mapping Rpt Success", _Obj, "", param1, _MFRMappingRpt.MFRMappingRptPK, _MFRMappingRpt.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update MFR Mapping Rpt Success");
                            }
                            if (PermissionID == "MFRMappingRpt_A")
                            {
                                _MFRMappingRptReps.MFRMappingRpt_Approved(_MFRMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved MFR Mapping Rpt Success", _Obj, "", param1, _MFRMappingRpt.MFRMappingRptPK, _MFRMappingRpt.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved MFR Mapping Rpt Success");
                            }
                            if (PermissionID == "MFRMappingRpt_V")
                            {
                                _MFRMappingRptReps.MFRMappingRpt_Void(_MFRMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void MFR Mapping Rpt Success", _Obj, "", param1, _MFRMappingRpt.MFRMappingRptPK, _MFRMappingRpt.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void MFR Mapping Rpt Success");
                            }
                            if (PermissionID == "MFRMappingRpt_R")
                            {
                                _MFRMappingRptReps.MFRMappingRpt_Reject(_MFRMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject MFR Mapping Rpt Success", _Obj, "", param1, _MFRMappingRpt.MFRMappingRptPK, _MFRMappingRpt.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject MFR Mapping Rpt Success");
                            }
                            if (PermissionID == "MFRMappingRpt_I")
                            {
                                int _lastPKByLastUpdate = _MFRMappingRptReps.MFRMappingRpt_Add(_MFRMappingRpt, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add MFR Mapping Rpt Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert MFR Mapping Rpt Success");
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


        [HttpGet]
        public HttpResponseMessage ValidateCheckRowMapping(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _MFRMappingRptReps.Validate_CheckRowMapping(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Row Mapping", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

    }
}