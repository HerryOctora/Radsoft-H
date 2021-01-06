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
    public class InstrumentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[Instrument] 
                            ([InstrumentPK],[HistoryPK],[Status],[ID],[Name],[Category],[Affiliated],[InstrumentTypePK]
                            ,[ReksadanaTypePK],[DepositoTypePK],[ISIN],[BankPK],[IssuerPK],[SectorPK],[HoldingPK]
                            ,[MarketPK],[IssueDate],[MaturityDate],[InterestPercent],[InterestPaymentType],[InterestType]
                            ,[LotInShare],[BitIsSuspend],[CurrencyPK],[RegulatorHaircut],[Liquidity],[NAWCHaircut]
                            ,[CompanyHaircut],[BondRating],[BitIsShortSell],[BitIsMargin],[BitIsScriptless],[TaxExpensePercent]
                            ,[InterestDaysType],[BloombergCode],[BitIsForeign],[FirstCouponDate],[CounterpartPK],[BankAccountNo],[SAPCustID],[InstrumentCompanyTypePK],[AnotherRating],[BloombergSecID],[ShortName],[BloombergISIN],";

        string _paramaterCommand = @"@ID,@Name,@Category,@Affiliated,@InstrumentTypePK
                            ,@ReksadanaTypePK,@DepositoTypePK,@ISIN,@BankPK,@IssuerPK,@SectorPK,@HoldingPK
                            ,@MarketPK,@IssueDate,@MaturityDate,@InterestPercent,@InterestPaymentType,@InterestType
                            ,@LotInShare,@BitIsSuspend,@CurrencyPK,@RegulatorHaircut,@Liquidity,@NAWCHaircut
                            ,@CompanyHaircut,@BondRating,@BitIsShortSell,@BitIsMargin,@BitIsScriptless,@TaxExpensePercent
                            ,@InterestDaysType,@BloombergCode,@BitIsForeign,@FirstCouponDate,@CounterpartPK,@BankAccountNo,@SAPCustID,@InstrumentCompanyTypePK,@AnotherRating,@BloombergSecID,@ShortName,@BloombergISIN,";


        //2
        private Instrument setInstrument(SqlDataReader dr)
        {
            Instrument M_Instrument = new Instrument();
            M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_Instrument.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Instrument.Status = Convert.ToInt32(dr["Status"]);
            M_Instrument.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Instrument.Notes = Convert.ToString(dr["Notes"]);
            M_Instrument.ID = Convert.ToString(dr["ID"]);
            M_Instrument.Name = Convert.ToString(dr["Name"]);
            M_Instrument.Category = Convert.ToString(dr["Category"]);
            M_Instrument.Affiliated = Convert.ToBoolean(dr["Affiliated"]);
            M_Instrument.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_Instrument.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_Instrument.ReksadanaTypePK = Convert.ToInt32(dr["ReksadanaTypePK"]);
            M_Instrument.ReksadanaTypeID = Convert.ToString(dr["ReksadanaTypeID"]);
            M_Instrument.DepositoTypePK = Convert.ToInt32(dr["DepositoTypePK"]);
            M_Instrument.DepositoTypeID = Convert.ToString(dr["DepositoTypeID"]);
            M_Instrument.ISIN = Convert.ToString(dr["ISIN"]);
            M_Instrument.BankPK = Convert.ToInt32(dr["BankPK"]);
            M_Instrument.BankID = Convert.ToString(dr["BankID"]);
            M_Instrument.BankName = Convert.ToString(dr["BankName"]);
            M_Instrument.IssuerPK = Convert.ToInt32(dr["IssuerPK"]);
            M_Instrument.IssuerID = Convert.ToString(dr["IssuerID"]);
            M_Instrument.IssuerName = Convert.ToString(dr["IssuerName"]);
            M_Instrument.SectorPK = Convert.ToInt32(dr["SectorPK"]);
            M_Instrument.SectorID = Convert.ToString(dr["SectorID"]);
            M_Instrument.SectorName = Convert.ToString(dr["SectorName"]);
            M_Instrument.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_Instrument.HoldingID = Convert.ToString(dr["HoldingID"]);
            M_Instrument.HoldingName = Convert.ToString(dr["HoldingName"]);
            M_Instrument.MarketPK = Convert.ToInt32(dr["MarketPK"]);
            M_Instrument.MarketID = Convert.ToString(dr["MarketID"]);
            M_Instrument.IssueDate = Convert.ToString(dr["IssueDate"]);
            M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
            M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
            M_Instrument.InterestPaymentType = Convert.ToInt32(dr["InterestPaymentType"]);
            M_Instrument.InterestPaymentTypeDesc = Convert.ToString(dr["InterestPaymentTypeDesc"]);
            M_Instrument.InterestType = Convert.ToInt32(dr["InterestType"]);
            M_Instrument.InterestTypeDesc = Convert.ToString(dr["InterestTypeDesc"]);
            M_Instrument.LotInShare = Convert.ToDecimal(dr["LotInShare"]);
            M_Instrument.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            M_Instrument.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_Instrument.RegulatorHaircut = Convert.ToDecimal(dr["RegulatorHaircut"]);
            M_Instrument.Liquidity = Convert.ToDecimal(dr["Liquidity"]);
            M_Instrument.NAWCHaircut = Convert.ToDecimal(dr["NAWCHaircut"]);
            M_Instrument.CompanyHaircut = Convert.ToDecimal(dr["CompanyHaircut"]);
            M_Instrument.BondRating = Convert.ToString(dr["BondRating"]);
            M_Instrument.BondRatingDesc = Convert.ToString(dr["BondRatingDesc"]);
            M_Instrument.BitIsShortSell = Convert.ToBoolean(dr["BitIsShortSell"]);
            M_Instrument.BitIsMargin = Convert.ToBoolean(dr["BitIsMargin"]);
            M_Instrument.BitIsScriptless = Convert.ToBoolean(dr["BitIsScriptless"]);
            M_Instrument.TaxExpensePercent = Convert.ToDecimal(dr["TaxExpensePercent"]);
            M_Instrument.InterestDaysType = Convert.ToInt32(dr["InterestDaysType"]);
            M_Instrument.InterestDaysTypeDesc = Convert.ToString(dr["InterestDaysTypeDesc"]);
            M_Instrument.BloombergCode = Convert.ToString(dr["BloombergCode"]);
            M_Instrument.BitIsForeign = Convert.ToBoolean(dr["BitIsForeign"]);
            M_Instrument.FirstCouponDate = Convert.ToString(dr["FirstCouponDate"]);
            M_Instrument.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_Instrument.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_Instrument.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            M_Instrument.BankAccountNo = Convert.ToString(dr["BankAccountNo"]);
            M_Instrument.SAPCustID = Convert.ToString(dr["SAPCustomerID"]);

            M_Instrument.InstrumentCompanyTypePK = Convert.ToInt32(dr["InstrumentCompanyTypePK"]);
            M_Instrument.InstrumentCompanyTypeID = Convert.ToString(dr["InstrumentCompanyTypeID"]);
            M_Instrument.InstrumentCompanyTypeName = Convert.ToString(dr["InstrumentCompanyTypeName"]);
            M_Instrument.AnotherRating = Convert.ToString(dr["AnotherRating"]);
            M_Instrument.BloombergSecID = Convert.ToString(dr["BloombergSecID"]);
            M_Instrument.ShortName = Convert.ToString(dr["ShortName"]);

            M_Instrument.BloombergISIN = Convert.ToString(dr["BloombergISIN"]);


            M_Instrument.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Instrument.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Instrument.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Instrument.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Instrument.EntryTime = dr["EntryTime"].ToString();
            M_Instrument.UpdateTime = dr["UpdateTime"].ToString();
            M_Instrument.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Instrument.VoidTime = dr["VoidTime"].ToString();
            M_Instrument.DBUserID = dr["DBUserID"].ToString();
            M_Instrument.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Instrument.LastUpdate = dr["LastUpdate"].ToString();
            M_Instrument.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);

            return M_Instrument;
        }


        // INSTRUMENT REPS

        public List<InstrumentForInvestment> Instrument_LookupForOMSDeposito(int _trxType, int _fundPK, DateTime _date, DateTime _instDate)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = "select InstrumentPK,I.BankPK,I.ID + ' - ' + I.Name ID, I.Name,'' AcqDate, I.MaturityDate, I.InterestPercent,C.ID BankID,0 Balance, I.Category, D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT " +
                               "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 Left join Bank C on I.BankPK = C.BankPK and C.status = 2 Left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 " +
                               "where I.status = 2 and IT.Type = 3 and I.MaturityDate >= @Date ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                              
                        declare @ValueDateFrom date


if (dbo.CheckIsYesterdayHoliday(@Date) = 1)
	set @ValueDateFrom = dateadd(day,1,dbo.FWorkingDay(@Date,-1))
else
	set @ValueDateFrom = @Date

	Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
	                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date and FundPK = @FundPK
                        )
                        and status = 2 and FundPK = @FundPK


                     
                select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate,A.Balance + isnull(F.Balance,0) Balance
                ,A.Category,D.ID CurrencyID,A.CurrencyPK 
                ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 0 Flag
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
	                where ValueDate = @date and StatusInvestment in (1,2) and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 5 
	                and FundPK = @fundpk and TrxType  = 2
	                group By A.InstrumentPK,A.BankBranchPK
                )F on A.InstrumentPK = F.InstrumentPK and A.BankBranchPK = F.BankBranchPK 
                where A.maturityDate > @ValueDateFrom and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and E.Type in (3) and A.status = 2    
				--where A.maturityDate > @date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and E.Type in (3) and A.status = 2                                  
                and A.Balance + isnull(F.Balance,0) > 0

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
	                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
                        )
                        and status = 2


                     
                select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate,A.Balance + isnull(F.Balance,0) Balance
                ,A.Category,D.ID CurrencyID,A.CurrencyPK 
                ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 0 Flag
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
	                where ValueDate = @date and StatusInvestment in (1,2) and StatusDealing <> 3 and StatusSettlement <> 3 and A.instrumentTypePK = 5 
	                and FundPK = @fundpk and TrxType  = 2
	                group By A.InstrumentPK,A.BankBranchPK
                )F on A.InstrumentPK = F.InstrumentPK and A.BankBranchPK = F.BankBranchPK 
                where A.maturityDate > @Date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and E.Type in (3) and A.status = 2                                
                and A.Balance + isnull(F.Balance,0) > 0

                                ";
                            }

                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else if (_trxType == 3)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                           
                                Declare @MaxDateEndDayFP datetime

                                Declare @TrailsPK int
                                
                                select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                                where ValueDate = 
                                (
                                select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @InstructionDate and FundPK = @FundPK
                                )
                                and status = 2 and FundPK = @FundPK

                    
                                select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate, isnull(A.Balance,0) Balance,A.Category,D.ID CurrencyID,A.CurrencyPK 
                                ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 1 Flag  from FundPosition A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                  
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                                where A.maturityDate = @InstructionDate and A.FundPK = @FundPK and A.TrailsPK = @TrailsPK and A.status  = 2 and E.Type = 3
                                and A.[Identity] in (
                                select TrxBuy from investment 
                                where ValueDate = @InstructionDate and TrxType = 3 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3)

                                union all

                                select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate, isnull(A.Balance,0) Balance,A.Category,D.ID CurrencyID,A.CurrencyPK 
                                ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 0 Flag from FundPosition A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                  
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                                where A.maturityDate = @InstructionDate and A.FundPK = @FundPK and A.TrailsPK = @TrailsPK and A.status  = 2 and E.Type = 3
                                and A.[Identity] not in (
                                select TrxBuy from investment 
                                where ValueDate = @InstructionDate and TrxType = 3 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3)
                                order by Flag,A.MaturityDate asc
                            ";
                            }
                            else
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                           
                    Declare @MaxDateEndDayFP datetime

                    Declare @TrailsPK int
                    --select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2
                                
                    select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                    where ValueDate = 
                    (
                    select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
                    )
                    and status = 2

                    
                    select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate, isnull(A.Balance,0) Balance,A.Category,D.ID CurrencyID,A.CurrencyPK 
                    ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 1 Flag  from FundPosition A
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                  
                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                    left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                    where A.maturityDate = @Date and A.FundPK = @FundPK and A.TrailsPK = @TrailsPK and A.status  = 2 and E.Type = 3
                    and A.[Identity] in (
                    select TrxBuy from investment 
                    where ValueDate = @Date and TrxType = 3 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3)

                    union all

                    select A.[Identity] NoFP,A.InstrumentPK,B.BankPK,B.ID + ' - ' + B.Name ID,C.ID BankID,A.InterestPercent,A.AcqDate,A.MaturityDate, isnull(A.Balance,0) Balance,A.Category,D.ID CurrencyID,A.CurrencyPK 
                    ,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.BankBranchPK,A.BitBreakable, 0 Flag from FundPosition A
                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                  
                    Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                    Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                    left join InstrumentType E on B.InstrumentTypePK = E.InstrumentTypePK and E.status = 2
                    where A.maturityDate = @Date and A.FundPK = @FundPK and A.TrailsPK = @TrailsPK and A.status  = 2 and E.Type = 3
                    and A.[Identity] not in (
                    select TrxBuy from investment 
                    where ValueDate = @Date and TrxType = 3 and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3)
                    order by Flag,A.MaturityDate asc
                            ";
                            }

                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@InstructionDate", _instDate);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    if (_host.CheckColumnIsExist(dr, "NoFP"))
                                    {
                                        M_Instrument.NoFP = dr["NoFP"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["NoFP"]);
                                    }
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.BankPK = Convert.ToInt32(dr["BankPK"]);
                                    M_Instrument.BankID = Convert.ToString(dr["BankID"]);
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.Category = Convert.ToString(dr["Category"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    M_Instrument.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                    M_Instrument.InterestDaysType = Convert.ToInt32(dr["InterestDaysType"]);
                                    M_Instrument.InterestPaymentType = Convert.ToInt32(dr["InterestPaymentType"]);
                                    M_Instrument.PaymentModeOnMaturity = Convert.ToInt32(dr["PaymentModeOnMaturity"]);
                                    M_Instrument.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_Instrument.BitBreakable = Convert.ToBoolean(dr["BitBreakable"]);
                                    M_Instrument.Flag = dr["Flag"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Flag"]);
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

        public List<InstrumentForInvestment> Instrument_LookupForOMSBond(int _trxType, int _fundPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT " +
                               "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 " +
                               "where I.status = 2 and IT.Type in (2,5) ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                                Declare @TrailsPK int
                                select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2 and FundPK = @FundPK

                                select A.InstrumentPK,B.ID + ' - ' + B.Name ID,B.InterestPercent,B.MaturityDate,A.Balance,D.ID CurrencyID,A.CurrencyPK from FundPosition A    
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                where B.maturityDate >= @Date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3)
                                
                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,B.InterestPercent,B.MaturityDate,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID,1   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3) and OrderStatus in ('M','P')
                                and FundPK = @fundpk and TrxType  = 1
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate

                                ";
                            }
                            else
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                                Declare @TrailsPK int
                                select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2

                                select A.InstrumentPK,B.ID + ' - ' + B.Name ID,B.InterestPercent,B.MaturityDate,A.Balance,D.ID CurrencyID,A.CurrencyPK from FundPosition A    
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                where B.maturityDate >= @Date and A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3)
                                
                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,B.InterestPercent,B.MaturityDate,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID,1   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3) and OrderStatus in ('M','P')
                                and FundPK = @fundpk and TrxType  = 1
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate

                                ";
                            }
                            
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    M_Instrument.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<InstrumentForInvestment> Instrument_LookupForOMSEquity(int _trxType, int _fundPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT " +
                               "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 " +
                               "where I.status = 2 and IT.Type in (1) order by I.ID ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                                Declare @TrailsPK int
                                select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2 and FundPK = @FundPK

                                select InstrumentPK,ID + ' - Total Lot : ' + replace(CONVERT(varchar, CAST(sum(A.Balance)/100 AS money), 1),'.00','') ID,sum(A.Balance) Balance,CurrencyID from (
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

                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  and A.instrumentTypePK in (1)
                                and FundPK = @fundpk and TrxType  = 2
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

                                select InstrumentPK,ID + ' - Total Lot : ' + replace(CONVERT(varchar, CAST(sum(A.Balance)/100 AS money), 1),'.00','') ID,sum(A.Balance) Balance,CurrencyID from (
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

                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  and A.instrumentTypePK in (1)
                                and FundPK = @fundpk and TrxType  = 2
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate
                                ) A
                                group By InstrumentPK,ID,CurrencyID
                                order by ID

                                ";
                            }
                            
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public InstrumentForOmsDeposito Instrument_GetInformationByInstrumentPKForOMSDeposit(int _instrumentPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForOmsDeposito M_Instrument = new InstrumentForOmsDeposito();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"Select BankPK, CONVERT(VARCHAR(10), B.MaturityDate, 103) MaturityDate,B.InterestPercent,B.CurrencyPK,B.Category ,CONVERT(VARCHAR(10), B.AcqDate, 103) AcqDate
                        From Instrument A left join FundPosition B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        where A.status = 2 and B.InstrumentPK = @InstrumentPK and Date = (select max(date) from fundposition where status = 2) ";

                      

                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.BankPK = Convert.ToInt16(dr["BankPK"]);
                                    M_Instrument.CurrencyPK = Convert.ToInt16(dr["CurrencyPK"]);
                                    M_Instrument.Category = Convert.ToString(dr["Category"]);
                                    return M_Instrument;
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

        public List<Instrument> Instrument_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Instrument> L_Instrument = new List<Instrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"Select case when Instrument.status=1 then 'PENDING' else Case When Instrument.status = 2 then 'APPROVED' else Case when Instrument.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                                C.ID CurrencyID,IT.ID InstrumentTypeID, I.ID IssuerID, I.Name IssuerName, S.ID SectorID, S.Name SectorName,H.ID HoldingID,H.Name HoldingName, 
                                M.ID MarketID,RT.Description ReksadanaTypeID,DT.DEPType DepositoTypeID, 
                                MV.DescOne BondRatingDesc,MV1.DescOne InterestTypeDesc,MV2.DescOne InterestDaysTypeDesc,MV3.DescOne InterestPaymentTypeDesc,B.ID BankID,B.Name BankName,D.ID CounterpartID,D.Name CounterpartName,
                                isnull(SAP.ID,'') + ' - ' + isnull(SAP.Name,'') SAPCustomerID,isnull(E.ID,'') InstrumentCompanyTypeID,isnull(E.Name,'') InstrumentCompanyTypeName,
                                Instrument.* from Instrument  left join 
                                InstrumentType IT on Instrument.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 left join 
                                Issuer I on Instrument.IssuerPK = I.IssuerPK and I.status = 2 left join 
                                Currency C on Instrument.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                                SubSector S on Instrument.SectorPK = S.SubSectorPK and S.status = 2 left join 
                                Holding H on Instrument.HoldingPK = H.HoldingPK and H.status = 2 left join 
                                Market M on Instrument.MarketPK = M.MarketPK and M.status = 2 left join 
                                MasterValue MV on Instrument.BondRating = MV.Code and MV.ID = 'BondRating' and MV.status = 2 left join   
                                MasterValue MV1 on Instrument.interestType = MV1.Code and MV1.ID = 'InterestType' and MV1.status = 2 left join    
                                MasterValue MV2 on Instrument.InterestDaysType = MV2.Code and MV2.ID = 'InterestDaysType' and MV2.status = 2 left join   
                                MasterValue MV3 on Instrument.interestPaymentType= MV3.Code and MV3.ID = 'interestPaymentType' and MV3.status = 2 left join                          
                                ReksadanaType RT on Instrument.ReksadanaTypePK = RT.ReksadanaTypePK and RT.status = 2 left join  
                                DepositoType DT on Instrument.DepositoTypePK = DT.DepositoTypePK and DT.status = 2 left join
                                Bank B on Instrument.BankPK = B.BankPK and B.status = 2 left join
                                Counterpart D on Instrument.CounterpartPK = D.CounterpartPK and D.status = 2 left join
                                ZSAP_MS_Customer SAP on Instrument.SAPCustID = SAP.ID left join
                                InstrumentCompanyType E on Instrument.InstrumentCompanyTypePK = E.InstrumentCompanyTypePK and E.status in (1,2)
                                where Instrument.status = @status order by InstrumentPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"Select case when Instrument.status=1 then 'PENDING' else Case When Instrument.status = 2 then 'APPROVED' else Case when Instrument.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                                C.ID CurrencyID,IT.ID InstrumentTypeID, I.ID IssuerID, I.Name IssuerName, S.ID SectorID, S.Name SectorName,H.ID HoldingID,H.Name HoldingName, 
                                M.ID MarketID,RT.Description ReksadanaTypeID,DT.DEPType DepositoTypeID, 
                                MV.DescOne BondRatingDesc,MV1.DescOne InterestTypeDesc,MV2.DescOne InterestDaysTypeDesc,MV3.DescOne InterestPaymentTypeDesc,B.ID BankID,B.Name BankName,D.ID CounterpartID,D.Name CounterpartName,
                                isnull(SAP.ID,'') + ' - ' + isnull(SAP.Name,'') SAPCustomerID,isnull(E.ID,'') InstrumentCompanyTypeID,isnull(E.Name,'') InstrumentCompanyTypeName,
                                Instrument.* from Instrument  left join 
                                InstrumentType IT on Instrument.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 left join 
                                Issuer I on Instrument.IssuerPK = I.IssuerPK and I.status = 2 left join 
                                Currency C on Instrument.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                                SubSector S on Instrument.SectorPK = S.SubSectorPK and S.status = 2 left join 
                                Holding H on Instrument.HoldingPK = H.HoldingPK and H.status = 2 left join 
                                Market M on Instrument.MarketPK = M.MarketPK and M.status = 2 left join 
                                MasterValue MV on Instrument.BondRating = MV.Code and MV.ID = 'BondRating' and MV.status = 2 left join   
                                MasterValue MV1 on Instrument.interestType = MV1.Code and MV1.ID = 'InterestType' and MV1.status = 2 left join    
                                MasterValue MV2 on Instrument.InterestDaysType = MV2.Code and MV2.ID = 'InterestDaysType' and MV2.status = 2 left join   
                                MasterValue MV3 on Instrument.interestPaymentType= MV3.Code and MV3.ID = 'interestPaymentType' and MV3.status = 2 left join                          
                                ReksadanaType RT on Instrument.ReksadanaTypePK = RT.ReksadanaTypePK and RT.status = 2 left join  
                                DepositoType DT on Instrument.DepositoTypePK = DT.DepositoTypePK and DT.status = 2 left join
                                Bank B on Instrument.BankPK = B.BankPK and B.status = 2 left join
                                Counterpart D on Instrument.CounterpartPK = D.CounterpartPK and D.status = 2 left join
                                ZSAP_MS_Customer SAP on Instrument.SAPCustID = SAP.ID left join
                                InstrumentCompanyType E on Instrument.InstrumentCompanyTypePK = E.InstrumentCompanyTypePK and E.status in (1,2)
                                order by InstrumentPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Instrument.Add(setInstrument(dr));
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

        public int Instrument_Add(Instrument _instrument, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(InstrumentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Instrument";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(InstrumentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Instrument";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _instrument.ID);
                        cmd.Parameters.AddWithValue("@Name", _instrument.Name);
                        cmd.Parameters.AddWithValue("@Category", _instrument.Category);
                        cmd.Parameters.AddWithValue("@Affiliated", _instrument.Affiliated);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrument.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@ReksadanaTypePK", _instrument.ReksadanaTypePK);
                        if (_instrument.InstrumentTypePK == 5)
                        {
                            cmd.Parameters.AddWithValue("@DepositoTypePK", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DepositoTypePK", _instrument.DepositoTypePK);
                        }

                        cmd.Parameters.AddWithValue("@ISIN", _instrument.ISIN);
                        cmd.Parameters.AddWithValue("@BankPK", _instrument.BankPK);
                        cmd.Parameters.AddWithValue("@IssuerPK", _instrument.IssuerPK);
                        cmd.Parameters.AddWithValue("@SectorPK", _instrument.SectorPK);
                        cmd.Parameters.AddWithValue("@HoldingPK", _instrument.HoldingPK);
                        cmd.Parameters.AddWithValue("@MarketPK", _instrument.MarketPK);
                        cmd.Parameters.AddWithValue("@IssueDate", _instrument.IssueDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _instrument.MaturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _instrument.InterestPercent);
                        cmd.Parameters.AddWithValue("@InterestPaymentType", _instrument.InterestPaymentType);
                        cmd.Parameters.AddWithValue("@InterestType", _instrument.InterestType);
                        cmd.Parameters.AddWithValue("@LotInShare", _instrument.LotInShare);
                        cmd.Parameters.AddWithValue("@BitIsSuspend", _instrument.BitIsSuspend);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _instrument.CurrencyPK);
                        cmd.Parameters.AddWithValue("@RegulatorHaircut", _instrument.RegulatorHaircut);
                        cmd.Parameters.AddWithValue("@Liquidity", _instrument.Liquidity);
                        cmd.Parameters.AddWithValue("@NAWCHaircut", _instrument.NAWCHaircut);
                        cmd.Parameters.AddWithValue("@CompanyHaircut", _instrument.CompanyHaircut);
                        cmd.Parameters.AddWithValue("@BondRating", _instrument.BondRating);
                        cmd.Parameters.AddWithValue("@BitIsShortSell", _instrument.BitIsShortSell);
                        cmd.Parameters.AddWithValue("@BitIsMargin", _instrument.BitIsMargin);
                        cmd.Parameters.AddWithValue("@BitIsScriptless", _instrument.BitIsScriptless);
                        cmd.Parameters.AddWithValue("@TaxExpensePercent", _instrument.TaxExpensePercent);
                        cmd.Parameters.AddWithValue("@InterestDaysType", _instrument.InterestDaysType);
                        cmd.Parameters.AddWithValue("@FirstCouponDate", _instrument.FirstCouponDate);
                        cmd.Parameters.AddWithValue("@BloombergCode", _instrument.BloombergCode);
                        cmd.Parameters.AddWithValue("@BitIsForeign", _instrument.BitIsForeign);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _instrument.CounterpartPK);
                        cmd.Parameters.AddWithValue("@BankAccountNo", _instrument.BankAccountNo);
                        cmd.Parameters.AddWithValue("@SAPCustID", _instrument.SAPCustID);

                        cmd.Parameters.AddWithValue("@InstrumentCompanyTypePK", _instrument.InstrumentCompanyTypePK);
                        cmd.Parameters.AddWithValue("@AnotherRating", _instrument.AnotherRating);
                        cmd.Parameters.AddWithValue("@BloombergSecID", _instrument.BloombergSecID);
                        cmd.Parameters.AddWithValue("@ShortName", _instrument.ShortName);

                        cmd.Parameters.AddWithValue("@BloombergISIN", _instrument.BloombergISIN);
                        

                        cmd.Parameters.AddWithValue("@EntryUsersID", _instrument.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Instrument");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Instrument_Update(Instrument _instrument, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_instrument.InstrumentPK, _instrument.HistoryPK, "instrument");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update Instrument set status=2,Notes=@Notes,ID=@ID,Name=@Name,
                                Category=@Category,Affiliated=@Affiliated,InstrumentTypePK=@InstrumentTypePK,ReksadanaTypePK=@ReksadanaTypePK,DepositoTypePK=@DepositoTypePK,
                                ISIN=@ISIN,BankPK=@BankPK,IssuerPK=@IssuerPK,SectorPK=@SectorPK,HoldingPK=@HoldingPK,MarketPK=@MarketPK,IssueDate=@IssueDate,MaturityDate=@MaturityDate,
                                InterestPercent=@InterestPercent,InterestPaymentType=@InterestPaymentType,InterestType=@InterestType,LotInShare=@LotInShare,BitIsSuspend=@BitIsSuspend,
                                CurrencyPK=@CurrencyPK,RegulatorHaircut=@RegulatorHaircut,Liquidity=@Liquidity,NAWCHaircut=@NAWCHaircut,CompanyHaircut=@CompanyHaircut,BondRating=@BondRating,
                                BitIsShortSell=@BitIsShortSell,BitIsMargin=@BitIsMargin,BitIsScriptless=@BitIsScriptless,TaxExpensePercent=@TaxExpensePercent,InterestDaysType=@InterestDaysType,
                                BloombergCode=@BloombergCode,BitIsForeign=@BitIsForeign,FirstCouponDate=@FirstCouponDate,@CounterpartPK,@BankAccountNo,SAPCustID=@SAPCustID,InstrumentCompanyTypePK=@InstrumentCompanyTypePK,AnotherRating=@AnotherRating,BloombergSecID=@BloombergSecID,ShortName=@ShortName ,BloombergISIN=@BloombergISIN ,
                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                where InstrumentPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _instrument.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                            cmd.Parameters.AddWithValue("@ID", _instrument.ID);
                            cmd.Parameters.AddWithValue("@Notes", _instrument.Notes);
                            cmd.Parameters.AddWithValue("@Name", _instrument.Name);
                            cmd.Parameters.AddWithValue("@Category", _instrument.Category);
                            cmd.Parameters.AddWithValue("@Affiliated", _instrument.Affiliated);
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrument.InstrumentTypePK);
                            cmd.Parameters.AddWithValue("@ReksadanaTypePK", _instrument.ReksadanaTypePK);
                            cmd.Parameters.AddWithValue("@DepositoTypePK", _instrument.DepositoTypePK);
                            cmd.Parameters.AddWithValue("@ISIN", _instrument.ISIN);
                            cmd.Parameters.AddWithValue("@BankPK", _instrument.BankPK);
                            cmd.Parameters.AddWithValue("@IssuerPK", _instrument.IssuerPK);
                            cmd.Parameters.AddWithValue("@SectorPK", _instrument.SectorPK);
                            cmd.Parameters.AddWithValue("@HoldingPK", _instrument.HoldingPK);
                            cmd.Parameters.AddWithValue("@MarketPK", _instrument.MarketPK);
                            cmd.Parameters.AddWithValue("@IssueDate", _instrument.IssueDate);
                            cmd.Parameters.AddWithValue("@MaturityDate", _instrument.MaturityDate);
                            cmd.Parameters.AddWithValue("@InterestPercent", _instrument.InterestPercent);
                            cmd.Parameters.AddWithValue("@InterestPaymentType", _instrument.InterestPaymentType);
                            cmd.Parameters.AddWithValue("@InterestType", _instrument.InterestType);
                            cmd.Parameters.AddWithValue("@LotInShare", _instrument.LotInShare);
                            cmd.Parameters.AddWithValue("@BitIsSuspend", _instrument.BitIsSuspend);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _instrument.CurrencyPK);
                            cmd.Parameters.AddWithValue("@RegulatorHaircut", _instrument.RegulatorHaircut);
                            cmd.Parameters.AddWithValue("@Liquidity", _instrument.Liquidity);
                            cmd.Parameters.AddWithValue("@NAWCHaircut", _instrument.NAWCHaircut);
                            cmd.Parameters.AddWithValue("@CompanyHaircut", _instrument.CompanyHaircut);
                            cmd.Parameters.AddWithValue("@BondRating", _instrument.BondRating);
                            cmd.Parameters.AddWithValue("@BitIsShortSell", _instrument.BitIsShortSell);
                            cmd.Parameters.AddWithValue("@BitIsMargin", _instrument.BitIsMargin);
                            cmd.Parameters.AddWithValue("@BitIsScriptless", _instrument.BitIsScriptless);
                            cmd.Parameters.AddWithValue("@TaxExpensePercent", _instrument.TaxExpensePercent);
                            cmd.Parameters.AddWithValue("@InterestDaysType", _instrument.InterestDaysType);
                            cmd.Parameters.AddWithValue("@BloombergCode", _instrument.BloombergCode);
                            cmd.Parameters.AddWithValue("@BitIsForeign", _instrument.BitIsForeign);
                            cmd.Parameters.AddWithValue("@FirstCouponDate", _instrument.FirstCouponDate);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _instrument.CounterpartPK);
                            cmd.Parameters.AddWithValue("@BankAccountNo", _instrument.BankAccountNo);
                            cmd.Parameters.AddWithValue("@SAPCustID", _instrument.SAPCustID);

                            cmd.Parameters.AddWithValue("@InstrumentCompanyTypePK", _instrument.InstrumentCompanyTypePK);
                            cmd.Parameters.AddWithValue("@AnotherRating", _instrument.AnotherRating);
                            cmd.Parameters.AddWithValue("@BloombergSecID", _instrument.BloombergSecID);
                            cmd.Parameters.AddWithValue("@ShortName", _instrument.ShortName);

                            cmd.Parameters.AddWithValue("@BloombergISIN", _instrument.BloombergISIN);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _instrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Instrument set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where InstrumentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _instrument.EntryUsersID);
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
                                cmd.CommandText = @"Update Instrument set Notes=@Notes,ID=@ID,Name=@Name,
                                Category=@Category,Affiliated=@Affiliated,InstrumentTypePK=@InstrumentTypePK,ReksadanaTypePK=@ReksadanaTypePK,DepositoTypePK=@DepositoTypePK,
                                ISIN=@ISIN,BankPK=@BankPK,IssuerPK=@IssuerPK,SectorPK=@SectorPK,HoldingPK=@HoldingPK,MarketPK=@MarketPK,IssueDate=@IssueDate,MaturityDate=@MaturityDate,
                                InterestPercent=@InterestPercent,InterestPaymentType=@InterestPaymentType,InterestType=@InterestType,LotInShare=@LotInShare,BitIsSuspend=@BitIsSuspend,
                                CurrencyPK=@CurrencyPK,RegulatorHaircut=@RegulatorHaircut,Liquidity=@Liquidity,NAWCHaircut=@NAWCHaircut,CompanyHaircut=@CompanyHaircut,BondRating=@BondRating,
                                BitIsShortSell=@BitIsShortSell,BitIsMargin=@BitIsMargin,BitIsScriptless=@BitIsScriptless,TaxExpensePercent=@TaxExpensePercent,InterestDaysType=@InterestDaysType,
                                BloombergCode=@BloombergCode,BitIsForeign=@BitIsForeign,FirstCouponDate=@FirstCouponDate,CounterpartPK=@CounterpartPK,BankAccountNo=@BankAccountNo,SAPCustID=@SAPCustID,InstrumentCompanyTypePK=@InstrumentCompanyTypePK,AnotherRating=@AnotherRating,BloombergSecID=@BloombergSecID,ShortName=@ShortName,BloombergISIN=@BloombergISIN,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate 
                                where InstrumentPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _instrument.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ID", _instrument.ID);
                                cmd.Parameters.AddWithValue("@Notes", _instrument.Notes);
                                cmd.Parameters.AddWithValue("@Name", _instrument.Name);
                                cmd.Parameters.AddWithValue("@Category", _instrument.Category);
                                cmd.Parameters.AddWithValue("@Affiliated", _instrument.Affiliated);
                                cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrument.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@ReksadanaTypePK", _instrument.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@DepositoTypePK", _instrument.DepositoTypePK);
                                cmd.Parameters.AddWithValue("@ISIN", _instrument.ISIN);
                                cmd.Parameters.AddWithValue("@BankPK", _instrument.BankPK);
                                cmd.Parameters.AddWithValue("@IssuerPK", _instrument.IssuerPK);
                                cmd.Parameters.AddWithValue("@SectorPK", _instrument.SectorPK);
                                cmd.Parameters.AddWithValue("@HoldingPK", _instrument.HoldingPK);
                                cmd.Parameters.AddWithValue("@MarketPK", _instrument.MarketPK);
                                cmd.Parameters.AddWithValue("@IssueDate", _instrument.IssueDate);
                                cmd.Parameters.AddWithValue("@MaturityDate", _instrument.MaturityDate);
                                cmd.Parameters.AddWithValue("@InterestPercent", _instrument.InterestPercent);
                                cmd.Parameters.AddWithValue("@InterestPaymentType", _instrument.InterestPaymentType);
                                cmd.Parameters.AddWithValue("@InterestType", _instrument.InterestType);
                                cmd.Parameters.AddWithValue("@LotInShare", _instrument.LotInShare);
                                cmd.Parameters.AddWithValue("@BitIsSuspend", _instrument.BitIsSuspend);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _instrument.CurrencyPK);
                                cmd.Parameters.AddWithValue("@RegulatorHaircut", _instrument.RegulatorHaircut);
                                cmd.Parameters.AddWithValue("@Liquidity", _instrument.Liquidity);
                                cmd.Parameters.AddWithValue("@NAWCHaircut", _instrument.NAWCHaircut);
                                cmd.Parameters.AddWithValue("@CompanyHaircut", _instrument.CompanyHaircut);
                                cmd.Parameters.AddWithValue("@BondRating", _instrument.BondRating);
                                cmd.Parameters.AddWithValue("@BitIsShortSell", _instrument.BitIsShortSell);
                                cmd.Parameters.AddWithValue("@BitIsMargin", _instrument.BitIsMargin);
                                cmd.Parameters.AddWithValue("@BitIsScriptless", _instrument.BitIsScriptless);
                                cmd.Parameters.AddWithValue("@TaxExpensePercent", _instrument.TaxExpensePercent);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _instrument.InterestDaysType);
                                cmd.Parameters.AddWithValue("@BloombergCode", _instrument.BloombergCode);
                                cmd.Parameters.AddWithValue("@BitIsForeign", _instrument.BitIsForeign);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _instrument.CounterpartPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _instrument.BankAccountNo);
                                cmd.Parameters.AddWithValue("@FirstCouponDate", _instrument.FirstCouponDate);
                                cmd.Parameters.AddWithValue("@SAPCustID", _instrument.SAPCustID);

                                cmd.Parameters.AddWithValue("@InstrumentCompanyTypePK", _instrument.InstrumentCompanyTypePK);
                                cmd.Parameters.AddWithValue("@AnotherRating", _instrument.AnotherRating);
                                cmd.Parameters.AddWithValue("@BloombergSecID", _instrument.BloombergSecID);
                                cmd.Parameters.AddWithValue("@ShortName", _instrument.ShortName);

                                cmd.Parameters.AddWithValue("@BloombergISIN", _instrument.BloombergISIN);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _instrument.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_instrument.InstrumentPK, "Instrument");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Instrument where InstrumentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _instrument.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _instrument.ID);
                                cmd.Parameters.AddWithValue("@Name", _instrument.Name);
                                cmd.Parameters.AddWithValue("@Category", _instrument.Category);
                                cmd.Parameters.AddWithValue("@Affiliated", _instrument.Affiliated);
                                cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrument.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@ReksadanaTypePK", _instrument.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@DepositoTypePK", _instrument.DepositoTypePK);
                                cmd.Parameters.AddWithValue("@ISIN", _instrument.ISIN);
                                cmd.Parameters.AddWithValue("@BankPK", _instrument.BankPK);
                                cmd.Parameters.AddWithValue("@IssuerPK", _instrument.IssuerPK);
                                cmd.Parameters.AddWithValue("@SectorPK", _instrument.SectorPK);
                                cmd.Parameters.AddWithValue("@HoldingPK", _instrument.HoldingPK);
                                cmd.Parameters.AddWithValue("@MarketPK", _instrument.MarketPK);
                                cmd.Parameters.AddWithValue("@IssueDate", _instrument.IssueDate);
                                cmd.Parameters.AddWithValue("@MaturityDate", _instrument.MaturityDate);
                                cmd.Parameters.AddWithValue("@InterestPercent", _instrument.InterestPercent);
                                cmd.Parameters.AddWithValue("@InterestPaymentType", _instrument.InterestPaymentType);
                                cmd.Parameters.AddWithValue("@InterestType", _instrument.InterestType);
                                cmd.Parameters.AddWithValue("@LotInShare", _instrument.LotInShare);
                                cmd.Parameters.AddWithValue("@BitIsSuspend", _instrument.BitIsSuspend);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _instrument.CurrencyPK);
                                cmd.Parameters.AddWithValue("@RegulatorHaircut", _instrument.RegulatorHaircut);
                                cmd.Parameters.AddWithValue("@Liquidity", _instrument.Liquidity);
                                cmd.Parameters.AddWithValue("@NAWCHaircut", _instrument.NAWCHaircut);
                                cmd.Parameters.AddWithValue("@CompanyHaircut", _instrument.CompanyHaircut);
                                cmd.Parameters.AddWithValue("@BondRating", _instrument.BondRating);
                                cmd.Parameters.AddWithValue("@BitIsShortSell", _instrument.BitIsShortSell);
                                cmd.Parameters.AddWithValue("@BitIsMargin", _instrument.BitIsMargin);
                                cmd.Parameters.AddWithValue("@BitIsScriptless", _instrument.BitIsScriptless);
                                cmd.Parameters.AddWithValue("@TaxExpensePercent", _instrument.TaxExpensePercent);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _instrument.InterestDaysType);
                                cmd.Parameters.AddWithValue("@BloombergCode", _instrument.BloombergCode);
                                cmd.Parameters.AddWithValue("@BitIsForeign", _instrument.BitIsForeign);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _instrument.CounterpartPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _instrument.BankAccountNo);
                                cmd.Parameters.AddWithValue("@FirstCouponDate", _instrument.FirstCouponDate);
                                cmd.Parameters.AddWithValue("@SAPCustID", _instrument.SAPCustID);

                                cmd.Parameters.AddWithValue("@InstrumentCompanyTypePK", _instrument.InstrumentCompanyTypePK);
                                cmd.Parameters.AddWithValue("@AnotherRating", _instrument.AnotherRating);
                                cmd.Parameters.AddWithValue("@BloombergSecID", _instrument.BloombergSecID);
                                cmd.Parameters.AddWithValue("@ShortName", _instrument.ShortName);

                                cmd.Parameters.AddWithValue("@BloombergISIN", _instrument.BloombergISIN);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _instrument.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Instrument set status= 4,Notes=@Notes, " +
                                     "LastUpdate=@lastupdate  where InstrumentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _instrument.Notes);
                                cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _instrument.HistoryPK);
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

        public void Instrument_Approved(Instrument _instrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Instrument set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where InstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrument.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Instrument set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where InstrumentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrument.ApprovedUsersID);
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

        public void Instrument_Reject(Instrument _instrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Instrument set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where InstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrument.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Instrument set status= 2,LastUpdate=@LastUpdate where InstrumentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
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

        public void Instrument_Void(Instrument _instrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Instrument set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where InstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrument.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<InstrumentCombo> Instrument_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  A.InstrumentPK,A.ID + ' - ' + A.Name ID, A.Name,B.ID Type  FROM [Instrument] A Left join InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK and B.Status = 2  where A.status = 2 order by A.ID,A.Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.Type = Convert.ToString(dr["Type"]);
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


        public List<InstrumentCombo> InstrumentBond_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  A.InstrumentPK,A.ID + ' - ' + A.Name ID, A.Name,B.ID Type  FROM [Instrument] A Left join InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK and B.Status = 2  where A.status = 2 and B.Type in(2,5,14) order by A.ID,A.Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.Type = Convert.ToString(dr["Type"]);
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


        public List<InstrumentCombo> Instrument_ComboByInstrumentTypePK(int _instrumentTypePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_instrumentTypePK == 2)
                        {
                            cmd.CommandText =
                            @"select InstrumentPK, ID + ' - ' + Name ID, Name 
                            from Instrument where InstrumentTypePK in (2,3,9,15) and status = 2
                            order by ID, Name";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select InstrumentPK, ID + ' - ' + Name ID, Name 
                            from Instrument where InstrumentTypePK = @InstrumentTypePK  and status = 2
                            order by ID, Name";
                        }

                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrumentTypePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
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

        public List<InstrumentCombo> Instrument_ComboByMarketPK(int _marketpk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            "select InstrumentPK, a.ID + ' - ' + a.Name ID, a.Name " +
                            "from Instrument a " +
                                "left join Market b on b.Status = 2 " +
                            "where a.Status = 2 and b.MarketPK = @MarketPK " +
                            "order by a.ID, a.Name";
                        cmd.Parameters.AddWithValue("@MarketPK", _marketpk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
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

        public List<InstrumentForInvestment> Instrument_LookupByInstrumentTypePK(int _instrumentTypePK)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_instrumentTypePK == 1)
                        {

                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent FROM  instrument I left join InstrumentType IT " +
                            "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 " +
                            "where I.status = 2 and IT.Type = 1 ";
                        }
                        else if(_instrumentTypePK == 2)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent FROM  instrument I left join InstrumentType IT " +
                           "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 " +
                           "where I.status = 2 and IT.Type in (2,5) ";
                        }
                        else if (_instrumentTypePK == 3)
                        {
                            cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name + ' - ' + CONVERT(VARCHAR(10), I.MaturityDate, 103) ID, I.Name, I.MaturityDate, I.InterestPercent FROM  instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 
                            where I.status = 2 and IT.Type in (3)  ";
                        }

                        else if (_instrumentTypePK == 4)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent FROM  instrument I left join InstrumentType IT " +
                           "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 " +
                           "where I.status = 2 and IT.Type in (4) ";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
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

        public List<InstrumentForInvestment> Instrument_LookupByInstrumentTypePKandTrxType(int _instrumentTypePK, int _trxType,int _fundPK, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_trxType == 0 || _trxType == 1 || _trxType == 4)
                        {
                            string _instrumentType = "";
                            if (_instrumentTypePK == 1)
                            {
                                _instrumentType = "1";
                            }
                            else if (_instrumentTypePK == 2)
                            {
                                _instrumentType = "2,5";
                            }
                            else if (_instrumentTypePK == 3)
                            {
                                _instrumentType = "3";
                            }
                            else
                            {
                                _instrumentType = "1,2,3,4,5";
                            }

                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent FROM  instrument I left join InstrumentType IT " +
                            "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 " +
                            "where I.status = 2 and IT.Type in (" + _instrumentType + ") order by I.ID ";
                           

                        }


                        else if (_trxType == 2)
                        {

                            string _instrumentType = "";
                            if (_instrumentTypePK == 1)
                            {
                                _instrumentType = "1";
                            }
                            else if (_instrumentTypePK == 2)
                            {
                                _instrumentType = "2,5";
                            }
                            else if (_instrumentTypePK == 3)
                            {
                                _instrumentType = "3";
                            }
                            else
                            {
                                _instrumentType = "1,2,3,4,5";
                            }

                            cmd.CommandText = " select FP.Balance,FP.AvgPrice,FP.AcqDate,FP.MaturityDate,isnull(FP.InterestPercent,0) InterestPercent,I.ID + ' - ' + I.Name ID,* from FundPosition FP " +
                             " Left Join Instrument I on FP.InstrumentPK  = I.InstrumentPK and I.status = 2     " +
                             " left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2    " +
                             " where IT.Type in (" + _instrumentType + ") and FP.FundPK = @FundPK  and FP.DATE  = (select MAX(Date) MaxDate from FundPosition where FundPK = @FundPK and Date <= @Date ) order by I.ID ";

                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }
                            //ROLLOVER
                        else if (_trxType == 3)
                        {
                            if (_instrumentTypePK == 3)
                            {
                                cmd.CommandText = " select FP.Balance,FP.AvgPrice,FP.AcqDate,FP.MaturityDate,isnull(FP.InterestPercent,0) InterestPercent,I.ID + ' - ' + I.Name ID,* from FundPosition FP " +
                                 " Left Join Instrument I on FP.InstrumentPK  = I.InstrumentPK and I.status = 2     " +
                                 " left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2    " +
                                 " where IT.Type in (3) and FP.FundPK = @FundPK   " +
                                 " and FP.MaturityDate = @Date and FP.Date = (select MAX(Date) MaxDate from FundPosition where FundPK = @FundPK and Date <= @Date ) order by I.ID ";
                            }
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }


                       

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if (_trxType == 2 || _trxType == 3)
                                    {
                                        InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                        M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                        M_Instrument.ID = Convert.ToString(dr["ID"]);
                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        M_Instrument.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                        M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                        L_Instrument.Add(M_Instrument);
                                    }
                                    else
                                    {
                                        InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                        M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                        M_Instrument.ID = Convert.ToString(dr["ID"]);
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        L_Instrument.Add(M_Instrument);

                                      
                                    }
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


        public InstrumentForInvestment Instrument_GetIntormationByInstrumentPK(int _instrumentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK " +
                            "";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    return M_Instrument;
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

        //INSERT KE FORM INVESTMENT
        public InstrumentForInvestment Instrument_GetInformationByInstrumentPKByTrxType(Investment _investment)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _instrumentType = "";
                        if (_investment.InstrumentTypePK == 1)
                        {
                            _instrumentType = "and A.instrumentTypePK in (1)";
                        }
                        else if (_investment.InstrumentTypePK == 2)
                        {
                            _instrumentType = "and A.instrumentTypePK in (2,3)";
                        }
                        else if (_investment.InstrumentTypePK == 5)
                        {
                            _instrumentType = "and A.instrumentTypePK in (5)";
                        }
                        else
                        {
                            _instrumentType = "and A.instrumentTypePK in (1,2,3,4,5)";
                        }

                        if (_investment.TrxType == 1)
                        {
                            cmd.CommandText = "Select MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";

                        }
                        else if (_investment.TrxType == 2)
                        {

                            //cmd.CommandText = " select FP.Balance,FP.AvgPrice,FP.AcqDate,FP.MaturityDate,isnull(FP.InterestPercent,0) InterestPercent,I.ID + ' - ' + I.Name ID from FundPosition FP " +
                            //     " Left Join Instrument I on FP.InstrumentPK  = I.InstrumentPK and I.status = 2     " +
                            //     " left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2    " +
                            //     " where FP.InstrumentPK =@InstrumentPK and FP.FundPK = @FundPK  and  FP.DATE  = (select MAX(Date) MaxDate from FundPosition where FundPK = @FundPK and Date <= @Date ) order by I.ID ";


                            cmd.CommandText = @"select FP.Balance,FP.AvgPrice,FP.AcqDate,FP.MaturityDate,isnull(FP.InterestPercent,0) InterestPercent,I.ID + ' - ' + I.Name ID from FundPosition FP  
                            Left Join Instrument I on FP.InstrumentPK  = I.InstrumentPK and I.status = 2      
                            left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2   
                            where FP.InstrumentPK =@InstrumentPK and FP.FundPK = @FundPK  and  FP.DATE  = (select MAX(Date) MaxDate from FundPosition where FundPK = @FundPK and Date <= @Date )   
                            union all
                            Select sum(DoneVolume) Balance,DonePrice,ValueDate,B.MaturityDate,B.InterestPercent,B.ID + ' - ' + B.Name ID from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status  = 2
                            where ValueDate = @date and StatusInvestment <> 3 and StatusDealing <> 3 " + _instrumentType + @" and TrxType = 1 and FundPK = @fundpk and A.InstrumentPK  = @InstrumentPK
                            group by A.InstrumentPK,TrxType,DonePrice,ValueDate,B.MaturityDate,B.InterestPercent,B.ID,B.Name ";

                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _investment.ValueDate);
                        }
                        else if (_investment.TrxType == 3)
                        {

                            cmd.CommandText =
                                " Declare @AcqDate DateTime " + 
                                " Declare @MaturityDate DateTime " +
                                " Declare @number int  " +
                                //" select @AcqDate = ValueDate From Investment where InstrumentTypePK = 3 and InvestmentPK " +
                                //" select @number =  InterestDue from Instrument where InstrumentPK =@InstrumentPK " +
                                " select @MaturityDate = DATEADD( month, @number, MaturityDate ), @AcqDate = AcqDate from FundPosition where InstrumentPK =@InstrumentPK and FundPK = @FundPK  " +
                                " select @AcqDate AcqDate,@MaturityDate MaturityDate,FP.InterestPercent InteresPercent,I.Name InstrumentName,I.ID + ' - ' + I.Name ID,* from FundPosition FP " +
                                " Left Join Instrument I on FP.InstrumentPK  = I.InstrumentPK and I.status = 2  " +
                                " where FP.InstrumentPK = @InstrumentPK and FP.FundPK = @FundPK  " +
                                " and FP.MaturityDate = @Date  order by I.ID";

                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _investment.ValueDate);

                        }
                        else
                        {

                            cmd.CommandText = "Select MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";

                        }
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if (_investment.TrxType == 0 || _investment.TrxType == 1 || _investment.TrxType == 4)
                                    {
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        return M_Instrument;
                                    }
                                    else
                                    {
                                        M_Instrument.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        //M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                
                                        return M_Instrument;
                                    }
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


        //INSERT KE FORM INVESTMENT
        public InstrumentForInvestment Instrument_GetInformationByInstrumentPKByTrxTypeForTrxPortfolio(Investment _investment)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //string _instrumentType = "";
                        //if (_investment.InstrumentTypePK == 1)
                        //{
                        //    _instrumentType = "and A.instrumentTypePK in (1)";
                        //}
                        //else if (_investment.InstrumentTypePK == 2)
                        //{
                        //    _instrumentType = "and A.instrumentTypePK in (2,3)";
                        //}
                        //else if (_investment.InstrumentTypePK == 5)
                        //{
                        //    _instrumentType = "and A.instrumentTypePK in (5)";
                        //}
                        //else
                        //{
                        //    _instrumentType = "and A.instrumentTypePK in (1,2,3,4,5)";
                        //}

                        if (_investment.TrxType == 1)
                        {
                            cmd.CommandText = "Select BankPK,MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";

                        }
                            //Sementara bukan tarikan dr journal detail
                        else if (_investment.TrxType == 2)
                        {

                                cmd.CommandText = @"Select BankPK,MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";
                            
                           
                        }
                        else if (_investment.TrxType == 3)
                        {

                                cmd.CommandText = @"Select BankPK,MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";

                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _investment.ValueDate);

                        }
                        else
                        {

                            cmd.CommandText = "Select MaturityDate,InterestPercent From Instrument  where status = 2 and InstrumentPK = @InstrumentPK order by ID ";

                        }
                        cmd.Parameters.AddWithValue("@InstrumentPK", _investment.InstrumentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if (_investment.TrxType == 0 || _investment.TrxType == 1 || _investment.TrxType == 4)
                                    {
                                        M_Instrument.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        return M_Instrument;
                                    }
                                    else
                                    {
                                        M_Instrument.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
                                        M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                        M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                        return M_Instrument;
                                    }
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

        public InstrumentForInvestment Instrument_SelectByInstrumentID(int _instrumentID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InstrumentPK,ID,Name,MaturityDate,InterestPercent, FROM [Instrument]  where InstrumentPK=@InstrumentPK and status = 2";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.LastCouponDate = Convert.ToString(dr["LastCouponDate"]);
                                    return M_Instrument;
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

        public string Get_NextCouponDate(DateTime _lastCouponDate, int _instrumentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " Declare @number int " +
                        //" select @number =  InterestDue from Instrument where InstrumentPK =@InstrumentPK " +
                        " select DATEADD( month, @number, @LastCouponDate ) as NextCouponDate ";

                        cmd.Parameters.AddWithValue("@LastCouponDate", _lastCouponDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToString(dr["NextCouponDate"]);

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

        public double Get_TenorForTimeDeposit(InstrumentForInvestment _investment)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                        if (_investment.TrxType == 1 || _investment.TrxType == 3)
                        {
                            if (Convert.ToDateTime(_investment.MaturityDate) > Convert.ToDateTime(_investment.SettledDate))
                            {
                                return (Convert.ToDateTime(_investment.MaturityDate) - Convert.ToDateTime(_investment.SettledDate)).TotalDays;
                            }

                            else
                            {
                                return 0;
                            }
                                  
                        }
                        else if (_investment.TrxType == 2 )
                        {
                            if (Convert.ToDateTime(_investment.SettledDate) > Convert.ToDateTime(_investment.AcqDate))
                            {
                                return (Convert.ToDateTime(_investment.SettledDate) - Convert.ToDateTime(_investment.AcqDate)).TotalDays;
                            }
                            
                            else
                            {
                                return 0;
                            }
                        }


                        return 0;

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Instrument_AddFromOMSTimeDeposit(DateTime _maturityDate, int _bankPK, decimal _interestPercent, string _category, int _currencyPK, string _userID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        --    if exists(
	                    --Select InterestPercent,* from Instrument 
	                    --where MaturityDate = @MaturityDate and InterestPercent = @InterestPercent and InstrumentTypePK = 5 and Category = @Category and CurrencyPK = @CurrencyPK
                        --and BankPK = @BankPK
	                    --)
	                    --BEGIN
		                --    select InstrumentPK From Instrument
		                --    where MaturityDate = @MaturityDate and InterestPercent = @InterestPercent and InstrumentTypePK = 5 and Category = @Category and CurrencyPK = @CurrencyPK
                        --    and BankPK = @BankPK
	                    --END
	                    --ELSE
	                    --BEGIN
		                   Declare @BankID nvarchar(200)
                            select @BankID = ID From Bank Where BankPK = @BankPK and Status = 2
                            Declare @MaxInstrumentPK int
                            select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
                            set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)
                            Insert Into Instrument(InstrumentPK,HistoryPK,Status,ID,Name,InstrumentTypePK,DepositoTypePK,lotinshare,BankPK,InterestPercent,MaturityDate,CurrencyPK,Category,TaxExpensePercent,IssuerPK,[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)
                            Select @MaxInstrumentPK,1,2,@BankID, 'TDP ' +@BankID,@InstrumentTypePK,1,1,@BankPK,@InterestPercent,@MaturityDate,@CurrencyPK,@Category,20,IssuerPK,@EntryUsersID,@Now,@EntryUsersID,@Now,@Now
                            from Bank 
                            where BankPK = @BankPK and status in (1,2)
                            Select @MaxInstrumentPK InstrumentPK
	                    --END
                        ";
                        cmd.Parameters.AddWithValue("@MaturityDate", _maturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _interestPercent);
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Now", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _currencyPK);
                        cmd.Parameters.AddWithValue("@Category", _category);

                        if (_category == "Negotiabale Certificate Deposit")
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 10);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 5);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["InstrumentPK"]);
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



        public int Get_InstrumentTypeByInstrumentPK(int _instrumentPK)
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
                            Select InstrumentTypePK from Instrument where InstrumentPK=@InstrumentPK and status in (1,2) ";

                        cmd.Parameters.AddWithValue("@instrumentPK", _instrumentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["InstrumentTypePK"]);

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

        public List<InstrumentCombo> Instrument_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InstrumentPK,ID + ' - ' + Name as ID, Name FROM [Instrument]  where status = 2 union all select 0,'All', '' order by InstrumentPK ";
                        //"SELECT  A.InstrumentPK,A.ID + ' - ' + A.Name ID, A.Name,B.ID Type  FROM [Instrument] A Left join InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK and B.Status = 2  where A.status = 2 order by A.ID,A.Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    //M_Instrument.Type = Convert.ToString(dr["Type"]);
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


        public InstrumentGetCouponDate Get_LastCouponDateandNextCouponDate(DateTime _valueDate, int _instrumentPK)
        {

            try
            {
                InstrumentGetCouponDate M = new InstrumentGetCouponDate();
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @" --DECLARE @LastCouponDate datetime      

--                                DECLARE @InstrumentPaymentType int

--                                select @InstrumentPaymentType = case when (InterestPaymentType in (16,17,18)) then 6 

--                                else  case when (InterestPaymentType in (7,8,9)) then 1  else  3 end end From instrument    

--                                WHERE InstrumentPK = @InstrumentPK And Status = 2

--                                SET @LastCouponDate=0     

--                                IF (@InstrumentPaymentType = 6)  
--BEGIN           
-- SELECT @LastCouponDate = 
 
-- case when eomonth(FirstCouponDate) = FirstCouponDate then

--	 case when @date between dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,4 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then eomonth(dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) 

--	 else case when @date between dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then eomonth(dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))

--	 else case when @date between dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then eomonth(dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))
   
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then eomonth(dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
 
--	 else case when @date between dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,0,dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then eomonth(dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))  
 
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate))   
--	 then eomonth(dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate))   
 
--	 else '12/31/2099'   
--	 end end end end end end 

-- else
--	case when @date between dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,4 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) 

--	 else case when @date between dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,3 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))

--	 else case when @date between dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,2 * (@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
--	 then dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))
   
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)  
 
--	 else case when @date between dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,0,dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
 
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate))   
--	 then dateadd(year,(datediff(year,FirstCouponDate,@date)-1),FirstCouponDate)   
 
--	 else '12/31/2099'   
--	 end end end end end end 
-- end
-- from instrument       
-- WHERE InstrumentPK = @InstrumentPK And Status = 2  
--END  
--ELSE IF (@InstrumentPaymentType = 1)  
--BEGIN  
-- SELECT @LastCouponDate =    
-- case when eomonth(FirstCouponDate) = FirstCouponDate then
--	 case when @date between convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101) and dateadd(month,1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101))  
--	 then eomonth(convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101))  
--	 else case when @date <  convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)  
--	 then eomonth(dateadd(month,-1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)))  
--	 else case when @date > convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)   
--	 then eomonth(dateadd(month,1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)))  
--	 else '12/31/2097'   
--	 end end end  
-- else
--	case when @date between convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101) and dateadd(month,1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101))  
--	 then convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)  
--	 else case when @date <  convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)  
--	 then dateadd(month,-1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101))  
--	 else case when @date > convert(datetime,convert(nvarchar(2),datepart(month,@date)) + '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101)   
--	 then dateadd(month,1,convert(datetime,convert(nvarchar(2),datepart(month,@date))+ '/' + convert(nvarchar(2),datepart(day,FirstCouponDate)) + '/' + convert(nvarchar(4),datepart(year,@date)),101))  
--	 else '12/31/2097'   
--	 end end end  
-- end
-- from instrument       
-- WHERE InstrumentPK = @InstrumentPK And Status = 2  
--END  
--ELSE   
--BEGIN  
-- SELECT @LastCouponDate =  
-- case when eomonth(FirstCouponDate) = FirstCouponDate then
--	 case when @date between dateadd(month,3*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,3*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
--	 then eomonth(dateadd(month,3*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
 
--	 else case when @date between dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,2*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
--	 then eomonth(dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
 
--	 else case when @date between dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))  
--	 then eomonth(dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))  
 
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then eomonth(dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  

--	 else case when @date between dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,0,dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then eomonth(dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
  
--	 else case when @date between dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then eomonth(dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))   
   
--	 else case when @date between  dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and  dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then eomonth(dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))     

--	 else case when @date between dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))) and dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))
--	 then eomonth(dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))))
 
--	 else case when @date between dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then eomonth(dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))
 
--	 else '12/31/2098'   
   
--	 end end end end end end end end end
-- else
--	case when @date between dateadd(month,3*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,3*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
--	 then dateadd(month,3*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))   
 
--	 else case when @date between dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,2*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
--	 then dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))   
 
--	 else case when @date between dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))  
--	 then dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
 
--	 else case when @date between dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate) and dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)  

--	 else case when @date between dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)) and dateadd(month,0,dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))  
--	 then dateadd(month,-(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))   
  
--	 else case when @date between dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then dateadd(year,-1,dateadd(month,2*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))   
   
--	 else case when @date between  dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and  dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))     

--	 else case when @date between dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))) and dateadd(year,-1,dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))
--	 then dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))
 
--	 else case when @date between dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))) and dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(month,(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate))))  
--	 then dateadd(year,-1,dateadd(month,-1*(@InstrumentPaymentType),dateadd(year,(datediff(year,FirstCouponDate,@date)),FirstCouponDate)))
 
--	 else '12/31/2098'   
   
--	 end end end end end end end end end
-- end

-- from instrument       
-- WHERE InstrumentPK = @InstrumentPK And Status = 2  
--END  


--                                    declare @NextCouponDate datetime
--                                    set @NextCouponDate = case when eomonth(@LastCouponDate) = @LastCouponDate then eomonth(dateadd(month,(@InstrumentPaymentType),@LastCouponDate)) else dateadd(month,(@InstrumentPaymentType),@LastCouponDate) end

--                                    --select @LastCouponDate,@NextCouponDate
--                                    IF (@LastCouponDate = @date or @NextCouponDate = @Date)
--                                    BEGIN
--	                                    set @LastCouponDate = @date
--                                    END
--                                    ELSE
--                                    BEGIN
--	                                    set @LastCouponDate = @LastCouponDate   
--                                    END

--                            SELECT @LastCouponDate LastCouponDate, @NextCouponDate NextCouponDate


							
select dbo.FgetLastCouponDate(@Date,@instrumentpk) LastCouponDate,dbo.Fgetnextcoupondate(@Date,@instrumentpk) NextCouponDate
                            ";

                        cmd.Parameters.AddWithValue("@date", _valueDate);
                        cmd.Parameters.AddWithValue("@instrumentpk", _instrumentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    M.LastCouponDate = Convert.ToString(dr["LastCouponDate"]);
                                    M.NextCouponDate = Convert.ToString(dr["NextCouponDate"]);
                                    return M;
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

        public string ImportInstrument(string _fileName, string _userID)
        {

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table InstrumentTemp";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.InstrumentTemp";
                bulkCopy.WriteToServer(CreateDataTableFromFileInstrument(_fileName));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                    Declare @InstrumentPK int


                    Select @InstrumentPK = max(instrumentPk) + 1 from instrument where status = 2
                    set @instrumentPK = isnull(@InstrumentPK,0)


                    INSERT INTO [dbo].[Instrument]
                               ([InstrumentPK]
                               ,[HistoryPK]
                               ,[Status]
                               ,[Notes]
                               ,[ID]
                               ,[Name]
                               ,[Category]
                               ,[Affiliated]
                               ,[InstrumentTypePK]
                               ,[ReksadanaTypePK]
                               ,[DepositoTypePK]
                               ,[ISIN]
                               ,[BankPK]
                               ,[IssuerPK]
                               ,[SectorPK]
                               ,[HoldingPK]
                               ,[MarketPK]
                               ,[IssueDate]
                               ,[MaturityDate]
                               ,[InterestPercent]
                               ,[LotInShare]
                               ,[BitIsSuspend]
                               ,[CurrencyPK]
                               ,[RegulatorHaircut]
                               ,[Liquidity]
                               ,[NAWCHairCut]
                               ,[CompanyHaircut]
                               ,[BondRating]
                               ,[BitIsShortSell]
                               ,[BitIsMargin]
                               ,[BitIsScriptless]
                               ,[TaxExpensePercent]
                               ,[InterestDaysType]
                               ,[InterestType]
                               ,[InterestPaymentType]
                               ,[CounterpartPK]
                               ,[EntryUsersID]
                               ,[EntryTime]
                               ,[ApprovedUsersID]
                               ,[ApprovedTime]
                               ,[LastUpdate]
                             
                    )

                    Select @InstrumentPK +  ROW_NUMBER() OVER(Order By A.Code ASC) ,1,2,'',A.code,A.Name,'',0,Case when A.TypeDesc = 'EQUITY' then 1 else C.InstrumentTypePK end,0,0,
                    A.Isin,0,isnull(D.IssuerPK,0),isnull(B.SubSectorPK,0),0,1,convert(date,A.ListingDate),convert(date,A.MaturityDate),A.Interest,
                    Case when A.TypeDesc = 'EQUITY' then 100 else 1 end,0,E.CurrencyPK,0,100,0,0,'',0,0,0,
                    20,Case when C.InstrumentTypePK = 2 then 2 else case when C.InstrumentTypePK = 3 then 3 else 0  end end,isnull(G.Code,0),isnull(F.Code,0 ),0,@userID,@Datetime,@userID,@Datetime,@Datetime
                

                    from InstrumentTemp  A
                    Left join SubSector B on A.Sector = B.ID and B.status = 2
                    left join InstrumentType C on A.TypeDesc = C.Name and C.status = 2
                    left join Issuer D on A.Issuer = D.Name and D.status = 2
                    left join Currency E on A.Currency = E.ID and E.status = 2
                    Left join MasterValue F on A.InterestFreq = F.DescOne and F.status = 2 and F.ID = 'InterestPaymentType'
                    Left join MasterValue G on A.InterestType = G.DescOne and G.status = 2 and G.ID = 'InterestType'
                    where A.Code not in
                    (
	                    Select ID From Instrument where status = 2
                    )
                        ";
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@Datetime", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import Instrument Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromFileInstrument(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Code";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Name";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TypeDesc";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Isin";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Issuer";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ListingDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Currency";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaturityDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "Interest";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestFreq";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Sector";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["Code"] = s[1];
                dr["Name"] = s[2];
                dr["TypeDesc"] = s[3];
                dr["Isin"] = s[4];
                dr["Issuer"] = s[5];
                dr["ListingDate"] = s[8];
                dr["Currency"] = s[9];
                dr["MaturityDate"] = s[12];
                dr["Interest"] = s[15] == "" ? 0 : Convert.ToDecimal(s[15]);
                dr["InterestType"] = s[16];
                dr["InterestFreq"] = s[17];
                dr["Sector"] = s[26];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public List<InstrumentForInvestment> Instrument_LookupForOMSEquityByMarketPK(int _trxType, int _fundPK, DateTime _date, int _marketPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK,I.InstrumentTypePK FROM  instrument I left join InstrumentType IT 
                               on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2
                               where I.status = 2 and IT.Type in (1) and I.MarketPK = @MarketPK order by I.ID ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
     
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK

set @MaxDateEndDayFP =  isnull(@MaxDateEndDayFP,'1900-01-01')

select A.InstrumentPK,A.InstrumentID,sum(A.Balance)  Balance,A.CurrencyID,A.InstrumentTypePK from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,B.InstrumentTypePK from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.MarketPK = @MarketPK                 

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType, B.InstrumentTypePK
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType, B.InstrumentTypePK
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
) A 

group by A.InstrumentPK,A.InstrumentID,A.CurrencyID,A.InstrumentTypePK

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
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
)
and status = 2

set @MaxDateEndDayFP =  isnull(@MaxDateEndDayFP,'1900-01-01')

select A.InstrumentPK,A.InstrumentID,sum(A.Balance)  Balance,A.CurrencyID,A.InstrumentTypePK from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,B.InstrumentTypePK from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.MarketPK = @MarketPK                 

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
) A 

group by A.InstrumentPK,A.InstrumentID,A.CurrencyID,A.InstrumentTypePK

                                ";
                            }
                            
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
                                    M_Instrument.ID = Convert.ToString(dr["InstrumentID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public List<InstrumentForInvestment> Instrument_LookupForOMSBondByMarketPK(int _trxType, int _fundPK, DateTime _date, int _marketPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK,isnull(12/B.Priority,0) PaymentType 
                            FROM  instrument I 
                            left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                            left join MasterValue B on I.InterestPaymentType = B.Code and B.ID = 'InterestPaymentType' and B.status = 2
                            where I.status = 2 and IT.InstrumentTypePK in (2,3,8,9,13,15) and I.MarketPK = @MarketPK and MaturityDate >= @Date order by I.ID ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                            Declare @TrailsPK int

                            select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date  and FundPk = @FundPK
                            )
                            and status = 2 and FundPk = @FundPK                                

                            select A.TrxBuy,A.TrxBuyType,A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,isnull(12/C.Priority,0) PaymentType from (

                            select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,'IDR' CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,[Identity] TrxBuy,'FP' TrxBuyType,A.AvgPrice,B.InterestPaymentType from FundPosition A    
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                            Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                            where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15) and A.MarketPK = @MarketPK 

                            union all
	
                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR',A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType
                            from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
                            and FundPK = @FundPK and TrxType  = 1  and A.MarketPK = @MarketPK 
                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType

                            ) A 
                            left join
                            (
                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType,A.AcqDate
                            from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            where ValueDate = @Date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
                            and FundPK = @FundPK and TrxType  = 2  and A.MarketPK = @MarketPK
                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType,A.AcqDate
                            ) B on  A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate
                            left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status = 2

                            Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,C.Priority
                            having A.Balance + sum(isnull(B.MovBalance,0)) <> 0                             

                            ";
                            }
                            else
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                            Declare @TrailsPK int

                            select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
                            )
                            and status = 2                                

                            select A.TrxBuy,A.TrxBuyType,A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,isnull(12/C.Priority,0) PaymentType from (

                            select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,'IDR' CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,[Identity] TrxBuy,'FP' TrxBuyType,A.AvgPrice,B.InterestPaymentType from FundPosition A    
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                            Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                            where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15) and A.MarketPK = @MarketPK 

                            union all
	
                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR',A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType
                            from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P')
                            and FundPK = @FundPK and TrxType  = 1  and A.MarketPK = @MarketPK 
                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType

                            ) A 
                            left join
                            (
                            Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
                            case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType,A.AcqDate
                            from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                            Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            where ValueDate = @Date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15)
                            and FundPK = @FundPK and TrxType  = 2  and A.MarketPK = @MarketPK
                            group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType,A.AcqDate
                            ) B on  A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate
                            left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status = 2

                            Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,C.Priority
                            having A.Balance + sum(isnull(B.MovBalance,0)) <> 0                             

                            ";
                            }
                                
                          
                            
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["InstrumentID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    if (_trxType == 2)
                                    {
                                        M_Instrument.BegBalance = Convert.ToDecimal(dr["BegBalance"]);
                                        M_Instrument.MovBalance = Convert.ToDecimal(dr["MovBalance"]);
                                        M_Instrument.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.TrxBuy = Convert.ToInt32(dr["TrxBuy"]);
                                        M_Instrument.TrxBuyType = Convert.ToString(dr["TrxBuyType"]);
                                    }
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.PaymentType = Convert.ToInt32(dr["PaymentType"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public List<InstrumentRightsCombo> Get_InstrumentRightsByInstrumentPK(int _instrumentPK, int _type, string Date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentRightsCombo> L_Instrument = new List<InstrumentRightsCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"Declare @ID nvarchar(50)
                                            select @ID = ID From Instrument where InstrumentPK = @InstrumentPK
                                            select A.InstrumentPK InstrumentRightsPK,C.FundPK, C.ID + '-' + C.Name FundID,B.ID + '-' + B.Name InstrumentRightsID,Balance from FundPosition A 
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                            left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                                            where B.InstrumentTypePK = @InstrumentTypePK and B.ID like '%' + @ID + '%' and Date = @Date and A.Status = 2";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        if (_type == 1) //rights
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 4);
                        }
                        else //warrant
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 16);
                            //Instrument = 16;
                        }
                        cmd.Parameters.AddWithValue("@Date", Date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentRightsCombo M_Instrument = new InstrumentRightsCombo();
                                    M_Instrument.InstrumentRightsPK = Convert.ToInt32(dr["InstrumentRightsPK"]);
                                    M_Instrument.InstrumentRightsID = Convert.ToString(dr["InstrumentRightsID"]);
                                    M_Instrument.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Instrument.FundID = Convert.ToString(dr["FundID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
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
                                cmd.CommandText = @"select A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,AcqDate,A.TrxBuy,A.Price from (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate AcqDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @Date and A.status = 2 and posted = 1 and C.Type in (1)
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,TrxPortfolioPK,A.Price
                
                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and C.Type in (1) and TrxType  = 1 
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,TrxPortfolioPK,A.Price

                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status <> 3  and C.Type in (1)
                                and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,TrxPortfolioPK,A.Price
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,AcqDate,A.TrxBuy,A.Price
                                having A.Balance + sum(isnull(B.MovBalance,0)) <> 0  ";
                            }

                        }
                        else if (_instrumentTypePK == 2)
                        {
                            if (_trxType == 1)
                            {
                                cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT 
                                on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                                where I.status = 2 and I.InstrumentTypePK in (2,3,8,9,11,13,14,15) ";
                            }
                            else
                            {
                                cmd.CommandText = @"select A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price from (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,A.ValueDate AcqDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @Date and A.status = 2 and posted = 1 and C.InstrumentTypePK in (2,3,8,9,11,13,14,15)
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price
                
                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and C.InstrumentTypePK in (2,3,8,9,11,13,14,15) and TrxType  = 1 
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price

                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK TrxBuy,A.Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status <> 3  and  C.InstrumentTypePK in (2,3,8,9,11,13,14,15)
                                 and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price
                                having A.Balance + sum(isnull(B.MovBalance,0)) <> 0  ";
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

                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType in (1,3)  then A.Volume else
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
                                where ValueDate = @Date and A.status <> 3  and C.Type = 3
                                and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                having A.Balance + sum(isnull(B.MovBalance,0)) <> 0  ";
                            }
                            else
                            {
                                cmd.CommandText = @"
                                select A.InstrumentPK,A.InstrumentID,sum(A.Balance) BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,sum(A.Balance + isnull(B.MovBalance,0)) Balance,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,A.Price,A.BankPK,A.BankBranchPK from (

                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType in (1,3)  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,'' AcqDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @Date and A.status = 2 and posted = 1 and Revised = 0 and C.Type = 3 and A.MaturityDate = @Date
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                

                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType in (1,3)  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,'' ValueDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and Revised = 0 and C.Type = 3 and TrxType  in (1,3) and A.MaturityDate = @Date
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK

                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType in (1,3)  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,'' ValueDate,A.InterestPercent,A.MaturityDate,0 TrxBuy,A.Price,A.BankPK,A.BankBranchPK
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status <> 3  and C.Type = 3 and A.MaturityDate = @Date
                                    and TrxType  = 2 and Posted = 1 and Revised = 0 
                                group By A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxPortfolioPK,A.Price,A.BankPK,A.BankBranchPK
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.CurrencyID,AcqDate,A.InterestPercent,A.MaturityDate,A.Price,A.BankPK,A.BankBranchPK
                                having sum(A.Balance + isnull(B.MovBalance,0)) <> 0   ";
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
                                cmd.CommandText = @"
                                
                                select A.InstrumentPK,A.InstrumentID,sum(A.Balance) BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,sum(A.Balance + isnull(B.MovBalance,0)) Balance,A.CurrencyID,A.AcqDate,A.TrxBuy,A.Price,0 InterestPercent,'' MaturityDate from (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) Balance,'IDR' CurrencyID,'' AcqDate,0 TrxBuy,0 Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate < @date and A.status = 2 and posted = 1 and C.Type in (4)
                                group By A.InstrumentPK,B.ID,B.Name
                
                                union all
	
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,'' AcqDate,0 TrxBuy,0 Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.Status = 2 and Posted = 1 and C.Type in (4) and TrxType  = 1 
                                group By A.InstrumentPK,B.ID,B.Name
                                ) A 
                                left join
                                (
                                Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.Volume else
                                case when A.trxType = 2  then A.Volume * -1 end end) MovBalance,'IDR' CurrencyID,'' AcqDate,0 TrxBuy,0 Price
                                from TrxPortfolio A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                                where ValueDate = @Date and A.status <> 3  and C.Type in (4)
                                and TrxType  = 2  
                                group By A.InstrumentPK,B.ID,B.Name
                                ) B on  A.InstrumentPK = B.InstrumentPK

                                Group By A.InstrumentPK,A.InstrumentID,A.AcqDate,A.CurrencyID,A.TrxBuy,A.Price
                                having sum(A.Balance + isnull(B.MovBalance,0)) > 0   ";
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
                                        }
                                        M_Instrument.BegBalance = Convert.ToDecimal(dr["BegBalance"]);
                                        M_Instrument.MovBalance = Convert.ToDecimal(dr["MovBalance"]);
                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.TrxBuy = Convert.ToInt32(dr["TrxBuy"]);
                                        M_Instrument.Price = Convert.ToDecimal(dr["Price"]);
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

        public List<InstrumentForInvestment> Instrument_LookupForOMSReksadanaByMarketPK(int _trxType, int _fundPK, DateTime _date, int _marketPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT " +
                               "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 " +
                               "where I.status = 2 and IT.Type in (4) and I.MarketPK = @MarketPK order by I.ID  ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
     
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date and FundPK = @FundPK
)
and status = 2 and FundPK = @FundPK

select A.InstrumentPK,A.InstrumentID,sum(A.Balance)  Balance,A.CurrencyID from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,AcqDate,A.AvgPrice Price,A.BitBreakable from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (4) and A.MarketPK = @MarketPK                 

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType, SettlementDate AcqDate,DonePrice Price,A.BitBreakable
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (4) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,A.SettlementDate,DonePrice,A.BitBreakable
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType , SettlementDate AcqDate,DonePrice Price, A.BitBreakable
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (4) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,A.SettlementDate,DonePrice,A.BitBreakable
	
) A 

group by A.InstrumentPK,A.InstrumentID,A.CurrencyID

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
	select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
)
and status = 2

select A.InstrumentPK,A.InstrumentID,sum(A.Balance)  Balance,A.CurrencyID, A.AcqDate,ISNULL(A.Price,0) Price,A.TrxBuy,A.TrxBuyType,A.BitBreakable from (

	select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,AcqDate,A.AvgPrice Price,A.BitBreakable from FundPosition A    
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
	Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (4) and A.MarketPK = @MarketPK                 

	union all
	
	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType, SettlementDate AcqDate,DonePrice Price,A.BitBreakable
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (4) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and A.OrderStatus = 'M'
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 1
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,A.SettlementDate,DonePrice,A.BitBreakable
	
	UNION ALL

	Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID
	,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
	,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType , SettlementDate AcqDate,DonePrice Price, A.BitBreakable
	from Investment A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
	left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
	where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (4) and 
	statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
	and FundPK = @FundPK 
	and A.MarketPK = @MarketPK  and A.TrxType = 2
	group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,A.SettlementDate,DonePrice,A.BitBreakable
	
) A 

group by A.InstrumentPK,A.InstrumentID,A.CurrencyID,A.AcqDate,A.Price,A.TrxBuy,A.TrxBuyType,A.BitBreakable

                                ";
                            }

                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {


                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["InstrumentID"]);
                                    if (_trxType == 2)
                                    {
                                        M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                        //M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        //M_Instrument.Price = Convert.ToDecimal(dr["Price"]);
                                        //M_Instrument.TrxBuy = Convert.ToInt32(dr["TrxBuy"]);
                                        //M_Instrument.TrxBuyType = Convert.ToString(dr["TrxBuyType"]);
                                        //M_Instrument.BitBreakable = Convert.ToBoolean(dr["BitBreakable"]);
                                    }
                                    M_Instrument.BitBreakable = true;
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);

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

        public List<InstrumentForInvestment> Instrument_LookupForOMSReksadana(int _trxType, int _fundPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = "select InstrumentPK,I.ID + ' - ' + I.Name ID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK FROM  instrument I left join InstrumentType IT " +
                               "on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 " +
                               "where I.status = 2 and IT.Type in (4) order by I.ID ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                        }
                        else if (_trxType == 2)
                        {
                            if (Tools.ParamFundScheme)
                            {
                                cmd.CommandText = @"
                                Declare @TrailsPK int
                                select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2 and FundPK = @FundPK

                                select InstrumentPK,ID + ' - Total Lot : ' + replace(CONVERT(varchar, CAST(sum(A.Balance)/100 AS money), 1),'.00','') ID,sum(A.Balance) Balance,CurrencyID from (
                                select A.InstrumentPK,B.ID + ' - ' + B.Name ID,A.Balance,D.ID CurrencyID from FundPosition A    
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (4)

                             
                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (4) and OrderStatus in ('M','P')
                                and FundPK = @fundpk and TrxType  = 1
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate

                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  and A.instrumentTypePK in (1)
                                and FundPK = @fundpk and TrxType  = 2
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate
                                ) A
                                group By InstrumentPK,ID,CurrencyID
                                order by ID

                                ";
                            }
                            else
                            {
                                cmd.CommandText = @"
                                Declare @TrailsPK int
                                select @TrailsPK = EndDayTrailsFundPortfolioPK from EndDayTrailsFundPortfolio where ValueDate = dbo.FWorkingDay(@Date,-1) and status = 2

                                select InstrumentPK,ID + ' - Total Lot : ' + replace(CONVERT(varchar, CAST(sum(A.Balance)/100 AS money), 1),'.00','') ID,sum(A.Balance) Balance,CurrencyID from (
                                select A.InstrumentPK,B.ID + ' - ' + B.Name ID,A.Balance,D.ID CurrencyID from FundPosition A    
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                                Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                                where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (4)

                             
                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (4) and OrderStatus in ('M','P')
                                and FundPK = @fundpk and TrxType  = 1
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate

                                union all

                                Select B.InstrumentPK,B.ID + ' - ' + B.Name ID,sum(case when A.TrxType = 1  then A.DoneVolume else
                                case when A.trxType = 2  then A.DoneVolume * -1 end end),'IDR' CurrencyID   
                                from Investment A
                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                                Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                                where ValueDate = @date and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  and A.instrumentTypePK in (1)
                                and FundPK = @fundpk and TrxType  = 2
                                group By B.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate
                                ) A
                                group By InstrumentPK,ID,CurrencyID
                                order by ID

                                ";
                            }
                           
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<InstrumentForInvestment> Instrument_LookupForOMSBondByMarketPKByCrossFund(int _trxType, int _fundPK, DateTime _date, int _marketPK, int _crossFundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                            select A.InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,DoneVolume Balance,  D.ID CurrencyID,I.CurrencyPK,isnull(12/B.Priority,0) PaymentType,A.OrderPrice Price , I.InstrumentTypePK
                            FROM  Investment A
                            left join Instrument I on A.InstrumentPK=I.InstrumentPK and I.status = 2 
                            left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                            left join MasterValue B on I.InterestPaymentType = B.Code and B.ID = 'InterestPaymentType' and B.status = 2
                            where I.status = 2 and I.InstrumentTypePK not in (1,5) and I.MarketPK = @MarketPK and A.MaturityDate >= @Date and ValueDate = @Date
                            and CrossFundFromPK = @FundPK and FundPK = @CrossFundPK and statusInvestment = 2 and TrxType = 2
                            order by I.ID ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@CrossFundPK", _crossFundPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["InstrumentID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.Price = Convert.ToDecimal(dr["Price"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.PaymentType = Convert.ToInt32(dr["PaymentType"]);
                                    M_Instrument.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
                                    L_Instrument.Add(M_Instrument);
                                }
                            } return L_Instrument;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<InstrumentCombo> InstrumentReksadana_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  A.InstrumentPK,A.ID + ' - ' + A.Name ID, A.Name,B.ID Type  FROM [Instrument] A Left join InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK and B.Status = 2  where A.status = 2 and B.Type in(4) order by A.ID,A.Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.Type = Convert.ToString(dr["Type"]);
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

        public InstrumentForInvestment Get_DataInstrumentForYield(int _instrumentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select InterestPaymentType PaymentPeriod,InterestDaysType from Instrument 
                        where InstrumentPK = @InstrumentPK and status = 2 ";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    M_Instrument.PaymentPeriod = Convert.ToInt32(dr["PaymentPeriod"]);
                                    M_Instrument.InterestDaysType = Convert.ToInt32(dr["InterestDaysType"]);

                                    return M_Instrument;

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

        public InstrumentCombo Find_Instrument(string _id)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InstrumentPK,ID + ' - ' + Name ID, Name,InstrumentTypePK FROM [Instrument]  where status = 2 and ID = @ID order by ID,Name";
                        cmd.Parameters.AddWithValue("@ID", _id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new InstrumentCombo()
                                {
                                    InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]),
                                    ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]),
                                    Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]),
                                    InstrumentTypePK = dr["InstrumentTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentTypePK"]),

                                };
                            }
                            else
                            {
                                return new InstrumentCombo()
                                {
                                    InstrumentPK = 0,
                                    ID = "",
                                    Name = "",
                                    InstrumentTypePK = 0,
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


        public InstrumentCombo Find_InstrumentTypeSell(string _id, int _fundPK, DateTime _date, int _marketPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //cmd.CommandText = "SELECT  InstrumentPK,ID + ' - ' + Name ID, Name FROM [Instrument]  where status = 2 and ID = @ID order by ID,Name";
                        if (Tools.ParamFundScheme)
                        {
                            cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date  and FundPK = @FundPK
                        )
                        and status = 2 and FundPK = @FundPK

                        set @MaxDateEndDayFP =  isnull(@MaxDateEndDayFP,'1900-01-01')

                        select InstrumentPK,InstrumentID ID,Name,InstrumentTypePK from (

                        select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,B.InstrumentTypePK from FundPosition A    
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                        Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status =                                             2
                        where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.MarketPK = @MarketPK   and B.ID = @ID               

                        union all
	
                        Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name
                        ,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
                        ,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
                        statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
                        and A.OrderStatus = 'M'
                        and FundPK = @FundPK  and B.ID = @ID
                        and A.MarketPK = @MarketPK  and A.TrxType = 1
                        group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
                        UNION ALL

                        Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name
                        ,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
                        ,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
                        statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
                        and FundPK = @FundPK and B.ID = @ID
                        and A.MarketPK = @MarketPK  and A.TrxType = 2
                        group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
                        ) A 


                        group by A.InstrumentPK,A.InstrumentID,A.CurrencyID,A.Name,A.InstrumentTypePK

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
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date
                        )
                        and status = 2

                        set @MaxDateEndDayFP =  isnull(@MaxDateEndDayFP,'1900-01-01')

                        select InstrumentPK,InstrumentID ID,Name,A.InstrumentTypePK from (

                        select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name,A.Balance,D.ID CurrencyID,[Identity] TrxBuy,'FP' TrxBuyType,B.InstrumentTypePK from FundPosition A    
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
                        Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status =  2
                        where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and C.type in (1) and A.MarketPK = @MarketPK   and B.ID = @ID               

                        union all
	
                        Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name
                        ,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) Balance
                        ,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
                        statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
                        and A.OrderStatus = 'M'
                        and FundPK = @FundPK  and B.ID = @ID
                        and A.MarketPK = @MarketPK  and A.TrxType = 1
                        group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
                        UNION ALL

                        Select A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,B.ID ID,B.Name Name
                        ,sum(case when DoneVolume <> 0 then DoneVolume else DoneAmount / case when dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK)  = 0 then [dbo].[FGetLastClosePrice](@Date,A.InstrumentPK)			else dbo.[FgetPriceFromInstrumentMarketInfo](@Date,A.InstrumentPK) End  End) *-1 Balance
                        ,D.ID,InvestmentPK TrxBuy,'INV' TrxBuyType,B.InstrumentTypePK
                        from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status = 2
                        where ValueDate > @MaxDateEndDayFP and ValueDate <= @Date  and C.Type in (1) and 
                        statusInvestment <> 3 and StatusSettlement <> 3 and StatusDealing <> 3
                        and FundPK = @FundPK and B.ID = @ID
                        and A.MarketPK = @MarketPK  and A.TrxType = 2
                        group By A.InstrumentPK,B.ID,B.Name,TrxBuy,TrxBuyType,InvestmentPK,D.ID,B.InstrumentTypePK
	
                        ) A 


                        group by A.InstrumentPK,A.InstrumentID,A.CurrencyID,A.Name,A.InstrumentTypePK
                        ";
                        }


                        cmd.Parameters.AddWithValue("@ID", _id);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new InstrumentCombo()
                                {
                                    InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]),
                                    ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]),
                                    Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]),
                                    InstrumentTypePK = dr["InstrumentTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentTypePK"]),
                                };
                            }
                            else
                            {
                                return new InstrumentCombo()
                                {
                                    InstrumentPK = 0,
                                    ID = "",
                                    Name = "",
                                    InstrumentTypePK = 0,
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


        public int Get_LastInstrumentByInstrumentTypePK(int _instrumentTypePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select top 1 InstrumentPK from Instrument where status = 2 
                        and InstrumentTypePK = @InstrumentTypePK order by InstrumentPK desc ";

                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrumentTypePK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToInt32(dr["InstrumentPK"]);

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

        public List<InstrumentCombo> InstrumentBondRating_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCombo> L_Instrument = new List<InstrumentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
                        if object_id('tempdb..#BondRating', 'u') is not null drop table #BondRating
                        create table #BondRating
                        (
                        BondRating nvarchar(50)
                        )
                        insert into #BondRating(BondRating)
                        SELECT distinct BondRating FROM [Instrument]  where status = 2 

                        select --ROW_NUMBER() OVER(ORDER BY BondRating ASC) AS BondRatingPK , 
                        case when BondRating = '' then 'There is no bond rating' else BondRating end ID,case when BondRating = '' then 'There is no bond rating' else BondRating end Name from #BondRating
                        union all 
                        select 'All','' ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCombo M_Instrument = new InstrumentCombo();
                                    //M_Instrument.BondRatingPK = Convert.ToInt32(dr["BondRatingPK"]);
                                    M_Instrument.Name = Convert.ToString(dr["Name"]);
                                    M_Instrument.ID = Convert.ToString(dr["ID"]);
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

        public int Instrument_AddFromSettlement(DateTime _maturityDate, int _instrumentPK, decimal _interestPercent, string _category, string _userID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        --    if exists(
	                    --Select InterestPercent,* from Instrument 
	                    --where MaturityDate = @MaturityDate and InterestPercent = @InterestPercent and InstrumentTypePK = 5 and Category = @Category and CurrencyPK = @CurrencyPK
                        --and BankPK = @BankPK
	                    --)
	                    --BEGIN
		                --    select InstrumentPK From Instrument
		                --    where MaturityDate = @MaturityDate and InterestPercent = @InterestPercent and InstrumentTypePK = 5 and Category = @Category and CurrencyPK = @CurrencyPK
                        --    and BankPK = @BankPK
	                    --END
	                    --ELSE
	                    --BEGIN
                            Declare @BankPK int
                            Declare @CurrencyPK int


                            select @BankPK = BankPK, @CurrencyPK = CurrencyPK from instrument 
                            where instrumentpk = @InstrumentPK and status = 2

		                    Declare @BankID nvarchar(200)
                            Declare @IssuerPK int
		                    select @BankID = ID,@IssuerPK = IssuerPK From Bank Where BankPK = @BankPK and Status = 2
		                    Declare @MaxInstrumentPK int
		                    select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
		                    set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)
		                    Insert Into Instrument(InstrumentPK,HistoryPK,Status,ID,Name,InstrumentTypePK,DepositoTypePK,IssuerPK,lotinshare,BankPK,InterestPercent,MaturityDate,CurrencyPK,Category,TaxExpensePercent,[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)
		                    Select @MaxInstrumentPK,1,2,@BankID, 'TDP ' +@BankID,@InstrumentTypePK,1,@IssuerPK,1,@BankPK,@InterestPercent,@MaturityDate,@CurrencyPK,@Category,20,@EntryUsersID,@Now,@EntryUsersID,@Now,@Now
		                    Select @MaxInstrumentPK InstrumentPK
	                    --END
                        ";
                        cmd.Parameters.AddWithValue("@MaturityDate", _maturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _interestPercent);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Now", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Category", _category);

                        if (_category == "Negotiabale Certificate Deposit")
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 10);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 5);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["InstrumentPK"]);
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

        //bagian fifo
        public List<InstrumentForInvestment> InstrumentFifoBond_LookupForOMSBondByMarketPK(int _trxType, int _fundPK, DateTime _date, int _marketPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentForInvestment> L_Instrument = new List<InstrumentForInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_trxType == 1)
                        {
                            cmd.CommandText = @"select InstrumentPK,I.ID + ' - ' + I.Name InstrumentID, I.Name, I.MaturityDate, I.InterestPercent,0 Balance,  D.ID CurrencyID,I.CurrencyPK,isnull(12/B.Priority,0) PaymentType 
                            FROM  instrument I 
                            left join InstrumentType IT on I.InstrumentTypePK=IT.InstrumentTypePK and IT.status = 2 left join Currency D on I.CurrencyPK = D.CurrencyPK and D.status = 2 
                            left join MasterValue B on I.InterestPaymentType = B.Code and B.ID = 'InterestPaymentType' and B.status = 2
                            where I.status = 2 and IT.InstrumentTypePK in (2,3,8,9,13,15) and I.MarketPK = @MarketPK and MaturityDate >= @Date order by I.ID ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                        }
                        else if (_trxType == 2)
                        {

                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"

                           

Declare @TrailsPK int
Declare @TrailsDate datetime

select @TrailsPK = EndDayTrailsFundPortfolioPK,@TrailsDate = ValueDate from EndDayTrailsFundPortfolio 
where ValueDate = 
(
select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @Date  and FundPK = @FundPK   
)
and status = 2 and FundPK = @FundPK                               

select A.TrxBuy,A.TrxBuyType,A.InstrumentPK,A.InstrumentID,A.Balance BegBalance,sum(isnull(B.MovBalance,0)) MovBalance,A.Balance + sum(isnull(B.MovBalance,0)) Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,isnull(12/C.Priority,0) PaymentType from (

select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,A.Balance Balance,'IDR' CurrencyID,'1900-01-01' AcqDate,C.InterestPercent,B.MaturityDate MaturityDate,0 TrxBuy,'FP' TrxBuyType,C.AvgPrice,B.InterestPaymentType from 
( select A.FundPK,A.InstrumentPK,sum(A.Balance) Balance from FundPosition A    
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
Left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2   
where A.FundPK = @FundPK  and A.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15) and A.MarketPK = @MarketPK  and A.status = 2 and A.MaturityDate >= @Date
group by A.FundPK,A.InstrumentPK
)A
left join FundPosition C on A.InstrumentPK = C.InstrumentPK and C.Status = 2   
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2    
Left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status = 2   
where C.FundPK = @FundPK  and C.TrailsPK = @TrailsPK and B.InstrumentTypePK in (2,3,8,9,13,15) and C.MarketPK = @MarketPK 
group By A.FundPK,A.InstrumentPK,B.ID,B.Name,A.Balance,C.InterestPercent,C.AvgPrice,B.InterestPaymentType,B.MaturityDate

union all
	
Select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR',A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
where ValueDate = @Date and StatusInvestment = 2 and StatusDealing = 2 and A.instrumentTypePK in (2,3,8,9,13,15) and OrderStatus in ('M','P') and A.MaturityDate >= @Date
and FundPK = @FundPK and TrxType  = 1  and A.MarketPK = @MarketPK 
group By A.FundPK,A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType

) A 
left join
(
Select A.FundPK,A.InstrumentPK,B.ID + ' - ' + B.Name InstrumentID,sum(case when A.TrxType = 1  then A.DoneVolume else
case when A.trxType = 2  then A.DoneVolume * -1 end end) MovBalance,'IDR' CurrencyID,A.ValueDate,A.InterestPercent,A.MaturityDate,InvestmentPK TrxBuy,'INV' TrxBuyType,A.DonePrice,B.InterestPaymentType,'1900-01-01' AcqDate
from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
Left join Bank C on B.BankPK = C.BankPK and C.status = 2 
where ((@ClientCode in ('20','21') and ValueDate > @TrailsDate and ValueDate <= @date )   or (@ClientCode = '03' and ValueDate = @Date ))
and StatusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3 and A.instrumentTypePK in (2,3,8,9,13,15) and A.MaturityDate >= @Date
and FundPK = @FundPK and TrxType  = 2  and A.MarketPK = @MarketPK
group By A.FundPK,A.InstrumentPK,B.ID,B.Name,B.InterestPercent,B.MaturityDate,A.ValueDate,A.InterestPercent,A.MaturityDate,TrxBuy,TrxBuyType,InvestmentPK,A.DonePrice,B.InterestPaymentType,A.AcqDate
) B on  A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate and A.FundPK = B.FundPK
left join MasterValue C on A.InterestPaymentType = C.Code and C.ID = 'InterestPaymentType' and C.status = 2

Group By A.FundPK,A.InstrumentPK,A.InstrumentID,A.Balance,A.CurrencyID,A.AcqDate,A.InterestPercent,A.MaturityDate,A.TrxBuy,A.TrxBuyType,A.AvgPrice,C.Priority
having A.Balance + sum(isnull(B.MovBalance,0)) <> 0 
order by InstrumentPK               

                            ";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketPK);
                            cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentForInvestment M_Instrument = new InstrumentForInvestment();
                                    M_Instrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
                                    M_Instrument.ID = Convert.ToString(dr["InstrumentID"]);
                                    M_Instrument.Balance = Convert.ToDecimal(dr["Balance"]);
                                    if (_trxType == 2)
                                    {
                                        M_Instrument.BegBalance = Convert.ToDecimal(dr["BegBalance"]);
                                        M_Instrument.MovBalance = Convert.ToDecimal(dr["MovBalance"]);
                                        M_Instrument.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                                        M_Instrument.AcqDate = Convert.ToString(dr["AcqDate"]);
                                        M_Instrument.TrxBuy = Convert.ToInt32(dr["TrxBuy"]);
                                        M_Instrument.TrxBuyType = Convert.ToString(dr["TrxBuyType"]);
                                    }
                                    M_Instrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
                                    M_Instrument.MaturityDate = Convert.ToString(dr["MaturityDate"]);
                                    M_Instrument.PaymentType = Convert.ToInt32(dr["PaymentType"]);
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

        //end fifo
        public bool Check_InstrumentStatusPending()
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
                        
                        if (@ClientCode = '03')
                        BEGIN
                            if Exists(select * From Instrument where Status in (1))  
                            BEGIN 
                                Select 1 Result END 
                            ELSE 
                            BEGIN   
                                Select 0 Result 
                            END 
                        END
                        ELSE
                        BEGIN
                            Select 0 Result 
                        END ";

                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
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

        public Boolean Validate_CheckFirstCouponDate(int _instrumentPK)
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

                        declare @FirstCouponDate datetime

                        select @FirstCouponDate = isnull(FirstCouponDate,'01/01/1900') from Instrument where InstrumentPK = @InstrumentPK and status in (1,2)

                        IF (@FirstCouponDate = '01/01/1900')
                        BEGIN
                            select 1 Result
                        END
                        ELSE
                        BEGIN
                            select 0 Result
                        END
                  ";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);

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