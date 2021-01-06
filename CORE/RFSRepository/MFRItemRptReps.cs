using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class MFRItemRptReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MFRItemRpt] " +
                            "([MFRItemRptPK],[HistoryPK],[Status],[ItemText],";
        string _paramaterCommand = "@ItemText,";

        //2
        private MFRItemRpt setMFRItemRpt(SqlDataReader dr)
        {
            MFRItemRpt M_MFRItemRpt = new MFRItemRpt();
            M_MFRItemRpt.MFRItemRptPK = Convert.ToInt32(dr["MFRItemRptPK"]);
            M_MFRItemRpt.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MFRItemRpt.Status = Convert.ToInt32(dr["Status"]);
            M_MFRItemRpt.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MFRItemRpt.Notes = Convert.ToString(dr["Notes"]);
            M_MFRItemRpt.ItemText = dr["ItemText"].ToString();
            M_MFRItemRpt.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MFRItemRpt.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MFRItemRpt.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MFRItemRpt.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MFRItemRpt.EntryTime = dr["EntryTime"].ToString();
            M_MFRItemRpt.UpdateTime = dr["UpdateTime"].ToString();
            M_MFRItemRpt.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MFRItemRpt.VoidTime = dr["VoidTime"].ToString();
            M_MFRItemRpt.DBUserID = dr["DBUserID"].ToString();
            M_MFRItemRpt.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MFRItemRpt.LastUpdate = dr["LastUpdate"].ToString();
            M_MFRItemRpt.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_MFRItemRpt;
        }

        public List<MFRItemRpt> MFRItemRpt_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MFRItemRpt> L_MFRItemRpt = new List<MFRItemRpt>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from MFRItemRpt 
                                                where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from MFRItemRpt ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MFRItemRpt.Add(setMFRItemRpt(dr));
                                }
                            }
                            return L_MFRItemRpt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MFRItemRpt_Add(MFRItemRpt _MFRItemRpt, bool _havePrivillege)
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
                                 "Select isnull(max(MFRItemRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MFRItemRpt";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRItemRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(MFRItemRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MFRItemRpt";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ItemText", _MFRItemRpt.ItemText);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MFRItemRpt.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MFRItemRpt");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MFRItemRpt_Update(MFRItemRpt _MFRItemRpt, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_MFRItemRpt.MFRItemRptPK, _MFRItemRpt.HistoryPK, "MFRItemRpt");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFRItemRpt set status=2, Notes=@Notes,ItemText=@ItemText," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MFRItemRptPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _MFRItemRpt.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                            cmd.Parameters.AddWithValue("@Notes", _MFRItemRpt.Notes);
                            cmd.Parameters.AddWithValue("@ItemText", _MFRItemRpt.ItemText);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRItemRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRItemRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFRItemRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFRItemRptPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _MFRItemRpt.EntryUsersID);
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
                                cmd.CommandText = "Update MFRItemRpt set Notes=@Notes,ItemText=@ItemText," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MFRItemRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRItemRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                                cmd.Parameters.AddWithValue("@Notes", _MFRItemRpt.Notes);
                                cmd.Parameters.AddWithValue("@ItemText", _MFRItemRpt.ItemText);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRItemRpt.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_MFRItemRpt.MFRItemRptPK, "MFRItemRpt");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MFRItemRpt where MFRItemRptPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRItemRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ItemText", _MFRItemRpt.ItemText);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRItemRpt.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MFRItemRpt set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where MFRItemRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _MFRItemRpt.Notes);
                                cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRItemRpt.HistoryPK);
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

        public void MFRItemRpt_Approved(MFRItemRpt _MFRItemRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRItemRpt set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where MFRItemRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRItemRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRItemRpt.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFRItemRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFRItemRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRItemRpt.ApprovedUsersID);
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

        public void MFRItemRpt_Reject(MFRItemRpt _MFRItemRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRItemRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFRItemRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRItemRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRItemRpt.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFRItemRpt set status= 2,LastUpdate=@LastUpdate where MFRItemRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
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

        public void MFRItemRpt_Void(MFRItemRpt _MFRItemRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRItemRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFRItemRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRItemRpt.MFRItemRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRItemRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRItemRpt.VoidUsersID);
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

        public List<MFRItemRptCombo> MFRItemRpt_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MFRItemRptCombo> L_MFRItemRpt = new List<MFRItemRptCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  MFRItemRptPK, ItemText FROM [MFRItemRpt]  where status = 2 order by MFRItemRptPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MFRItemRptCombo M_MFRItemRpt = new MFRItemRptCombo();
                                    M_MFRItemRpt.MFRItemRptPK = Convert.ToInt32(dr["MFRItemRptPK"]);
                                    M_MFRItemRpt.ItemText = Convert.ToString(dr["ItemText"]);
                                    L_MFRItemRpt.Add(M_MFRItemRpt);
                                }

                            }
                            return L_MFRItemRpt;
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