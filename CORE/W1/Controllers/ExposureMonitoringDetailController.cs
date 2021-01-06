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
    public class ExposureMonitoringDetailController : ApiController
    {
        static readonly string _Obj = "Exposure Monitoring Detail Controller";
        static readonly ExposureMonitoringDetailReps _exposureMonitoringDetailReps = new ExposureMonitoringDetailReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = status(pending = 1, approve = 2, history = 3)
        */
        [HttpGet]
        public HttpResponseMessage GetData(string param1, string param2, int param3,DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _exposureMonitoringDetailReps.ExposureMonitoringDetail_Select(param3,param4,param5));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]ExposureMonitoringDetail _exposureMonitoringDetail)
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
                            if (PermissionID == "ExposureMonitoringDetail_U")
                            {
                                int _newHisPK = _exposureMonitoringDetailReps.ExposureMonitoringDetail_Update(_exposureMonitoringDetail, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Exposure Monitoring Detail Success", _Obj, "", param1, _exposureMonitoringDetail.ExposureMonitoringDetailPK, _exposureMonitoringDetail.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Exposure Monitoring Detail Success");
                            }
                            if (PermissionID == "ExposureMonitoringDetail_A")
                            {
                                _exposureMonitoringDetailReps.ExposureMonitoringDetail_Approved(_exposureMonitoringDetail);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Exposure Monitoring Detail Success", _Obj, "", param1, _exposureMonitoringDetail.ExposureMonitoringDetailPK, _exposureMonitoringDetail.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Exposure Monitoring Detail Success");
                            }
                            if (PermissionID == "ExposureMonitoringDetail_V")
                            {
                                _exposureMonitoringDetailReps.ExposureMonitoringDetail_Void(_exposureMonitoringDetail);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Exposure Monitoring Detail Success", _Obj, "", param1, _exposureMonitoringDetail.ExposureMonitoringDetailPK, _exposureMonitoringDetail.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Exposure Monitoring Detail Success");
                            }
                            if (PermissionID == "ExposureMonitoringDetail_R")
                            {
                                _exposureMonitoringDetailReps.ExposureMonitoringDetail_Reject(_exposureMonitoringDetail);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Exposure Monitoring Detail Success", _Obj, "", param1, _exposureMonitoringDetail.ExposureMonitoringDetailPK, _exposureMonitoringDetail.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Exposure Monitoring Detail Success");
                            }
                            if (PermissionID == "ExposureMonitoringDetail_I")
                            {
                                int _lastPKByLastUpdate = _exposureMonitoringDetailReps.ExposureMonitoringDetail_Add(_exposureMonitoringDetail, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Exposure Monitoring Detail Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Exposure Monitoring Detail Success");
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