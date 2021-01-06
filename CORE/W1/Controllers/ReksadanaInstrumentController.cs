
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
    public class ReksadanaInstrumentController : ApiController
    {
        static readonly string _Obj = "ReksadanaInstrument Controller";
        static readonly ReksadanaInstrumentReps _ReksadanaInstrumentReps = new ReksadanaInstrumentReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _ReksadanaInstrumentReps.ReksadanaInstrument_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]ReksadanaInstrument _ReksadanaInstrument)
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
                            if (PermissionID == "ReksadanaInstrument_U")
                            {
                                int _newHisPK = _ReksadanaInstrumentReps.ReksadanaInstrument_Update(_ReksadanaInstrument, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Reksadana Instrument Success", _Obj, "", param1, _ReksadanaInstrument.ReksadanaInstrumentPK, _ReksadanaInstrument.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Reksadana Instrument Success");
                            }
                            if (PermissionID == "ReksadanaInstrument_A")
                            {
                                _ReksadanaInstrumentReps.ReksadanaInstrument_Approved(_ReksadanaInstrument);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Reksadana Instrument Success", _Obj, "", param1, _ReksadanaInstrument.ReksadanaInstrumentPK, _ReksadanaInstrument.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Reksadana Instrument Success");
                            }
                            if (PermissionID == "ReksadanaInstrument_V")
                            {
                                _ReksadanaInstrumentReps.ReksadanaInstrument_Void(_ReksadanaInstrument);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Reksadana Instrument Success", _Obj, "", param1, _ReksadanaInstrument.ReksadanaInstrumentPK, _ReksadanaInstrument.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void ReksadanaInstrument Success");
                            }
                            if (PermissionID == "ReksadanaInstrument_R")
                            {
                                _ReksadanaInstrumentReps.ReksadanaInstrument_Reject(_ReksadanaInstrument);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Reksadana Instrument Success", _Obj, "", param1, _ReksadanaInstrument.ReksadanaInstrumentPK, _ReksadanaInstrument.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Reksadana Instrument Success");
                            }
                            if (PermissionID == "ReksadanaInstrument_I")
                            {
                                int _lastPKByLastUpdate = _ReksadanaInstrumentReps.ReksadanaInstrument_Add(_ReksadanaInstrument, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Reksadana Instrument Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Reksadana Instrument Success");
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
