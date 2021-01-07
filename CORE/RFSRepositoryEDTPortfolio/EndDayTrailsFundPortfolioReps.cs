using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using RFSRepository;
using RFSRepositoryEDTPortfolio;


namespace RFSRepositoryEDTPortfolio
{
    public class EndDayTrailsFundPortfolioReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[EndDayTrailsFundPortfolio] " +
                            "([EndDayTrailsFundPortfolioPK],[HistoryPK],[Status],[BitValidate],[ValueDate],[LogMessages],";
        string _paramaterCommand = "@BitValidate,@ValueDate,@LogMessages,";

        //2
        private EndDayTrailsFundPortfolio setEndDayTrailsFundPortfolio(SqlDataReader dr)
        {
            EndDayTrailsFundPortfolio M_EndDayTrailsFundPortfolio = new EndDayTrailsFundPortfolio();
            M_EndDayTrailsFundPortfolio.EndDayTrailsFundPortfolioPK = Convert.ToInt32(dr["EndDayTrailsFundPortfolioPK"]);
            M_EndDayTrailsFundPortfolio.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_EndDayTrailsFundPortfolio.Status = Convert.ToInt32(dr["Status"]);
            M_EndDayTrailsFundPortfolio.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_EndDayTrailsFundPortfolio.Notes = Convert.ToString(dr["Notes"]);
            M_EndDayTrailsFundPortfolio.BitValidate = Convert.ToBoolean(dr["BitValidate"]);
            M_EndDayTrailsFundPortfolio.ValueDate = dr["ValueDate"].ToString();
            M_EndDayTrailsFundPortfolio.LogMessages = dr["LogMessages"].ToString();
            M_EndDayTrailsFundPortfolio.EntryUsersID = dr["EntryUsersID"].ToString();
            M_EndDayTrailsFundPortfolio.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_EndDayTrailsFundPortfolio.VoidUsersID = dr["VoidUsersID"].ToString();
            M_EndDayTrailsFundPortfolio.EntryTime = dr["EntryTime"].ToString();
            M_EndDayTrailsFundPortfolio.ApprovedTime = dr["ApprovedTime"].ToString();
            M_EndDayTrailsFundPortfolio.VoidTime = dr["VoidTime"].ToString();
            M_EndDayTrailsFundPortfolio.DBUserID = dr["DBUserID"].ToString();
            M_EndDayTrailsFundPortfolio.DBTerminalID = dr["DBTerminalID"].ToString();
            M_EndDayTrailsFundPortfolio.LastUpdate = dr["LastUpdate"].ToString();
            M_EndDayTrailsFundPortfolio.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_EndDayTrailsFundPortfolio;
        }

        //3
        public List<EndDayTrailsFundPortfolio> EndDayTrailsFundPortfolio_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<EndDayTrailsFundPortfolio> L_EndDayTrailsFundPortfolio = new List<EndDayTrailsFundPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolio where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolio";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndDayTrailsFundPortfolio.Add(setEndDayTrailsFundPortfolio(dr));
                                }
                            }
                            return L_EndDayTrailsFundPortfolio;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<EndDayTrailsFundPortfolio> EndDayTrailsFundPortfolio_SelectEndDayTrailsFundPortfolioDate(int _status, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<EndDayTrailsFundPortfolio> L_EndDayTrailsFundPortfolio = new List<EndDayTrailsFundPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolio where status = @status and ValueDate = @Date";
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolio where  ValueDate = @Date";
                        }

                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndDayTrailsFundPortfolio.Add(setEndDayTrailsFundPortfolio(dr));
                                }
                            }
                            return L_EndDayTrailsFundPortfolio;
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
        public void EndDayTrailsFundPortfolio_Add(EndDayTrailsFundPortfolio _EndDayTrailsFundPortfolio, bool _havePrivillege)
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
                                 "Select isnull(max(EndDayTrailsFundPortfolioPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from EndDayTrailsFundPortfolio";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _EndDayTrailsFundPortfolio.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(EndDayTrailsFundPortfolioPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from EndDayTrailsFundPortfolio";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BitValidate", _EndDayTrailsFundPortfolio.BitValidate);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@LogMessages", _EndDayTrailsFundPortfolio.LogMessages);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _EndDayTrailsFundPortfolio.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
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

        //7
        public void EndDayTrailsFundPortfolio_Approved(EndDayTrailsFundPortfolio _EndDayTrailsFundPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolio set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where EndDayTrailsFundPortfolioPK = @PK and historypk = @historyPK \n " +
                            "Update FundPosition set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate where TrailsPK = @PK and status = 1 " +
                            "Update FundJournal set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate , posted= 1,postedby = @ApprovedUsersID,PostedTime = @ApprovedTime where TrxNo = @PK and status = 1 ";

                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolio.EndDayTrailsFundPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _EndDayTrailsFundPortfolio.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update EndDayTrailsFundPortfolio set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where EndDayTrailsFundPortfolioPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolio.EndDayTrailsFundPortfolioPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolio.ApprovedUsersID);
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
        public void EndDayTrailsFundPortfolio_Reject(EndDayTrailsFundPortfolio _EndDayTrailsFundPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where EndDayTrailsFundPortfolioPK = @PK and historypk = @historyPK \n " +
                             @" --Update CorporateActionResult set status = 3 where Date = @ValueDate " +
                            "update FundPosition set status = 3 ,LastUpdate = @LastUpdate where Date = @ValueDate and TrailsPK = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolio.EndDayTrailsFundPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolio.VoidUsersID);
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

        //9
        public void EndDayTrailsFundPortfolio_Void(EndDayTrailsFundPortfolio _EndDayTrailsFundPortfolio)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate 
                            where EndDayTrailsFundPortfolioPK = @PK and historypk = @historyPK 
                     
                            update FundPosition set status = 3 ,LastUpdate = @LastUpdate where Date = @ValueDate and TrailsPK = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolio.EndDayTrailsFundPortfolioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolio.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolio.VoidUsersID);
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

        //7
        public string EndDayTrailsFundPortfolio_ApproveByDate(string _usersID, DateTime _date)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolio set status = 2, ApprovedUsersID = @ApprovedUsersID, ApprovedTime = @ApprovedTime, LastUpdate = @LastUpdate " +
                            "where Status not in (2, 3, 4) and ValueDate = @Date ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return "Success";
                    }
                }
            }
            catch (Exception err)
            {
                return err.Message.ToString();
                throw err;
            }

        }

        public int EndDayTrailsFundPortfolio_Generate(string _usersID, DateTime _valueDate)
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

                        UPDATE Investment set PriceMode = 1 where InstrumentTypePK  in(1,4,16)
  UPDATE Investment set PriceMode = 0 where InstrumentTypePK not in(1,4,16)
UPDATE Investment set MarketPK = 1
UPDATE Investment set Category = null where InstrumentTypePK  <> 5

Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioPK int                    

Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo                  
Select @maxEndDayTrailsFundPortfolioPK = max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio     
set @maxEndDayTrailsFundPortfolioPK = isnull(@maxEndDayTrailsFundPortfolioPK,1)               

Insert into EndDayTrailsFundPortfolio  (EndDayTrailsFundPortfolioPK,HistoryPK,Status,ValueDate,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select @maxEndDayTrailsFundPortfolioPK,1,1,@ValueDate,0
,'',@UsersID,@LastUpdate,@LastUpdate 

--drop table #ZLogicFundPosition   
--drop table #ZFundPosition     
--drop table #ZDividenSaham      
      
Create Table #ZFundPosition                  
(                  
InstrumentPK int,     
InstrumentTypePK int,                  
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
FundPK int,                  
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
AvgPrice numeric(38,12),                  
LastVolume numeric(38,4),                  
ClosePrice numeric(38,12),                  
TrxAmount numeric(38,6),              
AcqDate datetime,              
MaturityDate datetime,              
InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
BitBreakable bit
)                  
    
Create Table #ZLogicFundPosition              
(              
BuyVolume numeric(38,4),              
SellVolume numeric(38,4),              
BuyAmount numeric(38,4),       
SellAmount numeric(38,4),            
FundPK int,              
InstrumentPK int,              
SettlementDate datetime,              
MaturityDate datetime,              
InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
AcqDate datetime,
BitBreakable bit
)              

declare @AvgPriceEx table
(
FundPK int,
InstrumentPK int,
Price numeric(18,12)

)

insert into @AvgPriceEx
Select A.FundPK,A.InstrumentPK,[dbo].[FgetAvgpriceExercise](@ValueDate,A.FundPK,A.InstrumentPK) from Exercise A
left join FundPosition B on A.InstrumentRightsPK = B.InstrumentPK and A.FundPK = B.FundPK and B.Status = 2 
and B.Date  = 
(select Max(Date) from FundPosition C where C.Date < A.Date and C.FundPK = B.FundPK and C.status = 2)
where DistributionDate  <= @ValueDate and A.status = 2 
group by A.FundPK,A.InstrumentPK


-- TARIK JUAL BELI DARI INVESTMENT              
Insert into #ZLogicFundPosition	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable)               

Select SUM(BuyVolume) BuyVolume,SUM(SellVolume) SellVolume,SUM(BuyAmount) BuyAmount,SUM(SellAmount) SellAmount,B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,
B.InterestPercent,B.CurrencyPK,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate
,B.BankPK,B.BankBranchPK,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable
From               
(               
Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
isnull(A.SettlementDate,'') SettlementDate,isnull(A.MaturityDate,'') MaturityDate,isnull(A.InterestPercent,0) InterestPercent,
isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.TaxExpensePercent,0) TaxExpensePercent,isnull(A.MarketPK,0) MarketPK,
isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
A.AcqDate,A.BitBreakable
from (                 
	
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type = 1 then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2   and A.PeriodPK = @PeriodPK        
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type = 1 then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2   and A.PeriodPK = @PeriodPK              
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			
UNION ALL

select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type = 1 then null else SettlementDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2     and A.PeriodPK = @PeriodPK           
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK

UNION ALL
-- EXERCISE
select A.InstrumentPK,sum(BalanceExercise) BuyVolume,0 SellVolume,SUM(BalanceExercise * Price) BuyAmount,0 SellAmount, FundPK,               
null SettlementDate,              
null MaturityDate,              
null InterestPercent,DistributionDate,
B.CurrencyPK,null,0,1
,0 InterestDaysType
,0 InterestPaymentType
,0
,null
,0,0
,1,0,null AcqDate,0 BitBreakable       
from Exercise A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where DistributionDate <= @ValueDate  and A.Status = 2 and year(Date) = year(@ValueDate)            
Group By A.InstrumentPK,FundPK,B.CurrencyPK,DistributionDate



)A                
Group By A.InstrumentPK,A.FundPK,A.SettlementDate,A.MaturityDate,A.InterestPercent
,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,B.InterestPercent,B.CurrencyPK
,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable




--INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,             
isnull(A.BuyAmount,0) - (isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) 
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0)	TrxAmount
,A.AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType
,A.PriceMode,A.BitIsAmortized
From #ZLogicFundPosition A              
Left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2                
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK  
and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')                
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (1,2,4,5,14,9)



-- INSERT INVESTMENT + BEG BALANCE DEPOSITO ONLY ( INVESTMENT + BEG BALANCE )              
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
select InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,avg(AvgPrice)
,sum(LastVolume),avg(ClosePrice),TrxAmount,AcqDate,MaturityDate,InterestPercent
,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable
from (
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID InstrumentID,A.FundPK,C.ID FundID, 
1 AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
1 ClosePrice,                  
isnull(A.BuyAmount,0) - (1 * isnull(A.SellVolume,0))  + isnull		(B.TrxAmount,0) TrxAmount,              
A.AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when D.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From #ZLogicFundPosition A              
LEft join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2     
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK 
and isnull(A.Maturitydate,'01/01/1900') = isnull(B.MaturityDate,'01/01/1900')    
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (3)
)A  
group by InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,TrxAmount,AcqDate
,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) AvgPrice,              
isnull(A.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,                  
isnull(A.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900'),isnull(A.MaturityDate,'01/01/1900'),isnull(A.InterestPercent,0),C.CurrencyPK, A.Category,isnull(A.TaxExpensePercent,0),A.MarketPK
,A.PriceMode,A.BitIsAmortized,isnull(C.InterestDaysType,2),isnull(C.InterestPaymentType,1),Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2              
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZFundPosition B on A.InstrumentPK = B.InstrumentPK 
and A.FundPK = B.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')              
where A.PeriodPK = @PeriodPK              
) and E.Type in (1,2,4,5,14,9) and A.periodPK = @PeriodPK           

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT DEPOSITO ONLY             
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 1 AvgPrice,isnull(A.Volume,0) LastVolume,                  
1 ClosePrice, isnull(A.TrxAmount,0) TrxAmount,              
A.AcqDate,A.MaturityDate,A.InterestPercent,C.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.paymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2     
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2             
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZLogicFundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK  and A.MaturityDate = B.MaturityDate         
where A.PeriodPK = @PeriodPK             
) and E.Type in (3) and A.periodPK = @PeriodPK           



      
-- CORPORATE ACTION DIVIDEN SAHAM

Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)   

-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	
DELETE CorporateActionResult where Date = @ValueDate

-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate 



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate


-- CORPORATE ACTION DIVIDEN RIGHTS
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-R' and D.status = 2
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate


-- CORPORATE ACTION DIVIDEN WARRANT
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price,0 from Exercise 
where DistributionDate  = @ValueDate and status = 2


-- CORPORATE ACTION BOND AMORTIZEN
TRUNCATE TABLE #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
--B.Balance + C.BalanceFromInv LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK  in (2,3,9,15)
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 2 and A.Status = 2 and A.PaymentDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate




-- UPDATE POSISI ZFUNDPOSITION + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
select FundPK,A.InstrumentPK,Price, sum(Balance) Balance,A.status
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
and B.ID not like '%-W' and B.ID not like '%-R'
WHERE A.Date <= @ValueDate and PeriodPK = @PeriodPK

Group By FundPK,A.InstrumentPK,Price,A.status
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
left join instrumentType C on A.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where C.Type in (1,9,2,5,14)
--AND A.LastVolume > 0


                
---- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK RIGHTS
--IF NOT EXISTS
--(
--Select * from #ZFundPosition A 
--left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
--where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
--)
--BEGIN
--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
--TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
--Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
--left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
--left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
--where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2
--END





--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFUNDPOSITION
--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
--,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
--Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
--[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
--A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
--from CorporateActionResult A
--left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
--left join Fund C on A.FundPK = C.FundPK and C.status = 2

--where A.status = 2 and B.ID like '%-W' and PeriodPK = @PeriodPK

--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
--,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
--Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
--[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
--A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
--from CorporateActionResult A
--left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
--left join Fund C on A.FundPK = C.FundPK and C.status = 2
 
--where A.status = 2 and B.ID like '%-R' and PeriodPK = @Period


                       
-- UPDATE POSISI ZFUNDPOSITION + FUND POSITION ADJUSTMENT
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
SELECT FundPK,A.InstrumentPK,case when sum(balance) = 0 then 0 else sum(Price*Balance) / SUM(balance) end Price, sum(Balance) Balance,A.status,
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  THEN A.AcqDate ELSE NULL END AcqDate
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
WHERE A.status = 2 AND A.Date <= @ValueDate and PeriodPK = @PeriodPK
Group By FundPK,A.InstrumentPK,A.status,B.InstrumentTypePK,A.AcqDate
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
AND isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')


--INSERT INSTRUMENT YANG ADA DI FUND POSITION ADJUSTMENT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  

SELECT A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
SUM(A.Balance),dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
SUM(Balance*Price),
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  then  A.AcqDate else null End ,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,NULL,B.TaxExpensePercent,B.MarketPK
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
where A.status = 2 AND A.AcqDate <= @ValueDate and PeriodPK = @PeriodPK
and NOT EXISTS 
(SELECT * FROM #ZFundPosition C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(C.AcqDate,'01/01/1900'))
GROUP BY A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,B.TaxExpensePercent,B.MarketPK,A.AcqDate



-- DELETE RIGHTS AND WARRANT YANG EXPIRED
Delete A From #ZFundPosition A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-R' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 3
)B on A.InstrumentPK = B.InstrumentPK

Delete A From #ZFundPosition A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-W' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 5
)B on A.InstrumentPK = B.InstrumentPK






-- UPDATE AVGPRICE UNTUK EXERCISE 
IF  EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where DistributionDate <= @valuedate and status = 2
)
BEGIN
	Update A set A.AvgPrice = isnull(B.Price,A.AvgPrice),TrxAmount = isnull(B.Price,A.AvgPrice) * LastVolume from #ZFundPosition A
	left join @AvgPriceEx B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
END


-- UPDATE POSISI ZFUNDPOSITION EQUITY + (RIGHT / WARRANT) YG SUDAH DI EXERCISE -- ni ga berkurang
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.TrxAmount = A.AvgPrice * (A.TrxAmount + isnull(B.Balance,0))
from #ZFundPosition A
left join 
(
SELECT FundPK,A.InstrumentRightsPK, sum(BalanceExercise) * -1 Balance
from dbo.Exercise A
WHERE A.status = 2 AND A.DistributionDate <= @valuedate
Group By FundPK,A.InstrumentRightsPK
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentRightsPK
where B.Balance is not null




--select * from #ZLogicFundPosition where FundPK = 7 and InstrumentPK in (3761,3762)
--select * from #ZFundPosition where FundPK = 7 and InstrumentPK in (3761,3762)


Insert into FundPosition(FundPositionPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable)                  
Select @maxEndDayTrailsFundPortfolioPK,@maxEndDayTrailsFundPortfolioPK,1,1,'',@ValueDate,A.FundPK, FundID,                  
A.InstrumentPK,InstrumentID,AvgPrice,LastVolume
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then AvgPrice/100 else AvgPrice End * LastVolume CostValue
, ClosePrice,TrxAmount
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
AcqDate,A.MaturityDate,InterestPercent,A.CurrencyPK, Category,TaxExpensePercent,MarketPK
,isnull(InterestDaysType,0),isnull(InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(BankPK,0),isnull(A.BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
From #ZFundPosition  A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where A.LastVolume > 0 and B.MaturityDate >= @ValueDate --and A.FundPK = 7 and A.InstrumentPK in (3761,3762)
    

Delete FP From FundPosition FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
Where FundPositionPK = @maxEndDayTrailsFundPortfolioPK and I.InstrumentTypePK not in (1,4,6,16)
and FP.MaturityDate <= @ValueDate and FP.MaturityDate Is Not Null  


---------PROSES AMORTIZED DAN PRICE MODE------------------------------
update A set A.ClosePrice =  Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
when A.PriceMode = 2 then LowPriceValue
when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
else  
dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			 
end 
, A.MarketValue = A.Balance * Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
when A.PriceMode = 2 then LowPriceValue
when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
else  
dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			  
end / Case when D.InstrumentTypePK in (2,3,8,14,13,9,15)  then 100 else 1 end
from FundPosition A 
left join 
(
select InstrumentPK,LowPriceValue,ClosePriceValue,HighPriceValue From ClosePrice where Date =
(
Select max(Date) From ClosePrice where date <= @ValueDate and status = 2
) and status = 2
)B on A.InstrumentPK = B.InstrumentPK 
left join instrument C on A.InstrumentPK = C.instrumentPK and C.Status = 2
left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.TrailsPK = @maxEndDayTrailsFundPortfolioPK

-- STATIC CLOSEPRICE


Declare @StaticClosePrice table
(
InstrumentPK int,
InstrumentTypePK int,
ClosePrice numeric(18,8),
FundPK int
)

Declare @FFundPK int

Declare A Cursor For
Select FundPK from Fund where status = 2
Open A
Fetch next From A
Into @FFundPK
WHILE @@FETCH_STATUS = 0  
BEGIN
			

Declare @CInstrumentPK int

Declare B cursor For
Select distinct InstrumentPK from updateclosePrice where status = 2
Open B
Fetch Next From B
Into @CInstrumentPK
While @@Fetch_Status = 0
BEGIN
IF EXISTS(select * from UpdateClosePrice where status = 2 and InstrumentPK = @CInstrumentPK and FundPK = @FFundPK and Date = @ValueDate)
BEGIN

insert into @StaticClosePrice
Select A.InstrumentPK,InstrumentTypePK,A.ClosePriceValue,@FFundPK from UpdateClosePrice A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
where A.status = 2 and A.InstrumentPK = @CInstrumentPK 
and Date = (
Select Max(Date) From UpdateClosePrice where status = 2 and InstrumentPK = @CInstrumentPK
and Date <= @ValueDate and FundPK = @FFundPK
)  and FundPK = @FFundPK

END

FETCH NEXT FROM B INTO @CInstrumentPK  
END
Close B
Deallocate B


		
Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
left join @StaticClosePrice B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where A.Date = @ValueDate and A.TrailsPK = @maxEndDayTrailsFundPortfolioPK
and A.InstrumentPK in(
select instrumentPK From @StaticClosePrice where FundPK = @FFundPK
) and A.FundPK = @FFundPK and A.status = 1

	
FETCH NEXT FROM A 
INTO @FFundPK
END 

CLOSE A;  
DEALLOCATE A;





-- update TrxBuy di Investment untuk Sell / Rollover

declare @DTrxBuy int
declare @DInvestmentPK int
declare @DInstrumentPK int
declare @DFundPK int
declare @DDate datetime
declare @DNewIdentity bigint
declare @DAcqDate datetime

DECLARE C CURSOR FOR 
select TrxBuy,InvestmentPK,B.InstrumentPK,B.FundPK,B.Date,B.AcqDate from Investment A
left join FundPosition B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK in (5,10) and StatusSettlement in (1,2) and TrxType in (2,3)
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate  
While @@FETCH_STATUS = 0
BEGIN   

set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FundPosition where InstrumentPK = @DInstrumentPK and FundPK = @DFundPK and Date = @DDate and AcqDate = @DAcqDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusSettlement in (1,2)


Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate                  
END
Close C
Deallocate C    


	
Update EndDayTrailsFundPortfolio set BitValidate = 1 where EndDayTrailsFundPortfolioPK = @maxEndDayTrailsFundPortfolioPK and Status = 1        

Select @maxEndDayTrailsFundPortfolioPK LastPK
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

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
        
        public bool ValidateGenerateCheckYesterday(DateTime _valueDate)
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
                                  if not Exists
                                  (select * From EndDayTrailsFundPortfolio where Status = 2 and ValueDate = dbo.FNextWorkingDay(@ValueDate,-1))    
                                  BEGIN 
	                                Select 1 Result 
                                  END 
                                  ELSE 
                                  BEGIN     
	                                Select 0 Result 
                                  END  
                                  ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool ValidateGenerate(DateTime _valueDate)
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
                                  if Exists
                                  (select * From EndDayTrailsFundPortfolio where Status in (1,2) and ValueDate = @ValueDate)    
                                  BEGIN 
	                                Select 1 Result 
                                  END 
                                  ELSE 
                                  BEGIN     
	                                Select 0 Result 
                                  END  
                                  ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool ValidateGenerateEndDayTrailsFundPortfolio(DateTime _valueDate)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //RHB
                        cmd.CommandText = " if Not Exists(select * From EndDayTrailsFundPortfolio where Status in (1,2) and ValueDate = @ValueDate)   " +
                                          " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END    ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool ValidateGenerateSettlement(DateTime _valueDate)
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
                        delete investment where TrxType = 0 and instrumentpk = 0 and StatusInvestment = 1 
	                    and DealingPK = 0 and OrderPrice = 0 and Lot = 0

                        if Exists
                        (select * From Investment where  StatusInvestment = 1 and ValueDate = @ValueDate and InstrumentTypePK <> 6)    
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusDealing = 1 and ValueDate = @ValueDate and InstrumentTypePK <> 6) 
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE IF  Exists
                        (select * From Investment where  StatusSettlement = 1 and ValueDate = @ValueDate and InstrumentTypePK <> 6) 
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END   ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public List<NavProjection> EndDayTrailsFundPortfolio_GenerateNavProjection(DateTime _date)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<NavProjection> L_Model = new List<NavProjection>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        declare @FundPK  int
                        declare @StandartFundAdmin  int

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
                        create table #Net
                        (FundPK int,amount numeric(22,4))

                        DECLARE A CURSOR FOR 
                        Select FundPK,StandartFundAdmin from Fund A left join BankCustodian B on A.BankBranchPK  = B.BankCustodianPK and B.status  = 2
                        where A.status  = 2 

                        Open A
                        Fetch Next From A
                        Into @FundPK,@StandartFundAdmin
                        Declare @FundJournalAccountPK int
                        While @@FETCH_STATUS = 0
                        BEGIN



                        IF (@StandartFundAdmin = 1)
                        BEGIN
                            Insert Into #TotalAUM (FundPK,AUM)
                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                            and B.Type in (1,2) and B.ID not like '202%'
                        END
                        ELSE
                        BEGIN
                            --AUM Kemarin
                            Insert Into #TotalAUM (FundPK,AUM)
                            select FundPK,AUM from CloseNav where date = dbo.FWorkingDay(@ValueDate,-1) and fundpk  = @FundPK and Status  = 2
                            
                            --Market Value Hari ini - Market Value Kemarin
                            Insert Into #TotalAUM (FundPK,AUM)
                            select A.FundPK,sum(A.Amount) from (
                            select @FundPK FundPK,sum(MarketValue) Amount from FundPosition where date = @ValueDate and fundpk  = @FundPK and Status  = 2
                            union all
                            select @FundPK FundPK,sum(MarketValue) * -1 Amount from FundPosition where date = dbo.FWorkingDay(@ValueDate,-1) and fundpk  = @FundPK and Status  = 2
                            ) A group by A.FundPK

                            --Settle Hari ini
                            Insert Into #TotalAUM (FundPK,AUM)
                             select @FundPK FundPK,(sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) *-1) *-1 Amount from investment 
                             where  fundpk  = @FundPK and SettlementDate = @valuedate and statusSettlement  = 2

                            --Net Settlement
                            Insert Into #TotalAUM (FundPK,AUM)
                            select A.FundPK,sum(A.Amount) from (
                             select @FundPK FundPK,sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) *-1 Amount from investment 
                             where  fundpk  = @FundPK and SettlementDate between @valuedate and dbo.FWorkingDay(@ValueDate,2) and statusSettlement  = 2
                             union all
                             select @FundPK FundPK,sum(case when TrxType = 1 then (TotalAmount+WHTAmount) * -1 else (TotalAmount-WHTAmount) * 1 end ) Amount from investment 
                             where  fundpk  = @FundPK and SettlementDate between dbo.FWorkingDay(@ValueDate,1) and dbo.FWorkingDay(@ValueDate,3) and statusSettlement  = 2
                             ) A group by A.FundPK

                            --SUBS REDEMP                            
                            Insert Into #TotalAUM (FundPK,AUM)  
                            select FundPK,sum(TotalCashAmount) from ClientSubscription where ValueDate = dbo.FWorkingDay(@ValueDate,-1) and FundPK = @FundPK and status  = 2 and Posted  = 1
                            Group By FundPK

                            Insert Into #TotalAUM (FundPK,AUM)  
                            select FundPK,sum(TotalCashAmount) from ClientSubscription where ValueDate = @ValueDate and FundPK = @FundPK and status  = 1 and BitImmediateTransaction = 1
                            Group By FundPK

                            Insert Into #TotalAUM (FundPK,AUM)  
                            select FundPK,sum(TotalCashAmount * -1) from ClientRedemption where ValueDate = dbo.FWorkingDay(@ValueDate,-1) and FundPK = @FundPK and status  = 2 and Posted  = 1
                            Group By FundPK

                        END


                        if Not Exists
                        (select FundJournalAccountPK from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted = 1  and B.Reversed = 0
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
                        Select @BAum =  AUM From CloseNAV where Date = dbo.FWorkingDay(@ValueDate, - 1) and Status =  2 and FundPK = @BFundPK 
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
                        select @FundPK,case when isnull([dbo].[FGetTotalUnitByFundPK](dbo.FWorkingDay(@ValueDate,-1),@FundPK),0) = 0 then 1 else isnull([dbo].[FGetTotalUnitByFundPK](dbo.FWorkingDay(@ValueDate,-1),@FundPK),0) end

                        Fetch next From A Into @FundPK,@StandartFundAdmin
                        END
                        Close A
                        Deallocate A 

                        --drop table #Shares
                        --drop table #Cash
                        --drop table #Expense
                        --drop table #AR
                        --drop table #Net
                        --drop table #TotalAUM
                        --drop table #TotalUnit
                        --drop table #NAV

                        Insert Into #TotalAUM (FundPK,AUM)
                        select FundPK,sum(amount) from #Expense  Group By FundPK
                    

                        CREATE TABLE #NAV
                        (ValueDate Datetime, FundPK int, Amount numeric(22,2))
                        INSERT INTO #NAV (ValueDate,FundPK,Amount)
                        select @ValueDate,FundPK,sum(AUM) from #TotalAUM Group By FundPK 

                        --select FundPK,sum(amount) from #Shares Group By FundPK
                        --select FundPK,sum(amount) from #Cash  Group By FundPK
                        --select FundPK,sum(amount) from #Expense  Group By FundPK
                        --select FundPK,sum(amount) from #AR  Group By FundPK
                        --select FundPK,sum(amount) from #Net  Group By FundPK

                        select A.ValueDate,A.FundName,isnull(A.Nav,0) Nav,isnull(A.AUM,0) AUM,isnull(sum(((A.Nav/A.LastNAV) - 1)*100),0) Compare from (
                        select ValueDate,C.Name FundName,isnull(Sum(Amount/Unit),0) Nav, isnull(Amount,0) AUM,isnull(D.NAV,1) LastNAV from #NAV A 
                        left join #TotalUnit B on A.FundPK = B.FundPK
                        left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                        left join CloseNav D on A.FundPK = D.FundPK and D.Status = 2 and D.Date  = dbo.FWorkingDay(@ValueDate,-1)
                        Group By ValueDate,C.Name,Amount,D.Nav) A
                        Group By A.ValueDate,A.FundName,A.Nav,A.AUM
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setNavProjection(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private NavProjection setNavProjection(SqlDataReader dr)
        {
            NavProjection M_Model = new NavProjection();
            M_Model.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_Model.FundName = dr["FundName"].ToString();
            M_Model.Nav = Convert.ToDecimal(dr["Nav"]);
            M_Model.AUM = Convert.ToDecimal(dr["AUM"]);
            M_Model.Compare = Convert.ToDecimal(dr["Compare"]);

            return M_Model;
        }

        public Boolean Report_PortfolioValuation(string _userID, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                    
                        declare @InterestDays int

                        SELECT  
                        @InterestDays = DATEDIFF(DAY, valuedate, @valuedate)  
                        FROM enddaytrails  
                        WHERE enddaytrailspk = (SELECT  
                        MAX(enddaytrailspk)  
                        FROM enddaytrails  
                        WHERE valuedate < @valuedate  
                        AND status = 2)  



                        select FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
                        F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
                        FP.ClosePrice ClosePrice
                        ,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
                        ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / @InterestDays 
                        else 0 end AccrualHarian
                        ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / @InterestDays 
                        * datediff(day,FP.AcqDate,@ValueDate )
                        else FP.InterestPercent end Accrual

                        ,FP.InterestPercent 
                        ,FP.MarketValue MarketValue,
                        sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
                        case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav  
                        from fundposition FP   
                        left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status = 2   
                        left join Fund F on FP.FundPK = F.FundPK and F.status = 2   
                        left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2  
                        left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status = 2  
                        left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType' and M.Status = 2
                        where FP.status in (1,2)  and FP.Date = @ValueDate 
                        group by Fp.AVgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
                        FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne
                        order by I.ID
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "FundPortfolioValuationRpt" + "_" + _userID + ".xlsx";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Fund Portfolio");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundPortfolio> rList = new List<FundPortfolio>();
                                    while (dr0.Read())
                                    {
                                        FundPortfolio rSingle = new FundPortfolio();
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                        rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                        rSingle.AvgPrice = dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]);
                                        rSingle.Balance = dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.CostValue = dr0["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostValue"]);
                                        rSingle.ClosePrice = dr0["ClosePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["ClosePrice"]);
                                        rSingle.MarketValue = dr0["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["MarketValue"]);
                                        rSingle.Unrealised = dr0["Unrealised"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unrealised"]);
                                        //rSingle.PercentOfNav = dr0["PercentOfNav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentOfNav"]);
                                        rSingle.PercentOfNav = dr0["PercentOfNav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentOfNav"]);
                                        rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                        rSingle.PeriodeActual = Convert.ToString(dr0["PeriodeActual"]);
                                        rSingle.AccrualHarian = dr0["AccrualHarian"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualHarian"]);
                                        rSingle.Accrual = dr0["Accrual"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Accrual"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                    from r in rList
                                                    orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                                    group r by new { r.FundName, r.InstrumentTypeName, r.Date, r.InstrumentTypePK } into rGroup
                                                    select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "FUND : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "INSTRUMENT TYPE : ";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.InstrumentTypeName;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "DATE : ";
                                        worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "INS. NAME";
                                            worksheet.Cells[incRowExcel, 5].Value = "AVG PRICE";
                                            worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 6].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 7].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 8].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 9].Value = "UNREALISED";
                                            worksheet.Cells[incRowExcel, 10].Value = "(%) of NAV";

                                        }
                                        else if (rsHeader.Key.InstrumentTypePK == 5)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "INS. NAME";
                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL AMOUNT";
                                            worksheet.Cells[incRowExcel, 5].Value = "MATURITY DATE";
                                            worksheet.Cells[incRowExcel, 6].Value = "INTEREST %";
                                            worksheet.Cells[incRowExcel, 7].Value = "PERIODE ACCRUAL";
                                            worksheet.Cells[incRowExcel, 8].Value = "ACCRUAL HARIAN";
                                            worksheet.Cells[incRowExcel, 9].Value = "BUNGA YANG DI ACCRUAL";
                                            worksheet.Cells[incRowExcel, 10].Value = "(%) of NAV";

                                        }
                                        else
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "INS. NAME";
                                            worksheet.Cells[incRowExcel, 4].Value = "NOMINAL FACE VALUE";
                                            worksheet.Cells[incRowExcel, 5].Value = "AVG COST";
                                            worksheet.Cells[incRowExcel, 6].Value = "COST (IDR)";
                                            worksheet.Cells[incRowExcel, 7].Value = "TERM OF INTEREST";
                                            worksheet.Cells[incRowExcel, 8].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 9].Value = "UNREALISED";
                                            worksheet.Cells[incRowExcel, 10].Value = "(%) of NAV";
                                        }
                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":J" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            if (rsDetail.InstrumentTypePK == 1 || rsDetail.InstrumentTypePK == 4 || rsDetail.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,####0.00";

                                            }
                                            else if (rsDetail.InstrumentTypePK == 5)
                                            {

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.PeriodeActual;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.AccrualHarian;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Accrual;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Accrual;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,####0.00";
                                            }
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.PercentOfNav;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,####0.00";
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

                                        worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                        }
                                        else if (rsHeader.Key.InstrumentTypePK == 5)
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();

                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
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
                                    worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 FUND PORTFOLIO";



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

        public bool CheckOMS_EndDayTrailsFundPortfolio(DateTime _valueDate)
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
                        Declare @MaxDateEndDayFP datetime
                        select @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate <= @ValueDate
                        )
                        and status = 2

                        IF (@ValueDate = @MaxDateEndDayFP)
                        BEGIN 
	                        select 1 Result
                        END
                        ELSE 
                        BEGIN
	                        select 0 Result
                        END
                                  ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool CheckOMS_EndDayTrailsFundPortfolioWithParamFund(DateTime _valueDate, int _fundPK)
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
                        Declare @MaxDateEndDayFP datetime
                        select @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate <= @ValueDate and FundPK = @FundPK
                        )
                        and status = 2 and FundPK = @FundPK

                        IF (@ValueDate = @MaxDateEndDayFP)
                        BEGIN 
	                        select 1 Result
                        END
                        ELSE 
                        BEGIN
	                        select 0 Result
                        END
                                  ";

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

        public int EndDayTrailsFundPortfolio_GenerateWithParamFund(string _usersID, DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
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
                            _paramFund = "And FundPK in ( " + _edt.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
--testing dari sql
--declare @ValueDate date
--declare @UsersID nvarchar(20)
--declare @LastUpdate datetime
--declare @ClientCode nvarchar(20)

--set @ValueDate = '2020-01-30'
--set @UsersID = 'admin'
--set @lastupdate = getdate()
--set @ClientCode = '20'


--drop table #ZFundPosition
--drop table #ZLogicFundPosition
--drop table #ZDividenSaham
--drop table #StaticClosePrice
--drop table #ZFundFrom

Declare @CFundPK int
declare @EndDayTrailsFundPortfolioPK int
Create Table #ZFundPosition                  
(                  
InstrumentPK int,     
InstrumentTypePK int,                  
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
FundPK int,                  
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
AvgPrice numeric(38,12),                  
LastVolume numeric(38,4),                  
ClosePrice numeric(38,12),                  
TrxAmount numeric(38,6),              
AcqDate datetime,              
--MaturityDate datetime,              
--InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
BitBreakable bit
)                  

CREATE CLUSTERED INDEX indx_ZFundPosition ON #ZFundPosition (FundPK,InstrumentPK,InstrumentTypePK,BankPK,BankBranchPK);
    
Create Table #ZLogicFundPosition              
(              
BuyVolume numeric(38,4),              
SellVolume numeric(38,4),              
BuyAmount numeric(38,4),       
SellAmount numeric(38,4),            
FundPK int,              
InstrumentPK int,              
SettlementDate datetime,              
--MaturityDate datetime,              
--InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
AcqDate datetime,
BitBreakable bit
)      

CREATE CLUSTERED INDEX indx_ZLogicFundPosition ON #ZLogicFundPosition (FundPK,InstrumentPK,BankPK,BankBranchPK);

Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)      

CREATE CLUSTERED INDEX indx_ZDividenSaham ON #ZDividenSaham (FundPK,InstrumentPK);

Create table #StaticClosePrice
(
	FundPK int,
	InstrumentPK int,
	maxDate datetime
)

CREATE CLUSTERED INDEX indx_StaticClosePrice ON #StaticClosePrice (FundPK,InstrumentPK);

Create Table #ZFundFrom                  
(                   
	FundPK int,
	EndDayTrailsFundPortfolioPK int
)      

CREATE CLUSTERED INDEX indx_ZFundFrom  ON #ZFundFrom (FundPK,EndDayTrailsFundPortfolioPK);


Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioPK int       

                
Select @EndDayTrailsFundPortfolioPK = max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio    
set @EndDayTrailsFundPortfolioPK = isnull(@EndDayTrailsFundPortfolioPK,1)     


insert into #ZFundFrom(FundPK,EndDayTrailsFundPortfolioPK)
--select FundPK from Fund where status in (1,2)  " + _paramFund + @" and MaturityDate >= @ValueDate
--select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioPK from Fund where status in (1,2) and MaturityDate >= @ValueDate
select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioPK from Fund where status in (1,2) " + _paramFund + @" and (MaturityDate >= @ValueDate or MaturityDate = '01/01/1900')
--PARAM FUND



update FundPosition set status = 3,LastUpdate=@lastUpdate where Date = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)
update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @UsersID,VoidTime = @lastUpdate,LastUpdate=@lastUpdate
where ValueDate = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)          

UPDATE Investment set PriceMode = 1 where InstrumentTypePK  in (1,4,16)  and ValueDate = @ValueDate
update Investment set MarketPK = 1  where ValueDate = @ValueDate
update Investment set Category = null where InstrumentTypePK  <> 5  and ValueDate = @ValueDate
        
Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo  

Insert into EndDayTrailsFundPortfolio  (EndDayTrailsFundPortfolioPK,HistoryPK,Status,ValueDate,FundPK,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select EndDayTrailsFundPortfolioPK,1,2,@ValueDate,FundPK,0
,'',@UsersID,@LastUpdate,@LastUpdate  
from #ZFundFrom A     

      
                             

-- TARIK JUAL BELI DARI INVESTMENT              
Insert into #ZLogicFundPosition	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable)               

Select SUM(BuyVolume) BuyVolume,SUM(SellVolume) SellVolume,SUM(BuyAmount) BuyAmount,SUM(SellAmount) SellAmount,B.FundPK,B.InstrumentPK,B.SettlementDate,
B.CurrencyPK,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate
,B.BankPK,B.BankBranchPK,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable
From               
(               
Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
isnull(A.SettlementDate,'') SettlementDate,
isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.TaxExpensePercent,0) TaxExpensePercent,isnull(A.MarketPK,0) MarketPK,
isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
A.AcqDate,A.BitBreakable
from (                 
	
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type in (1,2,5,14) then null else AcqDate end SettlementDate,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.SettlementDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK <> 6 and A.FundPK in (select FundPK from #ZFundFrom)         
Group By A.InstrumentPK,FundPK,SettlementDate
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type in (1,2,5,14) then null else AcqDate end SettlementDate,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK <> 6  and A.FundPK in (select FundPK from #ZFundFrom)                     
Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			
UNION ALL

select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type = 1 then null else SettlementDate end SettlementDate,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
	,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.FundPK in (select FundPK from #ZFundFrom)              
Group By A.InstrumentPK,FundPK,SettlementDate
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK

-- REKSDANA--
UNION ALL
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type in (1,4) then null else AcqDate end SettlementDate,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)           
Group By A.InstrumentPK,FundPK,SettlementDate
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type in (1,4) then null else AcqDate end SettlementDate,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)                
Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
	

)A                
Group By A.InstrumentPK,A.FundPK,A.SettlementDate
,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.CurrencyPK
,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable



--INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,             
isnull(A.BuyAmount,0) - (case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0)	TrxAmount
,A.AcqDate,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType
,A.PriceMode,A.BitIsAmortized
From #ZLogicFundPosition A              
Left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2                
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2      
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK  
and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')                
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (1,2,4,5,14,9)



-- INSERT INVESTMENT + BEG BALANCE DEPOSITO ONLY ( INVESTMENT + BEG BALANCE )              
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
select InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,avg(AvgPrice)
,sum(LastVolume),avg(ClosePrice),TrxAmount,AcqDate
,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable
from (
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID InstrumentID,A.FundPK,C.ID FundID, 
1 AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
1 ClosePrice,                  
isnull(A.BuyAmount,0) - (1 * isnull(A.SellVolume,0))  + isnull		(B.TrxAmount,0) TrxAmount,              
A.AcqDate,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when D.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From #ZLogicFundPosition A              
LEft join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2     
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK 
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (3)
)A  
group by InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,TrxAmount,AcqDate
,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end AvgPrice,              
isnull(A.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,                  
isnull(A.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900'),C.CurrencyPK, A.Category,isnull(A.TaxExpensePercent,0),A.MarketPK
,A.PriceMode,A.BitIsAmortized,isnull(C.InterestDaysType,2),isnull(C.InterestPaymentType,1),Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2              
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
left join Fund D on A.FundPK = D.FundPK and D.status = 2          
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZFundPosition B on A.InstrumentPK = B.InstrumentPK 
and A.FundPK = B.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')              
where A.PeriodPK = @PeriodPK              
) and E.Type in (1,2,4,5,14,9) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)        
 

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT DEPOSITO ONLY             
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 1 AvgPrice,isnull(A.Volume,0) LastVolume,                  
1 ClosePrice, isnull(A.TrxAmount,0) TrxAmount,              
A.AcqDate,C.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.paymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2     
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2             
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZLogicFundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK          
where A.PeriodPK = @PeriodPK             
) and E.Type in (3) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)          


-- CORPORATE ACTION DIVIDEN SAHAM

-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	                    
delete CorporateActionResult where Date >= @ValueDate and FundPK in (select FundPK from #ZFundFrom)  


-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null and C.FundPK in (select FundPK from #ZFundFrom) 
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null and C.FundPK in (select FundPK from #ZFundFrom) 
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION DIVIDEN RIGHTS
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null  and C.FundPK in (select FundPK from #ZFundFrom) 

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-R' and D.status = 2
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION DIVIDEN WARRANT
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null  and C.FundPK in (select FundPK from #ZFundFrom) 

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price,0 from Exercise 
where DistributionDate  = @ValueDate and status = 2  and FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION BOND AMORTIZEN
TRUNCATE TABLE #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
--B.Balance + C.BalanceFromInv LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK  in (2,3,9,15)
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- UPDATE POSISI ZFUNDPOSITION + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
select FundPK,A.InstrumentPK,Price, sum(Balance) Balance,A.status
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
and B.ID not like '%-W' and B.ID not like '%-R'
WHERE A.Date <= @ValueDate and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom) 

Group By FundPK,A.InstrumentPK,Price,A.status
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
left join instrumentType C on A.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
left join Fund D on A.FundPK = D.FundPK and D.Status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where C.Type in (1,9,2,5,14)
--AND A.LastVolume > 0  
and A.FundPK = B.FundPK


                
-- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK RIGHTS/WARRANT
IF NOT EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
)
BEGIN
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
A.Price * BalanceExercise,null,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2  and A.FundPK in (select FundPK from #ZFundFrom) 
END




---- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK DISTRIBUTED DATE
--IF NOT EXISTS
--(
--Select * from #ZFundPosition A 
--left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
--where DistributionDate  = @ValueDate and status = 2
--)
--BEGIN
--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
--TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
--Select A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
--left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
--left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
--where DistributionDate  <= @ValueDate and A.status = 2
--END


--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
--tambahan boris untuk transaksi yg uda dijual sebagian
left join #ZFundPosition D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
where A.status = 2 and B.ID like '%-W' and PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom) and D.FundPK is null


Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,4,B.ID,A.FundPK,C.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
--tambahan boris untuk transaksi yg uda dijual sebagian
left join #ZFundPosition D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
where A.status = 2 and B.ID like '%-R' and PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom) and D.FundPK is null




                       
-- UPDATE POSISI ZFUNDPOSITION + FUND POSITION ADJUSTMENT
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
SELECT A.FundPK,A.InstrumentPK,case when sum(balance) = 0 then 0 else sum(Price*Balance) / SUM(balance) end Price, sum(Balance) Balance,A.status,
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  THEN A.AcqDate ELSE NULL END AcqDate
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
WHERE A.status = 2 AND A.Date <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
Group By A.FundPK,A.InstrumentPK,A.status,B.InstrumentTypePK,A.AcqDate
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
AND isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900') and A.FundPK in (select FundPK from #ZFundFrom) 
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2



--INSERT INSTRUMENT YANG ADA DI FUND POSITION ADJUSTMENT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  

SELECT A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
SUM(A.Balance),dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
SUM(Balance*Price),
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  then  A.AcqDate else null End ,
B.CurrencyPK,NULL,B.TaxExpensePercent,B.MarketPK
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join BankBranch B1 on C.BankBranchPK = B1.BankBranchPK and B1.Status = 2
left join Bank B2 on B1.BankPK = B2.BankPK and B2.Status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where A.status = 2 AND A.AcqDate <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
and NOT EXISTS 
(SELECT * FROM #ZFundPosition C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(C.AcqDate,'01/01/1900'))
GROUP BY A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,
B.CurrencyPK,B.TaxExpensePercent,B.MarketPK,A.AcqDate,B2.BankPK,F.AveragePriority



-- DELETE RIGHTS AND WARRANT YANG EXPIRED
Delete A From #ZFundPosition A
Inner join 
(
	Select C.InstrumentPK,A.ExDate AcqDate from CorporateAction A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join Instrument C on C.ID = B.ID + '-R' and C.status = 2
	where ExpiredDate <= @ValueDate and A.Status = 2 and A.Type = 3
)B on A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate

Delete A From #ZFundPosition A
Inner join 
(
	Select C.InstrumentPK,A.ExDate AcqDate from CorporateAction A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
	left join Instrument C on C.ID = B.ID + '-W' and C.status = 2
	where ExpiredDate <= @ValueDate and A.Status = 2 and A.Type = 5
)B on A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate

-- KURANGIN BALANCE WARRANT AND RIGHTS YANG ADA DI EXERCISE


IF  EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2 and A.FundPK in (select FundPK from #ZFundFrom) 
)
BEGIN
Update A set A.LastVolume = A.LastVolume - isnull(B.BalanceExercise,0) from #ZFundPosition A
left join Exercise B on A.InstrumentPK = B.InstrumentRightsPK and B.status = 2
where Date = @ValueDate and A.FundPK in (select FundPK from #ZFundFrom) 
END
 

Insert into FundPosition(FundPositionPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable)                  
Select C.EndDayTrailsFundPortfolioPK,C.EndDayTrailsFundPortfolioPK,1,2,'',@ValueDate,A.FundPK, FundID,                  
A.InstrumentPK,InstrumentID,CONVERT(decimal(18,12),AvgPrice),LastVolume
,case when A.InstrumentTypePK in (2,3,8,14,13,9,15)  then CONVERT(decimal(18,12),AvgPrice)/100 else CONVERT(decimal(18,12),AvgPrice) End * LastVolume CostValue
, ClosePrice,TrxAmount
,case when A.InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
AcqDate,D.MaturityDate,D.InterestPercent,A.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,isnull(A.InterestDaysType,0),isnull(A.InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(A.BankPK,0),isnull(A.BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
From #ZFundPosition  A WITH (NOLOCK)
left join Fund B on A.FundPK = B.FundPK
inner join #ZFundFrom C on A.FundPK = C.FundPK
left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
where A.LastVolume > 0 and B.status in (1,2)  and A.FundPK in (select FundPK from #ZFundFrom) 
  

Delete FP From FundPosition FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
left join #ZFundFrom B on FP.FundPK = B.FundPK 
Where FundPositionPK = B.EndDayTrailsFundPortfolioPK and I.InstrumentTypePK not in (1,4,6,16)
and FP.MaturityDate <= @ValueDate and FP.MaturityDate Is Not Null  and FP.FundPK in (select FundPK from #ZFundFrom)  


---------PROSES AMORTIZED DAN PRICE MODE------------------------------
update A set A.ClosePrice =  Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
			when A.PriceMode = 2 then LowPriceValue
				when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
		else  
			dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			 
		end 
, A.MarketValue = A.Balance * Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
			when A.PriceMode = 2 then LowPriceValue
				when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
		else  
			dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			  
		end / Case when D.InstrumentTypePK in (2,3,8,14,13,9,15)  then 100 else 1 end
from FundPosition A 
left join 
(
	select InstrumentPK,LowPriceValue,ClosePriceValue,HighPriceValue From ClosePrice where Date =
	(
		Select max(Date) From ClosePrice where date <= @ValueDate and status = 2
	) and status = 2
)B on A.InstrumentPK = B.InstrumentPK 
left join instrument C on A.InstrumentPK = C.instrumentPK and C.Status = 2
left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.FundPK in (select FundPK from #ZFundFrom)  and Date = @ValueDate and A.status = 2

-- STATIC CLOSEPRICE

insert into #StaticClosePrice
select distinct FundPK,instrumentPK,max(Date) maxDate from UpdateClosePrice 
where Date <= @ValueDate and status = 2 
and fundPK in
(
	Select FundPK from #ZFundFrom
)
group by FundPK,InstrumentPK
order by FundPK,InstrumentPK asc



if (@ClientCode = '05')
BEGIN
		Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) 
		then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
		INNER join (
				
            Select A.FundPK,A.InstrumentPK,B.ClosePriceValue ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
            left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
            where  A.maxDate = @ValueDate

		)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK 
		inner join #ZFundFrom C on A.FundPK = C.FundPK
		where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioPK
		and A.Status = 2
END
ELSE
BEGIN
		Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) 
		then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
		INNER join (
				
            Select A.FundPK,A.InstrumentPK,B.ClosePriceValue ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
            left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
            where isnull(B.ClosePriceValue,0) >= 0 and A.maxDate = @ValueDate

		)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK 
		inner join #ZFundFrom C on A.FundPK = C.FundPK
		where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioPK
		and A.Status = 2
END

-- update TrxBuy di Investment untuk Sell / Rollover

declare @DTrxBuy int
declare @DInvestmentPK int
declare @DInstrumentPK int
declare @DFundPK int
declare @DDate datetime
declare @DNewIdentity bigint
declare @DAcqDate datetime

DECLARE C CURSOR FOR 
select TrxBuy,InvestmentPK,B.InstrumentPK,B.FundPK,B.Date,B.AcqDate from Investment A
left join FundPosition B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK not in (1,4,16) and StatusInvestment in (1,2) and TrxType in (2,3)
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate  
While @@FETCH_STATUS = 0
BEGIN   

set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FundPosition where InstrumentPK = @DInstrumentPK and FundPK = @DFundPK and Date = @DDate and AcqDate = @DAcqDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusInvestment in (1,2)


Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate                  
END
Close C
Deallocate C  


	
Update A set BitValidate = 1, LogMessages = B.ID + ' - ' + B.Name from EndDayTrailsFundPortfolio A  
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where A.FundPK in (select FundPK from #ZFundFrom)  and A.Status = 2 and ValueDate = @ValueDate       


Select (select max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio) LastPK
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

        public EndDayTrailsFundPortfolio ValidateGenerateEndDayTrailsFundPortfolioParamFund(DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
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
                        if (!_host.findString(_edt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _edt.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"
                        Declare @CFundPK int                    

                        Declare @A Table (Result int, Notes nvarchar(500))

                        DECLARE A CURSOR FOR

                        select distinct FundPK from Fund where status in (1,2) and MaturityDate >= @ValueDate " + _paramFund + @"

                        Open A
                        Fetch Next From A
                        Into @CFundPK

                        While @@FETCH_STATUS = 0
                        Begin


                        -- FUND LAMA
	                    IF NOT EXISTS(select * From EndDayTrailsFundPortfolio A
	                    left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	                    where A.Status in (1,2) and ValueDate = @ValueDate and A.FundPK = @CFundPK ) 
	                    BEGIN 
		                    insert into @A
		                    Select 1 Result,'Please Check EndDayTrails FundPortfolio, Fund : ' + ID Notes from Fund where FundPk = @CFundPK and status in (1,2)
	                    END 
	                    ELSE 
	                    BEGIN 
		                    insert into @A
		                    Select 0 Result , '' Notes
	                    END
                        
                        Fetch next From A 
                        Into @CFundPK
                        end
                        Close A
                        Deallocate A

                        select top 1 Result,Notes from @A
                        order by Result desc   ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new EndDayTrailsFundPortfolio()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Notes = dr["Notes"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Notes"]),
                                };
                            }
                            else
                            {
                                return new EndDayTrailsFundPortfolio()
                                {
                                    Result = 0,
                                    Notes = "",

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
        
        public List<UpdateEndDayTrailsFundPortfolio> Get_DataForUpdateFundPosition(int _fundPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UpdateEndDayTrailsFundPortfolio> L_EndDayTrailsFundPortfolio = new List<UpdateEndDayTrailsFundPortfolio>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        select A.FundPK,A.InstrumentPK,B.ID InstrumentID,B.Name InstrumentName,A.MaturityDate,A.Balance,A.AcqDate,A.InterestPercent,
                        A.Category,MV1.DescOne InterestDaysTypeDesc,MV2.DescOne InterestPaymentTypeDesc,MV3.DescOne PaymentModeOnMaturityDesc from FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        left join MasterValue MV1 on A.InterestDaysType = MV1.Code and MV1.status in (1,2) and MV1.ID = 'InterestDaysType'
                        left join MasterValue MV2 on A.InterestPaymentType = MV2.Code and MV2.status in (1,2) and MV2.ID = 'InterestPaymentType'
                        left join MasterValue MV3 on A.PaymentModeOnMaturity = MV3.Code and MV3.status in (1,2) and MV3.ID = 'PaymentModeOnMaturity'
                        where Date = @Date and FundPK = @FundPK and A.status = 2 and B.InstrumentTypePK not in (1,4,16)
                        order by A.AcqDate,A.MaturityDate asc";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndDayTrailsFundPortfolio.Add(setUpdateEndDayTrailsFundPortfolio(dr));
                                }
                            }
                            return L_EndDayTrailsFundPortfolio;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }
        
        private UpdateEndDayTrailsFundPortfolio setUpdateEndDayTrailsFundPortfolio(SqlDataReader dr)
        {
            UpdateEndDayTrailsFundPortfolio M_UpdateEndDayTrailsFundPortfolio = new UpdateEndDayTrailsFundPortfolio();
            M_UpdateEndDayTrailsFundPortfolio.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_UpdateEndDayTrailsFundPortfolio.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_UpdateEndDayTrailsFundPortfolio.InstrumentID = dr["InstrumentID"].ToString();
            M_UpdateEndDayTrailsFundPortfolio.InstrumentName = dr["InstrumentName"].ToString();
            M_UpdateEndDayTrailsFundPortfolio.MaturityDate = Convert.ToString(dr["MaturityDate"]);
            M_UpdateEndDayTrailsFundPortfolio.Balance = Convert.ToDecimal(dr["Balance"]);
            M_UpdateEndDayTrailsFundPortfolio.AcqDate = Convert.ToString(dr["AcqDate"]);
            M_UpdateEndDayTrailsFundPortfolio.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
            M_UpdateEndDayTrailsFundPortfolio.Category = dr["Category"].ToString();
            M_UpdateEndDayTrailsFundPortfolio.InterestDaysTypeDesc = dr["InterestDaysTypeDesc"].ToString();
            M_UpdateEndDayTrailsFundPortfolio.InterestPaymentTypeDesc = dr["InterestPaymentTypeDesc"].ToString();
            M_UpdateEndDayTrailsFundPortfolio.PaymentModeOnMaturityDesc = dr["PaymentModeOnMaturityDesc"].ToString();
   
            return M_UpdateEndDayTrailsFundPortfolio;
        }

        // SEMENTARA CAM
        public int Update_DataFundPositionForCAM(UpdateEndDayTrailsFundPortfolio _updateEndDayTrailsFundPortfolio)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        update Instrument set MaturityDate = @MaturityDate,InterestPercent = @InterestPercent,UpdateUsersID = @UpdateUsersID where status = 2 and InstrumentPK = @InstrumentPK
                        update Investment set MaturityDate = @MaturityDate,InterestPercent = @InterestPercent,UpdateUsersID = @UpdateUsersID where StatusSettlement = 2 and InstrumentPK = @InstrumentPK
                        update FundPosition set  MaturityDate = @MaturityDate,InterestPercent = @InterestPercent,UpdateUsersID = @UpdateUsersID where status = 2 and InstrumentPK = @InstrumentPK
                        update FundEndYearPortfolio set  MaturityDate = @MaturityDate,InterestPercent = @InterestPercent where  InstrumentPK = @InstrumentPK ";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _updateEndDayTrailsFundPortfolio.InstrumentPK);
                        cmd.Parameters.AddWithValue("@MaturityDate", _updateEndDayTrailsFundPortfolio.MaturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _updateEndDayTrailsFundPortfolio.InterestPercent);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _updateEndDayTrailsFundPortfolio.UpdateUsersID);


                        cmd.ExecuteNonQuery();
                    }
                    return 0;


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Update_DataFundPosition(UpdateEndDayTrailsFundPortfolio _updateEndDayTrailsFundPortfolio)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"


                        declare @historyPK int
                        declare @NewHistoryPK int
                        declare @LastFPDate date

                        select @historyPK = historyPK from Instrument where instrumentpk = @InstrumentPK and status = 2
                        set @NewHistoryPK = @historyPK + 1


                        INSERT INTO [dbo].[Instrument] 
                        ([InstrumentPK],[HistoryPK],[Status],[ID],[Name],[Category],[Affiliated],[InstrumentTypePK]
                        ,[ReksadanaTypePK],[DepositoTypePK],[ISIN],[BankPK],[IssuerPK],[SectorPK],[HoldingPK]
                        ,[MarketPK],[IssueDate],[MaturityDate],[InterestPercent],[InterestPaymentType],[InterestType]
                        ,[LotInShare],[BitIsSuspend],[CurrencyPK],[RegulatorHaircut],[Liquidity],[NAWCHaircut]
                        ,[CompanyHaircut],[BondRating],[BitIsShortSell],[BitIsMargin],[BitIsScriptless],[TaxExpensePercent]
                        ,[InterestDaysType],[BloombergCode],[BitIsForeign],[FirstCouponDate],[CounterpartPK],[BankAccountNo],[SAPCustID],[InstrumentCompanyTypePK],[AnotherRating],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])
                        Select @InstrumentPK,@NewHistoryPK,2,ID,Name,Category,Affiliated,InstrumentTypePK
                        ,ReksadanaTypePK,DepositoTypePK,ISIN,BankPK,IssuerPK,SectorPK,HoldingPK
                        ,MarketPK,IssueDate,@MaturityDate,@InterestPercent,InterestPaymentType,InterestType
                        ,LotInShare,BitIsSuspend,CurrencyPK,RegulatorHaircut,Liquidity,NAWCHaircut
                        ,CompanyHaircut,BondRating,BitIsShortSell,BitIsMargin,BitIsScriptless,TaxExpensePercent
                        ,InterestDaysType,BloombergCode,BitIsForeign,FirstCouponDate,CounterpartPK,BankAccountNo,SAPCustID,InstrumentCompanyTypePK,AnotherRating,EntryUsersID,EntryTime,@UpdateUsersID,@LastUpdate,@LastUpdate
                        From Instrument where InstrumentPK = @InstrumentPK and historyPK = @HistoryPK

                        update instrument set status = 3 where instrumentpk = @InstrumentPK and HistoryPK = @historyPK
                        update Investment set MaturityDate = @MaturityDate,InterestPercent = @InterestPercent,UpdateUsersID = @UpdateUsersID where StatusSettlement = 2 and InstrumentPK = @InstrumentPK

                        select @LastFPDate = max(date) from FundPosition where status = 2 and instrumentpk = @InstrumentPK

                        update FundPosition set  MaturityDate = @MaturityDate,InterestPercent = @InterestPercent,UpdateUsersID = @UpdateUsersID where status = 2 and InstrumentPK = @InstrumentPK and date between @ValueDate and @LastFPDate
                        update FundEndYearPortfolio set  MaturityDate = @MaturityDate,InterestPercent = @InterestPercent where  InstrumentPK = @InstrumentPK ";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _updateEndDayTrailsFundPortfolio.InstrumentPK);
                        cmd.Parameters.AddWithValue("@MaturityDate", _updateEndDayTrailsFundPortfolio.MaturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _updateEndDayTrailsFundPortfolio.InterestPercent);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _updateEndDayTrailsFundPortfolio.UpdateUsersID);
                        cmd.Parameters.AddWithValue("@ValueDate", _updateEndDayTrailsFundPortfolio.ValueDate);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);


                        cmd.ExecuteNonQuery();
                    }
                    return 0;


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string Validate_StatusSettlement(DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
        {
            try
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

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                    declare @msg nvarchar(max)


                    
                       if EXISTS(select * From Investment A where A.StatusSettlement =  1 and A.StatusDealing  = 2 and A.StatusInvestment = 2 and A.ValueDate = @ValueDate " + _paramFund + @" 
					   )
                        begin
							SELECT @msg = COALESCE(@msg + ', ', '') + B.ID
							FROM Investment A
							left join Fund B on A.Fundpk = B.FundPK and B.Status in (1,2)
							where StatusSettlement =  1 and StatusDealing  = 2 and StatusInvestment = 2 and ValueDate = @ValueDate " + _paramFund + @" 

							set @msg = 'Fund : ' + @msg
                        end
					

						select isnull(@msg,'') Result
                  ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        //bagian fifo
        public int EndDayTrailsFundPortfolioFifoBond_GenerateWithParamFund(string _usersID, DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
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
                            _paramFund = "And FundPK in ( " + _edt.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

--declare @ValueDate date
--declare @ClientCode nvarchar(100)
--declare @UsersID nvarchar(100)
--declare @LastUpdate datetime

--set @ValueDate = '2020-09-23'
--set @ClientCode = '20'
--set @UsersID = 'admin'
--set @LastUpdate = getdate()

Declare @ValueDate datetime    
set @ValueDate = @paramValuedate  


Declare @CFundPK int
declare @EndDayTrailsFundPortfolioPK int

if object_id('tempdb..#ZFundPosition', 'u') is not null drop table #ZFundPosition 
Create Table #ZFundPosition                  
(                  
InstrumentPK int,     
InstrumentTypePK int,                  
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
FundPK int,                  
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
AvgPrice numeric(38,12),                  
LastVolume numeric(38,4),                  
ClosePrice numeric(38,12),                  
TrxAmount numeric(38,6),              
AcqDate datetime,              
--MaturityDate datetime,              
--InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
BitBreakable bit
)                  
CREATE CLUSTERED INDEX indx_ZFundPosition ON #ZFundPosition (FundPK,InstrumentPK,InstrumentTypePK,BankPK,BankBranchPK);
    

if object_id('tempdb..#ZLogicFundPosition', 'u') is not null drop table #ZLogicFundPosition 
Create Table #ZLogicFundPosition              
(              
BuyVolume numeric(38,4),              
SellVolume numeric(38,4),              
BuyAmount numeric(38,4),       
SellAmount numeric(38,4),            
FundPK int,              
InstrumentPK int,              
SettlementDate datetime,              
--MaturityDate datetime,              
--InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
AcqDate datetime,
BitBreakable bit
)      

CREATE CLUSTERED INDEX indx_ZLogicFundPosition ON #ZLogicFundPosition (FundPK,InstrumentPK,BankPK,BankBranchPK);


if object_id('tempdb..#ZDividenSaham', 'u') is not null drop table #ZDividenSaham
Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)      

CREATE CLUSTERED INDEX indx_ZDividenSaham ON #ZDividenSaham (FundPK,InstrumentPK);


if object_id('tempdb..#StaticClosePrice', 'u') is not null drop table #StaticClosePrice
Create table #StaticClosePrice
(
	FundPK int,
	InstrumentPK int,
	maxDate datetime
)

CREATE CLUSTERED INDEX indx_StaticClosePrice ON #StaticClosePrice (FundPK,InstrumentPK);

if object_id('tempdb..#ZFundFrom', 'u') is not null drop table #ZFundFrom
Create Table #ZFundFrom                  
(                   
	FundPK int,
	EndDayTrailsFundPortfolioPK int
)      

CREATE CLUSTERED INDEX indx_ZFundFrom  ON #ZFundFrom (FundPK,EndDayTrailsFundPortfolioPK);

if object_id('tempdb..#ZInstrument', 'u') is not null drop table #ZInstrument
Create Table #ZInstrument                  
(                   
	InstrumentPK int,
	FundPK int
)      

CREATE CLUSTERED INDEX indx_ZInstrument  ON #ZInstrument (InstrumentPK,FundPK);

if object_id('tempdb..#ZInstrumentFIFO', 'u') is not null drop table #ZInstrumentFIFO
Create Table #ZInstrumentFIFO                  
(                   
	InstrumentPK int,
	FundPK int
)      

CREATE CLUSTERED INDEX indx_ZInstrumentFIFO  ON #ZInstrumentFIFO (InstrumentPK,FundPK);

if object_id('tempdb..#FifoBondTrx', 'u') is not null drop table #FifoBondTrx 
create table #FifoBondTrx
(
	BuyVolume numeric(38,4),              
	SellVolume numeric(38,4),              
	BuyAmount numeric(38,4),       
	SellAmount numeric(38,4),            
	FundPK int,              
	InstrumentPK int,             
	ValueDate datetime,       
	SettlementDate datetime,              
	--MaturityDate datetime,              
	--InterestPercent numeric(38,8),
	CurrencyPK int,
	Category nvarchar(200) COLLATE DATABASE_DEFAULT,
	TaxExpensePercent numeric(19, 8),
	MarketPK int,
	InterestDaysType int,
	InterestPaymentType int,
	PaymentModeOnMaturity   int,
	PaymentInterestSpecificDate datetime,
	BankPK int,
	BankBranchPK int,
	PriceMode int,
	BitIsAmortized bit,
	AcqDate datetime,
	BitBreakable BIT
)
CREATE CLUSTERED INDEX indx_FifoBondTrx ON #FifoBondTrx (InstrumentPK,FundPK,AcqDate);

Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioPK int       

                
Select @EndDayTrailsFundPortfolioPK = max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio    
set @EndDayTrailsFundPortfolioPK = isnull(@EndDayTrailsFundPortfolioPK,1)     


insert into #ZFundFrom(FundPK,EndDayTrailsFundPortfolioPK)
--select FundPK from Fund where status in (1,2)  " + _paramFund + @" and MaturityDate >= @ValueDate
--select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioPK from Fund where status in (1,2) and MaturityDate >= @ValueDate
select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioPK from Fund where status in (1,2) 
" + _paramFund + @" 
--and FundPk = 72
and MaturityDate >= @ValueDate
--PARAM FUND

--update fifobond ketika mature

declare @maxDate date
set @maxDate = dbo.FWorkingDay(@ValueDate,-1)

-- CEK MATURITY FIFO BOND
declare @maxFifoDate date
if @ClientCode = '03'
    set @maxFifoDate = '2019-10-18'
else if @ClientCode = '20'
    set @maxFifoDate = '2020-06-19'
else if @ClientCode = '21'
    set @maxFifoDate = '2020-09-30'

insert into #ZInstrument
select distinct A.InstrumentPK,A.FundPK from FundPosition A
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
where date = @maxDate and A.MaturityDate <= @ValueDate and InstrumentTypePK in (2,3,8,9,13,15)
and FundPk in (select FundPK from #ZFundFrom)

insert into #ZInstrumentFIFO
select distinct A.InstrumentPK,A.FundPK from FundPosition A
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
where date = @maxDate and A.MaturityDate > @ValueDate and InstrumentTypePK in (2,3,8,9,13,15) and A.status = 2
and FundPk in (select FundPK from #ZFundFrom)

--INSERT DATA INVESTMENT BARU
declare @FifoInvestmentBuyPK int
declare @FifoInvestmentSellPK int
declare @FifoAcqDate date
declare @FifoAcqVolume numeric(32,8)
declare @FifoAcqPrice numeric(18,8)
declare @FifoDoneVolume numeric(32,8)
declare @FifoFundPk int
declare @FifoInstrumentPK int
declare @FifoFundPositionAdjustmentPK int
declare @FifoVol numeric(32,8)
declare @FifoTempVol numeric(32,8)

DECLARE A CURSOR
FOR 

	select InvestmentPK InvestmentBuyPK,0 InvestmentSellPK,SettlementDate AcqDate,DoneVolume AcqVolume,DonePrice AcqPrice, DoneVolume, A.FundPK, A.InstrumentPK, 0 FundPositionAdjustmentPK from Investment A
	inner join #ZInstrumentFIFO B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
	where TrxType = 1 and StatusSettlement = 2 and ValueDate > @maxFifoDate and InvestmentPK not in (select InvestmentBuyPK from FifoBondPositionTemp )
 
OPEN A;
 
FETCH NEXT FROM A INTO @FifoInvestmentBuyPK,@FifoInvestmentSellPK,@FifoAcqDate,@FifoAcqVolume,@FifoAcqPrice,@FifoDoneVolume,@FifoFundPk,@FifoInstrumentPK,@FifoFundPositionAdjustmentPK
 
WHILE @@FETCH_STATUS = 0
    BEGIN
		
		select @FifoVol =  sum(DoneVolume) from Investment where TrxType = 1 and StatusSettlement = 2 and StatusInvestment = 2 and StatusDealing = 2 and 
		SettlementDate = @FifoAcqDate and DonePrice = @FifoAcqPrice and fundpk = @FifoFundPk and InstrumentPK = @FifoInstrumentPK

		select @FifoTempVol = sum(AcqVolume) from FifoBondPositionTemp where AcqDate = @FifoAcqDate and AcqPrice = @FifoAcqPrice and fundpk = @FifoFundPk and InstrumentPK = @FifoInstrumentPK

		if @FifoTempVol is null
		begin
			insert into FifoBondPositionTemp
			select @FifoInvestmentBuyPK,@FifoInvestmentSellPK,@FifoAcqDate,@FifoAcqVolume,@FifoAcqPrice,@FifoDoneVolume,@FifoFundPk,@FifoInstrumentPK,@FifoFundPositionAdjustmentPK
			--select 'insert'
		end
		else 
		begin
			if @FifoVol <> @FifoTempVol 
			begin
				update FifoBondPositionTemp set AcqVolume = AcqVolume + @FifoAcqVolume, RemainingVolume = RemainingVolume + @FifoAcqVolume, InvestmentBuyPK = @FifoInvestmentBuyPK
				where AcqDate = @FifoAcqDate and AcqPrice = @FifoAcqPrice and fundpk = @FifoFundPk and InstrumentPK = @FifoInstrumentPK
				--select @FifoAcqVolume
			end
		end

        FETCH NEXT FROM A INTO @FifoInvestmentBuyPK,@FifoInvestmentSellPK,@FifoAcqDate,@FifoAcqVolume,@FifoAcqPrice,@FifoDoneVolume,@FifoFundPk,@FifoInstrumentPK,@FifoFundPositionAdjustmentPK
    END;
 
CLOSE A;
 
DEALLOCATE A;

--UPDATE BOND MATURE

update A set RemainingVolume = 0 from FiFoBondPosition A
inner join (
select FiFoBondPositionPK from FiFoBondPosition A
inner join #ZInstrument B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK
where A.status in (1,2) and RemainingVolume is null or RemainingVolume != 0
) B on A.FiFoBondPositionPK = B.FiFoBondPositionPK

update A set RemainingVolume = 0 from FifoBondPositionTemp A
inner join (
select InvestmentBuyPK from FifoBondPositionTemp A
inner join #ZInstrument B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK
where RemainingVolume is null or RemainingVolume != 0
) B on A.InvestmentBuyPK = B.InvestmentBuyPK

update FundPosition set status = 3,LastUpdate=@lastUpdate where Date = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)
update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @UsersID,VoidTime = @lastUpdate,LastUpdate=@lastUpdate
where ValueDate = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)          

UPDATE Investment set PriceMode = 1 where InstrumentTypePK  in (1,4,16)  and ValueDate = @ValueDate
update Investment set MarketPK = 1  where ValueDate = @ValueDate
update Investment set Category = null where InstrumentTypePK  <> 5  and ValueDate = @ValueDate
        
Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo  

Insert into EndDayTrailsFundPortfolio  (EndDayTrailsFundPortfolioPK,HistoryPK,Status,ValueDate,FundPK,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select EndDayTrailsFundPortfolioPK,1,2,@ValueDate,FundPK,0
,'',@UsersID,@LastUpdate,@LastUpdate  
from #ZFundFrom A     

-- SETUP DATA SELL FIFO DARI INVESTMENT


Insert into #FifoBondTrx(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,CurrencyPK,Category,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,ValueDate)   

Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
isnull(A.SettlementDate,'') SettlementDate,
isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.MarketPK,0) MarketPK,
isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
A.AcqDate,A.BitBreakable,A.ValueDate
from (                 
	
		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,
		CASE when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,
		A.AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate IS NOT NULL AND A.AcqVolume > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume1) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate1 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate1 end AcqDate,
		CASE when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice1 AcqPrice

		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate1 IS NOT NULL AND A.AcqVolume1 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate1,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice1
			
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume2) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate2 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate2 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice2 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate2 IS NOT NULL AND A.AcqVolume2 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate2,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice2

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume3) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate3 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate3 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice3 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate3 IS NOT NULL AND A.AcqVolume3 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate3,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice3

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume4) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate4 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate4 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice4 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate4 IS NOT NULL AND A.AcqVolume4 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate4,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice4

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume5) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate5 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate5 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice5 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16) and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate5 IS NOT NULL AND A.AcqVolume5 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate5,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice5

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume6) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate6 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate6 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice6 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate6 IS NOT NULL AND A.AcqVolume6 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate6,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice6

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume7) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate7 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate7 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice7 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate7 IS NOT NULL AND A.AcqVolume7 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate7,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice7

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume8) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate8 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate8 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice8 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate8 IS NOT NULL AND A.AcqVolume8 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate8,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice8

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume9) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate9 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate9 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice9 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate9 IS NOT NULL AND A.AcqVolume9 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate9,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice9
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume10) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,10,14) then null else AcqDate10 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate10 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice10 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate10 IS NOT NULL AND A.AcqVolume10 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate10,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice10

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume11) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,11,14) then null else AcqDate11 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate11 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice11 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate11 IS NOT NULL AND A.AcqVolume11 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate11,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice11

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume12) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,12,14) then null else AcqDate12 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate12 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice12 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate12 IS NOT NULL AND A.AcqVolume12 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate12,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice12

		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume13) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,13,14) then null else AcqDate13 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate13 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice13 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate13 IS NOT NULL AND A.AcqVolume13 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate13,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice13

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume14) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,14,14) then null else AcqDate14 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate14 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice14 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate14 IS NOT NULL AND A.AcqVolume14 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate14,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice14
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume15) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,15,14) then null else AcqDate15 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate15 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice15 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate15 IS NOT NULL AND A.AcqVolume15 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate15,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice15
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume16) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,16,14) then null else AcqDate16 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate16 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice16 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate16 IS NOT NULL AND A.AcqVolume16 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate16,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice16
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume17) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,17,14) then null else AcqDate17 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate17 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice17 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate17 IS NOT NULL AND A.AcqVolume17 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate17,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice17

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume18) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,18,14) then null else AcqDate18 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate18 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice18 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate18 IS NOT NULL AND A.AcqVolume18 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate18,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice18

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume19) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,19,14) then null else AcqDate19 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate19 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice19 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate19 IS NOT NULL AND A.AcqVolume19 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate19,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice19

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume20) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,20,14) then null else AcqDate20 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate20 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice20 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate20 IS NOT NULL AND A.AcqVolume20 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate20,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice20

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume21) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,21,14) then null else AcqDate21 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate21 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice21 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate21 IS NOT NULL AND A.AcqVolume21 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate21,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice21

		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume22) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,22,14) then null else AcqDate22 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate22 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice22 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate22 IS NOT NULL AND A.AcqVolume22 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate22,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice22
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume23) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,23,14) then null else AcqDate23 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate23 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice23 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate23 IS NOT NULL AND A.AcqVolume23 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate23,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice23
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume24) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,24,14) then null else AcqDate24 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate24 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice24 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate24 IS NOT NULL AND A.AcqVolume24 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate24,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice24
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume25) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,25,14) then null else AcqDate25 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate25 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice25 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate25 IS NOT NULL AND A.AcqVolume25 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate25,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice25
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume26) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,26,14) then null else AcqDate26 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate26 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice26 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate26 IS NOT NULL AND A.AcqVolume26 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate26,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice26
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume27) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,27,14) then null else AcqDate27 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate27 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice27 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate27 IS NOT NULL AND A.AcqVolume27 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate27,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice27
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume28) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,28,14) then null else AcqDate28 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate28 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice28 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate28 IS NOT NULL AND A.AcqVolume28 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate28,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice28
		
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume29) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,29,14) then null else AcqDate29 end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate29 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
		A.AcqPrice29 AcqPrice
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where  ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK  not in (1,4,5,6,16)  and A.FundPK in (select FundPK from #ZFundFrom)
		AND (A.AcqDate29 IS NOT NULL AND A.AcqVolume29 > 0)
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate29,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		,A.AcqPrice29

		)A                
Group By A.InstrumentPK,A.FundPK,A.SettlementDate
,A.ValueDate,A.CurrencyPK ,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable,A.AcqPrice       
        
                             

-- TARIK JUAL BELI DARI INVESTMENT              
Insert into #ZLogicFundPosition	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,CurrencyPK,Category,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable)               

Select SUM(BuyVolume) BuyVolume,SUM(SellVolume) SellVolume,SUM(BuyAmount) BuyAmount,SUM(SellAmount) SellAmount,B.FundPK,B.InstrumentPK,B.SettlementDate,B.CurrencyPK,B.Category,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate
,B.BankPK,B.BankBranchPK,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable
From               
(               
	Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
	,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
	isnull(A.SettlementDate,'') SettlementDate,
	isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.MarketPK,0) MarketPK,
	isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
	,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
	A.AcqDate,A.BitBreakable
	from (                 
	
		select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
		Case when C.Type in (1,2,5,6,9,14) then null else AcqDate end SettlementDate,ValueDate,
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,case when B.InstrumentTypePK = 5 then 0 else A.PriceMode end PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.SettlementDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
		from Investment A 
		Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
		where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK <> 6 and A.FundPK in (select FundPK from #ZFundFrom)         
		Group By A.InstrumentPK,FundPK,SettlementDate
		,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
		UNION ALL                  

		select InstrumentPK,BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,SettlementDate,ValueDate,CurrencyPK,Category,
		MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable from #FifoBondTrx


        UNION ALL                  

        select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
        Case when C.Type = 1 then null else AcqDate end SettlementDate,ValueDate,        
        B.CurrencyPK,A.Category,A.MarketPK
        ,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
        ,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
        ,A.PaymentModeOnMaturity
        ,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
        ,case when B.InstrumentTypePK = 5 then 0 else A.PriceMode end,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
        from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
        where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK in (1,4,5,16)  and A.FundPK in (select FundPK from #ZFundFrom)                     
        Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
        ,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
        ,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
		

		UNION ALL

		select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
		Case when C.Type = 1 then null else SettlementDate end SettlementDate,ValueDate,
		B.CurrencyPK,A.Category,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK
		,case when B.InstrumentTypePK = 5 then 0 else A.PriceMode end,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
		where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.FundPK in (select FundPK from #ZFundFrom)              
		Group By A.InstrumentPK,FundPK,SettlementDate
		,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK

		-- REKSDANA--
		UNION ALL
		select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
		Case when C.Type in (1,4) then null else AcqDate end SettlementDate,ValueDate,
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,case when B.InstrumentTypePK = 5 then 0 else A.PriceMode end PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
		from Investment A 
		Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
		where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)           
		Group By A.InstrumentPK,FundPK,SettlementDate
		,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
		UNION ALL                  

		select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
		Case when C.Type in (1,4) then null else AcqDate end SettlementDate,ValueDate,        
		B.CurrencyPK,A.Category,A.MarketPK
		,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
		,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
		,A.PaymentModeOnMaturity
		,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
		,case when B.InstrumentTypePK = 5 then 0 else A.PriceMode end PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
		from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
		left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
		where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)                
		Group By A.InstrumentPK,FundPK,SettlementDate,ValueDate
		,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
		,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
	

	)A                
	Group By A.InstrumentPK,A.FundPK,A.SettlementDate
	,A.ValueDate,A.CurrencyPK ,A.Category,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
	,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.CurrencyPK
,B.Category,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable

update A set TaxExpensePercent = B.TaxExpensePercent from #ZLogicFundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)



--INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,             
isnull(A.BuyAmount,0) - (isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) 
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0)	TrxAmount
,A.AcqDate,D.CurrencyPK, A.Category,D.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType
,A.PriceMode,A.BitIsAmortized
From #ZLogicFundPosition A              
Left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2                
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK  
and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')                
--and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (1,2,4,5,14,9)



-- INSERT INVESTMENT + BEG BALANCE DEPOSITO ONLY ( INVESTMENT + BEG BALANCE )              
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
select InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,avg(AvgPrice)
,sum(LastVolume),avg(ClosePrice),TrxAmount,AcqDate
,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable
from (
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID InstrumentID,A.FundPK,C.ID FundID, 
1 AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
1 ClosePrice,                  
isnull(A.BuyAmount,0) - (1 * isnull(A.SellVolume,0))  + isnull		(B.TrxAmount,0) TrxAmount,              
A.AcqDate,D.CurrencyPK, A.Category,D.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when D.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From #ZLogicFundPosition A              
LEft join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2     
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK   
--and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (3)
)A  
group by InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,TrxAmount,AcqDate
,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) AvgPrice,              
isnull(A.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,                  
isnull(A.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900'),C.CurrencyPK, A.Category,isnull(C.TaxExpensePercent,0),A.MarketPK
,A.PriceMode,A.BitIsAmortized,isnull(C.InterestDaysType,2),isnull(C.InterestPaymentType,1),Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2              
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZFundPosition B on A.InstrumentPK = B.InstrumentPK 
and A.FundPK = B.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')              
where A.PeriodPK = @PeriodPK              
) and E.Type in (1,2,4,5,14,9) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)          

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT DEPOSITO ONLY             
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 1 AvgPrice,isnull(A.Volume,0) LastVolume,                  
1 ClosePrice, isnull(A.TrxAmount,0) TrxAmount,              
A.AcqDate,C.CurrencyPK, A.Category,C.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.paymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2     
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2             
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZLogicFundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK       
where A.PeriodPK = @PeriodPK             
) and E.Type in (3) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)          


-- CORPORATE ACTION DIVIDEN SAHAM

-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	                    
delete CorporateActionResult where Date >= @ValueDate and FundPK in (select FundPK from #ZFundFrom)  


-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null and C.FundPK in (select FundPK from #ZFundFrom) 
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null and B.FundPK in (select FundPK from #ZFundFrom) 
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION DIVIDEN RIGHTS
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1 and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-R' and D.status = 2
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION DIVIDEN WARRANT
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom) 
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate
and C.FundPK is null and C.InstrumentPK is null  and B.FundPK in (select FundPK from #ZFundFrom) 

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price,0 from Exercise 
where DistributionDate  = @ValueDate and status = 2  and FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION BOND AMORTIZEN
TRUNCATE TABLE #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
--B.Balance + C.BalanceFromInv LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK  in (2,3,9,15)
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 2 and A.Status = 2 and A.PaymentDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- UPDATE POSISI ZFUNDPOSITION + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
select FundPK,A.InstrumentPK,Price, sum(Balance) Balance,A.status
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
and B.ID not like '%-W' and B.ID not like '%-R'
WHERE A.Date <= @ValueDate and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom) 

Group By FundPK,A.InstrumentPK,Price,A.status
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
left join instrumentType C on A.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where C.Type in (1,9,2,5,14)
--AND A.LastVolume > 0  
and A.FundPK = B.FundPK


                
-- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK RIGHTS/WARRANT
IF NOT EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
)
BEGIN
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
A.Price * BalanceExercise,null,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2  and A.FundPK in (select FundPK from #ZFundFrom) 
END




---- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK DISTRIBUTED DATE
--IF NOT EXISTS
--(
--Select * from #ZFundPosition A 
--left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
--where DistributionDate  = @ValueDate and status = 2
--)
--BEGIN
--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
--TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
--Select A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
--left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
--left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
--where DistributionDate  <= @ValueDate and A.status = 2
--END


--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
--tambahan boris untuk transaksi yg uda dijual sebagian
left join #ZFundPosition D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
where A.status = 2 and B.ID like '%-W' and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom)  and D.FundPK is null



Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,4,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
--tambahan boris untuk transaksi yg uda dijual sebagian
left join #ZFundPosition D on A.FundPK = D.FundPK and A.InstrumentPK = D.InstrumentPK
where A.status = 2 and B.ID like '%-R' and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom)  and D.FundPK is null


                       
-- UPDATE POSISI ZFUNDPOSITION + FUND POSITION ADJUSTMENT
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
SELECT FundPK,A.InstrumentPK,case when sum(balance) = 0 then 0 else sum(Price*Balance) / SUM(balance) end Price, sum(Balance) Balance,A.status,
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  THEN A.AcqDate ELSE NULL END AcqDate
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
WHERE A.status = 2 AND A.Date <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
Group By FundPK,A.InstrumentPK,A.status,B.InstrumentTypePK,A.AcqDate
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
AND isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900') and A.FundPK = B.FundPK




--INSERT INSTRUMENT YANG ADA DI FUND POSITION ADJUSTMENT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  

SELECT A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
SUM(A.Balance),dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
SUM(Balance*Price),
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  then  A.AcqDate else null End ,
B.CurrencyPK,NULL,B.TaxExpensePercent,B.MarketPK
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
where A.status = 2 AND A.AcqDate <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
and NOT EXISTS 
(SELECT * FROM #ZFundPosition C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(C.AcqDate,'01/01/1900'))
GROUP BY A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,
B.CurrencyPK,B.TaxExpensePercent,B.MarketPK,A.AcqDate



-- DELETE RIGHTS AND WARRANT YANG EXPIRED
Delete A From #ZFundPosition A
Inner join 
(
    Select C.InstrumentPK,A.ExDate AcqDate from CorporateAction A
    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
    left join Instrument C on C.ID = B.ID + '-R' and C.status = 2
    where ExpiredDate <= @ValueDate and A.Status = 2 and A.Type = 3
)B on A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate

Delete A From #ZFundPosition A
Inner join 
(
    Select C.InstrumentPK,A.ExDate AcqDate from CorporateAction A
    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
    left join Instrument C on C.ID = B.ID + '-W' and C.status = 2
    where ExpiredDate <= @ValueDate and A.Status = 2 and A.Type = 5
)B on A.InstrumentPK = B.InstrumentPK and A.AcqDate = B.AcqDate

-- KURANGIN BALANCE WARRANT AND RIGHTS YANG ADA DI EXERCISE


IF  EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2 and A.FundPK in (select FundPK from #ZFundFrom) 
)
BEGIN
Update A set A.LastVolume = A.LastVolume - isnull(B.BalanceExercise,0) from #ZFundPosition A
left join Exercise B on A.InstrumentPK = B.InstrumentRightsPK and B.status = 2
where Date = @ValueDate and A.FundPK in (select FundPK from #ZFundFrom) 
END


Insert into FundPosition(FundPositionPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable)                  
Select C.EndDayTrailsFundPortfolioPK,C.EndDayTrailsFundPortfolioPK,1,2,'',@ValueDate,A.FundPK, FundID,                  
A.InstrumentPK,InstrumentID,CONVERT(decimal(18,12),AvgPrice),LastVolume
,case when A.InstrumentTypePK in (2,3,8,14,13,9,15)  then CONVERT(decimal(18,12),AvgPrice)/100 else CONVERT(decimal(18,12),AvgPrice) End * LastVolume CostValue
, ClosePrice,TrxAmount
,case when A.InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
AcqDate,D.MaturityDate,D.InterestPercent,A.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,isnull(A.InterestDaysType,0),isnull(A.InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(A.BankPK,0),isnull(A.BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
From #ZFundPosition  A WITH (NOLOCK)
left join Fund B on A.FundPK = B.FundPK
left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2) 
inner join #ZFundFrom C on A.FundPK = C.FundPK
where A.LastVolume > 0 and B.status in (1,2)  and A.FundPK in (select FundPK from #ZFundFrom)

IF NOT EXISTS(
select InvestmentPK from Investment 
where ValueDate > @ValueDate and InstrumentTypePK in (2,3,8,9,11,13,14,15) and TrxType = 2 and StatusInvestment = 2 and statusDealing <> 3 and StatusSettlement <> 3 and FundPK in (select FundPK from #ZFundFrom) 
)
BEGIN
	delete FifoBondHistorical where date = @ValueDate and FundPK in (select FundPK from #ZFundFrom) 

	insert into FifoBondHistorical (Date,FundPK,FundID,InstrumentID,AcqDate,AcqPrice,AcqVolume,LastUpdate)
	select @ValueDate Date,A.FundPK,B.ID FundID,C.ID InstrumentID,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume, @LastUpdate from (
	select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
	union all
	select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume from FifoBondPositionTemp where RemainingVolume != 0
	union all
	select FundPK,InstrumentPK,SettlementDate,DonePrice,DoneVolume from Investment where ValueDate > '2019-10-18' and TrxType = 1 and statussettlement = 2 and statusdealing = 2 and statusinvestment = 2 and InstrumentTypePK in (2,3,8,9,13,15) and InvestmentPK not in ( 
	select InvestmentBuyPK from FifoBondPositionTemp
	)
	) A 
	left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
	where A.FundPK in  (select FundPK from #ZFundFrom) 
	group by A.FundPK,B.ID,C.ID,AcqDate,AcqPrice
END




Delete FP From FundPosition FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
left join #ZFundFrom B on FP.FundPK = B.FundPK 
Where FundPositionPK = B.EndDayTrailsFundPortfolioPK and I.InstrumentTypePK not in (1,4,6,16)
and FP.MaturityDate <= @ValueDate and FP.MaturityDate Is Not Null  and FP.FundPK in (select FundPK from #ZFundFrom)  


---------PROSES AMORTIZED DAN PRICE MODE------------------------------
update A set A.ClosePrice =  Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
			when A.PriceMode = 2 then LowPriceValue
				when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
		else  
			dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			 
		end 
, A.MarketValue = A.Balance * Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
			when A.PriceMode = 2 then LowPriceValue
				when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
		else  
			dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			  
		end / Case when D.InstrumentTypePK in (2,3,8,14,13,9,15)  then 100 else 1 end
from FundPosition A 
left join 
(
	select InstrumentPK,LowPriceValue,ClosePriceValue,HighPriceValue From ClosePrice where Date =
	(
		Select max(Date) From ClosePrice where date <= @ValueDate and status = 2
	) and status = 2
)B on A.InstrumentPK = B.InstrumentPK 
left join instrument C on A.InstrumentPK = C.instrumentPK and C.Status = 2
left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.FundPK in (select FundPK from #ZFundFrom)  and Date = @ValueDate and A.status = 2

-- STATIC CLOSEPRICE

insert into #StaticClosePrice
select distinct FundPK,instrumentPK,max(Date) maxDate from UpdateClosePrice 
where Date <= @ValueDate and status = 2 
and fundPK in
(
	Select FundPK from #ZFundFrom
)
group by FundPK,InstrumentPK
order by FundPK,InstrumentPK asc

--select * from #StaticClosePrice order by FundPK,instrumentPK,maxDate asc

	
--Select A.FundPK,A.InstrumentPK,B.ClosePriceValue ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
--left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
--left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
--where A.maxDate = @ValueDate

--order by fundPK,InstrumentPK

		--select A.FundPK,A.InstrumentPK,B.ClosePrice, A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
		--INNER join (
		--	Select A.FundPK,A.InstrumentPK,case when (B.ClosePriceValue is not null ) then B.ClosePriceValue else D.ClosePriceValue end ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
		--	left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
		--	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
		--	left join ClosePrice D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)

		--)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
		--inner join #ZFundFrom C on A.FundPK = C.FundPK 
		--where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioPK
		--and A.Status = 2
		--order by A.FundPK,A.InstrumentPK


		Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) 
		then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
		INNER join (
				
Select A.FundPK,A.InstrumentPK,B.ClosePriceValue ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
where isnull(B.ClosePriceValue,0) >= 0 and A.maxDate = @ValueDate

		)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK 
		inner join #ZFundFrom C on A.FundPK = C.FundPK
		where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioPK
		and A.Status = 2

		--Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
		--left join #StaticClosePrice B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
		--where A.Date = @ValueDate and A.TrailsPK = @maxEndDayTrailsFundPortfolioPK
		--and A.InstrumentPK in(
		--	select instrumentPK From #StaticClosePrice where FundPK = @CFundPK
		--) and A.FundPK = @CFundPK and A.status = 2

-- update TrxBuy di Investment untuk Sell / Rollover



Declare @Investment Table                   
(            
	TrxBuy int,    
	InvestmentPK int,
	InstrumentPK int,   
	FundPK int,
	Date datetime
)      


Insert into @Investment
select TrxBuy,InvestmentPK,B.InstrumentPK,B.FundPK,B.Date from Investment A
left join FundPosition B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK not in (1,4,16) and StatusInvestment in (1,2) and TrxType in (2,3) and A.FundPK in (select FundPK from #ZFundFrom)  



declare @DTrxBuy int
declare @DInvestmentPK int
declare @DInstrumentPK int
declare @DFundPK int
declare @DDate datetime
declare @DNewIdentity bigint


DECLARE C CURSOR FOR 

select * from @Investment
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate     
While @@FETCH_STATUS = 0
BEGIN   


set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FundPosition where InstrumentPK = @DInstrumentPK and FundPK = @DFundPK and Date = @DDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusInvestment in (1,2)

Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate                  
END
Close C
Deallocate C   

	
Update A set BitValidate = 1, LogMessages = B.ID + ' - ' + B.Name from EndDayTrailsFundPortfolio A  
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where A.FundPK in (select FundPK from #ZFundFrom)  and A.Status = 2 and ValueDate = @ValueDate       

--Fetch next From A Into @CFundPK
--END
--Close A
--Deallocate A


Select (select max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio) LastPK
                        ";
                        cmd.Parameters.AddWithValue("@paramValueDate", _valueDate);
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


        public string EndDayTrailsFundPortfolio_VoidBySelected(DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
        {
            try
            {
                string _paramPK = "";

                if (!_host.findString(_edt.stringEndDayTrailsFundPortfolioFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.stringEndDayTrailsFundPortfolioFrom))
                {
                    _paramPK = _edt.stringEndDayTrailsFundPortfolioFrom;
                }
                else
                {
                    _paramPK = "";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                            update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate 
                            where EndDayTrailsFundPortfolioPK in ( " + _paramPK + @" )
                     
                            update FundPosition set status = 3 ,LastUpdate = @LastUpdate where Date = @ValueDate and TrailsPK in ( " + _paramPK + @" )

                            select 'Void by selected success' Result
                        ";

                        cmd.Parameters.AddWithValue("@ValueDate", _edt.ValueDate);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _edt.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
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


        //end bagian fifo

        public string EndDayTrailsFundPortfolio_GenerateAverageFP(DateTime _date)
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
                            
                            


--declare @Date date
--                            declare @LastUpdate datetime

--                            set @Date = '2020-09-25'
                            set @LastUpdate = getdate()

                            --drop table #AVGRecalculation
                            --drop table #AVGResultList
                            --drop table #AVGResult

                            Declare @CInstrumentPK int,@CFundPK int, @Mode int
                            DECLARE @RunningAmount NUMERIC(22, 8) 
                            DECLARE @RunningBalance NUMERIC(22, 8) 
                            DECLARE @RunningAvgPrice NUMERIC(22, 8) 
                            DECLARE @COUNTER INT
                            DECLARE @PeriodPK INT 

                            SELECT @PeriodPK = periodpk 
                            FROM   period 
                            WHERE  @Date BETWEEN datefrom AND dateto 
                                    AND status = 2 

                            set @COUNTER = 1

                            Create table #AVGRecalculation 
                                ( 
		                            IdentInt int identity,
                                    [runningamount]   [NUMERIC](22, 8) NULL, 
                                    [runningbalance]  [NUMERIC](22, 8) NULL, 
                                    [runningavgprice] [NUMERIC](22, 8) NULL, 
		                            InstrumentPK int,
		                            FundPK int,
                                    [valuedate]       [DATETIME] NULL, 
                                    [volume]          [NUMERIC](22, 8) NULL, 
                                    [amount]          [NUMERIC](22, 8) NULL, 
                                    [price]           [NUMERIC](22, 8) NULL, 
                                    [trxtype]         [INT] NULL,
									[priority]		  [INT] NULL,
									CounterInt int NULL
                                ) 
                            CREATE CLUSTERED INDEX indx_AVGRecalculation ON #AVGRecalculation (FundPK,InstrumentPK);

                            Create table #AVGResultList
                            (
	                            Date Datetime,
	                            InstrumentPK int,
	                            FundPK int
                            )
                            CREATE CLUSTERED INDEX indx_AVGResultList ON #AVGResultList (FundPK,InstrumentPK);

                            Create table #AVGResult
                            (
	                            InstrumentPK int,
	                            FundPK int,
	                            AVGPrice numeric(22,8)
                            )
                            CREATE CLUSTERED INDEX indx_AVGResult ON #AVGResult (FundPK,InstrumentPK);

	                        INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,InstrumentPK,FundPK,valuedate,volume,amount,price,trxtype,priority) 
                            SELECT 0,0,0,InstrumentPK,FundPK,Dateadd(yy, Datediff(yy, 0, Dateadd(year, -1, @Date)) + 1, -1) ValueDate,            
											Cast(volume AS NUMERIC(26, 6))				Volume,            
											Cast(volume * avgprice AS NUMERIC(26, 6))	Amount,             
											Cast(avgprice AS NUMERIC(24, 8))			Price,           
											1											TrxType,
											0											[priority]            
							FROM fundendyearportfolio        
							WHERE periodpk = @PeriodPK 
										   
							INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,InstrumentPK,FundPK,valuedate,volume,amount,price,trxtype,priority) 
							SELECT 0,0,0,A.InstrumentPK,A.FundPK,valuedate,   
									Cast(donevolume AS NUMERIC(26, 6))				Volume,   
									Cast(donevolume * doneprice AS NUMERIC(26, 6))	Amount,   
									Cast(doneprice AS NUMERIC(26, 6))				Price,   
									trxtype,  
									case when (B.date is not null) then B.Priority else 0 end Priority  
							FROM investment A  
									left join FundAvgPriority B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.ValueDate = B.date and A.TrxType = B.Priority and B.Status = 2 
							WHERE statussettlement = 2   
									AND Year(valuedate) = Year(@Date)   
							group by A.InstrumentPK,A.FundPK,ValueDate,donevolume,DonePrice,TrxType,B.date,B.Priority,A.InvestmentPK   
							order by InvestmentPK asc  

							INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,InstrumentPK,FundPK,valuedate,volume,amount,price,trxtype,priority)            
							SELECT Cast(0 AS NUMERIC(26, 6)) RunningAmount,     
									Cast(0 AS NUMERIC(26, 6)) RunningBalance,     
									Cast(0 AS NUMERIC(24, 8)) RunningAvgPrice,     
									InstrumentPK,FundPK,ValueDate,Volume,Amount,Price,TrxType,priority     
							FROM   ( 
									SELECT InstrumentPK,FundPK,
											date                                     ValueDate,     
											Cast(balance AS NUMERIC(26, 6))          Volume,       
											Cast(balance * price AS NUMERIC(26, 6))  Amount,     
											Cast(price AS NUMERIC(26, 6))			Price,             
											1                                        TrxType,
											0										[priority]        
									FROM corporateactionresult       
									WHERE status = 2             
											AND date <= @Date       
											AND Year(date) = Year(@Date)        
											AND balance <> 0   
											     
									UNION ALL         
									SELECT InstrumentPK,FundPK,Date					ValueDate,    
											CAST(balance AS NUMERIC(26, 6))			Volume,            
											Cast(balance * price AS NUMERIC(26, 6)) Amount,      
											Cast(price AS NUMERIC(26, 6))			Price,             
											1										TrxType,
											0										[priority]               
									FROM dbo.FundPositionAdjustment           
									WHERE YEAR(Date) = Year(@Date) AND status = 2          
											AND Balance <> 0   
												           
									UNION ALL             
									SELECT InstrumentPK,FundPK,
											DistributionDate                                ValueDate,         
											Cast(BalanceExercise AS NUMERIC(26, 6))         Volume,          
											Cast(BalanceExercise * price AS NUMERIC(26, 6)) Amount,        
											Cast(price AS NUMERIC(26, 6))					Price,             
											1												TrxType,
											0												[priority]        
									FROM Exercise             
									WHERE DistributionDate <= @Date      
											AND Year(date) = Year(@Date)          
											AND BalanceExercise <> 0         
							) dt  

                            Declare A Cursor For
	                            Select Distinct  InstrumentPK,FundPK from FundPosition 
								where Date = @Date and status = 2
                            Open A
                            Fetch Next From A
                            Into @CInstrumentPK,@CFundPK

                            While @@FETCH_STATUS = 0  
                            BEGIN	
								set @Mode = 0
	                            SET @RunningAmount = 0 
                                SET @RunningBalance = 0 
                                SET @RunningAvgPrice = 0;
								select @Mode = isnull(AveragePriority,1) from FundAccountingSetup where status in (1,2) and FundPK = @CFundPK  

								if @Mode = 1
								begin
									WITH q 
											AS (SELECT TOP 1000000000 * FROM #AVGRecalculation where FundPK = @CFundPK and InstrumentPK = @CInstrumentPK
												ORDER  BY valuedate, trxtype ASC) 
									UPDATE q 
									SET    @RunningBalance = runningbalance = @RunningBalance + ( volume * CASE WHEN trxtype = 1 THEN 1 ELSE -1 END ), 
											@RunningAmount = runningamount = @RunningAmount + CASE WHEN trxtype = 1 THEN amount ELSE - volume * @RunningAvgPrice END, 
											@RunningAvgPrice = runningavgprice = CASE WHEN trxtype = 1 THEN CASE WHEN @RunningBalance = 0 THEN 0 ELSE @RunningAmount / @RunningBalance END ELSE @RunningAvgPrice END,
											@COUNTER = CounterInt = @Counter + 1 
								end
								else if @Mode = 2
								begin
									WITH q 
											AS (SELECT TOP 1000000000 * FROM #AVGRecalculation where FundPK = @CFundPK and InstrumentPK = @CInstrumentPK
												ORDER  BY valuedate asc,trxtype desc) 
									UPDATE q 
									SET    @RunningBalance = runningbalance = @RunningBalance + ( volume * CASE WHEN trxtype = 1 THEN 1 ELSE -1 END ), 
											@RunningAmount = runningamount = @RunningAmount + CASE WHEN trxtype = 1 THEN amount ELSE - volume * @RunningAvgPrice END, 
											@RunningAvgPrice = runningavgprice = CASE WHEN trxtype = 1 THEN CASE WHEN @RunningBalance = 0 THEN 0 ELSE @RunningAmount / @RunningBalance END ELSE @RunningAvgPrice END,
											@COUNTER = CounterInt = @Counter + 1 
								end
								else if @Mode = 3
								begin
									WITH q 
											AS (SELECT TOP 1000000000 * FROM #AVGRecalculation where FundPK = @CFundPK and InstrumentPK = @CInstrumentPK
												ORDER  BY IdentInt) 
									UPDATE q 
									SET    @RunningBalance = runningbalance = @RunningBalance + ( volume * CASE WHEN trxtype = 1 THEN 1 ELSE -1 END ), 
											@RunningAmount = runningamount = @RunningAmount + CASE WHEN trxtype = 1 THEN amount ELSE - volume * @RunningAvgPrice END, 
											@RunningAvgPrice = runningavgprice = CASE WHEN trxtype = 1 THEN CASE WHEN @RunningBalance = 0 THEN 0 ELSE @RunningAmount / @RunningBalance END ELSE @RunningAvgPrice END,
											@COUNTER = CounterInt = @Counter + 1 
								end

								

	                            Fetch Next From A 
	                            into @CInstrumentPK,@CFundPK
                            End	
                            Close A
                            Deallocate A

                            insert into #AVGResultList
                            Select max(ValueDate),InstrumentPK,FundPK From #AVGRecalculation 
                            where valuedate <= @Date
                            group by InstrumentPK,FundPK


                            DECLARE 
	                            @XDate date,
                                @XFundPK int, 
                                @XInstrumentPK int
 
                            DECLARE A CURSOR FOR 
	                            SELECT * from #AVGResultList
 
                            OPEN A
 
                            FETCH NEXT FROM A INTO 
                                @XDate,@XInstrumentPK,@XFundPK
 
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
        
                                    insert into #AVGResult
		                            Select top 1 A.InstrumentPK,A.FundPK,isnull(A.runningavgprice,0) From #AVGRecalculation A
		                            where A.InstrumentPK = @XInstrumentPK and A.FundPK = @XFundPK and A.valuedate = @XDate
		                            order by IdentInt desc
		
		
		                            FETCH NEXT FROM A INTO @XDate,@XInstrumentPK,@XFundPK 
                                END
 
                            CLOSE A
 
                            DEALLOCATE A

                            --Select FundPk,InstrumentPK,AVGPrice From #AVGResult 
                            --order by InstrumentPK,FundPK

                            --PROSES KE TABLE FUNDPOSITION

                            Update A Set A.AVGPrice =  isnull(B.AVGPrice,0), A.LastUpdate = @LastUpdate  From FundPosition A
                            left join #AVGResult B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK 
                            where A.Date = @Date and A.Status = 2


                            select 'Generate Average Success' Result

                        ";
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Result"].ToString();
                                }
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