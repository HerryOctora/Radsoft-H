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
    public class AccountingReportTemplateController : ApiController
    {
        static readonly string _Obj = "Accounting Report Template Controller";
        static readonly AccountingReportTemplateReps _AccountingReportTemplateReps = new AccountingReportTemplateReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = AccountingReportTemplatePK
       */
        [HttpGet]
        public HttpResponseMessage GetAccountingReportTemplateByPK(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _AccountingReportTemplateReps.AccountingReportTemplate_SelectByPK(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get AccountingReportTemplate By PK", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _AccountingReportTemplateReps.AccountingReportTemplate_Select(param3));
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
        [HttpGet]
        public HttpResponseMessage GetReportNameCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _AccountingReportTemplateReps.GetReportName_Combo());
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get ReportName Combo", param1, 0, 0, 0, "");
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
        [HttpGet]
        public HttpResponseMessage GetRecordFromCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _AccountingReportTemplateReps.GetRecordFrom_Combo());
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get RecordFrom Combo", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]AccountingReportTemplate _AccountingReportTemplate)
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
                            if (PermissionID == "AccountingReportTemplate_U")
                            {
                                bool dataExists = _AccountingReportTemplateReps.AccountingReportTemplate_CheckDataExists(_AccountingReportTemplate, "UPDATE");
                                if (dataExists)
                                {
                                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, "Data already exists!", _Obj, "Action", param1, 0, 0, 0, "");
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Data already exists!");
                                }
                                else
                                {
                                    int _newHisPK = _AccountingReportTemplateReps.AccountingReportTemplate_Update(_AccountingReportTemplate, havePrivillege);

                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update AccountingReportTemplate Success", _Obj, "", param1, _AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, _newHisPK, "UPDATE");
                                    return Request.CreateResponse(HttpStatusCode.OK, "Update AccountingReportTemplate Success");
                                }
                            }
                            if (PermissionID == "AccountingReportTemplate_CopyRecord")
                            {
                                string _msg = _AccountingReportTemplateReps.AccountingReportTemplate_CopyRecord(_AccountingReportTemplate);
                                if (_msg.Contains("Copy Record Success"))
                                {
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "CopyRecord AccountingReportTemplate Success", _Obj, "", param1, _AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, 0, "COPY");
                                    return Request.CreateResponse(HttpStatusCode.OK, "CopyRecord AccountingReportTemplate Success");
                                }
                                else
                                {
                                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, _msg, _Obj, "Action", param1, 0, 0, 0, "");
                                    return Request.CreateResponse(HttpStatusCode.InternalServerError, _msg);
                                }
                            }
                            if (PermissionID == "AccountingReportTemplate_A")
                            {
                                _AccountingReportTemplateReps.AccountingReportTemplate_Approved(_AccountingReportTemplate);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved AccountingReportTemplate Success", _Obj, "", param1, _AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved AccountingReportTemplate Success");
                            }
                            if (PermissionID == "AccountingReportTemplate_V")
                            {
                                _AccountingReportTemplateReps.AccountingReportTemplate_Void(_AccountingReportTemplate);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void AccountingReportTemplate Success", _Obj, "", param1, _AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void AccountingReportTemplate Success");
                            }
                            if (PermissionID == "AccountingReportTemplate_R")
                            {
                                _AccountingReportTemplateReps.AccountingReportTemplate_Reject(_AccountingReportTemplate);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject AccountingReportTemplate Success", _Obj, "", param1, _AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject AccountingReportTemplate Success");
                            }
                            if (PermissionID == "AccountingReportTemplate_I")
                            {
                                bool reportNameExists = _AccountingReportTemplateReps.AccountingReportTemplate_CheckReportNameExists(_AccountingReportTemplate);
                                if (reportNameExists)
                                {
                                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, "Report Name already exists!", _Obj, "Action", param1, 0, 0, 0, "");
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Report Name already exists!");
                                }
                                else
                                {
                                    bool dataExists = _AccountingReportTemplateReps.AccountingReportTemplate_CheckDataExists(_AccountingReportTemplate, "INSERT");
                                    if (dataExists)
                                    {
                                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, "Data already exists!", _Obj, "Action", param1, 0, 0, 0, "");
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Data already exists!");
                                    }
                                    else
                                    {
                                        int _lastPKByLastUpdate = _AccountingReportTemplateReps.AccountingReportTemplate_Add(_AccountingReportTemplate, havePrivillege);
                                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add AccountingReportTemplate Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                        return Request.CreateResponse(HttpStatusCode.OK, "Insert AccountingReportTemplate Success");
                                    }
                                }
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