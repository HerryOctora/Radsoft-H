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
using RFSRepositoryEDTPortfolio;

namespace W1.Controllers
{
    public class EndDayTrailsFundPortfolioForPIIController : ApiController
    {
        static readonly string _Obj = "End Day Trails Fund Portfolio Controller";
        static readonly EndDayTrailsFundPortfolioForPIIReps _EndDayTrailsFundPortfolioForPIIReps = new EndDayTrailsFundPortfolioForPIIReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


        /*
     * param1 = userID
     * param2 = sessionID
     * param3 = status(pending = 1, approve = 2, history = 3)
     */
        [HttpGet]
        public HttpResponseMessage GetDataByDate(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_SelectEndDayTrailsFundPortfolioForPIIDate(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data By Date", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

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
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_Select(param3));
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
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /*
             * param1 = userID
             * param2 = sessionID
             * param3 = Generate Date
             */
        [HttpGet]
        public HttpResponseMessage ValidateGenerateEndDayTrailsFundPortfolioForPII(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.ValidateGenerateEndDayTrailsFundPortfolioForPII(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateGenerateEndDayTrailsFundPortfolioForPII", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
      * param1 = userID
      * param2 = sessionID
      * param3 = Generate Date
      */
        [HttpGet]
        public HttpResponseMessage ValidateGenerateSettlement(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.ValidateGenerateSettlement(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Generate Settlement", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = permissionID
        */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]EndDayTrailsFundPortfolioForPII _EndDayTrailsFundPortfolioForPII)
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

                            if (PermissionID == "EndDayTrailsFundPortfolioForPII_A")
                            {
                                _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_Approved(_EndDayTrailsFundPortfolioForPII);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved EndDayTrailsFundPortfolioForPII Success", _Obj, "", param1, _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK, _EndDayTrailsFundPortfolioForPII.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved EndDayTrailsFundPortfolioForPII Success");
                            }
                            if (PermissionID == "EndDayTrailsFundPortfolioForPII_V")
                            {
                                _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_Void(_EndDayTrailsFundPortfolioForPII);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void EndDayTrailsFundPortfolioForPII Success", _Obj, "", param1, _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK, _EndDayTrailsFundPortfolioForPII.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void EndDayTrailsFundPortfolioForPII Success");
                            }
                            if (PermissionID == "EndDayTrailsFundPortfolioForPII_R")
                            {
                                _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_Reject(_EndDayTrailsFundPortfolioForPII);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject EndDayTrailsFundPortfolioForPII Success", _Obj, "", param1, _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK, _EndDayTrailsFundPortfolioForPII.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject EndDayTrailsFundPortfolioForPII Success");
                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoActionMessage);
                            }
                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
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


        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = permissionID
        * param4 = date
        */
        [HttpPost]
        public HttpResponseMessage ApproveByDate(string param1, string param2, string param3, DateTime param4)
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
                            if (PermissionID == "EndDayTrailsFundPortfolioForPII_ApproveByDate")
                            {
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approve End Day Trails By Date Success", _Obj, "", param1, 0, 0, 0, "APPROVED BY DATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approve End Day Trails By Date " + _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_ApproveByDate(param1, param4));
                            }
                            else
                            {
                                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Approve By Date", param1, 0, 0, 0, "");
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.NoPermissionMessage);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoActionMessage);
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approve By Date", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage + " : Approve By Date = " + PermissionID, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }




        [HttpGet]
        public HttpResponseMessage CheckOMS_EndDayTrailsFundPortfolioForPII(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.CheckOMS_EndDayTrailsFundPortfolioForPII(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data By Date", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage GenerateWithParamFundForPII(string param1, string param2, DateTime param3, [FromBody]EndDayTrailsFundPortfolioForPII _edt)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    string PermissionID = "EndDayTrailsFundPortfolioForPII_Generate";
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            int _lastPK = 0;

                                _lastPK = _EndDayTrailsFundPortfolioForPIIReps.EndDayTrailsFundPortfolioForPII_GenerateWithParamFund(param1, param3, _edt);


                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Generate End Day Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, "Generate End Day Success");
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Generate", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Generate", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage CheckOMS_EndDayTrailsFundPortfolioForPIIWithParamFund(string param1, string param2, DateTime param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.CheckOMS_EndDayTrailsFundPortfolioForPIIWithParamFund(param3, param4));

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data By Date", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*
 * param1 = userID
 * param2 = sessionID
 * param3 = Generate Date
 */
        [HttpGet]
        public HttpResponseMessage ValidateGenerateEndDayTrailsFundPortfolioForPIIParamFund(string param1, string param2, DateTime param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _EndDayTrailsFundPortfolioForPIIReps.ValidateGenerateEndDayTrailsFundPortfolioForPIIParamFund(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateGenerateEndDayTrailsFundPortfolioForPIIParamFund", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }







    }
}