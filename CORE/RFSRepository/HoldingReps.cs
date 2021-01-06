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
    public class HoldingReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[Holding] " +
                            "([HoldingPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private Holding setHolding(SqlDataReader dr)
        {
            Holding M_Holding = new Holding();
            M_Holding.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_Holding.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Holding.Status = Convert.ToInt32(dr["Status"]);
            M_Holding.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Holding.Notes = Convert.ToString(dr["Notes"]);
            M_Holding.ID = dr["ID"].ToString();
            M_Holding.Name = dr["Name"].ToString();
            M_Holding.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Holding.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Holding.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Holding.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Holding.EntryTime = dr["EntryTime"].ToString();
            M_Holding.UpdateTime = dr["UpdateTime"].ToString();
            M_Holding.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Holding.VoidTime = dr["VoidTime"].ToString();
            M_Holding.DBUserID = dr["DBUserID"].ToString();
            M_Holding.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Holding.LastUpdate = dr["LastUpdate"].ToString();
            M_Holding.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Holding;
        }

        public List<Holding> Holding_Select(int _status)
        {            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Holding> L_Holding = new List<Holding>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from Holding where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from Holding order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Holding.Add(setHolding(dr));
                                }
                            }
                            return L_Holding;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int Holding_Add(Holding _holding, bool _havePrivillege)
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
                                 "Select isnull(max(HoldingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Holding";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _holding.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(HoldingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastUpdate From Holding";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _holding.ID);
                        cmd.Parameters.AddWithValue("@Name", _holding.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _holding.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Holding");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int Holding_Update(Holding _holding, bool _havePrivillege)
        {            
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_holding.HoldingPK, _holding.HistoryPK, "holding");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Holding set status=2,Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where HoldingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _holding.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                            cmd.Parameters.AddWithValue("@ID", _holding.ID);
                            cmd.Parameters.AddWithValue("@Notes", _holding.Notes);
                            cmd.Parameters.AddWithValue("@Name", _holding.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _holding.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _holding.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Holding set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where HoldingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _holding.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update Holding set Notes=@Notes,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where HoldingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _holding.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                                cmd.Parameters.AddWithValue("@ID", _holding.ID);
                                cmd.Parameters.AddWithValue("@Notes", _holding.Notes);
                                cmd.Parameters.AddWithValue("@Name", _holding.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _holding.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_holding.HoldingPK, "Holding");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Holding where HoldingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _holding.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _holding.ID);
                                cmd.Parameters.AddWithValue("@Name", _holding.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _holding.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Holding set status= 4,Notes=@Notes,"+
                                    "LastUpdate=@LastUpdate where HoldingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _holding.Notes);
                                cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _holding.HistoryPK);
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

        public void Holding_Approved(Holding _holding)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Holding set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where HoldingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _holding.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _holding.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Holding set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where HoldingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _holding.ApprovedUsersID);
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

        public void Holding_Reject(Holding _holding)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Holding set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where HoldingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _holding.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _holding.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Holding set status= 2,LastUpdate=@LastUpdate where HoldingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Holding_Void(Holding _holding)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Holding set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where HoldingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _holding.HoldingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _holding.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _holding.VoidUsersID);
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

        //AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )
        public List<HoldingCombo> Holding_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HoldingCombo> L_Holding = new List<HoldingCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  HoldingPK,ID + ' - ' + Name ID, Name FROM [Holding]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    HoldingCombo M_Holding = new HoldingCombo();
                                    M_Holding.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
                                    M_Holding.ID = Convert.ToString(dr["ID"]);
                                    M_Holding.Name = Convert.ToString(dr["Name"]);
                                    L_Holding.Add(M_Holding);
                                }

                            }
                            return L_Holding;
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