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
    public class FundJournalController : ApiController
    {
        static readonly string _Obj = "Fund Journal Controller";
        static readonly FundJournalReps _fundJournalReps = new FundJournalReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        [HttpGet]
        public HttpResponseMessage FundJournalCheckBalance(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _fundJournalReps.Fund_JournalCheckBalance(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Alert Holiday", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
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
         * param3 = DateFrom
         * param4 = DateTo
         */
        [HttpGet]
        public HttpResponseMessage ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FundJournal_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _fundJournalReps.FundJournal_ApproveBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
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
        [HttpGet]
        public HttpResponseMessage RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FundJournal_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _fundJournalReps.FundJournal_RejectBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
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
        [HttpGet]
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FundJournal_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _fundJournalReps.FundJournal_VoidBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Void All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
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
        [HttpGet]
        public HttpResponseMessage PostingBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FundJournal_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _fundJournalReps.FundJournal_PostingBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
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
        public HttpResponseMessage ReverseBySelectedData(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                string PermissionID = "FundJournal_ReverseBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _fundJournalReps.FundJournal_ReverseBySelected(param1, PermissionID, param3, param4);
                        return Request.CreateResponse(HttpStatusCode.OK, "Reverse All By Selected Success");
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reverse By Selected Data", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


     //   /*
     //* param1 = userID
     //* param2 = sessionID
     //* param3 = DateFrom
     //* param4 = DateTo
     //*/
     //   [HttpGet]
     //   public HttpResponseMessage GeneratePortfolioRevaluation(string param1, string param2, DateTime param3)
     //   {
     //       try
     //       {
     //           bool session = Tools.SessionCheck(param1, param2);
     //           if (session)
     //           {
     //               try
     //               {
     //                   _fundJournalReps.Generate_PortfolioRevaluation(param1, param3);
     //                   return Request.CreateResponse(HttpStatusCode.OK, true);
     //               }
     //               catch (Exception err)
     //               {

     //                   throw err;
     //               }
     //           }
     //           else
     //           {
     //               _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Generate Portfolio Revaluation", param1, 0, 0, 0, "");
     //               return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
     //           }
     //       }
     //       catch (Exception err)
     //       {
     //           _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
     //           if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
     //       }
     //   }

     //   /*
     //  * param1 = userID
     //  * param2 = sessionID
     //  * param3 = DateFrom
     //  * param4 = DateTo
     //  */
     //   [HttpGet]
     //   public HttpResponseMessage GenerateCurrencyRevaluation(string param1, string param2, DateTime param3, DateTime param4)
     //   {
     //       try
     //       {
     //           bool session = Tools.SessionCheck(param1, param2);
     //           if (session)
     //           {
     //               try
     //               {
     //                   _fundJournalReps.Generate_CurrencyRevaluation(param1, param3, param4);
     //                   return Request.CreateResponse(HttpStatusCode.OK, true);
     //               }
     //               catch (Exception err)
     //               {

     //                   throw err;
     //               }
     //           }
     //           else
     //           {
     //               _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Generate Currency Revaluation", param1, 0, 0, 0, "");
     //               return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
     //           }
     //       }
     //       catch (Exception err)
     //       {
     //           _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
     //           if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
     //       }
     //   }

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetDataFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _fundJournalReps.FundJournal_SelectFromTo(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data From To", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetData(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _fundJournalReps.FundJournal_Select(param3));
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
    * param3 = status(pending = 1, approve = 2, history = 3)
    */
        [HttpGet]
        public HttpResponseMessage GetReferenceCombo(string param1, string param2, DateTime param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _fundJournalReps.Get_ReferenceCombo(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Reference Combo", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FundJournal _fundJournal)
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
                            if (PermissionID == "FundJournal_U")
                            {
                                int _newHisPK = _fundJournalReps.FundJournal_Update(_fundJournal, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Fund Journal Success");
                            }
                            if (PermissionID == "FundJournal_A")
                            {
                                _fundJournalReps.FundJournal_Approved(_fundJournal);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "APPROVED");

                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Fund Journal Success");
                            }
                            if (PermissionID == "FundJournal_V")
                            {
                                _fundJournalReps.FundJournal_Void(_fundJournal);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Fund Journal Success");
                            }
                            if (PermissionID == "FundJournal_R")
                            {
                                _fundJournalReps.FundJournal_Reject(_fundJournal);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Fund Journal Success");
                            }
                            if (PermissionID == "FundJournal_I")
                            {
                                FundJournalAddNew _fj = new FundJournalAddNew();
                                _fj = _fundJournalReps.FundJournal_Add(_fundJournal, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Fund Journal Success", _Obj, "", param1, _fj.FundJournalPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, _fj);
                            }
                            //if (PermissionID == "FundJournal_UnApproved")
                            //{
                            //    _fundJournalReps.FundJournal_UnApproved(_fundJournal);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved FundJournal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "APPROVED");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved FundJournal Success");
                            //}
                            if (PermissionID == "FundJournal_Posting")
                            {
                                _fundJournalReps.FundJournal_Posting(_fundJournal);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting Fund Journal Success");
                            }

                            if (PermissionID == "FundJournal_Reversed")
                            {
                                _fundJournalReps.FundJournal_Reversed(_fundJournal);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reversed Fund Journal Success", _Obj, "", param1, _fundJournal.FundJournalPK, _fundJournal.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reversed Fund Journal Success");
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


        //[HttpPost]
        //public HttpResponseMessage FundJournalVoucher(string param1, string param2, [FromBody]FundJournal _fundJournal)
        //{

        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                // check permission ama Masukin Log Disini
        //                if (_fundJournalReps.FundJournal_Voucher(param1, _fundJournal))
        //                {
        //                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "FundJournalVoucher_" + param1 + ".pdf");
        //                }
        //                else
        //                {
        //                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
        //                }

        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Action", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}


        [HttpPost]
        public HttpResponseMessage FundJournalVoucher(string param1, string param2, [FromBody]FundJournal _fundJournal)
        {

            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_fundJournalReps.FundJournal_Voucher(param1, _fundJournal))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "FundJournalVoucher_" + param1 + ".pdf");
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
