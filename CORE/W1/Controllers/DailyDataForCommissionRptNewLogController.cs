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
    public class DailyDataForCommissionRptNewLogController : ApiController
    {
        static readonly string _Obj = "Daily Data For Commission Rpt New Log Controller";
        static readonly DailyDataForCommissionRptNewLogReps _DailyDataForCommissionRptNewLogReps = new DailyDataForCommissionRptNewLogReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
* param1 = userID
* param2 = sessionID
* param3 = status(pending = 1, approve = 2, history = 3)
*/
        [HttpGet]
        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _DailyDataForCommissionRptNewLogReps.DailyDataForCommissionRptNewLog_SelectByDateFromTo(param3));
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


    }
}