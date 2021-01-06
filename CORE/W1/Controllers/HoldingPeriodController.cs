
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
    public class HoldingPeriodController : ApiController
    {
        static readonly string _Obj = "Holding Period Controller";
        static readonly HoldingPeriodReps _HoldingPeriodReps = new HoldingPeriodReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        [HttpGet]
        public HttpResponseMessage InitDataHoldingPeriod(string param1, string param2, string param3, string param4, DateTime param5, decimal param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _HoldingPeriodReps.Init_DataHoldingPeriod(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Init Data Holding Period", param1, 0, 0, 0, "");
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
        public string AddClientRed(string param1, string param2, [FromBody]List<ClientRedemptionHoldingPeriod> _ClientRedemptionHoldingPeriod)
        {
            string _msg = "";
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        try
                        {
                            return _msg = _HoldingPeriodReps.AddClientRedemption(_ClientRedemptionHoldingPeriod,param1);
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Investment_UpdateInvestmentAcq", param1, 0, 0, 0, "");
                    return "No Session";
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return "Internal Server Error";
            }
        }


    }
}
