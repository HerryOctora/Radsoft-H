﻿
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
    public class TemplateFundAccountingSetupController : ApiController
    {
        static readonly string _Obj = "Accounting Setup Controller";
        static readonly TemplateFundAccountingSetupReps _TemplateFundAccountingSetupReps = new TemplateFundAccountingSetupReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]TemplateFundAccountingSetup _TemplateFundAccountingSetup)
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
                            if (PermissionID == "TemplateFundAccountingSetup_U")
                            {
                                int _newHisPK = _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Update(_TemplateFundAccountingSetup, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Accounting Setup Success", _Obj, "", param1, _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, _TemplateFundAccountingSetup.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Accounting Setup Success");
                            }
                            if (PermissionID == "TemplateFundAccountingSetup_A")
                            {
                                _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Approved(_TemplateFundAccountingSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Accounting Setup Success", _Obj, "", param1, _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, _TemplateFundAccountingSetup.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Accounting Setup Success");
                            }
                            if (PermissionID == "TemplateFundAccountingSetup_V")
                            {
                                _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Void(_TemplateFundAccountingSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Accounting Setup Success", _Obj, "", param1, _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, _TemplateFundAccountingSetup.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Accounting Setup Success");
                            }
                            if (PermissionID == "TemplateFundAccountingSetup_R")
                            {
                                _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Reject(_TemplateFundAccountingSetup);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Accounting Setup Success", _Obj, "", param1, _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, _TemplateFundAccountingSetup.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Accounting Setup Success");
                            }
                            if (PermissionID == "TemplateFundAccountingSetup_I")
                            {
                                int _lastPKByLastUpdate = _TemplateFundAccountingSetupReps.TemplateFundAccountingSetup_Add(_TemplateFundAccountingSetup, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Accounting Setup Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Accounting Setup Success");
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
        public HttpResponseMessage CheckAlreadyHasExist(string param1, string param2)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _TemplateFundAccountingSetupReps.Get_CheckAlreadyHasExist());
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

    }
}
