using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class MFeeSetupOptionalReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MFeeSetupOptional] " +
                            "([MFeeSetupOptionalPK],[HistoryPK],[Status],[Date],[FundPK],[FeePercent],[Days],";
        string _paramaterCommand = "@Date,@FundPk,@FeePercent,@Days,";

        private MFeeSetupOptional setMFeeSetupOptional(SqlDataReader dr)
        {
            MFeeSetupOptional M_MFeeSetupOptional = new MFeeSetupOptional();
            M_MFeeSetupOptional.MFeeSetupOptionalPK = Convert.ToInt32(dr["MFeeSetupOptionalPK"]);
            M_MFeeSetupOptional.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MFeeSetupOptional.Status = Convert.ToInt32(dr["Status"]);
            M_MFeeSetupOptional.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MFeeSetupOptional.Notes = Convert.ToString(dr["Notes"]);
            M_MFeeSetupOptional.Date = Convert.ToDateTime(dr["Date"]);
            M_MFeeSetupOptional.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_MFeeSetupOptional.FundID = Convert.ToString(dr["FundID"]);
            M_MFeeSetupOptional.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
            M_MFeeSetupOptional.Days = Convert.ToInt32(dr["Days"]);
            M_MFeeSetupOptional.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MFeeSetupOptional.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MFeeSetupOptional.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MFeeSetupOptional.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MFeeSetupOptional.EntryTime = dr["EntryTime"].ToString();
            M_MFeeSetupOptional.UpdateTime = dr["UpdateTime"].ToString();
            M_MFeeSetupOptional.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MFeeSetupOptional.VoidTime = dr["VoidTime"].ToString();
            M_MFeeSetupOptional.DBUserID = dr["DBUserID"].ToString();
            M_MFeeSetupOptional.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MFeeSetupOptional.LastUpdate = dr["LastUpdate"].ToString();
            M_MFeeSetupOptional.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_MFeeSetupOptional;
        }

        public List<MFeeSetupOptional> MFeeSetupOptional_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MFeeSetupOptional> L_MFeeSetupOptional = new List<MFeeSetupOptional>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when MF.status=1 then 'PENDING' else Case When MF.status = 2 then 'APPROVED' else Case when MF.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundID,MF.* from MFeeSetupOptional MF left join 
                           Fund A on MF.FundPK = A.FundPK and A.status = 2 
                           where MF.status = @status
                           order by FundPK,Days
                             ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when MF.status=1 then 'PENDING' else Case When MF.status = 2 then 'APPROVED' else Case when MF.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundID,MF.* from MFeeSetupOptional MF left join 
                           Fund A on MF.FundPK = A.FundPK and A.status = 2 
                           order by FundPK,Days";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MFeeSetupOptional.Add(setMFeeSetupOptional(dr));
                                }
                            }
                            return L_MFeeSetupOptional;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MFeeSetupOptional_Add(MFeeSetupOptional _MFeeSetupOptional, bool _havePrivillege)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(MFeeSetupOptionalPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MFeeSetupOptional";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFeeSetupOptional.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(MFeeSetupOptionalPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MFeeSetupOptional";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _MFeeSetupOptional.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _MFeeSetupOptional.FundPK);
                        cmd.Parameters.AddWithValue("@FeePercent", _MFeeSetupOptional.FeePercent);
                        cmd.Parameters.AddWithValue("@Days", _MFeeSetupOptional.Days);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MFeeSetupOptional.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MFeeSetupOptional");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int MFeeSetupOptional_Update(MFeeSetupOptional _MFeeSetupOptional, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_MFeeSetupOptional.MFeeSetupOptionalPK, _MFeeSetupOptional.HistoryPK, "MFeeSetupOptional");;
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFeeSetupOptional set status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,FeePercent=@FeePercent,Days=@Days," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MFeeSetupOptionalPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _MFeeSetupOptional.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                            cmd.Parameters.AddWithValue("@Notes", _MFeeSetupOptional.Notes);
                            cmd.Parameters.AddWithValue("@Date", _MFeeSetupOptional.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _MFeeSetupOptional.FundPK);
                            cmd.Parameters.AddWithValue("@FeePercent", _MFeeSetupOptional.FeePercent);
                            cmd.Parameters.AddWithValue("@Days", _MFeeSetupOptional.Days);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _MFeeSetupOptional.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFeeSetupOptional.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFeeSetupOptional set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFeeSetupOptionalPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _MFeeSetupOptional.EntryUsersID);
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
                                cmd.CommandText = "Update MFeeSetupOptional set Notes=@Notes,Date=@Date,FundPK=@FundPK,FeePercent=@FeePercent,Days=@Days," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where MFeeSetupOptionalPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFeeSetupOptional.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                                cmd.Parameters.AddWithValue("@Notes", _MFeeSetupOptional.Notes);
                                cmd.Parameters.AddWithValue("@Date", _MFeeSetupOptional.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _MFeeSetupOptional.FundPK);
                                cmd.Parameters.AddWithValue("@FeePercent", _MFeeSetupOptional.FeePercent);
                                cmd.Parameters.AddWithValue("@Days", _MFeeSetupOptional.Days);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFeeSetupOptional.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_MFeeSetupOptional.MFeeSetupOptionalPK, "MFeeSetupOptional");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MFeeSetupOptional where MFeeSetupOptionalPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFeeSetupOptional.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _MFeeSetupOptional.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _MFeeSetupOptional.FundPK);
                                cmd.Parameters.AddWithValue("@FeePercent", _MFeeSetupOptional.FeePercent);
                                cmd.Parameters.AddWithValue("@Days", _MFeeSetupOptional.Days);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFeeSetupOptional.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MFeeSetupOptional set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where MFeeSetupOptionalPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _MFeeSetupOptional.Notes);
                                cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFeeSetupOptional.HistoryPK);
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

        public void MFeeSetupOptional_Approved(MFeeSetupOptional _MFeeSetupOptional)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFeeSetupOptional set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where MFeeSetupOptionalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFeeSetupOptional.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFeeSetupOptional.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFeeSetupOptional set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFeeSetupOptionalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFeeSetupOptional.ApprovedUsersID);
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

        public void MFeeSetupOptional_Reject(MFeeSetupOptional _MFeeSetupOptional)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFeeSetupOptional set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFeeSetupOptionalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFeeSetupOptional.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFeeSetupOptional.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFeeSetupOptional set status= 2,LastUpdate=@LastUpdate  where MFeeSetupOptionalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
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

        public void MFeeSetupOptional_Void(MFeeSetupOptional _MFeeSetupOptional)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFeeSetupOptional set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFeeSetupOptionalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFeeSetupOptional.MFeeSetupOptionalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFeeSetupOptional.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFeeSetupOptional.VoidUsersID);
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
