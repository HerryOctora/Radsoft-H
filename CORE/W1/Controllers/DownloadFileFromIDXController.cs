using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RFSRepository;
using RFSModel;
using RFSUtility;using RFSRepositoryOne;using RFSRepositoryTwo;using RFSRepositoryThree;
using System.ComponentModel;
using System.Configuration;


namespace W1.Controllers
{
    public class DownloadFileFromIDXController : ApiController
    {
        static readonly string _Obj = "Download File From IDX Controller";
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
        * param1 = userID
        * param2 = sessionID
        * param3 = permission
        * param4 = data model
        */
        [HttpPost]
        public HttpResponseMessage DownloadFileFromIDX(string param1, string param2, string param3, [FromBody] DownloadFileFromIDX _mdl)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    string PermissionID = param3;
                    try
                    {
                        // Set Variable
                        string _url = _mdl.FileLocation + _mdl.FileName;
                        string _storage = Tools.storage;
                        var _result = string.Empty;
                        var _resultDownload = string.Empty;
                        var _resultUnZip = string.Empty;
                        var _resultImport = string.Empty;

                        string ProxyStatus = ConfigurationManager.AppSettings["ProxyStatus"].ToString();
                        string ProxyIPAddress = ConfigurationManager.AppSettings["ProxyIPAddress"].ToString();
                        int ProxyPort = Convert.ToInt32(ConfigurationManager.AppSettings["ProxyPort"]);

                        // Step 1 : Download File From IDX
                        try
                        {
                            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                            {
                                using (System.Net.WebClient client = new System.Net.WebClient())
                                {
                                    if (System.IO.File.Exists(_storage + _mdl.FileName))
                                    {
                                        System.IO.File.Delete(_storage + _mdl.FileName);
                                    }

                                    if (System.IO.File.Exists(_storage + _mdl.FileName.Replace("zip", "DBF")))
                                    {
                                        System.IO.File.Delete(_storage + _mdl.FileName.Replace("zip", "DBF"));
                                    }

                                    //1 : with proxy
                                    //2 : disable proxy
                                    if (ProxyStatus == "1")
                                    {
                                        WebProxy wp = new WebProxy(ProxyIPAddress, ProxyPort);
                                        client.Proxy = wp;
                                    }

                                    //if (Tools.ClientCode == "05")
                                    //{
                                    //    WebProxy wp = new WebProxy(Tools._internetProxy, Tools._proxyPort);
                                    //    client.Proxy = wp;
                                    //}

                                    client.DownloadFile(new Uri(_url), _storage + _mdl.FileName);

                                    if (System.IO.File.Exists(_storage + _mdl.FileName))
                                    {
                                        _resultDownload = "Download File From IDX Success";
                                    }
                                    else
                                    {
                                        _resultDownload = "Download File From IDX Canceled, File Download Not Found!";
                                    }
                                }
                            }
                            else
                            {
                                _resultDownload = "Download File From IDX Canceled, Network Not Available!";
                            }
                        }
                        catch (Exception err)
                        {
                            _resultDownload = "Download File From IDX Canceled, Error : " + err.Message.ToString();
                            throw err;
                        }

                        // Step 2 : Unzip File after Download File From IDX Success
                        if (_resultDownload == "Download File From IDX Success")
                        {
                            try
                            {
                                if (System.IO.File.Exists(_storage + _mdl.FileName))
                                {
                                    System.IO.Compression.ZipFile.ExtractToDirectory(_storage + _mdl.FileName, _storage);
                                    _resultUnZip = "Unzip File From File Download Success";
                                }
                                else
                                {
                                    _resultDownload = "Unzip File From File Download Canceled, File Zip Not Found!";
                                }
                            }
                            catch (Exception err)
                            {
                                _resultUnZip = "Unzip File From File Download Canceled, Error : " + err.Message.ToString();
                                throw err;
                            }
                        }

                        // Step 3 : Import File after Unzip File and Download File From IDX Success
                        if (_resultDownload == "Download File From IDX Success" && _resultUnZip == "Unzip File From File Download Success")
                        {
                            try
                            {
                                _mdl.FileName = _mdl.FileName.Replace("zip", "DBF");
                                if (System.IO.File.Exists(_storage + _mdl.FileName))
                                {
                                    string _importMsg = string.Empty;
                                    if (_mdl.FileName.Substring(0, 2) == "CP")
                                    {
                                        ClosePriceReps _reps = new ClosePriceReps();
                                        _importMsg = _reps.ImportClosePrice(_mdl.FileName, param1, Tools.GetSubstring(_mdl.FileName, 4, 2) + "/" + Tools.GetSubstring(_mdl.FileName, 6, 2) + "/" + Tools.GetSubstring(_mdl.FileName, 2, 2));
                                    }
                                    else
                                    {
                                        _importMsg = "File Import Not Close Price Is Not Set";
                                    }

                                    if (string.IsNullOrEmpty(_importMsg))
                                    {
                                        _resultImport = "Import File From File Download Canceled, Result Message Import Not Found!";
                                    }
                                    else
                                    {
                                        if (_importMsg == "Success")
                                        {
                                            _resultImport = "Import File From File Download Success";
                                        }
                                        else
                                        {
                                            if (_importMsg.Contains("Error"))
                                            {
                                                _resultImport = "Import File From File Download Canceled, Error : " + _importMsg;
                                            }
                                            else
                                            {
                                                _resultImport = "Import File From File Download Canceled, " + _importMsg;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _resultImport = "Import File From File Download Canceled, File DBF Not Found!";
                                }
                            }
                            catch (Exception err)
                            {
                                _resultImport = "Import File From File Download Canceled, Error : " + err.Message.ToString();
                                throw err;
                            }
                        }

                        // Step 4 : Set Result Msg
                        if (!string.IsNullOrEmpty(_resultDownload))
                        {
                            _result = _resultDownload;
                        }
                        if (!string.IsNullOrEmpty(_resultDownload) && !string.IsNullOrEmpty(_resultUnZip))
                        {
                            _result = _resultDownload + "; " + _resultUnZip;
                        }
                        if (!string.IsNullOrEmpty(_resultDownload) && !string.IsNullOrEmpty(_resultUnZip) && !string.IsNullOrEmpty(_resultImport))
                        {
                            _result = _resultDownload + "; " + _resultUnZip + "; " + _resultImport;
                        }

                        // Step 5 : Log Activity To Database and Return Result Msg To Client Side [App]
                        _activityReps.Activity_LogInsert(DateTime.Now, PermissionID, true, _result, _Obj, "Download File From IDX", param1, 0, 0, 0, "DOWNLOAD");
                        return Request.CreateResponse(HttpStatusCode.OK, _result.Replace("; ", "<br />"));
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Download File From IDX", param1, 0, 0, 0, "");
                    return Request.CreateResponse(HttpStatusCode.NotFound, Tools.NoSessionMessage);
                }
            }
            catch (Exception err)
            {
                _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.InternalErrorMessage, err.StackTrace, err.Message, param1, 0, 0, 0, "");
                if (Tools.ClientCode == "05") { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Contact Administrator"); } else { return Request.CreateResponse(HttpStatusCode.InternalServerError, Tools.ErrorPrefix + err.Message); }
            }
        }


        private string DownloadComplete()
        {
            return "";
        }

    }
}
