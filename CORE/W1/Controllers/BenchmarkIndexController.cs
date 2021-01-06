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
    public class BenchmarkIndexController : ApiController
    {
        static readonly string _Obj = "Benchmark Index Controller";
        static readonly BenchmarkIndexReps _BenchmarkIndexReps = new BenchmarkIndexReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly Host _host = new Host();


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

                        return Request.CreateResponse(HttpStatusCode.OK, _BenchmarkIndexReps.BenchmarkIndex_SelectByDateFromTo(param3, param4, param5));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]BenchmarkIndex _BenchmarkIndex)
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
                            if (PermissionID == "BenchmarkIndex_U")
                            {
                                int _newHisPK = _BenchmarkIndexReps.BenchmarkIndex_Update(_BenchmarkIndex, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Benchmark Index Success", _Obj, "", param1, _BenchmarkIndex.BenchmarkIndexPK, _BenchmarkIndex.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Benchmark Index Success");
                            }
                            if (PermissionID == "BenchmarkIndex_A")
                            {
                                _BenchmarkIndexReps.BenchmarkIndex_Approved(_BenchmarkIndex);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Benchmark Index Success", _Obj, "", param1, _BenchmarkIndex.BenchmarkIndexPK, _BenchmarkIndex.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Benchmark Index Success");
                            }
                            if (PermissionID == "BenchmarkIndex_V")
                            {
                                _BenchmarkIndexReps.BenchmarkIndex_Void(_BenchmarkIndex);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Benchmark Index Success", _Obj, "", param1, _BenchmarkIndex.BenchmarkIndexPK, _BenchmarkIndex.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Benchmark Index Success");
                            }
                            if (PermissionID == "BenchmarkIndex_R")
                            {
                                _BenchmarkIndexReps.BenchmarkIndex_Reject(_BenchmarkIndex);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Benchmark Index Success", _Obj, "", param1, _BenchmarkIndex.BenchmarkIndexPK, _BenchmarkIndex.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Benchmark Index Success");
                            }
                            if (PermissionID == "BenchmarkIndex_I")
                            {
                                int _lastPKByLastUpdate = _BenchmarkIndexReps.BenchmarkIndex_Add(_BenchmarkIndex, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Benchmark Index Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Benchmark Index Success");
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
        public HttpResponseMessage ApproveBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody] BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string PermissionID = "BenchmarkIndex_ApproveBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {

                        _BenchmarkIndexReps.BenchmarkIndex_ApproveBySelected(param1, PermissionID, param3, param4, _BenchmarkIndex);
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


        [HttpGet]
        public HttpResponseMessage CheckHasAdd(string param1, string param2, string param3, string param4)
        {
            try
            {


                return Request.CreateResponse(HttpStatusCode.OK, _BenchmarkIndexReps.CheckHassAdd(param3, param4));


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
        [HttpPost]
        public HttpResponseMessage RejectBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody] BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string PermissionID = "BenchmarkIndex_RejectBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _BenchmarkIndexReps.BenchmarkIndex_RejectBySelected(param1, PermissionID, param3, param4, _BenchmarkIndex);
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
        public HttpResponseMessage VoidBySelectedData(string param1, string param2, DateTime param3, DateTime param4, [FromBody] BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string PermissionID = "BenchmarkIndex_VoidBySelected";
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        _BenchmarkIndexReps.BenchmarkIndex_VoidBySelected(param1, PermissionID, param3, param4, _BenchmarkIndex);
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
        public HttpResponseMessage Approve1BySelectedData(string param1, string param2, DateTime param3, DateTime param4, string param5, [FromBody] BenchmarkIndex _BenchmarkIndex)
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
                            bool havePrivillege = _host.Get_Privillege(param1, PermissionID);
                            if (PermissionID == "BenchmarkIndex_Approve1BySelected")
                            {
                                _BenchmarkIndexReps.BenchmarkIndex_Approve1BySelected(param1, PermissionID, param3, param4, _BenchmarkIndex);
                                return Request.CreateResponse(HttpStatusCode.OK, "Approve 1 All By Selected Success");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Approved 1 By Selected Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetBenchmarkIndexCombo(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _BenchmarkIndexReps.BenchmarkIndex_Combo());
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Market Combo", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetBIRate(string param1, string param2, DateTime param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _customClient20.BIRate_Combo(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Market Combo", param1, 0, 0, 0, "");
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
