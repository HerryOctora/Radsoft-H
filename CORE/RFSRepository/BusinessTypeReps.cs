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
    public class BusinessTypeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BusinessType] " +
                            "([BusinessTypePK],[HistoryPK],[Status],[ID],[Name],[SectorPK],";
        string _paramaterCommand = "@ID,@Name,@SectorPK,";

        //2
        private BusinessType setBusinessType(SqlDataReader dr)
        {
            BusinessType M_BusinessType = new BusinessType();
            M_BusinessType.BusinessTypePK = Convert.ToInt32(dr["BusinessTypePK"]);
            M_BusinessType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BusinessType.Status = Convert.ToInt32(dr["Status"]);
            M_BusinessType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BusinessType.Notes = Convert.ToString(dr["Notes"]);
            M_BusinessType.ID = dr["ID"].ToString();
            M_BusinessType.Name = dr["Name"].ToString();
            M_BusinessType.SectorPK = Convert.ToInt32(dr["SectorPK"]);
            M_BusinessType.SectorID = Convert.ToString(dr["SectorID"]);
            M_BusinessType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BusinessType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BusinessType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BusinessType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BusinessType.EntryTime = dr["EntryTime"].ToString();
            M_BusinessType.UpdateTime = dr["UpdateTime"].ToString();
            M_BusinessType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BusinessType.VoidTime = dr["VoidTime"].ToString();
            M_BusinessType.DBUserID = dr["DBUserID"].ToString();
            M_BusinessType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BusinessType.LastUpdate = dr["LastUpdate"].ToString();
            M_BusinessType.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BusinessType;
        }

        public List<BusinessType> BusinessType_Select(int _status)
        {
           
           try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BusinessType> L_businessType = new List<BusinessType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when BT.status=1 then 'PENDING' else Case When BT.status = 2 then 'APPROVED' else Case when BT.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,S.ID SectorID,BT.* from BusinessType BT left join " +
                            "Sector S on BT.SectorPK = S.SectorPK and S.status = 2 " +
                            "where BT.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when BT.status=1 then 'PENDING' else Case When BT.status = 2 then 'APPROVED' else Case when BT.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,S.ID SectorID,BT.* from BusinessType BT left join " +
                            "Sector S on BT.SectorPK = S.SectorPK and S.status = 2 " + 
                            "order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_businessType.Add(setBusinessType(dr));
                                }
                            }
                            return L_businessType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
          
        }

        public int BusinessType_Add(BusinessType _businessType, bool _havePrivillege)
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
                                 "Select isnull(max(BusinessTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From BusinessType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _businessType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BusinessTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From BusinessType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _businessType.ID);
                        cmd.Parameters.AddWithValue("@Name", _businessType.Name);
                        cmd.Parameters.AddWithValue("@SectorPK", _businessType.SectorPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _businessType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "BusinessType");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BusinessType_Update(BusinessType _businessType, bool _havePrivillege)
        {
           
           try
            {
                int _newHisPK;
                int status = _host.Get_Status(_businessType.BusinessTypePK, _businessType.HistoryPK, "businessType");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BusinessType set status=2,Notes=@Notes,ID=@ID,Name=@Name,SectorPK=@SectorPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BusinessTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _businessType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                            cmd.Parameters.AddWithValue("@ID", _businessType.ID);
                            cmd.Parameters.AddWithValue("@Notes", _businessType.Notes);
                            cmd.Parameters.AddWithValue("@Name", _businessType.Name);
                            cmd.Parameters.AddWithValue("@SectorPK", _businessType.SectorPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _businessType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _businessType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BusinessType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BusinessTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _businessType.EntryUsersID);
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
                                cmd.CommandText = "Update BusinessType set Notes=@Notes,ID=@ID,Name=@Name,SectorPK=@SectorPK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@LastUpdate " +
                                    "where BusinessTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _businessType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                                cmd.Parameters.AddWithValue("@ID", _businessType.ID);
                                cmd.Parameters.AddWithValue("@Notes", _businessType.Notes);
                                cmd.Parameters.AddWithValue("@Name", _businessType.Name);
                                cmd.Parameters.AddWithValue("@SectorPK", _businessType.SectorPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _businessType.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_businessType.BusinessTypePK, "BusinessType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BusinessType where BusinessTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _businessType.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _businessType.ID);
                                cmd.Parameters.AddWithValue("@Name", _businessType.Name);
                                cmd.Parameters.AddWithValue("@SectorPK", _businessType.SectorPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _businessType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BusinessType set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where BusinessTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _businessType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _businessType.HistoryPK);
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

        public void BusinessType_Approved(BusinessType _businessType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BusinessType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate " +
                            "where BusinessTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _businessType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _businessType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BusinessType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BusinessTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _businessType.ApprovedUsersID);
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

        public void BusinessType_Reject(BusinessType _businessType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BusinessType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BusinessTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _businessType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _businessType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BusinessType set status= 2,LastUpdate=@LastUpdate where BusinessTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
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

        public void BusinessType_Void(BusinessType _businessType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BusinessType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where BusinessTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _businessType.BusinessTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _businessType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _businessType.VoidUsersID);
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

        public List<BusinessTypeCombo> BusinessType_Combo()
        {
           
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BusinessTypeCombo> L_BusinessType = new List<BusinessTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  BusinessTypePK,ID + ' - ' + Name as ID, Name FROM [BusinessType]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BusinessTypeCombo M_BusinessType = new BusinessTypeCombo();
                                    M_BusinessType.BusinessTypePK = Convert.ToInt32(dr["BusinessTypePK"]);
                                    M_BusinessType.ID = Convert.ToString(dr["ID"]);
                                    M_BusinessType.Name = Convert.ToString(dr["Name"]);
                                    L_BusinessType.Add(M_BusinessType);
                                }

                            }
                            return L_BusinessType;
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