
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
    public class ClientSubscriptionController : ApiController
    {
        static readonly string _Obj = "ClientSubscription Controller";
        static readonly ClientSubscriptionReps _clientSubscriptionReps = new ClientSubscriptionReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly FundClientReps _fundClientReps = new FundClientReps();
        static readonly Host _host = new Host();


        static readonly CustomClient07Reps _customClient07 = new CustomClient07Reps();
        static readonly CustomClient08Reps _customClient08 = new CustomClient08Reps();
        static readonly CustomClient10Reps _customClient10 = new CustomClient10Reps();
        static readonly CustomClient11Reps _customClient11 = new CustomClient11Reps();
        static readonly CustomClient14Reps _customClient14 = new CustomClient14Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly CustomClient21Reps _customClient21 = new CustomClient21Reps();

          [HttpGet]
        public HttpResponseMessage DormantValidation(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_DormantClientSubscription(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientSubscription", param1, 0, 0, 0, "");
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
          public HttpResponseMessage SInvestSubscriptionRptBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "07")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient07.SInvestSubscriptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else if (Tools.ClientCode == "14")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient14.SInvestSubscriptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else if (Tools.ClientCode == "10")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient10.SInvestSubscriptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.SInvestSubscriptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "SInvest Subscription Rpt BySelectedData", param1, 0, 0, 0, "");
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
        [HttpPost]
        public HttpResponseMessage ValidateUnApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_UnApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate UnApprove", param1, 0, 0, 0, "");
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
    */
         [HttpPost]
        public HttpResponseMessage ValidateApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_ApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approve", param1, 0, 0, 0, "");
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
    * param3 = Posting Date
    */
        [HttpGet]
        public HttpResponseMessage ValidatePostingBySelected(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_PostingBySelected(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Subscription", param1, 0, 0, 0, "");
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
    * param3 = Posting Date
    */
        [HttpPost]
        public HttpResponseMessage ValidatePostingNAVBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_PostingNAVBySelected(param3, param4, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Subscription", param1, 0, 0, 0, "");
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
*/
        [HttpPost]
        public HttpResponseMessage ValidateVoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_VoidBySelected(param3, param4, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Void", param1, 0, 0, 0, "");
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
        */
        [HttpGet]
        public HttpResponseMessage GetPKFromUnitRegistry(string param1, string param2, string param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Get_PKFromUnitRegistry(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Account Combo Child Only", param1, 0, 0, 0, "");
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
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetDataByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.ClientSubscription_SelectClientSubscriptionDateFromTo(param3, param4, param5));
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
       */
        [HttpPost]
        public HttpResponseMessage ClientSubscriptionRecalculate(string param1, string param2, [FromBody]ParamClientSubscriptionRecalculate _param)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.ClientSubscription_Recalculate(_param));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Client Subscription Recalculate", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]ClientSubscription _clientSubscription)
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
                            if (PermissionID == "ClientSubscription_U")
                            {
                                int _newHisPK = _clientSubscriptionReps.ClientSubscription_Update(_clientSubscription, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Client Subscription Successs", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Client Subscription Success");
                            }
                            if (PermissionID == "ClientSubscription_A")
                            {
                                _clientSubscriptionReps.ClientSubscription_Approved(_clientSubscription);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Client Subscription Success", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Client Subscription Success");
                            }
                            if (PermissionID == "ClientSubscription_V")
                            {
                                _clientSubscriptionReps.ClientSubscription_Void(_clientSubscription);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Client Subscription Success", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Client Subscription Success");
                            }
                            if (PermissionID == "ClientSubscription_R")
                            {
                                _clientSubscriptionReps.ClientSubscription_Reject(_clientSubscription);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Client Subscription Success", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Client Subscription Success");
                            }
                            if (PermissionID == "ClientSubscription_I")
                            {
                                if (Tools.ClientCode == "21")
                                {
                                    ClientSubscriptionAddNew _cs = new ClientSubscriptionAddNew();
                                    _cs = _customClient21.ClientSubscription_Add(_clientSubscription, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Subscription Success", _Obj, "", param1, _cs.ClientSubscriptionPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _cs);
                                }
                                else
                                {
                                    int _lastPKByLastUpdate = _clientSubscriptionReps.ClientSubscription_Add(_clientSubscription, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Subscription Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, "Insert Client Subscription Success");
                                }


                            }

                            if (PermissionID == "ClientSubscription_Posting")
                            {
                                _clientSubscriptionReps.ClientSubscription_Posting(_clientSubscription);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Client Subscription Success", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting  Client Subscription Success");
                            }

                            if (PermissionID == "ClientSubscription_Revise")
                            {
                                if (Tools.ClientCode == "08")
                                {
                                    _customClient08.ClientSubscription_Revise(_clientSubscription);
                                }
                                else if (Tools.ClientCode == "07")
                                {
                                    _customClient07.ClientSubscription_Revise(_clientSubscription);
                                }
                                else
                                {
                                    _clientSubscriptionReps.ClientSubscription_Revise(_clientSubscription);
                                }
             
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Revise Client Subscription Success", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Client Subscription Success");
                            }

                            if (PermissionID == "ClientSubscriptionWithInterest_U")
                            {
                                int _newHisPK = _clientSubscriptionReps.ClientSubscriptionWithInterest_Update(_clientSubscription, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Client Subscription Successs", _Obj, "", param1, _clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Client Subscription Success");
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
        /*
    * param1 = userID
    * param2 = sessionID
    * param3 = DateFrom
    * param4 = DateTo
    */
        [HttpPost]
        public HttpResponseMessage ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientSubscription_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                      bool havePermission = _host.Get_Permission(param1, PermissionID);
                      if (havePermission)
                      {
                          try
                          {
                              _clientSubscriptionReps.ClientSubscription_ApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                              return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
                          }
                          catch (Exception err)
                          {

                              throw err;
                          }
                      }
                      else
                      {
                          _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                          return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                      }
                  
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approved By Selected Data", param1, 0, 0, 0, "");
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
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpPost]
        public HttpResponseMessage UnApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientSubscription_UnApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientSubscriptionReps.ClientSubscription_UnApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "UnApprove All By Selected Success");
                        }
                        catch (Exception err)
                        {

                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                   
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "UnApprove By Selected Data", param1, 0, 0, 0, "");
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
      * param3 = DateFrom
      * param4 = DateTo
      */
        [HttpPost]
        public HttpResponseMessage RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientSubscription_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                      if (havePermission)
                      {
                          try
                          {
                              _clientSubscriptionReps.ClientSubscription_RejectBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                              return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
                          }
                          catch (Exception err)
                          {

                              throw err;
                          }
                      }
                      else
                      {
                          _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                          return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                      }
                    
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reject By Selected Data", param1, 0, 0, 0, "");
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
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpPost]
        public HttpResponseMessage PostingBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientSubscription_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            if (Tools.ClientCode == "08")
                            {
                                _customClient08.ClientSubscription_PostingBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "07")
                            {
                                _customClient07.ClientSubscription_PostingBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _clientSubscriptionReps.ClientSubscription_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "24")
                            {
                                _clientSubscriptionReps.ClientSubscription_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "29")
                            {
                                _clientSubscriptionReps.ClientSubscription_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else
                            {
                                _clientSubscriptionReps.ClientSubscription_PostingJournalBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }

                            return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                        }
                        catch (Exception err)
                        {

                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }

                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Posting By Selected Data", param1, 0, 0, 0, "");
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
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpGet]
        public HttpResponseMessage PostingBySelectedDataManageUR(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "ClientSubscription_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            if (Tools.ClientCode == "08")
                            {
                                _customClient08.ClientSubscription_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _customClient10.ClientSubscription_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else if (Tools.ClientCode == "07")
                            {
                                _customClient07.ClientSubscription_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else
                            {
                                _clientSubscriptionReps.ClientSubscription_PostingBySelected(param1, param3, param4, true, null);
                            }



                            return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                        }
                        catch (Exception err)
                        {

                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }

                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Posting By Selected Data", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }/*
* param1 = userID
* param2 = sessionID
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpPost]
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientSubscription_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientSubscriptionReps.ClientSubscription_VoidBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Void By Selected Success");
                        }
                        catch (Exception err)
                        {

                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }

                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Void By Selected Data", param1, 0, 0, 0, "");
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
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpPost]
        public HttpResponseMessage GetNavBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            string PermissionID;
            PermissionID = param5;
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

                            _clientSubscriptionReps.ClientSubscription_GetNavBySelected(param1, param3, param4, _paramUnitRegistryBySelected);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Get Nav By Selected", _Obj, "", param1, 0, 0, 0, "Get Nav By Selected");
                            return Request.CreateResponse(HttpStatusCode.OK, "Get Nav By Selected Success");

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
        public HttpResponseMessage ClientSubscriptionValidation(string param1, string param2, int param3, int param4, decimal param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_AddClientSubscription(param4, param3, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientSubscription", param1, 0, 0, 0, "");
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
        public HttpResponseMessage RegularForm(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        // check permission ama Masukin Log Disini
                        if (_clientSubscriptionReps.RegularForm(param1, param3,param4,_clientSubscription))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "RegularForm_" + param1 + ".pdf");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        }
                        //}

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Subscription Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ClientSubscription_BatchForm(string param1, string param2, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientSubscriptionReps.ClientSubscriptionBatchForm(param1, _clientSubscription))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstruction_" + _clientSubscription.NAVDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Subscription Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchFormBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "20")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient20.ClientSubscriptionBatchFormBySelectedData(param1, param3, param4, _clientSubscription))
                            {

                                if (_clientSubscription.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".xlsx");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "21")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient21.ClientSubscriptionBatchFormBySelectedData(param1, param3, param4, _clientSubscription))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".pdf");


                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else
                        {
                            // check permission ama Masukin Log Disini
                            if (_clientSubscriptionReps.ClientSubscriptionBatchFormBySelectedData(param1, param3, param4, _clientSubscription))
                            {
                                if (_clientSubscription.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".xlsx");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }


                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Subscription Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchFormBySelectedDataMandiri(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientSubscriptionReps.ClientSubscriptionBatchFormBySelectedDataMandiri(param1, param3, param4, _clientSubscription))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".pdf");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Subscription Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckDescription(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //return Request.CreateResponse(HttpStatusCode.OK, "");
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_CheckDescription(param3, param4, param5, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckFundClientPending(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //return Request.CreateResponse(HttpStatusCode.OK, "");
                        return Request.CreateResponse(HttpStatusCode.OK, _fundClientReps.Validate_CheckFundClientPending(param3, param4, param5, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckHasAdd(string param1, string param2, int param3)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.CheckHassAdd(param3));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpPost]
        public HttpResponseMessage BatchFormBySelectedDataTaspen(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientSubscriptionReps.ClientSubscriptionBatchFormBySelectedDataTaspen(param1, param3, param4, _clientSubscription))
                        {

                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSUBInstructionBySelected_" + param1 + ".xlsx");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Subscription Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidationCheckCutOffTime(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _host.Validation_CheckCutOffTime(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validation Check CutOffTime", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CashInstructionBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSubscription _clientSubscription)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        // check permission ama Masukin Log Disini
                        if (_clientSubscriptionReps.CashInstruction_BySelectedData(param1, param3, param4, _clientSubscription))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "CashInstruction_" + param1 + ".xlsx");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                        }
                        //}

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Cash Instruction", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckClientAPERD(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //return Request.CreateResponse(HttpStatusCode.OK, "");
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_CheckClientAPERD(param3, param4, param5, _paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidasiHighRiskMonitoringForNikko(string param1, string param2, [FromBody]UnitRegistryFund _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //return Request.CreateResponse(HttpStatusCode.OK, "");
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_CheckHighRiskMonitoringForNikko(_paramUnitRegistryBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Dealing", param1, 0, 0, 0, "");
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
        * param3 = ValueDate
        * param4 = CashAmount
        * param5 = FundPK
        * param6 = FundClientPK
        */
        [HttpGet]
        public HttpResponseMessage ValidateClientSubscriptionCustom08(string param1, string param2, DateTime param3, decimal param4, int param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_Custom08(param1, param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientSubscription", param1, 0, 0, 0, "");
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
* param3 = Posting Date
*/
        [HttpGet]
        public HttpResponseMessage ValidateEDTUnitBySelected(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.Validate_EDTUnitBySelected(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Subscription", param1, 0, 0, 0, "");
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
     * param3 = ValueDate
     * param4 = CashAmount
     * param5 = FundPK
     * param6 = FundClientPK
     */
        [HttpGet]
        public HttpResponseMessage ValidateClientSubscription(string param1, string param2, DateTime param3, decimal param4, int param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSubscriptionReps.ValidateClientSubscription(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientSubscription", param1, 0, 0, 0, "");
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
