using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class TrxUnitPaymentMappingReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[TrxUnitPaymentMapping] " +
                            "([TrxUnitPaymentMappingPK],[HistoryPK],[Status],[TrxUnitPaymentTypePK],[TrxUnitPaymentProviderPK],";
        string _paramaterCommand = "@TrxUnitPaymentTypePK,@TrxUnitPaymentProviderPK,";

        private TrxUnitPaymentMapping setTrxUnitPaymentMapping(SqlDataReader dr)
        {
            TrxUnitPaymentMapping M_TrxUnitPaymentMapping = new TrxUnitPaymentMapping();

            M_TrxUnitPaymentMapping.TrxUnitPaymentMappingPK = Convert.ToInt32(dr["TrxUnitPaymentMappingPK"]);
            M_TrxUnitPaymentMapping.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TrxUnitPaymentMapping.Status = Convert.ToInt32(dr["Status"]);
            M_TrxUnitPaymentMapping.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TrxUnitPaymentMapping.Notes = Convert.ToString(dr["Notes"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentTypePK = Convert.ToInt32(dr["TrxUnitPaymentTypePK"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentTypeID = Convert.ToString(dr["TrxUnitPaymentTypeID"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentTypeName = Convert.ToString(dr["TrxUnitPaymentTypeName"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentProviderPK = Convert.ToInt32(dr["TrxUnitPaymentProviderPK"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentProviderID = Convert.ToString(dr["TrxUnitPaymentProviderID"]);
            M_TrxUnitPaymentMapping.TrxUnitPaymentProviderName = Convert.ToString(dr["TrxUnitPaymentProviderName"]);
            M_TrxUnitPaymentMapping.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TrxUnitPaymentMapping.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TrxUnitPaymentMapping.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TrxUnitPaymentMapping.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TrxUnitPaymentMapping.EntryTime = dr["EntryTime"].ToString();
            M_TrxUnitPaymentMapping.UpdateTime = dr["UpdateTime"].ToString();
            M_TrxUnitPaymentMapping.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TrxUnitPaymentMapping.VoidTime = dr["VoidTime"].ToString();
            M_TrxUnitPaymentMapping.DBUserID = dr["DBUserID"].ToString();
            M_TrxUnitPaymentMapping.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TrxUnitPaymentMapping.LastUpdate = dr["LastUpdate"].ToString();
            M_TrxUnitPaymentMapping.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_TrxUnitPaymentMapping;
        }

        public List<TrxUnitPaymentMapping> TrxUnitPaymentMapping_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    List<TrxUnitPaymentMapping> L_TrxUnitPaymentMapping = new List<TrxUnitPaymentMapping>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when RP.status=1 then 'PENDING' else Case When RP.status = 2 then 'APPROVED' else Case when RP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID TrxUnitPaymentProviderID,R.Name TrxUnitPaymentProviderName, P.ID TrxUnitPaymentTypeID,P.Name TrxUnitPaymentTypeName,RP.* from TrxUnitPaymentMapping RP left join 
                            TrxUnitPaymentProvider R on RP.TrxUnitPaymentProviderPK = R.TrxUnitPaymentProviderPK and R.status = 2 left join 
                            TrxUnitPaymentType P on RP.TrxUnitPaymentTypePK = P.TrxUnitPaymentTypePK and P.status = 2 
                            where RP.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {

                            cmd.CommandText = @"Select case when RP.status=1 then 'PENDING' else Case When RP.status = 2 then 'APPROVED' else Case when RP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID TrxUnitPaymentProviderID,R.Name TrxUnitPaymentProviderName, P.ID TrxUnitPaymentTypeID,P.Name TrxUnitPaymentTypeName,RP.* from TrxUnitPaymentMapping RP left join
                            TrxUnitPaymentProvider R on RP.TrxUnitPaymentProviderPK = R.TrxUnitPaymentProviderPK and R.status = 2 left join 
                            TrxUnitPaymentType P on RP.TrxUnitPaymentTypePK = P.TrxUnitPaymentTypePK and P.status = 2
                            order by TrxUnitPaymentProviderPK,TrxUnitPaymentTypePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxUnitPaymentMapping.Add(setTrxUnitPaymentMapping(dr));
                                }
                            }
                            return L_TrxUnitPaymentMapping;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentMapping_Add(TrxUnitPaymentMapping _TrxUnitPaymentMapping, bool _havePrivillege)
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
                                 "Select isnull(max(TrxUnitPaymentMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from TrxUnitPaymentMapping";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(TrxUnitPaymentMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from TrxUnitPaymentMapping";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _TrxUnitPaymentMapping.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _TrxUnitPaymentMapping.TrxUnitPaymentTypePK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TrxUnitPaymentMapping.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TrxUnitPaymentMapping");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentMapping_Update(TrxUnitPaymentMapping _TrxUnitPaymentMapping, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_TrxUnitPaymentMapping.TrxUnitPaymentMappingPK, _TrxUnitPaymentMapping.HistoryPK, "TrxUnitPaymentMapping");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxUnitPaymentMapping set status=2, Notes=@Notes,TrxUnitPaymentProviderPK=@TrxUnitPaymentProviderPK,TrxUnitPaymentTypePK=@TrxUnitPaymentTypePK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where TrxUnitPaymentMappingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentMapping.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                            cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentMapping.Notes);
                            cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _TrxUnitPaymentMapping.TrxUnitPaymentProviderPK);
                            cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _TrxUnitPaymentMapping.TrxUnitPaymentTypePK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxUnitPaymentMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxUnitPaymentMappingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentMapping.EntryUsersID);
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
                                cmd.CommandText = "Update TrxUnitPaymentMapping set Notes=@Notes,TrxUnitPaymentProviderPK=@TrxUnitPaymentProviderPK,TrxUnitPaymentTypePK=@TrxUnitPaymentTypePK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where TrxUnitPaymentMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentMapping.Notes);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _TrxUnitPaymentMapping.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _TrxUnitPaymentMapping.TrxUnitPaymentTypePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentMapping.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TrxUnitPaymentMapping.TrxUnitPaymentMappingPK, "TrxUnitPaymentMapping");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From TrxUnitPaymentMapping where TrxUnitPaymentMappingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _TrxUnitPaymentMapping.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _TrxUnitPaymentMapping.TrxUnitPaymentTypePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentMapping.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TrxUnitPaymentMapping set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where TrxUnitPaymentMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentMapping.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentMapping.HistoryPK);
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

        public void TrxUnitPaymentMapping_Approved(TrxUnitPaymentMapping _TrxUnitPaymentMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentMapping set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where TrxUnitPaymentMappingpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentMapping.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxUnitPaymentMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentMapping.ApprovedUsersID);
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

        public void TrxUnitPaymentMapping_Reject(TrxUnitPaymentMapping _TrxUnitPaymentMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where TrxUnitPaymentMappingpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentMapping.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentMapping set status= 2,LastUpdate=@LastUpdate  where TrxUnitPaymentMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
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

        public void TrxUnitPaymentMapping_Void(TrxUnitPaymentMapping _TrxUnitPaymentMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where TrxUnitPaymentMappingpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentMapping.TrxUnitPaymentMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentMapping.VoidUsersID);
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
