using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
namespace RFSRepository
{
    public class COADestinationTwoReps
    {
          Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[COADestinationTwo] " +
                            "([COADestinationTwoPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private COADestinationTwo setCOADestinationTwo(SqlDataReader dr)
        {
            COADestinationTwo M_COADestinationTwo = new COADestinationTwo();
            M_COADestinationTwo.COADestinationTwoPK = Convert.ToInt32(dr["COADestinationTwoPK"]);
            M_COADestinationTwo.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_COADestinationTwo.Status = Convert.ToInt32(dr["Status"]);
            M_COADestinationTwo.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_COADestinationTwo.Notes = Convert.ToString(dr["Notes"]);
            M_COADestinationTwo.ID = dr["ID"].ToString();
            M_COADestinationTwo.Name = dr["Name"].ToString();
            M_COADestinationTwo.EntryUsersID = dr["EntryUsersID"].ToString();
            M_COADestinationTwo.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_COADestinationTwo.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_COADestinationTwo.VoidUsersID = dr["VoidUsersID"].ToString();
            M_COADestinationTwo.EntryTime = dr["EntryTime"].ToString();
            M_COADestinationTwo.UpdateTime = dr["UpdateTime"].ToString();
            M_COADestinationTwo.ApprovedTime = dr["ApprovedTime"].ToString();
            M_COADestinationTwo.VoidTime = dr["VoidTime"].ToString();
            M_COADestinationTwo.DBUserID = dr["DBUserID"].ToString();
            M_COADestinationTwo.DBTerminalID = dr["DBTerminalID"].ToString();
            M_COADestinationTwo.LastUpdate = dr["LastUpdate"].ToString();
            M_COADestinationTwo.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_COADestinationTwo;
        }

        public List<COADestinationTwo> COADestinationTwo_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationTwo> L_COADestinationTwo = new List<COADestinationTwo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COADestinationTwo 
                                                where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from COADestinationTwo ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_COADestinationTwo.Add(setCOADestinationTwo(dr));
                                }
                            }
                            return L_COADestinationTwo;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationTwo_Add(COADestinationTwo _COADestinationTwo, bool _havePrivillege)
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
                                 "Select isnull(max(COADestinationTwoPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from COADestinationTwo";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(COADestinationTwoPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from COADestinationTwo";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _COADestinationTwo.ID);
                        cmd.Parameters.AddWithValue("@Name", _COADestinationTwo.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _COADestinationTwo.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "COADestinationTwo");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationTwo_Update(COADestinationTwo _COADestinationTwo, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_COADestinationTwo.COADestinationTwoPK, _COADestinationTwo.HistoryPK, "COADestinationTwo");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationTwo set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationTwoPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwo.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                            cmd.Parameters.AddWithValue("@Notes", _COADestinationTwo.Notes);
                            cmd.Parameters.AddWithValue("@ID", _COADestinationTwo.ID);
                            cmd.Parameters.AddWithValue("@Name", _COADestinationTwo.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationTwo set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationTwoPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwo.EntryUsersID);
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
                                cmd.CommandText = "Update COADestinationTwo set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationTwoPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwo.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationTwo.Notes);
                                cmd.Parameters.AddWithValue("@ID", _COADestinationTwo.ID);
                                cmd.Parameters.AddWithValue("@Name", _COADestinationTwo.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwo.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_COADestinationTwo.COADestinationTwoPK, "COADestinationTwo");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From COADestinationTwo where COADestinationTwoPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwo.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _COADestinationTwo.ID);
                                cmd.Parameters.AddWithValue("@Name", _COADestinationTwo.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwo.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update COADestinationTwo set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where COADestinationTwoPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationTwo.Notes);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwo.HistoryPK);
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

        public void COADestinationTwo_Approved(COADestinationTwo _COADestinationTwo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwo set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where COADestinationTwoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwo.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwo.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationTwo set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationTwoPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwo.ApprovedUsersID);
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

        public void COADestinationTwo_Reject(COADestinationTwo _COADestinationTwo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwo set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationTwoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwo.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwo.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationTwo set status= 2,LastUpdate=@LastUpdate where COADestinationTwoPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
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

        public void COADestinationTwo_Void(COADestinationTwo _COADestinationTwo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwo set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationTwoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwo.COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwo.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwo.VoidUsersID);
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

        public List<COADestinationTwoCombo> COADestinationTwo_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationTwoCombo> L_COADestinationTwo = new List<COADestinationTwoCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  COADestinationTwoPK,ID +' - '+ Name ID, Name FROM [COADestinationTwo]  where status = 2 order by COADestinationTwoPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    COADestinationTwoCombo M_COADestinationTwo = new COADestinationTwoCombo();
                                    M_COADestinationTwo.COADestinationTwoPK = Convert.ToInt32(dr["COADestinationTwoPK"]);
                                    M_COADestinationTwo.ID = Convert.ToString(dr["ID"]);
                                    M_COADestinationTwo.Name = Convert.ToString(dr["Name"]);
                                    L_COADestinationTwo.Add(M_COADestinationTwo);
                                }

                            }
                            return L_COADestinationTwo;
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