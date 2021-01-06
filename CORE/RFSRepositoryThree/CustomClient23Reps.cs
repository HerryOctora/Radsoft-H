using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;
using RFSRepository;
using OfficeOpenXml.Drawing.Chart;
using System.Xml;




namespace RFSRepositoryThree
{
    public class CustomClient23Reps
    {
        Host _host = new Host();

        public class FundPortfolioBPHK
        {
            public DateTime Date { get; set; }
            public int InstrumentTypePK { get; set; }
            public string InstrumentTypeName { get; set; }
            public string FundPK { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }
            public DateTime MaturityDate { get; set; }
            public decimal Balance { get; set; }
            public decimal CostValue { get; set; }
            public decimal AvgPrice { get; set; }
            public decimal ClosePrice { get; set; }
            public decimal MarketValue { get; set; }
            public decimal Unrealised { get; set; }
            public decimal PercentOfNav { get; set; }
            public decimal InterestPercent { get; set; }
            public string PeriodeActual { get; set; }
            public decimal AccrualHarian { get; set; }
            public decimal Accrual { get; set; }
            public string CheckedBy { get; set; }
            public string ApprovedBy { get; set; }
            public DateTime AcqDate { get; set; }
            public decimal DiffYear { get; set; }
            public decimal Yield { get; set; }
            public decimal Jan { get; set; }
            public decimal Feb { get; set; }
            public decimal Mar { get; set; }
            public decimal Apr { get; set; }
            public decimal Mei { get; set; }
            public decimal Jun { get; set; }
            public decimal Jul { get; set; }
            public decimal Agu { get; set; }
            public decimal Sep { get; set; }
            public decimal Okt { get; set; }
            public decimal Nov { get; set; }
            public decimal Des { get; set; }

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
                    
                    -- declare @valuedate datetime
declare @FundPK int

--set @valuedate = '05/15/19'
set @FundPK = 1



--DROP table #PVR
Create table #PVR 
(
FundPK int, InstrumentPK int,InstrumentID nvarchar(50),InstrumentName nvarchar(100),AcqDate nvarchar(23), MaturityDate nvarchar(23),
Jan numeric(22,2),Feb numeric(22,2),Mar numeric(22,2),Apr numeric(22,2),
May numeric(22,2),Jun numeric(22,2),Jul numeric(22,2),Aug numeric(22,2),
Sep numeric(22,2),Oct numeric(22,2),Nov numeric(22,2),Dec numeric(22,2)
)

----DROP table #TempPositionMonthly
Create table #TempPositionMonthly  (
FundPK int,
InstrumentPK int,
InstrumentID nvarchar(100),
InstrumentName nvarchar(500),
Balance numeric(22,2),
Bulan int,
AcqDate nvarchar(23),
MaturityDate nvarchar(23)
)


Declare @DateCounter datetime
Declare @StartDate datetime
Declare @EndDate datetime

set @DateCounter = EOMONTH(DATEADD(yy, DATEDIFF(yy, 0, @valuedate), 0)) -- 31 jan
set @StartDate = DATEADD(yy, DATEDIFF(yy, 0, @valuedate), 0) -- 01 jan
set @EndDate = DATEADD(yy, DATEDIFF(yy, 0, @valuedate) + 1, -1) -- 31 dec


while (@DateCounter<= @EndDate)
BEGIN



IF EXISTS (select * from FundPosition A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
AND Date between @StartDate and @DateCounter and A.status = 2 and A.FundPK = @FundPK))

BEGIN

	insert into #TempPositionMonthly
	SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,A.Balance, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101)
	from FundPosition A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
	WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
	AND Date <= @DateCounter and A.status = 2 and A.FundPK = @FundPK)
	Order BY B.ID
END
ELSE
BEGIN
	IF EXISTS(Select * from FundEndYearPortfolio)
	BEGIN
		insert into #TempPositionMonthly
		SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,0, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101)
		from FundEndYearPortfolio A
		left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
		WHERE   A.FundPK = @FundPK
		Order BY B.ID
	END
	ELSE
	BEGIN
		insert into #TempPositionMonthly
		SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,0, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101)
		from FundPosition A
		left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
		WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
		AND Date <= @DateCounter and A.status = 2 and A.FundPK = @FundPK)
		Order BY B.ID
	END

END


SET @StartDate = DATEADD(MONTH,1,@StartDate)
SET @DateCounter = EOMONTH(DATEADD(MONTH,1,@DateCounter),0)
			
END

DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
@query  AS NVARCHAR(MAX)
,@colsForQueryBalance AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(bulan) +',0) ' + QUOTENAME(bulan) 
from (SELECT DISTINCT bulan FROM #TempPositionMonthly) A
order by A.bulan
FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)') 
,1,1,'')


select @cols = STUFF((SELECT distinct ',' + QUOTENAME(bulan) 
from #TempPositionMonthly
				
FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)') 
,1,1,'')


select @colsForQueryBalance = STUFF((SELECT ',isnull(' + QUOTENAME(bulan) +',0) ' + QUOTENAME(Bulan) 
from (SELECT DISTINCT bulan FROM #TempPositionMonthly) A
order by A.bulan
FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)') 
,1,1,'')

set @query = 'SELECT FundPK,InstrumentPK,InstrumentID,InstrumentName,AcqDate,MaturityDate,' + @colsForQuery + ' into #finalResult  from 
(
SELECT FundPK,InstrumentPK,InstrumentID,Bulan,Balance,InstrumentName,AcqDate,MaturityDate FROM #TempPositionMonthly 
) x
pivot 
(
SUM(Balance)
for Bulan in (' + @cols + ')
) p 
order by InstrumentID asc

insert into #PVR
Select FundPK,InstrumentPK,InstrumentID,InstrumentName,AcqDate,MaturityDate,'+@colsForQueryBalance+'  From  #finalResult 
order by InstrumentID' 
exec(@query)

select InstrumentID,InstrumentName,AcqDate,A.MaturityDate,DATEDIFF(YEAR,AcqDate,A.MaturityDate) DiffYear,'0'Yield ,B.InterestPercent,
Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec from #PVR A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2) order by B.InstrumentTypePK asc
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
                                    List<FundPortfolioBPHK> rList = new List<FundPortfolioBPHK>();
                                    while (dr0.Read())
                                    {
                                        FundPortfolioBPHK rSingle = new FundPortfolioBPHK();
                                     
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]); //
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]); //
                                        rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]); //                                       
                                        rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);//                                       
                                        rSingle.AcqDate = Convert.ToDateTime(dr0["AcqDate"]); //
                                        rSingle.DiffYear = dr0["DiffYear"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DiffYear"]);//
                                        rSingle.Yield = dr0["Yield"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Yield"]);//

                                        rSingle.Jan = dr0["Jan"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jan"]);
                                        rSingle.Feb = dr0["Feb"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Feb"]);
                                        rSingle.Mar = dr0["Mar"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Mar"]);
                                        rSingle.Apr = dr0["Apr"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Apr"]);
                                        rSingle.Mei = dr0["May"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["May"]);
                                        rSingle.Jun = dr0["Jun"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jun"]);
                                        rSingle.Jul = dr0["Jul"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jul"]);
                                        rSingle.Agu = dr0["Aug"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Aug"]);
                                        rSingle.Sep = dr0["Sep"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Sep"]);
                                        rSingle.Okt = dr0["Oct"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Oct"]);
                                        rSingle.Nov = dr0["Nov"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Nov"]);
                                        rSingle.Des = dr0["Dec"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Dec"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                    from r in rList
                                                    group r by new { } into rGroup
                                                    select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Laporan Posisi Investasi BPKH ";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE :   ";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Value = _date;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                     
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. NAME";
                                            worksheet.Cells[incRowExcel, 3].Value = "TGL TRANSAKSI";
                                            worksheet.Cells[incRowExcel, 4].Value = "TGL JT TEMPO";
                                            worksheet.Cells[incRowExcel, 5].Value = "YEAR TO MATURITY";
                                            worksheet.Cells[incRowExcel, 6].Value = "% KUPON (GROSS)";
                                            worksheet.Cells[incRowExcel, 7].Value = "YIELD (GROSS)";
                                            worksheet.Cells[incRowExcel, 8].Value = "FACE VALUE JAN";
                                            worksheet.Cells[incRowExcel, 9].Value = "FACE VALUE FEB";
                                            worksheet.Cells[incRowExcel, 10].Value = "FACE VALUE MAR";
                                            worksheet.Cells[incRowExcel, 11].Value = "FACE VALUE APR";
                                            worksheet.Cells[incRowExcel, 12].Value = "FACE VALUE MEI";
                                            worksheet.Cells[incRowExcel, 13].Value = "FACE VALUE JUN";
                                            worksheet.Cells[incRowExcel, 14].Value = "FACE VALUE JUL";
                                            worksheet.Cells[incRowExcel, 15].Value = "FACE VALUE AGU";
                                            worksheet.Cells[incRowExcel, 16].Value = "FACE VALUE SEP";
                                            worksheet.Cells[incRowExcel, 17].Value = "FACE VALUE OKT";
                                            worksheet.Cells[incRowExcel, 18].Value = "FACE VALUE NOV";
                                            worksheet.Cells[incRowExcel, 19].Value = "FACE VALUE DES";
                                        //}
                                        string _range = "A" + incRowExcel + ":S" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":S" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":S" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":S" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":S" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DiffYear;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Yield;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Jan;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Feb;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Mar;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Apr;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Mei;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.Jun;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.Jul;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.Agu;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.Sep;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.Okt;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.Nov;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.Des;
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";

                                        

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

                                            worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["N" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["Q" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["Q" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["R" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["R" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["S" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["S" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                     

                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";

                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                       
                                        incRowExcel = incRowExcel + 2;
                                    }


                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeDetail = "A:S";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 19];

                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).Width = 21;
                                    worksheet.Column(4).Width = 21;
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).Width = 21;
                                    worksheet.Column(9).Width = 21;
                                    worksheet.Column(10).Width = 21;
                                    worksheet.Column(11).Width = 21;
                                    worksheet.Column(12).Width = 21;
                                    worksheet.Column(13).Width = 21;
                                    worksheet.Column(14).Width = 21;
                                    worksheet.Column(15).Width = 21;
                                    worksheet.Column(16).Width = 21;
                                    worksheet.Column(17).Width = 21;
                                    worksheet.Column(18).Width = 21;
                                    worksheet.Column(19).Width = 21;

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
InstrumentID nvarchar(100),                  
FundPK int,                  
FundID nvarchar(100),                  
AvgPrice numeric(38,12),                  
LastVolume numeric(38,4),                  
ClosePrice numeric(38,12),                  
TrxAmount numeric(38,6),              
AcqDate datetime,              
MaturityDate datetime,              
InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200),
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
BitBreakable BIT
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
Category nvarchar(200) ,
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
AcqPrice NUMERIC(18,8)
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
		AND A.InstrumentTypePK  IN (1,5,4,6,16,10,7)         
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
		AND A.InstrumentTypePK  IN (1,5,4,6,16,10,7)        
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

	)A                
	Group By A.InstrumentPK,A.FundPK,A.SettlementDate,A.MaturityDate,A.InterestPercent
	,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
	,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,B.InterestPercent,B.CurrencyPK
,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable


-- LOGIC MASUKIN BOND PER ACQ DATE DAN ACQ VOLUME, UNTUK COVER FIFO LOGIC

DECLARE @FifoBondTrx TABLE
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
Category nvarchar(200) ,
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
BitBreakable BIT,
AcqPrice NUMERIC(18,8)

)

						
DECLARE @FifoInvestmentPK int

DECLARE FIFO CURSOR FOR 
SELECT InvestmentPK 
FROM Investment 
WHERE ValueDate <= @ValueDate and trxType in (1,2) and StatusSettlement = 2  and year(ValueDate) = year(@ValueDate)         
		AND InstrumentTypePK NOT IN (1,5,4,6,16,10,7)  
OPEN FIFO  
FETCH NEXT FROM FIFO INTO @FifoInvestmentPK  

WHILE @@FETCH_STATUS = 0  
BEGIN  
	  
Insert into @FifoBondTrx	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,AcqPrice)   

		Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
	,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
	isnull(A.SettlementDate,'') SettlementDate,isnull(A.MaturityDate,'') MaturityDate,isnull(A.InterestPercent,0) InterestPercent,
	isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.TaxExpensePercent,0) TaxExpensePercent,isnull(A.MarketPK,0) MarketPK,
	isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
	,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
	A.AcqDate,A.BitBreakable,A.AcqPrice
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
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null 
			ELSE A.SettlementDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
			,A.DonePrice AcqPrice       
			from Investment A 
			Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
			where  A.InvestmentPK = @FifoInvestmentPK       AND A.TrxType = 1
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
		,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.DonePrice,A.SettlementDate
		
			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,
			CASE when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable,
			A.AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate IS NOT NULL AND A.AcqVolume > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice
		
			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume1) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate1 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate1 end AcqDate,
			CASE when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice1 AcqPrice

			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate1 IS NOT NULL AND A.AcqVolume1 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate1,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice1
			
			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume2) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate2 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate2 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice2 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate2 IS NOT NULL AND A.AcqVolume2 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate2,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice2


			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume3) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate3 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate3 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice3 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate3 IS NOT NULL AND A.AcqVolume3 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate3,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice3

						UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume4) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate4 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate4 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice4 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate4 IS NOT NULL AND A.AcqVolume4 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate4,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice4

			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume5) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate5 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate5 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice5 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate5 IS NOT NULL AND A.AcqVolume5 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate5,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice5


			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume6) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate6 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate6 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice6 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate6 IS NOT NULL AND A.AcqVolume6 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate6,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice6

						UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume7) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate7 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate7 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice7 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate7 IS NOT NULL AND A.AcqVolume7 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate7,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice7

				UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume8) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate8 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate8 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice8 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate8 IS NOT NULL AND A.AcqVolume8 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate8,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice8

			UNION ALL                  

			select A.InstrumentPK,0 BuyVolume,sum(A.AcqVolume9) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
			Case when C.Type = 1 then null else AcqDate9 end SettlementDate,              
			Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
			Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
			B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
			,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
			,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
			,A.PaymentModeOnMaturity
			,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
			,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate9 end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       ,
			A.AcqPrice9 AcqPrice
			from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
			left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
			where  A.InvestmentPK = @FifoInvestmentPK   AND A.TrxType = 2
			AND (A.AcqDate9 IS NOT NULL AND A.AcqVolume9 > 0)
			Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
			,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
			,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate9,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			,A.AcqPrice9

			)A                
	Group By A.InstrumentPK,A.FundPK,A.SettlementDate,A.MaturityDate,A.InterestPercent
	,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
	,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable,A.AcqPrice

      FETCH NEXT FROM FIFO INTO @FifoInvestmentPK 
END 

CLOSE FIFO  
DEALLOCATE FIFO 


Insert into #ZLogicFundPosition	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,AcqPrice)   

	Select SUM(BuyVolume),SUM(SellVolume),SUM(BuyAmount),SUM(SellAmount),FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,A.AcqPrice FROM @FifoBondTrx A
GROUP BY FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable,A.AcqPrice





 --INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE ) ( BOND ONLY )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
Case when E.InstrumentTypePK in (2,3,8,9,12,13,14,15) then A.AcqPrice else  isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end AvgPrice,                     
Sum(isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0)) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,             
sum(isnull(A.BuyAmount,0) - (isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) 
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0))	TrxAmount
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
AND isnull(A.AcqPrice,0) = isnull(B.AvgPrice,0)
and B.PeriodPK = @PeriodPK where E.Type in (2,5,14,9)
group by  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID, A.AcqPrice,A.AcqDate,A.MaturityDate
,A.InterestPercent,D.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,
D.InterestPaymentType,A.PriceMode,A.BitIsAmortized,E.InstrumentTypePK



 --INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE ) ( EQUITY ONLY )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
  ISNULL(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) AvgPrice,              
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
and B.PeriodPK = @PeriodPK where E.Type in (1)



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




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO ( BOND ONLY )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
A.AvgPrice AvgPrice,              
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
and A.AvgPrice = B.AvgPrice             
	where A.PeriodPK = @PeriodPK              
) and E.Type in (2,5,14,9) and A.periodPK = @PeriodPK      

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO ( EQUITY ONLY )
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
) and E.Type in (1) and A.periodPK = @PeriodPK        

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



-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.ValueDate,isnull(B.FundPK,0),isnull(B.InstrumentPK,0), isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0),0
From CorporateAction A 
left join  #ZFundPosition B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and
ValueDate = @ValueDate and A.status = 2


-- CORPORATE ACTION DIVIDEN SAHAM

Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)   



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
	Select  B.FundPK,B.InstrumentPK,B.Balance +  isnull(C.BalanceFromInv,0) LastBalance
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
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price
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

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price
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

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price from Exercise 
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


	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate





-- UPDATE POSISI ZFUNDPOSITION + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = Case when c.InstrumentTypePK in (2,3,8,9,12,13,14,15) then A.AvgPrice else  isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end ,  
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
select FundPK,A.InstrumentPK,Price, sum(Balance) Balance,A.status
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
and B.ID not like '%-W' and B.ID not like '%-R'

Group By FundPK,A.InstrumentPK,Price,A.status
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
left join instrumentType C on A.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
where C.Type in (1,9,2,5,14)
AND A.LastVolume > 0



                       
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






--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2

where A.status = 2 and B.ID like '%-W' 

Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
 
where A.status = 2 and B.ID like '%-R'


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

Update A set A.LastVolume = A.LastVolume - isnull(B.BalanceExercise,0) from #ZFundPosition A
left join Exercise B on A.InstrumentPK = B.InstrumentRightsPK and B.status = 2
where Date = @ValueDate


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
AcqDate,MaturityDate,InterestPercent,CurrencyPK, Category,TaxExpensePercent,MarketPK
,isnull(InterestDaysType,0),isnull(InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(BankPK,0),isnull(BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
From #ZFundPosition  A
where A.LastVolume > 0
    

Delete FP From FundPosition FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
Where FundPositionPK = @maxEndDayTrailsFundPortfolioPK and I.InstrumentTypePK not in (1,4,16)
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
		) and A.FundPK = @FFundPK 

	
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
where B.Date = @valuedate and InstrumentTypePK in (5,10) and StatusInvestment in (1,2) and TrxType in (2,3)
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

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

           #region Fund Portfolio BPKH
            if (_FundAccountingRpt.ReportName.Equals("Fund Portfolio BPKH"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                             // FUND
                            cmd.CommandText = @"
                            
                            select distinct A.FundPK from Fund A
                            where A.status in (1,2)
                            " + _paramFund;

                            cmd.CommandTimeout = 0;

                           
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (dr0.HasRows)
                                {

                                    string filePath = Tools.ReportsPath + "FundPortfolioBPKH" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundPortfolioBPKH" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PortfolioValuationReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Fund Portfolio BPKH");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundPortfolioBPHK> rList = new List<FundPortfolioBPHK>();
                                        while (dr0.Read())
                                        {
                                            FundPortfolioBPHK rSingle = new FundPortfolioBPHK();
                                            rSingle.FundPK = Convert.ToString(dr0["FundPK"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByFund =
                                        from r in rList
                                        orderby r.FundPK ascending
                                        group r by new { r.FundPK } into rGroup
                                        select rGroup;


                                        //int incRowExcel = 0;

                                        //incRowExcel = incRowExcel + 3;


                                        int incRowExcel = 1;

                                        foreach (var rsHeader in GroupByFund)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(rsHeader.Key.FundPK);
                                            incRowExcel ++;

                                            using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                            {
                                                DbCon1.Open();
                                                using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                                {

                                                    cmd1.CommandText = @"
                    

                                               --DROP table #PVR
                                                Create table #PVR 
                                                (
                                                FundPK int, InstrumentPK int,InstrumentID nvarchar(50),InstrumentName nvarchar(100),AcqDate nvarchar(23), MaturityDate nvarchar(23),AvgPrice numeric(22,8),
                                                Jan numeric(22,2),Feb numeric(22,2),Mar numeric(22,2),Apr numeric(22,2),
                                                May numeric(22,2),Jun numeric(22,2),Jul numeric(22,2),Aug numeric(22,2),
                                                Sep numeric(22,2),Oct numeric(22,2),Nov numeric(22,2),Dec numeric(22,2)
                                                )

                                                --DROP table #TempPositionMonthly
                                                Create table #TempPositionMonthly  (
                                                FundPK int,
                                                InstrumentPK int,
                                                InstrumentID nvarchar(100),
                                                InstrumentName nvarchar(500),
                                                Balance numeric(22,2),
                                                Bulan int,
                                                AcqDate nvarchar(23),
                                                MaturityDate nvarchar(23),
                                                AvgPrice numeric(22,8)
                                                )


                                                Declare @DateCounter datetime
                                                Declare @StartDate datetime
                                                Declare @EndDate datetime

                                                set @DateCounter = EOMONTH(DATEADD(yy, DATEDIFF(yy, 0, @valuedate), 0)) -- 31 jan
                                                set @StartDate = DATEADD(yy, DATEDIFF(yy, 0, @valuedate), 0) -- 01 jan
                                                set @EndDate = DATEADD(yy, DATEDIFF(yy, 0, @valuedate) + 1, -1) -- 31 dec


                                                while (@DateCounter<= @EndDate)
                                                BEGIN



                                                IF EXISTS (select * from FundPosition A
                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
                                                WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
                                                AND Date between @StartDate and @DateCounter and A.status = 2 and A.FundPK = @FundPK))

                                                BEGIN

	                                                insert into #TempPositionMonthly
	                                                SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,A.Balance, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101),A.AvgPrice
	                                                from FundPosition A
	                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
	                                                WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
	                                                AND Date <= @DateCounter and A.status = 2 and A.FundPK = @FundPK)
	                                                Order BY B.ID
                                                END
                                                ELSE
                                                BEGIN
	                                                IF EXISTS(Select * from FundEndYearPortfolio)
	                                                BEGIN
		                                                insert into #TempPositionMonthly
		                                                SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,0, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101),A.AvgPrice
		                                                from FundEndYearPortfolio A
		                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
		                                                WHERE   A.FundPK = @FundPK
		                                                Order BY B.ID
	                                                END
	                                                ELSE
	                                                BEGIN
		                                                insert into #TempPositionMonthly
		                                                SELECT A.FundPK,A.InstrumentPK,B.ID,B.Name,0, month(@DateCounter) bulan,convert(varchar, isnull(A.AcqDate,'01/01/1900'), 101),convert(varchar, isnull(A.MaturityDate,'01/01/1900'), 101),A.AvgPrice
		                                                from FundPosition A
		                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
		                                                WHERE  Date = (SELECT MAX(date) FROM FundPosition WHERE status = 2
		                                                AND Date <= @DateCounter and A.status = 2 and A.FundPK = @FundPK)
		                                                Order BY B.ID
	                                                END

                                                END


                                                SET @StartDate = DATEADD(MONTH,1,@StartDate)
                                                SET @DateCounter = EOMONTH(DATEADD(MONTH,1,@DateCounter),0)
			
                                                END

                                                DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                                                @query  AS NVARCHAR(MAX)
                                                ,@colsForQueryBalance AS NVARCHAR(MAX)

                                                select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(bulan) +',0) ' + QUOTENAME(bulan) 
                                                from (SELECT DISTINCT bulan FROM #TempPositionMonthly) A
                                                order by A.bulan
                                                FOR XML PATH(''), TYPE
                                                ).value('.', 'NVARCHAR(MAX)') 
                                                ,1,1,'')


                                                select @cols = STUFF((SELECT distinct ',' + QUOTENAME(bulan) 
                                                from #TempPositionMonthly
				
                                                FOR XML PATH(''), TYPE
                                                ).value('.', 'NVARCHAR(MAX)') 
                                                ,1,1,'')


                                                select @colsForQueryBalance = STUFF((SELECT ',isnull(' + QUOTENAME(bulan) +',0) ' + QUOTENAME(Bulan) 
                                                from (SELECT DISTINCT bulan FROM #TempPositionMonthly) A
                                                order by A.bulan
                                                FOR XML PATH(''), TYPE
                                                ).value('.', 'NVARCHAR(MAX)') 
                                                ,1,1,'')

                                                set @query = 'SELECT FundPK,InstrumentPK,InstrumentID,InstrumentName,AcqDate,MaturityDate,AvgPrice,' + @colsForQuery + ' into #finalResult  from 
                                                (
                                                SELECT FundPK,InstrumentPK,InstrumentID,Bulan,Balance,InstrumentName,AcqDate,MaturityDate,AvgPrice FROM #TempPositionMonthly 
                                                ) x
                                                pivot 
                                                (
                                                SUM(Balance)
                                                for Bulan in (' + @cols + ')
                                                ) p 
                                                order by InstrumentID asc

                                                insert into #PVR
                                                Select FundPK,InstrumentPK,InstrumentID,InstrumentName,AcqDate,MaturityDate,AvgPrice,'+@colsForQueryBalance+'  From  #finalResult 
                                                order by InstrumentID' 
                                                exec(@query)

                                                select InstrumentID,InstrumentName,AcqDate,A.MaturityDate,DATEDIFF(YEAR,AcqDate,A.MaturityDate) DiffYear,'0'Yield,A.AvgPrice ,B.InterestPercent,
                                                Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec from #PVR A
                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2) order by B.InstrumentTypePK asc
                                                                        ";

                                                    cmd1.CommandTimeout = 0;
                                                    cmd1.Parameters.AddWithValue("@FundPK", rsHeader.Key.FundPK);
                                                    cmd1.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

                                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                    {
                                                        if (dr1.HasRows)
                                                        {
                                                            


                                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                        using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                        {


                                                            //ATUR DATA GROUPINGNYA DULU
                                                            List<FundPortfolioBPHK> rList1 = new List<FundPortfolioBPHK>();
                                                            while (dr1.Read())
                                                            {
                                                                FundPortfolioBPHK rSingle1 = new FundPortfolioBPHK();

                                                                rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]); //
                                                                rSingle1.InstrumentName = Convert.ToString(dr1["InstrumentName"]); //
                                                                rSingle1.MaturityDate = Convert.ToDateTime(dr1["MaturityDate"]); //                                       
                                                                rSingle1.InterestPercent = dr1["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["InterestPercent"]);//                                       
                                                                rSingle1.AcqDate = Convert.ToDateTime(dr1["AcqDate"]); //
                                                                rSingle1.DiffYear = dr1["DiffYear"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["DiffYear"]);//
                                                                rSingle1.Yield = dr1["Yield"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Yield"]);//
                                                                rSingle1.AvgPrice = dr1["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["AvgPrice"]);//

                                                                rSingle1.Jan = dr1["Jan"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Jan"]);
                                                                rSingle1.Feb = dr1["Feb"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Feb"]);
                                                                rSingle1.Mar = dr1["Mar"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Mar"]);
                                                                rSingle1.Apr = dr1["Apr"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Apr"]);
                                                                rSingle1.Mei = dr1["May"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["May"]);
                                                                rSingle1.Jun = dr1["Jun"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Jun"]);
                                                                rSingle1.Jul = dr1["Jul"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Jul"]);
                                                                rSingle1.Agu = dr1["Aug"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Aug"]);
                                                                rSingle1.Sep = dr1["Sep"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Sep"]);
                                                                rSingle1.Okt = dr1["Oct"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Oct"]);
                                                                rSingle1.Nov = dr1["Nov"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Nov"]);
                                                                rSingle1.Des = dr1["Dec"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Dec"]);

                                                                rList1.Add(rSingle1);

                                                            }

                                                            incRowExcel++;
                                                            var GroupByReference =
                                                            from r1 in rList1
                                                            group r1 by new { } into rGroup1
                                                            select rGroup1;

                                                            //int incRowExcel = 0;

                                                            foreach (var rsHeader1 in GroupByReference)
                                                            {
                                                                incRowExcel = incRowExcel + 1;
                                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                worksheet.Cells[incRowExcel, 2].Value = "Laporan Posisi Investasi BPKH ";
                                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                worksheet.Cells[incRowExcel, 5].Value = "DATE :   ";
                                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                worksheet.Cells[incRowExcel, 6].Value = _FundAccountingRpt.ValueDateTo;
                                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                                incRowExcel++;
                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                                                incRowExcel++;

                                                                //Row B = 3
                                                                int RowB = incRowExcel;
                                                                int RowG = incRowExcel + 1;


                                                                worksheet.Cells[incRowExcel, 1].Value = "NO";
                                                                worksheet.Cells[incRowExcel, 2].Value = "INS. NAME";
                                                                worksheet.Cells[incRowExcel, 3].Value = "TGL TRANSAKSI";
                                                                worksheet.Cells[incRowExcel, 4].Value = "TGL JT TEMPO";
                                                                worksheet.Cells[incRowExcel, 5].Value = "YEAR TO MATURITY";
                                                                worksheet.Cells[incRowExcel, 6].Value = "% KUPON (GROSS)";
                                                                worksheet.Cells[incRowExcel, 7].Value = "YIELD (GROSS)";
                                                                worksheet.Cells[incRowExcel, 8].Value = "HARGA BELI";
                                                                worksheet.Cells[incRowExcel, 9].Value = "FACE VALUE JAN";
                                                                worksheet.Cells[incRowExcel, 10].Value = "FACE VALUE FEB";
                                                                worksheet.Cells[incRowExcel, 11].Value = "FACE VALUE MAR";
                                                                worksheet.Cells[incRowExcel, 12].Value = "FACE VALUE APR";
                                                                worksheet.Cells[incRowExcel, 13].Value = "FACE VALUE MEI";
                                                                worksheet.Cells[incRowExcel, 14].Value = "FACE VALUE JUN";
                                                                worksheet.Cells[incRowExcel, 15].Value = "FACE VALUE JUL";
                                                                worksheet.Cells[incRowExcel, 16].Value = "FACE VALUE AGU";
                                                                worksheet.Cells[incRowExcel, 17].Value = "FACE VALUE SEP";
                                                                worksheet.Cells[incRowExcel, 18].Value = "FACE VALUE OKT";
                                                                worksheet.Cells[incRowExcel, 19].Value = "FACE VALUE NOV";
                                                                worksheet.Cells[incRowExcel, 20].Value = "FACE VALUE DES";
                                                                //}
                                                                string _range = "A" + incRowExcel + ":T" + incRowExcel;

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
                                                                foreach (var rsDetail in rsHeader1)
                                                                {



                                                                    //ThickBox Border HEADER

                                                                    worksheet.Cells["A" + RowB + ":T" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + RowB + ":T" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + RowB + ":T" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + RowB + ":T" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate;
                                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.MaturityDate;
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DiffYear;
                                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,####0.00";
                                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,####0.00";
                                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Yield;
                                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,####0.00";
                                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AvgPrice;
                                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,####0.00";
                                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Jan;
                                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Feb;
                                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.Mar;
                                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Apr;
                                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Mei;
                                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Jun;
                                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.Jul;
                                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.Agu;
                                                                    worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.Sep;
                                                                    worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.Okt;
                                                                    worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.Nov;
                                                                    worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.Des;
                                                                    worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0.00";



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

                                                                worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["N" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["N" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["O" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["P" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["Q" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["Q" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["R" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["R" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["S" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["S" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                                worksheet.Cells["T" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["T" + RowB + ":T" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["T" + RowB + ":T" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["T" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                                worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";

                                                             
                                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                                                worksheet.Cells[incRowExcel, 14].Calculate();
                                                                worksheet.Cells[incRowExcel, 15].Calculate();
                                                                worksheet.Cells[incRowExcel, 16].Calculate();
                                                                worksheet.Cells[incRowExcel, 17].Calculate();
                                                                worksheet.Cells[incRowExcel, 18].Calculate();
                                                                worksheet.Cells[incRowExcel, 19].Calculate();
                                                                worksheet.Cells[incRowExcel, 20].Calculate();

                                                                incRowExcel = incRowExcel + 2;
                                                            }
                                                           
                                                        }


                                                        }

                                                    }
                                                }
                                            }
                                        }


                                        worksheet.Row(incRowExcel).PageBreak = true;

                                        string _rangeDetail = "A:T";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 20];

                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).AutoFit();
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 21;
                                        worksheet.Column(10).Width = 21;
                                        worksheet.Column(11).Width = 21;
                                        worksheet.Column(12).Width = 21;
                                        worksheet.Column(13).Width = 21;
                                        worksheet.Column(14).Width = 21;
                                        worksheet.Column(15).Width = 21;
                                        worksheet.Column(16).Width = 21;
                                        worksheet.Column(17).Width = 21;
                                        worksheet.Column(18).Width = 21;
                                        worksheet.Column(19).Width = 21;
                                        worksheet.Column(20).Width = 21;

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
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                    }
                                    
                                }
                                return true;
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

           
            else
            {
                return false;
            }
        }

    }
}