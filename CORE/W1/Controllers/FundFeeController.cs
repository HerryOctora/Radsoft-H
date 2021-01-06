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
    public class FundFeeController : ApiController
    {
        static readonly string _Obj = "Fund Fee Controller";
        static readonly FundFeeReps _FundFeeReps = new FundFeeReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();




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
                        return Request.CreateResponse(HttpStatusCode.OK, _FundFeeReps.FundFee_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundFee _FundFee)
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
                            if (PermissionID == "FundFee_U")
                            {
                                int _newHisPK = _FundFeeReps.FundFee_Update(_FundFee, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Fund Fee Success", _Obj, "", param1, _FundFee.FundFeePK, _FundFee.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_A")
                            {
                                _FundFeeReps.FundFee_Approved(_FundFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Fund Fee Success", _Obj, "", param1, _FundFee.FundFeePK, _FundFee.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_V")
                            {
                                _FundFeeReps.FundFee_Void(_FundFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Fund Fee Success", _Obj, "", param1, _FundFee.FundFeePK, _FundFee.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_R")
                            {
                                _FundFeeReps.FundFee_Reject(_FundFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Fund Fee Success", _Obj, "", param1, _FundFee.FundFeePK, _FundFee.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_I")
                            {
                                int _lastPK = _FundFeeReps.FundFee_Add(_FundFee, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Fee Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_AddFundFeeSetup")
                            {
                                int _lastPK = _FundFeeReps.AddFundFee(_FundFee, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Fee Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Fund Fee Success");
                            }
                            if (PermissionID == "FundFee_CopyFundFeeSetup")
                            {
                                int _lastPK = _FundFeeReps.CopyFundFee(_FundFee, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Copy Fund Fee Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Copy Fund Fee Success");
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
        public HttpResponseMessage GetDataFundFee(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FundFeeReps.FundFee_GetDataFundFee(param3));
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
        public HttpResponseMessage RejectedData(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        {
                            _FundFeeReps.RejectedDataFundFeeSetupBySelected(param1, param2, param3);
                            return Request.CreateResponse(HttpStatusCode.OK, "Reject Selected Data Success");
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



        [HttpPost]
        public HttpResponseMessage AddValidate(string param1, string param2, [FromBody]FundFee _FundFee)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FundFeeReps.Add_Validate(_FundFee));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Add", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckHasAddCopy(string param1, string param2, int param3, string param4)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _FundFeeReps.CheckHassAddCopy(param3, param4));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckMaxValue(string param1, string param2, [FromBody]CheckRangeTo _FundFee)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _FundFeeReps.CheckMaxValue(_FundFee));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


    }
}
