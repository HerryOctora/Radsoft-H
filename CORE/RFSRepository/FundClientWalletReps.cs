using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FundClientWalletReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientWallet] " +
                            "([FundClientWalletPK],[HistoryPK],[Status],[FundClientPK],[WalletPK],[BankPK],[Debit],[Credit],[WalletTransactionType],[WalletTransactionStatus],[IsProcessed],[Description],[Balance],";
        string _paramaterCommand = "@FundClientPK,@WalletPK,@BankPK,@Debit,@Credit,@WalletTransactionType,@WalletTransactionStatus,@IsProcessed,@Description,@Balance,";

        //2
        private FundClientWallet setFundClientWallet(SqlDataReader dr)
        {
            FundClientWallet M_FundClientWallet = new FundClientWallet();
            M_FundClientWallet.FundClientWalletPK = Convert.ToInt32(dr["FundClientWalletPK"]);
            M_FundClientWallet.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientWallet.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientWallet.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientWallet.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientWallet.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientWallet.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundClientWallet.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_FundClientWallet.WalletPK = Convert.ToString(dr["WalletPK"]);
            //M_FundClientWallet.WalletID = Convert.ToString(dr["WalletID"]);
            M_FundClientWallet.BankPK = Convert.ToInt32(dr["BankPK"]);
            M_FundClientWallet.BankID = Convert.ToString(dr["BankID"]);
            M_FundClientWallet.Debit = Convert.ToDecimal(dr["Debit"]);
            M_FundClientWallet.Credit = Convert.ToDecimal(dr["Credit"]);
            M_FundClientWallet.WalletTransactionType = Convert.ToInt32(dr["WalletTransactionType"]);
            M_FundClientWallet.WalletTransactionStatus = Convert.ToInt32(dr["WalletTransactionStatus"]);
            M_FundClientWallet.IsProcessed = Convert.ToBoolean(dr["IsProcessed"]);
            M_FundClientWallet.Description = Convert.ToString(dr["Description"]);
            M_FundClientWallet.Balance = Convert.ToDecimal(dr["Balance"]);           
            M_FundClientWallet.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientWallet.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientWallet.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientWallet.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientWallet.EntryTime = dr["EntryTime"].ToString();
            M_FundClientWallet.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientWallet.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientWallet.VoidTime = dr["VoidTime"].ToString();
            M_FundClientWallet.DBUserID = dr["DBUserID"].ToString();
            M_FundClientWallet.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientWallet.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientWallet.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientWallet;
        }

        public List<FundClientWallet> FundClientWallet_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientWallet> L_FundClientWallet = new List<FundClientWallet>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundClientName,B.ID FundClientID,D.Name BankName,D.ID BankID, A.* from FundClientWallet A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status  = 2
                            left join Bank D on A.BankPK = D.BankPK and D.status = 2
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundClientName,B.ID FundClientID, D.Name BankName,D.ID BankID, A.* from FundClientWallet A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status  = 2
                            left join Bank D on A.BankPK = D.BankPK and D.status = 2";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientWallet.Add(setFundClientWallet(dr));
                                }
                            }
                            return L_FundClientWallet;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientWallet_Add(FundClientWallet _FundClientWallet, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(FundClientWalletPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundClientWallet";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientWallet.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundClientWalletPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundClientWallet";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientWallet.FundClientPK);
                        cmd.Parameters.AddWithValue("@WalletPK", _FundClientWallet.WalletPK);
                        cmd.Parameters.AddWithValue("@BankPK", _FundClientWallet.BankPK);
                        cmd.Parameters.AddWithValue("@Debit", _FundClientWallet.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _FundClientWallet.Credit);
                        cmd.Parameters.AddWithValue("@WalletTransactionType", _FundClientWallet.WalletTransactionType);
                        cmd.Parameters.AddWithValue("@WalletTransactionStatus", _FundClientWallet.WalletTransactionStatus);
                        cmd.Parameters.AddWithValue("@IsProcessed", _FundClientWallet.IsProcessed);
                        cmd.Parameters.AddWithValue("@Description", _FundClientWallet.Description);
                        cmd.Parameters.AddWithValue("@Balance", _FundClientWallet.Balance);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientWallet.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundClientWallet");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientWallet_Update(FundClientWallet _FundClientWallet, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundClientWallet.FundClientWalletPK, _FundClientWallet.HistoryPK, "FundClientWallet");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientWallet set status=2, Notes=@Notes,FundClientPK=@FundClientPK,WalletPK=@WalletPK,BankPK=@BankPK,Debit=@Debit,Credit=@Credit,WalletTransactionType=@WalletTransactionType,WalletTransactionStatus=@WalletTransactionStatus,IsProcessed=@IsProcessed,Description=@Description,Balance=@Balance,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientWalletPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientWallet.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientWallet.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientWallet.FundClientPK);
                            cmd.Parameters.AddWithValue("@WalletPK", _FundClientWallet.WalletPK);
                            cmd.Parameters.AddWithValue("@BankPK", _FundClientWallet.BankPK);
                            cmd.Parameters.AddWithValue("@Debit", _FundClientWallet.Debit);
                            cmd.Parameters.AddWithValue("@Credit", _FundClientWallet.Credit);
                            cmd.Parameters.AddWithValue("@WalletTransactionType", _FundClientWallet.WalletTransactionType);
                            cmd.Parameters.AddWithValue("@WalletTransactionStatus", _FundClientWallet.WalletTransactionStatus);
                            cmd.Parameters.AddWithValue("@IsProcessed", _FundClientWallet.IsProcessed);
                            cmd.Parameters.AddWithValue("@Description", _FundClientWallet.Description);
                            cmd.Parameters.AddWithValue("@Balance", _FundClientWallet.Balance);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientWallet.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientWallet.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientWallet set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientWalletPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientWallet.EntryUsersID);
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
                                cmd.CommandText = "Update FundClientWallet set Notes=@Notes,FundClientPK=@FundClientPK,WalletPK=@WalletPK,BankPK=@BankPK,Debit=@Debit,Credit=@Credit,WalletTransactionType=@WalletTransactionType,WalletTransactionStatus=@WalletTransactionStatus,IsProcessed=@IsProcessed,Description=@Description,Balance=@Balance," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientWalletPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientWallet.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientWallet.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientWallet.FundClientPK);
                                cmd.Parameters.AddWithValue("@WalletPK", _FundClientWallet.WalletPK);
                                cmd.Parameters.AddWithValue("@BankPK", _FundClientWallet.BankPK);
                                cmd.Parameters.AddWithValue("@Debit", _FundClientWallet.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _FundClientWallet.Credit);
                                cmd.Parameters.AddWithValue("@WalletTransactionType", _FundClientWallet.WalletTransactionType);
                                cmd.Parameters.AddWithValue("@WalletTransactionStatus", _FundClientWallet.WalletTransactionStatus);
                                cmd.Parameters.AddWithValue("@IsProcessed", _FundClientWallet.IsProcessed);
                                cmd.Parameters.AddWithValue("@Description", _FundClientWallet.Description);
                                cmd.Parameters.AddWithValue("@Balance", _FundClientWallet.Balance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientWallet.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientWallet.FundClientWalletPK, "FundClientWallet");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From FundClientWallet where FundClientWalletPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientWallet.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientWallet.FundClientPK);
                                cmd.Parameters.AddWithValue("@WalletPK", _FundClientWallet.WalletPK);
                                cmd.Parameters.AddWithValue("@BankPK", _FundClientWallet.BankPK);
                                cmd.Parameters.AddWithValue("@Debit", _FundClientWallet.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _FundClientWallet.Credit);
                                cmd.Parameters.AddWithValue("@WalletTransactionType", _FundClientWallet.WalletTransactionType);
                                cmd.Parameters.AddWithValue("@WalletTransactionStatus", _FundClientWallet.WalletTransactionStatus);
                                cmd.Parameters.AddWithValue("@IsProcessed", _FundClientWallet.IsProcessed);
                                cmd.Parameters.AddWithValue("@Description", _FundClientWallet.Description);
                                cmd.Parameters.AddWithValue("@Balance", _FundClientWallet.Balance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientWallet.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientWallet set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where FundClientWalletPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientWallet.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientWallet.HistoryPK);
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

        public void FundClientWallet_Approved(FundClientWallet _FundClientWallet)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientWallet set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundClientWalletpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientWallet.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientWallet.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientWallet set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientWalletPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientWallet.ApprovedUsersID);
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

        public void FundClientWallet_Reject(FundClientWallet _FundClientWallet)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientWallet set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where FundClientWalletpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientWallet.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientWallet.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientWallet set status= 2,LastUpdate=@LastUpdate  where FundClientWalletPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
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

        public void FundClientWallet_Void(FundClientWallet _FundClientWallet)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientWallet set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientWalletpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientWallet.FundClientWalletPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientWallet.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientWallet.VoidUsersID);
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