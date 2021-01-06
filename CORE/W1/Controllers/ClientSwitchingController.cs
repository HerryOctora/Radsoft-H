
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
    public class ClientSwitchingController : ApiController
    {
        static readonly string _Obj = "ClientSwitching Controller";
        static readonly ClientSwitchingReps _clientSwitchingReps = new ClientSwitchingReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        static readonly CustomClient07Reps _customClient07 = new CustomClient07Reps();
        static readonly CustomClient08Reps _customClient08 = new CustomClient08Reps();
        static readonly CustomClient10Reps _customClient10 = new CustomClient10Reps();
        static readonly CustomClient14Reps _customClient14 = new CustomClient14Reps();
        static readonly CustomClient16Reps _customClient16 = new CustomClient16Reps();
        static readonly CustomClient21Reps _customClient21 = new CustomClient21Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        //static readonly CustomClient99Reps _customClient99 = new CustomClient99Reps();

        /*
* param1 = userID
* param2 = sessionID
* param3 = DateFrom
* param4 = DateTo
*/
        [HttpPost]
        public HttpResponseMessage SInvestSwitchingRptBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "16")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient16.SInvestSwitchingRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else if (Tools.ClientCode == "14")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient14.SInvestSwitchingRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else if (Tools.ClientCode == "10")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient10.SInvestSwitchingRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.SInvestSwitchingRpt_BySelectedData(param1, param3, param4, _paramUnitRegistryBySelected));
                        }


                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "SInvest Switching Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetNavBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _clientSwitchingReps.ClientSwitching_GetNavBySelected(param1, param3, param4, _paramUnitRegistryBySelected);
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_UnApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_PostingBySelected(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Posting Client Switching", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_VoidBySelected(param3, param4, _paramUnitRegistryBySelected));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.ClientSwitching_SelectClientSwitchingDateFromTo(param3, param4, param5));
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
        public HttpResponseMessage ClientSwitchingRecalculate(string param1, string param2, [FromBody]ParamClientSwitchingRecalculate _param)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.ClientSwitching_Recalculate(_param));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Client Switching Recalculate", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]ClientSwitching _clientSwitching)
        {
            string PermissionID;
            int _lastPKByLastUpdate;
            int _newHisPK;
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
                            if (PermissionID == "ClientSwitching_U")
                            {
                                if (Tools.ClientCode == "07")
                                {
                                    _newHisPK = _customClient07.ClientSwitching_Update(_clientSwitching, havePrivillege);
                                }
                                else
                                {
                                    _newHisPK = _clientSwitchingReps.ClientSwitching_Update(_clientSwitching, havePrivillege);
                                }
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Client Switching Successs", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Client Switching Success");
                            }
                            if (PermissionID == "ClientSwitching_A")
                            {
                                _clientSwitchingReps.ClientSwitching_Approved(_clientSwitching);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Client Switching Success", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Client Switching Success");
                            }
                            if (PermissionID == "ClientSwitching_V")
                            {
                                _clientSwitchingReps.ClientSwitching_Void(_clientSwitching);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Client Switching Success", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Client Switching Success");
                            }
                            if (PermissionID == "ClientSwitching_R")
                            {
                                _clientSwitchingReps.ClientSwitching_Reject(_clientSwitching);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Client Switching Success", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Client Switching Success");
                            }
                            if (PermissionID == "ClientSwitching_I")
                            {
                                if (Tools.ClientCode == "07")
                                {
                                    _lastPKByLastUpdate  = _customClient07.ClientSwitching_Add(_clientSwitching, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Switching Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, "Insert Client Switching Success");
                                }
                                else if (Tools.ClientCode == "21")
                                {
                                    ClientSwitchingAddNew _cs = new ClientSwitchingAddNew();
                                    _cs = _customClient21.ClientSwitching_Add(_clientSwitching, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Switching Success", _Obj, "", param1, _cs.ClientSwitchingPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _cs);
                                }
                                else
                                {
                                    _lastPKByLastUpdate = _clientSwitchingReps.ClientSwitching_Add(_clientSwitching, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Client Switching Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, "Insert Client Switching Success");
                                }
                                
                  
                            }
                            //if (PermissionID == "ClientSwitching_UnApproved")
                            //{
                            //    _clientSwitchingReps.ClientSwitching_UnApproved(_clientSwitching);
                            //    _activityReps.Activity_Insert(DateTime.Now, PermissionID, true, "UnApproved  ClientSwitching Success", "", "", param1);
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved  Client Subscription Success");
                            //}
                            //Belum
                            //if (PermissionID == "ClientSwitching_Posting")
                            //{
                            //    _clientSwitchingReps.ClientSwitching_Posting(_clientSwitching);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Client Subscription Success", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, 0, "POSTING");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Posting  Client Subscription Success");
                            //}

                            if (PermissionID == "ClientSwitching_Revise")
                            {
                                if (Tools.ClientCode == "07")
                                {
                                    _customClient07.ClientSwitching_Revise(param1, _clientSwitching);
                                } else if (Tools.ClientCode == "08")
                                {
                                    _customClient08.ClientSwitching_Revise(param1, _clientSwitching);
                                }
                                else
                                {
                                    _clientSwitchingReps.ClientSwitching_Revise(param1, _clientSwitching);
                                }
             
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Revise Client Switching Success", _Obj, "", param1, _clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Client Switching Success");
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
                string PermissionID = "ClientSwitching_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                     bool havePermission = _host.Get_Permission(param1, PermissionID);
                      if (havePermission)
                      {
                          try
                          {
                              _clientSwitchingReps.ClientSwitching_ApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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
                string PermissionID = "ClientSwitching_UnApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientSwitchingReps.ClientSwitching_UnApproveBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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
                string PermissionID = "ClientSwitching_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                     bool havePermission = _host.Get_Permission(param1, PermissionID);
                     if (havePermission)
                     {
                         try
                         {
                             _clientSwitchingReps.ClientSwitching_RejectBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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
                string PermissionID = "ClientSwitching_PostingBySelected";
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
                                _customClient08.ClientSwitching_PostingBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _clientSwitchingReps.ClientSwitching_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "24")
                            {
                                _clientSwitchingReps.ClientSwitching_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "29")
                            {
                                _clientSwitchingReps.ClientSwitching_PostingUnitBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else if (Tools.ClientCode == "99")
                            {
                                _clientSwitchingReps.ClientSwitching_PostingJournalBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
                            }
                            else
                            {
                                _clientSwitchingReps.ClientSwitching_PostingJournalBySelected(param1, param3, param4, false, _paramUnitRegistryBySelected);
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
                string PermissionID = "ClientSwitching_PostingBySelected";
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
                                _customClient08.ClientSwitching_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else if (Tools.ClientCode == "10")
                            {
                                _customClient10.ClientSwitching_PostingBySelected(param1, param3, param4, true, null);
                            }
                            else
                            {
                                _clientSwitchingReps.ClientSwitching_PostingBySelected(param1, param3, param4, true, null);
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
        public HttpResponseMessage ReviseBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody] ClientSwitching _clientSwitching)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _clientSwitchingReps.ClientSwitching_ReviseBySelected(param1, param3, param4, _clientSwitching);
                        return Request.CreateResponse(HttpStatusCode.OK, "Revise All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Revise By Selected Data", param1, 0, 0, 0, "");
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
                string PermissionID = "ClientSwitching_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {
                            _clientSwitchingReps.ClientSwitching_VoidBySelected(param1, PermissionID, param3, param4, _paramUnitRegistryBySelected);
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


        //mekel
        [HttpPost]
        public HttpResponseMessage BatchFormBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSwitching _clientSwitching)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "21")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient21.ClientSwitchingBatchFormBySelectedData(param1, param3, param4, _clientSwitching))
                            {
                                //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSWITCHInstructionBySelected_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "20")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient20.ClientSwitchingBatchFormBySelectedData(param1, param3, param4, _clientSwitching))
                            {
                                if (_clientSwitching.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSWITCHInstructionBySelected_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSWITCHInstructionBySelected_" + param1 + ".xlsx");
                                }
                            }
                               
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else
                        {
                            // check permission ama Masukin Log Disini
                            if (_clientSwitchingReps.ClientSwitchingBatchFormBySelectedData(param1, param3, param4, _clientSwitching))
                            {
                                //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSWITCHInstructionBySelected_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Switching Batch Form", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BatchFormBySelectedDataMandiri(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ClientSwitching _clientSwitching)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_clientSwitchingReps.ClientSwitchingBatchFormBySelectedDataMandiri(param1, param3, param4, _clientSwitching))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BatchFormSWITCHInstructionBySelected_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Switching Batch Form", param1, 0, 0, 0, "");
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_CheckDescription(param3, param4, param5, _paramUnitRegistryBySelected));
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

        //[HttpGet]
        //public HttpResponseMessage ValidateCheckFundClientPending(string param1, string param2, DateTime param3, DateTime param4, string param5)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _host.Validate_CheckFundClientPending(param3, param4, param5));
        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody]ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_ApproveBySelected(param3, param4, _paramUnitRegistryBySelected));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.ClientSwitching_GetTransferType(param3, param4, param5));
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
                        if (Tools.ClientCode == "07")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient07.Validate_CheckMinimumBalanceSwitching(param3, param4, param5, param6, param7));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_CheckMinimumBalance(param3, param4, param5, param6, param7));
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
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.Validate_CheckClientAPERD(param3, param4, param5, _paramUnitRegistryBySelected));
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
        * param5 = UnitAmount
        * param6 = FundPK
        * param7 = FundClientPK
        */
        [HttpPost]
        public HttpResponseMessage ValidateClientSwitching(string param1, string param2, [FromBody] ClientSwitching _ClientSwitching)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _clientSwitchingReps.ValidateClientSwitching(_ClientSwitching));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "ValidateAddClientSwitching", param1, 0, 0, 0, "");
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
