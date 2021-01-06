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
    public class LoginController : ApiController
    {

        static readonly string _Obj = "Fund Client Controller";

        static readonly LoginReps _loginReps = new LoginReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        [HttpGet]
        public HttpResponseMessage CheckID(string param1, string param2, string param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _loginReps.CheckID(param3));
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
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage SendMailByInput([FromBody]Login _changePassword)
        {
            try
            {
                try
                {

                    _loginReps.SendMail(_changePassword);
                    return Request.CreateResponse(HttpStatusCode.OK, "Send Email Success");


                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, _changePassword.UsersID, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = Password
        * 
        */
        [HttpGet]
        public HttpResponseMessage ChangePassword(string param1)
        {
            try
            {
                try
                {
                    if (_loginReps.ChangePassword(param1) == "Success Update")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Reset Password Success, Please Check Your E-mail For The New Password");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "ID/Email Not Register");
                    }
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); } }
            }
        }

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage SendMailResetPassword([FromBody]Login _changePassword)
        {
            try
            {
                try
                {

                    _loginReps.SendMail(_changePassword);
                    return Request.CreateResponse(HttpStatusCode.OK, "Send Email Success");

                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, _changePassword.UsersID, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



        [HttpPost]
        public HttpResponseMessage CheckEmail([FromBody]Login _changePassword)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _loginReps.CheckEmail(_changePassword));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, _changePassword.UsersID);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


    }
}