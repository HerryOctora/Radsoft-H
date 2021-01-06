using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class FundIndexReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundIndex] " +
                            "([FundIndexPK],[HistoryPK],[Status],[FundPK],[IndexPK],[BitDefault],";
        string _paramaterCommand = "@FundPK,@IndexPK,@BitDefault,";

        //2
        private FundIndex setFundIndex(SqlDataReader dr)
        {
            FundIndex M_FundIndex = new FundIndex();
            M_FundIndex.FundIndexPK = Convert.ToInt32(dr["FundIndexPK"]);
            M_FundIndex.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundIndex.Status = Convert.ToInt32(dr["Status"]);
            M_FundIndex.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundIndex.Notes = Convert.ToString(dr["Notes"]);
            M_FundIndex.BitDefault = Convert.ToBoolean(dr["BitDefault"]);
            M_FundIndex.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundIndex.FundID = Convert.ToString(dr["FundID"]);
            M_FundIndex.IndexPK = Convert.ToInt32(dr["IndexPK"]);
            M_FundIndex.IndexID = Convert.ToString(dr["IndexID"]);
            M_FundIndex.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundIndex.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundIndex.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundIndex.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundIndex.EntryTime = dr["EntryTime"].ToString();
            M_FundIndex.UpdateTime = dr["UpdateTime"].ToString();
            M_FundIndex.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundIndex.VoidTime = dr["VoidTime"].ToString();
            M_FundIndex.DBUserID = dr["DBUserID"].ToString();
            M_FundIndex.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundIndex.LastUpdate = dr["LastUpdate"].ToString();
            M_FundIndex.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundIndex;
        }

        public List<FundIndex> FundIndex_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundIndex> L_FundIndex = new List<FundIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundID, C.Name IndexID, A.* from FundIndex A
                            left join Fund B on A.FundPK = B.FundPK and B.status  = 2
                            left join [Index] C on A.IndexPK = C.IndexPK and C.status = 2
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundID, C.Name IndexID, A.* from FundIndex A
                            left join Fund B on A.FundPK = B.FundPK and B.status  = 2
                            left join [Index] C on A.IndexPK = C.IndexPK and C.status = 2";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundIndex.Add(setFundIndex(dr));
                                }
                            }
                            return L_FundIndex;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundIndex_Add(FundIndex _FundIndex, bool _havePrivillege)
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
                                 "Select isnull(max(FundIndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundIndex";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundIndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundIndex";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BitDefault", _FundIndex.BitDefault);
                        cmd.Parameters.AddWithValue("@FundPK", _FundIndex.FundPK);
                        cmd.Parameters.AddWithValue("@IndexPK", _FundIndex.IndexPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundIndex.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundIndex");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundIndex_Update(FundIndex _FundIndex, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundIndex.FundIndexPK, _FundIndex.HistoryPK, "FundIndex");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundIndex set status=2, Notes=@Notes,BitDefault=@BitDefault,FundPK=@FundPK,IndexPK=@IndexPK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundIndexPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundIndex.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundIndex.Notes);
                            cmd.Parameters.AddWithValue("@BitDefault", _FundIndex.BitDefault);
                            cmd.Parameters.AddWithValue("@FundPK", _FundIndex.FundPK);
                            cmd.Parameters.AddWithValue("@IndexPK", _FundIndex.IndexPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundIndexPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundIndex.EntryUsersID);
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
                                cmd.CommandText = "Update FundIndex set Notes=@Notes,BitDefault=@BitDefault,FundPK=@FundPK,IndexPK=@IndexPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundIndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundIndex.Notes);
                                cmd.Parameters.AddWithValue("@BitDefault", _FundIndex.BitDefault);
                                cmd.Parameters.AddWithValue("@FundPK", _FundIndex.FundPK);
                                cmd.Parameters.AddWithValue("@IndexPK", _FundIndex.IndexPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundIndex.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundIndex.FundIndexPK, "FundIndex");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From FundIndex where FundIndexPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@BitDefault", _FundIndex.BitDefault);
                                cmd.Parameters.AddWithValue("@FundPK", _FundIndex.FundPK);
                                cmd.Parameters.AddWithValue("@IndexPK", _FundIndex.IndexPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundIndex.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundIndex set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where FundIndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundIndex.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundIndex.HistoryPK);
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

        public void FundIndex_Approved(FundIndex _FundIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundIndex set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundIndexpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundIndex.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundIndex.ApprovedUsersID);
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

        public void FundIndex_Reject(FundIndex _FundIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where FundIndexpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundIndex.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundIndex set status= 2,LastUpdate=@LastUpdate  where FundIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
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

        public void FundIndex_Void(FundIndex _FundIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundIndexpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundIndex.FundIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundIndex.VoidUsersID);
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