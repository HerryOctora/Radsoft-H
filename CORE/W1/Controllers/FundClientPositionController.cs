
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
    public class FundClientPositionController : ApiController
    {
        static readonly string _Obj = "Fund Client Position Controller";
        static readonly FundClientPositionReps _FundClientPositionReps = new FundClientPositionReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();
        static readonly CustomClient10Reps _CustomClient10Reps = new CustomClient10Reps();


        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                
                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientPositionReps.FundClientPosition_SelectByDateFromTo(param3, param4, param5));
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


        [HttpGet]
        public HttpResponseMessage ValidateCashAmountByDate(string param1, string param2, int param3, int param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientPositionReps.Validate_CashAmountByDate(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Cash Amount By Date", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GenerateAverageFCP(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientPositionReps.FundClientPosition_GenerateAverageFCP(param3));
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


        [HttpPost]
        public HttpResponseMessage UpdateUnitFundClientPosition(string param1, string param2, [FromBody]FundClientPosition _fundClientPosition)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        int _newHisPK = _FundClientPositionReps.Update_UnitFundClientPosition(_fundClientPosition);
                        //_activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Retrieve Success", _Obj, "", param1, _department.DepartmentPK, _department.HistoryPK, _newHisPK, "UPDATE");
                        return Request.CreateResponse(HttpStatusCode.OK, "Update Unit Success");
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage UpdateAvgAfterPosting(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientPositionReps.FundClientPosition_UpdateAvgAfterPosting(param3, param4));
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


        [HttpGet]
        public HttpResponseMessage SchedulerCheckingFCP()
        {
            string param1 = "Scheduler";
            DateTime _dateTimeNow = DateTime.Now;

            try
            {
                try
                {
                    string _attachment = "";
                    string _bodyMsg;

                    if (Tools.ClientCode == "10")
                    {
                        _bodyMsg = "Disclaimer: data yg keluar di excel hanya product2 yg ada kegagalan, dan juga product-product dari moinves";
                        _attachment = _CustomClient10Reps.SchedulerCheckingFCP(param1);
                    }
                    else
                    {
                        _bodyMsg = _FundClientPositionReps.SchedulerEmail();
                        _attachment = _FundClientPositionReps.SchedulerCheckingFCP(param1);
                    }



                    if (_bodyMsg != "")
                    {
                        SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                        dt = SendEmailReps.SchedulerEmail(param1, Tools._SchedulerEmail, "Report NAV BK " + _dateTimeNow.ToString("yyyy-MM-dd HH:mm tt"), _bodyMsg, _attachment, Tools._SchedulerEmailBcc);

                        return Request.CreateResponse(HttpStatusCode.OK, "Scheduler Checking Done");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "No Data");
                    }

                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        [HttpGet]
        public HttpResponseMessage UpdateAvgAfterRevise(string param1, string param2, DateTime param3, DateTime param4, int param5, int param6, int param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _FundClientPositionReps.FundClientPosition_UpdateAvgAfterRevise(param3, param4, param5, param6, param7));
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
