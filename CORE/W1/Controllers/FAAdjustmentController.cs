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
    public class FAAdjustmentController : ApiController
    {
        static readonly string _Obj = "FAAdjustment Controller";
        static readonly FAAdjustmentReps _FAAdjustmentReps = new FAAdjustmentReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = DateFrom
         * param4 = DateTo
         */
        [HttpGet]
        public HttpResponseMessage ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FAAdjustment_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _FAAdjustmentReps.FAAdjustment_ApproveBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approved By Selected Data", param1, 0, 0, 0, "");
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
      * param3 = DateFrom
      * param4 = DateTo
      */
        [HttpGet]
        public HttpResponseMessage RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FAAdjustment_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _FAAdjustmentReps.FAAdjustment_RejectBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reject By Selected Data", param1, 0, 0, 0, "");
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
   * param3 = DateFrom
   * param4 = DateTo
   */
        [HttpGet]
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FAAdjustment_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _FAAdjustmentReps.FAAdjustment_VoidBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Void All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Void By Selected Data", param1, 0, 0, 0, "");
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
         * param3 = DateFrom
         * param4 = DateTo
         */
        [HttpGet]
        public HttpResponseMessage PostingBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FAAdjustment_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _FAAdjustmentReps.FAAdjustment_PostingBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Posting By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FAAdjustmentReps.FAAdjustment_SelectFromTo(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data From To", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FAAdjustmentReps.FAAdjustment_Select(param3));
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
    * param3 = status(pending = 1, approve = 2, history = 3)
    */
        [HttpGet]
        public HttpResponseMessage GetReferenceCombo(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FAAdjustmentReps.Get_ReferenceCombo(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Reference Combo", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FAAdjustment _FAAdjustment)
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
                            if (PermissionID == "FAAdjustment_U")
                            {
                                int _newHisPK = _FAAdjustmentReps.FAAdjustment_Update(_FAAdjustment, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FAAdjustment Success");
                            }
                            if (PermissionID == "FAAdjustment_A")
                            {
                                _FAAdjustmentReps.FAAdjustment_Approved(_FAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "APPROVED");

                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FAAdjustment Success");
                            }
                            if (PermissionID == "FAAdjustment_V")
                            {
                                _FAAdjustmentReps.FAAdjustment_Void(_FAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FAAdjustment Success");
                            }
                            if (PermissionID == "FAAdjustment_R")
                            {
                                _FAAdjustmentReps.FAAdjustment_Reject(_FAAdjustment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FAAdjustment Success");
                            }
                            if (PermissionID == "FAAdjustment_I")
                            {
                                FAAdjustmentAddNew _fj = new FAAdjustmentAddNew();
                                _fj = _FAAdjustmentReps.FAAdjustment_Add(_FAAdjustment, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FAAdjustment Success", _Obj, "", param1, _fj.FAAdjustmentPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, _fj);
                            }
                            //if (PermissionID == "FAAdjustment_UnApproved")
                            //{
                            //    _FAAdjustmentReps.FAAdjustment_UnApproved(_FAAdjustment);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "APPROVED");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved FAAdjustment Success");
                            //}
                            //if (PermissionID == "FAAdjustment_Posting")
                            //{
                            //    _FAAdjustmentReps.FAAdjustment_Posting(_FAAdjustment);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "POSTING");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Posting FAAdjustment Success");
                            //}

                            //if (PermissionID == "FAAdjustment_Reversed")
                            //{
                            //    _FAAdjustmentReps.FAAdjustment_Reversed(_FAAdjustment);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reversed FAAdjustment Success", _Obj, "", param1, _FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, 0, "REVISE");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Reversed FAAdjustment Success");
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

        /*
             * param1 = userID
             * param2 = sessionID
             */

        [HttpGet]
        public HttpResponseMessage GenerateNavForecast(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FAAdjustmentReps.FAAdjustment_GenerateNavForecast(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get NAV Forecast", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GenerateTBForecast(string param1, string param2, DateTime param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FAAdjustmentReps.FAAdjustment_GenerateTBForecast(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get TB Forecast", param1, 0, 0, 0, "");
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
