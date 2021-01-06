
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
    public class ClientRedemptionController : ApiController
    {
        static readonly string _Obj = "Client Redemption Controller";
        static readonly ClientRedemptionReps _clientRedemptionReps = new ClientRedemptionReps();
        static readonly FundClientReps _fundClientReps = new FundClientReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        static readonly CustomClient07Reps _customClient07 = new CustomClient07Reps();
        static readonly CustomClient08Reps _customClient08 = new CustomClient08Reps();
        static readonly CustomClient10Reps _customClient10 = new CustomClient10Reps();
        static readonly CustomClient11Reps _customClient11 = new CustomClient11Reps();
        static readonly CustomClient14Reps _customClient14 = new CustomClient14Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly CustomClient21Reps _customClient21 = new CustomClient21Reps();
        


        [HttpPost]
        public HttpResponseMessage BatchAmountFormBySelectedDataTaspen(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        {
                            // check permission ama Masukin Log Disini
                            if (_clientRedemptionReps.ClientRedemptionAmountBatchFormBySelectedDataTaspen(param1, param3, param4, _clientRedemption))
                            {
                                //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Redemption Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage SInvestRedemptionRptBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "10")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient10.SInvestRedemptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else if (Tools.ClientCode == "14")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient14.SInvestRedemptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.SInvestRedemptionRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "SInvest Redemption Rpt BySelectedData", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_UnApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
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
        public HttpResponseMessage ValidateApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_ApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_PostingBySelected(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Redemption", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_VoidBySelected(param3, param4, _paramUnitRegistryBySelected));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.ClientRedemption_SelectClientRedemptionDateFromTo(param3, param4, param5));
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
        /*
        * param1 = userID
        * param2 = sessionID
        */
        [HttpPost]
        public HttpResponseMessage ClientRedemptionRecalculate(string param1, string param2, [FromBody]ParamClientRedemptionRecalculate _param)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.ClientRedemption_Recalculate(_param));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Client Redemption Recalculate", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]ClientRedemption _clientRedemption)
        {
            string PermissionID;
            PermissionID = param3;
            int _newHisPK;
            int _lastPKByLastUpdate;
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
                            if (PermissionID == "ClientRedemption_U")
                            {
                                if(Tools.ClientCode == "07")
                                {
                                    _newHisPK = _customClient07.ClientRedemption_Update(_clientRedemption, havePrivillege);
                                }
                                else
                                {
                                    _newHisPK = _clientRedemptionReps.ClientRedemption_Update(_clientRedemption, havePrivillege);
                                }
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Client Redemption Successs", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Client Redemption Success");
                            }
                            if (PermissionID == "ClientRedemption_A")
                            {
                                _clientRedemptionReps.ClientRedemption_Approved(_clientRedemption);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Client Redemption Success", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Client Redemption Success");
                            }
                            if (PermissionID == "ClientRedemption_V")
                            {
                                _clientRedemptionReps.ClientRedemption_Void(_clientRedemption);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Client Redemption Success", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Client Redemption Success");
                            }
                            if (PermissionID == "ClientRedemption_R")
                            {
                                _clientRedemptionReps.ClientRedemption_Reject(_clientRedemption);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Client Redemption Success", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Client Redemption Success");
                            }
                            if (PermissionID == "ClientRedemption_I")
                            {
                                if (Tools.ClientCode == "07")
                                {
                                    _lastPKByLastUpdate = _customClient07.ClientRedemption_Add(_clientRedemption, havePrivillege);
                                }
                                else
                                {
                                    _lastPKByLastUpdate = _clientRedemptionReps.ClientRedemption_Add(_clientRedemption, havePrivillege);
                                }
                                
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Redemption Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Client Redemption Success");
                            }
                            //if (PermissionID == "ClientRedemption_UnApproved")
                            //{
                            //    _clientRedemptionReps.ClientRedemption_UnApproved(_clientRedemption);
                            //    _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "UnApproved  ClientRedemption Success", "", "", param1);
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved  Client Subscription Success");
                            //}
                            //Belum
                            if (PermissionID == "ClientRedemption_Posting")
                            {
                                _clientRedemptionReps.ClientRedemption_Posting(_clientRedemption);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Client Redemption Success", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting  Client Redemption Success");
                            }

                            if (PermissionID == "ClientRedemption_Revise")
                            {
                                if (Tools.ClientCode == "07")
                                {
                                    _customClient07.ClientRedemption_Revise(_clientRedemption);
                                }
                                else if (Tools.ClientCode == "08")
                                {
                                    _customClient08.ClientRedemption_Revise(_clientRedemption);
                                }
                                else
                                {
                                    _clientRedemptionReps.ClientRedemption_Revise(_clientRedemption);
                                }
                            
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Revise Client Redemption Success", _Obj, "", param1, _clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Client Redemption Success");
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
                string PermissionID = "ClientRedemption_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientRedemptionReps.ClientRedemption_ApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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
                string PermissionID = "ClientRedemption_UnApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientRedemptionReps.ClientRedemption_UnApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);

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
                string PermissionID = "ClientRedemption_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            if (Tools.ClientCode == "07")
                            {
                                _customClient07.ClientRedemption_RejectBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");

                            }
                            else
                            {
                                _clientRedemptionReps.ClientRedemption_RejectBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
                            }

               

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
                string PermissionID = "ClientRedemption_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            if (Tools.ClientCode == "07")
                            {
                                _customClient07.ClientRedemption_PostingBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "08")
                            {
                                _customClient08.ClientRedemption_PostingBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _clientRedemptionReps.ClientRedemption_PostingUnitOnlyBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "24")
                            {
                                _clientRedemptionReps.ClientRedemption_PostingUnitOnlyBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "29")
                            {
                                _clientRedemptionReps.ClientRedemption_PostingUnitOnlyBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "99")
                            {
                                _clientRedemptionReps.ClientRedemption_PostingJournalBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else
                            {
                                _clientRedemptionReps.ClientRedemption_PostingJournalBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
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


        [HttpGet]
        public HttpResponseMessage PostingBySelectedDataManageUR(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "ClientRedemption_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            if (Tools.ClientCode == "07")
                            {
                                _customClient07.ClientRedemption_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else if (Tools.ClientCode == "08")
                            {
                                _customClient08.ClientRedemption_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _customClient10.ClientRedemption_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else
                            {
                                _clientRedemptionReps.ClientRedemption_PostingBySelected(param1, param3, param4, true, null);
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
        [HttpPost]
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string PermissionID = "ClientRedemption_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientRedemptionReps.ClientRedemption_VoidBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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
        public HttpResponseMessage GetNavBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _clientRedemptionReps.ClientRedemption_GetNavBySelected(param1, param3, param4, _paramUnitRegistryBySelected);
                        return Request.CreateResponse(HttpStatusCode.OK, "Get Nav By Selected Success");
                       
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Nav By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ClientRedemptionValidation(string param1, string param2, int param3, int param4, decimal param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_AddClientRedemption(param4, param3, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientRedemption", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchAmountFormBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        {
                            if (Tools.ClientCode == "20")
                            {
                                // check permission ama Masukin Log Disini
                                if (_customClient20.ClientRedemptionAmountBatchFormBySelectedData(param1, param3, param4, _clientRedemption))
                                {
                                    if (_clientRedemption.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".xlsx");
                                    }


                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                                }
                            }

                            if (Tools.ClientCode == "21")
                            {
                                // check permission ama Masukin Log Disini
                                if (_customClient21.ClientRedemptionAmountBatchFormBySelectedData(param1, param3, param4, _clientRedemption))
                                {

                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                                }
                            }
                            else
                            {
                                // check permission ama Masukin Log Disini
                                if (_clientRedemptionReps.ClientRedemptionAmountBatchFormBySelectedData(param1, param3, param4, _clientRedemption))
                                {
                                    if (_clientRedemption.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".xlsx");
                                    }


                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                                }
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Redemption Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchAmountFormBySelectedDataMandiri(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        {
                            // check permission ama Masukin Log Disini
                            if (_clientRedemptionReps.ClientRedemptionAmountBatchFormBySelectedDataMandiri(param1, param3, param4, _clientRedemption))
                            {
                                //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstructionBySelected_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Redemption Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchUnitFormBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                            // check permission ama Masukin Log Disini
                            if (_clientRedemptionReps.ClientRedemptionUnitBatchFormBySelectedData(param1, param3, param4, _clientRedemption))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDUnitInstructionBySelected_" + param1 + ".pdf");
                                //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDUnitInstructionBySelected_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Redemption Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ClientRedemption_AmountBatchForm(string param1, string param2, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientRedemptionReps.ClientRedemptionAmountBatchForm(param1, _clientRedemption))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDAmountInstruction_" + _clientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Batch Form RED Amount Instruction", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ClientRedemption_UnitBatchForm(string param1, string param2, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientRedemptionReps.ClientRedemptionUnitBatchForm(param1, _clientRedemption))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDUnitInstruction_" + _clientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Batch Form RED Unit Instruction", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PaymentReportListing(string param1, string param2, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientRedemptionReps.ClientRedemption_PaymentReportListing(param1, _clientRedemption))
                        {
                            if (_clientRedemption.DownloadMode == "PDF")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDUnitInstructionBypaymentDate_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormREDUnitInstructionBypaymentDate_" + param1 + ".xlsx");
                            }
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Redemption Batch Form", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }
        //[HttpGet]
        //public HttpResponseMessage ValidateCheckDescription(string param1, string param2, DateTime param3, DateTime param4, string param5)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _host.Validate_CheckDescription(param3, param4, param5));
        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Description", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}

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
        public HttpResponseMessage DormantValidation(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_DormantClientRedemption(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientRedemption", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        //GetIdentity
        [HttpGet]
        public HttpResponseMessage GetTransferType(string param1, string param2, string param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.ClientRedemption_GetTransferType(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Identity Fundclient", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckMinimumBalance(string param1, string param2, decimal param3, decimal param4, DateTime param5, int param6, int param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if(Tools.ClientCode == "07")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient07.Validate_CheckMinimumBalanceRedemption(param3, param4, param5, param6, param7));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_CheckMinimumBalance(param3, param4, param5, param6, param7));
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Check Minimum Balance", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetUnitRedemptionAll(string param1, string param2, int param3, int param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Get_UnitRedemptionAll(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Unit Redemption All", param1, 0, 0, 0, "");
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
        public HttpResponseMessage InitDataHoldingPeriod(string param1, string param2, string param3, string param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Init_DataHoldingPeriod(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Init Data Holding Period", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataHoldingPeriod(string param1, string param2, int param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Get_DataHoldingPeriod(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Holding Period", param1, 0, 0, 0, "");
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
        public HttpResponseMessage UpdateRedemptionFeeHoldingPeriod(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _clientRedemptionReps.Update_RedemptionFeeHoldingPeriod(param3);
                        return Request.CreateResponse(HttpStatusCode.OK, "");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Update Redemption Fee Holding Period", param1, 0, 0, 0, "");
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

        [HttpGet]
        public HttpResponseMessage InsertClientRedemptionAllMaturedFund(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        _clientRedemptionReps.InsertClientRedemptionAll_MaturedFund(param1, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Insert Redemption ALL for Matured Fund Success");

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "InsertClientRedemption_AllMaturedFund", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateInsertClientRedemptionAllMaturedFund(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_InsertClientRedemptionAllMaturedFund(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Insert ClientRedemption All Matured Fund", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CashInstructionBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientRedemption _clientRedemption)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        // check permission ama Masukin Log Disini
                        if (_clientRedemptionReps.CashInstruction_BySelectedData(param1, param3, param4, _clientRedemption))
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

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = ValueDate
        * param4 = CashAmount
        * param5 = UnitAmount
        * param6 = FundPK
        * param7 = FundClientPK
        */
        [HttpGet]
        public HttpResponseMessage ValidateClientRedemption(string param1, string param2, DateTime param3, decimal param4, decimal param5, int param6, int param7, bool param8)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.ValidateClientRedemption(param3, param4, param5, param6, param7, param8));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientRedemption", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientRedemptionReps.Validate_EDTUnitBySelected(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Redemption", param1, 0, 0, 0, "");
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
