using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class CashierReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Cashier] " +
                            "([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Description],[DebitCredit],[DebitCurrencyPK], " +
                            " [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit], " +
                            " [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[CashierID],[Reference],[DocRef], ";

        string _paramaterCommand = "@PeriodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK," +
                            "@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,@BaseDebit,@BaseCredit,@PercentAmount,@FinalAmount,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@ConsigneePK,@InstrumentPK,@JournalNo,@CashierID,@Reference,@DocRef, ";

        private Cashier setCashier(SqlDataReader dr)
        {
            Cashier M_Cashier = new Cashier();
            M_Cashier.CashierPK = Convert.ToInt32(dr["CashierPK"]);
            M_Cashier.CashierID = Convert.ToInt64(dr["CashierID"]);
            M_Cashier.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Cashier.Selected = dr["Selected"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Selected"]);
            M_Cashier.Status = Convert.ToInt32(dr["Status"]);
            M_Cashier.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Cashier.Notes = Convert.ToString(dr["Notes"]);
            M_Cashier.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Cashier.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_Cashier.ValueDate = dr["ValueDate"].ToString();
            M_Cashier.Type = Convert.ToString(dr["Type"]);
            M_Cashier.RefNo = Convert.ToInt32(dr["RefNo"]);
            M_Cashier.Reference = dr["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reference"]);
            M_Cashier.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_Cashier.Description = Convert.ToString(dr["Description"]);
            M_Cashier.DebitCurrencyPK = Convert.ToInt32(dr["DebitCurrencyPK"]);
            M_Cashier.DebitCurrencyID = Convert.ToString(dr["DebitCurrencyID"]);
            M_Cashier.CreditCurrencyPK = Convert.ToInt32(dr["CreditCurrencyPK"]);
            M_Cashier.CreditCurrencyID = Convert.ToString(dr["CreditCurrencyID"]);
            M_Cashier.DebitCashRefPK = Convert.ToInt32(dr["DebitCashRefPK"]);
            M_Cashier.DebitCashRefID = Convert.ToString(dr["DebitCashRefID"]);
            M_Cashier.DebitCashRefName = Convert.ToString(dr["DebitCashRefName"]);
            M_Cashier.CreditCashRefPK = Convert.ToInt32(dr["CreditCashRefPK"]);
            M_Cashier.CreditCashRefID = Convert.ToString(dr["CreditCashRefID"]);
            M_Cashier.CreditCashRefName = Convert.ToString(dr["CreditCashRefName"]);
            M_Cashier.DebitAccountPK = Convert.ToInt32(dr["DebitAccountPK"]);
            M_Cashier.DebitAccountID = Convert.ToString(dr["DebitAccountID"]);
            M_Cashier.DebitAccountName = Convert.ToString(dr["DebitAccountName"]);
            M_Cashier.CreditAccountPK = Convert.ToInt32(dr["CreditAccountPK"]);
            M_Cashier.CreditAccountID = Convert.ToString(dr["CreditAccountID"]);
            M_Cashier.CreditAccountName = Convert.ToString(dr["CreditAccountName"]);
            M_Cashier.Debit = Convert.ToDecimal(dr["Debit"]);
            M_Cashier.Credit = Convert.ToDecimal(dr["Credit"]);
            M_Cashier.DebitCurrencyRate = Convert.ToDecimal(dr["DebitCurrencyRate"]);
            M_Cashier.CreditCurrencyRate = Convert.ToDecimal(dr["CreditCurrencyRate"]);
            M_Cashier.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            M_Cashier.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            M_Cashier.PercentAmount = Convert.ToDecimal(dr["PercentAmount"]);
            M_Cashier.FinalAmount = Convert.ToDecimal(dr["FinalAmount"]);
            M_Cashier.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Cashier.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_Cashier.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Cashier.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Cashier.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_Cashier.AgentID = Convert.ToString(dr["AgentID"]);
            M_Cashier.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_Cashier.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_Cashier.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_Cashier.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            M_Cashier.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_Cashier.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_Cashier.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_Cashier.JournalNo = Convert.ToInt32(dr["JournalNo"]);
            M_Cashier.DocRef = Convert.ToString(dr["DocRef"]);
            M_Cashier.Posted = Convert.ToBoolean(dr["Posted"]);
            M_Cashier.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_Cashier.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_Cashier.Revised = Convert.ToBoolean(dr["Revised"]);
            M_Cashier.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_Cashier.RevisedTime = Convert.ToString(dr["RevisedTime"]);
            M_Cashier.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Cashier.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Cashier.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Cashier.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Cashier.EntryTime = dr["EntryTime"].ToString();
            M_Cashier.UpdateTime = dr["UpdateTime"].ToString();
            M_Cashier.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Cashier.VoidTime = dr["VoidTime"].ToString();
            M_Cashier.DBUserID = dr["DBUserID"].ToString();
            M_Cashier.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Cashier.LastUpdate = dr["LastUpdate"].ToString();
            M_Cashier.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Cashier;
        }
        public List<Cashier> Cashier_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Cashier> L_Cashier = new List<Cashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            " Select B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                            " G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID,I.Name DebitCashRefName, J.ID CreditCashRefID,    " +
                            " J.Name CreditCashRefName,K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID, L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID,N.Name InstrumentName,   " +
                            " A.* From Cashier A Left join     " +
                            " Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 Left join     " +
                            " Office C on A.OfficePK = C.OfficePK and C.Status = 2 Left join     " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.Status = 2 Left join     " +
                            " Agent E on A.AgentPK = E.AgentPK and E.Status = 2 Left join     " +
                            " Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status = 2 Left join    " +
                            " Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.Status = 2 Left join     " +
                            " Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.Status = 2 Left join     " +
                            " CashRef I on A.DebitCashRefPK = I.CashRefPK and I.Status = 2 Left join     " +
                            " Cashref J on A.CreditCashRefPK = J.CashRefPK and J.Status = 2 Left join     " +
                            " Account K on A.DebitAccountPK = K.AccountPK and K.Status = 2 Left join     " +
                            " Account L on A.CreditAccountPK = L.AccountPK and L.Status = 2 Left join    " +
                            " Consignee M on A.ConsigneePK = M.ConsigneePK and M.Status = 2 Left join   " +
                            " Instrument N on A.InstrumentPK = N.InstrumentPK and N.Status = 2  " +
                            " Where A.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from Cashier order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Cashier.Add(setCashier(dr));
                                }
                            }
                            return L_Cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public CashierAddNew Cashier_Add(Cashier _cashier, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newCashierPK int \n " +
                                 "Select @newCashierPK = isnull(max(CashierPk),0) + 1 from Cashier \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select @newCashierPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newCashierPK newCashierPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newCashierPK int \n " +
                                 "Select @newCashierPK = isnull(max(CashierPk),0) + 1 from Cashier \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newCashierPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newCashierPK newCashierPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                        cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                        //cmd.Parameters.AddWithValue("@Reference", "");
                        cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                        cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                        cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                        cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                        cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                        cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                        cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                        cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                        cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                        cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                        cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                        cmd.Parameters.AddWithValue("@DocRef", _cashier.DocRef);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _cashier.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new CashierAddNew()
                                {
                                    CashierPK = Convert.ToInt32(dr["newCashierPK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert Cashier Success"
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }


        public int Cashier_Update(Cashier _cashier, bool _havePrivillege)
        {
            int _newHisPK;
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_cashier.CashierPK, _cashier.HistoryPK, "Cashier");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Cashier set status=2, Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                "Description=@Description,DebitCredit=@DebitCredit,DebitCurrencyPK=@DebitCurrencyPK,CreditCurrencyPK=@CreditCurrencyPK, " +
                                "DebitCashRefPK=@DebitCashRefPK,CreditCashRefPK=@CreditCashRefPK,DebitAccountPK=@DebitAccountPK,CreditAccountPK=@CreditAccountPK, " +
                                "Debit=@Debit,Credit=@Credit,DebitCurrencyRate=@DebitCurrencyRate,CreditCurrencyRate=@CreditCurrencyRate, " +
                                "BaseDebit=@BaseDebit,BaseCredit=@BaseCredit,PercentAmount=@PercentAmount,FinalAmount=@FinalAmount,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK, " +
                                "AgentPK=@AgentPK,CounterpartPK=@CounterpartPK,ConsigneePK=@ConsigneePK,InstrumentPK=@InstrumentPK,JournalNo=@JournalNo,DocRef=@DocRef, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where CashierPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                            cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                            cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                            cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                            cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                            cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                            cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                            cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                            cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                            cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                            cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                            cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                            cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                            cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                            cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                            cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                            cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                            cmd.Parameters.AddWithValue("@DocRef", _cashier.DocRef);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Cashier set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where CashierPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _cashier.EntryUsersID);
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
                                cmd.CommandText = "Update Cashier set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                    "Description=@Description,DebitCredit=@DebitCredit,DebitCurrencyPK=@DebitCurrencyPK,CreditCurrencyPK=@CreditCurrencyPK, " +
                                    "DebitCashRefPK=@DebitCashRefPK,CreditCashRefPK=@CreditCashRefPK,DebitAccountPK=@DebitAccountPK,CreditAccountPK=@CreditAccountPK, " +
                                    "Debit=@Debit,Credit=@Credit,DebitCurrencyRate=@DebitCurrencyRate,CreditCurrencyRate=@CreditCurrencyRate, " +
                                    "BaseDebit=@BaseDebit,BaseCredit=@BaseCredit,PercentAmount=@PercentAmount,FinalAmount=@FinalAmount,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK, " +
                                    "AgentPK=@AgentPK,CounterpartPK=@CounterpartPK,ConsigneePK=@ConsigneePK,InstrumentPK=@InstrumentPK,JournalNo=@JournalNo,DocRef=@DocRef, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate, Reference = @reference " +
                                    "where CashierPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                                cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                                cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                                cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                                cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                                cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                                cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                                cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                                cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                                cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                                cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                                cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                                cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                                cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                                cmd.Parameters.AddWithValue("@DocRef", _cashier.DocRef);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_cashier.CashierPK, "Cashier");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                 "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                              "From Cashier where CashierPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                                cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                                cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                                cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                                cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                                cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                                cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                                cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                                cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                                cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                                cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                                cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                                cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                                cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                                cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                                cmd.Parameters.AddWithValue("@DocRef", _cashier.DocRef);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();

                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Cashier set status= 4,Notes=@Notes," +
                                    "LastUpdate=@lastupdate where CashierPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
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

        public decimal Get_SumPercentAmountByReferenceByAccount(string _ref, int _accountPK, int _percentAmount, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText = " Select isnull (SUM(PercentAmount),0) + @PercentAmount AS SumPercentAmount " +
                            " from Cashier C left join Account A on C.DebitAccountPK = A.AccountPK " +
                            " Where Reference =@Reference and C.Status = 1 and C.DebitAccountPK = @AccountPK ";
                        }
                        else
                        {
                            cmd.CommandText = " Select isnull (SUM(PercentAmount),0) + @PercentAmount AS SumPercentAmount " +
                            " from Cashier C left join Account A on C.CreditAccountPK = A.AccountPK " +
                            " Where Reference =@Reference and C.Status = 1 and C.CreditAccountPK = @AccountPK ";
                        }


                        cmd.Parameters.AddWithValue("@Reference", _ref);
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);
                        cmd.Parameters.AddWithValue("@PercentAmount", _percentAmount);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["SumPercentAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_SumPercentAmountByCashierIDByAccount(long _cashierID, int _accountPK, int _percentAmount, string _type, int _periodPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText = " Select isnull (SUM(PercentAmount),0) + @PercentAmount AS SumPercentAmount " +
                            " from Cashier C left join Account A on C.DebitAccountPK = A.AccountPK " +
                            " Where CashierID =@CashierID and C.Status = 1 and C.DebitAccountPK = @AccountPK and C.PeriodPK = @PeriodPK  ";
                        }
                        else
                        {
                            cmd.CommandText = " Select isnull (SUM(PercentAmount),0) + @PercentAmount AS SumPercentAmount " +
                            " from Cashier C left join Account A on C.CreditAccountPK = A.AccountPK " +
                            " Where CashierID =@CashierID and C.Status = 1 and C.CreditAccountPK = @AccountPK and C.PeriodPK = @PeriodPK ";
                        }


                        cmd.Parameters.AddWithValue("@CashierID", _cashierID);
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        cmd.Parameters.AddWithValue("@PercentAmount", _percentAmount);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["SumPercentAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_BankBalanceByReference(string _ref, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText = " Select isnull(SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end),0) Balance from Cashier Where Reference =@Reference and status <> 3 ";

                        }
                        else
                        {
                            cmd.CommandText = " Select isnull(SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end),0) Balance from Cashier Where Reference =@Reference and status <> 3 ";
                        }
                        cmd.Parameters.AddWithValue("@Reference", _ref);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["Balance"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_BankBalanceByCashierID(long _cashierID, string _type, int _periodPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText = " Select isnull(SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end),0) Balance from Cashier Where CashierID =@CashierID and Type = @Type and PeriodPK = @PeriodPK ";

                        }
                        else
                        {
                            cmd.CommandText = " Select isnull(SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end),0) Balance from Cashier Where CashierID =@CashierID and Type = @Type and PeriodPK = @PeriodPK ";
                        }
                        cmd.Parameters.AddWithValue("@CashierID", _cashierID);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["Balance"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //7
        public void Cashier_Approved(Cashier _cashier)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

Declare @CashierID bigint
Declare @valueDateForRef datetime
Declare @typeForRef nvarchar(100)
Declare @PeriodPKForRef int
Declare @Ref nvarchar(100)

select @CashierID = CashierID,@typeForRef = Type,@PeriodPKForRef = PeriodPK,@Ref = Reference,@valueDateForRef = ValueDate  from cashier where CashierPK = @CashierPK

				
if isnull(@Ref,'') = '' or right(@Ref,4) != REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','') 
BEGIN
	Declare @LastNo int   
    Declare @Reference nvarchar(20) 
      
    if exists(Select Top 1 * from cashierReference where Type = @typeForRef And PeriodPK = @PeriodPKForRef   
        
    and substring(right(reference,4),1,2) = month(@valueDateForRef))       
    
    BEGIN       
    
    Select @LastNo = max(No) +  1 From CashierReference where Type = @typeForRef And PeriodPK = @PeriodPKForRef and   
    substring(right(reference,4),1,2) = month(@valueDateForRef)     

        
    Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @typeForRef = 'CP' then 'OUT' else    
        
    Case When @typeForRef = 'AR' then 'AR' else Case when @typeForRef = 'AP' then 'AP' else    
        
    case when @typeForRef = 'ADJ' then 'ADJ' Else Case when @typeForRef = 'INV' then 'INV' else 'IN' END END END END END   
        
    + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','') 
	      
    
    Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @typeForRef And PeriodPK = @PeriodPKForRef   
        
    and substring(right(reference,4),1,2) = month(@valueDateForRef)    
    
END    
    
ELSE BEGIN       
    
    Set @Reference = '1/' +  Case when @typeForRef = 'CP' then 'OUT' else    
        
    Case When @typeForRef = 'AR' then 'AR' else Case when @typeForRef = 'AP' then 'AP' else    
       
    case when @typeForRef = 'ADJ' then 'ADJ' Else Case when @typeForRef = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','')    
    
    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)    
        
    Select isnull(Max(CashierReferencePK),0) +  1,@PeriodPKForRef,@typeForRef,@Reference,1 from CashierReference   
    
    END       

	update Cashier set Reference = @Reference where CashierID = @CashierID and Type = @typeForRef and PeriodPK = @PeriodPKForRef 
END 


update Cashier set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate 
where CashierID = @CashierID and Type = @typeForRef and PeriodPK =  @PeriodPKForRef and Revised = 0 and status = 1 and CashierID <> 0


update Cashier set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate 
where reference = @Ref and Type = @typeForRef and PeriodPK =  @PeriodPKForRef and Revised = 0 and status = 1 and CashierID = 0
";
                        cmd.Parameters.AddWithValue("@CashierPK", _cashier.CashierPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashier.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashier.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

Declare @CashierID bigint
Declare @valueDateForRef datetime
Declare @typeForRef nvarchar(100)
Declare @PeriodPKForRef int
Declare @Ref nvarchar(100)
select @CashierID = CashierID,@typeForRef = Type,@PeriodPKForRef = PeriodPK,@valueDateForRef = ValueDate,@Ref = Reference  from cashier where CashierPK = @CashierPK

Update Cashier set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CashierID = @CashierID and Type = @typeForRef and PeriodPK =  @PeriodPKForRef  and status = 4 and cashierID <> 0
Update Cashier set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where Reference = @Ref and Type = @typeForRef and PeriodPK =  @PeriodPKForRef  and status = 4 and cashierID = 0
";
                        cmd.Parameters.AddWithValue("@CashierPK", _cashier.CashierPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashier.ApprovedUsersID);
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
        //8
        public void Cashier_Reject(Cashier _cashier)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Cashier set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CashierPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashier.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashier.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Cashier set status= 2,LastUpdate=@LastUpdate where CashierPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
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
        //9
        public void Cashier_Void(Cashier _cashier)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Cashier set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CashierPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashier.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashier.VoidUsersID);
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

        public void Cashier_UnApproved(Cashier _cashier)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Cashier set status = 1,LastUpdate=@LastUpdate " +
                            "where CashierPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashier.HistoryPK);
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

        public List<Cashier> Cashier_SelectFromTo(int _status, string _type, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Cashier> L_Cashier = new List<Cashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {

                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc " +
                            " ,case when Reference is not null and Reference <> '' then  cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) else 0 end RefNo,A.Reference,B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                            " G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID,I.Name DebitCashRefName, J.ID CreditCashRefID,J.Name CreditCashRefName,    " +
                            " K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID,L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID,N.Name InstrumentName,   " +
                            " A.* From Cashier A Left join     " +
                            " Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 Left join     " +
                            " Office C on A.OfficePK = C.OfficePK and C.Status = 2 Left join     " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.Status = 2 Left join     " +
                            " Agent E on A.AgentPK = E.AgentPK and E.Status = 2 Left join     " +
                            " Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status = 2 Left join    " +
                            " Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.Status = 2 Left join     " +
                            " Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.Status = 2 Left join     " +
                            " CashRef I on A.DebitCashRefPK = I.CashRefPK and I.Status = 2 Left join     " +
                            " Cashref J on A.CreditCashRefPK = J.CashRefPK and J.Status = 2 Left join     " +
                            " Account K on A.DebitAccountPK = K.AccountPK and K.Status = 2 Left join     " +
                            " Account L on A.CreditAccountPK = L.AccountPK and L.Status = 2 Left join    " +
                            " Consignee M on A.ConsigneePK = M.ConsigneePK and M.Status = 2 Left join   " +
                            " Instrument N on A.InstrumentPK = N.InstrumentPK and N.Status = 2  " +
                            " Where  A.Status = @Status and A.Type= @Type and ValueDate between @DateFrom and @DateTo order By RefNo  ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, "+
                             " case when Reference is not null and Reference <> '' then  cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) else 0 end RefNo,A.Reference,B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                            " G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID,I.Name DebitCashRefName, J.ID CreditCashRefID,J.Name CreditCashRefName,    " +
                            " K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID,L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID,N.Name InstrumentName,   " +
                            " A.* From Cashier A Left join     " +
                            " Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 Left join     " +
                            " Office C on A.OfficePK = C.OfficePK and C.Status = 2 Left join     " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.Status = 2 Left join     " +
                            " Agent E on A.AgentPK = E.AgentPK and E.Status = 2 Left join     " +
                            " Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status = 2 Left join    " +
                            " Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.Status = 2 Left join     " +
                            " Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.Status = 2 Left join     " +
                            " CashRef I on A.DebitCashRefPK = I.CashRefPK and I.Status = 2 Left join     " +
                            " Cashref J on A.CreditCashRefPK = J.CashRefPK and J.Status = 2 Left join     " +
                            " Account K on A.DebitAccountPK = K.AccountPK and K.Status = 2 Left join     " +
                            " Account L on A.CreditAccountPK = L.AccountPK and L.Status = 2 Left join    " +
                            " Consignee M on A.ConsigneePK = M.ConsigneePK and M.Status = 2 Left join   " +
                            " Instrument N on A.InstrumentPK = N.InstrumentPK and N.Status = 2  " +
                            " Where   A.Type= @Type and ValueDate between @DateFrom and @DateTo order By RefNo  ";
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Cashier.Add(setCashier(dr));
                                }
                            }
                            return L_Cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ReferenceFromCashier> Reference_SelectFromCashierByType(string _type, DateTime _valueDate, int _periodPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceFromCashier> L_cashier = new List<ReferenceFromCashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @"select Distinct A.Reference,A.RefNo from    
                        (    
                        Select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo  
                        From Cashier where status not in (3,4)and Posted = 0 and type = @type and PeriodPK = @PeriodPK   
                        Union All    
                        select Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from cashierReference where Type = @type and left(right(reference,4),2) =  month(@ValueDate)   
                        and reference in ( select distinct reference from cashier where status not in (3,4)and posted = 0 ) and PeriodPK = @PeriodPK       
                        ) A  
                        order By A.Refno 
                        ";
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceFromCashier M_cashier = new ReferenceFromCashier();
                                    M_cashier.Reference = Convert.ToString(dr["Reference"]);
                                    L_cashier.Add(M_cashier);
                                }
                            }
                            return L_cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public long Cashier_GetNewCashierID(string _type, int _periodPK)
        { 
          try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @"
                            Select isnull(max(CashierID),0) + 1 CashierID 
                            from Cashier Where PeriodPK  = @PeriodPK and Type = @Type and status not in (3,4)
                         
                        ";
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt64(dr["CashierID"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
          catch (Exception err)
          {
              throw err;
          }

        }

        public List<CashierIDFromCashier> CashierID_SelectFromCashierByType(string _type, int _periodPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CashierIDFromCashier> L_cashier = new List<CashierIDFromCashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @"
                            Select distinct CashierID,Reference 
                            from Cashier Where PeriodPK  = @PeriodPK and Type = @Type and status not in (3,4)and Posted = 0 and CashierID > 0
                            order by CashierID Desc
                        ";
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CashierIDFromCashier M_cashier = new CashierIDFromCashier();
                                    M_cashier.CashierID = Convert.ToInt64(dr["CashierID"]);
                                    M_cashier.Reference = Convert.ToString(dr["Reference"]);
                                    L_cashier.Add(M_cashier);
                                }
                            }
                            return L_cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<Cashier> Cashier_SelectByType(int _status, string _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Cashier> L_Cashier = new List<Cashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                                "G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID, J.ID CreditCashRefID," +
                                " K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID,L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID, N.Name InstrumentName,  " +
                                "A.* From Cashier A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK  and B.Status = 2 Left join " +
                                "Office C on A.OfficePK = C.OfficePK and C.status = 2 Left join " +
                                "Department D on A.DepartmentPK = D.DepartmentPK and D.status = 2 Left join " +
                                "Agent E on A.AgentPK = E.AgentPK  and E.status = 2 Left join " +
                                "Counterpart F on A.CounterpartPK = F.CounterpartPK and F.status = 2 Left join " +
                                "Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.status = 2 Left join " +
                                "Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.status = 2 Left join " +
                                "CashRef I on A.DebitCashRefPK = I.CashRefPK and I.status = 2 Left join " +
                                "Cashref J on A.CreditCashRefPK = J.CashRefPK  and J.status = 2 Left join " +
                                "Account K on A.DebitAccountPK = K.AccountPK Left join " +
                                "Account L on A.CreditAccountPK = L.AccountPK Left join " +
                                "Consignee M on A.ConsigneePK = M.ConsigneePK Left join " +
                                "Instrument N on A.InstrumentPK = N.InstrumentPK  " +
                                "Where A.Status = @status " +
                                "and A.Type = @type";
                            cmd.Parameters.AddWithValue("@status", _status);
                            cmd.Parameters.AddWithValue("@type", _type);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from Cashier order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Cashier.Add(setCashier(dr));
                                }
                            }
                            return L_Cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Add_CashierValidate(string _valueDate, string _cashRef, string _cashierID, string _type)
        {

            try
            {


                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmdR = DbCon.CreateCommand())
                    {
                        string _cashRefColumn = "";
                        if (_type == "CP" || _type == "CBT")
                        {
                            _cashRefColumn = "CreditCashRefPK";
                        }
                        if (_type == "CR")
                        {
                            _cashRefColumn = "DebitCashRefPK";
                        }
                        if (_type == "CAR" || _type == "CAP")
                        {
                            _cashRefColumn = "DebitAccountPK";
                        }


                        cmdR.CommandText =


                            @"
                            if exists (Select distinct(CashierID) CashierID From Cashier where status = 1 and Posted = 0 and CashierID = @CashierID and Type = @Type)    
                            Begin    
                   
	                            if exists (select * from Cashier where CashierID = @CashierID and ValueDate = @ValueDate and " + _cashRefColumn + @"= @CashRef)    
		                            BEGIN 
                                     SELECT 1 Result 
                                    END 
                                    ELSE 
                                    BEGIN 
                                      SELECT 0 Result
                                    END   
                            End    
                            Else    
                            Begin    
	                              SELECT 1 Result 
                            End ";




                        cmdR.Parameters.AddWithValue("@CashierID", _cashierID);
                        cmdR.Parameters.AddWithValue("@Type", _type);
                        cmdR.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmdR.Parameters.AddWithValue("@CashRef", _cashRef);
                        using (SqlDataReader drR = cmdR.ExecuteReader())
                        {

                            if (drR.HasRows)
                            {
                                drR.Read();
                                return Convert.ToBoolean(drR["Result"]);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Cashier_Posting(Cashier _cashier)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _bankParamCr = "";
                        string _bankParamCp = "";
                        string _journalDetailCr = "";
                        string _journalDetailCp = "";

                        //CP
                        _bankParamCp =
                        @" SET @AutoNo = 1    
                        Select @BankAmount = sum(case when DebitCredit = 'D' then Credit else credit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'D' then BaseCredit else Basecredit * -1 end)  From Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                            SELECT top 1 @JournalPK,@AutoNo,1,2,CreditAccountPK,CreditCurrencyPK,0,0,    
                            0,0,0,0,@BankDescription,@Reference,'C',@BankAmount,0,@BankAmount,    
                            CreditCurrencyRate,0,@BaseBankAmount,@UserID,@DateTimeNow from cashier where reference = @Reference and posted = 0 and status = 2      
                            SET @AutoNo = 2         ";

                        _journalDetailCp =

                         @"  
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                         SELECT @JournalPK,@AutoNo,1,2,@DebitAccountPK,@DebitCurrencyPK,@OfficePK,@DepartmentPK,    
                         @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@Reference,
		                 Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Debit,Case when @DebitCredit = 'D' then @Debit Else 0 END,    
                         Case when @DebitCredit = 'D' then 0 Else @Debit END,@DebitCurrencyRate,
		                 Case when @DebitCredit = 'D' then @BaseDebit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseDebit END,
		                 @UserID,@DateTimeNow    
                         SET @AutoNo = ISNULL(@AutoNo,0) +  1
                          ";


                        //CR
                        _bankParamCr =
                        @"      
                        SET @AutoNo = 1    
                           Select @BankAmount = sum(case when DebitCredit = 'C' then debit else Debit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'C' then BaseDebit * -1 else BaseDebit  end)  From Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                  SELECT  top 1 @JournalPK,@AutoNo,1,2,DebitAccountPK,DebitCurrencyPK,0,0,    
                         0,0,0,0,@BankDescription,@Reference,'D',abs(@BankAmount),abs(@BankAmount),0,DebitCurrencyRate,abs(@BaseBankAmount),0,@UserID,@DateTimeNow    
                         FROM Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                         SET @AutoNo = 2 
                        ";

                        _journalDetailCr =
                         @" 
                                INSERT INTO [dbo].[JournalDetail]    
                                        ([JournalPK]    
                                        ,[AutoNo]    
                                        ,[HistoryPK]    
                                        ,[Status]    
                                        ,[AccountPK]    
                                        ,[CurrencyPK]    
                                        ,[OfficePK]    
                                        ,[DepartmentPK]    
                                        ,[AgentPK]    
                                        ,[CounterpartPK]    
                                        ,[InstrumentPK]    
                                        ,[ConsigneePK]    
                                        ,[DetailDescription]    
                                        ,[DocRef]    
                                        ,[DebitCredit]    
                                        ,[Amount]    
                                        ,[Debit]    
                                        ,[Credit]    
                                        ,[CurrencyRate]    
                                        ,[BaseDebit]    
                                        ,[BaseCredit]    
                                        ,[LastUsersID]    
                                        ,[LastUpdate])    
                                 SELECT @JournalPK,@AutoNo,1,2,@CreditAccountPK,@CreditCurrencyPK,@OfficePK,@DepartmentPK,    
                                 @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@Reference,Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Credit,    
                                 Case when @DebitCredit = 'D' then @Credit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @Credit END,@CreditCurrencyRate,Case when @DebitCredit = 'D' then @BaseCredit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseCredit END,@UserID,@DateTimeNow    
                                 SET @AutoNo = ISNULL(@AutoNo,0) +  1

                            ";
                    
                            cmd.CommandText = @"
                     Declare @periodPK int    
                     Declare @ValueDate Datetime    
                     Declare @Type Nvarchar(10)    
                     Declare @Description Nvarchar(Max)    
                     Declare @DebitCurrencyPK int    
                     Declare @CreditCurrencyPK int    
                     Declare @DebitCashRefPK int    
                     Declare @CreditCashRefPK int    
                     Declare @DebitAccountPK int    
                     Declare @CreditAccountPK int    
                     Declare @Debit numeric(19,4)    
                     Declare @Credit numeric(19,4)    
                     Declare @DebitCurrencyRate numeric(19,4)    
                     Declare @CreditCurrencyRate numeric(19,4)    
                     Declare @BaseDebit numeric(19,4)    
                     Declare @BaseCredit numeric(19,4)    
                     Declare @OfficePK int    
                     Declare @DepartmentPK int    
                     Declare @AgentPK int    
                     Declare @CounterpartPK int    
                     Declare @InstrumentPK int    
                     Declare @ConsigneePK int    
                     Declare @DebitCredit nvarchar(1)    
                     Declare @AutoNo Int    
                     Declare @JournalPK int    
                     Declare @BankDescription nvarchar(Max)    
                     Declare @BankAmount Numeric(19,4) 
                     Declare @BaseBankAmount Numeric(19,4)    
       
                     --INSERT JOURNAL HEADER    
           

                    Select @JournalPK = isnull(max(JournalPK),0) +  1 From Journal     
                    SET @BankDescription = ''    
                    SELECT @BankDescription =  @BankDescription  +  ' - ' +  Description, @Type = Type     
                    FROM Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2    
           

           
                    INSERT INTO [dbo].[Journal]    
                            ([JournalPK]    
                            ,[HistoryPK]    
                            ,[Status]    
                            ,[Notes]    
                            ,[PeriodPK]    
                            ,[ValueDate]    
                            ,[TrxNo]    
                            ,[TrxName]    
                            ,[Reference]    
                            ,[Type]    
                            ,[Description]    
                            ,[Posted]    
                            ,[PostedBy]    
                            ,[PostedTime]    
                            ,[EntryUsersID]    
                            ,[EntryTime]    
                            ,[ApprovedUsersID]    
                            ,[ApprovedTime]    
                            ,[LastUpdate])    
                    SELECT Top 1 @JournalPK,1,2,'Posting From Cashier',PeriodPK,    
                    ValueDate,CashierPK,Type,@Reference,3,@BankDescription,1,entryusersID,EntryTime,    
                    @UserID,@DatetimeNow,@UserID,@DatetimeNow,@DatetimeNow    
                    FROM Cashier    
                    WHERE Reference = @Reference And Status = 2 and Posted = 0 and Revised = 0     

                    if @Type = 'CP' Begin

                    " + _bankParamCp +
                    @"
                    end 
                    else Begin
                    " + _bankParamCr +
                    @"
                    end

                 DECLARE Detail CURSOR FOR     
                 SELECT periodPK,ValueDate,Type,Description,DebitCredit,DebitCurrencyPK,CreditCurrencyPK,DebitCashRefPK    
                 ,CreditCashRefPK,DebitAccountPK,CreditAccountPK,Debit,Credit,DebitCurrencyRate,CreditCurrencyRate,    
                 BaseDebit,BaseCredit,OfficePK,DepartmentPK,AgentPK,CounterpartPK,InstrumentPK,ConsigneePK FROM Cashier    
                 WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2    
           
                 OPEN Detail              
                 FETCH NEXT FROM Detail INTO     
                 @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                 ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                 @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK    
           
                WHILE @@FETCH_STATUS = 0              
                BEGIN      
                if @Type = 'CP' Begin
                 " + _journalDetailCp +
                   @" end else Begin "
                   + _journalDetailCr + @"
                END
                FETCH NEXT FROM Detail INTO    
                @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK     
                END       
           
                CLOSE Detail        
                DEALLOCATE Detail       
           
                UPDATE CASHIER SET Posted = 1,PostedBy = @UserID,PostedTime = @DatetimeNow, JournalNo = @JournalPK, LastUpdate = @DatetimeNow    
                WHERE Reference = @Reference and Posted = 0 and Revised = 0  and status = 2

                    "
                            ;

                            cmd.Parameters.AddWithValue("@Reference", _cashier.ParamReference);
                            cmd.Parameters.AddWithValue("@UserID", _cashier.ParamUserID);
                            cmd.Parameters.AddWithValue("@DatetimeNow", _dateTimeNow);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Cashier_PostingBySelected(string _userID,string _permissionID, DateTime _dateFrom, DateTime _dateTo,string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _bankParamCr = "";
                        string _bankParamCp = "";
                        string _journalDetailCr = "";
                        string _journalDetailCp = "";

                        //CP
                        _bankParamCp =
                        @" 
                        
                        SET @AutoNo = 1    
                        Select @BankAmount = sum(case when DebitCredit = 'D' then Credit else credit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'D' then BaseCredit else Basecredit * -1 end)  From Cashier WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2    and Type = @CType and PeriodPK = @CPeriodPK   
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                            SELECT top 1 @JournalPK,@AutoNo,1,2,CreditAccountPK,case when CreditCurrencyPK = 0 then 1 else CreditCurrencyPK end,0,0,    
                            0,0,0,0,@BankDescription,DocRef,'C',@BankAmount,0,@BankAmount,    
                            CreditCurrencyRate,0,@BaseBankAmount,@UserID,@DateTimeNow from cashier where CashierID = @CashierID and posted = 0 and status = 2      and Type = @CType and PeriodPK = @CPeriodPK
                            SET @AutoNo = 2         ";

                        _journalDetailCp =

                         @"  
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                         SELECT @JournalPK,@AutoNo,1,2,@DebitAccountPK,@DebitCurrencyPK,@OfficePK,@DepartmentPK,    
                         @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@DocRef,
		                 Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Debit,Case when @DebitCredit = 'D' then @Debit Else 0 END,    
                         Case when @DebitCredit = 'D' then 0 Else @Debit END,@DebitCurrencyRate,
		                 Case when @DebitCredit = 'D' then @BaseDebit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseDebit END,
		                 @UserID,@DateTimeNow    
                         SET @AutoNo = ISNULL(@AutoNo,0) +  1
                          ";


                        //CR
                        _bankParamCr =
                        @"      
                        SET @AutoNo = 1    
                           Select @BankAmount = sum(case when DebitCredit = 'C' then debit else Debit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'C' then BaseDebit * -1 else BaseDebit  end)  From Cashier WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2    and Type = @CType and PeriodPK = @CPeriodPK   
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                  SELECT  top 1 @JournalPK,@AutoNo,1,2,DebitAccountPK,case when DebitCurrencyPK = 0 then 1 else DebitCurrencyPK end,0,0,    
                         0,0,0,0,@BankDescription,DocRef,'D',abs(@BankAmount),abs(@BankAmount),0,DebitCurrencyRate,abs(@BaseBankAmount),0,@UserID,@DateTimeNow    
                         FROM Cashier WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2  and Type = @CType and PeriodPK = @CPeriodPK     
                         SET @AutoNo = 2 
                        ";

                        _journalDetailCr =
                         @" 
                                INSERT INTO [dbo].[JournalDetail]    
                                        ([JournalPK]    
                                        ,[AutoNo]    
                                        ,[HistoryPK]    
                                        ,[Status]    
                                        ,[AccountPK]    
                                        ,[CurrencyPK]    
                                        ,[OfficePK]    
                                        ,[DepartmentPK]    
                                        ,[AgentPK]    
                                        ,[CounterpartPK]    
                                        ,[InstrumentPK]    
                                        ,[ConsigneePK]    
                                        ,[DetailDescription]    
                                        ,[DocRef]    
                                        ,[DebitCredit]    
                                        ,[Amount]    
                                        ,[Debit]    
                                        ,[Credit]    
                                        ,[CurrencyRate]    
                                        ,[BaseDebit]    
                                        ,[BaseCredit]    
                                        ,[LastUsersID]    
                                        ,[LastUpdate])    
                                 SELECT @JournalPK,@AutoNo,1,2,@CreditAccountPK,@CreditCurrencyPK,@OfficePK,@DepartmentPK,    
                                 @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@DocRef,Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Credit,    
                                 Case when @DebitCredit = 'D' then @Credit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @Credit END,@CreditCurrencyRate,Case when @DebitCredit = 'D' then @BaseCredit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseCredit END,@UserID,@DateTimeNow    
                                 SET @AutoNo = ISNULL(@AutoNo,0) +  1

                            ";



                        cmd.CommandText =
                            @"


                            Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UserID and Status = 2      
  
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                            Select @DatetimeNow,@PermissionID,'Cashier',CashierPK,1,'Posting by Selected Data',
                            @UserID,@IPAddress,@DatetimeNow  from Cashier 
                            where CashierID in (Select distinct CashierID From Cashier 
                            Where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 " + paramCashierPK + @" and type = @Paramtype ) 
                            and Posted = 0 and Revised = 0 and status = 2      

                             Declare @CashierID bigint  
                            declare @CValueDate datetime
                            Declare @CType nvarchar(100)
                            Declare @CPeriodPK int
       
                             Declare @periodPK int    
                             Declare @ValueDate Datetime    
                             Declare @Type Nvarchar(10)    
                             Declare @Description Nvarchar(Max)    
                             Declare @DebitCurrencyPK int    
                             Declare @CreditCurrencyPK int    
                             Declare @DebitCashRefPK int    
                             Declare @CreditCashRefPK int    
                             Declare @DebitAccountPK int    
                             Declare @CreditAccountPK int    
                             Declare @Debit numeric(19,4)    
                             Declare @Credit numeric(19,4)    
                             Declare @DebitCurrencyRate numeric(19,4)    
                             Declare @CreditCurrencyRate numeric(19,4)    
                             Declare @BaseDebit numeric(19,4)    
                             Declare @BaseCredit numeric(19,4)    
                             Declare @OfficePK int    
                             Declare @DepartmentPK int    
                             Declare @AgentPK int    
                             Declare @CounterpartPK int    
                             Declare @InstrumentPK int    
                             Declare @ConsigneePK int    
                             Declare @DebitCredit nvarchar(1)    
                             Declare @JournalPK int    
                             Declare @BankAmount Numeric(19,4) Declare @BaseBankAmount Numeric(19,4)    
                             Declare @BankDescription nvarchar(Max)    
                             Declare @Reference nvarchar(100)
                             Declare @DocRef nvarchar(400)

       
                              DECLARE Header CURSOR FOR             
                                 SELECT distinct CashierID,ValueDate,Type,PeriodPK FROM Cashier    
                                 WHERE status = 2 and Posted = 0 and revised = 0 " + paramCashierPK + @" and ValueDate between @DateFrom and @DateTo and type = @Paramtype   
                                    order by ValueDate Asc  
                              OPEN Header             
                                 FETCH NEXT FROM Header INTO @CashierID,@CValueDate,@Ctype,@CPeriodPK
                              WHILE @@FETCH_STATUS = 0              
                                BEGIN       


                                     Select @JournalPK = isnull(max(JournalPK),0) +  1 From Journal     
       
                                     Declare @AutoNo Int    
       
                                    SET @BankDescription = ''    
                                    SELECT @BankDescription =  @BankDescription +   ' - ' +  Description , @Type = Type    
                                    FROM Cashier WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2    and Type = @CType and PeriodPK = @CPeriodPK
                                     INSERT INTO [dbo].[Journal]    
                                                ([JournalPK]    
                                                ,[HistoryPK]    
                                                ,[Status]    
                                                ,[Notes]    
                                                ,[PeriodPK]    
                                                ,[ValueDate]    
                                                ,[TrxNo]    
                                                ,[TrxName]    
                                                ,[Reference]    
                                                ,[Type]    
                                                ,[Description]    
                                                ,[Posted]    
                                                ,[PostedBy]    
                                                ,[PostedTime]    
                                                ,[EntryUsersID]    
                                                ,[EntryTime]    
                                                ,[ApprovedUsersID]    
                                                ,[ApprovedTime]    
                                                ,[LastUpdate])    
                                     SELECT Top 1 @JournalPK,1,2,'Posting From Cashier',PeriodPK,    
                                     ValueDate,CashierPK,Type,Reference,3,@BankDescription,1,@UserID,@DatetimeNow,    
                                     EntryUsersID,EntryTime,@UserID,@DatetimeNow,@DatetimeNow    
                                     FROM Cashier    
                                     WHERE CashierID = @CashierID And Status = 2 and Posted = 0 and Revised = 0   and Type = @CType and PeriodPK = @CPeriodPK     
                              if @Type = 'CP' Begin       
                            " + _bankParamCp +
                              @"
                            end else Begin  
                            " + _bankParamCr +
                              @"
                            end
                            DECLARE Detail CURSOR FOR     
                                     SELECT PeriodPK,ValueDate,Type,Description,DebitCredit,DebitCurrencyPK,CreditCurrencyPK,DebitCashRefPK    
                                     ,CreditCashRefPK,DebitAccountPK,CreditAccountPK,Debit,Credit,DebitCurrencyRate,CreditCurrencyRate,    
                                     BaseDebit,BaseCredit,OfficePK,DepartmentPK,AgentPK,CounterpartPK,InstrumentPK,ConsigneePK,Reference,DocRef FROM Cashier    
                                     WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2  and Type = @CType and PeriodPK = @CPeriodPK   
       
                                     OPEN Detail              
                                     FETCH NEXT FROM Detail INTO     
                                     @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                                     ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                                     @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK ,@Reference,@DocRef  
       
                                    WHILE @@FETCH_STATUS = 0               
                              BEGIN      
                              if @Type = 'CP' Begin
                            "
                              + _journalDetailCp +
                              @"
                            end else Begin 
                            " + _journalDetailCr +
                              @"
                             END FETCH NEXT FROM Detail INTO    
                                    @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                                    ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                                    @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK ,@Reference,@DocRef       
                                    END       
       
                                    CLOSE Detail        
                                    DEALLOCATE Detail       
       
                                    UPDATE CASHIER SET Posted = 1,PostedBy = @UserID,PostedTime = @DatetimeNow, JournalNo = @JournalPK , LastUpdate = @DatetimeNow   
                                    WHERE CashierID = @CashierID and Posted = 0 and Revised = 0 and status = 2 and valueDate Between @DateFrom and @DateTo  
                                    and Type = @CType and PeriodPK = @CPeriodPK   
       
                                 FETCH NEXT FROM Header INTO @CashierID ,@CValueDate,@CType ,@CPeriodPK
                                END          
                                CLOSE Header       
                                DEALLOCATE Header  
                            "
                        ;
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@DatetimeNow", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Paramtype", _type);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Cashier_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

Declare @PeriodPK int
select @PeriodPK = PeriodPK from period where status in (1,2) and @DateFrom between DateFrom and DateTo

                     Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                     Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                     Select @Time,@PermissionID,'Cashier',CashierPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type  and PeriodPK = @PeriodpK
                     

Declare @CashierID bigint  
                    declare @CValueDate datetime
                    Declare @CType nvarchar(100)
                    Declare @CPeriodPK int  

DECLARE Header CURSOR FOR             
    SELECT distinct CashierID,ValueDate,Type,PeriodPK FROM Cashier    
    WHERE status = 1  " + paramCashierPK + @" and ValueDate between @DateFrom and @DateTo   and PeriodPK = @PeriodpK and Type = @type
    order by ValueDate Asc  
OPEN Header             
    FETCH NEXT FROM Header INTO @CashierID,@CValueDate,@Ctype,@CPeriodPK
WHILE @@FETCH_STATUS = 0              
BEGIN      



Declare @valueDateForRef datetime
Declare @typeForRef nvarchar(100)
Declare @PeriodPKForRef int
Declare @Ref nvarchar(100)

set @Ref = ''

select top 1 @valueDateForRef = ValueDate,@typeForRef = Type,@PeriodPKForRef = PeriodPK, @Ref = Reference  From Cashier where CashierID = @CashierID and Type = @Ctype
and PeriodPK = @CPeriodPK and status not in (3,4)
				
if isnull(@Ref,'') = '' or right(@Ref,4) != REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','') 
BEGIN
	Declare @LastNo int   
    Declare @Reference nvarchar(20) 
      
    if exists(Select Top 1 * from cashierReference where Type = @typeForRef And PeriodPK = @PeriodPKForRef   
        
    and substring(right(reference,4),1,2) = month(@valueDateForRef))       
    
    BEGIN       
    
    Select @LastNo = max(No) +  1 From CashierReference where Type = @typeForRef And PeriodPK = @PeriodPKForRef and   
    substring(right(reference,4),1,2) = month(@valueDateForRef)     

        
    Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @typeForRef = 'CP' then 'OUT' else    
        
    Case When @typeForRef = 'AR' then 'AR' else Case when @typeForRef = 'AP' then 'AP' else    
        
    case when @typeForRef = 'ADJ' then 'ADJ' Else Case when @typeForRef = 'INV' then 'INV' else 'IN' END END END END END   
        
    + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','') 
	      
    
    Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @typeForRef And PeriodPK = @PeriodPKForRef   
        
    and substring(right(reference,4),1,2) = month(@valueDateForRef)    
    
END    
    
ELSE BEGIN       
    
    Set @Reference = '1/' +  Case when @typeForRef = 'CP' then 'OUT' else    
        
    Case When @typeForRef = 'AR' then 'AR' else Case when @typeForRef = 'AP' then 'AP' else    
       
    case when @typeForRef = 'ADJ' then 'ADJ' Else Case when @typeForRef = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @valueDateForRef, 3), 5) ,'/','')    
    
    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)    
        
    Select isnull(Max(CashierReferencePK),0) +  1,@PeriodPKForRef,@typeForRef,@Reference,1 from CashierReference   
    
    END       

	update Cashier set Reference = @Reference where CashierID = @CashierID and Type = @typeForRef and PeriodPK = @PeriodPKForRef 
END 

  FETCH NEXT FROM Header INTO @CashierID ,@CValueDate,@CType ,@CPeriodPK
END          
CLOSE Header       
DEALLOCATE Header  




                     update Cashier set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                     where CashierID in ( Select CashierID from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type and PeriodPK = @PeriodpK)   and Status = 1  and type = @type   and PeriodPK = @PeriodpK
                     
                     Update Cashier set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where CashierID in (Select CashierID from Cashier where ValueDate between @DateFrom and @DateTo and Status = 4  and type = @type and PeriodPK = @PeriodpK)    
                     and Status = 4 " + paramCashierPK + @" and type = @type and PeriodPK = @PeriodpK
     
 

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Cashier_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo,string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                         Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                         Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                         Select @Time,@PermissionID,'Cashier',CashierPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  
                         from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type    
                         update Cashier set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                         where CashierPK in ( Select CashierPK from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type )  and Status = 1 " + paramCashierPK + @" and type = @type    
                         Update Cashier set status= 2  where CashierPK in (Select CashierPK from Cashier where ValueDate between @DateFrom and @DateTo and Status = 4 " + paramCashierPK + @" and type = @type)  and Status = 4 " + paramCashierPK + @" and type = @type  
     

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Cashier_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                    Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                     Select @Time,@PermissionID,'Cashier',CashierPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from Cashier where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 " + paramCashierPK + @" and type = @type    
                       update Cashier set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                       where CashierPK in ( Select CashierPK from Cashier where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 " + paramCashierPK + @" and type = @type )    
                       and Status = 2 and Posted = 0 and Revised = 0 and type = @type   " + paramCashierPK 


                        ;
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //Revise Cashier
        public void Cashier_Revise(Cashier _cashier)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {

                            cmd.CommandText = @"
                            Declare @Notes Nvarchar(1000)    

                            Declare @MaxCashierPK int    
                            Declare @CashierPK int    
       
                            DECLARE A CURSOR FOR         
                                SELECT CashierPK FROM Cashier    
                                WHERE Reference = @Reference And Status = 2 and Posted = 1 and revised = 0    
       
                                OPEN A          
                                FETCH NEXT FROM A INTO @CashierPK        
                                WHILE @@FETCH_STATUS = 0         
                                BEGIN        
       
 	                        Select @MaxCashierPK = ISNULL(MAX(CashierPK),0)  +  1 From Cashier     
                                    INSERT INTO [dbo].[Cashier]    
 					                            ([CashierPK]    
,CashierID
 					                            ,[HistoryPK]    
 					                            ,[Status]    
 					                            ,[Notes]    
 					                            ,[JournalNo]    
 					                            ,[PeriodPK]    
 					                            ,[ValueDate]    
 					                            ,[Type]    
 					                            ,[Reference]    
 					                            ,[DocRef]   
 					                            ,[Description]    
 					                            ,[DebitCredit]    
 					                            ,[DebitCurrencyPK]    
 					                            ,[CreditCurrencyPK]    
 					                            ,[DebitCashRefPK]    
 					                            ,[CreditCashRefPK]    
 					                            ,[DebitAccountPK]    
 					                            ,[CreditAccountPK]    
 					                            ,[Debit]    
 					                            ,[Credit]    
 					                            ,[DebitCurrencyRate]    
 					                            ,[CreditCurrencyRate]    
 					                            ,[BaseDebit]    
 					                            ,[BaseCredit]    
 					                            ,[PercentAmount]    
 					                            ,[FinalAmount]    
 					                            ,[OfficePK]    
 					                            ,[DepartmentPK]    
 					                            ,[AgentPK]    
 					                            ,[CounterpartPK]    
 					                            ,[InstrumentPK]    
 					                            ,[ConsigneePK]    
 					                            ,[EntryUsersID]    
 					                            ,[EntryTime]    
 					                            ,[LastUpdate])    
 			                        SELECT	   @MaxCashierPK,CashierID,1,1,'Pending Revised',0    
 					                            ,[PeriodPK]    
 					                            ,[ValueDate]    
 					                            ,[Type]    
 					                            ,[Reference]      
 					                            ,[DocRef] 
 					                            ,[Description]    
 					                            ,[DebitCredit]    
 					                            ,[DebitCurrencyPK]    
 					                            ,[CreditCurrencyPK]    
 					                            ,[DebitCashRefPK]    
 					                            ,[CreditCashRefPK]    
 					                            ,[DebitAccountPK]    
 					                            ,[CreditAccountPK]    
 					                            ,[Debit]    
 					                            ,[Credit]    
 					                            ,[DebitCurrencyRate]    
 					                            ,[CreditCurrencyRate]    
 					                            ,[BaseDebit]    
 					                            ,[BaseCredit]    
 					                            ,[PercentAmount]    
 					                            ,[FinalAmount]    
 					                            ,[OfficePK]    
 					                            ,[DepartmentPK]    
 					                            ,[AgentPK]    
 					                            ,[CounterpartPK]     
 					                            ,[InstrumentPK]    
 					                            ,[ConsigneePK]    
 					                            ,[EntryUsersID]    
 					                            ,[EntryTime]    
 					                            ,@DatetimeNow    
 			                        FROM Cashier     
 			                        WHERE CashierPK = @CashierPK     and status = 2 and posted = 1 and revised = 0
       
                            FETCH NEXT FROM A INTO @CashierPK    
                            END                 
       
                            CLOSE A              
                            DEALLOCATE A  
                            UPDATE Journal SET status = 3, Notes = 'Void By Cashier Revise',    
                            VoidUsersID = @UserID,VoidTime = @DatetimeNow     
                            WHERE JournalPK in    
                            (    
 	                        Select top 1 JournalNo From Cashier where Reference = @Reference and status = 2 and Revised = 0   and posted = 1  
                            ) 
     
                            UPDATE Cashier Set Status = 3,Revised = 1, RevisedBy = @UserID,Notes = 'Revise From Cashier',    
                            RevisedTime = @DatetimeNow     
                            WHERE Reference = @Reference and status = 2 and Posted = 1 and revised = 0    
       
                          ";
                            cmd.Parameters.AddWithValue("@Reference", _cashier.ParamReference);
                            cmd.Parameters.AddWithValue("@UserID", _cashier.ParamUserID);
                            cmd.Parameters.AddWithValue("@DatetimeNow", _dateTimeNow);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Cashier_ReviseBySelected(string _userID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, string _type)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {

                            cmd.CommandText =
                                @"Declare @CashierPK int  
                                Declare @MaxCashierPK int 
                                Declare @IPAddress nvarchar(50) 
                                select @IPAddress = IPAddress from Users where ID = @UserID and Status = 2      

                                Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                                Select @DatetimeNow,@PermissionID,'Cashier',CashierPK,1,'Revise by Selected Data',@UserID,@IPAddress,@DatetimeNow  
                                from Cashier 
                                WHERE  Reference 
                                in 
                                ( Select distinct Reference From Cashier Where Status = 2 and Posted = 1 and revised = 0 and Selected = 1 and ValueDate Between @DateFrom and @DateTo and type = @type ) 
                                and status = 2 and posted = 1 and revised = 0      

                                 Declare @Notes Nvarchar(1000)    

       
                                 DECLARE A CURSOR FOR         
                                     SELECT CashierPK FROM Cashier    
                                     WHERE  Reference in ( Select distinct Reference From Cashier Where Status = 2 
	                                 and Posted = 1 and revised = 0 and Selected = 1 and ValueDate Between @DateFrom and @DateTo and type = @type ) and status = 2 and posted = 1 and revised = 0    
       
                                     OPEN A          
                                     FETCH NEXT FROM A INTO @CashierPK        
                                     WHILE @@FETCH_STATUS = 0         
                                     BEGIN        
       
 	                                Select @MaxCashierPK = ISNULL(MAX(CashierPK),0) +  1 From Cashier     
                                          INSERT INTO [dbo].[Cashier]    
 					                                   ([CashierPK]    
 					                                   ,[HistoryPK]    
,CashierID
 					                                   ,[Status]    
 					                                   ,[Notes]    
 					                                   ,[JournalNo]    
 					                                   ,[PeriodPK]    
 					                                   ,[ValueDate]    
 					                                   ,[Type]    
 					                                   ,[Reference]     
 					                                   ,[DocRef]  
 					                                   ,[Description]    
 					                                   ,[DebitCredit]    
 					                                   ,[DebitCurrencyPK]    
 					                                   ,[CreditCurrencyPK]    
 					                                   ,[DebitCashRefPK]    
 					                                   ,[CreditCashRefPK]    
 					                                   ,[DebitAccountPK]    
 					                                   ,[CreditAccountPK]    
 					                                   ,[Debit]    
 					                                   ,[Credit]    
 					                                   ,[DebitCurrencyRate]    
 					                                   ,[CreditCurrencyRate]    
 					                                   ,[BaseDebit]    
 					                                   ,[BaseCredit]    
 					                                   ,[PercentAmount]    
 					                                   ,[FinalAmount]    
 					                                   ,[OfficePK]    
 					                                   ,[DepartmentPK]    
 					                                   ,[AgentPK]    
 					                                   ,[CounterpartPK]    
 					                                   ,[InstrumentPK]    
 					                                   ,[ConsigneePK]    
 					                                   ,[EntryUsersID]    
 					                                   ,[EntryTime]    
 					                                   ,[LastUpdate])    
 			                                SELECT	   @MaxCashierPK,1,CashierID,1,'Pending Revised',0    
 					                                   ,[PeriodPK]    
 					                                   ,[ValueDate]    
 					                                   ,[Type]    
 					                                   ,[Reference]      
 					                                   ,[DocRef] 
 					                                   ,[Description]    
 					                                   ,[DebitCredit]    
 					                                   ,[DebitCurrencyPK]    
 					                                   ,[CreditCurrencyPK]    
 					                                   ,[DebitCashRefPK]    
 					                                   ,[CreditCashRefPK]    
 					                                   ,[DebitAccountPK]    
 					                                   ,[CreditAccountPK]    
 					                                   ,[Debit]    
 					                                   ,[Credit]    
 					                                   ,[DebitCurrencyRate]    
 					                                   ,[CreditCurrencyRate]    
 					                                   ,[BaseDebit]    
 					                                   ,[BaseCredit]    
 					                                   ,[PercentAmount]    
 					                                   ,[FinalAmount]    
 					                                   ,[OfficePK]    
 					                                   ,[DepartmentPK]    
 					                                   ,[AgentPK]    
 					                                   ,[CounterpartPK]     
 					                                   ,[InstrumentPK]    
 					                                   ,[ConsigneePK]    
 					                                   ,[EntryUsersID]    
 					                                   ,[EntryTime]    
 					                                   ,@DatetimeNow    
 			                                FROM Cashier     
 			                                WHERE CashierPK = @CashierPK     and status = 2 and posted = 1 and revised = 0
       
                                 FETCH NEXT FROM A INTO @CashierPK    
                                 END                  
       
                                 CLOSE A              
                                 DEALLOCATE A       

                                 UPDATE Journal SET status = 3, Notes = 'Void By Cashier Revise',    
                                 VoidUsersID = @UserID,VoidTime = @DatetimeNow, Lastupdate = @DatetimeNow    
                                 WHERE JournalPK in    
                                 (    
 	                                Select distinct JournalNo From Cashier where   Reference in ( Select distinct Reference From Cashier Where Status = 2 and Posted = 1 and revised = 0 and Selected = 1 and ValueDate Between @DateFrom and @DateTo and type = @type )  
                                 )      

                                 UPDATE Cashier Set status = 3, Revised = 1, RevisedBy = @UserID,Notes = 'Revise From Cashier',    
                                 RevisedTime = @DatetimeNow,LastUpdate = @DatetimeNow     
                                 WHERE  Reference in ( Select distinct Reference From Cashier Where Status = 2 and Posted = 1 and revised = 0 and Selected = 1 and ValueDate Between @DateFrom and @DateTo and type = @type ) and posted = 1 and revised = 0 and status = 2
                                    ";
                            
                            cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            cmd.Parameters.AddWithValue("@DatetimeNow", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                            cmd.Parameters.AddWithValue("@type", _type);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string Cashier_Import(string _fileSource)
        {
            try
            {
                // delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "truncate table CashierImportTemp";
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.CashierImportTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromCashierExcelFile(_fileSource));
                }

                // logic kalo import success
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "select * from CashierImportTemp";
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    return "Success Import Cashier";
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromCashierExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "PK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "A";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "B";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "C";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "D";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "E";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "G";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "H";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "I";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "J";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "K";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "L";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "M";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "N";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "O";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "P";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["PK"] = odRdr[1];
                                    dr["A"] = odRdr[0];
                                    dr["B"] = odRdr[1];
                                    dr["C"] = odRdr[2];
                                    dr["D"] = odRdr[3];
                                    dr["E"] = odRdr[4];
                                    dr["F"] = odRdr[5];
                                    dr["G"] = odRdr[6];
                                    dr["H"] = Tools.IsNumber(odRdr[7].ToString().Replace(",", "")) == true ? odRdr[7] : 0;
                                    dr["I"] = Tools.IsNumber(odRdr[8].ToString().Replace(",", "")) == true ? odRdr[8] : 0;
                                    dr["J"] = Tools.IsNumber(odRdr[9].ToString().Replace(",", "")) == true ? odRdr[9] : 0;
                                    dr["K"] = odRdr[10];
                                    dr["L"] = odRdr[11];
                                    dr["M"] = odRdr[12];
                                    dr["N"] = odRdr[13];
                                    dr["O"] = odRdr[14];
                                    dr["P"] = odRdr[15];


                                    if (dr["A"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }

                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                // kirim msg gagal import
                throw err;
            }
        }

        public Boolean Payment_Voucher(string _userID, Cashier _cashier)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @" 
                            
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                        DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,F.ID DepartmentID,Case When DebitCredit ='D' then 1 else 2 end Row       
                        from Cashier C       
                        left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)         
                        UNION ALL       
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                        'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID ,3 Row          
                        from Cashier C       
                        left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID  and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)    
                        group by C.EntryUsersID, C.ApprovedUsersID, Valuedate,A.ID, A.Name, C.Reference     
                        Order By row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payment Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.PreparedBy, r.ApprovedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {



                                            //ThickBox Border HEADER

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsDetail.Rate;
                                            //worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseDebit;
                                            //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 12].Value = rsDetail.BaseCredit;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                       

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();

                                        incRowExcel = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(      " + rsHeader.Key.ApprovedBy + "      )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:G";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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

        public Boolean Receipt_Voucher(string _userID, Cashier _cashier)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @" 
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                            valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID , case when DebitCredit ='D' then 2 else 3 end Row    
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)         
                            UNION ALL         
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,        
                            valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID, 1 Row             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)       
                            group by C.EntryUsersID , C.ApprovedUsersID, Valuedate,A.ID, A.Name,C.Reference     
                            Order By Row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipt Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy, r.PreparedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {



                                            //ThickBox Border HEADER

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;





                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }
                                        
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(      " + rsHeader.Key.ApprovedBy + "      )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Row(incRowExcel).PageBreak = true;


                                    }

                                    string _rangeDetail = "A:G";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RECEIPT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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

        public List<Journal> Get_ReferenceComboByCashierType(DateTime _valuedateFrom, DateTime _valuedateTo, string _cashierType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Journal> L_Journal = new List<Journal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_cashierType == "IN")
                        {
                            _cashierType = "And Type in ( 'CR')";
                        }
                        else if (_cashierType == "OUT")
                        {
                            _cashierType = "And Type in ( 'CP')";
                        }
                        else if (_cashierType == "ALL")
                        {
                            _cashierType = "And Type in ( 'CR', 'CP' )";
                        }

                        cmd.CommandText = @"  Select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from Cashier 
                                          where valuedate between @ValueDateFrom and @ValueDateTo " + _cashierType +
                                          " order by Refno ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _valuedateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _valuedateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Journal M_Journal = new Journal();
                                    M_Journal.Reference = Convert.ToString(dr["Reference"]);
                                    L_Journal.Add(M_Journal);
                                }
                            }
                            return L_Journal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Journal> Get_ReferenceComboByCashierTypeRpt(DateTime _valuedateFrom, DateTime _valuedateTo, string _cashierType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Journal> L_Journal = new List<Journal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_cashierType == "IN")
                        {
                            _cashierType = "And Type in ( 'CR')";
                        }
                        else if (_cashierType == "OUT")
                        {
                            _cashierType = "And Type in ( 'CP')";
                        }
                        else if (_cashierType == "ALL")
                        {
                            _cashierType = "And Type in ( 'CR', 'CP' )";
                        }

                        cmd.CommandText = @" Select 'ALL' Reference,0 RefNo Union ALL  Select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from Cashier 
                                          where valuedate between @ValueDateFrom and @ValueDateTo " + _cashierType +
                                          " order by Refno ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _valuedateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _valuedateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Journal M_Journal = new Journal();
                                    M_Journal.Reference = Convert.ToString(dr["Reference"]);
                                    M_Journal.RefNo = Convert.ToInt32(dr["RefNo"]);
                                    L_Journal.Add(M_Journal);
                                }
                            }
                            return L_Journal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<ReferenceDetail> ReferenceDetail(int _status, string _reference, string _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceDetail> L_cashier = new List<ReferenceDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText =
                            @" 
                            Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                            DebitCredit,(Case When DebitCredit ='D' then cast(BaseDebit as decimal(30,2)) else 0 end) Debit,(Case When DebitCredit ='D' then 0 else cast(BaseCredit as decimal(30,2)) end) Credit,isnull(F.ID,'') DepartmentID       
                            from Cashier C       
                            left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference  and C.Status in (1,2)      
                            UNION ALL       
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                            'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID           
                            from Cashier C       
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference and C.Status in (1,2) 
                            group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                            Order By DebitCredit Desc ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else cast(BaseCredit as decimal(30,2)) end) Debit,(Case When DebitCredit ='C' then cast(BaseDebit as decimal(30,2)) else 0 end) Credit,F.ID DepartmentID       
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where C.Reference = @Reference  and C.Status in (1,2)         
                            UNION ALL         
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where C.Reference = @Reference  and C.Status in (1,2)       
                            group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                            Order By DebitCredit Desc ";
             
                        }
                        

                        cmd.Parameters.AddWithValue("@Reference", _reference);
                        cmd.Parameters.AddWithValue("@Status", _status);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceDetail M_cashier = new ReferenceDetail();
                                    M_cashier.ValueDate = Convert.ToString(dr["ValueDate"]);
                                    M_cashier.AccountID = Convert.ToString(dr["AccountID"]);
                                    M_cashier.AccountName = Convert.ToString(dr["AccountName"]);
                                    M_cashier.Description = Convert.ToString(dr["Description"]);
                                    M_cashier.DebitCredit = Convert.ToString(dr["DebitCredit"]);
                                    M_cashier.Debit = Convert.ToDecimal(dr["Debit"]);
                                    M_cashier.Credit = Convert.ToDecimal(dr["Credit"]);
                                    M_cashier.DepartmentID = Convert.ToString(dr["DepartmentID"]);
                                    L_cashier.Add(M_cashier);
                                }
                            }
                            return L_cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ReferenceDetail> CashierIDDetail(long _cashierID, int _periodPK, string _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceDetail> L_cashier = new List<ReferenceDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText =
                            @" 
                            
                            Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                            DebitCredit,(Case When DebitCredit ='D' then cast(BaseDebit as decimal(30,2)) else 0 end) Debit,(Case When DebitCredit ='D' then 0 else cast(BaseCredit as decimal(30,2)) end) Credit,isnull(F.ID,'') DepartmentID, case when  DebitCredit ='D' then 1 else 2 end Row    
                            from Cashier C       
                            left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.CashierID = @CashierID  and C.Status in (1,2)      and C.Type = @Type and C.PeriodPK = @PeriodPK
                            UNION ALL       
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                            'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID, 3 Row           
                            from Cashier C       
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.CashierID = @CashierID and C.Status in (1,2) and C.Type = @Type and C.PeriodPK = @PeriodPK
                            group by C.EntryUsersID,CashierID , Valuedate,A.ID, A.Name  
                            UNION ALL 
                            Select null,'', 'TOTAL : ', '',       
                            '',sum((Case When DebitCredit ='C' then 0 else cast(BaseCredit as decimal(30,2)) end)) Debit,sum((Case When DebitCredit ='C' then 0 else cast(BaseCredit as decimal(30,2)) end)) Credit,'' ,4 Row      
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where C.CashierID = @CashierID  and C.Status in (1,2)     and C.Type = @Type  and C.PeriodPK = @PeriodPK  
                            Order By Row,AccountID asc ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"
                            Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else cast(BaseCredit as decimal(30,2)) end) Debit,(Case When DebitCredit ='C' then cast(BaseDebit as decimal(30,2)) else 0 end) Credit,F.ID DepartmentID , Case When DebitCredit ='D' then 2 else 3 end Row      
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where C.CashierID = @CashierID  and C.Status in (1,2)     and C.Type = @Type  and C.PeriodPK = @PeriodPK  
                            UNION ALL         
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID , 1 Row            
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where C.CashierID = @CashierID  and C.Status in (1,2)       and C.Type = @Type and C.PeriodPK = @PeriodPK
                            group by C.EntryUsersID,CashierID , Valuedate,A.ID, A.Name   
                            UNION ALL 
                            Select null,'', 'TOTAL : ', '',       
                            '',sum((Case When DebitCredit ='D' then 0 else cast(BaseCredit as decimal(30,2)) end)) Debit,sum((Case When DebitCredit ='D' then 0 else cast(BaseCredit as decimal(30,2)) end)) Credit,'' ,4 Row      
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where C.CashierID = @CashierID  and C.Status in (1,2)     and C.Type = @Type  and C.PeriodPK = @PeriodPK  
  
                            Order By Row,AccountID asc ";

                        }


                        cmd.Parameters.AddWithValue("@CashierID", _cashierID);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceDetail M_cashier = new ReferenceDetail();
                                    M_cashier.ValueDate = dr["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ValueDate"]);
                                    M_cashier.AccountID = Convert.ToString(dr["AccountID"]);
                                    M_cashier.AccountName = Convert.ToString(dr["AccountName"]);
                                    M_cashier.Description = Convert.ToString(dr["Description"]);
                                    M_cashier.DebitCredit = Convert.ToString(dr["DebitCredit"]);
                                    M_cashier.Debit = Convert.ToDecimal(dr["Debit"]);
                                    M_cashier.Credit = Convert.ToDecimal(dr["Credit"]);
                                    M_cashier.DepartmentID = Convert.ToString(dr["DepartmentID"]);
                                    L_cashier.Add(M_cashier);
                                }
                            }
                            return L_cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckPostingCashier(DateTime _dateFrom, DateTime _dateTo, string _type)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Create Table #A
                        (CashierID nvarchar(50))
                        
                        Insert Into #A(CashierID)
                        select distinct CashierID from Cashier where status = 1 and Type = @Type and CashierID in (
                        select distinct CashierID from Cashier 
                        where ValueDate between @DateFrom and @DateTo and Selected = 1 and status = 2 and Type = @Type)

                        IF EXISTS(select CashierID from Cashier where status = 1 and Type = @Type and CashierID in (
                        select distinct CashierID from Cashier 
                        where ValueDate between @DateFrom and @DateTo and Selected = 1 and status = 2 and Type = @Type))
                        BEGIN
	                        DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + CashierID
                            FROM #A
                            SELECT 'Posting Cancel, Please Check Tab Pending , Cashier ID : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Type", _type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckPostingCashierByCashierID(DateTime _dateFrom, DateTime _dateTo, string _type, int _cashierID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                     IF EXISTS(select CashierID from Cashier where status = 1 and Type = @Type and CashierID in (@CashierID))
                        BEGIN
	                       
                            SELECT 'Posting Cancel, Please Check Tab Pending ,  ID : ' + cast (@cashierID as nvarchar(100)) as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@CashierID", _cashierID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public string Validate_CheckPostingCashierByReference(DateTime _dateFrom, DateTime _dateTo, string _type, int _cashierPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

declare @Reference nvarchar(200)
select  @Reference =  reference from Cashier where cashierPK = @CashierPK

                     IF EXISTS(select reference from Cashier where status = 1 and Type = @Type and reference in (@reference))
                        BEGIN
	                       
                            SELECT 'Posting Cancel, Please Check Tab Pending ,  Reference : ' + @reference as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@CashierPK", _cashierPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckPostingBySelectedCashierByReference(DateTime _dateFrom, DateTime _dateTo, string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                       Create Table #A
                        (CashierID nvarchar(50))
                        
                        Insert Into #A(CashierID)
                        select Reference from Cashier where status = 1 and Type = @Type and Reference in (
                        select distinct Reference from Cashier 
                        where ValueDate between @DateFrom and @DateTo " + paramCashierPK + @" and status = 2 and Type = @Type)

                        IF EXISTS(select Reference from Cashier where status = 1 and Type = @Type and Reference in (
                        select distinct Reference from Cashier 
                        where ValueDate between @DateFrom and @DateTo " + paramCashierPK + @" and status = 2 and Type = @Type))
                        BEGIN
	                        DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + CashierID
                            FROM #A
                            SELECT 'Posting Cancel, Please Check Tab Pending ,  Reference : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END    ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Type", _type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public string Validate_OtherTransactionBySelected(string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        IF EXISTS(select TrxNo,CashierID from Cashier where TrxNo <> 0 and Status in(1,2) and Type = @Type " + paramCashierPK + @")
                        BEGIN
	                        DECLARE @CashierID nvarchar(50)
                            SELECT @CashierID = CashierID 
                            FROM Cashier where TrxNo <> 0  and Status in(1,2)  and Type = @Type  " + paramCashierPK + @"
                            SELECT 'Reject/Update/Revise Cancel, Cashier ID : ' + @CashierID + ' , Has Add From Trx PortFolio' as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";
                        cmd.Parameters.AddWithValue("@Type", _type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_OtherTransaction(int _cashierPK,string _type)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        IF EXISTS(select TrxNo,CashierID from Cashier where TrxNo <> 0 and Status in(1,2) and Type = @Type  and CashierPK = @CashierPK)
                        BEGIN
	                        DECLARE @CashierID nvarchar(50)
                            SELECT @CashierID = CashierID 
                            FROM Cashier where TrxNo <> 0  and Status in(1,2)  and Type = @Type and CashierPK = @CashierPK
                            SELECT 'Reject/Update/Revise Cancel, Cashier ID : ' + @CashierID + ' , Has Add From Trx PortFolio' as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@CashierPK", _cashierPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
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