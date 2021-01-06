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
    public class BankInterestSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BankInterestSetup] " +
                            "([BankInterestSetupPK],[HistoryPK],[Status],[Date],[BankBranchPK],[MinimumBalance],[InterestPercent],[InterestDays],";
        string _paramaterCommand = "@Date,@BankBranchPK,@MinimumBalance,@InterestPercent,@InterestDays,";

        //2
        private BankInterestSetup setBankInterestSetup(SqlDataReader dr)
        {
            BankInterestSetup M_BankInterestSetup = new BankInterestSetup();
            M_BankInterestSetup.BankInterestSetupPK = Convert.ToInt32(dr["BankInterestSetupPK"]);
            M_BankInterestSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BankInterestSetup.Status = Convert.ToInt32(dr["Status"]);
            M_BankInterestSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BankInterestSetup.Notes = Convert.ToString(dr["Notes"]);
            M_BankInterestSetup.Date = dr["Date"].ToString();
            M_BankInterestSetup.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
            M_BankInterestSetup.BankBranchID = Convert.ToString(dr["BankBranchID"]);
            M_BankInterestSetup.MinimumBalance = Convert.ToDecimal(dr["MinimumBalance"]);
            M_BankInterestSetup.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
            M_BankInterestSetup.InterestDays = Convert.ToDecimal(dr["InterestDays"]);
            M_BankInterestSetup.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_BankInterestSetup.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_BankInterestSetup.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_BankInterestSetup.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_BankInterestSetup.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_BankInterestSetup.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_BankInterestSetup.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_BankInterestSetup.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_BankInterestSetup.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_BankInterestSetup.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_BankInterestSetup.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_BankInterestSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BankInterestSetup;
        }

        public List<BankInterestSetup> BankInterestSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankInterestSetup> L_BankInterestSetup = new List<BankInterestSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" Select case when BIS.status=1 then 'PENDING' else Case When BIS.status = 2 then 'APPROVED' else Case when BIS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc ,'All Bank Branch' BankBranchID, BIS.* from BankInterestSetup BIS left join 
                             BankBranch B ON BIS.BankBranchPK = B.BankBranchPK and B.Status = 2 
                             where BIS.status = @status and isnull(BIS.BankBranchPK,0) = 0
                             union all
                             Select case when BIS.status=1 then 'PENDING' else Case When BIS.status = 2 then 'APPROVED' else Case when BIS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc ,B.ID  BankBranchID,BIS.* from BankInterestSetup BIS left join 
                             BankBranch B ON BIS.BankBranchPK = B.BankBranchPK and B.Status = 2 
                             where BIS.status = @status and isnull(BIS.BankBranchPK,0) <> 0

";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @" Select case when BIS.status=1 then 'PENDING' else Case When BIS.status = 2 then 'APPROVED' else Case when BIS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc ,'All Bank Branch' BankBranchID, BIS.* from BankInterestSetup BIS left join 
                             BankBranch B ON BIS.BankBranchPK = B.BankBranchPK and B.Status = 2 
                             where isnull(BIS.BankBranchPK,0) = 0
                             union all
                             Select case when BIS.status=1 then 'PENDING' else Case When BIS.status = 2 then 'APPROVED' else Case when BIS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc ,B.ID  BankBranchID,BIS.* from BankInterestSetup BIS left join 
                             BankBranch B ON BIS.BankBranchPK = B.BankBranchPK and B.Status = 2 
                             where isnull(BIS.BankBranchPK,0) <> 0   ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BankInterestSetup.Add(setBankInterestSetup(dr));
                                }
                            }
                            return L_BankInterestSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BankInterestSetup_Add(BankInterestSetup _BankInterestSetup, bool _havePrivillege)
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
                                 "Select isnull(max(BankInterestSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from BankInterestSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BankInterestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BankInterestSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from BankInterestSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _BankInterestSetup.Date);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _BankInterestSetup.BankBranchPK);
                        cmd.Parameters.AddWithValue("@MinimumBalance", _BankInterestSetup.MinimumBalance);
                        cmd.Parameters.AddWithValue("@InterestPercent", _BankInterestSetup.InterestPercent);
                        cmd.Parameters.AddWithValue("@InterestDays", _BankInterestSetup.InterestDays);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BankInterestSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BankInterestSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int BankInterestSetup_Update(BankInterestSetup _BankInterestSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BankInterestSetup.BankInterestSetupPK, _BankInterestSetup.HistoryPK, "BankInterestSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update BankInterestSetup set status=2,Date=@Date,BankBranchPK=@BankBranchPK,MinimumBalance=@MinimumBalance,InterestPercent=@InterestPercent,InterestDays=@InterestDays,
                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                where BankInterestSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BankInterestSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _BankInterestSetup.Notes);
                            cmd.Parameters.AddWithValue("@Date", _BankInterestSetup.Date);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _BankInterestSetup.BankBranchPK);
                            cmd.Parameters.AddWithValue("@MinimumBalance", _BankInterestSetup.MinimumBalance);
                            cmd.Parameters.AddWithValue("@InterestPercent", _BankInterestSetup.InterestPercent);
                            cmd.Parameters.AddWithValue("@InterestDays", _BankInterestSetup.InterestDays);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BankInterestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BankInterestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BankInterestSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankInterestSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BankInterestSetup.EntryUsersID);
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
                                cmd.CommandText = @"Update BankInterestSetup set Notes=@Notes,Date=@Date,BankBranchPK=@BankBranchPK,MinimumBalance=@MinimumBalance,InterestPercent=@InterestPercent,InterestDays=@InterestDays,
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where BankInterestSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BankInterestSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _BankInterestSetup.Notes);
                                cmd.Parameters.AddWithValue("@Date", _BankInterestSetup.Date);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _BankInterestSetup.BankBranchPK);
                                cmd.Parameters.AddWithValue("@MinimumBalance", _BankInterestSetup.MinimumBalance);
                                cmd.Parameters.AddWithValue("@InterestPercent", _BankInterestSetup.InterestPercent);
                                cmd.Parameters.AddWithValue("@InterestDays", _BankInterestSetup.InterestDays);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BankInterestSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BankInterestSetup.BankInterestSetupPK, "BankInterestSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BankInterestSetup where BankInterestSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BankInterestSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _BankInterestSetup.Date);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _BankInterestSetup.BankBranchPK);
                                cmd.Parameters.AddWithValue("@MinimumBalance", _BankInterestSetup.MinimumBalance);
                                cmd.Parameters.AddWithValue("@InterestPercent", _BankInterestSetup.InterestPercent);
                                cmd.Parameters.AddWithValue("@InterestDays", _BankInterestSetup.InterestDays);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BankInterestSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BankInterestSetup set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where BankInterestSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BankInterestSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BankInterestSetup.HistoryPK);
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

        public void BankInterestSetup_Approved(BankInterestSetup _BankInterestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankInterestSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where BankInterestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BankInterestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BankInterestSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankInterestSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankInterestSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BankInterestSetup.ApprovedUsersID);
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

        public void BankInterestSetup_Reject(BankInterestSetup _BankInterestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankInterestSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BankInterestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BankInterestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BankInterestSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankInterestSetup set status= 2,lastupdate=@lastupdate where BankInterestSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
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

        public void BankInterestSetup_Void(BankInterestSetup _BankInterestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankInterestSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where BankInterestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BankInterestSetup.BankInterestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BankInterestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BankInterestSetup.VoidUsersID);
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