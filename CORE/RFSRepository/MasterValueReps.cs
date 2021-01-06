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
    public class MasterValueReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[MasterValue] " +
                            "([MasterValuePK],[HistoryPK],[Status],[ID],[Type],[Code],[DescOne],[DescTwo],[Priority],[IsHighRisk],[RiskCDD],";
        string _paramaterCommand = "@ID,@Type,@Code,@DescOne,@DescTwo,@Priority,@IsHighRisk,@RiskCDD,";

        //2
        private MasterValue setMasterValue(SqlDataReader dr)
        {
            MasterValue M_MasterValue = new MasterValue();
            M_MasterValue.MasterValuePK = Convert.ToInt32(dr["MasterValuePK"]);
            M_MasterValue.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MasterValue.Status = Convert.ToInt32(dr["Status"]);
            M_MasterValue.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MasterValue.Notes = Convert.ToString(dr["Notes"]);
            M_MasterValue.ID = dr["ID"].ToString();
            M_MasterValue.Type = dr["Type"].ToString();
            M_MasterValue.Code = dr["Code"].ToString();
            M_MasterValue.DescOne = dr["DescOne"].ToString();
            M_MasterValue.DescTwo = dr["DescTwo"].ToString();
            M_MasterValue.Priority = Convert.ToInt32(dr["Priority"]);
            M_MasterValue.IsHighRisk = Convert.ToBoolean(dr["IsHighRisk"]);
            M_MasterValue.RiskCDD = Convert.ToInt32(dr["RiskCDD"]);
            M_MasterValue.RiskCDDDesc = Convert.ToString(dr["RiskCDDDesc"]);
            M_MasterValue.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MasterValue.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MasterValue.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MasterValue.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MasterValue.EntryTime = dr["EntryTime"].ToString();
            M_MasterValue.UpdateTime = dr["UpdateTime"].ToString();
            M_MasterValue.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MasterValue.VoidTime = dr["VoidTime"].ToString();
            M_MasterValue.DBUserID = dr["DBUserID"].ToString();
            M_MasterValue.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MasterValue.LastUpdate = dr["LastUpdate"].ToString();
            M_MasterValue.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_MasterValue;
        }

        public List<MasterValue> MasterValue_SelectByMasterValueCommissionType(string _masterValueID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (_masterValueID == "Month")
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue
                            Where ID = @MasterValueID and status=2 and descOne  in ('Tiering','Progressive')
                            order by [MasterValuePK]";
                        }

                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue
                            Where ID = @MasterValueID and status=2 and descOne  in ('Tiering','Progressive')
                            order by [MasterValuePK]";
                        }

                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne RiskCDDDesc,* from MasterValue A
                                                left join Mastervalue B on A.RiskCDD = B.Code and B.ID = 'RiskCDD' and B.Status = 2
                                                where A.status = @status order by A.MasterValuePK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne RiskCDDDesc,* from MasterValue A
                                                left join Mastervalue B on A.RiskCDD = B.Code and B.ID = 'RiskCDD' and B.Status = 2
                                                order by A.MasterValuePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int MasterValue_Add(MasterValue _masterValue, bool _havePrivillege)
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
                                 "Select isnull(max(MasterValuePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from MasterValue";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _masterValue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(MasterValuePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from MasterValue";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _masterValue.ID);
                        cmd.Parameters.AddWithValue("@Type", _masterValue.Type);
                        cmd.Parameters.AddWithValue("@Code", _masterValue.Code);
                        cmd.Parameters.AddWithValue("@DescOne", _masterValue.DescOne);
                        cmd.Parameters.AddWithValue("@DescTwo", _masterValue.DescTwo);
                        cmd.Parameters.AddWithValue("@Priority", _masterValue.Priority);
                        cmd.Parameters.AddWithValue("@IsHighRisk", _masterValue.IsHighRisk);
                        cmd.Parameters.AddWithValue("@RiskCDD", _masterValue.RiskCDD);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _masterValue.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MasterValue");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int MasterValue_Update(MasterValue _masterValue, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_masterValue.MasterValuePK, _masterValue.HistoryPK, "masterValue");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update MasterValue set status=2, Notes=@Notes,ID=@ID,Type=@Type,Code=@Code,DescOne=@DescOne," +
                                "DescTwo=@DescTwo,Priority=@Priority,IsHighRisk=@IsHighRisk,RiskCDD=@RiskCDD, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where MasterValuePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _masterValue.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                            cmd.Parameters.AddWithValue("@ID", _masterValue.ID);
                            cmd.Parameters.AddWithValue("@Notes", _masterValue.Notes);
                            cmd.Parameters.AddWithValue("@Type", _masterValue.Type);
                            cmd.Parameters.AddWithValue("@Code", _masterValue.Code);
                            cmd.Parameters.AddWithValue("@DescOne", _masterValue.DescOne);
                            cmd.Parameters.AddWithValue("@DescTwo", _masterValue.DescTwo);
                            cmd.Parameters.AddWithValue("@Priority", _masterValue.Priority);
                            cmd.Parameters.AddWithValue("@IsHighRisk", _masterValue.IsHighRisk);
                            cmd.Parameters.AddWithValue("@RiskCDD", _masterValue.RiskCDD);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _masterValue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _masterValue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MasterValue set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where MasterValuePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _masterValue.EntryUsersID);
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
                                cmd.CommandText = "Update MasterValue set Notes=@Notes,ID=@ID,Type=@Type,Code=@Code,DescOne=@DescOne," +
                                    "DescTwo=@DescTwo,Priority=@Priority,IsHighRisk=@IsHighRisk,RiskCDD=@RiskCDD, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where MasterValuePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _masterValue.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                                cmd.Parameters.AddWithValue("@ID", _masterValue.ID);
                                cmd.Parameters.AddWithValue("@Notes", _masterValue.Notes);
                                cmd.Parameters.AddWithValue("@Type", _masterValue.Type);
                                cmd.Parameters.AddWithValue("@Code", _masterValue.Code);
                                cmd.Parameters.AddWithValue("@DescOne", _masterValue.DescOne);
                                cmd.Parameters.AddWithValue("@DescTwo", _masterValue.DescTwo);
                                cmd.Parameters.AddWithValue("@Priority", _masterValue.Priority);
                                cmd.Parameters.AddWithValue("@IsHighRisk", _masterValue.IsHighRisk);
                                cmd.Parameters.AddWithValue("@RiskCDD", _masterValue.RiskCDD);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _masterValue.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_masterValue.MasterValuePK, "MasterValue");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MasterValue where MasterValuePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _masterValue.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _masterValue.ID);
                                cmd.Parameters.AddWithValue("@Type", _masterValue.Type);
                                cmd.Parameters.AddWithValue("@Code", _masterValue.Code);
                                cmd.Parameters.AddWithValue("@DescOne", _masterValue.DescOne);
                                cmd.Parameters.AddWithValue("@DescTwo", _masterValue.DescTwo);
                                cmd.Parameters.AddWithValue("@Priority", _masterValue.Priority);
                                cmd.Parameters.AddWithValue("@IsHighRisk", _masterValue.IsHighRisk);
                                cmd.Parameters.AddWithValue("@RiskCDD", _masterValue.RiskCDD);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _masterValue.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MasterValue set status= 4,Notes=@Notes,"+
                                     "lastupdate=@lastupdate where MasterValuePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _masterValue.Notes);
                                cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _masterValue.HistoryPK);
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

        public void MasterValue_Approved(MasterValue _masterValue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MasterValue set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where MasterValuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _masterValue.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _masterValue.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MasterValue set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where MasterValuePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _masterValue.ApprovedUsersID);
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

        public void MasterValue_Reject(MasterValue _masterValue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MasterValue set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where MasterValuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _masterValue.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _masterValue.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MasterValue set status= 2,lastupdate=@lastupdate where MasterValuePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
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

        public void MasterValue_Void(MasterValue _masterValue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MasterValue set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where MasterValuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _masterValue.MasterValuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _masterValue.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _masterValue.VoidUsersID);
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
        public List<MasterValue> MasterValue_SelectByMasterValueID(string _masterValueID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (_masterValueID == "Month")
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,'' RiskCDDDesc,* From MasterValue " +
                            "Where ID = @MasterValueID and status=2 order by Priority ";
                        }

                        else if (_masterValueID == "RiskCDD")
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DescOne RiskCDDDesc,* From MasterValue 
                            Where ID = 'RiskCDD' and status=2  order by Priority ";
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,'' RiskCDDDesc,* From MasterValue " +
                            "Where ID = @MasterValueID and status=2  order by Priority ";
                        }
              
                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_SelectByMasterValueIDByTab(string _masterValueID, string _statusTab)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = @MasterValueID and Status = 2 and Type = @statusTab order by [MasterValuePK]";
                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        cmd.Parameters.AddWithValue("@statusTab", _statusTab);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_TransactionType(string _masterValueID, int _instrumentTypePK)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (_instrumentTypePK == 5 )
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                                " Where ID = @MasterValueID and status=2 and descOne  in ('PLACEMENT','LIQUIDATE','ROLLOVER','OVERNIGHT') " +
                                " order by [priority]";
                        }
                        else if (_instrumentTypePK == 4)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                               "Where ID = @MasterValueID and status=2 and DescOne in ('SUBSCRIPTION','REDEMPTION') " +
                               " order by [priority]";
                        }
                        else if (_instrumentTypePK == 99) // khusus Deposito yang Bagian Liquidate
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                                    " Where ID = @MasterValueID and status=2 and descOne  in ('LIQUIDATE','ROLLOVER') " +
                                    " order by [priority]";
                        }else

                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                              "Where ID = @MasterValueID and status=2 and DescOne in ('BUY','SELL') " +
                              " order by [priority]";
                        }

                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<MasterValue> MasterValue_SelectByFundExposure(string _fundID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (_fundID.Equals("All",StringComparison.OrdinalIgnoreCase))
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = 'ExposureType' and status=2 and DescOne like '%ALL%' order by Priority ";
                        }

                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = 'ExposureType' and status=2 and DescOne not like '%ALL%' order by Priority ";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_SelectBySettlementMode(int _type)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (_type == 1)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = 'SettlementMode' and status=2 and DescOne in ('RVP','RFOP') order by Priority ";
                        }

                        else if (_type == 2)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = 'SettlementMode' and status=2 and DescOne in ('DVP','DFOP') order by DescOne ";
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue " +
                            "Where ID = 'SettlementMode' and status=2 and DescOne in ('RVP','RFOP','DVP','DFOP') order by DescOne ";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MasterValue MasterValue_SelectByMasterValueIDSourceofFund(string _masterValueID, int _code)
        {
            int Code = 0;
            var DescOne = "";
            var SourceofFund = "";
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_masterValueID == "Month")
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* From MasterValue " +
                            "Where ID = @MasterValueID and status=2 order by Priority ";
                        }

                        else
                        {
                            if (_masterValueID == "IncomeSourceIND")
                            {
                                SourceofFund = "IncomeSourceIND";
                            }
                            else
                            {
                                SourceofFund = "IncomeSourceINS";
                            }
                            cmd.CommandText = @"
                            create table #SourceFund
                            (
                            sourceoffund int,
                            typesource nvarchar(100)
                            )
                            insert into #SourceFund
                            Select case when clientcategory  = 1 then SumberDanaIND else sumberDanaInstitusi end SumberDana,case when clientcategory  = 1 then 'SumberDanaIND' else 'sumberDanaInstitusi' end SumberDanaType  from fundclient A 


                            Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.Code, B.DescOne from #SourceFund A left join MasterValue B
                            on A.sourceoffund = B.Code and B.ID = " + "'" + SourceofFund + "' where B.Code = @Code and B.status in(1,2)";
                        }

                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        cmd.Parameters.AddWithValue("@Code", _code);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                {
                                    dr.Read();
                                    Code = Convert.ToInt32(dr["Code"]);
                                    DescOne = Convert.ToString(dr["DescOne"]);
                                }
                            }
                        }
                    }
                    return new MasterValue()
                    {
                        Code = Convert.ToString(Code),
                        DescOne = Convert.ToString(DescOne)
                    };
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public List<MasterValueCombo> MasterValue_SelectByFundType(string _masterValueID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterValueCombo> L_MasterValue = new List<MasterValueCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select MasterValuePK,Code,DescOne From MasterValue 
                            Where ID = @MasterValueID and status=2 union all select 0,0,'All' order by MasterValuePK";
                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MasterValueCombo M_MasterValue = new MasterValueCombo();
                                    M_MasterValue.MasterValuePK = Convert.ToInt32(dr["MasterValuePK"]);
                                    M_MasterValue.Code = Convert.ToString(dr["Code"]);
                                    M_MasterValue.DescOne = Convert.ToString(dr["DescOne"]);
                                    L_MasterValue.Add(M_MasterValue);
                                }

                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<MasterValueCombo> MasterValue_ID()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterValueCombo> L_MasterValue = new List<MasterValueCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select DISTINCT ID From MasterValue 
                        Where status = 2 ";
                        //cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MasterValueCombo M_MasterValue = new MasterValueCombo();
                                    M_MasterValue.ID = Convert.ToString(dr["ID"]);
                                    L_MasterValue.Add(M_MasterValue);
                                }

                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public List<MasterValue> MasterValue_SelectByMasterValueIntDaysTypeDeposito(string _masterValueID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {

                        if (Tools.ClientCode == "03")
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue 
                            Where ID = @MasterValueID and status=2 and Code in (1,2,3,4) order by Priority ";
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* From MasterValue 
                            Where ID = @MasterValueID and status=2 and Code in (2,3,4) order by Priority ";
                        }


                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_SelectFundFeeTypeByCustodiFeeSetup(string _masterValueID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        if (Tools.ClientCode == "03") // Request Insight klo other fee bisa pakai flat
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* from MasterValue 
                            Where ID = @MasterValueID and status = 2 ";
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,''RiskCDDDesc,* from MasterValue 
                            Where ID = @MasterValueID and status = 2 and code != 1";
                        }


                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValue> MasterValue_SelectByMasterValueIDBankBranch(string _masterValueID)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    List<MasterValue> L_MasterValue = new List<MasterValue>();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {

                        cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,'' RiskCDDDesc,* From MasterValue 
                            Where ID = @MasterValueID and status=2 and Code in (2,3,4)  order by Priority ";


                        cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MasterValue.Add(setMasterValue(dr));
                                }
                            }
                            return L_MasterValue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MasterValueCombo> MasterValue_IDHighRisk()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterValueCombo> L_MasterValue = new List<MasterValueCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select DISTINCT ID From MasterValue 
                        Where ID in('HrPEP','HrBusiness','SDICountry','SDIProvince','Occupation','HRBusiness','FundType') and status = 2 ";
                        //cmd.Parameters.AddWithValue("@MasterValueID", _masterValueID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MasterValueCombo M_MasterValue = new MasterValueCombo();
                                    M_MasterValue.ID = Convert.ToString(dr["ID"]);
                                    L_MasterValue.Add(M_MasterValue);
                                }

                            }
                            return L_MasterValue;
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