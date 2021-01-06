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
    public class TrxPortfolioReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"
                            INSERT INTO [dbo].[TrxPortfolio]    
                           ([TrxPortfolioPK],[HistoryPK],[Status],[PeriodPK],    
                            [ValueDate],[Reference],[InstrumentPK],[InstrumentTypePK],[Price],[AcqPrice],[Lot],    
                            [LotInShare],[Volume],[Amount],[InterestAmount],[IncomeTaxInterestAmount],[IncomeTaxGainAmount],[GrossAmount],[NetAmount],
                            [TrxType],[TrxTypeID],[CounterpartPK],[SettledDate],[AcqDate],[AcqVolume],
                            [AcqPrice1],[AcqDate1],[AcqVolume1], [AcqPrice2],[AcqDate2],[AcqVolume2], [AcqPrice3],[AcqDate3],[AcqVolume3], [AcqPrice4],[AcqDate4],[AcqVolume4], [AcqPrice5],[AcqDate5],[AcqVolume5], [AcqPrice6],[AcqDate6],[AcqVolume6], [AcqPrice7],[AcqDate7],[AcqVolume7], [AcqPrice8],[AcqDate8],[AcqVolume8], [AcqPrice9],[AcqDate9],[AcqVolume9],
                            [MaturityDate],[LastCouponDate],[NextCouponDate],[InterestPercent],[CompanyAccountTradingPK],[CashRefPK],[BrokerageFeePercent],[BrokerageFeeAmount],[LevyAmount],[VATAmount],[KPEIAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount],[RealisedAmount],[BankPK],[BankBranchPK],[Category],
                            ";

        string _paramaterCommand = @" @PeriodPK,@ValueDate,@Reference,@InstrumentPK,@InstrumentTypePK,@Price,@AcqPrice,@Lot, 
                                  @LotInShare,@Volume,@Amount,@InterestAmount,@IncomeTaxInterestAmount,@IncomeTaxGainAmount,@GrossAmount,@NetAmount,@TrxType,@TrxTypeID,@CounterpartPK,@SettledDate,@AcqDate,@AcqVolume,
                                    @AcqPrice1,@AcqDate1,@AcqVolume1,@AcqPrice2,@AcqDate2,@AcqVolume2,@AcqPrice3,@AcqDate3,@AcqVolume3,@AcqPrice4,@AcqDate4,@AcqVolume4,@AcqPrice5,@AcqDate5,@AcqVolume5,@AcqPrice6,@AcqDate6,@AcqVolume6,@AcqPrice7,@AcqDate7,@AcqVolume7,@AcqPrice8,@AcqDate8,@AcqVolume8,@AcqPrice9,@AcqDate9,@AcqVolume9,
                                    @MaturityDate,@LastCouponDate,@NextCouponDate,@InterestPercent,@CompanyAccountTradingPK,@CashRefPK,@BrokerageFeePercent,@BrokerageFeeAmount,@LevyAmount,@VATAmount,@KPEIAmount,@WHTAmount,@OTCAmount,@IncomeTaxSellAmount,@RealisedAmount,@BankPK, @BankBranchPK,@Category, ";

        string _insertJournal = "    INSERT INTO [dbo].[Journal]      " +
                                " ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[TrxNo],[TrxName], " +
                                " [Reference],[Type],[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID], " +
                                " [EntryTime],[ApprovedUsersID],[ApprovedTime]) ";

        string _insertJournalDetail = "    INSERT INTO [dbo].[JournalDetail]      " +
                                      " ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK], " +
                                      " [DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK], " +
                                      " [DetailDescription],[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate], " +
                                      " [BaseDebit],[BaseCredit],[LastUsersID]) ";


        string _declareTrxPortfolio = "  Declare @UserID Nvarchar(70)    " +
                                        "  Declare @ValueDate Datetime     " +
                                        "  Declare @InstrumentTypePK int     " +
                                        "  Declare @Description Nvarchar(1000)    " +
                                        "  Declare @InstrumentPK int      " +
                                        "  Declare @Price numeric(19, 4)    " +
                                        "  Declare @Lot numeric(19, 4)        " +
                                        "  Declare @Volume numeric(19, 4)     " +
                                        "  Declare @Amount numeric(19, 4)      " +
                                        "  Declare @TrxType Nvarchar(10)      " +
                                        "  Declare @CounterpartPK int        " +
                                        "  Declare @CompanyAccountTradingPK int        " +
                                        "  Declare @CashRefPK int        " +
                                        "  Declare @SettledDate Datetime       " +
                                        "  Declare @AcqDate Datetime       " +
                                        "  Declare @MaturityDate Datetime         " +
                                        "  Declare @InterestPercent numeric(19, 4)    " +
                                        "  Declare @periodPK int      ";

        //2
        private TrxPortfolio setTrxPortfolio(SqlDataReader dr)
        {
            TrxPortfolio M_TrxPortfolio = new TrxPortfolio();
            M_TrxPortfolio.TrxPortfolioPK = Convert.ToInt32(dr["TrxPortfolioPK"]);
            M_TrxPortfolio.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TrxPortfolio.Selected = Convert.ToBoolean(dr["Selected"]);
            M_TrxPortfolio.Status = Convert.ToInt32(dr["Status"]);
            M_TrxPortfolio.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TrxPortfolio.Notes = Convert.ToString(dr["Notes"]);
            M_TrxPortfolio.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_TrxPortfolio.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_TrxPortfolio.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_TrxPortfolio.Reference = dr["Reference"].ToString();
            M_TrxPortfolio.InstrumentTypePK = dr["InstrumentTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentTypePK"]);
            M_TrxPortfolio.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_TrxPortfolio.InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]);
            M_TrxPortfolio.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_TrxPortfolio.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_TrxPortfolio.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
            M_TrxPortfolio.BankID = Convert.ToString(dr["BankID"]);
            M_TrxPortfolio.BankName = Convert.ToString(dr["BankName"]);
            M_TrxPortfolio.BankBranchPK = dr["BankBranchPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankBranchPK"]);
            M_TrxPortfolio.BankBranchID = Convert.ToString(dr["BankBranchID"]);
            M_TrxPortfolio.Price = dr["Price"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Price"]);
            M_TrxPortfolio.Lot = dr["Lot"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Lot"]);
            M_TrxPortfolio.LotInShare = dr["LotInShare"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["LotInShare"]);
            M_TrxPortfolio.Volume = dr["Volume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Volume"]);
            M_TrxPortfolio.Amount = dr["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Amount"]);
            M_TrxPortfolio.InterestAmount = dr["InterestAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["InterestAmount"]);
            M_TrxPortfolio.IncomeTaxInterestAmount = dr["IncomeTaxInterestAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
            M_TrxPortfolio.IncomeTaxGainAmount = dr["IncomeTaxGainAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
            M_TrxPortfolio.LevyAmount = dr["LevyAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["LevyAmount"]);
            M_TrxPortfolio.VATAmount = dr["VATAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["VATAmount"]);
            M_TrxPortfolio.KPEIAmount = dr["KPEIAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["KPEIAmount"]);
            M_TrxPortfolio.WHTAmount = dr["WHTAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WHTAmount"]);
            M_TrxPortfolio.OTCAmount = dr["OTCAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OTCAmount"]);
            M_TrxPortfolio.IncomeTaxSellAmount = dr["IncomeTaxSellAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["IncomeTaxSellAmount"]);
            M_TrxPortfolio.RealisedAmount = dr["RealisedAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["RealisedAmount"]);
            M_TrxPortfolio.GrossAmount = dr["GrossAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
            M_TrxPortfolio.NetAmount = dr["NetAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["NetAmount"]);
            M_TrxPortfolio.TrxType = dr["TrxType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TrxType"]);
            M_TrxPortfolio.TrxTypeID = dr["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TrxTypeID"]);
            M_TrxPortfolio.CounterpartPK = dr["CounterpartPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CounterpartPK"]);
            M_TrxPortfolio.CounterpartID = dr["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CounterpartID"]);
            M_TrxPortfolio.CounterpartName = dr["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CounterpartName"]);
            M_TrxPortfolio.SettledDate = dr["SettledDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SettledDate"]);
            M_TrxPortfolio.AcqPrice = dr["AcqPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice"]);
            M_TrxPortfolio.AcqDate = dr["AcqDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate"]);
            M_TrxPortfolio.AcqVolume = dr["AcqVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume"]);
            M_TrxPortfolio.AcqPrice1 = dr["AcqPrice1"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice1"]);
            M_TrxPortfolio.AcqDate1 = dr["AcqDate1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate1"]);
            M_TrxPortfolio.AcqVolume1 = dr["AcqVolume1"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume1"]);
            M_TrxPortfolio.AcqPrice2 = dr["AcqPrice2"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice2"]);
            M_TrxPortfolio.AcqDate2 = dr["AcqDate2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate2"]);
            M_TrxPortfolio.AcqVolume2 = dr["AcqVolume2"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume2"]);
            M_TrxPortfolio.AcqPrice3 = dr["AcqPrice3"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice3"]);
            M_TrxPortfolio.AcqDate3 = dr["AcqDate3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate3"]);
            M_TrxPortfolio.AcqVolume3 = dr["AcqVolume3"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume3"]);
            M_TrxPortfolio.AcqPrice4 = dr["AcqPrice4"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice4"]);
            M_TrxPortfolio.AcqDate4 = dr["AcqDate4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate4"]);
            M_TrxPortfolio.AcqVolume4 = dr["AcqVolume4"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume4"]);
            M_TrxPortfolio.AcqPrice5 = dr["AcqPrice5"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice5"]);
            M_TrxPortfolio.AcqDate5 = dr["AcqDate5"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate5"]);
            M_TrxPortfolio.AcqVolume5 = dr["AcqVolume5"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume5"]);
            M_TrxPortfolio.AcqPrice6 = dr["AcqPrice6"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice6"]);
            M_TrxPortfolio.AcqDate6 = dr["AcqDate6"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate6"]);
            M_TrxPortfolio.AcqVolume6 = dr["AcqVolume6"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume6"]);
            M_TrxPortfolio.AcqPrice7 = dr["AcqPrice7"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice7"]);
            M_TrxPortfolio.AcqDate7 = dr["AcqDate7"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate7"]);
            M_TrxPortfolio.AcqVolume7 = dr["AcqVolume7"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume7"]);
            M_TrxPortfolio.AcqPrice8 = dr["AcqPrice8"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice8"]);
            M_TrxPortfolio.AcqDate8 = dr["AcqDate8"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate8"]);
            M_TrxPortfolio.AcqVolume8 = dr["AcqVolume8"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume8"]);
            M_TrxPortfolio.AcqPrice9 = dr["AcqPrice9"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqPrice9"]);
            M_TrxPortfolio.AcqDate9 = dr["AcqDate9"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AcqDate9"]);
            M_TrxPortfolio.AcqVolume9 = dr["AcqVolume9"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AcqVolume9"]);
            M_TrxPortfolio.LastCouponDate = dr["LastCouponDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastCouponDate"]);
            M_TrxPortfolio.NextCouponDate = dr["NextCouponDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NextCouponDate"]);
            M_TrxPortfolio.MaturityDate = dr["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["MaturityDate"]);
            M_TrxPortfolio.InterestPercent = dr["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["InterestPercent"]);
            M_TrxPortfolio.CompanyAccountTradingPK = dr["CompanyAccountTradingPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CompanyAccountTradingPK"]);
            M_TrxPortfolio.CompanyAccountTradingID = dr["CompanyAccountTradingID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CompanyAccountTradingID"]);
            M_TrxPortfolio.CompanyAccountTradingName = dr["CompanyAccountTradingName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CompanyAccountTradingName"]);
            M_TrxPortfolio.CashRefPK = dr["CashRefPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashRefPK"]);
            M_TrxPortfolio.CashRefID = dr["CashRefID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashRefID"]);
            M_TrxPortfolio.BrokerageFeePercent = dr["BrokerageFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["BrokerageFeePercent"]);
            M_TrxPortfolio.BrokerageFeeAmount = dr["BrokerageFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["BrokerageFeeAmount"]);
            M_TrxPortfolio.Category = dr["Category"].ToString();
            M_TrxPortfolio.Posted = dr["Posted"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Posted"]);
            M_TrxPortfolio.PostedBy = dr["PostedBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PostedBy"]);
            M_TrxPortfolio.PostedTime = dr["PostedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PostedTime"]);
            M_TrxPortfolio.Revised = dr["Revised"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Revised"]);
            M_TrxPortfolio.RevisedBy = dr["RevisedBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RevisedBy"]);
            M_TrxPortfolio.RevisedTime = dr["RevisedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RevisedTime"]);
            M_TrxPortfolio.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TrxPortfolio.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TrxPortfolio.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TrxPortfolio.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TrxPortfolio.EntryTime = dr["EntryTime"].ToString();
            M_TrxPortfolio.UpdateTime = dr["UpdateTime"].ToString();
            M_TrxPortfolio.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TrxPortfolio.VoidTime = dr["VoidTime"].ToString();
            M_TrxPortfolio.DBUserID = dr["DBUserID"].ToString();
            M_TrxPortfolio.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TrxPortfolio.LastUpdate = dr["LastUpdate"].ToString();
            M_TrxPortfolio.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_TrxPortfolio;
        }

        //3
        public List<TrxPortfolio> TrxPortfolio_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxPortfolio> L_TrxPortfolio = new List<TrxPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " select case when TP.status=1 then 'PENDING' else Case When TP.status = 2 then 'APPROVED' else Case when TP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefID,P.ID PeriodID,MV.DescTwo InstrumentTypeID,I.ID InstrumentID,I.Name InstrumentName,CP.ID CounterpartID, " +
                             " CP.Name CounterpartName,CAT.ID CompanyAccountTradingID, CAT.Name CompanyAccountTradingName, " +
                             " TP.TrxTypeID," +
                             " TP.* from TrxPortfolio TP left join     " +
                             " Period P on TP.PeriodPK = P.PeriodPK and P.Status = 2 left join     " +
                             " Instrument I on TP.InstrumentPK = I.InstrumentPK and I.Status = 2 left join     " +
                             " Counterpart CP on TP.CounterpartPK = CP.CounterpartPK and CP.Status = 2 left join     " +
                             " CompanyAccountTrading CAT on TP.CompanyAccountTradingPK = CAT.CompanyAccountTradingPK and CAT.Status = 2  left join " +
                             " CashRef CR on TP.CashRefPK = CR.CashRefPK and CR.Status = 2  left join " +
                             " MasterValue MV on TP.InstrumentTypePK = MV.Code and MV.Status = 2 and MV.ID = 'InstrumentType'    " +
                             " where  TP.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " select case when TP.status=1 then 'PENDING' else Case When TP.status = 2 then 'APPROVED' else Case when TP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefID,P.ID PeriodID,MV.DescTwo InstrumentTypeID,I.ID InstrumentID,I.Name InstrumentName,CP.ID CounterpartID, " +
                                  " CP.Name CounterpartName,CAT.ID CompanyAccountID, CAT.Name CompanyAccountName, " +
                                  " TP.TrxTypeID," +
                                  " TP.* from TrxPortfolio TP left join     " +
                                  " Period P on TP.PeriodPK = P.PeriodPK and P.Status = 2 left join     " +
                                  " Instrument I on TP.InstrumentPK = I.InstrumentPK and I.Status = 2 left join     " +
                                  " Counterpart CP on TP.CounterpartPK = CP.CounterpartPK and CP.Status = 2 left join     " +
                                  " CompanyAccountTrading CAT on TP.CompanyAccountTradingPK = CAT.CompanyAccountTradingPK and CAT.Status = 2  left join " +
                                  " CashRef CR on TP.CashRefPK = CR.CashRefPK and CR.Status = 2  left join " +
                                  " MasterValue MV on TP.InstrumentTypePK = MV.Code and MV.Status = 2 and MV.ID = 'InstrumentType'    ";
                           
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxPortfolio.Add(setTrxPortfolio(dr));
                                }
                            }
                            return L_TrxPortfolio;
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
        public TrxPortfolio TrxPortfolio_SelectByTrxPortfolioPK(int _trxPortfolioPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " select CR.ID CashRefID,P.ID PeriodID,IT.ID InstrumentTypeID,I.ID InstrumentID,I.Name InstrumentName,CP.ID CounterpartID, " +
                              " CP.Name CounterpartName,S.ID FundClientID, S.Name FundClientName, " +
                              " TP.TrxTypeID," +
                              " * from TrxPortfolio TP left join     " +
                              " Period P on TP.PeriodPK = P.PeriodPK and P.Status = 2 left join     " +
                              " InstrumentType IT on TP.InstrumentTypePK = IT.InstrumentTypePK and IT.Status = 2 left join     " +
                              " Instrument I on TP.InstrumentPK = I.InstrumentPK and I.Status = 2 left join     " +
                              " Counterpart CP on TP.CounterpartPK = CP.CounterpartPK and CP.Status = 2 left join     " +
                              " CashRef CR on TP.CashRefPK = CR.CashRefPK and CR.Status = 2  left join " +
                              " FundClient S on TP.CompanyAccountTradingPK = S.CompanyAccountTradingPK and S.Status = 2     " +
                            "where  TP.status = 4";
                        cmd.Parameters.AddWithValue("@TrxPortfolioPK", _trxPortfolioPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setTrxPortfolio(dr);
                            }
                            return null;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //5
        public int TrxPortfolio_Add(TrxPortfolio _trxPortfolio, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(TrxPortfolioPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from TrxPortfolio";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _trxPortfolio.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(TrxPortfolioPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from TrxPortfolio";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _trxPortfolio.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _trxPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@Reference", _trxPortfolio.Reference);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _trxPortfolio.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                        cmd.Parameters.AddWithValue("@BankPK", _trxPortfolio.BankPK);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _trxPortfolio.BankBranchPK);
                        cmd.Parameters.AddWithValue("@Price", _trxPortfolio.Price);
                        cmd.Parameters.AddWithValue("@AcqPrice", _trxPortfolio.AcqPrice);
                        cmd.Parameters.AddWithValue("@Lot", _trxPortfolio.Lot);
                        cmd.Parameters.AddWithValue("@LotInShare", _trxPortfolio.LotInShare);
                        cmd.Parameters.AddWithValue("@Volume", _trxPortfolio.Volume);
                        cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                        cmd.Parameters.AddWithValue("@InterestAmount", _trxPortfolio.InterestAmount);
                        cmd.Parameters.AddWithValue("@IncomeTaxInterestAmount", _trxPortfolio.IncomeTaxInterestAmount);
                        cmd.Parameters.AddWithValue("@IncomeTaxGainAmount", _trxPortfolio.IncomeTaxGainAmount);
                        cmd.Parameters.AddWithValue("@GrossAmount", _trxPortfolio.GrossAmount);
                        cmd.Parameters.AddWithValue("@NetAmount", _trxPortfolio.NetAmount);
                        cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                        cmd.Parameters.AddWithValue("@TrxTypeID", _trxPortfolio.TrxTypeID);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                        if (_trxPortfolio.Type == 3)
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.ValueDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.SettledDate);
                        }
                        cmd.Parameters.AddWithValue("@AcqDate", _trxPortfolio.AcqDate);
                        cmd.Parameters.AddWithValue("@AcqVolume", _trxPortfolio.AcqVolume);
                        cmd.Parameters.AddWithValue("@AcqPrice1", _trxPortfolio.AcqPrice1);
                        cmd.Parameters.AddWithValue("@AcqDate1", _trxPortfolio.AcqDate1);
                        cmd.Parameters.AddWithValue("@AcqVolume1", _trxPortfolio.AcqVolume1);
                        cmd.Parameters.AddWithValue("@AcqPrice2", _trxPortfolio.AcqPrice2);
                        cmd.Parameters.AddWithValue("@AcqDate2", _trxPortfolio.AcqDate2);
                        cmd.Parameters.AddWithValue("@AcqVolume2", _trxPortfolio.AcqVolume2);
                        cmd.Parameters.AddWithValue("@AcqPrice3", _trxPortfolio.AcqPrice3);
                        cmd.Parameters.AddWithValue("@AcqDate3", _trxPortfolio.AcqDate3);
                        cmd.Parameters.AddWithValue("@AcqVolume3", _trxPortfolio.AcqVolume3);
                        cmd.Parameters.AddWithValue("@AcqPrice4", _trxPortfolio.AcqPrice4);
                        cmd.Parameters.AddWithValue("@AcqDate4", _trxPortfolio.AcqDate4);
                        cmd.Parameters.AddWithValue("@AcqVolume4", _trxPortfolio.AcqVolume4);
                        cmd.Parameters.AddWithValue("@AcqPrice5", _trxPortfolio.AcqPrice5);
                        cmd.Parameters.AddWithValue("@AcqDate5", _trxPortfolio.AcqDate5);
                        cmd.Parameters.AddWithValue("@AcqVolume5", _trxPortfolio.AcqVolume5);
                        cmd.Parameters.AddWithValue("@AcqPrice6", _trxPortfolio.AcqPrice6);
                        cmd.Parameters.AddWithValue("@AcqDate6", _trxPortfolio.AcqDate6);
                        cmd.Parameters.AddWithValue("@AcqVolume6", _trxPortfolio.AcqVolume6);
                        cmd.Parameters.AddWithValue("@AcqPrice7", _trxPortfolio.AcqPrice7);
                        cmd.Parameters.AddWithValue("@AcqDate7", _trxPortfolio.AcqDate7);
                        cmd.Parameters.AddWithValue("@AcqVolume7", _trxPortfolio.AcqVolume7);
                        cmd.Parameters.AddWithValue("@AcqPrice8", _trxPortfolio.AcqPrice8);
                        cmd.Parameters.AddWithValue("@AcqDate8", _trxPortfolio.AcqDate8);
                        cmd.Parameters.AddWithValue("@AcqVolume8", _trxPortfolio.AcqVolume8);
                        cmd.Parameters.AddWithValue("@AcqPrice9", _trxPortfolio.AcqPrice9);
                        cmd.Parameters.AddWithValue("@AcqDate9", _trxPortfolio.AcqDate9);
                        cmd.Parameters.AddWithValue("@AcqVolume9", _trxPortfolio.AcqVolume9);
                        cmd.Parameters.AddWithValue("@LastCouponDate", _trxPortfolio.LastCouponDate);
                        cmd.Parameters.AddWithValue("@NextCouponDate", _trxPortfolio.NextCouponDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _trxPortfolio.MaturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _trxPortfolio.InterestPercent);
                        cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _trxPortfolio.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@CashRefPK", _trxPortfolio.CashRefPK);
                        cmd.Parameters.AddWithValue("@BrokerageFeePercent", _trxPortfolio.BrokerageFeePercent);
                        cmd.Parameters.AddWithValue("@BrokerageFeeAmount", _trxPortfolio.BrokerageFeeAmount);
                        cmd.Parameters.AddWithValue("@LevyAmount", _trxPortfolio.LevyAmount);
                        cmd.Parameters.AddWithValue("@VATAmount", _trxPortfolio.VATAmount);
                        cmd.Parameters.AddWithValue("@KPEIAmount", _trxPortfolio.KPEIAmount);
                        cmd.Parameters.AddWithValue("@WHTAmount", _trxPortfolio.WHTAmount);
                        cmd.Parameters.AddWithValue("@OTCAmount", _trxPortfolio.OTCAmount);
                        cmd.Parameters.AddWithValue("@IncomeTaxSellAmount", _trxPortfolio.IncomeTaxSellAmount);
                        cmd.Parameters.AddWithValue("@RealisedAmount", _trxPortfolio.RealisedAmount);
                        cmd.Parameters.AddWithValue("@Category", _trxPortfolio.Category);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _trxPortfolio.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "TrxPortfolio");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //6
        public int TrxPortfolio_Update(TrxPortfolio _trxPortfolio, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_trxPortfolio.TrxPortfolioPK, _trxPortfolio.HistoryPK, "TrxPortfolio");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update TrxPortfolio set status=2,Notes=@Notes, " +
                            "PeriodPK=@PeriodPK,ValueDate=@ValueDate,Reference=@Reference,InstrumentTypePK=@InstrumentTypePK,InstrumentPK=@InstrumentPK, " +
                            "Price=@Price,AcqPrice=@AcqPrice,Lot=@Lot,LotInShare=@LotInShare,Volume=@Volume,Amount=@Amount, " +
                            "InterestAmount=@InterestAmount,IncomeTaxInterestAmount=@IncomeTaxInterestAmount,IncomeTaxGainAmount=@IncomeTaxGainAmount,GrossAmount=@GrossAmount,NetAmount=@NetAmount,TrxType=@TrxType,TrxTypeID=@TrxTypeID, " +
                            "AcqDate=@AcqDate,AcqVolume=@AcqVolume,AcqPrice1 = @AcqPrice1,AcqDate1 = @AcqDate1,AcqVolume1 = @AcqVolume1,AcqPrice2 = @AcqPrice2,AcqDate2 = @AcqDate2,AcqVolume2 = @AcqVolume2,AcqPrice3 = @AcqPrice3,AcqDate3 = @AcqDate3,AcqVolume3 = @AcqVolume3,AcqPrice4 = @AcqPrice4,AcqDate4 = @AcqDate4,AcqVolume4 = @AcqVolume4,AcqPrice5 = @AcqPrice5,AcqDate5 = @AcqDate5,AcqVolume5 = @AcqVolume5,AcqPrice6 = @AcqPrice6,AcqDate6 = @AcqDate6,AcqVolume6 = @AcqVolume6,AcqPrice7 = @AcqPrice7,AcqDate7 = @AcqDate7,AcqVolume7 = @AcqVolume7,AcqPrice8 = @AcqPrice8,AcqDate8 = @AcqDate8,AcqVolume8 = @AcqVolume8,AcqPrice9 = @AcqPrice9,AcqDate9 = @AcqDate9,AcqVolume9 = @AcqVolume9, " +
                            "CounterpartPK=@CounterpartPK,SettledDate=@SettledDate,MaturityDate=@MaturityDate,LastCouponDate=@LastCouponDate,NextCouponDate=@NextCouponDate,InterestPercent=@InterestPercent,CompanyAccountTradingPK=@CompanyAccountTradingPK,CashRefPK=@CashRefPK,BrokerageFeePercent=@BrokerageFeePercent,BrokerageFeeAmount=@BrokerageFeeAmount," +
                            "LevyAmount=@LevyAmount,VATAmount=@VATAmount,KPEIAmount=@KPEIAmount,WHTAmount=@WHTAmount,OTCAmount=@OTCAmount,IncomeTaxSellAmount=@IncomeTaxSellAmount,RealisedAmount=@RealisedAmount,BankPK=@BankPK,BankBranchPK=@BankBranchPK,Category=@Category, " + 
                            "ApprovedUsersID=@ApprovedUsersID, " +
                            "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                            "where TrxPortfolioPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _trxPortfolio.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                            cmd.Parameters.AddWithValue("@Notes", _trxPortfolio.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _trxPortfolio.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _trxPortfolio.ValueDate);
                            cmd.Parameters.AddWithValue("@Reference", _trxPortfolio.Reference);
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", _trxPortfolio.InstrumentTypePK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                            cmd.Parameters.AddWithValue("@BankPK", _trxPortfolio.BankPK);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _trxPortfolio.BankBranchPK);
                            cmd.Parameters.AddWithValue("@Price", _trxPortfolio.Price);
                            cmd.Parameters.AddWithValue("@AcqPrice", _trxPortfolio.AcqPrice);
                            cmd.Parameters.AddWithValue("@Lot", _trxPortfolio.Lot);
                            cmd.Parameters.AddWithValue("@LotInShare", _trxPortfolio.LotInShare);
                            cmd.Parameters.AddWithValue("@Volume", _trxPortfolio.Volume);
                            cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                            cmd.Parameters.AddWithValue("@InterestAmount", _trxPortfolio.InterestAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxInterestAmount", _trxPortfolio.IncomeTaxInterestAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxGainAmount", _trxPortfolio.IncomeTaxGainAmount);
                            cmd.Parameters.AddWithValue("@GrossAmount", _trxPortfolio.GrossAmount);
                            cmd.Parameters.AddWithValue("@NetAmount", _trxPortfolio.NetAmount);
                            cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                            cmd.Parameters.AddWithValue("@TrxTypeID", _trxPortfolio.TrxTypeID);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                            cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.SettledDate);
                            cmd.Parameters.AddWithValue("@AcqDate", _trxPortfolio.AcqDate);
                            cmd.Parameters.AddWithValue("@AcqVolume", _trxPortfolio.AcqVolume);
                            cmd.Parameters.AddWithValue("@AcqPrice1", _trxPortfolio.AcqPrice1);
                            cmd.Parameters.AddWithValue("@AcqDate1", _trxPortfolio.AcqDate1);
                            cmd.Parameters.AddWithValue("@AcqVolume1", _trxPortfolio.AcqVolume1);
                            cmd.Parameters.AddWithValue("@AcqPrice2", _trxPortfolio.AcqPrice2);
                            cmd.Parameters.AddWithValue("@AcqDate2", _trxPortfolio.AcqDate2);
                            cmd.Parameters.AddWithValue("@AcqVolume2", _trxPortfolio.AcqVolume2);
                            cmd.Parameters.AddWithValue("@AcqPrice3", _trxPortfolio.AcqPrice3);
                            cmd.Parameters.AddWithValue("@AcqDate3", _trxPortfolio.AcqDate3);
                            cmd.Parameters.AddWithValue("@AcqVolume3", _trxPortfolio.AcqVolume3);
                            cmd.Parameters.AddWithValue("@AcqPrice4", _trxPortfolio.AcqPrice4);
                            cmd.Parameters.AddWithValue("@AcqDate4", _trxPortfolio.AcqDate4);
                            cmd.Parameters.AddWithValue("@AcqVolume4", _trxPortfolio.AcqVolume4);
                            cmd.Parameters.AddWithValue("@AcqPrice5", _trxPortfolio.AcqPrice5);
                            cmd.Parameters.AddWithValue("@AcqDate5", _trxPortfolio.AcqDate5);
                            cmd.Parameters.AddWithValue("@AcqVolume5", _trxPortfolio.AcqVolume5);
                            cmd.Parameters.AddWithValue("@AcqPrice6", _trxPortfolio.AcqPrice6);
                            cmd.Parameters.AddWithValue("@AcqDate6", _trxPortfolio.AcqDate6);
                            cmd.Parameters.AddWithValue("@AcqVolume6", _trxPortfolio.AcqVolume6);
                            cmd.Parameters.AddWithValue("@AcqPrice7", _trxPortfolio.AcqPrice7);
                            cmd.Parameters.AddWithValue("@AcqDate7", _trxPortfolio.AcqDate7);
                            cmd.Parameters.AddWithValue("@AcqVolume7", _trxPortfolio.AcqVolume7);
                            cmd.Parameters.AddWithValue("@AcqPrice8", _trxPortfolio.AcqPrice8);
                            cmd.Parameters.AddWithValue("@AcqDate8", _trxPortfolio.AcqDate8);
                            cmd.Parameters.AddWithValue("@AcqVolume8", _trxPortfolio.AcqVolume8);
                            cmd.Parameters.AddWithValue("@AcqPrice9", _trxPortfolio.AcqPrice9);
                            cmd.Parameters.AddWithValue("@AcqDate9", _trxPortfolio.AcqDate9);
                            cmd.Parameters.AddWithValue("@AcqVolume9", _trxPortfolio.AcqVolume9);
                            cmd.Parameters.AddWithValue("@LastCouponDate", _trxPortfolio.LastCouponDate);
                            cmd.Parameters.AddWithValue("@NextCouponDate", _trxPortfolio.NextCouponDate);
                            cmd.Parameters.AddWithValue("@MaturityDate", _trxPortfolio.MaturityDate);
                            cmd.Parameters.AddWithValue("@InterestPercent", _trxPortfolio.InterestPercent);
                            cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _trxPortfolio.CompanyAccountTradingPK);
                            cmd.Parameters.AddWithValue("@CashRefPK", _trxPortfolio.CashRefPK);
                            cmd.Parameters.AddWithValue("@BrokerageFeePercent", _trxPortfolio.BrokerageFeePercent);
                            cmd.Parameters.AddWithValue("@BrokerageFeeAmount", _trxPortfolio.BrokerageFeeAmount);
                            cmd.Parameters.AddWithValue("@LevyAmount", _trxPortfolio.LevyAmount);
                            cmd.Parameters.AddWithValue("@VATAmount", _trxPortfolio.VATAmount);
                            cmd.Parameters.AddWithValue("@KPEIAmount", _trxPortfolio.KPEIAmount);
                            cmd.Parameters.AddWithValue("@WHTAmount", _trxPortfolio.WHTAmount);
                            cmd.Parameters.AddWithValue("@OTCAmount", _trxPortfolio.OTCAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxSellAmount", _trxPortfolio.IncomeTaxSellAmount);
                            cmd.Parameters.AddWithValue("@RealisedAmount", _trxPortfolio.RealisedAmount);
                            cmd.Parameters.AddWithValue("@Category", _trxPortfolio.Category);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _trxPortfolio.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _trxPortfolio.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TrxPortfolio set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxPortfolioPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _trxPortfolio.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update TrxPortfolio set Notes=@Notes, " +
                                "PeriodPK=@PeriodPK,ValueDate=@ValueDate,Reference=@Reference,InstrumentTypePK=@InstrumentTypePK,InstrumentPK=@InstrumentPK, " +
                                "Price=@Price,AcqPrice=@AcqPrice,Lot=@Lot,LotInShare=@LotInShare,Volume=@Volume,Amount=@Amount," +
                                "InterestAmount=@InterestAmount,IncomeTaxInterestAmount=@IncomeTaxInterestAmount,IncomeTaxGainAmount=@IncomeTaxGainAmount,GrossAmount=@GrossAmount,NetAmount=@NetAmount,TrxType=@TrxType,TrxTypeID=@TrxTypeID, " +
                                "AcqDate=@AcqDate,AcqVolume=@AcqVolume,AcqPrice1 = @AcqPrice1,AcqDate1 = @AcqDate1,AcqVolume1 = @AcqVolume1,AcqPrice2 = @AcqPrice2,AcqDate2 = @AcqDate2,AcqVolume2 = @AcqVolume2,AcqPrice3 = @AcqPrice3,AcqDate3 = @AcqDate3,AcqVolume3 = @AcqVolume3,AcqPrice4 = @AcqPrice4,AcqDate4 = @AcqDate4,AcqVolume4 = @AcqVolume4,AcqPrice5 = @AcqPrice5,AcqDate5 = @AcqDate5,AcqVolume5 = @AcqVolume5,AcqPrice6 = @AcqPrice6,AcqDate6 = @AcqDate6,AcqVolume6 = @AcqVolume6,AcqPrice7 = @AcqPrice7,AcqDate7 = @AcqDate7,AcqVolume7 = @AcqVolume7,AcqPrice8 = @AcqPrice8,AcqDate8 = @AcqDate8,AcqVolume8 = @AcqVolume8,AcqPrice9 = @AcqPrice9,AcqDate9 = @AcqDate9,AcqVolume9 = @AcqVolume9, " +
                                "CounterpartPK=@CounterpartPK,SettledDate=@SettledDate,MaturityDate=@MaturityDate,LastCouponDate=@LastCouponDate,NextCouponDate=@NextCouponDate,InterestPercent=@InterestPercent,CompanyAccountTradingPK=@CompanyAccountTradingPK,CashRefPK=@CashRefPK,BrokerageFeePercent=@BrokerageFeePercent,BrokerageFeeAmount=@BrokerageFeeAmount, " +
                                "LevyAmount=@LevyAmount,VATAmount=@VATAmount,KPEIAmount=@KPEIAmount,WHTAmount=@WHTAmount,OTCAmount=@OTCAmount,IncomeTaxSellAmount=@IncomeTaxSellAmount,RealisedAmount=@RealisedAmount,BankPK=@BankPK,BankBranchPK=@BankBranchPK,Category=@Category, " + 
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@LastUpdate " +
                                "where TrxPortfolioPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _trxPortfolio.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                                cmd.Parameters.AddWithValue("@Notes", _trxPortfolio.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _trxPortfolio.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _trxPortfolio.ValueDate);
                                cmd.Parameters.AddWithValue("@Reference", _trxPortfolio.Reference);
                                cmd.Parameters.AddWithValue("@InstrumentTypePK", _trxPortfolio.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                                cmd.Parameters.AddWithValue("@BankPK", _trxPortfolio.BankPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _trxPortfolio.BankBranchPK);
                                cmd.Parameters.AddWithValue("@Price", _trxPortfolio.Price);
                                cmd.Parameters.AddWithValue("@AcqPrice", _trxPortfolio.AcqPrice);
                                cmd.Parameters.AddWithValue("@Lot", _trxPortfolio.Lot);
                                cmd.Parameters.AddWithValue("@LotInShare", _trxPortfolio.LotInShare);
                                cmd.Parameters.AddWithValue("@Volume", _trxPortfolio.Volume);
                                cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                                cmd.Parameters.AddWithValue("@InterestAmount", _trxPortfolio.InterestAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxInterestAmount", _trxPortfolio.IncomeTaxInterestAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxGainAmount", _trxPortfolio.IncomeTaxGainAmount);
                                cmd.Parameters.AddWithValue("@GrossAmount", _trxPortfolio.GrossAmount);
                                cmd.Parameters.AddWithValue("@NetAmount", _trxPortfolio.NetAmount);
                                cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                                cmd.Parameters.AddWithValue("@TrxTypeID", _trxPortfolio.TrxTypeID);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                                cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.SettledDate);
                                cmd.Parameters.AddWithValue("@AcqDate", _trxPortfolio.AcqDate);
                                cmd.Parameters.AddWithValue("@AcqVolume", _trxPortfolio.AcqVolume);
                                cmd.Parameters.AddWithValue("@AcqPrice1", _trxPortfolio.AcqPrice1);
                                cmd.Parameters.AddWithValue("@AcqDate1", _trxPortfolio.AcqDate1);
                                cmd.Parameters.AddWithValue("@AcqVolume1", _trxPortfolio.AcqVolume1);
                                cmd.Parameters.AddWithValue("@AcqPrice2", _trxPortfolio.AcqPrice2);
                                cmd.Parameters.AddWithValue("@AcqDate2", _trxPortfolio.AcqDate2);
                                cmd.Parameters.AddWithValue("@AcqVolume2", _trxPortfolio.AcqVolume2);
                                cmd.Parameters.AddWithValue("@AcqPrice3", _trxPortfolio.AcqPrice3);
                                cmd.Parameters.AddWithValue("@AcqDate3", _trxPortfolio.AcqDate3);
                                cmd.Parameters.AddWithValue("@AcqVolume3", _trxPortfolio.AcqVolume3);
                                cmd.Parameters.AddWithValue("@AcqPrice4", _trxPortfolio.AcqPrice4);
                                cmd.Parameters.AddWithValue("@AcqDate4", _trxPortfolio.AcqDate4);
                                cmd.Parameters.AddWithValue("@AcqVolume4", _trxPortfolio.AcqVolume4);
                                cmd.Parameters.AddWithValue("@AcqPrice5", _trxPortfolio.AcqPrice5);
                                cmd.Parameters.AddWithValue("@AcqDate5", _trxPortfolio.AcqDate5);
                                cmd.Parameters.AddWithValue("@AcqVolume5", _trxPortfolio.AcqVolume5);
                                cmd.Parameters.AddWithValue("@AcqPrice6", _trxPortfolio.AcqPrice6);
                                cmd.Parameters.AddWithValue("@AcqDate6", _trxPortfolio.AcqDate6);
                                cmd.Parameters.AddWithValue("@AcqVolume6", _trxPortfolio.AcqVolume6);
                                cmd.Parameters.AddWithValue("@AcqPrice7", _trxPortfolio.AcqPrice7);
                                cmd.Parameters.AddWithValue("@AcqDate7", _trxPortfolio.AcqDate7);
                                cmd.Parameters.AddWithValue("@AcqVolume7", _trxPortfolio.AcqVolume7);
                                cmd.Parameters.AddWithValue("@AcqPrice8", _trxPortfolio.AcqPrice8);
                                cmd.Parameters.AddWithValue("@AcqDate8", _trxPortfolio.AcqDate8);
                                cmd.Parameters.AddWithValue("@AcqVolume8", _trxPortfolio.AcqVolume8);
                                cmd.Parameters.AddWithValue("@AcqPrice9", _trxPortfolio.AcqPrice9);
                                cmd.Parameters.AddWithValue("@AcqDate9", _trxPortfolio.AcqDate9);
                                cmd.Parameters.AddWithValue("@AcqVolume9", _trxPortfolio.AcqVolume9);
                                cmd.Parameters.AddWithValue("@LastCouponDate", _trxPortfolio.LastCouponDate);
                                cmd.Parameters.AddWithValue("@NextCouponDate", _trxPortfolio.NextCouponDate);
                                cmd.Parameters.AddWithValue("@MaturityDate", _trxPortfolio.MaturityDate);
                                cmd.Parameters.AddWithValue("@InterestPercent", _trxPortfolio.InterestPercent);
                                cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _trxPortfolio.CompanyAccountTradingPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _trxPortfolio.CashRefPK);
                                cmd.Parameters.AddWithValue("@BrokerageFeePercent", _trxPortfolio.BrokerageFeePercent);
                                cmd.Parameters.AddWithValue("@BrokerageFeeAmount", _trxPortfolio.BrokerageFeeAmount);
                                cmd.Parameters.AddWithValue("@LevyAmount", _trxPortfolio.LevyAmount);
                                cmd.Parameters.AddWithValue("@VATAmount", _trxPortfolio.VATAmount);
                                cmd.Parameters.AddWithValue("@KPEIAmount", _trxPortfolio.KPEIAmount);
                                cmd.Parameters.AddWithValue("@WHTAmount", _trxPortfolio.WHTAmount);
                                cmd.Parameters.AddWithValue("@OTCAmount", _trxPortfolio.OTCAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxSellAmount", _trxPortfolio.IncomeTaxSellAmount);
                                cmd.Parameters.AddWithValue("@RealisedAmount", _trxPortfolio.RealisedAmount);
                                cmd.Parameters.AddWithValue("@Category", _trxPortfolio.Category);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _trxPortfolio.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_trxPortfolio.TrxPortfolioPK, "TrxPortfolio");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TrxPortfolio where TrxPortfolioPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _trxPortfolio.HistoryPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _trxPortfolio.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _trxPortfolio.ValueDate);
                                cmd.Parameters.AddWithValue("@Reference", _trxPortfolio.Reference);
                                cmd.Parameters.AddWithValue("@InstrumentTypePK", _trxPortfolio.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                                cmd.Parameters.AddWithValue("@BankPK", _trxPortfolio.BankPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _trxPortfolio.BankBranchPK);
                                cmd.Parameters.AddWithValue("@Price", _trxPortfolio.Price);
                                cmd.Parameters.AddWithValue("@AcqPrice", _trxPortfolio.AcqPrice);
                                cmd.Parameters.AddWithValue("@Lot", _trxPortfolio.Lot);
                                cmd.Parameters.AddWithValue("@LotInShare", _trxPortfolio.LotInShare);
                                cmd.Parameters.AddWithValue("@Volume", _trxPortfolio.Volume);
                                cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                                cmd.Parameters.AddWithValue("@InterestAmount", _trxPortfolio.InterestAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxInterestAmount", _trxPortfolio.IncomeTaxInterestAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxGainAmount", _trxPortfolio.IncomeTaxGainAmount);
                                cmd.Parameters.AddWithValue("@GrossAmount", _trxPortfolio.GrossAmount);
                                cmd.Parameters.AddWithValue("@NetAmount", _trxPortfolio.NetAmount);
                                cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                                cmd.Parameters.AddWithValue("@TrxTypeID", _trxPortfolio.TrxTypeID);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                                cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.SettledDate);
                                cmd.Parameters.AddWithValue("@AcqDate", _trxPortfolio.AcqDate);
                                cmd.Parameters.AddWithValue("@AcqVolume", _trxPortfolio.AcqVolume);
                                cmd.Parameters.AddWithValue("@AcqPrice1", _trxPortfolio.AcqPrice1);
                                cmd.Parameters.AddWithValue("@AcqDate1", _trxPortfolio.AcqDate1);
                                cmd.Parameters.AddWithValue("@AcqVolume1", _trxPortfolio.AcqVolume1);
                                cmd.Parameters.AddWithValue("@AcqPrice2", _trxPortfolio.AcqPrice2);
                                cmd.Parameters.AddWithValue("@AcqDate2", _trxPortfolio.AcqDate2);
                                cmd.Parameters.AddWithValue("@AcqVolume2", _trxPortfolio.AcqVolume2);
                                cmd.Parameters.AddWithValue("@AcqPrice3", _trxPortfolio.AcqPrice3);
                                cmd.Parameters.AddWithValue("@AcqDate3", _trxPortfolio.AcqDate3);
                                cmd.Parameters.AddWithValue("@AcqVolume3", _trxPortfolio.AcqVolume3);
                                cmd.Parameters.AddWithValue("@AcqPrice4", _trxPortfolio.AcqPrice4);
                                cmd.Parameters.AddWithValue("@AcqDate4", _trxPortfolio.AcqDate4);
                                cmd.Parameters.AddWithValue("@AcqVolume4", _trxPortfolio.AcqVolume4);
                                cmd.Parameters.AddWithValue("@AcqPrice5", _trxPortfolio.AcqPrice5);
                                cmd.Parameters.AddWithValue("@AcqDate5", _trxPortfolio.AcqDate5);
                                cmd.Parameters.AddWithValue("@AcqVolume5", _trxPortfolio.AcqVolume5);
                                cmd.Parameters.AddWithValue("@AcqPrice6", _trxPortfolio.AcqPrice6);
                                cmd.Parameters.AddWithValue("@AcqDate6", _trxPortfolio.AcqDate6);
                                cmd.Parameters.AddWithValue("@AcqVolume6", _trxPortfolio.AcqVolume6);
                                cmd.Parameters.AddWithValue("@AcqPrice7", _trxPortfolio.AcqPrice7);
                                cmd.Parameters.AddWithValue("@AcqDate7", _trxPortfolio.AcqDate7);
                                cmd.Parameters.AddWithValue("@AcqVolume7", _trxPortfolio.AcqVolume7);
                                cmd.Parameters.AddWithValue("@AcqPrice8", _trxPortfolio.AcqPrice8);
                                cmd.Parameters.AddWithValue("@AcqDate8", _trxPortfolio.AcqDate8);
                                cmd.Parameters.AddWithValue("@AcqVolume8", _trxPortfolio.AcqVolume8);
                                cmd.Parameters.AddWithValue("@AcqPrice9", _trxPortfolio.AcqPrice9);
                                cmd.Parameters.AddWithValue("@AcqDate9", _trxPortfolio.AcqDate9);
                                cmd.Parameters.AddWithValue("@AcqVolume9", _trxPortfolio.AcqVolume9);
                                cmd.Parameters.AddWithValue("@MaturityDate", _trxPortfolio.MaturityDate);
                                cmd.Parameters.AddWithValue("@LastCouponDate", _trxPortfolio.LastCouponDate);
                                cmd.Parameters.AddWithValue("@NextCouponDate", _trxPortfolio.NextCouponDate);
                                cmd.Parameters.AddWithValue("@InterestPercent", _trxPortfolio.InterestPercent);
                                cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _trxPortfolio.CompanyAccountTradingPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _trxPortfolio.CashRefPK);
                                cmd.Parameters.AddWithValue("@BrokerageFeePercent", _trxPortfolio.BrokerageFeePercent);
                                cmd.Parameters.AddWithValue("@BrokerageFeeAmount", _trxPortfolio.BrokerageFeeAmount);
                                cmd.Parameters.AddWithValue("@LevyAmount", _trxPortfolio.LevyAmount);
                                cmd.Parameters.AddWithValue("@VATAmount", _trxPortfolio.VATAmount);
                                cmd.Parameters.AddWithValue("@KPEIAmount", _trxPortfolio.KPEIAmount);
                                cmd.Parameters.AddWithValue("@WHTAmount", _trxPortfolio.WHTAmount);
                                cmd.Parameters.AddWithValue("@OTCAmount", _trxPortfolio.OTCAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxSellAmount", _trxPortfolio.IncomeTaxSellAmount);
                                cmd.Parameters.AddWithValue("@RealisedAmount", _trxPortfolio.RealisedAmount);
                                cmd.Parameters.AddWithValue("@Category", _trxPortfolio.Category);
                                cmd.Parameters.AddWithValue("@EntryUsersID", _trxPortfolio.EntryUsersID);
                                cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _trxPortfolio.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TrxPortfolio set status= 4,Notes=@Notes, " +
                                    " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime, lastupdate=@lastupdate where TrxPortfolioPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _trxPortfolio.Notes);
                                cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _trxPortfolio.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _trxPortfolio.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        //7
        public void TrxPortfolio_Approved(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxPortfolio set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where TrxPortfolioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _trxPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _trxPortfolio.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxPortfolio set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where TrxPortfolioPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _trxPortfolio.ApprovedUsersID);
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
        public void TrxPortfolio_Reject(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxPortfolio set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where TrxPortfolioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _trxPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _trxPortfolio.VoidUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TrxPortfolio set status= 2,LastUpdate=@LastUpdate where TrxPortfolioPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
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
        public void TrxPortfolio_Void(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TrxPortfolio set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@lastUpdate " +
                            "where TrxPortfolioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _trxPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _trxPortfolio.VoidUsersID);
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


        public void TrxPortfolio_UnApproved(TrxPortfolio _trxPortfolio)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update  TrxPortfolio set status = 1 " +
                            "where  TrxPortfolioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _trxPortfolio.HistoryPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //AZIZ
        public void TrxPortfolio_Posting(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxPortfolio.InstrumentTypePK == 2 && _trxPortfolio.TrxType == 1) // bond buy
                        {

                            cmd.CommandText = "Declare @JournalPK int Declare @SettledDate datetime Declare @AcqDate datetime Declare @ValueDate datetime Declare @Reference nvarchar(50) Declare @PeriodPK int \n " +
                            "Declare @InstrumentID Nvarchar(100) Declare @InstrumentPK int Declare @Currency int Declare @BondType int Declare @Volume numeric(19,4) \n  " +
                            "Declare @Price numeric(19,4) Declare @AcqPrice numeric(19,4) Declare @LastCouponDate datetime Declare @CouponRate numeric(19,6) \n " +
                            "Declare @InterestDue int Declare @InterestPeriod int \n " +
                            "SELECT @AcqDate = isnull(A.AcqDate,0),@SettledDate = A.SettledDate,@ValueDate = A.ValueDate,@PeriodPK = A.PeriodPK,@InstrumentID = B.ID, @Currency = B.CurrencyPK, @BondType = C.Type, @InstrumentPK = A.InstrumentPK, \n " +
                            "@Volume = A.Volume, @Price = A.Price, @AcqPrice = A.AcqPrice, @LastCouponDate = A.LastCouponDate, @CouponRate = A.InterestPercent,@InterestDue = B.InterestDue,@InterestPeriod = B.InterestPeriod  " +
                            "FROM TrxPortfolio A LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK and B.Status = 2 " +
                            "Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2 " +
                            "WHERE TrxPortfolioPK = @TrxPortfolioPK And A.Status = 2 and A.posted = 0  \n Set @CouponRate = round(@CouponRate * @InterestDue / @InterestPeriod / 100,6) \n " +
                            "Select @journalPK = isnull(Max(JournalPK),0) + 1 From Journal \n " +
                            "exec getJournalReference @ValueDate,'ADJ',@Reference out  \n " +
                            _insertJournal +
                            "SELECT @JournalPK,1,2,'Posting From Trx Portfolio (T0)',@PeriodPK, @ValueDate,@TrxPortfolioPK,'Portfolio',@Reference,2,'Buy Bond (T0) ' + @InstrumentID,  " +
                            "1,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow \n " +
                            "Declare @InvInBond int  " +
                            "Declare @BondInterest int  " +
                            "Declare @PPHFinal int  Declare @BondReceivable int \n  " +
                            "Declare @InterestDays int Declare @DivDays int Declare @DefaultDays int Declare @InterestAmount Numeric(19,4) Declare @ActualDays Numeric(18,6) \n " +
                            "set @InterestAmount = 0 set @InterestDays = 0 \n " +
                            "IF @Currency = 1 and @BondType = 5 " +
                            "BEGIN Select @InvInBond = InvInBondGovIDR,@BondInterest = InterestReceivableBondGovIDR,  @PPHFinal = PPHFinal,@BondReceivable = ArSellBond From AccountingSetup Where status = 2  \n " +
                            "Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) \n " +

                            "Set @DivDays =datediff(day,@LastCouponDate,dateadd(month,@InterestDue,@LastCouponDate))  " +
                            "set @ActualDays = @InterestDays * 1.000000 / @DivDays " +
                            "Set @InterestAmount  = @Volume / 1000000 * round( @CouponRate * @InterestDays / @DivDays * 1000000,0) \n END " +
                            "IF @Currency = 2 and @BondType = 5 " +
                            "BEGIN Select @InvInBond = InvInBondGovUSD,@BondInterest = InterestReceivableBondGovUSD,@PPHFinal = PPHFinal,@BondReceivable = ArSellBond  " +
                            "From AccountingSetup Where status = 2 \n " +
                            "Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) " +

                            "Set @DivDays = datediff(day,@LastCouponDate,dateadd(month,@InterestDue,@LastCouponDate)) " +
                            "set @ActualDays = @InterestDays * 1.000000 / @DivDays " +
                            "Set @InterestAmount  = @Volume / 1000000 * round( @CouponRate * @InterestDays / @DivDays * 1000000,0) \n END " +
                                //  "if len(@InterestAmount) - 5 = 8 begin if substring(cast(@InterestAmount as nvarchar(15)),5,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-3) end Else begin Set @InterestAmount = round(@InterestAmount,-4) end end " +
                                //"else if len(@InterestAmount) - 5 = 9 begin if substring(cast(@InterestAmount as nvarchar(15)),6,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-4) end Else begin Set @InterestAmount = round(@InterestAmount,-5) end end " +
                                //"else if len(@InterestAmount) - 5 = 7 begin  if substring(cast(@InterestAmount as nvarchar(15)),4,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-2) end Else begin Set @InterestAmount = round(@InterestAmount,-3) end end " +
                                //"else if len(@InterestAmount) - 5 = 10 begin  if substring(cast(@InterestAmount as nvarchar(15)),7,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-5) end Else begin Set @InterestAmount = round(@InterestAmount,-6) end end " +
                                //"else if len(@InterestAmount) - 5 = 6 begin if substring(cast(@InterestAmount as nvarchar(15)),3,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-1) end Else begin Set @InterestAmount = round(@InterestAmount,-2) end end \n " +
                            "IF @Currency = 1 and @BondType = 2 BEGIN \n Select @InvInBond = InvInBondCorpIDR,@BondInterest = InterestReceivableBondCorpIDR, " +
                            "@PPHFinal = PPHFinal,@BondReceivable = ArSellBond From AccountingSetup Where status = 2 " +
                                //"if Datediff(day,@LastCouponDate,@SettledDate) <= 15 begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)  end else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                            "if Datediff(day,@LastCouponDate,@SettledDate) <= 15 and (@InstrumentID not in ('BNGA01B','ADMF02CCN2','ISAT05B'))  Begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 60 and (@InstrumentID not in ('PPGD12A','BIIF05XXMF','CNAF01XXMF','MAPI01BCN1','PPGD01DCN2','BNII01SBCN2','PPLN11A','IMFI02BCN3','SSMM01B','MDLN01BCN1','BIIF01ACN2','BEXI03CCN1')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 30 and (@InstrumentID in ('ADMF03ACN1','IIFF01B')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end  else if  (@InstrumentID not in ('PPGD12A','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1   end  else if  (@InstrumentID in ('BIIF01ACN1','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','BEXI03CCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)   end else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end  \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                            "Set @InterestAmount  = @Volume * @CouponRate * @interestDays / @DivDays END  \n " +
                            "IF @Currency = 2 and @BondType = 2 BEGIN Select @InvInBond = InvInBondCorpUSD,@BondInterest = InterestReceivableBondCorpUSD, @PPHFinal = PPHFinal,@BondReceivable = ArSellBond From AccountingSetup Where status = 2 \n " +
                                //"if Datediff(day,@LastCouponDate,@SettledDate) <= 15 begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)  end else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                            "if Datediff(day,@LastCouponDate,@SettledDate) <= 15 and (@InstrumentID not in ('BNGA01B','ADMF02CCN2','ISAT05B'))  Begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 60 and (@InstrumentID not in ('PPGD12A','BIIF05XXMF','CNAF01XXMF','MAPI01BCN1','PPGD01DCN2','BNII01SBCN2','PPLN11A','IMFI02BCN3','SSMM01B','MDLN01BCN1','BIIF01ACN2','BEXI03CCN1')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 30 and (@InstrumentID in ('ADMF03ACN1','IIFF01B')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end  else if  (@InstrumentID not in ('PPGD12A','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1   end  else if  (@InstrumentID in ('BIIF01ACN1','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','BEXI03CCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)   end  else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                            "\n Set @InterestAmount  = @Volume * @CouponRate * @interestDays / @DivDays END \n " +
                            "Declare @AutoNo int Declare @Amount numeric(19,4) Set @AutoNo = 1 Set @Amount = @Volume * @Price/100 \n " +
                            _insertJournalDetail +
                            " \n Select @JournalPK,@AutoNo,1,2,@InvInBond,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID,'','D',@Amount,@amount,0,1,@Amount,0,@userID \n " +
                            "Set @AutoNo = @AutoNo + 1 \n " +
                            _insertJournalDetail +
                            " \n Select @JournalPK,@AutoNo,1,2,@BondInterest,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID,'','D',@InterestAmount, " +
                            "@InterestAmount,0,1,@InterestAmount,0,@userID \n " +
                            "Set @AutoNo = @AutoNo + 1 \n " +
                                //"Declare @PPHAmount numeric(18,4) Declare @MarginPrice numeric(18,4) Set @MarginPrice = @Price - @AcqPrice Set @PPHAmount = @MarginPrice / 100 * @Volume * 0.15 \n  " +
                            "Declare @AcqDays int Declare @PPHAmount numeric(18,4) Declare @MarginPrice numeric(18,4) Declare @TaxInterest numeric(18,4) Set @AcqDays = Datediff(day,@AcqDate,@SettledDate) Set @MarginPrice = @Price - @AcqPrice Set @PPHAmount = @MarginPrice / 100 * @Volume * 0.15 \n  if isnull(@AcqDays,0) = 0 begin Set @TaxInterest = 0 end else begin Set @TaxInterest = (( @AcqDays * @CouponRate  / @DivDays * 1000000 * (@volume / 1000000)) *0.15) end   \n " +
                            _insertJournalDetail +
                            " \n Select @JournalPK,@AutoNo,1,2,@PPHFinal,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID,'','C',@PPHAmount,0,@PPHAmount,1,0,@PPHAmount,@userID \n " +
                            "Set @AutoNo = @AutoNo + 1 \n " +
                            _insertJournalDetail +
                            " \n Select @JournalPK,@AutoNo,1,2,@PPHFinal,@Currency,0,0,0,0,@InstrumentPK,0,'Tax Interest Bond ' + @InstrumentID,'','C',@TaxInterest,0,@TaxInterest,1,0,@TaxInterest,@userID \n " +
                            "Set @AutoNo = @AutoNo + 1 \n Declare @ReceivableAmount numeric(18,4) set @ReceivableAmount = @Amount + @InterestAmount - @PPHAmount - @TaxInterest \n " +
                            _insertJournalDetail +
                            " \n Select @JournalPK,@AutoNo,1,2,@BondReceivable,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID + '- Settled Date: ' + cast(@settledDate as nvarchar(12)),'','C',@ReceivableAmount,0,@ReceivableAmount,1,0,@ReceivableAmount,@userID " +
                            " \n  Update TrxPortfolio Set Posted = 1, PostedBy = @UserID, PostedTime = @TimeNow,Notes = 'Posting to No ' + Cast(@journalPK as nvarchar(10)) where TrxPortfolioPK = @TrxPortfolioPK and Posted = 0 ";
                            cmd.Parameters.AddWithValue("@TrxPortfolioPK", _trxPortfolio.TrxPortfolioPK);
                            cmd.Parameters.AddWithValue("@UserID", _trxPortfolio.PostedBy);
                            cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        if (_trxPortfolio.InstrumentTypePK == 2 && _trxPortfolio.TrxType == 2) // Bond Sell
                        {
                            cmd.CommandText = "Declare @JournalPK int Declare @SettledDate datetime Declare @ValueDate datetime Declare @Reference nvarchar(50) Declare @PeriodPK int \n " +
                               "Declare @InstrumentID Nvarchar(100) Declare @InstrumentPK int Declare @Currency int Declare @BondType int Declare @Volume numeric(19,4) \n  " +
                               "Declare @Price numeric(19,4) Declare @AcqPrice numeric(19,4) Declare @LastCouponDate datetime Declare @CouponRate numeric(19,6) \n " +
                               "Declare @InterestDue int Declare @InterestPeriod int \n " +
                               "SELECT @SettledDate = A.SettledDate,@ValueDate = A.ValueDate,@PeriodPK = A.PeriodPK,@InstrumentID = B.ID, @Currency = B.CurrencyPK, @BondType = C.Type, @InstrumentPK = A.InstrumentPK, \n " +
                               "@Volume = A.Volume, @Price = A.Price, @AcqPrice = A.AcqPrice, @LastCouponDate = A.LastCouponDate, @CouponRate = A.InterestPercent,@InterestDue = B.InterestDue,@InterestPeriod = B.InterestPeriod  " +
                               "FROM TrxPortfolio A LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK and B.Status = 2 " +
                               "Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2 " +
                               "WHERE TrxPortfolioPK = @TrxPortfolioPK And A.Status = 2 and A.posted = 0  \n Set @CouponRate = round(@CouponRate * @InterestDue / @InterestPeriod / 100,6) \n " +
                               "Select @journalPK = isnull(Max(JournalPK),0) + 1 From Journal \n " +
                               "exec getJournalReference @ValueDate,'ADJ',@Reference out  \n " +
                               _insertJournal +
                               "SELECT @JournalPK,1,2,'Posting From Trx Portfolio (T0)',@PeriodPK, @ValueDate,@TrxPortfolioPK,'Portfolio',@Reference,2,'Sell Bond (T0) ' + @InstrumentID,  " +
                               "1,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow \n " +
                               "Declare @InvInBond int  " +
                               "Declare @BondInterest int  " +
                               "Declare @PPHFinal int  Declare @BondReceivable int Declare @RealisedGain int \n  " +
                               "Declare @InterestDays int Declare @DivDays int Declare @DefaultDays int Declare @InterestAmount Numeric(19,4) Declare @ActualDays Numeric(18,6) \n " +
                               "set @InterestAmount = 0 set @InterestDays = 0 \n " +
                               "IF @Currency = 1 and @BondType = 5 " +
                               "BEGIN Select @InvInBond = InvInBondGovIDR,@BondInterest = InterestReceivableBondGovIDR,  @PPHFinal = PPHFinal,@BondReceivable = ArSellBond , @RealisedGain = realisedBondGovIDR From AccountingSetup Where status = 2  \n " +
                               "Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) \n " +

                               "Set @DivDays = datediff(day,@LastCouponDate,dateadd(month,@InterestDue,@LastCouponDate))  " +
                               "set @ActualDays = @InterestDays * 1.000000 / @DivDays " +
                               "Set @InterestAmount  = @Volume / 1000000 * round( @CouponRate * @InterestDays / @DivDays * 1000000,0) \n END " +
                               "IF @Currency = 2 and @BondType = 5 " +
                               "BEGIN Select @InvInBond = InvInBondGovUSD,@BondInterest = InterestReceivableBondGovUSD,@PPHFinal = PPHFinal,@BondReceivable = ArSellBond ,@RealisedGain = realisedBondGovUSD  " +
                               "From AccountingSetup Where status = 2 \n " +
                               "Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) " +

                               "Set @DivDays = datediff(day,@LastCouponDate,dateadd(month,@InterestDue,@LastCouponDate))  " +
                               "set @ActualDays = @InterestDays * 1.0000 / @DivDays " +
                               "Set @InterestAmount  = @Volume / 1000000 * round( @CouponRate * @InterestDays / @DivDays * 1000000,0) \n END " +
                                //    "if len(@InterestAmount) - 5 = 8 begin if substring(cast(@InterestAmount as nvarchar(15)),5,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-3) end Else begin Set @InterestAmount = round(@InterestAmount,-4) end end " +
                                //"else if len(@InterestAmount) - 5 = 9 begin if substring(cast(@InterestAmount as nvarchar(15)),6,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-4) end Else begin Set @InterestAmount = round(@InterestAmount,-5) end end " +
                                //"else if len(@InterestAmount) - 5 = 7 begin  if substring(cast(@InterestAmount as nvarchar(15)),4,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-2) end Else begin Set @InterestAmount = round(@InterestAmount,-3) end end " +
                                //"else if len(@InterestAmount) - 5 = 10 begin  if substring(cast(@InterestAmount as nvarchar(15)),7,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-5) end Else begin Set @InterestAmount = round(@InterestAmount,-6) end end " +
                                //"else if len(@InterestAmount) - 5 = 6 begin if substring(cast(@InterestAmount as nvarchar(15)),3,1)  = 5 begin Set @InterestAmount = round(@InterestAmount,-1) end Else begin Set @InterestAmount = round(@InterestAmount,-2) end end \n " +
                               "IF @Currency = 1 and @BondType = 2 BEGIN \n Select @InvInBond = InvInBondCorpIDR,@BondInterest = InterestReceivableBondCorpIDR, " +
                               "@PPHFinal = PPHFinal,@BondReceivable = ArSellBond , @RealisedGain = realisedBondCorpIDR From AccountingSetup Where status = 2 " +
                               "if Datediff(day,@LastCouponDate,@SettledDate) <= 15 and (@InstrumentID not in ('BNGA01B','ADMF02CCN2','ISAT05B'))  Begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 60 and (@InstrumentID not in ('PPGD12A','BIIF05XXMF','CNAF01XXMF','MAPI01BCN1','PPGD01DCN2','BNII01SBCN2','PPLN11A','IMFI02BCN3','SSMM01B','MDLN01BCN1','BIIF01ACN2','BEXI03CCN1')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 30 and (@InstrumentID in ('ADMF03ACN1','IIFF01B')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end  else if  (@InstrumentID not in ('PPGD12A','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1   end  else if  (@InstrumentID in ('BIIF01ACN1','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','BEXI03CCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)   end else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                               "if (@InstrumentID = 'PPGD12A') begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end  " +
                               "Set @InterestAmount  = @Volume * @CouponRate * @interestDays / @DivDays END  \n " +
                               "IF @Currency = 2 and @BondType = 2 BEGIN Select @InvInBond = InvInBondCorpUSD,@BondInterest = InterestReceivableBondCorpUSD, @PPHFinal = PPHFinal,@BondReceivable = ArSellBond, @RealisedGain = realisedBondCorpUSD From AccountingSetup Where status = 2 \n " +
                               "if Datediff(day,@LastCouponDate,@SettledDate) <= 15 and (@InstrumentID not in ('BNGA01B','ADMF02CCN2','ISAT05B'))  Begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 60 and (@InstrumentID not in ('PPGD12A','BIIF05XXMF','CNAF01XXMF','MAPI01BCN1','PPGD01DCN2','BNII01SBCN2','PPLN11A','IMFI02BCN3','SSMM01B','MDLN01BCN1','BIIF01ACN2','BEXI03CCN1')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end else if  Datediff(day,@LastCouponDate,@SettledDate) >= 30 and (@InstrumentID in ('ADMF03ACN1','IIFF01B')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 2 end  else if  (@InstrumentID not in ('PPGD12A','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1   end  else if  (@InstrumentID in ('BIIF01ACN1','BTPN02ACN01','PPGD01DCN2','SIAISA01','BTPN02ACN1','BBRI01CCN2','SSMM01B','ISAT01BCN2','ISAT01ACN1','PNBN02CN1','PIGN01B','BSDE01CN2','MAPI01BCN1','APIA01A','IMFI02BCN3','BNLI01BCN1','BEXI03CCN1','JPFA01CN2')) begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate)   end else  begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end \n Set @DefaultDays = 360 * @InterestPeriod / 12 \n Set @DivDays = @DefaultDays * @interestDue / @InterestPeriod " +
                               "if (@InstrumentID = 'PPGD12A') begin Set @InterestDays = Datediff(day,@LastCouponDate,@SettledDate) - 1 end  " +
                               "\n Set @InterestAmount  = @Volume * @CouponRate * @interestDays / @DivDays END \n " +
                               "Declare @AutoNo int Declare @Amount numeric(19,4) Declare @AcqAmount numeric(19,4) Set @AutoNo = 1 Set @Amount = @Volume * @Price/100 set @AcqAmount = @Volume * @AcqPrice/100 \n " +
                               _insertJournalDetail +
                               " \n Select @JournalPK,@AutoNo,1,2,@InvInBond,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID,'','C',@AcqAmount,0,@AcqAmount,1,0,@AcqAmount,@userID \n " +
                               "Set @AutoNo = @AutoNo + 1 \n " +
                               _insertJournalDetail +
                               " \n Select @JournalPK,@AutoNo,1,2,@BondInterest,@Currency,0,0,0,0,@InstrumentPK,0,'Interest Bonds ' + @InstrumentID,'','C',@InterestAmount,0,@InterestAmount,1,0,@InterestAmount,@userID \n " +
                               "Set @AutoNo = @AutoNo + 1 \n " +
                               "Declare @PPHAmount numeric(18,4) Declare @MarginPrice numeric(18,4) Set @MarginPrice = @Price - @AcqPrice Set @PPHAmount = @MarginPrice / 100 * @Volume * 0.15 \n  " +
                                "\n Declare @ReceivableAmount numeric(18,4) set @ReceivableAmount = @Amount + @InterestAmount - @PPHAmount \n " +
                               _insertJournalDetail +
                               " \n Select @JournalPK,@AutoNo,1,2,@BondReceivable,@Currency,0,0,0,0,@InstrumentPK,0,'Bonds ' + @InstrumentID + '- Settled Date: ' + cast(@settledDate as nvarchar(12)),'','D',@ReceivableAmount,@ReceivableAmount,0,1,@ReceivableAmount,0,@userID \n " +
                               " \n Declare @RealisedAmount numeric(18,4) \n" +
                               " declare @AfterAllocateAmount numeric(18,4) Declare @CounterAmount numeric(18,4) Declare @Count Int Declare @Inc int set @Inc = 0 set @CounterAmount = 0 set @RealisedAmount = @ReceivableAmount - @InterestAmount - @AcqAmount \n  " +
                               " Declare @AllocDepartmentPK int Declare @AllocPercent numeric(18,8) Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup where AccountPK =  @RealisedGain and status = 2 \n " +
                               " if @Count = 0 Begin	Set @AutoNo = @AutoNo + 1 \n " +
                               _insertJournalDetail +
                               " Select @JournalPK,@AutoNo,1,2,@RealisedGain,@Currency,0,0,0,0,@InstrumentPK,0,'Capital Gain Bonds ' + @InstrumentID,'','C',@RealisedAmount,0,@RealisedAmount,1,0,@RealisedAmount,@userID \n END " +
                               " \n Else Begin \n " +
                               " Declare A Cursor For Select departmentPK,AllocationPercentage Item From AccountAllocateByCostCenterSetup where AccountPK =  @RealisedGain and status = 2 Open A Fetch Next From A Into @AllocDepartmentPk,@AllocPercent " +
                               " While @@Fetch_Status  = 0 Begin \n " +
                               " Set @AutoNo = @AutoNo + 1 set @AfterAllocateAmount =  @RealisedAmount * @AllocPercent / 100 Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount \n " +
                               _insertJournalDetail +
                               " Select @JournalPK,@AutoNo,1,2,@RealisedGain,@Currency,0,@AllocDepartmentPK,0,0,@InstrumentPK,0,'Capital Gain Bonds ' + @InstrumentID,'','C',@AfterAllocateAmount,0,@AfterAllocateAmount,1,0,@AfterAllocateAmount,@userID \n " +
                               " set @AfterAllocateAmount = 0 " +
                               " set @Inc = @Inc + 1 \n IF @Inc = @count Begin Declare @RoundAmount numeric(19,4) Set @RoundAmount = @CounterAmount - @RealisedAmount if  @RoundAmount <> 0 Begin 	Declare @RoundingDepartmentPK int select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  where AccountPK = @RealisedGain and status = 2 and BitRounding = 1 \n  " +
                               " Declare @LastAmount Numeric(19,4) Declare @FinalAmountAfterRounding numeric(19,4) \n Select @LastAmount =  Amount From JournalDetail where JournalPK = @JournalPK and AccountPK = @RealisedGain and DepartmentPK = @RoundingDepartmentPK and status = 2 \n " +
                               " set @FinalAmountAfterRounding = @lastAmount +  @RoundAmount Update JournalDetail Set Amount = @FinalAmountAfterRounding,Credit = @FinalAmountAfterRounding,baseCredit = @FinalAmountAfterRounding * CurrencyRate where JournalPK = @JournalPK and AccountPK = @RealisedGain and DepartmentPK = @RoundingDepartmentPK and status = 2 \n " +
                               " end end \n FETCH NEXT FROM A INTO  @AllocDepartmentPk,@AllocPercent End Close A DEALLOCATE  A END " +
                               " \n  Update TrxPortfolio Set Posted = 1, PostedBy = @UserID, PostedTime = @TimeNow,Notes = 'Posting to No ' + Cast(@journalPK as nvarchar(10)) where TrxPortfolioPK = @TrxPortfolioPK and posted = 0 ";
                            cmd.Parameters.AddWithValue("@TrxPortfolioPK", _trxPortfolio.TrxPortfolioPK);
                            cmd.Parameters.AddWithValue("@UserID", _trxPortfolio.PostedBy);
                            cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void TrxPortfolio_Revised(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                        @"
 
                        Declare @Reference nvarchar(50)
                        select @Reference = Reference From Journal WHERE TrxName ='TRANSACTION' and TrxNo in  
                        (  
                        	Select TrxPortfolioPK From TrxPortfolio where TrxPortfolioPK = @TrxPortfolioPK  
                        ) 

                        UPDATE Journal SET status = 3, Notes = 'Void By TrxPortfolio Revise',  
                        VoidUsersID = @UserID,VoidTime = @DatetimeNow  
                        WHERE TrxName ='TRANSACTION' and TrxNo in  
                        (  
                        	Select TrxPortfolioPK From TrxPortfolio where TrxPortfolioPK = @TrxPortfolioPK  
                        )  

                        UPDATE Journal SET status = 3, Notes = 'Void By TrxPortfolio Revise',  
                        VoidUsersID = @UserID,VoidTime = @DatetimeNow  
                        WHERE TrxName in ('CP','CR') and TrxNo in  
                        (  
                        	Select CashierPK from cashier where TrxNo = @TrxPortfolioPK  
                            and isnull(JournalNo,0) > 0
                        )  

                        UPDATE Cashier SET status = 3, Notes = 'Void By TrxPortfolio Revise',
                        VoidUsersID = @UserID,VoidTime = @DatetimeNow  where TrxNo in  
                        (  
                        	Select TrxPortfolioPK From TrxPortfolio where TrxPortfolioPK = @TrxPortfolioPK  
                        )  

                        Declare @MaxTrxPortfolioPK int  

                        Select @MaxTrxPortfolioPK = ISNULL(MAX(TrxPortfolioPK),0) + 1 From TrxPortfolio 
                                 INSERT INTO [dbo].[TrxPortfolio] 
                        					    ([TrxPortfolioPK] 
                        					    ,[HistoryPK] 
                        					    ,[Status] 
                        					    ,[Notes] 
                                                ,[ValueDate] 
                                                ,[Reference]
                                                ,[InstrumentTypePK]
                                                ,[InstrumentPK]
                                                ,[BankPK]
                                                ,[BankBranchPK]
                                                ,[Price]
                                                ,[Lot]
                                                ,[LotInShare]
                                                ,[Volume]
                                                ,[Amount]
                                                ,[InterestAmount]
                                                ,[IncomeTaxInterestAmount]
                                                ,[IncomeTaxGainAmount]
                                                ,[LevyAmount]
                                                ,[VATAmount]
                                                ,[KPEIAmount]
                                                ,[WHTAmount]
                                                ,[OTCAmount]
                                                ,[IncomeTaxSellAmount]
                                                ,[RealisedAmount]
                                                ,[GrossAmount]
                                                ,[NetAmount]
                                                ,[TrxType]
                                                ,[TrxTypeID]
                                                ,[CounterpartPK]
                                                ,[AcqPrice]
                                                ,[AcqDate] 
                                                ,[AcqVolume]
                                                ,[AcqVolume1]
                                                ,[AcqPrice1]
                                                ,[AcqDate1] 
                                                ,[AcqVolume2]
                                                ,[AcqPrice2]
                                                ,[AcqDate2] 
                                                ,[AcqVolume3]
                                                ,[AcqPrice3]
                                                ,[AcqDate3] 
                                                ,[AcqVolume4]
                                                ,[AcqPrice4]
                                                ,[AcqDate4] 
                                                ,[AcqVolume5]
                                                ,[AcqPrice5]
                                                ,[AcqDate5] 
                                                ,[AcqVolume6]
                                                ,[AcqPrice6]
                                                ,[AcqDate6] 
                                                ,[AcqVolume7]
                                                ,[AcqPrice7]
                                                ,[AcqDate7] 
                                                ,[AcqVolume8]
                                                ,[AcqPrice8]
                                                ,[AcqDate8] 
                                                ,[AcqVolume9]
                                                ,[AcqPrice9]
                                                ,[AcqDate9] 
                                                ,[SettledDate] 
                                                ,[LastCouponDate] 
                                                ,[NextCouponDate] 
                                                ,[MaturityDate] 
                                                ,[InterestPercent]
                                                ,[CompanyAccountTradingPK]
                                                ,[CashRefPK]
                                                ,[PeriodPK]
                                                ,[BrokerageFeePercent]
                                                ,[BrokerageFeeAmount] 
                        					    ,[EntryUsersID]  
                        					    ,[EntryTime])  
                        			SELECT	   @MaxTrxPortfolioPK,1,1,'Pending Revised'  
                        					    ,[ValueDate] 
                                                ,[Reference]
                                                ,[InstrumentTypePK]
                                                ,[InstrumentPK]
                                                ,[BankPK]
                                                ,[BankBranchPK]
                                                ,[Price]
                                                ,[Lot]
                                                ,[LotInShare]
                                                ,[Volume]
                                                ,[Amount]
                                                ,[InterestAmount]
                                                ,[IncomeTaxInterestAmount]
                                                ,[IncomeTaxGainAmount]
                                                ,[LevyAmount]
                                                ,[VATAmount]
                                                ,[KPEIAmount]
                                                ,[WHTAmount]
                                                ,[OTCAmount]
                                                ,[IncomeTaxSellAmount]
                                                ,[RealisedAmount]
                                                ,[GrossAmount]
                                                ,[NetAmount]
                                                ,[TrxType]
                                                ,[TrxTypeID]
                                                ,[CounterpartPK]
                                                ,[AcqPrice]
                                                ,[AcqDate] 
                                                ,[AcqVolume]
                                                ,[AcqVolume1]
                                                ,[AcqPrice1]
                                                ,[AcqDate1] 
                                                ,[AcqVolume2]
                                                ,[AcqPrice2]
                                                ,[AcqDate2] 
                                                ,[AcqVolume3]
                                                ,[AcqPrice3]
                                                ,[AcqDate3] 
                                                ,[AcqVolume4]
                                                ,[AcqPrice4]
                                                ,[AcqDate4] 
                                                ,[AcqVolume5]
                                                ,[AcqPrice5]
                                                ,[AcqDate5] 
                                                ,[AcqVolume6]
                                                ,[AcqPrice6]
                                                ,[AcqDate6] 
                                                ,[AcqVolume7]
                                                ,[AcqPrice7]
                                                ,[AcqDate7] 
                                                ,[AcqVolume8]
                                                ,[AcqPrice8]
                                                ,[AcqDate8] 
                                                ,[AcqVolume9]
                                                ,[AcqPrice9]
                                                ,[AcqDate9] 
                                                ,[SettledDate] 
                                                ,[LastCouponDate] 
                                                ,[NextCouponDate] 
                                                ,[MaturityDate] 
                                                ,[InterestPercent]
                                                ,[CompanyAccountTradingPK]
                                                ,[CashRefPK]
                                                ,[PeriodPK]
                                                ,[BrokerageFeePercent]
                                                ,[BrokerageFeeAmount] 
                        					    ,@UserID  
                        					    ,GetDate()  
                        			FROM TrxPortfolio   
                        			WHERE TrxPortfolioPK = @TrxPortfolioPK and status = 2  

                        UPDATE TrxPortfolio Set Status = 3,Revised = 1, RevisedBy = @UserID,Notes = 'Revise From TrxPortfolio',    
                        RevisedTime = @DatetimeNow     
                        WHERE TrxPortfolioPK = @TrxPortfolioPK and status = 2 and Posted = 1 and revised = 0  
                        ";

                        cmd.Parameters.AddWithValue("@TrxPortfolioPK", _trxPortfolio.TrxPortfolioPK);
                        cmd.Parameters.AddWithValue("@UserID", _trxPortfolio.RevisedBy);
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

        // 10 AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<TrxPortfolio> TrxPortfolio_SelectTrxPortfolioDateFromToByInstrumentType(int _status, DateTime _dateFrom, DateTime _dateTo, int _instrumentType)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxPortfolio> L_TrxPortfolio = new List<TrxPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select case when TP.status=1 then 'PENDING' else Case When TP.status = 2 then 'APPROVED' else Case when TP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefID,P.ID PeriodID,
                             case when TP.InstrumentTypePK = 1 then 'Equity' else case when TP.InstrumentTypePK = 2 then 'Bond' else case when TP.InstrumentTypePK = 3 then 'Deposito' else 'Reksadana' end end end InstrumentTypeID,I.ID InstrumentID,I.Name InstrumentName,CP.ID CounterpartID,  
                             CP.Name CounterpartName,CAT.ID CompanyAccountTradingID, CAT.Name CompanyAccountTradingName,  
                             TP.TrxTypeID, B.ID BankID,B.Name BankName,BR.ID BankBranchID,
                             TP.* from TrxPortfolio TP left join
                             Bank B on TP.BankPK = B.BankPK and B.Status = 2  left join 
                             BankBranch BR on TP.BankBranchPK = BR.BankBranchPK and BR.Status = 2  left join  
                             Period P on TP.PeriodPK = P.PeriodPK and P.Status = 2 left join     
                             Instrument I on TP.InstrumentPK = I.InstrumentPK and I.Status = 2 left join     
                             Counterpart CP on TP.CounterpartPK = CP.CounterpartPK and CP.Status = 2 left join     
                             CompanyAccountTrading CAT on TP.CompanyAccountTradingPK = CAT.CompanyAccountTradingPK and CAT.Status = 2  left join 
                             CashRef CR on TP.CashRefPK = CR.CashRefPK and CR.Status = 2        
                             where  TP.status = @status and TP.ValueDate  between @DateFrom and @DateTo and TP.InstrumentTypePK = @InstrumentTypePK ";
                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrumentType);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxPortfolio.Add(setTrxPortfolio(dr));
                                }
                            }
                            return L_TrxPortfolio;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<TrxPortfolio> TrxPortfolio_SelectTrxPortfolioDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrxPortfolio> L_TrxPortfolio = new List<TrxPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select case when TP.status=1 then 'PENDING' else Case When TP.status = 2 then 'APPROVED' else Case when TP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefID,P.ID PeriodID,
                             case when TP.InstrumentTypePK = 1 then 'Equity' else case when TP.InstrumentTypePK = 2 then 'Bond' else case when TP.InstrumentTypePK = 3 then 'Deposito' else 'Reksadana' end end end InstrumentTypeID,I.ID InstrumentID,I.Name InstrumentName,CP.ID CounterpartID,  
                             CP.Name CounterpartName,CAT.ID CompanyAccountTradingID, CAT.Name CompanyAccountTradingName,  
                             TP.TrxTypeID, B.ID BankID,B.Name BankName,BR.ID BankBranchID,
                             TP.* from TrxPortfolio TP left join
                             Bank B on TP.BankPK = B.BankPK and B.Status = 2  left join 
                             BankBranch BR on TP.BankBranchPK = BR.BankBranchPK and BR.Status = 2  left join 
                             Period P on TP.PeriodPK = P.PeriodPK and P.Status = 2 left join     
                             Instrument I on TP.InstrumentPK = I.InstrumentPK and I.Status = 2 left join     
                             Counterpart CP on TP.CounterpartPK = CP.CounterpartPK and CP.Status = 2 left join     
                             CompanyAccountTrading CAT on TP.CompanyAccountTradingPK = CAT.CompanyAccountTradingPK and CAT.Status = 2  left join 
                             CashRef CR on TP.CashRefPK = CR.CashRefPK and CR.Status = 2
                             where  TP.status = @status and TP.ValueDate  between @DateFrom and @DateTo ";

                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TrxPortfolio.Add(setTrxPortfolio(dr));
                                }
                            }
                            return L_TrxPortfolio;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //public TrxPortfolio TrxPortfolio_SelectByTrxPortfolioID(string _trxPortfolioID)
        //{

        //    try
        //    {
        //        DbCon.OpenConnection();
        //        using (SqlCommand cmd = DbCon.Con.CreateCommand())
        //        {


        //            cmd.CommandText = "Select * From TrxPortfolio " +
        //                "Where ID= @TrxPortfolioID and status=2";
        //            cmd.Parameters.AddWithValue("@TrxPortfolioID", _trxPortfolioID);


        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                if (dr.HasRows)
        //                {
        //                    dr.Read();
        //                    return setTrxPortfolio(dr);
        //                }
        //                return null;
        //            }
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //    finally
        //    {
        //        DbCon.CloseConnection();
        //    }
        //}





        public Boolean TrxPortfolioReport(string _userID, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        " select TrxPortfolioPK,Convert(Varchar(12), T.SettledDate, 101)SettledDate,Convert(Varchar(12), ValueDate, 101)ValueDate,Reference,I.ID InstrumentID,I.InterestPercent Rate,TrxTypeID, " +
                        " Price,AcqPrice,Volume,Amount,Convert(Varchar(12), T.LastCouponDate, 101) LastCouponDate,case when TrxType = 1 then [dbo].[FGetOtherBuyBond](TrxPortfolioPK) else [dbo].[FGetOtherSellBond](TrxPortfolioPK) end SettleAmount from TrxPortfolio T " +
                        " left join Instrument I on T.InstrumentPK = I.InstrumentPK " +
                        " where T.Status = 2 and ValueDate between @DateFrom and @DateTo  " +
                        " order by T.SettledDate ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "TrxPortfolioReport" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "TrxPortfolioReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Trx Portfolio Report");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<TrxPortfolioReport> rList = new List<TrxPortfolioReport>();
                                    while (dr0.Read())
                                    {
                                        TrxPortfolioReport rSingle = new TrxPortfolioReport();
                                        rSingle.TrxPortfolioPK = Convert.ToInt32(dr0["TrxPortfolioPK"]);
                                        rSingle.SettledDate = Convert.ToDateTime(dr0["SettledDate"]);
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.Rate = Convert.ToDecimal(dr0["Rate"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.Price = Convert.ToDecimal(dr0["Price"]);
                                        rSingle.AcqPrice = Convert.ToDecimal(dr0["AcqPrice"]);
                                        rSingle.Volume = Convert.ToDecimal(dr0["Volume"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                        rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                        rSingle.SettleAmount = Convert.ToDecimal(dr0["SettleAmount"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.SettledDate ascending
                                                 group r by new { r.SettledDate, r.Reference }
                                                     into rGroup
                                                     select rGroup;

                                    int incRowExcel = 1;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        //worksheet.Cells[1, 1].Value = "No.";
                                        //worksheet.Cells[1, 2].Value = "Settled Date";
                                        //worksheet.Cells[1, 3].Value = "Value Date";
                                        //worksheet.Cells[1, 4].Value = "Reference";
                                        //worksheet.Cells[1, 5].Value = "Instrument ID";
                                        //worksheet.Cells[1, 6].Value = "Rate";
                                        //worksheet.Cells[1, 7].Value = "B / S";
                                        //worksheet.Cells[1, 8].Value = "Price";
                                        //worksheet.Cells[1, 9].Value = "Acq Price";
                                        //worksheet.Cells[1, 10].Value = "Volume";
                                        //worksheet.Cells[1, 11].Value = "Amount";
                                        //worksheet.Cells[1, 12].Value = "Last Coupon Date";
                                        //worksheet.Cells[1, 13].Value = "Settle Amount";
                                        string _range = "A" + incRowExcel + ":M" + incRowExcel;

                                        //using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        //{
                                        //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        //    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                        //    r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                        //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        //    r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                        //    r.Style.Font.Size = 11;
                                        //    r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        //    r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                        //    r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        //}

                                        incRowExcel++;


                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[1, 1].Value = "TrxPortfolio No.";
                                            worksheet.Cells[1, 2].Value = "Settled Date";
                                            worksheet.Cells[1, 3].Value = "Value Date";
                                            worksheet.Cells[1, 4].Value = "Reference";
                                            worksheet.Cells[1, 5].Value = "Instrument ID";
                                            worksheet.Cells[1, 6].Value = "Rate";
                                            worksheet.Cells[1, 7].Value = "B / S";
                                            worksheet.Cells[1, 8].Value = "Price";
                                            worksheet.Cells[1, 9].Value = "Acq Price";
                                            worksheet.Cells[1, 10].Value = "Volume";
                                            worksheet.Cells[1, 11].Value = "Amount";
                                            worksheet.Cells[1, 12].Value = "Last Coupon Date";
                                            worksheet.Cells[1, 13].Value = "Settle Amount";


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.TrxPortfolioPK;
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.SettledDate).ToShortDateString();
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.ValueDate).ToShortDateString();
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Reference;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Rate;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.TrxTypeID;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.Price;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.AcqPrice;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Volume;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 12].Value = Convert.ToDateTime(rsDetail.LastCouponDate).ToShortDateString();
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.SettleAmount;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                            _endRowDetail = incRowExcel;








                                        }


                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:M";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 13];
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(14).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.Column(11).AutoFit();
                                    worksheet.Column(12).AutoFit();
                                    worksheet.Column(13).AutoFit();
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

                                    worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }

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


        public TrxPortfolioForNetAmount Get_NetAmount(TrxPortfolioForNetAmount _trxPortfolio)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    TrxPortfolioForNetAmount M_TrxPortfolio = new TrxPortfolioForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_trxPortfolio.InstrumentTypePK == 2)
                        {
                            cmd.CommandText = @"
                            Declare @TaxExpensePercent numeric(8,4)

                            Declare @Days int
                            Declare @DivDays int
                            Declare @InterestDays int

                            Declare @CurrencyID nvarchar(20)
                            Declare @CouponRate numeric(8,4)
                            Declare @InterestType int
                            Declare @InterestDaysType int
                            Declare @InterestPaymentType int


                            Select 
                            @TaxExpensePercent = A.TaxExpensePercent,
                            @CurrencyID = C.ID,@CouponRate = A.InterestPercent,@InterestType = A.InterestType, 
                            @InterestDaysType = A.InterestDaysType,@InterestPaymentType = 12/D.Priority
                            From Instrument A
                            left join Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2
                            left join MasterValue D on A.InterestPaymentType = D.Code and D.Status = 2 and D.ID = 'InterestPaymentType'
                            where InstrumentPK = @InstrumentPK and A.status = 2


                            Declare @AccuredInterestAmount numeric(22,4)
                            Declare @GrossAmount numeric(22,4)
                            Declare @NetAmount numeric(22,4)
                            Declare @ValuePerUnit int

                            Declare		@DaysAcq_2					int,
			                            @DaysAcq1_2					int,
			                            @DaysAcq2_2					int,
			                            @DaysAcq3_2					int,
			                            @DaysAcq4_2					int,
			                            @DaysAcq5_2					int,
			                            @CapGainAcq					decimal(22,4),
			                            @CapGainAcq1				decimal(22,4),
			                            @CapGainAcq2				decimal(22,4),
			                            @CapGainAcq3				decimal(22,4),
			                            @CapGainAcq4				decimal(22,4),
			                            @CapGainAcq5				decimal(22,4),
			                            @TaxCapGainAcq				decimal(22,4),
			                            @TaxCapGainAcq1				decimal(22,4),
			                            @TaxCapGainAcq2				decimal(22,4),
			                            @TaxCapGainAcq3				decimal(22,4),
			                            @TaxCapGainAcq4				decimal(22,4),
			                            @TaxCapGainAcq5				decimal(22,4),
			                            @AIAcq					decimal(22,4),
			                            @AIAcq1					decimal(22,4),
			                            @AIAcq2					decimal(22,4),
			                            @AIAcq3					decimal(22,4),
			                            @AIAcq4					decimal(22,4),
			                            @AIAcq5					decimal(22,4),
			                            @TaxAIAcq				decimal(22,4),
			                            @TaxAIAcq1				decimal(22,4),
			                            @TaxAIAcq2				decimal(22,4),
			                            @TaxAIAcq3				decimal(22,4),
			                            @TaxAIAcq4				decimal(22,4),
			                            @TaxAIAcq5				decimal(22,4),
			                            @TotalCapGain			decimal(22,4),
			                            @TotalAI				decimal(22,4),
			                            @TotalTaxCapGain		decimal(22,4),
			                            @TotalTaxAI				decimal(22,4)

                            if @CurrencyID = 'IDR'
                            begin	
	                            if @InterestType = 3 --ZERO COUPONT
	                            BEGIN
		                            set @AccuredInterestAmount = 0
		                            set @GrossAmount = @Volume * @price/100
	
		                            if @InterestDaysType in (2,3,4) -- [Govt Bond] <-> ACT/ACT , ACT/360 (3), ACT/365
		                            begin
			                            set @ValuePerUnit = 1000000
			                            set @DivDays = 0

			                            set @Days = 0

			                            set @InterestDays	= case when @InterestDaysType in (3) 
			                            then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate, @SettledDate)) else abs(datediff(day, @SettledDate, @AcqDate)) end -- pembagi hari

			                            set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * case when isnull(@AcqVolume, 0) <> 0 then @AcqVolume else @Volume end / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice) * case when isnull(@AcqVolume, 0) <> 0 then @AcqVolume else @Volume end / 100)  else 0 end

			                            set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end

			                            set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) 
			                            when @TrxType = 1 then((@Price - @AcqPrice2) * @AcqVolume2 / 100) else 0 end

			                            set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) 
			                            when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100) else 0 end

			                            set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) else 0 end

			                            set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) else 0 end

			                            set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100

		                            end
	
		                            if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
		                            begin
		
			                            set @ValuePerUnit = 1
			                            set @DivDays = 0
			
			                            set @Days = 0
		
			                            set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,					@SettledDate)) end -- pembagi hari

			                            set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
			                            set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
			                            set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
			                            set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
			                            set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
			                            set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end

			                            set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
		
		                            end
	
	                            END
	                            ELSE
	                            BEGIN
		                            if @InterestDaysType in (2,3,4) -- [Govt Bond] <-> ACT/ACT , ACT/360 (3), ACT/365
		                            begin
			                            set @ValuePerUnit = 1000000
			                            set @DivDays = case when @InterestDaysType in (3) 
			                            then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @NextCouponDate)) 
				                            else abs(datediff(day, @NextCouponDate, @LastCouponDate)) end -- pembagi hari

			                            set @Days = case when @InterestDaysType in (3) 
			                            then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) 
				                            else abs(datediff(day, @SettledDate, @LastCouponDate)) end -- pembagi hari

			                            set @InterestDays	= case when @InterestDaysType in (3) 
			                            then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate, @SettledDate)) else abs(datediff(day, @SettledDate, @AcqDate)) end -- pembagi hari

			                            set @DaysAcq_2 = case when @InterestDaysType in (3) then case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate, @SettledDate)) end else case when @AcqDate <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate)) end end -- total hari [acq date > prev coupon date]

			                            set @DaysAcq1_2	= case when @InterestDaysType in (3) then case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate1, @SettledDate)) end else case when @AcqDate1 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate1)) end end -- total hari [acq date 1 > prev coupon date]

			                            set @DaysAcq2_2	= case when @InterestDaysType in (3) then case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate2, @SettledDate)) end else case when @AcqDate2 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate2)) end end -- total hari [acq date 2 > prev coupon date]

			                            set @DaysAcq3_2	= case when @InterestDaysType in (3) then case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate3, @SettledDate)) end else case when @AcqDate3 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate3)) end end -- total hari [acq date 3 > prev coupon date]

			                            set @DaysAcq4_2	= case when @InterestDaysType in (3) then case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate4, @SettledDate)) end else case when @AcqDate4 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate4)) end end -- total hari [acq date 4 > prev coupon date]

			                            set @DaysAcq5_2	= case when @InterestDaysType in (3) then case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate5, @SettledDate)) end else case when @AcqDate5 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate5)) end end -- total hari [acq date 5 > prev coupon date]
						
		

			                            set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
			                            set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
			                            set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
			                            set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
			                            set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
			                            set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end
		
		

			                            set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * case when isnull(@AcqVolume, 0) <> 0 then @AcqVolume else @Volume end / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice) * case when isnull(@AcqVolume, 0) <> 0 then @AcqVolume else @Volume end / 100)  else 0 end

			                            set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end

			                            set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) 
			                            when @TrxType = 1 then((@Price - @AcqPrice2) * @AcqVolume2 / 100) else 0 end

			                            set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) 
			                            when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100) else 0 end

			                            set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) else 0 end

			                            set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) 
			                            when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) else 0 end

			                            set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100

			                            set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

			                            set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

			                            set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

			                            set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

			                            set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

			                            set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
			                            when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end


			                            set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100

		                            end
		
		                            if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
		                            begin
		
			                            set @ValuePerUnit = 1
			                            set @DivDays = case when @InterestDaysType in (6) 
			                            then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
			
			                            set @Days = case when @InterestDaysType in (6) 
			                            then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) end -- pembagi hari
		
			                            set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,					@SettledDate)) end -- pembagi hari


			                            set @DaysAcq_2 = case when @InterestDaysType in (6) then case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) end else case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate, @SettledDate)) end end -- total hari [acq date > prev coupon date]
			                            set @DaysAcq1_2 = case when @InterestDaysType in (6) then case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate1, @SettledDate)) end else case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate1, @SettledDate)) end end -- total hari [acq date 1 > prev coupon date]
			                            set @DaysAcq2_2 = case when @InterestDaysType in (6) then case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate2, @SettledDate)) end else case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate2, @SettledDate)) end end -- total hari [acq date 2 > prev coupon date]
			                            set @DaysAcq3_2 = case when @InterestDaysType in (6) then case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate3, @SettledDate)) end else case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate3, @SettledDate)) end end -- total hari [acq date 3 > prev coupon date]
			                            set @DaysAcq4_2 = case when @InterestDaysType in (6) then case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate4, @SettledDate)) end else case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate4, @SettledDate)) end end -- total hari [acq date 4 > prev coupon date]
			                            set @DaysAcq5_2 = case when @InterestDaysType in (6) then case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate5, @SettledDate)) end else case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate5, @SettledDate)) end end -- total hari [acq date 5 > prev coupon date]

			                            set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
			                            set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
			                            set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
			                            set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
			                            set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
			                            set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end

			                            set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
			                            set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
			                            set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
			                            set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
			                            set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
			                            set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end

			                            set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
			                            set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100

			                            set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end else 0 end
			                            set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end else 0 end
			                            set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end else 0 end
			                            set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end else 0 end
			                            set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end else 0 end
			                            set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end else 0 end


			                            set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
			                            set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100

		
			
		
		                            end
	                            end
                            end

                            if @CurrencyID = 'USD' -- USD
                            begin
	                            if @InterestType = 3 -- ZERO COUPONT
	                            BEGIN
		                            set @AccuredInterestAmount = 0
		                            set @GrossAmount = @Volume * @price/100
		
	                            END
	                            ELSE
	                            BEGIN
		                            if @InstrumentTypePK in (2) -- [Govt Bond]
		                            BEGIN	
				                            set @ValuePerUnit = 1
				                            set @DivDays = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @NextCouponDate))
				                            set @Days = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @SettledDate))
				                            set @InterestDays	= abs([dbo].[FGetDateDIffGovermentBond](@AcqDate, @SettledDate)) -- total hari Interest

			
		                            END
		                            Else if @InstrumentTypePK in (3) -- Corp Bond
		                            BEGIN
				                            set @ValuePerUnit = 1
				                            set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
				                            set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) -- total hari

			
		                            END
	                            END
                            end

                            Declare @TotalTax Numeric(22,4)

                            if @InterestType <> 3 --ZERO COUPON
                            BEGIN
	                            if @InstrumentTypePK in (3,8,9,13,15)
	                            BEGIN
		                            set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
		                            set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
	                            END
	                            ELSE IF @InstrumentTypePK in (2)
	                            BEGIN
		                            IF @BitIsRounding = 1
		                            BEGIN
			                            set @AccuredInterestAmount = @Volume / @ValuePerUnit * (round(@CouponRate / 100 
			                            * @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0))
			                            set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
		                            END
		                            ELSE
		                            BEGIN
			                            set @AccuredInterestAmount = @Volume / @ValuePerUnit * (@CouponRate / 100 
			                            * @Days / @DivDays / @InterestPaymentType * @ValuePerUnit)
			                            set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
		                            END
	                            END
                            END

                            set @TotalTaxCapGain = case when isnull(@TaxCapGainAcq,0) > 0 then  isnull(@TaxCapGainAcq,0) else 0 end 
                            + case when isnull(@TaxCapGainAcq1,0) > 0 then isnull(@TaxCapGainAcq1,0) else 0 end 
                            + case when isnull(@TaxCapGainAcq2,0) > 0 then isnull(@TaxCapGainAcq2,0) else 0 end
                            + case when isnull(@TaxCapGainAcq3,0) > 0 then isnull(@TaxCapGainAcq3,0) else 0 end
                            + case when isnull(@TaxCapGainAcq4,0) > 0 then isnull(@TaxCapGainAcq4,0) else 0 end 
                            + case when isnull(@TaxCapGainAcq5,0) > 0 then isnull(@TaxCapGainAcq5,0) else 0 end 

                            set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
                            + case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
                            + case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
                            + case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
                            + case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
                            + case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 

                            set @TotalCapGain = isnull(@CapGainAcq,0) + isnull(@CapGainAcq1,0) + isnull(@CapGainAcq2,0) + isnull(@CapGainAcq3,0) + isnull(@CapGainAcq4,0) + isnull(@CapGainAcq5,0)
                            set @TotalAI = isnull(@AIAcq,0) + isnull(@AIAcq1,0) + isnull(@AIAcq2,0) + isnull(@AIAcq3,0) + isnull(@AIAcq4,0) + isnull(@AIAcq5,0)


                            set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
                            set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)


                            declare @TrxAmount numeric (22,4)
                            declare @InvestmentAcc int

                            select @TrxAmount = case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@ValueDate,@InstrumentPK) 
                                                        when @ClientCode = '21' then dbo.FGetLastAvgFromInvestment_Acc_ByCompanyAccountTradingPK(@ValueDate,@InstrumentPK,@CompanyAccountTradingPK) 
                                                            else dbo.FGetLastAvgFromInvestment_Acc(@ValueDate,@InstrumentPK) end/100 * @Volume

                            --select @InvestmentAcc = InvInBond From AccountingSetup where status = 2

                            --select @TrxAmount =  sum(BaseDebit - BaseCredit) from JournalDetail A 
                            --left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
                            --where AccountPK = @InvestmentAcc and ValueDate <= @ValueDate 
                            --and InstrumentPK = @InstrumentPK 

                            Select isnull(@AccuredInterestAmount,0) InterestAmount, isnull(@TotalTaxAI,0) IncomeTaxInterestAmount,isnull(@TotalTaxCapGain,0) IncomeTaxGainAmount,
                            isnull(@GrossAmount,0) GrossAmount, isnull(@NetAmount,0) NetAmount,isnull(sum(@Amount - @TrxAmount),0) RealisedAmount
                             ";

                            cmd.Parameters.AddWithValue("@TaxCapitaGainPercent", 15);
                            cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", 5);
                            cmd.Parameters.AddWithValue("@BitIsRounding", 0);
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", _trxPortfolio.InstrumentTypePK);
                            cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                            cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);


                        }
                        else if (_trxPortfolio.InstrumentTypePK == 4)
                        {
                            cmd.CommandText =
                            @"
                          
                            Declare @BoardType int
                            set @BoardType = 1
                            Create Table #TrxPortfolio
                            (
                            TrxType int,
                            DoneAmount numeric (22,4),
                            CounterpartPK int,
                            )
                        
                            Insert Into #TrxPortfolio (TrxType,DoneAmount)
                            select @TrxType,@Price * @Volume

                            declare @TrxAmount numeric (22,4)
                            declare @InvestmentAcc int

                            select @TrxAmount = case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@ValueDate,@InstrumentPK) 
                                                        when @ClientCode = '21' then dbo.FGetLastAvgFromInvestment_Acc_ByCompanyAccountTradingPK(@ValueDate,@InstrumentPK,@CompanyAccountTradingPK) 
                                                            else dbo.FGetLastAvgFromInvestment_Acc(@ValueDate,@InstrumentPK) end * @Volume

                            Select 
                            DoneAmount NetAmount,isnull(sum(DoneAmount - @TrxAmount),0) RealisedAmount    
                            from #TrxPortfolio 
                            Group By DoneAmount,TrxType


 
                             ";


                            cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                            cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        }
                        else
                        {
                            cmd.CommandText =
                            @"
                            Declare @BoardType int
                            set @BoardType = 1
                            Create Table #TrxPortfolio
                            (
	                            TrxType int,
	                            DoneAmount numeric (22,4),
	                            CounterpartPK int,
	                            BoardType int
                            )
                        
                            Insert Into #TrxPortfolio (TrxType,DoneAmount,CounterpartPK,BoardType)
                            select @TrxType,@Price * @Volume,@CounterpartPK,@BoardType



                            declare @CommissionPercent numeric (22,4)
                            declare @LevyPercent numeric (22,4)
                            declare @KPEIPercent numeric (22,4)
                            declare @VATPercent numeric (22,4)
                            declare @WHTPercent numeric (22,4)
                            declare @OTCPercent numeric (22,4)
                            declare @IncomeTaxInterestPercent numeric (22,4)
                            declare @IncomeTaxGainPercent numeric (22,4)
                            declare @IncomeTaxSellPercent numeric (22,4)

                            declare @Comm numeric (22,4)
                            declare @Levy numeric (22,4)
                            declare @KPEI numeric (22,4)
                            declare @VAT numeric (22,4)
                            declare @WHT numeric (22,4)
                            declare @OTC numeric (22,4)
                            declare @TaxInterest numeric (22,4)
                            declare @TaxGain numeric (22,4)
                            declare @TaxSell numeric (22,4)
                            declare @TotalAmount numeric (22,4)


                            declare @RoundingModeCommission int
                            declare @DecimalPlacesCommission int
                            declare @RoundingModeVAT int
                            declare @DecimalPlacesVAT int
                            declare @RoundingModeLevy int
                            declare @DecimalPlacesLevy int
                            declare @RoundingModeKPEI int
                            declare @DecimalPlacesKPEI int
                            declare @RoundingModeWHT int
                            declare @DecimalPlacesWHT int
                            declare @RoundingModeOTC int
                            declare @DecimalPlacesOTC int
                            declare @RoundingModeTaxInterest int
                            declare @DecimalPlacesTaxInterest int
                            declare @RoundingModeTaxGain int
                            declare @DecimalPlacesTaxGain int
                            declare @RoundingModeTaxSell int
                            declare @DecimalPlacesTaxSell int

                            declare @TrxAmount numeric (22,4)
                            declare @InvestmentAcc int

                            

                            select @TrxAmount = case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@ValueDate,@InstrumentPK) 
                                                         when @ClientCode = '21' then dbo.FGetLastAvgFromInvestment_Acc_ByCompanyAccountTradingPK(@ValueDate,@InstrumentPK,@CompanyAccountTradingPK) 
                                                            else dbo.FGetLastAvgFromInvestment_Acc(@ValueDate,@InstrumentPK) end * @Volume

                            --select @InvestmentAcc = InvInEquity From AccountingSetup where status = 2
                            --select @TrxAmount =  sum(BaseDebit - BaseCredit) from JournalDetail A 
                            --left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
                            --where AccountPK = @InvestmentAcc and ValueDate <= @ValueDate 
                            --and InstrumentPK = @InstrumentPK 


                            select @RoundingModeCommission = RoundingCommission,@DecimalPlacesCommission = DecimalCommission,
                            @RoundingModeVAT = RoundingVAT,@DecimalPlacesVAT = DecimalVAT,
                            @RoundingModeLevy = RoundingLevy,@DecimalPlacesLevy = DecimalLevy,
                            @RoundingModeKPEI = RoundingKPEI,@DecimalPlacesKPEI = DecimalKPEI,
                            @RoundingModeWHT = RoundingWHT,@DecimalPlacesOTC = DecimalOTC,
                            @RoundingModeTaxInterest = RoundingTaxInterest,@DecimalPlacesTaxInterest = DecimalTaxInterest,
                            @RoundingModeTaxGain = RoundingTaxGain,@DecimalPlacesTaxGain = DecimalTaxGain,
                            @RoundingModeTaxSell = RoundingTaxSell,@DecimalPlacesTaxSell = DecimalTaxSell,@BoardType = A.BoardType
                            from CounterpartCommission B left join #TrxPortfolio A
                            on A.CounterpartPK = B.CounterpartPK 
                            where B.status  = 2 and B.BoardType = @BoardType and B.CounterpartPK = @CounterpartPK



                            Select 
                            C.Comm CommissionAmount,C.Levy LevyAmount,C.KPEI KPEIAmount,C.VAT VATAmount,C.WHT WHTAmount,C.OTC OTCAmount,C.TaxInterest IncomeTaxInterestAmount,C.TaxGain IncomeTaxGainAmount,0 InterestAmount,C.TaxSell IncomeTaxSellAmount,case when C.TrxType = 1 then sum(C.DoneAmount + C.Comm + C.Levy + C.KPEI + C.OTC + C.VAT) Else sum(C.DoneAmount - C.Comm - C.Levy - C.KPEI - C.OTC - C.VAT - C.TaxSell) End GrossAmount,case when C.TrxType = 1 then sum(C.DoneAmount + C.Comm + C.Levy + C.KPEI + C.OTC + C.VAT - C.WHT) Else sum(C.DoneAmount - C.Comm - C.Levy - C.KPEI - C.OTC - C.VAT - C.TaxSell + C.WHT) End NetAmount,isnull(sum(DoneAmount - @TrxAmount),0) RealisedAmount    
                            from (
                            select A.TrxType,A.DoneAmount,
                            B.CommissionPercent ,B.LevyPercent,B.KPEIPercent,B.VATPercent,
                            B.WHTPercent,B.OTCPercent,B.IncomeTaxInterestPercent,B.IncomeTaxGainPercent,B.IncomeTaxSellPercent,
                            --Comm
                            Case when @RoundingModeCommission = 1 and B.DecimalCommission = @DecimalPlacesCommission then Sum(isnull(round(A.DoneAmount * (B.CommissionPercent/100),@DecimalPlacesCommission),0))
                            else Case when @RoundingModeCommission = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.CommissionPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.CommissionPercent/100),0),0)) End End  Comm,
                            Case when @RoundingModeLevy = 1 and B.DecimalLevy = @DecimalPlacesLevy then Sum(isnull(round(A.DoneAmount * (B.LevyPercent/100),@DecimalPlacesLevy),0))
                            else Case when @RoundingModeLevy = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.LevyPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.LevyPercent/100),0),0)) End End  Levy,
                            Case when @RoundingModeKPEI = 1 and B.DecimalKPEI = @DecimalPlacesKPEI then Sum(isnull(round(A.DoneAmount * (B.KPEIPercent/100),@DecimalPlacesKPEI),0))
                            else Case when @RoundingModeKPEI = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.KPEIPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.KPEIPercent/100),0),0)) End End  KPEI,
                            Case when @RoundingModeVAT = 1 and B.DecimalVAT = @DecimalPlacesVAT then Sum(isnull(round(A.DoneAmount * (B.VATPercent/100),@DecimalPlacesVAT),0))
                            else Case when @RoundingModeVAT = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.VATPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.VATPercent/100),0),0)) End End  VAT,
                            Case when @RoundingModeWHT = 1 and B.DecimalWHT = @DecimalPlacesWHT then Sum(isnull(round(A.DoneAmount * (B.WHTPercent/100),@DecimalPlacesWHT),0))
                            else Case when @RoundingModeWHT = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.WHTPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.WHTPercent/100),0),0)) End End  WHT,
                            Case when @RoundingModeOTC = 1 and B.DecimalOTC = @DecimalPlacesOTC then Sum(isnull(round(A.DoneAmount * (B.OTCPercent/100),@DecimalPlacesOTC),0))
                            else Case when @RoundingModeOTC = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.OTCPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.OTCPercent/100),0),0)) End End  OTC,
                            Case when @RoundingModeTaxInterest = 1 and B.DecimalTaxInterest = @DecimalPlacesTaxInterest then Sum(isnull(round(A.DoneAmount * (B.IncomeTaxInterestPercent/100),@DecimalPlacesTaxInterest),0))
                            else Case when @RoundingModeTaxInterest = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.IncomeTaxInterestPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxInterestPercent/100),0),0)) End End  TaxInterest,
                            Case when @RoundingModeTaxGain = 1 and B.DecimalTaxGain = @DecimalPlacesTaxGain then Sum(isnull(round(A.DoneAmount * (B.IncomeTaxGainPercent/100),@DecimalPlacesTaxGain),0))
                            else Case when @RoundingModeTaxGain = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.IncomeTaxGainPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxGainPercent/100),0),0)) End End  TaxGain,
                            Case when A.TrxType = 1 then 0 else Case when @RoundingModeTaxSell = 1 and B.DecimalTaxSell = @DecimalPlacesTaxSell then Sum(isnull(round(A.DoneAmount * (B.IncomeTaxSellPercent/100),@DecimalPlacesTaxSell),0))
                            else Case when @RoundingModeTaxSell = 2 then Sum(isnull(FLOOR(A.DoneAmount * (B.IncomeTaxSellPercent/100)),0)) 
                            else  Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxSellPercent/100),0),0)) End End End TaxSell
                            from #TrxPortfolio A
                            left join CounterpartCommission B on A.CounterpartPK = B.CounterpartPK and B.Status = 2 where B.BoardType = @BoardType and B.CounterpartPK = @CounterpartPK
                            Group By A.DoneAmount,A.TrxType,
                            B.CommissionPercent,B.LevyPercent,B.KPEIPercent,B.VATPercent,
                            B.WHTPercent,B.OTCPercent,B.IncomeTaxInterestPercent,B.IncomeTaxGainPercent,B.IncomeTaxSellPercent,B.RoundingCommission,B.DecimalCommission,B.RoundingLevy,B.DecimalLevy,B.RoundingKPEI,B.DecimalKPEI,B.RoundingVAT,B.DecimalVAT,
                            B.RoundingWHT,B.DecimalWHT,B.RoundingOTC,B.DecimalOTC,B.RoundingTaxInterest,B.DecimalTaxInterest,B.RoundingTaxGain,B.DecimalTaxGain,B.RoundingTaxSell,B.DecimalTaxSell
                            ) C
                            Group By  C.TrxType,C.Comm,C.Levy,C.KPEI,C.VAT,C.WHT,C.OTC,C.TaxInterest,C.TaxGain,C.TaxSell,C.DoneAmount,
                            C.CommissionPercent,C.LevyPercent,C.KPEIPercent,C.VATPercent,
                            C.WHTPercent,C.OTCPercent,C.IncomeTaxInterestPercent,C.IncomeTaxGainPercent,C.IncomeTaxSellPercent

 
                             ";


                            cmd.Parameters.AddWithValue("@CounterpartPK", _trxPortfolio.CounterpartPK);
                            cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        }
                        cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _trxPortfolio.CompanyAccountTradingPK);
                        cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                        cmd.Parameters.AddWithValue("@ValueDate", _trxPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                        cmd.Parameters.AddWithValue("@SettledDate", _trxPortfolio.SettledDate);
                        cmd.Parameters.AddWithValue("@NextCouponDate", _trxPortfolio.NextCouponDate);
                        cmd.Parameters.AddWithValue("@LastCouponDate", _trxPortfolio.LastCouponDate);
                        cmd.Parameters.AddWithValue("@Price", _trxPortfolio.Price);
                        cmd.Parameters.AddWithValue("@Volume", _trxPortfolio.Volume);
                        cmd.Parameters.AddWithValue("@AcqPrice", _trxPortfolio.AcqPrice);
                        if (_trxPortfolio.AcqDate == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", _trxPortfolio.AcqDate);
                        }

                        if (_trxPortfolio.AcqDate1 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", _trxPortfolio.AcqDate1);
                        }

                        if (_trxPortfolio.AcqDate2 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", _trxPortfolio.AcqDate2);
                        }

                        if (_trxPortfolio.AcqDate3 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", _trxPortfolio.AcqDate3);
                        }

                        if (_trxPortfolio.AcqDate4 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", _trxPortfolio.AcqDate4);
                        }

                        if (_trxPortfolio.AcqDate5 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", _trxPortfolio.AcqDate5);
                        }

                        cmd.Parameters.AddWithValue("@AcqVolume", _trxPortfolio.AcqVolume);
                        cmd.Parameters.AddWithValue("@AcqPrice1", _trxPortfolio.AcqPrice1);
                        cmd.Parameters.AddWithValue("@AcqVolume1", _trxPortfolio.AcqVolume1);

                        cmd.Parameters.AddWithValue("@AcqPrice2", _trxPortfolio.AcqPrice2);
                        cmd.Parameters.AddWithValue("@AcqVolume2", _trxPortfolio.AcqVolume2);

                        cmd.Parameters.AddWithValue("@AcqPrice3", _trxPortfolio.AcqPrice3);
                        cmd.Parameters.AddWithValue("@AcqVolume3", _trxPortfolio.AcqVolume3);

                        cmd.Parameters.AddWithValue("@AcqPrice4", _trxPortfolio.AcqPrice4);
                        cmd.Parameters.AddWithValue("@AcqVolume4", _trxPortfolio.AcqVolume4);

                        cmd.Parameters.AddWithValue("@AcqPrice5", _trxPortfolio.AcqPrice5);
                        cmd.Parameters.AddWithValue("@AcqVolume5", _trxPortfolio.AcqVolume5);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if (_trxPortfolio.InstrumentTypePK == 2)
                                    {
                                        M_TrxPortfolio.RealisedAmount = Convert.ToDecimal(dr["RealisedAmount"]);
                                        M_TrxPortfolio.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
                                        M_TrxPortfolio.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
                                        M_TrxPortfolio.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
                                        M_TrxPortfolio.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                        M_TrxPortfolio.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                                    }
                                    else if (_trxPortfolio.InstrumentTypePK == 4)
                                    {
                                        M_TrxPortfolio.RealisedAmount = Convert.ToDecimal(dr["RealisedAmount"]);
                                        M_TrxPortfolio.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                                    }
                                    else
                                    {
                                        M_TrxPortfolio.CommissionAmount = Convert.ToDecimal(dr["CommissionAmount"]);
                                        M_TrxPortfolio.LevyAmount = Convert.ToDecimal(dr["LevyAmount"]);
                                        M_TrxPortfolio.KPEIAmount = Convert.ToDecimal(dr["KPEIAmount"]);
                                        M_TrxPortfolio.VATAmount = Convert.ToDecimal(dr["VATAmount"]);
                                        M_TrxPortfolio.WHTAmount = Convert.ToDecimal(dr["WHTAmount"]);
                                        M_TrxPortfolio.OTCAmount = Convert.ToDecimal(dr["OTCAmount"]);
                                        M_TrxPortfolio.IncomeTaxSellAmount = Convert.ToDecimal(dr["IncomeTaxSellAmount"]);
                                        M_TrxPortfolio.RealisedAmount = Convert.ToDecimal(dr["RealisedAmount"]);
                                        M_TrxPortfolio.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
                                        M_TrxPortfolio.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
                                        M_TrxPortfolio.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
                                        M_TrxPortfolio.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                        M_TrxPortfolio.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                                    }

                                    return M_TrxPortfolio;
                                }

                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public TrxPortfolioForNetAmount Net_Recalculate(TrxPortfolioForNetAmount _trxPortfolio)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    TrxPortfolioForNetAmount M_TrxPortfolio = new TrxPortfolioForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_trxPortfolio.InstrumentTypePK == 2)
                        {
                            cmd.CommandText = @"
                             select sum(@Amount + @InterestAmount) GrossAmount,
                             sum(@Amount + @InterestAmount - @IncomeTaxGainAmount - @IncomeTaxInterestAmount) NetAmount
                             ";

                            cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                            cmd.Parameters.AddWithValue("@InterestAmount", _trxPortfolio.InterestAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxGainAmount", _trxPortfolio.IncomeTaxGainAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxInterestAmount", _trxPortfolio.IncomeTaxInterestAmount);
                        }
                        else
                        {
                            cmd.CommandText =
                            @"
                            select case when @TrxType = 1 then sum(@Amount + @CommissionAmount + @LevyAmount + @KPEIAmount + @OTCAmount + @VATAmount) 
                            Else sum(@Amount - (@CommissionAmount + @LevyAmount + @KPEIAmount + @OTCAmount + @VATAmount + @IncomeTaxSellAmount)) End GrossAmount,
                            case when @TrxType = 1 then sum(@Amount + @CommissionAmount + @LevyAmount + @KPEIAmount + @OTCAmount + @VATAmount - @WHTAmount) 
                            Else sum(@Amount - (@CommissionAmount + @LevyAmount + @KPEIAmount + @OTCAmount + @VATAmount + @IncomeTaxSellAmount - @WHTAmount)) End NetAmount
 
                             ";
                            cmd.Parameters.AddWithValue("@TrxType", _trxPortfolio.TrxType);
                            cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);
                            cmd.Parameters.AddWithValue("@CommissionAmount", _trxPortfolio.CommissionAmount);
                            cmd.Parameters.AddWithValue("@LevyAmount", _trxPortfolio.LevyAmount);
                            cmd.Parameters.AddWithValue("@KPEIAmount", _trxPortfolio.KPEIAmount);
                            cmd.Parameters.AddWithValue("@OTCAmount", _trxPortfolio.OTCAmount);
                            cmd.Parameters.AddWithValue("@VATAmount", _trxPortfolio.VATAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxSellAmount", _trxPortfolio.IncomeTaxSellAmount);
                            cmd.Parameters.AddWithValue("@WHTAmount", _trxPortfolio.WHTAmount);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    M_TrxPortfolio.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                    M_TrxPortfolio.NetAmount = Convert.ToDecimal(dr["NetAmount"]);

                                    return M_TrxPortfolio;
                                }

                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }




        public bool Validate_ApproveBySelected(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if not Exists(select * From TrxPortfolio where Status = 1 and ValueDate between @ValueDateFrom and @ValueDateTo and selected = 1 and Posted = 0 and Revised = 0 ) " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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


        public void TrxPortfolio_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'TrxPortfolio',TrxPortfolioPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                                 update TrxPortfolio set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where status = 1 and TrxPortfolioPK in ( Select TrxPortfolioPK from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                                 Update TrxPortfolio set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where status = 4 and TrxPortfolioPK in (Select TrxPortfolioPK from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void TrxPortfolio_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'TrxPortfolio',TrxPortfolioPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                                          update TrxPortfolio set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and TrxPortfolioPK in ( Select TrxPortfolioPK from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                                          Update TrxPortfolio set status= 2  where status = 4 and TrxPortfolioPK in (Select TrxPortfolioPK from TrxPortfolio where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void TrxPortfolio_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                         	declare @PValueDate datetime
                            declare @PSettledDate datetime
                            declare @PMaturityDate datetime
                            declare @PPeriodPK int
                            declare @PInstrumentPK int
                            declare @PCounterpartPK int
                            declare @PTrxType int
                            declare @PReference nvarchar(50)
                            declare @InstrumentID nvarchar(50)
                            declare @PInstrumentTypePK int
                            declare @InstrumentType int
                            declare @PCashRefPK int
                            declare @PVolume numeric (22,4)
                            declare @PBrokerageFeeAmount numeric (22,4)
                            declare @PLevyAmount numeric (22,4)
                            declare @PKPEIAmount numeric (22,4)                 
                            declare @PVATAmount numeric (22,4)
                            declare @PWHTAmount numeric (22,4)
                            declare @POTCAmount numeric (22,4)
                            declare @PIncomeTaxSellAmount numeric (22,4)
                            declare @PIncomeTaxInterestAmount numeric (22,4)
                            declare @PIncomeTaxGainAmount numeric (22,4)
                            declare @PNetAmount numeric (22,4)
                            declare @PRealisedAmount numeric (22,4)
                            declare @PInterestAmount numeric (22,4)

                            declare @InvestmentAcc int
                            declare @PayablePurchaseAcc int
                            declare @ReceivableSaleAcc int
                            declare @CommissionAcc int
                            declare @LevyAcc int
                            declare @VatAcc int
                            declare @WhtAcc int
                            declare @CashAtBankAcc int
                            declare @RealisedAcc int
                            declare @IncomeSaleTaxAcc int
                            declare @InterestRecAcc int
                            declare @journalPK int
                            declare @TrxPortfolioPK int
                            declare @TotalFeeAcc int
                            declare @RealisedAccReksadana int
                            declare @PCurrencyPK int
 
                            declare @PAmount numeric(22,4)
                            declare @FPayablePurchaseAmount numeric (22,4)
                        
   
                            declare @CashierPK int
                            declare @CashierID int
	                        Declare @CurReference nvarchar(100)  
	          
                        Declare A Cursor For                  
	                        Select A.TrxPortfolioPK,A.ValueDate,A.PeriodPK,A.Reference,A.TrxType,A.CounterpartPK,A.InstrumentPK,                   
	                        A.CashRefPK,A.InterestAmount,A.SettledDate,A.Volume,A.Amount,                  
	                        A.BrokerageFeeAmount,A.LevyAmount,A.KPEIAmount,A.VATAmount,A.WHTAmount,A.OTCAmount,A.IncomeTaxSellAmount,                  
	                        A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount,A.NetAmount,A.InstrumentTypePK,A.RealisedAmount,A.MaturityDate,B.CurrencyPK            
	                        From TrxPortfolio A
                            left join CashRef B on A.CashRefPK = B.CashRefPK and B.status = 2        
	                        Where A.status = 2 and A.Posted = 0 and A.Revised = 0 and A.ValueDate between @DateFrom and @DateTo and A.Selected = 1              
	                        Open A                  
	                        Fetch Next From A                  
	                        Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	                        @PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	                        @PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	                        @PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate,@PCurrencyPK                 
                        While @@FETCH_STATUS = 0                  
                        Begin                  
	                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK 
		      				               
	                        -- A1. BUY EQUITY --                 
	                        if @PTrxType = 1 and @PInstrumentTypePK = 1                  
	                        BEGIN     

                                IF(@ClientCode = '09')    
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CP',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CP',@CurReference out
                                END

             
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  
		                        IF @InstrumentType in (1,4,16)                  
		                        BEGIN                  
			                        Select @InvestmentAcc = InvInEquity,@PayablePurchaseAcc = APPurchaseEquity From AccountingSetup where Status in (1,2)   
		                        END                                
		                        Select @CommissionAcc = BrokerageFee,@LevyAcc = JSXLevyFee,@VatAcc = VATFee,@WhtAcc = WHTFee 
		                        From AccountingSetup where Status in (1,2)                  
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                  

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
			                        @CurReference,'T0 EQUITY BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PAmount,                   
			                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                  


		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,2,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PNetAmount,                   
			                        0,@PNetAmount,1,0,@PNetAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                    

			                        Select @JournalPK,3,1,2,@CommissionAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PBrokerageFeeAmount,                   
			                        @PBrokerageFeeAmount,0,1,@PBrokerageFeeAmount,0,@LastUpdate From Account Where AccountPK = @CommissionAcc and Status = 2                  

		                        set @PLevyAmount = @PLevyAmount + @PKPeIAmount                

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,4,1,2,@LevyAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PLevyAmount,                   
			                        @PLevyAmount,0,1,@PLevyAmount,0,@LastUpdate From Account Where AccountPK = @LevyAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])  

			                        Select @JournalPK,5,1,2,@VatAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PVATAmount,                   
			                        @PVATAmount,0,1,@PVATAmount,0,@LastUpdate From Account Where AccountPK = @VatAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select @JournalPK,6,1,2,@WhtAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PWHTAmount,                   
			                        0,@PWHTAmount,1,0,@PWHTAmount,@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  

		                        -- T Settled  
		                        select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CP' and status not in (3,4)
		                        

                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP','','T-SETTLED EQUITY BUY: ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@PNetAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		    
		                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

		                        --	Select  @JournalPK,1,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PNetAmount,@PNetAmount,0,1,@PNetAmount,0,@LastUpdate   
		                        --	From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

		                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

		                        --	Select @JournalPK,2,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PNetAmount,                   
		                        --	0,@PNetAmount,1,0,@PNetAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                  
	                        END 
               
	                        -- A2. SELL EQUITY --              
	                        if @PTrxType = 2 and @PInstrumentTypePK = 1                  
	                        BEGIN      

                                IF(@ClientCode = '09')    
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CR',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CR',@CurReference out
                                END

            
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  
		                        IF @InstrumentType in (1,4,16)                  
		                        BEGIN                 
			                        Select @InvestmentAcc = InvInEquity,@ReceivableSaleAcc = ARSellEquity,@RealisedAcc = RealisedEquity From AccountingSetup where Status in (1,2)                  
		                        END                  
 
		                        Select @CommissionAcc = BrokerageFee,@LevyAcc = JSXLevyFee,@VatAcc = VATFee,@WhtAcc = WHTFee 
		                        ,@IncomeSaleTaxAcc = IncomeTaxArt23, @TotalFeeAcc = CadanganEquity
		                        From AccountingSetup where Status in (1,2)       
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  
		                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                    
			                        @CurReference,'T0 EQUITY SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,1,1,2,@CommissionAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PBrokerageFeeAmount,                   
			                        @PBrokerageFeeAmount,0,1,@PBrokerageFeeAmount,0,@LastUpdate From Account Where AccountPK = @CommissionAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,2,1,2,@LevyAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PLevyAmount + @PKPEIAmount,                   
			                        @PLevyAmount + @PKPEIAmount,0,1,@PLevyAmount + @PKPEIAmount,0,@LastUpdate From Account Where AccountPK = @LevyAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

			                        Select  @JournalPK,3,1,2,@VatAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PVATAmount,                   
			                        @PVATAmount,0,1,@PVATAmount,0,@LastUpdate From Account Where AccountPK = @VatAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

			                        Select  @JournalPK,4,1,2,@IncomeSaleTaxAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PIncomeTaxSellAmount,                   
			                        @PIncomeTaxSellAmount,0,1,@PIncomeTaxSellAmount,0,@LastUpdate From Account Where AccountPK = @IncomeSaleTaxAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,5,1,2,@WhtAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PWHTAmount,                   
			                        0,@PWHTAmount,1,0,@PWHTAmount,@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  

			
				                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
				                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,6,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',isnull(@PNetAmount,0),                   
                                    isnull(@PNetAmount,0),0,1,isnull(@PNetAmount,0),0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2   


		                        Declare @SellArAmount numeric(22,6)   
                                Declare @TotalFeeAmount numeric(22,6)            
		                        Set @SellArAmount = @PAmount - isnull(@PBrokerageFeeAmount,0) - isnull(@PLevyAmount,0) - isnull(@PKPEIAmount,0) - isnull(@PVATAmount,0) - isnull(@PIncomeTaxSellAmount,0) + isnull(@PWHTAmount,0)                
                                Set @TotalFeeAmount = isnull(@PBrokerageFeeAmount,0) + isnull(@PLevyAmount,0) + isnull(@PKPEIAmount,0) + isnull(@PVATAmount,0) + isnull(@PIncomeTaxSellAmount,0)       

		                        Declare @InvestmentShareAmount numeric(22,6)                          

		                        -- Gain Realised
		                        if @PRealisedAmount > 0              
		                        Begin            
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                                Select  @JournalPK,7,1,2,@InvestmentAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PAmount - abs(@PRealisedAmount),                   
			                                0,@PAmount- abs(@PRealisedAmount),1,0,@PAmount - abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

  
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

				                            Select @JournalPK,8,1,2,@RealisedAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@PRealisedAmount),                   
				                            0,abs(@PRealisedAmount),1,0,abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                  
         
		                        End       
     
		                        -- Loss Realised
		                        if @PRealisedAmount <= 0              
		                        begin     

    
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],[DepartmentPK],                 
		                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                                Select  @JournalPK,7,1,2,@RealisedAcc,1,@PInstrumentPK,@PCounterpartPK,3,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@PRealisedAmount),                   
			                                abs(@PRealisedAmount),0,1,abs(@PRealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                   
     

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],[DepartmentPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,8,1,2,@InvestmentAcc,1,@PInstrumentPK,@PCounterpartPK,3,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PAmount + abs(@PRealisedAmount),                   
				                        0,@PAmount + abs(@PRealisedAmount),1,0,@PAmount + abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                 
			 
       
		                        end  
            

                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2

		                        -- T SETTLED         
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)
                     

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)


                                select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR','','T-SETTLED EQUITY SELL: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@SellArAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  
     
		                      
	                        END    
	
	                        -- BUY BOND
	
	                        if @PTrxType = 1 and @PInstrumentTypePK = 2                 
	                        BEGIN  

                                IF(@ClientCode = '09')    
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CP',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CP',@CurReference out
                                END

		                        declare @BondDayAccrued int                 
		                        -- 2 = G-BOND                  
		                        -- 3 = C-BOND                  
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                                           

		                        if @InstrumentType not in (1,4,5,6,16)      
		                        BEGIN            	      
			                        Select @InvestmentAcc = InvInBond,@InterestRecAcc = InterestPurchaseBond,@PayablePurchaseAcc = APPurchaseBond, 
			                        @BondDayAccrued = InterestReceivableBond,@WHTAcc = IncomeTaxArt23
			                        From AccountingSetup where Status in (1,2)      
		                        END                  
                  
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  
	
		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal 
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                     

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                    
			                        @CurReference,'T0 BOND BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND BUY: ' + @InstrumentID,'D',@PAmount,                   
				                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,2,1,2,@InterestRecAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND BUY: ' + @InstrumentID,'D',@PInterestAmount,                   
				                        @PInterestAmount,0,1,@PInterestAmount,0,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2                   

		                                set @FPayablePurchaseAmount = @PAmount + isnull(@PInterestAmount,0) -  isnull(@PIncomeTaxInterestAmount,0) - isnull(@PIncomeTaxGainAmount,0)               

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                     

				                        Select @JournalPK,3,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND BUY: ' + @InstrumentID,'C',@FPayablePurchaseAmount,                   
				                        0,@FPayablePurchaseAmount,1,0,@FPayablePurchaseAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                     

				                        Select @JournalPK,4,1,2,@WhtAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND BUY: ' + @InstrumentID,'C',isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),                   
				                        0,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0) ,1,0,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  


                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2

		                        -- T Settled  
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CP' and status not in (3,4)


                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP','','T-SETTLED BOND BUY: ' + @InstrumentID,'D',1,
                                1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@FPayablePurchaseAmount,@FPayablePurchaseAmount,1,1,@FPayablePurchaseAmount,@FPayablePurchaseAmount,100,@FPayablePurchaseAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK    

              
		                        --select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal 
	
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                

			                    --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PSettledDate,4,TrxPortfolioPK,'TRANSACTION',                  
			                    --    @PReference,'T-Settled BOND BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

				                        --Select  @JournalPK,1,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'D',@FPayablePurchaseAmount,                   
				                        --@FPayablePurchaseAmount,0,1,@FPayablePurchaseAmount,0,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

				                        --Select @JournalPK,2,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'C',@FPayablePurchaseAmount,                   
				                        --0,@FPayablePurchaseAmount,1,0,@FPayablePurchaseAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

				                        --Select  @JournalPK,3,1,2,@BondDayAccrued,1,@PInstrumentPK,'T-Settled BOND BUY: ' + @InstrumentID,'D',@PInterestAmount,                   
				                        --@PInterestAmount,0,1,@PInterestAmount,0,@LastUpdate From Account Where AccountPK = @BondDayAccrued and Status = 2                  

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        --Select @JournalPK,4,1,2,@InterestRecAcc,1,@PInstrumentPK,'T-Settled BOND BUY: ' + @InstrumentID,'C',@PInterestAmount,                   
				                        --0,@PInterestAmount,1,0,@PInterestAmount,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2                  
	                        END   
	  
	                        --SELL BOND
           
	                        if @PTrxType = 2 and @PInstrumentTypePK = 2                  
	                        BEGIN        
                               
                                IF(@ClientCode = '09')  
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CR',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CR',@CurReference out
                                END

		                        -- 2 = G-BOND                  
		                        -- 3 = C-BOND                  
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                                           

		                        if @InstrumentType not in (1,4,5,6,16)         
		                        BEGIN            	      
			                        Select @InvestmentAcc = InvInBond,@InterestRecAcc = InterestPurchaseBond,@ReceivableSaleAcc = ARSellBond, 
			                        @BondDayAccrued = InterestReceivableBond,@WHTAcc = IncomeTaxArt23,@RealisedAcc=RealisedBond
			                        From AccountingSetup where Status in (1,2)      
		                        END                  
    
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
		                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                   
			                        @CurReference,'T0 BOND SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select  @JournalPK,1,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND SELL: ' + @InstrumentID,'D',@PNetAmount,                   
			                        @PNetAmount,0,1,@PNetAmount,0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2          
         
                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,2,1,2,@InterestRecAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND SELL: ' + @InstrumentID,'C',@PInterestAmount,                   
				                    0,@PInterestAmount,1,0,@PInterestAmount,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2   

                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                 
                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,3,1,2,@WHTAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND SELL: ' + @InstrumentID,'D',isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),                   
				                    isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),0,1,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),0,@LastUpdate From Account Where AccountPK = @WHTAcc and Status = 2  
         

		                        -- Gain Realised
		                        if @PRealisedAmount > 0              
		                        Begin              
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],[DepartmentPK],                 
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

				                        Select @JournalPK,4,1,2,@RealisedAcc,1,@PInstrumentPK,@PCounterpartPK,3,'T0 BOND SELL: ' + @InstrumentID,'C',abs(@PRealisedAmount),                   
				                        0,abs(@PRealisedAmount),1,0,abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                  

			                        set  @InvestmentShareAmount = @PAmount - abs(@PRealisedAmount)              
		                        End       
     
		                        -- Loss Realised
		                        if @PRealisedAmount <= 0              
		                        begin              
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK], [DepartmentPK],               
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,4,1,2,@RealisedAcc,1,@PInstrumentPK,@PCounterpartPK,3,'T0 BOND SELL: ' + @InstrumentID,'D',abs(@PRealisedAmount),                   
				                        abs(@PRealisedAmount),0,1,abs(@PRealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                 
			 
			                        set  @InvestmentShareAmount = @PAmount + abs(@PRealisedAmount)              
		                        end       

				                                    

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [CounterpartPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                        Select @JournalPK,5,1,2,@InvestmentAcc,1,@PInstrumentPK,@PCounterpartPK,'T0 BOND SELL: ' + @InstrumentID,'C',@InvestmentShareAmount,                   
				                        0,@InvestmentShareAmount,1,0,@InvestmentShareAmount,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2   


                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2

		                        -- T SETTLED      
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)

                

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR','','T-SETTLED BOND SELL: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@PNetAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  

        
		                                         
	                        END   

	                        -- A5.PLACEMENT DEPOSITO --                 
                            if (@PTrxType = 1 and @PInstrumentTypePK = 3)

                            BEGIN                  
                                Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                           

                                if @InstrumentType = 5                  
                                BEGIN  
                                    if (@PCurrencyPK = 1)
                                    BEGIN
                                        Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)  
                                    END
                                    ELSE
                                    BEGIN
                                        Select @InvestmentAcc = InvInTDUSD From AccountingSetup where Status in (1,2)  
                                    END                               
                                    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                END    


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CP' and status not in (3,4)
                
          
                                IF(@ClientCode = '09')    
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CP',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CP',@CurReference out
                                END


		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)


	                            select @CashierPK,@CashierID,1,1,@PPeriodPK, @PValueDate,'CP','',case when @PTrxType = 3 then 'ROLLOVER DEPOSITO : ' + @InstrumentID else 'PLACEMENT DEPOSITO : ' + @InstrumentID end,'D',1,
	                            1,0,@PCashRefPK,@InvestmentAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
	                            EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     


                                select @PPeriodPK = PeriodPK from Period where @PMaturityDate between DateFrom and DateTo and status = 2

                                --MATURE DEPOSITO
                                
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)
                
                                IF(@ClientCode = '09')  
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CR',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CR',@CurReference out
                                END



                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,@CashierID,1,1,@PPeriodPK, @PMaturityDate,'CR','','DEPOSIT MATURE: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@InvestmentAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  
                                  
                            END                  

                            -- A6. LIQUIDATE DEPOSITO --            
                            if @PTrxType = 2 and @PInstrumentTypePK = 3                  
                            BEGIN                  
                                
                                Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2       

                                update cashier set status = 3, VoidUsersID = @UsersID , VoidTime = @LastUpdate where TrxNo > 0 
                                and InstrumentPK = @PInstrumentPK
                                and Type = 'CR' and Status  = 1 and ValueDate >= @PValueDate            

                                if @InstrumentType = 5                  
                                BEGIN     
                                    if (@PCurrencyPK = 1)
                                    BEGIN
                                        Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)  
                                    END
                                    ELSE
                                    BEGIN
                                        Select @InvestmentAcc = InvInTDUSD From AccountingSetup where Status in (1,2)  
                                    END              
              
                                    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                END                  

                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2
                                
                                --select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                --select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)
          

                                --exec getJournalReference @PSettledDate,'CR',@CurReference out

                                --INSERT INTO [dbo].[Cashier]  
                                --([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                --[CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                --[Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                --[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                --select @CashierPK,@CashierID,1,1,@PPeriodPK, @PSettledDate,'CR','','LIQUIDATE DEPOSITO: ' + @InstrumentID,'C',1,
                                --1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                --EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  



                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)


                                IF(@ClientCode = '09') 
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CR',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CR',@CurReference out
                                END


                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,@CashierID,1,1,@PPeriodPK, @PValueDate,'CR','','DEPOSIT LIQUIDATE: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@InvestmentAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)


                                IF(@ClientCode = '09') 
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CP',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CP',@CurReference out
                                END



		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,@CashierID,1,1,@PPeriodPK, @PMaturityDate,'CP','','DEPOSIT MATURE : ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@InvestmentAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		     
                             
                            END    

                            -- A7. ROLLOVER DEPOSITO --         
                            if @PTrxType = 3 and @PInstrumentTypePK = 3                  
                            BEGIN                  
                                


DECLARE @OldInstrumentPK INT 


SELECT @OldInstrumentPK = B.InstrumentPK FROM Instrument A 
LEFT JOIN Instrument B ON A.ID = B.ID AND A.Name = B.Name AND 
A.InterestPercent = B.InterestPercent AND B.MaturityDate = @PValueDate
and A.InstrumentTypePK = B.InstrumentTypePK
WHERE A.InstrumentPK = @PInstrumentPK
AND A.status = 2


                                update cashier set ValueDate = @PMaturityDate,InstrumentPK = @PInstrumentPK,UpdateTime = @LastUpdate, UpdateUsersID = @UsersID where TrxNo > 0 
                                and InstrumentPK = @OldInstrumentPK
                                and Type = 'CR' and Status  = 1 and ValueDate >= @PValueDate           
                            END
                                --Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  

                                --if @InstrumentType = 5                  
                                --BEGIN                  
                                --    Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)                   
                                --    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                --END                  

                                -- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                --Select @JournalPK = ISNULL(JournalPK,0) + 1 from Journal                  

                                -- T0              
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                                

                                --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                   
                                --    @PReference,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                            

                                --        Select  @JournalPK,1,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                       

                                --        Select @JournalPK,2,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2  


                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                           

                                --        Select  @JournalPK,3,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                         

                                --        Select @JournalPK,4,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2  
        
                            --END    


                            -- BUY REKSADANA
    
                            if @PTrxType = 1 and @PInstrumentTypePK = 4                 
                            BEGIN             

                                IF(@ClientCode = '09')
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CP',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CP',@CurReference out
                                END


                                Select @InvestmentAcc = InvInReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana 
                                From AccountingSetup where Status in (1,2)                     

                                Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  

                                -- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal  

                                -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                     

                                    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                
                                    @CurReference,'T0 REKSADANA BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

                                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'D',@PAmount,                   
                                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

                                        Select @JournalPK,2,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'C',@PAmount,                   
                                        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                


                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CP' and status not in (3,4)

	

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP','','SUBSCRIPTION REKSADANA : ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		                                    


              


                            END   

                           --SELL REKSADANA

                            if @PTrxType = 2 and @PInstrumentTypePK = 4                  
                            BEGIN           
        	      
                                Select @InvestmentAcc = InvInReksadana,@RealisedAccReksadana = RealisedReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana 
                                From AccountingSetup where Status in (1,2)                

                                Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                     
                                Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  

                                select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
                                select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PPeriodPK and Type = 'CR' and status not in (3,4)
          
                                IF(@ClientCode = '09') 
                                BEGIN
                                    exec getJournalReference_09 @PSettledDate,'CR',@CurReference out
                                END
                                ELSE IF(@ClientCode = '21')    
                                BEGIN
                                    exec getJournalReference @PSettledDate,'GJ',@CurReference out
                                END
                                ELSE
                                BEGIN
                                    exec getJournalReference @PSettledDate,'CR',@CurReference out
                                END



                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,@CashierID,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR','','REDEMPTION REKSADANA: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  

        
                                -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
                                select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

                                -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

                                    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
                                    @CurReference,'T0 REKSADANA SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 


                                -- Gain Realised
                                if @PRealisedAmount >= 0              
                                Begin              
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[DepartmentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

                                        Select @JournalPK,1,1,2,@RealisedAccReksadana,1,@PInstrumentPK,3,'T0 REKSADANA SELL: ' + @InstrumentID,'C',abs(@PRealisedAmount),                   
                                        0,abs(@PRealisedAmount),1,0,abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @RealisedAccReksadana and Status = 2                  

                                    set  @InvestmentShareAmount = @PAmount - abs(@PRealisedAmount)            
                                End       

                                ---- Loss Realised
                                if @PRealisedAmount < 0              
                                begin              
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [DepartmentPK],               
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

                                        Select  @JournalPK,1,1,2,@RealisedAccReksadana,1,@PInstrumentPK,3,'T0 REKSADANA SELL: ' + @InstrumentID,'D',abs(@PRealisedAmount),                   
                                        abs(@PRealisedAmount),0,1,abs(@PRealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedAccReksadana and Status = 2                 

                                    set  @InvestmentShareAmount = @PAmount + abs(@PRealisedAmount)              
                                end              

                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

                                        Select @JournalPK,2,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'C',abs(@InvestmentShareAmount),                   
                                        0,abs(@InvestmentShareAmount),1,0,abs(@InvestmentShareAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2               


                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

                                        Select  @JournalPK,3,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'D',isnull(@PAmount,0),                   
                                        isnull(@PAmount,0),0,1,isnull(@PAmount,0),0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2                

                            END  

	                        update TrxPortfolio    
	                        set PostedBy = @UsersID,PostedTime = @LastUpdate,Posted = 1,Lastupdate = @LastUpdate    
	                        where TrxPortfolioPK = @TrxPortfolioPK and Status = 2 
	             
	          
                        Fetch next From A                   
	                        Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	                        @PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	                        @PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	                        @PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate,@PCurrencyPK            
                        END                  
                        Close A                  
                        Deallocate A

                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<ReferenceFromTrxPortfolio> Reference_SelectFromTrxPortfolio(DateTime _valueDate, int _periodPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceFromTrxPortfolio> L_TrxPortfolio = new List<ReferenceFromTrxPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @"select Distinct A.Reference,A.RefNo from    
                        (    
                        Select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo  
                        From TrxPortfolio where status not in (3,4)and Posted = 0 and PeriodPK = @PeriodPK  and Reference like '%INV%'  
                        Union All    
                        select Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from cashierReference where  left(right(reference,4),2) =  month(@ValueDate)   
                        and reference in ( select distinct reference from TrxPortfolio where status not in (3,4)and posted = 0 and Reference like '%INV%' ) and PeriodPK = @PeriodPK  and Type = 'INV'   
                        ) A  
                        order By A.Refno 
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceFromTrxPortfolio M_TrxPortfolio = new ReferenceFromTrxPortfolio();
                                    M_TrxPortfolio.Reference = Convert.ToString(dr["Reference"]);
                                    L_TrxPortfolio.Add(M_TrxPortfolio);
                                }
                            }
                            return L_TrxPortfolio;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean Report_TrxPortfolioValuation(string _userID, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Create Table #Temp
                        (InstrumentPK int,
                        Balance numeric(19,2),
                        Date Datetime,
                        InterestPercent numeric(19,4),
                        MaturityDate Datetime,
                        Price numeric(19,4),
                        BankPK int,
                        BankBranchPK int
                        )

                        Insert Into #Temp(InstrumentPK,Balance,Date,InterestPercent,MaturityDate,Price,BankPK,BankBranchPK)

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date,0 InterestPercent,'' MaturityDate,0 Price,0 BankPK,0 BankBranchPK from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1  and trxType = 1 and status = 2  and InstrumentTypePK in (1,2,4,5,6,8,9,11,13,14,15)

                        Group By InstrumentPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1  and trxType = 2 and status = 2 and InstrumentTypePK in (1,2,4,5,6,8,9,11,13,14,15)

                        Group By InstrumentPK
                        )A

                        Group By A.InstrumentPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0


                        union all

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,'',InterestPercent,MaturityDate,1 Price,BankPK,BankBranchPK from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and MaturityDate > @Date and trxType in (1,3) and status = 2  and InstrumentTypePK in (3)

                        Group By InstrumentPK,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,'',InterestPercent,MaturityDate,BankPK,BankBranchPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and MaturityDate > @Date and trxType in (2) and status = 2 and InstrumentTypePK in (3)

                        Group By InstrumentPK,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK
                        )A

                        Group By A.InstrumentPK,InterestPercent,MaturityDate,BankPK,BankBranchPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0



                        select C.InstrumentTypePK,C.Name InstrumentTypeName,A.InstrumentPK,B.ID InstrumentID,sum(A.Balance) Balance,D.ID CurrencyID,
                        A.InterestPercent,A.MaturityDate,case when C.InstrumentTypePK = 5 then 1 else case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc(@Date,A.InstrumentPK) end end AvgPrice,isnull(E.ClosePriceValue,1) ClosePrice,
                        case when C.InstrumentTypePK = 5 then sum(A.Balance) else sum(case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc(@Date,A.InstrumentPK) end * A.Balance) end CostValue,sum(isnull(E.ClosePriceValue,1) * A.Balance) MarketValue,sum(isnull(E.ClosePriceValue,1) * A.Balance) - sum(case when C.InstrumentTypePK = 5 then 1 else case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc(@Date,A.InstrumentPK) end end * A.Balance) Unrealised from #Temp A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status = 2
                        left join ClosePrice E on A.InstrumentPK = E.InstrumentPK and E.Status = 2 and E.Date = @Date
                        left join 
                        (
                        select InstrumentPK,max(ValueDate) AcqDate from TrxPortfolio where posted = 1 and status = 2 and TrxType = 1  and InstrumentTypePK = 1
                        group by instrumentPK
                        )F on A.InstrumentPK = F.InstrumentPK
                        Group By C.InstrumentTypePK,C.Name,A.InstrumentPK,B.ID,D.ID,
                        A.InterestPercent,A.MaturityDate,E.ClosePriceValue,A.Date
                        having sum(A.Balance) <> 0";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "TrxPortfolioValuationRpt" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "AccountingReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Trx Portfolio");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundPortfolio> rList = new List<FundPortfolio>();
                                    while (dr0.Read())
                                    {
                                        FundPortfolio rSingle = new FundPortfolio();
                                        //rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                        rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                        rSingle.AvgPrice = Convert.ToDecimal(dr0["AvgPrice"]);
                                        rSingle.Balance = Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.CostValue = Convert.ToDecimal(dr0["CostValue"]);
                                        rSingle.ClosePrice = Convert.ToDecimal(dr0["ClosePrice"]);
                                        rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"]);
                                        rSingle.Unrealised = Convert.ToDecimal(dr0["Unrealised"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                        from r in rList
                                        orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                        group r by new { r.FundName, r.InstrumentTypeName, r.Date, } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 2].Value = "DATE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _date;
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "INSTRUMENT TYPE : ";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.InstrumentTypeName;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                        incRowExcel++;

                                        //Row B = 3 fungsinya untuk bikin garis
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;
                                        string _range = "";
                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            //if (rsHeader.Key.InstrumentTypeName != "Equity Reguler")
                                            //{
                                            //worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            //}
                                            //else
                                            //{
                                            worksheet.Cells[incRowExcel, 3].Value = "AVG PRICE";
                                            //}
                                            worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 6].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL AMOUNT";
                                            worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            worksheet.Cells[incRowExcel, 5].Value = "INTEREST %";

                                            _range = "A" + incRowExcel + ":E" + incRowExcel;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            //if (rsHeader.Key.InstrumentTypeName != "Equity Reguler")
                                            //{
                                            //worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            //}
                                            //else
                                            //{
                                            worksheet.Cells[incRowExcel, 3].Value = "AVG PRICE";
                                            //}
                                            worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 6].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;

                                        }
                                        else
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "NOMINAL FACE VALUE";
                                            worksheet.Cells[incRowExcel, 4].Value = "AVG COST";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST (IDR)";
                                            worksheet.Cells[incRowExcel, 6].Value = "TERM OF INTEREST";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        }


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

                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            //worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                            //worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                            if (rsDetail.InstrumentTypePK == 1)
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }
                                            else if (rsDetail.InstrumentTypePK == 5 || rsDetail.InstrumentTypePK == 10)
                                            {

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (rsDetail.InstrumentTypePK == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = (rsDetail.CostValue) / 100;
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Accrual;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Value = (rsDetail.MarketValue) / 100;
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = (rsDetail.Unrealised) / 100;
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }


                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;


                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
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


                                            //worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }
                                        else
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }




                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                        }


                                        incRowExcel = incRowExcel + 2;



                                    }

                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeDetail = "A:J";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(11).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).Width = 30;
                                    //worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 TRX PORTFOLIO";



                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
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

        public Boolean Report_TrxPortfolioValuationByAccount(string _userID, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                       Create Table #Temp
                        (InstrumentPK int,
                        Balance numeric(19,2),
                        Date Datetime,
                        InterestPercent numeric(19,4),
                        MaturityDate Datetime,
                        Price numeric(19,4),
                        BankPK int,
                        BankBranchPK int,
						CompanyAccountTradingPK int,
                        )

                        Insert Into #Temp(InstrumentPK,Balance,Date,InterestPercent,MaturityDate,Price,BankPK,BankBranchPK,CompanyAccountTradingPK)

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date,0 InterestPercent,'' MaturityDate,0 Price,0 BankPK,0 BankBranchPK,CompanyAccountTradingPK from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,CompanyAccountTradingPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1  and trxType = 1 and status = 2  and InstrumentTypePK in (1,2,4,5,6,8,9,11,13,14,15)

                        Group By InstrumentPK,CompanyAccountTradingPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,CompanyAccountTradingPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1  and trxType = 2 and status = 2 and InstrumentTypePK in (1,2,4,5,6,8,9,11,13,14,15)

                        Group By InstrumentPK,CompanyAccountTradingPK
                        )A

                        Group By A.InstrumentPK,CompanyAccountTradingPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0


                        union all

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,'',InterestPercent,MaturityDate,1 Price,BankPK,BankBranchPK,CompanyAccountTradingPK from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK,CompanyAccountTradingPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and MaturityDate > @Date and trxType in (1,3) and status = 2  and InstrumentTypePK in (3)

                        Group By InstrumentPK,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK,CompanyAccountTradingPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,'',InterestPercent,MaturityDate,BankPK,BankBranchPK,CompanyAccountTradingPK from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and MaturityDate > @Date and trxType in (2) and status = 2 and InstrumentTypePK in (3)

                        Group By InstrumentPK,ValueDate,InterestPercent,MaturityDate,BankPK,BankBranchPK,CompanyAccountTradingPK
                        )A

                        Group By A.InstrumentPK,InterestPercent,MaturityDate,BankPK,BankBranchPK,CompanyAccountTradingPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0



                        select C.InstrumentTypePK,C.Name InstrumentTypeName,isnull(CAT.Name,'') CompanyAccountTradingName,A.InstrumentPK,B.ID InstrumentID,sum(A.Balance) Balance,D.ID CurrencyID,
                        A.InterestPercent,A.MaturityDate,case when C.InstrumentTypePK = 5 then 1 else case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc_ByCompanyAccountTradingPK(@Date,A.InstrumentPK,A.CompanyAccountTradingPK) end end AvgPrice,isnull(E.ClosePriceValue,1) ClosePrice,
                        case when C.InstrumentTypePK = 5 then sum(A.Balance) else sum(case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc(@Date,A.InstrumentPK) end * A.Balance) end CostValue,sum(isnull(E.ClosePriceValue,1) * A.Balance) MarketValue,sum(isnull(E.ClosePriceValue,1) * A.Balance) - sum(case when C.InstrumentTypePK = 5 then 1 else case when @ClientCode = '12' then dbo.FGetLastAvgFromInvestment_Acc_12(@Date,A.InstrumentPK) else dbo.FGetLastAvgFromInvestment_Acc_ByCompanyAccountTradingPK(@Date,A.InstrumentPK,A.CompanyAccountTradingPK) end end * A.Balance) Unrealised from #Temp A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status = 2
                        left join ClosePrice E on A.InstrumentPK = E.InstrumentPK and E.Status = 2 and E.Date = @Date
						left join CompanyAccountTrading CAT on CAT.CompanyAccountTradingPK = A.CompanyAccountTradingPK and CAT.Status in (1,2)
                        left join 
                        (
                        select InstrumentPK,max(ValueDate) AcqDate from TrxPortfolio where posted = 1 and status = 2 and TrxType = 1  and InstrumentTypePK = 1
                        group by instrumentPK
                        )F on A.InstrumentPK = F.InstrumentPK
                        Group By C.InstrumentTypePK,C.Name,A.InstrumentPK,B.ID,D.ID,
                        A.InterestPercent,A.MaturityDate,E.ClosePriceValue,A.Date,CAT.Name,A.CompanyAccountTradingPK
                        having sum(A.Balance) <> 0";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "TrxPortfolioValuationByAccountRpt" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "AccountingReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Trx Portfolio");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundPortfolio> rList = new List<FundPortfolio>();
                                    while (dr0.Read())
                                    {
                                        FundPortfolio rSingle = new FundPortfolio();
                                        //rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                        rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"]);
                                        rSingle.CompanyAccountTradingName = Convert.ToString(dr0["CompanyAccountTradingName"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                        rSingle.AvgPrice = Convert.ToDecimal(dr0["AvgPrice"]);
                                        rSingle.Balance = Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.CostValue = Convert.ToDecimal(dr0["CostValue"]);
                                        rSingle.ClosePrice = Convert.ToDecimal(dr0["ClosePrice"]);
                                        rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"]);
                                        rSingle.Unrealised = Convert.ToDecimal(dr0["Unrealised"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                        from r in rList
                                        orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                        group r by new { r.FundName, r.InstrumentTypeName, r.Date, r.CompanyAccountTradingName, } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 2].Value = "DATE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _date;
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "INSTRUMENT TYPE : ";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.InstrumentTypeName;

                                        incRowExcel++;
                                        worksheet.Cells["B" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "COMPANY ACCOUNT TRADING: " + rsHeader.Key.CompanyAccountTradingName;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                        incRowExcel++;

                                        //Row B = 3 fungsinya untuk bikin garis
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;
                                        string _range = "";
                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            //if (rsHeader.Key.InstrumentTypeName != "Equity Reguler")
                                            //{
                                            //worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            //}
                                            //else
                                            //{
                                            worksheet.Cells[incRowExcel, 3].Value = "AVG PRICE";
                                            //}
                                            worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 6].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL AMOUNT";
                                            worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            worksheet.Cells[incRowExcel, 5].Value = "INTEREST %";

                                            _range = "A" + incRowExcel + ":E" + incRowExcel;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            //if (rsHeader.Key.InstrumentTypeName != "Equity Reguler")
                                            //{
                                            //worksheet.Cells[incRowExcel, 4].Value = "MATURITY DATE";
                                            //}
                                            //else
                                            //{
                                            worksheet.Cells[incRowExcel, 3].Value = "AVG PRICE";
                                            //}
                                            worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 6].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;

                                        }
                                        else
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "NOMINAL FACE VALUE";
                                            worksheet.Cells[incRowExcel, 4].Value = "AVG COST";
                                            worksheet.Cells[incRowExcel, 5].Value = "COST (IDR)";
                                            worksheet.Cells[incRowExcel, 6].Value = "TERM OF INTEREST";
                                            worksheet.Cells[incRowExcel, 7].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNREALISED";
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        }


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

                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            //worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                            //worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                            if (rsDetail.InstrumentTypePK == 1)
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(C" + incRowExcel + "*D" + incRowExcel + ")";

                                                //worksheet.Cells[incRowExcel, 5].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Formula = "SUM(G" + incRowExcel + "-E" + incRowExcel + ")";
                                                //worksheet.Cells[incRowExcel, 8].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }
                                            else if (rsDetail.InstrumentTypePK == 5 || rsDetail.InstrumentTypePK == 10)
                                            {

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (rsDetail.InstrumentTypePK == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 5].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(C" + incRowExcel + "*D" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 8].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 8].Formula = "SUM(G" + incRowExcel + "-E" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(C" + incRowExcel + "*D" + incRowExcel + ")/100";
                                                //worksheet.Cells[incRowExcel, 5].Value = (rsDetail.CostValue) / 100;
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Accrual;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Value = (rsDetail.MarketValue) / 100;
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 8].Value = (rsDetail.Unrealised) / 100;
                                                worksheet.Cells[incRowExcel, 8].Formula = "SUM(G" + incRowExcel + "-E" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }


                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;


                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
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


                                            //worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }
                                        else
                                        {
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

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }




                                        if (rsHeader.Key.InstrumentTypeName == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Deposito Money Market")
                                        {
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        }
                                        else if (rsHeader.Key.InstrumentTypeName == "Mutual Fund")
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                        }


                                        incRowExcel = incRowExcel + 2;



                                    }

                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeDetail = "A:J";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(11).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).Width = 30;
                                    //worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 TRX PORTFOLIO";



                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
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

        public void TrxPortfolio_PostingWithoutBankBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
declare @PValueDate datetime
                            declare @PSettledDate datetime
                            declare @PMaturityDate datetime
                            declare @PPeriodPK int
                            declare @PInstrumentPK int
                            declare @PCounterpartPK int
                            declare @PTrxType int
                            declare @PReference nvarchar(50)
                            declare @InstrumentID nvarchar(50)
                            declare @PInstrumentTypePK int
                            declare @InstrumentType int
                            declare @PCashRefPK int
                            declare @PVolume numeric (22,4)
                            declare @PBrokerageFeeAmount numeric (22,4)
                            declare @PLevyAmount numeric (22,4)
                            declare @PKPEIAmount numeric (22,4)                 
                            declare @PVATAmount numeric (22,4)
                            declare @PWHTAmount numeric (22,4)
                            declare @POTCAmount numeric (22,4)
                            declare @PIncomeTaxSellAmount numeric (22,4)
                            declare @PIncomeTaxInterestAmount numeric (22,4)
                            declare @PIncomeTaxGainAmount numeric (22,4)
                            declare @PNetAmount numeric (22,4)
                            declare @PRealisedAmount numeric (22,4)
                            declare @PInterestAmount numeric (22,4)

                            declare @InvestmentAcc int
                            declare @PayablePurchaseAcc int
                            declare @ReceivableSaleAcc int
                            declare @CommissionAcc int
                            declare @LevyAcc int
                            declare @VatAcc int
                            declare @WhtAcc int
                            declare @CashAtBankAcc int
                            declare @RealisedAcc int
                            declare @IncomeSaleTaxAcc int
                            declare @InterestRecAcc int
                            declare @journalPK int
                            declare @TrxPortfolioPK int
                            declare @TotalFeeAcc int
                            declare @RealisedAccReksadana int
                            declare @PCurrencyPK int
 
                            declare @PAmount numeric(22,4)
                            declare @FPayablePurchaseAmount numeric (22,4)
                        
   
                            declare @CashierPK int
                            declare @CashierID int
	                        Declare @CurReference nvarchar(100)  


declare @SwitchingFundAcc int

declare @InvestmentShareAmount numeric (22,4)
	          
Declare A Cursor For                  
	Select A.TrxPortfolioPK,A.ValueDate,A.PeriodPK,A.Reference,A.TrxType,A.CounterpartPK,A.InstrumentPK,                   
	A.CashRefPK,A.InterestAmount,A.SettledDate,A.Volume,A.Amount,                  
	A.BrokerageFeeAmount,A.LevyAmount,A.KPEIAmount,A.VATAmount,A.WHTAmount,A.OTCAmount,A.IncomeTaxSellAmount,                  
	A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount,A.NetAmount,A.InstrumentTypePK,A.RealisedAmount,A.MaturityDate,B.CurrencyPK            
	From TrxPortfolio A
    left join CashRef B on A.CashRefPK = B.CashRefPK and B.status = 2        
	Where A.status = 2 and A.Posted = 0 and A.Revised = 0 and A.ValueDate between @DateFrom and @DateTo and A.Selected = 1 and A.InstrumentTypePK = 4             
	Open A                  
	Fetch Next From A                  
	Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	@PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	@PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	@PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate,@PCurrencyPK                 
While @@FETCH_STATUS = 0                  
Begin                  
	Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK 
		      				               
    -- BUY REKSADANA
    
    if @PTrxType = 1 and @PInstrumentTypePK = 4                 
    BEGIN            
        IF(@ClientCode = '21')    
        BEGIN
            exec getJournalReference @PSettledDate,'GJ',@CurReference out
        END
        ELSE
        BEGIN
            exec getJournalReference @PSettledDate,'CP',@CurReference out
        END
       

        Select @InvestmentAcc = InvInReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana,@SwitchingFundAcc = SwitchingFundAcc 
        From AccountingSetup where Status in (1,2)                                 

        -- Setup Account kelar diatas, Next masukin ke Fund Journal 
        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal  

        -- T0                  
		INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                     

            Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                
            @CurReference,'T0 REKSADANA BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

                Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'D',@PAmount,                   
                @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

                Select @JournalPK,2,1,2,@SwitchingFundAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'C',@PAmount,                   
                0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @SwitchingFundAcc and Status = 2                


        


    END   



    --SELL REKSADANA

    if @PTrxType = 2 and @PInstrumentTypePK = 4                  
    BEGIN           
        	      
        Select @InvestmentAcc = InvInReksadana,@RealisedAccReksadana = RealisedReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana ,@SwitchingFundAcc = SwitchingFundAcc
        From AccountingSetup where Status in (1,2)                
                
        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  

        select @PPeriodPK = PeriodPK from Period where @PSettledDate between DateFrom and DateTo and status = 2

        IF(@ClientCode = '21')    
        BEGIN
            exec getJournalReference @PSettledDate,'GJ',@CurReference out
        END
        ELSE
        BEGIN
            exec getJournalReference @PSettledDate,'CR',@CurReference out
        END          


        
        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

        -- T0                  
		INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

            Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
            @CurReference,'T0 REKSADANA SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 


        -- Gain Realised
        if @PRealisedAmount >= 0              
        Begin              
                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[DepartmentPK],                
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

                Select @JournalPK,1,1,2,@RealisedAccReksadana,1,@PInstrumentPK,3,'T0 REKSADANA SELL: ' + @InstrumentID,'C',@PRealisedAmount,                   
                0,@PRealisedAmount,1,0,@PRealisedAmount,@LastUpdate From Account Where AccountPK = @RealisedAccReksadana and Status = 2                  

            set  @InvestmentShareAmount = @PAmount - @PRealisedAmount              
        End       

        ---- Loss Realised
        if @PRealisedAmount < 0              
        begin              
                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK], [DepartmentPK],               
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

                Select  @JournalPK,1,1,2,@RealisedAccReksadana,1,@PInstrumentPK,3,'T0 REKSADANA SELL: ' + @InstrumentID,'D',abs(@PRealisedAmount),                   
                abs(@PRealisedAmount),0,1,abs(@PRealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedAccReksadana and Status = 2                 

            set  @InvestmentShareAmount = @PAmount + abs(@PRealisedAmount)              
        end              

                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

                Select @JournalPK,2,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'C',abs(@InvestmentShareAmount),                   
                0,abs(@InvestmentShareAmount),1,0,abs(@InvestmentShareAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2               


                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

                Select  @JournalPK,3,1,2,@SwitchingFundAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'D',isnull(@PAmount,0),                   
                isnull(@PAmount,0),0,1,isnull(@PAmount,0),0,@LastUpdate From Account Where AccountPK = @SwitchingFundAcc and Status = 2                

   
                                

    END  


	update TrxPortfolio    
	set PostedBy = @UsersID,PostedTime = @LastUpdate,Posted = 1,Lastupdate = @LastUpdate    
	where TrxPortfolioPK = @TrxPortfolioPK and Status = 2 
	             
	          
Fetch next From A                   
	Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	@PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	@PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	@PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate,@PCurrencyPK            
END                  
Close A                  
Deallocate A


   

                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean Validate_CheckAvailableInstrument(TrxPortfolio _trxPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
        
                            declare @CurrBalance numeric(19,4)

                            Create Table #tmp

                            (  
	                            [InstrumentPK] INTEGER NOT NULL,  
	                            [LastVolume] [numeric](18, 4) NOT NULL,                     
                            )


                            Insert into #tmp
                            Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume from (
	                            select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio
	                            where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 1 and status = 2 and Revised = 0 and InstrumentPK = @InstrumentPK
	                            Group By InstrumentPK
	                            UNION ALL
	                            select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio
	                            where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 2 and status = 2 and Revised = 0 and InstrumentPK = @InstrumentPK
	                            Group By InstrumentPK
	                            UNION ALL 
	                            select InstrumentPK,sum(Balance) BuyVolume,0 SellVolume from CorporateActionResult 
	                            where Date = @Date and FundPK = 9999 and InstrumentPK = @InstrumentPK
	                            Group By InstrumentPK
                            )A
                            Group By A.InstrumentPK
                            having sum(A.BuyVolume) - sum(A.SellVolume) > 0



                            Insert into #tmp
                            Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Volume from (
	                            select A.InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2
	                            where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK in (2,3,8,9,11,13,14,15) 
	                            and trxType = 1 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date and A.InstrumentPK = @InstrumentPK
	                            Group By A.InstrumentPK
	                            UNION ALL
	                            select A.InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio A
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2
	                            where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK  in (2,3,8,9,11,13,14,15)  
	                            and trxType = 2 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date and A.InstrumentPK = @InstrumentPK
	                            Group By A.InstrumentPK
                            )A 
                            Group By A.InstrumentPK
                            having sum(A.BuyVolume) - sum(A.SellVolume) > 0



                            Insert into #tmp                     
                            Select A.InstrumentPK,isnull(sum(A.BuyAmount) - sum(A.SellAmount),0) LastAmount from (
	                            select InstrumentPK,sum(Amount) BuyAmount,0 SellAmount from trxPortfolio
	                            where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 
	                            and trxType in (1,3) and Revised = 0 and status = 2 and InstrumentPK = @InstrumentPK
	                            Group By InstrumentPK
	                            UNION ALL
	                            select InstrumentPK,0 BuyAmount,sum(Amount) SellAmount from trxPortfolio
	                            where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 
	                            and trxType = 2 and Revised = 0 and status = 2 and InstrumentPK = @InstrumentPK
	                            Group By InstrumentPK,MaturityDate
                            )A
                            Group By A.InstrumentPK
                            having sum(A.BuyAmount) - sum(A.SellAmount) > 0


                            Insert into #tmp
                            Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume
                            from (
                            select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio
                            where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 1 and Revised = 0 and status = 2 and InstrumentPK = @InstrumentPK
                            Group By InstrumentPK
                            UNION ALL
                            select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio
                            where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 2 and Revised = 0 and status = 2 and InstrumentPK = @InstrumentPK
                            Group By InstrumentPK
                            )A
                            Group By A.InstrumentPK
                            having sum(A.BuyVolume) - sum(A.SellVolume) > 1

                            select @CurrBalance = sum(LastVolume) from #tmp

                            if @Balance > 0
                            BEGIN
	                            if @Balance > @CurrBalance
	                            Begin
		                            Select 1 Result
		                            return
	                            end
	                            else
	                            begin
		                            select 0 Result
		                            return
	                            end
                            END
                           ";

                        cmd.Parameters.AddWithValue("@date", _trxPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _trxPortfolio.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Balance", _trxPortfolio.Volume);
                        //cmd.Parameters.AddWithValue("@Amount", _trxPortfolio.Amount);

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






    }
}