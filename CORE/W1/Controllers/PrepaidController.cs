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
    public class PrepaidController : ApiController
    {
        static readonly string _Obj = "Prepaid Controller";
        static readonly PrepaidReps _prepaidReps = new PrepaidReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();



        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetDataFromTo(string param1, string param2, int param3, string param4, DateTime param5, DateTime param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _prepaidReps.Prepaid_SelectFromTo(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approved By GetDataFrom", param1, 0, 0, 0, "");
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
         * param4 = Prepaid Type
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
                        return Request.CreateResponse(HttpStatusCode.OK, _prepaidReps.Prepaid_Select(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approved By GetData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Prepaid _prepaid)
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
                            if (PermissionID == "Prepaid_U")
                            {
                                int _newHisPK = _prepaidReps.Prepaid_Update(_prepaid, havePrivillege);
                                _prepaidReps.Prepaid_Update(_prepaid, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Prepaid Success");
                            }
                            if (PermissionID == "Prepaid_A")
                            {
                                _prepaidReps.Prepaid_Approved(_prepaid);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Prepaid Success");
                            }
                            if (PermissionID == "Prepaid_V")
                            {
                                _prepaidReps.Prepaid_Void(_prepaid);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Prepaid Success");
                            }
                            if (PermissionID == "Prepaid_R")
                            {
                                _prepaidReps.Prepaid_Reject(_prepaid);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Prepaid Success");
                            }
                            if (PermissionID == "Prepaid_I")
                            {
                                PrepaidAddNew _c = new PrepaidAddNew();
                                _c = _prepaidReps.Prepaid_Add(_prepaid, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Add Prepaid Success");
                            }
                            if (PermissionID == "Prepaid_UnApproved")
                            {
                                _prepaidReps.Prepaid_UnApproved(_prepaid);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "UNAPPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Prepaid Success");
                            }
                           

                            if (PermissionID == "Prepaid_Posting")
                            {

                                _prepaidReps.Prepaid_Posting(_prepaid);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting Prepaid Success");
                            }

                            if (PermissionID == "Prepaid_Revise")
                            {

                                _prepaidReps.Prepaid_Revise(_prepaid);


                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Revise Prepaid Success", _Obj, "", param1, _prepaid.PrepaidPK, _prepaid.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Prepaid Success");
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
