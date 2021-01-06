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
    public class RiskQuestionnaireAnswerController : ApiController
    {
        static readonly string _Obj = "Risk Questionnaire Answer Controller";
        static readonly RiskQuestionnaireAnswerReps _RiskQuestionnaireAnswerReps = new RiskQuestionnaireAnswerReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]RiskQuestionnaireAnswer _RiskQuestionnaireAnswer)
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
                            if (PermissionID == "RiskQuestionnaireAnswer_U")
                            {
                                int _newHisPK = _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Update(_RiskQuestionnaireAnswer, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Risk Questionnaire Answer Success", _Obj, "", param1, _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, _RiskQuestionnaireAnswer.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Risk Questionnaire Answer Success");
                            }
                            if (PermissionID == "RiskQuestionnaireAnswer_A")
                            {
                                _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Approved(_RiskQuestionnaireAnswer);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Risk Questionnaire Answer Success", _Obj, "", param1, _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, _RiskQuestionnaireAnswer.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Risk Questionnaire Answer Success");
                            }
                            if (PermissionID == "RiskQuestionnaireAnswer_V")
                            {
                                _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Void(_RiskQuestionnaireAnswer);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Risk Questionnaire Answer Success", _Obj, "", param1, _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, _RiskQuestionnaireAnswer.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Risk Questionnaire Answer Success");
                            }
                            if (PermissionID == "RiskQuestionnaireAnswer_R")
                            {
                                _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Reject(_RiskQuestionnaireAnswer);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Risk Questionnaire Answer Success", _Obj, "", param1, _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, _RiskQuestionnaireAnswer.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Risk Questionnaire Answer Success");
                            }
                            if (PermissionID == "RiskQuestionnaireAnswer_I")
                            {
                                int _lastPK = _RiskQuestionnaireAnswerReps.RiskQuestionnaireAnswer_Add(_RiskQuestionnaireAnswer, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Risk Questionnaire Answer Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Risk Questionnaire Answer Success");
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
        public HttpResponseMessage GetAnswerRiskQuestionnaire(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _RiskQuestionnaireAnswerReps.RiskQuestionnaire_selectAnswer(param3));
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
       */
        [HttpPost]
        public HttpResponseMessage InsertFromFundClient(string param1, string param2, [FromBody] List<FundClientRiskQuestionnaire> _FundClientRiskQuestionnaire)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _RiskQuestionnaireAnswerReps.InsertFromFundClient(_FundClientRiskQuestionnaire);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert into Fund Client Risk Quesionnaire Success");
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Insert into Fund Client Risk Quesionnaire Success", param1, 0, 0, 0, "");
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