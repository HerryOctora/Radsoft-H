using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace RFSRepository
{
    public class NavMappingReportReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[NavMappingReport] " +
                            "([NavMappingReportPK],[HistoryPK],[Status],[BankBranchPK],[FundJournalAccountPK],[Row],[Description]," ;
        string _paramaterCommand = "@BankBranchPK,@FundJournalAccountPK,@Row,@Description,";

        //2
        private NavMappingReport setNavMappingReport(SqlDataReader dr)
        {
            NavMappingReport M_NavMappingReport = new NavMappingReport();
            M_NavMappingReport.NavMappingReportPK = Convert.ToInt32(dr["NavMappingReportPK"]);
            M_NavMappingReport.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_NavMappingReport.Status = Convert.ToInt32(dr["Status"]);
            M_NavMappingReport.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_NavMappingReport.Notes = Convert.ToString(dr["Notes"]);
            M_NavMappingReport.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
            M_NavMappingReport.BankBranchID = Convert.ToString(dr["BankBranchID"]);
            M_NavMappingReport.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_NavMappingReport.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_NavMappingReport.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_NavMappingReport.Row = Convert.ToInt32(dr["Row"]);

            M_NavMappingReport.Description = Convert.ToString(dr["Description"]);
            M_NavMappingReport.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_NavMappingReport.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_NavMappingReport.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_NavMappingReport.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_NavMappingReport.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_NavMappingReport.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_NavMappingReport.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_NavMappingReport.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_NavMappingReport.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_NavMappingReport.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_NavMappingReport.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_NavMappingReport.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_NavMappingReport;
        }

        public List<NavMappingReport> NavMappingReport_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<NavMappingReport> L_NavMappingReport = new List<NavMappingReport>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" 

                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundJournalAccountID,B.Name FundJournalAccountName,'All Bank Branch' BankBranchID,  A.*
                            from NavMappingReport A 
                            left join FundJournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status = 2     
                            where A.status = @status and isnull(A.BankBranchPK,0) = 0

                            Union All

                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            D.ID FundJournalAccountID,D.Name FundJournalAccountName,B.ID BankBranchID,  A.*
                            from NavMappingReport A 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 and B.Type = 2
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status = 2     
                            where A.status = @status and isnull(A.BankBranchPK,0) <> 0  ";

                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @" 

                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundJournalAccountID,B.Name FundJournalAccountName,'All Bank Branch' BankBranchID,  A.*
                            from NavMappingReport A 
                            left join FundJournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status = 2     
                            where isnull(A.BankBranchPK,0) = 0

                            Union All

                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            D.ID FundJournalAccountID,D.Name FundJournalAccountName,B.ID BankBranchID,  A.*
                            from NavMappingReport A 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 and B.Type = 2
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status = 2     
                            where isnull(A.BankBranchPK,0) <> 0 ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_NavMappingReport.Add(setNavMappingReport(dr));
                                }
                            }
                            return L_NavMappingReport;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int NavMappingReport_Add(NavMappingReport _NavMappingReport, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(NavMappingReportPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from NavMappingReport";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _NavMappingReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(NavMappingReportPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from NavMappingReport";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _NavMappingReport.BankBranchPK);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _NavMappingReport.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@Row", _NavMappingReport.Row);
                        cmd.Parameters.AddWithValue("@Description", _NavMappingReport.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _NavMappingReport.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "NavMappingReport");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int NavMappingReport_Update(NavMappingReport _NavMappingReport, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_NavMappingReport.NavMappingReportPK, _NavMappingReport.HistoryPK, "NavMappingReport");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update NavMappingReport set status=2,Notes=@Notes,BankBranchPK=@BankBranchPK,FundJournalAccountPK=@FundJournalAccountPK,Row=@Row,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where NavMappingReportPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _NavMappingReport.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                            cmd.Parameters.AddWithValue("@Notes", _NavMappingReport.Notes);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _NavMappingReport.BankBranchPK);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _NavMappingReport.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@Row", _NavMappingReport.Row);
                            cmd.Parameters.AddWithValue("@Description", _NavMappingReport.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _NavMappingReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _NavMappingReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update NavMappingReport set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where NavMappingReportPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _NavMappingReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        return 0;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update NavMappingReport set Notes=@Notes,BankBranchPK=@BankBranchPK,FundJournalAccountPK=@FundJournalAccountPK,Row=@Row,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where NavMappingReportPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _NavMappingReport.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _NavMappingReport.BankBranchPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _NavMappingReport.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@Row", _NavMappingReport.Row);
                                cmd.Parameters.AddWithValue("@Description", _NavMappingReport.Description);
                                cmd.Parameters.AddWithValue("@Notes", _NavMappingReport.Notes);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _NavMappingReport.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_NavMappingReport.NavMappingReportPK, "NavMappingReport");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From NavMappingReport where NavMappingReportPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _NavMappingReport.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _NavMappingReport.BankBranchPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _NavMappingReport.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@Row", _NavMappingReport.Row);
                                cmd.Parameters.AddWithValue("@Description", _NavMappingReport.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _NavMappingReport.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update NavMappingReport set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where NavMappingReportPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _NavMappingReport.Notes);
                                cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _NavMappingReport.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return _newHisPK;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void NavMappingReport_Approved(NavMappingReport _NavMappingReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update NavMappingReport set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where NavMappingReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _NavMappingReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _NavMappingReport.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update NavMappingReport set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where NavMappingReportPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _NavMappingReport.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void NavMappingReport_Reject(NavMappingReport _NavMappingReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update NavMappingReport set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where NavMappingReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _NavMappingReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _NavMappingReport.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update NavMappingReport set status= 2,lastupdate=@lastupdate where NavMappingReportPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void NavMappingReport_Void(NavMappingReport _NavMappingReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update NavMappingReport set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where NavMappingReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _NavMappingReport.NavMappingReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _NavMappingReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _NavMappingReport.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )



    }
}