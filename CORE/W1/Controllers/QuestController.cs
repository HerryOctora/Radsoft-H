using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.Web;

namespace W1.Controllers
{
    public class QuestController : ApiController
    {
        static readonly string _Obj = "Quest Controller";
        static readonly UsersReps _usersReps = new UsersReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly UsersAccessTrailReps _usersAccessTrail = new UsersAccessTrailReps();
        static readonly Host _host = new Host();

        /* param1 = userid
         * param2 = password
         * param3 = sessionid 
         * param4 = IpAddress
         */
        [HttpGet]
        public HttpResponseMessage LoginCheck_Old12052017(string param1, string param2, string param3, string param4)
        {
            const string PermissionID = "Login_Check";
            try
            {
                Users mUsers = new Users();
                mUsers = _usersReps.Users_SelectByUserID(param1);

                if (mUsers == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed");
                }
                else
                {
                    if (Convert.ToDateTime(mUsers.ExpireUsersDate) <= DateTime.Now)
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - User Expired", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - User Expired");
                    }

                    if (mUsers.BitEnabled == false)
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - User Disabled", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        _usersReps.Users_IncrementLoginRetry(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - User Disabled");
                    }

                    //// LOGIN SUKSES
                    if (mUsers.Password == Cipher.Encrypt(param2))
                    {
                        _usersReps.Users_UpdateSessionID(param3, param1, param4);
                        _usersAccessTrail.UsersAccessTrail_LoginSuccess(param1);
                        _usersReps.Users_ResetLoginRetry(param1);
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Login Check", "Quest Controller", param3, param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }

                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - Wrong Password", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        _usersReps.Users_IncrementLoginRetry(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - Wrong Password");
                    }
                }
            }
            catch (Exception err)
            {
                // Log Aktifitas yang error

                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace,err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userid
         * param2 = password
         * param3 = sessionid 
         * param4 = IpAddress
         */
        [HttpPost]
        public HttpResponseMessage LoginCheck([FromBody]Quest _quest)
        {
            string PermissionID = "Login_Check";
            string param1 = _quest.UsersID;
            string param2 = _quest.Password;
            string param3 = _quest.SessionID;
            string param4 = _quest.IpAddress;

            try
            {
                Users mUsers = new Users();
                mUsers = _usersReps.Users_SelectByUserID(param1);

                if (mUsers == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed");
                }
                else
                {
                    if (Convert.ToDateTime(mUsers.ExpireUsersDate) <= DateTime.Now)
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - User Expired", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - User Expired");
                    }

                    if (mUsers.BitEnabled == false)
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - User Disabled", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        _usersReps.Users_IncrementLoginRetry(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - User Disabled");
                    }

                    //// LOGIN SUKSES
                    //string _passwordDbase = Cipher.Decrypt(mUsers.Password);
                    //string _passwordLogin = param2;

                    if (mUsers.Password == Cipher.Encrypt(param2))
                    {
                        _usersReps.Users_UpdateSessionID(param3, param1, param4);
                        _usersAccessTrail.UsersAccessTrail_LoginSuccess(param1);
                        _usersReps.Users_ResetLoginRetry(param1);
                        _usersReps.Users_UpdateExpireSessionTime(param1);
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Login Check", "Quest Controller", param3, param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }

                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, "Login Failed - Wrong Password", _Obj, param3, param1, 0, 0, 0, "");
                        _usersAccessTrail.UsersAccessTrail_LoginFailed(param1);
                        _usersReps.Users_IncrementLoginRetry(param1);
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Login Failed - Wrong Password");
                    }
                }
            }
            catch (Exception err)
            {
                // Log Aktifitas yang error

                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param 1 = userID
         * param 2 = sessionID */
        [HttpGet]
        public HttpResponseMessage SessionCheck(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, false);
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