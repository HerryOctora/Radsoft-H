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
    public class SInvestSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[SInvestSetup] " +
                            "([SInvestSetupPK],[HistoryPK],[Status],[SinvestMoneyMarketFeePercent],[SinvestBondFeePercent],[SinvestEquityFeePercent],[SInvestFeeDays],[FundPK],";

        string _paramaterCommand = "@SinvestMoneyMarketFeePercent,@SinvestBondFeePercent,@SinvestEquityFeePercent,@SInvestFeeDays,@FundPK,";

        //2
        private SInvestSetup setSInvestSetup(SqlDataReader dr)
        {
            SInvestSetup M_SInvestSetup = new SInvestSetup();
            M_SInvestSetup.SInvestSetupPK = Convert.ToInt32(dr["SInvestSetupPK"]);
            M_SInvestSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SInvestSetup.Status = Convert.ToInt32(dr["Status"]);
            M_SInvestSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SInvestSetup.Notes = Convert.ToString(dr["Notes"]);
            M_SInvestSetup.SinvestMoneyMarketFeePercent = dr["SinvestMoneyMarketFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestMoneyMarketFeePercent"]);
            M_SInvestSetup.SinvestBondFeePercent = dr["SinvestBondFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestBondFeePercent"]);
            M_SInvestSetup.SinvestEquityFeePercent = dr["SinvestEquityFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestEquityFeePercent"]);
            M_SInvestSetup.SInvestFeeDays = dr["SInvestFeeDays"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SInvestFeeDays"]);
            M_SInvestSetup.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_SInvestSetup.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryUsersID"]);
            M_SInvestSetup.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateUsersID"]);
            M_SInvestSetup.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedUsersID"]);
            M_SInvestSetup.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidUsersID"]);
            M_SInvestSetup.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryTime"]);
            M_SInvestSetup.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateTime"]);
            M_SInvestSetup.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedTime"]);
            M_SInvestSetup.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidTime"]);
            M_SInvestSetup.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBUserID"]);
            M_SInvestSetup.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBTerminalID"]);
            M_SInvestSetup.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            M_SInvestSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_SInvestSetup;
        }

        //3
        public List<SInvestSetup> SInvestSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SInvestSetup> L_SInvestSetup = new List<SInvestSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" Select  case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from SInvestSetup where status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @" Select  case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from SInvestSetup " ;
                                
                             
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SInvestSetup.Add(setSInvestSetup(dr));
                                }
                            }
                            return L_SInvestSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SInvestSetup_Add(SInvestSetup _SInvestSetup, bool _havePrivillege)
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
                                 "Select isnull(max(SInvestSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from SInvestSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SInvestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(SInvestSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from SInvestSetup";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _SInvestSetup.SinvestMoneyMarketFeePercent);
                        cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _SInvestSetup.SinvestBondFeePercent);
                        cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _SInvestSetup.SinvestEquityFeePercent);
                        cmd.Parameters.AddWithValue("@SInvestFeeDays", _SInvestSetup.SInvestFeeDays);
                        cmd.Parameters.AddWithValue("@FundPK", _SInvestSetup.FundPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SInvestSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "SInvestSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int SInvestSetup_Update(SInvestSetup _SInvestSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SInvestSetup.SInvestSetupPK, _SInvestSetup.HistoryPK, "SInvestSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update SInvestSetup set status=2,Notes=@Notes,SinvestMoneyMarketFeePercent=@SinvestMoneyMarketFeePercent,SinvestBondFeePercent=@SinvestBondFeePercent,SinvestEquityFeePercent=@SinvestEquityFeePercent,SInvestFeeDays=@SInvestFeeDays,FundPK=@FundPK," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where SInvestSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _SInvestSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                            cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _SInvestSetup.SinvestMoneyMarketFeePercent);
                            cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _SInvestSetup.SinvestBondFeePercent);
                            cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _SInvestSetup.SinvestEquityFeePercent);
                            cmd.Parameters.AddWithValue("@SInvestFeeDays", _SInvestSetup.SInvestFeeDays);
                            cmd.Parameters.AddWithValue("@FundPK", _SInvestSetup.FundPK);
                            cmd.Parameters.AddWithValue("@Notes", _SInvestSetup.Notes);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SInvestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SInvestSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SInvestSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where SInvestSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SInvestSetup.EntryUsersID);
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
                                cmd.CommandText = "Update SInvestSetup set Notes=@Notes,SinvestMoneyMarketFeePercent=@SinvestMoneyMarketFeePercent,SinvestBondFeePercent=@SinvestBondFeePercent,SinvestEquityFeePercent=@SinvestEquityFeePercent,SInvestFeeDays=@SInvestFeeDays,FundPK=@FundPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where SInvestSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SInvestSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                                cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _SInvestSetup.SinvestMoneyMarketFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _SInvestSetup.SinvestBondFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _SInvestSetup.SinvestEquityFeePercent);
                                cmd.Parameters.AddWithValue("@SInvestFeeDays", _SInvestSetup.SInvestFeeDays);
                                cmd.Parameters.AddWithValue("@FundPK", _SInvestSetup.FundPK);
                                cmd.Parameters.AddWithValue("@Notes", _SInvestSetup.Notes);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SInvestSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SInvestSetup.SInvestSetupPK, "SInvestSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SInvestSetup where SInvestSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SInvestSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _SInvestSetup.SinvestMoneyMarketFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _SInvestSetup.SinvestBondFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _SInvestSetup.SinvestEquityFeePercent);
                                cmd.Parameters.AddWithValue("@SInvestFeeDays", _SInvestSetup.SInvestFeeDays);
                                cmd.Parameters.AddWithValue("@FundPK", _SInvestSetup.FundPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SInvestSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SInvestSetup set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where SInvestSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SInvestSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SInvestSetup.HistoryPK);
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

        public SInvestSetup SInvestSetup_SelectByPK(int _SInvestSetupPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                               @" Select  case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from SInvestSetup where SInvestSetupPK = @SInvestSetupPK ";
                        cmd.Parameters.AddWithValue("@SInvestSetupPK", _SInvestSetupPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setSInvestSetup(dr);
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

        public void SInvestSetup_Approved(SInvestSetup _SInvestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SInvestSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where SInvestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SInvestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SInvestSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SInvestSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where SInvestSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SInvestSetup.ApprovedUsersID);
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

        public void SInvestSetup_Reject(SInvestSetup _SInvestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SInvestSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where SInvestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SInvestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SInvestSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SInvestSetup set status= 2,lastupdate=@lastupdate where SInvestSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
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

        public void SInvestSetup_Void(SInvestSetup _SInvestSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SInvestSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where SInvestSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SInvestSetup.SInvestSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SInvestSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SInvestSetup.VoidUsersID);
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

      

    }
}