using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;



namespace RFSRepository
{
    public class PPH21SetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[PPH21Setup] " +
                            "([PPH21SetupPK],[HistoryPK],[Status],[Date],[RangeFrom],[RangeTo],[Percentage],";
        string _paramaterCommand = "@Date,@RangeFrom,@RangeTo,@Percentage,";

        //2
        private PPH21Setup setPPH21Setup(SqlDataReader dr)
        {
            PPH21Setup M_PPH21Setup = new PPH21Setup();
            M_PPH21Setup.PPH21SetupPK = Convert.ToInt32(dr["PPH21SetupPK"]);
            M_PPH21Setup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_PPH21Setup.Status = Convert.ToInt32(dr["Status"]);
            M_PPH21Setup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_PPH21Setup.Notes = Convert.ToString(dr["Notes"]);
            M_PPH21Setup.Date = Convert.ToString(dr["Date"]);
            M_PPH21Setup.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_PPH21Setup.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_PPH21Setup.Percentage = Convert.ToDecimal(dr["Percentage"]);
            M_PPH21Setup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_PPH21Setup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_PPH21Setup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_PPH21Setup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_PPH21Setup.EntryTime = dr["EntryTime"].ToString();
            M_PPH21Setup.UpdateTime = dr["UpdateTime"].ToString();
            M_PPH21Setup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_PPH21Setup.VoidTime = dr["VoidTime"].ToString();
            M_PPH21Setup.DBUserID = dr["DBUserID"].ToString();
            M_PPH21Setup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_PPH21Setup.LastUpdate = dr["LastUpdate"].ToString();
            M_PPH21Setup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_PPH21Setup;
        }

        public List<PPH21Setup> PPH21Setup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<PPH21Setup> L_PPH21Setup = new List<PPH21Setup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from PPH21Setup where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from PPH21Setup order by Date";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_PPH21Setup.Add(setPPH21Setup(dr));
                                }
                            }
                            return L_PPH21Setup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int PPH21Setup_Add(PPH21Setup _PPH21Setup, bool _havePrivillege)
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
                                 "Select isnull(max(PPH21SetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from PPH21Setup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _PPH21Setup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(PPH21SetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from PPH21Setup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _PPH21Setup.Date);
                        cmd.Parameters.AddWithValue("@RangeFrom", _PPH21Setup.RangeFrom);
                        cmd.Parameters.AddWithValue("@RangeTo", _PPH21Setup.RangeTo);
                        cmd.Parameters.AddWithValue("@Percentage", _PPH21Setup.Percentage);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _PPH21Setup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "PPH21Setup");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int PPH21Setup_Update(PPH21Setup _PPH21Setup, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_PPH21Setup.PPH21SetupPK, _PPH21Setup.HistoryPK, "PPH21Setup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update PPH21Setup set status=2, Notes=@Notes,Date=@Date,RangeFrom=@RangeFrom,RangeTo=@RangeTo,Percentage=@Percentage," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where PPH21SetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _PPH21Setup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _PPH21Setup.Notes);
                            
                            cmd.Parameters.AddWithValue("@Date", _PPH21Setup.Date);
                            cmd.Parameters.AddWithValue("@RangeFrom", _PPH21Setup.RangeFrom);
                            cmd.Parameters.AddWithValue("@RangeTo", _PPH21Setup.RangeTo);
                            cmd.Parameters.AddWithValue("@Percentage", _PPH21Setup.Percentage);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _PPH21Setup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _PPH21Setup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update PPH21Setup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where PPH21SetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _PPH21Setup.EntryUsersID);
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
                                cmd.CommandText = "Update PPH21Setup set Notes=@Notes,Date=@Date,RangeFrom=@RangeFrom,RangeTo=@RangeTo,Percentage=@Percentage," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where PPH21SetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _PPH21Setup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _PPH21Setup.Notes);

                                cmd.Parameters.AddWithValue("@Date", _PPH21Setup.Date);
                                cmd.Parameters.AddWithValue("@RangeFrom", _PPH21Setup.RangeFrom);
                                cmd.Parameters.AddWithValue("@RangeTo", _PPH21Setup.RangeTo);
                                cmd.Parameters.AddWithValue("@Percentage", _PPH21Setup.Percentage);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _PPH21Setup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_PPH21Setup.PPH21SetupPK, "PPH21Setup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From PPH21Setup where PPH21SetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _PPH21Setup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _PPH21Setup.Date);
                                cmd.Parameters.AddWithValue("@RangeFrom", _PPH21Setup.RangeFrom);
                                cmd.Parameters.AddWithValue("@RangeTo", _PPH21Setup.RangeTo);
                                cmd.Parameters.AddWithValue("@Percentage", _PPH21Setup.Percentage);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _PPH21Setup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update PPH21Setup set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where PPH21SetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _PPH21Setup.PPH21SetupPK);
                                cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _PPH21Setup.HistoryPK);
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

        public void PPH21Setup_Approved(PPH21Setup _PPH21Setup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PPH21Setup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where PPH21SetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PPH21Setup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _PPH21Setup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update PPH21Setup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where PPH21SetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PPH21Setup.ApprovedUsersID);
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

        public void PPH21Setup_Reject(PPH21Setup _PPH21Setup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PPH21Setup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PPH21SetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PPH21Setup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PPH21Setup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update PPH21Setup set status= 2,LastUpdate=@LastUpdate where PPH21SetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
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

        public void PPH21Setup_Void(PPH21Setup _PPH21Setup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PPH21Setup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PPH21SetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PPH21Setup.PPH21SetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PPH21Setup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PPH21Setup.VoidUsersID);
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

        //public List<GroupsCombo> Groups_Combo()
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            List<GroupsCombo> L_Groups = new List<GroupsCombo>();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT  GroupsPK,ID +' - '+ Name ID, Name FROM [Groups]  where status = 2 order by GroupsPK";
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        while (dr.Read())
        //                        {
        //                            GroupsCombo M_Groups = new GroupsCombo();
        //                            M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
        //                            M_Groups.ID = Convert.ToString(dr["ID"]);
        //                            M_Groups.Name = Convert.ToString(dr["Name"]);
        //                            L_Groups.Add(M_Groups);
        //                        }

        //                    }
        //                    return L_Groups;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }


        //}



    }
}
