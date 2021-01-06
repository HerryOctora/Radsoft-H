
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
    public class AdvisoryFeeController : ApiController
    {
        static readonly string _Obj = "AdvisoryFee Controller";
        static readonly AdvisoryFeeReps _AdvisoryFeeReps = new AdvisoryFeeReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        ///*
        // * param1 = userID
        // * param2 = sessionID
        // * param3 = status(pending = 1, approve = 2, history = 3)
        // */
        //[HttpGet]
        //public HttpResponseMessage GetGroupsCombo(string param1, string param2)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _groupsReps.Groups_Combo());
        //            }
        //            catch (Exception err)
        //            {

        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Groups Combo", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}


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
                        return Request.CreateResponse(HttpStatusCode.OK, _AdvisoryFeeReps.AdvisoryFee_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]AdvisoryFee _AdvisoryFee)
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
                            if (PermissionID == "AdvisoryFee_U")
                            {
                                int _newHisPK = _AdvisoryFeeReps.AdvisoryFee_Update(_AdvisoryFee, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update AdvisoryFee Success", _Obj, "", param1, _AdvisoryFee.AdvisoryFeePK, _AdvisoryFee.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update AdvisoryFee Success");
                            }
                            if (PermissionID == "AdvisoryFee_A")
                            {
                                _AdvisoryFeeReps.AdvisoryFee_Approved(_AdvisoryFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved AdvisoryFee Success", _Obj, "", param1, _AdvisoryFee.AdvisoryFeePK, _AdvisoryFee.HistoryPK, 0, "APPROVED");

                                return Request.CreateResponse(HttpStatusCode.OK, "Approved AdvisoryFee Success");
                            }
                            if (PermissionID == "AdvisoryFee_V")
                            {
                                _AdvisoryFeeReps.AdvisoryFee_Void(_AdvisoryFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void AdvisoryFee Success", _Obj, "", param1, _AdvisoryFee.AdvisoryFeePK, _AdvisoryFee.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void AdvisoryFee Success");
                            }
                            if (PermissionID == "AdvisoryFee_R")
                            {
                                _AdvisoryFeeReps.AdvisoryFee_Reject(_AdvisoryFee);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject AdvisoryFee Success", _Obj, "", param1, _AdvisoryFee.AdvisoryFeePK, _AdvisoryFee.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject AdvisoryFee Success");
                            }
                            if (PermissionID == "AdvisoryFee_I")
                            {
                                AdvisoryFeeAddNew _fj = new AdvisoryFeeAddNew();
                                _fj = _AdvisoryFeeReps.AdvisoryFee_Add(_AdvisoryFee, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add AdvisoryFee Success", _Obj, "", param1, _fj.AdvisoryFeePK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, _fj);
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
        public HttpResponseMessage CheckAdvisoryFeeStatus(string param1, string param2, int param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _AdvisoryFeeReps.CheckAdvisoryFeeStatus(param3));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



      


    }
}