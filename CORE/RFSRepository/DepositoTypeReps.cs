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
    public class DepositoTypeReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[DepositoType] " +
                            "([DepositoTypePK],[HistoryPK],[Status],[DepType],[Description],[TenorLimit],[CollateralLimit],";
        string _paramaterCommand = "@DepType,@Description,@TenorLimit,@CollateralLimit,";

        //2
        private DepositoType setDepositoType(SqlDataReader dr)
        {
            DepositoType M_DepositoType = new DepositoType();
            M_DepositoType.DepositoTypePK = Convert.ToInt32(dr["DepositoTypePK"]);
            M_DepositoType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DepositoType.Status = Convert.ToInt32(dr["Status"]);
            M_DepositoType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_DepositoType.Notes = Convert.ToString(dr["Notes"]);
            M_DepositoType.DepType = dr["DepType"].ToString();
            M_DepositoType.Description = Convert.ToString(dr["Description"]);
            M_DepositoType.TenorLimit = Convert.ToDecimal(dr["TenorLimit"]);
            M_DepositoType.CollateralLimit = Convert.ToDecimal(dr["CollateralLimit"]);
            M_DepositoType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_DepositoType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_DepositoType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_DepositoType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_DepositoType.EntryTime = dr["EntryTime"].ToString();
            M_DepositoType.UpdateTime = dr["UpdateTime"].ToString();
            M_DepositoType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_DepositoType.VoidTime = dr["VoidTime"].ToString();
            M_DepositoType.DBUserID = dr["DBUserID"].ToString();
            M_DepositoType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_DepositoType.LastUpdate = dr["LastUpdate"].ToString();
            M_DepositoType.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_DepositoType;
        }

        public List<DepositoType> DepositoType_Select(int _status)
        {
            
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepositoType> L_DepositoType = new List<DepositoType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from DepositoType where status= @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from DepositoType order by DepType";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DepositoType.Add(setDepositoType(dr));
                                }
                            }
                            return L_DepositoType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

    
        public int DepositoType_Add(DepositoType _depositoType, bool _havePrivillege)
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
                                 "Select isnull(max(DepositoTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from DepositoType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _depositoType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(DepositoTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from DepositoType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@DepType", _depositoType.DepType);
                        cmd.Parameters.AddWithValue("@Description", _depositoType.Description);
                        cmd.Parameters.AddWithValue("@TenorLimit", _depositoType.TenorLimit);
                        cmd.Parameters.AddWithValue("@CollateralLimit", _depositoType.CollateralLimit);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _depositoType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "DepositoType");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int DepositoType_Update(DepositoType _depositoType, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_depositoType.DepositoTypePK, _depositoType.HistoryPK, "DepositoType");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update DepositoType set status=2,Notes=@Notes,DepType=@DepType,Description=@Description,TenorLimit=@TenorLimit,CollateralLimit=@CollateralLimit,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastUpdate " +
                                "where DepositoTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _depositoType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                            cmd.Parameters.AddWithValue("@DepType", _depositoType.DepType);
                            cmd.Parameters.AddWithValue("@Notes", _depositoType.Notes);
                            cmd.Parameters.AddWithValue("@Description", _depositoType.Description);
                            cmd.Parameters.AddWithValue("@TenorLimit", _depositoType.TenorLimit);
                            cmd.Parameters.AddWithValue("@CollateralLimit", _depositoType.CollateralLimit);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _depositoType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _depositoType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update DepositoType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where DepositoTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _depositoType.EntryUsersID);
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
                                cmd.CommandText = "Update DepositoType set Notes=@Notes,DepType=@DepType,Description=@Description,TenorLimit=@TenorLimit,CollateralLimit=@CollateralLimit, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@LastUpdate " +
                                    "where DepositoTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _depositoType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                                cmd.Parameters.AddWithValue("@DepType", _depositoType.DepType);
                                cmd.Parameters.AddWithValue("@Notes", _depositoType.Notes);
                                cmd.Parameters.AddWithValue("@Description", _depositoType.Description);
                                cmd.Parameters.AddWithValue("@TenorLimit", _depositoType.TenorLimit);
                                cmd.Parameters.AddWithValue("@CollateralLimit", _depositoType.CollateralLimit);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _depositoType.EntryUsersID);
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

                                _newHisPK = _host.Get_NewHistoryPK(_depositoType.DepositoTypePK, "DepositoType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From DepositoType where DepositoTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _depositoType.HistoryPK);
                                cmd.Parameters.AddWithValue("@DepType", _depositoType.DepType);
                                cmd.Parameters.AddWithValue("@Description", _depositoType.Description);
                                cmd.Parameters.AddWithValue("@TenorLimit", _depositoType.TenorLimit);
                                cmd.Parameters.AddWithValue("@CollateralLimit", _depositoType.CollateralLimit);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _depositoType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update DepositoType set status= 4,Notes=@Notes,LastUpdate=@LastUpdate  where DepositoTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _depositoType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _depositoType.HistoryPK);
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

        public void DepositoType_Approved(DepositoType _depositoType)
        {
             try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DepositoType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where DepositoTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _depositoType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _depositoType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DepositoType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where DepositoTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _depositoType.ApprovedUsersID);
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

        public void DepositoType_Reject(DepositoType _depositoType)
        {
              try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DepositoType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where DepositoTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _depositoType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _depositoType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DepositoType set status= 2,LastUpdate=@LastUpdate where DepositoTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
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

        public void DepositoType_Void(DepositoType _depositoType)
        {
              try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DepositoType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate = @LastUpdate " +
                            "where DepositoTypepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _depositoType.DepositoTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _depositoType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _depositoType.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<DepositoTypeCombo> DepositoType_Combo()
        {
            
            
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepositoTypeCombo> L_DepositoType = new List<DepositoTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepositoTypePK,DepType, Description FROM [DepositoType]  where status = 2 order by DepType";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepositoTypeCombo M_DepositoType = new DepositoTypeCombo();
                                    M_DepositoType.DepositoTypePK = Convert.ToInt32(dr["DepositoTypePK"]);
                                    M_DepositoType.DepType = Convert.ToString(dr["DepType"]);
                                    M_DepositoType.Description = Convert.ToString(dr["Description"]);
                                    L_DepositoType.Add(M_DepositoType);
                                }

                            }
                            return L_DepositoType;
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