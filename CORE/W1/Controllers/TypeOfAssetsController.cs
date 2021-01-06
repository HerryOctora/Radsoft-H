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
    public class TypeOfAssetsController : ApiController
    {
        static readonly string _Obj = "TypeOfAssets Controller";
        static readonly TypeOfAssetsReps _TypeOfAssetsReps = new TypeOfAssetsReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _TypeOfAssetsReps.TypeOfAssets_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody] TypeOfAssets _TypeOfAssets)
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
                            if (PermissionID == "TypeOfAssets_U")
                            {
                                int _newHisPK = _TypeOfAssetsReps.TypeOfAssets_Update(_TypeOfAssets, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Type Of Assets Success", _Obj, "", param1, _TypeOfAssets.TypeOfAssetsPK, _TypeOfAssets.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Type Of Assets Success");
                            }
                            if (PermissionID == "TypeOfAssets_A")
                            {
                                _TypeOfAssetsReps.TypeOfAssets_Approved(_TypeOfAssets);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Type Of Assets Success", _Obj, "", param1, _TypeOfAssets.TypeOfAssetsPK, _TypeOfAssets.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Type Of Assets Success");
                            }
                            if (PermissionID == "TypeOfAssets_V")
                            {
                                _TypeOfAssetsReps.TypeOfAssets_Void(_TypeOfAssets);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Type Of Assets Success", _Obj, "", param1, _TypeOfAssets.TypeOfAssetsPK, _TypeOfAssets.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Type Of Assets Success");
                            }
                            if (PermissionID == "TypeOfAssets_R")
                            {
                                _TypeOfAssetsReps.TypeOfAssets_Reject(_TypeOfAssets);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Type Of Assets Success", _Obj, "", param1, _TypeOfAssets.TypeOfAssetsPK, _TypeOfAssets.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Type Of Assets Success");
                            }
                            if (PermissionID == "TypeOfAssets_I")
                            {
                                int _lastPKByLastUpdate = _TypeOfAssetsReps.TypeOfAssets_Add(_TypeOfAssets, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Type Of Assets Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Type Of Assets Success");
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
        public HttpResponseMessage GetInformationTypeOfAssets(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _TypeOfAssetsReps.Get_InformationTypeOfAssets(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Information By Type of Assets", param1, 0, 0, 0, "");
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
