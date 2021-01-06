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
    public class EndDayTrailsFundPortfolioForPIIReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[EndDayTrailsFundPortfolioForPII] " +
                            "([EndDayTrailsFundPortfolioForPIIPK],[HistoryPK],[Status],[BitValidate],[ValueDate],[LogMessages],";
        string _paramaterCommand = "@BitValidate,@ValueDate,@LogMessages,";

        //2
        private EndDayTrailsFundPortfolioForPII setEndDayTrailsFundPortfolioForPII(SqlDataReader dr)
        {
            EndDayTrailsFundPortfolioForPII M_EndDayTrailsFundPortfolioForPII = new EndDayTrailsFundPortfolioForPII();
            M_EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK = Convert.ToInt32(dr["EndDayTrailsFundPortfolioForPIIPK"]);
            M_EndDayTrailsFundPortfolioForPII.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_EndDayTrailsFundPortfolioForPII.Status = Convert.ToInt32(dr["Status"]);
            M_EndDayTrailsFundPortfolioForPII.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_EndDayTrailsFundPortfolioForPII.Notes = Convert.ToString(dr["Notes"]);
            M_EndDayTrailsFundPortfolioForPII.BitValidate = Convert.ToBoolean(dr["BitValidate"]);
            M_EndDayTrailsFundPortfolioForPII.ValueDate = dr["ValueDate"].ToString();
            M_EndDayTrailsFundPortfolioForPII.LogMessages = dr["LogMessages"].ToString();
            M_EndDayTrailsFundPortfolioForPII.EntryUsersID = dr["EntryUsersID"].ToString();
            M_EndDayTrailsFundPortfolioForPII.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_EndDayTrailsFundPortfolioForPII.VoidUsersID = dr["VoidUsersID"].ToString();
            M_EndDayTrailsFundPortfolioForPII.EntryTime = dr["EntryTime"].ToString();
            M_EndDayTrailsFundPortfolioForPII.ApprovedTime = dr["ApprovedTime"].ToString();
            M_EndDayTrailsFundPortfolioForPII.VoidTime = dr["VoidTime"].ToString();
            M_EndDayTrailsFundPortfolioForPII.DBUserID = dr["DBUserID"].ToString();
            M_EndDayTrailsFundPortfolioForPII.DBTerminalID = dr["DBTerminalID"].ToString();
            M_EndDayTrailsFundPortfolioForPII.LastUpdate = dr["LastUpdate"].ToString();
            M_EndDayTrailsFundPortfolioForPII.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_EndDayTrailsFundPortfolioForPII;
        }

        //3
        public List<EndDayTrailsFundPortfolioForPII> EndDayTrailsFundPortfolioForPII_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<EndDayTrailsFundPortfolioForPII> L_EndDayTrailsFundPortfolioForPII = new List<EndDayTrailsFundPortfolioForPII>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolioForPII where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolioForPII";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndDayTrailsFundPortfolioForPII.Add(setEndDayTrailsFundPortfolioForPII(dr));
                                }
                            }
                            return L_EndDayTrailsFundPortfolioForPII;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<EndDayTrailsFundPortfolioForPII> EndDayTrailsFundPortfolioForPII_SelectEndDayTrailsFundPortfolioForPIIDate(int _status, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<EndDayTrailsFundPortfolioForPII> L_EndDayTrailsFundPortfolioForPII = new List<EndDayTrailsFundPortfolioForPII>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolioForPII where status = @status and ValueDate = @Date";
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from EndDayTrailsFundPortfolioForPII where  ValueDate = @Date";
                        }

                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndDayTrailsFundPortfolioForPII.Add(setEndDayTrailsFundPortfolioForPII(dr));
                                }
                            }
                            return L_EndDayTrailsFundPortfolioForPII;
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
        public void EndDayTrailsFundPortfolioForPII_Add(EndDayTrailsFundPortfolioForPII _EndDayTrailsFundPortfolioForPII, bool _havePrivillege)
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
                                 "Select isnull(max(EndDayTrailsFundPortfolioForPIIPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from EndDayTrailsFundPortfolioForPII";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _EndDayTrailsFundPortfolioForPII.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(EndDayTrailsFundPortfolioForPIIPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from EndDayTrailsFundPortfolioForPII";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BitValidate", _EndDayTrailsFundPortfolioForPII.BitValidate);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolioForPII.ValueDate);
                        cmd.Parameters.AddWithValue("@LogMessages", _EndDayTrailsFundPortfolioForPII.LogMessages);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _EndDayTrailsFundPortfolioForPII.EntryUsersID);
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
        public void EndDayTrailsFundPortfolioForPII_Approved(EndDayTrailsFundPortfolioForPII _EndDayTrailsFundPortfolioForPII)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolioForPII set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where EndDayTrailsFundPortfolioForPIIPK = @PK and historypk = @historyPK \n " +
                            "Update FundPosition set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate where TrailsPK = @PK and status = 1 " +
                            "Update FundJournal set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate , posted= 1,postedby = @ApprovedUsersID,PostedTime = @ApprovedTime where TrxNo = @PK and status = 1 ";

                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolioForPII.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _EndDayTrailsFundPortfolioForPII.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update EndDayTrailsFundPortfolioForPII set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where EndDayTrailsFundPortfolioForPIIPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolioForPII.ApprovedUsersID);
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
        public void EndDayTrailsFundPortfolioForPII_Reject(EndDayTrailsFundPortfolioForPII _EndDayTrailsFundPortfolioForPII)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolioForPII set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where EndDayTrailsFundPortfolioForPIIPK = @PK and historypk = @historyPK \n " +
                             @"-- Update CorporateActionResult set status = 3 where Date = @ValueDate " +
                            "update FundPosition set status = 3 ,LastUpdate = @LastUpdate where Date = @ValueDate and TrailsPK = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolioForPII.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolioForPII.ValueDate);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolioForPII.VoidUsersID);
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
        public void EndDayTrailsFundPortfolioForPII_Void(EndDayTrailsFundPortfolioForPII _EndDayTrailsFundPortfolioForPII)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolioForPII set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where EndDayTrailsFundPortfolioForPIIPK = @PK and historypk = @historyPK \n " +
                            @" --Update CorporateActionResult set status = 3 where Date = @ValueDate " +
                            "update FundPosition set status = 3 ,LastUpdate = @LastUpdate where Date = @ValueDate and TrailsPK = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _EndDayTrailsFundPortfolioForPII.EndDayTrailsFundPortfolioForPIIPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndDayTrailsFundPortfolioForPII.HistoryPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _EndDayTrailsFundPortfolioForPII.ValueDate);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndDayTrailsFundPortfolioForPII.VoidUsersID);
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
        public string EndDayTrailsFundPortfolioForPII_ApproveByDate(string _usersID, DateTime _date)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update EndDayTrailsFundPortfolioForPII set status = 2, ApprovedUsersID = @ApprovedUsersID, ApprovedTime = @ApprovedTime, LastUpdate = @LastUpdate " +
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
update Investment set MarketPK = 1
update Investment set Category = null where InstrumentTypePK  <> 5

Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioPK int                    

Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo                  
Select @maxEndDayTrailsFundPortfolioPK = max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio     
set @maxEndDayTrailsFundPortfolioPK = isnull(@maxEndDayTrailsFundPortfolioPK,1)               

Insert into EndDayTrailsFundPortfolio  (EndDayTrailsFundPortfolioPK,HistoryPK,Status,ValueDate,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select @maxEndDayTrailsFundPortfolioPK,1,1,@ValueDate,0
,'',@UsersID,@LastUpdate,@LastUpdate                    
      
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
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)              
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
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)                 
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
where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)             
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
where DistributionDate <= @ValueDate  and A.Status = 2  and year(Date) = year(@ValueDate)             
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
AND A.LastVolume > 0


                
-- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK RIGHTS
IF NOT EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
)
BEGIN
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2
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

-- KURANGIN BALANCE WARRANT AND RIGHTS YANG ADA DI EXERCISE


IF  EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
)
BEGIN
Update A set A.LastVolume = A.LastVolume - isnull(B.BalanceExercise,0) from #ZFundPosition A
left join Exercise B on A.InstrumentPK = B.InstrumentRightsPK and B.status = 2
where Date = @ValueDate
END


--select * from #ZLogicFundPosition where FundPK = 7 and InstrumentPK = 978
--select * from #ZFundPosition where FundPK = 7 and InstrumentPK = 978

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
where A.LastVolume > 0 and B.MaturityDate >= @ValueDate 
    

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

DECLARE C CURSOR FOR 
select TrxBuy,InvestmentPK,B.InstrumentPK,B.FundPK,B.Date from Investment A
left join FundPosition B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK not in (1,4,16) and StatusSettlement = 2 and TrxType in (2,3)
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate     
While @@FETCH_STATUS = 0
BEGIN   

set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FundPosition where InstrumentPK = @CInstrumentPK and FundPK = @DFundPK and Date = @DDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusSettlement = 2

Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate                  
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
                                  (select * From EndDayTrailsFundPortfolioForPII where Status = 2 and ValueDate = dbo.FNextWorkingDay(@ValueDate,-1))    
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
                                  (select * From EndDayTrailsFundPortfolioForPII where Status in (1,2) and ValueDate = @ValueDate)    
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

        public bool ValidateGenerateEndDayTrailsFundPortfolioForPII(DateTime _valueDate)
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
                        cmd.CommandText = " if Not Exists(select * From EndDayTrailsFundPortfolioForPII where Status in (1,2) and ValueDate = @ValueDate)   " +
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

        public bool CheckOMS_EndDayTrailsFundPortfolioForPII(DateTime _valueDate)
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
                        select @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolioForPII 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolioForPII where status = 2 and valueDate <= @ValueDate
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

        public bool CheckOMS_EndDayTrailsFundPortfolioForPIIWithParamFund(DateTime _valueDate, int _fundPK)
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
                        select @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolioForPII 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolioForPII where status = 2 and valueDate <= @ValueDate and FundPK = @FundPK
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

        #region EDT FP pecah avg
        public int EndDayTrailsFundPortfolioForPII_GenerateWithParamFund(string _usersID, DateTime _valueDate, EndDayTrailsFundPortfolioForPII _edt) //pecah avg
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

--set @ValueDate = '2019-11-20'
--set @UsersID = 'admin'
--set @lastupdate = getdate()


--drop table #ZFPForPII
--drop table #ZLogicFPForPII
--drop table #ZDividenSaham
--drop table #StaticClosePrice
--drop table #ZFundFrom

Declare @CFundPK int
declare @EndDayTrailsFundPortfolioForPIIPK int
Create Table #ZFPForPII                  
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
BitBreakable bit,
InvestmentTrType int,
)                  

CREATE CLUSTERED INDEX indx_ZFPForPII ON #ZFPForPII (FundPK,InstrumentPK,InstrumentTypePK,BankPK,BankBranchPK);
    
Create Table #ZLogicFPForPII              
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
BitBreakable bit,
InvestmentTrType int
)      

CREATE CLUSTERED INDEX indx_ZLogicFPForPII ON #ZLogicFPForPII (FundPK,InstrumentPK,BankPK,BankBranchPK);

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
	EndDayTrailsFundPortfolioForPIIPK int
)      

CREATE CLUSTERED INDEX indx_ZFundFrom  ON #ZFundFrom (FundPK,EndDayTrailsFundPortfolioForPIIPK);


Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioForPIIPK int       

                
Select @EndDayTrailsFundPortfolioForPIIPK = max(ISNULL(EndDayTrailsFundPortfolioForPIIPK,0)) + 1 from EndDayTrailsFundPortfolioForPII    
set @EndDayTrailsFundPortfolioForPIIPK = isnull(@EndDayTrailsFundPortfolioForPIIPK,1)     


insert into #ZFundFrom(FundPK,EndDayTrailsFundPortfolioForPIIPK)
--select FundPK from Fund where status in (1,2)  " + _paramFund + @" and MaturityDate >= @ValueDate
--select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioForPIIPK from Fund where status in (1,2) and MaturityDate >= @ValueDate
select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioForPIIPK from Fund where status in (1,2) " + _paramFund + @" 
and (MaturityDate >= @ValueDate or MaturityDate = '01/01/1900')
--and FundPK = 3
--PARAM FUND



--DECLARE A CURSOR FOR     

----select FundPK from Fund where status in (1,2)  " + _paramFund + @" and MaturityDate >= @ValueDate
--select FundPK from Fund where status in (1,2)  and MaturityDate >= @ValueDate

--Open A
--Fetch Next From A
--Into @CFundPK

--While @@FETCH_STATUS = 0
--BEGIN    

update FPForPII set status = 3,LastUpdate=@lastUpdate where Date = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)
update EndDayTrailsFundPortfolioForPII set status = 3,VoidUsersID = @UsersID,VoidTime = @lastUpdate,LastUpdate=@lastUpdate
where ValueDate = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)          

UPDATE Investment set PriceMode = 1 where InstrumentTypePK  in (1,4,16)  and ValueDate = @ValueDate
update Investment set MarketPK = 1  where ValueDate = @ValueDate
update Investment set Category = null where InstrumentTypePK  <> 5  and ValueDate = @ValueDate
        
Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo  

Insert into EndDayTrailsFundPortfolioForPII  (EndDayTrailsFundPortfolioForPIIPK,HistoryPK,Status,ValueDate,FundPK,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select EndDayTrailsFundPortfolioForPIIPK,1,2,@ValueDate,FundPK,0
,'',@UsersID,@LastUpdate,@LastUpdate  
from #ZFundFrom A     

      
                             

-- TARIK JUAL BELI DARI INVESTMENT              
Insert into #ZLogicFPForPII	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,InvestmentTrType)               

Select SUM(BuyVolume) BuyVolume,SUM(SellVolume) SellVolume,SUM(BuyAmount) BuyAmount,SUM(SellAmount) SellAmount,B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,
B.InterestPercent,B.CurrencyPK,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate
,B.BankPK,B.BankBranchPK,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable,B.InvestmentTrType
From               
(               
Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
isnull(A.SettlementDate,'') SettlementDate,isnull(A.MaturityDate,'') MaturityDate,isnull(A.InterestPercent,0) InterestPercent,
isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.TaxExpensePercent,0) TaxExpensePercent,isnull(A.MarketPK,0) MarketPK,
isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
A.AcqDate,A.BitBreakable,A.InvestmentTrType
from (                 
	
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type in (1,2,5,14) then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then '1900-01-01' else A.SettlementDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,isnull(InvestmentTrType,0) InvestmentTrType         
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK <> 6 and A.FundPK in (select FundPK from #ZFundFrom)         
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK,A.InvestmentTrType
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type in (1,2,5,14) then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then '1900-01-01' else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable ,isnull(InvestmentTrType,0) InvestmentTrType        
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK <> 6  and A.FundPK in (select FundPK from #ZFundFrom)                     
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK,A.InvestmentTrType
			
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
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then '1900-01-01' else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,isnull(InvestmentTrType,0) InvestmentTrType         
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.FundPK in (select FundPK from #ZFundFrom)              
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK,InvestmentTrType

-- REKSDANA--
UNION ALL
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type in (1,4) then null else AcqDate end SettlementDate,              
Case when C.Type in (1,4) then null else A.MaturityDate end MaturityDate,              
Case when C.Type in (1,4) then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then '1900-01-01' else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,isnull(InvestmentTrType,0) InvestmentTrType           
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where SettlementDate <= @ValueDate and trxType = 1 and StatusSettlement = 2 and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)           
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK,InvestmentTrType
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type in (1,4) then null else AcqDate end SettlementDate,              
Case when C.Type in (1,4) then null else A.MaturityDate end MaturityDate,              
Case when C.Type in (1,4) then null else A.InterestPercent end InterestPercent,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then '1900-01-01' else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable ,isnull(InvestmentTrType,0) InvestmentTrType        
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where SettlementDate <= @ValueDate and trxType = 2 and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)  and A.InstrumentTypePK = 6   and A.FundPK in (select FundPK from #ZFundFrom)                
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK,InvestmentTrType
	

)A                
Group By A.InstrumentPK,A.FundPK,A.SettlementDate,A.MaturityDate,A.InterestPercent
,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable,A.InvestmentTrType
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,B.InterestPercent,B.CurrencyPK
,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable,B.InvestmentTrType



--INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE )
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized,InvestmentTrType)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
isnull(dbo.[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),0) AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,         
dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK) ClosePrice,             
isnull(A.BuyAmount,0) - (isnull(dbo.[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,B.InvestmentTrType),0) 
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0)	TrxAmount
,isnull(A.AcqDate,'01/01/1900'),A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType
,A.PriceMode,A.BitIsAmortized,A.InvestmentTrType
From #ZLogicFPForPII A              
Left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2                
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolioForPII B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK 
and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')                
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK and A.InvestmentTrType = B.InvestmentTrType
where E.Type in (1,2,4,5,14,9) 
group by A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,A.AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType,A.InvestmentTrType,
A.BuyVolume, A.SellVolume, B.Volume ,A.PriceMode,A.BitIsAmortized,B.InvestmentTrType,A.BuyAmount,B.TrxAmount


-- INSERT INVESTMENT + BEG BALANCE DEPOSITO ONLY ( INVESTMENT + BEG BALANCE )              
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable,InvestmentTrType)                  
select InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,avg(AvgPrice)
,sum(LastVolume),avg(ClosePrice),TrxAmount,isnull(AcqDate,'01/01/1900'),MaturityDate,InterestPercent
,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable,InvestmentTrType
from (
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID InstrumentID,A.FundPK,C.ID FundID, 
1 AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
1 ClosePrice,                  
isnull(A.BuyAmount,0) - (1 * isnull(A.SellVolume,0))  + isnull		(B.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900') AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when D.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,A.InvestmentTrType
From #ZLogicFPForPII A              
LEft join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2     
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolioForPII B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.InvestmentTrType = B.InvestmentTrType 
and isnull(A.Maturitydate,'01/01/1900') = isnull(B.MaturityDate,'01/01/1900')    
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (3)
)A  
group by InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,TrxAmount,AcqDate
,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable,InvestmentTrType


 

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable,InvestmentTrType)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
isnull(dbo.[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),0) AvgPrice,              
isnull(A.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK) ClosePrice,                  
isnull(A.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900'),isnull(A.MaturityDate,'01/01/1900'),isnull(A.InterestPercent,0),C.CurrencyPK, A.Category,isnull(A.TaxExpensePercent,0),A.MarketPK
,A.PriceMode,A.BitIsAmortized,isnull(C.InterestDaysType,2),isnull(C.InterestPaymentType,1),Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,A.InvestmentTrType
From FundEndYearPortfolioForPII A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2              
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioForPIIPK not in              
(              
Select FundEndYearPortfolioForPIIPK From FundEndYearPortfolioForPII A              
inner join #ZFPForPII B on A.InstrumentPK = B.InstrumentPK and A.InvestmentTrType = B.InvestmentTrType 
and A.FundPK = B.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')              
where A.PeriodPK = @PeriodPK              
) and E.Type in (1,2,4,5,14,9) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)       




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT DEPOSITO ONLY             
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable,InvestmentTrType)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 1 AvgPrice,isnull(A.Volume,0) LastVolume,                  
1 ClosePrice, isnull(A.TrxAmount,0) TrxAmount,              
A.AcqDate,A.MaturityDate,A.InterestPercent,C.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.paymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,A.InvestmentTrType
From FundEndYearPortfolioForPII A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2     
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2             
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioForPIIPK not in              
(              
Select FundEndYearPortfolioForPIIPK From FundEndYearPortfolioForPII A              
inner join #ZLogicFPForPII B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK  and A.MaturityDate = B.MaturityDate and A.InvestmentTrType = B.InvestmentTrType         
where A.PeriodPK = @PeriodPK             
) and E.Type in (3) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)          


-- CORPORATE ACTION DIVIDEN SAHAM

-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	                    
delete CorporateActionResult where Date = @ValueDate and FundPK in (select FundPK from #ZFundFrom)  


-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FPForPII B on A.InstrumentPK = B.InstrumentPK 
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
	

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK,InvestmentTrType)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate),InvestmentTrType
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate and B.FundPK in (select FundPK from #ZFundFrom) 



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FPForPII B on A.InstrumentPK = B.InstrumentPK 
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
left join FPForPII B on A.InstrumentPK = B.InstrumentPK 
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

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK,InvestmentTrType)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate),InvestmentTrType
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
left join FPForPII B on A.InstrumentPK = B.InstrumentPK 
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

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK,InvestmentTrType)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate),InvestmentTrType
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK,InvestmentTrType)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price,0,InvestmentTrType from Exercise 
where DistributionDate  = @ValueDate and status = 2  and FundPK in (select FundPK from #ZFundFrom) 


-- CORPORATE ACTION BOND AMORTIZEN
TRUNCATE TABLE #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
--B.Balance + C.BalanceFromInv LastBalance
From CorporateAction A 
left join FPForPII B on A.InstrumentPK = B.InstrumentPK 
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
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK,InvestmentTrType)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0,dbo.FgetPeriod(A.ValueDate),InvestmentTrType
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom) 


-- UPDATE POSISI ZFPForPII + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFPForPII A
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


                
-- EXERCISE BELUM ADA DI FPForPII, UNTUK RIGHTS/WARRANT
IF NOT EXISTS
(
Select * from #ZFPForPII A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
)
BEGIN
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized,InvestmentTrType)
Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK),
A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0,0 from Exercise A
left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2  and A.FundPK in (select FundPK from #ZFundFrom) 
END




---- EXERCISE BELUM ADA DI FPForPII, UNTUK DISTRIBUTED DATE
--IF NOT EXISTS
--(
--Select * from #ZFPForPII A 
--left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
--where DistributionDate  = @ValueDate and status = 2
--)
--BEGIN
--Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
--TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
--Select A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK),
--A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
--left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
--left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
--where DistributionDate  <= @ValueDate and A.status = 2
--END


--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFPForPII
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),
A.Balance,dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2

where A.status = 2 and B.ID like '%-W' and PeriodPK = @PeriodPK  and A.FundPK = @CFundPK



Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),
A.Balance,dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
 
where A.status = 2 and B.ID like '%-R' and PeriodPK = @PeriodPK  and A.FundPK = @CFundPK


                       
-- UPDATE POSISI ZFPForPII + FUND POSITION ADJUSTMENT
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = [dbo].[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFPForPII A
left join 
(
SELECT FundPK,A.InstrumentPK,case when sum(balance) = 0 then 0 else sum(Price*Balance) / SUM(balance) end Price, sum(Balance) Balance,A.status,
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  THEN A.AcqDate ELSE NULL END AcqDate
from dbo.FPForPIIAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
WHERE A.status = 2 AND A.Date <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
Group By FundPK,A.InstrumentPK,A.status,B.InstrumentTypePK,A.AcqDate
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
AND isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900') and A.FundPK in (select FundPK from #ZFundFrom) 




--INSERT INSTRUMENT YANG ADA DI FUND POSITION ADJUSTMENT TAPI GA ADA IN ZFPForPII
Insert into #ZFPForPII(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  

SELECT A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID, 
[dbo].[FgetlastavgfrominvestmentByInvestmentTrType] (@ValueDate,A.InstrumentPK,A.FundPK,A.InvestmentTrType),
SUM(A.Balance),dbo.FGetLastClosePriceForFPForPII(@ValueDate,A.InstrumentPK),
SUM(Balance*Price),
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  then  A.AcqDate else null End ,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,NULL,B.TaxExpensePercent,B.MarketPK
from dbo.FPForPIIAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
where A.status = 2 AND A.AcqDate <= @ValueDate and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 
and NOT EXISTS 
(SELECT * FROM #ZFPForPII C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(C.AcqDate,'01/01/1900'))
GROUP BY A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,B.TaxExpensePercent,B.MarketPK,A.AcqDate,A.InvestmentTrType



-- DELETE RIGHTS AND WARRANT YANG EXPIRED
Delete A From #ZFPForPII A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-R' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 3
)B on A.InstrumentPK = B.InstrumentPK

Delete A From #ZFPForPII A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-W' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 5
)B on A.InstrumentPK = B.InstrumentPK

-- KURANGIN BALANCE WARRANT AND RIGHTS YANG ADA DI EXERCISE


IF  EXISTS
(
Select * from #ZFPForPII A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where Date >= @ValueDate and DistributionDate > @valuedate and status = 2 and A.FundPK in (select FundPK from #ZFundFrom) 
)
BEGIN
Update A set A.LastVolume = A.LastVolume - isnull(B.BalanceExercise,0) from #ZFPForPII A
left join Exercise B on A.InstrumentPK = B.InstrumentRightsPK and B.status = 2
where Date = @ValueDate and A.FundPK in (select FundPK from #ZFundFrom) 
END


--select * from #ZLogicFPForPII where FundPK = 7 and InstrumentPK = 978
--select * from #ZFPForPII where FundPK = 7 and InstrumentPK = 978



--Insert into FPForPII(FPForPIIPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
--InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
--,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
--,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
--,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable)                  
--Select (select max(ISNULL(EndDayTrailsFundPortfolioForPIIPK,0)) + 1 from EndDayTrailsFundPortfolioForPII),(select max(ISNULL(EndDayTrailsFundPortfolioForPIIPK,0)) + 1 from EndDayTrailsFundPortfolioForPII),1,2,'',@ValueDate,A.FundPK, FundID,                  
--A.InstrumentPK,InstrumentID,CONVERT(decimal(18,12),AvgPrice),LastVolume
--,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then CONVERT(decimal(18,12),AvgPrice)/100 else CONVERT(decimal(18,12),AvgPrice) End * LastVolume CostValue
--, ClosePrice,TrxAmount
--,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
--AcqDate,A.MaturityDate,InterestPercent,A.CurrencyPK, Category,TaxExpensePercent,A.MarketPK
--,isnull(InterestDaysType,0),isnull(InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(A.BankPK,0),isnull(A.BankBranchPK,0)
--,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
--From #ZFPForPII  A WITH (NOLOCK)
--left join Fund B on A.FundPK = B.FundPK
--where A.LastVolume > 0 and B.status in (1,2)  and A.FundPK in (select FundPK from #ZFundFrom) 
  

--Delete FP From FPForPII FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
--Where FPForPIIPK = (select max(ISNULL(EndDayTrailsFundPortfolioForPIIPK,0)) + 1 from EndDayTrailsFundPortfolioForPII) and I.InstrumentTypePK not in (1,4,6,16)
--and FP.MaturityDate <= @ValueDate and FP.MaturityDate Is Not Null  and FP.FundPK in (select FundPK from #ZFundFrom)  
 
Insert into FPForPII(FPForPIIPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable,InvestmentTrType)                  
Select C.EndDayTrailsFundPortfolioForPIIPK,C.EndDayTrailsFundPortfolioForPIIPK,1,2,'',@ValueDate,A.FundPK, FundID,                  
A.InstrumentPK,InstrumentID,CONVERT(decimal(18,12),AvgPrice),LastVolume
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then CONVERT(decimal(18,12),AvgPrice)/100 else CONVERT(decimal(18,12),AvgPrice) End * LastVolume CostValue
, ClosePrice,TrxAmount
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
AcqDate,A.MaturityDate,InterestPercent,A.CurrencyPK, Category,TaxExpensePercent,A.MarketPK
,isnull(InterestDaysType,0),isnull(InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(A.BankPK,0),isnull(A.BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0),isnull(InvestmentTrType,0)
From #ZFPForPII  A WITH (NOLOCK)
left join Fund B on A.FundPK = B.FundPK
inner join #ZFundFrom C on A.FundPK = C.FundPK
where A.LastVolume > 0 and B.status in (1,2)  and A.FundPK in (select FundPK from #ZFundFrom) 
  

Delete FP From FPForPII FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
left join #ZFundFrom B on FP.FundPK = B.FundPK 
Where FPForPIIPK = B.EndDayTrailsFundPortfolioForPIIPK and I.InstrumentTypePK not in (1,4,6,16)
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
from FPForPII A 
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

		--select A.FundPK,A.InstrumentPK,B.ClosePrice, A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FPForPII A
		--INNER join (
		--	Select A.FundPK,A.InstrumentPK,case when (B.ClosePriceValue is not null ) then B.ClosePriceValue else D.ClosePriceValue end ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
		--	left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
		--	left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
		--	left join ClosePrice D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)

		--)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
		--inner join #ZFundFrom C on A.FundPK = C.FundPK 
		--where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioForPIIPK
		--and A.Status = 2
		--order by A.FundPK,A.InstrumentPK


		Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) 
		then B.ClosePrice/100 else B.ClosePrice end from FPForPII A
		INNER join (
				
Select A.FundPK,A.InstrumentPK,B.ClosePriceValue ClosePrice,C.InstrumentTypePK from #StaticClosePrice A
left join UpdateClosePrice B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and A.maxDate = B.Date and B.status = 2
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
where isnull(B.ClosePriceValue,0) > 0 and A.maxDate = @ValueDate

		)B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK 
		inner join #ZFundFrom C on A.FundPK = C.FundPK
		where A.Date = @ValueDate and A.TrailsPK = C.EndDayTrailsFundPortfolioForPIIPK
		and A.Status = 2

		--Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FPForPII A
		--left join #StaticClosePrice B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
		--where A.Date = @ValueDate and A.TrailsPK = @maxEndDayTrailsFundPortfolioForPIIPK
		--and A.InstrumentPK in(
		--	select instrumentPK From #StaticClosePrice where FundPK = @CFundPK
		--) and A.FundPK = @CFundPK and A.status = 2





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
left join FPForPII B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK not in (1,4,16) and StatusSettlement = 2 and TrxType in (2,3) and A.FundPK in (4)
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate  
While @@FETCH_STATUS = 0
BEGIN   

set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FPForPII where InstrumentPK = @DInstrumentPK and FundPK = @DFundPK and Date = @DDate and AcqDate = @DAcqDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusSettlement = 2


Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate                  
END
Close C
Deallocate C  


	
Update A set BitValidate = 1, LogMessages = B.ID + ' - ' + B.Name from EndDayTrailsFundPortfolioForPII A  
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where A.FundPK in (select FundPK from #ZFundFrom)  and A.Status = 2 and ValueDate = @ValueDate       

--Fetch next From A Into @CFundPK
--END
--Close A
--Deallocate A


                        Select (select max(ISNULL(EndDayTrailsFundPortfolioForPIIPK,0)) + 1 from EndDayTrailsFundPortfolioForPII) LastPK
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
        #endregion 


        public EndDayTrailsFundPortfolioForPII ValidateGenerateEndDayTrailsFundPortfolioForPIIParamFund(DateTime _valueDate, string _fundPK)
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
                        if (!_host.findString(_fundPK.ToLower(), "0", ",") && !string.IsNullOrEmpty(_fundPK))
                        {
                            _paramFund = "And FundPK  in ( " + _fundPK + " ) ";
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
	                    IF NOT EXISTS(select * From EndDayTrailsFundPortfolioForPII A
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
                                return new EndDayTrailsFundPortfolioForPII()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Notes = dr["Notes"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Notes"]),
                                };
                            }
                            else
                            {
                                return new EndDayTrailsFundPortfolioForPII()
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

    }
}
