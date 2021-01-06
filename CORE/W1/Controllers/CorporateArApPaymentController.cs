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
    public class CorporateArApPaymentController : ApiController
    {
        static readonly string _Obj = "CorporateArApPayment Controller";
        static readonly CorporateArApPaymentReps _CorporateArApPaymentReps = new CorporateArApPaymentReps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _CorporateArApPaymentReps.CorporateArApPayment_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]CorporateArApPayment _CorporateArApPayment)
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
                            if (PermissionID == "CorporateArApPayment_U")
                            {
                                int _newHisPK = _CorporateArApPaymentReps.CorporateArApPayment_Update(_CorporateArApPayment, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update CorporateArApPayment Success", _Obj, "", param1, _CorporateArApPayment.CorporateArApPaymentPK, _CorporateArApPayment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update CorporateArApPayment Success");
                            }
                            if (PermissionID == "CorporateArApPayment_A")
                            {
                                _CorporateArApPaymentReps.CorporateArApPayment_Approved(_CorporateArApPayment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved CorporateArApPayment Success", _Obj, "", param1, _CorporateArApPayment.CorporateArApPaymentPK, _CorporateArApPayment.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved CorporateArApPayment Success");
                            }
                            if (PermissionID == "CorporateArApPayment_V")
                            {
                                _CorporateArApPaymentReps.CorporateArApPayment_Void(_CorporateArApPayment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void CorporateArApPayment Success", _Obj, "", param1, _CorporateArApPayment.CorporateArApPaymentPK, _CorporateArApPayment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void CorporateArApPayment Success");
                            }
                            if (PermissionID == "CorporateArApPayment_R")
                            {
                                _CorporateArApPaymentReps.CorporateArApPayment_Reject(_CorporateArApPayment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject CorporateArApPayment Success", _Obj, "", param1, _CorporateArApPayment.CorporateArApPaymentPK, _CorporateArApPayment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject CorporateArApPayment Success");
                            }
                            if (PermissionID == "CorporateArApPayment_I")
                            {
                                int _lastPK = _CorporateArApPaymentReps.CorporateArApPayment_Add(_CorporateArApPayment, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add CorporateArApPayment Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert CorporateArApPayment Success");
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


        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _CorporateArApPaymentReps.CorporateArApPayment_SelectDataByDateFromTo(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data By Date From To", param1, 0, 0, 0, "");
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