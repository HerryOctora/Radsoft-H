using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Data.OleDb;using RFSRepository;

namespace RFSRepositoryThree
{
    public class CustomClient99Reps
    {
        Host _host = new Host();

        public string PTPDeposito_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, bool _param5, Investment _investment)
        {

            try
            {
                string _paramSettlementPK = "";

                if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                {
                    _paramSettlementPK = " And A.SettlementPK in (" + _investment.stringInvestmentFrom + ") ";
                }
                else
                {
                    _paramSettlementPK = " And A.SettlementPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _mature = "";
                        if (_param5 == true)
                        {
                            if (Tools.ClientCode == "01")
                            {
                                _mature = @"union all

                                select CONVERT(varchar(15), [identity]) + '/FP/'  
                                + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.Balance Quantity, 
                                A.Balance TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.Balance TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.Balance OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.Balance * 1 AmountTrf, A.InterestPercent BreakInterestPercent,AcqDate, 
                                round(A.Balance * (A.InterestPercent/100)/365 * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,1 Mature,0 BitRollOverInterest
                                from FundPosition A
                                left join Fund C on A.fundpk = C.fundpk and C.status = 2
                                left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2  
                                left join Bank F on A.BankPK = F.BankPK and F.status = 2
                                left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                where A.MaturityDate = @ValueDate and A.TrailsPK = @TrailsPK ";
                            }
                            else if (Tools.ClientCode == "02")
                            {
                                _mature = @"
                                union all

                                select A.Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.DoneVolume Quantity, 
                                A.DoneVolume TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.DoneAmount TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.DoneVolume OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.DoneVolume * 1 AmountTrf, A.InterestPercent BreakInterestPercent,A.AcqDate,A.AcqDate , 
                                round(A.DoneVolume * (A.InterestPercent/100)/case when G.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,0 BitRollOverInterest
                                from InvestmentMature A
                                left join Investment I on A.InvestmentPK = I.InvestmentPK and I.statusDealing = 2
                                left join Fund C on I.FundPK = C.FundPK and C.status in (1,2)
                                left join instrument D on I.instrumentpk = D.instrumentpk and D.status in (1,2)
                                left join Bank F on I.BankPK = F.BankPK and F.status in (1,2)
                                left join BankBranch G on I.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                where A.MaturityDate = @ValueDate and Selected = 1 ";
                            }
                            else if (Tools.ClientCode == "03")
                            {
                                _mature = @"union all
                                select A.Reference,A.valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.DoneAmount Quantity, 
                                A.DoneAmount TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.DoneAmount TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.DoneAmount OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.DoneAmount * 1 AmountTrf, A.InterestPercent BreakInterestPercent,A.AcqDate, 
                                round(A.DoneAmount * (A.InterestPercent/100)/case when G.InterestDaysType in (2) then DATEDIFF(DAY, J.DateFrom, J.DateTo) + 1 else case when G.InterestDaysType in (4) then 365 else 360 end end  * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,1 Mature,A.Reference ParentReference,G.InterestDaysType,0 BitRollOverInterest
                                from InvestmentMature A
                                left join Investment B on A.InvestmentPK = B.InvestmentPK and B.StatusSettlement = 2 and B.InvestmentPK <> 0 
                                left join Fund C on A.FundID = C.ID and C.status = 2
                                left join instrument D on B.instrumentpk = D.instrumentpk and D.status = 2  
                                left join Bank F on B.BankPK = F.BankPK and F.status = 2
                                left join BankBranch G on B.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                left join Period J on B.PeriodPK = J.PeriodPK and J.status = 2
                                where A.MaturityDate = @ValueDate and Selected = 1";
                            }
                            else if (Tools.ClientCode == "05")
                            {
                                _mature = "";
                            }
                            else if (Tools.ClientCode == "19")
                            {
                                _mature = @"
                                union all

                                select CONVERT(varchar(15), [identity]) + '/FP/'  
                                + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.Balance Quantity, 
                                A.Balance TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.Balance TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.Balance OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.Balance * 1 AmountTrf, A.InterestPercent BreakInterestPercent,AcqDate,AcqDate , 
                                round(A.Balance * (A.InterestPercent/100)/365 * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,0 BitRollOverInterest
                                from FundPosition A
                                left join Fund C on A.fundpk = C.fundpk and C.status = 2
                                left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2  
                                left join Bank F on A.BankPK = F.BankPK and F.status = 2
                                left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                where A.MaturityDate = @ValueDate and A.TrailsPK = @TrailsPK  ";
                            }
                            else if (Tools.ClientCode == "25")
                            {
                                _mature = @"
                                union all

                                select A.Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.DoneVolume Quantity, 
                                A.DoneVolume TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.DoneAmount TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.DoneVolume OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.DoneVolume * 1 AmountTrf, A.InterestPercent BreakInterestPercent,A.AcqDate,A.AcqDate , 
                                round(A.DoneVolume * (A.InterestPercent/100)/case when G.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.MaturityDate),0) * 0.8 InterestAmount
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
                                _mature = @"union all
						        select A.Reference,A.valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.DoneAmount Quantity, 
                                A.DoneAmount TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                                A.DoneAmount TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                                0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.DoneAmount OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.DoneAmount * 1 AmountTrf, A.InterestPercent BreakInterestPercent,A.AcqDate, 
                                round(A.DoneAmount * (A.InterestPercent/100)/case when G.InterestDaysType in (2) then DATEDIFF(DAY, J.DateFrom, J.DateTo) + 1 else case when G.InterestDaysType in (4) then 365 else 360 end end  * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,1 Mature,A.Reference ParentReference,0 BitRollOverInterest
                                from InvestmentMature A
                                left join Investment B on A.InvestmentPK = B.InvestmentPK and B.StatusSettlement = 2 and B.InvestmentPK <> 0 
                                left join Fund C on A.FundID = C.ID and C.status = 2
                                left join instrument D on B.instrumentpk = D.instrumentpk and D.status = 2  
                                left join Bank F on B.BankPK = F.BankPK and F.status = 2
                                left join BankBranch G on B.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                left join Period J on B.PeriodPK = J.PeriodPK and J.status = 2
                                where A.MaturityDate = @ValueDate and Selected = 1 ";
                            }
                        }
                        else
                        {
                            _mature = "";
                        }

                        if (Tools.ClientCode == "01")
                        {
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
                            + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '0' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '0' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),'')as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                            + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '0' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '0' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),'')as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) end -- 12.Investment.ValueDate
                            + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                            else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                            + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                            + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                            + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),'')))) else '' end -- 17.WithdrawalDate
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                            + '|' + '' -- 20.Withdrawal Interest
                            + '|' + '' -- 21.Total Withdrawal Amount
                            + '|' + case when A.BitRolloverInterest = 1 then '2' else '' end-- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.BitRolloverInterest = 1 then '0' else case when A.Mature = 1 then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else cast(isnull(A.AmountTrf,0) as nvarchar) end end end-- 26.Amount to be Transfer
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
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                            case when A.DoneAmount = I.Balance then 0 else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,
                            round(A.DoneAmount * (A.BreakInterestPercent/100)/365 * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,0 Mature,A.BitRolloverInterest
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
                            " + _paramSettlementPK + @"
                            and A.statusdealing = 2

                            Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                            A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                            A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,A.BitRolloverInterest,G.Email1


                                " + _mature + @"

                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.Mature ,A.BitRolloverInterest
                            order by A.ValueDate Asc
                            select * from #text 
                            END


                                        ";
                        }
                        else if (Tools.ClientCode == "02")
                        {
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
                            + '|' + case when A.BitRolloverInterest = 1 then '2' else '' end-- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.BitRolloverInterest = 1 then '0' else case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount + A.InterestAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 1  then cast(isnull(A.AmountTrf,0) as nvarchar) else '0' end end end-- 26.Amount to be Transfer
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
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                            case when A.DoneAmount = I.Balance then cast(isnull(A.DoneAmount,0) as decimal(30,2)) else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,I.AcqDate PrevDate,
                            round(A.DoneAmount * (A.BreakInterestPercent/100)/case when G.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,A.BitRolloverInterest
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
                            " + _paramSettlementPK + @"
                            and A.statusdealing = 2

                            Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                            A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                            A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,I.AcqDate,G.InterestDaysType,A.BitRolloverInterest,G.Email1

                                " + _mature + @"

                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.PrevDate,A.BitRolloverInterest
                            order by A.ValueDate Asc
                            select * from #text 
                            END


                                        ";
                        }
                        else if (Tools.ClientCode == "03")
                        {
                            cmd.CommandText = @"
                            Declare @TrailsPK int
                            Declare @MaxDateEndDayFP datetime

                            select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ValueDate   --and FundPK = @FundPK
                            )
                            and status = 2   --and FundPK = @FundPK

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
                            + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),0)as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                            + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),0)as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) end -- 12.Investment.ValueDate
                            + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                            else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                            + '|' + case when A.TrxType in (1,3)  then case when isnull(A.InterestPaymentType,'') in (1,4) then '1' 
                            else case when isnull(A.InterestPaymentType,'') = 7 then '4'
                            else case when isnull(A.InterestPaymentType,'') in (10,13) then '5'
                            else case when isnull(A.InterestPaymentType,'') = 16 then '6'
                            else case when isnull(A.InterestPaymentType,'') = 19 then '7' end end end end end
                            else '' end -- 14.Investment.InterestPaymentType              
                            + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                            + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),'')))) else '' end -- 17.WithdrawalDate
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(SUM(ISNULL(A.TradeAmount * BreakInterestPercent / 100 / CASE WHEN InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  * DATEDIFF(day,A.AcqDate,A.SettlementDate) as decimal(30,2)),'')as nvarchar)  else '' end -- 20.Withdrawal Interest
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(SUM(ISNULL(A.TradeAmount * BreakInterestPercent / 100 / CASE WHEN InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  * DATEDIFF(day,A.AcqDate,A.SettlementDate) + A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 21.Total Withdrawal Amount
                            + '|' + case when A.BitRolloverInterest = 1 then '2' else '' end-- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.BitRolloverInterest = 1 then '0' else case when A.Mature = 1 then cast(isnull(cast((SUM(ISNULL(A.TradeAmount * BreakInterestPercent / 100 / CASE WHEN InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  * DATEDIFF(day,A.AcqDate,A.SettlementDate) + A.TradeAmount )* -1 as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 2  then cast(isnull(cast((SUM(ISNULL(A.TradeAmount * A.BreakInterestPercent / 100 / CASE WHEN InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  * DATEDIFF(day,A.AcqDate,A.SettlementDate) + A.TradeAmount )* -1 as decimal(30,2)),'')as nvarchar) else cast(isnull(cast(A.AmountTrf as decimal(30,2)),0) as nvarchar) end end end-- 26.Amount to be Transfer
                            + '|' + -- 27.Statutory Type
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ContactPerson,'')))) -- 28.BankBranch.ContactPerson
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Phone1,'')))) -- 29.BankBranch.Phone1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Fax1,'')))) -- 30.BankBranch.Fax1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                            + '|' + case when A.TrxType in (2,3) then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ParentReference,''))))  else '' end -- 32.Investment.Reference
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                            + '|' + '' 
                            from (      
                            select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                            A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                            A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                            case when A.DoneAmount = I.Balance then 0 else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,
                            round(A.DoneAmount * (A.BreakInterestPercent/100)/case when G.InterestDaysType in (2) then DATEDIFF(DAY, K.DateFrom, K.DateTo) + 1 else case when G.InterestDaysType in (4) then 365 else 360 end end  * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,0 Mature,J.Reference ParentReference,G.InterestDaysType,A.BitRolloverInterest
                            from investment A
                            left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                            left join Fund C on A.fundpk = C.fundpk and C.status = 2
                            left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                            left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'   
                            left join Bank F on A.BankPK = F.BankPK and F.status = 2
                            left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                            left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                            left join FundPosition I on A.TrxBuy = I.[Identity] and I.status = 2
                            left join Investment J on case when A.TrxType = 3 then I.InstrumentPK  else A.InstrumentPK end = J.InstrumentPK
                            and case when A.TrxType = 3 then I.FundPK  else A.FundPK end = J.FundPK 
                            and case when A.TrxType = 3 then I.AcqDate  else A.AcqDate end = J.ValueDate 
                            and J.StatusSettlement = 2
                            left join Period K on A.PeriodPK = K.PeriodPK and K.status = 2
                            where    
                            A.ValueDate =  @ValueDate and A.InstrumentTypePK in (5)
                            " + _paramSettlementPK + @"
                            and A.statusdealing = 2

                            Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                            A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                            A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,J.Reference,G.InterestDaysType,K.DateFrom,K.DateTo,A.BitRolloverInterest


                                " + _mature + @"

                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.Mature ,A.ParentReference ,A.BitRolloverInterest
                            order by A.ValueDate Asc
                            select * from #text 
                            END


                                        ";
                        }
                        else if (Tools.ClientCode == "05")
                        {
                            cmd.CommandText = @"
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
                            + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),0)as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end  end -- 10.Investment.DoneAmount
                            + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end 
                            else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),0)as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end  end -- 11.Investment.InterestPercent
                            + '|' + case when A.TrxType in (2,3)  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) end -- 12.Investment.ValueDate
                            + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                            else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                            + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                            + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                            + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                            + '|' + case when A.InvestmentNotes = 'MATURE'  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) else case when A.InvestmentNotes = 'LIQUIDATE'  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),''))))   else ''  end end -- 17.WithdrawalDate
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),0)as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar)  else '' end -- 19.Withdrawal Principle
                            + '|' + case when A.TrxType = 2  then case when A.InterestAmount = 0 then '' else cast(isnull(cast(A.InterestAmount as decimal(30,2)),0)as nvarchar) end else '' end -- 20.Withdrawal Interest
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount + A.InterestAmount as decimal(30,2)),0)as nvarchar) else '' end -- 21.Total Withdrawal Amount
                            + '|' + case when A.TrxType = 3  then case when A.CurrencyPK = 1  then '1' else '2' end  else '' end -- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(((A.TradeAmount + A.InterestAmount) * -1) as decimal(30,2)),'')as nvarchar) else case when A.TrxType in (1)  then cast(isnull(A.AmountTrf,0) as nvarchar) else cast(isnull(cast(A.InterestAmount * -1 as decimal(30,2)),0)as nvarchar) end end-- 26.Amount to be Transfer
                            + '|' + -- 27.Statutory Type
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ContactPerson,'')))) -- 28.BankBranch.ContactPerson
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Phone1,'')))) -- 29.BankBranch.Phone1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Fax1,'')))) -- 30.BankBranch.Fax1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                            + '|' + case when A.TrxType = 2 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ParentReference,'')))) else '' end end -- 32.Investment.Reference
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                            + '|' + '' 
                            from (      
                                select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                                A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, Z.TrxType TransactionType,
                                A.TotalAmount, A.WHTAmount, Z.TrxTypeID InvestmentNotes, Z.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                                A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                                case when A.TrxType = 3 then cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) else cast(isnull(A.DoneAmount,0) as decimal(30,2)) end AmountTrf,case when Z.TrxTypeID = 'MATURE' then A.InterestPercent else A.BreakInterestPercent end BreakInterestPercent,Z.AcqDate,
                                case when Z.TrxTypeID = 'MATURE' then (round(A.DoneAmount * (A.InterestPercent/100)/365 * datediff(day,Z.AcqDate,A.MaturityDate),0)*0.8) 
                                else case when Z.TrxTypeID = 'LIQUIDATE' then (round(A.DoneAmount * (A.BreakInterestPercent/100)/365 * datediff(day,Z.AcqDate,A.SettlementDate),0)*0.8) 
                                else case when Z.TrxTypeID = 'ROLLOVER' then (round(A.DoneAmount * (I.InterestPercent/100)/365 * datediff(day,Z.AcqDate,A.SettlementDate),0)*0.8) 
                                else (round(A.DoneAmount * (A.InterestPercent/100)/365 * datediff(day,Z.AcqDate,A.SettlementDate),0)*0.8) end end end InterestAmount,Z.OldReference ParentReference,C.CurrencyPK
                                from InvestmentMature Z
                                left join investment A on Z.InvestmentPK = A.InvestmentPK and A.StatusSettlement in (1,2)
                                left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                                left join Fund C on A.fundpk = C.fundpk and C.status = 2
                                left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                                left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'   
                                left join Bank F on A.BankPK = F.BankPK and F.status = 2
                                left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                                left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                                left join FundPosition I on A.TrxBuy = I.[Identity] and I.status = 2
                                where selected = 1 and A.InvestmentPK in (select InvestmentPK from Investment where StatusSettlement in (1,2))

                                Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                                A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, Z.TrxType,
                                A.TotalAmount, A.WHTAmount ,Z.TrxTypeID, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                                A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,Z.AcqDate,Z.OldReference,C.CurrencyPK,Z.TransactionType


                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.ParentReference,A.CurrencyPK 
                            order by A.ValueDate Asc
                            select * from #text 
                            END



                                    ";
                        }
                        else if (Tools.ClientCode == "19")
                        {
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
                            + '|' + case when A.BitRolloverInterest = 1 then '2' else '' end-- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.BitRolloverInterest = 1 then '0' else case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount + A.InterestAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 1  then cast(isnull(A.AmountTrf,0) as nvarchar) else '0' end end end-- 26.Amount to be Transfer
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
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                            case when A.DoneAmount = I.Balance then cast(isnull(A.DoneAmount,0) as decimal(30,2)) else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,I.AcqDate PrevDate,
                            round(A.DoneAmount * (A.BreakInterestPercent/100)/365 * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,A.BitRolloverInterest
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
                            " + _paramSettlementPK + @"
                            and A.statusdealing = 2

                            Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                            A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                            A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,I.AcqDate,A.BitRolloverInterest,G.Email1

                                " + _mature + @"

                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.PrevDate ,A.BitRolloverInterest
                            order by A.ValueDate Asc
                            select * from #text 
                            END


                                        ";
                        }
                        else if (Tools.ClientCode == "25")
                        {
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
                                + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end 
                                else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),0)as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                                 + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end 
                                else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),0)as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                                + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else case when A.TrxType = 1 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), PrevDate, 112),'')))) end end -- 12.Investment.ValueDate
                                + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                                else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                                + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                                + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                                + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                                + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),''))))   else ''  end -- 17.WithdrawalDate
                                + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                                + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                                + '|' + case when A.TrxType = 2  then case when A.InterestAmount = 0 then '' else cast(isnull(cast(A.InterestAmount as decimal(30,2)),'')as nvarchar) end else '' end -- 20.Withdrawal Interest
                                + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount + A.InterestAmount as decimal(30,2)),'')as nvarchar) else '' end -- 21.Total Withdrawal Amount
                                + '|' + -- 22.Rollover Type
                                + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                                + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                                + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                                + '|' + case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount + A.InterestAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 1  then cast(isnull(A.AmountTrf,0) as nvarchar) else '0' end end-- 26.Amount to be Transfer
                                + '|' + -- 27.Statutory Type
                                + '|' + isnull(A.ContactPerson,'') -- 28.BankBranch.ContactPerson
                                + '|' + isnull(A.Phone1,'') -- 29.BankBranch.Phone1
                                + '|' + isnull(A.Fax1,'') -- 30.BankBranch.Fax1
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                                + '|' + case when A.TrxType = 2 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else '' end end -- 32.Investment.Reference
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                                + '|' + ''  
                                from (      
                                select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                                A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                                A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                                A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                                case when A.DoneAmount = I.Balance then cast(isnull(A.DoneAmount,0) as decimal(30,2)) else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,I.AcqDate PrevDate,
                                round(A.DoneAmount * (A.BreakInterestPercent/100)/case when G.InterestDaysType in (2,4) then 365 else 360 end * datediff(day,A.AcqDate,A.Valuedate),0) * 0.8 InterestAmount
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
                                " + _paramSettlementPK + @"
                                and A.statusdealing = 2

                                Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                                A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                                A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                                A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                                A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,I.AcqDate,G.InterestDaysType,G.Email1

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
                        }
                        else
                        {
                            cmd.CommandText = @"
                            Declare @TrailsPK int
                            Declare @MaxDateEndDayFP datetime

                            select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ValueDate   --and FundPK = @FundPK
                            )
                            and status = 2   --and FundPK = @FundPK

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
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) end -- 12.Investment.ValueDate
                            + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                            else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                            + '|' + case when A.TrxType in (1,3)  then case when isnull(A.InterestPaymentType,'') in (1,4) then '1' 
                            else case when isnull(A.InterestPaymentType,'') = 7 then '4'
                            else case when isnull(A.InterestPaymentType,'') in (10,13) then '5'
                            else case when isnull(A.InterestPaymentType,'') = 16 then '6'
                            else case when isnull(A.InterestPaymentType,'') = 19 then '7' end end end end end
                            else '' end -- 14.Investment.InterestPaymentType              
                            + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                            + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                            + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),'')))) else '' end -- 17.WithdrawalDate
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                            + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                            + '|' + '' -- 20.Withdrawal Interest
                            + '|' + '' -- 21.Total Withdrawal Amount
                            + '|' + case when A.BitRolloverInterest = 1 then '2' else '' end-- 22.Rollover Type
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                            + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                            + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                            + '|' + case when A.BitRolloverInterest = 1 then '0' else case when A.Mature = 1 then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else cast(isnull(cast(A.AmountTrf as decimal(30,2)),0) as nvarchar) end end end-- 26.Amount to be Transfer
                            + '|' + -- 27.Statutory Type
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ContactPerson,'')))) -- 28.BankBranch.ContactPerson
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Phone1,'')))) -- 29.BankBranch.Phone1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Fax1,'')))) -- 30.BankBranch.Fax1
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                            + '|' + case when A.TrxType in (2,3) then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ParentReference,''))))  else '' end -- 32.Investment.Reference
                            + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                            + '|' + '' 
                            from (      
                            select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                            A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                            A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,case when G.Email1 = '' then G.ContactPerson else G.ContactPerson + '-' + G.Email1 end ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                            case when A.DoneAmount = I.Balance then 0 else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,
                            round(A.DoneAmount * (A.BreakInterestPercent/100)/case when G.InterestDaysType in (2) then DATEDIFF(DAY, K.DateFrom, K.DateTo) + 1 else case when G.InterestDaysType in (4) then 365 else 360 end end  * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,0 Mature,J.Reference ParentReference,A.BitRolloverInterest
                            from investment A
                            left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                            left join Fund C on A.fundpk = C.fundpk and C.status = 2
                            left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                            left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'   
                            left join Bank F on A.BankPK = F.BankPK and F.status = 2
                            left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                            left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                            left join FundPosition I on A.TrxBuy = I.[Identity] and I.status = 2
                            left join Investment J on case when A.TrxType = 3 then I.InstrumentPK  else A.InstrumentPK end = J.InstrumentPK
                            and case when A.TrxType = 3 then I.FundPK  else A.FundPK end = J.FundPK 
                            and case when A.TrxType = 3 then I.AcqDate  else A.AcqDate end = J.ValueDate 
                            and J.StatusSettlement = 2
                            left join Period K on A.PeriodPK = K.PeriodPK and K.status = 2
                            where    
                            A.ValueDate =  @ValueDate and A.InstrumentTypePK in (5)
                            " + _paramSettlementPK + @"
                            and A.statusdealing = 2

                            Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                            A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                            A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                            A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,J.Reference,G.InterestDaysType,K.DateFrom,K.DateTo,A.BitRolloverInterest,G.Email1

                                " + _mature + @"

                            )A    
                            Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                            A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                            A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                            A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.Mature ,A.ParentReference,A.BitRolloverInterest
                            order by A.ValueDate Asc
                            select * from #text 
                            END


                                        ";
                        }

                        cmd.CommandTimeout = 0;
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


		public int EndDayTrails_GenerateFundJournalOnlyWithParamFund(string _usersID, DateTime _valueDate, EndDayTrails _edt)
		{
			try
			{
				DateTime _datetimeNow = DateTime.Now;
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{
						string _paramFund = "";

						if (!_host.findString(_edt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.FundFrom))
						{
							_paramFund = "And A.FundPK in ( " + _edt.FundFrom + " ) ";
						}
						else
						{
							_paramFund = "";
						}

						cmd.CommandTimeout = 0;
						cmd.CommandText = @"
                        Declare @XFundPK int
                        Declare @XFundID nvarchar(50)
                        Declare @TrailsPK int
                        Declare @PeriodPK int

                        select @PeriodPK = PeriodPK from Period where status = 2 and @ValueDate between DateFrom and DateTo

                        DECLARE X CURSOR FOR     

                        select FundPK,ID from Fund A where status in (1,2)  " + _paramFund + @" and (MaturityDate >= @ValueDate or MaturityDate = '01/01/1900')

                        Open X
                        Fetch Next From X
                        Into @XFundPK,@XFundID

                        While @@FETCH_STATUS = 0
                        BEGIN  

                      
                        --VOID EDT FUND JOURNAL ONLY

                        update FundJournal set Posted = 0,Status = 3,VoidUsersID = @UsersID ,VoidTime= @lastUpdate, LastUpdate = @LastUpdate 			
                        where  Type not in (1,11) and Status = 2 and TrxName not in ('SUBSCRIPTION','REDEMPTION','SWITCHING') and PeriodPK = @PeriodPK and TrxNo =
                        (
                        Select EndDayTrailsPK from EndDayTrails where ValueDate = @ValueDate and status = 2 and Notes = 'Fund Journal' and FundPK = @XFundPK
                        )  



                        update EndDayTrails set status = 3,VoidUsersID = @UsersID,VoidTime = @LastUpdate,LastUpdate=@lastUpdate    
                        where ValueDate = @ValueDate and status = 2 and Notes = 'Fund Journal'   and FundPK = @XFundPK
                  

                        if (@ClientCode <> '05')
                        BEGIN
                            update CloseNav set status = 3,VoidUsersID = @UsersID ,VoidTime= @LastUpdate, LastUpdate = @LastUpdate 
                            where Date = @ValueDate and status <> 3  and FundPK = @XFundPK
                        END

                        Fetch next From X   
                        Into @XFundPK,@XFundID                
                        End                  
                        Close X                  
                        Deallocate X 

                        ";
						cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
						cmd.Parameters.AddWithValue("@UsersID", _usersID);
						cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
						cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
						cmd.ExecuteNonQuery();



					}
				}

				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{
						string _paramFund = "";

						if (!_host.findString(_edt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.FundFrom))
						{
							_paramFund = "And A.FundPK in ( " + _edt.FundFrom + " ) ";
						}
						else
						{
							_paramFund = "";
						}

						cmd.CommandTimeout = 0;
						cmd.CommandText = @"
  

--TEMP TABLE & VARIABEL GLOBAL
BEGIN
	declare @KPDTaxPercent int
	select @KPDTaxPercent = KPDTaxExpenseForBond from SettlementSetup where status = 2

	declare @DepositoTaxPercent numeric(20,2)
	set @DepositoTaxPercent = 20

	declare @DepositoARPercent numeric(20,2)
	set @DepositoARPercent = 100 - @DepositoTaxPercent

	Declare @TotalDaysPerYear int
	select @TotalDaysPerYear = datediff(Day,DateFrom,dateadd(year,1,DateFrom)) from Period where @ValueDate between DateFrom and DateTo and status = 2

	Declare @DefaultBankAccountPK int
	set @DefaultBankAccountPK = 3

	DECLARE @FundJournalPKTemp INT
	SET @FundJournalPKTemp = 0

	DECLARE @CorporateActionPKTemp INT
	SET @CorporateActionPKTemp = 0
	
	DECLARE @DateMinOne DATETIME
	SET @DateMinOne = dbo.FWorkingDay(@ValueDate,-1)

	Declare @TotalDaysYesterdayTillValueDate int
	set @TotalDaysYesterdayTillValueDate = DateDiff(day,@DateMinOne,@ValueDate)

	Declare @PeriodPK int                  
	Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo  and status = 2  

	CREATE TABLE #InterestPaymentBondData
		(
			Date datetime,
			FundPK int,
			InstrumentTypePK int,
			InstrumentPK int,
			Balance numeric(19,4),
			InterestPercent numeric(19,8),
			AcqDate datetime,
			MaturityDate datetime,
			InterestDaysType int,
			InterestPaymentType int,
			LastPaymentDate datetime,
			PaymentDate datetime

		)
	CREATE CLUSTERED INDEX indx_InterestPaymentBond ON #InterestPaymentBondData (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #BondPaymentDate
		(
			InstrumentPK int,
			LastPaymentDate datetime,
			PaymentDate datetime
		)
	CREATE CLUSTERED INDEX indx_BondPaymentDate ON #BondPaymentDate (InstrumentPK);

	CREATE TABLE #InterestPaymentBondDivDays
		(
			FundPK int,	
			InstrumentPK int,
			AcqDate datetime,
			DivDays int,
			DivPayment int,
			CountDays int
		)
	CREATE CLUSTERED INDEX indx_InterestPaymentBondDivDays ON #InterestPaymentBondDivDays (FundPK,InstrumentPK,AcqDate);


	CREATE TABLE #TaxInterestPaymentBondDivDays
		(
			FundPK int,	
			InstrumentPK int,
			AcqDate datetime,
			TaxDays int
		)
	CREATE CLUSTERED INDEX indx_TaxInterestPaymentBondDivDays ON #TaxInterestPaymentBondDivDays (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #InterestPaymentBond
		(
			FundJournalPK int,
			ValueDate datetime,
			InstrumentPK int,
			InstrumentID nvarchar(50),
			FundPK int,
			FundID nvarchar(50),
			InterestAmount numeric(19,4),
			TaxAmount numeric(19,4),
			InterestCalculation int
		)
	CREATE CLUSTERED INDEX indx_InterestPaymentBond ON #InterestPaymentBond (FundJournalPK);

	CREATE TABLE #InterestPaymentBondRoundedCalculation
	(
	FundPK int,
	InstrumentPK int,
	AcqDate datetime,
	GrossInterestAmount numeric(18,4),
	TaxInterestAmount numeric(18,4),
	)
	CREATE CLUSTERED INDEX indx_InterestPaymentBondRoundedCalculation ON #InterestPaymentBondRoundedCalculation (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #MatureBondData
		(
			Date datetime,
			FundPK int,
			InstrumentTypePK int,
			InstrumentPK int,
			Balance numeric(19,4),
			CostValue numeric(19,4),
			InterestPercent numeric(19,8),
			AcqDate datetime,
			MaturityDate datetime,
			InterestDaysType int,
			InterestPaymentType int,
			LastPaymentDate datetime,

		)
	CREATE CLUSTERED INDEX indx_MatureBondData ON #MatureBondData (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #BondMatureDate
		(
			InstrumentPK int,
			LastPaymentDate datetime
		)
	CREATE CLUSTERED INDEX indx_BondPaymentDate ON #BondMatureDate (InstrumentPK);

	CREATE TABLE #MatureBondDivDays
		(
			FundPK int,	
			InstrumentPK int,
			AcqDate datetime,
			DivDays int,
			DivPayment int,
			CountDays int
		)
	CREATE CLUSTERED INDEX indx_MatureBondDivDays ON #MatureBondDivDays (FundPK,InstrumentPK,AcqDate);


	CREATE TABLE #TaxMatureBondDivDays
		(
			FundPK int,	
			InstrumentPK int,
			AcqDate datetime,
			TaxDays int
		)
	CREATE CLUSTERED INDEX indx_TaxMatureBondDivDays ON #TaxMatureBondDivDays (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #MatureBond
		(
			FundJournalPK int,
			ValueDate datetime,
			InstrumentPK int,
			InstrumentID nvarchar(50),
			FundPK int,
			FundID nvarchar(50),
			Balance numeric(19,4),
			RealiseAmount numeric(19,4),
			InterestAmount numeric(19,4),
			TaxAmount numeric(19,4),
			InterestCalculation int,
			CapitalGainAmount numeric(19,4)
		)
	CREATE CLUSTERED INDEX indx_MatureBond ON #MatureBond (FundJournalPK);

	CREATE TABLE #MatureBondRoundedCalculation
		(
			FundPK int,
			InstrumentPK int,
			AcqDate datetime,
			GrossInterestAmount numeric(18,4),
			TaxInterestAmount numeric(18,4),
		)
	CREATE CLUSTERED INDEX indx_MatureBondRoundedCalculation ON #MatureBondRoundedCalculation (FundPK,InstrumentPK,AcqDate);

	Create Table #Exercise               
	(                
		FundJournalPK int,
		FundPK int,
		FundID nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),  
		InstrumentRightsPK int,
		EquityAmount numeric(19,4),
		CashAmount numeric(19,4),
		RightsWarrantAmount numeric(19,4)
	)   

	CREATE CLUSTERED INDEX indx_Exercise ON #Exercise (InstrumentPK,FundPK);

	CREATE TABLE #FPDeposito
	(
		FundJournalPK int,
		ValueDate date,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		DoneAmount numeric (32,8),
		DoneVolume numeric(19,4),
		InterestPaymentType INT,
		InterestDaysType INT,
		MaturityDate date,
		InterestPercent numeric(18,4),
		PaymentDate date,
		PaymentModeOnMaturity int,
		AcqDate date,
	)
	
	CREATE CLUSTERED INDEX indx_FPDeposito ON #FPDeposito (FundJournalPK);

	CREATE TABLE #InterestDailyDeposito
	(
		FundPK int,
		InstrumentPK int,
		MaturityDate date,
		AcqDate date,
		IncomeDepositoAmount numeric(32,8),
		ARInterestDepositoAmount numeric(32,8),
		TaxDepositoAmount numeric(32,8)
	)
	CREATE CLUSTERED INDEX indx_InterestDailyDeposito ON #InterestDailyDeposito (FundPK,InstrumentPK);

	CREATE TABLE #PaymentDeposito
	(
		FundJournalPK int,
		ValueDate date,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		DoneAmount numeric (32,8),
		DoneVolume numeric(19,4),
		InterestPaymentType INT,
		InterestDaysType INT,
		MaturityDate date,
		InterestPercent numeric(18,4),
		PaymentDate date,
		PaymentModeOnMaturity int,
		AcqDate date
	)
	CREATE CLUSTERED INDEX indx_PaymentDeposito ON #PaymentDeposito (FundJournalPK);

	CREATE TABLE #InterestPaymentDeposito
	(
		FundPK int,
		InstrumentPK int,
		MaturityDate date,
		AcqDate date,
		IncomeDepositoAmount numeric(32,8),
		ARInterestDepositoAmount numeric(32,8),
		TaxDepositoAmount numeric(32,8)
	)
	CREATE CLUSTERED INDEX indx_InterestPaymentDeposito ON #InterestPaymentDeposito (FundPK,InstrumentPK);

	CREATE TABLE #MaturedDeposito
	(
		FundJournalPK int,
		ValueDate date,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		DoneVolume numeric(19,4),
	)
	CREATE CLUSTERED INDEX indx_MaturedDeposito ON #MaturedDeposito (FundJournalPK);


	CREATE TABLE #InterestDeposito
	(
		FundPK int,
		InstrumentPK int,
		BreakInterestPercent numeric(18,4),
		MaturityDate date,
		AcqDate date,
		IncomeDepositoAmount numeric(32,8),
		ARInterestDepositoAmount numeric(32,8),
		TaxDepositoAmount numeric(32,8),
		NewInterestAmount numeric(32,8),
		NewIncomeAmount numeric(32,8),
		NewTaxAmount numeric(32,8)
	)
	CREATE CLUSTERED INDEX indx_InterestDeposito ON #InterestDeposito (FundPK,InstrumentPK);


	Create Table #DividenCashSaham               
	(                
		FundJournalPK int,
		FundPK int,
		FundID nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),              
		LastVolume numeric(18,4),
		PaymentDate datetime,
		RecordingDate datetime,
		Earn numeric(22,4),
		Hold numeric(22,4),
		TaxDueDate int,
		FundTypeInternal int
	)   

	CREATE CLUSTERED INDEX indx_DividenCashSaham ON #DividenCashSaham (FundJournalPK,InstrumentPK,FundPK);



	CREATE TABLE #PendingSubscription
	(
		FundJournalPK int,
		ValueDate datetime,
		ClientSubscriptionPK int,
		FundClientPK int,
		FundClientName nvarchar(200),
		FundPK int,
		FundID nvarchar(100),
		FundCashRefPK int,
		CashAmount numeric(19,6),
		SubscriptionFeeAmount numeric(19,6),
		TotalCashAmount numeric(19,6),
		BitFirstNav bit,
		BitPendingSubscription bit
	)

	CREATE CLUSTERED INDEX indx_PendingSubscription ON #PendingSubscription (FundJournalPK,FundClientPK,FundPK);


	CREATE TABLE #LastFundFeeDistinct
	(
		Date datetime,
		FundPK int,
		BitPendingSubscription bit
	)
	
	CREATE CLUSTERED INDEX indx_LastFundFeeDistinct ON #LastFundFeeDistinct (FundPK);
	
	CREATE TABLE #InterestBondData
		(
			Date datetime,
			FundPK int,
			InstrumentTypePK int,
			InstrumentPK int,
			Balance numeric(19,4),
			InterestPercent numeric(19,8),
			AcqDate datetime,
			MaturityDate datetime,
			InterestDaysType int,
			InterestPaymentType int

		)
	CREATE CLUSTERED INDEX indx_InterestBond ON #InterestBondData (FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #InterestBondDivDays
		(
			FundPK int,	
			InstrumentPK int,
			AcqDate datetime,
			DivDays int,
			DivPayment int,
			CountDays int
		)
	CREATE CLUSTERED INDEX indx_InterestBondDivDays ON #InterestBondDivDays (FundPK,InstrumentPK,AcqDate);


	CREATE TABLE #TempInterestBond
	(
	Date datetime,
	FundPK int,
	InstrumentPK int,
	AcqDate datetime,
	InterestAmount numeric(22,4),
	TaxAmount numeric(22,4),
	FinalAmount numeric(22,4)
	)
	CREATE CLUSTERED INDEX indx_TempInterestBond ON #TempInterestBond (Date,FundPK,InstrumentPK,AcqDate);

	CREATE TABLE #TempInterestBondTotal
	(
	Date datetime,
	FundPK int,
	InstrumentPK int,
	AcqDate datetime,
	InterestAmount numeric(22,4),
	TaxAmount numeric(22,4),
	FinalAmount numeric(22,4)
	)
	CREATE CLUSTERED INDEX indx_TempInterestBondTotal ON #TempInterestBondTotal (Date,FundPK,InstrumentPK,AcqDate);


	CREATE TABLE #InterestBond
	(
			FundJournalPK int,
			ValueDate datetime,
			InstrumentTypePK int,
			InstrumentPK int,
			InstrumentID nvarchar(50),
			FundPK int,
			FundID nvarchar(50),
			InterestAmount numeric(19,4),
			TaxPercent int,
			AccruedInterestCalculation int,
			FinalAmount numeric(18,4),
			TaxAmount numeric(18,4),-- ni untuk BCA karena bukan ambil dr %
			InterestPaymentType int,
			SukukType int
	)
	CREATE CLUSTERED INDEX indx_InterestBond ON #InterestBond (FundJournalPK);


	CREATE TABLE #FundPositionRevalForJournal
	(
		FundJournalPK int,
		FundPK int,
		InstrumentPK int,
		InstrumentID nvarchar(200),
		InstrumentTypePK int,
		ReksadanaTypePK int,
		FinalAmount numeric(22,4)
	)
	CREATE CLUSTERED INDEX indx_FundPositionRevalForJournal ON #FundPositionRevalForJournal (FundPK,InstrumentPK);

	CREATE TABLE #RevalFundPositionDistinct
	(
		FundPK int,
		InstrumentPK int,
		InstrumentID nvarchar(200),
		InstrumentTypePK int,
		ReksadanaTypePK int
	)
	CREATE CLUSTERED INDEX indx_RevalFundPositionDistinct ON #RevalFundPositionDistinct (FundPK,InstrumentPK);

	CREATE TABLE #SellReksadanaDistinct
	(
		FundPK int,
		InstrumentPK int
	)
	CREATE CLUSTERED INDEX indx_SellReksadanaDistinct ON #SellReksadanaDistinct (FundPK,InstrumentPK);

	CREATE TABLE #AvgReksadana
	(
		FundPK int,
		InstrumentPK int,
		Date datetime,
		AvgCost numeric(24,8)
	)
	CREATE CLUSTERED INDEX indx_AvgReksadana ON #AvgReksadana (FundPK,InstrumentPK);

	CREATE TABLE #InvBuyReksadana
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		ReksadanaTypePK int
	)
	CREATE CLUSTERED INDEX indx_InvBuyReksadana ON #InvBuyReksadana (FundJournalPK);

	CREATE TABLE #InvSellReksadana
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		ReksadanaTypePK int,
		DoneVolume numeric(19,6) 
	)
	CREATE CLUSTERED INDEX indx_InvSellReksadana ON #InvSellReksadana (FundJournalPK);

	CREATE TABLE #SellBondDistinct
	(
		FundPK int,
		InstrumentPK int
	)
	CREATE CLUSTERED INDEX indx_SellBondDistinct ON #SellBondDistinct (FundPK,InstrumentPK);

	CREATE TABLE #AvgBond
	(
		FundPK int,
		InstrumentPK int,
		Date datetime,
		AvgCost numeric(24,8)
	)
	CREATE CLUSTERED INDEX indx_AvgBond ON #AvgBond (FundPK,InstrumentPK);


	CREATE TABLE #InvBuyBond
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		DoneAccruedInterest numeric(19,6),
		IncomeTaxGainAmount numeric(19,6),
		IncomeTaxInterestAmount numeric(19,6)
	)

	CREATE CLUSTERED INDEX indx_InvBuyBond ON #InvBuyBond (FundJournalPK);

	CREATE TABLE #InvSellBond
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		DoneAccruedInterest numeric(19,6),
		IncomeTaxGainAmount numeric(19,6),
		IncomeTaxInterestAmount numeric(19,6),
		DoneVolume numeric(19,4)
	)
	CREATE CLUSTERED INDEX indx_InvSellBond ON #InvSellBond (FundJournalPK);

	CREATE TABLE #InvDepositoLiquidate
	(
		FundJournalPK int,
		ValueDate date,
		SettlementDate date,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric (32,8),
		DoneVolume numeric(19,4),
		BreakInterestPercent numeric(18,4),
		InterestPaymentType INT,
		InterestDaysType INT,
		MaturityDate date,
		InterestPercent numeric(18,4),
		AcqDate date,
		PaymentDate date,
		PaymentModeOnMaturity int
	)
	
	CREATE CLUSTERED INDEX indx_InvDepositoLiquidate ON #InvDepositoLiquidate (FundJournalPK);


	CREATE TABLE #InvDepositoPlacement
	(
		FundJournalPK int,
		FundCashRefPK int,
		TrxType int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		FundPK int,
		DoneAmount numeric(19,4)
	)

	CREATE CLUSTERED INDEX indx_InvDepositoPlacement ON #InvDepositoPlacement (FundJournalPK);

	CREATE TABLE #InterestGiroForJournal
	(
		FundJournalPK int,
		FundPK int,
		FundID nvarchar(300),
		InterestAmount numeric(19,4)
	)
	CREATE CLUSTERED INDEX indx_InterestGiroForJournal ON #InterestGiroForJournal (FundJournalPK);

	CREATE TABLE #LastBankInterestSetup
	(
		Date datetime,
		BankBranchPK int,
		InterestPercent numeric(18,8),
		InterestDays int,
		MinimumBalance numeric(19,4)
	)
	CREATE CLUSTERED INDEX indx_LastBankInterestSetup ON #LastBankInterestSetup (Date,BankBranchPK);

	CREATE TABLE #LastBankInterestSetupDistinct
	(
		Date datetime,
		BankBranchPK int
	)
	CREATE CLUSTERED INDEX indx_LastBankInterestSetupDistinct ON #LastBankInterestSetupDistinct (Date,BankBranchPK);


	CREATE TABLE #DailyFeeForJournal
	(
		FundJournalPK int,
		FundPK int,
		FundID nvarchar(300),
		MFeeAmount numeric(19,4),
		CustodiFeeAmount numeric(19,4),
		AuditFeeAmount numeric(19,4),
		SInvestFeeAmount numeric(19,4),
		MovementFeeAmount numeric(19,4),
		OtherFeeOneAmount numeric(19,4),
		OtherFeeTwoAmount numeric(19,4),
		CBESTAmount numeric(19,4)
	)
	CREATE CLUSTERED INDEX indx_DailyFeeForJournal ON #DailyFeeForJournal (FundJournalPK,FundPK);

	CREATE TABLE #CBESTTransaction
	(
		FundPK int,
		TrxEquity int,
		TrxCorpBond int,
		TrxGovBond int
	)
	CREATE CLUSTERED INDEX indx_CBESTTransaction ON #CBESTTransaction (FundPK);

	CREATE TABLE #LastAUMYesterdayDistinct
	(
		Date datetime,
		FundPK int
	)
	CREATE CLUSTERED INDEX indx_LastAUMYesterdayDistinct ON #LastAUMYesterdayDistinct (Date,FundPK);

	CREATE TABLE #LastAUMYesterday
	(
		Date datetime,
		FundPK int,
		AUM numeric(24,4)
		
	)
	CREATE CLUSTERED INDEX indx_LastAUM ON #LastAUMYesterday (Date,FundPK);



	CREATE TABLE #LastFundFeeForDailyFeeDistinct
	(
		Date datetime,
		FundPK int
	)
	CREATE CLUSTERED INDEX indx_LastFundFeeForDailyFeeDistinct ON #LastFundFeeForDailyFeeDistinct (Date,FundPK);


	CREATE TABLE #AccruedInterestCalculation
	(
		FundPK int,
		AccruedInterestCalculation int
	)
	CREATE CLUSTERED INDEX indx_AccruedInterestCalculation ON #AccruedInterestCalculation (FundPK,AccruedInterestCalculation);

	
	CREATE TABLE #FundFeeHeader
	(
		Date datetime,
		FundJournalPK int,
		FundPK int,
		FundID nvarchar(300),
		FundType int,
		RoundingMode int,
		DecimalPlaces int,
		BitSinvestFee bit,
		AuditFeeAmount numeric(19,4),
		AuditFeeDays int,
		SInvestFeeDays int,
		SinvestFeePercent numeric(18,8),
		MovementFeeAmount numeric(19,4),
		OtherFeeOneAmount numeric(19,4),
		OtherFeeTwoAmount numeric(19,4),
		CBESTEquityAmount numeric(19,4),
		CBESTCorpBondAmount numeric(19,4),
		CBESTGovBondAmount  numeric(19,4)
	
	)
	CREATE CLUSTERED INDEX indx_FundFeeHeader ON #FundFeeHeader (FundJournalPK,FundPK);


	CREATE Table #FundParam 
	(
	FundPK INT,
	ID nvarchar(200),
	Name NVARCHAR(400)
	)
	CREATE CLUSTERED INDEX indx_FundParam ON #FundParam (FundPK);

	CREATE TABLE #DataRevalSementara
	(
	FundJournalPK INT,
	AutoNo INT,
	ValueDate DATETIME,
	FundPK INT,
	InstrumentPK INT,
	InstrumentTypePK INT,
	InstrumentID NVARCHAR(50),
	UnRealised NUMERIC(19,4)
	)
	CREATE CLUSTERED INDEX indx_DataRevalSementara ON #DataRevalSementara (FundJournalPK,AutoNo);

	CREATE TABLE #EDT
	(
		EndDayTrailsPK int,
		FundPK int
	)
	CREATE CLUSTERED INDEX indx_EDT ON #EDT (EndDayTrailsPK,FundPK);

	CREATE TABLE #JournalHeader 
	(
		FundJournalPK INT,
		HistoryPK INT,
		Status INT,
		Notes NVARCHAR(1000),
		PeriodPK INT,
		ValueDate DATETIME,
		Type nvarchar(50),
		TrxNo INT,
		TrxName nvarchar(150),
		Reference nvarchar(75),
		Description nvarchar(1500)
	)

	CREATE CLUSTERED INDEX indx_JournalHeader ON #JournalHeader (FundJournalPK);

	CREATE TABLE #JournalDetail 
	(
		FundJournalPK INT,
		AutoNo INT,
		HistoryPK BIGINT,
		Status INT,
		FundJournalAccountPK  INT,
		CurrencyPK INT,
		FundPK INT,
		FundClientPK INT,
		InstrumentPK INT,
		DetailDescription nvarchar(1500),
		DebitCredit char(1),
		Amount NUMERIC(19,4),
		Debit NUMERIC(19,4),
		Credit NUMERIC(19,4),
		CurrencyRate NUMERIC(19,4),
		BaseDebit NUMERIC(19,4),
		BaseCredit NUMERIC(19,4)
	)

	CREATE CLUSTERED INDEX indx_JournalDetail ON #JournalDetail (FundJournalPK,AutoNo);

	CREATE TABLE #InvBuyEquity
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		InstrumentTypePK int,
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		CommissionAmount numeric(19,4),
		LevyAmount numeric(19,4),
		VATAmount numeric(19,4),
		OTCAmount numeric(19,4),
		WHTAmount numeric(19,4),
		KPEIAmount numeric(19,4)
	)

	CREATE TABLE #InvSellEquity
	(
		FundJournalPK int,
		ValueDate datetime,
		SettlementDate Datetime,
		Reference nvarchar(100),
		InstrumentPK int,
		InstrumentID nvarchar(100),
		InstrumentTypePK int,
		FundPK int,
		FundCashRefPK int,
		DoneAmount numeric(19,6),
		CommissionAmount numeric(19,4),
		LevyAmount numeric(19,4),
		VATAmount numeric(19,4),
		OTCAmount numeric(19,4),
		WHTAmount numeric(19,4),
		KPEIAmount numeric(19,4),
		IncomeTaxSellAmount numeric(19,4),
		DoneVolume numeric(19,4),
		BitExercise bit,
		InvestmentPK int
	)

	CREATE CLUSTERED INDEX indx_InvSellEquity ON #InvSellEquity (FundJournalPK);


	CREATE TABLE #SellEquityDistinct
	(
		FundPK int,
		InstrumentPK int,
		BitExercise bit,
		InvestmentPK int
	)
	CREATE CLUSTERED INDEX indx_SellEquityDistinct ON #SellEquityDistinct (FundPK,InstrumentPK,BitExercise,InvestmentPK);

	CREATE TABLE #AvgEquity
	(
		InvestmentPK int,
		FundPK int,
		InstrumentPK int,
		Date datetime,
		AvgCost numeric(24,8)
	)
	CREATE CLUSTERED INDEX indx_AvgEquity ON #AvgEquity (InvestmentPK,FundPK,InstrumentPK);


    Create Table #FundPositionAdjustment               
    (                
	    FundJournalPK int,
	    FundPK int,
	    FundID nvarchar(100),
	    InstrumentPK int,
	    InstrumentID nvarchar(100),  
	    InstrumentTypePK int,
	    InvestmentAmount numeric(19,4),
	    RevalAmount numeric(19,4),
	    TrxType int
    )   

    CREATE CLUSTERED INDEX indx_FundPositionAdjustment ON #FundPositionAdjustment (InstrumentPK,FundPK);


	
END

--SET PARAM FUND
BEGIN
	INSERT INTO #FundParam
	SELECT FundPK,ID,A.Name FROM Fund A where status in (1,2)   " + _paramFund + @" 
	AND (MaturityDate >= @ValueDate or MaturityDate = '01/01/1900')     
	--and ID not in ('NAVCOUS','NAVCP3','XCID0000','CIPTUS00')
END

--INSERT INTO EDT 
BEGIN
	DECLARE @maxEndDayTrailsPK INT

	SET @maxEndDayTrailsPK = 1

	SELECT TOP 1 @maxEndDayTrailsPK = EndDayTrailsPK + 1  FROM dbo.EndDayTrails
	ORDER BY EndDayTrailsPK DESC

	SET @maxEndDayTrailsPK = ISNULL(@maxEndDayTrailsPK,1)
	

	INSERT INTO #EDT
			( EndDayTrailsPK, FundPK )
	Select @maxEndDayTrailsPK + ROW_NUMBER() OVER	(ORDER BY FundPK ASC),FundPK
	FROM #FundParam

	Insert into EndDayTrails(EndDayTrailsPK,HistoryPK,Status,Notes,ValueDate,FundPK,BitValidate,LogMessages
	,EntryUsersID,EntryTime,LastUpdate)                    
	Select @maxEndDayTrailsPK + ROW_NUMBER() OVER	(ORDER BY FundPK ASC),
	1,2,'Fund Journal',@ValueDate,FundPK,1,ID + ' - ' + Name,@UsersID,@LastUpdate,@LastUpdate 
	FROM #FundParam
END

-- VALIDASI DATA AWAL SEBELOM PROSES JALAN
BEGIN
	DECLARE @Msg NVARCHAR(MAX)
	DECLARE @ReasonValidation NVARCHAR(1000)
	SET @Msg = ''
	SET @ReasonValidation = ''

	INSERT INTO #LastFundFeeForDailyFeeDistinct
	Select  max(date) Date,FundPK From FundFee 
	Where status = 2 and Date <= @valuedate and FundPK in (select FundPK from #FundParam) 
	group by FundPK


	INSERT INTO #AccruedInterestCalculation
	select A.FundPK,isnull(A.AccruedInterestCalculation,1) from FundFee A
	left join #LastFundFeeForDailyFeeDistinct B on A.FundPK = B.FundPK and A.Date = B.Date
	where status = 2 and A.FundPK in (select FundPK from #FundParam)  and B.Date is not null


	--. CEK ADA FUND YANG TIDAK ADA FUND FEE
	BEGIN
			INSERT INTO #FundFeeHeader
				( 
				  Date,
				  FundJournalPK,
				  FundPK ,
				  FundID,
				  FundType,
				  RoundingMode,
				  DecimalPlaces,
				  BitSinvestFee,
				  AuditFeeAmount ,
				  AuditFeeDays,
				  SInvestFeeDays,
				  SinvestFeePercent,
				  MovementFeeAmount,
				  OtherFeeOneAmount,
				  OtherFeeTwoAmount,
				  CBESTEquityAmount,
				  CBESTCorpBondAmount,
				  CBESTGovBondAmount 
				)
		Select max(A.Date)
		,ROW_NUMBER() OVER (ORDER BY A.FundPK ASC)
		,A.FundPK
		,B.ID
		,B.Type
		,D.JournalRoundingMode
		,D.JournalDecimalPlaces
		,isnull(B.BitSinvestFee,1)
		,case when H.Nav is null then 0 else AuditFeeAmount end --tambahan disini boris
		,Case When isnull(BitActDivDays,0) = 0 then AuditFeeDays else @TotalDaysPerYear END 
		,Case When isnull(BitActDivDays,0) = 0 then  isnull(E.SinvestFeeDays,F.SinvestFeeDays) else @TotalDaysPerYear END 
		,Case when B.Type in (1,3,4,6,8,11,12) then isnull(E.SinvestMoneyMarketFeePercent,F.SinvestMoneyMarketFeePercent)
					When B.Type in (2,7,9) then isnull(E.SinvestBondFeePercent,F.SinvestBondFeePercent)
						Else isnull(E.SinvestEquityFeePercent,F.SinvestEquityFeePercent) END
		,MovementFeeAmount,OtherFeeOneAmount,OtherFeeTwoAmount
		,CBESTEquityAmount,CBESTCorpBondAmount,CBESTGovBondAmount 
		From FundFee A
		Left join Fund B on A.FundPK = B.FundPK and B.status  in (1,2)
		left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status  in (1,2)
		left join Bank D on C.BankPK = D.BankPK and D.status  in (1,2)
		Left join SInvestSetup E on A.FundPK = E.FundPK and E.Status in (1,2)
		Left join SInvestSetup F on F.FundPK = 0 and F.Status in (1,2)
		left join #LastFundFeeForDailyFeeDistinct G on A.FundPK = G.FundPK 
		left join CloseNAV H on A.FundPK = H.FundPK and H.Date = @DateMinOne and H.status = 2 --tambahan disini boris
		Where A.status = 2 and A.FundPK in (select FundPK from #FundParam) and A.Date = G.Date
		Group By A.FundPK	
		,AuditFeeAmount,AuditFeeDays,BitActDivDays,D.JournalRoundingMode,D.JournalDecimalPlaces,B.BitSinvestFee,E.SinvestFeeDays,F.SinvestFeeDays	
		,B.Type,E.SinvestEquityFeePercent,F.SinvestEquityFeePercent,E.SinvestMoneyMarketFeePercent,F.SinvestMoneyMarketFeePercent,	
		E.SinvestBondFeePercent,F.SinvestBondFeePercent,MovementFeeAmount,OtherFeeOneAmount,OtherFeeTwoAmount,B.ID,CBESTEquityAmount,CBESTCorpBondAmount,CBESTGovBondAmount,H.AUM,H.Nav



		--IF EXISTS( 
--			SELECT A.FundPK FROM #FundParam A
--			LEFT JOIN #FundFeeHeader B ON A.FundPK = B.FundPK 
--			WHERE B.FundPK IS null
--		)
--		BEGIN
--			SELECT @ReasonValidation = @ReasonValidation + ' ,' + A.ID  FROM #FundParam A
--			LEFT JOIN #FundFeeHeader B ON A.FundPK = B.FundPK 
--			WHERE B.FundPK IS null
            
--			SELECT @Msg = 'This Fund: ' + @ReasonValidation + ' do not have data in fund Fee, Generate EDT Journal Only param Fund Cancel' 

--			UPDATE B set B.LogMessages = B.LogMessages + ' ||| ' + @Msg,B.BitValidate = 0,B.Status = 3,B.VoidUsersID = @UsersID,B.VoidTime = @Lastupdate 
--			FROM #EDT A
--			LEFT JOIN dbo.EndDayTrails B ON A.EndDayTrailsPK = B.EndDayTrailsPK

--			--SELECT @Msg LastPK
--			RETURN
--		END
	END


	----. CEK ADA FUND YANG FUNDACCOUNTING SETUPNYA LEBIH DARI 1 BARIS APA ENGGA
--	BEGIN
--		IF EXISTS( 
--			SELECT A.FundPK,COUNT(A.FundAccountingSetupPK) Total FROM FundAccountingSetup A
--			LEFT JOIN #FundParam B ON A.FundPK = B.FundPK 
--			WHERE A.Status in (1,2)
--			GROUP BY A.FundPK
--			HAVING COUNT(A.FundAccountingSetupPK) > 1
--		)
--		BEGIN 
--			SELECT @ReasonValidation = @ReasonValidation + ' ,' + B.ID  FROM FundAccountingSetup A
--			LEFT JOIN #FundParam B ON A.FundPK = B.FundPK 
--			WHERE A.Status = 2
--			GROUP BY B.ID
--			HAVING COUNT(A.FundAccountingSetupPK) > 1

--			SELECT @Msg = 'This Fund: ' + @ReasonValidation + ' Have more than one setup in fund accounting setup , Generate EDT Journal Only param Fund Cancel' 
			
--			UPDATE B set B.LogMessages = B.LogMessages + ' ||| ' + @Msg,B.BitValidate = 0,B.Status = 3,B.VoidUsersID = @UsersID,B.VoidTime = @Lastupdate 
--			FROM #EDT A
--			LEFT JOIN dbo.EndDayTrails B ON A.EndDayTrailsPK = B.EndDayTrailsPK

--			--SELECT @Msg LastPK
--			RETURN
--		END
--	END


--	--. CEK ADA FUND YANG TIDAK ADA DI FUND ACCOUNTING SETUP
--	BEGIN
--		IF EXISTS( 
--			SELECT A.FundPK FROM #FundParam A
--			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status in (2)
--			WHERE B.FundPK IS null
--		)
--		BEGIN
--			SELECT @ReasonValidation = @ReasonValidation + ' ,' + A.ID  FROM #FundParam A
--			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
--			WHERE B.FundPK IS NULL
            
--			SELECT @Msg = 'This Fund: ' + @ReasonValidation + ' do not have data in fund accounting setup Approved status , Generate EDT Journal Only param Fund Cancel' 

--			UPDATE B set B.LogMessages = B.LogMessages + ' ||| ' + @Msg,B.BitValidate = 0,B.Status = 3,B.VoidUsersID = @UsersID,B.VoidTime = @Lastupdate 
--			FROM #EDT A
--			LEFT JOIN dbo.EndDayTrails B ON A.EndDayTrailsPK = B.EndDayTrailsPK

--			--SELECT @Msg LastPK
--			RETURN
--		END
--	END

	

	
END

--1.REVAL SEBELUM ADA NILAI TUTUP HARI INI
BEGIN
	IF NOT Exists(              
	Select * from ClosePrice where Status = 2 and date = @ValueDate              
	)  
	BEGIN
	
		INSERT INTO #DataRevalSementara
				( FundJournalPK ,
				  AutoNo ,
				  ValueDate ,
				  FundPK ,
				  InstrumentPK ,
				  InstrumentTypePK ,
				  InstrumentID ,
				  UnRealised
				)
		SELECT 0,0,ValueDate,A.FundPK,A.InstrumentPK,A.InstrumentTypePK,C.ID InstrumentID,sum(DoneVolume *  ((AvgPrice - ClosePrice)/100))  Unrealised 
				from Investment A
				left join FundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK and B.status = 2 and Date = @DateMinOne
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)       
				where StatusSettlement = 2 and ValueDate = @ValueDate and A.InstrumentTypePK in (1,4,16)
				AND A.FundPK IN(
					SELECT FundPK FROM #FundParam
				)
				group by ValueDate,A.FundPK,A.InstrumentPK,A.InstrumentTypePK,C.ID
				

		INSERT INTO #DataRevalSementara
				( FundJournalPK ,
				  AutoNo ,
				  ValueDate ,
				  FundPK ,
				  InstrumentPK ,
				  InstrumentTypePK ,
				  InstrumentID ,
				  UnRealised
				)
				select 0,0,ValueDate,A.FundPK,A.InstrumentPK,A.InstrumentTypePK,C.ID InstrumentID,sum(DoneVolume *  ((AvgPrice - ClosePrice)/100)) Unrealised from Investment A
				left join FundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK and A.AcqDate = B.AcqDate and B.status = 2 and Date = @DateMinOne
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)       
				where StatusSettlement = 2  and ValueDate = @ValueDate and A.InstrumentTypePK not in (1,4,5,16)
				AND A.FundPK IN(
					SELECT FundPK FROM #FundParam
				)
				group by ValueDate,A.FundPK,A.InstrumentPK,A.InstrumentTypePK,C.ID


		UPDATE A SET A.FundJournalPK = B.FundJournalPK,A.AutoNo = 1 FROM #DataRevalSementara A
		LEFT JOIN (
				SELECT FundPK,InstrumentPK,ROW_NUMBER() OVER	(ORDER BY FundPK ASC) FundJournalPK  FROM #DataRevalSementara
				GROUP BY FundPK,InstrumentPK
		)B ON A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK

		-- Unrealised <> 0
		BEGIN
			INSERT INTO #JournalHeader
					( FundJournalPK ,
					  HistoryPK ,
					  Status ,
					  Notes ,
					  PeriodPK ,
					  ValueDate ,
					  Type ,
					  TrxNo ,
					  TrxName ,
					  Reference ,
					  Description
					)
			SELECT A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
			,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'PORTFOLIO REVALUATION BEFORE CLOSE PRICE','','INSTRUMENT: ' + A.InstrumentID
			FROM #DataRevalSementara A
			LEFT JOIN #EDT B ON A.FundPK = B.FundPK 
			WHERE ISNULL(A.UnRealised,0) <> 0

			INSERT INTO #JournalDetail
					( FundJournalPK ,
					  AutoNo ,
					  HistoryPK ,
					  Status ,
					  FundJournalAccountPK ,
					  CurrencyPK ,
					  FundPK ,
					  FundClientPK ,
					  InstrumentPK ,
					  DetailDescription ,
					  DebitCredit ,
					  Amount ,
					  Debit ,
					  Credit ,
					  CurrencyRate ,
					  BaseDebit ,
					  BaseCredit
					)
			SELECT A.FundJournalPK + @FundJournalPKTemp,A.AutoNo,1,2
			,Case When ISNULL(A.UnRealised,0) < 0 Then CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN RevaluationBond ELSE RevaluationEquity END
				Else CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN UnrealisedBond ELSE UnrealisedEquity END END
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK,'INSTRUMENT: ' + A.InstrumentID
			,'D'
			,isnull(ABS(A.UnRealised),0)
			,isnull(ABS(A.UnRealised),0),0
			,1
			,isnull(ABS(A.UnRealised),0),0
			FROM #DataRevalSementara A
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.Status = 2
			LEFT JOIN dbo.FundJournalAccount C ON C.FundJournalAccountPK = Case When ISNULL(A.UnRealised,0) < 0 Then CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN RevaluationBond ELSE RevaluationEquity END
				Else CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN UnrealisedBond ELSE UnrealisedEquity END END AND C.status IN (1,2)
			WHERE ISNULL(A.UnRealised,0) <> 0

			INSERT INTO #JournalDetail
					( FundJournalPK ,
					  AutoNo ,
					  HistoryPK ,
					  Status ,
					  FundJournalAccountPK ,
					  CurrencyPK ,
					  FundPK ,
					  FundClientPK ,
					  InstrumentPK ,
					  DetailDescription ,
					  DebitCredit ,
					  Amount ,
					  Debit ,
					  Credit ,
					  CurrencyRate ,
					  BaseDebit ,
					  BaseCredit
					)

			SELECT A.FundJournalPK + @FundJournalPKTemp,A.AutoNo + 1,1,2
			,Case When ISNULL(A.UnRealised,0) < 0 Then CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN UnrealisedBond ELSE UnrealisedEquity END
				ELSE CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN RevaluationBond ELSE RevaluationEquity END END
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK,'INSTRUMENT: ' + A.InstrumentID,'C',isnull(ABS(A.UnRealised),0)
			,0,isnull(ABS(A.UnRealised),0)
			,1
			,0,isnull(ABS(A.UnRealised),0)
			FROM #DataRevalSementara A
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.Status = 2
			LEFT JOIN dbo.FundJournalAccount C ON C.FundJournalAccountPK = Case When ISNULL(A.UnRealised,0) < 0 Then CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN UnrealisedBond ELSE UnrealisedEquity END
				ELSE CASE WHEN A.InstrumentTypePK in (2,3,8,9,11,13,14,15) THEN RevaluationBond ELSE RevaluationEquity END END AND C.status IN (1,2)
			WHERE ISNULL(A.UnRealised,0) <> 0
		END

	END   
END  

--2.BUY EQUITY
BEGIN

	INSERT INTO #InvBuyEquity
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			InstrumentTypePK,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			CommissionAmount ,
			LevyAmount ,
			VATAmount ,
			OTCAmount,
			WHTAmount,
			KPEIAmount
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.InstrumentTypePK,
			A.FundPK ,
			A.FundCashRefPK,
			DoneAmount ,
			CommissionAmount ,
			LevyAmount ,
			VATAmount ,
			OTCAmount,
			WHTAmount,
			KPEIAmount
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 1
	and A.InstrumentTypePK in (1,4,16) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc
	
	-- T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 EQUITY BUY: ' + A.InstrumentID   
		From #InvBuyEquity A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,Case when A.InstrumentTypePK = 1 Then B.InvestmentEquity
				When A.InstrumentTypePK = 4 then B.InvestmentRights
					When A.InstrumentTypePK = 16 then B.InvestmentWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 EQUITY BUY: ' + A.InstrumentID,'D',A.DoneAmount
		,A.DoneAmount,0
		,1
		,A.DoneAmount,0
		From #InvBuyEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.InvestmentEquity
													When A.InstrumentTypePK = 4 then B.InvestmentRights
														When A.InstrumentTypePK = 16 then B.InvestmentWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.WithHoldingTaxPPH23,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'WHT T0 EQUITY BUY: ' + A.InstrumentID,'C',A.WHTAmount
		,0,A.WHTAmount
		,1
		,0,A.WHTAmount
		From #InvBuyEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.WithHoldingTaxPPH23 = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		Where D.WHTDueDate = 1
		and  isnull(A.WHTAmount,0) > 0
    
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)


		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,Case when A.InstrumentTypePK = 1 Then B.PayablePurchaseEquity
				When A.InstrumentTypePK = 4 then B.PayablePurchaseRights
					When A.InstrumentTypePK = 16 then B.PayablePurchaseWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AP T0 EQUITY BUY: ' + A.InstrumentID,'C',A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END
		,0,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END
		,1
		,0,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0)
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END
		From #InvBuyEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.PayablePurchaseEquity
													When A.InstrumentTypePK = 4 then B.PayablePurchaseRights
														When A.InstrumentTypePK = 16 then B.PayablePurchaseWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D on A.FundPK = D.FundPK and D.status in (1,2)
		Where isnull(A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
			,B.BrokerCommission,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'BROKER COMM T0 EQUITY BUY: ' + A.InstrumentID,'D',isnull(A.CommissionAmount,0)
			,isnull(A.CommissionAmount,0),0
			,1
			,isnull(A.CommissionAmount,0),0
			From #InvBuyEquity A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.BrokerCommission = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.CommissionAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
			,B.BrokerLevy,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'LEVY KPEI T0 EQUITY BUY: ' + A.InstrumentID,'D',isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0)
			,isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0),0
			,1
			,isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0),0
			From #InvBuyEquity A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.BrokerLevy = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
			,B.BrokerVat,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'VAT T0 EQUITY BUY: ' + A.InstrumentID,'D',isnull(A.VATAmount,0)
			,isnull(A.VATAmount,0),0
			,1
			,isnull(A.VATAmount,0),0
			From #InvBuyEquity A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.BrokerVat = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.VATAmount,0) > 0
	END

	-- TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-Settled EQUITY BUY: ' + A.InstrumentID   
		From #InvBuyEquity A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)


		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,Case when A.InstrumentTypePK = 1 Then B.PayablePurchaseEquity
				When A.InstrumentTypePK = 4 then B.PayablePurchaseRights
					When A.InstrumentTypePK = 16 then B.PayablePurchaseWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AP T-SETTLED EQUITY BUY: ' + A.InstrumentID,'D',A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END
		,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END ,0
		,1
		,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0)
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END ,0
		From #InvBuyEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.PayablePurchaseEquity
													When A.InstrumentTypePK = 4 then B.PayablePurchaseRights
														When A.InstrumentTypePK = 16 then B.PayablePurchaseWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D on A.FundPK = D.FundPK and D.status in (1,2)
		WHERE isnull(A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- Case when D.WHTDueDate = 1 THEN isnull(A.WHTAmount,0) ELSE 0 END,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)


		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED EQUITY BUY: ' + A.InstrumentID,'C',A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- isnull(A.WHTAmount,0)
		,0,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- isnull(A.WHTAmount,0)
		,1
		,0,A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0)
		- isnull(A.WHTAmount,0)
		From #InvBuyEquity A  
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		Where isnull(A.DoneAmount + ISNULL(A.CommissionAmount,0) + ISNULL(A.LevyAmount,0) + ISNULL(A.KPEIAmount,0) + ISNULL(A.VATAmount,0) - ISNULL(A.OTCAmount,0) 
		- isnull(A.WHTAmount,0),0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.WithHoldingTaxPPH23,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'WHT T-SETTLED BUY: ' + A.InstrumentID,'C',A.WHTAmount
		,0,A.WHTAmount
		,1
		,0,A.WHTAmount
		From #InvBuyEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.WithHoldingTaxPPH23 = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		Where D.WHTDueDate = 0
		and isnull(A.WHTAmount,0) > 0
	
	END
END

--3.SELL EQUITY
BEGIN
	INSERT INTO #InvSellEquity
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			InstrumentTypePK,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			CommissionAmount ,
			LevyAmount ,
			VATAmount ,
			OTCAmount,
			WHTAmount,
			KPEIAmount,
			IncomeTaxSellAmount,
			DoneVolume,
			BitExercise,
			InvestmentPK
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			C.InstrumentTypePK,
			A.FundPK ,
			FundCashRefPK,
			DoneAmount ,
			CommissionAmount ,
			LevyAmount ,
			VATAmount ,
			OTCAmount,
			WHTAmount,
			KPEIAmount,
			IncomeTaxSellAmount,
			DoneVolume,case when D.InstrumentPK is null then 0 else 1 end BitExercise,InvestmentPK
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN
	(
	  select FundPK,InstrumentPK from Exercise where DistributionDate <= @Valuedate and Status = 2 
	   
	)D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 2
	and A.InstrumentTypePK in (1,4,16) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc




	--T0
	BEGIN
		
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 EQUITY SELL: ' + A.InstrumentID   
		From #InvSellEquity A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK

		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.BrokerCommission,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'BROKER COMM T0 EQUITY SELL: ' + A.InstrumentID,'D',A.CommissionAmount
		,A.CommissionAmount,0
		,1
		,A.CommissionAmount,0
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.BrokerCommission = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CommissionAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.BrokerLevy,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'LEVY KPEI T0 EQUITY SELL: ' + A.InstrumentID,'D',isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0)
		,isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0),0
		,1
		,isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0),0
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.BrokerLevy = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.LevyAmount,0) + isnull(A.KPEIAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.BrokerVat,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'VAT T0 EQUITY SELL: ' + A.InstrumentID,'D',A.VATAmount
		,A.VATAmount,0
		,1
		,A.VATAmount,0
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.BrokerVat = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.VATAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.BrokerSalesTax,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME TAX T0 EQUITY SELL: ' + A.InstrumentID,'D',A.IncomeTaxSellAmount
		,A.IncomeTaxSellAmount,0
		,1
		,A.IncomeTaxSellAmount,0
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.BrokerSalesTax = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.IncomeTaxSellAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.WithHoldingTaxPPH23,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'WHT T0 EQUITY SELL: ' + A.InstrumentID,'C',A.WHTAmount
		,0,A.WHTAmount
		,1
		,0,A.WHTAmount
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.WithHoldingTaxPPH23 = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		Where D.WHTDueDate = 1
		and isnull(A.WHTAmount,0) > 0

		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
		,Case when A.InstrumentTypePK = 1 Then B.AccountReceivableSaleEquity
				When A.InstrumentTypePK = 4 then B.AccountReceivableSaleRights
					When A.InstrumentTypePK = 16 then B.AccountReceivableSaleWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T0 EQUITY SELL: ' + A.InstrumentID,'D',A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END
		,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END,0
		,1
		,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END,0
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.AccountReceivableSaleEquity
													When A.InstrumentTypePK = 4 then B.AccountReceivableSaleRights
														When A.InstrumentTypePK = 16 then B.AccountReceivableSaleWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		Left join Fund D on A.FundPK = D.FundPK and D.status in (1,2)
		Where isnull(A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END,0) > 0
	

		INSERT INTO #SellEquityDistinct (FundPK,InstrumentPK,BitExercise,InvestmentPK)
		Select Distinct FundPK,InstrumentPK,BitExercise,InvestmentPK From #InvSellEquity

	

		INSERT INTO #AvgEquity
					(
						InvestmentPK,
						FundPK, 
						InstrumentPK,
						Date, 
						AvgCost 
					)
		Select  A.InvestmentPK,A.FundPK,A.InstrumentPK,@ValueDate
		,Case when BitExercise = 1 then isnull([dbo].[FgetAvgpriceExercise](@ValueDate,A.FundPK,InstrumentPK),0)
			else Case when isnull(B.AveragePriority,1) = 1 then [dbo].[Fgetlastavgfrominvestment] 	(@ValueDate,InstrumentPK,A.FundPK) 
						when  isnull(B.AveragePriority,1) = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPKForEDT] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentPK),0) Else
				[dbo].[Fgetlastavgfrominvestment] (@DateMinOne,InstrumentPK,A.FundPK) END
				END
		From #SellEquityDistinct A
		left join FundAccountingSetup B on A.FundPK = B.FundPK and B.status = 2



	



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,7,1,2
		,Case when A.InstrumentTypePK = 1 Then B.RealisedEquity
				When A.InstrumentTypePK = 4 then B.RealisedRights
					When A.InstrumentTypePK = 16 then B.RealisedWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REALISED T0 EQUITY SELL: ' + A.InstrumentID
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 'C' Else 'D' End
		, ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		,1
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.RealisedEquity
													When A.InstrumentTypePK = 4 then B.RealisedRights
														When A.InstrumentTypePK = 16 then B.RealisedWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgEquity D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK and A.InvestmentPK = D.InvestmentPK
		Where ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) > 0

		
		



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,8,1,2
		,Case when A.InstrumentTypePK = 1 Then B.InvestmentEquity
				When A.InstrumentTypePK = 4 then B.InvestmentRights
					When A.InstrumentTypePK = 16 then B.InvestmentWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 EQUITY SELL: ' + A.InstrumentID,'C',isnull(E.AvgCost,0) * isnull(A.DoneVolume,0)
		,0,isnull(E.AvgCost,0) * isnull(A.DoneVolume,0)
		,1
		,0,isnull(E.AvgCost,0) * isnull(A.DoneVolume,0)
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.InvestmentEquity
													When A.InstrumentTypePK = 4 then B.InvestmentRights
														When A.InstrumentTypePK = 16 then B.InvestmentWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		LEFT JOIN #AvgEquity E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.InvestmentPK = E.InvestmentPK
		Where isnull(E.AvgCost,0) * isnull(A.DoneVolume,0) > 0
		
	END

	--TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-SETTLED EQUITY SELL: ' + A.InstrumentID   
		From #InvSellEquity A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED EQUITY SELL: ' + A.InstrumentID,'D',A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ isnull(A.WHTAmount,0)
		,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ isnull(A.WHTAmount,0),0
		,1
		,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ isnull(A.WHTAmount,0),0
		From #InvSellEquity A  
		LEFT JOIN FundCashRef B on A.FundCashRefPK = B.FundCashRefPK and B.status in (1,2)
		LEFT JOIN FundJournalAccount C ON B.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND C.status IN (1,2)
		Left join Fund D on A.FundPK = D.FundPK and D.status in (1,2)
		Where isnull(A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ isnull(A.WHTAmount,0),0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,Case when A.InstrumentTypePK = 1 Then B.AccountReceivableSaleEquity
				When A.InstrumentTypePK = 4 then B.AccountReceivableSaleRights
					When A.InstrumentTypePK = 16 then B.AccountReceivableSaleWarrant END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T-SETTLED EQUITY SELL: ' + A.InstrumentID,'C',A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END
		,0,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END
		,1
		,0,A.DoneAmount - isnull(A.CommissionAmount,0) - isnull(A.LevyAmount,0) - isnull(A.KPEIAmount,0) - isnull(A.VATAmount,0) - isnull(A.IncomeTaxSellAmount,0) + isnull(A.OTCAmount,0)
		+ Case when D.WHTDueDate = 1 then isnull(A.WHTAmount,0) Else 0 END
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case when A.InstrumentTypePK = 1 Then B.AccountReceivableSaleEquity
													When A.InstrumentTypePK = 4 then B.AccountReceivableSaleRights
														When A.InstrumentTypePK = 16 then B.AccountReceivableSaleWarrant END = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		LEFT JOIN #AvgEquity E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.InvestmentPK = E.InvestmentPK
		Where isnull(E.AvgCost,0) * isnull(A.DoneVolume,0) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)

		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.WithHoldingTaxPPH23,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'WHT T-SETTLED EQUITY SELL: ' + A.InstrumentID,'C',A.WHTAmount
		,0,A.WHTAmount
		,1
		,0,A.WHTAmount
		From #InvSellEquity A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.WithHoldingTaxPPH23 = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
		Where D.WHTDueDate = 0 and Isnull(A.WHTAmount,0) > 0

	END
END

--4.BUY BOND
BEGIN
	INSERT INTO #InvBuyBond
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			DoneAccruedInterest ,
			IncomeTaxGainAmount ,
			IncomeTaxInterestAmount
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			A.FundCashRefPK,
			DoneAmount ,
			DoneAccruedInterest ,
			IncomeTaxGainAmount ,
			IncomeTaxInterestAmount
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 1
	and A.InstrumentTypePK in (2,3,8,9,12,13,14,15) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc

	 --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 BOND BUY: ' + A.InstrumentID   
		From #InvBuyBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InvestmentBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 BOND BUY: ' + A.InstrumentID,'D',A.DoneAmount
		,A.DoneAmount,0
		,1
		,A.DoneAmount,0
		From #InvBuyBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
			,B.InterestRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'INTEREST (BUY/SELL) T0 BOND BUY: ' + A.InstrumentID,'D',isnull(A.DoneAccruedInterest,0)
			,isnull(A.DoneAccruedInterest,0),0
			,1
			,isnull(A.DoneAccruedInterest,0),0
			From #InvBuyBond A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.InterestRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.DoneAccruedInterest,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
			,B.TaxCapitalGainBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'TAX GAIN T0 BOND BUY: ' + A.InstrumentID,'C',isnull(A.IncomeTaxGainAmount,0)
			,0,isnull(A.IncomeTaxGainAmount,0)
			,1
			,0,isnull(A.IncomeTaxGainAmount,0)
			From #InvBuyBond A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.TaxCapitalGainBond = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.IncomeTaxGainAmount,0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
			,B.TaxInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'TAX INTEREST T0 BOND BUY: ' + A.InstrumentID,'C',isnull(A.IncomeTaxInterestAmount,0)
			,0,isnull(A.IncomeTaxInterestAmount,0)
			,1
			,0,isnull(A.IncomeTaxInterestAmount,0)
			From #InvBuyBond A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.TaxInterestBond = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.IncomeTaxInterestAmount,0) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
			,B.PayablePurRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'AP T0 BOND BUY: ' + A.InstrumentID,'C',isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)
			,0,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)
			,1
			,0,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)
			From #InvBuyBond A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.PayablePurRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) > 0
	END

	 --TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-SETTLED BOND BUY: ' + A.InstrumentID   
		From #InvBuyBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
			,B.PayablePurRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'AP T-SETTLED BOND BUY: ' + A.InstrumentID,'D',isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)
			,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0),0
			,1
			,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0),0
			From #InvBuyBond A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON B.PayablePurRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) > 0

	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED BOND BUY: ' + A.InstrumentID,'C',isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)
		,0,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) 
		,1
		,0,isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) 
		From #InvBuyBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		Where isnull(A.DoneAmount,0) + isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)  > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST ACCR T-SETTLED BOND BUY: ' + A.InstrumentID
		,'D'
		,isnull(A.DoneAccruedInterest,0)
		,isnull(A.DoneAccruedInterest,0),0
		,1
		,isnull(A.DoneAccruedInterest,0),0
		From #InvBuyBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAccruedInterest,0) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.InterestRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST (BUY/SELL) T-SETTLED BOND BUY: ' + A.InstrumentID
		,'C'
		,isnull(A.DoneAccruedInterest,0)
		,0,isnull(A.DoneAccruedInterest,0)
		,1
		,0,isnull(A.DoneAccruedInterest,0)
		From #InvBuyBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAccruedInterest,0) > 0

	
	END
END

--5.SELL BOND
BEGIN
	INSERT INTO #InvSellBond
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			DoneAccruedInterest ,
			IncomeTaxGainAmount ,
			IncomeTaxInterestAmount,
			DoneVolume
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			DoneAccruedInterest ,
			IncomeTaxGainAmount ,
			IncomeTaxInterestAmount,
			DoneVolume
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 2
	and A.InstrumentTypePK  in (2,3,8,9,12,13,14,15) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc



	--T0
	BEGIN
		
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 BOND SELL: ' + A.InstrumentID   
		From #InvSellBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK



		INSERT INTO #SellBondDistinct (FundPK,InstrumentPK)
		Select Distinct FundPK,InstrumentPK From #InvSellBond


		INSERT INTO #AvgBond
					(
						FundPK, 
						InstrumentPK,
						Date, 
						AvgCost 
					)
		Select  A.FundPK,A.InstrumentPK,@ValueDate
		,Case when isnull(B.AveragePriorityBond,1) = 1 then [dbo].[Fgetlastavgfrominvestment] (@ValueDate,InstrumentPK,A.FundPK)/100 Else
				[dbo].[Fgetlastavgfrominvestment] (@DateMinOne,InstrumentPK,A.FundPK)/100 END
		From #SellBondDistinct A
		left join FundAccountingSetup B on A.FundPK = B.FundPK and B.status = 2


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InvestmentBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 BOND SELL: ' + A.InstrumentID
		, 'C'
		, ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		,0,ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		,1
		,0,ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgBond D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) > 0

		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST (BUY/SELL) T0 BOND SELL: ' + A.InstrumentID
		, 'C'
		, ABS(isnull(A.DoneAccruedInterest,0))
		,0,ABS(isnull(A.DoneAccruedInterest,0))
		,1
		,0,ABS(isnull(A.DoneAccruedInterest,0))
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.DoneAccruedInterest,0)) > 0


		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.TaxCapitalGainBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX GAIN T0 BOND SELL: ' + A.InstrumentID
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 'C' Else 'D' End
		, ABS(isnull(A.IncomeTaxGainAmount,0))
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 0 Else ABS(isnull(A.IncomeTaxGainAmount,0)) End 
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then ABS(isnull(A.IncomeTaxGainAmount,0)) Else 0 End
		,1
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 0 Else ABS(isnull(A.IncomeTaxGainAmount,0)) End 
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then ABS(isnull(A.IncomeTaxGainAmount,0)) Else 0 End
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxCapitalGainBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.IncomeTaxGainAmount,0)) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.TaxInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX INTEREST T0 BOND SELL: ' + A.InstrumentID
		, 'D'
		, ABS(isnull(A.IncomeTaxInterestAmount,0))
		,ABS(isnull(A.IncomeTaxInterestAmount,0)),0
		,1
		,ABS(isnull(A.IncomeTaxInterestAmount,0)),0
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxInterestBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.IncomeTaxInterestAmount,0)) > 0


		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.RealisedBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REALISED T0 BOND SELL: ' + A.InstrumentID
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 'C' Else 'D' End
		, ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		,1
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.RealisedBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgBond D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
		,B.AccountReceivableSaleBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T0 BOND SELL: ' + A.InstrumentID
		,'D'
		, ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))
		,ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))),0
		,1
		,ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))),0
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.AccountReceivableSaleBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgBond D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))) > 0


	END



	 --TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-SETTLED BOND SELL: ' + A.InstrumentID   
		From #InvSellBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.AccountReceivableSaleBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T0 BOND SELL: ' + A.InstrumentID
		,'C'
		,ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))
		,0,ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))
		,1
		,0,ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.AccountReceivableSaleBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgBond D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))) > 0

	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED BOND SELL: ' + A.InstrumentID
		,'D'
		,ABS((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))
		,ABS((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))),0
		,1
		,ABS((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))),0
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		LEFT JOIN #AvgBond F on A.FundPK = F.FundPK and A.InstrumentPK = F.InstrumentPK
		Where ABS((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) + isnull(A.DoneAccruedInterest,0) - 
		isnull(A.IncomeTaxGainAmount,0) - 
		isnull(A.IncomeTaxInterestAmount,0) - ((isnull(F.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)))  > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.InterestRecBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST (BUY/SELL) T-SETTLED BOND SELL: ' + A.InstrumentID
		,'D'
		,ABS(isnull(A.DoneAccruedInterest,0))
		,ABS(isnull(A.DoneAccruedInterest,0)),0
		,1
		,ABS(isnull(A.DoneAccruedInterest,0)),0
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestRecBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.DoneAccruedInterest,0)) > 0

	
	
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'ACCR INTEREST T-SETTLED BOND SELL: ' + A.InstrumentID
		,case when isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) < 0 then 'D' else 'C' end
		,ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0))
		,case when isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) < 0 then ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0))
			else 0 end
		,case when isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) < 0 then 0
			else ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)) end
		,1
		,case when isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) < 0 then ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0))
			else 0 end
		,case when isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0) < 0 then 0
			else ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)) end

		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.DoneAccruedInterest,0) - isnull(A.IncomeTaxGainAmount,0) - isnull(A.IncomeTaxInterestAmount,0)) > 0




		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.TaxCapitalGainBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX GAIN T-SETTLED BOND SELL: ' + A.InstrumentID
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 'D' Else 'C' End
		, ABS(isnull(A.IncomeTaxGainAmount,0))
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then ABS(isnull(A.IncomeTaxGainAmount,0)) Else 0 End 
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 0 Else ABS(isnull(A.IncomeTaxGainAmount,0)) End
		,1
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then ABS(isnull(A.IncomeTaxGainAmount,0)) Else 0 End 
		, Case when isnull(A.IncomeTaxGainAmount,0) <= 0 then 0 Else ABS(isnull(A.IncomeTaxGainAmount,0)) End
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxCapitalGainBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.IncomeTaxGainAmount,0)) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
		,B.TaxInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX GAIN T-SETTLED BOND SELL: ' + A.InstrumentID
		,'C'
		,ABS(isnull(A.IncomeTaxInterestAmount,0))
		,0,ABS(isnull(A.IncomeTaxInterestAmount,0))
		,1
		,0,ABS(isnull(A.IncomeTaxInterestAmount,0))
		From #InvSellBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxInterestBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.IncomeTaxInterestAmount,0)) > 0


	END


END

--6.INTEREST BOND
BEGIN
	INSERT INTO #InterestBondData -- FUND POSITION MAX(DATE) -1 DAN TIDAK ADA TRX, MATURE PUN DIHITUNG
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,sum(A.Balance),A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType 
	From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne 
	and A.AcqDate < @ValueDate and A.MaturityDate >= @ValueDate and A.FundPK in (select FundPK from #FundParam) 
	and NOT EXISTS 
    (  
		SELECT InstrumentPK FROM Investment D 
		WHERE A.InstrumentPK = D.InstrumentPK 
		AND A.FundPK = D.FundPK and A.AcqDate = D.AcqDate and D.StatusSettlement  = 2 
		and @ValueDate <= SettlementDate and A.FundPK = D.FundPK 
		and A.Balance = D.DoneVolume and D.TrxType = 2 and D.FundPK in (select FundPK from #FundParam) 
	)
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate

	INSERT INTO #InterestBondData --JUAL BOND MASIH BELUM SETTLED
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.Balance,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType
	From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	and A.AcqDate < @ValueDate and A.FundPK in (select FundPK from #FundParam) 
	left join
	(  
		SELECT InstrumentPK, FundPK, AcqDate, sum(DoneVolume) DoneVolume FROM Investment  
		WHERE  StatusSettlement  = 2 and TrxType = 2 and @ValueDate between ValueDate and SettlementDate   
		and FundPK in (select FundPK from #FundParam) 
		group by InstrumentPK,FundPK, AcqDate
	) D on A.InstrumentPK = D.InstrumentPK AND A.FundPK = D.FundPK and A.AcqDate = D.AcqDate 
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne and D.InstrumentPK is not null
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.Balance,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.DoneVolume
	having sum(A.Balance) = D.DoneVolume
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate

	INSERT INTO #InterestBondDivDays 
	select FundPK,InstrumentPK,AcqDate,DivDays,DivPayment,sum(CountDays) CountDays
	from
	( 
		select A.FundPK,A.InstrumentPK,A.AcqDate,
		case when A.InterestDaysType in (2) then DATEDIFF(Day,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK),dbo.FgetNextCouponDate(@ValueDate,A.InstrumentPK))  
				when A.InterestDaysType in (1,3,5,6,7) then 360 else 365  end DivDays,
		case when A.InterestPaymentType in (1,4,7) then 12 
				when A.InterestPaymentType in (13) then 4 
					when A.InterestPaymentType in (16) then 2 else 1 end DivPayment,
		case when A.InterestDaysType in (1,5,6,7) then 
				case when day(B.Date) = 31 then 0 
						when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 31 - day(B.Date) else 1 end else 1 end CountDays
		from #InterestBondData A
		left join ZDT_WorkingDays B on B.Date > @DateMinOne and B.Date <= @ValueDate
		where A.InstrumentTypePK in (2,3,8,9,11,13,14,15)
	) A
	group by FundPK,InstrumentPK,AcqDate,DivDays,DivPayment




	-- Rounded Calculation BCA DAN HSBC

	Declare @CFundPK int,@CInstrumentPK int,@CBalance numeric(18,2),@CInterestPercent numeric(18,6),@CAcqDate datetime,@CLastCouponDate datetime,@CAccruedInterestCalculation int
	Declare @CInstrumentTypePK int,@CPaymentDays int,@ZValueDate datetime,@CDivDays int,@CTaxExpensePercent numeric(18,4)
	Declare @CHoldDays int,@CPrevHoldDays int,@CLastAcqDays int,@CLastPrevAcqDays int,@CAcqDays int, @CPrevAcqDays int
	Declare @CGrossInterestAmountRound numeric(18,2),@CPrevGrossInterestAmountRound numeric(18,2),@CTaxGrossInterestAmount numeric(18,2),@CPrevTaxGrossInterestAmount numeric(18,2)
	Declare @CInterestAmount numeric(18,4),@CTaxExpenseAmount numeric(18,4),@CFinalAmount numeric(18,4),@CTrxAmount numeric(18,4)
	Declare @CGrossInterestAmountRoundCalc numeric(18,8),@CPrevGrossInterestAmountRoundCalc numeric(18,8),@CTaxGrossInterestAmountRoundCalc numeric(18,8),@CPrevTaxGrossInterestAmountRoundCalc numeric(18,8)

	Declare @InterestBondCalculation table
	(
		FundPK int,
		InstrumentPK int,
		Balance numeric(18,4),
		InterestPercent numeric(18,6),
		AcqDate datetime,
		LastCoupondate datetime,
		AccruedInterestCalculation int,
		PaymentDays int,
		TaxExpensePercent numeric(18,2)
	)

	Declare @InterestBondRoundingCalculation table
	(
		CDate datetime,
		CFundPK int,
		CInstrumentPK int,
		CBalance numeric(18,4),
		CInterestPercent numeric(18,6),
		CAcqDate datetime,
		CLastCouponDate datetime,
		CDivDays int,
		CHoldDays int,
		CPrevHoldDays int,
		CLastAcqDays int,
		CLastPrevAcqDays int,
		CAcqDays int,
		CPrevAcqDays int,
		CAccruedInterestCalculation int,
		CPaymentDays int,
		CTaxExpensePercent numeric(18,2)
	)

	Declare @InterestBondRoundingCheckCalculation table
	(
		CDate datetime,
		CFundPK int,
		CInstrumentPK int,
		CBalance numeric(18,4),
		CAcqDate datetime,
		CGrossInterestAmountRoundCalc numeric(22,8),
		CPrevGrossInterestAmountRoundCalc numeric(22,8),
		CTaxGrossInterestAmountRoundCalc numeric(22,8),
		CPrevTaxGrossInterestAmountRoundCalc numeric(22,8),
		CTaxExpensePercent numeric(18,2),
		CAccruedInterestCalculation int
	)

	Declare @InterestBondRoundingFinalCalculation table
	(
		CDate datetime,
		CFundPK int,
		CInstrumentPK int,
		CAcqDate datetime,
		CAccruedInterestCalculation int,
		CGrossInterestAmountRound numeric(22,8),
		CPrevGrossInterestAmountRound numeric(22,8),
		CTaxGrossInterestAmount numeric(22,8),
		CPrevTaxGrossInterestAmount numeric(22,8)
	)

	Insert into @InterestBondCalculation 	-- UNTUK BCA GOV BOND, HSBC GOV DAN CORPORATE BOND
	select A.FundPK,A.InstrumentPK,Balance,A.InterestPercent,AcqDate,  
	case when dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK) = @Valuedate then dbo.FgetLastCouponDate(dbo.FworkingDay(@ValueDate,-1),A.InstrumentPK)       
			else dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK) end,B.AccruedInterestCalculation, 12 / [priority] PaymentDays, TaxExpensePercent  from #InterestBondData A
	LEFT JOIN #AccruedInterestCalculation B on A.FundPK = B.FundPK
	LEFT JOIN Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	LEFT JOIN MasterValue D ON C.InterestPaymentType = D.code AND D.id = 'InterestPaymentType' AND D.status = 2 
	where ((A.InstrumentTypePK in (2,13,15) and B.AccruedInterestCalculation in (3)) or (A.InstrumentTypePK in (2,3,13,15) and B.AccruedInterestCalculation in (4))) and AcqDate < @valuedate




	DECLARE ZZ CURSOR FOR

	SELECT  TOP (DATEDIFF(DAY, dbo.FworkingDay(@ValueDate,-1), @ValueDate)) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id), dbo.FworkingDay(@ValueDate,-1))
	FROM sys.all_objects a CROSS JOIN sys.all_objects b

	Open ZZ
	Fetch Next From ZZ
	Into @ZValueDate

	While @@FETCH_STATUS = 0
	BEGIN



		delete @InterestBondRoundingCalculation
		insert into @InterestBondRoundingCalculation
		select @ZValueDate,FundPK,A.InstrumentPK,Balance,A.InterestPercent,AcqDate,LastCoupondate, 
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(LastCoupondate,dbo.Fgetnextcoupondate(@ValueDate,A.InstrumentPK)) 
			else datediff(day,LastCoupondate,case when dbo.Fgetnextcoupondate(dbo.FworkingDay(@ValueDate,-1),A.InstrumentPK) = @ValueDate then dbo.Fgetnextcoupondate(dbo.FworkingDay(@ValueDate,-1),A.InstrumentPK) else dbo.Fgetnextcoupondate(@ValueDate,A.InstrumentPK) end) end CDivDays,
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(LastCoupondate,@ZValueDate) else datediff(day,LastCoupondate,@ZValueDate) end CHoldDays, 
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(LastCoupondate,@ZValueDate - 1) else datediff(day,LastCoupondate,@ZValueDate - 1) end CPrevHoldDays,
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(LastCoupondate,AcqDate) else datediff(day,LastCoupondate,AcqDate) end CLastAcqDays, 
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(AcqDate,@ZValueDate - 1) else datediff(day,AcqDate,@ZValueDate - 1) end CLastPrevAcqDays,
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(AcqDate,@ZValueDate) else datediff(day,AcqDate,@ZValueDate) end CAcqDays, 
		case when A.AccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 and B.InterestDaysType in (1,3,5,6,7) then dbo.FgetDateDiffCorporateBond(AcqDate,@ZValueDate - 1) else datediff(day,AcqDate,@ZValueDate - 1) end CPrevAcqDays, 
		AccruedInterestCalculation,PaymentDays,A.TaxExpensePercent 
		FROM @InterestBondCalculation A
		left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
		--where FundPK = 9 and B.InstrumentTypePK = 3 


		delete @InterestBondRoundingCheckCalculation
		insert into @InterestBondRoundingCheckCalculation
		select CDate,CFundPK,CInstrumentPK,CBalance,CAcqDate,
		-- GROSS
		case when CAccruedInterestCalculation = 3 then cast((cast(CHoldDays as float)/ cast(CDivDays as float)) as float) *  round(CInterestPercent * 1000000/ 100 / CPaymentDays,0) 
				when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CBalance * CInterestPercent/100/CPaymentDays/CDivDays * CHoldDays
					when CAccruedInterestCalculation = 4 and B.InstrumentTypePK in (2,13,15) then round(cast(CHoldDays as float)/ cast(CDivDays as float) * (1000000 * CInterestPercent/100/CPaymentDays),0)		
						else round(cast((cast(CHoldDays as float)/ cast(CDivDays as float)) as float) *  CInterestPercent * 1000000/ 100 / CPaymentDays,0)  end CGrossInterestAmountRoundCalc,

		case when CAccruedInterestCalculation = 3 then cast((cast(CPrevHoldDays as float)/ cast(CDivDays as float)) as float) *  round(CInterestPercent * 1000000/ 100 / CPaymentDays,0) 
				when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CBalance * CInterestPercent/100/CPaymentDays/CDivDays * CPrevHoldDays
					when CAccruedInterestCalculation = 4 and B.InstrumentTypePK in (2,13,15) then round(cast(CPrevHoldDays as float)/ cast(CDivDays as float) * (1000000 * CInterestPercent/100/CPaymentDays),0)	
						else round(cast((cast(CPrevHoldDays as float)/ cast(CDivDays as float)) as float) *  CInterestPercent * 1000000/ 100 / CPaymentDays,0)  end CPrevGrossInterestAmountRoundCalc ,
	
		-- TAX
		case when CAccruedInterestCalculation = 3 then cast((cast(case when CAcqDate <= CLastCouponDate then CHoldDays else CAcqDays end as float)/ cast(CDivDays as float)) as float) *  round(CInterestPercent * 1000000/ 100 / CPaymentDays,0) 
				when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then (CTaxExpensePercent/100) * CBalance * CInterestPercent/100/CPaymentDays/CDivDays * case when CAcqDate <= CLastCouponDate then CHoldDays else CAcqDays end	
					when CAccruedInterestCalculation = 4 and B.InstrumentTypePK in (2,13,15) then round(cast(case when CAcqDate <= CLastCouponDate then CHoldDays else CAcqDays end as float)/ cast(CDivDays as float) * (1000000 * CInterestPercent/100/CPaymentDays),0)
						else round(cast((cast(case when CAcqDate <= CLastCouponDate then CHoldDays else CAcqDays end as float)/ cast(CDivDays as float)) as float) *  CInterestPercent * 1000000/ 100 / CPaymentDays,0) end CTaxGrossInterestAmountRoundCalc,

		case when CAccruedInterestCalculation = 3 then cast((cast(case when CAcqDate <= CLastCouponDate then CPrevHoldDays else CPrevAcqDays end as float)/ cast(CDivDays as float)) as float) *  round(CInterestPercent * 1000000/ 100 / CPaymentDays,0) 
				when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then (CTaxExpensePercent/100) * CBalance * CInterestPercent/100/CPaymentDays/CDivDays * case when CAcqDate <= CLastCouponDate then CPrevHoldDays else CPrevAcqDays end
					when CAccruedInterestCalculation = 4 and B.InstrumentTypePK in (2,13,15) then round(cast(case when CAcqDate <= CLastCouponDate then CPrevHoldDays else CPrevAcqDays end as float)/ cast(CDivDays as float) * (1000000 * CInterestPercent/100/CPaymentDays),0)
						else round(cast((cast(case when CAcqDate <= CLastCouponDate then CPrevHoldDays else CPrevAcqDays end as float)/ cast(CDivDays as float)) as float) *  CInterestPercent * 1000000/ 100 / CPaymentDays,0)  end CPrevTaxGrossInterestAmountRoundCalc,
		CTaxExpensePercent,CAccruedInterestCalculation

		from @InterestBondRoundingCalculation A
		left join Instrument B on A.CInstrumentPK = B.InstrumentPK and B.status in (1,2)


	

		delete @InterestBondRoundingFinalCalculation
		insert into @InterestBondRoundingFinalCalculation
		select CDate,CFundPK,CInstrumentPK,CAcqDate,CAccruedInterestCalculation,
		case when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CGrossInterestAmountRoundCalc 
			else CBalance /1000000 * case when CGrossInterestAmountRoundCalc % 1 < 0.51 then FLOOR(CGrossInterestAmountRoundCalc) else round(CGrossInterestAmountRoundCalc,0) end end CGrossInterestAmountRound,
		case when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CPrevGrossInterestAmountRoundCalc 
			else CBalance /1000000 * case when CPrevGrossInterestAmountRoundCalc % 1 < 0.51 then FLOOR(CPrevGrossInterestAmountRoundCalc) else round(CPrevGrossInterestAmountRoundCalc,0) end end CPrevGrossInterestAmountRound,
		case when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CTaxGrossInterestAmountRoundCalc 
			else (CTaxExpensePercent/100) * CBalance /1000000 * case when CTaxGrossInterestAmountRoundCalc % 1 < 0.51 then FLOOR(CTaxGrossInterestAmountRoundCalc) else round(CTaxGrossInterestAmountRoundCalc,0) end end CTaxGrossInterestAmount,
		case when CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3 then CPrevTaxGrossInterestAmountRoundCalc 
			else (CTaxExpensePercent/100) * CBalance /1000000 * case when CPrevTaxGrossInterestAmountRoundCalc % 1 < 0.51 then FLOOR(CPrevTaxGrossInterestAmountRoundCalc) else round(CPrevTaxGrossInterestAmountRoundCalc,0) end end CPrevTaxGrossInterestAmount  
		from @InterestBondRoundingCheckCalculation A
		left join Instrument B on A.CInstrumentPK = B.InstrumentPK and B.status in (1,2)



		-- ni buat HSBC DAN BCA GOV BOND
		insert into #TempInterestBond(Date,FundPK,InstrumentPK,AcqDate,InterestAmount,TaxAmount,FinalAmount)
		select @valuedate,CFundPK,CInstrumentPK,CAcqDate,
		CGrossInterestAmountRound - CPrevGrossInterestAmountRound,
		CTaxGrossInterestAmount - CPrevTaxGrossInterestAmount,
		(CGrossInterestAmountRound - CPrevGrossInterestAmountRound) - (CTaxGrossInterestAmount - CPrevTaxGrossInterestAmount)
		from @InterestBondRoundingFinalCalculation A
		left join Instrument B on A.CInstrumentPK = B.InstrumentPK and B.status in (1,2)
		where (A.CAccruedInterestCalculation = 3 and B.InstrumentTypePK in (2,13,15)) or (A.CAccruedInterestCalculation = 4 and B.InstrumentTypePK in (2,13,15))

		-- ni buat HSBC CORPORATE
		insert into #TempInterestBond(Date,FundPK,InstrumentPK,AcqDate,InterestAmount,TaxAmount,FinalAmount)
		select @valuedate,CFundPK,CInstrumentPK,CAcqDate,
		round(sum(CGrossInterestAmountRound),0) - round(sum(CPrevGrossInterestAmountRound),0) ,
		round(sum(CTaxGrossInterestAmount),0) - round(sum(CPrevTaxGrossInterestAmount),0) ,
		(round(sum(CGrossInterestAmountRound),0) - round(sum(CPrevGrossInterestAmountRound),0)) - (round(sum(CTaxGrossInterestAmount),0) - round(sum(CPrevTaxGrossInterestAmount),0)) 
		from @InterestBondRoundingFinalCalculation A
		left join Instrument B on A.CInstrumentPK = B.InstrumentPK and B.status in (1,2)
		where A.CAccruedInterestCalculation = 4 and B.InstrumentTypePK = 3
		group by CFundPK,CInstrumentPK,CAcqDate

        
	Fetch next From ZZ Into @ZValueDate
	END
	Close ZZ
	Deallocate ZZ


	INSERT INTO #TempInterestBondTotal(Date,FundPK,InstrumentPK,AcqDate,InterestAmount,TaxAmount,FinalAmount)
	select Date,FundPK,InstrumentPK,AcqDate,sum(InterestAmount),sum(TaxAmount),sum(FinalAmount) from #TempInterestBond 
	where Date = @valuedate
	group by Date,FundPK,InstrumentPK,AcqDate



	INSERT INTO #InterestBond
		(	FundJournalPK ,
			ValueDate ,
			InstrumentTypePK ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundID,
			InterestAmount,
			TaxPercent,
			AccruedInterestCalculation,
			FinalAmount,
			TaxAmount,
			InterestPaymentType,
			SukukType
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.FundPK,A.InstrumentTypePK,A.InstrumentPK,A.AcqDate ASC) Number ,
			@ValueDate ,
			A.InstrumentTypePK,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			D.ID FundID,
			case when A.InstrumentTypePK in (15) and C.SukukType = 2 then A.Balance * A.InterestPercent / 100 / B.DivDays /case when A.InterestDaysType in (2) then B.DivPayment else 1 end * B.CountDays -- ini untuk SUKUK Corporate
					when A.InstrumentTypePK in (2,13,15) and E.AccruedInterestCalculation in (3,4)  then F.InterestAmount -- versi Rounding gov BCA, kecuali untuk Monthly
						when A.InstrumentTypePK in (2,13,15) and E.AccruedInterestCalculation in (2) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays / case when A.InterestDaysType in (2) then B.DivPayment else 1 end * 1000000,0) * B.CountDays -- versi gov Rounding CORE			
							when A.InstrumentTypePK in (3) and E.AccruedInterestCalculation in (4) then F.InterestAmount
								else A.Balance * A.InterestPercent / 100 / B.DivDays /case when A.InterestDaysType in (2) then B.DivPayment else 1 end * B.CountDays  end InterestAmount,

			case when D.FundTypeInternal = 2 then @KPDTaxPercent 
					else C.TaxExpensePercent end TaxPercent,E.AccruedInterestCalculation,
			case when A.InstrumentTypePK in (15) and C.SukukType = 2 then 0
					when A.InstrumentTypePK in (2,13,15) and E.AccruedInterestCalculation in (3,4) then F.FinalAmount 
						when A.InstrumentTypePK in (3) and E.AccruedInterestCalculation in (4) then F.FinalAmount 
							else 0 end,
			case when A.InstrumentTypePK in (15) and C.SukukType = 2 then 0
					when A.InstrumentTypePK in (2,13,15) and E.AccruedInterestCalculation in (3,4) then F.TaxAmount 
						when A.InstrumentTypePK in (3) and E.AccruedInterestCalculation in (4) then F.TaxAmount 
							else 0 end,A.InterestPaymentType, isnull(C.SukukType,1)
	From #InterestBondData A  
	LEFT JOIN #InterestBondDivDays B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN dbo.Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
	LEFT JOIN #AccruedInterestCalculation E on A.FundPK = E.FundPK
	LEFT JOIN #TempInterestBondTotal F on A.FundPK = F.FundPK and A.InstrumentPK = F.InstrumentPK and A.AcqDate = F.AcqDate
	order by Number asc



	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,9,B.EndDayTrailsPK,'INTEREST BOND','','INTEREST BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID  
		From #InterestBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR INTEREST BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D'
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount - (A.InterestAmount * A.TaxPercent/100)
				 when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.FinalAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.FinalAmount 
						else A.InterestAmount - (A.InterestAmount * A.TaxPercent/100) end
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount - (A.InterestAmount * A.TaxPercent/100) 
				when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.FinalAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.FinalAmount 
						else A.InterestAmount - (A.InterestAmount * A.TaxPercent/100) end,0
		,1
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount - (A.InterestAmount * A.TaxPercent/100) 
				when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.FinalAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.FinalAmount 
						else A.InterestAmount - (A.InterestAmount * A.TaxPercent/100) end,0
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where (isnull(A.InterestAmount - (A.InterestAmount * A.TaxPercent/100),0) > 0 and @ClientCode <> '05') 
		or (isnull(A.InterestAmount - (A.InterestAmount * A.TaxPercent/100),0) > 0 and @ClientCode = '05' and A.FundPK <> 43) 

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.TaxExpenseInterestIncomeBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX INTEREST BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D'
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount * A.TaxPercent/100
				when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.TaxAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.TaxAmount
						else A.InterestAmount * A.TaxPercent/100 end
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount * A.TaxPercent/100
				when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.TaxAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.TaxAmount
						else A.InterestAmount * A.TaxPercent/100 end,0
		,1
		,case when A.InstrumentTypePK in (15) and A.SukukType = 2 then A.InterestAmount * A.TaxPercent/100
				when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3,4)  then A.TaxAmount 
					when A.InstrumentTypePK in (3) and A.AccruedInterestCalculation in (4) then A.TaxAmount
						else A.InterestAmount * A.TaxPercent/100 end,0
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxExpenseInterestIncomeBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where (isnull(A.InterestAmount * A.TaxPercent/100,0) > 0  and @ClientCode <> '05') 
		or (isnull(A.InterestAmount * A.TaxPercent/100,0) > 0 and @ClientCode = '05' and A.FundPK <> 43) 


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.IncomeInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME INTEREST BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C'
		,A.InterestAmount
		,0,A.InterestAmount
		,1
		,0,A.InterestAmount
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where (isnull(A.InterestAmount,0) > 0  and @ClientCode <> '05') 
		or (isnull(A.InterestAmount,0) > 0 and @ClientCode = '05' and A.FundPK <> 43) 


		-- CUSTOM MNC DANAMON
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR INTEREST BOND DANAMON : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D',isnull(A.InterestAmount,0)
		,isnull(A.InterestAmount,0),0
		,1
		,isnull(A.InterestAmount,0),0
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount,0) > 0 and (@ClientCode = '05' and A.FundPK = 43)

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.TaxExpenseInterestIncomeBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX INTEREST BOND DANAMON : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C',case when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3)  then A.TaxAmount else A.InterestAmount * A.TaxPercent/100 end
		,0,case when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3)  then A.TaxAmount else A.InterestAmount * A.TaxPercent/100 end
		,1
		,0,case when A.InstrumentTypePK in (2,13,15) and A.AccruedInterestCalculation in (3)  then A.TaxAmount else A.InterestAmount * A.TaxPercent/100 end
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxExpenseInterestIncomeBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount * A.TaxPercent/100,0) > 0 and (@ClientCode = '05' and A.FundPK = 43)


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.IncomeInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME INTEREST BOND DANAMON : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C',isnull(A.InterestAmount - (A.InterestAmount * A.TaxPercent/100),0)
		,0,isnull(A.InterestAmount - (A.InterestAmount * A.TaxPercent/100),0)
		,1
		,0,isnull(A.InterestAmount - (A.InterestAmount * A.TaxPercent/100),0)
		From #InterestBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount,0) > 0 and (@ClientCode = '05' and A.FundPK = 43)

	END

END




--7.INTEREST PAYMENT BOND
BEGIN
	INSERT INTO #BondPaymentDate
	select distinct A.InstrumentPK,case when A.InterestPaymentType in (7) then dateadd(month,-1,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) 
										 when A.InterestPaymentType in (13) then dateadd(month,-3,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
											when A.InterestPaymentType in (16) then dateadd(month,-6,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
												when A.InterestPaymentType in (19) then dateadd(month,-12,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) end,
	dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK) 
	from FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne  and A.FundPK in (select FundPK from #FundParam) 


	INSERT INTO #InterestPaymentBondData -- FUND POSITION MAX(DATE) -1 DAN TIDAK ADA TRX
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,sum(A.Balance),A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate,D.PaymentDate 
	From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	left join #BondPaymentDate D on A.InstrumentPK = D.InstrumentPK
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne 
	and A.AcqDate < @ValueDate and A.MaturityDate > @ValueDate and  D.PaymentDate > @DateMinOne and D.PaymentDate <= @ValueDate and A.FundPK in (select FundPK from #FundParam) 	
	and NOT EXISTS 
    (  
		SELECT InstrumentPK FROM Investment D WHERE A.InstrumentPK = D.InstrumentPK 
		AND A.FundPK = D.FundPK and A.AcqDate = D.AcqDate and D.StatusSettlement  = 2 
		and @ValueDate <= SettlementDate and A.FundPK = D.FundPK 
		and A.Balance = D.DoneVolume and D.TrxType = 2 and D.FundPK in (select FundPK from #FundParam) 
	)
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate,D.PaymentDate
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate

	INSERT INTO #InterestPaymentBondData --JUAL BOND MASIH BELUM SETTLED
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,sum(A.Balance),A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate,D.PaymentDate 
	From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	left join #BondPaymentDate D on A.InstrumentPK = D.InstrumentPK
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne
	and A.AcqDate < @ValueDate and A.MaturityDate > @ValueDate and  D.PaymentDate > @DateMinOne and D.PaymentDate <= @ValueDate  and A.FundPK in (select FundPK from #FundParam) 
	and EXISTS 
    (  
		SELECT * FROM Investment D WHERE A.InstrumentPK = D.InstrumentPK AND A.FundPK = D.FundPK 
		and StatusSettlement  = 2 and TrxType = 2 and @ValueDate between ValueDate and SettlementDate   
		and A.[Identity] = D.TrxBuy and A.FundPK = D.FundPK and D.FundPK in (select FundPK from #FundParam) 
	)
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate,D.PaymentDate
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate
	
	INSERT INTO #InterestPaymentBondDivDays 
	select FundPK,InstrumentPK,AcqDate,DivDays,DivPayment,sum(CountDays) CountDays
	from
	( 
		select A.FundPK,A.InstrumentPK,A.AcqDate,
		case when A.InterestDaysType in (2) then DATEDIFF(Day,
								case when A.InterestPaymentType in (7) then dateadd(month,-1,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) 
										 when A.InterestPaymentType in (13) then dateadd(month,-3,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
											when A.InterestPaymentType in (16) then dateadd(month,-6,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
												when A.InterestPaymentType in (19) then dateadd(month,-12,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) end,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))  
				when A.InterestDaysType in (1,3,5,6,7) then 360 else 365  end DivDays,
		case when A.InterestPaymentType in (1,4,7) then 12 
				when A.InterestPaymentType in (13) then 4 
					when A.InterestPaymentType in (16) then 2 else 1 end DivPayment,
		case when A.InterestDaysType in (1,5,6,7) then 
				case when day(B.Date) = 31 then 0 
						when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 31 - day(B.Date) else 1 end else 1 end CountDays
		from #InterestPaymentBondData A
		left join ZDT_WorkingDays B on B.Date > A.LastPaymentDate  and B.Date <= @ValueDate									  
		where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and B.Date <= A.PaymentDate 
	) A
	group by FundPK,InstrumentPK,AcqDate,DivDays,DivPayment


	INSERT INTO #TaxInterestPaymentBondDivDays 
	select FundPK,InstrumentPK,AcqDate,sum(TaxDays) TaxDays
	from
	( 
		select A.FundPK,A.InstrumentPK,A.AcqDate,
		case when A.InterestDaysType in (1,5,6,7) then 
				case when day(B.Date) = 31 then 0 
						when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 31 - day(B.Date) else 1 end else 1 end TaxDays, B.Date
		from #InterestPaymentBondData A
		left join ZDT_WorkingDays B on B.Date > case when A.AcqDate >= A.LastPaymentDate then A.AcqDate else A.LastPaymentDate end and B.Date <= @ValueDate									  
		where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and B.Date <= A.PaymentDate 
	) A
	group by FundPK,InstrumentPK,AcqDate

	INSERT INTO #InterestPaymentBondRoundedCalculation
	select A.FundPK,InstrumentPK,AcqDate,sum(dbo.FGetBondInterestAccruedForRoundedCalculation(B.Date,A.FundPK,A.InstrumentPK,A.AcqDate,A.Balance)),
	sum(dbo.FGetTaxBondInterestAccruedForRoundedCalculation(B.Date,A.FundPK,A.InstrumentPK,A.AcqDate,A.Balance))
	from #InterestPaymentBondData A
	left join ZDT_WorkingDays B on B.Date > A.LastPaymentDate  and B.Date <= @ValueDate
	LEFT JOIN #AccruedInterestCalculation F on A.FundPK = F.FundPK 								  
	where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and B.Date <= A.PaymentDate and F.AccruedInterestCalculation = 3	--and A.FundPK = 4 and A.InstrumentPK = 730 --and A.AcqDate = '07/06/2020'
	group by A.FundPK,InstrumentPK,AcqDate


	INSERT INTO #InterestPaymentBond
		(	FundJournalPK ,
			ValueDate ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundID,
			InterestAmount,
			TaxAmount,
			InterestCalculation
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.FundPK,A.InstrumentTypePK,A.InstrumentPK,A.AcqDate ASC) Number ,
			@ValueDate ,
			A.InstrumentPK ,
			D.ID InstrumentID ,
			A.FundPK ,
			E.ID FundID,
			case when A.InstrumentTypePK in (2,13,15) and F.AccruedInterestCalculation in (3)  then G.GrossInterestAmount
					when A.InstrumentTypePK in (2,13,15) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays / 
						case when A.InterestDaysType in (2) then B.DivPayment else 1 end  * 1000000,0) * B.CountDays 
							else A.Balance * A.InterestPercent / 100 / B.DivDays / 
								case when A.InterestDaysType in (2) then B.DivPayment else 1 end  * B.CountDays  end InterestAmount,

			case when A.InstrumentTypePK in (2,13,15) and F.AccruedInterestCalculation in (3)  then G.TaxInterestAmount
					when A.InstrumentTypePK in (2,13,15) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays / 
						case when A.InterestDaysType in (2) then B.DivPayment else 1 end * 1000000,0) * C.TaxDays *
											case when E.FundTypeInternal = 2 then @KPDTaxPercent/100 
													else D.TaxExpensePercent/100 end

														else A.Balance * A.InterestPercent / 100 / B.DivDays / 
															case when A.InterestDaysType in (2) then B.DivPayment else 1 end * C.TaxDays *
																	case when E.FundTypeInternal = 2 then @KPDTaxPercent/100 
																			else D.TaxExpensePercent/100 end  end  TaxAmount,F.AccruedInterestCalculation
	From #InterestPaymentBondData A  
	LEFT JOIN #InterestPaymentBondDivDays B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate
	LEFT JOIN #TaxInterestPaymentBondDivDays C on A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK and A.AcqDate = C.AcqDate
	LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status IN (1,2)
	LEFT JOIN dbo.Fund E ON A.FundPK = E.FundPK AND E.status IN (1,2)
	LEFT JOIN #AccruedInterestCalculation F on A.FundPK = F.FundPK
	LEFT JOIN #InterestPaymentBondRoundedCalculation G on A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK and A.AcqDate = G.AcqDate
	order by Number asc

	
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,8,B.EndDayTrailsPK,'REC COUPON','','INTEREST PAYMENT BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID  
		From #InterestPaymentBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,@DefaultBankAccountPK,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST PAYMENT BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D',isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0)
		,isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0),0
		,1
		,isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0),0
		From #InterestPaymentBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON @DefaultBankAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST PAYMENT BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C',case when A.InterestCalculation = 3 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		,0,case when A.InterestCalculation = 3 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		,1
		,0,case when A.InterestCalculation = 3 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		From #InterestPaymentBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status IN (1,2)
		Where case when A.InterestCalculation = 3 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,case when A.InterestCalculation = 3 then B.WHTTaxPayableAccrInterestBond else B.TaxInterestBond end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST PAYMENT BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D',isnull(A.TaxAmount,0)
		,isnull(A.TaxAmount,0),0
		,1
		,isnull(A.TaxAmount,0),0
		From #InterestPaymentBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.InterestCalculation = 3 then B.WHTTaxPayableAccrInterestBond else B.TaxInterestBond end = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status = 2
		Where isnull(A.TaxAmount,0) > 0 and A.InterestCalculation = 3 and D.InstrumentTypePK in (2,13,15)
	END

END


--8.MATURE BOND
BEGIN
	INSERT INTO #BondMatureDate
	select distinct A.InstrumentPK,case when A.InterestPaymentType in (7) then dateadd(month,-1,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) 
										 when A.InterestPaymentType in (13) then dateadd(month,-3,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
											when A.InterestPaymentType in (16) then dateadd(month,-6,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK))
												when A.InterestPaymentType in (19) then dateadd(month,-12,dbo.FgetLastCouponDate(@ValueDate,A.InstrumentPK)) end 
	from FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne  and A.FundPK in (select FundPK from #FundParam) 


	INSERT INTO #MatureBondData -- FUND POSITION MAX(DATE) -1 DAN TIDAK ADA TRX
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,sum(A.Balance),A.CostValue,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	left join #BondMatureDate D on A.InstrumentPK = D.InstrumentPK
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne 
	and A.AcqDate < @ValueDate and A.MaturityDate > @DateMinOne and A.MaturityDate <= @ValueDate	 and A.FundPK in (select FundPK from #FundParam) 
	and NOT EXISTS 
    (  
		SELECT InstrumentPK FROM Investment D 
		WHERE A.InstrumentPK = D.InstrumentPK 
		AND A.FundPK = D.FundPK and A.AcqDate = D.AcqDate and D.StatusSettlement  = 2 
		and @ValueDate <= SettlementDate and A.FundPK = D.FundPK 
		and A.Balance = D.DoneVolume and D.TrxType = 2 and D.FundPK in (select FundPK from #FundParam) 
	)
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.CostValue,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate


	INSERT INTO #MatureBondData --JUAL BOND MASIH BELUM SETTLED
	Select Distinct A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,sum(A.Balance),A.CostValue,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate From FundPosition A
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
	left join #BondMatureDate D on A.InstrumentPK = D.InstrumentPK
	where C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and A.Date = @DateMinOne
	and A.AcqDate < @ValueDate and A.MaturityDate > @DateMinOne and A.MaturityDate <= @ValueDate  and A.FundPK in (select FundPK from #FundParam) 
	and EXISTS 
    (  
		SELECT * FROM Investment D WHERE A.InstrumentPK = D.InstrumentPK AND A.FundPK = D.FundPK 
		and StatusSettlement  = 2 and TrxType = 2 and @ValueDate between ValueDate and SettlementDate   
		and A.[Identity] = D.TrxBuy and A.FundPK = D.FundPK and D.FundPK in (select FundPK from #FundParam) 
	)
	group by A.Date,A.FundPK,C.InstrumentTypePK,A.InstrumentPK,A.CostValue,A.InterestPercent,A.AcqDate,
	A.MaturityDate,A.InterestDaysType,A.InterestPaymentType,D.LastPaymentDate
	order by A.FundPK,C.InstrumentTypePK,A.InstrumentPK,AcqDate
	
	INSERT INTO #MatureBondDivDays 
	select FundPK,InstrumentPK,AcqDate,DivDays,DivPayment,sum(CountDays) CountDays
	from
	( 
		select A.FundPK,A.InstrumentPK,A.AcqDate,
		case when A.InterestDaysType in (2) then DATEDIFF(Day,dbo.FgetLastCouponDate(A.Date,A.InstrumentPK),A.MaturityDate)  
				when A.InterestDaysType in (1,3,5,6,7) then 360 else 365  end DivDays,
		case when A.InterestPaymentType in (1,4,7) then 12 
				when A.InterestPaymentType in (13) then 4 
					when A.InterestPaymentType in (16) then 2 else 1 end DivPayment,
		case when A.InterestDaysType in (1,5,6,7) then 
				case when day(B.Date) = 31 then 0 
						when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 30 - day(B.Date) else 1 end else 1 end CountDays
		from #MatureBondData A
		left join ZDT_WorkingDays B on B.Date > A.LastPaymentDate  and B.Date <= @ValueDate									  
		where A.InstrumentTypePK in (2,3,8,9,11,13,14,15)
	) A
	group by FundPK,InstrumentPK,AcqDate,DivDays,DivPayment


	INSERT INTO #TaxMatureBondDivDays 
	select FundPK,InstrumentPK,AcqDate,sum(TaxDays) TaxDays
	from
	( 
		select A.FundPK,A.InstrumentPK,A.AcqDate,
		case when A.InterestDaysType in (1,5,6,7) then 
				case when day(B.Date) = 31 then 0 
						when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 30 - day(B.Date) else 1 end else 1 end TaxDays, B.Date
		from #MatureBondData A
		left join ZDT_WorkingDays B on B.Date > case when A.AcqDate >= A.LastPaymentDate then A.AcqDate else A.LastPaymentDate end and B.Date <= @ValueDate									  
		where A.InstrumentTypePK in (2,3,8,9,11,13,14,15)
	) A
	group by FundPK,InstrumentPK,AcqDate

	INSERT INTO #MatureBondRoundedCalculation
	select A.FundPK,InstrumentPK,AcqDate,sum(dbo.FGetBondInterestAccruedForRoundedCalculation(B.Date,A.FundPK,A.InstrumentPK,A.AcqDate,A.Balance)),
	sum(dbo.FGetTaxBondInterestAccruedForRoundedCalculation(B.Date,A.FundPK,A.InstrumentPK,A.AcqDate,A.Balance))
	from #MatureBondData A
	left join ZDT_WorkingDays B on B.Date > A.LastPaymentDate  and B.Date <= @ValueDate
	LEFT JOIN #AccruedInterestCalculation F on A.FundPK = F.FundPK 								  
	where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and B.Date <= A.MaturityDate and F.AccruedInterestCalculation = 3	--and A.FundPK = 4 and A.InstrumentPK = 730 --and A.AcqDate = '07/06/2020'
	group by A.FundPK,InstrumentPK,AcqDate



	INSERT INTO #MatureBond
		(	FundJournalPK ,
			ValueDate ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundID,
			Balance,
			RealiseAmount,
			InterestAmount,
			TaxAmount,
			InterestCalculation,
			CapitalGainAmount

		)

	Select ROW_NUMBER() OVER	(ORDER BY A.FundPK,A.InstrumentTypePK,A.InstrumentPK,A.AcqDate ASC) Number ,
			@ValueDate ,
			A.InstrumentPK ,
			D.ID InstrumentID ,
			A.FundPK ,
			E.ID FundID,
			A.Balance,
			A.CostValue - A.Balance,
			case when A.InstrumentTypePK in (2,13,15) and F.AccruedInterestCalculation in (3)  then G.GrossInterestAmount
					when A.InstrumentTypePK in (2,13,15) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays / 
						case when A.InterestDaysType in (2) then B.DivPayment else 1 end  * 1000000,0) * B.CountDays 
						else A.Balance * A.InterestPercent / 100 / B.DivDays / 
							case when A.InterestDaysType in (2) then B.DivPayment else 1 end  * B.CountDays  end InterestAmount,
			
			case when A.InstrumentTypePK in (2,13,15) and F.AccruedInterestCalculation in (3)  then G.TaxInterestAmount
					when A.InstrumentTypePK in (2,13) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays / 
						case when A.InterestDaysType in (2) then B.DivPayment else 1 end  * 1000000,0) * C.TaxDays *
							case when E.FundTypeInternal = 2 then @KPDTaxPercent/100 
									else D.TaxExpensePercent/100 end

											else A.Balance * A.InterestPercent / 100 / B.DivDays / 
															case when A.InterestDaysType in (2) then B.DivPayment else 1 end * C.TaxDays *
																	case when E.FundTypeInternal = 2 then @KPDTaxPercent/100 
																			else D.TaxExpensePercent/100 end  end  TaxAmount,F.AccruedInterestCalculation,
	case when (A.CostValue - A.Balance) > 0 then 0.05 * (A.CostValue - A.Balance) else 0 end
	From #MatureBondData A  
	LEFT JOIN #MatureBondDivDays B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate
	LEFT JOIN #TaxMatureBondDivDays C on A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK and A.AcqDate = C.AcqDate
	LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status IN (1,2)
	LEFT JOIN dbo.Fund E ON A.FundPK = E.FundPK AND E.status IN (1,2)
	LEFT JOIN #AccruedInterestCalculation F on A.FundPK = F.FundPK
	LEFT JOIN #MatureBondRoundedCalculation G on A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK and A.AcqDate = G.AcqDate
	order by Number asc



	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,8,B.EndDayTrailsPK,'MATURE TO BANK','','MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID  
		From #MatureBond A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,@DefaultBankAccountPK,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D',case when A.CapitalGainAmount > 0 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) - (isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0)) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		,case when A.CapitalGainAmount > 0 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) - (isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0)) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end,0
		,1
		,case when A.CapitalGainAmount > 0 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) - (isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0)) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end,0
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON @DefaultBankAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status = 2
		Where isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C'
		,case when A.CapitalGainAmount > 0  and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		,0,case when A.CapitalGainAmount > 0  and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		,1
		,0,case when A.CapitalGainAmount > 0  and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status = 2
		Where case when A.CapitalGainAmount > 0 and D.InstrumentTypePK in (2,13,15) then isnull(A.InterestAmount,0) else isnull(A.InterestAmount,0) - isnull(A.TaxAmount,0) end > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,@DefaultBankAccountPK,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INVESTMENT MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D',isnull(A.Balance,0)
		,isnull(A.Balance,0),0
		,1
		,isnull(A.Balance,0),0
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON @DefaultBankAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.Balance,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.InvestmentBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INVESTMENT MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'C',isnull(A.Balance,0)
		,0,isnull(A.Balance,0)
		,1
		,0,isnull(A.Balance,0)
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.Balance,0) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.RealisedBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REALISED MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID
		,Case when isnull(A.RealiseAmount,0) > 0 then 'D' else 'C' end,ABS(isnull(A.RealiseAmount,0))
		,Case when isnull(A.RealiseAmount,0) > 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end,Case when isnull(A.RealiseAmount,0) < 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end
		,1
		,Case when isnull(A.RealiseAmount,0) > 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end,Case when isnull(A.RealiseAmount,0) < 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.RealisedBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.RealiseAmount,0) <> 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
		,B.InvestmentBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REALISED MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,Case when isnull(A.RealiseAmount,0) < 0 then 'D' else 'C' end,ABS(isnull(A.RealiseAmount,0))
		,Case when isnull(A.RealiseAmount,0) < 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end,Case when isnull(A.RealiseAmount,0) > 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end
		,1
		,Case when isnull(A.RealiseAmount,0) < 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end,Case when isnull(A.RealiseAmount,0) > 0 then ABS(isnull(A.RealiseAmount,0))  else 0 end
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentBond = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.RealiseAmount,0) <> 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,7,1,2
		,B.TaxInterestBond,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST MATURE BOND : ' + A.InstrumentID + ', FUND : ' + A.FundID,'D'
		,isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0)
		,isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0),0
		,1
		,isnull(A.TaxAmount,0) - isnull(A.CapitalGainAmount,0),0
		From #MatureBond A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrBond = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN dbo.Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status = 2
		Where A.CapitalGainAmount > 0 and D.InstrumentTypePK in (2,13,15)

		

	END

	



END

--9.BUY REKSADANA
BEGIN

	INSERT INTO #InvBuyReksadana
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount,
			ReksadanaTypePK
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			A.FundCashRefPK,
			DoneAmount ,
			ReksadanaTypePK
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 1
	and A.InstrumentTypePK in (6) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc

	
	 --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 REKSADANA BUY: ' + A.InstrumentID   
		From #InvBuyReksadana A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,case when A.ReksadanaTypePK = 6 then B.InvestmentPrivateEquityFund 
				when A.ReksadanaTypePK = 7 then InvestmentProtectedFund 
					else InvestmentMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 REKSADANA BUY: ' + A.InstrumentID,'D',A.DoneAmount
		,A.DoneAmount,0
		,1
		,A.DoneAmount,0
		From #InvBuyReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.InvestmentPrivateEquityFund 
												when A.ReksadanaTypePK = 7 then InvestmentProtectedFund 
													else InvestmentMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,case when A.ReksadanaTypePK = 6 then B.PayablePurchasePrivateEquityFund 
					when A.ReksadanaTypePK = 7 then PayablePurchaseProtectedFund 
							else PayablePurchaseMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AP T0 REKSADANA BUY: ' + A.InstrumentID,'C',A.DoneAmount
		,0,A.DoneAmount
		,1
		,0,A.DoneAmount
		From #InvBuyReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.PayablePurchasePrivateEquityFund 
													when A.ReksadanaTypePK = 7 then PayablePurchaseProtectedFund 
															else PayablePurchaseMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

		



	END





	 --TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-SETTLED REKSADANA BUY: ' + A.InstrumentID   
		From #InvBuyReksadana A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
			,case when A.ReksadanaTypePK = 6 then B.PayablePurchasePrivateEquityFund 
					when A.ReksadanaTypePK = 7 then PayablePurchaseProtectedFund 
							else PayablePurchaseMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'AP T-SETTLED REKSADANA BUY: ' + A.InstrumentID,'D',isnull(A.DoneAmount,0)
			,isnull(A.DoneAmount,0),0
			,1
			,isnull(A.DoneAmount,0),0
			From #InvBuyReksadana A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.PayablePurchasePrivateEquityFund 
													when A.ReksadanaTypePK = 7 then PayablePurchaseProtectedFund 
														else PayablePurchaseMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.DoneAmount,0) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED REKSADANA BUY: ' + A.InstrumentID,'C',A.DoneAmount
		,0,ISNULL(A.DoneAmount,0) 
		,1
		,0,ISNULL(A.DoneAmount,0) 
		From #InvBuyReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		Where ISNULL(A.DoneAmount,0) > 0


	
	END
END

--10.SELL REKSADANA
BEGIN
	INSERT INTO #InvSellReksadana
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			ReksadanaTypePK,
			DoneVolume
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			FundPK ,
			FundCashRefPK,
			DoneAmount ,
			C.ReksadanaTypePK,
			DoneVolume
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 2
	and A.InstrumentTypePK  in (6) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc



	--T0
	BEGIN
		
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 REKSADANA SELL: ' + A.InstrumentID   
		From #InvSellReksadana A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK



		INSERT INTO #SellReksadanaDistinct (FundPK,InstrumentPK)
		Select Distinct FundPK,InstrumentPK From #InvSellReksadana

		

		INSERT INTO #AvgReksadana
					(
						FundPK, 
						InstrumentPK,
						Date, 
						AvgCost 
					)
		Select  A.FundPK,A.InstrumentPK,@ValueDate
		,Case when isnull(B.AveragePriority,1) = 1 then [dbo].[Fgetlastavgfrominvestment] (@ValueDate,InstrumentPK,A.FundPK) Else
				[dbo].[Fgetlastavgfrominvestment] (@DateMinOne,InstrumentPK,A.FundPK) END
		From #SellReksadanaDistinct A
		left join FundAccountingSetup B on A.FundPK = B.FundPK and B.status = 2



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,case when A.ReksadanaTypePK = 6 then B.AccountReceivableSalePrivateEquityFund 
				when A.ReksadanaTypePK = 7 then AccountReceivableSaleProtectedFund 
					else AccountReceivableSaleMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T0 REKSADANA SELL: ' + A.InstrumentID
		, 'D'
		, ABS(isnull(A.DoneAmount,0))
		, ABS(isnull(A.DoneAmount,0)),0
		,1
		, ABS(isnull(A.DoneAmount,0)),0
		From #InvSellReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.AccountReceivableSalePrivateEquityFund 
													when A.ReksadanaTypePK = 7 then AccountReceivableSaleProtectedFund 
														else AccountReceivableSaleMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
		Where ABS(isnull(A.DoneAmount,0)) > 0

	

		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.RealisedMutualFund,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REALISED T0 REKSADANA SELL: ' + A.InstrumentID
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 'C' Else 'D' End
		, ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		,1
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then 0 else ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) End 
		,Case when isnull(D.AvgCost,0) * isnull(A.DoneVolume,0) <= A.DoneAmount then ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0))  else 0 End
		From #InvSellReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.RealisedMutualFund = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgReksadana D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS((isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) - isnull(A.DoneAmount,0)) > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,case when A.ReksadanaTypePK = 6 then B.InvestmentPrivateEquityFund when A.ReksadanaTypePK = 7 then InvestmentProtectedFund else InvestmentMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 REKSADANA SELL: ' + A.InstrumentID
		,'C'
		, ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		,0,ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		,1
		,0,ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0))
		From #InvSellReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.InvestmentPrivateEquityFund when A.ReksadanaTypePK = 7 then InvestmentProtectedFund else InvestmentMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgReksadana D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS(isnull(D.AvgCost,0) * isnull(A.DoneVolume,0)) > 0


	END




	-- TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.SettlementDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T-SETTLED REKSADANA SELL: ' + A.InstrumentID   
		From #InvSellReksadana A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
	
		

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,case when A.ReksadanaTypePK = 6 then B.AccountReceivableSalePrivateEquityFund when A.ReksadanaTypePK = 7 then AccountReceivableSaleProtectedFund else AccountReceivableSaleMutualFund end,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR T-SETTLED REKSADANA SELL: ' + A.InstrumentID
		, 'C'
		, ABS(isnull(A.DoneAmount,0))
		,0, ABS(isnull(A.DoneAmount,0))
		,1
		,0, ABS(isnull(A.DoneAmount,0))
		From #InvSellReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.ReksadanaTypePK = 6 then B.AccountReceivableSalePrivateEquityFund when A.ReksadanaTypePK = 7 then AccountReceivableSaleProtectedFund else AccountReceivableSaleMutualFund end = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #AvgReksadana D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
		Where ABS(isnull(A.DoneAmount,0)) > 0



		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED REKSADANA SELL: ' + A.InstrumentID,'D',A.DoneAmount
		,ISNULL(A.DoneAmount,0),0 
		,1
		,ISNULL(A.DoneAmount,0),0 
		From #InvSellReksadana A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		Where ISNULL(A.DoneAmount,0) > 0




	END


END

--11.DEPOSITO PLACEMENT & ROLLOVER
BEGIN
	INSERT INTO #InvDepositoPlacement
		(   FundJournalPK ,
			FundCashRefPK,
			TrxType,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			DoneAmount 
		)
	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.FundCashRefPK,
			A.TrxType,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			B.ID InstrumentID ,
			A.FundPK ,
			DoneAmount 
	From Investment A  
	LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType in (1,3)
	and A.InstrumentTypePK in (5,10)
	And isnull(A.DoneAmount,0) > 0 and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc

	Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
	Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

	INSERT INTO #JournalHeader
					( FundJournalPK ,
						HistoryPK ,
						Status ,
						Notes ,
						PeriodPK ,
						ValueDate ,
						Type ,
						TrxNo ,
						TrxName ,
						Reference ,
						Description
					)

	Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
			,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),Case when A.TrxType = 1 Then 'PLACEMENT DEPOSIT: ' Else 'ROLLOVER DEPOSIT: ' END
			+ A.InstrumentID   
	From #InvDepositoPlacement A  
	LEFT JOIN #EDT B on A.FundPK = B.FundPK

	INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InvestmentTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,Case when A.TrxType = 1 Then 'PLACEMENT INV DEPOSIT: ' Else 'ROLLOVER INV DEPOSIT: ' END + A.InstrumentID,'D',A.DoneAmount
		,A.DoneAmount,0
		,1
		,A.DoneAmount,0
		From #InvDepositoPlacement A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,0,A.InstrumentPK
		,Case when A.TrxType = 1 Then 'PLACEMENT CASH DEPOSIT: ' Else 'ROLLOVER CASH DEPOSIT: ' END + A.InstrumentID,'C',A.DoneAmount
		,0,A.DoneAmount
		,1
		,0,A.DoneAmount
		From #InvDepositoPlacement A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status in(1,2)
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

END

--12.LIQUIDATE DEPOSITO
BEGIN
	INSERT INTO #InvDepositoLiquidate
		( FundJournalPK ,
			ValueDate ,
			SettlementDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			FundCashRefPK,
			BreakInterestPercent,
			InterestPaymentType,
			InterestDaysType,
			MaturityDate,
			InterestPercent,
			AcqDate,
			DoneVolume,
			DoneAmount,
			PaymentDate,
			PaymentModeOnMaturity
		)


	Select ROW_NUMBER() OVER	(ORDER BY A.InvestmentPK ASC) Number ,
			A.ValueDate ,
			SettlementDate ,
			Reference ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			A.FundCashRefPK,
			A.BreakInterestPercent,
			A.InterestPaymentType,
			A.InterestDaysType,
			A.MaturityDate,
			A.InterestPercent,
			A.AcqDate,
			A.DoneVolume,
			a.DoneAmount,
			[dbo].[FGetPaymentDateDeposito](A.AcqDate,A.MaturityDate,A.ValueDate,D.Priority),
			PaymentModeOnMaturity
	From Investment A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN MasterValue D on A.InterestPaymentType = D.Code and D.ID = 'InterestPaymentType' and D.Status in (1,2)
	Where A.StatusSettlement = 2 and A.Posted = 0 
	and A.ValueDate > @DateMinOne and A.ValueDate <= @ValueDate 
	and A.TrxType = 2
	and A.InstrumentTypePK in (5,10) and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc
	
	--T0
	BEGIN
		
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'T0 DEPOSIT LIQUIDATE: ' + A.InstrumentID   
		From #InvDepositoLiquidate A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,0,A.InstrumentPK
		,'CASH T0 DEPOSIT LIQUIDATE: ' + A.InstrumentID,'D',A.DoneAmount
		,A.DoneAmount,0
		,1
		,A.DoneAmount,0
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InvestmentTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV T0 DEPOSIT LIQUIDATE: ' + A.InstrumentID,'C',A.DoneAmount
		,0,A.DoneAmount
		,1
		,0,A.DoneAmount
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.DoneAmount,0) > 0

		

	END

	--Perhitungan Interest
	begin
		insert into #InterestDeposito (
				FundPK,
				InstrumentPK,
				BreakInterestPercent,
				MaturityDate,
				AcqDate,
				IncomeDepositoAmount,
				NewIncomeAmount
			)
		select A.FundPK,
			   A.instrumentPK,
			   A.BreakInterestPercent,
			   A.MaturityDate,
			   A.AcqDate,
			   case when A.InterestPaymentType in (7,13,16,19) then 
				   dbo.[FGetAccruedInterestDepositoActAct](A.PaymentDate,case when A.PaymentModeOnMaturity = 2 and D.IsHoliday = 1 then D.DT1 when A.PaymentModeOnMaturity = 3 and D.IsHoliday = 1 then D.DTM1 else A.ValueDate end,A.DoneVolume,A.InterestPercent,A.InterestDaysType) 
			   else 
				   dbo.[FGetAccruedInterestDepositoActAct](A.AcqDate,case when A.PaymentModeOnMaturity = 2 and D.IsHoliday = 1 then D.DT1 when A.PaymentModeOnMaturity = 3 and D.IsHoliday = 1 then D.DTM1 else A.ValueDate end,A.DoneVolume,A.InterestPercent,A.InterestDaysType) 
			   end,
			   case when A.InterestPaymentType in (7,13,16,19) then 
				   dbo.[FGetAccruedInterestDepositoActAct](A.PaymentDate,case when A.PaymentModeOnMaturity = 2 and D.IsHoliday = 1 then D.DT1 when A.PaymentModeOnMaturity = 3 and D.IsHoliday = 1 then D.DTM1 else A.ValueDate end,A.DoneVolume,A.BreakInterestPercent,A.InterestDaysType) 
			   else 
				   dbo.[FGetAccruedInterestDepositoActAct](A.AcqDate,case when A.PaymentModeOnMaturity = 2 and D.IsHoliday = 1 then D.DT1 when A.PaymentModeOnMaturity = 3 and D.IsHoliday = 1 then D.DTM1 else A.ValueDate end,A.DoneVolume,A.BreakInterestPercent,A.InterestDaysType) 
			   end
		from #InvDepositoLiquidate A
		left join FundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK and B.Date = @DateMinOne and B.Status = 2
		left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status in (1,2)
		left join ZDT_WorkingDays D on A.ValueDate = D.Date
		where isnull(A.BreakInterestPercent,0) >= 0

		update A set A.ARInterestDepositoAmount = @DepositoARPercent/100 * A.IncomeDepositoAmount
		, A.TaxDepositoAmount = @DepositoTaxPercent/100 * A.IncomeDepositoAmount
		, A.NewTaxAmount =  @DepositoTaxPercent/100 * NewIncomeAmount , a.NewInterestAmount = @DepositoARPercent/100 * NewIncomeAmount
		from #InterestDeposito A
	end
	
	--OLD BREAK INTEREST
	BEGIN
		
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
		

		INSERT INTO #JournalHeader
						( FundJournalPK ,
							HistoryPK ,
							Status ,
							Notes ,
							PeriodPK ,
							ValueDate ,
							Type ,
							TrxNo ,
							TrxName ,
							Reference ,
							Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'REVERSE OLD LIQUIDATE INTEREST: ' + A.InstrumentID   
		From #InvDepositoLiquidate A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK 
		and A.BreakInterestPercent = E.BreakInterestPercent and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.IncomeDepositoAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.IncomeInterestTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REVERSE INCOME OLD LIQUIDATE INTEREST: ' + A.InstrumentID,'D',E.IncomeDepositoAmount
		,E.IncomeDepositoAmount,0
		,1
		,E.IncomeDepositoAmount,0
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.IncomeDepositoAmount,0) > 0
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'REVERSE INTEREST OLD LIQUIDATE INTEREST: ' + A.InstrumentID,'C',E.ARInterestDepositoAmount
		,0,E.ARInterestDepositoAmount
		,1
		,0,E.ARInterestDepositoAmount
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.ARInterestDepositoAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,case when B.IncomeInterestTimeDeposit <> TaxExpenseInterestIncomeTimeDeposit then TaxExpenseInterestIncomeTimeDeposit else IncomeInterestTimeDeposit end,
		case when B.IncomeInterestTimeDeposit <> TaxExpenseInterestIncomeTimeDeposit then C.CurrencyPK else D.CurrencyPK end,A.FundPK,0,A.InstrumentPK
		,'TAX LIQUIDATE BREAK INTEREST: ' + A.InstrumentID,'C',E.TaxDepositoAmount
		,0,E.TaxDepositoAmount
		,1
		,0,E.TaxDepositoAmount
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount D ON B.IncomeInterestTimeDeposit = D.FundJournalAccountPK AND D.status IN (1,2)
		LEFT JOIN FundJournalAccount C ON B.TaxExpenseInterestIncomeTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK 
		and A.BreakInterestPercent = E.BreakInterestPercent and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.TaxDepositoAmount,0) > 0

	END

	--NEW BREAK INTEREST
	begin
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
		

		INSERT INTO #JournalHeader
						( FundJournalPK ,
							HistoryPK ,
							Status ,
							Notes ,
							PeriodPK ,
							ValueDate ,
							Type ,
							TrxNo ,
							TrxName ,
							Reference ,
							Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'LIQUIDATE NEW BREAK INTEREST: ' + A.InstrumentID   
		From #InvDepositoLiquidate A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		WHERE A.BreakInterestPercent > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InterestAccrTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR LIQUIDATE NEW BREAK INTEREST: ' + A.InstrumentID,'D',E.NewInterestAmount
		,E.NewInterestAmount,0
		,1
		,E.NewInterestAmount,0
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where A.BreakInterestPercent > 0 and isnull(E.NewInterestAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.IncomeInterestTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME LIQUIDATE NEW BREAK INTEREST: ' + A.InstrumentID,'C',E.NewIncomeAmount
		,0,E.NewIncomeAmount
		,1
		,0,E.NewIncomeAmount
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where A.BreakInterestPercent > 0 and isnull(E.NewInterestAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.TaxExpenseInterestIncomeTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX LIQUIDATE NEW BREAK INTEREST: ' + A.InstrumentID,'D',E.NewTaxAmount
		,E.NewTaxAmount,0
		,1
		,E.NewTaxAmount,0
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.TaxExpenseInterestIncomeTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where A.BreakInterestPercent > 0 and isnull(E.NewInterestAmount,0) > 0
		
		--BREAK INTEREST NEW AR TERHADAP BANK
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)


		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,5,B.EndDayTrailsPK,'TRANSACTION',isnull(A.Reference,''),'LIQUIDATE NEW BREAK INTEREST PAYMENT: ' + A.InstrumentID   
		From #InvDepositoLiquidate A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		WHERE A.BreakInterestPercent > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,0,A.InstrumentPK
		,'CASH LIQUIDATE BREAK INTEREST PAYMENT: ' + A.InstrumentID,'D',E.NewInterestAmount
		,E.NewInterestAmount,0
		,1
		,E.NewInterestAmount,0
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status in (1,2)
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where A.BreakInterestPercent > 0 and isnull(E.NewInterestAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INTEREST LIQUIDATE BREAK INTEREST PAYMENT: ' + A.InstrumentID,'C',E.NewInterestAmount
		,0,E.NewInterestAmount
		,1
		,0,E.NewInterestAmount
		From #InvDepositoLiquidate A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.BreakInterestPercent = E.BreakInterestPercent 
		and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where A.BreakInterestPercent > 0  and isnull(E.NewInterestAmount,0) > 0
	end
		
END

--13.DAILY INTEREST DEPOSITO
BEGIN
	INSERT INTO #FPDeposito
		( FundJournalPK ,
			ValueDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			InterestPaymentType,
			InterestDaysType,
			MaturityDate,
			InterestPercent,
			DoneVolume,
			DoneAmount,
			PaymentDate,
			PaymentModeOnMaturity,
			AcqDate
		)

	Select ROW_NUMBER() OVER (ORDER BY [Identity] ASC) Number ,ValueDate,Reference,InstrumentPK,InstrumentID,FundPK,InterestPaymentType,
	InterestDaysType,MaturityDate,InterestPercent,DoneVolume,DoneAmount,PaymentDate,PaymentModeOnMaturity,AcqDate
	from
	(
		Select A.[Identity],
				case when A.MaturityDate between A.Date and @ValueDate then case when A.PaymentModeOnMaturity = 1 then A.MaturityDate 
							when A.PaymentModeOnMaturity = 3 then A.Date else @ValueDate end else @ValueDate end ValueDate,
				'' Reference,
				A.InstrumentPK ,
				C.ID InstrumentID ,
				A.FundPK ,
				A.InterestPaymentType,
				A.InterestDaysType,
				A.MaturityDate,
				A.InterestPercent,
				A.Balance DoneVolume,
				a.Balance DoneAmount,
				A.Date PaymentDate,
				PaymentModeOnMaturity,
				A.AcqDate
		From FundPosition A  
		LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
		Where A.Status = 2 
		and A.date = @DateMinOne 
		and C.InstrumentTypePK in (5,10) and ((A.MaturityDate >= @ValueDate) or (A.MaturityDate > @DateMinOne and A.MaturityDate < @valuedate)) and A.FundPK in (select FundPK from #FundParam) 

		union all
		Select A.[Identity] ,
				A.Date,
				'' ,
				A.InstrumentPK ,
				C.ID InstrumentID ,
				A.FundPK ,
				A.InterestPaymentType,
				A.InterestDaysType,
				A.MaturityDate,
				A.InterestPercent,
				A.Balance,
				a.Balance,
				A.AcqDate,
				PaymentModeOnMaturity,
				A.AcqDate from FundPosition A  
		LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
		Where A.Status = 2 
		and A.date = @ValueDate and A.MaturityDate > @ValueDate and C.InstrumentTypePK in (5,10)  and A.FundPK in (select FundPK from #FundParam) 
		and A.AcqDate  in 
		(
			select D.valuedate from investment D where A.InstrumentPK = D.InstrumentPK AND A.FundPK = D.FundPK and D.valuedate > @DateMinOne and D.ValueDate < @valuedate and StatusSettlement  = 2 and D.InstrumentTypePK in (5,10)   and D.TrxType in (1,3)
		)
		and A.InstrumentPK not in 
		(  
			SELECT C.InstrumentPK FROM Investment C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and A.AcqDate = C.AcqDate and ValueDate = @ValueDate and StatusSettlement  = 2 and C.InstrumentTypePK in (5,10)  and C.TrxType in (1,3)
		)
	
	) Z
	order by Number asc

	--Perhitungan Daily Interest
	begin
		insert into #InterestDailyDeposito (
				FundPK,
				InstrumentPK,
				MaturityDate,
				AcqDate,
				IncomeDepositoAmount
			)
		select A.FundPK,
			   A.instrumentPK,
			   A.MaturityDate,
			   A.AcqDate,
			   dbo.[FGetAccruedInterestDepositoActAct](A.PaymentDate,A.ValueDate,A.DoneVolume,A.InterestPercent,A.InterestDaysType) 
		from #FPDeposito A

		update A set A.ARInterestDepositoAmount = @DepositoARPercent/100 * A.IncomeDepositoAmount, A.TaxDepositoAmount = @DepositoTaxPercent/100 * A.IncomeDepositoAmount
		from #InterestDailyDeposito A
	end

	--Journal Daily Interest Deposito
	begin
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
		

		INSERT INTO #JournalHeader
						( FundJournalPK ,
							HistoryPK ,
							Status ,
							Notes ,
							PeriodPK ,
							ValueDate ,
							Type ,
							TrxNo ,
							TrxName ,
							Reference ,
							Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,7,B.EndDayTrailsPK,'INTEREST DEPOSITO',isnull(A.Reference,''),'DAILY INTEREST DEPOSITO: ' + A.InstrumentID   
		From #FPDeposito A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.IncomeInterestTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME DAILY INTEREST DEPOSITO: ' + A.InstrumentID,'C',E.IncomeDepositoAmount
		,0,E.IncomeDepositoAmount
		,1
		,0,E.IncomeDepositoAmount
		From #FPDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDailyDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.IncomeDepositoAmount,0) > 0
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'DAILY INTEREST DEPOSITO: ' + A.InstrumentID,'D',E.ARInterestDepositoAmount
		,E.ARInterestDepositoAmount,0
		,1
		,E.ARInterestDepositoAmount,0
		From #FPDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDailyDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.IncomeDepositoAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,case when B.IncomeInterestTimeDeposit <> TaxExpenseInterestIncomeTimeDeposit then TaxExpenseInterestIncomeTimeDeposit else IncomeInterestTimeDeposit end,
		case when B.IncomeInterestTimeDeposit <> TaxExpenseInterestIncomeTimeDeposit then C.CurrencyPK else D.CurrencyPK end,A.FundPK,0,A.InstrumentPK
		,'TAX DAILY INTEREST DEPOSITO: ' + A.InstrumentID,'D',E.TaxDepositoAmount
		,E.TaxDepositoAmount,0
		,1
		,E.TaxDepositoAmount,0
		From #FPDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount D ON B.IncomeInterestTimeDeposit = D.FundJournalAccountPK AND D.status IN (1,2)
		LEFT JOIN FundJournalAccount C ON B.TaxExpenseInterestIncomeTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestDailyDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		Where isnull(E.IncomeDepositoAmount,0) > 0
	end

end

--14.PAYMENT DEPOSITO
BEGIN
	INSERT INTO #PaymentDeposito
		( FundJournalPK ,
			ValueDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			InterestPaymentType,
			InterestDaysType,
			MaturityDate,
			InterestPercent,
			DoneVolume,
			DoneAmount,
			PaymentDate,
			PaymentModeOnMaturity,
			AcqDate
		)


	Select ROW_NUMBER() OVER	(ORDER BY A.[Identity] ASC) Number ,
						case when A.PaymentModeOnMaturity = 1 then A.MaturityDate 
					when A.PaymentModeOnMaturity = 2 then @valuedate
						else dbo.fworkingday(@valuedate,-1) end,
			'' ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			A.InterestPaymentType,
			A.InterestDaysType,
			A.MaturityDate,
			A.InterestPercent,
			A.Balance,
			a.Balance,
			case when A.InterestPaymentType = 1 then A.AcqDate else dbo.[FGetPaymentDateDeposito](A.AcqDate,A.MaturityDate,A.Date,D.Priority) end,
			A.PaymentModeOnMaturity,
			A.AcqDate
	From FundPosition A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN MasterValue D on A.InterestPaymentType = D.Code and D.ID = 'InterestPaymentType' and D.Status in (1,2)
	left join Investment F on A.InstrumentPK = F.InstrumentPK and A.FundPK = F.FundPK and F.StatusSettlement = 2 and F.TrxType = 2
	Where A.Status = 2 
	and A.date = @DateMinOne 
	and C.InstrumentTypePK in (5,10)
	and case when A.InterestPaymentType = 1 then A.MaturityDate 
		when A.InterestPaymentType = 7  and A.MaturityDate = @valuedate then A.MaturityDate
			else dbo.[FGetPaymentDateDeposito](A.AcqDate,A.MaturityDate,@ValueDate,D.Priority) end > @DateMinOne 
	and case when A.InterestPaymentType = 1 then A.MaturityDate 
		when A.InterestPaymentType = 7  and A.MaturityDate = @valuedate then A.MaturityDate
			else dbo.[FGetPaymentDateDeposito](A.AcqDate,A.MaturityDate,@ValueDate,D.Priority) end <= @ValueDate
	and F.FundPk is null and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc


	--Perhitungan Interest PAYMENT
	begin
		insert into #InterestPaymentDeposito (
				FundPK,
				InstrumentPK,
				MaturityDate,
				AcqDate,
				IncomeDepositoAmount
			)
		select A.FundPK,
			   A.instrumentPK,
			   A.MaturityDate,
			   A.AcqDate,
			   dbo.[FGetAccruedInterestDepositoActAct](A.PaymentDate,A.ValueDate,A.DoneVolume,A.InterestPercent,A.InterestDaysType) 
		from #PaymentDeposito A

		update A set A.ARInterestDepositoAmount = @DepositoARPercent/100 * A.IncomeDepositoAmount, A.TaxDepositoAmount = @DepositoTaxPercent/100 * A.IncomeDepositoAmount
		from #InterestPaymentDeposito A 
	end

	--Journal AR Interest to bank
	begin
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,8,B.EndDayTrailsPK,'AR INTEREST TO BANK',isnull(A.Reference,''),'INSTRUMENT: ' + A.InstrumentID   
		From #PaymentDeposito A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		LEFT JOIN #InterestPaymentDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		where E.ARInterestDepositoAmount > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,0,A.InstrumentPK
		,'AR INTEREST TO BANK INSTRUMENT: ' + A.InstrumentID,'D',E.ARInterestDepositoAmount
		,E.ARInterestDepositoAmount,0
		,1
		,E.ARInterestDepositoAmount,0
		From #PaymentDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundPK = D.FundPK and D.Status = 2 and BitDefaultInvestment = 1
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestPaymentDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		where E.ARInterestDepositoAmount > 0
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InterestAccrTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR INTEREST TO BANK INSTRUMENT: ' + A.InstrumentID,'C',E.ARInterestDepositoAmount
		,0,E.ARInterestDepositoAmount
		,1
		,0,E.ARInterestDepositoAmount
		From #PaymentDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN #InterestPaymentDeposito E on A.FundPK = E.FundPK and A.InstrumentPK = E.InstrumentPK and A.MaturityDate = E.MaturityDate and A.AcqDate = E.AcqDate
		where E.ARInterestDepositoAmount > 0
	end

end

--15.DEPOSITO MATURED
BEGIN
	INSERT INTO #MaturedDeposito
		( FundJournalPK ,
			ValueDate ,
			Reference ,
			InstrumentPK ,
			InstrumentID ,
			FundPK ,
			DoneVolume
		)


	Select ROW_NUMBER() OVER	(ORDER BY A.[Identity] ASC) Number ,
			@ValueDate,
			'' ,
			A.InstrumentPK ,
			C.ID InstrumentID ,
			A.FundPK ,
			A.Balance
	From FundPosition A  
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN MasterValue D on A.InterestPaymentType = D.Code and D.ID = 'InterestPaymentType' and D.Status in (1,2)
	Where A.Status = 2 
	and A.date = @DateMinOne 
	and C.InstrumentTypePK in (5,10)
	and A.MaturityDate > @DateMinOne and A.MaturityDate <= @ValueDate and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc

	--JOURNAL MATURED (INV TO BANK)
	begin
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,8,B.EndDayTrailsPK,'INV DEPOSIT TO BANK',isnull(A.Reference,''),'INSTRUMENT: ' + A.InstrumentID   
		From #MaturedDeposito A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,0,A.InstrumentPK
		,'INV DEPOSIT TO BANK INSTRUMENT: ' + A.InstrumentID,'D',A.DoneVolume
		,A.DoneVolume,0
		,1
		,A.DoneVolume,0
		From #MaturedDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundPK = D.FundPK and D.Status = 2 and BitDefaultInvestment = 1
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		WHERE isnull(A.DoneVolume,0) > 0
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.InvestmentTimeDeposit,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INV DEPOSIT TO BANK INSTRUMENT: ' + A.InstrumentID,'C',A.DoneVolume
		,0,A.DoneVolume
		,1
		,0,A.DoneVolume
		From #MaturedDeposito A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentTimeDeposit = C.FundJournalAccountPK AND C.status IN (1,2)
		WHERE isnull(A.DoneVolume,0) > 0
	end

end

--16.REVAL
BEGIN
	IF Exists(              
    	Select top 1 ClosePricePK from ClosePrice where Status = 2 and date = @ValueDate              
    	)  
    	BEGIN
	INSERT INTO #RevalFundPositionDistinct
	Select Distinct A.FundPK,A.InstrumentPK,B.ID,B.InstrumentTypePK,isnull(B.ReksadanaTypePK,0) From (
		Select Distinct FundPK,InstrumentPK From FundPosition where status = 2 and Date = @ValueDate
		UNION ALL
		Select Distinct FundPK,InstrumentPK From FundPosition where status = 2 and Date = @DateMinOne
	)A LEFT JOIN Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
	Where B.InstrumentTypePK <> 5 and A.FundPK in (select FundPK from #FundParam) 
        END

	INSERT INTO #FundPositionRevalForJournal
        ( FundJournalPK ,
          FundPK ,
          InstrumentPK ,
		  InstrumentID,
		  InstrumentTypePK,
		  ReksadanaTypePK,
		  FinalAmount
        )
	Select ROW_NUMBER() OVER (ORDER BY A.FundPK,A.InstrumentPK ASC) 
	,A.FundPK,A.InstrumentPK,A.InstrumentID,A.InstrumentTypePK,A.ReksadanaTypePK
	,case when D.InstrumentPK is not null then SUM(isnull(B.MarketValue,0)-isnull(B.CostValue,0)-(isnull(C.MarketValue,0)-isnull(C.CostValue,0))) 
			When B.instrumentPK is null and C.instrumentPK is not null then SUM(isnull(C.MarketValue,0)-isnull(C.CostValue,0)) * -1 
				Else SUM(isnull(B.MarketValue,0) - isnull(C.MarketValue,0)) END
	
	From #RevalFundPositionDistinct A
	left join 
	(
	select InstrumentPK,FundPK, sum(MarketValue) MarketValue, sum(CostValue) CostValue from FundPosition 
	where Date = @ValueDate and Status = 2 
	group by InstrumentPK, FundPK
	) B on A.InstrumentPK = B.instrumentPK and A.FundPK = B.FundPK 
	left join 
	(
	select InstrumentPK,FundPK, sum(MarketValue) MarketValue, sum(CostValue) CostValue from FundPosition 
	where Date = @DateMinOne and Status = 2 
	group by InstrumentPK, FundPK
	) C on A.InstrumentPK = C.instrumentPK and A.FundPK = C.FundPK
	left join
	(
	   SELECT distinct InstrumentPK,FundPK FROM Investment A WHERE A.statussettlement  = 2 and A.valuedate = @valuedate 
	   --union all 
	   --SELECT distinct InstrumentPK,FundPK FROM CorporateActionResult A WHERE A.Date = @valuedate and A.Status = 2
	)D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
	Group By A.FundPK,A.InstrumentPK,A.InstrumentTypePK,B.instrumentPK, D.InstrumentPK , C.InstrumentPK, A.InstrumentID,A.ReksadanaTypePK


	Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
	Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

	INSERT INTO #JournalHeader
					( FundJournalPK ,
						HistoryPK ,
						Status ,
						Notes ,
						PeriodPK ,
						ValueDate ,
						Type ,
						TrxNo ,
						TrxName ,
						Reference ,
						Description
					)

	Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
			,@PeriodPK,@ValueDate,10,B.EndDayTrailsPK,'PORTFOLIO REVALUATION','','InstrumentID : ' + A.InstrumentID   
	From #FundPositionRevalForJournal A  
	LEFT JOIN #EDT B on A.FundPK = B.FundPK

	INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,Case When A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond 
				When A.InstrumentTypePK = 1 then B.RevaluationEquity
					When A.InstrumentTypePK = 4 then B.RevaluationRights
						When A.InstrumentTypePK = 16 then B.RevaluationWarrant
							When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 7 then B.RevaluationProtectedFund
								When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 6 then B.RevaluationPrivateEquityFund
									When A.InstrumentTypePK = 6 and A.ReksadanaTypePK not in (7,6) then B.RevaluationMutualFund END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'InstrumentID: ' + A.InstrumentID
		,Case when A.FinalAmount > 0 then 'D' else 'C' end
		,ABS(A.FinalAmount)
		,Case when A.FinalAmount > 0 then ABS(A.FinalAmount)  else 0 end 
		,Case when A.FinalAmount < 0 then ABS(A.FinalAmount)  else 0 end 
		,1
		,Case when A.FinalAmount > 0 then ABS(A.FinalAmount)  else 0 end 
		,Case when A.FinalAmount < 0 then ABS(A.FinalAmount)  else 0 end 
		From #FundPositionRevalForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case When A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond 
				When A.InstrumentTypePK = 1 then B.RevaluationEquity
					When A.InstrumentTypePK = 4 then B.RevaluationRights
						When A.InstrumentTypePK = 16 then B.RevaluationWarrant
							When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 7 then B.RevaluationProtectedFund
								When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 6 then B.RevaluationPrivateEquityFund
									When A.InstrumentTypePK = 6 and A.ReksadanaTypePK not in (7,6) then B.RevaluationMutualFund END = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.FinalAmount,0) <> 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,Case When A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.UnrealisedBond 
				When A.InstrumentTypePK = 1 then B.UnrealisedEquity
					When A.InstrumentTypePK = 4 then B.UnrealisedRights
						When A.InstrumentTypePK = 16 then B.UnrealisedWarrant
							When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 7 then B.UnrealisedProtectedFund
								When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 6 then B.UnrealisedPrivateEquityFund
									When A.InstrumentTypePK = 6 and A.ReksadanaTypePK not in (7,6) then B.UnrealisedMutualFund END
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'InstrumentID: ' + A.InstrumentID
		,Case when A.FinalAmount < 0 then 'D' else 'C' end
		,ABS(A.FinalAmount)
		,Case when A.FinalAmount < 0 then ABS(A.FinalAmount)  else 0 end 
		,Case when A.FinalAmount > 0 then ABS(A.FinalAmount)  else 0 end 
		,1
		,Case when A.FinalAmount < 0 then ABS(A.FinalAmount)  else 0 end 
		,Case when A.FinalAmount > 0 then ABS(A.FinalAmount)  else 0 end 
		From #FundPositionRevalForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON Case When A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.UnrealisedBond 
				When A.InstrumentTypePK = 1 then B.UnrealisedEquity
					When A.InstrumentTypePK = 4 then B.UnrealisedRights
						When A.InstrumentTypePK = 16 then B.UnrealisedWarrant
							When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 7 then B.UnrealisedProtectedFund
								When A.InstrumentTypePK = 6 and A.ReksadanaTypePK = 6 then B.UnrealisedPrivateEquityFund
									When A.InstrumentTypePK = 6 and A.ReksadanaTypePK not in (7,6) then B.UnrealisedMutualFund END = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.FinalAmount,0) <> 0


END

--17.ACCRUED DAILY FEE
BEGIN
	
	INSERT INTO #LastAUMYesterdayDistinct
	Select  max(date) Date,FundPK From CloseNAV 
	Where status = 2 and Date = @DateMinOne and FundPK in (select FundPK from #FundParam) 
	group by FundPK

	INSERT INTO #LastAUMYesterday
	Select  A.Date,A.FundPK
	,Case when  B.AUM < 0  then 0 else B.Aum END
	From #LastAUMYesterdayDistinct A 
	left join CloseNAV B on A.FundPK = B.FundPK and A.Date = B.Date and B.status in (1,2)



	INSERT INTO #CBESTTransaction
	(
		FundPK,
		TrxEquity,
		TrxCorpBond,
		TrxGovBond
	)
	SELECT FundPK,0,0,0 FROM #FundFeeHeader

	UPDATE A SET A.TrxEquity = isnull(B.TotalEquity,0) 
	,A.TrxCorpBond = isnull(C.TotalCorpBond,0) 
	,A.TrxGovBond = isnull(D.TotalGovBond,0) 
	From #CBESTTransaction A
	left join
	(
		SELECT COUNT(INVESTMENTPK) TotalEquity,FundPK FROM INVESTMENT A 
		WHERE StatusSettlement = 2 and ValueDate = @ValueDate  and InstrumentTypePK in (1,4,16) and A.FundPK in (select FundPK from #FundParam) 
		GROUP BY FundPK
	)B on A.FundPK = B.FundPK
	left join
	(
		SELECT COUNT(INVESTMENTPK) TotalCorpBond,FundPK FROM INVESTMENT A 
		WHERE StatusSettlement = 2 and ValueDate = @ValueDate  and InstrumentTypePK in (3) and A.FundPK in (select FundPK from #FundParam) 
		GROUP BY FundPK
	)C on A.FundPK = C.FundPK
	left join
	(
		SELECT COUNT(INVESTMENTPK) TotalGovBond,FundPK FROM INVESTMENT A 
		WHERE StatusSettlement = 2 and ValueDate = @ValueDate  and InstrumentTypePK in (2,9,12,13,14,15) and A.FundPK in (select FundPK from #FundParam) 
		GROUP BY FundPK
	)D on A.FundPK = D.FundPK

		
	INSERT INTO #DailyFeeForJournal
	(
		FundJournalPK ,
		FundID,
		FundPK ,
		MFeeAmount ,
		CustodiFeeAmount ,
		AuditFeeAmount ,
		SInvestFeeAmount ,
		MovementFeeAmount,
		OtherFeeOneAmount,
		OtherFeeTwoAmount,
		CBESTAmount
	)

	Select ROW_NUMBER() OVER (ORDER BY A.FundPK ASC) FundJournalPK,A.FundID
	,A.FundPK,[dbo].[Fgetmanagementfeefromfundfeesetup] (@ValueDate,A.FundPK) * @TotalDaysYesterdayTillValueDate MFeeAmount 
	,[dbo].FGetCustodiFeeFromCustodiFeeSetup  (@ValueDate,A.FundPK) * @TotalDaysYesterdayTillValueDate CustodiFeeAmount
	,Case When A.RoundingMode = 1 then CEILING(AuditFeeAmount)
			When A.RoundingMode = 2 then FLOOR(AuditFeeAmount)
				When A.RoundingMode = 3 then ROUND(AuditFeeAmount,DecimalPlaces)
		 END	* @TotalDaysYesterdayTillValueDate AuditFeeAmount
	,Case When A.BitSinvestFee = 1 then isnull(Case When A.RoundingMode = 1 then CEILING(B.AUM * A.SinvestFeePercent / 100 / Case when isnull(SInvestFeeDays,0) = 0 Then 1 else SInvestFeeDays END)
			When A.RoundingMode = 2 then FLOOR(B.AUM * A.SinvestFeePercent / 100 / Case when isnull(SInvestFeeDays,0) = 0 Then 1 else SInvestFeeDays END)
				When A.RoundingMode = 3 then ROUND(B.AUM * A.SinvestFeePercent / 100 / Case when isnull(SInvestFeeDays,0) = 0 Then 1 else SInvestFeeDays END,DecimalPlaces)
		 END	* @TotalDaysYesterdayTillValueDate,0) Else 0 END SInvestAmount
	,Case When A.RoundingMode = 1 then CEILING(MovementFeeAmount)
			When A.RoundingMode = 2 then FLOOR(MovementFeeAmount)
				When A.RoundingMode = 3 then ROUND(MovementFeeAmount,DecimalPlaces)  END	* @TotalDaysYesterdayTillValueDate MovementFeeAmount
	,[dbo].FGetOtherFeeOneFromCustodiFeeSetup  (@ValueDate,A.FundPK)	* @TotalDaysYesterdayTillValueDate OtherFeeOneAmount
	,[dbo].FGetOtherFeeTwoFromCustodiFeeSetup  (@ValueDate,A.FundPK)	* @TotalDaysYesterdayTillValueDate OtherFeeTwoAmount
	,isnull(CBESTEquityAmount * C.TrxEquity,0) + isnull(CBESTCorpBondAmount * C.TrxCorpBond,0) + isnull(CBESTGovBondAmount * C.TrxGovBond,0)
	From #FundFeeHeader A
	Left join #LastAUMYesterday B on A.FundPK = B.FundPK
	Left join #CBESTTransaction C on A.FundPK = C.FundPK

	Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
	Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

	INSERT INTO #JournalHeader
					( FundJournalPK ,
						HistoryPK ,
						Status ,
						Notes ,
						PeriodPK ,
						ValueDate ,
						Type ,
						TrxNo ,
						TrxName ,
						Reference ,
						Description
					)

	Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
			,@PeriodPK,@ValueDate,6,B.EndDayTrailsPK,'DAILY FEE','','FundID : ' + A.FundID   
	From #DailyFeeForJournal A  
	LEFT JOIN #EDT B on A.FundPK = B.FundPK

	INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.ManagementFeeExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'MANAGEMENT FEE FUND ID: ' + A.FundID,'D',A.MFeeAmount 
		,A.MFeeAmount,0
		,1
		,A.MFeeAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.ManagementFeeExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.MFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.PayableManagementFee
		,C.CurrencyPK,A.FundPK,0,0
		,'MANAGEMENT FEE FUND ID: ' + A.FundID,'C',A.MFeeAmount 
		,0,A.MFeeAmount
		,1
		,0,A.MFeeAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableManagementFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.MFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.CustodianFeeExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'CUSTODI FEE FUND ID: ' + A.FundID,'D',A.CustodiFeeAmount 
		,A.CustodiFeeAmount,0
		,1
		,A.CustodiFeeAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.CustodianFeeExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CustodiFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,B.PayableCustodianFee
		,C.CurrencyPK,A.FundPK,0,0
		,'CUSTODI FEE FUND ID: ' + A.FundID,'C',A.CustodiFeeAmount 
		,0,A.CustodiFeeAmount
		,1
		,0,A.CustodiFeeAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableCustodianFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CustodiFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.AuditFeeExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'AUDIT FEE FUND ID: ' + A.FundID,'D',A.AuditFeeAmount 
		,A.AuditFeeAmount,0
		,1
		,A.AuditFeeAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.AuditFeeExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.AuditFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,6,1,2
		,B.PayableAuditFee
		,C.CurrencyPK,A.FundPK,0,0
		,'AUDIT FEE FUND ID: ' + A.FundID,'C',A.AuditFeeAmount 
		,0,A.AuditFeeAmount
		,1
		,0,A.AuditFeeAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableAuditFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.AuditFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,7,1,2
		,B.MovementFeeExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'MOVEMENT FEE FUND ID: ' + A.FundID,'D',A.MovementFeeAmount 
		,A.MovementFeeAmount,0
		,1
		,A.MovementFeeAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.MovementFeeExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.MovementFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,8,1,2
		,B.PayableMovementFee
		,C.CurrencyPK,A.FundPK,0,0
		,'MOVEMENT FEE FUND ID: ' + A.FundID,'C',A.MovementFeeAmount 
		,0,A.MovementFeeAmount
		,1
		,0,A.MovementFeeAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableMovementFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.MovementFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,9,1,2
		,B.OtherFeeOneExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'OTHER FEE ONE FUND ID: ' + A.FundID,'D',A.OtherFeeOneAmount 
		,A.OtherFeeOneAmount,0
		,1
		,A.OtherFeeOneAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.OtherFeeOneExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.OtherFeeOneAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,10,1,2
		,B.PayableOtherFeeOne
		,C.CurrencyPK,A.FundPK,0,0
		,'OTHER FEE ONE FUND ID: ' + A.FundID,'C',A.OtherFeeOneAmount 
		,0,A.OtherFeeOneAmount
		,1
		,0,A.OtherFeeOneAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableOtherFeeOne = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.OtherFeeOneAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,11,1,2
		,B.OtherFeeTwoExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'OTHER FEE TWO FUND ID: ' + A.FundID,'D',A.OtherFeeTwoAmount 
		,A.OtherFeeTwoAmount,0
		,1
		,A.OtherFeeTwoAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.OtherFeeTwoExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.OtherFeeTwoAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,12,1,2
		,B.PayableOtherFeeTwo
		,C.CurrencyPK,A.FundPK,0,0
		,'OTHER FEE TWO FUND ID: ' + A.FundID,'C',A.OtherFeeTwoAmount 
		,0,A.OtherFeeTwoAmount
		,1
		,0,A.OtherFeeTwoAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableOtherFeeTwo = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.OtherFeeTwoAmount,0) > 0

	INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,13,1,2
		,B.CBESTExpense
		,C.CurrencyPK,A.FundPK,0,0
		,'CBEST FEE FUND ID: ' + A.FundID,'D',A.CBESTAmount
		,A.CBESTAmount,0
		,1
		,A.CBESTAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.CBESTExpense = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CBESTAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,14,1,2
		,B.PayableCBEST
		,C.CurrencyPK,A.FundPK,0,0
		,'CBEST FEE FUND ID: ' + A.FundID,'C',A.CBESTAmount 
		,0,A.CBESTAmount
		,1
		,0,A.CBESTAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableCBEST = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CBESTAmount,0) > 0

        INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,15,1,2
		,B.SInvestFee
		,C.CurrencyPK,A.FundPK,0,0
		,'SINVEST FEE FUND ID: ' + A.FundID,'D',A.SInvestFeeAmount
		,A.SInvestFeeAmount,0
		,1
		,A.SInvestFeeAmount,0
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.SInvestFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.SInvestFeeAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,16,1,2
		,B.PayableSInvestFee
		,C.CurrencyPK,A.FundPK,0,0
		,'SINVEST FEE FUND ID: ' + A.FundID,'C',A.SInvestFeeAmount 
		,0,A.SInvestFeeAmount
		,1
		,0,A.SInvestFeeAmount
		From #DailyFeeForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableSInvestFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.SInvestFeeAmount,0) > 0

END

--18.BANK INTEREST / INTEREST CURRENT GIRO
BEGIN

	INSERT INTO #LastBankInterestSetupDistinct
	select max(Date) Date,BankBranchPK
	from BankInterestSetup 
	where status = 2 and Date <= @ValueDate
	group by BankBranchPK,InterestPercent,InterestDays

	INSERT INTO #LastBankInterestSetup
	Select A.Date,A.BankBranchPK,isnull(B.InterestPercent,0),isnull(B.InterestDays,0),isnull(B.MinimumBalance,0) From #LastBankInterestSetupDistinct A
	left join BankInterestSetup B on A.Date = B.Date and A.BankBranchPK = B.BankBranchPK and B.status in (1,2)

	INSERT INTO #InterestGiroForJournal(FundJournalPK,A.FundPK,FundID,InterestAmount)
	Select ROW_NUMBER() OVER(ORDER BY A.FundPK ASC),A.FundPK,A.FundID,A.InterestAmount 
	From (	
		Select  A.FundPk,B.ID FundID,isnull(A.FundJournalAccountPK,@DefaultBankAccountPK) CashAcc
		,SUM(isnull(
				Case When dbo.FGetAccountFundJournalBalanceByFundPK(@DateMinOne,A.FundJournalAccountPK,A.FundPK) > isnull(D.MinimumBalance,E.MinimumBalance ) then
					Case When isnull(A.FundJournalAccountPK,0) = 0 then 0 else dbo.FGetAccountFundJournalBalanceByFundPK(@DateMinOne,A.FundJournalAccountPK,A.FundPK) 
											* isnull(D.InterestPercent,E.InterestPercent) / 100 / isnull(D.InterestDays,E.InterestDays) * @TotalDaysYesterdayTillValueDate END
												ELSE 0 END 
					,0)) InterestAmount
		From FundCashRef A 
		left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
		left join FundAccountingSetup C on A.FundPK = C.FundPK and C.status in (1,2)
		Left join #LastBankInterestSetup D on B.BankBranchPK = D.BankBranchPK 
		Left join #LastBankInterestSetup E on  D.BankBranchPK = 0
		where A.status in (1,2) and isnull(C.BitAccruedInterestGiroDaily,1) = 1 and isnull(A.bitdefaultinvestment,0) = 1 and A.Type = 3 and A.FundPK in (select FundPK from #FundParam) 
		Group by B.ID,isnull(A.FundJournalAccountPK,@DefaultBankAccountPK),A.FundPK
		HAVING SUM(isnull(
				Case When isnull(A.FundJournalAccountPK,0) = 0 then 0 else dbo.FGetAccountFundJournalBalanceByFundPK(@DateMinOne,A.FundJournalAccountPK,A.FundPK) 
										* isnull(D.InterestPercent,E.InterestPercent) / 100 / isnull(D.InterestDays,E.InterestDays) * @TotalDaysYesterdayTillValueDate END
					,0)) > 0
	)A

	
	Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
	Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

	INSERT INTO #JournalHeader
					( FundJournalPK ,
						HistoryPK ,
						Status ,
						Notes ,
						PeriodPK ,
						ValueDate ,
						Type ,
						TrxNo ,
						TrxName ,
						Reference ,
						Description
					)

	Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
			,@PeriodPK,@ValueDate,6,B.EndDayTrailsPK,'BANK DAILY INTEREST','','FundID : ' + A.FundID   
	From #InterestGiroForJournal A  
	LEFT JOIN #EDT B on A.FundPK = B.FundPK

	INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InterestAccrGiro
		,C.CurrencyPK,A.FundPK,0,0
		,'BANK DAILY INTEREST FEE FUND ID: ' + A.FundID,'D',A.InterestAmount 
		,A.InterestAmount,0
		,1
		,A.InterestAmount,0
		From #InterestGiroForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InterestAccrGiro = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount,0) > 0

		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)
			
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.IncomeInterestGiro
		,C.CurrencyPK,A.FundPK,0,0
		,'BANK DAILY INTEREST FEE FUND ID: ' + A.FundID,'C',A.InterestAmount 
		,0,A.InterestAmount
		,1
		,0,A.InterestAmount
		From #InterestGiroForJournal A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeInterestGiro = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InterestAmount,0) > 0

END

--19.PENDING SUBSCRIPTION
BEGIN

		INSERT INTO #LastFundFeeDistinct
		select isnull(max(Date),@valuedate) Date,A.FundPK,isnull(BitPendingSubscription,1)
		from Fund A
		left join FundFee B on A.FundPK = B.FundPK and B.status = 2 and Date <= @valuedate
		where  A.FundPK in (select FundPK from #FundParam) 
		group by A.FundPK,BitPendingSubscription


	INSERT INTO #PendingSubscription
		(	
			FundJournalPK ,
			ValueDate ,
			ClientSubscriptionPK ,
			FundClientPK ,
			FundClientName ,
			FundPK ,
			FundID ,
			FundCashRefPK ,
			CashAmount ,
			SubscriptionFeeAmount ,
			TotalCashAmount,
			BitFirstNav,
			BitPendingSubscription
		)

	Select ROW_NUMBER() OVER	(ORDER BY A.ClientSubscriptionPK ASC) Number ,
			A.ValueDate ,
			A.ClientSubscriptionPK ,
			A.FundClientPK ,
			D.Name ,
			A.FundPK ,
			B.ID ,
			A.CashRefPK ,
			A.CashAmount ,
			isnull(A.SubscriptionFeeAmount,0) SubscriptionFeeAmount,
			A.TotalCashAmount,
			case when B.IssueDate = @ValueDate then 1 else 0 end BitFirstNav,
			isnull(C.BitPendingSubscription,0) BitPendingSubscription

	From ClientSubscription A  
	LEFT JOIN dbo.Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN #LastFundFeeDistinct C on A.FundPK = C.FundPK
	LEFT JOIN dbo.FundClient D ON A.FundClientPK = D.FundClientPK AND D.status IN (1,2)
	Where A.status not in (3,4) and A.FundPK in (select FundPK from #FundParam) 
	and A.ValueDate = @ValueDate and A.Type not in (3,6,7)
	order by Number asc

	
	 --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,2,B.EndDayTrailsPK,'Pending Subscription',isnull(A.ClientSubscriptionPK,'')
				,'Pending Subscription T0 Fund Client : ' + A.FundClientName + ', Fund : ' + A.FundID    
		From #PendingSubscription A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		Where A.BitPendingSubscription = 1
	


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,isnull(C.FundJournalAccountPK,@DefaultBankAccountPK),D.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Cash Subscription Fund Client : ' + A.FundClientName + ', Fund : ' + A.FundID ,'D', isnull(A.CashAmount,0)
		,isnull(A.CashAmount,0),0 
		,1
		,isnull(A.CashAmount,0),0 
		From #PendingSubscription A  
		LEFT JOIN FundCashRef C on A.FundCashRefPK = C.FundCashRefPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = isnull(C.FundJournalAccountPK,@DefaultBankAccountPK) AND D.status IN (1,2)
		Where isnull(A.CashAmount,0) > 0
		and  A.BitPendingSubscription = 1


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,case when A.BitFirstNav = 1 then B.Subscription  
				when A.BitPendingSubscription = 1 then B.PendingSubscription 
					else B.Subscription end
		,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Subscription Fund Client : ' + A.FundClientName + ', Fund : ' + A.FundID ,'C'
		,isnull(CASE WHEN A.BitFirstNav = 1 THEN A.TotalCashAmount ELSE A.CashAmount END,0)
		,0,isnull(CASE WHEN A.BitFirstNav = 1 THEN A.TotalCashAmount ELSE A.CashAmount END,0)
		,1
		,0,isnull(CASE WHEN A.BitFirstNav = 1 THEN A.TotalCashAmount ELSE A.CashAmount END,0)
		From #PendingSubscription A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON 
			case when A.BitFirstNav = 1 then B.Subscription  
				when A.BitPendingSubscription = 1 then B.PendingSubscription 
					else B.Subscription end = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.TotalCashAmount,0) > 0
		and  A.BitPendingSubscription = 1


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.PayableSubscriptionFee,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Fee Subscription Fund Client : ' + A.FundClientName + ', Fund : ' + A.FundID ,'C',isnull(A.SubscriptionFeeAmount,0)
		,0,isnull(A.SubscriptionFeeAmount,0)
		,1
		,0,isnull(A.SubscriptionFeeAmount,0)
		From #PendingSubscription A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PayableSubscriptionFee = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.SubscriptionFeeAmount,0) > 0 AND A.BitFirstNav = 1
		and  A.BitPendingSubscription = 1 
		
	END



END

--20.DIVIDEN CASH
BEGIN
	Insert into #DividenCashSaham
		(
			FundJournalPK ,
			FundPK,
			FundID,
			InstrumentPK,
			InstrumentID,
			LastVolume,
			PaymentDate,
			RecordingDate,
			Hold,
			Earn,
			TaxDueDate,
			FundTypeInternal
		)
	Select  ROW_NUMBER() OVER (ORDER BY B.FundPK,B.InstrumentPK ASC) Number ,B.FundPK,D.ID,B.InstrumentPK,E.ID,
	B.Balance LastBalance,A.PaymentDate,A.RecordingDate,A.Hold,A.Earn,isnull(A.TaxDueDate,1) TaxDueDate,isnull(D.FundTypeInternal,1) FundTypeInternal
	From CorporateAction A 
	left join FundPosition B on A.InstrumentPK = B.InstrumentPK	and B.Date = @DateMinOne and B.status = 2
	--left join (
	--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
	--	, SettlementDate, ValueDate 
	--	from Investment where statusSettlement = 2
	--	and InstrumentTypePK = 1 and FundPK in (select FundPK from #FundParam) 
	--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
	--) C on C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK and C.ValueDate >= A.ValueDate
	left join Fund D on B.FundPK = D.FundPK and D.status in (1,2)
	left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.status in (1,2)
	where A.Type = 1 and A.Status = 2 and A.ExDate = @ValueDate and B.FundPK in (select FundPK from #FundParam) 
	order by Number asc

	Select @CorporateActionPKTemp = max(fundjournalPK) From #DividenCashSaham
	Select @CorporateActionPKTemp = isnull(@CorporateActionPKTemp,0)



	Insert into #DividenCashSaham
		(
			FundJournalPK ,
			FundPK,
			FundID,
			InstrumentPK,
			InstrumentID,
			LastVolume,
			PaymentDate,
			RecordingDate,
			Hold,
			Earn,
			TaxDueDate,
			FundTypeInternal
		)
	Select @CorporateActionPKTemp + ROW_NUMBER() OVER (ORDER BY B.FundPK,B.InstrumentPK ASC) Number ,isnull(B.FundPK,0),D.ID,isnull(B.InstrumentPK,0),E.ID,
	isnull(B.BalanceFromInv,0),A.PaymentDate,A.RecordingDate,A.Hold,A.Earn,isnull(A.TaxDueDate,1) TaxDueDate,isnull(D.FundTypeInternal,1) FundTypeInternal
	from CorporateAction A
	Left join (
		select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
		, SettlementDate, ValueDate 
		from Investment where statusSettlement = 2
		and InstrumentTypePK = 1 and FundPK in (select FundPK from #FundParam) 
		Group by InstrumentPK,FundPK,SettlementDate,ValueDate
	) B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK and B.ValueDate >= A.ValueDate
	left join #DividenCashSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
	left join Fund D on B.FundPK = D.FundPK and D.status in (1,2)
	left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.status in (1,2)
	where A.Type = 1 and A.Status = 2 and A.ExDate = @ValueDate and C.FundPK in (select FundPK from #FundParam) 
	and C.FundPK is null and C.InstrumentPK is null

	
	 --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	


		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,12,B.EndDayTrailsPK,'DIVIDEND','','DIVIDEND CASH CUM DATE, Instrument : ' + A.InstrumentID + ', Fund : ' + A.FundID    
		From #DividenCashSaham A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		where A.FundPK <> 0
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.ARDividend 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'AR DIVIDEND : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D'
		,isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END
		,isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END,0
		,1
		,isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END,0
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.ARDividend = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END > 0



		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.IncomeDividend 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INCOME DIVIDEND : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.LastVolume/A.Hold * A.Earn,0)
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0)
		,1
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0)
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.IncomeDividend  = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.LastVolume/A.Hold * A.Earn,0) > 0



	


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,TaxProvision 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX PROVISION : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100,0
		,1
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100,0
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON TaxProvision = C.FundJournalAccountPK AND C.status IN (1,2)
		Where A.FundTypeInternal <> 2 and isnull(A.LastVolume/A.Hold * A.Earn,0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
		,PayableTaxProvision 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'PAYABLE TAX PROVISION : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100
		,1
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxProvisionPercent/100
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON PayableTaxProvision = C.FundJournalAccountPK AND C.status IN (1,2)
		Where A.FundTypeInternal <> 2 and isnull(A.LastVolume/A.Hold * A.Earn,0) > 0



			INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,5,1,2
		,B.PrepaidTaxDividend 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX DIVIDEND : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100,0
		,1
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100,0
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PrepaidTaxDividend  = C.FundJournalAccountPK AND C.status IN (1,2)
		Where A.TaxDueDate <> 1 and isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100 > 0



	END

	-- TSETTLED
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	

		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,A.PaymentDate,12,B.EndDayTrailsPK,'DIVIDEND','','DIVIDEND CASH T-SETTLED, Instrument : ' + A.InstrumentID + ', Fund : ' + A.FundID    
		From #DividenCashSaham A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		where A.FundPK <> 0
	
		INSERT INTO #JournalDetail
						( FundJournalPK ,
						  AutoNo ,
						  HistoryPK ,
						  Status ,
						  FundJournalAccountPK ,
						  CurrencyPK ,
						  FundPK ,
						  FundClientPK ,
						  InstrumentPK ,
						  DetailDescription ,
						  DebitCredit ,
						  Amount ,
						  Debit ,
						  Credit ,
						  CurrencyRate ,
						  BaseDebit ,
						  BaseCredit
						)


		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,@DefaultBankAccountPK,D.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH T-SETTLED DIVIDEND : ' + A.InstrumentID,'D',isnull(A.LastVolume/A.Hold * A.Earn,0) - isnull(A.LastVolume/A.Hold * A.Earn,0) * F.TaxPercentageDividend/100  
		,isnull(A.LastVolume/A.Hold * A.Earn,0) - isnull(A.LastVolume/A.Hold * A.Earn,0) * F.TaxPercentageDividend/100,0
		,1
		,isnull(A.LastVolume/A.Hold * A.Earn,0) - isnull(A.LastVolume/A.Hold * A.Earn,0) * F.TaxPercentageDividend/100,0

		From #DividenCashSaham A  
		LEFT JOIN FundJournalAccount D ON D.FundJournalAccountPK = @DefaultBankAccountPK AND D.status IN (1,2)
		LEFT JOIN Fund E on A.FundPK = E.FundPK and E.status in (1,2)
		LEFT JOIN FundAccountingSetup F on A.FundPK = F.FundPK and F.status in (1,2)
		Where isnull(A.LastVolume/A.Hold * A.Earn,0) -  isnull(A.LastVolume/A.Hold * A.Earn,0) * F.TaxPercentageDividend/100 > 0


	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.ARDividend 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'PAYABLE TAX PROVISION : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END
		,1
		,0,isnull(A.LastVolume/A.Hold * A.Earn,0) - Case when A.TaxDueDate = 1 THEN 0 ELSE isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100  END
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON ARDividend = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.LastVolume/A.Hold * A.Earn,0) -  isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100 > 0
	



		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.PrepaidTaxDividend 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'TAX DIVIDEND : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100,0
		,1
		,isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100,0
		From #DividenCashSaham A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PrepaidTaxDividend  = C.FundJournalAccountPK AND C.status IN (1,2)
		Where A.TaxDueDate = 1 and isnull(A.LastVolume/A.Hold * A.Earn,0) * B.TaxPercentageDividend/100 > 0





	END



END

--21.EXERCISE
BEGIN
	Insert into #Exercise
		(
			FundJournalPK ,
			FundPK ,
			FundID ,
			InstrumentPK ,
			InstrumentID ,  
			InstrumentRightsPK ,        
			EquityAmount ,
			CashAmount ,
			RightsWarrantAmount
		)
	Select  ROW_NUMBER() OVER (ORDER BY A.FundPK,A.InstrumentPK ASC) Number ,A.FundPK,C.ID,A.InstrumentPK,D.ID,A.InstrumentRightsPK,
	B.AvgPrice * A.BalanceExercise
	,A.Price * A.BalanceExercise
	,(B.AvgPrice * A.BalanceExercise) - (A.Price * A.BalanceExercise) from Exercise A
	left join FundPosition B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.Status = 2 and B.Date = @valuedate
	left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
	left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
	where DistributionDate = @valuedate and A.status = 2 and A.FundPK in (select FundPK from #FundParam) 
	order by Number asc


	-- --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	


		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,12,B.EndDayTrailsPK,'EXERCISE','','EXERCISE , Instrument : ' + A.InstrumentID + ', Fund : ' + A.FundID    
		From #Exercise A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		where A.FundPK <> 0
	

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.InvestmentEquity 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'EXERCISE EQUITY : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.EquityAmount,0)
		,isnull(A.EquityAmount,0),0
		,1
		,isnull(A.EquityAmount,0),0
		From #Exercise A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.InvestmentEquity = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.EquityAmount,0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,@DefaultBankAccountPK 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'EXERCISE : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.CashAmount,0)
		,0,isnull(A.CashAmount,0)
		,1
		,0,isnull(A.CashAmount,0)
		From #Exercise A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON @DefaultBankAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.CashAmount,0) > 0


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,case when C.InstrumentTypePK = 4 then B.InvestmentRights else B.InvestmentWarrant end 
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'EXERCISE : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.RightsWarrantAmount,0)
		,0,isnull(A.RightsWarrantAmount,0)
		,1
		,0,isnull(A.RightsWarrantAmount,0)
		From #Exercise A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN Instrument C on A.InstrumentRightsPK = C.InstrumentPK and C.status in (1,2)
		LEFT JOIN FundJournalAccount D ON case when C.InstrumentTypePK = 4 then B.InvestmentRights else B.InvestmentWarrant end = D.FundJournalAccountPK AND D.status IN (1,2)

		Where isnull(A.RightsWarrantAmount,0) > 0

	END




END




--22.FUND POSITION ADJUSTMENT
BEGIN
	Insert into #FundPositionAdjustment
		(
			FundJournalPK ,
			FundPK ,
			FundID ,
			InstrumentPK ,
			InstrumentID ,  
			InstrumentTypePK ,    
			InvestmentAmount ,
			RevalAmount,
			TrxType
		)
	Select  ROW_NUMBER() OVER (ORDER BY A.FundPK,A.InstrumentPK ASC) Number ,A.FundPK,C.ID,A.InstrumentPK,D.ID,D.InstrumentTypePK,
	case when D.InstrumentTypePK in (2,3,8,9,11,13,14,15) then abs(A.Balance) * (B.AvgPrice/100) else abs(A.Balance) * B.AvgPrice end,
	case when D.InstrumentTypePK in (2,3,8,9,11,13,14,15) then sum(B.ClosePrice - B.AvgPrice)/100 * 	abs(A.Balance) else sum(B.ClosePrice - B.AvgPrice) * abs(A.Balance) end Reval, 2 from FundPositionAdjustment A
	left join FundPosition B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.Date = B.Date and B.status = 2
	left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
	left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
	where A.Date = @ValueDate and A.status = 2 and A.Balance < 0 and A.FundPK in (select FundPK from #FundParam) 
	group by A.FundPK,C.ID,A.InstrumentPK,D.ID,D.InstrumentTypePK,A.Balance,B.AvgPrice
	order by Number asc


	Insert into #FundPositionAdjustment
		(
			FundJournalPK ,
			FundPK ,
			FundID ,
			InstrumentPK ,
			InstrumentID ,  
			InstrumentTypePK ,    
			InvestmentAmount ,
			RevalAmount,
			TrxType
		)
	Select  ROW_NUMBER() OVER (ORDER BY A.FundPK,A.InstrumentPK ASC) Number ,A.FundPK,C.ID,A.InstrumentPK,D.ID,D.InstrumentTypePK,
	case when D.InstrumentTypePK in (2,3,8,9,11,13,14,15) then abs(A.Balance) * (A.Price/100) else abs(A.Balance) * A.Price end,
	case when D.InstrumentTypePK in (2,3,8,9,11,13,14,15) then abs(A.Balance) * (A.Price/100) else abs(A.Balance) * A.Price end Reval,1 from FundPositionAdjustment A
	left join FundPosition B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.Date = B.Date and B.status = 2
	left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
	left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
	where A.Date = @ValueDate and A.status = 2 and A.Balance > 0 and A.FundPK in (select FundPK from #FundParam) 
	group by A.FundPK,C.ID,A.InstrumentPK,D.ID,D.InstrumentTypePK,A.Balance,A.Price
	order by Number asc


	-- --T0
	BEGIN

		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)
	


		INSERT INTO #JournalHeader
						( FundJournalPK ,
						  HistoryPK ,
						  Status ,
						  Notes ,
						  PeriodPK ,
						  ValueDate ,
						  Type ,
						  TrxNo ,
						  TrxName ,
						  Reference ,
						  Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting End Day Trails No: ' + CAST(B.EndDayTrailsPK as nvarchar(15))
				,@PeriodPK,@ValueDate,12,B.EndDayTrailsPK,'FP ADJUSTMENT','','FP ADJUSTMENT , Instrument : ' + A.InstrumentID + ', Fund : ' + A.FundID    
		From #FundPositionAdjustment A  
		LEFT JOIN #EDT B on A.FundPK = B.FundPK
		where A.FundPK <> 0
	
		-- FP ADJUSTMENT SELL
		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,case when A.InstrumentTypePK in (1,4,16) then B.InvestmentEquity 
				when A.InstrumentTypePK in (6) then B.InvestmentMutualFund 
					when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.InvestmentBond
						else B.InvestmentBond end
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'INVESTMENT ' + case when A.InstrumentTypePK in (1,4,16) then 'EQUITY'
								when A.InstrumentTypePK in (6) then 'MUTUAL FUND'
									when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then 'BOND' else '' end + ' : ' 
		+ A.InstrumentID + ', Fund : ' + A.FundID ,'C',isnull(A.InvestmentAmount,0)
		,0,isnull(A.InvestmentAmount,0)
		,1
		,0,isnull(A.InvestmentAmount,0)
		From #FundPositionAdjustment A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON case when A.InstrumentTypePK in (1,4,16) then B.InvestmentEquity 
												when A.InstrumentTypePK in (6) then B.InvestmentMutualFund 
													when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.InvestmentBond
														else B.InvestmentBond end = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InvestmentAmount,0) > 0 and TrxType = 2


		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,@DefaultBankAccountPK
		,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
		,'CASH : ' + A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.InvestmentAmount,0)
		,isnull(A.InvestmentAmount,0),0
		,1
		,isnull(A.InvestmentAmount,0),0
		From #FundPositionAdjustment A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON @DefaultBankAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		Where isnull(A.InvestmentAmount,0) > 0 and TrxType = 2


		INSERT INTO #JournalDetail
					( FundJournalPK ,
					  AutoNo ,
					  HistoryPK ,
					  Status ,
					  FundJournalAccountPK ,
					  CurrencyPK ,
					  FundPK ,
					  FundClientPK ,
					  InstrumentPK ,
					  DetailDescription ,
					  DebitCredit ,
					  Amount ,
					  Debit ,
					  Credit ,
					  CurrencyRate ,
					  BaseDebit ,
					  BaseCredit
					)
			SELECT A.FundJournalPK + @FundJournalPKTemp,3,1,2
			,case when A.InstrumentTypePK in (1,4,16) then B.UnrealisedEquity 
				when A.InstrumentTypePK in (6) then B.UnrealisedMutualFund 
					when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.UnrealisedBond
						else B.UnrealisedBond end
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK,'INSTRUMENT: ' + A.InstrumentID
			,'C'
			,isnull(ABS(A.InvestmentAmount),0)
			,0,isnull(ABS(A.InvestmentAmount),0)
			,1
			,0,isnull(ABS(A.InvestmentAmount),0)
			FROM #FundPositionAdjustment A
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.Status = 2
			LEFT JOIN dbo.FundJournalAccount C ON C.FundJournalAccountPK = case when A.InstrumentTypePK in (1,4,16) then B.UnrealisedEquity 
																					when A.InstrumentTypePK in (6) then B.UnrealisedMutualFund 
																						when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.UnrealisedBond
																							else B.UnrealisedBond end  AND C.status IN (1,2)
			WHERE ISNULL(A.InvestmentAmount,0) <> 0 and TrxType = 2


			INSERT INTO #JournalDetail
					( FundJournalPK ,
					  AutoNo ,
					  HistoryPK ,
					  Status ,
					  FundJournalAccountPK ,
					  CurrencyPK ,
					  FundPK ,
					  FundClientPK ,
					  InstrumentPK ,
					  DetailDescription ,
					  DebitCredit ,
					  Amount ,
					  Debit ,
					  Credit ,
					  CurrencyRate ,
					  BaseDebit ,
					  BaseCredit
					)

			SELECT A.FundJournalPK + @FundJournalPKTemp,4,1,2
			,case when A.InstrumentTypePK in (1,4,16) then B.RevaluationEquity 
				when A.InstrumentTypePK in (6) then B.RevaluationMutualFund 
					when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond
						else B.RevaluationBond end
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK,'INSTRUMENT: ' + A.InstrumentID
			,'D'
			,isnull(ABS(A.InvestmentAmount),0)
			,isnull(ABS(A.InvestmentAmount),0),0
			,1
			,isnull(ABS(A.InvestmentAmount),0),0
			FROM #FundPositionAdjustment A
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.Status = 2
			LEFT JOIN dbo.FundJournalAccount C ON C.FundJournalAccountPK = case when A.InstrumentTypePK in (1,4,16) then B.RevaluationEquity 
																					when A.InstrumentTypePK in (6) then B.RevaluationMutualFund 
																						when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond
																							else B.RevaluationBond end AND C.status IN (1,2)
			WHERE ISNULL(A.InvestmentAmount,0) <> 0 and TrxType = 2



			-- FP ADJUSTMENT BUY
			INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
			Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
			,case when A.InstrumentTypePK in (1,4,16) then B.InvestmentEquity 
					when A.InstrumentTypePK in (6) then B.InvestmentMutualFund 
						when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.InvestmentBond
							else B.InvestmentBond end
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK
			,'INVESTMENT ' + case when A.InstrumentTypePK in (1,4,16) then 'EQUITY'
									when A.InstrumentTypePK in (6) then 'MUTUAL FUND'
										when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then 'BOND' else '' end + ' : ' 
			+ A.InstrumentID + ', Fund : ' + A.FundID ,'D',isnull(A.InvestmentAmount,0)
			,isnull(A.InvestmentAmount,0),0
			,1
			,isnull(A.InvestmentAmount,0),0
			From #FundPositionAdjustment A  
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
			LEFT JOIN FundJournalAccount C ON case when A.InstrumentTypePK in (1,4,16) then B.InvestmentEquity 
													when A.InstrumentTypePK in (6) then B.InvestmentMutualFund 
														when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.InvestmentBond
															else B.InvestmentBond end = C.FundJournalAccountPK AND C.status IN (1,2)
			Where isnull(A.InvestmentAmount,0) > 0 and TrxType = 1



			INSERT INTO #JournalDetail
					( FundJournalPK ,
					  AutoNo ,
					  HistoryPK ,
					  Status ,
					  FundJournalAccountPK ,
					  CurrencyPK ,
					  FundPK ,
					  FundClientPK ,
					  InstrumentPK ,
					  DetailDescription ,
					  DebitCredit ,
					  Amount ,
					  Debit ,
					  Credit ,
					  CurrencyRate ,
					  BaseDebit ,
					  BaseCredit
					)

			SELECT A.FundJournalPK + @FundJournalPKTemp,2,1,2
			,case when A.InstrumentTypePK in (1,4,16) then B.RevaluationEquity 
				when A.InstrumentTypePK in (6) then B.RevaluationMutualFund 
					when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond
						else B.RevaluationBond end
			,C.CurrencyPK,A.FundPK,0,A.InstrumentPK,'INSTRUMENT: ' + A.InstrumentID
			,'C'
			,isnull(ABS(A.InvestmentAmount),0)
			,0,isnull(ABS(A.InvestmentAmount),0)
			,1
			,0,isnull(ABS(A.InvestmentAmount),0)
			FROM #FundPositionAdjustment A
			LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.Status = 2
			LEFT JOIN dbo.FundJournalAccount C ON C.FundJournalAccountPK = case when A.InstrumentTypePK in (1,4,16) then B.RevaluationEquity 
																					when A.InstrumentTypePK in (6) then B.RevaluationMutualFund 
																						when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then B.RevaluationBond
																							else B.RevaluationBond end AND C.status IN (1,2)
			WHERE ISNULL(A.InvestmentAmount,0) <> 0 and TrxType = 1

	END

END




--Select * From #FundFeeHeader
--Select * From #DailyFeeForJournal
--Select * From #AvgEquity
--SELECT * FROM #JournalHeader order by FundJournalPK desc
--SELECT * FROM #JournalDetail  order by FundJournalPK Desc, AutoNo Asc

--SELECT * FROM #JournalDetail where DetailDescription like '%BOND%' and instrumentPK > 0  order by FundJournalPK Desc, AutoNo Asc

--SELECT * FROM #JournalDetail where fundPK = 5 order by FundJournalPK Desc, AutoNo Asc


--Select FundJournalPK,Sum(Debit-Credit) Amount From #JournalDetail
--group by FundJournalPK
--having Sum(Debit-Credit) <> 0

--Select * From #JournalDetail where FundJOurnalPK = 160 order by autoNo asc
--Select * From #JournalDetail where FundPK = 12 and InstrumentPK = 1073 order by FundjournalPK, autoNo asc



--
--SELECT A.FundJournalPK,B.AutoNo,A.TrxName,A.Description,C.Name,B.DetailDescription,B.DebitCredit,B.Amount,B.BaseDebit,B.BaseCredit FROM #JournalHeader A
--left join #JournalDetail B on A.FundJournalPK = B.FundJournalPK
--left join FundJournalAccount C on B.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
--where B.FundPK = 39
--and C.Name in ('INTEREST RECEIVABLE (ACCRUAL) - TIME DEPOSIT')
----and A.TrxName = 'INTEREST DEPOSITO'
--order by C.Name


-- CEK DAILY FEE
--SELECT A.FundJournalPK,B.AutoNo,A.TrxName,A.Description,C.Name,B.DetailDescription,B.DebitCredit,B.Amount,B.BaseDebit,B.BaseCredit FROM #JournalHeader A
--left join #JournalDetail B on A.FundJournalPK = B.FundJournalPK
--left join FundJournalAccount C on B.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
--where B.FundPK = 21

--and A.TrxName = 'DAILY FEE'
--order by C.Name


-- CEK TRANSAKSI
--SELECT A.FundJournalPK,B.AutoNo,A.TrxName,A.Description,C.Name,B.DetailDescription,B.DebitCredit,B.Amount,B.BaseDebit,B.BaseCredit FROM #JournalHeader A
--left join #JournalDetail B on A.FundJournalPK = B.FundJournalPK
--left join FundJournalAccount C on B.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
--where B.FundPK = 21

--and A.Reference = '647/INV/0220'
--order by C.Name


-- CEK REVAL
--SELECT A.FundJournalPK,B.AutoNo,A.TrxName,A.Description,C.Name,B.DetailDescription,B.DebitCredit,B.Amount,B.BaseDebit,B.BaseCredit FROM #JournalHeader A
--left join #JournalDetail B on A.FundJournalPK = B.FundJournalPK
--left join FundJournalAccount C on B.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
--where B.FundPK = 21

--and C.Name in ('INVESTMENT - BOND','UNREALISED P/L - BOND')
--and TrxName = 'PORTFOLIO REVALUATION'
--order by C.Name
--order by A.FundJournalPK,B.AutoNo

declare @MaxFundJournalPK int

select @MaxFundJournalPK = max(FundJournalPK) from FundJournal
Set @MaxFundJournalPK = isnull(@MaxFundJournalPK ,1)


Insert Into FundJournal(FundJournalPK,HistoryPK,Status,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
Posted,PostedBy,PostedTime,EntryUsersID,EntryTime,LastUpdate)

select 	@MaxFundJournalPK + FundJournalPK,1,2,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
1,@UsersID,@Lastupdate,@UsersID,@Lastupdate,@Lastupdate
from #JournalHeader


Insert Into FundJournalDetail(FundJournalPK,AutoNo,HistoryPK,Status,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,LastUsersID,LastUpdate)

select 	@MaxFundJournalPK + FundJournalPK,AutoNo,HistoryPK,2,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,@UsersID,@LastUpdate
from #JournalDetail

--select * from #JournalHeader
--select * from #JournalDetail


Select @maxEndDayTrailsPK LastPK  
                             
                             
                        ";
						cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
						cmd.Parameters.AddWithValue("@UsersID", _usersID);
						cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
						cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								dr.Read();
								return Convert.ToInt32(dr["LastPK"]);

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







	}
}