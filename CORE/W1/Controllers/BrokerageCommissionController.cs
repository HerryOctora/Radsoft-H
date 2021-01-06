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
    public class BrokerageCommissionController : ApiController
    {

        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly BrokerageCommissionReps _brokerageCommission = new BrokerageCommissionReps();
        //static readonly BrokerageCommissionReps _brokerageCommission = new BrokerageCommissionReps();
        static readonly Host _host = new Host();

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

                            Action = FileD.Headers.ContentDisposition.Name;

                            if (Action.StartsWith("\"") && Action.EndsWith("\""))
                            {
                                Action = Action.Trim('"');
                            }

                            if (Action.Contains(@"/") || Action.Contains(@"\"))
                            {
                                Action = Path.GetFileName(Action);
                            }

                            if (Action == "BC_MSSales")
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
                                    BrokerageCommissionReps _cPR = new BrokerageCommissionReps();
                                    _msg = _cPR.ImportMasterSales(Tools.ExcelFilePath + fileName, param1);
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

                            Action = FileD.Headers.ContentDisposition.Name;

                            if (Action.StartsWith("\"") && Action.EndsWith("\""))
                            {
                                Action = Action.Trim('"');
                            }

                            if (Action.Contains(@"/") || Action.Contains(@"\"))
                            {
                                Action = Path.GetFileName(Action);
                            }


                            if (Action == "BC_MSTransaction")
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
                                if (ext == ".xlsx" || ext == ".xls")
                                {
                                    BrokerageCommissionReps _brokerageCommission = new BrokerageCommissionReps();
                                    _msg = _brokerageCommission.ImportMasterTransaction(Tools.ExcelFilePath + fileName, param1, param4);
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
                        return Request.CreateResponse(HttpStatusCode.OK, _brokerageCommission.Sales_Select(param3));
                        //return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);

                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data", param1, 0, 0, 0, "");
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
        public HttpResponseMessage GetDataTransaction(string param1, string param2)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _brokerageCommission.Transaction_Select());
                        //return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);

                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    //_activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Data", param1, 0, 0, 0, "");
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