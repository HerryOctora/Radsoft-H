using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class GoodFundReconcileReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[GoodFundReconcile] " +
                            "([GoodFundReconcilePK],[HistoryPK],[Status],[Date],[FundClientPK],[TotalTransaction],[FundAmount],[Difference],[BitIsGood],";
        string _paramaterCommand = "@Date,@FundClientPK,@TotalTransaction,@FundAmount,@Difference,@BitIsGood,";

        //2
        private GoodFundReconcile setGoodFundReconcile(SqlDataReader dr)
        {
            GoodFundReconcile M_GoodFundReconcile = new GoodFundReconcile();
            M_GoodFundReconcile.GoodFundReconcilePK = Convert.ToInt32(dr["GoodFundReconcilePK"]);
            M_GoodFundReconcile.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_GoodFundReconcile.Status = Convert.ToInt32(dr["Status"]);
            M_GoodFundReconcile.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_GoodFundReconcile.Notes = Convert.ToString(dr["Notes"]);
            M_GoodFundReconcile.Date = dr["Date"].ToString();
            M_GoodFundReconcile.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_GoodFundReconcile.FundClientID = dr["FundClientID"].ToString();
            M_GoodFundReconcile.FundClientName = dr["FundClientName"].ToString();
            M_GoodFundReconcile.TotalTransaction = Convert.ToDecimal(dr["TotalTransaction"]);
            M_GoodFundReconcile.FundAmount = Convert.ToDecimal(dr["FundAmount"]);
            M_GoodFundReconcile.Difference = Convert.ToDecimal(dr["Difference"]);
            M_GoodFundReconcile.BitIsGood = Convert.ToBoolean(dr["BitIsGood"]);
            M_GoodFundReconcile.EntryUsersID = dr["EntryUsersID"].ToString();
            M_GoodFundReconcile.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_GoodFundReconcile.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_GoodFundReconcile.VoidUsersID = dr["VoidUsersID"].ToString();
            M_GoodFundReconcile.EntryTime = dr["EntryTime"].ToString();
            M_GoodFundReconcile.UpdateTime = dr["UpdateTime"].ToString();
            M_GoodFundReconcile.ApprovedTime = dr["ApprovedTime"].ToString();
            M_GoodFundReconcile.VoidTime = dr["VoidTime"].ToString();
            M_GoodFundReconcile.DBUserID = dr["DBUserID"].ToString();
            M_GoodFundReconcile.DBTerminalID = dr["DBTerminalID"].ToString();
            M_GoodFundReconcile.LastUpdate = dr["LastUpdate"].ToString();
            M_GoodFundReconcile.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_GoodFundReconcile;
        }

        public List<GoodFundReconcile> GoodFundReconcile_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GoodFundReconcile> L_GoodFundReconcile = new List<GoodFundReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundClientID,B.Name FundClientName,A.* from GoodFundReconcile A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2  
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundClientID,B.Name FundClientName,A.* from GoodFundReconcile A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2  
                            order by FundClientPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GoodFundReconcile.Add(setGoodFundReconcile(dr));
                                }
                            }
                            return L_GoodFundReconcile;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int GoodFundReconcile_Add(GoodFundReconcile _GoodFundReconcile, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(GoodFundReconcilePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from GoodFundReconcile";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _GoodFundReconcile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(GoodFundReconcilePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from GoodFundReconcile";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _GoodFundReconcile.Date);
                        cmd.Parameters.AddWithValue("@FundClientPK", _GoodFundReconcile.FundClientPK);
                        cmd.Parameters.AddWithValue("@TotalTransaction", _GoodFundReconcile.TotalTransaction);
                        cmd.Parameters.AddWithValue("@FundAmount", _GoodFundReconcile.FundAmount);
                        cmd.Parameters.AddWithValue("@Difference", _GoodFundReconcile.Difference);
                        cmd.Parameters.AddWithValue("@BitIsGood", _GoodFundReconcile.BitIsGood);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _GoodFundReconcile.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "GoodFundReconcile");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int GoodFundReconcile_Update(GoodFundReconcile _GoodFundReconcile, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_GoodFundReconcile.GoodFundReconcilePK, _GoodFundReconcile.HistoryPK, "GoodFundReconcile");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GoodFundReconcile set status=2, Notes=@Notes,Date=@Date,FundClientPK=@FundClientPK,TotalTransaction=@TotalTransaction,FundAmount=@FundAmount,Difference=@Difference,BitIsGood=@BitIsGood," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GoodFundReconcilePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _GoodFundReconcile.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                            cmd.Parameters.AddWithValue("@Notes", _GoodFundReconcile.Notes);
                            cmd.Parameters.AddWithValue("@Date", _GoodFundReconcile.Date);
                            cmd.Parameters.AddWithValue("@FundClientPK", _GoodFundReconcile.FundClientPK);
                            cmd.Parameters.AddWithValue("@TotalTransaction", _GoodFundReconcile.TotalTransaction);
                            cmd.Parameters.AddWithValue("@FundAmount", _GoodFundReconcile.FundAmount);
                            cmd.Parameters.AddWithValue("@Difference", _GoodFundReconcile.Difference);
                            cmd.Parameters.AddWithValue("@BitIsGood", _GoodFundReconcile.BitIsGood);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _GoodFundReconcile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _GoodFundReconcile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GoodFundReconcile set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GoodFundReconcilePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _GoodFundReconcile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update GoodFundReconcile set Notes=@Notes,Date=@Date,FundClientPK=@FundClientPK,TotalTransaction=@TotalTransaction,FundAmount=@FundAmount,Difference=@Difference,BitIsGood=@BitIsGood," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GoodFundReconcilePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _GoodFundReconcile.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                                cmd.Parameters.AddWithValue("@Notes", _GoodFundReconcile.Notes);
                                cmd.Parameters.AddWithValue("@Date", _GoodFundReconcile.Date);
                                cmd.Parameters.AddWithValue("@FundClientPK", _GoodFundReconcile.FundClientPK);
                                cmd.Parameters.AddWithValue("@TotalTransaction", _GoodFundReconcile.TotalTransaction);
                                cmd.Parameters.AddWithValue("@FundAmount", _GoodFundReconcile.FundAmount);
                                cmd.Parameters.AddWithValue("@Difference", _GoodFundReconcile.Difference);
                                cmd.Parameters.AddWithValue("@BitIsGood", _GoodFundReconcile.BitIsGood);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _GoodFundReconcile.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_GoodFundReconcile.GoodFundReconcilePK, "GoodFundReconcile");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From GoodFundReconcile where GoodFundReconcilePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _GoodFundReconcile.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _GoodFundReconcile.Date);
                                cmd.Parameters.AddWithValue("@FundClientPK", _GoodFundReconcile.FundClientPK);
                                cmd.Parameters.AddWithValue("@TotalTransaction", _GoodFundReconcile.TotalTransaction);
                                cmd.Parameters.AddWithValue("@FundAmount", _GoodFundReconcile.FundAmount);
                                cmd.Parameters.AddWithValue("@Difference", _GoodFundReconcile.Difference);
                                cmd.Parameters.AddWithValue("@BitIsGood", _GoodFundReconcile.BitIsGood);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _GoodFundReconcile.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update GoodFundReconcile set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where GoodFundReconcilePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _GoodFundReconcile.Notes);
                                cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _GoodFundReconcile.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void GoodFundReconcile_Approved(GoodFundReconcile _GoodFundReconcile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GoodFundReconcile set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where GoodFundReconcilePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GoodFundReconcile.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _GoodFundReconcile.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GoodFundReconcile set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GoodFundReconcilePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GoodFundReconcile.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void GoodFundReconcile_Reject(GoodFundReconcile _GoodFundReconcile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GoodFundReconcile set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GoodFundReconcilePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GoodFundReconcile.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GoodFundReconcile.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GoodFundReconcile set status= 2,LastUpdate=@LastUpdate where GoodFundReconcilePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void GoodFundReconcile_Void(GoodFundReconcile _GoodFundReconcile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GoodFundReconcile set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GoodFundReconcilePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GoodFundReconcile.GoodFundReconcilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GoodFundReconcile.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GoodFundReconcile.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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