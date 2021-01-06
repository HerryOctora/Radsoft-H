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
    public class COADestinationOneMappingRptController : ApiController
    {
        static readonly string _Obj = "COA Destination One MappingRpt Controller";
        static readonly COADestinationOneMappingRptReps _COADestinationOneMappingRptReps = new COADestinationOneMappingRptReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]COADestinationOneMappingRpt _COADestinationOneMappingRpt)
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
                            if (PermissionID == "COADestinationOneMappingRpt_U")
                            {
                                int _newHisPK = _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Update(_COADestinationOneMappingRpt, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update COA Destination One MappingRpt Success", _Obj, "", param1, _COADestinationOneMappingRpt.COADestinationOneMappingRptPK, _COADestinationOneMappingRpt.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update COA Destination One MappingRpt Success");
                            }
                            if (PermissionID == "COADestinationOneMappingRpt_A")
                            {
                                _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Approved(_COADestinationOneMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved COA Destination One MappingRpt Success", _Obj, "", param1, _COADestinationOneMappingRpt.COADestinationOneMappingRptPK, _COADestinationOneMappingRpt.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved COA Destination One MappingRpt Success");
                            }
                            if (PermissionID == "COADestinationOneMappingRpt_V")
                            {
                                _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Void(_COADestinationOneMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void COA Destination One MappingRpt Success", _Obj, "", param1, _COADestinationOneMappingRpt.COADestinationOneMappingRptPK, _COADestinationOneMappingRpt.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void COA Destination One MappingRpt Success");
                            }
                            if (PermissionID == "COADestinationOneMappingRpt_R")
                            {
                                _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Reject(_COADestinationOneMappingRpt);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject COA Destination One MappingRpt Success", _Obj, "", param1, _COADestinationOneMappingRpt.COADestinationOneMappingRptPK, _COADestinationOneMappingRpt.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject COA Destination One MappingRpt Success");
                            }
                            if (PermissionID == "COADestinationOneMappingRpt_I")
                            {
                                int _lastPKByLastUpdate = _COADestinationOneMappingRptReps.COADestinationOneMappingRpt_Add(_COADestinationOneMappingRpt, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add COA Destination One MappingRpt Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert COA Destination One MappingRpt Success");
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