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
    public class CompanyAccountTradingController : ApiController
    {
        static readonly string _Obj = "Company Account Trading Controller";
        static readonly CompanyAccountTradingReps _CompanyAccountTradingReps = new CompanyAccountTradingReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
   * param1 = userID
   * param2 = sessionID
   */
        [HttpGet]
        public HttpResponseMessage GetCompanyAccountTradingCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _CompanyAccountTradingReps.CompanyAccountTrading_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get CompanyAccountTrading Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _CompanyAccountTradingReps.CompanyAccountTrading_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]CompanyAccountTrading _CompanyAccountTrading)
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
                            if (PermissionID == "CompanyAccountTrading_U")
                            {
                                int _newHisPK = _CompanyAccountTradingReps.CompanyAccountTrading_Update(_CompanyAccountTrading, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Company Account Trading Success", _Obj, "", param1, _CompanyAccountTrading.CompanyAccountTradingPK, _CompanyAccountTrading.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Company Account Trading Success");
                            }
                            if (PermissionID == "CompanyAccountTrading_A")
                            {
                                _CompanyAccountTradingReps.CompanyAccountTrading_Approved(_CompanyAccountTrading);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Company Account Trading Success", _Obj, "", param1, _CompanyAccountTrading.CompanyAccountTradingPK, _CompanyAccountTrading.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Company Account Trading Success");
                            }
                            if (PermissionID == "CompanyAccountTrading_V")
                            {
                                _CompanyAccountTradingReps.CompanyAccountTrading_Void(_CompanyAccountTrading);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Company Account Trading Success", _Obj, "", param1, _CompanyAccountTrading.CompanyAccountTradingPK, _CompanyAccountTrading.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Company Account Trading Success");
                            }
                            if (PermissionID == "CompanyAccountTrading_R")
                            {
                                _CompanyAccountTradingReps.CompanyAccountTrading_Reject(_CompanyAccountTrading);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Company Account Trading Success", _Obj, "", param1, _CompanyAccountTrading.CompanyAccountTradingPK, _CompanyAccountTrading.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Company Account Trading Success");
                            }
                            if (PermissionID == "CompanyAccountTrading_I")
                            {
                                int _lastPKByLastUpdate = _CompanyAccountTradingReps.CompanyAccountTrading_Add(_CompanyAccountTrading, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Company Account Trading Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Company Account Trading Success");
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