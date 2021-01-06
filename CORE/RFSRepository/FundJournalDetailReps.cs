using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FundJournalDetailReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundJournalDetail] " +
                            "([HistoryPK],[FundJournalPK],[AutoNo],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@FundJournalAccountPK,@CurrencyPK,@FundPK,@FundClientPK,@InstrumentPK,@DetailDescription,@DebitCredit,@Amount,@Debit,@Credit,@CurrencyRate,@BaseDebit,@BaseCredit,@LastUsersID,@LastUpdate";

        //2
        private FundJournalDetail setFundJournalDetail(SqlDataReader dr)
        {
            FundJournalDetail M_FundJournalDetail = new FundJournalDetail();
            M_FundJournalDetail.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
            M_FundJournalDetail.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_FundJournalDetail.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundJournalDetail.Status = Convert.ToInt32(dr["Status"]);
            M_FundJournalDetail.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_FundJournalDetail.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_FundJournalDetail.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_FundJournalDetail.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_FundJournalDetail.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_FundJournalDetail.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundJournalDetail.FundID = Convert.ToString(dr["FundID"]);
            M_FundJournalDetail.FundName = Convert.ToString(dr["FundName"]);
            M_FundJournalDetail.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundJournalDetail.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundJournalDetail.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_FundJournalDetail.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_FundJournalDetail.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_FundJournalDetail.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_FundJournalDetail.DetailDescription = Convert.ToString(dr["DetailDescription"]);
            M_FundJournalDetail.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_FundJournalDetail.Amount = Convert.ToDecimal(dr["Amount"]);
            M_FundJournalDetail.Debit = Convert.ToDecimal(dr["Debit"]);
            M_FundJournalDetail.Credit = Convert.ToDecimal(dr["Credit"]);
            M_FundJournalDetail.CurrencyRate = Convert.ToDecimal(dr["CurrencyRate"]);
            M_FundJournalDetail.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            M_FundJournalDetail.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            M_FundJournalDetail.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_FundJournalDetail.LastUpdate = dr["LastUpdate"].ToString();
            M_FundJournalDetail.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundJournalDetail;
        }

        public List<FundJournalDetail> FundJournalDetail_Select(int _status, int _FundJournalPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalDetail> L_FundJournalDetail = new List<FundJournalDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select a.ID FundJournalAccountID, a.Name FundJournalAccountName,c.ID CurrencyID,  " +
                             " F.ID FundID,F.Name FundName,FC.id FundClientID, FC.Name FundClientName,I.id InstrumentID, I.Name InstrumentName, " +
                             " jd.* from FundJournalDetail jd Left join     " +
                             " FundJournalAccount a on jd.FundJournalAccountPK = a.FundJournalAccountPK and a.Status = 2 Left join     " +
                             " Currency c on jd.CurrencyPK = c.CurrencyPK and c.Status =2 Left join    " +
                             " Fund F on jd.FundPK = F.FundPK and F.Status = 2 Left join     " +
                             " Instrument I on jd.InstrumentPK = I.InstrumentPK and I.Status = 2 Left join     " +
                             " FundClient FC on jd.FundClientPK = FC.FundClientPK and FC.Status = 2     " +
                             " where FundJournalPK = @FundJournalPK and jd.Status = @Status " +
                             "order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select a.ID FundJournalAccountID, a.Name FundJournalAccountName,c.ID CurrencyID,  " +
                             " F.ID FundID,F.Name FundName,FC.id FundClientID, FC.Name FundClientName,I.id InstrumentID, I.Name InstrumentName, " +
                             " jd.* from FundJournalDetail jd Left join     " +
                             " FundJournalAccount a on jd.FundJournalAccountPK = a.FundJournalAccountPK and a.Status = 2 Left join     " +
                             " Currency c on jd.CurrencyPK = c.CurrencyPK and c.Status =2 Left join    " +
                             " Fund F on jd.FundPK = F.FundPK and F.Status = 2 Left join     " +
                             " Instrument I on jd.InstrumentPK = I.InstrumentPK and I.Status = 2 Left join     " +
                             " FundClient FC on jd.FundClientPK = FC.FundClientPK and FC.Status = 2     " +
                             " where FundJournalPK = @FundJournalPK order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundJournalDetail.Add(setFundJournalDetail(dr));
                                }
                            }
                            return L_FundJournalDetail;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundJournalDetail_Add(FundJournalDetail _FundJournalDetail)
        {
            try
            {
                int _autoNo = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _autoNo = _host.Get_DetailNewAutoNo(_FundJournalDetail.FundJournalPK, "FundJournalDetail", "FundJournalPK");
                        cmd.CommandText =
                                  "update FundJournal set lastupdate = @Lastupdate where FundJournalPK = @FundJournalPK and status = 1 \n " +
                                  "update FundJournal set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FundJournalPK = @FundJournalPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,@FundJournalPK,@AutoNo,@status," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalDetail.FundJournalPK);
                        cmd.Parameters.AddWithValue("@status", _FundJournalDetail.Status);
                        cmd.Parameters.AddWithValue("@AutoNo", _autoNo);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FundJournalDetail.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _FundJournalDetail.CurrencyPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundJournalDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundJournalDetail.FundClientPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FundJournalDetail.InstrumentPK);   
                        cmd.Parameters.AddWithValue("@DetailDescription", _FundJournalDetail.DetailDescription);
                        cmd.Parameters.AddWithValue("@DebitCredit", _FundJournalDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _FundJournalDetail.Amount);
                        cmd.Parameters.AddWithValue("@Debit", _FundJournalDetail.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _FundJournalDetail.Credit);
                        cmd.Parameters.AddWithValue("@CurrencyRate", _FundJournalDetail.CurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _FundJournalDetail.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _FundJournalDetail.BaseCredit);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalDetail.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();
                        return _autoNo;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void FundJournalDetail_Update(FundJournalDetail _FundJournalDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update FundJournal set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where FundJournalPK = @FundJournalPK and status = 2 \n " +
                            "Update FundJournalDetail " +
                            "Set FundJournalAccountPK = @FundJournalAccountPK,CurrencyPK = @CurrencyPK,FundPK = @FundPK, " +
                            "FundClientPK = @FundClientPK,InstrumentPK = @InstrumentPK,DetailDescription = @DetailDescription,DebitCredit = @DebitCredit, " +
                            "Amount = @Amount,Debit = @Debit,Credit = @Credit,CurrencyRate = @CurrencyRate, " +
                            "BaseDebit= @BaseDebit, BaseCredit = @BaseCredit,LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                            "Where FundJournalPK = @FundJournalPK and AutoNo = @AutoNo ";

                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FundJournalDetail.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _FundJournalDetail.CurrencyPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundJournalDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundJournalDetail.FundClientPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FundJournalDetail.InstrumentPK);  
                        cmd.Parameters.AddWithValue("@DetailDescription", _FundJournalDetail.DetailDescription);
                        cmd.Parameters.AddWithValue("@DebitCredit", _FundJournalDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _FundJournalDetail.Amount);
                        cmd.Parameters.AddWithValue("@Debit", _FundJournalDetail.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _FundJournalDetail.Credit);
                        cmd.Parameters.AddWithValue("@CurrencyRate", _FundJournalDetail.CurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _FundJournalDetail.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _FundJournalDetail.BaseCredit);
                        cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalDetail.FundJournalPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FundJournalDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalDetail.LastUsersID);
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

        public void FundJournalDetail_Delete(FundJournalDetail _FundJournalDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            "update FundJournal set  lastupdate = @Lastupdate where FundJournalPK = @FundJournalPK and status = 1 \n " +
                            "update FundJournal set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FundJournalPK = @FundJournalPK and status = 2 \n " +
                            "delete FundJournalDetail where FundJournalPK = @FundJournalPK and AutoNo = @AutoNo ";
                        cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalDetail.FundJournalPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FundJournalDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalDetail.LastUsersID);
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