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
    public class IndexReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [Index] " +
                            "([IndexPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private Index setIndex(SqlDataReader dr)
        {
            Index M_index = new Index();
            M_index.IndexPK = Convert.ToInt32(dr["IndexPK"]);
            M_index.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_index.Status = Convert.ToInt32(dr["Status"]);
            M_index.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_index.Notes = Convert.ToString(dr["Notes"]);
            M_index.ID = dr["ID"].ToString();
            M_index.Name = dr["Name"].ToString();
            M_index.EntryUsersID = dr["EntryUsersID"].ToString();
            M_index.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_index.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_index.VoidUsersID = dr["VoidUsersID"].ToString();
            M_index.EntryTime = dr["EntryTime"].ToString();
            M_index.UpdateTime = dr["UpdateTime"].ToString();
            M_index.ApprovedTime = dr["ApprovedTime"].ToString();
            M_index.VoidTime = dr["VoidTime"].ToString();
            M_index.DBUserID = dr["DBUserID"].ToString();
            M_index.DBTerminalID = dr["DBTerminalID"].ToString();
            M_index.LastUpdate = dr["LastUpdate"].ToString();
            M_index.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_index;
        }

        public List<Index> Index_Select(int _status)
        {            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Index> L_index = new List<Index>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from [Index] where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from [Index] order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_index.Add(setIndex(dr));
                                }
                            }
                            return L_index;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int Index_Add(Index _index, bool _havePrivillege)
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
                                 "Select isnull(max(IndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From [Index]";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _index.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(IndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastUpdate from [index]";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _index.ID);
                        cmd.Parameters.AddWithValue("@Name", _index.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _index.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Index");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int Index_Update(Index _index, bool _havePrivillege)
        {            
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_index.IndexPK, _index.HistoryPK, "Index");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update [Index] set status=2,Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where IndexPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _index.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                            cmd.Parameters.AddWithValue("@ID", _index.ID);
                            cmd.Parameters.AddWithValue("@Notes", _index.Notes);
                            cmd.Parameters.AddWithValue("@Name", _index.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _index.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _index.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update [Index]  set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where IndexPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _index.EntryUsersID);
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
                                cmd.CommandText = "Update [Index]  set Notes=@Notes,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where IndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _index.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                                cmd.Parameters.AddWithValue("@ID", _index.ID);
                                cmd.Parameters.AddWithValue("@Notes", _index.Notes);
                                cmd.Parameters.AddWithValue("@Name", _index.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _index.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_index.IndexPK, "Index");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From [Index] where IndexPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _index.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _index.ID);
                                cmd.Parameters.AddWithValue("@Name", _index.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _index.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update [Index]  set status= 4,Notes=@Notes," +
                                    "LastUpdate=@LastUpdate where IndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _index.Notes);
                                cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _index.HistoryPK);
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

        public void Index_Approved(Index _index)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update [Index] set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where IndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _index.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _index.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [Index] set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where IndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _index.ApprovedUsersID);
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

        public void Index_Reject(Index _index)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update [Index] set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where IndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _index.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _index.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [Index] set status= 2,LastUpdate=@LastUpdate where IndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
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

        public void Index_Void(Index _index)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update [Index] set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where IndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _index.IndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _index.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _index.VoidUsersID);
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
        public List<IndexCombo> Index_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<IndexCombo> L_index = new List<IndexCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  IndexPK,ID + ' - ' + Name ID, Name FROM [Index]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    IndexCombo M_index = new IndexCombo();
                                    M_index.IndexPK = Convert.ToInt32(dr["IndexPK"]);
                                    M_index.ID = Convert.ToString(dr["ID"]);
                                    M_index.Name = Convert.ToString(dr["Name"]);
                                    L_index.Add(M_index);
                                }

                            }
                            return L_index;
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