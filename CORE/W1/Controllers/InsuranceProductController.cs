
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
    public class InsuranceProductController : ApiController
    {
        static readonly string _Obj = "InsuranceProduct Controller";
        static readonly InsuranceProductReps _InsuranceProductReps = new InsuranceProductReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _InsuranceProductReps.InsuranceProduct_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]InsuranceProduct _InsuranceProduct)
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
                            if (PermissionID == "InsuranceProduct_U")
                            {
                                int _newHisPK = _InsuranceProductReps.InsuranceProduct_Update(_InsuranceProduct, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update InsuranceProduct Success", _Obj, "", param1, _InsuranceProduct.InsuranceProductPK, _InsuranceProduct.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update InsuranceProduct Success");
                            }
                            if (PermissionID == "InsuranceProduct_A")
                            {
                                _InsuranceProductReps.InsuranceProduct_Approved(_InsuranceProduct);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved InsuranceProduct Success", _Obj, "", param1, _InsuranceProduct.FundClientPK, _InsuranceProduct.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved InsuranceProduct Success");
                            }
                            if (PermissionID == "InsuranceProduct_V")
                            {
                                _InsuranceProductReps.InsuranceProduct_Void(_InsuranceProduct);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void InsuranceProduct Success", _Obj, "", param1, _InsuranceProduct.InsuranceProductPK, _InsuranceProduct.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void InsuranceProduct Success");
                            }
                            if (PermissionID == "InsuranceProduct_R")
                            {
                                _InsuranceProductReps.InsuranceProduct_Reject(_InsuranceProduct);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject InsuranceProduct Success", _Obj, "", param1, _InsuranceProduct.InsuranceProductPK, _InsuranceProduct.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject InsuranceProduct Success");
                            }
                            if (PermissionID == "InsuranceProduct_I")
                            {
                                int _lastPKByLastUpdate = _InsuranceProductReps.InsuranceProduct_Add(_InsuranceProduct, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add InsuranceProduct Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert InsuranceProduct Success");
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