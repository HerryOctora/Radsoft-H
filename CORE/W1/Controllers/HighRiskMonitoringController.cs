
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
    public class HighRiskMonitoringController : ApiController
    {
        static readonly string _Obj = "High Risk Monitoring Controller";
        static readonly HighRiskMonitoringReps _highRiskMonitoringReps = new HighRiskMonitoringReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();
        static readonly CustomClient08Reps _customClient08 = new CustomClient08Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();

        //0 fund client
        //2 investment (OMS)
        //3 SUBS (UR)
        //4 RED (UR)
        //5 SWI (UR)
        //Custom Nikko
        //97 Client Subs 
        //98 BlackList
        //99 EDD


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = FundClientPK
        * param4 = FundPK
        */
        [HttpGet]
        public HttpResponseMessage CheckInvestorAndFundRiskProfile(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_CheckInvestorAndFundRiskProfile(param4, param3, param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
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
       * param3 = FundClientPK
       */
        [HttpGet]
        public HttpResponseMessage CheckClientSuspend(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_CheckClientSuspend(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Client Suspend", param1, 0, 0, 0, "");
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
        * param3 = status(pending = 1, approve = 2, history = 3)
        */
        [HttpGet]
        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "20")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_SelectHighRiskMonitoringDateFromToCustomClient20(param3, param4, param5, param6));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_SelectHighRiskMonitoringDateFromTo(param3, param4, param5));
                        }
                       
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data ByDate From To", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]HighRiskMonitoring _highRiskMonitoring)
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

                            if (PermissionID == "HighRiskMonitoring_A")
                            {
                                _highRiskMonitoringReps.HighRiskMonitoring_Approved(_highRiskMonitoring);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved High Risk Monitoring Success", _Obj, "", param1, _highRiskMonitoring.HighRiskMonitoringPK, _highRiskMonitoring.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved High Risk Monitoring Success");
                            }
                            else if (PermissionID == "HighRiskMonitoring_UnApproved")
                            {
                                _highRiskMonitoringReps.HighRiskMonitoring_UnApproved(_highRiskMonitoring);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved High Risk Monitoring Success", _Obj, "", param1, _highRiskMonitoring.HighRiskMonitoringPK, _highRiskMonitoring.HistoryPK, 0, "UNAPPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "UnApproved High Risk Monitoring Success");
                            }
                            else if (PermissionID == "HighRiskMonitoring_U")
                            {
                                int _newHisPK = _highRiskMonitoringReps.HighRiskMonitoring_Update(_highRiskMonitoring, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update HighRiskMonitoring  Success", _Obj, "", param1, _highRiskMonitoring.HighRiskMonitoringPK, _highRiskMonitoring.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update High Risk Monitoring  Success");
                            }
                            else if (PermissionID == "HighRiskMonitoring_R")
                            {
                                _highRiskMonitoringReps.HighRiskMonitoring_Reject(_highRiskMonitoring);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject HighRiskMonitoring Success", _Obj, "", param1, _highRiskMonitoring.HighRiskMonitoringPK, _highRiskMonitoring.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject High Risk Monitoring Success");
                            }
                            else if (PermissionID == "HighRiskMonitoring_V")
                            {
                                _highRiskMonitoringReps.HighRiskMonitoring_Void(_highRiskMonitoring);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void High Risk Monitoring Success", _Obj, "", param1, _highRiskMonitoring.HighRiskMonitoringPK, _highRiskMonitoring.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void High Risk Monitoring Success");
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
        public HttpResponseMessage CheckMaxUnitFundandIncomePerAnnum(string param1, string param2, decimal param3, int param4, DateTime param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "08")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient08.HighRiskMonitoring_CheckMaxUnitFundAndIncomePerAnnum(param1, param3, param4, param5, param6));     
                        }
                        else
                        { 
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_CheckMaxUnitFundAndIncomePerAnnum(param1, param3, param4, param5, param6));
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckStatusHighRiskMonitoring(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.CheckHighRiskMonitoringStatus(param3,param4, _paramUnitRegistryBySelected));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage ValidasiCustomClient08(string param1, string param2, [FromBody]ClientSubscription _ClientSubs)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.ValidateCustomClient08(_ClientSubs));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Custom Client 08", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DormantValidation(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_ValidateDormantClient(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddUnitRegistry", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckExposurePreTrade(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.Validate_CheckExposurePreTrade(_investment));
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage CheckDescription(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.Validate_CheckDescription(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage CheckDataHighRiskSetup(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.Validate_CheckDataHighRiskSetup(param3, param4, param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        /*
  * param1 = userID
  * param2 = sessionID
  * param3 = DateFrom
  * param4 = DateTo
  */
        [HttpGet]
        public HttpResponseMessage SuspendBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "HighRiskMonitoring_SuspendBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _highRiskMonitoringReps.HighRiskMonitoring_SuspendBySelected(param1, PermissionID, param3, param4);
                            return Request.CreateResponse(HttpStatusCode.OK, "Suspend All By Selected Success");
                        }
                        catch (Exception err)
                        {

                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }

                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Suspend By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InsertHighRiskMonitoring_ExposurePreTrade(string param1, string param2, [FromBody]HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "99")
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoring_ExposurePreTrade(param1, _highRiskMonitoring));
                        else
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoring_ExposurePreTrade(param1, _highRiskMonitoring));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        
        [HttpPost]
        public HttpResponseMessage InsertHighRiskMonitoring_IncomeExposure(string param1, string param2, [FromBody]HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoring_IncomeExposure(param1, _highRiskMonitoring));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Income Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertHighRiskMonitoringCustomClient08(string param1, string param2, [FromBody]ClientSubscription _ClientSubs)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoringCustomClient08(param1, _ClientSubs));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Income Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckCounterpartExposureFromHighRisk(string param1, string param2, [FromBody]HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.Check_CounterpartExposureFromHighRisk(_highRiskMonitoring));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check_CounterpartExposureFromHighRisk", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage HighRiskMonitoring_CheckHighRiskMonitoringFromSubscription(string param1, string param2, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_CheckHighRiskMonitoringFromSubscription(_clientSubscription));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
               
        [HttpPost]
        public HttpResponseMessage HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription(string param1, string param2, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription(_clientSubscription));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        
        [HttpPost]
        public HttpResponseMessage EmailExposure(string param1, string param2, [FromBody]HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                try
                {
                    //setup untuk id approved
                    string _BodyMessage, _ID, _Session;
                    _ID = param1;
                    _Session = param2;

                    DateTime _dateTimeNow = DateTime.Now;

                    ////update session time
                    //UsersReps _userReps = new UsersReps();
                    //_userReps.Users_UpdateSessionID(_Session, _ID, "");

                    _BodyMessage = _highRiskMonitoringReps.EmailExposure(_ID, _Session, _highRiskMonitoring);

                    if (_BodyMessage != "")
                    {
                        SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                        dt = SendEmailReps.SendEmailTestingByInput(param1, Tools._EmailHighRiskMonitoring, "Breach Investment " + _dateTimeNow, _BodyMessage, "", "");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, _BodyMessage);
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage ApprovedFromEmail(int param1, string param2)
        {
            try
            {
                try
                {
                    int _status;
                    _status = _highRiskMonitoringReps.HighRiskMonitoring_ApprovedFromEmail(param1, param2);

                    if (_status == 1)
                        return Request.CreateResponse(HttpStatusCode.OK, "Approved High Risk Monitoring By Email");
                    if (_status == 2)
                        return Request.CreateResponse(HttpStatusCode.OK, "Data has been approved/rejected");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "Session has Expired, please check High Risk Monitoring on system");
                }
                catch (Exception err)
                {

                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._EmailHighRiskMonitoring, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage RejectFromEmail(int param1, string param2)
        {
            try
            {
                try
                {
                    int _status;
                    _status = _highRiskMonitoringReps.HighRiskMonitoring_RejectFromEmail(param1, param2);

                    if (_status == 1)
                        return Request.CreateResponse(HttpStatusCode.OK, "Reject High Risk Monitoring By Email");
                    if (_status == 2)
                        return Request.CreateResponse(HttpStatusCode.OK, "Data has been approved/rejected");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "Session has Expired, please check High Risk Monitoring on system");
                }
                catch (Exception err)
                {

                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._EmailHighRiskMonitoring, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage EmailFundExposure(string param1, string param2, [FromBody]HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                try
                {
                    //setup untuk id approved
                    string _BodyMessage, _ID, _Session;
                    _ID = param1;
                    _Session = param2;

                    ////update session time
                    //UsersReps _userReps = new UsersReps();
                    //_userReps.Users_UpdateSessionID(_Session, _ID, "");

                    _BodyMessage = _highRiskMonitoringReps.EmailFundExposure(_ID, _Session, _highRiskMonitoring);

                    if (_BodyMessage != "")
                    {
                        SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                        dt = SendEmailReps.SendEmailTestingByInput(param1, Tools._EmailHighRiskMonitoring, "Breach Investment", _BodyMessage, "", "");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, _BodyMessage);
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage InsertHighRiskMonitoring_ExposureFromOMSEquity(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoring_ExposureFromOMSEquity(param1, param3));

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CheckInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage EmailFundExposureFromOMSEquity(string param1, string param2, string param3)
        {
            try
            {
                try
                {
                    //setup untuk id approved
                    string _BodyMessage, _ID, _Session;
                    _ID = param1;
                    _Session = param2;

                    ////update session time
                    //UsersReps _userReps = new UsersReps();
                    //_userReps.Users_UpdateSessionID(_Session, _ID, "");

                    _BodyMessage = _highRiskMonitoringReps.EmailFundExposureFromImportOMSEquity(_ID, _Session, param3);

                    if (_BodyMessage != "")
                    {
                        SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                        dt = SendEmailReps.SendEmailTestingByInput(param1, Tools._EmailHighRiskMonitoring, "Breach Investment", _BodyMessage, "", "");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, _BodyMessage);
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage InsertHighRiskMonitoringCustomClient20(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _customClient20.Insert_HighRiskMonitoringCustomClient20(param1, param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Income Exposure", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage ValidationHighRiskNikko(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_ValidationHighRiskNikko(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddUnitRegistry", param1, 0, 0, 0, "");
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
        public HttpResponseMessage UpdateKYCFromHighRiskMonitoring(string param1, string param2, [FromBody]HighRiskMonitoring _HighRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.UpdateKYCFromHighRiskMonitoring(param1, _HighRiskMonitoring));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Update KYC from High Risk Monitoring", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage Check100MilClient(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_Check100MilClient(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check 100 Mil Client", param1, 0, 0, 0, "");
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
        * param3 = FundClientPK
        * param4 = FundPK
        */
        [HttpGet]
        public HttpResponseMessage ValidateInvestorAndFundRiskProfile(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_ValidateInvestorAndFundRiskProfile(param4, param3, param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate InvestorAndFundRiskProfile", param1, 0, 0, 0, "");
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
        * param3 = FundClientPK
        * param4 = FundPK
        */
        [HttpGet]
        public HttpResponseMessage InsertHighRiskMonitoringInvestorAndFundRiskProfile(string param1, string param2, [FromBody] HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _highRiskMonitoringReps.HighRiskMonitoring_InsertHighRiskMonitoringInvestorAndFundRiskProfile(param1, _highRiskMonitoring);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert into highriskmonitoring success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertHighRiskMonitoringInvestorAndFundRiskProfile", param1, 0, 0, 0, "");
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
        * param3 = CashAmount
        * param4 = FundPK
        * param5 = NAVDate
        * param6 = FundClientPK
        */
        [HttpGet]
        public HttpResponseMessage ValidateMaxUnitFundandIncomePerAnnum(string param1, string param2, decimal param3, int param4, DateTime param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "08")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient08.HighRiskMonitoring_ValidateMaxUnitFundAndIncomePerAnnum(param1, param3, param4, param5, param6));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.HighRiskMonitoring_ValidateMaxUnitFundAndIncomePerAnnum(param1, param3, param4, param5, param6));
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateMaxUnitFundAndIncomePerAnnum", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InsertHighRiskMonitoringMaxUnitFundandIncomePerAnnum(string param1, string param2, [FromBody] HighRiskMonitoring _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _highRiskMonitoringReps.HighRiskMonitoring_InsertHighRiskMonitoringMaxUnitFundAndIncomePerAnnum(param1, _highRiskMonitoring);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert into highriskmonitoring success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertHighRiskMonitoringMaxUnitFundAndIncomePerAnnum", param1, 0, 0, 0, "");
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
        * param3 = valuedate
        */
        [HttpPost]
        public HttpResponseMessage InsertIntoHighRiskMonitoringByClientSubscription(string param1, string param2, DateTime param3, [FromBody] List<ValidateClientSubscription> _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _highRiskMonitoringReps.HighRiskMonitoring_InsertIntoHighRiskMonitoringByClientSubscription(param1, param3, _highRiskMonitoring);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert into highriskmonitoring success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertHighRiskMonitoringMaxUnitFundAndIncomePerAnnum", param1, 0, 0, 0, "");
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
        * param3 = valuedate
        */
        [HttpPost]
        public HttpResponseMessage InsertIntoHighRiskMonitoringByUnitRegistry(string param1, string param2, DateTime param3, [FromBody] List<ValidateUnitRegistry> _highRiskMonitoring)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _highRiskMonitoringReps.HighRiskMonitoring_InsertIntoHighRiskMonitoringByUnitRegistry(param1, param3, _highRiskMonitoring);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert into highriskmonitoring success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertIntoHighRiskMonitoringByUnitRegistry", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InsertHighRiskMonitoring_ExposureFromOMSTimeDeposit(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _highRiskMonitoringReps.InsertHighRiskMonitoring_ExposureFromOMSTimeDeposit(param1, param3));

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertHighRiskMonitoring_ExposureFromOMSTimeDeposit", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage EmailFundExposureFromOMSTimeDeposit(string param1, string param2, string param3)
        {
            try
            {
                try
                {
                    //setup untuk id approved
                    string _BodyMessage, _ID, _Session;
                    _ID = param1;
                    _Session = param2;

                    ////update session time
                    //UsersReps _userReps = new UsersReps();
                    //_userReps.Users_UpdateSessionID(_Session, _ID, "");

                    _BodyMessage = _highRiskMonitoringReps.EmailFundExposureFromImportOMSTimeDeposit(_ID, _Session, param3);

                    if (_BodyMessage != "")
                    {
                        SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                        dt = SendEmailReps.SendEmailTestingByInput(param1, Tools._EmailHighRiskMonitoring, "Breach Investment", _BodyMessage, "", "");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, _BodyMessage);
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }


    }
}
