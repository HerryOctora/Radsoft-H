using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class TrxUnitPaymentProviderReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[TrxUnitPaymentProvider] " +
                            "([TrxUnitPaymentProviderPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        private TrxUnitPaymentProvider setTrxUnitPaymentProvider(SqlDataReader dr)
        {
            TrxUnitPaymentProvider M_TrxUnitPaymentProvider = new TrxUnitPaymentProvider();
            M_TrxUnitPaymentProvider.TrxUnitPaymentProviderPK = Convert.ToInt32(dr["TrxUnitPaymentProviderPK"]);
            M_TrxUnitPaymentProvider.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TrxUnitPaymentProvider.Status = Convert.ToInt32(dr["Status"]);
            M_TrxUnitPaymentProvider.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TrxUnitPaymentProvider.Notes = Convert.ToString(dr["Notes"]);
            M_TrxUnitPaymentProvider.ID = dr["ID"].ToString();
            M_TrxUnitPaymentProvider.Name = dr["Name"].ToString();
            M_TrxUnitPaymentProvider.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TrxUnitPaymentProvider.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TrxUnitPaymentProvider.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TrxUnitPaymentProvider.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TrxUnitPaymentProvider.EntryTime = dr["EntryTime"].ToString();
            M_TrxUnitPaymentProvider.UpdateTime = dr["UpdateTime"].ToString();
            M_TrxUnitPaymentProvider.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TrxUnitPaymentProvider.VoidTime = dr["VoidTime"].ToString();
            M_TrxUnitPaymentProvider.DBUserID = dr["DBUserID"].ToString();
            M_TrxUnitPaymentProvider.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TrxUnitPaymentProvider.LastUpdate = dr["LastUpdate"].ToString();
            M_TrxUnitPaymentProvider.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_TrxUnitPaymentProvider;
        }

        public List<TrxUnitPaymentProvider> TrxUnitPaymentProvider_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxUnitPaymentProvider> L_TrxUnitPaymentProvider = new List<TrxUnitPaymentProvider>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from TrxUnitPaymentProvider where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from TrxUnitPaymentProvider order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxUnitPaymentProvider.Add(setTrxUnitPaymentProvider(dr));
                                }
                            }
                            return L_TrxUnitPaymentProvider;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentProvider_Add(TrxUnitPaymentProvider _TrxUnitPaymentProvider, bool _havePrivillege)
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
                                 "Select isnull(max(TrxUnitPaymentProviderPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From TrxUnitPaymentProvider";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentProvider.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(TrxUnitPaymentProviderPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From TrxUnitPaymentProvider";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentProvider.ID);
                        cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentProvider.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TrxUnitPaymentProvider.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TrxUnitPaymentProvider");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentProvider_Update(TrxUnitPaymentProvider _TrxUnitPaymentProvider, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, _TrxUnitPaymentProvider.HistoryPK, "TrxUnitPaymentProvider");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxUnitPaymentProvider set status=2,Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where TrxUnitPaymentProviderPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentProvider.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                            cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentProvider.Notes);
                            cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentProvider.ID);
                            cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentProvider.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentProvider.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentProvider.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxUnitPaymentProvider set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxUnitPaymentProviderPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentProvider.EntryUsersID);
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
                                cmd.CommandText = "Update TrxUnitPaymentProvider set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where TrxUnitPaymentProviderPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentProvider.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentProvider.Notes);
                                cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentProvider.ID);
                                cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentProvider.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentProvider.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TrxUnitPaymentProvider.TrxUnitPaymentProviderPK, "TrxUnitPaymentProvider");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TrxUnitPaymentProvider where TrxUnitPaymentProviderPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentProvider.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentProvider.ID);
                                cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentProvider.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentProvider.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TrxUnitPaymentProvider set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate " +
                                    " where TrxUnitPaymentProviderPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentProvider.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentProvider.HistoryPK);
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

        public void TrxUnitPaymentProvider_Approved(TrxUnitPaymentProvider _TrxUnitPaymentProvider)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentProvider set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where TrxUnitPaymentProviderPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentProvider.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentProvider.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentProvider set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxUnitPaymentProviderPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentProvider.ApprovedUsersID);
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

        public void TrxUnitPaymentProvider_Reject(TrxUnitPaymentProvider _TrxUnitPaymentProvider)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentProvider set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where TrxUnitPaymentProviderPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentProvider.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentProvider.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentProvider set status= 2,LastUpdate=@LastUpdate where TrxUnitPaymentProviderPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
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

        public void TrxUnitPaymentProvider_Void(TrxUnitPaymentProvider _TrxUnitPaymentProvider)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentProvider set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where TrxUnitPaymentProviderPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentProvider.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentProvider.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentProvider.VoidUsersID);
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

        public List<TrxUnitPaymentProviderCombo> TrxUnitPaymentProvider_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxUnitPaymentProviderCombo> L_TrxUnitPaymentProvider = new List<TrxUnitPaymentProviderCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  TrxUnitPaymentProviderPK,ID + ' - ' + Name as ID, Name FROM [TrxUnitPaymentProvider]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TrxUnitPaymentProviderCombo M_TrxUnitPaymentProvider = new TrxUnitPaymentProviderCombo();
                                    M_TrxUnitPaymentProvider.TrxUnitPaymentProviderPK = Convert.ToInt32(dr["TrxUnitPaymentProviderPK"]);
                                    M_TrxUnitPaymentProvider.ID = Convert.ToString(dr["ID"]);
                                    M_TrxUnitPaymentProvider.Name = Convert.ToString(dr["Name"]);
                                    L_TrxUnitPaymentProvider.Add(M_TrxUnitPaymentProvider);
                                }

                            }
                            return L_TrxUnitPaymentProvider;
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
