
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using RFSRepository;

namespace W1.Controllers
{
    public class FFSSetup_02Controller : ApiController
    {
        static readonly string _Obj = "FFSSetup_02 Controller";
        static readonly FFSSetup_02Reps _FFSSetup_02Reps = new FFSSetup_02Reps();
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
                        return Request.CreateResponse(HttpStatusCode.OK, _FFSSetup_02Reps.FFSSetup_02_Select(param3));
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
        public HttpResponseMessage Action(string param1, string param2, string param3, [FromBody]FFSSetup_02 _FFSSetup_02)
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
                            if (PermissionID == "FFSSetup_02_U")
                            {
                                int _newHisPK = _FFSSetup_02Reps.FFSSetup_02_Update(_FFSSetup_02, havePrivillege);

                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Update FFSSetup_02 Success", _Obj, "", param1, _FFSSetup_02.FFSSetup_02PK, _FFSSetup_02.HistoryPK, _newHisPK, "UPDATE");
                                return Request.CreateResponse(HttpStatusCode.OK, "Update FFSSetup_02 Success");
                            }
                            if (PermissionID == "FFSSetup_02_A")
                            {
                                _FFSSetup_02Reps.FFSSetup_02_Approved(_FFSSetup_02);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Approved FFSSetup_02 Success", _Obj, "", param1, _FFSSetup_02.FFSSetup_02PK, _FFSSetup_02.HistoryPK, 0, "APPROVED");
                                return Request.CreateResponse(HttpStatusCode.OK, "Approved FFSSetup_02 Success");
                            }
                            if (PermissionID == "FFSSetup_02_V")
                            {
                                _FFSSetup_02Reps.FFSSetup_02_Void(_FFSSetup_02);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Void FFSSetup_02 Success", _Obj, "", param1, _FFSSetup_02.FFSSetup_02PK, _FFSSetup_02.HistoryPK, 0, "VOID");
                                return Request.CreateResponse(HttpStatusCode.OK, "Void FFSSetup_02 Success");
                            }
                            if (PermissionID == "FFSSetup_02_R")
                            {
                                _FFSSetup_02Reps.FFSSetup_02_Reject(_FFSSetup_02);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Reject FFSSetup_02 Success", _Obj, "", param1, _FFSSetup_02.FFSSetup_02PK, _FFSSetup_02.HistoryPK, 0, "REJECT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Reject FFSSetup_02 Success");
                            }
                            if (PermissionID == "FFSSetup_02_I")
                            {
                                int _lastPKByLastUpdate = _FFSSetup_02Reps.FFSSetup_02_Add(_FFSSetup_02, havePrivillege);
                                _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, "Add FFSSetup_02 Success", _Obj, "", param1, _lastPKByLastUpdate, 0, 0, "INSERT");
                                return Request.CreateResponse(HttpStatusCode.OK, "Insert FFSSetup_02 Success");
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


        public Task<HttpResponseMessage> UploadImageData(string param1, string param2, string param3, string param4, int param5, string param6)
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



                            else if (Action == "Image_Import")
                            {
                                string ext = Path.GetExtension(storage + fileName);
                                if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png"
                                    || ext.ToLower() == ".pdf" || ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx"
                                    || ext.ToLower() == ".txt")
                                {
                                    // Ambil logic LastName berdasarkan Param1
                                    FFSSetup_02Reps _nC = new FFSSetup_02Reps();
                                    string _path = "E:/Radsoft/15 Oktober/CORE/W1/Images/02/" + param6 + ext;

                                    File.Move(FileD.LocalFileName, Path.Combine(storage, fileName));
                                    Boolean _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(storage + fileName);

                                    } while (_fileReady == false);

                                    string NewFileName = param6 + ext;
                                    File.Move(Path.Combine(storage, fileName), Path.Combine(Tools.ImgFFSPath02, NewFileName));
                                    _fileReady = false;
                                    do
                                    {
                                        Thread.Sleep(500);
                                        _fileReady = _host.IsFileReady(Tools.ImgFFSPath02 + NewFileName);

                                    } while (_fileReady == false);


                                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png"
                                        || ext.ToLower() == ".pdf" || ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx"
                                        || ext.ToLower() == ".txt"
                                        )
                                    {

                                        _msg = _nC.ImportImage(NewFileName, _path, param5);
                                    }
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
       */
        [HttpGet]
        public HttpResponseMessage GetFundID(string param1, string param2, int param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _FFSSetup_02Reps.GetFundID(param3));
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Get Fund Client Combo Rpt", param1, 0, 0, 0, "");
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
