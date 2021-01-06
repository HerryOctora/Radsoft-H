using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.IO;
using System.Net.Http.Headers;

namespace W1.Controllers
{
    public class UsersController : ApiController
    {
        static readonly string _Obj = "Users Controller";
        static readonly UsersReps _usersReps = new UsersReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = PermissionID
        * param4 = userID user yang di Reset
        */
        [HttpGet]
        public HttpResponseMessage Users_Reset(string param1, string param2, string param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, param3);
                    if (havePermission == true)
                    {
                        try
                        {
                            _usersReps.Users_Reset(param4,param1);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Reset User Success", _Obj, "", param1, 0, 0, 0, "RESET USER");
                            return Request.CreateResponse(HttpStatusCode.OK, "Reset User Success");
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, param3, false, Tools.NoPermissionLogMessage, _Obj, "Reset User", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reset User", param1, 0, 0, 0, "");
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
     * param3 = PermissionID
     * param4 = userID user yang di Enable
     */
        [HttpGet]
        public HttpResponseMessage Users_Enable(string param1, string param2, string param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, param3);
                    if (havePermission == true)
                    {
                        try
                        {
                            _usersReps.Users_Enable(param4);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Enable User Success", _Obj, "", param1, 0, 0, 0, "ENABLE USER");
                            return Request.CreateResponse(HttpStatusCode.OK, "Enable User Success");
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, param3, false, Tools.NoPermissionLogMessage, _Obj, "Enable User", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Enable User", param1, 0, 0, 0, "");
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
  * param3 = PermissionID
  * param4 = userID user yang di Disable
  */
        [HttpGet]
        public HttpResponseMessage Users_Disable(string param1, string param2, string param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, param3);
                    if (havePermission == true)
                    {
                        try
                        {
                            _usersReps.Users_Disable(param4);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Disable User Success", _Obj, "", param1, 0, 0, 0, "DISABLE USER");
                            return Request.CreateResponse(HttpStatusCode.OK, "Disable User Success");
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, param3, false, Tools.NoPermissionLogMessage, _Obj, "Disable User", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Disable User", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetUsersCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _usersReps.Users_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Users Combo", param1, 0, 0, 0, "");
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
       * param3 = status(pending = 1, approve = 2, history = 3, all = 9)
       */
        //[HttpGet]
        //public HttpResponseMessage DownloadData(string param1, string param2)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                if (_usersReps.Listing(param1))
        //                {
        //                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "Users_" + param1 + ".xlsx");
        //                }
        //                else
        //                {
        //                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
        //                }

        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.NoSessionLogMessage, param1, param2,"");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}


        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3, all = 9)
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
                        return Request.CreateResponse(HttpStatusCode.OK, _usersReps.Users_Select(param3));
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

        [HttpPost]
        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Users _users)
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
                            if (PermissionID == "Users_U")
                            {
                                int _newHisPK = _usersReps.Users_Update(_users, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Form users Success", _Obj, "", param1, _users.UsersPK, _users.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Users Success");
                            }
                            if (PermissionID == "Users_A")
                            {
                                _usersReps.Users_Approved(_users);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Form users Success", _Obj, "", param1, _users.UsersPK, _users.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Users Success");
                            }
                            if (PermissionID == "Users_V")
                            {
                                _usersReps.Users_Void(_users);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Form users Success", _Obj, "", param1, _users.UsersPK, _users.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Users Success");
                            }
                            if (PermissionID == "Users_R")
                            {
                                _usersReps.Users_Reject(_users);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Form users Success", _Obj, "", param1, _users.UsersPK, _users.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Users Success");
                            }
                            if (PermissionID == "Users_I")
                            {
                                int _lastPKByLastUpdate = _usersReps.Users_Add(_users, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Form users Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Users Success");
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
        public HttpResponseMessage GetUsersClientMode(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _usersReps.Get_UsersClientMode());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Users Client Mode", param1, 0, 0, 0, "");
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
      * param3 = status(pending = 1, approve = 2, history = 3, all = 9)
      */
        [HttpGet]
        public HttpResponseMessage GetDataByUsersID(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _usersReps.Users_SelectByUserID(param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data User By usersID", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckExistingEmail(string param1, string param2, int param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usersReps.Get_CheckExistingEmail(param2, param3));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



    }
}
