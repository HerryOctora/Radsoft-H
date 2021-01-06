using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class COAFromSourceReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[COAFromSource] " +
                            "([COAFromSourcePK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private COAFromSource setCOAFromSource(SqlDataReader dr)
        {
            COAFromSource M_COAFromSource = new COAFromSource();
            M_COAFromSource.COAFromSourcePK = Convert.ToInt32(dr["COAFromSourcePK"]);
            M_COAFromSource.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_COAFromSource.Status = Convert.ToInt32(dr["Status"]);
            M_COAFromSource.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_COAFromSource.Notes = Convert.ToString(dr["Notes"]);
            M_COAFromSource.ID = dr["ID"].ToString();
            M_COAFromSource.Name = dr["Name"].ToString();
            M_COAFromSource.EntryUsersID = dr["EntryUsersID"].ToString();
            M_COAFromSource.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_COAFromSource.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_COAFromSource.VoidUsersID = dr["VoidUsersID"].ToString();
            M_COAFromSource.EntryTime = dr["EntryTime"].ToString();
            M_COAFromSource.UpdateTime = dr["UpdateTime"].ToString();
            M_COAFromSource.ApprovedTime = dr["ApprovedTime"].ToString();
            M_COAFromSource.VoidTime = dr["VoidTime"].ToString();
            M_COAFromSource.DBUserID = dr["DBUserID"].ToString();
            M_COAFromSource.DBTerminalID = dr["DBTerminalID"].ToString();
            M_COAFromSource.LastUpdate = dr["LastUpdate"].ToString();
            M_COAFromSource.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_COAFromSource;
        }

        public List<COAFromSource> COAFromSource_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COAFromSource> L_COAFromSource = new List<COAFromSource>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COAFromSource 
                                                where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COAFromSource ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_COAFromSource.Add(setCOAFromSource(dr));
                                }
                            }
                            return L_COAFromSource;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COAFromSource_Add(COAFromSource _COAFromSource, bool _havePrivillege)
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
                                 "Select isnull(max(COAFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from COAFromSource";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(COAFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from COAFromSource";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _COAFromSource.ID);
                        cmd.Parameters.AddWithValue("@Name", _COAFromSource.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _COAFromSource.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "COAFromSource");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COAFromSource_Update(COAFromSource _COAFromSource, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_COAFromSource.COAFromSourcePK, _COAFromSource.HistoryPK, "COAFromSource");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COAFromSource set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COAFromSourcePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _COAFromSource.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                            cmd.Parameters.AddWithValue("@Notes", _COAFromSource.Notes);
                            cmd.Parameters.AddWithValue("@ID", _COAFromSource.ID);
                            cmd.Parameters.AddWithValue("@Name", _COAFromSource.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _COAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COAFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COAFromSourcePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _COAFromSource.EntryUsersID);
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
                                cmd.CommandText = "Update COAFromSource set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COAFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _COAFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@Notes", _COAFromSource.Notes);
                                cmd.Parameters.AddWithValue("@ID", _COAFromSource.ID);
                                cmd.Parameters.AddWithValue("@Name", _COAFromSource.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COAFromSource.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_COAFromSource.COAFromSourcePK, "COAFromSource");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From COAFromSource where COAFromSourcePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COAFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _COAFromSource.ID);
                                cmd.Parameters.AddWithValue("@Name", _COAFromSource.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COAFromSource.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update COAFromSource set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where COAFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _COAFromSource.Notes);
                                cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COAFromSource.HistoryPK);
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

        public void COAFromSource_Approved(COAFromSource _COAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COAFromSource set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where COAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _COAFromSource.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COAFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COAFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COAFromSource.ApprovedUsersID);
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

        public void COAFromSource_Reject(COAFromSource _COAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COAFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COAFromSource.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COAFromSource set status= 2,LastUpdate=@LastUpdate where COAFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
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

        public void COAFromSource_Void(COAFromSource _COAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COAFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COAFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COAFromSource.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<COAFromSourceCombo> COAFromSource_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COAFromSourceCombo> L_COAFromSource = new List<COAFromSourceCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  COAFromSourcePK,ID +' - '+ Name ID, Name FROM [COAFromSource]  where status = 2 order by COAFromSourcePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    COAFromSourceCombo M_COAFromSource = new COAFromSourceCombo();
                                    M_COAFromSource.COAFromSourcePK = Convert.ToInt32(dr["COAFromSourcePK"]);
                                    M_COAFromSource.ID = Convert.ToString(dr["ID"]);
                                    M_COAFromSource.Name = Convert.ToString(dr["Name"]);
                                    L_COAFromSource.Add(M_COAFromSource);
                                }

                            }
                            return L_COAFromSource;
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