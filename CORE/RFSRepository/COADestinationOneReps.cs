using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class COADestinationOneReps
    {
         Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[COADestinationOne] " +
                            "([COADestinationOnePK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private COADestinationOne setCOADestinationOne(SqlDataReader dr)
        {
            COADestinationOne M_COADestinationOne = new COADestinationOne();
            M_COADestinationOne.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
            M_COADestinationOne.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_COADestinationOne.Status = Convert.ToInt32(dr["Status"]);
            M_COADestinationOne.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_COADestinationOne.Notes = Convert.ToString(dr["Notes"]);
            M_COADestinationOne.ID = dr["ID"].ToString();
            M_COADestinationOne.Name = dr["Name"].ToString();
            M_COADestinationOne.EntryUsersID = dr["EntryUsersID"].ToString();
            M_COADestinationOne.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_COADestinationOne.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_COADestinationOne.VoidUsersID = dr["VoidUsersID"].ToString();
            M_COADestinationOne.EntryTime = dr["EntryTime"].ToString();
            M_COADestinationOne.UpdateTime = dr["UpdateTime"].ToString();
            M_COADestinationOne.ApprovedTime = dr["ApprovedTime"].ToString();
            M_COADestinationOne.VoidTime = dr["VoidTime"].ToString();
            M_COADestinationOne.DBUserID = dr["DBUserID"].ToString();
            M_COADestinationOne.DBTerminalID = dr["DBTerminalID"].ToString();
            M_COADestinationOne.LastUpdate = dr["LastUpdate"].ToString();
            M_COADestinationOne.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_COADestinationOne;
        }

        public List<COADestinationOne> COADestinationOne_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationOne> L_COADestinationOne = new List<COADestinationOne>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COADestinationOne 
                                                where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COADestinationOne ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_COADestinationOne.Add(setCOADestinationOne(dr));
                                }
                            }
                            return L_COADestinationOne;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationOne_Add(COADestinationOne _COADestinationOne, bool _havePrivillege)
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
                                 "Select isnull(max(COADestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from COADestinationOne";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(COADestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from COADestinationOne";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _COADestinationOne.ID);
                        cmd.Parameters.AddWithValue("@Name", _COADestinationOne.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _COADestinationOne.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "COADestinationOne");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationOne_Update(COADestinationOne _COADestinationOne, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_COADestinationOne.COADestinationOnePK, _COADestinationOne.HistoryPK, "COADestinationOne");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationOne set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationOnePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOne.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@Notes", _COADestinationOne.Notes);
                            cmd.Parameters.AddWithValue("@ID", _COADestinationOne.ID);
                            cmd.Parameters.AddWithValue("@Name", _COADestinationOne.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationOnePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOne.EntryUsersID);
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
                                cmd.CommandText = "Update COADestinationOne set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@ID", _COADestinationOne.ID);
                                cmd.Parameters.AddWithValue("@Name", _COADestinationOne.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOne.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_COADestinationOne.COADestinationOnePK, "COADestinationOne");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From COADestinationOne where COADestinationOnePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _COADestinationOne.ID);
                                cmd.Parameters.AddWithValue("@Name", _COADestinationOne.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOne.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update COADestinationOne set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where COADestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOne.HistoryPK);
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

        public void COADestinationOne_Approved(COADestinationOne _COADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOne set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where COADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOne.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOne.ApprovedUsersID);
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

        public void COADestinationOne_Reject(COADestinationOne _COADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOne.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationOne set status= 2,LastUpdate=@LastUpdate where COADestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
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

        public void COADestinationOne_Void(COADestinationOne _COADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOne.VoidUsersID);
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

        public List<COADestinationOneCombo> COADestinationOne_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationOneCombo> L_COADestinationOne = new List<COADestinationOneCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  COADestinationOnePK,ID +' - '+ Name ID, Name FROM [COADestinationOne]  where status = 2 order by COADestinationOnePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    COADestinationOneCombo M_COADestinationOne = new COADestinationOneCombo();
                                    M_COADestinationOne.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
                                    M_COADestinationOne.ID = Convert.ToString(dr["ID"]);
                                    M_COADestinationOne.Name = Convert.ToString(dr["Name"]);
                                    L_COADestinationOne.Add(M_COADestinationOne);
                                }

                            }
                            return L_COADestinationOne;
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