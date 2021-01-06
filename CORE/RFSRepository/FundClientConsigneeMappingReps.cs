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
    public class FundClientConsigneeMappingReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientConsigneeMapping] " +
                            "([FundClientConsigneeMappingPK],[HistoryPK],[Status],[FundClientPK],[ConsigneePK],[Description],";
        string _paramaterCommand = "@FundClientPK,@ConsigneePK,@Description,";

        //2
        private FundClientConsigneeMapping setFundClientConsigneeMapping(SqlDataReader dr)
        {
            FundClientConsigneeMapping M_FundClientConsigneeMapping = new FundClientConsigneeMapping();
            M_FundClientConsigneeMapping.FundClientConsigneeMappingPK = Convert.ToInt32(dr["FundClientConsigneeMappingPK"]);
            M_FundClientConsigneeMapping.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientConsigneeMapping.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientConsigneeMapping.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientConsigneeMapping.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientConsigneeMapping.FundClientDesc = dr["FundClientDesc"].ToString();
            M_FundClientConsigneeMapping.ConsigneeDesc = dr["ConsigneeDesc"].ToString();
            M_FundClientConsigneeMapping.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientConsigneeMapping.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_FundClientConsigneeMapping.Description = dr["Description"].ToString();
            M_FundClientConsigneeMapping.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientConsigneeMapping.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientConsigneeMapping.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientConsigneeMapping.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientConsigneeMapping.EntryTime = dr["EntryTime"].ToString();
            M_FundClientConsigneeMapping.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientConsigneeMapping.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientConsigneeMapping.VoidTime = dr["VoidTime"].ToString();
            M_FundClientConsigneeMapping.DBUserID = dr["DBUserID"].ToString();
            M_FundClientConsigneeMapping.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientConsigneeMapping.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientConsigneeMapping.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);

            return M_FundClientConsigneeMapping;
        }

        public List<FundClientConsigneeMapping> FundClientConsigneeMapping_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientConsigneeMapping> L_FundClientConsigneeMapping = new List<FundClientConsigneeMapping>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                                    Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.status = 3 then 'VOID' else 'WAITING' END END END statusDesc,
                                    B.Name FundClientDesc, C.Name ConsigneeDesc,* 
                                    from FundClientConsigneeMapping A
                                    left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                    left join Consignee C on A.ConsigneePK = C.ConsigneePK and C.status in (1,2)
                                    where A.status = @Status order by FundClientConsigneeMappingPK
                                ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                    Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.status = 3 then 'VOID' else 'WAITING' END END END statusDesc,
                                    B.Name FundClientDesc, C.Name ConsigneeDesc,* 
                                    from FundClientConsigneeMapping A
                                    left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                    left join Consignee C on A.ConsigneePK = C.ConsigneePK and C.status in (1,2)
                                    order by FundClientConsigneeMappingPK
                            ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientConsigneeMapping.Add(setFundClientConsigneeMapping(dr));
                                }
                            }
                            return L_FundClientConsigneeMapping;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int FundClientConsigneeMapping_Add(FundClientConsigneeMapping _FundClientConsigneeMapping, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(FundClientConsigneeMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundClientConsigneeMapping";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientConsigneeMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundClientConsigneeMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundClientConsigneeMapping";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientConsigneeMapping.FundClientPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _FundClientConsigneeMapping.ConsigneePK);
                        cmd.Parameters.AddWithValue("@Description", _FundClientConsigneeMapping.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientConsigneeMapping.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundClientConsigneeMapping");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int FundClientConsigneeMapping_Update(FundClientConsigneeMapping _FundClientConsigneeMapping, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FundClientConsigneeMapping.FundClientConsigneeMappingPK, _FundClientConsigneeMapping.HistoryPK, "FundClientConsigneeMapping");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FundClientConsigneeMapping set status=2, Notes=@Notes,FundClientPK=@FundClientPK,ConsigneePK=@ConsigneePK,Description=@Description,
                                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                                where FundClientConsigneeMappingPK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientConsigneeMapping.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientConsigneeMapping.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientConsigneeMapping.FundClientPK);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _FundClientConsigneeMapping.ConsigneePK);
                            cmd.Parameters.AddWithValue("@Description", _FundClientConsigneeMapping.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientConsigneeMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientConsigneeMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientConsigneeMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientConsigneeMappingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientConsigneeMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = @"Update FundClientConsigneeMapping set Notes=@Notes,FundClientPK=@FundClientPK,ConsigneePK=@ConsigneePK,Description=@Description,
                                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                                    where FundClientConsigneeMappingPK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientConsigneeMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientConsigneeMapping.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientConsigneeMapping.FundClientPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _FundClientConsigneeMapping.ConsigneePK);
                                cmd.Parameters.AddWithValue("@Description", _FundClientConsigneeMapping.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientConsigneeMapping.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientConsigneeMapping.FundClientConsigneeMappingPK, "FundClientConsigneeMapping");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientConsigneeMapping where FundClientConsigneeMappingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientConsigneeMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientConsigneeMapping.FundClientPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _FundClientConsigneeMapping.ConsigneePK);
                                cmd.Parameters.AddWithValue("@Description", _FundClientConsigneeMapping.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientConsigneeMapping.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update FundClientConsigneeMapping set status= 4,Notes=@Notes,
                                                    lastupdate=@lastupdate where FundClientConsigneeMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientConsigneeMapping.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientConsigneeMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void FundClientConsigneeMapping_Approved(FundClientConsigneeMapping _FundClientConsigneeMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update FundClientConsigneeMapping set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate
                                            where FundClientConsigneeMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientConsigneeMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientConsigneeMapping.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientConsigneeMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientConsigneeMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientConsigneeMapping.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundClientConsigneeMapping_Reject(FundClientConsigneeMapping _FundClientConsigneeMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update FundClientConsigneeMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate
                                            where FundClientConsigneeMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientConsigneeMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientConsigneeMapping.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientConsigneeMapping set status= 2,lastupdate=@lastupdate where FundClientConsigneeMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundClientConsigneeMapping_Void(FundClientConsigneeMapping _FundClientConsigneeMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update FundClientConsigneeMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate
                                            where FundClientConsigneeMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientConsigneeMapping.FundClientConsigneeMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientConsigneeMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientConsigneeMapping.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

    }
}