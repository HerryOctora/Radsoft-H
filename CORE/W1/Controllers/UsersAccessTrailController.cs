
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
    public class UsersAccessTrailController : ApiController
    {
        static readonly UsersAccessTrailReps _usersAccessTrailReps = new UsersAccessTrailReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();



        /*
         * param1 = userID
         * param2 = sessionID
         */
        [HttpGet]
        public HttpResponseMessage GetUserAccessTrailByID(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _usersAccessTrailReps.UsersAccessTrail_SelectByID(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.NoSessionLogMessage, param1, param2,"");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
        * param1 = userID
        * param2 = sessionID
        */
        [HttpGet]
        public HttpResponseMessage UsersAccessTrail_Logout(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _usersAccessTrailReps.UsersAccessTrail_Logout(param1));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.NoSessionLogMessage, param1, param2,"");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        ///*
        //* param1 = userID
        //* param2 = sessionID
        //* param3 = UsersAccessTrailPK
        //*/
        //[HttpGet]
        //public HttpResponseMessage GetOldData(string param1, string param2, int param3)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _usersAccessTrailReps.UsersAccessTrail_SelectByUsersAccessTrailPK(param3));
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


        ///*
        // * param1 = userID
        // * param2 = sessionID
        // * param3 = permissionID
        // */
        //[HttpPost]
        //public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]UsersAccessTrail _usersAccessTrail)
        //{
        //    string PermissionID;
        //    PermissionID = param3;
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                bool havePermission = _host.Get_Permission(param1, PermissionID);
        //                if (havePermission)
        //                {
        //                    bool havePrivillege = _host.Get_Privillege(param1, PermissionID);
        //                    if (PermissionID == "UsersAccessTrail_U")
        //                    {
        //                        _usersAccessTrailReps.UsersAccessTrail_Update(_usersAccessTrail, havePrivillege);
        //                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "Update UsersAccessTrail Success", "", "", param1);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Update UsersAccessTrail Success");
        //                    }
        //                    if (PermissionID == "UsersAccessTrail_A")
        //                    {
        //                        _usersAccessTrailReps.UsersAccessTrail_Approved(_usersAccessTrail);
        //                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "Approved UsersAccessTrail Success", "", "", param1);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Approved UsersAccessTrail Success");
        //                    }
        //                    if (PermissionID == "UsersAccessTrail_V")
        //                    {
        //                        _usersAccessTrailReps.UsersAccessTrail_Void(_usersAccessTrail);
        //                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "Void UsersAccessTrail Success", "", "", param1);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Void UsersAccessTrail Success");
        //                    }
        //                    if (PermissionID == "UsersAccessTrail_R")
        //                    {
        //                        _usersAccessTrailReps.UsersAccessTrail_Reject(_usersAccessTrail);
        //                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "Reject UsersAccessTrail Success", "", "", param1);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Reject UsersAccessTrail Success");
        //                    }
        //                    if (PermissionID == "UsersAccessTrail_I")
        //                    {
        //                        _usersAccessTrailReps.UsersAccessTrail_Add(_usersAccessTrail, havePrivillege);
        //                        _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "Add UsersAccessTrail Success", "", "", param1);
        //                        return Request.CreateResponse(HttpStatusCode.OK, "Insert UsersAccessTrail Success");
        //                    }
        //                    else
        //                    {
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoActionMessage);
        //                    }

        //                }
        //                else
        //                {
        //                    _activityReps.Activity_Insert(DateTime.Now, PermissionID, false, Tools.NoPermissionLogMessage, param1, param2, param1);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
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
        //        _activityReps.Activity_Insert(DateTime.Now,"", false, Tools.InternalErrorMessage + " : Action = " + PermissionID, err.Message, err.StackTrace, param1);
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}

    }
}
