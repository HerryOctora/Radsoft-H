
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

namespace W1.Controllers
{
    public class BudgetController : ApiController
    {
        static readonly string _Obj = "Budget Controller";
        static readonly BudgetReps _BudgetReps = new BudgetReps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();


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
                        return Request.CreateResponse(HttpStatusCode.OK, _BudgetReps.Budget_Select(param3));
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        /*
         * param1 = userID
         * param2 = sessionID
         * param3 = permissionID
         */
        [HttpPost]
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]Budget _Budget)
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
                            if (PermissionID == "Budget_U")
                            {
                                int _newHisPK = _BudgetReps.Budget_Update(_Budget, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update Budget Success", _Obj, "", param1, _Budget.BudgetPK, _Budget.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update Budget Success");
                            }
                            if (PermissionID == "Budget_A")
                            {
                                _BudgetReps.Budget_Approved(_Budget);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved Budget Success", _Obj, "", param1, _Budget.BudgetPK, _Budget.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved Budget Success");
                            }
                            if (PermissionID == "Budget_V")
                            {
                                _BudgetReps.Budget_Void(_Budget);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void Budget Success", _Obj, "", param1, _Budget.BudgetPK, _Budget.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void Budget Success");
                            }
                            if (PermissionID == "Budget_R")
                            {
                                _BudgetReps.Budget_Reject(_Budget);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject Budget Success", _Obj, "", param1, _Budget.BudgetPK, _Budget.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject Budget Success");
                            }
                            if (PermissionID == "Budget_I")
                            {
                                int _lastPKByLastUpdate = _BudgetReps.Budget_Add(_Budget, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add Budget Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert Budget Success");
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage CheckHasAdd(string param1, string param2, int param3, int param4)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _BudgetReps.CheckHassAdd(param3, param4));
            }
            catch (Exception err)
            {
                _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage BudgetReport(string param1, string param2, [FromBody]BudgetRpt _BudgetRpt)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        if (_BudgetReps.GenerateReportBudget(param1, _BudgetRpt))
                        {
                            if (_BudgetRpt.ReportName == "01 Laporan Ledger Budget")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanLedgerBudgetRpt_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "02 Laporan Per Coa Per Cabang")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanPerCoaPerCabangRpt_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "03 Laporan FundFlow Cabang")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanFundFlowCabang_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "04 Laporan FundFlow Per Sales")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanFundFlowPerSales_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "05 Laporan FundFlow Summary")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanFundFlowSummary_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "06 Laporan AUM Cabang dan Sales")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanAUMCabangdanSales_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "07 Laporan Aum Summary")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanAumSummary_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "08 Laporan MI Fee Cabang dan Sales")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanMIFeeCabangdanSales_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "09 Laporan MI Fee Summary")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanMIFeeSummary_" + param1 + ".xlsx");
                            }
                            else if (_BudgetRpt.ReportName == "10 Laporan Profit And Loss")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlReportsPath + "LaporanProfitAndLoss_" + param1 + ".xlsx");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.CreateReportFailedOrNoData);
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Accounting Report", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message);
            }
        }
    }
}
