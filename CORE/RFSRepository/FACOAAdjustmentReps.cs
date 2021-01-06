using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;



namespace RFSRepository
{
    public class FACOAAdjustmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FACOAAdjustment] " +
                            "([FACOAAdjustmentPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private FACOAAdjustment setFACOAAdjustment(SqlDataReader dr)
        {
            FACOAAdjustment M_FACOAAdjustment = new FACOAAdjustment();
            M_FACOAAdjustment.FACOAAdjustmentPK = Convert.ToInt32(dr["FACOAAdjustmentPK"]);
            M_FACOAAdjustment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FACOAAdjustment.Status = Convert.ToInt32(dr["Status"]);
            M_FACOAAdjustment.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FACOAAdjustment.Notes = Convert.ToString(dr["Notes"]);
            M_FACOAAdjustment.ID = dr["ID"].ToString();
            M_FACOAAdjustment.Name = dr["Name"].ToString();
            M_FACOAAdjustment.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FACOAAdjustment.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FACOAAdjustment.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FACOAAdjustment.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FACOAAdjustment.EntryTime = dr["EntryTime"].ToString();
            M_FACOAAdjustment.UpdateTime = dr["UpdateTime"].ToString();
            M_FACOAAdjustment.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FACOAAdjustment.VoidTime = dr["VoidTime"].ToString();
            M_FACOAAdjustment.DBUserID = dr["DBUserID"].ToString();
            M_FACOAAdjustment.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FACOAAdjustment.LastUpdate = dr["LastUpdate"].ToString();
            M_FACOAAdjustment.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FACOAAdjustment;
        }

        public List<FACOAAdjustment> FACOAAdjustment_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FACOAAdjustment> L_FACOAAdjustment = new List<FACOAAdjustment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from FACOAAdjustment where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from FACOAAdjustment order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FACOAAdjustment.Add(setFACOAAdjustment(dr));
                                }
                            }
                            return L_FACOAAdjustment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FACOAAdjustment_Add(FACOAAdjustment _FACOAAdjustment, bool _havePrivillege)
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
                                 "Select isnull(max(FACOAAdjustmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FACOAAdjustment";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FACOAAdjustmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FACOAAdjustment";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _FACOAAdjustment.ID);
                        cmd.Parameters.AddWithValue("@Name", _FACOAAdjustment.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FACOAAdjustment.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FACOAAdjustment");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FACOAAdjustment_Update(FACOAAdjustment _FACOAAdjustment, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FACOAAdjustment.FACOAAdjustmentPK, _FACOAAdjustment.HistoryPK, "FACOAAdjustment");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FACOAAdjustment set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FACOAAdjustmentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FACOAAdjustment.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@Notes", _FACOAAdjustment.Notes);
                            cmd.Parameters.AddWithValue("@ID", _FACOAAdjustment.ID);
                            cmd.Parameters.AddWithValue("@Name", _FACOAAdjustment.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FACOAAdjustment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FACOAAdjustmentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAAdjustment.EntryUsersID);
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
                                cmd.CommandText = "Update FACOAAdjustment set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FACOAAdjustmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@Notes", _FACOAAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@ID", _FACOAAdjustment.ID);
                                cmd.Parameters.AddWithValue("@Name", _FACOAAdjustment.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAAdjustment.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FACOAAdjustment.FACOAAdjustmentPK, "FACOAAdjustment");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FACOAAdjustment where FACOAAdjustmentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _FACOAAdjustment.ID);
                                cmd.Parameters.AddWithValue("@Name", _FACOAAdjustment.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAAdjustment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FACOAAdjustment set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FACOAAdjustmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FACOAAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAAdjustment.HistoryPK);
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

        public void FACOAAdjustment_Approved(FACOAAdjustment _FACOAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAAdjustment set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FACOAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAAdjustment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FACOAAdjustment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FACOAAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAAdjustment.ApprovedUsersID);
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

        public void FACOAAdjustment_Reject(FACOAAdjustment _FACOAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FACOAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAAdjustment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FACOAAdjustment set status= 2,LastUpdate=@LastUpdate where FACOAAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
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

        public void FACOAAdjustment_Void(FACOAAdjustment _FACOAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FACOAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAAdjustment.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAAdjustment.VoidUsersID);
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

        public List<FACOAAdjustmentCombo> FACOAAdjustment_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FACOAAdjustmentCombo> L_FACOAAdjustment = new List<FACOAAdjustmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FACOAAdjustmentPK,ID +' - '+ Name ID, Name FROM [FACOAAdjustment]  where status = 2 order by FACOAAdjustmentPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FACOAAdjustmentCombo M_FACOAAdjustment = new FACOAAdjustmentCombo();
                                    M_FACOAAdjustment.FACOAAdjustmentPK = Convert.ToInt32(dr["FACOAAdjustmentPK"]);
                                    M_FACOAAdjustment.ID = Convert.ToString(dr["ID"]);
                                    M_FACOAAdjustment.Name = Convert.ToString(dr["Name"]);
                                    L_FACOAAdjustment.Add(M_FACOAAdjustment);
                                }

                            }
                            return L_FACOAAdjustment;
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