
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
    public class BankInterestSetupController : ApiController
    {
        static readonly string _Obj = "BankInterestSetup Controller";
        static readonly BankInterestSetupReps _BankInterestSetupReps = new BankInterestSetupReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


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
                        return Request.CreateResponse(HttpStatusCode.OK, _BankInterestSetupReps.BankInterestSetup_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BankInterestSetup _BankInterestSetup)
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
                            if (PermissionID == "BankInterestSetup_U")
                            {
                                int _newHisPK = _BankInterestSetupReps.BankInterestSetup_Update(_BankInterestSetup, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update BankInterestSetup Custodian Success", _Obj, "", param1, _BankInterestSetup.BankInterestSetupPK, _BankInterestSetup.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Bank Interest Setup Success");
                            }
                            if (PermissionID == "BankInterestSetup_A")
                            {
                                _BankInterestSetupReps.BankInterestSetup_Approved(_BankInterestSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved BankInterestSetup Custodian Success", _Obj, "", param1, _BankInterestSetup.BankInterestSetupPK, _BankInterestSetup.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Bank Interest Setup Success");
                            }
                            if (PermissionID == "BankInterestSetup_V")
                            {
                                _BankInterestSetupReps.BankInterestSetup_Void(_BankInterestSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void BankInterestSetup Custodian Success", _Obj, "", param1, _BankInterestSetup.BankInterestSetupPK, _BankInterestSetup.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Bank Interest Setup Success");
                            }
                            if (PermissionID == "BankInterestSetup_R")
                            {
                                _BankInterestSetupReps.BankInterestSetup_Reject(_BankInterestSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject BankInterestSetup Custodian Success", _Obj, "", param1, _BankInterestSetup.BankInterestSetupPK, _BankInterestSetup.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Bank Interest Setup Success");
                            }
                            if (PermissionID == "BankInterestSetup_I")
                            {
                                int _lastPKByLastUpdate = _BankInterestSetupReps.BankInterestSetup_Add(_BankInterestSetup, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add BankInterestSetup Custodian Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Bank Interest Setup Success");
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
