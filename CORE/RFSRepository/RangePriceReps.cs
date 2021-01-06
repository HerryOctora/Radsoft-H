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
    public class RangePriceReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[RangePrice] " +
                            "([RangePricePK],[HistoryPK],[Status],[MinPrice],[MaxPrice],[MinPricePercent],[MaxPricePercent],";
        string _paramaterCommand = "@MinPrice,@MaxPrice,@MinPricePercent,@MaxPricePercent,";

        //2
        private RangePrice setRangePrice(SqlDataReader dr)
        {
            RangePrice M_RangePrice = new RangePrice();
            M_RangePrice.RangePricePK = Convert.ToInt32(dr["RangePricePK"]);
            M_RangePrice.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RangePrice.Status = Convert.ToInt32(dr["Status"]);
            M_RangePrice.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RangePrice.Notes = Convert.ToString(dr["Notes"]);
            M_RangePrice.MinPrice = Convert.ToDecimal(dr["MinPrice"]);
            M_RangePrice.MaxPrice = Convert.ToDecimal(dr["MaxPrice"]);
            M_RangePrice.MinPricePercent = Convert.ToDecimal(dr["MinPricePercent"]);
            M_RangePrice.MaxPricePercent = Convert.ToDecimal(dr["MaxPricePercent"]);
            M_RangePrice.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RangePrice.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RangePrice.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RangePrice.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RangePrice.EntryTime = dr["EntryTime"].ToString();
            M_RangePrice.UpdateTime = dr["UpdateTime"].ToString();
            M_RangePrice.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RangePrice.VoidTime = dr["VoidTime"].ToString();
            M_RangePrice.DBUserID = dr["DBUserID"].ToString();
            M_RangePrice.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RangePrice.LastUpdate = dr["LastUpdate"].ToString();
            M_RangePrice.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RangePrice;
        }

        public List<RangePrice> RangePrice_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RangePrice> L_RangePrice = new List<RangePrice>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from RangePrice where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from RangePrice order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RangePrice.Add(setRangePrice(dr));
                                }
                            }
                            return L_RangePrice;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RangePrice_Add(RangePrice _RangePrice, bool _havePrivillege)
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
                                 "Select isnull(max(RangePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From RangePrice";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RangePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(RangePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From RangePrice";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@MinPrice", _RangePrice.MinPrice);
                        cmd.Parameters.AddWithValue("@MaxPrice", _RangePrice.MaxPrice);
                        cmd.Parameters.AddWithValue("@MinPricePercent", _RangePrice.MinPricePercent);
                        cmd.Parameters.AddWithValue("@MaxPricePercent", _RangePrice.MaxPricePercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RangePrice.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RangePrice");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int RangePrice_Update(RangePrice _RangePrice, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_RangePrice.RangePricePK, _RangePrice.HistoryPK, "RangePrice");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RangePrice set status=2, Notes=@Notes,MinPrice=@MinPrice,MaxPrice=@MaxPrice,MinPricePercent=@MinPricePercent,MaxPricePercent=@MaxPricePercent," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where RangePricePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RangePrice.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                            cmd.Parameters.AddWithValue("@Notes", _RangePrice.Notes);
                            cmd.Parameters.AddWithValue("@MinPrice", _RangePrice.MinPrice);
                            cmd.Parameters.AddWithValue("@MaxPrice", _RangePrice.MaxPrice);
                            cmd.Parameters.AddWithValue("@MinPricePercent", _RangePrice.MinPricePercent);
                            cmd.Parameters.AddWithValue("@MaxPricePercent", _RangePrice.MaxPricePercent);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RangePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RangePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RangePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where RangePricePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RangePrice.EntryUsersID);
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
                                cmd.CommandText = "Update RangePrice set  Notes=@Notes,MinPrice=@MinPrice,MaxPrice=@MaxPrice,MinPricePercent=@MinPricePercent,MaxPricePercent=@MaxPricePercent," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where RangePricePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RangePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                                cmd.Parameters.AddWithValue("@Notes", _RangePrice.Notes);
                                cmd.Parameters.AddWithValue("@MinPrice", _RangePrice.MinPrice);
                                cmd.Parameters.AddWithValue("@MaxPrice", _RangePrice.MaxPrice);
                                cmd.Parameters.AddWithValue("@MinPricePercent", _RangePrice.MinPricePercent);
                                cmd.Parameters.AddWithValue("@MaxPricePercent", _RangePrice.MaxPricePercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RangePrice.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RangePrice.RangePricePK, "RangePrice");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RangePrice where RangePricePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RangePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@MinPrice", _RangePrice.MinPrice);
                                cmd.Parameters.AddWithValue("@MaxPrice", _RangePrice.MaxPrice);
                                cmd.Parameters.AddWithValue("@MinPricePercent", _RangePrice.MinPricePercent);
                                cmd.Parameters.AddWithValue("@MaxPricePercent", _RangePrice.MaxPricePercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RangePrice.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RangePrice set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where RangePricePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RangePrice.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RangePrice.HistoryPK);
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

        public void RangePrice_Approved(RangePrice _RangePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RangePrice set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where RangePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RangePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RangePrice.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RangePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where RangePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RangePrice.ApprovedUsersID);
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

        public void RangePrice_Reject(RangePrice _RangePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RangePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where RangePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RangePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RangePrice.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RangePrice set status= 2,lastupdate=@lastupdate where RangePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
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

        public void RangePrice_Void(RangePrice _RangePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RangePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where RangePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RangePrice.RangePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RangePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RangePrice.VoidUsersID);
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