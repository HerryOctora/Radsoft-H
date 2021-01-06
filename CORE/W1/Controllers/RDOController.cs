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
    public class RDOController : ApiController
    {
        static readonly string _Obj = "RDO Controller";
        static readonly AccountReps _accountReps = new AccountReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();
        static readonly RDOReps _rdo = new RDOReps();
        static readonly string _key = "RDOAPI";

        public class MessageInternalServerError
        {
            public int code { get; set; }
            public string message { get; set; } 
            public string error { get; set; }

        }

         public class RDOPeriod
        {
            public string ID { get; set; }
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public string Description { get; set; }
        }

         [HttpGet]
         public HttpResponseMessage GetMasterFundByFundCode(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterFundByFundCode(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterFundByFundCode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterFundByFundCode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMatriksSwitchingByFundCode(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMatriksSwithingByFundCode(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMatriksSwitchingByFundCode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMatriksSwitchingByFundCode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpPost]
         public HttpResponseMessage UserProfileUpdateIndividual(string param1, [FromBody]List<AddProfile> _rdoAddProfile)
         {

             if (param1 == _key)
             {
                 return Request.CreateResponse(HttpStatusCode.OK, _rdo.RDOUpdateProfile(_rdoAddProfile));
                 //return Request.CreateResponse(HttpStatusCode.OK, "OK");
             }
             else
             {
                 //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddProfile", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage RDOAddPeriod(string param1, [FromBody]List<RDOPeriod> RDOPeriod)
         {

             if (param1 == _key)
             {
                 //return Request.CreateResponse(HttpStatusCode.OK, _rdo.RDOAddPeriod(_period));
                 return Request.CreateResponse(HttpStatusCode.OK, "OK");
             }
             else
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddPeriod", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage UserProfileAddIndividual(string param1, [FromBody]List<AddProfile> _rdoAddProfile)
         {

             if (param1 == _key)
             {
                 var result = _rdo.RDOAddProfile(_rdoAddProfile);
                 var _resultdata = result.data;
                 var _resultdatavalidate = result.dataValidate;
                 if (_resultdatavalidate != null)
                 {
                     if (result.dataValidate.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data Error : " + result.dataValidate.Count.ToString(), _Obj, "RDO Add Profile", Tools._userAPI, 0, 0, 0, "");
                     }


                 }
                 else if (_resultdata != null)
                 {
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO Add Profile", Tools._userAPI, 0, 0, 0, "");
                     }


                 }
                 return Request.CreateResponse(HttpStatusCode.OK, result);
                 //return Request.CreateResponse(HttpStatusCode.OK, "OK");
             }
             else
             {
                 //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddProfile", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage RDOAddProfile(string param1, [FromBody]List<AddProfile> _rdoAddProfile)
         {

             if (param1 == _key)
             {
                 var result = _rdo.RDOAddProfile(_rdoAddProfile);
                 var _resultdata = result.data;
                 var _resultdatavalidate = result.dataValidate;
                 if (_resultdatavalidate != null)
                 {
                     if (result.dataValidate.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data Error : " + result.dataValidate.Count.ToString(), _Obj, "RDO Add Profile", Tools._userAPI, 0, 0, 0, "");
                     }
                     
                     
                 }
                 else if (_resultdata != null)
                {
                    if (result.data.Count > 0)
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO Add Profile", Tools._userAPI, 0, 0, 0, "");
                    }


                } 
                 return Request.CreateResponse(HttpStatusCode.OK, result);
                 
             }
             else
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, Tools.NoPermissionLogMessage, _Obj, "RDO Add Profile", Tools._userAPI, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage ValidateTransaction(string param1, [FromBody]AddTransaction _rdoAddTransaction)
         {

             if (param1 == _key)
             {
                 var result = _rdo.ValidateTransaction(_rdoAddTransaction);
                 if (result.data.Count > 0)
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO Validate Transaction", Tools._userAPI, 0, 0, 0, "");
                 }
                 return Request.CreateResponse(HttpStatusCode.OK, _rdo.ValidateTransaction(_rdoAddTransaction));
             }
             else
             {
                 //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddTransaction", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage RDOAddTransaction(string param1, [FromBody]List<AddTransaction> _rdoAddTransaction)
         {

             if (param1 == _key)
             {
                 var result = _rdo.RDOAddTransaction(_rdoAddTransaction);
                 var _resultdata = result.data;

                 if (_resultdata != null)
                 {
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO Add Transaction", Tools._userAPI, 0, 0, 0, "");
                     }


                 }
                 return Request.CreateResponse(HttpStatusCode.OK, result);
               
             }
             else
             {
                 //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddTransaction", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpPost]
         public HttpResponseMessage RDOGetExistingClient(string param1, [FromBody]List<GetExistingClient> _rdoAddProfile)
         {

             if (param1 == _key)
             {
                 var result = _rdo.GetExistingClient(_rdoAddProfile);
                 var _resultdata = result.data;
                 var _resultdatavalidate = result.dataReturn;
                 if (_resultdatavalidate != null)
                 {
                     if (result.dataReturn.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data : " + result.dataReturn.Count.ToString(), _Obj, "RDO Existing Client", Tools._userAPI, 0, 0, 0, "");
                     }


                 }
                 else 
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "No Data To Present", _Obj, "RDO Existing Client", Tools._userAPI, 0, 0, 0, "");
                 } 

                 return Request.CreateResponse(HttpStatusCode.OK, result);
                 //return Request.CreateResponse(HttpStatusCode.OK, "OK");
             }
             else
             {
                 //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "RDOAddProfile", param1, 0, 0, 0, "");
                 return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
             }
         }

         [HttpGet]
         public HttpResponseMessage GetMasterBank(string param1)
         {
             
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterBank();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterBank", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterBank", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterCurrency(string param1)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterCurrency();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterCurrency", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterCurrency", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }


         [HttpGet]
         public HttpResponseMessage GetMasterFund(string param1)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterFund();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterFund", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterFund", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }


         [HttpGet]
         public HttpResponseMessage GetMatriksSwitching(string param1)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMatriksSwithing();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMatriksSwitching", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMatriksSwitching", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterValueForProfileWithLang(string param1, string param2, string param3)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterValueWithLang(param2,param3);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterValue", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterValue For Profile With Lang", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterValueForProfile(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterValue(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterValue", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterValue", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterValue(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterValue(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterValue", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterValue", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterValueAllWithLang(string param1,string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterValueAllWithLang(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterValueAll", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterValueAll With Lang", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetMasterValueAll(string param1)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetMasterValueAll();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetMasterValueAll", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetMasterValueAll", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetAllClientBalanceFromTo(string param1, DateTime param2, DateTime param3)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllClientBalanceFromTo(param2, param3);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetClientBalanceFromTo", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetClientBalanceFromTo", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetClientBalanceFromToByIFUACode(string param1, string param2, DateTime param3, DateTime param4)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllClientBalanceFromToByIFUACode(param2, param3, param4);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetClientBalanceFromToByIFUACode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetClientBalanceFromToByIFUACode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetNAVFromTo(string param1, DateTime param2, DateTime param3)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllNAVFromTo(param2, param3);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetNAVFromTo", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetNAVFromTo", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetNAVFromToByFundCode(string param1, string param2, DateTime param3, DateTime param4)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllNAVFromToByFundCode(param2, param3, param4);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetNAVFromToByFundCode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetNAVFromToByFundCode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetAllTransactionStatus(string param1)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllTransactionStatus();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetAllTransactionStatus", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetAllTransactionStatus", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetAllTransactionStatusFromTo(string param1, DateTime param2, DateTime param3)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetAllTransactionStatusFromTo(param2, param3);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetAllTransactionStatusFromTo", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetAllTransactionStatusFromTo", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetTransactionStatusByTrxID(string param1, string param2)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetTransactionStatusByTrxID(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetTransactionStatusByTrxID", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetTransactionStatusByTrxID", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage GetAllTransactionByIFUACode(string param1, string param2)
         {

             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.GetTransactioByIFUACode(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO GetTransactionStatusBy IFUACode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO GetTransactionStatusBy IFUACode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage VerifyAllClient(string param1)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.VerifyAllClient();
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO VerifyAllClient", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO VerifyAllClient", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage VerifyClientByIFUACode(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.VerifyClientByIFUACode(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO VerifyClientByIFUACode", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO VerifyClientByIFUACode", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage VerifyClientByEmail(string param1, string param2)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.VerifyClientByEmail(param2);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO VerifyClientByEmail", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO VerifyClientByEmail", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }

         [HttpGet]
         public HttpResponseMessage VerifyClientByLastUpdateFromTo(string param1, DateTime param2, DateTime param3)
         {
             try
             {
                 if (param1 == _key)
                 {
                     var result = _rdo.VerifyClientByLastUpdateFromTo( param2, param3);
                     if (result.data.Count > 0)
                     {
                         _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", true, "Total Data: " + result.data.Count.ToString(), _Obj, "RDO VerifyClientByLastUpdateFromTo", Tools._userAPI, 0, 0, 0, "");
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 else
                 {
                     _activityReps.Activity_LogInsert(DateTime.Now, "RDO_API", false, Tools.NoPermissionLogMessage, _Obj, "RDO VerifyClientByLastUpdateFromTo", Tools._userAPI, 0, 0, 0, "");
                     return Request.CreateResponse(HttpStatusCode.Unauthorized, Tools.NoPermissionMessage);
                 }
             }
             catch (Exception err)
             {
                 _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, Tools._userAPI, 0, 0, 0, "");
                 MessageInternalServerError _e = new MessageInternalServerError();
                 _e.code = 500;
                 _e.message = "internal server error";
                 _e.error = err.Message + " | " + err.StackTrace;
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, _e);
             }

         }
    }
}
