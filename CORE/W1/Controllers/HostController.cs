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
    public class HostController : ApiController
    {
        static readonly string _Obj = "Host Controller";
        static readonly UsersReps _usersReps = new UsersReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
       * param1 = TableName
       * param2 = PK
       */
        [HttpGet]
        public HttpResponseMessage GetCompareData(string param1, string param2)
        {
            try
            {
                try
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _host.CompareData_Select(param1, param2));
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = Password
        * 
        */
        [HttpPost]
        public HttpResponseMessage ChangePassword(string param1, string param2, [FromBody]ChangePassword _changePassword)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                string PermissionID = "Users_ChangePassword";
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    havePermission = true;
                    if (havePermission == true)
                    {
                        try
                        {
                            string _msg;
                            _msg = _host.User_ChangePassword(param1, _changePassword.NewPassword);
                            _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, _msg, "", "", param1);
                            if (_msg != "True")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _msg);
                            }
                            else
                            {
                                _msg = "Success Change Password";
                                return Request.CreateResponse(HttpStatusCode.OK, _msg);
                            }
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, false, Tools.NoPermissionLogMessage, param1, param2, param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Change Password", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
        * param2 = Password 
        */
        [HttpGet]
        public HttpResponseMessage UnlockScreen(string param1, string param2)
        {
            try
            {
                if (param2 == _host.Get_UserPassword(param1))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _host.Get_UserSessionID(param1));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = SessionID
         * param3 = tableName
         * param4 = toggle
         * param5 = PK
         */
        [HttpGet]
        public HttpResponseMessage SelectDeselectData(string param1, string param2, string param3, bool param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (!_host.CheckOnlyOneWordInString(param3))
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectData(param3, param4, param5);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = SessionID
         * param3 = tableName
         * param4 = toggle
         */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllData(string param1, string param2, string param3, bool param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (!_host.CheckOnlyOneWordInString(param3))
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectAllData(param3, param4);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
      * param2 = SessionID
      * param3 = tableName
      * param4 = toggle
      * param5 = PK
      * param6 = type
      */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataInvestment(string param1, string param2, string param3, bool param4, int param5, string param6, string param7)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (_host.CheckOnlyOneWordInString(param3) == false || _host.CheckOnlyOneWordInString(param6) == false)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectDataInvestment(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Investment", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = datefrom
     * param6 = DateTo
     * param7 = type
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateInvestment(string param1, string param2, string param3, bool param4, DateTime param5, DateTime param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (_host.CheckOnlyOneWordInString(param3) == false)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectAllDataByDateInvestment(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Investment", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
       * param2 = SessionID
       * param3 = tableName
       * param4 = toggle
       * param5 = PK
       * param6 = type
       */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataCashier(string param1, string param2, string param3, bool param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (_host.CheckOnlyOneWordInString(param3) == false)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectDataCashier(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Cashier", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
        * param2 = SessionID
        * param3 = tableName
        * param4 = toggle
        * param5 = datefrom
        * param6 = DateTo
        */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDate(string param1, string param2, string param3, bool param4, DateTime param5, DateTime param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (_host.CheckOnlyOneWordInString(param3) == false)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectAllDataByDate(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
        * param2 = SessionID
        * param3 = tableName
        * param4 = toggle
        * param5 = datefrom
        * param6 = DateTo
        * param7 = type
        */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateCashier(string param1, string param2, string param3, bool param4, DateTime param5, DateTime param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    if (_host.CheckOnlyOneWordInString(param3) == false)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                    }
                    _host.SelectDeselectAllDataByDateCashier(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Cashier", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = SessionID 
         */
        [HttpGet]
        public HttpResponseMessage CheckChangePassword(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _host.CheckChangePassword(param1));
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Change Password", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }

            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = Initial Date 
         * param4 = total hari 
         */
        [HttpGet]
        public HttpResponseMessage GetNextWorkingDay(string param1, string param2, DateTime param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.GetWorkingDay(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Next Working Day", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = Initial Date 
       * param4 = total hari 
       */
        [HttpGet]
        public HttpResponseMessage CheckHoliday(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Check_Holiday(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Alert Holiday", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
         * param1 = userID
         */
        [HttpGet]
        public HttpResponseMessage GetServerDateTime(string param1)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.GetServerDateTime());
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = permissionID */
        [HttpGet]
        public HttpResponseMessage PermissionOpenCheck(string param1, string param2)
        {
            try
            {
                bool havePermission = _host.Get_Permission(param1, param2);
                if (havePermission)
                {
                    _activityReps.Activity_Insert(DateTime.Now, param2, true, "Open Form success", _Obj, "Permission Open Check", param1);
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, param2, false, Tools.NoPermissionLogMessage, param1, param2, param1);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = PrimaryKey
         * param3 = historyPK
         * param4 = tableName */
        [HttpGet]
        public HttpResponseMessage GetLastUpdate(string param1, int param2, long param3, string param4)
        {
            try
            {
                if (!_host.CheckOnlyOneWordInString(param4))
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.SQLInjectionMessage, "", "", param1);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.SQLInjectionMessage);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _host.Get_LastUpdate(param2, param3, param4));
                }

            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID */
        [HttpGet]
        public HttpResponseMessage GetLastTrxDateFromTrxClient(string param1)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_LastTrxDateFromTrxClient());
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
        * param1 = userID
        * param2 = SessionID
        */
        [HttpGet]
        public HttpResponseMessage GetIdleTimeMinutes(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Get_IdleTimeMinutes());
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Idle Time Minutes", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = PrimaryKey
         * param3 = historyPK
         * param4 = tableName */
        [HttpGet]
        public HttpResponseMessage CheckExistingID(string param1, string param2, string param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_CheckExistingID(param2, param3));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
        * param2 = PrimaryKey
        * param3 = historyPK
        * param4 = tableName */
        [HttpGet]
        public HttpResponseMessage CheckAlreadyHasApproved(string param1, string param2)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_CheckAlreadyHasApproved(param2));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
  * param2 = sessionID
  * param3 = reference
  */
        [HttpGet]
        public HttpResponseMessage GetUnitAmountByFundPKandFundClientPK(string param1, string param2, int param3, int param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Get_UnitAmountByFundPKandFundClientPK(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Unit Amount By FundPK and FundClientPK", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
   * param2 = SessionID
   * param3 = tableName
   * param4 = toggle
   * param5 = PK
   * param6 = type
   */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataEquity(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    _host.SelectDeselectDataEquity(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Equity", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = datefrom
     * param6 = DateTo
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateEquity(string param1, string param2, bool param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    _host.SelectDeselectAllDataEquity(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Equity", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
   * param2 = SessionID
   * param3 = tableName
   * param4 = toggle
   * param5 = PK
   * param6 = type
   */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataDealingEquity(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    _host.SelectDeselectDataDealingEquity(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Equity", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
* param2 = SessionID
* param3 = tableName
* param4 = toggle
* param5 = PK
* param6 = type
*/
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataDealingBond(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {

                    _host.SelectDeselectDataDealingBond(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Bond", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
* param2 = SessionID
* param3 = tableName
* param4 = toggle
* param5 = PK
* param6 = type
*/
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataDealingTimeDeposit(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataDealingTimeDeposit(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Time Deposit", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
      * param2 = SessionID
      * param3 = tableName
      * param4 = toggle
      * param5 = PK
      * param6 = type
      */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataSettlementEquity(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataSettlementEquity(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Equity", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = PK
     * param6 = type
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataSettlementBond(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataSettlementBond(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Bond", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
   * param2 = SessionID
   * param3 = tableName
   * param4 = toggle
   * param5 = PK
   * param6 = type
   */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataSettlementTimeDeposit(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataSettlementTimeDeposit(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data TimeDeposit", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
 * param2 = SessionID
 * param3 = tableName
 * param4 = toggle
 * param5 = PK
 * param6 = type
 */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataBond(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataBond(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Bond", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = datefrom
     * param6 = DateTo
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateBond(string param1, string param2, bool param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectAllDataBond(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Bond", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
* param2 = SessionID
* param3 = tableName
* param4 = toggle
* param5 = PK
* param6 = type
*/
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataTimeDeposit(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataTimeDeposit(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Time Deposit", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = datefrom
     * param6 = DateTo
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateTimeDeposit(string param1, string param2, bool param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectAllDataTimeDeposit(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Time Deposit", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /* param1 = userID
   * param2 = SessionID
   * param3 = tableName
   * param4 = toggle
   * param5 = PK
   * param6 = type
   */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataReksadana(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataReksadana(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Reksadana", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = SessionID
     * param3 = tableName
     * param4 = toggle
     * param5 = datefrom
     * param6 = DateTo
     */
        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateReksadana(string param1, string param2, bool param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectAllDataReksadana(param3, param4, param5, param6, param7);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Reksadana", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
   * param2 = SessionID
   * param3 = tableName
   * param4 = toggle
   * param5 = PK
   * param6 = type
   */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataDealingReksadana(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataDealingReksadana(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Reksadana", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
      * param2 = SessionID
      * param3 = tableName
      * param4 = toggle
      * param5 = PK
      * param6 = type
      */
        [HttpGet]
        public HttpResponseMessage SelectDeselectDataSettlementReksadana(string param1, string param2, bool param3, int param4, int param5, string param6)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.SelectDeselectDataSettlementReksadana(param3, param4, param5, param6);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Reksadana", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
* param2 = SessionID
* param3 = tableName
* param4 = toggle
* param5 = PK
* param6 = type
*/
        [HttpGet]
        public HttpResponseMessage ResetSelectedInvestment(string param1, string param2, string param3)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.Reset_SelectedInvestment(param3);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reset Selected Investment", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = parameter search
       */
        [HttpGet]
        public HttpResponseMessage GetNotification(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Get_Notification(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "GetNotification", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckAlreadyHasExist(string param1, string param2, string param3, string param4, string param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_CheckAlreadyHasExist(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = date
        */
        [HttpGet]
        public HttpResponseMessage GetDailyTransactionsByDate(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.GetDailyTransactionsByDate(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, param1, param2, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPositionProfilRisiko(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_PositionProfilRisiko(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetKebijakanInvestasi(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_KebijakanInvestasi(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetInformasiProduk(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_InformasiProduk(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetKomposisiSektor(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_KomposisiSektor(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetGrafikKinerja1(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_GrafikKinerja1(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetGrafikKinerja2(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_GrafikKinerja2(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetTableKinerja1(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_TableKinerja1(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* 
        * param1 = userID
        * param2 = sessionID
        * param3 = valueDate
        * param4 = periodPK
        * param5 = fundPK
        */
        [HttpGet]
        public HttpResponseMessage GetTableKinerja2(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _host.Get_TableKinerja2(param3, param4, param5));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage ResetSelected(string param1, string param2, string param3)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _host.Reset_Selected(param3);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reset Selected SAP", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        [HttpGet]
        public HttpResponseMessage PermissionOpenCheckForSettlement(string param1, string param2)
        {
            try
            {
                bool havePermission = _host.Get_Permission(param1, param2);
                if (havePermission)
                {
                    _activityReps.Activity_Insert(DateTime.Now, param2, true, "Open Form success", _Obj, "Permission Open Check", param1);
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, param2, false, Tools.NoPermissionLogMessage, param1, param2, param1);
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        [HttpGet]
        public HttpResponseMessage GetEndOfMonth(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.GetEndOfMonth(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Next Working Day", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
  * param1 = userID
  * param2 = sessionID
  * param3 = ID
  * param4 = Object
  */
        [HttpGet]
        public HttpResponseMessage GetPKByID(string param1, string param2, string param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Get_PKByID(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get PK By ID", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

    }
}