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
    public class FinanceSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FinanceSetup] " +
                            "([FinanceSetupPK],[HistoryPK],[Status],[CashierReferencePrefix],[JournalMonthOpen],[PeriodPK],";

        string _paramaterCommand = "@CashierReferencePrefix,@JournalMonthOpen,@PeriodPK,";

        //2
        private FinanceSetup setFinanceSetup(SqlDataReader dr)
        {
            FinanceSetup M_FinanceSetup = new FinanceSetup();
            M_FinanceSetup.FinanceSetupPK = Convert.ToInt32(dr["FinanceSetupPK"]);
            M_FinanceSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FinanceSetup.Status = Convert.ToInt32(dr["Status"]);
            M_FinanceSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FinanceSetup.Notes = Convert.ToString(dr["Notes"]);
            M_FinanceSetup.CashierReferencePrefix = dr["CashierReferencePrefix"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashierReferencePrefix"]);
            M_FinanceSetup.JournalMonthOpen = dr["JournalMonthOpen"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["JournalMonthOpen"]);
            M_FinanceSetup.JournalMonthOpenDesc = dr["JournalMonthOpenDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["JournalMonthOpenDesc"]);
            M_FinanceSetup.PeriodPK = dr["PeriodPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PeriodPK"]);
            M_FinanceSetup.PeriodPKDesc = dr["PeriodPKDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PeriodPKDesc"]);
            M_FinanceSetup.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryUsersID"]);
            M_FinanceSetup.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateUsersID"]);
            M_FinanceSetup.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedUsersID"]);
            M_FinanceSetup.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidUsersID"]);
            M_FinanceSetup.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryTime"]);
            M_FinanceSetup.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateTime"]);
            M_FinanceSetup.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedTime"]);
            M_FinanceSetup.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidTime"]);
            M_FinanceSetup.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBUserID"]);
            M_FinanceSetup.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBTerminalID"]);
            M_FinanceSetup.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            M_FinanceSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FinanceSetup;
        }

        //3
        public List<FinanceSetup> FinanceSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FinanceSetup> L_FinanceSetup = new List<FinanceSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select  case when FS.status=1 then 'PENDING' else Case When FS.status = 2 then 'APPROVED' else Case when FS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne JournalMonthOpenDesc,P.ID PeriodPKDesc,FS.* from FinanceSetup FS left join MasterValue MV " +
                              " on FS.JournalMonthOpen = MV.Code and MV.ID ='Month' and MV.Status = 2 left join" +
                              " period P on FS.PeriodPK = P.PeriodPK and P.status = 2 where FS.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select  case when FS.status=1 then 'PENDING' else Case When FS.status = 2 then 'APPROVED' else Case when FS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne JournalMonthOpenDesc,P.ID PeriodPKDesc,FS.* from FinanceSetup FS left join MasterValue MV " +
                              " on FS.JournalMonthOpen = MV.Code and MV.ID ='Month' and MV.Status = 2 left join period P on FS.PeriodPK = P.PeriodPK and P.status = 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FinanceSetup.Add(setFinanceSetup(dr));
                                }
                            }
                            return L_FinanceSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FinanceSetup_Add(FinanceSetup _financeSetup, bool _havePrivillege)
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
                                 "Select isnull(max(FinanceSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FinanceSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _financeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FinanceSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FinanceSetup";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@CashierReferencePrefix", _financeSetup.CashierReferencePrefix);
                        cmd.Parameters.AddWithValue("@JournalMonthOpen", _financeSetup.JournalMonthOpen);
                        cmd.Parameters.AddWithValue("@PeriodPK", _financeSetup.PeriodPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _financeSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FinanceSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int FinanceSetup_Update(FinanceSetup _financeSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_financeSetup.FinanceSetupPK, _financeSetup.HistoryPK, "FinanceSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update FinanceSetup set status=2,Notes=@Notes,CashierReferencePrefix=@CashierReferencePrefix,JournalMonthOpen=@JournalMonthOpen,PeriodPK=@PeriodPK," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FinanceSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _financeSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                            cmd.Parameters.AddWithValue("@CashierReferencePrefix", _financeSetup.CashierReferencePrefix);
                            cmd.Parameters.AddWithValue("@JournalMonthOpen", _financeSetup.JournalMonthOpen);
                            cmd.Parameters.AddWithValue("@PeriodPK", _financeSetup.PeriodPK);
                            cmd.Parameters.AddWithValue("@Notes", _financeSetup.Notes);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _financeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _financeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FinanceSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FinanceSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _financeSetup.EntryUsersID);
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
                                cmd.CommandText = "Update FinanceSetup set Notes=@Notes,CashierReferencePrefix=@CashierReferencePrefix,JournalMonthOpen=@JournalMonthOpen,PeriodPK=@PeriodPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FinanceSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _financeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                                cmd.Parameters.AddWithValue("@CashierReferencePrefix", _financeSetup.CashierReferencePrefix);
                                cmd.Parameters.AddWithValue("@JournalMonthOpen", _financeSetup.JournalMonthOpen);
                                cmd.Parameters.AddWithValue("@PeriodPK", _financeSetup.PeriodPK);
                                cmd.Parameters.AddWithValue("@Notes", _financeSetup.Notes);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _financeSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_financeSetup.FinanceSetupPK, "financeSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FinanceSetup where FinanceSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _financeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@CashierReferencePrefix", _financeSetup.CashierReferencePrefix);
                                cmd.Parameters.AddWithValue("@JournalMonthOpen", _financeSetup.JournalMonthOpen);
                                cmd.Parameters.AddWithValue("@PeriodPK", _financeSetup.PeriodPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _financeSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FinanceSetup set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where FinanceSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _financeSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _financeSetup.HistoryPK);
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

        public FinanceSetup FinanceSetup_SelectByPK(int _FinanceSetupPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                                " Select  case when FS.status=1 then 'PENDING' else Case When FS.status = 2 then 'APPROVED' else Case when FS.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne JournalMonthOpenDesc,FS.* from FinanceSetup FS left join MasterValue MV " +
                                " on FS.JournalMonthOpen = MV.Code and MV.ID ='Month' and MV.Status = 2 " +
                                " where FS.FinanceSetupPK = @FinanceSetupPK ";
                        cmd.Parameters.AddWithValue("@FinanceSetupPK", _FinanceSetupPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setFinanceSetup(dr);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FinanceSetup_Approved(FinanceSetup _financeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FinanceSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FinanceSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _financeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _financeSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FinanceSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FinanceSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _financeSetup.ApprovedUsersID);
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

        public void FinanceSetup_Reject(FinanceSetup _financeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FinanceSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FinanceSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _financeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _financeSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FinanceSetup set status= 2,lastupdate=@lastupdate where FinanceSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
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

        public void FinanceSetup_Void(FinanceSetup _financeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FinanceSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where FinanceSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _financeSetup.FinanceSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _financeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _financeSetup.VoidUsersID);
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

        public int Get_JournalMonthOpen()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " SELECT  JournalMonthOpen FROM [FinanceSetup]  where status = 2 ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToInt32(dr["JournalMonthOpen"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Get_JournalPeriodOpen()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " SELECT  PeriodPK FROM [FinanceSetup]  where status = 2 ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToInt32(dr["PeriodPK"]);
                                }
                            }
                            return 0;
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