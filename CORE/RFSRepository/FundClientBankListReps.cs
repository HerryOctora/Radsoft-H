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
    public class FundClientBankListReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientBankList] " +
                            "([FundClientBankListPK],[HistoryPK],[Status],[FundClientPK],[BankPK],[AccountName],[AccountNo],[NoBank],[CurrencyPK]," +
                            " ";

        string _paramaterCommand = "@FundClientPK,@BankPK,@AccountName,@AccountNo,@NoBank,@CurrencyPK," +
                            " ";

        //2
        private FundClientBankList setFundClientBankList(SqlDataReader dr)
        {
            FundClientBankList M_FundClientBankList = new FundClientBankList();
            M_FundClientBankList.FundClientBankListPK = Convert.ToInt32(dr["FundClientBankListPK"]);
            M_FundClientBankList.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientBankList.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientBankList.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientBankList.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientBankList.FundClientPK = dr["FundClientPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientBankList.ID = Convert.ToString(dr["ID"]);
            M_FundClientBankList.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
            M_FundClientBankList.BankID = dr["BankID"].ToString();
            M_FundClientBankList.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_FundClientBankList.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_FundClientBankList.AccountName = dr["AccountName"].ToString();
            M_FundClientBankList.AccountNo = dr["AccountNo"].ToString();
            M_FundClientBankList.NoBank = Convert.ToString(dr["NoBank"]);
            M_FundClientBankList.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientBankList.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientBankList.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientBankList.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientBankList.EntryTime = dr["EntryTime"].ToString();
            M_FundClientBankList.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientBankList.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientBankList.VoidTime = dr["VoidTime"].ToString();
            M_FundClientBankList.DBUserID = dr["DBUserID"].ToString();
            M_FundClientBankList.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientBankList.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientBankList.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_FundClientBankList;
        }

        public List<FundClientBankList> FundClientBankList_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientBankList> L_FundClientBankList = new List<FundClientBankList>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                                 Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                  C.BankPK,C.ID BankID,D.CurrencyPK,D.ID CurrencyID, E.FundClientPK FundClientPK, E.ID + ' - ' + E.Name ID,B.* from FundClientBankList B      
                                  left join Bank C on B.BankPK = C.BankPK and C.status in(1,2)   
                                  left join Currency D on B.CurrencyPK = D.CurrencyPK and C.status in(1,2)   
                                  left join FundClient E on B.FundClientPK = E.FundClientPK and E.status in(1,2)    
                                  where B.status = @status
                            ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                  Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                  C.BankPK,C.ID BankID,D.CurrencyPK,D.ID CurrencyID, E.FundClientPK FundClientPK, E.ID + ' - ' + E.Name ID,B.* from FundClientBankList B      
                                  left join Bank C on B.BankPK = C.BankPK and C.status in(1,2)   
                                  left join Currency D on B.CurrencyPK = D.CurrencyPK and C.status in(1,2)   
                                  left join FundClient E on B.FundClientPK = E.FundClientPK and E.status in(1,2)   
                        ";



                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientBankList.Add(setFundClientBankList(dr));
                                }
                            }
                            return L_FundClientBankList;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientBankList_Add(FundClientBankList _FundClientBankList, bool _havePrivillege)
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
                                 "Select isnull(max(FundClientBankListPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundClientBankList";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankList.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundClientBankListPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundClientBankList";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankList.FundClientPK);
                        cmd.Parameters.AddWithValue("@BankPK", _FundClientBankList.BankPK);
                        cmd.Parameters.AddWithValue("@AccountName", _FundClientBankList.AccountName);
                        cmd.Parameters.AddWithValue("@AccountNo", _FundClientBankList.AccountNo);
                        cmd.Parameters.AddWithValue("@NoBank", _FundClientBankList.NoBank);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _FundClientBankList.CurrencyPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientBankList.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundClientBankList");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientBankList_Update(FundClientBankList _FundClientBankList, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FundClientBankList.FundClientBankListPK, _FundClientBankList.HistoryPK, "FundClientBankList");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientBankList set status=2,Notes=@Notes,FundClientPK=@FundClientPK,BankPK=@BankPK,AccountName=@AccountName,AccountNo=@AccountNo,NoBank=@NoBank,CurrencyPK=@CurrencyPK, " +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FundClientBankListPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankList.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientBankList.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankList.FundClientPK);
                            cmd.Parameters.AddWithValue("@BankPK", _FundClientBankList.BankPK);
                            cmd.Parameters.AddWithValue("@AccountName", _FundClientBankList.AccountName);
                            cmd.Parameters.AddWithValue("@AccountNo", _FundClientBankList.AccountNo);
                            cmd.Parameters.AddWithValue("@NoBank", _FundClientBankList.NoBank);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _FundClientBankList.CurrencyPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankList.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankList.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientBankList set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientBankListPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankList.EntryUsersID);
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
                                cmd.CommandText = "Update FundClientBankList set Notes=@Notes,FundClientPK=@FundClientPK,BankPK=@BankPK,AccountName=@AccountName,AccountNo=@AccountNo,NoBank=@NoBank,CurrencyPK=@CurrencyPK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FundClientBankListPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankList.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientBankList.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankList.FundClientPK);
                                cmd.Parameters.AddWithValue("@BankPK", _FundClientBankList.BankPK);
                                cmd.Parameters.AddWithValue("@AccountName", _FundClientBankList.AccountName);
                                cmd.Parameters.AddWithValue("@AccountNo", _FundClientBankList.AccountNo);
                                cmd.Parameters.AddWithValue("@NoBank", _FundClientBankList.NoBank);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _FundClientBankList.CurrencyPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankList.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientBankList.FundClientBankListPK, "FundClientBankList");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientBankList where FundClientBankListPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankList.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankList.FundClientPK);
                                cmd.Parameters.AddWithValue("@BankPK", _FundClientBankList.BankPK);
                                cmd.Parameters.AddWithValue("@AccountName", _FundClientBankList.AccountName);
                                cmd.Parameters.AddWithValue("@AccountNo", _FundClientBankList.AccountNo);
                                cmd.Parameters.AddWithValue("@NoBank", _FundClientBankList.NoBank);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _FundClientBankList.CurrencyPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankList.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientBankList set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where FundClientBankListPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientBankList.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankList.HistoryPK);
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

        public void FundClientBankList_Approved(FundClientBankList _FundClientBankList)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankList set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundClientBankListPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankList.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankList.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientBankList set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientBankListPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankList.ApprovedUsersID);
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

        public void FundClientBankList_Reject(FundClientBankList _FundClientBankList)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankList set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundClientBankListPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankList.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankList.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientBankList set status= 2,lastupdate=@lastupdate where FundClientBankListPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
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

        public void FundClientBankList_Void(FundClientBankList _FundClientBankList)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankList set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where FundClientBankListPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankList.FundClientBankListPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankList.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankList.VoidUsersID);
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

        public bool CheckHassAdd(int _fundclientpk, int _bankpk, int _noBank)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"if EXISTS (
                        select * from FundClientBankList where FundClientPK = @FundClientPK and NoBank = @NoBank and status in(1,2)
                        )
                        BEGIN
	                        select 1 Result
                        END
                        ELSE
                        BEGIN
	                        select 0 Result
                        END";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundclientpk);
                        cmd.Parameters.AddWithValue("@BankPK", _bankpk);
                        cmd.Parameters.AddWithValue("@NoBank", _noBank);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)

                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        private FundClientBankList setFundClientBankListCombo(SqlDataReader dr)
        {
            FundClientBankList M_FundClientBankList = new FundClientBankList();
            M_FundClientBankList.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientBankList.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
            M_FundClientBankList.BankID = dr["BankID"].ToString();
            M_FundClientBankList.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_FundClientBankList.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_FundClientBankList.AccountName = dr["AccountName"].ToString();
            M_FundClientBankList.AccountNo = dr["AccountNo"].ToString();
            M_FundClientBankList.NoBank = Convert.ToString(dr["NoBank"]);
            return M_FundClientBankList;
        }

        public List<FundClientBankList> FundClientBankList_GetDataCombo(int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientBankList> L_FundClientBankList = new List<FundClientBankList>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                            cmd.CommandText = @"
                                    select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                    C.ID BankID,B.ID CurrencyID,* from FundClientBankList A 
                                    left Join Currency B on A.CurrencyPK = B.CurrencyPK and B.Status in(1,2)
                                    left Join Bank C on A.BankPK = C.BankPK and C.Status in(1,2)  
                                    where A.status = 2 and A.FundClientPK = @FundClientPK
                            ";
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientBankList.Add(setFundClientBankListCombo(dr));
                                }
                            }
                            return L_FundClientBankList;
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