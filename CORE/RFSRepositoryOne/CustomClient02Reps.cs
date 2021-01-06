using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;using RFSRepository;
using OfficeOpenXml.Drawing.Chart;
using System.Xml;


namespace RFSRepositoryOne
{
    public class CustomClient02Reps
    {
        Host _host = new Host();

        public class AccountActivityByCounterpart
        {
            public decimal StartBalance { get; set; }
            public int journalPK { get; set; }
            public DateTime ValueDate { get; set; }
            public string Reference { get; set; }
            public int RefNo { get; set; }
            public string AccountID { get; set; }
            public string AccountName { get; set; }
            public int AccountType { get; set; }
            public string DetailDescription { get; set; }
            public string DebitCredit { get; set; }
            public decimal Amount { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
            public decimal Rate { get; set; }
            public decimal BaseDebit { get; set; }
            public decimal BaseCredit { get; set; }
            public string CurrencyID { get; set; }
            public string OfficeID { get; set; }
            public string DepartmentID { get; set; }
            public string AgentID { get; set; }
            public string ConsigneeID { get; set; }
            public string InstrumentID { get; set; }
            public string CounterpartID { get; set; }
            public string CheckedBy { get; set; }
            public string ApprovedBy { get; set; }

        }


        private class PortfolioValuationReport
        {
            public string SecurityCode { get; set; }
            public string SecurityDescription { get; set; }
            public string TimeDeposit { get; set; }
            public string BICode { get; set; }
            public string Branch { get; set; }
            public string InstrumentTypeName { get; set; }
            public decimal QtyOfUnit { get; set; }
            public decimal Lot { get; set; }
            public decimal AverageCost { get; set; }
            public decimal BookValue { get; set; }
            public decimal MarketPrice { get; set; }
            public decimal MarketValue { get; set; }
            public decimal UnrealizedProfitLoss { get; set; }
            public decimal PercentFR { get; set; }
            public decimal Nominal { get; set; }
            public decimal RateGross { get; set; }
            public decimal AccIntTD { get; set; }
            public string TradeDate { get; set; }
            public string MaturityDate { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public int InstrumentTypePK { get; set; }
            public string Date { get; set; }
        }


         private class TrxSummary
        {
            public string InstrumentID { get; set; }
            public string InstrumentTypeName { get; set; }
            public string TrxTypeID { get; set; }
            public DateTime ValueDate { get; set; }
            public DateTime SettlementDate { get; set; }
            public string MaturityDate { get; set; }
            public string CounterpartCode { get; set; }
            public string InstrumentName { get; set; }
            public decimal DoneVolume { get; set; }
            public decimal DonePrice { get; set; }
            public decimal DoneAmount { get; set; }
            public decimal TotalBrokerFee { get; set; }
            public decimal WHTAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal AcqPrice { get; set; }
            public decimal Realised { get; set; }
            public decimal DoneAccruedInterest { get; set; }
            public decimal InterestPercent { get; set; }
            public string BankName { get; set; }
            public int Tenor { get; set; }
            public string FundName { get; set; }

        }

         private class PencairanDeposito
         {
             public string BankCustodian { get; set; }
             public string Address { get; set; }
             public string Attn1 { get; set; }
             public string PhoneAttn1 { get; set; }
             public string FaxAttn1 { get; set; }
             public string Attn2 { get; set; }
             public string PhoneAttn2 { get; set; }
             public string CC1 { get; set; }
             public string PhoneCC1 { get; set; }
             public string CC2 { get; set; }
             public string PhoneCC2 { get; set; }
             public string Remark { get; set; }
             public string Periode { get; set; }
             public string NamaBank { get; set; }
             public decimal Jumlah { get; set; }
             public decimal Rate { get; set; }
             public string Kota { get; set; }
             public string Reference { get; set; }
             public string Instruksi { get; set; }
             public string Currency { get; set; }
             public string NoRek { get; set; }
             public string Fund { get; set; }
             public string BankPlacement { get; set; }
         }

         private class PlacementDeposito
         {
             public string BankCustodian { get; set; }
             public string Address { get; set; }
             public string Attn1 { get; set; }
             public string PhoneAttn1 { get; set; }
             public string FaxAttn1 { get; set; }
             public string Attn2 { get; set; }
             public string PhoneAttn2 { get; set; }
             public string CC1 { get; set; }
             public string PhoneCC1 { get; set; }
             public string PhoneCC2 { get; set; }
             public string FaxCC1 { get; set; }
             //public string Periode { get; set; }
             public string NamaBank { get; set; }
             public decimal Jumlah { get; set; }
             public decimal Rate { get; set; }
             public string Kota { get; set; }
             public string Reference { get; set; }
             public string Instruksi { get; set; }
             public string Currency { get; set; }
             public string NomorRekeningInstrument { get; set; }
             public string NamaRekeningInstrument { get; set; }
             public string NomorRekeningFund { get; set; }
             public string NamaRekeningFund { get; set; }
             public string Fund { get; set; }
             public string BankPlacement { get; set; }
             public string BICCode { get; set; }
             public string Cabang { get; set; }
             public string RefNo { get; set; }
             public string PeriodID { get; set; }
             public string Month { get; set; }
             public string ValueDate { get; set; }
             public string MaturityDate { get; set; }

             public string BranchName { get; set; }
         }

         private class PerpanjanganDeposito
         {
             public string BankCustodian { get; set; }
             public string Address { get; set; }
             public string Address2 { get; set; }
             public string Address3 { get; set; }
             public string Attn1 { get; set; }
             public string PhoneAttn1 { get; set; }
             public string ext { get; set; }
             public string ext1 { get; set; }
             public string Fax { get; set; }
             public string Attn2 { get; set; }
             public string PhoneAttn2 { get; set; }
             public string CC1 { get; set; }
             public string PhoneCC1 { get; set; }
             public string CC2 { get; set; }
             public string PhoneCC2 { get; set; }
             public string Remark { get; set; }
             public string Periode { get; set; }
             public string NamaBank { get; set; }
             public decimal Jumlah { get; set; }
             public decimal Rate { get; set; }
             public string Kota { get; set; }
             public string Reference { get; set; }
             public string Instruksi { get; set; }
             public string Currency { get; set; }
             public string NoRek { get; set; }
             public string Fund { get; set; }
             public string BankPlacement { get; set; }
             public string KodeBIC { get; set; }
         }

         private class SummaryPerCabang
         {
             public string TransactionType { get; set; }
             public string Tahun { get; set; }
             public string Cabang { get; set; }
             public string Bulan { get; set; }
             public string Fund { get; set; }
             public string LastMonth { get; set; }
             public decimal NominalNC { get; set; }
             public decimal NominalTopUp { get; set; }
             public decimal CountNC { get; set; }
             public decimal CountTopUp { get; set; }
         }


         public class LaporanSaham
         {
             public DateTime Trade { get; set; }
             public DateTime Settle { get; set; }
             public string Type { get; set; }
             public string InstrumentID { get; set; } 
             public decimal Quantity { get; set; }
             public decimal CostPrice { get; set; }
             public decimal TotalCost { get; set; }
             public decimal SellPrice { get; set; }
             public decimal Proceed { get; set; }
             public decimal GainLoss { get; set; }
             public decimal Total { get; set; }
             public decimal BegBalanceVolume { get; set; }
             public decimal BegBalanceCostPrice { get; set; }
             public decimal BegBalanceTotalCost { get; set; }
             public string FundName { get; set; }
             public string CounterpartID { get; set; } 


         }

         public class PVRRpt
         {
             public int Row { get; set; }
             public decimal Balance { get; set; }
             public string FundName { get; set; }
             public string InstrumentID { get; set; }
             public string InstrumentName { get; set; }

             public decimal UnitQuantity { get; set; }
             public decimal AverageCost { get; set; }
             public decimal BookValue { get; set; }
             public decimal MarketPrice { get; set; }
             public decimal MarketValue { get; set; }
             public decimal UnrealizedProfitLoss { get; set; }
             public decimal PercentProfilLoss { get; set; }
             public decimal Lot { get; set; }
             public string MarketCap { get; set; }
             public string Sector { get; set; }
             public string MaturityDate { get; set; }
             public string BondType { get; set; }
             public string TimeDeposit { get; set; }
             public string BICode { get; set; }
             public string Branch { get; set; }
             public string TradeDate { get; set; }
             public decimal Nominal { get; set; }
             public decimal Rate { get; set; }
             public decimal AccTD { get; set; }
         }

         public class BrokerStock
         {
             public int CounterpartPK { get; set; }
             public string CounterpartID { get; set; }
             public string CounterpartName { get; set; }
             public decimal Buy { get; set; }
             public decimal Sell { get; set; }
             public decimal Total { get; set; }
             public decimal MTD { get; set; }

         }

         private class FundFactSheet
         {
             public string TujuanStrategiInvestasi { get; set; }
             public string InformasiProduk { get; set; }
             public string ManajerInvestasi { get; set; }
             public string MarketReview { get; set; }
             public string UngkapanSanggahan { get; set; }
             public DateTime TanggalPerdana { get; set; }

             public string FaktorResikoYangUtama { get; set; }
             public string ManfaatInvestasi { get; set; }
             public decimal NilaiAktivaBersih { get; set; }
             public decimal TotalUnitPenyertaan { get; set; }
             public decimal NilaiAktivaBersihUnit { get; set; }
             public decimal ImbalJasaManajerInvestasi { get; set; }
             public decimal ImbalJasaBankKustodian { get; set; }
             public decimal BiayaPembelian { get; set; }
             public decimal BiayaPenjualan { get; set; }
             public decimal BiayaPengalihan { get; set; }
             public string BankKustodianID { get; set; }
             public string FundID { get; set; }
             public string FundName { get; set; }
             public string Path { get; set; }
             public string BankAccountID { get; set; }
             public string KebijakanInvestasi { get; set; }

         }

         private class DailyComplianceReport
         {
             public string Date { get; set; }
             public string FundID { get; set; }
             public string FundName { get; set; }
             public decimal Amount { get; set; }
             public decimal NAVPercent { get; set; }
             public string Type { get; set; }
             public int InstrumentType { get; set; }
             public string InstrumentID { get; set; }
             public decimal TotalAUM { get; set; }

             public decimal DepositoAmount { get; set; }
             public decimal DEPPercentOfNav { get; set; }
             public decimal BondAmount { get; set; }
             public decimal BondPercentOfNav { get; set; }
             public decimal EquityAmount { get; set; }
             public decimal EQPercentOfNav { get; set; }
             public decimal TotalPercent { get; set; }
         }

         private decimal Get_TotalBuySellByCountepartForBrokerStockRpt(int _counterpartPK, string _paramFund, DateTime _dateFrom, DateTime _dateTo)
         {

             try
             {

                 using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                 {
                     DbCon.Open();
                     DateTime _dateTimeNow = DateTime.Now;
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {

                         cmd.CommandText = @"

                        select sum(A.DoneAmount) Result
                        from Investment A 
                        where A.StatusSettlement = 2 and A.ValueDate between @DateFrom and @DateTo and A.InstrumentTypePK = 1 and A.CounterpartPK  = @CounterpartPK " + _paramFund;

                         cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                         cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                         cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
                         using (SqlDataReader dr = cmd.ExecuteReader())
                         {
                             if (dr.HasRows)
                             {
                                 dr.Read();
                                 return Convert.ToDecimal(dr["Result"]);

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

         private decimal Get_TotalBuySellForBrokerStockRpt(string _paramFund, DateTime _dateFrom, DateTime _dateTo)
         {

             try
             {

                 using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                 {
                     DbCon.Open();
                     DateTime _dateTimeNow = DateTime.Now;
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {

                         cmd.CommandText = @"

                        select sum(A.DoneAmount) Result
                        from Investment A 
                        where A.StatusSettlement = 2 and A.ValueDate between @DateFrom and @DateTo and A.InstrumentTypePK = 1 " + _paramFund;

                         cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                         cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                         using (SqlDataReader dr = cmd.ExecuteReader())
                         {
                             if (dr.HasRows)
                             {
                                 dr.Read();
                                 return Convert.ToDecimal(dr["Result"]);

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
        public string FundClient_GenerateNewClientID(int _clientCategory, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //RCORE
                        cmd.CommandText = @" 							
                                Declare @NewClientID  nvarchar(100)    
                                Declare @MaxClientID  int
                                                
                                select @MaxClientID =   max(convert(int,right(ID,4)))  + 1 from FundClient where  status in (1,2) and FundClientPK > 80
                                and left(ID,2) =  cast(right(year(getdate()),2) as nvarchar(2))

                                select @maxClientID = isnull(@MaxClientID,1)
					
                                declare @LENdigit int

                                select @LENdigit = LEN(@maxClientID) 
							
                                If @LENdigit = 1
                                BEGIN
	                                set @NewClientID =   cast(right(year(getdate()),2) as nvarchar(2)) + '000' + CAST(@MaxClientID as nvarchar) 
                                END
                                If @LENdigit = 2
                                BEGIN
	                                set @NewClientID =   cast(right(year(getdate()),2) as nvarchar(2)) + '00' + CAST(@MaxClientID as nvarchar) 
                                END
                                If @LENdigit = 3
                                BEGIN
	                                set @NewClientID =   cast(right(year(getdate()),2) as nvarchar(2)) + '0' + CAST(@MaxClientID as nvarchar) 
                                END
                                If @LENdigit = 4
                                BEGIN
	                                set @NewClientID =  cast(right(year(getdate()),2) as nvarchar(2))  + CAST(@MaxClientID as nvarchar) 
                                END

                                Select @NewClientID NewClientID
                       ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["NewClientID"]);
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
        public List<InstrumentForTrxPortfolio> Get_InstrumentTrxPortfolioByInstrumentTypePK(int _instrumentTypePK, int _trxType, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForTrxPortfolio> L_Instrument = new List<InstrumentForTrxPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_instrumentTypePK == 1)
                        {
                            if (_trxType == 1)
                            {
                                cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT 
                                on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                                where I.status = 2 and IT.Type in (1) order by I.ID ";
                            }
                            else
                            {
                                cmd.CommandText = @"Create Table #Temp
                                (InstrumentPK int,
                                Balance numeric(19,2),
                                Date Datetime
                                )

                                Insert Into #Temp(InstrumentPK,Balance,Date)
                                Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date from (

                                select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 1 and status = 2  and InstrumentTypePK = 1

                                Group By InstrumentPK

                                UNION ALL

                                select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 2 and status = 2 and InstrumentTypePK = 1

                                Group By InstrumentPK
                                )A

                                Group By A.InstrumentPK

                                having sum(A.BuyVolume) - sum(A.SellVolume) > 0


                                select A.InstrumentPK,A.Balance,B.ID + ' - ' + B.Name InstrumentID,C.ID CurrencyID,AcqDate from #Temp A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                left join Currency C on B.CurrencyPK = C.CurrencyPK and C.Status = 2
                                left join 
                                (
                                select InstrumentPK,max(ValueDate) AcqDate from TrxPortfolio where posted = 1 and status = 2 and TrxType = 1  and InstrumentTypePK = 1
                                group by instrumentPK
                                )D on A.InstrumentPK = D.InstrumentPK  ";
                            }

                        }
                        else if (_instrumentTypePK == 2)
                        {
                            if (_trxType == 1)
                            {
                                cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT 
                                on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                                where I.status = 2 and IT.Type in (2,5) ";
                            }
                            else
                            {
                                cmd.CommandText = @"Create Table #Temp
                                (InstrumentPK int,
                                Balance numeric(19,2),
                                Date Datetime
                                )

                                Insert Into #Temp(InstrumentPK,Balance,Date)
                                Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date from (

                                select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 1 and status = 2  and InstrumentTypePK in (2,3,8,9,11,13,14,15) 

                                Group By InstrumentPK

                                UNION ALL

                                select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 2 and status = 2 and InstrumentTypePK in (2,3,8,9,11,13,14,15) 

                                Group By InstrumentPK
                                )A

                                Group By A.InstrumentPK

                                having sum(A.BuyVolume) - sum(A.SellVolume) > 0


                                select A.InstrumentPK,A.Balance,B.ID + ' - ' + B.Name InstrumentID,C.ID CurrencyID,AcqDate from #Temp A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                left join Currency C on B.CurrencyPK = C.CurrencyPK and C.Status = 2
                                left join 
                                (
                                select InstrumentPK,max(ValueDate) AcqDate from TrxPortfolio where posted = 1 and status = 2 and TrxType = 1  and InstrumentTypePK in (2,3,8,9,11,13,14,15) 
                                group by instrumentPK
                                )D on A.InstrumentPK = D.InstrumentPK  ";
                            }
                        }
                        else if (_instrumentTypePK == 5)
                        {
                            if (_trxType == 1)
                            {
                                cmd.CommandText = @"select InstrumentPK,I.BankPK,I.ID + ' - ' + I.Name InstrumentID, I.Name,'' AcqDate, I.MaturityDate, I.InterestPercent,C.ID BankID,0 Balance, I.Category, D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT 
                                on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 Left join Bank C on I.BankPK = C.BankPK and C.status = 2 Left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                                where I.status = 2 and IT.Type = 3 and I.MaturityDate >= @Date";
                            }
                            else if (_trxType == 2)
                            {
                                cmd.CommandText = @"
                                select A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price,A.BankPK,A.BankBranchPK from (

                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate AcqDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @Date and A.status = 2 and posted = 1 and C.Type = 3 and A.MaturityDate > @Date
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                

                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and C.Type = 3 and TrxType  = 1 
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK

                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status not in (3,4) and C.Type = 3
                                and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                having A.Balance + sum(isnull(B.MovBalance,0)) <> 0  ";
                            }
                            else
                            {
                                cmd.CommandText = @"
                                select A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price,A.BankPK,A.BankBranchPK from (

                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate AcqDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @Date and A.status = 2 and posted = 1 and C.Type = 3 and A.MaturityDate = @Date
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                

                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and C.Type = 3 and TrxType  = 1  and A.MaturityDate = @Date
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK

                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status not in (3,4) and C.Type = 3 and A.MaturityDate = @Date
                                 and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                having A.Balance + sum(isnull(B.MovBalance,0)) <> 0    ";
                            }
                        }

                        else if (_instrumentTypePK == 4)
                        {
                            if (_trxType == 1)
                            {
                                cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT 
                                on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                                where I.status = 2 and IT.Type in (4) ";
                            }
                            else
                            {
                                cmd.CommandText =
                                @"Create Table #Temp
                                (InstrumentPK int,
                                Balance numeric(19,2),
                                InterestPercent numeric(19,2),
                                Date Datetime
                                )

                                Insert Into #Temp(InstrumentPK,Balance,InterestPercent,Date)

                                Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume, A.InterestPercent,@Date from (

                                select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume, InterestPercent from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 1 and status = 2  and InstrumentTypePK in (4) 

                                Group By InstrumentPK, InterestPercent

                                UNION ALL

                                select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume, InterestPercent from trxPortfolio

                                where ValueDate <= @Date and Posted = 1  and trxType = 2 and status = 2 and InstrumentTypePK in (4) 

                                Group By InstrumentPK, InterestPercent
                                )A

                                Group By A.InstrumentPK, InterestPercent

                                having sum(A.BuyVolume) - sum(A.SellVolume) > 0


                                select A.InstrumentPK,A.Balance,B.ID + ' - ' + B.Name InstrumentID,C.ID CurrencyID,AcqDate, A.InterestPercent, AcqDate MaturityDate from #Temp A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                left join Currency C on B.CurrencyPK = C.CurrencyPK and C.Status = 2
                                left join 
                                (
                                select InstrumentPK,max(ValueDate) AcqDate from TrxPortfolio where posted = 1 and status = 2 and TrxType = 1  and InstrumentTypePK in (4) 
                                group by instrumentPK, InterestPercent
                                )D on A.InstrumentPK = D.InstrumentPK  ";
                            }
                        }

                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForTrxPortfolio M_Instrument = new InstrumentForTrxPortfolio();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    if (_instrumentTypePK != 1)
                                    {
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    }
                                    if (_trxType != 1)
                                    {
                                        if (_instrumentTypePK == 5)
                                        {
                                            M_Instrument.BankPK = Convert.ToInt32(dr["BankPK"]);
                                            M_Instrument.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                            M_Instrument.BegBalance = Convert.ToDecimal(dr["BegBalance"]);
                                            M_Instrument.MovBalance = Convert.ToDecimal(dr["MovBalance"]);
                                            M_Instrument.TrxBuy = Convert.ToInt32(dr["TrxBuy"]);
                                            M_Instrument.Price = Convert.ToDecimal(dr["Price"]);
                                        }

                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);

                                    }

                                    L_Instrument.Add(M_Instrument);
                                }

                            }
                            return L_Instrument;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public bool FundClient_SInvest(string _userID, string _category, int _fundClientPKFrom, int _fundClientPKTo, FundClient _FundClient)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string paramFundClientSelected = "";
                        if (_FundClient.FundClientSelected == "" || _FundClient.FundClientSelected == "0")
                        {
                            paramFundClientSelected = "";
                        }
                        else
                        {
                            paramFundClientSelected = "and FC.FundClientPK in (" + _FundClient.FundClientSelected + ") ";
                        }

                        string _paramFundClientPK = "";
                        if (_fundClientPKFrom == 0 || _fundClientPKTo == 0)
                        {
                            _paramFundClientPK = "";
                        }
                        else
                        {
                            _paramFundClientPK = " And FC.FundClientPK Between " + _fundClientPKFrom + @" and " + _fundClientPKTo;
                        }
                        if (_category == "1")
                        {
                            cmd.CommandText = @"

BEGIN  
SET NOCOUNT ON         
select '1'  
+'|' + @CompanyID    
+ '|' + ''  
+ '|' + RTRIM(LTRIM(isnull(NamaDepanInd,'')))      
+ '|' + RTRIM(LTRIM(isnull(NamaTengahInd,'')))      
+ '|' + RTRIM(LTRIM(isnull(NamaBelakangInd,''))) 
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(nationality,''))))  
+ '|' + (isnull(NoIdentitasInd1,''))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when IdentitasInd1 = 7 then '99981231' else case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) = '19000101' or CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) < '20160802' then '' else CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) end End),''))))          
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,''))))   
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),''))))          
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Countryofbirth,''))))    
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,''))))   
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),''))))   
+ '|' + case when JenisKelamin = '0' then '' else isnull(cast(JenisKelamin as nvarchar(1)),'') end 
+ '|' + case when Pendidikan = '0' then '' else isnull(cast(Pendidikan as nvarchar(1)),'') end  
+ '|' + case when mothermaidenname = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(mothermaidenname ,'')))) end      
+ '|' + case when Agama = '0' then '' else isnull(cast(Agama as nvarchar(1)),'') end  
+ '|' + case when Pekerjaan = '0' then '' else isnull(cast(Pekerjaan as nvarchar(1)),'') end    
+ '|' + case when PenghasilanInd = '0' then '' else isnull(cast(PenghasilanInd as nvarchar(1)),'') end   
+ '|' + case when StatusPerkawinan = '0' then '' else isnull(cast(StatusPerkawinan as nvarchar(1)),'') end   
+ '|' + case when SpouseName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SpouseName ,'')))) end      
+ '|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar(1)),'') end  
+ '|' + case when MaksudTujuanInd = '0' then '' else isnull(cast(MaksudTujuanInd as nvarchar(1)),'') end   
+ '|' + case when SumberDanaInd = '0' then '' else isnull(cast(SumberDanaInd as nvarchar(2)),'') end   
+ '|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar(1)),'') end  
+ '|' + case when OtherAlamatInd1 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(OtherAlamatInd1,''))),char(13),''),char(10),'') end
+ '|' + case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar(4)),'')))) end     
+ '|' + case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end      
+ '|' + case when AlamatInd1 = '0' then '' else   REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd1,''))),char(13),''),char(10),'') end      
+ '|' + case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV13.DescOne,'')                                    
+ '|' + case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end    
+ '|' + isnull(CountryofCorrespondence,'')  
+ '|' + case when AlamatInd2 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd2,''))),char(13),''),char(10),'') end   
+ '|' + case when KodeKotaInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd2 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV14.DescOne,'')                                     
+ '|' + case when KodePosInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd2 ,'')))) end   
+ '|' + isnull(CountryofDomicile,'') 
+ '|' + case when TeleponRumah = '0' then '' else isnull(TeleponRumah ,'') end    
+ '|' + case when TeleponSelular = '0' then '' else isnull(TeleponSelular ,'') end    
+ '|' + case when fc.Fax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.Fax ,'')))) end     
+ '|' + case when fc.Email = '0' then '' else isnull(fc.Email,'') end     
+ '|' + case when StatementType = '0' then '' else isnull(cast(StatementType as nvarchar(1)),'') end    
+ '|' + case when FATCA = '0' then '' else isnull(cast(FATCA as nvarchar(1)),'') end   
+ '|' + case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end    
+ '|' + case when TINIssuanceCountry = '0' then '' else isnull(cast(TINIssuanceCountry as nvarchar(2)),'') end                                   
+ '|' +  case when B1.Country = 'ID' then case when isnull(B1.SInvestID,'') <> '' and  isnull(B1.BICode,'') <> '' then '' else isnull(B1.SInvestID,'') end else '' end  
+ '|' + case when B1.Country = 'ID' then isnull(B1.BICode,'') else '' end                           
+ '|' + isnull(B1.Name,'') 
+ '|' + isnull(B1.Country,'') 
+ '|' + case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV15.DescOne,'') 
+ '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(30)),'') end
+ '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end 
+ '|' +  case when B2.Country = 'ID' then case when isnull(B2.SInvestID,'') <> '' and  isnull(B2.BICode,'') <> '' then '' else isnull(B2.SInvestID,'') end else '' end  
+ '|' + case when B2.Country = 'ID' then isnull(B2.BICode,'') else '' end   
+ '|' + isnull(B2.Name,'') 
+ '|' + isnull(B2.Country,'') 
+ '|' + case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV16.DescOne,'') 
+ '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end   
+ '|' +  case when B3.Country = 'ID' then case when isnull(B3.SInvestID,'') <> '' and  isnull(B3.BICode,'') <> '' then '' else isnull(B3.SInvestID,'') end else '' end  
+ '|' + case when B3.Country = 'ID' then isnull(B3.BICode,'') else '' end   
+ '|' + isnull(B3.Name,'') 
+ '|' + isnull(B3.Country,'') 
+ '|' + case when BankBranchName3 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName3 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV17.DescOne,'') 
+ '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar(100)),'') end                                      
+ '|' + isnull(FC.ID,'') ResultText                                     
from fundclient FC left join Agent A on FC.SellingAgentPK = A.AgentPK and A.Status = 2   
left join MasterValue MV3 on FC.JenisKelamin = MV3.Code and MV3.status =2  and MV3.ID ='Sex'   
left join MasterValue MV4 on FC.Pendidikan = MV4.Code and MV4.status =2  and MV4.ID ='EducationalBackground'   
left join MasterValue MV5 on FC.Agama = MV5.Code and MV5.status =2  and MV5.ID ='Religion'   
left join MasterValue MV6 on FC.Pekerjaan = MV6.Code and MV6.status =2  and MV6.ID ='Occupation'   
left join MasterValue MV7 on FC.PenghasilanInd = MV7.Code and MV7.status =2  and MV7.ID ='IncomeInd'   
left join MasterValue MV8 on FC.StatusPerkawinan = MV8.Code and MV8.status =2  and MV8.ID ='MaritalStatus'   
left join MasterValue MV9 on FC.InvestorsRiskProfile = MV9.Code and MV9.status =2  and MV9.ID ='InvestorsRiskProfile'  
left join MasterValue MV10 on FC.MaksudTujuanInd = MV10.Code and MV10.status =2  and MV10.ID ='InvestmentObjectivesIND'   
left join MasterValue MV11 on FC.SumberDanaInd = MV11.Code and MV11.status =2  and MV11.ID ='IncomeSourceIND'   
left join MasterValue MV12 on FC.AssetOwner = MV12.Code and MV12.status =2  and MV12.ID ='AssetOwner'   
left join MasterValue MV13 on FC.KodeKotaInd1 = MV13.Code and MV13.status =2  and MV13.ID ='CityRHB'   
left join MasterValue MV14 on FC.KodeKotaInd2 = MV14.Code and MV14.status =2  and MV14.ID ='CityRHB'   
left join MasterValue MV15 on FC.MataUang1 = MV15.Code and MV15.status =2  and MV15.ID ='MataUang'   
left join MasterValue MV16 on FC.MataUang2 = MV16.Code and MV16.status =2  and MV16.ID ='MataUang'   
left join MasterValue MV17 on FC.MataUang3 = MV17.Code and MV17.status =2  and MV17.ID ='MataUang' 
left join Bank B1 on fc.NamaBank1 = B1.BankPK and B1.status = 2   
left join Bank B2 on fc.NamaBank2 = B2.BankPK and B2.status = 2   
left join Bank B3 on fc.NamaBank3 = B3.BankPK and B3.status = 2 
where FC.Status = 2 and FC.InvestorType = 1  " + paramFundClientSelected + @"
" + _paramFundClientPK + @" 
order by FC.name asc 

 END  ";
                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            using (SqlDataReader dr1 = cmd.ExecuteReader())
                            {
                                if (dr1.HasRows)
                                {
                                    string filePath;
                                    filePath = Tools.SInvestTextPath + "SInvestIndividuTxtVersion.txt";
                                    FileInfo txtFile = new FileInfo(filePath);
                                    if (txtFile.Exists)
                                    {
                                        txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {
                                        file.WriteLine("");
                                        while (dr1.Read())
                                        {

                                            file.WriteLine(Convert.ToString(dr1["ResultText"]));
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            cmd.CommandText = @"
                                   
                    BEGIN 
                      select '1'  
+'|' + @CompanyID     
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,''))))    
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Name,''))))       
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Negara,''))))  
+'|' + RTRIM(LTRIM(case when NomorSIUP = '0' then '' else isnull(cast(NomorSIUP as nvarchar(40)),'') end))   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), SIUPExpirationDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SIUPExpirationDate, 112) else '' End))) 
+'|' + case when NoSKD = '0' then '' else RTRIM(LTRIM(isnull(cast(NoSKD as nvarchar(40)),''))) end
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), ExpiredDateSKD, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateSKD, 112) else '' End))) 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,'')))) 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End)))
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryofEstablishment,''))))  
+'|' + isnull(MV10.DescOne,'') 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalBerdiri, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalBerdiri, 112) else '' End),''))))         
+'|' + RTRIM(LTRIM((isnull(NomorAnggaran,''))))
+'|' + case when Tipe = '0' then '' else isnull(cast(Tipe as nvarchar(1)),'') end 
+'|' + case when Karakteristik = '0' then '' else isnull(cast(Karakteristik as nvarchar(1)),'') end 
+'|' + case when PenghasilanInstitusi = '0' then '' else isnull(cast(PenghasilanInstitusi as nvarchar(1)),'') end 
+'|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar(1)),'') end 
+'|' + case when MaksudTujuanInstitusi = '0' then '' else isnull(cast(MaksudTujuanInstitusi as nvarchar(1)),'') end 
+'|' + case when SumberDanaInstitusi = '0' then '' else isnull(cast(SumberDanaInstitusi as nvarchar(1)),'') end 
+'|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar(1)),'') end  
+'|' + case when AlamatPerusahaan = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatPerusahaan ,''))),char(13),''),char(10),'') end   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CompanyCityName,'')))) 
+'|' + isnull(MV18.DescOne,'')                                      
+'|' + case when KodePosIns = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosIns ,'')))) end  
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryOfCompany,''))))   
+'|' + case when TeleponBisnis = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TeleponBisnis ,'')))) end    
+'|' + case when FC.Companyfax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Companyfax ,'')))) end    
+'|' + case when fc.CompanyMail = '0' then '' else isnull(fc.CompanyMail,'') end     
+'|' + case when StatementType = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(StatementType ,'')))) end   
+'|' + case when NamaDepanIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns1 ,'')))) end   
+'|' + case when NamaTengahIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns1 ,'')))) end  
+'|' + case when NamaBelakangIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns1 ,'')))) end  
+'|' + case when Jabatan1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan1 ,'')))) end   
+'|' + case when fc.PhoneIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns1 ,'')))) end   
+'|' + case when fc.EmailIns1 = '0' then '' else isnull(fc.EmailIns1,'') end    
+'|' +  
+'|' + case when fc.NoIdentitasIns11 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.NoIdentitasIns11 ,'')))) end   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) else '' End),''))))  
+'|' +  
+'|' +  
+'|' + case when NamaDepanIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns2 ,'')))) end   
+'|' + case when NamaTengahIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns2 ,'')))) end  
+'|' + case when NamaBelakangIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns2 ,'')))) end  
+'|' + case when Jabatan2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan2 ,'')))) end   
+'|' + case when fc.PhoneIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns2 ,'')))) end   
+'|' + case when fc.EmailIns2 = '0' then '' else isnull(fc.EmailIns2,'') end   
+'|' +  
+'|' + case when fc.NoIdentitasIns21 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.NoIdentitasIns21 ,'')))) end   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasIns21, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasIns21, 112) else '' End),''))))   
+'|' +  
+'|' +  
+'|' + case when AssetFor1Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor1Year ,'')))) end  
+'|' + case when AssetFor2Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor2Year ,'')))) end   
+'|' + case when AssetFor3Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor3Year ,'')))) end 
+'|' + case when OperatingProfitFor1Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor1Year ,'')))) end   
+'|' + case when OperatingProfitFor2Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor2Year ,'')))) end   
+'|' + case when OperatingProfitFor3Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor3Year ,'')))) end 
+'|' + case when FATCA = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FATCA ,'')))) end 
+'|' + case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end  
+'|' + case when TINIssuanceCountry = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TINIssuanceCountry ,'')))) end  
+'|' + case when GIIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(GIIN ,'')))) end   
+'|' + case when SubstantialOwnerName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerName ,'')))) end    
+'|' + case when SubstantialOwnerAddress = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerAddress ,'')))) end    
+'|' + case when SubstantialOwnerTIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerTIN ,'')))) end                                   
+ '|' + ''
+ '|' + isnull(B1.BICode,'')                        
+ '|' + isnull(B1.Name,'') 
+ '|' + isnull(B1.Country,'') 
+ '|' + case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV15.DescOne,'') 
+ '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(30)),'') end
+ '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end 
+ '|' +  ''
+ '|' + isnull(B2.BICode,'')                        
+ '|' + isnull(B2.Name,'') 
+ '|' + isnull(B2.Country,'') 
+ '|' + case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV16.DescOne,'') 
+ '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end   
+ '|' + ''
+ '|' + isnull(B3.BICode,'')                        
+ '|' + isnull(B3.Name,'') 
+ '|' + isnull(B3.Country,'') 
+ '|' + case when BankBranchName3 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName3,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV17.DescOne,'') 
+ '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar(100)),'') end                                      
+ '|' + isnull(FC.ID,'')   ResultText
from fundclient FC left join Agent A on FC.SellingAgentPK = A.AgentPK and A.Status = 2   
left join MasterValue MV3 on FC.Tipe = MV3.Code and MV3.status =2  and MV3.ID ='CompanyType'   
left join MasterValue MV4 on FC.Karakteristik = MV4.Code and MV4.status =2  and MV4.ID ='CompanyCharacteristic'   
left join MasterValue MV5 on FC.PenghasilanInstitusi = MV5.Code and MV5.status =2  and MV5.ID ='IncomeINS'   
left join MasterValue MV6 on FC.InvestorsRiskProfile = MV6.Code and MV6.status =2  and MV6.ID ='InvestorsRiskProfile'   
left join MasterValue MV7 on FC.MaksudTujuanInstitusi = MV7.Code and MV7.status =2  and MV7.ID ='InvestmentObjectivesINS'  
left join MasterValue MV8 on FC.SumberDanaInstitusi = MV8.Code and MV8.status =2  and MV8.ID ='IncomeSourceINS'   
left join MasterValue MV9 on FC.AssetOwner = MV9.Code and MV9.status =2  and MV9.ID ='AssetOwner'   
left join MasterValue MV10 on FC.KodeKotaIns = MV10.Code and MV10.status =2  and MV10.ID ='CityRHB'   
left join MasterValue MV15 on FC.MataUang1 = MV15.Code and MV15.status =2  and MV15.ID ='MataUang'   
left join MasterValue MV16 on FC.MataUang2 = MV16.Code and MV16.status =2  and MV16.ID ='MataUang'   
left join MasterValue MV17 on FC.MataUang3 = MV17.Code and MV17.status =2  and MV17.ID ='MataUang'     
left join MasterValue MV18 on CompanyCityName = MV18.Code and MV18.status =2  and MV18.ID ='CityRHB'  
left join Bank B1 on fc.NamaBank1 = B1.BankPK and B1.status = 2   
left join Bank B2 on fc.NamaBank2 = B2.BankPK and B2.status = 2   
left join Bank B3 on fc.NamaBank3 = B3.BankPK and B3.status = 2 
where FC.Status = 2 and FC.InvestorType = 2 " + paramFundClientSelected + @"
" + _paramFundClientPK + @" 
order by FC.name asc  
                        END ";

                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            using (SqlDataReader dr1 = cmd.ExecuteReader())
                            {
                                if (dr1.HasRows)
                                {
                                    if (dr1.HasRows)
                                    {
                                        string filePath;
                                        filePath = Tools.SInvestTextPath + "SInvestInstitusiTxtVersion.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            file.WriteLine("");
                                            while (dr1.Read())
                                            {
                                                file.WriteLine(Convert.ToString(dr1["ResultText"]));
                                            }
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                            }
                        }
                        return false;

                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Boolean GenerateReportAccounting(string _userID, AccountingRpt _accountingRpt)
        {

            #region Account Activity By Counterpart
            if (_accountingRpt.ReportName.Equals("Account Activity By Counterpart"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramAccount = "";
                            string _paramOffice = "";
                            string _paramDepartment = "";
                            string _paramAgent = "";
                            string _paramConsignee = "";
                            string _paramInstrument = "";
                            string _paramCounterpart = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");

                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "And B.AccountPK  in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }

                            if (!_host.findString(_accountingRpt.OfficeFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.OfficeFrom))
                            {
                                _paramOffice = "And B.OfficePK  in ( " + _accountingRpt.OfficeFrom + " ) ";
                            }
                            else
                            {
                                _paramOffice = "";
                            }

                            if (!_host.findString(_accountingRpt.DepartmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.DepartmentFrom))
                            {
                                _paramDepartment = "And B.DepartmentPK  in ( " + _accountingRpt.DepartmentFrom + " ) ";
                            }
                            else
                            {
                                _paramDepartment = "";
                            }

                            if (!_host.findString(_accountingRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AgentFrom))
                            {
                                _paramAgent = "And B.AgentPK  in ( " + _accountingRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            if (!_host.findString(_accountingRpt.ConsigneeFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.ConsigneeFrom))
                            {
                                _paramConsignee = "And B.ConsigneePK  in ( " + _accountingRpt.ConsigneeFrom + " ) ";
                            }
                            else
                            {
                                _paramConsignee = "";
                            }


                            if (!_host.findString(_accountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.InstrumentFrom))
                            {
                                _paramInstrument = "And B.InstrumentPK  in ( " + _accountingRpt.InstrumentFrom + " ) ";
                            }
                            else
                            {
                                _paramInstrument = "";
                            }

                            if (!_host.findString(_accountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And B.CounterpartPK  in ( " + _accountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4)";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4) ";
                            }
                            cmd.CommandText = @"
	                        Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                            ------
                            DECLARE @AccountRpt TABLE
                            (
                            AccountPK int
                            )

                            INSERT INTO @AccountRpt
                            ( AccountPK )
                            SELECT DISTINCT B.AccountPK 
                            FROM journal A  
                            LEFT join journalDetail B on A.JournalPK = B.JournalPK   
                            Where A.ValueDate Between @ValueDateFrom and @ValueDateTo 
                            and A.PeriodPK = @PeriodPK 


                            DECLARE @AccountBalance TABLE
                            (
                            AccountPK INT,
                            StartBalance NUMERIC(22,4)
                            )

                            INSERT INTO @AccountBalance
                            ( AccountPK, StartBalance )
                            SELECT AccountPK,[dbo].[FGetStartAccountBalance](@ValueDateFrom,AccountPK) FROM @AccountRpt
                            ------



                            select C.Type AccountType,A.JournalPK,A.ValueDate,A.Reference,C.ID AccountID,C.Name AccountName,    
                            isnull(D.ID,'') CurrencyID,isnull(E.ID,'') OfficeID,isnull(F.ID,'') DepartmentID,isnull(G.ID,'') AgentID,isnull(H.ID,'') ConsigneeID,    
                            isnull(I.ID,'') InstrumentID,B.DetailDescription,B.DebitCredit,B.Amount,B.Debit,B.Credit,B.CurrencyRate Rate,    
                            B.BaseDebit,B.BaseCredit,isnull(K.StartBalance,0) StartBalance ,    
                            case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo,isnull(J.ID,'') CounterpartID     
                            from journal A    
                            left join journalDetail B on A.JournalPK = B.JournalPK and B.status in (1,2)    
                            left join Account C on B.AccountPK = C.AccountPK and C.status in (1,2)    
                            left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status in (1,2)    
                            left join Office E on B.OfficePK = E.OfficePK and E.status in (1,2)    
                            left join Department F on B.DepartmentPK = F.DepartmentPK and F.status in (1,2)    
                            left join Agent G on B.AgentPK = G.AgentPK and G.status in (1,2)    
                            left join Consignee H on B.consigneePK = H.ConsigneePK and H.status in (1,2)    
                            left join Instrument I on B.InstrumentPK = I.InstrumentPK and I.status in (1,2) 
                            left join Counterpart J on B.CounterpartPK = J.CounterpartPK and J.status in (1,2) 
                            LEFT JOIN @AccountBalance K ON B.AccountPK  = K.AccountPK  
                            Where A.ValueDate Between @ValueDateFrom and @ValueDateTo 
                            and A.PeriodPK = @PeriodPK 
                             " + _status + _paramAccount + _paramOffice + _paramDepartment + _paramAgent + _paramConsignee + _paramInstrument + _paramCounterpart;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@AccountFrom", _accountingRpt.AccountFrom);
                            cmd.Parameters.AddWithValue("@OfficeFrom", _accountingRpt.OfficeFrom);
                            cmd.Parameters.AddWithValue("@DepartmentFrom", _accountingRpt.DepartmentFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _accountingRpt.AgentFrom);
                            cmd.Parameters.AddWithValue("@ConsigneeFrom", _accountingRpt.ConsigneeFrom);
                            cmd.Parameters.AddWithValue("@InstrumentFrom", _accountingRpt.InstrumentFrom);
                            cmd.Parameters.AddWithValue("@CounterpartFrom", _accountingRpt.CounterpartFrom);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "AccountActivityByCounterpart" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "AccountActivityByCounterpart" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Account Activity By Counterpart");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<AccountActivityByCounterpart> rList = new List<AccountActivityByCounterpart>();
                                        while (dr0.Read())
                                        {
                                            AccountActivityByCounterpart rSingle = new AccountActivityByCounterpart();
                                            rSingle.AccountType = dr0["AccountType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt16(dr0["AccountType"]);
                                            rSingle.StartBalance = dr0["StartBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["StartBalance"]);
                                            rSingle.journalPK = dr0["journalPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["journalPK"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Reference = dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]);
                                            rSingle.RefNo = dr0["RefNo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["RefNo"]);
                                            rSingle.DetailDescription = dr0["DetailDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DetailDescription"]);
                                            rSingle.AccountID = dr0["AccountID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountID"]);
                                            rSingle.AccountName = dr0["AccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountName"]);
                                            rSingle.CurrencyID = dr0["CurrencyID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.DebitCredit = dr0["DebitCredit"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DebitCredit"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.Debit = dr0["Debit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Debit"]);
                                            rSingle.Credit = dr0["Credit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Credit"]);
                                            rSingle.Rate = dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]);
                                            rSingle.BaseDebit = dr0["BaseDebit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BaseDebit"]);
                                            rSingle.BaseCredit = dr0["BaseCredit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BaseCredit"]);
                                            rSingle.OfficeID = dr0["OfficeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OfficeID"]);
                                            rSingle.DepartmentID = dr0["DepartmentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DepartmentID"]);
                                            rSingle.AgentID = dr0["AgentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AgentID"]);
                                            rSingle.ConsigneeID = dr0["ConsigneeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ConsigneeID"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.CounterpartID = dr0["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartID"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByAccountID =
                                            from r in rList
                                            orderby r.AccountID, r.ValueDate, r.RefNo ascending
                                            group r by new { r.AccountID, r.AccountName, r.CurrencyID, r.StartBalance } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;
                                        int _rowEndBalance = 0;
                                        foreach (var rsHeader in GroupByAccountID)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "ACC : ";
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.AccountID + "-" + rsHeader.Key.AccountName;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "DATEFROM :";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 7].Value = _accountingRpt.ValueDateFrom;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "CURR ID : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CurrencyID;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "DATETO :";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 7].Value = _accountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Value = "BEG BALANCE :";
                                            if (_accountingRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                            }
                                            else if (_accountingRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                            }
                                            else if (_accountingRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.000000";

                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00000000";

                                            }
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.StartBalance;
                                            _rowEndBalance = incRowExcel;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "DATE";
                                            worksheet.Cells[incRowExcel, 3].Value = "REF";
                                            worksheet.Cells[incRowExcel, 4].Value = "DESC";
                                            worksheet.Cells[incRowExcel, 5].Value = "COUNT";
                                            worksheet.Cells[incRowExcel, 6].Value = "INST";
                                            worksheet.Cells[incRowExcel, 7].Value = "BASE DEBIT";
                                            worksheet.Cells[incRowExcel, 8].Value = "BASE CREDIT";
                                            worksheet.Cells[incRowExcel, 9].Value = "BALANCE";
                                            string _range = "A" + incRowExcel + ":I" + incRowExcel;
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

                                            _range = "A" + incRowExcel + ":I" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":I" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Reference;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.DetailDescription;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.BaseDebit;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.BaseCredit;
                                                if (rsDetail.AccountType == 1 || rsDetail.AccountType == 4)
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Formula = "I" + _rowEndBalance + "+G" + incRowExcel + "-H" + incRowExcel;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Formula = "I" + _rowEndBalance + "-G" + incRowExcel + "+H" + incRowExcel;
                                                }

                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                                //worksheet.Cells[incRowExcel, 9].Calculate();
                                                _rowEndBalance = incRowExcel;

                                                incRowExcel++;
                                                _range = "A" + incRowExcel + ":I" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                }
                                                _no++;


                                            }

                                            worksheet.Cells["I" + _rowEndBalance + ":I" + _rowEndBalance].Calculate();

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _accountingRpt.PageBreak;

                                        }
                                        string _rangeDetail = "A:I";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(10).Width = 1;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(5).AutoFit();
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.Column(9).AutoFit();
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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 ACCOUNT ACTIVITY BY COUNTERPART";

                                        if (_accountingRpt.ValueDateTo <= _compareDate)
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftBatchReport();
                                        }
                                        else
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        }

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Fixed Asset
            else if (_accountingRpt.ReportName.Equals("Fixed Asset"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramData = "";

                            decimal _netPnlValueDateFrom = 0;
                            decimal _netPnlLastMonth = 0;
                            decimal _netPnLValueDateTo = 0;

                            if (_accountingRpt.ParamData == 1)
                            {
                                _paramData = " and A.Groups = 1  ";
                            }
                            else if (_accountingRpt.ParamData == 2)
                            {
                                _paramData = " and A.Groups = 0  ";
                            }
                            else if (_accountingRpt.ParamData == 3)
                            {
                                _paramData = " and A.Groups in (0,1)  ";
                            }


                            cmd.CommandText =
                             @" BEGIN 
                             SET NOCOUNT ON  
                             Select Z.row,Z.AccountID,Z.Name,Z.ID,sum(Z.PreviousBaseBalance) PreviousBaseBalance,sum(z.BaseDebitMutasi) BaseDebitMutasi , 
                             sum(z.BaseCreditMutasi) BaseCreditMutasi , sum(z.CurrentBaseBalance) CurrentBaseBalance 
                             from 
                             ( 
                             SELECT C.Row,E.ID AccountID, E.Name, 
                             D.ID,   
                             sum(CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4))) AS PreviousBaseBalance,   
                             sum(CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) AS BaseDebitMutasi,   
                             sum(CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))) AS BaseCreditMutasi,  
                             sum(CAST(A.CurrentBaseBalance AS NUMERIC(19,4))) AS CurrentBaseBalance   
                             FROM (  
                             SELECT A.AccountPK,    
                             SUM(B.Balance) AS CurrentBalance,   
                             SUM(B.BaseBalance) AS CurrentBaseBalance, 
                             SUM(B.SumDebit) AS CurrentDebit,   
                             SUM(B.SumCredit) AS CurrentCredit,  
                             SUM(B.SumBaseDebit) AS CurrentBaseDebit,    
                             SUM(B.SumBaseCredit) AS CurrentBaseCredit   
                             FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (   
                             SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,    
                             SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,    
                             SUM(A.Debit) AS SumDebit,    
                             SUM(A.Credit) AS SumCredit,   
                             SUM(A.BaseDebit) AS SumBaseDebit,   
                             SUM(A.BaseCredit) AS SumBaseCredit,   
                             C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,   
                             C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9   
                             FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK   
                             INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status = 2 
                             INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status = 2  
                             WHERE B.ValueDate <= @DateTo   
                             AND C.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) 
                             AND D.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) 
                             Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,    
                             C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,  
                             C.ParentPK7, C.ParentPK8, C.ParentPK9    
                             ) AS B    
                             WHERE A.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) " +
                            _paramData +
                            @" AND C.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) 
                             AND (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK   
                             OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK   
                             OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK   
                             OR B.ParentPK9 = A.AccountPK)   
                             Group BY A.AccountPK   
                            ) AS A LEFT JOIN (  
                             SELECT A.AccountPK,   
                             SUM(B.Balance) AS PreviousBalance,    
                             SUM(B.BaseBalance) AS PreviousBaseBalance,  
                             SUM(B.SumDebit) AS PreviousDebit, 
                             SUM(B.SumCredit) AS PreviousCredit,   
                             SUM(B.SumBaseDebit) AS PreviousBaseDebit,   
                             SUM(B.SumBaseCredit) AS PreviousBaseCredit  
                             FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (  
                             SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,   
                             SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,   
                             SUM(A.Debit) AS SumDebit,   
                             SUM(A.Credit) AS SumCredit,   
                             SUM(A.BaseDebit) AS SumBaseDebit,    
                             SUM(A.BaseCredit) AS SumBaseCredit,    
                             C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,    
                             C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9   
                             FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK    
                             INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status = 2 
                             INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status = 2
                             WHERE B.ValueDate <= @DateFrom    
                             AND C.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) 
                             AND D.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) 
                             Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,    
                             C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,  
                              C.ParentPK7, C.ParentPK8, C.ParentPK9    
                             ) AS B   
                             WHERE A.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) " +
                            _paramData +
                            @" AND C.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) 
                             AND (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK   
                             OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK   
                             OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK    
                             OR B.ParentPK9 = A.AccountPK)  
                             Group BY A.AccountPK   
                             ) AS B ON A.AccountPK = B.AccountPK    
                             INNER JOIN OSKMonthlyMappingReport C ON A.AccountPK = C.AccountPK   And C.Status = 2 
                             INNER JOIN Account E on C.AccountPK = E.AccountPK and E.Status = 2 
                             INNER JOIN Currency D ON E.CurrencyPK = D.CurrencyPK   And D.Status = 2 
                             WHERE (A.CurrentBalance <> 0)   
                             OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)   
                             OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)   
                             OR (A.CurrentBaseBalance <> 0)   
                             OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)    
                             OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
                             Group by C.Row,E.ID, E.Name, E.[Groups],  
                             D.ID 
                             UNION ALL 
                             Select B.row,A.AccountID AccountID,A.AccountName Name,A.ID ID,0 previousBaseBalance, 
                             sum(A.BaseDebitMutasi) BaseDebitMutasi,sum(A.BaseCreditMutasi) BaseCreditMutasi, 
                             sum(A.BaseDebitMutasi) - sum(A.BaseCreditMutasi) CurrentBaseBalance 
                             from ( 
                             Select     
                             A.AccountPK,A.ID AccountID, A.Name AccountName, K.ID ID,      
                             0 PreviousBaseBalance , BaseDebit BaseDebitMutasi,0 BaseCreditMutasi, 0 CurrentBaseBalance 
                             from Cashier C       
                             left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2        
                             left join Office E on C.OfficePK = E.OfficePK and E.status = 2        
                             left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2        
                             left join Agent G on C.AgentPK = G.AgentPK and G.status = 2        
                             left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2        
                             left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2   
                             left join Cashref J on C.DebitCashRefPK = J.CashRefPK and J.status = 2 
                             left join Currency K on c.CreditCurrencyPK = K.CurrencyPK and K.status = 2 
                             Where C.ValueDate between @DateFrom and @DateTo and C.Status = 2        
                             and A.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) 
                             and K.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) " +
                            _paramData +
                             @"and c.status = 2 and c.posted = 0 
                             UNION ALL       
                             Select    
                             A.AccountPK,A.ID AccountID, A.Name AccountName, K.ID ID, 
                             0 PreviousBaseBalance , 0 BaseDebitMutasi,BaseCredit BaseCreditMutasi, 0 CurrentBaseBalance 
                             from Cashier C        
                             left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                             left join Office E on C.OfficePK = E.OfficePK and E.status = 2        
                             left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2      
                             left join Agent G on C.AgentPK = G.AgentPK and G.status = 2        
                             left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2        
                             left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                             left join Cashref J on C.DebitCashRefPK = J.CashRefPK and J.status = 2 
                             left join Currency K on c.CreditCurrencyPK = K.CurrencyPK and K.status = 2
                             Where C.ValueDate between @DateFrom and @DateTo and C.Status = 2    
                             and A.ID Between left(@IDFrom,charindex(' ',@IDFrom) - 1) and left(@IDTo,charindex(' ',@IDTo) - 1) 
                             and K.ID Between left(@CurrencyIDFrom,charindex(' ',@CurrencyIDFrom) - 1) and left(@CurrencyIDTo,charindex(' ',@CurrencyIDTo) - 1) " +
                            _paramData +
                            @" and c.status = 2 and c.posted = 0 
                             ) A INNER JOIN OSKMonthlyMappingReport B ON A.AccountPK = B.AccountPK   And B.Status = 2 
                             group by A.AccountID ,A.AccountName ,A.ID ,B.row 
                             )Z 
                             group by z.row,z.accountid,z.name,z.id 
                             order by z.row 
                             END  ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _accountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@IDFrom", _accountingRpt.AccountFrom);
                            cmd.Parameters.AddWithValue("@IDTo", _accountingRpt.AccountTo);
                            cmd.Parameters.AddWithValue("@CurrencyIDFrom", _accountingRpt.CurrencyFrom);
                            cmd.Parameters.AddWithValue("@CurrencyIDTo", _accountingRpt.CurrencyTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    using (SqlConnection subCon = new SqlConnection(Tools.conString))
                                    {
                                        subCon.Open();
                                        using (SqlCommand subCmd = subCon.CreateCommand())
                                        {
                                            subCmd.CommandText = @"Declare @DateFrom datetime Declare @DateTo datetime Declare @PeriodPK int 
                                                        set @DateFrom = DATEADD(m, DATEDIFF(m,0,@Date), 0) 
                                                        set @DateTo = DATEADD(m, DATEDIFF(m,0,@Date) + 1, -1) 
                                                        Declare @Income int Declare @Expense int 
                                                        select @Income = Income,@Expense = Expense From AccountingSetup where status = 2 
                                                        select @PeriodPK = PeriodPK from period Where status = 2 and @Date Between Datefrom and DateTo
                                                        Select Sum(A.Income+A.Expense) - sum(A.IncomeLastMonth + A.ExpenseLastMonth) Net 
                                                        ,sum(A.IncomeLastMonth + A.ExpenseLastMonth) NetLastMonth From ( 
                                                        select sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) expense , 0 As Income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                        from journal j left join journalDetail jd on j.JournalPK = jd.journalPK  
                                                        left join account a on a.AccountPK = jd.AccountPK and a.status = 2 
                                                        where j.ValueDate <= @Dateto  
                                                        and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Expense 
                                                        in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                         a.parentpk7, a.parentpk8, a.parentpk9) 
                                                        UNION ALL 
                                                        select  0 as Expense,sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) income,0 IncomeLastMonth,0 ExpenseLastMonth
                                                        from journal j left join journalDetail jd on j.JournalPK = jd.journalPK  
                                                        left join account a on a.AccountPK = jd.AccountPK and a.status = 2
                                                        where j.ValueDate <= @Dateto  
                                                        and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Income 
                                                        in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                        a.parentpk7, a.parentpk8, a.parentpk9) 
                                                        UNION ALL 
                                                        select 0 as expense , 0 As Income,  sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) IncomeLastMonth, 0 as ExpenseLastMonth 
                                                        from journal j left join journalDetail jd on j.JournalPK = jd.journalPK 
                                                        left join account a on a.AccountPK = jd.AccountPK and a.status = 2 
                                                        where j.ValueDate < @DateFrom 
                                                        and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Expense  
                                                        in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                        a.parentpk7, a.parentpk8, a.parentpk9)
                                                        UNION ALL
                                                        select  0 as Expense,0 as income,0 as IncomeLastMonth,sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) ExpenseLastMonth 
                                                        from journal j left join journalDetail jd on j.JournalPK = jd.journalPK  
                                                        left join account a on a.AccountPK = jd.AccountPK and a.status = 2 
                                                        where j.ValueDate < @DateFrom 
                                                        and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Income 
                                                        in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                        a.parentpk7, a.parentpk8, a.parentpk9))A";
                                            subCmd.CommandTimeout = 0;
                                            subCmd.Parameters.AddWithValue("@Date", _accountingRpt.ValueDateFrom);
                                            using (SqlDataReader dr1 = subCmd.ExecuteReader())
                                            {
                                                if (!dr1.HasRows)
                                                {
                                                    return false;
                                                }
                                                else
                                                {
                                                    dr1.Read();
                                                    _netPnlValueDateFrom = Convert.ToDecimal(dr1["Net"]);
                                                    _netPnlLastMonth = Convert.ToDecimal(dr1["NetLastMonth"]);
                                                }
                                            }
                                        }
                                    }

                                    using (SqlConnection subCon1 = new SqlConnection(Tools.conString))
                                    {
                                        subCon1.Open();
                                        using (SqlCommand subCmd1 = subCon1.CreateCommand())
                                        {
                                            subCmd1.CommandText = @"Declare @DateFrom datetime Declare @DateTo datetime Declare @PeriodPK int 
                                                set @DateFrom = DATEADD(m, DATEDIFF(m,0,@Date), 0) 
                                                set @DateTo = DATEADD(m, DATEDIFF(m,0,@Date) + 1, -1) 
                                                Declare @Income int 
                                                Declare @Expense int 
                                                select @Income = Income,@Expense = Expense From AccountingSetup 
                                                where status = 2 
                                                select @PeriodPK = PeriodPK from period Where 
                                                status = 2 and @Date Between Datefrom and DateTo 
                                                Select Sum(A.Income+A.Expense) - sum(A.IncomeLastMonth + A.ExpenseLastMonth) Net 
                                                ,sum(A.IncomeLastMonth + A.ExpenseLastMonth) NetLastMonth
                                                From 
                                                ( 
                                                select sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) expense , 0 As Income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                from journal j left join journalDetail jd on j.JournalPK = jd.journalPK  
                                                left join account a on a.AccountPK = jd.AccountPK and a.status = 2 
                                                where j.ValueDate <= @Dateto 
                                                and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Expense  
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6,
                                                a.parentpk7, a.parentpk8, a.parentpk9) 
                                                UNION ALL
                                                select  0 as Expense,sum(isnull(jd.BaseDebit,0) - isnull(jd.BaseCredit,0)) income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                from journal j left join journalDetail jd on j.JournalPK = jd.journalPK  
                                                left join account a on a.AccountPK = jd.AccountPK and a.status = 2 
                                                where j.ValueDate <= @Dateto  
                                                and a.Type >= 3 and J.PeriodPK = @PeriodPK and @Income 
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                a.parentpk7, a.parentpk8, a.parentpk9) 
                                                UNION ALL 
                                                select sum(isnull(jd.BaseDebit,0)) expense , 0 As Income,0 IncomeLastMonth,0 ExpenseLastMonth
                                                from cashier jd 
                                                left join account a on a.AccountPK = jd.DebitAccountPK and a.status = 2 
                                                where jd.ValueDate between @DateFrom and @DateTo 
                                                and a.Type >= 3 and Jd.PeriodPK = @PeriodPK and @Expense  
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                a.parentpk7, a.parentpk8, a.parentpk9) and jd.status = 2 and jd.posted = 0 
                                                UNION ALL
                                                select sum(isnull(jd.BaseCredit,0)) expense , 0 As Income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                from cashier jd   
                                                left join account a on a.AccountPK = jd.CreditAccountPK and a.status = 2 
                                                where jd.ValueDate between @DateFrom and @DateTo 
                                                and a.Type >= 3 and Jd.PeriodPK = @PeriodPK and @Expense 
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                a.parentpk7, a.parentpk8, a.parentpk9) and jd.status = 2 and jd.posted = 0 
                                                UNION ALL 
                                                select  0 as Expense,sum(isnull(jd.BaseDebit,0)) * -1 income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                from cashier jd  
                                                left join account a on a.AccountPK = jd.DebitAccountPK and a.status = 2 
                                                where jd.ValueDate between @DateFrom and @DateTo 
                                                and a.Type >= 3 and Jd.PeriodPK = @PeriodPK and @Income 
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                a.parentpk7, a.parentpk8, a.parentpk9) and jd.status = 2 and jd.posted = 0 
                                                UNION ALL 
                                                select  0 as Expense,sum(isnull(jd.BaseCredit,0)) * -1 income,0 IncomeLastMonth,0 ExpenseLastMonth 
                                                from cashier jd  
                                                left join account a on a.AccountPK = jd.CreditAccountPK and a.status = 2
                                                where jd.ValueDate between @DateFrom and @DateTo 
                                                and a.Type >= 3 and Jd.PeriodPK = @PeriodPK and @Income 
                                                in (a.accountpk, a.parentpk1, a.parentpk2, a.parentpk3, a.parentpk4, a.parentpk5, a.parentpk6, 
                                                a.parentpk7, a.parentpk8, a.parentpk9) and jd.status = 2 and jd.posted = 0 
                                                )A ";
                                            subCmd1.CommandTimeout = 0;
                                            subCmd1.Parameters.AddWithValue("@Date", _accountingRpt.ValueDateTo);
                                            using (SqlDataReader dr2 = subCmd1.ExecuteReader())
                                            {
                                                if (!dr2.HasRows)
                                                {
                                                    return false;
                                                }
                                                else
                                                {
                                                    dr2.Read();
                                                    _netPnLValueDateTo = Convert.ToDecimal(dr2["Net"]);
                                                }
                                            }
                                        }
                                    }


                                    string filePath = Tools.ReportsPath + "RHBOSKMonthlyProjection" + "_" + _userID + ".xlsx";
                                    File.Copy(Tools.ReportsTemplatePath + "OSKMonthlyProjectionTemplate.xlsx", filePath, true);
                                    FileInfo excelFile = new FileInfo(filePath);

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {

                                        ExcelWorksheet cover = package.Workbook.Worksheets[1];
                                        cover.Cells[24, 1].Value = _accountingRpt.ValueDateTo;
                                        ExcelWorksheet bs = package.Workbook.Worksheets[2];
                                        bs.Cells[7, 6].Value = _accountingRpt.ValueDateFrom;
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[5];

                                        if (_accountingRpt.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[97, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[98, 5].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[97, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[98, 5].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[97, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[98, 5].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[97, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[98, 5].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[97, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[98, 5].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[97, 5].Value = _netPnlLastMonth;
                                        worksheet.Cells[98, 5].Value = _netPnlValueDateFrom;
                                        worksheet.Cells[98, 8].Value = _netPnLValueDateTo;
                                        while (dr0.Read())
                                        {



                                            if (_accountingRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Value = Convert.ToDecimal(worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Value) + Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Value = Convert.ToDecimal(worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Value) + Convert.ToDecimal(dr0["BaseDebitMutasi"]);
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Value = Convert.ToDecimal(worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Value) + Convert.ToDecimal(dr0["BaseCreditMutasi"]);
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Value = Convert.ToDecimal(worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Value) + Convert.ToDecimal(dr0["CurrentBaseBalance"]);

                                        }
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



            #endregion
            


          
            else
            {
                return false;
            }
        }

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

           

            #region Transaction Summary
            if (_FundAccountingRpt.ReportName.Equals("Transaction Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }


                            cmd.CommandText =

                            @"
                            select E.Name, case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then 'BOND' when A.InstrumentTypePK = 1 then 'EQUITY' else 'TIME DEPOSIT' end InstrumentTypeName
                            ,A.TrxTypeID,B.ID InstrumentID,ValueDate,SettlementDate,isnull(C.ID,'') CounterpartCode,B.Name InstrumentName,DoneVolume,DonePrice,DoneAmount,
                            sum(TotalAmount - DoneAmount + WHTAmount) TotalBrokerFee,case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then sum(IncomeTaxGainAmount+IncomeTaxInterestAmount) when A.InstrumentTypePK = 1 then WHTAmount else 0 end WHTAmount, TotalAmount,dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK) AcqPrice,sum(DonePrice - dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK)) * DoneVolume Realised,
                            isnull(DoneAccruedInterest,0) DoneAccruedInterest,A.InterestPercent,isnull(D.Name,'') BankName,case when A.InstrumentTypePK = 5 then isnull(DATEDIFF(day,A.ValueDate ,A.MaturityDate),0) else 0 end Tenor,A.MaturityDate from Investment A 
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status = 2
                            left join Bank D on A.BankPK = D.BankPK and D.Status = 2
                            left join Fund E on A.FundPK = E.FundPK and E.status = 2
                            where StatusSettlement = 2 and valuedate between @ValueDateFrom and @ValueDateTo 
                            group by E.Name,A.TrxTypeID,B.ID,ValueDate,SettlementDate,C.ID,B.Name,DoneVolume,DonePrice,DoneAmount,
                            WHTAmount, TotalAmount,AcqPrice,DoneAccruedInterest,A.InterestPercent,D.Name,A.MaturityDate,A.InstrumentTypePK,A.MaturityDate,A.InstrumentPK,A.FundPK,A.InvestmentPK

                            ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "TransactionSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TransactionSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Transaction Summary");

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<TrxSummary> rList = new List<TrxSummary>();
                                        while (dr0.Read())
                                        {
                                            TrxSummary rSingle = new TrxSummary();
                                            rSingle.FundName = Convert.ToString(dr0["Name"]);
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"]);
                                            rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.CounterpartCode = Convert.ToString(dr0["CounterpartCode"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DonePrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.DoneAmount = Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.TotalBrokerFee = Convert.ToDecimal(dr0["TotalBrokerFee"]);
                                            rSingle.WHTAmount = Convert.ToDecimal(dr0["WHTAmount"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.AcqPrice = Convert.ToDecimal(dr0["AcqPrice"]);
                                            rSingle.Realised = Convert.ToDecimal(dr0["Realised"]);
                                            rSingle.DoneAccruedInterest = Convert.ToDecimal(dr0["DoneAccruedInterest"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                            rSingle.Tenor = Convert.ToInt32(dr0["Tenor"]);
                                            rSingle.MaturityDate = dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.FundName, r.InstrumentTypeName, r.TrxTypeID } into rGroup
                                                     select rGroup;

                                        int incRowExcel = 1;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Investment Transaction";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Period";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMMM-yyyy") + "-" + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMMM-yyyy");
                                        incRowExcel = incRowExcel + 2;

                                        //int incRowExcel = 1;
                                        int RowA = incRowExcel;
                                        incRowExcel = incRowExcel + 1;
                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Instrument Type";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Transaction";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.TrxTypeID;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Transaction Date";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Settlement Date";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Counterpart Code";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Securities Name/Code";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Quantity";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Price";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Tot. Counterpart Fee";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "WHT";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Total Settlement";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = "Book Cost";
                                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 15].Value = "Realized Gain/Loss";
                                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                }

                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }
                                            else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Transaction Date";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Settlement Date";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Counterpart Code";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Securities Name";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Price";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "WHT";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Total Settlement";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = "Book Cost";
                                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 15].Value = "Realized Gain/Loss";
                                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                }
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }

                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Bank Placement";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Placement Date";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Interest %";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Tenor";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Interest";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Net Amount";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            //area header

                                            int _no = 1;
                                            int _noA = 1;
                                            int _noB = 1;


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //ThickBox Border HEADER
                                                //int RowD = incRowExcel;
                                                //int RowE = incRowExcel + 1;

                                                //area detail
                                                if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.CounterpartCode;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.InstrumentName;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.DonePrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalBrokerFee;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.WHTAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.AcqPrice;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.Realised;
                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    }

                                                }

                                                else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _noA;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.CounterpartCode;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.InstrumentName;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.DonePrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.DoneAccruedInterest;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.WHTAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.AcqPrice;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.Realised;
                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    }

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _noB;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankName;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Tenor;
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAccruedInterest;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                }

                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                _noA++;
                                                _noB++;
                                                if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                                {
                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }

                                                }
                                                else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                                {
                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }

                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                    worksheet.Cells["D" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["D" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }


                                                incRowExcel++;

                                            }

                                            if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                            {
                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();

                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Calculate();
                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();
                                                }

                                            }
                                            else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                            {
                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + RowB + ":O" + RowB].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();

                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Calculate();

                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();
                                                }


                                            }
                                            else
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                worksheet.Cells["C" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 5].Calculate();

                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Calculate();

                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }


                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                            incRowExcel++;

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).AutoFit();
                                        worksheet.Column(12).AutoFit();
                                        worksheet.Column(13).AutoFit();
                                        worksheet.Column(14).AutoFit();
                                        worksheet.Column(15).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Transaction Summary";



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
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
            #endregion

            #region Pencairan Deposito
            if (_FundAccountingRpt.ReportName.Equals("Pencairan Deposito"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"Declare @EndDayTrailsPK int
                            Declare @MaxDateEndDayFP datetime
                            Declare @PeriodPK int

                            select @PeriodPK = PeriodPK from Period where status = 2 and @valuedate between DateFrom and DateTo

                            select @EndDayTrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @valuedate 
                            and status = 2) and status = 2

                            select 'Jakarta' Kota,Reference,C.ID BankCustodian,D.ID BranchName,C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 and A.trxType = 2 
                            union all


                            select 'Jakarta' Kota,A.Reference, 
                            BC.ID BankCustodian,D.ID BranchName,BC.Address,BC.ContactPerson Attn1,BC.Phone1 PhoneAttn1,BC.Fax1 FaxAttn1,BC.Phone2 PhoneAttn2,
                            B.ContactPerson CC1,B.Phone1 PhoneCC1, B.Phone2 PhoneCC2,B.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            B.BankAccountNo NomorRekeningInstrument,
                            B.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            C.BICode BICCode,
                            C.Name BankPlacement,
                            G.BankAccountNo NomorRekeningFund,
                            G.BankAccountName NamaRekeningFund,
                            F.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month

                            from Investment A
                            left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join InstrumentType D on I.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            left join Fund F on A.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            left join Currency E on F.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join FundCashRef G on A.FundCashRefPK = G.FundCashRefPK and G.Status in (1,2)
                            where A.InstrumentTypePK = 5 and A.MaturityDate = @valuedate  and StatusSettlement in (2)
                            and A.InstrumentPK not in  (
                            select InstrumentPK	from Investment where StatusSettlement = 2 and TrxType in (2) and MaturityDate = @valuedate
                            ) and A.PeriodPK = @PeriodPK



                            union all
                       
                            select 'Jakarta' Kota,CONVERT(varchar(10), A.FundEndYearPortfolioPK) + '/FP/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference, 
                            BC.ID BankCustodian,D.ID BranchName,BC.Address,BC.ContactPerson Attn1,BC.Phone1 PhoneAttn1,BC.Fax1 FaxAttn1,BC.Phone2 PhoneAttn2,
                            B.ContactPerson CC1,B.Phone1 PhoneCC1, B.Phone2 PhoneCC2,B.Fax1 FaxCC1,'' Remarks,'' Instruksi,
                            E.ID Currency,A.Volume Jumlah,@valuedate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            B.BankAccountNo NomorRekeningInstrument,
                            B.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            C.BICode BICCode,
                            C.Name BankPlacement,
                            G.BankAccountNo NomorRekeningFund,
                            G.BankAccountName NamaRekeningFund,
                            F.Name Fund,'' RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month

                            from FundEndYearPortfolio A
                            left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join InstrumentType D on I.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            left join Fund F on A.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            left join Currency E on F.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
	                         left join FundCashRef G on G.FundPK = F.FundPK and G.Status in (1,2)
                            where A.MaturityDate = @valuedate and I.InstrumentTypePK = 5 and A.PeriodPK = @PeriodPK ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PencairanDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PencairanDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PencairanDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pencairan Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            //rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchName"]));  
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 3;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. " + rsDetail.RefNo + "/STL-FA-ARO/" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth,";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + " ( Telp. " + rsDetail.PhoneAttn1 + " / " + rsDetail.PhoneAttn2 + " ) ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.CC1 + " ( Telp." + rsDetail.PhoneCC1 + " / " + rsDetail.PhoneCC2 + " Fax. " + rsDetail.FaxCC1 + " ), " + rsDetail.BankPlacement;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal";
                                                worksheet.Cells[incRowExcel, 2].Value = " : Pencairan Deposito a/n  " + rsDetail.Fund + " di " + rsDetail.BankPlacement + " - " + rsDetail.BranchName;

                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan Hormat";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, maka kami bersama ini konfirmasikan pencairan deposito atas penempatan dana sebelumnya sebagai berikut : ";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy");
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " pa ";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Mohon principal dan bunga dapat ditransfer ke: ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nomor Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Atas perhatian dan kerjasamanya kami ucapkan terima kasih.";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat Kami,";
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 1].Value = "Steve Novento";
                                                worksheet.Cells[incRowExcel, 3].Value = "Zidi Kristian";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "On Behalf Of " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 3].Value = "On Behalf Of " + rsDetail.Fund;




                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                                incRowExcel = incRowExcel + 5;

                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
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

            #endregion

            #region Placement Deposito
            if (_FundAccountingRpt.ReportName.Equals("Placement Deposito"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"select 'Jakarta' Kota,Reference,C.ID BankCustodian,D.ID BranchName, C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month,* from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PlacementDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PlacementDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PlacementDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Placement Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));  
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchName"]));  
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 3;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. " + rsDetail.RefNo + "/ STL-FA-ARO /" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth,";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + " ( Telp. " + rsDetail.PhoneAttn1 + " / " + "Telp. " + rsDetail.PhoneAttn2 + " ) ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.CC1 + " ( Telp" + rsDetail.PhoneCC1 + "Telp. " + rsDetail.PhoneCC2 + " Fax. " + rsDetail.FaxCC1 + " ), " + rsDetail.BankPlacement;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal";
                                                worksheet.Cells[incRowExcel, 2].Value = " : Penempatan Deposito a/n " + rsDetail.Fund + " di " + rsDetail.BankPlacement + " - " + rsDetail.BranchName;

                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan Hormat,";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, maka kami bersama ini konfirmasikan penempatan dana dengan kondisi sebagai berikut : ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " pa ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy") + " (apabila dicairkan maka bunga berjalan diperhitungkan) ";
                                                incRowExcel = incRowExcel + 2;
                                                int RowA = incRowExcel;
                                                int RowB = incRowExcel + 1;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dana tersebut akan ditransfer dari " + rsDetail.BankCustodian + " (att. " + rsDetail.Attn1 + " ( Telp" + rsDetail.PhoneAttn1 + "/" + rsDetail.PhoneAttn2 + "))" + " pada tanggal " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = ("melalui fasilitas (RTGS) ke : ");

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankPlacement;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningInstrument;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nomor Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningInstrument;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kode BIC";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BICCode;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Pada saat jatuh tempo, principal (sesuai instruksi) dan bunga mohon ditransfer ke:";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nomor Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Adapun sertifikat deposito mohon dikirim kepada custodian kami.";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Atas perhatian dan kerjasamanya kami ucapkan terima kasih.";
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat Kami,";
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 1].Value = "Steve Novento";
                                                worksheet.Cells[incRowExcel, 3].Value = "Zidi Kristian";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "On Behalf Of " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 3].Value = "On Behalf Of " + rsDetail.Fund;




                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                                //incRowExcel = incRowExcel + 5;

                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();




                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
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

            #endregion


            #region Perpanjangan Deposito
            if (_FundAccountingRpt.ReportName.Equals("Perpanjangan Deposito"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"select 'Jakarta' Kota,Reference,C.ID BankCustodian,C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month,* from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 and A.TrxType = 3 ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PerpanjanganDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PerpanjanganDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PerpanjanganDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Perpanjangan Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 3;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. " + rsDetail.RefNo + "/STL-FA-ARO/" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth,";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + " ( Telp. " + rsDetail.PhoneAttn1 + " / " + rsDetail.PhoneAttn2 + " ) ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.CC1 + " ( Telp." + rsDetail.PhoneCC1 + " / " + rsDetail.PhoneCC2 + " Fax. " + rsDetail.FaxCC1 + " ), " + rsDetail.BankPlacement;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal";
                                                worksheet.Cells[incRowExcel, 2].Value = " : Rollver Deposito a/n " + rsDetail.Fund + " di " + rsDetail.BankPlacement + " - " + rsDetail.BankCustodian;

                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan Hormat";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, maka kami bersama ini konfirmasikan perpanjangan deposito dengan kondisi sebagai berikut : ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " pa ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy");
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Mohon pada saat jatuh tempo, principal dan bunga mohon ditransfer ke:";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nomor Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Adapun sertifikat deposito mohon dikirim kepada custodian kami.";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Atas perhatian dan kerjasamanya kami ucapkan terima kasih.";
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat Kami,";
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 1].Value = "Steve Novento";
                                                worksheet.Cells[incRowExcel, 3].Value = "Zidi Kristian";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "On Behalf Of " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 3].Value = "On Behalf Of " + rsDetail.Fund;




                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                                incRowExcel = incRowExcel + 5;

                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
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

            #endregion

            #region Laporan Laba Rugi Saham
            if (_FundAccountingRpt.ReportName.Equals("Laporan Laba Rugi Saham"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramInstrument = "";
                            string _paramFund = "";
                            if (!_host.findString(_FundAccountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.InstrumentFrom))
                            {
                                _paramInstrument = "And B.InstrumentPK  in ( " + _FundAccountingRpt.InstrumentFrom + " ) ";
                            }
                            else
                            {
                                _paramInstrument = "";
                            }


                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And D.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText = @"
	                        Declare @PeriodPK int

                                declare @LastDayOfLastYear datetime
                                set @LastDayOfLastYear = DATEADD(yy, DATEDIFF(yy, 0, dateadd(year,-1,@DateTo)) + 1, -1)


                                select @PeriodPK = PeriodPK from period where @DateFrom between DateFrom and DateTo and status = 2

                                Select A.InstrumentPK,A.FundPK,D.Name FundName,B.ID Saham,A.ValueDate TradeDate,A.SettlementDate,  
                                A.TrxTypeID Type,case when A.TrxType = 1 then DoneVolume else DoneVolume * -1 end  Quantity,
                                 [dbo].[FGetLastAvgFromInvestmentWithTotalAmount] (A.ValueDate,A.InstrumentPK,A.FundPK) CostPrice,
                                isnull(DoneVolume,0)  * [dbo].[FGetLastAvgFromInvestmentWithTotalAmount] (A.ValueDate,A.InstrumentPK,A.FundPK) * case when A.TrxType = 1 then 1 else -1 end TotalCost,
                                Case when A.TrxType = 1 then 0 else A.TotalAmount / A.donevolume end SellPrice,
                                Case when A.TrxType = 1 then 0 else A.TotalAmount  end Proceed,
                                isnull(C.Volume,0) BegBalanceVolume, 
                                [dbo].[FGetLastAvgFromInvestmentWithTotalAmount] (@LastDayOfLastYear,A.InstrumentPK,A.FundPK) BegBalanceCostPrice,
                                isnull(C.Volume,0) * [dbo].[FGetLastAvgFromInvestmentWithTotalAmount] (@LastDayOfLastYear,A.InstrumentPK,A.FundPK) BegBalanceTotalCost, isnull(E.ID,'') CounterpartID 
                                from Investment A

                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                                left join FundEndYearPortfolio C on A.InstrumentPK = C.InstrumentPK and A.FundPK = C.FundPK and C.PeriodPK = @PeriodPK
                                left join Fund D on A.FundPK = D.FundPK and D.status in (1,2)
                                left join Counterpart E on A.CounterpartPK = E.CounterpartPK and E.status in (1,2)
    
                                where A.StatusSettlement = 2 "
                                + _paramInstrument + _paramFund + @"
                                and A.ValueDate between @DateFrom and @DateTo

                                order by A.ValueDate asc";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanSaham" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanSaham" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Laporan Saham");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<LaporanSaham> rList = new List<LaporanSaham>();
                                        while (dr0.Read())
                                        {
                                            LaporanSaham rSingle = new LaporanSaham();
                                            rSingle.Trade = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.Settle = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.Type = dr0["Type"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Type"]);
                                            rSingle.Quantity = dr0["Quantity"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Quantity"]);
                                            rSingle.CostPrice = dr0["CostPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostPrice"]);
                                            rSingle.TotalCost = dr0["TotalCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalCost"]);
                                            rSingle.SellPrice = dr0["SellPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["SellPrice"]);
                                            rSingle.Proceed = dr0["Proceed"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Proceed"]);
                                            //rSingle.GainLoss = dr0["GainLoss"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["GainLoss"]);
                                            //rSingle.Total = dr0["Total"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Total"]);
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentID = dr0["Saham"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Saham"]);
                                            rSingle.BegBalanceVolume = dr0["BegBalanceVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BegBalanceVolume"]);
                                            rSingle.BegBalanceCostPrice = dr0["BegBalanceCostPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BegBalanceCostPrice"]);
                                            rSingle.BegBalanceTotalCost = dr0["BegBalanceTotalCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BegBalanceTotalCost"]);
                                            rSingle.CounterpartID = dr0["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartID"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByAccountID =
                                            from r in rList
                                            orderby r.InstrumentID ascending
                                            group r by new { r.FundName, r.InstrumentID, r.BegBalanceVolume, r.BegBalanceCostPrice, r.BegBalanceTotalCost } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;
                                        int _rowEndBalance = 0;


                                        int _startRowDetail = 0;
                                        int _endRowDetail = 0;

                                        foreach (var rsHeader in GroupByAccountID)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 18;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "LAPORAN LABA & RUGI";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "SAHAM";
                                            worksheet.Cells[incRowExcel, 2].Value = "TRADE";
                                            worksheet.Cells[incRowExcel, 3].Value = "SETTLE";
                                            worksheet.Cells[incRowExcel, 4].Value = "TYPE";
                                            worksheet.Cells[incRowExcel, 5].Value = "QUANTITY";
                                            worksheet.Cells[incRowExcel, 6].Value = "COST PRICE (Include fee)";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 35;
                                            worksheet.Cells[incRowExcel, 7].Value = "TOTAL COST";
                                            worksheet.Cells[incRowExcel, 8].Value = "SELL PRICE";
                                            worksheet.Cells[incRowExcel, 9].Value = "PROCEED";
                                            worksheet.Cells[incRowExcel, 10].Value = "GAIN(LOSS)";
                                            worksheet.Cells[incRowExcel, 11].Value = "TOTAL";
                                            worksheet.Cells[incRowExcel, 12].Value = "COUNTERPART";

                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Color.SetColor(Color.White);
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Black);
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = incRowExcel + 2;

                                            _startRowDetail = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.InstrumentID;
                                            worksheet.Cells[incRowExcel, 5].Value = rsHeader.Key.BegBalanceVolume;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.BegBalanceCostPrice;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Value = rsHeader.Key.BegBalanceTotalCost;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail
                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.Trade).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.Settle).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Quantity;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CostPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalCost;

                                                if (rsDetail.Type == "SELL")
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.SellPrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Proceed;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(I" + incRowExcel + "+G" + incRowExcel + ")";
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 10].Calculate();
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                }


                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.CounterpartID;
                                                //worksheet.Cells[incRowExcel, 10].Value = rsDetail.GainLoss;
                                                //worksheet.Cells[incRowExcel, 11].Value = rsDetail.Total;
                                                incRowExcel++;
                                                _endRowDetail = incRowExcel;
                                            }
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "IFERROR(SUM(G" + incRowExcel + "/E" + incRowExcel + "),0)";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;

                                            incRowExcel++;

                                            //worksheet.Cells["A" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + _startRowDetail + ":G" + _endRowDetail].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["B" + _startRowDetail + ":G" + _endRowDetail].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                            incRowExcel++;

                                        }
                                        int _rowBottom = incRowExcel - 2;
                                        worksheet.Cells[_rowBottom, 9].Value = "GRAND TOTAL";

                                        worksheet.Cells[_rowBottom, 11].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[_rowBottom, 11].Formula = "SUM(J" + 6 + ":J" + _endRowDetail + ")";
                                        worksheet.Cells[_rowBottom, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[_rowBottom, 11].Calculate();
                                        worksheet.Cells[_rowBottom, 11].Style.Font.Bold = true;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 2, 12];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 15;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 LAPORAN SAHAM";


                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Portfolio Valuation Report
            else if (_FundAccountingRpt.ReportName.Equals("Portfolio Valuation Report"))
            {
                try
                {

                    string filePath = Tools.ReportsPath + "PortfolioValuationReport" + "_" + _userID + ".xlsx";
                    string pdfPath = Tools.ReportsPath + "PortfolioValuationReport" + "_" + _userID + ".pdf";
                    FileInfo excelFile = new FileInfo(filePath);
                    if (excelFile.Exists)
                    {
                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        excelFile = new FileInfo(filePath);
                    }
                    ExcelPackage package = new ExcelPackage(excelFile);
                    package.Workbook.Properties.Title = "UnitRegistryReport";
                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report");

                    int _startAsset = 0;
                    int _RowNominal = 0;
                    int _rowMarketValue1 = 0;
                    int _rowMarketValue2 = 0;
                    int _this = 0;
                    bool _row1 = false;
                    bool _row2 = false;
                    bool _row3 = false;
                    bool _row4 = false;
                    //ATUR DATA GROUPINGNYA DULU
                    int incRowExcel = 1;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                                @"
                                
                                    Select '2' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,2,@FundPK) Balance -- CASH AT BANK
                                    UNION ALL
                                    Select '3' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,37,@FundPK) Balance -- INTEREST RECEIVABLE (ACCRUAL) - CURRENT ACCOUNT
                                    UNION ALL
                                    Select '4' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,38,@FundPK) Balance -- 	INTEREST RECEIVABLE (ACCRUAL) - TIME DEPOSIT
                                    UNION ALL
                                    Select '5' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,34,@FundPK) Balance -- INTEREST RECEIVABLE (ACCRUAL) - BOND
                                    UNION ALL
                                    Select '6' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,26,@FundPK) Balance -- INTEREST RECEIVABLE (BUY/SELL) - BOND
                                    UNION ALL
                                    Select '7' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,33,@FundPK) Balance -- 	INTEREST RECEIVABLE (BUY/SELL) - TIME DEPOSIT
                                    UNION ALL
                                    Select '8' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,52,@FundPK) Balance -- RECEIVABLE - DIVIDEND FROM EQUITY
                                    UNION ALL
                                    Select '9' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,53,@FundPK) Balance -- RECEIVABLE - CONSENT FEE FROM BONDS
                                    UNION ALL
                                    Select '10' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,60,@FundPK) Balance -- RECEIVABLE - OTHER	
                                    UNION ALL
                                    Select '11' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,59,@FundPK) Balance -- RECEIVABLE - REFUND
                                    UNION ALL
                                    Select '12' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,61,@FundPK) Balance -- RECEIVABLE - CLAIMS
                                    UNION ALL
                                    Select '13' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,195,@FundPK) Balance -- OTHER ASSETS	
                                    UNION ALL
                                    Select '14' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,42,@FundPK) Balance -- RECEIVABLE SALE - EQUITY
                                    UNION ALL
                                    Select '15' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,45,@FundPK) Balance -- RECEIVABLE SALE - NEGOTIABLE CERTIFICATE OF DEPOSIT
                                    UNION ALL
                                    Select '16' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,49,@FundPK) Balance -- RECEIVABLE SALE - TIME DEPOSIT
                                    UNION ALL
                                    Select '17' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,50,@FundPK) Balance -- RECEIVABLE SALE - BOND
                                    UNION ALL
                                    Select '18' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,56,@FundPK) Balance -- PREPAID TAX - PPH 23 (DIVIDEND)
                                    UNION ALL
                                    Select '19' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,57,@FundPK) Balance   -- PREPAID TAX - PPH 25
                                    UNION ALL
                                    Select '20' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,197,@FundPK) Balance -- PREPAID TAX - OTHER
                                    UNION ALL
                                    Select '23' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,79,@FundPK) * -1 Balance -- PAYABLE - MANAGEMENT FEE
                                    UNION ALL
                                    Select '24' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,80,@FundPK) * -1 Balance -- PAYABLE - CUSTODIAN FEE
                                    UNION ALL
                                    Select '25' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,202,@FundPK) * -1  Balance -- PAYABLE - SINVEST FEE
                                    UNION ALL
                                    Select '26' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,78,@FundPK) * -1 Balance -- PAYABLE - MOVEMENT FEE (CUST)
                                    UNION ALL
                                    Select '27' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,77,@FundPK) * -1 Balance -- PAYABLE - BROKER FEE (C-BEST)
                                    UNION ALL
                                    Select '28' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,81,@FundPK) * -1 Balance -- PAYABLE - AUDIT FEE
                                    UNION ALL
                                    Select '29' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,82,@FundPK) * -1 Balance -- PAYABLE - SELLING FEE
                                    UNION ALL
                                    Select '30' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,104,@FundPK) * -1 Balance -- PAYABLE - REPORTING FEE
                                    UNION ALL
                                    Select '31' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,100,@FundPK) * -1 Balance -- PAYABLE - BANK TRANSFER CHARGE
                                    UNION ALL
                                    Select '32' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,101,@FundPK) * -1 Balance -- PAYABLE - PROSPEKTUS FEE
                                    UNION ALL
                                    Select '33' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,102,@FundPK) * -1 Balance -- PAYABLE - CLAIMS
                                    UNION ALL
                                    Select '34' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,105,@FundPK) * -1 Balance -- PAYABLE - OTHER FEE
                                    UNION ALL
                                    Select '35' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,196,@FundPK) * -1 Balance -- OTHER LIABILITIES
                                    UNION ALL
                                    Select '36' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,94,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - ACCRUED INTEREST
                                    UNION ALL
                                    Select '37' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,93,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - CAPITAL GAIN
                                    UNION ALL
                                    Select '38' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,95,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - PPH23 COMM. FROM BROKER	
                                    UNION ALL
                                    Select '39' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,96,@FundPK)* -1 Balance -- PAYABLE - INCOME TAX
                                    UNION ALL
                                    Select '40' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,97,@FundPK)* -1 Balance -- PAYABLE - ESTIMATION TAX
                                    UNION ALL
                                    Select '41' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,98,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - AUDIT FEE
                                    UNION ALL
                                    Select '42' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,194,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE
                                    UNION ALL
                                    Select '43' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,66,@FundPK) * -1 Balance -- PAYABLE PURCHASE - EQUITY
                                    UNION ALL
                                    Select '44' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,67,@FundPK) * -1 Balance -- PAYABLE PURCHASE - BOND
                                    UNION ALL
                                    Select '45' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,71,@FundPK) * -1 Balance -- PAYABLE PURCHASE - NEGOTIABLE CERTIFICATE OF DEPOSIT
                                    UNION ALL
                                    Select '46' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,75,@FundPK) * -1 Balance -- PAYABLE PURCHASE - TIME DEPOSIT
                                    UNION ALL
                                    Select '47' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,89,@FundPK) * -1 Balance -- PENDING SUBSCRIPTION
                                    UNION ALL
                                    Select '48' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,85,@FundPK) * -1 Balance -- PAYABLE - SUBSCRIPTION FEE
                                    UNION ALL
                                    Select '49' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,84,@FundPK) * -1 Balance --PAYABLE - REDEMPTION
                                    UNION ALL
                                    Select '50' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,86,@FundPK) * -1 Balance -- PAYABLE - REDEMPTION FEE
                                    UNION ALL
                                    Select '51' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,87,@FundPK) * -1 Balance -- PAYABLE - DISTRIBUTED INCOME
                                    UNION ALL
                                    Select '52' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,88,@FundPK) * -1 Balance -- PAYABLE - FROM SWITCH OUT
                                    UNION ALL
                                    Select '53' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,90,@FundPK) * -1 Balance -- DISTRIBUTION INCOME PAYABLE
                                    UNION ALL
                                    Select '54' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,103,@FundPK) * -1 Balance --PayableSInvestFee
                                 ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {

                                if (dr0.HasRows)
                                {
                                    _row1 = dr0.HasRows;
                                    List<PVRRpt> rList = new List<PVRRpt>();
                                    while (dr0.Read())
                                    {
                                        PVRRpt rSingle = new PVRRpt();
                                        rSingle.Balance = dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.Row = dr0["Row"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["Row"]);

                                        rList.Add(rSingle);

                                    }
                                    var QueryByFundID =
                                            from r in rList
                                            group r by new { } into rGroup
                                            select rGroup;

                                    foreach (var rsHeader in QueryByFundID)
                                    {
                                        worksheet.Cells[incRowExcel, 1].Value = "ASSET";
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Font.Size = 13;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        incRowExcel++;

                                        _startAsset = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = "1";
                                        worksheet.Cells[incRowExcel, 2].Value = "CASH AT BANK";
                                        worksheet.Cells[incRowExcel, 6].Value = "TOTAL NET ASSET VALUE";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundAUM(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 8].Value = "(Last Day NAV)";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "2";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - CURRENT ACCOUNT";
                                        worksheet.Cells[incRowExcel, 6].Value = "TOTAL OUTSTANDING UNIT";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundUnitPosition(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Value = _host.Get_LastNavYesterday(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "3";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "NET ASSET VALUE PER UNIT";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundAUM(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom) / _host.Get_FundUnitPosition(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "4";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - BOND";
                                        worksheet.Cells[incRowExcel, 6].Value = "CHANGE / DAY";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_ChangeNavPerDay(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Value = "(Last Year NAV)";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "5";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (BUY/SELL) - BOND";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD IN THE LAST 30 DAYS";
                                        worksheet.Cells[incRowExcel, 8].Value = _host.Get_NavLastYear(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "6";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (BUY/SELL) - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD IN THE LAST 1 YEARS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "7";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - DIVIDEND FROM EQUITY";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD YEAR to DATE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "8";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - CONSENT FEE FROM BONDS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "9";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - OTHER";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "10";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - REFUND";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "11";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - CLAIMS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "12";
                                        worksheet.Cells[incRowExcel, 2].Value = "OTHER ASSETS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "13";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - EQUITY";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "14";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - NEGOTIABLE CERTIFICATE OF DEPOSIT";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "15";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "16";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - BOND";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "17";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - PPH 23 (DIVIDEND)";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "18";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - PPH 25";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "19";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - OTHER";
                                        int _endAsset = incRowExcel;
                                        incRowExcel++;

                                        int _end = incRowExcel - 13;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Fill.BackgroundColor.SetColor(Color.MediumPurple);
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LIABILITIES";
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Font.Size = 13;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        incRowExcel++;
                                        int _startLiabilities = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = "20";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - MANAGEMENT FEE";

                                        int _startCrossCheck = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Crosscheck";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "21";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - CUSTODIAN FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "22";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SINVEST FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "Selisih";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G2" + "-G18" + ")";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "23";
                                        worksheet.Cells[incRowExcel, 2].Value = "A/P MOVEMENT FEE (CUST)";

                                        worksheet.Cells[incRowExcel, 6].Value = "Unrealized";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "24";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - BROKER FEE (C-BEST)";

                                        int _endCrossCheck = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Selisih";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "25";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - AUDIT FEE";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "26";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SELLING FEE";

                                        int _startcheckBy = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Prepared by";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "27";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REPORTING FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "Checked by";

                                        worksheet.Cells[incRowExcel, 1].Value = "38";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - BANK TRANSFER CHARGE";
                                        incRowExcel++;

                                        int _endcheckBy = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Investment Manager";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "29";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - PROSPEKTUS FEE";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "30";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - CLAIMS";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "31";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - OTHER FEE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "32";
                                        worksheet.Cells[incRowExcel, 2].Value = "OTHER LIABILITIES";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "33";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - ACCRUED INTEREST";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "34";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - CAPITAL GAIN";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "35";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - PPH23 COMM. FROM BROKER";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "36";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - INCOME TAX";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "37";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - ESTIMATION TAX";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "38";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - AUDIT FEE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "39";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "40";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - EQUITY";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "41";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - BOND";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "42";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - NEGOTIABLE CERTIFICATE OF DEPOSIT";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "43";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - TIME DEPOSIT";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "44";
                                        worksheet.Cells[incRowExcel, 2].Value = "PENDING SUBSCRIPTION";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "45";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SUBSCRIPTION FEE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "46";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REDEMPTION";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "47";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REDEMPTION FEE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "48";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - DISTRIBUTED INCOME";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "49";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - FROM SWITCH OUT";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "50";
                                        worksheet.Cells[incRowExcel, 2].Value = "DISTRIBUTION INCOME PAYABLE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "51";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - OTHER";
                                        int _endLiabilities = incRowExcel;

                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                        worksheet.Cells["C" + 23 + ":C" + 54].Style.Font.Color.SetColor(Color.Red);
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[rsDetail.Row, 3].Value = rsDetail.Balance;
                                            worksheet.Cells[rsDetail.Row, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[rsDetail.Row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;

                                        }

                                        incRowExcel++;
                                        worksheet.Cells[55, 2, 55, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[55, 2, 55, 3].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells[55, 2, 55, 3].Style.Font.Bold = true;
                                        worksheet.Cells[55, 2].Value = "Total Cash & Equivalent";

                                        worksheet.Cells[55, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[55, 3].Formula = "SUM(C2" + ":C54" + ")";
                                        worksheet.Cells[55, 3].Calculate();
                                        incRowExcel++;

                                    }
                                }

                                //-----------------------------------
                                using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                {
                                    DbCon1.Open();
                                    using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                    {
                                        //                                              
                                        cmd1.CommandText =
                                        @"
                                                select isnull(B.Name,'') FundName,isnull(C.ID,'') SecuritiesCode,isnull(C.Name,'') SecuritiesDescription, 
                                                isnull(A.Balance,0) QtyOfUnit, isnull(A.Balance,0) / 100 Lot, 
                                                isnull(A.AvgPrice,0) AvgCost,
                                                isnull(A.Balance,0) * isnull(A.AvgPrice,0) BookValue,
                                                isnull(A.ClosePrice,0) MarketPrice,
                                                isnull(A.Balance,0) * isnull(A.ClosePrice,0) MarketValue,
                                                (isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))  Unrealised,
                                                case when (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) > 0 then
                                                ((isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))) /  (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) * 100 
                                                else 0 end UnrealisedPercent,
                                                '' ByMarketCap,
                                                isnull(D.ID,'') SubSector,
                                                isnull(E.Name,'') Sector
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join SubSector D on C.SectorPK = D.SubSectorPK and D.status in (1,2)
                                                left join Sector E on D.SectorPK = E.SectorPK and E.status in (1,2)
                                                where A.status = 2 and A.Date = @Date
                                                and A.FundPK= @FundPK
                                                and C.InstrumentTypePK in (1,4,16)
                                                ";
                                        cmd1.CommandTimeout = 0;

                                        cmd1.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                        cmd1.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd1.ExecuteNonQuery();


                                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        {
                                            if (dr1.HasRows)
                                                _row2 = dr1.HasRows;
                                            {
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList1 = new List<PVRRpt>();
                                                    while (dr1.Read())
                                                    {
                                                        PVRRpt rSingle1 = new PVRRpt();
                                                        rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                        rSingle1.InstrumentID = Convert.ToString(dr1["SecuritiesCode"]);
                                                        rSingle1.InstrumentName = dr1["SecuritiesDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr1["SecuritiesDescription"]);
                                                        rSingle1.UnitQuantity = dr1["QtyOfUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["QtyOfUnit"]);
                                                        rSingle1.AverageCost = dr1["AvgCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["AvgCost"]);
                                                        rSingle1.BookValue = dr1["BookValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["BookValue"]);
                                                        rSingle1.MarketPrice = Convert.ToDecimal(dr1["MarketPrice"]);
                                                        rSingle1.MarketValue = Convert.ToDecimal(dr1["MarketValue"]);
                                                        rSingle1.UnrealizedProfitLoss = Convert.ToDecimal(dr1["Unrealised"]);
                                                        rSingle1.PercentProfilLoss = Convert.ToDecimal(dr1["UnrealisedPercent"]);
                                                        rSingle1.Lot = Convert.ToDecimal(dr1["Lot"]);
                                                        rSingle1.MarketCap = Convert.ToString(dr1["ByMarketCap"]);
                                                        rSingle1.Sector = Convert.ToString(dr1["Sector"]);
                                                        //rSingle1.PercentTA = Convert.ToDecimal(dr1["PercentTA"]);
                                                        //rSingle1.PercentYTM = Convert.ToDecimal(dr0["PercentYTM"]);
                                                        //rSingle1.MDur = Convert.ToDecimal(dr0["MDur"]);
                                                        //rSingle1.CouponRate = Convert.ToDecimal(dr0["CouponRate"]);
                                                        //rSingle1.Compliance = Convert.ToDecimal(dr1["Compliance"]);
                                                        //rSingle1.RatingObligasi = Convert.ToDecimal(dr0["RatingObligasi"]);
                                                        //rSingle1.ComplianceIBPA = Convert.ToDecimal(dr0["ComplianceIBPA"]);
                                                        //rSingle1.BondType = Convert.ToDecimal(dr0["BondType"]);

                                                        rList1.Add(rSingle1);

                                                    }


                                                    var QueryByFundID1 =
                                                        from r1 in rList1
                                                        group r1 by new { r1.FundName } into rGroup1
                                                        select rGroup1;
                                                    incRowExcel = incRowExcel - 50;
                                                    int _StartRow = incRowExcel + 1;
                                                    foreach (var rsHeader1 in QueryByFundID1)
                                                    {
                                                        _this = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yy");
                                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.Font.Size = 14;

                                                        incRowExcel++;
                                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader1.Key.FundName;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        incRowExcel = incRowExcel + 2;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                                        worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                        worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                        worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                        worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Lot";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                        worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                        worksheet.Cells[incRowExcel, 11].Value = "% fr P/L";
                                                        worksheet.Cells[incRowExcel, 12].Value = "BY MARKET CAP";
                                                        worksheet.Cells[incRowExcel, 13].Value = "BY SECTOR";
                                                        worksheet.Cells[incRowExcel, 14].Value = "% fr TA";
                                                        worksheet.Cells[incRowExcel, 15].Value = "Beta";
                                                        worksheet.Cells[incRowExcel, 16].Value = "% Segment";
                                                        worksheet.Cells[incRowExcel, 17].Value = "Compliance";
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel = incRowExcel + 2;

                                                        worksheet.Cells[incRowExcel, 2].Value = "STOCKS";
                                                        worksheet.Cells[incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells[incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                                        incRowExcel++;
                                                        //area header

                                                        int _no = 1;
                                                        int _startRowDetail = incRowExcel;
                                                        int _endRowDetail = 0;
                                                        var _fundID = "";
                                                        foreach (var rsDetail1 in rsHeader1)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["C" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["K" + incRowExcel + ":M" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["N" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail1.InstrumentName;
                                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitQuantity;
                                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail1.Lot;
                                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail1.AverageCost;
                                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail1.BookValue;
                                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail1.MarketPrice;
                                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail1.MarketValue;
                                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail1.UnrealizedProfitLoss;
                                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail1.PercentProfilLoss / 100;
                                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00 %";

                                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail1.MarketCap;
                                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail1.Sector;

                                                            worksheet.Cells[incRowExcel, 14].Formula = "I" + incRowExcel + "/G2" + "";
                                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            //ThickBox Border
                                                            _endRowDetail = incRowExcel;
                                                            incRowExcel++;
                                                            _no++;
                                                        }

                                                        incRowExcel++;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL";

                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 4].Calculate();

                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 5].Calculate();

                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                                        _rowMarketValue1 = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + incRowExcel + "/G" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 11].Calculate();

                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(I" + incRowExcel + "/G" + 2 + ")";
                                                        worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 14].Calculate();

                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 15].Calculate();

                                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 16].Calculate();

                                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 17].Calculate();

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                incRowExcel = incRowExcel + 2;

                                //-----------------------------------
                                using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                {
                                    DbCon2.Open();
                                    using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                    {
                                        //                                              
                                        cmd2.CommandText =
                                        @"
                                                select isnull(C.ID,'') SecuritiesCode,isnull(C.Name,'') SecuritiesDescription, 
                                                isnull(A.Balance,0) QtyOfUnit, C.MaturityDate, 
                                                isnull(A.AvgPrice,0) AvgCost,
                                                isnull(A.Balance,0) * isnull(A.AvgPrice,0) BookValue,
                                                isnull(A.ClosePrice,0) MarketPrice,
                                                isnull(A.Balance,0) * isnull(A.ClosePrice,0) MarketValue,
                                                (isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))  Unrealised,
                                                case when (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) > 0 then
                                                ((isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))) /  (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) * 100 else 0 end UnrealisedPercent,
                                                 isnull(D.Name,'') BondType
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join instrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                                                where A.status = 2 
                                                and A.Date = @Date
                                                and A.FundPK= @FundPK
						                        and C.InstrumentTypePK in (2,3,8,9,11,12,13,14,15)
                                                ";
                                        cmd2.CommandTimeout = 0;

                                        cmd2.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                        cmd2.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd2.ExecuteNonQuery();


                                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                        {
                                            if (dr2.HasRows)
                                            {
                                                _row3 = dr2.HasRows;
                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList1 = new List<PVRRpt>();
                                                    while (dr2.Read())
                                                    {
                                                        PVRRpt rSingle1 = new PVRRpt();
                                                        rSingle1.InstrumentID = Convert.ToString(dr2["SecuritiesCode"]);
                                                        rSingle1.InstrumentName = dr2["SecuritiesDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["SecuritiesDescription"]);
                                                        rSingle1.UnitQuantity = dr2["QtyOfUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["QtyOfUnit"]);
                                                        rSingle1.MaturityDate = Convert.ToString(dr2["MaturityDate"]);
                                                        rSingle1.AverageCost = dr2["AvgCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["AvgCost"]);
                                                        rSingle1.BookValue = dr2["BookValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["BookValue"]);
                                                        rSingle1.MarketPrice = Convert.ToDecimal(dr2["MarketPrice"]);
                                                        rSingle1.MarketValue = Convert.ToDecimal(dr2["MarketValue"]);
                                                        rSingle1.UnrealizedProfitLoss = Convert.ToDecimal(dr2["Unrealised"]);
                                                        rSingle1.PercentProfilLoss = Convert.ToDecimal(dr2["UnrealisedPercent"]);
                                                        //rSingle1.PercentTA = Convert.ToDecimal(dr1["PercentTA"]);
                                                        //rSingle1.PercentYTM = Convert.ToDecimal(dr0["PercentYTM"]);
                                                        //rSingle1.MDur = Convert.ToDecimal(dr0["MDur"]);
                                                        //rSingle1.CouponRate = Convert.ToDecimal(dr0["CouponRate"]);
                                                        //rSingle1.Compliance = Convert.ToDecimal(dr1["Compliance"]);
                                                        //rSingle1.RatingObligasi = Convert.ToDecimal(dr0["RatingObligasi"]);
                                                        //rSingle1.ComplianceIBPA = Convert.ToDecimal(dr0["ComplianceIBPA"]);
                                                        rSingle1.BondType = Convert.ToString(dr2["BondType"]);

                                                        rList1.Add(rSingle1);

                                                    }


                                                    var QueryByFundID1 =
                                                        from r1 in rList1
                                                        group r1 by new { } into rGroup1
                                                        select rGroup1;

                                                    incRowExcel = incRowExcel + 6;
                                                    int _StartRow = incRowExcel + 1;
                                                    foreach (var rsHeader1 in QueryByFundID1)
                                                    {

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                        worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                        worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                        worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                        worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                        worksheet.Cells[incRowExcel, 11].Value = "% fr P/L";
                                                        worksheet.Cells[incRowExcel, 12].Value = "% fr TA";
                                                        worksheet.Cells[incRowExcel, 13].Value = "% YTM";
                                                        worksheet.Cells[incRowExcel, 14].Value = "MDur";
                                                        worksheet.Cells[incRowExcel, 15].Value = "Coupon Rate";
                                                        worksheet.Cells[incRowExcel, 16].Value = "Compliance Max. 10%";
                                                        worksheet.Cells[incRowExcel, 17].Value = "Rating Obligasi";
                                                        worksheet.Cells[incRowExcel, 18].Value = "Compliance IBPA";
                                                        worksheet.Cells[incRowExcel, 19].Value = "BONDS TYPE";

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                                        // Row C = 4

                                                        incRowExcel = incRowExcel + 2;
                                                        //area header
                                                        int _no = 1;
                                                        int _startRowDetail1 = incRowExcel;
                                                        int _endRowDetail1 = 0;
                                                        foreach (var rsDetail1 in rsHeader1)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["E" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail1.InstrumentName;
                                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitQuantity;
                                                            worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail1.MaturityDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail1.AverageCost;
                                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail1.BookValue / 100;
                                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail1.MarketPrice;
                                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail1.MarketValue / 100;
                                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail1.UnrealizedProfitLoss / 100;
                                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail1.PercentProfilLoss;
                                                            worksheet.Cells[incRowExcel, 12].Formula = "I" + incRowExcel + "/C31";
                                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                                            worksheet.Cells[incRowExcel, 13].Value = "-";
                                                            worksheet.Cells[incRowExcel, 14].Value = "-";
                                                            worksheet.Cells[incRowExcel, 15].Value = "-";
                                                            worksheet.Cells[incRowExcel, 16].Value = "-";
                                                            worksheet.Cells[incRowExcel, 17].Value = "-";
                                                            worksheet.Cells[incRowExcel, 18].Value = "-";
                                                            worksheet.Cells[incRowExcel, 19].Value = rsDetail1.BondType;
                                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            worksheet.Cells["M" + incRowExcel + ":S" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            incRowExcel++;
                                                            _endRowDetail1 = incRowExcel;
                                                            _no++;
                                                        }
                                                        int _EndRow = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL";

                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail1 + ":D" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 4].Calculate();

                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail1 + ":G" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                                        _rowMarketValue2 = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail1 + ":I" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail1 + ":J" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + incRowExcel + "/G" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 11].Calculate();

                                                        //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 12].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 12].Calculate();

                                                        //worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 13].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 13].Calculate();

                                                        //worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 14].Calculate();

                                                        worksheet.Cells["A" + _startRowDetail1 + ":S" + _EndRow].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _StartRow + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _StartRow + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail1 + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                incRowExcel = incRowExcel + 2;

                                //-----------------------------------
                                using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                {
                                    DbCon2.Open();
                                    using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                    {
                                        //                                              
                                        cmd2.CommandText =
                                        @"
                                                select 
                                                isnull(F.Name,'') SecuritiesCode,
                                                isnull(F.SInvestID,'') BiCode,
                                                isnull(E.ID,'') Branch,
                                                isnull(A.Balance,0) Nominal,
                                                A.AcqDate TradeDate,
                                                A.MaturityDate,
                                                A.InterestPercent Rate,
                                                [dbo].[Fgetdepositointerestaccrued] (@date,A.InstrumentPK,A.Balance,A.InterestDaysType,A.InterestPercent,A.AcqDate) AccruedInterest
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join instrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                                                left join BankBranch E on A.BankBranchPK = E.BankBranchPK and E.status in (1,2)
                                                left join Bank F on E.BankPK = F.BankPK and F.status in (1,2)
                                                where A.status = 2 and A.Date = @Date
                                                and A.FundPK= @FundPK and C.InstrumentTypePK in (5)
                                                ";
                                        cmd2.CommandTimeout = 0;

                                        cmd2.Parameters.AddWithValue("@date", _FundAccountingRpt.ValueDateFrom);
                                        cmd2.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd2.ExecuteNonQuery();


                                        using (SqlDataReader dr3 = cmd2.ExecuteReader())
                                        {
                                            if (dr3.HasRows)
                                            {
                                                _row4 = dr3.HasRows;
                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList2 = new List<PVRRpt>();
                                                    while (dr3.Read())
                                                    {
                                                        PVRRpt rSingle2 = new PVRRpt();
                                                        rSingle2.TimeDeposit = Convert.ToString(dr3["SecuritiesCode"]);
                                                        rSingle2.BICode = Convert.ToString(dr3["BICode"]);
                                                        rSingle2.Branch = Convert.ToString(dr3["Branch"]);
                                                        rSingle2.Nominal = Convert.ToDecimal(dr3["Nominal"]);
                                                        rSingle2.TradeDate = Convert.ToString(dr3["TradeDate"]);
                                                        rSingle2.MaturityDate = Convert.ToString(dr3["MaturityDate"]);
                                                        rSingle2.Rate = Convert.ToDecimal(dr3["Rate"]);
                                                        rSingle2.AccTD = Convert.ToDecimal(dr3["AccruedInterest"]);
                                                        //rSingle2.PercentTA = Convert.ToDecimal(dr2["PercentTA"]);
                                                        //rSingle2.MaturityAlert = Convert.ToString(dr2["MaturityAlert"]);
                                                        rList2.Add(rSingle2);

                                                    }


                                                    var QueryByFundID2 =
                                                        from r2 in rList2
                                                        group r2 by new { } into rGroup2
                                                        select rGroup2;

                                                    incRowExcel = incRowExcel + 6;

                                                    int _startRow = incRowExcel;
                                                    foreach (var rsHeader2 in QueryByFundID2)
                                                    {
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                                        worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSITS";
                                                        worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                                        worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Rate(Gross)";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Acc Int. TD(Net)";
                                                        worksheet.Cells[incRowExcel, 10].Value = "%fr TA";
                                                        worksheet.Cells[incRowExcel, 11].Value = "Mature Alert";
                                                        incRowExcel++;

                                                        // Row C = 4
                                                        int RowCZ = incRowExcel;

                                                        //area header
                                                        int _no = 1;
                                                        int _startRowDetail = incRowExcel;
                                                        int _endRowDetail = 0;
                                                        foreach (var rsDetail2 in rsHeader2)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail2.TimeDeposit;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail2.BICode;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail2.Branch;
                                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail2.Nominal;
                                                            worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail2.TradeDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail2.MaturityDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 8].Value = Convert.ToDecimal(rsDetail2.Rate / 100).ToString("0.00 %");
                                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells[incRowExcel, 9].Formula = "((E" + incRowExcel + "*H" + incRowExcel + "*(A" + _this + "-F" + incRowExcel + "))/365)*0.8/100";
                                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                                            worksheet.Cells[incRowExcel, 10].Formula = "E" + incRowExcel + "/G2";
                                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "0.00 %";
                                                            _endRowDetail = incRowExcel;
                                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            worksheet.Cells["K" + incRowExcel + ":K" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            incRowExcel++;
                                                            _no++;

                                                        }

                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);


                                                        _RowNominal = incRowExcel;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 5].Calculate();

                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;

                                                        incRowExcel++;
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                //if (_row2 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}
                                //else if (_row3 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue1 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}

                                //else if (_row4 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+I" + _rowMarketValue1 + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}
                                //else
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue1 + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}


                                int _lastRow = incRowExcel;

                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                worksheet.PrinterSettings.FitToPage = true;
                                worksheet.PrinterSettings.FitToWidth = 1;
                                worksheet.PrinterSettings.FitToHeight = 0;
                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 19];
                                worksheet.Column(1).Width = 5;
                                worksheet.Column(2).Width = 30;
                                worksheet.Column(3).Width = 25;
                                worksheet.Column(4).Width = 25;
                                worksheet.Column(5).Width = 25;
                                worksheet.Column(6).Width = 25;
                                worksheet.Column(7).Width = 25;
                                worksheet.Column(8).Width = 25;
                                worksheet.Column(9).Width = 25;
                                worksheet.Column(10).Width = 25;
                                worksheet.Column(11).Width = 15;
                                worksheet.Column(12).Width = 25;
                                worksheet.Column(13).Width = 55;
                                worksheet.Column(14).Width = 15;
                                worksheet.Column(15).Width = 25;
                                worksheet.Column(16).Width = 25;
                                worksheet.Column(17).Width = 20;
                                worksheet.Column(18).Width = 20;
                                worksheet.Column(19).Width = 20;



                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B Portfolio Valuation Report";



                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                Image img = Image.FromFile(Tools.ReportImage);
                                worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                package.Save();
                                if (_FundAccountingRpt.DownloadMode == "PDF")
                                {
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                }
                                return true;


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
            #endregion

            #region Broker Stock
            else if (_FundAccountingRpt.ReportName.Equals("Broker Stock"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            //                          
                            cmd.CommandText =
                                @"
                                select A.CounterpartPK,B.ID CounterpartID,B.Name CounterpartName,sum(Case when TrxType = 1 then DoneAmount else 0 end) BUY,
                                sum(Case when TrxType = 2 then DoneAmount else 0 end) SELL
                                from Investment A
                                left join Counterpart B on A.CounterpartPK = B.CounterpartPK and B.Status = 2
                                where StatusSettlement = 2 and ValueDate between @ValueDateFrom and @ValueDateTo and A.InstrumentTypePK = 1 " + _paramFundFrom + @"
                                group by A.CounterpartPK,B.ID,B.Name
                                 ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "BrokerStock" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "BrokerStock" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Broker Stock");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BrokerStock> rList = new List<BrokerStock>();
                                        while (dr0.Read())
                                        {
                                            BrokerStock rSingle = new BrokerStock();
                                            rSingle.CounterpartPK = dr0["CounterpartPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["CounterpartPK"]);
                                            rSingle.CounterpartID = dr0["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartID"]);
                                            rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                            rSingle.Buy = Convert.ToDecimal(dr0["Buy"]);
                                            rSingle.Sell = Convert.ToDecimal(dr0["Sell"]);

                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd MMMM yyyy");
                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "INDONESIA";
                                            //worksheet.Cells[incRowExcel, 2].Value = "PERIODE : " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMMM yyyy");
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel = incRowExcel + 2;

                                            int _RowA = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "BROKER ID";
                                            worksheet.Cells[incRowExcel, 2].Value = "BROKER NAME";
                                            worksheet.Cells[incRowExcel, 3].Value = "BUY";
                                            worksheet.Cells[incRowExcel, 4].Value = "SELL";
                                            worksheet.Cells[incRowExcel, 5].Value = "TOTAL";
                                            worksheet.Cells[incRowExcel, 6].Value = "MTD%";
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.CounterpartID;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.CounterpartName;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Buy;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Sell;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(C" + incRowExcel + ":D" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = Get_TotalBuySellByCountepartForBrokerStockRpt(rsDetail.CounterpartPK, _paramFundFrom, _FundAccountingRpt.ValueDateFrom, _FundAccountingRpt.ValueDateTo) / Get_TotalBuySellForBrokerStockRpt(_paramFundFrom, _FundAccountingRpt.ValueDateFrom, _FundAccountingRpt.ValueDateTo) * 100;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            worksheet.Cells[incRowExcel, 1].Value = "TOTAL";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 3].Calculate();

                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Calculate();

                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Calculate();

                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Calculate();

                                            worksheet.Cells["A" + _RowA + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _RowA + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _RowA + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _RowA + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        }



                                        incRowExcel = incRowExcel + 2;




                                        int _lastRow = incRowExcel;

                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 6];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 45;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 25;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B Daily Total Transaction Report For All \n &28&B Subscription , Redemption , Switching";



                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion


            #region Daily Compliance Report
            else if (_FundAccountingRpt.ReportName.Equals("Daily Compliance Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";

                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText =

                             @"
                            
                                create table #A (FundID nvarchar(50),FundName nvarchar(100),DepositoAmount numeric(22,4),DEPPercentOfNav numeric(18,4),BondAmount numeric(22,4),BondPercentOfNav numeric(18,4),EquityAmount numeric(22,4),EQPercentOfNav numeric(18,4))
                                insert into #A (FundID,FundName,DepositoAmount,DEPPercentOfNav,BondAmount,BondPercentOfNav,EquityAmount,EQPercentOfNav)
                                select B.ID,B.Name,0,0,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (1,4,16) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                union all
                                select B.ID,B.Name,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK not in (1,4,5,6,16) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                union all
                                select B.ID,B.Name,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0,0,0   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (5) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                select @ValueDate Date,FundID,FundName,sum(DepositoAmount) DepositoAmount,sum(DEPPercentOfNav) DEPPercentOfNav,sum(BondAmount) BondAmount,sum(BondPercentOfNav) BondPercentOfNav,sum(EquityAmount) EquityAmount,sum(EQPercentOfNav) EQPercentOfNav,sum(DEPPercentOfNav + BondPercentOfNav + EQPercentOfNav ) TotalPercent  from #A
                                group By FundID,FundName
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyComplianceReport> rList = new List<DailyComplianceReport>();
                                        while (dr0.Read())
                                        {
                                            DailyComplianceReport rSingle = new DailyComplianceReport();
                                            rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.DepositoAmount = Convert.ToDecimal(dr0["DepositoAmount"]);
                                            rSingle.DEPPercentOfNav = Convert.ToDecimal(dr0["DEPPercentOfNav"]);
                                            rSingle.BondAmount = Convert.ToDecimal(dr0["BondAmount"]);
                                            rSingle.BondPercentOfNav = Convert.ToDecimal(dr0["BondPercentOfNav"]);
                                            rSingle.EquityAmount = Convert.ToDecimal(dr0["EquityAmount"]);
                                            rSingle.EQPercentOfNav = Convert.ToDecimal(dr0["EQPercentOfNav"]);
                                            rSingle.TotalPercent = Convert.ToDecimal(dr0["TotalPercent"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.Date } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Daily Compliance Report";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date   :  ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            //int rowA = incRowExcel;
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "FUND NAME";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "DEPOSITO";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "BOND";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Merge = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "EQUITY";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Total Investment (% NAV)";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            //worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            //worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            //worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            //worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            //worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            //incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            //var _fundID = "";
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                //int RowD = incRowExcel;
                                                //int RowE = incRowExcel + 1;

                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DepositoAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.DEPPercentOfNav;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BondAmount;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.BondPercentOfNav;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.EquityAmount;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EQPercentOfNav;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalPercent;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                //_fundID = rsDetail.Fund;
                                            }

                                            worksheet.Cells["A" + _endRowDetail + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        }




                                        incRowExcel++;

                                        //-----------------------------------
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                cmd1.CommandText =

                                                @"
                                           	Create Table #Exposure
            (
            InstrumentTypePK int,
            InstrumentPK int,
            FundPK int,
            Amount numeric(22,2),
            NAVPercent numeric(18,4),
			ExposureType nvarchar(100)
            )

            Declare @InstrumentTypePK int
            Declare @InstrumentPK int
            Declare @FundPK int
            Declare @Amount numeric(22,2)
            Declare @NAVPercent numeric(18,4)
            Declare @WarningMaxExposurePercent numeric(18,4)

            SET ANSI_WARNINGS OFF

            DECLARE A CURSOR FOR 
            select C.InstrumentTypePK,A.InstrumentPK,A.FundPK,isnull(sum(A.MarketValue),0),case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
            where A.Date = @ValueDate and A.status = 2 
			--" + _paramFund + @"
            group by C.InstrumentTypePK,A.InstrumentPK,A.FundPK,D.AUM
            having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
        	
            Open A
            Fetch Next From A
            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
        
            While @@FETCH_STATUS = 0
            BEGIN
            set @WarningMaxExposurePercent = 0

            IF (@InstrumentTypePK in (1,4,16))
            BEGIN
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'EQUITY ALL'
		            END
	            END

            END
            ELSE IF (@InstrumentTypePK = 5)
            BEGIN  
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 10 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 10 and status = 2  and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'BOND ALL'
		            END
	            END
            END
            ELSE
            BEGIN  
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 13 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 13 and status = 2  and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'DEPOSITO ALL'
		            END
	            END
            END
            Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            END
            Close A
            Deallocate A 
			

			DECLARE A CURSOR FOR 
          		select 0,A.IssuerPK,A.FundPK,sum(isnull(A.MarketValue,0)),sum(isnull(A.NAVPercent,0)) from
				(
				select C.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
				left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
				left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK <> 5 and isnull(C.IssuerPK,0) <> 0
				--" + _paramFund + @"
				group by C.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
				having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
				UNION ALL
				select H.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
				left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
				left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
				left join BankBranch G on A.BankbranchPK = G.BankBranchPK and G.status in (1,2)
				left join Bank H on G.BankPK = H.BankPK and H.status in (1,2)
				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0
				--" + _paramFund + @"
				group by H.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
				having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
				) A 
				group by A.IssuerPK,A.FundPK

        	
            Open A
            Fetch Next From A
            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
        
            While @@FETCH_STATUS = 0
            BEGIN
            set @WarningMaxExposurePercent = 0
			IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent, ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent, 'ISSUER ALL'
		            END
	            END

			  Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            END
            Close A
            Deallocate A 



            select @ValueDate Date,E.Name Type,C.ID InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status in (1,2)
			where A.ExposureType in
			(
			'DEPOSITO ALL','EQUITY ALL','BOND ALL'
			)

			UNION ALL

			 select @ValueDate Date,'ISSUER ALL' Type,C.Name InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            left join Issuer C on A.InstrumentPK = C.IssuerPK and C.Status in (1,2) 
			where A.ExposureType in
			(
			'ISSUER ALL'
			)
		
			

                                                 ";

                                                cmd1.CommandTimeout = 0;
                                                cmd1.Parameters.AddWithValue("@valuedate", _FundAccountingRpt.ValueDateTo);
                                                //cmd1.Parameters.AddWithValue("@FundFrom", _FundAccountingRpt.FundFrom);



                                                cmd1.ExecuteNonQuery();


                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                {
                                                    //if (!dr1.HasRows)
                                                    //{
                                                    //    return false;
                                                    //}
                                                    //else
                                                    //{


                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                    using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                    {

                                                        //ATUR DATA GROUPINGNYA DULU
                                                        List<DailyComplianceReport> rList1 = new List<DailyComplianceReport>();
                                                        while (dr1.Read())
                                                        {
                                                            DailyComplianceReport rSingle1 = new DailyComplianceReport();
                                                            rSingle1.Date = Convert.ToString(dr1["Date"]);
                                                            rSingle1.FundID = Convert.ToString(dr1["FundID"]);
                                                            rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                            rSingle1.Amount = Convert.ToDecimal(dr1["Amount"]);
                                                            rSingle1.NAVPercent = Convert.ToDecimal(dr1["NAVPercent"]);
                                                            rSingle1.Type = Convert.ToString(dr1["Type"]);
                                                            rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]);
                                                            rList1.Add(rSingle1);

                                                        }


                                                        var QueryByFundID1 =
                                                            from r1 in rList1
                                                            group r1 by new { r1.Date } into rGroup1
                                                            select rGroup1;

                                                        incRowExcel = incRowExcel + 2;
                                                        int _endRowDetailZ = 0;


                                                        foreach (var rsHeader1 in QueryByFundID1)
                                                        {
                                                            //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            incRowExcel = incRowExcel + 2;
                                                            //Row B = 3
                                                            int RowBZ = incRowExcel;
                                                            int RowGZ = incRowExcel + 1;

                                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 3].Value = "Exposure Monitoring";
                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Merge = true;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
                                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            incRowExcel++;


                                                            worksheet.Cells[incRowExcel, 3].Value = "Type";
                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                            //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 4].Value = "Instrument";
                                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                            //worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                            //worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                            incRowExcel++;

                                                            // Row C = 4
                                                            int RowCZ = incRowExcel;

                                                            //incRowExcel++;
                                                            //area header

                                                            int _noZ = 1;
                                                            int _startRowDetailZ = incRowExcel;
                                                            foreach (var rsDetail1 in rsHeader1)
                                                            {
                                                                //Row D = 5
                                                                int RowDZ = incRowExcel;
                                                                int RowEZ = incRowExcel + 1;


                                                                //ThickBox Border

                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                                //area detail
                                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundID;
                                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
                                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Type;
                                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail1.InstrumentID;
                                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail1.Amount;
                                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail1.NAVPercent;
                                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;



                                                                _endRowDetailZ = incRowExcel;
                                                                _noZ++;
                                                                incRowExcel++;

                                                            }

                                                            worksheet.Cells["A" + _endRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                            worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                            worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                            //incRowExcel++;
                                                        }
                                                        //disini
                                                        incRowExcel++;

                                                        //-----------------------------------
                                                        using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                                        {
                                                            DbCon2.Open();
                                                            using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                                            {
                                                                cmd2.CommandText =

                                                                @"
                                                                    select @ValueDate Date,B.ID FundID,B.Name FundName,isnull(A.AUM,0) TotalAUM from CloseNAV A
                                                                    left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                                    where A.date = @ValueDate and A.status = 2 " + _paramFund;

                                                                cmd2.CommandTimeout = 0;
                                                                cmd2.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

                                                                cmd2.ExecuteNonQuery();


                                                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                                {
                                                                    //if (!dr2.HasRows)
                                                                    //{
                                                                    //    return false;
                                                                    //}
                                                                    //else
                                                                    //{


                                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                                    using (ExcelPackage package2 = new ExcelPackage(excelFile))
                                                                    {

                                                                        //ATUR DATA GROUPINGNYA DULU
                                                                        List<DailyComplianceReport> rList2 = new List<DailyComplianceReport>();
                                                                        while (dr2.Read())
                                                                        {
                                                                            DailyComplianceReport rSingle2 = new DailyComplianceReport();
                                                                            rSingle2.Date = Convert.ToString(dr2["Date"]);
                                                                            rSingle2.FundID = Convert.ToString(dr2["FundID"]);
                                                                            rSingle2.FundName = Convert.ToString(dr2["FundName"]);
                                                                            rSingle2.TotalAUM = Convert.ToDecimal(dr2["TotalAUM"]);
                                                                            rList2.Add(rSingle2);

                                                                        }


                                                                        var QueryByFundID2 =
                                                                            from r2 in rList2
                                                                            group r2 by new { r2.Date } into rGroup2
                                                                            select rGroup2;

                                                                        incRowExcel = incRowExcel + 2;
                                                                        int _endRowDetailZZ = 0;


                                                                        foreach (var rsHeader2 in QueryByFundID2)
                                                                        {
                                                                            //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            incRowExcel = incRowExcel + 2;
                                                                            //Row B = 3
                                                                            int RowBZ = incRowExcel;
                                                                            int RowGZ = incRowExcel + 1;

                                                                            worksheet.Cells[incRowExcel, 1].Value = "AUM monitoring";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Merge = true;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            incRowExcel++;

                                                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            //worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                            //worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            worksheet.Cells[incRowExcel, 3].Value = "Total AUM";
                                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                            //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;






                                                                            incRowExcel++;

                                                                            // Row C = 4
                                                                            int RowCZ = incRowExcel;

                                                                            //incRowExcel++;
                                                                            //area header

                                                                            int _noZ = 1;
                                                                            int _startRowDetailZ = incRowExcel;
                                                                            foreach (var rsDetail2 in rsHeader2)
                                                                            {
                                                                                //Row D = 5
                                                                                int RowDZ = incRowExcel;
                                                                                int RowEZ = incRowExcel + 1;


                                                                                //ThickBox Border

                                                                                //if (rsDetail1.Type == "Subscription")
                                                                                //{
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                                                //area detail
                                                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail2.FundID;
                                                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail2.FundName;
                                                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail2.TotalAUM;
                                                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;




                                                                                _endRowDetailZZ = incRowExcel;
                                                                                _noZ++;
                                                                                incRowExcel++;

                                                                            }

                                                                            worksheet.Cells["A" + _endRowDetailZZ + ":C" + _endRowDetailZZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                            worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                            worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                            incRowExcel++;
                                                                        }





                                                                        //string _rangeA1 = "A:M" + incRowExcel;
                                                                        //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                                        //{
                                                                        //    r.Style.Font.Size = 22;
                                                                        //}
                                                                        //}

                                                                    }
                                                                }
                                                            }
                                                        }



                                                        //string _rangeA1 = "A:M" + incRowExcel;
                                                        //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                        //{
                                                        //    r.Style.Font.Size = 22;
                                                        //}
                                                    }

                                                    //}
                                                }
                                            }
                                        }



                                        int _lastRow = incRowExcel;

                                        //incRowExcel = incRowExcel + 7;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                        //incRowExcel++;
                                        //worksheet.Row(incRowExcel).Height = 50;
                                        //worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        //worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        string _rangeA = "A:I" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        //worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 30;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 25;
                                        //worksheet.Column(10).Width = 20;
                                        //worksheet.Column(11).Width = 20;
                                        //worksheet.Column(12).Width = 20;
                                        //worksheet.Column(13).Width = 15;
                                        //worksheet.Column(14).Width = 15;
                                        //worksheet.Column(15).Width = 30;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &18&B Daily Total Transaction Report ";



                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region History Settlement
            if (_FundAccountingRpt.ReportName.Equals("History Settlement"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            //string _paramFundFrom = "";

                            //if (!_host.findString(_CustodianRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CustodianRpt.FundFrom))
                            //{
                            //    _paramFundFrom = "And A.FundPK  in ( " + _CustodianRpt.FundFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramFundFrom = "";
                            //}



                            cmd.CommandText = @"
                            select A.ValueDate TanggalTransaksi,A.SettlementDate,D.ID StockID,D.Name StockName,B.ID Fund,C.ID Broker,MV.DescOne Type,A.TrxTypeID,A.DonePrice,A.DoneLot,A.Amount,A.CommissionAmount,A.LevyAmount,A.VATAmount,A.WHTAmount,A.IncomeTaxSellAmount,A.TotalAmount, * from Investment A
                            Left join Fund B ON A.FundPK = B.Fundpk  and  B.Status in (1,2)
                            left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.status in (1,2)
                            left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
                            left join MasterValue MV on A.BoardType = MV.Code and MV.Status=2 and MV.ID ='BoardType' 
                             where A.ValueDate Between  @ValueDateFrom and @valueDateTo and A.InstrumentTypePK = 1 and A.StatusSettlement = 2 
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@valueDateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "HistorySettlement" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "HistorySettlement" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "HistorySettlement";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("History Settlement");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<HistorySettlement> rList = new List<HistorySettlement>();
                                        while (dr0.Read())
                                        {

                                            HistorySettlement rSingle = new HistorySettlement();

                                            rSingle.TanggalTransaksi = Convert.ToString(dr0["TanggalTransaksi"]);
                                            rSingle.SettlementDate = Convert.ToString(dr0["SettlementDate"]);
                                            rSingle.StockId = Convert.ToString(dr0["StockId"]);
                                            rSingle.StockName = Convert.ToString(dr0["StockName"]);
                                            rSingle.Fund = Convert.ToString(dr0["Fund"]);
                                            rSingle.Broker = Convert.ToString(dr0["Broker"]);
                                            rSingle.Type = Convert.ToString(dr0["Type"]);
                                            rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.DonePrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.DoneLot = Convert.ToDecimal(dr0["DoneLot"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.CommissionAmount = Convert.ToDecimal(dr0["CommissionAmount"]);
                                            rSingle.LevyAmount = Convert.ToDecimal(dr0["LevyAmount"]);
                                            rSingle.VATAmount = Convert.ToDecimal(dr0["VATAmount"]);
                                            rSingle.WHTAmount = Convert.ToDecimal(dr0["WHTAmount"]);
                                            rSingle.IncomeTaxSellAmount = Convert.ToDecimal(dr0["IncomeTaxSellAmount"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);







                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                //orderby r ascending
                                                group r by new { r } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date From :";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yy";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date To : ";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yy";


                                        incRowExcel = incRowExcel + 2; ;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Tanggal Transaksi";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Settlement Date";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Stock Id";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "Stock Name";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Value = "Broker";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Type";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "TrxType ID";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Value = "Done Price";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Value = "Done Lot";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 13].Value = "Commission Amount";
                                        worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 14].Value = "Levy Amount";
                                        worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 15].Value = "VAT Amount";
                                        worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 16].Value = "WHT Amount";
                                        worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 17].Value = "Income Tax Sell Amount";
                                        worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 18].Value = "Total Amount";



                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells["K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["O" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells["P" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["R" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        int first = incRowExcel;


                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        int no = 1;
                                        foreach (var rsHeader in GroupByReference)
                                        {





                                            incRowExcel = incRowExcel + 1;



                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = no;

                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.TanggalTransaksi;
                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.TanggalTransaksi).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.SettlementDate).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.StockId;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.StockName;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Broker;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Type; ;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TrxTypeID;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.DoneLot;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.CommissionAmount;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.LevyAmount;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.VATAmount;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.WHTAmount;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.IncomeTaxSellAmount;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 17, incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 17, incRowExcel, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.TotalAmount;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 18, incRowExcel, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 18, incRowExcel, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;



                                                no++;



                                            }








                                            //int last = incRowExcel - 1;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                            //foreach (var rsHeader in GroupByReference)
                                            //{

                                        }
                                        _endRowDetail = incRowExcel;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 13].Calculate();
                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 14].Calculate();
                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 15].Calculate();
                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 16].Calculate();
                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 17].Calculate();
                                        worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 18].Calculate();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Total";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Total";
                                        //worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 18].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 18, 18];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 18;
                                        worksheet.Column(5).Width = 40;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 13;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 18;
                                        worksheet.Column(12).Width = 18;
                                        worksheet.Column(13).Width = 26;
                                        worksheet.Column(14).Width = 23;
                                        worksheet.Column(15).Width = 23;
                                        worksheet.Column(16).Width = 23;
                                        worksheet.Column(17).Width = 30;
                                        worksheet.Column(18).Width = 26;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 History Settlement";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            else
            {
                return false;
            }
        }





        public string PTPDeposito_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, bool _param5)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _mature = "";
                        if (_param5 == true)
                        {
                            _mature = @"
                            union all

                            select A.Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.DoneVolume Quantity, 
                            A.DoneVolume TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                            A.DoneAmount TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                            0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.DoneVolume OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.DoneVolume * 1 AmountTrf, A.InterestPercent BreakInterestPercent,A.AcqDate,A.AcqDate , 
                            round(A.DoneVolume * (A.InterestPercent/100)/case when I.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount
                            from InvestmentMature A
                            left join Investment I on A.InvestmentPK = I.InvestmentPK and I.statusDealing = 2
                            left join Fund C on I.FundPK = C.FundPK and C.status in (1,2)
                            left join instrument D on I.instrumentpk = D.instrumentpk and D.status in (1,2)
                            left join Bank F on I.BankPK = F.BankPK and F.status in (1,2)
                            left join BankBranch G on I.BankBranchPK = G.BankBranchPK and G.status = 2
                            left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                            where A.MaturityDate = @ValueDate and Selected = 1 ";
                        }
                        else
                        {
                            _mature = "";
                        }
                        cmd.CommandText = @"
                     Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ValueDate
                        )
                        and status = 2

                        BEGIN  
                                SET NOCOUNT ON         
          
                            create table #Text(      
                            [ResultText] [nvarchar](1000)  NULL          
                            )                        
        
                        truncate table #Text  
                        insert into #Text     
                        select ''     
                        insert into #Text
                        Select  
                        'NEWM' -- 1.Transaction Status
                        + '|' + cast(isnull(A.TrxType,'') as nvarchar) -- 2.Investment.TrxType
                        + '|' + @CompanyID -- 3.IM Code
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundCode,'')))) -- 4.Fund.SInvestCode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankCode,'')))) -- 5.Bank.PTPCode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankBranchCode,'')))) -- 6.BankBranch.PTPCode
                        + '|' + RTRIM(LTRIM((isnull(A.BankAccountName,'')))) -- 7.BankBranch.BankAccountName
                        + '|' + RTRIM(LTRIM((isnull(A.BankAccountNo,'')))) -- 8.BankBranch.BankAccountNo
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.CurrencyID,'')))) -- 9.Instrument.CurrencyPK
                        + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '0' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '0' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),0)as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                         + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '0' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '0' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),0)as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else case when A.TrxType = 1 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), PrevDate, 112),'')))) end end -- 12.Investment.ValueDate
                        + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                        else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                        + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                        + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                        + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),''))))   else ''  end -- 17.WithdrawalDate
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                        + '|' + case when A.TrxType = 2  then case when A.InterestAmount = 0 then '0' else cast(isnull(cast(A.InterestAmount as decimal(30,2)),'')as nvarchar) end else '' end -- 20.Withdrawal Interest
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount + A.InterestAmount as decimal(30,2)),'')as nvarchar) else '' end -- 21.Total Withdrawal Amount
                        + '|' + -- 22.Rollover Type
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                        + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount + A.InterestAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 1  then cast(isnull(A.AmountTrf,0) as nvarchar) else '0' end end-- 26.Amount to be Transfer
                        + '|' + -- 27.Statutory Type
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ContactPerson,'')))) -- 28.BankBranch.ContactPerson
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Phone1,'')))) -- 29.BankBranch.Phone1
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Fax1,'')))) -- 30.BankBranch.Fax1
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                        + '|' + case when A.TrxType = 2 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else '' end end -- 32.Investment.Reference
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                        + '|' + '' 
                        from (      
                        select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                        A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                        A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                        A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                        A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                        case when A.DoneAmount = I.Balance then cast(isnull(A.DoneAmount,0) as decimal(30,2)) else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,I.AcqDate PrevDate,
                        round(A.DoneAmount * (A.BreakInterestPercent/100)/case when A.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount
                        from investment A
                        left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                        left join Fund C on A.fundpk = C.fundpk and C.status = 2
                        left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                        left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'   
                        left join Bank F on A.BankPK = F.BankPK and F.status = 2
                        left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                        left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                        left join FundPosition I on A.TrxBuy = I.[Identity] and I.status = 2
                        where    
                        A.ValueDate =  @ValueDate and A.InstrumentTypePK in (5)
                        and A.SelectedSettlement = 1
                        and A.statusdealing = 2

                        Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                        A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                        A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                        A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                        A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,I.AcqDate,A.InterestDaysType

                            " + _mature + @"

                        )A    
                        Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                        A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                        A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                        A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.PrevDate
                        order by A.ValueDate Asc
                        select * from #text 
                        END


                                    ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "PTP_Deposito.txt";
                                FileInfo txtFile = new FileInfo(filePath);
                                if (txtFile.Exists)
                                {
                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                }

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                {
                                    while (dr.Read())
                                    {
                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
                                    }
                                    return Tools.HtmlSinvestTextPath + "PTP_Deposito.txt";
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



        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Summary Sub - Red Per-Cabang
            if (_unitRegistryRpt.ReportName.Equals("Summary Sub - Red Per-Cabang"))
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
                                select case when ClientSubscriptionPK between 1 and 10 then 'REKSA DANA AURORA DANA EKUITAS' else 'REKSA DANA AURORA DANA BOND' end Fund, case when ClientSubscriptionPK between 1 and 10 then 'Redemption'
                                else case when ClientSubscriptionPK between 11 and 30 then 'Subscription' else case when ClientSubscriptionPK between 31 and 50 then 'Switching' 
                                end end end TransactionType, case when  ClientSubscriptionPK between 1 and 10
                                then ValueDate else case when ClientSubscriptionPK between 11 and 30 then ValueDate else case when ClientSubscriptionPK between 31 and 50 then ValueDate end end end Bulan, 
                                CashAmount NominalNC, TotalCashAmount NominalTopUp, CashAmount CountNC, TotalCashAmount CountTopUp, 'JAKARTA' Cabang from ClientSubscription 
                                where ClientSubscriptionPK between 1 and 25 order by ValueDate

                            "
                            ;

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _unitRegistryRpt.ValueDateFrom);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SummaryPerCabang" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SummaryPerCabang" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "UnitRegistryReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Summary Per Cabang");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<SummaryPerCabang> rList = new List<SummaryPerCabang>();
                                        while (dr0.Read())
                                        {
                                            SummaryPerCabang rSingle = new SummaryPerCabang();
                                            rSingle.Fund = Convert.ToString(dr0["Fund"]);
                                            rSingle.TransactionType = Convert.ToString(dr0["TransactionType"]);
                                            rSingle.Bulan = Convert.ToString(dr0["Bulan"]);
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"]);
                                            rSingle.NominalNC = Convert.ToDecimal(dr0["NominalNC"]);
                                            rSingle.NominalTopUp = Convert.ToDecimal(dr0["NominalTopUp"]);
                                            rSingle.CountNC = Convert.ToDecimal(dr0["CountNC"]);
                                            rSingle.CountTopUp = Convert.ToDecimal(dr0["CountTopUp"]);
                                            //rSingle.LastMonth = Convert.ToString(dr0["LastMonth"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         group r by new { r.Fund, r.TransactionType, r.Tahun, r.Bulan, r.LastMonth } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        int _startRowDetail = 0;
                                        int _endRowDetail = 0;
                                        var _month = "";
                                        var _year = "";
                                        int _RowTotalMonth = 0;
                                        int _RowTotalYear = 0;
                                        //int _rowEndBalance = 0;
                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;

                                            if (_month != "" && _month != Convert.ToDateTime(rsHeader.Key.Bulan).ToString("MMM"))
                                            {
                                                worksheet.Cells[_RowTotalMonth, 2].Value = _month + " Total";
                                                worksheet.Cells["B" + _RowTotalMonth + ":C" + _RowTotalMonth].Merge = true;
                                                worksheet.Cells[_RowTotalMonth, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[_RowTotalMonth, 4].Style.Font.Bold = true;
                                                worksheet.Cells[_RowTotalMonth, 4].Calculate();
                                                worksheet.Cells[_RowTotalMonth, 4].Style.Numberformat.Format = "#,##0";
                                            }
                                            else
                                            {
                                                incRowExcel = incRowExcel - 1;
                                            }


                                            if (_year != "" && _year != Convert.ToDateTime(rsHeader.Key.Bulan).ToString("yyyy"))
                                            {
                                                incRowExcel = incRowExcel + 3;
                                                _RowTotalYear = _RowTotalMonth + 1;
                                                worksheet.Cells[_RowTotalYear, 1].Value = _year + " Total";

                                                worksheet.Cells["B" + _RowTotalYear + ":C" + _RowTotalYear].Merge = true;
                                                worksheet.Cells[_RowTotalYear, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[_RowTotalYear, 4].Style.Font.Bold = true;
                                                worksheet.Cells[_RowTotalYear, 4].Calculate();
                                                worksheet.Cells[_RowTotalYear, 4].Style.Numberformat.Format = "#,##0";
                                            }


                                            //worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["K" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                            int RowA = incRowExcel;
                                            int RowB = incRowExcel + 1;

                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["K" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Tahun";
                                            worksheet.Cells[incRowExcel, 2].Value = "Bulan";
                                            worksheet.Cells[incRowExcel, 3].Value = "CAB";
                                            worksheet.Cells[incRowExcel, 4].Value = "Nominal NC";
                                            worksheet.Cells[incRowExcel, 5].Value = "Nominal Top Up";
                                            worksheet.Cells[incRowExcel, 6].Value = "Count of NC";
                                            worksheet.Cells[incRowExcel, 7].Value = "Count of Top Up";
                                            worksheet.Cells[incRowExcel, 8].Value = "Total Nominal NC";
                                            worksheet.Cells[incRowExcel, 9].Value = "Total Nominal Top Up";
                                            worksheet.Cells[incRowExcel, 10].Value = "Total Count of NC";
                                            worksheet.Cells[incRowExcel, 11].Value = "Total Count of Top Up";

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsHeader.Key.Bulan).ToString("yyyy");
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsHeader.Key.Bulan).ToString("MMM");
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            _startRowDetail = incRowExcel;
                                            _endRowDetail = incRowExcel;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Cabang;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.NominalNC;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NominalTopUp;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CountNC;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CountTopUp;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NominalNC;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.NominalTopUp;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.CountNC;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.CountTopUp;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";


                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;

                                                _RowTotalMonth = incRowExcel;

                                            }


                                            _month = Convert.ToDateTime(rsHeader.Key.Bulan).ToString("MMM");
                                            _year = Convert.ToDateTime(rsHeader.Key.Bulan).ToString("yyyy");



                                            //worksheet.Cells["C" + _RowB + ":K" + RowTotal].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["C" + _RowB + ":K" + RowTotal].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + _RowB + ":B" + RowTotal].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + _RowB + ":B" + RowTotal].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells["K" + _RowB + ":K" + RowTotal].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //worksheet.Cells[incRowExcel, 2].Value = _month + " Total";

                                        //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 4].Calculate();
                                        //worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                        //incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Value = _year + " Total";
                                        //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 4].Calculate();
                                        //worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                        //incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Grand Total";
                                        //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 4].Calculate();
                                        //worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 7];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 SUMMARY PER CABANG";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion
            else
            {
                return false;
            }
        }

        public string LaporanKeuangan(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "LaporanKeuangan" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\02\\" +  "02_LapKeu.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    // MKBD05
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                    ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                    using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                    {
                        DbCon05.Open();

                        using (SqlCommand cmd05 = DbCon05.CreateCommand())
                        {
                            string _status = "";
                            string _paramAccount = "";


                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "And A.AccountPK  in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }


                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }

                            cmd05.CommandText = @"  Declare @PrevDateTo datetime
                            Declare @PrevDateFrom datetime

                            
set @PrevDateFrom = dateadd(month,-1,@ValueDateFrom)
set @PrevDateTo = dateadd(month,-1,@ValueDateto)
set @PrevDateTo = dateadd(mm, Datediff(mm,0,@PrevDateTo)+1,-1)


Declare @PeriodPK int
Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

create table #LapKeuAurora
(
ID nvarchar(200),
Name nvarchar(1000),
PrevBalance numeric(22,4),
CurrBalance numeric(22,4),
Type int
)

CREATE TABLE #A
(
ID nvarchar(200),
Type int,
Name nvarchar(1000),
CurrBalance numeric(22,4),
YtdBalance numeric(22,4),
PrevBalance numeric(22,4),
)


insert into #LapKeuAurora
SELECT C.ID, C.Name,
CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) * case when A.Type in (2,3) then -1 else 1 end AS PreviousBaseBalance,      
Case when A.Type <= 2 then  CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) else  
CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) -   CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))  end 
* case when A.Type in (2,3) then -1 else 1 end AS CurrentBaseBalance   
,B.Type
FROM (      
SELECT A.AccountPK,       
SUM(B.Balance) AS CurrentBalance,       
SUM(B.BaseBalance) AS CurrentBaseBalance,      
SUM(B.SumDebit) AS CurrentDebit,       
SUM(B.SumCredit) AS CurrentCredit,       
SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
SUM(B.SumBaseCredit) AS CurrentBaseCredit,
A.type      
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
SUM(A.Debit) AS SumDebit,      
SUM(A.Credit) AS SumCredit,      
SUM(A.BaseDebit) AS SumBaseDebit,      
SUM(A.BaseCredit) AS SumBaseCredit,      
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
" + _status + _paramAccount + @"     
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE 
(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK,A.Type      
) AS A LEFT JOIN (       
SELECT A.AccountPK,        
SUM(B.Balance) AS PreviousBalance,        
SUM(B.BaseBalance) AS PreviousBaseBalance,       
SUM(B.SumDebit) AS PreviousDebit,        
SUM(B.SumCredit) AS PreviousCredit,        
SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
SUM(B.SumBaseCredit) AS PreviousBaseCredit,
A.Type  
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
SUM(A.Debit) AS SumDebit,        
SUM(A.Credit) AS SumCredit,        
SUM(A.BaseDebit) AS SumBaseDebit,        
SUM(A.BaseCredit) AS SumBaseCredit,        
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
and B.Status <> 3
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE  (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK,A.Type       
) AS B ON A.AccountPK = B.AccountPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
WHERE (A.CurrentBalance <> 0)        
OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
OR (A.CurrentBaseBalance <> 0)        
OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     
Order BY C.ID 

	
insert into #A(ID,Name,Type,CurrBalance,YtdBalance,PrevBalance)
select A.ID,A.Name,Z.type,A.CurrBalance,isnull(B.CurrentBaseBalance,0) * case when A.Type in (2,3) then -1 else 1 end YtdBalance 
, case when A.type <= 2 then isnull(A.PrevBalance,0) else isnull(C.CurrentBaseBalance - C.PreviousBaseBalance,0) end * case when A.Type in (2,3) then -1 else 1 end PrevBalance
	
from #LapKeuAurora A
left join
(
SELECT C.ID,
CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance
FROM (      
SELECT A.AccountPK,       
SUM(B.Balance) AS CurrentBalance,       
SUM(B.BaseBalance) AS CurrentBaseBalance,      
SUM(B.SumDebit) AS CurrentDebit,       
SUM(B.SumCredit) AS CurrentCredit,       
SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
SUM(B.SumBaseCredit) AS CurrentBaseCredit      
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
SUM(A.Debit) AS SumDebit,      
SUM(A.Credit) AS SumCredit,      
SUM(A.BaseDebit) AS SumBaseDebit,      
SUM(A.BaseCredit) AS SumBaseCredit,      
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
" + _status + _paramAccount + @"     
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE 
(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK       
) AS A LEFT JOIN (       
SELECT A.AccountPK,        
SUM(B.Balance) AS PreviousBalance,        
SUM(B.BaseBalance) AS PreviousBaseBalance,       
SUM(B.SumDebit) AS PreviousDebit,        
SUM(B.SumCredit) AS PreviousCredit,        
SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
SUM(B.SumBaseCredit) AS PreviousBaseCredit       
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
SUM(A.Debit) AS SumDebit,        
SUM(A.Credit) AS SumCredit,        
SUM(A.BaseDebit) AS SumBaseDebit,        
SUM(A.BaseCredit) AS SumBaseCredit,        
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK 
" + _status + _paramAccount + @"     
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE  (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK       
) AS B ON A.AccountPK = B.AccountPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
WHERE (A.CurrentBalance <> 0)        
OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
OR (A.CurrentBaseBalance <> 0)        
OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)    
)B on A.ID  collate DATABASE_DEFAULT  = B.ID  collate DATABASE_DEFAULT      
Left join
(
SELECT C.ID,
CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance,
CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4))  AS PreviousBaseBalance
FROM (      
SELECT A.AccountPK,       
SUM(B.Balance) AS CurrentBalance,       
SUM(B.BaseBalance) AS CurrentBaseBalance,      
SUM(B.SumDebit) AS CurrentDebit,       
SUM(B.SumCredit) AS CurrentCredit,       
SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
SUM(B.SumBaseCredit) AS CurrentBaseCredit      
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
SUM(A.Debit) AS SumDebit,      
SUM(A.Credit) AS SumCredit,      
SUM(A.BaseDebit) AS SumBaseDebit,      
SUM(A.BaseCredit) AS SumBaseCredit,      
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
WHERE  B.ValueDate <= @PrevDateTo and  B.PeriodPK = @PeriodPK
" + _status + _paramAccount + @"     
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE 
(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK       
) AS A LEFT JOIN (       
SELECT A.AccountPK,        
SUM(B.Balance) AS PreviousBalance,        
SUM(B.BaseBalance) AS PreviousBaseBalance,       
SUM(B.SumDebit) AS PreviousDebit,        
SUM(B.SumCredit) AS PreviousCredit,        
SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
SUM(B.SumBaseCredit) AS PreviousBaseCredit       
FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
SUM(A.Debit) AS SumDebit,        
SUM(A.Credit) AS SumCredit,        
SUM(A.BaseDebit) AS SumBaseDebit,        
SUM(A.BaseCredit) AS SumBaseCredit,        
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
WHERE  B.ValueDate < @PrevDateFrom and  B.PeriodPK = @PeriodPK 
" + _status + _paramAccount + @"     
Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
C.ParentPK7, C.ParentPK8, C.ParentPK9        
) AS B        
WHERE  (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
Group BY A.AccountPK       
) AS B ON A.AccountPK = B.AccountPK        
INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
WHERE (A.CurrentBalance <> 0)        
OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
OR (A.CurrentBaseBalance <> 0)        
OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 

)C on A.ID = C.ID     
left join account Z on A.ID = Z.ID and Z.status in (1,2)     

select ID,Type,Name,CurrBalance,YtdBalance,case when type = 2 then PrevBalance * -1 else PrevBalance end PrevBalance from #A                         
                            ";
                            cmd05.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd05.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);

                            using (SqlDataReader dr05 = cmd05.ExecuteReader())
                            {
                                if (dr05.HasRows)
                                {
                                    List<AccountingRpt> rList = new List<AccountingRpt>();
                                    while (dr05.Read())
                                    {
                                        AccountingRpt rSingle = new AccountingRpt();
                                        rSingle.ID = Convert.ToString(dr05["ID"]);
                                        rSingle.Name = Convert.ToString(dr05["Name"]);
                                        rSingle.PrevBalance = Convert.ToDecimal(dr05["PrevBalance"]);
                                        rSingle.CurrBalance = Convert.ToDecimal(dr05["CurrBalance"]);
                                        rSingle.YtdBalance = Convert.ToDecimal(dr05["YtdBalance"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID5 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel = 3;
                                    int incRowExcel2 = 5;
                                    int incRowExcel3 = 800;
                                    var Days = _accountingRpt.ValueDateFrom;
                                    var month = new DateTime(Days.Year, Days.Month, 1);
                                    var first = month.AddMonths(-1);
                                    foreach (var rsHeader in QueryByClientID5)
                                    {
                                        worksheet5.Cells[4, 5].Value = Convert.ToDateTime(first).ToString("MMM yy");
                                        worksheet5.Cells[4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet5.Cells[4, 6].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MMM yy");
                                        worksheet5.Cells[4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet5.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MMM yy");
                                        worksheet5.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet5.Cells[incRowExcel2, 2].Value = rsDetail.ID;

                                            worksheet5.Cells[incRowExcel2, 3].Value = rsDetail.Name;

                                            worksheet5.Cells[incRowExcel2, 4].Value = rsDetail.YtdBalance;
                                            worksheet5.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet5.Cells[incRowExcel2, 5].Value = rsDetail.PrevBalance;
                                            worksheet5.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet5.Cells[incRowExcel2, 6].Value = rsDetail.CurrBalance;
                                            worksheet5.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,##0";
                                            incRowExcel2++;

                                        }

                                        worksheet1.Cells["B" + incRowExcel + ":I" + incRowExcel3].Calculate();
                                        worksheet2.Cells["B" + incRowExcel + ":I" + incRowExcel3].Calculate();
                                        worksheet3.Cells["B" + incRowExcel + ":F" + incRowExcel3].Calculate();
                                        worksheet4.Cells["B" + incRowExcel + ":F" + incRowExcel3].Calculate();
                                        worksheet5.Cells["B" + incRowExcel + ":F" + incRowExcel3].Calculate();
                                        int _IncRowA5 = incRowExcel2 + 1000;
                                    }





                                    worksheet5.PrinterSettings.FitToPage = true;
                                    worksheet5.PrinterSettings.FitToWidth = 1;
                                    worksheet5.PrinterSettings.FitToHeight = 0;
                                    worksheet5.PrinterSettings.PrintArea = worksheet5.Cells[1, 1, incRowExcel2 - 1, 6];
                                    worksheet5.Column(1).AutoFit();
                                    worksheet5.Column(2).AutoFit();
                                    worksheet5.Column(3).AutoFit();
                                    worksheet5.Column(4).AutoFit();
                                    worksheet5.Column(5).AutoFit();
                                    worksheet5.Column(6).AutoFit();

                                }


                            }


                            using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                            {
                                DbCon02.Open();
                                using (SqlCommand cmd02 = DbCon02.CreateCommand())
                                {
                                    cmd02.CommandText = @"Select C.ID,sum(B.BaseCredit - B.baseDebit) Balance from Journal A
                                                        left join JournalDetail B on A.JournalPK = B.JournalPK
                                                        left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                        where A.ValueDate between @ValueDateFrom and @ValueDateTo
                                                        and B.AccountPK = 129

                                                        -- PARAM STATUS DISINI
                                                        and A.status <> 3


                                                        group by C.ID";
                                    cmd02.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                    cmd02.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);

                                    using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                    {
                                        if (dr02.HasRows)
                                        {
                                            List<AccountingRpt> rList = new List<AccountingRpt>();
                                            while (dr02.Read())
                                            {
                                                AccountingRpt rSingle = new AccountingRpt();
                                                rSingle.ID = Convert.ToString(dr02["ID"]);
                                                rSingle.Balance = Convert.ToDecimal(dr02["Balance"]);
                                                rList.Add(rSingle);
                                            }

                                            var QueryByClientID5 =
                                             from r in rList
                                             group r by new { } into rGroup
                                             select rGroup;
                                            int incRowExcel2 = 9;
                                            int _startRowExcel = 9;
                                            int _endRowExcel = 0;
                                            foreach (var rsHeader in QueryByClientID5)
                                            {

                                                foreach (var rsDetail in rsHeader)
                                                {

                                                    worksheet4.Cells[incRowExcel2, 8].Value = rsDetail.ID;
                                                    worksheet4.Cells[incRowExcel2, 9].Value = rsDetail.Balance;
                                                    worksheet4.Cells[incRowExcel2, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    _endRowExcel = incRowExcel2;
                                                    incRowExcel2++;

                                                }


                                                worksheet4.Cells[incRowExcel2, 9].Formula = "SUM(I" + _startRowExcel + ":I" + _endRowExcel + ")";
                                                worksheet4.Cells[incRowExcel2, 9].Calculate();
                                                worksheet4.Cells[incRowExcel2, 9].Style.Numberformat.Format = "#,##0.0000";
                                            }


                                            worksheet4.PrinterSettings.FitToPage = true;
                                            worksheet4.PrinterSettings.FitToWidth = 1;
                                            worksheet4.PrinterSettings.FitToHeight = 0;
                                            //worksheet4.PrinterSettings.PrintArea = worksheet5.Cells[1, 1, incRowExcel2 - 1, ];
                                            worksheet4.Column(8).Width = 20;
                                            worksheet4.Column(9).Width = 20;

                                        }
                                    }
                                }


                            }
                                
                            
                        }

                    }

                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string GenerateFFS(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

            #region FFS
            var _fundName = "";
            int Result = 0;
            string filePath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".xlsx";
            string pdfPath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".pdf";
                
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        select Code from fund a left join mastervalue b on a.Type = b.Code and b.id = 'fundtype' and b.status in(1,2)
                        where a.fundpk = @FundPK
                           ";

                        cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                Result = Convert.ToInt32(dr["Code"]);
                                if(Result == 1 || Result == 3 || Result == 4 || Result == 6 || Result == 8 || Result == 11 || Result == 12 )
                                {
                                    File.Copy(Tools.ReportsTemplatePath + "\\02\\" + "02_FFSTemplate_MoneyMarket.xlsx", filePath, true);
                                }
                                else if(Result == 2 || Result == 7)
                                {
                                    File.Copy(Tools.ReportsTemplatePath + "\\02\\" + "02_FFSTemplate_Bond.xlsx", filePath, true);
                                }
                                else if (Result == 5 || Result == 10)
                                {
                                    File.Copy(Tools.ReportsTemplatePath + "\\02\\" + "02_FFSTemplate_Equity.xlsx", filePath, true);
                                }
                                else if (Result == 9)
                                {
                                    File.Copy(Tools.ReportsTemplatePath + "\\02\\" + "02_FFSTemplate_MixedAsset.xlsx", filePath, true);
                                }
                                else if (Result == 5)
                                {
                                    File.Copy(Tools.ReportsTemplatePath + "\\02\\" + "02_FFSTemplate_05.xlsx", filePath, true);
                                }

                            }
                        }
                    }
                }
                
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    #region 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @" select B.ID FundID, SUBSTRING (B.Name, 12, 30) FundName, C.Name BankKustodianID,* from ffssetup_02 A 
                                left join Fund B on A.FundPK = B.FundPK and B.status in(1,2) 
                                left join Bank C on A.BankKustodianPK = C.BankPK and C.status in(1,2)  where A.status = 2 and A.FundPK = @FundPK and ValueDate = @Date";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {

                                    return "false";
                                }
                                else
                                {
                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundFactSheet> rList = new List<FundFactSheet>();
                                    while (dr0.Read())
                                    {

                                        FundFactSheet rSingle = new FundFactSheet();
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.TujuanStrategiInvestasi = Convert.ToString(dr0["TujuanStrategiInvestasi"]);
                                        rSingle.MarketReview = Convert.ToString(dr0["MarketReview"]);
                                        rSingle.UngkapanSanggahan = Convert.ToString(dr0["UngkapanSanggahan"]);
                                        rSingle.TanggalPerdana = Convert.ToDateTime(dr0["TanggalPerdana"]);
                                        rSingle.NilaiAktivaBersih = Convert.ToDecimal(dr0["NilaiAktivaBersih"]);
                                        rSingle.TotalUnitPenyertaan = Convert.ToDecimal(dr0["TotalUnitPenyertaan"]);
                                        rSingle.NilaiAktivaBersihUnit = Convert.ToDecimal(dr0["NilaiAktivaBersihUnit"]);
                                        rSingle.ImbalJasaManajerInvestasi = Convert.ToDecimal(dr0["ImbalJasaManajerInvestasi"]);
                                        rSingle.ImbalJasaBankKustodian = Convert.ToDecimal(dr0["ImbalJasaBankKustodian"]);
                                        rSingle.FaktorResikoYangUtama = Convert.ToString(dr0["FaktorResikoYangUtama"]);
                                        rSingle.ManfaatInvestasi = Convert.ToString(dr0["ManfaatInvestasi"]);
                                        rSingle.BiayaPembelian = Convert.ToDecimal(dr0["BiayaPembelian"]);
                                        rSingle.BiayaPenjualan = Convert.ToDecimal(dr0["BiayaPenjualan"]);
                                        rSingle.BiayaPengalihan = Convert.ToDecimal(dr0["BiayaPengalihan"]);
                                        rSingle.BankKustodianID = Convert.ToString(dr0["BankKustodianID"]);
                                        rSingle.BankAccountID = Convert.ToString(dr0["BankKustodianID"]);
                                        rSingle.Path = Convert.ToString(dr0["Path"]);
                                        rSingle.KebijakanInvestasi = Convert.ToString(dr0["KebijakanInvestasi"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                    from r in rList
                                    group r by new { } into rGroup
                                    select rGroup;

                                    int incRowExcel = 11;
                                    int _rowKet = 0;
                                    //incRowExcel = incRowExcel + 1;
                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        worksheet.Cells[76, 5].Value = "RETURN";
                                        worksheet.Cells[76, 6].Value = "1 Mo";
                                        worksheet.Cells[76, 7].Value = "3 Mo";
                                        worksheet.Cells[76, 8].Value = "6 Mo";
                                        worksheet.Cells[76, 9].Value = "YTD";
                                        worksheet.Cells[76, 10].Value = "1Y";
                                        worksheet.Cells[76, 11].Value = "3Y";
                                        worksheet.Cells[76, 12].Value = "5Y";
                                        worksheet.Cells[76, 13].Value = "Sejak Perdana";
                                        worksheet.Cells["E" + 76 + ":M" + 76].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + 76 + ":M" + 76].Style.Border.Bottom.Color.SetColor(Color.DeepSkyBlue);
                                        worksheet.Cells["E" + 76 + ":M" + 76].Style.Font.Bold = true;
                                        //end area header


                                        worksheet.Cells[84, 5].Value = "Beta";
                                        worksheet.Cells["E" + 84 + ":F" + 84].Merge = true;
                                        worksheet.Cells["E" + 85 + ":F" + 85].Merge = true;
                                        worksheet.Cells[84, 7].Value = "Sharpe Ratio";
                                        worksheet.Cells["G" + 84 + ":H" + 84].Merge = true;
                                        worksheet.Cells["G" + 85 + ":H" + 85].Merge = true;
                                        worksheet.Cells[84, 9].Value = "Ann. Std Deviation";
                                        worksheet.Cells["I" + 84 + ":J" + 84].Merge = true;
                                        worksheet.Cells["I" + 85 + ":J" + 85].Merge = true;
                                        worksheet.Cells[84, 11].Value = "Information Ratio";
                                        worksheet.Cells["K" + 84 + ":M" + 84].Merge = true;
                                        worksheet.Cells["K" + 85 + ":M" + 85].Merge = true;
                                        worksheet.Cells["E" + 84 + ":M" + 84].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["E" + 84 + ":M" + 84].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["E" + 84 + ":M" + 84].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(25, 25, 112));
                                        worksheet.Cells["E" + 84 + ":M" + 84].Style.Font.Color.SetColor(Color.White);
                                        worksheet.Cells["E" + 84 + ":M" + 84].Style.Font.Bold = true;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[11, 1].Value = "Fact Sheet - " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMM yyyy") + " | " + "Reksa Dana Saham";
                                            worksheet.Cells[11, 1].Style.Font.Name = "Biko";
                                            worksheet.Cells[11, 1].Style.Font.Color.SetColor(Color.DarkSlateBlue);
                                            worksheet.Cells[11, 1].Style.Font.Size = 18;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Color.SetColor(Color.DeepSkyBlue);

                                            worksheet.Cells[12, 1].Value = rsDetail.FundName;
                                            worksheet.Cells[12, 1].Style.Font.Name = "Biko";
                                            worksheet.Cells[12, 1].Style.Font.Size = 40;
                                            worksheet.Cells[12, 1].Style.Font.Color.SetColor(Color.DarkSlateBlue);
                                            incRowExcel = incRowExcel + 13;
                                            

                                            worksheet.Cells[25, 1].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[25, 1].Value = "Tujuan & Strategi Investasi";
                                            worksheet.Cells[25, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _rowA = 26;
                                            int _rowB = 26 + 4;
                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.TujuanStrategiInvestasi;
                                            worksheet.Cells["A" + _rowA + ":M" + _rowB].Merge = true;
                                            worksheet.Cells["A" + _rowA + ":M" + _rowB].Style.WrapText = true;
                                            worksheet.Cells["A" + _rowA + ":M" + _rowB].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["A" + _rowA + ":M" + _rowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            worksheet.Row(26).Height = 20;
                                            incRowExcel = incRowExcel + 6;

                                            //worksheet.Cells[22, 1].Style.Font.Bold = true;
                                            worksheet.Cells[32, 1].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[32, 1].Value = "Kebijakan Investasi";
                                            worksheet.Cells["A" + 32 + ":B" + 32].Merge = true;
                                            worksheet.Cells[32, 1].Style.Font.Bold = true;
                                            worksheet.Cells[32, 3].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[32, 3].Value = "Alokasi Aset";
                                            worksheet.Cells[32, 3].Style.Font.Bold = true;
                                            worksheet.Cells["E" + 32 + ":G" + 32].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[32, 5].Value = "Seleksi Efek";
                                            worksheet.Cells[32, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + 32 + ":G" + 32].Merge = true;
                                            incRowExcel++;

                                            int _rowC = incRowExcel;
                                            int _rowD = incRowExcel + 2;
                                            string textWithNewLine = rsDetail.KebijakanInvestasi.Replace("/n", Environment.NewLine);

                                            worksheet.Cells[incRowExcel, 1].Value = textWithNewLine;
                                            worksheet.Cells["A" + _rowC + ":B" + _rowD].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 2].Value = "80% - 100% ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "Surat";
                                            //worksheet.Cells[incRowExcel, 2].Value = "Utang 0 - 20% ";
                                            //worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "Pasar";
                                            //worksheet.Cells[incRowExcel, 2].Value = "Uang 0 - 20% ";
                                            //worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            //worksheet.Cells["A" + _rowC + ":B" + _rowD].Merge = true;

                                            //worksheet.Cells["C" + _rowC + ":C" + _rowD].Merge = true;
                                            worksheet.Row(incRowExcel).Height = 30;
                                            worksheet.Cells[33, 5].Value = "No";
                                            worksheet.Cells[33, 6].Value = "Kode";
                                            worksheet.Cells[33, 7].Value = "Emiten";
                                            worksheet.Cells["E" + 33 + ":I" + 33].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["E" + 33 + ":I" + 33].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                            worksheet.Cells["E" + 33 + ":I" + 33].Style.Font.Bold = true;
                                            incRowExcel++;

                                            incRowExcel++;
                                            worksheet.Cells[39, 5].Value = "Diurutkan berdasarkan abjad kode";
                                            worksheet.Cells[39, 6].Style.Font.Size = 9;
                                            worksheet.Cells["E" + 39 + ":H" + 39].Merge = true;
                                            incRowExcel++;

                                            int rowIndex11 = 41;
                                            int colIndex11 = 0;
                                            //int PixelTop1 = 88;
                                            //int PixelLeft1 = 129;
                                            //int Height11 = 134;
                                            //int Width11 = 838;
                                            //Image img31 = Image.FromFile(@"E:\Radsoft\22 November\CORE\W1\Images\02\ini.PNG");
                                            //Image thumb2 = img31.GetThumbnailImage(538, 134, null, IntPtr.);
                                            //ExcelPicture pic21 = worksheet.Drawings.AddPicture("Sample19", img31);
                                            //pic21.SetPosition(rowIndex11, 0, colIndex11, 0);
                                            //pic21.Locked.ToString();
                                            //pic.SetPosition(PixelTop, PixelLeft);  
                                            //pic21.SetSize(Height11, Width11);
                                            //pic21.SetSize(20);
                                            worksheet.Protection.IsProtected = false;
                                            worksheet.Protection.AllowSelectLockedCells = false;
                                            

                                            worksheet.Cells[40, 1].Style.Font.Bold = true;
                                            worksheet.Cells[40, 1].Value = "Profil Risiko";
                                            worksheet.Cells[40, 1].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[40, 1].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 12;

                                            worksheet.Cells[52, 1].Style.Font.Bold = true;
                                            worksheet.Cells[52, 1].Value = "Informasi Produk";
                                            worksheet.Cells[52, 1].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[52, 1].Style.Font.Bold = true;
                                            worksheet.Cells[52, 5].Value = "Grafik Kinerja";
                                            worksheet.Cells[52, 5].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            worksheet.Cells[52, 5].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 6;
                                            worksheet.Cells[54, 1].Value = "Tanggal Perdana";
                                            worksheet.Cells["A" + 54 + ":B" + 54].Merge = true;
                                            worksheet.Cells[54, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[54, 3].Value = rsDetail.TanggalPerdana;
                                            worksheet.Cells[54, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[54, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(54).Height = 30;
                                            incRowExcel++;

                                            worksheet.Cells[55, 1].Value = "Nilai Aktiva Bersih";
                                            worksheet.Cells["A" + 55 + ":B" + 55].Merge = true;
                                            worksheet.Cells[55, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[55, 3].Value = rsDetail.NilaiAktivaBersih;
                                            worksheet.Cells[55, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[55, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(55).Height = 30;
                                            incRowExcel++;

                                            worksheet.Cells[56, 1].Value = "Total Unit Penyertaan";
                                            worksheet.Cells["A" + 56 + ":B" + 56].Merge = true;
                                            worksheet.Cells[56, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[56, 3].Value = rsDetail.TotalUnitPenyertaan;
                                            worksheet.Cells[56, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[56, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(56).Height = 30;
                                            incRowExcel++;

                                            worksheet.Cells[57, 1].Value = "Nilai Aktiva Bersih/Unit";
                                            worksheet.Cells["A" + 57 + ":B" + 57].Merge = true;
                                            worksheet.Cells[57, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[57, 3].Value = rsDetail.NilaiAktivaBersihUnit;
                                            worksheet.Cells[57, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[57, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(57).Height = 30;
                                            incRowExcel++;

                                            int _rowG = 58;
                                            int _rowH = 58 + 2;
                                            worksheet.Cells[58, 1].Value = "Faktor Risiko yang Utama";
                                            worksheet.Cells["A" + 58 + ":B" + 58].Merge = true;
                                            worksheet.Cells[58, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[58, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[58, 1].Style.WrapText = true;
                                            worksheet.Cells[58, 3].Value = rsDetail.FaktorResikoYangUtama;
                                            worksheet.Cells[58, 3].Style.WrapText = true;
                                            worksheet.Cells[58, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["C" + _rowG + ":C" + _rowH].Merge = true;
                                            worksheet.Row(58).Height = 30;
                                            //worksheet.Row(incRowExcel).Height = 30;
                                            incRowExcel = 58 + 2;
                                            worksheet.Cells[67, 5].Value = "Market Review";
                                            worksheet.Cells[67, 5].Style.Font.Bold = true;
                                            worksheet.Cells[67, 5].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            incRowExcel++;

                                            int _rowJ = 68;
                                            int _rowI = 68 + 5;
                                            worksheet.Cells[68, 5].Value = rsDetail.MarketReview;
                                            worksheet.Cells[68, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[68, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            worksheet.Cells["E" + _rowJ + ":M" + _rowI].Merge = true;


                                            int _rowE = 61;
                                            int _rowF = 61 + 3;
                                            worksheet.Cells[61, 1].Value = "Manfaat Investasi";
                                            worksheet.Cells["A" + 61 + ":B" + 61].Merge = true;
                                            worksheet.Cells[61, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[61, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //worksheet.Cells["A" + _rowE + ":A" + _rowF].Merge = true;
                                            worksheet.Cells[61, 3].Value = rsDetail.ManfaatInvestasi;
                                            worksheet.Cells[61, 3].Style.WrapText = true;
                                            worksheet.Cells[61, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[61, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["C" + _rowE + ":C" + _rowF].Merge = true;
                                            worksheet.Row(61).Height = 25;
                                            worksheet.Row(62).Height = 24;
                                            worksheet.Row(63).Height = 24;
                                            worksheet.Row(64).Height = 24;

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;



                                            //incRowExcel = incRowExcel + 3;

                                            worksheet.Cells[66, 1].Value = "Imbal Jasa Manajer Investasi";
                                            worksheet.Cells["A" + 66 + ":B" + 66].Merge = true;
                                            worksheet.Cells[66, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[66, 3].Value = rsDetail.ImbalJasaManajerInvestasi + "%";
                                            worksheet.Cells[66, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(66).Height = 20;
                                            incRowExcel++;

                                            worksheet.Cells[67, 1].Value = "Imbal Jasa Bank Kustodian";
                                            worksheet.Cells["A" + 67 + ":B" + 67].Merge = true;
                                            worksheet.Cells[67, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[67, 3].Value = rsDetail.ImbalJasaBankKustodian + "%";
                                            worksheet.Cells[67, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(67).Height = 20;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[68, 1].Value = "Biaya Pembelian";
                                            worksheet.Cells["A" + 68 + ":B" + 68].Merge = true;
                                            worksheet.Cells[68, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[68, 3].Value = rsDetail.BiayaPembelian + "%";
                                            worksheet.Cells[68, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(68).Height = 20;

                                            worksheet.Cells[75, 5].Value = "Tabel Kinerja";
                                            worksheet.Cells[75, 5].Style.Font.Bold = true;
                                            worksheet.Cells[75, 5].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            incRowExcel++;

                                            //worksheet.Cells["E" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["E" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Color.SetColor(Color.DeepSkyBlue);
                                            incRowExcel = incRowExcel + 12;
                                            _rowKet = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "Keterangan: EKUITAS (Aurora Dana Ekuitas), IRDSH(Indeks Reksa Dana Saham), IHSG (Indeks Harga Saham Gabungan)";
                                            worksheet.Cells["E" + incRowExcel + ":M" + incRowExcel].Merge = true;

                                            incRowExcel = incRowExcel + 8;

                                            worksheet.Cells["E" + 85 + ":M" + 85].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["E" + 85 + ":M" + 85].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["E" + 85 + ":M" + 85].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(173, 216, 230));

                                           

                                            worksheet.Cells[69, 1].Value = "Biaya Penjualan";
                                            worksheet.Cells["A" + 69 + ":B" + 69].Merge = true;
                                            worksheet.Cells[69, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[69, 3].Value = rsDetail.BiayaPenjualan + "%";
                                            worksheet.Cells[69, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(69).Height = 20;
                                            incRowExcel++;

                                            worksheet.Cells[70, 1].Value = "Biaya Pengalihan";
                                            worksheet.Cells["A" + 70 + ":B" + 70].Merge = true;
                                            worksheet.Cells[70, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[70, 3].Value = rsDetail.BiayaPengalihan + "%";
                                            worksheet.Cells[70, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(70).Height = 20;
                                            incRowExcel++;

                                            worksheet.Cells[71, 1].Value = "Bank Kustodian";
                                            worksheet.Cells["A" + 71 + ":B" + 71].Merge = true;
                                            worksheet.Cells[71, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[71, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[71, 3].Value = rsDetail.BankKustodianID;
                                            worksheet.Cells[71, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[71, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(71).Height = 20;
                                            incRowExcel++;

                                            int _RowX = 72;
                                            int _RowY = 72 + 5;
                                            worksheet.Cells[72, 1].Value = "Bank Account";
                                            worksheet.Cells["A" + 72 + ":B" + 72].Merge = true;
                                            worksheet.Cells[72, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[72, 3].Value = rsDetail.BankAccountID;
                                            worksheet.Cells[72, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + _RowX + ":C" + _RowY].Merge = true;
                                            //worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[72, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[72, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //worksheet.Row(58).Height = 50;
                                            incRowExcel = 72 + 4;

                                            worksheet.Cells[85, 1].Value = "Manajer Investasi";
                                            worksheet.Cells[85, 1].Style.Font.Bold = true;
                                            worksheet.Cells[85, 1].Style.Font.Color.SetColor(Color.DodgerBlue);

                                            worksheet.Cells[87, 5].Value = "Ungkapan & Sanggahan";
                                            worksheet.Cells[87, 5].Style.Font.Bold = true;
                                            worksheet.Cells[87, 5].Style.Font.Color.SetColor(Color.DodgerBlue);
                                            incRowExcel++;
                                            int _rowM = 88;
                                            int _rowN = 88 + 2;
                                            worksheet.Cells[88, 5].Value = rsDetail.UngkapanSanggahan;
                                            worksheet.Cells[88, 5].Style.WrapText = true;
                                            worksheet.Cells["E" + _rowM + ":M" + _rowN].Merge = true;
                                            worksheet.Cells[88, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[88, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            
                                            worksheet.Cells[46, 6, 49, 7].Style.Font.Color.SetColor(Color.White);
                                            worksheet.Cells[46, 8, 49, 9].Style.Font.Color.SetColor(Color.White);


                                        }

                                        worksheet.Cells["A" + 25 + ":M" + 90].Style.Font.Size = 15;
                                        worksheet.Cells[_rowKet, 5].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 10;
                                        int _disA = 51;
                                        int _disB = 80;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”) hanya merupakan informasi dan/atau keterangan yang tidak dapat diartikan sebagai suatu saran/advise bisnis tertentu, karenanya Dokumen tersebut tidak bersifat mengikat. Segala hal yang berkaitan dengan diterimanya dan/atau dipergunakannya Dokumen tersebut sebagai pengambilan keputusan bisnis dan/atau investasi merupa kan tanggung jawab pribadi atas segala risiko yang mungkin timbul. Sehubungan dengan risiko dan tanggung jawab pribadi atas Dokumen, pengguna dengan ini menyetujui untuk melepaskan segala tanggung jawab dan risiko hukum kepada PT. Jasa Capital Asset Management atas diterimanya dan/atau dipergunakannya Dokumen.";
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Merge = true;
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Style.WrapText = true;
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        //worksheet.Cells["A" + _disA + ":J" + _disB].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                        worksheet.Cells["A" + _disA + ":C" + _disB].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _disA + ":C" + _disB].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                        //incRowExcel = incRowExcel + 5;
                                        incRowExcel = incRowExcel + 15;
                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                    }

                                    worksheet.Cells["A" + 11 + ":Z" + 90].Style.Font.Name = "Biko";


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 13];
                                    worksheet.Column(1).Width = 20;
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 35.5;
                                    worksheet.Column(4).Width = 5;
                                    worksheet.Column(5).Width = 15;
                                    worksheet.Column(6).Width = 13;
                                    worksheet.Column(7).Width = 27;
                                    worksheet.Column(8).Width = 27;
                                    worksheet.Column(9).Width = 27;
                                    worksheet.Column(10).Width = 13;
                                    worksheet.Column(11).Width = 12;
                                    worksheet.Column(12).Width = 14;
                                    worksheet.Column(13).Width = 25;

                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "&16&B PT. JASA CAPITAL ASSET MANAGEMENT \n " + Convert.ToString(_fundName); 


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 FUND FACT SHEET \n" + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMM yyyy");

                                    //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //Image thumb = img.GetThumbnailImage(165, 75, null, IntPtr.Zero);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.CenteredText = "\n \n \n \n \n \n \n &14&B Disclaimer" + "&10 : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”)";


                                }
                            }
                        }
                    }
                    #endregion


                    #region 2

                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[1];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"Select top 5 A.InstrumentID,  isnull(D.Name,'') PortfolioEfek from FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join SubSector C on B.SectorPK = C.SubSectorPK and C.status in (1,2)
                            left join Sector D on C.SectorPK = D.SectorPK and D.status in (1,2)
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2 and B.InstrumentTypePK in (1,4,16)
                            order by A.InstrumentID";
                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (!dr02.HasRows)
                                {
                                    return "false";
                                }
                                else
                                {
                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                    while (dr02.Read())
                                    {
                                        FundAccountingRpt rSingle = new FundAccountingRpt();
                                        rSingle.InstrumentID = Convert.ToString(dr02["InstrumentID"]);
                                        rSingle.PortfolioEfek = Convert.ToString(dr02["PortfolioEfek"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel2 = 34;
                                    int _no = 1;
                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet2.Cells[incRowExcel2, 5].Value = _no;
                                            worksheet2.Cells[incRowExcel2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet2.Cells[incRowExcel2, 6].Value = rsDetail.InstrumentID;
                                            worksheet2.Cells[incRowExcel2, 7].Value = rsDetail.PortfolioEfek;
                                            worksheet.Cells["G" + incRowExcel2 + ":I" + incRowExcel2].Merge = true;
                                            worksheet.Row(incRowExcel2).Height = 23;
                                            _no++;
                                            incRowExcel2++;


                                        }


                                    }

                                }


                            }

                        }

                    }
                    #endregion


//                    #region 3

//                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[1];
//                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
//                    {
//                        DbCon02.Open();
//                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
//                        {
//                            cmd02.CommandText = @"Declare @Aum numeric(22,4)
//                            Select @aum = aum from closeNAV where date = @Date and FundPK = @FundPK and status = 2
//
//                            Declare @TotalInvestment numeric(22,4)
//                            Select @TotalInvestment = sum(MarketValue) From FundPosition A
//                            where A.FundPK = @FundPK and A.date = @Date
//                            and A.status = 2
//
//
//                            Select  
//                            Case when InstrumentTypePK in  (1,4,16) then 'SAHAM'
//	                             when InstrumentTypePK in  (2,3,8,9,11,12,13,14,15) then 'OBLIGASI'
//	                             when InstrumentTypePK in  (5) then 'DEPOSITO'
//	                             end InsType,sum(MarketValue)/@Aum * 100  PercentInvestment
//                            From FundPosition A
//                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
//                            where A.FundPK = @FundPK and A.date = @Date
//                            and A.status = 2
//                            group by B.InstrumentTypePK
//                            UNION ALL
//                            Select 'KAS' InsType , (@Aum - @TotalInvestment) / @Aum * 100 PercentInvestment";
//                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
//                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

//                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
//                            {
//                                if (!dr02.HasRows)
//                                {
//                                    return "false";
//                                }
//                                else
//                                {
//                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
//                                    while (dr02.Read())
//                                    {
//                                        FundAccountingRpt rSingle = new FundAccountingRpt();
//                                        rSingle.InsType = Convert.ToString(dr02["InsType"]);
//                                        rSingle.PercentInvestment = Convert.ToDecimal(dr02["PercentInvestment"]);
//                                        rList.Add(rSingle);
//                                    }
//                                    var QueryByClientID2 =
//                                     from r in rList
//                                     group r by new { } into rGroup
//                                     select rGroup;

//                                    int incRowExcel3 = 33;
//                                    foreach (var rsHeader in QueryByClientID2)
//                                    {
//                                        foreach (var rsDetail in rsHeader)
//                                        {


//                                            worksheet2.Cells[incRowExcel3, 1].Value = rsDetail.InsType;
//                                            worksheet2.Cells[incRowExcel3, 2].Value = rsDetail.PercentInvestment;
//                                            incRowExcel3++;

//                                            //worksheet.Cells["A" + incRowExcel3 + ":B" + incRowExcel3].Style.Font.Color.SetColor(Color.White);




//                                        }


//                                    }

//                                }


//                            }

//                        }

//                    }
//                    #endregion


                    #region 4

                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets[1];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"Declare @TotalInvestmentEquity numeric(22,4)
                            Select @TotalInvestmentEquity = sum(MarketValue) From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)

                            where A.FundPK = @FundPK and A.date = @Date and B.InstrumentTypePK in (1,4,16)
                            and A.status = 2
                            Select top 7 D.Name Sector,sum(marketValue)/@TotalInvestmentEquity * 100 AlokasiSector from FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join SubSector C on B.SectorPK = C.SubSectorPK and C.status in (1,2)
                            left join Sector D on C.SectorPK = D.SectorPK and D.status in (1,2)
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2 and B.InstrumentTypePK in (1,4,16)
                            group by D.Name";
                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (!dr02.HasRows)
                                {
                                    return "false";
                                }
                                else
                                {
                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                    while (dr02.Read())
                                    {
                                        FundAccountingRpt rSingle = new FundAccountingRpt();
                                        rSingle.Sectors = Convert.ToString(dr02["Sector"]);
                                        rSingle.AlokasiSector = Convert.ToDecimal(dr02["AlokasiSector"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel4 = 34;
                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {


                                            worksheet2.Cells[incRowExcel4, 11].Value = rsDetail.Sectors;
                                            worksheet2.Cells[incRowExcel4, 12].Value = rsDetail.AlokasiSector;
                                            worksheet2.Cells["K" + incRowExcel4 + ":L" + incRowExcel4].Style.Font.Color.SetColor(Color.White);
                                            incRowExcel4++;

                                            ////2
                                        }


                                    }

                                }


                            }

                        }

                    }
                    #endregion



                    //create a new piechart of type Line satu
                    ExcelPieChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Pie) as ExcelPieChart;
                    //set the title
                    lineChart.Title.Text = "Komposisi Sektor";
                    lineChart.DataLabel.ShowPercent = true;
                    lineChart.DataLabel.ShowCategory = true;
                    lineChart.DataLabel.ShowLeaderLines = true;
                    lineChart.Legend.Remove();

                    //create the ranges for the chart
                    var rangeLabel = worksheet.Cells["K34:K40"];
                    var range1 = worksheet.Cells["L34:L40"];

                    //add the ranges to the chart
                    lineChart.Series.Add(range1, rangeLabel);


                    //position of the legend
                    lineChart.Style = eChartStyle.Style19;




                    //size of the chart
                    lineChart.SetSize(440, 350);

                    //add the chart at cell B6
                    lineChart.SetPosition(32, 30, 9, 0);
                    
                    package.Save();
                    return filePath;


                }

            }
            catch (Exception err)
            {
                return "false";
                throw err;
            }


            #endregion

        }


        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {

            #region SiPesat
            if (_OjkRpt.ReportName.Equals("4"))
            {
                #region Txt
                if (_OjkRpt.DownloadMode == "Txt")
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
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),105 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z


                        insert into #Text

                        select distinct IDPJK + '|' + RTRIM(LTRIM(isnull(InvestorType,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(TempatLahir,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(Name,''))) + '|' +
                        isnull(TanggalLahir,'') + '|' + 
                        convert(nvarchar(15),ISNULL(Alamat,'')) + '|' +
                        CAST(NoKTP as nvarchar(30)) + '|' +
                        CAST(NoIDLain as nvarchar(30)) + '|' +
                        RTRIM(LTRIM(ISNULL(ID,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(NPWP,'')))
                        from @A


                        select * from #Text ";
                                cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        string filePath = Tools.ARIATextPath + "Sipesat.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            while (dr.Read())
                                            {

                                                file.WriteLine(Convert.ToString(dr["ResultText"]));
                                            }
                                            return true;
                                        }

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


                #endregion

                #region Excel
                else
                {
                    if (_OjkRpt.ReportName.Equals("4"))
                    {
                        int rowcell = 0;
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {
                                    cmd.CommandText = @"
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),106 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z

                        select distinct IDPJK,InvestorType,TempatLahir,Name,TanggalLahir,Alamat,NoKTP,NoIDLain,ID,NPWP from @A 

                        ";
                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                    cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);
                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "Sipesat_" + _userID + ".xlsx";
                                            int incRowExcel = 0;
                                            int _startRowDetail, _endRowDetail;
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }

                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "Sipesat";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("SiPesatReport");

                                                List<SipesatRpt> rList = new List<SipesatRpt>();
                                                while (dr0.Read())
                                                {
                                                    SipesatRpt rSingle = new SipesatRpt();

                                                    rSingle.InvestorType = Convert.ToInt32(dr0["InvestorType"]);
                                                    rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                                    rSingle.TempatLahir = Convert.ToString(dr0["TempatLahir"]);
                                                    rSingle.TanggalLahir = Convert.ToString(dr0["TanggalLahir"]);
                                                    rSingle.Alamat = Convert.ToString(dr0["Alamat"]);
                                                    rSingle.NoKTP = Convert.ToString(dr0["NoKTP"]);
                                                    rSingle.NoIDLain = Convert.ToString(dr0["NoIDLain"]);
                                                    rSingle.ID = Convert.ToString(dr0["ID"]);
                                                    rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                                    rSingle.IDPJK = Convert.ToString(dr0["IDPJK"]);

                                                    rList.Add(rSingle);
                                                }

                                                var GroupByReference =
                                                    from r in rList
                                                    group r by new { } into rGroup
                                                    select rGroup;
                                                foreach (var rsHeader in GroupByReference)
                                                {

                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    worksheet.Cells[incRowExcel, 1].Value = "ID PJK";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Nasabah";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = "Nama Nasabah";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 4].Value = "Tempat Lahir";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Tanggal Lahir";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 6].Value = "Alamat";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 7].Value = "KTP";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Value = "Identitas Lain";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 9].Value = "CIF/Kepesertaan";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 10].Value = "NPWP";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    incRowExcel++;

                                                    //end area header
                                                    foreach (var rsDetail in rsHeader)
                                                    {

                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.IDPJK;
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorType;
                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.TempatLahir;
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalLahir;
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Alamat;
                                                        worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        //worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.NoKTP;
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.NoIDLain;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.NPWP;
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;


                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                    }
                                                    int _endRow = incRowExcel - 1;
                                                    worksheet.Cells["A" + _endRow + ":J" + _endRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                    worksheet.Row(incRowExcel).PageBreak = true;
                                                }


                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 0;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                                worksheet.Cells.AutoFitColumns(0);
                                                worksheet.Column(1).Width = 20;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 40;
                                                worksheet.Column(4).Width = 20;
                                                worksheet.Column(5).Width = 20;
                                                worksheet.Column(6).Width = 40;
                                                worksheet.Column(7).Width = 20;
                                                worksheet.Column(8).Width = 20;
                                                worksheet.Column(9).Width = 20;
                                                worksheet.Column(10).Width = 20;
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                // worksheet.PrinterSettings.FitToPage = true;
                                                //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 SiPesat Report";
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderLeftText();

                                                Image img = Image.FromFile(Tools.ReportImage);
                                                worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
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

                }

                #endregion

                return true;
            }//else if
            #endregion

            #region KPD
            else if (_OjkRpt.ReportName.Equals("5"))
            {
                #region Txt

                if (_OjkRpt.DownloadMode == "Txt")
                {
                    try
                    {
                        DateTime _datetimeNow = DateTime.Now;
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                string _companyCode = "";


                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                        create table #Text(      
                        [ResultText] [nvarchar](1000)  NULL          
                        )                        
        
                        truncate table #Text --     

          DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
DECLARE @KPD Table 
(
	KodeNasabah NVARCHAR(5),
	NamaNasabah NVARCHAR(200),
	NoKontrak NVARCHAR(100),
	TglKontrakFrom NVARCHAR(50),
	TglKontrakTo NVARCHAR(50),
	NoAdendum NVARCHAR(100),
	TglAdendum NVARCHAR(100),
	NilaiInvestasiAwalIDR NUMERIC(22,4),
	NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	NilaiInvestasiAkhirIDR NUMERIC(22,4),
	NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	JenisEfek NVARCHAR(100),
	DnLn NVARCHAR(2),
	JumlahEfek NUMERIC(22,4),
	NilaiPembelian NUMERIC(22,4),
	NilaiNominal NUMERIC(22,4),
	HPW NUMERIC(22,4),
	Deposito NUMERIC(22,4),
	TotalNilai NUMERIC(22,4),
	KodeBK NVARCHAR(10),
	Keterangan NVARCHAR(50),
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2
AND A.UnitAmount > 1  " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
While @@FETCH_STATUS = 0
Begin
	
	INSERT INTO @KPD
	        ( KodeNasabah ,
	          NamaNasabah ,
	          NoKontrak ,
	          TglKontrakFrom ,
	          TglKontrakTo ,
	          NoAdendum ,
	          TglAdendum ,
	          NilaiInvestasiAwalIDR ,
	          NilaiInvestasiAwalNonIDR ,
	          NilaiInvestasiAkhirIDR ,
	          NilaiInvestasiAkhirNonIDR ,
	          JenisEfek ,
	          DnLn ,
	          JumlahEfek ,
	          NilaiPembelian ,
	          NilaiNominal ,
	          HPW ,
	          Deposito ,
	          TotalNilai ,
	          KodeBK ,
	          Keterangan ,
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

insert into #Text
 
select 
isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
'|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
'|' + isnull(RTRIM(LTRIM(isnull(NoKontrak,''))),'')  +  --3
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakFrom,'')))),'')  +  --4
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakTo,'')))),'')  +  --5
'|' + isnull(RTRIM(LTRIM(isnull(NoAdendum,''))),'')  +  --6
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglAdendum,'')))),'')  +  --7
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,0))),'')  +  --8
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalNonIDR,0))),'')  + --9
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirIDR,0))),'')  + --10
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirNonIDR,0))),'')  +  --11
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DnLn,'')))),'')  + --13
'|' + isnull(RTRIM(LTRIM(isnull(JumlahEfek,0))),'')  + --14
'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,0))),'')  +  --15
'|' + isnull(RTRIM(LTRIM(isnull(NilaiNominal,0))),'')  +  --16
'|' + isnull(RTRIM(LTRIM(isnull(HPW,0))),'')  +  --17
'|' + isnull(RTRIM(LTRIM(isnull(Deposito,0))),'')  +  --18
'|' + isnull(RTRIM(LTRIM(isnull(TotalNilai,0))),'')  +  --19
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Keterangan,'')))),'') + --21
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
from @KPD

select * from #text

                         ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _Fund);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        _companyCode = _host.Get_CompanyID();
                                        string filePath = Tools.ARIATextPath + _companyCode + "KPD.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            while (dr.Read())
                                            {
                                                file.WriteLine(Convert.ToString(dr["ResultText"]));
                                            }
                                            //return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
                                            return true;
                                        }

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

                #endregion

                #region Excel

                if (_OjkRpt.DownloadMode == "Excel")
                {
                    try
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText =
                                @"

DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
DECLARE @KPD Table 
(
	KodeNasabah NVARCHAR(5),
	NamaNasabah NVARCHAR(200),
	NoKontrak NVARCHAR(100),
	TglKontrakFrom NVARCHAR(50),
	TglKontrakTo NVARCHAR(50),
	NoAdendum NVARCHAR(100),
	TglAdendum NVARCHAR(100),
	NilaiInvestasiAwalIDR NUMERIC(22,4),
	NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	NilaiInvestasiAkhirIDR NUMERIC(22,4),
	NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	JenisEfek NVARCHAR(100),
	DnLn NVARCHAR(2),
	JumlahEfek NUMERIC(22,4),
	NilaiPembelian NUMERIC(22,4),
	NilaiNominal NUMERIC(22,4),
	HPW NUMERIC(22,4),
	Deposito NUMERIC(22,4),
	TotalNilai NUMERIC(22,4),
	KodeBK NVARCHAR(10),
	Keterangan NVARCHAR(50),
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2 
AND A.UnitAmount > 1 " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
While @@FETCH_STATUS = 0
Begin
	
	INSERT INTO @KPD
	        ( KodeNasabah ,
	          NamaNasabah ,
	          NoKontrak ,
	          TglKontrakFrom ,
	          TglKontrakTo ,
	          NoAdendum ,
	          TglAdendum ,
	          NilaiInvestasiAwalIDR ,
	          NilaiInvestasiAwalNonIDR ,
	          NilaiInvestasiAkhirIDR ,
	          NilaiInvestasiAkhirNonIDR ,
	          JenisEfek ,
	          DnLn ,
	          JumlahEfek ,
	          NilaiPembelian ,
	          NilaiNominal ,
	          HPW ,
	          Deposito ,
	          TotalNilai ,
	          KodeBK ,
	          Keterangan ,
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

SELECT * FROM @KPD
                            ";


                                //cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);

                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _sInvestRpt.FundFrom);

                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "KPD" + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "KPD" + "_" + _userID + ".pdf";
                                        FileInfo excelFile = new FileInfo(filePath);
                                        if (excelFile.Exists)
                                        {
                                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                            excelFile = new FileInfo(filePath);
                                        }


                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                        using (ExcelPackage package = new ExcelPackage(excelFile))
                                        {
                                            package.Workbook.Properties.Title = "KPDReport";
                                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPD Report");


                                            //ATUR DATA GROUPINGNYA DULU
                                            List<KPD> rList = new List<KPD>();
                                            while (dr0.Read())
                                            {


                                                KPD rSingle = new KPD();
                                                rSingle.KodeNasabah = Convert.ToString(dr0["KodeNasabah"]);
                                                rSingle.NamaNasabah = Convert.ToString(dr0["NamaNasabah"]);
                                                rSingle.NomorKontrak = Convert.ToString(dr0["NoKontrak"]);
                                                rSingle.TanggalKontrak = Convert.ToString(dr0["TglKontrakFrom"]);
                                                rSingle.TanggalJatuhTempo = Convert.ToString(dr0["TglKontrakTo"]);
                                                rSingle.NomorAdendum = Convert.ToString(dr0["NoAdendum"]);
                                                rSingle.TanggalAdendum = Convert.ToString(dr0["TglAdendum"]);
                                                rSingle.NilaiInvestasiAwalIDR = Convert.ToString(dr0["NilaiInvestasiAwalIDR"]);
                                                rSingle.NilaiInvestasiAwalNonIDR = Convert.ToString(dr0["NilaiInvestasiAwalNonIDR"]);
                                                rSingle.NilaiInvestasiAkhir = Convert.ToString(dr0["NilaiInvestasiAkhirIDR"]);
                                                rSingle.NilaiInvestasiAkhirNonIDR = Convert.ToString(dr0["NilaiInvestasiAkhirNonIDR"]);
                                                rSingle.JenisEfek = Convert.ToString(dr0["JenisEfek"]);
                                                rSingle.KodeKategoriEfek = Convert.ToInt32(dr0["DnLn"]);
                                                rSingle.JumlahEfek = Convert.ToString(dr0["JumlahEfek"]);
                                                rSingle.NilaiPembelian = Convert.ToString(dr0["NilaiPembelian"]);
                                                rSingle.NilaiNominal = Convert.ToString(dr0["NilaiNominal"]);
                                                rSingle.HPW = Convert.ToString(dr0["HPW"]);
                                                rSingle.Deposito = Convert.ToString(dr0["Deposito"]);
                                                rSingle.TotalInvestasi = Convert.ToString(dr0["TotalNilai"]);
                                                rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                rSingle.Keterangan = Convert.ToString(dr0["Keterangan"]);
                                                rSingle.SID = Convert.ToString(dr0["SID"]);
                                                rList.Add(rSingle);

                                            }

                                            var QueryByClientID =
                                             from r in rList
                                             group r by new { } into rGroup
                                             select rGroup;

                                            int incRowExcel = 0;
                                            int _startRowDetail = 0;
                                            foreach (var rsHeader in QueryByClientID)
                                            {

                                                incRowExcel++;
                                                //Row A = 2
                                                int RowA = incRowExcel;
                                                int RowB = incRowExcel + 1;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.WrapText = true;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                worksheet.Cells[incRowExcel, 1].Value = "Kode Nasabah";
                                                worksheet.Cells[incRowExcel, 2].Value = "Nama Nasabah";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nomer Kontrak";
                                                worksheet.Cells[incRowExcel, 4].Value = "Tanggal Kontrak";
                                                worksheet.Cells[incRowExcel, 5].Value = "Tanggal Jatuh Tempo";
                                                worksheet.Cells[incRowExcel, 6].Value = "Nomer Adendum";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tanggal Adendum";
                                                worksheet.Cells[incRowExcel, 8].Value = "Nilai Investasi Awal IDR";
                                                worksheet.Cells[incRowExcel, 9].Value = "Nilai Investasi Awal Non IDR";
                                                worksheet.Cells[incRowExcel, 10].Value = "Nilai investasi Akhir IDR";
                                                worksheet.Cells[incRowExcel, 11].Value = "Nilai investasi Akhir Non IDR";
                                                worksheet.Cells[incRowExcel, 12].Value = "Kode Efek";
                                                worksheet.Cells[incRowExcel, 13].Value = "Kode Kategori Efek";
                                                worksheet.Cells[incRowExcel, 14].Value = "Jumlah Efek";
                                                worksheet.Cells[incRowExcel, 15].Value = "Nilai Pembelian";
                                                worksheet.Cells[incRowExcel, 16].Value = "Nilai Nominal";
                                                worksheet.Cells[incRowExcel, 17].Value = "HPW";
                                                worksheet.Cells[incRowExcel, 18].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 19].Value = "Total Investasi";
                                                worksheet.Cells[incRowExcel, 20].Value = "Kode BK";
                                                worksheet.Cells[incRowExcel, 21].Value = "Keterangan";
                                                worksheet.Cells[incRowExcel, 22].Value = "SID";

                                                //area header
                                                int _endRowDetail = 0;
                                                int _startRow = incRowExcel;
                                                incRowExcel++;
                                                _startRowDetail = incRowExcel;
                                                foreach (var rsDetail in rsHeader)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.KodeNasabah;
                                                    worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaNasabah;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.NomorKontrak;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TanggalKontrak;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalJatuhTempo;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NomorAdendum;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.TanggalAdendum;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NilaiInvestasiAwalIDR;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.NilaiInvestasiAwalNonIDR;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.NilaiInvestasiAkhir;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.NilaiInvestasiAkhirNonIDR;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.JenisEfek;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.KodeKategoriEfek;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.JumlahEfek;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.NilaiPembelian;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.NilaiNominal;
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.HPW;
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.Deposito;
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.TotalInvestasi;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.KodeBK;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.Keterangan;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.SID;

                                                    _endRowDetail = incRowExcel;

                                                    incRowExcel++;


                                                }

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                //worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                //worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                //worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                //worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                //worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                //worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                //worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                //worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                //worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                //worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                //worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                //worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                //worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                //worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                //worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                //worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                //worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                //worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                //worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                //worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                //worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Font.Bold = true;

                                                worksheet.Cells["A" + _startRow + ":V" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }



                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 1;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 22];
                                            worksheet.Column(1).Width = 9;
                                            worksheet.Column(2).Width = 35;
                                            worksheet.Column(3).Width = 20;
                                            worksheet.Column(4).Width = 20;
                                            worksheet.Column(5).Width = 20;
                                            worksheet.Column(6).Width = 20;
                                            worksheet.Column(7).Width = 20;
                                            worksheet.Column(8).Width = 20;
                                            worksheet.Column(9).Width = 20;
                                            worksheet.Column(10).Width = 20;
                                            worksheet.Column(11).Width = 20;
                                            worksheet.Column(12).Width = 20;
                                            worksheet.Column(13).Width = 20;
                                            worksheet.Column(14).Width = 20;
                                            worksheet.Column(15).Width = 20;
                                            worksheet.Column(16).Width = 20;
                                            worksheet.Column(17).Width = 20;
                                            worksheet.Column(18).Width = 20;
                                            worksheet.Column(19).Width = 20;
                                            worksheet.Column(20).Width = 20;
                                            worksheet.Column(21).Width = 20;
                                            worksheet.Column(22).Width = 20;



                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                            worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 KPD REPORT";

                                            // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                            worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                            worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                            worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                            worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                            //Image img = Image.FromFile(Tools.ReportImage);
                                            //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                            //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                            worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                            string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                            ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                            //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



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



                #endregion

                return true;
            }//else if
            #endregion

            #region NKPD
            else if (_OjkRpt.ReportName.Equals("3"))
            {
                #region Txt
                if (_OjkRpt.DownloadMode == "Txt")
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
                         declare @FinalDate datetime
                         set @FinalDate = dbo.FWorkingDay(@Date ,-1)

            --drop table #Text --
            create table #Text(                    
            [ResultText] [nvarchar](1000)  NULL                    
            )                     
                
            insert into #Text   
                 

            SELECT  RTRIM(LTRIM(isnull(FU.NKPDName,'')))             
            + '|' + RTRIM(LTRIM(isnull(AAA.NKPDCode,'')))         
            + '|' + RTRIM(LTRIM(isnull(A.jumlahPerorangan,0)))
            + '|' + CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    
            + '|' + CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    
            + '|' + CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    
            + '|' + CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(I.jumlahBank,0)))        
            + '|' + CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
            + '|' + RTRIM(LTRIM(isnull(K.jumlahPT,0)))     
            + '|' + CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                    
            + '|' + RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     
            + '|' + CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        
            + '|' + CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
            + '|' + RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  
            + '|' + CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     
            + '|' + CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
            + '|' + RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0)))
            + '|' + CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))             
            
            ------ASING            
            + '|' + RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0)))      
            + '|' + CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     
            + '|' + CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0)))  
            + '|' + CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        
            + '|' + CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
            + '|' + RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   
            + '|' + CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                        
            + '|' + RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) 
            + '|' + CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                         
            + '|' + RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    
            + '|' + CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   
            + '|' + CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
            + '|' + RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  
            + '|' + CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
            + '|' + RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       
            + '|' + CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           
            + '|' + CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) 
            + '|' +   CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
             ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
            / (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
             )) * 100 END AS NVARCHAR(30))    
	       
            + '|' + CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
             ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
            / (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
             )) * 100 END AS NVARCHAR(30))  
             
            FROM FundClientPosition FCP (NOLOCK)                      
            LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in (1,2)         
            LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
            LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
            LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status in (1,2)
            LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status in (1,2)
            ----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status in (1,2)       
            LEFT JOIN             
            (            
            select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
            left join FundClient g            
            on CS.FundClientPK = g.FundClientPK   and g.Status in (1,2)       
            where g.InvestorType = 1 and g.nationality= 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 1          
            and g.SACode = ''            
            group by CS.FundPK            
            ) A On FCP.FundPK = A.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG        
            on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)      
            where CG.InvestorType = 1 and CG.nationality= 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0   
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) B On FCP.FundPK = B.FundPK            
             
            LEFT JOIN             
            (            
            ----------EFEK----------------        
            select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) C On FCP.FundPK = C.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
            and CG.SACode = ''                
            
            group by CS.FundPK            
            ) D On FCP.FundPK = D.FundPK            
             
             
            LEFT JOIN             
            (            
            ---------DANA PENSIUN-------------        
            select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) E On FCP.FundPK = E.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) F On FCP.FundPK = F.FundPK            
             
            LEFT JOIN             
            (            
            ----------ASURANSI-----------        
            select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) G On FCP.FundPK = G.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)    
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4          
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) H On FCP.FundPK = H.FundPK            
             
            LEFT JOIN             
            (            
            ------------BANK-----------        
            select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)           
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) I On FCP.FundPK = I.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)          
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) J On FCP.FundPK = J.FundPK            
             
            LEFT JOIN             
            (            
            --------PEURSAHAAN SWASTA-----------        
            select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)          
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) K On FCP.FundPK = K.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)       
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) L On FCP.FundPK = L.FundPK            
             
            LEFT JOIN             
            (            
            ---------------BUMN----------------        
            select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) M On FCP.FundPK = M.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1 
            and CG.SACode = ''                         
            
            group by CS.FundPK            
            ) N On FCP.FundPK = N.FundPK            
             
            LEFT JOIN             
            (            
            -------------BUMD-------------        
            select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8      
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) O On FCP.FundPK = O.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8    
            and CG.SACode = ''                     
            
            group by CS.FundPK            
            ) P On FCP.FundPK = P.FundPK            
             
            LEFT JOIN             
            (            
            -----YAYASAN-----------        
            select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) Q On FCP.FundPK = Q.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2       
            and CG.SACode = ''                
            
            group by CS.FundPK            
            ) R On FCP.FundPK = R.FundPK            
             
            LEFT JOIN             
            (            
            ------------KOPERASI--------------        
            select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                     
            
            group by CS.FundPK            
            ) S On FCP.FundPK = S.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) T On FCP.FundPK = T.FundPK    

					
            ------------LEMBAGA LAINNYA--------------            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8           
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) U On FCP.FundPK = U.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8         
            and CG.SACode = ''               
            
            group by CS.FundPK            
            ) V On FCP.FundPK = V.FundPK            
             
            ----ASING            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 1 and CG.nationality <> 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AA On FCP.FundPK = AA.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)       
            where CG.InvestorType = 1 and CG.nationality <> 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0             
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AB On FCP.FundPK = AB.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)   
            and CG.SACode = ''                    
            
            group by CS.FundPK          
            ) AC On FCP.FundPK = AC.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)    
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AD On FCP.FundPK = AD.FundPK            
             
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)    
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AE On FCP.FundPK = AE.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6     
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AF On FCP.FundPK = AF.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) AG On FCP.FundPK = AG.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4       
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AH On FCP.FundPK = AH.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)         
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) AI On FCP.FundPK = AI.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)
            and CG.SACode = ''                        
            
            group by CS.FundPK            
            ) AJ On FCP.FundPK = AJ.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7) 
            and CG.SACode = ''                      
            
            group by CS.FundPK            
            ) AK On FCP.FundPK = AK.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)  
            and CG.SACode = ''                       
            
            group by CS.FundPK            
            ) AL On FCP.FundPK = AL.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK   and CG.Status in (1,2)       
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1            
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) AM On FCP.FundPK = AM.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AN On FCP.FundPK = AN.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<>'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8  
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) AO On FCP.FundPK = AO.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8      
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AP On FCP.FundPK = AP.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'          
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2      
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AQ On FCP.FundPK = AQ.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2       
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AR On FCP.FundPK = AR.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8 
            and CG.SACode = ''                        
            
            group by CS.FundPK            
            ) SS On FCP.FundPK = SS.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                      
            
            group by CS.FundPK            
            ) AT On FCP.FundPK = AT.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AU On FCP.FundPK = AU.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8         
            and CG.SACode = ''               
            
            group by CS.FundPK            
            ) AV On FCP.FundPK = AV.FundPK            
            left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in (1,2)       
            WHERE FCP.Date =@date 
            and Z.FundTypeInternal <> 2   -- BUKAN KPD        
            GROUP BY FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
            B.UnitAmount           
            ,C.jumlahPerusahaanEfek,            
            D.UnitAmount ,E.jumlahDanaPensiun,            
            F.UnitAmount ,G.jumlahAsuransi,            
            H.UnitAmount ,I.jumlahBank,           
            J.UnitAmount ,K.jumlahPT,            
            L.UnitAmount ,M.jumlahBUMN,            
            N.UnitAmount ,O.jumlahBUMD,            
            P.UnitAmount ,Q.jumlahYayasan,            
            R.UnitAmount ,S.jumlahKoperasi,            
            T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
            V.UnitAmount ,            
            ----asing            
            AA.jumlahPeroranganAsing,            
            AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
            AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
            AF.UnitAmount ,AG.jumlahAsuransiAsing,            
            AH.UnitAmount ,AI.jumlahBankAsing,            
            AJ.UnitAmount ,AK.jumlahPTAsing,            
            AL.UnitAmount ,AM.jumlahBUMNAsing,            
            AN.UnitAmount ,AO.jumlahBUMDAsing,            
            AP.UnitAmount ,AQ.jumlahYayasanAsing,            
            AR.UnitAmount ,SS.jumlahKoperasiAsing,            
            AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
            AV.UnitAmount , FCP.FundPK
             
            select * from #text

                                 ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        string filePath = Tools.ARIATextPath + "NKPD01.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            while (dr.Read())
                                            {

                                                file.WriteLine(Convert.ToString(dr["ResultText"]));
                                            }
                                            return true;
                                            //return Tools.HtmlARIATextPath + "NKPD01.txt";
                                        }

                                    }
                                    return true;
                                }

                            }
                        }
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }

                } // bates

                #endregion

                #region Excel
                else
                {

                    if (_OjkRpt.ReportName.Equals("3"))
                    {
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {

                                    string _paramFund = "";
                                    _paramFund = "left(@FundFrom,charindex('-',@FundFrom) - 1) ";


                                    cmd.CommandText =
                                    @"
                             Declare @FinalDate datetime
   set @FinalDate =dbo.FWorkingDay(@Date ,-1)

                    SELECT  RTRIM(LTRIM(isnull(FU.Name,''))) FundName,  
					RTRIM(LTRIM(isnull(FU.NKPDName,''))) KodeProduk            
, RTRIM(LTRIM(isnull(AAA.NKPDCode,''))) KodeBK         
, RTRIM(LTRIM(isnull(A.jumlahPerorangan,0))) JmlNasabahPerorangan
, CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahPerorangan                   
, RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    JmlNasabahLembagaPE
, CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))  DanaNasabahLembagaPE                   
, RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    JmlNasabahLembagaDAPEN
, CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaDAPEN                 
, RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    JmlNasabahLembagaAsuransi
, CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaAsuransi                  
, RTRIM(LTRIM(isnull(I.jumlahBank,0)))        JmlNasabahLembagaBank
, CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBank             
, RTRIM(LTRIM(isnull(K.jumlahPT,0)))     JmlNasabahLembagaSwasta
, CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaSwasta                   
, RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     JmlNasabahLembagaBUMN
, CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMN                 
, RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        JmlNasabahLembagaBUMD
, CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMD             
, RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  JmlNasabahLembagaYayasan
, CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaYayasan                    
, RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     JmlNasabahLembagaKoperasi
, CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaKoperasi                
, RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0))) JmlNasabahLembagaLainnya
, CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaLainnya            
            
------ASING            
, RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0))) JmlAsingPerorangan     
, CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingPerorangan                  
, RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     JmlAsingLembagaPE
, CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaPE                    
, RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0))) JmlAsingLembagaDAPEN 
, CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaDAPEN                  
, RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        JmlAsingLembagaAsuransi
, CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaAsuransi                
, RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   JmlAsingLembagaBank
, CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBank                       
, RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) JmlAsingLembagaSwasta
, CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaSwasta                        
, RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    JmlAsingLembagaBUMN
, CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMN                     
, RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   JmlAsingLembagaBUMD
, CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMD                      
, RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  JmlAsingLembagaYayasan
, CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaYayasan                      
, RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       JmlAsingLembagaKoperasi
, CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaKoperasi                  
, RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           JmlAsingLembagaLainnya
, CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaLainnya


, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiDN



, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiLN
	         
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in  (1,2)           
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status = 2  
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status = 2  
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status = 2         
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in  (1,2)         
where g.InvestorType = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1     
and g.SACode = ''      
            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)       
where CG.InvestorType = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''           
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''               
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6   
and CG.SACode = ''            
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''          
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''           
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''             
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)   
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''          
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1     
and CG.SACode = ''          
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1   
and CG.SACode = ''               
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''              
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''               
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''             
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''            
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''          
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)  
and CG.SACode = ''              
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0     
and CG.SACode = ''           
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)         
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0    
and CG.SACode = ''             
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)     
and CG.SACode = ''        
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6        
and CG.SACode = ''     
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''           
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''        
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''           
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''          
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''            
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''          
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''           
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1   
and CG.SACode = ''            
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1      
and CG.SACode = ''       
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''        
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8 
and CG.SACode = ''             
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2    
and CG.SACode = ''         
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2   
and CG.SACode = ''           
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''            
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)   
and CG.SACode = ''           
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in  (1,2)        
WHERE FCP.Date =@FinalDate
and Z.FundTypeInternal <> 2   -- BUKAN KPD        
AND FCP.UnitAmount > 10
GROUP BY FU.Name,FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK ";

                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@date", _OjkRpt.Date);


                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".xlsx";
                                            string pdfPath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".pdf";
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }


                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "NKPDReport";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NKPD Report");


                                                //ATUR DATA GROUPINGNYA DULU
                                                List<NKPD> rList = new List<NKPD>();
                                                while (dr0.Read())
                                                {
                                                    NKPD rSingle = new NKPD();
                                                    rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                                    rSingle.KodeProduk = Convert.ToString(dr0["KodeProduk"]);
                                                    rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                    rSingle.JmlNasabahPerorangan = Convert.ToInt32(dr0["JmlNasabahPerorangan"]);
                                                    rSingle.DanaNasabahPerorangan = Convert.ToDecimal(dr0["DanaNasabahPerorangan"]);
                                                    rSingle.JmlNasabahLembagaPE = Convert.ToInt32(dr0["JmlNasabahLembagaPE"]);
                                                    rSingle.DanaNasabahLembagaPE = Convert.ToDecimal(dr0["DanaNasabahLembagaPE"]);
                                                    rSingle.JmlNasabahLembagaDAPEN = Convert.ToInt32(dr0["JmlNasabahLembagaDAPEN"]);
                                                    rSingle.DanaNasabahLembagaDAPEN = Convert.ToDecimal(dr0["DanaNasabahLembagaDAPEN"]);
                                                    rSingle.JmlNasabahLembagaAsuransi = Convert.ToInt32(dr0["JmlNasabahLembagaAsuransi"]);
                                                    rSingle.DanaNasabahLembagaAsuransi = Convert.ToDecimal(dr0["DanaNasabahLembagaAsuransi"]);
                                                    rSingle.JmlNasabahLembagaBank = Convert.ToInt32(dr0["JmlNasabahLembagaBank"]);
                                                    rSingle.DanaNasabahLembagaBank = Convert.ToDecimal(dr0["DanaNasabahLembagaBank"]);
                                                    rSingle.JmlNasabahLembagaSwasta = Convert.ToInt32(dr0["JmlNasabahLembagaSwasta"]);
                                                    rSingle.DanaNasabahLembagaSwasta = Convert.ToDecimal(dr0["DanaNasabahLembagaSwasta"]);
                                                    rSingle.JmlNasabahLembagaBUMN = Convert.ToInt32(dr0["JmlNasabahLembagaBUMN"]);
                                                    rSingle.DanaNasabahLembagaBUMN = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMN"]);
                                                    rSingle.JmlNasabahLembagaBUMD = Convert.ToInt32(dr0["JmlNasabahLembagaBUMD"]);
                                                    rSingle.DanaNasabahLembagaBUMD = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMD"]);
                                                    rSingle.JmlNasabahLembagaYayasan = Convert.ToInt32(dr0["JmlNasabahLembagaYayasan"]);
                                                    rSingle.DanaNasabahLembagaYayasan = Convert.ToDecimal(dr0["DanaNasabahLembagaYayasan"]);
                                                    rSingle.JmlNasabahLembagaKoperasi = Convert.ToInt32(dr0["JmlNasabahLembagaKoperasi"]);
                                                    rSingle.DanaNasabahLembagaKoperasi = Convert.ToDecimal(dr0["DanaNasabahLembagaKoperasi"]);
                                                    rSingle.JmlNasabahLembagaLainnya = Convert.ToInt32(dr0["JmlNasabahLembagaLainnya"]);
                                                    rSingle.DanaNasabahLembagaLainnya = Convert.ToDecimal(dr0["DanaNasabahLembagaLainnya"]);
                                                    rSingle.JmlAsingPerorangan = Convert.ToInt32(dr0["JmlAsingPerorangan"]);
                                                    rSingle.DanaAsingPerorangan = Convert.ToDecimal(dr0["DanaAsingPerorangan"]);
                                                    rSingle.JmlAsingLembagaPE = Convert.ToInt32(dr0["JmlAsingLembagaPE"]);
                                                    rSingle.DanaAsingLembagaPE = Convert.ToDecimal(dr0["DanaAsingLembagaPE"]);
                                                    rSingle.JmlAsingLembagaDAPEN = Convert.ToInt32(dr0["JmlAsingLembagaDAPEN"]);
                                                    rSingle.DanaAsingLembagaDAPEN = Convert.ToDecimal(dr0["DanaAsingLembagaDAPEN"]);
                                                    rSingle.JmlAsingLembagaAsuransi = Convert.ToInt32(dr0["JmlAsingLembagaAsuransi"]);
                                                    rSingle.DanaAsingLembagaAsuransi = Convert.ToDecimal(dr0["DanaAsingLembagaAsuransi"]);
                                                    rSingle.JmlAsingLembagaBank = Convert.ToInt32(dr0["JmlAsingLembagaBank"]);
                                                    rSingle.DanaAsingLembagaBank = Convert.ToDecimal(dr0["DanaAsingLembagaBank"]);
                                                    rSingle.JmlAsingLembagaSwasta = Convert.ToInt32(dr0["JmlAsingLembagaSwasta"]);
                                                    rSingle.DanaAsingLembagaSwasta = Convert.ToDecimal(dr0["DanaAsingLembagaSwasta"]);
                                                    rSingle.JmlAsingLembagaBUMN = Convert.ToInt32(dr0["JmlAsingLembagaBUMN"]);
                                                    rSingle.DanaAsingLembagaBUMN = Convert.ToDecimal(dr0["DanaAsingLembagaBUMN"]);
                                                    rSingle.JmlAsingLembagaBUMD = Convert.ToInt32(dr0["JmlAsingLembagaBUMD"]);
                                                    rSingle.DanaAsingLembagaBUMD = Convert.ToDecimal(dr0["DanaAsingLembagaBUMD"]);
                                                    rSingle.JmlAsingLembagaYayasan = Convert.ToInt32(dr0["JmlAsingLembagaYayasan"]);
                                                    rSingle.DanaAsingLembagaYayasan = Convert.ToDecimal(dr0["DanaAsingLembagaYayasan"]);
                                                    rSingle.JmlAsingLembagaKoperasi = Convert.ToInt32(dr0["JmlAsingLembagaKoperasi"]);
                                                    rSingle.DanaAsingLembagaKoperasi = Convert.ToDecimal(dr0["DanaAsingLembagaKoperasi"]);
                                                    rSingle.JmlAsingLembagaLainnya = Convert.ToInt32(dr0["JmlAsingLembagaLainnya"]);
                                                    rSingle.DanaAsingLembagaLainnya = Convert.ToDecimal(dr0["DanaAsingLembagaLainnya"]);
                                                    rSingle.InvestasiDN = Convert.ToDecimal(dr0["InvestasiDN"]);
                                                    rSingle.InvestasiLN = Convert.ToDecimal(dr0["InvestasiLN"]);
                                                    rList.Add(rSingle);

                                                }

                                                var QueryByClientID =
                                                 from r in rList
                                                 group r by new { } into rGroup
                                                 select rGroup;

                                                int incRowExcel = 0;
                                                int _startRowDetail = 0;
                                                foreach (var rsHeader in QueryByClientID)
                                                {

                                                    incRowExcel++;
                                                    //Row A = 2
                                                    int RowA = incRowExcel;
                                                    int RowB = incRowExcel + 1;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.WrapText = true;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                    worksheet.Cells[incRowExcel, 1].Value = "Nama Produk";
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Produk";
                                                    worksheet.Cells[incRowExcel, 3].Value = "Kode BK";
                                                    worksheet.Cells[incRowExcel, 4].Value = "Jumlah Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 5].Value = "Dana Kelolaan Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 6].Value = "Jumlah Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 7].Value = "Dana Kelolaan Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 8].Value = "Jumlah Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 9].Value = "Dana Kelolaan Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 10].Value = "Jumlah Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 11].Value = "Dana Kelolaan Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 12].Value = "Jumlah Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 13].Value = "Dana Kelolaan Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 14].Value = "Jumlah Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 15].Value = "Dana Kelolaan Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 16].Value = "Jumlah Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 17].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 18].Value = "Jumlah Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 19].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 20].Value = "Jumlah Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 21].Value = "Dana Kelolaan Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 22].Value = "Jumlah Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 23].Value = "Dana Kelolaan Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 24].Value = "Jumlah Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 25].Value = "Dana Kelolaan Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 26].Value = "Jumlah Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 27].Value = "Dana Kelolaan Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 28].Value = "Jumlah Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 29].Value = "Dana Kelolaan Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 30].Value = "Jumlah Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 31].Value = "Dana Kelolaan Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 32].Value = "Jumlah Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 33].Value = "Dana Kelolaan Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 34].Value = "Jumlah Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 35].Value = "Dana Kelolaan Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 36].Value = "Jumlah Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 37].Value = "Dana Kelolaan Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 38].Value = "Jumlah Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 39].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 40].Value = "Jumlah Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 41].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 42].Value = "Jumlah Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 43].Value = "Dana Kelolaan Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 44].Value = "Jumlah Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 45].Value = "Dana Kelolaan Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 46].Value = "Jumlah Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 47].Value = "Dana Kelolaan Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 48].Value = "Investasi DN";
                                                    worksheet.Cells[incRowExcel, 49].Value = "Investasi LN";
                                                    worksheet.Cells[incRowExcel, 50].Value = "Total";

                                                    incRowExcel++;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 1].Value = "(1)";
                                                    worksheet.Cells[incRowExcel, 2].Value = "(2)";
                                                    worksheet.Cells[incRowExcel, 3].Value = "(3)";
                                                    worksheet.Cells[incRowExcel, 4].Value = "(4)";
                                                    worksheet.Cells[incRowExcel, 5].Value = "(5)";
                                                    worksheet.Cells[incRowExcel, 6].Value = "(6)";
                                                    worksheet.Cells[incRowExcel, 7].Value = "(7)";
                                                    worksheet.Cells[incRowExcel, 8].Value = "(8)";
                                                    worksheet.Cells[incRowExcel, 9].Value = "(9)";
                                                    worksheet.Cells[incRowExcel, 10].Value = "(10)";
                                                    worksheet.Cells[incRowExcel, 11].Value = "(11)";
                                                    worksheet.Cells[incRowExcel, 12].Value = "(12)";
                                                    worksheet.Cells[incRowExcel, 13].Value = "(13)";
                                                    worksheet.Cells[incRowExcel, 14].Value = "(14)";
                                                    worksheet.Cells[incRowExcel, 15].Value = "(15)";
                                                    worksheet.Cells[incRowExcel, 16].Value = "(16)";
                                                    worksheet.Cells[incRowExcel, 17].Value = "(17)";
                                                    worksheet.Cells[incRowExcel, 18].Value = "(18)";
                                                    worksheet.Cells[incRowExcel, 19].Value = "(19)";
                                                    worksheet.Cells[incRowExcel, 20].Value = "(20)";
                                                    worksheet.Cells[incRowExcel, 21].Value = "(21)";
                                                    worksheet.Cells[incRowExcel, 22].Value = "(22)";
                                                    worksheet.Cells[incRowExcel, 23].Value = "(23)";
                                                    worksheet.Cells[incRowExcel, 24].Value = "(24)";
                                                    worksheet.Cells[incRowExcel, 25].Value = "(25)";
                                                    worksheet.Cells[incRowExcel, 26].Value = "(26)";
                                                    worksheet.Cells[incRowExcel, 27].Value = "(27)";
                                                    worksheet.Cells[incRowExcel, 28].Value = "(28)";
                                                    worksheet.Cells[incRowExcel, 29].Value = "(29)";
                                                    worksheet.Cells[incRowExcel, 30].Value = "(30)";
                                                    worksheet.Cells[incRowExcel, 31].Value = "(31)";
                                                    worksheet.Cells[incRowExcel, 32].Value = "(32)";
                                                    worksheet.Cells[incRowExcel, 33].Value = "(33)";
                                                    worksheet.Cells[incRowExcel, 34].Value = "(34)";
                                                    worksheet.Cells[incRowExcel, 35].Value = "(35)";
                                                    worksheet.Cells[incRowExcel, 36].Value = "(36)";
                                                    worksheet.Cells[incRowExcel, 37].Value = "(37)";
                                                    worksheet.Cells[incRowExcel, 38].Value = "(38)";
                                                    worksheet.Cells[incRowExcel, 39].Value = "(39)";
                                                    worksheet.Cells[incRowExcel, 40].Value = "(40)";
                                                    worksheet.Cells[incRowExcel, 41].Value = "(41)";
                                                    worksheet.Cells[incRowExcel, 42].Value = "(42)";
                                                    worksheet.Cells[incRowExcel, 43].Value = "(43)";
                                                    worksheet.Cells[incRowExcel, 44].Value = "(44)";
                                                    worksheet.Cells[incRowExcel, 45].Value = "(45)";
                                                    worksheet.Cells[incRowExcel, 46].Value = "(46)";
                                                    worksheet.Cells[incRowExcel, 47].Value = "(47)";
                                                    worksheet.Cells[incRowExcel, 48].Value = "(48)";

                                                    //area header
                                                    int _endRowDetail = 0;
                                                    int _startRow = incRowExcel;
                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    foreach (var rsDetail in rsHeader)
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.KodeProduk;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.KodeBK;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.JmlNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.DanaNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.JmlNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.DanaNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.JmlNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.DanaNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.JmlNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail.DanaNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail.JmlNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail.DanaNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.JmlNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.DanaNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 16].Value = rsDetail.JmlNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 17].Value = rsDetail.DanaNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 18].Value = rsDetail.JmlNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 19].Value = rsDetail.DanaNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 20].Value = rsDetail.JmlNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 21].Value = rsDetail.DanaNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 22].Value = rsDetail.JmlNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 23].Value = rsDetail.DanaNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 24].Value = rsDetail.JmlNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 25].Value = rsDetail.DanaNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 26].Value = rsDetail.JmlAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 27].Value = rsDetail.DanaAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 28].Value = rsDetail.JmlAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 29].Value = rsDetail.DanaAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 30].Value = rsDetail.JmlAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 31].Value = rsDetail.DanaAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 32].Value = rsDetail.JmlAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 33].Value = rsDetail.DanaAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 34].Value = rsDetail.JmlAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 35].Value = rsDetail.DanaAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 36].Value = rsDetail.JmlAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 37].Value = rsDetail.DanaAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 38].Value = rsDetail.JmlAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 39].Value = rsDetail.DanaAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 40].Value = rsDetail.JmlAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 41].Value = rsDetail.DanaAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 42].Value = rsDetail.JmlAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 43].Value = rsDetail.DanaAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 44].Value = rsDetail.JmlAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 45].Value = rsDetail.DanaAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 46].Value = rsDetail.JmlAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 47].Value = rsDetail.DanaAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 48].Value = rsDetail.InvestasiDN;
                                                        worksheet.Cells[incRowExcel, 49].Value = rsDetail.InvestasiLN;
                                                        worksheet.Cells[incRowExcel, 50].Formula =
                                                        "SUM(E" + incRowExcel + "+G" + incRowExcel + "+I" + incRowExcel + "+K" + incRowExcel + "+M" + incRowExcel +
                                                        "+O" + incRowExcel + "+Q" + incRowExcel + "+S" + incRowExcel + "+U" + incRowExcel + "+W" + incRowExcel + "+Y" + incRowExcel +
                                                        "+AA" + incRowExcel + "+AC" + incRowExcel + "+AE" + incRowExcel + "+AG" + incRowExcel + "+AI" + incRowExcel + "+AK" + incRowExcel +
                                                        "+AM" + incRowExcel + "+AO" + incRowExcel + "+AQ" + incRowExcel + "+AS" + incRowExcel + "+AU" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 50].Calculate();

                                                        _endRowDetail = incRowExcel;

                                                        incRowExcel++;


                                                    }

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                    worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 23].Formula = "SUM(W" + _startRowDetail + ":W" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 24].Formula = "SUM(X" + _startRowDetail + ":X" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 25].Formula = "SUM(Y" + _startRowDetail + ":Y" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 26].Formula = "SUM(Z" + _startRowDetail + ":Z" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 27].Formula = "SUM(AA" + _startRowDetail + ":AA" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 28].Formula = "SUM(AB" + _startRowDetail + ":AB" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 29].Formula = "SUM(AC" + _startRowDetail + ":AC" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 30].Formula = "SUM(AD" + _startRowDetail + ":AD" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 31].Formula = "SUM(AE" + _startRowDetail + ":AE" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 32].Formula = "SUM(AF" + _startRowDetail + ":AF" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 33].Formula = "SUM(AG" + _startRowDetail + ":AG" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 34].Formula = "SUM(AH" + _startRowDetail + ":AH" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 35].Formula = "SUM(AI" + _startRowDetail + ":AI" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 36].Formula = "SUM(AJ" + _startRowDetail + ":AJ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 37].Formula = "SUM(AK" + _startRowDetail + ":AK" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 38].Formula = "SUM(AL" + _startRowDetail + ":AL" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 39].Formula = "SUM(AM" + _startRowDetail + ":AM" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 40].Formula = "SUM(AN" + _startRowDetail + ":AN" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 41].Formula = "SUM(AO" + _startRowDetail + ":AO" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 42].Formula = "SUM(AP" + _startRowDetail + ":AP" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 43].Formula = "SUM(AQ" + _startRowDetail + ":AQ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 44].Formula = "SUM(AR" + _startRowDetail + ":AR" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 45].Formula = "SUM(AS" + _startRowDetail + ":AS" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 46].Formula = "SUM(AT" + _startRowDetail + ":AT" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 47].Formula = "SUM(AU" + _startRowDetail + ":AU" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 48].Formula = "SUM(AV" + _startRowDetail + ":AV" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 49].Formula = "SUM(AW" + _startRowDetail + ":AW" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 50].Formula = "SUM(AX" + _startRowDetail + ":AX" + _endRowDetail + ")";
                                                    worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                    worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                    worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                    worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                    worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                    worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                    worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                    worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                    worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                    worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                    worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                    worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                    worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                    worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                    worksheet.Cells["V" + incRowExcel + ":W" + incRowExcel].Calculate();
                                                    worksheet.Cells["W" + incRowExcel + ":X" + incRowExcel].Calculate();
                                                    worksheet.Cells["X" + incRowExcel + ":Y" + incRowExcel].Calculate();
                                                    worksheet.Cells["Z" + incRowExcel + ":Z" + incRowExcel].Calculate();
                                                    worksheet.Cells["AA" + incRowExcel + ":AA" + incRowExcel].Calculate();
                                                    worksheet.Cells["AB" + incRowExcel + ":AB" + incRowExcel].Calculate();
                                                    worksheet.Cells["AC" + incRowExcel + ":AC" + incRowExcel].Calculate();
                                                    worksheet.Cells["AD" + incRowExcel + ":AD" + incRowExcel].Calculate();
                                                    worksheet.Cells["AE" + incRowExcel + ":AE" + incRowExcel].Calculate();
                                                    worksheet.Cells["AF" + incRowExcel + ":AF" + incRowExcel].Calculate();
                                                    worksheet.Cells["AG" + incRowExcel + ":AG" + incRowExcel].Calculate();
                                                    worksheet.Cells["AH" + incRowExcel + ":AH" + incRowExcel].Calculate();
                                                    worksheet.Cells["AI" + incRowExcel + ":AI" + incRowExcel].Calculate();
                                                    worksheet.Cells["AJ" + incRowExcel + ":AJ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AK" + incRowExcel + ":AK" + incRowExcel].Calculate();
                                                    worksheet.Cells["AL" + incRowExcel + ":AL" + incRowExcel].Calculate();
                                                    worksheet.Cells["AM" + incRowExcel + ":AM" + incRowExcel].Calculate();
                                                    worksheet.Cells["AN" + incRowExcel + ":AN" + incRowExcel].Calculate();
                                                    worksheet.Cells["AO" + incRowExcel + ":AO" + incRowExcel].Calculate();
                                                    worksheet.Cells["AP" + incRowExcel + ":AP" + incRowExcel].Calculate();
                                                    worksheet.Cells["AQ" + incRowExcel + ":AQ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AR" + incRowExcel + ":AR" + incRowExcel].Calculate();
                                                    worksheet.Cells["AS" + incRowExcel + ":AS" + incRowExcel].Calculate();
                                                    worksheet.Cells["AT" + incRowExcel + ":AT" + incRowExcel].Calculate();
                                                    worksheet.Cells["AU" + incRowExcel + ":AU" + incRowExcel].Calculate();
                                                    worksheet.Cells["AV" + incRowExcel + ":AV" + incRowExcel].Calculate();
                                                    worksheet.Cells["AW" + incRowExcel + ":AW" + incRowExcel].Calculate();
                                                    worksheet.Cells["AX" + incRowExcel + ":AX" + incRowExcel].Calculate();
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Font.Bold = true;

                                                    worksheet.Cells["A" + _startRow + ":AX" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                }



                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 1;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 50];
                                                worksheet.Column(1).Width = 45;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 10;
                                                worksheet.Column(4).Width = 20;
                                                worksheet.Column(5).Width = 20;
                                                worksheet.Column(6).Width = 20;
                                                worksheet.Column(7).Width = 20;
                                                worksheet.Column(8).Width = 20;
                                                worksheet.Column(9).Width = 20;
                                                worksheet.Column(10).Width = 20;
                                                worksheet.Column(11).Width = 20;
                                                worksheet.Column(12).Width = 20;
                                                worksheet.Column(13).Width = 20;
                                                worksheet.Column(14).Width = 20;
                                                worksheet.Column(15).Width = 20;
                                                worksheet.Column(16).Width = 20;
                                                worksheet.Column(17).Width = 20;
                                                worksheet.Column(18).Width = 20;
                                                worksheet.Column(19).Width = 20;
                                                worksheet.Column(20).Width = 20;
                                                worksheet.Column(21).Width = 20;
                                                worksheet.Column(22).Width = 20;
                                                worksheet.Column(23).Width = 20;
                                                worksheet.Column(24).Width = 20;
                                                worksheet.Column(25).Width = 20;
                                                worksheet.Column(26).Width = 20;
                                                worksheet.Column(27).Width = 20;
                                                worksheet.Column(28).Width = 20;
                                                worksheet.Column(29).Width = 20;
                                                worksheet.Column(30).Width = 20;
                                                worksheet.Column(31).Width = 20;
                                                worksheet.Column(32).Width = 20;
                                                worksheet.Column(33).Width = 20;
                                                worksheet.Column(34).Width = 20;
                                                worksheet.Column(35).Width = 20;
                                                worksheet.Column(36).Width = 20;
                                                worksheet.Column(37).Width = 20;
                                                worksheet.Column(38).Width = 20;
                                                worksheet.Column(39).Width = 20;
                                                worksheet.Column(40).Width = 20;
                                                worksheet.Column(41).Width = 20;
                                                worksheet.Column(42).Width = 20;
                                                worksheet.Column(43).Width = 20;
                                                worksheet.Column(44).Width = 20;
                                                worksheet.Column(45).Width = 20;
                                                worksheet.Column(46).Width = 20;
                                                worksheet.Column(47).Width = 20;
                                                worksheet.Column(48).Width = 20;
                                                worksheet.Column(49).Width = 20;
                                                worksheet.Column(50).Width = 20;



                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 NKPD REPORT";

                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                //Image img = Image.FromFile(Tools.ReportImage);
                                                //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                                //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                                //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



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

                }
                #endregion

                return true;
            }//else if
            #endregion

            else
            {
                return false;
            }
        }

    }
}