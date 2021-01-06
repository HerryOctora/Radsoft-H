using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using SucorInvest.Connect;


namespace RFSRepository
{
    public class SIEquityReps
    {
        #region SIEquity
        public string ImportSIEquity(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table SIEquityTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.SIEquityTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromSIEquity(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
                                declare @TAReferenceNo nvarchar(50)
Declare @CurReference nvarchar(100) 
Declare @Date Datetime
declare @LastNo int
declare @periodPK int

begin
DECLARE A CURSOR FOR 
select TaRefNo from SIEquityTemp A
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2


Open A
Fetch Next From A
Into @TAReferenceNo

While @@FETCH_STATUS = 0
BEGIN
declare @InvestmentPK int
Select @InvestmentPK = max(InvestmentPK) + 1 From investment
if isnull(@InvestmentPK,0) = 0 BEGIN  Select @InvestmentPK = isnull(max(InvestmentPK),0) + 1 From investment END  
declare @DealingPK int
Select @DealingPK = max(DealingPK) + 1 From investment
if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
declare @SettlementPK int
Select @SettlementPK = max(SettlementPK) + 1 From investment
if isnull(@SettlementPK,0) = 0 BEGIN  Select @SettlementPK = isnull(max(SettlementPK),0) + 1 From investment END  

        
select @Date = convert(datetime,convert(varchar(10),TradeDate,120))
from SIEquityTemp A
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
where  TaRefNo = @TAReferenceNo

select @PeriodPK = PeriodPK 
from Period 
where @Date between DateFrom and DateTo and [Status] = 2

--exec getJournalReference @Date,'INV',@CurReference out

set @CurReference = dbo.FGetFundJournalReference(@Date,'INV')

Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
substring(right(reference,4),1,2) = month(@Date) 

if exists(Select Top 1 * from cashierReference where Type = 'INV' And PeriodPK = @PeriodPK 
and substring(right(reference,4),1,2) = month(@Date)  )    
BEGIN
	Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
	and substring(right(reference,4),1,2) = month(@Date) 
END
ELSE
BEGIN
	Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
	Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
END 

INSERT INTO [dbo].[Investment]
([InvestmentPK],[DealingPK],[SettlementPK],[HistoryPK],[StatusInvestment],[StatusDealing],[StatusSettlement],[Notes]
,[ValueDate],[MarketPK],[PeriodPK],[Category],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID]
,[CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[AcqPrice]
,[Volume],[Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[DoneAccruedInterest],[LastCouponDate]
,[NextCouponDate],[MaturityDate],[SettlementDate],[AcqDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice]
,[DoneAmount],[Tenor],[CommissionPercent],[LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent]
,[IncomeTaxSellPercent],[IncomeTaxInterestPercent],[IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount]
,[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount],[IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount]
,[CurrencyRate],[AcqPrice1],[AcqDate1],[AcqPrice2],[AcqDate2],[AcqPrice3],[AcqDate3],[AcqPrice4],[AcqDate4]
,[AcqPrice5],[AcqDate5],[SettlementMode],[BoardType],[OrderStatus],[InterestDaysType],[InterestPaymentType]
,[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[Posted],[PostedBy],[PostedTime]
,[Revised],[RevisedBy],[RevisedTime],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],[ApprovedTime]
,[VoidUsersID],[VoidTime],[EntryDealingID],[EntryDealingTime],[UpdateDealingID],[UpdateDealingTime]
,[ApprovedDealingID],[ApprovedDealingTime],[VoidDealingID],[VoidDealingTime],[EntrySettlementID],[EntrySettlementTime]
,[UpdateSettlementID],[UpdateSettlementTime],[ApprovedSettlementID],[ApprovedSettlementTime]
,[VoidSettlementID],[VoidSettlementTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB],[SelectedInvestment]
,[SelectedDealing],[SelectedSettlement],[BankBranchPK],[BankPK],[AcqVolume],[AcqVolume1],[AcqVolume2],[AcqVolume3]
,[AcqVolume4],[AcqVolume5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7]
,[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],[TaxExpensePercent],[TrxBuy],[TrxBuyType]
,[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[OldAmount],[OldInterestPercent],[OldMaturityDate],[InvestmentTrType])
     



select @InvestmentPK [InvestmentPK],@DealingPK [DealingPK],@SettlementPK [SettlementPK],1 HistoryPK,2 StatusInvestment,2 StatusDealing,2 StatusSettlement,null Notes,
convert(datetime,convert(varchar(10),TradeDate,120)) ValueDate,1 MarketPK,PeriodPK,null Category,convert(datetime,convert(varchar(10),TradeDate,120)) InstructionDate,
@CurReference Reference,1 InstrumentTypePK,BuySell TrxType,Case when BuySell = '1' then 'BUY' else 'SELL' end TrxTypeID,C.CounterpartPK,D.InstrumentPK,isnull(E.FundPK,0) FundPK,F.FundCashRefPK,
 cast(Price as numeric(18,8)) OrderPrice,cast(Quantity as numeric(18,8))/100 Lot,100 LotInShare,null RangePrice,0 AcqPrice,cast(Quantity as numeric(18,8)) Volume,cast(TradeAmount as numeric(18,8)) Amount,0 InterestPercent,0 BreakInteresPercent,
0 AccruedInterest,0 DoneAccruedInterest,null LastCouponDate,null NextCouponDate,null MaturityDate,convert(datetime,convert(varchar(10),SettlementDate,120)) SettlementDate,null AcqDate,
'' InvestmentNotes,cast(Quantity as numeric(18,8))/100 DoneLot,cast(Quantity as numeric(18,8)) DoneVolume,cast(Price as numeric(18,8)) DonePrice,cast(TradeAmount as numeric(18,8)) DoneAmount,0 Tenor,
cast(cast(Commission as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  CommissionPercent,
cast(cast(Levy as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  LevyPercent,
0 KPEIPercent,
cast(cast(VAT as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  VATPercent,
cast(cast(WHTCommission as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  WHTPercent,
cast(cast(isnull(case when OtherCharges is null then '0'  end,0) as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  OTCPercent,
cast(cast(SalesTax as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  IncomeTaxSellPercent,
0 IncomeTaxInterestPercent,0 IncomeTaxGainPercent,
cast(Commission as numeric(18,8))  CommissionAmount,
cast(Levy as numeric(18,8))  LevyAmount,
0 KPEIAmount,cast(VAT as numeric(18,8))   VATAmount,
cast(WHTCommission as numeric(18,8))   WHTAmount,
cast(isnull(case when OtherCharges is null then '0'  end,0) as numeric(18,8))  OTCAmount,
cast(SalesTax as numeric(18,8))  IncomeTaxSellAmount,
 0 IncomeTaxInterestAmount,0 IncomeTaxGainAmount,cast(NetSettlementAmount as numeric(18,8))  NetAmount,1 CurrencyRate,
null Acqprice1,null AcqDate1,null Acqprice2,null AcqDate2,null Acqprice3,null AcqDate3,null Acqprice4,null AcqDate4,null Acqprice5,null AcqDate5,
 InstructionType  SettlementMode,1 BoardType,'M' OrderStatus,0 InterestDaysType,0 InterestPaymentType,0 PaymentModeOnMaturity,null PaymentInterestSpecificDate,
 1 PriceMode,0 BitIsAmortized,0 Posted,@UsersID PostedBy,@LastUpdate PostedTime,0 Revised,null RevisedBy,null RevisedTime,
@UsersID EntryUsersID,@LastUpdate EntryTime,null UpdateusersID,null UpdateTime,@UsersID ApprovedUsersID,@LastUpdate ApprovedTime,
null VoidUsersID,null VoidTime,@UsersID EntryDealingID,@LastUpdate EntryDealingTime,null UpdateDealingID,null UpdateDealingTime,
@UsersID ApprovedDealingID,@LastUpdate ApprovedDealignTime,null VoidDealingID,null VoidDealingTime,
@UsersID EntrySettlementID,@LastUpdate EntrySettlementTime,null UpdateSettlementID,null UpdateSettlementTime,
@UsersID ApprovedSettlementID,@LastUpdate ApprovedSettlementTime,null VoidSettlementID,null VoidSettlementTime,@UsersID DBUsersID,
@UsersID DBTerminalID,@LastUpdate LastUpdate,null LastUpdateDB,0 SelectedInvestment,0 SelectedDealing,0 SelectedSettlement,
0 BankBranchPK,0 BankPK,null AcqVolume,null AcqVolume1,null AcqVolume2,null AcqVolume3,null AcqVolume4,null AcqVolume5,
null AcqPrice6,null AcqVolume6,null AcqDate6,null AcqPrice7,null AcqVolume7,null AcqDate7,null AcqPrice8,null AcqVolume8,null AcqDate8,
null AcqPrice9,null AcqVolume9,null AcqDate9,5 TaxExpensePercent,null TrxBuy,null TrxBuyType,0 YieldPercent,0 BitIsRounding,
0 AccruedHoldingAmount,0 OldAmount,0 OldInteresPercent,null OldMaturityDate,3 InvestmentTrType
from SIEquityTemp A
left join Period B on YEAR(CONVERT(char(8), TradeDate, 112)) = year(DateFrom) and B.status = 2
left join Counterpart C on A.BRCode = C.SInvestCode and C.status = 2
left join Instrument D on A.SecurityCode = D.ID and D.status = 2
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
left join FundCashRef F on E.FundPK = F.FundPK and F.status = 2 and F.Type = 3
where  TaRefNo = @TAReferenceNo

Fetch next From A Into @TAReferenceNo
END
Close A
Deallocate A
end
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import SI Equity Done";

                        }

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromSIEquity(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TaRefNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TradeDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SettlementDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BRCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BRName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CounterpartyCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CounterpartyName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CPSafeKeepingAccNumber";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlaceOfSettlement";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);






            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundSafeKeepingAccNumber";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCodeType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BuySell";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Price";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Quantity";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TradeAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Commission";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SalesTax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Levy";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "VAT";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherCharges";
            dc.Unique = false;
            dt.Columns.Add(dc);





            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "GrossSettlementAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WHTCommission";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NetSettlementAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InstructionType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Remarks";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TCReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIReferenceID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SICreationDateTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CutOffStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CancellationReason";
            dc.Unique = false;
            dt.Columns.Add(dc);




            FileInfo excelFile = new System.IO.FileInfo(_fileSource);
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                int i = 2;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                var end = worksheet.Dimension.End;
                while (i <= end.Row)
                {
                    dr = dt.NewRow();
                    if (worksheet.Cells[i, 1].Value == null)
                        dr["TransactionStatus"] = "";
                    else
                        dr["TransactionStatus"] = worksheet.Cells[i, 1].Value.ToString();

                    if (worksheet.Cells[i, 2].Value == null)
                        dr["TaRefNo"] = "";
                    else
                        dr["TaRefNo"] = worksheet.Cells[i, 2].Value.ToString();

                    if (worksheet.Cells[i, 3].Value == null)
                        dr["TradeDate"] = "";
                    else
                        dr["TradeDate"] = worksheet.Cells[i, 3].Value.ToString();

                    if (worksheet.Cells[i, 4].Value == null)
                        dr["SettlementDate"] = "";
                    else
                        dr["SettlementDate"] = worksheet.Cells[i, 4].Value.ToString();

                    if (worksheet.Cells[i, 5].Value == null)
                        dr["IMCode"] = "";
                    else
                        dr["IMCode"] = worksheet.Cells[i, 5].Value.ToString();

                    if (worksheet.Cells[i, 6].Value == null)
                        dr["IMName"] = "";
                    else
                        dr["IMName"] = worksheet.Cells[i, 6].Value.ToString();

                    if (worksheet.Cells[i, 7].Value == null)
                        dr["CBCode"] = "";
                    else
                        dr["CBCode"] = worksheet.Cells[i, 7].Value.ToString();

                    if (worksheet.Cells[i, 8].Value == null)
                        dr["CBName"] = "";
                    else
                        dr["CBName"] = worksheet.Cells[i, 8].Value.ToString();

                    if (worksheet.Cells[i, 9].Value == null)
                        dr["BRCode"] = "";
                    else
                        dr["BRCode"] = worksheet.Cells[i, 9].Value.ToString();

                    if (worksheet.Cells[i, 10].Value == null)
                        dr["BRName"] = "";
                    else
                        dr["BRName"] = worksheet.Cells[i, 10].Value.ToString();

                    if (worksheet.Cells[i, 11].Value == null)
                        dr["CounterpartyCode"] = "";
                    else
                        dr["CounterpartyCode"] = worksheet.Cells[i, 11].Value.ToString();

                    if (worksheet.Cells[i, 12].Value == null)
                        dr["CounterpartyName"] = "";
                    else
                        dr["CounterpartyName"] = worksheet.Cells[i, 12].Value.ToString();

                    if (worksheet.Cells[i, 13].Value == null)
                        dr["CPSafeKeepingAccNumber"] = "";
                    else
                        dr["CPSafeKeepingAccNumber"] = worksheet.Cells[i, 13].Value.ToString();

                    if (worksheet.Cells[i, 14].Value == null)
                        dr["PlaceOfSettlement"] = 0;
                    else
                        dr["PlaceOfSettlement"] = worksheet.Cells[i, 14].Value.ToString();

                    if (worksheet.Cells[i, 15].Value == null)
                        dr["FundCode"] = 0;
                    else
                        dr["FundCode"] = worksheet.Cells[i, 15].Value.ToString();

                    if (worksheet.Cells[i, 16].Value == null)
                        dr["FundName"] = 0;
                    else
                        dr["FundName"] = worksheet.Cells[i, 16].Value.ToString();

                    if (worksheet.Cells[i, 17].Value == null)
                        dr["FundSafeKeepingAccNumber"] = 0;
                    else
                        dr["FundSafeKeepingAccNumber"] = worksheet.Cells[i, 17].Value.ToString();

                    if (worksheet.Cells[i, 18].Value == null)
                        dr["RegistrationType"] = 0;
                    else
                        dr["RegistrationType"] = worksheet.Cells[i, 18].Value.ToString();

                    if (worksheet.Cells[i, 19].Value == null)
                        dr["SecurityType"] = 0;
                    else
                        dr["SecurityType"] = worksheet.Cells[i, 19].Value.ToString();

                    if (worksheet.Cells[i, 20].Value == null)
                        dr["SecurityCodeType"] = 0;
                    else
                        dr["SecurityCodeType"] = worksheet.Cells[i, 20].Value.ToString();

                    if (worksheet.Cells[i, 21].Value == null)
                        dr["SecurityCode"] = 0;
                    else
                        dr["SecurityCode"] = worksheet.Cells[i, 21].Value.ToString();

                    if (worksheet.Cells[i, 22].Value == null)
                        dr["SecurityName"] = 0;
                    else
                        dr["SecurityName"] = worksheet.Cells[i, 22].Value.ToString();

                    if (worksheet.Cells[i, 24].Value == null)
                        dr["CCY"] = 0;
                    else
                        dr["CCY"] = worksheet.Cells[i, 24].Value.ToString();

                    if (worksheet.Cells[i, 23].Value == null)
                        dr["BuySell"] = 0;
                    else
                        dr["BuySell"] = worksheet.Cells[i, 23].Value.ToString();

                    if (worksheet.Cells[i, 25].Value == null)
                        dr["Price"] = 0;
                    else
                        dr["Price"] = worksheet.Cells[i, 25].Value.ToString();

                    if (worksheet.Cells[i, 26].Value == null)
                        dr["Quantity"] = 0;
                    else
                        dr["Quantity"] = worksheet.Cells[i, 26].Value.ToString();

                    if (worksheet.Cells[i, 27].Value == null)
                        dr["TradeAmount"] = 0;
                    else
                        dr["TradeAmount"] = worksheet.Cells[i, 27].Value.ToString();

                    if (worksheet.Cells[i, 28].Value == null)
                        dr["Commission"] = 0;
                    else
                        dr["Commission"] = worksheet.Cells[i, 28].Value.ToString();

                    if (worksheet.Cells[i, 29].Value == null)
                        dr["SalesTax"] = 0;
                    else
                        dr["SalesTax"] = worksheet.Cells[i, 29].Value.ToString();

                    if (worksheet.Cells[i, 30].Value == null)
                        dr["Levy"] = 0;
                    else
                        dr["Levy"] = worksheet.Cells[i, 30].Value.ToString();

                    if (worksheet.Cells[i, 31].Value == null)
                        dr["VAT"] = 0;
                    else
                        dr["VAT"] = worksheet.Cells[i, 31].Value.ToString();

                    if (worksheet.Cells[i, 32].Value == null)
                        dr["OtherCharges"] = 0;
                    else
                        dr["OtherCharges"] = worksheet.Cells[i, 32].Value.ToString();

                    if (worksheet.Cells[i, 33].Value == null)
                        dr["GrossSettlementAmount"] = 0;
                    else
                        dr["GrossSettlementAmount"] = worksheet.Cells[i, 33].Value.ToString();

                    if (worksheet.Cells[i, 34].Value == null)
                        dr["WHTCommission"] = 0;
                    else
                        dr["WHTCommission"] = worksheet.Cells[i, 34].Value.ToString();

                    if (worksheet.Cells[i, 35].Value == null)
                        dr["NetSettlementAmount"] = 0;
                    else
                        dr["NetSettlementAmount"] = worksheet.Cells[i, 35].Value.ToString();

                    if (worksheet.Cells[i, 36].Value == null)
                        dr["InstructionType"] = 0;
                    else
                        dr["InstructionType"] = worksheet.Cells[i, 36].Value.ToString();

                    if (worksheet.Cells[i, 37].Value == null)
                        dr["Remarks"] = 0;
                    else
                        dr["Remarks"] = worksheet.Cells[i, 37].Value.ToString();

                    if (worksheet.Cells[i, 38].Value == null)
                        dr["TCReferenceNo"] = 0;
                    else
                        dr["TCReferenceNo"] = worksheet.Cells[i, 38].Value.ToString();

                    if (worksheet.Cells[i, 39].Value == null)
                        dr["SIReferenceID"] = 0;
                    else
                        dr["SIReferenceID"] = worksheet.Cells[i, 39].Value.ToString();

                    if (worksheet.Cells[i, 40].Value == null)
                        dr["SICreationDateTime"] = 0;
                    else
                        dr["SICreationDateTime"] = worksheet.Cells[i, 40].Value.ToString();

                    if (worksheet.Cells[i, 41].Value == null)
                        dr["CutOffStatus"] = 0;
                    else
                        dr["CutOffStatus"] = worksheet.Cells[i, 41].Value.ToString();

                    if (worksheet.Cells[i, 42].Value == null)
                        dr["CancellationReason"] = 0;
                    else
                        dr["CancellationReason"] = worksheet.Cells[i, 42].Value.ToString();

                    //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                    if (dr["TransactionStatus"].Equals(null) != true ||
                        dr["TaRefNo"].Equals(null) != true ||
                        dr["TradeDate"].Equals(null) != true ||
                        dr["SettlementDate"].Equals(null) != true ||
                        dr["IMCode"].Equals(null) != true ||
                        dr["IMName"].Equals(null) != true ||
                        dr["CBCode"].Equals(null) != true ||
                        dr["CBName"].Equals(null) != true ||
                        dr["BRCode"].Equals(null) != true ||
                        dr["BRName"].Equals(null) != true ||
                        dr["CounterpartyCode"].Equals(null) != true ||
                        dr["CounterpartyName"].Equals(null) != true ||
                        dr["CPSafeKeepingAccNumber"].Equals(null) != true ||
                        dr["PlaceOfSettlement"].Equals(null) != true ||
                        dr["FundCode"].Equals(null) != true ||

                        dr["FundName"].Equals(null) != true ||
                        dr["FundSafeKeepingAccNumber"].Equals(null) != true ||
                        dr["RegistrationType"].Equals(null) != true ||
                        dr["SecurityType"].Equals(null) != true ||
                        dr["SecurityCodeType"].Equals(null) != true ||
                        dr["SecurityCode"].Equals(null) != true ||
                        dr["CCY"].Equals(null) != true ||
                        dr["SecurityaName"].Equals(null) != true ||
                        dr["BuySell"].Equals(null) != true ||
                        dr["Price"].Equals(null) != true ||
                        dr["Quantity"].Equals(null) != true ||
                        dr["TradeAmount"].Equals(null) != true ||
                        dr["Commission"].Equals(null) != true ||
                        dr["SalesTax"].Equals(null) != true ||
                        dr["Levy"].Equals(null) != true ||

                        dr["VAT"].Equals(null) != true ||
                        dr["OtherCharges"].Equals(null) != true ||
                        dr["GrossSettlementAmount"].Equals(null) != true ||
                        dr["WHTCommission"].Equals(null) != true ||
                        dr["NetSettlementAmount"].Equals(null) != true ||
                        dr["InstructionType"].Equals(null) != true ||
                        dr["Remarks"].Equals(null) != true ||
                        dr["TCReferenceNo"].Equals(null) != true ||
                        dr["SIReferenceID"].Equals(null) != true ||
                        dr["SICreationDateTime"].Equals(null) != true ||
                        dr["CutOffStatus"].Equals(null) != true ||

                        dr["CancellationReason"].Equals(null) != true) { dt.Rows.Add(dr); }
                    i++;

                }
            }

            return dt;
        }

        public string ImportSIEquityCsv(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table SIEquityTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.SIEquityTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromSIEquityCsv(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
                                declare @TAReferenceNo nvarchar(50)
Declare @CurReference nvarchar(100) 
Declare @Date Datetime
declare @LastNo int
declare @periodPK int

begin
DECLARE A CURSOR FOR 
select TaRefNo from SIEquityTemp A
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2


Open A
Fetch Next From A
Into @TAReferenceNo

While @@FETCH_STATUS = 0
BEGIN
declare @InvestmentPK int
Select @InvestmentPK = max(InvestmentPK) + 1 From investment
if isnull(@InvestmentPK,0) = 0 BEGIN  Select @InvestmentPK = isnull(max(InvestmentPK),0) + 1 From investment END  
declare @DealingPK int
Select @DealingPK = max(DealingPK) + 1 From investment
if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
declare @SettlementPK int
Select @SettlementPK = max(SettlementPK) + 1 From investment
if isnull(@SettlementPK,0) = 0 BEGIN  Select @SettlementPK = isnull(max(SettlementPK),0) + 1 From investment END  

        
select @Date = convert(datetime,convert(varchar(10),TradeDate,120))
from SIEquityTemp A
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
where  TaRefNo = @TAReferenceNo

select @PeriodPK = PeriodPK 
from Period 
where @Date between DateFrom and DateTo and [Status] = 2

--exec getJournalReference @Date,'INV',@CurReference out

set @CurReference = dbo.FGetFundJournalReference(@Date,'INV')

Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
substring(right(reference,4),1,2) = month(@Date) 

if exists(Select Top 1 * from cashierReference where Type = 'INV' And PeriodPK = @PeriodPK 
and substring(right(reference,4),1,2) = month(@Date)  )    
BEGIN
	Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
	and substring(right(reference,4),1,2) = month(@Date) 
END
ELSE
BEGIN
	Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
	Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
END 

INSERT INTO [dbo].[Investment]
([InvestmentPK],[DealingPK],[SettlementPK],[HistoryPK],[StatusInvestment],[StatusDealing],[StatusSettlement],[Notes]
,[ValueDate],[MarketPK],[PeriodPK],[Category],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID]
,[CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[AcqPrice]
,[Volume],[Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[DoneAccruedInterest],[LastCouponDate]
,[NextCouponDate],[MaturityDate],[SettlementDate],[AcqDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice]
,[DoneAmount],[Tenor],[CommissionPercent],[LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent]
,[IncomeTaxSellPercent],[IncomeTaxInterestPercent],[IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount]
,[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount],[IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount]
,[CurrencyRate],[AcqPrice1],[AcqDate1],[AcqPrice2],[AcqDate2],[AcqPrice3],[AcqDate3],[AcqPrice4],[AcqDate4]
,[AcqPrice5],[AcqDate5],[SettlementMode],[BoardType],[OrderStatus],[InterestDaysType],[InterestPaymentType]
,[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[Posted],[PostedBy],[PostedTime]
,[Revised],[RevisedBy],[RevisedTime],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],[ApprovedTime]
,[VoidUsersID],[VoidTime],[EntryDealingID],[EntryDealingTime],[UpdateDealingID],[UpdateDealingTime]
,[ApprovedDealingID],[ApprovedDealingTime],[VoidDealingID],[VoidDealingTime],[EntrySettlementID],[EntrySettlementTime]
,[UpdateSettlementID],[UpdateSettlementTime],[ApprovedSettlementID],[ApprovedSettlementTime]
,[VoidSettlementID],[VoidSettlementTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB],[SelectedInvestment]
,[SelectedDealing],[SelectedSettlement],[BankBranchPK],[BankPK],[AcqVolume],[AcqVolume1],[AcqVolume2],[AcqVolume3]
,[AcqVolume4],[AcqVolume5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7]
,[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],[TaxExpensePercent],[TrxBuy],[TrxBuyType]
,[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[OldAmount],[OldInterestPercent],[OldMaturityDate],[InvestmentTrType])
     



select @InvestmentPK [InvestmentPK],@DealingPK [DealingPK],@SettlementPK [SettlementPK],1 HistoryPK,2 StatusInvestment,2 StatusDealing,2 StatusSettlement,null Notes,
convert(datetime,convert(varchar(10),TradeDate,120)) ValueDate,1 MarketPK,PeriodPK,null Category,convert(datetime,convert(varchar(10),TradeDate,120)) InstructionDate,
@CurReference Reference,1 InstrumentTypePK,BuySell TrxType,Case when BuySell = '1' then 'BUY' else 'SELL' end TrxTypeID,C.CounterpartPK,D.InstrumentPK,isnull(E.FundPK,0) FundPK,F.FundCashRefPK,
 cast(Price as numeric(18,8)) OrderPrice,cast(Quantity as numeric(18,8))/100 Lot,100 LotInShare,null RangePrice,0 AcqPrice,cast(Quantity as numeric(18,8)) Volume,cast(TradeAmount as numeric(18,8)) Amount,0 InterestPercent,0 BreakInteresPercent,
0 AccruedInterest,0 DoneAccruedInterest,null LastCouponDate,null NextCouponDate,null MaturityDate,convert(datetime,convert(varchar(10),SettlementDate,120)) SettlementDate,null AcqDate,
'' InvestmentNotes,cast(Quantity as numeric(18,8))/100 DoneLot,cast(Quantity as numeric(18,8)) DoneVolume,cast(Price as numeric(18,8)) DonePrice,cast(TradeAmount as numeric(18,8)) DoneAmount,0 Tenor,
cast(cast(Commission as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  CommissionPercent,
cast(cast(Levy as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  LevyPercent,
0 KPEIPercent,
cast(cast(VAT as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  VATPercent,
cast(cast(WHTCommission as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  WHTPercent,
cast(cast(isnull(case when OtherCharges is null then '0'  end,0) as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  OTCPercent,
cast(cast(SalesTax as numeric(18,8))/cast(TradeAmount as numeric(18,8))*100 as numeric(18,8))  IncomeTaxSellPercent,
0 IncomeTaxInterestPercent,0 IncomeTaxGainPercent,
cast(Commission as numeric(18,8))  CommissionAmount,
cast(Levy as numeric(18,8))  LevyAmount,
0 KPEIAmount,cast(VAT as numeric(18,8))   VATAmount,
cast(WHTCommission as numeric(18,8))   WHTAmount,
cast(isnull(case when OtherCharges is null then '0'  end,0) as numeric(18,8))  OTCAmount,
cast(SalesTax as numeric(18,8))  IncomeTaxSellAmount,
 0 IncomeTaxInterestAmount,0 IncomeTaxGainAmount,cast(NetSettlementAmount as numeric(18,8))  NetAmount,1 CurrencyRate,
null Acqprice1,null AcqDate1,null Acqprice2,null AcqDate2,null Acqprice3,null AcqDate3,null Acqprice4,null AcqDate4,null Acqprice5,null AcqDate5,
 InstructionType  SettlementMode,1 BoardType,'M' OrderStatus,0 InterestDaysType,0 InterestPaymentType,0 PaymentModeOnMaturity,null PaymentInterestSpecificDate,
 1 PriceMode,0 BitIsAmortized,0 Posted,@UsersID PostedBy,@LastUpdate PostedTime,0 Revised,null RevisedBy,null RevisedTime,
@UsersID EntryUsersID,@LastUpdate EntryTime,null UpdateusersID,null UpdateTime,@UsersID ApprovedUsersID,@LastUpdate ApprovedTime,
null VoidUsersID,null VoidTime,@UsersID EntryDealingID,@LastUpdate EntryDealingTime,null UpdateDealingID,null UpdateDealingTime,
@UsersID ApprovedDealingID,@LastUpdate ApprovedDealignTime,null VoidDealingID,null VoidDealingTime,
@UsersID EntrySettlementID,@LastUpdate EntrySettlementTime,null UpdateSettlementID,null UpdateSettlementTime,
@UsersID ApprovedSettlementID,@LastUpdate ApprovedSettlementTime,null VoidSettlementID,null VoidSettlementTime,@UsersID DBUsersID,
@UsersID DBTerminalID,@LastUpdate LastUpdate,null LastUpdateDB,0 SelectedInvestment,0 SelectedDealing,0 SelectedSettlement,
0 BankBranchPK,0 BankPK,null AcqVolume,null AcqVolume1,null AcqVolume2,null AcqVolume3,null AcqVolume4,null AcqVolume5,
null AcqPrice6,null AcqVolume6,null AcqDate6,null AcqPrice7,null AcqVolume7,null AcqDate7,null AcqPrice8,null AcqVolume8,null AcqDate8,
null AcqPrice9,null AcqVolume9,null AcqDate9,5 TaxExpensePercent,null TrxBuy,null TrxBuyType,0 YieldPercent,0 BitIsRounding,
0 AccruedHoldingAmount,0 OldAmount,0 OldInteresPercent,null OldMaturityDate,3 InvestmentTrType
from SIEquityTemp A
left join Period B on YEAR(CONVERT(char(8), TradeDate, 112)) = year(DateFrom) and B.status = 2
left join Counterpart C on A.BRCode = C.SInvestCode and C.status = 2
left join Instrument D on A.SecurityCode = D.ID and D.status = 2
left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
left join FundCashRef F on E.FundPK = F.FundPK and F.status = 2 and F.Type = 3
where  TaRefNo = @TAReferenceNo

Fetch next From A Into @TAReferenceNo
END
Close A
Deallocate A
end
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import SI Equity Done";

                        }

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromSIEquityCsv(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TaRefNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TradeDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SettlementDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BRCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BRName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CounterpartyCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CounterpartyName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CPSafeKeepingAccNumber";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlaceOfSettlement";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);






            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundSafeKeepingAccNumber";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCodeType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BuySell";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CCY";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Price";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Quantity";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TradeAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Commission";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SalesTax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Levy";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "VAT";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherCharges";
            dc.Unique = false;
            dt.Columns.Add(dc);





            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "GrossSettlementAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WHTCommission";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NetSettlementAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InstructionType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Remarks";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TCReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIReferenceID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SICreationDateTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CutOffStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CancellationReason";
            dc.Unique = false;
            dt.Columns.Add(dc);




            StreamReader sr = new StreamReader(_fileSource);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { ',' });
                dr = dt.NewRow();
                dr["TransactionStatus"] = s[0];
                dr["TaRefNo"] = s[1];
                dr["TradeDate"] = s[2];
                dr["SettlementDate"] = s[3];
                dr["IMCode"] = s[4];
                dr["IMName"] = s[5];
                dr["CBCode"] = s[6];
                dr["CBName"] = s[7];
                dr["BRCode"] = s[8];
                dr["BRName"] = s[9];
                dr["CounterpartyCode"] = s[10];
                dr["CounterpartyName"] = s[11];
                dr["CPSafeKeepingAccNumber"] = s[12];
                dr["PlaceOfSettlement"] = s[13];
                dr["FundCode"] = s[14];
                dr["FundName"] = s[15];
                dr["FundSafeKeepingAccNumber"] = s[16];
                dr["RegistrationType"] = s[17];
                dr["SecurityType"] = s[18];
                dr["SecurityCodeType"] = s[19];
                dr["SecurityCode"] = s[20];
                dr["SecurityName"] = s[21];
                dr["BuySell"] = s[22];
                dr["CCY"] = s[23];
                dr["Price"] = s[24];
                dr["Quantity"] = s[25];
                dr["TradeAmount"] = s[26];
                dr["Commission"] = s[27];
                dr["SalesTax"] = s[28];
                dr["Levy"] = s[29];
                dr["VAT"] = s[30];
                dr["OtherCharges"] = s[31];
                dr["GrossSettlementAmount"] = s[32];
                dr["WHTCommission"] = s[33];
                dr["NetSettlementAmount"] = s[34];
                dr["InstructionType"] = s[35];
                dr["Remarks"] = s[36];
                dr["TCReferenceNo"] = s[37];
                dr["SIReferenceID"] = s[38];
                dr["SICreationDateTime"] = s[39];
                dr["CutOffStatus"] = s[40];
                dr["CancellationReason"] = s[41];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        #endregion

        #region SIDeposito
        public string ImportSIDeposito(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table SIDepositoTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.SIDepositoTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromSIDeposito(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
                                    Declare @SIReferenceID nvarchar(50)
                                    Declare @CurReference nvarchar(100) 
                                    Declare @Date Datetime
                                    declare @LastNo int
                                    declare @periodPK int
                                    Declare @BankID nvarchar(200)
                                    Declare @MaxInstrumentPK int
                                    declare @BankPK int
                                    declare @TrxType int
                                    declare @InterestPercent numeric(18,8)
									declare @MaturityDate date

                                    DECLARE A CURSOR FOR 
                                    select SIReferenceID from SIDepositoTemp A
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status in (1,2)
                                    where ActionType <> 3 
                                    --E.FundPK is not null 

                                    Open A
                                    Fetch Next From A
                                    Into @SIReferenceID

                                    While @@FETCH_STATUS = 0
                                    BEGIN
                                    declare @InvestmentPK int
                                    Select @InvestmentPK = max(InvestmentPK) + 1 From investment
                                    if isnull(@InvestmentPK,0) = 0 BEGIN  Select @InvestmentPK = isnull(max(InvestmentPK),0) + 1 From investment END  
                                    declare @DealingPK int
                                    Select @DealingPK = max(DealingPK) + 1 From investment
                                    if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                                    declare @SettlementPK int
                                    Select @SettlementPK = max(SettlementPK) + 1 From investment
                                    if isnull(@SettlementPK,0) = 0 BEGIN  Select @SettlementPK = isnull(max(SettlementPK),0) + 1 From investment END  
        
                                    select @Date = convert(datetime,convert(varchar(10),InputCancelationDate,120)) 
                                    from SIDepositoTemp A
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
                                    where SIReferenceID = @SIReferenceID  and ActionType <> 3
                                    --E.FundPK is not null and 

                                    select @PeriodPK = PeriodPK 
                                    from Period 
                                    where @Date between DateFrom and DateTo and [Status] = 2

                                    --insrument

                                    select @BankPK = isnull(B.BankPK,0),@InterestPercent = InterestRatePS, @MaturityDate = MaturityDate,@TrxType = ActionType from SIDepositoTemp A
                                    left join BankBranch B on A.BranchCode = B.PTPCode and B.status = 2
									where A.SIReferenceID = @SIReferenceID
									
									if exists (
									select * from Instrument where bankpk = @BankPK and InterestPercent = @InterestPercent and MaturityDate = @MaturityDate and status in (1,2)
									) 
										select @MaxInstrumentPK = InstrumentPK from Instrument where bankpk = @BankPK and InterestPercent = @InterestPercent and MaturityDate = @MaturityDate and status in (1,2)
									else
									begin
										if @TrxType = 1 
										begin
											select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
											select @BankID = ID From Bank Where BankPK = @BankPK and Status = 2
                                    
											set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)

											Insert Into Instrument(InstrumentPK,HistoryPK,Status,ID,Name,InstrumentTypePK,DepositoTypePK,lotinshare,BankPK,InterestPercent,MaturityDate,CurrencyPK,Category,TaxExpensePercent,[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)
											Select @MaxInstrumentPK,1,2,@BankID, 'TDP ' +@BankID,5,1,1,@BankPK,InterestRatePS,MaturityDate,B.CurrencyPK,'',20,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from SIDepositoTemp A 
											left join Currency B on A.CCY = B.ID and B.Status in (1,2)
											where A.SIReferenceID = @SIReferenceID
										end
										else
											set @MaxInstrumentPK = 0
									end
		                    
                                    --reference
                                    set @CurReference = dbo.FGetFundJournalReference(@Date,'INV')

                                    Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
                                    substring(right(reference,4),1,2) = month(@Date) 

                                    if exists(Select Top 1 * from cashierReference where Type = 'INV' And PeriodPK = @PeriodPK 
                                    and substring(right(reference,4),1,2) = month(@Date)  )    
                                    BEGIN
	                                    Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
	                                    and substring(right(reference,4),1,2) = month(@Date) 
                                    END
                                    ELSE
                                    BEGIN
	                                    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
	                                    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
                                    END 

                                    INSERT INTO [dbo].[Investment]
                                    ([InvestmentPK],[DealingPK],[SettlementPK],[HistoryPK],[StatusInvestment],[StatusDealing],[StatusSettlement],[Notes]
                                    ,[ValueDate],[MarketPK],[PeriodPK],[Category],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID]
                                    ,[CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[AcqPrice]
                                    ,[Volume],[Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[DoneAccruedInterest],[LastCouponDate]
                                    ,[NextCouponDate],[MaturityDate],[SettlementDate],[AcqDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice]
                                    ,[DoneAmount],[Tenor],[CommissionPercent],[LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent]
                                    ,[IncomeTaxSellPercent],[IncomeTaxInterestPercent],[IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount]
                                    ,[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount],[IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount]
                                    ,[CurrencyRate],[AcqPrice1],[AcqDate1],[AcqPrice2],[AcqDate2],[AcqPrice3],[AcqDate3],[AcqPrice4],[AcqDate4]
                                    ,[AcqPrice5],[AcqDate5],[SettlementMode],[BoardType],[OrderStatus],[InterestDaysType],[InterestPaymentType]
                                    ,[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[Posted],[PostedBy],[PostedTime]
                                    ,[Revised],[RevisedBy],[RevisedTime],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],[ApprovedTime]
                                    ,[VoidUsersID],[VoidTime],[EntryDealingID],[EntryDealingTime],[UpdateDealingID],[UpdateDealingTime]
                                    ,[ApprovedDealingID],[ApprovedDealingTime],[VoidDealingID],[VoidDealingTime],[EntrySettlementID],[EntrySettlementTime]
                                    ,[UpdateSettlementID],[UpdateSettlementTime],[ApprovedSettlementID],[ApprovedSettlementTime]
                                    ,[VoidSettlementID],[VoidSettlementTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB],[SelectedInvestment]
                                    ,[SelectedDealing],[SelectedSettlement],[BankBranchPK],[BankPK],[AcqVolume],[AcqVolume1],[AcqVolume2],[AcqVolume3]
                                    ,[AcqVolume4],[AcqVolume5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7]
                                    ,[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],[TaxExpensePercent],[TrxBuy],[TrxBuyType]
                                    ,[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[OldAmount],[OldInterestPercent],[OldMaturityDate],[InvestmentTrType])
     


                                    select @InvestmentPK [InvestmentPK],@DealingPK [DealingPK],@SettlementPK [SettlementPK],1 HistoryPK,2 StatusInvestment,2 StatusDealing,2 StatusSettlement,null Notes,
                                    case when ActionType = 1 then convert(datetime,convert(varchar(10),PlacementDate,120)) else  case when WithdrawalDate = 0 then null else convert(datetime,convert(varchar(10),WithdrawalDate,120)) end end ValueDate,
                                    1 MarketPK,PeriodPK,case when ActionType = 1 then case when DATEDIFF(day,cast(PlacementDate as date),cast(A.MaturityDate as date)) <= 30 then 'Deposito On Call' else 'Deposito Normal' end else 
									case when DATEDIFF(day,cast(WithdrawalDate as date),cast(A.MaturityDate as date)) <= 30 then 'Deposito On Call' else 'Deposito Normal' end end Category,convert(datetime,convert(varchar(10),InputCancelationDate,120)) InstructionDate,
                                    @CurReference Reference,5 InstrumentTypePK,Case when ActionType = 1 then 1 else 2 end TrxType,Case when ActionType = 1 then 'PLACEMENT' else 'LIQUIDATE' end  TrxTypeID,
                                    0 CounterpartPK,@MaxInstrumentPK,E.FundPK,F.FundCashRefPK,1 OrderPrice,0 Lot,100 LotInShare,null RangePrice,0 AcqPrice,
                                     cast(Principle as decimal(22,2)) Volume,cast(Principle as decimal(22,2)) Amount,cast(A.InterestRatePS as decimal(22,8)) InterestPercent,case when ActionType = 1 then 0 else cast(isnull(A.AdjustedInterestRatePS,0) as decimal(22,8)) end BreakInteresPercent,
                                    case when ActionType = 1 then 0 else cast(isnull(WithdrawalInterest,0) as decimal(18,8)) end AccruedInterest,case when ActionType = 1 then 0 else cast(WithdrawalInterest as decimal(22,2)) end DoneAccruedInterest,
                                    null LastCouponDate,null NextCouponDate,convert(datetime,convert(varchar(10),A.MaturityDate,120)) MaturityDate,case when ActionType = 1 then convert(datetime,convert(varchar(10),PlacementDate,120)) else case when WithdrawalDate = 0 then null else convert(datetime,convert(varchar(10),WithdrawalDate,120)) end end SettlementDate,
                                    convert(datetime,convert(varchar(10),PlacementDate,120)) AcqDate,Case when ActionType = 1 then 'PLACEMENT' else 'LIQUIDATE' end InvestmentNotes,0 DoneLot,cast(Principle as decimal(22,2)) DoneVolume,1 DonePrice,cast(Principle as decimal(22,2)) DoneAmount,0 Tenor,
                                    0  CommissionPercent,0  LevyPercent,0 KPEIPercent,0  VATPercent,0  WHTPercent,0  OTCPercent,0  IncomeTaxSellPercent,0 IncomeTaxInterestPercent,0 IncomeTaxGainPercent,
                                    0  CommissionAmount,0  LevyAmount,0 KPEIAmount,0   VATAmount,0   WHTAmount,0  OTCAmount,0  IncomeTaxSellAmount,
                                    0 IncomeTaxInterestAmount,0 IncomeTaxGainAmount,cast(Principle as decimal(22,2))  NetAmount,1 CurrencyRate,
                                    null Acqprice1,null AcqDate1,null Acqprice2,null AcqDate2,null Acqprice3,null AcqDate3,null Acqprice4,null AcqDate4,null Acqprice5,null AcqDate5,
                                    case when ActionType = 1 then 1 else 2 end  SettlementMode,0 BoardType,'M' OrderStatus,
                                    case when InterestDayCountConvention = 1 then 2 when InterestDayCountConvention = 2 then 4 else 0 end InterestDaysType,1 InterestPaymentType,1 PaymentModeOnMaturity,null PaymentInterestSpecificDate,
                                     0 PriceMode,0 BitIsAmortized,0 Posted,@UsersID PostedBy,@LastUpdate PostedTime,0 Revised,null RevisedBy,null RevisedTime,
                                    @UsersID EntryUsersID,@LastUpdate EntryTime,null UpdateusersID,null UpdateTime,@UsersID ApprovedUsersID,@LastUpdate ApprovedTime,
                                    null VoidUsersID,null VoidTime,@UsersID EntryDealingID,@LastUpdate EntryDealingTime,null UpdateDealingID,null UpdateDealingTime,
                                    @UsersID ApprovedDealingID,@LastUpdate ApprovedDealignTime,null VoidDealingID,null VoidDealingTime,
                                    @UsersID EntrySettlementID,@LastUpdate EntrySettlementTime,null UpdateSettlementID,null UpdateSettlementTime,
                                    @UsersID ApprovedSettlementID,@LastUpdate ApprovedSettlementTime,null VoidSettlementID,null VoidSettlementTime,@UsersID DBUsersID,
                                    @UsersID DBTerminalID,@LastUpdate LastUpdate,null LastUpdateDB,0 SelectedInvestment,0 SelectedDealing,0 SelectedSettlement,
                                    H.BankBranchPK BankBranchPK,H.BankPK BankPK,null AcqVolume,null AcqVolume1,null AcqVolume2,null AcqVolume3,null AcqVolume4,null AcqVolume5,
                                    null AcqPrice6,null AcqVolume6,null AcqDate6,null AcqPrice7,null AcqVolume7,null AcqDate7,null AcqPrice8,null AcqVolume8,null AcqDate8,
                                    null AcqPrice9,null AcqVolume9,null AcqDate9,20 TaxExpensePercent,null TrxBuy,null TrxBuyType,0 YieldPercent,0 BitIsRounding,
                                    0 AccruedHoldingAmount,0 OldAmount,0 OldInteresPercent,null OldMaturityDate,3 InvestmentTrType
                                    from SIDepositoTemp A
                                    left join Period B on YEAR(CONVERT(char(8), InputCancelationDate, 112)) = year(DateFrom) and B.status = 2
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
                                    left join FundCashRef F on E.FundPK = F.FundPK and F.status = 2 and F.Type = 3
                                    left join BankBranch H on A.BranchCode = H.PTPCode and H.status = 2
                                    where SIReferenceID = @SIReferenceID  and ActionType <> 3
                                    --E.FundPK is not null 

                                    Fetch next From A Into @SIReferenceID
                                    END
                                    Close A
                                    Deallocate A
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import SI Deposito Done";

                        }

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromSIDeposito(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ActionType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InputCancelationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BranchCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BranchName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCashACName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCashACNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CCY";
            dc.Unique = false;
            dt.Columns.Add(dc);






            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Principle";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestRatePS";
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
            dc.ColumnName = "InterestFrequency";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestDayCountConvention";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ShariaDeposit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AdjustedInterestRatePS";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalPrinciple";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalInterest";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalWithdrawalAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RolloverDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RolloverType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewPrincipleAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);





            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewInterestratePS";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewMaturityDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AmountToBeTransferred";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatutoryType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ContactPerson";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TelephoneNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FaxNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ParentReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Description";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIReferenceID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SICreationDateTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CutOffStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CancelationReason";
            dc.Unique = false;
            dt.Columns.Add(dc);





            FileInfo excelFile = new FileInfo(_fileSource);
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                int i = 2;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                var end = worksheet.Dimension.End;
                while (i <= end.Row)
                {
                    dr = dt.NewRow();
                    if (worksheet.Cells[i, 1].Value == null)
                        dr["TransactionStatus"] = "";
                    else
                        dr["TransactionStatus"] = worksheet.Cells[i, 1].Value.ToString();

                    if (worksheet.Cells[i, 2].Value == null)
                        dr["ActionType"] = "";
                    else
                        dr["ActionType"] = worksheet.Cells[i, 2].Value.ToString();

                    if (worksheet.Cells[i, 3].Value == null)
                        dr["InputCancelationDate"] = "";
                    else
                        dr["InputCancelationDate"] = worksheet.Cells[i, 3].Value.ToString();

                    if (worksheet.Cells[i, 4].Value == null)
                        dr["IMCode"] = "";
                    else
                        dr["IMCode"] = worksheet.Cells[i, 4].Value.ToString();

                    if (worksheet.Cells[i, 5].Value == null)
                        dr["IMName"] = "";
                    else
                        dr["IMName"] = worksheet.Cells[i, 5].Value.ToString();

                    if (worksheet.Cells[i, 6].Value == null)
                        dr["CBCode"] = "";
                    else
                        dr["CBCode"] = worksheet.Cells[i, 6].Value.ToString();

                    if (worksheet.Cells[i, 7].Value == null)
                        dr["CBName"] = "";
                    else
                        dr["CBName"] = worksheet.Cells[i, 7].Value.ToString();

                    if (worksheet.Cells[i, 8].Value == null)
                        dr["FundCode"] = "";
                    else
                        dr["FundCode"] = worksheet.Cells[i, 8].Value.ToString();

                    if (worksheet.Cells[i, 9].Value == null)
                        dr["FundName"] = "";
                    else
                        dr["FundName"] = worksheet.Cells[i, 9].Value.ToString();

                    if (worksheet.Cells[i, 10].Value == null)
                        dr["PlacementBankCode"] = "";
                    else
                        dr["PlacementBankCode"] = worksheet.Cells[i, 10].Value.ToString();

                    if (worksheet.Cells[i, 11].Value == null)
                        dr["PlacementBankName"] = "";
                    else
                        dr["PlacementBankName"] = worksheet.Cells[i, 11].Value.ToString();

                    if (worksheet.Cells[i, 12].Value == null)
                        dr["BranchCode"] = "";
                    else
                        dr["BranchCode"] = worksheet.Cells[i, 12].Value.ToString();

                    if (worksheet.Cells[i, 13].Value == null)
                        dr["BranchName"] = "";
                    else
                        dr["BranchName"] = worksheet.Cells[i, 13].Value.ToString();

                    if (worksheet.Cells[i, 14].Value == null)
                        dr["PlacementBankCashACName"] = 0;
                    else
                        dr["PlacementBankCashACName"] = worksheet.Cells[i, 14].Value.ToString();

                    if (worksheet.Cells[i, 15].Value == null)
                        dr["PlacementBankCashACNo"] = 0;
                    else
                        dr["PlacementBankCashACNo"] = worksheet.Cells[i, 15].Value.ToString();

                    if (worksheet.Cells[i, 16].Value == null)
                        dr["CCY"] = 0;
                    else
                        dr["CCY"] = worksheet.Cells[i, 16].Value.ToString();

                    if (worksheet.Cells[i, 17].Value == null)
                        dr["Principle"] = 0;
                    else
                        dr["Principle"] = worksheet.Cells[i, 17].Value.ToString();

                    if (worksheet.Cells[i, 18].Value == null)
                        dr["InterestRatePS"] = 0;
                    else
                        dr["InterestRatePS"] = worksheet.Cells[i, 18].Value.ToString();

                    if (worksheet.Cells[i, 19].Value == null)
                        dr["PlacementDate"] = "";
                    else
                        dr["PlacementDate"] = worksheet.Cells[i, 19].Value.ToString();

                    if (worksheet.Cells[i, 20].Value == null)
                        dr["MaturityDate"] = "";
                    else
                        dr["MaturityDate"] = worksheet.Cells[i, 20].Value.ToString();

                    if (worksheet.Cells[i, 21].Value == null)
                        dr["InterestFrequency"] = 0;
                    else
                        dr["InterestFrequency"] = worksheet.Cells[i, 21].Value.ToString();

                    if (worksheet.Cells[i, 22].Value == null)
                        dr["InterestDayCountConvention"] = 0;
                    else
                        dr["InterestDayCountConvention"] = worksheet.Cells[i, 22].Value.ToString();

                    if (worksheet.Cells[i, 23].Value == null)
                        dr["InterestType"] = 0;
                    else
                        dr["InterestType"] = worksheet.Cells[i, 23].Value.ToString();

                    if (worksheet.Cells[i, 24].Value == null)
                        dr["ShariaDeposit"] = 0;
                    else
                        dr["ShariaDeposit"] = worksheet.Cells[i, 24].Value.ToString();

                    if (worksheet.Cells[i, 25].Value == null)
                        dr["WithdrawalDate"] = "19000101";
                    else
                        dr["WithdrawalDate"] = worksheet.Cells[i, 25].Value.ToString();

                    if (worksheet.Cells[i, 26].Value == null)
                        dr["AdjustedInterestRatePS"] = 0;
                    else
                        dr["AdjustedInterestRatePS"] = worksheet.Cells[i, 26].Value.ToString();

                    if (worksheet.Cells[i, 27].Value == null)
                        dr["WithdrawalPrinciple"] = 0;
                    else
                        dr["WithdrawalPrinciple"] = worksheet.Cells[i, 27].Value.ToString();

                    if (worksheet.Cells[i, 28].Value == null)
                        dr["WithdrawalInterest"] = 0;
                    else
                        dr["WithdrawalInterest"] = worksheet.Cells[i, 28].Value.ToString();

                    if (worksheet.Cells[i, 29].Value == null)
                        dr["TotalWithdrawalAmount"] = 0;
                    else
                        dr["TotalWithdrawalAmount"] = worksheet.Cells[i, 29].Value.ToString();

                    if (worksheet.Cells[i, 30].Value == null)
                        dr["RolloverDate"] = 0;
                    else
                        dr["RolloverDate"] = worksheet.Cells[i, 30].Value.ToString();

                    if (worksheet.Cells[i, 31].Value == null)
                        dr["RolloverType"] = 0;
                    else
                        dr["RolloverType"] = worksheet.Cells[i, 31].Value.ToString();

                    if (worksheet.Cells[i, 32].Value == null)
                        dr["NewPrincipleAmount"] = 0;
                    else
                        dr["NewPrincipleAmount"] = worksheet.Cells[i, 32].Value.ToString();

                    if (worksheet.Cells[i, 33].Value == null)
                        dr["NewInterestRatePS"] = 0;
                    else
                        dr["NewInterestRatePS"] = worksheet.Cells[i, 33].Value.ToString();

                    if (worksheet.Cells[i, 34].Value == null)
                        dr["NewMaturityDate"] = 0;
                    else
                        dr["NewMaturityDate"] = worksheet.Cells[i, 34].Value.ToString();

                    if (worksheet.Cells[i, 35].Value == null)
                        dr["AmountToBeTransferred"] = 0;
                    else
                        dr["AmountToBeTransferred"] = worksheet.Cells[i, 35].Value.ToString();

                    if (worksheet.Cells[i, 36].Value == null)
                        dr["StatutoryType"] = 0;
                    else
                        dr["StatutoryType"] = worksheet.Cells[i, 36].Value.ToString();

                    if (worksheet.Cells[i, 37].Value == null)
                        dr["ContactPerson"] = 0;
                    else
                        dr["ContactPerson"] = worksheet.Cells[i, 37].Value.ToString();

                    if (worksheet.Cells[i, 38].Value == null)
                        dr["TelephoneNo"] = 0;
                    else
                        dr["TelephoneNo"] = worksheet.Cells[i, 38].Value.ToString();

                    if (worksheet.Cells[i, 39].Value == null)
                        dr["FaxNo"] = 0;
                    else
                        dr["FaxNo"] = worksheet.Cells[i, 39].Value.ToString();

                    if (worksheet.Cells[i, 40].Value == null)
                        dr["ReferenceNo"] = 0;
                    else
                        dr["ReferenceNo"] = worksheet.Cells[i, 40].Value.ToString();

                    if (worksheet.Cells[i, 41].Value == null)
                        dr["ParentReferenceNo"] = 0;
                    else
                        dr["ParentReferenceNo"] = worksheet.Cells[i, 41].Value.ToString();

                    if (worksheet.Cells[i, 42].Value == null)
                        dr["Description"] = 0;
                    else
                        dr["Description"] = worksheet.Cells[i, 42].Value.ToString();

                    if (worksheet.Cells[i, 43].Value == null)
                        dr["SIReferenceID"] = 0;
                    else
                        dr["SIReferenceID"] = worksheet.Cells[i, 43].Value.ToString();

                    if (worksheet.Cells[i, 44].Value == null)
                        dr["SICreationDateTime"] = 0;
                    else
                        dr["SICreationDateTime"] = worksheet.Cells[i, 44].Value.ToString();

                    if (worksheet.Cells[i, 45].Value == null)
                        dr["CutOffStatus"] = 0;
                    else
                        dr["CutOffStatus"] = worksheet.Cells[i, 45].Value.ToString();

                    if (worksheet.Cells[i, 46].Value == null)
                        dr["CancelationReason"] = 0;
                    else
                        dr["CancelationReason"] = worksheet.Cells[i, 46].Value.ToString();

                    //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                    if (dr["TransactionStatus"].Equals(null) != true ||
                        dr["ActionType"].Equals(null) != true ||
                        dr["InputCancelationDate"].Equals(null) != true ||
                        dr["IMCode"].Equals(null) != true ||
                        dr["IMName"].Equals(null) != true ||
                        dr["CBCode"].Equals(null) != true ||
                        dr["CBName"].Equals(null) != true ||
                        dr["FundCode"].Equals(null) != true ||
                        dr["FundName"].Equals(null) != true ||
                        dr["PlacementBankCode"].Equals(null) != true ||
                        dr["PlacementBankName"].Equals(null) != true ||
                        dr["BranchCode"].Equals(null) != true ||
                        dr["BranchName"].Equals(null) != true ||
                        dr["PlacementBankCashACName"].Equals(null) != true ||
                        dr["PlacementBankCashACNo"].Equals(null) != true ||

                        dr["CCY"].Equals(null) != true ||
                        dr["Principle"].Equals(null) != true ||
                        dr["InterestRatePS"].Equals(null) != true ||
                        dr["PlacementDate"].Equals(null) != true ||
                        dr["MaturityDate"].Equals(null) != true ||
                        dr["InterestFrequency"].Equals(null) != true ||
                        dr["InterestDayCountConvention"].Equals(null) != true ||
                        dr["InterestType"].Equals(null) != true ||
                        dr["ShariaDeposit"].Equals(null) != true ||
                        dr["WithdrawalDate"].Equals(null) != true ||
                        dr["AdjustedInterestRatePS"].Equals(null) != true ||
                        dr["WithdrawalPrinciple"].Equals(null) != true ||
                        dr["WithdrawalInterest"].Equals(null) != true ||
                        dr["TotalWithdrawalAmount"].Equals(null) != true ||
                        dr["RolloverDate"].Equals(null) != true ||

                        dr["RolloverType"].Equals(null) != true ||
                        dr["NewPrincipleAmount"].Equals(null) != true ||
                        dr["NewInterestRatePS"].Equals(null) != true ||
                        dr["NewMaturityDate"].Equals(null) != true ||
                        dr["AmountToBeTransferred"].Equals(null) != true ||
                        dr["StatutoryType"].Equals(null) != true ||
                        dr["ContactPerson"].Equals(null) != true ||
                        dr["TelephoneNo"].Equals(null) != true ||
                        dr["FaxNo"].Equals(null) != true ||
                        dr["ReferenceNo"].Equals(null) != true ||
                        dr["ParentReferenceNo"].Equals(null) != true ||
                        dr["Description"].Equals(null) != true ||
                        dr["SIReferenceID"].Equals(null) != true ||
                        dr["SICreationDateTime"].Equals(null) != true ||
                        dr["CutOffStatus"].Equals(null) != true ||

                        dr["CancellationReason"].Equals(null) != true) { dt.Rows.Add(dr); }
                    i++;

                }
            }

            return dt;
        }

        public string ImportSIDepositoCsv(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table SIDepositoTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.SIDepositoTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromSIDepositoCsv(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
                                    Declare @SIReferenceID nvarchar(50)
                                    Declare @CurReference nvarchar(100) 
                                    Declare @Date Datetime
                                    declare @LastNo int
                                    declare @periodPK int
                                    Declare @BankID nvarchar(200)
                                    Declare @MaxInstrumentPK int
                                    declare @BankPK int
                                    declare @TrxType int
                                    declare @InterestPercent numeric(18,8)
									declare @MaturityDate date

                                    DECLARE A CURSOR FOR 
                                    select SIReferenceID from SIDepositoTemp A
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status in (1,2)
                                    where ActionType <> 3 
                                    --E.FundPK is not null 

                                    Open A
                                    Fetch Next From A
                                    Into @SIReferenceID

                                    While @@FETCH_STATUS = 0
                                    BEGIN
                                    declare @InvestmentPK int
                                    Select @InvestmentPK = max(InvestmentPK) + 1 From investment
                                    if isnull(@InvestmentPK,0) = 0 BEGIN  Select @InvestmentPK = isnull(max(InvestmentPK),0) + 1 From investment END  
                                    declare @DealingPK int
                                    Select @DealingPK = max(DealingPK) + 1 From investment
                                    if isnull(@DealingPK,0) = 0 BEGIN  Select @DealingPK = isnull(max(DealingPK),0) + 1 From investment END  
                                    declare @SettlementPK int
                                    Select @SettlementPK = max(SettlementPK) + 1 From investment
                                    if isnull(@SettlementPK,0) = 0 BEGIN  Select @SettlementPK = isnull(max(SettlementPK),0) + 1 From investment END  
        
                                    select @Date = convert(datetime,convert(varchar(10),InputCancelationDate,120)) 
                                    from SIDepositoTemp A
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
                                    where SIReferenceID = @SIReferenceID  and ActionType <> 3
                                    --E.FundPK is not null and 

                                    select @PeriodPK = PeriodPK 
                                    from Period 
                                    where @Date between DateFrom and DateTo and [Status] = 2

                                    --insrument

                                    select @BankPK = isnull(B.BankPK,0),@InterestPercent = InterestRatePS, @MaturityDate = MaturityDate,@TrxType = ActionType from SIDepositoTemp A
                                    left join BankBranch B on A.BranchCode = B.PTPCode and B.status = 2
									where A.SIReferenceID = @SIReferenceID
									
									if exists (
									select * from Instrument where bankpk = @BankPK and InterestPercent = @InterestPercent and MaturityDate = @MaturityDate and status in (1,2)
									) 
										select @MaxInstrumentPK = InstrumentPK from Instrument where bankpk = @BankPK and InterestPercent = @InterestPercent and MaturityDate = @MaturityDate and status in (1,2)
									else
									begin
										if @TrxType = 1 
										begin
											select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
											select @BankID = ID From Bank Where BankPK = @BankPK and Status = 2
                                    
											set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)

											Insert Into Instrument(InstrumentPK,HistoryPK,Status,ID,Name,InstrumentTypePK,DepositoTypePK,lotinshare,BankPK,InterestPercent,MaturityDate,CurrencyPK,Category,TaxExpensePercent,[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)
											Select @MaxInstrumentPK,1,2,@BankID, 'TDP ' +@BankID,5,1,1,@BankPK,InterestRatePS,MaturityDate,B.CurrencyPK,'',20,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from SIDepositoTemp A 
											left join Currency B on A.CCY = B.ID and B.Status in (1,2)
											where A.SIReferenceID = @SIReferenceID
										end
										else
											set @MaxInstrumentPK = 0
									end
		                    
                                    --reference
                                    set @CurReference = dbo.FGetFundJournalReference(@Date,'INV')

                                    Select @LastNo = max(No) + 1 From CashierReference where Type = 'INV' And PeriodPK = @periodPK and 
                                    substring(right(reference,4),1,2) = month(@Date) 

                                    if exists(Select Top 1 * from cashierReference where Type = 'INV' And PeriodPK = @PeriodPK 
                                    and substring(right(reference,4),1,2) = month(@Date)  )    
                                    BEGIN
	                                    Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = 'INV' And PeriodPK = @periodPK 
	                                    and substring(right(reference,4),1,2) = month(@Date) 
                                    END
                                    ELSE
                                    BEGIN
	                                    Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No) 
	                                    Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,'INV',@CurReference,1 from CashierReference 
                                    END 

                                    INSERT INTO [dbo].[Investment]
                                    ([InvestmentPK],[DealingPK],[SettlementPK],[HistoryPK],[StatusInvestment],[StatusDealing],[StatusSettlement],[Notes]
                                    ,[ValueDate],[MarketPK],[PeriodPK],[Category],[InstructionDate],[Reference],[InstrumentTypePK],[TrxType],[TrxTypeID]
                                    ,[CounterpartPK],[InstrumentPK],[FundPK],[FundCashRefPK],[OrderPrice],[Lot],[LotInShare],[RangePrice],[AcqPrice]
                                    ,[Volume],[Amount],[InterestPercent],[BreakInterestPercent],[AccruedInterest],[DoneAccruedInterest],[LastCouponDate]
                                    ,[NextCouponDate],[MaturityDate],[SettlementDate],[AcqDate],[InvestmentNotes],[DoneLot],[DoneVolume],[DonePrice]
                                    ,[DoneAmount],[Tenor],[CommissionPercent],[LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent]
                                    ,[IncomeTaxSellPercent],[IncomeTaxInterestPercent],[IncomeTaxGainPercent],[CommissionAmount],[LevyAmount],[KPEIAmount]
                                    ,[VATAmount],[WHTAmount],[OTCAmount],[IncomeTaxSellAmount],[IncomeTaxInterestAmount],[IncomeTaxGainAmount],[TotalAmount]
                                    ,[CurrencyRate],[AcqPrice1],[AcqDate1],[AcqPrice2],[AcqDate2],[AcqPrice3],[AcqDate3],[AcqPrice4],[AcqDate4]
                                    ,[AcqPrice5],[AcqDate5],[SettlementMode],[BoardType],[OrderStatus],[InterestDaysType],[InterestPaymentType]
                                    ,[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PriceMode],[BitIsAmortized],[Posted],[PostedBy],[PostedTime]
                                    ,[Revised],[RevisedBy],[RevisedTime],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],[ApprovedTime]
                                    ,[VoidUsersID],[VoidTime],[EntryDealingID],[EntryDealingTime],[UpdateDealingID],[UpdateDealingTime]
                                    ,[ApprovedDealingID],[ApprovedDealingTime],[VoidDealingID],[VoidDealingTime],[EntrySettlementID],[EntrySettlementTime]
                                    ,[UpdateSettlementID],[UpdateSettlementTime],[ApprovedSettlementID],[ApprovedSettlementTime]
                                    ,[VoidSettlementID],[VoidSettlementTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB],[SelectedInvestment]
                                    ,[SelectedDealing],[SelectedSettlement],[BankBranchPK],[BankPK],[AcqVolume],[AcqVolume1],[AcqVolume2],[AcqVolume3]
                                    ,[AcqVolume4],[AcqVolume5],[AcqPrice6],[AcqVolume6],[AcqDate6],[AcqPrice7],[AcqVolume7],[AcqDate7]
                                    ,[AcqPrice8],[AcqVolume8],[AcqDate8],[AcqPrice9],[AcqVolume9],[AcqDate9],[TaxExpensePercent],[TrxBuy],[TrxBuyType]
                                    ,[YieldPercent],[BitIsRounding],[AccruedHoldingAmount],[OldAmount],[OldInterestPercent],[OldMaturityDate],[InvestmentTrType])
     


                                    select @InvestmentPK [InvestmentPK],@DealingPK [DealingPK],@SettlementPK [SettlementPK],1 HistoryPK,2 StatusInvestment,2 StatusDealing,2 StatusSettlement,null Notes,
                                    case when ActionType = 1 then convert(datetime,convert(varchar(10),PlacementDate,120)) else  case when WithdrawalDate = 0 then null else convert(datetime,convert(varchar(10),WithdrawalDate,120)) end end ValueDate,
                                    1 MarketPK,PeriodPK,case when ActionType = 1 then case when DATEDIFF(day,cast(PlacementDate as date),cast(A.MaturityDate as date)) <= 30 then 'Deposito On Call' else 'Deposito Normal' end else 
									case when DATEDIFF(day,cast(WithdrawalDate as date),cast(A.MaturityDate as date)) <= 30 then 'Deposito On Call' else 'Deposito Normal' end end Category,convert(datetime,convert(varchar(10),InputCancelationDate,120)) InstructionDate,
                                    @CurReference Reference,5 InstrumentTypePK,Case when ActionType = 1 then 1 else 2 end TrxType,Case when ActionType = 1 then 'PLACEMENT' else 'LIQUIDATE' end  TrxTypeID,
                                    0 CounterpartPK,@MaxInstrumentPK,E.FundPK,F.FundCashRefPK,1 OrderPrice,0 Lot,100 LotInShare,null RangePrice,0 AcqPrice,
                                     cast(Principle as decimal(22,2)) Volume,cast(Principle as decimal(22,2)) Amount,cast(A.InterestRatePS as decimal(22,8)) InterestPercent,case when ActionType = 1 then 0 else cast(isnull(A.AdjustedInterestRatePS,0) as decimal(22,8)) end BreakInteresPercent,
                                    case when ActionType = 1 then 0 else cast(isnull(WithdrawalInterest,0) as decimal(18,8)) end AccruedInterest,case when ActionType = 1 then 0 else cast(WithdrawalInterest as decimal(22,2)) end DoneAccruedInterest,
                                    null LastCouponDate,null NextCouponDate,convert(datetime,convert(varchar(10),A.MaturityDate,120)) MaturityDate,case when ActionType = 1 then convert(datetime,convert(varchar(10),PlacementDate,120)) else case when WithdrawalDate = 0 then null else convert(datetime,convert(varchar(10),WithdrawalDate,120)) end end SettlementDate,
                                    convert(datetime,convert(varchar(10),PlacementDate,120)) AcqDate,Case when ActionType = 1 then 'PLACEMENT' else 'LIQUIDATE' end InvestmentNotes,0 DoneLot,cast(Principle as decimal(22,2)) DoneVolume,1 DonePrice,cast(Principle as decimal(22,2)) DoneAmount,0 Tenor,
                                    0  CommissionPercent,0  LevyPercent,0 KPEIPercent,0  VATPercent,0  WHTPercent,0  OTCPercent,0  IncomeTaxSellPercent,0 IncomeTaxInterestPercent,0 IncomeTaxGainPercent,
                                    0  CommissionAmount,0  LevyAmount,0 KPEIAmount,0   VATAmount,0   WHTAmount,0  OTCAmount,0  IncomeTaxSellAmount,
                                    0 IncomeTaxInterestAmount,0 IncomeTaxGainAmount,cast(Principle as decimal(22,2))  NetAmount,1 CurrencyRate,
                                    null Acqprice1,null AcqDate1,null Acqprice2,null AcqDate2,null Acqprice3,null AcqDate3,null Acqprice4,null AcqDate4,null Acqprice5,null AcqDate5,
                                    case when ActionType = 1 then 1 else 2 end  SettlementMode,0 BoardType,'M' OrderStatus,
                                    case when InterestDayCountConvention = 1 then 2 when InterestDayCountConvention = 2 then 4 else 0 end InterestDaysType,1 InterestPaymentType,1 PaymentModeOnMaturity,null PaymentInterestSpecificDate,
                                     0 PriceMode,0 BitIsAmortized,0 Posted,@UsersID PostedBy,@LastUpdate PostedTime,0 Revised,null RevisedBy,null RevisedTime,
                                    @UsersID EntryUsersID,@LastUpdate EntryTime,null UpdateusersID,null UpdateTime,@UsersID ApprovedUsersID,@LastUpdate ApprovedTime,
                                    null VoidUsersID,null VoidTime,@UsersID EntryDealingID,@LastUpdate EntryDealingTime,null UpdateDealingID,null UpdateDealingTime,
                                    @UsersID ApprovedDealingID,@LastUpdate ApprovedDealignTime,null VoidDealingID,null VoidDealingTime,
                                    @UsersID EntrySettlementID,@LastUpdate EntrySettlementTime,null UpdateSettlementID,null UpdateSettlementTime,
                                    @UsersID ApprovedSettlementID,@LastUpdate ApprovedSettlementTime,null VoidSettlementID,null VoidSettlementTime,@UsersID DBUsersID,
                                    @UsersID DBTerminalID,@LastUpdate LastUpdate,null LastUpdateDB,0 SelectedInvestment,0 SelectedDealing,0 SelectedSettlement,
                                    H.BankBranchPK BankBranchPK,H.BankPK BankPK,null AcqVolume,null AcqVolume1,null AcqVolume2,null AcqVolume3,null AcqVolume4,null AcqVolume5,
                                    null AcqPrice6,null AcqVolume6,null AcqDate6,null AcqPrice7,null AcqVolume7,null AcqDate7,null AcqPrice8,null AcqVolume8,null AcqDate8,
                                    null AcqPrice9,null AcqVolume9,null AcqDate9,20 TaxExpensePercent,null TrxBuy,null TrxBuyType,0 YieldPercent,0 BitIsRounding,
                                    0 AccruedHoldingAmount,0 OldAmount,0 OldInteresPercent,null OldMaturityDate,3 InvestmentTrType
                                    from SIDepositoTemp A
                                    left join Period B on YEAR(CONVERT(char(8), InputCancelationDate, 112)) = year(DateFrom) and B.status = 2
                                    left join Fund E on A.FundCode = E.SInvestCode and E.status = 2
                                    left join FundCashRef F on E.FundPK = F.FundPK and F.status = 2 and F.Type = 3
                                    left join BankBranch H on A.BranchCode = H.PTPCode and H.status = 2
                                    where SIReferenceID = @SIReferenceID  and ActionType <> 3
                                    --E.FundPK is not null 

                                    Fetch next From A Into @SIReferenceID
                                    END
                                    Close A
                                    Deallocate A
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import SI Deposito Done";

                        }

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromSIDepositoCsv(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ActionType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InputCancelationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BranchCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BranchName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCashACName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlacementBankCashACNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CCY";
            dc.Unique = false;
            dt.Columns.Add(dc);






            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Principle";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestRatePS";
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
            dc.ColumnName = "InterestFrequency";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestDayCountConvention";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ShariaDeposit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AdjustedInterestRatePS";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalPrinciple";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "WithdrawalInterest";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalWithdrawalAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RolloverDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RolloverType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewPrincipleAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);





            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewInterestratePS";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NewMaturityDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AmountToBeTransferred";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatutoryType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ContactPerson";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TelephoneNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FaxNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ParentReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Description";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIReferenceID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SICreationDateTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CutOffStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CancellationReason";
            dc.Unique = false;
            dt.Columns.Add(dc);





            StreamReader sr = new StreamReader(_fileSource);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { ',' });
                dr = dt.NewRow();
                dr["TransactionStatus"] = s[0];
                dr["ActionType"] = s[1];
                dr["InputCancelationDate"] = s[2];
                dr["IMCode"] = s[3];
                dr["IMName"] = s[4];
                dr["CBCode"] = s[5];
                dr["CBName"] = s[6];
                dr["FundCode"] = s[7];
                dr["FundName"] = s[8];
                dr["PlacementBankCode"] = s[9];
                dr["PlacementBankName"] = s[10];
                dr["BranchCode"] = s[11];
                dr["BranchName"] = s[12];
                dr["PlacementBankCashACName"] = s[13];
                dr["PlacementBankCashACNo"] = s[14];
                dr["CCY"] = s[15];
                dr["Principle"] = s[16];
                dr["InterestRatePS"] = s[17];
                dr["PlacementDate"] = s[18];
                dr["MaturityDate"] = s[19];
                dr["InterestFrequency"] = s[20];
                dr["InterestDayCountConvention"] = s[21];
                dr["InterestType"] = s[23];
                dr["ShariaDeposit"] = s[22];
                dr["WithdrawalDate"] = s[24];
                dr["AdjustedInterestRatePS"] = s[25];
                dr["WithdrawalPrinciple"] = s[26];
                dr["WithdrawalInterest"] = s[27];
                dr["TotalWithdrawalAmount"] = s[28];
                dr["RolloverDate"] = s[29];
                dr["RolloverType"] = s[30];
                dr["NewPrincipleAmount"] = s[31];
                dr["NewInterestRatePS"] = s[32];
                dr["NewMaturityDate"] = s[33];
                dr["AmountToBeTransferred"] = s[34];
                dr["StatutoryType"] = s[35];
                dr["ContactPerson"] = s[36];
                dr["TelephoneNo"] = s[37];
                dr["FaxNo"] = s[38];
                dr["ReferenceNo"] = s[39];
                dr["ParentReferenceNo"] = s[40];
                dr["Description"] = s[41];
                dr["SIReferenceID"] = s[42];
                dr["SICreationDateTime"] = s[43];
                dr["CutOffStatus"] = s[44];
                dr["CancellationReason"] = s[45];


                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        #endregion
    }
}
