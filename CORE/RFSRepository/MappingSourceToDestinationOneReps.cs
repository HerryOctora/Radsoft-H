using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class MappingSourceToDestinationOneReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MappingSourceToDestinationOne] " +
                            "([MappingSourceToDestinationOnePK],[HistoryPK],[Status],[COAFromSourcePK],[COADestinationOnePK],";
        string _paramaterCommand = "@COAFromSourcePK,@COADestinationOnePK,";

        //2

        private MappingSourceToDestinationOne setMappingSourceToDestinationOne(SqlDataReader dr)
        {
            MappingSourceToDestinationOne M_MappingSourceToDestinationOne = new MappingSourceToDestinationOne();
            M_MappingSourceToDestinationOne.MappingSourceToDestinationOnePK = Convert.ToInt32(dr["MappingSourceToDestinationOnePK"]);
            M_MappingSourceToDestinationOne.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MappingSourceToDestinationOne.Status = Convert.ToInt32(dr["Status"]);
            M_MappingSourceToDestinationOne.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MappingSourceToDestinationOne.Notes = Convert.ToString(dr["Notes"]);
            M_MappingSourceToDestinationOne.COAFromSourcePK = Convert.ToInt32(dr["COAFromSourcePK"]);
            M_MappingSourceToDestinationOne.COAFromSourceID = Convert.ToString(dr["COAFromSourceID"]);
            M_MappingSourceToDestinationOne.COAFromSourceName = Convert.ToString(dr["COAFromSourceName"]);
            M_MappingSourceToDestinationOne.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
            M_MappingSourceToDestinationOne.COADestinationOneID = Convert.ToString(dr["COADestinationOneID"]);
            M_MappingSourceToDestinationOne.COADestinationOneName = Convert.ToString(dr["COADestinationOneName"]);
            M_MappingSourceToDestinationOne.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MappingSourceToDestinationOne.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MappingSourceToDestinationOne.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MappingSourceToDestinationOne.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MappingSourceToDestinationOne.EntryTime = dr["EntryTime"].ToString();
            M_MappingSourceToDestinationOne.UpdateTime = dr["UpdateTime"].ToString();
            M_MappingSourceToDestinationOne.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MappingSourceToDestinationOne.VoidTime = dr["VoidTime"].ToString();
            M_MappingSourceToDestinationOne.DBUserID = dr["DBUserID"].ToString();
            M_MappingSourceToDestinationOne.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MappingSourceToDestinationOne.LastUpdate = dr["LastUpdate"].ToString();
            M_MappingSourceToDestinationOne.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_MappingSourceToDestinationOne;
        }

        public List<MappingSourceToDestinationOne> MappingSourceToDestinationOne_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MappingSourceToDestinationOne> L_MappingSourceToDestinationOne = new List<MappingSourceToDestinationOne>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID,B.Name COAFromSourceName ,C.ID COADestinationOneID,C.Name COADestinationOneName ,A.* from MappingSourceToDestinationOne A
                                                left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                                                left join COADestinationOne C on A.COADestinationOnePK = C.COADestinationOnePK  and C.status = 2 
                                                where A.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID,B.Name COAFromSourceName ,C.ID COADestinationOneID,C.Name COADestinationOneName ,A.* from MappingSourceToDestinationOne A
                                                left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                                                left join COADestinationOne C on A.COADestinationOnePK = C.COADestinationOnePK  and C.status = 2 
                                                order by COAFromSourcePK,COADestinationOnePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MappingSourceToDestinationOne.Add(setMappingSourceToDestinationOne(dr));
                                }
                            }
                            return L_MappingSourceToDestinationOne;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MappingSourceToDestinationOne_Add(MappingSourceToDestinationOne _MappingSourceToDestinationOne, bool _havePrivillege)
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
                                 "Select isnull(max(MappingSourceToDestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MappingSourceToDestinationOne";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MappingSourceToDestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(MappingSourceToDestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MappingSourceToDestinationOne";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COAFromSourcePK", _MappingSourceToDestinationOne.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@COADestinationOnePK", _MappingSourceToDestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MappingSourceToDestinationOne.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MappingSourceToDestinationOne");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MappingSourceToDestinationOne_Update(MappingSourceToDestinationOne _MappingSourceToDestinationOne, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_MappingSourceToDestinationOne.MappingSourceToDestinationOnePK, _MappingSourceToDestinationOne.HistoryPK, "MappingSourceToDestinationOne"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MappingSourceToDestinationOne set status=2, Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,COADestinationOnePK=@COADestinationOnePK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MappingSourceToDestinationOnePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _MappingSourceToDestinationOne.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                            cmd.Parameters.AddWithValue("@Notes", _MappingSourceToDestinationOne.Notes);
                            cmd.Parameters.AddWithValue("@COAFromSourcePK", _MappingSourceToDestinationOne.COAFromSourcePK);
                            cmd.Parameters.AddWithValue("@COADestinationOnePK", _MappingSourceToDestinationOne.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _MappingSourceToDestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MappingSourceToDestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MappingSourceToDestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MappingSourceToDestinationOnePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _MappingSourceToDestinationOne.EntryUsersID);
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
                                cmd.CommandText = "Update MappingSourceToDestinationOne set Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,COADestinationOnePK=@COADestinationOnePK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where MappingSourceToDestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _MappingSourceToDestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                                cmd.Parameters.AddWithValue("@Notes", _MappingSourceToDestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _MappingSourceToDestinationOne.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _MappingSourceToDestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MappingSourceToDestinationOne.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_MappingSourceToDestinationOne.MappingSourceToDestinationOnePK, "MappingSourceToDestinationOne");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MappingSourceToDestinationOne where MappingSourceToDestinationOnePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MappingSourceToDestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _MappingSourceToDestinationOne.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _MappingSourceToDestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MappingSourceToDestinationOne.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MappingSourceToDestinationOne set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where MappingSourceToDestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _MappingSourceToDestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MappingSourceToDestinationOne.HistoryPK);
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

        public void MappingSourceToDestinationOne_Approved(MappingSourceToDestinationOne _MappingSourceToDestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MappingSourceToDestinationOne set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where MappingSourceToDestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _MappingSourceToDestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MappingSourceToDestinationOne.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MappingSourceToDestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MappingSourceToDestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MappingSourceToDestinationOne.ApprovedUsersID);
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

        public void MappingSourceToDestinationOne_Reject(MappingSourceToDestinationOne _MappingSourceToDestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MappingSourceToDestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MappingSourceToDestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _MappingSourceToDestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MappingSourceToDestinationOne.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MappingSourceToDestinationOne set status= 2,LastUpdate=@LastUpdate  where MappingSourceToDestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
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

        public void MappingSourceToDestinationOne_Void(MappingSourceToDestinationOne _MappingSourceToDestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MappingSourceToDestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MappingSourceToDestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MappingSourceToDestinationOne.MappingSourceToDestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _MappingSourceToDestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MappingSourceToDestinationOne.VoidUsersID);
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

    }
}