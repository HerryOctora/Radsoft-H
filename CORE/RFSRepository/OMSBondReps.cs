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
using System.Data;
using Excel.FinancialFunctions;

namespace RFSRepository
{
    public class OMSBondReps
    {
        Host _host = new Host();

        private OMSBond setOMSBond(SqlDataReader dr)
        {
            OMSBond M_temp = new OMSBond();
            M_temp.Name = Convert.ToString(dr["Name"]);
            M_temp.CurrBalance = Convert.ToDecimal(dr["CurrBalance"]);
            M_temp.CurrNAVPercent = Convert.ToDecimal(dr["CurrNAVPercent"]);
            M_temp.Movement = Convert.ToDecimal(dr["Movement"]);
            M_temp.AfterBalance = Convert.ToDecimal(dr["AfterBalance"]);
            M_temp.AfterNAVPercent = Convert.ToDecimal(dr["AfterNAVPercent"]);
            M_temp.MovementTOne = Convert.ToDecimal(dr["MovementTOne"]);
            M_temp.AfterTOne = Convert.ToDecimal(dr["AfterTOne"]);
            M_temp.AfterTOneNAVPercent = Convert.ToDecimal(dr["AfterTOneNAVPercent"]);
            M_temp.MovementTTwo = Convert.ToDecimal(dr["MovementTTwo"]);
            M_temp.AfterTTwo = Convert.ToDecimal(dr["AfterTTwo"]);
            M_temp.AfterTTwoNAVPercent = Convert.ToDecimal(dr["AfterTTwoNAVPercent"]);
            M_temp.MovementTThree = Convert.ToDecimal(dr["MovementTThree"]);
            M_temp.AfterTThree = Convert.ToDecimal(dr["AfterTThree"]);
            M_temp.AfterTThreeNAVPercent = Convert.ToDecimal(dr["AfterTThreeNAVPercent"]);
            return M_temp;
        }

        public List<OMSBond> OMSBond_PerFund(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSBond> L_OMSBond = new List<OMSBond>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                   
Declare @EndDayTrailsFundPortfolioPK int
  
Declare @MaxDateEndDayFP datetime
Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
Select max(ValueDate) from endDayTrailsFundPortfolio where
valuedate < @Date  and status = 2 and FundPK = @FundPK
)
and status = 2  and FundPK = @FundPK


Declare @DateT1 datetime  
Declare @DateT2 datetime  
Declare @DateT3 datetime 
  
set @DateT1 = dbo.FWorkingDay(@Date,1)  
set @DateT2 = dbo.FWorkingDay(@Date,2)  
set @DateT3 = dbo.FWorkingDay(@Date,3) 

  
Declare @AUM numeric(22,4)  
Select @AUM = AUM From CloseNav where 
Date = (
select max(Date) from closeNAV where status = 2 and Date < @Date and FundPK = @FundPK
)
and FundPK = @FundPK  and status = 2
  
set @AUM = isnull(@AUM,1)

IF @Aum = 0
BEGIN
set @AUM = 1
END

Create table #tmpOmsBondPerFundBalance
(
Name nvarchar(200) COLLATE DATABASE_DEFAULT,
CurrBalance decimal(22,4),
CurrNAVPercent decimal(22,4),
Movement decimal(22,4),
AfterBalance decimal(22,4),
AfterNAVPercent decimal(22,4),
MovementTOne decimal(22,4),
AfterTOne decimal(22,4),
AfterTOneNAVPercent decimal(22,4),
MovementTTwo decimal(22,4),
AfterTTwo decimal(22,4),
AfterTTwoNAVPercent decimal(22,4),
MovementTThree numeric(22,4),
AfterTThree numeric(22,4),
AfterTThreeNAVPercent numeric(18,6),
)             
	
insert into #tmpOmsBondPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)       
  
Select  B.ID,sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) CurrBalance, 
isnull(sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End))/@AUM * 100,0) [CurrNAVPercent],  
isnull(D.Movement,0) Movement,
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  AfterBalance,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)) / @AUM * 100,0) AfterNAVPercent,  
   
isnull(F.Movement,0)  MovementTOne,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0) + isnull(F.Movement,0)  AfterTOne,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)+ isnull(F.Movement,0) ) / @AUM * 100,0) AfterTOneNAVPercent,  
   
isnull(H.Movement,0)  MovementTTwo,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0) + isnull(F.Movement,0)  + isnull(H.Movement,0)  AfterTTwo,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0)  + isnull(H.Movement,0) ) / @AUM * 100,0) AfterTTwoNAVPercent,

isnull(N.Movement,0)  MovementTThree,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0)  + isnull(H.Movement,0)  + isnull(N.Movement,0)  AfterTThree,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0) + isnull(H.Movement,0) + isnull(N.Movement,0) ) / @AUM * 100,0) AfterTThreeNAVPercent    
from [FundPosition] A  
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate between @MaxDateEndDayFP and @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  

left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @fundpk  
and A.InstrumentTypePK in (2,3,8,9,13,15)  and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)F on B.InstrumentPK = F.InstrumentPK  

left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)H on B.InstrumentPK = H.InstrumentPK  

left join -- T3 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)N on B.InstrumentPK = N.InstrumentPK  

where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
and J.Type in (2,5,14)
Group by B.ID,D.Movement,F.Movement,H.Movement,N.Movement




insert into #tmpOmsBondPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)   
Select B.ID,0,0,
isnull(J.Movement,0) , 
isnull(J.Movement,0) ,
isnull(J.Movement ,0) / @AUM * 100,
isnull(D.Movement,0) ,
isnull(J.Movement,0) + isnull(D.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) ,0) / @AUM * 100,
isnull(E.Movement,0) ,
isnull(J.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) + isnull(E.Movement,0) ,0) / @AUM * 100,
isnull(I.Movement,0),
isnull(J.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,0) / @AUM * 100
From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)J on B.InstrumentPK = J.InstrumentPK  
left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)E on B.InstrumentPK = E.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)I on B.InstrumentPK = I.InstrumentPK  

where ValueDate between @MaxDateEndDayFP and @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsBondPerFundBalance	)
Group By B.ID,D.Movement,E.Movement,I.Movement,J.Movement

Select * from #tmpOmsBondPerFundBalance";


                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                   
Declare @EndDayTrailsFundPortfolioPK int
  
Declare @MaxDateEndDayFP datetime
Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
Select max(ValueDate) from endDayTrailsFundPortfolio where
valuedate < @Date  and status = 2
)
and status = 2


Declare @DateT1 datetime  
Declare @DateT2 datetime  
Declare @DateT3 datetime 
  
set @DateT1 = dbo.FWorkingDay(@Date,1)  
set @DateT2 = dbo.FWorkingDay(@Date,2)  
set @DateT3 = dbo.FWorkingDay(@Date,3) 

  
Declare @AUM numeric(22,4)  
Select @AUM = AUM From CloseNav where 
Date = (
select max(Date) from closeNAV where status = 2 and Date < @Date and FundPK = @FundPK
)
and FundPK = @FundPK  and status = 2
  
set @AUM = isnull(@AUM,1)

IF @Aum = 0
BEGIN
set @AUM = 1
END

Create table #tmpOmsBondPerFundBalance
(
Name nvarchar(200) COLLATE DATABASE_DEFAULT,
CurrBalance decimal(22,4),
CurrNAVPercent decimal(22,4),
Movement decimal(22,4),
AfterBalance decimal(22,4),
AfterNAVPercent decimal(22,4),
MovementTOne decimal(22,4),
AfterTOne decimal(22,4),
AfterTOneNAVPercent decimal(22,4),
MovementTTwo decimal(22,4),
AfterTTwo decimal(22,4),
AfterTTwoNAVPercent decimal(22,4),
MovementTThree numeric(22,4),
AfterTThree numeric(22,4),
AfterTThreeNAVPercent numeric(18,6),
)             
	
insert into #tmpOmsBondPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)       
  
Select  B.ID,sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) CurrBalance, 
isnull(sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End))/@AUM * 100,0) [CurrNAVPercent],  
isnull(D.Movement,0) Movement,
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  AfterBalance,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)) / @AUM * 100,0) AfterNAVPercent,  
   
isnull(F.Movement,0)  MovementTOne,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0) + isnull(F.Movement,0)  AfterTOne,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)+ isnull(F.Movement,0) ) / @AUM * 100,0) AfterTOneNAVPercent,  
   
isnull(H.Movement,0)  MovementTTwo,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0) + isnull(F.Movement,0)  + isnull(H.Movement,0)  AfterTTwo,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0)  + isnull(H.Movement,0) ) / @AUM * 100,0) AfterTTwoNAVPercent,

isnull(N.Movement,0)  MovementTThree,  
sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0)  + isnull(H.Movement,0)  + isnull(N.Movement,0)  AfterTThree,  
isnull((sum(Balance * (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End)) + isnull(D.Movement,0)  + isnull(F.Movement,0) + isnull(H.Movement,0) + isnull(N.Movement,0) ) / @AUM * 100,0) AfterTThreeNAVPercent    
from [FundPosition] A  
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate between @MaxDateEndDayFP and @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  

left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @fundpk  
and A.InstrumentTypePK in (2,3,8,9,13,15)  and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)F on B.InstrumentPK = F.InstrumentPK  

left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)H on B.InstrumentPK = H.InstrumentPK  

left join -- T3 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)N on B.InstrumentPK = N.InstrumentPK  

where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
and J.Type in (2,5,14)
Group by B.ID,D.Movement,F.Movement,H.Movement,N.Movement




insert into #tmpOmsBondPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)   
Select B.ID,0,0,
isnull(J.Movement,0) , 
isnull(J.Movement,0) ,
isnull(J.Movement ,0) / @AUM * 100,
isnull(D.Movement,0) ,
isnull(J.Movement,0) + isnull(D.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) ,0) / @AUM * 100,
isnull(E.Movement,0) ,
isnull(J.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) + isnull(E.Movement,0) ,0) / @AUM * 100,
isnull(I.Movement,0),
isnull(J.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,
isnull(J.Movement + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,0) / @AUM * 100
From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)J on B.InstrumentPK = J.InstrumentPK  
left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)E on B.InstrumentPK = E.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)I on B.InstrumentPK = I.InstrumentPK  

where ValueDate between @MaxDateEndDayFP and @Date and FundPK = @FundPK  
and A.InstrumentTypePK in (2,3,8,9,13,15) and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsBondPerFundBalance	)
Group By B.ID,D.Movement,E.Movement,I.Movement,J.Movement

Select * from #tmpOmsBondPerFundBalance";


                        }




                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_OMSBond.Add(setOMSBond(dr));
                                }
                            }
                            return L_OMSBond;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<OMSBondByInstrument> OMSBondByInstrument(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSBondByInstrument> L_model = new List<OMSBondByInstrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                     
                         Declare @TotalMarketValue numeric(26,6)

                        select @TotalMarketValue = aum From closeNav
                        where Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                        and status = 2


                        Create table #OMSBondByAllInstrument
                        (
                        InstrumentTypeID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
                        Balance numeric(18,0),
                        AvgPrice numeric(18,6),
                        CostValue numeric(22,4),
                        ClosePrice numeric(18,4),
                        LastPrice numeric(18,4),
                        NextCouponDate datetime,
                        MaturityDate datetime,
                        Status int
                        )
                        	
                        Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,NextCouponDate,MaturityDate,Status)
                        Select F.ID,A.InstrumentID,C.ID,
                        A.balance + isnull(
                        case when D.TrxType = 1 then isnull(D.DoneVolume,0) else
                        case when D.trxType = 2 then D.DoneVolume * -1 end end ,0)
                        + isnull(
                        case when E.TrxType = 1 then E.DoneVolume else
                        case when E.trxType = 2 then E.DoneVolume * -1 end end ,0)
                        ,
                        A.AvgPrice,A.Balance * A.AvgPrice CostValue,
                        [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0)
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        -- sisi buy dulu
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (2,3,8,9,13,15)
                        and TrxType = 1 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )D on A.InstrumentPK = D.InstrumentPK
                        -- sisi Sell
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (2,3,8,9,13,15)
                        and TrxType = 2 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )E on A.InstrumentPK = E.InstrumentPK
                        left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,NextCouponDate,MaturityDate,Status)
                        Select D.ID,B.ID,C.ID,   
                        case when A.TrxType = 1 then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,
                        case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
                        case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
                        end end,	[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.NextCouponDate,A.MaturityDate ,1 
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and orderstatus = 'M' and A.instrumentTypePK in (2,3,8,9,13,15)
                        and FundPK = @FundPK and A.TrxType = 1
                        and B.ID Not in 
                        (
                        select instrumentID From #OMSBondByAllInstrument
                        )
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )

                        union all

                        Select D.ID,B.ID,C.ID,   
                        case when A.TrxType = 1 then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,
                        case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
                        case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
                        end end,	[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice ,A.NextCouponDate,A.MaturityDate,1 
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        where ValueDate = @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
                        and FundPK = @FundPK and A.TrxType = 2
                        and B.ID Not in 
                        (
                        select instrumentID From #OMSBondByAllInstrument
                        )
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )


                        Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,NextCouponDate,MaturityDate,Status)
                        Select F.ID,A.InstrumentID,C.ID,
                        A.balance + isnull(
                        case when D.TrxType = 1 then D.DoneVolume else
                        case when D.trxType = 2 then D.DoneVolume * -1 end end,0)
                        + isnull(
                        case when E.TrxType = 1 then E.DoneVolume else
                        case when E.trxType = 2 then E.DoneVolume * -1 end end,0)
                        ,
                        A.AvgPrice,A.Balance * A.AvgPrice CostValue,
                        [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        G.ClosePriceValue LastPrice,dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0)
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        left join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.Status = 2
                        -- sisi buy dulu
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (2,3,8,9,13,15)
                        and TrxType = 1 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )D on A.InstrumentPK = D.InstrumentPK
                        -- sisi Sell
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (2,3,8,9,13,15)
                        and TrxType = 2 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )E on A.InstrumentPK = E.InstrumentPK
                        left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2
                        and B.InstrumentPK  in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )

                        
                        
                        Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,NextCouponDate,MaturityDate,Status)
                        Select D.ID,B.ID,C.ID,   
                        case when A.TrxType = 1 then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,
                        case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
                        case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
                        end end,	[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.NextCouponDate,A.MaturityDate ,1 
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and orderstatus = 'M' and A.instrumentTypePK in (2,3,8,9,13,15)
                        and FundPK = @FundPK and A.TrxType = 1
                        and B.ID Not in 
                        (
                        select instrumentID From #OMSBondByAllInstrument
                        )
                        and B.InstrumentPK  in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )

                        union all

                        Select D.ID,B.ID,C.ID,   
                        case when A.TrxType = 1 then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,
                        case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
                        case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
                        end end,	[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.NextCouponDate,A.MaturityDate ,1 
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        where ValueDate = @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
                        and FundPK = @FundPK and A.TrxType = 2
                        and B.ID Not in 
                        (
                        select instrumentID From #OMSBondByAllInstrument
                        )
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
             



                        Select InstrumentTypeID,InstrumentID,SectorID,isnull(sum(Balance * (LastPrice/100)) / @TotalMarketValue * 100,0) CurrentExposure  ,
                        sum(Balance) Volume,avg(AvgPrice) AvgPrice,sum(Balance * (AvgPrice/100)) Cost,
                        Avg(ClosePrice) ClosePrice,Avg(LastPrice) LastPrice,
                        ((Avg(LastPrice) / Avg(case when ClosePrice = 0 then 1 else closeprice end)) - 1) * 100 PriceDifference,sum(Balance * (LastPrice/100)) MarketValue,
                        (sum(Balance * (LastPrice/100))) - (sum(Balance * (AvgPrice/100))) Unrealized, 
                        ((sum(Balance * (LastPrice/100))) / (sum(Balance * case when AvgPrice = 0 then 1 else (AvgPrice/100) end)) -1) * 100 GainLoss,NextCouponDate,MaturityDate,Status

                        From #OMSBondByAllInstrument
                        Group by InstrumentTypeID,SectorID,InstrumentID,NextCouponDate,MaturityDate,Status
                        having sum(balance) > 0
                        order by InstrumentID  
                                                                           
                                                ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSBondByInstrument M_model = new OMSBondByInstrument();
                                    M_model.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.SectorID = Convert.ToString(dr["SectorID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Volume = Convert.ToDecimal(dr["Volume"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    M_model.NextCouponDate = Convert.ToDateTime(dr["NextCouponDate"]);
                                    M_model.MaturityDate = Convert.ToDateTime(dr["MaturityDate"]);
                                    M_model.Status = Convert.ToInt32(dr["Status"]);
                                    L_model.Add(M_model);
                                }
                            } return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

//        public string OMSBond_ListStockForYahooFinance(int _fundPK, DateTime _date)
//        {
//            try
//            {
//                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
//                {
//                    DbCon.Open();
//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {

//                        cmd.CommandTimeout = 0;
//                        cmd.CommandText = @"
//                        Declare @result Nvarchar(1000)
//                        set @result = ''
//                        Select  @result = @result +  InstrumentID + '.JK,' from FundPosition A
//						 Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
//						left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
//						where date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
//						and C.Type = 1
//
//	                    Select @result = @result +  B.ID + '.JK,' From Investment A
//	                    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
//						left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
//	                    where StatusInvestment <> 3 
//	                    and A.InstrumentTypePK = 1 and ValueDate >= @Date
//						and C.Type = 1
//
//                        select left(@Result,len(@result) - 1) result
//                        ";
//                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
//                        cmd.Parameters.AddWithValue("@Date", _date);

//                        using (SqlDataReader dr = cmd.ExecuteReader())
//                        {
//                            if (dr.HasRows)
//                            {
//                                dr.Read();
//                                return dr["result"].ToString();
//                            }
//                            return "";
//                        }
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                throw err;
//            }
//        }

        public List<OMSExposureBond> OMSExposureBond_PerFund(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSExposureBond> L_model = new List<OMSExposureBond>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     
Declare @MaxDateEndDayFP datetime

Declare @TrailsPK int

Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
	Select max(ValueDate) from endDayTrailsFundPortfolio where
	valuedate < @Date  and status = 2 and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK


set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))

Create table #OMSBondExposure
(
FundID nvarchar(100),
ExposureType nvarchar(100),
ExposureID nvarchar(100),
Balance numeric (22,4),
AvgPrice numeric (22,4),
LastPrice numeric (22,4),
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8),
Status int,
Amount numeric(22,4)
	
)


Create table #OMSBondExposureTemp
(
AUMPrevDay numeric(22,4),
ExposureType nvarchar(100),
ExposureID nvarchar(100),
MaxExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8),
Balance numeric (22,4),
AvgPrice numeric (22,4),
LastPrice numeric (22,4),
Status int,
Amount numeric(22,4)
	
)

						

Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
	    select max(date) from CloseNAV where date < @Date
	    and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter
left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type in (2,5,14)

	
Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100)
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end
,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1
,A.DoneAmount  * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on B.InstrumentTypePK = D.Parameter
left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3 
and A.StatusSettlement <> 3
and E.Type in (2,5,14)


-- BOND

Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'BOND ALL' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2  and   H.Type in (2,5,14)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'BOND ALL' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14)
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3






Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER BOND' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER BOND' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100)
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14) and A.StatusDealing <> 3 and A.StatusInvestment <> 3 
and A.StatusSettlement <> 3 

Insert into #OMSBondExposureTemp(AUMPrevDay,ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent
,WarningMaxExposurePercent,WarningMinExposurePercent,Balance,AvgPrice,LastPrice,Status,Amount)
select @TotalMarketValue,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent
, sum(balance)  Balance
,avg(AvgPrice),avg(LastPrice),Status,sum(MarketValue)
from #OMSBondExposure
group by ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent,Status,AvgPrice


Select AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(Amount) MarketValue, sum(Amount)/ AUMPrevDay * 100 ExposurePercent,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSBondExposureTemp A
Group by AUMPrevDay,ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by ExposureType ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     
Declare @MaxDateEndDayFP datetime

Declare @TrailsPK int

Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
	Select max(ValueDate) from endDayTrailsFundPortfolio where
	valuedate < @Date  and status = 2
)
and status = 2


set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))

Create table #OMSBondExposure
(
FundID nvarchar(100),
ExposureType nvarchar(100),
ExposureID nvarchar(100),
Balance numeric (22,4),
AvgPrice numeric (22,4),
LastPrice numeric (22,4),
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8),
Status int,
Amount numeric(22,4)
	
)


Create table #OMSBondExposureTemp
(
AUMPrevDay numeric(22,4),
ExposureType nvarchar(100),
ExposureID nvarchar(100),
MaxExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8),
Balance numeric (22,4),
AvgPrice numeric (22,4),
LastPrice numeric (22,4),
Status int,
Amount numeric(22,4)
	
)

						

Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
	    select max(date) from CloseNAV where date < @Date
	    and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter
left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type in (2,5,14)

	
Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100)
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end
,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1
,A.DoneAmount  * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on B.InstrumentTypePK = D.Parameter
left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3 
and A.StatusSettlement <> 3
and E.Type in (2,5,14)


-- BOND

Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'BOND ALL' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2  and   H.Type in (2,5,14)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'BOND ALL' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14)
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3






Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER BOND' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14)


Insert into #OMSBondExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER BOND' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSBond(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100)
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14) and A.StatusDealing <> 3 and A.StatusInvestment <> 3 
and A.StatusSettlement <> 3 

Insert into #OMSBondExposureTemp(AUMPrevDay,ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent
,WarningMaxExposurePercent,WarningMinExposurePercent,Balance,AvgPrice,LastPrice,Status,Amount)
select @TotalMarketValue,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent
, sum(balance)  Balance
,avg(AvgPrice),avg(LastPrice),Status,sum(MarketValue)
from #OMSBondExposure
group by ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent,Status,AvgPrice


Select AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(Amount) MarketValue, sum(Amount)/ AUMPrevDay * 100 ExposurePercent,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when sum(Balance * (LastPrice/100))/ AUMPrevDay * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSBondExposureTemp A
Group by AUMPrevDay,ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by ExposureType ";
                        }
                        

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSExposureBond M_model = new OMSExposureBond();
                                    M_model.ExposureType = Convert.ToString(dr["ExposureType"]);
                                    M_model.ExposureID = Convert.ToString(dr["ExposureID"]);
                                    M_model.MaxExposurePercent = Convert.ToDecimal(dr["MaxExposurePercent"]);
                                    M_model.MinExposurePercent = Convert.ToDecimal(dr["MinExposurePercent"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.ExposurePercent = Convert.ToDecimal(dr["ExposurePercent"]);
                                    M_model.AlertMinExposure = dr["AlertMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinExposure"]);
                                    M_model.AlertMaxExposure = dr["AlertMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxExposure"]);
                                    M_model.AlertWarningMinExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMinExposure"]);
                                    M_model.AlertWarningMaxExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMaxExposure"]);
                                    L_model.Add(M_model);
                                }
                            } return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<OMSBondBySector> OMSBondBySector(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSBondBySector> L_model = new List<OMSBondBySector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                    
                    Declare @TotalMarketValue numeric(26,6)

                    select @TotalMarketValue = aum From closeNav
                    where Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                    and status = 2
	             
                    Create table #OMSBondBySector
                    (
		                InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
	                    SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
	                    Balance numeric(18,0),
	                    AvgPrice numeric(18,6),
	                    CostValue numeric(22,4),
	                    ClosePrice numeric(18,4),
	                    LastPrice numeric(18,4),
	                    LotInShare int
                    )
	
                    Insert into #OMSBondBySector(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare)
                    Select A.InstrumentID,C.ID,
	                A.balance + isnull(case when D.TrxType = 1 and D.StatusDealing in( 0,1) then D.Volume else
	                case when D.TrxType = 2 and D.StatusDealing in( 0,1) then D.Volume * -1 else
	                case when D.TrxType = 1 and D.StatusDealing = 2 then D.DoneVolume else
	                case when D.trxType = 2 and D.StatusDealing = 2 then D.DoneVolume * -1 end end end end,0)
	                + isnull(case when E.TrxType = 1 and E.StatusDealing in( 0,1) then E.Volume else
	                case when E.TrxType = 2 and E.StatusDealing in( 0,1) then E.Volume * -1 else
	                case when E.TrxType = 1 and E.StatusDealing = 2 then E.DoneVolume else
	                case when E.trxType = 2 and E.StatusDealing = 2 then E.DoneVolume * -1 end end end end,0)
	                ,
	                A.AvgPrice,A.Balance * A.AvgPrice CostValue,
	                [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                    dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) LastPrice, B.LotInShare
                    From FundPosition A
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                    left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
	                -- sisi buy dulu
	                left join (
		                Select TrxType,StatusDealing,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
		                and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (2,3,8,9,13,15)
		                and TrxType = 1 and FundPK = @FundPK
		                group by InstrumentPK,TrxType,StatusDealing
	                )D on A.InstrumentPK = D.InstrumentPK
	                -- sisi Sell
	                left join (
		                Select TrxType,StatusDealing,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
		                and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK = 1
		                and TrxType = 2 and FundPK = @FundPK
		                group by InstrumentPK,TrxType,StatusDealing
	                )E on A.InstrumentPK = E.InstrumentPK
                    left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
                    where A.Date = dbo.FWorkingDay(@Date,-1) and F.Type in (2,5,14) and A.FundPK = @FundPK


	                Insert into #OMSBondBySector(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare)
	                Select B.ID,C.ID,   
	                case when A.TrxType = 1 and A.StatusDealing in( 0,1) then A.Volume else
	                case when A.TrxType = 2 and A.StatusDealing in( 0,1) then A.Volume * -1 else
	                case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume else
	                case when A.trxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1 end end end end,
	                case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
	                case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
	                end end,	[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
                    dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) LastPrice,B.LotInShare  
	                from Investment A
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	                left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
	                where ValueDate = @Date and StatusInvestment <> 3 and StatusDealing <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
                    and FundPK = @FundPK
	                and B.ID Not in 
	                (
		                select instrumentID From #OMSBondBySector
	                )


                    Select InstrumentID,SectorID,isnull(sum(Balance * (LastPrice/100)) / @TotalMarketValue * 100,0) CurrentExposure  ,
	                sum(Balance) Volume,avg(AvgPrice) AvgPrice,sum(Balance * AvgPrice)/100 Cost,
                    Avg(ClosePrice) ClosePrice,Avg(LastPrice) LastPrice,
	                ((Avg(LastPrice/100) / Avg(case when ClosePrice = 0 then 1 else closeprice end)/100) - 1) * 100 PriceDifference,sum(Balance * LastPrice)/100 MarketValue,
                    (sum(Balance * LastPrice)/100) - (sum(Balance * AvgPrice)/100) Unrealized, 
	                ((sum(Balance * LastPrice)/100) / (sum(Balance * case when AvgPrice = 0 then 1 else AvgPrice end)/100) -1) * 100 GainLoss

                    From #OMSBondBySector
                    Group by SectorID,InstrumentID
                    having sum(balance) > 0
                                                                
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSBondBySector M_model = new OMSBondBySector();
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.SectorID = Convert.ToString(dr["SectorID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Volume = Convert.ToDecimal(dr["Volume"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    L_model.Add(M_model);
                                }
                            }
                            return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public decimal OMSBondGetNetBuySell(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Create table #NetBuySell
                        (
	                        Amount numeric(22,4)
                        )
                        Insert into #NetBuySell (Amount) 

                        Select sum(Amount * -1) from Investment where ValueDate = @Date and FundPK = @FundPK and StatusInvestment  = 2 and OrderStatus in ('M','P') and TrxType  = 1 and InstrumentTypePK in (2,3,8,9,13,15)

                        Insert into #NetBuySell (Amount) 

                        Select sum(Amount) from Investment where ValueDate = @Date and FundPK = @FundPK and StatusInvestment  = 2 and OrderStatus in ('M','P') and TrxType  = 2 and InstrumentTypePK in (2,3,8,9,13,15)

                        Select isnull(SUM(Amount),0) Amount from #NetBuySell
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Amount"]);
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

        public decimal OMSBondGetDealingNetBuySellBond(DateTime _date, int _fundPK, int _counterpartPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramCounterpart = "";

                        if (_fundPK != 0)
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_counterpartPK != 0)
                        {
                            _paramCounterpart = "And CounterpartPK = @CounterpartPK ";
                        }
                        else
                        {
                            _paramCounterpart = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Create table #NetBuySell
                        (
	                        Amount numeric(22,4)
                        )
                        Insert into #NetBuySell (Amount) 

                        Select sum(DoneAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus in ('M','P') and TrxType  = 1 and InstrumentTypePK in (2,3,8,9,13,15)

                        Insert into #NetBuySell (Amount) 

                        Select DoneAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus  in ('M','P') and TrxType  = 2 and InstrumentTypePK in (2,3,8,9,13,15)

                        Select isnull(SUM(Amount),0) Amount from #NetBuySell
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        if (_fundPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        if (_counterpartPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Amount"]);
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

        public decimal OMSBondGetSettlementNetBuySellBond(DateTime _date, int _fundPK, int _counterpartPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramCounterpart = "";

                        if (_fundPK != 0)
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (_counterpartPK != 0)
                        {
                            _paramCounterpart = "And CounterpartPK = @CounterpartPK ";
                        }
                        else
                        {
                            _paramCounterpart = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Create table #NetBuySell
                        (
	                        TotalAmount numeric(24,2)
                        )
                        Insert into #NetBuySell (TotalAmount) 

                        Select sum(TotalAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2  and TrxType  = 1 and InstrumentTypePK in (2,3,8,9,13,15)
                        Insert into #NetBuySell (TotalAmount) 

                        Select TotalAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2 and TrxType  = 2 and InstrumentTypePK in (2,3,8,9,13,15)

                        Select isnull(SUM(TotalAmount),0) TotalAmount from #NetBuySell
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        if (_fundPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        if (_counterpartPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["TotalAmount"]);
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

        public decimal OMSBondGetAUMYesterday(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        if @FundPK = 0 
                        BEGIN
                        Select sum(AUM) AUM from CloseNAV where date = (select max(date) 
                            from CloseNAV where date <= dbo.FWorkingDay(@Date,-1)
                            and status = 2
                        )
                        and status = 2
   
                        END
                        ELSE
                        BEGIN
                     
                        Select AUM from CloseNAV where date = (select max(date) 
                            from CloseNAV where date <= dbo.FWorkingDay(@Date,-1)
                            and FundPK = @FundPK
                            and status = 2
                        )
                        and FundPK = @FundPK
                        and status = 2
   
                        END
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["AUM"]);
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

        public List<OMSBondByMaturity> OMSBondByMaturity(DateTime _date, int _fundPK, string _filterTypeMaturity, int _filterPeriod, int _filterValue)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSBondByMaturity> L_model = new List<OMSBondByMaturity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramTypeMaturity = "";
                        //string _paramPeriod = "";
                        //string _paramValue = "";

                        if (_filterTypeMaturity == "1")
                        {
                            _paramTypeMaturity = "where MaturityDate between @MaxDateEndDayFP and @DateTo ";
                        }
                        else if (_filterTypeMaturity == "2")
                        {
                            _paramTypeMaturity = "where NextCouponDate between @MaxDateEndDayFP and @DateTo ";
                        }
                        else
                        {
                            _paramTypeMaturity = "where MaturityDate between @MaxDateEndDayFP and @DateTo ";
                        }

                        //if (_filterPeriod != "0")
                        //{
                        //    _paramPeriod = "And CR.FundPK  = @FundPK ";
                        //}
                        //else
                        //{
                        //    _paramPeriod = "";
                        //}

                        //if (_filterValue != "0")
                        //{
                        //    _paramValue = "And CR.FundPK  = @FundPK ";
                        //}
                        //else
                        //{
                        //    _paramValue = "";
                        //}

                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
                        endDayTrailsFundPortfolio where 
                        valuedate = 
                        (
	                        Select max(ValueDate) from endDayTrailsFundPortfolio where
	                        valuedate < @Date  and status = 2  and FundPK = @FundPK
                        )
                        and status = 2 and FundPK = @FundPK


                        set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))

                        declare @DateTo datetime


                        select @DateTo = case when @FilterPeriod = 1 then DATEADD(DAY,@FilterValue,@Date) else case when @FilterPeriod = 2 then DATEADD(WEEK,@FilterValue,@Date)
                        else case when @FilterPeriod = 3 then DATEADD(MONTH,@FilterValue,@Date) else case when @FilterPeriod = 4 then DATEADD(YEAR,@FilterValue,@Date) else '12/31/2099' End End End End


                        
                        Declare @TotalMarketValue numeric(22,4)
  
                        Select @TotalMarketValue = AUM From CloseNav where 
                        Date = (
                        select max(Date) from closeNAV where status = 2 and Date < @Date and FundPK = @FundPK
                        )
                        and FundPK = @FundPK  and status = 2
  
                        set @TotalMarketValue = isnull(@TotalMarketValue,1)

                        IF @TotalMarketValue = 0
                        BEGIN
                        set @TotalMarketValue = 1
                        END

                        
                        Create table #OMSBondByAllInstrument
(
InstrumentTypeID nvarchar(100) COLLATE DATABASE_DEFAULT,
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(18,0),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LastCouponDate datetime,
NextCouponDate datetime,
MaturityDate datetime,
Status int,
InterestPercent numeric(18,6),
)
                        	
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select F.ID,A.InstrumentID,C.ID,
A.balance + isnull(
case when D.TrxType = 1 then isnull(D.DoneVolume,0) else
case when D.trxType = 2 then D.DoneVolume * -1 end end ,0)
+ isnull(
case when E.TrxType = 1 then E.DoneVolume else
case when E.trxType = 2 then E.DoneVolume * -1 end end ,0)
,
A.AvgPrice,A.Balance * A.AvgPrice CostValue,
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,dbo.FgetLastCouponDate(@date,A.InstrumentPK),dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0),A.InterestPercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
-- sisi buy dulu
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 1 and FundPK = @FundPK
group by InstrumentPK,TrxType
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 2 and FundPK = @FundPK
group by InstrumentPK,TrxType
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.Date between @MaxDateEndDayFP and @Date and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2 and A.TrailsPK = @TrailsPK
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)
                        
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 1
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

union all

Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice ,A.LastCouponDate,A.NextCouponDate,A.MaturityDate,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 2
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)


Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select F.ID,A.InstrumentID,C.ID,
A.balance + isnull(
case when D.TrxType = 1 then D.DoneVolume else
case when D.trxType = 2 then D.DoneVolume * -1 end end,0)
+ isnull(
case when E.TrxType = 1 then E.DoneVolume else
case when E.trxType = 2 then E.DoneVolume * -1 end end,0)
,
A.AvgPrice,A.Balance * A.AvgPrice CostValue,
isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
G.ClosePriceValue LastPrice,dbo.FgetLastCouponDate(@date,A.InstrumentPK),dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0),A.InterestPercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.Status = 2
-- sisi buy dulu
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 1 and FundPK = @FundPK
group by InstrumentPK,TrxType
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 2 and FundPK = @FundPK
group by InstrumentPK,TrxType
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.Date between @MaxDateEndDayFP and @Date and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2 and A.TrailsPK = @TrailsPK
and B.InstrumentPK  in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

                        
                        
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 1
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK  in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

union all

Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1 ,A.InterestPercent
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 2
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)
       
       
             



                        Select InstrumentTypeID,InstrumentID,SectorID,isnull(sum(Balance * (LastPrice/100)) / @TotalMarketValue * 100,0) CurrentExposure  ,
                        sum(Balance) Volume,avg(AvgPrice) AvgPrice,sum(Balance * (AvgPrice/100)) Cost,
                        Avg(ClosePrice) ClosePrice,Avg(LastPrice) LastPrice,
                        ((Avg(LastPrice) / Avg(case when ClosePrice = 0 then 1 else closeprice end)) - 1) * 100 PriceDifference,sum(Balance * (LastPrice/100)) MarketValue,
                        (sum(Balance * (LastPrice/100))) - (sum(Balance * (AvgPrice/100))) Unrealized, 
                        ((sum(Balance * (LastPrice))) / (sum(Balance * case when AvgPrice = 0 then 1 else (AvgPrice) end)) -1) * 100 GainLoss,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent

                        From #OMSBondByAllInstrument
                        " + _paramTypeMaturity + @"
                        Group by InstrumentTypeID,SectorID,InstrumentID,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent
                        having sum(balance) > 0
                        order by InstrumentID  
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
                        endDayTrailsFundPortfolio where 
                        valuedate = 
                        (
	                        Select max(ValueDate) from endDayTrailsFundPortfolio where
	                        valuedate < @Date  and status = 2
                        )
                        and status = 2


                        set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))

                        declare @DateTo datetime


                        select @DateTo = case when @FilterPeriod = 1 then DATEADD(DAY,@FilterValue,@Date) else case when @FilterPeriod = 2 then DATEADD(WEEK,@FilterValue,@Date)
                        else case when @FilterPeriod = 3 then DATEADD(MONTH,@FilterValue,@Date) else case when @FilterPeriod = 4 then DATEADD(YEAR,@FilterValue,@Date) else '12/31/2099' End End End End


                        
                        Declare @TotalMarketValue numeric(22,4)
  
                        Select @TotalMarketValue = AUM From CloseNav where 
                        Date = (
                        select max(Date) from closeNAV where status = 2 and Date < @Date and FundPK = @FundPK
                        )
                        and FundPK = @FundPK  and status = 2
  
                        set @TotalMarketValue = isnull(@TotalMarketValue,1)

                        IF @TotalMarketValue = 0
                        BEGIN
                        set @TotalMarketValue = 1
                        END

                        
                        Create table #OMSBondByAllInstrument
(
InstrumentTypeID nvarchar(100) COLLATE DATABASE_DEFAULT,
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(18,0),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LastCouponDate datetime,
NextCouponDate datetime,
MaturityDate datetime,
Status int,
InterestPercent numeric(18,6),
)
                        	
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select F.ID,A.InstrumentID,C.ID,
A.balance + isnull(
case when D.TrxType = 1 then isnull(D.DoneVolume,0) else
case when D.trxType = 2 then D.DoneVolume * -1 end end ,0)
+ isnull(
case when E.TrxType = 1 then E.DoneVolume else
case when E.trxType = 2 then E.DoneVolume * -1 end end ,0)
,
A.AvgPrice,A.Balance * A.AvgPrice CostValue,
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,dbo.FgetLastCouponDate(@date,A.InstrumentPK),dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0),A.InterestPercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
-- sisi buy dulu
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 1 and FundPK = @FundPK
group by InstrumentPK,TrxType
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 2 and FundPK = @FundPK
group by InstrumentPK,TrxType
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.Date between @MaxDateEndDayFP and @Date and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2 and A.TrailsPK = @TrailsPK
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)
                        
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 1
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

union all

Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice ,A.LastCouponDate,A.NextCouponDate,A.MaturityDate,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 2
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK not in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)


Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select F.ID,A.InstrumentID,C.ID,
A.balance + isnull(
case when D.TrxType = 1 then D.DoneVolume else
case when D.trxType = 2 then D.DoneVolume * -1 end end,0)
+ isnull(
case when E.TrxType = 1 then E.DoneVolume else
case when E.trxType = 2 then E.DoneVolume * -1 end end,0)
,
A.AvgPrice,A.Balance * A.AvgPrice CostValue,
isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
G.ClosePriceValue LastPrice,dbo.FgetLastCouponDate(@date,A.InstrumentPK),dbo.FgetNextCouponDate(@date,A.InstrumentPK),A.MaturityDate,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0),A.InterestPercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.Status = 2
-- sisi buy dulu
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 1 and FundPK = @FundPK
group by InstrumentPK,TrxType
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and instrumentTypePK in (2,3,8,9,13,15)
and TrxType = 2 and FundPK = @FundPK
group by InstrumentPK,TrxType
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.Date between @MaxDateEndDayFP and @Date and F.Type in (2,5,14) and A.FundPK = @FundPK and A.status  = 2 and A.TrailsPK = @TrailsPK
and B.InstrumentPK  in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

                        
                        
Insert into #OMSBondByAllInstrument(InstrumentTypeID,InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent)
Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1,A.InterestPercent 
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 1
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK  in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)

union all

Select D.ID,B.ID,C.ID,   
case when A.TrxType = 1 then A.DoneVolume else
case when A.trxType = 2 then A.DoneVolume * -1 end end,
case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
end end,	isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then isnull([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK),100) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,A.LastCouponDate,A.NextCouponDate,A.MaturityDate ,1 ,A.InterestPercent
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where ValueDate between @MaxDateEndDayFP and @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and A.TrxType = 2
and B.ID Not in 
(
select instrumentID From #OMSBondByAllInstrument
)
and B.InstrumentPK in
( 
select instrumentPK From UpdateClosePrice where status  = 2
)
       
       
             



                        Select InstrumentTypeID,InstrumentID,SectorID,isnull(sum(Balance * (LastPrice/100)) / @TotalMarketValue * 100,0) CurrentExposure  ,
                        sum(Balance) Volume,avg(AvgPrice) AvgPrice,sum(Balance * (AvgPrice/100)) Cost,
                        Avg(ClosePrice) ClosePrice,Avg(LastPrice) LastPrice,
                        ((Avg(LastPrice) / Avg(case when ClosePrice = 0 then 1 else closeprice end)) - 1) * 100 PriceDifference,sum(Balance * (LastPrice/100)) MarketValue,
                        (sum(Balance * (LastPrice/100))) - (sum(Balance * (AvgPrice/100))) Unrealized, 
                        ((sum(Balance * (LastPrice))) / (sum(Balance * case when AvgPrice = 0 then 1 else (AvgPrice) end)) -1) * 100 GainLoss,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent

                        From #OMSBondByAllInstrument
                        " + _paramTypeMaturity + @"
                        Group by InstrumentTypeID,SectorID,InstrumentID,LastCouponDate,NextCouponDate,MaturityDate,Status,InterestPercent
                        having sum(balance) > 0
                        order by InstrumentID  
                        ";
                        }


                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FilterPeriod", _filterPeriod);
                        cmd.Parameters.AddWithValue("@FilterValue", _filterValue);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSBondByMaturity M_model = new OMSBondByMaturity();
                                    M_model.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.SectorID = Convert.ToString(dr["SectorID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Volume = Convert.ToDecimal(dr["Volume"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    M_model.LastCouponDate = Convert.ToDateTime(dr["LastCouponDate"]);
                                    M_model.NextCouponDate = Convert.ToDateTime(dr["NextCouponDate"]);
                                    M_model.MaturityDate = Convert.ToDateTime(dr["MaturityDate"]);
                                    M_model.Status = Convert.ToInt32(dr["Status"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    L_model.Add(M_model);
                                }
                            }
                            return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Validate_ApproveBySelectedDataOMSBond(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And InvestmentPK in (0) ";
                        }

                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandTimeout = 0;
                        if (Tools.ClientCode == "03")
                        {
                            cmd.CommandText = @"
                        if Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = '' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )      
                        BEGIN 
                        Select 2 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 6 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";
                        }
                        else
                        {
                            cmd.CommandText = @"
                        if Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = '' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )      
                        BEGIN 
                        Select 2 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'M' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 5 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  = @InstrumentTypePK " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 6 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";
                        }



                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@ValueDateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

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

        public int Validate_RejectBySelectedDataOMSBond(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And InvestmentPK in (0) ";
                        }
                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        if Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  not in(1,4,5,6,7,10,11,16) " + _paramInvestmentPK  + _paramFund + @" )        
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  not in(1,4,5,6,7,10,11,16) " + _paramInvestmentPK  + _paramFund + @" )         
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  not in(1,4,5,6,7,10,11,16) " + _paramInvestmentPK + _paramFund + @" )         
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'M' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  not in(1,4,5,6,7,10,11,16) " + _paramInvestmentPK + _paramFund + @" )         
                        BEGIN 
                        Select 5 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType and InstrumentTypePK  not in(1,4,5,6,7,10,11,16) " + _paramInvestmentPK  + _paramFund + @" )         
                        BEGIN 
                        Select 6 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@ValueDateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

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

        public int Investment_ApproveOMSBondBySelected(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And InvestmentPK in (0) ";
                        }
                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @investmentPK int
                        declare @historyPK int
                        declare @DealingPK int
                        declare @Notes nvarchar(500)
                        declare @OrderPrice numeric(22,8)
                        declare @Volume numeric(22,2)
                        declare @Amount numeric(22,0)
                        declare @AccruedInterest numeric(22,0)
                        Declare @TaxPercentageBond numeric(8,4)
		                Declare @TaxPercentageCapitalGain numeric(8,4)                        
                        Declare @FundPK int

                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = '' and Status = 2   
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)   
                        Select @Time,'InvestmentInstruction_RejectOMSBondBySelected','Investment',InvestmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Investment where ValueDate between @DateFrom and @DateTo and statusInvestment = 1 " + _paramInvestmentPK + @"  and  InstrumentTypePK = @InstrumentTypePK and TrxType = @TrxType

                        DECLARE A CURSOR FOR 
	                            Select InvestmentPK,DealingPK,HistoryPK,InvestmentNotes,OrderPrice,Volume,Amount,AccruedInterest,FundPK From investment 
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom " + _paramInvestmentPK + @" and  InstrumentTypePK in (2,3,8,9,13,15) and TrxType = @TrxType 

                        Open A
                        Fetch Next From A
                        Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@FundPK

                        While @@FETCH_STATUS = 0
                        BEGIN

                        Select @TaxPercentageBond = TaxPercentageBond,@TaxPercentageCapitalGain =  TaxPercentageCapitalGain 
                        from FundAccountingSetup where status = 2 and FundPK = @FundPK
                           
                        set  @TaxPercentageBond = isnull(@TaxPercentageBond,0)
                        set  @TaxPercentageCapitalGain = isnull(@TaxPercentageCapitalGain,0)

                        Select @DealingPK = max(DealingPK) + 1 From investment
                        if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                        update Investment set DealingPK = @DealingPK, statusInvestment = 2, statusDealing = 1,InvestmentNotes=@Notes,DonePrice=@OrderPrice,DoneVolume=@Volume,DoneAccruedInterest=@AccruedInterest,BoardType = 1 ,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,EntryDealingID = @ApprovedUsersID,EntryDealingTime = @ApprovedTime ,LastUpdate=@LastUpdate
                        ,IncomeTaxInterestPercent = @TaxPercentageBond , IncomeTaxGainPercent = @TaxPercentageCapitalGain, TaxExpensePercent = @TaxPercentageBond
                        where InvestmentPK = @InvestmentPK
                      
                        Fetch next From A Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@FundPK
                        END
                        Close A
                        Deallocate A 

                        Update Investment set SelectedInvestment  = 0";



                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@UsersID", _investment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _investment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Time", _dateTimeNow);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
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

        public int Investment_RejectOMSBondBySelected(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ")";
                        }
                        else
                        {
                            _paramInvestmentPK = " And InvestmentPK in (0) ";
                        }
                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"Update Investment set StatusInvestment  = 3,statusDealing = 0,statusSettlement = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime
                            where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) " + _paramFund +
                            " Update Investment set SelectedInvestment  = 0";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _investment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
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

        public int Validate_CheckAvailableInstrument(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            Declare @CurrBalance numeric (18,4)
                            
                             Declare @TrailsPK int
    select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio 
    where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2 and FundPK = @FundPK

    select @CurrBalance =  A.Balance + isnull(B.Balance,0) from (
	
	    select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,'IDR' CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,[Identity] TrxBuy,'FP' TrxBuyType from FundPosition A    
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	    where A.FundPK =  @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15)               


	    union all
	
	    Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
	    case when A.trxType = 2  then A.DoneVolume * -1 end end) Balance,'IDR',A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType
	    from Investment A
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	    where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
	    and FundPK = @FundPK and TrxType  = 1  
	    group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK

    ) A left join
    (
	    Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
	    case when A.trxType = 2  then A.DoneVolume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType 
	    from Investment A
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	    where ValueDate = @date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and A.instrumentTypePK in (2,3,8,9,13,15)
	    and FundPK = @FundPK and TrxType  = 2
	    group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType
    ) B on A.TrxBuy = B.TrxBuy and A.TrxBuyType = B.TrxBuyType and A.InstrumentPK = B.InstrumentPK
    where A.InstrumentPK = @InstrumentPK and A.TrxBuy = @TrxBuy and A.TrxBuyType = @TrxBuyType

    IF (@Balance > @CurrBalance)
    BEGIN
	    select 1 Result
    END
    ELSE
    BEGIN
	    select 2 Result
    END
                           ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                            Declare @CurrBalance numeric (18,4)
                            
                             Declare @TrailsPK int
    select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2

    select @CurrBalance =  A.Balance + isnull(B.Balance,0) from (
	
	    select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,'IDR' CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,[Identity] TrxBuy,'FP' TrxBuyType from FundPosition A    
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	    where A.FundPK =  @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15)               


	    union all
	
	    Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
	    case when A.trxType = 2  then A.DoneVolume * -1 end end) Balance,'IDR',A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType
	    from Investment A
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	    where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
	    and FundPK = @FundPK and TrxType  = 1  
	    group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK

    ) A left join
    (
	    Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
	    case when A.trxType = 2  then A.DoneVolume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType 
	    from Investment A
	    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	    where ValueDate = @date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and A.instrumentTypePK in (2,3,8,9,13,15)
	    and FundPK = @FundPK and TrxType  = 2
	    group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType
    ) B on A.TrxBuy = B.TrxBuy and A.TrxBuyType = B.TrxBuyType and A.InstrumentPK = B.InstrumentPK
    where A.InstrumentPK = @InstrumentPK and A.TrxBuy = @TrxBuy and A.TrxBuyType = @TrxBuyType

    IF (@Balance > @CurrBalance)
    BEGIN
	    select 1 Result
    END
    ELSE
    BEGIN
	    select 2 Result
    END
                           ";
                        }


                        cmd.Parameters.AddWithValue("@date", _investment.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                        cmd.Parameters.AddWithValue("@Balance", _investment.Volume);
                        cmd.Parameters.AddWithValue("@TrxBuy", _investment.TrxBuy);
                        cmd.Parameters.AddWithValue("@TrxBuyType", _investment.TrxBuyType);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

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

        public FundExposure Validate_CheckExposure(DateTime _valuedate, int _instrumentPK, int _fundPK, decimal _amount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                      
Declare @MaxDateEndDayFP datetime

Declare @TrailsPK int

Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
Select max(ValueDate) from endDayTrailsFundPortfolio where
valuedate < @Date  and status = 2 and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK


set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))


Create table #Exposure
(
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
AlertMinExposure bit,
AlertMaxExposure bit,
WarningMinExposure bit,
WarningMaxExposure bit
	
)

Create table #OMSBondExposure
(
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(32,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8)
	
)



						

Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
select max(date) from CloseNAV where date <= dbo.FWorkingDay(@Date,-1) 
and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1)



------------------------- INSTRUMENT TYPE CHECKING ---------------------------------

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'INSTRUMENT TYPE',0,0,D.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter 
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2



---------------------- BOND ALL CHECKING ------------------------


Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'BOND ALL',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK  
)C on C.parameter = 0
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 


---------------------- EQUITY PER SINGLE INSTRUMENT CHECKING ------------------------

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'PER SINGLE INSTRUMENT BOND',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK  and Parameter > 0
)C on C.parameter = @InstrumentPK
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2 
Where FundPK = @FundPK and A.status = 2 




Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, D.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter
left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type in (2,5,14)

	
Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,isnull(A.Amount,0)
,0,E.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on B.InstrumentTypePK = D.Parameter
left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 and E.Type in (2,5,14)


-- BOND

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'BOND ALL' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14) 
and A.InstrumentPK = @InstrumentPK


Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'BOND ALL' ExposureType
,isnull(A.Amount,0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14)
and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 and A.StatusDealing <> 3
and A.InstrumentPK = @InstrumentPK





Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'PER SINGLE INSTRUMENT BOND' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14)
and A.InstrumentPK = @InstrumentPK

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'PER SINGLE INSTRUMENT BOND' ExposureType
,isnull(A.Amount,0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14) and A.StatusDealing <> 3 and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 
and A.InstrumentPK = @InstrumentPK



---- CASE SAAT BLM ADA DI INVESTMENT / FUND POSITION
Declare @InstrumentID nvarchar(100)

select @InstrumentID = ID from Instrument where InstrumentPK = @InstrumentPK and status = 2

IF NOT EXISTS(select * from Investment where  ValueDate = @date and FundPK = @FundPK and InstrumentPK = @InstrumentPK and StatusDealing <> 3 and StatusSettlement <> 3 and StatusInvestment <> 3  )
BEGIN
	Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
	MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
	select ID FundID,'ISSUER' ExposureType,0,0,@InstrumentID,
	MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent from FundExposure A
	left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	where A.Type = 2 and A.Status = 2 and A.FundPK = @FundPK and Parameter = 0
END


Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure)                     
Select ExposureType,
UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,(sum(MarketValue) + @Amount) MarketValue, (sum(MarketValue)+@Amount)/ @TotalMarketValue * 100 ExposurePercent,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSBondExposure A 
left join Instrument B on A.ExposureID = B.ID and B.status  = 2
--where instrumentPK  =  @InstrumentPK
Group by ExposureID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,ExposureType
order by ExposureID 

select  ExposureType,ExposureID,Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end AlertExposure,ExposurePercent,MaxExposurePercent,MinExposurePercent from #Exposure where 
Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end > 0 and ExposureID  
not in ('RG','DEP')
order by AlertExposure desc

                           ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                      
Declare @MaxDateEndDayFP datetime

Declare @TrailsPK int

Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
Select max(ValueDate) from endDayTrailsFundPortfolio where
valuedate < @Date  and status = 2
)
and status = 2


set @MaxDateEndDayFP = isnull(@MaxDateEndDayFP,dbo.fworkingday(@date,-1))


Create table #Exposure
(
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
AlertMinExposure bit,
AlertMaxExposure bit,
WarningMinExposure bit,
WarningMaxExposure bit
	
)

Create table #OMSBondExposure
(
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(32,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8)
	
)



						

Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
select max(date) from CloseNAV where date <= dbo.FWorkingDay(@Date,-1) 
and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1)



------------------------- INSTRUMENT TYPE CHECKING ---------------------------------

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'INSTRUMENT TYPE',0,0,D.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter 
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2



---------------------- BOND ALL CHECKING ------------------------


Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'BOND ALL',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK  
)C on C.parameter = 0
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 


---------------------- EQUITY PER SINGLE INSTRUMENT CHECKING ------------------------

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'PER SINGLE INSTRUMENT BOND',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK  and Parameter > 0
)C on C.parameter = @InstrumentPK
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2 
Where FundPK = @FundPK and A.status = 2 




Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, D.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter
left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type in (2,5,14)

	
Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,isnull(A.Amount,0)
,0,E.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on B.InstrumentTypePK = D.Parameter
left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 and E.Type in (2,5,14)


-- BOND

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'BOND ALL' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14) 
and A.InstrumentPK = @InstrumentPK


Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'BOND ALL' ExposureType
,isnull(A.Amount,0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14)
and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 and A.StatusDealing <> 3
and A.InstrumentPK = @InstrumentPK





Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'PER SINGLE INSTRUMENT BOND' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) / 100
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End 
,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type in (2,5,14)
and A.InstrumentPK = @InstrumentPK

Insert into #OMSBondExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'PER SINGLE INSTRUMENT BOND' ExposureType
,isnull(A.Amount,0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type in (2,5,14) and A.StatusDealing <> 3 and A.StatusInvestment <> 3  and A.StatusSettlement <> 3 
and A.InstrumentPK = @InstrumentPK


Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure)                     
Select ExposureType,
UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,(sum(MarketValue) + @Amount) MarketValue, (sum(MarketValue)+@Amount)/ @TotalMarketValue * 100 ExposurePercent,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSBondExposure A 
left join Instrument B on A.ExposureID = B.ID and B.status  = 2
--where instrumentPK  =  @InstrumentPK
Group by ExposureID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,ExposureType
order by ExposureID 

select  ExposureType,ExposureID,Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end AlertExposure,ExposurePercent,MaxExposurePercent,MinExposurePercent from #Exposure where 
Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end > 0 and ExposureID  
not in ('RG','DEP')
order by AlertExposure desc

                           ";
                        }
                        

                        cmd.Parameters.AddWithValue("@date", _valuedate);
                        cmd.Parameters.AddWithValue("@instrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@fundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Amount", _amount);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundExposure()
                                {
                                    ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]),
                                    AlertExposure = dr["AlertExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AlertExposure"]),
                                    ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]),
                                    MaxExposurePercent = dr["MaxExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxExposurePercent"]),

                                };
                            }
                            else
                            {
                                return new FundExposure()
                                {
                                    ExposureID = "",
                                    AlertExposure = 0,
                                    ExposurePercent = 0,
                                    MaxExposurePercent = 0,

                                };
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

        public Boolean OMSBond_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ")";
                        }
                        else
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (0) ";
                        }

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo
                                        ,isnull(IV.EntryDealingID,'') DealingID
                                        ,isnull(J.PTPCode,'') PTPCode
                                        ,isnull(K.ID,'') CounterpartID
                                            ,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                                          F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                                          IV.Amount,IV.Notes, IV.RangePrice, IV.*   
                                          from Investment IV       
                                          left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                                          left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                                          left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2
                                          left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                                          left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                                          Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IT.Type in (2,5,14)
                                          " + _paramInvestmentPK + _paramFund + @" order by IV.InvestmentPK ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "OMSBondListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSBondListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Investment Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        if (rSingle.InstrumentType == "Equity Reguler")
                                        {
                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);

                                        }
                                        else if (rSingle.InstrumentType == "Corporate Bond" || rSingle.InstrumentType == "Government Bond")
                                        {

                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {

                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.Volume = Convert.ToDecimal(dr0["DoneVolume"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.EntryDealingID = Convert.ToString(dr0["DealingID"]);
                                        rSingle.PTPCode = Convert.ToString(dr0["PTPCode"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;



                                    foreach (var rsHeader in GroupByFundID)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":J" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EntryDealingID;
                                                //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.CounterpartID;
                                                //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.PTPCode;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler" || rsHeader.Key.InstrumentType == "Deposito Money Market")
                                        {
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        }
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    worksheet.Cells["B" + incRowExcel + ":G" + (incRowExcel + 7)].Merge = true;
                                    int _rowNotes = incRowExcel + 10;
                                    worksheet.Cells["B" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + _rowNotes + ":G" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["G" + incRowExcel + ":G" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    incRowExcel = incRowExcel + 13;

                                    int _RowA = incRowExcel;
                                    int _RowB = incRowExcel + 7;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Bold = true;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Size = 15;
                                    if (Tools.ClientCode == "01") //ASCEND
                                    {
                                        if (_listing.Signature1 != 0)
                                        {
                                            worksheet.Cells[_RowA, 2].Value = _host.Get_PositionSignature(_listing.Signature1);
                                            worksheet.Cells[_RowA, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 2].Value = "( " + _host.Get_SignatureName(_listing.Signature1) + " )";
                                            worksheet.Cells[_RowB, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 2].Value = "";
                                            worksheet.Cells[_RowA, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 2].Value = "";
                                            worksheet.Cells[_RowB, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }


                                        if (_listing.Signature2 != 0)
                                        {
                                            worksheet.Cells[_RowA, 4].Value = _host.Get_PositionSignature(_listing.Signature2);
                                            worksheet.Cells[_RowA, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 4].Value = "( " + _host.Get_SignatureName(_listing.Signature2) + " )";
                                            worksheet.Cells[_RowB, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 4].Value = _host.Get_PositionSignature(_listing.Signature2);
                                            worksheet.Cells[_RowA, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 4].Value = "";
                                            worksheet.Cells[_RowB, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }

                                        if (_listing.Signature3 != 0)
                                        {
                                            worksheet.Cells[_RowA, 6].Value = _host.Get_PositionSignature(_listing.Signature3);
                                            worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 6].Value = "( " + _host.Get_SignatureName(_listing.Signature3) + " )";
                                            worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 6].Value = "";
                                            worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 6].Value = "";
                                            worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }

                                        if (_listing.Signature4 != 0)
                                        {
                                            worksheet.Cells[_RowA, 8].Value = _host.Get_PositionSignature(_listing.Signature4);
                                            worksheet.Cells[_RowA, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 8].Value = "( " + _host.Get_SignatureName(_listing.Signature4) + " )";
                                            worksheet.Cells[_RowB, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 8].Value = "";
                                            worksheet.Cells[_RowA, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 8].Value = "";
                                            worksheet.Cells[_RowB, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                        worksheet.Row(incRowExcel).PageBreak = true;
                                    }

                                    else if (Tools.ClientCode == "18")
                                    {
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepare By";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = "Approval";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Value = "Mengetahui";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 10;
                                        worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_PrepareByInvestment(Convert.ToDateTime(_listing.ParamListDate));
                                        worksheet.Cells[incRowExcel, 3].Value = "  )";

                                        worksheet.Cells[incRowExcel, 4].Value = "               (";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = _host.Get_ApprovalByInvestment(Convert.ToDateTime(_listing.ParamListDate));
                                        worksheet.Cells[incRowExcel, 6].Value = "       )";

                                        worksheet.Cells[incRowExcel, 7].Value = "(       ";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        worksheet.Cells[incRowExcel, 10].Value = "      )";
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Fund Manager";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = "Head of Investment";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Value = "Director";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Merge = true;

                                        worksheet.Row(incRowExcel).PageBreak = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[incRowExcel, 1].Value = "Prepare By";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Approval";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                        worksheet.Cells[incRowExcel, 3].Value = ")";
                                        worksheet.Cells[incRowExcel, 4].Value = "(    ";
                                        worksheet.Cells[incRowExcel, 6].Value = ")";
                                        worksheet.Row(incRowExcel).PageBreak = true;
                                    }
                                    string _rangeA = "A1:J" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 INVESTMENT APPROVAL";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

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

        public OMSBondForNetAmount Get_NetAmount(OMSBondForNetAmount _omsBond)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_omsBond.InstrumentTypePK == 2 || _omsBond.InstrumentTypePK == 3 || _omsBond.InstrumentTypePK == 8 || _omsBond.InstrumentTypePK == 9 || _omsBond.InstrumentTypePK == 13 || _omsBond.InstrumentTypePK == 15)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

--declare @DealingPk int
--declare @Volume numeric(22,8)
--declare @historyPk int
--declare @price numeric(22,4)
--declare @AcqDate date
--declare @TrxType int
--declare @SettledDate date
--declare @AcqPrice numeric(22,4)
--declare @AcqVolume numeric(22,4)
--declare @AcqPrice1 numeric(22,4)
--declare @AcqPrice2 numeric(22,4)
--declare @AcqPrice3 numeric(22,4)
--declare @AcqPrice4 numeric(22,4)
--declare @AcqPrice5 numeric(22,4)
--declare @AcqVolume1 numeric(22,4)
--declare @AcqVolume2 numeric(22,4)
--declare @AcqVolume3 numeric(22,4)
--declare @AcqVolume4 numeric(22,4)
--declare @AcqVolume5 numeric(22,4)
--declare @TaxCapitaGainPercent numeric(22,4)
--declare @lastCouponDate date
--declare @nextCouponDate date
--declare @AcqDate1 date
--declare @AcqDate2 date
--declare @AcqDate3 date
--declare @AcqDate4 date
--declare @AcqDate5 date
--declare @IncomeTaxInterestPercent numeric(22,4)
--declare @ValueDate date
--declare @InstrumentTypePk int
--declare @ClientCode nvarchar(20)

--set @historyPk = 1
--set @DealingPk = 218
--set @Volume = 700000000
--set @price = 102.200000
--set @AcqDate = '05/14/2020'
--set @TrxType = 1
--set @SettledDate = '2020-09-01'
--set @AcqPrice = 102.6
--set @AcqVolume = 700000000
--set @TaxCapitaGainPercent = 15
--set @lastCouponDate = '08/19/2020'
--set @nextCouponDate = '11/19/2020'
--set @IncomeTaxInterestPercent = 5
--set @ValueDate = '2020-08-31'
--set @InstrumentTypePk = 3
--set @ClientCode = '20'

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
@CurrencyID = C.ID,@CouponRate = A.InterestPercent,@InterestType = B.InterestType, 
@InterestDaysType = B.InterestDaysType,@InterestPaymentType = 12/D.Priority
From Investment A
left join instrument B on A.InstrumentPK = B.instrumentPK and B.status = 2
left join Currency C on B.CurrencyPK = C.CurrencyPK and C.status = 2
left join MasterValue D on B.InterestPaymentType = D.Code and D.Status = 2 and D.ID = 'InterestPaymentType'
where DealingPK = @DealingPK and A.HistoryPK = @HistoryPK


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
		
			set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari

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
			when @TrxType = 1then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

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


            Declare @StartOfYear Datetime
            Declare @ParamAcqDate Datetime
            Declare @ParamAcqDate1 Datetime
            Declare @ParamAcqDate2 Datetime
            Declare @ParamAcqDate3 Datetime
            Declare @ParamAcqDate4 Datetime
            Declare @ParamAcqDate5 Datetime

            set @StartOfYear = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101) 
            set @ParamAcqDate = convert(varchar(10),@AcqDate,101) 
            set @ParamAcqDate1 = convert(varchar(10),@AcqDate1,101) 
            set @ParamAcqDate2 = convert(varchar(10),@AcqDate2,101) 
            set @ParamAcqDate3 = convert(varchar(10),@AcqDate3,101) 
            set @ParamAcqDate4 = convert(varchar(10),@AcqDate4,101) 
            set @ParamAcqDate5 = convert(varchar(10),@AcqDate5,101) 

            IF (@ParamAcqDate <= @StartOfYear)
            BEGIN
	            set @AcqDate = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate1 <= @StartOfYear)
            BEGIN
	            set @AcqDate1 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate2 <= @StartOfYear)
            BEGIN
	            set @AcqDate2 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate3 <= @StartOfYear)
            BEGIN
	            set @AcqDate3 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate4 <= @StartOfYear)
            BEGIN
	            set @AcqDate4 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate5 <= @StartOfYear)
            BEGIN
	            set @AcqDate5 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END

		
			set @ValuePerUnit = 1
			set @DivDays = case when @InterestDaysType in (6) 
			then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) 
            when @InterestDaysType = 1 then 90
            else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
			
			set @Days = case when @InterestDaysType in (6) 
			then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) end -- pembagi hari
		
			set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari


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

			set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
			set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
			set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
			set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
			set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
			set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end

			set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
			set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
			set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
			set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
			set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
			set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100

			set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end else 0 end
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
				set @ValuePerUnit = 1000000
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

--if @InterestType <> 3 --ZERO COUPON
--BEGIN
	if @InstrumentTypePK in (3,8,9,15)
	BEGIN
		set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
		set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
	END
	ELSE IF @InstrumentTypePK in (2,13)
	BEGIN
            IF (@ClientCode = '20' OR @ClientCode = '21')
            BEGIN
                set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
			     / @DivDays / @InterestPaymentType * @ValuePerUnit  * @Days, 0)
			    set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
            END
            ELSE
            BEGIN
                set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
			     / @DivDays / @InterestPaymentType * @ValuePerUnit, 0) * @Days -- DAYS DI KALI SETELAH DAPAT INTEREST DAILY
			    set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
            END
			

	END
--END


set @TotalTaxCapGain = isnull(@TaxCapGainAcq,0) + isnull(@TaxCapGainAcq1,0) + isnull(@TaxCapGainAcq2,0) + isnull(@TaxCapGainAcq3,0) + isnull(@TaxCapGainAcq4,0) + isnull(@TaxCapGainAcq5,0)


set @TotalTaxAI = isnull(@TaxAIAcq,0) + isnull(@TaxAIAcq1,0) + isnull(@TaxAIAcq2,0) + isnull(@TaxAIAcq3,0) + isnull(@TaxAIAcq4,0) + isnull(@TaxAIAcq5,0)

set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
+ case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
+ case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
+ case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
+ case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
+ case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 

if @ClientCode = '20'
begin
	if @TotalTaxCapGain + @TotalTaxAI < 0
		set @TotalTaxAI = 0
end


IF(@ClientCode <> 21)
BEGIN
set @TotalTaxCapGain = case when isnull(@TaxCapGainAcq,0) > 0 then  isnull(@TaxCapGainAcq,0) else 0 end 
+ case when isnull(@TaxCapGainAcq1,0) > 0 then isnull(@TaxCapGainAcq1,0) else 0 end 
+ case when isnull(@TaxCapGainAcq2,0) > 0 then isnull(@TaxCapGainAcq2,0) else 0 end
+ case when isnull(@TaxCapGainAcq3,0) > 0 then isnull(@TaxCapGainAcq3,0) else 0 end
+ case when isnull(@TaxCapGainAcq4,0) > 0 then isnull(@TaxCapGainAcq4,0) else 0 end 
+ case when isnull(@TaxCapGainAcq5,0) > 0 then isnull(@TaxCapGainAcq5,0) else 0 end 
END
ELSE
BEGIN
set @TotalTaxCapGain = isnull(@TaxCapGainAcq,0)
+ isnull(@TaxCapGainAcq1,0)
+ isnull(@TaxCapGainAcq2,0)
+ isnull(@TaxCapGainAcq3,0)
+ isnull(@TaxCapGainAcq4,0)
+ isnull(@TaxCapGainAcq5,0)
END



set @TotalCapGain = isnull(@CapGainAcq,0) + isnull(@CapGainAcq1,0) + isnull(@CapGainAcq2,0) + isnull(@CapGainAcq3,0) + isnull(@CapGainAcq4,0) + isnull(@CapGainAcq5,0)
set @TotalAI = isnull(@AIAcq,0) + isnull(@AIAcq1,0) + isnull(@AIAcq2,0) + isnull(@AIAcq3,0) + isnull(@AIAcq4,0) + isnull(@AIAcq5,0)








IF (@ClientCode = '21' AND @TrxType = 2)
BEGIN
    set @TotalTax = (isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxCapGain end,0) 
                    + isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxAI end,0))
END
ELSE
BEGIN
    set @TotalTax = (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) 
END

 set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)


--if @TrxType = 1
--set @TotalTaxAI = @AccuredInterestAmount * @IncomeTaxInterestPercent / 100


--Select @TotalTaxCapGain taxCapGain,@TotalCapGain CapGain,@TotalAI Ai,@TotalTaxAI taxAi,@TotalTax TotalTax,@NetAmount Net,@GrossAmount gross, @AccuredInterestAmount Holding

                          
--Update Investment set IncomeTaxInterestAmount = @TotalTaxAI, IncomeTaxGainAmount = @TotalTaxCapGain, TotalAmount = @NetAmount,DoneAccruedInterest = @AccuredInterestAmount
--where DealingPK = @DealingPK and HistoryPK = @HistoryPK


Select isnull(@AccuredInterestAmount,0) InterestAmount, isnull(@TotalTaxAI,0) IncomeTaxInterestAmount,isnull(@TotalTaxCapGain,0) IncomeTaxGainAmount,
isnull(@GrossAmount,0) GrossAmount, isnull(@NetAmount,0) NetAmount   ";

                        }
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.Parameters.AddWithValue("@DealingPK", _omsBond.DealingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _omsBond.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _omsBond.ValueDate);
                        //cmd.Parameters.AddWithValue("@InstrumentPK", _omsBond.InstrumentPK);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _omsBond.InstrumentTypePK);


                        if (_omsBond.SettledDate == null)
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", _omsBond.SettledDate);
                        }
                        cmd.Parameters.AddWithValue("@NextCouponDate", _omsBond.NextCouponDate);
                        cmd.Parameters.AddWithValue("@LastCouponDate", _omsBond.LastCouponDate);
                        cmd.Parameters.AddWithValue("@TrxType", _omsBond.TrxType);
                        cmd.Parameters.AddWithValue("@Price", _omsBond.Price);
                        cmd.Parameters.AddWithValue("@Volume", _omsBond.Volume);
                        cmd.Parameters.AddWithValue("@AcqPrice", _omsBond.AcqPrice);
                        if (_omsBond.AcqDate == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", _omsBond.AcqDate);
                        }

                        if (_omsBond.AcqDate1 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", _omsBond.AcqDate1);
                        }

                        if (_omsBond.AcqDate2 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", _omsBond.AcqDate2);
                        }

                        if (_omsBond.AcqDate3 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", _omsBond.AcqDate3);
                        }

                        if (_omsBond.AcqDate4 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", _omsBond.AcqDate4);
                        }

                        if (_omsBond.AcqDate5 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", _omsBond.AcqDate5);
                        }

                        cmd.Parameters.AddWithValue("@AcqVolume", _omsBond.AcqVolume);
                        cmd.Parameters.AddWithValue("@AcqPrice1", _omsBond.AcqPrice1);
                        cmd.Parameters.AddWithValue("@AcqVolume1", _omsBond.AcqVolume1);

                        cmd.Parameters.AddWithValue("@AcqPrice2", _omsBond.AcqPrice2);
                        cmd.Parameters.AddWithValue("@AcqVolume2", _omsBond.AcqVolume2);

                        cmd.Parameters.AddWithValue("@AcqPrice3", _omsBond.AcqPrice3);
                        cmd.Parameters.AddWithValue("@AcqVolume3", _omsBond.AcqVolume3);

                        cmd.Parameters.AddWithValue("@AcqPrice4", _omsBond.AcqPrice4);
                        cmd.Parameters.AddWithValue("@AcqVolume4", _omsBond.AcqVolume4);

                        cmd.Parameters.AddWithValue("@AcqPrice5", _omsBond.AcqPrice5);
                        cmd.Parameters.AddWithValue("@AcqVolume5", _omsBond.AcqVolume5);

                        cmd.Parameters.AddWithValue("@TaxCapitaGainPercent", _omsBond.TaxCapitaGainPercent);
                        cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _omsBond.TaxInterestPercent);
                        //cmd.Parameters.AddWithValue("@BitIsRounding", _omsBond.BitIsRounding);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    //M_OMSBond.RealisedAmount = Convert.ToDecimal(dr["RealisedAmount"]);
                                    M_OMSBond.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
                                    M_OMSBond.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
                                    M_OMSBond.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
                                    M_OMSBond.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                    M_OMSBond.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                                    return M_OMSBond;
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

        public OMSBondForNetAmount Get_GrossAmount(OMSBondForNetAmount _omsBond)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_omsBond.InstrumentTypePK == 2)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

 
DECLARE @InterestAmount numeric(18, 6) 
DECLARE @GrossAmount numeric(18, 6)       
    
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
    
    
CREATE TABLE #InterestBondDivDays    
(    
FundPK int,     
InstrumentPK int,    
AcqDate datetime,    
DivDays int,    
DivPayment int,    
CountDays int    
)    
    
    
CREATE TABLE #InterestBond    
(    
 
ValueDate datetime,    
InstrumentPK int,    
InstrumentID nvarchar(50),    
FundPK int,    
FundID nvarchar(50),    
InterestAmount numeric(19,4),    
TaxPercent int    
)    
    

Declare @FundPK int
set @FundPK = 0

set @InterestAmount = 0
set @GrossAmount = 0

declare @KPDTaxPercent int    
select @KPDTaxPercent = KPDTaxExpenseForBond from SettlementSetup where status = 2    

declare @AcqDate datetime
select @Acqdate = dbo.FgetLastCouponDate(@SettledDate,@InstrumentPK) 
    
 INSERT INTO #InterestBondData -- FUND POSITION MAX(DATE) -1 DAN TIDAK ADA TRX, MATURE PUN DIHITUNG    
 Select Distinct @SettledDate,@FundPK,A.InstrumentTypePK,A.InstrumentPK,@Volume,A.InterestPercent,@Acqdate,
 A.MaturityDate,A.InterestDaysType,A.InterestPaymentType     
 From Instrument A     
 where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and A.status = 2 and
 @AcqDate < @SettledDate and A.MaturityDate >= @SettledDate and A.InstrumentPK = @InstrumentPK  
    

    
 INSERT INTO #InterestBondDivDays     
 select FundPK,InstrumentPK,AcqDate,DivDays,DivPayment,sum(CountDays) CountDays    
 from    
 (     
  select A.FundPK,A.InstrumentPK,A.AcqDate,    
  case when A.InterestDaysType in (2) then DATEDIFF(Day,dbo.FgetLastCouponDate(@SettledDate,A.InstrumentPK),dbo.FgetNextCouponDate(@SettledDate,A.InstrumentPK))      
    when A.InterestDaysType in (1,3,5,6,7) then 360 else 365  end DivDays,    
  case when A.InterestPaymentType in (1,4,7) then 12     
    when A.InterestPaymentType in (13) then 4     
     when A.InterestPaymentType in (16) then 2 else 1 end DivPayment,    
  case when A.InterestDaysType in (1,5,6,7) then     
    case when day(B.Date) = 31 then 0     
      when month(B.Date) = 2 and day(eomonth(B.Date)) = day(B.Date) then 31 - day(B.Date) else 1 end else 1 end CountDays    
  from #InterestBondData A    
  left join ZDT_WorkingDays B on B.Date > A.AcqDate and B.Date <= @SettledDate    
  where A.InstrumentTypePK in (2,3,8,9,11,13,14,15) and B.Date <= @SettledDate    
 ) A    
 group by FundPK,InstrumentPK,AcqDate,DivDays,DivPayment    
  
  

if (@ClientCode = '20') -- custom nikko , hitung interest lgsg dengan pengali hari
BEGIN
	 INSERT INTO #InterestBond    
	  (   
	   ValueDate ,    
	   InstrumentPK ,    
	   InstrumentID ,    
	   FundPK ,    
	   FundID,    
	   InterestAmount,    
	   TaxPercent    
	  )    
    
	 Select  
	   @SettledDate ,    
	   A.InstrumentPK ,    
	   C.ID InstrumentID ,    
	   A.FundPK ,    
	   isnull(D.ID,'') FundID,    
	   case when A.InstrumentTypePK in (2,13) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays /     
		 case when A.InterestDaysType in (2) then B.DivPayment else 1 end * 1000000* B.CountDays,0)      
		   else A.Balance * A.InterestPercent / 100 / B.DivDays /     
			 case when A.InterestDaysType in (2) then B.DivPayment else 1 end * B.CountDays  end InterestAmount,    
	   case when D.FundTypeInternal = 2 then @KPDTaxPercent     
		 else C.TaxExpensePercent end TaxPercent    
	 From #InterestBondData A      
	 LEFT JOIN #InterestBondDivDays B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate    
	 LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)    
	 LEFT JOIN dbo.Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)    

	 END
 ELSE
 BEGIN
	 INSERT INTO #InterestBond    
	  (   
	   ValueDate ,    
	   InstrumentPK ,    
	   InstrumentID ,    
	   FundPK ,    
	   FundID,    
	   InterestAmount,    
	   TaxPercent    
	  )    
    
	 Select  
	   @SettledDate ,    
	   A.InstrumentPK ,    
	   C.ID InstrumentID ,    
	   A.FundPK ,    
	   isnull(D.ID,'') FundID,    
	   case when A.InstrumentTypePK in (2,13) then A.Balance / 1000000 * ROUND(A.InterestPercent / 100 / B.DivDays /     
		 case when A.InterestDaysType in (2) then B.DivPayment else 1 end * 1000000,0) * B.CountDays     
		   else A.Balance * A.InterestPercent / 100 / B.DivDays /     
			 case when A.InterestDaysType in (2) then B.DivPayment else 1 end * B.CountDays  end InterestAmount,    
	   case when D.FundTypeInternal = 2 then @KPDTaxPercent     
		 else C.TaxExpensePercent end TaxPercent    
	 From #InterestBondData A      
	 LEFT JOIN #InterestBondDivDays B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate    
	 LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)    
	 LEFT JOIN dbo.Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)    

 END
    
 select @InterestAmount =  isnull(InterestAmount,0) from #InterestBond 
 select @GrossAmount = (@Volume * @Price/100) + isnull(@InterestAmount,0)

Select @InterestAmount InterestAmount, @GrossAmount GrossAmount
   ";

                        }


                        //cmd.Parameters.AddWithValue("@ValueDate", _omsBond.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _omsBond.InstrumentPK);
                        cmd.Parameters.AddWithValue("@SettledDate", _omsBond.SettledDate);
                        //cmd.Parameters.AddWithValue("@NextCouponDate", _omsBond.NextCouponDate);
                        //cmd.Parameters.AddWithValue("@AcqDate", _omsBond.LastCouponDate);
                        cmd.Parameters.AddWithValue("@Price", _omsBond.Price);
                        cmd.Parameters.AddWithValue("@Volume", _omsBond.Volume);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setGrossAmount(dr);
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


        private OMSBondForNetAmount setGrossAmount(SqlDataReader dr)
        {
            OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
            M_OMSBond.InterestAmount = dr["InterestAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["InterestAmount"]);
            M_OMSBond.GrossAmount = dr["GrossAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
            return M_OMSBond;
        }

               
        public void Get_FIFOBond(DateTime _dateFrom, int _trxtype)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxtype == 2)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
 
                        CREATE TABLE #tempFP
                        (
	                        DataPK INT,
	                        FundPK INT,
	                        AcqDate DATETIME,
	                        Balance NUMERIC(22,4),
	                        AvgPrice NUMERIC(18,4),
	                        InstrumentPK INT,
	                        RemainingBalance NUMERIC(22,4)

                        )


                        INSERT INTO #tempFP
                        SELECT ROW_NUMBER() OVER(ORDER BY A.FundPK ASC),A.FundPK,A.AcqDate,A.Balance,A.AvgPrice,A.InstrumentPK,A.Balance
                        FROM fundPosition A WHERE A.date = 
                        (
                        Select max(ValueDate) from endDayTrailsFundPortfolio where
                        valuedate < @Date  and status = 2
                        )
                        and status = 2
                        AND A.status = 2
                        ORDER BY A.AcqDate ASC

                        DECLARE @CInvestmentPK INT
                        DECLARE @CDealingPK INT
                        DECLARE @CInstrumentPK INT
                        DECLARE @CFundPK INT
                        DECLARE @CBalance NUMERIC(22,4)


                        DECLARE @CDataPK INT
                        DECLARE @FPAcqDate DATETIME
                        DECLARE @FPAcqPrice NUMERIC(8,4)
                        DECLARE @FPAcqVolume NUMERIC(22,4)
                        DECLARE @CCounterACQ int


                        DECLARE A CURSOR FOR
	                        SELECT InvestmentPK,DealingPK,InstrumentPK,FundPK,Volume FROM dbo.Investment 
	                        WHERE ValueDate = @Date 
	                        AND InstrumentTypePK NOT IN (1,5) and StatusInvestment = 2 and statusDealing <> 3 and TrxType = 2
	                        ORDER BY InvestmentPK asc
                        OPEN A
                        FETCH NEXT FROM A 
                        INTO @CInvestmentPK,@CDealingPK,@CInstrumentPK,@CFundPK,@CBalance
	
                        WHILE @@FETCH_STATUS = 0
                        BEGIN
	                        SET @CCounterACQ = 1

		                        UPDATE Investment
		                        SET AcqDate = NULL
		                        ,AcqVolume = 0
		                        ,AcqPrice = 0
                        ,AcqDate1 = NULL
		                        ,AcqVolume1 = 0
		                        ,AcqPrice1 = 0
                        ,AcqDate2 = NULL
		                        ,AcqVolume2 = 0
		                        ,AcqPrice2 = 0
                        ,AcqDate3 = NULL
		                        ,AcqVolume3 = 0
		                        ,AcqPrice3 = 0
                        ,AcqDate4 = NULL
		                        ,AcqVolume4 = 0
		                        ,AcqPrice4 = 0
                        ,AcqDate5 = NULL
		                        ,AcqVolume5 = 0
		                        ,AcqPrice5 = 0
                        ,AcqDate6 = NULL
		                        ,AcqVolume6 = 0
		                        ,AcqPrice6 = 0
                        ,AcqDate7 = NULL
		                        ,AcqVolume7 = 0
		                        ,AcqPrice7 = 0
                        ,AcqDate8 = NULL
		                        ,AcqVolume8 = 0
		                        ,AcqPrice8 = 0
                        ,AcqDate9 = NULL
		                        ,AcqVolume9 = 0
		                        ,AcqPrice9 = 0

		                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2 and statusdealing <> 3 


	                        WHILE (@CBalance > 0) 
	                        BEGIN
		                        SELECT TOP 1 @CDataPK = DataPK,@FPAcqDate = AcqDate, @FPAcqVolume = 
		                        CASE WHEN @CBalance <= RemainingBalance THEN @CBalance ELSE RemainingBalance END
		                        ,@FPAcqPrice = AvgPrice
		                        FROM #tempFP
		                        WHERE RemainingBalance <> 0 AND InstrumentPK = @CInstrumentPK AND FundPK = @CFundPK


		                        IF @CCounterACQ = 1
		                        BEGIN
			                        UPDATE Investment SET AcqDate = @FPAcqDate,AcqPrice = @FPAcqPrice, AcqVolume = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK  and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 2
		                        BEGIN
			                        UPDATE Investment SET AcqDate1 = @FPAcqDate,AcqPrice1 = @FPAcqPrice, AcqVolume1 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 3
		                        BEGIN
			                        UPDATE Investment SET AcqDate2 = @FPAcqDate,AcqPrice2 = @FPAcqPrice, AcqVolume2 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 4
		                        BEGIN
			                        UPDATE Investment SET AcqDate3 = @FPAcqDate,AcqPrice3 = @FPAcqPrice, AcqVolume3 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 5
		                        BEGIN
			                        UPDATE Investment SET AcqDate4 = @FPAcqDate,AcqPrice4 = @FPAcqPrice, AcqVolume4 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 6
		                        BEGIN
			                        UPDATE Investment SET AcqDate5 = @FPAcqDate,AcqPrice5 = @FPAcqPrice, AcqVolume5 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 7
		                        BEGIN
			                        UPDATE Investment SET AcqDate6 = @FPAcqDate,AcqPrice6 = @FPAcqPrice, AcqVolume6 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 8
		                        BEGIN
			                        UPDATE Investment SET AcqDate7 = @FPAcqDate,AcqPrice7 = @FPAcqPrice, AcqVolume7 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 9
		                        BEGIN
			                        UPDATE Investment SET AcqDate8 = @FPAcqDate,AcqPrice8 = @FPAcqPrice, AcqVolume8 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END
		                        IF @CCounterACQ = 10
		                        BEGIN
			                        UPDATE Investment SET AcqDate9 = @FPAcqDate,AcqPrice9 = @FPAcqPrice, AcqVolume9 = @FPAcqVolume
			                        WHERE InvestmentPK = @CInvestmentPK AND DealingPK = @CDealingPK and StatusInvestment = 2
		                        END

		                        UPDATE #tempFP SET RemainingBalance = 
		                        CASE WHEN RemainingBalance - @FPAcqVolume < 0 THEN 0 ELSE RemainingBalance - @FPAcqVolume end
		                        WHERE DataPK = @CDataPK 
		
		                        SET @CBalance = @CBalance - @FPAcqVolume

		                        SET @CCounterACQ = @CCounterACQ + 1
	                        END

	                        FETCH NEXT FROM A
	                        INTO @CInvestmentPK,@CDealingPK,@CInstrumentPK,@CFundPK,@CBalance
                        END
                        CLOSE A
                        DEALLOCATE A    ";

                            cmd.Parameters.AddWithValue("@Date", _dateFrom);
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
               
        public decimal Get_Yield(OMSBond _omsBond)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        decimal Yield;
                        Yield = Convert.ToDecimal(Financial.Yield(Convert.ToDateTime(_omsBond.SettledDate), Convert.ToDateTime(_omsBond.MaturityDate),
                        Convert.ToDouble(_omsBond.InterestRate) / 100, Convert.ToDouble(_omsBond.CostPrice), 100.0,
                        Tools.BondPaymentPeriodExcelConversion(Convert.ToInt32(_omsBond.PaymentPeriod)),
                        Tools.BondInterestBasisExcelConvertion(Convert.ToInt32(_omsBond.InterestDaysType)))
                        );

                        

                        return Yield;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public OMSBondForNetAmount Get_NetAmountForSettlement(OMSBondForNetAmount _omsBond)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_omsBond.InstrumentTypePK == 2 || _omsBond.InstrumentTypePK == 3 || _omsBond.InstrumentTypePK == 9 || _omsBond.InstrumentTypePK == 13 || _omsBond.InstrumentTypePK == 15)
                        {
                            cmd.CommandTimeout = 0;
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
@CurrencyID = C.ID,@CouponRate = A.InterestPercent,@InterestType = B.InterestType, 
@InterestDaysType = B.InterestDaysType,@InterestPaymentType = 12/D.Priority
From Investment A
left join instrument B on A.InstrumentPK = B.instrumentPK and B.status = 2
left join Currency C on B.CurrencyPK = C.CurrencyPK and C.status = 2
left join MasterValue D on B.InterestPaymentType = D.Code and D.Status = 2 and D.ID = 'InterestPaymentType'
where DealingPK = @DealingPK and A.HistoryPK = @HistoryPK


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
		
			set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari

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


            Declare @StartOfYear Datetime
            Declare @ParamAcqDate Datetime
            Declare @ParamAcqDate1 Datetime
            Declare @ParamAcqDate2 Datetime
            Declare @ParamAcqDate3 Datetime
            Declare @ParamAcqDate4 Datetime
            Declare @ParamAcqDate5 Datetime

            set @StartOfYear = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101) 
            set @ParamAcqDate = convert(varchar(10),@AcqDate,101) 
            set @ParamAcqDate1 = convert(varchar(10),@AcqDate1,101) 
            set @ParamAcqDate2 = convert(varchar(10),@AcqDate2,101) 
            set @ParamAcqDate3 = convert(varchar(10),@AcqDate3,101) 
            set @ParamAcqDate4 = convert(varchar(10),@AcqDate4,101) 
            set @ParamAcqDate5 = convert(varchar(10),@AcqDate5,101) 

            IF (@ParamAcqDate <= @StartOfYear)
            BEGIN
	            set @AcqDate = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate1 <= @StartOfYear)
            BEGIN
	            set @AcqDate1 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate2 <= @StartOfYear)
            BEGIN
	            set @AcqDate2 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate3 <= @StartOfYear)
            BEGIN
	            set @AcqDate3 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate4 <= @StartOfYear)
            BEGIN
	            set @AcqDate4 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END
            IF (@ParamAcqDate5 <= @StartOfYear)
            BEGIN
	            set @AcqDate5 = convert(varchar(10),DATEADD(yy, DATEDIFF(yy,0,@ValueDate), 0),101)   
            END

		
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

			set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
			set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
			set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
			set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
			set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
			set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end

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
				set @ValuePerUnit = 1000000
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

--if @InterestType <> 3 --ZERO COUPON
--BEGIN
	if @InstrumentTypePK in (3,8,9,15)
	BEGIN
		set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
		set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
	END
	ELSE IF @InstrumentTypePK in (2,13)
	BEGIN
		--IF @BitIsRounding = 1
		--BEGIN
			set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
			 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0) * @Days -- DAYS DI KALI SETELAH DAPAT INTEREST DAILY
			set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
		--END
		--ELSE
		--BEGIN
		--	set @AccuredInterestAmount = @Volume / @ValuePerUnit * (@CouponRate / 100 
		--	* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit)
		--	set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
		--END
	END
--END


set @TotalTaxCapGain = isnull(@TaxCapGainAcq,0) + isnull(@TaxCapGainAcq1,0) + isnull(@TaxCapGainAcq2,0) + isnull(@TaxCapGainAcq3,0) + isnull(@TaxCapGainAcq4,0) + isnull(@TaxCapGainAcq5,0)

--set @TotalTaxCapGain = case when isnull(@TaxCapGainAcq,0) > 0 then  isnull(@TaxCapGainAcq,0) else 0 end 
--+ case when isnull(@TaxCapGainAcq1,0) > 0 then isnull(@TaxCapGainAcq1,0) else 0 end 
--+ case when isnull(@TaxCapGainAcq2,0) > 0 then isnull(@TaxCapGainAcq2,0) else 0 end
--+ case when isnull(@TaxCapGainAcq3,0) > 0 then isnull(@TaxCapGainAcq3,0) else 0 end
--+ case when isnull(@TaxCapGainAcq4,0) > 0 then isnull(@TaxCapGainAcq4,0) else 0 end 
--+ case when isnull(@TaxCapGainAcq5,0) > 0 then isnull(@TaxCapGainAcq5,0) else 0 end 

set @TotalTaxAI = isnull(@TaxAIAcq,0) + isnull(@TaxAIAcq1,0) + isnull(@TaxAIAcq2,0) + isnull(@TaxAIAcq3,0) + isnull(@TaxAIAcq4,0) + isnull(@TaxAIAcq5,0)

--set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
--+ case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
--+ case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
--+ case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
--+ case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
--+ case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 

set @TotalCapGain = isnull(@CapGainAcq,0) + isnull(@CapGainAcq1,0) + isnull(@CapGainAcq2,0) + isnull(@CapGainAcq3,0) + isnull(@CapGainAcq4,0) + isnull(@CapGainAcq5,0)
set @TotalAI = isnull(@AIAcq,0) + isnull(@AIAcq1,0) + isnull(@AIAcq2,0) + isnull(@AIAcq3,0) + isnull(@AIAcq4,0) + isnull(@AIAcq5,0)


--IF (@TotalTaxCapGain > 0)
--BEGIN
	    set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
	    set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)
--END
--ELSE
--BEGIN

--    if (abs(@TotalTaxCapGain) >= abs(@TotalTaxAI))
--    BEGIN

--	    set @TotalTax = 0
--	    set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)
 --   END
--END


--Select @TotalTaxCapGain taxCapGain,@TotalCapGain CapGain,@TotalAI Ai,@TotalTaxAI taxAi,@TotalTax TotalTax,@NetAmount Net,@GrossAmount gross, @AccuredInterestAmount Holding

                          
--Update Investment set IncomeTaxInterestAmount = @TotalTaxAI, IncomeTaxGainAmount = @TotalTaxCapGain, TotalAmount = @NetAmount,DoneAccruedInterest = @AccuredInterestAmount
--where DealingPK = @DealingPK and HistoryPK = @HistoryPK

--Select isnull(@AccuredInterestAmount,0) InterestAmount, isnull(@TotalTaxAI,0) IncomeTaxInterestAmount,isnull(@TotalTaxCapGain,0) IncomeTaxGainAmount,
--isnull(@GrossAmount,0) GrossAmount, isnull(@NetAmount,0) NetAmount 
   ";

                        }

                        cmd.Parameters.AddWithValue("@DealingPK", _omsBond.DealingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _omsBond.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _omsBond.ValueDate);
                        //cmd.Parameters.AddWithValue("@InstrumentPK", _omsBond.InstrumentPK);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _omsBond.InstrumentTypePK);
                        if (_omsBond.SettledDate == null)
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SettledDate", _omsBond.SettledDate);
                        }
                        cmd.Parameters.AddWithValue("@NextCouponDate", _omsBond.NextCouponDate);
                        cmd.Parameters.AddWithValue("@LastCouponDate", _omsBond.LastCouponDate);
                        cmd.Parameters.AddWithValue("@TrxType", _omsBond.TrxType);
                        cmd.Parameters.AddWithValue("@Price", _omsBond.Price);
                        cmd.Parameters.AddWithValue("@Volume", _omsBond.Volume);
                        cmd.Parameters.AddWithValue("@AcqPrice", _omsBond.AcqPrice);
                        if (_omsBond.AcqDate == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate", _omsBond.AcqDate);
                        }

                        if (_omsBond.AcqDate1 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate1", _omsBond.AcqDate1);
                        }

                        if (_omsBond.AcqDate2 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate2", _omsBond.AcqDate2);
                        }

                        if (_omsBond.AcqDate3 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate3", _omsBond.AcqDate3);
                        }

                        if (_omsBond.AcqDate4 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate4", _omsBond.AcqDate4);
                        }

                        if (_omsBond.AcqDate5 == null)
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AcqDate5", _omsBond.AcqDate5);
                        }

                        cmd.Parameters.AddWithValue("@AcqVolume", _omsBond.AcqVolume);
                        cmd.Parameters.AddWithValue("@AcqPrice1", _omsBond.AcqPrice1);
                        cmd.Parameters.AddWithValue("@AcqVolume1", _omsBond.AcqVolume1);

                        cmd.Parameters.AddWithValue("@AcqPrice2", _omsBond.AcqPrice2);
                        cmd.Parameters.AddWithValue("@AcqVolume2", _omsBond.AcqVolume2);

                        cmd.Parameters.AddWithValue("@AcqPrice3", _omsBond.AcqPrice3);
                        cmd.Parameters.AddWithValue("@AcqVolume3", _omsBond.AcqVolume3);

                        cmd.Parameters.AddWithValue("@AcqPrice4", _omsBond.AcqPrice4);
                        cmd.Parameters.AddWithValue("@AcqVolume4", _omsBond.AcqVolume4);

                        cmd.Parameters.AddWithValue("@AcqPrice5", _omsBond.AcqPrice5);
                        cmd.Parameters.AddWithValue("@AcqVolume5", _omsBond.AcqVolume5);

                        cmd.Parameters.AddWithValue("@TaxCapitaGainPercent", _omsBond.TaxCapitaGainPercent);
                        cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _omsBond.TaxInterestPercent);
                        //cmd.Parameters.AddWithValue("@BitIsRounding", _omsBond.BitIsRounding);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    //M_OMSBond.RealisedAmount = Convert.ToDecimal(dr["RealisedAmount"]);
                                    M_OMSBond.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
                                    M_OMSBond.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
                                    M_OMSBond.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
                                    M_OMSBond.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                    M_OMSBond.NetAmount = Convert.ToDecimal(dr["NetAmount"]);

                                    return M_OMSBond;
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

        public string ImportOMSBond(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;
                // delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "truncate table ZOMSBond";
                        cmd.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.ZOMSBond";
                    bulkCopy.WriteToServer(CreateDataTableFromOMSBondExcelFile(_fileSource));
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText =
                          @"
                           DECLARE 
                                @FundPK int, 
                                @Date   date,
	                            @InstrumentPK int,
	                            @Balance numeric(22,2),
	                            @TrailsPK int,
	                            @InstrumentName nvarchar(100),
	                            @FundName nvarchar(100)
 

                             if Exists( Select * from ZOMSBond 
                            where Instrument not in
                            (
	                            select ID from Instrument where status in (1,2)
                            ))
                            BEGIN

                            declare @Message nvarchar(max)
                            set @Message = ' '
                            select distinct @Message = @Message  + Instrument + ' , '  from ZOMSBond 
	                            where Instrument not in
		                            (
			                            select ID from Instrument where status in (1,2)
		                            )

	                            Select top 1  'false' result,substring(@Message,1,len(@Message)-1) + 'not found in master Instrument' ResultDesc from ZOMSBond 
	                            where Instrument not in
		                            (
			                            select ID from Instrument where status in (1,2)
		                            )
                            END
                            ELSE
                            BEGIN
	                            set @Message = ''
	                            DECLARE A CURSOR FOR 
		                            select B.FundPK,C.InstrumentPK,cast(Nominal as numeric(22,2)) Balance,Cast(TransactionDate as date) Valuedate,A.Instrument,A.FundSell from ZOMSBond A
		                            left join Fund B on A.FundSell = B.ID and B.Status in (1,2)
		                            left join Instrument C on A.Instrument = C.ID and C.Status in (1,2)
		                            where A.Fundbuy = ''
	                            OPEN A
 
	                            FETCH NEXT FROM A INTO @FundPK,@InstrumentPK,@Balance,@Date,@InstrumentName,@FundName
 
	                            WHILE @@FETCH_STATUS = 0
		                            BEGIN
			                            select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio 
			                            where ValueDate = 
			                            (
			                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date  and FundPK = @FundPK   
			                            )
			                            and status = 2 and FundPK = @FundPK       

			                            Declare @CurrBalance numeric (18,4)

			                            select @CurrBalance =  A.Balance + sum(isnull(B.MovBalance,0)) from (

			                            select AB.InstrumentPK,AB.InstrumentID,sum(isnull(AB.Balance,0)) Balance,AB.CurrencyID,AB.AcqDate,AB.InterestPercent,AB.MaturityDate,AB.TrxBuy,AB.TrxBuyType, AB.AvgPrice,AB.InterestPaymentType from(
			                            select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(A.Balance) Balance,'IDR' CurrencyID,'1900-01-01' AcqDate,A.InterestPercent,
			                            A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 AvgPrice,B.InterestPaymentType from FundPosition A    
			                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
			                            Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
			                            where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15) and A.status = 2         
			                            group by A.InstrumentPK,B.ID,B.Name,A.InterestPercent,A.MaturityDate,B.InterestPaymentType
			                            union all	
			                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
			                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR','1900-01-01' AcqDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 DonePrice,B.InterestPaymentType
			                            from Investment A
			                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
			                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
			                            where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
			                            and FundPK = @FundPK and TrxType  = 1  
			                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,
			                            TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType
			                            )AB
			                            group By AB.InstrumentPK,AB.InstrumentID,AB.CurrencyID,AB.AcqDate,AB.InterestPercent,AB.MaturityDate,AB.TrxBuy,AB.TrxBuyType, AB.AvgPrice,AB.InterestPaymentType


			                            ) A 
			                            left join
			                            (
			                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
			                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR' CurrencyID,'1900-01-01' ValueDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 DonePrice,B.InterestPaymentType
			                            from Investment A
			                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
			                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
			                            where ValueDate = @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and A.instrumentTypePK in (2,3,8,9,13,15)
			                            and FundPK = @FundPK and TrxType  = 2
			                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType
			                            ) B on  A.InstrumentPK = B.InstrumentPK
			                            left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status = 2
			                            where A.InstrumentPK = @InstrumentPK
			                            Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,C.Priority
			                            IF (@Balance > @CurrBalance)
			                            BEGIN
				                            set @Message = @Message + 'Instrument : ' + @InstrumentName + ', Fund : ' + @FundName + ' , '
			                            END


			                            FETCH NEXT FROM A INTO @FundPK,@InstrumentPK,@Balance,@Date,@InstrumentName,@FundName
		                            END;
 
	                            CLOSE A;
 
	                            DEALLOCATE A;

	                            if @Message != ''
	                            begin
		                            set @Message = substring(@Message,1,len(@Message)-1) + ' is Short Sell, please check again!'
		                            select 'false' result, @Message ResultDesc
	                            end
	                            else
		                            select 'true' result,'' ResultDesc
	
                            END     

                            ";

                        using (SqlDataReader dr01 = cmd2.ExecuteReader())
                        {
                            dr01.Read();
                            if (Convert.ToString(dr01["Result"]) == "false")
                            {
                                _msg = Convert.ToString(dr01["ResultDesc"]);
                                return _msg;
                            }
                            else
                            {

                                conn.Close();
                                conn.Open();
                                using (SqlCommand cmd = conn.CreateCommand())
                                {
                                    cmd.CommandText = @"
                                    DECLARE 
                                        @FundSell int, 
                                        @FundBuy  int,
                                        @InstrumentID  int,
                                        @Volume  numeric(32,8),
                                        @SellPrice numeric(22,4),
	                                    @BuyPrice numeric(22,4),
	                                    @SettlementDate date,
	                                    @ValueDate date,
	                                    @Type int,
	                                    @InstrumentTypePK int,
	                                    @TrxID nvarchar(20),
	                                    @LastCouponDate date,
	                                    @NextCouponDate date,
	                                    @LotInShare int,
                                        @Amount numeric(32,8),
                                        @InterestPercent numeric(18,8),
                                        @TaxExpensePercent numeric(18,8),
	                                    @MaturityDate date,
                                        @MaxInvestmentPK int,
	                                    @PeriodPK int,
	                                    @LastNo int,   
                                        @Reference nvarchar(20),
                                        @TrxName nvarchar(20),
	                                    @InterestDaysType int,
	                                    @InterestPaymentType int

										Declare @AccuredInterestAmount numeric(22,4)
										Declare @GrossAmount numeric(22,4)
										Declare @CurrencyPK int
										Declare @InterestType int
										Declare @CouponRate numeric(8,4)
										Declare @ValuePerUnit int
										Declare @Days int
										Declare @DivDays int
										Declare @CurrencyID nvarchar(20)
										Declare @Price numeric(18,8)

 
                                    DECLARE A CURSOR
                                    FOR 
	                                    select isnull(FSell.FundPK,0), isnull(FBuy.FundPK,0), I.InstrumentPK,A.Nominal,A.SellPrice,A.BuyPrice,A.SettlementDate, A.TransactionDate, case when UPPER(LTRIM(RTRIM(A.Type))) = 'BUY' then 1 when UPPER(LTRIM(RTRIM(A.Type))) = 'SELL' then 2 when UPPER(LTRIM(RTRIM(A.Type))) = 'CROSSFUND' then 3 else 0 end, I.InstrumentTypePK,
	                                    UPPER(LTRIM(RTRIM(A.Type))),case when A.SettlementDate >= dbo.Fgetnextcoupondate(A.TransactionDate,I.InstrumentPK) then dbo.FgetLastCouponDate(A.SettlementDate,I.InstrumentPK) else dbo.FgetLastCouponDate(A.TransactionDate,I.InstrumentPK) end,
										case when A.SettlementDate >= dbo.Fgetnextcoupondate(A.TransactionDate,I.InstrumentPK) then dbo.Fgetnextcoupondate(A.SettlementDate,I.InstrumentPK) else dbo.Fgetnextcoupondate(A.TransactionDate,I.InstrumentPK) end,I.LotInShare,I.InterestPercent,I.MaturityDate,I.InterestDaysType,I.InterestPaymentType,I.TaxExpensePercent from ZOMSBond A
	                                    left join Fund FBuy on A.Fundbuy = FBuy.ID and FBuy.Status in (1,2)
	                                    left join Fund FSell on A.FundSell = FSell.ID and FSell.Status in (1,2)
	                                    left join Instrument I on A.Instrument = I.ID and I.Status in (1,2)
										where UPPER(LTRIM(RTRIM(A.Type))) <> ''
                                    OPEN A;
 
                                    FETCH NEXT FROM A INTO 
                                        @FundSell, @FundBuy, @InstrumentID, @Volume, @SellPrice, @BuyPrice, @SettlementDate, @ValueDate, @Type,@InstrumentTypePK,@TrxID,@LastCouponDate,@NextCouponDate,@LotInShare,@InterestPercent,@MaturityDate,@InterestDaysType,@InterestPaymentType,@TaxExpensePercent
 
                                    WHILE @@FETCH_STATUS = 0
                                        BEGIN
		                                    select @MaxInvestmentPK = max(InvestmentPK) + 1 from Investment
		                                    select @PeriodPK = periodpk from Period where @ValueDate between DateFrom and DateTo
		                                    if @Type = 3
		                                    begin
			                                    --sell
			                                    set @TrxName = 'INV'
			                                    set @Type = 2
			                                    set @TrxID = 'SELL'
			                                    if exists(Select Top 1 * from cashierReference where Type = @TrxName And PeriodPK = @PeriodPK    
                                                and substring(right(reference,4),1,2) = month(@ValueDate))       
                                                BEGIN   
					                                    Select @LastNo = max(No) +  1 From CashierReference where Type = @TrxName And PeriodPK = @periodPK and   
					                                    substring(right(reference,4),1,2) = month(@ValueDate)    
						  
					                                    Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END  
					                                    + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
					                                    Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @TrxName And PeriodPK = @periodPK    
					                                    and substring(right(reference,4),1,2) = month(@ValueDate)    
                                                END    
                                                ELSE 
			                                    BEGIN      
					                                    Set @Reference = '1/' +  Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
					                                    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)  

					                                    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@TrxName,@Reference,1 from CashierReference   
                                                END       

												set @Price = @SellPrice

												Select @CurrencyID = B.ID,@InterestDaysType = A.InterestDaysType,@InterestType = A.InterestType 
												,@CouponRate = A.InterestPercent, @InterestPaymentType = 12/C.Priority,
												@InstrumentTypePK = A.InstrumentTypePK
												from Instrument A
												left join Currency B on A.CurrencyPK = B.CurrencyPK and B.Status = 2
												left join MasterValue C on A.InterestPaymentType = C.Code and C.status = 2 and C.ID = 'InterestPaymentType'
												where instrumentPK = @InstrumentID and A.status = 2

												if @CurrencyID = 'IDR'
												begin	
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
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
															then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettlementDate)) 
																else abs(datediff(day, @SettlementDate, @LastCouponDate)) end -- pembagi hari


																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
															end
														end

														if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
														begin
		
														if @InstrumentTypePK = 9
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
			
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days/ 360
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
														else
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
		
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
			
														end
												end

												if @CurrencyID = 'USD' -- USD
												begin
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
													END
													ELSE
													BEGIN
														if @InstrumentTypePK in (2) -- [Govt Bond]
														BEGIN	
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @NextCouponDate))
																set @Days = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @SettlementDate))

																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (3) -- Corp Bond
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (9) -- Corp Bond, instrumenttype :  Medium Term Notes
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * @Days/ 360 
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
		
													END
												end

			                                    INSERT INTO [dbo].[Investment]  
			                                    ([InvestmentPK],[HistoryPK],[StatusInvestment],[DealingPK],[StatusDealing],[SettlementPK],[StatusSettlement],[ValueDate],[PeriodPK],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID], 
			                                    [CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[Volume], 
			                                    [Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[LastCouponDate],[NextCouponDate], 
			                                    [MaturityDate],[SettlementDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice],[DoneAmount],[Tenor],[CommissionPercent],  
			                                    [LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent],[IncomeTaxSellPercent],[IncomeTaxInterestPercent], 
			                                    [IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount],[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount], 
			                                    [IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount],[CurrencyRate],[SettlementMode],[BoardType],[AcqPrice],[AcqVolume],[AcqDate],[AcqPrice1],[AcqVolume1],[AcqDate1],[AcqPrice2],[AcqVolume2],[AcqDate2],[AcqPrice3],[AcqVolume3],[AcqDate3],[AcqPrice4],[AcqVolume4],[AcqDate4],[AcqPrice5],[AcqVolume5],[AcqDate5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7],[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],  
			                                    [Category],[MarketPK],[TrxBuy],[TrxBuyType],[InterestDaysType],[InterestPaymentType],[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[DoneAccruedInterest],[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[BitBreakable],[CrossFundFromPK],[PurposeOfTransaction],[StatutoryType],[BitForeignTrx],[CPSafekeepingAccNumber],[PlaceOfSettlement],[FundSafekeepingAccountNumber],[SecurityCodeType],[BitHTM],[BankBranchPK],[BankPK],[TaxExpensePercent],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])
			                                    select @MaxInvestmentPK,1,1,0,0,0,0,@ValueDate,@PeriodPK,@ValueDate,@Reference,@InstrumentTypePK,@Type,@TrxID,
			                                    0,@InstrumentID,case when @Type = 1 then @FundBuy else @FundSell end,0,case when @Type = 1 then @BuyPrice else @SellPrice end,0,@LotInShare,0,@Volume,
			                                    case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,@InterestPercent,0,@AccuredInterestAmount,@LastCouponDate,@NextCouponDate,
			                                    @MaturityDate,@SettlementDate,'',0,@Volume,case when @Type = 1 then @BuyPrice else @SellPrice end,case when @Type = 1 then (@BuyPrice/100 * @Volume) else (@SellPrice/100 * @Volume) end,0,0,
			                                    0,0,0,0,0,0,0,
			                                    0,0,0,0,0,0,0,0,
			                                    0,0,case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,0,0,0,0,@Volume,@SettlementDate,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,
			                                    null,1,null,'',@InterestDaysType,@InterestPaymentType,0,null,1,0,0,0,0,0,0,@FundBuy,0,0,0,'','','',0,0,0,0,@TaxExpensePercent,@Userid,getdate(),null,null,getdate()

			                                    --buy
			                                    set @TrxName = 'INV'
			                                    set @Type = 1
			                                    set @TrxID = 'BUY'
			                                    select @MaxInvestmentPK = max(InvestmentPK) + 1 from Investment
			                                    if exists(Select Top 1 * from cashierReference where Type = @TrxName And PeriodPK = @PeriodPK    
                                                and substring(right(reference,4),1,2) = month(@ValueDate))       
                                                BEGIN   
					                                    Select @LastNo = max(No) +  1 From CashierReference where Type = @TrxName And PeriodPK = @periodPK and   
					                                    substring(right(reference,4),1,2) = month(@ValueDate)    
						  
					                                    Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END  
					                                    + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
					                                    Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @TrxName And PeriodPK = @periodPK    
					                                    and substring(right(reference,4),1,2) = month(@ValueDate)    
                                                END    
                                                ELSE 
			                                    BEGIN      
					                                    Set @Reference = '1/' +  Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
					                                    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)  

					                                    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@TrxName,@Reference,1 from CashierReference   
                                                END       

												set @Price = @SellPrice

												Select @CurrencyID = B.ID,@InterestDaysType = A.InterestDaysType,@InterestType = A.InterestType 
												,@CouponRate = A.InterestPercent, @InterestPaymentType = 12/C.Priority,
												@InstrumentTypePK = A.InstrumentTypePK
												from Instrument A
												left join Currency B on A.CurrencyPK = B.CurrencyPK and B.Status = 2
												left join MasterValue C on A.InterestPaymentType = C.Code and C.status = 2 and C.ID = 'InterestPaymentType'
												where instrumentPK = @InstrumentID and A.status = 2

												if @CurrencyID = 'IDR'
												begin	
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
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
															then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettlementDate)) 
																else abs(datediff(day, @SettlementDate, @LastCouponDate)) end -- pembagi hari


																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
															end
														end

														if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
														begin
		
														if @InstrumentTypePK = 9
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
			
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days/ 360
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
														else
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
		
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
			
														end
												end

												if @CurrencyID = 'USD' -- USD
												begin
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
													END
													ELSE
													BEGIN
														if @InstrumentTypePK in (2) -- [Govt Bond]
														BEGIN	
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @NextCouponDate))
																set @Days = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @SettlementDate))

																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (3) -- Corp Bond
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (9) -- Corp Bond, instrumenttype :  Medium Term Notes
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * @Days/ 360 
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
		
													END
												end

			                                    INSERT INTO [dbo].[Investment]  
			                                    ([InvestmentPK],[HistoryPK],[StatusInvestment],[DealingPK],[StatusDealing],[SettlementPK],[StatusSettlement],[ValueDate],[PeriodPK],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID], 
			                                    [CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[Volume], 
			                                    [Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[LastCouponDate],[NextCouponDate], 
			                                    [MaturityDate],[SettlementDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice],[DoneAmount],[Tenor],[CommissionPercent],  
			                                    [LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent],[IncomeTaxSellPercent],[IncomeTaxInterestPercent], 
			                                    [IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount],[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount], 
			                                    [IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount],[CurrencyRate],[SettlementMode],[BoardType],[AcqPrice],[AcqVolume],[AcqDate],[AcqPrice1],[AcqVolume1],[AcqDate1],[AcqPrice2],[AcqVolume2],[AcqDate2],[AcqPrice3],[AcqVolume3],[AcqDate3],[AcqPrice4],[AcqVolume4],[AcqDate4],[AcqPrice5],[AcqVolume5],[AcqDate5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7],[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],  
			                                    [Category],[MarketPK],[TrxBuy],[TrxBuyType],[InterestDaysType],[InterestPaymentType],[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[DoneAccruedInterest],[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[BitBreakable],[CrossFundFromPK],[PurposeOfTransaction],[StatutoryType],[BitForeignTrx],[CPSafekeepingAccNumber],[PlaceOfSettlement],[FundSafekeepingAccountNumber],[SecurityCodeType],[BitHTM],[BankBranchPK],[BankPK],[TaxExpensePercent],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])
			                                    select @MaxInvestmentPK,1,1,0,0,0,0,@ValueDate,@PeriodPK,@ValueDate,@Reference,@InstrumentTypePK,@Type,@TrxID,
			                                    0,@InstrumentID,case when @Type = 1 then @FundBuy else @FundSell end,0,case when @Type = 1 then @BuyPrice else @SellPrice end,0,@LotInShare,0,@Volume,
			                                    case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,@InterestPercent,0,@AccuredInterestAmount,@LastCouponDate,@NextCouponDate,
			                                    @MaturityDate,@SettlementDate,'',0,@Volume,case when @Type = 1 then @BuyPrice else @SellPrice end,case when @Type = 1 then (@BuyPrice/100 * @Volume) else (@SellPrice/100 * @Volume) end,0,0,
			                                    0,0,0,0,0,0,0,
			                                    0,0,0,0,0,0,0,0,
			                                    0,0,case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,0,0,0,0,@Volume,@SettlementDate,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,
			                                    null,1,null,'',@InterestDaysType,@InterestPaymentType,0,null,1,0,0,0,0,0,0,@FundSell,0,0,0,'','','',0,0,0,0,@TaxExpensePercent,@Userid,getdate(),null,null,getdate()
		                                    end
		                                    else
		                                    begin
		
			                                    set @TrxName = 'INV'
                                                if exists(Select Top 1 * from cashierReference where Type = @TrxName And PeriodPK = @PeriodPK    
                                                and substring(right(reference,4),1,2) = month(@ValueDate))       
                                                BEGIN   
					                                    Select @LastNo = max(No) +  1 From CashierReference where Type = @TrxName And PeriodPK = @periodPK and   
					                                    substring(right(reference,4),1,2) = month(@ValueDate)    
						  
					                                    Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END  
					                                    + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
					                                    Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @TrxName And PeriodPK = @periodPK    
					                                    and substring(right(reference,4),1,2) = month(@ValueDate)    
                                                END    
                                                ELSE 
			                                    BEGIN      
					                                    Set @Reference = '1/' +  Case when @TrxName = 'CP' then 'OUT' else    
					                                    Case When @TrxName = 'AR' then 'AR' else Case when @TrxName = 'AP' then 'AP' else    
					                                    case when @TrxName = 'ADJ' then 'ADJ' Else Case when @TrxName = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
					                                    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)  

					                                    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@TrxName,@Reference,1 from CashierReference   
                                                END       

												set @Price = case when @Type = 1 then @BuyPrice when @Type = 2 then @SellPrice else 0 end 

												Select @CurrencyID = B.ID,@InterestDaysType = A.InterestDaysType,@InterestType = A.InterestType 
												,@CouponRate = A.InterestPercent, @InterestPaymentType = 12/C.Priority,
												@InstrumentTypePK = A.InstrumentTypePK
												from Instrument A
												left join Currency B on A.CurrencyPK = B.CurrencyPK and B.Status = 2
												left join MasterValue C on A.InterestPaymentType = C.Code and C.status = 2 and C.ID = 'InterestPaymentType'
												where instrumentPK = @InstrumentID and A.status = 2

												if @CurrencyID = 'IDR'
												begin	
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
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
															then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettlementDate)) 
																else abs(datediff(day, @SettlementDate, @LastCouponDate)) end -- pembagi hari


																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
															end
														end

														if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
														begin
		
														if @InstrumentTypePK = 9
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
			
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days/ 360
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
														else
														begin
															set @ValuePerUnit = 1
															set @DivDays = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
															set @Days = case when @InterestDaysType in (6) 
															then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettlementDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) end -- pembagi hari
		
																set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														end
			
														end
												end

												if @CurrencyID = 'USD' -- USD
												begin
													if @InterestType = 3
													BEGIN
														set @AccuredInterestAmount = 0
														set @GrossAmount = @Volume * @price/100
														Select  @AccuredInterestAmount InterestAmount, @GrossAmount GrossAmount
														return
													END
													ELSE
													BEGIN
														if @InstrumentTypePK in (2) -- [Govt Bond]
														BEGIN	
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @NextCouponDate))
																set @Days = abs([dbo].[FGetDateDIffGovermentBond](@LastCouponDate, @SettlementDate))

																set @AccuredInterestAmount = @Volume / @ValuePerUnit * round(@CouponRate / 100 
																* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (3) -- Corp Bond
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
														Else if @InstrumentTypePK in (9) -- Corp Bond, instrumenttype :  Medium Term Notes
														BEGIN
																set @ValuePerUnit = 1
																set @DivDays = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) -- pembagi hari
																set @Days = abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettlementDate)) -- total hari

																	set @AccuredInterestAmount = @Volume * @Days/ 360 
																set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
														END
		
													END
												end

			                                    INSERT INTO [dbo].[Investment]  
			                                    ([InvestmentPK],[HistoryPK],[StatusInvestment],[DealingPK],[StatusDealing],[SettlementPK],[StatusSettlement],[ValueDate],[PeriodPK],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID], 
			                                    [CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[Volume], 
			                                    [Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[LastCouponDate],[NextCouponDate], 
			                                    [MaturityDate],[SettlementDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice],[DoneAmount],[Tenor],[CommissionPercent],  
			                                    [LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent],[IncomeTaxSellPercent],[IncomeTaxInterestPercent], 
			                                    [IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount],[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount], 
			                                    [IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount],[CurrencyRate],[SettlementMode],[BoardType],[AcqPrice],[AcqVolume],[AcqDate],[AcqPrice1],[AcqVolume1],[AcqDate1],[AcqPrice2],[AcqVolume2],[AcqDate2],[AcqPrice3],[AcqVolume3],[AcqDate3],[AcqPrice4],[AcqVolume4],[AcqDate4],[AcqPrice5],[AcqVolume5],[AcqDate5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7],[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],  
			                                    [Category],[MarketPK],[TrxBuy],[TrxBuyType],[InterestDaysType],[InterestPaymentType],[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[DoneAccruedInterest],[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[BitBreakable],[CrossFundFromPK],[PurposeOfTransaction],[StatutoryType],[BitForeignTrx],[CPSafekeepingAccNumber],[PlaceOfSettlement],[FundSafekeepingAccountNumber],[SecurityCodeType],[BitHTM],[BankBranchPK],[BankPK],[TaxExpensePercent],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])
			                                    select @MaxInvestmentPK,1,1,0,0,0,0,@ValueDate,@PeriodPK,@ValueDate,@Reference,@InstrumentTypePK,@Type,@TrxID,
			                                    0,@InstrumentID,case when @Type = 1 then @FundBuy else @FundSell end,0,case when @Type = 1 then @BuyPrice else @SellPrice end,0,@LotInShare,0,@Volume,
			                                    case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,@InterestPercent,0,@AccuredInterestAmount,@LastCouponDate,@NextCouponDate,
			                                    @MaturityDate,@SettlementDate,'',0,@Volume,case when @Type = 1 then @BuyPrice else @SellPrice end,case when @Type = 1 then (@BuyPrice/100 * @Volume)  else (@SellPrice/100 * @Volume) end,0,0,
			                                    0,0,0,0,0,0,0,
			                                    0,0,0,0,0,0,0,0,
			                                    0,0,case when @Type = 1 then @BuyPrice/100 * @Volume else @SellPrice/100 * @Volume end,0,0,0,0,@Volume,@SettlementDate,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,0,0,null,
			                                    null,1,null,'',@InterestDaysType,@InterestPaymentType,0,null,1,0,0,0,0,0,0,0,0,0,0,'','','',0,0,0,0,@TaxExpensePercent,@Userid,getdate(),null,null,getdate()

		                                    end

		


                                            FETCH NEXT FROM A INTO @FundSell, @FundBuy, @InstrumentID, @Volume, @SellPrice, @BuyPrice, @SettlementDate, @ValueDate, @Type,@InstrumentTypePK,@TrxID,@LastCouponDate,@NextCouponDate,@LotInShare,@InterestPercent,@MaturityDate,@InterestDaysType,@InterestPaymentType,@TaxExpensePercent
                                        END
 
                                    CLOSE A
 
                                    DEALLOCATE A
";
                                    cmd.Parameters.AddWithValue("@Userid", _userID);
                                    using (SqlDataReader dr = cmd.ExecuteReader())
                                    {
                                        _msg = "Import OMS Bond Success";
                                        return _msg;
                                    }
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

        private DataTable CreateDataTableFromOMSBondExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundSell";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fundbuy";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Instrument";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SellPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BuyPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SettlementDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransactionDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        int a;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        a = end.Row;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["FundSell"] = "";
                            else
                                dr["FundSell"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["FundBuy"] = "";
                            else
                                dr["FundBuy"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["Instrument"] = "";
                            else
                                dr["Instrument"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["Nominal"] = 0;
                            else
                                dr["Nominal"] = Convert.ToDecimal(worksheet.Cells[i, 4].Value).ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["SellPrice"] = 0;
                            else
                                dr["SellPrice"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null)
                                dr["BuyPrice"] = 0;
                            else
                                dr["BuyPrice"] = worksheet.Cells[i, 6].Value.ToString();

                            if (worksheet.Cells[i, 8].Value == null)
                                dr["SettlementDate"] = "";
                            else
                                dr["SettlementDate"] = worksheet.Cells[i, 8].Value.ToString();

                            if (worksheet.Cells[i, 7].Value == null)
                                dr["TransactionDate"] = "";
                            else
                                dr["TransactionDate"] = worksheet.Cells[i, 7].Value.ToString();

                            if (worksheet.Cells[i, 9].Value == null)
                                dr["Type"] = "";
                            else
                                dr["Type"] = worksheet.Cells[i, 9].Value.ToString();


                            //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                            if (dr["FundSell"].Equals(null) != true ||
                                dr["FundBuy"].Equals(null) != true ||
                                dr["Instrument"].Equals(null) != true ||
                                dr["Nominal"].Equals(null) != true ||
                                dr["SellPrice"].Equals(null) != true ||
                                dr["BuyPrice"].Equals(null) != true ||
                                dr["SettlementDate"].Equals(null) != true ||
                                dr["TransactionDate"].Equals(null) != true ||
                                dr["Type"].Equals(null) != true)
                            { dt.Rows.Add(dr); }
                            i++;

                        }
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public OMSBondForNetAmount Get_NetAmountSellBond(OMSBondForNetAmount _omsBond)
        {
            try
            {

                string _paramDealingPK = "";
                string _paramDealing = "";

                if (!_host.findString(_omsBond.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_omsBond.stringInvestmentFrom))
                {
                    _paramDealingPK = " And A.DealingPK in (" + _omsBond.stringInvestmentFrom + ") ";
                    _paramDealing = " And DealingPK in (" + _omsBond.stringInvestmentFrom + ") ";
                }
                else
                {
                    _paramDealingPK = " And A.DealingPK in (0) ";
                    _paramDealing = " And DealingPK in (0) ";
                }


                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
 
                    
Declare @DealingPK int
Declare @HistoryPK int
Declare @ValueDate datetime
Declare @InstrumentPK int
Declare @InstrumentTypePK int
Declare @SettledDate datetime
Declare @NextCouponDate datetime
Declare @LastCouponDate datetime
Declare @TrxType int
Declare @Price numeric(18,4)
Declare @Volume numeric(18,4)
Declare @AcqPrice numeric(18,4)
Declare @AcqDate datetime
Declare @AcqVolume numeric(18,4)
Declare @TaxCapitaGainPercent numeric(18,4)
Declare @IncomeTaxInterestPercent numeric(18,4)
Declare @BitIsRounding bit

Declare @AcqPrice1 numeric(18,4)
Declare @AcqDate1 datetime
Declare @AcqVolume1 numeric(18,4)
Declare @AcqPrice2 numeric(18,4)
Declare @AcqDate2 datetime
Declare @AcqVolume2 numeric(18,4)
Declare @AcqPrice3 numeric(18,4)
Declare @AcqDate3 datetime
Declare @AcqVolume3 numeric(18,4)
Declare @AcqPrice4 numeric(18,4)
Declare @AcqDate4 datetime
Declare @AcqVolume4 numeric(18,4)
Declare @AcqPrice5 numeric(18,4)
Declare @AcqDate5 datetime
Declare @AcqVolume5 numeric(18,4)
Declare @AcqPrice6 numeric(18,4)
Declare @AcqDate6 datetime
Declare @AcqVolume6 numeric(18,4)
Declare @AcqPrice7 numeric(18,4)
Declare @AcqDate7 datetime
Declare @AcqVolume7 numeric(18,4)
Declare @AcqPrice8 numeric(18,4)
Declare @AcqDate8 datetime
Declare @AcqVolume8 numeric(18,4)
Declare @AcqPrice9 numeric(18,4)
Declare @AcqDate9 datetime
Declare @AcqVolume9 numeric(18,4)


Declare @TaxExpensePercent numeric(8,4)

Declare @Days int
Declare @DivDays int
Declare @InterestDays int

Declare @CurrencyID nvarchar(20)
Declare @CouponRate numeric(8,4)
Declare @InterestType int
Declare @InterestDaysType int
Declare @InterestPaymentType int



Declare @AccuredInterestAmount numeric(22,4)
Declare @GrossAmount numeric(22,4)
Declare @NetAmount numeric(22,4)
Declare @ValuePerUnit int

Declare		@DaysAcq_2		int,
@DaysAcq1_2					int,
@DaysAcq2_2					int,
@DaysAcq3_2					int,
@DaysAcq4_2					int,
@DaysAcq5_2					int,
@DaysAcq6_2					int,
@DaysAcq7_2					int,
@DaysAcq8_2					int,
@DaysAcq9_2					int,
@CapGainAcq					decimal(22,4),
@CapGainAcq1				decimal(22,4),
@CapGainAcq2				decimal(22,4),
@CapGainAcq3				decimal(22,4),
@CapGainAcq4				decimal(22,4),
@CapGainAcq5				decimal(22,4),
@CapGainAcq6				decimal(22,4),
@CapGainAcq7				decimal(22,4),
@CapGainAcq8				decimal(22,4),
@CapGainAcq9				decimal(22,4),
@TaxCapGainAcq				decimal(22,4),
@TaxCapGainAcq1				decimal(22,4),
@TaxCapGainAcq2				decimal(22,4),
@TaxCapGainAcq3				decimal(22,4),
@TaxCapGainAcq4				decimal(22,4),
@TaxCapGainAcq5				decimal(22,4),
@TaxCapGainAcq6				decimal(22,4),
@TaxCapGainAcq7				decimal(22,4),
@TaxCapGainAcq8				decimal(22,4),
@TaxCapGainAcq9				decimal(22,4),
@AIAcq					decimal(22,4),
@AIAcq1					decimal(22,4),
@AIAcq2					decimal(22,4),
@AIAcq3					decimal(22,4),
@AIAcq4					decimal(22,4),
@AIAcq5					decimal(22,4),
@AIAcq6					decimal(22,4),
@AIAcq7					decimal(22,4),
@AIAcq8					decimal(22,4),
@AIAcq9					decimal(22,4),
@TaxAIAcq				decimal(22,4),
@TaxAIAcq1				decimal(22,4),
@TaxAIAcq2				decimal(22,4),
@TaxAIAcq3				decimal(22,4),
@TaxAIAcq4				decimal(22,4),
@TaxAIAcq5				decimal(22,4),
@TaxAIAcq6				decimal(22,4),
@TaxAIAcq7				decimal(22,4),
@TaxAIAcq8				decimal(22,4),
@TaxAIAcq9				decimal(22,4),
@TotalCapGain			decimal(22,4),
@TotalAI				decimal(22,4),
@TotalTaxCapGain		decimal(22,4),
@TotalTaxAI				decimal(22,4)


declare @BitNoCapitalGainTax bit
select @BitNoCapitalGainTax = isnull(BitNoCapitalGainTax,0) from CounterpartCommission where CounterpartPK = @CounterpartPK and status = 2

DECLARE A CURSOR FOR 

Select A.DealingPK,A.HistoryPK,A.ValueDate,A.InstrumentPK,A.InstrumentTypePK,
A.SettlementDate,A.NextCouponDate,A.LastCouponDate,
A.TrxType,A.OrderPrice,A.Volume,A.AcqPrice,A.AcqDate,
A.AcqVolume,A.IncomeTaxGainPercent,A.IncomeTaxInterestPercent,
A.BitIsRounding,
A.TaxExpensePercent,
C.ID,A.InterestPercent,B.InterestType, 
B.InterestDaysType,12/D.Priority,
A.AcqPrice1,A.AcqDate1,A.AcqVolume1,A.AcqPrice2,A.AcqDate2,A.AcqVolume2,A.AcqPrice3,A.AcqDate3,A.AcqVolume3,
A.AcqPrice4,A.AcqDate4,A.AcqVolume4,A.AcqPrice5,A.AcqDate5,A.AcqVolume5,A.AcqPrice6,A.AcqDate6,A.AcqVolume6,
A.AcqPrice7,A.AcqDate7,A.AcqVolume7,A.AcqPrice8,A.AcqDate8,A.AcqVolume8,A.AcqPrice9,A.AcqDate9,A.AcqVolume9
From Investment A
left join instrument B on A.InstrumentPK = B.instrumentPK and B.status = 2
left join Currency C on B.CurrencyPK = C.CurrencyPK and C.status = 2
left join MasterValue D on B.InterestPaymentType = D.Code and D.Status = 2 and D.ID = 'InterestPaymentType'
where StatusDealing = 1 and ValueDate between @DateFrom and @DateTo and TrxType = 2 and A.InstrumentTypePK in (2,3,8,9,13,15) 

" + _paramDealingPK + @"

Open A
Fetch Next From A
Into @DealingPK,@HistoryPK,@ValueDate,@InstrumentPK,@InstrumentTypePK,
@SettledDate,@NextCouponDate,@LastCouponDate,
@TrxType, @Price,@Volume,@AcqPrice,@AcqDate,
@AcqVolume,@TaxCapitaGainPercent,@IncomeTaxInterestPercent,
@BitIsRounding,@TaxExpensePercent,
@CurrencyID,@CouponRate,@InterestType, 
@InterestDaysType,@InterestPaymentType,
@AcqPrice1,@AcqDate1,@AcqVolume1,@AcqPrice2,@AcqDate2,@AcqVolume2,@AcqPrice3,@AcqDate3,@AcqVolume3,
@AcqPrice4,@AcqDate4,@AcqVolume4,@AcqPrice5,@AcqDate5,@AcqVolume5,@AcqPrice6,@AcqDate6,@AcqVolume6,
@AcqPrice7,@AcqDate7,@AcqVolume7,@AcqPrice8,@AcqDate8,@AcqVolume8,@AcqPrice9,@AcqDate9,@AcqVolume9

While @@FETCH_STATUS = 0
BEGIN  

set @TotalCapGain	 = 0	
set @TotalAI		 = 0
set @TotalTaxCapGain = 0	
set @TotalTaxAI		 = 0	

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

set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) else 0 end

set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) else 0 end

set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) else 0 end

set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) else 0 end

set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

end
	
if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
begin
		
set @ValuePerUnit = 1
set @DivDays = 0
			
set @Days = 0
		
set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari

set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end
set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100)  else 0 end
set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100)  else 0 end
set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100)  else 0 end
set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100)  else 0 end



set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100
		
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

set @DaysAcq6_2	= case when @InterestDaysType in (3) then case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate6, @SettledDate)) end else case when @AcqDate6 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate6)) end end -- total hari [acq date 6 > prev coupon date]
	
set @DaysAcq7_2	= case when @InterestDaysType in (3) then case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate7, @SettledDate)) end else case when @AcqDate7 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate7)) end end -- total hari [acq date 7 > prev coupon date]

set @DaysAcq8_2	= case when @InterestDaysType in (3) then case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate8, @SettledDate)) end else case when @AcqDate8 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate8)) end end -- total hari [acq date 8 > prev coupon date]
	
set @DaysAcq9_2	= case when @InterestDaysType in (3) then case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate9, @SettledDate)) end else case when @AcqDate9 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate9)) end end -- total hari [acq date 9 > prev coupon date]
						
		

set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end
set @DaysAcq6_2		= case when (isnull(@AcqDate6, '') = '' or year(@AcqDate6) = 1900) then 0 else @DaysAcq6_2 end
set @DaysAcq7_2		= case when (isnull(@AcqDate7, '') = '' or year(@AcqDate7) = 1900) then 0 else @DaysAcq7_2 end
set @DaysAcq8_2		= case when (isnull(@AcqDate8, '') = '' or year(@AcqDate8) = 1900) then 0 else @DaysAcq8_2 end
set @DaysAcq9_2		= case when (isnull(@AcqDate9, '') = '' or year(@AcqDate9) = 1900) then 0 else @DaysAcq9_2 end

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

set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) else 0 end

set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) else 0 end

set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) else 0 end

set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) else 0 end

set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq6 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq7 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq8 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq9 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq6	= @AIAcq6 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq7	= @AIAcq7 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq8	= @AIAcq8 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq9	= @AIAcq9 * @IncomeTaxInterestPercent / 100

end
		
if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
begin
		
set @ValuePerUnit = 1
set @DivDays = case when @InterestDaysType in (6) 
then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
			
set @Days = case when @InterestDaysType in (6) 
then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) end -- pembagi hari
		
set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari


set @DaysAcq_2 = case when @InterestDaysType in (6) then case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) end else case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate, @SettledDate)) end end -- total hari [acq date > prev coupon date]
set @DaysAcq1_2 = case when @InterestDaysType in (6) then case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate1, @SettledDate)) end else case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate1, @SettledDate)) end end -- total hari [acq date 1 > prev coupon date]
set @DaysAcq2_2 = case when @InterestDaysType in (6) then case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate2, @SettledDate)) end else case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate2, @SettledDate)) end end -- total hari [acq date 2 > prev coupon date]
set @DaysAcq3_2 = case when @InterestDaysType in (6) then case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate3, @SettledDate)) end else case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate3, @SettledDate)) end end -- total hari [acq date 3 > prev coupon date]
set @DaysAcq4_2 = case when @InterestDaysType in (6) then case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate4, @SettledDate)) end else case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate4, @SettledDate)) end end -- total hari [acq date 4 > prev coupon date]
set @DaysAcq5_2 = case when @InterestDaysType in (6) then case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate5, @SettledDate)) end else case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate5, @SettledDate)) end end -- total hari [acq date 5 > prev coupon date]
set @DaysAcq6_2 = case when @InterestDaysType in (6) then case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate6, @SettledDate)) end else case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate6, @SettledDate)) end end -- total hari [acq date 6 > prev coupon date]
set @DaysAcq7_2 = case when @InterestDaysType in (6) then case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate7, @SettledDate)) end else case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate7, @SettledDate)) end end -- total hari [acq date 7 > prev coupon date]
set @DaysAcq8_2 = case when @InterestDaysType in (6) then case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate8, @SettledDate)) end else case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate8, @SettledDate)) end end -- total hari [acq date 8 > prev coupon date]
set @DaysAcq9_2 = case when @InterestDaysType in (6) then case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate9, @SettledDate)) end else case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate9, @SettledDate)) end end -- total hari [acq date 9 > prev coupon date]


set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end
set @DaysAcq6_2		= case when (isnull(@AcqDate6, '') = '' or year(@AcqDate6) = 1900) then 0 else @DaysAcq6_2 end
set @DaysAcq7_2		= case when (isnull(@AcqDate7, '') = '' or year(@AcqDate7) = 1900) then 0 else @DaysAcq7_2 end
set @DaysAcq8_2		= case when (isnull(@AcqDate8, '') = '' or year(@AcqDate8) = 1900) then 0 else @DaysAcq8_2 end
set @DaysAcq9_2		= case when (isnull(@AcqDate9, '') = '' or year(@AcqDate9) = 1900) then 0 else @DaysAcq9_2 end



set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end
set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100)  else 0 end
set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100)  else 0 end
set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100)  else 0 end
set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100)  else 0 end



set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1
then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end else 0 end

set @AIAcq6 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 * @CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 * @CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq7 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 * @CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 * @CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq8 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 * @CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 * @CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq9 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 * @CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 * @CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType) end else 0 end


set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq6	= @AIAcq6 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq7	= @AIAcq7 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq8	= @AIAcq8 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq9	= @AIAcq9 * @IncomeTaxInterestPercent / 100

		
			
		
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
	set @ValuePerUnit = 1000000
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

--if @InterestType <> 3 --ZERO COUPON
--BEGIN
	if @InstrumentTypePK in (3,8,9,15)
	BEGIN
		set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
		set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
	END
	ELSE IF @InstrumentTypePK in (2,13)
	BEGIN
		--IF @BitIsRounding = 1
		--BEGIN
			set @AccuredInterestAmount = @Volume / @ValuePerUnit * (round(@CouponRate / 100 
			* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0))
			set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
		--END
		--ELSE
		--BEGIN
		--	set @AccuredInterestAmount = @Volume / @ValuePerUnit * (@CouponRate / 100 
		--	* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit)
		--	set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
		--END
	END
--END





IF(@ClientCode = '21')
BEGIN

    set @TotalTaxCapGain = isnull(@TaxCapGainAcq,0)
    + isnull(@TaxCapGainAcq1,0)
    + isnull(@TaxCapGainAcq2,0)
    + isnull(@TaxCapGainAcq3,0)
    + isnull(@TaxCapGainAcq4,0)
    + isnull(@TaxCapGainAcq5,0)
    
   

END
ELSE
BEGIN
    set @TotalTaxCapGain = case when isnull(@TaxCapGainAcq,0) > 0 then  isnull(@TaxCapGainAcq,0) else 0 end 
    + case when isnull(@TaxCapGainAcq1,0) > 0 then isnull(@TaxCapGainAcq1,0) else 0 end 
    + case when isnull(@TaxCapGainAcq2,0) > 0 then isnull(@TaxCapGainAcq2,0) else 0 end
    + case when isnull(@TaxCapGainAcq3,0) > 0 then isnull(@TaxCapGainAcq3,0) else 0 end
    + case when isnull(@TaxCapGainAcq4,0) > 0 then isnull(@TaxCapGainAcq4,0) else 0 end 
    + case when isnull(@TaxCapGainAcq5,0) > 0 then isnull(@TaxCapGainAcq5,0) else 0 end 
    + case when isnull(@TaxCapGainAcq6,0) > 0 then isnull(@TaxCapGainAcq6,0) else 0 end 
    + case when isnull(@TaxCapGainAcq7,0) > 0 then isnull(@TaxCapGainAcq7,0) else 0 end 
    + case when isnull(@TaxCapGainAcq8,0) > 0 then isnull(@TaxCapGainAcq8,0) else 0 end 
    + case when isnull(@TaxCapGainAcq9,0) > 0 then isnull(@TaxCapGainAcq9,0) else 0 end 

    set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
    + case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
    + case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
    + case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
    + case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
    + case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 
    + case when isnull(@TaxAIAcq6,0) > 0 then isnull(@TaxAIAcq6,0) else 0 end 
    + case when isnull(@TaxAIAcq7,0) > 0 then isnull(@TaxAIAcq7,0) else 0 end 
    + case when isnull(@TaxAIAcq8,0) > 0 then isnull(@TaxAIAcq8,0) else 0 end 
    + case when isnull(@TaxAIAcq9,0) > 0 then isnull(@TaxAIAcq9,0) else 0 end 
END



set @TotalCapGain = isnull(@CapGainAcq,0) + isnull(@CapGainAcq1,0) + isnull(@CapGainAcq2,0) + isnull(@CapGainAcq3,0) + isnull(@CapGainAcq4,0) + isnull(@CapGainAcq5,0) + isnull(@CapGainAcq6,0) + isnull(@CapGainAcq7,0) + isnull(@CapGainAcq8,0) + isnull(@CapGainAcq9,0)
set @TotalAI = isnull(@AIAcq,0) + isnull(@AIAcq1,0) + isnull(@AIAcq2,0) + isnull(@AIAcq3,0) + isnull(@AIAcq4,0) + isnull(@AIAcq5,0) + isnull(@AIAcq6,0) + isnull(@AIAcq7,0) + isnull(@AIAcq8,0) + isnull(@AIAcq9,0)




IF (@BitNoCapitalGainTax) = 1
BEGIN

	set @TotalTaxAI =  isnull(@TaxCapGainAcq,0) + isnull(@TaxCapGainAcq1,0) + isnull(@TaxCapGainAcq2,0) + isnull(@TaxCapGainAcq3,0) + isnull(@TaxCapGainAcq4,0)
	+ isnull(@TaxCapGainAcq5,0) + isnull(@TaxCapGainAcq6,0) + isnull(@TaxCapGainAcq7,0) + isnull(@TaxCapGainAcq8,0) + isnull(@TaxCapGainAcq9,0) + isnull(@TotalTaxAI,0) 

	set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
END
ELSE
BEGIN

	set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
END


IF (@ClientCode = '21')
BEGIN
set @TotalTaxAI = isnull(@TaxAIAcq,0) + isnull(@TaxAIAcq1,0) + isnull(@TaxAIAcq2,0) + isnull(@TaxAIAcq3,0) + isnull(@TaxAIAcq4,0) + isnull(@TaxAIAcq5,0)

set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
+ case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
+ case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
+ case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
+ case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
+ case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 
END

IF (@ClientCode = '21' AND @TrxType = 2)
BEGIN
    set @TotalTax = (isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxCapGain end,0) 
                    + isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxAI end,0))
END
ELSE
BEGIN
    set @TotalTax = (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) 
END



set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)


--Select @TotalTaxCapGain taxCapGain,@TotalCapGain CapGain,@TotalAI Ai,@TotalTaxAI taxAi,@TotalTax TotalTax,@NetAmount Net,@GrossAmount gross, @AccuredInterestAmount Holding

                          
Update Investment set IncomeTaxInterestAmount = @TotalTaxAI, IncomeTaxGainAmount = @TotalTaxCapGain, TotalAmount = @NetAmount,DoneAccruedInterest = @AccuredInterestAmount
where DealingPK = @DealingPK and HistoryPK = @HistoryPK



Fetch next From A Into @DealingPK,@HistoryPK,@ValueDate,@InstrumentPK,@InstrumentTypePK,
@SettledDate,@NextCouponDate,@LastCouponDate,
@TrxType, @Price,@Volume,@AcqPrice,@AcqDate,
@AcqVolume,@TaxCapitaGainPercent,@IncomeTaxInterestPercent,
@BitIsRounding,@TaxExpensePercent,
@CurrencyID,@CouponRate,@InterestType, 
@InterestDaysType,@InterestPaymentType,
@AcqPrice1,@AcqDate1,@AcqVolume1,@AcqPrice2,@AcqDate2,@AcqVolume2,@AcqPrice3,@AcqDate3,@AcqVolume3,
@AcqPrice4,@AcqDate4,@AcqVolume4,@AcqPrice5,@AcqDate5,@AcqVolume5,@AcqPrice6,@AcqDate6,@AcqVolume6,
@AcqPrice7,@AcqDate7,@AcqVolume7,@AcqPrice8,@AcqDate8,@AcqVolume8,@AcqPrice9,@AcqDate9,@AcqVolume9
END
Close A
Deallocate A

Select isnull(@AccuredInterestAmount,0) InterestAmount, isnull(@TotalTaxAI,0) IncomeTaxInterestAmount,isnull(@TotalTaxCapGain,0) IncomeTaxGainAmount,
isnull(@GrossAmount,0) GrossAmount, isnull(@NetAmount,0) NetAmount   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _omsBond.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _omsBond.DateTo);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _omsBond.CounterpartPK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    M_OMSBond.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
                                    M_OMSBond.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
                                    M_OMSBond.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
                                    M_OMSBond.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                                    M_OMSBond.NetAmount = Convert.ToDecimal(dr["NetAmount"]);



                                    return M_OMSBond;
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


        public int InvestmentFifoBond_ApproveOMSBondBySelected(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And InvestmentPK in (0) ";
                        }
                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @investmentPK int
                        declare @historyPK int
                        declare @DealingPK int
                        declare @Notes nvarchar(500)
                        declare @OrderPrice numeric(22,8)
                        declare @Volume numeric(22,2)
                        declare @Amount numeric(22,0)
                        declare @AccruedInterest numeric(22,0)
                        Declare @TaxPercentageBond numeric(8,4)
		                Declare @TaxPercentageCapitalGain numeric(8,4)                        
                        Declare @FundPK int
                        declare @IncomeTaxGainAmount numeric(30,8)
                        declare @AccruedInterestDays int
                        declare @AccruedInterestAmount numeric(30,8)
                        declare @DaysOfHoldingInterest int
                        declare @DaysOfLastCoupon int
                        declare @IncomeTaxInterestAmount numeric(30,8)
                        declare @TotalTaxIncomeAmount numeric(30,8)
                        declare @TaxAmount numeric(30,8)
                        declare @LastCouponDate date
                        declare @SettlementDate date
                        Declare @TaxInterestPercent numeric(22,4)
                        Declare @CutOffDate date

                        if @ClientCode = '03'
                            set @CutOffDate = '2019-10-18'
                        else if @ClientCode = '20'
                            set @CutOffDate = '2020-06-19'
                        else if @ClientCode = '21'
                            set @CutOffDate = '2020-09-30'
                        else if @ClientCode = '22'
                            set @CutOffDate = '2020-08-28'

                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = '' and Status = 2   
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)   
                        Select @Time,'InvestmentInstruction_RejectOMSBondBySelected','Investment',InvestmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Investment where ValueDate between @DateFrom and @DateTo and statusInvestment = 1 and SelectedInvestment  = 1  and  InstrumentTypePK = @InstrumentTypePK and TrxType = @TrxType

                         DECLARE A CURSOR FOR 
	                            Select InvestmentPK From investment 
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom 
								" + _paramInvestmentPK + @" 
								and  InstrumentTypePK in (2,3,8,9,13,15) and TrxType = @TrxType 

                        Open A
                        Fetch Next From A
                        Into @InvestmentPK

                        While @@FETCH_STATUS = 0
                        BEGIN              
							declare @instrumentpk int
							declare @Price numeric(22,4)
							declare @DoneVolume numeric(22,4)
							declare @Counter int
							declare @maxFifoDate date

							if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
							create table #tableFifoSelect
							(
								FifoBondPositionPK int,
								InvestmentPK int,
								AcqDate date,
								AcqVolume numeric(22,4),
								AcqPrice numeric(22,4)
							)
							CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


							if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
							create table #tableInvest
							(
								FifoBondPositionPK int,
								InvesmentBuyPK int,
								InvesmentSellPK int,
								AcqDate date,
								AcqVolume numeric(22,4),
								AcqPrice numeric(22,4),
								RemainingVolume numeric(22,4)
							)
							CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

							select @instrumentpk = InstrumentPK, @FundPK = FundPK, @volume = DoneVolume, @DoneVolume = DoneVolume from investment where InvestmentPK = @investmentpk and StatusInvestment = 1 and TrxType = 2

                            select @maxFifoDate = isnull(max(CutOffDate),@CutOffDate) from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk

							if exists (select * from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk )
							begin

								insert into #tableFifoSelect(FifoBondPositionPK,InvestmentPK,AcqDate,AcqVolume,AcqPrice)
								select A.FifoBondPositionPK,A.InvestmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
								select FifoBondPositionPK,0 InvestmentPK,AcqDate,RemainingVolume DoneVolume,AcqPrice from FifoBondPosition 
										where FundPK = @fundpk and InstrumentPk = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and status in (1,2)
										union all
										select 0,InvestmentBuyPK,AcqDate,RemainingVolume,AcqPrice from FifoBondPositionTemp
										where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0)
								)A 
								order by A.AcqDate,A.InvestmentPK
							end
							else
							begin
								insert into #tableFifoSelect(FifoBondPositionPK,InvestmentPK,AcqDate,AcqVolume,AcqPrice)
								select A.A,A.InvestmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
									select 0 A,InvestmentBuyPK InvestmentPK,AcqDate,RemainingVolume DoneVolume,AcqPrice from FifoBondPositionTemp
									where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0)
								)A 
								order by A.AcqDate,A.InvestmentPK
							end
								

							--select * from #tableFifoSelect
							--order by AcqDate

							set @Counter = 0

							declare @FifobondPositionPK int
							declare @AcqDate date
							declare @AcqVolume numeric(22,4)
							declare @AcqPrice numeric(22,4)
							declare @Query nvarchar(500)
							declare @InvestmentBuyPK int 

							DECLARE AB CURSOR FOR   
								select FifoBondPositionPK,InvestmentPK,AcqVolume,AcqPrice,AcqDate from #tableFifoSelect order by AcqDate
							OPEN AB  
							FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentBuyPK,@AcqVolume,@AcqPrice,@AcqDate
  
							WHILE @@FETCH_STATUS = 0  
							BEGIN  
								if @DoneVolume  - @AcqVolume > 0
								begin
									insert into #tableInvest(FifoBondPositionPK,InvesmentBuyPK,InvesmentSellPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
									select @FifobondPositionPK,@InvestmentBuyPK,@investmentpk,@AcqDate,@AcqVolume,@AcqPrice,0
		
									if @Counter = 0
										set @Query = 'update Investment set AcqDate = ''' + Cast(@AcqDate as nvarchar) +''', AcqPrice = ' + cast(@AcqPrice as nvarchar) + ', AcqVolume = ' + cast(@AcqVolume as nvarchar) + ' where StatusInvestment = 1 and InvestmentPK = ' + cast(@InvestmentPK as nvarchar)
									else
										set @Query = 'update Investment set AcqDate' + Cast(@Counter as nvarchar) + ' = ''' + Cast(@AcqDate as nvarchar) +''', AcqPrice' + Cast(@Counter as nvarchar) + ' = ' + cast(@AcqPrice as nvarchar) + ', AcqVolume' + Cast(@Counter as nvarchar) + ' = ' + cast(@AcqVolume as nvarchar) + ' where StatusInvestment = 1 and InvestmentPK = ' + cast(@InvestmentPK as nvarchar)
									exec(@Query)

									if @FifobondPositionPK != 0
									update FiFoBondPosition set RemainingVolume = 0, investmentpk = @investmentpk where FiFoBondPositionPK = @FifobondPositionPK and status in (1,2)

									if @InvestmentBuyPK !=0
									begin
										if exists(select * from FiFoBondPositionTemp where InvestmentBuyPK = @InvestmentBuyPK and RemainingVolume != 0)
											update FifoBondPositionTemp set RemainingVolume = 0, InvestmentSellPK = @investmentpk where InvestmentBuyPK = @InvestmentBuyPK 
										else
											insert into FifoBondPositionTemp(InvestmentBuyPK,InvestmentSellPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume,FundPK,InstrumentPK)
											select @InvestmentBuyPK,@investmentpk,@AcqDate,@AcqVolume,@AcqPrice,0,@FundPK,@instrumentpk
									end

									set @DoneVolume = @DoneVolume - @AcqVolume
									set @Counter = @Counter + 1
								end
								else
								begin
									insert into #tableInvest(FifoBondPositionPK,InvesmentBuyPK,InvesmentSellPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
									select @FifobondPositionPK,@InvestmentBuyPK,@investmentpk,@AcqDate,@AcqVolume,@AcqPrice,@AcqVolume - @DoneVolume
		
									if @Counter = 0
										set @Query = 'update Investment set AcqDate = ''' + Cast(@AcqDate as nvarchar) +''', AcqPrice = ' + cast(@AcqPrice as nvarchar) + ', AcqVolume = ' + cast((@DoneVolume) as nvarchar) + ' where StatusInvestment = 1 and InvestmentPK = ' + cast(@InvestmentPK as nvarchar)
									else
										set @Query = 'update Investment set AcqDate' + Cast(@Counter as nvarchar) + ' = ''' + Cast(@AcqDate as nvarchar) +''', AcqPrice' + Cast(@Counter as nvarchar) + ' = ' + cast(@AcqPrice as nvarchar) + ', AcqVolume' + Cast(@Counter as nvarchar) + ' = ' + cast((@DoneVolume) as nvarchar) + ' where StatusInvestment = 1 and InvestmentPK = ' + cast(@InvestmentPK as nvarchar)
									exec(@Query)

									if @FifobondPositionPK != 0
									update FiFoBondPosition set RemainingVolume = @AcqVolume - @DoneVolume, investmentpk = @investmentpk where FiFoBondPositionPK = @FifobondPositionPK and status in (1,2)

									if @InvestmentBuyPK !=0
									begin
										if exists(select * from FiFoBondPositionTemp where InvestmentBuyPK = @InvestmentBuyPK and RemainingVolume != 0)
											update FifoBondPositionTemp set RemainingVolume = @AcqVolume - @DoneVolume, InvestmentSellPK = @investmentpk where InvestmentBuyPK = @InvestmentBuyPK
										else
											insert into FifoBondPositionTemp(InvestmentBuyPK,InvestmentSellPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume,FundPK,InstrumentPK)
											select @InvestmentBuyPK,@investmentpk,@AcqDate,@AcqVolume,@AcqPrice,@AcqVolume - @DoneVolume,@FundPK,@instrumentpk
									end


									break
								end
		
							FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentBuyPK,@AcqVolume,@AcqPrice,@AcqDate
							END  
  
							CLOSE AB  
							DEALLOCATE AB 

                        Fetch next From A Into @InvestmentPK
                        END
                        Close A
                        Deallocate A

                        
                        DECLARE A CURSOR FOR 
	                            Select InvestmentPK,DealingPK,HistoryPK,InvestmentNotes,OrderPrice,Volume,Amount,AccruedInterest,FundPK,LastCouponDate,SettlementDate,5 From investment 
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom " + _paramInvestmentPK + @" 
								and  InstrumentTypePK in (2,3,8,9,13,15) and TrxType = @TrxType 

                        Open A
                        Fetch Next From A
                        Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@FundPK,@LastCouponDate,@SettlementDate,@TaxInterestPercent

                        While @@FETCH_STATUS = 0
                        BEGIN
                                if @TrxType = 2
                                    set @AccruedInterest = 0
                                -- pecah acq 1
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate is not null and AcqVolume != 0 and AcqPrice != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice)/100 * AcqVolume, @DaysOfHoldingInterest = case when AcqDate >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate is not null and AcqVolume != 0 and AcqPrice != 0)
             


                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate is not null and AcqVolume != 0 and AcqPrice != 0)
                                        
                                    if @TrxType = 2
                                    begin
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
                                    end
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 1)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,1,@TrxType,reference,AcqVolume,AcqDate,AcqPrice,AcqVolume * AcqPrice/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate is not null and AcqVolume != 0 and AcqPrice != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume,A.AcqDate = B.AcqDate,A.AcqPrice = B.AcqPrice,A.AcqAmount = B.AcqVolume * B.AcqPrice/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate is not null and B.AcqVolume != 0 and B.AcqPrice != 0)
		                            end
	                            end

	                            -- pecah acq 2
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate1 is not null and AcqVolume1 != 0 and AcqPrice1 != 0))
	                            begin

			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice1)/100 * AcqVolume1, @DaysOfHoldingInterest = case when AcqDate1 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate1,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate1,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate1 is not null and AcqVolume1 != 0 and AcqPrice1 != 0)
            

                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume1/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume1 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate1 is not null and AcqVolume1 != 0 and AcqPrice1 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 2)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,2,@TrxType,reference,AcqVolume1,AcqDate1,AcqPrice1,AcqVolume1 * AcqPrice1/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate1 is not null and AcqVolume1 != 0 and AcqPrice1 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume1,A.AcqDate = B.AcqDate1,A.AcqPrice = B.AcqPrice1,A.AcqAmount = B.AcqVolume1 * B.AcqPrice1/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate1 is not null and B.AcqVolume1 != 0 and B.AcqPrice1 != 0)
		                            end
	                            end

	                            -- acq 3
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate2 is not null and AcqVolume2 != 0 and AcqPrice2 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice2)/100 * AcqVolume2, @DaysOfHoldingInterest = case when AcqDate2 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate2,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate2,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate2 is not null and AcqVolume2 != 0 and AcqPrice2 != 0)
            

                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume2/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume2 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate2 is not null and AcqVolume2 != 0 and AcqPrice2 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 3)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,3,@TrxType,reference,AcqVolume2,AcqDate2,AcqPrice2,AcqVolume2 * AcqPrice2/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate2 is not null and AcqVolume2 != 0 and AcqPrice2 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume2,A.AcqDate = B.AcqDate2,A.AcqPrice = B.AcqPrice2,A.AcqAmount = B.AcqVolume2 * B.AcqPrice2/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate2 is not null and B.AcqVolume2 != 0 and B.AcqPrice2 != 0)
		                            end
	                            end
	
	                            -- acq 4
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate3 is not null and AcqVolume3 != 0 and AcqPrice3 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice3)/100 * AcqVolume3, @DaysOfHoldingInterest = case when AcqDate3 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate3,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate3,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate3 is not null and AcqVolume3 != 0 and AcqPrice3 != 0)
            


                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume3/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume3 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate3 is not null and AcqVolume3 != 0 and AcqPrice3 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 4)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,4,@TrxType,reference,AcqVolume3,AcqDate3,AcqPrice3,AcqVolume3 * AcqPrice3/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate3 is not null and AcqVolume3 != 0 and AcqPrice3 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume3,A.AcqDate = B.AcqDate3,A.AcqPrice = B.AcqPrice3,A.AcqAmount = B.AcqVolume3 * B.AcqPrice3/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate3 is not null and B.AcqVolume3 != 0 and B.AcqPrice3 != 0)
		                            end
	                            end
	
	                            --acq 5
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate4 is not null and AcqVolume4 != 0 and AcqPrice4 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice4)/100 * AcqVolume4, @DaysOfHoldingInterest = case when AcqDate4 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate4,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate4,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate4 is not null and AcqVolume4 != 0 and AcqPrice4 != 0)
            


                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume4/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume4 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate4 is not null and AcqVolume4 != 0 and AcqPrice4 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 5)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,5,@TrxType,reference,AcqVolume4,AcqDate4,AcqPrice4,AcqVolume4 * AcqPrice4/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate4 is not null and AcqVolume4 != 0 and AcqPrice4 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume4,A.AcqDate = B.AcqDate4,A.AcqPrice = B.AcqPrice4,A.AcqAmount = B.AcqVolume4 * B.AcqPrice4/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate4 is not null and B.AcqVolume4 != 0 and B.AcqPrice4 != 0)
		                            end
	                            end
	
	                            --acq 6
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate5 is not null and AcqVolume5 != 0 and AcqPrice5 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice5)/100 * AcqVolume5, @DaysOfHoldingInterest = case when AcqDate5 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate5,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate5,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate5 is not null and AcqVolume5 != 0 and AcqPrice5 != 0)

                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume5/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume5 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate5 is not null and AcqVolume5 != 0 and AcqPrice5 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 6)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,6,@TrxType,reference,AcqVolume5,AcqDate5,AcqPrice5,AcqVolume5 * AcqPrice5/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate5 is not null and AcqVolume5 != 0 and AcqPrice5 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume5,A.AcqDate = B.AcqDate5,A.AcqPrice = B.AcqPrice5,A.AcqAmount = B.AcqVolume5 * B.AcqPrice5/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate5 is not null and B.AcqVolume5 != 0 and B.AcqPrice5 != 0)
		                            end
	                            end
	
	                            --acq 7
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate6 is not null and AcqVolume6 != 0 and AcqPrice6 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice6)/100 * AcqVolume6, @DaysOfHoldingInterest = case when AcqDate6 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate6,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate6,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate6 is not null and AcqVolume6 != 0 and AcqPrice6 != 0)


                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume6/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume6 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate6 is not null and AcqVolume6 != 0 and AcqPrice6 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 7)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,7,@TrxType,reference,AcqVolume6,AcqDate6,AcqPrice6,AcqVolume6 * AcqPrice6/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate6 is not null and AcqVolume6 != 0 and AcqPrice6 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume6,A.AcqDate = B.AcqDate6,A.AcqPrice = B.AcqPrice6,A.AcqAmount = B.AcqVolume6 * B.AcqPrice6/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate6 is not null and B.AcqVolume6 != 0 and B.AcqPrice6 != 0)
		                            end
	                            end
	
	                            -- acq 8
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate7 is not null and AcqVolume7 != 0 and AcqPrice7 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice7)/100 * AcqVolume7, @DaysOfHoldingInterest = case when AcqDate7 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate7,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate7,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate7 is not null and AcqVolume7 != 0 and AcqPrice7 != 0)

                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume7/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume7 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate7 is not null and AcqVolume7 != 0 and AcqPrice7 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 8)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,8,@TrxType,reference,AcqVolume7,AcqDate7,AcqPrice7,AcqVolume7 * AcqPrice7/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate7 is not null and AcqVolume7 != 0 and AcqPrice7 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume7,A.AcqDate = B.AcqDate7,A.AcqPrice = B.AcqPrice7,A.AcqAmount = B.AcqVolume7 * B.AcqPrice7/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate7 is not null and B.AcqVolume7 != 0 and B.AcqPrice7 != 0)
		                            end
	                            end

	                            -- acq 9
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate8 is not null and AcqVolume8 != 0 and AcqPrice8 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice8)/100 * AcqVolume8, @DaysOfHoldingInterest = case when AcqDate8 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate8,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate8,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate8 is not null and AcqVolume8 != 0 and AcqPrice8 != 0)

                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume8/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume8 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate8 is not null and AcqVolume8 != 0 and AcqPrice8 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 9)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,9,@TrxType,reference,AcqVolume8,AcqDate8,AcqPrice8,AcqVolume8 * AcqPrice8/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate8 is not null and AcqVolume8 != 0 and AcqPrice8 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume8,A.AcqDate = B.AcqDate8,A.AcqPrice = B.AcqPrice8,A.AcqAmount = B.AcqVolume8 * B.AcqPrice8/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate8 is not null and B.AcqVolume8 != 0 and B.AcqPrice8 != 0)
		                            end
	                            end

	                            -- acq 10 -- baru sampe sini benerin untuk yg Monthly
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate9 is not null and AcqVolume9 != 0 and AcqPrice9 != 0))
	                            begin
			                        select @IncomeTaxGainAmount = (DonePrice - AcqPrice9)/100 * AcqVolume9, @DaysOfHoldingInterest = case when AcqDate9 >= LastCouponDate then case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate9,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate9,SettlementDate) end else
			                        case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end, 
                                    @AccruedInterestDays = case when A.InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
			                        @DaysOfLastCoupon = case when A.InstrumentTypePK in ( 2, 13 ) and B.InterestPaymentType <> 7 then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment A
			                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			                        where Investmentpk = @investmentPK and A.HistoryPK = @historyPK and (AcqDate9 is not null and AcqVolume9 != 0 and AcqPrice9 != 0)


                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume9/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume9 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate9 is not null and AcqVolume9 != 0 and AcqPrice9 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 10)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,10,@TrxType,reference,AcqVolume9,AcqDate9,AcqPrice9,AcqVolume9 * AcqPrice9/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate9 is not null and AcqVolume9 != 0 and AcqPrice9 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume9,A.AcqDate = B.AcqDate9,A.AcqPrice = B.AcqPrice9,A.AcqAmount = B.AcqVolume9 * B.AcqPrice9/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate9 is not null and B.AcqVolume9 != 0 and B.AcqPrice9 != 0)
		                            end
	                            end

								-- acq 11
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate10 is not null and AcqVolume10 != 0 and AcqPrice10 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice10)/100 * AcqVolume10, @DaysOfHoldingInterest = case when AcqDate10 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate10,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate10,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate10 is not null and AcqVolume10 != 0 and AcqPrice10 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume10/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume10 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate10 is not null and AcqVolume10 != 0 and AcqPrice10 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 11)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,11,@TrxType,reference,AcqVolume10,AcqDate10,AcqPrice10,AcqVolume10 * AcqPrice10/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate10 is not null and AcqVolume10 != 0 and AcqPrice10 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume10,A.AcqDate = B.AcqDate10,A.AcqPrice = B.AcqPrice10,A.AcqAmount = B.AcqVolume10 * B.AcqPrice10/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate10 is not null and B.AcqVolume10 != 0 and B.AcqPrice10 != 0)
		                            end
	                            end

								-- acq 12
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate11 is not null and AcqVolume11 != 0 and AcqPrice11 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice11)/100 * AcqVolume11, @DaysOfHoldingInterest = case when AcqDate11 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate11,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate11,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate11 is not null and AcqVolume11 != 0 and AcqPrice11 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume11/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume11 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate11 is not null and AcqVolume11 != 0 and AcqPrice11 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 12)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,12,@TrxType,reference,AcqVolume11,AcqDate11,AcqPrice11,AcqVolume11 * AcqPrice11/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate11 is not null and AcqVolume11 != 0 and AcqPrice11 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume11,A.AcqDate = B.AcqDate11,A.AcqPrice = B.AcqPrice11,A.AcqAmount = B.AcqVolume11 * B.AcqPrice11/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate11 is not null and B.AcqVolume11 != 0 and B.AcqPrice11 != 0)
		                            end
	                            end

								-- acq 13
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate12 is not null and AcqVolume12 != 0 and AcqPrice12 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice12)/100 * AcqVolume12, @DaysOfHoldingInterest = case when AcqDate12 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate12,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate12,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate12 is not null and AcqVolume12 != 0 and AcqPrice12 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume12/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume12 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate12 is not null and AcqVolume12 != 0 and AcqPrice12 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 13)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,13,@TrxType,reference,AcqVolume12,AcqDate12,AcqPrice12,AcqVolume12 * AcqPrice12/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate12 is not null and AcqVolume12 != 0 and AcqPrice12 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume12,A.AcqDate = B.AcqDate12,A.AcqPrice = B.AcqPrice12,A.AcqAmount = B.AcqVolume12 * B.AcqPrice12/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate12 is not null and B.AcqVolume12 != 0 and B.AcqPrice12 != 0)
		                            end
	                            end

								-- acq 14
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate13 is not null and AcqVolume13 != 0 and AcqPrice13 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice13)/100 * AcqVolume13, @DaysOfHoldingInterest = case when AcqDate13 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate13,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate13,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate13 is not null and AcqVolume13 != 0 and AcqPrice13 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume13/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume13 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate13 is not null and AcqVolume13 != 0 and AcqPrice13 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 14)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,14,@TrxType,reference,AcqVolume13,AcqDate13,AcqPrice13,AcqVolume13 * AcqPrice13/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate13 is not null and AcqVolume13 != 0 and AcqPrice13 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume13,A.AcqDate = B.AcqDate13,A.AcqPrice = B.AcqPrice13,A.AcqAmount = B.AcqVolume13 * B.AcqPrice13/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate13 is not null and B.AcqVolume13 != 0 and B.AcqPrice13 != 0)
		                            end
	                            end

								-- acq 15
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate14 is not null and AcqVolume14 != 0 and AcqPrice14 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice14)/100 * AcqVolume14, @DaysOfHoldingInterest = case when AcqDate14 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate14,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate14,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate14 is not null and AcqVolume14 != 0 and AcqPrice14 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume14/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume14 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate14 is not null and AcqVolume14 != 0 and AcqPrice14 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 15)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,15,@TrxType,reference,AcqVolume14,AcqDate14,AcqPrice14,AcqVolume14 * AcqPrice14/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate14 is not null and AcqVolume14 != 0 and AcqPrice14 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume14,A.AcqDate = B.AcqDate14,A.AcqPrice = B.AcqPrice14,A.AcqAmount = B.AcqVolume14 * B.AcqPrice14/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate14 is not null and B.AcqVolume14 != 0 and B.AcqPrice14 != 0)
		                            end
	                            end

								-- acq 16
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate15 is not null and AcqVolume15 != 0 and AcqPrice15 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice15)/100 * AcqVolume15, @DaysOfHoldingInterest = case when AcqDate15 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate15,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate15,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate15 is not null and AcqVolume15 != 0 and AcqPrice15 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume15/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume15 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate15 is not null and AcqVolume15 != 0 and AcqPrice15 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 16)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,16,@TrxType,reference,AcqVolume15,AcqDate15,AcqPrice15,AcqVolume15 * AcqPrice15/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate15 is not null and AcqVolume15 != 0 and AcqPrice15 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume15,A.AcqDate = B.AcqDate15,A.AcqPrice = B.AcqPrice15,A.AcqAmount = B.AcqVolume15 * B.AcqPrice15/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate15 is not null and B.AcqVolume15 != 0 and B.AcqPrice15 != 0)
		                            end
	                            end

								-- acq 17
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate16 is not null and AcqVolume16 != 0 and AcqPrice16 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice16)/100 * AcqVolume16, @DaysOfHoldingInterest = case when AcqDate16 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate16,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate16,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate16 is not null and AcqVolume16 != 0 and AcqPrice16 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume16/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume16 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate16 is not null and AcqVolume16 != 0 and AcqPrice16 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 17)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,17,@TrxType,reference,AcqVolume16,AcqDate16,AcqPrice16,AcqVolume16 * AcqPrice16/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate16 is not null and AcqVolume16 != 0 and AcqPrice16 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume16,A.AcqDate = B.AcqDate16,A.AcqPrice = B.AcqPrice16,A.AcqAmount = B.AcqVolume16 * B.AcqPrice16/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate16 is not null and B.AcqVolume16 != 0 and B.AcqPrice16 != 0)
		                            end
	                            end

								-- acq 18
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate17 is not null and AcqVolume17 != 0 and AcqPrice17 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice17)/100 * AcqVolume17, @DaysOfHoldingInterest = case when AcqDate17 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate17,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate17,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate17 is not null and AcqVolume17 != 0 and AcqPrice17 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume17/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume17 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate17 is not null and AcqVolume17 != 0 and AcqPrice17 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 18)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,18,@TrxType,reference,AcqVolume17,AcqDate17,AcqPrice17,AcqVolume17 * AcqPrice17/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate17 is not null and AcqVolume17 != 0 and AcqPrice17 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume17,A.AcqDate = B.AcqDate17,A.AcqPrice = B.AcqPrice17,A.AcqAmount = B.AcqVolume17 * B.AcqPrice17/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate17 is not null and B.AcqVolume17 != 0 and B.AcqPrice17 != 0)
		                            end
	                            end

								-- acq 19
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate18 is not null and AcqVolume18 != 0 and AcqPrice18 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice18)/100 * AcqVolume18, @DaysOfHoldingInterest = case when AcqDate18 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate18,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate18,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate18 is not null and AcqVolume18 != 0 and AcqPrice18 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume18/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume18 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate18 is not null and AcqVolume18 != 0 and AcqPrice18 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 19)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,19,@TrxType,reference,AcqVolume18,AcqDate18,AcqPrice18,AcqVolume18 * AcqPrice18/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate18 is not null and AcqVolume18 != 0 and AcqPrice18 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume18,A.AcqDate = B.AcqDate18,A.AcqPrice = B.AcqPrice18,A.AcqAmount = B.AcqVolume18 * B.AcqPrice18/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate18 is not null and B.AcqVolume18 != 0 and B.AcqPrice18 != 0)
		                            end
	                            end

								-- acq 20
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate19 is not null and AcqVolume19 != 0 and AcqPrice19 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice19)/100 * AcqVolume19, @DaysOfHoldingInterest = case when AcqDate19 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate19,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate19,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 360 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate19 is not null and AcqVolume19 != 0 and AcqPrice19 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume19/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume19 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate19 is not null and AcqVolume19 != 0 and AcqPrice19 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 20)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,20,@TrxType,reference,AcqVolume19,AcqDate19,AcqPrice19,AcqVolume19 * AcqPrice19/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate19 is not null and AcqVolume19 != 0 and AcqPrice19 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume19,A.AcqDate = B.AcqDate19,A.AcqPrice = B.AcqPrice19,A.AcqAmount = B.AcqVolume19 * B.AcqPrice19/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate19 is not null and B.AcqVolume19 != 0 and B.AcqPrice19 != 0)
		                            end
	                            end

								--acq 21
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate20 is not null and AcqVolume20 != 0 and AcqPrice20 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice20)/100 * AcqVolume20, @DaysOfHoldingInterest = case when AcqDate20 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate20,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate20,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3200 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate20 is not null and AcqVolume20 != 0 and AcqPrice20 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume20/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume20 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate20 is not null and AcqVolume20 != 0 and AcqPrice20 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 21)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,21,@TrxType,reference,AcqVolume20,AcqDate20,AcqPrice20,AcqVolume20 * AcqPrice20/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate20 is not null and AcqVolume20 != 0 and AcqPrice20 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume20,A.AcqDate = B.AcqDate20,A.AcqPrice = B.AcqPrice20,A.AcqAmount = B.AcqVolume20 * B.AcqPrice20/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate20 is not null and B.AcqVolume20 != 0 and B.AcqPrice20 != 0)
		                            end
	                            end

								--acq 22
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate21 is not null and AcqVolume21 != 0 and AcqPrice21 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice21)/100 * AcqVolume21, @DaysOfHoldingInterest = case when AcqDate21 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate21,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate21,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3210 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate21 is not null and AcqVolume21 != 0 and AcqPrice21 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume21/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume21 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate21 is not null and AcqVolume21 != 0 and AcqPrice21 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 22)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,22,@TrxType,reference,AcqVolume21,AcqDate21,AcqPrice21,AcqVolume21 * AcqPrice21/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate21 is not null and AcqVolume21 != 0 and AcqPrice21 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume21,A.AcqDate = B.AcqDate21,A.AcqPrice = B.AcqPrice21,A.AcqAmount = B.AcqVolume21 * B.AcqPrice21/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate21 is not null and B.AcqVolume21 != 0 and B.AcqPrice21 != 0)
		                            end
	                            end

								--acq 23
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate22 is not null and AcqVolume22 != 0 and AcqPrice22 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice22)/100 * AcqVolume22, @DaysOfHoldingInterest = case when AcqDate22 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate22,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate22,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3220 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate22 is not null and AcqVolume22 != 0 and AcqPrice22 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume22/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume22 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate22 is not null and AcqVolume22 != 0 and AcqPrice22 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 23)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,23,@TrxType,reference,AcqVolume22,AcqDate22,AcqPrice22,AcqVolume22 * AcqPrice22/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate22 is not null and AcqVolume22 != 0 and AcqPrice22 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume22,A.AcqDate = B.AcqDate22,A.AcqPrice = B.AcqPrice22,A.AcqAmount = B.AcqVolume22 * B.AcqPrice22/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate22 is not null and B.AcqVolume22 != 0 and B.AcqPrice22 != 0)
		                            end
	                            end

								--acq 24
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate23 is not null and AcqVolume23 != 0 and AcqPrice23 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice23)/100 * AcqVolume23, @DaysOfHoldingInterest = case when AcqDate23 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate23,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate23,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3230 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate23 is not null and AcqVolume23 != 0 and AcqPrice23 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume23/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume23 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate23 is not null and AcqVolume23 != 0 and AcqPrice23 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 24)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,24,@TrxType,reference,AcqVolume23,AcqDate23,AcqPrice23,AcqVolume23 * AcqPrice23/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate23 is not null and AcqVolume23 != 0 and AcqPrice23 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume23,A.AcqDate = B.AcqDate23,A.AcqPrice = B.AcqPrice23,A.AcqAmount = B.AcqVolume23 * B.AcqPrice23/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate23 is not null and B.AcqVolume23 != 0 and B.AcqPrice23 != 0)
		                            end
	                            end

								--acq 25
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate24 is not null and AcqVolume24 != 0 and AcqPrice24 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice24)/100 * AcqVolume24, @DaysOfHoldingInterest = case when AcqDate24 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate24,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate24,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3240 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate24 is not null and AcqVolume24 != 0 and AcqPrice24 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume24/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume24 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate24 is not null and AcqVolume24 != 0 and AcqPrice24 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 25)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,25,@TrxType,reference,AcqVolume24,AcqDate24,AcqPrice24,AcqVolume24 * AcqPrice24/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate24 is not null and AcqVolume24 != 0 and AcqPrice24 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume24,A.AcqDate = B.AcqDate24,A.AcqPrice = B.AcqPrice24,A.AcqAmount = B.AcqVolume24 * B.AcqPrice24/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate24 is not null and B.AcqVolume24 != 0 and B.AcqPrice24 != 0)
		                            end
	                            end

								--acq 26
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate25 is not null and AcqVolume25 != 0 and AcqPrice25 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice25)/100 * AcqVolume25, @DaysOfHoldingInterest = case when AcqDate25 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate25,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate25,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3250 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate25 is not null and AcqVolume25 != 0 and AcqPrice25 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume25/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume25 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate25 is not null and AcqVolume25 != 0 and AcqPrice25 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 26)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,26,@TrxType,reference,AcqVolume25,AcqDate25,AcqPrice25,AcqVolume25 * AcqPrice25/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate25 is not null and AcqVolume25 != 0 and AcqPrice25 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume25,A.AcqDate = B.AcqDate25,A.AcqPrice = B.AcqPrice25,A.AcqAmount = B.AcqVolume25 * B.AcqPrice25/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate25 is not null and B.AcqVolume25 != 0 and B.AcqPrice25 != 0)
		                            end
	                            end

								--acq 27
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate26 is not null and AcqVolume26 != 0 and AcqPrice26 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice26)/100 * AcqVolume26, @DaysOfHoldingInterest = case when AcqDate26 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate26,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate26,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3260 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate26 is not null and AcqVolume26 != 0 and AcqPrice26 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume26/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume26 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate26 is not null and AcqVolume26 != 0 and AcqPrice26 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 27)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,27,@TrxType,reference,AcqVolume26,AcqDate26,AcqPrice26,AcqVolume26 * AcqPrice26/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate26 is not null and AcqVolume26 != 0 and AcqPrice26 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume26,A.AcqDate = B.AcqDate26,A.AcqPrice = B.AcqPrice26,A.AcqAmount = B.AcqVolume26 * B.AcqPrice26/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate26 is not null and B.AcqVolume26 != 0 and B.AcqPrice26 != 0)
		                            end
	                            end

								--acq 28
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate27 is not null and AcqVolume27 != 0 and AcqPrice27 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice27)/100 * AcqVolume27, @DaysOfHoldingInterest = case when AcqDate27 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate27,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate27,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3270 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate27 is not null and AcqVolume27 != 0 and AcqPrice27 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume27/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume27 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate27 is not null and AcqVolume27 != 0 and AcqPrice27 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 28)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,28,@TrxType,reference,AcqVolume27,AcqDate27,AcqPrice27,AcqVolume27 * AcqPrice27/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate27 is not null and AcqVolume27 != 0 and AcqPrice27 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume27,A.AcqDate = B.AcqDate27,A.AcqPrice = B.AcqPrice27,A.AcqAmount = B.AcqVolume27 * B.AcqPrice27/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate27 is not null and B.AcqVolume27 != 0 and B.AcqPrice27 != 0)
		                            end
	                            end

								--acq 29
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate28 is not null and AcqVolume28 != 0 and AcqPrice28 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice28)/100 * AcqVolume28, @DaysOfHoldingInterest = case when AcqDate28 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate28,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate28,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3280 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate28 is not null and AcqVolume28 != 0 and AcqPrice28 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume28/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume28 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate28 is not null and AcqVolume28 != 0 and AcqPrice28 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 29)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,29,@TrxType,reference,AcqVolume28,AcqDate28,AcqPrice28,AcqVolume28 * AcqPrice28/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate28 is not null and AcqVolume28 != 0 and AcqPrice28 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume28,A.AcqDate = B.AcqDate28,A.AcqPrice = B.AcqPrice28,A.AcqAmount = B.AcqVolume28 * B.AcqPrice28/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate28 is not null and B.AcqVolume28 != 0 and B.AcqPrice28 != 0)
		                            end
	                            end

								--acq 30
	                            if exists (select * from Investment where InvestmentPK = @investmentPK and HistoryPK = @historyPK and (AcqDate29 is not null and AcqVolume29 != 0 and AcqPrice29 != 0))
	                            begin
									select @IncomeTaxGainAmount = (DonePrice - AcqPrice29)/100 * AcqVolume29, @DaysOfHoldingInterest = case when AcqDate29 >= LastCouponDate then case when InstrumentTypePK in ( 2, 13 ) then datediff(day,AcqDate29,SettlementDate) else dbo.FgetDateDiffCorporateBond(AcqDate29,SettlementDate) end else
									case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end end,
                                    @AccruedInterestDays = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,SettlementDate) else dbo.FgetDateDiffCorporateBond(LastCouponDate,SettlementDate) end,
										@DaysOfLastCoupon = case when InstrumentTypePK in ( 2, 13 ) then datediff(day,LastCouponDate,NextCouponDate) * 2 else 3290 end 
                                    from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate29 is not null and AcqVolume29 != 0 and AcqPrice29 != 0)
                                    select @IncomeTaxInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume29/ 1000000 * round(( InterestPercent * 1000000 * @DaysOfHoldingInterest)/100/@DaysOfLastCoupon,0) else (AcqVolume29 * InterestPercent )/@DaysOfLastCoupon/100 * @DaysOfHoldingInterest end, 
                                    @AccruedInterestAmount = case when InstrumentTypePK in ( 2, 13 ) then AcqVolume/ 1000000 * round(( InterestPercent * 1000000 * @AccruedInterestDays)/100/@DaysOfLastCoupon,0) else (AcqVolume * InterestPercent )/@DaysOfLastCoupon/100 * @AccruedInterestDays end  from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate29 is not null and AcqVolume29 != 0 and AcqPrice29 != 0)

                                    if @TrxType = 2
                                        set @AccruedInterest = @AccruedInterest + @AccruedInterestAmount
			                        if @IncomeTaxGainAmount > @IncomeTaxInterestAmount
				                        set @TotalTaxIncomeAmount = @IncomeTaxInterestAmount
			                        else
				                        set @TotalTaxIncomeAmount = @IncomeTaxGainAmount + @IncomeTaxInterestAmount

			                        set @TaxAmount = (@TaxInterestPercent * @IncomeTaxGainAmount / 100) + (@IncomeTaxInterestAmount * @TaxInterestPercent / 100)
                                    if @TaxAmount < 0
                                        set @TaxAmount = 0

		                            if not exists (select * from InvestmentTaxDataAcq where InvestmentPK = @investmentPK and AcqNo = 30)
		                            begin
			                            insert into InvestmentTaxDataAcq(InvestmentPK,AcqNo,TrxType,Reference,DoneAmount,AcqDate,AcqPrice,AcqAmount,IncomeTaxGainAmount,DaysOfHoldingInterest,IncomeTaxInterestAmount,TotalTaxIncomeAmount,TaxExpensePercent,TaxAmount)
			                            select @investmentPK,30,@TrxType,reference,AcqVolume29,AcqDate29,AcqPrice29,AcqVolume29 * AcqPrice29/100,@IncomeTaxGainAmount ,@DaysOfHoldingInterest,@IncomeTaxInterestAmount,@TotalTaxIncomeAmount,@TaxInterestPercent,@TaxAmount from Investment where Investmentpk = @investmentPK and HistoryPK = @historyPK and (AcqDate29 is not null and AcqVolume29 != 0 and AcqPrice29 != 0)
		                            end
		                            else
		                            begin
			                            update A set A.Reference = B.Reference,A.DoneAmount = B.AcqVolume29,A.AcqDate = B.AcqDate29,A.AcqPrice = B.AcqPrice29,A.AcqAmount = B.AcqVolume29 * B.AcqPrice29/100, A.IncomeTaxGainAmount = @IncomeTaxGainAmount,A.DaysOfHoldingInterest = @DaysOfHoldingInterest,A.IncomeTaxInterestAmount = @IncomeTaxInterestAmount, A.TotalTaxIncomeAmount = @TotalTaxIncomeAmount, A.TaxExpensePercent = @TaxInterestPercent, A.TaxAmount = @TaxAmount from InvestmentTaxDataAcq A 
			                            left join Investment B on A.InvestmentPK = B.InvestmentPK and B.HistoryPK = @historyPK and (B.AcqDate29 is not null and B.AcqVolume29 != 0 and B.AcqPrice29 != 0)
		                            end
	                            end

                            if @TrxType = 1
                            begin
	                            Select @TaxPercentageBond = isnull(TaxPercentageBond,0),@TaxPercentageCapitalGain =  isnull(TaxPercentageCapitalGain,0) 
	                            from FundAccountingSetup where status = 2 and FundPK = @FundPK
                            end
                            else
                            begin
                                Select @TaxPercentageBond = isnull(TaxPercentageBond,0),@TaxPercentageCapitalGain =  isnull(TaxPercentageCapitalGainSell,0)
	                            from FundAccountingSetup where status = 2 and FundPK = @FundPK
                            end

	                        Select @DealingPK = max(DealingPK) + 1 From investment
	                        if isnull(@DealingPK,0) = 0 
							BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment 
							END  
	                        update Investment set DealingPK = @DealingPK, statusInvestment = 2, statusDealing = 1,InvestmentNotes=@Notes,DonePrice=@OrderPrice,DoneVolume=@Volume,AccruedInterest=@AccruedInterest,DoneAccruedInterest=@AccruedInterest,BoardType = 1 ,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,EntryDealingID = @ApprovedUsersID,EntryDealingTime = @ApprovedTime ,LastUpdate=@LastUpdate
	                        ,IncomeTaxInterestPercent = @TaxPercentageBond , IncomeTaxGainPercent = @TaxPercentageCapitalGain, TaxExpensePercent = @TaxPercentageBond
	                        where InvestmentPK = @InvestmentPK
                      
                        Fetch next From A Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@FundPK,@LastCouponDate,@SettlementDate,@TaxInterestPercent
                        END
                        Close A
                        Deallocate A 

                        Update Investment set SelectedInvestment  = 0";



                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@UsersID", _investment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _investment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Time", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
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


        public OMSBondForNetAmount Get_NetAmountSellBondByDealingPK(OMSBondForNetAmount _omsBond)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    OMSBondForNetAmount M_OMSBond = new OMSBondForNetAmount();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
 
                    
Declare @DealingPK int
Declare @HistoryPK int
Declare @ValueDate datetime
Declare @InstrumentPK int
Declare @InstrumentTypePK int
Declare @SettledDate datetime
Declare @NextCouponDate datetime
Declare @LastCouponDate datetime
Declare @TrxType int
Declare @Price numeric(18,4)
Declare @Volume numeric(18,4)
Declare @AcqPrice numeric(18,4)
Declare @AcqDate datetime
Declare @AcqVolume numeric(18,4)
Declare @TaxCapitaGainPercent numeric(18,4)
Declare @IncomeTaxInterestPercent numeric(18,4)
Declare @BitIsRounding bit

Declare @AcqPrice1 numeric(18,4)
Declare @AcqDate1 datetime
Declare @AcqVolume1 numeric(18,4)
Declare @AcqPrice2 numeric(18,4)
Declare @AcqDate2 datetime
Declare @AcqVolume2 numeric(18,4)
Declare @AcqPrice3 numeric(18,4)
Declare @AcqDate3 datetime
Declare @AcqVolume3 numeric(18,4)
Declare @AcqPrice4 numeric(18,4)
Declare @AcqDate4 datetime
Declare @AcqVolume4 numeric(18,4)
Declare @AcqPrice5 numeric(18,4)
Declare @AcqDate5 datetime
Declare @AcqVolume5 numeric(18,4)
Declare @AcqPrice6 numeric(18,4)
Declare @AcqDate6 datetime
Declare @AcqVolume6 numeric(18,4)
Declare @AcqPrice7 numeric(18,4)
Declare @AcqDate7 datetime
Declare @AcqVolume7 numeric(18,4)
Declare @AcqPrice8 numeric(18,4)
Declare @AcqDate8 datetime
Declare @AcqVolume8 numeric(18,4)
Declare @AcqPrice9 numeric(18,4)
Declare @AcqDate9 datetime
Declare @AcqVolume9 numeric(18,4)


Declare @TaxExpensePercent numeric(8,4)

Declare @Days int
Declare @DivDays int
Declare @InterestDays int

Declare @CurrencyID nvarchar(20)
Declare @CouponRate numeric(8,4)
Declare @InterestType int
Declare @InterestDaysType int
Declare @InterestPaymentType int



Declare @AccuredInterestAmount numeric(22,4)
Declare @GrossAmount numeric(22,4)
Declare @NetAmount numeric(22,4)
Declare @ValuePerUnit int

Declare		@DaysAcq_2		int,
@DaysAcq1_2					int,
@DaysAcq2_2					int,
@DaysAcq3_2					int,
@DaysAcq4_2					int,
@DaysAcq5_2					int,
@DaysAcq6_2					int,
@DaysAcq7_2					int,
@DaysAcq8_2					int,
@DaysAcq9_2					int,
@CapGainAcq					decimal(22,4),
@CapGainAcq1				decimal(22,4),
@CapGainAcq2				decimal(22,4),
@CapGainAcq3				decimal(22,4),
@CapGainAcq4				decimal(22,4),
@CapGainAcq5				decimal(22,4),
@CapGainAcq6				decimal(22,4),
@CapGainAcq7				decimal(22,4),
@CapGainAcq8				decimal(22,4),
@CapGainAcq9				decimal(22,4),
@TaxCapGainAcq				decimal(22,4),
@TaxCapGainAcq1				decimal(22,4),
@TaxCapGainAcq2				decimal(22,4),
@TaxCapGainAcq3				decimal(22,4),
@TaxCapGainAcq4				decimal(22,4),
@TaxCapGainAcq5				decimal(22,4),
@TaxCapGainAcq6				decimal(22,4),
@TaxCapGainAcq7				decimal(22,4),
@TaxCapGainAcq8				decimal(22,4),
@TaxCapGainAcq9				decimal(22,4),
@AIAcq					decimal(22,4),
@AIAcq1					decimal(22,4),
@AIAcq2					decimal(22,4),
@AIAcq3					decimal(22,4),
@AIAcq4					decimal(22,4),
@AIAcq5					decimal(22,4),
@AIAcq6					decimal(22,4),
@AIAcq7					decimal(22,4),
@AIAcq8					decimal(22,4),
@AIAcq9					decimal(22,4),
@TaxAIAcq				decimal(22,4),
@TaxAIAcq1				decimal(22,4),
@TaxAIAcq2				decimal(22,4),
@TaxAIAcq3				decimal(22,4),
@TaxAIAcq4				decimal(22,4),
@TaxAIAcq5				decimal(22,4),
@TaxAIAcq6				decimal(22,4),
@TaxAIAcq7				decimal(22,4),
@TaxAIAcq8				decimal(22,4),
@TaxAIAcq9				decimal(22,4),
@TotalCapGain			decimal(22,4),
@TotalAI				decimal(22,4),
@TotalTaxCapGain		decimal(22,4),
@TotalTaxAI				decimal(22,4)


declare @BitNoCapitalGainTax bit
select @BitNoCapitalGainTax = isnull(BitNoCapitalGainTax,0) from CounterpartCommission where CounterpartPK = @CounterpartPK and status = 2

DECLARE A CURSOR FOR 

Select A.DealingPK,A.HistoryPK,A.ValueDate,A.InstrumentPK,A.InstrumentTypePK,
A.SettlementDate,A.NextCouponDate,A.LastCouponDate,
A.TrxType,@ParamDonePrice,A.DoneVolume,@ParamAcqPrice,A.AcqDate,
A.AcqVolume,@ParamTaxGainPercent,@ParamTaxInterestPercent,
A.BitIsRounding,
A.TaxExpensePercent,
C.ID,A.InterestPercent,B.InterestType, 
B.InterestDaysType,12/D.Priority,
A.AcqPrice1,A.AcqDate1,A.AcqVolume1,A.AcqPrice2,A.AcqDate2,A.AcqVolume2,A.AcqPrice3,A.AcqDate3,A.AcqVolume3,
A.AcqPrice4,A.AcqDate4,A.AcqVolume4,A.AcqPrice5,A.AcqDate5,A.AcqVolume5,A.AcqPrice6,A.AcqDate6,A.AcqVolume6,
A.AcqPrice7,A.AcqDate7,A.AcqVolume7,A.AcqPrice8,A.AcqDate8,A.AcqVolume8,A.AcqPrice9,A.AcqDate9,A.AcqVolume9
From Investment A
left join instrument B on A.InstrumentPK = B.instrumentPK and B.status = 2
left join Currency C on B.CurrencyPK = C.CurrencyPK and C.status = 2
left join MasterValue D on B.InterestPaymentType = D.Code and D.Status = 2 and D.ID = 'InterestPaymentType'
where DealingPK = @CDealingPK 

Open A
Fetch Next From A
Into @DealingPK,@HistoryPK,@ValueDate,@InstrumentPK,@InstrumentTypePK,
@SettledDate,@NextCouponDate,@LastCouponDate,
@TrxType, @Price,@Volume,@AcqPrice,@AcqDate,
@AcqVolume,@TaxCapitaGainPercent,@IncomeTaxInterestPercent,
@BitIsRounding,@TaxExpensePercent,
@CurrencyID,@CouponRate,@InterestType, 
@InterestDaysType,@InterestPaymentType,
@AcqPrice1,@AcqDate1,@AcqVolume1,@AcqPrice2,@AcqDate2,@AcqVolume2,@AcqPrice3,@AcqDate3,@AcqVolume3,
@AcqPrice4,@AcqDate4,@AcqVolume4,@AcqPrice5,@AcqDate5,@AcqVolume5,@AcqPrice6,@AcqDate6,@AcqVolume6,
@AcqPrice7,@AcqDate7,@AcqVolume7,@AcqPrice8,@AcqDate8,@AcqVolume8,@AcqPrice9,@AcqDate9,@AcqVolume9

While @@FETCH_STATUS = 0
BEGIN  

set @TotalCapGain	 = 0	
set @TotalAI		 = 0
set @TotalTaxCapGain = 0	
set @TotalTaxAI		 = 0	

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

set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) else 0 end

set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) else 0 end

set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) else 0 end

set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) else 0 end

set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

end
	
if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
begin
		
set @ValuePerUnit = 1
set @DivDays = 0
			
set @Days = 0
		
set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari

set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end
set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100)  else 0 end
set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100)  else 0 end
set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100)  else 0 end
set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100)  else 0 end



set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100
		
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

set @DaysAcq6_2	= case when @InterestDaysType in (3) then case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate6, @SettledDate)) end else case when @AcqDate6 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate6)) end end -- total hari [acq date 6 > prev coupon date]
	
set @DaysAcq7_2	= case when @InterestDaysType in (3) then case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate7, @SettledDate)) end else case when @AcqDate7 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate7)) end end -- total hari [acq date 7 > prev coupon date]

set @DaysAcq8_2	= case when @InterestDaysType in (3) then case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate8, @SettledDate)) end else case when @AcqDate8 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate8)) end end -- total hari [acq date 8 > prev coupon date]
	
set @DaysAcq9_2	= case when @InterestDaysType in (3) then case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffGovermentBond_ACT360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffGovermentBond_ACT360](@AcqDate9, @SettledDate)) end else case when @AcqDate9 <= @LastCouponDate then abs(datediff(day, @SettledDate, @LastCouponDate)) else abs(datediff(day, @SettledDate, @AcqDate9)) end end -- total hari [acq date 9 > prev coupon date]
						
		

set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end
set @DaysAcq6_2		= case when (isnull(@AcqDate6, '') = '' or year(@AcqDate6) = 1900) then 0 else @DaysAcq6_2 end
set @DaysAcq7_2		= case when (isnull(@AcqDate7, '') = '' or year(@AcqDate7) = 1900) then 0 else @DaysAcq7_2 end
set @DaysAcq8_2		= case when (isnull(@AcqDate8, '') = '' or year(@AcqDate8) = 1900) then 0 else @DaysAcq8_2 end
set @DaysAcq9_2		= case when (isnull(@AcqDate9, '') = '' or year(@AcqDate9) = 1900) then 0 else @DaysAcq9_2 end

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

set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) else 0 end

set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) else 0 end

set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) else 0 end

set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) 
when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) else 0 end

set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq6 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq7 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq8 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @AIAcq9 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end 
when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 / @ValuePerUnit * round(@CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType * @ValuePerUnit, 0)) end else 0 end

set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq6	= @AIAcq6 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq7	= @AIAcq7 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq8	= @AIAcq8 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq9	= @AIAcq9 * @IncomeTaxInterestPercent / 100

end
		
if @InterestDaysType in (1,5,6,7,8,9) -- [Corp Bond] <-> 30/360 (US/NASD), 30/360 (EUROPEAN), 30/360, 30/360 NON-EOM, ISMA-30/360, ISMA-30/360 NONEOM
begin
		
set @ValuePerUnit = 1
set @DivDays = case when @InterestDaysType in (6) 
then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @NextCouponDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @NextCouponDate)) end -- pembagi hari
			
set @Days = case when @InterestDaysType in (6) 
then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) end -- pembagi hari
		
set @InterestDays = case when @InterestDaysType in (6) then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate,@SettledDate)) end -- pembagi hari


set @DaysAcq_2 = case when @InterestDaysType in (6) then case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate, @SettledDate)) end else case when @AcqDate <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate, @SettledDate)) end end -- total hari [acq date > prev coupon date]
set @DaysAcq1_2 = case when @InterestDaysType in (6) then case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate1, @SettledDate)) end else case when @AcqDate1 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate1, @SettledDate)) end end -- total hari [acq date 1 > prev coupon date]
set @DaysAcq2_2 = case when @InterestDaysType in (6) then case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate2, @SettledDate)) end else case when @AcqDate2 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate2, @SettledDate)) end end -- total hari [acq date 2 > prev coupon date]
set @DaysAcq3_2 = case when @InterestDaysType in (6) then case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate3, @SettledDate)) end else case when @AcqDate3 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate3, @SettledDate)) end end -- total hari [acq date 3 > prev coupon date]
set @DaysAcq4_2 = case when @InterestDaysType in (6) then case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate4, @SettledDate)) end else case when @AcqDate4 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate4, @SettledDate)) end end -- total hari [acq date 4 > prev coupon date]
set @DaysAcq5_2 = case when @InterestDaysType in (6) then case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate5, @SettledDate)) end else case when @AcqDate5 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate5, @SettledDate)) end end -- total hari [acq date 5 > prev coupon date]
set @DaysAcq6_2 = case when @InterestDaysType in (6) then case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate6, @SettledDate)) end else case when @AcqDate6 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate6, @SettledDate)) end end -- total hari [acq date 6 > prev coupon date]
set @DaysAcq7_2 = case when @InterestDaysType in (6) then case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate7, @SettledDate)) end else case when @AcqDate7 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate7, @SettledDate)) end end -- total hari [acq date 7 > prev coupon date]
set @DaysAcq8_2 = case when @InterestDaysType in (6) then case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate8, @SettledDate)) end else case when @AcqDate8 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate8, @SettledDate)) end end -- total hari [acq date 8 > prev coupon date]
set @DaysAcq9_2 = case when @InterestDaysType in (6) then case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond_ISMA30360](@AcqDate9, @SettledDate)) end else case when @AcqDate9 <= @LastCouponDate then abs([dbo].[FGetDateDIffCorporateBond](@LastCouponDate, @SettledDate)) else abs([dbo].[FGetDateDIffCorporateBond](@AcqDate9, @SettledDate)) end end -- total hari [acq date 9 > prev coupon date]


set @DaysAcq_2		= case when (isnull(@AcqDate, '') = '' or year(@AcqDate) = 1900) then 0 else @DaysAcq_2 end
set @DaysAcq1_2		= case when (isnull(@AcqDate1, '') = '' or year(@AcqDate1) = 1900) then 0 else @DaysAcq1_2 end
set @DaysAcq2_2		= case when (isnull(@AcqDate2, '') = '' or year(@AcqDate2) = 1900) then 0 else @DaysAcq2_2 end
set @DaysAcq3_2		= case when (isnull(@AcqDate3, '') = '' or year(@AcqDate3) = 1900) then 0 else @DaysAcq3_2 end
set @DaysAcq4_2		= case when (isnull(@AcqDate4, '') = '' or year(@AcqDate4) = 1900) then 0 else @DaysAcq4_2 end
set @DaysAcq5_2		= case when (isnull(@AcqDate5, '') = '' or year(@AcqDate5) = 1900) then 0 else @DaysAcq5_2 end
set @DaysAcq6_2		= case when (isnull(@AcqDate6, '') = '' or year(@AcqDate6) = 1900) then 0 else @DaysAcq6_2 end
set @DaysAcq7_2		= case when (isnull(@AcqDate7, '') = '' or year(@AcqDate7) = 1900) then 0 else @DaysAcq7_2 end
set @DaysAcq8_2		= case when (isnull(@AcqDate8, '') = '' or year(@AcqDate8) = 1900) then 0 else @DaysAcq8_2 end
set @DaysAcq9_2		= case when (isnull(@AcqDate9, '') = '' or year(@AcqDate9) = 1900) then 0 else @DaysAcq9_2 end



set @CapGainAcq	= case when @TrxType = 2 then ((@Price - @AcqPrice) * @AcqVolume / 100) when @TrxType = 1 then  ((@Price - @AcqPrice) * @AcqVolume / 100)  else 0 end
set @CapGainAcq1 = case when @TrxType = 2 then ((@Price - @AcqPrice1) * @AcqVolume1 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice1) * @AcqVolume1 / 100)  else 0 end
set @CapGainAcq2 = case when @TrxType = 2 then ((@Price - @AcqPrice2) * @AcqVolume2 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice2) * @AcqVolume2 / 100)  else 0 end
set @CapGainAcq3 = case when @TrxType = 2 then ((@Price - @AcqPrice3) * @AcqVolume3 / 100) when @TrxType = 1 then  ((@Price - @AcqPrice3) * @AcqVolume3 / 100)  else 0 end
set @CapGainAcq4 = case when @TrxType = 2 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100) when @TrxType = 1 then ((@Price - @AcqPrice4) * @AcqVolume4 / 100)  else 0 end
set @CapGainAcq5 = case when @TrxType = 2 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100) when @TrxType = 1 then ((@Price - @AcqPrice5) * @AcqVolume5 / 100)  else 0 end
set @CapGainAcq6 = case when @TrxType = 2 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100) when @TrxType = 1 then ((@Price - @AcqPrice6) * @AcqVolume6 / 100)  else 0 end
set @CapGainAcq7 = case when @TrxType = 2 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100) when @TrxType = 1 then ((@Price - @AcqPrice7) * @AcqVolume7 / 100)  else 0 end
set @CapGainAcq8 = case when @TrxType = 2 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100) when @TrxType = 1 then ((@Price - @AcqPrice8) * @AcqVolume8 / 100)  else 0 end
set @CapGainAcq9 = case when @TrxType = 2 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100) when @TrxType = 1 then ((@Price - @AcqPrice9) * @AcqVolume9 / 100)  else 0 end



set @TaxCapGainAcq	= @CapGainAcq * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq1	= @CapGainAcq1 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq2	= @CapGainAcq2 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq3	= @CapGainAcq3 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq4	= @CapGainAcq4 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq5	= @CapGainAcq5 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq6	= @CapGainAcq6 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq7	= @CapGainAcq7 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq8	= @CapGainAcq8 * @TaxCapitaGainPercent / 100
set @TaxCapGainAcq9	= @CapGainAcq9 * @TaxCapitaGainPercent / 100

set @AIAcq = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1
then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume * @CouponRate / 100 * @DaysAcq_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq1 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume1 * @CouponRate / 100 * @DaysAcq1_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq2 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume2 * @CouponRate / 100 * @DaysAcq2_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq3 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume3 * @CouponRate / 100 * @DaysAcq3_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq4 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume4 * @CouponRate / 100 * @DaysAcq4_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq5 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume5 * @CouponRate / 100 * @DaysAcq5_2 / @DivDays / @InterestPaymentType) end else 0 end

set @AIAcq6 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 * @CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume6 * @CouponRate / 100 * @DaysAcq6_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq7 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 * @CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume7 * @CouponRate / 100 * @DaysAcq7_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq8 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 * @CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume8 * @CouponRate / 100 * @DaysAcq8_2 / @DivDays / @InterestPaymentType) end else 0 end
set @AIAcq9 = case when @TrxType = 2 then case when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 * @CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType) end when @TrxType = 1 then case  when isnull(@InterestDays, 0) = 0 then 0 else (@AcqVolume9 * @CouponRate / 100 * @DaysAcq9_2 / @DivDays / @InterestPaymentType) end else 0 end


set @TaxAIAcq	= @AIAcq * @IncomeTaxInterestPercent / 100
set @TaxAIAcq1	= @AIAcq1 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq2	= @AIAcq2 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq3	= @AIAcq3 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq4	= @AIAcq4 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq5	= @AIAcq5 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq6	= @AIAcq6 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq7	= @AIAcq7 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq8	= @AIAcq8 * @IncomeTaxInterestPercent / 100
set @TaxAIAcq9	= @AIAcq9 * @IncomeTaxInterestPercent / 100

		
			
		
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
	set @ValuePerUnit = 1000000
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

--if @InterestType <> 3 --ZERO COUPON
--BEGIN
	if @InstrumentTypePK in (3,8,9,15)
	BEGIN
		set @AccuredInterestAmount = @Volume * (@CouponRate / 100) * @Days / @DivDays / @InterestPaymentType
		set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
	END
	ELSE IF @InstrumentTypePK in (2,13)
	BEGIN
		--IF @BitIsRounding = 1
		--BEGIN
			set @AccuredInterestAmount = @Volume / @ValuePerUnit * (round(@CouponRate / 100 
			* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit, 0))
			set @GrossAmount = round((@Volume * @price/100),0) + @AccuredInterestAmount
		--END
		--ELSE
		--BEGIN
		--	set @AccuredInterestAmount = @Volume / @ValuePerUnit * (@CouponRate / 100 
		--	* @Days / @DivDays / @InterestPaymentType * @ValuePerUnit)
		--	set @GrossAmount = (@Volume * @price/100) + @AccuredInterestAmount
		--END
	END
--END


IF(@ClientCode = '21')
BEGIN

    set @TotalTaxCapGain = isnull(@TaxCapGainAcq,0)
    + isnull(@TaxCapGainAcq1,0)
    + isnull(@TaxCapGainAcq2,0)
    + isnull(@TaxCapGainAcq3,0)
    + isnull(@TaxCapGainAcq4,0)
    + isnull(@TaxCapGainAcq5,0)
    
   

END
ELSE
BEGIN
    set @TotalTaxCapGain = case when isnull(@TaxCapGainAcq,0) > 0 then  isnull(@TaxCapGainAcq,0) else 0 end 
    + case when isnull(@TaxCapGainAcq1,0) > 0 then isnull(@TaxCapGainAcq1,0) else 0 end 
    + case when isnull(@TaxCapGainAcq2,0) > 0 then isnull(@TaxCapGainAcq2,0) else 0 end
    + case when isnull(@TaxCapGainAcq3,0) > 0 then isnull(@TaxCapGainAcq3,0) else 0 end
    + case when isnull(@TaxCapGainAcq4,0) > 0 then isnull(@TaxCapGainAcq4,0) else 0 end 
    + case when isnull(@TaxCapGainAcq5,0) > 0 then isnull(@TaxCapGainAcq5,0) else 0 end 
    + case when isnull(@TaxCapGainAcq6,0) > 0 then isnull(@TaxCapGainAcq6,0) else 0 end 
    + case when isnull(@TaxCapGainAcq7,0) > 0 then isnull(@TaxCapGainAcq7,0) else 0 end 
    + case when isnull(@TaxCapGainAcq8,0) > 0 then isnull(@TaxCapGainAcq8,0) else 0 end 
    + case when isnull(@TaxCapGainAcq9,0) > 0 then isnull(@TaxCapGainAcq9,0) else 0 end 

    set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
    + case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
    + case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
    + case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
    + case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
    + case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 
    + case when isnull(@TaxAIAcq6,0) > 0 then isnull(@TaxAIAcq6,0) else 0 end 
    + case when isnull(@TaxAIAcq7,0) > 0 then isnull(@TaxAIAcq7,0) else 0 end 
    + case when isnull(@TaxAIAcq8,0) > 0 then isnull(@TaxAIAcq8,0) else 0 end 
    + case when isnull(@TaxAIAcq9,0) > 0 then isnull(@TaxAIAcq9,0) else 0 end 
END






set @TotalCapGain = isnull(@CapGainAcq,0) + isnull(@CapGainAcq1,0) + isnull(@CapGainAcq2,0) + isnull(@CapGainAcq3,0) + isnull(@CapGainAcq4,0) + isnull(@CapGainAcq5,0) + isnull(@CapGainAcq6,0) + isnull(@CapGainAcq7,0) + isnull(@CapGainAcq8,0) + isnull(@CapGainAcq9,0)
set @TotalAI = isnull(@AIAcq,0) + isnull(@AIAcq1,0) + isnull(@AIAcq2,0) + isnull(@AIAcq3,0) + isnull(@AIAcq4,0) + isnull(@AIAcq5,0) + isnull(@AIAcq6,0) + isnull(@AIAcq7,0) + isnull(@AIAcq8,0) + isnull(@AIAcq9,0)




IF (@BitNoCapitalGainTax) = 1
BEGIN

	set @TotalTaxAI =  isnull(@TaxCapGainAcq,0) + isnull(@TaxCapGainAcq1,0) + isnull(@TaxCapGainAcq2,0) + isnull(@TaxCapGainAcq3,0) + isnull(@TaxCapGainAcq4,0)
	+ isnull(@TaxCapGainAcq5,0) + isnull(@TaxCapGainAcq6,0) + isnull(@TaxCapGainAcq7,0) + isnull(@TaxCapGainAcq8,0) + isnull(@TaxCapGainAcq9,0) + isnull(@TotalTaxAI,0) 

	set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
END
ELSE
BEGIN

	set @TotalTax = (@TotalTaxCapGain + @TotalTaxAI)
END

IF (@ClientCode = '21')
BEGIN
set @TotalTaxAI = isnull(@TaxAIAcq,0) + isnull(@TaxAIAcq1,0) + isnull(@TaxAIAcq2,0) + isnull(@TaxAIAcq3,0) + isnull(@TaxAIAcq4,0) + isnull(@TaxAIAcq5,0)

set @TotalTaxAI = case when isnull(@TaxAIAcq,0) > 0 then isnull(@TaxAIAcq,0) else 0 end 
+ case when isnull(@TaxAIAcq1,0) > 0 then isnull(@TaxAIAcq1,0) else 0 end 
+ case when isnull(@TaxAIAcq2,0) > 0 then isnull(@TaxAIAcq2,0) else 0 end 
+ case when isnull(@TaxAIAcq3,0) > 0 then isnull(@TaxAIAcq3,0) else 0 end 
+ case when isnull(@TaxAIAcq4,0) > 0 then isnull(@TaxAIAcq4,0) else 0 end  
+ case when isnull(@TaxAIAcq5,0) > 0 then isnull(@TaxAIAcq5,0) else 0 end 
END

IF (@ClientCode = '21' AND @TrxType = 2)
BEGIN
    set @TotalTax = (isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxCapGain end,0) 
                    + isnull(case when (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) < 0 then 0 else @TotalTaxAI end,0))
END
ELSE
BEGIN
    set @TotalTax = (isnull(@TotalTaxCapGain,0) + isnull(@TotalTaxAI,0)) 
END




set @NetAmount = isnull(@GrossAmount,0) - isnull(@TotalTax,0)


--Select @TotalTaxCapGain taxCapGain,@TotalCapGain CapGain,@TotalAI Ai,@TotalTaxAI taxAi,@TotalTax TotalTax,@NetAmount Net,@GrossAmount gross, @AccuredInterestAmount Holding

                         


Fetch next From A Into @DealingPK,@HistoryPK,@ValueDate,@InstrumentPK,@InstrumentTypePK,
@SettledDate,@NextCouponDate,@LastCouponDate,
@TrxType, @Price,@Volume,@AcqPrice,@AcqDate,
@AcqVolume,@TaxCapitaGainPercent,@IncomeTaxInterestPercent,
@BitIsRounding,@TaxExpensePercent,
@CurrencyID,@CouponRate,@InterestType, 
@InterestDaysType,@InterestPaymentType,
@AcqPrice1,@AcqDate1,@AcqVolume1,@AcqPrice2,@AcqDate2,@AcqVolume2,@AcqPrice3,@AcqDate3,@AcqVolume3,
@AcqPrice4,@AcqDate4,@AcqVolume4,@AcqPrice5,@AcqDate5,@AcqVolume5,@AcqPrice6,@AcqDate6,@AcqVolume6,
@AcqPrice7,@AcqDate7,@AcqVolume7,@AcqPrice8,@AcqDate8,@AcqVolume8,@AcqPrice9,@AcqDate9,@AcqVolume9
END
Close A
Deallocate A

Select isnull(@AccuredInterestAmount,0) InterestAmount, isnull(@TotalTaxAI,0) IncomeTaxInterestAmount,isnull(@TotalTaxCapGain,0) IncomeTaxGainAmount,
isnull(@GrossAmount,0) GrossAmount, isnull(@NetAmount,0) NetAmount   ";

                        cmd.Parameters.AddWithValue("@CDealingPK", _omsBond.DealingPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _omsBond.CounterpartPK);
                        cmd.Parameters.AddWithValue("@ParamTaxGainPercent", _omsBond.IncomeTaxGainPercent);
                        cmd.Parameters.AddWithValue("@ParamTaxInterestPercent", _omsBond.IncomeTaxInterestPercent);
                        cmd.Parameters.AddWithValue("@ParamAcqPrice", _omsBond.AcqPrice);
                        cmd.Parameters.AddWithValue("@ParamDonePrice", _omsBond.DonePrice);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setOMSBondForNetAmount(dr);
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


        private OMSBondForNetAmount setOMSBondForNetAmount(SqlDataReader dr)
        {
            OMSBondForNetAmount M_BondForNetAmount = new OMSBondForNetAmount();
            M_BondForNetAmount.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
            M_BondForNetAmount.IncomeTaxInterestAmount = Convert.ToDecimal(dr["IncomeTaxInterestAmount"]);
            M_BondForNetAmount.IncomeTaxGainAmount = Convert.ToDecimal(dr["IncomeTaxGainAmount"]);
            M_BondForNetAmount.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
            M_BondForNetAmount.NetAmount = Convert.ToDecimal(dr["NetAmount"]);

            return M_BondForNetAmount;
        }

        public FundPriceExposure Validate_CheckRangePriceExposure(DateTime _valuedate, int _instrumentPK, decimal _price)
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
                            
                        declare @Status int
                        declare @Message nvarchar(max)
                        declare @MinPrice numeric(18,8)
                        declare @MaxPrice numeric(18,8)
                        declare @MaxDate date

                        select @MaxDate = max(Date) from ClosePrice where InstrumentPK = @InstrumentPK and status = 2 and date <= @ValueDate
 
                        set @Status = 0
                        set @Message = ''


                        if @MaxDate is not null
                        begin

	                        select @MinPrice = LowPriceValue, @MaxPrice = HighPriceValue from ClosePrice where InstrumentPK = @InstrumentPK and date = @MaxDate and status = 2

	                        if @Price not between @MinPrice and @MaxPrice and @MaxPrice <> 0
	                        begin
		                        set @Status = 1
		                        set @Message = 'Price should be between </br> Min Price  : ' + cast(@MinPrice as nvarchar) + ' </br> and Max Price : ' + cast(@MaxPrice as nvarchar) + '</br></br>'
	                        end

                        end

                        select @Status Validate,@Message Result

                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valuedate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@Price", _price);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundPriceExposure()
                                {
                                    Validate = dr["Validate"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Validate"]),
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? "" : dr["Result"].ToString(),
                                };
                            }
                            else
                            {
                                return new FundPriceExposure()
                                {
                                    Validate = 0,
                                    Result = "",

                                };
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

        public string Insert_ExposureRangePrice(DateTime _valuedate, int _instrumentPK, int _fundPK, decimal _price, int _InvestmentPK, string _UsersID)
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
                            declare @Minprice numeric(18,4)
                            declare @Maxprice numeric(18,4)
                            declare @parameter numeric(18,4)
                            declare @Reason nvarchar(max)
                            DECLARE @PK int
                            declare @FundID nvarchar(100)
                            declare @InstrumentID nvarchar(100)

                            set @Reason = '';

		                    select @Minprice = LowPriceValue, @MaxPrice = HighPriceValue
		                    From ClosePrice A
			                where A.Date = (select max(date) from ClosePrice A where A.date <= @Date and A.InstrumentPK = @instrumentPK and A.status  = 2)
                            and A.status = 2 and A.InstrumentPK = @InstrumentPK
                            

                            select @FundID = ID from Fund where status = 2 and FundPK = @FundPK
                            set @FundID = isnull(@FundID,'')

                            select @InstrumentID = ID from Instrument where status = 2 and InstrumentPK = @InstrumentPK
                            set @InstrumentID = isnull(@InstrumentID,'')

                            if @price not between @Minprice and @MaxPrice
                                set @Reason = 'EXPOSURE PRICE <br /><br /> FUND : ' + @FundID + ' <br /> INSTRUMENT : ' + @InstrumentID +'  <br /> PRICE : ' + cast(@price as nvarchar) + '  <br /> MIN : ' + cast(@Minprice as nvarchar) + '  <br /> MAX : ' + cast(@Maxprice as nvarchar)

                            select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		                    set @PK = isnull(@PK,1)

                            DECLARE @r varchar(50)

                            SELECT @r = coalesce(@r, '') + n
                            FROM (SELECT top 50 
                            CHAR(number) n FROM
                            master..spt_values
                            WHERE type = 'P' AND 
                            (number between ascii(0) and ascii(9)
                            or number between ascii('A') and ascii('Z')
                            or number between ascii('a') and ascii('z'))
                            ORDER BY newid()) a

                            while exists (select * from HighRiskMonitoring where SecurityEmail = @r)
                            begin
	                            SELECT @r = coalesce(@r, '') + n
	                            FROM (SELECT top 50 
	                            CHAR(number) n FROM
	                            master..spt_values
	                            WHERE type = 'P' AND 
	                            (number between ascii(0) and ascii(9)
	                            or number between ascii('A') and ascii('Z')
	                            or number between ascii('a') and ascii('z'))
	                            ORDER BY newid()) a
                            end

                            IF (@ClientCode <> '05')
                            BEGIN
                                if @Reason != ''
                                begin
                                    insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,Description,SecurityEmail,EmailExpiredTime)
		                            Select @PK,1,1,2,@InvestmentPK,@date,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,'Range Price',@r,DATEADD(MINUTE," + Tools._EmailSessionTime + @",@lastUpdate)
                                end
                            END


                            select 'Success' Result


                           ";

                        cmd.Parameters.AddWithValue("@date", _valuedate);
                        cmd.Parameters.AddWithValue("@instrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@fundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@price", _price);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _InvestmentPK);
                        cmd.Parameters.AddWithValue("@UsersID", _UsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
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

        //bagian fifo disini

        public int InvestmentFifoBond_RejectOMSBondBySelected(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramInvestmentPK = "";
                        string _paramCInvestmentPK = "";
                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = "  And InvestmentPK in (0) ";
                        }

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramCInvestmentPK = " B.InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramCInvestmentPK = "  B.InvestmentPK in (0) ";
                        }
                        if (_investment.FundID != "0")
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"


                            CREATE TABLE #Investment
                            (
	                            InvestmentPK int,
	                            HistoryPK int,
	                            FundPK int,
	                            CrossFundFromPK int,
	                            Valuedate datetime,
	                            InstrumentPK int,
	                            InstrumentTypePK int,
	                            OrderPrice numeric(19,4),
	                            Volume numeric(19,4),
	                            Amount numeric(19,4),
	                            SettlementDate datetime
                            )


                            insert into #Investment
                            select InvestmentPK,HistoryPK,FundPK, CrossFundFromPK, ValueDate, InstrumentPK, 
                            InstrumentTypePK, OrderPrice, Volume, Amount, SettlementDate
                            from Investment where StatusInvestment in (1,2) " + _paramInvestmentPK + @"


                            Update A set StatusInvestment  = 3,statusDealing = 0,statusSettlement = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime  from Investment A
                            left join #Investment B on A.FundPK = B.CrossFundFromPK and A.CrossFundFromPK = B.FundPK 
                            and A.ValueDate = B.Valuedate and A.InstrumentPK = B.InstrumentPK and A.InstrumentTypePK = B.InstrumentTypePK
                            and A.OrderPrice = B.OrderPrice and A.Volume = B.Volume and A.Amount = B.Amount 
                            and A.SettlementDate = B.SettlementDate 
                            where " + _paramCInvestmentPK + @" and A.CrossFundFromPK <> 0 and B.CrossFundFromPK <> 0

-------------------------------------------------------------------

                            declare @InvestmentPK int
                            declare @DealingPK int
                            declare @HistoryPK int
                            declare @AcqPriceInv numeric(22,4)
                            declare @AcqVolumeInv numeric(22,4)
                            declare @AcqDateInv date
							declare @instrumentpk int
	                        declare @FundPK1 int
	                        declare @Price numeric(22,4)
	                        declare @volume numeric(22,4)
	                        declare @DoneVolume numeric(22,4)
	                        declare @RemainingVolume numeric(22,4)
	                        declare @maxFifoDate date
							declare @FifobondPositionPK int
	                        declare @Query nvarchar(500)
	                        declare @InvestmentSellPK int
	                        declare @InvestmentBuyPK int
	                        declare @AcqPrice numeric(22,4)
	                        declare @AcqVolume numeric(22,4)
	                        declare @AcqDate date

                            declare @tableInvestment table (
                                InvestmentPK int,
                                DealingPK int,
                                HistoryPK int,
                                InstrumentPK int,
                                FundPK int,
                                DoneVolume numeric(32,4),
                                AcqDate date,
                                AcqPrice numeric(19,8),
                                AcqVolume numeric(32,4)
                            )
 
                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate,AcqPrice,AcqVolume from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate1,AcqPrice1,AcqVolume1 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate1 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate2,AcqPrice2,AcqVolume2 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate2 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate3,AcqPrice3,AcqVolume3 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate3 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate4,AcqPrice4,AcqVolume4 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate4 is not null "
                            + _paramFund +
                            @"

                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate5,AcqPrice5,AcqVolume5 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate5 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate6,AcqPrice6,AcqVolume6 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate6 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate7,AcqPrice7,AcqVolume7 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate7 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate8,AcqPrice8,AcqVolume8 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate8 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate9,AcqPrice9,AcqVolume9 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate9 is not null "
                            + _paramFund +
                            @"

                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate10,AcqPrice10,AcqVolume10 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate10 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate11,AcqPrice11,AcqVolume11 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate11 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate12,AcqPrice12,AcqVolume12 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate12 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate13,AcqPrice13,AcqVolume13 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate13 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate14,AcqPrice14,AcqVolume14 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate14 is not null "
                            + _paramFund +
                            @"

                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate15,AcqPrice15,AcqVolume15 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate15 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate16,AcqPrice16,AcqVolume16 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate16 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate17,AcqPrice17,AcqVolume17 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate17 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate18,AcqPrice18,AcqVolume18 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate18 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate19,AcqPrice19,AcqVolume19 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate19 is not null "
                            + _paramFund +
                            @"

                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate20,AcqPrice20,AcqVolume20 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate20 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate21,AcqPrice21,AcqVolume21 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate21 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate22,AcqPrice22,AcqVolume22 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate22 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate23,AcqPrice23,AcqVolume23 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate23 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate24,AcqPrice24,AcqVolume24 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate24 is not null "
                            + _paramFund +
                            @"

                            insert into @tableInvestment(InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume)
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate25,AcqPrice25,AcqVolume25 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate25 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate26,AcqPrice26,AcqVolume26 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate26 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate27,AcqPrice27,AcqVolume27 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2)  and AcqDate27 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate28,AcqPrice28,AcqVolume28 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate28 is not null "
                            + _paramFund +
                            @"

                            union all
                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPk,FundPk,DoneVolume,AcqDate29,AcqPrice29,AcqVolume29 from Investment 
	                        where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" 
							and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) and AcqDate29 is not null "
                            + _paramFund +
                            @"

                            DECLARE A CURSOR FOR 

	                            select InvestmentPK,DealingPK,HistoryPK,InstrumentPK,FundPK,DoneVolume,AcqDate,AcqPrice,AcqVolume from @tableInvestment
							
                                Open A
                            Fetch Next From A
                            Into @InvestmentPK,@DealingPK,@HistoryPK,@instrumentpk,@FundPK1,@DoneVolume,@AcqDateInv,@AcqPriceInv,@AcqVolumeInv

                            While @@FETCH_STATUS = 0
                            BEGIN              
	                            

	                            if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
	                            create table #tableFifoSelect
	                            (
		                            FifoBondPositionPK int,
									InvestmentBuyPK int,
		                            InvestmentPK int,
		                            AcqDate date,
		                            AcqVolume numeric(22,4),
		                            RemainingVolume numeric(22,4),
		                            AcqPrice numeric(22,4)
	                            )
	                            CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


	                            if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
	                            create table #tableInvest
	                            (
		                            FifoBondPositionPK int,
		                            InvesmentBuyPK int,
		                            InvesmentSellPK int,
		                            AcqDate date,
		                            AcqVolume numeric(22,4),
		                            AcqPrice numeric(22,4),
		                            RemainingVolume numeric(22,4)
	                            )
	                            CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

	                            insert into #tableFifoSelect(FifoBondPositionPK,InvestmentBuyPK,InvestmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                            select A.FifoBondPositionPK,InvestmentBuyPK,A.InvestmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice,RemainingVolume from (
			                            select FifoBondPositionPK,0 InvestmentBuyPK,InvestmentPK,AcqDate,AcqVolume DoneVolume,AcqPrice,RemainingVolume from FifoBondPosition 
			                            where FundPK = @FundPK1 and InstrumentPk = @instrumentpk and status in (1,2) and AcqPrice = @AcqPriceInv and AcqDate = @AcqDateInv
			                            union all
			                            select 0,InvestmentBuyPK,InvestmentSellPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume from FifoBondPositionTemp
			                            where FundPK = @FundPK1 and InstrumentPK = @instrumentpk and AcqPrice = @AcqPriceInv and AcqDate = @AcqDateInv --and InvestmentSellPK <> 0
	                            )A 
	                            order by A.AcqDate					

	                            --select * from #tableFifoSelect
	                            --order by AcqDate
	                            -- @AcqDateInv,@AcqPriceInv,@AcqVolumeInv
	                            --order by AcqDate
	                            --select @AcqDateInv = AcqDate, @AcqPriceInv = AcqPrice, @AcqVolumeInv = AcqVolume from investment where InvestmentPK = @investmentpk and StatusInvestment = 2 and TrxType = 2
	

	                            
								
	                            DECLARE AB CURSOR FOR   
		                            select FifoBondPositionPK,InvestmentPK,AcqVolume,AcqPrice,AcqDate,RemainingVolume,InvestmentBuyPK from #tableFifoSelect 
                                    where AcqPrice = @AcqPriceInv and AcqDate = @AcqDateInv and InvestmentPK = @InvestmentPK order by AcqDate desc
	                            OPEN AB  
	                            FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate,@RemainingVolume,@InvestmentBuyPK
  
	                            WHILE @@FETCH_STATUS = 0  
	                            BEGIN  
		                            if @FifobondPositionPK = 0
		                            begin
			                            if (@AcqDate = @AcqDateInv and @AcqPrice = @AcqPriceInv and @DoneVolume = @AcqVolumeInv)
				                            update FifoBondPositionTemp set RemainingVolume = @AcqVolumeInv + @RemainingVolume,InvestmentSellPK = 0 where InvestmentBuyPK = @InvestmentBuyPK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @instrumentpk
				                            --select @AcqVolumeInv ,@AcqVolume, @RemainingVolume,'101',@InvestmentBuyPK
			                            else
				                            update FifoBondPositionTemp set RemainingVolume = @AcqVolumeInv + @RemainingVolume,InvestmentSellPK = 0 where InvestmentBuyPK = @InvestmentBuyPK and AcqDate = @AcqDate and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @instrumentpk
											--select @AcqVolume,'102', @RemainingVolume
									end
		                            else
		                            begin
			                            if (@AcqDate = @AcqDateInv and @AcqPrice = @AcqPriceInv and @DoneVolume = @AcqVolumeInv)
				                            update FifoBondPosition set RemainingVolume = @AcqVolumeInv + @RemainingVolume,InvestmentPK = 0 where InvestmentPK = @InvestmentPK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @instrumentpk
				                            --select @AcqVolumeInv + @RemainingVolume,'105'
			                            else
				                            update FifoBondPosition set RemainingVolume = @AcqVolumeInv + @RemainingVolume,InvestmentPK = 0 where InvestmentPK = @InvestmentPK and AcqDate = @AcqDate and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @instrumentpk
				                            --select @AcqVolume,'109', @RemainingVolume, @DoneVolume, @AcqDate,@AcqPrice, @AcqVolumeInv
		                            end
		
		
		
	                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate,@RemainingVolume,@InvestmentBuyPK
	                            end
	                            CLOSE AB  
	                            DEALLOCATE AB 

                            Fetch next From A Into @InvestmentPK,@DealingPK,@HistoryPK,@instrumentpk,@FundPK1,@DoneVolume,@AcqDateInv,@AcqPriceInv,@AcqVolumeInv
                            END
                            Close A
                            Deallocate A

                            Update Investment set StatusInvestment  = 3,statusDealing = 0,statusSettlement = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime
                            where InstrumentTypePK  in (2,3,8,9,13,15) and TrxType = @TrxType " + _paramInvestmentPK + @" and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) 
                            " + _paramFund +
                            " Update Investment set SelectedInvestment  = 0";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _investment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
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

        public int ValidateFifoBond_CheckAvailableInstrument(Investment _investment)
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
                           
Declare @TrailsPK int
Declare @TrailsDate datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK,@TrailsDate = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date  and FundPK = @FundPK   
)
and status = 2 and FundPK = @FundPK                                  

Declare @CurrBalance numeric (18,4)

select @CurrBalance =  A.Balance + sum(isnull(B.MovBalance,0)) from (

select AB.FundPK,AB.InstrumentPK,AB.InstrumentID,sum(isnull(AB.Balance,0)) Balance,AB.CurrencyID,AB.AcqDate,AB.InterestPercent,AB.MaturityDate,AB.TrxBuy,AB.TrxBuyType, AB.AvgPrice,AB.InterestPaymentType from(
select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(A.Balance) Balance,'IDR' CurrencyID,'1900-01-01' AcqDate,A.InterestPercent,
A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 AvgPrice,B.InterestPaymentType from FundPosition A    
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15)      and A.status = 2         
group by A.FundPK,A.InstrumentPK,B.ID,B.Name,A.InterestPercent,A.MaturityDate,B.InterestPaymentType
union all	
Select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR','1900-01-01' AcqDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 DonePrice,B.InterestPaymentType
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
and FundPK = @FundPK and TrxType  = 1  
group By A.FundPK,A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,
TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType
)AB
group By AB.FundPK,AB.InstrumentPK,AB.InstrumentID,AB.CurrencyID,AB.AcqDate,AB.InterestPercent,AB.MaturityDate,AB.TrxBuy,AB.TrxBuyType, AB.AvgPrice,AB.InterestPaymentType


) A 
left join
(
Select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR' CurrencyID,'1900-01-01' ValueDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,'' TrxBuyType,0 DonePrice,B.InterestPaymentType
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
where (@ClientCode = '20' or @ClientCode = '21' and ValueDate > @TrailsDate and ValueDate <= @date )  or (@ClientCode = '03' and ValueDate = @Date )
and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and A.instrumentTypePK in (2,3,8,9,13,15)
and FundPK = @FundPK and TrxType  = 2
group By A.FundPK,A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType
) B on  A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status = 2
where A.InstrumentPK = @InstrumentPK
Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,C.Priority
IF (@Balance > @CurrBalance)
BEGIN
	select 1 Result 
END
ELSE
BEGIN
	select 2 Result
END

                           ";

                        cmd.Parameters.AddWithValue("@date", _investment.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                        cmd.Parameters.AddWithValue("@Balance", _investment.Volume);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        //cmd.Parameters.AddWithValue("@TrxBuyType", _investment.TrxBuyType);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);
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

        //end bagian fifo

    }
}