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

namespace RFSRepository
{
    public class ExposureMonitoringReps
    {
        Host _host = new Host();

        public List<ExposureMonitoring> Exposure_PerFund(DateTime _date, int _exposureType, string _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ExposureMonitoring> L_model = new List<ExposureMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;

                        string _paramExposureType = "";

                        if (_exposureType == 4)
                        {
                            _paramExposureType = @"Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * case when D.InstrumentTypePK in (2,3) then
                            (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100 else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100 End) 
                            else (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End) end, 
                            0 ExposurePercent, D.Name ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
                            )C on B.InstrumentTypePK = C.Parameter
                            left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
                            where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )
	
                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'INSTRUMENT TYPE' ExposureType,
                            isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end ) * case when E.InstrumentTypePK in (2,3) then
                            (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100 else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100 End) 
                            else (case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End) end
                            ,0)
                            ,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Inner Join
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
                            )D on B.InstrumentTypePK = D.Parameter
                            left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
                            where A.ValueDate = @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )

                            --update closeprice

                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * case when D.InstrumentTypePK in (2,3) then G.ClosePriceValue/100 else G.ClosePriceValue end, 
                            0 ExposurePercent, D.Name ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
                            )C on B.InstrumentTypePK = C.Parameter
                            left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
                            where A.Date = dbo.FWorkingDay(@Date,-1) and A.FundPK = @FundPK and A.Status = 2
                            and B.InstrumentPK  in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )
	
                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'INSTRUMENT TYPE' ExposureType,
                            isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end ) * case when E.InstrumentTypePK in (2,3) then G.ClosePriceValue/100 else G.ClosePriceValue end
                            ,0)
                            ,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            Inner Join
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
                            )D on B.InstrumentTypePK = D.Parameter
                            left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
                            where A.ValueDate = @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                            and B.InstrumentPK in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            ) 
                             ---------------   CASH    ------------------------

                        create table #CashInvestmentProjection
                        (FundPK int,amount numeric(22,4))

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select FundPK,Amount from CashProjection where FundPK = @FundPK and ValueDate = dbo.FWorkingDay(@date,-1)

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (144) and fundpk = @FundPK  

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (145) and fundpk = @FundPK  

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (146) and fundpk = @FundPK  

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (167) and fundpk = @FundPK  

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (344) and fundpk = @FundPK  


                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(basedebit-basecredit) * -1 from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1 
                        where  ValueDate <= @date and FundJournalAccountPK in (336) and fundpk = @FundPK  

                        insert into #CashInvestmentProjection (FundPK,Amount)
                        select @FundPK,sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) from investment 
                        where  fundpk  = @FundPK and ValueDate =  dbo.FWorkingDay(@date,-3) and statusSettlement  = 2

                        insert into #CashInvestmentProjection (FundPK,Amount)
                        select @FundPK,sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) from investment 
                        where  fundpk  = @FundPK and ValueDate =  dbo.FWorkingDay(@date,-2) and statusSettlement  = 2

                        insert into #CashInvestmentProjection (FundPK,Amount)
                        select @FundPK,sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) from investment 
                        where  fundpk  = @FundPK and ValueDate =  dbo.FWorkingDay(@date,-1) and statusSettlement  = 2

                        insert into #CashInvestmentProjection (FundPK,Amount)
                        select FundPK,sum(TotalCashAmount) from ClientSubscription where ValueDate = dbo.FWorkingDay(@date,-1) and FundPK = @FundPK and status  = 2 and Posted  = 1
                        Group By FundPK

                        insert into #CashInvestmentProjection (FundPK,Amount)
                        select A.FundPK,sum((TotalUnitAmount * B.Nav )* -1) from ClientRedemption A left join CloseNav B
                        on A.valuedate = B.date and A.FundPK = B.FundPK and B.status = 2
                        where A.ValueDate = dbo.FWorkingDay(@date,-1) and A.FundPK = @FundPK and A.status  in (1,2)
                        Group By A.FundPK

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(case when TrxType = 1 then (DoneAmount) * -1 else (DoneAmount) * 1 end ) from investment where valuedate = @date and fundpk  = @FundPK and StatusDealing  in (2) and OrderStatus in ('M','P')
                        and TrxType  = 2

                        insert into #CashInvestmentProjection (FundPK,amount)
                        select @FundPK,sum(case when TrxType = 1 then (DoneAmount) * -1 else (DoneAmount) * 1 end ) from investment where valuedate = @date and fundpk  = @FundPK and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 
                        and TrxType  = 1


                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)

                        select C.ID FundID,'CASH' ExposureType,sum(isnull(Amount,0)) MarketValue,(sum(isnull(Amount,0))/AUM * 100) ExposurePercent,'CASH' ExposureID,
                        D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent from #CashInvestmentProjection A
                        left join CloseNAV B on A.FundPK = B.FundPK and B.status  = 2
                        left join Fund C on A.FundPK = C.FundPK and C.status  = 2
                        inner join 
                        (
                        Select FundPK,status,Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 11 and Status = 2 and FundPK = @FundPK 
                        ) D on A.FundPK = D.FundPK and D.status  = 2
                        where B.Date = dbo.FWorkingDay(@Date,-1)
                        Group By C.ID,AUM,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
              
                                
                               ";

                        }

                        else if (_exposureType == 5)
                        {
                            _paramExposureType = @"Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )C on C.Parameter = 0
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type = 1
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )


                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'EQUITY ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end   * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  
                        ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Inner Join
                        (

                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )D on D.Parameter = 0
                        where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type = 1
                        and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        --update closeprice

                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * G.ClosePriceValue ,0 ExposurePercent, B.ID ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )C on C.Parameter = 0
                        where A.Date = dbo.FWorkingDay(@Date,-1) and A.FundPK = @FundPK and A.Status = 2 and F.Type = 1
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )


                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'EQUITY ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end   * G.ClosePriceValue  
                        ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        Inner Join
                        (

                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )D on D.Parameter = 0
                        where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type = 1
                        and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        ) ";

                        }


                        else if (_exposureType == 6)
                        {
                            _paramExposureType = @" Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'CORPORATE BOND ALL' ExposureType,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else (dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100) End ,0 ExposurePercent, B.ID ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 6 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )C on C.Parameter = 0
                            where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type in (2)
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )


                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'CORPORATE BOND ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end   * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else (dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100) End  
                            ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Inner Join
                            (

                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 6 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )D on D.Parameter = 0
                            where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type in (2)
                            and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )
                            --update closeprice

                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'CORPORATE BOND ALL' ExposureType,A.Balance * (G.ClosePriceValue/100) ,0 ExposurePercent, B.ID ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 6 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )C on C.Parameter = 0
                            where A.Date = dbo.FWorkingDay(@Date,-1) and A.FundPK = @FundPK and A.Status = 2 and F.Type in (2)
                            and B.InstrumentPK in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )


                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'CORPORATE BOND ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end   * (G.ClosePriceValue/100)  
                            ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            Inner Join
                            (

                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 6 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )D on D.Parameter = 0
                            where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type in (2)
                            and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                            and B.InstrumentPK in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            ) ";

                        }

                        else if (_exposureType == 7)
                        {
                            _paramExposureType = @" Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'CORPORATE BOND ALL' ExposureType,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else (dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100) End ,0 ExposurePercent, B.ID ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 7 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )C on C.Parameter = 0
                            where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type in (5)
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )


                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'CORPORATE BOND ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end   * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then ([dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)/100) else (dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)/100) End  
                            ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Inner Join
                            (

                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 7 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )D on D.Parameter = 0
                            where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type in (5)
                            and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                            and B.InstrumentPK not in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )
                            --update closeprice

                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select A.FundID,'CORPORATE BOND ALL' ExposureType,A.Balance * (G.ClosePriceValue/100) ,0 ExposurePercent, B.ID ExposureID,  
                            C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            inner join 
                            (
                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 7 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )C on C.Parameter = 0
                            where A.Date = dbo.FWorkingDay(@Date,-1) and A.FundPK = @FundPK and A.Status = 2 and F.Type in (5)
                            and B.InstrumentPK in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            )


                            Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                            MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                            Select C.ID,'CORPORATE BOND ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                            else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                            else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                            else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                            end end end end   * (G.ClosePriceValue/100)  
                            ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                            From investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                            Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                            Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                            Inner Join
                            (

                            Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                            From FundExposure where Type = 7 and Status = 2 and FundPK = @FundPK and Parameter = 0
                            )D on D.Parameter = 0
                            where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type in (5)
                            and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                            and B.InstrumentPK in
                            ( 
                            select instrumentPK From UpdateClosePrice where status  = 2
                            ) ";

                        }
                        else
                        {
                            _paramExposureType = "";
                        }


                        cmd.CommandText = @"
                        Create table #OMSExposure
                        (
	                        FundID nvarchar(100),
	                        ExposureType nvarchar(100),
	                        ExposureID nvarchar(100),
	                        MarketValue numeric(22,4),
	                        ExposurePercent numeric(18,8),
	                        MinExposurePercent numeric(18,8),
	                        MaxExposurePercent numeric(18,8),
	                        WarningMinExposurePercent numeric(18,8),
	                        WarningMaxExposurePercent numeric(18,8)
	
                        )


                        Declare @TotalMarketValue numeric(26,6)

                        select @TotalMarketValue = aum From closeNav
                        where Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                        and status = 2

	                    set @TotalMarketValue = isnull(@TotalMarketValue,1) "

                        + _paramExposureType +

                        @" Select 
                        @TotalMarketValue AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(MarketValue) MarketValue, sum(MarketValue)/ @TotalMarketValue * 100 ExposurePercent,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
                        From #OMSExposure A
                        Group by ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
                        order by ExposureType                                                                         
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ExposureMonitoring M_model = new ExposureMonitoring();
                                    M_model.ExposureType = Convert.ToString(dr["ExposureType"]);
                                    M_model.ExposureID = Convert.ToString(dr["ExposureID"]);
                                    M_model.MaxExposurePercent = Convert.ToDecimal(dr["MaxExposurePercent"]);
                                    M_model.MinExposurePercent = Convert.ToDecimal(dr["MinExposurePercent"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.ExposurePercent = Convert.ToDecimal(dr["ExposurePercent"]);
                                    M_model.AlertMinExposure = dr["AlertMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinExposure"]);
                                    M_model.AlertMaxExposure = dr["AlertMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxExposure"]);
                                    M_model.AlertWarningMaxExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMinExposure"]);
                                    M_model.AlertWarningMinExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMaxExposure"]);
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


        public List<ExposureMonitoring> OMSExposureEquity_PerFundByEquity(DateTime _date, string _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ExposureMonitoring> L_model = new List<ExposureMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;

                        cmd.CommandText = @"
                        Create table #OMSExposure
                        (
                        	FundID nvarchar(100),
                        	ExposureType nvarchar(100),
                        	ExposureID nvarchar(100),
                        	MarketValue numeric(22,4),
                        	ExposurePercent numeric(18,8),
                        	MinExposurePercent numeric(18,8),
                        	MaxExposurePercent numeric(18,8),
                        	WarningMinExposurePercent numeric(18,8),
                        	WarningMaxExposurePercent numeric(18,8)
                        	
                        )
                        
                        
                        Declare @TotalMarketValue numeric(26,6)
                        
                        select @TotalMarketValue = aum From closeNav
                        where Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                        and status = 2
                        
                        set @TotalMarketValue = isnull(@TotalMarketValue,1)
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'PER SECTOR' ExposureType,A.Balance * dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK),0 ExposurePercent, D.Name ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        inner join 
                        (
                        	Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        	From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
                        )C on B.SectorPK = C.Parameter
                        left join Sector D on C.Parameter = D.SectorPK and D.Status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'PER SECTOR' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end  ) * dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
                        ,0),0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Inner Join
                        (
                        	Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        	From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK
                        )D on B.SectorPK = D.Parameter
                        left join Sector E on D.Parameter = E.SectorPK and E.Status = 2
                        where A.ValueDate = @Date and A.FundPK = @FundPK  and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'SECTOR ALL' ExposureType,A.Balance * dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK),0 ExposurePercent, D.Name ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        inner join 
                        (
                        	Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        	From FundExposure where Type = 3 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )C on C.Parameter = 0
                        left join Sector D on B.SectorPK = D.SectorPK and D.Status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End, 
                        0 ExposurePercent, D.Name ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
                        )C on B.InstrumentTypePK = C.Parameter
                        left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        	
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'INSTRUMENT TYPE' ExposureType,
                        isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end ) * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End
                        ,0)
                        ,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Inner Join
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
                        )D on B.InstrumentTypePK = D.Parameter
                        left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
                        where A.ValueDate = @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        --update closeprice
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'INSTRUMENT TYPE' ExposureType,A.Balance * G.ClosePriceValue, 
                        0 ExposurePercent, D.Name ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK 
                        )C on B.InstrumentTypePK = C.Parameter
                        left join InstrumentType D on C.Parameter = D.InstrumentTypePK and D.Status = 2
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2
                        and B.InstrumentPK  in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        	
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'INSTRUMENT TYPE' ExposureType,
                        isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end ) * G.ClosePriceValue
                        ,0)
                        ,0,E.Name,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        Inner Join
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 4 and Status = 2 and FundPK = @FundPK
                        )D on B.InstrumentTypePK = D.Parameter
                        left join InstrumentType E on D.Parameter = E.InstrumentTypePK and E.Status = 2
                        where A.ValueDate = @Date and A.FundPK = @FundPK and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End ,0 ExposurePercent, B.ID ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )C on C.Parameter = 0
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type = 1
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'EQUITY ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end   * case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK) else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  
                        ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Inner Join
                        (
                        
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )D on D.Parameter = 0
                        where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type = 1
                        and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                        and B.InstrumentPK not in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        --update closeprice
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'EQUITY ALL' ExposureType,A.Balance * G.ClosePriceValue ,0 ExposurePercent, B.ID ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        inner join 
                        (
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )C on C.Parameter = 0
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type = 1
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'EQUITY ALL' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end   * G.ClosePriceValue  
                        ),0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Left Join UpdateClosePrice G on B.InstrumentPK = G.InstrumentPK and G.status = 2
                        Inner Join
                        (
                        
                        Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK and Parameter = 0
                        )D on D.Parameter = 0
                        where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type = 1
                        and A.StatusInvestment <> 3 and A.StatusDealing <> 3
                        and B.InstrumentPK in
                        ( 
                        select instrumentPK From UpdateClosePrice where status  = 2
                        )
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select A.FundID,'PER EQUITY' ExposureType,A.Balance * dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK),0 ExposurePercent, B.ID ExposureID,  
                        C.MinExposurePercent,c.MaxExposurePercent,c.WarningMinExposurePercent,c.WarningMaxExposurePercent
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        inner join 
                        (
                        	Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        	From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
                        )C on B.InstrumentPK = C.Parameter 
                        where A.Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK and A.Status = 2 and F.Type = 1
                        
                        
                        Insert into #OMSExposure (FundID,ExposureType,MarketValue,ExposurePercent,ExposureID,
                        MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent)
                        Select C.ID,'PER EQUITY' ExposureType,isnull((Case when A.TrxType = 1 and A.StatusDealing in (0,1) then A.Volume 
                        else case when A.TrxType = 2 and A.StatusDealing in (0,1) then A.Volume * -1
                        else case when A.TrxType = 1 and A.StatusDealing = 2 then A.DoneVolume
                        else case when A.TrxType = 2 and A.StatusDealing = 2 then A.DoneVolume * -1
                        end end end end  ) * dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) 
                        ,0),0,B.ID,D.MinExposurePercent,D.MaxExposurePercent,D.WarningMinExposurePercent,D.WarningMaxExposurePercent
                        From investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Left Join Fund C on A.FundPK = C.FundPk and C.status = 2
                        Left Join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.Status = 2
                        Inner Join
                        (
                        	Select Parameter,MinExposurePercent,MaxExposurePercent, WarningMaxExposurePercent,WarningMinExposurePercent 
                        	From FundExposure where Type = 5 and Status = 2 and FundPK = @FundPK 
                        )D on B.InstrumentPK = D.Parameter
                        where A.ValueDate = @Date and A.FundPK = @FundPK and F.Type= 1 and A.StatusDealing <> 3 and A.StatusInvestment <> 3
                        
                        
                        Select 
                        @TotalMarketValue AUMPrevDay,ExposureType,UPPER(ExposureID) ExposureID,MaxExposurePercent,MinExposurePercent,sum(MarketValue) MarketValue, sum(MarketValue)/ @TotalMarketValue * 100 ExposurePercent,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 <= MinExposurePercent then 1 else 0 end AlertMinExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 >= MaxExposurePercent then 1 else 0 end AlertMaxExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 <= WarningMinExposurePercent then 1 else 0 end WarningMinExposure,
                        case when sum(MarketValue)/ @TotalMarketValue * 100 >= WarningMaxExposurePercent then 1 else 0 end WarningMaxExposure
                        From #OMSExposure A
                        Group by ExposureID,ExposureType,MinExposurePercent,MaxExposurePercent,WarningMinExposurePercent,WarningMaxExposurePercent
                        order by ExposureType                                                                         
                        ";


                      
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ExposureMonitoring M_model = new ExposureMonitoring();
                                    M_model.ExposureType = Convert.ToString(dr["ExposureType"]);
                                    M_model.ExposureID = Convert.ToString(dr["ExposureID"]);
                                    M_model.MaxExposurePercent = Convert.ToDecimal(dr["MaxExposurePercent"]);
                                    M_model.MinExposurePercent = Convert.ToDecimal(dr["MinExposurePercent"]);
                                    M_model.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
                                    M_model.ExposurePercent = Convert.ToDecimal(dr["ExposurePercent"]);
                                    M_model.AlertMinExposure = dr["AlertMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinExposure"]);
                                    M_model.AlertMaxExposure = dr["AlertMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxExposure"]);
                                    M_model.AlertWarningMaxExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMinExposure"]);
                                    M_model.AlertWarningMinExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMaxExposure"]);
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



        public List<ExposureMonitoring> Get_ExposureName()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ExposureMonitoring> L_ExposureMonitoring = new List<ExposureMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        
                            cmd.CommandText = @"  
                            select Code Exposure,DescOne ExposureName from MasterValue where ID = 'ExposureType' and status in (1,2)";
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ExposureMonitoring.Add(setExposureMonitoring(dr));
                                }
                            }
                            return L_ExposureMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private ExposureMonitoring setExposureMonitoring(SqlDataReader dr)
        {
            ExposureMonitoring M_ExposureMonitoring = new ExposureMonitoring();
            M_ExposureMonitoring.Exposure = Convert.ToInt32(dr["Exposure"]);
            M_ExposureMonitoring.ExposureName = Convert.ToString(dr["ExposureName"]);
            return M_ExposureMonitoring;
        }



		public void Generate_ExposureMonitoringByDateByFundPK(DateTime _date, string _fundPK, string _usersID)
		{
			try
			{
				DateTime _dateTimeNow = DateTime.Now;
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<DataExposureMonitoring> L_ExposureMonitoring = new List<DataExposureMonitoring>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						string _paramFund = "";
						string _paramSplitByFund = "";

						if (_fundPK == "0")
						{
							_paramFund = "";
						}
						else
						{
							_paramFund = " and A.FundPK =  " + _fundPK;
						}


						if (Tools.ParamFundScheme)
						{
							_paramSplitByFund = " and FundPK = @FundPK ";
						}
						else
						{
							_paramSplitByFund = "";
						}


						cmd.CommandTimeout = 0;
						cmd.CommandText = @"


declare @XFundPK int

--declare @date date
--declare @ClientCode nvarchar(20)
--declare @UsersID nvarchar(20)
--declare @Time datetime

--set @date = '2020-09-18'
--set @ClientCode = '21'
--set @XFundPK = 5
--set @UsersID = 'admin'
--set @Time = getdate()

declare @InstrumentPK int
declare @Amount numeric(22,4)
declare @paramBondRating int

set @InstrumentPK = 0
set @Amount = 0
set @XFundPK = " + _fundPK + @"

--setup variable
begin
	Declare @PeriodPK int

	select @PeriodPK = PeriodPK from Period where @Date between DateFrom and DateTo and status = 2

	Declare @TrailsPK int
	Declare @MaxDateEndDayFP datetime
	Declare @TotalMarketValue numeric(26,6)
	Declare @TotalMarketValueAllFund numeric(26,6)
	DECLARE @QInstrumentPK int
	DECLARE @PInstrumentPK int

	DECLARE @EInstrumentPK INT
	DECLARE @EAmount NUMERIC(22,4)
	DECLARE @EFundPK INT

	DECLARE @WInstrumentPK INT
	DECLARE @WAmount NUMERIC(22,4)

	DECLARE @CFundPK INT
	DECLARE @CType INT
	DECLARE @CParameter INT
	DECLARE @CMinExp NUMERIC(8,4)
	DECLARE @CMaxExp NUMERIC(8,4)
	DECLARE @CWarningMinExp NUMERIC(8,4)
	DECLARE @CWarningMaxExp NUMERIC(8,4)
	DECLARE @CMinVal NUMERIC(22,4)
	DECLARE @CMaxVal NUMERIC(22,4)
	DECLARE @CWarningMinVal NUMERIC(8,4)
	DECLARE @CWarningMaxVal NUMERIC(8,4)

	DECLARE @TotalInvestmentAllFundForCounterpartExposure numeric(26,6)
	Declare @FundPK int
	
	declare @TotalDirectInvestment numeric(26,6)
	declare @TotalLandAndProperty numeric(26,6)
	declare @yesterday date

	DECLARE @Exposure TABLE
	(
	FundPK INT,
	Exposure INT,
	ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
	Parameter INT,
	ParameterDesc nvarchar(100) COLLATE DATABASE_DEFAULT,

	MarketValue numeric(30,4),
	ExposurePercent numeric(18,8),

	MinExposurePercent numeric(18,8),
	WarningMinExposure numeric(18,8),
	AlertWarningMinExposure BIT,
	AlertMinExposure bit,

	MaxExposurePercent numeric(18,8),
	WarningMaxExposure numeric(18,8),
	AlertWarningMaxExposure BIT,
	AlertMaxExposure BIT,

	MinValue NUMERIC(22,4),
	WarningMinValue NUMERIC(22,4),
	AlertWarningMinValue BIT,
	AlertMinValue BIT,

	MaxValue NUMERIC(22,4),
	WarningMaxValue NUMERIC(22,4),
	AlertWarningMaxValue BIT,
	AlertMaxValue bit,
	Behavior nvarchar(100)

	)


	DECLARE @InvestmentPosition TABLE
	(
		FundPK INT,
		InstrumentPK INT,
		Amount NUMERIC(22,4)
	)

	DECLARE @InvestmentPrice TABLE
	(
		InstrumentPK INT,
		Price NUMERIC(22,4)
	)

	DECLARE @InvestmentPositionALLFund TABLE
	(
		FundPK int,
		InstrumentPK INT,
		Amount NUMERIC(22,4)
	)

	Declare @PositionForExp1 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp2 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp3 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp4 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp5 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp9 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp10 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp13 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	DECLARE @InstrumentIndex TABLE
	(
		InstrumentPK INT,
		[IndexPK] int
	)

	Declare @PositionForExp14 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp16 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp18 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp19 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(32,4),
		ExposurePercent NUMERIC(10,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp20 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)


	Declare @PositionForExp22 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)


	Declare @PositionForExp23 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp24 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp25 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp26 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp27 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp28 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(30,4),
		AUM numeric(30,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp29 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

	Declare @PositionForExp31 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)



	Declare @PositionForExp32 TABLE
	(
		FundPK INT,
		Exposure INT,
		ExposureDesc NVARCHAR(200),
		Parameter INT,
		ParameterDesc NVARCHAR(200),
		InstrumentPK INT,
		InstrumentID NVARCHAR(100),
		MarketValue NUMERIC(22,4),
		AUM numeric(22,4),
		ExposurePercent NUMERIC(8,4),
		Behavior nvarchar(100)
	)

end

 --type di fundexposure 
-- 1 -> | INSTRUMENT TYPE GROUP | DONE
-- 2 | BOND | DONE
-- 3 | SECTOR | DONE
-- 4 | INSTRUMENT TYPE | DONE
-- 5 | EQUITY | DONE
-- 9 | ALL FUND PER BANK | DONE
-- 10 | PER FUND PER BANK | DONE
-- 13 | ISSUER | DONE
-- 14 | INDEX | DONE
-- 15 | SYARIAH ONLY - BELUM
-- 16 | TOTAL PORTFOLIO | DONE
-- 17 | ALL FUND EQUITY < Market CAP - BELUM
-- 18 | ISSUER ALL FUND | DONE

-- 20 | COUNTERPART EXPOSURE
-- 22 | DIRECT INVESTMENT
-- 23 | LAND AND PROPERTY

-- 24 | CAMEL SCORE BANK PER FUND | 
-- 25 | BOND RATING | DONE
-- 26 | TOTAL FOREIGN PORTFOLIO PER FUND |  DONE
-- 27 | KIK EBA PER COUNTERPART | DONE
-- 28 | INVESTMENT OTHER THAN DEPOSIT | DONE
-- 29 | AFFILIATED INVESTMENT | DONE
-- 30 | TOTAL AUM ALL FUND | DONE
-- 31 | TOTAL HOLDING PER FUND | DONE
-- 32 | CAPITAL CLASSIFICATION


select @TotalMarketValueAllFund = SUM(ISNULL(aum,0)) From closeNav
where Date = (
	 select max(date) from CloseNAV where date <= @Date AND status = 2
)
and status = 2 

SET @TotalMarketValueAllFund = ISNULL(@TotalMarketValueAllFund,1)

set @yesterday = dbo.FWorkingDay(@date,-1)

DECLARE Z CURSOR FOR 
Select distinct FundPK from FundExposure A  where status = 2 " + _paramFund + @"
--and FundPK = @XFundPK 

Open Z
Fetch Next From Z
Into @FundPK

While @@FETCH_STATUS = 0
Begin


--SETUP--
BEGIN


if (@ClientCode = '21')
BEGIN
	Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
	endDayTrailsFundPortfolio where 
	valuedate = 
	(
		Select max(ValueDate) from endDayTrailsFundPortfolio where
		valuedate < @Date  and status = 2  
		" + _paramSplitByFund + @"
		--and FundPK = @FundPK 
	)
	and status = 2  
	" + _paramSplitByFund + @"
	--and FundPK = @FundPK 
END
ELSE
BEGIN
	Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
	endDayTrailsFundPortfolio where 
	valuedate = 
	(
		Select max(ValueDate) from endDayTrailsFundPortfolio where
		valuedate <= @Date  and status = 2  
		" + _paramSplitByFund + @"
		--and FundPK = @FundPK 
	)
	and status = 2  
	" + _paramSplitByFund + @"
	--and FundPK = @FundPK 
END


select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date <= @Date
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 


set @TotalMarketValue = case when @TotalMarketValue = 0 then 1 else isnull(@TotalMarketValue,1) end

select @TotalInvestmentAllFundForCounterpartExposure = SUM(case when TrxType = 1 then ISNULL(DoneAmount,0) else ISNULL(DoneAmount,0) * -1 end)
FROM dbo.Investment A
LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
WHERE  ValueDate <= @Date  and A.PeriodPK = @PeriodPK and StatusSettlement = 2
AND C.GroupType in (1,2)

SET @TotalInvestmentAllFundForCounterpartExposure = ISNULL(@TotalInvestmentAllFundForCounterpartExposure,1)

--select @TotalDirectInvestment = sum(A.NetAmount - isnull(B.NetAmountSell,0)) from DirectInvestment A
--left join SellDirectInvestment B on A.DirectInvestmentPK = B.DirectInvestmentPK and B.status = 2
--where A.Status = 2 and Valuedate <= @Date and fundpk = @FundPK
--group by  A.FundPK,A.DirectInvestmentPK,A.ProjectName
--having sum(A.NetAmount - isnull(B.NetAmountSell,0)) > 0

set @TotalDirectInvestment = isnull(@TotalDirectInvestment,0)

--select @TotalLandAndProperty = sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) from LandAndProperty A
--where A.Status = 2 and BuyValueDate <= @Date and fundpk = @FundPK
--group by  A.FundPK,A.LandAndPropertyPK,A.Nama
--having sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) > 0

set @TotalLandAndProperty = isnull(@TotalDirectInvestment,0)


Declare Q Cursor For
	SELECT DISTINCT InstrumentPK FROM dbo.Investment A
	WHERE 
	 A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
	and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 
	-- PARAM DISINI
	AND A.FundPK = @FundPK 

Open Q
Fetch Next From Q
INTO @QInstrumentPK
While @@FETCH_STATUS = 0  
BEGIN

	INSERT INTO @InvestmentPrice
	        ( InstrumentPK, Price )
	SELECT A.InstrumentPK,ISNULL(ClosePriceValue,0) FROM ClosePrice A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
	WHERE A.status = 2 AND A.instrumentPK = @QInstrumentPK  and 
    date =
	(
		SELECT MAX(Date) FROM dbo.ClosePrice WHERE status = 2 AND instrumentPK = @QInstrumentPK AND date <= @Date
	)
    union all
	select InstrumentPK,1 from Instrument where InstrumentPK = @QInstrumentPK and InstrumentTypePK in (5,10)

	Fetch Next From Q
	INTO @QInstrumentPK
End	
Close Q
Deallocate Q

INSERT INTO @InvestmentPosition
        ( FundPK, InstrumentPK, Amount )
SELECT FundPK,A.InstrumentPK, SUM(ISNULL(CASE WHEN A.DoneVolume > 0 THEN A.DoneVolume * ISNULL(B.Price,A.DonePrice) * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END
ELSE A.DoneAmount * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END END,0)
/ case when A.InstrumentTypePK not in (1,4,5,6,16) then 100 else 1 end
) 
FROM Investment A
LEFT JOIN @InvestmentPrice B ON A.InstrumentPK = B.InstrumentPK 
WHERE  
 A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3  

-- PARAM DISINI
AND A.FundPK = @FundPK 

GROUP BY FundPK,A.InstrumentPK


INSERT INTO @InvestmentPositionALLFund
        ( FundPK,InstrumentPK, Amount )
SELECT A.FundPK,A.InstrumentPK, SUM(ISNULL(CASE WHEN A.DoneVolume > 0 THEN A.DoneVolume * ISNULL(B.Price,A.DonePrice) * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END
ELSE A.DoneAmount * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END END,0)
/ case when A.InstrumentTypePK not in (1,4,5,6,16) then 100 else 1 end
) 
FROM Investment A
LEFT JOIN @InvestmentPrice B ON A.InstrumentPK = B.InstrumentPK 
WHERE   A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3  
GROUP BY A.InstrumentPK,A.FundPK

END

	--1--
	BEGIN
	
	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 1
		)
	BEGIN
		INSERT INTO @PositionForExp1
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,1,ISNULL(E.DescOne,''),ISNULL(C.GroupType,0),ISNULL(D.DescOne,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end  = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON C.GroupType = D.Code AND D.id = 'InstrumentGroupType' AND D.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 1 AND E.ID = 'ExposureType' AND E.status IN (1,2)

			WHERE  Date = @MaxDateEndDayFP and A.Status = 2
			AND D.DescOne IS NOT NULL

			-- PARAM DISINI
	AND A.FundPK = @FundPK 

			GROUP BY D.DescOne,C.GroupType,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	END

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 1
		)
	BEGIN
		INSERT INTO @PositionForExp1
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		select FundPK,1,ISNULL(B.DescOne,''),A.Parameter,case when A.Parameter = 1 then 'EQUITY/SAHAM' when A.Parameter = 2 then 'BOND/FIXED INCOME' else 'TIME DEPOSIT' end,
		0,'MIN EXPOSURE',0,0,0,'' from FundExposure A
		LEFT JOIN dbo.MasterValue B ON B.Code = 1 AND B.ID = 'ExposureType' AND B.status IN (1,2)
		where FundPK = @FundPK and A.Type = 1 and MinExposurePercent > 0 and A.status in (1,2)
		and Parameter not in (
		select C.GroupType from FundPosition A
		left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
		left join InstrumentType C on case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end  = C.InstrumentTypePK and C.status in (1,2)
		where Date = @MaxDateEndDayFP and A.status  = 2 and A.FundPK = @FundPK
		)
	END



	END

	--2--
	BEGIN



	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 2
		)
	BEGIN
		INSERT INTO @PositionForExp2
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,2,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end  = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 2 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP 
			AND C.GroupType = 2 and A.Status = 2 --and B.InstrumentTypePK not in (2,12,13,14,15)
			-- PARAM DISINI
	AND A.FundPK = @FundPK 

			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	END



	END

	--3--
	BEGIN


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 3
		)
	BEGIN
		INSERT INTO @PositionForExp3
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,3,ISNULL(E.DescOne,''),ISNULL(C.SectorPK,0),ISNULL(D.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.SubSector C ON B.SectorPK =  C.SubSectorPK AND C.status IN (1,2)
			LEFT JOIN Sector D ON C.SectorPK = D.SectorPK AND D.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 3 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP  and A.Status = 2
			AND D.ID IS NOT NULL
			-- PARAM DISINI
	AND A.FundPK = @FundPK 
			GROUP BY D.ID,C.SectorPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	

	
	END


	END

	--4--
	BEGIN

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 4
		)
	BEGIN
		INSERT INTO @PositionForExp4
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,4,ISNULL(E.DescOne,''),ISNULL(B.InstrumentTypePK,0),ISNULL(C.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end  = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 4 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP  --and B.InstrumentTypePK not in (2,13,15)
			AND C.ID IS NOT NULL and A.Status = 2
			-- PARAM DISINI
	AND A.FundPK = @FundPK 
			GROUP BY C.ID,B.InstrumentTypePK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	
	END

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 4
		)
	BEGIN
		INSERT INTO @PositionForExp4
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)

		select FundPK,4,ISNULL(B.DescOne,''),A.Parameter,C.Name,
		0,'MIN EXPOSURE',0,0,0,'' from FundExposure A
		LEFT JOIN dbo.MasterValue B ON B.Code = 4 AND B.ID = 'ExposureType' AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON A.Parameter = C.InstrumentTypePK AND C.status IN (1,2)
		where FundPK = @FundPK and A.Type = 4 and MinExposurePercent > 0 and A.status in (1,2)
		and Parameter not in (
		select C.InstrumentTypePK from FundPosition A
		left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
		left join InstrumentType C on case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end  = C.InstrumentTypePK and C.status in (1,2)
		where Date = @MaxDateEndDayFP and A.status  = 2 and A.FundPK = @FundPK
		)

	END


	END

	--5--
	BEGIN

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 5
		)
	BEGIN
		INSERT INTO @PositionForExp5
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,5,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 5 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP --
			AND C.GroupType = 1 and A.Status = 2
			-- PARAM DISINI
	AND A.FundPK = @FundPK 
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	

	END


	END

	--10--
	BEGIN


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 10
		)
	BEGIN
		INSERT INTO @PositionForExp10
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,10,ISNULL(E.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 10 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP 
			AND C.GroupType = 3 and A.Status = 2
			AND ISNULL(G.ID,'') <> ''
			-- PARAM DISINI
	AND A.FundPK = @FundPK 
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,G.BankPK,G.ID

	
	END


	END

	--13--
	BEGIN


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 13
		)
	BEGIN
		INSERT INTO @PositionForExp13
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,13,ISNULL(E.DescOne,''),ISNULL(C.IssuerPK,0),ISNULL(C.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.Issuer C ON B.IssuerPK =  C.IssuerPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 13 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP 
			AND C.ID IS NOT NULL and A.Status = 2

			--PARAM DISINI
	AND FundPk = @FundPK 
			GROUP BY C.ID,C.IssuerPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	
	END


	END

	--14--
	BEGIN



	Declare P Cursor For
			SELECT DISTINCT InstrumentPK FROM dbo.FundPosition A WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP 
	Open P
	Fetch Next From P
	INTO @PInstrumentPK

	While @@FETCH_STATUS = 0  
	Begin
	
		INSERT INTO @InstrumentIndex
				( InstrumentPK, IndexPK )
		SELECT @InstrumentPK,IndexPK FROM dbo.InstrumentIndex WHERE Date = (
			SELECT MAX(Date) FROM dbo.InstrumentIndex WHERE status = 2 AND InstrumentPK = @PInstrumentPK
		)AND InstrumentPK = @InstrumentPK AND Status = 2 
	
		Fetch Next From P
		into @PInstrumentPK
	End	
	Close P
	Deallocate P


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 14
		)
	BEGIN
		INSERT INTO @PositionForExp14
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,14,ISNULL(E.DescOne,''),ISNULL(C.IndexPK,0),ISNULL(D.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN @InstrumentIndex C ON B.InstrumentPK =  C.InstrumentPK
			LEFT JOIN dbo.[Index] D ON C.IndexPK = D.IndexPK AND D.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 14 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP 
			AND D.ID IS NOT NULL and A.Status = 2

			--PARAM DISINI
	AND FundPk = @FundPK 

			GROUP BY D.ID,C.IndexPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK


	
	END


	END

	--16--
	BEGIN
	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 16
		)
	BEGIN
		INSERT INTO @PositionForExp16
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,16,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @MaxDateEndDayFP and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 16 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @MaxDateEndDayFP  --and B.InstrumentTypePK not in (2,13,15)
			AND B.ID IS NOT NULL and A.Status = 2

			--PARAM DISINI
	AND FundPk = @FundPK 

			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK
	

	END

	END

	--20--
	BEGIN

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 20
		)
	BEGIN
		INSERT INTO @PositionForExp20
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,20,ISNULL(E.DescOne,''),ISNULL(G.CounterpartPK,0),ISNULL(G.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.DoneAmount,0)) MarketValue
			,@TotalInvestmentAllFundForCounterpartExposure
			, SUM(ISNULL(A.DoneAmount,0)) /  @TotalInvestmentAllFundForCounterpartExposure * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @Date and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.Investment A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 20 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			LEFT JOIN Counterpart G ON A.CounterpartPK = G.CounterpartPK AND G.status IN (1,2)
			WHERE  ValueDate <= @Date and PeriodPK = @PeriodPK and StatusSettlement = 2
			AND C.GroupType in (1,2) 
			--PARAM DISINI
	AND FundPk = @FundPK 

			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,G.CounterpartPK,G.ID
		

	END


	END

	----22--
	--BEGIN

	--IF EXISTS(
	--	SELECT TOP 1 Type FROM dbo.FundExposure WHERE status = 2
	--	AND Type = 22
	--	)
	--BEGIN
	--	INSERT INTO @PositionForExp22
	--			( FundPK ,
	--			  Exposure ,
	--			  ExposureDesc,
	--			  Parameter ,
	--			  ParameterDesc ,
	--			  InstrumentPK,
	--			  InstrumentID ,
	--			  MarketValue ,
	--			  AUM ,
	--			  ExposurePercent,
	--			  Behavior
	--			)


	--		select C.FundPK,22,ISNULL(E.DescOne,''),C.DirectInvestmentPK,''
	--		,0,ISNULL(C.ProjectName ,'')
	--		,SUM(ISNULL(C.NetAmount,0)) MarketValue
	--		,@TotalMarketValue
	--		, SUM(ISNULL(C.NetAmount,0)) /  @TotalMarketValue * 100 ExposurePercent,''
	--		from (
	--		select A.FundPK,A.DirectInvestmentPK,A.ProjectName,sum(A.NetAmount - isnull(B.NetAmountSell,0)) NetAmount from DirectInvestment A
	--		left join SellDirectInvestment B on A.DirectInvestmentPK = B.DirectInvestmentPK and B.status = 2
	--		where A.Status = 2 and Valuedate <= @MaxDateEndDayFP and fundpk = @FundPK
	--		group by  A.FundPK,A.DirectInvestmentPK,A.ProjectName
	--		having sum(A.NetAmount - isnull(B.NetAmountSell,0)) > 0
	--		)C
	--		LEFT JOIN dbo.MasterValue E ON E.Code = 22 AND E.ID = 'ExposureType' AND E.status IN (1,2)

	--		-- PARAM DISINI
	--		AND C.FundPK = @FundPK 
	--		GROUP BY C.FundPK,E.DescOne,C.DirectInvestmentPK,C.ProjectName

	
	--END

	--END

	----23--
	--BEGIN

	--IF EXISTS(
	--	SELECT TOP 1 Type FROM dbo.FundExposure WHERE status = 2
	--	AND Type = 23
	--	)
	--BEGIN
	--	INSERT INTO @PositionForExp23
	--			( FundPK ,
	--			  Exposure ,
	--			  ExposureDesc,
	--			  Parameter ,
	--			  ParameterDesc ,
	--			  InstrumentPK,
	--			  InstrumentID ,
	--			  MarketValue ,
	--			  AUM ,
	--			  ExposurePercent,
	--			  Behavior
	--			)


	--		select C.FundPK,23,ISNULL(E.DescOne,''),C.LandAndPropertyPK,''
	--		,0,ISNULL(C.Nama ,'')
	--		,SUM(ISNULL(C.NetAmount,0)) MarketValue
	--		,@TotalMarketValue
	--		, SUM(ISNULL(C.NetAmount,0)) /  @TotalMarketValue * 100 ExposurePercent,''
	--		from (
	--		select A.FundPK,A.LandAndPropertyPK,A.Nama,sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) NetAmount from LandAndProperty A
	--		where A.Status = 2 and BuyValueDate <= @MaxDateEndDayFP and fundpk = @FundPK
	--		group by  A.FundPK,A.LandAndPropertyPK,A.Nama
	--		having sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) > 0
	--		)C
	--		LEFT JOIN dbo.MasterValue E ON E.Code = 23 AND E.ID = 'ExposureType' AND E.status IN (1,2)

	--		-- PARAM DISINI
	--		AND C.FundPK = @FundPK 
	--		GROUP BY C.FundPK,E.DescOne,C.LandAndPropertyPK,C.Nama

	
	--END

	--END

	--24--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
			AND Type = 24
		)
		BEGIN
			INSERT INTO @PositionForExp24
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,24,ISNULL(E.DescOne,''),ISNULL(F.BankPK,0),isnull(F.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 24 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			left join Bank F on A.BankPK = F.BankPK and F.Status in (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP and B.InstrumentTypePK in (5,10)
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,F.ID,F.BankPK
	

		END

	END

	--25--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
			AND Type = 25
		)
		BEGIN
			select @paramBondRating = parameter from FundExposure where fundpk = @FundPK and Type = 25

			INSERT INTO @PositionForExp25
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,25,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID + ',Rating :' + B.BondRating,'') 
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			,100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 25 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			left join dbo.MasterValue G on B.BondRating = G.DescOne and G.ID = 'BondRating' and G.Status in (1,2)
			left join Bank F on A.BankPK = F.BankPK and F.Status in (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP and B.InstrumentTypePK in (2,3,8,9,13,15) and BondRating <> '' and G.Priority > @paramBondRating
			group by A.FundPK,E.DescOne,A.InstrumentPK,B.ID,B.BondRating
	

		END

	END

	--26--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 26
		)
		BEGIN
			INSERT INTO @PositionForExp26
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,26,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 26 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP
			and B.BitIsForeign = 1
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK
	

		END

	END

	--27--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 27
		)
		BEGIN
			INSERT INTO @PositionForExp27
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,27,ISNULL(E.DescOne,''),ISNULL(B.CounterpartPK,0),isnull(F.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 27 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			left join Counterpart F on B.CounterpartPK = F.CounterpartPK and F.Status in (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP and B.InstrumentTypePK in (8)
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK, B.CounterpartPK,F.ID
	

		END

	END
	
	--28--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 28
		)
		BEGIN
			INSERT INTO @PositionForExp28
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,28,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,(SUM(ISNULL(A.MarketValue ,0) ) + @TotalDirectInvestment + @TotalLandAndProperty) MarketValue
			,@TotalMarketValue
			, (SUM(ISNULL(A.MarketValue,0)) + @TotalDirectInvestment + @TotalLandAndProperty) /  @TotalMarketValue * 100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 28 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP and B.InstrumentTypePK not in (5,10)
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK
	

		END

	END
	
	--29--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 29
		)
		BEGIN
			INSERT INTO @PositionForExp29
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
					  Behavior
					)
			SELECT A.FundPK,29,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValue
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,''
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 29 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP
			and B.Affiliated = 1
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK
	

		END

	END

	--31--
	BEGIN


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 31
		)
	BEGIN
		INSERT INTO @PositionForExp31
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		
				SELECT A.FundPK,31,ISNULL(E.DescOne,''),ISNULL(D.HoldingPK,0),ISNULL(D.ID,'')
				,ISNULL(D.HoldingPK,0)
				,ISNULL(D.ID ,'')
				,SUM(ISNULL(A.MarketValue,0)) MarketValue
				,@TotalMarketValue
				, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
				''
				FROM dbo.FundPosition A
				LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
				LEFT JOIN dbo.Issuer C ON B.IssuerPK =  C.IssuerPK AND C.status IN (1,2)
				LEFT JOIN dbo.Holding D ON C.HoldingPK =  D.HoldingPK AND D.status IN (1,2)
				LEFT JOIN dbo.MasterValue E ON E.Code = 31 AND E.ID = 'ExposureType' AND E.status IN (1,2)
				WHERE  Date = @MaxDateEndDayFP 
				AND D.ID IS NOT NULL and A.Status = 2

				--PARAM DISINI
				AND FundPk = @FundPK 
				GROUP BY D.ID,D.HoldingPK,A.FundPK,E.DescOne
				order by D.HoldingPK

	
	END


	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
		AND Type = 32
		)
	BEGIN
	INSERT INTO @PositionForExp32
			( FundPK ,
				Exposure ,
				ExposureDesc,
				Parameter ,
				ParameterDesc ,
				InstrumentPK,
				InstrumentID ,
				MarketValue ,
				AUM ,
				ExposurePercent,
				Behavior
			)
	SELECT A.FundPK,32,ISNULL(E.DescOne,''),ISNULL(G.CapitalClassification,0)
	,case when G.CapitalClassification = 1 then 'Capital Classification 1'
			when G.CapitalClassification = 2 then 'Capital Classification 2'
				when G.CapitalClassification = 3 then 'Capital Classification 3'
					else 'Capital Classification 4' end
	,isnull(G.BankPK,0)
	,isnull(G.ID,'')
	,SUM(ISNULL(A.MarketValue,0)) MarketValue
	,@TotalMarketValue
	, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent,
	0
	FROM dbo.FundPosition A
	LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
	LEFT JOIN dbo.MasterValue E ON E.Code = 32 AND E.ID = 'ExposureType' AND E.status IN (1,2)
	LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
	WHERE  Date = @MaxDateEndDayFP 
	AND C.GroupType = 3 and A.Status = 2
	AND ISNULL(G.CapitalClassification,0) <> 0
	-- PARAM DISINI
	AND A.FundPK = @FundPK 
	GROUP BY A.FundPK,B.ID,E.DescOne,G.CapitalClassification,G.BankPK,G.ID

	END

	END


-- HANDLE DATA INVESTMENT PER FUND

if @MaxDateEndDayFP = @yesterday
BEGIN



	DECLARE W Cursor For
			SELECT InstrumentPK,case when @ClientCode = 21 then 0 else Amount end Amount FROM @InvestmentPosition WHERE fundPK = @FundPK
	Open W
	Fetch Next From W
	INTO @WInstrumentPK,@WAmount
	
	While @@FETCH_STATUS = 0  
	BEGIN
    
		--1--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp1 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp1 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp1
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,1,ISNULL(D.DescOne,''),ISNULL(B.GroupType,0),ISNULL(C.DescOne,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 
			,'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue C ON B.GroupType = C.Code AND C.id = 'InstrumentGroupType' AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 1 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND C.DescOne IS NOT NULL
		END

		--2--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp2 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp2 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp2
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,2,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 2 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND B.GroupType = 2 --and A.InstrumentTypePK not in (2,12,13,14,15)
			AND isnull(@WInstrumentPK,0) <> 0
		END
		
		--3--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp3 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp3 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp3
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,3,ISNULL(D.DescOne,''),ISNULL(B.SectorPK,0),ISNULL(C.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.SubSector B ON A.SectorPK =  B.SubSectorPK AND B.status IN (1,2)
			LEFT JOIN Sector C ON B.SectorPK = C.SectorPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 3 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--4--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp4 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp4 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp4
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,4,ISNULL(D.DescOne,''),ISNULL(A.InstrumentTypePK,0),ISNULL(B.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 4 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0 AND isnull(B.ID,'') <> ''
		END

		--5--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp5 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp5 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp5
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,5,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 5 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND B.GroupType = 1
		END

		--10--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp10 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp10 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp10
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,10,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 10 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND ISNULL(G.ID,'')  <> ''
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--13--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp13 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp13 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp13
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,13,ISNULL(D.DescOne,''),ISNULL(B.IssuerPK,0),ISNULL(B.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.Issuer B ON A.IssuerPK = B.IssuerPK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 13 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND ISNULL(B.ID,'')  <> ''
		END

		--14--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp14 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp14 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp14
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,14,ISNULL(D.DescOne,''),ISNULL(B.IndexPK,0),ISNULL(C.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN @InstrumentIndex B ON A.InstrumentPK =  B.InstrumentPK
			LEFT JOIN dbo.[Index] C ON B.IndexPK = C.IndexPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 14 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND C.ID IS NOT null
			AND isnull(@WInstrumentPK,0) <> 0
			AND ISNULL(C.ID,'')  <> ''
		END

		--16--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp16 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp16 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp16
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,16,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 16 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--24--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp24 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp24 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
		
			INSERT INTO @PositionForExp24
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,24,ISNULL(D.DescOne,''),ISNULL(A.BankPK,0),ISNULL(E.ID,'')
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 24 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			left join Bank E on A.BankPK = E.BankPK and E.Status in (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and B.InstrumentTypePK in (5,10)
			AND isnull(@WInstrumentPK,0) <> 0
		END
		
		--25--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp25 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp25 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			select @paramBondRating = parameter from FundExposure where fundpk = @FundPK and Type = 25

			INSERT INTO @PositionForExp25
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,25,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,100,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 25 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			left join dbo.MasterValue G on A.BondRating = G.DescOne and G.ID = 'BondRating' and G.Status in (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK in (2,3,8,9,13,15) and A.BondRating <> ''  and G.Priority > @paramBondRating
			AND isnull(@WInstrumentPK,0) <> 0

		END

		--26--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp26 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp26 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

			INSERT INTO @PositionForExp26
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,26,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 26 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.BitIsForeign = 1
			AND isnull(@WInstrumentPK,0) <> 0

		END
		
		--27--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp27 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp27 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

			INSERT INTO @PositionForExp27
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,27,ISNULL(D.DescOne,''),ISNULL(A.CounterpartPK,0),ISNULL(E.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 27 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			left join Counterpart E on A.CounterpartPK = E.CounterpartPK and E.Status in (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and B.InstrumentTypePK = 8
			AND isnull(@WInstrumentPK,0) <> 0

		END
		
		--28--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp28 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp28 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

			INSERT INTO @PositionForExp28
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,28,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,(ISNULL(@WAmount,0) + @TotalDirectInvestment + @TotalLandAndProperty)
			,ISNULL(@TotalMarketValue,0)
			,(ISNULL(@WAmount,0) + @TotalDirectInvestment + @TotalLandAndProperty)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 28 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK not in (5,10)
			AND isnull(@WInstrumentPK,0) <> 0

		END
		
		--29--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp29 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp29 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

			INSERT INTO @PositionForExp29
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,29,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 29 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.Affiliated = 1
			AND isnull(@WInstrumentPK,0) <> 0

		END


		--31--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp31 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp31 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp31
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent,
					  Behavior
		        )
			SELECT @FundPK,31,ISNULL(D.DescOne,''),ISNULL(C.HoldingPK,0),ISNULL(C.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 ,
			'ACTIVE'
			FROM dbo.Instrument A
			LEFT JOIN dbo.Issuer B ON A.IssuerPK = B.IssuerPK AND B.status IN (1,2)
			LEFT JOIN dbo.Holding C ON B.HoldingPK = C.HoldingPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 31 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND ISNULL(C.ID,'')  <> ''
		END


	
		
		FETCH Next From W
		into @WInstrumentPK,@WAmount
	END	
	Close W
	Deallocate W



END



-- PROSES EXPOSURE AKHIR
BEGIN


Declare A Cursor For
	SELECT FundPK,CAST(Type AS INT) Type,Parameter,MinExposurePercent,MaxExposurePercent 
	,WarningMinExposurePercent,WarningMaxExposurePercent,MinValue
	,MaxValue,WarningMinValue,WarningMaxValue
	FROM dbo.FundExposure WHERE status = 2 
	AND FundPK = @FundPK
	ORDER BY Type asc


Open A
Fetch Next From A
INTO @CFundPK,@CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
,@CWarningMinVal,@CWarningMaxVal
While @@FETCH_STATUS = 0  
Begin

	IF @CType = 1
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )

		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp1
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 2
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp2
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 2 AND @CParameter = 0
	BEGIN
	INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
	SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp2
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 3
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp3
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 3 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc  + ' | ALL PARAM ',
		Parameter,ParameterDesc 
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp3
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 4
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp4
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 5
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp5
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 5 AND @CParameter = 0
	BEGIN
	INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
	SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp5
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 10
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp10
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 10 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp10
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 13
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp13
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 13 AND @CParameter = 0 -- CUSTOM 20, UNTUK TIPE ISSUER DAN PARAMETER ALL, ISSUERPK 419 DI EXCLUDE
	BEGIN
		IF (@ClientCode = '20')
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue,
						  Behavior
					)
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
			Behavior
			FROM @PositionForExp13 A
			left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			where FundPK = @CFundPK and B.IssuerPK <> 419
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
		END
		ELSE
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue,
						  Behavior
					)
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
			Behavior
			FROM @PositionForExp13
			where FundPK = @CFundPK
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
		END
		
	END

	IF @CType = 14
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp14
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 14 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp14
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END
	
	IF @CType = 16
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )

		SELECT FundPK,Exposure,ExposureDesc,
		0,''
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp16
		where  FundPK = @CFundPK
		GROUP BY FundPK,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 20
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalInvestmentAllFundForCounterpartExposure * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp20
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 20 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalInvestmentAllFundForCounterpartExposure * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp20
		where  FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	--IF @CType = 24
	--BEGIN
	--	INSERT INTO @Exposure
	--	        ( 
	--			  FundPK,
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue,
	--			  Behavior
	--	        )
	--	SELECT A.FundPK,Exposure,ExposureDesc,
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
	--	,0,0,0,0,isnull(C.ExposurePercent,0),0,0,0,0,0,0,0,0,0,0,0,''
	--	FROM @PositionForExp24 A
	--	left join Bank B on A.Parameter = B.BankPK and B.Status in (1,2)
	--	left join CamelMapping C on B.CamelScore between C.FromValue and C.ToValue and A.FundPK = C.FundPK
	--	WHERE Parameter = @CParameter
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,C.ExposurePercent,A.FundPK
	--END

	--IF @CType = 24 and @CParameter = 0
	--BEGIN

	--	INSERT INTO @Exposure
	--	        ( 
	--			  FundPK,
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue,
	--			  Behavior
	--	        )
	--	SELECT A.FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
	--	,0,0,0,0,isnull(C.ExposurePercent,0),0,0,0,0,0,0,0,0,0,0,0,''
	--	FROM @PositionForExp24 A
	--	left join Bank B on A.Parameter = B.BankPK and B.Status in (1,2)
	--	left join CamelMapping C on B.CamelScore between C.FromValue and C.ToValue and A.FundPK = C.FundPK
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,C.ExposurePercent,A.FundPK
	--END

	IF @CType = 25
	BEGIN

		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
				  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0))
		,100 ExposurePercent
		,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,Behavior
		FROM @PositionForExp25
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,FundPK,Behavior

	END

	--IF @CType = 26
	--BEGIN
	--	INSERT INTO @Exposure
	--	        ( 
	--			  FundPK,
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue,
	--			  Behavior
	--	        )
	--	SELECT FundPK,Exposure,ExposureDesc,
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
	--	,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,''
	--	FROM @PositionForExp26
	--	WHERE Parameter = @CParameter
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,FundPK
	--END

	IF @CType = 26 and @CParameter = 0
	BEGIN

		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
				  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		0,''
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,Behavior
		FROM @PositionForExp26
		GROUP BY Exposure,ExposureDesc,FundPK,Behavior
	END

	IF @CType = 27 and @CParameter = 0
	BEGIN

		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
				  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,Behavior
		FROM @PositionForExp27
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,FundPK,Behavior
	END

	IF @CType = 28 and @CParameter = 0
	BEGIN

		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
				  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,Behavior
		FROM @PositionForExp28
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,FundPK,Behavior
	END
	
	IF @CType = 29 and @CParameter = 0
	BEGIN

		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
				  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,Behavior
		FROM @PositionForExp29
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,FundPK,Behavior
	END


	IF @CType = 31
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  FundPK,
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue,
					  Behavior
		        )
		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
		Behavior
		FROM @PositionForExp31
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END

	IF @CType = 31 AND @CParameter = 0 
	BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue,
						  Behavior
					)
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,
			Behavior
			FROM @PositionForExp31
			where FundPK = @CFundPK
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	
		
	END


	IF @CType = 32
	BEGIN
		INSERT INTO @Exposure
				( 
					FundPK,
					Exposure,
					ExposureID ,
					Parameter ,
					ParameterDesc ,
					MarketValue ,
					ExposurePercent ,
					MinExposurePercent ,
					WarningMinExposure ,
					AlertWarningMinExposure,
					AlertMinExposure ,
					MaxExposurePercent ,
					WarningMaxExposure ,
					AlertWarningMaxExposure ,
					AlertMaxExposure ,
					MinValue ,
					WarningMinValue ,
					AlertWarningMinValue ,
					AlertMinValue ,
					MaxValue ,
					WarningMaxValue ,
					AlertWarningMaxValue ,
					AlertMaxValue,
						Behavior
				)

		SELECT FundPK,Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,Behavior
		FROM @PositionForExp32
		WHERE Parameter = @CParameter and FundPK = @CFundPK
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,Behavior
	END


	Fetch Next From A 
	INTO @CFundPK,@CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
	,@CWarningMinVal,@CWarningMaxVal 
End	
Close A
Deallocate A

END



Fetch next From Z 
Into @FundPK
END
Close Z
Deallocate Z



--ALL FUND (9,18,19)
begin
	Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
	endDayTrailsFundPortfolio where 
	valuedate = 
	(
		Select max(ValueDate) from endDayTrailsFundPortfolio where
		valuedate <= @Date  and status = 2 
	)
	and status = 2 

	declare @tableMarketCap table 
	(
		InstrumentPK int,
		valuedate date,
		MarketCap numeric(32,8)
	)

	insert into @tableMarketCap(InstrumentPK,valuedate)
	select InstrumentPK,max(date) from InstrumentIndex where Status = 2
	group by InstrumentPK

	update A set A.MarketCap = isnull(B.MarketCap,0)
	from @tableMarketCap A
	left join InstrumentIndex B on A.InstrumentPK = B.InstrumentPK and A.valuedate = B.Date and B.Status = 2


	--9--
	BEGIN

	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = 0 AND status = 2
		AND Type = 9
		)
	BEGIN
		INSERT INTO @PositionForExp9
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,9,ISNULL(E.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValueAllFund
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValueAllFund * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @Date and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 9 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
			WHERE  Date = @Date  and A.Status = 2
			AND C.GroupType = 3
			GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,G.BankPK,G.ID
		


	
	END


	END
	
	--18--
	BEGIN



	IF EXISTS(
		SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = 0 AND status = 2
		AND Type = 18
		)
	BEGIN
		INSERT INTO @PositionForExp18
				( FundPK ,
				  Exposure ,
				  ExposureDesc,
				  Parameter ,
				  ParameterDesc ,
				  InstrumentPK,
				  InstrumentID ,
				  MarketValue ,
				  AUM ,
				  ExposurePercent,
				  Behavior
				)
		SELECT A.FundPK,18,ISNULL(E.DescOne,''),ISNULL(C.IssuerPK,0),ISNULL(C.ID,'')
			,ISNULL(A.InstrumentPK,0)
			,ISNULL(B.ID ,'')
			,SUM(ISNULL(A.MarketValue,0)) MarketValue
			,@TotalMarketValueAllFund
			, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValueAllFund * 100 ExposurePercent,
			case when exists(select * from Investment where valuedate = @Date and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
			FROM dbo.FundPosition A
			LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
			LEFT JOIN dbo.Issuer C ON B.IssuerPK =  C.IssuerPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue E ON E.Code = 18 AND E.ID = 'ExposureType' AND E.status IN (1,2)
			WHERE  Date = @Date and A.Status = 2
			AND C.ID IS NOT NULL
			GROUP BY C.ID,C.IssuerPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	
	END


	END

	--19--
	BEGIN
		IF EXISTS(
			SELECT TOP 1 Type FROM dbo.FundExposure WHERE status = 2 and fundpk = 0
			AND Type = 19
			)
		BEGIN
			INSERT INTO @PositionForExp19
					( FundPK ,
					  Exposure ,
					  ExposureDesc,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
				  Behavior
					)
			SELECT A.FundPK,19,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'')
				,ISNULL(A.InstrumentPK,0)
				,ISNULL(B.ID ,'')
				,SUM(ISNULL(A.MarketValue,0)) MarketValue
				,isnull(F.MarketCap,0)
				,case when isnull(F.MarketCap,0) = 0 then 0 else SUM(ISNULL(A.MarketValue,0)) /  isnull(F.MarketCap,0) * 100 end ExposurePercent,
				case when exists(select * from Investment where valuedate = @Date and StatusSettlement = 2 and InstrumentPK = A.InstrumentPK and FundPK = A.FundPK and TrxType = 1) then 'ACTIVE' else '' end
				FROM dbo.FundPosition A
				LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
				LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
				LEFT JOIN dbo.MasterValue E ON E.Code = 19 AND E.ID = 'ExposureType' AND E.status IN (1,2)
				left join @tableMarketCap F on A.InstrumentPK = F.InstrumentPK
				WHERE Date = @Date and B.InstrumentTypePK in (1,4,16)
				GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,F.MarketCap,A.MarketValue

	END
	END



	-- HANDLE DATA INVESTMENT ALL FUND
	if @MaxDateEndDayFP = @yesterday
	BEGIN


		DECLARE E Cursor For
				SELECT InstrumentPK,Amount,FundPK FROM @InvestmentPositionALLFund 
		Open E
		Fetch Next From E
		INTO @EInstrumentPK,@EAmount,@EFundPK
		While @@FETCH_STATUS = 0  
		BEGIN

			--9--
			IF EXISTS(
					SELECT TOP 1 * FROM @PositionForExp9 WHERE InstrumentPK = @InstrumentPK
			)
			BEGIN
				UPDATE @PositionForExp9 SET MarketValue = MarketValue + ISNULL(@EAmount,0)
				,ExposurePercent = (MarketValue + ISNULL(@EAmount,0)) / @TotalMarketValueAllFund * 100
				WHERE InstrumentPK = @EInstrumentPK AND FundPK = @FundPK
			END
			ELSE
			BEGIN
				INSERT INTO @PositionForExp9
					( FundPK ,
					  Exposure ,
					  ExposureDesc ,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK ,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
						  Behavior
					)
				SELECT @EFundPK,9,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'') 
				,ISNULL(@EInstrumentPK,0),ISNULL(A.ID,'')
				,ISNULL(@EAmount,0)
				,ISNULL(@TotalMarketValueAllFund,0)
				,ISNULL(@EAmount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 ,
				''
				FROM dbo.Instrument A
				LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
				LEFT JOIN dbo.MasterValue D ON D.Code = 9 AND D.ID = 'ExposureType' AND D.status IN (1,2)
				LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
				WHERE A.InstrumentPK = @EInstrumentPK AND A.status IN (1,2)
				AND isnull(@EInstrumentPK,0) <> 0
				AND B.GroupType = 3
			END

			--18--
			IF EXISTS(
					SELECT TOP 1 * FROM @PositionForExp18 WHERE InstrumentPK = @InstrumentPK
			)
			BEGIN
				UPDATE @PositionForExp18 SET MarketValue = MarketValue + ISNULL(@EAmount,0)
				,ExposurePercent = (MarketValue + ISNULL(@EAmount,0)) / @TotalMarketValueAllFund * 100
				WHERE InstrumentPK = @EInstrumentPK AND FundPK = @FundPK
			END
			ELSE
			BEGIN
				INSERT INTO @PositionForExp18
					( FundPK ,
					  Exposure ,
					  ExposureDesc ,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK ,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent,
						  Behavior
					)
				SELECT @EFundPK,18,ISNULL(D.DescOne,''),ISNULL(A.IssuerPK,0),ISNULL(B.ID,'') 
				,ISNULL(@EInstrumentPK,0),ISNULL(A.ID,'')
				,ISNULL(@EAmount,0)
				,ISNULL(@TotalMarketValueAllFund,0)
				,ISNULL(@EAmount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 ,
				''
				FROM dbo.Instrument A
				LEFT JOIN dbo.Issuer B ON A.IssuerPK =  B.IssuerPK AND B.status IN (1,2)
				LEFT JOIN dbo.MasterValue D ON D.Code = 18 AND D.ID = 'ExposureType' AND D.status IN (1,2)
				WHERE A.InstrumentPK = @EInstrumentPK AND A.status IN (1,2)
				AND isnull(@EInstrumentPK,0) <> 0
				AND ISNULL(B.ID,'')  <> ''
		
			END

			--19--
			IF EXISTS(
					SELECT TOP 1 * FROM @PositionForExp19 WHERE InstrumentPK = @InstrumentPK
			)
			BEGIN
				UPDATE @PositionForExp19 SET MarketValue = MarketValue + ISNULL(@EAmount,0)
				,ExposurePercent = (MarketValue + ISNULL(@EAmount,0)) / @TotalInvestmentAllFundForCounterpartExposure * 100
				WHERE InstrumentPK = @EInstrumentPK AND FundPK = @FundPK
			END
			ELSE
			BEGIN
				INSERT INTO @PositionForExp19
					( FundPK ,
					  Exposure ,
					  ExposureDesc ,
					  Parameter ,
					  ParameterDesc ,
					  InstrumentPK ,
					  InstrumentID ,
					  MarketValue ,
					  AUM ,
					  ExposurePercent
					)
				SELECT @EFundPK,9,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'') 
				,ISNULL(@EInstrumentPK,0),ISNULL(A.ID,'')
				,ISNULL(@EAmount,0)
				,ISNULL(@TotalMarketValueAllFund,0)
				,ISNULL(@EAmount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 
				FROM dbo.Instrument A
				LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
				LEFT JOIN dbo.MasterValue D ON D.Code = 9 AND D.ID = 'ExposureType' AND D.status IN (1,2)
				LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
				WHERE A.InstrumentPK = @EInstrumentPK AND A.status IN (1,2)
				AND isnull(@EInstrumentPK,0) <> 0
				AND B.GroupType = 3
			END


		
		FETCH Next From E
			into @EInstrumentPK,@EAmount,@EFundPK
		End	
		Close E
		Deallocate E

	END

	--PROSES EXPOSURE AKHIR ALL FUND ALL PARAMETER
	BEGIN
	Declare A Cursor For
		SELECT FundPK,CAST(Type AS INT) Type,Parameter,MinExposurePercent,MaxExposurePercent 
		,WarningMinExposurePercent,WarningMaxExposurePercent,MinValue
		,MaxValue,WarningMinValue,WarningMaxValue
		FROM dbo.FundExposure WHERE status = 2 
		AND FundPK = 0
		ORDER BY Type asc
	Open A
	Fetch Next From A
	INTO @CFundPK,@CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
	,@CWarningMinVal,@CWarningMaxVal
	While @@FETCH_STATUS = 0  
	Begin

		IF @CType = 9 AND @CParameter <> 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc,
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp9
			WHERE Parameter = @CParameter
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 9 AND @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp9
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 18 AND @CParameter <> 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc,
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp18
			WHERE Parameter = @CParameter 
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 18 and @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					) 
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp18
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 19 AND @CParameter <> 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc,
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			,case when isnull(F.MarketCap,0) = 0 then 0 else SUM(ISNULL(MarketValue,0)) / isnull(F.MarketCap,0) * 100 end ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp19 A
			left join @tableMarketCap F on A.InstrumentPK = F.InstrumentPK
			WHERE Parameter = @CParameter 
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,F.MarketCap
		END

		IF @CType = 19 AND @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			,case when isnull(F.MarketCap,0) = 0 then 0 else SUM(ISNULL(MarketValue,0)) / isnull(F.MarketCap,0) * 100 end ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp19 A
			left join @tableMarketCap F on A.InstrumentPK = F.InstrumentPK
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc,F.MarketCap

		END


	

		IF @CType = 22 AND @CParameter <> 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc,
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp22
			WHERE Parameter = @CParameter 
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 22 and @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					) 
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp22
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END


		IF @CType = 23 AND @CParameter <> 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT FundPK,Exposure,ExposureDesc,
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp23
			WHERE Parameter = @CParameter 
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END

		IF @CType = 23 and @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
					( 
					  FundPK,
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					) 
			SELECT FundPK,Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp23
			GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
		END


        IF @CType = 30 AND @CParameter = 0
		BEGIN
			INSERT INTO @Exposure
				( 
					Exposure,
					ExposureID ,
					Parameter ,
					ParameterDesc ,
					MarketValue ,
					ExposurePercent ,
					MinExposurePercent ,
					WarningMinExposure ,
					AlertWarningMinExposure,
					AlertMinExposure ,
					MaxExposurePercent ,
					WarningMaxExposure ,
					AlertWarningMaxExposure ,
					AlertMaxExposure ,
					MinValue ,
					WarningMinValue ,
					AlertWarningMinValue ,
					AlertMinValue ,
					MaxValue ,
					WarningMaxValue ,
					AlertWarningMaxValue ,
					AlertMaxValue,
					Behavior
				)
			SELECT 30,ISNULL(DescOne,'') + ' | ALL FUND ',
			0,'ALL'
			, @TotalMarketValueAllFund MarketValue
			, 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0,0
			FROM MasterValue A 
			WHERE Code = 30 AND ID = 'ExposureType' AND status IN (1,2)
		END


	Fetch Next From A 
		INTO @CFundPK,@CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
		,@CWarningMinVal,@CWarningMaxVal 
	End	
	Close A
	Deallocate A
END

end




update @Exposure 
SET AlertMinExposure = 1 WHERE ExposurePercent < MinExposurePercent AND MinExposurePercent > 0
update @Exposure 
SET AlertWarningMinExposure = 1 WHERE ExposurePercent < WarningMinExposure AND WarningMinExposure > 0
UPDATE @Exposure
SET AlertMaxExposure = 1 WHERE ExposurePercent > MaxExposurePercent AND MaxExposurePercent > 0
UPDATE @Exposure
SET AlertWarningMaxExposure = 1 WHERE ExposurePercent > WarningMaxExposure AND WarningMaxExposure > 0

UPDATE @Exposure
SET AlertMinValue = 1 WHERE MarketValue < MinValue AND MinValue > 0
UPDATE @Exposure
SET AlertWarningMinValue = 1 WHERE MarketValue < WarningMinValue AND WarningMinValue > 0
UPDATE @Exposure
SET AlertMaxValue = 1 WHERE MarketValue > case when MaxValue = 0 then 1000000000000000 else MaxValue end AND MaxValue > 0
UPDATE @Exposure
SET AlertWarningMaxValue = 1 WHERE MarketValue > WarningMaxValue AND WarningMaxValue > 0


--select * from @PositionForExp19
--select * from @Exposure

DELETE A from ZTEMP_EXPOSURE_MONITORING A where Date = @Date " + _paramFund + @"
--and FundPK = @FundPK

--delete all fund
DELETE ZTEMP_EXPOSURE_MONITORING where Date = @Date and Exposure in (9,18,19,30)

INSERT INTO ZTEMP_EXPOSURE_MONITORING(Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,UsersID,EntryTime,LastUpdate,Behavior)

select @Date Date,isnull(FundPK,@XFundPK),Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,@UsersID,@Time,@Time,case when Behavior = 'ACTIVE' then 1 else 2 end from @Exposure
where (ExposurePercent > 0 or MarketValue > 0) and Exposure not in (9,18,19,30)


-- NI UNTUK MIN EXPOSURE
INSERT INTO ZTEMP_EXPOSURE_MONITORING(Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,UsersID,EntryTime,LastUpdate,Behavior)

select @Date Date,isnull(FundPK,@XFundPK),Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,@UsersID,@Time,@Time,case when Behavior = 'ACTIVE' then 1 else 2 end from @Exposure
where (ExposurePercent = 0 or MarketValue = 0) and Exposure in (1,4)

--all fund
INSERT INTO ZTEMP_EXPOSURE_MONITORING(Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,UsersID,EntryTime,LastUpdate,Behavior)

select @Date Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,@UsersID,@Time,@Time,case when Behavior = 'ACTIVE' then 1 else 2 end from @Exposure
where (ExposurePercent > 0 or MarketValue > 0 ) and Exposure in (9,18,19) and (AlertMaxExposure = 1 or AlertMinExposure = 1 or AlertWarningMaxExposure = 1 or AlertWarningMinExposure = 1 or AlertMaxValue = 1 or AlertMaxExposure = 1)


INSERT INTO ZTEMP_EXPOSURE_MONITORING(Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,UsersID,EntryTime,LastUpdate,Behavior)

select @Date Date,FundPK,Exposure,ExposureID,Parameter,ParameterDesc,ExposurePercent,WarningMaxExposure,MaxExposurePercent,
WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue,
AlertWarningMaxExposure,AlertMaxExposure,AlertWarningMinExposure,AlertMinExposure,
AlertWarningMaxValue,AlertMaxValue,AlertWarningMinValue,AlertMinValue,@UsersID,@Time,@Time,case when Behavior = 'ACTIVE' then 1 else 2 end from @Exposure
where (ExposurePercent > 0 or MarketValue > 0 ) and Exposure in (30)


DELETE A from ZTEMP_EXPOSURE_MONITORING_DETAIL A where Date = @Date " + _paramFund + @"
--and FundPK = @FundPK

--delete all fund
DELETE ZTEMP_EXPOSURE_MONITORING_DETAIL where Date = @Date and Exposure in (9,18,19)

INSERT INTO ZTEMP_EXPOSURE_MONITORING_DETAIL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp1
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp2
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp3
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp4
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp5
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp9
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp10
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp13
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp14
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp16
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp18
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp19
UNION ALL
select @Date Date,FundPK,Exposure,ExposureDesc,Parameter,ParameterDesc,
InstrumentPK,InstrumentID,MarketValue,AUM,ExposurePercent from @PositionForExp20



delete A from ExposureMonitoringDetail A where TanggalPelanggaran = @Date " + _paramFund + @"
--@XFundPK



Declare @ExposureMonitoringDetailPK int
Select @ExposureMonitoringDetailPK = max(ExposureMonitoringDetailPK) from ExposureMonitoringDetail
set @ExposureMonitoringDetailPK = isnull(@ExposureMonitoringDetailPK,0)

insert into ExposureMonitoringDetail
(ExposureMonitoringDetailPK,HistoryPK,Status,Date,FundPK,BankCustodian,KebijakanInvestasi,Exposure,TanggalPelanggaran,Batasan,NoSurat,TanggalSurat,TanggalTerimaSurat,Remedy,ExDate,StatusExposure,Remarks,EntryUsersID,EntryTime,LastUpdate,parameter,instrumentpk)
select @ExposureMonitoringDetailPK +  ROW_NUMBER() OVER(Order By A.KebijakanInvestasi ASC),1,1,CONVERT(date, getdate()),A.FundPK,A.BankCustodian,A.KebijakanInvestasi,A.Exposure,A.TanggalPelanggaran,A.Batasan,A.NoSurat,A.TanggalSurat,A.TanggalTerimaSurat,A.Remedy,A.ExDate,A.StatusExposure,A.Remarks,@UsersID,@Time,@Time,A.parameter,A.instrumentpk
from (
select A.FundPK,D.ID BankCustodian,A.ExposureID + ' - ' + A.ParameterDesc KebijakanInvestasi,CONVERT(varchar, CAST(sum(A.ExposurePercent) AS money), 1) + '%' Exposure,@Date TanggalPelanggaran,
case when A.AlertWarningMaxExposure = 1 then 'Warning Max Exposure : ' + CONVERT(varchar, CAST(WarningMaxExposure AS money), 1) + '%'
		when A.AlertMaxExposure = 1 then 'Max Exposure : ' + CONVERT(varchar, CAST(MaxExposurePercent AS money), 1) + '%'
			when A.AlertWarningMinExposure = 1 then 'Warning Min Exposure : ' + CONVERT(varchar, CAST(WarningMinExposure AS money), 1) + '%'
				when A.AlertMinExposure = 1 then 'Min Exposure : ' + CONVERT(varchar, CAST(MinExposurePercent AS money), 1) + '%' end Batasan,
'' NoSurat,@date TanggalSurat,@date TanggalTerimaSurat,Behavior Remedy,dbo.FWorkingDay(@date,case when Behavior = 1 then 10 else 20 end) ExDate,2 StatusExposure,'' Remarks, A.Exposure parameter, A.Parameter instrumentpk
 from ZTEMP_EXPOSURE_MONITORING A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status in (1,2)
left join Bank D on C.BankPK = D.BankPK and D.status in (1,2)
left join ExposureMonitoringDetail F on A.FundPK = F.FundPK and A.Parameter = F.InstrumentPK and A.Exposure = F.Parameter and F.StatusExposure = 2 and F.status in (1,2)
where A.date = @date and (A.AlertMaxExposure <> 0 or A.AlertMinExposure <> 0) and A.Exposure <> 16 and F.FundPk is null
" + _paramFund + @"
--@XFundPK
group by A.FundPK,D.ID,A.ExposureID,A.ParameterDesc,A.AlertWarningMaxExposure,A.AlertMaxExposure,A.AlertWarningMinExposure,A.AlertMinExposure,
A.WarningMaxExposure,A.MaxExposurePercent,A.WarningMinExposure,A.MinExposurePercent,behavior,A.Exposure,A.Parameter

union all
-- TOTAL PORTFOLIO
select A.FundPK,D.ID BankCustodian,A.ExposureID KebijakanInvestasi,CONVERT(varchar, CAST(sum(A.ExposurePercent) AS money), 1) + '%' Exposure,@Date TanggalPelanggaran,
case when A.AlertWarningMaxExposure = 1 then 'Warning Max Exposure : ' + CONVERT(varchar, CAST(WarningMaxExposure AS money), 1) + '%'
		when A.AlertMaxExposure = 1 then 'Max Exposure : ' + CONVERT(varchar, CAST(MaxExposurePercent AS money), 1) + '%'
			when A.AlertWarningMinExposure = 1 then 'Warning Min Exposure : ' + CONVERT(varchar, CAST(WarningMinExposure AS money), 1) + '%'
				when A.AlertMinExposure = 1 then 'Min Exposure : ' + CONVERT(varchar, CAST(MinExposurePercent AS money), 1) + '%' end Batasan,
'' NoSurat,@date TanggalSurat,@date TanggalTerimaSurat,Behavior Remedy,dbo.FWorkingDay(@date,case when Behavior = 1 then 10 else 20 end)  ExDate,2 StatusExposure,'' Remarks, A.Exposure parameter, 0 instrumentpk
 from ZTEMP_EXPOSURE_MONITORING A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status in (1,2)
left join Bank D on C.BankPK = D.BankPK and D.status in (1,2)
left join ExposureMonitoringDetail F on A.FundPK = F.FundPK and A.Parameter = F.InstrumentPK and A.Exposure = F.Parameter and F.StatusExposure = 2 and F.status in (1,2)
where A.date = @date and (A.AlertMaxExposure <> 0 or A.AlertMinExposure <> 0)  and A.Exposure = 16 and F.FundPk is null
" + _paramFund + @"
--@XFundPK
group by A.FundPK,D.ID,A.ExposureID,A.AlertWarningMaxExposure,A.AlertMaxExposure,A.AlertWarningMinExposure,A.AlertMinExposure,
A.WarningMaxExposure,A.MaxExposurePercent,A.WarningMinExposure,A.MinExposurePercent,behavior,A.Exposure
) A

-- update statusexposure
update A set StatusExposure = 1
from ExposureMonitoringDetail A
inner join ZTEMP_EXPOSURE_MONITORING C on A.FundPK = C.FundPK and A.Parameter = C.Exposure and A.InstrumentPK = C.Parameter and C.Date = @date
where A.StatusExposure = 2 and (C.AlertMaxExposure = 0 and C.AlertMinExposure = 0)

";
						cmd.Parameters.AddWithValue("@Date", _date);
						cmd.Parameters.AddWithValue("@UsersID", _usersID);
						cmd.Parameters.AddWithValue("@Time", _dateTimeNow);
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


		public List<DataExposureMonitoring> Get_DataExposureMonitoringByDateByFundPK(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DataExposureMonitoring> L_ExposureMonitoring = new List<DataExposureMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        if (_fundPK != 0)
                        {
                            _paramFund = "And A.FundPK  = @ZFundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"



                        select B.ID FundID,* from  ZTEMP_EXPOSURE_MONITORING A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where Date = @Date and A.Exposure not in (9,18,19) " + _paramFund + @"

                        --ALL FUND ALL PARAMETER
						union all
						select B.ID FundID,* from  ZTEMP_EXPOSURE_MONITORING A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where Date = @Date  and A.Exposure in (9,18,19)"

                        ;


                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ZFundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ExposureMonitoring.Add(setDataExposureMonitoring(dr));
                                }
                            }
                            return L_ExposureMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private DataExposureMonitoring setDataExposureMonitoring(SqlDataReader dr)
        {
            DataExposureMonitoring M_ExposureMonitoring = new DataExposureMonitoring();
            M_ExposureMonitoring.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_ExposureMonitoring.FundID = dr["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FundID"]);
            M_ExposureMonitoring.Exposure = dr["Exposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Exposure"]);
            M_ExposureMonitoring.ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]);
            M_ExposureMonitoring.Parameter = dr["Parameter"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Parameter"]);
            M_ExposureMonitoring.ParameterDesc = dr["ParameterDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParameterDesc"]);
            M_ExposureMonitoring.ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]);
            M_ExposureMonitoring.WarningMaxExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxExposure"]);
            M_ExposureMonitoring.MaxExposurePercent = dr["MaxExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxExposurePercent"]);
            M_ExposureMonitoring.WarningMinExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinExposure"]);
            M_ExposureMonitoring.MinExposurePercent = dr["MinExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinExposurePercent"]);
            M_ExposureMonitoring.MarketValue = dr["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MarketValue"]);
            M_ExposureMonitoring.WarningMaxValue = dr["WarningMaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxValue"]);
            M_ExposureMonitoring.MaxValue = dr["MaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxValue"]);
            M_ExposureMonitoring.WarningMinValue = dr["WarningMinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinValue"]);
            M_ExposureMonitoring.MinValue = dr["MinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinValue"]);

            M_ExposureMonitoring.AlertWarningMaxExposure = dr["AlertWarningMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertWarningMaxExposure"]);
            M_ExposureMonitoring.AlertMaxExposure = dr["AlertMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxExposure"]);
            M_ExposureMonitoring.AlertWarningMinExposure = dr["AlertWarningMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertWarningMinExposure"]);
            M_ExposureMonitoring.AlertMinExposure = dr["AlertMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinExposure"]);
            M_ExposureMonitoring.AlertWarningMaxValue = dr["AlertWarningMaxValue"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertWarningMaxValue"]);
            M_ExposureMonitoring.AlertMaxValue = dr["AlertMaxValue"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxValue"]);
            M_ExposureMonitoring.AlertWarningMinValue = dr["AlertWarningMinValue"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertWarningMinValue"]);
            M_ExposureMonitoring.AlertMinValue = dr["AlertMinValue"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinValue"]);
            return M_ExposureMonitoring;
        }



        public List<DataDetailExposureMonitoring> Get_DataDetailExposureMonitoringByDateByFundPK(DateTime _date, int _fundPK, int _exposure, int _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DataDetailExposureMonitoring> L_ExposureMonitoring = new List<DataDetailExposureMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                        IF (@Parameter = 0)
                        BEGIN
                            select B.ID FundID,* from  ZTEMP_EXPOSURE_MONITORING_DETAIL A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where Date = @Date and Exposure = @Exposure and A.FundPK = @FundPK 
                            order by Exposurepercent Desc
                        END
                        ELSE
                        BEGIN
                            select B.ID FundID,* from  ZTEMP_EXPOSURE_MONITORING_DETAIL A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where Date = @Date and Exposure = @Exposure and Parameter = @Parameter and A.FundPK = @FundPK 
                            order by Exposurepercent Desc
                        END

                    ";


                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Exposure", _exposure);
                        cmd.Parameters.AddWithValue("@Parameter", _param);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ExposureMonitoring.Add(setDataDetailExposureMonitoring(dr));
                                }
                            }
                            return L_ExposureMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private DataDetailExposureMonitoring setDataDetailExposureMonitoring(SqlDataReader dr)
        {
            DataDetailExposureMonitoring M_ExposureMonitoring = new DataDetailExposureMonitoring();
            M_ExposureMonitoring.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_ExposureMonitoring.FundID = dr["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FundID"]);
            M_ExposureMonitoring.Exposure = dr["Exposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Exposure"]);
            M_ExposureMonitoring.ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]);
            M_ExposureMonitoring.InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]);
            M_ExposureMonitoring.InstrumentID = dr["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InstrumentID"]);
            M_ExposureMonitoring.Parameter = dr["Parameter"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Parameter"]);
            M_ExposureMonitoring.ParameterDesc = dr["ParameterDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParameterDesc"]);
            M_ExposureMonitoring.MarketValue = dr["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MarketValue"]);
            M_ExposureMonitoring.AUM = dr["AUM"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AUM"]);
            M_ExposureMonitoring.ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]);
          
            return M_ExposureMonitoring;
        }

    }
}