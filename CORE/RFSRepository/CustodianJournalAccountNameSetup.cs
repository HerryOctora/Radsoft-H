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
    public class CustodianJournalAccountNameSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CustodianJournalAccountNameSetup] " +
                            "([CustodianJournalAccountNameSetupPK],[HistoryPK],[Status],[FundPK],[FundJournalAccountPK],[Name],";
        string _paramaterCommand = "@FundPK,@FundJournalAccountPK,@Name,";

        //2
        private CustodianJournalAccountNameSetup setCustodianJournalAccountNameSetup(SqlDataReader dr)
        {
            CustodianJournalAccountNameSetup M_CustodianJournalAccountNameSetup = new CustodianJournalAccountNameSetup();
            M_CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK = Convert.ToInt32(dr["CustodianJournalAccountNameSetupPK"]);
            M_CustodianJournalAccountNameSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CustodianJournalAccountNameSetup.Status = Convert.ToInt32(dr["Status"]);
            M_CustodianJournalAccountNameSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CustodianJournalAccountNameSetup.Notes = Convert.ToString(dr["Notes"]);
            M_CustodianJournalAccountNameSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_CustodianJournalAccountNameSetup.FundID = Convert.ToString(dr["FundID"]);
            M_CustodianJournalAccountNameSetup.FundName = Convert.ToString(dr["FundName"]);
            M_CustodianJournalAccountNameSetup.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_CustodianJournalAccountNameSetup.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_CustodianJournalAccountNameSetup.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_CustodianJournalAccountNameSetup.Name = dr["Name"].ToString();
            M_CustodianJournalAccountNameSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CustodianJournalAccountNameSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CustodianJournalAccountNameSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CustodianJournalAccountNameSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CustodianJournalAccountNameSetup.EntryTime = dr["EntryTime"].ToString();
            M_CustodianJournalAccountNameSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_CustodianJournalAccountNameSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CustodianJournalAccountNameSetup.VoidTime = dr["VoidTime"].ToString();
            M_CustodianJournalAccountNameSetup.DBUserID = dr["DBUserID"].ToString();
            M_CustodianJournalAccountNameSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CustodianJournalAccountNameSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_CustodianJournalAccountNameSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CustodianJournalAccountNameSetup;
        }

        public List<CustodianJournalAccountNameSetup> CustodianJournalAccountNameSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianJournalAccountNameSetup> L_CustodianJournalAccountNameSetup = new List<CustodianJournalAccountNameSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID FundJournalAccountID,A.Name FundJournalAccountName, F.ID FundID,F.Name FundName,  CR.* from CustodianJournalAccountNameSetup CR left join " +
                            "FundJournalAccount A on CR.FundJournalAccountPK = A.FundJournalAccountPK and A.status =2 left join " +
                            "Fund F on CR.FundPK = F.FundPK and F.status = 2 " +
                            "where CR.status = @status and A.status = 2  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID FundJournalAccountID,A.Name FundJournalAccountName, F.ID FundID,F.Name FundName,  CR.* from CustodianJournalAccountNameSetup CR left join " +
                            "FundJournalAccount A on CR.FundJournalAccountPK = A.FundJournalAccountPK and A.status =2 left join " +
                            "Fund F on CR.FundPK = F.FundPK and F.status = 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CustodianJournalAccountNameSetup.Add(setCustodianJournalAccountNameSetup(dr));
                                }
                            }
                            return L_CustodianJournalAccountNameSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CustodianJournalAccountNameSetup_Add(CustodianJournalAccountNameSetup _CustodianJournalAccountNameSetup, bool _havePrivillege)
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
                                 "Select isnull(max(CustodianJournalAccountNameSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from CustodianJournalAccountNameSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CustodianJournalAccountNameSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from CustodianJournalAccountNameSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _CustodianJournalAccountNameSetup.FundPK);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _CustodianJournalAccountNameSetup.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@Name", _CustodianJournalAccountNameSetup.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CustodianJournalAccountNameSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int CustodianJournalAccountNameSetup_Update(CustodianJournalAccountNameSetup _CustodianJournalAccountNameSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, _CustodianJournalAccountNameSetup.HistoryPK, "CustodianJournalAccountNameSetup");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CustodianJournalAccountNameSetup set status=2, Notes=@Notes,FundPK=@FundPK,FundJournalAccountPK=@FundJournalAccountPK,Name=@Name," +
                                    "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CustodianJournalAccountNameSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CustodianJournalAccountNameSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _CustodianJournalAccountNameSetup.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _CustodianJournalAccountNameSetup.FundPK);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _CustodianJournalAccountNameSetup.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@Name", _CustodianJournalAccountNameSetup.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CustodianJournalAccountNameSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianJournalAccountNameSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
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
                                cmd.CommandText = "Update CustodianJournalAccountNameSetup set Notes=@Notes,FundPK=@FundPK,FundJournalAccountPK=@FundJournalAccountPK,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CustodianJournalAccountNameSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustodianJournalAccountNameSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _CustodianJournalAccountNameSetup.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _CustodianJournalAccountNameSetup.FundPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _CustodianJournalAccountNameSetup.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@Name", _CustodianJournalAccountNameSetup.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK, "CustodianJournalAccountNameSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CustodianJournalAccountNameSetup where CustodianJournalAccountNameSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustodianJournalAccountNameSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _CustodianJournalAccountNameSetup.FundPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _CustodianJournalAccountNameSetup.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@Name", _CustodianJournalAccountNameSetup.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CustodianJournalAccountNameSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CustodianJournalAccountNameSetup set status= 4,Notes=@Notes, " +
                                    "lastupdate=@lastupdate where CustodianJournalAccountNameSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CustodianJournalAccountNameSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustodianJournalAccountNameSetup.HistoryPK);
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

        public void CustodianJournalAccountNameSetup_Approved(CustodianJournalAccountNameSetup _CustodianJournalAccountNameSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianJournalAccountNameSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CustodianJournalAccountNameSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustodianJournalAccountNameSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustodianJournalAccountNameSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustodianJournalAccountNameSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianJournalAccountNameSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustodianJournalAccountNameSetup.ApprovedUsersID);
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

        public void CustodianJournalAccountNameSetup_Reject(CustodianJournalAccountNameSetup _CustodianJournalAccountNameSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianJournalAccountNameSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CustodianJournalAccountNameSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustodianJournalAccountNameSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustodianJournalAccountNameSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustodianJournalAccountNameSetup set status= 2,lastupdate=@lastupdate where CustodianJournalAccountNameSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
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

        public void CustodianJournalAccountNameSetup_Void(CustodianJournalAccountNameSetup _CustodianJournalAccountNameSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianJournalAccountNameSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CustodianJournalAccountNameSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustodianJournalAccountNameSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustodianJournalAccountNameSetup.VoidUsersID);
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

        public List<CustodianJournalAccountNameSetupCombo> CustodianJournalAccountNameSetup_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianJournalAccountNameSetupCombo> L_CustodianJournalAccountNameSetup = new List<CustodianJournalAccountNameSetupCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CustodianJournalAccountNameSetupPK, Name, FundJournalAccountPK FROM [CustodianJournalAccountNameSetup]  where status = 2 order by Name";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustodianJournalAccountNameSetupCombo M_CustodianJournalAccountNameSetup = new CustodianJournalAccountNameSetupCombo();
                                    M_CustodianJournalAccountNameSetup.CustodianJournalAccountNameSetupPK = Convert.ToInt32(dr["CustodianJournalAccountNameSetupPK"]);
                                    M_CustodianJournalAccountNameSetup.Name = Convert.ToString(dr["Name"]);
                                    M_CustodianJournalAccountNameSetup.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    L_CustodianJournalAccountNameSetup.Add(M_CustodianJournalAccountNameSetup);
                                }

                            }
                            return L_CustodianJournalAccountNameSetup;
                        }
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