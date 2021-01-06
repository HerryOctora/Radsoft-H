
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
    public class MISCostCenterController : ApiController
    {
        static readonly string _Obj = "MISCostCenter Controller";
        static readonly MISCostCenterReps _MISCostCenterReps = new MISCostCenterReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
* param1 = userID
* param2 = sessionID
*/
        [HttpPost]
        public HttpResponseMessage MISCostCenterUpdateParentAndDepth(string param1, string param2)
        {
            string PermissionID;
            PermissionID = "Account_Restructure";
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (_host.Get_Permission(param1, PermissionID) == true)
                        {
                            if (_MISCostCenterReps.MISCostCenter_UpdateParentAndDepth() == true)
                            {
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "MISCostCenter Restructure Success", _Obj, "", param1, 0, 0, 0, "MISCostCenter RESTRUCTURE");
                                return Request.CreateResponse(HttpStatusCode.OK, " MISCostCenter Restructure Success ");
                            }
                            else
                            {
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "MISCostCenter Restructure Failed", _Obj, "", param1, 0, 0, 0, "MISCostCenter RESTRUCTURE");
                                return Request.CreateResponse(HttpStatusCode.OK, " MISCostCenter Restructure Failed ");
                            }
                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, Tools.NoPermissionLogMessage, _Obj, "MISCostCenter Restructure", param1, 0, 0, 0, "");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "MISCostCenter Restructure", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetMISCostCenterCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _MISCostCenterReps.MISCostCenter_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get MISCostCenter Combo", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetMISCostCenterComboGroupOnly(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _MISCostCenterReps.MISCostCenter_ComboGroupOnly());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get MISCostCenter Combo Group Only", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _MISCostCenterReps.MISCostCenter_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]MISCostCenter _MISCostCenter)
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
                            if (PermissionID == "MISCostCenter_U")
                            {
                                int _newHisPK = _MISCostCenterReps.MISCostCenter_Update(_MISCostCenter, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update MISCostCenter Success", _Obj, "", param1, _MISCostCenter.MISCostCenterPK, _MISCostCenter.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update MISCostCenter Success");
                            }
                            if (PermissionID == "MISCostCenter_A")
                            {
                                _MISCostCenterReps.MISCostCenter_Approved(_MISCostCenter);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved MISCostCenter Success", _Obj, "", param1, _MISCostCenter.MISCostCenterPK, _MISCostCenter.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved MISCostCenter Success");
                            }
                            if (PermissionID == "MISCostCenter_V")
                            {
                                _MISCostCenterReps.MISCostCenter_Void(_MISCostCenter);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void MISCostCenter Success", _Obj, "", param1, _MISCostCenter.MISCostCenterPK, _MISCostCenter.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void MISCostCenter Success");
                            }
                            if (PermissionID == "MISCostCenter_R")
                            {
                                _MISCostCenterReps.MISCostCenter_Reject(_MISCostCenter);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject MISCostCenter Success", _Obj, "", param1, _MISCostCenter.MISCostCenterPK, _MISCostCenter.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject MISCostCenter Success");
                            }
                            if (PermissionID == "MISCostCenter_I")
                            {
                                int _lastPKByLastUpdate = _MISCostCenterReps.MISCostCenter_Add(_MISCostCenter, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add MISCostCenter Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert MISCostCenter Success");
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