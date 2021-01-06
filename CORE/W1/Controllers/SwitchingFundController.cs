using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.IO;
using System.Net.Http.Headers;


namespace W1.Controllers
{
    public class SwitchingFundController : ApiController
    {
        static readonly string _Obj = "Switching Fund Controller";
        static readonly SwitchingFundReps _switchingFundReps = new SwitchingFundReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = FundPK 
       */
        [HttpGet]
        public HttpResponseMessage GetFundRefByFundPK(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _switchingFundReps.Get_FundRefByFundPK(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Fund By FundPK", param1, 0, 0, 0, "");
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
         * param3 = status(pending = 1, approve = 2, history = 3, all = 9)
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
                        return Request.CreateResponse(HttpStatusCode.OK, _switchingFundReps.SwitchingFund_Select(param3));
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
        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]SwitchingFund _SwitchingFund)
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
                            if (PermissionID == "SwitchingFund_U")
                            {
                                int _newHisPK = _switchingFundReps.SwitchingFund_Update(_SwitchingFund, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Switching Fund Success", _Obj, "", param1, _SwitchingFund.SwitchingFundPK, _SwitchingFund.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Switching Fund Success");
                            }
                            if (PermissionID == "SwitchingFund_A")
                            {
                                _switchingFundReps.SwitchingFund_Approved(_SwitchingFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Switching Fund Success", _Obj, "", param1, _SwitchingFund.SwitchingFundPK, _SwitchingFund.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Switching Fund Success");
                            }
                            if (PermissionID == "SwitchingFund_V")
                            {
                                _switchingFundReps.SwitchingFund_Void(_SwitchingFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Switching Fund Success", _Obj, "", param1, _SwitchingFund.SwitchingFundPK, _SwitchingFund.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Switching Fund Success");
                            }
                            if (PermissionID == "SwitchingFund_R")
                            {
                                _switchingFundReps.SwitchingFund_Reject(_SwitchingFund);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Switching Fund Success", _Obj, "", param1, _SwitchingFund.SwitchingFundPK, _SwitchingFund.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Switching Fund Success");
                            }
                            if (PermissionID == "SwitchingFund_I")
                            {
                                int _lastPK = _switchingFundReps.SwitchingFund_Add(_SwitchingFund, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Switching Fund Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Switching Fund Success");
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
        public HttpResponseMessage GetCheckAlreadyHasApprovedSwitchingFund(string param1, string param2, int param3, int param4)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _switchingFundReps.Get_CheckAlreadyHasApprovedSwitchingFund(param3, param4));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }
    }
}
