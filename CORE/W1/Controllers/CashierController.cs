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
    public class CashierController : ApiController
    {
        static readonly string _Obj = "Cashier Controller";
        static readonly CashierReps _cashierReps = new CashierReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        static readonly CustomClient01Reps _customClient01 = new CustomClient01Reps();
        static readonly CustomClient05Reps _customClient05 = new CustomClient05Reps();
        static readonly CustomClient09Reps _customClient09 = new CustomClient09Reps(); 
        static readonly CustomClient12Reps _customClient12 = new CustomClient12Reps();
        static readonly CustomClient14Reps _customClient14 = new CustomClient14Reps();
        static readonly CustomClient19Reps _customClient19 = new CustomClient19Reps();
        static readonly CustomClient22Reps _customClient22 = new CustomClient22Reps();

        /*
          * param1 = userID
          * param2 = sessionID
          * param3 = DateFrom
          * param4 = DateTo
          */
        [HttpPost]
        public HttpResponseMessage ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string PermissionID = "Cashier_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "01")
                        {
                            _customClient01.Cashier_ApproveBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
                        }
                        else if (Tools.ClientCode == "09")
                        {
                            _customClient09.Cashier_ApproveBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
                        }
                        else
                        {
                            _cashierReps.Cashier_ApproveBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Approve All By Selected Success");
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

        /*
          * param1 = userID
          * param2 = sessionID
          * param3 = DateFrom
          * param4 = DateTo
          */
        [HttpPost]
        public HttpResponseMessage RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string PermissionID = "Cashier_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _cashierReps.Cashier_RejectBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
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
        [HttpPost]
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string PermissionID = "Cashier_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _cashierReps.Cashier_VoidBySelected(param1, PermissionID, param3, param4,param5, _paramCashierBySelected);
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
        [HttpPost]
        public HttpResponseMessage PostingBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string PermissionID = "Cashier_PostingBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (Tools.ClientCode == "01")
                        {
                            _customClient01.Cashier_PostingBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                        }
                        else
                        {
                            _cashierReps.Cashier_PostingBySelected(param1, PermissionID, param3, param4, param5, _paramCashierBySelected);
                            return Request.CreateResponse(HttpStatusCode.OK, "Posting All By Selected Success");
                        }

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
        public HttpResponseMessage ReviseBySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                string PermissionID = "Cashier_ReviseBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _cashierReps.Cashier_ReviseBySelected(param1, PermissionID, param3, param4,param5);
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
        * param5 = CashierType
        */
        [HttpGet]
        public HttpResponseMessage GetReferenceComboByCashierType(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_ReferenceComboByCashierType(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Reference Combo By CashierType", param1, 0, 0, 0, "");
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
      * param5 = CashierType
      */
        [HttpGet]
        public HttpResponseMessage GetReferenceComboByCashierTypeRpt(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_ReferenceComboByCashierTypeRpt(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Reference Combo By CashierType Rpt", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
         * param2 = sessionID
         * param3 = reference
         * param4 = accountPK
         */
        [HttpGet]
        public HttpResponseMessage GetSumPercentAmountByReferenceByAccount(string param1, string param2, string param3, int param4, int param5, string param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_SumPercentAmountByReferenceByAccount(param3 = param3.Replace("-", "/"), param4, param5, param6));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Sum Percent Amount By Reference By Account", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
        * param2 = sessionID
        * param3 = reference
        * param4 = accountPK
        */
        [HttpGet]
        public HttpResponseMessage GetSumPercentAmountByCashierIDByAccount(string param1, string param2, long param3, int param4, int param5, string param6, int param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_SumPercentAmountByCashierIDByAccount(param3, param4, param5, param6, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Sum Percent Amount By Reference By Account", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
        /* param1 = userID
        * param2 = sessionID
        * param3 = reference
        */
        [HttpGet]
        public HttpResponseMessage GetBankBalanceByReference(string param1, string param2, string param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_BankBalanceByReference(param3 = param3.Replace("-", "/"), param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Bank Balance By Reference", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
       * param2 = sessionID
       * param3 = CashierID
       */
        [HttpGet]
        public HttpResponseMessage GetBankBalanceByCashierID(string param1, string param2, long param3, string param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Get_BankBalanceByCashierID(param3, param4, param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Bank Balance By Reference", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }

        /* param1 = userID
     * param2 = sessionID
     * param3 = CashierID
     */
        [HttpGet]
        public HttpResponseMessage Cashier_GenerateNewCashierID(string param1, string param2, string param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Cashier_GetNewCashierID(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Cashier Generate New CashierID", param1, 0, 0, 0, "");
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
        public HttpResponseMessage AddCashierValidate(string param1, string param2, string param3, string param4, string param5, string param6)
        {
            try
            {
                {
                    if (Tools.ClientCode == "01")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _customClient01.Add_CashierValidate(param3, param4, param5, param6));
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Add_CashierValidate(param3, param4, param5, param6));
                    }
                   

                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Add Cashier Validate", param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }

        }

        /*
       * param1 = userID
       * param2 = sessionID
       * param3 = status(pending = 1, approve = 2, history = 3)
       */
        [HttpGet]
        public HttpResponseMessage GetDataFromTo(string param1, string param2, int param3, string param4, DateTime param5, DateTime param6)
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
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient01.Cashier_SelectFromTo(param3, param4, param5, param6));
                        }
                        else if (Tools.ClientCode == "09")
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _customClient09.Cashier_SelectFromTo(param3, param4, param5, param6));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Cashier_SelectFromTo(param3, param4, param5, param6));
                        }
                       
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
         * param4 = Cashier Type
         */
        [HttpGet]
        public HttpResponseMessage GetData(string param1, string param2, int param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Cashier_SelectByType(param3, param4));
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
      * param3 = Type
      */
        [HttpGet]
        public HttpResponseMessage ReferenceSelectFromCashierByType(string param1, string param2, string param3, DateTime param4, int param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Reference_SelectFromCashierByType(param3, param4,param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reference Select From Cashier By Type", param1, 0, 0, 0, "");
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
    * param3 = Type
    */
        [HttpGet]
        public HttpResponseMessage CashierIDSelectFromCashierByType(string param1, string param2, string param3, int param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.CashierID_SelectFromCashierByType(param3, param4));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "CashierID Select From Cashier By Type", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Cashier _cashier)
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
                            if (PermissionID == "CashierPayment_U")
                            {
                                int _newHisPK = 0;
                                if (Tools.ClientCode == "01")
                                {
                                    _newHisPK = _customClient01.Cashier_Update(_cashier, havePrivillege);
                                }
                                else
                                {
                                    _newHisPK = _cashierReps.Cashier_Update(_cashier, havePrivillege);
                                }

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Journal Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Cashier Payment Success");
                            }
                            if (PermissionID == "CashierPayment_A")
                            {
                                _cashierReps.Cashier_Approved(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Cashier Payment Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Cashier Payment Success");
                            }
                            if (PermissionID == "CashierPayment_V")
                            {
                                _cashierReps.Cashier_Void(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Cashier Payment Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Cashier Payment Success");
                            }
                            if (PermissionID == "CashierPayment_R")
                            {
                                _cashierReps.Cashier_Reject(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Cashier Payment Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Cashier Payment Success");
                            }
                            if (PermissionID == "CashierPayment_I")
                            {
                                CashierAddNew _c = new CashierAddNew();
                                if (Tools.ClientCode == "01")
                                {
                                    _c = _customClient01.Cashier_Add(_cashier, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Cashier Payment Success", _Obj, "", param1, _c.CashierPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _c);
                                }
                                else
                                {
                                    _c = _cashierReps.Cashier_Add(_cashier, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Cashier Payment Success", _Obj, "", param1, _c.CashierPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _c);
                                }

                            }

                            if (PermissionID == "CashierPayment_Posting")
                            {
                                _cashierReps.Cashier_Posting(_cashier);
                                
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Cashier Payment Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting Cashier Payment Success");
                            }

                            if (PermissionID == "CashierPayment_Revise")
                            {
                                _cashierReps.Cashier_Revise(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reversed Cashier Payment Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Cashier Payment Success");
                            }


                            //RECEIPT
                            if (PermissionID == "CashierReceipt_U")
                            {
                                int _newHisPK = 0;
                                if (Tools.ClientCode == "01")
                                {
                                    _newHisPK = _customClient01.Cashier_Update(_cashier, havePrivillege);
                                }
                                else
                                {
                                    _newHisPK = _cashierReps.Cashier_Update(_cashier, havePrivillege);
                                }
                          

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Cashier Receipt Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Cashier Receipt Success");
                            }
                            if (PermissionID == "CashierReceipt_A")
                            {
                                _cashierReps.Cashier_Approved(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Cashier Receipt Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Cashier Receipt Success");
                            }
                            if (PermissionID == "CashierReceipt_V")
                            {
                                _cashierReps.Cashier_Void(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Cashier Receipt Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Cashier Receipt Success");
                            }
                            if (PermissionID == "CashierReceipt_R")
                            {
                                _cashierReps.Cashier_Reject(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Cashier Receipt Success", _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Cashier Receipt Success");
                            }
                            if (PermissionID == "CashierReceipt_I")
                            {
                                CashierAddNew _c = new CashierAddNew();
                                if (Tools.ClientCode == "01")
                                {
                                    _c = _customClient01.Cashier_Add(_cashier, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Cashier Receipt Success", _Obj, "", param1, _c.CashierPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _c);
                                }
                                else
                                {
                                    _c = _cashierReps.Cashier_Add(_cashier, havePrivillege);
                                    _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Cashier Receipt Success", _Obj, "", param1, _c.CashierPK, 0, 0, "INSERT");
                                    return Request.CreateResponse(HttpStatusCode.OK, _c);
                                }
                            }

                            if (PermissionID == "CashierReceipt_Posting")
                            {
                                _cashierReps.Cashier_Posting(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Posting Cashier Receipt Success:" + _cashier.ParamReference, _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "POSTING");
                                return Request.CreateResponse(HttpStatusCode.OK, "Posting Cashier Receipt Success");
                            }

                            if (PermissionID == "CashierReceipt_Revise")
                            {

                                _cashierReps.Cashier_Revise(_cashier);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Revise Cashier Receipt Success:" + _cashier.ParamReference, _Obj, "", param1, _cashier.CashierPK, _cashier.HistoryPK, 0, "REVISE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Revise Cashier Receipt Success");
                            }

                            //BOOK TRANSFER
                            //if (PermissionID == "CashierBookTransfer_U")
                            //{
                            //    _cashierReps.Cashier_Update(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Update Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Update Cashier Book Transfer Success");
                            //}
                            //if (PermissionID == "CashierBookTransfer_A")
                            //{
                            //    _cashierReps.Cashier_Approved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Approved Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Approved Cashier Book Transfer Success");
                            //}
                            //if (PermissionID == "CashierBookTransfer_V")
                            //{
                            //    _cashierReps.Cashier_Void(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Void Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Void Cashier Book Transfer Success");
                            //}
                            //if (PermissionID == "CashierBookTransfer_R")
                            //{
                            //    _cashierReps.Cashier_Reject(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Reject Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Reject Cashier Book Transfer Success");
                            //}
                            //if (PermissionID == "CashierBookTransfer_I")
                            //{
                            //    CashierAddNew _c = new CashierAddNew();
                            //    _c = _cashierReps.Cashier_Add(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Insert Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, _c);
                            //}
                            //if (PermissionID == "CashierBookTransfer_UnApproved")
                            //{
                            //    _cashierReps.Cashier_UnApproved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "UnApproved Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Cashier Book Transfer Success");
                            //}

                            //if (PermissionID == "CashierBookTransfer_Posting")
                            //{

                            //    _cashierReps.Cashier_Posting(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Posting Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Posting Cashier Book Transfer Success");
                            //}

                            //if (PermissionID == "CashierBookTransfer_Revise")
                            //{

                            //    _cashierReps.Cashier_Revise(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Revise Cashier Book Transfer Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Revise Cashier Book Transfer Success");
                            //}

                            ////ACCOUNT RECEIVABLE
                            //if (PermissionID == "CashierAccountReceivable_U")
                            //{
                            //    _cashierReps.Cashier_Update(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Update Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Update Cashier Account Receivable Success");
                            //}
                            //if (PermissionID == "CashierAccountReceivable_A")
                            //{
                            //    _cashierReps.Cashier_Approved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Approved Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Approved Cashier Account Receivable Success");
                            //}
                            //if (PermissionID == "CashierAccountReceivable_V")
                            //{
                            //    _cashierReps.Cashier_Void(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Void Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Void Cashier Account Receivable Success");
                            //}
                            //if (PermissionID == "CashierAccountReceivable_R")
                            //{
                            //    _cashierReps.Cashier_Reject(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Reject Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Reject Cashier Account Receivable Success");
                            //}
                            //if (PermissionID == "CashierAccountReceivable_I")
                            //{
                            //    CashierAddNew _c = new CashierAddNew();
                            //    _c = _cashierReps.Cashier_Add(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Insert Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, _c);
                            //}
                            //if (PermissionID == "CashierAccountReceivable_UnApproved")
                            //{
                            //    _cashierReps.Cashier_UnApproved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "UnApproved Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Cashier Account Receivable Success");
                            //}


                            //if (PermissionID == "CashierAccountReceivable_Posting")
                            //{

                            //    _cashierReps.Cashier_Posting(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Posting Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Posting Cashier Account Receivable Success");
                            //}

                            //if (PermissionID == "CashierAccountReceivable_Revise")
                            //{

                            //    _cashierReps.Cashier_Revise(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Revise Cashier Account Receivable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Revise Cashier Account Receivable Success");
                            //}

                            ////ACCOUNT PAYABLE
                            //if (PermissionID == "CashierAccountPayable_U")
                            //{
                            //    _cashierReps.Cashier_Update(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Update Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Update Cashier Account Payable Success");
                            //}
                            //if (PermissionID == "CashierAccountPayable_A")
                            //{
                            //    _cashierReps.Cashier_Approved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Approved Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Approved Cashier Account Payable Success");
                            //}
                            //if (PermissionID == "CashierAccountPayable_V")
                            //{
                            //    _cashierReps.Cashier_Void(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Void Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Void Cashier Account Payable Success");
                            //}
                            //if (PermissionID == "CashierAccountPayable_R")
                            //{
                            //    _cashierReps.Cashier_Reject(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Reject Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Reject Cashier Account Payable Success");
                            //}
                            //if (PermissionID == "CashierAccountPayable_I")
                            //{
                            //    CashierAddNew _c = new CashierAddNew();
                            //    _c = _cashierReps.Cashier_Add(_cashier, havePrivillege);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Insert Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, _c);
                            //}
                            //if (PermissionID == "CashierAccountPayable_UnApproved")
                            //{
                            //    _cashierReps.Cashier_UnApproved(_cashier);
                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "UnApproved Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "UnApproved Cashier Account Payable Success");
                            //}

                            //if (PermissionID == "CashierAccountPayable_Posting")
                            //{

                            //    _cashierReps.Cashier_Posting(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Posting Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Posting Cashier Account Payable Success");
                            //}

                            //if (PermissionID == "CashierAccountPayable_Revise")
                            //{

                            //    _cashierReps.Cashier_Revise(_cashier);


                            //    _activityReps.Activity_Insert(DateTime.Now, _host.Get_PermissionPK(PermissionID), true, "Revise Cashier Account Payable Success", "", "", _host.Get_UsersPK(param1));
                            //    return Request.CreateResponse(HttpStatusCode.OK, "Revise Cashier Account Payable Success");
                            //}


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

        [HttpPost]
        public HttpResponseMessage PaymentVoucher(string param1, string param2, [FromBody]Cashier _cashier)
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
                            // check permission ama Masukin Log Disini
                            if (_customClient01.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "05")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient05.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "12")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient12.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "14")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient14.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "19")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient19.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "22")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient22.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else
                        {
                            // check permission ama Masukin Log Disini
                            if (_cashierReps.Payment_Voucher(param1, _cashier))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "PaymentVoucher_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Payment Voucher", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ReceiptVoucher(string param1, string param2, [FromBody]Cashier _cashier)
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
                            // check permission ama Masukin Log Disini
                            if (_customClient01.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "05")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient05.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "12")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient12.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "14")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient14.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else if (Tools.ClientCode == "19")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient19.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }

                        else if (Tools.ClientCode == "22")
                        {
                            // check permission ama Masukin Log Disini
                            if (_customClient22.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
                            }
                        }
                        else
                        {
                            // check permission ama Masukin Log Disini
                            if (_cashierReps.Receipt_Voucher(param1, _cashier))
                            {

                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "ReceiptVoucher_" + param1 + ".pdf");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Payment Voucher", param1, 0, 0, 0, "");
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
      * param3 = Type
      */
        [HttpGet]
        public HttpResponseMessage GetReferenceDetail(string param1, string param2, int param3, string param4, string param5, string param6, string param7)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                string Reference = param4 + '/' + param5 + '/' + param6;
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.ReferenceDetail(param3, Reference, param7));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Reference Select From Cashier By Type", param1, 0, 0, 0, "");
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
     * param3 = Type
     */
        [HttpGet]
        public HttpResponseMessage GetCashierIDDetail(string param1, string param2, long param3, int param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.CashierIDDetail(param3,param4,param5));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Cashier ID Select Detail", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckPostingCashier(string param1, string param2, DateTime param3, DateTime param4, string param5)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_CheckPostingCashier(param3, param4, param5));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Posting Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckPostingCashierById(string param1, string param2, DateTime param3, DateTime param4, string param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_CheckPostingCashierByCashierID(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Posting Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckPostingBySelectedCashierByReference(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_CheckPostingBySelectedCashierByReference(param3, param4, param5, _paramCashierBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Posting Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckPostingCashierByReference(string param1, string param2, DateTime param3, DateTime param4, string param5, int param6)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_CheckPostingCashierByReference(param3, param4, param5, param6));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Posting Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Cashier_ValidateOtherTransactionBySelected(string param1, string param2, string param3, [FromBody]ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_OtherTransactionBySelected(param3, _paramCashierBySelected));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Other Transactio Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage Cashier_ValidateOtherTransaction(string param1, string param2, int param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _cashierReps.Validate_OtherTransaction(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Other Transactio Cashier", param1, 0, 0, 0, "");
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
        public HttpResponseMessage ValidateCheckStatusPosting(string param1, string param2, DateTime param3, string param4)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _customClient12.Validate_CheckStatusPosting(param3, param4));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Validate Check Posting Cashier", param1, 0, 0, 0, "");
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
