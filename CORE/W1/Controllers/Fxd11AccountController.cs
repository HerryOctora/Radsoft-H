
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
    public class Fxd11AccountController : ApiController
    {
        static readonly string _Obj = "Fxd11Account Controller";
        static readonly Fxd11AccountReps _Fxd11AccountReps = new Fxd11AccountReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
     * param1 = userID
     * param2 = sessionID
     */
        [HttpGet]
        public HttpResponseMessage GetFxd11AccountCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _Fxd11AccountReps.Fxd11Account_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Fxd11Account Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _Fxd11AccountReps.Fxd11Account_Select(param3));
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
         * param3 = Fxd11AccountID
         */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Fxd11Account _Fxd11Account)
        {
            string Fxd11AccountID;
            Fxd11AccountID = param3;
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool haveFxd11Account = _host.Get_Permission(param1, Fxd11AccountID);
                        if (haveFxd11Account)
                        {
                            bool havePrivillege = _host.Get_Privillege(param1, Fxd11AccountID);
                            if (Fxd11AccountID == "Fxd11Account_U")
                            {
                                int _newHisPK = _Fxd11AccountReps.Fxd11Account_Update(_Fxd11Account, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, Fxd11AccountID, true, "Update Fxd11Account Success", _Obj, "", param1, _Fxd11Account.Fxd11AccountPK, _Fxd11Account.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fxd11Account Success");
                            }
                            if (Fxd11AccountID == "Fxd11Account_A")
                            {
                                _Fxd11AccountReps.Fxd11Account_Approved(_Fxd11Account);
                                _activityReps.Activity_LogInsert(DateTime.Now, Fxd11AccountID, true, "Approved Fxd11Account Success", _Obj, "", param1, _Fxd11Account.Fxd11AccountPK, _Fxd11Account.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fxd11Account Success");
                            }
                            if (Fxd11AccountID == "Fxd11Account_V")
                            {
                                _Fxd11AccountReps.Fxd11Account_Void(_Fxd11Account);
                                _activityReps.Activity_LogInsert(DateTime.Now, Fxd11AccountID, true, "Void Fxd11Account Success", _Obj, "", param1, _Fxd11Account.Fxd11AccountPK, _Fxd11Account.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fxd11Account Success");
                            }
                            if (Fxd11AccountID == "Fxd11Account_R")
                            {
                                _Fxd11AccountReps.Fxd11Account_Reject(_Fxd11Account);
                                _activityReps.Activity_LogInsert(DateTime.Now, Fxd11AccountID, true, "Reject Fxd11Account Success", _Obj, "", param1, _Fxd11Account.Fxd11AccountPK, _Fxd11Account.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fxd11Account Success");
                            }
                            if (Fxd11AccountID == "Fxd11Account_I")
                            {
                                int _lastPK = _Fxd11AccountReps.Fxd11Account_Add(_Fxd11Account, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, Fxd11AccountID, true, "Add Fxd11Account Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Fxd11Account Success");
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