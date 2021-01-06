using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Threading;
using RFSRepository;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;

namespace W1.Controllers
{
    public class UploadController : ApiController
    {

        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly CustomClient03Reps _customClient03 = new CustomClient03Reps();
        static readonly CustomClient05Reps _customClient05 = new CustomClient05Reps();
        static readonly CustomClient20Reps _customClient20 = new CustomClient20Reps();
        static readonly CustomClient24Reps _customClient24 = new CustomClient24Reps();
        static readonly CustomClient25Reps _customClient25 = new CustomClient25Reps();
        static readonly CustomClient29Reps _customClient29 = new CustomClient29Reps();
        static readonly BudgetSummaryReps _budgetSummary = new BudgetSummaryReps();
        static readonly FFSSetup_02Reps _FFSSetup_02Reps = new FFSSetup_02Reps();
        static readonly Host _host = new Host();

        string[] fileTypeFilter = { ".xls", ".xlsx", ".pdf", ".dbf", ".txt", ".csv", ".jpeg", ".png", ".jpg" };

        string _messageFileTypeFilter = "Extention File that allowed : xls,xlsx,pdf,dbf,txt,csv,jpeg,png,jpg";

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = PermissionID
        * param4 = otherParam
        */
        public Task<HttpResponseMessage> UploadData(string param1, string param2, string param3)
        {
            string _msg = "Import File";
            //bool _msgExisting = false;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = Tools.root;
            string storage = Tools.storage;
            var provider = new MultipartFormDataStreamProvider(root);
            string Action;
            string _ObjStackTrace = "Upload Controller";
            // Read the form data and return an async task.
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    try
                    {
                        _activityReps.Activity_Insert(DateTime.Now, param3, true, _msg, "", "", param1);
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, "Trade is Faulted / Canceled", _ObjStackTrace, "", param1);
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }
                        bool session = Tools.SessionCheck(param1, param2);
                        if (!session)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoSessionLogMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoSessionMessage);
                        }
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (!havePermission)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoPermissionMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                        }
                        foreach (MultipartFileData FileD in provider.FileData)
                        {
                            bool flagFile = false;
                            if (string.IsNullOrEmpty(FileD.Headers.ContentDisposition.FileName))
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                            }

                            string fileName = FileD.Headers.ContentDisposition.FileName;
                            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                            {
                                fileName = fileName.Trim('"');
                            }

                            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                            {
                                fileName = Path.GetFileName(fileName);
                            }

                            string checkExt = fileName.GetRightString(fileName.Length - fileName.IndexOf("."));

                            foreach (string check in fileTypeFilter)
                            {
                                if (checkExt.ToLower() == check)
                                {
                                    flagFile = true;
                                }
                            }

                            if (!flagFile)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _messageFileTypeFilter);
                            }

                            Action = FileD.Headers.ContentDisposition.Name;

                            if (Action.StartsWith("\"") && Action.EndsWith("\""))
                            {
                                Action = Action.Trim('"');
                            }

                            if (Action.Contains(@"/") || Action.Contains(@"\"))
                            {
                                Action = Path.GetFileName(Action);
                            }

                            if (Action == "ClosePrice")
                            {
                                fileName = "CP" + Tools.GetRightString(fileName, 10);
                                if (File.Exists(Tools.DBFFilePath + fileName))
                                {
                                    File.Delete(Tools.DBFFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                ClosePriceReps _cR = new ClosePriceReps();

                                _msg = _cR.ImportClosePrice(fileName, param1, Tools.GetSubstring(fileName, 4, 2) + "/" + Tools.GetSubstring(fileName, 6, 2) + "/" + Tools.GetSubstring(fileName, 2, 2));

                            }
                            else if (Action == "ImportHaircutMKBD")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                HaircutMKBDReps _cR = new HaircutMKBDReps();
                                _msg = _cR.ImportHaircutMKBD(fileName, param1);


                            }
                            else if (Action == "UnitReconcile")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    UnitReconcileReps _uR = new UnitReconcileReps();
                                    _msg = _uR.UnitReconcileTempImport(Tools.ExcelFilePath + fileName, param1);

                                    if (Tools.ClientCode == "03")
                                    {
                                        UnitReconcileReps _uR1 = new UnitReconcileReps();
                                        _msg = _uR.FundClientPositionForAPERDImport(Tools.ExcelFilePath + fileName, param1);
                                    }
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "NavReconcile")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    CloseNavReps _cN = new CloseNavReps();
                                    _msg = _cN.NavReconcileImport(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "HaircutMKBD")
                            {
                                fileName = "haircutMKBD" + Tools.GetRightString(fileName, 12);
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                ClosePriceReps _cR = new ClosePriceReps();
                                _msg = _cR.ImportHaircutMKBD(fileName, param1, Tools.GetSubstring(fileName, 13, 2) + "/" + Tools.GetSubstring(fileName, 11, 2) + "/" + Tools.GetSubstring(fileName, 17, 2));

                            }

                            else if (Action == "Journal")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    JournalReps _jR = new JournalReps();
                                    if (Tools.ClientCode == "05")
                                    {
                                        CustomClient05Reps _customClient05 = new CustomClient05Reps();
                                        _msg = _customClient05.JournalImport(param1, Tools.ExcelFilePath + fileName);
                                    }
                                    else if (Tools.ClientCode == "25")
                                    {
                                        CustomClient25Reps _customClient25 = new CustomClient25Reps();
                                        _msg = _customClient25.JournalImport(param1, Tools.ExcelFilePath + fileName);
                                    }
                                    else
                                    {
                                        _msg = _jR.JournalImport(param1, Tools.ExcelFilePath + fileName);
                                    }


                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "TBReconcileTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    TBReconcileTempReps _tBR = new TBReconcileTempReps();
                                    _msg = _tBR.TBReconcileTempImport(Tools.ExcelFilePath + fileName, param1, Tools.GetSubstring(fileName, 0, 2) + "/" + Tools.GetSubstring(fileName, 2, 2) + "/" + Tools.GetSubstring(fileName, 4, 4));

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "UpdatePaymentSInvestTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    UpdatePaymentSInvestTempReps _jR = new UpdatePaymentSInvestTempReps();
                                    _msg = _jR.UpdatePaymentSInvestTempImportSwitching(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "DailyTransactionForInvestment")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    DailyTransactionForInvestmentReps _sI = new DailyTransactionForInvestmentReps();
                                    _msg = _sI.DailyTransactionForInvestmentImportSwitching(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "DailyTransactionForInvestmentSubRedTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    DailyTransactionForInvestmentReps _sI = new DailyTransactionForInvestmentReps();
                                    _msg = _sI.DailyTransactionForInvestmentImportSubsRed(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "ImportAPERDSummary")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                UpdatePaymentSInvestTempReps _cR = new UpdatePaymentSInvestTempReps();
                                _msg = _cR.ImportAPERDSummary(fileName, param1);


                            }

                            else if (Action == "ScheduleOfCashDividend")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ScheduleOfCashDividend(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            else if (Action == "ClosePriceBond")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();
                                    _msg = _cPR.ImportClosePriceBond(Tools.ExcelFilePath + fileName, param1, Tools.GetSubstring(fileName, 6, 2) + "/" + Tools.GetSubstring(fileName, 8, 2) + "/" + Tools.GetSubstring(fileName, 10, 2));
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else if (Action == "CloseNav")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    CloseNavReps _cNR = new CloseNavReps();
                                    _msg = _cNR.ImportCloseNav(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "CurrencyRate")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    CurrencyRateReps _cNR = new CurrencyRateReps();
                                    _msg = _cNR.ImportCurrencyRate(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "PortfolioForFFS")
                            {
                                fileName = "Holding" + Tools.GetRightString(fileName, 13);
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    FFSForOJKReps _cNR = new FFSForOJKReps();
                                    _msg = _cNR.ImportPortfolioForFFS(Tools.ExcelFilePath + fileName, param1, Tools.GetSubstring(fileName, 7, 2) + "/" + Tools.GetSubstring(fileName, 9, 2) + "/" + Tools.GetSubstring(fileName, 11, 4));
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "AUM")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    AUMReps _AUM = new AUMReps();
                                    _msg = _AUM.ImportAUM(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "Cashier")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    CashierReps _cR = new CashierReps();
                                    _msg = _cR.Cashier_Import(Tools.ExcelFilePath + fileName);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "InstrumentIndex")
                            {
                                string fileNameDate = "";
                                fileNameDate = Tools.GetRightString(fileName, 10);
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    InstrumentIndexReps _cPR = new InstrumentIndexReps();
                                    _msg = _cPR.ImportInstrumentIndex(Tools.ExcelFilePath + fileName, Tools.GetSubstring(fileNameDate, 2, 2) + "/" + Tools.GetSubstring(fileNameDate, 4, 2) + "/" + Tools.GetSubstring(fileNameDate, 0, 2), fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "InstrumentSyariah")
                            {
                                string fileNameDate = "";
                                fileNameDate = Tools.GetRightString(fileName, 10);
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    InstrumentSyariahReps _cPR = new InstrumentSyariahReps();
                                    _msg = _cPR.ImportInstrumentSyariah(Tools.ExcelFilePath + fileName, Tools.GetSubstring(fileNameDate, 2, 2) + "/" + Tools.GetSubstring(fileNameDate, 4, 2) + "/" + Tools.GetSubstring(fileNameDate, 0, 2), fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "Instrument")
                            {

                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".txt")
                                {
                                    InstrumentReps _cR = new InstrumentReps();
                                    _msg = _cR.ImportInstrument(fileName, param1);
                                }
                                else if (Tools.ClientCode == "25" && (ext == ".xls" || ext == ".xlsx"))
                                {
                                    CustomClient25Reps _cR = new CustomClient25Reps();
                                    _msg = _cR.ImportInstrument(Tools.ExcelFilePath + fileName, param1);
                                }

                            }

                            else if (Action == "BlackListName")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BlackListNameReps _bLNR = new BlackListNameReps();
                                    _msg = _bLNR.ImportBlackListName(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "IBPA")
                            {

                                if (File.Exists(Tools.CSVFilePath + fileName))
                                {
                                    File.Delete(Tools.CSVFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.CSVFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.CSVFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();
                                    _msg = _cPR.ImportIBPA(Tools.CSVFilePath + fileName, param1, Tools.GetSubstring(fileName, 6, 2) + "/" + Tools.GetSubstring(fileName, 8, 2) + "/" + Tools.GetSubstring(fileName, 10, 2));
                                }
                                else if (ext == ".txt")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();
                                    _msg = _cPR.ImportIBPA_Text(fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "OldIBPA")
                            {

                                if (File.Exists(Tools.CSVFilePath + fileName))
                                {
                                    File.Delete(Tools.CSVFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.CSVFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.CSVFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();
                                    _msg = _cPR.ImportOldIBPA(Tools.CSVFilePath + fileName, param1);
                                }
                                else if (ext == ".txt")
                                {
                                    if (Tools.ClientCode == "21")
                                    {
                                        CustomClient21Reps _cPR = new CustomClient21Reps();
                                        _msg = _cPR.ImportOldIBPA_Text(fileName, param1);
                                    }
                                    else
                                    {
                                        _msg = "This request is not properly formatted";
                                    }
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else if (Action == "UpdateSIDTemp")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                if (Tools.ClientCode == "20")
                                {
                                    //custom upload
                                    _customClient20.SIDImport(fileName, param1);
                                }
                                else if (Tools.ClientCode == "29")
                                {
                                    //custom upload
                                    _customClient29.SIDImport(fileName, param1);
                                }
                                else
                                {
                                    FundClientReps _cR = new FundClientReps();
                                    _msg = _cR.SIDImport(fileName, param1);
                                }


                            }

                            else if (Action == "UpdateFailedSIDTemp")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                FundClientReps _cR = new FundClientReps();
                                _msg = _cR.SIDFailedGenerateImport(Tools.ExcelFilePath + fileName, param1);

                            }



                            else if (Action == "UpdateBanksTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    FundClientReps _nC = new FundClientReps();
                                    _msg = _nC.BanksImport(Tools.ExcelFilePath + fileName, param1, Tools.GetSubstring(fileName, 6, 2) + "/" + Tools.GetSubstring(fileName, 8, 2) + "/" + Tools.GetSubstring(fileName, 10, 2));
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "UpdatePaymentSInvestSubRedTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    UpdatePaymentSInvestTempReps _sI = new UpdatePaymentSInvestTempReps();
                                    _msg = _sI.UpdatePaymentSInvestTempImportSubsRed(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "InternalClosePrice")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    UpdateClosePriceReps _cPR = new UpdateClosePriceReps();
                                    _msg = _cPR.InternalClosePriceImport(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "TransactionPromo")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    ClientSubscriptionReps _nC = new ClientSubscriptionReps();
                                    _msg = _nC.TransactionPromoImport(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "PromoMoInvest")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    ClientSubscriptionReps _nC = new ClientSubscriptionReps();
                                    _msg = _nC.PromoMoInvestImport(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "FxdImport")
                            {
                                string ext = Path.GetExtension(Tools.CSVFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.CSVFilePath + fileName))
                                    {
                                        File.Delete(Tools.CSVFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.CSVFilePath + fileName);

                                    } while (_fileReady == false);

                                    CloseNavReps _cPR = new CloseNavReps();
                                    _msg = _cPR.FxdImportCsv(Tools.CSVFilePath + fileName, param1);

                                }
                                else if (ext == ".txt")
                                {
                                    if (File.Exists(Tools.TxtFilePath + fileName))
                                    {
                                        File.Delete(Tools.TxtFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                    } while (_fileReady == false);

                                    CloseNavReps _cR = new CloseNavReps();
                                    _msg = _cR.FxdImport(fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "FxdImport14")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                ImportFxdReps _cR = new ImportFxdReps();
                                _msg = _cR.FxdImport14(fileName, param1);

                            }

                            else if (Action == "UnitReconcileTxt")
                            {

                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                UnitReconcileReps _cR = new UnitReconcileReps();
                                _msg = _cR.UnitReconcileTempImportFromTxt(fileName, param1);
                            }

                            else if (Action == "UnitReconcileTxtAPERD")
                            {

                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                } while (_fileReady == false);

                                UnitReconcileReps _cR = new UnitReconcileReps();
                                _msg = _cR.UnitReconcileTempImportFromTxtAPERD(fileName, param1);
                            }


                            else if (Action == "CloseNavSInvest")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);

                                    {
                                        CloseNavReps _cNR = new CloseNavReps();
                                        _msg = _cNR.ImportCloseNavSInvestFromExcel(Tools.ExcelFilePath + fileName, param1);
                                    }
                                }

                                if (ext == ".txt")
                                {
                                    if (File.Exists(Tools.TxtFilePath + fileName))
                                    {
                                        File.Delete(Tools.TxtFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                    } while (_fileReady == false);

                                    CloseNavReps _cR = new CloseNavReps();
                                    _msg = _cR.CloseNavSInvestFromTxt(fileName, param1);
                                }

                            }
                            else if (Action == "ClosePriceReksadana")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();
                                    _msg = _cPR.ImportClosePriceReksadana(Tools.ExcelFilePath + fileName, param1, Tools.GetSubstring(fileName, 11, 2) + "/" + Tools.GetSubstring(fileName, 13, 2) + "/" + Tools.GetSubstring(fileName, 15, 2));
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "BudgetTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BudgetReps _jR = new BudgetReps();
                                    _msg = _jR.BudgetTemp(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "TemplateReport")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                if (File.Exists(Tools.ReportsTemplatePath + fileName))
                                {
                                    File.Delete(Tools.ReportsTemplatePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(Tools.ReportsTemplatePath, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ReportsTemplatePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ReportsTemplatePath + fileName);
                                if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx" || ext.ToLower() == ".XLS")
                                {
                                    return Request.CreateResponse(HttpStatusCode.NotFound, "IMPORT DONE");

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else if (Action == "NonAPERDClientInd")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    if (Tools.ClientCode == "21")
                                    {
                                        CustomClient21Reps _nC = new CustomClient21Reps();
                                        _msg = _nC.ImportNonAPERDFundClientInd(Tools.TxtFilePath + fileName, param1);
                                    }
                                    else
                                    {
                                        DataMigrationReps _nC = new DataMigrationReps();
                                        _msg = _nC.ImportNonAPERDFundClientInd(Tools.TxtFilePath + fileName, param1);
                                    }
                                    
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "NonAPERDClientIns")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportNonAPERDFundClientIns(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "NonAPERDClientBank")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportNonAPERDFundClientBank(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                          


                            else if (Action == "NonAPERDTrxSubRed")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    if (Tools.ClientCode == "21")
                                    {
                                        CustomClient21Reps _nC = new CustomClient21Reps();
                                        _msg = _nC.TransactionSubsRedempText(Tools.TxtFilePath + fileName, param1);
                                    }
                                    else
                                    {
                                        DataMigrationReps _nC = new DataMigrationReps();
                                        _msg = _nC.TransactionSubsRedempText(Tools.TxtFilePath + fileName, param1);
                                    }
                                    
                                   
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            else if (Action == "NonAPERDTrxSwitching")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    if (Tools.ClientCode == "21")
                                    {
                                        CustomClient21Reps _nC = new CustomClient21Reps();
                                        _msg = _nC.TransactionSwitchingText(Tools.TxtFilePath + fileName, param1);
                                    }
                                    else
                                    {
                                        DataMigrationReps _nC = new DataMigrationReps();
                                        _msg = _nC.TransactionSwitchingText(Tools.TxtFilePath + fileName, param1);
                                    }
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "NonAPERDBalance")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportBalancePosition(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "NonAPERDDI")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".xls")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportDistributedIncome(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            //APERD

                            else if (Action == "APERDClientInd")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    if (Tools.ClientCode == "24")
                                    {
                                        _msg = _customClient24.ImportAPERDFundClientInd(Tools.ExcelFilePath + fileName, param1);
                                    }
                                    else
                                    {
                                        _msg = _nC.ImportAPERDFundClientInd(Tools.ExcelFilePath + fileName, param1);
                                    }
                                    
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "APERDClientIns")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    if (Tools.ClientCode == "24")
                                    {
                                        _msg = _customClient24.ImportAPERDFundClientIns(Tools.ExcelFilePath + fileName, param1);
                                    }
                                    else
                                    {
                                        _msg = _nC.ImportAPERDFundClientIns(Tools.ExcelFilePath + fileName, param1);
                                    }


                                    
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            


                            else if (Action == "APERDTrxSubRed")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportAPERDSubRed(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            else if (Action == "APERDTrxSwitching")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportAPERDSwitching(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            else if (Action == "APERDBalance")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportAPERDBalancePosition(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "APERDDI")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".xls")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _nC.ImportAPERDDistributedIncome(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "APERDSubsRedempExcel")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".xls")
                                {
                                    DataMigrationReps _nC = new DataMigrationReps();
                                    _msg = _customClient24.ImportAPERDSubsRedempExcel(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "PTPUploadEquitySI")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    PTPUploadReps _nC = new PTPUploadReps();
                                    _msg = _nC.PTPImportEquitySI(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }
                            else if (Action == "PTPUploadTDCB")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    PTPUploadReps _nC = new PTPUploadReps();
                                    _msg = _nC.PTPImportTDCB(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }
                            else if (Action == "PTPUploadTDACB")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    PTPUploadReps _nC = new PTPUploadReps();
                                    _msg = _nC.PTPImportTDACB(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }
                            else if (Action == "PTPUploadFITax")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    PTPUploadReps _nC = new PTPUploadReps();
                                    _msg = _nC.ImportPTPUploadFITax(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }
                            else if (Action == "PTPUploadFICb")
                            {
                                if (File.Exists(Tools.TxtFilePath + fileName))
                                {
                                    File.Delete(Tools.TxtFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.TxtFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.TxtFilePath + fileName);
                                if (ext.ToLower() == ".txt")
                                {
                                    PTPUploadReps _nC = new PTPUploadReps();
                                    _msg = _nC.ImportPTPUploadFICb(Tools.TxtFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "UploadSubs")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".xlsx")
                                {
                                    ClientSubscriptionReps _nC = new ClientSubscriptionReps();
                                    _msg = _nC.ImportUploadSubcription(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "UploadBenchmarkIndex")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext.ToLower() == ".xlsx")
                                {
                                    BenchmarkIndexReps _nC = new BenchmarkIndexReps();
                                    _msg = _nC.ImportBenchmarkIndex(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "OMSBond")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    OMSBondReps _cNR = new OMSBondReps();
                                    _msg = _cNR.ImportOMSBond(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "SIEquity")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        SIEquityReps _nC = new SIEquityReps();
                                        _msg = _nC.ImportSIEquity(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }

                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        SIEquityReps _nC = new SIEquityReps();
                                        _msg = _nC.ImportSIEquityCsv(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "SIDeposito")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx" || ext == ".xlsb")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        SIEquityReps _nC = new SIEquityReps();
                                        _msg = _nC.ImportSIDeposito(Tools.ExcelFilePath + fileName, param1);
                                    }


                                }

                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        SIEquityReps _nC = new SIEquityReps();
                                        _msg = _nC.ImportSIDepositoCsv(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "SInvestDeposito")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx" || ext == ".xlsb")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        InvestmentReps _nC = new InvestmentReps();
                                        _msg = _nC.ImportSInvestDeposito(Tools.ExcelFilePath + fileName, param1);
                                    }


                                }

                                if (ext == ".txt")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        InvestmentReps _nC = new InvestmentReps();
                                        _msg = _nC.ImportSInvestDepositoTxt(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                            }

                            else if (Action == "FFSSetup")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);

                                    {
                                        FFSSetupReps _FFS = new FFSSetupReps();
                                        _msg = _FFS.ImportFFSSetupFromExcel(Tools.ExcelFilePath + fileName, param1);
                                    }
                                }
                            }

                            else if (Action == "BondInformationFromIBPA")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BondInformationFromIBPAReps _cN = new BondInformationFromIBPAReps();
                                    _msg = _cN.ImportBondInformationFromIBPA(Tools.ExcelFilePath + fileName, param1);
                                }
                                if (ext == ".csv")
                                {
                                    BondInformationFromIBPAReps _cN = new BondInformationFromIBPAReps();
                                    _msg = _cN.ImportBondInformationFromIBPACsv(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "AumForBudgetBegBalance")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    AumForBudgetBegBalanceReps _nC = new AumForBudgetBegBalanceReps();
                                    _msg = _nC.AumForBudgetBegBalanceImport(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }

                            else if (Action == "RevenueTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    RevenueReps _jR = new RevenueReps();
                                    if (Tools.ClientCode == "05")
                                    {

                                        _msg = _customClient05.RevenueTemp(Tools.ExcelFilePath + fileName, param1);
                                    }
                                    else
                                    {

                                        _msg = _jR.RevenueTemp(Tools.ExcelFilePath + fileName, param1);
                                    }



                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "OFACSDN")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        BlackListNameReps _nC = new BlackListNameReps();
                                        _msg = _nC.ImportSDN(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "OFACAdd")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        BlackListNameReps _nC = new BlackListNameReps();
                                        _msg = _nC.ImportAdd(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "OFACAlt")
                            {
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                    } while (_fileReady == false);
                                    {
                                        BlackListNameReps _nC = new BlackListNameReps();
                                        _msg = _nC.ImportAlt(Tools.ExcelFilePath + fileName, param1);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "DowJones")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BlackListNameReps _cNR = new BlackListNameReps();
                                    _msg = _cNR.ImportDowJones(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "WindowRedemption")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    FundWindowRedemptionReps _cNR = new FundWindowRedemptionReps();
                                    _msg = _cNR.ImportWindowRedemption(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "CloseNAVInfovesta")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    CloseNAVInfovestaReps _cNR = new CloseNAVInfovestaReps();
                                    _msg = _cNR.ImportCloseNAVInfovesta(Tools.ExcelFilePath + fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "No Action Found");
                            }
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, _msg);
                    }
                    catch (Exception err)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
                    }
                });
            return task;
        }

        public Task<HttpResponseMessage> UploadDataFourParams(string param1, string param2, string param3, string param4)
        {
            string _msg = "Import File";
            //bool _msgExisting = false;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = Tools.root;
            string storage = Tools.storage;
            var provider = new MultipartFormDataStreamProvider(root);
            string Action;
            string _ObjStackTrace = "Upload Controller";
            // Read the form data and return an async task.
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    try
                    {
                        _activityReps.Activity_Insert(DateTime.Now, param3, true, _msg, "", "", param1);
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, "Trade is Faulted / Canceled", _ObjStackTrace, "", param1);
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }
                        bool session = Tools.SessionCheck(param1, param2);
                        if (!session)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoSessionLogMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoSessionMessage);
                        }
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (!havePermission)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoPermissionMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                        }
                        foreach (MultipartFileData FileD in provider.FileData)
                        {
                            bool flagFile = false;
                            if (string.IsNullOrEmpty(FileD.Headers.ContentDisposition.FileName))
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                            }

                            string fileName = FileD.Headers.ContentDisposition.FileName;
                            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                            {
                                fileName = fileName.Trim('"');
                            }

                            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                            {
                                fileName = Path.GetFileName(fileName);
                            }

                            string checkExt = fileName.GetRightString(fileName.Length - fileName.IndexOf("."));

                            foreach (string check in fileTypeFilter)
                            {
                                if (checkExt.ToLower() == check)
                                {
                                    flagFile = true;
                                }
                            }

                            if (!flagFile)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _messageFileTypeFilter);
                            }

                            Action = FileD.Headers.ContentDisposition.Name;

                            if (Action.StartsWith("\"") && Action.EndsWith("\""))
                            {
                                Action = Action.Trim('"');
                            }

                            if (Action.Contains(@"/") || Action.Contains(@"\"))
                            {
                                Action = Path.GetFileName(Action);
                            }


                            if (Action == "BenchmarkIndex")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".csv")
                                {
                                    BenchmarkIndexReps _cPR = new BenchmarkIndexReps();
                                    _msg = _cPR.BenchmarkIndexUploadFromYahoo(Tools.ExcelFilePath + fileName, param1, param4);
                                }
                                else if (ext.ToUpper() == ".DBF")
                                {
                                    BenchmarkIndexReps _cPR = new BenchmarkIndexReps();
                                    _msg = _cPR.BenchmarkIndexUploadFromIDX(fileName, param1);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else if (Action == "ImportOmsEquityTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    OMSEquityReps _jR = new OMSEquityReps();
                                    if (Tools.ClientCode == "25" || Tools.ClientCode == "01")
                                    {
                                        _msg = _customClient25.ImportOmsEquityTemp(Tools.ExcelFilePath + fileName, param1, param4);
                                    }
                                    else
                                    {
                                        _msg = _jR.ImportOmsEquityTemp(Tools.ExcelFilePath + fileName, param1, param4);

                                        if (Tools.ClientCode == "21" && Tools.ComplianceEmail == true)
                                        {
                                            string _BodyMessage;

                                            HighRiskMonitoringReps _highRiskMonitoringReps = new HighRiskMonitoringReps();

                                            _BodyMessage = _highRiskMonitoringReps.EmailExposureByImport(param4);

                                            if (_BodyMessage != "")
                                            {
                                                SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                                                dt = SendEmailReps.SendEmailTestingByInput(param1, Tools._EmailHighRiskMonitoring, "Breach Investment", _BodyMessage, "", "");
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "ImportOmsDepositoTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    OMSTimeDepositReps _jR = new OMSTimeDepositReps();
                                    _msg = _jR.ImportOmsDepositoTemp(Tools.ExcelFilePath + fileName, param1, param4);

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }


                            else if (Action == "ImportDealingEquityTemp")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    InvestmentReps _jR = new InvestmentReps();
                                    if (Tools.ClientCode == "03")
                                    {
                                        _msg = _customClient03.ImportDealingEquityTemp(Tools.ExcelFilePath + fileName, param1, param4);
                                    }
                                    else if (Tools.ClientCode == "05")
                                    {
                                        _msg = _customClient05.ImportDealingEquityTemp(Tools.ExcelFilePath + fileName, param1, param4);
                                    }
                                    else
                                    {
                                        _msg = _jR.ImportDealingEquityTemp(Tools.ExcelFilePath + fileName, param1, param4);
                                    }
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "BlackListNameDTTOT")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BlackListNameReps _bLNR = new BlackListNameReps();
                                    _msg = _bLNR.ImportBlackListNameDTTOT(Tools.ExcelFilePath + fileName, param1, param4);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "BlackListNamePPATK")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BlackListNameReps _bLNR = new BlackListNameReps();
                                    _msg = _bLNR.ImportBlackListNamePPATK(Tools.ExcelFilePath + fileName, param1, param4);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "BlackListNameKPK")
                            {
                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BlackListNameReps _bLNR = new BlackListNameReps();
                                    _msg = _bLNR.ImportBlackListNameKPK(Tools.ExcelFilePath + fileName, param1, param4);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "BloombergEquity")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx" || ext == ".xlsb")
                                {
                                    BloombergEquityReps _cPR = new BloombergEquityReps();
                                    _msg = _cPR.ImportBloombergEquity(Tools.ExcelFilePath + fileName, param1, param4);
                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }
                            else if (Action == "ClosePriceBond")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    ClosePriceReps _cPR = new ClosePriceReps();

                                    if (Tools.ClientCode == "05")
                                    {
                                        _msg = _customClient05.ImportClosePriceBond(Tools.ExcelFilePath + fileName, param1, param4);
                                    }

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "BudgetSummary")
                            {

                                if (File.Exists(Tools.ExcelFilePath + fileName))
                                {
                                    File.Delete(Tools.ExcelFilePath + fileName);
                                }

                                File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                Boolean _fileReady = false;
                                do
                                {
                                    Thread.Sleep(500);
                                    _fileReady = _host.IsFileReady(Tools.ExcelFilePath + fileName);

                                } while (_fileReady == false);
                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xls" || ext == ".xlsx")
                                {
                                    BudgetSummaryReps _cPR = new BudgetSummaryReps();

                                    //if (Tools.ClientCode == "05")
                                    //{
                                    _msg = _budgetSummary.BudgetSummaryImport(Tools.ExcelFilePath + fileName, param1, param4);
                                    //}

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "FundClientDocument")
                            {
                                string ext = Path.GetExtension(storage + fileName);
                                if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png"
                                    || ext.ToLower() == ".pdf" || ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx"
                                    || ext.ToLower() == ".txt")
                                {
                                    // Ambil logic LastName berdasarkan Param1
                                    FundClientDocumentReps _nC = new FundClientDocumentReps();
                                    int fileNumber = _nC.DocumentImport_GetFileNumber(param1, param4);

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(storage + fileName);

                                    } while (_fileReady == false);
                                    string _desc = Path.GetFileName(fileName);
                                    string NewFileName = param4 + "_" + fileNumber.ToString() + "_" + _desc;
                                    string Description = fileName;
                                    File.Move(Path.Combine(storage, fileName), Path.Combine(Tools.ImgFilePath, NewFileName));
                                    _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ImgFilePath + NewFileName);

                                    } while (_fileReady == false);


                                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png"
                                        || ext.ToLower() == ".pdf" || ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx"
                                        || ext.ToLower() == ".txt"
                                        )
                                    {

                                        _msg = _nC.DocumentImport(param1, param4, fileNumber, NewFileName, Description);
                                    }
                                }

                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }
                            }

                            else if (Action == "ClosePrice")
                            {


                                string ext = Path.GetExtension(Tools.ExcelFilePath + fileName);
                                if (ext == ".xlsx")
                                {

                                    if (File.Exists(Tools.ExcelFilePath + fileName))
                                    {
                                        File.Delete(Tools.ExcelFilePath + fileName);
                                    }

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.DBFFilePath + fileName);

                                    } while (_fileReady == false);

                                    ClosePriceReps _cR = new ClosePriceReps();
                                    _msg = _cR.ImportClosePriceEquityFromExcelFile(Tools.ExcelFilePath + fileName, param1, param4);

                                }
                                else
                                {
                                    _msg = "This request is not properly formatted";
                                }

                            }


                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "No Action Found");
                            }
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, _msg);
                    }
                    catch (Exception err)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
                    }
                });
            return task;
        }

        public Task<HttpResponseMessage> UploadDataFiveParams(string param1, string param2, string param3, string param4, string param5)
        {
            string _msg = "Import File";
            //bool _msgExisting = false;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = Tools.root;
            string storage = Tools.storage;
            var provider = new MultipartFormDataStreamProvider(root);
            string Action;
            string _ObjStackTrace = "Upload Controller";
            // Read the form data and return an async task.
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    try
                    {
                        _activityReps.Activity_Insert(DateTime.Now, param3, true, _msg, "", "", param1);
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, "Trade is Faulted / Canceled", _ObjStackTrace, "", param1);
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }
                        bool session = Tools.SessionCheck(param1, param2);
                        if (!session)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoSessionLogMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoSessionMessage);
                        }
                        bool havePermission = _host.Get_Permission(param1, param3);
                        if (!havePermission)
                        {
                            _activityReps.Activity_Insert(DateTime.Now, param3, false, Tools.NoPermissionMessage, _ObjStackTrace, "", param1);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                        }
                        foreach (MultipartFileData FileD in provider.FileData)
                        {
                            bool flagFile = false;
                            if (string.IsNullOrEmpty(FileD.Headers.ContentDisposition.FileName))
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                            }

                            string fileName = FileD.Headers.ContentDisposition.FileName;
                            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                            {
                                fileName = fileName.Trim('"');
                            }

                            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                            {
                                fileName = Path.GetFileName(fileName);
                            }

                            string checkExt = fileName.GetRightString(fileName.Length - fileName.IndexOf("."));

                            foreach (string check in fileTypeFilter)
                            {
                                if (checkExt.ToLower() == check)
                                {
                                    flagFile = true;
                                }
                            }

                            if (!flagFile)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _messageFileTypeFilter);
                            }

                            Action = FileD.Headers.ContentDisposition.Name;

                            if (Action.StartsWith("\"") && Action.EndsWith("\""))
                            {
                                Action = Action.Trim('"');
                            }

                            if (Action.Contains(@"/") || Action.Contains(@"\"))
                            {
                                Action = Path.GetFileName(Action);
                            }

                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "No Action Found");
                            }
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, _msg);
                    }
                    catch (Exception err)
                    {
                        _activityReps.Activity_Insert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.Message, err.StackTrace, param1);
                        if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
                    }
                });
            return task;
        }


    }
}