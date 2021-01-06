using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class JournalDetailReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[JournalDetail] " +
                            "([HistoryPK],[JournalPK],[AutoNo],[Status],[AccountPK],[CurrencyPK],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription],[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@AccountPK,@CurrencyPK,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@DetailDescription,@DocRef,@DebitCredit,@Amount,@Debit,@Credit,@CurrencyRate,@BaseDebit,@BaseCredit,@LastUsersID,@LastUpdate";

        //2
        private JournalDetail setJournalDetail(SqlDataReader dr)
        {
            JournalDetail M_JournalDetail = new JournalDetail();
            M_JournalDetail.JournalPK = Convert.ToInt32(dr["JournalPK"]);
            M_JournalDetail.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_JournalDetail.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_JournalDetail.Status = Convert.ToInt32(dr["Status"]);
            M_JournalDetail.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_JournalDetail.AccountID = Convert.ToString(dr["AccountID"]);
            M_JournalDetail.AccountName = Convert.ToString(dr["AccountName"]);
            M_JournalDetail.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_JournalDetail.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_JournalDetail.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_JournalDetail.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_JournalDetail.OfficeName = Convert.ToString(dr["OfficeName"]);
            M_JournalDetail.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_JournalDetail.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_JournalDetail.DepartmentName = Convert.ToString(dr["DepartmentName"]);
            M_JournalDetail.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_JournalDetail.AgentID = Convert.ToString(dr["AgentID"]);
            M_JournalDetail.AgentName = Convert.ToString(dr["AgentName"]);
            M_JournalDetail.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_JournalDetail.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_JournalDetail.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            M_JournalDetail.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_JournalDetail.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_JournalDetail.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_JournalDetail.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_JournalDetail.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            M_JournalDetail.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
            M_JournalDetail.DetailDescription = Convert.ToString(dr["DetailDescription"]);
            M_JournalDetail.DocRef = Convert.ToString(dr["DocRef"]);
            M_JournalDetail.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_JournalDetail.Amount = Convert.ToDecimal(dr["Amount"]);
            M_JournalDetail.Debit = Convert.ToDecimal(dr["Debit"]);
            M_JournalDetail.Credit = Convert.ToDecimal(dr["Credit"]);
            M_JournalDetail.CurrencyRate = Convert.ToDecimal(dr["CurrencyRate"]);
            M_JournalDetail.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            M_JournalDetail.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            M_JournalDetail.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_JournalDetail.LastUpdate = dr["LastUpdate"].ToString();
            M_JournalDetail.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_JournalDetail;
        }

        //3
        public List<JournalDetail> JournalDetail_Select(int _status, int _journalPK)
        {
            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<JournalDetail> L_JournalDetail = new List<JournalDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select a.ID AccountID, a.Name AccountName,c.ID CurrencyID,  " +   
                             " o.ID OfficeID,o.Name OfficeName,d.id DepartmentID, d.Name DepartmentName,ag.ID AgentID,ag.Name AgentName,     " +
                             " cp.ID CounterpartID,cp.Name CounterpartName,i.ID InstrumentID,i.Name InstrumentName,co.ID ConsigneeID,Co.Name ConsigneeName, " +
                             " jd.* from JournalDetail jd Left join     " +
                             " Account a on jd.AccountPK = a.AccountPK and a.Status = 2 Left join     " +
                             " Currency c on jd.CurrencyPK = c.CurrencyPK and c.Status =2 Left join    " + 
                             " Office o on jd.OfficePK = o.OfficePK and o.Status = 2 Left join     " +
                             " Department d on jd.DepartmentPK = d.DepartmentPK and d.Status = 2 Left join     " +
                             " Agent ag on jd.AgentPK = ag.AgentPK and ag.Status =2 Left join     " +
                             " Counterpart cp on jd.CounterpartPK = cp.CounterpartPK and cp.Status = 2 Left join     " +
                             " Instrument i on jd.InstrumentPK = i.InstrumentPK  and I.Status = 2 left join " +
                             " Consignee co  on jd.consigneePK = co.consigneePK  and co.Status = 2  " +
                             " where JournalPK = @JournalPK and jd.Status = @Status " +       
                             "order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@JournalPK", _journalPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from JournalDetail where JournalPK = @JournalPK order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@JournalPK", _journalPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_JournalDetail.Add(setJournalDetail(dr));
                                }
                            }
                            return L_JournalDetail;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        //4
        public int JournalDetail_Add(JournalDetail _journalDetail)
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
                        _autoNo = _host.Get_DetailNewAutoNo(_journalDetail.JournalPK,"JournalDetail","JournalPK");
                        cmd.CommandText =
                                  "update journal set lastupdate = @Lastupdate where journalPK = @JournalPK and status = 1 \n " +
                                  "update journal set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where journalPK = @JournalPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,@JournalPK,@AutoNo,@status," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@JournalPK", _journalDetail.JournalPK);
                        cmd.Parameters.AddWithValue("@status", _journalDetail.Status);
                        cmd.Parameters.AddWithValue("@AutoNo", _autoNo);
                        cmd.Parameters.AddWithValue("@AccountPK", _journalDetail.AccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _journalDetail.CurrencyPK);
                        cmd.Parameters.AddWithValue("@OfficePK", _journalDetail.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _journalDetail.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _journalDetail.AgentPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _journalDetail.CounterpartPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _journalDetail.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _journalDetail.ConsigneePK);
                        cmd.Parameters.AddWithValue("@DetailDescription", _journalDetail.DetailDescription);
                        cmd.Parameters.AddWithValue("@DocRef", _journalDetail.DocRef);
                        cmd.Parameters.AddWithValue("@DebitCredit", _journalDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _journalDetail.Amount);
                        cmd.Parameters.AddWithValue("@Debit", _journalDetail.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _journalDetail.Credit);
                        cmd.Parameters.AddWithValue("@CurrencyRate", _journalDetail.CurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _journalDetail.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _journalDetail.BaseCredit);
                        cmd.Parameters.AddWithValue("@LastUsersID", _journalDetail.LastUsersID);
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
        public void JournalDetail_Update(JournalDetail _journalDetail)
        {
             try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update journal set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where journalPK = @JournalPK and status = 2 \n "+
                            "Update JournalDetail " +
                            "Set AccountPK = @AccountPK,CurrencyPK = @CurrencyPK,OfficePK = @OfficePK, " +
                            "DepartmentPK = @DepartmentPK,AgentPK = @AgentPK,CounterpartPK = @CounterpartPK,InstrumentPK = @InstrumentPK, " +
                            "ConsigneePK = @ConsigneePK ,DetailDescription = @DetailDescription,DocRef = @DocRef,DebitCredit = @DebitCredit, " +
                            "Amount = @Amount,Debit = @Debit,Credit = @Credit,CurrencyRate = @CurrencyRate, " +
                            "BaseDebit= @BaseDebit, BaseCredit = @BaseCredit,LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                            "Where JournalPK = @JournalPK and AutoNo = @AutoNo ";

                        cmd.Parameters.AddWithValue("@AccountPK", _journalDetail.AccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _journalDetail.CurrencyPK);
                        cmd.Parameters.AddWithValue("@OfficePK", _journalDetail.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _journalDetail.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _journalDetail.AgentPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _journalDetail.CounterpartPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _journalDetail.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _journalDetail.ConsigneePK);
                        cmd.Parameters.AddWithValue("@DetailDescription", _journalDetail.DetailDescription);
                        cmd.Parameters.AddWithValue("@DocRef", _journalDetail.DocRef);
                        cmd.Parameters.AddWithValue("@DebitCredit", _journalDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _journalDetail.Amount);
                        cmd.Parameters.AddWithValue("@Debit", _journalDetail.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _journalDetail.Credit);
                        cmd.Parameters.AddWithValue("@CurrencyRate", _journalDetail.CurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _journalDetail.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _journalDetail.BaseCredit);
                        cmd.Parameters.AddWithValue("@JournalPK", _journalDetail.JournalPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _journalDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@LastUsersID", _journalDetail.LastUsersID);
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

        public void JournalDetail_Delete(JournalDetail _journalDetail)
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
                            "update journal set  lastupdate = @Lastupdate where journalPK = @JournalPK and status = 1 \n " +
                            "update journal set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where journalPK = @JournalPK and status = 2 \n " +
                            "delete journalDetail where journalPK = @JournalPK and AutoNo = @AutoNo ";
                        cmd.Parameters.AddWithValue("@JournalPK", _journalDetail.JournalPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _journalDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _journalDetail.LastUsersID);
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