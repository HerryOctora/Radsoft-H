using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using System.Security.Cryptography;
using System.Data.SqlClient;
using RestSharp.Serializers;
using RFSRepository;
using System.IO;
using System.Net;

namespace RFSRepositoryThree
{
    public class HostToHostTeravinReps
    {
        private string _url = "https://apigw.int.prod.principal.co.id";
        private string _userName = "radsoft";
        private string _accessKey = "66c3bb2b-43d7-40f0-82df-3e66f1b600a6";

        public class SessionWrapper
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public int refresh_expires_in { get; set; }
            public string refresh_token { get; set; }
            public string token_type { get; set; }
            public string session_state { get; set; }
            public string scope { get; set; }
        }


        public class TrxUnit
        {
            public string FrontID { get; set; }
        }

        //http://10.70.224.142:17050/Radsoft/HostToHostTeravin/Logincheck/admin/RAD-ID15540d2b-9c5f-5133-ac2f-a3b439358331
        public SessionWrapper LoginRequest()
        {
  
            SessionWrapper _respon = new SessionWrapper();
            var Client = new RestClient(_url);
           
            var Request = new RestRequest("api/v1/ext/login", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Request.AddParameter("client_id", _userName);
            Request.AddParameter("client_secret", _accessKey);
            var Execute = Client.Execute(Request);
            JsonDeserializer deserial = new JsonDeserializer();
            _respon = deserial.Deserialize<SessionWrapper>(Execute);
            return _respon;
        }

        public string TransactionUpdate(string _dateFrom,string _dateTo)
        {
            List<TrxUnit> _l = new List<TrxUnit>();
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
	                        Select TransactionPK From ClientSubscription where Posted = 1 and status = 2
                            and isnull(TransactionPK,'') <> '' and NAVDate between @DateFrom and @DateTo
                    ";
                    cmd1.Parameters.AddWithValue("@DateFrom", _dateFrom);
                    cmd1.Parameters.AddWithValue("@DateTo", _dateTo);
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {
                                TrxUnit _m = new TrxUnit();
                                _m.FrontID = dr0["TransactionPK"].ToString();
                                _l.Add(_m);
                            }
                        }
                    }
                }
            }

            SessionWrapper _respon = new SessionWrapper();
            _respon = LoginRequest();

            foreach (var a in _l)
            {
                var Client = new RestClient(_url);
                var Request = new RestRequest("api/v1/webhooks/transaction-update/" + a.FrontID, Method.GET);
                Request.AddHeader("Authorization", "Bearer " + _respon.access_token);
                var Execute = Client.Execute(Request);
            }
            return "OK";

        }

        public string NAVUpdate()
        {
            //http://10.70.224.142:17050/Radsoft/HostToHostTeravin/NAVUpdate/admin/RAD-ID15540d2b-9c5f-5133-ac2f-a3b439358331 
            SessionWrapper _respon = new SessionWrapper();
            _respon = LoginRequest();
            //return _respon.token_type;
            var Client = new RestClient(_url);
            var Request = new RestRequest("api/v1/webhooks/nav-update", Method.GET);
            Request.AddHeader("Authorization", "Bearer " + _respon.access_token);
            var Execute = Client.Execute(Request);
            return Execute.ResponseStatus + " " + Execute.Content + " " + Execute.ErrorMessage;
        }

        public string BalanceUpdate()
        {
            SessionWrapper _respon = new SessionWrapper();
            _respon = LoginRequest();
            var Client = new RestClient(_url);
            var Request = new RestRequest("api/v1/webhooks/balance-update", Method.GET);
            Request.AddHeader("Authorization", "Bearer " + _respon.access_token);
            var Execute = Client.Execute(Request);
            return "OK";
        }


    }
}
