using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class CorporateArApPaymentReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CorporateArApPayment] " +
                            "([CorporateArApPaymentPK],[HistoryPK],[Status],[ValueDate],[Amount],[CashRefPK],[Type],[CheckNo],[CorporateArApPK],[Description],[CurrencyPK],[LateAmountCharge],";
        string _paramaterCommand = "@ValueDate,@Amount,@CashRefPK,@Type,@CheckNo,@CorporateArApPK,@Description,@CurrencyPK,@LateAmountCharge,";

        private CorporateArApPayment setCorporateArApPayment(SqlDataReader dr)
        {
            CorporateArApPayment M_CorporateArApPayment = new CorporateArApPayment();
            M_CorporateArApPayment.CorporateArApPaymentPK = Convert.ToInt32(dr["CorporateArApPaymentPK"]);
            M_CorporateArApPayment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CorporateArApPayment.Status = Convert.ToInt32(dr["Status"]);
            M_CorporateArApPayment.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CorporateArApPayment.Notes = Convert.ToString(dr["Notes"]);
            M_CorporateArApPayment.ValueDate = dr["ValueDate"].ToString();
            M_CorporateArApPayment.Type = Convert.ToInt32(dr["Type"]);
            M_CorporateArApPayment.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_CorporateArApPayment.Amount = Convert.ToString(dr["Amount"]);
            M_CorporateArApPayment.CheckNo = Convert.ToString(dr["CheckNo"]);
            M_CorporateArApPayment.CorporateArApPK = Convert.ToInt32(dr["CorporateArApPK"]);
            M_CorporateArApPayment.CorporateArApID = Convert.ToString(dr["CorporateArApID"]);
            M_CorporateArApPayment.Description = Convert.ToString(dr["Description"]);
            M_CorporateArApPayment.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_CorporateArApPayment.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_CorporateArApPayment.LateAmountCharge = Convert.ToDecimal(dr["LateAmountCharge"]);
            M_CorporateArApPayment.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
            M_CorporateArApPayment.CashRefID = Convert.ToString(dr["CashRefID"]);

            M_CorporateArApPayment.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CorporateArApPayment.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CorporateArApPayment.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CorporateArApPayment.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CorporateArApPayment.EntryTime = dr["EntryTime"].ToString();
            M_CorporateArApPayment.UpdateTime = dr["UpdateTime"].ToString();
            M_CorporateArApPayment.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CorporateArApPayment.VoidTime = dr["VoidTime"].ToString();
            M_CorporateArApPayment.DBUserID = dr["DBUserID"].ToString();
            M_CorporateArApPayment.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CorporateArApPayment.LastUpdate = dr["LastUpdate"].ToString();
            M_CorporateArApPayment.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_CorporateArApPayment;
        }

        public List<CorporateArApPayment> CorporateArApPayment_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CorporateArApPayment> L_CorporateArApPayment = new List<CorporateArApPayment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID CashRefID, C.ID CurrencyID, D.AccountDebitPK CorporateArApID, * from CorporateArApPayment A
							left join Cashref B on A.CashRefPK = B.CashRefPK and B.status  = 2
                            left join Currency C on A.CurrencyPK = C.CurrencyPK and C.Status = 2
                            left join CorporateArAp D on A.CorporateArApPK = D.CorporateArApPK and D.Status = 2
							where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID CashRefID, C.ID CurrencyID, D.AccountDebitPK CorporateArApID, * from CorporateArApPayment A
							left join Cashref B on A.CashRefPK = B.CashRefPK and B.status  = 2
                            left join Currency C on A.CurrencyPK = C.CurrencyPK and C.Status = 2
                            left join CorporateArAp D on A.CorporateArApPK = D.CorporateArApPK and D.Status = 2";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CorporateArApPayment.Add(setCorporateArApPayment(dr));
                                }
                            }
                            return L_CorporateArApPayment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CorporateArApPayment_Add(CorporateArApPayment _CorporateArApPayment, bool _havePrivillege)
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
                                 "Select isnull(max(CorporateArApPaymentPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CorporateArApPayment";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateArApPayment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CorporateArApPaymentPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CorporateArApPayment";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ValueDate", _CorporateArApPayment.ValueDate);
                        cmd.Parameters.AddWithValue("@Type", _CorporateArApPayment.Type);
                        cmd.Parameters.AddWithValue("@CashRefPK", _CorporateArApPayment.CashRefPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _CorporateArApPayment.CurrencyPK);
                        cmd.Parameters.AddWithValue("@Amount", _CorporateArApPayment.Amount);
                        cmd.Parameters.AddWithValue("@CorporateArApPK", _CorporateArApPayment.CorporateArApPK);
                        cmd.Parameters.AddWithValue("@Description", _CorporateArApPayment.Description);
                        cmd.Parameters.AddWithValue("@LateAmountCharge", _CorporateArApPayment.LateAmountCharge);
                        cmd.Parameters.AddWithValue("@CheckNo", _CorporateArApPayment.CheckNo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CorporateArApPayment.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CorporateArApPayment");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CorporateArApPayment_Update(CorporateArApPayment _CorporateArApPayment, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CorporateArApPayment.CorporateArApPaymentPK, _CorporateArApPayment.HistoryPK, "CorporateArApPayment");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateArApPayment set status=2, Notes=@Notes,ValueDate=@ValueDate,Type=@Type,Amount=@Amount,CurrencyPK=@CurrencyPK,CashRefPK=@CashRefPK,Description=@Description,LateAmountCharge=@LateAmountCharge,CheckNo=@CheckNo, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CorporateArApPaymentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CorporateArApPayment.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _CorporateArApPayment.ValueDate);
                            cmd.Parameters.AddWithValue("@Type", _CorporateArApPayment.Type);
                            cmd.Parameters.AddWithValue("@CashRefPK", _CorporateArApPayment.CashRefPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _CorporateArApPayment.CurrencyPK);
                            cmd.Parameters.AddWithValue("@Amount", _CorporateArApPayment.Amount);
                            cmd.Parameters.AddWithValue("@Description", _CorporateArApPayment.Description);
                            cmd.Parameters.AddWithValue("@LateAmountCharge", _CorporateArApPayment.LateAmountCharge);
                            cmd.Parameters.AddWithValue("@CheckNo", _CorporateArApPayment.CheckNo);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateArApPayment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateArApPayment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateArApPayment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CorporateArApPaymentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateArApPayment.EntryUsersID);
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
                                cmd.CommandText = @"Update CorporateArApPayment set Notes=@Notes,ValueDate=@ValueDate,Type=@Type,Amount=@Amount,CurrencyPK=@CurrencyPK,CashRefPK=@CashRefPK,Description=@Description,LateAmountCharge=@LateAmountCharge,CheckNo=@CheckNo, 
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate
                                where CorporateArApPaymentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateArApPayment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                                cmd.Parameters.AddWithValue("@Notes", _CorporateArApPayment.Notes);
                                cmd.Parameters.AddWithValue("@ValueDate", _CorporateArApPayment.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _CorporateArApPayment.Type);
                                cmd.Parameters.AddWithValue("@CashRefPK", _CorporateArApPayment.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _CorporateArApPayment.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Amount", _CorporateArApPayment.Amount);
                                cmd.Parameters.AddWithValue("@Description", _CorporateArApPayment.Description);
                                cmd.Parameters.AddWithValue("@LateAmountCharge", _CorporateArApPayment.LateAmountCharge);
                                cmd.Parameters.AddWithValue("@CheckNo", _CorporateArApPayment.CheckNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateArApPayment.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CorporateArApPayment.CorporateArApPaymentPK, "CorporateArApPayment");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From CorporateArApPayment where CorporateArApPaymentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateArApPayment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _CorporateArApPayment.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _CorporateArApPayment.Type);
                                cmd.Parameters.AddWithValue("@CashRefPK", _CorporateArApPayment.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _CorporateArApPayment.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Amount", _CorporateArApPayment.Amount);
                                cmd.Parameters.AddWithValue("@Description", _CorporateArApPayment.Description);
                                cmd.Parameters.AddWithValue("@LateAmountCharge", _CorporateArApPayment.LateAmountCharge);
                                cmd.Parameters.AddWithValue("@CheckNo", _CorporateArApPayment.CheckNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateArApPayment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CorporateArApPayment set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where CorporateArApPaymentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CorporateArApPayment.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateArApPayment.HistoryPK);
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


        public void CorporateArApPayment_Approved(CorporateArApPayment _CorporateArApPayment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateArApPayment set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where CorporateArApPaymentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateArApPayment.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateArApPayment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateArApPayment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CorporateArApPaymentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateArApPayment.ApprovedUsersID);
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

        public void CorporateArApPayment_Reject(CorporateArApPayment _CorporateArApPayment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateArApPayment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where CorporateArApPaymentpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateArApPayment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateArApPayment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateArApPayment set status= 2,LastUpdate=@LastUpdate  where CorporateArApPaymentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
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

        public void CorporateArApPayment_Void(CorporateArApPayment _CorporateArApPayment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateArApPayment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CorporateArApPaymentpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateArApPayment.CorporateArApPaymentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateArApPayment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateArApPayment.VoidUsersID);
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


        public List<CorporateArApPayment> CorporateArApPayment_SelectDataByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CorporateArApPayment> L_CorporateArApPayment = new List<CorporateArApPayment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select Case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID CashRefID, C.ID CurrencyID, D.AccountDebitPK CorporateArApID,E.DescOne TypeDesc, * from CorporateArApPayment A
							left join Cashref B on A.CashRefPK = B.CashRefPK and B.status  = 2
                            left join Currency C on A.CurrencyPK = C.CurrencyPK and C.Status = 2
                            left join CorporateArAp D on A.CorporateArApPK = D.AccountDebitPK and D.Status = 1 
							left join MasterValue E on A.Type = E.Code and E.Status = 2
                            where A.status=@status and A.ValueDate between @DateFrom and @DateTo and E.ID='CorporateArApType' order by A.ValueDate ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select Case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, B.ID CashRefID, C.ID CurrencyID, D.AccountDebitPK CorporateArApID,E.DescOne TypeDesc, * from CorporateArApPayment A
							left join Cashref B on A.CashRefPK = B.CashRefPK and B.status  = 2
                            left join Currency C on A.CurrencyPK = C.CurrencyPK and C.Status = 2
                            left join CorporateArAp D on A.CorporateArApPK = D.AccountDebitPK and D.Status = 1 
							left join MasterValue E on A.Type = E.Code and E.Status = 2
                            where A.ValueDate between @DateFrom and @DateTo  and E.ID='CorporateArApType' order by A.ValueDate";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CorporateArApPayment.Add(setCorporateArApPayment(dr));
                                }
                            }
                            return L_CorporateArApPayment;
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