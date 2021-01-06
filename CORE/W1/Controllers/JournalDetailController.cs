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
    public class JournalDetailController : ApiController
    {
        static readonly string _Obj = "Journal Detail Controller";
        static readonly JournalDetailReps _journalDetailReps = new JournalDetailReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = status(pending = 1, approve = 2, history = 3)
         * param4 = JournalPK
         */
        [HttpGet]
        public HttpResponseMessage GetData(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _journalDetailReps.JournalDetail_Select(param3, param4));
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
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, ""  );
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
      * param1 = userID
      * param2 = sessionID
      * param3 = permissionID
      */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]JournalDetail _journalDetail)
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
                            if (PermissionID == "JournalDetail_U")
                            {
                                _journalDetailReps.JournalDetail_Update(_journalDetail);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Journal Detail Success", _Obj, "", param1, _journalDetail.JournalPK, _journalDetail.AutoNo, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Journal Detail Success");
                            }
                            if (PermissionID == "JournalDetail_I")
                            {
                                int _autoNo;
                                 _autoNo = _journalDetailReps.JournalDetail_Add(_journalDetail);
                                 _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Insert Journal Detail Success", _Obj, "", param1, _journalDetail.JournalPK, _autoNo, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Journal Detail Success");
                            }
                            if (PermissionID == "JournalDetail_D")
                            {
                                _journalDetailReps.JournalDetail_Delete(_journalDetail);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Delete Journal Detail Success", _Obj, "", param1, _journalDetail.JournalPK, _journalDetail.AutoNo, 0, "");
                                return Request.CreateResponse(HttpStatusCode.OK, "Delete Journal Detail Success");
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
