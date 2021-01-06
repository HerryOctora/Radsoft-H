
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
    public class RiskProfileMonitoringController : ApiController
    {
        static readonly string _Obj = "Risk Profile Monitoring Controller";
        static readonly RiskProfileMonitoringReps _RiskProfileMonitoringReps = new RiskProfileMonitoringReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = value date
        */
        [HttpGet]
        public HttpResponseMessage GetDataByDate(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _RiskProfileMonitoringReps.RiskProfileMonitoring_SelectRiskProfileMonitoringByDate(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data By Date", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = permissionID
        */
        //[HttpPost]
        //public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]RiskProfileMonitoring _RiskProfileMonitoring)
        //{
        //    string PermissionID;
        //    PermissionID = param3;
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                bool havePermission = _host.Get_Permission(param1, PermissionID);
        //                if (havePermission)
        //                {
        //                    bool havePrivillege = _host.Get_Privillege(param1, PermissionID);
        //                    if (PermissionID == "RiskProfileMonitoring_Suspend")
        //                    {
        //                        _RiskProfileMonitoringReps.RiskProfileMonitoring_Suspend(_RiskProfileMonitoring);
        //                        _activityReps.Activity_ActionInsert(DateTime.Now, PermissionID, true, "Suspend FundClient Success", "", "", param1, _RiskProfileMonitoring.FundClientPK);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Suspend FundClient Success");
        //                    }
        //                    else if (PermissionID == "RiskProfileMonitoring_AutoSuspend")
        //                    {
        //                        _RiskProfileMonitoringReps.RiskProfileMonitoring_AutoSuspend(_RiskProfileMonitoring);
        //                        _activityReps.Activity_ActionInsert(DateTime.Now, PermissionID, true, "Auto Suspend FundClient Success", "", "", param1, _RiskProfileMonitoring.FundClientPK);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Auto Suspend FundClient Success");
        //                    }
        //                    else if (PermissionID == "RiskProfileMonitoring_Activated")
        //                    {
        //                        _RiskProfileMonitoringReps.RiskProfileMonitoring_Activated(_RiskProfileMonitoring);
        //                        _activityReps.Activity_ActionInsert(DateTime.Now, PermissionID, true, "Activated FundClient Success", "", "", param1, _RiskProfileMonitoring.FundClientPK);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Activated FundClient Success");
        //                    }
        //                    else
        //                    {
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoActionMessage);
        //                    }
        //                }
        //                else
        //                {
        //                    _activityReps.Activity_Insert(DateTime.Now, PermissionID, false, Tools.NoPermissionLogMessage, param1, param2, param1);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
        //                }
        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Action", param1);
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage + " : Action = " + PermissionID, err.Message, err.StackTrace, param1);
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}



    }
}
