
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
    public class TrxUnitPaymentTypeController : ApiController
    {
        static readonly string _Obj = "TrxUnitPaymentType Controller";
        static readonly TrxUnitPaymentTypeReps _trxUnitPaymentTypeReps = new TrxUnitPaymentTypeReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
      * param1 = userID
      * param2 = sessionID
      */
        [HttpGet]
        public HttpResponseMessage GetTrxUnitPaymentTypeCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _trxUnitPaymentTypeReps.TrxUnitPaymentType_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get TrxUnitPaymentType Combo", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _trxUnitPaymentTypeReps.TrxUnitPaymentType_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]TrxUnitPaymentType _TrxUnitPaymentType)
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
                            if (PermissionID == "TrxUnitPaymentType_U")
                            {
                                int _newHisPK = _trxUnitPaymentTypeReps.TrxUnitPaymentType_Update(_TrxUnitPaymentType, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update TrxUnitPaymentType Success", _Obj, "", param1, _TrxUnitPaymentType.TrxUnitPaymentTypePK, _TrxUnitPaymentType.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update TrxUnitPaymentType Success");
                            }
                            if (PermissionID == "TrxUnitPaymentType_A")
                            {
                                _trxUnitPaymentTypeReps.TrxUnitPaymentType_Approved(_TrxUnitPaymentType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved TrxUnitPaymentType Success", _Obj, "", param1, _TrxUnitPaymentType.TrxUnitPaymentTypePK, _TrxUnitPaymentType.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved TrxUnitPaymentType Success");
                            }
                            if (PermissionID == "TrxUnitPaymentType_V")
                            {
                                _trxUnitPaymentTypeReps.TrxUnitPaymentType_Void(_TrxUnitPaymentType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void TrxUnitPaymentType Success", _Obj, "", param1, _TrxUnitPaymentType.TrxUnitPaymentTypePK, _TrxUnitPaymentType.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void TrxUnitPaymentType Success");
                            }
                            if (PermissionID == "TrxUnitPaymentType_R")
                            {
                                _trxUnitPaymentTypeReps.TrxUnitPaymentType_Reject(_TrxUnitPaymentType);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject TrxUnitPaymentType Success", _Obj, "", param1, _TrxUnitPaymentType.TrxUnitPaymentTypePK, _TrxUnitPaymentType.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject TrxUnitPaymentType Success");
                            }
                            if (PermissionID == "TrxUnitPaymentType_I")
                            {
                                int _lastPK = _trxUnitPaymentTypeReps.TrxUnitPaymentType_Add(_TrxUnitPaymentType, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add TrxUnitPaymentType Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert TrxUnitPaymentType Success");
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