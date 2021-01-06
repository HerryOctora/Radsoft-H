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
using RFSRepositoryTwo;
using RFSRepositoryThree;

namespace W1.Controllers
{
    public class DividenDirectInvestmentController : ApiController
    {
        static readonly string _Obj = "Advisory Fee Client Controller";
        static readonly DividenDirectInvestmentReps _DividenDirectInvestmentReps = new DividenDirectInvestmentReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _DividenDirectInvestmentReps.DividenDirectInvestment_Select(param3, param4));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]DividenDirectInvestment _DividenDirectInvestment)
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
                            if (PermissionID == "DividenDirectInvestment_U")
                            {
                                _DividenDirectInvestmentReps.DividenDirectInvestment_Update(_DividenDirectInvestment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Journal Detail Success", _Obj, "", param1, _DividenDirectInvestment.DirectInvestmentPK, _DividenDirectInvestment.DividenDirectInvestmentPK, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Journal Detail Success");
                            }
                            if (PermissionID == "DividenDirectInvestment_I")
                            {
                                int _DividenDirectInvestmentPK;
                                _DividenDirectInvestmentPK = _DividenDirectInvestmentReps.DividenDirectInvestment_Add(_DividenDirectInvestment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Insert Journal Detail Success", _Obj, "", param1, _DividenDirectInvestment.DirectInvestmentPK, _DividenDirectInvestmentPK, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Journal Detail Success");
                            }
                            if (PermissionID == "DividenDirectInvestment_D")
                            {
                                _DividenDirectInvestmentReps.DividenDirectInvestment_Delete(_DividenDirectInvestment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Delete Journal Detail Success", _Obj, "", param1, _DividenDirectInvestment.DirectInvestmentPK, _DividenDirectInvestment.DividenDirectInvestmentPK, 0, "");
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

        [HttpGet]
        public HttpResponseMessage CheckDividenDirectInvestment(string param1, string param2, int param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _DividenDirectInvestmentReps.CheckDividenDirectInvestment(param3));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

    }
}