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
    public class FundJournalScenarioController : ApiController
    {
        static readonly string _Obj = "Fund Journal Controller";
        static readonly FundJournalScenarioReps _FundJournalScenarioReps = new FundJournalScenarioReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FundJournalScenarioReps.FundJournalScenario_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundJournalScenario _FundJournalScenario)
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
                            if (PermissionID == "FundJournalScenario_U")
                            {
                                int _newHisPK = _FundJournalScenarioReps.FundJournalScenario_Update(_FundJournalScenario, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Fund Journal Success", _Obj, "", param1, _FundJournalScenario.FundJournalScenarioPK, _FundJournalScenario.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fund Journal Success");
                            }
                            if (PermissionID == "FundJournalScenario_A")
                            {
                                _FundJournalScenarioReps.FundJournalScenario_Approved(_FundJournalScenario);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Fund Journal Success", _Obj, "", param1, _FundJournalScenario.FundJournalScenarioPK, _FundJournalScenario.HistoryPK, 0, "APPROVED");

                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fund Journal Success");
                            }
                            if (PermissionID == "FundJournalScenario_V")
                            {
                                _FundJournalScenarioReps.FundJournalScenario_Void(_FundJournalScenario);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Fund Journal Success", _Obj, "", param1, _FundJournalScenario.FundJournalScenarioPK, _FundJournalScenario.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fund Journal Success");
                            }
                            if (PermissionID == "FundJournalScenario_R")
                            {
                                _FundJournalScenarioReps.FundJournalScenario_Reject(_FundJournalScenario);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Fund Journal Success", _Obj, "", param1, _FundJournalScenario.FundJournalScenarioPK, _FundJournalScenario.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fund Journal Success");
                            }
                            if (PermissionID == "FundJournalScenario_I")
                            {
                                FundJournalScenarioAddNew _fj = new FundJournalScenarioAddNew();
                                _fj = _FundJournalScenarioReps.FundJournalScenario_Add(_FundJournalScenario, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Journal Success", _Obj, "", param1, _fj.FundJournalScenarioPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, _fj);
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


        //[HttpPost]
      

    }
}
