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
    public class InstrumentMarketInfoController : ApiController
    {
        static readonly string _Obj = "Instrument Market Info";
        static readonly InstrumentMarketInfoReps _instrumentMarketInfoReps = new InstrumentMarketInfoReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();
        /*
          * param1 = userID
          * param2 = sessionID
          */
        [HttpPost]
        public HttpResponseMessage InstrumentMarketInfo_ReNewData(string param1, string param2, [FromBody]List<InstrumentMarketInfo> _listInstrumentMarketInfo)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _instrumentMarketInfoReps.InstrumentMarketInfo_ReNewData(_listInstrumentMarketInfo);
                        return Request.CreateResponse(HttpStatusCode.OK, "Renew Market Info Success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InstrumentMarketInfo_ReNewData", param1, 0, 0, 0, "");
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
