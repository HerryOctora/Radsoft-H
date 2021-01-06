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
    public class MarketReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[Market] " +
                            "([MarketPK],[HistoryPK],[Status],[ID],[Name],[Type],[CurrencyPK],";
        string _paramaterCommand = "@ID,@Name,@Type,@CurrencyPK,";

        //2
        private Market setMarket(SqlDataReader dr)
        {
            Market M_Market = new Market();
            M_Market.MarketPK = Convert.ToInt32(dr["MarketPK"]);
            M_Market.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Market.Status = Convert.ToInt32(dr["Status"]);
            M_Market.StatusDesc =  Convert.ToString(dr["StatusDesc"]);
            M_Market.Notes = Convert.ToString(dr["Notes"]);
            M_Market.ID = dr["ID"].ToString();
            M_Market.Name = dr["Name"].ToString();
            M_Market.Type = Convert.ToInt32(dr["Type"]);
            M_Market.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Market.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_Market.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_Market.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Market.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Market.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Market.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Market.EntryTime = dr["EntryTime"].ToString();
            M_Market.UpdateTime = dr["UpdateTime"].ToString();
            M_Market.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Market.VoidTime = dr["VoidTime"].ToString();
            M_Market.DBUserID = dr["DBUserID"].ToString();
            M_Market.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Market.LastUpdate = dr["LastUpdate"].ToString();
            M_Market.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Market;
        }

        public List<Market> Market_Select(int _status)
        {
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Market> L_Market = new List<Market>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when M.status=1 then 'PENDING' else Case When M.status = 2 then 'APPROVED' else Case when M.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc, C.ID CurrencyID,M.* from Market M left join " +
                            "Currency C on C.CurrencyPK = M.CurrencyPK and C.status = 2 left join Mastervalue MV on M.Type = MV.Code and MV.ID = 'MarketType' and MV.Status = 2 " +
                            "where M.status = @status order by MarketPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when M.status=1 then 'PENDING' else Case When M.status = 2 then 'APPROVED' else Case when M.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc, C.ID CurrencyID,M.* from Market M left join " +
                            "Currency C on C.CurrencyPK = M.CurrencyPK and C.status = 2 left join Mastervalue MV on M.Type = MV.Code and MV.ID = 'MarketType' and MV.Status = 2 order by MarketPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Market.Add(setMarket(dr));
                                }
                            }
                            return L_Market;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Market_Add(Market _market, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(MarketPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Market";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _market.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(MarketPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Market";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _market.ID);
                        cmd.Parameters.AddWithValue("@Name", _market.Name);
                        cmd.Parameters.AddWithValue("@Type", _market.Type);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _market.CurrencyPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _market.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "MasterValue");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Market_Update(Market _market, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_market.MarketPK, _market.HistoryPK, "market");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update Market set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,CurrencyPK=@CurrencyPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where MarketPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _market.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                            cmd.Parameters.AddWithValue("@ID", _market.ID);
                            cmd.Parameters.AddWithValue("@Notes", _market.Notes);
                            cmd.Parameters.AddWithValue("@Name", _market.Name);
                            cmd.Parameters.AddWithValue("@Type", _market.Type);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _market.CurrencyPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _market.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _market.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Market set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MarketPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _market.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update Market set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,CurrencyPK=@CurrencyPK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where MarketPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _market.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                                cmd.Parameters.AddWithValue("@ID", _market.ID);
                                cmd.Parameters.AddWithValue("@Notes", _market.Notes);
                                cmd.Parameters.AddWithValue("@Name", _market.Name);
                                cmd.Parameters.AddWithValue("@Type", _market.Type);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _market.CurrencyPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _market.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_market.MarketPK, "Market");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Market where MarketPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _market.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _market.ID);
                                cmd.Parameters.AddWithValue("@Name", _market.Name);
                                cmd.Parameters.AddWithValue("@Type", _market.Type);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _market.CurrencyPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _market.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Market set status= 4,Notes=@Notes,"+
                                    "LastUpdate=@LastUpdate where MarketPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _market.Notes);
                                cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _market.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public void Market_Approved(Market _market)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Market set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where MarketPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                        cmd.Parameters.AddWithValue("@historyPK", _market.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _market.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime",_dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Market set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MarketPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _market.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@lastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Market_Reject(Market _market)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Market set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where MarketPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                        cmd.Parameters.AddWithValue("@historyPK", _market.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _market.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Market set status= 2,LastUpdate=@LastUpdate where MarketPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public void Market_Void(Market _market)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Market set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where MarketPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _market.MarketPK);
                        cmd.Parameters.AddWithValue("@historyPK", _market.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _market.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public List<MarketCombo> Market_Combo()
        {
            
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MarketCombo> L_Market = new List<MarketCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  MarketPK,ID + ' - ' + Name ID, Name FROM [Market]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MarketCombo M_Market = new MarketCombo();
                                    M_Market.MarketPK = Convert.ToInt32(dr["MarketPK"]);
                                    M_Market.ID = Convert.ToString(dr["ID"]);
                                    M_Market.Name = Convert.ToString(dr["Name"]);
                                    L_Market.Add(M_Market);
                                }

                            }
                            return L_Market;
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