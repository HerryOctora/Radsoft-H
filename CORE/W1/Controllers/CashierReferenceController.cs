
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
    public class CashierReferenceController : ApiController
    {
        static readonly string _Obj = "Cashier Reference Controller";
        static readonly CashierReferenceReps _cashierReferenceReps = new CashierReferenceReps();
        static readonly CustomClient05Reps _customClient05 = new CustomClient05Reps();
        static readonly CustomClient09Reps _customClient09 = new CustomClient09Reps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        



        [HttpGet]
        public HttpResponseMessage CashierReference_GenerateNewReference(string param1, string param2, string param3, int param4, DateTime param5, string param6)
        {
            string PermissionID;
            PermissionID = param6;
            try
            {
                bool session = Tools.SessionCheck(param1, param2);
                if (session)
                {
                    bool havePermission = _host.Get_Permission(param1, PermissionID);
                    if (havePermission)
                    {
                        try
                        {

                            if (Tools.ClientCode == "05")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _customClient05.CashierReference_GenerateNewReference(param3, param4, param5));
                            }
                            else if (Tools.ClientCode == "09")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _customClient09.CashierReference_GenerateNewReference(param3, param4, param5));
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, _cashierReferenceReps.CashierReference_GenerateNewReference(param3, param4, param5));
                            }


                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoPermissionLogMessage, _Obj, "New Reference Permission", param1, 0, 0, 0, "");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Tools.NoPermissionMessage);
                    }
                }
                else
                {
                    _activityReps.Activity_LogInsert(DateTime.Now, "", false, Tools.NoSessionLogMessage, _Obj, "Cashier Reference", param1, 0, 0, 0, "");
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
