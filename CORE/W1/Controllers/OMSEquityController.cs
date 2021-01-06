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
    public class OMSEquityController : ApiController
    {
        static readonly string _Obj = "OMS Equity Controller";
        static readonly OMSEquityReps _omsEquityReps = new OMSEquityReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        static readonly CustomClient05Reps _customClient05 = new CustomClient05Reps();
        static readonly CustomClient06Reps _customClient06 = new CustomClient06Reps();
        static readonly CustomClient09Reps _customClient09 = new CustomClient09Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly CustomClient28Reps _customClient28 = new CustomClient28Reps();
        static readonly CustomClient99Reps _customClient99 = new CustomClient99Reps();

        /*
    * param1 = userID
    * param2 = sessionID
    * param3 = FundPK
    * param4 = Date
    */
        [HttpGet]
        public HttpResponseMessage OMSEquity_PerFund(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquity_PerFund(param4, param3));

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_PerFund", param1, 0, 0, 0, "");
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
    * param3 = FundPK
    * param4 = Date
    */
        [HttpGet]
        public HttpResponseMessage OMSEquity_ListStockForYahooFinance(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquity_ListStockForYahooFinance(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_PerFund", param1, 0, 0, 0, "");
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
     * param3 = FundPK
     * param4 = Date
    */
        [HttpGet]
        public HttpResponseMessage OMSEquity_Exposure_PerFund(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSExposureEquity_PerFund(param4, param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_Exposure_PerFund", param1, 0, 0, 0, "");
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
       * param3 = FundPK
       * param4 = Date
      */
        [HttpGet]
        public HttpResponseMessage OMSEquity_BySectorID(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "09")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient09.OMSEquityBySector(param4, param3));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityBySector(param4, param3));
                        }
                
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_BySectorID", param1, 0, 0, 0, "");
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
    * param3 = FundPK
    * param4 = Date
   */
        [HttpGet]
        public HttpResponseMessage OMSEquity_ByIndex(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "09")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient09.OMSEquityByIndex(param4, param3));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityByIndex(param4, param3));
                        }
         
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_ByIndex", param1, 0, 0, 0, "");
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
* param3 = FundPK
* param4 = Date
*/
        [HttpGet]
        public HttpResponseMessage OMSEquity_ByInstrument(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "09")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient09.OMSEquityByInstrument(param4, param3));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityByInstrument(param4, param3));
                        }
          
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_ByInstrument", param1, 0, 0, 0, "");
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
      * param3 = FundPK
      * param4 = Date
      */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetNetBuySell(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetNetBuySell(param4, param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetNetBuySell", param1, 0, 0, 0, "");
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
   * param3 = FundPK
   * param4 = Date
   */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetDealingNetBuySellEquity(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetDealingNetBuySellEquity(param4, param3,param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetDealingNetBuySell", param1, 0, 0, 0, "");
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
  * param3 = FundPK
  * param4 = Date
  */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetSettlementNetBuySellEquity(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetSettlementNetBuySellEquity(param4, param3, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetSettlementNetBuySellEquity", param1, 0, 0, 0, "");
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
 * param3 = FundPK
 * param4 = Date
 */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetSettlementNetBuySellBond(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetSettlementNetBuySellBond(param4, param3, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetSettlementNetBuySellBond", param1, 0, 0, 0, "");
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
        * param3 = FundPK
        * param4 = Date
        */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetAvailableCash(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "05")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient05.OMSEquityGetNetAvailableCash(param4, param3));
                        }
                        //else if (Tools.ClientCode == "09")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient09.OMSEquityGetNetAvailableCash(param4, param3));
                        //}
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetNetAvailableCash(param4, param3, param5));
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetAvailableCash", param1, 0, 0, 0, "");
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
         * param3 = FundPK
         * param4 = Date
         */
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetAUMYesterday(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetAUMYesterday(param4, param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetAUMYesterday", param1, 0, 0, 0, "");
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
      * param3 = FundPK
      * param4 = Date
      */
        [HttpGet]
        public HttpResponseMessage OMSEquityGetNetAvailableCashDetail(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "05")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient05.OMSEquityGetNetAvailableCashDetail(param4, param3));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetNetAvailableCashDetail(param4, param3, param5));
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquityGetNetAvailableCashDetail", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateApproveBySelectedDataOMSEquity(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_ApproveBySelectedDataOMSEquity(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approved OMS Equity", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage ValidateRejectBySelectedDataOMSEquity(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_RejectBySelectedDataOMSEquity(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Reject OMS Equity", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
              * param1 = userID
              * param2 = sessionID
              */
        [HttpPost]
        public HttpResponseMessage ApproveOMSEquityBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "InvestmentInstruction_ApproveOMSEquityBySelected";
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
                            if (Tools.ClientCode == "05")
                            {
                                if (_customClient05.Validation_EntrierApproverByInvesment(param1, "Investment", "OMSEquity", _investment).Result == "0")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, _customClient05.Validation_EntrierApproverByInvesment(param1, "Investment", "OMSEquity", _investment).ResultText);

                                }
                                else
                                {
                                    int _lastPK = _omsEquityReps.Investment_ApproveOMSEquityBySelected(_investment);
                                    _host.Notification_Add(param1, "Data Investment Has Been Approved", PermissionID);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Investment Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                                    return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                                }
                            }
                            else
                            {

                                int _lastPK = _omsEquityReps.Investment_ApproveOMSEquityBySelected(_investment);
                                _host.Notification_Add(param1, "Data Investment Has Been Approved", PermissionID);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Investment Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
        /*
      * param1 = userID
      * param2 = sessionID
      */
        [HttpPost]
        public HttpResponseMessage RejectOMSEquityBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "InvestmentInstruction_RejectOMSEquityBySelected";
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
                            int _lastPK = _omsEquityReps.Investment_RejectOMSEquityBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Investment Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
        public HttpResponseMessage ValidateCheckAvailableCash(string param1, string param2, decimal param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "05")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient05.Validate_CheckAvailableCash(param3, param4, param5));
                        }
                        //else if (Tools.ClientCode == "09")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient09.Validate_CheckAvailableCash(param3, param4, param5));
                        //}
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_CheckAvailableCash(param3, param4, param5));
                        }
                      
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approved OMS Equity", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        [HttpPost]
        public HttpResponseMessage ValidateCheckAvailableInstrument(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_CheckAvailableInstrument(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Instrument", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage GenerateNavProjection(string param1, string param2, DateTime param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquity_GenerateNavProjection(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get NAV Projection", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckExposure(string param1, string param2, DateTime param3, int param4, int param5, decimal param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_CheckExposure(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage ValidateCheckPriceExposure(string param1, string param2, DateTime param3, int param4, int param5, decimal param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_CheckPriceExposure(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Price Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = FundPK
        * param4 = Date
        */
        [HttpGet]
        public HttpResponseMessage GetTotalLotForOMSEquity(string param1, string param2, int param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Get_TotalLotForOMSEquity(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetAUMYesterday", param1, 0, 0, 0, "");
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
        public HttpResponseMessage OMSEquityListingRpt(string param1, string param2, [FromBody]InvestmentListing _listing)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //if (Tools.ClientCode == "05")
                        //{
                        //    if (_customClient05.OMSEquity_ListingRpt(param1, _listing))
                        //    {
                        //        if (_listing.DownloadMode == "PDF")
                        //        {
                        //            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                        //        }
                        //        else
                        //        {
                        //            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                        //        }
                        //    }

                        //    else
                        //    {
                        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        //    }
                        //}
                        //else
                        //{
                        if (Tools.ClientCode == "06")
                        {
                            if (_customClient06.OMSEquity_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "20")
                        {
                            if (_customClient20.OMSEquity_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else if (Tools.ClientCode == "28")
                        {
                            if (_customClient28.OMSEquity_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else
                        {
                            if (_omsEquityReps.OMSEquity_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "OMSEquityListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        //}

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMS Equity Listing Rpt", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateUpdateCheckAvailableCash(string param1, string param2, int param3, decimal param4, decimal param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Validate_UpdateCheckAvailableCash(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approved OMS Equity", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
* param1 = userID
* param2 = sessionID
* param3 = FundPK
* param4 = Date
*/
        [HttpGet]
        public HttpResponseMessage OMSEquity_GetPendingCash(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                            return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquityGetNetPendingCash(param4, param3));
                       

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_GetAvailableCash", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InsertExposureRangePrice(string param1, string param2, DateTime param3, int param4, int param5, decimal param6, int param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.Insert_ExposureRangePrice(param3, param4, param5, param6, param7, param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Price Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage CheckExposureFromImport(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.OMSEquity_CheckExposureFromImport());

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "OMSEquity_PerFund", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InsertIntoInvestment(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _omsEquityReps.InsertIntoInvestment());
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Insert into Investment From Exposure Import OMS", param1, 0, 0, 0, "");
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
