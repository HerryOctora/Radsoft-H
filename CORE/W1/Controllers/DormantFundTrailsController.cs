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
    public class DormantFundTrailsController : ApiController
    {
        static readonly string _Obj = "Dormant Fund Trails Controller";
        static readonly DormantFundTrailsReps _DormantFundTrailsReps = new DormantFundTrailsReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _DormantFundTrailsReps.DormantFundTrails_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]DormantFundTrails _DormantFundTrails)
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
                            //if (PermissionID == "DormantFundTrails_U")
                            //{
                            //    int _newHisPK = _DormantFundTrailsReps.DormantFundTrails_Update(_DormantFundTrails, havePrivillege);

                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Dormant Fund Trails Success", _Obj, "", param1, _DormantFundTrails.DormantFundTrailsPK, _DormantFundTrails.HistoryPK, _newHisPK, "UPDATE");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Update Dormant Fund Trails Success");
                            //}
                            if (PermissionID == "DormantFundTrails_A")
                            {
                                _DormantFundTrailsReps.DormantFundTrails_Approved(_DormantFundTrails);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Dormant Fund Trails Success", _Obj, "", param1, _DormantFundTrails.DormantFundTrailsPK, _DormantFundTrails.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Dormant Fund Trails Success");
                            }
                            if (PermissionID == "DormantFundTrails_V")
                            {
                                _DormantFundTrailsReps.DormantFundTrails_Void(_DormantFundTrails);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Dormant Fund Trails Success", _Obj, "", param1, _DormantFundTrails.DormantFundTrailsPK, _DormantFundTrails.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Dormant Fund Trails Success");
                            }
                            if (PermissionID == "DormantFundTrails_R")
                            {
                                _DormantFundTrailsReps.DormantFundTrails_Reject(_DormantFundTrails);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Dormant Fund Trails Success", _Obj, "", param1, _DormantFundTrails.DormantFundTrailsPK, _DormantFundTrails.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Dormant Fund Trails Success");
                            }
                            //if (PermissionID == "DormantFundTrails_I")
                            //{
                            //    int _lastPKByLastUpdate = _DormantFundTrailsReps.DormantFundTrails_Add(_DormantFundTrails, havePrivillege);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Dormant Fund Trails Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Insert Dormant Fund Trails Success");
                            //}
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


        [HttpPost]
        public HttpResponseMessage GenerateDormant(string param1, string param2, DateTime param3, [FromBody]DormantFundTrails _dft)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    string PermissionID = "DormantFundTrails_Generate";
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            int _lastPK = 0;

                            _lastPK = _DormantFundTrailsReps.DormantFundTrails_GenerateDormant(param1, param3, _dft);
                            
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Generate Dormant Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, "Generate Dormant Success");
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

        [HttpPost]
        public HttpResponseMessage ActivateFund(string param1, string param2, DateTime param3, [FromBody]DormantFundTrails _dft)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    string PermissionID = "DormantFundTrails_Activate";
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            int _lastPK = 0;

                            _lastPK = _DormantFundTrailsReps.DormantFundTrails_ActivateFund(param1, param3, _dft);

                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Activate Fund Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, "Activate Fund  Success");
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
        */
        [HttpGet]
        public HttpResponseMessage GetActivateFundCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _DormantFundTrailsReps.ActivateFund_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Activate Fund Combo", param1, 0, 0, 0, "");
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