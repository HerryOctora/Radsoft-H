using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class CompanyAccountTradingReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CompanyAccountTrading] " +
                            "([CompanyAccountTradingPK],[HistoryPK],[Status],[ID],[Name],[SID],[CounterpartPK],";
        string _paramaterCommand = "@ID,@Name,@SID,@CounterpartPK,";

        //2
        private CompanyAccountTrading setCompanyAccountTrading(SqlDataReader dr)
        {
            CompanyAccountTrading M_CompanyAccountTrading = new CompanyAccountTrading();
            M_CompanyAccountTrading.CompanyAccountTradingPK = Convert.ToInt32(dr["CompanyAccountTradingPK"]);
            M_CompanyAccountTrading.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CompanyAccountTrading.Status = Convert.ToInt32(dr["Status"]);
            M_CompanyAccountTrading.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CompanyAccountTrading.Notes = Convert.ToString(dr["Notes"]);
            M_CompanyAccountTrading.ID = Convert.ToString(dr["ID"]);
            M_CompanyAccountTrading.Name = Convert.ToString(dr["Name"]);
            M_CompanyAccountTrading.SID = Convert.ToString(dr["SID"]);
            M_CompanyAccountTrading.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_CompanyAccountTrading.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_CompanyAccountTrading.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CompanyAccountTrading.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CompanyAccountTrading.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CompanyAccountTrading.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CompanyAccountTrading.EntryTime = dr["EntryTime"].ToString();
            M_CompanyAccountTrading.UpdateTime = dr["UpdateTime"].ToString();
            M_CompanyAccountTrading.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CompanyAccountTrading.VoidTime = dr["VoidTime"].ToString();
            M_CompanyAccountTrading.DBUserID = dr["DBUserID"].ToString();
            M_CompanyAccountTrading.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CompanyAccountTrading.LastUpdate = dr["LastUpdate"].ToString();
            M_CompanyAccountTrading.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CompanyAccountTrading;
        }

        public List<CompanyAccountTrading> CompanyAccountTrading_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyAccountTrading> L_CompanyAccountTrading = new List<CompanyAccountTrading>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID CounterpartID,GR.* from CompanyAccountTrading GR 
                            left join Counterpart G on GR.CounterpartPK = G.CounterpartPK and G.status = 2 
                            where GR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID CounterpartID,GR.* from CompanyAccountTrading GR 
                            left join Counterpart G on GR.CounterpartPK = G.CounterpartPK and G.status = 2 
                            order by CounterpartPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CompanyAccountTrading.Add(setCompanyAccountTrading(dr));
                                }
                            }
                            return L_CompanyAccountTrading;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CompanyAccountTrading_Add(CompanyAccountTrading _CompanyAccountTrading, bool _havePrivillege)
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
                                 "Select isnull(max(CompanyAccountTradingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CompanyAccountTrading";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyAccountTrading.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CompanyAccountTradingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CompanyAccountTrading";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _CompanyAccountTrading.ID);
                        cmd.Parameters.AddWithValue("@Name", _CompanyAccountTrading.Name);
                        cmd.Parameters.AddWithValue("@SID", _CompanyAccountTrading.SID);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _CompanyAccountTrading.CounterpartPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CompanyAccountTrading.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CompanyAccountTrading");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CompanyAccountTrading_Update(CompanyAccountTrading _CompanyAccountTrading, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CompanyAccountTrading.CompanyAccountTradingPK, _CompanyAccountTrading.HistoryPK, "CompanyAccountTrading"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CompanyAccountTrading set status=2,Notes=@Notes,ID=@ID,Name=@Name,SID=@SID,CounterpartPK=@CounterpartPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CompanyAccountTradingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CompanyAccountTrading.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                            cmd.Parameters.AddWithValue("@Notes", _CompanyAccountTrading.Notes);
                            cmd.Parameters.AddWithValue("@ID", _CompanyAccountTrading.ID);
                            cmd.Parameters.AddWithValue("@Name", _CompanyAccountTrading.Name);
                            cmd.Parameters.AddWithValue("@SID", _CompanyAccountTrading.SID);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _CompanyAccountTrading.CounterpartPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyAccountTrading.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyAccountTrading.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CompanyAccountTrading set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyAccountTradingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyAccountTrading.EntryUsersID);
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
                                cmd.CommandText = "Update CompanyAccountTrading set Notes=@Notes,ID=@ID,Name=@Name,SID=@SID,CounterpartPK=@CounterpartPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where CompanyAccountTradingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyAccountTrading.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                                cmd.Parameters.AddWithValue("@Notes", _CompanyAccountTrading.Notes);
                                cmd.Parameters.AddWithValue("@ID", _CompanyAccountTrading.ID);
                                cmd.Parameters.AddWithValue("@Name", _CompanyAccountTrading.Name);
                                cmd.Parameters.AddWithValue("@SID", _CompanyAccountTrading.SID);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _CompanyAccountTrading.CounterpartPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyAccountTrading.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CompanyAccountTrading.CompanyAccountTradingPK, "CompanyAccountTrading");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CompanyAccountTrading where CompanyAccountTradingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyAccountTrading.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _CompanyAccountTrading.ID);
                                cmd.Parameters.AddWithValue("@Name", _CompanyAccountTrading.Name);
                                cmd.Parameters.AddWithValue("@SID", _CompanyAccountTrading.SID);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _CompanyAccountTrading.CounterpartPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyAccountTrading.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CompanyAccountTrading set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where CompanyAccountTradingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CompanyAccountTrading.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyAccountTrading.HistoryPK);
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

        public void CompanyAccountTrading_Approved(CompanyAccountTrading _CompanyAccountTrading)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyAccountTrading set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where CompanyAccountTradingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyAccountTrading.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyAccountTrading.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CompanyAccountTrading set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyAccountTradingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyAccountTrading.ApprovedUsersID);
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

        public void CompanyAccountTrading_Reject(CompanyAccountTrading _CompanyAccountTrading)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyAccountTrading set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CompanyAccountTradingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyAccountTrading.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyAccountTrading.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CompanyAccountTrading set status= 2,LastUpdate=@LastUpdate  where CompanyAccountTradingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
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

        public void CompanyAccountTrading_Void(CompanyAccountTrading _CompanyAccountTrading)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyAccountTrading set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CompanyAccountTradingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyAccountTrading.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyAccountTrading.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyAccountTrading.VoidUsersID);
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

        public List<CompanyAccountTradingCombo> CompanyAccountTrading_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyAccountTradingCombo> L_CompanyAccountTrading = new List<CompanyAccountTradingCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CompanyAccountTradingPK,ID + ' - ' + Name ID, Name FROM [CompanyAccountTrading]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CompanyAccountTradingCombo M_CompanyAccountTrading = new CompanyAccountTradingCombo();
                                    M_CompanyAccountTrading.CompanyAccountTradingPK = Convert.ToInt32(dr["CompanyAccountTradingPK"]);
                                    M_CompanyAccountTrading.ID = Convert.ToString(dr["ID"]);
                                    M_CompanyAccountTrading.Name = Convert.ToString(dr["Name"]);
                                    L_CompanyAccountTrading.Add(M_CompanyAccountTrading);
                                }

                            }
                            return L_CompanyAccountTrading;
                        }
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