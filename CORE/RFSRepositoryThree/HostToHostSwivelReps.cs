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

namespace RFSUtility
{
    public class HostToHostSwivelReps
    {
        private string _url = "https://principaldev.istrives.com";
        private string _accessKey = "0mTE5NVwvvTNwgrd";
        private string _userName = "wiranto@radsoftsystem.com";

        public class ResponChallengeRequest
        {
            public bool success { get; set; }
            public ResultChallenge result { get; set; }
        }

        public class ResultChallenge
        {
            public string token { get; set; }
            public string serverTime { get; set; }
            public string expireTime { get; set; }
        }


        public class ResponLoginRequest
        {
            public bool success { get; set; }
            public ResultLogin result { get; set; }
        }

        public class ResultLogin
        {
            public string sessionName { get; set; }
            public string userId { get; set; }
            public string version { get; set; }
            public string vtigerVersion { get; set; }
        }

        public class ResponClientRevise
        {
            public bool success { get; set; }
            public ResultClientRevise result { get; set; }
        }

        public class ResultClientRevise
        {

            public string id { get; set; }
            public string cf_1204 { get; set; } //Customer Status
            public string cf_1032 { get; set; } //SID
            public string cf_1030 { get; set; } //IFUA
            public string cf_1034 { get; set; } //IFUA DATE
            public string contact_no { get; set; } //IFUA DATE
        }

        public class ResponLoginHelpDeskCreate
        {
            public bool success { get; set; }
            public ResultHelpDeskCreate result { get; set; }
        }

        public class ResultHelpDeskCreate
        {
            public long PK { get; set; }
            public string ticket_no { get; set; }
            public string ticket_title { get; set; }
            public string assigned_user_id { get; set; }
            public string ticketpriorities { get; set; }
            public string ticketstatus { get; set; }
            public string ticketcategories { get; set; }
            public string description { get; set; }
            public string source { get; set; }
            public string cf_962 { get; set; }
            public string cf_968 { get; set; }
            public string cf_976 { get; set; }
            public string cf_1267 { get; set; }
        
        }

        public class ResultReconCustomers
        {
            public string subject { get; set; }
            public string activitytype { get; set; }
            public string date_start { get; set; }
            public string time_start { get; set; }
            public string due_date { get; set; }
            public string time_end { get; set; }
            public string assigned_user_id { get; set; }
            public string description { get; set; }
            public string eventstatus { get; set; }
            public string cf_1314 { get; set; }
            public string cf_1316 { get; set; }
            public string cf_1318 { get; set; }
            public string cf_1320 { get; set; }

        }

        public class ResultReconVolume
        {
            public string subject { get; set; }
            public string activitytype { get; set; }
            public string date_start { get; set; }
            public string time_start { get; set; }
            public string due_date { get; set; }
            public string time_end { get; set; }
            public string assigned_user_id { get; set; }
            public string description { get; set; }
            public string eventstatus { get; set; }
            public string cf_1326 { get; set; }
        }

        public class ResultReconFund
        {
            public string subject { get; set; }
            public string activitytype { get; set; }
            public string date_start { get; set; }
            public string time_start { get; set; }
            public string due_date { get; set; }
            public string time_end { get; set; }
            public string assigned_user_id { get; set; }
            public string description { get; set; }
            public string eventstatus { get; set; }
            public string cf_1330 { get; set; }
            public string cf_1322 { get; set; }
            public string cf_1324 { get; set; }


        }

        public string ReconFundUnit()
        {
            ResponLoginRequest _loginReq = new ResponLoginRequest();
            _loginReq = LoginRequest();


            List<ResultReconFund> _l = new List<ResultReconFund>();

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"

Declare @cf_1322 int
Declare @cf_1324 int


Declare @Date datetime
set @date = '07/06/20'
--set @Date = Dbo.FWorkingDay(cast(@date as Date),-1)
set @Date = Dbo.FWorkingDay(cast(GetDate() as Date),-1)



Declare @table table
(
	FundPK int,
	TotalUnit numeric(22,4)
)

insert into @table
Select A.FundPK,Sum(isnull(UnitAmount,0)) TotalUnit From FundClientPosition A
	left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) and B.IsPublic = 1
	where A.Date = @Date
	group by A.FundPK


Declare @tableNAV table
(
	FundPK int,
	NAV numeric(18,8)
)
insert into @tableNAV
Select FundPK,Nav From CloseNAV where status in (1,2) and Date = @Date

Select 'Recon Unit' Task,A.Name,isnull(B.TotalUnit,0) cf_1322  
,isnull(C.NAV,0) cf_1324, Format(GetDate(),'yyyy-MM-dd') date_start
,Format(GetDate(),'HH:mm:ss') time_start,A.ID fund_id
From Fund A
left join @table B on A.FundPK = B.FundPK
left join @tableNAV C on A.FundPK = C.FundPK
where A.status in (1,2)
and A.IsPublic = 1 and isnull(B.TotalUnit,0) > 0
                    ";
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {
                                ResultReconFund _m = new ResultReconFund();
                                _m.subject = "Recon-TA";
                                _m.activitytype = "Recon - Fund";
                                _m.date_start = dr0["date_start"].ToString();
                                _m.time_start = dr0["time_start"].ToString();
                                _m.due_date = dr0["date_start"].ToString();
                                _m.time_end = dr0["time_start"].ToString();
                                _m.assigned_user_id = "19x52";
                                _m.description = "NA";
                                _m.eventstatus = "Planned";
                                _m.cf_1330 = dr0["fund_id"].ToString();
                                _m.cf_1322 = dr0["cf_1322"].ToString();
                                _m.cf_1324 = dr0["cf_1324"].ToString();
                                _l.Add(_m);

                            }

                        }
                        else
                        {
                            return "NO DATA";
                        }
                    }

                }
            }
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string filePath = Tools.SInvestTextPath + "LogSwivel_ReconFundUnit.txt";
            FileInfo txtFile = new FileInfo(filePath);

            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log For recon Fund unit");
                }
            }
            foreach (var a in _l)
            {
                string _el = "";
                JsonSerializer serializer = new JsonSerializer();
                _el = serializer.Serialize(a);

                Request.AddParameter("operation", "create");
                Request.AddParameter("sessionName", _loginReq.result.sessionName);
                Request.AddParameter("element", _el);
                Request.AddParameter("elementType", "Events");
                var Execute = Client.Execute(Request);
                Request.Parameters.Clear();
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(a.activitytype + " " + a.date_start + " " + a.time_start + " " + a.cf_1330 + " " + a.cf_1322 + " " + a.cf_1324 + " " + Execute.Content.ToString());
                }
            }



            return "OK";
        }

        public string ReconVolume()
        {
            ResponLoginRequest _loginReq = new ResponLoginRequest();
            _loginReq = LoginRequest();


            ResultReconVolume _m = new ResultReconVolume();

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
Declare @cf_1326 int

Select @cf_1326 = count(ClientSubscriptionPK) From ClientSubscription 
where EntryUsersID = 'RDO' and ValueDate = Dbo.FWorkingDay(cast(GetDate() as Date),-1)
and status in (1,2)  and Revised = 0

Select 'ReconVolume' Task, @cf_1326 cf_1326, Format(GetDate(),'yyyy-MM-dd') date_start
,Format(GetDate(),'HH:mm:ss') time_start
                    ";
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {

                                _m.subject = "Recon-TA";
                                _m.activitytype = "Recon - Volume";
                                _m.date_start = dr0["date_start"].ToString();
                                _m.time_start = dr0["time_start"].ToString();
                                _m.due_date = dr0["date_start"].ToString();
                                _m.time_end = dr0["time_start"].ToString();
                                _m.assigned_user_id = "19x52";
                                _m.description = "NA";
                                _m.eventstatus = "Planned";
                                _m.cf_1326 = dr0["cf_1326"].ToString();
                               

                            }

                        }
                        else
                        {
                            return "NO DATA";
                        }
                    }

                }
            }
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string _el = "";
            JsonSerializer serializer = new JsonSerializer();
            _el = serializer.Serialize(_m);

            Request.AddParameter("operation", "create");
            Request.AddParameter("sessionName", _loginReq.result.sessionName);
            Request.AddParameter("element", _el);
            Request.AddParameter("elementType", "Events");
            var Execute = Client.Execute(Request);


            string filePath = Tools.SInvestTextPath + "LogSwivel_ReconVolume.txt";
            FileInfo txtFile = new FileInfo(filePath);

            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log For recon Volume");
                }
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(_m.activitytype + " " + _m.date_start + " " + _m.time_start + " " + _m.cf_1326  + " " + Execute.Content.ToString());

            }



            return "OK";
        }

        public string ReconCustomers()
        {
            ResponLoginRequest _loginReq = new ResponLoginRequest();
            _loginReq = LoginRequest();


            ResultReconCustomers _m = new ResultReconCustomers();

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
	                      
Declare @MaxDate datetime
Select @MaxDate = max(date) From FundClientPosition
where Date <= cast(GetDate() as Date)

Declare @cf_1314 int
Declare @cf_1316 int
Declare @cf_1318 int
Declare @cf_1320 int
Declare @cf_test int
Declare @cf_test2 int

Select @cf_1314 = count(FundClientPK) from FundClient where status in (1,2)
and EntryUsersID = 'RDO'


Select @cf_1320 = count(FundClientPK) from FundClient where status in (1,2)
and EntryUsersID = 'RDO'
and isnull(SID,'') = '' and isnull(IFUACode,'') = ''


Select @cf_1316 = count(FundClientPositionPK)
from FundClientPosition A
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
where A.Date = @MaxDate
and A.FundPK in
(
	Select FundPK from Fund Where status in (1,2) and IsPublic = 1
)
and B.EntryUsersID = 'RDO' and A.UnitAmount > 0


Select @cf_test = count(B.FundClientPK)
from FundClientPosition A
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
where A.Date = @MaxDate
and A.FundPK in
(
	Select FundPK from Fund Where status in (1,2) and IsPublic = 1
)
and B.EntryUsersID = 'RDO' and A.UnitAmount = 0 

Select @cf_test2 = count(A.FundClientPK) from fundclient A
where A.EntryUsersID = 'RDO' and A.FundClientPK not in (select distinct (FundClientPK) from FundClientPosition where date = @MaxDate)

select @cf_1318 = @cf_test + @cf_test2

Select 'ReconCustomer' Task, Format(GetDate(),'yyyy-MM-dd') date_start
,Format(GetDate(),'HH:mm:ss') time_start
,isnull(@cf_1314,0) cf_1314
,isnull(@cf_1316,0) cf_1316
,isnull(@cf_1318,0) cf_1318
,isnull(@cf_1320,0) cf_1320

                    ";
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {

                                _m.subject = "Recon-TA";
                                _m.activitytype = "Recon - Customers";
                                _m.date_start = dr0["date_start"].ToString();
                                _m.time_start = dr0["time_start"].ToString();
                                _m.due_date = dr0["date_start"].ToString();
                                _m.time_end = dr0["time_start"].ToString();
                                _m.assigned_user_id = "19x52";
                                _m.description = "NA";
                                _m.eventstatus = "Planned";
                                _m.cf_1314 = dr0["cf_1314"].ToString();
                                _m.cf_1316 = dr0["cf_1316"].ToString();
                                _m.cf_1318 = dr0["cf_1318"].ToString();
                                _m.cf_1320 = dr0["cf_1320"].ToString();

                            }

                        }
                        else
                        {
                            return "NO DATA";
                        }
                    }
                }
            }
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string _el = "";
            JsonSerializer serializer = new JsonSerializer();
            _el = serializer.Serialize(_m);

            Request.AddParameter("operation", "create");
            Request.AddParameter("sessionName", _loginReq.result.sessionName);
            Request.AddParameter("element", _el);
            Request.AddParameter("elementType", "Events");
            var Execute = Client.Execute(Request);


            string filePath = Tools.SInvestTextPath + "LogSwivel_ReconCustomer.txt";
            FileInfo txtFile = new FileInfo(filePath);

            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log For recon Customer");
                }
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(_m.activitytype + " " + _m.date_start + " " + _m.time_start + " " + _m.cf_1314 + " " + _m.cf_1316 + " " + _m.cf_1318 + " " + _m.cf_1320 + " " + Execute.Content.ToString());

            }

            return "OK";
        }

        public string HelpDeskCrete(int _source)
        {
            ResponLoginRequest _loginReq = new ResponLoginRequest();
            _loginReq = LoginRequest();

            List<ResultHelpDeskCreate> _element = new List<ResultHelpDeskCreate>();

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
	                        Select PK,ticketpriorities,ticketstatus,ticketcategories,ticket_title 
	                        ,description,source,cf_962,cf_968,cf_976,cf_1267
	                        From ZRDO_SWIVEL_HELPDESK
	                        Where BitSent = 0 and RadSource = @Source
                    ";
                    cmd1.Parameters.AddWithValue("@Source", _source);
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {
                                ResultHelpDeskCreate _m = new ResultHelpDeskCreate();
                                _m.PK = Convert.ToInt64(dr0["PK"]);
                                _m.assigned_user_id = "19x1";
                                _m.ticketpriorities = dr0["ticketpriorities"].ToString();
                                _m.ticketstatus = dr0["ticketstatus"].ToString();
                                _m.ticketcategories = dr0["ticketcategories"].ToString();
                                _m.ticket_title = dr0["ticket_title"].ToString();
                                _m.description = dr0["description"].ToString();
                                _m.source = dr0["source"].ToString();
                                _m.cf_962 = dr0["cf_962"].ToString();
                                _m.cf_968 = dr0["cf_968"].ToString();
                                _m.cf_976 = dr0["cf_976"].ToString();
                                _m.cf_1267 = dr0["cf_1267"].ToString();
                                _element.Add(_m);
                            }

                        }
                        else {
                            return "NO DATA";
                        }
                    }
                }
            }
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            string filePath = Tools.SInvestTextPath + "LogSwivel_HelpDesk.txt";
            FileInfo txtFile = new FileInfo(filePath);

            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log For Help Desk");
                }
            }
            using (SqlConnection con1 = new SqlConnection(Tools.conString))
            {
                con1.Open();
                using (SqlCommand cmd2 = con1.CreateCommand())
                {
                    
                    cmd2.CommandText = @"Update ZRDO_SWIVEL_HELPDESK set BitSent = 1 , Response = @Response where PK = @PK";
                    cmd2.Parameters.Add("@PK", System.Data.SqlDbType.BigInt);
                    cmd2.Parameters.Add("@Response", System.Data.SqlDbType.NVarChar);

                    foreach (var a in _element)
                    {
                        string _el = "";
                        JsonSerializer serializer = new JsonSerializer();
                        _el = serializer.Serialize(a);

                        Request.AddParameter("operation", "create");
                        Request.AddParameter("sessionName", _loginReq.result.sessionName);
                        Request.AddParameter("element", _el);
                        Request.AddParameter("elementType", "HelpDesk");
                        var Execute = Client.Execute(Request);

                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            sw.WriteLine(a.ticketstatus + " " + a.ticketcategories + " " + a.ticket_title + " " + a.description + " " + a.source + " " + a.cf_962 + " " + a.cf_968 + " " + a.cf_976 + " " + a.cf_1267 + " " + Execute.Content.ToString());
                        }

                        
                        Request.Parameters.Clear();                        
                       
                            cmd2.Parameters["@PK"].Value = a.PK;
                            cmd2.Parameters["@Response"].Value = a.ticketstatus + " " + a.ticketcategories + " " + a.ticket_title + " " + a.description + " " + a.source + " " + a.cf_962 + " " + a.cf_968 + " " + a.cf_976 + " " + a.cf_1267 + " " + Execute.Content.ToString();
                            cmd2.ExecuteNonQuery();
                       
                    }
                    
                }
            }
            return "OK";
        }

        public string ClientRevise()
        {
            ResponLoginRequest _loginReq = new ResponLoginRequest();
            _loginReq = LoginRequest();

            List<ResultClientRevise> _element = new List<ResultClientRevise>();
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
                        Select SID,IFUACode,FrontID,Format(OpeningDateSinvest,'yyyy-MM-dd') OpeningDate From [IFUASIDFromSinvestUploadLog]
                    ";
                    using (SqlDataReader dr0 = cmd1.ExecuteReader())
                    {
                        if (dr0.HasRows)
                        {
                            while (dr0.Read())
                            {
                                ResultClientRevise _m = new ResultClientRevise();
                                _m.id = dr0["FrontID"].ToString();
                                _m.cf_1204 = "Ready";
                                _m.cf_1032 = dr0["SID"].ToString();
                                _m.cf_1030 = dr0["IFUACode"].ToString();
                                _m.cf_1034 = dr0["OpeningDate"].ToString();
                                _element.Add(_m);
                            }
                            
                        }
                    }

                }
            }
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string filePath = Tools.SInvestTextPath + "LogSwivel_ClientRevise.txt";
            FileInfo txtFile = new FileInfo(filePath);

            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log For Client Revise");
                }
            }

            foreach (var a in _element)
            {
                string _el = "";
                JsonSerializer serializer = new JsonSerializer();
                _el = serializer.Serialize(a);

                Request.AddParameter("operation", "revisecontactbycifid");
                Request.AddParameter("sessionName", _loginReq.result.sessionName);
                Request.AddParameter("element", _el);
                Request.AddParameter("cifid", a.id);
                var Execute = Client.Execute(Request);

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(a.id + " " + a.cf_1204 + " " + a.cf_1032 + " " + a.cf_1030 + " " + a.cf_1034 + " " + Execute.Content.ToString());
                }


                Request.Parameters.Clear();
                
            }

         
           
            return "OK";
        }

        public string Computemd5(string rawData)
        {
            // Create a SHA256   
            using (MD5 md5 = MD5.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public ResponChallengeRequest ChallengeRequest()
        {
            ResponChallengeRequest _respon = new ResponChallengeRequest();
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php?operation=getchallenge&username=" + _userName, Method.GET);
            var Execute = Client.Execute(Request);
            JsonDeserializer deserial = new JsonDeserializer();
            _respon = deserial.Deserialize<ResponChallengeRequest>(Execute);
            return _respon;
        }

        public ResponLoginRequest LoginRequest()
        {
            ResponChallengeRequest _cr = new ResponChallengeRequest();
            _cr = ChallengeRequest();
            string _hash = Computemd5(_cr.result.token + _accessKey);
            ResponLoginRequest _respon = new ResponLoginRequest();
            var Client = new RestClient(_url);
            var Request = new RestRequest("icrm/webservice.php", Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Request.AddParameter("operation", "login");
            Request.AddParameter("username", _userName);
            Request.AddParameter("accessKey", _hash);
            var Execute = Client.Execute(Request);
            JsonDeserializer deserial = new JsonDeserializer();
            _respon = deserial.Deserialize<ResponLoginRequest>(Execute);
            return _respon;
        }


    }
}
