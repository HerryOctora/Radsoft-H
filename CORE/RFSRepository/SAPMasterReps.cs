using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using SAP.Middleware.Connector;
using System.Data;
using RFSModel;
using RFSUtility;


namespace RFSRepository
{
    public class SAPMasterReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[ZSAP_BridgeJournal] " +
                            "([ZSAP_BridgeJournalPK],[HistoryPK],[Status],[FundJournalAccountPK],[SAPAccountID],";
        string _paramaterCommand = "@FundJournalAccountPK,@SAPAccountID,";

        private SAPMaster setSAPMSCustomer(SqlDataReader dr)
        {
            SAPMaster M_SAPMSCustomer = new SAPMaster();
            M_SAPMSCustomer.ID = dr["ID"].ToString();
            M_SAPMSCustomer.Name = dr["Name"].ToString();
            M_SAPMSCustomer.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SAPMSCustomer.EntryTime = dr["EntryTime"].ToString();
            M_SAPMSCustomer.LastUpdate = dr["LastUpdate"].ToString();
            M_SAPMSCustomer.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SAPMSCustomer;
        }

        public List<SAPMaster> SAPMSCustomer_Select()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SAPMaster> L_SAPMSCustomer = new List<SAPMaster>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select * from ZSAP_MS_CUSTOMER ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SAPMSCustomer.Add(setSAPMSCustomer(dr));
                                }
                            }
                            return L_SAPMSCustomer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        private SAPMaster setSAPMSAccount(SqlDataReader dr)
        {
            SAPMaster M_SAPMSAccount = new SAPMaster();
            M_SAPMSAccount.ID = dr["ID"].ToString();
            M_SAPMSAccount.Name = dr["Name"].ToString();
            M_SAPMSAccount.AccountType = dr["AccountType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountType"]);
            M_SAPMSAccount.AccountTypeID = dr["AccountTypeID"].ToString();
            M_SAPMSAccount.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SAPMSAccount.EntryTime = dr["EntryTime"].ToString();
            M_SAPMSAccount.LastUpdate = dr["LastUpdate"].ToString();
            M_SAPMSAccount.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SAPMSAccount;
        }

        public List<SAPMaster> SAPMSAccount_Select()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SAPMaster> L_SAPMSAccount = new List<SAPMaster>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select case when isnull(AccountType,0) = 1 then 'GL' 
                        else case when isnull(AccountType,0) = 2 then 'AR' else '' end end AccountTypeID,* 
                        from ZSAP_MS_ACCOUNT";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SAPMSAccount.Add(setSAPMSAccount(dr));
                                }
                            }
                            return L_SAPMSAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<SAPMSCustomerCombo> SAPMS_GetCustomerCombo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SAPMSCustomerCombo> L_Customer = new List<SAPMSCustomerCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ID, ID + ' - ' + Name Name FROM [ZSAP_MS_CUSTOMER] order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SAPMSCustomerCombo M_Customer = new SAPMSCustomerCombo();
                                    M_Customer.ID = Convert.ToString(dr["ID"]);
                                    M_Customer.Name = Convert.ToString(dr["Name"]);
                                    L_Customer.Add(M_Customer);
                                }
                            }
                            return L_Customer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<SAPMSAccountCombo> SAPMS_GetDataAccountCombo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SAPMSAccountCombo> L_Account = new List<SAPMSAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ID, ID + ' - ' + Name Name FROM [ZSAP_MS_ACCOUNT] order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SAPMSAccountCombo M_Account = new SAPMSAccountCombo();
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        //SAP Bridge
        private SAPBridgeJournal setSAPBridgeJournal(SqlDataReader dr)
        {
            SAPBridgeJournal M_SAPBridgeJournal = new SAPBridgeJournal();
            M_SAPBridgeJournal.ZSAP_BridgeJournalPK = Convert.ToInt32(dr["ZSAP_BridgeJournalPK"]);
            M_SAPBridgeJournal.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SAPBridgeJournal.Status = Convert.ToInt32(dr["Status"]);
            M_SAPBridgeJournal.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SAPBridgeJournal.Notes = Convert.ToString(dr["Notes"]);
            M_SAPBridgeJournal.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_SAPBridgeJournal.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_SAPBridgeJournal.SAPAccountID = dr["SAPAccountID"].ToString();
            M_SAPBridgeJournal.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SAPBridgeJournal.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SAPBridgeJournal.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SAPBridgeJournal.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SAPBridgeJournal.EntryTime = dr["EntryTime"].ToString();
            M_SAPBridgeJournal.UpdateTime = dr["UpdateTime"].ToString();
            M_SAPBridgeJournal.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SAPBridgeJournal.VoidTime = dr["VoidTime"].ToString();
            M_SAPBridgeJournal.DBUserID = dr["DBUserID"].ToString();
            M_SAPBridgeJournal.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SAPBridgeJournal.LastUpdate = dr["LastUpdate"].ToString();
            M_SAPBridgeJournal.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SAPBridgeJournal;
        }

        public List<SAPBridgeJournal> SAPBridgeJournal_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SAPBridgeJournal> L_SAPBridgeJournal = new List<SAPBridgeJournal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID + ' - ' + B.Name FundJournalAccountID,* from ZSAP_BridgeJournal  A
                            Left Join FundJournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status in(1,2)
                            Left Join ZSAP_MS_ACCOUNT C on A.SAPAccountID = C.ID where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID + ' - ' + B.Name FundJournalAccountID,* from ZSAP_BridgeJournal  A
                            Left Join FundJournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status in(1,2)
                            Left Join ZSAP_MS_ACCOUNT C on A.SAPAccountID = C.ID order by A.ZSAP_BridgeJournalPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SAPBridgeJournal.Add(setSAPBridgeJournal(dr));
                                }
                            }
                            return L_SAPBridgeJournal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SAPBridgeJournal_Add(SAPBridgeJournal _SAPBridgeJournal, bool _havePrivillege)
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
                                 "Select isnull(max(ZSAP_BridgeJournalPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From ZSAP_BridgeJournal";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SAPBridgeJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(ZSAP_BridgeJournalPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From ZSAP_BridgeJournal";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _SAPBridgeJournal.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@SAPAccountID", _SAPBridgeJournal.SAPAccountID);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SAPBridgeJournal.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "ZSAP_BridgeJournal");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int SAPBridgeJournal_Update(SAPBridgeJournal _SAPBridgeJournal, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_SAPBridgeJournal.ZSAP_BridgeJournalPK, _SAPBridgeJournal.HistoryPK, "SAPBridgeJournal");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ZSAP_BridgeJournal set status=2, Notes=@Notes,FundJournalAccountPK=@FundJournalAccountPK,SAPAccountID=@SAPAccountID," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where ZSAP_BridgeJournalPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SAPBridgeJournal.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                            cmd.Parameters.AddWithValue("@Notes", _SAPBridgeJournal.Notes);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _SAPBridgeJournal.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@SAPAccountID", _SAPBridgeJournal.SAPAccountID);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SAPBridgeJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SAPBridgeJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ZSAP_BridgeJournal set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where ZSAP_BridgeJournalPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SAPBridgeJournal.EntryUsersID);
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
                                cmd.CommandText = "Update ZSAP_BridgeJournal set  Notes=@Notes,FundJournalAccountPK=@FundJournalAccountPK,SAPAccountID=@SAPAccountID," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where ZSAP_BridgeJournalPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SAPBridgeJournal.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                                cmd.Parameters.AddWithValue("@Notes", _SAPBridgeJournal.Notes);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _SAPBridgeJournal.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@SAPAccountID", _SAPBridgeJournal.SAPAccountID);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SAPBridgeJournal.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SAPBridgeJournal.ZSAP_BridgeJournalPK, "SAPBridgeJournal");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ZSAP_BridgeJournal where ZSAP_BridgeJournalPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SAPBridgeJournal.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _SAPBridgeJournal.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@SAPAccountID", _SAPBridgeJournal.SAPAccountID);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SAPBridgeJournal.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ZSAP_BridgeJournal set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where ZSAP_BridgeJournalPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SAPBridgeJournal.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SAPBridgeJournal.HistoryPK);
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

        public void SAPBridgeJournal_Approved(SAPBridgeJournal _SAPBridgeJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ZSAP_BridgeJournal set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where ZSAP_BridgeJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SAPBridgeJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SAPBridgeJournal.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ZSAP_BridgeJournal set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where ZSAP_BridgeJournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SAPBridgeJournal.ApprovedUsersID);
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

        public void SAPBridgeJournal_Reject(SAPBridgeJournal _SAPBridgeJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ZSAP_BridgeJournal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where ZSAP_BridgeJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SAPBridgeJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SAPBridgeJournal.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ZSAP_BridgeJournal set status= 2,lastupdate=@lastupdate where ZSAP_BridgeJournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
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

        public void SAPBridgeJournal_Void(SAPBridgeJournal _SAPBridgeJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ZSAP_BridgeJournal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where ZSAP_BridgeJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SAPBridgeJournal.ZSAP_BridgeJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SAPBridgeJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SAPBridgeJournal.VoidUsersID);
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

        public string GetAccountFromSAP(string _usersID)
        {
            SAPConnectionConfig SAPCon = new SAPConnectionConfig();


            RfcDestinationManager.RegisterDestinationConfiguration(SAPCon);


            RfcDestination dest = RfcDestinationManager.GetDestination(Tools._sapServer);

            RfcRepository repo = dest.Repository;

            IRfcFunction testfn = repo.CreateFunction("ZFM_GET_GLMASTER");

            testfn.Invoke(dest);

            var AccountList = testfn.GetTable("T_GLMASTER");

            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "ID";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Name";
                dc.Unique = false;
                dt.Columns.Add(dc);

                for (int i = 0; i < AccountList.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["ID"] = AccountList[i].GetValue("SAKNR");
                    dr["Name"] = AccountList[i].GetValue("TXT50");
                    dt.Rows.Add(dr);
                }
                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table ZSAP_ACCOUNT_TEMP";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {

                    bulkCopy.DestinationTableName = "dbo.ZSAP_ACCOUNT_TEMP";
                    bulkCopy.WriteToServer(dt);
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    var _msg = "";
                    DateTime _now = DateTime.Now;
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText = @"
                                          INSERT INTO ZSAP_MS_ACCOUNT	
( 
	ID,
	Name,
	EntryUsersID,EntryTime,LastUpdate,LastUpdateDB
) 
Select SUBSTRING(ID, 3, 10), Name,  @UsersID, @TimeNow, @TimeNow, @TimeNow From ZSAP_ACCOUNT_TEMP
WHERE SUBSTRING(ID, 3, 10) NOT IN
(
SELECT ID FROM dbo.ZSAP_MS_Account
)


DELETE dbo.ZSAP_MS_Account
WHERE ID NOT IN
(
SELECT SUBSTRING(ID, 3, 10) FROM dbo.ZSAP_ACCOUNT_TEMP
)

                                            select 'true','Success Get Data From SAP' ResultDesc
                                            ";

                        cmd1.Parameters.AddWithValue("@TimeNow", _now);
                        cmd1.Parameters.AddWithValue("@UsersID", _usersID);
                        using (SqlDataReader dr01 = cmd1.ExecuteReader())
                        {
                            if (dr01.HasRows)
                            {
                                while (dr01.Read())
                                {
                                    _msg = Convert.ToString(dr01["ResultDesc"]);

                                }

                            }
                        }
                        RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                        return _msg;
                    }
                }
            }
        }

        public string Validate_SAPMSAccount()
        {
            try
            {
                string _msg = "";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                       if Exists
(select * from ZSAP_BridgeJournal 
where SAPAccountID not in(select ID From ZSAP_MS_ACCOUNT ))    
BEGIN 
    declare @Message nvarchar(max)
                    set @Message = ' '
                    select distinct @Message = @Message +' , ' + SAPAccountID  + ' This data Has no Data Master'  from ZSAP_BridgeJournal 
	                    where SAPAccountID not in
		                    (
			                    select ID From ZSAP_MS_ACCOUNT 
		                    ) AND Status IN (1,2)

	                    Select top 1  'false' result,@Message ResultDesc from ZSAP_BridgeJournal 
	                    where SAPAccountID not in
		                    (
			                    select ID From ZSAP_MS_ACCOUNT 
		                    )
END 
ELSE 
BEGIN     
Select 'True' Result , '' ResultDesc
END 

   ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["ResultDesc"]);

                            }
                            return _msg;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string GetBusinessAreaFromSAP(string _usersID)
        {
            SAPConnectionConfig SAPCon = new SAPConnectionConfig();


            RfcDestinationManager.RegisterDestinationConfiguration(SAPCon);

            RfcDestination dest = RfcDestinationManager.GetDestination(Tools._sapServer);

            RfcRepository repo = dest.Repository;

            IRfcFunction testfn = repo.CreateFunction("ZFM_GET_BUSINESS_AREA");

            testfn.Invoke(dest);

            var AccountList = testfn.GetTable("IT_BUSINESS_AREA");

            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "ID";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Name";
                dc.Unique = false;
                dt.Columns.Add(dc);

                for (int i = 0; i < AccountList.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["ID"] = AccountList[i].GetValue("GSBER");
                    dr["Name"] = AccountList[i].GetValue("GTEXT");
                    dt.Rows.Add(dr);
                }
                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table ZSAP_BUSINESSAREA";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {

                    bulkCopy.DestinationTableName = "dbo.ZSAP_BUSINESSAREA";
                    bulkCopy.WriteToServer(dt);
                }

                //Console.WriteLine("Total Data GL: " + count.ToString());

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    //List<ReturnAccount> _s = new List<ReturnAccount>();
                    var _msg = "";
                    DateTime _now = DateTime.Now;
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText = @"
                                            if Exists( Select * from ZSAP_BUSINESSAREA 
                                            where SUBSTRING(ID, 3, 10) in 
                                            (
	                                            select ID from SAPMSBusinessArea 
                                            ))
                                            BEGIN

                                            declare @Message nvarchar(max)
                                            set @Message = ' '
                                            select distinct @Message = @Message +' , ' + ID  from ZSAP_BUSINESSAREA 
	                                            where SUBSTRING(ID, 3, 10) in
		                                            (
			                                            select ID from SAPMSBusinessArea 
		                                            )

	                                            Select top 1  'false' result,@Message ResultDesc from ZSAP_BUSINESSAREA 
	                                            where SUBSTRING(ID, 3, 10) in
		                                            (
			                                            select ID from SAPMSBusinessArea 
		                                            )
                                            END
                                            ELSE
                                            BEGIN
	                                            INSERT INTO SAPMSBusinessArea	
                                            ( 
											  ID,
											  Name,
											  EntryUsersID,EntryTime,LastUpdate
                                            ) 
                                            Select SUBSTRING(ID, 3, 10), Name, 'admin', GETDATE(), GETDATE() Name From ZSAP_BUSINESSAREA

                                            select 'Success Get Data From SAP' ResultDesc
                                            END
                                            ";

                        cmd1.Parameters.AddWithValue("@TimeNow", _now);
                        cmd1.Parameters.AddWithValue("@UsersID", _usersID);
                        using (SqlDataReader dr01 = cmd1.ExecuteReader())
                        {
                            if (dr01.HasRows)
                            {
                                while (dr01.Read())
                                {
                                    if (Convert.ToString(dr01["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr01["ResultDesc"]);

                                    }

                                }

                            }
                        }
                        return _msg;
                    }
                }
            }
        }

        public string GetCustomerFromSAP(string _usersID)
        {
            SAPConnectionConfig SAPCon = new SAPConnectionConfig();


            RfcDestinationManager.RegisterDestinationConfiguration(SAPCon);

            RfcDestination dest = RfcDestinationManager.GetDestination(Tools._sapServer);

            RfcRepository repo = dest.Repository;

            IRfcFunction testfn = repo.CreateFunction("ZFM_GET_CUST_MASTER");

            testfn.Invoke(dest);

            var AccountList = testfn.GetTable("T_CUSTOMER");

            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "ID";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Name";
                dc.Unique = false;
                dt.Columns.Add(dc);

                for (int i = 0; i < AccountList.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["ID"] = AccountList[i].GetValue("KUNNR");
                    dr["Name"] = AccountList[i].GetValue("NAME1");
                    dt.Rows.Add(dr);
                }
                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table ZSAP_CUSTOMER_TEMP";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {

                    bulkCopy.DestinationTableName = "dbo.ZSAP_CUSTOMER_TEMP";
                    bulkCopy.WriteToServer(dt);
                }

                //Console.WriteLine("Total Data GL: " + count.ToString());

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    //List<ReturnAccount> _s = new List<ReturnAccount>();
                    var _msg = "";
                    DateTime _now = DateTime.Now;
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText = @"
                                           
INSERT INTO ZSAP_MS_CUSTOMER	
( 
	ID,
	Name,
	EntryUsersID,EntryTime,LastUpdate,LastUpdateDB
) 
Select ID, Name, @UsersID, @TimeNow, @TimeNow, @TimeNow  From ZSAP_CUSTOMER_TEMP
WHERE ID NOT IN
(
SELECT ID FROM dbo.ZSAP_MS_CUSTOMER
)


DELETE dbo.ZSAP_MS_CUSTOMER
WHERE ID NOT IN
(
SELECT ID FROM dbo.ZSAP_CUSTOMER_TEMP
)


                                            select 'Success Get Data From SAP' ResultDesc
                                            ";

                        cmd1.Parameters.AddWithValue("@TimeNow", _now);
                        cmd1.Parameters.AddWithValue("@UsersID", _usersID);
                        using (SqlDataReader dr01 = cmd1.ExecuteReader())
                        {
                            if (dr01.HasRows)
                            {
                                while (dr01.Read())
                                {
                                    _msg = Convert.ToString(dr01["ResultDesc"]);

                                }

                            }
                        }
                        RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                        return _msg;
                    }
                }
            }
        }

        public string Validate_SAPMSCustomer()
        {
            try
            {
                string _msg = "";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                    IF Exists
(select SAPCustID from dbo.Instrument 
where ID not in(SELECT ID From ZSAP_MS_Customer) AND status IN (1,2) AND SAPcustID <> '0' AND LEN(ISNULL(SAPCustID,'')) > 2 )    
BEGIN 
    declare @Message nvarchar(max)
set @Message = ' '
select distinct @Message = @Message +' , ' + SAPCustID  + ' This data has no Data Master' from Instrument 
	where SAPCustID not in
		(
			select  ID From ZSAP_MS_Customer
		) and status IN (1,2) AND SAPcustID <> '0' AND LEN(ISNULL(SAPCustID,'')) > 2

	Select top 1  'false' result,@Message ResultDesc from Instrument 
	where SAPCustID not in
		(
			select  ID From ZSAP_MS_Customer
		)and status IN (1,2) AND SAPcustID <> '0' AND LEN(ISNULL(SAPCustID,'')) > 2
END 
ELSE 
BEGIN     
select 'true' Result,'' ResultDesc
END";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["ResultDesc"]);

                            }
                            return _msg;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private InterfaceJournalSAP setInterfaceJournalSAP(SqlDataReader dr)
        {
            InterfaceJournalSAP M_InterfaceJournalSAP = new InterfaceJournalSAP();
            M_InterfaceJournalSAP.FundID = Convert.ToString(dr["FundID"]);
            M_InterfaceJournalSAP.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
            M_InterfaceJournalSAP.InvestmentPK = Convert.ToInt32(dr["InvestmentPK"]);
            M_InterfaceJournalSAP.Selected = Convert.ToBoolean(dr["Selected"]);
            M_InterfaceJournalSAP.Date = Convert.ToString(dr["ValueDate"]);
            M_InterfaceJournalSAP.FundJournalDate = Convert.ToString(dr["FundJournalDate"]);
            M_InterfaceJournalSAP.BuySell = Convert.ToString(dr["TrxTypeID"]);
            M_InterfaceJournalSAP.FundJournalType = Convert.ToString(dr["FundJournalType"]);
            M_InterfaceJournalSAP.Description = Convert.ToString(dr["Description"]);
            M_InterfaceJournalSAP.JournalReference = Convert.ToString(dr["JournalRef"]);
            M_InterfaceJournalSAP.DocFrom = Convert.ToString(dr["DocSAP"]);
            return M_InterfaceJournalSAP;
        }

        public List<InterfaceJournalSAP> InterfaceJournalSAP_Select(DateTime _date, string _fundID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InterfaceJournalSAP> L_InterfaceJournalSAP = new List<InterfaceJournalSAP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        if (_fundID != "0")
                        {
                            _paramFund = "And A.FundPK  = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandText = @"select D.ID FundID,A.SelectedSAP selected,B.FundJournalPK,A.InvestmentPK,A.ValueDate,B.ValueDate FundJournalDate,A.TrxTypeID,
                        B.Description,B.Reference JournalRef, DocSAP,B.TrxName FundJournalType from Investment A
                        left join FundJournal B on A.Reference = B.Reference and B.status = 2
                        left join Instrument C on A.instrumentPK = C.InstrumentPK and C.Status in (1,2)
                        left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
                        where B.Type = 5 and B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and A.ValueDate = @Date and C.InstrumentTypePK not in (6) " + _paramFund + @"
                        union all
                        select D.ID FundID,A.SelectedSAP selected,B.FundJournalPK,A.InvestmentPK,A.ValueDate,B.ValueDate FundJournalDate,A.TrxTypeID,
                        B.Description,B.Reference JournalRef, DocSAP,B.TrxName FundJournalType from Investment A
                        left join FundJournal B on A.Reference = B.Reference and B.status = 2
                        left join Instrument C on A.instrumentPK = C.InstrumentPK and C.Status in (1,2)
                        left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
                        where B.Type = 5 and B.Posted = 1 and B.Reversed = 0 and A.StatusInvestment = 2 and A.ValueDate = @Date and C.InstrumentTypePK in (6)" + _paramFund;
                        
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InterfaceJournalSAP.Add(setInterfaceJournalSAP(dr));
                                }
                            }
                            return L_InterfaceJournalSAP;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private InterfaceJournalSAP setDetailInterfaceJournalSAP(SqlDataReader dr)
        {
            InterfaceJournalSAP M_DetailInterfaceJournalSAP = new InterfaceJournalSAP();
            M_DetailInterfaceJournalSAP.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
            M_DetailInterfaceJournalSAP.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_DetailInterfaceJournalSAP.AccountID = dr["AccountID"].ToString();
            M_DetailInterfaceJournalSAP.AccountName = dr["AccountName"].ToString();
            M_DetailInterfaceJournalSAP.DetailDescription = dr["DetailDescription"].ToString();
            M_DetailInterfaceJournalSAP.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            M_DetailInterfaceJournalSAP.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            return M_DetailInterfaceJournalSAP;
        }

        public List<InterfaceJournalSAP> DetailInterfaceJournalSAP_Select(int _pk, string _fundJournalType, int _fundPK, DateTime _date, string _description)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InterfaceJournalSAP> L_InterfaceJournalSAP = new List<InterfaceJournalSAP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundJournalType == "TRANSACTION")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            select A.FundJournalPK,A.AutoNo,C.ID AccountID,C.Name AccountName,A.DetailDescription,A.BaseDebit,A.BaseCredit from FundJournalDetail A  
                            left join ZSAP_BridgeJournal B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status in (1,2)
                            left join ZSAP_MS_ACCOUNT C on B.SAPAccountID = C.ID
                            where FundJournalPK = @PK";

                        }
                        else if (_fundJournalType == "ADJUSTMENT")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            select A.FundJournalPK,A.AutoNo,C.ID AccountID,C.Name AccountName,A.DetailDescription,A.BaseDebit,A.BaseCredit from FundJournalDetail A  
                            left join ZSAP_BridgeJournal B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.status in (1,2)
                            left join ZSAP_MS_ACCOUNT C on B.SAPAccountID = C.ID
                            where FundJournalPK = @PK";

                        }
                        else if (_fundJournalType == "REC COUPON")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            declare @DatelastMonth datetime
                            declare @InstrumentPK int
                            declare @AcqDate datetime
                            declare @EndDate datetime

                            select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @PK and Date = dbo.FWorkingDay(@Date,-1) and status = 2

                            select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@Date)) then eomonth(@Date) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@Date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@Date) as nvarchar(4)) as nvarchar(10)),101) end

                            select @DatelastMonth = case when @DatelastMonth >= @Date then DATEADD(month, -1, @DatelastMonth) else @DatelastMonth end
                            select @DatelastMonth = dateadd(day,1,@DatelastMonth)
                            select @EndDate = @Date


                            select 0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY AccountID ASC) AutoNo,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @PK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 
                            )D";


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }

                        else if (_fundJournalType == "REC COUPON EBA")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            declare @DatelastMonth datetime
                            declare @InstrumentPK int
                            declare @AcqDate datetime
                            declare @EndDate datetime

                            select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @PK and Date = dbo.FWorkingDay(@Date,-1) and status = 2

                            select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@Date)) then eomonth(@Date) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@Date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@Date) as nvarchar(4)) as nvarchar(10)),101) end

                            select @DatelastMonth = case when @DatelastMonth >= @Date then DATEADD(month, -1, @DatelastMonth) else @DatelastMonth end
                            select @DatelastMonth = dateadd(day,1,@DatelastMonth)
                            select @EndDate = @Date


                            select 0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY AccountID ASC) AutoNo,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = 'REC COUPON' and A.FundPK = @FundPK and A.InstrumentPK = @PK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 
                            )D";


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }

                        else if (_fundJournalType == "REC COUPON DEPOSITO")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                                             
                            declare @InterestTemp Table 
                            (
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            FundPK int,
                            BankPK int,
                            BaseDebit numeric(18,0),
                            BaseCredit numeric(18,0)

                            )

                            declare @DatelastMonth datetime
                            declare @InstrumentPK int
                            declare @AcqDate datetime
                            declare @EndDate datetime


                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date < @Date and FundPK = @FundPK
                            )
                            and C.InstrumentTypePK in (5) and A.status = 2 and C.BankPK = @PK

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

                            select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2

                            select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@Date)) then eomonth(@Date) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@Date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@Date) as nvarchar(4)) as nvarchar(10)),101) end

                            select @DatelastMonth = case when @DatelastMonth >= @Date then DATEADD(month, -1, @DatelastMonth) else @DatelastMonth end
                            select @DatelastMonth = dateadd(day,1,@DatelastMonth)
                            select @EndDate = @Date

                            insert into @InterestTemp
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = 'REC COUPON' and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 



                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;

                            select 0 FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription,BaseCredit ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @InterestTemp";


                            //cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }




                        else if (_fundJournalType == "AMORTIZE")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
  
                            declare @Amortize Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,4),
                            BaseCredit numeric(18,4)

                            )

                            declare @InstrumentPK int

                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and 
                            C.InstrumentTypePK not in (1,4,5,6,16)

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

                            insert into @Amortize
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate between DATEADD(month, DATEDIFF(month, 0, @Date), 0) and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @Amortize

                        ";

                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }

                        else if (_fundJournalType == "INTEREST BOND" || _fundJournalType == "INTEREST FUND" || _fundJournalType == "INTEREST EBA")
                        {
                            string _paramDescription = "";
                            if (_description == "PIUTANG BOND")
                            {
                                _paramDescription = " C.InstrumentTypePK not in (1,4,5,6,8,16)";
                            }
                            else if (_description == "PIUTANG REKSADANA")
                            {
                                _paramDescription = " C.InstrumentTypePK in (6)";
                            }
                            else if (_description == "PIUTANG EBA")
                            {
                                _paramDescription = " C.InstrumentTypePK in (8)";
                            }

                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            declare @Interest Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,4),
                            BaseCredit numeric(18,4)

                            )

                            declare @DatelastMonth datetime
                            declare @InstrumentPK int
                            declare @AcqDate datetime
                            declare @EndDate datetime



                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and 
                            " + _paramDescription + @"

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN




                            select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2



                            select @DatelastMonth = DATEADD(month, DATEDIFF(month, 0, @Date), 0)

                            select @EndDate = @Date

                            insert into @Interest
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate between @DatelastMonth and @EndDate and TrxName = case when @FundJournalType = 'Interest EBA' then 'Interest Bond' else @FundJournalType end and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @Interest";


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }

                        else if (_fundJournalType == "INTEREST DEPOSIT")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            declare @Interest Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,4),
                            BaseCredit numeric(18,4)

                            )

                            declare @InterestTemp Table 
                            (
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            FundPK int,
                            BankPK int,
                            BaseDebit numeric(18,0),
                            BaseCredit numeric(18,0)

                            )

                            declare @DatelastMonth datetime
                            declare @InstrumentPK int
                            declare @AcqDate datetime
                            declare @EndDate datetime
                            declare @ValueDateBeg datetime
                            declare @DatePreviousMonth datetime


                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            )
                            and C.InstrumentTypePK in (5) and A.status = 2

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN




                            select @ValueDateBeg = eomonth(DATEADD(month, -1, @Date))

                            select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2


                            if (datepart(day,@AcqDate) = 31)
                            BEGIN
                            select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@date) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@date) as nvarchar(2)) + '/30/' + cast(datepart(year,@date) as nvarchar(4)) as nvarchar(10)),101) end

                            END
                            ELSE
                            BEGIN
                            select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@date) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@date) as nvarchar(4)) as nvarchar(10)),101) end

                            END




                            select @DatePreviousMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@ValueDateBeg) else 
                            CONVERT(DATETIME,CAST(cast(datepart(month,@ValueDateBeg) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@ValueDateBeg) as nvarchar(4)) as nvarchar(10)),101) end

                            select @DatelastMonth = case when @date < @DatelastMonth then @DatePreviousMonth else @DatelastMonth end
                            select @DatelastMonth = dateadd(day,1,@DatelastMonth)

                            insert into @InterestTemp
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.BankPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate between @DatelastMonth and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.BankPK 

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;

                            insert into @Interest
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select AccountID,AccountName,DetailDescription,sum(BaseDebit) BaseDebit,sum(BaseCredit) BaseCredit from @InterestTemp
                            group by AccountID,AccountName,DetailDescription
                            )D

                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription,BaseCredit ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @Interest

                            ";


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }


                        else if (_fundJournalType == "PORTFOLIO REVALUATION")
                        {

                            string _paramDescription = "";

                            if (_description == "PORTFOLIO REVALUATION EQUITY")
                            {
                                _paramDescription = @"
                            declare @Reval Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,0),
                            BaseCredit numeric(18,0)

                            )

                            declare @InstrumentPK int

                            declare @lastPreviousMonth datetime
                            select @lastPreviousMonth = max(ValueDate) from EndDayTrailsFundPortfolio where status = 2
                            and ValueDate <= dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))
                            



                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and  C.InstrumentTypePK in (1,4,16)

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

                            

                            insert into @Reval
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,A.FundPK,C.InstrumentPK,
                            case when MarketValue >= 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue < 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
                            case when MarketValue < 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseCredit     
                            from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
                            left join 
                            (
	                            select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
	                            select InstrumentPK,(MarketValue - CostValue) MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @Date and FundPK = @FundPK
	                            union all
	                            select InstrumentPK,(MarketValue - CostValue)  * -1 MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @lastPreviousMonth and FundPK = @FundPK
	                            ) G group by InstrumentPK,FundPK
                            ) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,A.FundPK,C.InstrumentPK,D.Type,H.MarketValue 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @Reval
                            ";
                            }

                            else if (_description == "PORTFOLIO REVALUATION BOND")
                            {
                                _paramDescription =
                                    @"

                            declare @Reval Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,0),
                            BaseCredit numeric(18,0)

                            )

                            declare @InstrumentPK int
                            declare @AcqDate datetime

                            declare @lastPreviousMonth datetime
                            select @lastPreviousMonth = max(ValueDate) from EndDayTrailsFundPortfolio where status = 2
                            and ValueDate <= dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))

                            declare @lastMonth datetime
                            select @lastMonth = dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))





                            Declare A Cursor For

                            select distinct A.InstrumentPK,A.AcqDate from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and  C.InstrumentTypePK not in (1,4,5,6,8,16)

                            Open A
                            Fetch next From A
                            Into @InstrumentPK,@AcqDate
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

							IF NOT EXISTS(select * from fundposition where FundPK = @FundPK and InstrumentPk = @InstrumentPK and AcqDate = @AcQdate
							and Date = @lastPreviousMonth)
							BEGIN
							select @lastPreviousMonth = @AcqDate
							END
                            

                            insert into @Reval
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,A.FundPK,C.InstrumentPK,
                            case when MarketValue >= 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue < 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
                            case when MarketValue < 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseCredit     
                            from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
                            left join 
                            (
	                            select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
	                            select A.InstrumentPK,
	                            CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
	                            (
	                            @date,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
	                            A.Balance,A.CostValue
	                            ),0)))) ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@Date) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1))))  END MarketValue,
	                            FundPK from FundPosition A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
	                            where A.InstrumentPK = @InstrumentPK and A.status = 2  and FundPK = @FundPK and A.AcqDate = @AcqDate and A.BitHTM <> 1
                                and Date  = (
                                select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                                ) and A.status = 2
	                            union all
	                            select A.InstrumentPK,
	                            CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
	                            (
	                            @lastMonth,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
	                            A.Balance,A.CostValue
	                            ),0)))) * -1  ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@lastMonth) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1)))) * - 1  END MarketValue,
	                            FundPK from FundPosition A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
	                            where A.InstrumentPK = @InstrumentPK and A.status = 2 and date = @lastPreviousMonth and FundPK = @FundPK  and A.AcqDate = @AcqDate  and A.BitHTM <> 1
	                            ) G group by InstrumentPK,FundPK
                            ) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,A.FundPK,C.InstrumentPK,D.Type,H.MarketValue 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK,@AcqDate
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,sum(BaseDebit) BaseDebit,sum(BaseCredit) BaseCredit from  @Reval
                            group by FundJournalPk,AccountID,AccountName,
                            DetailDescription


                            ";

                            }
                            else if (_description == "PORTFOLIO REVALUATION EBA")
                            {
                                _paramDescription =
                                    @"

                            declare @Reval Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,0),
                            BaseCredit numeric(18,0)

                            )

                            declare @InstrumentPK int
                            declare @AcqDate datetime

                            declare @lastPreviousMonth datetime
                            select @lastPreviousMonth = max(ValueDate) from EndDayTrailsFundPortfolio where status = 2
                            and ValueDate <= dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))

                            declare @lastMonth datetime
                            select @lastMonth = dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))





                            Declare A Cursor For

                            select distinct A.InstrumentPK,A.AcqDate from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where   Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and  C.InstrumentTypePK in (8)

                            Open A
                            Fetch next From A
                            Into @InstrumentPK,@AcqDate
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

							IF NOT EXISTS(select * from fundposition where FundPK = @FundPK and InstrumentPk = @InstrumentPK and AcqDate = @AcQdate
							and Date = @lastPreviousMonth)
							BEGIN
							select @lastPreviousMonth = @AcqDate
							END
                            

                            insert into @Reval
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,A.FundPK,C.InstrumentPK,
                            case when MarketValue >= 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue < 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
                            case when MarketValue < 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseCredit     
                            from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
                            left join 
                            (
	                            select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
	                            select A.InstrumentPK,
	                            CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
	                            (
	                            @date,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
	                            A.Balance,A.CostValue
	                            ),0)))) ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@Date) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1))))  END MarketValue,
	                            FundPK from FundPosition A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
	                            where A.InstrumentPK = @InstrumentPK and A.status = 2  and FundPK = @FundPK and A.AcqDate = @AcqDate and A.BitHTM <> 1
                                and Date  = (
                                select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                                ) and A.status = 2
	                            union all
	                            select A.InstrumentPK,
	                            CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
	                            (
	                            @lastMonth,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
	                            A.Balance,A.CostValue
	                            ),0)))) * -1  ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@lastMonth) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1)))) * - 1  END MarketValue,
	                            FundPK from FundPosition A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
	                            where A.InstrumentPK = @InstrumentPK and A.status = 2 and date = @lastPreviousMonth and FundPK = @FundPK  and A.AcqDate = @AcqDate  and A.BitHTM <> 1
	                            ) G group by InstrumentPK,FundPK
                            ) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,A.FundPK,C.InstrumentPK,D.Type,H.MarketValue 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK,@AcqDate
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,sum(BaseDebit) BaseDebit,sum(BaseCredit) BaseCredit from  @Reval
                            group by FundJournalPk,AccountID,AccountName,
                            DetailDescription


                            ";

                            }
                            else if (_description == "PORTFOLIO REVALUATION REKSADANA")
                            {
                                _paramDescription =
                                    @"

                            declare @Reval Table 
                            (
                            FundJournalPK int,
                            AccountID nvarchar(50),
                            AccountName nvarchar(100),
                            DetailDescription nvarchar(200),
                            BaseDebit numeric(18,4),
                            BaseCredit numeric(18,4)

                            )

                            declare @InstrumentPK int

                            declare @lastPreviousMonth datetime
                            select @lastPreviousMonth = max(ValueDate) from EndDayTrailsFundPortfolio where status = 2
                            and ValueDate <= dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))
                            



                            Declare A Cursor For

                            select distinct A.InstrumentPK from FundPosition A
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where  Date  = (
                            select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and A.status = 2
                            and  C.InstrumentTypePK in (6)

                            Open A
                            Fetch next From A
                            Into @InstrumentPK
                            WHILE @@FETCH_STATUS = 0  
                            BEGIN

                            

                            insert into @Reval
                            select 0 FundJournalPK,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,A.FundPK,C.InstrumentPK,
                            case when MarketValue >= 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue < 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
                            case when MarketValue < 0 and D.Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and D.Type <> 1 then abs(MarketValue) else 0 end end BaseCredit     
                            from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
                            left join 
                            (
	                            select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
	                            select InstrumentPK,(MarketValue - CostValue) MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @Date and FundPK = @FundPK
	                            union all
	                            select InstrumentPK,(MarketValue - CostValue) * -1 MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @lastPreviousMonth and FundPK = @FundPK
	                            ) G group by InstrumentPK,FundPK
                            ) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                            group by F.ID,F.Name,DetailDescription,A.FundPK,C.InstrumentPK,D.Type,H.MarketValue 
                            )D

                            FETCH NEXT FROM A 
                            INTO @InstrumentPK
                            END 

                            CLOSE A;  
                            DEALLOCATE A;


                            select FundJournalPk,ROW_NUMBER() OVER(ORDER BY DetailDescription ASC) AutoNo,AccountID,AccountName,
                            DetailDescription,BaseDebit,BaseCredit from  @Reval

                            ";


                            }



                            cmd.CommandTimeout = 0;
                            cmd.CommandText = _paramDescription;


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }

                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            declare @StartOfMonth datetime
                            declare @InstrumentPK int

                            select @StartOfMonth = DATEADD(DAY,1,EOMONTH(@Date,-1))

                            select 0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY AccountID ASC) AutoNo,AccountID,AccountName,DetailDescription,BaseDebit,BaseCredit
                            from (
                            select F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
                            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
                            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                            left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

                            where A.status = 2 and Posted = 1 and Reversed = 0 and
                            ValueDate between @StartOfMonth and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @PK 
                            group by F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 
                            )D";


                            cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);

                        }


                        cmd.Parameters.AddWithValue("@PK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InterfaceJournalSAP.Add(setDetailInterfaceJournalSAP(dr));
                                }
                            }
                            return L_InterfaceJournalSAP;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public void SelectDeselectAllDataByDate(bool _toggle, DateTime _Date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        @" Update A set SelectedSAP = @Toggle from Investment A 
                        left join FundJournal B on A.Reference = B.Reference and B.status = 2                   
                        where B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and A.valueDate = @Date ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@Date", _Date);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectData(bool _toggle, int _PK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update A set SelectedSAP = @Toggle from Investment A 
                        left join FundJournal B on A.Reference = B.Reference and B.status = 2                   
                        where B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and InvestmentPK  = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string InterfaceJournalSAP_PostingBySelected(string _usersID, DateTime _date, string _fundJournalType)
        {
            SAPConnectionConfig SAPCon = new SAPConnectionConfig();
            try
            {
                RfcDestinationManager.RegisterDestinationConfiguration(SAPCon);

                RfcDestination dest = RfcDestinationManager.GetDestination(Tools._sapServer);

                RfcRepository repo = dest.Repository;

                IRfcFunction testfn = repo.CreateFunction("ZFM_PA_DOCUMENT_POST");

                IRfcTable tableAccountGL = testfn.GetTable("ACCOUNT_GL");
                IRfcTable tableCURRENCYAMOUNT = testfn.GetTable("CURRENCYAMOUNT");
                IRfcTable tableAccountReceivable = testfn.GetTable("ACCOUNTRECEIVABLE");
                IRfcTable tableCriteria1 = testfn.GetTable("CRITERIA");

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InterfaceJournalSAP> _interfaceJournalSAP = new List<InterfaceJournalSAP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFundJournalType = "";

                        if (_fundJournalType != "ALL")
                        {
                            _paramFundJournalType = " and FundJournalType = @ParamFundJournalType ";
                        }
                        else
                        {
                            _paramFundJournalType = "";
                        }

                        if (_fundJournalType == "TRANSACTION")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                Declare @PostingDate nvarchar(10)
Declare @TransactionPK int
Declare @DocType nvarchar(10)
Declare @ItemText nvarchar(100)
Declare @HeaderText nvarchar(100)
Declare @RefNo nvarchar(100)
Declare @ItemAgeng nvarchar(100)  
        
DECLARE @A TABLE
(
InvestmentPK int, HEADER_TXT nvarchar(100),COMP_CODE nvarchar(100),PSTNG_DATE nvarchar(100),
FISC_YEAR nvarchar(100),FIS_PERIOD nvarchar(100),REF_DOC_NO nvarchar(100),DOC_TYPE nvarchar(100),
GROUP_DOC nvarchar(100),ITEMNO_ACC nvarchar(100),GL_ACCOUNT nvarchar(100),ITEM_TEXT nvarchar(100),
BUS_AREA nvarchar(100),ORDERID nvarchar(100),FUNDS_CTR nvarchar(100),COST_CENTER nvarchar(100),
CURRENCY nvarchar(100),AMT_DOCCUR nvarchar(100),DEBIT_CREDIT nvarchar(100),AccountType nvarchar(100),
CUSTOMER nvarchar(100),PROFIT_CTR nvarchar(100), COSTCENTER nvarchar(100) 
        
)

		

        
Insert into @A
SELECT A.InvestmentPk,replace(B.Reference,'/INV','') + '/' +CONVERT(VARCHAR(10), B.ValueDate, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), B.ValueDate, 112) PSTNG_DATE,DATEPART(YEAR,B.ValueDate) FISC_YEAR,DATEPART(MONTH,B.ValueDate) FIS_PERIOD,
A.InvestmentPK REF_DOC_NO,
        
CASE WHEN ISNULL(F.BitIsBank,0) = 1  AND  D.BaseCredit <> 0 THEN 'JO' else CASE WHEN ISNULL(F.BitIsBank,0) = 1  AND  D.BaseDebit <> 0 THEN 'JI' ELSE
'JM' END END DOC_TYPE,
        
case when C.InstrumentTypePK = 1 then 'EQUITY' else case when C.InstrumentTypePK = 6 then 'REKSADANA'
else case when C.InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when C.InstrumentTypePK not in (1,4,5,6,8,16) then 'BOND' else case when C.InstrumentTypePK in (8) then 'KIK EBA'
else 'UNIT-LINK' end end end end end GROUP_DOC,
D.AutoNo ITEMNO_ACC,ISNULL(F.ID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COSTCENTER,
'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when A.InstrumentTypePK = 5 then isnull(G.SAPCustID,'') else  isnull(C.SAPCustID,'') end CUSTOMER  
,'P-J010-000' PROFIT_CTR 
,'P-J010-000' COSTCENTER 
from Investment A
left join FundJournal B on A.Reference = B.Reference and B.status = 2
left join Instrument C on A.instrumentPK = C.InstrumentPK and C.Status in (1,2)
left join FundJournalDetail D on B.FundJournalPK = D.FundJournalPK and D.Status = 2
left join ZSAP_BridgeJournal E on D.FundJournalAccountPK = E.FundJournalAccountPK and E.Status in (1,2)
left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
where B.Type = 5 and B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and A.ValueDate = @Date and A.InstrumentTypePK not in (5)
and A.SelectedSAP = 1

union all

SELECT A.InvestmentPk,replace(B.Reference,'/INV','') + '/' +CONVERT(VARCHAR(10), B.ValueDate, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), B.ValueDate, 112) PSTNG_DATE,DATEPART(YEAR,B.ValueDate) FISC_YEAR,DATEPART(MONTH,B.ValueDate) FIS_PERIOD,
A.InvestmentPK REF_DOC_NO,
        
CASE WHEN ISNULL(F.BitIsBank,0) = 1  AND  D.BaseCredit <> 0 THEN 'JO' else CASE WHEN ISNULL(F.BitIsBank,0) = 1  AND  D.BaseDebit <> 0 THEN 'JI' ELSE
'JM' END END DOC_TYPE,
        
case when C.InstrumentTypePK = 1 then 'EQUITY' else case when C.InstrumentTypePK = 6 then 'REKSADANA'
else case when C.InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when C.InstrumentTypePK not in (1,4,5,6,8,16) then 'BOND' else case when C.InstrumentTypePK in (8) then 'KIK EBA'
else 'UNIT-LINK' end end end end end GROUP_DOC,
0 ITEMNO_ACC,ISNULL(F.ID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COSTCENTER,
'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when A.InstrumentTypePK = 5 then isnull(G.SAPCustID,'') else  isnull(C.SAPCustID,'') end CUSTOMER  
,'P-J010-000' PROFIT_CTR 
,'P-J010-000' COSTCENTER 
from Investment A
left join FundJournal B on A.Reference = B.Reference and B.status = 2
left join Instrument C on A.instrumentPK = C.InstrumentPK and C.Status in (1,2)
left join FundJournalDetail D on B.FundJournalPK = D.FundJournalPK and D.Status = 2
left join ZSAP_BridgeJournal E on D.FundJournalAccountPK = E.FundJournalAccountPK and E.Status in (1,2)
left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
where B.Type = 5 and B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and A.ValueDate = @Date and A.InstrumentTypePK  in (5)
and A.SelectedSAP = 1


        
        
Declare Z Cursor For 
        
select InvestmentPK,DOC_TYPE,PSTNG_DATE,ITEM_TEXT from @A 
group by InvestmentPK,DOC_TYPE,PSTNG_DATE ,ITEM_TEXT
order by DOC_TYPE asc
        
Open Z                  
Fetch Next From Z                  
Into @TransactionPK,@DocType,@PostingDate,@ItemAgeng
While @@FETCH_STATUS = 0                  
Begin 

Update @A set DOC_TYPE = @DocType where InvestmentPk = @TransactionPK and PSTNG_DATE = @PostingDate and DOC_TYPE = 'JM' and ITEM_TEXT = @ItemAgeng

Fetch next From Z                   
Into @TransactionPK,@DocType,@PostingDate,@ItemAgeng
END                  
Close Z                  
Deallocate z

-- buat deposito
Declare A Cursor For 
        
select InvestmentPK,DOC_TYPE,ITEM_TEXT,HEADER_TXT,REF_DOC_NO from @A where GROUP_DOC = 'TIME DEPOSIT'
group by InvestmentPK,DOC_TYPE,ITEM_TEXT,HEADER_TXT,REF_DOC_NO
order by InvestmentPK asc
        
Open A                  
Fetch Next From A                  
Into @TransactionPK,@DocType,@ItemText,@HeaderText,@RefNo
While @@FETCH_STATUS = 0                  
Begin 

Update @A set HEADER_TXT = @HeaderText, InvestmentPk = @TransactionPK , REF_DOC_NO = @RefNo where ITEM_TEXT = @ItemText


Fetch next From A                   
Into @TransactionPK,@DocType,@ItemText,@HeaderText,@RefNo
END                  
Close A                  
Deallocate A


select * from @A where GROUP_DOC <> 'TIME DEPOSIT'
union all
SELECT InvestmentPK,HEADER_TXT ,COMP_CODE ,PSTNG_DATE ,
FISC_YEAR ,FIS_PERIOD ,REF_DOC_NO ,DOC_TYPE ,
GROUP_DOC ,ROW_NUMBER() OVER (PARTITION BY ITEM_TEXT ORDER BY InvestmentPK) AS ITEMNO_ACC,GL_ACCOUNT ,ITEM_TEXT ,
BUS_AREA ,ORDERID ,FUNDS_CTR ,COST_CENTER ,
CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
CUSTOMER ,PROFIT_CTR , COSTCENTER
       
FROM   @A where GROUP_DOC = 'TIME DEPOSIT'
ORDER  BY ITEM_TEXT  ";


                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
        Declare @PostingDate nvarchar(10)
Declare @TransactionPK int
Declare @DocType nvarchar(10)
        
                          
Declare @FundPK int
Declare @PK int
Declare @ValueDate datetime
Declare @FundJournalType nvarchar(100)
Declare @Description nvarchar(200)        

declare @AcqDate datetime
declare @EndDate datetime
declare @DatelastMonth datetime
declare @DatePreviousMonth datetime

declare @ValueDateBeg datetime
        
        
Declare @StartOfMonth datetime
Declare @InstrumentPK int
        
Declare @JFundPK int,@JInstrumentPK int,@JInterestRate numeric(18,8),@JMaturityDate datetime
Declare @JSettledDate datetime,@JCostPrice numeric(18,8),@JFaceValue numeric(24,4)
Declare @JCostValue numeric(22,8),@JAmount numeric(24,10),@JBitHTM bit,@JInstrumentTypePK int
Declare @JInvAmortizeAcc int,@JAmortizeAcc int,@JInstrumentID nvarchar(100), @JValueDate datetime
Declare @ZIdentity bigint
        
Declare @lastPreviousMonth datetime
select @lastPreviousMonth = max(ValueDate) from EndDayTrailsFundPortfolio where status = 2
and ValueDate <= dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))
        
declare @lastMonth datetime
select @lastMonth = dateadd(day,-1,DATEADD(DAY,1,EOMONTH(@Date,-1)))
        
 

CREATE TABLE #A
(
FundJournalAccountPK int,Description nvarchar(500),BaseDebit numeric(22,4),BaseCredit numeric(22,4), InstrumentPK int
)
        
DECLARE @A TABLE
(
InvestmentPK int, HEADER_TXT nvarchar(100),COMP_CODE nvarchar(100),PSTNG_DATE nvarchar(100),
FISC_YEAR nvarchar(100),FIS_PERIOD nvarchar(100),REF_DOC_NO nvarchar(100),DOC_TYPE nvarchar(100),
GROUP_DOC nvarchar(100),ITEMNO_ACC nvarchar(100),GL_ACCOUNT nvarchar(100),ITEM_TEXT nvarchar(100),
BUS_AREA nvarchar(100),ORDERID nvarchar(100),FUNDS_CTR nvarchar(100),COST_CENTER nvarchar(100),
CURRENCY nvarchar(100),AMT_DOCCUR nvarchar(100),DEBIT_CREDIT nvarchar(100),AccountType nvarchar(100),
CUSTOMER nvarchar(100),PROFIT_CTR nvarchar(100), COSTCENTER nvarchar(100) 
        
)

DECLARE @B TABLE
(
InvestmentPK int, HEADER_TXT nvarchar(100),COMP_CODE nvarchar(100),PSTNG_DATE nvarchar(100),
FISC_YEAR nvarchar(100),FIS_PERIOD nvarchar(100),REF_DOC_NO nvarchar(100),DOC_TYPE nvarchar(100),
GROUP_DOC nvarchar(100),ITEMNO_ACC nvarchar(100),GL_ACCOUNT nvarchar(100),ITEM_TEXT nvarchar(100),
BUS_AREA nvarchar(100),ORDERID nvarchar(100),FUNDS_CTR nvarchar(100),COST_CENTER nvarchar(100),
CURRENCY nvarchar(100),AMT_DOCCUR nvarchar(100),DEBIT_CREDIT nvarchar(100),AccountType nvarchar(100),
CUSTOMER nvarchar(100),PROFIT_CTR nvarchar(100), COSTCENTER nvarchar(100) 
        
)


declare @Interest Table 
(
FundJournalPK int,
AccountID nvarchar(50),
SAPAccountID nvarchar(50),
SAPAccountName nvarchar(100),
DetailDescription nvarchar(200),
BaseDebit numeric(18,4),
BaseCredit numeric(18,4),
BankPK int
)

declare @InterestTemp Table 
(
AccountID nvarchar(50),
SAPAccountID nvarchar(50),
SAPAccountName nvarchar(100),
DetailDescription nvarchar(200),
FundPK int,
BankPK int,
BaseDebit numeric(18,4),
BaseCredit numeric(18,4)

)
        
-- pak tinggal yg ORDERID yg belum muncul nih pak
        
Declare B Cursor For 
select FundPK,FundJournalPK,ValueDate,FundJournalType,Description from ZSAP_FUNDJOURNAL_TEMP where   selected = 1 " + _paramFundJournalType + @"
Open B                  
Fetch Next From B                  
Into @FundPK,@PK,@ValueDate,@FundJournalType,@Description
While @@FETCH_STATUS = 0                  
Begin 
        
        IF (@FundJournalType in ('AMORTIZE'))
        BEGIN
        delete @B

        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
        ) and A.status = 2
        and 
        C.InstrumentTypePK not in (1,4,5,6,16)

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

            Insert into @B	
			SELECT case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
			else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '22'
			else '05' end end end end FundJournalPK,replace(case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
			else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '22'
			else '05' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
			case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
			else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '22'
			else '05' end end end end REF_DOC_NO,
            'JM'  DOC_TYPE,

            case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
            else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
            else 'UNIT-LINK' end end end end GROUP_DOC,
            ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
            'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
            ,'P-J010-000' PROFIT_CTR 
            ,'P-J010-000' COSTCENTER 
            from (

                select BitIsBank,SAPCustID,InstrumentTypePK,FundJournalPK,AccountID,AccountType,
				AccountName,DetailDescription DetailDescription,
				case when sum(BaseDebit-BaseCredit) < 0 then 0 else abs(sum(BaseDebit-BaseCredit)) end BaseDebit,
				case when sum(BaseDebit-BaseCredit) < 0 then abs(sum(BaseDebit-BaseCredit)) else 0 end BaseCredit
					from (
				select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,F.ID AccountID,AccountType,
				F.Name AccountName,A.DetailDescription DetailDescription,abs(sum(A.BaseDebit)) BaseDebit,abs(sum(A.BaseCredit)) BaseCredit 
				from FundJournalDetail A
				left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
				left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
				left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
				left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

				where A.status = 2 and Posted = 1 and Reversed = 0 and
				ValueDate between DATEADD(month, DATEDIFF(month, 0, @Date), 0) and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
				group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription
				) Z
				group by BitIsBank,SAPCustID,InstrumentTypePK,FundJournalPK,AccountID,AccountType,
				AccountName,DetailDescription
            ) D


        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;


		Insert into @A
        select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
        FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
        GROUP_DOC,ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
        BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
        CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
        CUSTOMER,PROFIT_CTR , COSTCENTER  from @B

        
        
    END
        
    ELSE IF (@FundJournalType in ('ADJUSTMENT'))
    BEGIN
        Insert into @A
        select FundJournalPK,replace(FundJournalPK,'/ADJ','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        @PK REF_DOC_NO,
        CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseCredit <> 0 THEN 'JO' else CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseDebit <> 0 THEN 'JI' ELSE
            'JM' END END DOC_TYPE,
        
        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
        else 'UNIT-LINK' end end end end GROUP_DOC,
        ROW_NUMBER() OVER(ORDER BY @PK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COSTCENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustBank,'') else  isnull(SAPCustInstrument,'') end CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (
        select F.BitIsBank,A.FundJournalPK,F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,C.InstrumentTypePK,F.AccountType,C.SAPCustID SAPCustInstrument,G.SAPCustID SAPCustBank,
        case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
        case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
        left join Bank G on C.BankPK = G.BankPK and G.Status in (1,2)
        where A.status = 2 and Posted = 1 and Reversed = 0 and A.FundJournalPK = @PK 
        group by F.BitIsBank,A.FundJournalPK,F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK, C.InstrumentTypePK,F.AccountType,C.SAPCustID,G.SAPCustID
        )D
        
        
        Declare Z Cursor For 
        
        select InvestmentPK,DOC_TYPE,PSTNG_DATE from @A
        group by InvestmentPK,DOC_TYPE,PSTNG_DATE 
        order by DOC_TYPE asc
        
        Open Z                  
        Fetch Next From Z                  
        Into @TransactionPK,@DocType,@PostingDate
        While @@FETCH_STATUS = 0                  
        Begin 
        
        Update @A set DOC_TYPE = @DocType where InvestmentPk = @TransactionPK and PSTNG_DATE = @PostingDate and DOC_TYPE = 'JM'
        
        Fetch next From Z                   
        Into @TransactionPK,@DocType,@PostingDate
        END                  
        Close Z                  
        Deallocate z
        
        
        
    END
    ELSE IF(@FundJournalType in ('REC COUPON'))
	BEGIN


		
            Insert into @A

            SELECT @PK FundJournalPK,replace(@PK,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
            @PK REF_DOC_NO,
            CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseCredit <> 0 THEN 'JO' else CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseDebit <> 0 THEN 'JI' ELSE
            'JI' END END DOC_TYPE,
            case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
            else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
            else 'UNIT-LINK' end end end end GROUP_DOC,
            ROW_NUMBER() OVER(ORDER BY @PK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
            'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustIDBank,'') else  isnull(SAPCustIDInstrument,'') end CUSTOMER  
            ,'P-J010-000' PROFIT_CTR 
            ,'P-J010-000' COSTCENTER 
            from (
            select F.BitIsBank,F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,C.InstrumentTypePK,F.AccountType,C.SAPCustID SAPCustInstrument,G.SAPCustID SAPCustBank,
            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit,G.SAPCustID SAPCustIDBank,C.SAPCustID SAPCustIDInstrument    from FundJournalDetail A
            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
            left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
            left join Bank G on C.BankPK = G.BankPK and G.Status in (1,2)
            where A.status = 2 and Posted = 1 and Reversed = 0 and
            ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @PK 
            group by F.BitIsBank,F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK, C.InstrumentTypePK,F.AccountType,C.SAPCustID,G.SAPCustID
            )D

    END
	ELSE IF(@FundJournalType in ('REC COUPON EBA'))
	BEGIN


		
            Insert into @A

            SELECT @PK FundJournalPK,replace(@PK,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
            @PK REF_DOC_NO,
            CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseCredit <> 0 THEN 'JO' else CASE WHEN ISNULL(BitIsBank,0) = 1  AND  D.BaseDebit <> 0 THEN 'JI' ELSE
            'JI' END END DOC_TYPE,
            case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
            else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,8,16) then 'BOND' else case when InstrumentTypePK  in (8) then 'KIK EBA'
            else 'UNIT-LINK' end end end end end GROUP_DOC,
            ROW_NUMBER() OVER(ORDER BY @PK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
            'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustIDBank,'') else  isnull(SAPCustIDInstrument,'') end CUSTOMER  
            ,'P-J010-000' PROFIT_CTR 
            ,'P-J010-000' COSTCENTER 
            from (
            select F.BitIsBank,F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,C.InstrumentTypePK,F.AccountType,C.SAPCustID SAPCustInstrument,G.SAPCustID SAPCustBank,
            case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
            case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit,G.SAPCustID SAPCustIDBank,C.SAPCustID SAPCustIDInstrument    from FundJournalDetail A
            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
            left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
            left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
            left join Bank G on C.BankPK = G.BankPK and G.Status in (1,2)
            where A.status = 2 and Posted = 1 and Reversed = 0 and
            ValueDate = @Date and TrxName = 'REC COUPON' and A.FundPK = @FundPK and A.InstrumentPK = @PK 
            group by F.BitIsBank,F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK, C.InstrumentTypePK,F.AccountType,C.SAPCustID,G.SAPCustID
            )D

    END
    ELSE IF(@FundJournalType in ('REC COUPON DEPOSITO'))
	BEGIN

        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date < @Date and FundPK = @FundPK
        )
        and C.InstrumentTypePK in (5) and A.status = 2 and C.BankPK = @PK

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2

        select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@Date)) then eomonth(@Date) else 
        CONVERT(DATETIME,CAST(cast(datepart(month,@Date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@Date) as nvarchar(4)) as nvarchar(10)),101) end

        select @DatelastMonth = case when @DatelastMonth >= @Date then DATEADD(month, -1, @DatelastMonth) else @DatelastMonth end
        select @DatelastMonth = dateadd(day,1,@DatelastMonth)
        select @EndDate = @Date

						

        insert into @InterestTemp
        select D.ID,F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,
        case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
        case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate = @Date and TrxName = 'REC COUPON' and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
        group by D.ID,F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK 



        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;


        insert into @A
        SELECT '03' FundJournalPK,replace(@PK,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        @PK REF_DOC_NO,
        'JI'  DOC_TYPE,
        'TIME DEPOSIT' GROUP_DOC,
        ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType, SAPCustID CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (

        select F.AccountType AccountType,D.SAPCustID SAPCustID ,F.ID AccountID,A.DetailDescription,A.BaseDebit,A.BaseCredit from @InterestTemp A
        left join FundJournalAccount B on A.AccountID = B.ID and B.status in (1,2)
        left join Instrument C on A.BankPK = C.InstrumentPK and C.status in (1,2)
		left join Bank D on C.BankPK = D.BankPK and D.status in (1,2)
        left join ZSAP_BridgeJournal E on B.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
        ) D

    END
    ELSE IF(@FundJournalType in ('INTEREST DEPOSIT'))
    BEGIN
   

        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
        ) and A.status = 2
        and C.InstrumentTypePK in (5)

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

        select @ValueDateBeg = eomonth(DATEADD(month, -1, @Date))

        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2


        if (datepart(day,@AcqDate) = 31)
        BEGIN
        select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@date) else 
        CONVERT(DATETIME,CAST(cast(datepart(month,@date) as nvarchar(2)) + '/30/' + cast(datepart(year,@date) as nvarchar(4)) as nvarchar(10)),101) end

        END
        ELSE
        BEGIN
        select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@date) else 
        CONVERT(DATETIME,CAST(cast(datepart(month,@date) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@date) as nvarchar(4)) as nvarchar(10)),101) end

        END




        select @DatePreviousMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@ValueDateBeg) else 
        CONVERT(DATETIME,CAST(cast(datepart(month,@ValueDateBeg) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@ValueDateBeg) as nvarchar(4)) as nvarchar(10)),101) end

        select @DatelastMonth = case when @date < @DatelastMonth then @DatePreviousMonth else @DatelastMonth end
        select @DatelastMonth = dateadd(day,1,@DatelastMonth)

        insert into @InterestTemp
        select D.ID AccountID,F.ID SAPAccountID,F.Name SAPAccountName,A.DetailDescription,FundPK,C.BankPK,
        case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
        case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit    from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate between @DatelastMonth and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
        group by D.ID,F.ID,F.Name,DetailDescription,FundPK,C.BankPK 



        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;


        insert into @Interest
        select 0 FundJournalPK,AccountID,SAPAccountID,SAPAccountName,DetailDescription,BaseDebit,BaseCredit,BankPK
        from (
        select AccountID,SAPAccountID,SAPAccountName,DetailDescription,sum(BaseDebit) BaseDebit,sum(BaseCredit) BaseCredit,BankPK from @InterestTemp
        group by AccountID,SAPAccountID,SAPAccountName,DetailDescription,BankPK
        )D


        insert into @A
        SELECT '03' FundJournalPK,replace('03','/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        '03' REF_DOC_NO,
        'JM'  DOC_TYPE,
        'TIME DEPOSIT' GROUP_DOC,
        ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType, SAPCustID CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (

        select F.AccountType AccountType,C.SAPCustID,F.ID AccountID,A.DetailDescription,A.BaseDebit,A.BaseCredit from @Interest A
        left join FundJournalAccount B on A.AccountID = B.ID and B.status in (1,2)
        left join Bank C on A.BankPK = C.BankPK and C.status in (1,2)
		left join ZSAP_BridgeJournal E on B.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
		left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
        ) D

    END

    ELSE IF(@FundJournalType in ('INTEREST BOND'))
    BEGIN
    
        delete @B
        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
        ) and A.status = 2
        and 
        C.InstrumentTypePK not in (1,4,5,6,8,16)

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2
        		
        select @DatelastMonth = DATEADD(month, DATEDIFF(month, 0, @Date), 0)

        select @EndDate = @Date

        Insert into @B	
        SELECT case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end FundJournalPK,replace(case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end REF_DOC_NO,
        'JM'  DOC_TYPE,

        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
        else 'UNIT-LINK' end end end end GROUP_DOC,
        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (

        select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
        F.Name AccountName,A.DetailDescription DetailDescription,abs(sum(A.BaseDebit)) BaseDebit,abs(sum(A.BaseCredit)) BaseCredit 
            from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate between @DatelastMonth and @EndDate and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
        group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription
        ) D



        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;

		Insert into @A
        select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
        FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
        GROUP_DOC,ROW_NUMBER() OVER(ORDER BY ITEM_TEXT ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
        BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
        CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
        CUSTOMER,PROFIT_CTR , COSTCENTER  from @B

    END
	ELSE IF(@FundJournalType in ('INTEREST EBA'))
    BEGIN
    
        delete @B
        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
        ) and A.status = 2
        and 
        C.InstrumentTypePK  in (8)

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2
        		
        select @DatelastMonth = DATEADD(month, DATEDIFF(month, 0, @Date), 0)

        select @EndDate = @Date

        Insert into @B	
        SELECT case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '02'
        else '05' end end end end FundJournalPK,replace(case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '02'
        else '05' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '02'
        else '05' end end end end REF_DOC_NO,
        'JM'  DOC_TYPE,

        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,8,16) then 'BOND' else case when InstrumentTypePK not in (1,4,5,6,16) then 'KIK EBA'
        else 'UNIT-LINK' end end end end end GROUP_DOC,
        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (

        select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
        F.Name AccountName,A.DetailDescription DetailDescription,abs(sum(A.BaseDebit)) BaseDebit,abs(sum(A.BaseCredit)) BaseCredit 
            from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate between @DatelastMonth and @EndDate and TrxName = case when @FundJournalType = 'Interest EBA' then 'Interest Bond' else @FundJournalType end and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
        group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription
        ) D



        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;

		Insert into @A
        select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
        FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
        GROUP_DOC,ROW_NUMBER() OVER(ORDER BY ITEM_TEXT ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
        BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
        CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
        CUSTOMER,PROFIT_CTR , COSTCENTER  from @B

    END
    ELSE IF(@FundJournalType in ('INTEREST FUND'))
    BEGIN
    
        delete @B
        Declare A Cursor For

        select distinct A.InstrumentPK from FundPosition A
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
        where   Date  = (
        select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
        ) and A.status = 2
        and 
        C.InstrumentTypePK in (6)

        Open A
        Fetch next From A
        Into @InstrumentPK
        WHILE @@FETCH_STATUS = 0  
        BEGIN

        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @InstrumentPK and Date = dbo.FWorkingDay(@Date,-1) and status = 2
        		
        select @DatelastMonth = DATEADD(month, DATEDIFF(month, 0, @Date), 0)

        select @EndDate = @Date

        Insert into @B	
        SELECT case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end FundJournalPK,replace(case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        case when InstrumentTypePK = 1 then '01' else case when InstrumentTypePK = 6 then '04'
        else case when InstrumentTypePK = 5 then '03' else case when InstrumentTypePK not in (1,4,5,6,16) then '02'
        else '05' end end end end REF_DOC_NO,
        'JM'  DOC_TYPE,

        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
        else 'UNIT-LINK' end end end end GROUP_DOC,
        ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (

        select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
        F.Name AccountName,A.DetailDescription DetailDescription,abs(sum(A.BaseDebit)) BaseDebit,abs(sum(A.BaseCredit)) BaseCredit 
            from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID

        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate between @DatelastMonth and @EndDate and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
        group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription
        ) D



        FETCH NEXT FROM A 
        INTO @InstrumentPK
        END 

        CLOSE A;  
        DEALLOCATE A;

		Insert into @A
        select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
        FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
        GROUP_DOC,ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
        BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
        CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
        CUSTOMER,PROFIT_CTR , COSTCENTER  from @B

    END
        
        
    ELSE IF(@FundJournalType in ('PORTFOLIO REVALUATION'))
    BEGIN
        IF (@Description = 'PORTFOLIO REVALUATION EQUITY')
        BEGIN
					delete @B
                    Declare A Cursor For

                    select distinct A.InstrumentPK from FundPosition A
                    left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                    where   Date  = (
                    select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                    ) and A.status = 2
                    and 
                    C.InstrumentTypePK in (1,4,16)

                    Open A
                    Fetch next From A
                    Into @InstrumentPK
                    WHILE @@FETCH_STATUS = 0  
                    BEGIN

                        Insert into @B	
                        SELECT  case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end  FundJournalPK,
						replace(case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,
						'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
                        case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end REF_DOC_NO,
                        'JM'  DOC_TYPE,

                        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
                        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
                        else 'UNIT-LINK' end end end end GROUP_DOC,
                        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
                        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
                        ,'P-J010-000' PROFIT_CTR 
                        ,'P-J010-000' COSTCENTER 
                        from (

                            select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,AutoNo,AccountID,AccountType,
                                AccountName,DetailDescription,
								case when MarketValue >= 0 and Type = 1 then abs(MarketValue) else case when MarketValue < 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
								case when MarketValue < 0 and Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseCredit    
                            from (
                                select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
                                F.Name AccountName,A.DetailDescription DetailDescription,MarketValue,D.Type
                                from FundJournalDetail A
                                left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                                left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                                left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                                left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
								left join 
								(
									select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
									select InstrumentPK,(MarketValue - CostValue) MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @Date and FundPK = @FundPK
									union all
									select InstrumentPK,(MarketValue - CostValue) * -1 MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @lastPreviousMonth and FundPK = @FundPK
									) G group by InstrumentPK,FundPK
								) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                                where A.status = 2 and Posted = 1 and Reversed = 0 and
                                ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                                group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription,MarketValue,D.Type
                            )G
                        ) D

                        FETCH NEXT FROM A 
                        INTO @InstrumentPK
                        END 

                        CLOSE A;  
                        DEALLOCATE A;

						--Insert into @A
						--select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						--FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						--GROUP_DOC,ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
						--BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						--CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
						--CUSTOMER,PROFIT_CTR , COSTCENTER  from @B
						Insert into @A
						select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,ROW_NUMBER() OVER(ORDER BY ITEM_TEXT ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,sum(cast(AMT_DOCCUR as numeric(18,0))) AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER  from @B
						group by InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER 
						order by ITEM_TEXT

        END

        ELSE IF (@Description = 'PORTFOLIO REVALUATION BOND')
        BEGIN

					delete @B
                    Declare A Cursor For

                    select distinct A.InstrumentPK,A.AcqDate from FundPosition A
                    left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                    where   Date  = (
                    select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                    ) and A.status = 2
                    and 
                    C.InstrumentTypePK not in (1,4,5,6,16)

                    Open A
                    Fetch next From A
                    Into @InstrumentPK,@AcqDate
                    WHILE @@FETCH_STATUS = 0  
                    BEGIN

					IF NOT EXISTS(select * from fundposition where FundPK = @FundPK and InstrumentPk = @InstrumentPK and AcqDate = @AcQdate
					and Date = @lastPreviousMonth)
					BEGIN
					select @lastPreviousMonth = @AcqDate
					END

                        Insert into @B	
                        SELECT  case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end  FundJournalPK,
						replace(case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,
						'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
                        case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end REF_DOC_NO,
                        'JM'  DOC_TYPE,

                        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
                        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
                        else 'UNIT-LINK' end end end end GROUP_DOC,
                        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
                        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
                        ,'P-J010-000' PROFIT_CTR 
                        ,'P-J010-000' COSTCENTER 
                        from (

                            select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,AutoNo,AccountID,AccountType,
                                AccountName,DetailDescription,
								case when MarketValue >= 0 and Type = 1 then abs(MarketValue) else case when MarketValue < 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
								case when MarketValue < 0 and Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseCredit    
                            from (
                                select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
                                F.Name AccountName,A.DetailDescription DetailDescription,MarketValue,D.Type
                                from FundJournalDetail A
                                left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                                left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                                left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                                left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
								left join 
								(
									select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
									select A.InstrumentPK,
									CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
									(
									@date,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
									A.Balance,A.CostValue
									),0)))) ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@Date) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1))))  END MarketValue,
									FundPK from FundPosition A
									left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
									where A.InstrumentPK = @InstrumentPK and A.status = 2  and FundPK = @FundPK and A.AcqDate = @AcqDate  and A.BitHTM <> 1
									and Date  = (
									select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
									) and A.status = 2
									union all
									select A.InstrumentPK,
									CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
									(
									@lastMonth,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
									A.Balance,A.CostValue
									),0)))) * -1  ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@lastMonth) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1)))) * - 1  END MarketValue,
									FundPK from FundPosition A
									left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
									where A.InstrumentPK = @InstrumentPK and A.status = 2 and date = @lastPreviousMonth and FundPK = @FundPK  and A.AcqDate = @AcqDate  and A.BitHTM <> 1
									) G group by InstrumentPK,FundPK
								) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                                where A.status = 2 and Posted = 1 and Reversed = 0 and
                                ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK
                                group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription,MarketValue,D.Type
                            )G
                        ) D

                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@AcqDate
                        END 

                        CLOSE A;  
                        DEALLOCATE A;

						Insert into @A
						select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,ROW_NUMBER() OVER(ORDER BY ITEM_TEXT ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,sum(cast(AMT_DOCCUR as numeric(18,0))) AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER  from @B
						group by InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER 
						order by ITEM_TEXT
        END
		ELSE IF (@Description = 'PORTFOLIO REVALUATION EBA')
        BEGIN

					delete @B
                    Declare A Cursor For

                    select distinct A.InstrumentPK,A.AcqDate from FundPosition A
                    left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                    where   Date  = (
                    select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                    ) and A.status = 2
                    and 
                    C.InstrumentTypePK  in (8)

                    Open A
                    Fetch next From A
                    Into @InstrumentPK,@AcqDate
                    WHILE @@FETCH_STATUS = 0  
                    BEGIN

					IF NOT EXISTS(select * from fundposition where FundPK = @FundPK and InstrumentPk = @InstrumentPK and AcqDate = @AcQdate
					and Date = @lastPreviousMonth)
					BEGIN
					select @lastPreviousMonth = @AcqDate
					END

                        Insert into @B	
                        SELECT  case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '12'
						else '15' end end end end  FundJournalPK,
						replace(case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '12'
						else '15' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,
						'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
                        case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,8,16) then '12'
						else '15' end end end end REF_DOC_NO,
                        'JM'  DOC_TYPE,

                        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
                        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,8,16) then 'BOND' else case when InstrumentTypePK  in (8) then 'KIK EBA'
                        else 'UNIT-LINK' end end end end end GROUP_DOC,
                        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
                        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
                        ,'P-J010-000' PROFIT_CTR 
                        ,'P-J010-000' COSTCENTER 
                        from (

                            select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,AutoNo,AccountID,AccountType,
                                AccountName,DetailDescription,
								case when MarketValue >= 0 and Type = 1 then abs(MarketValue) else case when MarketValue < 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
								case when MarketValue < 0 and Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseCredit    
                            from (
                                select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
                                F.Name AccountName,A.DetailDescription DetailDescription,MarketValue,D.Type
                                from FundJournalDetail A
                                left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                                left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                                left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                                left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
								left join 
								(
									select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
									select A.InstrumentPK,
									CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
									(
									@date,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
									A.Balance,A.CostValue
									),0)))) ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@Date) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1))))  END MarketValue,
									FundPK from FundPosition A
									left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
									where A.InstrumentPK = @InstrumentPK and A.status = 2  and FundPK = @FundPK and A.AcqDate = @AcqDate  and A.BitHTM <> 1
									and Date  = (
									select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
									) and A.status = 2
									union all
									select A.InstrumentPK,
									CASE WHEN B.InstrumentTypePK <> 13 THEN (A.MarketValue - (A.Balance + (A.CostValue - A.Balance - isnull([dbo].[FgetTotalAmortizeFromStartEffectiveByDate]  
									(
									@lastMonth,A.InstrumentPK,A.InterestPercent,A.MaturityDate,A.AvgPrice,
									A.Balance,A.CostValue
									),0)))) * -1  ELSE (A.MarketValue - (A.Balance + ((A.CostValue - A.Balance) -((Datediff(day,A.AcqDate,@lastMonth) * (A.Balance - A.CostValue) / Datediff(day,A.AcqDate,A.MaturityDate))*-1)))) * - 1  END MarketValue,
									FundPK from FundPosition A
									left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
									where A.InstrumentPK = @InstrumentPK and A.status = 2 and date = @lastPreviousMonth and FundPK = @FundPK  and A.AcqDate = @AcqDate  and A.BitHTM <> 1
									) G group by InstrumentPK,FundPK
								) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                                where A.status = 2 and Posted = 1 and Reversed = 0 and
                                ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK
                                group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription,MarketValue,D.Type
                            )G
                        ) D

                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@AcqDate
                        END 

                        CLOSE A;  
                        DEALLOCATE A;

						Insert into @A
						select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,ROW_NUMBER() OVER(ORDER BY ITEM_TEXT ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,sum(cast(AMT_DOCCUR as numeric(18,0))) AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER  from @B
						group by InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER 
						order by ITEM_TEXT
        END
        ELSE IF (@Description = 'PORTFOLIO REVALUATION REKSADANA')
        BEGIN

					delete @B
                    Declare A Cursor For

                    select distinct A.InstrumentPK from FundPosition A
                    left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                    where   Date  = (
                    select max(Date) from FundPosition where status = 2 and Date <= @Date and FundPK = @FundPK
                    ) and A.status = 2
                    and 
                    C.InstrumentTypePK in (6)

                    Open A
                    Fetch next From A
                    Into @InstrumentPK
                    WHILE @@FETCH_STATUS = 0  
                    BEGIN

                        Insert into @B	
                        SELECT  case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end  FundJournalPK,
						replace(case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end,'/INV','') + '/' +CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,
						'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
                        case when InstrumentTypePK = 1 then '11' else case when InstrumentTypePK = 6 then '14'
						else case when InstrumentTypePK = 5 then '13' else case when InstrumentTypePK not in (1,4,5,6,16) then '12'
						else '15' end end end end REF_DOC_NO,
                        'JM'  DOC_TYPE,

                        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
                        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
                        else 'UNIT-LINK' end end end end GROUP_DOC,
                        0 ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
                        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustID,'') else  isnull(SAPCustID,'') end CUSTOMER  
                        ,'P-J010-000' PROFIT_CTR 
                        ,'P-J010-000' COSTCENTER 
                        from (

                            select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,AutoNo,AccountID,AccountType,
                                AccountName,DetailDescription,
								case when MarketValue >= 0 and Type = 1 then abs(MarketValue) else case when MarketValue < 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseDebit,
								case when MarketValue < 0 and Type = 1 then abs(MarketValue) else case when MarketValue >= 0 and Type <> 1 then abs(MarketValue) else 0 end end BaseCredit    
                            from (
                                select BitIsBank,SAPCustID,InstrumentTypePK,0 FundJournalPK,ROW_NUMBER() OVER(ORDER BY A.FundJournalAccountPK ASC) AutoNo,F.ID AccountID,AccountType,
                                F.Name AccountName,A.DetailDescription DetailDescription,cast(MarketValue as numeric(22,0)) MarketValue,D.Type
                                from FundJournalDetail A
                                left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                                left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
                                left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
                                left join ZSAP_MS_ACCOUNT F on E.SAPAccountID = F.ID
								left join 
								(
									select InstrumentPK,sum(MarketValue) MarketValue,FundPK from (
									select InstrumentPK,(MarketValue - CostValue) MarketValue,FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @Date and FundPK = @FundPK
									union all
									select InstrumentPK,(MarketValue - CostValue) * -1 MarketValue, FundPK from FundPosition where InstrumentPK = @InstrumentPK and status = 2 and date = @lastPreviousMonth and FundPK = @FundPK
									) G group by InstrumentPK,FundPK
								) H on A.InstrumentPK = H.InstrumentPK and A.FundPK = H.FundPK

                                where A.status = 2 and Posted = 1 and Reversed = 0 and
                                ValueDate = @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK 
                                group by BitIsBank,SAPCustID,InstrumentTypePK,A.FundJournalAccountPK,F.ID,AccountType,F.Name,A.DetailDescription,MarketValue,D.Type
                            )G
                        ) D

                        FETCH NEXT FROM A 
                        INTO @InstrumentPK
                        END 

                        CLOSE A;  
                        DEALLOCATE A;

						Insert into @A
						select InvestmentPK, HEADER_TXT,COMP_CODE,PSTNG_DATE,
						FISC_YEAR,FIS_PERIOD,REF_DOC_NO,DOC_TYPE,
						GROUP_DOC,ROW_NUMBER() OVER(ORDER BY @InstrumentPK ASC) ITEMNO_ACC,GL_ACCOUNT,ITEM_TEXT,
						BUS_AREA,ORDERID ,FUNDS_CTR ,COST_CENTER ,
						CURRENCY ,AMT_DOCCUR ,DEBIT_CREDIT ,AccountType ,
						CUSTOMER,PROFIT_CTR , COSTCENTER  from @B
        END
    END
        	
    ELSE
    BEGIN
        
        select @ValueDateBeg = eomonth(DATEADD(month, -1, @Date))
        select @AcqDate = AcqDate from FundPosition where FundPK = @FundPK and InstrumentPK = @PK and Date = dbo.FWorkingDay(@Date,-1) and status = 2
        select @DatelastMonth = case when datepart(day,@AcqDate) > day(eomonth(@ValueDateBeg)) then eomonth(@ValueDateBeg) else 
        CONVERT(DATETIME,CAST(cast(datepart(month,@ValueDateBeg) as nvarchar(2)) + '/' + cast(datepart(day,@AcqDate) as nvarchar(2)) + '/' + cast(datepart(year,@ValueDateBeg) as nvarchar(4)) as nvarchar(10)),101) end
        
        select @DatelastMonth = DATEADD(DAY, 1, @DatelastMonth)
        		
        Insert into @A
        
        SELECT @PK FundJournalPK,replace(@PK,'/INV','') + '/' + CONVERT(VARCHAR(10), @Date, 112) HEADER_TXT,'AJT' COMP_CODE,CONVERT(VARCHAR(10), @Date, 112) PSTNG_DATE,DATEPART(YEAR,@Date) FISC_YEAR,DATEPART(MONTH,@Date) FIS_PERIOD,
        @PK REF_DOC_NO,
        'JM'  DOC_TYPE,
        case when InstrumentTypePK = 1 then 'EQUITY' else case when InstrumentTypePK = 6 then 'REKSADANA'
        else case when InstrumentTypePK = 5 then 'TIME DEPOSIT' else case when InstrumentTypePK not in (1,4,5,6,16) then 'BOND'
        else 'UNIT-LINK' end end end end GROUP_DOC,
        ROW_NUMBER() OVER(ORDER BY @PK ASC) ITEMNO_ACC,ISNULL(AccountID,'21211201')  GL_ACCOUNT,D.DetailDescription ITEM_TEXT,'J000' BUS_AREA,'Z00J-INVEST' ORDERID,'' FUNDS_CTR,'' COST_CENTER,
        'IDR' CURRENCY,case when D.BaseDebit <> 0 then BaseDebit else BaseCredit end AMT_DOCCUR,case when D.BaseDebit <> 0 then 'S' else 'H' end DEBIT_CREDIT,AccountType AccountType, case when InstrumentTypePK = 5 then isnull(SAPCustIDBank,'') else  isnull(SAPCustIDInstrument,'') end CUSTOMER  
        ,'P-J010-000' PROFIT_CTR 
        ,'P-J010-000' COSTCENTER 
        from (
        select F.BitIsBank,F.ID AccountID,F.Name AccountName,A.DetailDescription,FundPK,C.InstrumentPK,C.InstrumentTypePK,F.AccountType,C.SAPCustID SAPCustInstrument,G.SAPCustID SAPCustBank,
        case when sum(BaseDebit-BaseCredit) >= 0 then sum(BaseDebit-BaseCredit) else 0 end BaseDebit,
        case when sum(BaseDebit-BaseCredit) < 0 then sum(BaseCredit-BaseDebit) else 0 end BaseCredit,G.SAPCustID SAPCustIDBank,C.SAPCustID SAPCustIDInstrument    from FundJournalDetail A
        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Status = 2
        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
        left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2) 
        left join ZSAP_BridgeJournal E on A.FundJournalAccountPK = E.FundJournalAccountPK and E.status in (1,2)
        left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
        left join Bank G on C.BankPK = G.BankPK and G.Status in (1,2)
        where A.status = 2 and Posted = 1 and Reversed = 0 and
        ValueDate between @DatelastMonth and @Date and TrxName = @FundJournalType and A.FundPK = @FundPK and A.InstrumentPK = @PK 
        group by F.BitIsBank,F.ID,F.Name,DetailDescription,FundPK,C.InstrumentPK, C.InstrumentTypePK,F.AccountType,C.SAPCustID,G.SAPCustID
        )D
    END
        	
        
        
Fetch next From B                   
Into @FundPK,@PK,@ValueDate,@FundJournalType,@Description
END                  
Close B                  
Deallocate B
        
select  InvestmentPK , HEADER_TXT ,COMP_CODE ,PSTNG_DATE ,
FISC_YEAR ,FIS_PERIOD ,REF_DOC_NO ,DOC_TYPE ,
GROUP_DOC ,ITEMNO_ACC ,GL_ACCOUNT ,ITEM_TEXT ,
BUS_AREA ,ORDERID ,FUNDS_CTR ,COST_CENTER ,
CURRENCY ,cast(AMT_DOCCUR as nvarchar(100)) AMT_DOCCUR,DEBIT_CREDIT ,AccountType ,
CUSTOMER ,PROFIT_CTR , COSTCENTER   from @A
        ";
                            cmd.Parameters.AddWithValue("@ParamFundJournalType", _fundJournalType);
                        }



                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                List<SAPJournalModel> rList = new List<SAPJournalModel>();
                                while (dr.Read())
                                {
                                    SAPJournalModel _m = new SAPJournalModel();
                                    _m.HEADER_TXT = dr["HEADER_TXT"].ToString();
                                    _m.COMP_CODE = Convert.ToString(dr["COMP_CODE"]);
                                    _m.PSTNG_DATE = Convert.ToString(dr["PSTNG_DATE"]);
                                    _m.FISC_YEAR = Convert.ToString(dr["FISC_YEAR"]);
                                    _m.FIS_PERIOD = Convert.ToString(dr["FIS_PERIOD"]);
                                    _m.REF_DOC_NO = Convert.ToString(dr["REF_DOC_NO"]);
                                    _m.DOC_TYPE = Convert.ToString(dr["DOC_TYPE"]);
                                    _m.GROUP_DOC = Convert.ToString(dr["GROUP_DOC"]);

                                    _m.ITEMNO_ACC = Convert.ToString(dr["ITEMNO_ACC"]);
                                    _m.GL_ACCOUNT = Convert.ToString(dr["GL_ACCOUNT"]);
                                    _m.BUS_AREA = Convert.ToString(dr["BUS_AREA"]);
                                    _m.ITEM_TEXT = Convert.ToString(dr["ITEM_TEXT"]);
                                    _m.ORDERID = Convert.ToString(dr["ORDERID"]);
                                    _m.FUNDS_CTR = Convert.ToString(dr["FUNDS_CTR"]);
                                    _m.COSTCENTER = Convert.ToString(dr["COSTCENTER"]);
                                    _m.CUSTOMER = Convert.ToString(dr["CUSTOMER"]);

                                    _m.PROFIT_CTR = Convert.ToString(dr["PROFIT_CTR"]);

                                    _m.CURRENCY = Convert.ToString(dr["CURRENCY"]);
                                    _m.AMT_DOCCUR = Convert.ToDecimal(dr["AMT_DOCCUR"]);
                                    _m.DEBIT_CREDIT = Convert.ToString(dr["DEBIT_CREDIT"]);

                                    _m.ACC_TYPE = dr["AccountType"].ToString();
                                    rList.Add(_m);


                                }
                                // tambahin tarikan Instrument,Customer,Type GL, ama grouping dibawah
                                var GroupBy =
                                from r in rList
                                orderby r.PSTNG_DATE ascending
                                group r by new { r.HEADER_TXT, r.PSTNG_DATE, r.COMP_CODE, r.FISC_YEAR, r.FIS_PERIOD, r.REF_DOC_NO, r.DOC_TYPE, r.GROUP_DOC } into rGroup
                                select rGroup;

                                int countDetail;
                                int countHeader;
                                countHeader = -1;
                                string _a;
                                List<SendSAPMessage> _l = new List<SendSAPMessage>();
                                foreach (var rsHeader in GroupBy)
                                {
                                    countHeader++;
                                    tableAccountGL.Clear();
                                    tableCURRENCYAMOUNT.Clear();
                                    tableAccountReceivable.Clear();

                                    countDetail = 0;
                                    testfn.SetValue("HEADER_TXT", rsHeader.Key.HEADER_TXT); //Journal Reference
                                    testfn.SetValue("COMP_CODE", rsHeader.Key.COMP_CODE); // Hardcode
                                    testfn.SetValue("DOC_DATE", rsHeader.Key.PSTNG_DATE); // Sesuain Tanggal
                                    testfn.SetValue("PSTNG_DATE", rsHeader.Key.PSTNG_DATE); // Sesuain Tanggal
                                    testfn.SetValue("FISC_YEAR", rsHeader.Key.FISC_YEAR);// Sesuain Tanggal Year
                                    testfn.SetValue("FIS_PERIOD", rsHeader.Key.FIS_PERIOD);// Sesuain Tanggal Bulan
                                    testfn.SetValue("REF_DOC_NO", rsHeader.Key.REF_DOC_NO);// Investment Number
                                    testfn.SetValue("DOC_TYPE", rsHeader.Key.DOC_TYPE);// Hardcode
                                    testfn.SetValue("GROUP_DOC", rsHeader.Key.GROUP_DOC); // Ikutin di Filter Di Menu SAP

                                    foreach (var rsDetail in rsHeader)
                                    {
                                        #region AddDetail

                                        countDetail++;
                                        if (countDetail == 1)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL1 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL1.SetValue("ITEMNO_ACC", "0000000001"); // Auto no
                                                structAccoungGL1.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL1.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL1.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL1.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL1.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL1.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL1.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL1.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                structAccoungGL1.SetValue("FUNDS_CTR", rsDetail.FUNDS_CTR);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL1.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL1.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL1.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL1.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL1.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL1);

                                                IRfcStructure structCURRENCYAMOUNTGL1 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL1.SetValue("ITEMNO_ACC", "0000000001");
                                                structCURRENCYAMOUNTGL1.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL1.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL1.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL1);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR1 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR1.SetValue("ITEMNO_ACC", "0000000001");
                                                structAccoungAR1.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR1.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR1.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR1.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR1.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR1);

                                                IRfcStructure structCURRENCYAMOUNTGL1 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL1.SetValue("ITEMNO_ACC", "0000000001");
                                                structCURRENCYAMOUNTGL1.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL1.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL1.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL1);
                                            }
                                        }

                                        if (countDetail == 2)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL2 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL2.SetValue("ITEMNO_ACC", "0000000002"); // Auto no
                                                structAccoungGL2.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL2.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL2.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL2.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL2.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL2.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL2.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL2.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL2.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL2.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL2.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL2.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL2.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL2);

                                                IRfcStructure structCURRENCYAMOUNTGL2 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL2.SetValue("ITEMNO_ACC", "0000000002");
                                                structCURRENCYAMOUNTGL2.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL2.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL2.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL2);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR2 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR2.SetValue("ITEMNO_ACC", "0000000002");
                                                structAccoungAR2.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR2.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR2.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR2.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR2.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR2);

                                                IRfcStructure structCURRENCYAMOUNTGL2 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL2.SetValue("ITEMNO_ACC", "0000000002");
                                                structCURRENCYAMOUNTGL2.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL2.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL2.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL2);
                                            }
                                        }

                                        if (countDetail == 3)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL3 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL3.SetValue("ITEMNO_ACC", "0000000003"); // Auto no
                                                structAccoungGL3.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL3.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL3.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL3.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL3.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL3.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL3.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL3.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL3.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL3.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL3.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL3.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL3.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL3);

                                                IRfcStructure structCURRENCYAMOUNTGL3 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL3.SetValue("ITEMNO_ACC", "0000000003");
                                                structCURRENCYAMOUNTGL3.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL3.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL3.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL3);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR3 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR3.SetValue("ITEMNO_ACC", "0000000003");
                                                structAccoungAR3.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR3.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR3.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR3.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR3.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR3);

                                                IRfcStructure structCURRENCYAMOUNTGL3 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL3.SetValue("ITEMNO_ACC", "0000000003");
                                                structCURRENCYAMOUNTGL3.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL3.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL3.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL3);
                                            }
                                        }

                                        if (countDetail == 4)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL4 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL4.SetValue("ITEMNO_ACC", "0000000004"); // Auto no
                                                structAccoungGL4.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL4.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL4.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL4.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL4.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL4.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL4.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL4.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL4.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL4.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL4.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL4.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL4.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL4);

                                                IRfcStructure structCURRENCYAMOUNTGL4 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL4.SetValue("ITEMNO_ACC", "0000000004");
                                                structCURRENCYAMOUNTGL4.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL4.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL4.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL4);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR4 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR4.SetValue("ITEMNO_ACC", "0000000004");
                                                structAccoungAR4.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR4.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR4.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR4.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR4.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR4);

                                                IRfcStructure structCURRENCYAMOUNTGL4 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL4.SetValue("ITEMNO_ACC", "0000000004");
                                                structCURRENCYAMOUNTGL4.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL4.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL4.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL4);
                                            }
                                        }
                                        // 5
                                        if (countDetail == 5)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL5 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL5.SetValue("ITEMNO_ACC", "0000000005"); // Auto no
                                                structAccoungGL5.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL5.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL5.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL5.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL5.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL5.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL5.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL5.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL5.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL5.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL5.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL5.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL5.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL5);

                                                IRfcStructure structCURRENCYAMOUNTGL5 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL5.SetValue("ITEMNO_ACC", "0000000005");
                                                structCURRENCYAMOUNTGL5.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL5.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL5.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL5);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR5 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR5.SetValue("ITEMNO_ACC", "0000000005");
                                                structAccoungAR5.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR5.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR5.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR5.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR5.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR5);

                                                IRfcStructure structCURRENCYAMOUNTGL5 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL5.SetValue("ITEMNO_ACC", "0000000005");
                                                structCURRENCYAMOUNTGL5.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL5.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL5.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL5);
                                            }
                                        }

                                        // 6
                                        if (countDetail == 6)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL6 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL6.SetValue("ITEMNO_ACC", "0000000006"); // Auto no
                                                structAccoungGL6.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL6.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL6.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL6.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL6.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL6.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL6.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL6.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL6.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL6.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL6.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL6.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL6.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL6);

                                                IRfcStructure structCURRENCYAMOUNTGL6 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL6.SetValue("ITEMNO_ACC", "0000000006");
                                                structCURRENCYAMOUNTGL6.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL6.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL6.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL6);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR6 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR6.SetValue("ITEMNO_ACC", "0000000006");
                                                structAccoungAR6.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR6.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR6.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR6.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR6.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR6);

                                                IRfcStructure structCURRENCYAMOUNTGL6 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL6.SetValue("ITEMNO_ACC", "0000000006");
                                                structCURRENCYAMOUNTGL6.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL6.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL6.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL6);
                                            }
                                        }

                                        // 7
                                        if (countDetail == 7)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL7 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL7.SetValue("ITEMNO_ACC", "0000000007"); // Auto no
                                                structAccoungGL7.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL7.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL7.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL7.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL7.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL7.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL7.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL7.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL7.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL7.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL7.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL7.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL7.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL7);

                                                IRfcStructure structCURRENCYAMOUNTGL7 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL7.SetValue("ITEMNO_ACC", "0000000007");
                                                structCURRENCYAMOUNTGL7.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL7.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL7.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL7);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR7 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR7.SetValue("ITEMNO_ACC", "0000000007");
                                                structAccoungAR7.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR7.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR7.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR7.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR7.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR7);

                                                IRfcStructure structCURRENCYAMOUNTGL7 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL7.SetValue("ITEMNO_ACC", "0000000007");
                                                structCURRENCYAMOUNTGL7.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL7.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL7.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL7);
                                            }
                                        }

                                        // 8
                                        if (countDetail == 8)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL8 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL8.SetValue("ITEMNO_ACC", "0000000008"); // Auto no
                                                structAccoungGL8.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL8.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL8.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL8.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL8.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL8.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL8.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL8.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL8.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL8.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL8.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL8.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL8.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL8);

                                                IRfcStructure structCURRENCYAMOUNTGL8 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL8.SetValue("ITEMNO_ACC", "0000000008");
                                                structCURRENCYAMOUNTGL8.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL8.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL8.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL8);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR8 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR8.SetValue("ITEMNO_ACC", "0000000008");
                                                structAccoungAR8.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR8.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR8.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR8.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR8.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR8);

                                                IRfcStructure structCURRENCYAMOUNTGL8 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL8.SetValue("ITEMNO_ACC", "0000000008");
                                                structCURRENCYAMOUNTGL8.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL8.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL8.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL8);
                                            }
                                        }

                                        // 9
                                        if (countDetail == 9)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL9 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL9.SetValue("ITEMNO_ACC", "0000000009"); // Auto no
                                                structAccoungGL9.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL9.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL9.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL9.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL9.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL9.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL9.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL9.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL9.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL9.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL9.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL9.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL9.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL9);

                                                IRfcStructure structCURRENCYAMOUNTGL9 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL9.SetValue("ITEMNO_ACC", "0000000009");
                                                structCURRENCYAMOUNTGL9.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL9.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL9.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL9);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR9 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR9.SetValue("ITEMNO_ACC", "0000000009");
                                                structAccoungAR9.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR9.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR9.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR9.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR9.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR9);

                                                IRfcStructure structCURRENCYAMOUNTGL9 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL9.SetValue("ITEMNO_ACC", "0000000009");
                                                structCURRENCYAMOUNTGL9.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL9.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL9.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL9);
                                            }
                                        }

                                        // 10
                                        if (countDetail == 10)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL10 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL10.SetValue("ITEMNO_ACC", "0000000010"); // Auto no
                                                structAccoungGL10.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL10.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL10.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL10.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL10.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL10.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL10.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL10.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL10.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL10.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL10.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL10.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL10.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL10);

                                                IRfcStructure structCURRENCYAMOUNTGL10 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL10.SetValue("ITEMNO_ACC", "0000000010");
                                                structCURRENCYAMOUNTGL10.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL10.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL10.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL10);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR10 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR10.SetValue("ITEMNO_ACC", "0000000010");
                                                structAccoungAR10.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR10.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR10.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR10.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR10.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR10);

                                                IRfcStructure structCURRENCYAMOUNTGL10 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL10.SetValue("ITEMNO_ACC", "0000000010");
                                                structCURRENCYAMOUNTGL10.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL10.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL10.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL10);
                                            }
                                        }

                                        // 11
                                        if (countDetail == 11)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL11 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL11.SetValue("ITEMNO_ACC", "0000000011"); // Auto no
                                                structAccoungGL11.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL11.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL11.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL11.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL11.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL11.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL11.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL11.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL11.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL11.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL11.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL11.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL11.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL11);

                                                IRfcStructure structCURRENCYAMOUNTGL11 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL11.SetValue("ITEMNO_ACC", "0000000011");
                                                structCURRENCYAMOUNTGL11.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL11.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL11.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL11);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR11 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR11.SetValue("ITEMNO_ACC", "0000000011");
                                                structAccoungAR11.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR11.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR11.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR11.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR11.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR11);

                                                IRfcStructure structCURRENCYAMOUNTGL11 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL11.SetValue("ITEMNO_ACC", "0000000011");
                                                structCURRENCYAMOUNTGL11.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL11.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL11.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL11);
                                            }
                                        }

                                        // 12
                                        if (countDetail == 12)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL12 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL12.SetValue("ITEMNO_ACC", "0000000012"); // Auto no
                                                structAccoungGL12.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL12.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL12.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL12.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL12.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL12.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL12.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL12.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL12.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL12.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL12.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL12.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL12.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL12);

                                                IRfcStructure structCURRENCYAMOUNTGL12 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL12.SetValue("ITEMNO_ACC", "0000000012");
                                                structCURRENCYAMOUNTGL12.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL12.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL12.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL12);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR12 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR12.SetValue("ITEMNO_ACC", "0000000012");
                                                structAccoungAR12.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR12.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR12.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR12.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR12.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR12);

                                                IRfcStructure structCURRENCYAMOUNTGL12 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL12.SetValue("ITEMNO_ACC", "0000000012");
                                                structCURRENCYAMOUNTGL12.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL12.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL12.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL12);
                                            }
                                        }

                                        // 13
                                        if (countDetail == 13)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL13 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL13.SetValue("ITEMNO_ACC", "0000000013"); // Auto no
                                                structAccoungGL13.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL13.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL13.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL13.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL13.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL13.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL13.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL13.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL13.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL13.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL13.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL13.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL13.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL13);

                                                IRfcStructure structCURRENCYAMOUNTGL13 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL13.SetValue("ITEMNO_ACC", "0000000013");
                                                structCURRENCYAMOUNTGL13.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL13.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL13.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL13);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR13 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR13.SetValue("ITEMNO_ACC", "0000000013");
                                                structAccoungAR13.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR13.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR13.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR13.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR13.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR13);

                                                IRfcStructure structCURRENCYAMOUNTGL13 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL13.SetValue("ITEMNO_ACC", "0000000013");
                                                structCURRENCYAMOUNTGL13.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL13.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL13.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL13);
                                            }
                                        }

                                        // 14
                                        if (countDetail == 14)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL14 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL14.SetValue("ITEMNO_ACC", "0000000014"); // Auto no
                                                structAccoungGL14.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL14.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL14.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL14.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL14.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL14.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL14.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL14.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL14.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL14.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL14.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL14.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL14.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL14);

                                                IRfcStructure structCURRENCYAMOUNTGL14 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL14.SetValue("ITEMNO_ACC", "0000000014");
                                                structCURRENCYAMOUNTGL14.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL14.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL14.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL14);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR14 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR14.SetValue("ITEMNO_ACC", "0000000014");
                                                structAccoungAR14.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR14.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR14.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR14.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR14.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR14);

                                                IRfcStructure structCURRENCYAMOUNTGL14 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL14.SetValue("ITEMNO_ACC", "0000000014");
                                                structCURRENCYAMOUNTGL14.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL14.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL14.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL14);
                                            }
                                        }

                                        // 15
                                        if (countDetail == 15)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL15 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL15.SetValue("ITEMNO_ACC", "0000000015"); // Auto no
                                                structAccoungGL15.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL15.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL15.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL15.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL15.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL15.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL15.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL15.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL15.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL15.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL15.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL15.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL15.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL15);

                                                IRfcStructure structCURRENCYAMOUNTGL15 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL15.SetValue("ITEMNO_ACC", "0000000015");
                                                structCURRENCYAMOUNTGL15.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL15.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL15.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL15);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR15 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR15.SetValue("ITEMNO_ACC", "0000000015");
                                                structAccoungAR15.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR15.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR15.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR15.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR15.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR15);

                                                IRfcStructure structCURRENCYAMOUNTGL15 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL15.SetValue("ITEMNO_ACC", "0000000015");
                                                structCURRENCYAMOUNTGL15.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL15.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL15.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL15);
                                            }
                                        }


                                        // 16
                                        if (countDetail == 16)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL16 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL16.SetValue("ITEMNO_ACC", "0000000016"); // Auto no
                                                structAccoungGL16.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL16.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL16.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL16.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL16.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL16.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL16.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL16.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL16.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL16.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL16.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL16.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL16.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL16);

                                                IRfcStructure structCURRENCYAMOUNTGL16 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL16.SetValue("ITEMNO_ACC", "0000000016");
                                                structCURRENCYAMOUNTGL16.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL16.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL16.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL16);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR16 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR16.SetValue("ITEMNO_ACC", "0000000016");
                                                structAccoungAR16.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR16.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR16.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR16.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR16.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR16);

                                                IRfcStructure structCURRENCYAMOUNTGL16 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL16.SetValue("ITEMNO_ACC", "0000000016");
                                                structCURRENCYAMOUNTGL16.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL16.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL16.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL16);
                                            }
                                        }

                                        // 17
                                        if (countDetail == 17)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL17 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL17.SetValue("ITEMNO_ACC", "0000000017"); // Auto no
                                                structAccoungGL17.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL17.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL17.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL17.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL17.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL17.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL17.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL17.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL17.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL17.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL17.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL17.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL17.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL17);

                                                IRfcStructure structCURRENCYAMOUNTGL17 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL17.SetValue("ITEMNO_ACC", "0000000017");
                                                structCURRENCYAMOUNTGL17.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL17.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL17.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL17);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR17 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR17.SetValue("ITEMNO_ACC", "0000000017");
                                                structAccoungAR17.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR17.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR17.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR17.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR17.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR17);

                                                IRfcStructure structCURRENCYAMOUNTGL17 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL17.SetValue("ITEMNO_ACC", "0000000017");
                                                structCURRENCYAMOUNTGL17.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL17.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL17.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL17);
                                            }
                                        }

                                        // 18
                                        if (countDetail == 18)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL18 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL18.SetValue("ITEMNO_ACC", "0000000018"); // Auto no
                                                structAccoungGL18.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL18.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL18.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL18.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL18.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL18.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL18.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL18.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL18.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL18.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL18.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL18.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL18.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL18);

                                                IRfcStructure structCURRENCYAMOUNTGL18 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL18.SetValue("ITEMNO_ACC", "0000000018");
                                                structCURRENCYAMOUNTGL18.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL18.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL18.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL18);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR18 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR18.SetValue("ITEMNO_ACC", "0000000018");
                                                structAccoungAR18.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR18.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR18.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR18.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR18.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR18);

                                                IRfcStructure structCURRENCYAMOUNTGL18 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL18.SetValue("ITEMNO_ACC", "0000000018");
                                                structCURRENCYAMOUNTGL18.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL18.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL18.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL18);
                                            }
                                        }

                                        // 19
                                        if (countDetail == 19)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL19 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL19.SetValue("ITEMNO_ACC", "0000000019"); // Auto no
                                                structAccoungGL19.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL19.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL19.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL19.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL19.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL19.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL19.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL19.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL19.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL19.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL19.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL19.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL19.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL19);

                                                IRfcStructure structCURRENCYAMOUNTGL19 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL19.SetValue("ITEMNO_ACC", "0000000019");
                                                structCURRENCYAMOUNTGL19.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL19.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL19.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL19);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {
                                                IRfcStructure structAccoungAR19 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR19.SetValue("ITEMNO_ACC", "0000000019");
                                                structAccoungAR19.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR19.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR19.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR19.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR19.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR19);

                                                IRfcStructure structCURRENCYAMOUNTGL19 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL19.SetValue("ITEMNO_ACC", "0000000019");
                                                structCURRENCYAMOUNTGL19.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL19.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL19.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL19);
                                            }
                                        }

                                        // 20
                                        if (countDetail == 20)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL20 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL20.SetValue("ITEMNO_ACC", "0000000020"); // Auto no
                                                structAccoungGL20.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL20.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL20.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL20.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL20.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL20.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL20.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL20.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL20.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL20.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL20.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL20.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL20.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL20);

                                                IRfcStructure structCURRENCYAMOUNTGL20 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL20.SetValue("ITEMNO_ACC", "0000000020");
                                                structCURRENCYAMOUNTGL20.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL20.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL20.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL20);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR20 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR20.SetValue("ITEMNO_ACC", "0000000020");
                                                structAccoungAR20.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR20.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR20.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR20.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR20.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR20);

                                                IRfcStructure structCURRENCYAMOUNTGL20 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL20.SetValue("ITEMNO_ACC", "0000000020");
                                                structCURRENCYAMOUNTGL20.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL20.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL20.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL20);
                                            }
                                        }

                                        // 21
                                        if (countDetail == 21)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL21 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL21.SetValue("ITEMNO_ACC", "0000000021"); // Auto no
                                                structAccoungGL21.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL21.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL21.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL21.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL21.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL21.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL21.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL21.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL21.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL21.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL21.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL21.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL21.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL21);

                                                IRfcStructure structCURRENCYAMOUNTGL21 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL21.SetValue("ITEMNO_ACC", "0000000021");
                                                structCURRENCYAMOUNTGL21.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL21.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL21.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL21);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR21 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR21.SetValue("ITEMNO_ACC", "0000000021");
                                                structAccoungAR21.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR21.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR21.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR21.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR21.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR21);

                                                IRfcStructure structCURRENCYAMOUNTGL21 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL21.SetValue("ITEMNO_ACC", "0000000021");
                                                structCURRENCYAMOUNTGL21.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL21.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL21.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL21);
                                            }
                                        }

                                        // 22
                                        if (countDetail == 22)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL22 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL22.SetValue("ITEMNO_ACC", "0000000022"); // Auto no
                                                structAccoungGL22.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL22.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL22.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL22.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL22.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL22.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL22.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL22.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL22.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL22.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL22.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL22.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL22.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL22);

                                                IRfcStructure structCURRENCYAMOUNTGL22 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL22.SetValue("ITEMNO_ACC", "0000000022");
                                                structCURRENCYAMOUNTGL22.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL22.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL22.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL22);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR22 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR22.SetValue("ITEMNO_ACC", "0000000022");
                                                structAccoungAR22.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR22.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR22.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR22.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR22.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR22);

                                                IRfcStructure structCURRENCYAMOUNTGL22 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL22.SetValue("ITEMNO_ACC", "0000000022");
                                                structCURRENCYAMOUNTGL22.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL22.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL22.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL22);
                                            }
                                        }

                                        // 23
                                        if (countDetail == 23)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL23 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL23.SetValue("ITEMNO_ACC", "0000000023"); // Auto no
                                                structAccoungGL23.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL23.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL23.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL23.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL23.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL23.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL23.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL23.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL23.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL23.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL23.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL23.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL23.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL23);

                                                IRfcStructure structCURRENCYAMOUNTGL23 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL23.SetValue("ITEMNO_ACC", "0000000023");
                                                structCURRENCYAMOUNTGL23.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL23.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL23.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL23);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR23 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR23.SetValue("ITEMNO_ACC", "0000000023");
                                                structAccoungAR23.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR23.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR23.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR23.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR23.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR23);

                                                IRfcStructure structCURRENCYAMOUNTGL23 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL23.SetValue("ITEMNO_ACC", "0000000023");
                                                structCURRENCYAMOUNTGL23.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL23.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL23.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL23);
                                            }
                                        }

                                        // 24
                                        if (countDetail == 24)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL24 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL24.SetValue("ITEMNO_ACC", "0000000024"); // Auto no
                                                structAccoungGL24.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL24.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL24.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL24.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL24.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL24.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL24.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL24.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL24.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL24.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL24.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL24.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL24.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL24);

                                                IRfcStructure structCURRENCYAMOUNTGL24 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL24.SetValue("ITEMNO_ACC", "0000000024");
                                                structCURRENCYAMOUNTGL24.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL24.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL24.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL24);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR24 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR24.SetValue("ITEMNO_ACC", "0000000024");
                                                structAccoungAR24.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR24.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR24.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR24.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR24.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR24);

                                                IRfcStructure structCURRENCYAMOUNTGL24 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL24.SetValue("ITEMNO_ACC", "0000000024");
                                                structCURRENCYAMOUNTGL24.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL24.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL24.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL24);
                                            }
                                        }

                                        // 25
                                        if (countDetail == 25)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL25 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL25.SetValue("ITEMNO_ACC", "0000000025"); // Auto no
                                                structAccoungGL25.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL25.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL25.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL25.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL25.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL25.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL25.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL25.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL25.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL25.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL25.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL25.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL25.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL25);

                                                IRfcStructure structCURRENCYAMOUNTGL25 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL25.SetValue("ITEMNO_ACC", "0000000025");
                                                structCURRENCYAMOUNTGL25.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL25.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL25.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL25);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR25 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR25.SetValue("ITEMNO_ACC", "0000000025");
                                                structAccoungAR25.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR25.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR25.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR25.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR25.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR25);

                                                IRfcStructure structCURRENCYAMOUNTGL25 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL25.SetValue("ITEMNO_ACC", "0000000025");
                                                structCURRENCYAMOUNTGL25.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL25.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL25.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL25);
                                            }
                                        }

                                        // 26
                                        if (countDetail == 26)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL26 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL26.SetValue("ITEMNO_ACC", "0000000026"); // Auto no
                                                structAccoungGL26.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL26.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL26.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL26.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL26.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL26.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL26.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL26.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL26.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL26.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL26.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL26.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL26.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL26);

                                                IRfcStructure structCURRENCYAMOUNTGL26 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL26.SetValue("ITEMNO_ACC", "0000000026");
                                                structCURRENCYAMOUNTGL26.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL26.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL26.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL26);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR26 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR26.SetValue("ITEMNO_ACC", "0000000026");
                                                structAccoungAR26.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR26.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR26.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR26.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR26.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR26);

                                                IRfcStructure structCURRENCYAMOUNTGL26 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL26.SetValue("ITEMNO_ACC", "0000000026");
                                                structCURRENCYAMOUNTGL26.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL26.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL26.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL26);
                                            }
                                        }

                                        // 27
                                        if (countDetail == 27)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL27 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL27.SetValue("ITEMNO_ACC", "0000000027"); // Auto no
                                                structAccoungGL27.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL27.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL27.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL27.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL27.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL27.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL27.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL27.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL27.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL27.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL27.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL27.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL27.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL27);

                                                IRfcStructure structCURRENCYAMOUNTGL27 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL27.SetValue("ITEMNO_ACC", "0000000027");
                                                structCURRENCYAMOUNTGL27.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL27.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL27.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL27);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR27 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR27.SetValue("ITEMNO_ACC", "0000000027");
                                                structAccoungAR27.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR27.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR27.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR27.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR27.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR27);

                                                IRfcStructure structCURRENCYAMOUNTGL27 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL27.SetValue("ITEMNO_ACC", "0000000027");
                                                structCURRENCYAMOUNTGL27.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL27.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL27.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL27);
                                            }
                                        }

                                        // 28
                                        if (countDetail == 28)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL28 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL28.SetValue("ITEMNO_ACC", "0000000028"); // Auto no
                                                structAccoungGL28.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL28.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL28.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL28.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL28.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL28.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL28.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL28.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL28.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL28.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL28.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL28.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL28.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL28);

                                                IRfcStructure structCURRENCYAMOUNTGL28 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL28.SetValue("ITEMNO_ACC", "0000000028");
                                                structCURRENCYAMOUNTGL28.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL28.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL28.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL28);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR28 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR28.SetValue("ITEMNO_ACC", "0000000028");
                                                structAccoungAR28.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR28.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR28.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR28.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR28.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR28);

                                                IRfcStructure structCURRENCYAMOUNTGL28 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL28.SetValue("ITEMNO_ACC", "0000000028");
                                                structCURRENCYAMOUNTGL28.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL28.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL28.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL28);
                                            }
                                        }


                                        // 29
                                        if (countDetail == 29)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL29 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL29.SetValue("ITEMNO_ACC", "0000000029"); // Auto no
                                                structAccoungGL29.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL29.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL29.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL29.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL29.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL29.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL29.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL29.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL29.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL29.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL29.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL29.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL29.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL29);

                                                IRfcStructure structCURRENCYAMOUNTGL29 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL29.SetValue("ITEMNO_ACC", "0000000029");
                                                structCURRENCYAMOUNTGL29.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL29.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL29.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL29);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR29 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR29.SetValue("ITEMNO_ACC", "0000000029");
                                                structAccoungAR29.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR29.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR29.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR29.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR29.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR29);

                                                IRfcStructure structCURRENCYAMOUNTGL29 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL29.SetValue("ITEMNO_ACC", "0000000029");
                                                structCURRENCYAMOUNTGL29.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL29.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL29.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL29);
                                            }
                                        }


                                        // 30
                                        if (countDetail == 30)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL30 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL30.SetValue("ITEMNO_ACC", "0000000030"); // Auto no
                                                structAccoungGL30.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL30.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL30.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL30.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL30.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL30.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL30.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL30.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL30.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL30.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL30.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL30.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL30.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL30);

                                                IRfcStructure structCURRENCYAMOUNTGL30 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL30.SetValue("ITEMNO_ACC", "0000000030");
                                                structCURRENCYAMOUNTGL30.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL30.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL30.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL30);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR30 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR30.SetValue("ITEMNO_ACC", "0000000030");
                                                structAccoungAR30.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR30.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR30.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR30.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR30.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR30);

                                                IRfcStructure structCURRENCYAMOUNTGL30 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL30.SetValue("ITEMNO_ACC", "0000000030");
                                                structCURRENCYAMOUNTGL30.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL30.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL30.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL30);
                                            }
                                        }

                                        // 31
                                        if (countDetail == 31)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL31 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL31.SetValue("ITEMNO_ACC", "0000000031"); // Auto no
                                                structAccoungGL31.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL31.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL31.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL31.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL31.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL31.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL31.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL31.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL31.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL31.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL31.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL31.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL31.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL31);

                                                IRfcStructure structCURRENCYAMOUNTGL31 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL31.SetValue("ITEMNO_ACC", "0000000031");
                                                structCURRENCYAMOUNTGL31.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL31.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL31.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL31);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR31 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR31.SetValue("ITEMNO_ACC", "0000000031");
                                                structAccoungAR31.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR31.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR31.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR31.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR31.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR31);

                                                IRfcStructure structCURRENCYAMOUNTGL31 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL31.SetValue("ITEMNO_ACC", "0000000031");
                                                structCURRENCYAMOUNTGL31.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL31.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL31.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL31);
                                            }
                                        }


                                        // 32
                                        if (countDetail == 32)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL32 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL32.SetValue("ITEMNO_ACC", "0000000032"); // Auto no
                                                structAccoungGL32.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL32.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL32.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL32.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL32.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL32.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL32.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL32.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL32.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL32.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL32.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL32.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL32.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL32);

                                                IRfcStructure structCURRENCYAMOUNTGL32 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL32.SetValue("ITEMNO_ACC", "0000000032");
                                                structCURRENCYAMOUNTGL32.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL32.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL32.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL32);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR32 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR32.SetValue("ITEMNO_ACC", "0000000032");
                                                structAccoungAR32.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR32.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR32.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR32.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR32.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR32);

                                                IRfcStructure structCURRENCYAMOUNTGL32 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL32.SetValue("ITEMNO_ACC", "0000000032");
                                                structCURRENCYAMOUNTGL32.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL32.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL32.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL32);
                                            }
                                        }

                                        // 33
                                        if (countDetail == 33)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL33 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL33.SetValue("ITEMNO_ACC", "0000000033"); // Auto no
                                                structAccoungGL33.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL33.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL33.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL33.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL33.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL33.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL33.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL33.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL33.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL33.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL33.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL33.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL33.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL33);

                                                IRfcStructure structCURRENCYAMOUNTGL33 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL33.SetValue("ITEMNO_ACC", "0000000033");
                                                structCURRENCYAMOUNTGL33.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL33.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL33.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL33);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR33 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR33.SetValue("ITEMNO_ACC", "0000000033");
                                                structAccoungAR33.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR33.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR33.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR33.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR33.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR33);

                                                IRfcStructure structCURRENCYAMOUNTGL33 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL33.SetValue("ITEMNO_ACC", "0000000033");
                                                structCURRENCYAMOUNTGL33.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL33.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL33.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL33);
                                            }
                                        }

                                        // 34
                                        if (countDetail == 34)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL34 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL34.SetValue("ITEMNO_ACC", "0000000034"); // Auto no
                                                structAccoungGL34.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL34.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL34.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL34.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL34.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL34.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL34.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL34.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL34.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL34.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL34.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL34.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL34.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL34);

                                                IRfcStructure structCURRENCYAMOUNTGL34 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL34.SetValue("ITEMNO_ACC", "0000000034");
                                                structCURRENCYAMOUNTGL34.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL34.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL34.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL34);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR34 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR34.SetValue("ITEMNO_ACC", "0000000034");
                                                structAccoungAR34.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR34.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR34.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR34.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR34.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR34);

                                                IRfcStructure structCURRENCYAMOUNTGL34 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL34.SetValue("ITEMNO_ACC", "0000000034");
                                                structCURRENCYAMOUNTGL34.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL34.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL34.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL34);
                                            }
                                        }


                                        // 35
                                        if (countDetail == 35)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL35 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL35.SetValue("ITEMNO_ACC", "0000000035"); // Auto no
                                                structAccoungGL35.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL35.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL35.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL35.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL35.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL35.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL35.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL35.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL35.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL35.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL35.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL35.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL35.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL35);

                                                IRfcStructure structCURRENCYAMOUNTGL35 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL35.SetValue("ITEMNO_ACC", "0000000035");
                                                structCURRENCYAMOUNTGL35.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL35.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL35.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL35);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR35 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR35.SetValue("ITEMNO_ACC", "0000000035");
                                                structAccoungAR35.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR35.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR35.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR35.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR35.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR35);

                                                IRfcStructure structCURRENCYAMOUNTGL35 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL35.SetValue("ITEMNO_ACC", "0000000035");
                                                structCURRENCYAMOUNTGL35.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL35.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL35.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL35);
                                            }
                                        }


                                        // 36
                                        if (countDetail == 36)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL36 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL36.SetValue("ITEMNO_ACC", "0000000036"); // Auto no
                                                structAccoungGL36.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL36.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL36.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL36.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL36.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL36.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL36.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL36.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL36.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL36.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL36.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL36.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL36.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL36);

                                                IRfcStructure structCURRENCYAMOUNTGL36 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL36.SetValue("ITEMNO_ACC", "0000000036");
                                                structCURRENCYAMOUNTGL36.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL36.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL36.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL36);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR36 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR36.SetValue("ITEMNO_ACC", "0000000036");
                                                structAccoungAR36.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR36.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR36.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR36.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR36.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR36);

                                                IRfcStructure structCURRENCYAMOUNTGL36 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL36.SetValue("ITEMNO_ACC", "0000000036");
                                                structCURRENCYAMOUNTGL36.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL36.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL36.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL36);
                                            }
                                        }

                                        // 37
                                        if (countDetail == 37)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL37 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL37.SetValue("ITEMNO_ACC", "0000000037"); // Auto no
                                                structAccoungGL37.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL37.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL37.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL37.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL37.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL37.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL37.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL37.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL37.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL37.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL37.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL37.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL37.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL37);

                                                IRfcStructure structCURRENCYAMOUNTGL37 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL37.SetValue("ITEMNO_ACC", "0000000037");
                                                structCURRENCYAMOUNTGL37.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL37.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL37.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL37);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR37 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR37.SetValue("ITEMNO_ACC", "0000000037");
                                                structAccoungAR37.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR37.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR37.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR37.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR37.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR37);

                                                IRfcStructure structCURRENCYAMOUNTGL37 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL37.SetValue("ITEMNO_ACC", "0000000037");
                                                structCURRENCYAMOUNTGL37.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL37.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL37.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL37);
                                            }
                                        }

                                        // 38
                                        if (countDetail == 38)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL38 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL38.SetValue("ITEMNO_ACC", "0000000038"); // Auto no
                                                structAccoungGL38.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL38.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL38.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL38.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL38.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL38.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL38.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL38.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL38.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL38.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL38.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL38.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL38.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL38);

                                                IRfcStructure structCURRENCYAMOUNTGL38 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL38.SetValue("ITEMNO_ACC", "0000000038");
                                                structCURRENCYAMOUNTGL38.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL38.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL38.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL38);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR38 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR38.SetValue("ITEMNO_ACC", "0000000038");
                                                structAccoungAR38.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR38.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR38.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR38.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR38.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR38);

                                                IRfcStructure structCURRENCYAMOUNTGL38 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL38.SetValue("ITEMNO_ACC", "0000000038");
                                                structCURRENCYAMOUNTGL38.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL38.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL38.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL38);
                                            }
                                        }

                                        // 39
                                        if (countDetail == 39)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL39 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL39.SetValue("ITEMNO_ACC", "0000000039"); // Auto no
                                                structAccoungGL39.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL39.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL39.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL39.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL39.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL39.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL39.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL39.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL39.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL39.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL39.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL39.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL39.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL39);

                                                IRfcStructure structCURRENCYAMOUNTGL39 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL39.SetValue("ITEMNO_ACC", "0000000039");
                                                structCURRENCYAMOUNTGL39.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL39.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL39.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL39);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR39 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR39.SetValue("ITEMNO_ACC", "0000000039");
                                                structAccoungAR39.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR39.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR39.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR39.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR39.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR39);

                                                IRfcStructure structCURRENCYAMOUNTGL39 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL39.SetValue("ITEMNO_ACC", "0000000039");
                                                structCURRENCYAMOUNTGL39.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL39.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL39.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL39);
                                            }
                                        }


                                        // 40
                                        if (countDetail == 40)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL40 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL40.SetValue("ITEMNO_ACC", "0000000040"); // Auto no
                                                structAccoungGL40.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL40.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL40.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL40.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL40.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL40.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL40.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL40.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL40.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL40.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL40.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL40.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL40.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL40);

                                                IRfcStructure structCURRENCYAMOUNTGL40 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL40.SetValue("ITEMNO_ACC", "0000000040");
                                                structCURRENCYAMOUNTGL40.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL40.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL40.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL40);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR40 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR40.SetValue("ITEMNO_ACC", "0000000040");
                                                structAccoungAR40.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR40.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR40.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR40.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR40.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR40);

                                                IRfcStructure structCURRENCYAMOUNTGL40 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL40.SetValue("ITEMNO_ACC", "0000000040");
                                                structCURRENCYAMOUNTGL40.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL40.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL40.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL40);
                                            }
                                        }


                                        // 41
                                        if (countDetail == 41)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL41 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL41.SetValue("ITEMNO_ACC", "0000000041"); // Auto no
                                                structAccoungGL41.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL41.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL41.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL41.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL41.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL41.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL41.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL41.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL41.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL41.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL41.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL41.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL41.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL41);

                                                IRfcStructure structCURRENCYAMOUNTGL41 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL41.SetValue("ITEMNO_ACC", "0000000041");
                                                structCURRENCYAMOUNTGL41.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL41.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL41.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL41);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR41 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR41.SetValue("ITEMNO_ACC", "0000000041");
                                                structAccoungAR41.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR41.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR41.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR41.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR41.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR41);

                                                IRfcStructure structCURRENCYAMOUNTGL41 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL41.SetValue("ITEMNO_ACC", "0000000041");
                                                structCURRENCYAMOUNTGL41.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL41.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL41.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL41);
                                            }
                                        }

                                        // 42
                                        if (countDetail == 42)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL42 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL42.SetValue("ITEMNO_ACC", "0000000042"); // Auto no
                                                structAccoungGL42.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL42.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL42.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL42.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL42.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL42.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL42.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL42.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL42.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL42.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL42.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL42.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL42.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL42);

                                                IRfcStructure structCURRENCYAMOUNTGL42 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL42.SetValue("ITEMNO_ACC", "0000000042");
                                                structCURRENCYAMOUNTGL42.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL42.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL42.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL42);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR42 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR42.SetValue("ITEMNO_ACC", "0000000042");
                                                structAccoungAR42.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR42.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR42.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR42.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR42.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR42);

                                                IRfcStructure structCURRENCYAMOUNTGL42 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL42.SetValue("ITEMNO_ACC", "0000000042");
                                                structCURRENCYAMOUNTGL42.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL42.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL42.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL42);
                                            }
                                        }


                                        // 43
                                        if (countDetail == 43)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL43 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL43.SetValue("ITEMNO_ACC", "0000000043"); // Auto no
                                                structAccoungGL43.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL43.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL43.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL43.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL43.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL43.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL43.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL43.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL43.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL43.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL43.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL43.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL43.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL43);

                                                IRfcStructure structCURRENCYAMOUNTGL43 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL43.SetValue("ITEMNO_ACC", "0000000043");
                                                structCURRENCYAMOUNTGL43.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL43.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL43.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL43);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR43 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR43.SetValue("ITEMNO_ACC", "0000000043");
                                                structAccoungAR43.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR43.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR43.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR43.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR43.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR43);

                                                IRfcStructure structCURRENCYAMOUNTGL43 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL43.SetValue("ITEMNO_ACC", "0000000043");
                                                structCURRENCYAMOUNTGL43.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL43.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL43.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL43);
                                            }
                                        }


                                        // 44
                                        if (countDetail == 44)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL44 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL44.SetValue("ITEMNO_ACC", "0000000044"); // Auto no
                                                structAccoungGL44.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL44.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL44.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL44.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL44.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL44.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL44.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL44.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL44.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL44.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL44.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL44.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL44.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL44);

                                                IRfcStructure structCURRENCYAMOUNTGL44 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL44.SetValue("ITEMNO_ACC", "0000000044");
                                                structCURRENCYAMOUNTGL44.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL44.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL44.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL44);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR44 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR44.SetValue("ITEMNO_ACC", "0000000044");
                                                structAccoungAR44.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR44.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR44.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR44.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR44.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR44);

                                                IRfcStructure structCURRENCYAMOUNTGL44 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL44.SetValue("ITEMNO_ACC", "0000000044");
                                                structCURRENCYAMOUNTGL44.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL44.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL44.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL44);
                                            }
                                        }

                                        // 45
                                        if (countDetail == 45)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL45 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL45.SetValue("ITEMNO_ACC", "0000000045"); // Auto no
                                                structAccoungGL45.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL45.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL45.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL45.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL45.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL45.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL45.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL45.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL45.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL45.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL45.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL45.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL45.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL45);

                                                IRfcStructure structCURRENCYAMOUNTGL45 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL45.SetValue("ITEMNO_ACC", "0000000045");
                                                structCURRENCYAMOUNTGL45.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL45.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL45.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL45);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR45 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR45.SetValue("ITEMNO_ACC", "0000000045");
                                                structAccoungAR45.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR45.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR45.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR45.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR45.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR45);

                                                IRfcStructure structCURRENCYAMOUNTGL45 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL45.SetValue("ITEMNO_ACC", "0000000045");
                                                structCURRENCYAMOUNTGL45.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL45.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL45.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL45);
                                            }
                                        }


                                        // 46
                                        if (countDetail == 46)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL46 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL46.SetValue("ITEMNO_ACC", "0000000046"); // Auto no
                                                structAccoungGL46.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL46.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL46.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL46.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL46.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL46.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL46.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL46.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL46.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL46.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL46.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL46.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL46.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL46);

                                                IRfcStructure structCURRENCYAMOUNTGL46 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL46.SetValue("ITEMNO_ACC", "0000000046");
                                                structCURRENCYAMOUNTGL46.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL46.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL46.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL46);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR46 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR46.SetValue("ITEMNO_ACC", "0000000046");
                                                structAccoungAR46.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR46.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR46.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR46.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR46.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR46);

                                                IRfcStructure structCURRENCYAMOUNTGL46 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL46.SetValue("ITEMNO_ACC", "0000000046");
                                                structCURRENCYAMOUNTGL46.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL46.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL46.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL46);
                                            }
                                        }

                                        // 47
                                        if (countDetail == 47)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL47 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL47.SetValue("ITEMNO_ACC", "0000000047"); // Auto no
                                                structAccoungGL47.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL47.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL47.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL47.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL47.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL47.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL47.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL47.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL47.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL47.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL47.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL47.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL47.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL47);

                                                IRfcStructure structCURRENCYAMOUNTGL47 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL47.SetValue("ITEMNO_ACC", "0000000047");
                                                structCURRENCYAMOUNTGL47.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL47.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL47.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL47);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR47 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR47.SetValue("ITEMNO_ACC", "0000000047");
                                                structAccoungAR47.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR47.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR47.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR47.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR47.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR47);

                                                IRfcStructure structCURRENCYAMOUNTGL47 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL47.SetValue("ITEMNO_ACC", "0000000047");
                                                structCURRENCYAMOUNTGL47.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL47.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL47.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL47);
                                            }
                                        }

                                        // 48
                                        if (countDetail == 48)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL48 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL48.SetValue("ITEMNO_ACC", "0000000048"); // Auto no
                                                structAccoungGL48.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL48.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL48.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL48.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL48.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL48.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL48.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL48.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL48.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL48.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL48.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL48.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL48.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL48);

                                                IRfcStructure structCURRENCYAMOUNTGL48 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL48.SetValue("ITEMNO_ACC", "0000000048");
                                                structCURRENCYAMOUNTGL48.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL48.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL48.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL48);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR48 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR48.SetValue("ITEMNO_ACC", "0000000048");
                                                structAccoungAR48.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR48.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR48.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR48.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR48.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR48);

                                                IRfcStructure structCURRENCYAMOUNTGL48 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL48.SetValue("ITEMNO_ACC", "0000000048");
                                                structCURRENCYAMOUNTGL48.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL48.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL48.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL48);
                                            }
                                        }

                                        // 49
                                        if (countDetail == 49)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL49 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL49.SetValue("ITEMNO_ACC", "0000000049"); // Auto no
                                                structAccoungGL49.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL49.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL49.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL49.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL49.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL49.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL49.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL49.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL49.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL49.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL49.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL49.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL49.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL49);

                                                IRfcStructure structCURRENCYAMOUNTGL49 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL49.SetValue("ITEMNO_ACC", "0000000049");
                                                structCURRENCYAMOUNTGL49.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL49.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL49.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL49);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR49 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR49.SetValue("ITEMNO_ACC", "0000000049");
                                                structAccoungAR49.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR49.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR49.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR49.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR49.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR49);

                                                IRfcStructure structCURRENCYAMOUNTGL49 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL49.SetValue("ITEMNO_ACC", "0000000049");
                                                structCURRENCYAMOUNTGL49.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL49.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL49.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL49);
                                            }
                                        }

                                        // 50
                                        if (countDetail == 50)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL50 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL50.SetValue("ITEMNO_ACC", "0000000050"); // Auto no
                                                structAccoungGL50.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL50.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL50.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL50.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL50.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL50.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL50.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL50.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL50.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL50.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL50.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL50.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL50.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL50);

                                                IRfcStructure structCURRENCYAMOUNTGL50 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL50.SetValue("ITEMNO_ACC", "0000000050");
                                                structCURRENCYAMOUNTGL50.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL50.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL50.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL50);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR50 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR50.SetValue("ITEMNO_ACC", "0000000050");
                                                structAccoungAR50.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR50.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR50.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR50.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR50.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR50);

                                                IRfcStructure structCURRENCYAMOUNTGL50 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL50.SetValue("ITEMNO_ACC", "0000000050");
                                                structCURRENCYAMOUNTGL50.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL50.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL50.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL50);
                                            }
                                        }


                                        // 51
                                        if (countDetail == 51)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL51 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL51.SetValue("ITEMNO_ACC", "0000000051"); // Auto no
                                                structAccoungGL51.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL51.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL51.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL51.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL51.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL51.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL51.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL51.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL51.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL51.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL51.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL51.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL51.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL51);

                                                IRfcStructure structCURRENCYAMOUNTGL51 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL51.SetValue("ITEMNO_ACC", "0000000051");
                                                structCURRENCYAMOUNTGL51.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL51.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL51.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL51);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR51 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR51.SetValue("ITEMNO_ACC", "0000000051");
                                                structAccoungAR51.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR51.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR51.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR51.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR51.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR51);

                                                IRfcStructure structCURRENCYAMOUNTGL51 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL51.SetValue("ITEMNO_ACC", "0000000051");
                                                structCURRENCYAMOUNTGL51.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL51.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL51.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL51);
                                            }
                                        }


                                        // 52
                                        if (countDetail == 52)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL52 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL52.SetValue("ITEMNO_ACC", "0000000052"); // Auto no
                                                structAccoungGL52.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL52.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL52.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL52.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL52.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL52.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL52.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL52.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL52.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL52.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL52.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL52.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL52.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL52);

                                                IRfcStructure structCURRENCYAMOUNTGL52 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL52.SetValue("ITEMNO_ACC", "0000000052");
                                                structCURRENCYAMOUNTGL52.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL52.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL52.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL52);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR52 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR52.SetValue("ITEMNO_ACC", "0000000052");
                                                structAccoungAR52.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR52.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR52.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR52.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR52.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR52);

                                                IRfcStructure structCURRENCYAMOUNTGL52 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL52.SetValue("ITEMNO_ACC", "0000000052");
                                                structCURRENCYAMOUNTGL52.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL52.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL52.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL52);
                                            }
                                        }

                                        // 53
                                        if (countDetail == 53)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL53 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL53.SetValue("ITEMNO_ACC", "0000000053"); // Auto no
                                                structAccoungGL53.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL53.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL53.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL53.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL53.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL53.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL53.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL53.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL53.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL53.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL53.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL53.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL53.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL53);

                                                IRfcStructure structCURRENCYAMOUNTGL53 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL53.SetValue("ITEMNO_ACC", "0000000053");
                                                structCURRENCYAMOUNTGL53.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL53.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL53.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL53);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR53 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR53.SetValue("ITEMNO_ACC", "0000000053");
                                                structAccoungAR53.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR53.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR53.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR53.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR53.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR53);

                                                IRfcStructure structCURRENCYAMOUNTGL53 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL53.SetValue("ITEMNO_ACC", "0000000053");
                                                structCURRENCYAMOUNTGL53.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL53.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL53.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL53);
                                            }
                                        }

                                        // 54
                                        if (countDetail == 54)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL54 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL54.SetValue("ITEMNO_ACC", "0000000054"); // Auto no
                                                structAccoungGL54.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL54.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL54.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL54.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL54.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL54.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL54.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL54.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL54.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL54.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL54.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL54.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL54.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL54);

                                                IRfcStructure structCURRENCYAMOUNTGL54 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL54.SetValue("ITEMNO_ACC", "0000000054");
                                                structCURRENCYAMOUNTGL54.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL54.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL54.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL54);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR54 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR54.SetValue("ITEMNO_ACC", "0000000054");
                                                structAccoungAR54.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR54.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR54.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR54.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR54.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR54);

                                                IRfcStructure structCURRENCYAMOUNTGL54 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL54.SetValue("ITEMNO_ACC", "0000000054");
                                                structCURRENCYAMOUNTGL54.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL54.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL54.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL54);
                                            }
                                        }

                                        // 55
                                        if (countDetail == 55)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL55 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL55.SetValue("ITEMNO_ACC", "0000000055"); // Auto no
                                                structAccoungGL55.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL55.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL55.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL55.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL55.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL55.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL55.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL55.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL55.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL55.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL55.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL55.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL55.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL55);

                                                IRfcStructure structCURRENCYAMOUNTGL55 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL55.SetValue("ITEMNO_ACC", "0000000055");
                                                structCURRENCYAMOUNTGL55.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL55.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL55.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL55);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR55 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR55.SetValue("ITEMNO_ACC", "0000000055");
                                                structAccoungAR55.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR55.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR55.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR55.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR55.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR55);

                                                IRfcStructure structCURRENCYAMOUNTGL55 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL55.SetValue("ITEMNO_ACC", "0000000055");
                                                structCURRENCYAMOUNTGL55.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL55.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL55.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL55);
                                            }
                                        }

                                        // 56
                                        if (countDetail == 56)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL56 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL56.SetValue("ITEMNO_ACC", "0000000056"); // Auto no
                                                structAccoungGL56.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL56.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL56.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL56.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL56.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL56.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL56.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL56.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL56.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL56.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL56.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL56.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL56.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL56);

                                                IRfcStructure structCURRENCYAMOUNTGL56 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL56.SetValue("ITEMNO_ACC", "0000000056");
                                                structCURRENCYAMOUNTGL56.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL56.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL56.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL56);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR56 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR56.SetValue("ITEMNO_ACC", "0000000056");
                                                structAccoungAR56.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR56.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR56.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR56.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR56.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR56);

                                                IRfcStructure structCURRENCYAMOUNTGL56 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL56.SetValue("ITEMNO_ACC", "0000000056");
                                                structCURRENCYAMOUNTGL56.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL56.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL56.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL56);
                                            }
                                        }

                                        // 57
                                        if (countDetail == 57)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL57 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL57.SetValue("ITEMNO_ACC", "0000000057"); // Auto no
                                                structAccoungGL57.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL57.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL57.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL57.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL57.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL57.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL57.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL57.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL57.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL57.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL57.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL57.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL57.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL57);

                                                IRfcStructure structCURRENCYAMOUNTGL57 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL57.SetValue("ITEMNO_ACC", "0000000057");
                                                structCURRENCYAMOUNTGL57.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL57.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL57.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL57);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR57 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR57.SetValue("ITEMNO_ACC", "0000000057");
                                                structAccoungAR57.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR57.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR57.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR57.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR57.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR57);

                                                IRfcStructure structCURRENCYAMOUNTGL57 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL57.SetValue("ITEMNO_ACC", "0000000057");
                                                structCURRENCYAMOUNTGL57.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL57.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL57.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL57);
                                            }
                                        }

                                        // 58
                                        if (countDetail == 58)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL58 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL58.SetValue("ITEMNO_ACC", "0000000058"); // Auto no
                                                structAccoungGL58.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL58.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL58.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL58.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL58.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL58.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL58.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL58.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL58.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL58.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL58.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL58.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL58.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL58);

                                                IRfcStructure structCURRENCYAMOUNTGL58 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL58.SetValue("ITEMNO_ACC", "0000000058");
                                                structCURRENCYAMOUNTGL58.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL58.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL58.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL58);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR58 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR58.SetValue("ITEMNO_ACC", "0000000058");
                                                structAccoungAR58.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR58.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR58.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR58.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR58.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR58);

                                                IRfcStructure structCURRENCYAMOUNTGL58 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL58.SetValue("ITEMNO_ACC", "0000000058");
                                                structCURRENCYAMOUNTGL58.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL58.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL58.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL58);
                                            }
                                        }

                                        // 59
                                        if (countDetail == 59)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL59 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL59.SetValue("ITEMNO_ACC", "0000000059"); // Auto no
                                                structAccoungGL59.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL59.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL59.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL59.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL59.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL59.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL59.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL59.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL59.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL59.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL59.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL59.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL59.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL59);

                                                IRfcStructure structCURRENCYAMOUNTGL59 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL59.SetValue("ITEMNO_ACC", "0000000059");
                                                structCURRENCYAMOUNTGL59.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL59.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL59.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL59);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR59 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR59.SetValue("ITEMNO_ACC", "0000000059");
                                                structAccoungAR59.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR59.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR59.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR59.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR59.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR59);

                                                IRfcStructure structCURRENCYAMOUNTGL59 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL59.SetValue("ITEMNO_ACC", "0000000059");
                                                structCURRENCYAMOUNTGL59.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL59.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL59.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL59);
                                            }
                                        }

                                        // 60
                                        if (countDetail == 60)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL60 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL60.SetValue("ITEMNO_ACC", "0000000060"); // Auto no
                                                structAccoungGL60.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL60.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL60.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL60.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL60.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL60.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL60.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL60.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL60.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL60.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL60.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL60.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL60.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL60);

                                                IRfcStructure structCURRENCYAMOUNTGL60 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL60.SetValue("ITEMNO_ACC", "0000000060");
                                                structCURRENCYAMOUNTGL60.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL60.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL60.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL60);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR60 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR60.SetValue("ITEMNO_ACC", "0000000060");
                                                structAccoungAR60.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR60.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR60.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR60.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR60.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR60);

                                                IRfcStructure structCURRENCYAMOUNTGL60 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL60.SetValue("ITEMNO_ACC", "0000000060");
                                                structCURRENCYAMOUNTGL60.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL60.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL60.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL60);
                                            }
                                        }

                                        // 61
                                        if (countDetail == 61)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL61 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL61.SetValue("ITEMNO_ACC", "0000000061"); // Auto no
                                                structAccoungGL61.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL61.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL61.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL61.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL61.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL61.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL61.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL61.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL61.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL61.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL61.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL61.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL61.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL61);

                                                IRfcStructure structCURRENCYAMOUNTGL61 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL61.SetValue("ITEMNO_ACC", "0000000061");
                                                structCURRENCYAMOUNTGL61.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL61.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL61.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL61);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR61 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR61.SetValue("ITEMNO_ACC", "0000000061");
                                                structAccoungAR61.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR61.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR61.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR61.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR61.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR61);

                                                IRfcStructure structCURRENCYAMOUNTGL61 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL61.SetValue("ITEMNO_ACC", "0000000061");
                                                structCURRENCYAMOUNTGL61.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL61.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL61.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL61);
                                            }
                                        }

                                        // 62
                                        if (countDetail == 62)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL62 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL62.SetValue("ITEMNO_ACC", "0000000062"); // Auto no
                                                structAccoungGL62.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL62.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL62.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL62.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL62.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL62.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL62.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL62.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL62.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL62.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL62.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL62.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL62.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL62);

                                                IRfcStructure structCURRENCYAMOUNTGL62 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL62.SetValue("ITEMNO_ACC", "0000000062");
                                                structCURRENCYAMOUNTGL62.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL62.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL62.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL62);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR62 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR62.SetValue("ITEMNO_ACC", "0000000062");
                                                structAccoungAR62.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR62.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR62.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR62.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR62.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR62);

                                                IRfcStructure structCURRENCYAMOUNTGL62 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL62.SetValue("ITEMNO_ACC", "0000000062");
                                                structCURRENCYAMOUNTGL62.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL62.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL62.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL62);
                                            }
                                        }


                                        // 63
                                        if (countDetail == 63)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL63 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL63.SetValue("ITEMNO_ACC", "0000000063"); // Auto no
                                                structAccoungGL63.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL63.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL63.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL63.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL63.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL63.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL63.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL63.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL63.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL63.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL63.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL63.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL63.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL63);

                                                IRfcStructure structCURRENCYAMOUNTGL63 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL63.SetValue("ITEMNO_ACC", "0000000063");
                                                structCURRENCYAMOUNTGL63.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL63.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL63.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL63);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR63 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR63.SetValue("ITEMNO_ACC", "0000000063");
                                                structAccoungAR63.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR63.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR63.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR63.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR63.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR63);

                                                IRfcStructure structCURRENCYAMOUNTGL63 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL63.SetValue("ITEMNO_ACC", "0000000063");
                                                structCURRENCYAMOUNTGL63.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL63.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL63.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL63);
                                            }
                                        }

                                        // 64
                                        if (countDetail == 64)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL64 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL64.SetValue("ITEMNO_ACC", "0000000064"); // Auto no
                                                structAccoungGL64.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL64.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL64.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL64.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL64.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL64.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL64.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL64.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL64.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL64.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL64.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL64.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL64.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL64);

                                                IRfcStructure structCURRENCYAMOUNTGL64 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL64.SetValue("ITEMNO_ACC", "0000000064");
                                                structCURRENCYAMOUNTGL64.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL64.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL64.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL64);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR64 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR64.SetValue("ITEMNO_ACC", "0000000064");
                                                structAccoungAR64.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR64.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR64.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR64.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR64.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR64);

                                                IRfcStructure structCURRENCYAMOUNTGL64 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL64.SetValue("ITEMNO_ACC", "0000000064");
                                                structCURRENCYAMOUNTGL64.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL64.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL64.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL64);
                                            }
                                        }

                                        // 65
                                        if (countDetail == 65)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL65 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL65.SetValue("ITEMNO_ACC", "0000000065"); // Auto no
                                                structAccoungGL65.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL65.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL65.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL65.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL65.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL65.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL65.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL65.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL65.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL65.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL65.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL65.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL65.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL65);

                                                IRfcStructure structCURRENCYAMOUNTGL65 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL65.SetValue("ITEMNO_ACC", "0000000065");
                                                structCURRENCYAMOUNTGL65.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL65.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL65.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL65);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR65 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR65.SetValue("ITEMNO_ACC", "0000000065");
                                                structAccoungAR65.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR65.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR65.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR65.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR65.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR65);

                                                IRfcStructure structCURRENCYAMOUNTGL65 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL65.SetValue("ITEMNO_ACC", "0000000065");
                                                structCURRENCYAMOUNTGL65.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL65.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL65.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL65);
                                            }
                                        }

                                        // 66
                                        if (countDetail == 66)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL66 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL66.SetValue("ITEMNO_ACC", "0000000066"); // Auto no
                                                structAccoungGL66.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL66.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL66.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL66.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL66.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL66.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL66.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL66.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL66.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL66.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL66.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL66.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL66.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL66);

                                                IRfcStructure structCURRENCYAMOUNTGL66 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL66.SetValue("ITEMNO_ACC", "0000000066");
                                                structCURRENCYAMOUNTGL66.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL66.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL66.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL66);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR66 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR66.SetValue("ITEMNO_ACC", "0000000066");
                                                structAccoungAR66.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR66.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR66.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR66.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR66.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR66);

                                                IRfcStructure structCURRENCYAMOUNTGL66 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL66.SetValue("ITEMNO_ACC", "0000000066");
                                                structCURRENCYAMOUNTGL66.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL66.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL66.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL66);
                                            }
                                        }

                                        // 67
                                        if (countDetail == 67)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL67 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL67.SetValue("ITEMNO_ACC", "0000000067"); // Auto no
                                                structAccoungGL67.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL67.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL67.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL67.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL67.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL67.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL67.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL67.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL67.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL67.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL67.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL67.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL67.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL67);

                                                IRfcStructure structCURRENCYAMOUNTGL67 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL67.SetValue("ITEMNO_ACC", "0000000067");
                                                structCURRENCYAMOUNTGL67.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL67.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL67.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL67);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR67 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR67.SetValue("ITEMNO_ACC", "0000000067");
                                                structAccoungAR67.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR67.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR67.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR67.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR67.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR67);

                                                IRfcStructure structCURRENCYAMOUNTGL67 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL67.SetValue("ITEMNO_ACC", "0000000067");
                                                structCURRENCYAMOUNTGL67.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL67.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL67.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL67);
                                            }
                                        }

                                        // 68
                                        if (countDetail == 68)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL68 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL68.SetValue("ITEMNO_ACC", "0000000068"); // Auto no
                                                structAccoungGL68.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL68.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL68.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL68.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL68.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL68.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL68.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL68.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL68.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL68.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL68.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL68.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL68.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL68);

                                                IRfcStructure structCURRENCYAMOUNTGL68 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL68.SetValue("ITEMNO_ACC", "0000000068");
                                                structCURRENCYAMOUNTGL68.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL68.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL68.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL68);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR68 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR68.SetValue("ITEMNO_ACC", "0000000068");
                                                structAccoungAR68.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR68.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR68.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR68.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR68.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR68);

                                                IRfcStructure structCURRENCYAMOUNTGL68 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL68.SetValue("ITEMNO_ACC", "0000000068");
                                                structCURRENCYAMOUNTGL68.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL68.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL68.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL68);
                                            }
                                        }

                                        // 69
                                        if (countDetail == 69)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL69 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL69.SetValue("ITEMNO_ACC", "0000000069"); // Auto no
                                                structAccoungGL69.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL69.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL69.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL69.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL69.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL69.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL69.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL69.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL69.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL69.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL69.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL69.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL69.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL69);

                                                IRfcStructure structCURRENCYAMOUNTGL69 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL69.SetValue("ITEMNO_ACC", "0000000069");
                                                structCURRENCYAMOUNTGL69.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL69.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL69.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL69);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR69 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR69.SetValue("ITEMNO_ACC", "0000000069");
                                                structAccoungAR69.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR69.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR69.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR69.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR69.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR69);

                                                IRfcStructure structCURRENCYAMOUNTGL69 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL69.SetValue("ITEMNO_ACC", "0000000069");
                                                structCURRENCYAMOUNTGL69.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL69.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL69.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL69);
                                            }
                                        }

                                        // 70
                                        if (countDetail == 70)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL70 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL70.SetValue("ITEMNO_ACC", "0000000070"); // Auto no
                                                structAccoungGL70.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL70.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL70.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL70.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL70.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL70.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL70.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL70.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL70.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL70.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL70.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL70.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL70.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL70);

                                                IRfcStructure structCURRENCYAMOUNTGL70 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL70.SetValue("ITEMNO_ACC", "0000000070");
                                                structCURRENCYAMOUNTGL70.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL70.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL70.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL70);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR70 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR70.SetValue("ITEMNO_ACC", "0000000070");
                                                structAccoungAR70.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR70.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR70.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR70.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR70.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR70);

                                                IRfcStructure structCURRENCYAMOUNTGL70 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL70.SetValue("ITEMNO_ACC", "0000000070");
                                                structCURRENCYAMOUNTGL70.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL70.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL70.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL70);
                                            }
                                        }

                                        // 71
                                        if (countDetail == 71)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL71 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL71.SetValue("ITEMNO_ACC", "0000000071"); // Auto no
                                                structAccoungGL71.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL71.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL71.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL71.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL71.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL71.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL71.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL71.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL71.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL71.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL71.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL71.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL71.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL71);

                                                IRfcStructure structCURRENCYAMOUNTGL71 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL71.SetValue("ITEMNO_ACC", "0000000071");
                                                structCURRENCYAMOUNTGL71.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL71.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL71.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL71);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR71 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR71.SetValue("ITEMNO_ACC", "0000000071");
                                                structAccoungAR71.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR71.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR71.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR71.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR71.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR71);

                                                IRfcStructure structCURRENCYAMOUNTGL71 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL71.SetValue("ITEMNO_ACC", "0000000071");
                                                structCURRENCYAMOUNTGL71.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL71.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL71.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL71);
                                            }
                                        }

                                        // 72
                                        if (countDetail == 72)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL72 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL72.SetValue("ITEMNO_ACC", "0000000072"); // Auto no
                                                structAccoungGL72.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL72.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL72.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL72.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL72.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL72.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL72.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL72.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL72.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL72.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL72.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL72.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL72.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL72);

                                                IRfcStructure structCURRENCYAMOUNTGL72 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL72.SetValue("ITEMNO_ACC", "0000000072");
                                                structCURRENCYAMOUNTGL72.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL72.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL72.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL72);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR72 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR72.SetValue("ITEMNO_ACC", "0000000072");
                                                structAccoungAR72.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR72.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR72.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR72.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR72.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR72);

                                                IRfcStructure structCURRENCYAMOUNTGL72 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL72.SetValue("ITEMNO_ACC", "0000000072");
                                                structCURRENCYAMOUNTGL72.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL72.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL72.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL72);
                                            }
                                        }

                                        // 73
                                        if (countDetail == 73)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL73 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL73.SetValue("ITEMNO_ACC", "0000000073"); // Auto no
                                                structAccoungGL73.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL73.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL73.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL73.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL73.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL73.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL73.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL73.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL73.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL73.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL73.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL73.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL73.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL73);

                                                IRfcStructure structCURRENCYAMOUNTGL73 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL73.SetValue("ITEMNO_ACC", "0000000073");
                                                structCURRENCYAMOUNTGL73.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL73.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL73.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL73);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR73 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR73.SetValue("ITEMNO_ACC", "0000000073");
                                                structAccoungAR73.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR73.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR73.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR73.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR73.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR73);

                                                IRfcStructure structCURRENCYAMOUNTGL73 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL73.SetValue("ITEMNO_ACC", "0000000073");
                                                structCURRENCYAMOUNTGL73.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL73.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL73.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL73);
                                            }
                                        }

                                        // 74
                                        if (countDetail == 74)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL74 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL74.SetValue("ITEMNO_ACC", "0000000074"); // Auto no
                                                structAccoungGL74.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL74.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL74.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL74.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL74.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL74.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL74.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL74.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL74.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL74.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL74.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL74.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL74.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL74);

                                                IRfcStructure structCURRENCYAMOUNTGL74 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL74.SetValue("ITEMNO_ACC", "0000000074");
                                                structCURRENCYAMOUNTGL74.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL74.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL74.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL74);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR74 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR74.SetValue("ITEMNO_ACC", "0000000074");
                                                structAccoungAR74.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR74.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR74.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR74.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR74.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR74);

                                                IRfcStructure structCURRENCYAMOUNTGL74 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL74.SetValue("ITEMNO_ACC", "0000000074");
                                                structCURRENCYAMOUNTGL74.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL74.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL74.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL74);
                                            }
                                        }

                                        // 75
                                        if (countDetail == 75)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL75 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL75.SetValue("ITEMNO_ACC", "0000000075"); // Auto no
                                                structAccoungGL75.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL75.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL75.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL75.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL75.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL75.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL75.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL75.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL75.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL75.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL75.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL75.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL75.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL75);

                                                IRfcStructure structCURRENCYAMOUNTGL75 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL75.SetValue("ITEMNO_ACC", "0000000075");
                                                structCURRENCYAMOUNTGL75.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL75.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL75.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL75);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR75 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR75.SetValue("ITEMNO_ACC", "0000000075");
                                                structAccoungAR75.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR75.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR75.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR75.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR75.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR75);

                                                IRfcStructure structCURRENCYAMOUNTGL75 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL75.SetValue("ITEMNO_ACC", "0000000075");
                                                structCURRENCYAMOUNTGL75.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL75.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL75.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL75);
                                            }
                                        }

                                        // 76
                                        if (countDetail == 76)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL76 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL76.SetValue("ITEMNO_ACC", "0000000076"); // Auto no
                                                structAccoungGL76.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL76.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL76.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL76.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL76.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL76.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL76.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL76.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL76.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL76.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL76.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL76.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL76.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL76);

                                                IRfcStructure structCURRENCYAMOUNTGL76 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL76.SetValue("ITEMNO_ACC", "0000000076");
                                                structCURRENCYAMOUNTGL76.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL76.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL76.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL76);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR76 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR76.SetValue("ITEMNO_ACC", "0000000076");
                                                structAccoungAR76.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR76.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR76.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR76.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR76.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR76);

                                                IRfcStructure structCURRENCYAMOUNTGL76 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL76.SetValue("ITEMNO_ACC", "0000000076");
                                                structCURRENCYAMOUNTGL76.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL76.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL76.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL76);
                                            }
                                        }

                                        // 77
                                        if (countDetail == 77)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL77 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL77.SetValue("ITEMNO_ACC", "0000000077"); // Auto no
                                                structAccoungGL77.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL77.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL77.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL77.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL77.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL77.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL77.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL77.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL77.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL77.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL77.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL77.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL77.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL77);

                                                IRfcStructure structCURRENCYAMOUNTGL77 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL77.SetValue("ITEMNO_ACC", "0000000077");
                                                structCURRENCYAMOUNTGL77.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL77.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL77.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL77);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR77 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR77.SetValue("ITEMNO_ACC", "0000000077");
                                                structAccoungAR77.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR77.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR77.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR77.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR77.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR77);

                                                IRfcStructure structCURRENCYAMOUNTGL77 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL77.SetValue("ITEMNO_ACC", "0000000077");
                                                structCURRENCYAMOUNTGL77.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL77.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL77.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL77);
                                            }
                                        }

                                        // 78
                                        if (countDetail == 78)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL78 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL78.SetValue("ITEMNO_ACC", "0000000078"); // Auto no
                                                structAccoungGL78.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL78.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL78.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL78.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL78.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL78.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL78.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL78.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL78.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL78.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL78.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL78.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL78.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL78);

                                                IRfcStructure structCURRENCYAMOUNTGL78 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL78.SetValue("ITEMNO_ACC", "0000000078");
                                                structCURRENCYAMOUNTGL78.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL78.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL78.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL78);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR78 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR78.SetValue("ITEMNO_ACC", "0000000078");
                                                structAccoungAR78.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR78.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR78.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR78.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR78.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR78);

                                                IRfcStructure structCURRENCYAMOUNTGL78 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL78.SetValue("ITEMNO_ACC", "0000000078");
                                                structCURRENCYAMOUNTGL78.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL78.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL78.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL78);
                                            }
                                        }

                                        // 79
                                        if (countDetail == 79)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL79 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL79.SetValue("ITEMNO_ACC", "0000000079"); // Auto no
                                                structAccoungGL79.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL79.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL79.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL79.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL79.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL79.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL79.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL79.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL79.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL79.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL79.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL79.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL79.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL79);

                                                IRfcStructure structCURRENCYAMOUNTGL79 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL79.SetValue("ITEMNO_ACC", "0000000079");
                                                structCURRENCYAMOUNTGL79.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL79.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL79.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL79);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR79 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR79.SetValue("ITEMNO_ACC", "0000000079");
                                                structAccoungAR79.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR79.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR79.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR79.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR79.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR79);

                                                IRfcStructure structCURRENCYAMOUNTGL79 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL79.SetValue("ITEMNO_ACC", "0000000079");
                                                structCURRENCYAMOUNTGL79.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL79.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL79.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL79);
                                            }
                                        }

                                        // 80
                                        if (countDetail == 80)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL80 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL80.SetValue("ITEMNO_ACC", "0000000080"); // Auto no
                                                structAccoungGL80.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL80.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL80.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL80.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL80.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL80.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL80.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL80.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL80.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL80.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL80.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL80.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL80.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL80);

                                                IRfcStructure structCURRENCYAMOUNTGL80 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL80.SetValue("ITEMNO_ACC", "0000000080");
                                                structCURRENCYAMOUNTGL80.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL80.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL80.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL80);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR80 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR80.SetValue("ITEMNO_ACC", "0000000080");
                                                structAccoungAR80.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR80.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR80.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR80.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR80.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR80);

                                                IRfcStructure structCURRENCYAMOUNTGL80 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL80.SetValue("ITEMNO_ACC", "0000000080");
                                                structCURRENCYAMOUNTGL80.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL80.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL80.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL80);
                                            }
                                        }

                                        // 81
                                        if (countDetail == 81)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL81 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL81.SetValue("ITEMNO_ACC", "0000000081"); // Auto no
                                                structAccoungGL81.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL81.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL81.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL81.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL81.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL81.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL81.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL81.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL81.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL81.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL81.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL81.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL81.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL81);

                                                IRfcStructure structCURRENCYAMOUNTGL81 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL81.SetValue("ITEMNO_ACC", "0000000081");
                                                structCURRENCYAMOUNTGL81.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL81.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL81.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL81);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR81 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR81.SetValue("ITEMNO_ACC", "0000000081");
                                                structAccoungAR81.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR81.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR81.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR81.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR81.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR81);

                                                IRfcStructure structCURRENCYAMOUNTGL81 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL81.SetValue("ITEMNO_ACC", "0000000081");
                                                structCURRENCYAMOUNTGL81.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL81.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL81.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL81);
                                            }
                                        }

                                        // 82
                                        if (countDetail == 82)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL82 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL82.SetValue("ITEMNO_ACC", "0000000082"); // Auto no
                                                structAccoungGL82.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL82.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL82.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL82.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL82.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL82.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL82.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL82.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL82.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL82.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL82.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL82.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL82.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL82);

                                                IRfcStructure structCURRENCYAMOUNTGL82 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL82.SetValue("ITEMNO_ACC", "0000000082");
                                                structCURRENCYAMOUNTGL82.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL82.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL82.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL82);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR82 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR82.SetValue("ITEMNO_ACC", "0000000082");
                                                structAccoungAR82.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR82.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR82.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR82.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR82.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR82);

                                                IRfcStructure structCURRENCYAMOUNTGL82 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL82.SetValue("ITEMNO_ACC", "0000000082");
                                                structCURRENCYAMOUNTGL82.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL82.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL82.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL82);
                                            }
                                        }

                                        // 83
                                        if (countDetail == 83)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL83 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL83.SetValue("ITEMNO_ACC", "0000000083"); // Auto no
                                                structAccoungGL83.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL83.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL83.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL83.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL83.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL83.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL83.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL83.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL83.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL83.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL83.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL83.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL83.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL83);

                                                IRfcStructure structCURRENCYAMOUNTGL83 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL83.SetValue("ITEMNO_ACC", "0000000083");
                                                structCURRENCYAMOUNTGL83.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL83.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL83.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL83);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR83 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR83.SetValue("ITEMNO_ACC", "0000000083");
                                                structAccoungAR83.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR83.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR83.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR83.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR83.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR83);

                                                IRfcStructure structCURRENCYAMOUNTGL83 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL83.SetValue("ITEMNO_ACC", "0000000083");
                                                structCURRENCYAMOUNTGL83.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL83.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL83.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL83);
                                            }
                                        }

                                        // 84
                                        if (countDetail == 84)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL84 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL84.SetValue("ITEMNO_ACC", "0000000084"); // Auto no
                                                structAccoungGL84.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL84.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL84.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL84.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL84.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL84.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL84.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL84.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL84.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL84.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL84.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL84.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL84.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL84);

                                                IRfcStructure structCURRENCYAMOUNTGL84 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL84.SetValue("ITEMNO_ACC", "0000000084");
                                                structCURRENCYAMOUNTGL84.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL84.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL84.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL84);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR84 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR84.SetValue("ITEMNO_ACC", "0000000084");
                                                structAccoungAR84.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR84.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR84.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR84.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR84.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR84);

                                                IRfcStructure structCURRENCYAMOUNTGL84 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL84.SetValue("ITEMNO_ACC", "0000000084");
                                                structCURRENCYAMOUNTGL84.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL84.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL84.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL84);
                                            }
                                        }

                                        // 85
                                        if (countDetail == 85)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL85 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL85.SetValue("ITEMNO_ACC", "0000000085"); // Auto no
                                                structAccoungGL85.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL85.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL85.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL85.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL85.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL85.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL85.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL85.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL85.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL85.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL85.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL85.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL85.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL85);

                                                IRfcStructure structCURRENCYAMOUNTGL85 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL85.SetValue("ITEMNO_ACC", "0000000085");
                                                structCURRENCYAMOUNTGL85.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL85.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL85.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL85);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR85 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR85.SetValue("ITEMNO_ACC", "0000000085");
                                                structAccoungAR85.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR85.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR85.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR85.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR85.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR85);

                                                IRfcStructure structCURRENCYAMOUNTGL85 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL85.SetValue("ITEMNO_ACC", "0000000085");
                                                structCURRENCYAMOUNTGL85.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL85.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL85.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL85);
                                            }
                                        }


                                        // 86
                                        if (countDetail == 86)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL86 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL86.SetValue("ITEMNO_ACC", "0000000086"); // Auto no
                                                structAccoungGL86.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL86.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL86.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL86.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL86.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL86.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL86.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL86.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL86.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL86.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL86.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL86.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL86.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL86);

                                                IRfcStructure structCURRENCYAMOUNTGL86 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL86.SetValue("ITEMNO_ACC", "0000000086");
                                                structCURRENCYAMOUNTGL86.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL86.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL86.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL86);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR86 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR86.SetValue("ITEMNO_ACC", "0000000086");
                                                structAccoungAR86.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR86.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR86.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR86.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR86.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR86);

                                                IRfcStructure structCURRENCYAMOUNTGL86 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL86.SetValue("ITEMNO_ACC", "0000000086");
                                                structCURRENCYAMOUNTGL86.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL86.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL86.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL86);
                                            }
                                        }

                                        // 87
                                        if (countDetail == 87)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL87 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL87.SetValue("ITEMNO_ACC", "0000000087"); // Auto no
                                                structAccoungGL87.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL87.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL87.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL87.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL87.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL87.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL87.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL87.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL87.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL87.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL87.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL87.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL87.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL87);

                                                IRfcStructure structCURRENCYAMOUNTGL87 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL87.SetValue("ITEMNO_ACC", "0000000087");
                                                structCURRENCYAMOUNTGL87.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL87.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL87.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL87);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR87 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR87.SetValue("ITEMNO_ACC", "0000000087");
                                                structAccoungAR87.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR87.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR87.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR87.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR87.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR87);

                                                IRfcStructure structCURRENCYAMOUNTGL87 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL87.SetValue("ITEMNO_ACC", "0000000087");
                                                structCURRENCYAMOUNTGL87.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL87.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL87.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL87);
                                            }
                                        }


                                        // 88
                                        if (countDetail == 88)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL88 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL88.SetValue("ITEMNO_ACC", "0000000088"); // Auto no
                                                structAccoungGL88.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL88.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL88.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL88.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL88.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL88.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL88.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL88.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL88.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL88.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL88.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL88.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL88.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL88);

                                                IRfcStructure structCURRENCYAMOUNTGL88 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL88.SetValue("ITEMNO_ACC", "0000000088");
                                                structCURRENCYAMOUNTGL88.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL88.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL88.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL88);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR88 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR88.SetValue("ITEMNO_ACC", "0000000088");
                                                structAccoungAR88.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR88.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR88.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR88.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR88.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR88);

                                                IRfcStructure structCURRENCYAMOUNTGL88 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL88.SetValue("ITEMNO_ACC", "0000000088");
                                                structCURRENCYAMOUNTGL88.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL88.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL88.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL88);
                                            }
                                        }


                                        // 89
                                        if (countDetail == 89)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL89 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL89.SetValue("ITEMNO_ACC", "0000000089"); // Auto no
                                                structAccoungGL89.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL89.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL89.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL89.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL89.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL89.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL89.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL89.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL89.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL89.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL89.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL89.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL89.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL89);

                                                IRfcStructure structCURRENCYAMOUNTGL89 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL89.SetValue("ITEMNO_ACC", "0000000089");
                                                structCURRENCYAMOUNTGL89.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL89.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL89.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL89);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR89 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR89.SetValue("ITEMNO_ACC", "0000000089");
                                                structAccoungAR89.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR89.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR89.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR89.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR89.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR89);

                                                IRfcStructure structCURRENCYAMOUNTGL89 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL89.SetValue("ITEMNO_ACC", "0000000089");
                                                structCURRENCYAMOUNTGL89.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL89.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL89.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL89);
                                            }
                                        }

                                        // 90
                                        if (countDetail == 90)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL90 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL90.SetValue("ITEMNO_ACC", "0000000090"); // Auto no
                                                structAccoungGL90.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL90.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL90.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL90.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL90.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL90.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL90.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL90.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL90.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL90.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL90.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL90.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL90.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL90);

                                                IRfcStructure structCURRENCYAMOUNTGL90 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL90.SetValue("ITEMNO_ACC", "0000000090");
                                                structCURRENCYAMOUNTGL90.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL90.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL90.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL90);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR90 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR90.SetValue("ITEMNO_ACC", "0000000090");
                                                structAccoungAR90.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR90.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR90.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR90.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR90.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR90);

                                                IRfcStructure structCURRENCYAMOUNTGL90 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL90.SetValue("ITEMNO_ACC", "0000000090");
                                                structCURRENCYAMOUNTGL90.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL90.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL90.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL90);
                                            }
                                        }

                                        // 91
                                        if (countDetail == 91)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL91 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL91.SetValue("ITEMNO_ACC", "0000000091"); // Auto no
                                                structAccoungGL91.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL91.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL91.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL91.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL91.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL91.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL91.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL91.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL91.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL91.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL91.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL91.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL91.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL91);

                                                IRfcStructure structCURRENCYAMOUNTGL91 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL91.SetValue("ITEMNO_ACC", "0000000091");
                                                structCURRENCYAMOUNTGL91.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL91.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL91.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL91);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR91 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR91.SetValue("ITEMNO_ACC", "0000000091");
                                                structAccoungAR91.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR91.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR91.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR91.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR91.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR91);

                                                IRfcStructure structCURRENCYAMOUNTGL91 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL91.SetValue("ITEMNO_ACC", "0000000091");
                                                structCURRENCYAMOUNTGL91.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL91.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL91.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL91);
                                            }
                                        }

                                        // 92
                                        if (countDetail == 92)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL92 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL92.SetValue("ITEMNO_ACC", "0000000092"); // Auto no
                                                structAccoungGL92.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL92.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL92.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL92.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL92.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL92.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL92.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL92.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL92.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL92.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL92.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL92.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL92.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL92);

                                                IRfcStructure structCURRENCYAMOUNTGL92 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL92.SetValue("ITEMNO_ACC", "0000000092");
                                                structCURRENCYAMOUNTGL92.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL92.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL92.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL92);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR92 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR92.SetValue("ITEMNO_ACC", "0000000092");
                                                structAccoungAR92.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR92.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR92.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR92.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR92.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR92);

                                                IRfcStructure structCURRENCYAMOUNTGL92 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL92.SetValue("ITEMNO_ACC", "0000000092");
                                                structCURRENCYAMOUNTGL92.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL92.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL92.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL92);
                                            }
                                        }

                                        // 93
                                        if (countDetail == 93)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL93 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL93.SetValue("ITEMNO_ACC", "0000000093"); // Auto no
                                                structAccoungGL93.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL93.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL93.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL93.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL93.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL93.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL93.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL93.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL93.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL93.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL93.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL93.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL93.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL93);

                                                IRfcStructure structCURRENCYAMOUNTGL93 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL93.SetValue("ITEMNO_ACC", "0000000093");
                                                structCURRENCYAMOUNTGL93.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL93.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL93.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL93);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR93 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR93.SetValue("ITEMNO_ACC", "0000000093");
                                                structAccoungAR93.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR93.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR93.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR93.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR93.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR93);

                                                IRfcStructure structCURRENCYAMOUNTGL93 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL93.SetValue("ITEMNO_ACC", "0000000093");
                                                structCURRENCYAMOUNTGL93.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL93.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL93.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL93);
                                            }
                                        }

                                        // 94
                                        if (countDetail == 94)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL94 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL94.SetValue("ITEMNO_ACC", "0000000094"); // Auto no
                                                structAccoungGL94.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL94.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL94.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL94.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL94.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL94.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL94.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL94.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL94.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL94.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL94.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL94.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL94.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL94);

                                                IRfcStructure structCURRENCYAMOUNTGL94 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL94.SetValue("ITEMNO_ACC", "0000000094");
                                                structCURRENCYAMOUNTGL94.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL94.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL94.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL94);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR94 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR94.SetValue("ITEMNO_ACC", "0000000094");
                                                structAccoungAR94.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR94.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR94.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR94.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR94.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR94);

                                                IRfcStructure structCURRENCYAMOUNTGL94 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL94.SetValue("ITEMNO_ACC", "0000000094");
                                                structCURRENCYAMOUNTGL94.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL94.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL94.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL94);
                                            }
                                        }

                                        // 95
                                        if (countDetail == 95)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL95 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL95.SetValue("ITEMNO_ACC", "0000000095"); // Auto no
                                                structAccoungGL95.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL95.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL95.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL95.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL95.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL95.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL95.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL95.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL95.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL95.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL95.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL95.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL95.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL95);

                                                IRfcStructure structCURRENCYAMOUNTGL95 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL95.SetValue("ITEMNO_ACC", "0000000095");
                                                structCURRENCYAMOUNTGL95.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL95.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL95.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL95);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR95 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR95.SetValue("ITEMNO_ACC", "0000000095");
                                                structAccoungAR95.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR95.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR95.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR95.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR95.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR95);

                                                IRfcStructure structCURRENCYAMOUNTGL95 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL95.SetValue("ITEMNO_ACC", "0000000095");
                                                structCURRENCYAMOUNTGL95.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL95.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL95.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL95);
                                            }
                                        }

                                        // 96
                                        if (countDetail == 96)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL96 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL96.SetValue("ITEMNO_ACC", "0000000096"); // Auto no
                                                structAccoungGL96.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL96.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL96.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL96.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL96.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL96.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL96.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL96.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL96.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL96.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL96.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL96.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL96.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL96);

                                                IRfcStructure structCURRENCYAMOUNTGL96 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL96.SetValue("ITEMNO_ACC", "0000000096");
                                                structCURRENCYAMOUNTGL96.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL96.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL96.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL96);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR96 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR96.SetValue("ITEMNO_ACC", "0000000096");
                                                structAccoungAR96.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR96.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR96.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR96.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR96.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR96);

                                                IRfcStructure structCURRENCYAMOUNTGL96 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL96.SetValue("ITEMNO_ACC", "0000000096");
                                                structCURRENCYAMOUNTGL96.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL96.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL96.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL96);
                                            }
                                        }

                                        // 97
                                        if (countDetail == 97)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL97 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL97.SetValue("ITEMNO_ACC", "0000000097"); // Auto no
                                                structAccoungGL97.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL97.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL97.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL97.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL97.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL97.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL97.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL97.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL97.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL97.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL97.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL97.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL97.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL97);

                                                IRfcStructure structCURRENCYAMOUNTGL97 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL97.SetValue("ITEMNO_ACC", "0000000097");
                                                structCURRENCYAMOUNTGL97.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL97.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL97.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL97);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR97 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR97.SetValue("ITEMNO_ACC", "0000000097");
                                                structAccoungAR97.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR97.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR97.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR97.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR97.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR97);

                                                IRfcStructure structCURRENCYAMOUNTGL97 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL97.SetValue("ITEMNO_ACC", "0000000097");
                                                structCURRENCYAMOUNTGL97.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL97.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL97.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL97);
                                            }
                                        }

                                        // 98
                                        if (countDetail == 98)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL98 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL98.SetValue("ITEMNO_ACC", "0000000098"); // Auto no
                                                structAccoungGL98.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL98.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL98.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL98.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL98.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL98.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL98.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL98.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL98.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL98.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL98.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL98.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL98.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL98);

                                                IRfcStructure structCURRENCYAMOUNTGL98 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL98.SetValue("ITEMNO_ACC", "0000000098");
                                                structCURRENCYAMOUNTGL98.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL98.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL98.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL98);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR98 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR98.SetValue("ITEMNO_ACC", "0000000098");
                                                structAccoungAR98.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR98.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR98.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR98.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR98.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR98);

                                                IRfcStructure structCURRENCYAMOUNTGL98 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL98.SetValue("ITEMNO_ACC", "0000000098");
                                                structCURRENCYAMOUNTGL98.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL98.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL98.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL98);
                                            }
                                        }

                                        // 99
                                        if (countDetail == 99)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL99 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL99.SetValue("ITEMNO_ACC", "0000000099"); // Auto no
                                                structAccoungGL99.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL99.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL99.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL99.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL99.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL99.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL99.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL99.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL99.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL99.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL99.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL99.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL99.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL99);

                                                IRfcStructure structCURRENCYAMOUNTGL99 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL99.SetValue("ITEMNO_ACC", "0000000099");
                                                structCURRENCYAMOUNTGL99.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL99.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL99.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL99);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR99 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR99.SetValue("ITEMNO_ACC", "0000000099");
                                                structAccoungAR99.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR99.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR99.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR99.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR99.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR99);

                                                IRfcStructure structCURRENCYAMOUNTGL99 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL99.SetValue("ITEMNO_ACC", "0000000099");
                                                structCURRENCYAMOUNTGL99.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL99.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL99.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL99);
                                            }
                                        }

                                        // 100
                                        if (countDetail == 100)
                                        {
                                            if (rsDetail.ACC_TYPE == "1")
                                            {
                                                IRfcStructure structAccoungGL100 = repo.GetStructureMetadata("ZBAPI_ACCOUNTGL2").CreateStructure();

                                                structAccoungGL100.SetValue("ITEMNO_ACC", "0000000100"); // Auto no
                                                structAccoungGL100.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT); // no GL
                                                structAccoungGL100.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT); // Description di Level Journal Detail
                                                structAccoungGL100.SetValue("COMP_CODE", rsDetail.COMP_CODE); // hardcode
                                                structAccoungGL100.SetValue("BUS_AREA", rsDetail.BUS_AREA); // hardcode
                                                structAccoungGL100.SetValue("FIS_PERIOD", rsDetail.FIS_PERIOD);
                                                structAccoungGL100.SetValue("FISC_YEAR", rsDetail.FISC_YEAR);
                                                structAccoungGL100.SetValue("PSTNG_DATE", rsDetail.PSTNG_DATE);
                                                structAccoungGL100.SetValue("VALUE_DATE", rsDetail.PSTNG_DATE);

                                                if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "5")
                                                {
                                                    structAccoungGL100.SetValue("ORDERID", rsDetail.ORDERID);
                                                    structAccoungGL100.SetValue("PROFIT_CTR", rsDetail.PROFIT_CTR);
                                                }
                                                else if (rsDetail.GL_ACCOUNT.Substring(0, 1) == "6")
                                                {
                                                    structAccoungGL100.SetValue("COSTCENTER", rsDetail.COSTCENTER);
                                                }
                                                else
                                                {
                                                    structAccoungGL100.SetValue("PROFIT_CTR", "");
                                                    structAccoungGL100.SetValue("COSTCENTER", "");
                                                }

                                                tableAccountGL.Append(structAccoungGL100);

                                                IRfcStructure structCURRENCYAMOUNTGL100 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL100.SetValue("ITEMNO_ACC", "0000000100");
                                                structCURRENCYAMOUNTGL100.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL100.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL100.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL100);

                                            }

                                            if (rsDetail.ACC_TYPE == "2")
                                            {

                                                IRfcStructure structAccoungAR100 = repo.GetStructureMetadata("ZBAPI_ACCOUNTRECEIVABLE2").CreateStructure();

                                                structAccoungAR100.SetValue("ITEMNO_ACC", "0000000100");
                                                structAccoungAR100.SetValue("CUSTOMER", rsDetail.CUSTOMER);
                                                structAccoungAR100.SetValue("GL_ACCOUNT", rsDetail.GL_ACCOUNT);
                                                structAccoungAR100.SetValue("COMP_CODE", rsDetail.COMP_CODE);
                                                structAccoungAR100.SetValue("BUS_AREA", rsDetail.BUS_AREA);
                                                structAccoungAR100.SetValue("ITEM_TEXT", rsDetail.ITEM_TEXT);

                                                tableAccountReceivable.Append(structAccoungAR100);

                                                IRfcStructure structCURRENCYAMOUNTGL100 = repo.GetStructureMetadata("ZBAPI_CURRENCYAMOUNT2").CreateStructure();

                                                structCURRENCYAMOUNTGL100.SetValue("ITEMNO_ACC", "0000000100");
                                                structCURRENCYAMOUNTGL100.SetValue("CURRENCY", rsDetail.CURRENCY);
                                                structCURRENCYAMOUNTGL100.SetValue("AMT_DOCCUR", rsDetail.AMT_DOCCUR);
                                                structCURRENCYAMOUNTGL100.SetValue("DEBIT_CREDIT", rsDetail.DEBIT_CREDIT);

                                                tableCURRENCYAMOUNT.Append(structCURRENCYAMOUNTGL100);
                                            }
                                        }
                                        #endregion


                                    }
                                    testfn.Invoke(dest);
                                    var result = testfn.GetTable("BAPI_RETURN"); //Error pas disini
                                    var resultA = testfn.GetTable("T_RETURN");
                                    string _resultPerVoucher;
                                    _resultPerVoucher = "";
                                    if (result.Count > 0)
                                    {

                                        SendSAPMessage _m = new SendSAPMessage();
                                        _m.SAPDocNumber = result[countHeader].GetValue("DOC_NUMSAP").ToString();
                                        _m.Message = result[countHeader].GetValue("MESSAGE").ToString();
                                        _m.RadsoftDocNumber = result[countHeader].GetValue("DOC_NUMCORE").ToString();
                                        _resultPerVoucher = result[countHeader].GetValue("MESSAGE").ToString();
                                        _l.Add(_m);
                                    }

                                    if (_resultPerVoucher != "Successfully")
                                    {
                                        SendSAPMessage _m = new SendSAPMessage();
                                        if (resultA.Count > 0)
                                        {
                                            for (int i = 0; i < resultA.Count; i++)
                                            {
                                                _m.Message = _m.Message + " | " + resultA[i].GetValue("MESSAGE").ToString();
                                            }
                                        }

                                        RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                                        return _m.Message;
                                    }
                                    else
                                    {
                                        if (resultA.Count > 0)
                                        {
                                            for (int i = 0; i < resultA.Count; i++)
                                            {
                                                SendSAPMessage _m = new SendSAPMessage();
                                                _m.Message = resultA[i].GetValue("MESSAGE").ToString();
                                                _l.Add(_m);
                                            }
                                        }


                                        //_a = UpdateInvestmentFromPosting(_l, _date);
                                        //RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                                        //return _a;

                                    }



                                }

                                if (_fundJournalType == "TRANSACTION")
                                {
                                    _a = UpdateInvestmentFromPostingTransaction(_l, _date);
                                }
                                else
                                {
                                    _a = UpdateInvestmentFromPosting(_l, _date);
                                }



                                RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                                return _a;

                            }
                        }
                        return null;
                    }
                }
            }
            catch (Exception err)
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(SAPCon);
                throw err;
            }

        }


        private string UpdateInvestmentFromPostingTransaction(List<SendSAPMessage> _l, DateTime _date)
        {
            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow drow;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Message";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "SAPDocNumber";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "RadsoftDocNumber";
                dc.Unique = false;
                dt.Columns.Add(dc);

                foreach (var r in _l)
                {
                    drow = dt.NewRow();
                    drow["Message"] = r.Message;
                    drow["SAPDocNumber"] = r.SAPDocNumber;
                    drow["RadsoftDocNumber"] = r.RadsoftDocNumber;
                    dt.Rows.Add(drow);
                }

                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table ZSAP_POSTINGRETURN";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {

                    bulkCopy.DestinationTableName = "dbo.ZSAP_POSTINGRETURN";
                    bulkCopy.WriteToServer(dt);
                }



                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {



                        // Ganti logik tanggal ke Fund Journal
                        cmd0.CommandText = @"
                        Declare  @Description Table
                        (Msg nvarchar(50), SAPNumber nvarchar(50), Reference nvarchar(50))

                        DECLARE @A Table  
                        (Reference nvarchar(50),DOCSAP nvarchar(500),InstrumentTypePK INT)
 
                        DECLARE @Reference nvarchar(50)
                        DECLARE @InstrumentTypePK int
                        DECLARE @combinedString VARCHAR(MAX)

                        declare @BBankPK int
                        declare @BReference nvarchar(50)
                        declare @BInvestmentPK int
                        declare @BDate datetime
                        declare @BTrxType int


                        DECLARE A CURSOR FOR 
                        select distinct Reference,InstrumentTypePK from Investment where SelectedSAP = 1 and StatusSettlement = 2 and ValueDate = @date
                        Open A
                        Fetch Next From A
                        Into @Reference,@InstrumentTypePK

                        While @@FETCH_STATUS = 0
                        BEGIN
                   
                        Insert Into @Description(Msg,SAPNumber,Reference)
                        select  distinct B.Message,B.SAPDocNumber,A.Reference from dbo.Investment A
                        INNER JOIN FundJournal C ON A.Reference = C.Reference AND C.status IN (2)
                        INNER JOIN ZSAP_POSTINGRETURN B ON replace(C.Reference,'/INV','') = LEFT(B.RadsoftDocNumber,LEN(B.RadsoftDocNumber) - 9) 
                        AND C.ValueDate = RIGHT(B.RadsoftDocNumber,8) 
                        WHERE  B.Message = 'Successfully' and C.Posted = 1 AND C.Reversed = 0 and A.Reference = @Reference and StatusSettlement = 2 and A.SelectedSAP = 1
                        AND B.SAPDocNumber IS NOT NULL 

                        SELECT @combinedString = COALESCE(@combinedString + ' | ', '') + SAPNumber
                        FROM @Description where Reference = @Reference



                        if(@InstrumentTypePK <> 5)
                        BEGIN
	                        Insert Into @A(Reference,DOCSAP,InstrumentTypePK)
	                        select Reference,@combinedString,@InstrumentTypePK from @Description where Reference = @Reference
                        END
                        ELSE
                        BEGIN

	                        DECLARE B CURSOR FOR 
	                        select BankPK,Reference,InvestmentPK,ValueDate,TrxType from Investment where Reference = @Reference and ValueDate = @Date
	                        order by InvestmentPK Desc
	                        Open B
	                        Fetch Next From B
	                        Into @BBankPK,@BReference,@BInvestmentPK,@BDate,@BTrxType

	                        While @@FETCH_STATUS = 0
	                        BEGIN
		                        Insert Into @A(Reference,DOCSAP,InstrumentTypePK)
		                        select @BReference,@combinedString,@InstrumentTypePK

	                        Fetch next From B Into @BBankPK,@BReference,@BInvestmentPK,@BDate,@BTrxType
	                        END
	                        Close B
	                        Deallocate B 


                        END


                        Fetch next From A Into @Reference,@InstrumentTypePK
                        END
                        Close A
                        Deallocate A 

                        Update A Set A.DOCSAP = B.DOCSAP
                        From @A A
                        Left Join (Select * From @A Where DOCSAP Is Not Null) B 
                        On A.InstrumentTypePK = B.InstrumentTypePK
                        Where A.DOCSAP Is Null  

				
                        update A set DocSAP = B.DOCSAP from Investment A 
                        left join @A B on A.Reference = B.Reference
                        where  StatusSettlement = 2 and ValueDate = @Date  and SelectedSAP = 1

                        SELECT 'Doc Number: ' + @combinedString  as Result
                         ";

                        cmd0.Parameters.AddWithValue("@Date", _date);
                        cmd0.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd0.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["Result"]);
                                }
                            }
                            return "";
                        }
                    }
                }



            }
        }

        private string UpdateInvestmentFromPosting(List<SendSAPMessage> _l, DateTime _date)
        {
            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow drow;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Message";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "SAPDocNumber";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "RadsoftDocNumber";
                dc.Unique = false;
                dt.Columns.Add(dc);

                foreach (var r in _l)
                {
                    drow = dt.NewRow();
                    drow["Message"] = r.Message;
                    drow["SAPDocNumber"] = r.SAPDocNumber;
                    drow["RadsoftDocNumber"] = r.RadsoftDocNumber;
                    dt.Rows.Add(drow);
                }

                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table ZSAP_POSTINGRETURN";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {

                    bulkCopy.DestinationTableName = "dbo.ZSAP_POSTINGRETURN";
                    bulkCopy.WriteToServer(dt);
                }



                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {



//                        // Ganti logik tanggal ke Fund Journal
//                        cmd0.CommandText = @"
//                        Declare  @Description Table
//                        (Msg nvarchar(50), SAPNumber nvarchar(50), FundJournalPK int, FundJournalType nvarchar(50))
//
//                        DECLARE @A Table  
//                        (FundJournalPK int,DOCSAP nvarchar(500))
// 
//                        DECLARE @FundJournalPK int
//                        DECLARE @FundJournalType nvarchar(50)
//                        DECLARE @combinedString VARCHAR(MAX)
//
//                        DECLARE A CURSOR FOR 
//                        select distinct FundJournalPK,FundJournalType from ZSAP_FUNDJOURNAL_TEMP where Selected = 1  and ValueDate = @date
//                        Open A
//                        Fetch Next From A
//                        Into @FundJournalPK,@FundJournalType
//
//                        While @@FETCH_STATUS = 0
//                        BEGIN
//
//                   
//                        Insert Into @Description(Msg,SAPNumber,FundJournalPK,FundJournalType)
//                        select  distinct B.Message,B.SAPDocNumber,A.FundJournalPK,A.FundJournalType from dbo.ZSAP_FUNDJOURNAL_TEMP A
//                        INNER JOIN ZSAP_POSTINGRETURN B ON FundJournalPK = LEFT(B.RadsoftDocNumber,LEN(B.RadsoftDocNumber) - 9) 
//                        AND A.ValueDate = RIGHT(B.RadsoftDocNumber,8) 
//                        WHERE  B.Message = 'Successfully' and A.FundJournalPK = @FundJournalPK and A.Selected = 1
//                        AND B.SAPDocNumber IS NOT NULL 
//
//
//
//                        SELECT @combinedString = COALESCE(@combinedString + ' | ', '') + SAPNumber
//                        FROM @Description where FundJournalPK = @FundJournalPK and FundJournalType = @FundJournalType
//
//
//                        Insert Into @A(FundJournalPK,DOCSAP)
//                        select FundJournalPK,SAPNumber from @Description where FundJournalPK = @FundJournalPK   and FundJournalType = @FundJournalType
//
//                        Fetch next From A Into @FundJournalPK,@FundJournalType
//                        END
//                        Close A
//                        Deallocate A 
//
//						
//                        update A set DocSAP = isnull(B.DOCSAP,'') from ZSAP_FUNDJOURNAL_TEMP A 
//                        left join @A B on A.FundJournalPK = B.FundJournalPK
//                        where  ValueDate = @Date and FundJournalType = @FundJournalType and A.Selected = 1
//
//                        SELECT 'Doc Number: ' + @combinedString  as Result
//                         ";
                        cmd0.CommandText = @"
                        
                        Declare  @Description Table
                        (Msg nvarchar(50), SAPNumber nvarchar(50), FundJournalPK int, FundJournalType nvarchar(50), Description nvarchar(100))

                        DECLARE @A Table  
                        (FundJournalPK int,DOCSAP nvarchar(500), FundJournalType nvarchar(500), Description nvarchar(100))
 
                        DECLARE @FundJournalPK int
                        DECLARE @FundJournalType nvarchar(50)
                        DECLARE @DetailDescription nvarchar(100)
                        DECLARE @combinedString VARCHAR(MAX)

                        DECLARE A CURSOR FOR 
                        select distinct FundJournalPK,FundJournalType,Description from ZSAP_FUNDJOURNAL_TEMP where Selected = 1  and ValueDate = @date
                        Open A
                        Fetch Next From A
                        Into @FundJournalPK,@FundJournalType,@DetailDescription

                        While @@FETCH_STATUS = 0
                        BEGIN

                        delete  @Description                  
                        Insert Into @Description(Msg,SAPNumber,FundJournalPK,FundJournalType,Description)
                        select  distinct B.Message,B.SAPDocNumber,A.FundJournalPK,A.FundJournalType,A.Description from dbo.ZSAP_FUNDJOURNAL_TEMP A
                        INNER JOIN ZSAP_POSTINGRETURN B ON  A.ValueDate = RIGHT(B.RadsoftDocNumber,8)  
                        and case when FundJournalPK = 0 then case when A.Description = 'PIUTANG BOND' then '02' 
                        else case when A.Description = 'PIUTANG DEPOSITO' then '03' 
                        else case when A.Description = 'PIUTANG REKSADANA' then '04' 
                        else case when A.Description = 'PIUTANG EBA' then '05' 
                        else case when A.Description = 'PORTFOLIO REVALUATION EQUITY' then '11' 
                        else case when A.Description = 'PORTFOLIO REVALUATION BOND' then '12' 
                        else case when A.Description = 'PORTFOLIO REVALUATION REKSADANA' then '14' 
                        else case when A.Description = 'PORTFOLIO REVALUATION EBA' then '15' 
                        else case when A.Description = 'AMORTIZE' then '22'
                        end end end end end end end end end else FundJournalPK end = LEFT(B.RadsoftDocNumber,LEN(B.RadsoftDocNumber) - 9)
                        WHERE  B.Message = 'Successfully' and A.FundJournalPK = @FundJournalPK and A.Selected = 1 and FundJournalType = @FundJournalType
                        AND B.SAPDocNumber IS NOT NULL 


                        SELECT @combinedString = COALESCE(@combinedString + ' | ', '') + SAPNumber
                        FROM @Description where FundJournalPK = @FundJournalPK and FundJournalType = @FundJournalType and Description = @DetailDescription


                        Insert Into @A(FundJournalPK,DOCSAP,FundJournalType,Description)
                        select FundJournalPK,SAPNumber,FundJournalType,Description from @Description where FundJournalPK = @FundJournalPK   and FundJournalType = @FundJournalType and Description = @DetailDescription

                        Fetch next From A Into @FundJournalPK,@FundJournalType,@DetailDescription
                        END
                        Close A
                        Deallocate A 
                        --select * from @A
                        --select * from @Description
                        --select * from ZSAP_FUNDJOURNAL_TEMP

                        --select A.DocSAP,isnull(B.DOCSAP,''),* from ZSAP_FUNDJOURNAL_TEMP A 
                        --left join @A B on A.FundJournalPK = B.FundJournalPK and A.FundJournalType = B.FundJournalType and A.Description = B.Description
                        --where  ValueDate = @Date  and A.Selected = 1
		
                        update A set DocSAP = isnull(B.DOCSAP,'') from ZSAP_FUNDJOURNAL_TEMP A 
                        left join @A B on A.FundJournalPK = B.FundJournalPK  and A.FundJournalType = B.FundJournalType and A.Description = B.Description
                        where  ValueDate = @Date  and A.Selected = 1

                        SELECT 'Doc Number: ' + @combinedString  as Result  ";

                        cmd0.Parameters.AddWithValue("@Date", _date);
                        cmd0.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd0.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["Result"]);
                                }
                            }
                            return "";
                        }
                    }
                }



            }
        }

        public int SAPMSAccount_Update(SAPMaster _sapMSAccount, bool _havePrivillege)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update SAPMSAccount set AccountType=@AccountType where ID = @ID and Name = @Name";

                        cmd.Parameters.AddWithValue("@ID", _sapMSAccount.ID);
                        cmd.Parameters.AddWithValue("@Name", _sapMSAccount.Name);
                        cmd.Parameters.AddWithValue("@AccountType", _sapMSAccount.AccountType);

                        cmd.ExecuteNonQuery();
                    }
                    return 0;

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public Boolean ReportLPTI_BySelectedData(string _userID, ReportLPTI _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                        @" select A.InstrumentTypePK,A.SettlementDate,C.Name InstrumentName,A.TrxType,A.TrxTypeID,G.Name FundName,J.BankAccountNo,A.InvestmentPK,A.ValueDate InstructionDate,C.ID InstrumentID,isnull(C.SAPCustID,'') KodeCustomer,F.ID KodeAkun, 'J000' BusinessArea, H.Name CounterpartName,case when K.ID like '5%' then D.Amount *-1 else D.Amount end Amount  from Investment A
                        left join FundJournal B on A.Reference = B.Reference and B.status = 2
                        left join Instrument C on A.instrumentPK = C.InstrumentPK and C.Status in (1,2)
                        left join FundJournalDetail D on B.FundJournalPK = D.FundJournalPK and D.Status = 2
                        left join ZSAP_BridgeJournal E on D.FundJournalAccountPK = E.FundJournalAccountPK and E.Status in (1,2)
                        left join ZSAP_MS_Account F on E.SAPAccountID = F.ID
                        left join Fund G on A.FundPK = G.FundPK and G.status in (1,2)
                        left join Counterpart H on A.CounterpartPK = H.CounterpartPK and H.status in (1,2)
                        left join FundCashRef I on A.FundCashRefPK = I.FundCashRefPK and I.status in (1,2)
                        left join BankBranch J on I.BankBranchPK = J.BankBranchPK and J.status in (1,2)
                        left join FundJournalAccount K on D.FundJournalAccountPK = K.FundJournalAccountPK and K.status in (1,2)
                        where B.Type = 5 and B.Posted = 1 and B.Reversed = 0 and A.StatusSettlement = 2 and A.ValueDate = @Date 
                        and K.FundJournalAccountPK not in (42,66) and A.SelectedSAP = 1";


                        cmd.Parameters.AddWithValue("@Date", _listing.ParamListDate);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReportLPTI" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReportLPTI" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LPTI Saham");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<ReportLPTI> rList = new List<ReportLPTI>();
                                    while (dr0.Read())
                                    {
                                        ReportLPTI rSingle = new ReportLPTI();
                                        rSingle.InstrumentTypePK = dr0["InstrumentTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["InstrumentTypePK"]);
                                        rSingle.InvestmentPK = dr0["InvestmentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["InvestmentPK"]);
                                        rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                        rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                        rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.TrxType = Convert.ToInt32(dr0["TrxType"]);
                                        rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                        rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.KodeCustomer = dr0["KodeCustomer"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodeCustomer"]);
                                        rSingle.KodeAkun = dr0["KodeAkun"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodeAkun"]);
                                        rSingle.BusinessArea = dr0["BusinessArea"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BusinessArea"]);
                                        rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                        rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                        rSingle.BankAccountNo = dr0["BankAccountNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNo"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        orderby r.TrxTypeID ascending
                                        group r by new { r.FundName, r.TrxTypeID, r.BankAccountNo, r.SettlementDate } into rGroup
                                        //group r by new { r.FundName, r.TrxTypeID, r.FaxNo, r.Telp, r.BankAccountNo, r.FundRekName, r.FundID, r.CustodianID, r.FaxNo2, r.Telp2 } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;
                                    //int _startRowDetail = 0;

                                    foreach (var rsHeader in QueryByFundID)
                                    {


                                        int _endRowDetail = incRowExcel;


                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "LEMBAR PENGANTAR TRANSAKSI INVESTASI (LPTI)";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                        incRowExcel = incRowExcel + 3;

                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "Dari ";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 4].Value = "Kepala Divisi Investasi";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth. ";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 4].Value = "Departemen Keuangan";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "Lampiran";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = "Instruksi Pembelian Saham";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = "Instruksi Penjualan Saham";
                                        }

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "Perihal";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = "Pembelian Saham TUD (Tersedia Untuk Dibeli) Pada Pasar Sekunder";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = "Penjualan Saham TUD (Tersedia Untuk Dijual) Pada Pasar Sekunder";
                                        }

                                        incRowExcel = incRowExcel + 4;
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Menunjuk perihal tersebut diatas, dengan ini mohon dikirimkan dana atas Pembelian Saham untuk settlement";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        }
                                        else
                                        {
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Menunjuk perihal tersebut diatas, dengan ini mohon diterima dana atas Penjualan Saham untuk settlement";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        }

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "Tanggal";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.SettlementDate;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd MMMM yyyy";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        //worksheet.Cells[incRowExcel, 4].Value = "sebesar Rp " + rsHeader.Key.Amount.ToString("N0") + " atas broker sebagai terlampir.";
                                        worksheet.Cells[incRowExcel, 4].Value = "sebesar Rp " + "" + " atas broker sebagai terlampir.";

                                        incRowExcel++;
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Value = "Adapun rincian kebutuhan adalah sebagai berikut :";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Value = "Adapun rincian penerimaan adalah sebagai berikut :";
                                        }


                                        incRowExcel = incRowExcel + 2;

                                        int RowF = incRowExcel;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 2].Value = "Nama Emiten";
                                        worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = "Kode Customer";
                                        worksheet.Cells[incRowExcel, 6].Value = "Kode Akun";
                                        worksheet.Cells[incRowExcel, 7].Value = "Business Area";
                                        worksheet.Cells[incRowExcel, 8].Value = "Broker";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 9].Value = "Nominal";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Row(incRowExcel).Height = 30;
                                        incRowExcel++;


                                        worksheet.Cells["A" + RowF + ":I" + RowF].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF + ":I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["A" + RowF + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        int RowD = incRowExcel;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        //int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.KodeCustomer;
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.KodeAkun;
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.BusinessArea;
                                            worksheet.Cells[incRowExcel, 7].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.CounterpartName;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Row(incRowExcel).Height = 55;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;
                                        }

                                        //worksheet.Cells[incRowExcel, 2].Value = "TOTAL :";
                                        //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        //worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 9].Calculate();

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Atas transaksi tersebut mohon bantuannya untuk melaksanakan hal-hal berikut :";
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "1. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 3].Value = "Menerima saham selambat-lambatnya tanggal " + Convert.ToDateTime(rsHeader.Key.SettlementDate).ToString("dd MMMM yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "1. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 3].Value = "Menyerahkan saham selambat-lambatnya tanggal " + Convert.ToDateTime(rsHeader.Key.SettlementDate).ToString("dd MMMM yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        }


                                        incRowExcel++;
                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "2. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 3].Value = "Mengirimkan uang selambat-lambatnya tanggal  " + Convert.ToDateTime(rsHeader.Key.SettlementDate).ToString("dd MMMM yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "2. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 3].Value = "Menerima uang selambat-lambatnya tanggal " + Convert.ToDateTime(rsHeader.Key.SettlementDate).ToString("dd MMMM yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        }


                                        incRowExcel++;

                                        if (rsHeader.Key.TrxTypeID == "BUY")
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "3. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                            worksheet.Cells[incRowExcel, 3].Value = "Dana Pembelian Saham ditransfer ke rekening yang digunakan untuk pembayaran (sesuai surat terlampir) yaitu Bank Mandiri No. Rek " + rsHeader.Key.BankAccountNo + " A/N PT. Asuransi Jiwa Taspen";
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 51;
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "3. ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                            worksheet.Cells[incRowExcel, 3].Value = "Dana hasil Penjualan Saham diterima di rekening penerima (sesuai surat terlampir) Bank Mandiri No. Rek " + rsHeader.Key.BankAccountNo + " A/N PT. Asuransi Jiwa Taspen";
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 51;
                                        }


                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terima kasih.";
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;

                                        incRowExcel = incRowExcel + 10;
                                        worksheet.Cells[incRowExcel, 8].Value = "Jakarta, " + Convert.ToDateTime(rsHeader.Key.SettlementDate).ToString("dd MMMM yyyy");
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 15;
                                        worksheet.Cells[incRowExcel, 8].Value = "Ruben Sukatendel";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 8].Value = "Kepala Divisi Investasi";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }



                                    string _rangeA = "A:I" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 20;
                                    }


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 9];
                                    worksheet.Column(1).Width = 12;
                                    worksheet.Column(2).Width = 7;
                                    worksheet.Column(3).Width = 18;
                                    worksheet.Column(4).Width = 18;
                                    worksheet.Column(5).Width = 25;
                                    worksheet.Column(6).Width = 20;
                                    worksheet.Column(7).Width = 25;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 30;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_listing.DownloadMode == "PDF")
                                    {
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    }
                                    return true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }



        }

        public List<InterfaceJournalSAP> Init_AmortizeInterfaceSAP(DateTime _dateFrom)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InterfaceJournalSAP> L_InterfaceJournalSAP = new List<InterfaceJournalSAP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" 
Delete  ZSAP_FUNDJOURNAL_TEMP where DocSAP  = ''


Insert into ZSAP_FUNDJOURNAL_TEMP
select FundPK,Selected,FundJournalPK,ValueDate,FundJournalType,Description,DOCSAP from (
select distinct A.FundPK,0 Selected,0 FundJournalPK,ValueDate,'INTEREST BOND' FundJournalType,'PIUTANG BOND' Description,'' DOCSAP from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'INTEREST BOND'  and A.status = 2 and C.InstrumentTypePK not in (1,4,5,6,8,16)
union all
select distinct A.FundPK,0 Selected,0 FundJournalPK,ValueDate,'INTEREST EBA' FundJournalType,'PIUTANG EBA' Description,'' DOCSAP from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'INTEREST BOND'  and A.status = 2  and C.InstrumentTypePK  in (8)
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'INTEREST DEPOSIT' FundJournalType,'PIUTANG DEPOSITO','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'INTEREST DEPOSIT'  and A.status = 2
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'INTEREST FUND' FundJournalType,'PIUTANG REKSADANA','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'INTEREST FUND'  and A.status = 2
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'PORTFOLIO REVALUATION' FundJournalType,'PORTFOLIO REVALUATION EQUITY','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'PORTFOLIO REVALUATION'  and A.status = 2 and C.InstrumentTypePK  in (1,4,16)
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'PORTFOLIO REVALUATION' FundJournalType,'PORTFOLIO REVALUATION BOND','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'PORTFOLIO REVALUATION'  and A.status = 2 and C.InstrumentTypePK not in (1,4,5,6,8,16)
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'PORTFOLIO REVALUATION' FundJournalType,'PORTFOLIO REVALUATION EBA','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'PORTFOLIO REVALUATION'  and A.status = 2 and C.InstrumentTypePK in (8)
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'PORTFOLIO REVALUATION' FundJournalType,'PORTFOLIO REVALUATION REKSADANA','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'PORTFOLIO REVALUATION'  and A.status = 2 and C.InstrumentTypePK in (6)
union all
select distinct A.FundPK,0,A.InstrumentPK FundJournalPK,ValueDate,'REC COUPON' FundJournalType,'INSTRUMENT : ' + C.ID Description,'' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'REC COUPON'  and A.status = 2 and C.InstrumentTypePK not in (1,5,8)
union all
select distinct A.FundPK,0,A.InstrumentPK FundJournalPK,ValueDate,'REC COUPON EBA' FundJournalType,'INSTRUMENT : ' + C.ID Description,'' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'REC COUPON'  and A.status = 2 and C.InstrumentTypePK in (8)
union all
select distinct A.FundPK,0,D.BankPK FundJournalPK,ValueDate,'REC COUPON DEPOSITO' FundJournalType,'REC COUPON DEPOSITO BANK : ' + C.ID Description,'' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
left join Bank D on C.BankPK = D.BankPK and D.Status in (1,2)
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'REC COUPON'  and A.status = 2 and C.InstrumentTypePK  in (5)
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'DIVIDEND' FundJournalType,'INSTRUMENT : ' + C.ID Description,'' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'DIVIDEND'  and A.status = 2
--union all
--select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'MATURE BANK' FundJournalType,'INSTRUMENT : ' + C.ID Description,'' from FundJournalDetail A
--left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
--left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
--where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'MATURE BANK'  and A.status = 2
union all
select distinct A.FundPK,0,A.FundJournalPK FundJournalPK,ValueDate,'ADJUSTMENT' FundJournalType,Description,'' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and Type = 1  and A.status = 2
union all
select distinct A.FundPK,0,0 FundJournalPK,ValueDate,'AMORTIZE' FundJournalType,'AMORTIZE','' from FundJournalDetail A
left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
where Posted = 1 and Reversed = 0 and ValueDate = @Date and TrxName = 'AMORTIZE'  and A.status = 2

--union all
--select distinct A.FundPK,0,A.InstrumentPK,A.Date,'AMORTIZE' FundJournalType,'Amortize : ' + C.ID ,'' from FundPosition A 
--left join BondInterestAndAmortizeDiscount B on A.InstrumentPK = B.InstrumentPK and A.AvgPrice = B.CostPrice and A.Balance = B.FaceValue and A.Date = B.Date
--left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
--where A.Date = @Date and C.InstrumentTypePK not in (1,5,6) and A.status = 2 and A.AvgPrice <> 100

) A 
where not exists
(select * from ZSAP_FUNDJOURNAL_TEMP B where A.ValueDate = B.ValueDate and A.FundJournalType = B.FundJournalType and A.Description = B.Description and A.FundJournalPK = B.FundJournalPK and B.DocSAP  <> '')


select B.ID FundID,A.* from ZSAP_FUNDJOURNAL_TEMP A 
left join Fund B on A.FundPk = B.FundPK and B.status in (1,2)
where ValueDate = @Date
                            ";

                        cmd.Parameters.AddWithValue("@Date", _dateFrom);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InterfaceJournalSAP M_InterfaceJournalSAP = new InterfaceJournalSAP();
                                    M_InterfaceJournalSAP.Selected = Convert.ToBoolean(dr["Selected"]);
                                    M_InterfaceJournalSAP.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_InterfaceJournalSAP.FundID = Convert.ToString(dr["FundID"]);
                                    M_InterfaceJournalSAP.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
                                    M_InterfaceJournalSAP.Date = Convert.ToString(dr["ValueDate"]);
                                    M_InterfaceJournalSAP.FundJournalType = Convert.ToString(dr["FundJournalType"]);
                                    M_InterfaceJournalSAP.Description = Convert.ToString(dr["Description"]);
                                    M_InterfaceJournalSAP.DOCSAP = Convert.ToString(dr["DOCSAP"]);

                                    L_InterfaceJournalSAP.Add(M_InterfaceJournalSAP);

                                }
                            }

                            return L_InterfaceJournalSAP;
                        }
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }
        }


        public List<InterfaceJournalSAP> Get_DataAmortizeInterfaceSAP(DateTime _dateFrom, string _fundJournalType, string _fundID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InterfaceJournalSAP> L_InterfaceJournalSAP = new List<InterfaceJournalSAP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        if (_fundID != "0")
                        {
                            _paramFund = "And A.FundPK  = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        string _paramType = "";
                        if (_fundJournalType != "0")
                        {
                            _paramType = " and A.FundJournalType  = @FundJournalType ";
                        }
                        else
                        {
                            _paramType = "";
                        }

                        cmd.CommandText = @" 

                            select B.ID FundID,A.* from ZSAP_FUNDJOURNAL_TEMP A 
                            left join Fund B on A.FundPk = B.FundPK and B.status in (1,2)
                            where ValueDate  = @Date " + _paramType + _paramFund ;

                        cmd.Parameters.AddWithValue("@Date", _dateFrom);
                        cmd.Parameters.AddWithValue("@FundJournalType", _fundJournalType);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InterfaceJournalSAP M_InterfaceJournalSAP = new InterfaceJournalSAP();
                                    M_InterfaceJournalSAP.Selected = Convert.ToBoolean(dr["Selected"]);
                                    M_InterfaceJournalSAP.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_InterfaceJournalSAP.FundID = Convert.ToString(dr["FundID"]);
                                    M_InterfaceJournalSAP.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
                                    M_InterfaceJournalSAP.Date = Convert.ToString(dr["ValueDate"]);
                                    M_InterfaceJournalSAP.FundJournalType = Convert.ToString(dr["FundJournalType"]);
                                    M_InterfaceJournalSAP.Description = Convert.ToString(dr["Description"]);
                                    M_InterfaceJournalSAP.DOCSAP = Convert.ToString(dr["DOCSAP"]);
                                    L_InterfaceJournalSAP.Add(M_InterfaceJournalSAP);

                                }
                            }

                            return L_InterfaceJournalSAP;
                        }
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }
        }


        public void SelectDeselectAllDataByDateAmortize(bool _toggle, DateTime _Date, string _journalType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramType = "";
                        if (_journalType != "ALL")
                        {
                            _paramType = " and FundJournalType = @FundJournalType ";
                        }
                        else
                        {
                            _paramType = "";
                        }
                        cmd.CommandText =
                        @" Update ZSAP_FUNDJOURNAL_TEMP set Selected = @Toggle  where valueDate = @Date  " + _paramType;

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@Date", _Date);
                        cmd.Parameters.AddWithValue("@FundJournalType", _journalType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataAmortize(bool _toggle, int _fundjournalPK, string _fundjournalType, string _description, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundjournalType == "REC COUPON" || _fundjournalType == "REC COUPON DEPOSITO" || _fundjournalType == "ADJUSTMENT")
                        {
                            cmd.CommandText = @"Update ZSAP_FUNDJOURNAL_TEMP set Selected = @Toggle where FundJournalPK = @PK and FundJournalType = @Type and ValueDate = @Date ";
                        }
                        else
                        {
                            cmd.CommandText = @"Update ZSAP_FUNDJOURNAL_TEMP set Selected = @Toggle where FundJournalPK = @PK and FundJournalType = @Type and Description = @Description and Valuedate = @Date";
                        }
             
                        cmd.Parameters.AddWithValue("@PK", _fundjournalPK);
                        cmd.Parameters.AddWithValue("@Type", _fundjournalType);
                        cmd.Parameters.AddWithValue("@Description", _description);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Boolean Preview_InterfaceJournalInterestSAP(string _userID, InterfaceJournalSAP _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        if (_listing.FundID != "0")
                        {
                            _paramFund = "And F.FundPK  = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        string _paramType = "";
                        if (_listing.FundJournalType != "ALL")
                        {
                            _paramType = " and A.FundJournalType  = @FundJournalType ";
                        }
                        else
                        {
                            _paramType = "";
                        }

                        cmd.CommandText = @" 
                        declare @intdays int
                        declare @DateYesterday datetime

                        select @DateYesterday = dbo.fworkingday(@DateTo,-1)        
                        Select @intdays =  DateDiff(day,@DateYesterday,@DateTo) 

                        select FundID,FundJournalType,sum(Total) Total from (
                        select F.ID FundID,FundJournalType, sum(C.BaseDebit-BaseCredit) Total from ZSAP_FUNDJOURNAL_TEMP A 
                        left join Fund F on A.FundPk = F.FundPK and F.status in (1,2)
                        left join FundJournalDetail C on A.FundJournalPK = C.FundJournalPK and C.status = 2
                        left join FundJournal D on C.FundJournalPK = D.FundJournalPK and D.status = 2
                        left join FundJournalAccount E on C.FundJournalAccountPK = E.FundJournalAccountPK and E.status = 2
                        where A.ValueDate between @DateFrom and @DateTo and D.Posted = 1 and D.Reversed = 0 and E.Type = 1 and FundJournalType not in ('AMORTIZE','ADJUSTMENT')
                        " + _paramFund + @"
                        group By F.ID,FundJournalType
                        union all
                        select F.ID,'AMORTIZE',sum(Premium -  TotalAmortizeDiscount) Total from BondInterestAndAmortizeDiscount A
                        left join FundPosition B on A.InstrumentPK = B.InstrumentPK and A.CostValue = B.TrxAmount and A.Date = B.Date and B.Status = 2
                        left join Instrument C on A.InstrumentPK = C.InstrumentPK  and C.status in (1,2)
                        left join Fund F on B.FundPK = F.FundPK and F.Status in (1,2)
                        where C.InstrumentTypePK not in (13,15) and A.Date = @DateTo  " + _paramFund + @"
                        group by F.ID
                        union all

                        select F.ID,'AMORTIZE',isnull(Premium - ((Premium/TotalDays) * datediff(day,SettledDate,A.Date)),0) Total from BondInterestAndAmortizeDiscount A
                        left join FundPosition B on A.InstrumentPK = B.InstrumentPK and A.CostValue = B.TrxAmount and A.Date = B.Date and B.Status = 2
                        left join Instrument C on A.InstrumentPK = C.InstrumentPK  and C.status in (1,2)
                        left join Fund F on B.FundPK = F.FundPK and F.Status in (1,2)
                        where C.InstrumentTypePK in (13,15) and A.Date = @DateTo  " + _paramFund + @"
                        group by F.ID,A.Premium,A.TotalDays,SettledDate,A.Date
                        )Z
                        group by FundID,FundJournalType
                        ";

                        cmd.Parameters.AddWithValue("@DateFrom", _listing.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _listing.DateTo);
                        cmd.Parameters.AddWithValue("@FundJournalType", _listing.FundJournalType);
                        cmd.Parameters.AddWithValue("@FundPK", _listing.FundID);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReportLPTI" + "_" + _listing.DateTo.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReportLPTI" + "_" + _listing.DateTo.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LPTI Saham");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InterfaceJournalSAP> rList = new List<InterfaceJournalSAP>();
                                    while (dr0.Read())
                                    {
                                        InterfaceJournalSAP rSingle = new InterfaceJournalSAP();
                                        rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                        rSingle.FundJournalType = dr0["FundJournalType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundJournalType"]);
                                        rSingle.Total = dr0["Total"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Total"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        orderby r.FundID,r.FundJournalType ascending
                                        group r by new { r.FundID, r.FundJournalType } into rGroup
                                        //group r by new { r.FundName, r.TrxTypeID, r.FaxNo, r.Telp, r.BankAccountNo, r.FundRekName, r.FundID, r.CustodianID, r.FaxNo2, r.Telp2 } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;
                                    //int _startRowDetail = 0;

                                    foreach (var rsHeader in QueryByFundID)
                                    {


                                        int _endRowDetail = incRowExcel;


                                        int RowF = incRowExcel;
                                      
                                        int RowD = incRowExcel;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        //int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundID;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundJournalType;
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Total;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Row(incRowExcel).Height = 55;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;
                                        }

                                        //worksheet.Cells[incRowExcel, 2].Value = "TOTAL :";
                                        //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        //worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 9].Calculate();

                                        incRowExcel = incRowExcel + 4;


                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }



                                    string _rangeA = "A:I" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 20;
                                    }


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 9];
                                    worksheet.Column(1).Width = 12;
                                    worksheet.Column(2).Width = 7;
                                    worksheet.Column(3).Width = 18;
                                    worksheet.Column(4).Width = 18;
                                    worksheet.Column(5).Width = 25;
                                    worksheet.Column(6).Width = 40;
                                    worksheet.Column(7).Width = 25;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 30;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);

                                    return true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }



        }


        public void Reset_SelectedSAP()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"
                        Update Investment set SelectedSAP = 0

                        Update ZSAP_FUNDJOURNAL_TEMP set selected = 0
                                                ";
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