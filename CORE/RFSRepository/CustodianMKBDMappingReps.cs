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
    public class CustodianMKBDMappingReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CustodianMKBDMapping] " +
                            "([CustodianMKBDMappingPK],[HistoryPK],[Status],[CustodianPK],[Description],[CounterpartType],[ClientAccountType],[MKBD07],[COL],";

        string _paramaterCommand = "@CustodianPK,@Description,@CounterpartType,@ClientAccountType,@MKBD07,@COL, ";

        //2
        private CustodianMKBDMapping setCustodianMKBDMapping(SqlDataReader dr)
        {
            CustodianMKBDMapping M_CustodianMKBDMapping = new CustodianMKBDMapping();
            M_CustodianMKBDMapping.CustodianMKBDMappingPK = Convert.ToInt32(dr["CustodianMKBDMappingPK"]);
            M_CustodianMKBDMapping.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CustodianMKBDMapping.Status = Convert.ToInt32(dr["Status"]);
            M_CustodianMKBDMapping.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CustodianMKBDMapping.Notes = Convert.ToString(dr["Notes"]);
            M_CustodianMKBDMapping.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
            M_CustodianMKBDMapping.CustodianID = Convert.ToString(dr["CustodianID"]);
            M_CustodianMKBDMapping.CustodianName = Convert.ToString(dr["CustodianName"]);
            M_CustodianMKBDMapping.Description = Convert.ToString(dr["Description"]);
            M_CustodianMKBDMapping.CounterpartType = Convert.ToInt32(dr["CounterpartType"]);
            M_CustodianMKBDMapping.CounterpartTypeDesc = Convert.ToString(dr["CounterpartTypeDesc"]);
            M_CustodianMKBDMapping.ClientAccountType = Convert.ToInt32(dr["ClientAccountType"]);
            M_CustodianMKBDMapping.ClientAccountTypeDesc = Convert.ToString(dr["ClientAccountTypeDesc"]);
            M_CustodianMKBDMapping.MKBD07 = Convert.ToInt32(dr["MKBD07"]);
            M_CustodianMKBDMapping.COL = Convert.ToInt32(dr["COL"]);
            M_CustodianMKBDMapping.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_CustodianMKBDMapping.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_CustodianMKBDMapping.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_CustodianMKBDMapping.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_CustodianMKBDMapping.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_CustodianMKBDMapping.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_CustodianMKBDMapping.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_CustodianMKBDMapping.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_CustodianMKBDMapping.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_CustodianMKBDMapping.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_CustodianMKBDMapping.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_CustodianMKBDMapping.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CustodianMKBDMapping;
        }

        public List<CustodianMKBDMapping> CustodianMKBDMapping_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianMKBDMapping> L_custodianMKBDMapping = new List<CustodianMKBDMapping>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " select case when CM.status=1 then 'PENDING' else Case When CM.status = 2 then 'APPROVED' else Case when CM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV1.DescOne CounterpartTypeDesc,MV2.DescOne ClientAccountTypeDesc,C.ID CustodianID,C.Name CustodianName,  " +
                            " * from CustodianMKBDMapping CM     " +
                            " left join MasterValue MV1 on CM.CounterpartType = MV1.Code and MV1.ID = 'CounterpartType'    " +
                            " left join MasterValue MV2 on CM.ClientAccountType = MV2.Code and MV2.ID = 'ClientAccountType'    " +
                            " left join Custodian C on CM.CustodianPK = C.CustodianPK and  C.status = 2    " +
                            " where CM.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " select case when CM.status=1 then 'PENDING' else Case When CM.status = 2 then 'APPROVED' else Case when CM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV1.DescOne CounterpartTypeDesc,MV2.DescOne ClientAccountTypeDesc,C.ID CustodianID,C.Name CustodianName,  " +
                            " * from CustodianMKBDMapping CM     " +
                            " left join MasterValue MV1 on CM.CounterpartType = MV1.Code and MV1.ID = 'CounterpartType'    " +
                            " left join MasterValue MV2 on CM.ClientAccountType = MV2.Code and MV2.ID = 'ClientAccountType'    " +
                            " left join Custodian C on CM.CustodianPK = C.CustodianPK and  C.status = 2    ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_custodianMKBDMapping.Add(setCustodianMKBDMapping(dr));
                                }
                            }
                            return L_custodianMKBDMapping;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int CustodianMKBDMapping_Add(CustodianMKBDMapping _custodianMKBDMapping, bool _havePrivillege)
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
                                 "Select isnull(max(CustodianMKBDMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from CustodianMKBDMapping";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodianMKBDMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CustodianMKBDMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from CustodianMKBDMapping";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@CustodianPK", _custodianMKBDMapping.CustodianPK);
                        cmd.Parameters.AddWithValue("@Description", _custodianMKBDMapping.Description);
                        cmd.Parameters.AddWithValue("@CounterpartType", _custodianMKBDMapping.CounterpartType);
                        cmd.Parameters.AddWithValue("@ClientAccountType", _custodianMKBDMapping.ClientAccountType);
                        cmd.Parameters.AddWithValue("@MKBD07", _custodianMKBDMapping.MKBD07);
                        cmd.Parameters.AddWithValue("@COL", _custodianMKBDMapping.COL);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _custodianMKBDMapping.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CustodianMKBDMapping");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int CustodianMKBDMapping_Update(CustodianMKBDMapping _custodianMKBDMapping, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_custodianMKBDMapping.CustodianMKBDMappingPK, _custodianMKBDMapping.HistoryPK, "CustodianMKBDMapping");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update CustodianMKBDMapping set status=2,Notes=@Notes,CustodianPK=@CustodianPK,Description=@Description,CounterpartType=@CounterpartType,ClientAccountType=@ClientAccountType, " +
                                "MKBD07=@MKBD07,COL=@COL, " +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CustodianMKBDMappingPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _custodianMKBDMapping.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                            cmd.Parameters.AddWithValue("@Notes", _custodianMKBDMapping.Notes);
                            cmd.Parameters.AddWithValue("@CustodianPK", _custodianMKBDMapping.CustodianPK);
                            cmd.Parameters.AddWithValue("@Description", _custodianMKBDMapping.Description);
                            cmd.Parameters.AddWithValue("@CounterpartType", _custodianMKBDMapping.CounterpartType);
                            cmd.Parameters.AddWithValue("@ClientAccountType", _custodianMKBDMapping.ClientAccountType);
                            cmd.Parameters.AddWithValue("@MKBD07", _custodianMKBDMapping.MKBD07);
                            cmd.Parameters.AddWithValue("@COL", _custodianMKBDMapping.COL);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _custodianMKBDMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodianMKBDMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CustodianMKBDMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianMKBDMappingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _custodianMKBDMapping.EntryUsersID);
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
                                cmd.CommandText = "Update CustodianMKBDMapping set Notes=@Notes,CustodianPK=@CustodianPK,Description=@Description,CounterpartType=@CounterpartType,ClientAccountType=@ClientAccountType, " +
                                "MKBD07=@MKBD07,COL=@COL, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CustodianMKBDMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodianMKBDMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                                cmd.Parameters.AddWithValue("@Notes", _custodianMKBDMapping.Notes);
                                cmd.Parameters.AddWithValue("@CustodianPK", _custodianMKBDMapping.CustodianPK);
                                cmd.Parameters.AddWithValue("@Description", _custodianMKBDMapping.Description);
                                cmd.Parameters.AddWithValue("@CounterpartType", _custodianMKBDMapping.CounterpartType);
                                cmd.Parameters.AddWithValue("@ClientAccountType", _custodianMKBDMapping.ClientAccountType);
                                cmd.Parameters.AddWithValue("@MKBD07", _custodianMKBDMapping.MKBD07);
                                cmd.Parameters.AddWithValue("@COL", _custodianMKBDMapping.COL);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _custodianMKBDMapping.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_custodianMKBDMapping.CustodianMKBDMappingPK, "CustodianMKBDMapping");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CustodianMKBDMapping where CustodianMKBDMappingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodianMKBDMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@CustodianPK", _custodianMKBDMapping.CustodianPK);
                                cmd.Parameters.AddWithValue("@Description", _custodianMKBDMapping.Description);
                                cmd.Parameters.AddWithValue("@CounterpartType", _custodianMKBDMapping.CounterpartType);
                                cmd.Parameters.AddWithValue("@ClientAccountType", _custodianMKBDMapping.ClientAccountType);
                                cmd.Parameters.AddWithValue("@MKBD07", _custodianMKBDMapping.MKBD07);
                                cmd.Parameters.AddWithValue("@COL", _custodianMKBDMapping.COL);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _custodianMKBDMapping.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CustodianMKBDMapping set status = 4, Notes=@Notes, " +
                                " lastupdate=@lastupdate where CustodianMKBDMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _custodianMKBDMapping.Notes);
                                cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodianMKBDMapping.HistoryPK);
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

        public void CustodianMKBDMapping_Approved(CustodianMKBDMapping _custodianMKBDMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianMKBDMapping set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CustodianMKBDMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodianMKBDMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodianMKBDMapping.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustodianMKBDMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianMKBDMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodianMKBDMapping.ApprovedUsersID);
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

        public void CustodianMKBDMapping_Reject(CustodianMKBDMapping _custodianMKBDMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianMKBDMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CustodianMKBDMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodianMKBDMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodianMKBDMapping.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustodianMKBDMapping set status= 2,lastupdate=@lastupdate where CustodianMKBDMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
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

        public void CustodianMKBDMapping_Void(CustodianMKBDMapping _custodianMKBDMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodianMKBDMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where CustodianMKBDMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodianMKBDMapping.CustodianMKBDMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodianMKBDMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodianMKBDMapping.VoidUsersID);
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