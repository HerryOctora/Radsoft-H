
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
    public class TemplateImportController : ApiController
    {
        static readonly string _Obj = "TemplateImport Controller";
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();



        [HttpGet]
        public HttpResponseMessage GenerateTemplateImport(string param1, string param2, string param3)
        {
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    try
                    {
                        string _code = Tools.GetSubstring(param3, 0, 2);
                        if (Tools.ClientCode != "00" && _code == Tools.ClientCode)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlTemplatePath + "/" + Tools.ClientCode + "/" + param3 + ".xlsx");
                        }
                        else
                        {
                            if (param3 == "TemplateEOI2019")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlTemplatePath + param3 + ".xlsm");
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Tools.HtmlTemplatePath + param3 + ".xlsx");
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
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Unit Registry Report", param1, 0, 0, 0, "");
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
