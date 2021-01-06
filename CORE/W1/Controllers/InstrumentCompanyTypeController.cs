
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;
using RFSRepositoryOne;

namespace W1.Controllers
{
    public class InstrumentCompanyTypeController : ApiController
    {
        static readonly string _Obj = "InstrumentCompanyType Controller";
        static readonly InstrumentCompanyTypeReps _InstrumentCompanyTypeReps = new InstrumentCompanyTypeReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = status(pending = 1, approve = 2, history = 3)
        */
        [HttpGet]
        public HttpResponseMessage GetInstrumentCompanyTypeCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _InstrumentCompanyTypeReps.InstrumentCompanyType_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get InstrumentCompanyType Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _InstrumentCompanyTypeReps.InstrumentCompanyType_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]InstrumentCompanyType _InstrumentCompanyType)
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
                            if (PermissionID == "InstrumentCompanyType_U")
                            {
                                int _newHisPK = _InstrumentCompanyTypeReps.InstrumentCompanyType_Update(_InstrumentCompanyType, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update InstrumentCompanyType Success", _Obj, "", param1, _InstrumentCompanyType.InstrumentCompanyTypePK, _InstrumentCompanyType.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update InstrumentCompanyType Success");
                            }
                            if (PermissionID == "InstrumentCompanyType_A")
                            {
                                _InstrumentCompanyTypeReps.InstrumentCompanyType_Approved(_InstrumentCompanyType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved InstrumentCompanyType Success", _Obj, "", param1, _InstrumentCompanyType.InstrumentCompanyTypePK, _InstrumentCompanyType.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved InstrumentCompanyType Success");
                            }
                            if (PermissionID == "InstrumentCompanyType_V")
                            {
                                _InstrumentCompanyTypeReps.InstrumentCompanyType_Void(_InstrumentCompanyType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void InstrumentCompanyType Success", _Obj, "", param1, _InstrumentCompanyType.InstrumentCompanyTypePK, _InstrumentCompanyType.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void InstrumentCompanyType Success");
                            }
                            if (PermissionID == "InstrumentCompanyType_R")
                            {
                                _InstrumentCompanyTypeReps.InstrumentCompanyType_Reject(_InstrumentCompanyType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject InstrumentCompanyType Success", _Obj, "", param1, _InstrumentCompanyType.InstrumentCompanyTypePK, _InstrumentCompanyType.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject InstrumentCompanyType Success");
                            }
                            if (PermissionID == "InstrumentCompanyType_I")
                            {
                                int _lastPKByLastUpdate = _InstrumentCompanyTypeReps.InstrumentCompanyType_Add(_InstrumentCompanyType, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add InstrumentCompanyType Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert InstrumentCompanyType Success");
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