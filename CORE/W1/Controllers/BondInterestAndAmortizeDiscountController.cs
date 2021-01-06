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
    public class BondInterestAndAmortizeDiscountController : ApiController
    {
        static readonly string _Obj = "Bond Interest And Amortize Discount Controller";
        static readonly CustomClient11Reps _c11 = new CustomClient11Reps();
        static readonly ActivityReps _activityReps = new ActivityReps();
        static readonly Host _host = new Host();

        /*
     * param1 = userID
     * param2 = sessionID
     * param3 = AccountPK
     */
        [HttpGet]
        public HttpResponseMessage A(string param1, string param2, int param3)
        {
            _c11.GenerateBondInterestAndAmortizeDiscount(param1);
            return Request.CreateResponse(HttpStatusCode.OK, "Oce");
        }

        [HttpGet]
        public HttpResponseMessage AmortizeBond(string param1, string param2, string param3, string param4)
        {
            _c11.GenerateBondInterestAndAmortizeDiscountFromSettlement(param1,param3,param4);
            return Request.CreateResponse(HttpStatusCode.OK, "Amortize Bond Success");
        }
    }
}
