
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
    public class InvestmentController : ApiController
    {
        static readonly string _Obj = "Investment Controller";
        static readonly InvestmentReps _investmentReps = new InvestmentReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        static readonly CustomClient01Reps _customClient01 = new CustomClient01Reps();
        static readonly CustomClient02Reps _customClient02 = new CustomClient02Reps();
        static readonly CustomClient03Reps _customClient03 = new CustomClient03Reps();
        static readonly CustomClient05Reps _customClient05 = new CustomClient05Reps();
        static readonly CustomClient06Reps _customClient06 = new CustomClient06Reps();
        static readonly CustomClient09Reps _customClient09 = new CustomClient09Reps();
        static readonly CustomClient12Reps _customClient12 = new CustomClient12Reps();
        static readonly CustomClient17Reps _customClient17 = new CustomClient17Reps();
        static readonly CustomClient19Reps _customClient19 = new CustomClient19Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly CustomClient21Reps _customClient21 = new CustomClient21Reps();
        static readonly CustomClient25Reps _customClient25 = new CustomClient25Reps();
        static readonly CustomClient99Reps _customClient99 = new CustomClient99Reps();



        [HttpGet]
        public HttpResponseMessage GetDataCounterpartExposure(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "05")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient05.Get_CounterpartExposure(param3));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_CounterpartExposure(param3));
                        }


                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "GetDataCounterpartExposure", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataInvestmentAcq(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_InvestmentDataAcq(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "GetDataCounterpartExposure", param1, 0, 0, 0, "");
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
        public string Investment_UpdateInvestmentAcq(string param1, string param2,[FromBody]List<InvestmentDataAcq> _InvestmentAcq)
        {
            string _msg = "";
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        try
                        {
                            return _msg = _investmentReps.Investment_UpdateDataInvestmentAcq(_InvestmentAcq);
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Investment_UpdateInvestmentAcq", param1, 0, 0, 0, "");
                    return "No Session";
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return "Internal Server Error";
            }
        }


        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = permissionID
       */
        [HttpPost]
        public HttpResponseMessage Investment_AddAmend(string param1, string param2, string param3, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (havePermission)
                        {

                            int _lastPK = _investmentReps.Investment_AddAmend(_investment);
                           // _host.Notification_Add(param1, "Add data in Investment", PermissionID);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Insert Amend Investment Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Insert Amend Investment Success", param1, 0, 0, 0, "");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Insert Amend Investment Success", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Investment_AmendReject(string param1, string param2,string param3, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (havePermission)
                        {
                            _investmentReps.Investment_AmendReject(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Amend Reject Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, 0, "REJECT");
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Amend Reject Investment Success", param1, 0, 0, 0, "");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Amend Reject Investment Success", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Investment_CancelAmendReject(string param1, string param2, string param3, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (havePermission)
                        {
                            _investmentReps.Investment_CancelAmend(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, param3, true, "Amend Cancel Reject Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, 0, "REJECT");
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "Amend Cancel Reject Investment Success", param1, 0, 0, 0, "");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Amend  Cancel Reject Investment Success", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        //INVESTMENT DATA
        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = status(pending = 1, approve = 2, history = 3)
        * param4 = DateFrom
        * param5 = DateTo
        * param6 = Instrument Type
        */


        [HttpGet]
        public HttpResponseMessage GetDataInvestmentByDateFromToAndInstrumentType(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateFromToAndInstrumentType(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type", param1, 0, 0, 0, "");
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
     * param4 = DateFrom
     * param5 = DateTo
     * param6 = Instrument Type
     */


        //[HttpGet]
        //public HttpResponseMessage GetDataInvestmentByDateFromToAndInstrumentTypeBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateFromToAndInstrumentTypeBuyOnly(param3, param4, param5, param6));
        //            }
        //            catch (Exception err)
        //            {

        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Buy Only", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}


        /*
      * param1 = userID
      * param2 = sessionID
      * param3 = status(pending = 1, approve = 2, history = 3)
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        //[HttpGet]
        //public HttpResponseMessage GetDataInvestmentByDateFromToAndInstrumentTypeSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6)
        //{
        //    try
        //    {
        //        bool session = Tools.SessionCheck(param1, param2);
        //        if (session)
        //        {
        //            try
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateFromToAndInstrumentTypeSellOnly(param3, param4, param5, param6));
        //            }
        //            catch (Exception err)
        //            {

        //                throw err;
        //            }
        //        }
        //        else
        //        {
        //            _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
        //            return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //    }
        //}



        //  /*
        //* param1 = userID
        //* param2 = sessionID
        //*/
        //  [HttpGet]
        //  public HttpResponseMessage GetMessageFromInvestmentNotes(string param1, string param2, DateTime param3, string param4, string param5)
        //  {
        //      try
        //      {
        //          bool session = Tools.SessionCheck(param1, param2);
        //          if (session)
        //          {
        //              try
        //              {
        //                  return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_MessageFromInvestmentNotes(param3,param4,param5));
        //              }
        //              catch (Exception err)
        //              {
        //                  throw err;
        //              }
        //          }
        //          else
        //          {
        //              _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Account Combo Child Only", param1, 0, 0, 0, "");
        //              return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
        //          }
        //      }
        //      catch (Exception err)
        //      {
        //          _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
        //          if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
        //      }
        //  }


        /*
   * param1 = userID
   * param2 = sessionID
   */
        [HttpGet]
        public HttpResponseMessage GetReferenceFromInvestment(string param1, string param2, DateTime param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_ReferenceFromInvestment(param3, param4));
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

        [HttpPost]
        public HttpResponseMessage InvestmentListingRpt(string param1, string param2, [FromBody]InvestmentListing _listing)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        // check permission ama Masukin Log Disini
                        if (_investmentReps.Investment_ListingRpt(param1, _listing))
                        {
                            //return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_"  + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "InvestmentListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Investment Listing Rpt", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }




        //DEALING RPT

        /*
  * param1 = userID
  * param2 = sessionID
  */
        [HttpGet]
        public HttpResponseMessage GetReferenceFromDealing(string param1, string param2, DateTime param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_ReferenceFromDealing(param3, param4));
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

        [HttpPost]
        public HttpResponseMessage DealingListingRpt(string param1, string param2, [FromBody]InvestmentListing _listing)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        #region KOSPIN
                        if (Tools.ClientCode == "12") //KOSPIN                        
                        {
                            if (_customClient12.Dealing_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        #endregion

                        #region Insight
                        else if (Tools.ClientCode == "03")
                        {
                            if (_customClient03.Dealing_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        #endregion

                        #region INDOASIA
                        else if (Tools.ClientCode == "06")
                        {
                            if (_customClient06.Dealing_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        #endregion

                        #region VALBURY
                        else if (Tools.ClientCode == "25")
                        {
                            if (_customClient25.Dealing_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        #endregion

                        else
                        {
                            if (_investmentReps.Dealing_ListingRpt(param1, _listing))
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "DealingListing_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Dealing Listing Rpt", param1, 0, 0, 0, "");
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
        [HttpGet]
        public HttpResponseMessage GetReferenceFromSettlement(string param1, string param2, DateTime param3, int param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_ReferenceFromSettlement(param3, param4, param5));
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




        [HttpPost]
        public HttpResponseMessage SettlementListingRpt(string param1, string param2, [FromBody]InvestmentListing _listing)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "03")
                        {
                            if (_customClient03.Settlement_ListingRpt(param1, _listing))
                            {
                                if (_listing.ParamInstType == "1")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else if (_listing.ParamInstType == "2")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else if (Tools.ClientCode == "06")
                        {
                            if (_customClient06.Settlement_ListingRpt(param1, _listing))
                            {
                                if (_listing.ParamInstType == "1")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else if (_listing.ParamInstType == "2")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else if (Tools.ClientCode == "09")
                        {
                            if (_customClient09.Settlement_ListingRpt(param1, _listing))
                            {
                                if (_listing.ParamInstType == "1")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else if (_listing.ParamInstType == "2")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else if (Tools.ClientCode == "19")
                        {
                            if (_customClient19.Settlement_ListingRpt(param1, _listing))
                            {
                                if (_listing.ParamInstType == "1")
                                {

                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else if (_listing.ParamInstType == "2")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }

                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else
                        {
                            if (_investmentReps.Settlement_ListingRpt(param1, _listing))
                            {
                                if (_listing.ParamInstType == "1")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else if (_listing.ParamInstType == "2")
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
                                }
                                else
                                {
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                    }
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Settlement Listing Rpt", param1, 0, 0, 0, "");
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
        public HttpResponseMessage SettlementListingRptEMCO(string param1, string param2, [FromBody]InvestmentListing _listing)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (_investmentReps.Settlement_ListingRptEMCO(param1, _listing))
                        {
                            if (_listing.ParamInstType == "2")
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingBond_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }

                            }
                            else if (_listing.ParamInstType == "1")
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingEquity_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }


                            }
                            else if (_listing.ParamInstType == "5")
                            {
                                if (_listing.DownloadMode == "PDF")
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".pdf");
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListingDeposito_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + param1 + ".xlsx");
                                }


                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "SettlementListing_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Settlement Listing Rpt", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataInvestmentByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateFromTo(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        //DEALING DATA



        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       * param4 = DateFrom
       * param5 = DateTo
       * param6 = Instrument Type
       */
        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToAndInstrumentType(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToAndInstrumentType(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To And Instrument Type", param1, 0, 0, 0, "");
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
       //* param1 = userID
       //* param2 = sessionID
       //* param3 = status(pending = 1, approve = 2, history = 3)
       //*/
       // [HttpGet]
       // public HttpResponseMessage GetDataDealingByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
       // {
       //     try
       //     {
       //         bool session = Tools.SessionCheck(param1, param2);
       //         if (session)
       //         {
       //             try
       //             {
       //                 return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromTo(param3, param4, param5));
       //             }
       //             catch (Exception err)
       //             {

       //                 throw err;
       //             }
       //         }
       //         else
       //         {
       //             _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To", param1, 0, 0, 0, "");
       //             return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
       //         }
       //     }
       //     catch (Exception err)
       //     {
       //         _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
       //         if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
       //     }
       // }


        //SETTLEMENT DATA



        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       * param4 = DateFrom
       * param5 = DateTo
       * param6 = Instrument Type
       */
        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToAndInstrumentType(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToAndInstrumentType(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To And Instrument Type", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataSettlementByDateFromTo(string param1, string param2, int param3, DateTime param4, DateTime param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromTo(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Dealing_ApproveValidate(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Dealing_ApproveValidate(_investment));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Dealing Approve Validate", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Settlement_ApproveValidate(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Settlement_ApproveValidate(_investment));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Settlement Approve Validate", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        /*ACTION
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Investment _investment)
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
                            if (PermissionID == "InvestmentInstruction_U")
                            {
                                int _newHisPK = _investmentReps.Investment_Update(_investment, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Investment Success");
                            }
                            if (PermissionID == "InvestmentInstruction_A")
                            {
                                _investmentReps.Investment_Approved(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Investment Success");
                            }
                            if (PermissionID == "InvestmentInstruction_V")
                            {
                                _investmentReps.Investment_Void(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Investment Success");
                            }
                            if (PermissionID == "InvestmentInstruction_R")
                            {
                                _investmentReps.Investment_Reject(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Investment Success", _Obj, "", param1, _investment.InvestmentPK, _investment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Investment Success");
                            }
                            if (PermissionID == "InvestmentInstruction_I")
                            {
                                int _lastPK = _investmentReps.Investment_Add(_investment, havePrivillege);
                                _host.Notification_Add(param1, "Add data in Investment", PermissionID);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Insert Investment Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Investment Success");
                            }
                          
                        
                            //DEALING
                            if (PermissionID == "DealingInstruction_U")
                            {
                                int _newHisPK = _investmentReps.Dealing_Update(_investment, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Dealing Success", _Obj, "", param1, _investment.DealingPK, _investment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Dealing Success");
                            }
                            if (PermissionID == "DealingInstruction_A")
                            {
                                _investmentReps.Dealing_Approved(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Dealing Success", _Obj, "", param1, _investment.DealingPK, _investment.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Dealing Success");
                            }
                            if (PermissionID == "DealingInstruction_V")
                            {
                                _investmentReps.Dealing_Void(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Dealing Success", _Obj, "", param1, _investment.DealingPK, _investment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Dealing Success");
                            }
                            if (PermissionID == "DealingInstruction_R")
                            {
                                _investmentReps.Dealing_Reject(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Dealing Success", _Obj, "", param1, _investment.DealingPK, _investment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Dealing Success");
                            }
                            //if (PermissionID == "DealingInstruction_I")
                            //{
                            //    int _lastPK = _investmentReps.Dealing_Add(_investment, havePrivillege);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Insert Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Insert Dealing Success");
                            //}
                            if (PermissionID == "DealingInstruction_UnApproved")
                            {
                                _investmentReps.Dealing_UnApproved(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApprove Dealing Success", _Obj, "", param1, _investment.DealingPK, _investment.HistoryPK, 0, "UNAPPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Dealing Success");
                            }
                            //SETTLEMENT
                            if (PermissionID == "SettlementInstruction_U")
                            {
                                int _newHisPK = _investmentReps.Settlement_Update(_investment, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Settlement Success", _Obj, "", param1, _investment.SettlementPK, _investment.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Settlement Success");
                            }
                            if (PermissionID == "SettlementInstruction_A")
                            {
                                _investmentReps.Settlement_Approved(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Settlement Success", _Obj, "", param1, _investment.SettlementPK, _investment.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Settlement Success");
                            }
                            if (PermissionID == "SettlementInstruction_V")
                            {
                                _investmentReps.Settlement_Void(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Settlement Success", _Obj, "", param1, _investment.SettlementPK, _investment.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Settlement Success");
                            }
                            if (PermissionID == "SettlementInstruction_R")
                            {
                                _investmentReps.Settlement_Reject(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Settlement Success", _Obj, "", param1, _investment.SettlementPK, _investment.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Settlement Success");
                            }
                            //if (PermissionID == "SettlementInstruction_I")
                            //{
                            //    int _lastPK = _investmentReps.Investment_Add(_investment, havePrivillege);
                            //    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Insert Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Insert Settlement Success");
                            //}
                            if (PermissionID == "SettlementInstruction_UnApproved")
                            {
                                _investmentReps.Settlement_UnApproved(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved Settlement Success", _Obj, "", param1, _investment.SettlementPK, _investment.HistoryPK, 0, "UNAPPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Settlement Success");
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
          * param3 = permissionID
          */
        [HttpPost]
        public HttpResponseMessage InvestmentSplit(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "DealingInstruction_Split";
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
                            int _lastPK = _investmentReps.Investment_SplitAdd(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Split Investment Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpPost]
        public HttpResponseMessage Investment_GetBondInterest(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Instrument_GetBondInterest(_investment));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Bond Interest", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Investment_ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Investment_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Investment_ApproveBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
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


        [HttpGet]
        public HttpResponseMessage Investment_RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Investment_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Investment_RejectBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Rejectd By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Investment_VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Investment_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Investment_VoidBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Void All By Selected Success");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Voidd By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Dealing_ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Dealing_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Dealing_ApproveBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
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


        [HttpGet]
        public HttpResponseMessage Dealing_RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Dealing_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Dealing_RejectBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Reject All By Selected Success");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Rejectd By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Dealing_VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                string PermissionID;
                PermissionID = param6;
                //string PermissionID = "Dealing_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        bool havePermission = _host.Get_Permission(param1, PermissionID);
                        if (havePermission)
                        {
                            _investmentReps.Dealing_VoidBySelected(param1, PermissionID, param3, param4, param5);
                            return Request.CreateResponse(HttpStatusCode.OK, "Void All By Selected Success");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Voidd By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Settlement_ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                string PermissionID = "Settlement_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _investmentReps.Settlement_ApproveBySelected(param1, PermissionID, param3, param4, param5);
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
        public HttpResponseMessage Settlement_RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                string PermissionID = "Settlement_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _investmentReps.Settlement_RejectBySelected(param1, PermissionID, param3, param4, param5);
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
        public HttpResponseMessage Settlement_VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                string PermissionID = "Settlement_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _investmentReps.Settlement_VoidBySelected(param1, PermissionID, param3, param4, param5);
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
       * param3 = permissionID
       */
        [HttpPost]
        public HttpResponseMessage InvestmentNotes(string param1, string param2, string param3, [FromBody]InvestmentListing _investment)
        {
            string PermissionID;
            PermissionID = "InvestmentNotes_I";
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

                            if (PermissionID == "InvestmentNotes_I")
                            {
                                int _lastPKByLastUpdate = _investmentReps.InvestmentNotes_Add(param3, _investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add " + param3 + " Notes Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");

                                return Request.CreateResponse(HttpStatusCode.OK, "Insert " + param3 + " Notes Success");
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
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartEquityBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartEquityBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Equity Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartEquitySellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartEquitySellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Equity Sell Only", param1, 0, 0, 0, "");
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
* param4 = DateFrom
* param5 = DateTo
* param6 = Instrument Type
*/


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartBondBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartBondBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Bond Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartBondSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartBondSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Bond Sell Only", param1, 0, 0, 0, "");
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
* param4 = DateFrom
* param5 = DateTo
* param6 = Instrument Type
*/


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartTimeDepositBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartTimeDepositBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund TimeDeposit Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartTimeDepositSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartTimeDepositSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund TimeDeposit Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartReksadanaBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartReksadanaBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Reksadana Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataDealingByDateFromToByFundByCounterpartReksadanaSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingByDateFromToByFundByCounterpartReksadanaSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Reksadana Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartEquityBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartEquityBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Counterpart Equity Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartEquitySellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartEquitySellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Countepart Equity Sell Only", param1, 0, 0, 0, "");
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
* param4 = DateFrom
* param5 = DateTo
* param6 = Instrument Type
*/


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartBondBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartBondBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Counterpart Bond Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartBondSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartBondSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Countepart Bond Sell Only", param1, 0, 0, 0, "");
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
* param4 = DateFrom
* param5 = DateTo
* param6 = Instrument Type
*/


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Counterpart TimeDeposit Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Counterpart TimeDeposit Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartReksadanaBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartReksadanaBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Counterpart Reksadana Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataSettlementByDateFromToByFundByCounterpartReksadanaSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataSettlementByDateFromToByFundByCounterpartReksadanaSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Settlement By Date From To By Fund By Countepart Reksadana Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage UpdateCounterpartBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "DealingInstruction_UpdateCounterpartBySelected";
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
                            int _lastPK = _investmentReps.Investment_UpdateCounterpartBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Counterpart Success", _Obj, "", param1, _lastPK, 0, 0, "INSERT");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
    */
        [HttpPost]
        public HttpResponseMessage ValidateApproveBySelectedDataSettlement(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_ApproveBySelectedDataSettlement(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approved Settlement", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateUnApproveBySelectedDataSettlement(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_UnApproveBySelectedDataSettlement(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Approved Settlement", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateRejectBySelectedDataSettlement(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_RejectBySelectedDataSettlement(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Reject Settlement", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateUpdateBrokerBySelectedData(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_UpdateBrokerBySelectedData(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Update Broker", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ApproveSettlementBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "SettlementInstruction_ApproveSettlementBySelected";
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
                            int _lastPK = _investmentReps.Investment_ApproveSettlementBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
       */
        [HttpPost]
        public HttpResponseMessage UnApproveSettlementBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "SettlementInstruction_UnApproveSettlementBySelected";
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
                            int _lastPK = _investmentReps.Investment_UnApproveSettlementBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnApproved Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
      */
        [HttpPost]
        public HttpResponseMessage RejectSettlementBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "SettlementInstruction_RejectSettlementBySelected";
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
                            if (Tools.ClientCode == "03")
                            {
                                int _lastPK = _customClient03.Investment_RejectSettlementBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                            }
                            else if (Tools.ClientCode == "20")
                            {
                                int _lastPK = _customClient20.Investment_RejectSettlementBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                            }
                            else
                            {
                                int _lastPK = _investmentReps.Investment_RejectSettlementBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Settlement Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
  * param3 = status(pending = 1, approve = 2, history = 3)
  * param4 = DateFrom
  * param5 = DateTo
  * param6 = Instrument Type
  */


        [HttpGet]
        public HttpResponseMessage GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Buy Only", param1, 0, 0, 0, "");
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
      * param4 = DateFrom
      * param5 = DateTo
      * param6 = Instrument Type
      */


        [HttpGet]
        public HttpResponseMessage GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly(string param1, string param2, int param3, DateTime param4, DateTime param5, int param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateApproveBySelectedDataDealing(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_ApproveBySelectedDataDealing(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Match Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateRejectBySelectedDataDealing(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_RejectBySelectedDataDealing(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Reject Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ApproveDealingBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "DealingInstruction_ApproveDealingBySelected";
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
                            if (Tools.ClientCode == "17")
                            {
                                int _lastPK = _customClient17.Investment_ApproveDealingBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Match Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                            }
                            else
                            {
                                int _lastPK = _investmentReps.Investment_ApproveDealingBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Match Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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
      */
        [HttpPost]
        public HttpResponseMessage RejectDealingBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "DealingInstruction_RejectDealingBySelected";
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
                            if (Tools.ClientCode == "03")
                            {
                                int _lastPK = _customClient03.Investment_RejectDealingBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                            }
                            else if (Tools.ClientCode == "20")
                            {
                                int _lastPK = _customClient20.Investment_RejectDealingBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
                            }
                            else
                            {
                                int _lastPK = _investmentReps.Investment_RejectDealingBySelected(_investment);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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


        [HttpPost]
        public HttpResponseMessage ValidateUnApproveBySelectedDataDealing(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_UnApproveBySelectedDataDealing(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate UnMatch Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage UnApproveDealingBySelected(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "DealingInstruction_UnApproveDealingBySelected";
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
                            int _lastPK = _investmentReps.Investment_UnApproveDealingBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "UnMatch Dealing Success", _Obj, "", param1, _lastPK, 0, 0, "APPROVED");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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

        [HttpPost]
        public HttpResponseMessage ValidateCheckTotalLotSplit(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CheckTotalLotSplit(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Total Split", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckTotalAmountSplit(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CheckTotalAmountSplit(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Total Amount", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckTotalAcqVolume(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CheckTotalAcqVolume(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Total Acq Volume", param1, 0, 0, 0, "");
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
        public HttpResponseMessage NettingReport(string param1, string param2, DateTime param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        if (_investmentReps.Netting_Report(param1, param3, param4))
                        {

                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "NettingReport" + "_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Settlement Listing Rpt", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckDataInvestment(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Check_DataInvestment(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Data Investment", param1, 0, 0, 0, "");
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
        public HttpResponseMessage CheckDataDealing(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Check_DataDealing(param3));
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
        public HttpResponseMessage ValidateCounterpartPercentage(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CounterpartPercentage(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Exposure", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByEquity(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "25")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient25.PTPEquity_BySelectedData(param1, param3, param4, _investment));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPEquity_BySelectedData(param1, param3, param4, _investment));
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Equity Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByBond(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "01")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient01.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else if (Tools.ClientCode == "03")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient03.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else if (Tools.ClientCode == "05")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient05.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else if (Tools.ClientCode == "20")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient20.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else if (Tools.ClientCode == "21")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient21.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else if (Tools.ClientCode == "25")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient25.PTPBond_BySelectedData(param1, param3, param4, _investment));
                        }
                        else
                        {
                            _investmentReps.PTPBond_BySelectedData(param1, param3, param4, _investment);
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlSinvestTextPath + "SINVEST_BOND.zip");
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Bond Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByDeposito(string param1, string param2, DateTime param3, DateTime param4, bool param5, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //if (Tools.ClientCode == "01")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient01.PTPDeposito_BySelectedData(param1, param3, param4, param5));
                        //}
                        //else if (Tools.ClientCode == "02")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient02.PTPDeposito_BySelectedData(param1, param3, param4, param5));
                        //}
                        //else if (Tools.ClientCode == "03")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient03.PTPDeposito_BySelectedData(param1, param3, param4, param5));
                        //}
                        //else if (Tools.ClientCode == "05")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient05.PTPDeposito_BySelectedData(param1, param3, param4, param5));
                        //}
                        //else if (Tools.ClientCode == "19")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient19.PTPDeposito_BySelectedData(param1, param3, param4, param5));
                        //}
                        if (Tools.ClientCode == "99")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient99.PTPDeposito_BySelectedData(param1, param3, param4, param5, _investment));
                        }
                        if (Tools.ClientCode == "25")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient25.PTPDeposito_BySelectedData(param1, param3, param4, param5, _investment));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPDeposito_BySelectedData(param1, param3, param4, param5, _investment));
                        }

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Deposito Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage BrokerFeeRpt(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (_investmentReps.BrokerFee_Rpt(param1))
                        {

                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "BrokerFeeRpt_" + param1 + ".pdf");


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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Broker Fee Rpt", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckPriceExposureSplit(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CheckPriceExposureSplit(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Price Exposure Split", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateUpdateInvestmentAcq(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_CheckStatusSettlement(param3));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Price Exposure Split", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateApproveBySelectedDataDealingBond(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_ApproveBySelectedDataDealingBond(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Match Dealing", param1, 0, 0, 0, "");
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
        public HttpResponseMessage SettlementRecalculate(string param1, string param2, [FromBody]SettlementRecalculate _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Settlement_Recalculate(_settlement));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Dealing Approve Validate", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataMatureByDate(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataMatureByDate());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage SelectDeselectDataMature(string param1, string param2, bool param3, string param4)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _investmentReps.SelectDeselectDataMature(param3, param4);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Mature", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateMature(string param1, string param2, bool param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _investmentReps.SelectDeselectAllDataByDateMature(param3, param4);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Mature", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        [HttpGet]
        public HttpResponseMessage InitDataMatureByDate(string param1, string param2, DateTime param3, string param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Init_DataMatureByDate(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataDepositoByDate(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDepositoByDate());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage SelectDeselectDataDeposito(string param1, string param2, bool param3, string param4)
        {
            try
            {

                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _investmentReps.SelectDeselectDataDeposito(param3, param4);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect Data Deposito", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        [HttpGet]
        public HttpResponseMessage SelectDeselectAllDataByDateDeposito(string param1, string param2, bool param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    _investmentReps.SelectDeselectAllDataByDateDeposito(param3, param4);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Select Deselect All Data By Date Deposito", param1);
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }



        [HttpGet]
        public HttpResponseMessage InitDataDepositoByDate(string param1, string param2, DateTime param3, DateTime param4, string param5, string param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        //if (Tools.ClientCode == "05")
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.OK, _customClient05.Init_DataDepositoByDate(param3, param4, param5, param6));
                        //}
                        //else
                        //{
                            return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Init_DataDepositoByDate(param3, param4, param5, param6));
                        //} 
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Investment By Date From To And Instrument Type Sell Only", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByCrossFund(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
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
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient21.PTPCrossFund_BySelectedData(param1, param3, param4, _settlement));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPCrossFund_BySelectedData(param1, param3, param4, _settlement));
                        }
                        
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Bond Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage AmortizeBond(string param1, string param2, [FromBody]Investment _investment)
        {
            string PermissionID;
            PermissionID = "SettlementInstruction_BondGenAmortize";
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
                            int _lastPK = _investmentReps.Investment_AmortizeBondBySelected(_investment);
                            _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Amortize Bond Success", _Obj, "", param1, _lastPK, 0, 0, "AMORTIZE BOND");
                            return Request.CreateResponse(HttpStatusCode.OK, _lastPK);
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

        [HttpPost]
        public HttpResponseMessage ValidateGetAvgPriceByTrx(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Validate_GetAvgPriceByTrx(param3, param4, _settlement));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Get AvgPrice By Trx", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetAvgPriceByTrx(string param1, string param2, [FromBody]InvestmentAvgPriceByTrx _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_AvgPriceByTrx(_investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get AvgPrice By Trx", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPAvgPriceByEquity(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPAvgPriceByEquity_BySelectedData(param1, param3, param4, _settlement));
                        

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Equity Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByOverseasEquity(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPOverseasEquity_BySelectedData(param1, param3, param4, _settlement));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP By Overseas Equity", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByOverseasBond(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPOverseasBond_BySelectedData(param1, param3, param4, _settlement));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP By Overseas Bond", param1, 0, 0, 0, "");
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
        public HttpResponseMessage UpdateSettlementEquityBuy(string param1, string param2, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        int _newHisPK = _investmentReps.Update_SettlementEquityBuy(_settlement);
                        //_activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Retrieve Success", _Obj, "", param1, _department.DepartmentPK, _department.HistoryPK, _newHisPK, "UPDATE");
                        return Request.CreateResponse(HttpStatusCode.OK, "Update Settlement Success");
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


        [HttpPost]
        public HttpResponseMessage UpdateSettlementEquitySell(string param1, string param2, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        int _newHisPK = _investmentReps.Update_SettlementEquitySell(_settlement);
                        //_activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Retrieve Success", _Obj, "", param1, _department.DepartmentPK, _department.HistoryPK, _newHisPK, "UPDATE");
                        return Request.CreateResponse(HttpStatusCode.OK, "Update Settlement Success");
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
        public HttpResponseMessage GetDataDetail(string param1, string param2, int param3, DateTime param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_DataDealingTotalByInstrument(param3,param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data Dealing By Date From To By Fund Equity Buy Only", param1, 0, 0, 0, "");
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
        [HttpGet]
        public HttpResponseMessage GetLastInvestmentPK(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.Get_LastInvestmentPK());
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Last InvestmentPK", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }



        [HttpPost]
        public HttpResponseMessage PTPByReksadana(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPReksadana_BySelectedData(param1, param3, param4, _settlement));


                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Reksadana Rpt By SelectedData", param1, 0, 0, 0, "");
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
* param3 = MKBDTrailsPK
*/
        [HttpPost]
        public HttpResponseMessage GenerateReportBroker(string param1, string param2, DateTime param3, DateTime param4)
        {

            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        if (_investmentReps.GenerateBroker(param1, param3, param4))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlBrokerPath + "Broker_" + param1 + ".xlsx");
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
                    //_activityReps.Activity_LogInsert(DateTime.Now, "GenerateDailyTransaction", false, Tools.NoPermissionLogMessage, _Obj, "Generate Daily Transaction", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Generate Daily Transaction", param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage UpdateMaturityForDeposito(string param1, string param2, [FromBody]Investment _settlement)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        int _newHisPK = _investmentReps.Update_MaturityForDeposito(_settlement);
                        //_activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Retrieve Success", _Obj, "", param1, _department.DepartmentPK, _department.HistoryPK, _newHisPK, "UPDATE");
                        return Request.CreateResponse(HttpStatusCode.OK, "Update Settlement Success");
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage KertasKerjaDealingSaham(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaDealing_Saham(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaDealingSaham_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Dealing Saham", param1, 0, 0, 0, "");
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
        public HttpResponseMessage KertasKerjaDealingObligasi(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaDealing_Obligasi(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaDealingObligasi_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Dealing Obligasi", param1, 0, 0, 0, "");
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
        public HttpResponseMessage KertasKerjaDealingTimeDeposit(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaDealing_TimeDeposit(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaDealingTimeDeposit_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Dealing TimeDeposit", param1, 0, 0, 0, "");
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
        public HttpResponseMessage PTPByDepositoAmmend(string param1, string param2, DateTime param3, DateTime param4, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _investmentReps.PTPDepositoAmmend_BySelectedData(param1, param3, param4, _investment));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "PTP Deposito Rpt BySelectedData", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetReferenceForDepoAmmend(string param1, string param2, [FromBody]Investment _investment)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        string _reference;
                        _reference = _investmentReps.Investment_GetReferenceForDepoAmmend(_investment);

                        return Request.CreateResponse(HttpStatusCode.OK, _reference);

                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Instrument_AddFromOMSTimeDeposit", param1, 0, 0, 0, "");
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
        public HttpResponseMessage KertasKerjaSettlementSaham(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaSettlement_Saham(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaSettlementSaham_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Settlement Saham", param1, 0, 0, 0, "");
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
        public HttpResponseMessage KertasKerjaSettlementObligasi(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaSettlement_Obligasi(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaSettlementObligasi_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Settlement Obligasi", param1, 0, 0, 0, "");
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
        public HttpResponseMessage KertasKerjaSettlementTimeDeposit(string param1, string param2, DateTime param3, DateTime param4, [FromBody]KertasKerja _kertasKerja)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {


                        if (_customClient20.KertasKerjaSettlement_TimeDeposit(param1, param3, param4, _kertasKerja))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "KertasKerjaSettlementTimeDeposit_" + param1 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Kertas Kerja Settlement TimeDeposit", param1, 0, 0, 0, "");
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
