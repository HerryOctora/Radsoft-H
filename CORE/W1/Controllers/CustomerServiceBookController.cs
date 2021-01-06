﻿
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
    public class CustomerServiceBookController : ApiController
    {
        static readonly string _Obj = "CustomerServiceBook Controller";
        static readonly CustomerServiceBookReps _CustomerServiceBookReps = new CustomerServiceBookReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

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
                        return Request.CreateResponse(HttpStatusCode.OK, _CustomerServiceBookReps.CustomerServiceBook_Select(param3));
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

        [HttpGet]
        public HttpResponseMessage GetDataCombo(string param1, string param2, int param3,int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _CustomerServiceBookReps.CustomerServiceBook_GetCustomerCombo(param3, param4));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]CustomerServiceBook _CustomerServiceBook)
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
                            if (PermissionID == "CustomerServiceBook_U")
                            {
                                int _newHisPK = _CustomerServiceBookReps.CustomerServiceBook_Update(_CustomerServiceBook, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Customer Service Book Success", _Obj, "", param1, _CustomerServiceBook.CustomerServiceBookPK, _CustomerServiceBook.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Customer Service Book  Success");
                            }
                            if (PermissionID == "CustomerServiceBook_A")
                            {
                                _CustomerServiceBookReps.CustomerServiceBook_Approved(_CustomerServiceBook);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Customer Service Book Success", _Obj, "", param1, _CustomerServiceBook.CustomerServiceBookPK, _CustomerServiceBook.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Customer Service Book Success");
                            }
                            if (PermissionID == "CustomerServiceBook_V")
                            {
                                _CustomerServiceBookReps.CustomerServiceBook_Void(_CustomerServiceBook);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Customer Service Book Success", _Obj, "", param1, _CustomerServiceBook.CustomerServiceBookPK, _CustomerServiceBook.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Customer Service Book Success");
                            }
                            if (PermissionID == "CustomerServiceBook_R")
                            {
                                _CustomerServiceBookReps.CustomerServiceBook_Reject(_CustomerServiceBook);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Customer Service Book Success", _Obj, "", param1, _CustomerServiceBook.CustomerServiceBookPK, _CustomerServiceBook.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Customer Service Book Success");
                            }
                            if (PermissionID == "CustomerServiceBook_I")
                            {
                                int _lastPKByLastUpdate = _CustomerServiceBookReps.CustomerServiceBook_Add(_CustomerServiceBook, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Customer Service Book Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Customer Service Book Success");
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


        [HttpPost]
        public HttpResponseMessage CheckDone(string param1, string param2, [FromBody]CustomerServiceBook _CustomerServiceBook)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        {
                            _CustomerServiceBookReps.CustomerServiceBook_CheckDone(_CustomerServiceBook);
                            if (_CustomerServiceBook.Param==2)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, "Customer Service Book Done");
                            }
                            else if (_CustomerServiceBook.Param == 3)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, "Customer Service Book Undone");
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, "Customer Service Book Done");
                            
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
