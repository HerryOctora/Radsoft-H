
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
    public class RegulerInstructionController : ApiController
    {
        static readonly string _Obj = "Reguler Instruction Controller";
        static readonly RegulerInstructionReps _regulerInstructionReps = new RegulerInstructionReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetAutoDebitDateFromRegulerInstruction(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _regulerInstructionReps.Get_AutoDebitDateFromRegulerInstruction());
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
      * param3 = Generate Date
      */
        [HttpPost]
        public HttpResponseMessage GenerateDataRegulerInstructionToClientSubscription(string param1, string param2, [FromBody]RegulerInstruction _regulerInstruction)
        {
            string _msg = "";
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    string PermissionID = "RegulerInstruction_Generate";
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _msg =_regulerInstructionReps.Generate_DataRegulerInstructionToClientSubscription(_regulerInstruction);
                            //_activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Generate Reguler Instruction Success", _Obj, "", param1, 0, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, _msg);
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Generate", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Generate", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _regulerInstructionReps.RegulerInstruction_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]RegulerInstruction _regulerInstruction)
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
                            if (PermissionID == "RegulerInstruction_U")
                            {
                                int _newHisPK = _regulerInstructionReps.RegulerInstruction_Update(_regulerInstruction, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Reguler Instruction Success", _Obj, "", param1, _regulerInstruction.RegulerInstructionPK, _regulerInstruction.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Reguler Instruction Success");
                            }
                            if (PermissionID == "RegulerInstruction_A")
                            {
                                _regulerInstructionReps.RegulerInstruction_Approved(_regulerInstruction);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Reguler Instruction Success", _Obj, "", param1, _regulerInstruction.RegulerInstructionPK, _regulerInstruction.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Reguler Instruction Success");
                            }
                            if (PermissionID == "RegulerInstruction_V")
                            {
                                _regulerInstructionReps.RegulerInstruction_Void(_regulerInstruction);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Reguler Instruction Success", _Obj, "", param1, _regulerInstruction.RegulerInstructionPK, _regulerInstruction.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Reguler Instruction Success");
                            }
                            if (PermissionID == "RegulerInstruction_R")
                            {
                                _regulerInstructionReps.RegulerInstruction_Reject(_regulerInstruction);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Reguler Instruction Success", _Obj, "", param1, _regulerInstruction.RegulerInstructionPK, _regulerInstruction.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Reguler Instruction Success");
                            }
                            if (PermissionID == "RegulerInstruction_I")
                            {
                                int _lastPKByLastUpdate = _regulerInstructionReps.RegulerInstruction_Add(_regulerInstruction, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Reguler Instruction Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Reguler Instruction Success");
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
