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
    public class OMSReksadanaReps
    {
        Host _host = new Host();

        private OMSReksadana setOMSReksadana(SqlDataReader dr)
        {
            OMSReksadana M_temp = new OMSReksadana();
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

        public List<OMSReksadana> OMSReksadana_PerFund(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSReksadana> L_OMSEquity = new List<OMSReksadana>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
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

    Create table #tmpOmsEquityPerFundBalance
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
	
    insert into #tmpOmsEquityPerFundBalance(
    Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent,MovementTThree,AfterTThree,AfterTThreeNAVPercent
    )       
  
    Select  B.ID,sum(MarketValue) CurrBalance, isnull(sum(MarketValue)/@AUM * 100,0) [CurrNAVPercent]
	,(case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0) Movement
	
	,sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)  AfterBalance
	
	,isnull((sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0)) / @AUM * 100,0) AfterNAVPercent,  
   
    isnull(F.Movement,0)  MovementTOne,  
   (sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)  AfterTOne,  
    isnull(((sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))+ isnull(F.Movement,0) ) / @AUM * 100,0) AfterTOneNAVPercent,  
   
    isnull(H.Movement,0)  MovementTTwo,  
       (sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  AfterTTwo,  
    isnull((   (sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)    + isnull(H.Movement,0) ) / @AUM * 100,0) AfterTTwoNAVPercent,

    isnull(N.Movement,0)  MovementTThree,  
       (sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0)  AfterTThree,  
    isnull(((sum(MarketValue) + (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End - A.ClosePrice) * sum(A.Balance) + isnull(D.Movement,0))  + isnull(F.Movement,0)   + isnull(H.Movement,0)  + isnull(N.Movement,0) ) / @AUM * 100,0) AfterTThreeNAVPercent    
    from [FundPosition] A  
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
    Group by B.ID,D.Movement,F.Movement,H.Movement,N.Movement,A.ClosePrice,A.InstrumentPK




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
    Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
    where ValueDate = @Date and FundPK = @FundPK  
    and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.InstrumentPK  
    )J on B.InstrumentPK = J.InstrumentPK  
    left join -- T1 from investment  
    (  
    Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
    where ValueDate = @DateT1 and FundPK = @FundPK  
    and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.InstrumentPK  
    )D on B.InstrumentPK = D.InstrumentPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
    where ValueDate = @DateT2 and FundPK = @FundPK  
    and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.InstrumentPK  
    )E on B.InstrumentPK = E.InstrumentPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when DoneVolume > 0 then DoneVolume * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) end else DoneAmount end * Case when TrxType in(1,3) then 1 else -1 end),0) Movement,B.InstrumentPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
    where ValueDate = @DateT3 and FundPK = @FundPK  
    and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.InstrumentPK  
    )I on B.InstrumentPK = I.InstrumentPK  

    where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
    and A.InstrumentTypePK = 1 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    and B.ID not in (
    select Name from #tmpOmsEquityPerFundBalance	)
    Group By B.ID,D.Movement,E.Movement,I.Movement,j.Movement

    Select * from #tmpOmsEquityPerFundBalance 
	where CurrBalance > 0 or Movement > 0 or
	MovementTOne > 0 or AfterTOne > 0 or
	MovementTTwo > 0 or AfterTTwo > 0 or
	MovementTThree > 0 or  AfterTThree > 0



  ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_OMSEquity.Add(setOMSReksadana(dr));
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

        public string OMSReksadana_ListStockForYahooFinance(int _fundPK, DateTime _date)
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

        public List<OMSExposureReksadana> OMSExposureReksadana_PerFund(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSExposureReksadana> L_model = new List<OMSExposureReksadana>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
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

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSExposureReksadana M_model = new OMSExposureReksadana();
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
        public List<OMSReksadanaByInstrument> OMSReksadanaByInstrument(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSReksadanaByInstrument> L_model = new List<OMSReksadanaByInstrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
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



-- AMBIL DARI POSISI DITAMBAH MOVEMENT                        	
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,C.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
A.Balance * dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK) CostValue,
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then 
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
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
where A.TrailsPK = @TrailsPK and F.Type = 1 and A.FundPK = @FundPK and A.status  = 2



-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
                        
Insert into #OMSEquityByAllInstrument(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select B.ID,C.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,B.LotInShare ,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 
and StatusSettlement <> 3 
and A.instrumentTypePK = 1
and FundPK = @FundPK 
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
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSReksadanaByInstrument M_model = new OMSReksadanaByInstrument();
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

        public List<OMSReksadanaBySector> OMSReksadanaBySector(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSReksadanaBySector> L_model = new List<OMSReksadanaBySector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
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
	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,G.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
A.Balance * dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK) CostValue,
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice, B.LotInShare,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
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
			
						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityBySector(InstrumentID,SectorID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,F.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1 end
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join SubSector C on B.SectorPK = C.SubSectorPK and C.Status = 2
left join Sector F on C.SectorPK = F.SectorPK and F.status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
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
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSReksadanaBySector M_model = new OMSReksadanaBySector();
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

        public decimal OMSReksadanaGetNetBuySell(DateTime _date, int _fundPK)
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

        public decimal OMSReksadanaGetDealingNetBuySellReksadana(DateTime _date, int _fundPK, int _counterpartPK)
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

        public decimal OMSReksadanaGetSettlementNetBuySellReksadana(DateTime _date, int _fundPK, int _counterpartPK)
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

        public decimal OMSReksadanaGetSettlementNetBuySellBond(DateTime _date, int _fundPK, int _counterpartPK)
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

        public decimal OMSReksadanaGetNetAvailableCash(DateTime _date, int _fundPK)
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
                     
Declare @valueDateT7 datetime

create table #CashInvestmentProjection
(FundPK int,amount numeric(22,4), Description nvarchar(400))
Declare @MaxEndDayTrailsDate datetime
Declare @TrailsPK int


Select @MaxEndDayTrailsDate = max(ValueDate) from EndDayTrails where status = 2
and ValueDate < @ValueDate
	

	
Select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio
where ValueDate =(
	Select Max(ValueDate) from EndDayTrailsFundPortfolio where status = 2 and ValueDate < @ValueDate
) and status = 2

if @FundPK = 0
BEGIN
	Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	BEGIN
	-- IMMEDIATE SUBS, CASH, AR JATOH TEMPO,BOND TD SETTLED, - AP >= ValueDate, REDEMPTION
	-- + jual Settled hari ini - Beli settled >= ValueDate

	-- CASH
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalance] (@ValueDate,2),'CASH AT BANK'

	--IMMEDIATE SUBS
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	and ValueDate >= @MaxEndDayTrailsDate

	--IMMEDIATE REDEMPTION
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	and ValueDate >= @MaxEndDayTrailsDate

	-- BOND AND TD Settled
	insert into #CashInvestmentProjection (FundPK,amount, Description)
	Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	where TrailsPK = @TrailsPK
	And MaturityDate = @ValueDate 


	-- AP TRANSAKSI DAN FEE 

    insert into #CashInvestmentProjection (FundPK,amount,Description)
    select @FundPK, 
    [dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,64) * -1,'ALL PAYABLE'


	-- AR TRANSAKSI DAN OTHERS AR
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,40),'ALL RECEIVABLE'


	-- AP TRANSAKSI HARI H
if 	@MaxEndDayTrailsDate > @ValueDate
BEGIN
        
    insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where  SettlementDate > @MaxEndDayTrailsDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
END
ELSE
BEGIN
       insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where  SettlementDate >= @ValueDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
END



	-- AR TRANSAKSI SETTLED PER VALUE DATE
	insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	from investment 
	where  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 2
	and OrderStatus = 'M'
	-- 

	END
	select sum(isnull(Amount,0)) Amount from #CashInvestmentProjection

END
ELSE 
BEGIN
	Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	BEGIN

	-- CASH
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDate,2,@FundPK),'CASH AT BANK'
		
	--IMMEDIATE SUBS
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	and ValueDate >= @MaxEndDayTrailsDate
	and FundPK = @FundPK

	--IMMEDIATE REDEMPTION
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	and ValueDate >= @MaxEndDayTrailsDate
	and FundPK = @FundPK

	-- BOND AND TD Settled
	insert into #CashInvestmentProjection (FundPK,amount, Description)
	Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	where TrailsPK = @TrailsPK
	And MaturityDate = @ValueDate 
	and FundPK = @fundPK

	-- AP TRANSAKSI DAN FEE 
    insert into #CashInvestmentProjection (FundPK,amount,Description)
    select @FundPK, 
    [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,64,@FundPK) * -1,'ALL PAYABLE'

	-- AR TRANSAKSI DAN OTHERS AR
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,40,@FundPK),'ALL RECEIVABLE'


	-- AP TRANSAKSI HARI H
if 	@MaxEndDayTrailsDate > @ValueDate
BEGIN
        
    insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where  SettlementDate > @MaxEndDayTrailsDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
    and FundPK = @FundPK
END
ELSE
BEGIN
       insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where  SettlementDate >= @ValueDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
 and FundPK = @FundPK
END

	-- AR TRANSAKSI SETTLED PER VALUE DATE
	insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	from investment 
	where  fundpk  = @FundPK and  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 2
	and OrderStatus = 'M'
	-- 



	END
                        
	select sum(isnull(Amount,0)) Amount from #CashInvestmentProjection
END


                         ";
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


        public List<OMSReksadanaCashProjection> OMSReksadanaGetNetAvailableCashDetail(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSReksadanaCashProjection> L_model = new List<OMSReksadanaCashProjection>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

Declare @valueDateT7 datetime

create table #CashInvestmentProjection
(FundPK int,amount numeric(22,4), Description nvarchar(400))
Declare @MaxEndDayTrailsDate datetime
Declare @TrailsPK int


Select @MaxEndDayTrailsDate = max(ValueDate) from EndDayTrails where status = 2
and ValueDate < @ValueDate
	

	
Select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio
where ValueDate =(
	Select Max(ValueDate) from EndDayTrailsFundPortfolio where status = 2 and ValueDate < @ValueDate
) and status = 2

if @FundPK = 0
BEGIN
	Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	BEGIN
	-- IMMEDIATE SUBS, CASH, AR JATOH TEMPO,BOND TD SETTLED, - AP >= ValueDate, REDEMPTION
	-- + jual Settled hari ini - Beli settled >= ValueDate

	-- CASH
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalance] (@ValueDate,2),'CASH AT BANK'

	--IMMEDIATE SUBS
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	and ValueDate >= @MaxEndDayTrailsDate

	--IMMEDIATE REDEMPTION
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	and ValueDate >= @MaxEndDayTrailsDate

	-- BOND AND TD Settled
	insert into #CashInvestmentProjection (FundPK,amount, Description)
	Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	where TrailsPK = @TrailsPK
	And MaturityDate = @ValueDate 


	-- AP TRANSAKSI DAN FEE 

    insert into #CashInvestmentProjection (FundPK,amount,Description)
    select @FundPK, 
    [dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,64) * -1,'ALL PAYABLE'





	-- AR TRANSAKSI DAN OTHERS AR
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,40),'ALL RECEIVABLE'


	-- AP TRANSAKSI HARI H
	insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where   SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1


	-- AR TRANSAKSI SETTLED PER VALUE DATE
	insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	from investment 
	where  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 2
	and OrderStatus = 'M'
	-- 

	END
	   select isnull(Amount,0) Amount, Description FSource from #CashInvestmentProjection
			                where amount <> 0

END
ELSE 
BEGIN
	Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	BEGIN

	-- CASH
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDate,2,@FundPK),'CASH AT BANK'
		
	--IMMEDIATE SUBS
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	and ValueDate >= @MaxEndDayTrailsDate
	and FundPK = @FundPK

	--IMMEDIATE REDEMPTION
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	and ValueDate >= @MaxEndDayTrailsDate
	and FundPK = @FundPK

	-- BOND AND TD Settled
	insert into #CashInvestmentProjection (FundPK,amount, Description)
	Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	where TrailsPK = @TrailsPK
	And MaturityDate = @ValueDate 
	and FundPK = @fundPK

	-- AP TRANSAKSI DAN FEE 
    insert into #CashInvestmentProjection (FundPK,amount,Description)
    select @FundPK, 
    [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,64,@FundPK) * -1,'ALL PAYABLE'

	-- AR TRANSAKSI DAN OTHERS AR
	insert into #CashInvestmentProjection (FundPK,amount,Description)
	select @FundPK, 
	[dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,40,@FundPK),'ALL RECEIVABLE'


	-- AP TRANSAKSI HARI H

if 	@MaxEndDayTrailsDate > @ValueDate
BEGIN
        
    insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where fundpk  = @FundPK and SettlementDate > @MaxEndDayTrailsDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
END
ELSE
BEGIN
       insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	from investment 
	where fundpk  = @FundPK and SettlementDate >= @ValueDate
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 1
END

	-- AR TRANSAKSI SETTLED PER VALUE DATE
	insert into #CashInvestmentProjection (FundPK,Amount,Description)
	select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	from investment 
	where  fundpk  = @FundPK and  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	and statusInvestment  <> 3 
	and statusDealing  <> 3
	and statusSettlement  <> 3
	and TrxType = 2
	and OrderStatus = 'M'
	-- 

	END
                        
	   select isnull(Amount,0) Amount, Description FSource from #CashInvestmentProjection
			                where amount <> 0
END


                        ";
                        cmd.Parameters.AddWithValue("@valueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSReksadanaCashProjection M_model = new OMSReksadanaCashProjection();
                                    M_model.FSource = Convert.ToString(dr["FSource"]);
                                    M_model.Amount = Convert.ToDecimal(dr["Amount"]);
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

        public decimal OMSReksadanaGetAUMYesterday(DateTime _date, int _fundPK)
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
        public List<OMSReksadanaByIndex> OMSReksadanaByIndex(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSReksadanaByIndex> L_model = new List<OMSReksadanaByIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
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
	
------ ADA POSISI DAN ADA TRANSAKSI
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,CostValue,ClosePrice,LastPrice,LotInShare,Status,Amount)
Select A.InstrumentID,C1.ID,
A.balance + isnull(D.Volume,0)
+  isnull(E.Volume,0) Volume
,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
A.Balance * dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK) CostValue,
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 
then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice, B.LotInShare
,Case when D.Volume <> 0 or E.Volume <> 0  then 1 else 0 end
,0
From FundPosition A
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
			
						
-- AMBIL DARI MOVEMENT YANG GA ADA DI POSISI AWAL
Insert into #OMSEquityByIndex(InstrumentID,IndexID,Balance,AvgPrice,ClosePrice,LastPrice,LotInShare,status,Amount)
Select B.ID,C1.ID,   
Case when A.TrxType = 1 then A.DoneVolume else A.DoneVolume * -1 end,
dbo.FGetLastAvgFromInvestment_OMSEquity(@Date,A.InstrumentPK,A.FundPK),
[dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) ClosePrice,    
case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) 
else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End LastPrice,B.LotInShare,1,
Case when A.TrxType = 1 then A.DoneAmount else A.DoneAmount * -1   end
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join InstrumentIndex C on B.InstrumentPK = C.InstrumentPK and C.Status = 2
left join [Index] C1 on C.IndexPK = C1.IndexPK and C1.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 1
and FundPK = @FundPK
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
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSReksadanaByIndex M_model = new OMSReksadanaByIndex();
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

        public int Validate_ApproveBySelectedDataOMSReksadana(Investment _investment)
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

        public int Validate_RejectBySelectedDataOMSReksadana(Investment _investment)
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

        public int Investment_ApproveOMSReksadanaBySelected(Investment _investment)
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
                        string _paramOrderStatus = "";
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

                        if (_investment.TrxType == 2)
                        {
                            _paramOrderStatus = ",OrderStatus = 'O' ";
                        }
                        else
                        {
                            _paramOrderStatus = "";
                        }


                        cmd.CommandText = @"
                        declare @investmentPK int
                        declare @historyPK int
                        declare @DealingPK int
                        declare @Notes nvarchar(500)
                        declare @OrderPrice numeric(22,4)
                        declare @DoneVolume numeric(22,4)
                        declare @Amount numeric(22,4)
                        declare @AccruedInterest numeric(22,0)

                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = '' and Status = 2   
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)   
                        Select @Time,'InvestmentInstruction_RejectOMSEquityBySelected','Investment',InvestmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Investment where ValueDate between @DateFrom and @DateTo and statusInvestment = 1 " + _paramInvestmentPK + "  and  InstrumentTypePK = @InstrumentTypePK and TrxType = @TrxType " + _paramFund +

                        @"DECLARE A CURSOR FOR 
	                            Select InvestmentPK,DealingPK,HistoryPK,InvestmentNotes,OrderPrice,DoneVolume,Amount,AccruedInterest From investment 
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom " + _paramInvestmentPK + " and  InstrumentTypePK = @InstrumentTypePK and TrxType = @TrxType " + _paramFund +

                        @"Open A
                        Fetch Next From A
                        Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@DoneVolume,@Amount,@AccruedInterest

                        While @@FETCH_STATUS = 0
                        BEGIN
                        Select @DealingPK = max(DealingPK) + 1 From investment
                        if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                        update Investment set DealingPK = @DealingPK, statusInvestment = 2, statusDealing = 1,InvestmentNotes=@Notes,DonePrice=@OrderPrice" + _paramOrderStatus + @", DoneVolume=@DoneVolume,DoneAmount=@Amount,BoardType = 1,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,EntryDealingID = @ApprovedUsersID,EntryDealingTime = @ApprovedTime ,LastUpdate=@LastUpdate
                        where InvestmentPK = @InvestmentPK
                        Fetch next From A Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@DoneVolume,@Amount,@AccruedInterest
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

        public int Investment_RejectOMSReksadanaBySelected(Investment _investment)
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

                        cmd.CommandText = @"Update Investment set StatusInvestment  = 3,statusDealing = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime
                            where InstrumentTypePK = @InstrumentTypePK and TrxType = @TrxType " + _paramInvestmentPK + " and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) " + _paramFund +
                            " --Update Investment set SelectedInvestment  = 0";

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

                        cmd.CommandText = @"
                            
                        declare @ValidateAmount numeric (18,4)
                      
                        Declare @valueDateT7 datetime

                        create table #CashInvestmentProjection
                        (FundPK int,amount numeric(22,4), Description nvarchar(400))
                        Declare @MaxEndDayTrailsDate datetime
                        Declare @TrailsPK int


                        Select @MaxEndDayTrailsDate = max(ValueDate) from EndDayTrails where status = 2
                        and ValueDate < @ValueDate
	

	
                        Select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio
                        where ValueDate =(
	                        Select Max(ValueDate) from EndDayTrailsFundPortfolio where status = 2 and ValueDate < @ValueDate
                        ) and status = 2

                        if @FundPK = 0
                        BEGIN
	                        Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	                        BEGIN
	                        -- IMMEDIATE SUBS, CASH, AR JATOH TEMPO,BOND TD SETTLED, - AP >= ValueDate, REDEMPTION
	                        -- + jual Settled hari ini - Beli settled >= ValueDate

	                        -- CASH
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        select @FundPK, 
	                        [dbo].[FGetGroupAccountFundJournalBalance] (@ValueDate,2),'CASH AT BANK'

	                        --IMMEDIATE SUBS
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	                        and ValueDate >= @MaxEndDayTrailsDate

	                        --IMMEDIATE REDEMPTION
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	                        and ValueDate >= @MaxEndDayTrailsDate

	                        -- BOND AND TD Settled
	                        insert into #CashInvestmentProjection (FundPK,amount, Description)
	                        Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	                        where TrailsPK = @TrailsPK
	                        And MaturityDate = @ValueDate 


	                        -- AP TRANSAKSI DAN FEE 

                            insert into #CashInvestmentProjection (FundPK,amount,Description)
                            select @FundPK, 
                            [dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,64) * -1,'ALL PAYABLE'


	                        -- AR TRANSAKSI DAN OTHERS AR
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        select @FundPK, 
	                        [dbo].[FGetGroupAccountFundJournalBalance] (@ValueDateT7,40),'ALL RECEIVABLE'


	                        -- AP TRANSAKSI HARI H
                        if 	@MaxEndDayTrailsDate > @ValueDate
                        BEGIN
        
                            insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	                        from investment 
	                        where  SettlementDate > @MaxEndDayTrailsDate
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 1
                        END
                        ELSE
                        BEGIN
                               insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	                        from investment 
	                        where  SettlementDate >= @ValueDate
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 1
                        END



	                        -- AR TRANSAKSI SETTLED PER VALUE DATE
	                        insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	                        from investment 
	                        where  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 2
	                        and OrderStatus = 'M'
	                        -- 

	                        END
	                        select sum(isnull(Amount,0)) Amount from #CashInvestmentProjection

                        END
                        ELSE 
                        BEGIN
	                        Set @ValueDateT7 = dbo.fworkingday(@ValueDate,7)
	                        BEGIN

	                        -- CASH
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        select @FundPK, 
	                        [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDate,2,@FundPK),'CASH AT BANK'
		
	                        --IMMEDIATE SUBS
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        Select @FundPK,sum(totalCashAmount),'IMMEDIATE SUBSCRIPTION' from ClientSubscription where status not in (3,4)and BitImmediateTransaction = 1
	                        and ValueDate >= @MaxEndDayTrailsDate
	                        and FundPK = @FundPK

	                        --IMMEDIATE REDEMPTION
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        Select @FundPK,sum(totalCashAmount) * -1,'REDEMPTION' from ClientRedemption where status not in (3,4)
	                        and ValueDate >= @MaxEndDayTrailsDate
	                        and FundPK = @FundPK

	                        -- BOND AND TD Settled
	                        insert into #CashInvestmentProjection (FundPK,amount, Description)
	                        Select @FundPK,sum(MarketValue),'BOND AND TD MATURED' from FundPosition
	                        where TrailsPK = @TrailsPK
	                        And MaturityDate = @ValueDate 
	                        and FundPK = @fundPK

	                        -- AP TRANSAKSI DAN FEE 
                            insert into #CashInvestmentProjection (FundPK,amount,Description)
                            select @FundPK, 
                            [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,64,@FundPK) * -1,'ALL PAYABLE'

	                        -- AR TRANSAKSI DAN OTHERS AR
	                        insert into #CashInvestmentProjection (FundPK,amount,Description)
	                        select @FundPK, 
	                        [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@ValueDateT7,40,@FundPK),'ALL RECEIVABLE'


	                        -- AP TRANSAKSI HARI H
                        if 	@MaxEndDayTrailsDate > @ValueDate
                        BEGIN
        
                            insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	                        from investment 
	                        where  SettlementDate > @MaxEndDayTrailsDate
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 1
                            and FundPK = @FundPK
                        END
                        ELSE
                        BEGIN
                               insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount) * -1,'AP SETTLED >= VALUEDATE' 
	                        from investment 
	                        where  SettlementDate >= @ValueDate
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 1
                         and FundPK = @FundPK
                        END

	                        -- AR TRANSAKSI SETTLED PER VALUE DATE
	                        insert into #CashInvestmentProjection (FundPK,Amount,Description)
	                        select @FundPK,sum(totalamount),'AR SETTLED = VALUEDATE' 
	                        from investment 
	                        where  fundpk  = @FundPK and  SettlementDate > case when  @MaxEndDayTrailsDate > @ValueDate then @MaxEndDayTrailsDate else @ValueDate end
	                        and statusInvestment  <> 3 
	                        and statusDealing  <> 3
	                        and statusSettlement  <> 3
	                        and TrxType = 2
	                        and OrderStatus = 'M'
	                        -- 

	                        END
                        
	  
                        END


                        select @ValidateAmount = sum(isnull(Amount,0)) from #CashInvestmentProjection

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
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public NavProjection OMSReksadana_GenerateNavProjection(DateTime _date, string _fundID)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

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

                        cmd.CommandTimeout = 0;
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



Insert into #Exposure
(ExposureType,ExposureID,MaxExposurePercent,MinExposurePercent,MarketValue,ExposurePercent,AlertMinExposure,
AlertMaxExposure,WarningMinExposure,WarningMaxExposure)                     
Select ExposureType,
UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,(sum(MarketValue) + @Amount) MarketValue, (sum(MarketValue)+@Amount)/ @TotalMarketValue * 100 ExposurePercent,
case when (isnull(sum(MarketValue),0) + @Amount)/ @TotalMarketValue * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
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
                            
                            set @MaxPrice = case when @MaxPrice = 0 or @MaxPrice is null then 100000 else @maxPrice end
                            select round(@Minprice,0) MinPrice,round(@Maxprice,0) MaxPrice,case when @Price between round(@MinPrice,0) and round(@Maxprice,0) then 0 else 1 End Validate 

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

        public decimal Get_TotalLotForOMSReksadana(int _fundPK, DateTime _date, int _instrumentPK)
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

       

    }
}