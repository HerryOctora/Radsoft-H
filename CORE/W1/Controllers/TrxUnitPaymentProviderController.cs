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
using RFSRepositoryTwo;
using RFSRepositoryThree;

namespace W1.Controllers
{
    public class TrxUnitPaymentProviderController : ApiController
    {
        static readonly string _Obj = "TrxUnitPaymentProvider Controller";
        static readonly TrxUnitPaymentProviderReps _TrxUnitPaymentProviderReps = new TrxUnitPaymentProviderReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
        * param1 = userID
        * param2 = sessionID
        */
        [HttpGet]
        public HttpResponseMessage GetTrxUnitPaymentProviderCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get TrxUnitPaymentProvider Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]TrxUnitPaymentProvider _TrxUnitPaymentProvider)
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
                            if (PermissionID == "TrxUnitPaymentProvider_U")
                            {
                                int _newHisPK = _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Update(_TrxUnitPaymentProvider, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update TrxUnitPaymentProvider Success", _Obj, "", param1, _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, _TrxUnitPaymentProvider.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update TrxUnitPaymentProvider Success");
                            }
                            if (PermissionID == "TrxUnitPaymentProvider_A")
                            {
                                _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Approved(_TrxUnitPaymentProvider);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved TrxUnitPaymentProvider Success", _Obj, "", param1, _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, _TrxUnitPaymentProvider.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved TrxUnitPaymentProvider Success");
                            }
                            if (PermissionID == "TrxUnitPaymentProvider_V")
                            {
                                _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Void(_TrxUnitPaymentProvider);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void TrxUnitPaymentProvider Success", _Obj, "", param1, _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, _TrxUnitPaymentProvider.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void TrxUnitPaymentProvider Success");
                            }
                            if (PermissionID == "TrxUnitPaymentProvider_R")
                            {
                                _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Reject(_TrxUnitPaymentProvider);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject TrxUnitPaymentProvider Success", _Obj, "", param1, _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, _TrxUnitPaymentProvider.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject TrxUnitPaymentProvider Success");
                            }
                            if (PermissionID == "TrxUnitPaymentProvider_I")
                            {
                                int _lastPK = _TrxUnitPaymentProviderReps.TrxUnitPaymentProvider_Add(_TrxUnitPaymentProvider, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add TrxUnitPaymentProvider Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert TrxUnitPaymentProvider Success");
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