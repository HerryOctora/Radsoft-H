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
    public class OMSTimeDepositReps
    {
        Host _host = new Host();


        public class TemplateOMSDeposito
        {
            public int SysNo { get; set; }
            public DateTime ValueDate { get; set; }
            public string Fund { get; set; }
            public string Bank { get; set; }
            public string BankBranch { get; set; }
            public string TrxType { get; set; }
            public string Category { get; set; }
            public string BitBreakable { get; set; }
            public DateTime PlacementDate { get; set; }
            public DateTime MaturityDate { get; set; }
            public decimal InterestRate { get; set; }
            public string InterestPaymentType { get; set; }
            public string PaymentModeOnMature { get; set; }
            public decimal BreakInterestRate { get; set; }
            public string RolloverWithInterest { get; set; }
            public decimal Principal { get; set; }
            public string StatutoryType { get; set; }
            public string Remarks { get; set; }
        }



        private OMSTimeDeposit setOMSTimeDeposit(SqlDataReader dr)
        {
            OMSTimeDeposit M_temp = new OMSTimeDeposit();
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
            if (_host.CheckColumnIsExist(dr, "BitRollOverInterest"))
            {
                M_temp.BitRollOverInterest = dr["BitRollOverInterest"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitRollOverInterest"]);
            }
            if (_host.CheckColumnIsExist(dr, "StatutoryType"))
            {
                M_temp.StatutoryType = dr["StatutoryType"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["StatutoryType"]);
            }
            return M_temp;
        }

        // RHB CUSTOM
        public List<OMSRHBBankBranchBridging> RHBBankBranch(int _bankPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSRHBBankBranchBridging> L_model = new List<OMSRHBBankBranchBridging>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                         select OMSBridgingBankBranchPK PK,BankBranchCode + '-' + BankDescription ID,BankDescription from OMSBridgingBankBranch   where BankPK = @BankPK
                        ";
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSRHBBankBranchBridging M_model = new OMSRHBBankBranchBridging();
                                    M_model.PK = Convert.ToInt32(dr["PK"]);
                                    M_model.BankBranchCode = Convert.ToString(dr["ID"]);
                                    M_model.BankDescription = Convert.ToString(dr["BankDescription"]);
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

        public List<OMSTimeDeposit> OMSTimeDeposit_BankPosition_PerFund(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSTimeDeposit> L_OMSTimeDeposit = new List<OMSTimeDeposit>();
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
	and status = 2 and FundPK = @FundPK

    Declare @DateT1 datetime  
    Declare @DateT2 datetime  
    Declare @StartDateT2 datetime

    set @DateT1 = dbo.FWorkingDay(@Date,1)  
    set @DateT2 = dbo.FWorkingDay(@Date,2)  
    set @StartDateT2 = DATEADD(DAY,1,@DateT1)

  
    Declare @AUM numeric(22,4)  
    Select @AUM = AUM From CloseNav where Date = 
	(
		Select max(Date) from CloseNAV where status = 2 and FundPK = @FundPK and Date < @Date
	)
    and FundPK = @FundPK  and status = 2
  
    set @AUM = isnull(@AUM,1)

    IF @Aum = 0
    BEGIN
    set @AUM = 1
    END

    Create table #tmpOmsDepositoPerFundBalance
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
    )             
	
    insert into #tmpOmsDepositoPerFundBalance(
    Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent
    )       
  
    Select  C.Name,sum(Balance) CurrBalance, isnull(sum(Balance)/@AUM * 100,0) [CurrNAVPercent],  
    isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(K.MatureBalance,0) Movement,
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(K.MatureBalance,0) AfterBalance,  
    isnull((sum(Balance) + isnull(D.Movement,0)  + isnull(K.MatureBalance,0)) / @AUM * 100,0) AfterNAVPercent,  
   
    isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0) MovementTOne,  
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0)  + isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0)  AfterTOne,  
    isnull((sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0)  + isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0)) / @AUM * 100,0) AfterTOneNAVPercent,  
   
    isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0) MovementTTwo,  
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(L.MatureBalance,0) + isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0) AfterTTwo,  
    isnull((sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(L.MatureBalance,0) + isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0)) / @AUM * 100,0) AfterTTwoNAVPercent  
    from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3 
    Group By B.BankPK  
    )D on B.BankPK = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where   A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @Date and FundPK = @FundPK  and C.Type = 3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK  
    )E on B.BankPK = E.BankPK  
    left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )F on B.BankPK = F.BankPK  
    left join -- T1 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance, B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK  and C.Type = 3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK   
    )G on B.BankPK = G.BankPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )H on B.BankPK = H.BankPK  
    left join -- T2 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance, B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate = @DateT2 and FundPK = @FundPK  and C.Type =3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK   
    )I on B.BankPK = I.BankPK  

    left join -- T0 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @Date and FundPK = @FundPK  and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )K on B.BankPK = K.BankPK  
    left join -- T1 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK   and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )L on B.BankPK = L.BankPK  
    left join -- T2 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT2 and FundPK = @FundPK  and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )M on B.BankPK = M.BankPK  

    where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
    and J.Type = 3 
    Group by C.Name,D.Movement,E.MaturedBalance,F.Movement,G.MaturedBalance,H.Movement,I.MaturedBalance,K.MatureBalance,L.MatureBalance,M.MatureBalance

  
    insert into #tmpOmsDepositoPerFundBalance(
    Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent
    )   
    Select C.Name,0,0,
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(F.MatureBalance,0) TESS, 
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(F.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end) + isnull(F.MatureBalance,0),0) / @AUM * 100,
    isnull(D.Movement,0) + isnull(G.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0),
    isnull(isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0),0) / @AUM * 100,
    isnull(E.Movement,0) + isnull(H.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(E.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0) + isnull(H.MatureBalance,0),
    isnull(isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(E.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0) + isnull(H.MatureBalance,0),0) / @AUM * 100
    From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
	Left join InstrumentType Z on B.InstrumentTypePK = Z.InstrumentTypePK and Z.status = 2
    left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )I on B.BankPK = I.BankPK

    left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )D on B.BankPK = D.BankPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )E on B.BankPK = E.BankPK  
    left join -- T0 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )F on B.BankPK = F.BankPK  
    left join -- T1 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )G on B.BankPK = G.BankPK  
    left join -- T2 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )H on B.BankPK = H.BankPK  
    where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
    and Z.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    and C.Name not in (
    select isnull(name,'') from #tmpOmsDepositoPerFundBalance where CurrBalance > 0	)
    Group By C.Name,D.Movement,E.Movement ,F.MatureBalance,G.MatureBalance,H.MatureBalance

    Select * from #tmpOmsDepositoPerFundBalance --where CurrBalance > 0 or Movement > 0 

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
    Declare @StartDateT2 datetime

    set @DateT1 = dbo.FWorkingDay(@Date,1)  
    set @DateT2 = dbo.FWorkingDay(@Date,2)  
    set @StartDateT2 = DATEADD(DAY,1,@DateT1)

  
    Declare @AUM numeric(22,4)  
    Select @AUM = AUM From CloseNav where Date = 
	(
		Select max(Date) from CloseNAV where status = 2 and FundPK = @FundPK and Date < @Date
	)
    and FundPK = @FundPK  and status = 2
  
    set @AUM = isnull(@AUM,1)

    IF @Aum = 0
    BEGIN
    set @AUM = 1
    END

    Create table #tmpOmsDepositoPerFundBalance
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
    )             
	
    insert into #tmpOmsDepositoPerFundBalance(
    Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent
    )       
  
    Select  C.Name,sum(Balance) CurrBalance, isnull(sum(Balance)/@AUM * 100,0) [CurrNAVPercent],  
    isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(K.MatureBalance,0) Movement,
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(K.MatureBalance,0) AfterBalance,  
    isnull((sum(Balance) + isnull(D.Movement,0)  + isnull(K.MatureBalance,0)) / @AUM * 100,0) AfterNAVPercent,  
   
    isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0) MovementTOne,  
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0)  + isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0)  AfterTOne,  
    isnull((sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0)  + isnull(F.Movement,0) + isnull(G.MaturedBalance,0) + isnull(L.MatureBalance,0)) / @AUM * 100,0) AfterTOneNAVPercent,  
   
    isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0) MovementTTwo,  
    sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(L.MatureBalance,0) + isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0) AfterTTwo,  
    isnull((sum(Balance) + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) + isnull(L.MatureBalance,0) + isnull(H.Movement,0) + isnull(I.MaturedBalance,0) + isnull(M.MatureBalance,0)) / @AUM * 100,0) AfterTTwoNAVPercent  
    from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    Left join InstrumentType J on B.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3 
    Group By B.BankPK  
    )D on B.BankPK = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where   A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @Date and FundPK = @FundPK  and C.Type = 3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK  
    )E on B.BankPK = E.BankPK  
    left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )F on B.BankPK = F.BankPK  
    left join -- T1 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance, B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK  and C.Type = 3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK   
    )G on B.BankPK = G.BankPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )H on B.BankPK = H.BankPK  
    left join -- T2 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance, B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
    Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate = @DateT2 and FundPK = @FundPK  and C.Type =3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK   
    )I on B.BankPK = I.BankPK  

    left join -- T0 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @Date and FundPK = @FundPK  and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )K on B.BankPK = K.BankPK  
    left join -- T1 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK   and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )L on B.BankPK = L.BankPK  
    left join -- T2 from Investment Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT2 and FundPK = @FundPK  and A.ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )M on B.BankPK = M.BankPK  

    where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK  
    and J.Type = 3 
    Group by C.Name,D.Movement,E.MaturedBalance,F.Movement,G.MaturedBalance,H.Movement,I.MaturedBalance,K.MatureBalance,L.MatureBalance,M.MatureBalance

  
    insert into #tmpOmsDepositoPerFundBalance(
    Name,CurrBalance,CurrNAVPercent,Movement,AfterBalance,AfterNAVPercent,MovementTOne,AfterTOne,AfterTOneNAVPercent,MovementTTwo,AfterTTwo,AfterTTwoNAVPercent
    )   
    Select C.Name,0,0,
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(F.MatureBalance,0) TESS, 
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(F.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end) + isnull(F.MatureBalance,0),0) / @AUM * 100,
    isnull(D.Movement,0) + isnull(G.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0),
    isnull(isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0),0) / @AUM * 100,
    isnull(E.Movement,0) + isnull(H.MatureBalance,0),
    isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(E.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0) + isnull(H.MatureBalance,0),
    isnull(isnull(sum(case when TrxType in(1,3) then I.Movement else I.Movement * -1 end),0) + isnull(D.Movement,0) + isnull(E.Movement,0) + isnull(F.MatureBalance,0) + isnull(G.MatureBalance,0) + isnull(H.MatureBalance,0),0) / @AUM * 100
    From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2 
	Left join InstrumentType Z on B.InstrumentTypePK = Z.InstrumentTypePK and Z.status = 2
    left join Bank C on B.BankPK = C.BankPK and C.status = 2 
	left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )I on B.BankPK = I.BankPK

    left join -- T1 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )D on B.BankPK = D.BankPK  
    left join -- T2 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate = @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )E on B.BankPK = E.BankPK  
    left join -- T0 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @Date and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )F on B.BankPK = F.BankPK  
    left join -- T1 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where  A.MaturityDate = @DateT1 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )G on B.BankPK = G.BankPK  
    left join -- T2 from Mature  
    (  
    Select isnull(sum(amount * -1),0) MatureBalance,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate > @MaxDateEndDayFP and A.MaturityDate <= @DateT2 and FundPK = @FundPK  
    and C.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    Group By B.BankPK  
    )H on B.BankPK = H.BankPK  
    where ValueDate > @MaxDateEndDayFP  and FundPK = @FundPK  
    and Z.Type = 3 and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3   
    and C.Name not in (
    select isnull(name,'') from #tmpOmsDepositoPerFundBalance where CurrBalance > 0	)
    Group By C.Name,D.Movement,E.Movement ,F.MatureBalance,G.MatureBalance,H.MatureBalance

    Select * from #tmpOmsDepositoPerFundBalance --where CurrBalance > 0 or Movement > 0 

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
                                    L_OMSTimeDeposit.Add(setOMSTimeDeposit(dr));
                                }
                            }
                            return L_OMSTimeDeposit;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<OMSExposureDeposito> OMSExposureDeposito(DateTime _date, string _fundID, int _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSExposureDeposito> L_model = new List<OMSExposureDeposito>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_fundID == "0")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"


                            Create table #tmpExposureDeposito
                            (
                            FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
                            ExposureType nvarchar(200) COLLATE DATABASE_DEFAULT,
                            Parameter nvarchar(200) COLLATE DATABASE_DEFAULT,
                            CurrentValue Decimal(22,4),
                            PotentialValue Decimal(22,4),
                            MaxValue Decimal(22,4),
                            DifferenceValue Decimal(22,4),
                            CurrentPercentage Decimal(12,4),
                            PotentialPercentage Decimal(12,4),
                            MaxPercentage decimal(12,4),
                            DifferencePercentage Decimal(12,4),
                            )

                            Create table #tmpExposureDepositoA
                            (
                            Movement Decimal(22,4),
                            BankPK int
                            )
                            Declare @EndDayTrailsFundPortfolioPK int
                            Declare @BankName nvarchar(100)
                            Declare @Parameter nvarchar(100)

                            Declare @MaxDateEndDayFP datetime
                            Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
                            endDayTrailsFundPortfolio where 
                            valuedate = 
                            (
	                            Select max(ValueDate) from endDayTrailsFundPortfolio where
	                            valuedate < @Date  and status = 2
                            )
                            and status = 2


                            Declare @TotalMarketValue numeric(26,6)

                            select @TotalMarketValue = sum(aum) From closeNav
                            where Date = (
			                            Select max(date) from CloseNAV where date < @Date and status = 2
		                            )
                            and status = 2

                            set @TotalMarketValue = case when @TotalMarketValue = 0 then 1 else isnull(@TotalMarketValue,1) end

                                if @TotalMarketValue = 1
                                BEGIN
                                    return
                                END


                            Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                            ,MaxPercentage,DifferencePercentage)

                            Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,B.BankPK Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,sum(isnull(B.CurrBal,0))/@TotalMarketValue * 100,0,MaxExposurePercent,0   
                            from FundExposure A LEFT JOIN  
                            (  
                                Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                                Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                            left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                                Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3
                                Group By B.BankPK,C.ID   
                            )B ON A.Parameter = 0   
                            where A.Type = 9 and A.Status = 2 and A.Parameter = 0
                            group By B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent

                            insert into #tmpExposureDepositoA
                            select sum(Movement) Movement,BankPK from (
                            Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                            left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                            where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
                            and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and A.MaturityDate  > @Date
                            Group By B.BankPK  
                            union all
                            Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                            Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                            where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
                            Group By B.BankPK  ) A group by BankPK



                            Insert into #tmpExposureDeposito
                            select 'ALL FUND','ALL FUND PER BANK',A.BankPK,0,A.Movement,B.MaxValue,0,0, Movement/@TotalMarketValue * 100, 
                            MaxExposurePercent,0
                            from #tmpExposureDepositoA A
                            left join FundExposure B on B.Parameter = 0 and B.Type = 9 and B.status = 2
                            where A.BankPK not in
                            (
	                            Select isnull(Parameter,0) from #tmpExposureDeposito
                            )



                            update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@TotalMarketValue * 100 from #tmpExposureDepositoA
                            where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK
                            update A set Parameter = B.Name from #tmpExposureDeposito A
                            left join Bank B on A.Parameter = B.BankPK and B.Status = 2



                            DECLARE A CURSOR FOR 
                            Select B.Name from FundExposure A 
                            left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
                            where A.status  = 2 and Type = 9 and Parameter <> 0

                            Open A
                            Fetch Next From A
                            Into @BankName
                            While @@FETCH_STATUS = 0
                            BEGIN
	                            DECLARE B CURSOR FOR 
	                            Select isnull(F.Name,'') Parameter
	                            from FundExposure A LEFT JOIN  
	                            (  
	                            Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	                            Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                            left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	                            Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3
	                            Group By B.BankPK,C.ID   
	                            )B ON A.Parameter = B.BankPK 
	                            left join -- T0 from investment  
	                            (  
	                            Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	                            left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	                            where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
	                            and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and A.MaturityDate  > @Date
	                            Group By B.BankPK  
	                            )D on A.Parameter = D.BankPK  
	                            left join -- T0 Matured   
	                            (  
	                            Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	                            Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                            where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
	                            Group By B.BankPK  
	                            )E on A.Parameter = E.BankPK
	                            left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	                            where A.Type = 9 and A.status  = 2 and A.Parameter <> 0

	                            Open B
	                            Fetch Next From B
	                            Into @Parameter
	                            While @@FETCH_STATUS = 0
	                            BEGIN
	                            IF (@BankName = @Parameter)
	                            BEGIN
	                            delete #tmpExposureDeposito where Parameter = @BankName
	                            END
	                            Fetch next From B                   
	                            Into @Parameter             
	                            end                  
	                            Close B                  
	                            Deallocate B 

                            Fetch next From A                   
                            Into @BankName             
                            end                  
                            Close A                  
                            Deallocate A 

                            Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                            ,MaxPercentage,DifferencePercentage)
                            Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
                            isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
                            isnull(B.CurrBal,0)/@TotalMarketValue * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValue * 100,MaxExposurePercent,0   

                            from FundExposure A LEFT JOIN  
                            (  
                            Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                            Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                            left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3
                            Group By B.BankPK,C.ID   
                            )B ON A.Parameter = B.BankPK 
                            left join -- T0 from investment  
                            (  
                            Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                            left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                            where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
                            and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and A.MaturityDate  > @Date
                            Group By B.BankPK  
                            )D on A.Parameter = D.BankPK  
                            left join -- T0 Matured   
                            (  
                            Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                            Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                            where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
                            Group By B.BankPK  
                            )E on A.Parameter = E.BankPK
                            left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
                            where A.Type = 9 and A.status  = 2 and A.Parameter <> 0
                        

                            update #tmpExposureDeposito set DifferencePercentage = MaxPercentage - (CurrentPercentage + PotentialPercentage)
                            ,DifferenceValue = MaxValue - (CurrentValue + PotentialValue)
                  
                       	       

                            Select FundID,ExposureType,Parameter,CurrentValue,PotentialValue,isnull(MaxValue,0) MaxValue,isnull(DifferenceValue,0) DifferenceValue
                            ,0 CurrentPercentage,0 PotentialPercentage,0 MaxPercentage,0 DifferencePercentage 
                            ,0 DifferenceAmount
                            from #tmpExposureDeposito
                            where CurrentValue <> 0 or PotentialValue <> 0
                
                
                        ";
                        }
                        else if (_type == 1)
                        {
                            if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

	                    Create table #tmpExposureDeposito
                        (
                        FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        ExposureType nvarchar(200) COLLATE DATABASE_DEFAULT,
                        Parameter nvarchar(200) COLLATE DATABASE_DEFAULT,
                        CurrentValue Decimal(22,4),
                        PotentialValue Decimal(22,4),
                        MaxValue Decimal(22,4),
                        DifferenceValue Decimal(22,4),
                        CurrentPercentage Decimal(12,4),
                        PotentialPercentage Decimal(12,4),
                        MaxPercentage decimal(12,4),
                        DifferencePercentage Decimal(12,4),
                        )

                        Create table #tmpExposureDepositoA
                        (
                        Movement Decimal(22,4),
                        BankPK int
                        )
                        Declare @EndDayTrailsFundPortfolioPK int
                        Declare @BankName nvarchar(100)
                        Declare @Parameter nvarchar(100)
   
   
   
		                    Declare @MaxDateEndDayFP datetime
		                    Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
		                    endDayTrailsFundPortfolio where 
		                    valuedate = 
		                    (
			                    Select max(ValueDate) from endDayTrailsFundPortfolio where
			                    valuedate < @Date  and status = 2 and FundPK = @FundPK
		                    )
		                    and status = 2 and FundPK = @FundPK

                        Declare @fundID nvarchar(100)
                        select @FundID = ID From Fund Where FundPK = @FundPK and status = 2
                        Declare @TotalMarketValue numeric(26,6)

                        select @TotalMarketValue = aum From closeNav
                        where Date = 
	                    (
		                    Select max(date) from CloseNAV where date < @Date and status = 2 and FundPK = @FundPK
	                    )
	                    and FundPK = @FundPK
                        and status = 2

                        set @TotalMarketValue = case when @TotalMarketValue = 0 then 1 else isnull(@TotalMarketValue,1) end

                        if @TotalMarketValue = 1
                        BEGIN
                            return
                        END
	 
	
                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select E.ID FundID,'PER FUND PER BANK' ExposureType,B.BankPK Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,
                        sum(isnull(B.CurrBal,0))/@TotalMarketValue * 100,0,MaxExposurePercent,0   
                        from FundExposure A LEFT JOIN  
                        (  
                            Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                            Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @FundPK
                            Group By B.BankPK,C.ID   
                        )B ON A.Parameter = 0 
                        Left join Fund E on A.FundPK = E.FundPK and E.status = 2   
                        where A.Type = 10 and A.Status = 2 and A.Parameter = 0 and A.FundPK = @FundPK

                        group By E.ID,B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent

                        -----ga ada posisi sebelumnya
                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
	
                        select E.ID FundID,'PER FUND PER BANK' ExposureType,BankPK Parameter,0,sum(Movement) Movement,MaxValue,0,0,0,MaxExposurePercent,0 from (
			                    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK,FundPK From Investment A  
			                    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
			                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
			                    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
			                     and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
			                    Group By B.BankPK ,FundPK 
   
			                    union all
   
			                    Select sum(Balance) * -1 MaturedBalance,B.BankPK,FundPK From [FundPosition] A  
			                    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2 
			                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
			                    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK 
			                    and C.type = 3
			                    Group By B.BankPK,FundPK  ) 
	                    A 
	                    Left join FundExposure C on A.FundPK = C.FundPK and C.status = 2 
	                    Left join Fund E on A.FundPK = E.FundPK and E.status = 2 
	                    where C.Type = 10 and C.Parameter = 0 and C.FundPK = @FundPK
	                    and BankPK not in (
	                    select isnull(Parameter,0) from #tmpExposureDeposito
	                    )
                        group by E.ID,BankPK,MaxValue,MaxExposurePercent

                        insert into #tmpExposureDepositoA
                        select sum(Movement) Movement,BankPK from (
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  
                        and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
                        Group By B.BankPK  
                        union all
                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
                        Group By B.BankPK  ) A group by BankPK



                        update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@TotalMarketValue * 100 from #tmpExposureDepositoA
                        where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK
                        update A set Parameter = B.Name from #tmpExposureDeposito A
                        left join Bank B on A.Parameter = B.BankPK and B.Status = 2

                        DECLARE A CURSOR FOR 
                        Select B.Name from FundExposure A 
                        left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
                        where A.status  = 2 and Type = 10 and Parameter <> 0

                        Open A
                        Fetch Next From A
                        Into @BankName
                        While @@FETCH_STATUS = 0
                        BEGIN
	                        DECLARE B CURSOR FOR 
	                        Select isnull(F.Name,'') Parameter
	                        from FundExposure A LEFT JOIN  
	                        (  
	                        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk
	                        Group By B.BankPK,C.ID   
	                        )B ON A.Parameter = B.BankPK 
	                        left join -- T0 from investment  
	                        (  
	                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
	                        and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @fundpk and A.MaturityDate  > @Date
	                        Group By B.BankPK  
	                        )D on A.Parameter = D.BankPK  
	                        left join -- T0 Matured   
	                        (  
	                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2
		                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2  
	                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @fundpk and C.type = 3
	                        Group By B.BankPK  
	                        )E on A.Parameter = E.BankPK
	                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	                        where A.Type = 10 and A.status  = 2 and A.Parameter <> 0

	                        Open B
	                        Fetch Next From B
	                        Into @Parameter
	                        While @@FETCH_STATUS = 0
	                        BEGIN
	                        IF (@BankName = @Parameter)
	                        BEGIN
	                        delete #tmpExposureDeposito where Parameter = @BankName
	                        END
	                        Fetch next From B                   
	                        Into @Parameter             
	                        end                  
	                        Close B                  
	                        Deallocate B 

                        Fetch next From A                   
                        Into @BankName             
                        end                  
                        Close A                  
                        Deallocate A 

                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select G.ID FundID,'PER FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
                        isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
                        isnull(B.CurrBal,0)/@TotalMarketValue * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValue * 100,MaxExposurePercent,0   

                        from FundExposure A LEFT JOIN  
                        (  
                        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk
                        Group By B.BankPK,C.ID   
                        )B ON A.Parameter = B.BankPK 
                        left join -- T0 from investment  
                        (  
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
                        and  C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK
                        Group By B.BankPK  
                        )D on A.Parameter = D.BankPK  
                        left join -- T0 Matured   
                        (  
                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
                        Group By B.BankPK  
                        )E on A.Parameter = E.BankPK
                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
                        left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
                        where A.Type = 10 and A.status  = 2 and A.Parameter <> 0

	
	                      Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select G.ID FundID,'INSTRUMENT TYPE' ExposureType,'DEPOSITO' Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
                        isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
                        isnull(B.CurrBal,0)/@TotalMarketValue * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValue * 100,MaxExposurePercent,0   

                        from FundExposure A LEFT JOIN  
                        (  
                        Select Sum(Balance) CurrBal from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 3 and A.FundPK = @fundpk
                        Group By B.InstrumentTypePK   
                        )B ON A.Parameter = 5
                        left join -- T0 from investment  
                        (  
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement
	                    From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
                        and C.Type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
                        )D on A.Parameter = 5
                        left join -- T0 Matured   
                        (  
                        Select sum(Balance) * -1 MaturedBalance From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
    
                        )E on A.Parameter = 5
                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
                        left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
                        where A.Type = 4 and A.status  = 2 and A.Parameter = 5


                        update #tmpExposureDeposito set DifferencePercentage = MaxPercentage - (CurrentPercentage + PotentialPercentage)
                        ,DifferenceValue = MaxValue - (CurrentValue + PotentialValue)
        

                        select @FundID FundID,A.ExposureType,A.Parameter,A.CurrentValue,A.PotentialValue,isnull(A.MaxValue,0) MaxValue, isnull(A.DifferenceValue,0) DifferenceValue,A.CurrentPercentage,A.PotentialPercentage,A.MaxPercentage,
	                    A.DifferencePercentage, Cast(isnull(A.DifferencePercentage * @TotalMarketValue,0) / 100 as numeric(24,4)) DifferenceAmount
	                    from #tmpExposureDeposito A
		                  
	                    where (currentValue <> 0 or PotentialValue <> 0 ) and FundID = @FundID
                                                    ";
                            }
                            else
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

	                    Create table #tmpExposureDeposito
                        (
                        FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        ExposureType nvarchar(200) COLLATE DATABASE_DEFAULT,
                        Parameter nvarchar(200) COLLATE DATABASE_DEFAULT,
                        CurrentValue Decimal(22,4),
                        PotentialValue Decimal(22,4),
                        MaxValue Decimal(22,4),
                        DifferenceValue Decimal(22,4),
                        CurrentPercentage Decimal(12,4),
                        PotentialPercentage Decimal(12,4),
                        MaxPercentage decimal(12,4),
                        DifferencePercentage Decimal(12,4),
                        )

                        Create table #tmpExposureDepositoA
                        (
                        Movement Decimal(22,4),
                        BankPK int
                        )
                        Declare @EndDayTrailsFundPortfolioPK int
                        Declare @BankName nvarchar(100)
                        Declare @Parameter nvarchar(100)
   
   
   
		                    Declare @MaxDateEndDayFP datetime
		                    Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
		                    endDayTrailsFundPortfolio where 
		                    valuedate = 
		                    (
			                    Select max(ValueDate) from endDayTrailsFundPortfolio where
			                    valuedate < @Date  and status = 2
		                    )
		                    and status = 2

                        Declare @fundID nvarchar(100)
                        select @FundID = ID From Fund Where FundPK = @FundPK and status = 2
                        Declare @TotalMarketValue numeric(26,6)

                        select @TotalMarketValue = aum From closeNav
                        where Date = 
	                    (
		                    Select max(date) from CloseNAV where date < @Date and status = 2 and FundPK = @FundPK
	                    )
	                    and FundPK = @FundPK
                        and status = 2

                        set @TotalMarketValue = isnull(@TotalMarketValue,1)

                        if @TotalMarketValue = 1
                        BEGIN
                            return
                        END
	 
	
                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select E.ID FundID,'PER FUND PER BANK' ExposureType,B.BankPK Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,
                        sum(isnull(B.CurrBal,0))/@TotalMarketValue * 100,0,MaxExposurePercent,0   
                        from FundExposure A LEFT JOIN  
                        (  
                            Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                            Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @FundPK
                            Group By B.BankPK,C.ID   
                        )B ON A.Parameter = 0 
                        Left join Fund E on A.FundPK = E.FundPK and E.status = 2   
                        where A.Type = 10 and A.Status = 2 and A.Parameter = 0 and A.FundPK = @FundPK

                        group By E.ID,B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent

                        -----ga ada posisi sebelumnya
                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
	
                        select E.ID FundID,'PER FUND PER BANK' ExposureType,BankPK Parameter,0,sum(Movement) Movement,MaxValue,0,0,0,MaxExposurePercent,0 from (
			                    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK,FundPK From Investment A  
			                    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
			                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
			                    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date
			                     and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
			                    Group By B.BankPK ,FundPK 
   
			                    union all
   
			                    Select sum(Balance) * -1 MaturedBalance,B.BankPK,FundPK From [FundPosition] A  
			                    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2 
			                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
			                    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK 
			                    and C.type = 3
			                    Group By B.BankPK,FundPK  ) 
	                    A 
	                    Left join FundExposure C on A.FundPK = C.FundPK and C.status = 2 
	                    Left join Fund E on A.FundPK = E.FundPK and E.status = 2 
	                    where C.Type = 10 and C.Parameter = 0 and C.FundPK = @FundPK
	                    and BankPK not in (
	                    select isnull(Parameter,0) from #tmpExposureDeposito
	                    )
                        group by E.ID,BankPK,MaxValue,MaxExposurePercent

                        insert into #tmpExposureDepositoA
                        select sum(Movement) Movement,BankPK from (
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  
                        and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
                        Group By B.BankPK  
                        union all
                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
                        Group By B.BankPK  ) A group by BankPK



                        update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@TotalMarketValue * 100 from #tmpExposureDepositoA
                        where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK
                        update A set Parameter = B.Name from #tmpExposureDeposito A
                        left join Bank B on A.Parameter = B.BankPK and B.Status = 2

                        DECLARE A CURSOR FOR 
                        Select B.Name from FundExposure A 
                        left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
                        where A.status  = 2 and Type = 10 and Parameter <> 0

                        Open A
                        Fetch Next From A
                        Into @BankName
                        While @@FETCH_STATUS = 0
                        BEGIN
	                        DECLARE B CURSOR FOR 
	                        Select isnull(F.Name,'') Parameter
	                        from FundExposure A LEFT JOIN  
	                        (  
	                        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk
	                        Group By B.BankPK,C.ID   
	                        )B ON A.Parameter = B.BankPK 
	                        left join -- T0 from investment  
	                        (  
	                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
	                        and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @fundpk and A.MaturityDate  > @Date
	                        Group By B.BankPK  
	                        )D on A.Parameter = D.BankPK  
	                        left join -- T0 Matured   
	                        (  
	                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2
		                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2  
	                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @fundpk and C.type = 3
	                        Group By B.BankPK  
	                        )E on A.Parameter = E.BankPK
	                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	                        where A.Type = 10 and A.status  = 2 and A.Parameter <> 0

	                        Open B
	                        Fetch Next From B
	                        Into @Parameter
	                        While @@FETCH_STATUS = 0
	                        BEGIN
	                        IF (@BankName = @Parameter)
	                        BEGIN
	                        delete #tmpExposureDeposito where Parameter = @BankName
	                        END
	                        Fetch next From B                   
	                        Into @Parameter             
	                        end                  
	                        Close B                  
	                        Deallocate B 

                        Fetch next From A                   
                        Into @BankName             
                        end                  
                        Close A                  
                        Deallocate A 

                        Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select G.ID FundID,'PER FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
                        isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
                        isnull(B.CurrBal,0)/@TotalMarketValue * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValue * 100,MaxExposurePercent,0   

                        from FundExposure A LEFT JOIN  
                        (  
                        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk
                        Group By B.BankPK,C.ID   
                        )B ON A.Parameter = B.BankPK 
                        left join -- T0 from investment  
                        (  
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
                        and  C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK
                        Group By B.BankPK  
                        )D on A.Parameter = D.BankPK  
                        left join -- T0 Matured   
                        (  
                        Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
                        Group By B.BankPK  
                        )E on A.Parameter = E.BankPK
                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
                        left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
                        where A.Type = 10 and A.status  = 2 and A.Parameter <> 0

	
	                      Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
                        ,MaxPercentage,DifferencePercentage)
                        Select G.ID FundID,'INSTRUMENT TYPE' ExposureType,'DEPOSITO' Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
                        isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
                        isnull(B.CurrBal,0)/@TotalMarketValue * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValue * 100,MaxExposurePercent,0   

                        from FundExposure A LEFT JOIN  
                        (  
                        Select Sum(Balance) CurrBal from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 3 and A.FundPK = @fundpk
                        Group By B.InstrumentTypePK   
                        )B ON A.Parameter = 5
                        left join -- T0 from investment  
                        (  
                        Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement
	                    From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date   
                        and C.Type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and A.MaturityDate  > @Date
                        )D on A.Parameter = 5
                        left join -- T0 Matured   
                        (  
                        Select sum(Balance) * -1 MaturedBalance From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
    
                        )E on A.Parameter = 5
                        left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
                        left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
                        where A.Type = 4 and A.status  = 2 and A.Parameter = 5


                        update #tmpExposureDeposito set DifferencePercentage = MaxPercentage - (CurrentPercentage + PotentialPercentage)
                        ,DifferenceValue = MaxValue - (CurrentValue + PotentialValue)
        

                        select @FundID FundID,A.ExposureType,A.Parameter,A.CurrentValue,A.PotentialValue,isnull(A.MaxValue,0) MaxValue, isnull(A.DifferenceValue,0) DifferenceValue,A.CurrentPercentage,A.PotentialPercentage,A.MaxPercentage,
	                    A.DifferencePercentage, Cast(isnull(A.DifferencePercentage * @TotalMarketValue,0) / 100 as numeric(24,4)) DifferenceAmount
	                    from #tmpExposureDeposito A
		                  
	                    where (currentValue <> 0 or PotentialValue <> 0 ) and FundID = @FundID
                                                    ";
                            }
                            
                            cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        }
                        
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSExposureDeposito M_model = new OMSExposureDeposito();
                                    M_model.FundID = Convert.ToString(dr["FundID"]);
                                    M_model.ExposureType = Convert.ToString(dr["ExposureType"]);
                                    M_model.Parameter = Convert.ToString(dr["Parameter"]);
                                    M_model.CurrentValue = Convert.ToDecimal(dr["CurrentValue"]);
                                    M_model.PotentialValue = Convert.ToDecimal(dr["PotentialValue"]);
                                    M_model.MaxValue = Convert.ToDecimal(dr["MaxValue"]);
                                    M_model.DifferenceValue = Convert.ToDecimal(dr["DifferenceValue"]);
                                    M_model.CurrentPercentage = Convert.ToDecimal(dr["CurrentPercentage"]);
                                    M_model.PotentialPercentage = Convert.ToDecimal(dr["PotentialPercentage"]);
                                    M_model.MaxPercentage = Convert.ToDecimal(dr["MaxPercentage"]);
                                    M_model.DifferencePercentage = Convert.ToDecimal(dr["DifferencePercentage"]);
                                    M_model.DifferenceAmount = Convert.ToDecimal(dr["DifferenceAmount"]);
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

        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDepositoExposurePotentialDetail(DateTime _date, string _bankName, string _fundID, string _exposureType)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
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
	and status = 2 and FundPK = @FundPK

if @ExposureType  = 'ALL FUND PER BANK'
BEGIN
     
   
Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,   
B.ID,A.MaturityDate,A.InterestPercent From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and C.Name = @BankName  
and A.InstrumentTypePK = 5 and StatusInvestment <> 3   
     
UNION ALL    
     
Select Balance * -1 Balance,  
B.ID,B.MaturityDate,A.InterestPercent   
From [FundPosition] A  
Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where A.MaturityDate = @Date and C.Name = @BankName  
END
if @ExposureType  = 'PER FUND PER BANK'
BEGIN
Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,   
B.ID,A.MaturityDate,A.InterestPercent From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
left join Fund D on A.FundPK = D.FundPK and D.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and C.Name = @BankName  
and A.InstrumentTypePK = 5 and StatusInvestment <> 3    and D.ID = @FundID
     
UNION ALL    
     
Select Balance * -1 Balance,  
B.ID,A.MaturityDate,A.InterestPercent   
From [FundPosition] A  
Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where A.MaturityDate = @Date and C.Name = @BankName  and A.FundID = @FundID

END
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

if @ExposureType  = 'ALL FUND PER BANK'
BEGIN
     
   
Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,   
B.ID,A.MaturityDate,A.InterestPercent From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and C.Name = @BankName  
and A.InstrumentTypePK = 5 and StatusInvestment <> 3   
     
UNION ALL    
     
Select Balance * -1 Balance,  
B.ID,B.MaturityDate,A.InterestPercent   
From [FundPosition] A  
Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where A.MaturityDate = @Date and C.Name = @BankName  
END
if @ExposureType  = 'PER FUND PER BANK'
BEGIN
Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,   
B.ID,A.MaturityDate,A.InterestPercent From Investment A  
left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
left join Fund D on A.FundPK = D.FundPK and D.Status = 2
where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and C.Name = @BankName  
and A.InstrumentTypePK = 5 and StatusInvestment <> 3    and D.ID = @FundID
     
UNION ALL    
     
Select Balance * -1 Balance,  
B.ID,A.MaturityDate,A.InterestPercent   
From [FundPosition] A  
Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
where A.MaturityDate = @Date and C.Name = @BankName  and A.FundID = @FundID

END
                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ExposureType", _exposureType);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@FundID", _fundID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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


        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDeposito_ExposureDetailCurrentBalance(DateTime _date, string _bankName, string _fundID, string _exposureType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                      Declare @EndDayTrailsFundPortfolioPK int
	                    Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
	                    and Status = 2 and FundPK = @FundPK
  
                    if @ExposureType  = 'ALL FUND PER BANK'
                    BEGIN
     
   
                        Select Balance, B.ID,A.MaturityDate,A.InterestPercent from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        Where A.FundPositionPK  = @EndDayTrailsFundPortfolioPK and C.Name = @BankName  
                    END
                    if @ExposureType  = 'PER FUND PER BANK'
                    BEGIN
     
                        Select Balance, B.ID,A.MaturityDate,A.InterestPercent from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        Where A.FundPositionPK  = @EndDayTrailsFundPortfolioPK and C.Name = @BankName  
	                    and A.FundID = @FundID
                    END
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                      Declare @EndDayTrailsFundPortfolioPK int
	                    Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
	                    and Status = 2 
  
                    if @ExposureType  = 'ALL FUND PER BANK'
                    BEGIN
     
   
                        Select Balance, B.ID,A.MaturityDate,A.InterestPercent from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        Where A.FundPositionPK  = @EndDayTrailsFundPortfolioPK and C.Name = @BankName  
                    END
                    if @ExposureType  = 'PER FUND PER BANK'
                    BEGIN
     
                        Select Balance, B.ID,A.MaturityDate,A.InterestPercent from [FundPosition] A  
                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
                        Where A.FundPositionPK  = @EndDayTrailsFundPortfolioPK and C.Name = @BankName  
	                    and A.FundID = @FundID
                    END
                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ExposureType", _exposureType);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@FundID", _fundID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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


        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDeposito_InstrumentDetailPerFundPerBankT0(int _fundPK, string _bankName, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 and FundPK = @FundPK
                         select B.ID,A.InterestPercent,A.MaturityDate,A.Balance from [FundPosition] A    
                         Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                         Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                         where C.Name = @bankName and A.FundPK = @FundPK and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 
                         select B.ID,A.InterestPercent,A.MaturityDate,A.Balance from [FundPosition] A    
                         Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                         Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                         where C.Name = @bankName and A.FundPK = @FundPK and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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

        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDeposito_InstrumentDetailPerFundPerBankT0Movement(int _fundPK, string _bankName, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
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
	            and status = 2 and FundPK = @FundPK


                Select Balance * -1 Balance, B.ID,A.MaturityDate,A.InterestPercent From [FundPosition] A  
                Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                where A.MaturityDate = @Date and FundPK = @FundPK and C.Name = @BankName  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
     
                UNION ALL    
     
                Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,  
                B.ID,A.MaturityDate,A.InterestPercent  
                From Investment A  
                left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  and A.MaturityDate > @Date
                and A.InstrumentTypePK = 5 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3   
                and C.Name = @BankName  
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


                Select Balance * -1 Balance, B.ID,A.MaturityDate,A.InterestPercent From [FundPosition] A  
                Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                where A.MaturityDate = @Date and FundPK = @FundPK and C.Name = @BankName  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
     
                UNION ALL    
     
                Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,  
                B.ID,A.MaturityDate,A.InterestPercent  
                From Investment A  
                left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and FundPK = @FundPK  and A.MaturityDate > @Date
                and A.InstrumentTypePK = 5 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3   
                and C.Name = @BankName  
                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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

        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDeposito_InstrumentDetailPerFundPerBankT1Movement(int _fundPK, string _bankName, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @DateT1 datetime  

  
                        set @DateT1 = dbo.FWorkingDay(@Date,1)  

                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 and FundPK = @FundPK
 
                        Select amount * -1 Balance,  
                        B.ID,A.MaturityDate,A.InterestPercent  
                        From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where ValueDate = @Date and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3    
                        and C.Name = @BankName  and A.MaturityDate = @DateT1
                        UNION ALL


                        Select Balance * -1 Balance, B.ID,A.MaturityDate,A.InterestPercent From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where A.MaturityDate = @DateT1 and FundPK = @FundPK and C.Name = @BankName  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
     
                        UNION ALL    
     
                        Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,  
                        B.ID,A.MaturityDate,A.InterestPercent  
                        From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where ValueDate = @DateT1 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        and C.Name = @BankName
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @DateT1 datetime  

  
                        set @DateT1 = dbo.FWorkingDay(@Date,1)  

                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 
 
                        Select amount * -1 Balance,  
                        B.ID,A.MaturityDate,A.InterestPercent  
                        From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where ValueDate = @Date and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3    
                        and C.Name = @BankName  and A.MaturityDate = @DateT1
                        UNION ALL


                        Select Balance * -1 Balance, B.ID,A.MaturityDate,A.InterestPercent From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where A.MaturityDate = @DateT1 and FundPK = @FundPK and C.Name = @BankName  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
     
                        UNION ALL    
     
                        Select isnull(case when TrxType in(1,3) then Amount else amount * -1 end,0) Balance,  
                        B.ID,A.MaturityDate,A.InterestPercent  
                        From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        Left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                        where ValueDate = @DateT1 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        and C.Name = @BankName
                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@DateT1", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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

        public List<OMSDepositoInstrumentDetailPerFundPerBank> OMSDeposito_InstrumentDetailPerFundPerBankT2Movement(int _fundPK, string _bankName, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     
                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 and FundPK = @FundPK
                        Declare @DateT2 datetime  
  
                        set @DateT2 = dbo.FWorkingDay(@Date,2)  

                        Declare @DateT1 datetime  
  
                        set @DateT1 = dbo.FWorkingDay(@Date,1)  
    
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Balance From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        where ValueDate = @DateT2 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        Group By B.ID,A.InterestPercent,A.MaturityDate  
                        UNION ALL 
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(Balance),0) Balance From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                        Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate = @DateT2 and FundPK = @FundPK  and C.Type =3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
                        Group By B.ID,A.InterestPercent,A.MaturityDate  
                        UNION ALL 
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(Amount * -1),0) Balance From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        where A.MaturityDate = @DateT2 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        Group By B.ID,A.InterestPercent,A.MaturityDate  

                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                     
                        Declare @EndDayTrailsFundPortfolioPK int
                        Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK From EndDayTrailsFundPortfolio Where ValueDate = dbo.Fworkingday(@Date,-1)
                        and Status = 2 
                        Declare @DateT2 datetime  
  
                        set @DateT2 = dbo.FWorkingDay(@Date,2)  

                        Declare @DateT1 datetime  
  
                        set @DateT1 = dbo.FWorkingDay(@Date,1)  
    
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Balance From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        where ValueDate = @DateT2 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        Group By B.ID,A.InterestPercent,A.MaturityDate  
                        UNION ALL 
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(Balance),0) Balance From [FundPosition] A  
                        Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
                        Left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        where A.MaturityDate = @DateT2 and FundPK = @FundPK  and C.Type =3 and A.TrailsPK = @EndDayTrailsFundPortfolioPK
                        Group By B.ID,A.InterestPercent,A.MaturityDate  
                        UNION ALL 
                        Select B.ID,A.InterestPercent,A.MaturityDate,isnull(sum(Amount * -1),0) Balance From Investment A  
                        left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
                        where A.MaturityDate = @DateT2 and FundPK = @FundPK  
                        and A.InstrumentTypePK = 5 and StatusInvestment <> 3  and StatusDealing <> 3 and StatusSettlement <> 3    
                        Group By B.ID,A.InterestPercent,A.MaturityDate  

                        ";
                        }
                        
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@BankName", _bankName);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSDepositoInstrumentDetailPerFundPerBank M_model = new OMSDepositoInstrumentDetailPerFundPerBank();
                                    M_model.InstrumentID = Convert.ToString(dr["ID"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
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

        public Boolean Export_OMSTimeDeposit(string _userID, DateTime _dateFrom)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText =
                            @"
     select F.ID Product,OB.BankBranchCode Instrument,isnull(OB.CounterPaty,'') CounterParty,valuedate DealDate,I.MaturityDate MaturityDate,Volume Principal,
                            cast(I.InterestPercent as numeric(8,2)) Rate ,'20' TaxPercentage,0 IsBreakable,InvestmentNotes InstructionNo, 
							isnull(FC.BankAccountNo,'') BankAccountNo,1 ContractRate,'MONHTLY' InterestFreq,'NULL' Remarks
							from investment IV
                            left join Fund F on IV.FundPK = F.FundPK and F.Status = 2
                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join Bank B on I.BankPK = B.BankPK and B.status = 2 
                            left join OMSBridgingInstrument O on B.bankPK = O.BankPK and I.CurrencyPK = O.CurrencyPK and IV.Category = O.category
                            left join OMSBridgingBankBranch OB on IV.BankBranchPK = OB.OMSBridgingBankBranchPK 
							left join FundCashRef FC on F.DefaultFundCashRefPK = FC.FundCashRefPK and FC.status = 2
                            where statusInvestment = 2 and ValueDate = @Date

                                ";


                        
                        cmd.Parameters.AddWithValue("@Date", _dateFrom);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ExportOMSTimeDeposit" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "ExportOMSTimeDeposit";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Export OMS TimeDeposit");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<OMSTimeDepositRpt> rList = new List<OMSTimeDepositRpt>();
                                    while (dr0.Read())
                                    {

                                        OMSTimeDepositRpt rSingle = new OMSTimeDepositRpt();
                                        rSingle.FundID = Convert.ToString(dr0["Product"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["Instrument"]);
                                        rSingle.BankID = Convert.ToString(dr0["CounterParty"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["DealDate"]);
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                        rSingle.Volume = Convert.ToDecimal(dr0["Principal"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["Rate"]);
                                        rSingle.TaxPercentage = Convert.ToDecimal(dr0["TaxPercentage"]);
                                        rSingle.IsBreakable = Convert.ToInt32(dr0["IsBreakable"]);
                                        rSingle.Reference = Convert.ToString(dr0["InstructionNo"]);
                                        rSingle.BankAccountNo = Convert.ToString(dr0["BankAccountNo"]);
                                        rSingle.ContractRate = Convert.ToString(dr0["ContractRate"]);
                                        rSingle.InterestFreq = Convert.ToString(dr0["InterestFreq"]);
                                        rSingle.Remarks = Convert.ToString(dr0["Remarks"]);
                                        rList.Add(rSingle);

                                    }
                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Value = "PRODUCT";
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Value = "INSTRUMENT";
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 3].Value = "COUNTERPARTY";
                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 4].Value = "DEAL DATE";
                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 5].Value = "MATURITY DATE";
                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 6].Value = "PRINCIPAL";
                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 7].Value = "RATE";
                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 8].Value = "TAX PERCENTAGE";
                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 9].Value = "IS BREAKABLE";
                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 10].Value = "INSTRUCTION NO";
                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                    
                                    worksheet.Cells[incRowExcel, 11].Value = "ACCOUNT NUMBER";
                                    worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;

                                    worksheet.Cells[incRowExcel, 12].Value = "CONTRACT RATE";
                                    worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;

                                    worksheet.Cells[incRowExcel, 13].Value = "INTEREST FREQUENCY";
                                    worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;

                                    worksheet.Cells[incRowExcel, 14].Value = "REMARKS";
                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;

                                    string _range = "A" + incRowExcel + ":N" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                        r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                        r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                        r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                    }
                                    incRowExcel++;

                                    var QueryByClientID =
                                     from r in rList
                                     group r by new { r.FundID } into rGroup
                                     select rGroup;



                                    foreach (var rsHeader in QueryByClientID)
                                    {

                                        //incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;




                                        //incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        // Untuk Cetak Tebal


                                        //area header

                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundID;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankID;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.SettlementDate;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "MM/dd/yyyy";
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.MaturityDate;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "MM/dd/yyyy";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Volume;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.InterestPercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.TaxPercentage;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.IsBreakable;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Reference;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.BankAccountNo;
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.ContractRate;
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.InterestFreq;
                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.Remarks;
                                            incRowExcel++;



                                        }


                                    }



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    //worksheet.PrinterSettings.FitToHeight = 0;
                                    //worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 10];
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.Column(11).AutoFit();
                                    worksheet.Column(12).AutoFit();
                                    worksheet.Column(13).AutoFit();
                                    worksheet.Column(14).AutoFit();
                                    worksheet.Column(15).AutoFit();

                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Export OMS TimeDeposit";




                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
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

        public decimal OMSTimeDeposite_GetRollOverInterestByDeposito(Investment _investment)
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
                        declare @InterestAmount numeric(22,4)
						declare @DepositoTaxPersen numeric(10,4)

						select @DepositoTaxPersen = TaxPercentageTD from FundAccountingSetup where fundpk = @FundPK and status = 2

                        Select @InterestAmount = SUM(ISNULL(@Balance * @InterestPercent / 100 /CASE WHEN @InterestDaysType = 4 then 365 ELSE 360 END,0))  *  datediff(day,@AcqDate,@MaturityDate) * ((100 - @DepositoTaxPersen) / 100)

                        select isnull(@InterestAmount,0) + isnull(@Balance,0) RollOverInterest  
                        ";

                        cmd.Parameters.AddWithValue("@InterestPaymentType", _investment.InterestPaymentType);
                        cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _investment.PaymentModeOnMaturity);
                        cmd.Parameters.AddWithValue("@InterestDaysType", _investment.InterestDaysType);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@InterestPercent", _investment.InterestPercent);
                        cmd.Parameters.AddWithValue("@Balance", _investment.Amount);
                        cmd.Parameters.AddWithValue("@ValueDate", _investment.ValueDate);
                        cmd.Parameters.AddWithValue("@AcqDate", _investment.@AcqDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _investment.@MaturityDate);
                        cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["RollOverInterest"]);

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

        public int Validate_ApproveBySelectedDataOMSTimeDeposit(Investment _investment)
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
                        string _paramTrxType = "";
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
                            _paramFund = " And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_investment.TrxType == 4)
                        {
                            _paramTrxType = " and TrxType in (1,2,3) ";
                        }
                        else if (_investment.TrxType == 1)
                        {
                            _paramTrxType = " and TrxType in (1,3) ";
                        }
                        else
                        {
                            _paramTrxType = " and TrxType = @TrxType ";
                        }
                        cmd.CommandTimeout = 0;

                        if (Tools.ClientCode == "03")
                        {
                            cmd.CommandText = @"
                        if Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = '' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )      
                        BEGIN 
                        Select 2 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 4 Result 
                        END 

                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
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
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = '' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )      
                        BEGIN 
                        Select 2 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'M' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
                        BEGIN 
                        Select 5 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )     
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
                        if (_investment.TrxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        }
                        //cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
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

        public int Validate_RejectBySelectedDataOMSTimeDeposit(Investment _investment)
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
                        string _paramTrxType = "";
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
                            _paramFund = " And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_investment.TrxType == 4)
                        {
                            _paramTrxType = " and TrxType in (1,2,3)";
                        }
                        if (_investment.TrxType == 1)
                        {
                            _paramTrxType = " and TrxType in (1,3)";
                        }
                        else
                        {
                            _paramTrxType = " and TrxType = @TrxType ";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        if Exists
                        (select * From Investment where  StatusInvestment = 3 and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )        
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and ValueDate between @ValueDateFrom and @ValueDateTo  " + _paramTrxType + " and InstrumentTypePK in (5,10) " + _paramInvestmentPK + _paramFund + @" )         
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and ValueDate between @ValueDateFrom and @ValueDateTo  " + _paramTrxType + " and InstrumentTypePK in (5,10) " + _paramInvestmentPK + _paramFund + @" )         
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'M' and ValueDate between @ValueDateFrom and @ValueDateTo  " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )         
                        BEGIN 
                        Select 5 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramTrxType + " and InstrumentTypePK  in (5,10) " + _paramInvestmentPK + _paramFund + @" )         
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
                        if (_investment.TrxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        }
                        //cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
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

        public int Investment_ApproveOMSTimeDepositBySelected(Investment _investment)
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
                        string _paramTrxType = "";
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
                            _paramFund = " and FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_investment.TrxType == 4)
                        {
                            _paramTrxType = " and TrxType in (1,2,3) ";
                        }
                        else if (_investment.TrxType == 1)
                        {
                            _paramTrxType = " and TrxType in (1,3) ";
                        }
                        else
                        {
                            _paramTrxType = " and TrxType = @TrxType ";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @investmentPK int
                        declare @historyPK int
                        declare @DealingPK int
                        declare @Notes nvarchar(500)
                        declare @OrderPrice numeric(22,8)
                        declare @Volume numeric(22,0)
                        declare @Amount numeric(22,0)
                        declare @AccruedInterest numeric(22,0)
                        declare @BankBranchPK int

                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = '' and Status = 2   
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)   
                        Select @Time,'InvestmentInstruction_RejectOMSEquityBySelected','Investment',InvestmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Investment where ValueDate between @DateFrom and @DateTo and statusInvestment = 1 " + _paramInvestmentPK + "  and  InstrumentTypePK = 5  " + _paramTrxType + _paramFund +

                        
                        @" DECLARE A CURSOR FOR 
	                            Select InvestmentPK,DealingPK,HistoryPK,InvestmentNotes,OrderPrice,Volume,Amount,AccruedInterest,BankBranchPK From investment 
	                            where statusInvestment = 1 and ValueDate between @datefrom and @datefrom "+ _paramInvestmentPK + " and  InstrumentTypePK in (5,10) " + _paramTrxType + _paramFund +

                        @" Open A
                        Fetch Next From A
                        Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@BankBranchPK

                        While @@FETCH_STATUS = 0
                        BEGIN
                        Select @DealingPK = max(DealingPK) + 1 From investment
                        if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                        update Investment set DealingPK = @DealingPK, statusInvestment = 2, statusDealing = 1,InvestmentNotes=@Notes,DonePrice=@OrderPrice,DoneVolume=@Volume,DoneAmount=@Amount,BankBranchPK=@BankBranchPK,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,EntryDealingID = @ApprovedUsersID,EntryDealingTime = @ApprovedTime ,LastUpdate=@LastUpdate
                        where InvestmentPK = @InvestmentPK
                        Fetch next From A Into @investmentPK,@DealingPK,@historyPK,@Notes,@OrderPrice,@Volume,@Amount,@AccruedInterest,@BankBranchPK
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
                        //cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        if (_investment.TrxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        }
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

        public int Investment_RejectOMSTimeDepositBySelected(Investment _investment)
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
                        string _paramTrxType = "";
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
                            _paramFund = " and FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_investment.TrxType == 4)
                        {
                            _paramTrxType = " and TrxType in (1,2,3) ";
                        }
                        else if (_investment.TrxType == 1)
                        {
                            _paramTrxType = " and TrxType in (1,3) ";
                        }
                        else
                        {
                            _paramTrxType = " and TrxType = @TrxType ";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"Update Investment set StatusInvestment  = 3,statusDealing = 0,OrderStatus = 'R', VoidUsersID = @VoidUsersID,VoidTime = @VoidTime
                            where InstrumentTypePK in (5,10) " + _paramTrxType + _paramInvestmentPK + " and ValueDate between @DateFrom and @DateTo and statusInvestment in  (1,2) " + _paramFund +
                            " --Update Investment set SelectedInvestment  = 0";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        //cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        if (_investment.TrxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        }
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

        public decimal OMSTimeDepositGetDealingNetBuySellTimeDeposit(DateTime _date, int _fundPK, int _counterpartPK)
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

                        Select sum(DoneAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus in ('M','P') and TrxType  = 1 and InstrumentTypePK in (5)

                        Insert into #NetBuySell (Amount) 

                        Select DoneAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus  in ('M','P') and TrxType  = 2 and InstrumentTypePK in (5)

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

        public decimal OMSTimeDepositGetSettlementNetBuySellTimeDeposit(DateTime _date, int _fundPK, int _counterpartPK)
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

                        Select sum(TotalAmount * -1) from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2  and TrxType  = 1 and InstrumentTypePK in (5)
                        Insert into #NetBuySell (TotalAmount) 

                        Select TotalAmount from Investment where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusSettlement  = 2 and TrxType  = 2 and InstrumentTypePK in (5)

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

        public List<OMSTimeDepositByInstrument> OMSTimeDepositByInstrument(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSTimeDepositByInstrument> L_model = new List<OMSTimeDepositByInstrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                        Declare @TotalMarketValue numeric(26,6)
                        declare @instrumentPK int
                        declare @Balance numeric(18,0)
                        declare @InterestPercent numeric(18,4)
                        declare @AcqDate datetime
                        declare @MaturityDate datetime   
                  
                        select @TotalMarketValue = aum From closeNav
                        where Date = dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                        and status = 2
  
   
                        Create table #OMSTimeDepositByAllInstrument
                        (
                        InstrumentPK int,
                        InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        BankID nvarchar(100) COLLATE DATABASE_DEFAULT,
                        Category nvarchar(100) COLLATE DATABASE_DEFAULT,
                        Balance numeric(18,0),
                        InterestPercent numeric(18,4),
                        InterestDays int,
                        AcqDate datetime,
                        MaturityDate datetime,
                        TaxPercent numeric(18,4),
                        Net numeric(18,4),
                        Status int
                        )
                        	
                        Insert into #OMSTimeDepositByAllInstrument(InstrumentPK,InstrumentID,BankID,Category,Balance,InterestPercent,InterestDays,AcqDate,MaturityDate,TaxPercent,Net,Status)
                        Select A.InstrumentPK,A.InstrumentID,H.Name,G.Category,
                        A.balance + isnull(
                        case when D.TrxType = 1 then isnull(D.DoneVolume,0) else
                        case when D.trxType = 2 then D.DoneVolume * -1 end end ,0)
                        + isnull(
                        case when E.TrxType = 1 then E.DoneVolume else
                        case when E.trxType = 2 then E.DoneVolume * -1 end end ,0)
                        ,   
                        B.InterestPercent, B.Days,A.AcqDate,B.MaturityDate,B.TaxExpensePercent,0,isnull(case when D.TrxType = 1 then 1 else case when E.TrxType = 2 then 1 else 0  end end,0)
                        From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        -- sisi buy dulu
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (5)
                        and TrxType = 1 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )D on A.InstrumentPK = D.InstrumentPK
                        -- sisi Sell
                        left join (
                        Select TrxType,sum(volume) Volume, sum(DoneVolume) DoneVolume,InstrumentPK from Investment where ValueDate = @Date
                        and StatusInvestment <> 3 and StatusDealing <> 3 and instrumentTypePK in (5)
                        and TrxType = 2 and FundPK = @FundPK
                        group by InstrumentPK,TrxType
                        )E on A.InstrumentPK = E.InstrumentPK
                        left join InstrumentType F on B.InstrumentTypePK = F.InstrumentTypePK and F.status = 2
                        left join Instrument G on A.InstrumentPK = G.InstrumentPK and G.status = 2
                        left join Bank H on H.BankPK = G.BankPK and H.Status = 2 
                        where A.Date = dbo.FWorkingDay(@Date,-1) and F.Type in (3) and A.FundPK = @FundPK and A.status  = 2
                        order by MaturityDate
                        
                        Insert into #OMSTimeDepositByAllInstrument(InstrumentPK,InstrumentID,BankID,Category,Balance,InterestPercent,InterestDays,AcqDate,MaturityDate,TaxPercent,Net,Status)
                        Select A.InstrumentPK,B.ID, C.Name,B.Category,
                        case when A.TrxType in (1,3) then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,  
                        A.InterestPercent,B.Days,A.ValueDate,A.MaturityDate,B.TaxExpensePercent,0 ,1
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Bank C on C.BankPK = B.BankPK and C.Status = 2 
                        where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and orderstatus = 'M' and A.instrumentTypePK  in (5)
                        and FundPK = @FundPK and A.TrxType in (1,3)



                        union all

                        Select B.InstrumentPK,B.ID, C.Name, B.Category,
                        case when A.TrxType in (1,3) then A.DoneVolume else
                        case when A.trxType = 2 then A.DoneVolume * -1 end end,    
                        A.InterestPercent,B.Days,A.ValueDate,A.MaturityDate,B.TaxExpensePercent,0 ,1
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join Bank C on C.BankPK = B.BankPK and C.Status = 2 
                        where ValueDate = @Date and StatusInvestment <> 3 and StatusDealing <> 3 and statussettlement <> 3 and A.instrumentTypePK  in (5)
                        and FundPK = @FundPK and A.TrxType = 2
                        order by MaturityDate


                        Declare B Cursor For                  

                        Select A.InstrumentPK,A.Balance,A.InterestPercent,A.AcqDate,A.MaturityDate
                        From #OMSTimeDepositByAllInstrument A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        Group by A.InstrumentPK,A.Balance,A.InterestPercent,A.MaturityDate,A.AcqDate
                        having sum(A.balance) > 0
                        order by MaturityDate asc           

              
                        Open B                  

                        Fetch Next From B                  

                        Into @InstrumentPK,@Balance,@InterestPercent,@AcqDate,@MaturityDate                  

                        While @@FETCH_STATUS = 0                  

                        Begin  

                        if Exists
                        (Select InstrumentPK,Balance,InterestPercent,AcqDate,MaturityDate
                        From RealizedOMSTimeDeposit where InstrumentPK = @InstrumentPK and Balance=@Balance and InterestPercent=@InterestPercent and AcqDate=@AcqDate and MaturityDate=@MaturityDate and ValueDate =@date and FundPK = @FundPK)
                        BEGIN
                        update RealizedOMSTimeDeposit set InstrumentPK = @InstrumentPK, Balance=@Balance,InterestPercent=@InterestPercent,AcqDate=@AcqDate,MaturityDate=@MaturityDate
                        where InstrumentPK = @InstrumentPK and Balance=@Balance and InterestPercent=@InterestPercent and AcqDate=@AcqDate and MaturityDate=@MaturityDate
                        END
                        ELSE
                        BEGIN
                        Insert Into RealizedOMSTimeDeposit (ValueDate,FundPK,InstrumentPK,Balance,InterestPercent,AcqDate,MaturityDate)
                        Select @Date,@FundPK,A.InstrumentPK,A.Balance,A.InterestPercent,A.AcqDate,A.MaturityDate
                        From #OMSTimeDepositByAllInstrument A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        Group by A.InstrumentPK,A.Balance,A.InterestPercent,A.MaturityDate,A.AcqDate
                        having sum(A.balance) > 0
                        order by MaturityDate asc
                        END
                        Fetch next From B                   

                        Into @InstrumentPK,@Balance,@InterestPercent,@AcqDate,@MaturityDate            

                        end                  

                        Close B                  

                        Deallocate B 
 
                       
                        Select A.InstrumentPK,InstrumentID,BankID,A.Category,isnull(sum(A.Balance) / @TotalMarketValue * 100,0) CurrentExposure,
                        sum(A.Balance) Balance,A.InterestPercent,A.InterestDays,A.AcqDate,A.MaturityDate,TaxPercent,
                        case when A.AcqDate >= case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end
                        then A.AcqDate else
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end end NisbahDateFrom,
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) + 1,A.MaturityDate) end NisbahDateTo,
                        DATEDIFF(day, case when A.AcqDate >= case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end
                        then A.AcqDate else
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end end, case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) + 1,A.MaturityDate) end) Tenor,
                        dbo.FGetDepositoInterestAccrued_EMCO(A.InstrumentPK,sum(A.Balance),DATEDIFF(day, case when A.AcqDate >= case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end
                        then A.AcqDate else
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end end, case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) + 1,A.MaturityDate) end)) Net,A.Status,isnull(sum(C.RealizedAmount),0) RealizedAmount, sum(dbo.FGetDepositoInterestAccrued_EMCO(A.InstrumentPK,A.Balance,DATEDIFF(day, case when A.AcqDate >= case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end
                        then A.AcqDate else
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end end, case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) + 1,A.MaturityDate) end)) - isnull(C.RealizedAmount,0)) Difference,isnull(sum(C.RealInterestPercent),0) RealInterestPercent

                        From #OMSTimeDepositByAllInstrument A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        left join RealizedOMSTimeDeposit C on A.InstrumentPK = C.InstrumentPK and A.Balance = C.Balance and A.InterestPercent = C.InterestPercent and A.AcqDate = C.AcqDate and A.MaturityDate = C.MaturityDate
                        Group by A.InstrumentPK,InstrumentID,BankID,A.Category,A.InterestPercent,A.InterestDays,A.AcqDate,A.MaturityDate,TaxPercent,A.status
                        order by MaturityDate asc                             
                                                ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSTimeDepositByInstrument M_model = new OMSTimeDepositByInstrument();
                                    M_model.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.BankID = Convert.ToString(dr["BankID"]);
                                    M_model.Category = Convert.ToString(dr["Category"]);
                                    M_model.CurrentExposure = Convert.ToDecimal(dr["CurrentExposure"]);
                                    M_model.Volume = Convert.ToDecimal(dr["Balance"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.InterestDays = Convert.ToInt32(dr["InterestDays"]);
                                    M_model.AcqDate = Convert.ToDateTime(dr["AcqDate"]);
                                    M_model.MaturityDate = Convert.ToDateTime(dr["MaturityDate"]);
                                    M_model.TaxPercent = Convert.ToDecimal(dr["TaxPercent"]);
                                    M_model.NisbahDateFrom = Convert.ToDateTime(dr["NisbahDateFrom"]);
                                    M_model.NisbahDateTo = Convert.ToDateTime(dr["NisbahDateTo"]);
                                    M_model.Tenor = Convert.ToInt32(dr["Tenor"]);
                                    M_model.Net = Convert.ToInt32(dr["Net"]);
                                    M_model.RealizedAmount = Convert.ToDecimal(dr["RealizedAmount"]);
                                    M_model.Difference = Convert.ToDecimal(dr["Difference"]);
                                    M_model.RealInterestPercent = Convert.ToDecimal(dr["RealInterestPercent"]);
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

        public int Validate_UpdateDataOMSTimeDeposit(int _investmentPK)
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
                        if Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = '' and InvestmentPK = @InvestmentPK )      
                        BEGIN 
                        Select 2 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 3 and InvestmentPK = @InvestmentPK )  
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'R' and InvestmentPK = @InvestmentPK )  
                        BEGIN 
                        Select 3 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'O' and InvestmentPK = @InvestmentPK )  
                        BEGIN 
                        Select 4 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'M' and InvestmentPK = @InvestmentPK )    
                        BEGIN 
                        Select 5 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusInvestment = 2 and OrderStatus = 'P' and InvestmentPK = @InvestmentPK )  
                        BEGIN 
                        Select 6 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";

                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);
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

        public int Validate_CheckAvailableInstrument(DateTime _valuedate, int _instrumentPK, int _fundPK, decimal _amount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                       
	
                    Declare @CurrBalance numeric (18,4)
  
                    Declare @TrailsPK int
                    Declare @MaxDateEndDayFP datetime

                    select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                    where ValueDate = 
                    (
	                    select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date and FundPk = @FundPK
                    )
                    and status = 2 and FundPK = @FundPK

                    select @CurrBalance = A.Balance + isnull(F.Balance,0)
                    from FundPosition A    
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                    left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                    left join
                    (

	                    Select A.InstrumentPK,A.BankBranchPK,sum(DoneVolume) * -1 Balance
	                    from Investment A
	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                        Left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	                    where ValueDate = @date and StatusInvestment = 1 and StatusDealing <> 3 and StatusSettlement <> 3 and D.Type = 3
	                    and FundPK = @fundpk and TrxType  = 2
	                    group By A.InstrumentPK,A.BankBranchPK
                    )F on A.InstrumentPK = F.InstrumentPK and A.BankBranchPK = F.BankBranchPK 
                    where A.maturityDate > @Date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and E.Type in (3) and A.status = 2                                
                    and (A.Balance + isnull(F.Balance,0)) > 0 and A.InstrumentPK = @InstrumentPK

                    if @Amount > @CurrBalance
	                    Begin
		                    Select 1 Result
		                    return
	                    end
	                    else
	                    begin
		                    select 0 Result
		                    return
	                    end
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

                    select @CurrBalance = A.Balance + isnull(F.Balance,0)
                    from FundPosition A    
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                    left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                    left join
                    (

	                    Select A.InstrumentPK,A.BankBranchPK,sum(DoneVolume) * -1 Balance
	                    from Investment A
	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                        Left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	                    where ValueDate = @date and StatusInvestment = 1 and StatusDealing <> 3 and StatusSettlement <> 3 and D.Type = 3
	                    and FundPK = @fundpk and TrxType  = 2
	                    group By A.InstrumentPK,A.BankBranchPK
                    )F on A.InstrumentPK = F.InstrumentPK and A.BankBranchPK = F.BankBranchPK 
                    where A.maturityDate > @Date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and E.Type in (3) and A.status = 2                                
                    and (A.Balance + isnull(F.Balance,0)) > 0 and A.InstrumentPK = @InstrumentPK

                    if @Amount > @CurrBalance
	                    Begin
		                    Select 1 Result
		                    return
	                    end
	                    else
	                    begin
		                    select 0 Result
		                    return
	                    end
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

        public void Update_RealizedAmountOMSTimeDeposit(OMSTimeDepositByInstrument _oMSTimeDepositByInstrument)
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
                        if (_oMSTimeDepositByInstrument.FundPK != 0)
                        {
                            _paramFund = " And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        update RealizedOMSTimeDeposit set RealizedAmount = @RealizedAmount where InstrumentPK = @InstrumentPK and Balance = @Balance and InterestPercent = @InterestPercent 
                        and AcqDate =@AcqDate and MaturityDate = @MaturityDate and ValueDate = @ValueDate " + _paramFund  +

                        @" 
                        declare @RealInterestPercent numeric (18,4)
                        select @RealInterestPercent = sum(100 * (@RealizedAmount * @InterestDays)/(@Balance * @Tenor * (1-(@TaxPercent/100))))
                        update RealizedOMSTimeDeposit set RealInterestPercent = @RealInterestPercent where InstrumentPK = @InstrumentPK and Balance = @Balance and InterestPercent = @InterestPercent 
                        and AcqDate =@AcqDate and MaturityDate = @MaturityDate and ValueDate = @ValueDate " + _paramFund;

                        cmd.Parameters.AddWithValue("@RealizedAmount", _oMSTimeDepositByInstrument.RealizedAmount);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _oMSTimeDepositByInstrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Balance", _oMSTimeDepositByInstrument.Volume);
                        cmd.Parameters.AddWithValue("@InterestPercent", _oMSTimeDepositByInstrument.InterestPercent);
                        cmd.Parameters.AddWithValue("@TaxPercent", _oMSTimeDepositByInstrument.TaxPercent);
                        cmd.Parameters.AddWithValue("@AcqDate", _oMSTimeDepositByInstrument.AcqDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _oMSTimeDepositByInstrument.MaturityDate);
                        cmd.Parameters.AddWithValue("@Tenor", _oMSTimeDepositByInstrument.Tenor);
                        cmd.Parameters.AddWithValue("@InterestDays", _oMSTimeDepositByInstrument.InterestDays);
                        if (_oMSTimeDepositByInstrument.FundPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _oMSTimeDepositByInstrument.FundPK);
                        }
                        cmd.Parameters.AddWithValue("@ValueDate", _oMSTimeDepositByInstrument.ValueDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public NetOMSTimeDeposit NetOMSTimeDeposit(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    NetOMSTimeDeposit M_NetOMSTimeDeposit = new NetOMSTimeDeposit();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @FeePercent numeric (18,4)
                        set @FeePercent  = 0.437
                        select isnull(A.TotalInterestBilyet,0) GrossBilyet, isnull((A.TotalBalance * (@FeePercent/100) * 31/365),0) FeeBilyet, sum(isnull(A.TotalInterestBilyet - (A.TotalBalance * (@FeePercent/100) *31/365),0)) NetBilyet , isnull(sum(A.TotalInterestBilyet - (A.TotalBalance * (@FeePercent/100) *31/365))/A.TotalBalance * 12 ,0) NetPercentBilyet,
                        isnull(A.TotalInterestReal,0) GrossReal, isnull((A.TotalBalance * (@FeePercent/100) * 31/365),0) FeeReal, sum(isnull(A.TotalInterestReal - (A.TotalBalance * (@FeePercent/100) *31/365),0)) NetReal , isnull(sum(A.TotalInterestReal - (A.TotalBalance * (@FeePercent/100) *31/365))/A.TotalBalance * 12 ,0) NetPercentReal
                        from 
                        (select sum(isnull(dbo.FGetDepositoInterestAccrued_EMCO(A.InstrumentPK,A.Balance,DATEDIFF(day, case when A.AcqDate >= case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end
                        then A.AcqDate else
                        case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) - 1,A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) end end, case when day(A.MaturityDate) >= day(@date) then  DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date),A.MaturityDate) 
                        else DATEADD(month,DATEDIFF(MONTH, A.MaturityDate, @date) + 1,A.MaturityDate) end)),0)) TotalInterestBilyet ,sum(RealizedAmount)TotalInterestReal, sum(A.Balance) TotalBalance
                        from RealizedOMSTimeDeposit A where A.ValueDate = @Date and A.FundPK  = @FundPK) A
                        group by A.TotalBalance,A.TotalInterestBilyet,A.TotalInterestReal
                                                ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    M_NetOMSTimeDeposit.GrossBilyet = Convert.ToDecimal(dr["GrossBilyet"]);
                                    M_NetOMSTimeDeposit.FeeBilyet = Convert.ToDecimal(dr["FeeBilyet"]);
                                    M_NetOMSTimeDeposit.NetBilyet = Convert.ToDecimal(dr["NetBilyet"]);
                                    M_NetOMSTimeDeposit.NetPercentBilyet = Convert.ToDecimal(dr["NetPercentBilyet"]);
                                    M_NetOMSTimeDeposit.GrossReal = Convert.ToDecimal(dr["GrossReal"]);
                                    M_NetOMSTimeDeposit.FeeReal = Convert.ToDecimal(dr["FeeReal"]);
                                    M_NetOMSTimeDeposit.NetReal = Convert.ToDecimal(dr["NetReal"]);
                                    M_NetOMSTimeDeposit.NetPercentReal = Convert.ToDecimal(dr["NetPercentReal"]);
                                    return M_NetOMSTimeDeposit;
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
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"



Declare @BankPK int
Declare @BankName nvarchar(200)
Select @BankPK = A.BankPK,@BankName = B.Name from Instrument A
left join Bank B on A.BankPK = B.BankPK and B.status = 2
where A.status = 2 and InstrumentPK = @InstrumentPK


Create table #tmpExposureDeposito
    (
    FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
    ExposureType nvarchar(200) COLLATE DATABASE_DEFAULT,
    Parameter nvarchar(200) COLLATE DATABASE_DEFAULT,
    CurrentValue Decimal(22,4),
    PotentialValue Decimal(22,4),
    MaxValue Decimal(22,4),
    DifferenceValue Decimal(22,4),
    CurrentPercentage Decimal(22,4),
    PotentialPercentage Decimal(22,4),
    MaxPercentage decimal(8,4),
    DifferencePercentage Decimal(22,4),
	WarningPercentage Decimal(22,4),
	WarningValue Decimal(22,4)
    )


    Create table #tmpExposureDepositoA
    (
    Movement Decimal(22,4),
    BankPK int
    )



    Declare @EndDayTrailsFundPortfolioPK int
   
    Declare @Parameter nvarchar(100)
   
Declare @MaxDateEndDayFP datetime
	Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
	endDayTrailsFundPortfolio where 
	valuedate = 
	(
		Select max(ValueDate) from endDayTrailsFundPortfolio where
		valuedate < @Date  and status = 2 and FundPK = @FundPK
	)
	and status = 2 and FundPK = @FundPK


    Declare @TotalMarketValueAllFund numeric(26,6)

    select @TotalMarketValueAllFund = sum(aum) From closeNav
    where Date = (
		Select max(date) from CloseNAV where date < @Date and status = 2 
	)
    and status = 2

    set @TotalMarketValueAllFund = isnull(@TotalMarketValueAllFund,1)

        if @TotalMarketValueAllFund = 1
        BEGIN
            return
        END
		

	 --AMBIL POSISI AWAL DARI FUND POSITION UNTUK ALL FUND PER ALL BANK ( CEK NANTI, smntara dibikin ga boleh ALL FUND ALL BANK )
    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,isnull(B.BankPK,@BankPK)  Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,sum(isnull(B.CurrBal,0))/@TotalMarketValueAllFund *100,0,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue
    from FundExposure A LEFT JOIN  
    (  
        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and C.BankPK = @BankPK
        Group By B.BankPK,C.ID   
    )B ON A.Parameter = 0   
    where A.Type = 9 and A.Status = 2 and A.Parameter = 0
    group By B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue
	
	
    insert into #tmpExposureDepositoA
    select sum(Movement) Movement,BankPK from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date 
    and C.Type = 3 
	and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and A.MaturityDate  > @Date
    Group By B.BankPK  
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and C.type = 3 and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK  
	) A group by BankPK


	
Insert into #tmpExposureDeposito
select 'ALL FUND','ALL FUND PER BANK',A.BankPK,0,A.Movement,B.MaxValue,0,0, Movement/@TotalMarketValueAllFund * 100, 
MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue
from #tmpExposureDepositoA A
left join FundExposure B on B.Parameter = 0 and B.Type = 9 and B.status = 2
where A.BankPK not in
(
	Select Parameter from #tmpExposureDeposito
)



    update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@TotalMarketValueAllFund * 100 from #tmpExposureDepositoA
    where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK
    update A set Parameter = B.Name from #tmpExposureDeposito A
    left join Bank B on A.Parameter = B.BankPK and B.Status = 2

	
	-- DELETE UNTUK ALL FUND PER BANK CODE : 9, BANK : BUKAN ALL

    DECLARE A CURSOR FOR 
    Select B.Name from FundExposure A 
    left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
    where A.status  = 2 and Type = 9 and Parameter <> 0
	
    Open A
    Fetch Next From A
    Into @BankName
    While @@FETCH_STATUS = 0
    BEGIN
	    DECLARE B CURSOR FOR 
	    Select isnull(F.Name,'') Parameter
	    from FundExposure A LEFT JOIN  
	    (  
	    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3
	    Group By B.BankPK,C.ID   
	    )B ON A.Parameter = B.BankPK 
	    left join -- T0 from investment  
	    (  
	    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
	    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	    Group By B.BankPK  
	    )D on A.Parameter = D.BankPK  
	    left join -- T0 Matured   
	    (  
	    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK 
		and C.type = 3
	    Group By B.BankPK  
	    )E on A.Parameter = E.BankPK
	    left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	    where A.Type = 9 and A.status  = 2 and A.Parameter <> 0

	    Open B
	    Fetch Next From B
	    Into @Parameter
	    While @@FETCH_STATUS = 0
	    BEGIN
	    IF (@BankName = @Parameter)
	    BEGIN
	    delete #tmpExposureDeposito where Parameter = @BankName
	    END
	    Fetch next From B                   
	    Into @Parameter             
	    end                  
	    Close B                  
	    Deallocate B 

    Fetch next From A                   
    Into @BankName             
    end                  
    Close A                  
    Deallocate A 
	
    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@TotalMarketValueAllFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValueAllFund * 100,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and C.BankPK = @BankPK
    Group By B.BankPK,C.ID   
    )B ON A.Parameter = B.BankPK 
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and  B.BankPK = @BankPK
    Group By B.BankPK  
    )D on A.Parameter = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and  B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  
    )E on A.Parameter = E.BankPK
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    where A.Type = 9 and A.status  = 2 and A.Parameter <> 0

	
	

	-- PER FUND PER BANK CODE 10
	Declare @FundID nvarchar(200)
	select @FundID = ID from Fund where status = 2 and FundPK = @FundPK

	Declare @totalMarketValuePerFund numeric(22,4)
	 select @totalMarketValuePerFund = aum From closeNav
    where Date = 
	(
	Select max(date) from CloseNAV where date < @Date and status = 2 and FundPK = @FundPK
	)
    and status = 2 and FundPK = @FundPK

	  Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)


    Select @FundID FundID,'PER FUND PER BANK' ExposureType,isnull(B.BankPK,@BankPK) Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,sum(isnull(B.CurrBal,0))/@totalMarketValuePerFund * 100,0,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue
    from FundExposure A LEFT JOIN  
    (  
        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @FundPK and C.BankPK = @BankPK
        Group By B.BankPK,C.ID   
    )B ON A.Parameter = 0 
    Left join Fund E on A.FundPK = E.FundPK and E.status = 2   
    where A.Type = 10  and A.Status = 2 and A.Parameter = 0 and A.FundPK = @FundPK
    group By E.ID,B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue


    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    select E.ID FundID,'PER FUND PER BANK' ExposureType,BankPK Parameter,0,sum(Movement) Movement,MaxValue,0,0,0,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK,FundPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK ,FundPK 
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK,FundPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2 
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK,FundPK  ) A 
    Left join FundExposure C on C.FundPK = @FundPK and C.status = 2 
    Left join Fund E on A.FundPK = E.FundPK and E.status = 2 
    where C.Type = 10   and C.Parameter = 0 and A.FundPK = @FundPK
	and A.BankPK not in
	(
		Select parameter from #tmpExposureDeposito where ExposureType = 'PER FUND PER BANK' 
	)
    group by E.ID,BankPK,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue


	

    insert into #tmpExposureDepositoA
    select sum(Movement) Movement,BankPK from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK  
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  ) A group by BankPK


    update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@totalMarketValuePerFund * 100 from #tmpExposureDepositoA
    where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK and ExposureType = 'PER FUND PER BANK' 
    update A set Parameter = B.Name from #tmpExposureDeposito A
    left join Bank B on A.Parameter = B.BankPK and B.Status = 2 
	where ExposureType = 'PER FUND PER BANK'

    DECLARE A CURSOR FOR 
    Select B.Name from FundExposure A 
    left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
    where A.status  = 2 and Type = 10 and A.FundPK = @FundPK and Parameter <> 0

    Open A
    Fetch Next From A
    Into @BankName
    While @@FETCH_STATUS = 0
    BEGIN
	    DECLARE B CURSOR FOR 
	    Select isnull(F.Name,'') Parameter
	    from FundExposure A LEFT JOIN  
	    (  
	    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk and B.BankPK = @BankPK
	    Group By B.BankPK,C.ID   
	    )B ON A.Parameter = B.BankPK 
	    left join -- T0 from investment  
	    (  
	    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
	    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @fundpk and B.BankPK = @BankPK
	    Group By B.BankPK  
	    )D on A.Parameter = D.BankPK  
	    left join -- T0 Matured   
	    (  
	    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @fundpk and B.BankPK = @BankPK and C.type = 3
	    Group By B.BankPK  
	    )E on A.Parameter = E.BankPK
	    left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	    where A.Type = 10 and A.FundPK = @FundPK and A.status  = 2 and A.Parameter <> 0

	    Open B
	    Fetch Next From B
	    Into @Parameter
	    While @@FETCH_STATUS = 0
	    BEGIN
	    IF (@BankName = @Parameter)
	    BEGIN
	    delete #tmpExposureDeposito where Parameter = @BankName
		and ExposureType = 'PER FUND PER BANK'
	    END
	    Fetch next From B                   
	    Into @Parameter             
	    end                  
	    Close B                  
	    Deallocate B 

    Fetch next From A                   
    Into @BankName             
    end                  
    Close A                  
    Deallocate A 
	

    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select @FundID FundID,'PER FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@totalMarketValuePerFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@totalMarketValuePerFund * 100,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk and C.BankPK = @BankPK
    Group By B.BankPK,C.ID   
    )B ON A.Parameter = B.BankPK 
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK  
    )D on A.Parameter = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  
    )E on A.Parameter = E.BankPK
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
    where A.Type = 10 and A.FundPK = @FundPK and A.status  = 2 and A.Parameter <> 0

	  Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select G.ID FundID,'INSTRUMENT TYPE' ExposureType,'DEPOSITO' Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@totalMarketValuePerFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@totalMarketValuePerFund * 100,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 3 and A.FundPK = @fundpk
    Group By B.InstrumentTypePK   
    )B ON A.Parameter = 5
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement
	From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.Type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK
    )D on A.Parameter = 5
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2

    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
    
    )E on A.Parameter = 5
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
    where A.Type = 4 and A.status  = 2 and A.Parameter = 5

	
	

	if @totalMarketValuePerFund > 1
	BEGIN
	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValuePerFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount, C.Parameter = b.Name
	From instrument A
	left join Bank B on A.BankPK = B.BankPK and B.status = 2
	left join #tmpExposureDeposito  C on  C.Parameter  = B.name
	where B.BankPK = @BankPK and C.ExposureType = 'PER FUND PER BANK'
	and A.InstrumentPK = @InstrumentPK

	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValuePerFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount
	from #tmpExposureDeposito  C 
	where  C.ExposureType = 'INSTRUMENT TYPE'
	END

	if @totalMarketValueALLFund > 1
	BEGIN
	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValueALLFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount, C.Parameter = b.Name
	From instrument A
	left join Bank B on A.BankPK = B.BankPK and B.status = 2
	left join #tmpExposureDeposito  C on  C.Parameter  = B.name
	where B.BankPK = @BankPK and C.ExposureType = 'ALL FUND PER BANK'
	and A.InstrumentPK = @InstrumentPK
	END


Select ExposureType + ' | ' + Parameter ExposureID,case when maxValue = 0 then MaxPercentage else maxValue end MaxExposurePercent
,sum(case when maxvalue = 0 then (CurrentPercentage+PotentialPercentage) else (CurrentValue+PotentialValue) end) ExposurePercent
,Case when sum(CurrentValue+PotentialValue) >= maxValue and MaxValue > 0 then 4
 when sum(CurrentPercentage+PotentialPercentage) >= MaxPercentage and MaxPercentage > 0   then 3  
 when sum(CurrentValue+PotentialValue) >= WarningValue and WarningValue > 0 then 2 
 when sum(CurrentPercentage+PotentialPercentage) > WarningPercentage  and WarningPercentage > 0   then 1 
else 0 end  AlertExposure,warningValue,warningpercentage
from #tmpExposureDeposito where Parameter = @BankName and ExposureType in ('ALL FUND PER BANK','PER FUND PER BANK')
group by ExposureType,Parameter,maxValue,MaxPercentage,WarningPercentage,WarningValue

UNION ALL

Select ExposureType + ' | ' + Parameter ExposureID,case when maxValue = 0 then MaxPercentage else maxValue end MaxExposurePercent
,sum(case when maxvalue = 0 then (CurrentPercentage+PotentialPercentage) else (CurrentValue+PotentialValue) end) ExposurePercent
,Case when sum(CurrentValue+PotentialValue) >= maxValue and MaxValue > 0 then 4
 when sum(CurrentPercentage+PotentialPercentage) >= MaxPercentage and MaxPercentage > 0   then 3  
 when sum(CurrentValue+PotentialValue) >= WarningValue and WarningValue > 0 then 2 
 when sum(CurrentPercentage+PotentialPercentage) > WarningPercentage  and WarningPercentage > 0   then 1 
else 0 end  AlertExposure,warningValue,warningpercentage
from #tmpExposureDeposito where Parameter = 'DEPOSITO' and FundID = @FundID
group by ExposureType,Parameter,maxValue,MaxPercentage,WarningPercentage,WarningValue


order by alertExposure Desc";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"



Declare @BankPK int
Declare @BankName nvarchar(200)
Select @BankPK = A.BankPK,@BankName = B.Name from Instrument A
left join Bank B on A.BankPK = B.BankPK and B.status = 2
where A.status = 2 and InstrumentPK = @InstrumentPK


Create table #tmpExposureDeposito
    (
    FundID nvarchar(100) COLLATE DATABASE_DEFAULT,
    ExposureType nvarchar(200) COLLATE DATABASE_DEFAULT,
    Parameter nvarchar(200) COLLATE DATABASE_DEFAULT,
    CurrentValue Decimal(22,4),
    PotentialValue Decimal(22,4),
    MaxValue Decimal(22,4),
    DifferenceValue Decimal(22,4),
    CurrentPercentage Decimal(22,4),
    PotentialPercentage Decimal(22,4),
    MaxPercentage decimal(8,4),
    DifferencePercentage Decimal(22,4),
	WarningPercentage Decimal(22,4),
	WarningValue Decimal(22,4)
    )


    Create table #tmpExposureDepositoA
    (
    Movement Decimal(22,4),
    BankPK int
    )



    Declare @EndDayTrailsFundPortfolioPK int
   
    Declare @Parameter nvarchar(100)
   
Declare @MaxDateEndDayFP datetime
	Select @EndDayTrailsFundPortfolioPK =  EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
	endDayTrailsFundPortfolio where 
	valuedate = 
	(
		Select max(ValueDate) from endDayTrailsFundPortfolio where
		valuedate < @Date  and status = 2
	)
	and status = 2


    Declare @TotalMarketValueAllFund numeric(26,6)

    select @TotalMarketValueAllFund = sum(aum) From closeNav
    where Date = (
		Select max(date) from CloseNAV where date < @Date and status = 2 
	)
    and status = 2

    set @TotalMarketValueAllFund = isnull(@TotalMarketValueAllFund,1)

        if @TotalMarketValueAllFund = 1
        BEGIN
            return
        END
		

	 --AMBIL POSISI AWAL DARI FUND POSITION UNTUK ALL FUND PER ALL BANK ( CEK NANTI, smntara dibikin ga boleh ALL FUND ALL BANK )
    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,isnull(B.BankPK,@BankPK)  Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,sum(isnull(B.CurrBal,0))/@TotalMarketValueAllFund *100,0,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue
    from FundExposure A LEFT JOIN  
    (  
        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and C.BankPK = @BankPK
        Group By B.BankPK,C.ID   
    )B ON A.Parameter = 0   
    where A.Type = 9 and A.Status = 2 and A.Parameter = 0
    group By B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue
	
	
    insert into #tmpExposureDepositoA
    select sum(Movement) Movement,BankPK from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date 
    and C.Type = 3 
	and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and A.MaturityDate  > @Date
    Group By B.BankPK  
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and C.type = 3 and A.FundPositionPK = @EndDayTrailsFundPortfolioPK
    Group By B.BankPK  
	) A group by BankPK


	
Insert into #tmpExposureDeposito
select 'ALL FUND','ALL FUND PER BANK',A.BankPK,0,A.Movement,B.MaxValue,0,0, Movement/@TotalMarketValueAllFund * 100, 
MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue
from #tmpExposureDepositoA A
left join FundExposure B on B.Parameter = 0 and B.Type = 9 and B.status = 2
where A.BankPK not in
(
	Select Parameter from #tmpExposureDeposito
)



    update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@TotalMarketValueAllFund * 100 from #tmpExposureDepositoA
    where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK
    update A set Parameter = B.Name from #tmpExposureDeposito A
    left join Bank B on A.Parameter = B.BankPK and B.Status = 2

	
	-- DELETE UNTUK ALL FUND PER BANK CODE : 9, BANK : BUKAN ALL

    DECLARE A CURSOR FOR 
    Select B.Name from FundExposure A 
    left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
    where A.status  = 2 and Type = 9 and Parameter <> 0
	
    Open A
    Fetch Next From A
    Into @BankName
    While @@FETCH_STATUS = 0
    BEGIN
	    DECLARE B CURSOR FOR 
	    Select isnull(F.Name,'') Parameter
	    from FundExposure A LEFT JOIN  
	    (  
	    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3
	    Group By B.BankPK,C.ID   
	    )B ON A.Parameter = B.BankPK 
	    left join -- T0 from investment  
	    (  
	    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
	    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	    Group By B.BankPK  
	    )D on A.Parameter = D.BankPK  
	    left join -- T0 Matured   
	    (  
	    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK 
		and C.type = 3
	    Group By B.BankPK  
	    )E on A.Parameter = E.BankPK
	    left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	    where A.Type = 9 and A.status  = 2 and A.Parameter <> 0

	    Open B
	    Fetch Next From B
	    Into @Parameter
	    While @@FETCH_STATUS = 0
	    BEGIN
	    IF (@BankName = @Parameter)
	    BEGIN
	    delete #tmpExposureDeposito where Parameter = @BankName
	    END
	    Fetch next From B                   
	    Into @Parameter             
	    end                  
	    Close B                  
	    Deallocate B 

    Fetch next From A                   
    Into @BankName             
    end                  
    Close A                  
    Deallocate A 
	
    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select 'ALL FUND' FundID,'ALL FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@TotalMarketValueAllFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@TotalMarketValueAllFund * 100,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and C.BankPK = @BankPK
    Group By B.BankPK,C.ID   
    )B ON A.Parameter = B.BankPK 
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3 and  B.BankPK = @BankPK
    Group By B.BankPK  
    )D on A.Parameter = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and  B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  
    )E on A.Parameter = E.BankPK
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    where A.Type = 9 and A.status  = 2 and A.Parameter <> 0

	
	

	-- PER FUND PER BANK CODE 10
	Declare @FundID nvarchar(200)
	select @FundID = ID from Fund where status = 2 and FundPK = @FundPK

	Declare @totalMarketValuePerFund numeric(22,4)
	 select @totalMarketValuePerFund = aum From closeNav
    where Date = 
	(
	Select max(date) from CloseNAV where date < @Date and status = 2 and FundPK = @FundPK
	)
    and status = 2 and FundPK = @FundPK

	  Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)


    Select @FundID FundID,'PER FUND PER BANK' ExposureType,isnull(B.BankPK,@BankPK) Parameter,sum(isnull(B.CurrBal,0)) CurrentValue,0,MaxValue,0,sum(isnull(B.CurrBal,0))/@totalMarketValuePerFund * 100,0,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue
    from FundExposure A LEFT JOIN  
    (  
        Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
        Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
        Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @FundPK and C.BankPK = @BankPK
        Group By B.BankPK,C.ID   
    )B ON A.Parameter = 0 
    Left join Fund E on A.FundPK = E.FundPK and E.status = 2   
    where A.Type = 10  and A.Status = 2 and A.Parameter = 0 and A.FundPK = @FundPK
    group By E.ID,B.BankPK,B.CurrBal,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue


    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    select E.ID FundID,'PER FUND PER BANK' ExposureType,BankPK Parameter,0,sum(Movement) Movement,MaxValue,0,0,0,MaxExposurePercent,0,WarningMaxExposurePercent,WarningMaxValue from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK,FundPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK ,FundPK 
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK,FundPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2 
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK,FundPK  ) A 
    Left join FundExposure C on C.FundPK = @FundPK and C.status = 2 
    Left join Fund E on A.FundPK = E.FundPK and E.status = 2 
    where C.Type = 10   and C.Parameter = 0 and A.FundPK = @FundPK
	and A.BankPK not in
	(
		Select parameter from #tmpExposureDeposito where ExposureType = 'PER FUND PER BANK' 
	)
    group by E.ID,BankPK,MaxValue,MaxExposurePercent,WarningMaxExposurePercent,WarningMaxValue


	

    insert into #tmpExposureDepositoA
    select sum(Movement) Movement,BankPK from (
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK  
    union all
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  ) A group by BankPK


    update #tmpExposureDeposito set PotentialValue =  Movement, PotentialPercentage = Movement/@totalMarketValuePerFund * 100 from #tmpExposureDepositoA
    where #tmpExposureDeposito.Parameter = #tmpExposureDepositoA.BankPK and ExposureType = 'PER FUND PER BANK' 
    update A set Parameter = B.Name from #tmpExposureDeposito A
    left join Bank B on A.Parameter = B.BankPK and B.Status = 2 
	where ExposureType = 'PER FUND PER BANK'

    DECLARE A CURSOR FOR 
    Select B.Name from FundExposure A 
    left join Bank B on A.Parameter  = B.BankPK and B.status  = 2
    where A.status  = 2 and Type = 10 and A.FundPK = @FundPK and Parameter <> 0

    Open A
    Fetch Next From A
    Into @BankName
    While @@FETCH_STATUS = 0
    BEGIN
	    DECLARE B CURSOR FOR 
	    Select isnull(F.Name,'') Parameter
	    from FundExposure A LEFT JOIN  
	    (  
	    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
	    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
	    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
	    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk and B.BankPK = @BankPK
	    Group By B.BankPK,C.ID   
	    )B ON A.Parameter = B.BankPK 
	    left join -- T0 from investment  
	    (  
	    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
	    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
	    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @fundpk and B.BankPK = @BankPK
	    Group By B.BankPK  
	    )D on A.Parameter = D.BankPK  
	    left join -- T0 Matured   
	    (  
	    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
	    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @fundpk and B.BankPK = @BankPK and C.type = 3
	    Group By B.BankPK  
	    )E on A.Parameter = E.BankPK
	    left join Bank F on A.Parameter = F.BankPK and F.Status = 2
	    where A.Type = 10 and A.FundPK = @FundPK and A.status  = 2 and A.Parameter <> 0

	    Open B
	    Fetch Next From B
	    Into @Parameter
	    While @@FETCH_STATUS = 0
	    BEGIN
	    IF (@BankName = @Parameter)
	    BEGIN
	    delete #tmpExposureDeposito where Parameter = @BankName
		and ExposureType = 'PER FUND PER BANK'
	    END
	    Fetch next From B                   
	    Into @Parameter             
	    end                  
	    Close B                  
	    Deallocate B 

    Fetch next From A                   
    Into @BankName             
    end                  
    Close A                  
    Deallocate A 
	

    Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select @FundID FundID,'PER FUND PER BANK' ExposureType,isnull(F.Name,'') Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@totalMarketValuePerFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@totalMarketValuePerFund * 100,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal, B.BankPK,C.ID from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
    Left join Bank C on B.BankPK = C.BankPK and C.status = 2  
    left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and D.Type = 3 and A.FundPK = @fundpk and C.BankPK = @BankPK
    Group By B.BankPK,C.ID   
    )B ON A.Parameter = B.BankPK 
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement,B.BankPK From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK and B.BankPK = @BankPK
    Group By B.BankPK  
    )D on A.Parameter = D.BankPK  
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance,B.BankPK From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and B.BankPK = @BankPK and C.type = 3
    Group By B.BankPK  
    )E on A.Parameter = E.BankPK
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
    where A.Type = 10 and A.FundPK = @FundPK and A.status  = 2 and A.Parameter <> 0

	  Insert into #tmpExposureDeposito(FundID,ExposureType,Parameter,CurrentValue,PotentialValue,MaxValue,DifferenceValue,CurrentPercentage,PotentialPercentage
    ,MaxPercentage,DifferencePercentage,WarningPercentage,WarningValue)
    Select G.ID FundID,'INSTRUMENT TYPE' ExposureType,'DEPOSITO' Parameter,isnull(B.CurrBal,0) CurrentValue,isnull(D.Movement,0) + isnull(E.MaturedBalance,0) PotentialValue,MaxValue,  
    isnull(MaxValue - (B.CurrBal + isnull(D.Movement,0) + isnull(E.MaturedBalance,0) ),0) Difference,
    isnull(B.CurrBal,0)/@totalMarketValuePerFund * 100,(isnull(D.Movement,0) + isnull(E.MaturedBalance,0))/@totalMarketValuePerFund * 100,MaxExposurePercent,0   ,WarningMaxExposurePercent,WarningMaxValue

    from FundExposure A LEFT JOIN  
    (  
    Select Sum(Balance) CurrBal from [FundPosition] A  
    Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    Where A.FundPositionPK = @EndDayTrailsFundPortfolioPK and C.Type = 3 and A.FundPK = @fundpk
    Group By B.InstrumentTypePK   
    )B ON A.Parameter = 5
    left join -- T0 from investment  
    (  
    Select isnull(sum(case when TrxType in(1,3) then Amount else amount * -1 end),0) Movement
	From Investment A  
    left join Instrument B on A.InstrumentPK = B.InstrumentPk and B.Status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
    where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and A.MaturityDate > @Date
    and C.Type = 3 and StatusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3   and A.FundPK = @FundPK
    )D on A.Parameter = 5
    left join -- T0 Matured   
    (  
    Select sum(Balance) * -1 MaturedBalance From [FundPosition] A  
    Left join instrument B on A.instrumentPK = B.InstrumentPK and B.status = 2  
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2

    where A.MaturityDate >  @MaxDateEndDayFP and  A.MaturityDate <=  @Date  and A.FundPositionPK = @EndDayTrailsFundPortfolioPK and A.FundPK = @FundPK and C.type = 3
    
    )E on A.Parameter = 5
    left join Bank F on A.Parameter = F.BankPK and F.Status = 2  
    left join Fund G on A.FundPK = G.FundPK and G.Status = 2 
    where A.Type = 4 and A.status  = 2 and A.Parameter = 5

	
	

	if @totalMarketValuePerFund > 1
	BEGIN
	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValuePerFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount, C.Parameter = b.Name
	From instrument A
	left join Bank B on A.BankPK = B.BankPK and B.status = 2
	left join #tmpExposureDeposito  C on  C.Parameter  = B.name
	where B.BankPK = @BankPK and C.ExposureType = 'PER FUND PER BANK'
	and A.InstrumentPK = @InstrumentPK

	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValuePerFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount
	from #tmpExposureDeposito  C 
	where  C.ExposureType = 'INSTRUMENT TYPE'
	END

	if @totalMarketValueALLFund > 1
	BEGIN
	update c set C.PotentialPercentage = c.PotentialPercentage + (@amount/@totalMarketValueALLFund * 100), 
	C.Potentialvalue = c.PotentialValue + @amount, C.Parameter = b.Name
	From instrument A
	left join Bank B on A.BankPK = B.BankPK and B.status = 2
	left join #tmpExposureDeposito  C on  C.Parameter  = B.name
	where B.BankPK = @BankPK and C.ExposureType = 'ALL FUND PER BANK'
	and A.InstrumentPK = @InstrumentPK
	END


Select ExposureType + ' | ' + Parameter ExposureID,case when maxValue = 0 then MaxPercentage else maxValue end MaxExposurePercent
,sum(case when maxvalue = 0 then (CurrentPercentage+PotentialPercentage) else (CurrentValue+PotentialValue) end) ExposurePercent
,Case when sum(CurrentValue+PotentialValue) >= maxValue and MaxValue > 0 then 4
 when sum(CurrentPercentage+PotentialPercentage) >= MaxPercentage and MaxPercentage > 0   then 3  
 when sum(CurrentValue+PotentialValue) >= WarningValue and WarningValue > 0 then 2 
 when sum(CurrentPercentage+PotentialPercentage) > WarningPercentage  and WarningPercentage > 0   then 1 
else 0 end  AlertExposure,warningValue,warningpercentage
from #tmpExposureDeposito where Parameter = @BankName and ExposureType in ('ALL FUND PER BANK','PER FUND PER BANK')
group by ExposureType,Parameter,maxValue,MaxPercentage,WarningPercentage,WarningValue

UNION ALL

Select ExposureType + ' | ' + Parameter ExposureID,case when maxValue = 0 then MaxPercentage else maxValue end MaxExposurePercent
,sum(case when maxvalue = 0 then (CurrentPercentage+PotentialPercentage) else (CurrentValue+PotentialValue) end) ExposurePercent
,Case when sum(CurrentValue+PotentialValue) >= maxValue and MaxValue > 0 then 4
 when sum(CurrentPercentage+PotentialPercentage) >= MaxPercentage and MaxPercentage > 0   then 3  
 when sum(CurrentValue+PotentialValue) >= WarningValue and WarningValue > 0 then 2 
 when sum(CurrentPercentage+PotentialPercentage) > WarningPercentage  and WarningPercentage > 0   then 1 
else 0 end  AlertExposure,warningValue,warningpercentage
from #tmpExposureDeposito where Parameter = 'DEPOSITO' and FundID = @FundID
group by ExposureType,Parameter,maxValue,MaxPercentage,WarningPercentage,WarningValue


order by alertExposure Desc";
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

        public Boolean OMSTimeDeposit_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _bitIsMature = "";
                        string _paramInvestmentPK = "";
                        string _paramMatureInvestmentPk = "";
                        string _paramFundPK = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                            _paramMatureInvestmentPk = " InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = "";
                            _paramMatureInvestmentPk = "";
                        }


                        if (!_host.findString(_listing.ParamFundID.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.ParamFundID))
                        {
                            _paramFund = " And IV.FundPK in (" + _listing.ParamFundID + ") ";
                            _paramFundPK = "And FundPK in (" + _listing.ParamFundID + ") ";
                        }
                        else
                        {
                            _paramFund = "";
                            _paramFundPK = "";
                        }

                        if (_listing.BitIsMature == true)
                        {
                            _bitIsMature = @"union all
                            select Reference, RefNo,ValueDate,InstrumentID,InstrumentName,    
                            FundID,InstrumentType,InvestmentPK,Volume,OrderPrice,InterestPercent,TrxTypeID,   
                            Amount,Notes,RangePrice ,MaturityDate ,DoneVolume,DoneAmount,'',AcqDate  ,'' DealingID,'' PTPCode
                            from InvestmentMature where " + _paramMatureInvestmentPk;
                        }
                        else
                        {
                            _bitIsMature = "";
                        }
                        if (Tools.ClientCode == "02" || Tools.ClientCode == "03" || Tools.ClientCode == "21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate " + _paramFundPK + @"
                        )
                        and status = 2 " + _paramFundPK + @"

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                        IV.Amount,IV.Notes, IV.RangePrice,IV.MaturityDate,IV.DoneVolume,IV.DoneAmount,IV.Notes,IV.AcqDate
                        ,isnull(IV.EntryDealingID,'') DealingID,isnull(J.PTPCode,'') PTPCode
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2      
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2    
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3  and IT.Type = 3 " + _paramInvestmentPK + _paramFund + _bitIsMature +
                            @"
                        order by FundID
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                        )
                        and status = 2

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                        IV.Amount,IV.Notes, IV.RangePrice,IV.MaturityDate,IV.DoneVolume,IV.DoneAmount,IV.Notes,IV.AcqDate
                        ,isnull(IV.EntryDealingID,'') DealingID,isnull(J.PTPCode,'') PTPCode
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2      
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2    
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3  and IT.Type = 3 " + _paramInvestmentPK + _paramFund + _bitIsMature +
                            @"
                        order by FundID
                        ";
                        }



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
                                string filePath = Tools.ReportsPath + "OMSTimeDepositListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSTimeDepositListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                        if (rSingle.InstrumentType == "EQUITY")
                                        {
                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);

                                        }
                                        else if (rSingle.InstrumentType == "BOND")
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
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

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
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",  dd-MMM-yyyy";
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
                                        if (rsHeader.Key.InstrumentType == "EQUITY")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "BOND")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                                worksheet.Cells[incRowExcel, 9].Value = "DealerID";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTPCode";

                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                                worksheet.Cells[incRowExcel, 9].Value = "DealerID";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTPCode";
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
                                            if (rsHeader.Key.InstrumentType == "EQUITY")
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
                                            }
                                            else if (rsHeader.Key.InstrumentType == "BOND")
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
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EntryDealingID;
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
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EntryDealingID;
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
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        if (rsHeader.Key.InstrumentType != "BOND")
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
                                        worksheet.Cells[incRowExcel, 1].Value = "(            ";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_PrepareByInvestment(Convert.ToDateTime(_listing.ParamListDate));
                                        worksheet.Cells[incRowExcel, 3].Value = "          )";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
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
        
        public List<OMSTimeDepositByMaturity> Get_DataInvestmentTimeDepositByMaturity(int _fundPK, DateTime _date, int _paramPeriod, int _paramValue)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSTimeDepositByMaturity> L_model = new List<OMSTimeDepositByMaturity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode =="02" || Tools.ClientCode =="03" || Tools.ClientCode =="21")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @" 
                    
declare @DateTo datetime


Declare @TrailsPK int 
Declare @MaxEndDayTrailsDate datetime
Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxEndDayTrailsDate =  ValueDate from EndDayTrailsFundPortfolio
where status = 2 and ValueDate =(
	Select max(ValueDate) from EndDayTrailsFundPortfolio where ValueDate < @Date and status = 2 and FundPK = @FundPK
) and FundPK = @FundPK



select @DateTo = case when @FilterPeriod = 1 then DATEADD(DAY,@FilterValue,@Date) else case when @FilterPeriod = 2 then DATEADD(WEEK,@FilterValue,@Date)
else case when @FilterPeriod = 3 then DATEADD(MONTH,@FilterValue,@Date) else case when @FilterPeriod = 4 then DATEADD(YEAR,@FilterValue,@Date) else '12/31/2099' End End End End

Select B.ID InstrumentID,B.Name InstrumentName,Balance,AcqDate,A.MaturityDate,A.InterestPercent,B.Category
,C.id BankBranchID
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join BankBranch C on A.BankBranchPK = C.BankBranchPK and c.status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.MaturityDate > @date and A.MaturityDate <= @DateTo and D.Type = 3 and A.TrailsPK = @TrailsPK and A.FundPK = @FundPK and A.status = 2

UNION ALL 

Select  B.ID InstrumentID,B.Name InstrumentName,A.Volume Balance,AcqDate,A.MaturityDate,A.InterestPercent,B.Category
,C.id BankBranchID from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join BankBranch C on A.BankBranchPK = C.BankBranchPK and c.status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.MaturityDate > @date and A.MaturityDate <= @DateTo and D.Type = 3 and ValueDate > @MaxEndDayTrailsDate and ValueDate <= @Date  and A.FundPK = @FundPK and A.StatusInvestment <> 3
and A.StatusDealing <> 3 and A.StatusSettlement <> 3
                        ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @" 
                   

                  
declare @DateTo datetime


Declare @TrailsPK int 
Declare @MaxEndDayTrailsDate datetime
Select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxEndDayTrailsDate =  ValueDate from EndDayTrailsFundPortfolio
where status = 2 and ValueDate =(
	Select max(ValueDate) from EndDayTrailsFundPortfolio where ValueDate < @Date and status = 2
)



select @DateTo = case when @FilterPeriod = 1 then DATEADD(DAY,@FilterValue,@Date) else case when @FilterPeriod = 2 then DATEADD(WEEK,@FilterValue,@Date)
else case when @FilterPeriod = 3 then DATEADD(MONTH,@FilterValue,@Date) else case when @FilterPeriod = 4 then DATEADD(YEAR,@FilterValue,@Date) else '12/31/2099' End End End End

Select B.ID InstrumentID,B.Name InstrumentName,Balance,AcqDate,A.MaturityDate,A.InterestPercent,B.Category
,C.id BankBranchID
From FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join BankBranch C on A.BankBranchPK = C.BankBranchPK and c.status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.MaturityDate > @date and A.MaturityDate <= @DateTo and D.Type = 3 and A.TrailsPK = @TrailsPK and A.FundPK = @FundPK and A.status = 2

UNION ALL 

Select  B.ID InstrumentID,B.Name InstrumentName,A.Volume Balance,AcqDate,A.MaturityDate,A.InterestPercent,B.Category
,C.id BankBranchID from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
left join BankBranch C on A.BankBranchPK = C.BankBranchPK and c.status = 2
left join InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.MaturityDate > @date and A.MaturityDate <= @DateTo and D.Type = 3 and ValueDate > @MaxEndDayTrailsDate and ValueDate <= @Date  and A.FundPK = @FundPK and A.StatusInvestment <> 3
and A.StatusDealing <> 3 and A.StatusSettlement <> 3
                        ";
                        }
                        

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FilterPeriod", _paramPeriod);
                        cmd.Parameters.AddWithValue("@FilterValue", _paramValue);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OMSTimeDepositByMaturity M_model = new OMSTimeDepositByMaturity();
                                    M_model.InstrumentID = Convert.ToString(dr["InstrumentID"]);
                                    M_model.InstrumentName = Convert.ToString(dr["InstrumentName"]);
                                    M_model.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_model.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_model.AcqDate = Convert.ToDateTime(dr["AcqDate"]);
                                    M_model.MaturityDate = Convert.ToDateTime(dr["MaturityDate"]);
                                    M_model.Category = Convert.ToString(dr["Category"]);
                                    M_model.BankBranchID = Convert.ToString(dr["BankBranchID"]);
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

        public string ImportOmsDepositoTemp(string _fileSource, string _userID, string _valueDate)
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
                        cmd2.CommandText = "truncate table OMSTimeDepositImportTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "OMSTimeDepositImportTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromOMSDepositoTempExcelFile(_fileSource));
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        // logic Checks
                        // 1. check available cash
                        // 2. check price exposure

                        cmd2.CommandTimeout = 0;
                        cmd2.CommandText =
                            @"



--declare @UsersID nvarchar(20)
--declare @LastUpdate datetime
--declare @ClientCode nvarchar(20)
--declare @ValueDate date

--set @UsersID = 'admin'
--set @LastUpdate = getdate()
--set @ClientCode = '03'
--set @ValueDate = '2020-12-10'

declare @FundPK int
declare @BankPK int
declare @BankBranchPK int
declare @InterestDaysType int
declare @InterestPaymentType int
declare @PaymentModeOnMaturity int
declare @TrxType int
declare @TryTypeID nvarchar(20)
declare @investmentPK int
declare @instrumentPK int
declare @NewinstrumentPK int
declare @PlacementDate date
declare @AcqDate date
declare @MaturityDate date
declare @InterestRate numeric(18,2)
declare @BreakInterestRate numeric(18,2)
declare @BitRollOverInterest bit
declare @StatutoryType int
declare @InvestmentNotes nvarchar(200)
declare @Amount numeric(32,8)
declare @BitAvailableCash bit
declare @BitBreakable bit
declare	@ExposureType nvarchar(100)
declare	@Category nvarchar(100)
declare	@ExposureID nvarchar(100)
declare	@AlertExposure int
declare	@ExposurePercent numeric(18,4)
declare @MaxExposurePercent numeric(18,4)
declare @MinExposurePercent numeric(18,4)
declare @msg nvarchar(1000)
declare @InstrumentID nvarchar(100)
declare @MaxPK int
declare @No int
declare @CurReference nvarchar(100)
declare @LastNo int
declare @periodPK int
declare @Type nvarchar(20)
Declare @StatusSuccess int
Declare @CurrencyPK int
Declare @BankID nvarchar(200)
Declare @MaxInstrumentPK int
declare @Query nvarchar(max)
declare @counter int
declare @LastRow int
declare @MaxInvestmentPK bigint
declare @TrxBuy int
declare @FundCashRefPK int
declare @FPDate date

truncate table [ZTEMP_Import_Investment_DEPOSITO]
TRUNCATE TABLE ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO

if @ClientCode = '20'
    set @Type = 'TD'
else
	set @Type	= 'INV'

set @LastNo	= 0
set @LastRow = 0
set @AlertExposure = 0
set @StatusSuccess = 1
set @counter = 1
--set @msg = ''

select @MaxInvestmentPK = isnull(max(InvestmentPK), 0) from Investment

select @FPDate = date from FundPosition where status = 2 and date = (
	select max(date) from FundPosition where status = 2 and date <= @ValueDate
)

begin transaction

DECLARE OMS CURSOR
FOR 
	select D.FundPK,B.BankPK,C.BankBranchPK,C.InterestDaysType,F.Code InterestPaymentType,G.Code PaymentModeOnMaturity,
	case when A.TrxType = 'Placement' then 1 when A.TrxType = 'Liquidate' then 2 when A.TrxType = 'Rollover' then 3 else 0 end TrxType,A.TrxType TrxTypeID,
	isnull(A.SysNo,0) InvestmentPK,isnull(E.InstrumentPK,0) InstrumentPK,cast(A.PlacementDate as date) PlacementDate,cast(A.MaturityDate as date) MaturityDate,
	case when A.InterestRate = '' then 0 else cast(A.InterestRate as numeric(22,2)) end InterestRate,case when isnull(A.BreakInterestRate,'') = '' then 0 else cast(isnull(A.BreakInterestRate,0) as numeric(22,2)) end BreakInterestRate,
	case when A.RollOverWithInterest = 'Yes' then 1 else 0 end BitRollOverWithInterest, case when A.StatutoryType = 'Yes' then 2 else 0 end StatutoryType,isnull(A.Remarks,'') InvestmentNotes,
	cast(A.Principal as numeric(32,2)) + case when A.RollOverWithInterest = 'Yes' then dbo.FGetAccruedInterestDepositoActAct(E.AcqDate,E.MaturityDate,A.Principal,A.InterestRate,C.InterestDaysType) else 0 end  Principal,
	case when A.BitBreakable = 'Yes' then 1 else 0 end BitRollOverWithInterest,isnull(A.Category,'') Category, isnull(D.CurrencyPK,0) CurrencyPK,isnull(SysNo,0)
	from OMSTimeDepositImportTemp A
	left join Bank B on A.Bank = B.ID and B.Status = 2
	left join BankBranch C on A.BankBranch = C.PTPCode and C.Status = 2 and B.BankPK = C.BankPK
	left join Fund D on A.Fund = D.ID and D.Status = 2
	left join Investment E on A.SysNo = E.InvestmentPK and E.statusSettlement = 2
	left join MasterValue F on A.InterestPaymentType = F.DescOne and F.ID = 'InterestPaymentType' and F.Status = 2
	left join MasterValue G on A.PaymentModeOnMature = G.DescOne and G.ID = 'PaymentModeOnmaturity' and G.Status = 2
 
OPEN OMS;
 
FETCH NEXT 
FROM OMS INTO @FundPK,@BankPK,@BankBranchPK,@InterestDaysType,@InterestPaymentType,@PaymentModeOnMaturity,@TrxType,@TryTypeID,@investmentPK,
			@instrumentPK,@PlacementDate,@MaturityDate,@InterestRate,@BreakInterestRate,@BitRollOverInterest,@StatutoryType,@InvestmentNotes,@Amount,@BitBreakable,@Category,@CurrencyPK,@TrxBuy
 
WHILE @@FETCH_STATUS = 0
    BEGIN

		------check posisi fundposition
		if not exists (
			select * from FundPosition where [identity] = @investmentPK and status = 2 and FundPK = @FundPK and BankPK = @BankPK and BankBranchPK = @BankBranchPK and date = @FPDate 
		) and @investmentPK <> 0 and @TrxType in (2,3)
		begin
			set @StatusSuccess = 0
			set @msg = COALESCE(@msg + ', ', '') + cast(@investmentPK as nvarchar)
		end
	
		set @FundCashRefPK = 0
		select @FundCashRefPK = isnull(FundCashRefPK,0) from FundCashRef where bitdefaultinvestment = 1 and status = 2 and fundpk = @FundPK
		set @FundCashRefPK = isnull(@FundCashRefPK,0)

		select @PeriodPK = PeriodPK 
		from Period 
		where @ValueDate between DateFrom and DateTo and [Status] = 2
		
		if @TrxType = 2
		begin
			set @MaxInvestmentPK = @MaxInvestmentPK + 1	

			--ambil instrument dari FP
            select @instrumentPK = InstrumentPK,@InterestDaysType = InterestDaysType,@AcqDate = AcqDate from FundPosition where [identity] = @investmentPK and status = 2
                
			set @instrumentPK = isnull(@instrumentPK,0)
			set @InterestDaysType = isnull(@InterestDaysType,0)
			set @AcqDate = isnull(@AcqDate,'1900-01-01')


			--cek exposure
			select @ExposureType = ExposureType, @ExposureID = ExposureID, @AlertExposure = AlertExposure,
				   @ExposurePercent = ExposurePercent, @MaxExposurePercent = MaxExposurePercent, @MinExposurePercent = MinExposurePercent
			from [dbo].[FCheckExposure] (@ValueDate, @instrumentPK, @FundPK, @Amount, @TrxType,3)

			-- AlertExposure = 1 => AlertMaxExposure = 0 and WarningMaxExposure = 1
			-- AlertExposure = 2 => AlertMaxExposure = 1 and WarningMaxExposure = 1
			-- AlertExposure = 3 => AlertMinExposure = 0 and WarningMinExposure = 1
			-- AlertExposure = 4 => AlertMinExposure = 1 and WarningMinExposure = 1

			exec dbo.[FundExposureForImportOMSTimeDeposit] @date = @valuedate,@FundPK = @FundPK,@InstrumentPK = @InstrumentPK,@Amount = @Amount,@trxType = @TrxType, @InvPK = @MaxInvestmentPK

				if @ClientCode = '20'
                begin

                    select @MaxPK = max(ReferenceForNikkoPK) from ReferenceForNikko
                    set @MaxPK = isnull(@MaxPK,0) + 1

                    select @No = MAX(no) from ReferenceForNikko where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type
						
	                set @No = isnull(@No,0) + 1

                    if exists (select * from ReferenceForNikko where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type)
                    begin
	                    update ReferenceForNikko set No = @No where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type
                    end
                    else 
                    begin
	                    insert into ReferenceForNikko
	                    select @MaxPK,@No,@Type,month(@ValueDate),year(@ValueDate)
                    end

                    set @CurReference = cast(@No as nvarchar) + '/' + @Type + '/NSI/FM/' + cast(SUBSTRING(CONVERT(nvarchar(6),@ValueDate, 112),5,2) as nvarchar) + '/' + substring(cast(year(@ValueDate) as nvarchar),3,2)

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

				--insert into temp
				insert into [ZTEMP_Import_Investment_DEPOSITO] (
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
				select @MaxInvestmentPK as InvestmentPK, 0 as DealingPK, 0 as SettlementPK, 1 as HistoryPK,
				1 as StatusInvestment, 0 as StatusDealing, 0 as StatusSettlement, @ValueDate as ValueDate, 1 as MarketPK, @PeriodPK as PeriodPK, @ValueDate as InstructionDate,
				@CurReference,
				5 as InstrumentTypePK, @TrxType TrxType, @TryTypeID TrxTypeID, 
				0 as CounterpartPK, @InstrumentPK, @FundPK, @FundCashRefPK as FundCashRefPK, 1 OrderPrice, 0 as Lot, 
				0 LotInShare, 0 as RangePrice, 0 as AcqPrice, cast(@Amount as numeric(22,4)) as Volume, cast(@Amount as numeric(22,4)) as Amount, 
				@InterestRate as InterestPercent, @BreakInterestRate as BreakInterestPercent, 0 as AccruedInterest, @ValueDate SettlementDate, @InvestmentNotes as InvestmentNotes, 
				0 as DoneLot, cast(@Amount as numeric(22,4)) as DoneVolume, 0 DonePrice, cast(@Amount as numeric(22,4)) as DoneAmount, 
				0 as Tenor, 0 as CommissionPercent, 0 as LevyPercent, 0 as KPEIPercent, 0 as VATPercent, 0 as WHTPercent, 0 as OTCPercent, 0 as IncomeTaxSellPercent, 
				0 as IncomeTaxInterestPercent, 0 as IncomeTaxGainPercent, 0 as CommissionAmount, 0 as LevyAmount, 0 as KPEIAmount, 0 as VATAmount, 0 as WHTAmount, 0 as OTCAmount, 
				0 as IncomeTaxSellAmount, 0 as IncomeTaxInterestAmount, 0 as IncomeTaxGainAmount, cast(@Amount as numeric(22,4)) as TotalAmount,
				0 as CurrencyRate, 0 as AcqPrice1, 0 as AcqPrice2, 0 as AcqPrice3, 0 as AcqPrice4, 0 as AcqPrice5, 
				0 as SettlementMode, 0 as BoardType, @InterestDaysType as InterestDaysType, @InterestPaymentType as InterestPaymentType, 
				@PaymentModeOnMaturity as PaymentModeOnMaturity, 0 as PriceMode, 0 as BitIsAmortized, 0 as Posted, 0 as Revised, 
				@UsersID as EntryUsersID, @LastUpdate as EntryTime, @LastUpdate as LastUpdate,
				0 as SelectedInvestment, 0 as SelectedDealing, 0 as SelectedSettlement, @BankBranchPK as BankBranchPK, @BankPK as BankPK, 
				0 as AcqVolume, 0 as AcqVolume1, 0 as AcqVolume2, 0 as AcqVolume3, 0 as AcqVolume4, 0 as AcqVolume5, 0 as AcqPrice6, 
				0 as AcqVolume6, 0 as AcqPrice7, 0 as AcqVolume7, 0 as AcqPrice8, 0 as AcqVolume8, 0 as AcqPrice9, 0 as AcqVolume9, 0 as TaxExpensePercent, 0 as YieldPercent, 0 as DoneAccruedInterest,
				@StatutoryType StatutoryType, @BitRollOverInterest BitRollOverInterest, @BitBreakable, @Category Category, @MaturityDate MaturityDate, @TrxBuy TrxBuy, @AcqDate AcqDate

		end
		else
		begin
			select @InstrumentID = ID from Instrument where InstrumentPK = @NewinstrumentPK and status = 2


            if @TrxType = 3
            begin
                select @InterestDaysType = InterestDaysType,@AcqDate = AcqDate from FundPosition where [identity] = @investmentPK and status = 2
                
			    set @InterestDaysType = isnull(@InterestDaysType,0)
			    set @AcqDate = isnull(@AcqDate,'1900-01-01')
            end
			else if @TrxType = 1
			begin
				 set @AcqDate = isnull(@ValueDate,'1900-01-01')
			end

			--cek available cash
			select @BitAvailableCash = [dbo].[FCheckAvailableCash] (@ValueDate, @FundPK, @Amount)
			if isnull(@BitAvailableCash, 0) = 1
			begin
				set @msg		= 'Import OMS Time Deposit Canceled, </br> Can Not Process This Data! </br> Exposure : ' + cast(@InstrumentID as nvarchar(30)) + ' </br> Cash Not Available '
			end

			if isnull(@BitAvailableCash, 0) = 0 -- Cash Available
			BEGIN

				set @MaxInvestmentPK = @MaxInvestmentPK + 1	

				--create new instrument
                select @BankID = ID From Bank Where BankPK = @BankPK and Status = 2
                select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
                set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)

                Insert Into Instrument(InstrumentPK,HistoryPK,Status,ID,Name,InstrumentTypePK,DepositoTypePK,lotinshare,BankPK,InterestPercent,MaturityDate,CurrencyPK,Category,TaxExpensePercent,IssuerPK,[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)
                Select @MaxInstrumentPK,1,2,@BankID, 'TDP ' +@BankID,5,1,1,@BankPK,@InterestRate,@MaturityDate,@CurrencyPK,@Category,20,IssuerPK,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate
                from Bank 
                where BankPK = @BankPK and status in (1,2)
                
				set @instrumentPK = @MaxInstrumentPK


				--cek Exposure
				select @ExposureType = ExposureType, @ExposureID = ExposureID, @AlertExposure = AlertExposure,
					   @ExposurePercent = ExposurePercent, @MaxExposurePercent = MaxExposurePercent, @MinExposurePercent = MinExposurePercent
				from [dbo].[FCheckExposure] (@ValueDate, @instrumentPK, @FundPK, @Amount, @TrxType,3)

				-- AlertExposure = 1 => AlertMaxExposure = 0 and WarningMaxExposure = 1
				-- AlertExposure = 2 => AlertMaxExposure = 1 and WarningMaxExposure = 1
				-- AlertExposure = 3 => AlertMinExposure = 0 and WarningMinExposure = 1
				-- AlertExposure = 4 => AlertMinExposure = 1 and WarningMinExposure = 1
				exec dbo.[FundExposureForImportOMSTimeDeposit] @date = @valuedate,@FundPK = @FundPK,@InstrumentPK = @InstrumentPK,@Amount = @Amount,@trxType = @TrxType, @InvPK = @MaxInvestmentPK

				if @ClientCode = '20'
                begin

                    select @MaxPK = max(ReferenceForNikkoPK) from ReferenceForNikko
                    set @MaxPK = isnull(@MaxPK,0) + 1

                    select @No = MAX(no) from ReferenceForNikko where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type
						
	                set @No = isnull(@No,0) + 1

                    if exists (select * from ReferenceForNikko where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type)
                    begin
	                    update ReferenceForNikko set No = @No where month = month(@ValueDate) and year = year(@ValueDate) and Type = @Type
                    end
                    else 
                    begin
	                    insert into ReferenceForNikko
	                    select @MaxPK,@No,@Type,month(@ValueDate),year(@ValueDate)
                    end

                    set @CurReference = cast(@No as nvarchar) + '/' + @Type + '/NSI/FM/' + cast(SUBSTRING(CONVERT(nvarchar(6),@ValueDate, 112),5,2) as nvarchar) + '/' + substring(cast(year(@ValueDate) as nvarchar),3,2)

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

				--insert into temp
				insert into [ZTEMP_Import_Investment_DEPOSITO] (
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
				select @MaxInvestmentPK as InvestmentPK, 0 as DealingPK, 0 as SettlementPK, 1 as HistoryPK,
				1 as StatusInvestment, 0 as StatusDealing, 0 as StatusSettlement, @ValueDate as ValueDate, 1 as MarketPK, @PeriodPK as PeriodPK, @ValueDate as InstructionDate,
				@CurReference,
				5 as InstrumentTypePK, @TrxType TrxType, @TryTypeID TrxTypeID, 
				0 as CounterpartPK, @InstrumentPK, @FundPK, 0 as FundCashRefPK, 1 OrderPrice, 0 as Lot, 
				0 LotInShare, 0 as RangePrice, 0 as AcqPrice, cast(@Amount as numeric(22,4)) as Volume, cast(@Amount as numeric(22,4)) as Amount, 
				@InterestRate as InterestPercent, @BreakInterestRate as BreakInterestPercent, 0 as AccruedInterest, @ValueDate as SettlementDate, @InvestmentNotes as InvestmentNotes, 
				0 as DoneLot, cast(@Amount as numeric(22,4)) as DoneVolume, 0 DonePrice, cast(@Amount as numeric(22,4)) as DoneAmount, 
				0 as Tenor, 0 as CommissionPercent, 0 as LevyPercent, 0 as KPEIPercent, 0 as VATPercent, 0 as WHTPercent, 0 as OTCPercent, 0 as IncomeTaxSellPercent, 
				0 as IncomeTaxInterestPercent, 0 as IncomeTaxGainPercent, 0 as CommissionAmount, 0 as LevyAmount, 0 as KPEIAmount, 0 as VATAmount, 0 as WHTAmount, 0 as OTCAmount, 
				0 as IncomeTaxSellAmount, 0 as IncomeTaxInterestAmount, 0 as IncomeTaxGainAmount, cast(@Amount as numeric(22,4)) as TotalAmount,
				0 as CurrencyRate, 0 as AcqPrice1, 0 as AcqPrice2, 0 as AcqPrice3, 0 as AcqPrice4, 0 as AcqPrice5, 
				0 as SettlementMode, 0 as BoardType, @InterestDaysType as InterestDaysType, @InterestPaymentType as InterestPaymentType, 
				@PaymentModeOnMaturity as PaymentModeOnMaturity, 0 as PriceMode, 0 as BitIsAmortized, 0 as Posted, 0 as Revised, 
				@UsersID as EntryUsersID, @LastUpdate as EntryTime, @LastUpdate as LastUpdate,
				0 as SelectedInvestment, 0 as SelectedDealing, 0 as SelectedSettlement, @BankBranchPK as BankBranchPK, @BankPK as BankPK, 
				0 as AcqVolume, 0 as AcqVolume1, 0 as AcqVolume2, 0 as AcqVolume3, 0 as AcqVolume4, 0 as AcqVolume5, 0 as AcqPrice6, 
				0 as AcqVolume6, 0 as AcqPrice7, 0 as AcqVolume7, 0 as AcqPrice8, 0 as AcqVolume8, 0 as AcqPrice9, 0 as AcqVolume9, 0 as TaxExpensePercent, 0 as YieldPercent, 0 as DoneAccruedInterest,
				@StatutoryType StatutoryType, @BitRollOverInterest BitRollOverInterest, @BitBreakable, @Category Category, @MaturityDate MaturityDate, @TrxBuy TrxBuy, @AcqDate AcqDate

			END
			


		end


        FETCH NEXT 
		FROM OMS INTO @FundPK,@BankPK,@BankBranchPK,@InterestDaysType,@InterestPaymentType,@PaymentModeOnMaturity,@TrxType,@TryTypeID,@investmentPK,
					@instrumentPK,@PlacementDate,@MaturityDate,@InterestRate,@BreakInterestRate,@BitRollOverInterest,@StatutoryType,@InvestmentNotes,@Amount,@BitBreakable,@Category,@CurrencyPK,@TrxBuy
    END;
 
CLOSE OMS;
 
DEALLOCATE OMS;


if(@StatusSuccess = 1)
begin
	commit transaction
	set @msg = 'Import OMS Time Deposit Success'
end
Else
begin
	rollback transaction
	if @StatusSuccess = 0
		set @msg = 'Please check sys no : ' + @msg + ' , make sure the sys no is correct'
end

select @msg as ResultMsg


                            ";
                        cmd2.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd2.Parameters.AddWithValue("@UsersID", _userID);
                        cmd2.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd2.Parameters.AddWithValue("@LastUpdate", _now);
                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            if (!dr2.HasRows)
                            {

                                _msg = "Import OMS Time Deposit Canceled, import data not found!";
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
                return "Import OMS Time Deposit Error : " + err.Message.ToString();
                throw err;
            }
        }

        private DataTable CreateDataTableFromOMSDepositoTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SysNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ValueDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Bank";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BankBranch";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TrxType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Category";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BitBreakable";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PlacementDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MaturityDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InterestRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InterestPaymentType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentModeOnMature";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BreakInterestRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "RolloverWithInterest";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Principal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "StatutoryType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Remarks";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            string SheetName = "SELECT * FROM [Sheet2$]";
                            odCmd.CommandText = SheetName;
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

                                    dr["SysNo"] = odRdr[0];
                                    dr["ValueDate"] = "";
                                    dr["TrxType"] = odRdr[1];
                                    dr["Fund"] = odRdr[2];
                                    dr["Category"] = odRdr[3];
                                    dr["Bank"] = odRdr[4];
                                    dr["BankBranch"] = odRdr[5];
                                    dr["PlacementDate"] = odRdr[6];
                                    dr["MaturityDate"] = odRdr[7];
                                    dr["InterestRate"] = odRdr[8];
                                    dr["Principal"] = odRdr[9];
                                    dr["BreakInterestRate"] = odRdr[10];
                                    dr["RolloverWithInterest"] = odRdr[11];
                                    dr["BitBreakable"] = odRdr[12];
                                    dr["InterestPaymentType"] = odRdr[13];
                                    dr["PaymentModeOnMature"] = odRdr[14];
                                    dr["StatutoryType"] = odRdr[15];
                                    dr["Remarks"] = odRdr[16];

                                    if (dr["TrxType"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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
                throw err;
            }
        }

        public Boolean Generate_OMSTimeDeposit(string _userID, DateTime _date)
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
declare @Yesterday date
select @Yesterday = max(date) from FundPosition where status = 2

select	A.[Identity]	SysNo,
		A.Date		ValueDate,
		B.ID			Fund,
		C.ID			Bank,
		D.PTPCode		BankBranch,
		'Placement'		TrxType,
		A.Category		Category,
		case when A.BitBreakable = 1 then 'Yes' else 'No' end BitBreakable,
		A.AcqDate	PlacementDate,
		A.MaturityDate		MaturityDate,
		A.InterestPercent	InterestRate,
		MV1.DescOne			InterestPaymentType,
		MV2.DescOne			PaymentModeOnMature,
		0 BreakInterestRate,
		'' RolloverWithInterest,
		A.Balance Principal,
		'' StatutoryType,
		'' Remarks
		From FundPosition A
Left Join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
Left Join Bank C on A.BankPK = C.BankPK and C.Status in (1,2)
Left Join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
left join MasterValue MV1 on A.InterestPaymentType = MV1.Code and MV1.ID = 'InterestPaymentType' and MV1.Status = 2
left join MasterValue MV2 on A.PaymentModeOnMaturity = MV2.Code and MV2.ID = 'PaymentModeOnmaturity' and MV2.Status = 2
left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.Status = 2
where InstrumentTypePK in (5) and A.MaturityDate >= @ValueDate and A.Date = @Yesterday and A.Status = 2
and A.[Identity] not in (
    select isnull(TrxBuy,0) from Investment where StatusInvestment in (1,2) and InstrumentTypePK = 5 and TrxType in (2,3)
)
 ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        //cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "TemplateOMSDeposito" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "TemplateOMSDeposito" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "Report";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<TemplateOMSDeposito> rList = new List<TemplateOMSDeposito>();
                                    while (dr0.Read())
                                    {

                                        TemplateOMSDeposito rSingle = new TemplateOMSDeposito();
                                        rSingle.SysNo = Convert.ToInt32(dr0["SysNo"]);
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Fund = dr0["Fund"].ToString();
                                        rSingle.Bank = dr0["Bank"].ToString();
                                        rSingle.BankBranch = dr0["BankBranch"].ToString();
                                        rSingle.TrxType = dr0["TrxType"].ToString();
                                        rSingle.Category = dr0["Category"].ToString();
                                        rSingle.BitBreakable = dr0["BitBreakable"].ToString();
                                        rSingle.PlacementDate = Convert.ToDateTime(dr0["PlacementDate"]);
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                        rSingle.InterestRate = Convert.ToDecimal(dr0["InterestRate"]);
                                        rSingle.InterestPaymentType = dr0["InterestPaymentType"].ToString();
                                        rSingle.PaymentModeOnMature = dr0["PaymentModeOnMature"].ToString();
                                        rSingle.BreakInterestRate = Convert.ToDecimal(dr0["BreakInterestRate"]);
                                        rSingle.RolloverWithInterest = dr0["RolloverWithInterest"].ToString();
                                        rSingle.Principal = Convert.ToDecimal(dr0["Principal"]);
                                        rSingle.StatutoryType = dr0["StatutoryType"].ToString();
                                        rSingle.Remarks = dr0["Remarks"].ToString();
                                        rList.Add(rSingle);

                                    }



                                    var GroupByReference =
                                            from r in rList
                                                //orderby r ascending
                                            group r by new { } into rGroup
                                            select rGroup;

                                    int incRowExcel = 0;


                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel++;




                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells[incRowExcel, 1].Value = "Sys No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "TrxType";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 3].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 4].Value = "Category";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Bank";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Bank Branch";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Placement Date";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 8].Value = "Maturity Date";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Interest Rate";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        worksheet.Cells[incRowExcel, 10].Value = "Principal";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Break Interest Rate";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 12].Value = "Rollover With Interest";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 13].Value = "BitBreakable";
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 14].Value = "Interest Payment Type";
                                        worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 15].Value = "Payment Mode On Mature";
                                        worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 16].Value = "Statutory Type";
                                        worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 17].Value = "Remarks";
                                        worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        int _no = 1;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.SysNo;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.TrxType;

                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.Fund;

                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Category;

                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Bank;

                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.BankBranch;

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.PlacementDate;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "MM/dd/yyyy";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.MaturityDate;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "MM/dd/yyyy";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            if (rsDetail.InterestRate == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Value = "";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.InterestRate;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            }

                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Principal;

                                            if (rsDetail.BreakInterestRate == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 11].Value = "";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.BreakInterestRate;
                                            }

                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.RolloverWithInterest;

                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.BitBreakable;
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.InterestPaymentType;

                                            worksheet.Cells[incRowExcel, 15].Value = rsDetail.PaymentModeOnMature;


                                            worksheet.Cells[incRowExcel, 16].Value = rsDetail.StatutoryType;

                                            worksheet.Cells[incRowExcel, 17].Value = rsDetail.Remarks;


                                            _no++;
                                            _endRowDetail = incRowExcel;


                                        }


                                    }

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 17];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 12;
                                    worksheet.Column(3).Width = 21;
                                    worksheet.Column(4).Width = 16;
                                    worksheet.Column(5).Width = 12;
                                    worksheet.Column(6).Width = 12;
                                    worksheet.Column(7).Width = 16;
                                    worksheet.Column(8).Width = 16;
                                    worksheet.Column(9).Width = 15;
                                    worksheet.Column(10).Width = 18;
                                    worksheet.Column(11).Width = 18;
                                    worksheet.Column(12).Width = 21;
                                    worksheet.Column(13).Width = 12;
                                    worksheet.Column(14).Width = 28;
                                    worksheet.Column(15).Width = 28;
                                    worksheet.Column(16).Width = 14;
                                    worksheet.Column(17).Width = 21;
                                    //worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Template Import OMS Time Deposit";



                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    worksheet = package.Workbook.Worksheets.Add("Sheet2");


                                    incRowExcel = 0;

                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                    worksheet.Cells[incRowExcel, 1].Value = "Sys No";
                                    worksheet.Cells[incRowExcel, 1].AddComment("Fill If Liquidate / Rollover", "User");
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 2].Value = "TrxType";
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 3].Value = "Fund";
                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 4].Value = "Category";
                                    worksheet.Cells[incRowExcel, 4].AddComment("NORMAL / On Call", "User");
                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 5].Value = "Bank";
                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 6].Value = "Bank Branch";
                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 7].Value = "Placement Date";
                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 8].Value = "Maturity Date";
                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 9].Value = "Interest Rate";
                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                    worksheet.Cells[incRowExcel, 10].Value = "Principal";
                                    worksheet.Cells[incRowExcel, 10].AddComment("Amount", "User");
                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 11].Value = "Break Interest Rate";
                                    worksheet.Cells[incRowExcel, 11].AddComment("Fill 'Yes' If User Want To Break the Liquidate", "User");
                                    worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 12].Value = "Rollover With Interest";
                                    worksheet.Cells[incRowExcel, 12].AddComment("Fill 'Yes' If Rollover With Or Without Interest", "User");
                                    worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 13].Value = "BitBreakable";
                                    worksheet.Cells[incRowExcel, 13].AddComment("Fill 'Yes' If placement is Breakable", "User");
                                    worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 14].Value = "Interest Payment Type";
                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 15].Value = "Payment Mode On Mature";
                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                    worksheet.Cells[incRowExcel, 16].Value = "Statutory Type";
                                    worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                    worksheet.Cells[incRowExcel, 17].Value = "Remarks";
                                    worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 17];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 12;
                                    worksheet.Column(3).Width = 21;
                                    worksheet.Column(4).Width = 16;
                                    worksheet.Column(5).Width = 12;
                                    worksheet.Column(6).Width = 12;
                                    worksheet.Column(7).Width = 16;
                                    worksheet.Column(8).Width = 16;
                                    worksheet.Column(9).Width = 15;
                                    worksheet.Column(10).Width = 18;
                                    worksheet.Column(11).Width = 18;
                                    worksheet.Column(12).Width = 21;
                                    worksheet.Column(13).Width = 12;
                                    worksheet.Column(14).Width = 28;
                                    worksheet.Column(15).Width = 28;
                                    worksheet.Column(16).Width = 14;
                                    worksheet.Column(17).Width = 21;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                                                                   // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Template Import OMS Time Deposit";



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

        public int OMSTimeDeposit_CheckExposureFromImport()
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
	                                select * from ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO
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
                            select * from ZTEMP_Import_Investment_DEPOSITO

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




    }

}