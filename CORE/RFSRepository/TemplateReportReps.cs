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
    public class TemplateReportReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[TemplateReport] " +
                            "([TemplateReportPK],[HistoryPK],[Status],[ReportName],[AccountPK],[Row],[Col],[Operator],";

        string _paramaterCommand = "@ReportName,@AccountPK,@Row,@Col,@Operator,";

        //2
        private TemplateReport setTemplateReport(SqlDataReader dr)
        {
            TemplateReport M_TemplateReport = new TemplateReport();
            M_TemplateReport.TemplateReportPK = Convert.ToInt32(dr["TemplateReportPK"]);
            M_TemplateReport.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TemplateReport.Status = Convert.ToInt32(dr["Status"]);
            M_TemplateReport.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TemplateReport.Notes = Convert.ToString(dr["Notes"]);
            M_TemplateReport.ReportName = dr["ReportName"].ToString();
            M_TemplateReport.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_TemplateReport.AccountID = Convert.ToString(dr["AccountID"]);
            M_TemplateReport.AccountName = Convert.ToString(dr["AccountName"]);
            M_TemplateReport.Row = Convert.ToInt32(dr["Row"]);
            M_TemplateReport.Col = Convert.ToInt32(dr["Col"]);
            M_TemplateReport.Operator = dr["Operator"].ToString();
            M_TemplateReport.OperatorDesc = dr["OperatorDesc"].ToString();
            M_TemplateReport.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TemplateReport.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TemplateReport.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TemplateReport.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TemplateReport.EntryTime = dr["EntryTime"].ToString();
            M_TemplateReport.UpdateTime = dr["UpdateTime"].ToString();
            M_TemplateReport.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TemplateReport.VoidTime = dr["VoidTime"].ToString();
            M_TemplateReport.DBUserID = dr["DBUserID"].ToString();
            M_TemplateReport.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TemplateReport.LastUpdate = dr["LastUpdate"].ToString();
            M_TemplateReport.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_TemplateReport;
        }

        public List<TemplateReport> TemplateReport_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TemplateReport> L_TemplateReport = new List<TemplateReport>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                              Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID AccountID,B.Name AccountName, Case when Operator = 1 then '+' else case when Operator = 2 then '-' end end OperatorDesc,* from TemplateReport A left join 
                                Account B on A.AccountPK = B.AccountPK and B.status = 2                            
                              where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                              Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID AccountID,B.Name AccountName, Case when Operator = 1 then '+' else case when Operator = 2 then '-' end end OperatorDesc, * from TemplateReport A left join 
                                Account B on A.AccountPK = B.AccountPK and B.status = 2
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TemplateReport.Add(setTemplateReport(dr));
                                }
                            }
                            return L_TemplateReport;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TemplateReport_Add(TemplateReport _TemplateReport, bool _havePrivillege)
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
                                 "Select isnull(max(TemplateReportPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from TemplateReport";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(TemplateReportPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from TemplateReport";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                        cmd.Parameters.AddWithValue("@AccountPK", _TemplateReport.AccountPK);
                        cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                        cmd.Parameters.AddWithValue("@Col", _TemplateReport.Col);
                        cmd.Parameters.AddWithValue("@Operator", _TemplateReport.Operator);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TemplateReport.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TemplateReport");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TemplateReport_Update(TemplateReport _TemplateReport, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_TemplateReport.TemplateReportPK, _TemplateReport.HistoryPK, "TemplateReport");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TemplateReport set status=2,Notes=@Notes,ReportName=@ReportName,AccountPK=@AccountPK,Row=@Row,Col=@Col,Operator=@Operator," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where TemplateReportPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _TemplateReport.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                            cmd.Parameters.AddWithValue("@Notes", _TemplateReport.Notes);
                            cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                            cmd.Parameters.AddWithValue("@AccountPK", _TemplateReport.AccountPK);
                            cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                            cmd.Parameters.AddWithValue("@Col", _TemplateReport.Col);
                            cmd.Parameters.AddWithValue("@Operator", _TemplateReport.Operator);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateReport.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TemplateReport set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TemplateReportPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateReport.EntryUsersID);
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
                                cmd.CommandText = "Update TemplateReport set Notes=@Notes,ReportName=@ReportName,AccountPK=@AccountPK,Row=@Row,Col=@Col,Operator=@Operator,"+
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where TemplateReportPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateReport.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                                cmd.Parameters.AddWithValue("@Notes", _TemplateReport.Notes);
                                cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                                cmd.Parameters.AddWithValue("@AccountPK", _TemplateReport.AccountPK);
                                cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                                cmd.Parameters.AddWithValue("@Col", _TemplateReport.Col);
                                cmd.Parameters.AddWithValue("@Operator", _TemplateReport.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateReport.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TemplateReport.TemplateReportPK, "TemplateReport");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TemplateReport where TemplateReportPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateReport.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                                cmd.Parameters.AddWithValue("@AccountPK", _TemplateReport.AccountPK);
                                cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                                cmd.Parameters.AddWithValue("@Col", _TemplateReport.Col);
                                cmd.Parameters.AddWithValue("@Operator", _TemplateReport.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateReport.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TemplateReport set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where TemplateReportPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TemplateReport.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateReport.HistoryPK);
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

        public void TemplateReport_Approved(TemplateReport _TemplateReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateReport set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where TemplateReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateReport.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TemplateReport set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TemplateReportPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateReport.ApprovedUsersID);
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

        public void TemplateReport_Reject(TemplateReport _TemplateReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateReport set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where TemplateReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateReport.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TemplateReport set status= 2,lastupdate=@lastupdate where TemplateReportPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
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

        public void TemplateReport_Void(TemplateReport _TemplateReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateReport set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where TemplateReportPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateReport.TemplateReportPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateReport.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateReport.VoidUsersID);
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

        public List<TemplateReport> TemplateReport_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TemplateReport> L_AccountBudget = new List<TemplateReport>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  Distinct ReportName FROM [TemplateReport]  where status in (1,2) order by ReportName";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TemplateReport M_TemplateReport = new TemplateReport();
                                    M_TemplateReport.ReportName = Convert.ToString(dr["ReportName"]);
                                    L_AccountBudget.Add(M_TemplateReport);
                                }

                            }
                            return L_AccountBudget;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool CheckData(string _templateName, int _accountPK, int _row)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        if Exists(Select * From TemplateReport where ReportName = @ReportName and Row = @Row and AccountPK = @AccountPK and status = 2)
                        BEGIN
                            select 1 Result
                        END
                        ELSE
                        BEGIN
                             select 0 Result
                        END
                        ";
                        cmd.Parameters.AddWithValue("@ReportName", _templateName);
                        cmd.Parameters.AddWithValue("@Row", _row);
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);
                            }
                        }
                        return false;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void TemplateReport_InsertRow(string _usersID, TemplateReport _TemplateReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                            UPDATE TemplateReport
                            SET Row = Row + 1
                            where Row >= @Row and ReportName = @ReportName

                            Insert Into TemplateReport ([TemplateReportPK],[HistoryPK],[Status],[ReportName],[AccountPK],[Row],[Col],[Operator],[EntryUsersID],[EntryTime],lastupdate)
                            Select isnull(max(TemplateReportPk),0) + 1,1,2,@ReportName,@AccountPK,@Row,@Col,@Operator,@EntryUsersID,@lastupdate,@lastupdate from TemplateReport";
                            cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                            cmd.Parameters.AddWithValue("@AccountPK", _TemplateReport.AccountPK);
                            cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                            cmd.Parameters.AddWithValue("@Col", _TemplateReport.Col);
                            cmd.Parameters.AddWithValue("@Operator", _TemplateReport.Operator);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.Parameters.AddWithValue("@EntryUsersID", _usersID);
                            cmd.ExecuteNonQuery();
                        }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void TemplateReport_DeleteRow(string _usersID, TemplateReport _TemplateReport)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DELETE TemplateReport where ReportName = @ReportName and Row = @Row and status = 2
                            
                            UPDATE TemplateReport
                            SET Row = Row - 1
                            where Row >= @Row and ReportName = @ReportName";
                        cmd.Parameters.AddWithValue("@ReportName", _TemplateReport.ReportName);
                        cmd.Parameters.AddWithValue("@Row", _TemplateReport.Row);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _usersID);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
    }
}