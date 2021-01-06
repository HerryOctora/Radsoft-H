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
using System.Data.OleDb;

namespace RFSRepository
{
    public class OMSEquityReps
    {
        Host _host = new Host();

        private OMSEquity setOMSEquity(SqlDataReader dr)
        {
            OMSEquity M_temp = new OMSEquity();
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

        public List<OMSEquity> OMSEquity_PerFund(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquity> L_OMSEquity = new List<OMSEquity>();
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
	valuedate < @Date  and status = 2  and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK


Declare @DateT1 datetime  
Declare @DateT2 datetime  
Declare @DateT3 datetime 
  
set @DateT1 = dbo.FWorkingDay(@Date,1)  
set @DateT2 = dbo.FWorkingDay(@Date,2)  
set @DateT3 = dbo.FWorkingDay(@Date,3) 

  
Declare @AUM numeric(32,4)  
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

Create table #tmpOmsEquityPerFundBalance
(
Name nvarchar(200) COLLATE DATABASE_DEFAULT,
CurrBalance decimal(32,4),
CurrNAVPercent decimal(32,4),
Movement decimal(32,4),
AfterBalance decimal(32,4),
AfterNAVPercent decimal(32,4),
MovementTOne decimal(32,4),
AfterTOne decimal(32,4),
AfterTOneNAVPercent decimal(32,4),
MovementTTwo decimal(32,4),
AfterTTwo decimal(32,4),
AfterTTwoNAVPercent decimal(32,4),
MovementTThree numeric(32,4),
AfterTThree numeric(32,4),
AfterTThreeNAVPercent numeric(32,6),
)          
	
CREATE CLUSTERED INDEX indx_tmpOmsEquityPerFundBalance ON #tmpOmsEquityPerFundBalance (Name,CurrBalance,CurrNAVPercent,Movement,
AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,
AfterTThree,AfterTThreeNAVPercent);   


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2



	
insert into #tmpOmsEquityPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)       
  
Select  B.ID,sum(MarketValue) CurrBalance, isnull(sum(MarketValue)/@AUM * 100,0) [CurrNAVPercent]
,(case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0) Movement
	
,sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)  AfterBalance
	
,isnull((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)) / @AUM * 100,0) AfterNAVPercent,  
   
isnull(F.Movement,0)  MovementTOne,  
(sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)  AfterTOne,  
isnull(((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))+ isnull(F.Movement,0) ) / @AUM * 100,0) AfterTOneNAVPercent,  
   
isnull(H.Movement,0)  MovementTTwo,  
    (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  AfterTTwo,  
isnull((   (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)    + isnull(H.Movement,0) ) / @AUM * 100,0) AfterTTwoNAVPercent,

isnull(N.Movement,0)  MovementTThree,  
    (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0)  AfterTThree,  
isnull(((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0) ) / @AUM * 100,0) AfterTThreeNAVPercent    
from [FundPosition] A  
left join #LastAVgFromFP C on A.Date = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  

left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @fundpk  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)F on B.InstrumentPK = F.InstrumentPK  

left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)H on B.InstrumentPK = H.InstrumentPK  

left join -- T3 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)N on B.InstrumentPK = N.InstrumentPK  

where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
and J.Type = 1
Group by B.ID,D.Movement,F.Movement,H.Movement,N.Movement,A.ClosePrice,A.InstrumentPK,C.MarketInfoPrice,C.ClosePrice



Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsEquityPerFundBalance)


insert into #tmpOmsEquityPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)   
Select B.ID,0,0
,isnull(j.Movement,0)
,isnull(j.Movement,0)
,isnull(j.Movement,0) /@AUM * 100
,isnull(D.Movement,0) 
,isnull(j.Movement,0) + isnull(D.Movement,0) ,
isnull(j.Movement,0) + isnull(D.Movement,0) / @AUM * 100,
isnull(E.Movement,0) ,
isnull(j.Movement,0) ,
isnull(j.Movement,0) / @AUM * 100,
isnull(I.Movement,0),
isnull(j.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,
isnull(j.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0)  / @AUM * 100
From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
left join -- T0 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @Date and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)J on B.InstrumentPK = J.InstrumentPK  
left join -- T1 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT1 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT2 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)E on B.InstrumentPK = E.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT3 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)I on B.InstrumentPK = I.InstrumentPK  

where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsEquityPerFundBalance)
Group By B.ID,D.Movement,E.Movement,I.Movement,J.Movement

Select * from #tmpOmsEquityPerFundBalance 
where CurrBalance > 0 or Movement > 0 or
MovementTOne > 0 or AfterTOne > 0 or
MovementTTwo > 0 or AfterTTwo > 0 or
MovementTThree > 0 or  AfterTThree > 0

  ";
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

  
Declare @AUM numeric(32,4)  
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

Create table #tmpOmsEquityPerFundBalance
(
Name nvarchar(200) COLLATE DATABASE_DEFAULT,
CurrBalance decimal(32,4),
CurrNAVPercent decimal(32,4),
Movement decimal(32,4),
AfterBalance decimal(32,4),
AfterNAVPercent decimal(32,4),
MovementTOne decimal(32,4),
AfterTOne decimal(32,4),
AfterTOneNAVPercent decimal(32,4),
MovementTTwo decimal(32,4),
AfterTTwo decimal(32,4),
AfterTTwoNAVPercent decimal(32,4),
MovementTThree numeric(32,4),
AfterTThree numeric(32,4),
AfterTThreeNAVPercent numeric(32,6),
)          
	
CREATE CLUSTERED INDEX indx_tmpOmsEquityPerFundBalance ON #tmpOmsEquityPerFundBalance (Name,CurrBalance,CurrNAVPercent,Movement,
AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,
AfterTThree,AfterTThreeNAVPercent);   


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2



	
insert into #tmpOmsEquityPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)       
  
Select  B.ID,sum(MarketValue) CurrBalance, isnull(sum(MarketValue)/@AUM * 100,0) [CurrNAVPercent]
,(case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0) Movement
	
,sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)  AfterBalance
	
,isnull((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)) / @AUM * 100,0) AfterNAVPercent,  
   
isnull(F.Movement,0)  MovementTOne,  
(sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)  AfterTOne,  
isnull(((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))+ isnull(F.Movement,0) ) / @AUM * 100,0) AfterTOneNAVPercent,  
   
isnull(H.Movement,0)  MovementTTwo,  
    (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  AfterTTwo,  
isnull((   (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)    + isnull(H.Movement,0) ) / @AUM * 100,0) AfterTTwoNAVPercent,

isnull(N.Movement,0)  MovementTThree,  
    (sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0)  AfterTThree,  
isnull(((sum(MarketValue) + (case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0) ) / @AUM * 100,0) AfterTThreeNAVPercent    
from [FundPosition] A  
left join #LastAVgFromFP C on A.Date = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
left join -- T0 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  

left join -- T1 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT1 and FundPK = @fundpk  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)F on B.InstrumentPK = F.InstrumentPK  

left join -- T2 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT2 and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)H on B.InstrumentPK = H.InstrumentPK  

left join -- T3 from investment  
(  
Select isnull(sum(case when TrxType in(1,3) then DoneAmount else DoneAmount * -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
where ValueDate = @DateT3 and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)N on B.InstrumentPK = N.InstrumentPK  

where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
and J.Type = 1
Group by B.ID,D.Movement,F.Movement,H.Movement,N.Movement,A.ClosePrice,A.InstrumentPK,C.MarketInfoPrice,C.ClosePrice



Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsEquityPerFundBalance)


insert into #tmpOmsEquityPerFundBalance(
Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
)   
Select B.ID,0,0
,isnull(j.Movement,0)
,isnull(j.Movement,0)
,isnull(j.Movement,0) /@AUM * 100
,isnull(D.Movement,0) 
,isnull(j.Movement,0) + isnull(D.Movement,0) ,
isnull(j.Movement,0) + isnull(D.Movement,0) / @AUM * 100,
isnull(E.Movement,0) ,
isnull(j.Movement,0) ,
isnull(j.Movement,0) / @AUM * 100,
isnull(I.Movement,0),
isnull(j.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0) ,
isnull(j.Movement,0) + isnull(D.Movement,0) + isnull(E.Movement,0)  + isnull(I.Movement,0)  / @AUM * 100
From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
left join -- T0 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @Date and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)J on B.InstrumentPK = J.InstrumentPK  
left join -- T1 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT1 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)D on B.InstrumentPK = D.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT2 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)E on B.InstrumentPK = E.InstrumentPK  
left join -- T2 from investment  
(  
Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when C.MarketInfoPrice  = 0 then C.ClosePrice else C.MarketInfoPrice end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
left join #LastAVgFromInvestment C on A.ValueDate = C.Date and A.FundPK = C.FundPK and A.InstrumentPK = C.InstrumentPK
where ValueDate = @DateT3 and A.FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
Group By B.InstrumentPK  
)I on B.InstrumentPK = I.InstrumentPK  

where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
and B.ID not in (
select Name from #tmpOmsEquityPerFundBalance)
Group By B.ID,D.Movement,E.Movement,I.Movement,J.Movement

Select * from #tmpOmsEquityPerFundBalance 
where CurrBalance > 0 or Movement > 0 or
MovementTOne > 0 or AfterTOne > 0 or
MovementTTwo > 0 or AfterTTwo > 0 or
MovementTThree > 0 or  AfterTThree > 0


  ";
                        }



                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_OMSEquity.Add(setOMSEquity(dr));
                                }
                            }
                            return L_OMSEquity;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string OMSEquity_ListStockForYahooFinance(int _fundPK, DateTime _date)
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
                        Declare @result Nvarchar(1000)
                        set @result = ''
                        Select  @result = @result +  InstrumentID + '.JK,' from FundPosition A
						 Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
						left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
						where date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
						and C.Type = 1

	                    Select @result = @result +  B.ID + '.JK,' From Investment A
	                    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
						left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	                    where StatusInvestment <> 3 
	                    and A.InstrumentTypePK = 1 and ValueDate >= @Date
						and C.Type = 1

                        select left(@Result,len(@result) - 1) result
                        ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["result"].ToString();
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

        public List<OMSExposureEquity> OMSExposureEquity_PerFund(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSExposureEquity> L_model = new List<OMSExposureEquity>();
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
	valuedate < @Date  and status = 2  and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK



Create table #OMSEquityExposure
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


Create table #OMSEquityExposureTemp
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





Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER INDEX' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2  and date <= @Date
	) and status = 2 
	
)E on A.InstrumentPK = E.InstrumentPK
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)C on E.IndexPK = C.Parameter
left join [Index] D on E.IndexPK = D.IndexPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1




Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER INDEX' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end
* case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,

0,F.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,
A.DoneAmount *  case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2  and date <= @Date
	) and status = 2 
	
)E on A.InstrumentPK = E.InstrumentPK
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)D on E.IndexPK = D.Parameter
left join [Index] F on F.IndexPK = E.IndexPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and H.Type = 1





Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER SECTOR' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on E.SubSectorPK = B.SectorPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)C on E.SectorPK = C.Parameter
left join Sector D on E.SectorPK = D.SectorPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER SECTOR' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end
* case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,

0,F.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,
A.DoneAmount *  case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on B.SectorPK = E.SubSectorPK and E.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)D on E.SectorPK = D.Parameter
left join Sector F on F.SectorPK = E.SectorPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and H.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
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
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type = 1

	
Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
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
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and E.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'EQUITY ALL' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1 and H.Type = 1


Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'EQUITY ALL' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.statusSettlement <> 3 and F.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER EQUITY' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1 


Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER EQUITY' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type= 1 and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 


Insert into #OMSEquityExposureTemp(AUMPrevDay,ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent
,WarningMaxExposurePercent,WarningMinExposurePercent,Balance,AvgPrice,LastPrice,Status,Amount)
select @TotalMarketValue,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent
, sum(balance)  Balance
,avg(AvgPrice),avg(LastPrice),Status,sum(MarketValue)
from #OMSEquityExposure
group by ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent,Status,AvgPrice


Select AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(Amount) MarketValue, sum(Amount)/ AUMPrevDay * 100 ExposurePercent,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSEquityExposureTemp A
Group by AUMPrevDay,ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by ExposureType      ";
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



Create table #OMSEquityExposure
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


Create table #OMSEquityExposureTemp
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





Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER INDEX' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2  and date <= @Date
	) and status = 2 
	
)E on A.InstrumentPK = E.InstrumentPK
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)C on E.IndexPK = C.Parameter
left join [Index] D on E.IndexPK = D.IndexPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1




Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER INDEX' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end
* case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,

0,F.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,
A.DoneAmount *  case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2  and date <= @Date
	) and status = 2 
	
)E on A.InstrumentPK = E.InstrumentPK
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)D on E.IndexPK = D.Parameter
left join [Index] F on F.IndexPK = E.IndexPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and H.Type = 1





Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER SECTOR' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, D.Name ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on E.SubSectorPK = B.SectorPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)C on E.SectorPK = C.Parameter
left join Sector D on E.SectorPK = D.SectorPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER SECTOR' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end
* case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,

0,F.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,
A.DoneAmount *  case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on B.SectorPK = E.SubSectorPK and E.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)D on E.SectorPK = D.Parameter
left join Sector F on F.SectorPK = E.SectorPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and H.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
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
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and D.Type = 1

	
Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
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
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 and E.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'EQUITY ALL' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1 and H.Type = 1


Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'EQUITY ALL' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.statusSettlement <> 3 and F.Type = 1



Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select A.FundID,'PER EQUITY' ExposureType,A.Balance,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)

,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent,0,0
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentPK = C.Parameter 
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1 


Insert into #OMSEquityExposure (FundID,ExposureType,Balance,AvgPrice,LastPrice,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,status,amount)
Select C.ID,'PER EQUITY' ExposureType,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end * case when A.TrxType = 1 then 1 else -1 end
,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK)
,case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End

,Case When A.DoneVolume > 0 then A.DoneVolume else A.DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End end *  case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End * case when A.TrxType = 1 then 1 else -1 end,
0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent,1,A.DoneAmount * case when A.TrxType = 1 then 1 else -1 end
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)D on B.InstrumentPK = D.Parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type= 1 and A.StatusDealing <> 3 and A.StatusInvestment <> 3 and A.StatusSettlement <> 3 


Insert into #OMSEquityExposureTemp(AUMPrevDay,ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent
,WarningMaxExposurePercent,WarningMinExposurePercent,Balance,AvgPrice,LastPrice,Status,Amount)
select @TotalMarketValue,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent
, sum(balance)  Balance
,avg(AvgPrice),avg(LastPrice),Status,sum(MarketValue)
from #OMSEquityExposure
group by ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,WarningMaxExposurePercent,WarningMinExposurePercent,Status,AvgPrice


Select AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(Amount) MarketValue, sum(Amount)/ AUMPrevDay * 100 ExposurePercent,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when sum(Balance * LastPrice)/ AUMPrevDay * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSEquityExposureTemp A
Group by AUMPrevDay,ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by ExposureType      ";
                        }

                        

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSExposureEquity M_model = new OMSExposureEquity();
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

        // NEW VERSION WITH ONLY INPUT AMOUNT
        public List<OMSEquityByInstrument> OMSEquityByInstrument(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquityByInstrument> L_model = new List<OMSEquityByInstrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where 	date < @Date  
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
               
						
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

Create table #OMSEquityByAllInstrument
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByAllInstrument ON #OMSEquityByAllInstrument (InstrumentID,SectorID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	

Create table #OMSEquityByAllInstrumentTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByAllInstrumentTemp ON #OMSEquityByAllInstrumentTemp (InstrumentID,SectorID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	



CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2

               


-- AMBIL DARI POSISI DITAMBAH MOVEMENT                        	
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,C.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
G.AvgPrice,
A.Balance * G.AvgPrice CostValue,
G.ClosePrice ClosePrice,    
case when G.MarketInfoPrice  = 0 then 
G.ClosePrice else G.MarketInfoPrice End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP G on A.Date = G.Date and A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
-- sisi buy dulu
left join (
		Select 
		sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
		,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
		and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and instrumentTypePK = 1
		and TrxType = 1 and FundPK = @FundPK
		group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
		Select 
sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
		,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
		and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and instrumentTypePK = 1
		and TrxType = 2 and FundPK = @FundPK
		group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK and A.status  = 2 and B.InstrumentTypePK = 1



Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByAllInstrument
)



-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
                        
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select B.ID,C.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice ClosePrice,    
case when D.MarketInfoPrice  = 0 then D.ClosePrice else D.MarketInfoPrice End LastPrice,B.LotInShare ,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 
and StatusSettlement <> 3 
and A.instrumentTypePK = 1
and A.FundPK = @FundPK 
and B.ID Not in 
(
select instrumentID From #OMSEquityByAllInstrument
)


Insert into #OMSEquityByAllInstrumentTemp(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,SectorID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
0 CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityByAllInstrument
Group by SectorID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID


Select InstrumentID,SectorID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityByAllInstrumentTemp
where Balance <> 0


                                                ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where 	date < @Date  
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
               
						
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

Create table #OMSEquityByAllInstrument
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByAllInstrument ON #OMSEquityByAllInstrument (InstrumentID,SectorID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	

Create table #OMSEquityByAllInstrumentTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByAllInstrumentTemp ON #OMSEquityByAllInstrumentTemp (InstrumentID,SectorID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	



CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2

               


-- AMBIL DARI POSISI DITAMBAH MOVEMENT                        	
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,C.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
G.AvgPrice,
A.Balance * G.AvgPrice CostValue,
G.ClosePrice ClosePrice,    
case when G.MarketInfoPrice  = 0 then 
G.ClosePrice else G.MarketInfoPrice End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP G on A.Date = G.Date and A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
-- sisi buy dulu
left join (
		Select 
		sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
		,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
		and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and instrumentTypePK = 1
		and TrxType = 1 and FundPK = @FundPK
		group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
		Select 
sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
		,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
		and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  and instrumentTypePK = 1
		and TrxType = 2 and FundPK = @FundPK
		group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK and A.status  = 2 and B.InstrumentTypePK = 1



Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByAllInstrument
)



-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
                        
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select B.ID,C.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice ClosePrice,    
case when D.MarketInfoPrice  = 0 then D.ClosePrice else D.MarketInfoPrice End LastPrice,B.LotInShare ,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 
and StatusSettlement <> 3 
and A.instrumentTypePK = 1
and A.FundPK = @FundPK 
and B.ID Not in 
(
select instrumentID From #OMSEquityByAllInstrument
)


Insert into #OMSEquityByAllInstrumentTemp(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,SectorID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
0 CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityByAllInstrument
Group by SectorID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID


Select InstrumentID,SectorID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityByAllInstrumentTemp
where Balance <> 0


                                                ";
                        }

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSEquityByInstrument M_model = new OMSEquityByInstrument();
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.SectorID = Convert.ToString(dr["SectorID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Lot = Convert.ToDecimal(dr["Lot"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    M_model.Status = Convert.ToInt32(dr["Status"]);
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

        public List<OMSEquityBySector> OMSEquityBySector(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquityBySector> L_model = new List<OMSEquityBySector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date < @Date   
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
                        

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

Create table #OMSEquityBySector
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)
		
CREATE CLUSTERED INDEX indx_OMSEquityBySector ON #OMSEquityBySector (InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);				 
		                 
	
Create table #OMSEquityBySectorTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)		                 

CREATE CLUSTERED INDEX indx_OMSEquityBySectorTemp ON #OMSEquityBySectorTemp (InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where A.TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2





	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,G.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
H.AvgPrice,
A.Balance * H.AvgPrice CostValue,
H.ClosePrice ClosePrice,    
case when H.MarketInfoPrice  = 0 
then H.ClosePrice 
else H.MarketInfoPrice End LastPrice, B.LotInShare,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP H on A.Date = H.Date and A.FundPK = H.FundPK and A.InstrumentPK = H.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
left join Sector G on C.SectorPK = G.SectorPK and G.status = 2
-- sisi buy dulu
left join (
	Select 
		sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
	,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 1 and FundPK = @FundPK
	group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
	Select 
	sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
	,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 2 and FundPK = @FundPK
	group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK
and A.status = 2
			


Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityBySector
)	




						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,F.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice ClosePrice,    
case when D.MarketInfoPrice  = 0 then D.ClosePrice else D.MarketInfoPrice End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
left join Sector F on C.SectorPK = F.SectorPK and F.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and A.FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityBySector
)						
						

Insert into #OMSEquityBySectorTemp(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,SectorID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
0 CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityBySector
Group by SectorID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID

Select InstrumentID,SectorID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityBySectorTemp
where Balance <> 0


                                                                          
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date < @Date   
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
                        

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

Create table #OMSEquityBySector
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)
		
CREATE CLUSTERED INDEX indx_OMSEquityBySector ON #OMSEquityBySector (InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);				 
		                 
	
Create table #OMSEquityBySectorTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)		                 

CREATE CLUSTERED INDEX indx_OMSEquityBySectorTemp ON #OMSEquityBySectorTemp (InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where A.TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2





	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,G.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
H.AvgPrice,
A.Balance * H.AvgPrice CostValue,
H.ClosePrice ClosePrice,    
case when H.MarketInfoPrice  = 0 
then H.ClosePrice 
else H.MarketInfoPrice End LastPrice, B.LotInShare,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP H on A.Date = H.Date and A.FundPK = H.FundPK and A.InstrumentPK = H.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
left join Sector G on C.SectorPK = G.SectorPK and G.status = 2
-- sisi buy dulu
left join (
	Select 
		sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
	,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 1 and FundPK = @FundPK
	group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
	Select 
	sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
	,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 2 and FundPK = @FundPK
	group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK
and A.status = 2
			


Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityBySector
)	




						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,F.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice ClosePrice,    
case when D.MarketInfoPrice  = 0 then D.ClosePrice else D.MarketInfoPrice End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
left join Sector F on C.SectorPK = F.SectorPK and F.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and A.FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityBySector
)						
						

Insert into #OMSEquityBySectorTemp(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,SectorID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
0 CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityBySector
Group by SectorID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID

Select InstrumentID,SectorID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityBySectorTemp
where Balance <> 0

                                                                          
                        ";
                        }

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSEquityBySector M_model = new OMSEquityBySector();
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.SectorID = Convert.ToString(dr["SectorID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Lot = Convert.ToDecimal(dr["Lot"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    M_model.Status = Convert.ToInt16(dr["Status"]);
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

        public decimal OMSEquityGetNetBuySell(DateTime _date, int _fundPK)
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

                        Select sum(DoneAmount * -1) from Investment where ValueDate = @Date and FundPK = @FundPK and StatusInvestment  <> 3 and StatusDealing  <> 3 and StatusSettlement  <> 3  and TrxType  = 1 and InstrumentTypePK = 1

                        Insert into #NetBuySell (Amount) 

                        Select sum(DoneAmount) from Investment where ValueDate = @Date and FundPK = @FundPK and StatusInvestment  <> 3 and StatusDealing  <> 3 and StatusSettlement  <> 3 and TrxType  = 2 and InstrumentTypePK = 1

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

        public decimal OMSEquityGetDealingNetBuySellEquity(DateTime _date, int _fundPK, int _counterpartPK)
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

                        Select sum(DoneAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @"  and statusInvestment  = 2 
                        and statusDealing  <> 3
                        and statusSettlement  <> 3 and TrxType  = 1 and InstrumentTypePK = 1 

                        Insert into #NetBuySell (Amount) 

                        Select DoneAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and statusInvestment  = 2 
                        and statusDealing  <> 3
                        and statusSettlement  <> 3 and TrxType  = 2 and InstrumentTypePK = 1

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

        public decimal OMSEquityGetSettlementNetBuySellEquity(DateTime _date, int _fundPK, int _counterpartPK)
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

                        Select sum(TotalAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2  and TrxType  = 1 and InstrumentTypePK = 1 

                        Insert into #NetBuySell (TotalAmount) 

                        Select TotalAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2 and TrxType  = 2 and InstrumentTypePK = 1

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

        public decimal OMSEquityGetSettlementNetBuySellBond(DateTime _date, int _fundPK, int _counterpartPK)
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

                        Select sum(TotalAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2 and TrxType  = 1 and InstrumentTypePK = 2 

                        Insert into #NetBuySell (TotalAmount) 

                        Select TotalAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2 and TrxType  = 2 and InstrumentTypePK = 2

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

        public decimal OMSEquityGetNetAvailableCash(DateTime _date, int _fundPK, int _paramDays)
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

--declare @date date
--declare @FundPK int

--set @date = '2020-09-23'
--set @FundPK = 9

--drop table #Result
--drop table #FinalResult
--drop table #ResultData

--Declare @ParamDays int

--set @ParamDays = 0 

DECLARE @DateMinOne DATETIME

set @DateMinOne = dbo.FWorkingDay(@Date,-1)

DECLARE @DateTo DATETIME

SET @DateTo = dateadd(day,@ParamDays,@Date)


Declare @BTotalDays int
Declare @BDateFrom datetime

select @BDateFrom = DateFrom from Period where @Date between DateFrom and DateTo and status = 2
select @BTotalDays = datediff(Day,@BDateFrom,dateadd(year,1,@BDateFrom))



CREATE TABLE #Result 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

CREATE CLUSTERED INDEX indx_Result ON #Result (Position,Name,Date);


CREATE TABlE #FinalResult 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4)
)

CREATE CLUSTERED INDEX indx_FinalResult  ON #FinalResult (Position,Name,Date,Balance);

CREATE TABle #ResultData
(

	Position INT,
	ValueDate DATETIME,
	TotalAmount NUMERIC(22,4),
)

CREATE CLUSTERED INDEX indx_ResultData  ON #ResultData (Position,ValueDate,TotalAmount);


DECLARE @CounterDate datetime
SET @CounterDate = @Date


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 2,'Placement Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 3,'Liquidate Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 4,'Time Deposit Mature',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 5,'AR Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 6,'Settle Buy Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 7,'Settle Sell Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 8,'Settle Buy Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 9,'Settle Sell Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 10,'Coupon Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 11,'Subscription',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 12,'Redemption',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 13,'Management Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 14,'Custodi Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 15,'Other Fee',@CounterDate

	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END



--INSERT INTO #FinalResult
--        ( Position, Name, Date, Balance, FundPK )

--SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance,@FundPK 
--FROM #Result A

--LEFT JOIN 
--(

--SELECT A.Position,A.ValueDate,SUM(ISNULL(A.totalAmount,0)) TotalAmount FROM
--	(
		insert into #ResultData
-- DEPOSITO BUY
		SELECT 2 Position,ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		insert into #ResultData
		
-- DEPOSITO BREAK
		SELECT 3 Position,ValueDate,isnull(TotalAmount,0) FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData
	
-- DEPOSITO MATURED
		SELECT 4 Position,MaturityDate ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
		AND InstrumentPK not in 
		(
			select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
		)

		insert into #ResultData

-- AR TIME DEPOSIT
		
		select 5 Position,case when IsHoliday = 1 then DT1 else Date end Date,isnull(sum(Amount),0) Amount
		From (
		-- PLACEMENT 
		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.MaturityDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.MaturityDate  and B.ValueDate between @date and @dateto --and B.Valuedate = @date
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.TrxType in (1,3)
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		and InstrumentPK not in
		(
			select InstrumentPK from Investment B where StatusSettlement = 2 and TrxType = 2 and B.MaturityDate <= A.Date  and FundPK = @FundPK
		)
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.MaturityDate

		UNION ALL

	

		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.BreakInterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.ValueDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.ValueDate
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.Category = 'Deposit Normal'
		AND B.TrxType = 2
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.ValueDate

	

		UNION ALL

		-- INTEREST TD MATURE NORMAL
		SELECT C.FundPK,IsHoliday,DT1,A.Date,'Bunga TD' Item
		,SUM(ISNULL(B.Balance * InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * 
		case when B.InterestPaymentType <> 7 then DATEDIFF(day,B.AcqDate,B.MaturityDate) 
				else DATEDIFF(day,dateadd(month,-1,A.Date),A.Date) end Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		left join Fund C on C.status = 2 
		LEFT JOIN 
		(
			SELECT A.Date,C.FundPK,A.MaturityDate,A.Balance,A.InterestPercent,A.InterestDaysType,A.AcqDate,A.InterestPaymentType FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			left join Fund C on C.status = 2 AND A.FundPK = C.FundPK
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition A
				left join Fund C on C.status = 2 WHERE A.fundPK = C.FundPK AND date < @Date AND A.status = 2  and A.FundPK = @FundPK
			) AND A.FundPK = C.FundPK
			AND B.InstrumentTypePK = 5  and A.FundPK = @FundPK
			AND B.InstrumentPK not in 
			(
				select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
			) 
	
		)B ON A.Date = case when B.InterestPaymentType <> 7 then  B.MaturityDate else  dateadd(month, month(B.Date) + 1 - month(B.MaturityDate), B.MaturityDate) end and B.FundPK = C.FundPK

		WHERE  A.Date BETWEEN @Date AND @Dateto  and C.FundPK = @FundPK
		GROUP BY C.FundPK,A.Date,B.AcqDate,B.MaturityDate,IsHoliday,DT1,B.InterestPaymentType


		


		) C
		left join Fund D on D.status = 2 and C.FundPK = D.FundPK 
		GROUP BY Date,Item,IsHoliday,DT1



		insert into #ResultData

-- EQUITY BUY
		SELECT 6 Position,ValueDate ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusInvestment <> 3
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- EQUITY SELL
		SELECT 7 Position,SettlementDate ValueDate,isnull(TotalAmount,0)  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- BOND BUY
		SELECT 8 Position,ValueDate ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusInvestment <> 3
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- BOND SELL
		SELECT 9 Position,SettlementDate ValueDate,isnull(TotalAmount,0)  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- COUPON BOND

		select 10 Position,Z.Date,isnull(sum(Z.GrossAmount - Z.TaxAmount),0) Amount from 
		(
		SELECT case when A.IsHoliday = 1 then A.DT1 else A.Date end Date,'Coupon Obligasi' Item,
		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
				case when B.AcqDate <= B.MaturityDate then datediff(day,B.LastCouponDate,B.MaturityDate) else 
					datediff(day,B.LastCouponDate,B.MaturityDate) end end),0) 
		else
		isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
		* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
			case when B.AcqDate <= B.NextCouponDate then datediff(day,B.LastCouponDate,B.NextCouponDate) else 
				datediff(day,B.LastCouponDate,B.NextCouponDate) end end),0)  
		end GrossAmount,

		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.MaturityDate then (B.InterestDaysType/B.InterestPaymentType) + datediff(day,B.NextCouponDate,B.MaturityDate) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.MaturityDate) end),0) * 0.05
		else 
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.LastCouponDate then (B.InterestDaysType/B.InterestPaymentType) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.NextCouponDate) end),0) * 0.05
	
		end	TaxAmount
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN 
		(
			SELECT A.Date,A.FundPK,A.InstrumentPK,dbo.FgetLastCouponDate(A.Date,A.InstrumentPK) LastCouponDate,dbo.Fgetnextcoupondate(A.Date,A.InstrumentPK) NextCouponDate,A.Balance,A.AcqDate,
			case when A.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when A.InterestPaymentType in (1,4,7) then 12 
						when A.InterestPaymentType in (13) then 4 
							when A.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,A.MaturityDate FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
			) AND FundPK = @FundPK
			AND B.InstrumentTypePK not in (1,4,5,6,16)

			---- Terima Kupon saat ada jual habis tapi SettleDate = CouponDate
			union all
			select SettlementDate,A.FundPK,A.InstrumentPK,dbo.FgetLastcoupondate(ValueDate,A.InstrumentPK),dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK),DoneVolume,AcqDate,
			case when B.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when B.InterestPaymentType in (1,4,7) then 12 
						when B.InterestPaymentType in (13) then 4 
							when B.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,B.MaturityDate
			from Investment A
			left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
			where TrxType = 2 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.InstrumentTypePK  not in (1,4,5,6,16)
			and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) > dbo.FWorkingDay(SettlementDate,-1) and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) <= SettlementDate
			and SettlementDate between @Date and @dateto and FundPK = @FundPK
			and A.InstrumentPK not in 
			(
				SELECT A.InstrumentPK FROM dbo.FundPosition A
				LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
				WHERE A.status = 2 AND Date =
				(
					SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
				) AND FundPK = @FundPK
				AND B.InstrumentTypePK not in (1,4,5,6,16)
			)
	
		)B ON A.Date = case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end and B.AcqDate < case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end

		WHERE  A.Date BETWEEN case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND @Dateto
		GROUP BY A.Date,B.AcqDate,B.NextCouponDate,B.LastCouponDate,IsHoliday,DT1,B.MaturityDate,B.FundPK
		)Z
		where Z.Date between @Date and @dateto
		group by Z.Date

		insert into #ResultData

-- Subscription
		SELECT 11 Position,A.ValueDate,isnull(CashAmount,0) TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- Redemption
		SELECT 12 Position,A.PaymentDate,isnull(CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END,0) TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK and TotalCashAmount>0

		insert into #ResultData

-- Management Fee
		SELECT 13 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 79

		insert into #ResultData

-- Custodi Fee
		SELECT 14 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 80

		insert into #ResultData

-- Other Fee
		SELECT 15 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 103

--	)A
--	GROUP BY A.Position,A.ValueDate
--)B ON A.Date = B.ValueDate AND A.Position = B.Position


--select * from #FinalResult



INSERT INTO #FinalResult
        ( Position, Name, Date, Balance )


SELECT 1,'Saldo Last Day',@Date
,dbo.[FgetAccountBalanceByDateByParentByFundPK](DTM1,2,@FundPK) Amount

FROM dbo.ZDT_WorkingDays
WHERE Date = @Date

INSERT INTO #FinalResult
        ( Position, Name, Date, Balance )

SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance 
FROM #Result A
LEFT JOIN 
(
select Position,ValueDate,SUM(ISNULL(TotalAmount,0)) TotalAmount from #ResultData
group By Position,ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


select sum(Balance) Amount from #FinalResult



                         ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@ParamDays", _paramDays);

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




        public List<OMSEquityCashProjection> OMSEquityGetNetAvailableCashDetail(DateTime _date, int _fundPK, int _paramDays)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquityCashProjection> L_model = new List<OMSEquityCashProjection>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

--declare @date date
--declare @FundPK int
--declare @paramdays int

--set @date = '2020-09-23'
--set @paramdays = 7
--set @FundPK = 9


--drop table #Result
--drop table #FinalResult
--drop table #ResultWithTDays


DECLARE @DateMinOne DATETIME

set @DateMinOne = dbo.FWorkingDay(@Date,-1)

DECLARE @DateTo DATETIME

--SET @DateTo = dateadd(day,@ParamDays,@Date)

SET @DateTo = dbo.FWorkingDay(@Date,@Paramdays)

Declare @BTotalDays int
Declare @BDateFrom datetime

select @BDateFrom = DateFrom from Period where @Date between DateFrom and DateTo and status = 2
select @BTotalDays = datediff(Day,@BDateFrom,dateadd(year,1,@BDateFrom))




CREATE TABLE #Result 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

CREATE CLUSTERED INDEX indx_Result ON #Result (Position,Name,Date);


CREATE TABlE #FinalResult 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4),
	FundPK int
)

CREATE CLUSTERED INDEX indx_FinalResult  ON #FinalResult (Position,Name,Date,Balance,FundPK);



CREATE TABle #ResultWithTDays
(
    Flag NVARCHAR(50),
	TDays NVARCHAR(50),
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4),
	FundPK INT,
)

CREATE CLUSTERED INDEX indx_ResultWithTDays  ON #ResultWithTDays (Flag,TDays,Position,Name,Date,Balance,FundPK);



DECLARE @CounterDate datetime
SET @CounterDate = @Date


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 2,'Placement Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 3,'Liquidate Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 4,'Time Deposit Mature',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 5,'AR Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 6,'Settle Buy Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 7,'Settle Sell Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 8,'Settle Buy Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 9,'Settle Sell Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 10,'Coupon Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 11,'Subscription',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 12,'Redemption',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 13,'Management Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 14,'Custodi Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 15,'Other Fee',@CounterDate

	--SET @CounterDate = DATEADD(DAY,1,@CounterDate)
	SET @CounterDate = dbo.FWorkingDay(@CounterDate,1) 
END





INSERT INTO #FinalResult
        ( Position,Name,Date,Balance,FundPK )
SELECT 1,'Saldo Last Day',case when Date < @date then @date else DATEADD(day,1,date) end
,case when IsHoliday = 1 and Date < @date then 0 when Date = DATEADD(day,-1,date) then dbo.[FgetAccountBalanceByDateByParentByFundPK](DATEADD(day,-1,date),2,@FundPK)
	else dbo.FGetAvailableCashByDateByFundPK(@date,@FundPK,DATEDIFF(day,@date,Date)) end Amount
,@FundPK
FROM dbo.ZDT_WorkingDays
WHERE Date BETWEEN @DateMinOne AND DATEADD(day,-1,@DateTo)
group by IsHoliday,Date,DT1
--having case when IsHoliday = 1 and Date < @date then 0 when Date = @date then dbo.[FGetAccountFundJournalBalanceByFundPK](DATEADD(day,-1,date),3,@FundPK)
--	else dbo.FGetAvailableCashByDateByFundPK(@date,@FundPK,DATEDIFF(day,@date,Date)) end > 0
order by Date asc





INSERT INTO #FinalResult
        ( Position, Name, Date, Balance, FundPK )

SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance,@FundPK 
FROM #Result A

LEFT JOIN 
(

SELECT A.Position,A.ValueDate,SUM(ISNULL(A.totalAmount,0)) TotalAmount FROM
	(
		
		
-- DEPOSITO BUY
		SELECT 2 Position,ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL
		
-- DEPOSITO BREAK
		SELECT 3 Position,ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL
	
-- DEPOSITO MATURED
		SELECT 4 Position,MaturityDate ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
		AND InstrumentPK not in 
		(
			select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
		)

		UNION ALL


-- AR TIME DEPOSIT
		
		select 5 Position,case when IsHoliday = 1 then DT1 else Date end Date,isnull(sum(Amount),0) Amount
		From (
			-- PLACEMENT 
		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.MaturityDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.MaturityDate  and B.ValueDate between @date and @dateto --and B.Valuedate = @date
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.TrxType in (1,3)
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		and InstrumentPK not in
		(
			select InstrumentPK from Investment B where StatusSettlement = 2 and TrxType = 2 and B.MaturityDate <= A.Date  and FundPK = @FundPK
		)
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.MaturityDate

		UNION ALL

	

		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.BreakInterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.ValueDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.ValueDate
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.Category = 'Deposit Normal'
		AND B.TrxType = 2
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.ValueDate

	

		UNION ALL

		-- INTEREST TD MATURE NORMAL
		SELECT C.FundPK,IsHoliday,DT1,A.Date,'Bunga TD' Item
		,SUM(ISNULL(B.Balance * InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * 
		case when B.InterestPaymentType <> 7 then DATEDIFF(day,B.AcqDate,B.MaturityDate) 
				else DATEDIFF(day,dateadd(month,-1,A.Date),A.Date) end Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		left join Fund C on C.status = 2 
		LEFT JOIN 
		(
			SELECT A.Date,C.FundPK,A.MaturityDate,A.Balance,A.InterestPercent,A.InterestDaysType,A.AcqDate,A.InterestPaymentType FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			left join Fund C on C.status = 2 AND A.FundPK = C.FundPK
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition A
				left join Fund C on C.status = 2 WHERE A.fundPK = C.FundPK AND date < @Date AND A.status = 2  and A.FundPK = @FundPK
			) AND A.FundPK = C.FundPK
			AND B.InstrumentTypePK = 5  and A.FundPK = @FundPK
			AND B.InstrumentPK not in 
			(
				select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
			) 
	
		)B ON A.Date = case when B.InterestPaymentType <> 7 then  B.MaturityDate else  dateadd(month, month(B.Date) + 1 - month(B.MaturityDate), B.MaturityDate) end and B.FundPK = C.FundPK

		WHERE  A.Date BETWEEN @Date AND @Dateto  and C.FundPK = @FundPK
		GROUP BY C.FundPK,A.Date,B.AcqDate,B.MaturityDate,IsHoliday,DT1,B.InterestPaymentType


		


		) C
		left join Fund D on D.status = 2 and C.FundPK = D.FundPK 
		GROUP BY Date,Item,IsHoliday,DT1


		--UNION ALL
	
	

---- EQUITY BUY
		--SELECT 6 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		--WHERE StatusSettlement = 2
		--AND InstrumentTypePK IN(1,4,16)
		--AND TrxType = 1
		--AND SettlementDate BETWEEN @Date AND @DateTo
		--AND FundPK = @FundPK

		UNION ALL


-- EQUITY BUY
		SELECT 6 Position,ValueDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusInvestment  <> 3
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY SELL
		SELECT 7 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

	
		--UNION ALL

---- BOND BUY
		--SELECT 8 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		--WHERE StatusSettlement = 2
		--AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		--AND TrxType = 1
		--AND SettlementDate BETWEEN @Date AND @DateTo
		--AND FundPK = @FundPK

		UNION ALL


-- BOND BUY
		SELECT 8 Position,ValueDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusInvestment  <> 3
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL


-- BOND SELL
		SELECT 9 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- COUPON BOND
        select 10 Position,Z.Date,sum(Z.GrossAmount - Z.TaxAmount) Amount from 
		(
		SELECT case when A.IsHoliday = 1 then A.DT1 else A.Date end Date,'Coupon Obligasi' Item,
		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
				case when B.AcqDate <= B.MaturityDate then datediff(day,B.LastCouponDate,B.MaturityDate) else 
					datediff(day,B.LastCouponDate,B.MaturityDate) end end),0) 
		else
		isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
		* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
			case when B.AcqDate <= B.NextCouponDate then datediff(day,B.LastCouponDate,B.NextCouponDate) else 
				datediff(day,B.LastCouponDate,B.NextCouponDate) end end),0)  
		end GrossAmount,

		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.MaturityDate then (B.InterestDaysType/B.InterestPaymentType) + datediff(day,B.NextCouponDate,B.MaturityDate) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.MaturityDate) end),0) * 0.05
		else 
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.LastCouponDate then (B.InterestDaysType/B.InterestPaymentType) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.NextCouponDate) end),0) * 0.05
	
		end	TaxAmount
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN 
		(
			SELECT A.Date,A.FundPK,A.InstrumentPK,dbo.FgetLastCouponDate(A.Date,A.InstrumentPK) LastCouponDate,dbo.Fgetnextcoupondate(A.Date,A.InstrumentPK) NextCouponDate,A.Balance,A.AcqDate,
			case when A.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when A.InterestPaymentType in (1,4,7) then 12 
						when A.InterestPaymentType in (13) then 4 
							when A.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,A.MaturityDate FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
			) AND FundPK = @FundPK
			AND B.InstrumentTypePK not in (1,4,5,6,16)

			---- Terima Kupon saat ada jual habis tapi SettleDate = CouponDate
			union all
			select SettlementDate,A.FundPK,A.InstrumentPK,dbo.FgetLastcoupondate(ValueDate,A.InstrumentPK),dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK),DoneVolume,AcqDate,
			case when B.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when B.InterestPaymentType in (1,4,7) then 12 
						when B.InterestPaymentType in (13) then 4 
							when B.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,B.MaturityDate
			from Investment A
			left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
			where TrxType = 2 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.InstrumentTypePK  not in (1,4,5,6,16)
			and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) > dbo.FWorkingDay(SettlementDate,-1) and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) <= SettlementDate
			and SettlementDate between @Date and @dateto and FundPK = @FundPK
			and A.InstrumentPK not in 
			(
				SELECT A.InstrumentPK FROM dbo.FundPosition A
				LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
				WHERE A.status = 2 AND Date =
				(
					SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
				) AND FundPK = @FundPK
				AND B.InstrumentTypePK not in (1,4,5,6,16)
			)
	
		)B ON A.Date = case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end and B.AcqDate < case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end

		WHERE  A.Date BETWEEN case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND @Dateto
		GROUP BY A.Date,B.AcqDate,B.NextCouponDate,B.LastCouponDate,IsHoliday,DT1,B.MaturityDate,B.FundPK
		)Z
		where Z.Date between @Date and @dateto
		group by Z.Date

		UNION ALL

-- Subscription
		SELECT 11 Position,A.ValueDate,CashAmount TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Redemption
		SELECT 12 Position,A.PaymentDate,CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK and TotalCashAmount>0

		UNION ALL

-- Management Fee
		SELECT 13 Position,@Date,sum(BaseDebit-BaseCredit) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 79

		UNION ALL

-- Custodi Fee
		SELECT 14 Position,@Date,sum(BaseDebit-BaseCredit) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 80

		UNION ALL

-- Other Fee
		SELECT 15 Position,@Date,sum(BaseDebit-BaseCredit) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 103

	)A
	GROUP BY A.Position,A.ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position



delete #FinalResult where  dbo.CheckTodayIsHoliday(date) = 1



insert into #ResultWithTDays
select '','T' + cast(dbo.FGetTotalDiffDayWorkDayOnly(dateadd(day,1,@date),date) as nvarchar(50)) TDays,A.* from #FinalResult A


Declare @TDays nvarchar(50)
select @TDays = TDays from #ResultWithTDays
order by Date asc

update #ResultWithTDays set Flag = @TDays


DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + TDays +',0) ' + QUOTENAME(TDays ) 
                    from (SELECT DISTINCT TDays FROM #ResultWithTDays) A
					order by A.TDays
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(TDays) 
                    from #ResultWithTDays
				
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'SELECT Flag,Name Keterangan,' + @colsForQuery + ' from 
                                (
                                SELECT Flag,Position,Name,Balance,FundPK,TDays FROM #ResultWithTDays 
                            ) x 
                            pivot 
                            (
                                SUM(Balance)
                                for TDays  in (' + @cols + ')
                            ) p 
			                order by Position asc
			                '
                exec(@query)	

                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@ParamDays", _paramDays);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSEquityCashProjection M_model = new OMSEquityCashProjection();
                                    M_model.Flag = Convert.ToString(dr["Flag"]);
                                    M_model.FSource = Convert.ToString(dr["Keterangan"]);
                                    M_model.T0 = Convert.ToDecimal(dr["T0"]);
                                    if (_host.CheckColumnIsExist(dr, "T1"))
                                    {
                                        M_model.T1 = Convert.ToDecimal(dr["T1"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T2"))
                                    {
                                        M_model.T2 = Convert.ToDecimal(dr["T2"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T3"))
                                    {
                                        M_model.T3 = Convert.ToDecimal(dr["T3"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T4"))
                                    {
                                        M_model.T4 = Convert.ToDecimal(dr["T4"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T5"))
                                    {
                                        M_model.T5 = Convert.ToDecimal(dr["T5"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T6"))
                                    {
                                        M_model.T6 = Convert.ToDecimal(dr["T6"]);
                                    }
                                    if (_host.CheckColumnIsExist(dr, "T7"))
                                    {
                                        M_model.T7 = Convert.ToDecimal(dr["T7"]);
                                    }
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



        public decimal OMSEquityGetAUMYesterday(DateTime _date, int _fundPK)
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
                        declare @DateminOne datetime
                        set @DateminOne = dbo.FWorkingDay(@Date,-1)

                        if @FundPK = 0 
                        BEGIN
                            Select sum(AUM) AUM from CloseNAV where date = (select max(date) 
                            from CloseNAV where date <= @DateminOne
                            and status = 2
                            )
                            and status = 2
   
                        END
                        ELSE
                        BEGIN
                     
                            Select AUM from CloseNAV where date = (select max(date) 
                            from CloseNAV where date <= @DateminOne
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

        public List<OMSEquityByIndex> OMSEquityByIndex(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquityByIndex> L_model = new List<OMSEquityByIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
  

Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date < @Date  
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
                        
						
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


Create table #OMSEquityByIndex
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
IndexID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByIndex ON #OMSEquityByIndex (InstrumentID,IndexID,Balance,AvgPrice,CostValue,
ClosePrice,LastPrice,LotInShare,Status,Amount);	
						 
Create table #OMSEquityByIndexTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
IndexID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)	

CREATE CLUSTERED INDEX indx_OMSEquityByIndexTemp ON #OMSEquityByIndexTemp (InstrumentID,IndexID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);

Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2

               
	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,
C1.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
G.AvgPrice,
A.Balance * G.AvgPrice CostValue,
G.ClosePrice,    
case when G.MarketInfoPrice  = 0 
then G.ClosePrice 
else G.MarketInfoPrice End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP G on A.Date = G.Date and A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join InstrumentIndex C on B.InstrumentPK = C.InstrumentPK and C.Status = 2
left join [Index] C1 on C.IndexPK = C1.IndexPK and C1.Status = 2
-- sisi buy dulu
left join (
	Select 
	sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
	,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 1 and FundPK = @FundPK
	group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
	Select 
	sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
	,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 2 and FundPK = @FundPK
	group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK
and A.status = 2

			


Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByIndex
)


				
						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,C1.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice,    
case when D.MarketInfoPrice = 0 then D.ClosePrice 
else D.MarketInfoPrice End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1   end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join InstrumentIndex C on B.InstrumentPK = C.InstrumentPK and C.Status = 2
left join [Index] C1 on C.IndexPK = C1.IndexPK and C1.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and A.FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByIndex
)




Insert into #OMSEquityByIndexTemp(InstrumentID,IndexID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,IndexID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
sum(case when Balance <> 0  then Balance else Amount end * case when AvgPrice = 0 and Balance = 0 then 1 else AvgPrice End) CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityByIndex
Group by IndexID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID



Select InstrumentID,isnull(IndexID,'NoIndex') IndexID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityByIndexTemp
where Balance <> 0
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
  

Declare @TotalMarketValue numeric(26,6)
                        
select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date < @Date  
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2             
                        
						
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


Create table #OMSEquityByIndex
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
IndexID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)

CREATE CLUSTERED INDEX indx_OMSEquityByIndex ON #OMSEquityByIndex (InstrumentID,IndexID,Balance,AvgPrice,CostValue,
ClosePrice,LastPrice,LotInShare,Status,Amount);	
						 
Create table #OMSEquityByIndexTemp
(
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
IndexID nvarchar(200) COLLATE DATABASE_DEFAULT,
Balance numeric(22,4),
AvgPrice numeric(18,6),
CostValue numeric(22,4),
ClosePrice numeric(18,4),
LastPrice numeric(18,4),
LotInShare numeric(18,4),
Status int,
Amount numeric(22,4)
)	

CREATE CLUSTERED INDEX indx_OMSEquityByIndexTemp ON #OMSEquityByIndexTemp (InstrumentID,IndexID,Balance,AvgPrice,
CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount);	


CREATE TABLE #LastAVgFromFP
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromFP ON #LastAVgFromFP (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);


CREATE TABLE #LastAVgFromInvestment
(
Date datetime,
InstrumentPK int,
FundPK int,
AvgPrice numeric(22,12),
MarketInfoPrice numeric(22,12),
ClosePrice numeric(22,12)
)

CREATE CLUSTERED INDEX indx_LastAVgFromInvestment ON #LastAVgFromInvestment (Date,InstrumentPK,FundPK,AvgPrice,MarketInfoPrice,ClosePrice);

Insert into #LastAVgFromFP
select Date,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where TrailsPK = @TrailsPK and C.Type = 1 and A.FundPK = @FundPK and A.status = 2

               
	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,
C1.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
G.AvgPrice,
A.Balance * G.AvgPrice CostValue,
G.ClosePrice,    
case when G.MarketInfoPrice  = 0 
then G.ClosePrice 
else G.MarketInfoPrice End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
left join #LastAVgFromFP G on A.Date = G.Date and A.FundPK = G.FundPK and A.InstrumentPK = G.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join InstrumentIndex C on B.InstrumentPK = C.InstrumentPK and C.Status = 2
left join [Index] C1 on C.IndexPK = C1.IndexPK and C1.Status = 2
-- sisi buy dulu
left join (
	Select 
	sum(isnull(case when D.DoneVolume <> 0 then D.DoneVolume else D.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) Volume
	,InstrumentPK from Investment D where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 1 and FundPK = @FundPK
	group by InstrumentPK
)D on A.InstrumentPK = D.InstrumentPK
-- sisi Sell
left join (
	Select 
	sum(isnull(case when E.DoneVolume <> 0 then E.DoneVolume else E.DoneAmount/ case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,InstrumentPK) End end,0)) * -1 Volume
	,InstrumentPK from Investment E where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and instrumentTypePK = 1
	and TrxType = 2 and FundPK = @FundPK
	group by InstrumentPK
)E on A.InstrumentPK = E.InstrumentPK
left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK
and A.status = 2

			


Insert into #LastAVgFromInvestment
select ValueDate,A.InstrumentPK,FundPK,dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,FundPK),
dbo.FgetPriceFromInstrumentMarketInfo(@Date,A.InstrumentPK),dbo.FGetLastClosePrice(@Date,A.InstrumentPK)  from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByIndex
)


				
						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,C1.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
D.AvgPrice,
D.ClosePrice,    
case when D.MarketInfoPrice = 0 then D.ClosePrice 
else D.MarketInfoPrice End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1   end
from Investment A
left join #LastAVgFromInvestment D on A.ValueDate = D.Date and A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join InstrumentIndex C on B.InstrumentPK = C.InstrumentPK and C.Status = 2
left join [Index] C1 on C.IndexPK = C1.IndexPK and C1.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and A.FundPK = @FundPK
and B.ID Not in 
(
select instrumentID From #OMSEquityByIndex
)




Insert into #OMSEquityByIndexTemp(InstrumentID,IndexID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
select InstrumentID,IndexID
,sum(case when Balance <> 0 then Balance else Amount / lastPrice  End) Balance
,avg(case when AvgPrice = 0 then 0 else AvgPrice End) AvgPrice,
sum(case when Balance <> 0  then Balance else Amount end * case when AvgPrice = 0 and Balance = 0 then 1 else AvgPrice End) CostValue
,ClosePrice,LastPrice,LotInShare,status,sum(amount) Amount from #OMSEquityByIndex
Group by IndexID,InstrumentID,Status,ClosePrice,LastPrice,LotInShare
order by InstrumentID



Select InstrumentID,isnull(IndexID,'NoIndex') IndexID,(Balance * LastPrice) / @TotalMarketValue * 100 CurrentExposure 
,Balance/lotInShare Lot,AvgPrice,balance * AvgPrice Cost,ClosePrice,LastPrice
,Case when lastPrice/ClosePrice = 1 then 0 else LastPrice/ClosePrice * 100 end PriceDifference
,Balance * LastPrice MarketValue
,(Balance * LastPrice) - (balance * AvgPrice) Unrealized
, ((Balance * LastPrice / (balance * AvgPrice)) - 1) * 100   GainLoss
,Status
from #OMSEquityByIndexTemp
where Balance <> 0
                        ";
                        }


                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSEquityByIndex M_model = new OMSEquityByIndex();
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.IndexID = Convert.ToString(dr["IndexID"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Lot = Convert.ToDecimal(dr["Lot"]);
                                    M_model.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                    M_model.Cost = Convert.ToDecimal(dr["Cost"]);
                                    M_model.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
                                    M_model.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
                                    M_model.PriceDifference = Convert.ToDecimal(dr["PriceDifference"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.UnRealized = Convert.ToDecimal(dr["UnRealized"]);
                                    M_model.GainLoss = Convert.ToDecimal(dr["GainLoss"]);
                                    M_model.Status = Convert.ToInt16(dr["Status"]);
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

        public int Validate_ApproveBySelectedDataOMSEquity(Investment _investment)
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

        public int Validate_RejectBySelectedDataOMSEquity(Investment _investment)
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

        public int Investment_ApproveOMSEquityBySelected(Investment _investment)
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
                            _paramFund = "And A.FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        --declare @RegDays int
                        --select @RegDays = RegDays from SettlementSetup where status = 2


                        declare @investmentPK int
                        declare @historyPK int
                        declare @DealingPK int
                        declare @Notes nvarchar(500)
                        declare @OrderPrice numeric(22,4)
                        declare @Lot numeric(22,4)
                        declare @Amount numeric(22,4)
                        declare @AccruedInterest numeric(22,0)
                        declare @ParamRegDays int

                        Declare @RegDays table
                        (
	                        FundPK int,
	                        RegDays int
                        ) 
                        insert into @RegDays
                        select A.FundPK,isnull(B.RegDays,C.RegDays) from Fund A
                        left join SettlementSetup B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join SettlementSetup C on C.FundPK = 0 and C.status in (1,2)
                        where A.status in (1,2)

                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = '' and Status = 2   
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)   
                        Select @Time,'InvestmentInstruction_RejectOMSEquityBySelected','Investment',InvestmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Investment A where ValueDate between @DateFrom and @DateTo and statusInvestment = 1 " + _paramInvestmentPK + "  and  InstrumentTypePK in (1,4,16) and TrxType = @TrxType " + _paramFund +

                        @"DECLARE A CURSOR FOR 
	                            Select InvestmentPK,DealingPK,HistoryPK,InvestmentNotes,OrderPrice,Lot,Amount,AccruedInterest,B.RegDays From investment A
                                left join @RegDays B on A.FundPK =  B.FundPK
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom " + _paramInvestmentPK + " and  InstrumentTypePK in (1,4,16) and TrxType = @TrxType " + _paramFund +

                        @"Open A
                        Fetch Next From A
                        Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Lot,@Amount,@AccruedInterest,@ParamRegDays

                        While @@FETCH_STATUS = 0
                        BEGIN
                        Select @DealingPK = max(DealingPK) + 1 From investment
                        if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                        update Investment set DealingPK = @DealingPK, statusInvestment = 2, statusDealing = 1,InvestmentNotes=@Notes,DonePrice=@OrderPrice,DoneLot=@Lot,DoneVolume=@Lot*100,DoneAmount=@Amount,BoardType = 1,SettlementDate = dbo.FWorkingDay(@DateFrom,@ParamRegDays) ,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,EntryDealingID = @ApprovedUsersID,EntryDealingTime = @ApprovedTime ,LastUpdate=@LastUpdate
                        where InvestmentPK = @InvestmentPK
                        Fetch next From A Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Lot,@Amount,@AccruedInterest,@ParamRegDays
                        END
                        Close A
                        Deallocate A 

                        --Update Investment set SelectedInvestment  = 0";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }

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

        public int Investment_RejectOMSEquityBySelected(Investment _investment)
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
                        cmd.CommandText = @"Update Investment set StatusInvestment  = 3,statusDealing = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime
                            where InstrumentTypePK in (1,4,16) and TrxType = @TrxType " + _paramInvestmentPK +" and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) " + _paramFund +
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

        public Boolean Validate_CheckAvailableCash(decimal _amount, DateTime _valueDate, int _fundPK)
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
                            
                        --declare @Date date
--declare @FundPK int
--declare @Amount numeric(22,8)


--set @Date = '2020-06-30'
--set @FundPK = 42
--set @Amount = 15676500000

--drop table #FinalResult
--drop table #ResultData
--drop table #Result


Declare @ParamDays int

set @ParamDays = 0 

DECLARE @DateMinOne DATETIME

set @DateMinOne = dbo.FWorkingDay(@Date,-1)

DECLARE @DateTo DATETIME

SET @DateTo = dateadd(day,@ParamDays,@Date)

declare @ValidateAmount numeric(32,4)


Declare @BTotalDays int
Declare @BDateFrom datetime

select @BDateFrom = DateFrom from Period where @Date between DateFrom and DateTo and status = 2
select @BTotalDays = datediff(Day,@BDateFrom,dateadd(year,1,@BDateFrom))



CREATE TABLE #Result 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

CREATE CLUSTERED INDEX indx_Result ON #Result (Position,Name,Date);


CREATE TABlE #FinalResult 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4)
)

CREATE CLUSTERED INDEX indx_FinalResult  ON #FinalResult (Position,Name,Date,Balance);

CREATE TABle #ResultData
(

	Position INT,
	ValueDate DATETIME,
	TotalAmount NUMERIC(22,4),
)

CREATE CLUSTERED INDEX indx_ResultData  ON #ResultData (Position,ValueDate,TotalAmount);


DECLARE @CounterDate datetime
SET @CounterDate = @Date


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 2,'Placement Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 3,'Liquidate Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 4,'Time Deposit Mature',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 5,'AR Time Deposit',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 6,'Settle Buy Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 7,'Settle Sell Equity',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 8,'Settle Buy Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 9,'Settle Sell Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 10,'Coupon Bond',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 11,'Subscription',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 12,'Redemption',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 13,'Management Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 14,'Custodi Fee',@CounterDate

	INSERT INTO #Result
        ( Position, Name, Date )
	SELECT 15,'Other Fee',@CounterDate

	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END



--INSERT INTO #FinalResult
--        ( Position, Name, Date, Balance, FundPK )

--SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance,@FundPK 
--FROM #Result A

--LEFT JOIN 
--(

--SELECT A.Position,A.ValueDate,SUM(ISNULL(A.totalAmount,0)) TotalAmount FROM
--	(
		insert into #ResultData
-- DEPOSITO BUY
		SELECT 2 Position,ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		insert into #ResultData
		
-- DEPOSITO BREAK
		SELECT 3 Position,ValueDate,isnull(TotalAmount,0) FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData
	
-- DEPOSITO MATURED
		SELECT 4 Position,MaturityDate ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
		AND InstrumentPK not in 
		(
			select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
		)

		insert into #ResultData

-- AR TIME DEPOSIT
		
		select 5 Position,case when IsHoliday = 1 then DT1 else Date end Date,isnull(sum(Amount),0) Amount
		From (
		-- PLACEMENT 
		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.MaturityDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.MaturityDate  and B.ValueDate between @date and @dateto --and B.Valuedate = @date
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.TrxType in (1,3)
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		and InstrumentPK not in
		(
			select InstrumentPK from Investment B where StatusSettlement = 2 and TrxType = 2 and B.MaturityDate <= A.Date  and FundPK = @FundPK
		)
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.MaturityDate

		UNION ALL

	

		SELECT B.FundPK,IsHoliday,DT1,Date,'Bunga TD' Item
		,SUM(ISNULL(B.DoneVolume * B.BreakInterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * datediff(day,B.AcqDate,B.ValueDate) Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN dbo.Investment B ON A.Date = B.ValueDate
		AND B.StatusInvestment <> 3 and B.StatusDealing <> 3 and B.StatusSettlement <> 3 
		AND B.InstrumentTypePK = 5
		AND B.Category = 'Deposit Normal'
		AND B.TrxType = 2
		AND B.FundPK = @FundPK
		WHERE Date BETWEEN @date AND @Dateto
		GROUP BY B.FundPK,A.Date,IsHoliday,DT1,B.AcqDate,B.ValueDate

	

		UNION ALL

		-- INTEREST TD MATURE NORMAL
		SELECT C.FundPK,IsHoliday,DT1,A.Date,'Bunga TD' Item
		,SUM(ISNULL(B.Balance * InterestPercent / 100 /
		CASE WHEN B.InterestDaysType = 4 then 365 when B.InterestDaysType = 2 then @BTotalDays ELSE 360 END,0)) * 0.8 * 
		case when B.InterestPaymentType <> 7 then DATEDIFF(day,B.AcqDate,B.MaturityDate) 
				else DATEDIFF(day,dateadd(month,-1,A.Date),A.Date) end Amount
		,5 Baris
		FROM dbo.ZDT_WorkingDays A
		left join Fund C on C.status = 2 
		LEFT JOIN 
		(
			SELECT A.Date,C.FundPK,A.MaturityDate,A.Balance,A.InterestPercent,A.InterestDaysType,A.AcqDate,A.InterestPaymentType FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			left join Fund C on C.status = 2 AND A.FundPK = C.FundPK
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition A
				left join Fund C on C.status = 2 WHERE A.fundPK = C.FundPK AND date < @Date AND A.status = 2  and A.FundPK = @FundPK
			) AND A.FundPK = C.FundPK
			AND B.InstrumentTypePK = 5  and A.FundPK = @FundPK
			AND B.InstrumentPK not in 
			(
				select InstrumentPK from Investment where StatusSettlement = 2 and TrxType = 2 and MaturityDate between @Date and @DateTo and FundPK = @FundPK
			) 
	
		)B ON A.Date = case when B.InterestPaymentType <> 7 then  B.MaturityDate else  dateadd(month, month(B.Date) + 1 - month(B.MaturityDate), B.MaturityDate) end and B.FundPK = C.FundPK

		WHERE  A.Date BETWEEN @Date AND @Dateto  and C.FundPK = @FundPK
		GROUP BY C.FundPK,A.Date,B.AcqDate,B.MaturityDate,IsHoliday,DT1,B.InterestPaymentType


		


		) C
		left join Fund D on D.status = 2 and C.FundPK = D.FundPK 
		GROUP BY Date,Item,IsHoliday,DT1



		insert into #ResultData

-- EQUITY BUY
		SELECT 6 Position,SettlementDate ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- EQUITY SELL
		SELECT 7 Position,SettlementDate ValueDate,isnull(TotalAmount,0)  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- BOND BUY
		SELECT 8 Position,SettlementDate ValueDate,isnull(TotalAmount * -1,0) TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- BOND SELL
		SELECT 9 Position,SettlementDate ValueDate,isnull(TotalAmount,0)  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- COUPON BOND

		select 10 Position,Z.Date,isnull(sum(Z.GrossAmount - Z.TaxAmount),0) Amount from 
		(
		SELECT case when A.IsHoliday = 1 then A.DT1 else A.Date end Date,'Coupon Obligasi' Item,
		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
				case when B.AcqDate <= B.MaturityDate then datediff(day,B.LastCouponDate,B.MaturityDate) else 
					datediff(day,B.LastCouponDate,B.MaturityDate) end end),0) 
		else
		isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
		* case when B.InterestDaysType = 360 then B.InterestDaysType/B.InterestPaymentType else 
			case when B.AcqDate <= B.NextCouponDate then datediff(day,B.LastCouponDate,B.NextCouponDate) else 
				datediff(day,B.LastCouponDate,B.NextCouponDate) end end),0)  
		end GrossAmount,

		case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.MaturityDate then (B.InterestDaysType/B.InterestPaymentType) + datediff(day,B.NextCouponDate,B.MaturityDate) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.MaturityDate) end),0) * 0.05
		else 
			isnull(SUM(ISNULL(dbo.FGetDailyBondInterestAccrued(B.Date,B.InstrumentPK,B.FundPK,B.AcqDate),0) 
			* case when B.InterestDaysType = 360 and B.AcqDate <= B.LastCouponDate then (B.InterestDaysType/B.InterestPaymentType) 
					else dbo.FgetDateDiffCorporateBond(B.AcqDate,B.NextCouponDate) end),0) * 0.05
	
		end	TaxAmount
		FROM dbo.ZDT_WorkingDays A
		LEFT JOIN 
		(
			SELECT A.Date,A.FundPK,A.InstrumentPK,dbo.FgetLastCouponDate(A.Date,A.InstrumentPK) LastCouponDate,dbo.Fgetnextcoupondate(A.Date,A.InstrumentPK) NextCouponDate,A.Balance,A.AcqDate,
			case when A.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when A.InterestPaymentType in (1,4,7) then 12 
						when A.InterestPaymentType in (13) then 4 
							when A.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,A.MaturityDate FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			WHERE A.status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
			) AND FundPK = @FundPK
			AND B.InstrumentTypePK not in (1,4,5,6,16)

			---- Terima Kupon saat ada jual habis tapi SettleDate = CouponDate
			union all
			select SettlementDate,A.FundPK,A.InstrumentPK,dbo.FgetLastcoupondate(ValueDate,A.InstrumentPK),dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK),DoneVolume,AcqDate,
			case when B.InterestDaysType in (1,3,5,6,7) then 360 else 1 end InterestDaysType,		
			case when B.InterestPaymentType in (1,4,7) then 12 
						when B.InterestPaymentType in (13) then 4 
							when B.InterestPaymentType in (16) then 2 else 1 end InterestPaymentType,B.MaturityDate
			from Investment A
			left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
			where TrxType = 2 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.InstrumentTypePK  not in (1,4,5,6,16)
			and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) > dbo.FWorkingDay(SettlementDate,-1) and dbo.Fgetnextcoupondate(ValueDate,A.InstrumentPK) <= SettlementDate
			and SettlementDate between @Date and @dateto and FundPK = @FundPK
			and A.InstrumentPK not in 
			(
				SELECT A.InstrumentPK FROM dbo.FundPosition A
				LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
				WHERE A.status = 2 AND Date =
				(
					SELECT MAX(Date) FROM dbo.FundPosition WHERE fundPK = @FundPK AND date < case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND status = 2
				) AND FundPK = @FundPK
				AND B.InstrumentTypePK not in (1,4,5,6,16)
			)
	
		)B ON A.Date = case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end and B.AcqDate < case when datediff(day,B.NextCouponDate,B.MaturityDate) < 20 then B.MaturityDate else B.NextCouponDate end

		WHERE  A.Date BETWEEN case when dbo.CheckIsYesterdayHoliday(@Date) = 1 then @DateMinOne else @Date end AND @Dateto
		GROUP BY A.Date,B.AcqDate,B.NextCouponDate,B.LastCouponDate,IsHoliday,DT1,B.MaturityDate,B.FundPK
		)Z
		where Z.Date between @Date and @dateto
		group by Z.Date

		insert into #ResultData

-- Subscription
		SELECT 11 Position,A.ValueDate,isnull(CashAmount,0) TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		insert into #ResultData

-- Redemption
		SELECT 12 Position,A.PaymentDate,isnull(CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END,0) TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK and TotalCashAmount>0

		insert into #ResultData

-- Management Fee
		SELECT 13 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 79

		insert into #ResultData

-- Custodi Fee
		SELECT 14 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 80

		insert into #ResultData

-- Other Fee
		SELECT 15 Position,@Date,isnull(sum(BaseDebit-BaseCredit),0) from FundJournalDetail A
		left join FundJournal B on A.FundJournalPK = B.FundJournalPK
		where B.Status = 2 and B.Posted = 1 and B.Reversed = 0 and A.FundPK = @FundPK 
		and B.ValueDate = @Date and A.FundJournalAccountPK = 103

--	)A
--	GROUP BY A.Position,A.ValueDate
--)B ON A.Date = B.ValueDate AND A.Position = B.Position


--select * from #FinalResult



INSERT INTO #FinalResult
        ( Position, Name, Date, Balance )


SELECT 1,'Saldo Last Day',@Date
,dbo.[FGetAccountFundJournalBalanceByFundPK](DTM1,3,@FundPK) Amount

FROM dbo.ZDT_WorkingDays
WHERE Date = @Date


INSERT INTO #FinalResult
        ( Position, Name, Date, Balance )

SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance 
FROM #Result A
LEFT JOIN 
(
select Position,ValueDate,SUM(ISNULL(TotalAmount,0)) TotalAmount from #ResultData
group By Position,ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


  select @ValidateAmount = sum(Balance) from #FinalResult
  

                        IF (@Amount > @ValidateAmount)
                        BEGIN
	                        select 1 Result
                        END
                        ELSE	
                        BEGIN
	                        select 0 Result
                        END

                           ";



                        cmd.Parameters.AddWithValue("@Amount", _amount);
                        cmd.Parameters.AddWithValue("@Date", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
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

        public Boolean Validate_CheckAvailableInstrument(Investment _investment)
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
Declare @MaxDateEndDayFP datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date and FundPK = @FundPK
)
and status = 2  and FundPK = @FundPK

select @CurrBalance = sum(A.Balance)  from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.InstrumentPK = @InstrumentPK

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.InstrumentPK = @InstrumentPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.InstrumentPK = @InstrumentPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID
	
) A 


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

if @Amount > 0
BEGIN
	if ((@Amount /case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)
	else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  ) > @CurrBalance )
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
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
        
Declare @CurrBalance numeric (18,4)
  
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
)
and status = 2

select @CurrBalance = sum(A.Balance)  from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.InstrumentPK = @InstrumentPK

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.InstrumentPK = @InstrumentPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.InstrumentPK = @InstrumentPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID
	
) A 


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

if @Amount > 0
BEGIN
	if ((@Amount /case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,@InstrumentPK)
	else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,@InstrumentPK) End  ) > @CurrBalance )
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
                        }

                        

                        cmd.Parameters.AddWithValue("@date", _investment.ValueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                        cmd.Parameters.AddWithValue("@Balance", _investment.Volume);
                        cmd.Parameters.AddWithValue("@TrxBuy", _investment.TrxBuy);
                        cmd.Parameters.AddWithValue("@TrxBuyType", _investment.TrxBuyType);
                        cmd.Parameters.AddWithValue("@Amount", _investment.Amount);
                        cmd.Parameters.AddWithValue("@MethodType", _investment.MethodType);

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

        public NavProjection OMSEquity_GenerateNavProjection(DateTime _date, string _fundID)
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

                        --drop table #Shares
                        --drop table #Cash
                        --drop table #Expense
                        --drop table #AR
                        --drop table #TotalAUM
                        --drop table #TotalUnit
                        --drop table #NAV

                        declare @WorkingDate datetime
                        set @WorkingDate = dbo.FWorkingDay(@ValueDate, - 1)

                        IF (@FundPK <> 0)
                        BEGIN
                        CREATE TABLE #TotalAUM
                        (FundPK int,AUM numeric (22,2))
                        CREATE TABLE #TotalUnit
                        (FundPK int,Unit numeric (22,4))
                        create table #Shares
                        (FundPK int,amount numeric(22,4))
                        create table #Cash
                        (FundPK int,amount numeric(22,4))
                        create table #Expense
                        (FundPK int,amount numeric(22,4))
                        create table #AR
                        (FundPK int,amount numeric(22,4))


                        Create table #OMSEquityByAllInstrumentProjection
                        (
                        InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        SectorID nvarchar(200) COLLATE DATABASE_DEFAULT,
                        Balance numeric(18,0),
                        AvgPrice numeric(18,6),
                        CostValue numeric(22,4),
                        ClosePrice numeric(18,4),
                        LastPrice numeric(18,4),
                        LotInShare int,
                        Status int
                        )          

                        insert into #Cash (FundPK,amount)
                        select @FundPK,AUM From CloseNav where Date = @WorkingDate and status  = 2 and FundPK = @FundPK
                                      
                        insert into #Cash (FundPK,amount)
                        select FundPK FundPK,sum((LastPrice - OrderPrice) * Volume) Amount from (
                        Select FundPK,B.ID InstrumentID,   
                        case when A.TrxType = 1 and A.StatusDealing in( 0,1) then A.Volume else
                        case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume else
                        case when A.trxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1 end end end Volume,
                        case when A.StatusDealing in( 0,1) then isnull(A.OrderPrice,1) else
                        case when A.StatusDealing = 2 then isnull(A.DonePrice,1) 
                        end end OrderPrice,	[dbo].[FGetLastClosePrice](@ValueDate,A.InstrumentPK) ClosePrice, 
                        case when dbo.[FgetPriceFromInstrumentMarketInfo](@ValueDate,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@ValueDate,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@ValueDate,A.InstrumentPK) End LastPrice,B.LotInShare LotInShare,1 Flag 
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Sector C on B.SectorPK = C.SectorPK and C.Status = 2
                        where ValueDate = @ValueDate and StatusInvestment <> 3 and StatusDealing <> 3 and A.instrumentTypePK = 1
                        and FundPK = @FundPK ) A
                        group by FundPK
                   
                        insert into #Cash (FundPK,amount)
                        Select @FundPK,sum(A.MarketValue)
                        from
                        (                        
                        Select sum(Balance * LastPrice) MarketValue                   
                        From #OMSEquityByAllInstrumentProjection
                        Group by SectorID,InstrumentID,Status
                        having sum(balance) > 0
                        )A



                        Declare @CInstrumentPK    int                        

                        Declare @CFundPK     int                  

                        Declare @CBalance     numeric(18,0)                  

                        Declare C Cursor For                   

                        Select A.InstrumentPK,A.FundPK,A.Balance                                   

                        From FundPosition A                                

                        where TrailsPK = (              

                        Select EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where status = 2 and ValueDate = @WorkingDate              

                        )   and FundPK = @FundPK and status  = 2            

                        Open C                  

                        Fetch Next From C                  

                        Into @CInstrumentPK,@CFundPK,@CBalance



                        While @@FETCH_STATUS = 0                  

                        Begin    
                        insert into #AR (FundPK,Amount)
                        select @FundPK,dbo.[FGetBondInterestAccrued] (@ValueDate,@CInstrumentPK,@CBalance)

                        insert into #AR (FundPK,Amount)
                        select @FundPK,dbo.[FGetDepositoInterestAccrued] (@ValueDate,@CInstrumentPK,@CBalance)  
   
                        Fetch next From C                   

                        Into @CInstrumentPK,@CFundPK,@CBalance            

                        end        

                        Close C                  

                        Deallocate C

                        --DAILY FEE

                        if Not Exists
                        (select FundJournalAccountPK from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 and B.Reversed = 0
                        where  ValueDate = @valuedate and TrxName = 'DAILY FEE' and fundpk = @FundPK) 
                        BEGIN 
                        Declare @BAum     numeric(22,8)  
                        Declare @BFundPK int  
                        Declare @BManagementFeePercent numeric(18,6)                  
                        Declare @BCustodiFeePercent  numeric(18,6)                  
                        Declare @BAuditFeePercent  numeric(18,6)                  
                        Declare @BManagementFeeDays  int                  
                        Declare @BCustodiFeeDays  int                  
                        Declare @BAuditFeeDays   int     
                        Declare @BManagementFeeAmount numeric(18,6)                  
                        Declare @BCustodiFeeAmount  numeric(18,6)                  
                        Declare @BAuditFeeAmount  numeric(18,6)  
                        Declare @feeDays  int                           

                        Select @feeDays =  DateDiff(day,ValueDate,@ValueDate) From EndDayTrails              

                        Where EndDayTrailsPK =              

                        (              

                        Select Max(EndDayTrailsPK) From EndDayTrails Where ValueDate < @ValueDate and status = 2              

                        )              

                        set @feeDays = isnull(@feeDays,1)


                        Declare B Cursor For                  

                        Select FundPK,ManagementFeePercent,CustodiFeePercent,AuditFeeAmount,ManagementFeeDays, CustodiFeeDays,AuditFeeDays                  

                        From FundFee Where FundPK  = @FundPK and Date =                  

                        (                  
                        Select max(date) From FundFee where status = 2 and date < @ValueDate                  

                        )                  

                        Open B                  

                        Fetch Next From B                  

                        Into @BFundPK,@BManagementFeePercent,@BCustodiFeePercent,@BAuditFeeAmount,@BManagementFeeDays,@BCustodiFeeDays,@BAuditFeeDays                  

                        While @@FETCH_STATUS = 0                  

                        Begin  
                        Select @BAum =  AUM From CloseNAV where Date = @WorkingDate and Status =  2 and FundPK = @BFundPK 
                        insert into #Expense (FundPK,Amount)
                        Select  @BFundPK,isnull(((@BAum * (@BManagementFeePercent/100))/ @BManagementFeeDays),0) * @FeeDays * -1   
                        insert into #Expense (FundPK,Amount)                 
                        Select  @BFundPK,isnull(((@BAum * (@BCustodiFeePercent/100))/ @BCustodiFeeDays),0)  * @FeeDays * -1  
                        insert into #Expense (FundPK,Amount)                
                        Select  @BFundPK,isnull((@BAuditFeeAmount),0)  * @FeeDays * -1   

                        Fetch next From B                   

                        Into @BFundPK,@BManagementFeePercent,@BCustodiFeePercent,@BAuditFeeAmount,@BManagementFeeDays,@BCustodiFeeDays,@BAuditFeeDays             

                        end                  

                        Close B                  

                        Deallocate B 
                        END 



                        insert into #TotalUnit (FundPK,Unit)
                        select @FundPK,case when isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) = 0 then 1 else isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) end



                        Insert Into #TotalAUM (FundPK,AUM)
                        select FundPK,sum(amount) from #Shares Group By FundPK
                        Insert Into #TotalAUM (FundPK,AUM)
                        select FundPK,sum(amount) from #Cash  Group By FundPK
                        Insert Into #TotalAUM (FundPK,AUM)
                        select FundPK,sum(amount) from #Expense  Group By FundPK
                        Insert Into #TotalAUM (FundPK,AUM)
                        select FundPK,sum(amount) from #AR  Group By FundPK

                        CREATE TABLE #NAV
                        (ValueDate Datetime, FundPK int, Amount numeric(22,2))
                        INSERT INTO #NAV (ValueDate,FundPK,Amount)
                        select @ValueDate,FundPK,sum(AUM) from #TotalAUM Group By FundPK 


                        select A.FundName,A.Nav,A.AUM,sum(((A.Nav/A.LastNAV) - 1)*100) Compare from (
                        select C.Name FundName,isnull(Sum(Amount/Unit),0) Nav, isnull(Amount,0) AUM,isnull(D.NAV,0) LastNAV from #NAV A 
                        left join #TotalUnit B on A.FundPK = B.FundPK
                        left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                        left join CloseNav D on A.FundPK = D.FundPK and D.Status = 2 and D.Date  = @WorkingDate
                        where B.FundPK =@FundPK
                        Group By C.Name,Amount,D.Nav) A 
                        Group By A.FundName,A.Nav,A.AUM
                        END
                        ELSE
                        BEGIN
                        select '' FundName,0 Nav, 0 AUM,0 Compare 
                        END

                      
                        ";

                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new NavProjection()
                                {
                                    FundName = dr["FundName"].ToString(),
                                    Nav = Convert.ToDecimal(dr["Nav"]),
                                    AUM = Convert.ToDecimal(dr["AUM"]),
                                    Compare = Convert.ToDecimal(dr["Compare"])
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

        //private NavProjection setNavProjection(SqlDataReader dr)
        //{
        //    NavProjection M_Model = new NavProjection();
        //    M_Model.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
        //    M_Model.FundName = dr["FundName"].ToString();
        //    M_Model.Nav = Convert.ToDecimal(dr["Nav"]);
        //    M_Model.AUM = Convert.ToDecimal(dr["AUM"]);
        //    M_Model.Compare = Convert.ToDecimal(dr["Compare"]);

        //    return M_Model;
        //}
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
               

                        
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime
Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
	Select max(ValueDate) from endDayTrailsFundPortfolio where
	valuedate < @Date  and status = 2 and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK






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

Create table #OMSEquityExposure
(
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8)
	
)
Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date <@Date
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1) 

---------------------- EQUITY ALL CHECKING ------------------------


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'EQUITY ALL',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK  
)C on C.parameter = 0
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 


---------------------- EQUITY PER SINGLE INSTRUMENT CHECKING ------------------------

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'PER SINGLE INSTRUMENT EQUITY',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK  and Parameter > 0
)C on C.parameter = @InstrumentPK
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2 
Where FundPK = @FundPK and A.status = 2 



------------------------- INSTRUMENT TYPE CHECKING ---------------------------------

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
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

------------------------- SECTOR ---------------------------------
Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'SECTOR',0,0,E.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
left join SubSector D on B.SectorPK = D.SubSectorPK and D.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @fundPK 
)C on D.SectorPK = C.Parameter 
Left join Sector E on D.SectorPK = E.SectorPK and E.status = 2
Where FundPK = @fundPK and A.status = 2

------------------------- INDEX ---------------------------------
Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'INDEX',0,0,E.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
left join InstrumentIndex D on B.InstrumentPK = D.InstrumentPK and D.status = 2
left join [Index] E on D.IndexPK = E.IndexPK and E.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @fundPK 
)C on E.IndexPK = C.Parameter 
Where FundPK = @fundPK and A.status = 2


--------------SYARIAH CHECKING----------------
IF EXISTS(Select * from fundExposure where FundPK = @FundPK and Type = 15 and status = 2)
BEGIN
if Not Exists(select * from instrumentSyariah where InstrumentPK = @InstrumentPK and status = 2)
BEGIN
Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure) 
Select 'SYARIAH','SYARIAH',100,0,0,100,1,1,1,1
END
	
END


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'SYARIAH',0,0,D.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter 
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 




------------------------POSISI GET ANGKA DARI POSISI DAN MOVEMENT--------------------------------




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'PER SINGLE INSTRUMENT EQUITY' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)C on C.Parameter = @InstrumentPK
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1
and A.InstrumentPK = @InstrumentPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'PER SINGLE INSTRUMENT EQUITY' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)D on D.Parameter  = @InstrumentPK
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.InstrumentPK = @InstrumentPK



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1
and A.InstrumentPK = @InstrumentPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'EQUITY ALL' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.InstrumentPK = @InstrumentPK



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, F.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on F.Type = C.Parameter
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on F.Type = D.parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3


Declare @PSectorPK int
Select @PSectorPK = B.SectorPK from Instrument A 
left join SubSector B on A.SectorPK = B.SubSectorPK and B.status = 2
where A.InstrumentPK = @InstrumentPK

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'SECTOR' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, D.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on E.SubSectorPK = B.SectorPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)C on E.SectorPK = C.Parameter
left join Sector D on E.SectorPK = D.SectorPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1 
and E.SectorPK = @PSectorPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'SECTOR' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on B.SectorPK = E.SubSectorPK and E.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)D on E.SectorPK = D.Parameter
left join Sector F on F.SectorPK = E.SectorPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3 and H.Type = 1
and E.SectorPK = @PSectorPK


--Declare @PIndexPK int
--Select @PIndexPK = IndexPK from InstrumentIndex 
--	where Date = (
--		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
--	) and status = 2 and InstrumentPK = @InstrumentPK

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INDEX' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, E.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
	) and status = 2 --and InstrumentPK = @InstrumentPK
	
)D on D.InstrumentPK = A.InstrumentPK
left join [Index] E on D.IndexPK = E.IndexPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)C on E.IndexPK = C.Parameter
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1 




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INDEX' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
	) and status = 2 --and InstrumentPK = @InstrumentPK
	
)E on E.InstrumentPK = A.InstrumentPK
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)D on E.IndexPK = D.Parameter
left join [Index] F on E.IndexPK = F.indexPK and F.status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3 and H.Type = 1


-- ISSUER

Declare @PIssuerPK int
Select @PIssuerPK = IssuerPK from Instrument 
where InstrumentPK = @InstrumentPK and status in (1,2)



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
select B.ID,'ISSUER',sum(isnull(A.MarketValue,0)),sum(isnull(A.NAVPercent,0)),C.ID,D.MinExposurePercent,
D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent from
(
select C.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
where A.TrailsPK = @TrailsPK and A.status = 2 and C.InstrumentTypePK <> 5 and isnull(C.IssuerPK,0) <> 0 and C.IssuerPK = @PIssuerPK
and A.FundPK = @FundPK 
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
where A.Date > @MaxDateEndDayFP and A.status = 2 and C.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0  and H.IssuerPK = @PIssuerPK
and A.FundPK = @FundPK 
group by H.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
) A 
left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
left join Issuer C on A.IssuerPK = C.IssuerPK and C.Status in (1,2)
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
group by B.ID,C.ID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by C.ID



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'ISSUER' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
left join Issuer F on B.IssuerPK = F.IssuerPK and F.status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3
and B.InstrumentTypePK <> 5 and isnull(F.IssuerPK,0) <> 0 and F.IssuerPK = @PIssuerPK


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'ISSUER' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,I.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
left join BankBranch G on A.BankbranchPK = G.BankBranchPK and G.status in (1,2)
left join Bank H on G.BankPK = H.BankPK and H.status in (1,2)
left join Issuer I on H.IssuerPK = I.IssuerPK and I.status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3
and E.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0 and H.IssuerPK = @PIssuerPK



Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure)                     
Select ExposureType,
UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,(sum(MarketValue) + @Amount) MarketValue, (sum(MarketValue)+@Amount)/ @TotalMarketValue * 100 ExposurePercent,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 < MinExposurePercent then 1 else 0 end AlertMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSEquityExposure A 
left join Instrument B on A.ExposureID = B.ID and B.status  = 2
--where instrumentPK  =  @InstrumentPK
Group by ExposureID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,ExposureType
order by ExposureID 


select  ExposureType,ExposureID,Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end AlertExposure,ExposurePercent,MaxExposurePercent,MinExposurePercent from #Exposure where 
Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end > 0 and ExposureID not in ('G-BOND','C-BOND','DEP')
order by AlertExposure desc
                           ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
               

                        
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime
Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
	Select max(ValueDate) from endDayTrailsFundPortfolio where
	valuedate < @Date  and status = 2
)
and status = 2






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

Create table #OMSEquityExposure
(
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureType nvarchar(100) COLLATE DATABASE_DEFAULT,
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
MarketValue numeric(22,4),
ExposurePercent numeric(18,8),
MinExposurePercent numeric(18,8),
MaxExposurePercent numeric(18,8),
WarningMinExposurePercent numeric(18,8),
WarningMaxExposurePercent numeric(18,8)
	
)
Declare @TotalMarketValue numeric(26,6)

select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date <@Date
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

set @TotalMarketValue = isnull(@TotalMarketValue,1) 

---------------------- EQUITY ALL CHECKING ------------------------


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'EQUITY ALL',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK  
)C on C.parameter = 0
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 


---------------------- EQUITY PER SINGLE INSTRUMENT CHECKING ------------------------

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'PER SINGLE INSTRUMENT EQUITY',0,0,B.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK  and Parameter > 0
)C on C.parameter = @InstrumentPK
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2 
Where FundPK = @FundPK and A.status = 2 



------------------------- INSTRUMENT TYPE CHECKING ---------------------------------

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
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

------------------------- SECTOR ---------------------------------
Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'SECTOR',0,0,E.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
left join SubSector D on B.SectorPK = D.SubSectorPK and D.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @fundPK 
)C on D.SectorPK = C.Parameter 
Left join Sector E on D.SectorPK = E.SectorPK and E.status = 2
Where FundPK = @fundPK and A.status = 2

------------------------- INDEX ---------------------------------
Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'INDEX',0,0,E.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
left join InstrumentIndex D on B.InstrumentPK = D.InstrumentPK and D.status = 2
left join [Index] E on D.IndexPK = E.IndexPK and E.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @fundPK 
)C on E.IndexPK = C.Parameter 
Where FundPK = @fundPK and A.status = 2


--------------SYARIAH CHECKING----------------
IF EXISTS(Select * from fundExposure where FundPK = @FundPK and Type = 15 and status = 2)
BEGIN
if Not Exists(select * from instrumentSyariah where InstrumentPK = @InstrumentPK and status = 2)
BEGIN
Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure) 
Select 'SYARIAH','SYARIAH',100,0,0,100,1,1,1,1
END
	
END


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.ID,'SYARIAH',0,0,D.ID,C.MinExposurePercent,c.MaxExposurePercent,
c.WarningMinExposurePercent,c.WarningMaxExposurePercent From Fund A
left join Instrument B on B.InstrumentPK = @InstrumentPK and B.status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 13 and Status = 2 and FundPK = @FundPK 
)C on B.InstrumentTypePK = C.Parameter 
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
Where FundPK = @FundPK and A.status = 2 




------------------------POSISI GET ANGKA DARI POSISI DAN MOVEMENT--------------------------------




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'PER SINGLE INSTRUMENT EQUITY' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)C on C.Parameter = @InstrumentPK
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1
and A.InstrumentPK = @InstrumentPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'PER SINGLE INSTRUMENT EQUITY' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
)D on D.Parameter  = @InstrumentPK
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.InstrumentPK = @InstrumentPK



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)C on C.Parameter = 0
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and F.Type = 1
and A.InstrumentPK = @InstrumentPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'EQUITY ALL' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.InstrumentPK = @InstrumentPK



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, F.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
)C on F.Type = C.Parameter
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INSTRUMENT TYPE' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
Inner Join
(

Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
)D on F.Type = D.parameter
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK and F.Type = 1
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3


Declare @PSectorPK int
Select @PSectorPK = B.SectorPK from Instrument A 
left join SubSector B on A.SectorPK = B.SubSectorPK and B.status = 2
where A.InstrumentPK = @InstrumentPK

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'SECTOR' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, D.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on E.SubSectorPK = B.SectorPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)C on E.SectorPK = C.Parameter
left join Sector D on E.SectorPK = D.SectorPK and D.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1 
and E.SectorPK = @PSectorPK




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'SECTOR' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join SubSector E on B.SectorPK = E.SubSectorPK and E.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
)D on E.SectorPK = D.Parameter
left join Sector F on F.SectorPK = E.SectorPK and F.Status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3 and H.Type = 1
and E.SectorPK = @PSectorPK


--Declare @PIndexPK int
--Select @PIndexPK = IndexPK from InstrumentIndex 
--	where Date = (
--		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
--	) and status = 2 and InstrumentPK = @InstrumentPK

Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select A.FundID,'INDEX' ExposureType,A.Balance * 
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, E.ID ExposureID,  
C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
	) and status = 2 --and InstrumentPK = @InstrumentPK
	
)D on D.InstrumentPK = A.InstrumentPK
left join [Index] E on D.IndexPK = E.IndexPK and E.Status = 2
inner join 
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)C on E.IndexPK = C.Parameter
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.TrailsPK = @TrailsPK and FundPK = @FundPK and A.Status = 2 and H.Type  = 1 




Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'INDEX' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join (
	Select InstrumentPK, IndexPK from InstrumentIndex 
	where Date = (
		select max(date) from InstrumentIndex where status = 2 and InstrumentPK = @InstrumentPK and date <= @Date
	) and status = 2 --and InstrumentPK = @InstrumentPK
	
)E on E.InstrumentPK = A.InstrumentPK
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 14 and Status = 2 and FundPK = @FundPK
)D on E.IndexPK = D.Parameter
left join [Index] F on E.IndexPK = F.indexPK and F.status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3 and H.Type = 1


-- ISSUER

Declare @PIssuerPK int
Select @PIssuerPK = IssuerPK from Instrument 
where InstrumentPK = @InstrumentPK and status in (1,2)



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
select B.ID,'ISSUER',sum(isnull(A.MarketValue,0)),sum(isnull(A.NAVPercent,0)),C.ID,D.MinExposurePercent,
D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent from
(
select C.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
where A.TrailsPK = @TrailsPK and A.status = 2 and C.InstrumentTypePK <> 5 and isnull(C.IssuerPK,0) <> 0 and C.IssuerPK = @PIssuerPK
and A.FundPK = @FundPK 
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
where A.Date > @MaxDateEndDayFP and A.status = 2 and C.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0  and H.IssuerPK = @PIssuerPK
and A.FundPK = @FundPK 
group by H.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
) A 
left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
left join Issuer C on A.IssuerPK = C.IssuerPK and C.Status in (1,2)
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
group by B.ID,C.ID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
order by C.ID



Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'ISSUER' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,F.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
left join Issuer F on B.IssuerPK = F.IssuerPK and F.status = 2
left join InstrumentType H on B.InstrumentTypePK = H.InstrumentTypePK and H.Status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3
and B.InstrumentTypePK <> 5 and isnull(F.IssuerPK,0) <> 0 and F.IssuerPK = @PIssuerPK


Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
Select C.ID,'ISSUER' ExposureType
,isnull((case when A.DoneVolume > 0 then A.DoneVolume  * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
	then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
		End else A.DoneAmount end),0)
,0,I.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
From investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
Inner Join
(
Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
From FundExposure where Type = 2 and Status = 2 and FundPK = @FundPK and Parameter = 0
)D on D.Parameter = 0
left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
left join BankBranch G on A.BankbranchPK = G.BankBranchPK and G.status in (1,2)
left join Bank H on G.BankPK = H.BankPK and H.status in (1,2)
left join Issuer I on H.IssuerPK = I.IssuerPK and I.status = 2
where A.ValueDate  > @MaxDateEndDayFP and A.ValueDate <= @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusSettlement <> 3 and A.StatusInvestment <> 3
and E.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0 and H.IssuerPK = @PIssuerPK


---- CASE SAAT BLM ADA DI INVESTMENT / FUND POSITION
Declare @InstrumentID nvarchar(100)

select @InstrumentID = ID from Instrument where InstrumentPK = @InstrumentPK and status = 2

IF NOT EXISTS(select * from Investment where  ValueDate = @date and FundPK = @FundPK and InstrumentPK = @InstrumentPK and StatusDealing <> 3 and StatusSettlement <> 3 and StatusInvestment <> 3  )
BEGIN
	Insert into #OMSEquityExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
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
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 < MinExposurePercent then 1 else 0 end AlertMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
From #OMSEquityExposure A 
left join Instrument B on A.ExposureID = B.ID and B.status  = 2
--where instrumentPK  =  @InstrumentPK
Group by ExposureID,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent,ExposureType
order by ExposureID 


select  ExposureType,ExposureID,Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end AlertExposure,ExposurePercent,MaxExposurePercent,MinExposurePercent from #Exposure where 
Case when AlertMaxExposure = 0 and WarningMaxExposure = 1 then 1 else case when AlertMaxExposure = 1 and WarningMaxExposure = 1 then 2
else Case when AlertMinExposure = 0 and WarningMinExposure = 1 then 3 else case when AlertMinExposure = 1 and WarningMinExposure = 1 then 4 end end end end > 0 and ExposureID not in ('G-BOND','C-BOND','DEP')
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
                                    MinExposurePercent = dr["MinExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinExposurePercent"]),

                                };
                            }
                            else
                            {
                                return new FundExposure()
                                {
                                    ExposureID = "",
                                    AlertExposure = 0,
                                    ExposurePercent = 0,
                                    MinExposurePercent = 0,
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

        public FundPriceExposure Validate_CheckPriceExposure(DateTime _valuedate, int _instrumentPK, int _fundPK, decimal _price)
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
                            declare @Closeprice numeric(18,4)
                            declare @Minprice numeric(18,4)
                            declare @Maxprice numeric(18,4)
                            declare @parameter numeric(18,4)

		                    select @Closeprice = ClosePriceValue
		                    From ClosePrice A
			                where A.Date = (select max(date) from ClosePrice A where A.date <= @Date and A.InstrumentPK = @instrumentPK and A.status  = 2)
                            and A.status = 2 and A.InstrumentPK = @InstrumentPK
                            
                            set @ClosePrice = isnull(@ClosePrice,0)

                            select @Minprice = @Closeprice - (@Closeprice * (MinPricePercent/100)), @Maxprice = @Closeprice + (@Closeprice * (MaxPricePercent/100)) 
                            From RangePrice A 
                            where A.status  = 2 and  @Closeprice between MinPrice and MaxPrice
                            
                            set @MaxPrice = case when @MaxPrice = 0 or @MaxPrice is null then 0 else @maxPrice end
                            select round(@Minprice,0) MinPrice,round(@Maxprice,0) MaxPrice,case when @MaxPrice = 0 then 0 else case when @Price between round(@MinPrice,0) and round(@Maxprice,0) then 0 else 1 End end Validate 

                           ";

                        cmd.Parameters.AddWithValue("@date", _valuedate);
                        cmd.Parameters.AddWithValue("@instrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@fundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@price", _price);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundPriceExposure()
                                {
                                    MinPrice = dr["MinPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinPrice"]),
                                    MaxPrice = dr["MaxPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxPrice"]),
                                    Validate = dr["Validate"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Validate"]),
                                };
                            }
                            else
                            {
                                return new FundPriceExposure()
                                {
                                    MinPrice = 0,
                                    MaxPrice = 0,
                                    Validate = 0,

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

        public decimal Get_TotalLotForOMSEquity(int _fundPK, DateTime _date, int _instrumentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @TrailsPK int
                        select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2 and FundPK = @FundPK

                        select InstrumentPK,ID,sum(A.Balance) Balance,CurrencyID from (
                        select A.InstrumentPK,B.ID + ' - ' + B.Name ID,A.Balance,D.ID CurrencyID from FundPosition A    
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                        Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                        where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (1)

                             
                        union all

                        Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                        case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                        where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (1) and OrderStatus in ('M','P')
                        and FundPK = @fundpk and TrxType  = 1
                        group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate
                        ) A
                        group By InstrumentPK,ID,CurrencyID
                        order by ID
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @TrailsPK int
                        select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2

                        select InstrumentPK,ID,sum(A.Balance) Balance,CurrencyID from (
                        select A.InstrumentPK,B.ID + ' - ' + B.Name ID,A.Balance,D.ID CurrencyID from FundPosition A    
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                        Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                        where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (1)

                             
                        union all

                        Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                        case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                        where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (1) and OrderStatus in ('M','P')
                        and FundPK = @fundpk and TrxType  = 1
                        group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate
                        ) A
                        group By InstrumentPK,ID,CurrencyID
                        order by ID
                        ";
                        }

                        
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

        public Boolean OMSEquity_ListingRpt(string _userID, InvestmentListing _listing)
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
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
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
                                            ,IV.valueDate
                                            ,I.ID InstrumentID
                                            ,I.Name InstrumentName
                                            ,F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,IV.Amount,IV.Notes, IV.RangePrice, IV.*   
                                            from Investment IV       
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                                            left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                                            left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                                            Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3   and IT.Type = 1
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
                                string filePath = Tools.ReportsPath + "OMSEquityListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSEquityListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);
                                        rSingle.PTPCode = Convert.ToString(dr0["PTPCode"]);

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
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EntryDealingID;
                                                //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.CounterpartID;
                                                //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.PTPCode;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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

                                        _range = "A" + incRowExcel + ":G" + incRowExcel;
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

                                        worksheet.Cells[incRowExcel, 4].Value = "  (";
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
                                        incRowExcel++;
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
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
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


        public Boolean Validate_UpdateCheckAvailableCash(int _investmentPK, decimal _amount, decimal _cashAvailable)
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
                            Declare @Amount numeric (22,4)
                            select @Amount = Amount from Investment where InvestmentPK = @InvestmentPK and statusInvestment <> 3
                            if ( " + _amount + @" > " + _cashAvailable + @" + @Amount)
                            BEGIN
                            select 1 Result
                            END
                            ELSE
                            BEGIN
                            select 0 Result
                            END 
                           ";

                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);

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


        public string ImportOmsEquityTemp(string _fileSource, string _userID, string _valueDate)
        {
            string _msg = string.Empty;
            DateTime _now = DateTime.Now;
            try
            {
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table dbo.OmsEquityImportTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.OmsEquityImportTemp";
                    if (Tools.ClientCode == "03")
                        bulkCopy.WriteToServer(CreateDataTableFromOMSEquityTempExcelFile_03(_fileSource));
                    else
                        bulkCopy.WriteToServer(CreateDataTableFromOMSEquityTempExcelFile(_fileSource));

                }
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        // logic Checks
                        // 1. Instrument sudah ada atau belum di investment sesuai valudate
                        // 2. instrument sudah ada atau belum di master instrument
                        // 3. check enddaytrails fund portfolio di hari itu uda ada apa belum
                        // 4. check available cash
                        // 5. check price exposure
                        // 6. 

                        cmd2.CommandTimeout = 0;
                        cmd2.CommandText =
                            @"
--declare @ValueDate		date,
--		@UsersID		nvarchar(100),
--		@LastUpdate		datetime,
--		@ParamFundScheme int,
--		@ClientCode nvarchar(20),
--		@Date			date

--	set @ValueDate		= '2020-05-19'
--	set @UsersID		= 'admin'
--	set @LastUpdate		= '2020-05-19'
--	set @ParamFundScheme = 1
--	set @ClientCode = '17'
--	set @Date		= '2020-05-19'

--drop table #TableSelectData

truncate table [ZTEMP_Import_Investment]

delete OmsEquityImportTemp where BuySell = ''

TRUNCATE TABLE ZTEMP_FUNDEXPOSURE_IMPORT

create table #TableSelectData (
	BuySell nvarchar(20),
	InstrumentID nvarchar(20),
	Price numeric(18,4),
	Lot numeric(22,4),
	Volume numeric(22,4),
	Amount numeric(32,6),
	MarketPK int,
	InstrumentPK int,
	InstrumentTypePK int,
	LotInShare numeric(32,6),
	FundPK int,
	MethodType int,
	BitContinueSave int
)

Declare @CurReference nvarchar(100)  
Declare @PK int    
Declare @CFundID nvarchar(100)
Declare @StatusSuccess int
	
	           
declare @success	int,
@msg		nvarchar(max)

set @success	= 1
set @msg		= ''
declare @Cp1 nvarchar(100)


declare @MaxInvestmentPK bigint

select @MaxInvestmentPK = isnull(max(InvestmentPK), 0) from Investment

if exists (select * from OmsEquityImportTemp where BuySell <> 'B/S' and InstrumentID  <> '')
begin
-- Cek Master Instrument
if not exists (
select distinct InstrumentID
from OmsEquityImportTemp 
where BuySell <> 'B/S' and InstrumentID not in (select distinct ID from Instrument where [Status] = 2) and InstrumentID <> ''
)
begin
	begin transaction

	declare @PeriodPK		int,
			@Type			nvarchar(100),
			@LastNo			int

	declare @CInstrumentPK			int,
			@CInstrumentTypePK		int,
			@CFundPK				int,
			@CMarketPK				int,
			@CLotInShare			numeric(18,4),
			@CBuySell				nvarchar(20),
			@CInstrumentID			nvarchar(100),
			@CPrice					numeric(22,6),
			@CLot					numeric(22,4),
			@CVolume					numeric(22,4),
			@CAmount				numeric(22,4),
			@CBitContinueSave		bit,
			@CMethodType			int
		
	select @PeriodPK = PeriodPK 
	from Period 
	where @ValueDate between DateFrom and DateTo and [Status] = 2

    if @ClientCode = '20'
        set @Type = 'EQ'
    else
		set @Type	= 'INV'

	set @LastNo	= 0

												
	declare @BitOMSEndDayTrailsFundPortfolio	bit,
			@BitAvailableCash					bit,
			@MinPrice							numeric(18,4),
			@MaxPrice							numeric(18,4),
			@Validate							bit,
			@ExposureType						nvarchar(100),
			@ExposureID							nvarchar(100),
			@AlertExposure						int,
			@ExposurePercent					numeric(18,4),
			@MaxExposurePercent					numeric(18,4),
			@MinExposurePercent					numeric(18,4),
			@TrxBuy								int,
			@TrxBuyType							nvarchar(50),
			@MethodType							int,
			@BitAvailableInstrument				bit,
			@TrxType							int

	declare @LastRow int
		set @LastRow = 0
		
	set @AlertExposure = 0
	set @StatusSuccess = 1

	declare @Query nvarchar(max)
	declare @counter int
		set @counter = 1

    declare @Reference nvarchar(100)
    declare @No int
    declare @MaxPK int
    declare @Reason nvarchar(max)
    DECLARE @HighRiskMonitoringPK int
    declare @FundID nvarchar(100)
    declare @InstrumentID nvarchar(100)



		declare curImport cursor for
			select top 1 F1 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F1 is not null and F1 <> 'Spare Fund'
			union all
			select top 1 F2 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F2 is not null and F2 <> 'Spare Fund'
			union all
			select top 1 F3 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F3 is not null and F3 <> 'Spare Fund'
			union all
			select top 1 F4 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F4 is not null and F4 <> 'Spare Fund'
			union all
			select top 1 F5 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F5 is not null and F5 <> 'Spare Fund'
			union all
			select top 1 F6 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F6 is not null and F6 <> 'Spare Fund'
			union all
			select top 1 F7 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F7 is not null and F7 <> 'Spare Fund'
			union all
			select top 1 F8 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F8 is not null and F8 <> 'Spare Fund'
			union all
			select top 1 F9 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F9 is not null and F9 <> 'Spare Fund'
			union all
			select top 1 F10 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F10 is not null and F10 <> 'Spare Fund'
			union all
			select top 1 F11 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F11 is not null and F11 <> 'Spare Fund'
			union all
			select top 1 F12 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F12 is not null and F12 <> 'Spare Fund'
			union all
			select top 1 F13 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F13 is not null and F13 <> 'Spare Fund'
			union all
			select top 1 F14 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F14 is not null and F14 <> 'Spare Fund'
			union all
			select top 1 F15 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F15 is not null and F15 <> 'Spare Fund'
			union all
			select top 1 F16 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F16 is not null and F16 <> 'Spare Fund'

			union all
			select top 1 F17 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F17 is not null and F17 <> 'Spare Fund'
			union all
			select top 1 F18 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F18 is not null and F18 <> 'Spare Fund'
			union all
			select top 1 F19 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F19 is not null and F19 <> 'Spare Fund'
			union all
			select top 1 F20 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F20 is not null and F20 <> 'Spare Fund'
			union all
			select top 1 F21 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F21 is not null and F21 <> 'Spare Fund'
			union all
			select top 1 F22 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F22 is not null and F22 <> 'Spare Fund'

			union all
			select top 1 F23 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F23 is not null and F23 <> 'Spare Fund'
			union all
			select top 1 F24 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F24 is not null and F24 <> 'Spare Fund'
			union all
			select top 1 F25 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F25 is not null and F25 <> 'Spare Fund'
			union all
			select top 1 F26 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F26 is not null and F26 <> 'Spare Fund'
			union all
			select top 1 F27 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F27 is not null and F27 <> 'Spare Fund'

			union all
			select top 1 F28 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F28 is not null and F28 <> 'Spare Fund'
			union all
			select top 1 F29 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F29 is not null and F29 <> 'Spare Fund'
			union all
			select top 1 F30 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F30 is not null and F30 <> 'Spare Fund'
			union all
			select top 1 F31 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F31 is not null and F31 <> 'Spare Fund'
			union all
			select top 1 F32 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F32 is not null and F32 <> 'Spare Fund'

			union all
			select top 1 F33 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F33 is not null and F33 <> 'Spare Fund'
			union all
			select top 1 F34 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F34 is not null and F34 <> 'Spare Fund'
			union all
			select top 1 F35 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F35 is not null and F35 <> 'Spare Fund'
			union all
			select top 1 F36 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F36 is not null and F36 <> 'Spare Fund'
			union all
			select top 1 F37 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F37 is not null and F37 <> 'Spare Fund'

			union all
			select top 1 F38 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F38 is not null and F38 <> 'Spare Fund'
			union all
			select top 1 F39 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F39 is not null and F39 <> 'Spare Fund'
			union all
			select top 1 F40 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F40 is not null and F40 <> 'Spare Fund'
			union all
			select top 1 F41 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F41 is not null and F41 <> 'Spare Fund'
			union all
			select top 1 F42 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F42 is not null and F42 <> 'Spare Fund'

			union all
			select top 1 F43 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F43 is not null and F43 <> 'Spare Fund'
			union all
			select top 1 F44 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F44 is not null and F44 <> 'Spare Fund'
			union all
			select top 1 F45 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F45 is not null and F45 <> 'Spare Fund'
			union all
			select top 1 F46 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F46 is not null and F46 <> 'Spare Fund'
			union all
			select top 1 F47 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F47 is not null and F47 <> 'Spare Fund'

			union all
			select top 1 F48 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F48 is not null and F48 <> 'Spare Fund'
			union all
			select top 1 F49 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F49 is not null and F49 <> 'Spare Fund'
			union all
			select top 1 F50 from OmsEquityImportTemp where BuySell = 'B/S' and InstrumentID  <> '' and F50 is not null and F50 <> 'Spare Fund'
		open curImport

		fetch next from curImport
		into @Cp1

		while @@fetch_status = 0  
		begin  
			if @Cp1 <> 'Spare Fund' and isnull(@Cp1, '') <> '' and len(@Cp1) <> 0
			begin


					set @Query = 'truncate table #TableSelectData
							insert into #TableSelectData
							select 
							a.BuySell as CBuySell, a.InstrumentID as CInstrumentID, 
										cast(isnull(a.Price, 0) as numeric(22,4)) as CPrice, cast(isnull(a.F' + cast(@Counter as nvarchar) + ', 0) as numeric(22,4)) as CLot, 
										cast(isnull(a.F' + cast(@Counter as nvarchar) + ', 0) as numeric(22,4)) * isnull(b.LotInShare, 0) as CVolume,
										cast(isnull(a.F' + cast(@Counter as nvarchar) + ', 0) as numeric(22,4)) * isnull(b.LotInShare, 0) * cast(isnull(a.Price, 0) as numeric(22,4)) as CAmount,
										b.MarketPK, b.InstrumentPK, b.InstrumentTypePK, isnull(b.LotInShare, 0) as LotInShare, c.FundPK, 
										cast(1 as int) as MethodType, cast(1 as bit) as CBitContinueSave
									from OmsEquityImportTemp a
										left join Instrument b on a.InstrumentID = b.ID and b.[Status] = 2
										left join Fund c on c.ID ='''+ @Cp1 +''' and c.[Status] = 2
						where a.BuySell <> ''B/S'' and cast(F' + cast(@Counter as nvarchar) + ' as numeric(22,4)) > 0 and a.InstrumentID  <> '''' and B.InstrumentTypePK in (1,4,16)
					'

					exec(@Query)

 
					DECLARE AB CURSOR FOR 
						select BuySell,InstrumentID,Price,Lot,Volume,Amount,MarketPK,InstrumentPK,InstrumentTypePK, LotInShare, FundPK, MethodType, BitContinueSave from #TableSelectData
					OPEN AB;
 
					FETCH NEXT FROM AB INTO 
						 @CBuySell,@CInstrumentID,@CPrice,@CLot,@CVolume,@CAmount,@CMarketPK,@CInstrumentPK,@CInstrumentTypePK,@CLotInShare,@CFundPK,@CMethodType,@CBitContinueSave
 
					WHILE @@FETCH_STATUS = 0
						BEGIN

							if (@CBuySell = 'B')
								set @TrxType = 1
							else
								set @TrxType = 2

							if @Cp1 <> 'Spare Fund' and isnull(@Cp1, '') <> '' and len(@Cp1) <> 0
							begin
								if @CBuySell = 'B'
									begin
										-- Check OMS End Day Trails Fund Portfolio
										select @BitOMSEndDayTrailsFundPortfolio = 0

										if isnull(@BitOMSEndDayTrailsFundPortfolio, 0) = 1
										begin
					 
											set @success	= 11
											set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> End Day Trails Fund Portfolio Already Generated '
										end

										if isnull(@BitOMSEndDayTrailsFundPortfolio, 0) = 0 -- Belum Melakukan Generate End Day Trails FundPortfolio
										begin
											-- Check Available Cash
											if (@ClientCode = '05')
											BEGIN
												select @BitAvailableCash = 0
											END
											ELSE
											BEGIN
												select @BitAvailableCash = [dbo].[FCheckAvailableCash] (@ValueDate, @CFundPK, @CAmount)
											END
									
											if isnull(@BitAvailableCash, 0) = 1
											begin
						 
												set @success	= 12
												set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> Exposure : ' + cast(@CInstrumentID as nvarchar(30)) + ' </br> Cash Not Available '
											end

											if isnull(@BitAvailableCash, 0) = 0 -- Cash Available
											begin
												-- Check Price Exposure
												select @MinPrice = MinPrice, @MaxPrice = MaxPrice, @Validate = Validate
												from [dbo].[FCheckPriceExposure] (@ValueDate, @CInstrumentPK, @CFundPK, @CPrice)
										
										
						
												set @MaxInvestmentPK = @MaxInvestmentPK + 1	
                                                --custom cam
                                                if (@ClientCode = '21' and isnull(@Validate, 0) = 1)
                                                begin
                                                    
                                                    select @FundID = ID from Fund where status = 2 and FundPK = @CFundPK
                                                    set @FundID = isnull(@FundID,'')

                                                    set @Reason = 'EXPOSURE PRICE <br /><br /> FUND : ' + @FundID + ' <br /> INSTRUMENT : ' + @CInstrumentID +'  <br /> PRICE : ' + cast(@CPrice as nvarchar) + '  <br /> MIN : ' + cast(round(@MinPrice,0) as nvarchar) + '  <br /> MAX : ' + cast(round(@MaxPrice,0) as nvarchar)
                                                    
                                                    select @HighRiskMonitoringPK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		                                            set @HighRiskMonitoringPK = isnull(@HighRiskMonitoringPK,1)

                                                    insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,Description)
		                                            Select @HighRiskMonitoringPK,1,1,2,@MaxInvestmentPK,@valuedate,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,'Range Price'


                                                    SET @validate = 0
                                                end
												else if ( @ClientCode = '17' )
                                                    SET @validate = 0
                                                else
                                                begin
												    if isnull(@Validate, 0) = 1
												    begin
														set @MaxInvestmentPK = @MaxInvestmentPK - 1
														set @StatusSuccess = 0
													    set @success	= 13
													    set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> Exposure : ' + cast(@CInstrumentID as nvarchar(30)) + ' </br> Min Price : ' + cast(@MinPrice as nvarchar(30)) + ' </br> and Max Price : ' + cast(@MaxPrice as nvarchar(30))
												    end
												end

												if isnull(@Validate, 0) = 0 -- Validate Completed, Price yg digunakan ada diantara Range Price (Min Price ...Price... Max Price)
												begin
													-- Check Exposure
													select @ExposureType = ExposureType, @ExposureID = ExposureID, @AlertExposure = AlertExposure,
														@ExposurePercent = ExposurePercent, @MaxExposurePercent = MaxExposurePercent, @MinExposurePercent = MinExposurePercent
													from [dbo].[FCheckExposure] (@ValueDate, @CInstrumentPK, @CFundPK, @CAmount, @TrxType,1)
														
													-- AlertExposure = 1 => AlertMaxExposure = 0 and WarningMaxExposure = 1
													-- AlertExposure = 2 => AlertMaxExposure = 1 and WarningMaxExposure = 1
													-- AlertExposure = 3 => AlertMinExposure = 0 and WarningMinExposure = 1
													-- AlertExposure = 4 => AlertMinExposure = 1 and WarningMinExposure = 1
											
                                                --custom cam
                                                if (@ClientCode = '21' and isnull(@AlertExposure,0)  in (2,4))
                                                begin
                                                    set @Reason = ''
		                                            select @Reason = 'ExposureID : ' + @ExposureID + ', Parameter : ' + @CInstrumentID + ', Exposure Percent : ' +cast(@ExposurePercent as nvarchar) + ' %'


		                                            select @HighRiskMonitoringPK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		                                            set @HighRiskMonitoringPK = isnull(@HighRiskMonitoringPK,1)

		                                            insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected)
		                                            Select @HighRiskMonitoringPK,1,1,@Type,@MaxInvestmentPK,@valuedate,@Reason,@UsersID,@LastUpdate,@LastUpdate,0

                                                   set @AlertExposure = 1
                                                end
													
												--insert exposure
                                                begin

													exec dbo.[FundExposureForImportOMSEquity] @date = @valuedate,@FundPK = @CFundPK,@InstrumentPK = @CInstrumentPK,@Amount = @CAmount,@trxType = @TrxType, @InvPK = @MaxInvestmentPK

													--select @LastNo = isnull(max([No]) , 0) + 1
													--from CashierReference 
													--where [Type] = @Type and PeriodPK = @PeriodPK and substring(right(Reference, 4), 1, 2) = month(@ValueDate)   
										
													if @ClientCode = '20'
                                                    begin

                                                        select @MaxPK = max(ReferenceForNikkoPK) from ReferenceForNikko

                                                        set @MaxPK = isnull(@MaxPK,0) + 1

                                                        select @No = MAX(no) from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type
						
	                                                    set @No = isnull(@No,0) + 1

                                                        if exists (select * from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type)
                                                        begin
	                                                        update ReferenceForNikko set No = @No where month = month(@date) and year = year(@date) and Type = @Type
                                                        end
                                                        else 
                                                        begin
	                                                        insert into ReferenceForNikko
	                                                        select @MaxPK,@No,@Type,month(@date),year(@Date)
                                                        end

                                                        set @CurReference = cast(@No as nvarchar) + '/' + @Type + '/NSI/FM/' + cast(SUBSTRING(CONVERT(nvarchar(6),@Date, 112),5,2) as nvarchar) + '/' + substring(cast(year(@date) as nvarchar),3,2)

                                                    end
                                                    else
                                                    begin
														set @CurReference = dbo.FGetFundJournalReference(@ValueDate,'INV')

														Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
														substring(right(reference,4),1,2) = month(@ValueDate) 

														if exists(Select Top 1 * from cashierReference where Type = @type And PeriodPK = @PeriodPK 
														and substring(right(reference,4),1,2) = month(@ValueDate)  )    
														BEGIN
															Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
															and substring(right(reference,4),1,2) = month(@ValueDate) 
														END
														ELSE
														BEGIN
															Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
															Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
														END
													END
			
							

													insert into [ZTEMP_Import_Investment] (
														InvestmentPK, DealingPK, SettlementPK, HistoryPK, StatusInvestment, StatusDealing, StatusSettlement, ValueDate, MarketPK, PeriodPK, 
														InstructionDate, Reference, InstrumentTypePK, TrxType, TrxTypeID, CounterpartPK, InstrumentPK, FundPK, FundCashRefPK, 
														OrderPrice, Lot, LotInShare, RangePrice, AcqPrice, Volume, Amount, InterestPercent, BreakInterestPercent, AccruedInterest,
														SettlementDate, InvestmentNotes, DoneLot, DoneVolume, DonePrice, DoneAmount, Tenor, 
														CommissionPercent, LevyPercent, KPEIPercent, VATPercent, WHTPercent, OTCPercent, IncomeTaxSellPercent, 
														IncomeTaxInterestPercent, IncomeTaxGainPercent, CommissionAmount, LevyAmount, KPEIAmount, VATAmount, WHTAmount, OTCAmount, 
														IncomeTaxSellAmount, IncomeTaxInterestAmount, IncomeTaxGainAmount, TotalAmount, CurrencyRate, 
														AcqPrice1, AcqPrice2, AcqPrice3, AcqPrice4, AcqPrice5, SettlementMode, BoardType, InterestDaysType, InterestPaymentType, 
														PaymentModeOnMaturity, PriceMode, BitIsAmortized, Posted, Revised, EntryUsersID, EntryTime, LastUpdate,
														SelectedInvestment, SelectedDealing, SelectedSettlement, BankBranchPK, BankPK, 
														AcqVolume, AcqVolume1, AcqVolume2, AcqVolume3, AcqVolume4, AcqVolume5, AcqPrice6,
														AcqVolume6, AcqPrice7, AcqVolume7, AcqPrice8, AcqVolume8, AcqPrice9, AcqVolume9, TaxExpensePercent,YieldPercent,DoneAccruedInterest
													)
													select @MaxInvestmentPK as InvestmentPK, 0 as DealingPK, 0 as SettlementPK, 1 as HistoryPK,
														1 as StatusInvestment, 0 as StatusDealing, 0 as StatusSettlement, @ValueDate as ValueDate, @CMarketPK as MarketPK, @PeriodPK as PeriodPK, @ValueDate as InstructionDate,
														@CurReference,
														@CInstrumentTypePK as InstrumentTypePK, case when @CBuySell = 'B' then 1 else 2 end as TrxType, case when @CBuySell = 'B' then 'BUY' else 'SELL' end as TrxTypeID, 
														0 as CounterpartPK, @CInstrumentPK, @CFundPK, 0 as FundCashRefPK, cast(@CPrice as numeric(22,4)) as OrderPrice, cast(@CLot as numeric(22,4)) as Lot, 
														cast(@CLotInShare as numeric(22,4)) as LotInShare, 0 as RangePrice, 0 as AcqPrice, @CVolume as Volume, cast(@CAmount as numeric(22,4)) as Amount, 
														0 as InterestPercent, 0 as BreakInterestPercent, 0 as AccruedInterest, dbo.FWorkingDay(@ValueDate, 3) as SettlementDate, 'Import' as InvestmentNotes, 
														cast(@CLot as numeric(22,4)) as DoneLot, cast(@CVolume as numeric(22,4)) as DoneVolume, cast(@CPrice as numeric(22,4)) as DonePrice, cast(@CAmount as numeric(22,4)) as DoneAmount, 
														0 as Tenor, 0 as CommissionPercent, 0 as LevyPercent, 0 as KPEIPercent, 0 as VATPercent, 0 as WHTPercent, 0 as OTCPercent, 0 as IncomeTaxSellPercent, 
														0 as IncomeTaxInterestPercent, 0 as IncomeTaxGainPercent, 0 as CommissionAmount, 0 as LevyAmount, 0 as KPEIAmount, 0 as VATAmount, 0 as WHTAmount, 0 as OTCAmount, 
														0 as IncomeTaxSellAmount, 0 as IncomeTaxInterestAmount, 0 as IncomeTaxGainAmount, cast(@CAmount as numeric(22,4)) as TotalAmount,
														0 as CurrencyRate, 0 as AcqPrice1, 0 as AcqPrice2, 0 as AcqPrice3, 0 as AcqPrice4, 0 as AcqPrice5, 
														0 as SettlementMode, 0 as BoardType, 0 as InterestDaysType, 0 as InterestPaymentType, 
														0 as PaymentModeOnMaturity, 0 as PriceMode, 0 as BitIsAmortized, 0 as Posted, 0 as Revised, 
														@UsersID as EntryUsersID, @LastUpdate as EntryTime, @LastUpdate as LastUpdate,
														0 as SelectedInvestment, 0 as SelectedDealing, 0 as SelectedSettlement, 0 as BankBranchPK, 0 as BankPK, 
														0 as AcqVolume, 0 as AcqVolume1, 0 as AcqVolume2, 0 as AcqVolume3, 0 as AcqVolume4, 0 as AcqVolume5, 0 as AcqPrice6, 
														0 as AcqVolume6, 0 as AcqPrice7, 0 as AcqVolume7, 0 as AcqPrice8, 0 as AcqVolume8, 0 as AcqPrice9, 0 as AcqVolume9, 0 as TaxExpensePercent, 0 as YieldPercent, 0 as DoneAccruedInterest
												end

														
													
												end
											end
										end
									end
							
									if @CBuySell = 'S'
									begin

										-- Check OMS End Day Trails Fund Portfolio
										select @BitOMSEndDayTrailsFundPortfolio = 0
								
										if isnull(@BitOMSEndDayTrailsFundPortfolio, 0) = 1
										begin
					 
											set @success	= 15
											set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> End Day Trails Fund Portfolio Already Generated '
										end

										if isnull(@BitOMSEndDayTrailsFundPortfolio, 0) = 0 -- Belum Melakukan Generate End Day Trails FundPortfolio
										begin
											-- Check Price Exposure
											select @MinPrice = MinPrice, @MaxPrice = MaxPrice, @Validate = Validate
											from [dbo].[FCheckPriceExposure] (@ValueDate, @CInstrumentPK, @CFundPK, @CPrice)

									
												set @MaxInvestmentPK = @MaxInvestmentPK + 1	
											--custom cam
                                                if (@ClientCode = '21' and isnull(@Validate, 0) = 1)
                                                begin
                                                    
                                                    select @FundID = ID from Fund where status = 2 and FundPK = @CFundPK
                                                    set @FundID = isnull(@FundID,'')

                                                    set @Reason = 'EXPOSURE PRICE <br /><br /> FUND : ' + @FundID + ' <br /> INSTRUMENT : ' + @CInstrumentID +'  <br /> PRICE : ' + cast(@CPrice as nvarchar) + '  <br /> MIN : ' + cast(round(@MinPrice,0) as nvarchar) + '  <br /> MAX : ' + cast(round(@MaxPrice,0) as nvarchar)
                                                    
                                                    select @HighRiskMonitoringPK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		                                            set @HighRiskMonitoringPK = isnull(@HighRiskMonitoringPK,1)

                                                    insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,Description)
		                                            Select @HighRiskMonitoringPK,1,1,2,@MaxInvestmentPK,@date,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,'Range Price'


                                                    SET @validate = 0
                                                end
												else if ( @ClientCode = '17' )
                                                    SET @validate = 0
                                                else
                                                begin
												    if isnull(@Validate, 0) = 1
													begin
														set @StatusSuccess = 0
														set @MaxInvestmentPK = @MaxInvestmentPK - 1	
														set @success	= 16
														set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> Exposure : ' + cast(@CInstrumentID as nvarchar(30)) + ' </br> Min Price : ' + cast(@MinPrice as nvarchar(30)) + ' </br> and Max Price : ' + cast(@MaxPrice as nvarchar(30))
													end
												end

											

											if isnull(@Validate, 0) = 0 -- Validate Completed, Price yg digunakan ada diantara Range Price (Min Price ...Price... Max Price)
											begin
												-- Check Available Instrument
												set @TrxBuy		= 0
												set @TrxBuyType	= ''
												if (@ParamFundScheme = 1)
												begin
													select @BitAvailableInstrument = [dbo].[FCheckAvailableInstrumentParamFund] (@ValueDate, @CInstrumentPK, @CFundPK, @CVolume, @TrxBuy, @TrxBuyType, @CAmount, @MethodType)
												end
												else
												begin
													select @BitAvailableInstrument = [dbo].[FCheckAvailableInstrument] (@ValueDate, @CInstrumentPK, @CFundPK, @CVolume, @TrxBuy, @TrxBuyType, @CAmount, @MethodType)
												end
							
										
												if isnull(@BitAvailableInstrument, 0) = 1
												begin
							 
													set @success	= 17
													set @msg		= 'Import OMS Equity Canceled, </br> Can Not Process This Data! ' + ' </br> Short Sell : ' + cast(@CInstrumentID as nvarchar(30)) + ' </br>'
												end

												if isnull(@BitAvailableInstrument, 0) = 0 -- Instrument Available 
												begin											
													--select @LastNo = isnull(max([No]) , 0) + 1
													--from CashierReference 
													--where [Type] = @Type and PeriodPK = @PeriodPK and substring(right(Reference, 4), 1, 2) = month(@ValueDate)   

                                                    if @ClientCode = '20'
                                                    begin

                                                        select @MaxPK = max(ReferenceForNikkoPK) from ReferenceForNikko

                                                        set @MaxPK = isnull(@MaxPK,0) + 1

                                                        select @No = MAX(no) from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type
						
	                                                    set @No = isnull(@No,0) + 1

                                                        if exists (select * from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type)
                                                        begin
	                                                        update ReferenceForNikko set No = @No where month = month(@date) and year = year(@date) and Type = @Type
                                                        end
                                                        else 
                                                        begin
	                                                        insert into ReferenceForNikko
	                                                        select @MaxPK,@No,@Type,month(@date),year(@Date)
                                                        end

                                                        set @CurReference = cast(@No as nvarchar) + '/' + @Type + '/NSI/FM/' + cast(SUBSTRING(CONVERT(nvarchar(6),@Date, 112),5,2) as nvarchar) + '/' + substring(cast(year(@date) as nvarchar),3,2)

                                                    end
                                                    else
                                                    begin
                                                        set @CurReference = dbo.FGetFundJournalReference(@ValueDate,'INV')

													    Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
													    substring(right(reference,4),1,2) = month(@ValueDate) 

													    if exists(Select Top 1 * from cashierReference where Type = @type And PeriodPK = @PeriodPK 
													    and substring(right(reference,4),1,2) = month(@ValueDate)  )    
													    BEGIN
														    Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
														    and substring(right(reference,4),1,2) = month(@ValueDate) 
													    END
													    ELSE
													    BEGIN
														    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
														    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
													    END
                                                    END
													
					
													insert into [ZTEMP_Import_Investment] (
														InvestmentPK, DealingPK, SettlementPK, HistoryPK, StatusInvestment, StatusDealing, StatusSettlement, ValueDate, MarketPK, PeriodPK, 
														InstructionDate, Reference, InstrumentTypePK, TrxType, TrxTypeID, CounterpartPK, InstrumentPK, FundPK, FundCashRefPK, 
														OrderPrice, Lot, LotInShare, RangePrice, AcqPrice, Volume, Amount, InterestPercent, BreakInterestPercent, AccruedInterest,
														SettlementDate, InvestmentNotes, DoneLot, DoneVolume, DonePrice, DoneAmount, Tenor, 
														CommissionPercent, LevyPercent, KPEIPercent, VATPercent, WHTPercent, OTCPercent, IncomeTaxSellPercent, 
														IncomeTaxInterestPercent, IncomeTaxGainPercent, CommissionAmount, LevyAmount, KPEIAmount, VATAmount, WHTAmount, OTCAmount, 
														IncomeTaxSellAmount, IncomeTaxInterestAmount, IncomeTaxGainAmount, TotalAmount, CurrencyRate, 
														AcqPrice1, AcqPrice2, AcqPrice3, AcqPrice4, AcqPrice5, SettlementMode, BoardType, InterestDaysType, InterestPaymentType, 
														PaymentModeOnMaturity, PriceMode, BitIsAmortized, Posted, Revised, EntryUsersID, EntryTime, LastUpdate,
														SelectedInvestment, SelectedDealing, SelectedSettlement, BankBranchPK, BankPK, 
														AcqVolume, AcqVolume1, AcqVolume2, AcqVolume3, AcqVolume4, AcqVolume5, AcqPrice6,
														AcqVolume6, AcqPrice7, AcqVolume7, AcqPrice8, AcqVolume8, AcqPrice9, AcqVolume9, TaxExpensePercent,YieldPercent,DoneAccruedInterest
													)
													select @MaxInvestmentPK as InvestmentPK, 0 as DealingPK, 0 as SettlementPK, 1 as HistoryPK,
														1 as StatusInvestment, 0 as StatusDealing, 0 as StatusSettlement, @ValueDate as ValueDate, @CMarketPK as MarketPK, @PeriodPK as PeriodPK, @ValueDate as InstructionDate,
														@CurReference,
														@CInstrumentTypePK as InstrumentTypePK, case when @CBuySell = 'B' then 1 else 2 end as TrxType, case when @CBuySell = 'B' then 'BUY' else 'SELL' end as TrxTypeID, 
														0 as CounterpartPK, @CInstrumentPK, @CFundPK, 0 as FundCashRefPK, cast(@CPrice as numeric(22,4)) as OrderPrice, cast(@CLot as numeric(22,4)) as Lot, 
														cast(@CLotInShare as numeric(22,4)) as LotInShare, 0 as RangePrice, 0 as AcqPrice, @CVolume as Volume, cast(@CAmount as numeric(22,4)) as Amount, 
														0 as InterestPercent, 0 as BreakInterestPercent, 0 as AccruedInterest, dbo.FWorkingDay(@ValueDate, 3) as SettlementDate, 'Import' as InvestmentNotes, 
														cast(@CLot as numeric(22,4)) as DoneLot, cast(@CVolume as numeric(22,4)) as DoneVolume, cast(@CPrice as numeric(22,4)) as DonePrice, cast(@CAmount as numeric(22,4)) as DoneAmount, 
														0 as Tenor, 0 as CommissionPercent, 0 as LevyPercent, 0 as KPEIPercent, 0 as VATPercent, 0 as WHTPercent, 0 as OTCPercent, 0 as IncomeTaxSellPercent, 
														0 as IncomeTaxInterestPercent, 0 as IncomeTaxGainPercent, 0 as CommissionAmount, 0 as LevyAmount, 0 as KPEIAmount, 0 as VATAmount, 0 as WHTAmount, 0 as OTCAmount, 
														0 as IncomeTaxSellAmount, 0 as IncomeTaxInterestAmount, 0 as IncomeTaxGainAmount, cast(@CAmount as numeric(22,4)) as TotalAmount,
														0 as CurrencyRate, 0 as AcqPrice1, 0 as AcqPrice2, 0 as AcqPrice3, 0 as AcqPrice4, 0 as AcqPrice5, 
														0 as SettlementMode, 0 as BoardType, 0 as InterestDaysType, 0 as InterestPaymentType, 
														0 as PaymentModeOnMaturity, 0 as PriceMode, 0 as BitIsAmortized, 0 as Posted, 0 as Revised, 
														@UsersID as EntryUsersID, @LastUpdate as EntryTime, @LastUpdate as LastUpdate,
														0 as SelectedInvestment, 0 as SelectedDealing, 0 as SelectedSettlement, 0 as BankBranchPK, 0 as BankPK, 
														0 as AcqVolume, 0 as AcqVolume1, 0 as AcqVolume2, 0 as AcqVolume3, 0 as AcqVolume4, 0 as AcqVolume5, 0 as AcqPrice6, 
														0 as AcqVolume6, 0 as AcqPrice7, 0 as AcqVolume7, 0 as AcqPrice8, 0 as AcqVolume8, 0 as AcqPrice9, 0 as AcqVolume9, 0 as TaxExpensePercent, 0 as YieldPercent, 0 as DoneAccruedInterest
												end
											end									
										end
									end

							end
		
							set @LastRow = @MaxInvestmentPK

							FETCH NEXT FROM AB 
							INTO @CBuySell,@CInstrumentID,@CPrice,@CLot,@CVolume,@CAmount,@CMarketPK,@CInstrumentPK,@CInstrumentTypePK,@CLotInShare,@CFundPK,@CMethodType,@CBitContinueSave
						END;
 
					CLOSE AB; 
					DEALLOCATE AB;

					set @Counter = @Counter + 1

			end
			fetch next from curImport 
			into @Cp1
		end   
		close curImport
		deallocate curImport
	--if @LastRow > 0
	--begin
	--	update CashierReference set 
	--		Reference = cast(@LastRow as nvarchar(10)) + '/' + @Type  + '/' + replace(right(convert(nvarchar(8), @ValueDate, 3), 5) ,'/',''), 
	--		[No] = @LastNo + @LastRow 
	--	where [Type] = @Type and PeriodPK = @PeriodPK and substring(right(Reference, 4), 1, 2) = month(@ValueDate)    
	--end


if(@StatusSuccess = 1)
begin
	commit transaction
	set @msg = 'Import OMS Equity Success'
end
Else
begin
	rollback transaction
end
end
else
begin
	set @msg = 'Check Master Instrument'
end
end




select @msg as ResultMsg


--select * from [ZTEMP_Import_Investment] where valuedate = '2020-05-19'

--select * from ZTEMP_FUNDEXPOSURE
                            ";
                        cmd2.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd2.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd2.Parameters.AddWithValue("@UsersID", _userID);
                        cmd2.Parameters.AddWithValue("@Date", _now);
                        cmd2.Parameters.AddWithValue("@LastUpdate", _now);
                        if (Tools.ParamFundScheme)
                        {
                            cmd2.Parameters.AddWithValue("@ParamFundScheme", 1);
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@ParamFundScheme", 0);
                        }
                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            if (!dr2.HasRows)
                            {

                                _msg = "Import OMS Equity Canceled, import data not found!";
                            }
                            else
                            {
                                dr2.Read();
                                _msg = Convert.ToString(dr2["ResultMsg"]);
                            }
                        }
                    }
                }

                return _msg;
            }
            catch (Exception err)
            {
                return "Import OMS Equity Error : " + err.Message.ToString();
                throw err;
            }
        }


        private DataTable CreateDataTableFromOMSEquityTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BuySell";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Price";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F3";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F4";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F5";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F6";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F7";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F8";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F9";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F10";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F13";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F14";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F15";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F16";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F17";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F18";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F19";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F20";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F21";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F22";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F23";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F24";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F25";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F26";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F27";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F28";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F29";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F30";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F31";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F32";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F33";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F34";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F35";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F36";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F37";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F38";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F39";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F40";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F41";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F42";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F43";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F44";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F45";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F46";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F47";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F48";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F49";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F50";
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            odCmd.CommandText = "SELECT * FROM [FormatLoad$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                if (odRdr.HasRows)
                                {
                                    odRdr.Read();

                                    int _no = 1;
                                    string _val = string.Empty;
                                    do
                                    {
                                        dr = dt.NewRow();
                                        if (_no == 1) { _val = "Spare Fund"; } else { _val = "0"; }

                                        dr["BuySell"] = Convert.ToString(odRdr[0]);
                                        dr["InstrumentID"] = Convert.ToString(odRdr[1]);
                                        dr["Price"] = Convert.ToString(odRdr[2]);
                                        dr["F1"] = odRdr[3].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[3]);
                                        dr["F2"] = odRdr[4].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[4]);
                                        dr["F3"] = odRdr[5].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[5]);
                                        dr["F4"] = odRdr[6].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[6]);
                                        dr["F5"] = odRdr[7].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[7]);
                                        dr["F6"] = odRdr[8].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[8]);
                                        dr["F7"] = odRdr[9].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[9]);
                                        dr["F8"] = odRdr[10].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[10]);
                                        dr["F9"] = odRdr[11].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[11]);
                                        dr["F10"] = odRdr[12].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[12]);
                                        dr["F11"] = odRdr[13].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[13]);
                                        dr["F12"] = odRdr[14].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[14]);
                                        dr["F13"] = odRdr[15].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[15]);
                                        dr["F14"] = odRdr[16].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[16]);
                                        dr["F15"] = odRdr[17].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[17]);
                                        dr["F16"] = odRdr[18].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[18]);

                                        dr["F17"] = odRdr[19].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[19]);
                                        dr["F18"] = odRdr[20].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[20]);
                                        dr["F19"] = odRdr[21].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[21]);
                                        dr["F20"] = odRdr[22].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[22]);

                                        dr["F21"] = odRdr[23].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[23]);
                                        dr["F22"] = odRdr[24].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[24]);
                                        dr["F23"] = odRdr[25].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[25]);
                                        dr["F24"] = odRdr[26].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[26]);
                                        dr["F25"] = odRdr[27].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[27]);
                                        dr["F26"] = odRdr[28].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[28]);
                                        dr["F27"] = odRdr[29].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[29]);
                                        dr["F28"] = odRdr[30].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[30]);
                                        dr["F29"] = odRdr[31].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[31]);
                                        dr["F30"] = odRdr[32].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[32]);

                                        dr["F31"] = odRdr[33].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[33]);
                                        dr["F32"] = odRdr[34].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[34]);
                                        dr["F33"] = odRdr[35].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[35]);
                                        dr["F34"] = odRdr[36].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[36]);
                                        dr["F35"] = odRdr[37].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[37]);
                                        dr["F36"] = odRdr[38].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[38]);
                                        dr["F37"] = odRdr[39].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[39]);
                                        dr["F38"] = odRdr[40].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[40]);
                                        dr["F39"] = odRdr[41].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[41]);
                                        dr["F40"] = odRdr[42].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[42]);

                                        dr["F41"] = odRdr[43].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[43]);
                                        dr["F42"] = odRdr[44].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[44]);
                                        dr["F43"] = odRdr[45].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[45]);
                                        dr["F44"] = odRdr[46].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[46]);
                                        dr["F45"] = odRdr[47].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[47]);
                                        dr["F46"] = odRdr[48].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[48]);
                                        dr["F47"] = odRdr[49].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[49]);
                                        dr["F48"] = odRdr[50].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[50]);
                                        dr["F49"] = odRdr[51].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[51]);
                                        dr["F50"] = odRdr[52].Equals(DBNull.Value) == true ? _val : Convert.ToString(odRdr[52]);


                                        if (
                                            dr["BuySell"].Equals(DBNull.Value) != true &&
                                            dr["InstrumentID"].Equals(DBNull.Value) != true &&
                                            dr["Price"].Equals(DBNull.Value) != true
                                        ) { dt.Rows.Add(dr); _no++; }
                                    } while (odRdr.Read());
                                }
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public decimal OMSEquityGetNetPendingCash(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     


Declare  @CashInvestmentProjection table
(FundPK int,amount numeric(22,4), Description nvarchar(400))
Declare @MaxEndDayTrailsDate datetime
Declare @TrailsPK int


Select @MaxEndDayTrailsDate = max(ValueDate) from EndDayTrails where status = 2
and ValueDate <= @ValueDate and FundPK = @FundPK
	

	
Select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio
where ValueDate =(
	Select Max(ValueDate) from EndDayTrailsFundPortfolio where status = 2 and ValueDate < @ValueDate and FundPK = @FundPK
) and status = 2 and FundPK = @FundPK

IF(@MaxEndDayTrailsDate >= @ValueDate)
BEGIN
	SELECT 0 Amount
	RETURN	
END	

if @FundPK = 0
BEGIN
	SELECT 0 Amount
END
ELSE
BEGIN

	BEGIN
	
	--SELL
	insert into @CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'PENDING CASH SELL' 
	from investment 
	where  fundpk  = @FundPK and  SettlementDate > @MaxEndDayTrailsDate 
	AND SettlementDate > @ValueDate
	and statusInvestment  not in (3,4)
	and statusDealing  not in (3,4)
	and statusSettlement  not in (3,4)
	and TrxType = 2
	and OrderStatus = 'M'

	END
                        
	select sum(isnull(Amount,0)) Amount from @CashInvestmentProjection

END



                         ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     


Declare  @CashInvestmentProjection table
(FundPK int,amount numeric(22,4), Description nvarchar(400))
Declare @MaxEndDayTrailsDate datetime
Declare @TrailsPK int


Select @MaxEndDayTrailsDate = max(ValueDate) from EndDayTrails where status = 2
and ValueDate <= @ValueDate
	

	
Select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio
where ValueDate =(
	Select Max(ValueDate) from EndDayTrailsFundPortfolio where status = 2 and ValueDate < @ValueDate
) and status = 2

IF(@MaxEndDayTrailsDate >= @ValueDate)
BEGIN
	SELECT 0 Amount
	RETURN	
END	

if @FundPK = 0
BEGIN
	SELECT 0 Amount
END
ELSE
BEGIN

	BEGIN
	
	--SELL
	insert into @CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'PENDING CASH SELL' 
	from investment 
	where  fundpk  = @FundPK and  SettlementDate > @MaxEndDayTrailsDate 
	AND SettlementDate > @ValueDate
	and statusInvestment  not in (3,4)
	and statusDealing  not in (3,4)
	and statusSettlement  not in (3,4)
	and TrxType = 2
	and OrderStatus = 'M'

	END
                        
	select sum(isnull(Amount,0)) Amount from @CashInvestmentProjection

END



                         ";
                        }
                        
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
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
                            declare @Closeprice numeric(18,4)
                            declare @Minprice numeric(18,4)
                            declare @Maxprice numeric(18,4)
                            declare @parameter numeric(18,4)
                            declare @Reason nvarchar(max)
                            DECLARE @PK int
                            declare @FundID nvarchar(100)
                            declare @InstrumentID nvarchar(100)

		                    select @Closeprice = ClosePriceValue
		                    From ClosePrice A
			                where A.Date = (select max(date) from ClosePrice A where A.date <= @Date and A.InstrumentPK = @instrumentPK and A.status  = 2)
                            and A.status = 2 and A.InstrumentPK = @InstrumentPK
                            
                            set @ClosePrice = isnull(@ClosePrice,0)

                            select @Minprice = @Closeprice - (@Closeprice * (MinPricePercent/100)), @Maxprice = @Closeprice + (@Closeprice * (MaxPricePercent/100)) 
                            From RangePrice A 
                            where A.status  = 2 and  @Closeprice between MinPrice and MaxPrice
                            
                            set @MaxPrice = case when @MaxPrice = 0 or @MaxPrice is null then 0 else @maxPrice end

                            select @FundID = ID from Fund where status = 2 and FundPK = @FundPK
                            set @FundID = isnull(@FundID,'')

                            select @InstrumentID = ID from Instrument where status = 2 and InstrumentPK = @InstrumentPK
                            set @InstrumentID = isnull(@InstrumentID,'')

                            if @price not between @Minprice and @Maxprice
							begin
								set @Reason = 'EXPOSURE PRICE <br /><br /> FUND : ' + @FundID + ' <br /> INSTRUMENT : ' + @InstrumentID +'  <br /> PRICE : ' + cast(@price as nvarchar) + '  <br /> MIN : ' + cast(round(@Minprice,0) as nvarchar) + '  <br /> MAX : ' + cast(round(@Maxprice,0) as nvarchar)
							

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

                                insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,Description,SecurityEmail,EmailExpiredTime)
		                        Select @PK,1,1,2,@InvestmentPK,@date,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,'Range Price',@r,DATEADD(MINUTE," + Tools._EmailSessionTime + @",@lastUpdate)

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


        public int OMSEquity_CheckExposureFromImport()
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
                                declare @StatusExposure int
                                set @StatusExposure = 0

                                if exists(
	                                select * from ZTEMP_FUNDEXPOSURE_IMPORT
                                )
                                set @StatusExposure = 1

                                select @StatusExposure Result
                            ";

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

        public string InsertIntoInvestment()
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
                            
                            insert into Investment (
	                            InvestmentPK, DealingPK, SettlementPK, HistoryPK, StatusInvestment, StatusDealing, StatusSettlement, ValueDate, MarketPK, PeriodPK, 
	                            InstructionDate, Reference, InstrumentTypePK, TrxType, TrxTypeID, CounterpartPK, InstrumentPK, FundPK, FundCashRefPK, 
	                            OrderPrice, Lot, LotInShare, RangePrice, AcqPrice, Volume, Amount, InterestPercent, BreakInterestPercent, AccruedInterest,
	                            SettlementDate, InvestmentNotes, DoneLot, DoneVolume, DonePrice, DoneAmount, Tenor, 
	                            CommissionPercent, LevyPercent, KPEIPercent, VATPercent, WHTPercent, OTCPercent, IncomeTaxSellPercent, 
	                            IncomeTaxInterestPercent, IncomeTaxGainPercent, CommissionAmount, LevyAmount, KPEIAmount, VATAmount, WHTAmount, OTCAmount, 
	                            IncomeTaxSellAmount, IncomeTaxInterestAmount, IncomeTaxGainAmount, TotalAmount, CurrencyRate, 
	                            AcqPrice1, AcqPrice2, AcqPrice3, AcqPrice4, AcqPrice5, SettlementMode, BoardType, InterestDaysType, InterestPaymentType, 
	                            PaymentModeOnMaturity, PriceMode, BitIsAmortized, Posted, Revised, EntryUsersID, EntryTime, LastUpdate,
	                            SelectedInvestment, SelectedDealing, SelectedSettlement, BankBranchPK, BankPK, 
	                            AcqVolume, AcqVolume1, AcqVolume2, AcqVolume3, AcqVolume4, AcqVolume5, AcqPrice6,
	                            AcqVolume6, AcqPrice7, AcqVolume7, AcqPrice8, AcqVolume8, AcqPrice9, AcqVolume9, TaxExpensePercent,YieldPercent,DoneAccruedInterest,
								StatutoryType,BitRollOverInterest,BitBreakable,Category,MaturityDate,TrxBuy,AcqDate
                            )
                            select * from ZTEMP_Import_Investment

                           ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            return "Import Success";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        private DataTable CreateDataTableFromOMSEquityTempExcelFile_03(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BuySell";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Price";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F3";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F4";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F5";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F6";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F7";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F8";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F9";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F10";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F13";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F14";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F15";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F16";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F17";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F18";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F19";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F20";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F21";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F22";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F23";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F24";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F25";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F26";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F27";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F28";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F29";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F30";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F31";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F32";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F33";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F34";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F35";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F36";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F37";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F38";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F39";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F40";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F41";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F42";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F43";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F44";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F45";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F46";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F47";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F48";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F49";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "F50";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 1;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int lastColumn = worksheet.Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;
                        while (i <= worksheet.Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row)
                        {
                            dr = dt.NewRow();
                            dr["BuySell"] = worksheet.Cells[i, 1].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 1].Value.ToString();
                            dr["InstrumentID"] = worksheet.Cells[i, 2].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 2].Value.ToString();
                            dr["Price"] = worksheet.Cells[i, 3].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 3].Value.ToString();

                            if (4 <= lastColumn)
                            {
                                dr["F1"] = worksheet.Cells[i, 4].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 4].Value.ToString();
                            }

                            if (5 <= lastColumn)
                            {
                                dr["F2"] = worksheet.Cells[i, 5].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 5].Value.ToString();
                            }
                            if (6 <= lastColumn)
                            {
                                dr["F3"] = worksheet.Cells[i, 6].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 6].Value.ToString();
                            }
                            if (7 <= lastColumn)
                            {
                                dr["F4"] = worksheet.Cells[i, 7].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 7].Value.ToString();
                            }
                            if (8 <= lastColumn)
                            {
                                dr["F5"] = worksheet.Cells[i, 8].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 8].Value.ToString();
                            }
                            if (9 <= lastColumn)
                            {
                                dr["F6"] = worksheet.Cells[i, 9].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 9].Value.ToString();
                            }

                            if (10 <= lastColumn)
                            {
                                dr["F7"] = worksheet.Cells[i, 10].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 10].Value.ToString();
                            }
                            if (11 <= lastColumn)
                            {
                                dr["F8"] = worksheet.Cells[i, 11].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 11].Value.ToString();
                            }
                            if (12 <= lastColumn)
                            {
                                dr["F9"] = worksheet.Cells[i, 12].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 12].Value.ToString();
                            }
                            if (13 <= lastColumn)
                            {
                                dr["F10"] = worksheet.Cells[i, 13].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 13].Value.ToString();
                            }
                            if (14 <= lastColumn)
                            {
                                dr["F11"] = worksheet.Cells[i, 14].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 14].Value.ToString();
                            }
                            if (15 <= lastColumn)
                            {
                                dr["F12"] = worksheet.Cells[i, 15].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 15].Value.ToString();
                            }
                            if (16 <= lastColumn)
                            {
                                dr["F13"] = worksheet.Cells[i, 16].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 16].Value.ToString();
                            }
                            if (17 <= lastColumn)
                            {
                                dr["F14"] = worksheet.Cells[i, 17].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 17].Value.ToString();
                            }
                            if (18 <= lastColumn)
                            {
                                dr["F15"] = worksheet.Cells[i, 18].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 18].Value.ToString();
                            }
                            if (19 <= lastColumn)
                            {
                                dr["F16"] = worksheet.Cells[i, 19].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 19].Value.ToString();
                            }
                            if (20 <= lastColumn)
                            {
                                dr["F17"] = worksheet.Cells[i, 20].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 20].Value.ToString();
                            }
                            if (21 <= lastColumn)
                            {
                                dr["F18"] = worksheet.Cells[i, 21].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 21].Value.ToString();
                            }
                            if (22 <= lastColumn)
                            {
                                dr["F19"] = worksheet.Cells[i, 22].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 22].Value.ToString();
                            }
                            if (23 <= lastColumn)
                            {
                                dr["F20"] = worksheet.Cells[i, 23].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 23].Value.ToString();
                            }
                            if (24 <= lastColumn)
                            {
                                dr["F21"] = worksheet.Cells[i, 24].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 24].Value.ToString();
                            }
                            if (25 <= lastColumn)
                            {
                                dr["F22"] = worksheet.Cells[i, 25].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 25].Value.ToString();
                            }
                            if (26 <= lastColumn)
                            {
                                dr["F23"] = worksheet.Cells[i, 26].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 26].Value.ToString();
                            }
                            if (27 <= lastColumn)
                            {
                                dr["F24"] = worksheet.Cells[i, 27].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 27].Value.ToString();
                            }
                            if (28 <= lastColumn)
                            {
                                dr["F25"] = worksheet.Cells[i, 28].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 28].Value.ToString();
                            }
                            if (29 <= lastColumn)
                            {
                                dr["F26"] = worksheet.Cells[i, 29].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 29].Value.ToString();
                            }
                            if (30 <= lastColumn)
                            {
                                dr["F27"] = worksheet.Cells[i, 30].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 30].Value.ToString();
                            }
                            if (31 <= lastColumn)
                            {
                                dr["F28"] = worksheet.Cells[i, 31].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 31].Value.ToString();
                            }
                            if (32 <= lastColumn)
                            {
                                dr["F29"] = worksheet.Cells[i, 32].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 32].Value.ToString();
                            }
                            if (33 <= lastColumn)
                            {
                                dr["F30"] = worksheet.Cells[i, 33].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 33].Value.ToString();
                            }
                            if (34 <= lastColumn)
                            {
                                dr["F31"] = worksheet.Cells[i, 34].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 34].Value.ToString();
                            }
                            if (35 <= lastColumn)
                            {
                                dr["F32"] = worksheet.Cells[i, 35].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 35].Value.ToString();
                            }
                            if (36 <= lastColumn)
                            {
                                dr["F33"] = worksheet.Cells[i, 36].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 36].Value.ToString();
                            }
                            if (37 <= lastColumn)
                            {
                                dr["F34"] = worksheet.Cells[i, 37].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 37].Value.ToString();
                            }
                            if (38 <= lastColumn)
                            {
                                dr["F35"] = worksheet.Cells[i, 38].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 38].Value.ToString();
                            }
                            if (39 <= lastColumn)
                            {
                                dr["F36"] = worksheet.Cells[i, 39].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 39].Value.ToString();
                            }
                            if (40 <= lastColumn)
                            {
                                dr["F37"] = worksheet.Cells[i, 40].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 40].Value.ToString();
                            }
                            if (41 <= lastColumn)
                            {
                                dr["F38"] = worksheet.Cells[i, 41].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 41].Value.ToString();
                            }
                            if (42 <= lastColumn)
                            {
                                dr["F39"] = worksheet.Cells[i, 42].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 42].Value.ToString();
                            }
                            if (43 <= lastColumn)
                            {
                                dr["F40"] = worksheet.Cells[i, 43].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 43].Value.ToString();
                            }
                            if (44 <= lastColumn)
                            {
                                dr["F41"] = worksheet.Cells[i, 44].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 44].Value.ToString();
                            }
                            if (45 <= lastColumn)
                            {
                                dr["F42"] = worksheet.Cells[i, 45].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 45].Value.ToString();
                            }
                            if (46 <= lastColumn)
                            {
                                dr["F43"] = worksheet.Cells[i, 46].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 46].Value.ToString();
                            }
                            if (47 <= lastColumn)
                            {
                                dr["F44"] = worksheet.Cells[i, 47].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 47].Value.ToString();
                            }
                            if (48 <= lastColumn)
                            {
                                dr["F45"] = worksheet.Cells[i, 48].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 48].Value.ToString();
                            }
                            if (49 <= lastColumn)
                            {
                                dr["F46"] = worksheet.Cells[i, 49].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 49].Value.ToString();
                            }
                            if (50 <= lastColumn)
                            {
                                dr["F47"] = worksheet.Cells[i, 50].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 50].Value.ToString();
                            }
                            if (51 <= lastColumn)
                            {
                                dr["F48"] = worksheet.Cells[i, 51].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 51].Value.ToString();
                            }
                            if (52 <= lastColumn)
                            {
                                dr["F49"] = worksheet.Cells[i, 52].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 52].Value.ToString();
                            }
                            if (53 <= lastColumn)
                            {
                                dr["F50"] = worksheet.Cells[i, 53].Value.Equals(DBNull.Value) == true ? "" : worksheet.Cells[i, 53].Value.ToString();
                            }
                            dt.Rows.Add(dr);
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



    }
}