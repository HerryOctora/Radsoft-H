using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class TrxUnitPaymentTypeReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[TrxUnitPaymentType] " +
                         "([TrxUnitPaymentTypePK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        private TrxUnitPaymentType setTrxUnitPaymentType(SqlDataReader dr)
        {
            TrxUnitPaymentType M_TrxUnitPaymentType = new TrxUnitPaymentType();
            M_TrxUnitPaymentType.TrxUnitPaymentTypePK = Convert.ToInt32(dr["TrxUnitPaymentTypePK"]);
            M_TrxUnitPaymentType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TrxUnitPaymentType.Status = Convert.ToInt32(dr["Status"]);
            M_TrxUnitPaymentType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TrxUnitPaymentType.Notes = Convert.ToString(dr["Notes"]);
            M_TrxUnitPaymentType.ID = dr["ID"].ToString();
            M_TrxUnitPaymentType.Name = dr["Name"].ToString();
            M_TrxUnitPaymentType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TrxUnitPaymentType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TrxUnitPaymentType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TrxUnitPaymentType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TrxUnitPaymentType.EntryTime = dr["EntryTime"].ToString();
            M_TrxUnitPaymentType.UpdateTime = dr["UpdateTime"].ToString();
            M_TrxUnitPaymentType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TrxUnitPaymentType.VoidTime = dr["VoidTime"].ToString();
            M_TrxUnitPaymentType.DBUserID = dr["DBUserID"].ToString();
            M_TrxUnitPaymentType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TrxUnitPaymentType.LastUpdate = dr["LastUpdate"].ToString();
            M_TrxUnitPaymentType.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_TrxUnitPaymentType;
        }

        public List<TrxUnitPaymentType> TrxUnitPaymentType_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxUnitPaymentType> L_TrxUnitPaymentType = new List<TrxUnitPaymentType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from TrxUnitPaymentType where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from TrxUnitPaymentType order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxUnitPaymentType.Add(setTrxUnitPaymentType(dr));
                                }
                            }
                            return L_TrxUnitPaymentType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentType_Add(TrxUnitPaymentType _TrxUnitPaymentType, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(TrxUnitPaymentTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from TrxUnitPaymentType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(TrxUnitPaymentTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from TrxUnitPaymentType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentType.ID);
                        cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentType.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TrxUnitPaymentType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TrxUnitPaymentType");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TrxUnitPaymentType_Update(TrxUnitPaymentType _TrxUnitPaymentType, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_TrxUnitPaymentType.TrxUnitPaymentTypePK, _TrxUnitPaymentType.HistoryPK, "TrxUnitPaymentType");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText = "Update TrxUnitPaymentType set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                                "where TrxUnitPaymentTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                            cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentType.Notes);
                            cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentType.ID);
                            cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentType.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxUnitPaymentType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where TrxUnitPaymentTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentType.EntryUsersID);
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
                                cmd.CommandText = "Update TrxUnitPaymentType set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                                "where TrxUnitPaymentTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentType.Notes);
                                cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentType.ID);
                                cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentType.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentType.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TrxUnitPaymentType.TrxUnitPaymentTypePK, "TrxUnitPaymentType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TrxUnitPaymentType where TrxUnitPaymentTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentType.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _TrxUnitPaymentType.ID);
                                cmd.Parameters.AddWithValue("@Name", _TrxUnitPaymentType.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TrxUnitPaymentType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TrxUnitPaymentType set status= 4,Notes=@Notes, lastUpdate = @LastUpdate where TrxUnitPaymentTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TrxUnitPaymentType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TrxUnitPaymentType.HistoryPK);
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

        public void TrxUnitPaymentType_Approved(TrxUnitPaymentType _TrxUnitPaymentType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate  " +
                            "where TrxUnitPaymentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TrxUnitPaymentType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where TrxUnitPaymentTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentType.ApprovedUsersID);
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

        public void TrxUnitPaymentType_Reject(TrxUnitPaymentType _TrxUnitPaymentType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where TrxUnitPaymentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxUnitPaymentType set status= 2,LastUpdate = @LastUpdate where TrxUnitPaymentTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
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

        public void TrxUnitPaymentType_Void(TrxUnitPaymentType _TrxUnitPaymentType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxUnitPaymentType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where TrxUnitPaymentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TrxUnitPaymentType.TrxUnitPaymentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _TrxUnitPaymentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TrxUnitPaymentType.VoidUsersID);
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

        public List<TrxUnitPaymentTypeCombo> TrxUnitPaymentType_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxUnitPaymentTypeCombo> L_Roles = new List<TrxUnitPaymentTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  TrxUnitPaymentTypePK,ID + ' - ' + Name as ID, Name  FROM [TrxUnitPaymentType]  where status = 2 order by TrxUnitPaymentTypePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TrxUnitPaymentTypeCombo M_TrxUnitPaymentType = new TrxUnitPaymentTypeCombo();
                                    M_TrxUnitPaymentType.TrxUnitPaymentTypePK = Convert.ToInt32(dr["TrxUnitPaymentTypePK"]);
                                    M_TrxUnitPaymentType.ID = Convert.ToString(dr["ID"]);
                                    M_TrxUnitPaymentType.Name = Convert.ToString(dr["Name"]);
                                    L_Roles.Add(M_TrxUnitPaymentType);
                                }
                            }
                            return L_Roles;
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
