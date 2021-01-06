using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace RFSRepository
{
    public class ReksadanaTypeReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = "INSERT INTO [dbo].[ReksadanaType] " +
                            "([ReksadanaTypePK],[HistoryPK],[Status],[RDType],[Description],[MKBDNo],";
        string _paramaterCommand = "@RDType,@Description,@MKBDNo,";

        //2
        private ReksadanaType setReksadanaType(SqlDataReader dr)
        {
            ReksadanaType M_ReksadanaType = new ReksadanaType();
            M_ReksadanaType.ReksadanaTypePK = Convert.ToInt32(dr["ReksadanaTypePK"]);
            M_ReksadanaType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ReksadanaType.Status = Convert.ToInt32(dr["Status"]);
            M_ReksadanaType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ReksadanaType.Notes = Convert.ToString(dr["Notes"]);
            M_ReksadanaType.RDType = dr["RDType"].ToString();
            M_ReksadanaType.Description = Convert.ToString(dr["Description"]);
            M_ReksadanaType.MKBDNo = Convert.ToInt32(dr["MKBDNo"]);
            M_ReksadanaType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ReksadanaType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ReksadanaType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ReksadanaType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ReksadanaType.EntryTime = dr["EntryTime"].ToString();
            M_ReksadanaType.UpdateTime = dr["UpdateTime"].ToString();
            M_ReksadanaType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ReksadanaType.VoidTime = dr["VoidTime"].ToString();
            M_ReksadanaType.DBUserID = dr["DBUserID"].ToString();
            M_ReksadanaType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ReksadanaType.LastUpdate = dr["LastUpdate"].ToString();
            M_ReksadanaType.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_ReksadanaType;
        }

        public List<ReksadanaType> ReksadanaType_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReksadanaType> L_ReksadanaType = new List<ReksadanaType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from ReksadanaType where status= @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from ReksadanaType order by RDType";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ReksadanaType.Add(setReksadanaType(dr));
                                }
                            }
                            return L_ReksadanaType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    
        public int ReksadanaType_Add(ReksadanaType _reksadanaType, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ReksadanaTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from ReksadanaType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _reksadanaType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ReksadanaTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from ReksadanaType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@RDType", _reksadanaType.RDType);
                        cmd.Parameters.AddWithValue("@Description", _reksadanaType.Description);
                        cmd.Parameters.AddWithValue("@MKBDNo", _reksadanaType.MKBDNo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _reksadanaType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ReksadanaType");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int ReksadanaType_Update(ReksadanaType _reksadanaType, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_reksadanaType.ReksadanaTypePK, _reksadanaType.HistoryPK, "ReksadanaType");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                   

                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ReksadanaType set status=2,Notes=@Notes,RDType=@RDType,Description=@Description,MKBDNo=@MKBDNo,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where ReksadanaTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _reksadanaType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                            cmd.Parameters.AddWithValue("@RDType", _reksadanaType.RDType);
                            cmd.Parameters.AddWithValue("@Notes", _reksadanaType.Notes);
                            cmd.Parameters.AddWithValue("@Description", _reksadanaType.Description);
                            cmd.Parameters.AddWithValue("@MKBDNo", _reksadanaType.MKBDNo);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _reksadanaType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _reksadanaType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ReksadanaType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where ReksadanaTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _reksadanaType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update ReksadanaType set Notes=@Notes,RDType=@RDType,Description=@Description,MKBDNo=@MKBDNo, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where ReksadanaTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _reksadanaType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@RDType", _reksadanaType.RDType);
                                cmd.Parameters.AddWithValue("@Notes", _reksadanaType.Notes);
                                cmd.Parameters.AddWithValue("@Description", _reksadanaType.Description);
                                cmd.Parameters.AddWithValue("@MKBDNo", _reksadanaType.MKBDNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _reksadanaType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                _newHisPK = _host.Get_NewHistoryPK(_reksadanaType.ReksadanaTypePK, "ReksadanaType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ReksadanaType where ReksadanaTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _reksadanaType.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@RDType", _reksadanaType.RDType);
                                cmd.Parameters.AddWithValue("@Description", _reksadanaType.Description);
                                cmd.Parameters.AddWithValue("@MKBDNo", _reksadanaType.MKBDNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _reksadanaType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ReksadanaType set status= 4,Notes=@Notes, " +
                                    "LastUpdate=@LastUpdate where ReksadanaTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _reksadanaType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _reksadanaType.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public void ReksadanaType_Approved(ReksadanaType _reksadanaType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where ReksadanaTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _reksadanaType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _reksadanaType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ReksadanaType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where ReksadanaTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _reksadanaType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ReksadanaType_Reject(ReksadanaType _reksadanaType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ReksadanaTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _reksadanaType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _reksadanaType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ReksadanaType set status= 2,LastUpdate=@LastUpdate where ReksadanaTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ReksadanaType_Void(ReksadanaType _reksadanaType)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ReksadanaTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _reksadanaType.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _reksadanaType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _reksadanaType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<ReksadanaTypeCombo> ReksadanaType_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReksadanaTypeCombo> L_ReksadanaType = new List<ReksadanaTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ReksadanaTypePK,RDType, Description FROM [ReksadanaType]  where status = 2 order by RDType";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReksadanaTypeCombo M_ReksadanaType = new ReksadanaTypeCombo();
                                    M_ReksadanaType.ReksadanaTypePK = Convert.ToInt32(dr["ReksadanaTypePK"]);
                                    M_ReksadanaType.RDType = Convert.ToString(dr["RDType"]);
                                    M_ReksadanaType.Description = Convert.ToString(dr["Description"]);
                                    L_ReksadanaType.Add(M_ReksadanaType);
                                }

                            }
                            return L_ReksadanaType;
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